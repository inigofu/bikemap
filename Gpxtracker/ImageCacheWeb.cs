//------------------------------------------------------------------------------
// <copyright file="ImageCacheWeb.cs" company="">
// Copyright (c) 2009 GPS Track Viewer development team
// http://GpxTracker.codeplex.com/
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, 
// MA 02110-1301, USA.
// </copyright>
// <author></author>
// <date>22.09.2010</date>
// <summary>Contains information about the ImageCacheWeb class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Net;

	/// <summary>
	/// Represents the image cache on the web.
	/// </summary>
	class ImageCacheWeb
	{
		private LinkedList<Tile> _requestQueue = new LinkedList<Tile>();

		private MapDataSource _mapDataSource;

		private bool _serverError = false;
		private int _maxQueueLength = 20;
		private int _requestsOpen = 0;

		private WebStreamReadyCallback _callback;

		/// <summary>
		/// This class stores the request state of the request.
		/// </summary>
		private class RequestState
		{
			public WebRequest request;
			public Tile tile;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageCacheWeb"/> class.
		/// </summary>
		/// <param name="mapDataSource">The map data source.</param>
		/// <param name="callback">The callback for ready images.</param>
		public ImageCacheWeb(MapDataSource mapDataSource, WebStreamReadyCallback callback)
		{
			_mapDataSource = mapDataSource;
			_callback = callback;
		}

		/// <summary>
		/// The callback for ready images.
		/// </summary>
		public delegate void WebStreamReadyCallback(Tile tile, Stream stream);

		/// <summary>
		/// Requests the specified tile.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <param name="zoom">The zoom factor.</param>
		public void Request(int x, int y, int zoom)
		{
			if (_mapDataSource == null)
				return;

			if (_serverError == true)
				return;

			Tile tile = new Tile();
			tile.X = x;
			tile.Y = y;
			tile.Zoom = zoom;
			tile.IsCached = false;
			tile.OnDisk = false;
			tile.IsRequested = false;
	
			lock (_requestQueue)
			{
				foreach (Tile t in _requestQueue)
				{
					if ((t.X == x) && (t.Y == y) && (t.Zoom == zoom))
					{
						return;
					}
				}
					
				if (_requestsOpen < 2)
				{
					_requestsOpen++;
					tile.IsRequested = true;
				}
	
				if (_requestQueue.Count > _maxQueueLength)
					_requestQueue.RemoveLast();

				_requestQueue.AddFirst(tile);
			}

			if (tile.IsRequested)
			{
				DownloadTileAsync(tile); 
			}
		}

		private void DownloadTileAsync(Tile tile)
		{
			Uri uri = _mapDataSource.GetUri(tile.X, tile.Y, tile.Zoom);
			WebRequest request;

			try
			{
				request = WebRequest.Create(uri);
			}
			catch (NotSupportedException e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message + "\n" + uri);
				return;
			}

			RequestState requestState = new RequestState();
            requestState.request = request;
			requestState.tile = tile;

			// BeginGetResponse is really slow if remote name cannot be resolved
			request.BeginGetResponse(new AsyncCallback(RequestCallback), requestState);
		}

		private void RequestCallback(IAsyncResult asyncResult)
		{
			RequestState requestState = (RequestState)asyncResult.AsyncState;
			WebRequest request = requestState.request;

			WebResponse response = null;

			try
			{
				response = request.EndGetResponse(asyncResult);
			}
			catch (WebException e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
			}

			Tile tile = requestState.tile;

			if (response == null)
			{
				lock (_requestQueue)
				{
					_requestsOpen--;
					_requestQueue.Remove(tile);
				}

				_serverError = true;

				return;
			}

			Stream resp_stream = response.GetResponseStream();
			MemoryStream mem_stream = WriteToMemStream(resp_stream);

			if (_callback != null)
			{
				_callback(tile, mem_stream);
			}

			tile.IsRequested = false;		// redundant in ImageCacheDisk.Add()

			resp_stream.Close();
			mem_stream.Close();

			Tile next_tile = null;

			lock (_requestQueue)
			{
				_requestsOpen--;
				_requestQueue.Remove(tile);

				LinkedListNode<Tile> next_node = _requestQueue.First;

				while (next_node != null)
				{
					if (!next_node.Value.IsRequested)
					{
						next_tile = next_node.Value;

						next_tile.IsRequested = true;
						_requestsOpen++;
						break;
					}

					next_node = next_node.Next;
				}
			}

			if (next_tile != null)
			{
				DownloadTileAsync(next_tile);
			}
		}

		private static MemoryStream WriteToMemStream(Stream stream)
		{
			int read;
			byte[] buffer = new byte[32768];
			MemoryStream ms = new MemoryStream(32768);

			while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
			{
				ms.Write(buffer, 0, read);
			}

			return ms;
		}
	}
}

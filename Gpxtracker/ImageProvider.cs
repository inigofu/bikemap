//------------------------------------------------------------------------------
// <copyright file="ImageProvider.cs" company="">
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
// <summary>Contains information about the ImageProvider class.</summary>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Threading;

namespace GpxTracker
{
	/// <summary>
	/// Represents an image provider. It serves images as requested.
	/// </summary>
	class ImageProvider : IDisposable
	{
		private ImageCacheWeb imageCacheWeb;
		private ImageCacheDisk imageCacheDisk;
		private ImageCacheMemory imageCacheMemory;

		private Bitmap BitmapLoading;
		private Bitmap BitmapMissing;
		
		private TileReadyCallback tileReadyCallback;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ImageProvider"/> class.
		/// </summary>
		public ImageProvider()
		{
			// TODO: replace with tileSize
			int bitmapSize = 256;

			BitmapLoading = new Bitmap(bitmapSize, bitmapSize);
			BitmapMissing = new Bitmap(bitmapSize, bitmapSize);

			using (Graphics g = Graphics.FromImage(BitmapLoading))
			{
				g.FillRectangle(Brushes.LightGray, 0, 0, bitmapSize, bitmapSize);
			}

			using (Graphics g = Graphics.FromImage(BitmapMissing))
			{
				g.FillRectangle(Brushes.Gray, 0, 0, bitmapSize, bitmapSize);
			}
		}

		/// <summary>
		/// Represents a callback for tiles that signal their ready status.
		/// </summary>
		public delegate void TileReadyCallback(int x, int y, int zoom, Bitmap bmp);

		/// <summary>
		/// Sets the map source.
		/// </summary>
		/// <param name="mapDataSource">The map data source.</param>
		public void SetMapSource(MapDataSource mapDataSource)
		{
			imageCacheDisk = new ImageCacheDisk(mapDataSource.LocalPath, DiskStreamReady);
			imageCacheWeb = new ImageCacheWeb(mapDataSource, WebStreamReady);
			imageCacheMemory = new ImageCacheMemory();
		}

		/// <summary>
		/// Registers the render single tile ready callback.
		/// </summary>
		/// <param name="cb">The callback delegate.</param>
		public void RegisterRenderSingleTileCB(TileReadyCallback cb)
		{
			tileReadyCallback = cb;
		}

		/// <summary>
		/// Gets a tile from the storage.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <param name="zoom">The zoom factor.</param>
		/// <returns>The bitmap image.</returns>
		public Bitmap GetTileBitmap(int x, int y, int zoom)
		{
			Tile tile = imageCacheDisk.GetTile(x, y, zoom);

			if (tile == null)
			{
				// Tile was downloaded before, but could not be written to disk
				Bitmap bmp = imageCacheMemory.Get(x, y, zoom);

				if (bmp != null)
					return bmp;

				imageCacheWeb.Request(x, y, zoom);
				return BitmapLoading;
			}
			else if (tile.IsRequested)
			{
				return BitmapLoading;
			}
			else if (tile.IsCached)
			{
				return imageCacheMemory.Get(x, y, zoom);
			}
			else if (tile.OnDisk)
			{
				imageCacheDisk.Request(x, y, zoom);
				return BitmapLoading;
			}
			else
			{
				return BitmapLoading;
			};
		}

		/// <summary>
		/// Called by the web image provider when a image is ready.
		/// </summary>
		/// <param name="tile">The tile.</param>
		/// <param name="stream">The stream.</param>
		public void WebStreamReady(Tile tile, Stream stream)
		{
			Bitmap bmp = new Bitmap(stream);

			imageCacheDisk.Add(stream, tile);
			imageCacheMemory.Add(bmp, tile);

			tileReadyCallback.Invoke(tile.X, tile.Y, tile.Zoom, bmp);
		}


		/// <summary>
		/// Called by the disk image provider when a image is ready.
		/// </summary>
		/// <param name="tile">The tile.</param>
		/// <param name="stream">The stream.</param>
		public void DiskStreamReady(Tile tile, Stream stream)
		{
			Bitmap bmp = new Bitmap(stream);
			imageCacheMemory.Add(bmp, tile);

			tileReadyCallback.Invoke(tile.X, tile.Y, tile.Zoom, bmp);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (BitmapMissing != null)
			{
				BitmapMissing.Dispose();
				BitmapMissing = null;
			}

			if (BitmapLoading != null)
			{
				BitmapLoading.Dispose();
				BitmapLoading = null;
			}

			GC.SuppressFinalize(this);
		}
	}
}

//------------------------------------------------------------------------------
// <copyright file="ImageCacheMemory.cs" company="">
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
// <summary>Contains information about the ImageCacheMemory class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System.Collections.Generic;
	using System.Drawing;

	/// <summary>
	/// Represents the image cache in memory.
	/// </summary>
	class ImageCacheMemory
	{
		private struct Item
		{
			public Tile				Tile;
			public Bitmap			Bitmap;
		}

		private LinkedList<Item> _list = new LinkedList<Item>();

		private readonly object _lock = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageCacheMemory"/> class.
		/// </summary>
		public ImageCacheMemory()
		{
			// 1 Image = 256kB
			// 200 Images == 52 MB
			MaxItemCount = 200;
		}

		/// <summary>
		/// Gets the current item count.
		/// </summary>
		/// <value>The current item count.</value>
		public int CurrentItemCount { get { return _list.Count; } }

		/// <summary>
		/// Gets or sets the maximum item count.
		/// </summary>
		/// <value>The maximum item count.</value>
		public int MaxItemCount { get; set; }

		/// <summary>
		/// Adds the specified image source.
		/// </summary>
		/// <param name="bmp">The image source.</param>
		/// <param name="tile">The corresponding tile.</param>
		public void Add(Bitmap bmp, Tile tile)
		{
			Item item = new Item();
			item.Bitmap = bmp;
			item.Tile = tile;

			lock (_lock)
			{
				if (_list.Count > MaxItemCount)
				{
		//			Console.WriteLine("Item {0}/{1}/{2} removed.", it.Zoom, it.X, it.Y);

					Item lastItem = _list.Last.Value;
					lastItem.Bitmap.Dispose();
					lastItem.Bitmap = null;
					lastItem.Tile.IsCached = false;

					_list.RemoveLast();
				}

				_list.AddFirst(item);
			}

			tile.IsCached = true;
	//		Console.WriteLine("Item {0}/{1}/{2} added.", zoom, x, y);
		}

		/// <summary>
		/// Gets the image for the specified tile.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <param name="zoom">The zoom factor.</param>
		/// <returns>The bitmap image.</returns>
		public Bitmap Get(int x, int y, int zoom)
		{
			lock (_lock)
			{
				foreach (Item node in _list)
				{
					Tile tile = node.Tile;

					if (tile.X == x && tile.Y == y && tile.Zoom == zoom)
					{
						_list.Remove(node);
						_list.AddFirst(node);

						return node.Bitmap;
					}
				}
			}

			return null;
		}
	}
}

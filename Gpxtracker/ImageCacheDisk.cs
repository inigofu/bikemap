//------------------------------------------------------------------------------
// <copyright file="ImageCacheDisk.cs" company="">
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
// <summary>Contains information about the ImageCacheDisk class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.IO;
	using System.Drawing;
	using System.Threading;
	using System.Windows.Forms;

	/// <summary>
	/// Represents the image cache on the disk.
	/// </summary>
	class ImageCacheDisk
	{
		private readonly object _lockObject = new object();

		private string Root;
		private DiskStreamReadyCallback _callback;

		private Dictionary<int, Dictionary<int, Dictionary<int, Tile>>> Map = new Dictionary<int, Dictionary<int, Dictionary<int, Tile>>>();

		/// <summary>
		/// The callback for ready images.
		/// </summary>
		public delegate void DiskStreamReadyCallback(Tile tile, Stream stream);

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageCacheDisk"/> class.
		/// </summary>
		/// <param name="folderName">Name of the root folder.</param>
		/// <param name="callback">The callback delegate to call when the data is ready.</param>
		public ImageCacheDisk(string folderName, DiskStreamReadyCallback callback)
		{
			_callback = callback;

			Map.Clear();
			Root = Path.Combine(FileSystem.LocalMapsFolder, folderName);

			DirectoryInfo rootInfo = new DirectoryInfo(Root);

			if (!rootInfo.Exists)
			{
				try
				{
					rootInfo.Create();
				}
				catch (Exception)
				{
					MessageBox.Show("Could not write to folder \"" + rootInfo.FullName + 
						"\". Please choose a different folder.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					
					return;
				}
			}
			TimeSpan maxAge = Properties.Settings.Default.MapDataMaxAge;
			DateTime minDate = DateTime.UtcNow - maxAge;

			DirectoryInfo[] zoom_dir_list = rootInfo.GetDirectories();
			foreach (DirectoryInfo zoom_info in zoom_dir_list)
			{
				int key_zoom;

				if (int.TryParse(zoom_info.Name, out key_zoom))
				{
					Dictionary<int,Dictionary<int,Tile>> dict_zoom = new Dictionary<int, Dictionary<int, Tile>>();

					DirectoryInfo[] x_dir_list = zoom_info.GetDirectories();
					foreach (DirectoryInfo x_info in x_dir_list)
					{
						int key_x;

						if (int.TryParse(x_info.Name, out key_x))
						{
							Dictionary<int, Tile> dict_x = new Dictionary<int, Tile>();

							FileInfo[] y_file_list = x_info.GetFiles();
							foreach (FileInfo y_info in y_file_list)
							{
								int key_y;

								if (int.TryParse(Path.GetFileNameWithoutExtension(y_info.Name), out key_y))
								{
									// use only if file is not too old
									if (y_info.CreationTime > minDate)
									{
										Tile tile = new Tile();
										tile.X = key_x;
										tile.Y = key_y;
										tile.Zoom = key_zoom;
										tile.IsCached = false;
										tile.OnDisk = true;

										dict_x.Add(key_y, tile);
									}
								}
							}
							
							dict_zoom.Add(key_x, dict_x);
						}
					}

					Map.Add(key_zoom, dict_zoom);
				}
			}
		}

		/// <summary>
		/// Adds the specified image stream (from the web probably).
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="tile">The tile it belongs to.</param>
		public void Add(Stream stream, Tile tile)
		{
			string fname = Root + "\\" + tile.Zoom + "\\" + tile.X + "\\" + tile.Y + ".png";

			if (WriteToFile(fname, stream))
			{
				tile.OnDisk = true;
				tile.IsRequested = false;

				SetTile(tile);
			}
		}

		/// <summary>
		/// Gets the tile info for a specific coordinate.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <param name="zoom">The zoom factor.</param>
		/// <returns>The tile info.</returns>
		public Tile GetTile(int x, int y, int zoom)
		{
			lock (_lockObject)
			{
				Dictionary<int, Dictionary<int, Tile>> dict_z;

				if (Map.TryGetValue(zoom, out dict_z))
				{
					Dictionary<int, Tile> dict_x;

					if (dict_z.TryGetValue(x, out dict_x))
					{
						Tile tile;

						if (dict_x.TryGetValue(y, out tile))
						{
							return tile;
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Requests the specified tile and return immediately.
		/// </summary>
		/// <param name="x">The x-coordinate.</param>
		/// <param name="y">The y-coordinate.</param>
		/// <param name="zoom">The zoom factor.</param>
		public void Request(int x, int y, int zoom)
		{
			Tile tile = GetTile(x, y, zoom);
			ThreadPool.QueueUserWorkItem(new WaitCallback(RequestThread), tile);
		}

		private void RequestThread(object parameter)
		{
			Tile tile = parameter as Tile;

			if (tile == null)
				return;				// should never happen

			if (tile.IsRequested)
				return;

			tile.IsRequested = true;

			int x = tile.X;
			int y = tile.Y;
			int zoom = tile.Zoom;

			string fname = Root + "\\" + zoom + "\\" + x + "\\" + y + ".png";

			FileStream fs = null;

			try
			{
				using (fs = File.Open(fname, FileMode.Open, FileAccess.Read))
				{
					if (_callback != null)
					{
						_callback(tile, fs);
					}
				}
			}
			catch (ArgumentException)
			{
				// file is probably damaged
				File.Delete(fname);			// TODO: catch exceptions if any

				tile.OnDisk = false;
				RemoveTile(tile);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				throw;
			}

			tile.IsRequested = false;
		}

		private static bool WriteToFile(string fname, Stream stream)
		{
			int read;
			byte[] buffer = new byte[32768];

			try
			{
				stream.Position = 0;

				Directory.CreateDirectory(Path.GetDirectoryName(fname));

				using (FileStream fs = new FileStream(fname, FileMode.Create, FileAccess.Write))
				{
					while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
					{
						fs.Write(buffer, 0, read);
					}
				}
			}
			catch
			{
				return false;
			}

			return true;
		}

		private void RemoveTile(Tile tile)
		{
			lock (_lockObject)
			{
				Map[tile.Zoom][tile.X].Remove(tile.Y);
			}
		}

		private void SetTile(Tile tile)
		{
			lock (_lockObject)
			{
				Dictionary<int, Dictionary<int, Tile>> dict_z;
				Dictionary<int, Tile> dict_x;

				if (!Map.TryGetValue(tile.Zoom, out dict_z))
					Map.Add(tile.Zoom, dict_z = new Dictionary<int, Dictionary<int, Tile>>());

				if (!dict_z.TryGetValue(tile.X, out dict_x))
					dict_z.Add(tile.X, dict_x = new Dictionary<int,Tile>());

				dict_x[tile.Y] = tile;
			}
		}
	}
}

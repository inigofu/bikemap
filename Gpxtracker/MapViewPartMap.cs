//------------------------------------------------------------------------------
// <copyright file="MapViewPartMap.cs" company="">
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
// <date>09.11.2010</date>
// <summary>Contains information about the MapViewPartMap class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Drawing;
	using System.Globalization;

	class MapViewPartMap : MapViewPart
	{
		private const int TileWidth = 256;
		private const int TileHeight = 256;

		private ImageProvider _imageProvider = new ImageProvider();

		private MapView _view;

		private Font _font = new Font(FontFamily.GenericSansSerif, 12.0f, FontStyle.Bold);


		public MapViewPartMap(MapView view)
		{
			_view = view;

			_imageProvider.RegisterRenderSingleTileCB(RenderSingleTile);
		}

		public bool RenderThinGrid { get; set; }
		public bool RenderThickGrid { get; set; }
		public bool RenderTileNumbers { get; set; }

		private void RenderSingleTile(int x, int y, int zoom, Bitmap bmp)
		{
			_view.RenderSingleTile(x, y, zoom, bmp);
		}

		public void Render(Graphics graphics, MyPoint origin, int zoom)
		{
			Rectangle rc = _view.ClientRectangle;

			// lon --> x
			// lat --> y

			int px, py;
			int tx, ty;

			int max_tile_num = (1 << zoom);

			py = -origin.Y % TileHeight;
			ty = origin.Y / TileHeight;

			while (py < rc.Height)
			{
				if (ty >= 0 && ty < max_tile_num)
				{
					px = -origin.X % TileWidth;
					tx = origin.X / TileWidth;

					while (px < rc.Width)
					{
						if (tx >= 0 && tx < max_tile_num)
						{
							Bitmap bitmap = _imageProvider.GetTileBitmap(tx, ty, zoom);

							if (bitmap != null)
							{
								DrawBitmap(graphics, origin, bitmap, tx, ty);
							}
						}

						px += TileWidth;
						tx++;
					}
				}

				py += TileHeight;
				ty++;
			}
		}

		public void DrawBitmap(Graphics graphics, MyPoint origin, Bitmap bmp, int tx, int ty)
		{
			int px = tx * TileWidth - origin.X;
			int py = ty * TileHeight - origin.Y;

			// DrawImageUnscaled() seems to be faster than DrawImage()
			graphics.DrawImageUnscaled(bmp, (int)px, (int)py);

			// Draw tile numbers if desired
			if (RenderTileNumbers)
			{
				string str = String.Format(CultureInfo.InvariantCulture, "{0} / {1}", tx, ty);
				SizeF sz = graphics.MeasureString(str, _font);
				int dx = (TileWidth - (int)sz.Width) / 2;
				int dy = (TileHeight - (int)sz.Height) / 2;
				graphics.DrawString(str, _font, Brushes.Black, px + dx, py + dy);
			}

			// Draw thick grid if desired
			if (RenderThickGrid)
			{
				graphics.DrawRectangle(Pens.Black, new Rectangle(px + 1, py + 1, 254, 254));
			}

			// Draw thin grid if desired
			if (RenderThinGrid)
			{
				graphics.DrawLine(Pens.Black, px, py, px + TileWidth, py);
				graphics.DrawLine(Pens.Black, px, py, px, py + TileHeight);
			}
		}


		public void SetMapSource(MapDataSource source)
		{
			_imageProvider.SetMapSource(source);
		}
	}
}

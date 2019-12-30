//------------------------------------------------------------------------------
// <copyright file="MapViewPartScale.cs" company="">
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
// <summary>Contains information about the MapViewPartScale class.</summary>
//------------------------------------------------------------------------------

using System.Drawing;
namespace GpxTracker
{
	class MapViewPartScale : MapViewPart
	{
		private MapView _view;
		private bool _visible;

		private Font _font = new Font(FontFamily.GenericSansSerif, 12.0f, FontStyle.Bold);

		/// <summary>
		/// Initializes a new instance of the <see cref="MapViewPartScale"/> class.
		/// </summary>
		/// <param name="view">The view.</param>
		public MapViewPartScale(MapView view)
		{
			_view = view;
		}

		public void SetVisible(bool yesno)
		{
			_visible = yesno;
			_view.Invalidate();
		}

		public void Render(Graphics graphics, MyPoint org, int zoom)
		{
			if (!_visible)
				return;

			const int height = 8;
			const int posX = 20;
			const int hSpace = 10;

			int posY = 30;
			//int posY = ClientSize.Height - 30;

			int width = 150;

			Pen pen = new Pen(Color.Black, 2.0f);

			int textHeight = (int)_font.GetHeight();

			graphics.DrawLine(pen, posX, posY, posX + width, posY);
			graphics.DrawLine(pen, posX, posY - height, posX, posY + height);
			graphics.DrawLine(pen, posX + width, posY - height, posX + width, posY + height);

			MyPointF world0 = ScreenToWorld(org, zoom);
			MyPointF world1 = ScreenToWorld(org + new MyPoint(width, 0), zoom);

			double dist = TrackDataManager.ComputeDistance(world0.Y, world0.X, world1.Y, world1.X);
			graphics.DrawString(Units.Provider.DistanceString(dist), _font, Brushes.Black, posX + width + hSpace, posY - textHeight / 2);
		}
	}
}

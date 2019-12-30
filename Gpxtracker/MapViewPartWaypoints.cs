//------------------------------------------------------------------------------
// <copyright file="MapViewPartWaypoints.cs" company="">
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
// <summary>Contains information about the MapViewPartWaypoints class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System.Collections.Generic;
	using System.Drawing;

	/// <summary>
	/// Represents a waypoint renderer.
	/// </summary>
	class MapViewPartWaypoints : MapViewPart
	{
		private class Waypoint
		{
			public Waypoint(MyPointF worldPos, string text)
			{
				WorldPos = worldPos;
				Text = text;
			}

			public MyPointF WorldPos { get; private set; }
			public string Text { get; private set; }
		}

		private List<Waypoint> _waypointList = new List<Waypoint>();

		private Bitmap _bitmapFlag = Properties.Resources.flag_icon_blue;
		private MyPoint _flagOffset = new MyPoint(3, 25);
		private Font _font = new Font(FontFamily.GenericSansSerif, 9.0f, FontStyle.Bold);

		/// <summary>
		/// Clears all waypoints.
		/// </summary>
		public void Clear()
		{
			_waypointList.Clear();
		}

		/// <summary>
		/// Adds a new waypoint.
		/// </summary>
		/// <param name="point">The position of the waypoint.</param>
		public void AddWaypoint(MyPointF point, string text)
		{
			_waypointList.Add(new Waypoint(point, text));
		}

		public void Render(Graphics graphics, MyPoint origin, int zoom)
		{
			if (_bitmapFlag == null)
				return;

			MyPoint org = origin;

			foreach (Waypoint wayPt in _waypointList)
			{
				PointF pos = WorldToScreen(wayPt.WorldPos, zoom) - org;

				graphics.DrawLine(Pens.Black, pos.X - 5, pos.Y - 3, pos.X + 5, pos.Y + 3);
				graphics.DrawLine(Pens.Black, pos.X - 5, pos.Y + 3, pos.X + 5, pos.Y - 3);

				// TODO: find out why the bitmap is scaled if width/height are not specified
				graphics.DrawImage(_bitmapFlag, pos.X - _flagOffset.X, pos.Y - _flagOffset.Y, _bitmapFlag.Width, _bitmapFlag.Height);

				// Draw text
				SizeF offset = graphics.MeasureString(wayPt.Text, _font);
				pos.X -= offset.Width * 0.5f;
				pos.Y += 3.0f;
				graphics.DrawString(wayPt.Text, _font, Brushes.Black, pos);
			}
		}
	}
}

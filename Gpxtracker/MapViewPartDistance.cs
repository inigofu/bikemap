//------------------------------------------------------------------------------
// <copyright file="MapViewPartDistance.cs" company="">
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
// <date>07.11.2010</date>
// <summary>Contains information about the MapViewPartDistance class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Drawing.Text;
	using System.Windows.Forms;

	/// <summary>
	/// Represents a distance measurement tool.
	/// </summary>
	class MapViewPartDistance : MapViewPart
	{
		private class DistancePair
		{
			public MyPointF WorldPos0 { get; set; }
			public MyPointF WorldPos1 { get; set; }
			public double Distance { get; set; }
			public bool DrawCircle { get; set; }
		}

		private Stack<DistancePair> _list = new Stack<DistancePair>();

		private Font _font = new Font(FontFamily.GenericSansSerif, 12.0f);

		private DistancePair _openPair;

		private MapView _view;

		/// <summary>
		/// Initializes a new instance of the <see cref="MapViewPartDistance"/> class.
		/// </summary>
		/// <param name="view">The MapView.</param>
		public MapViewPartDistance(MapView view)
		{
			_view = view;
		}

		/// <summary>
		/// Clears all data.
		/// </summary>
		public void Clear()
		{
			_list.Clear();
			_view.Invalidate();
		}

		public void OnMouseMove(MyPoint origin, int zoom, MouseEventArgs e)
		{
			if (_openPair != null)
			{
				MyPoint cursor = new MyPoint(origin.X + e.X, origin.Y + e.Y);

				_openPair.WorldPos1 = ScreenToWorld(cursor, zoom);
				_openPair.Distance = TrackDataManager.ComputeDistance(_openPair.WorldPos0.Y, _openPair.WorldPos0.X, _openPair.WorldPos1.Y, _openPair.WorldPos1.X); ; 
				_view.Invalidate();
			}
		}

		public void OnMouseDown(MyPoint origin, int zoom, MouseEventArgs e)
		{
			MyPoint cursor = new MyPoint(origin.X + e.X, origin.Y + e.Y);

			if (e.Button == MouseButtons.Left)
			{
				if (_openPair == null)
				{
					_openPair = new DistancePair()
					{
						WorldPos0 = ScreenToWorld(cursor, zoom),
						WorldPos1 = ScreenToWorld(cursor, zoom),
						DrawCircle = true,
						Distance = 0
					};

					_list.Push(_openPair);
				}
				else
				{
					_openPair.DrawCircle = false;
					_openPair = null;
					_view.Invalidate();
				}
			}

			if (e.Button == MouseButtons.Right)
			{
				if (_openPair != null)
				{
					_openPair = null;
					_list.Pop();		// remove _openPair from the list
					_view.Invalidate();
				}
			}

		}

		public void Render(Graphics graphics, MyPoint origin, int zoom)
		{
			float len = 5.0f;

			// Restore rendering hint and smoothing mode when done
			TextRenderingHint oldHint = graphics.TextRenderingHint;
			SmoothingMode oldMode = graphics.SmoothingMode;

			graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			
			foreach (DistancePair pair in _list)
			{
				MyPointF p0 = (MyPointF)(WorldToScreen(pair.WorldPos0, zoom) - origin);
				MyPointF p1 = (MyPointF)(WorldToScreen(pair.WorldPos1, zoom) - origin);

				MyPointF dir = p1 - p0;
				MyPointF pp = new MyPointF(-dir.Y, dir.X).Normalize();	// perpendicular direction

				// Draw circle around p0 and p1
				MyPointF center = p0 + dir * 0.5f;
				if (pair.DrawCircle)
				{
					float radius = dir.GetLength() * 0.5f;
					graphics.DrawEllipse(Pens.Gray, center.X - radius, center.Y - radius, radius * 2.0f, radius * 2.0f);
				}

				// Draw line between p0 and p1
				graphics.DrawLine(Pens.Black, p0, p1);

				// Draw border line at p0
				graphics.DrawLine(Pens.Black, p0 + pp * len, p0 - pp * len);

				// Draw border line at p1
				graphics.DrawLine(Pens.Black, p1 + pp * len, p1 - pp * len);

				// Draw text
				string text = Units.Provider.DistanceString(pair.Distance);

				// The y-direction runs from top to bottom
				double angle = Math.Atan2(dir.X, -dir.Y);

				if (angle > Math.PI)
					angle -= Math.PI;

				if (angle < 0)
					angle += Math.PI;

				angle -= Math.PI / 2.0;

				float cos = (float)Math.Cos(angle);
				float sin = (float)Math.Sin(angle);

				SizeF textSize = graphics.MeasureString(text, _font);

				// Setup rotation matrix from angle and position
				graphics.Transform = new Matrix(cos, sin, -sin, cos, center.X, center.Y);
				graphics.DrawString(text, _font, Brushes.Black, -textSize.Width * 0.5f, -textSize.Height);
				graphics.ResetTransform();
			}

			// Restore rendering hint and smoothing mode
			graphics.TextRenderingHint = oldHint;
			graphics.SmoothingMode = oldMode;
		}
	}
}


//------------------------------------------------------------------------------
// <copyright file="MapViewPartTrack.cs" company="">
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
// <summary>Contains information about the MapViewPartTrack class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System.Drawing;
	using System.Windows.Forms;
	using System.Collections.Generic;
	using System;
	using System.Drawing.Drawing2D;

	/// <summary>
	/// Represents a track renderer.
	/// </summary>
	class MapViewPartTrack : MapViewPart
	{
		private class Marker
		{
			public Marker(int point, string text)
			{
				Point = point;
				Text = text;
			}

			public int Point { get; private set; }
			public string Text { get; private set; }
		}
		
		private MapView _view;

		private List<Marker> _markerList = new List<Marker>();

		private MyPoint[] _pointsScreen = new MyPoint[0];
		private MyPointF[] _pointsWorld = new MyPointF[0];

		private int _selectedIndex;
		private int _controlIndex;

		private MyPointF _boundingBoxWorldMin = new MyPointF();
		private MyPointF _boundingBoxWorldMax = new MyPointF();

		/// <summary>
		/// Initializes a new instance of the <see cref="MapViewPartTrack"/> class.
		/// </summary>
		/// <param name="view">The view.</param>
		public MapViewPartTrack(MapView view)
		{
			_view = view;
		}

		public bool RenderAntiAliased { get; set; }

		/// <summary>
		/// Sets the selected and the control indexes.
		/// </summary>
		/// <param name="sel">The selected index.</param>
		/// <param name="ctrl">The control index.</param>
		public void SetIndices(int sel, int ctrl, MyPoint org)
		{
			const int margin = 10;

			Rectangle rcSel = Rectangle.Empty;
			Rectangle rcCtrl = Rectangle.Empty;
			Rectangle rect = Rectangle.Empty;

			if (_selectedIndex != sel)
			{
				rcSel = GetBoundingBox(_selectedIndex, sel);

				rect = rcSel;

				_selectedIndex = sel;
			}

			if (_controlIndex != ctrl)
			{
				rcCtrl = GetBoundingBox(_controlIndex, ctrl);

				if (rcSel != Rectangle.Empty)
				{
					rect = Rectangle.Union(rcSel, rcCtrl);
				}
				else
				{
					rect = rcCtrl;
				}

				_controlIndex = ctrl;
			}


			rect.Offset(-org.X, -org.Y);
			rect.Inflate(margin, margin);

			_view.Invalidate(rect);
		}

		/// <summary>
		/// Clears all markers.
		/// </summary>
		public void ClearMarkers()
		{
			_markerList.Clear();
		}

		/// <summary>
		/// Adds a marker. A marker is just a point along the track. Waypoints can be anywhere.
		/// </summary>
		/// <param name="point">The index of the position of the marker.</param>
		/// <param name="text">The text of the marker.</param>
		public void AddMarker(int point, string text)
		{
			_markerList.Add(new Marker(point, text));
		}

		public void OnMouseDoubleClick(MyPoint origin, int zoom, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (_pointsScreen.Length == 0)
					return;

				PointF cur = new PointF(origin.X + e.X, origin.Y + e.Y);

				int ind = FindIndexClosest(cur);

				if (DistSq(cur, _pointsScreen[ind]) > 225.0f)		// 15^2
					ind = 0;

				if (ind == _controlIndex)
					return;

				_controlIndex = ind;
				_view.Invalidate();

				OnControlIndexChanged();
			}
		}

		public void OnMouseDown(MyPoint origin, int zoom, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (_pointsScreen.Length == 0)
					return;

				PointF cur = new PointF(origin.X + e.X, origin.Y + e.Y);

				int ind = FindIndexClosest(cur);

				if (DistSq(cur, _pointsScreen[ind]) > 225.0f)		// 15^2
					ind = 0;

				if (ind == _selectedIndex)
					return;

				_selectedIndex = ind;
				_view.Invalidate();

				OnSelectedIndexChanged();
			}
		}

		public void Render(Graphics graphics, MyPoint origin, int zoom)
		{
			if (_pointsScreen.Length == 0)
				return;

			SmoothingMode oldMode = graphics.SmoothingMode;

			if (RenderAntiAliased)
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
			}
			else
			{
				graphics.SmoothingMode = SmoothingMode.Default;
			}

			Pen normal_pen = new Pen(Color.DarkGreen, 2.0f);
			Pen select_pen = new Pen(Color.MediumSlateBlue, 2.0f);
			Pen border_pen = new Pen(Color.Black, 2.0f);

			int first = Math.Min(_selectedIndex, _controlIndex);
			int last = Math.Max(_selectedIndex, _controlIndex);

			DrawLineSub(graphics, origin, normal_pen, 0, first + 1);
			DrawLineSub(graphics, origin, select_pen, first, last + 1);
			DrawLineSub(graphics, origin, normal_pen, last, _pointsScreen.Length);

			DrawMarkers(graphics, origin);

			DrawBorder(graphics, origin, border_pen, 0, 8.0f);
			DrawBorder(graphics, origin, border_pen, _pointsScreen.Length - 1, 8.0f);

			DrawBorder(graphics, origin, border_pen, first, 8.0f);
			DrawBorder(graphics, origin, border_pen, last, 8.0f);

			normal_pen.Dispose();
			select_pen.Dispose();
			border_pen.Dispose();

			graphics.SmoothingMode = oldMode;
		}

		private void DrawMarkers(Graphics graphics, MyPoint origin)
		{
			using (Pen markerPen = new Pen(Color.Purple, 2.0f))
			{
				foreach (Marker marker in _markerList)
				{
					DrawBorder(graphics, origin, markerPen, marker.Point, 8.0f);
				}
			}
		}

		private void DrawLineSub(Graphics g, MyPoint org, Pen pen, int start, int end)
		{
			if (end - start < 2)
				return;

			PointF[] line_pts = new PointF[(end - start - 1) * 2];

			int ind = 0;

			for (int i = start; i < end - 1; i++)
			{
				line_pts[ind].X = (_pointsScreen[i].X - org.X);
				line_pts[ind].Y = (_pointsScreen[i].Y - org.Y);
				ind++;

				line_pts[ind].X = (_pointsScreen[i + 1].X - org.X);
				line_pts[ind].Y = (_pointsScreen[i + 1].Y - org.Y);
				ind++;
			}

			g.DrawLines(pen, line_pts);
		}

		private Rectangle GetBoundingBox(int index0, int index1)
		{
			int min = Math.Min(index0, index1);
			int max = Math.Max(index0, index1);

			int lowX = Int32.MaxValue;
			int lowY = Int32.MaxValue;
			int highX = Int32.MinValue;
			int highY = Int32.MinValue;

			for (int i = min; i <= max; i++)	// include higher index
			{
				MyPoint pt = _pointsScreen[i];

				if (pt.X < lowX)
					lowX = pt.X;

				if (pt.Y < lowY)
					lowY = pt.Y;

				if (pt.X > highX)
					highX = pt.X;

				if (pt.Y > highY)
					highY = pt.Y;
			}

			return Rectangle.FromLTRB(lowX, lowY, highX, highY);
		}

		private MyPointF GetDirection(int index)
		{
			int range = 1;
			MyPoint dir = MyPoint.Zero;
			float distSq = 0;

			while (distSq < 16 && range < 20)
			{
				// compute direction from [-range..range] around index
				int lowBound = Math.Max(index - range, 0);
				int highBound = Math.Min(index + range, _pointsScreen.Length - 1);

				range++;

				dir = _pointsScreen[highBound] - _pointsScreen[lowBound];
				distSq = dir.GetLengthSquare();

				if (lowBound == 0 && highBound == _pointsScreen.Length - 1)
					break;		// this is as good as it gets
			}

			if (distSq == 0.0f)
				return MyPointF.Zero;

			float dist = (float)Math.Sqrt(distSq);
			return new MyPointF(dir.X / dist, dir.Y / dist);
		}

		private void DrawBorder(Graphics graphics, MyPoint org, Pen pen, int ind, float len)
		{
			PointF dir = GetDirection(ind);

			PointF n = new PointF(dir.Y, -dir.X);
			PointF p = new PointF();

			p.X = _pointsScreen[ind].X - org.X;
			p.Y = _pointsScreen[ind].Y - org.Y;

			graphics.DrawLine(pen, p.X - n.X * len, p.Y - n.Y * len, p.X + n.X * len, p.Y + n.Y * len);
		}

		/// <summary>
		/// Sets the points that are used for the track.
		/// </summary>
		/// <param name="pts">The points that are used for the track.</param>
		/// <param name="zoom">The zoom factor.</param>
		public void SetPoints(PointF[] pts, int zoom)
		{
			_selectedIndex = 0;
			_controlIndex = 0;

			_pointsWorld = new MyPointF[pts.Length];

			for (int i = 0; i < pts.Length; i++)
			{
				_pointsWorld[i] = pts[i];
			}

			UpdatePointSet(zoom);

			CalcBoundingBox(pts);

			OnSelectedIndexChanged();
			OnControlIndexChanged();

			_view.Invalidate();
		}

		public void UpdatePointSet(int zoom)
		{
			_pointsScreen = new MyPoint[_pointsWorld.Length];

			for (int i = 0; i < _pointsWorld.Length; i++)
			{
				_pointsScreen[i] = WorldToScreen(_pointsWorld[i], zoom);
			}
		}

		private void CalcBoundingBox(PointF[] pts)
		{
			if (_pointsScreen.Length == 0)
				return;

			float minX = pts[0].X;
			float maxX = pts[0].X;

			float minY = pts[0].Y;
			float maxY = pts[0].Y;

			foreach (PointF pt in pts)
			{
				if (pt.X < minX)
					minX = pt.X;

				if (pt.Y < minY)
					minY = pt.Y;

				if (pt.X > maxX)
					maxX = pt.X;

				if (pt.Y > maxY)
					maxY = pt.Y;
			}

			_boundingBoxWorldMin = new MyPointF(minX, minY);
			_boundingBoxWorldMax = new MyPointF(maxX, maxY);
		}

		public double FindMaxZoomX()
		{
			double screenExt = _view.ClientSize.Width;
			double worldExt = _boundingBoxWorldMax.X - _boundingBoxWorldMin.X;

			double zoom = Math.Log(screenExt / 256.0 * 360.0 / worldExt, 2);

			return zoom;
		}

		public double FindMaxZoomY()
		{
			double screenExtY = _view.ClientSize.Height;

			double worldMax = _boundingBoxWorldMax.Y;
			double worldMin = _boundingBoxWorldMin.Y;

			double innerA = 128.0 * (1.0 - Math.Log(Math.Tan(worldMax * Math.PI / 180.0) + 1.0 / Math.Cos(worldMax * Math.PI / 180.0)) / Math.PI);
			double innerB = 128.0 * (1.0 - Math.Log(Math.Tan(worldMin * Math.PI / 180.0) + 1.0 / Math.Cos(worldMin * Math.PI / 180.0)) / Math.PI);

			double zoom = Math.Log(screenExtY / (innerB - innerA), 2.0);

			return zoom;
		}

		private static float DistSq(PointF p1, PointF p2)
		{
			float dx = (p1.X - p2.X);
			float dy = (p1.Y - p2.Y);
			return dx * dx + dy * dy;
		}

		private int FindIndexClosest(PointF pt)
		{
			return FindIndexClosestBruteforce(pt, 0, _pointsScreen.Length - 1);
		}

		private int FindIndexClosestBruteforce(PointF pt, int indLeft, int indRight)
		{
			int ib = 0;										// best value

			if (indRight >= _pointsScreen.Length)
				return indLeft;

			if (indLeft >= indRight)
				return indRight;

			float dist_sq;
			float min_dist_sq = DistSq(_pointsScreen[ib], pt);

			for (int i = indLeft; i <= indRight; i++)
			{
				dist_sq = DistSq(_pointsScreen[i], pt);

				if (dist_sq < min_dist_sq)
				{
					ib = i;
					min_dist_sq = dist_sq;
				}
			}

			return ib;
		}

		/// <summary>
		/// Computes the center of the tracks bounding box.
		/// </summary>
		/// <returns>The center of the tracks bounding box</returns>
		public MyPointF GetCenter()
		{
			float x = (_boundingBoxWorldMin.X + _boundingBoxWorldMax.X) * 0.5f;
			float y = (_boundingBoxWorldMin.Y + _boundingBoxWorldMax.Y) * 0.5f;
			return new MyPointF(x, y);
		}

		public event EventHandler<IndexChangedEventArgs> SelectedIndexChanged;
		public event EventHandler<IndexChangedEventArgs> ControlIndexChanged;

		private void OnSelectedIndexChanged()
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, new IndexChangedEventArgs(_selectedIndex, _controlIndex));
			}
		}

		private void OnControlIndexChanged()
		{
			if (ControlIndexChanged != null)
			{
				ControlIndexChanged(this, new IndexChangedEventArgs(_selectedIndex, _controlIndex));
			}
		}


	}
}

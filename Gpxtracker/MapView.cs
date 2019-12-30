//------------------------------------------------------------------------------
// <copyright file="MapView.cs" company="">
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
// <summary>Contains information about the MapView class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Globalization;
	using System.Windows.Forms;

	/// <summary>
	/// Represents a view for map image data.
	/// </summary>
	partial class MapView : PictureBox
	{
		/// <summary>
		/// Flags for the rendering of the control.
		/// </summary>
		[Flags]
		public enum RenderFlags
		{
			/// <summary>
			/// Display no extras.
			/// </summary>
			None = 0x00,

			/// <summary>
			/// Display a thin grid.
			/// </summary>
			ThinGrid = 0x01,

			/// <summary>
			/// Display a thick grid.
			/// </summary>
			ThickGrid = 0x02,

			/// <summary>
			/// Display tile numbers.
			/// </summary>
			Numbers = 0x04,

			/// <summary>
			/// Enable anti-aliased rendering.
			/// </summary>
			AntiAlias = 0x08
		}

		/// <summary>
		/// Specifies the mode MapView operates in.
		/// </summary>
		[Flags]
		public enum Mode
		{
			/// <summary>
			/// Default mode
			/// </summary>
			Default = 0x00,

			/// <summary>
			/// Distance measure mode 
			/// </summary>
			Measure = 0x01
		}

		private MapDataSource _mapDataSource;

		private RenderFlags _renderFlags;
		private Mode _mode;

		private MyPoint _dragStart = new MyPoint();
		private MyPoint _dragOff = new MyPoint();

		private MapViewPartWaypoints _partWaypoints;
		private MapViewPartDistance _partDistance;
		private MapViewPartTrack _partTrack;
		private MapViewPartScale _partScale;
		private MapViewPartMap _partMap;

		private int _zoom;
		private int _minZoom;
		private int _maxZoom;

		private MyPoint _screenPos;

		/// <summary>
		/// Initializes a new instance of the <see cref="MapView"/> class.
		/// </summary>
		public MapView()
		{
			// AllPaintingInWmPaint should only be applied if the UserPaint bit is set to true.
			SetStyle(ControlStyles.UserPaint, true);	// true by default

			// The control ignores the window message WM_ERASEBKGND to reduce flicker. 
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			// The control is drawn opaque and the background (=parent) is not painted.
			SetStyle(ControlStyles.Opaque, true);

			_partWaypoints = new MapViewPartWaypoints();
			_partDistance = new MapViewPartDistance(this);
			_partTrack = new MapViewPartTrack(this);
			_partScale = new MapViewPartScale(this);
			_partMap = new MapViewPartMap(this);

			_partTrack.SelectedIndexChanged += new EventHandler<IndexChangedEventArgs>(OnSelectedIndexChanged);
			_partTrack.ControlIndexChanged += new EventHandler<IndexChangedEventArgs>(OnControlIndexChanged);

			// Don't use _renderFlags here, because parts need the update
			MyRenderFlags = RenderFlags.ThickGrid | RenderFlags.Numbers;

			InitializeComponent();
		}

		public void RenderSingleTile(int x, int y, int zoom, Bitmap bmp)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<int, int, int, Bitmap>(RenderSingleTile), x, y, zoom, bmp);
			}
			else
			{
				MyPoint origin = _screenPos + _dragOff;

				using (Graphics graphics = CreateGraphics())
				{
					_partMap.DrawBitmap(graphics, origin, bmp, x, y);

					_partTrack.Render(graphics, origin, _zoom);
					_partWaypoints.Render(graphics, origin, _zoom);
					_partDistance.Render(graphics, origin, _zoom);
					_partScale.Render(graphics, origin, _zoom);
				}
			}
		}

		// Routes events from _partTrack further on
		private void OnSelectedIndexChanged(object sender, IndexChangedEventArgs e)
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, new IndexChangedEventArgs(e.SelectedIndex, e.ControlIndex));
			}
		}

		// Routes events from _partTrack further on
		private void OnControlIndexChanged(object sender, IndexChangedEventArgs e)
		{
			if (ControlIndexChanged != null)
			{
				ControlIndexChanged(this, new IndexChangedEventArgs(e.SelectedIndex, e.ControlIndex));
			}
		}

		/// <summary>
		/// Sets the map data source.
		/// </summary>
		/// <param name="source">The source.</param>
		public void SetMapDataSource(MapDataSource source)
		{
			_mapDataSource = source;

			_minZoom = source.MinZoom;
			_maxZoom = source.MaxZoom;

			_partMap.SetMapSource(source);

			ResetView();
		}

		/// <summary>
		/// Renders everything to a bitmap.
		/// </summary>
		/// <returns>The bitmap that contains the full scene.</returns>
		public Bitmap RenderToBitmap()
		{
			Bitmap bmp = new Bitmap(ClientSize.Width, ClientSize.Height);
			MyPoint origin = _screenPos + _dragOff;
			
			using (Graphics graphics = Graphics.FromImage(bmp))
			{
				_partMap.Render(graphics, origin, _zoom);
				_partTrack.Render(graphics, origin, _zoom);
				_partWaypoints.Render(graphics, origin, _zoom);
				_partDistance.Render(graphics, origin, _zoom);
				_partScale.Render(graphics, origin, _zoom);
			}

			return bmp;
		}


		/// <summary>
		/// Shows the yardstick.
		/// </summary>
		/// <param name="yesno">If set to <c>true</c> the yardstick is rendered.</param>
		public void ShowYardstick(bool yesno)
		{
			_partScale.SetVisible(yesno);
		}

		/// <summary>
		/// Resets the view. Adjusts the zoom and centers the view.
		/// </summary>
		public void ResetView()
		{
			AdjustZoom();

			CenterView(_partTrack.GetCenter());
		}

		/// <summary>
		/// Zooms out one level.
		/// </summary>
		public void ZoomOut()
		{
			if (_zoom <= _minZoom)
				return;

			MyPoint center = GetClientRectCenter();
			MyPointF world = ScreenToWorld(_screenPos + center);

			_zoom--;

			_screenPos = WorldToScreen(world) - center;

			_partTrack.UpdatePointSet(_zoom);

			Invalidate();
		}

		/// <summary>
		/// Zooms in one level.
		/// </summary>
		public void ZoomIn()
		{
			if (_zoom >= _maxZoom)
				return;

			MyPoint center = GetClientRectCenter();
			MyPointF world = ScreenToWorld(_screenPos + center);

			_zoom++;

			_screenPos = WorldToScreen(world) - center;

			_partTrack.UpdatePointSet(_zoom);

			Invalidate();
		}

		/// <summary>
		/// Gets the zoom factor.
		/// </summary>
		/// <returns>The zoom factor.</returns>
		public int GetZoom()
		{
			return _zoom;
		}

		/// <summary>
		/// Clears all markers.
		/// </summary>
		public void ClearMarkers()
		{
			_partTrack.ClearMarkers();
		}

		/// <summary>
		/// Adds a marker. A marker is just a point along the track. Waypoints can be anywhere.
		/// </summary>
		/// <param name="point">The index of the position of the marker.</param>
		/// <param name="text">The text of the marker.</param>
		public void AddMarker(int point, string text)
		{
			_partTrack.AddMarker(point, text);
		}

		/// <summary>
		/// Clears all waypoints.
		/// </summary>
		public void ClearWaypoints()
		{
			_partWaypoints.Clear();
		}

		/// <summary>
		/// Adds a new waypoint.
		/// </summary>
		/// <param name="point">The position of the waypoint.</param>
		/// <param name="text">The text description.</param>
		public void AddWaypoint(PointF point, string text)
		{
			_partWaypoints.AddWaypoint(point, text);
		}

		
		/// <summary>
		/// Sets the selected and the control indexes.
		/// </summary>
		/// <param name="sel">The selected index.</param>
		/// <param name="ctrl">The control index.</param>
		public void SetIndices(int sel, int ctrl)
		{
			MyPoint origin = _screenPos + _dragOff;

			_partTrack.SetIndices(sel, ctrl, origin);
		}
				
		/// <summary>
		/// Sets the points that are used for the track.
		/// </summary>
		/// <param name="pts">The points that are used for the track.</param>
		public void SetPoints(PointF[] pts)
		{
			_partTrack.SetPoints(pts, _zoom);
		}


		/// <summary>
		/// Clears the distance pair markers.
		/// </summary>
		public void ClearDistances()
		{
			_partDistance.Clear();
		}

		/// <summary>
		/// Sets the distance measure mode.
		/// </summary>
		/// <param name="onoff">if set to <c>true</c> [onoff].</param>
		public void SetDistanceMeasureMode(bool onoff)
		{
			_mode = onoff ? Mode.Measure : Mode.Default;
		}
		
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			MyPoint origin = _screenPos + _dragOff;

			switch (_mode)
			{
				case Mode.Default:
					_partTrack.OnMouseDoubleClick(origin, _zoom, e);
					break;
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			MyPoint origin = _screenPos + _dragOff;

			switch (_mode)
			{
				case Mode.Default:
					_partTrack.OnMouseDown(origin, _zoom, e);
					break;

				case Mode.Measure:
					_partDistance.OnMouseDown(origin, _zoom, e);
					break;
			}

			if (e.Button == MouseButtons.Right)
			{
				Cursor = Cursors.Hand;

				_dragStart = new MyPoint(e.X, e.Y);
				_dragOff = MyPoint.Zero;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			MyPoint origin = _screenPos + _dragOff;

			switch (_mode)
			{
				case Mode.Measure:
					_partDistance.OnMouseMove(origin, _zoom, e);
					break;
			}

			if (e.Button == MouseButtons.Right)
			{
				MyPoint pos = new MyPoint(e.X, e.Y);
				_dragOff = _dragStart - pos;

				Invalidate();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (e.Button == MouseButtons.Right)
			{
				Cursor = Cursors.Default;
				
				_screenPos += _dragOff;
				_dragOff = MyPoint.Zero;
			}
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);

			if (e.Delta < 0)
				_zoom--;
			else
				_zoom++;

			Invalidate();
		}

		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			//base.OnPaintBackground(pevent);

			// Do nothing
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			// base.OnPaint(pe);

			if (DesignMode)
			{
				pe.Graphics.FillRectangle(Brushes.Honeydew, pe.ClipRectangle);
				return;
			}

			MyPoint origin = _screenPos + _dragOff;

			_partMap.Render(pe.Graphics, origin, _zoom);
			_partTrack.Render(pe.Graphics, origin, _zoom);
			_partWaypoints.Render(pe.Graphics, origin, _zoom);
			_partDistance.Render(pe.Graphics, origin, _zoom);
			_partScale.Render(pe.Graphics, origin, _zoom);
		}

		private MyPoint WorldToScreen(MyPointF pt)
		{
			int x = (int)(256.0 * ((pt.X + 180.0) / 360.0 * (1 << _zoom)));
			int y = (int)(256.0 * ((1.0 - Math.Log(Math.Tan(pt.Y * Math.PI / 180.0) + 
				1.0 / Math.Cos(pt.Y * Math.PI / 180.0)) / Math.PI) / 2.0 * (1 << _zoom)));
		
			return new MyPoint(x, y);
		}

		private MyPointF ScreenToWorld(MyPoint pt) 
		{
		    double n = Math.PI - ((2.0 * Math.PI * pt.Y / 256.0f) / Math.Pow(2.0, _zoom));

			float x = (float)(((pt.X / 256.0f) / Math.Pow(2.0, _zoom) * 360.0) - 180.0);
			float y = (float)(180.0 / Math.PI * Math.Atan(0.5 * (Math.Exp(n) - Math.Exp(-n))));

			return new MyPointF(x, y);
		}

		private void AdjustZoom()
		{
			double zoomX = _partTrack.FindMaxZoomX();
			double zoomY = _partTrack.FindMaxZoomY();

			int zoom = (int)Math.Floor(Math.Min(zoomX, zoomY));

			if (zoom < _minZoom)
				zoom = _minZoom;

			if (zoom > _maxZoom)
				zoom = _maxZoom;

			if (zoom != _zoom)
			{
				_zoom = zoom;
				_partTrack.UpdatePointSet(_zoom);
				OnZoomChanged();
			}
		}


		private void CenterView(MyPointF world)
		{
			MyPoint center = GetClientRectCenter();
			
			_screenPos = WorldToScreen(world) - center;
			
			Invalidate();
		}

		private MyPoint GetClientRectCenter()
		{
			int x = ClientRectangle.Width / 2;
			int y = ClientRectangle.Height / 2;

			return new MyPoint(x, y);
		}
	}
}

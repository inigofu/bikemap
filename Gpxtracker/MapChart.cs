//------------------------------------------------------------------------------
// <copyright file="MapChart.cs" company="">
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
// <summary>Contains information about the MapChart class.</summary>
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
	/// Represents a chart control that works on track data.
	/// </summary>
	public partial class MapChart : UserControl
	{
		private const float _arrowSize = 3.0f;

		private List<Marker> _markerList = new List<Marker>();

		private PointF[] _points;
		private PointF _pointMin = new Point();
		private PointF _pointMax = new Point();

		private PointF _roundMin;
		private PointF _roundMax;

		private bool _enableAntiAliasing = true;
		private bool _invalid = true;

		private RectangleF _chartRect = new RectangleF();
		private int _selectedIndex;
		private int _controlIndex;

		private int _oldSelectedIndex;
		private int _oldControlIndex;

		private Pen _penArrow;
		private Font _fontLegend;

		private Bitmap _bufferNormal;
		private Bitmap _bufferSelected;
		private Bitmap _bufferFinal;

		/// <summary>
		/// Initializes a new instance of the <see cref="MapChart"/> class.
		/// </summary>
		public MapChart()
		{
			// AllPaintingInWmPaint should only be applied if the UserPaint bit is set to true.
			SetStyle(ControlStyles.UserPaint, true);	// true by default

			// The control ignores the window message WM_ERASEBKGND to reduce flicker. 
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			// The control is drawn opaque and the background (=parent) is not painted.
			SetStyle(ControlStyles.Opaque, true);

			BackgroundColorTop = Color.White;
			BackgroundColorBottom = Color.LightGray;

			LineColor = Color.Blue;
			ChartFillBrushTop = Color.Aquamarine;
			ChartFillBrushBottom = Color.MediumSlateBlue;

            _penArrow = new Pen(Color.Black);
            _penArrow.EndCap = LineCap.Custom;
            _penArrow.CustomEndCap = new AdjustableArrowCap(_arrowSize, _arrowSize, true);

			_fontLegend = new Font(FontFamily.GenericSansSerif, 7.0f, FontStyle.Regular);

			InitializeComponent();
		}

		/// <summary>
		/// Sets the points that will be displayed in the chart.
		/// </summary>
		/// <param name="pts">The points that will be displayed in the chart.</param>
		public void SetPoints(PointF[] pts)
		{
			if ((pts == null) || (pts.Length < 2))
			{
				_points = null;
				Invalidate();
				return;
			}

			_points = pts;

			_pointMin = _pointMax = _points[0];
			
			foreach (PointF pt in _points)
			{
				if (pt.X < _pointMin.X)
					_pointMin.X = pt.X;

				if (pt.X > _pointMax.X)
					_pointMax.X = pt.X;

				if (pt.Y < _pointMin.Y)
					_pointMin.Y = pt.Y;

				if (pt.Y > _pointMax.Y)
					_pointMax.Y = pt.Y;
			}

			if (_pointMax.X == _pointMin.X || _pointMax.Y == _pointMin.Y)
			{
				_points = null;
				Invalidate();
				return;
			}

			float minY, maxY;

			RoundIntelligent(_pointMin.Y, _pointMax.Y, out minY, out maxY);

			_roundMax.X = _pointMax.X;
			_roundMax.Y = maxY;
			_roundMin.X = _pointMin.X;
			_roundMin.Y = minY;

			_invalid = true;

			Invalidate();
		}

		/// <summary>
		/// Sets the control and selected indexes.
		/// </summary>
		/// <param name="sel">The selected index.</param>
		/// <param name="ctrl">The control index.</param>
		public void SetIndexes(int sel, int ctrl)
		{
			_oldSelectedIndex = _selectedIndex;
			_oldControlIndex = _controlIndex;

			_selectedIndex = sel;
			_controlIndex = ctrl;

			UpdateFinalBuffer();
			RenderFinalBuffer();
		}

		/// <summary>
		/// Clears all markers.
		/// </summary>
		public void ClearMarkers()
		{
			_markerList.Clear();
		}

		/// <summary>
		/// Adds a marker.
		/// </summary>
		/// <param name="value">The value (position along the x-axis).</param>
		/// <param name="text">The text of the marker.</param>
		public void AddMarker(float value, string text)
		{
			_markerList.Add(new Marker(value, text));
		}

		/// <summary>
		/// Raised by the MouseDown event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (_points == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				float sx = ScreenXToValueX(e.X);
				int ind = FindIndexOfValueX(sx);

				if (ind != _selectedIndex || ind != _controlIndex)
				{
					_oldSelectedIndex = _selectedIndex;
					_oldControlIndex = _controlIndex;
					
					_controlIndex = ind;
					_selectedIndex = ind;

					OnSelIndexChanged();
					OnControlIndexChanged();
					
					UpdateFinalBuffer();
					RenderFinalBuffer();
				}
			}

			if (e.Button == MouseButtons.Right)
			{
				if (_controlIndex != 0)
				{
					_oldControlIndex = _controlIndex;
					_controlIndex = 0;

					OnControlIndexChanged();
					
					UpdateFinalBuffer();
					RenderFinalBuffer();
				}
			}

			OnMouseMove(e);
		}

		/// <summary>
		/// Raised by the MouseMove event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (_points == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				float sx = ScreenXToValueX(e.X);
				int ind = FindIndexOfValueX(sx);

				if (ind != _selectedIndex)
				{
					_oldSelectedIndex = _selectedIndex;
					_selectedIndex = ind;

					OnSelIndexChanged();
					
					UpdateFinalBuffer();
					RenderFinalBuffer();
				}
			}
		}

		/// <summary>
		/// Raised by the <see cref="E:System.Windows.Forms.Control.SizeChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			float width = (float)ClientRectangle.Width;
			float height = (float)ClientRectangle.Height;

			float labelWidth = 35.0f;
			float labelHeight = 30.0f;

			float px1 = Padding.Left + labelWidth;
			float py1 = Padding.Top + labelHeight;
			float px2 = Padding.Right + labelWidth;
			float py2 = Padding.Bottom + labelHeight;

			_chartRect.X = px1;
			_chartRect.Y = py1;
			_chartRect.Width = width - px1 - px2;
			_chartRect.Height = height - py1 - py2;

			_invalid = true;

			Invalidate();
		}

		/// <summary>
		/// Paints the background of the control.
		/// </summary>
		/// <remarks>Is not called because <see cref="ControlStyles.AllPaintingInWmPaint"/> is set.</remarks>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			// Do NOT call base method
			// base.OnPaintBackground(pevent);
		}

		/// <summary>
		/// Paints the scene.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// Do NOT call base method
			// base.OnPaint(e);

			if (_invalid == true)
			{
				_bufferNormal = AdjustDoubleBufferSize(_bufferNormal);
				_bufferSelected = AdjustDoubleBufferSize(_bufferSelected);
				_bufferFinal = AdjustDoubleBufferSize(_bufferFinal);

				using (Graphics graphics = Graphics.FromImage(_bufferNormal))
				{
					Render(graphics, false);
				}

				using (Graphics graphics = Graphics.FromImage(_bufferSelected))
				{
					Render(graphics, true);
				}
			}

			UpdateFinalBuffer();

			e.Graphics.DrawImage(_bufferFinal, 0, 0);
		}

		private static void RoundIntelligent(float val_min, float val_max, out float rnd_min, out float rnd_max)
		{
			float val = Math.Max(val_min, val_max);			// just in case
			
			double rnd_exp;

			rnd_exp = Math.Floor(Math.Log10(val));			// transform to format #.##### * 10 ^ (rnd_exp)

			rnd_min = (float)(Math.Floor(val_min / Math.Pow(10.0, rnd_exp)) * Math.Pow(10, rnd_exp));
			rnd_max = (float)(Math.Ceiling(val_max / Math.Pow(10.0, rnd_exp)) * Math.Pow(10, rnd_exp));
		}

		private static float FindMultiplier(float size, float range, float desiredSpacing)
		{
            float count = size / desiredSpacing;

			float ratio = range / count;

			float[] list = { 500.0f, 200.0f, 100.0f, 50.0f, 20.0f, 10.0f, 5.0f, 2.0f, 1.0f, 0.5f, 0.25f, 0.1f };

			for (int i = 0; i < list.Length; i++)
			{
				if (ratio > list[i] * 0.5f)
				{
					float rem = (float)Math.IEEERemainder(ratio, list[i]);
					return ratio - rem; 
				}
			}

			return ratio;
		}

		private Bitmap AdjustDoubleBufferSize(Bitmap bmp)
		{
			if (bmp != null)
			{
				if (bmp.Size != ClientSize)
				{
					bmp.Dispose();
					return new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
				}
				else
				{
					return bmp;
				}
			}
			else
			{
				return new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
			}
		}

		private void RenderFinalBuffer()
		{
			using (Graphics graphics = CreateGraphics())
			{
				graphics.DrawImage(_bufferFinal, 0, 0);
			}
		}

		private void UpdateFinalBuffer()
		{
			using (Graphics graphics = Graphics.FromImage(_bufferFinal))
			{
				RenderToBuffer(graphics);
			}
		}

		private void RenderToBuffer(Graphics graphics)
		{
			if (_points == null || _points.Length == 0)
			{
				graphics.DrawImage(_bufferNormal, 0, 0);
				return;
			}

			if (_selectedIndex == _controlIndex)
			{
				graphics.DrawImage(_bufferNormal, 0, 0);

				_oldSelectedIndex = _selectedIndex;
				_oldControlIndex = _controlIndex;
			}
			else if (_invalid)
			{
				int first = Math.Min(_selectedIndex, _controlIndex);
				int last = Math.Max(_selectedIndex, _controlIndex);

				int left = (int)ValueXToScreenX(_points[first].X);
				int right = (int)ValueXToScreenX(_points[last].X);

				DrawBuffer(graphics, _bufferNormal, 0, left);
				DrawBuffer(graphics, _bufferSelected, left, right);
				DrawBuffer(graphics, _bufferNormal, right, ClientSize.Width);

				_invalid = false;
			}
			else
			{
				// old state:
				//
				//          normal              selected              normal
				// |---------------------|xxxxxxxxxxxxxxxxxxxx|--------------------|
				//                    leftOld             rightOld

				int leftIndOld = Math.Min(_oldSelectedIndex, _oldControlIndex);
				int rightIndOld = Math.Max(_oldSelectedIndex, _oldControlIndex);

				int leftOld = (int)ValueXToScreenX(_points[leftIndOld].X);
				int rightOld = (int)ValueXToScreenX(_points[rightIndOld].X);

				// new state
				//
				//      normal         selected               normal
				// |------------|xxxxxxxxxxxxxxxx|---------------------------------|
				//           leftNew          rightNew

				int leftIndNew = Math.Min(_selectedIndex, _controlIndex);
				int rightIndNew = Math.Max(_selectedIndex, _controlIndex);

				int leftNew = (int)ValueXToScreenX(_points[leftIndNew].X);
				int rightNew = (int)ValueXToScreenX(_points[rightIndNew].X);


				// difference
				//                        safety               safety
				//              |xxxxxxxx|x      |------------|-
				//           leftNew leftOld  rightNew    rightOld

				int safety = 2;			// overdraw vertical line

				if (leftNew > leftOld)
				{
					DrawBuffer(graphics, _bufferNormal, leftOld, leftNew);
				}
				else
				{
					DrawBuffer(graphics, _bufferSelected, leftNew, leftOld + safety);
				}
				
				if (rightNew > rightOld)
				{
					DrawBuffer(graphics, _bufferSelected, rightOld, rightNew);
				}
				else
				{
					DrawBuffer(graphics, _bufferNormal, rightNew, rightOld + safety);
				}

				_oldSelectedIndex = _selectedIndex;
				_oldControlIndex = _controlIndex;
			}

			if (_selectedIndex >= 0 && _selectedIndex < _points.Length)
			{
				int dy = 2;
				int x = (int)ValueXToScreenX(_points[_selectedIndex].X);
				graphics.DrawLine(Pens.Brown, x, (int)_chartRect.Top - dy, x, (int)_chartRect.Bottom + dy);
				graphics.DrawLine(Pens.Brown, x + 1, (int)_chartRect.Top - dy, x + 1, (int)_chartRect.Bottom + dy);
			}

			if (_controlIndex >= 0 && _controlIndex < _points.Length)
			{
				int dy = 2;
				int x = (int)ValueXToScreenX(_points[_controlIndex].X);
				graphics.DrawLine(Pens.DarkOliveGreen, x, (int)_chartRect.Top - dy, x, (int)_chartRect.Bottom + dy);
				graphics.DrawLine(Pens.DarkOliveGreen, x + 1, (int)_chartRect.Top - dy, x + 1, (int)_chartRect.Bottom + dy);
			}
		}

		private void DrawBuffer(Graphics graphics, Bitmap bitmap, int left, int right)
		{
			int height = ClientRectangle.Height;

			Rectangle rect = new Rectangle(left, 0, right - left, height);

			graphics.DrawImage(bitmap, rect, rect, GraphicsUnit.Pixel);
		}

		private void Render(Graphics graphics, bool selected)
		{
			using (Brush brush = new LinearGradientBrush(ClientRectangle, BackgroundColorTop, BackgroundColorBottom, LinearGradientMode.Vertical))
			{
				graphics.FillRectangle(brush, ClientRectangle);
			}

			if (_points == null)
				return;

			if (_points.Length == 0)
				return;
			
			DrawCurveFilled(graphics, selected);

			DrawHorzGrid(graphics);
			DrawVertGrid(graphics);

			DrawMarkers(graphics);

			if (_enableAntiAliasing)
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;

				DrawCurve(graphics);
				
				graphics.SmoothingMode = SmoothingMode.Default;
			}
			else
			{
				DrawCurve(graphics);
			}
		}

		private void DrawMarkers(Graphics graphics)
		{
			Pen pen = Pens.Black;
			Brush brush = Brushes.Black;
			Font font = _fontLegend;

			const float overlap = 3.0f;
			const float textOffset = 8.0f;

			List<RectangleF> rectList = new List<RectangleF>();

			// Add left border -> don't overdraw the arrow
			rectList.Add(new RectangleF(_chartRect.Left, 0, 5.0f, _chartRect.Height));

			// Add right border -> no reason really
//			rectList.Add(new RectangleF(_chartRect.Right - 5.0f, 0, 5.0f, _chartRect.Height));

			foreach (Marker marker in _markerList)
			{
				float pos = ValueXToScreenX(marker.Value);
				SizeF textSize = graphics.MeasureString(marker.Text, font);
				float x = pos - textSize.Width * 0.5f;
				float y = _chartRect.Top - overlap - textSize.Height - textOffset;
				RectangleF textRect = new RectangleF(x, y, textSize.Width, textSize.Height);

				graphics.DrawLine(pen, pos, _chartRect.Top - overlap, pos, _chartRect.Bottom + overlap);

				if (CanDrawMarkerDesc(rectList, textRect))
				{
					graphics.DrawString(marker.Text, font, brush, x, y);

					rectList.Add(textRect);
				}
			}
		}

		private static bool CanDrawMarkerDesc(ICollection<RectangleF> rectList, RectangleF rect)
		{
			foreach (RectangleF rc in rectList)
			{
				if (rect.IntersectsWith(rc))
				{
					return false;
				}
			}

			return true;
		}

		private void DrawCurve(Graphics graphics)
		{
			float x1, y1, x2, y2;
			float width = _chartRect.Width; 
			float height = _chartRect.Height; 

			float fx = width / (_roundMax.X - _roundMin.X);
			float fy = height / (_roundMax.Y - _roundMin.Y);

			x1 = _chartRect.Left + fx * (_points[0].X - _roundMin.X);
			y1 = _chartRect.Bottom - fy * (_points[0].Y - _roundMin.Y);

			PointF[] pts = new PointF[_points.Length * 2];

			for (int i = 0; i < _points.Length; i++)
			{
				x2 = _chartRect.Left + fx * (_points[i].X - _roundMin.X);
				y2 = _chartRect.Bottom - fy * (_points[i].Y - _roundMin.Y);

				pts[i * 2 + 0].X = x1;
				pts[i * 2 + 0].Y = y1;
				pts[i * 2 + 1].X = x2;
				pts[i * 2 + 1].Y = y2;

				x1 = x2;
				y1 = y2;
			}

			using (Pen pen = new Pen(LineColor, 1.0f))
			{
				graphics.DrawLines(pen, pts);
			}
		}
		
		private void DrawCurveFilled(Graphics g, bool selected)
		{
			Color colorTop;
			Color colorBottom;
			
			if (selected)
			{
				colorTop = Color.PaleTurquoise;
				colorBottom = Color.DarkCyan;
			}
			else
			{
				colorTop = ChartFillBrushTop;
				colorBottom = ChartFillBrushBottom;
			}

			using (Brush brush = new LinearGradientBrush(ClientRectangle, colorTop, colorBottom, LinearGradientMode.Vertical))
			{
				DrawSubCurveFilled(g, brush);
			}
		}

		private void DrawSubCurveFilled(Graphics graphics, Brush brush)
		{
			float x1, y1, x2, y2;
			float width = _chartRect.Width; 
			float height = _chartRect.Height; 

			float fx = width / (_roundMax.X - _roundMin.X);
			float fy = height / (_roundMax.Y - _roundMin.Y);

			x1 = _chartRect.Left + fx * (_points[0].X - _roundMin.X);
			y1 = _chartRect.Bottom - fy * (_points[0].Y - _roundMin.Y);
			x2 = x1;
			y2 = y1;

			using (GraphicsPath path = new GraphicsPath())
			{
				path.AddLine(x1, _chartRect.Bottom, x1, y1);

				for (int i = 0; i < _points.Length; i++)
				{
					x2 = _chartRect.Left + fx * (_points[i].X - _roundMin.X);
					y2 = _chartRect.Bottom - fy * (_points[i].Y - _roundMin.Y);

					path.AddLine(x1, y1, x2, y2);

					x1 = x2;
					y1 = y2;
				}

				path.AddLine(x2, y2, x2, _chartRect.Bottom);

				graphics.FillPath(brush, path);
			}
		}

		private void DrawVertGrid(Graphics g)
		{
            Pen pen = Pens.Black;
			Brush brush = Brushes.Black;
			
			const float pad_text = 5.0f;
			const float pad_indi = 3.0f;
            const float overlap = 3.0f;

			float x, y;

			float val = _roundMin.Y;
			float range = _roundMax.Y - _roundMin.Y;
			float multi = FindMultiplier(_chartRect.Height, range, 30.0f);

			string fmt = (range > 10.0f) ? "F0" : "F2";

			do
			{
				float pos = _chartRect.Bottom - ((val - _roundMin.Y) / range) * _chartRect.Height;

				string str = val.ToString(fmt, CultureInfo.CurrentCulture);
				SizeF size = g.MeasureString(str, _fontLegend);

				x = _chartRect.Left - size.Width - pad_text;
				y = pos - size.Height * 0.5f;

				g.DrawString(str, _fontLegend, brush, x, y);
				g.DrawLine(pen, _chartRect.Left - pad_indi, pos, _chartRect.Right + overlap, pos);

				val += multi;
			}
			while (val <= _roundMax.Y);

			// Draw vertical arrow
			x = _chartRect.Left;
			y = _chartRect.Top - overlap;
			g.DrawLine(_penArrow, x, y, x, y - 2.0f * _arrowSize);

			// Draw vertical axis description
			SizeF descSize = g.MeasureString(VertDesc, _fontLegend);
			x = _chartRect.Left - descSize.Width * 0.5f;
			y = _chartRect.Top - overlap - descSize.Height - 2.0f * _arrowSize;
			g.DrawString(VertDesc, _fontLegend, brush, x, y); 
		}

		private void DrawHorzGrid(Graphics g)
		{
			Pen pen = Pens.Black;
			Brush brush = Brushes.Black;

			const float pad_text = 5.0f;
			const float pad_indi = 3.0f;
            const float arrowSize = 3.0f;
            const float overlap = 3.0f;

			float x, y;

			float range = _roundMax.X - _roundMin.X;
			float multi = FindMultiplier(_chartRect.Width, range, 30.0f);
			float val = _roundMin.X;

			string fmt = (range > 10.0f) ? "F0" : "F2";

			do
			{
				float pos = _chartRect.Left + ((val - _roundMin.X) / range) * _chartRect.Width;

				string str = val.ToString(fmt, CultureInfo.CurrentCulture);
				SizeF size = g.MeasureString(str, _fontLegend);

				x = pos - size.Width * 0.5f;
				y = _chartRect.Bottom + pad_text;

				g.DrawString(str, _fontLegend, brush, x, y);
				g.DrawLine(pen, pos, _chartRect.Top - overlap, pos, _chartRect.Bottom + pad_indi);

				val += multi;
			}
			while (val <= _roundMax.X);

			// Draw right grid line
			g.DrawLine(pen, _chartRect.Right, _chartRect.Top - overlap, _chartRect.Right, _chartRect.Bottom + pad_indi);

			// Draw horizontal arrow
			x = _chartRect.Right + overlap;
			y = _chartRect.Bottom;
			g.DrawLine(_penArrow, x, y, x + 2.0f * arrowSize, y);

			// Draw horizontal axis description
			x = _chartRect.Right + overlap + arrowSize * 3.0f;
			y = _chartRect.Bottom - _fontLegend.Height / 2 - 1;
			g.DrawString(HorzDesc, _fontLegend, brush, x, y);
		}

		private float ValueXToScreenX(float val)
		{
			float fx = _chartRect.Width / (_roundMax.X - _roundMin.X);
			return _chartRect.Left + fx * (val - _roundMin.X);
		}
		
		private float ScreenXToValueX(float val)
		{
			float fx = (_roundMax.X - _roundMin.X) / _chartRect.Width;
			return _roundMin.X + fx * (val - _chartRect.Left);
		}

		private int FindIndexOfValueX(float val)
		{
			if (val <= _pointMin.X)
				return 0;

			if (val >= _pointMax.X)
				return _points.Length - 1;

			for (int i = 0; i < _points.Length; i++)
			{
				if (_points[i].X > val)
					return i;
			}

			return -1;
		}

		private class Marker
		{
			public Marker(float value, string text)
			{
				Value = value;
				Text = text;
			}

			public float Value { get; private set; }
			public string Text { get; private set; }
		};
	}
}

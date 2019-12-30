//------------------------------------------------------------------------------
// <copyright file="MapChart.Designer.cs" company="">
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

using System;
using System.ComponentModel;
using System.Drawing;

namespace GpxTracker
{
	partial class MapChart
	{
		/// <summary> 
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Occurs when the selected index has changed.
		/// </summary>
		[Category("Action")]
		[Description("Fires when the selection index has changed.")]
		public event EventHandler<IndexChangedEventArgs> SelectedIndexChanged;

		/// <summary>
		/// Occurs when the control index has changed.
		/// </summary>
		[Category("Action")]
		[Description("Fires when the control index has changed.")]
		public event EventHandler<IndexChangedEventArgs> ControlIndexChanged;

		/// <summary>
		/// Gets or sets the background color at the top.
		/// </summary>
		/// <value>The background color at the top.</value>
		[Category("Appearance")]
		[Description("Background color used at the top")]
		[DefaultValue(typeof(Color), "White")]
		public Color BackgroundColorTop { get; set; }

		/// <summary>
		/// Gets or sets the background color at the bottom.
		/// </summary>
		/// <value>The background color at the bottom.</value>
		[Category("Appearance")]
		[Description("Background color used at the bottom")]
		[DefaultValue(typeof(Color), "LightGray")]
		public Color BackgroundColorBottom { get; set; }

		/// <summary>
		/// Gets or sets the color of the chart line.
		/// </summary>
		/// <value>The color of the chart line.</value>
		[Category("Appearance")]
		[Description("Color used to draw the chart line")]
		[DefaultValue(typeof(Color), "Blue")]
		public Color LineColor { get; set; }

		/// <summary>
		/// Gets or sets the chart fill brush at the top.
		/// </summary>
		/// <value>The chart fill brush at the top.</value>
		[Category("Appearance")]
		[Description("Chart color used at the top")]
		[DefaultValue(typeof(Color), "Aquamarine")]
		public Color ChartFillBrushTop { get; set; }

		/// <summary>
		/// Gets or sets the chart fill brush at the bottom.
		/// </summary>
		/// <value>The chart fill brush at the bottom.</value>
		[Category("Appearance")]
		[Description("Chart color used at the bottom")]
		[DefaultValue(typeof(Color), "MediumSlateBlue")]
		public Color ChartFillBrushBottom { get; set; }

		/// <summary>
		/// Gets or sets the horizontal description.
		/// </summary>
		/// <value>The horitontal description.</value>
		[Category("Appearance")]
		[Description("Horizontal description")]
		[DefaultValue("x-Value")]
		public string HorzDesc { get; set; }

		/// <summary>
		/// Gets or sets the vertical description.
		/// </summary>
		/// <value>The vertical description.</value>
		[Category("Appearance")]
		[Description("Vertical description")]
		[DefaultValue("y-Value")]
		public string VertDesc { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether anti-aliasing should be enabled.
		/// </summary>
		/// <value><c>true</c> if anti-aliasing is enable; otherwise, <c>false</c>.</value>
		[Category("Appearance")]
		[Description("Enable Anti-Aliasing")]
		[DefaultValue(false)]
		public bool EnableAntiAliasing
		{
			get 
			{ 
				return _enableAntiAliasing; 
			}

			set
			{
				if (value != _enableAntiAliasing)
				{
					_enableAntiAliasing = value;
					Invalidate();
				}
			}
		}

		#region Vom Komponenten-Designer generierter Code

		/// <summary> 
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				_fontLegend.Dispose();
				_penArrow.Dispose();
				_bufferNormal.Dispose();
				_bufferSelected.Dispose();
				_bufferFinal.Dispose();

				components.Dispose();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Called when the selected index has changed.
		/// </summary>
		protected virtual void OnSelIndexChanged()
		{
			if (SelectedIndexChanged != null)
			{
				SelectedIndexChanged(this, new IndexChangedEventArgs(_selectedIndex, _controlIndex));
			}
		}

		/// <summary>
		/// Called when the control index has changed.
		/// </summary>
		protected virtual void OnControlIndexChanged()
		{
			if (ControlIndexChanged != null)
			{
				ControlIndexChanged(this, new IndexChangedEventArgs(_selectedIndex, _controlIndex));
			}
		}

		/// <summary> 
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// MapChart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Name = "MapChart";
			this.Size = new System.Drawing.Size(603, 197);
			this.ResumeLayout(false);
		}

		#endregion
	}
}

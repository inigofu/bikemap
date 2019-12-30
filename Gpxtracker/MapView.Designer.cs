//------------------------------------------------------------------------------
// <copyright file="MapView.Designer.cs" company="">
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

using System;
using System.ComponentModel;

namespace GpxTracker
{
	partial class MapView
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		[Category("Action")]
		[Description("Fires when the selection index has changed.")]
		public event EventHandler<IndexChangedEventArgs> SelectedIndexChanged;

		[Category("Action")]
		[Description("Fires when the control index has changed.")]
		public event EventHandler<IndexChangedEventArgs> ControlIndexChanged;

		[Category("Action")]
		[Description("Fires when the zoom value has changed.")]
		public event EventHandler<ZoomChangedEventArgs> ZoomChanged;

		protected virtual void OnZoomChanged()
		{
			if (ZoomChanged != null)
			{
				ZoomChanged(this, new ZoomChangedEventArgs(_zoom));
			}
		}

		[Category("Appearance")]
		[Description("Gets or sets flags for visual appearance")]
		[DefaultValue(RenderFlags.None)]
		public RenderFlags MyRenderFlags
		{
			get 
			{ 
				return _renderFlags; 
			}
			set 
			{
				if (_renderFlags != value)
				{
					_renderFlags = value;

					_partMap.RenderThickGrid = (_renderFlags & RenderFlags.ThickGrid) == RenderFlags.ThickGrid;
					_partMap.RenderThinGrid = (_renderFlags & RenderFlags.ThinGrid) == RenderFlags.ThinGrid;
					_partMap.RenderTileNumbers = (_renderFlags & RenderFlags.Numbers) == RenderFlags.Numbers;
					_partTrack.RenderAntiAliased = (_renderFlags & RenderFlags.AntiAlias) == RenderFlags.AntiAlias;

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
			if (disposing)
			{
				// TODO: dispose _parts
				//if (_font != null)
				//    _font.Dispose();

				//if (_imageProvider != null)
				//    _imageProvider.Dispose();

				if (components != null)
					components.Dispose();
			}

			base.Dispose(disposing);
		}

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung. 
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		#endregion
	}
}

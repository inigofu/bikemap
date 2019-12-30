//------------------------------------------------------------------------------
// <copyright file="SettingsEditDialog.cs" company="">
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
// <summary>Contains information about the SettingsEditDialog class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Windows.Forms;

	/// <summary>
	/// Represents a settings dialog for map image sources.
	/// </summary>
	public partial class SettingsEditDialog : Form
	{
		private MapDataSource _source;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsEditDialog"/> class.
		/// </summary>
		/// <param name="source">The source.</param>
		public SettingsEditDialog(MapDataSource source)
		{
			_source = source;

			InitializeComponent();

			string _format = _source.Format;
			_format = _format.Replace("{0}", "{zoom}");
			_format = _format.Replace("{1}", "{x}");
			_format = _format.Replace("{2}", "{y}");

			textBoxName.Text = _source.Name;
			textBoxFormat.Text = _format;
			textBoxLocalPath.Text = _source.LocalPath;
			numEditMaxZoom.Value = _source.MaxZoom;
			numEditMinZoom.Value = _source.MinZoom;
			textBoxName.Name = _source.Name;
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			string _format = textBoxFormat.Text;
			_format = _format.Replace("{zoom}", "{0}");
			_format = _format.Replace("{x}", "{1}");
			_format = _format.Replace("{y}", "{2}");

			_source.Format = _format;
			_source.LocalPath = textBoxLocalPath.Text;
			_source.MaxZoom = (int)numEditMaxZoom.Value;
			_source.MinZoom = (int)numEditMinZoom.Value;
			_source.Name = textBoxName.Text;
		}
	}
}

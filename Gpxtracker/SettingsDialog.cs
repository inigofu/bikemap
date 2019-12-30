//------------------------------------------------------------------------------
// <copyright file="SettingsDialog.cs" company="">
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
// <summary>Contains information about the SettingsDialog class.</summary>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GpxTracker
{
	using CacheComboBoxItem = ComboBoxItem<string, TimeSpan>;

	/// <summary>
	/// Represents the settings dialog.
	/// </summary>
	public partial class SettingsDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsDialog"/> class.
		/// </summary>
		public SettingsDialog()
		{
			InitializeComponent();

			textBoxFolder.Text = FileSystem.LocalRootFolder;

			comboBoxSystem.DataBindings.Add("SelectedIndex", Properties.Settings.Default, "System");

			comboBoxMapSource.DataSource = Properties.Settings.Default.MapDataSourceList;
			comboBoxMapSource.DisplayMember = "Name";
			comboBoxMapSource.DataBindings.Add("SelectedIndex", Properties.Settings.Default, "MapDataSourceIndex");

			// The values listed here _must_ contain the default value 
			// specified in Properties.Settings.Default.MapDataMaxAge
			List<CacheComboBoxItem> cacheItems = new List<CacheComboBoxItem>();
			cacheItems.Add(new CacheComboBoxItem("Session", TimeSpan.Zero));
			cacheItems.Add(new CacheComboBoxItem("1 Day", TimeSpan.FromDays(1.0)));
			cacheItems.Add(new CacheComboBoxItem("7 Days", TimeSpan.FromDays(7.0)));
			cacheItems.Add(new CacheComboBoxItem("30 Days", TimeSpan.FromDays(30.0)));
			cacheItems.Add(new CacheComboBoxItem("90 Days", TimeSpan.FromDays(90.0)));
			cacheItems.Add(new CacheComboBoxItem("Always", TimeSpan.FromDays(10000.0)));

			comboBoxCacheHistory.DataSource = cacheItems;
			comboBoxCacheHistory.ValueMember = "Value";
			comboBoxCacheHistory.DisplayMember = "Display";
			comboBoxCacheHistory.DataBindings.Add("SelectedValue", Properties.Settings.Default, "MapDataMaxAge");
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			// Update filesystem prior to all other classes
			FileSystem.LocalRootFolder = textBoxFolder.Text;

			Properties.Settings.Default.Save();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.Reload();
		}

		private void buttonEditSource_Click(object sender, EventArgs e)
		{
			MapDataSource source = (MapDataSource)comboBoxMapSource.SelectedItem;

			SettingsEditDialog dialog = new SettingsEditDialog(source);
			dialog.ShowDialog(this);
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.SelectedPath = textBoxFolder.Text;

			DialogResult result = folderBrowserDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				textBoxFolder.Text = folderBrowserDialog.SelectedPath;
			}
		}
	}
}

//------------------------------------------------------------------------------
// <copyright file="SyncForm.cs" company="">
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
// <summary>Contains information about the SyncForm class.</summary>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace GpxTracker
{
	partial class SyncForm : DeviceNotifyForm
	{
		private const string _subDirectory = "Garmin\\History";

		private List<TrackFileInfo> TrackFileList = new List<TrackFileInfo>();

		public SyncForm()
		{
			InitializeComponent();

			DirectoryInfo info = FindFolder();
			
			UpdateTextbox(info);
			FillList(info);
			UpdateDataGridView();

			DeviceArrivedEvent += new EventHandler<DeviceEventArgs>(formDeviceArrivedEvent);
		}
/*
		// TODO: make MainForm DeviceNotifyForm and call this with
		public SyncForm(char volume)
		{
			InitializeComponent();

			DirectoryInfo info = new DirectoryInfo(volume + ":\\" + _subDirectory);

			// if not found in the default directory use root instead
			if (!info.Exists)
				info = new DirectoryInfo(volume + ":\\");

			UpdateTextbox(info);
			FillList(info);
			UpdateDataGridView();

			DeviceArrivedEvent += new EventHandler<DeviceEventArgs>(FormDeviceArrivedEvent);
		}
*/
		private void formDeviceArrivedEvent(object sender, DeviceEventArgs args)
		{
			DirectoryInfo info = new DirectoryInfo(args.Volume + ":\\" + _subDirectory);

			// if not found in the default directory use root instead
			if (!info.Exists)
				info = new DirectoryInfo(args.Volume + ":\\");

			// TODO: revisit
			UpdateTextbox(info);
			FillList(info);
			UpdateDataGridView();
		}

		private void UpdateTextbox(DirectoryInfo info)
		{
			if (info != null && info.Exists)
			{
				textBoxFolder.Text = info.FullName;
			}
		}

		private static DirectoryInfo FindFolder()
		{
			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.DriveType == DriveType.Removable)
				{
					DirectoryInfo di = new DirectoryInfo(drive.Name + _subDirectory);

					if (di.Exists)
					{
						return di;
					}
				}
			}

			// TODO: replace with users home folder
			return new DirectoryInfo("F:\\Garmin\\history");
		}

		private void UpdateDataGridView()
		{
			dataGridView.Rows.Clear();

			foreach (TrackFileInfo fi in TrackFileList)
			{
				string desc = fi.FileName.Remove(fi.FileName.Length-4);

			//	DateTime time;
			//	if (DateTime.TryParseExact(desc, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))

				object[] items = 
				{
					false,
					fi.FileName,
					fi.ExistLocal ? "Yes" : "No",
					fi.Size
				};

				int index = dataGridView.Rows.Add(items);
				DataGridViewRow row = dataGridView.Rows[index];

				row.Tag = fi;

				if (fi.ExistLocal)
				{
					row.DefaultCellStyle.BackColor = Color.LightGray;
					row.ReadOnly = true;
				}
			}

			dataGridView.Sort(dgvColDescription, ListSortDirection.Descending);

			labelSelectedFiles.Text = String.Format(CultureInfo.CurrentCulture, "{0} Files", dataGridView.Rows.Count);
		}

		private void FillList(DirectoryInfo dir)
		{
			DirectoryInfo dil = new DirectoryInfo(FileSystem.LocalTrackFolder);

			if (!dir.Exists)
				return;

			if (!dil.Exists)
				return;

			FileInfo[] fi_remote = dir.GetFiles();			// there is a version that supports wildcards,
            FileInfo[] fi_local = dil.GetFiles();			// but it does not work correctly

			TrackFileList.Clear();

            foreach (FileInfo fi in fi_remote)
            {
				if (TrackDataManager.CanOpenFile(fi.FullName))
				{
					TrackFileInfo info = new TrackFileInfo();

					info.FileName = fi.Name;
					info.FullNameRemote = fi.FullName;
					info.FullNameLocal = dil.FullName + Path.DirectorySeparatorChar + fi.Name;
					info.ExistLocal = false;
					info.Size = fi.Length;

					TrackFileList.Add(info);
				}
            }
            
            foreach (FileInfo fi in fi_local)
            {
				if (TrackDataManager.CanOpenFile(fi.FullName))
				{
					foreach (TrackFileInfo info in TrackFileList)
					{
						if (info.FileName == fi.Name)
						{
							info.ExistLocal = true;
							break;
						}
					}
				}
            }
        }

		private void buttonOK_Click(object sender, EventArgs e)
		{
			List<TrackFileInfo> list = new List<TrackFileInfo>();

			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				if ((bool)row.Cells[0].Value == true)		// checkBox
				{
					TrackFileInfo tfi = row.Tag as TrackFileInfo;
					list.Add(tfi);
				}
			}

			CopyForm cf = new CopyForm(list);
			cf.ShowDialog();
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = textBoxFolder.Text;
			DialogResult result = folderBrowserDialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				DirectoryInfo info = new DirectoryInfo(folderBrowserDialog.SelectedPath);

				UpdateTextbox(info);
				FillList(info);
				UpdateDataGridView();
			}
		}

		private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dataGridView.Columns[e.ColumnIndex].Name == "dgvColSize")
			{
				e.Value = Units.Provider.DiskSpaceString((long)e.Value);
				e.FormattingApplied = true;
			}
		}

		private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				if (!row.ReadOnly)
				{
					row.Cells[dgvColCheck.Name].Value = checkBoxSelectAll.Checked;
				}
			}
		}

		// never called
		// TODO: find appropriate checkedChange event
		// google: CurrentCellDirtyStateChanged Event
/*		private void UpdateSelectedFilesCount()
		{
			int count = 0;
			foreach (DataGridViewRow row in dataGridView.Rows)
			{
				if ((bool)row.Cells[dgvColCheck.Name].Value == true)
					count++;
			}

			labelSelectedFiles.Text = String.Format(CultureInfo.CurrentCulture, "{0} File{1} selected", count, count == 1 ? "" : "s");
		}
*/
	}
}

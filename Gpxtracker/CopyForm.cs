//------------------------------------------------------------------------------
// <copyright file="CopyForm.cs" company="">
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
// <summary>Contains information about the CopyForm class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Threading;
	using System.Windows.Forms;

	/// <summary>
	/// Displays a form with a copy animation while files are being copied.
	/// </summary>
	public partial class CopyForm : Form
	{
		private IEnumerable<TrackFileInfo> copyFileList;

		private int fileCountNow;
		private int fileCountTotal;

		private long fileSizeNow;
		private long fileSizeTotal;

		/// <summary>
		/// Initializes a new instance of the <see cref="CopyForm"/> class.
		/// </summary>
		/// <param name="list">The list of file that should be copied.</param>
		public CopyForm(IEnumerable<TrackFileInfo> list)
		{
			InitializeComponent();

			copyFileList = list;

			fileSizeTotal = 0;
			fileCountTotal = copyFileList.Count();

			foreach (TrackFileInfo fi in copyFileList)
			{
				fileSizeTotal += fi.Size;
			}

			progressBar.Value = 0;
			progressBar.Maximum = (int)fileSizeTotal;
		}

		/// <summary>
		/// Updates the gui status labels and the progress bar.
		/// </summary>
		/// <param name="file">The track file info.</param>
		private void UpdateStatus(TrackFileInfo file)
		{
			fileCountNow++;
			fileSizeNow += file.Size;

			progressBar.Value = (int)fileSizeNow;

			labelStatus0.Text = file.FileName;
			labelStatus1.Text = String.Format(CultureInfo.CurrentCulture, "Files: {0} / {1}", fileCountNow, fileCountTotal);
		}

		private void Finish()
		{
			animation.Finish();

			buttonCancel.Enabled = false;
			buttonOK.Enabled = true;
		}

		private void CopyForm_Load(object sender, EventArgs e)
		{
			var updateDelegate = new Action<TrackFileInfo>(UpdateStatus);
			var finishDelegate = new Action(Finish);

			Thread thread = new Thread(new ThreadStart(() =>
			{
				foreach (TrackFileInfo fi in copyFileList)
				{
					File.Copy(fi.FullNameRemote, fi.FullNameLocal);

					//Thread.Sleep((int)fi.Size / 3000);

					Invoke(updateDelegate, fi);
				}

				Invoke(finishDelegate);
			}));

			thread.Start();
		}
	}
}

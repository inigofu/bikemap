//------------------------------------------------------------------------------
// <copyright file="MainForm.cs" company="">
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
// <summary>Contains information about the MainForm class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Configuration;
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.Globalization;
	using System.Text;
	using System.Threading;
	using System.Windows.Forms;
	using GpxTracker.Properties;

	/// <summary>
	/// The main form of the application.
	/// </summary>
	public partial class MainForm : Form
    {
		private enum ChartModeVert
		{
			Height,
			Speed,
		};

		private enum ChartModeHorz
		{
			Distance,
			Time
		};

		private TrackDataManager _trackDataManager = new TrackDataManager();

		private ChartModeHorz chartModeHorz;
		private ChartModeVert chartModeVert;

		private LoadingDialog loadingDialog = new LoadingDialog();

        private int secuencia;
        private double distancia;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainForm"/> class.
		/// </summary>
        public MainForm()
        {
            InitializeComponent();

			Settings.Default.SettingsSaving += new SettingsSavingEventHandler(Settings_SettingsSaving);
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			UpdateChartButtons();
			UpdateZoomButtons();

			UpdateMapDataSource();

			ClearInfoBox();

		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			ShowFirstStartDialog();

			Application.DoEvents();

			LoadTracks();
		}

		private void ShowFirstStartDialog()
		{
			if (Settings.Default.FirstStartFlag)
			{
				FirstStartDialog firstStartDialog = new FirstStartDialog();
				if (firstStartDialog.ShowDialog(this) == DialogResult.Yes)
				{
					SyncForm syncForm = new SyncForm();
					syncForm.ShowDialog();

					// LoadTracks() is called anyway later in MainForm_Shown()
				}

				Settings.Default.FirstStartFlag = false;
				Settings.Default.Save();						// TODO: save only when exiting?
			}
		}

		private void Settings_SettingsSaving(object sender, CancelEventArgs e)
		{
			UpdateMapDataSource();
		}

		private void InitializeTracklistView()
		{
			// DataGridView is not sortable when DataSource is set,
			// unless the source is a sortable BindingList<T> which
			// has to be implemented manually.

			SortableBindingList<TrackActivity> list = new SortableBindingList<TrackActivity>(_trackDataManager.Items);

			dataGridView.AutoGenerateColumns = false;
			dataGridView.DataSource = list;

			dataGridView.Sort(colTime, ListSortDirection.Descending);

			UpdateMapView();
			UpdateMapChart();
			UpdateChartLabels();
			UpdateChartButtons();
		}

		private TrackActivity GetSelectedTrack()
		{
			if (dataGridView.SelectedRows.Count == 0)
				return null;

			DataGridViewRow row = dataGridView.SelectedRows[0];

			return row.DataBoundItem as TrackActivity;
		}

		private void UpdateMapChart()
		{
			TrackActivity track = GetSelectedTrack();

			if (track == null)
				return;

			mapChart.ClearMarkers();

			List<PointF> chartPts = new List<PointF>();

			foreach (TrackLap lap in track.Laps)
			{
				foreach (TrackPoint pt in lap.Points)
				{
					chartPts.Add(ChartPointFromTrackPoint(pt));
				}

				if (lap.Points.Count > 0)
				{
					string text = String.Format(CultureInfo.CurrentCulture, "Lap {0}", lap.Index);
					mapChart.AddMarker(ChartPointFromTrackPoint(lap.Points[0]).X, text);
				}
			}
			
			mapChart.SetPoints(chartPts.ToArray());
		}

		private void SetMapViewTrackPoints(TrackActivity track)
		{
			List<PointF> points = new List<PointF>();

			foreach (TrackLap lap in track.Laps)
			{
				foreach (TrackPoint pt in lap.Points)
				{
					points.Add(new PointF(pt.Longitude, pt.Latitude));
				}
			}

			mapView.SetPoints(points.ToArray());
		}

		private void SetMapViewWaypoints(TrackActivity track)
		{
			mapView.ClearWaypoints();

			foreach (TrackWaypoint pt in track.Waypoints)
			{
				string text = pt.Name;

				if (!String.IsNullOrEmpty(pt.Name))
				{
					if (!String.IsNullOrEmpty(pt.Description))
						text += " - " + pt.Description;
				}
				else 
				{
					text = pt.Description;
				}

				PointF wpt = new PointF(pt.Longitude, pt.Latitude);
				mapView.AddWaypoint(wpt, text);
			}
		}

		private void SetMapViewMarkers(TrackActivity track)
		{
			mapView.ClearMarkers();

			foreach (TrackLap lap in track.Laps)
			{
				if (lap.Points.Count > 0)
				{
					string text = String.Format(CultureInfo.CurrentCulture, "Lap {0}", lap.Index);
					mapView.AddMarker(lap.Points[0].Index, text);
				}
			}
		}

		private void UpdateMapView()
		{
			TrackActivity track = GetSelectedTrack();

			if (track == null)
				return;

			SetMapViewTrackPoints(track);
			SetMapViewWaypoints(track);
			SetMapViewMarkers(track);

			mapView.ResetView();
		}

		private void UpdateChartLabels()
		{
			switch (chartModeVert)
			{
				case ChartModeVert.Height:
					mapChart.VertDesc = Units.Provider.HeightUnitString();
					break;

				case ChartModeVert.Speed:
					mapChart.VertDesc = Units.Provider.SpeedUnitString();
					break;
			};

			switch (chartModeHorz)
			{
				case ChartModeHorz.Distance:
					mapChart.HorzDesc = Units.Provider.DistanceUnitString();
					break;

				case ChartModeHorz.Time:
					mapChart.HorzDesc = "min";
					break;
			};
		}
		
		private MyPointF ChartPointFromTrackPoint(TrackPoint trackPt)
		{
			float x = 0;
			float y = 0;

			switch (chartModeVert)
			{
				case ChartModeVert.Height:
					y = (float)Units.Provider.ConvertHeight(trackPt.Altitude);
					break;

				case ChartModeVert.Speed:
					y = (float)Units.Provider.ConvertSpeed(trackPt.Speed);
					break;
			};

			switch (chartModeHorz)
			{
				case ChartModeHorz.Distance:
					x = (float)Units.Provider.ConvertDistance(trackPt.Distance * 0.001f);
					break;

				case ChartModeHorz.Time:
					x = (float)trackPt.TimeDelta.TotalMinutes;
					break;
			};

			return new MyPointF(x, y);
		}

		private void UpdateInfoBox(int selIndex, int ctrlIndex)
		{
			TrackActivity track = GetSelectedTrack();
            
            if (track != null)
			{
				TrackPoint selPoint = GetPointFromIndex(track, selIndex);
				TrackPoint ctrlPoint = GetPointFromIndex(track, ctrlIndex);

				if (selPoint != null && ctrlPoint != null)
				{
					float dist = Math.Abs(selPoint.Distance - ctrlPoint.Distance);
					float acc_asc = Math.Abs(selPoint.AccAsc - ctrlPoint.AccAsc);
					float acc_desc = Math.Abs(selPoint.AccDesc - ctrlPoint.AccDesc);
					TimeSpan time = (selPoint.Time - ctrlPoint.Time).Duration();
					
					labelTime.Text = time.ToString();
					labelDist.Text = Units.Provider.DistanceString(dist);

					labelAlti.Text = Units.Provider.HeightString(selPoint.Altitude);

					labelAccAsc.Text = Units.Provider.HeightString(acc_asc);
					labelAccDesc.Text = Units.Provider.HeightString(acc_desc);

					labelSpeedAvg.Text = Units.Provider.SpeedString(time, dist);
					labelSpeedCurr.Text = Units.Provider.SpeedString(selPoint.Speed);
				}
				else
				{
					ClearInfoBox();
				}
			}
		}

		private static TrackPoint GetPointFromIndex(TrackActivity track, int index)
		{
			foreach (TrackLap lap in track.Laps)
			{
				if (index >= lap.FirstPointIndex && index <= lap.LastPointIndex)
				{
					return lap.Points[index - lap.FirstPointIndex];
				}
			}

			return null;
		}
        private static int GetIndexDistance(TrackActivity track, double meter, int index)
        {
            foreach (TrackLap lap in track.Laps)
            {
                if (index >= lap.FirstPointIndex && index <= lap.LastPointIndex)
                {
                   
                        for (int i = index; i < lap.LastPointIndex; i++)
                        {
                            if (lap.Points[i].Distance >= meter)
                            {
                                return i;
                            }
                        }
                    
                }
            }

            return 0;
        }

        private void ClearInfoBox()
		{
			string emptyStr = "-";

			labelTime.Text = emptyStr;

			labelDist.Text = emptyStr;

			labelAlti.Text = emptyStr;

			labelAccAsc.Text = emptyStr;
			labelAccDesc.Text = emptyStr;

			labelSpeedAvg.Text = emptyStr;
			labelSpeedCurr.Text = emptyStr;
		}

		private void LoadTracks()
		{
			Thread thread = new Thread(new ThreadStart(LoadFileThreadProc));
			thread.Name = "Loading thread";
			thread.Start();

			loadingDialog.ShowDialog(this);		// start internal message pump

			thread.Join();						// should finish immediately

			InitializeTracklistView();
		}


		private void LoadFileThreadProc()
		{
			_trackDataManager.ProcessFolder(FileSystem.LocalTrackFolder);
			_trackDataManager.RepairTrackData();

			Invoke(new MethodInvoker(loadingDialog.Close));		// send close message to exit ShowDialog() in main thread
		}

		private void mapView_SelIndexChanged(object sender, IndexChangedEventArgs e)
		{
			UpdateInfoBox(e.SelectedIndex, e.ControlIndex);
			mapChart.SetIndexes(e.SelectedIndex, e.ControlIndex);
		}

		private void mapView_ControlIndexChanged(object sender, IndexChangedEventArgs e)
		{
			UpdateInfoBox(e.SelectedIndex, e.ControlIndex);
			mapChart.SetIndexes(e.SelectedIndex, e.ControlIndex);
		}

		private void mapChart_SelIndexChanged(object sender, IndexChangedEventArgs e)
		{
			UpdateInfoBox(e.SelectedIndex, e.ControlIndex);
			mapView.SetIndices(e.SelectedIndex, e.ControlIndex);
		}

		private void mapChart_ControlIndexChanged(object sender, IndexChangedEventArgs e)
		{
			UpdateInfoBox(e.SelectedIndex, e.ControlIndex);
			mapView.SetIndices(e.SelectedIndex, e.ControlIndex);
		}

		private void menuItemExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void tsbZoomIn_Click(object sender, EventArgs e)
		{
			mapView.ZoomIn();
			tslZoom.Text = mapView.GetZoom().ToString(CultureInfo.CurrentCulture);
		}

		private void tsbZoomOut_Click(object sender, EventArgs e)
		{
			mapView.ZoomOut();
			tslZoom.Text = mapView.GetZoom().ToString(CultureInfo.CurrentCulture);
		}

		private void menuItemAbout_Click(object sender, EventArgs e)
		{
			
		}

		private void menuItemSettings_Click(object sender, EventArgs e)
		{
			SettingsDialog settingDialog = new SettingsDialog();
			settingDialog.ShowDialog(this);
		}

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
			UpdateMapView();
			UpdateMapChart();
		}

		// Clicking on the column header causes the sorting order to change
		// this again causes the selection to change
		private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			dataGridView_SelectionChanged(sender, e);
		}

		private void tsbChartSpeed_Click(object sender, EventArgs e)
		{
			chartModeVert = ChartModeVert.Speed;
			UpdateChartButtons();
			UpdateChartLabels();
			UpdateMapChart();
		}

		private void tsbChartHeight_Click(object sender, EventArgs e)
		{
			chartModeVert = ChartModeVert.Height;
			UpdateChartButtons();
			UpdateChartLabels();
			UpdateMapChart();
		}

		private void tsbChartTime_Click(object sender, EventArgs e)
		{
			chartModeHorz = ChartModeHorz.Time;
			UpdateChartButtons();
			UpdateChartLabels();
			UpdateMapChart();
		}

		private void tsbChartDistance_Click(object sender, EventArgs e)
		{
			chartModeHorz = ChartModeHorz.Distance;
			UpdateChartButtons();
			UpdateChartLabels();
			UpdateMapChart();
		}

		private void menuItemImport_Click(object sender, EventArgs e)
		{
			SyncForm syncForm = new SyncForm();
			if (syncForm.ShowDialog() == DialogResult.OK)
			{
				LoadTracks();
			}
		}

		private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.Value == null)
				return;

			DataGridViewColumn column = dataGridView.Columns[e.ColumnIndex];

			if (column.Name == "colDistance")
			{
				e.Value = Units.Provider.DistanceString((Double)e.Value);
				e.FormattingApplied = true;
			}
			else if (column.Name == "colTime")
			{
				e.Value = Units.Provider.TimeString((TimeSpan)e.Value);
			}
		}

		private void mapView_ZoomChanged(object sender, ZoomChangedEventArgs e)
		{
			tslZoom.Text = e.Zoom.ToString(CultureInfo.CurrentCulture);
		}

		private void UpdateChartButtons()
		{
			this.tsbChartTime.Checked = (chartModeHorz == ChartModeHorz.Time);
			this.tsbChartDistance.Checked = (chartModeHorz == ChartModeHorz.Distance);
			this.tsbChartHeight.Checked = (chartModeVert == ChartModeVert.Height);
			this.tsbChartSpeed.Checked = (chartModeVert == ChartModeVert.Speed);
		}

		private void UpdateMapDataSource()
		{
			int index = Settings.Default.MapDataSourceIndex;
			List<MapDataSource> list = Settings.Default.MapDataSourceList;

			MapDataSource source = null;

			if (index >= 0 && index < list.Count)
			{
				source = Settings.Default.MapDataSourceList[index];
			}

			mapView.SetMapDataSource(source);
		}

		private void UpdateZoomButtons()
		{
			tslZoom.Text = mapView.GetZoom().ToString(CultureInfo.CurrentCulture);
		}

		private static string ConstructImageFilter()
		{
			StringBuilder sum = new StringBuilder();
			StringBuilder result = new StringBuilder();

			ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
			
			foreach (ImageCodecInfo info in codecs)
			{
				string extension = info.FilenameExtension.ToLowerInvariant();

				if (result.Length > 0)
					result.Append("|");

				if (sum.Length > 0)
					sum.Append(";");

				sum.Append(extension);

				result.AppendFormat("{0} ({1})|{1}", info.CodecName, extension);
			}

			result.AppendFormat("|All image files|{0}", sum);
			result.Append("|All files (*.*)|*.*");

			return result.ToString();
		}

		private void tsbScreenshot_Click(object sender, EventArgs e)
		{
            /*SaveFileDialog ofd = new SaveFileDialog();
		//	ofd.Title = 
		//	ofd.InitialDirectory = 
			ofd.Filter = ConstructImageFilter();
			ofd.FilterIndex = 1;
			ofd.AddExtension = true;
			ofd.RestoreDirectory = true;

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				using (Bitmap bmp = mapView.RenderToBitmap())
				{
					bmp.Save(ofd.FileName);
				}
			}*/
            distancia = distancia + 100;
            TrackActivity track = GetSelectedTrack();
            secuencia = GetIndexDistance(track,distancia,secuencia);
            UpdateInfoBox(secuencia,0);
            mapChart.SetIndexes(secuencia, 0);
            mapView.SetIndices(secuencia, 0);
        }

		private void yardstickMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			mapView.ShowYardstick(menuItemYardstick.Checked);
		}

		private void mapGridMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			mapView.MyRenderFlags ^= MapView.RenderFlags.ThinGrid;
		}

		private void mapNumbersMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			mapView.MyRenderFlags ^= MapView.RenderFlags.Numbers;
		}

		private void tsbMeasure_Click(object sender, EventArgs e)
		{
			tsbMeasure.Checked = !tsbMeasure.Checked;

			mapView.ClearDistances();
			mapView.SetDistanceMeasureMode(tsbMeasure.Checked);
		}

        private void MapView_Click(object sender, EventArgs e)
        {

        }
    }
}

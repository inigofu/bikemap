//------------------------------------------------------------------------------
// <copyright file="TrackDataManager.cs" company="">
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
// <summary>Contains information about the TrackDataManager class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.IO;

	/// <summary>
	/// Represents a manager class for <see cref="TrackFileProvider"/>s.
	/// </summary>
	class TrackDataManager
	{
		private List<TrackActivity> _trackData = new List<TrackActivity>();
		private static List<TrackFileProvider> _trackDataProvider = new List<TrackFileProvider>();

		/// <summary>
		/// Initializes the <see cref="TrackDataManager"/> class.
		/// </summary>
		static TrackDataManager()
		{
			_trackDataProvider.Add(new TrackFileProviderGpx());
			_trackDataProvider.Add(new TrackFileProviderKml());
			_trackDataProvider.Add(new TrackFileProviderTcx());
		}

		/// <summary>
		/// Gets the track activity items.
		/// </summary>
		/// <value>The items.</value>
		public IList<TrackActivity> Items
		{
			get
			{
				return _trackData;
			}
		}

		/// <summary>
		/// Determines whether the specified file can be read.
		/// </summary>
		/// <param name="filename">The filename.</param>
		/// <returns>
		/// 	<c>true</c> if the specified file can be read; otherwise, <c>false</c>.
		/// </returns>
		public static bool CanOpenFile(string filename)
		{
			foreach (TrackFileProvider provider in _trackDataProvider)
			{
				if (provider.CanReadFile(filename))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Clears all track data.
		/// </summary>
		public void Clear()
		{
			_trackData.Clear();
		}

		/// <summary>
		/// Processes the folder. Search in all sub-folders for valid track files and add them to the list.
		/// </summary>
		/// <param name="folder">The folder.</param>
		public void ProcessFolder(string folder)
		{
			DirectoryInfo dir = new DirectoryInfo(folder);

			if (!dir.Exists)
				return;

			foreach (TrackFileProvider provider in _trackDataProvider)
			{
				foreach (FileInfo fi in dir.GetFiles())
				{
					if (provider.CanReadFile(fi.FullName))
					{
						ICollection<TrackActivity> activities = provider.ReadFile(fi.FullName);

						foreach (TrackActivity activity in activities)
						{
							PostProcessActivityData(activity);

							_trackData.Add(activity);
						}
					}
				}
			}
		}

		/// <summary>
		/// Computes the distance between two points.
		/// </summary>
		/// <param name="pos1">The first position.</param>
		/// <param name="pos2">The second position.</param>
		/// <returns>The distance between two points</returns>
		public static double ComputeDistance(TrackPoint pos1, TrackPoint pos2)
		{
			return ComputeDistance(pos1.Latitude, pos1.Longitude, pos2.Latitude, pos2.Longitude);
		}

		/// <summary>
		/// Computes the distance between two points.
		/// </summary>
		/// <param name="lat1">The latitude of the first point.</param>
		/// <param name="lon1">The longitude of the first point.</param>
		/// <param name="lat2">The latitude of the second point.</param>
		/// <param name="lon2">The longitude of the second point.</param>
		/// <returns>The distance between two points</returns>
		public static double ComputeDistance(double lat1, double lon1, double lat2, double lon2)
		{
			double radius = 6371000;		// 6371 kilometers == 3960 miles

			double deltaLat = toRadian(lat2 - lat1);
			double deltaLon = toRadian(lon2 - lon1);

			// a is the square of half the chord length between the points
			double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
				Math.Cos(toRadian(lat1)) * Math.Cos(toRadian(lat2)) *
				Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

			// c is the angular distance in radians
			double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));

			return radius * c;
		}

		/// <summary>
		/// Repairs the track data. Repairing contains:
		/// <list type="bullet">
		/// <item>Remove empty tracks.</item>
		/// <item>Fix empty names.</item>
		/// <item>Fix non-null distance first entries.</item>
		/// <item>Fix missing distances.</item>
		/// </list>
		/// </summary>
		public void RepairTrackData()
		{
			RemoveEmptyTracks();
			FixEmptyNames();
			FixNonNullStarts();
			FixDistances();
		}

		private void FixEmptyNames()
		{
			foreach (TrackActivity activity in _trackData)
			{
				if (String.IsNullOrEmpty(activity.Name))
				{
					activity.Name = Path.GetFileName(activity.FileName);
				}
			}
		}

		private void FixDistances()
		{
			foreach (TrackActivity activity in _trackData)
			{
				if (activity.Laps.Count == 0)
					continue;

				if (activity.Laps[0].Points.Count == 0)
					continue;

				TrackPoint prevPoint = activity.Laps[0].Points[0];

				foreach (TrackLap lap in activity.Laps)
				{
					foreach (TrackPoint point in lap.Points)
					{
						if (point.Distance < prevPoint.Distance)
						{
							point.Distance = prevPoint.Distance + (float)ComputeDistance(prevPoint, point);
						}

						prevPoint = point;
					}
				}
			}
		}

		private void FixNonNullStarts()
		{
			foreach (TrackActivity activity in _trackData)
			{
				if (activity.Laps.Count > 0)
				{
					float offset = activity.Laps[0].Points[0].Distance;

					if (offset != 0.0)
					{
						foreach (TrackLap lap in activity.Laps)
						{
							foreach (TrackPoint point in lap.Points)
							{
								point.Distance -= offset;
							}

						}

						activity.Laps[0].TotalDistance -= offset;
						activity.TotalDistance -= offset;
					}
				}
			}
		}

		private void RemoveEmptyTracks()
		{
			foreach (TrackActivity activity in _trackData)
			{
				activity.Laps.RemoveAll((TrackLap lap) => (lap.TotalDistance == 0.0));
			}

			_trackData.RemoveAll((TrackActivity activity) => (activity.TotalDistance == 0.0 && activity.Waypoints.Count == 0));
		}

		private static void PostProcessActivityData(TrackActivity activity)
		{
			int lapIndex = 0;
			int pointIndex = 0;

			TimeSpan totalTime = TimeSpan.Zero;
			double totalDist = 0;

			TrackPoint firstPoint = FindFirstPoint(activity);

			if (firstPoint == null)
				return;

			foreach (TrackLap lap in activity.Laps)
			{
				lap.Index = lapIndex++;
				lap.FirstPointIndex = pointIndex;
				lap.LastPointIndex = pointIndex + lap.Points.Count - 1;

				PostProcessPointIndex(lap, ref pointIndex);
				PostProcessPointTimeDelta(lap, firstPoint.Time);

				PostProcessLapData(lap);

				totalTime += lap.TotalTime;
				totalDist += lap.TotalDistance;
			}

			activity.StartTime = firstPoint.Time;
			activity.TotalTime = totalTime;
			activity.TotalDistance = totalDist;
		}

		private static TrackPoint FindFirstPoint(TrackActivity activity)
		{
			foreach (TrackLap lap in activity.Laps)
			{
				foreach (TrackPoint point in lap.Points)
				{
					return point;
				}
			}

			return null;
		}

		private static void PostProcessPointTimeDelta(TrackLap lap, DateTime startTime)
		{
			foreach (TrackPoint point in lap.Points)
			{
				point.TimeDelta = point.Time - startTime;
			}
		}

		private static void PostProcessPointIndex(TrackLap lap, ref int firstIndex)
		{
			foreach (TrackPoint point in lap.Points)
			{
				point.Index = firstIndex++;
			}
		}

		private static void PostProcessPointDistance(TrackLap lap)
		{
			TrackPoint prevPoint = lap.Points[0];
			double accDist = 0;

			foreach (TrackPoint point in lap.Points)
			{
				accDist += ComputeDistance(point, prevPoint);
				point.Distance = (float)accDist;

				prevPoint = point;
			}
		}

		private static void PostProcessLapData(TrackLap lap)
		{
			if (lap.Points.Count < 2)
				return;

			if (lap.Points[lap.Points.Count - 1].Distance == 0.0)
			{
				PostProcessPointDistance(lap);
			}

			lap.StartTime = lap.Points[0].Time;
			lap.TotalDistance = lap.Points[lap.Points.Count - 1].Distance - lap.Points[0].Distance;
			lap.TotalTime = lap.Points[lap.Points.Count - 1].Time - lap.Points[0].Time;

			float accumAsc = 0;
			float accumDesc = 0;
			float prevAlti = lap.Points[0].Altitude;

			foreach (TrackPoint pt in lap.Points)
			{
				// compute accumulated ascent/descent
				float diff = pt.Altitude - prevAlti;
				prevAlti = pt.Altitude;

				if (diff > 0)
					accumAsc += diff;
				else
					accumDesc -= diff;			// diff is negative so (-diff) is positive.

				pt.AccAsc = accumAsc;
				pt.AccDesc = accumDesc;

				ComputeSpeed(lap, pt);
			}
		}

		private static void ComputeSpeed(TrackLap lap, TrackPoint pt)
		{
			const int range = 2;

			int lapIndex = pt.Index - lap.FirstPointIndex;

			// compute speed from [-range..range] around index
			int lowBound = Math.Max(lapIndex - range, 0);
			int highBound = Math.Min(lapIndex + range, lap.Points.Count - 1);

			float deltaDistance = lap.Points[highBound].Distance - lap.Points[lowBound].Distance;
			TimeSpan deltaTime = lap.Points[highBound].Time - lap.Points[lowBound].Time;

			if (deltaTime != TimeSpan.Zero)
			{
				pt.Speed = 0.001f * deltaDistance / (float)deltaTime.TotalHours;
			}
		}

		private static double toRadian(double val)
		{
			return (Math.PI / 180) * val;
		}
	}
}

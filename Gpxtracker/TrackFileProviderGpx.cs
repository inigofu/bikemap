//------------------------------------------------------------------------------
// <copyright file="TrackFileProviderGpx.cs" company="">
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
// <summary>Contains information about the TrackFileProviderGpx class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml.Serialization;

	/// <summary>
	/// Represents a track file provider for GPX files.
	/// </summary>
	public partial class TrackFileProviderGpx : TrackFileProvider
	{
		private XmlSerializer _xmlSerializer = new XmlSerializer(typeof(GpxTree));

		/// <summary>
		/// Determines whether the specified file can be read.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		/// 	<c>true</c> if the specified file can be read; otherwise, <c>false</c>.
		/// </returns>
		public bool CanReadFile(string file)
		{
			return file.EndsWith(".gpx", StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Reads the file and return the contained activities.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public ICollection<TrackActivity> ReadFile(string fileName)
		{
			List<TrackActivity> activities = new List<TrackActivity>();

			GpxTree tree = ParseFile(fileName);

			if (tree == null)
				return activities;

			if (tree.Tracks != null)
			{
				foreach (GpxTrack gpxTrack in tree.Tracks)
				{
					TrackActivity track = ConvertTrack(gpxTrack);

					track.FileName = fileName;

					activities.Add(track);
				}
			}

			if (tree.Waypoints != null)
			{
				TrackActivity activity = new TrackActivity();
				activity.FileName = fileName;
				activity.Laps = new List<TrackLap>();
				activity.Waypoints = new List<TrackWaypoint>();

				foreach (GpxWaypoint gpxWaypoint in tree.Waypoints)
				{
					activity.Waypoints.Add(ConvertWaypoint(gpxWaypoint));
				}

				activities.Add(activity);
			}

			return activities;
		}

		private GpxTree ParseFile(string file)
		{
			GpxTree tree = null;

			try
			{
				using (StreamReader reader = new StreamReader(file))
				{
					tree = _xmlSerializer.Deserialize(reader) as GpxTree;
				}
			}

			catch (IOException)
			{
				// Error reading file
			}

			catch (InvalidOperationException)
			{
				// Serialization error
			}

			return tree;
		}

		private static TrackWaypoint ConvertWaypoint(GpxWaypoint gpxWaypoint)
		{
			TrackWaypoint waypoint = new TrackWaypoint();

			waypoint.Altitude = (float)gpxWaypoint.Elevation;
			waypoint.Latitude = (float)gpxWaypoint.Lat;
			waypoint.Longitude = (float)gpxWaypoint.Lon;
			waypoint.Name = gpxWaypoint.Name;
			waypoint.Description = gpxWaypoint.Description;

			return waypoint;
		}

		private static TrackActivity ConvertTrack(GpxTrack gpxTrack)
		{
			TrackActivity track = new TrackActivity();
			track.Laps = new List<TrackLap>();
			track.Waypoints = new List<TrackWaypoint>();

			track.Name = gpxTrack.Name;
			track.Description = gpxTrack.Description;

			foreach (GpxTrackSegment segment in gpxTrack.TrackSegments)
			{
				TrackLap lap = new TrackLap();
				lap.Points = new List<TrackPoint>();

				foreach (GpxTrackPoint gpxPt in segment.TrackPoints)
				{
					TrackPoint pt = new TrackPoint();

					pt.Latitude = (float)gpxPt.Lat;
					pt.Longitude = (float)gpxPt.Lon;
					pt.Altitude = (float)gpxPt.Elevation;
					pt.Time = gpxPt.Time;

					lap.Points.Add(pt);
				}

				track.Laps.Add(lap);
			}

			return track;
		}
	}
}

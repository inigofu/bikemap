//------------------------------------------------------------------------------
// <copyright file="TrackFileProviderTcx.cs" company="">
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
// <summary>Contains information about the TrackFileProviderTcx class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Xml.Serialization;

	/// <summary>
	/// Represents a track file provider for TCX files.
	/// </summary>
	public partial class TrackFileProviderTcx : TrackFileProvider
	{
		private XmlSerializer _xmlSerializer = new XmlSerializer(typeof(TcxTree));

		/// <summary>
		/// Determines whether the specified file can be read.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		/// 	<c>true</c> if the specified file can be read; otherwise, <c>false</c>.
		/// </returns>
		public bool CanReadFile(string file)
		{
			return file.EndsWith(".tcx", StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Reads the file and return the contained activities.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		public ICollection<TrackActivity> ReadFile(string fileName)
		{
			List<TrackActivity>	activities = new List<TrackActivity>();

			TcxTree tree = ParseFile(fileName);

			if (tree == null)
				return activities;

			if (tree.CourseList != null && tree.CourseList.Courses != null)
			{
				foreach (TcxCourse tcxCourse in tree.CourseList.Courses)
				{
					TrackActivity track = ConvertCourse(tcxCourse);

					track.FileName = fileName;

					activities.Add(track);
				}
			}

			if (tree.ActivityList != null && tree.ActivityList.Activities != null)
			{
				foreach (TcxActivity tcxActivity in tree.ActivityList.Activities)
				{
					TrackActivity track = ConvertActivity(tcxActivity);

					track.FileName = fileName;

					activities.Add(track);
				}
			}

			return activities;
		}

		private TcxTree ParseFile(string file)
		{
			TcxTree tree = null;

			try
			{
				using (StreamReader reader = new StreamReader(file))
				{
					tree = _xmlSerializer.Deserialize(reader) as TcxTree;
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
	
		private static TrackActivity ConvertCourse(TcxCourse tcxCourse)
		{
			TrackActivity track = new TrackActivity();
			track.Laps = new List<TrackLap>();
			track.Waypoints = new List<TrackWaypoint>();

			track.Name = tcxCourse.Name;

			TrackLap lap = new TrackLap();
			lap.Points = new List<TrackPoint>();

			if (tcxCourse.CoursePoints != null)
			{
				foreach (TcxCoursePoint tcxCoursePoint in tcxCourse.CoursePoints)
				{
					track.Waypoints.Add(ConvertWaypoint(tcxCoursePoint));
				}
			}

			if (tcxCourse.TrackpointList != null && tcxCourse.TrackpointList.Trackpoints != null)
			{
				foreach (TcxTrackPoint tcxPt in tcxCourse.TrackpointList.Trackpoints)
				{
					TrackPoint pt = ConvertTrackPoint(tcxPt);

					lap.Points.Add(pt);
				}
			}

			track.Laps.Add(lap);

			return track;
		}

		private static TrackWaypoint ConvertWaypoint(TcxCoursePoint tcxCoursePoint)
		{
			TrackWaypoint waypoint = new TrackWaypoint();

			waypoint.Name = tcxCoursePoint.Name;
			waypoint.Altitude = (float)tcxCoursePoint.AltitudeMeters;
			waypoint.Latitude = (float)tcxCoursePoint.Position.LatitudeDegrees;
			waypoint.Longitude = (float)tcxCoursePoint.Position.LongitudeDegrees;
			waypoint.Time = tcxCoursePoint.Time;

			return waypoint;
		}

		private static TrackActivity ConvertActivity(TcxActivity tcxActivity)
		{
			TrackActivity track = new TrackActivity();
			track.Laps = new List<TrackLap>();
			track.Waypoints = new List<TrackWaypoint>();

			foreach (TcxActivityLap tcxLap in tcxActivity.Laps)
			{
				TrackLap lap = new TrackLap();
				lap.Points = new List<TrackPoint>();

				if (tcxLap.Tracks != null)
				{
					foreach (TcxTrackPointList tcxTrack in tcxLap.Tracks)
					{
						if (tcxTrack.Trackpoints != null)
						{
							foreach (TcxTrackPoint tcxPoint in tcxTrack.Trackpoints)
							{
								TrackPoint pt = ConvertTrackPoint(tcxPoint);

								if (pt != null)
								{
									lap.Points.Add(pt);
								}
							}
						}
					}
				}

				track.Laps.Add(lap);
			}

			return track;
		}

		private static TrackPoint ConvertTrackPoint(TcxTrackPoint tcxPoint)
		{
			if (tcxPoint.Position == null)
				return null;

			TrackPoint pt = new TrackPoint();

			pt.Latitude = (float)tcxPoint.Position.LatitudeDegrees;
			pt.Longitude = (float)tcxPoint.Position.LongitudeDegrees;
			pt.Altitude = (float)tcxPoint.AltitudeMeters;
			pt.Distance = (float)tcxPoint.DistanceMeters;
			pt.Time = tcxPoint.Time;

			return pt;
		}
	}
}

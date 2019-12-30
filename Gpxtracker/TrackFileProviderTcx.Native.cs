//------------------------------------------------------------------------------
// <copyright file="TrackFileProviderTcx.Native.cs" company="">
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

using System;
using System.Xml;
using System.Xml.Serialization;

namespace GpxTracker
{
	public partial class TrackFileProviderTcx : TrackFileProvider
	{
	//                       --- TcxTree ---
	//                     /                 \
	//                    /                   \
	//            TcxActivityList         TcxCourseList
	//                  |                      |
	//            TcxActivity[]            TcxCourse[] ----------+----------------
	//                  |                      |                 |                 \
	//           TcxActivityLap[]      TcxTrackPointList   TcxCourseLap[]    TcxCoursePoint[]
	//                  |                      |
	//          TcxTrackPointList      TcxTrackPoint[]
	//                  |
	//           TcxTrackPoint[]
		
		[XmlRoot("TrainingCenterDatabase", Namespace = "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2")]
		public class TcxTree
		{
			[XmlElement("Activities")]
			public TcxActivityList ActivityList { get; set; }

			[XmlElement("Courses")]
			public TcxCourseList CourseList { get; set; }
		}

		public class TcxActivityList
		{
			[XmlElement("Activity")]
			public TcxActivity[] Activities { get; set; }
		}

		public class TcxCourseList
		{
			[XmlElement("Course")]
			public TcxCourse[] Courses { get; set; }
		}

		public class TcxActivity
		{
			[XmlElement("Id")]
			public DateTime Id { get; set; }

			[XmlElement("Lap")]
			public TcxActivityLap[] Laps { get; set; }

			[XmlElement("Notes")]
			public string Notes { get; set; }
		}

		public class TcxCourse
		{
			[XmlElement("Name")]
			public string Name { get; set; }

			[XmlElement("Lap")]
			public TcxCourseLap[] Laps { get; set; }

			[XmlElement("Track")]
			public TcxTrackPointList TrackpointList { get; set; }

			[XmlElement("Notes")]
			public string Notes { get; set; }

			[XmlElement("CoursePoints")]
			public TcxCoursePoint[] CoursePoints { get; set; }
		}

		public class TcxCourseLap
		{
			[XmlElement("TotalTimeSeconds")]
			public double TotalTimeSeconds { get; set; }

			[XmlElement("DistanceMeters")]
			public double DistanceMeters { get; set; }

			[XmlElement("BeginPosition")]
			public TcxPosition BeginPosition { get; set; }

			[XmlElement("BeginAltitudeMeters")]
			public double BeginAltitudeMeters { get; set; }

			[XmlElement("BeginAltitudeMetersSpecified")]
			public bool BeginAltitudeMetersSpecified { get; set; }

			[XmlElement("EndPosition")]
			public TcxPosition EndPosition { get; set; }

			[XmlElement("EndAltitudeMeters")]
			public double EndAltitudeMeters { get; set; }

			[XmlElement("EndAltitudeMetersSpecified")]
			public bool EndAltitudeMetersSpecified { get; set; }

			[XmlElement("Cadence")]
			public byte Cadence { get; set; }

			[XmlElement("CadenceSpecified")]
			public bool CadenceSpecified { get; set; }
		}

		public class TcxCoursePoint
		{
			[XmlElement("Name")]
			public string Name { get; set; }

			[XmlElement("Time")]
			public DateTime Time { get; set; }

			[XmlElement("Position")]
			public TcxPosition Position { get; set; }

			[XmlElement("AltitudeMeters")]
			public double AltitudeMeters { get; set; }

			[XmlElement("AltitudeMetersSpecified")]
			public bool AltitudeMetersSpecified { get; set; }

			[XmlElement("Notes")]
			public string Notes { get; set; }
		}

		public class TcxActivityLap
		{
			[XmlElement("TotalTimeSeconds")]
			public double TotalTimeSeconds { get; set; }

			[XmlElement("DistanceMeters")]
			public double DistanceMeters { get; set; }

			[XmlElement("MaximumSpeed")]
			public double MaximumSpeed { get; set; }

			[XmlElement("MaximumSpeedSpecified")]
			public bool MaximumSpeedSpecified { get; set; }

			[XmlElement("Calories")]
			public int Calories { get; set; }

			[XmlElement("Cadence")]
			public byte Cadence { get; set; }

			[XmlElement("CadenceSpecified")]
			public bool CadenceSpecified { get; set; }

			[XmlElement("Track")]
			public TcxTrackPointList[] Tracks { get; set; }

			[XmlElement("Notes")]
			public string Notes { get; set; }

			[XmlAttribute("StartTime")]
			public DateTime StartTime { get; set; }
		}

		public class TcxTrackPointList
		{
			[XmlElement("Trackpoint")]
			public TcxTrackPoint[] Trackpoints { get; set; }
		}

		public class TcxTrackPoint
		{
			[XmlElement("Time")]
			public DateTime Time { get; set; }

			[XmlElement("Position")]
			public TcxPosition Position { get; set; }

			[XmlElement("AltitudeMeters")]
			public double AltitudeMeters { get; set; }

			[XmlElement("AltitudeMetersSpecified")]
			public bool AltitudeMetersSpecified { get; set; }

			[XmlElement("DistanceMeters")]
			public double DistanceMeters { get; set; }

			[XmlElement("DistanceMetersSpecified")]
			public bool DistanceMetersSpecified { get; set; }
		}

		public class TcxPosition
		{
			[XmlElement("LatitudeDegrees")]
			public double LatitudeDegrees { get; set; }

			[XmlElement("LongitudeDegrees")]
			public double LongitudeDegrees { get; set; }
		}
	}
}

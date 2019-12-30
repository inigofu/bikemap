//------------------------------------------------------------------------------
// <copyright file="TrackFileProviderGpx.Native.cs" company="">
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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace GpxTracker
{
	public partial class TrackFileProviderGpx
	{
		// CA1034: Nested types should not be visible
		// --> XMLSerializer requires the type to be externally visible

		// CA1819: Properties should not return arrays
		// --> XMLSerializer requires arrays

		[XmlRoot("gpx", Namespace = "http://www.topografix.com/GPX/1/1")]
		[SuppressMessage("Microsoft.Design", "CA1034")]
		public class GpxTree
		{
			[XmlAttribute("version")]
			public string Version { get; set; }

			[XmlAttribute("creator")]
			public string Creator { get; set; }

			[XmlElement("wpt")]
			[SuppressMessage("Microsoft.Performance", "CA1819")]
			public GpxWaypoint[] Waypoints { get; set; }

			[XmlElement("trk")]
			[SuppressMessage("Microsoft.Performance", "CA1819")]
			public GpxTrack[] Tracks { get; set; }
		}

		[XmlRoot("wpt")]
		[SuppressMessage("Microsoft.Design", "CA1034")]
		public class GpxWaypoint
		{
			[XmlAttribute(DataType = "double", AttributeName = "lat")]
			public double Lat { get; set; }

			[XmlAttribute(DataType = "double", AttributeName = "lon")]
			public double Lon { get; set; }

			[XmlElement("ele", DataType = "double")]
			public double Elevation { get; set; }

			[XmlElement("name")]
			public string Name { get; set; }

			[XmlAttribute("cmt")]
			public string Comment { get; set; }

			[XmlElement("desc")]
			public string Description { get; set; }

			[XmlElement("time")]
			public DateTime Time { get; set; }
		}

		[XmlRoot("trk")]
		[SuppressMessage("Microsoft.Design", "CA1034")]
		public class GpxTrack
		{
			[XmlElement("name")]
			public string Name { get; set; }

			[XmlElement("cmt")]
			public string Comment { get; set; }

			[XmlElement("desc")]
			public string Description { get; set; }

			[XmlElement("trkseg")]
			[SuppressMessage("Microsoft.Performance", "CA1819")]
			public GpxTrackSegment[] TrackSegments { get; set; }
		}

		[XmlRoot("trkseg")]
		[SuppressMessage("Microsoft.Design", "CA1034")]
		public class GpxTrackSegment
		{
			[XmlElement("trkpt")]
			[SuppressMessage("Microsoft.Performance", "CA1819")]
			public GpxTrackPoint[] TrackPoints { get; set; }
		}

		[XmlRoot("trkpt")]
		[SuppressMessage("Microsoft.Design", "CA1034")]
		public class GpxTrackPoint
		{
			[XmlAttribute(DataType = "double", AttributeName = "lat")]
			public double Lat { get; set; }

			[XmlAttribute(DataType = "double", AttributeName = "lon")]
			public double Lon { get; set; }

			[XmlElement("ele")]
			public double Elevation { get; set; }

			[XmlElement("time")]
			public DateTime Time { get; set; }
		}
	
	}
}


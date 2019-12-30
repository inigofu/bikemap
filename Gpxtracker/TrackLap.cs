//------------------------------------------------------------------------------
// <copyright file="TrackLap.cs" company="">
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
// <date>24.10.2010</date>
// <summary>Contains information about the TrackLap class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Represents a lap of a track.
	/// </summary>
	public class TrackLap
	{
		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index { get; set; }

		/// <summary>
		/// Gets or sets the index of the first point.
		/// </summary>
		/// <value>The index of the first point.</value>
		public int FirstPointIndex { get; set; }

		/// <summary>
		/// Gets or sets the index of the last point.
		/// </summary>
		/// <value>The index of the last point.</value>
		public int LastPointIndex { get; set; }

		/// <summary>
		/// Gets or sets the points.
		/// </summary>
		/// <value>The points.</value>
		public List<TrackPoint> Points { get; set; }

		/// <summary>
		/// Gets or sets the start time.
		/// </summary>
		/// <value>The start time.</value>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// Gets or sets the total time.
		/// </summary>
		/// <value>The total time.</value>
		public TimeSpan TotalTime { get; set; }

		/// <summary>
		/// Gets or sets the total distance.
		/// </summary>
		/// <value>The total distance.</value>
		public double TotalDistance { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "TrackLap " + Index;
		}
	};
}

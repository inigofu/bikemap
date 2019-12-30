//------------------------------------------------------------------------------
// <copyright file="TrackPoint.cs" company="">
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
// <summary>Contains information about the TrackPoint class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Represents a point of a track.
	/// </summary>
	public class TrackPoint
	{
		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		/// <value>The index.</value>
		public int Index { get; set; }

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude.</value>
		public float Latitude { get; set; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude.</value>
		public float Longitude { get; set; }

		/// <summary>
		/// Gets or sets the altitude.
		/// </summary>
		/// <value>The altitude.</value>
		public float Altitude { get; set; }

		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>The distance.</value>
		public float Distance { get; set; }

		/// <summary>
		/// Gets or sets the accumulated ascent.
		/// </summary>
		/// <value>The accumulated ascent.</value>
		public float AccAsc { get; set; }

		/// <summary>
		/// Gets or sets the accumulated descent.
		/// </summary>
		/// <value>The accumulated descent.</value>
		public float AccDesc { get; set; }

		/// <summary>
		/// Gets or sets the speed.
		/// </summary>
		/// <value>The speed.</value>
		public float Speed { get; set; }

		/// <summary>
		/// Gets or sets the time.
		/// </summary>
		/// <value>The time.</value>
		public DateTime Time { get; set; }

		/// <summary>
		/// Gets or sets the time delta. That is the time since the start of the track.
		/// </summary>
		/// <value>The time delta.</value>
		public TimeSpan TimeDelta { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "TrackPoint " + Index;
		}
	};
}


//------------------------------------------------------------------------------
// <copyright file="UnitProviderBasic.cs" company="">
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
// <date>5.12.2010</date>
// <summary>Contains information about the abstract UnitProviderBasic class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Globalization;

	/// <summary>
	/// Provides basic functionality that most systems have in common.
	/// </summary>
	abstract class UnitProviderBasic : UnitProvider
	{
		/// <summary>
		/// Returns a string that represents memory space.
		/// </summary>
		/// <param name="size">The memory/disk space.</param>
		/// <returns>A string that represents memory space.</returns>
		public string DiskSpaceString(long size)
		{
			const long limit = 8192;			// 8 kB and 8 MB

			if (size > limit * 1024)
			{
				return String.Format(CultureInfo.CurrentCulture, "{0} MB", size / (1024 * 1024));
			}

			if (size > limit)
			{
				return String.Format(CultureInfo.CurrentCulture, "{0} kB", size / (1024));
			}

			return String.Format(CultureInfo.CurrentCulture, "{0} Bytes", size);
		}

		/// <summary>
		/// Returns a string that represents a time span.
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		/// <returns>A string that represents a time span</returns>
		public string TimeString(TimeSpan timeSpan)
		{
			if (timeSpan.TotalSeconds < 10.0)
			{
				return String.Format(CultureInfo.CurrentCulture, "{0:F2} sec.", timeSpan.TotalSeconds);
			}

			if (timeSpan.TotalSeconds < 100.0)
			{
				return String.Format(CultureInfo.CurrentCulture, "{0:F2} sec.", timeSpan.TotalSeconds);
			}

			if (timeSpan.TotalMinutes < 60.0)
			{
				return String.Format(CultureInfo.CurrentCulture, "{0}:{1:D2} min.", timeSpan.Minutes, timeSpan.Seconds);
			}

			return String.Format(CultureInfo.CurrentCulture, "{0}:{1:D2}:{2:D2} hrs.", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}

		/// <summary>
		/// Returns a string that represents speed.
		/// </summary>
		/// <param name="time">The time.</param>
		/// <param name="distInKm">The distance in km.</param>
		/// <returns>A string that represents speed</returns>
		public string SpeedString(TimeSpan time, double distInKm)
		{
			if (time.TotalSeconds > 10.0)
			{
				return SpeedString(distInKm * 0.001 / time.TotalHours);
			}
			else
			{
				return "-";
			}
		}

		public abstract string DistanceString(double dist);
		public abstract string SpeedString(double speed);
		public abstract string HeightString(double height);
		public abstract string HeightUnitString();
		public abstract string SpeedUnitString();
		public abstract string DistanceUnitString();
		public abstract double ConvertHeight(double height);
		public abstract double ConvertDistance(double dist);
		public abstract double ConvertSpeed(double speed);
	}
}

//------------------------------------------------------------------------------
// <copyright file="UnitProviderMetric.cs" company="">
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
// <summary>Contains information about the UnitProviderMetric class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System.Globalization;

	class UnitProviderMetric : UnitProviderBasic
	{
		/// <summary>
		/// Returns a string that represents a distance.
		/// </summary>
		/// <param name="dist">The distance in meters.</param>
		/// <returns>A string that represents a distance.</returns>
		public override string DistanceString(double dist)
		{
			if (dist < 1000.0)
			{
				return dist.ToString("F0", CultureInfo.CurrentCulture) + " m";
			}
			else if (dist < 10000.0)
			{
				return (dist * 0.001).ToString("F2", CultureInfo.CurrentCulture) + " km";
			}
			else
			{
				return (dist * 0.001).ToString("F1", CultureInfo.CurrentCulture) + " km";
			}
		}

		/// <summary>
		/// Returns a string that represents speed.
		/// </summary>
		/// <param name="speed">The speed.</param>
		/// <returns>A string that represents speed.</returns>
		public override string SpeedString(double speed)
		{
			return speed.ToString("F2", CultureInfo.CurrentCulture) + " km/h";
		}

		public override string HeightString(double height)
		{
			return height.ToString("F2", CultureInfo.CurrentCulture) + " m";
		}

		public override string HeightUnitString()
		{
			return "m";
		}

		public override string DistanceUnitString()
		{
			return "km";
		}

		public override string SpeedUnitString()
		{
			return "km/h";
		}

		public override double ConvertDistance(double dist)
		{
			return dist;
		}

		public override double ConvertHeight(double height)
		{
			return height;
		}

		public override double ConvertSpeed(double speed)
		{
			return speed;
		}
	}
}

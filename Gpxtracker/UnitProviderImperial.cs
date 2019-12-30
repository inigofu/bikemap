//------------------------------------------------------------------------------
// <copyright file="UnitProviderImperial.cs" company="">
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
// <summary>Contains information about the UnitProviderImperial class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System.Globalization;

	class UnitProviderImperial : UnitProviderBasic
	{
		/// <summary>
		/// Returns a string that represents a distance.
		/// </summary>
		/// <param name="dist">The distance in meters.</param>
		/// <returns>A string that represents a distance.</returns>
		public override string DistanceString(double dist)
		{
			// 1 mile is 5280 feet or 1609 meters
			if (dist < 2000.0)
			{
				dist = MetersToFeet(dist);
				return dist.ToString("F0", CultureInfo.CurrentCulture) + " ft";
			}
			else if (dist < 16090.0)
			{
				dist = MetersToMiles(dist);
				return dist.ToString("F2", CultureInfo.CurrentCulture) + " miles";
			}
			else
			{
				dist = MetersToMiles(dist);
				return dist.ToString("F1", CultureInfo.CurrentCulture) + " miles";
			}
		}

		/// <summary>
		/// Returns a string that represents speed.
		/// </summary>
		/// <param name="speed">The speed.</param>
		/// <returns>A string that represents speed.</returns>
		public override string SpeedString(double speed)
		{
			speed = MetersToMiles(speed * 1000.0);
			return speed.ToString("F2", CultureInfo.CurrentCulture) + " miles/h";
		}

		public override string HeightString(double height)
		{
			height = MetersToFeet(height);
			return height.ToString("F2", CultureInfo.CurrentCulture) + " ft";
		}

		private static double MetersToFeet(double dist)
		{
			return dist * 3.280839895013123359580052;
		}

		private static double MetersToMiles(double dist)
		{
			return dist * 0.000621371192;
		}

		public override double ConvertHeight(double height)
		{
			return MetersToFeet(height);
		}

		public override double ConvertDistance(double dist)
		{
			return MetersToMiles(dist * 1000.0);
		}

		public override double ConvertSpeed(double speed)
		{
			return MetersToMiles(speed * 1000.0);
		}

		public override string HeightUnitString()
		{
			return "ft";
		}

		public override string DistanceUnitString()
		{
			return "miles";
		}

		public override string SpeedUnitString()
		{
			return "miles/h";
		}
	}
}

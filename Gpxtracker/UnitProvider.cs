//------------------------------------------------------------------------------
// <copyright file="UnitProvider.cs" company="">
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
// <summary>Contains information about the UnitProvider interface.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;

	/// <summary>
	/// Represents an interface for different unit systems.
	/// </summary>
	interface UnitProvider
	{
		string TimeString(TimeSpan timeSpan);
		string DiskSpaceString(long size);
		string SpeedString(TimeSpan time, double distInKm);
		
		string DistanceString(double dist);
		string SpeedString(double speed);
		string HeightString(double height);
		
		string HeightUnitString();
		string SpeedUnitString();
		string DistanceUnitString();

		double ConvertHeight(double height);
		double ConvertDistance(double dist);
		double ConvertSpeed(double speed);
	}
}

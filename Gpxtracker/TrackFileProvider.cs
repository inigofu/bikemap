//------------------------------------------------------------------------------
// <copyright file="TrackFileProvider.cs" company="">
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
// <summary>Contains information about the TrackFileProvider interface.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System.Collections.Generic;

	/// <summary>
	/// Represents a provider for track files.
	/// </summary>
	interface TrackFileProvider
	{
		/// <summary>
		/// Determines whether the specified file can be read.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns>
		/// 	<c>true</c> if the specified file can be read; otherwise, <c>false</c>.
		/// </returns>
		bool CanReadFile(string file);

		/// <summary>
		/// Reads the file and return the contained activities.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns></returns>
		ICollection<TrackActivity> ReadFile(string fileName);
	}
}

//------------------------------------------------------------------------------
// <copyright file="TrackFileInfo.cs" company="">
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
// <summary>Contains information about the TrackFileInfo class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	/// <summary>
	/// Represents information about a track file.
	/// </summary>
	public class TrackFileInfo
	{
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the local full name.
		/// </summary>
		/// <value>The local full name.</value>
		public string FullNameLocal { get; set; }

		/// <summary>
		/// Gets or sets the remote full name.
		/// </summary>
		/// <value>The remote full name.</value>
		public string FullNameRemote { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the file exists locally.
		/// </summary>
		/// <value><c>true</c> if the file exists locally; otherwise, <c>false</c>.</value>
		public bool ExistLocal { get; set; }

		/// <summary>
		/// Gets or sets the file size.
		/// </summary>
		/// <value>The file size.</value>
		public long Size { get; set; }
	}
}

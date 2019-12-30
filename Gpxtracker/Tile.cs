//------------------------------------------------------------------------------
// <copyright file="Tile.cs" company="">
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
// <summary>Contains information about the Tile class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	/// <summary>
	/// Represents an image tile.
	/// </summary>
	public class Tile
	{
		/// <summary>
		/// Gets or sets the x-coordinate.
		/// </summary>
		/// <value>The x-coordinate.</value>
		public int X { get; set; }

		/// <summary>
		/// Gets or sets the y-coordinate.
		/// </summary>
		/// <value>The y-coordinate.</value>
		public int Y { get; set; }

		/// <summary>
		/// Gets or sets the zoom factor.
		/// </summary>
		/// <value>The zoom factor.</value>
		public int Zoom	{ get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the tile is cached in memory.
		/// </summary>
		/// <value><c>true</c> if the tile is cached in memory is cached; otherwise, <c>false</c>.</value>
		public bool IsCached { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the tile is requested for loading.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the tile is requested; otherwise, <c>false</c>.
		/// </value>
		public bool IsRequested	{ get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the tile exists on disk.
		/// </summary>
		/// <value><c>true</c> if the tile exists on disk; otherwise, <c>false</c>.</value>
		public bool OnDisk { get; set; }
	}
}

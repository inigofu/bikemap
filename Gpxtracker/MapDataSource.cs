//------------------------------------------------------------------------------
// <copyright file="MapDataSource.cs" company="">
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
// <summary>Contains information about the MapDataSource class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Globalization;

	/// <summary>
	/// Represents a (web) source of map image data.
	/// </summary>
	[Serializable]
	public class MapDataSource
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MapDataSource"/> class.
		/// </summary>
		public MapDataSource()
		{
			// class must be public in oder to be serializable for XMLSerializer
			// TODO: cannot be serialized because it does not have a parameterless constructor
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MapDataSource"/> class.
		/// </summary>
		/// <param name="name">The name of the source.</param>
		/// <param name="format">The URI format.</param>
		/// <param name="localPath">The local path.</param>
		/// <param name="minZoom">The min zoom factor.</param>
		/// <param name="maxZoom">The max zoom factor.</param>
		public MapDataSource(string name, string format, string localPath, int minZoom, int maxZoom)
		{
			Name = name;
			Format = format;
			LocalPath = localPath;
			MinZoom = minZoom;
			MaxZoom = maxZoom;
		}

		/// <summary>
		/// Gets or sets the URI format.
		/// </summary>
		/// <value>The URI format.</value>
		public string Format { get; set; }

		/// <summary>
		/// Gets or sets the name of the source.
		/// </summary>
		/// <value>The name of the source.</value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the local path.
		/// </summary>
		/// <value>The local path.</value>
		public string LocalPath { get; set; }

		/// <summary>
		/// Gets or sets the min zoom factor.
		/// </summary>
		/// <value>The min zoom factor.</value>
		public int MinZoom { get; set; }

		/// <summary>
		/// Gets or sets the max zoom factor.
		/// </summary>
		/// <value>The max zoom factor.</value>
		public int MaxZoom { get; set; }

		/// <summary>
		/// Builds a URI from the tile infi.
		/// </summary>
		/// <param name="tileX">The x-coordinate of the tile.</param>
		/// <param name="tileY">The y-coordinate of the tile.</param>
		/// <param name="tileZoom">The zoom of the tile.</param>
		/// <returns></returns>
		public Uri GetUri(int tileX, int tileY, int tileZoom)
		{
			return new Uri(String.Format(CultureInfo.InvariantCulture, Format, tileZoom, tileX, tileY));
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}

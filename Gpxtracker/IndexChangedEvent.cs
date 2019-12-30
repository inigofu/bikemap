//------------------------------------------------------------------------------
// <copyright file="IndexChangedEvent.cs" company="">
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
// <summary>Contains information about the ZoomChangedEventArgs class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;

	/// <summary>
	/// Represents an selected index / control index changed event.
	/// </summary>
	public class IndexChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IndexChangedEventArgs"/> class.
		/// </summary>
		/// <param name="selIndex">The selected index.</param>
		/// <param name="ctrlIndex">The control index.</param>
		public IndexChangedEventArgs(int selIndex, int ctrlIndex)
		{
			ControlIndex = ctrlIndex;
			SelectedIndex = selIndex;
		}

		/// <summary>
		/// Gets the index of the selected.
		/// </summary>
		/// <value>The selected index.</value>
		public int SelectedIndex { get; private set; }

		/// <summary>
		/// Gets the index of the control.
		/// </summary>
		/// <value>The control index.</value>
		public int ControlIndex { get; private set; }
	}

	/// <summary>
	/// Represents a zoom changed event.
	/// </summary>
	public class ZoomChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ZoomChangedEventArgs"/> class.
		/// </summary>
		/// <param name="zoom">The zoom factor.</param>
		public ZoomChangedEventArgs(int zoom)
		{
			Zoom = zoom;
		}

		/// <summary>
		/// Gets the zoom.
		/// </summary>
		/// <value>The zoom factor.</value>
		public int Zoom { get; private set; }
	}
}

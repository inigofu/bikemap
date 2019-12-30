//------------------------------------------------------------------------------
// <copyright file="ComboBoxItem.cs" company="">
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
// <summary>Contains information about the ComboBoxItem class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	/// <summary>
	/// Combines a value that should be displayed with the actual value.
	/// </summary>
	/// <typeparam name="DisplayType">The type of the display value type.</typeparam>
	/// <typeparam name="ValueType">The type of the actual value type.</typeparam>
	public class ComboBoxItem<DisplayType, ValueType>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ComboBoxItem&lt;DisplayType, ValueType&gt;"/> class.
		/// </summary>
		/// <param name="display">The display value.</param>
		/// <param name="value">The actual value.</param>
		public ComboBoxItem(DisplayType display, ValueType value)
		{
			Display = display;
			Value = value;
		}

		/// <summary>
		/// Gets the display value.
		/// </summary>
		/// <value>The display value.</value>
		public DisplayType Display { get; private set; }

		/// <summary>
		/// Gets the actual value.
		/// </summary>
		/// <value>The actual value.</value>
		public ValueType Value { get; private set; }
	}
}

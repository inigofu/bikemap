//------------------------------------------------------------------------------
// <copyright file="SortableBindingList.cs" company="">
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
// <summary>Contains information about the SortableBindingList class.</summary>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GpxTracker
{
	/// <summary>
	/// Provides a generic collection that supports data binding of sortable data.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	public class SortableBindingList<T> : BindingList<T>
	{
		private bool _isSorted;

		/// <summary>
		/// Initializes a new instance of the <see cref="SortableBindingList&lt;T&gt;"/> class.
		/// </summary>
		public SortableBindingList() : base() 
		{ 
		}

		/// <summary>
		/// Initializes a new instance of the SortableBindingList class.
		/// </summary>
		/// <param name="list">An <see cref="T:System.Collections.Generic.IList`1"/> of items to 
		/// be contained in the <see cref="T:System.ComponentModel.BindingList`1"/>.</param>
		public SortableBindingList(IList<T> list) : base(list) 
		{ 
		}

		/// <summary>
		/// Gets a value indicating whether the list supports sorting.
		/// </summary>
		/// <value>A value indicating whether the list supports sorting.</value>
		/// <returns>true if the list supports sorting; otherwise, false. The default is false.</returns>
		protected override bool SupportsSortingCore
		{
			get { return true; }
		}

		/// <summary>
		/// Gets a value indicating whether the list is sorted.
		/// </summary>
		/// <value>true if the list is sorted; otherwise, false. The default is false.</value>
		protected override bool IsSortedCore
		{
			get { return _isSorted; }
		}

		/// <summary>
		/// Removes any sort applied with <see cref="M:System.ComponentModel.BindingList`1.ApplySortCore(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)"/> 
		/// if sorting is implemented in a derived class; otherwise, raises <see cref="T:System.NotSupportedException"/>.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">Method is not overridden in a derived class. </exception>
		protected override void RemoveSortCore()
		{
			_isSorted = false;
		}

		/// <summary>
		/// Sorts the items if overridden in a derived class; otherwise, throws a System.NotSupportedException.
		/// </summary>
		/// <param name="property">A System.ComponentModel.PropertyDescriptor that specifies the property to sort on.</param>
		/// <param name="direction">One of the System.ComponentModel.ListSortDirection values.</param>
		protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
		{
			List<T> itemsList = Items as List<T>;

			if (itemsList == null)
				return;

			if (property.PropertyType.GetInterface("IComparable") != null)
			{
				itemsList.Sort(new Comparison<T>(delegate(T x, T y)
				{
					// Compare x to y if x is not null. If x is, but y isn't, we compare y
					// to x and reverse the result. If both are null, they're equal.
					if (property.GetValue(x) != null)
						return ((IComparable)property.GetValue(x)).CompareTo(property.GetValue(y)) * (direction == ListSortDirection.Descending ? -1 : 1);
					else if (property.GetValue(y) != null)
						return ((IComparable)property.GetValue(y)).CompareTo(property.GetValue(x)) * (direction == ListSortDirection.Descending ? 1 : -1);
					else
						return 0;
				}));
			}

			_isSorted = true;
		}
	}
}

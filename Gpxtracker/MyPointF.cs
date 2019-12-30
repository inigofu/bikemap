//------------------------------------------------------------------------------
// <copyright file="MyPointF.cs" company="">
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
// <summary>Contains information about the MyPointF struct.</summary>
//------------------------------------------------------------------------------

using System;
using System.Globalization;

namespace GpxTracker
{
	struct MyPointF
	{
		private float _x;
		private float _y;

		public static readonly MyPointF Zero = new MyPointF(0.0f, 0.0f);

		public MyPointF(float x, float y)
		{
			_x = x;
			_y = y;
		}

		public float X { get { return _x; } }
		public float Y { get { return _y; } }

		public static MyPointF Plus(MyPointF pt)
		{
			return pt;
		}

		public static MyPointF Negate(MyPointF pt)
		{
			return -pt;
		}

		public static MyPointF Subtract(MyPointF pt)
		{
			return -pt;
		}

		public static MyPointF operator -(MyPointF pt)
		{
			return new MyPointF(-pt._x, -pt._y);
		}

		public static MyPointF Multiply(MyPointF pt, float fac)
		{
			return pt * fac;
		}

		public static MyPointF operator *(MyPointF pt1, float fac)
		{
			return new MyPointF(pt1._x * fac, pt1._y * fac);
		}

		public static MyPointF Multiply(float fac, MyPointF pt)
		{
			return fac * pt;
		}

		public static MyPointF operator *(float fac, MyPointF pt1)
		{
			return new MyPointF(pt1._x * fac, pt1._y * fac);
		}

		public static MyPointF Add(MyPointF pt1, MyPointF pt2)
		{
			return pt1 + pt2;
		}

		public static MyPointF operator +(MyPointF pt1, MyPointF pt2)
		{
			return new MyPointF(pt1._x + pt2._x, pt1._y + pt2._y);
		}

		public static MyPointF Subtract(MyPointF pt1, MyPointF pt2)
		{
			return pt1 - pt2;
		}

		public static MyPointF operator -(MyPointF pt1, MyPointF pt2)
		{
			return new MyPointF(pt1._x - pt2._x, pt1._y - pt2._y);
		}

		public static bool operator !=(MyPointF pt1, MyPointF pt2)
		{
			return (pt1._x != pt2._x) || (pt1._y != pt2._y);
		}

		public static bool operator ==(MyPointF pt1, MyPointF pt2)
		{
			return (pt1._x == pt2._x) && (pt1._y == pt2._y);
		}

		public static implicit operator System.Drawing.PointF(MyPointF pt) 
		{ 
			return new System.Drawing.PointF(pt._x, pt._y); 
		}

		public static System.Drawing.PointF ToPointF(MyPointF pt)
		{
			return (System.Drawing.PointF)pt;
		}

		public static explicit operator MyPointF(MyPoint pt)
		{
			return new MyPointF(pt.X, pt.Y);
		}

		public static MyPointF ToMyPointF(MyPoint pt)
		{
			return (MyPointF)pt;
		}

		public static implicit operator MyPointF(System.Drawing.PointF pt) 
		{ 
			return new MyPointF(pt.X, pt.Y); 
		}

		public static MyPointF ToMyPointF(System.Drawing.PointF pt)
		{
			return (MyPointF)pt;
		}

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
				return false;

			// If parameter cannot be cast to Point return false.
			if (obj.GetType() != typeof(MyPointF))
				return false;

			MyPointF p = (MyPointF)obj;

			// Return true if the fields match:
			return (_x == p._x) && (_y == p._y);
		}

		public override int GetHashCode() 
		{ 
			return X.GetHashCode() ^ Y.GetHashCode(); 
		}

		public float GetLength()
		{
			return (float)Math.Sqrt(GetLengthSquare());
		}

		public float GetLengthSquare()
		{
			return _x * _x + _y * _y;
		}

		public MyPointF Normalize()
		{
			return this * (1.0f / GetLength());
		}

		public override string ToString()
		{
			return String.Format(CultureInfo.InvariantCulture, "{0:F2} / {1:F2}", X, Y);
		}
	}
}

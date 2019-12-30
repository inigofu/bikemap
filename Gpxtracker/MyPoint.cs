//------------------------------------------------------------------------------
// <copyright file="MyPoint.cs" company="">
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
// <summary>Contains information about the MyPoint struct.</summary>
//------------------------------------------------------------------------------

using System;
using System.Globalization;

namespace GpxTracker
{
	struct MyPoint
	{
		public static readonly MyPoint Zero = new MyPoint(0, 0);

		public MyPoint(int x, int y) : this()
		{
			X = x;
			Y = y;
		}

		public int X { get; private set; }
		public int Y { get; private set; }

		public static MyPoint Plus(MyPoint pt)
		{
			return pt;
		}

		public static MyPoint Negate(MyPoint pt)
		{
			return -pt;
		}

		public static MyPoint operator -(MyPoint pt)
		{
			return new MyPoint(-pt.X, -pt.Y);
		}

		public static MyPoint Multiply(MyPoint pt1, int fac)
		{
			return pt1 * fac;
		}

		public static MyPoint operator *(MyPoint pt1, int fac)
		{
			return new MyPoint(pt1.X * fac, pt1.Y * fac);
		}

		public static MyPoint Multiply(int fac, MyPoint pt1)
		{
			return fac * pt1;
		}

		public static MyPoint operator *(int fac, MyPoint pt1)
		{
			return new MyPoint(pt1.X * fac, pt1.Y * fac);
		}

		public static MyPoint Add(MyPoint pt1, MyPoint pt2)
		{
			return pt1 + pt2;
		}

		public static MyPoint operator +(MyPoint pt1, MyPoint pt2)
		{
			return new MyPoint(pt1.X + pt2.X, pt1.Y + pt2.Y);
		}

		public static MyPoint Subtract(MyPoint pt1, MyPoint pt2)
		{
			return pt1 - pt2;
		}

		public static MyPoint operator -(MyPoint pt1, MyPoint pt2)
		{
			return new MyPoint(pt1.X - pt2.X, pt1.Y - pt2.Y);
		}

		public static bool operator !=(MyPoint pt1, MyPoint pt2)
		{
			return (pt1.X != pt2.X) || (pt1.Y != pt2.Y);
		}

		public static bool operator ==(MyPoint pt1, MyPoint pt2)
		{
			return (pt1.X == pt2.X) && (pt1.Y == pt2.Y);
		}

		public static implicit operator System.Drawing.PointF(MyPoint pt) 
		{ 
			return new System.Drawing.PointF(pt.X, pt.Y); 
		}

		public static System.Drawing.PointF ToPointF(MyPoint pt)
		{
			return (System.Drawing.PointF)pt;
		}

		public static implicit operator MyPoint(System.Drawing.Point pt) 
		{ 
			return new MyPoint(pt.X, pt.Y); 
		}

		public static explicit operator MyPoint(MyPointF pt)
		{
			return new MyPoint((int)pt.X, (int)pt.Y);
		}

		public static MyPoint ToMyPoint(MyPointF pt)
		{
			return (MyPoint)pt;
		}

		public static MyPoint ToMyPoint(System.Drawing.Point pt)
		{
			return (MyPoint)pt;
		}

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
				return false;

			// If parameter cannot be cast to Point return false.
			if (obj.GetType() != typeof(MyPoint))
				return false;

			MyPoint p = (MyPoint)obj;

			// Return true if the fields match:
			return (X == p.X) && (Y == p.Y); 
		}

		public override int GetHashCode() 
		{ 
			return (X << 16) | Y; 
		}

		public double GetLength()
		{
			return Math.Sqrt(GetLengthSquare());
		}

		public int GetLengthSquare()
		{
			return X * X + Y * Y;
		}

		public override string ToString()
		{
			return String.Format(CultureInfo.InvariantCulture, "{0} / {1}", X, Y);
		}
	}
}

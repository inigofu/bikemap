//------------------------------------------------------------------------------
// <copyright file="MapViewPart.cs" company="">
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
// <date>07.11.2010</date>
// <summary>Contains information about the MapViewPart class.</summary>
//------------------------------------------------------------------------------


namespace GpxTracker
{
	using System;

	class MapViewPart
	{
		protected MyPoint WorldToScreen(MyPointF pt, int zoom)
		{
			int x = (int)(256.0 * ((pt.X + 180.0) / 360.0 * (1 << zoom)));
			int y = (int)(256.0 * ((1.0 - Math.Log(Math.Tan(pt.Y * Math.PI / 180.0) +
				1.0 / Math.Cos(pt.Y * Math.PI / 180.0)) / Math.PI) / 2.0 * (1 << zoom)));

			return new MyPoint(x, y);
		}

		protected MyPointF ScreenToWorld(MyPoint pt, int zoom)
		{
			double n = Math.PI - ((2.0 * Math.PI * pt.Y / 256.0f) / Math.Pow(2.0, zoom));

			float x = (float)(((pt.X / 256.0f) / Math.Pow(2.0, zoom) * 360.0) - 180.0);
			float y = (float)(180.0 / Math.PI * Math.Atan(0.5 * (Math.Exp(n) - Math.Exp(-n))));

			return new MyPointF(x, y);
		}
	}
}

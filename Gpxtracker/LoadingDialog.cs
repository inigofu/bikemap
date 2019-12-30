//------------------------------------------------------------------------------
// <copyright file="LoadingDialog.cs" company="">
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
// <summary>Contains information about the LoadingDialog class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System.Windows.Forms;
	
	/// <summary>
	/// Represents a loading dialog with a progress bar.
	/// </summary>
	public partial class LoadingDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LoadingDialog"/> class.
		/// </summary>
		public LoadingDialog()
		{
			InitializeComponent();
		}
	}
}
//------------------------------------------------------------------------------
// <copyright file="FileSystem.cs" company="">
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
// <summary>Contains information about the FileSystem class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.IO;
	using System.Reflection;
	using System.Windows.Forms;
	using GpxTracker.Properties;

	/// <summary>
	/// The file system of the application.
	/// </summary>
	public static class FileSystem
	{
		private static string _appDir;

		/// <summary>
		/// Initializes static members of the <see cref="FileSystem"/> class.
		/// </summary>
		static FileSystem()
		{
			string path = Properties.Settings.Default.LocalRootPath;

			if (String.IsNullOrEmpty(path))
			{
				string root = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string sub = Application.ProductName;

				LocalRootFolder = Path.Combine(root, sub);
			}
			else
			{
				LocalRootFolder = path;
			}
		}

		/// <summary>
		/// Gets or sets the local root folder.
		/// </summary>
		/// <value>The local root folder.</value>
		public static string LocalRootFolder
		{
			get
			{
				return _appDir;
			}

			set
			{
				_appDir = value;

				Settings.Default.LocalRootPath = value;

				LocalTrackFolder = Path.Combine(value, "tracks");
				LocalMapsFolder = Path.Combine(value, "maps");
			}
		}

		/// <summary>
		/// Gets the local track folder.
		/// </summary>
		/// <value>The local track folder.</value>
		public static string LocalTrackFolder { get; private set; }

		/// <summary>
		/// Gets the local maps folder.
		/// </summary>
		/// <value>The local maps folder.</value>
		public static string LocalMapsFolder { get; private set; }

		/// <summary>
		/// Gets the path of the assembly.
		/// </summary>
		/// <returns>The path of the assembly.</returns>
		public static string GetAssemblyPath()
		{
			string path = Assembly.GetExecutingAssembly().GetName().CodeBase;
			path = Path.GetDirectoryName(path);

			if (path.StartsWith("file:\\", StringComparison.OrdinalIgnoreCase))
			{
				path = _appDir.Substring(6);
			}

			return path;
		}

		/// <summary>
		/// Gets the absolute path.
		/// </summary>
		/// <param name="path">The path to use.</param>
		/// <returns>The absolute path.</returns>
		public static string GetAbsolutePath(string path)
		{
			if (Path.IsPathRooted(path))
				return path;

			return Path.Combine(_appDir, path);
		}
	}
}

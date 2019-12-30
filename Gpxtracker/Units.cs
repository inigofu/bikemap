//------------------------------------------------------------------------------
// <copyright file="Units.cs" company="">
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
// <date>5.12.2010</date>
// <summary>Contains information about the Units class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.Globalization;
	using System.ComponentModel;
	using GpxTracker.Properties;

	/// <summary>
	/// Provides access to a UnitProver interface.
	/// </summary>
	static class Units
	{
		public static UnitProvider Provider { get; private set; }

		static Units()
		{
			Settings.Default.SettingsLoaded += new System.Configuration.SettingsLoadedEventHandler(SettingsLoaded);
			Settings.Default.SettingsSaving += new System.Configuration.SettingsSavingEventHandler(SettingsSaving);

			UpdateFromSettings();
		}

		private static void UpdateFromSettings()
		{
			switch (Settings.Default.System)
			{
				case 0:
					Provider = new UnitProviderMetric();
					break;

				case 1:
					Provider = new UnitProviderImperial();
					break;

				default:
					throw new InvalidOperationException();
			}
		}

		private static void SettingsLoaded(object sender, System.Configuration.SettingsLoadedEventArgs e)
		{
			UpdateFromSettings();
		}

		private static void SettingsSaving(object sender, CancelEventArgs e)
		{
			UpdateFromSettings();
		}
	}
}

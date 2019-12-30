//------------------------------------------------------------------------------
// <copyright file="SettingsExt.cs" company="">
// Copyright (c) 2009 GPS Track Viewer development team
// http://trackviewer.codeplex.com/
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
// <summary>Contains information about the Settings class.</summary>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace GpxTracker.Properties
{
    partial class Settings
    {
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
		<ArrayOfMapDataSource xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
			xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
			<MapDataSource>
				<Format>http://tile.openstreetmap.org/{0}/{1}/{2}.png</Format>
				<Name>Mapnik</Name>
				<LocalPath>mapnik</LocalPath>
				<MinZoom>0</MinZoom>
				<MaxZoom>18</MaxZoom>
			</MapDataSource>
			<MapDataSource>
				<Format>http://tah.openstreetmap.org/Tiles/tile/{0}/{1}/{2}.png</Format>
				<Name>Unknown 1</Name>
				<LocalPath>unknown1</LocalPath>
				<MinZoom>0</MinZoom>
				<MaxZoom>18</MaxZoom>
			</MapDataSource>
			<MapDataSource>
				<Format>http://andy.sandbox.cloudmade.com/tiles/cycle/{0}/{1}/{2}.png</Format>
				<Name>Unknown 2</Name>
				<LocalPath>unknown2</LocalPath>
				<MinZoom>0</MinZoom>
				<MaxZoom>18</MaxZoom>
			</MapDataSource>
		</ArrayOfMapDataSource>")]

        public List<MapDataSource> MapDataSourceList
        {
            get { return ((List<MapDataSource>)(this["MapDataSourceList"])); }
            set { this["MapDataSourceList"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("0")]
        public int MapDataSourceIndex
        {
            get { return ((int)(this["MapDataSourceIndex"])); }
            set { this["MapDataSourceIndex"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValue("10000")]          // measured in days
        public TimeSpan MapDataMaxAge
        {
            get { return ((TimeSpan)(this["MapDataMaxAge"])); }
            set { this["MapDataMaxAge"] = value; }
        }
    }

}

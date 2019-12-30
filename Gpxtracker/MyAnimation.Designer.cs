//------------------------------------------------------------------------------
// <copyright file="MyAnimation.Designer.cs" company="">
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
// <summary>Contains information about the MyAnimation class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Design;
	using System.Windows.Forms;
	using System.Windows.Forms.Design;
	
	/// <summary>
	/// Represents a <see cref="ControlDesigner"/> for <see cref="MyAnimation"/>
	/// </summary>
	internal class MyAnimationControlDesigner : ControlDesigner
    {
		public override SelectionRules SelectionRules { get { return SelectionRules.Moveable; } }
	}

	[DesignerAttribute(typeof(MyAnimationControlDesigner))]
	partial class MyAnimation
	{
		/// <summary>
		/// Represents a <see cref="FileNameEditor"/> for AVI files.
		/// </summary>
		private class FileNameEditorAVI : FileNameEditor
		{
			/// <summary>
			/// Initializes the open file dialog when it is created.
			/// </summary>
			/// <param name="openFileDialog">The <see cref="T:System.Windows.Forms.OpenFileDialog"/> 
			/// to use to select a file name.</param>
			protected override void InitializeDialog(OpenFileDialog openFileDialog)
			{
				openFileDialog.Filter = "AVI Files (*.avi)|*.avi|All Files (*.*)|*.*";

				openFileDialog.CheckFileExists = true;
				openFileDialog.DereferenceLinks = true;
			}
		}

		/// <summary>
		/// Occurs when playback has finished.
		/// </summary>
		[Description("Occurs when playback has finished")]
		public event EventHandler AviStopped;

		/// <summary>
		/// Occurs when playback has started.
		/// </summary>
		[Description("Occurs when playback has started")]
		public event EventHandler AviStarted;

		/// <summary>
		/// Gets a value indicating whether an animation is open.
		/// </summary>
		/// <value><c>true</c> if an animation is open; otherwise, <c>false</c>.</value>
		[Browsable(false)]
		public bool IsOpen
		{
			get { return m_isOpen; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether it should start immediately after opening.
		/// </summary>
		/// <value><c>true</c> if it should start immediately after opening; otherwise, <c>false</c>.</value>
		[DefaultValue(true)]
		[Description("Start immediately after opening")]
		[Category("Behavior")]
		public bool AutoStart
		{
			set { m_AutoStart = value; }
			get { return m_AutoStart; }
		}

		/// <summary>
		/// Gets or sets the background image displayed in the control.
		/// </summary>
		/// <value></value>
		/// <returns>An <see cref="T:System.Drawing.Image"/> that represents the image to display in the background of the control.</returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false)]
		public override Image BackgroundImage { get; set; }

		/// <summary>
		/// Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout"/> enumeration.
		/// </summary>
		/// <value></value>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout"/> (<see cref="F:System.Windows.Forms.ImageLayout.Center"/> , <see cref="F:System.Windows.Forms.ImageLayout.None"/>, <see cref="F:System.Windows.Forms.ImageLayout.Stretch"/>, <see cref="F:System.Windows.Forms.ImageLayout.Tile"/>, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom"/>). <see cref="F:System.Windows.Forms.ImageLayout.Tile"/> is the default value.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist. </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout { get; set; }

		/// <summary>
		/// Gets or sets the border style.
		/// </summary>
		/// <value>The border style.</value>
		[Category("Appearance")]
		[Description("The border style")]
		[DefaultValue(BorderStyle.None)]
		public BorderStyle BorderStyle
		{
			get
			{
				return m_BorderStyle;
			}

			set
			{
				m_BorderStyle = value;
				RecreateWindow(); 
				Reopen();
			}
		}

		/// <summary>
		/// Gets or sets the background color for the control.
		/// </summary>
		/// <value></value>
		/// <returns>A <see cref="T:System.Drawing.Color"/> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"/> property.</returns>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/>
		/// </PermissionSet>
		public override System.Drawing.Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				Reopen();
			}
		}

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>The type of the file.</value>
		[Category("Data")]
		[Description("Filename")]
		public AviFileType FileType
		{
			get 
			{ 
				return m_FileType; 
			}
			set
			{
				m_FileType = value;
				m_FileName = null;
				Open(m_FileType);
			}
		}

		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>The name of the file.</value>
		[Category("Data")]
		[Description("Filename")]
		[Editor(typeof(FileNameEditorAVI), typeof(UITypeEditor))]
		public string FileName
		{
			get 
			{ 
				return m_FileName; 
			}

			set 
			{ 
				m_FileType = AviFileType.ExternalFile;
				m_FileName = value;
				Open(m_FileName);
			}
		}
	}
}

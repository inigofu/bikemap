//------------------------------------------------------------------------------
// <copyright file="MyAnimation.cs" company="">
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

// Version 1.2
// June 21th 2010
//
// isometric_god@gmx.de
//

namespace GpxTracker
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Runtime.InteropServices;
	using System.Security.Permissions;
	using System.Windows.Forms;

	/// <summary>
	/// <para>
	/// How it works:
	/// The whole thing is based on the "SysAnimate32" object that is provided by the Microsoft Common Controls.
	/// Unfortunately there is no port to .NET available, so most of this code is just wrapping native Win32 API
	/// </para><para>
	/// First, a few functions and constants have to be defined. Their original versions can be found in windows.h
	/// and its relatives like gdi32.h, comctl32.h, etc.
	/// </para><para>
	/// Most of Windows native AVI files are contained in the shell32.dll file. Their resource IDs are not stored in 
	/// header files, but can be looked up directly in the DLL by any decent resource viewer tool. 
	/// </para><para>
	/// The basic idea is to create a NativeWindow control with classname set to "SysAnimate32" which gives a
	/// native window handle to the desired object. The animation object is controlled by sending messages to it.
	/// Animation Control Message -> ACM_OPEN, ACM_PLAY, ACM_STOP
	/// </para><para>
	/// There are two ways to open files:
	///
	/// 1) Load AVI from file. Only plain simple AVI files are supported. No sound, 
	///    no fancy compression, simple transparency.
	///
	/// 2) Load AVI from resource
	///    The Windows library "shell32.dll" which contains several Windows-internal AVIs is loaded as resource data file
	///    and its contents are transferred to the SysAnimate32 control
	/// </para><para>
	/// The size of the loaded AVI is retrieved by calling the native function GetWindowRect() right after loading. The control
	/// resizes itself automatically as the ACS_CENTER flag is not set, the hosting .NET control object is resizing to it's
	/// child's area by overriding SetBoundsCore() 
	/// </para><para>
	/// The control always uses ACS_TRANSPARENT flag which allows to replace the transparent part of the animation with a
	/// different color. By default this is the systems default control color. See GetSysColor() for more information.
	/// Before drawing the first time the control sends the WM_CTLCOLORSTATIC message to its parent which is the .NET 
	/// animation control here and hands over its device context handle (HDC). The background color can be changed by
	/// calling SetBkColor() with the appropriate BGR color value. See the RGB() macro for information on how to convert
	/// ARGB into BGR.
	/// </para><para>
	/// Every succeeding WM_CTLCOLORSTATIC is ignored silenty which makes it necessary to reopen the file if the
	/// background color changes.
	/// </para><para>
	/// The WM_COMMAND message is caught to test for the ACN_START and ACN_STOP flags. They are sent only once per 
	/// play/stop command and not after every loop
	/// </para><para>
	/// Some ideas and also a few lines of code were reused by three projects
	/// 1) The .NET Animation Control by Martin Cook
	///    http://www.codeproject.com/KB/miscctrl/CGAnimation.aspx
	///
	/// 2) Animation Control by "Volte"
	///    http://www.xtremedotnettalk.com/showthread.php?t=74578
	///
	/// 3) José Roca on animation controls
	///    http://www.forum.it-berater.org/index.php?topic=989.msg1572
	/// </para><para>
	/// For more information about SysAnimate32, see the CAnimateCtrl Class (MFC) currently at
	/// http://msdn.microsoft.com/en-us/library/z44k3stc(VS.80).aspx
	/// </para>
	/// </summary>
	/// <remarks>
	/// ACS_CENTER is never used as it does not only center itself with its parent, 
	/// but also draws the area outside the animation with default-control-color
	/// </remarks>
	public partial class MyAnimation : Control
	{
		/// <summary>
		/// Equals Win32 API struct RECT.
		/// GetWindowRect() requires this struct.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct MyRect
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		/// <summary>
		/// Native methods required for the class.
		/// </summary>
		private static class NativeMethods
		{
			[DllImport("user32.dll", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
			public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, string lParam);

			[DllImport("user32.dll")]
			public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GetWindowRect(IntPtr hWnd, ref MyRect rect);

			[DllImport("kernel32", CharSet = CharSet.Unicode, ThrowOnUnmappableChar = true)]
			public static extern IntPtr LoadLibraryEx(string lpLibFileName, IntPtr hFile, int dwFlags);

			[DllImport("kernel32")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FreeLibrary(IntPtr hLibModule);

			[DllImport("gdi32")]
			public static extern uint SetBkColor(IntPtr hdc, uint crColor);
		}

		private const int WM_CTLCOLORSTATIC = 0x138;		// callback which asks fot SetBkColor()
		private const int WM_COMMAND = 0x111;				// notify messages for ACN_START and ACN_STOP
		private const int WM_USER = 0x400;

		private const int ACM_OPENA = (WM_USER + 100);		// MBCS version
		private const int ACM_OPENW = (WM_USER + 103);		// UNICODE version
		private const int ACM_PLAY  = (WM_USER + 101);
		private const int ACM_STOP  = (WM_USER + 102);

		private const int ACN_START = 1;					// callback encapsulated in WM_COMMAND
		private const int ACN_STOP = 2;						// callback encapsulated in WM_COMMAND

		private const int ACS_CENTER      = 1;				// never used
		private const int ACS_TRANSPARENT = 2;
		private const int ACS_AUTOPLAY    = 4;
		private const int ACS_TIMER       = 8; 
		
		private const int WS_VISIBLE = 0x10000000;
		private const int WS_CHILD   = 0x40000000;
		private const int WS_BORDER  = 0x00800000;

		private const int WS_EX_STATICEDGE = 0x00020000;

		private const int LOAD_LIBRARY_AS_DATAFILE = 0x0002;

		private readonly int ACM_OPEN = (Marshal.SystemDefaultCharSize == 1) ? ACM_OPENA : ACM_OPENW;

		private bool m_AutoStart = true;
		private IntPtr m_hShellModule;
		private bool m_isOpen;
		private AviFileType m_FileType;
		private string m_FileName;
		private BorderStyle m_BorderStyle;
		private NativeWindow m_Window; // The actual animation control

		/// <summary>
		/// 
		/// </summary>
		public enum AviFileType
		{
			/// <summary>
			/// An external AVI file.
			/// </summary>
			ExternalFile = 0,

			/// <summary>
			/// The "search for folder" AVI file.
			/// </summary>
			Search4Folder = 150,

			/// <summary>
			/// The "seach for file" AVI file.
			/// </summary>
			Search4File = 151,

			/// <summary>
			/// The "search for computer" AVI file.
			/// </summary>
			Search4Computer = 152,

			/// <summary>
			/// The "move file" AVI file.
			/// </summary>
			MoveFile = 160,

			/// <summary>
			/// The "copy file" AVI file.
			/// </summary>
			CopyFile = 161,

			/// <summary>
			/// The "delete to recycle bin" AVI file.
			/// </summary>
			Delete2RecycleBin = 162,
			/// <summary>
			/// The "clean the recycle bin" AVI file.
			/// </summary>
			CleanRecycleBin = 163,

			/// <summary>
			/// The "delete file" AVI file.
			/// </summary>
			DeleteFile = 164,

			/// <summary>
			/// The "copy settings" AVI file.
			/// </summary>
			CopySettings = 165,

			/// <summary>
			/// The "search the internet" AVI file.
			/// </summary>
			SearchWeb = 166,

			/// <summary>
			/// CopyOld1.
			/// </summary>
			CopyOld1 = 167,

			/// <summary>
			/// CopyOld2.
			/// </summary>
			CopyOld2 = 168,

			/// <summary>
			/// DeleteOld.
			/// </summary>
			DeleteOld = 169,

			/// <summary>
			/// DownloadOld.
			/// </summary>
			DownloadOld = 170,
		
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MyAnimation"/> class.
		/// </summary>
		public MyAnimation()
		{
			RecreateWindow();
		}

		/// <summary>
		/// Opens the specified animation type.
		/// </summary>
		/// <param name="file">The animation type to open.</param>
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Open(AviFileType file)
		{
			LoadShell();
			m_isOpen = (NativeMethods.SendMessage(m_Window.Handle, ACM_OPEN, m_hShellModule, (IntPtr)file) != IntPtr.Zero);

			Size = GetInternalSize();
		}

		/// <summary>
		/// Opens the specified AVI file.
		/// </summary>
		/// <param name="fname">The AVI filename.</param>
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Open(string fname)
		{
			UnloadShell();
			m_isOpen = (NativeMethods.SendMessage(m_Window.Handle, ACM_OPEN, IntPtr.Zero, fname) != IntPtr.Zero);
		}

		/// <summary>
		/// Stop immediately and reset to frame 0.
		/// </summary>
		public void Reset()
		{
			Play(1, 0, 0);
		}

		/// <summary>
		/// Stop after the last frame.
		/// </summary>
		public void Finish()
		{
			Play(1, -1, -1);
		}

		/// <summary>
		/// Stops the animation.
		/// </summary>
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Stop()
		{
			NativeMethods.SendMessage(m_Window.Handle, ACM_STOP, IntPtr.Zero, IntPtr.Zero);
		}

		/// <summary>
		/// Main Play method: plays a certain section of the AVI a certain amount of
		/// times. If you specify -1 for repeat, it loops infinitely. If you specify 0
		/// for from, it starts from the beginning. If you specify -1 for to, it will play
		/// until the end.
		/// </summary>
		public void Play()
		{
			Play(-1, 0, -1);		// play from the beginning to the end, loop infinitely
		}

		/// <summary>
		/// Plays the specified repeat count number of times.
		/// </summary>
		/// <param name="repeat">The number of repetitions.</param>
		/// <param name="startFrame">The start frame.</param>
		/// <param name="endFrame">The end frame.</param>
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void Play(int repeat, int startFrame, int endFrame)
		{
			NativeMethods.SendMessage(m_Window.Handle, ACM_PLAY, 
				new IntPtr(repeat), MakeIntPtr(
					Math.Min(startFrame, 0xffff), 
					Math.Min(endFrame, 0xffff)));
		}

		/// <summary>
		/// Recreates the window.
		/// </summary>
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected void RecreateWindow()
		{
			CreateParams cp = new CreateParams();

			if (m_Window != null)
				m_Window.DestroyHandle();

			// Create a "SysAnimate32" control, which is the Windows
			// class name for the common control Animate window

			cp.ClassName = "SysAnimate32";
			cp.Parent = this.Handle;
			cp.Width = 500;
			cp.Height = 170;
			cp.Style = WS_CHILD | WS_VISIBLE | ACS_TRANSPARENT;

			switch (m_BorderStyle)
			{
				case BorderStyle.FixedSingle:
					cp.Style |= WS_BORDER;
					break;

				case BorderStyle.Fixed3D:
					cp.ExStyle |= WS_EX_STATICEDGE;
					break;
			}

			//		if (m_Center)
			//			cp.Style |= ACS_CENTER;

			if (m_AutoStart)
				cp.Style |= ACS_AUTOPLAY;

			m_Window = new NativeWindow();
			m_Window.CreateHandle(cp);
		}

		/// <summary>
		/// Performs the work of setting the specified bounds of this control.
		/// </summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left"/> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top"/> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width"/> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height"/> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified"/> values.</param>
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Size) != 0)
			{
				Size sz = GetInternalSize();
				width = sz.Width;
				height = sz.Height;
			}

			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process.</param>
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);

			if (m.Msg == WM_CTLCOLORSTATIC)
			{
	//			System.Diagnostics.Debug.Print("WM_CTLCOLORSTATIC");
				if (NativeMethods.SetBkColor(m.WParam, ColorToBGR(BackColor)) == 0xFFFFFFFF)	// CLR_INVALID
				{
					// ignore silently
				}
			}

			if (m.Msg == WM_COMMAND)
			{
				switch ((((uint)m.WParam) & 0xFFFF0000) >> 16)
				{
					case ACN_START:
	//					System.Diagnostics.Debug.Print("ACN_START");
						if (AviStarted != null) 
							AviStarted(this, new EventArgs());
						break;
					case ACN_STOP:
	//					System.Diagnostics.Debug.Print("ACN_STOP");
						if (AviStopped != null) 
							AviStopped(this, new EventArgs());
						break;
				}
			}
		}

		private void Reopen()
		{
			if (m_FileType == AviFileType.ExternalFile)
				Open(m_FileName);
			else
				Open(m_FileType);
		}

		private void LoadShell()
		{
			if (m_hShellModule == IntPtr.Zero)
			{
				m_hShellModule = NativeMethods.LoadLibraryEx("shell32.dll", IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
			}
		}

		private void UnloadShell()
		{
			if (m_hShellModule != IntPtr.Zero)
			{
				if (NativeMethods.FreeLibrary(m_hShellModule) == false)
				{
					int error = Marshal.GetLastWin32Error();
					throw new Win32Exception(error);
				}

				m_hShellModule = IntPtr.Zero;
			}
		}

		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private Size GetInternalSize()
		{
			MyRect rc;
			rc.Left = rc.Top = rc.Right = rc.Bottom = 0;
			NativeMethods.GetWindowRect(m_Window.Handle, ref rc);

			return new Size(rc.Right - rc.Left, rc.Bottom - rc.Top);
		}

		private static uint ColorToBGR(Color color)
		{
			uint r = (uint)color.R;
			uint g = (uint)color.G;
			uint b = (uint)color.B;

			return ((r) | (g << 8) | (b << 16));
		}

		private static IntPtr MakeIntPtr(int low, int high)
		{
			return new IntPtr((low & 0xffff) | (((short)high) << 0x10));
		}
	}
}

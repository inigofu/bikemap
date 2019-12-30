//------------------------------------------------------------------------------
// <copyright file="DeviceNotifyForm.cs" company="">
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
// <summary>Contains information about the DeviceNotifyForm class.</summary>
//------------------------------------------------------------------------------

namespace GpxTracker
{
	using System;
	using System.ComponentModel;
	using System.Windows.Forms;
	using System.Runtime.InteropServices;
	using System.Security.Permissions;

	/// <summary>
	/// The event args for the <see cref="DeviceNotifyForm"/> class.
	/// </summary>
	public class DeviceEventArgs : EventArgs
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DeviceEventArgs"/> class.
		/// </summary>
		/// <param name="volume">The volume(=drive) of interest.</param>
		public DeviceEventArgs(char volume)
		{
			Volume = volume;
		}

		/// <summary>
		/// Gets the volume.
		/// </summary>
		/// <value>The volume.</value>
		public char Volume { get; private set; }
	}
	
	/// <summary>
	/// A form that supports notification about newly attached devices.
	/// </summary>
	public class DeviceNotifyForm : Form
	{
		// wParam: DeviceEvent
		// lParam: A pointer to a structure that contains event-specific data.
		// The DBT_DEVICEARRIVAL and DBT_DEVICEREMOVECOMPLETE events are automatically broadcast to all top-level windows for port devices.
		// Volume notifications are also broadcast to top-level windows
		// -> See "RegisterDeviceNotification"
		private const int WM_DEVICECHANGE = 0x0219;

		private enum VolumeFlags : int
		{
			DBTF_MEDIA = 0x0001,
			DBTF_NET   = 0x0002
		}

		private enum DeviceEvent : uint
		{
			DBT_DEVNODES_CHANGED        = 0x0007,
			DBT_DEVICEARRIVAL           = 0x8000,  // system detected a new device
			DBT_DEVICEQUERYREMOVE       = 0x8001,  // wants to remove, may fail
			DBT_DEVICEQUERYREMOVEFAILED = 0x8002,  // removal aborted
			DBT_DEVICEREMOVEPENDING     = 0x8003,  // about to remove, still avail.
			DBT_DEVICEREMOVECOMPLETE    = 0x8004,  // device is gone
			DBT_DEVICETYPESPECIFIC      = 0x8005,  // type specific event
			DBT_CUSTOMEVENT             = 0x8006,  // user-defined event
		}

		private enum DeviceType : uint
		{
			DBT_DEVTYP_OEM              = 0x00000000,  // oem-defined device type
			DBT_DEVTYP_DEVNODE          = 0x00000001,  // devnode number
			DBT_DEVTYP_VOLUME           = 0x00000002,  // logical volume
			DBT_DEVTYP_PORT             = 0x00000003,  // serial, parallel
			DBT_DEVTYP_NET              = 0x00000004,  // network resource
			DBT_DEVTYP_DEVICEINTERFACE  = 0x00000005,  // device interface class
			DBT_DEVTYP_HANDLE           = 0x00000006,  // file system handle
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct DEV_BROADCAST_HDR
		{
			public uint dbch_size;							// size of the structure
			public uint dbch_devicetype;					// see DeviceType
			public uint dbch_reserved;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct DEV_BROADCAST_VOLUME
		{
			public uint dbch_size;							// identical to DEV_BROADCAST_HDR
			public uint dbch_devicetype;
			public uint dbch_reserved;
			public uint dbcv_unitmask;						// specifies drive letters
			public ushort dbcv_flags;						// see VolumeFlags
		}

		/// <summary>
		/// Occurs when a new device has arrived.
		/// </summary>
		[Category("Behavior")]
		[Description("Occurs when a new device has arrived")]
		public event EventHandler<DeviceEventArgs> DeviceArrivedEvent;

		/// <summary>
		/// Occurs when a device has been removed.
		/// </summary>
		[Category("Behavior")]
		[Description("Occurs when a device has been removed completely")]
		public event EventHandler<DeviceEventArgs> DeviceRemovedEvent;

		private void DeviceArrived(int unitmask)
		{
			for (int i = 0; i < 26; ++i)
			{
				if ((unitmask & 0x1) == 0x1)
				{
					var volume = (char)(i + (int)'A');
					if (DeviceArrivedEvent != null)
						DeviceArrivedEvent(this, new DeviceEventArgs(volume));
				}

				unitmask = unitmask >> 1;
			}
		}

		private void DeviceRemoved(int unitmask)
		{
			for (int i = 0; i < 26; ++i)
			{
				if ((unitmask & 0x1) == 0x1)
				{
					var volume = (char)(i + (int)'A');
					if (DeviceRemovedEvent != null)
						DeviceRemovedEvent(this, new DeviceEventArgs(volume));
				}

				unitmask = unitmask >> 1;
			}
		}

		/// <summary>
		/// The WndProc function where the events are caugt.
		/// </summary>
		/// <param name="msg">The message.</param>
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message msg)
		{
			base.WndProc(ref msg);

			if (msg.Msg != WM_DEVICECHANGE)
				return;

			var deviceEvent = (DeviceEvent)msg.WParam;

			switch (deviceEvent)
			{
				case DeviceEvent.DBT_DEVICEARRIVAL:
					{
						DEV_BROADCAST_VOLUME volume;
						var hdr = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(msg.LParam, typeof(DEV_BROADCAST_HDR));

						if (hdr.dbch_devicetype == (uint)DeviceType.DBT_DEVTYP_VOLUME)
						{
							volume = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(msg.LParam, typeof(DEV_BROADCAST_VOLUME));

							DeviceArrived((int)volume.dbcv_unitmask);
						}
					}
					break;

				case DeviceEvent.DBT_DEVICEREMOVECOMPLETE:
					{
						DEV_BROADCAST_VOLUME volume;
						var hdr = (DEV_BROADCAST_HDR)Marshal.PtrToStructure(msg.LParam, typeof(DEV_BROADCAST_HDR));

						if (hdr.dbch_devicetype == (uint)DeviceType.DBT_DEVTYP_VOLUME)
						{
							volume = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(msg.LParam, typeof(DEV_BROADCAST_VOLUME));

							DeviceRemoved((int)volume.dbcv_unitmask);
						}
					}
					break;
			}
		}
	}
}

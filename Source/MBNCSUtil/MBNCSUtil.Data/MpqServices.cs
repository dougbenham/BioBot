using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace MBNCSUtil.Data
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public sealed class MpqServices
	{
		private static class SingletonHost
		{
			public static MpqServices Singleton;

			static SingletonHost()
			{
				MpqServices.SingletonHost.Singleton = new MpqServices();
			}
		}

		private string m_path;

		private IntPtr m_hMod;

		private List<MpqArchive> m_archives;

		private static MpqServices Instance
		{
			get
			{
				return MpqServices.SingletonHost.Singleton;
			}
		}

		private MpqServices()
		{
			this.m_path = Path.GetTempFileName();
			FileStream fileStream = new FileStream(this.m_path, FileMode.Open, FileAccess.Write, FileShare.None);
			byte[] array;
			if (NativeMethods.Is64BitProcess)
			{
				array = Resources.StormLib64;
			}
			else
			{
				array = Resources.StormLib32;
			}
			fileStream.Write(array, 0, array.Length);
			fileStream.Close();
			this.m_hMod = NativeMethods.LoadLibrary(this.m_path);
			if (this.m_hMod == IntPtr.Zero)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				File.Delete(this.m_path);
				throw new Win32Exception(lastWin32Error);
			}
			LateBoundStormDllApi.Initialize(this.m_hMod);
			this.m_archives = new List<MpqArchive>();
		}

		public static MpqArchive OpenArchive(string fullPath)
		{
			MpqServices arg_05_0 = MpqServices.Instance;
			MpqArchive mpqArchive = new MpqArchive(fullPath);
			MpqServices.Instance.m_archives.Add(mpqArchive);
			return mpqArchive;
		}

		internal static void NotifyArchiveDisposed(MpqArchive archive)
		{
			if (MpqServices.Instance.m_archives.Contains(archive))
			{
				MpqServices.Instance.m_archives.Remove(archive);
			}
		}

		public static void CloseArchive(MpqArchive archive)
		{
			MpqServices arg_05_0 = MpqServices.Instance;
			archive.Dispose();
		}
	}
}

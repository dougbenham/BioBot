using System;
using System.Runtime.InteropServices;

namespace MBNCSUtil.Data
{
	internal static class NativeMethods
	{
		public static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern IntPtr LoadLibrary(string path);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ThrowOnUnmappableChar = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
	}
}

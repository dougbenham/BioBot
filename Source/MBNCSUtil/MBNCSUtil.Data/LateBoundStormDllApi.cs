using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace MBNCSUtil.Data
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	internal static class LateBoundStormDllApi
	{
		private static SFileOpenArchiveCallback callback_SFileOpenArchive;

		private static SFileCloseArchiveCallback callback_SFileCloseArchive;

		private static SFileHasFileCallback callback_SFileHasFile;

		private static SFileOpenFileExCallback callback_SFileOpenFileEx;

		private static SFileCloseFileCallback callback_SFileCloseFile;

		private static SFileGetFileSizeCallback callback_SFileGetFileSize;

		private static SFileSetFilePointerCallback callback_SFileSetPointer;

		private static SFileReadFileCallback callback_SFileReadFile;

		[DebuggerStepThrough]
		private static void ThrowMpqException(MpqErrorCodes status)
		{
			switch (status)
			{
			case MpqErrorCodes.MpqInvalid:
				throw new MpqException(Resources.mpq_mpqArchiveCorrupt);
			case MpqErrorCodes.FileNotFound:
				throw new MpqException(Resources.mpq_fileNotFound);
			default:
				if (status == MpqErrorCodes.BadOpenMode)
				{
					throw new MpqException(Resources.mpq_badOpenMode);
				}
				throw new MpqException(string.Format(CultureInfo.InvariantCulture, Resources.mpq_UnknownErrorType, new object[]
				{
					status
				}));
			}
		}

		public static void Initialize(IntPtr hModule)
		{
			IntPtr procAddress = NativeMethods.GetProcAddress(hModule, "SFileOpenArchive");
			LateBoundStormDllApi.callback_SFileOpenArchive = (SFileOpenArchiveCallback)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(SFileOpenArchiveCallback));
			IntPtr procAddress2 = NativeMethods.GetProcAddress(hModule, "SFileCloseArchive");
			LateBoundStormDllApi.callback_SFileCloseArchive = (SFileCloseArchiveCallback)Marshal.GetDelegateForFunctionPointer(procAddress2, typeof(SFileCloseArchiveCallback));
			IntPtr procAddress3 = NativeMethods.GetProcAddress(hModule, "SFileOpenFileEx");
			LateBoundStormDllApi.callback_SFileOpenFileEx = (SFileOpenFileExCallback)Marshal.GetDelegateForFunctionPointer(procAddress3, typeof(SFileOpenFileExCallback));
			IntPtr procAddress4 = NativeMethods.GetProcAddress(hModule, "SFileHasFile");
			LateBoundStormDllApi.callback_SFileHasFile = (SFileHasFileCallback)Marshal.GetDelegateForFunctionPointer(procAddress4, typeof(SFileHasFileCallback));
			IntPtr procAddress5 = NativeMethods.GetProcAddress(hModule, "SFileCloseFile");
			LateBoundStormDllApi.callback_SFileCloseFile = (SFileCloseFileCallback)Marshal.GetDelegateForFunctionPointer(procAddress5, typeof(SFileCloseFileCallback));
			IntPtr procAddress6 = NativeMethods.GetProcAddress(hModule, "SFileGetFileSize");
			LateBoundStormDllApi.callback_SFileGetFileSize = (SFileGetFileSizeCallback)Marshal.GetDelegateForFunctionPointer(procAddress6, typeof(SFileGetFileSizeCallback));
			IntPtr procAddress7 = NativeMethods.GetProcAddress(hModule, "SFileSetFilePointer");
			LateBoundStormDllApi.callback_SFileSetPointer = (SFileSetFilePointerCallback)Marshal.GetDelegateForFunctionPointer(procAddress7, typeof(SFileSetFilePointerCallback));
			IntPtr procAddress8 = NativeMethods.GetProcAddress(hModule, "SFileReadFile");
			LateBoundStormDllApi.callback_SFileReadFile = (SFileReadFileCallback)Marshal.GetDelegateForFunctionPointer(procAddress8, typeof(SFileReadFileCallback));
		}

		public static IntPtr SFileOpenArchive(string fileName, uint dwPriority, uint dwFlags)
		{
			IntPtr zero = IntPtr.Zero;
			MpqErrorCodes mpqErrorCodes = LateBoundStormDllApi.callback_SFileOpenArchive(fileName, dwPriority, dwFlags, ref zero);
			if (mpqErrorCodes != MpqErrorCodes.Okay)
			{
				LateBoundStormDllApi.ThrowMpqException(mpqErrorCodes);
			}
			return zero;
		}

		public static void SFileCloseArchive(IntPtr hMPQ)
		{
			MpqErrorCodes mpqErrorCodes = LateBoundStormDllApi.callback_SFileCloseArchive(hMPQ);
			if (mpqErrorCodes != MpqErrorCodes.Okay)
			{
				LateBoundStormDllApi.ThrowMpqException(mpqErrorCodes);
			}
		}

		public static bool SFileHasFile(IntPtr hMPQ, string fileName)
		{
			return LateBoundStormDllApi.callback_SFileHasFile(hMPQ, fileName);
		}

		public static IntPtr SFileOpenFileEx(IntPtr hMPQ, string fileName, SearchType searchScope)
		{
			IntPtr zero = IntPtr.Zero;
			MpqErrorCodes mpqErrorCodes = LateBoundStormDllApi.callback_SFileOpenFileEx(hMPQ, fileName, searchScope, ref zero);
			if (mpqErrorCodes != MpqErrorCodes.Okay)
			{
				LateBoundStormDllApi.ThrowMpqException(mpqErrorCodes);
			}
			return zero;
		}

		public static void SFileCloseFile(IntPtr hFile)
		{
			MpqErrorCodes mpqErrorCodes = LateBoundStormDllApi.callback_SFileCloseFile(hFile);
			if (mpqErrorCodes != MpqErrorCodes.Okay)
			{
				LateBoundStormDllApi.ThrowMpqException(mpqErrorCodes);
			}
		}

		public static long SFileGetFileSize(IntPtr hFile)
		{
			int num = 0;
			int num2 = LateBoundStormDllApi.callback_SFileGetFileSize(hFile, ref num);
			return (long)(num + num2);
		}

		public static long SFileSetFilePointer(IntPtr hFile, long distanceToMove, SeekOrigin seekType)
		{
			int num = (int)(distanceToMove >> 32);
			int num2 = (int)(distanceToMove & (long)((ulong)-1));
			num2 = LateBoundStormDllApi.callback_SFileSetPointer(hFile, num2, ref num, seekType);
			return (long)(num + num2);
		}

		public static int SFileReadFile(IntPtr hFile, byte[] lpBuffer, int numberToRead)
		{
			int result = 0;
			MpqErrorCodes mpqErrorCodes = LateBoundStormDllApi.callback_SFileReadFile(hFile, lpBuffer, (uint)numberToRead, ref result, IntPtr.Zero);
			if (mpqErrorCodes != MpqErrorCodes.Okay)
			{
				Debugger.Break();
				LateBoundStormDllApi.ThrowMpqException(mpqErrorCodes);
			}
			return result;
		}
	}
}

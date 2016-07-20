using System;

namespace MBNCSUtil.Data
{
	internal delegate MpqErrorCodes SFileOpenFileExCallback(IntPtr hMPQ, string lpFileName, SearchType dwSearchScope, ref IntPtr hFile);
}

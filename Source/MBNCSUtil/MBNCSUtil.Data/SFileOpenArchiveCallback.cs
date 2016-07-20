using System;

namespace MBNCSUtil.Data
{
	internal delegate MpqErrorCodes SFileOpenArchiveCallback(string lpFileName, uint dwPriority, uint dwFlags, ref IntPtr hMPQ);
}

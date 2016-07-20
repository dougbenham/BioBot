using System;

namespace MBNCSUtil.Data
{
	internal delegate MpqErrorCodes SFileOpenFileCallback(string lpFileName, ref IntPtr hFile);
}

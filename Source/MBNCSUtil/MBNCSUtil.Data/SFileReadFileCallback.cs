using System;

namespace MBNCSUtil.Data
{
	internal delegate MpqErrorCodes SFileReadFileCallback(IntPtr hFile, byte[] lpBuffer, uint nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);
}

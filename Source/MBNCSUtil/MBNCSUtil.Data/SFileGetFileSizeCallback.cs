using System;

namespace MBNCSUtil.Data
{
	internal delegate int SFileGetFileSizeCallback(IntPtr hFile, ref int lpFileSizeHigh);
}

using System;
using System.IO;

namespace MBNCSUtil.Data
{
	internal delegate int SFileSetFilePointerCallback(IntPtr hFile, int lDistanceToMove, ref int lplDistanceToMoveHigh, SeekOrigin dwMoveMethod);
}

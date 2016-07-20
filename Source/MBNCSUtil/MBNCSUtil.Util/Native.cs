using System;

namespace MBNCSUtil.Util
{
	internal static class Native
	{
		internal unsafe static void Memcpy(void* target, void* src, int byteLength)
		{
			if (byteLength % 4 == 0)
			{
				byteLength /= 4;
				for (int i = 0; i < byteLength; i++)
				{
					*(int*)((byte*)target + (IntPtr)i * 4) = *(int*)((byte*)src + (IntPtr)i * 4);
				}
				return;
			}
			for (int j = 0; j < byteLength; j++)
			{
				((byte*)target)[j] = ((byte*)src)[j];
			}
		}

		internal unsafe static void Memset(void* target, byte value, int byteLength)
		{
			if (byteLength % 4 == 0)
			{
				int num = (int)value | (int)value << 8 | (int)value << 16 | (int)value << 24;
				byteLength /= 4;
				for (int i = 0; i < byteLength; i++)
				{
					*(int*)((byte*)target + (IntPtr)i * 4) = num;
				}
				return;
			}
			for (int j = 0; j < byteLength; j++)
			{
				((byte*)target)[j] = value;
			}
		}

		internal unsafe static byte* Memmove(byte* dest, byte* src, int byteCount)
		{
			using (HeapPtr heapPtr = new HeapPtr(byteCount, AllocMethod.HGlobal))
			{
				heapPtr.ReadData(src, byteCount);
				Native.Memcpy((void*)dest, heapPtr.ToPointer(), byteCount);
			}
			return dest;
		}
	}
}

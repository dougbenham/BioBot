using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MBNCSUtil.Util
{
	internal sealed class LockdownHeap
	{
		private class LDHeapRecord
		{
			public byte[] data;
		}

		private List<LockdownHeap.LDHeapRecord> m_obs;

		public int CurrentLength
		{
			get
			{
				return this.m_obs.Count;
			}
		}

		public LockdownHeap()
		{
			this.m_obs = new List<LockdownHeap.LDHeapRecord>();
		}

		public void Add(int[] src)
		{
			byte[] array = new byte[src.Length * 4];
			Buffer.BlockCopy(src, 0, array, 0, src.Length * 4);
			this.Add(array);
		}

		public void Add(byte[] data)
		{
			if (data.Length < 16)
			{
				throw new ArgumentOutOfRangeException("data", "Argument must be 16 bytes or longer.");
			}
			LockdownHeap.LDHeapRecord lDHeapRecord = new LockdownHeap.LDHeapRecord();
			lDHeapRecord.data = new byte[16];
			Buffer.BlockCopy(data, 0, lDHeapRecord.data, 0, 16);
			this.m_obs.Add(lDHeapRecord);
		}

		public HeapPtr ToPointer()
		{
			int byteLength = this.m_obs.Count * 16;
			HeapPtr heapPtr = new HeapPtr(byteLength, AllocMethod.HGlobal);
			for (int i = 0; i < this.m_obs.Count; i++)
			{
				IntPtr destination = new IntPtr(((IntPtr)heapPtr).ToInt64() + (long)(i * 16));
				Marshal.Copy(this.m_obs[i].data, 0, destination, 16);
			}
			return heapPtr;
		}
	}
}

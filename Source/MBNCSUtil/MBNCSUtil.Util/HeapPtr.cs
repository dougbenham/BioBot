using System;
using System.Runtime.InteropServices;

namespace MBNCSUtil.Util
{
	internal sealed class HeapPtr : IDisposable
	{
		private int m_len;

		private IntPtr m_ptr;

		private AllocMethod m_method;

		public HeapPtr(int byteLength, AllocMethod method)
		{
			this.m_len = byteLength;
			if (!Enum.IsDefined(typeof(AllocMethod), method))
			{
				throw new ArgumentOutOfRangeException("method");
			}
			if (byteLength < 0)
			{
				throw new ArgumentOutOfRangeException("byteLength");
			}
			this.m_method = method;
			switch (method)
			{
			case AllocMethod.HGlobal:
				this.m_ptr = Marshal.AllocHGlobal(byteLength);
				return;
			case AllocMethod.CoTaskMem:
				this.m_ptr = Marshal.AllocCoTaskMem(byteLength);
				return;
			default:
				return;
			}
		}

		public unsafe void ReadData(byte* ptr, int byteLength)
		{
			byte[] array = new byte[byteLength];
			Marshal.Copy(new IntPtr((void*)ptr), array, 0, byteLength);
			this.MarshalData(array);
		}

		public void MarshalData(byte[] data)
		{
			if (this.m_ptr == IntPtr.Zero)
			{
				throw new ObjectDisposedException("HeapPtr");
			}
			if (data.Length > this.m_len)
			{
				this.m_len = data.Length;
				this.Realloc(data.Length);
			}
			Marshal.Copy(data, 0, this.m_ptr, data.Length);
		}

		public void Realloc(int newLength)
		{
			if (this.m_ptr == IntPtr.Zero)
			{
				throw new ObjectDisposedException("HeapPtr");
			}
			switch (this.m_method)
			{
			case AllocMethod.HGlobal:
				this.m_ptr = Marshal.ReAllocHGlobal(this.m_ptr, new IntPtr(newLength));
				return;
			case AllocMethod.CoTaskMem:
				this.m_ptr = Marshal.ReAllocCoTaskMem(this.m_ptr, newLength);
				return;
			default:
				throw new InvalidOperationException("Invalid memory type.");
			}
		}

		public unsafe void* ToPointer()
		{
			if (this.m_ptr == IntPtr.Zero)
			{
				throw new ObjectDisposedException("HeapPtr");
			}
			return this.m_ptr.ToPointer();
		}

		~HeapPtr()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (this.m_ptr == IntPtr.Zero)
			{
				return;
			}
			switch (this.m_method)
			{
			case AllocMethod.HGlobal:
				Marshal.FreeHGlobal(this.m_ptr);
				break;
			case AllocMethod.CoTaskMem:
				Marshal.FreeCoTaskMem(this.m_ptr);
				break;
			}
			this.m_ptr = IntPtr.Zero;
		}

		public unsafe static implicit operator byte*(HeapPtr h)
		{
			return (byte*)h.ToPointer();
		}

		public static explicit operator IntPtr(HeapPtr h)
		{
			return h.m_ptr;
		}
	}
}

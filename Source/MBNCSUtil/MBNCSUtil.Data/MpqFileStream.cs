using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;

namespace MBNCSUtil.Data
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public class MpqFileStream : Stream, IDisposable
	{
		private IntPtr m_hFile;

		private string m_path;

		private MpqArchive m_owner;

		private bool m_disposed;

		private long m_pos;

		public string Name
		{
			get
			{
				return this.m_path;
			}
		}

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override long Length
		{
			get
			{
				this.checkDisposed();
				return LateBoundStormDllApi.SFileGetFileSize(this.m_hFile);
			}
		}

		public override long Position
		{
			get
			{
				return this.m_pos;
			}
			set
			{
				this.checkDisposed();
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		[DebuggerStepThrough]
		private void checkDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(this.m_path, "The MpqFileStream for this object has been disposed.");
			}
		}

		[DebuggerStepThrough]
		internal MpqFileStream(string internalPath, MpqArchive parent)
		{
			this.m_owner = parent;
			IntPtr hFile = LateBoundStormDllApi.SFileOpenFileEx(parent.Handle, internalPath, SearchType.CurrentOnly);
			this.m_path = internalPath;
			this.m_hFile = hFile;
		}

		public new void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			if (this.m_hFile != IntPtr.Zero)
			{
				LateBoundStormDllApi.SFileCloseFile(this.m_hFile);
				this.m_hFile = IntPtr.Zero;
				this.m_owner.FileIsDisposed(this);
				this.m_owner = null;
				this.m_path = null;
			}
			this.m_disposed = true;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			this.checkDisposed();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", "The read buffer cannot be null.");
			}
			int num = count - offset;
			if (num > buffer.Length)
			{
				num = buffer.Length;
			}
			int num2 = (int)(this.Length - this.Position);
			if (num2 > num)
			{
				num2 = num;
			}
			byte[] array = new byte[num];
			int num3 = LateBoundStormDllApi.SFileReadFile(this.m_hFile, array, num2);
			Buffer.BlockCopy(array, 0, buffer, offset, num3);
			this.m_pos += (long)num3;
			return num3;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			long pos = LateBoundStormDllApi.SFileSetFilePointer(this.m_hFile, offset, origin);
			this.m_pos = pos;
			return this.m_pos;
		}

		public override void Flush()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}
	}
}

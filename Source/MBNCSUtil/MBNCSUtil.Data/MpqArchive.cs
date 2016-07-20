using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace MBNCSUtil.Data
{
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public class MpqArchive : IDisposable
	{
		private IntPtr m_hMPQ;

		private bool m_disposed;

		private List<MpqFileStream> m_files;

		internal IntPtr Handle
		{
			[DebuggerStepThrough]
			get
			{
				this.checkDisposed();
				return this.m_hMPQ;
			}
		}

		[DebuggerStepThrough]
		private void checkDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("MpqArchive");
			}
		}

		internal MpqArchive(string path)
		{
			this.m_files = new List<MpqFileStream>();
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(Resources.fileNotFound, path);
			}
			this.m_hMPQ = LateBoundStormDllApi.SFileOpenArchive(path, 1u, 0u);
		}

		public MpqFileStream OpenFile(string mpqFilePath)
		{
			if (mpqFilePath == null)
			{
				throw new ArgumentNullException(Resources.param_mpqFilePath, Resources.mpqFilePathArgNull);
			}
			return new MpqFileStream(mpqFilePath, this);
		}

		[DebuggerStepThrough]
		internal void FileIsDisposed(MpqFileStream mfs)
		{
			this.checkDisposed();
			this.m_files.Remove(mfs);
		}

		public bool ContainsFile(string fileName)
		{
			return LateBoundStormDllApi.SFileHasFile(this.m_hMPQ, fileName);
		}

		public void SaveToPath(string mpqFileName, string pathBase)
		{
			this.SaveToPath(mpqFileName, pathBase, false);
		}

		public void SaveToPath(string mpqFileName, string pathBase, bool useFullMpqPath)
		{
			string path;
			if (useFullMpqPath)
			{
				path = Path.Combine(pathBase, mpqFileName);
			}
			else
			{
				path = Path.Combine(pathBase, mpqFileName.Substring(mpqFileName.LastIndexOf('\\') + 1));
			}
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			using (MpqFileStream mpqFileStream = this.OpenFile(mpqFileName))
			{
				byte[] array = new byte[mpqFileStream.Length];
				mpqFileStream.Read(array, 0, array.Length);
				File.WriteAllBytes(path, array);
			}
		}

		~MpqArchive()
		{
			this.Dispose(false);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this.m_disposed)
			{
				return;
			}
			if (disposing)
			{
				foreach (MpqFileStream current in this.m_files)
				{
					current.Dispose();
				}
				this.m_files.Clear();
				this.m_files = null;
			}
			if (this.m_hMPQ != IntPtr.Zero)
			{
				LateBoundStormDllApi.SFileCloseArchive(this.m_hMPQ);
			}
			this.m_disposed = true;
			MpqServices.NotifyArchiveDisposed(this);
		}

		public string GetListFile()
		{
			string result = string.Empty;
			using (MpqFileStream mpqFileStream = this.OpenFile("(listfile)"))
			{
				StreamReader streamReader = new StreamReader(mpqFileStream, Encoding.ASCII);
				result = streamReader.ReadToEnd();
				streamReader.Close();
			}
			return result;
		}
	}
}

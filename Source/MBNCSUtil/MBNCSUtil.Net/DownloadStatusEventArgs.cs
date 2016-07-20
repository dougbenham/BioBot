using System;

namespace MBNCSUtil.Net
{
	public sealed class DownloadStatusEventArgs : EventArgs
	{
		private int m_count;

		private int m_total;

		private string m_fileName;

		public int DownloadStatus
		{
			get
			{
				return this.m_count;
			}
		}

		public int FileLength
		{
			get
			{
				return this.m_total;
			}
		}

		public string FileName
		{
			get
			{
				return this.m_fileName;
			}
		}

		internal DownloadStatusEventArgs(int current, int total, string file)
		{
			this.m_count = current;
			this.m_total = total;
			this.m_fileName = file;
		}
	}
}

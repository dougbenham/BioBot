using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace MBNCSUtil.Net
{
	public abstract class BnFtpRequestBase
	{
		private string m_fileName;

		private string m_localFile;

		private string m_product;

		private string m_server = "useast.battle.net";

		private int m_size;

		private DateTime? m_time;

		public event DownloadStatusEventHandler FilePartDownloaded
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.FilePartDownloaded = (DownloadStatusEventHandler)Delegate.Combine(this.FilePartDownloaded, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.FilePartDownloaded = (DownloadStatusEventHandler)Delegate.Remove(this.FilePartDownloaded, value);
			}
		}

		public virtual string Product
		{
			get
			{
				return this.m_product;
			}
		}

		public string LocalFileName
		{
			get
			{
				return this.m_localFile;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(Resources.param_value, Resources.fileNull);
				}
				if (!value.Contains("\\"))
				{
					value = Path.GetFullPath(".\\" + value);
				}
				string directoryName = Path.GetDirectoryName(value);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				this.m_localFile = value;
			}
		}

		public string FileName
		{
			get
			{
				return this.m_fileName;
			}
		}

		public int FileSize
		{
			get
			{
				return this.m_size;
			}
			protected set
			{
				this.m_size = value;
			}
		}

		public DateTime? FileTime
		{
			get
			{
				return this.m_time;
			}
		}

		public string Server
		{
			get
			{
				return this.m_server;
			}
			set
			{
				if (value == null)
				{
					value = "useast.battle.net";
				}
				this.m_server = value;
			}
		}

		protected BnFtpRequestBase(string fileName, string product, DateTime? fileTime)
		{
			this.m_fileName = fileName;
			if (fileName.IndexOf('\\') != -1)
			{
				this.m_fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
			}
			this.m_product = product.ToUpperInvariant();
			this.m_time = fileTime;
			this.LocalFileName = fileName;
		}

		protected virtual void OnFilePartDownloaded(DownloadStatusEventArgs e)
		{
			if (this.FilePartDownloaded != null)
			{
				this.FilePartDownloaded(this, e);
			}
		}

		public abstract void ExecuteRequest();
	}
}

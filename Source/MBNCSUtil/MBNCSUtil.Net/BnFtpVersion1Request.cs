using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace MBNCSUtil.Net
{
	public class BnFtpVersion1Request : BnFtpRequestBase
	{
		private string m_adExt;

		private int m_adId;

		private bool m_ad;

		public BnFtpVersion1Request(string productId, string fileName, DateTime? fileTime) : base(fileName, productId, fileTime)
		{
			string product = this.Product;
			if (product != Resources.star && product != Resources.sexp && product != Resources.d2dv && product != Resources.d2xp && product != Resources.w2bn)
			{
				throw new ArgumentOutOfRangeException(Resources.param_productId, productId, Resources.bnftp_ver1invalidProduct);
			}
		}

		public BnFtpVersion1Request(string productId, string fileName, DateTime fileTime, int adBannerId, string adBannerExtension) : this(productId, fileName, new DateTime?(fileTime))
		{
			this.m_adExt = adBannerExtension;
			this.m_adId = adBannerId;
			this.m_ad = true;
		}

		public override void ExecuteRequest()
		{
			DataBuffer dataBuffer = new DataBuffer();
			dataBuffer.InsertInt16((short)(33 + base.FileName.Length));
			dataBuffer.InsertInt16(256);
			dataBuffer.InsertDwordString("IX86");
			dataBuffer.InsertDwordString(this.Product);
			if (this.m_ad)
			{
				dataBuffer.InsertInt32(this.m_adId);
				dataBuffer.InsertDwordString(this.m_adExt);
			}
			else
			{
				dataBuffer.InsertInt64(0L);
			}
			dataBuffer.InsertInt32(0);
			if (base.FileTime.HasValue)
			{
				dataBuffer.InsertInt64(base.FileTime.Value.ToFileTimeUtc());
			}
			else
			{
				dataBuffer.InsertInt64(0L);
			}
			dataBuffer.InsertCString(base.FileName);
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(base.Server, 6112);
			socket.Send(new byte[]
			{
				2
			});
			socket.Send(dataBuffer.GetData(), 0, dataBuffer.Count, SocketFlags.None);
			byte[] array = new byte[2];
			socket.Receive(array, 2, SocketFlags.None);
			int num = (int)BitConverter.ToInt16(array, 0);
			Trace.WriteLine(num, "Header Length");
			byte[] array2 = new byte[num - 2];
			socket.Receive(array2, num - 2, SocketFlags.None);
			DataReader dataReader = new DataReader(array2);
			dataReader.Seek(2);
			int num2 = dataReader.ReadInt32();
			base.FileSize = num2;
			dataReader.Seek(8);
			long fileTime = dataReader.ReadInt64();
			string strA = dataReader.ReadCString();
			if (string.Compare(strA, base.FileName, StringComparison.OrdinalIgnoreCase) != 0 || base.FileSize == 0)
			{
				throw new FileNotFoundException(Resources.bnftp_filenotfound);
			}
			Trace.WriteLine(num2, "File Size");
			byte[] buffer = this.ReceiveLoop(socket, num2);
			socket.Close();
			FileStream fileStream = new FileStream(base.LocalFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
			fileStream.SetLength((long)num2);
			fileStream.Write(buffer, 0, num2);
			fileStream.Flush();
			fileStream.Close();
			DateTime lastWriteTimeUtc = DateTime.FromFileTimeUtc(fileTime);
			File.SetLastWriteTimeUtc(base.LocalFileName, lastWriteTimeUtc);
		}

		private byte[] ReceiveLoop(Socket sck, int totalLength)
		{
			byte[] array = new byte[totalLength];
			int num = 0;
			using (NetworkStream networkStream = new NetworkStream(sck, false))
			{
				while (sck.Connected && num < totalLength)
				{
					int count = Math.Min(totalLength - num, 10240);
					int num2 = networkStream.Read(array, num, count);
					if (num2 == 0)
					{
						throw new SocketException();
					}
					num += num2;
					try
					{
						this.OnFilePartDownloaded(new DownloadStatusEventArgs(num, totalLength, base.FileName));
					}
					catch
					{
					}
				}
			}
			return array;
		}
	}
}

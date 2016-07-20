using System;
using System.IO;
using System.Net.Sockets;

namespace MBNCSUtil.Net
{
	public class BnFtpVersion2Request : BnFtpRequestBase
	{
		private int m_adId;

		private string m_adExt;

		private CdKey m_key;

		private bool m_ad;

		public BnFtpVersion2Request(string productId, string fileName, DateTime fileTime, string cdKey) : base(fileName, productId, new DateTime?(fileTime))
		{
			string product = this.Product;
			if (product != Resources.war3 && product != Resources.w3xp)
			{
				throw new ArgumentOutOfRangeException(Resources.param_productId, productId, Resources.bnftp_ver2invalidProduct);
			}
			this.m_key = new CdKey(cdKey);
		}

		public BnFtpVersion2Request(string fileName, string product, DateTime fileTime, string cdKey, int adId, string adFileExtension) : this(fileName, product, fileTime, cdKey)
		{
			this.m_ad = true;
			this.m_adId = adId;
			this.m_adExt = adFileExtension;
		}

		public override void ExecuteRequest()
		{
			DataBuffer dataBuffer = new DataBuffer();
			dataBuffer.InsertInt16(20);
			dataBuffer.InsertInt16(512);
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
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(base.Server, 6112);
			socket.Send(new byte[]
			{
				2
			});
			socket.Send(dataBuffer.GetData(), 0, dataBuffer.Count, SocketFlags.None);
			NetworkStream str = new NetworkStream(socket, false);
			DataReader dataReader = new DataReader(str, 4);
			int serverToken = dataReader.ReadInt32();
			DataBuffer dataBuffer2 = new DataBuffer();
			dataBuffer2.InsertInt32(0);
			if (base.FileTime.HasValue)
			{
				dataBuffer2.InsertInt64(base.FileTime.Value.ToFileTimeUtc());
			}
			else
			{
				dataBuffer2.InsertInt64(0L);
			}
			int num = new Random().Next();
			dataBuffer2.InsertInt32(num);
			dataBuffer2.InsertInt32(this.m_key.Key.Length);
			dataBuffer2.InsertInt32(this.m_key.Product);
			dataBuffer2.InsertInt32(this.m_key.Value1);
			dataBuffer2.InsertInt32(0);
			dataBuffer2.InsertByteArray(this.m_key.GetHash(num, serverToken));
			dataBuffer2.InsertCString(base.FileName);
			socket.Send(dataBuffer2.GetData(), 0, dataBuffer2.Count, SocketFlags.None);
			dataReader = new DataReader(str, 4);
			int length = dataReader.ReadInt32() - 4;
			dataReader = new DataReader(str, length);
			base.FileSize = dataReader.ReadInt32();
			dataReader.Seek(8);
			long fileTime = dataReader.ReadInt64();
			DateTime.FromFileTimeUtc(fileTime);
			string strA = dataReader.ReadCString();
			if (string.Compare(strA, base.FileName, StringComparison.OrdinalIgnoreCase) != 0 || base.FileSize == 0)
			{
				throw new FileNotFoundException(Resources.bnftp_filenotfound);
			}
			byte[] buffer = this.ReceiveLoop(socket, base.FileSize);
			socket.Close();
			FileStream fileStream = new FileStream(base.LocalFileName, FileMode.OpenOrCreate, FileAccess.Write);
			fileStream.Write(buffer, 0, base.FileSize);
			fileStream.Flush();
			fileStream.Close();
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

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MBNCSUtil
{
	[ComVisible(false)]
	public sealed class OldAuth
	{
		private OldAuth()
		{
		}

		public static byte[] HashData(byte[] data)
		{
			return XSha1.CalculateHash(data);
		}

		public static byte[] HashPassword(string data)
		{
			return OldAuth.HashData(Encoding.ASCII.GetBytes(data));
		}

		public static byte[] DoubleHashData(byte[] data, int clientToken, int serverToken)
		{
			return OldAuth.DoubleHashData(data, (uint)clientToken, (uint)serverToken);
		}

		public static byte[] DoubleHashPassword(string data, int clientToken, int serverToken)
		{
			return OldAuth.DoubleHashData(Encoding.ASCII.GetBytes(data), (uint)clientToken, (uint)serverToken);
		}

		[CLSCompliant(false)]
		public static byte[] DoubleHashPassword(string data, uint clientToken, uint serverToken)
		{
			return OldAuth.DoubleHashData(Encoding.ASCII.GetBytes(data), clientToken, serverToken);
		}

		[CLSCompliant(false)]
		public static byte[] DoubleHashData(byte[] data, uint clientToken, uint serverToken)
		{
			MemoryStream memoryStream = new MemoryStream(28);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			byte[] buffer = XSha1.CalculateHash(data);
			binaryWriter.Write(clientToken);
			binaryWriter.Write(serverToken);
			binaryWriter.Write(buffer);
			byte[] buffer2 = memoryStream.GetBuffer();
			return XSha1.CalculateHash(buffer2);
		}
	}
}

using MBNCSUtil.Util;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MBNCSUtil
{
	public sealed class NLS
	{
		public const string Modulus = "F8FF1A8B619918032186B68CA092B5557E976C78C73212D91216F6658523C787";

		public const int Generator = 47;

		public const int SignatureKey = 65537;

		public const string ServerModulus = "cf8d697fbac28db6fd9d54cc4140edc296785157e7bdf52db032d940668e16ea76348a8e6932844120d38a085e3df42a98dd00c2e4fc26fdf425d34d2dc582d020a606a1d577e1c973b8f3cb9e430788fc395a150b480f293556ba2dfcc1e5dcb556b58f0ecd3b3aa1b41942e820fab032e30b9d786efac30fc50d0fabd6a3d5 ";

		private static readonly SHA1 s_sha = new SHA1Managed();

		private static readonly RandomNumberGenerator s_rand = new RNGCryptoServiceProvider();

		private static readonly BigInteger s_modulus = new BigInteger("F8FF1A8B619918032186B68CA092B5557E976C78C73212D91216F6658523C787", 16);

		private static readonly BigInteger s_generator = new BigInteger(47uL);

		private string userName;

		private string password;

		private byte[] k;

		private byte[] userNameAscii;

		private BigInteger verifier;

		private BigInteger x;

		private BigInteger a;

		private BigInteger A;

		private BigInteger m1;

		public NLS(string Username, string Password)
		{
			this.userName = Username;
			this.userNameAscii = Encoding.ASCII.GetBytes(this.userName);
			this.password = Password;
			byte[] array = new byte[32];
			NLS.s_rand.GetNonZeroBytes(array);
			this.a = new BigInteger(array);
			this.a %= NLS.s_modulus;
			this.a = new BigInteger(NLS.ReverseArray(this.a.GetBytes()));
			this.A = new BigInteger(NLS.ReverseArray(NLS.s_generator.ModPow(this.a, NLS.s_modulus).GetBytes()));
		}

		public bool VerifyServerProof(byte[] serverProof)
		{
			if (serverProof.Length != 20)
			{
				throw new ArgumentOutOfRangeException(Resources.nlsServerProof20);
			}
			MemoryStream memoryStream = new MemoryStream(92);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(NLS.EnsureArrayLength(this.A.GetBytes(), 32));
			binaryWriter.Write(this.m1.GetBytes());
			binaryWriter.Write(this.k);
			byte[] buffer = memoryStream.GetBuffer();
			memoryStream.Close();
			byte[] inData = NLS.s_sha.ComputeHash(buffer);
			BigInteger bigInteger = new BigInteger(inData);
			BigInteger obj = new BigInteger(serverProof);
			return bigInteger.Equals(obj);
		}

		public int LoginProof(Stream stream, byte[] serverSalt, byte[] serverRandomKey)
		{
			if (serverSalt.Length != 32)
			{
				throw new ArgumentOutOfRangeException(Resources.param_salt, serverSalt, Resources.nlsSalt32);
			}
			if (serverRandomKey.Length != 32)
			{
				throw new ArgumentOutOfRangeException(Resources.param_serverKey, serverRandomKey, Resources.nlsServerKey32);
			}
			if (stream.Position + 20L > stream.Length)
			{
				throw new IOException(Resources.nlsLoginProofSpace);
			}
			this.CalculateM1(serverSalt, serverRandomKey);
			stream.Write(NLS.EnsureArrayLength(this.m1.GetBytes(), 20), 0, 20);
			return 20;
		}

		public int LoginProof(byte[] buffer, int startIndex, int totalLength, byte[] serverSalt, byte[] serverKey)
		{
			MemoryStream stream = new MemoryStream(buffer, startIndex, totalLength, true);
			return this.LoginProof(stream, serverSalt, serverKey);
		}

		public int LoginProof(DataBuffer logonProofPacket, byte[] serverSalt, byte[] serverKey)
		{
			byte[] array = new byte[20];
			int result = this.LoginProof(array, 0, 20, serverSalt, serverKey);
			logonProofPacket.Insert(array);
			return result;
		}

		public int LoginAccount(Stream stream)
		{
			if (stream.Position + 33L + (long)this.userNameAscii.Length > stream.Length)
			{
				throw new IOException(Resources.nlsAcctLoginSpace);
			}
			stream.Write(NLS.EnsureArrayLength(this.A.GetBytes(), 32), 0, 32);
			stream.Write(this.userNameAscii, 0, this.userNameAscii.Length);
			stream.WriteByte(0);
			return 33 + this.userNameAscii.Length;
		}

		public int LoginAccount(DataBuffer loginPacket)
		{
			byte[] array = new byte[33 + this.userNameAscii.Length];
			int result = this.LoginAccount(array, 0, array.Length);
			loginPacket.Insert(array);
			return result;
		}

		public int LoginAccount(byte[] buffer, int startIndex, int totalLength)
		{
			MemoryStream stream = new MemoryStream(buffer, startIndex, totalLength, true);
			return this.LoginAccount(stream);
		}

		public int CreateAccount(Stream stream)
		{
			if (stream.Position + 65L + (long)this.userNameAscii.Length > stream.Length)
			{
				throw new IOException(Resources.nlsAcctCreateSpace);
			}
			byte[] array = new byte[32];
			NLS.s_rand.GetNonZeroBytes(array);
			this.CalculateVerifier(array);
			stream.Write(NLS.EnsureArrayLength(array, 32), 0, 32);
			stream.Write(NLS.ReverseArray(NLS.EnsureArrayLength(this.verifier.GetBytes(), 32)), 0, 32);
			stream.Write(this.userNameAscii, 0, this.userNameAscii.Length);
			stream.WriteByte(0);
			return 65 + this.userNameAscii.Length;
		}

		public int CreateAccount(DataBuffer acctPacket)
		{
			byte[] array = new byte[65 + this.userName.Length];
			int result = this.CreateAccount(array, 0, array.Length);
			acctPacket.InsertByteArray(array);
			return result;
		}

		public int CreateAccount(byte[] buffer, int startIndex, int totalLength)
		{
			MemoryStream stream = new MemoryStream(buffer, startIndex, totalLength, true);
			return this.CreateAccount(stream);
		}

		public static bool ValidateServerSignature(byte[] serverSignature, byte[] ipAddress)
		{
			if (serverSignature.Length != 128)
			{
				throw new ArgumentOutOfRangeException(Resources.nlsSrvSig128);
			}
			BigInteger exp = new BigInteger(new byte[]
			{
				0,
				1,
				0,
				1
			});
			BigInteger n = new BigInteger("cf8d697fbac28db6fd9d54cc4140edc296785157e7bdf52db032d940668e16ea76348a8e6932844120d38a085e3df42a98dd00c2e4fc26fdf425d34d2dc582d020a606a1d577e1c973b8f3cb9e430788fc395a150b480f293556ba2dfcc1e5dcb556b58f0ecd3b3aa1b41942e820fab032e30b9d786efac30fc50d0fabd6a3d5 ", 16);
			BigInteger bigInteger = new BigInteger(NLS.ReverseArray(serverSignature));
			byte[] bytes = bigInteger.ModPow(exp, n).GetBytes();
			BigInteger obj = new BigInteger(NLS.ReverseArray(bytes));
			MemoryStream memoryStream = new MemoryStream(bytes.Length);
			memoryStream.Write(ipAddress, 0, 4);
			for (int i = 4; i < bytes.Length; i++)
			{
				memoryStream.WriteByte(187);
			}
			memoryStream.Seek(-1L, SeekOrigin.Current);
			memoryStream.WriteByte(11);
			BigInteger bigInteger2 = new BigInteger(memoryStream.GetBuffer());
			memoryStream.Close();
			return bigInteger2.Equals(obj);
		}

		private void CalculateVerifier(byte[] serverSalt)
		{
			string s = this.userName.ToUpper(CultureInfo.InvariantCulture) + ":" + this.password.ToUpper(CultureInfo.InvariantCulture);
			byte[] bytes = Encoding.ASCII.GetBytes(s);
			byte[] array = NLS.s_sha.ComputeHash(bytes);
			byte[] array2 = new byte[serverSalt.Length + array.Length];
			Array.Copy(serverSalt, array2, serverSalt.Length);
			Array.Copy(array, 0, array2, serverSalt.Length, array.Length);
			byte[] array3 = NLS.s_sha.ComputeHash(array2);
			lock (this)
			{
				this.x = new BigInteger(NLS.ReverseArray(array3));
				this.verifier = NLS.s_generator.ModPow(this.x, NLS.s_modulus);
			}
		}

		private void CalculateM1(byte[] saltFromServer, byte[] issuedServerKey)
		{
			BigInteger bi = new BigInteger(NLS.ReverseArray(issuedServerKey));
			byte[] inData = NLS.s_sha.ComputeHash(issuedServerKey);
			BigInteger bi2 = new BigInteger(inData, 4);
			if (this.verifier == null)
			{
				this.CalculateVerifier(saltFromServer);
			}
			BigInteger bigInteger = (NLS.s_modulus + bi - this.verifier) % NLS.s_modulus;
			bigInteger = bigInteger.ModPow(this.a + bi2 * this.x, NLS.s_modulus);
			byte[] array = NLS.EnsureArrayLength(NLS.ReverseArray(bigInteger.GetBytes()), 32);
			byte[] array2 = new byte[16];
			byte[] array3 = new byte[16];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				array2[num] = array[i];
				array3[num] = array[i + 1];
				i += 2;
				num++;
			}
			byte[] array4 = NLS.s_sha.ComputeHash(array2);
			byte[] array5 = NLS.s_sha.ComputeHash(array3);
			byte[] array6 = new byte[40];
			for (int j = 0; j < array6.Length; j++)
			{
				if ((j & 1) == 0)
				{
					array6[j] = array4[j / 2];
				}
				else
				{
					array6[j] = array5[j / 2];
				}
			}
			BigInteger bi3 = new BigInteger(NLS.s_sha.ComputeHash(NLS.ReverseArray(NLS.s_generator.GetBytes())));
			BigInteger bi4 = new BigInteger(NLS.s_sha.ComputeHash(NLS.ReverseArray(NLS.s_modulus.GetBytes())));
			BigInteger bigInteger2 = bi3 ^ bi4;
			MemoryStream memoryStream = new MemoryStream(40 + saltFromServer.Length + this.A.GetBytes().Length + issuedServerKey.Length + array6.Length);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(bigInteger2.GetBytes());
			binaryWriter.Write(NLS.s_sha.ComputeHash(Encoding.ASCII.GetBytes(this.userName.ToUpper(CultureInfo.InvariantCulture))));
			binaryWriter.Write(saltFromServer);
			binaryWriter.Write(NLS.EnsureArrayLength(this.A.GetBytes(), 32));
			binaryWriter.Write(issuedServerKey);
			binaryWriter.Write(array6);
			byte[] buffer = memoryStream.GetBuffer();
			memoryStream.Close();
			byte[] inData2 = NLS.s_sha.ComputeHash(buffer);
			lock (this)
			{
				this.k = array6;
				this.m1 = new BigInteger(inData2);
			}
		}

		private static byte[] ReverseArray(byte[] array)
		{
			byte[] array2 = new byte[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[array.Length - 1 - i];
			}
			return array2;
		}

		private static byte[] EnsureArrayLength(byte[] array, int minSize)
		{
			if (array.Length < minSize)
			{
				byte[] array2 = new byte[minSize];
				Buffer.BlockCopy(array, 0, array2, minSize - array.Length, array.Length);
				array = array2;
			}
			return array;
		}
	}
}

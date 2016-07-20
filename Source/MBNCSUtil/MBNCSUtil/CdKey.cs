using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace MBNCSUtil
{
	public sealed class CdKey
	{
		private const uint W3_KEYLEN = 26u;

		private const uint W3_BUFLEN = 52u;

		private string key;

		private uint product;

		private uint val1;

		private byte[] val2;

		private byte[] hash;

		private bool valid;

		private static readonly byte[] w2Map = new byte[]
		{
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			0,
			255,
			1,
			255,
			2,
			3,
			4,
			5,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			255,
			13,
			14,
			255,
			15,
			16,
			255,
			17,
			255,
			18,
			255,
			19,
			255,
			20,
			21,
			22,
			255,
			23,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			255,
			13,
			14,
			255,
			15,
			16,
			255,
			17,
			255,
			18,
			255,
			19,
			255,
			20,
			21,
			22,
			255,
			23,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255
		};

		private static readonly byte[] w3KeyMap = new byte[]
		{
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			0,
			255,
			1,
			255,
			2,
			3,
			4,
			5,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			255,
			13,
			14,
			255,
			15,
			16,
			255,
			17,
			255,
			18,
			255,
			19,
			255,
			20,
			21,
			22,
			23,
			24,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			255,
			13,
			14,
			255,
			15,
			16,
			255,
			17,
			255,
			18,
			255,
			19,
			255,
			20,
			21,
			22,
			23,
			24,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255,
			255
		};

		private static readonly byte[] w3TranslateMap = new byte[]
		{
			9,
			4,
			7,
			15,
			13,
			10,
			3,
			11,
			1,
			2,
			12,
			8,
			6,
			14,
			5,
			0,
			9,
			11,
			5,
			4,
			8,
			15,
			1,
			14,
			7,
			0,
			3,
			2,
			10,
			6,
			13,
			12,
			12,
			14,
			1,
			4,
			9,
			15,
			10,
			11,
			13,
			6,
			0,
			8,
			7,
			2,
			5,
			3,
			11,
			2,
			5,
			14,
			13,
			3,
			9,
			0,
			1,
			15,
			7,
			12,
			10,
			6,
			4,
			8,
			6,
			2,
			4,
			5,
			11,
			8,
			12,
			14,
			13,
			15,
			7,
			1,
			10,
			0,
			3,
			9,
			5,
			4,
			14,
			12,
			7,
			6,
			13,
			10,
			15,
			2,
			9,
			1,
			0,
			11,
			8,
			3,
			12,
			7,
			8,
			15,
			11,
			0,
			5,
			9,
			13,
			10,
			6,
			14,
			2,
			4,
			3,
			1,
			3,
			10,
			14,
			8,
			1,
			11,
			5,
			4,
			2,
			15,
			13,
			12,
			6,
			7,
			9,
			0,
			12,
			13,
			1,
			15,
			8,
			14,
			5,
			11,
			3,
			10,
			9,
			0,
			7,
			2,
			4,
			6,
			13,
			10,
			7,
			14,
			1,
			6,
			11,
			8,
			15,
			12,
			5,
			2,
			3,
			0,
			4,
			9,
			3,
			14,
			7,
			5,
			11,
			15,
			8,
			12,
			1,
			10,
			4,
			13,
			0,
			6,
			9,
			2,
			11,
			6,
			9,
			4,
			1,
			8,
			10,
			13,
			7,
			14,
			0,
			12,
			15,
			2,
			3,
			5,
			12,
			7,
			8,
			13,
			3,
			11,
			0,
			14,
			6,
			15,
			9,
			4,
			10,
			1,
			5,
			2,
			12,
			6,
			13,
			9,
			11,
			0,
			1,
			2,
			15,
			7,
			3,
			4,
			10,
			14,
			8,
			5,
			3,
			6,
			1,
			5,
			11,
			12,
			8,
			0,
			15,
			14,
			9,
			4,
			7,
			10,
			13,
			2,
			10,
			7,
			11,
			15,
			2,
			8,
			0,
			13,
			14,
			12,
			1,
			6,
			9,
			3,
			5,
			4,
			10,
			11,
			13,
			4,
			3,
			8,
			5,
			9,
			1,
			0,
			15,
			12,
			7,
			14,
			2,
			6,
			11,
			4,
			13,
			15,
			1,
			6,
			3,
			14,
			7,
			10,
			12,
			8,
			9,
			2,
			5,
			0,
			9,
			6,
			7,
			0,
			1,
			10,
			13,
			2,
			3,
			14,
			15,
			12,
			5,
			11,
			4,
			8,
			13,
			14,
			5,
			6,
			1,
			9,
			8,
			12,
			2,
			15,
			3,
			7,
			11,
			4,
			0,
			10,
			9,
			15,
			4,
			0,
			1,
			6,
			10,
			14,
			2,
			3,
			7,
			13,
			5,
			11,
			8,
			12,
			3,
			14,
			1,
			10,
			2,
			12,
			8,
			4,
			11,
			7,
			13,
			0,
			15,
			6,
			9,
			5,
			7,
			2,
			12,
			6,
			10,
			8,
			11,
			0,
			15,
			4,
			3,
			14,
			9,
			1,
			13,
			5,
			12,
			4,
			5,
			9,
			10,
			2,
			8,
			13,
			3,
			15,
			1,
			14,
			6,
			7,
			11,
			0,
			10,
			8,
			14,
			13,
			9,
			15,
			3,
			0,
			4,
			6,
			1,
			12,
			7,
			11,
			2,
			5,
			3,
			12,
			4,
			10,
			2,
			15,
			13,
			14,
			7,
			0,
			5,
			8,
			1,
			6,
			11,
			9,
			10,
			12,
			1,
			0,
			9,
			14,
			13,
			11,
			3,
			7,
			15,
			8,
			5,
			2,
			4,
			6,
			14,
			10,
			1,
			8,
			7,
			6,
			5,
			12,
			2,
			15,
			0,
			13,
			3,
			11,
			4,
			9,
			3,
			8,
			14,
			0,
			7,
			9,
			15,
			12,
			1,
			6,
			13,
			2,
			5,
			10,
			11,
			4,
			3,
			10,
			12,
			4,
			13,
			11,
			9,
			14,
			15,
			6,
			1,
			7,
			2,
			0,
			5,
			8
		};

		public string Key
		{
			get
			{
				return this.key;
			}
		}

		public int Product
		{
			get
			{
				return (int)this.product;
			}
		}

		public int Value1
		{
			get
			{
				return (int)this.val1;
			}
		}

		[Obsolete("This property will be removed in a future version.  Consider changing to GetValue2().")]
		public byte[] Value2
		{
			get
			{
				byte[] array = new byte[this.val2.Length];
				Buffer.BlockCopy(this.val2, 0, array, 0, array.Length);
				return array;
			}
		}

		public bool IsValid
		{
			get
			{
				return this.valid;
			}
		}

		public CdKey(string cdKey)
		{
			this.InitializePrivate(cdKey);
		}

		private void InitializePrivate(string cdKey)
		{
			if (cdKey == null)
			{
				throw new ArgumentNullException(Resources.param_cdKey, Resources.cdKeyArgNull);
			}
			this.key = cdKey;
			int length = cdKey.Length;
			if (length == 13)
			{
				for (int i = 0; i < 13; i++)
				{
					if (!char.IsDigit(cdKey, i))
					{
						throw new ArgumentOutOfRangeException(Resources.param_cdKey, cdKey, Resources.invalidCdKeySc);
					}
				}
				this.procSCKey();
				return;
			}
			if (length == 16)
			{
				for (int j = 0; j < 16; j++)
				{
					if (!char.IsLetterOrDigit(cdKey, j))
					{
						throw new ArgumentOutOfRangeException(Resources.param_cdKey, cdKey, Resources.invalidCdKeyWar2);
					}
				}
				this.procW2Key();
				return;
			}
			if (length != 26)
			{
				throw new ArgumentOutOfRangeException(Resources.param_cdKey, cdKey, Resources.invalidCdKeyGeneral);
			}
			for (int k = 0; k < 26; k++)
			{
				if (!char.IsLetterOrDigit(cdKey, k))
				{
					throw new ArgumentOutOfRangeException(Resources.param_cdKey, cdKey, Resources.invalidCdKeyWar3);
				}
			}
			this.procW3Key();
		}

		public static CdKey CreateDecoder(string key)
		{
			return new CdKey(key);
		}

		public byte[] GetValue2()
		{
			byte[] array = new byte[this.val2.Length];
			Buffer.BlockCopy(this.val2, 0, array, 0, array.Length);
			return array;
		}

		public byte[] GetHash(int clientToken, int serverToken)
		{
			return this.GetHash((uint)clientToken, (uint)serverToken);
		}

		[CLSCompliant(false)]
		public byte[] GetHash(uint clientToken, uint serverToken)
		{
			lock (this)
			{
				if (this.hash == null)
				{
					this.calculateHash(clientToken, serverToken);
				}
			}
			return this.hash;
		}

		private void procSCKey()
		{
			uint num = 330078017u;
			uint num2 = 3u;
			for (int i = 0; i < 12; i++)
			{
				num2 += ((uint)(this.key[i] - '0') ^ num2 * 2u);
			}
			if ((ulong)(num2 % 10u) != (ulong)((long)(this.key[12] - '0')))
			{
				this.valid = false;
				return;
			}
			int num3 = 11;
			char[] array = this.key.ToCharArray();
			for (int j = 194; j >= 7; j -= 17)
			{
				char c = array[num3];
				array[num3] = array[j % 12];
				array[j % 12] = c;
				num3--;
			}
			for (int k = this.key.Length - 2; k >= 0; k--)
			{
				char c = char.ToUpper(array[k], CultureInfo.InvariantCulture);
				array[k] = c;
				if (c <= '7')
				{
					char[] expr_CB_cp_0 = array;
					int expr_CB_cp_1 = k;
					expr_CB_cp_0[expr_CB_cp_1] ^= (char)(num & 7u);
					num >>= 3;
				}
				else if (c <= 'A')
				{
					char[] expr_F1_cp_0 = array;
					int expr_F1_cp_1 = k;
					expr_F1_cp_0[expr_F1_cp_1] ^= (char)(k & 1);
				}
			}
			this.key = new string(array);
			this.product = uint.Parse(this.key.Substring(0, 2), CultureInfo.InvariantCulture);
			this.val1 = uint.Parse(this.key.Substring(2, 7), CultureInfo.InvariantCulture);
			this.val2 = BitConverter.GetBytes(uint.Parse(this.key.Substring(9, 3), CultureInfo.InvariantCulture));
			this.valid = true;
		}

		private void procW2Key()
		{
			char[] array = this.key.ToCharArray();
			uint num = 1u;
			uint num2 = 0u;
			for (int i = 0; i < 16; i += 2)
			{
				byte b = CdKey.w2Map[(int)this.key[i]];
				uint num3 = (uint)(b * 3);
				byte b2 = CdKey.w2Map[(int)array[i + 1]];
				num3 = (uint)b2 + num3 * 8u;
				if (num3 >= 256u)
				{
					num3 -= 256u;
					num2 |= num;
				}
				uint num4 = num3 >> 4;
				array[i] = (num4 % 16u).ToString("x", CultureInfo.InvariantCulture)[0];
				array[i + 1] = (num3 % 16u).ToString("x", CultureInfo.InvariantCulture)[0];
				num <<= 1;
			}
			uint num5 = 3u;
			for (int j = 0; j < 16; j++)
			{
				byte b = (byte)(array[j] & 'ÿ');
				uint num3 = CdKey.getNumValue((char)b);
				uint num4 = num5 * 2u;
				num3 ^= num4;
				num5 += num3;
			}
			num5 &= 255u;
			if (num5 != num2)
			{
				this.valid = false;
				return;
			}
			this.valid = true;
			for (int k = 15; k >= 0; k--)
			{
				byte b = (byte)array[k];
				uint num3;
				if (k > 8)
				{
					num3 = (uint)(k - 9);
				}
				else
				{
					num3 = (uint)(15 + k - 8);
				}
				num3 &= 15u;
				byte b2 = (byte)array[(int)((UIntPtr)num3)];
				array[k] = (char)b2;
				array[(int)((UIntPtr)num3)] = (char)b;
			}
			uint num6 = 330078017u;
			for (int l = 15; l >= 0; l--)
			{
				array[l] = char.ToUpper(array[l], CultureInfo.InvariantCulture);
				byte b = (byte)array[l];
				if (b <= 55)
				{
					num5 = num6;
					byte b2 = ((byte)(num5 & 255u) & 7) ^ b;
					num5 >>= 3;
					array[l] = (char)b2;
					num6 = num5;
				}
				else if (b < 65)
				{
					array[l] = (char)(((ushort)l & 1) ^ (ushort)b);
				}
			}
			this.key = new string(array);
			this.product = uint.Parse(this.key.Substring(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.val1 = uint.Parse(this.key.Substring(2, 6), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
			this.val2 = BitConverter.GetBytes(uint.Parse(this.key.Substring(8, 8), NumberStyles.HexNumber, CultureInfo.InvariantCulture));
		}

		private static uint getNumValue(char c)
		{
			char c2 = char.ToUpper(c, CultureInfo.InvariantCulture);
			if (!char.IsDigit(c2))
			{
				return (uint)(c2 - '7');
			}
			return (uint)(c2 - '0');
		}

		private unsafe void procW3Key()
		{
			char[] array = this.key.ToUpper(CultureInfo.InvariantCulture).ToCharArray();
			byte[] array2 = new byte[52];
			uint[] array3 = new uint[4];
			uint num = 33u;
			int i;
			for (i = 0; i < this.key.Length; i++)
			{
				uint num2 = (num + 1973u) % 52u;
				num = (num2 + 1973u) % 52u;
				byte b = CdKey.w3KeyMap[(int)array[i]];
				array2[(int)((UIntPtr)num2)] = b / 5;
				array2[(int)((UIntPtr)num)] = b % 5;
			}
			i = 52;
			fixed (uint* ptr = &array3[0])
			{
				do
				{
					CdKey.mult(4, 5, ptr + 3, (uint)array2[i - 1]);
				}
				while (--i != 0);
			}
			CdKey.decodeKeyTable(array3);
			this.product = array3[0] >> 10;
			for (i = 0; i < 4; i++)
			{
				array3[i] = CdKey.SWAP4(array3[i]);
			}
			MemoryStream memoryStream = new MemoryStream(16);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			binaryWriter.Write(array3[0]);
			binaryWriter.Write(array3[1]);
			binaryWriter.Write(array3[2]);
			binaryWriter.Write(array3[3]);
			memoryStream.Seek(2L, SeekOrigin.Begin);
			this.val1 = CdKey.SWAP4(binaryReader.ReadUInt32() & 4294967040u);
			this.val2 = new byte[10];
			MemoryStream output = new MemoryStream(this.val2, true);
			BinaryWriter binaryWriter2 = new BinaryWriter(output);
			binaryWriter2.Write(CdKey.SWAP2(binaryReader.ReadUInt16()));
			binaryWriter2.Write(CdKey.SWAP4(binaryReader.ReadUInt32()));
			binaryWriter2.Write(CdKey.SWAP4(binaryReader.ReadUInt32()));
			memoryStream.Close();
			this.valid = true;
		}

		private unsafe static void mult(int r, int x, uint* a, uint dcByte)
		{
			while (r-- != 0)
			{
				ulong num = (ulong)(*a & 4294967295u) * (ulong)((long)x & (long)((ulong)-1));
				*(a--) = dcByte + (uint)num;
				dcByte = (uint)(num >> 32);
			}
		}

		private static void decodeKeyTable(uint[] keyTable)
		{
			uint[] array = new uint[4];
			uint num = 29u;
			int i = 464;
			uint num2;
			do
			{
				num2 = (num & 7u) << 2;
				uint num3 = num >> 3;
				uint num4 = keyTable[(int)((UIntPtr)(3u - num3))];
				num4 &= 15u << (int)num2;
				num4 >>= (int)num2;
				int j;
				if (i < 464)
				{
					j = 29;
					while ((long)j > (long)((ulong)num))
					{
						uint num5 = (uint)((uint)(j & 7) << 2);
						uint num6 = keyTable[3 - (j >> 3)];
						num6 &= 15u << (int)num5;
						num6 >>= (int)num5;
						num4 = (uint)CdKey.w3TranslateMap[(int)(checked((IntPtr)(unchecked((ulong)num6 ^ (ulong)((long)((int)CdKey.w3TranslateMap[(int)(checked((IntPtr)(unchecked((ulong)num4 + (ulong)((long)i)))))] + i))))))];
						j--;
					}
				}
				num = (uint)(j = (int)(num - 1u));
				while (j >= 0)
				{
					uint num5 = (uint)((long)j & 7L) << 2;
					uint num6 = keyTable[3 - (j >> 3)];
					num6 &= 15u << (int)num5;
					num6 >>= (int)num5;
					num4 = (uint)CdKey.w3TranslateMap[(int)(checked((IntPtr)(unchecked((ulong)num6 ^ (ulong)((long)((int)CdKey.w3TranslateMap[(int)(checked((IntPtr)(unchecked((ulong)num4 + (ulong)((long)i)))))] + i))))))];
					j--;
				}
				j = (int)(3u - num3);
				uint num7 = (uint)((uint)(CdKey.w3TranslateMap[(int)(checked((IntPtr)(unchecked((ulong)num4 + (ulong)((long)i)))))] & 15) << (int)num2);
				keyTable[j] = (num7 | (~(15u << (int)num2) & keyTable[j]));
			}
			while ((i -= 16) >= 0);
			num2 = 0u;
			for (i = 0; i < 4; i++)
			{
				array[i] = keyTable[i];
			}
			for (uint num8 = 0u; num8 < 120u; num8 += 1u)
			{
				uint num9 = 12u;
				uint num10 = num8 & 31u;
				uint num5 = num2 & 31u;
				uint num11 = 3u - (num8 >> 5);
				num9 -= num2 >> 5 << 2;
				num9 /= 4u;
				uint num6 = array[(int)((UIntPtr)num9)];
				num6 &= 1u << (int)num5;
				num6 >>= (int)num5;
				uint num12 = keyTable[(int)((UIntPtr)num11)];
				keyTable[(int)((UIntPtr)num11)] = (num6 & 1u);
				keyTable[(int)((UIntPtr)num11)] <<= (int)num10;
				keyTable[(int)((UIntPtr)num11)] |= (~(1u << (int)num10) & num12);
				num2 += 11u;
				if (num2 >= 120u)
				{
					num2 -= 120u;
				}
			}
		}

		private static uint SWAP4(uint num)
		{
			return (num >> 24 & 255u) | (num >> 8 & 65280u) | (num << 8 & 16711680u) | (num << 24 & 4278190080u);
		}

		private static ushort SWAP2(ushort num)
		{
			return (ushort)((num >> 8 & 255) | ((int)num << 8 & 65280));
		}

		private void calculateHash(uint clientToken, uint serverToken)
		{
			if (!this.valid)
			{
				throw new InvalidOperationException(Resources.invalidCdKeyHashed);
			}
			MemoryStream memoryStream = new MemoryStream(26);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(clientToken);
			binaryWriter.Write(serverToken);
			int length = this.key.Length;
			if (length != 13 && length != 16)
			{
				if (length == 26)
				{
					binaryWriter.Write(this.product);
					binaryWriter.Write(this.val1);
					binaryWriter.Write(this.val2);
					byte[] buffer = memoryStream.GetBuffer();
					SHA1 sHA = new SHA1Managed();
					this.hash = sHA.ComputeHash(buffer);
				}
			}
			else
			{
				binaryWriter.Write(this.product);
				binaryWriter.Write(this.val1);
				binaryWriter.Write(0);
				binaryWriter.Write(this.val2);
				binaryWriter.Write(0);
				this.hash = XSha1.CalculateHash(memoryStream.GetBuffer());
			}
			memoryStream.Close();
		}
	}
}

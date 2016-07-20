using System;
using System.IO;

namespace MBNCSUtil.Util
{
	internal static class LockdownSha1
	{
		internal class Context
		{
			public int[] bitlen = new int[2];

			public int[] state = new int[32];
		}

		private static readonly byte[] MysteryBuffer;

		private static uint ROTL32(uint value, int shift)
		{
			return value << shift | value >> 32 - (shift & 31);
		}

		private static int ROTL32(int value, int shift)
		{
			return (int)LockdownSha1.ROTL32((uint)value, shift);
		}

		private unsafe static void Tweedle(ref int rotater, int bitwise, int bitwise2, int bitwise3, int* adder, ref int result)
		{
			result += LockdownSha1.ROTL32(bitwise3, 5) + ((~rotater & bitwise2) | (rotater & bitwise)) + *adder + 1518500249;
			*adder = 0;
			rotater = LockdownSha1.ROTL32(rotater, 30);
		}

		private unsafe static void Twitter(ref int rotater, int bitwise, int rotater2, int bitwise2, int* rotater3, ref int result)
		{
			result += (((bitwise2 | bitwise) & rotater) | (bitwise2 & bitwise)) + (LockdownSha1.ROTL32(rotater2, 5) + *rotater3) - 1894007588;
			*rotater3 = 0;
			rotater = LockdownSha1.ROTL32(rotater, 30);
		}

		private unsafe static void Sha1Transform(int* data, int* state)
		{
			int* ptr = stackalloc int[4 * 80 / 4];
			Native.Memcpy((void*)ptr, (void*)data, 64);
			for (int i = 0; i < 64; i++)
			{
				ptr[i + 16] = LockdownSha1.ROTL32(ptr[i + 13] ^ ptr[i + 8] ^ ptr[i] ^ ptr[i + 2], 1);
				if (ptr[i + 16] == -1)
				{
					ptr[i + 16] = (int)LockdownSha1.ROTL32((uint)(ptr[i + 13] ^ ptr[i + 8] ^ ptr[i] ^ ptr[i + 2]), 1);
				}
			}
			int num = *state;
			int num2 = state[1];
			int num3 = state[2];
			int num4 = state[3];
			int num5 = state[4];
			for (int i = 0; i < 20; i += 5)
			{
				LockdownSha1.Tweedle(ref num2, num3, num4, num, ptr + i, ref num5);
				LockdownSha1.Tweedle(ref num, num2, num3, num5, ptr + (1 + i), ref num4);
				LockdownSha1.Tweedle(ref num5, num, num2, num4, ptr + (2 + i), ref num3);
				LockdownSha1.Tweedle(ref num4, num5, num, num3, ptr + (3 + i), ref num2);
				LockdownSha1.Tweedle(ref num3, num4, num5, num2, ptr + (4 + i), ref num);
			}
			int num6 = num;
			int num7 = num4;
			for (int i = 20; i < 40; i += 5)
			{
				int num8 = ptr[i] + LockdownSha1.ROTL32(num6, 5) + (num7 ^ num3 ^ num2);
				num7 = num7 + LockdownSha1.ROTL32(num8 + num5 + 1859775393, 5) + (num3 ^ LockdownSha1.ROTL32(num2, 30) ^ num6) + ptr[i + 1] + 1859775393;
				num3 = num3 + LockdownSha1.ROTL32(num7, 5) + (num8 + num5 + 1859775393 ^ LockdownSha1.ROTL32(num2, 30) ^ LockdownSha1.ROTL32(num6, 30)) + ptr[i + 2] + 1859775393;
				num5 = LockdownSha1.ROTL32(num8 + num5 + 1859775393, 30);
				num2 = LockdownSha1.ROTL32(num2, 30) + LockdownSha1.ROTL32(num3, 5) + (num5 ^ num7 ^ LockdownSha1.ROTL32(num6, 30)) + ptr[i + 3] + 1859775393;
				num7 = LockdownSha1.ROTL32(num7, 30);
				num6 = LockdownSha1.ROTL32(num6, 30) + LockdownSha1.ROTL32(num2, 5) + (num5 ^ num7 ^ num3) + ptr[i + 4] + 1859775393;
				num3 = LockdownSha1.ROTL32(num3, 30);
				Native.Memset((void*)ptr, 0, 20);
			}
			num = num6;
			num4 = num7;
			for (int i = 40; i < 60; i += 5)
			{
				LockdownSha1.Twitter(ref num2, num4, num, num3, ptr + i, ref num5);
				LockdownSha1.Twitter(ref num, num3, num5, num2, ptr + (i + 1), ref num4);
				LockdownSha1.Twitter(ref num5, num2, num4, num, ptr + (i + 2), ref num3);
				LockdownSha1.Twitter(ref num4, num, num3, num5, ptr + (i + 3), ref num2);
				LockdownSha1.Twitter(ref num3, num5, num2, num4, ptr + (i + 4), ref num);
			}
			int num9 = num;
			num7 = num4;
			for (int i = 60; i < 80; i += 5)
			{
				int num8 = LockdownSha1.ROTL32(num9, 5) + (num7 ^ num3 ^ num2) + ptr[i] + num5 - 899497514;
				num2 = LockdownSha1.ROTL32(num2, 30);
				num5 = num8;
				num7 = (num3 ^ num2 ^ num9) + ptr[i + 1] + num7 + LockdownSha1.ROTL32(num8, 5) - 899497514;
				num9 = LockdownSha1.ROTL32(num9, 30);
				num8 = LockdownSha1.ROTL32(num7, 5);
				num8 = (num5 ^ num2 ^ num9) + ptr[i + 2] + num3 + num8 - 899497514;
				num5 = LockdownSha1.ROTL32(num5, 30);
				num3 = num8;
				num8 = LockdownSha1.ROTL32(num8, 5) + (num5 ^ num7 ^ num9) + ptr[i + 3] + num2 - 899497514;
				num7 = LockdownSha1.ROTL32(num7, 30);
				int num10 = (num5 ^ num7 ^ num3) + ptr[i + 4];
				num2 = num8;
				num8 = LockdownSha1.ROTL32(num8, 5);
				num3 = LockdownSha1.ROTL32(num3, 30);
				num9 = num10 + num9 + num8 - 899497514;
				Native.Memset((void*)ptr, 0, 20);
			}
			*state += num9;
			state[1] = state[1] + num2;
			state[2] = state[2] + num3;
			state[3] = state[3] + num7;
			state[4] = state[4] + num5;
		}

		public static LockdownSha1.Context Init()
		{
			LockdownSha1.Context context = new LockdownSha1.Context();
			context.state[0] = 1732584193;
			context.state[1] = -271733879;
			context.state[2] = -1732584194;
			context.state[3] = 271733878;
			context.state[4] = -1009589776;
			return context;
		}

		public unsafe static void Update(LockdownSha1.Context ctx, byte[] data, int len)
		{
			fixed (byte* ptr = data)
			{
				LockdownSha1.Update(ctx, ptr, len);
			}
		}

		public unsafe static void Update(LockdownSha1.Context ctx, byte* data, int len)
		{
			fixed (int* bitlen = ctx.bitlen)
			{
				fixed (int* state = ctx.state)
				{
					byte* ptr = (byte*)state;
					int num = len >> 29;
					int num2 = len << 3;
					int i = *bitlen / 8 & 63;
					if (*bitlen + num2 < *bitlen || *bitlen + num2 < num2)
					{
						bitlen[1]++;
					}
					*bitlen += num2;
					bitlen[1] = bitlen[1] + num;
					len += i;
					data -= i;
					if (len >= 64)
					{
						if (i != 0)
						{
							while (i < 64)
							{
								ptr[20 + i] = data[i];
								i++;
							}
							LockdownSha1.Sha1Transform((int*)(ptr + 20), (int*)ptr);
							len -= 64;
							data += 64;
							i = 0;
						}
						if (len >= 64)
						{
							num2 = len;
							for (int j = 0; j < num2 / 64; j++)
							{
								LockdownSha1.Sha1Transform((int*)data, (int*)ptr);
								len -= 64;
								data += 64;
							}
						}
					}
					while (i < len)
					{
						ptr[i + 28 - 8] = data[i];
						i++;
					}
				}
			}
		}

		public unsafe static void Final(LockdownSha1.Context ctx, out byte[] hash)
		{
			int* ptr = stackalloc int[4 * 2 / 4];
			*ptr = ctx.bitlen[0];
			ptr[1] = ctx.bitlen[1];
			LockdownSha1.Update(ctx, LockdownSha1.MysteryBuffer, (-((ctx.bitlen[0] >> 3 | ctx.bitlen[1] << 29) + 9) & 63) + 1);
			LockdownSha1.Update(ctx, (byte*)ptr, 8);
			hash = new byte[20];
			fixed (byte* ptr2 = hash)
			{
				int* ptr3 = (int*)ptr2;
				for (int i = 0; i < 5; i++)
				{
					ptr3[i] = ctx.state[i];
				}
			}
		}

		public static void Final(LockdownSha1.Context ctx, HeapPtr ptr)
		{
			byte[] data;
			LockdownSha1.Final(ctx, out data);
			ptr.MarshalData(data);
		}

		public unsafe static void Pad(LockdownSha1.Context ctx, int amount)
		{
			byte* data = stackalloc byte[4096];
			while (amount > 4096)
			{
				LockdownSha1.Update(ctx, data, 4096);
				amount -= 4096;
			}
			LockdownSha1.Update(ctx, data, amount);
		}

		public static void HashFile(LockdownSha1.Context ctx, string filename)
		{
			byte[] array = File.ReadAllBytes(filename);
			LockdownSha1.Update(ctx, array, array.Length);
		}

		static LockdownSha1()
		{
			// Note: this type is marked as 'beforefieldinit'.
			byte[] array = new byte[81];
			array[0] = 128;
			LockdownSha1.MysteryBuffer = array;
		}
	}
}

using System;

namespace MBNCSUtil.Util
{
	internal class BigInteger
	{
		private const int maxLength = 70;

		public static readonly BigInteger Zero = new BigInteger(0L);

		public static readonly BigInteger One = new BigInteger(1L);

		private uint[] data;

		private int dataLength;

		public BigInteger()
		{
			this.data = new uint[70];
			this.dataLength = 1;
		}

		public BigInteger(long value)
		{
			this.data = new uint[70];
			long num = value;
			this.dataLength = 0;
			while (value != 0L && this.dataLength < 70)
			{
				this.data[this.dataLength] = (uint)(value & (long)((ulong)-1));
				value >>= 32;
				this.dataLength++;
			}
			if (num > 0L)
			{
				if (value != 0L || (this.data[69] & 2147483648u) != 0u)
				{
					throw new ArithmeticException("Positive overflow in constructor.");
				}
			}
			else if (num < 0L && (value != -1L || (this.data[this.dataLength - 1] & 2147483648u) == 0u))
			{
				throw new ArithmeticException("Negative underflow in constructor.");
			}
			if (this.dataLength == 0)
			{
				this.dataLength = 1;
			}
		}

		public BigInteger(ulong value)
		{
			this.data = new uint[70];
			this.dataLength = 0;
			while (value != 0uL && this.dataLength < 70)
			{
				this.data[this.dataLength] = (uint)(value & (ulong)-1);
				value >>= 32;
				this.dataLength++;
			}
			if (value != 0uL || (this.data[69] & 2147483648u) != 0u)
			{
				throw new ArithmeticException("Positive overflow in constructor.");
			}
			if (this.dataLength == 0)
			{
				this.dataLength = 1;
			}
		}

		public BigInteger(BigInteger bi)
		{
			this.data = new uint[70];
			this.dataLength = bi.dataLength;
			for (int i = 0; i < this.dataLength; i++)
			{
				this.data[i] = bi.data[i];
			}
		}

		public BigInteger(string value, int radix)
		{
			BigInteger bi = new BigInteger(1L);
			BigInteger bigInteger = new BigInteger();
			value = value.ToUpper().Trim();
			int num = 0;
			if (value[0] == '-')
			{
				num = 1;
			}
			for (int i = value.Length - 1; i >= num; i--)
			{
				int num2 = (int)value[i];
				if (num2 >= 48 && num2 <= 57)
				{
					num2 -= 48;
				}
				else if (num2 >= 65 && num2 <= 90)
				{
					num2 = num2 - 65 + 10;
				}
				else
				{
					num2 = 9999999;
				}
				if (num2 >= radix)
				{
					throw new ArithmeticException("Invalid string in constructor.");
				}
				if (value[0] == '-')
				{
					num2 = -num2;
				}
				bigInteger += bi * num2;
				if (i - 1 >= num)
				{
					bi *= radix;
				}
			}
			if (value[0] == '-')
			{
				if ((bigInteger.data[69] & 2147483648u) == 0u)
				{
					throw new ArithmeticException("Negative underflow in constructor.");
				}
			}
			else if ((bigInteger.data[69] & 2147483648u) != 0u)
			{
				throw new ArithmeticException("Positive overflow in constructor.");
			}
			this.data = new uint[70];
			for (int j = 0; j < bigInteger.dataLength; j++)
			{
				this.data[j] = bigInteger.data[j];
			}
			this.dataLength = bigInteger.dataLength;
		}

		private static T[] ReverseArray<T>(T[] array)
		{
			T[] array2 = new T[array.Length];
			Array.Copy(array, array2, array.Length);
			Array.Reverse(array2);
			return array2;
		}

		public BigInteger(byte[] inData)
		{
			this.dataLength = inData.Length >> 2;
			int num = inData.Length & 3;
			if (num != 0)
			{
				this.dataLength++;
			}
			if (this.dataLength > 70)
			{
				throw new ArithmeticException("Byte overflow in constructor.");
			}
			this.data = new uint[70];
			int i = inData.Length - 1;
			int num2 = 0;
			while (i >= 3)
			{
				this.data[num2] = (uint)(((int)inData[i - 3] << 24) + ((int)inData[i - 2] << 16) + ((int)inData[i - 1] << 8) + (int)inData[i]);
				i -= 4;
				num2++;
			}
			if (num == 1)
			{
				this.data[this.dataLength - 1] = (uint)inData[0];
			}
			else if (num == 2)
			{
				this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 8) + (int)inData[1]);
			}
			else if (num == 3)
			{
				this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 16) + ((int)inData[1] << 8) + (int)inData[2]);
			}
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
			{
				this.dataLength--;
			}
		}

		public BigInteger(byte[] inData, int inLen)
		{
			this.dataLength = inLen >> 2;
			int num = inLen & 3;
			if (num != 0)
			{
				this.dataLength++;
			}
			if (this.dataLength > 70 || inLen > inData.Length)
			{
				throw new ArithmeticException("Byte overflow in constructor.");
			}
			this.data = new uint[70];
			int i = inLen - 1;
			int num2 = 0;
			while (i >= 3)
			{
				this.data[num2] = (uint)(((int)inData[i - 3] << 24) + ((int)inData[i - 2] << 16) + ((int)inData[i - 1] << 8) + (int)inData[i]);
				i -= 4;
				num2++;
			}
			if (num == 1)
			{
				this.data[this.dataLength - 1] = (uint)inData[0];
			}
			else if (num == 2)
			{
				this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 8) + (int)inData[1]);
			}
			else if (num == 3)
			{
				this.data[this.dataLength - 1] = (uint)(((int)inData[0] << 16) + ((int)inData[1] << 8) + (int)inData[2]);
			}
			if (this.dataLength == 0)
			{
				this.dataLength = 1;
			}
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
			{
				this.dataLength--;
			}
		}

		public BigInteger(uint[] inData)
		{
			this.dataLength = inData.Length;
			if (this.dataLength > 70)
			{
				throw new ArithmeticException("Byte overflow in constructor.");
			}
			this.data = new uint[70];
			int i = this.dataLength - 1;
			int num = 0;
			while (i >= 0)
			{
				this.data[num] = inData[i];
				i--;
				num++;
			}
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
			{
				this.dataLength--;
			}
		}

		public static implicit operator BigInteger(long value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(int value)
		{
			return new BigInteger((long)value);
		}

		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger((ulong)value);
		}

		public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			bigInteger.dataLength = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength);
			long num = 0L;
			for (int i = 0; i < bigInteger.dataLength; i++)
			{
				long num2 = (long)((ulong)bi1.data[i] + (ulong)bi2.data[i] + (ulong)num);
				num = num2 >> 32;
				bigInteger.data[i] = (uint)(num2 & (long)((ulong)-1));
			}
			if (num != 0L && bigInteger.dataLength < 70)
			{
				bigInteger.data[bigInteger.dataLength] = (uint)num;
				bigInteger.dataLength++;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			int num3 = 69;
			if ((bi1.data[num3] & 2147483648u) == (bi2.data[num3] & 2147483648u) && (bigInteger.data[num3] & 2147483648u) != (bi1.data[num3] & 2147483648u))
			{
				throw new ArithmeticException();
			}
			return bigInteger;
		}

		public static BigInteger operator ++(BigInteger bi1)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			long num = 1L;
			int num2 = 0;
			while (num != 0L && num2 < 70)
			{
				long num3 = (long)((ulong)bigInteger.data[num2]);
				num3 += 1L;
				bigInteger.data[num2] = (uint)(num3 & (long)((ulong)-1));
				num = num3 >> 32;
				num2++;
			}
			if (num2 > bigInteger.dataLength)
			{
				bigInteger.dataLength = num2;
			}
			else
			{
				while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
				{
					bigInteger.dataLength--;
				}
			}
			int num4 = 69;
			if ((bi1.data[num4] & 2147483648u) == 0u && (bigInteger.data[num4] & 2147483648u) != (bi1.data[num4] & 2147483648u))
			{
				throw new ArithmeticException("Overflow in ++.");
			}
			return bigInteger;
		}

		public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			bigInteger.dataLength = ((bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength);
			long num = 0L;
			for (int i = 0; i < bigInteger.dataLength; i++)
			{
				long num2 = (long)((ulong)bi1.data[i] - (ulong)bi2.data[i] - (ulong)num);
				bigInteger.data[i] = (uint)(num2 & (long)((ulong)-1));
				if (num2 < 0L)
				{
					num = 1L;
				}
				else
				{
					num = 0L;
				}
			}
			if (num != 0L)
			{
				for (int j = bigInteger.dataLength; j < 70; j++)
				{
					bigInteger.data[j] = 4294967295u;
				}
				bigInteger.dataLength = 70;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			int num3 = 69;
			if ((bi1.data[num3] & 2147483648u) != (bi2.data[num3] & 2147483648u) && (bigInteger.data[num3] & 2147483648u) != (bi1.data[num3] & 2147483648u))
			{
				throw new ArithmeticException();
			}
			return bigInteger;
		}

		public static BigInteger operator --(BigInteger bi1)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			bool flag = true;
			int num = 0;
			while (flag && num < 70)
			{
				long num2 = (long)((ulong)bigInteger.data[num]);
				num2 -= 1L;
				bigInteger.data[num] = (uint)(num2 & (long)((ulong)-1));
				if (num2 >= 0L)
				{
					flag = false;
				}
				num++;
			}
			if (num > bigInteger.dataLength)
			{
				bigInteger.dataLength = num;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			int num3 = 69;
			if ((bi1.data[num3] & 2147483648u) != 0u && (bigInteger.data[num3] & 2147483648u) != (bi1.data[num3] & 2147483648u))
			{
				throw new ArithmeticException("Underflow in --.");
			}
			return bigInteger;
		}

		public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
		{
			int num = 69;
			bool flag = false;
			bool flag2 = false;
			try
			{
				if ((bi1.data[num] & 2147483648u) != 0u)
				{
					flag = true;
					bi1 = -bi1;
				}
				if ((bi2.data[num] & 2147483648u) != 0u)
				{
					flag2 = true;
					bi2 = -bi2;
				}
			}
			catch (Exception)
			{
			}
			BigInteger bigInteger = new BigInteger();
			try
			{
				for (int i = 0; i < bi1.dataLength; i++)
				{
					if (bi1.data[i] != 0u)
					{
						ulong num2 = 0uL;
						int j = 0;
						int num3 = i;
						while (j < bi2.dataLength)
						{
							ulong num4 = (ulong)bi1.data[i] * (ulong)bi2.data[j] + (ulong)bigInteger.data[num3] + num2;
							bigInteger.data[num3] = (uint)(num4 & (ulong)-1);
							num2 = num4 >> 32;
							j++;
							num3++;
						}
						if (num2 != 0uL)
						{
							bigInteger.data[i + bi2.dataLength] = (uint)num2;
						}
					}
				}
			}
			catch (Exception)
			{
				throw new ArithmeticException("Multiplication overflow.");
			}
			bigInteger.dataLength = bi1.dataLength + bi2.dataLength;
			if (bigInteger.dataLength > 70)
			{
				bigInteger.dataLength = 70;
			}
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			if ((bigInteger.data[num] & 2147483648u) != 0u)
			{
				if (flag != flag2 && bigInteger.data[num] == 2147483648u)
				{
					if (bigInteger.dataLength == 1)
					{
						return bigInteger;
					}
					bool flag3 = true;
					int num5 = 0;
					while (num5 < bigInteger.dataLength - 1 && flag3)
					{
						if (bigInteger.data[num5] != 0u)
						{
							flag3 = false;
						}
						num5++;
					}
					if (flag3)
					{
						return bigInteger;
					}
				}
				throw new ArithmeticException("Multiplication overflow.");
			}
			if (flag != flag2)
			{
				return -bigInteger;
			}
			return bigInteger;
		}

		public static BigInteger operator <<(BigInteger bi1, int shiftVal)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			bigInteger.dataLength = BigInteger.shiftLeft(bigInteger.data, shiftVal);
			return bigInteger;
		}

		private static int shiftLeft(uint[] buffer, int shiftVal)
		{
			int num = 32;
			int num2 = buffer.Length;
			while (num2 > 1 && buffer[num2 - 1] == 0u)
			{
				num2--;
			}
			for (int i = shiftVal; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
				}
				ulong num3 = 0uL;
				for (int j = 0; j < num2; j++)
				{
					ulong num4 = (ulong)buffer[j] << num;
					num4 |= num3;
					buffer[j] = (uint)(num4 & (ulong)-1);
					num3 = num4 >> 32;
				}
				if (num3 != 0uL && num2 + 1 <= buffer.Length)
				{
					buffer[num2] = (uint)num3;
					num2++;
				}
			}
			return num2;
		}

		public static BigInteger operator >>(BigInteger bi1, int shiftVal)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			bigInteger.dataLength = BigInteger.shiftRight(bigInteger.data, shiftVal);
			if ((bi1.data[69] & 2147483648u) != 0u)
			{
				for (int i = 69; i >= bigInteger.dataLength; i--)
				{
					bigInteger.data[i] = 4294967295u;
				}
				uint num = 2147483648u;
				int num2 = 0;
				while (num2 < 32 && (bigInteger.data[bigInteger.dataLength - 1] & num) == 0u)
				{
					bigInteger.data[bigInteger.dataLength - 1] |= num;
					num >>= 1;
					num2++;
				}
				bigInteger.dataLength = 70;
			}
			return bigInteger;
		}

		private static int shiftRight(uint[] buffer, int shiftVal)
		{
			int num = 32;
			int num2 = 0;
			int num3 = buffer.Length;
			while (num3 > 1 && buffer[num3 - 1] == 0u)
			{
				num3--;
			}
			for (int i = shiftVal; i > 0; i -= num)
			{
				if (i < num)
				{
					num = i;
					num2 = 32 - num;
				}
				ulong num4 = 0uL;
				for (int j = num3 - 1; j >= 0; j--)
				{
					ulong num5 = (ulong)buffer[j] >> num;
					num5 |= num4;
					num4 = (ulong)buffer[j] << num2;
					buffer[j] = (uint)num5;
				}
			}
			while (num3 > 1 && buffer[num3 - 1] == 0u)
			{
				num3--;
			}
			return num3;
		}

		public static BigInteger operator ~(BigInteger bi1)
		{
			BigInteger bigInteger = new BigInteger(bi1);
			for (int i = 0; i < 70; i++)
			{
				bigInteger.data[i] = ~bi1.data[i];
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		public static BigInteger operator -(BigInteger bi1)
		{
			if (bi1.dataLength == 1 && bi1.data[0] == 0u)
			{
				return new BigInteger();
			}
			BigInteger bigInteger = new BigInteger(bi1);
			for (int i = 0; i < 70; i++)
			{
				bigInteger.data[i] = ~bi1.data[i];
			}
			long num = 1L;
			int num2 = 0;
			while (num != 0L && num2 < 70)
			{
				long num3 = (long)((ulong)bigInteger.data[num2]);
				num3 += 1L;
				bigInteger.data[num2] = (uint)(num3 & (long)((ulong)-1));
				num = num3 >> 32;
				num2++;
			}
			if ((bi1.data[69] & 2147483648u) == (bigInteger.data[69] & 2147483648u))
			{
				throw new ArithmeticException("Overflow in negation.\n");
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		public static bool operator ==(BigInteger bi1, BigInteger bi2)
		{
			return (object.ReferenceEquals(bi1, null) && object.ReferenceEquals(bi2, null)) || (!object.ReferenceEquals(bi1, null) && !object.ReferenceEquals(bi2, null) && bi1.Equals(bi2));
		}

		public static bool operator !=(BigInteger bi1, BigInteger bi2)
		{
			return (!object.ReferenceEquals(bi1, null) || !object.ReferenceEquals(bi2, null)) && (object.ReferenceEquals(bi1, null) || object.ReferenceEquals(bi2, null) || !bi1.Equals(bi2));
		}

		public override bool Equals(object o)
		{
			BigInteger bigInteger = (BigInteger)o;
			if (this.dataLength != bigInteger.dataLength)
			{
				return false;
			}
			for (int i = 0; i < this.dataLength; i++)
			{
				if (this.data[i] != bigInteger.data[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public static bool operator >(BigInteger bi1, BigInteger bi2)
		{
			int num = 69;
			if ((bi1.data[num] & 2147483648u) != 0u && (bi2.data[num] & 2147483648u) == 0u)
			{
				return false;
			}
			if ((bi1.data[num] & 2147483648u) == 0u && (bi2.data[num] & 2147483648u) != 0u)
			{
				return true;
			}
			int num2 = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			num = num2 - 1;
			while (num >= 0 && bi1.data[num] == bi2.data[num])
			{
				num--;
			}
			return num >= 0 && bi1.data[num] > bi2.data[num];
		}

		public static bool operator <(BigInteger bi1, BigInteger bi2)
		{
			int num = 69;
			if ((bi1.data[num] & 2147483648u) != 0u && (bi2.data[num] & 2147483648u) == 0u)
			{
				return true;
			}
			if ((bi1.data[num] & 2147483648u) == 0u && (bi2.data[num] & 2147483648u) != 0u)
			{
				return false;
			}
			int num2 = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			num = num2 - 1;
			while (num >= 0 && bi1.data[num] == bi2.data[num])
			{
				num--;
			}
			return num >= 0 && bi1.data[num] < bi2.data[num];
		}

		public static bool operator >=(BigInteger bi1, BigInteger bi2)
		{
			return bi1 == bi2 || bi1 > bi2;
		}

		public static bool operator <=(BigInteger bi1, BigInteger bi2)
		{
			return bi1 == bi2 || bi1 < bi2;
		}

		private static void multiByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
		{
			uint[] array = new uint[70];
			int num = bi1.dataLength + 1;
			uint[] array2 = new uint[num];
			uint num2 = 2147483648u;
			uint num3 = bi2.data[bi2.dataLength - 1];
			int num4 = 0;
			int num5 = 0;
			while (num2 != 0u && (num3 & num2) == 0u)
			{
				num4++;
				num2 >>= 1;
			}
			for (int i = 0; i < bi1.dataLength; i++)
			{
				array2[i] = bi1.data[i];
			}
			BigInteger.shiftLeft(array2, num4);
			bi2 <<= num4;
			int j = num - bi2.dataLength;
			int num6 = num - 1;
			ulong num7 = (ulong)bi2.data[bi2.dataLength - 1];
			ulong num8 = (ulong)bi2.data[bi2.dataLength - 2];
			int num9 = bi2.dataLength + 1;
			uint[] array3 = new uint[num9];
			while (j > 0)
			{
				ulong num10 = ((ulong)array2[num6] << 32) + (ulong)array2[num6 - 1];
				ulong num11 = num10 / num7;
				ulong num12 = num10 % num7;
				bool flag = false;
				while (!flag)
				{
					flag = true;
					if (num11 == 4294967296uL || num11 * num8 > (num12 << 32) + (ulong)array2[num6 - 2])
					{
						num11 -= 1uL;
						num12 += num7;
						if (num12 < 4294967296uL)
						{
							flag = false;
						}
					}
				}
				for (int k = 0; k < num9; k++)
				{
					array3[k] = array2[num6 - k];
				}
				BigInteger bigInteger = new BigInteger(array3);
				BigInteger bigInteger2 = bi2 * (long)num11;
				while (bigInteger2 > bigInteger)
				{
					num11 -= 1uL;
					bigInteger2 -= bi2;
				}
				BigInteger bigInteger3 = bigInteger - bigInteger2;
				for (int l = 0; l < num9; l++)
				{
					array2[num6 - l] = bigInteger3.data[bi2.dataLength - l];
				}
				array[num5++] = (uint)num11;
				num6--;
				j--;
			}
			outQuotient.dataLength = num5;
			int m = 0;
			int n = outQuotient.dataLength - 1;
			while (n >= 0)
			{
				outQuotient.data[m] = array[n];
				n--;
				m++;
			}
			while (m < 70)
			{
				outQuotient.data[m] = 0u;
				m++;
			}
			while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0u)
			{
				outQuotient.dataLength--;
			}
			if (outQuotient.dataLength == 0)
			{
				outQuotient.dataLength = 1;
			}
			outRemainder.dataLength = BigInteger.shiftRight(array2, num4);
			for (m = 0; m < outRemainder.dataLength; m++)
			{
				outRemainder.data[m] = array2[m];
			}
			while (m < 70)
			{
				outRemainder.data[m] = 0u;
				m++;
			}
		}

		private static void singleByteDivide(BigInteger bi1, BigInteger bi2, BigInteger outQuotient, BigInteger outRemainder)
		{
			uint[] array = new uint[70];
			int num = 0;
			for (int i = 0; i < 70; i++)
			{
				outRemainder.data[i] = bi1.data[i];
			}
			outRemainder.dataLength = bi1.dataLength;
			while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0u)
			{
				outRemainder.dataLength--;
			}
			ulong num2 = (ulong)bi2.data[0];
			int j = outRemainder.dataLength - 1;
			ulong num3 = (ulong)outRemainder.data[j];
			if (num3 >= num2)
			{
				ulong num4 = num3 / num2;
				array[num++] = (uint)num4;
				outRemainder.data[j] = (uint)(num3 % num2);
			}
			j--;
			while (j >= 0)
			{
				num3 = ((ulong)outRemainder.data[j + 1] << 32) + (ulong)outRemainder.data[j];
				ulong num5 = num3 / num2;
				array[num++] = (uint)num5;
				outRemainder.data[j + 1] = 0u;
				outRemainder.data[j--] = (uint)(num3 % num2);
			}
			outQuotient.dataLength = num;
			int k = 0;
			int l = outQuotient.dataLength - 1;
			while (l >= 0)
			{
				outQuotient.data[k] = array[l];
				l--;
				k++;
			}
			while (k < 70)
			{
				outQuotient.data[k] = 0u;
				k++;
			}
			while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0u)
			{
				outQuotient.dataLength--;
			}
			if (outQuotient.dataLength == 0)
			{
				outQuotient.dataLength = 1;
			}
			while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0u)
			{
				outRemainder.dataLength--;
			}
		}

		public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			BigInteger outRemainder = new BigInteger();
			int num = 69;
			bool flag = false;
			bool flag2 = false;
			if ((bi1.data[num] & 2147483648u) != 0u)
			{
				bi1 = -bi1;
				flag2 = true;
			}
			if ((bi2.data[num] & 2147483648u) != 0u)
			{
				bi2 = -bi2;
				flag = true;
			}
			if (bi1 < bi2)
			{
				return bigInteger;
			}
			if (bi2.dataLength == 1)
			{
				BigInteger.singleByteDivide(bi1, bi2, bigInteger, outRemainder);
			}
			else
			{
				BigInteger.multiByteDivide(bi1, bi2, bigInteger, outRemainder);
			}
			if (flag2 != flag)
			{
				return -bigInteger;
			}
			return bigInteger;
		}

		public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
		{
			BigInteger outQuotient = new BigInteger();
			BigInteger bigInteger = new BigInteger(bi1);
			int num = 69;
			bool flag = false;
			if ((bi1.data[num] & 2147483648u) != 0u)
			{
				bi1 = -bi1;
				flag = true;
			}
			if ((bi2.data[num] & 2147483648u) != 0u)
			{
				bi2 = -bi2;
			}
			if (bi1 < bi2)
			{
				return bigInteger;
			}
			if (bi2.dataLength == 1)
			{
				BigInteger.singleByteDivide(bi1, bi2, outQuotient, bigInteger);
			}
			else
			{
				BigInteger.multiByteDivide(bi1, bi2, outQuotient, bigInteger);
			}
			if (flag)
			{
				return -bigInteger;
			}
			return bigInteger;
		}

		public static BigInteger operator &(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			for (int i = 0; i < num; i++)
			{
				uint num2 = bi1.data[i] & bi2.data[i];
				bigInteger.data[i] = num2;
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		public static BigInteger operator |(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			for (int i = 0; i < num; i++)
			{
				uint num2 = bi1.data[i] | bi2.data[i];
				bigInteger.data[i] = num2;
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		public static BigInteger operator ^(BigInteger bi1, BigInteger bi2)
		{
			BigInteger bigInteger = new BigInteger();
			int num = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
			for (int i = 0; i < num; i++)
			{
				uint num2 = bi1.data[i] ^ bi2.data[i];
				bigInteger.data[i] = num2;
			}
			bigInteger.dataLength = 70;
			while (bigInteger.dataLength > 1 && bigInteger.data[bigInteger.dataLength - 1] == 0u)
			{
				bigInteger.dataLength--;
			}
			return bigInteger;
		}

		public BigInteger Max(BigInteger bi)
		{
			if (this > bi)
			{
				return new BigInteger(this);
			}
			return new BigInteger(bi);
		}

		public BigInteger Min(BigInteger bi)
		{
			if (this < bi)
			{
				return new BigInteger(this);
			}
			return new BigInteger(bi);
		}

		public BigInteger Abs()
		{
			if ((this.data[69] & 2147483648u) != 0u)
			{
				return -this;
			}
			return new BigInteger(this);
		}

		public override string ToString()
		{
			return this.ToString(10);
		}

		public string ToString(int radix)
		{
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentException("Radix must be >= 2 and <= 36");
			}
			string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string text2 = "";
			BigInteger bigInteger = this;
			bool flag = false;
			if ((bigInteger.data[69] & 2147483648u) != 0u)
			{
				flag = true;
				try
				{
					bigInteger = -bigInteger;
				}
				catch (Exception)
				{
				}
			}
			BigInteger bigInteger2 = new BigInteger();
			BigInteger bigInteger3 = new BigInteger();
			BigInteger bi = new BigInteger((long)radix);
			if (bigInteger.dataLength == 1 && bigInteger.data[0] == 0u)
			{
				text2 = "0";
			}
			else
			{
				while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] != 0u))
				{
					BigInteger.singleByteDivide(bigInteger, bi, bigInteger2, bigInteger3);
					if (bigInteger3.data[0] < 10u)
					{
						text2 = bigInteger3.data[0] + text2;
					}
					else
					{
						text2 = text[(int)(bigInteger3.data[0] - 10u)] + text2;
					}
					bigInteger = bigInteger2;
				}
				if (flag)
				{
					text2 = "-" + text2;
				}
			}
			return text2;
		}

		public string ToHexString()
		{
			string text = this.data[this.dataLength - 1].ToString("X");
			for (int i = this.dataLength - 2; i >= 0; i--)
			{
				text += this.data[i].ToString("X8");
			}
			return text;
		}

		public BigInteger ModPow(BigInteger exp, BigInteger n)
		{
			if ((exp.data[69] & 2147483648u) != 0u)
			{
				throw new ArithmeticException("Positive exponents only.");
			}
			BigInteger bigInteger = 1;
			bool flag = false;
			BigInteger bigInteger2;
			if ((this.data[69] & 2147483648u) != 0u)
			{
				bigInteger2 = -this % n;
				flag = true;
			}
			else
			{
				bigInteger2 = this % n;
			}
			if ((n.data[69] & 2147483648u) != 0u)
			{
				n = -n;
			}
			BigInteger bigInteger3 = new BigInteger();
			int num = n.dataLength << 1;
			bigInteger3.data[num] = 1u;
			bigInteger3.dataLength = num + 1;
			bigInteger3 /= n;
			int num2 = exp.BitCount();
			int num3 = 0;
			for (int i = 0; i < exp.dataLength; i++)
			{
				uint num4 = 1u;
				int j = 0;
				while (j < 32)
				{
					if ((exp.data[i] & num4) != 0u)
					{
						bigInteger = this.BarrettReduction(bigInteger * bigInteger2, n, bigInteger3);
					}
					num4 <<= 1;
					bigInteger2 = this.BarrettReduction(bigInteger2 * bigInteger2, n, bigInteger3);
					if (bigInteger2.dataLength == 1 && bigInteger2.data[0] == 1u)
					{
						if (flag && (exp.data[0] & 1u) != 0u)
						{
							return -bigInteger;
						}
						return bigInteger;
					}
					else
					{
						num3++;
						if (num3 == num2)
						{
							break;
						}
						j++;
					}
				}
			}
			if (flag && (exp.data[0] & 1u) != 0u)
			{
				return -bigInteger;
			}
			return bigInteger;
		}

		private BigInteger BarrettReduction(BigInteger x, BigInteger n, BigInteger constant)
		{
			int num = n.dataLength;
			int num2 = num + 1;
			int num3 = num - 1;
			BigInteger bigInteger = new BigInteger();
			int i = num3;
			int num4 = 0;
			while (i < x.dataLength)
			{
				bigInteger.data[num4] = x.data[i];
				i++;
				num4++;
			}
			bigInteger.dataLength = x.dataLength - num3;
			if (bigInteger.dataLength <= 0)
			{
				bigInteger.dataLength = 1;
			}
			BigInteger bigInteger2 = bigInteger * constant;
			BigInteger bigInteger3 = new BigInteger();
			int j = num2;
			int num5 = 0;
			while (j < bigInteger2.dataLength)
			{
				bigInteger3.data[num5] = bigInteger2.data[j];
				j++;
				num5++;
			}
			bigInteger3.dataLength = bigInteger2.dataLength - num2;
			if (bigInteger3.dataLength <= 0)
			{
				bigInteger3.dataLength = 1;
			}
			BigInteger bigInteger4 = new BigInteger();
			int num6 = (x.dataLength > num2) ? num2 : x.dataLength;
			for (int k = 0; k < num6; k++)
			{
				bigInteger4.data[k] = x.data[k];
			}
			bigInteger4.dataLength = num6;
			BigInteger bigInteger5 = new BigInteger();
			for (int l = 0; l < bigInteger3.dataLength; l++)
			{
				if (bigInteger3.data[l] != 0u)
				{
					ulong num7 = 0uL;
					int num8 = l;
					int num9 = 0;
					while (num9 < n.dataLength && num8 < num2)
					{
						ulong num10 = (ulong)bigInteger3.data[l] * (ulong)n.data[num9] + (ulong)bigInteger5.data[num8] + num7;
						bigInteger5.data[num8] = (uint)(num10 & (ulong)-1);
						num7 = num10 >> 32;
						num9++;
						num8++;
					}
					if (num8 < num2)
					{
						bigInteger5.data[num8] = (uint)num7;
					}
				}
			}
			bigInteger5.dataLength = num2;
			while (bigInteger5.dataLength > 1 && bigInteger5.data[bigInteger5.dataLength - 1] == 0u)
			{
				bigInteger5.dataLength--;
			}
			bigInteger4 -= bigInteger5;
			if ((bigInteger4.data[69] & 2147483648u) != 0u)
			{
				BigInteger bigInteger6 = new BigInteger();
				bigInteger6.data[num2] = 1u;
				bigInteger6.dataLength = num2 + 1;
				bigInteger4 += bigInteger6;
			}
			while (bigInteger4 >= n)
			{
				bigInteger4 -= n;
			}
			return bigInteger4;
		}

		public int BitCount()
		{
			while (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
			{
				this.dataLength--;
			}
			uint num = this.data[this.dataLength - 1];
			uint num2 = 2147483648u;
			int num3 = 32;
			while (num3 > 0 && (num & num2) == 0u)
			{
				num3--;
				num2 >>= 1;
			}
			return num3 + (this.dataLength - 1 << 5);
		}

		public int ToInt32()
		{
			return (int)this.data[0];
		}

		public long ToInt64()
		{
			long num = 0L;
			num = (long)((ulong)this.data[0]);
			try
			{
				num |= (long)((long)((ulong)this.data[1]) << 32);
			}
			catch (Exception)
			{
				if ((this.data[0] & 2147483648u) != 0u)
				{
					num = (long)this.data[0];
				}
			}
			return num;
		}

		public BigInteger ModInverse(BigInteger modulus)
		{
			BigInteger[] array = new BigInteger[]
			{
				0,
				1
			};
			BigInteger[] array2 = new BigInteger[2];
			BigInteger[] array3 = new BigInteger[]
			{
				0,
				0
			};
			int num = 0;
			BigInteger bi = modulus;
			BigInteger bigInteger = this;
			while (bigInteger.dataLength > 1 || (bigInteger.dataLength == 1 && bigInteger.data[0] != 0u))
			{
				BigInteger bigInteger2 = new BigInteger();
				BigInteger bigInteger3 = new BigInteger();
				if (num > 1)
				{
					BigInteger bigInteger4 = (array[0] - array[1] * array2[0]) % modulus;
					array[0] = array[1];
					array[1] = bigInteger4;
				}
				if (bigInteger.dataLength == 1)
				{
					BigInteger.singleByteDivide(bi, bigInteger, bigInteger2, bigInteger3);
				}
				else
				{
					BigInteger.multiByteDivide(bi, bigInteger, bigInteger2, bigInteger3);
				}
				array2[0] = array2[1];
				array3[0] = array3[1];
				array2[1] = bigInteger2;
				array3[1] = bigInteger3;
				bi = bigInteger;
				bigInteger = bigInteger3;
				num++;
			}
			if (array3[0].dataLength > 1 || (array3[0].dataLength == 1 && array3[0].data[0] != 1u))
			{
				throw new ArithmeticException("No inverse!");
			}
			BigInteger bigInteger5 = (array[0] - array[1] * array2[0]) % modulus;
			if ((bigInteger5.data[69] & 2147483648u) != 0u)
			{
				bigInteger5 += modulus;
			}
			return bigInteger5;
		}

		public byte[] GetBytes()
		{
			int num = this.BitCount();
			int num2 = num >> 3;
			if ((num & 7) != 0)
			{
				num2++;
			}
			byte[] array = new byte[num2];
			int num3 = 0;
			uint num4 = this.data[this.dataLength - 1];
			uint num5;
			if ((num5 = (num4 >> 24 & 255u)) != 0u)
			{
				array[num3++] = (byte)num5;
			}
			if ((num5 = (num4 >> 16 & 255u)) != 0u)
			{
				array[num3++] = (byte)num5;
			}
			if ((num5 = (num4 >> 8 & 255u)) != 0u)
			{
				array[num3++] = (byte)num5;
			}
			if ((num5 = (num4 & 255u)) != 0u)
			{
				array[num3++] = (byte)num5;
			}
			int i = this.dataLength - 2;
			while (i >= 0)
			{
				num4 = this.data[i];
				array[num3 + 3] = (byte)(num4 & 255u);
				num4 >>= 8;
				array[num3 + 2] = (byte)(num4 & 255u);
				num4 >>= 8;
				array[num3 + 1] = (byte)(num4 & 255u);
				num4 >>= 8;
				array[num3] = (byte)(num4 & 255u);
				i--;
				num3 += 4;
			}
			return array;
		}

		public void SetBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			byte b = (byte)(bitNum & 31u);
			uint num2 = 1u << (int)b;
			this.data[(int)((UIntPtr)num)] |= num2;
			if ((ulong)num >= (ulong)((long)this.dataLength))
			{
				this.dataLength = (int)(num + 1u);
			}
		}

		public void UnsetBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			if ((ulong)num < (ulong)((long)this.dataLength))
			{
				byte b = (byte)(bitNum & 31u);
				uint num2 = 1u << (int)b;
				uint num3 = 4294967295u ^ num2;
				this.data[(int)((UIntPtr)num)] &= num3;
				if (this.dataLength > 1 && this.data[this.dataLength - 1] == 0u)
				{
					this.dataLength--;
				}
			}
		}

		public BigInteger SquareRoot()
		{
			uint num = (uint)this.BitCount();
			if ((num & 1u) != 0u)
			{
				num = (num >> 1) + 1u;
			}
			else
			{
				num >>= 1;
			}
			uint num2 = num >> 5;
			byte b = (byte)(num & 31u);
			BigInteger bigInteger = new BigInteger();
			uint num3;
			if (b == 0)
			{
				num3 = 2147483648u;
			}
			else
			{
				num3 = 1u << (int)b;
				num2 += 1u;
			}
			bigInteger.dataLength = (int)num2;
			for (int i = (int)(num2 - 1u); i >= 0; i--)
			{
				while (num3 != 0u)
				{
					bigInteger.data[i] ^= num3;
					if (bigInteger * bigInteger > this)
					{
						bigInteger.data[i] ^= num3;
					}
					num3 >>= 1;
				}
				num3 = 2147483648u;
			}
			return bigInteger;
		}
	}
}

using System;
using System.IO;
using System.Text;

namespace MBNCSUtil
{
	public class DataReader
	{
		private byte[] m_data;

		private int m_index;

		[Obsolete("This property is deprecated and should no longer be used.  To access the underlying data you should use the UnderlyingBuffer property.", true)]
		protected virtual byte[] Data
		{
			get
			{
				byte[] array = new byte[this.m_data.Length];
				Buffer.BlockCopy(this.m_data, 0, array, 0, array.Length);
				return array;
			}
		}

		protected byte[] UnderlyingBuffer
		{
			get
			{
				return this.m_data;
			}
		}

		public virtual int Length
		{
			get
			{
				return this.m_data.Length;
			}
		}

		public DataReader(Stream str, int length)
		{
			if (str == null)
			{
				throw new ArgumentNullException(Resources.param_str, Resources.streamNull);
			}
			int i = length;
			int num = 0;
			this.m_data = new byte[i];
			while (i > 0)
			{
				int num2 = str.Read(this.m_data, num, i);
				num += num2;
				i -= num2;
			}
		}

		public DataReader(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(Resources.param_data, Resources.dataNull);
			}
			this.m_data = data;
		}

		public bool ReadBoolean()
		{
			return BitConverter.ToInt32(this.m_data, this.m_index) != 0;
		}

		public byte ReadByte()
		{
			return this.m_data[this.m_index++];
		}

		public byte[] ReadByteArray(int expectedItems)
		{
			byte[] array = new byte[expectedItems];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, expectedItems);
			this.m_index += expectedItems;
			return array;
		}

		public byte[] ReadNullTerminatedByteArray()
		{
			int num = this.m_index;
			while (num < this.m_data.Length && this.m_data[num] != 0)
			{
				num++;
			}
			byte[] array = new byte[num - this.m_index];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, array.Length);
			this.m_index = num + 1;
			return array;
		}

		public short ReadInt16()
		{
			short result = BitConverter.ToInt16(this.m_data, this.m_index);
			this.m_index += 2;
			return result;
		}

		public short[] ReadInt16Array(int expectedItems)
		{
			short[] array = new short[expectedItems];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, expectedItems * 2);
			this.m_index += expectedItems * 2;
			return array;
		}

		[CLSCompliant(false)]
		public ushort ReadUInt16()
		{
			ushort result = BitConverter.ToUInt16(this.m_data, this.m_index);
			this.m_index += 2;
			return result;
		}

		[CLSCompliant(false)]
		public ushort[] ReadUInt16Array(int expectedItems)
		{
			ushort[] array = new ushort[expectedItems];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, expectedItems * 2);
			this.m_index += expectedItems * 2;
			return array;
		}

		public int ReadInt32()
		{
			int result = BitConverter.ToInt32(this.m_data, this.m_index);
			this.m_index += 4;
			return result;
		}

		public int[] ReadInt32Array(int expectedItems)
		{
			int[] array = new int[expectedItems];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, expectedItems * 4);
			this.m_index += expectedItems * 4;
			return array;
		}

		[CLSCompliant(false)]
		public uint ReadUInt32()
		{
			uint result = BitConverter.ToUInt32(this.m_data, this.m_index);
			this.m_index += 4;
			return result;
		}

		[CLSCompliant(false)]
		public uint[] ReadUInt32Array(int expectedItems)
		{
			uint[] array = new uint[expectedItems];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, expectedItems * 4);
			this.m_index += expectedItems * 4;
			return array;
		}

		public long ReadInt64()
		{
			long result = BitConverter.ToInt64(this.m_data, this.m_index);
			this.m_index += 8;
			return result;
		}

		public long[] ReadInt64Array(int expectedItems)
		{
			long[] array = new long[expectedItems];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, expectedItems * 8);
			this.m_index += expectedItems * 8;
			return array;
		}

		[CLSCompliant(false)]
		public ulong ReadUInt64()
		{
			ulong result = BitConverter.ToUInt64(this.m_data, this.m_index);
			this.m_index += 8;
			return result;
		}

		[CLSCompliant(false)]
		public ulong[] ReadUInt64Array(int expectedItems)
		{
			ulong[] array = new ulong[expectedItems];
			Buffer.BlockCopy(this.m_data, this.m_index, array, 0, expectedItems * 8);
			this.m_index += expectedItems * 8;
			return array;
		}

		public int Peek()
		{
			if (this.m_index >= this.m_data.Length)
			{
				return -1;
			}
			return (int)this.m_data[this.m_index];
		}

		public string PeekDwordString(byte padding)
		{
			byte[] array = new byte[4];
			int num = -1;
			int i = this.m_index;
			int num2 = 3;
			while (i < this.m_index + 4)
			{
				array[num2] = this.m_data[i];
				if (array[num2] == padding)
				{
					num = num2;
				}
				i++;
				num2--;
			}
			if (num == -1)
			{
				num = 4;
			}
			return Encoding.ASCII.GetString(array, 0, num);
		}

		public string ReadDwordString(byte padding)
		{
			string result = this.PeekDwordString(padding);
			this.m_index += 4;
			return result;
		}

		public string ReadCString()
		{
			return this.ReadCString(Encoding.ASCII);
		}

		public string ReadCString(Encoding enc)
		{
			return this.ReadTerminatedString('\0', enc);
		}

		public string ReadPascalString()
		{
			return this.ReadPascalString(Encoding.ASCII);
		}

		public string ReadPascalString(Encoding enc)
		{
			int count = (int)this.ReadByte();
			string @string = enc.GetString(this.m_data, this.m_index, count);
			this.m_index += enc.GetByteCount(@string);
			return @string;
		}

		public string ReadWidePascalString()
		{
			return this.ReadWidePascalString(Encoding.ASCII);
		}

		public string ReadWidePascalString(Encoding enc)
		{
			int count = (int)this.ReadInt16();
			string @string = enc.GetString(this.m_data, this.m_index, count);
			this.m_index += enc.GetByteCount(@string);
			return @string;
		}

		public string ReadTerminatedString(char Terminator, Encoding enc)
		{
			int num = this.m_index;
			if (enc != Encoding.Unicode)
			{
				if (enc != Encoding.BigEndianUnicode)
				{
					while (num < this.m_data.Length && (char)this.m_data[num] != (Terminator & 'ÿ'))
					{
						num++;
					}
					goto IL_66;
				}
			}
			while (num < this.m_data.Length && num + 1 < this.m_data.Length)
			{
				if (BitConverter.ToChar(this.m_data, num) == Terminator)
				{
					break;
				}
				num++;
			}
			IL_66:
			string @string = enc.GetString(this.m_data, this.m_index, num - this.m_index);
			this.m_index = num + 1;
			return @string;
		}

		public bool Seek(int offset)
		{
			bool result = false;
			if (this.m_index + offset < this.m_data.Length)
			{
				this.m_index += offset;
				result = true;
			}
			return result;
		}

		public override string ToString()
		{
			return DataFormatter.Format(this.m_data, 0, this.Length);
		}
	}
}

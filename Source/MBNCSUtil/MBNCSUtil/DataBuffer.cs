using System;
using System.IO;
using System.Text;

namespace MBNCSUtil
{
	public class DataBuffer : IDisposable
	{
		private MemoryStream m_ms;

		private int m_len;

		public virtual int Count
		{
			get
			{
				return this.m_len;
			}
		}

		public DataBuffer()
		{
			this.m_ms = new MemoryStream();
		}

		public void Insert(bool b)
		{
			if (b)
			{
				this.Insert(1);
				return;
			}
			this.Insert(0);
		}

		public void InsertBoolean(bool b)
		{
			if (b)
			{
				this.Insert(1);
				return;
			}
			this.Insert(0);
		}

		public void Insert(byte b)
		{
			lock (this)
			{
				this.m_ms.WriteByte(b);
				this.m_len++;
			}
		}

		public void InsertByte(byte b)
		{
			lock (this)
			{
				this.m_ms.WriteByte(b);
				this.m_len++;
			}
		}

		[CLSCompliant(false)]
		public void Insert(sbyte b)
		{
			lock (this)
			{
				this.m_ms.WriteByte((byte)b);
				this.m_len++;
			}
		}

		[CLSCompliant(false)]
		public void InsertSByte(sbyte b)
		{
			lock (this)
			{
				this.m_ms.WriteByte((byte)b);
				this.m_len++;
			}
		}

		public void Insert(byte[] b)
		{
			lock (this)
			{
				this.m_ms.Write(b, 0, b.Length);
				this.m_len += b.Length;
			}
		}

		public void InsertByteArray(byte[] b)
		{
			lock (this)
			{
				this.m_ms.Write(b, 0, b.Length);
				this.m_len += b.Length;
			}
		}

		[CLSCompliant(false)]
		public void Insert(sbyte[] b)
		{
			byte[] array = new byte[b.Length];
			Buffer.BlockCopy(b, 0, array, 0, b.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, b.Length);
				this.m_len += b.Length;
			}
		}

		[CLSCompliant(false)]
		public void InsertSByteArray(sbyte[] b)
		{
			byte[] array = new byte[b.Length];
			Buffer.BlockCopy(b, 0, array, 0, b.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, b.Length);
				this.m_len += b.Length;
			}
		}

		public void Insert(short s)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(s), 0, 2);
				this.m_len += 2;
			}
		}

		public void InsertInt16(short s)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(s), 0, 2);
				this.m_len += 2;
			}
		}

		[CLSCompliant(false)]
		public void Insert(ushort s)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(s), 0, 2);
				this.m_len += 2;
			}
		}

		[CLSCompliant(false)]
		public void InsertUInt16(ushort s)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(s), 0, 2);
				this.m_len += 2;
			}
		}

		public void Insert(short[] s)
		{
			byte[] array = new byte[s.Length * 2];
			Buffer.BlockCopy(s, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		public void InsertInt16Array(short[] s)
		{
			byte[] array = new byte[s.Length * 2];
			Buffer.BlockCopy(s, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		[CLSCompliant(false)]
		public void Insert(ushort[] s)
		{
			byte[] array = new byte[s.Length * 2];
			Buffer.BlockCopy(s, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		[CLSCompliant(false)]
		public void InsertUInt16Array(ushort[] s)
		{
			byte[] array = new byte[s.Length * 2];
			Buffer.BlockCopy(s, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		public void Insert(int i)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(i), 0, 4);
				this.m_len += 4;
			}
		}

		public void InsertInt32(int i)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(i), 0, 4);
				this.m_len += 4;
			}
		}

		[CLSCompliant(false)]
		public void Insert(uint i)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(i), 0, 4);
				this.m_len += 4;
			}
		}

		[CLSCompliant(false)]
		public void InsertUInt32(uint i)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(i), 0, 4);
				this.m_len += 4;
			}
		}

		public void Insert(int[] i)
		{
			byte[] array = new byte[i.Length * 4];
			Buffer.BlockCopy(i, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		public void InsertInt32Array(int[] i)
		{
			byte[] array = new byte[i.Length * 4];
			Buffer.BlockCopy(i, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		[CLSCompliant(false)]
		public void Insert(uint[] i)
		{
			byte[] array = new byte[i.Length * 4];
			Buffer.BlockCopy(i, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		[CLSCompliant(false)]
		public void InsertUInt32Array(uint[] i)
		{
			byte[] array = new byte[i.Length * 4];
			Buffer.BlockCopy(i, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		public void Insert(long l)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(l), 0, 8);
				this.m_len += 8;
			}
		}

		public void InsertInt64(long l)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(l), 0, 8);
				this.m_len += 8;
			}
		}

		[CLSCompliant(false)]
		public void Insert(ulong l)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(l), 0, 8);
				this.m_len += 8;
			}
		}

		[CLSCompliant(false)]
		public void InsertUInt64(ulong l)
		{
			lock (this)
			{
				this.m_ms.Write(BitConverter.GetBytes(l), 0, 8);
				this.m_len += 8;
			}
		}

		public void Insert(long[] l)
		{
			byte[] array = new byte[l.Length * 8];
			Buffer.BlockCopy(l, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		public void InsertInt64Array(long[] l)
		{
			byte[] array = new byte[l.Length * 8];
			Buffer.BlockCopy(l, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		[CLSCompliant(false)]
		public void Insert(ulong[] l)
		{
			byte[] array = new byte[l.Length * 8];
			Buffer.BlockCopy(l, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		[CLSCompliant(false)]
		public void InsertUInt64Array(ulong[] l)
		{
			byte[] array = new byte[l.Length * 8];
			Buffer.BlockCopy(l, 0, array, 0, array.Length);
			lock (this)
			{
				this.m_ms.Write(array, 0, array.Length);
				this.m_len += array.Length;
			}
		}

		public void InsertCString(string str)
		{
			this.InsertCString(str, Encoding.ASCII);
		}

		public void InsertCString(string str, Encoding enc)
		{
			if (str == null)
			{
				throw new ArgumentNullException(Resources.param_str, Resources.strNull);
			}
			if (enc == null)
			{
				throw new ArgumentNullException(Resources.param_enc, Resources.encNull);
			}
			this.Insert(enc.GetBytes(str));
			char[] chars = new char[1];
			this.Insert(new byte[enc.GetByteCount(chars)]);
		}

		public void InsertPascalString(string str)
		{
			this.InsertPascalString(str, Encoding.ASCII);
		}

		public void InsertPascalString(string str, Encoding enc)
		{
			if (str.Length > 255)
			{
				throw new ArgumentException("String length was too long; max length 255.", "str");
			}
			this.Insert((byte)(str.Length & 255));
			this.Insert(enc.GetBytes(str));
		}

		public void InsertWidePascalString(string str)
		{
			this.InsertWidePascalString(str, Encoding.ASCII);
		}

		public void InsertWidePascalString(string str, Encoding enc)
		{
			if (str.Length > 65535)
			{
				throw new ArgumentException("String length was too long; max length 65535.", "str");
			}
			this.Insert((ushort)(str.Length & 65535));
			this.Insert(enc.GetBytes(str));
		}

		public void InsertDwordString(string str)
		{
			this.InsertDwordString(str, 0);
		}

		public void InsertDwordString(string str, byte padding)
		{
			if (str.Length > 4)
			{
				throw new ArgumentException("String length was too long; max length 4.", "str");
			}
			lock (this)
			{
				if (str.Length < 4)
				{
					int num = 4 - str.Length;
					for (int i = 0; i < num; i++)
					{
						this.Insert(padding);
					}
				}
				byte[] bytes = Encoding.ASCII.GetBytes(str);
				for (int j = bytes.Length - 1; j >= 0; j--)
				{
					this.Insert(bytes[j]);
				}
			}
		}

		public virtual byte[] GetData()
		{
			byte[] array = null;
			lock (this)
			{
				array = new byte[this.m_len];
				Buffer.BlockCopy(this.m_ms.GetBuffer(), 0, array, 0, this.m_len);
			}
			return array;
		}

		public virtual int WriteToOutputStream(Stream str)
		{
			lock (this)
			{
				byte[] data = this.GetData();
				str.Write(data, 0, this.Count);
			}
			return this.Count;
		}

		public override string ToString()
		{
			return DataFormatter.Format(this.GetData(), 0, this.Count);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_ms != null)
			{
				this.m_ms.Dispose();
				this.m_ms = null;
			}
		}
	}
}

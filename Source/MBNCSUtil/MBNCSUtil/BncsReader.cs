using System;
using System.IO;

namespace MBNCSUtil
{
	public class BncsReader : DataReader
	{
		private int m_len;

		private byte m_id;

		public override int Length
		{
			get
			{
				return this.m_len;
			}
		}

		public byte PacketID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		public BncsReader(Stream str) : this(str, new BinaryReader(str))
		{
		}

		private BncsReader(Stream str, BinaryReader br) : this(str, br.ReadBytes(2)[1], br.ReadUInt16())
		{
		}

		private BncsReader(Stream str, byte id, ushort len) : base(str, (int)(len - 4))
		{
			this.m_id = id;
			this.m_len = (int)len;
		}

		public BncsReader(byte[] data) : this(new MemoryStream(data, 4, data.Length - 4, false, false), data[1], BitConverter.ToUInt16(data, 2))
		{
		}
	}
}

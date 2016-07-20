using System;

namespace MBNCSUtil
{
	public sealed class BncsPacket : DataBuffer
	{
		private byte m_id;

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

		public override int Count
		{
			get
			{
				return base.Count + 4;
			}
		}

		public BncsPacket(byte id)
		{
			this.m_id = id;
		}

		public override byte[] GetData()
		{
			byte[] array = new byte[this.Count];
			byte[] data = base.GetData();
			array[0] = 255;
			array[1] = this.m_id;
			byte[] bytes = BitConverter.GetBytes((ushort)(this.Count & 65535));
			array[2] = bytes[0];
			array[3] = bytes[1];
			Buffer.BlockCopy(data, 0, array, 4, data.Length);
			return array;
		}
	}
}

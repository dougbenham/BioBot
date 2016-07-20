using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace BioBot
{
	public abstract class D2Socket : Sockets
	{
		public delegate void PacketLostEventHandler(int PacketID);

		private struct IdAndTime
		{
			public int PacketId;

			public uint time;
		}

		private const int ChatHeaderLen = 4;

		private const int RealmHeaderLen = 3;

		private const int GameHeaderLen = 2;

		private SockType SocketType;

		private bool Firstpacket;

		private D2Socket.PacketLostEventHandler PacketLostEvent;

		private SortedList PacketInWait;

		private Thread PacketWaiting;

		public event D2Socket.PacketLostEventHandler PacketLost
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.PacketLostEvent = (D2Socket.PacketLostEventHandler)Delegate.Combine(this.PacketLostEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.PacketLostEvent = (D2Socket.PacketLostEventHandler)Delegate.Remove(this.PacketLostEvent, value);
			}
		}

		public abstract void ParsePacket(byte[] Packet);

		public D2Socket(SockType Type)
		{
			base.Disconnection += new Sockets.DisconnectionEventHandler(this.D2Socket_Disconnection);
			base.Connection += new Sockets.ConnectionEventHandler(this.D2Socket_Connection);
			this.Firstpacket = true;
			this.PacketInWait = new SortedList();
			this.SocketType = Type;
			this.PacketWaiting = new Thread(new ThreadStart(this.Waiting));
			this.PacketWaiting.IsBackground = true;
			this.PacketWaiting.Start();
		}

		private void Waiting()
		{
			checked
			{
				while (true)
				{
					Thread.Sleep(1000);
					if (this.PacketInWait.Count != 0)
					{
						SortedList packetInWait = this.PacketInWait;
						lock (packetInWait)
						{
							int arg_34_0 = 0;
							int num = this.PacketInWait.Count - 1;
							for (int i = arg_34_0; i <= num; i++)
							{
								if (Operators.ConditionalCompareObjectGreater(Operators.SubtractObject(Environment.TickCount, this.PacketInWait.GetByIndex(i)), 15000, false))
								{
									D2Socket.PacketLostEventHandler packetLostEvent = this.PacketLostEvent;
									if (packetLostEvent != null)
									{
										packetLostEvent(Conversions.ToInteger(this.PacketInWait.Keys.Cast<object>().ElementAtOrDefault(i)));
									}
									this.PacketInWait.Remove(i);
									break;
								}
							}
						}
					}
				}
			}
		}

		protected void ReportPacket(int PacketID)
		{
			SortedList packetInWait = this.PacketInWait;
			lock (packetInWait)
			{
				if (this.PacketInWait.ContainsKey(PacketID))
				{
					this.PacketInWait.Remove(PacketID);
				}
			}
		}

		public void WaitForPacket(int PacketID)
		{
			SortedList packetInWait = this.PacketInWait;
			lock (packetInWait)
			{
				if (this.PacketInWait.ContainsKey(PacketID))
				{
					this.PacketInWait.Remove(PacketID);
				}
				this.PacketInWait.Add(PacketID, Environment.TickCount);
			}
		}

		public override void DataReceived(int TotalLength)
		{
			checked
			{
				switch (this.SocketType)
				{
				case SockType.Chat:
					if (TotalLength >= 4)
					{
						byte[] array = new byte[5];
						this.PeekData(ref array, 4);
						if (array[0] == 255)
						{
							short num = (short)BitConverter.ToUInt16(array, 2);
							byte[] packet = new byte[(int)(num + 1)];
							if (TotalLength >= (int)num)
							{
								this.GetData(ref packet, (int)num);
								this.ParsePacket(packet);
							}
						}
					}
					break;
				case SockType.Realm:
					if (TotalLength >= 3)
					{
						byte[] array = new byte[4];
						this.PeekData(ref array, 3);
						short num = (short)BitConverter.ToUInt16(array, 0);
						byte[] packet = new byte[(int)(num + 1)];
						if (TotalLength >= (int)num)
						{
							this.GetData(ref packet, (int)num);
							this.ParsePacket(packet);
						}
					}
					break;
				case SockType.Game:
					if (TotalLength >= 2)
					{
						byte[] array = new byte[2];
						if (this.Firstpacket)
						{
							this.GetData(ref array, 2);
							this.Firstpacket = false;
							this.ParsePacket(array);
							return;
						}
						this.PeekData(ref array, 2);
						short num2 = (short)Compression.ComputeDataLength(array);
						if (TotalLength >= (int)num2)
						{
							byte[] packet2 = new byte[(int)(num2 + 1)];
							this.GetData(ref packet2, (int)num2);
							int num3 = 24567;
							byte[] buffer = new byte[num3 + 1];
							int num4 = Compression.DecompressPacket(packet2, ref buffer, num3);
							if (num4 != 0)
							{
								List<byte[]> list = Compression.SplitPackets(buffer, num4);
								if (list != null)
								{
									try
									{
										List<byte[]>.Enumerator enumerator = list.GetEnumerator();
										while (enumerator.MoveNext())
										{
											byte[] current = enumerator.Current;
											this.ParsePacket(current);
										}
									}
									finally
									{
										List<byte[]>.Enumerator enumerator;
										((IDisposable)enumerator).Dispose();
									}
								}
							}
						}
					}
					break;
				}
			}
		}

		private void D2Socket_Connection()
		{
			this.Firstpacket = true;
		}

		private void D2Socket_Disconnection()
		{
			this.Firstpacket = false;
		}
	}
}

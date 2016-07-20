using BotCore;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace BioBot
{
	public abstract class Sockets : IDisposable
	{
		public delegate void ConnectionEventHandler();

		public delegate void DisconnectionEventHandler();

		public delegate void FailToConnectEventHandler();

		public delegate void UpdateEventHandler(int Time);

		private Sockets.ConnectionEventHandler ConnectionEvent;

		private Sockets.DisconnectionEventHandler DisconnectionEvent;

		private Sockets.FailToConnectEventHandler FailToConnectEvent;

		private Sockets.UpdateEventHandler UpdateEvent;

		private Socket Tcpsocket;

		private bool g_bSckConnected;

		public IPAddress IpAddress;

		public ushort Port;

		private IPAddress ProxyAddress;

		private ushort ProxyPort;

		private string ProxyUsername;

		private string ProxyPassword;

		private int ProxyPacketSequence;

		private bool disposedValue;

		public event Sockets.ConnectionEventHandler Connection
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.ConnectionEvent = (Sockets.ConnectionEventHandler)Delegate.Combine(this.ConnectionEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.ConnectionEvent = (Sockets.ConnectionEventHandler)Delegate.Remove(this.ConnectionEvent, value);
			}
		}

		public event Sockets.DisconnectionEventHandler Disconnection
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.DisconnectionEvent = (Sockets.DisconnectionEventHandler)Delegate.Combine(this.DisconnectionEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.DisconnectionEvent = (Sockets.DisconnectionEventHandler)Delegate.Remove(this.DisconnectionEvent, value);
			}
		}

		public event Sockets.FailToConnectEventHandler FailToConnect
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.FailToConnectEvent = (Sockets.FailToConnectEventHandler)Delegate.Combine(this.FailToConnectEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.FailToConnectEvent = (Sockets.FailToConnectEventHandler)Delegate.Remove(this.FailToConnectEvent, value);
			}
		}

		public event Sockets.UpdateEventHandler Update
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.UpdateEvent = (Sockets.UpdateEventHandler)Delegate.Combine(this.UpdateEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.UpdateEvent = (Sockets.UpdateEventHandler)Delegate.Remove(this.UpdateEvent, value);
			}
		}

		public object Connected
		{
			get
			{
				return this.Tcpsocket.Connected;
			}
		}

		public abstract void DataReceived(int TotalLength);

		public Sockets()
		{
			this.Disconnection += new Sockets.DisconnectionEventHandler(this.Disconnected);
			this.Tcpsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.g_bSckConnected = false;
			this.ProxyPacketSequence = 0;
			this.disposedValue = false;
			new Thread(new ThreadStart(this.Readthread))
			{
				IsBackground = true
			}.Start();
			new Thread(new ThreadStart(this.StateThread))
			{
				IsBackground = true
			}.Start();
		}

		public void ConnectProxy()
		{
			Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				socket.Connect(this.ProxyAddress, (int)this.ProxyPort);
			}
			catch (Exception expr_1F)
			{
				ProjectData.SetProjectError(expr_1F);
                this.FailToConnectEvent?.Invoke();
                ProjectData.ClearProjectError();
				return;
			}
			DataBuffer dataBuffer = new DataBuffer();
			dataBuffer.InsertByte(5);
			dataBuffer.InsertByte(2);
			dataBuffer.InsertByte(0);
			dataBuffer.InsertByte(2);
			socket.Send(dataBuffer.GetData());
			byte[] array = new byte[256];
			socket.Receive(array, 2, SocketFlags.None);
			byte b = array[1];
			checked
			{
				if (b == 2)
				{
					DataBuffer dataBuffer2 = new DataBuffer();
					dataBuffer2.InsertByte(1);
					dataBuffer2.InsertByte(BitConverter.GetBytes(this.ProxyUsername.Length)[0]);
					dataBuffer2.Insert(Encoding.Default.GetBytes(this.ProxyUsername));
					dataBuffer2.InsertByte(BitConverter.GetBytes(this.ProxyPassword.Length)[0]);
					dataBuffer2.Insert(Encoding.Default.GetBytes(this.ProxyPassword));
					socket.Send(dataBuffer2.GetData());
					socket.Receive(array, 2, SocketFlags.None);
					if (array[1] != 0)
					{
						socket.Close();
						Sockets.FailToConnectEventHandler failToConnectEvent = this.FailToConnectEvent;
						if (failToConnectEvent != null)
						{
							failToConnectEvent();
							return;
						}
					}
					else
					{
						DataBuffer dataBuffer3 = new DataBuffer();
						dataBuffer3.InsertByte(5);
						dataBuffer3.InsertByte(1);
						dataBuffer3.InsertByte(0);
						AddressFamily addressFamily = this.IpAddress.AddressFamily;
						if (addressFamily == AddressFamily.InterNetwork)
						{
							dataBuffer3.InsertByte(1);
						}
						else if (addressFamily == AddressFamily.InterNetworkV6)
						{
							dataBuffer3.InsertByte(4);
						}
						dataBuffer3.InsertByteArray(this.IpAddress.GetAddressBytes());
						byte[] bytes = BitConverter.GetBytes(this.Port);
						for (int i = bytes.Length - 1; i >= 0; i--)
						{
							dataBuffer3.InsertByte(bytes[i]);
						}
						socket.Send(dataBuffer3.GetData());
						socket.Receive(array, 2, SocketFlags.None);
						byte b2 = array[1];
						if (b2 != 0)
						{
							socket.Close();
							Sockets.FailToConnectEventHandler failToConnectEvent = this.FailToConnectEvent;
							if (failToConnectEvent != null)
							{
								failToConnectEvent();
								return;
							}
						}
						else
						{
							AddressFamily addressFamily2 = this.IpAddress.AddressFamily;
							if (addressFamily2 == AddressFamily.InterNetwork)
							{
								socket.Receive(array, 8, SocketFlags.None);
							}
							else if (addressFamily2 == AddressFamily.InterNetworkV6)
							{
								socket.Receive(array, 20, SocketFlags.None);
							}
							this.Tcpsocket = socket;
						}
					}
				}
				else
				{
					socket.Close();
					Sockets.FailToConnectEventHandler failToConnectEvent = this.FailToConnectEvent;
					if (failToConnectEvent != null)
					{
						failToConnectEvent();
						return;
					}
				}
			}
		}

		public Sockets(Socket Socket) : this()
		{
			this.Tcpsocket = Socket;
		}

		public void StateThread()
		{
			while (true)
			{
				Thread.Sleep(20);
				if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(this.Connected, this.g_bSckConnected, false))))
				{
					this.g_bSckConnected = !this.g_bSckConnected;
					switch (this.g_bSckConnected)
					{
					case false:
					{
                                this.DisconnectionEvent?.Invoke();
                                break;
					}
					case true:
					{
                                this.ConnectionEvent?.Invoke();
                                break;
					}
					}
				}
			}
		}

		public void Readthread()
		{
			while (true)
			{
				Thread.Sleep(10);
				int num = 0;
				if (this.Tcpsocket != null && this.Tcpsocket.Connected)
				{
					num = this.Tcpsocket.Available;
                    this.UpdateEvent?.Invoke(Environment.TickCount);
                }
				if (num > 0)
				{
					this.DataReceived(num);
				}
			}
		}

		public void Connect(string IpAddress, ushort Port)
		{
			try
			{
				this.IpAddress = IPAddress.Parse(IpAddress);
			}
			catch (Exception expr_0E)
			{
				ProjectData.SetProjectError(expr_0E);
				this.IpAddress = Dns.GetHostEntry(IpAddress).AddressList[0];
				ProjectData.ClearProjectError();
			}
			this.Port = Port;
			new Thread(new ThreadStart(this.TryConnect))
			{
				IsBackground = true
			}.Start();
		}

		public void Connect(string IpAddress, ushort Port, string ProxyAddress, string ProxyPort, string Username, string Password)
		{
			try
			{
				this.IpAddress = IPAddress.Parse(IpAddress);
			}
			catch (Exception expr_0E)
			{
				ProjectData.SetProjectError(expr_0E);
				this.IpAddress = Dns.GetHostEntry(IpAddress).AddressList[0];
				ProjectData.ClearProjectError();
			}
			try
			{
				this.ProxyAddress = IPAddress.Parse(ProxyAddress);
			}
			catch (Exception expr_3D)
			{
				ProjectData.SetProjectError(expr_3D);
				this.ProxyAddress = Dns.GetHostEntry(ProxyAddress).AddressList[0];
				ProjectData.ClearProjectError();
			}
			this.Port = Port;
			this.ProxyPort = Conversions.ToUShort(ProxyPort);
			this.ProxyUsername = Username;
			this.ProxyPassword = Password;
			this.ConnectProxy();
		}

		private void TryConnect()
		{
			this.Disconnect();
			this.Tcpsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				this.Tcpsocket.Connect(this.IpAddress, (int)this.Port);
			}
			catch (Exception expr_2D)
			{
				ProjectData.SetProjectError(expr_2D);
                this.FailToConnectEvent?.Invoke();
                ProjectData.ClearProjectError();
			}
		}

		protected void Raisevent_Connected()
		{
            this.ConnectionEvent?.Invoke();
        }

		public void Disconnect()
		{
			Socket tcpsocket = this.Tcpsocket;
			lock (tcpsocket)
			{
				try
				{
					this.Tcpsocket.Shutdown(SocketShutdown.Both);
				}
				catch (Exception arg_1B_0)
				{
					ProjectData.SetProjectError(arg_1B_0);
					ProjectData.ClearProjectError();
				}
				this.Tcpsocket.Close();
			}
		}

		public void Disconnected()
		{
			this.Disconnect();
		}

		public void GetData(ref byte[] Buffer, int Length)
		{
			try
			{
				this.Tcpsocket.Receive(Buffer, 0, Length, SocketFlags.None);
			}
			catch (Exception expr_13)
			{
				ProjectData.SetProjectError(expr_13);
				this.Disconnect();
				ProjectData.ClearProjectError();
			}
		}

		public void PeekData(ref byte[] Buffer, int Length)
		{
			try
			{
				Socket tcpsocket = this.Tcpsocket;
				lock (tcpsocket)
				{
					this.Tcpsocket.Receive(Buffer, 0, Length, SocketFlags.Peek);
				}
			}
			catch (Exception expr_29)
			{
				ProjectData.SetProjectError(expr_29);
				this.Disconnect();
				ProjectData.ClearProjectError();
			}
		}

		public void SendPacket(byte[] bytes)
		{
			if (Conversions.ToBoolean(this.Connected))
			{
				try
				{
					Socket tcpsocket = this.Tcpsocket;
					lock (tcpsocket)
					{
						this.Tcpsocket.Send(bytes);
					}
				}
				catch (Exception expr_32)
				{
					ProjectData.SetProjectError(expr_32);
					this.Disconnect();
					ProjectData.ClearProjectError();
				}
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
			}
			this.disposedValue = true;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}

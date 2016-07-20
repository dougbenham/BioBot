using BotCore.RealmClient;
using BotCore.RealmServer;
using MBNCSUtil;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BioBot
{
	public class RealmSocket : D2Socket
	{
		public delegate void OnCharacterCreationResponseEventHandler(CharacterCreationResponse Packet);

		public delegate void OnCharacterDeletionResponseEventHandler(CharacterDeletionResponse Packet);

		public delegate void OnCharacterListEventHandler(CharacterList Packet);

		public delegate void OnCharacterLogonResponseEventHandler(CharacterLogonResponse Packet);

		public delegate void OnCharacterUpgradeResponseEventHandler(CharacterUpgradeResponse Packet);

		public delegate void OnCreateGameResponseEventHandler(CreateGameResponse Packet);

		public delegate void OnGameCreationQueueEventHandler(GameCreationQueue Packet);

		public delegate void OnGameInfoEventHandler(GameInfo Packet);

		public delegate void OnGameListEventHandler(GameList Packet);

		public delegate void OnJoinGameResponseEventHandler(JoinGameResponse Packet);

		public delegate void OnMessageOfTheDayEventHandler(MessageOfTheDay Packet);

		public delegate void OnRealmStartupResponseEventHandler(RealmStartupResponse Packet);

		private RealmSocket.OnCharacterCreationResponseEventHandler OnCharacterCreationResponseEvent;

		private RealmSocket.OnCharacterDeletionResponseEventHandler OnCharacterDeletionResponseEvent;

		private RealmSocket.OnCharacterListEventHandler OnCharacterListEvent;

		private RealmSocket.OnCharacterLogonResponseEventHandler OnCharacterLogonResponseEvent;

		private RealmSocket.OnCharacterUpgradeResponseEventHandler OnCharacterUpgradeResponseEvent;

		private RealmSocket.OnCreateGameResponseEventHandler OnCreateGameResponseEvent;

		private RealmSocket.OnGameCreationQueueEventHandler OnGameCreationQueueEvent;

		private RealmSocket.OnGameInfoEventHandler OnGameInfoEvent;

		private RealmSocket.OnGameListEventHandler OnGameListEvent;

		private RealmSocket.OnJoinGameResponseEventHandler OnJoinGameResponseEvent;

		private RealmSocket.OnMessageOfTheDayEventHandler OnMessageOfTheDayEvent;

		private RealmSocket.OnRealmStartupResponseEventHandler OnRealmStartupResponseEvent;

		private LogBox Log;

		private string g_sUniqueUsername;

		private IPAddress g_ServerIp;

		private byte[] g_StartupData;

		public event RealmSocket.OnCharacterCreationResponseEventHandler OnCharacterCreationResponse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnCharacterCreationResponseEvent = (RealmSocket.OnCharacterCreationResponseEventHandler)Delegate.Combine(this.OnCharacterCreationResponseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnCharacterCreationResponseEvent = (RealmSocket.OnCharacterCreationResponseEventHandler)Delegate.Remove(this.OnCharacterCreationResponseEvent, value);
			}
		}

		public event RealmSocket.OnCharacterDeletionResponseEventHandler OnCharacterDeletionResponse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnCharacterDeletionResponseEvent = (RealmSocket.OnCharacterDeletionResponseEventHandler)Delegate.Combine(this.OnCharacterDeletionResponseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnCharacterDeletionResponseEvent = (RealmSocket.OnCharacterDeletionResponseEventHandler)Delegate.Remove(this.OnCharacterDeletionResponseEvent, value);
			}
		}

		public event RealmSocket.OnCharacterListEventHandler OnCharacterList
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnCharacterListEvent = (RealmSocket.OnCharacterListEventHandler)Delegate.Combine(this.OnCharacterListEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnCharacterListEvent = (RealmSocket.OnCharacterListEventHandler)Delegate.Remove(this.OnCharacterListEvent, value);
			}
		}

		public event RealmSocket.OnCharacterLogonResponseEventHandler OnCharacterLogonResponse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnCharacterLogonResponseEvent = (RealmSocket.OnCharacterLogonResponseEventHandler)Delegate.Combine(this.OnCharacterLogonResponseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnCharacterLogonResponseEvent = (RealmSocket.OnCharacterLogonResponseEventHandler)Delegate.Remove(this.OnCharacterLogonResponseEvent, value);
			}
		}

		public event RealmSocket.OnCharacterUpgradeResponseEventHandler OnCharacterUpgradeResponse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnCharacterUpgradeResponseEvent = (RealmSocket.OnCharacterUpgradeResponseEventHandler)Delegate.Combine(this.OnCharacterUpgradeResponseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnCharacterUpgradeResponseEvent = (RealmSocket.OnCharacterUpgradeResponseEventHandler)Delegate.Remove(this.OnCharacterUpgradeResponseEvent, value);
			}
		}

		public event RealmSocket.OnCreateGameResponseEventHandler OnCreateGameResponse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnCreateGameResponseEvent = (RealmSocket.OnCreateGameResponseEventHandler)Delegate.Combine(this.OnCreateGameResponseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnCreateGameResponseEvent = (RealmSocket.OnCreateGameResponseEventHandler)Delegate.Remove(this.OnCreateGameResponseEvent, value);
			}
		}

		public event RealmSocket.OnGameCreationQueueEventHandler OnGameCreationQueue
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameCreationQueueEvent = (RealmSocket.OnGameCreationQueueEventHandler)Delegate.Combine(this.OnGameCreationQueueEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameCreationQueueEvent = (RealmSocket.OnGameCreationQueueEventHandler)Delegate.Remove(this.OnGameCreationQueueEvent, value);
			}
		}

		public event RealmSocket.OnGameInfoEventHandler OnGameInfo
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameInfoEvent = (RealmSocket.OnGameInfoEventHandler)Delegate.Combine(this.OnGameInfoEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameInfoEvent = (RealmSocket.OnGameInfoEventHandler)Delegate.Remove(this.OnGameInfoEvent, value);
			}
		}

		public event RealmSocket.OnGameListEventHandler OnGameList
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnGameListEvent = (RealmSocket.OnGameListEventHandler)Delegate.Combine(this.OnGameListEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnGameListEvent = (RealmSocket.OnGameListEventHandler)Delegate.Remove(this.OnGameListEvent, value);
			}
		}

		public event RealmSocket.OnJoinGameResponseEventHandler OnJoinGameResponse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnJoinGameResponseEvent = (RealmSocket.OnJoinGameResponseEventHandler)Delegate.Combine(this.OnJoinGameResponseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnJoinGameResponseEvent = (RealmSocket.OnJoinGameResponseEventHandler)Delegate.Remove(this.OnJoinGameResponseEvent, value);
			}
		}

		public event RealmSocket.OnMessageOfTheDayEventHandler OnMessageOfTheDay
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnMessageOfTheDayEvent = (RealmSocket.OnMessageOfTheDayEventHandler)Delegate.Combine(this.OnMessageOfTheDayEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnMessageOfTheDayEvent = (RealmSocket.OnMessageOfTheDayEventHandler)Delegate.Remove(this.OnMessageOfTheDayEvent, value);
			}
		}

		public event RealmSocket.OnRealmStartupResponseEventHandler OnRealmStartupResponse
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.OnRealmStartupResponseEvent = (RealmSocket.OnRealmStartupResponseEventHandler)Delegate.Combine(this.OnRealmStartupResponseEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.OnRealmStartupResponseEvent = (RealmSocket.OnRealmStartupResponseEventHandler)Delegate.Remove(this.OnRealmStartupResponseEvent, value);
			}
		}

		public RealmSocket(ref LogBox LogWriter) : base(SockType.Realm)
		{
			base.Connection += new Sockets.ConnectionEventHandler(this.Realm_Connection);
			base.Disconnection += new Sockets.DisconnectionEventHandler(this.OnDisconnect);
			this.Log = LogWriter;
		}

		public void Connect(byte[] StartupData, string UniqueUsername, IPAddress ServerIp, ProxyInfo Proxy = null)
		{
			this.g_sUniqueUsername = UniqueUsername;
			this.g_ServerIp = ServerIp;
			this.g_StartupData = StartupData;
			int num = 6112;
			checked
			{
				if (Proxy == null)
				{
					this.WriteToLog("Connecting: " + this.g_ServerIp.ToString(), Color.Gray);
					base.Connect(this.g_ServerIp.ToString(), (ushort)num);
				}
				else
				{
					this.WriteToLog("Connecting W/ Proxy: " + this.g_ServerIp.ToString(), Color.Gray);
					base.Connect(this.g_ServerIp.ToString(), (ushort)num, Proxy.Address, Conversions.ToString((uint)Proxy.Port), Proxy.Username, Proxy.Password);
				}
			}
		}

		private void OnDisconnect()
		{
			this.WriteToLog("Connection Lost..", Color.Red);
		}

		private void Realm_Connection()
		{
			byte[] bytes = new byte[]
			{
				1
			};
			this.SendPacket(bytes);
			Thread.Sleep(50);
			RealmStartupRequest packet = new RealmStartupRequest(this.g_StartupData, this.g_sUniqueUsername);
			this.SendPacket(packet);
			this.WriteToLog("Authorizing..", Color.Gray);
		}

		public override void ParsePacket(byte[] Data)
		{
			DataReader dataReader = new DataReader(Data);
			int num = (int)dataReader.ReadInt16();
			RealmServerPacket packetID = (RealmServerPacket)dataReader.ReadByte();
			this.ReportPacket((int)packetID);
			switch (packetID)
			{
			case RealmServerPacket.RealmStartupResponse:
			{
				RealmSocket.OnRealmStartupResponseEventHandler onRealmStartupResponseEvent = this.OnRealmStartupResponseEvent;
				if (onRealmStartupResponseEvent != null)
				{
					onRealmStartupResponseEvent(new RealmStartupResponse(Data));
				}
				break;
			}
			case RealmServerPacket.CharacterCreationResponse:
			{
				RealmSocket.OnCharacterCreationResponseEventHandler onCharacterCreationResponseEvent = this.OnCharacterCreationResponseEvent;
				if (onCharacterCreationResponseEvent != null)
				{
					onCharacterCreationResponseEvent(new CharacterCreationResponse(Data));
				}
				break;
			}
			case RealmServerPacket.CreateGameResponse:
			{
				RealmSocket.OnCreateGameResponseEventHandler onCreateGameResponseEvent = this.OnCreateGameResponseEvent;
				if (onCreateGameResponseEvent != null)
				{
					onCreateGameResponseEvent(new CreateGameResponse(Data));
				}
				break;
			}
			case RealmServerPacket.JoinGameResponse:
			{
				RealmSocket.OnJoinGameResponseEventHandler onJoinGameResponseEvent = this.OnJoinGameResponseEvent;
				if (onJoinGameResponseEvent != null)
				{
					onJoinGameResponseEvent(new JoinGameResponse(Data));
				}
				break;
			}
			case RealmServerPacket.GameList:
			{
				RealmSocket.OnGameListEventHandler onGameListEvent = this.OnGameListEvent;
				if (onGameListEvent != null)
				{
					onGameListEvent(new GameList(Data));
				}
				break;
			}
			case RealmServerPacket.GameInfo:
			{
				RealmSocket.OnGameInfoEventHandler onGameInfoEvent = this.OnGameInfoEvent;
				if (onGameInfoEvent != null)
				{
					onGameInfoEvent(new GameInfo(Data));
				}
				break;
			}
			case RealmServerPacket.CharacterLogonResponse:
			{
				RealmSocket.OnCharacterLogonResponseEventHandler onCharacterLogonResponseEvent = this.OnCharacterLogonResponseEvent;
				if (onCharacterLogonResponseEvent != null)
				{
					onCharacterLogonResponseEvent(new CharacterLogonResponse(Data));
				}
				break;
			}
			case RealmServerPacket.CharacterDeletionResponse:
			{
				RealmSocket.OnCharacterDeletionResponseEventHandler onCharacterDeletionResponseEvent = this.OnCharacterDeletionResponseEvent;
				if (onCharacterDeletionResponseEvent != null)
				{
					onCharacterDeletionResponseEvent(new CharacterDeletionResponse(Data));
				}
				break;
			}
			case RealmServerPacket.MessageOfTheDay:
			{
				RealmSocket.OnMessageOfTheDayEventHandler onMessageOfTheDayEvent = this.OnMessageOfTheDayEvent;
				if (onMessageOfTheDayEvent != null)
				{
					onMessageOfTheDayEvent(new MessageOfTheDay(Data));
				}
				break;
			}
			case RealmServerPacket.GameCreationQueue:
			{
				RealmSocket.OnGameCreationQueueEventHandler onGameCreationQueueEvent = this.OnGameCreationQueueEvent;
				if (onGameCreationQueueEvent != null)
				{
					onGameCreationQueueEvent(new GameCreationQueue(Data));
				}
				break;
			}
			case RealmServerPacket.CharacterUpgradeResponse:
			{
				RealmSocket.OnCharacterUpgradeResponseEventHandler onCharacterUpgradeResponseEvent = this.OnCharacterUpgradeResponseEvent;
				if (onCharacterUpgradeResponseEvent != null)
				{
					onCharacterUpgradeResponseEvent(new CharacterUpgradeResponse(Data));
				}
				break;
			}
			case RealmServerPacket.CharacterList:
			{
				RealmSocket.OnCharacterListEventHandler onCharacterListEvent = this.OnCharacterListEvent;
				if (onCharacterListEvent != null)
				{
					onCharacterListEvent(new CharacterList(Data));
				}
				break;
			}
			}
		}

		public void SendPacket(RCPacket Packet)
		{
			this.SendPacket(Packet.Data);
		}

		public void WriteToLog(string Text, Color Color)
		{
			this.Log.AddLine(Text, Color, HorizontalAlignment.Left);
		}
	}
}

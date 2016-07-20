using BioBot.Game;
using BotCore.BnetClient;
using BotCore.BnetServer;
using BotCore.GameClient;
using BotCore.GameServer;
using BotCore.RealmClient;
using BotCore.RealmServer;
using D2Data;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BioBot
{
	public abstract class ConnectionManager
	{
		protected delegate void FailToJoinGameEventHandler();

		protected delegate void FailToCreateGameEventHandler();

		[AccessedThroughProperty("Chat")]
		private ChatSocket _Chat;

		[AccessedThroughProperty("Game")]
		private GameSocket _Game;

		protected uint D2GHash;

		private uint D2Token;

		protected LogBox Log;

		protected ConnectInfo Infos;

		protected Hero_t Hero;

		public ProxyInfo Proxy;

		public bool Initialized;

		private ConnectionManager.FailToJoinGameEventHandler FailToJoinGameEvent;

		private ConnectionManager.FailToCreateGameEventHandler FailToCreateGameEvent;

		[AccessedThroughProperty("Realm")]
		private RealmSocket _Realm;

		private ushort ReqID;

		protected event ConnectionManager.FailToJoinGameEventHandler FailToJoinGame
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.FailToJoinGameEvent = (ConnectionManager.FailToJoinGameEventHandler)Delegate.Combine(this.FailToJoinGameEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.FailToJoinGameEvent = (ConnectionManager.FailToJoinGameEventHandler)Delegate.Remove(this.FailToJoinGameEvent, value);
			}
		}

		protected event ConnectionManager.FailToCreateGameEventHandler FailToCreateGame
		{
			[MethodImpl(MethodImplOptions.Synchronized)]
			add
			{
				this.FailToCreateGameEvent = (ConnectionManager.FailToCreateGameEventHandler)Delegate.Combine(this.FailToCreateGameEvent, value);
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			remove
			{
				this.FailToCreateGameEvent = (ConnectionManager.FailToCreateGameEventHandler)Delegate.Remove(this.FailToCreateGameEvent, value);
			}
		}

		protected virtual ChatSocket Chat
		{
			get
			{
				return this._Chat;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ChatSocket.OnServerKeepAliveEventHandler obj = new ChatSocket.OnServerKeepAliveEventHandler(this.ChatSocket_OnServerKeepAlive);
				ChatSocket.OnBnetAuthResponseEventHandler obj2 = new ChatSocket.OnBnetAuthResponseEventHandler(this.ChatSocket_OnBnetAuthResponse);
				ChatSocket.OnBnetPingEventHandler obj3 = new ChatSocket.OnBnetPingEventHandler(this.ChatSocket_OnBnetPing);
				ChatSocket.OnBnetConnectionResponseEventHandler obj4 = new ChatSocket.OnBnetConnectionResponseEventHandler(this.ChatSocket_OnBnetConnectionResponse);
				ChatSocket.OnBnetLogonResponseEventHandler obj5 = new ChatSocket.OnBnetLogonResponseEventHandler(this.ChatSocket_OnBnetLogonResponse);
				ChatSocket.OnRealmLogonResponseEventHandler obj6 = new ChatSocket.OnRealmLogonResponseEventHandler(this.ChatSocket_OnRealmLogonResponse);
				if (this._Chat != null)
				{
					this._Chat.OnServerKeepAlive -= obj;
					this._Chat.OnBnetAuthResponse -= obj2;
					this._Chat.OnBnetPing -= obj3;
					this._Chat.OnBnetConnectionResponse -= obj4;
					this._Chat.OnBnetLogonResponse -= obj5;
					this._Chat.OnRealmLogonResponse -= obj6;
				}
				this._Chat = value;
				if (this._Chat != null)
				{
					this._Chat.OnServerKeepAlive += obj;
					this._Chat.OnBnetAuthResponse += obj2;
					this._Chat.OnBnetPing += obj3;
					this._Chat.OnBnetConnectionResponse += obj4;
					this._Chat.OnBnetLogonResponse += obj5;
					this._Chat.OnRealmLogonResponse += obj6;
				}
			}
		}

		protected virtual GameSocket Game
		{
			get
			{
				return this._Game;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				GameSocket.OnPlayerReassignEventHandler obj = new GameSocket.OnPlayerReassignEventHandler(this.Game_OnPlayerReassign);
				GameSocket.OnPlayerMoveEventHandler obj2 = new GameSocket.OnPlayerMoveEventHandler(this.Game_OnPlayerMove);
				GameSocket.OnGameHandshakeEventHandler obj3 = new GameSocket.OnGameHandshakeEventHandler(this.Game_OnGameHandshake);
				GameSocket.OnRequestLogonInfoEventHandler obj4 = new GameSocket.OnRequestLogonInfoEventHandler(this.GameSocket_OnRequestLogonInfo);
				GameSocket.OnGameLogonSuccessEventHandler obj5 = new GameSocket.OnGameLogonSuccessEventHandler(this.GameSocket_OnGameLogonSuccess);
				GameSocket.OnGameLoadingEventHandler obj6 = new GameSocket.OnGameLoadingEventHandler(this.GameSocket_OnGameLoading);
				GameSocket.OnGameLogonReceiptEventHandler obj7 = new GameSocket.OnGameLogonReceiptEventHandler(this.GameSocket_OnGameLogonReceipt);
				Sockets.FailToConnectEventHandler obj8 = new Sockets.FailToConnectEventHandler(this.Game_FailToConnect);
				D2Socket.PacketLostEventHandler obj9 = delegate(int a0)
				{
					this.Game_PacketLost((GameServerPacket)a0);
				};
				GameSocket.OnPlayerLifeManaChangeEventHandler obj10 = new GameSocket.OnPlayerLifeManaChangeEventHandler(this.Game_OnPlayerLifeManaChange);
				if (this._Game != null)
				{
					this._Game.OnPlayerReassign -= obj;
					this._Game.OnPlayerMove -= obj2;
					this._Game.OnGameHandshake -= obj3;
					this._Game.OnRequestLogonInfo -= obj4;
					this._Game.OnGameLogonSuccess -= obj5;
					this._Game.OnGameLoading -= obj6;
					this._Game.OnGameLogonReceipt -= obj7;
					this._Game.FailToConnect -= obj8;
					this._Game.PacketLost -= obj9;
					this._Game.OnPlayerLifeManaChange -= obj10;
				}
				this._Game = value;
				if (this._Game != null)
				{
					this._Game.OnPlayerReassign += obj;
					this._Game.OnPlayerMove += obj2;
					this._Game.OnGameHandshake += obj3;
					this._Game.OnRequestLogonInfo += obj4;
					this._Game.OnGameLogonSuccess += obj5;
					this._Game.OnGameLoading += obj6;
					this._Game.OnGameLogonReceipt += obj7;
					this._Game.FailToConnect += obj8;
					this._Game.PacketLost += obj9;
					this._Game.OnPlayerLifeManaChange += obj10;
				}
			}
		}

		protected virtual RealmSocket Realm
		{
			get
			{
				return this._Realm;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RealmSocket.OnJoinGameResponseEventHandler obj = new RealmSocket.OnJoinGameResponseEventHandler(this.RealmSocket_OnJoinGameResponse);
				RealmSocket.OnCharacterLogonResponseEventHandler obj2 = new RealmSocket.OnCharacterLogonResponseEventHandler(this.Log_OnCharacterLogonResponse);
				Sockets.DisconnectionEventHandler obj3 = new Sockets.DisconnectionEventHandler(this.RealmSocket_OnDisconnected);
				RealmSocket.OnCharacterLogonResponseEventHandler obj4 = new RealmSocket.OnCharacterLogonResponseEventHandler(this.RealmSocket_OnCharacterLogonResponse);
				RealmSocket.OnCreateGameResponseEventHandler obj5 = new RealmSocket.OnCreateGameResponseEventHandler(this.Realm_OnCreateGameResponse);
				D2Socket.PacketLostEventHandler obj6 = delegate(int a0)
				{
					this.Realm_PacketLost(checked((RealmServerPacket)a0));
				};
				RealmSocket.OnRealmStartupResponseEventHandler obj7 = new RealmSocket.OnRealmStartupResponseEventHandler(this.RealmSocket_OnRealmStartupResponse);
				RealmSocket.OnCharacterListEventHandler obj8 = new RealmSocket.OnCharacterListEventHandler(this.RealmSocket_OnCharacterList);
				if (this._Realm != null)
				{
					this._Realm.OnJoinGameResponse -= obj;
					this._Realm.OnCharacterLogonResponse -= obj2;
					this._Realm.Disconnection -= obj3;
					this._Realm.OnCharacterLogonResponse -= obj4;
					this._Realm.OnCreateGameResponse -= obj5;
					this._Realm.PacketLost -= obj6;
					this._Realm.OnRealmStartupResponse -= obj7;
					this._Realm.OnCharacterList -= obj8;
				}
				this._Realm = value;
				if (this._Realm != null)
				{
					this._Realm.OnJoinGameResponse += obj;
					this._Realm.OnCharacterLogonResponse += obj2;
					this._Realm.Disconnection += obj3;
					this._Realm.OnCharacterLogonResponse += obj4;
					this._Realm.OnCreateGameResponse += obj5;
					this._Realm.PacketLost += obj6;
					this._Realm.OnRealmStartupResponse += obj7;
					this._Realm.OnCharacterList += obj8;
				}
			}
		}

		public ConnectionManager()
		{
			this.Initialized = false;
			this.ReqID = 1;
		}

		protected void ConnectChat(ref LogBox LogWriter, ConnectInfo Information)
		{
			this.Log = LogWriter;
			this.Disconnect();
			this.Initialized = true;
			this.Chat = new ChatSocket(ref LogWriter);
			this.Realm = new RealmSocket(ref LogWriter);
			this.Game = new GameSocket(ref LogWriter);
			this.Infos = Information;
			this.Hero.Account = this.Infos.BnetUserName;
			this.Chat.Connect(Information, this.Proxy);
		}

		private void ChatSocket_OnBnetConnectionResponse(BnetConnectionResponse packet)
		{
			this.Chat.WriteToLog("[0x" + Conversions.ToString(packet.Packetid) + "] Attempting to authorize.", Color.Gray);
		}

		private void ChatSocket_OnBnetAuthResponse(BnetAuthResponse packet)
		{
			BnetAuthResult result = packet.result;
			if (result == BnetAuthResult.Success)
			{
				this.Chat.WriteToLog("[0x" + Conversions.ToString(packet.Packetid) + "] Authenticated", Color.Gray);
			}
			else if (result == BnetAuthResult.CDKeyInUse || result == BnetAuthResult.BannedCDKey || result == BnetAuthResult.InvalidCDKey)
			{
				this.Chat.WriteToLog(string.Concat(new string[]
				{
					packet.result.ToString(),
					":   ",
					this.Infos.ClassicCdKey,
					"    /     ",
					this.Infos.ExpCdKey
				}), Color.DarkRed);
				this.Disconnect();
			}
			else
			{
				this.Chat.WriteToLog("[0x" + Conversions.ToString(packet.Packetid) + "]Authentication failed: " + packet.result, Color.Red);
				this.Disconnect();
			}
		}

		protected virtual void ChatSocket_OnBnetLogonResponse(BnetLogonResponse Packet)
		{
			if (Packet.result == BnetLogonResult.Success)
			{
				this.Chat.WriteToLog("[0x" + Conversions.ToString(Packet.Packetid) + "] Querying realms..", Color.Gray);
				QueryRealms bnetPacket = new QueryRealms();
				this.Chat.SendPacket(bnetPacket);
			}
			else
			{
				this.Chat.WriteToLog("[0x" + Conversions.ToString(Packet.Packetid) + "]Logon Failed: " + Packet.result, Color.Red);
				if (Operators.CompareString(Packet.reason, "", false) != 0)
				{
					this.Chat.WriteToLog("[0x" + Conversions.ToString(Packet.Packetid) + "]Reason: " + Packet.reason, Color.Red);
				}
				this.Disconnect();
			}
		}

		protected virtual void ChatSocket_OnBnetPing(BnetPing Packet)
		{
			BnetPong bnetPacket = new BnetPong(Packet.timestamp);
			this.Chat.SendPacket(bnetPacket);
		}

		protected virtual void ChatSocket_OnServerKeepAlive(BotCore.BnetServer.KeepAlive packet)
		{
			BotCore.BnetClient.KeepAlive bnetPacket = new BotCore.BnetClient.KeepAlive();
			this.Chat.SendPacket(bnetPacket);
		}

		protected virtual void ChatSocket_OnRealmLogonResponse(RealmLogonResponse Packet)
		{
			RealmLogonResult result = Packet.result;
			if (result == RealmLogonResult.Success)
			{
				this.Chat.WriteToLog("[0x" + Conversions.ToString(Packet.Packetid) + "] Information received. Connecting to realm.", Color.Gray);
				this.Realm.Connect(Packet.StartupData, Packet.username, Packet.realmServerIP, this.Proxy);
			}
			else
			{
				this.Chat.WriteToLog("[0x" + Conversions.ToString(Packet.Packetid) + "]Connection Failed, Reason: " + Packet.result, Color.Red);
				this.Chat.Disconnect();
			}
		}

		private void Game_PacketLost(GameServerPacket PacketID)
		{
			if (PacketID == GameServerPacket.GameLogonSuccess)
			{
				this.Game.WriteToLog("Game Connection TimedOut", Color.Orange);
                this.FailToJoinGameEvent?.Invoke();
            }
		}

		private void Game_FailToConnect()
		{
            this.FailToJoinGameEvent?.Invoke();
        }

		private void GameSocket_OnGameLogonReceipt(GameLogonReceipt Packet)
		{
			this.Hero.Difficulty = Packet.Difficulty;
			this.Hero.Expansion = Packet.Expansion;
			this.Hero.Ladder = Packet.Ladder;
			this.Hero.HardCore = Packet.Hardcore;
			this.Game.WriteToLog("Joining Game", Color.Yellow);
		}

		private void GameSocket_OnGameLoading(GameLoading Packet)
		{
			this.Game.WriteToLog("Game Loading", Color.Yellow);
		}

		protected virtual void GameSocket_OnGameLogonSuccess(GameLogonSuccess Packet)
		{
			this.Game.WriteToLog("Entered Game", Color.Green);
			EnterGame packet = new EnterGame();
			this.Game.SendPacket(packet);
		}

		protected virtual void GameSocket_OnRequestLogonInfo(RequestLogonInfo Packet)
		{
			GameLogonRequest packet = new GameLogonRequest(this.D2GHash, this.D2Token, this.Hero.Name, (CharacterClass)this.Hero.Class, 13);
			this.Game.SendPacket(packet);
		}

		private void Game_OnGameHandshake(GameHandshake Packet)
		{
			this.Hero.UID = Packet.UID;
		}

		private void Game_OnPlayerMove(PlayerMove Packet)
		{
			if (Packet.UID == this.Hero.UID)
			{
				this.Hero.Position.X = Packet.TargetX;
				this.Hero.Position.Y = Packet.TargetY;
			}
		}

		private void Game_OnPlayerReassign(PlayerReassign Packet)
		{
			if (Packet.UID == this.Hero.UID)
			{
				this.Hero.Position.X = Packet.X;
				this.Hero.Position.Y = Packet.Y;
			}
		}

		private void Game_OnPlayerLifeManaChange(PlayerLifeManaChange Packet)
		{
			this.Hero.Position.X = Packet.X;
			this.Hero.Position.Y = Packet.Y;
			this.Hero.Mana = Packet.Mana;
			this.Hero.Life = Packet.Life;
		}

		public abstract void Initialize(ushort Index, ref LogBox Log, ConnectInfo BnetInfos);

		protected abstract void Destroy();

		public void Disconnect()
		{
			this.Initialized = false;
			if (this.Chat != null)
			{
				this.Chat.Disconnect();
			}
			if (this.Realm != null)
			{
				this.Realm.Disconnect();
			}
			if (this.Game != null)
			{
				this.Game.Disconnect();
			}
			this.D2GHash = 0u;
			this.D2Token = 0u;
			this.ReqID = 1;
			this.Destroy();
		}

		protected virtual void RealmSocket_OnCharacterList(CharacterList Packet)
		{
			int arg_0D_0 = 0;
			checked
			{
				int num = (int)(unchecked((ulong)Packet.Listed) - 1uL);
				for (int i = arg_0D_0; i <= num; i++)
				{
					if (this.Infos.CharName == null)
					{
						this.Infos.CharName = "";
					}
					if (Operators.CompareString(this.Infos.CharName, "", false) == 0 | Operators.CompareString(Packet.Characters[i].Name.ToLower(), this.Infos.CharName.ToLower(), false) == 0)
					{
						CharacterLogonRequest packet = new CharacterLogonRequest(Packet.Characters[i].Name);
						this.Realm.SendPacket(packet);
						this.Hero.Name = Packet.Characters[i].Name;
						this.Hero.Class = Packet.Characters[i].Class;
						this.Hero.Level = Packet.Characters[i].Level;
						this.Realm.WriteToLog("Login with Char: " + this.Hero.Name, Color.Orange);
						return;
					}
				}
				this.Realm.WriteToLog("Character not found, idling", Color.Orange);
			}
		}

		protected virtual void RealmSocket_OnCharacterLogonResponse(CharacterLogonResponse Packet)
		{
			RealmCharacterActionResult result = Packet.Result;
			if (result == RealmCharacterActionResult.Success)
			{
				ChannelListRequest bnetPacket = new ChannelListRequest();
				this.Chat.SendPacket(bnetPacket);
				EnterChatRequest bnetPacket2 = new EnterChatRequest(this.Hero.Name);
				this.Chat.SendPacket(bnetPacket2);
				JoinChannel bnetPacket3 = new JoinChannel("Diablo II", true);
				this.Chat.SendPacket(bnetPacket3);
			}
		}

		private void Log_OnCharacterLogonResponse(CharacterLogonResponse Packet)
		{
			RealmCharacterActionResult result = Packet.Result;
			if (result == RealmCharacterActionResult.Success)
			{
				this.Realm.WriteToLog("Character Logon Successful", Color.Green);
			}
			else
			{
				this.Realm.WriteToLog("Character Logon Failed: " + Packet.Result, Color.Red);
			}
		}

		protected virtual void RealmSocket_OnJoinGameResponse(JoinGameResponse Packet)
		{
			JoinGameResult result = Packet.Result;
			if (result == JoinGameResult.Sucess)
			{
				this.Realm.WriteToLog("[GAME] Connecting To Game Server (" + Packet.GameServerIP + ")", Color.Yellow);
				LeaveChat bnetPacket = new LeaveChat();
				this.Chat.SendPacket(bnetPacket);
				this.D2GHash = Packet.GameHash;
				this.D2Token = (uint)Packet.GameToken;
				this.Game.Connect(Packet.GameServerIP, this.Proxy);
				this.Game.WaitForPacket(2);
			}
			else
			{
				this.Realm.WriteToLog("[GAME] Cannot Join Game, Reason: " + Packet.Result, Color.Red);
                this.FailToJoinGameEvent?.Invoke();
            }
		}

		protected virtual void RealmSocket_OnRealmStartupResponse(RealmStartupResponse Packet)
		{
			RealmStartupResult result = Packet.Result;
			checked
			{
				if (result == RealmStartupResult.Success)
				{
					this.Realm.WriteToLog("[0x" + Conversions.ToString((byte)Packet.PacketID) + "] Connection Successful", Color.Gray);
					MainForm.BotsConnected++;
					CharacterListRequest packet = new CharacterListRequest(8);
					this.Realm.SendPacket(packet);
				}
				else
				{
					this.Realm.WriteToLog("[0x" + Conversions.ToString((byte)Packet.PacketID) + "] Connection Failed, Reason: " + Packet.Result, Color.Red);
					this.Realm.Disconnect();
				}
			}
		}

		private void Realm_PacketLost(RealmServerPacket PacketID)
		{
		    switch (PacketID)
		    {
		        case RealmServerPacket.CreateGameResponse:
		        {
		            this.Log.AddLine("Create Game Failed", Color.Red);
		            this.FailToCreateGameEvent?.Invoke();
		            break;
		        }
		        case RealmServerPacket.JoinGameResponse:
		        {
		            this.Log.AddLine("Join Game Failed", Color.Red);
		            this.FailToJoinGameEvent?.Invoke();
		            break;
		        }
		    }
		}

		private void Realm_OnCreateGameResponse(CreateGameResponse Packet)
		{
			CreateGameResult result = Packet.Result;
			if (result == CreateGameResult.Sucess)
			{
				this.Realm.WriteToLog("Game Created", Color.Green);
			}
			else
			{
				this.Realm.WriteToLog("Game Creation Failed, Reason: " + Packet.Result, Color.Red);
                this.FailToCreateGameEvent?.Invoke();
            }
		}

		private void RealmSocket_OnDisconnected()
		{
			this.ReqID = 1;
		}

		protected virtual void CreateGame(string Name, GameDifficulty Difficulty, byte MaxPlayers, string Description = "", string Password = "")
		{
			this.Realm.WriteToLog("Creating Game: " + Name, Color.Orange);
			this.Realm.WaitForPacket(3);
			CreateGameRequest packet = new CreateGameRequest(this.ReqID, Difficulty, 8, Name, Password, Description);
			this.Realm.SendPacket(packet);
			checked
			{
				this.ReqID += 1;
			}
		}

		protected virtual void JoinGame(string Name, string Password = "")
		{
			this.Realm.WriteToLog("Joining Game: " + Name, Color.Orange);
			this.Realm.WaitForPacket(4);
			JoinGameRequest packet = new JoinGameRequest(this.ReqID, Name, Password);
			this.Realm.SendPacket(packet);
			checked
			{
				this.ReqID += 1;
			}
		}

		protected virtual void RequestGameList()
		{
			this.Realm.WriteToLog("Requesting Game List", Color.Orange);
			GameListRequest packet = new GameListRequest(this.ReqID);
			this.Realm.SendPacket(packet);
			checked
			{
				this.ReqID += 1;
			}
		}

		protected virtual void RequestGameInfo(string Name)
		{
			GameInfoRequest packet = new GameInfoRequest(this.ReqID, Name);
			this.Realm.SendPacket(packet);
			checked
			{
				this.ReqID += 1;
			}
		}

		/*[DebuggerStepThrough, CompilerGenerated]
		private void _Lambda$__1(int a0)
		{
			this.Game_PacketLost((GameServerPacket)a0);
		}

		[DebuggerStepThrough, CompilerGenerated]
		private void _Lambda$__2(int a0)
		{
			this.Realm_PacketLost(checked((RealmServerPacket)a0));
		}*/
	}
}

using BotCore.RealmClient;
using BotCore.RealmServer;
using System;
using System.Drawing;
using System.Net;

namespace BioBot
{
	public interface IRealm
	{
		public delegate void OnConnectedEventHandler();

		public delegate void OnDisconnectedEventHandler();

		public delegate void UpdateEventHandler();

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

		event IRealm.OnConnectedEventHandler OnConnected;

		event IRealm.OnDisconnectedEventHandler OnDisconnected;

		event IRealm.UpdateEventHandler Update;

		event IRealm.OnCharacterCreationResponseEventHandler OnCharacterCreationResponse;

		event IRealm.OnCharacterDeletionResponseEventHandler OnCharacterDeletionResponse;

		event IRealm.OnCharacterListEventHandler OnCharacterList;

		event IRealm.OnCharacterLogonResponseEventHandler OnCharacterLogonResponse;

		event IRealm.OnCharacterUpgradeResponseEventHandler OnCharacterUpgradeResponse;

		event IRealm.OnCreateGameResponseEventHandler OnCreateGameResponse;

		event IRealm.OnGameCreationQueueEventHandler OnGameCreationQueue;

		event IRealm.OnGameInfoEventHandler OnGameInfo;

		event IRealm.OnGameListEventHandler OnGameList;

		event IRealm.OnJoinGameResponseEventHandler OnJoinGameResponse;

		event IRealm.OnMessageOfTheDayEventHandler OnMessageOfTheDay;

		event IRealm.OnRealmStartupResponseEventHandler OnRealmStartupResponse;

		bool Connected
		{
			get;
		}

		void WriteToLog(string Text, Color Color);

		void Connect(byte[] StartupData, string UniqueUsername, IPAddress ServerIp);

		void Disconnect();

		void SendPacket(byte[] bytes);

		void SendPacket(RCPacket Packet);
	}
}

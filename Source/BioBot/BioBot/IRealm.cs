using BotCore.RealmClient;
using BotCore.RealmServer;
using System;
using System.Drawing;
using System.Net;

namespace BioBot
{
    namespace Realm
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
    }

    public interface IRealm
	{
		event Realm.OnConnectedEventHandler OnConnected;

		event Realm.OnDisconnectedEventHandler OnDisconnected;

		event Realm.UpdateEventHandler Update;

		event Realm.OnCharacterCreationResponseEventHandler OnCharacterCreationResponse;

		event Realm.OnCharacterDeletionResponseEventHandler OnCharacterDeletionResponse;

		event Realm.OnCharacterListEventHandler OnCharacterList;

		event Realm.OnCharacterLogonResponseEventHandler OnCharacterLogonResponse;

		event Realm.OnCharacterUpgradeResponseEventHandler OnCharacterUpgradeResponse;

		event Realm.OnCreateGameResponseEventHandler OnCreateGameResponse;

		event Realm.OnGameCreationQueueEventHandler OnGameCreationQueue;

		event Realm.OnGameInfoEventHandler OnGameInfo;

		event Realm.OnGameListEventHandler OnGameList;

		event Realm.OnJoinGameResponseEventHandler OnJoinGameResponse;

		event Realm.OnMessageOfTheDayEventHandler OnMessageOfTheDay;

		event Realm.OnRealmStartupResponseEventHandler OnRealmStartupResponse;

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

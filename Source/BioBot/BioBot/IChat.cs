using BotCore.BnetClient;
using BotCore.BnetServer;
using System;
using System.Drawing;

namespace BioBot
{
    namespace Chat
    {
        public delegate void OnConnectedEventHandler();

        public delegate void OnDisconnectedEventHandler();

        public delegate void UpdateEventHandler();

        public delegate void OnAdInfoEventHandler(AdInfo packet);

        public delegate void OnBnetAuthResponseEventHandler(BnetAuthResponse packet);

        public delegate void OnBnetConnectionResponseEventHandler(BnetConnectionResponse packet);

        public delegate void OnBnetLogonResponseEventHandler(BnetLogonResponse packet);

        public delegate void OnBnetPingEventHandler(BnetPing packet);

        public delegate void OnChannelListEventHandler(ChannelList packet);

        public delegate void OnChatEventEventHandler(ChatEvent packet);

        public delegate void OnEnterChatResponseEventHandler(EnterChatResponse packet);

        public delegate void OnExtraWorkInfoEventHandler(ExtraWorkInfo packet);

        public delegate void OnFileTimeInfoEventHandler(FileTimeInfo packet);

        public delegate void OnServerKeepAliveEventHandler(BotCore.BnetServer.KeepAlive packet);

        public delegate void OnNewsInfoEventHandler(NewsInfo packet);

        public delegate void OnQueryRealmsResponseEventHandler(QueryRealmsResponse packet);

        public delegate void OnRealmLogonResponseEventHandler(RealmLogonResponse packet);

        public delegate void OnRequiredExtraWorkInfoEventHandler(RequiredExtraWorkInfo packet);
    }

	public interface IChat
	{
		event Chat.OnConnectedEventHandler OnConnected;

		event Chat.OnDisconnectedEventHandler OnDisconnected;

		event Chat.UpdateEventHandler Update;

		event Chat.OnAdInfoEventHandler OnAdInfo;

		event Chat.OnBnetAuthResponseEventHandler OnBnetAuthResponse;

		event Chat.OnBnetConnectionResponseEventHandler OnBnetConnectionResponse;

		event Chat.OnBnetLogonResponseEventHandler OnBnetLogonResponse;

		event Chat.OnBnetPingEventHandler OnBnetPing;

		event Chat.OnChannelListEventHandler OnChannelList;

		event Chat.OnChatEventEventHandler OnChatEvent;

		event Chat.OnEnterChatResponseEventHandler OnEnterChatResponse;

		event Chat.OnExtraWorkInfoEventHandler OnExtraWorkInfo;

		event Chat.OnFileTimeInfoEventHandler OnFileTimeInfo;

		event Chat.OnServerKeepAliveEventHandler OnServerKeepAlive;

		event Chat.OnNewsInfoEventHandler OnNewsInfo;

		event Chat.OnQueryRealmsResponseEventHandler OnQueryRealmsResponse;

		event Chat.OnRealmLogonResponseEventHandler OnRealmLogonResponse;

		event Chat.OnRequiredExtraWorkInfoEventHandler OnRequiredExtraWorkInfo;

		void WriteToLog(string Text, Color Color);

		void Connect(ConnectInfo Infos);

		void Disconnect();

		void SendPacket(byte[] bytes);

		void SendPacket(BcPacket BnetPacket);
	}
}

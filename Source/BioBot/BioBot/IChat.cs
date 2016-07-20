using BotCore.BnetClient;
using BotCore.BnetServer;
using System;
using System.Drawing;

namespace BioBot
{
	public interface IChat
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

		event IChat.OnConnectedEventHandler OnConnected;

		event IChat.OnDisconnectedEventHandler OnDisconnected;

		event IChat.UpdateEventHandler Update;

		event IChat.OnAdInfoEventHandler OnAdInfo;

		event IChat.OnBnetAuthResponseEventHandler OnBnetAuthResponse;

		event IChat.OnBnetConnectionResponseEventHandler OnBnetConnectionResponse;

		event IChat.OnBnetLogonResponseEventHandler OnBnetLogonResponse;

		event IChat.OnBnetPingEventHandler OnBnetPing;

		event IChat.OnChannelListEventHandler OnChannelList;

		event IChat.OnChatEventEventHandler OnChatEvent;

		event IChat.OnEnterChatResponseEventHandler OnEnterChatResponse;

		event IChat.OnExtraWorkInfoEventHandler OnExtraWorkInfo;

		event IChat.OnFileTimeInfoEventHandler OnFileTimeInfo;

		event IChat.OnServerKeepAliveEventHandler OnServerKeepAlive;

		event IChat.OnNewsInfoEventHandler OnNewsInfo;

		event IChat.OnQueryRealmsResponseEventHandler OnQueryRealmsResponse;

		event IChat.OnRealmLogonResponseEventHandler OnRealmLogonResponse;

		event IChat.OnRequiredExtraWorkInfoEventHandler OnRequiredExtraWorkInfo;

		void WriteToLog(string Text, Color Color);

		void Connect(ConnectInfo Infos);

		void Disconnect();

		void SendPacket(byte[] bytes);

		void SendPacket(BcPacket BnetPacket);
	}
}

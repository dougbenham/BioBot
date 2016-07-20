using BioBot.My;
using BotCore.BnetClient;
using BotCore.BnetServer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using BNSharp.MBNCSUtil;

namespace BioBot
{
    public class ChatSocket : D2Socket
    {
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

        public delegate void OnNewsInfoEventHandler(NewsInfo packet);

        public delegate void OnQueryRealmsResponseEventHandler(QueryRealmsResponse packet);

        public delegate void OnRealmLogonResponseEventHandler(RealmLogonResponse packet);

        public delegate void OnRequiredExtraWorkInfoEventHandler(RequiredExtraWorkInfo packet);

        public delegate void OnServerKeepAliveEventHandler(BotCore.BnetServer.KeepAlive packet);

        private ChatSocket.OnAdInfoEventHandler OnAdInfoEvent;

        private ChatSocket.OnBnetAuthResponseEventHandler OnBnetAuthResponseEvent;

        private ChatSocket.OnBnetConnectionResponseEventHandler OnBnetConnectionResponseEvent;

        private ChatSocket.OnBnetLogonResponseEventHandler OnBnetLogonResponseEvent;

        private ChatSocket.OnBnetPingEventHandler OnBnetPingEvent;

        private ChatSocket.OnChannelListEventHandler OnChannelListEvent;

        private ChatSocket.OnChatEventEventHandler OnChatEventEvent;

        private ChatSocket.OnEnterChatResponseEventHandler OnEnterChatResponseEvent;

        private ChatSocket.OnExtraWorkInfoEventHandler OnExtraWorkInfoEvent;

        private ChatSocket.OnFileTimeInfoEventHandler OnFileTimeInfoEvent;

        private ChatSocket.OnNewsInfoEventHandler OnNewsInfoEvent;

        private ChatSocket.OnQueryRealmsResponseEventHandler OnQueryRealmsResponseEvent;

        private ChatSocket.OnRealmLogonResponseEventHandler OnRealmLogonResponseEvent;

        private ChatSocket.OnRequiredExtraWorkInfoEventHandler OnRequiredExtraWorkInfoEvent;

        private ChatSocket.OnServerKeepAliveEventHandler OnServerKeepAliveEvent;

        private LogBox Log;

        private ConnectInfo BnetInfo;

        private bool Exp;

        private int g_iClientToken;

        private int g_iServerToken;

        public event ChatSocket.OnAdInfoEventHandler OnAdInfo
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnAdInfoEvent = (ChatSocket.OnAdInfoEventHandler) Delegate.Combine(this.OnAdInfoEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnAdInfoEvent = (ChatSocket.OnAdInfoEventHandler) Delegate.Remove(this.OnAdInfoEvent, value);
            }
        }

        public event ChatSocket.OnBnetAuthResponseEventHandler OnBnetAuthResponse
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnBnetAuthResponseEvent =
                    (ChatSocket.OnBnetAuthResponseEventHandler) Delegate.Combine(this.OnBnetAuthResponseEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnBnetAuthResponseEvent =
                    (ChatSocket.OnBnetAuthResponseEventHandler) Delegate.Remove(this.OnBnetAuthResponseEvent, value);
            }
        }

        public event ChatSocket.OnBnetConnectionResponseEventHandler OnBnetConnectionResponse
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnBnetConnectionResponseEvent =
                    (ChatSocket.OnBnetConnectionResponseEventHandler)
                        Delegate.Combine(this.OnBnetConnectionResponseEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnBnetConnectionResponseEvent =
                    (ChatSocket.OnBnetConnectionResponseEventHandler)
                        Delegate.Remove(this.OnBnetConnectionResponseEvent, value);
            }
        }

        public event ChatSocket.OnBnetLogonResponseEventHandler OnBnetLogonResponse
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnBnetLogonResponseEvent =
                    (ChatSocket.OnBnetLogonResponseEventHandler) Delegate.Combine(this.OnBnetLogonResponseEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnBnetLogonResponseEvent =
                    (ChatSocket.OnBnetLogonResponseEventHandler) Delegate.Remove(this.OnBnetLogonResponseEvent, value);
            }
        }

        public event ChatSocket.OnBnetPingEventHandler OnBnetPing
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnBnetPingEvent = (ChatSocket.OnBnetPingEventHandler) Delegate.Combine(this.OnBnetPingEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnBnetPingEvent = (ChatSocket.OnBnetPingEventHandler) Delegate.Remove(this.OnBnetPingEvent, value);
            }
        }

        public event ChatSocket.OnChannelListEventHandler OnChannelList
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnChannelListEvent =
                    (ChatSocket.OnChannelListEventHandler) Delegate.Combine(this.OnChannelListEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnChannelListEvent =
                    (ChatSocket.OnChannelListEventHandler) Delegate.Remove(this.OnChannelListEvent, value);
            }
        }

        public event ChatSocket.OnChatEventEventHandler OnChatEvent
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnChatEventEvent =
                    (ChatSocket.OnChatEventEventHandler) Delegate.Combine(this.OnChatEventEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnChatEventEvent =
                    (ChatSocket.OnChatEventEventHandler) Delegate.Remove(this.OnChatEventEvent, value);
            }
        }

        public event ChatSocket.OnEnterChatResponseEventHandler OnEnterChatResponse
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnEnterChatResponseEvent =
                    (ChatSocket.OnEnterChatResponseEventHandler) Delegate.Combine(this.OnEnterChatResponseEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnEnterChatResponseEvent =
                    (ChatSocket.OnEnterChatResponseEventHandler) Delegate.Remove(this.OnEnterChatResponseEvent, value);
            }
        }

        public event ChatSocket.OnExtraWorkInfoEventHandler OnExtraWorkInfo
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnExtraWorkInfoEvent =
                    (ChatSocket.OnExtraWorkInfoEventHandler) Delegate.Combine(this.OnExtraWorkInfoEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnExtraWorkInfoEvent =
                    (ChatSocket.OnExtraWorkInfoEventHandler) Delegate.Remove(this.OnExtraWorkInfoEvent, value);
            }
        }

        public event ChatSocket.OnFileTimeInfoEventHandler OnFileTimeInfo
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnFileTimeInfoEvent =
                    (ChatSocket.OnFileTimeInfoEventHandler) Delegate.Combine(this.OnFileTimeInfoEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnFileTimeInfoEvent =
                    (ChatSocket.OnFileTimeInfoEventHandler) Delegate.Remove(this.OnFileTimeInfoEvent, value);
            }
        }

        public event ChatSocket.OnNewsInfoEventHandler OnNewsInfo
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnNewsInfoEvent = (ChatSocket.OnNewsInfoEventHandler) Delegate.Combine(this.OnNewsInfoEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnNewsInfoEvent = (ChatSocket.OnNewsInfoEventHandler) Delegate.Remove(this.OnNewsInfoEvent, value);
            }
        }

        public event ChatSocket.OnQueryRealmsResponseEventHandler OnQueryRealmsResponse
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnQueryRealmsResponseEvent =
                    (ChatSocket.OnQueryRealmsResponseEventHandler)
                        Delegate.Combine(this.OnQueryRealmsResponseEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnQueryRealmsResponseEvent =
                    (ChatSocket.OnQueryRealmsResponseEventHandler)
                        Delegate.Remove(this.OnQueryRealmsResponseEvent, value);
            }
        }

        public event ChatSocket.OnRealmLogonResponseEventHandler OnRealmLogonResponse
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnRealmLogonResponseEvent =
                    (ChatSocket.OnRealmLogonResponseEventHandler)
                        Delegate.Combine(this.OnRealmLogonResponseEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnRealmLogonResponseEvent =
                    (ChatSocket.OnRealmLogonResponseEventHandler) Delegate.Remove(this.OnRealmLogonResponseEvent, value);
            }
        }

        public event ChatSocket.OnRequiredExtraWorkInfoEventHandler OnRequiredExtraWorkInfo
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnRequiredExtraWorkInfoEvent =
                    (ChatSocket.OnRequiredExtraWorkInfoEventHandler)
                        Delegate.Combine(this.OnRequiredExtraWorkInfoEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnRequiredExtraWorkInfoEvent =
                    (ChatSocket.OnRequiredExtraWorkInfoEventHandler)
                        Delegate.Remove(this.OnRequiredExtraWorkInfoEvent, value);
            }
        }

        public event ChatSocket.OnServerKeepAliveEventHandler OnServerKeepAlive
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.OnServerKeepAliveEvent =
                    (ChatSocket.OnServerKeepAliveEventHandler) Delegate.Combine(this.OnServerKeepAliveEvent, value);
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.OnServerKeepAliveEvent =
                    (ChatSocket.OnServerKeepAliveEventHandler) Delegate.Remove(this.OnServerKeepAliveEvent, value);
            }
        }

        public ChatSocket(ref LogBox LogWriter) : base(SockType.Chat)
        {
            base.Disconnection += new Sockets.DisconnectionEventHandler(this.OnDisconnect);
            base.Connection += new Sockets.ConnectionEventHandler(this.Chat_Connection);
            this.Log = LogWriter;
        }

        public ChatSocket(ref LogBox LogWriter, ProxyInfo Proxy) : base(SockType.Chat)
        {
            base.Disconnection += new Sockets.DisconnectionEventHandler(this.OnDisconnect);
            base.Connection += new Sockets.ConnectionEventHandler(this.Chat_Connection);
            this.Log = LogWriter;
        }

        public void Connect(ConnectInfo Infos, ProxyInfo Proxy = null)
        {
            this.g_iClientToken = new Random().Next(237600000, 237699999);
            this.BnetInfo = Infos;
            checked
            {
                if (Proxy == null)
                {
                    this.WriteToLog("Connecting: " + this.BnetInfo.Realm, Color.Gray);
                    base.Connect(this.BnetInfo.Realm, (ushort) this.BnetInfo.Port);
                }
                else
                {
                    this.WriteToLog(this.BnetInfo.Realm, Color.Gray);
                    base.Connect(this.BnetInfo.Realm, (ushort) this.BnetInfo.Port, Proxy.Address,
                        Conversions.ToString((uint) Proxy.Port), Proxy.Username, Proxy.Password);
                }
            }
        }

        private void OnDisconnect()
        {
            this.WriteToLog("Connection Lost..", Color.Red);
        }

        public override void ParsePacket(byte[] Data)
        {
            BncsReader bncsReader = new BncsReader(Data);
            BnetServerPacket packetID = (BnetServerPacket) bncsReader.PacketID;
            this.ReportPacket((int) packetID);
            switch (packetID)
            {
                case BnetServerPacket.KeepAlive:
                {
                    this.OnServerKeepAliveEvent?.Invoke(new BotCore.BnetServer.KeepAlive(Data));
                    break;
                }
                case BnetServerPacket.EnterChatResponse:
                {
                    this.OnEnterChatResponseEvent?.Invoke(new EnterChatResponse(Data));
                    break;
                }
                case BnetServerPacket.ChannelList:
                {
                    this.OnChannelListEvent?.Invoke(new ChannelList(Data));
                    break;
                }
                case BnetServerPacket.ChatEvent:
                {
                    this.OnChatEventEvent?.Invoke(new ChatEvent(Data));
                    break;
                }
                case BnetServerPacket.AdInfo:
                {
                    this.OnAdInfoEvent?.Invoke(new AdInfo(Data));
                    break;
                }
                case BnetServerPacket.BnetPing:
                {
                    this.OnBnetPingEvent?.Invoke(new BnetPing(Data));
                    break;
                }
                case BnetServerPacket.FileTimeInfo:
                {
                    this.OnFileTimeInfoEvent?.Invoke(new FileTimeInfo(Data));
                    break;
                }
                case BnetServerPacket.BnetLogonResponse:
                {
                    this.OnBnetLogonResponseEvent?.Invoke(new BnetLogonResponse(Data));
                    break;
                }
                case BnetServerPacket.RealmLogonResponse:
                {
                    this.OnRealmLogonResponseEvent?.Invoke(new RealmLogonResponse(Data));
                    break;
                }
                case BnetServerPacket.QueryRealmsResponse:
                {
                    this.Handle_SID_QUERYREALMS2(bncsReader);
                    this.OnQueryRealmsResponseEvent?.Invoke(new QueryRealmsResponse(Data));
                    break;
                }
                case BnetServerPacket.NewsInfo:
                {
                    this.OnNewsInfoEvent?.Invoke(new NewsInfo(Data));
                    break;
                }
                case BnetServerPacket.ExtraWorkInfo:
                {
                    this.OnExtraWorkInfoEvent?.Invoke(new ExtraWorkInfo(Data));
                    break;
                }
                case BnetServerPacket.RequiredExtraWorkInfo:
                {
                    this.OnRequiredExtraWorkInfoEvent?.Invoke(new RequiredExtraWorkInfo(Data));
                    break;
                }
                case BnetServerPacket.BnetConnectionResponse:
                {
                    this.Handle_SID_AUTH_INFO(bncsReader);
                    this.OnBnetConnectionResponseEvent?.Invoke(new BnetConnectionResponse(Data));
                    break;
                }
                case BnetServerPacket.BnetAuthResponse:
                {
                    this.Handle_AUTH_CHECK(bncsReader);
                    this.OnBnetAuthResponseEvent?.Invoke(new BnetAuthResponse(Data));
                    break;
                }
            }
        }

        public void Handle_SID_QUERYREALMS2(BncsReader Reader)
        {
            Reader.ReadInt32();
            int num = Reader.ReadInt32();
            if (num == 0)
            {
                this.WriteToLog("Realm logon failed. No realms found.", Color.Red);
                this.Disconnect();
                return;
            }
            string text = num + " realm(s) found: ";
            string text2 = "";
            int arg_47_0 = 0;
            checked
            {
                int num2 = num - 1;
                for (int i = arg_47_0; i <= num2; i++)
                {
                    Reader.ReadInt32();
                    string text3 = Reader.ReadCString();
                    string text4 = Reader.ReadCString();
                    if (i == 0)
                    {
                        text2 = text3;
                    }
                    text = string.Concat(new string[]
                    {
                        text,
                        text3,
                        " (",
                        text4,
                        "), "
                    });
                }
                text = text.Substring(0, text.Length - 1) + ".";
                this.WriteToLog(text, Color.Gray);
                this.WriteToLog("Getting logon information for " + text2 + "..", Color.Gray);
                this.Send_SID_LOGONREALMEX(text2);
            }
        }

        public void Handle_SID_AUTH_INFO(BncsReader Reader)
        {
            int num = Reader.ReadInt32();
            int num2 = Reader.ReadInt32();
            int num3 = Reader.ReadInt32();
            long num4 = Reader.ReadInt64();
            string mpqName = Reader.ReadCString();
            byte[] array = Reader.ReadNullTerminatedByteArray();
            List<string> list = new List<string>();
            this.g_iServerToken = num2;
            int num5;
            if (this.Exp)
            {
                num5 = 2;
            }
            else
            {
                num5 = 1;
            }
            BattleNetClient battleNetClient = new BattleNetClient(this.Exp);
            checked
            {
                string[] array2 = new string[num5];
                CdKey[] array3 = new CdKey[num5];
                byte[][] array4 = new byte[num5][];
                int[] array5 = new int[num5];
                array2[0] = this.BnetInfo.ClassicCdKey;
                array2[0] = array2[0].Replace("-", "");
                array2[0] = array2[0].Replace(" ", "");
                if (num5 >= 2)
                {
                    array2[1] = this.BnetInfo.ExpCdKey;
                    array2[1] = array2[1].Replace("-", "");
                    array2[1] = array2[1].Replace(" ", "");
                }
                int arg_111_0 = 0;
                int num6 = num5 - 1;
                for (int i = arg_111_0; i <= num6; i++)
                {
                    try
                    {
                        array5[i] = array2[i].Length;
                        array3[i] = CdKey.CreateDecoder(array2[i]);
                        array4[i] = array3[i].GetHash(this.g_iClientToken, this.g_iServerToken);
                        if (!array3[i].IsValid)
                        {
                            this.WriteToLog("Warning Cd-Key(" + Conversions.ToString(i) + ") is invalid", Color.Red);
                            this.Disconnect();
                        }
                    }
                    catch (Exception expr_186)
                    {
                        ProjectData.SetProjectError(expr_186);
                        this.WriteToLog("Error while preparing the cd-key " + i, Color.Red);
                        this.Disconnect();
                        ProjectData.ClearProjectError();
                        return;
                    }
                }
                string str = "";
                char[] array6 = new char[array.Length];
                int arg_1EF_0 = 0;
                int num7 = array.Length - 1;
                for (int j = arg_1EF_0; j <= num7; j++)
                {
                    array6[j] = Strings.ChrW((int) array[j]);
                }
                int exeInfo = CheckRevision.GetExeInfo(battleNetClient.g_sHashes[0], out str);
                int i2 = CheckRevision.DoCheckRevision(new string(array6), battleNetClient.g_sHashes,
                    CheckRevision.ExtractMPQNumber(mpqName));
                BncsPacket bncsPacket = new BncsPacket(81);
                bncsPacket.InsertInt32(this.g_iClientToken);
                bncsPacket.InsertInt32(exeInfo);
                bncsPacket.InsertInt32(i2);
                bncsPacket.InsertInt32(num5);
                bncsPacket.InsertBoolean(false);
                int arg_27B_0 = 0;
                int num8 = num5 - 1;
                for (int k = arg_27B_0; k <= num8; k++)
                {
                    bncsPacket.InsertInt32(array5[k]);
                    bncsPacket.InsertInt32(array3[k].Product);
                    bncsPacket.InsertInt32(array3[k].Value1);
                    bncsPacket.InsertInt32(0);
                    bncsPacket.InsertByteArray(array4[k]);
                }
                bncsPacket.InsertCString(str);
                bncsPacket.InsertCString(this.BnetInfo.CdKeyOwner);
                this.SendPacket(bncsPacket.UnderlyingBuffer);
            }
        }

        public void Handle_AUTH_CHECK(BncsReader Reader)
        {
            int num = Reader.ReadInt32();
            if (num == 0)
            {
                this.Send_SID_LOGONRESPONSE();
            }
        }

        private void Send_SID_LOGONRESPONSE()
        {
            byte[] b = new byte[0];
            b = OldAuth.DoubleHashPassword(this.BnetInfo.BnetPassword, this.g_iClientToken, this.g_iServerToken);
            BncsPacket bncsPacket = new BncsPacket(58);
            bncsPacket.InsertInt32(this.g_iClientToken);
            bncsPacket.InsertInt32(this.g_iServerToken);
            bncsPacket.InsertByteArray(b);
            bncsPacket.InsertCString(this.BnetInfo.BnetUserName);
            this.SendPacket(bncsPacket.UnderlyingBuffer);
        }

        private void Send_SID_LOGONREALMEX(string a_sRealmTitle)
        {
            BncsPacket bncsPacket = new BncsPacket(62);
            byte[] b = OldAuth.DoubleHashPassword("password", this.g_iClientToken, this.g_iServerToken);
            bncsPacket.InsertInt32(this.g_iClientToken);
            bncsPacket.InsertByteArray(b);
            bncsPacket.InsertCString(a_sRealmTitle);
            this.SendPacket(bncsPacket.UnderlyingBuffer);
        }

        public void SendPacket(BcPacket BnetPacket)
        {
            this.SendPacket(BnetPacket.Data);
        }

        public void WriteToLog(string Text, Color Color)
        {
            this.Log.AddLine(Text, Color, HorizontalAlignment.Left);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private void Chat_Connection()
        {
            byte[] bytes = new byte[]
            {
                1
            };
            this.SendPacket(bytes);
            Thread.Sleep(5);
            BncsPacket bncsPacket = new BncsPacket(80);
            bncsPacket.InsertInt32(0);
            bncsPacket.InsertDwordString("IX86");
            List<string> list = new List<string>();
            string[] collection = File.ReadAllLines(MyProject.Application.Info.DirectoryPath + "/Settings/cdkeys.txt");
            list.AddRange(collection);
            string[] array = list[Plugintab.DeclareKey].Split(new char[]
            {
                '/'
            });
            if (Operators.CompareString(array[0], "", false) != 0)
            {
                this.Exp = false;
                if (array.Count<string>() == 2 && Operators.CompareString(array[1], "", false) != 0)
                {
                    this.Exp = true;
                }
            }
            if (this.Exp)
            {
                bncsPacket.InsertDwordString("D2XP");
            }
            else
            {
                bncsPacket.InsertDwordString("D2DV");
            }
            bncsPacket.InsertInt32(BattleNetClient.g_bVerbyte);
            bncsPacket.InsertInt32(CultureInfo.CurrentUICulture.LCID);
            bncsPacket.InsertInt32(16777343);
            bncsPacket.InsertInt32(checked((int) Math.Round(DateTime.UtcNow.Subtract(DateTime.Now).TotalMinutes)));
            bncsPacket.InsertInt32(CultureInfo.CurrentUICulture.LCID);
            bncsPacket.InsertInt32(CultureInfo.CurrentUICulture.LCID);
            bncsPacket.InsertCString(RegionInfo.CurrentRegion.ThreeLetterISORegionName);
            bncsPacket.InsertCString(RegionInfo.CurrentRegion.DisplayName);
            this.SendPacket(bncsPacket.UnderlyingBuffer);
        }
    }
}

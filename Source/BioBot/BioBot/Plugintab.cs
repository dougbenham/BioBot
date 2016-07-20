using BioBot.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BioBot
{
	public class Plugintab : TabPage
	{
		private IContainer components;

		[AccessedThroughProperty("ListBox1")]
		private ListBox _ListBox1;

		[AccessedThroughProperty("Button1")]
		private Button _Button1;

		[AccessedThroughProperty("BConnect")]
		private Button _BConnect;

		[AccessedThroughProperty("BConnectDelay")]
		private System.Windows.Forms.Timer _BConnectDelay;

		[AccessedThroughProperty("BConnectAll")]
		private Button _BConnectAll;

		[AccessedThroughProperty("BotList")]
		private ListBox _BotList;

		[AccessedThroughProperty("Closebutton")]
		private Button _Closebutton;

		[AccessedThroughProperty("Box")]
		private Panel _Box;

		[AccessedThroughProperty("Optionz")]
		private List<ContextMenuStrip> _Optionz;

		private List<ToolStripButton> Connect;

		private List<ToolStripButton> ConnectAll;

		private List<LogBox> LogBox;

		private PluginServices.AvailablePlugin SelectedPlugin;

		private string Realm;

		public static List<ConnectInfo> BotInfos = new List<ConnectInfo>();

		private List<ConnectionManager> BotInstances;

		private List<string> RawcdKeys;

		private ProxyInfo Proxy;

		public static int DeclareKey;

		private Thread ConnectAllThread;

		internal virtual ListBox ListBox1
		{
			get
			{
				return this._ListBox1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ListBox1 = value;
			}
		}

		internal virtual Button Button1
		{
			get
			{
				return this._Button1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Button1_Click);
				if (this._Button1 != null)
				{
					this._Button1.Click -= value2;
				}
				this._Button1 = value;
				if (this._Button1 != null)
				{
					this._Button1.Click += value2;
				}
			}
		}

		internal virtual Button BConnect
		{
			get
			{
				return this._BConnect;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._BConnect = value;
			}
		}

        internal virtual System.Windows.Forms.Timer BConnectDelay
		{
			get
			{
				return this._BConnectDelay;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.BConnectDelay_Tick);
				if (this._BConnectDelay != null)
				{
					this._BConnectDelay.Tick -= value2;
				}
				this._BConnectDelay = value;
				if (this._BConnectDelay != null)
				{
					this._BConnectDelay.Tick += value2;
				}
			}
		}

        internal virtual Button BConnectAll
		{
			get
			{
				return this._BConnectAll;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.BConnectAll_Click);
				if (this._BConnectAll != null)
				{
					this._BConnectAll.Click -= value2;
				}
				this._BConnectAll = value;
				if (this._BConnectAll != null)
				{
					this._BConnectAll.Click += value2;
				}
			}
		}

        internal virtual ListBox BotList
		{
			get
			{
				return this._BotList;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.BotList_SelectedIndexChanged);
				KeyEventHandler value3 = new KeyEventHandler(this.BotList_KeyDown);
				MouseEventHandler value4 = new MouseEventHandler(this.BotList_MouseDown);
				if (this._BotList != null)
				{
					this._BotList.SelectedIndexChanged -= value2;
					this._BotList.KeyDown -= value3;
					this._BotList.MouseDown -= value4;
				}
				this._BotList = value;
				if (this._BotList != null)
				{
					this._BotList.SelectedIndexChanged += value2;
					this._BotList.KeyDown += value3;
					this._BotList.MouseDown += value4;
				}
			}
		}

        internal virtual Button Closebutton
		{
			get
			{
				return this._Closebutton;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Closebutton_Click);
				if (this._Closebutton != null)
				{
					this._Closebutton.Click -= value2;
				}
				this._Closebutton = value;
				if (this._Closebutton != null)
				{
					this._Closebutton.Click += value2;
				}
			}
		}

        internal virtual Panel Box
		{
			get
			{
				return this._Box;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Box = value;
			}
		}

        internal virtual List<ContextMenuStrip> Optionz
		{
			get
			{
				return this._Optionz;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Optionz = value;
			}
		}

		[DebuggerNonUserCode]
		public Plugintab(IContainer container) : this()
		{
			if (container != null)
			{
				container.Add(this);
			}
		}

		[DebuggerNonUserCode]
		public Plugintab()
		{
			this.BConnect = new Button();
			this.BConnectDelay = new System.Windows.Forms.Timer();
			this.BConnectAll = new Button();
			this.BotList = new ListBox();
			this.Closebutton = new Button();
			this.Box = new Panel();
			this.Optionz = new List<ContextMenuStrip>();
			this.Connect = new List<ToolStripButton>();
			this.ConnectAll = new List<ToolStripButton>();
			this.LogBox = new List<LogBox>();
			this.BotInstances = new List<ConnectionManager>();
			this.RawcdKeys = new List<string>();
			this.InitializeComponent();
		}

		[DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.ListBox1 = new ListBox();
			this.Button1 = new Button();
			this.SuspendLayout();
			this.ListBox1.FormattingEnabled = true;
			Control arg_38_0 = this.ListBox1;
			Point location = new Point(6, 6);
			arg_38_0.Location = location;
			this.ListBox1.Name = "ListBox1";
			Control arg_62_0 = this.ListBox1;
			Size size = new Size(85, 244);
			arg_62_0.Size = size;
			this.ListBox1.TabIndex = 0;
			Control arg_83_0 = this.Button1;
			location = new Point(0, 0);
			arg_83_0.Location = location;
			this.Button1.Name = "Button1";
			Control arg_AA_0 = this.Button1;
			size = new Size(75, 23);
			arg_AA_0.Size = size;
			this.Button1.TabIndex = 0;
			this.Button1.Text = "Close";
			this.Button1.UseVisualStyleBackColor = true;
			this.ResumeLayout(false);
		}

		public void Generateform()
		{
			this.Box = new Panel();
			this.Box.Parent = this;
			this.Box.BackColor = Color.FromArgb(43, 43, 43);
			this.Box.BorderStyle = BorderStyle.FixedSingle;
			Control arg_4A_0 = this.Box;
			Point location = new Point(0, -9);
			arg_4A_0.Location = location;
			Control arg_65_0 = this.Box;
			Size size = new Size(120, 247);
			arg_65_0.Size = size;
			this.BotList = new ListBox();
			this.BotList.Parent = this;
			Control arg_91_0 = this.BotList;
			location = new Point(-1, -1);
			arg_91_0.Location = location;
			Control arg_AF_0 = this.BotList;
			size = new Size(190, 240);
			arg_AF_0.Size = size;
			this.BotList.BackColor = Color.FromArgb(43, 43, 43);
			this.BotList.ForeColor = Color.Gray;
			this.BotList.BorderStyle = BorderStyle.None;
			this.BotList.BringToFront();
			this.BConnectDelay.Enabled = false;
			this.BConnectDelay.Interval = Conversions.ToInteger(MyProject.Forms.MainForm.TextBox1.Text);
			this.BConnectAll.Parent = this;
			Control arg_145_0 = this.BConnectAll;
			location = new Point(146, 300);
			arg_145_0.Location = location;
			Control arg_160_0 = this.BConnectAll;
			size = new Size(131, 36);
			arg_160_0.Size = size;
			this.BConnectAll.Text = "Connect All";
			this.Closebutton.Parent = this;
			Control arg_191_0 = this.Closebutton;
			location = new Point(0, 0);
			arg_191_0.Location = location;
			Control arg_1A9_0 = this.Closebutton;
			size = new Size(20, 20);
			arg_1A9_0.Size = size;
			this.Closebutton.Text = "X";
		}

		public Plugintab(PluginServices.AvailablePlugin SelectedPlugin, string Realm, List<string> CdKeys, List<string> Accounts, string Proxy)
		{
			this.BConnect = new Button();
			this.BConnectDelay = new System.Windows.Forms.Timer();
			this.BConnectAll = new Button();
			this.BotList = new ListBox();
			this.Closebutton = new Button();
			this.Box = new Panel();
			this.Optionz = new List<ContextMenuStrip>();
			this.Connect = new List<ToolStripButton>();
			this.ConnectAll = new List<ToolStripButton>();
			this.LogBox = new List<LogBox>();
			this.BotInstances = new List<ConnectionManager>();
			this.RawcdKeys = new List<string>();
			this.Generateform();
			this.Realm = Realm.ToLower();
			this.SelectedPlugin = SelectedPlugin;
			string text = "";
			if (Operators.CompareString(Realm, "useast.battle.net", false) == 0)
			{
				text += "East";
			}
			else if (Operators.CompareString(Realm, "uswest.battle.net", false) == 0)
			{
				text += "West";
			}
			else if (Operators.CompareString(Realm, "asia.battle.net", false) == 0)
			{
				text += "Asia";
			}
			else if (Operators.CompareString(Realm, "europe.battle.net", false) == 0)
			{
				text += "Europe";
			}
			this.Text = text;
			this.BackColor = Color.FromArgb(43, 43, 43);
			this.BorderStyle = BorderStyle.FixedSingle;
			if (Operators.CompareString(Proxy, "", false) != 0)
			{
				this.Proxy = new ProxyInfo();
				this.Proxy.Address = Proxy.Split(new char[]
				{
					':'
				})[0];
				this.Proxy.Port = Conversions.ToUShort(Proxy.Split(new char[]
				{
					'/'
				})[0].Split(new char[]
				{
					':'
				})[1]);
				this.Proxy.Username = Proxy.Split(new char[]
				{
					'/'
				})[1];
				this.Proxy.Password = Proxy.Split(new char[]
				{
					'/'
				})[2];
			}
			this.RawcdKeys = CdKeys;
			this.GenerateBotInfos(CdKeys, Accounts);
		    foreach (var connect in Plugintab.BotInfos)
		    {
		        ToolStripButton toolStripButton = new ToolStripButton("Connect");
		        ToolStripButton toolStripButton2 = new ToolStripButton("Connect All");
		        this.Connect.Add(toolStripButton);
		        this.ConnectAll.Add(toolStripButton2);
		        ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
		        contextMenuStrip.TopLevel = false;
		        contextMenuStrip.Parent = this;
		        contextMenuStrip.BackColor = Color.FromArgb(43, 43, 43);
		        contextMenuStrip.ForeColor = Color.Gray;
		        contextMenuStrip.ShowImageMargin = false;
		        contextMenuStrip.Items.Add(toolStripButton);
		        contextMenuStrip.Items.Add(toolStripButton2);
		        this.Optionz.Add(contextMenuStrip);
		        LogBox logBox = new LogBox();
		        logBox.Parent = this;
		        logBox.Visible = false;
		        logBox.BackColor = Color.FromArgb(43, 43, 43);
		        logBox.BorderStyle = BorderStyle.None;
		        logBox.ReadOnly = true;
		        Control arg_30A_0 = logBox;
		        Point point = new Point(1, 1);
		        arg_30A_0.Location = point;
		        Control arg_328_0 = logBox;
		        point = new Point(550, 262);
		        arg_328_0.Size = (Size) point;
		        this.LogBox.Add(logBox);
		        MyProject.Forms.MainForm.Panel4.Controls.Add(logBox);
		        IPluginModule pluginModule = (IPluginModule) PluginServices.CreateInstance(SelectedPlugin);
		        this.BotInstances.Add((ConnectionManager) pluginModule);
		        this.BotList.Items.Add(connect.BnetUserName);
		    }
		    this.BotList.SelectedIndex = 0;
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public void GenerateBotInfos(List<string> CdKeys, List<string> Accounts)
		{
			iniFile iniFile = new iniFile(MyProject.Application.Info.DirectoryPath + "\\Settings\\Config.ini");
			string @string = iniFile.GetString("Settings", "CdkeyOwner", "bb");
			string string2 = iniFile.GetString("Settings", "DefaultPassword", "");
			int num = Conversions.ToInteger(Interaction.IIf(Accounts.Count < CdKeys.Count, Accounts.Count, CdKeys.Count));
			int arg_80_0 = 0;
			checked
			{
				int num2 = num - 1;
				for (int i = arg_80_0; i <= num2; i++)
				{
					ConnectInfo connectInfo = new ConnectInfo();
					connectInfo.Realm = this.Realm;
					connectInfo.CdKeyOwner = @string;
					connectInfo.BnetPassword = string2;
					Accounts[i] = Accounts[i].Replace(" ", "");
					string[] array = Accounts[i].Split(new char[]
					{
						'/'
					});
					if (Operators.CompareString(array[0], "", false) != 0)
					{
						connectInfo.BnetUserName = array[0];
						if (array.Count<string>() == 2)
						{
							connectInfo.BnetPassword = array[1];
						}
						CdKeys[i] = CdKeys[i].Replace(" ", "").Replace("-", "");
						string[] array2 = CdKeys[i].Split(new char[]
						{
							'/'
						});
						if (Operators.CompareString(array2[0], "", false) != 0)
						{
							connectInfo.ClassicCdKey = array2[0];
							if (array2.Count<string>() != 2)
							{
								goto IL_1A9;
							}
							connectInfo.ExpCdKey = array2[1];
						}
						Plugintab.BotInfos.Add(connectInfo);
					}
					IL_1A9:;
				}
			}
		}

		private void ConnectAll_Click(object sender, EventArgs e)
		{
			if (this.ConnectAllThread != null)
			{
				this.ConnectAllThread.Abort();
			}
			this.ConnectAllThread = new Thread(new ThreadStart(this.ConnectAllThred))
			{
				IsBackground = true
			};
			this.ConnectAllThread.Start();
		}

		private void ConnectAllThred()
		{
			int arg_0F_0 = 0;
			checked
			{
				int num = this.BotInstances.Count - 1;
				for (int i = arg_0F_0; i <= num; i++)
				{
					if (!this.BotInstances[i].Initialized)
					{
						this.BotInstances[i].Proxy = this.Proxy;
						ConnectionManager arg_6C_0 = this.BotInstances[i];
						ushort arg_6C_1 = (ushort)i;
						List<LogBox> logBox = this.LogBox;
						List<LogBox> arg_58_0 = logBox;
						int index = i;
						LogBox value = arg_58_0[index];
						arg_6C_0.Initialize(arg_6C_1, ref value, Plugintab.BotInfos[i]);
						logBox[index] = value;
					}
					if (i != this.BotList.Items.Count - 1)
					{
						Plugintab.DeclareKey++;
					}
					else
					{
						Plugintab.DeclareKey = 0;
					}
					Thread.Sleep(MainForm.Delay);
				}
			}
		}

		private void Connect_Click(object sender, EventArgs e)
		{
			int selectedIndex = this.BotList.SelectedIndex;
			if (selectedIndex >= 0)
			{
				if (Operators.CompareString(this.Connect[selectedIndex].Text, "Connect", false) == 0)
				{
					this.BotInstances[selectedIndex].Proxy = this.Proxy;
					ConnectionManager arg_75_0 = this.BotInstances[selectedIndex];
					ushort arg_75_1 = checked((ushort)selectedIndex);
					List<LogBox> logBox = this.LogBox;
					List<LogBox> arg_62_0 = logBox;
					int index = selectedIndex;
					LogBox value = arg_62_0[index];
					arg_75_0.Initialize(arg_75_1, ref value, Plugintab.BotInfos[selectedIndex]);
					logBox[index] = value;
					Plugintab.DeclareKey = selectedIndex;
					this.Connect[selectedIndex].Text = "Disconnect";
				}
				else
				{
					this.BotInstances[selectedIndex].Disconnect();
					this.Connect[selectedIndex].Text = "Connect";
				}
			}
		}

		private void BotList_MouseDown(object sender, MouseEventArgs e)
		{
			checked
			{
				if (e.Button == MouseButtons.Right)
				{
					int arg_24_0 = 0;
					int num = this.BotList.Items.Count - 1;
					for (int i = arg_24_0; i <= num; i++)
					{
						if (this.Optionz[i].Visible)
						{
							this.Optionz[i].Close();
						}
					}
					ToolStripDropDown arg_8E_0 = this.Optionz[this.BotList.SelectedIndex];
					Point screenLocation = new Point(e.Location.X, e.Location.Y);
					arg_8E_0.Show(screenLocation);
				}
			}
		}

		private void BotList_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				int selectedIndex = this.BotList.SelectedIndex;
				if (selectedIndex > -1)
				{
					this.BotList.Items.RemoveAt(selectedIndex);
					this.LogBox[selectedIndex].Dispose();
					this.LogBox.RemoveAt(selectedIndex);
					this.BotInstances[selectedIndex].Disconnect();
					this.BotInstances.RemoveAt(selectedIndex);
					this.BotList.SelectedIndex = checked(selectedIndex - 1);
				}
			}
		}

		private void BotList_SelectedIndexChanged(object sender, EventArgs e)
		{
		    foreach (var instance in this.LogBox)
		        instance.Visible = false;
			int arg_4A_0 = 0;
			checked
			{
				int num = this.BotList.Items.Count - 1;
				for (int i = arg_4A_0; i <= num; i++)
				{
					if (this.Optionz[i].Visible)
					{
						this.Optionz[i].Close();
					}
				}
				if (this.BotList.SelectedIndex >= 0)
				{
					this.LogBox[this.BotList.SelectedIndex].Visible = true;
					this.Connect[this.BotList.SelectedIndex].Click += new EventHandler(this.Connect_Click);
					this.ConnectAll[this.BotList.SelectedIndex].Click += new EventHandler(this.ConnectAll_Click);
					if (this.BotInstances[this.BotList.SelectedIndex].Initialized)
					{
						this.BConnect.Text = "Disconnect";
					}
					else
					{
						this.BConnect.Text = "Connect";
					}
				}
			}
		}

		private void BConnectDelay_Tick(object sender, EventArgs e)
		{
		}

		private void BConnectAll_Click(object sender, EventArgs e)
		{
			int arg_0F_0 = 0;
			checked
			{
				int num = this.BotInstances.Count - 1;
				for (int i = arg_0F_0; i <= num; i++)
				{
					if (!this.BotInstances[i].Initialized)
					{
						this.BotInstances[i].Proxy = this.Proxy;
						ConnectionManager arg_69_0 = this.BotInstances[i];
						ushort arg_69_1 = (ushort)i;
						List<LogBox> logBox = this.LogBox;
						List<LogBox> arg_55_0 = logBox;
						int index = i;
						LogBox value = arg_55_0[index];
						arg_69_0.Initialize(arg_69_1, ref value, Plugintab.BotInfos[i]);
						logBox[index] = value;
					}
				}
			}
		}

		private void Closebutton_Click(object sender, EventArgs e)
		{
            foreach (var instance in this.BotInstances)
                instance.Disconnect();

            foreach (var instance in this.LogBox)
                instance.Dispose();

			string realm = this.Realm;
			if (Operators.CompareString(realm, "useast.battle.net", false) == 0)
			{
				MyProject.Forms.MainForm.KeyListEast.AddRange(this.RawcdKeys);
			}
			else if (Operators.CompareString(realm, "uswest.battle.net", false) == 0)
			{
				MyProject.Forms.MainForm.KeyListWest.AddRange(this.RawcdKeys);
			}
			else if (Operators.CompareString(realm, "asia.battle.net", false) == 0)
			{
				MyProject.Forms.MainForm.KeyListAsia.AddRange(this.RawcdKeys);
			}
			else if (Operators.CompareString(realm, "europe.battle.net", false) == 0)
			{
				MyProject.Forms.MainForm.KeyListEurope.AddRange(this.RawcdKeys);
			}
			this.RawcdKeys.Clear();
			this.BotInstances.Clear();
			this.LogBox.Clear();
			this.BotList.Items.Clear();
			Plugintab.BotInfos.Clear();
			MyProject.Forms.MainForm.MainTab.SelectedIndex = 0;
			this.Dispose();
		}

		private void Button1_Click(object sender, EventArgs e)
		{
		}
	}
}

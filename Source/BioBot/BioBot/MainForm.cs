using BioBot.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BioBot
{
	[DesignerGenerated]
	public class MainForm : Form
	{
		private IContainer components;

		[AccessedThroughProperty("CPlugin")]
		private ComboBox _CPlugin;

		[AccessedThroughProperty("LPlugin")]
		private Label _LPlugin;

		[AccessedThroughProperty("CRealm")]
		private ComboBox _CRealm;

		[AccessedThroughProperty("LRealm")]
		private Label _LRealm;

		[AccessedThroughProperty("Label19")]
		private Label _Label19;

		[AccessedThroughProperty("CProxy")]
		private ComboBox _CProxy;

		[AccessedThroughProperty("Label18")]
		private Label _Label18;

		[AccessedThroughProperty("CProxyPass")]
		private TextBox _CProxyPass;

		[AccessedThroughProperty("CProxyUser")]
		private TextBox _CProxyUser;

		[AccessedThroughProperty("CAccounts")]
		private ComboBox _CAccounts;

		[AccessedThroughProperty("Label15")]
		private Label _Label15;

		[AccessedThroughProperty("CProxyPort")]
		private TextBox _CProxyPort;

		[AccessedThroughProperty("Label21")]
		private Label _Label21;

		[AccessedThroughProperty("LProxy")]
		private Label _LProxy;

		[AccessedThroughProperty("MainTab")]
		private CTabControl _MainTab;

		[AccessedThroughProperty("Panel1")]
		private Panel _Panel1;

		[AccessedThroughProperty("Label1")]
		private Label _Label1;

		[AccessedThroughProperty("Label2")]
		private Label _Label2;

		[AccessedThroughProperty("Timer1")]
		private Timer _Timer1;

		[AccessedThroughProperty("Label3")]
		private Label _Label3;

		[AccessedThroughProperty("Panel2")]
		private Panel _Panel2;

		[AccessedThroughProperty("Panel3")]
		private Panel _Panel3;

		[AccessedThroughProperty("Panel4")]
		private Panel _Panel4;

		[AccessedThroughProperty("Label4")]
		private Label _Label4;

		[AccessedThroughProperty("Label5")]
		private Label _Label5;

		[AccessedThroughProperty("MyGroupBox1")]
		private myGroupBox _MyGroupBox1;

		[AccessedThroughProperty("MyGroupBox2")]
		private myGroupBox _MyGroupBox2;

		[AccessedThroughProperty("Label6")]
		private Label _Label6;

		[AccessedThroughProperty("ContextMenuStrip1")]
		private ContextMenuStrip _ContextMenuStrip1;

		[AccessedThroughProperty("SettingsToolStripMenuItem")]
		private ToolStripMenuItem _SettingsToolStripMenuItem;

		[AccessedThroughProperty("ShowBotsToolStripMenuItem")]
		private ToolStripMenuItem _ShowBotsToolStripMenuItem;

		[AccessedThroughProperty("LoadBotsToolStripMenuItem")]
		private ToolStripMenuItem _LoadBotsToolStripMenuItem;

		[AccessedThroughProperty("NotifyIcon1")]
		private NotifyIcon _NotifyIcon1;

		[AccessedThroughProperty("Label7")]
		private Label _Label7;

		[AccessedThroughProperty("Label8")]
		private Label _Label8;

		[AccessedThroughProperty("Timer2")]
		private Timer _Timer2;

		[AccessedThroughProperty("Label9")]
		private Label _Label9;

		[AccessedThroughProperty("TextBox1")]
		private TextBox _TextBox1;

		[AccessedThroughProperty("Label10")]
		private Label _Label10;

		private bool drag;

		private int mousex;

		private int mousey;

		private PluginServices.AvailablePlugin[] Plugins;

		private List<Counter> CountList;

		internal List<string> KeyListEast;

		internal List<string> KeyListWest;

		internal List<string> KeyListAsia;

		internal List<string> KeyListEurope;

		[CompilerGenerated]
		private bool _Minimized;

		internal string NotifyCreate;

		private int BotAttempts;

		public static Dictionary<string, List<string>> Accounts = new Dictionary<string, List<string>>();

		public static string Realm;

		public static int BotsConnected;

		public static int Delay;

		private List<string> Proxies;

		internal virtual ComboBox CPlugin
		{
			get
			{
				return this._CPlugin;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._CPlugin = value;
			}
		}

		internal virtual Label LPlugin
		{
			get
			{
				return this._LPlugin;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LPlugin = value;
			}
		}

		internal virtual ComboBox CRealm
		{
			get
			{
				return this._CRealm;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._CRealm = value;
			}
		}

		internal virtual Label LRealm
		{
			get
			{
				return this._LRealm;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LRealm = value;
			}
		}

		internal virtual Label Label19
		{
			get
			{
				return this._Label19;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label19 = value;
			}
		}

		internal virtual ComboBox CProxy
		{
			get
			{
				return this._CProxy;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CProxy_SelectedIndexChanged);
				if (this._CProxy != null)
				{
					this._CProxy.SelectedIndexChanged -= value2;
				}
				this._CProxy = value;
				if (this._CProxy != null)
				{
					this._CProxy.SelectedIndexChanged += value2;
				}
			}
		}

		internal virtual Label Label18
		{
			get
			{
				return this._Label18;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label18 = value;
			}
		}

		internal virtual TextBox CProxyPass
		{
			get
			{
				return this._CProxyPass;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._CProxyPass = value;
			}
		}

		internal virtual TextBox CProxyUser
		{
			get
			{
				return this._CProxyUser;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._CProxyUser = value;
			}
		}

		internal virtual ComboBox CAccounts
		{
			get
			{
				return this._CAccounts;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._CAccounts = value;
			}
		}

		internal virtual Label Label15
		{
			get
			{
				return this._Label15;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label15 = value;
			}
		}

		internal virtual TextBox CProxyPort
		{
			get
			{
				return this._CProxyPort;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._CProxyPort = value;
			}
		}

		internal virtual Label Label21
		{
			get
			{
				return this._Label21;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label21 = value;
			}
		}

		internal virtual Label LProxy
		{
			get
			{
				return this._LProxy;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._LProxy = value;
			}
		}

		internal virtual CTabControl MainTab
		{
			get
			{
				return this._MainTab;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._MainTab = value;
			}
		}

		internal virtual Panel Panel1
		{
			get
			{
				return this._Panel1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.Panel1_MouseUp);
				MouseEventHandler value3 = new MouseEventHandler(this.Panel1_MouseMove);
				MouseEventHandler value4 = new MouseEventHandler(this.Panel1_MouseDown);
				if (this._Panel1 != null)
				{
					this._Panel1.MouseUp -= value2;
					this._Panel1.MouseMove -= value3;
					this._Panel1.MouseDown -= value4;
				}
				this._Panel1 = value;
				if (this._Panel1 != null)
				{
					this._Panel1.MouseUp += value2;
					this._Panel1.MouseMove += value3;
					this._Panel1.MouseDown += value4;
				}
			}
		}

		internal virtual Label Label1
		{
			get
			{
				return this._Label1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label1 = value;
			}
		}

		internal virtual Label Label2
		{
			get
			{
				return this._Label2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Label2_Click);
				if (this._Label2 != null)
				{
					this._Label2.Click -= value2;
				}
				this._Label2 = value;
				if (this._Label2 != null)
				{
					this._Label2.Click += value2;
				}
			}
		}

		internal virtual Timer Timer1
		{
			get
			{
				return this._Timer1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Timer1_Tick);
				if (this._Timer1 != null)
				{
					this._Timer1.Tick -= value2;
				}
				this._Timer1 = value;
				if (this._Timer1 != null)
				{
					this._Timer1.Tick += value2;
				}
			}
		}

		internal virtual Label Label3
		{
			get
			{
				return this._Label3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label3 = value;
			}
		}

		internal virtual Panel Panel2
		{
			get
			{
				return this._Panel2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Panel2 = value;
			}
		}

		internal virtual Panel Panel3
		{
			get
			{
				return this._Panel3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Panel3 = value;
			}
		}

		internal virtual Panel Panel4
		{
			get
			{
				return this._Panel4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Panel4 = value;
			}
		}

		internal virtual Label Label4
		{
			get
			{
				return this._Label4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label4 = value;
			}
		}

		internal virtual Label Label5
		{
			get
			{
				return this._Label5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label5 = value;
			}
		}

		internal virtual myGroupBox MyGroupBox1
		{
			get
			{
				return this._MyGroupBox1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._MyGroupBox1 = value;
			}
		}

		internal virtual myGroupBox MyGroupBox2
		{
			get
			{
				return this._MyGroupBox2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._MyGroupBox2 = value;
			}
		}

		internal virtual Label Label6
		{
			get
			{
				return this._Label6;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label6 = value;
			}
		}

		internal virtual ContextMenuStrip ContextMenuStrip1
		{
			get
			{
				return this._ContextMenuStrip1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ContextMenuStrip1 = value;
			}
		}

		internal virtual ToolStripMenuItem SettingsToolStripMenuItem
		{
			get
			{
				return this._SettingsToolStripMenuItem;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SettingsToolStripMenuItem_Click);
				if (this._SettingsToolStripMenuItem != null)
				{
					this._SettingsToolStripMenuItem.Click -= value2;
				}
				this._SettingsToolStripMenuItem = value;
				if (this._SettingsToolStripMenuItem != null)
				{
					this._SettingsToolStripMenuItem.Click += value2;
				}
			}
		}

		internal virtual ToolStripMenuItem ShowBotsToolStripMenuItem
		{
			get
			{
				return this._ShowBotsToolStripMenuItem;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ShowBotsToolStripMenuItem_Click);
				if (this._ShowBotsToolStripMenuItem != null)
				{
					this._ShowBotsToolStripMenuItem.Click -= value2;
				}
				this._ShowBotsToolStripMenuItem = value;
				if (this._ShowBotsToolStripMenuItem != null)
				{
					this._ShowBotsToolStripMenuItem.Click += value2;
				}
			}
		}

		internal virtual ToolStripMenuItem LoadBotsToolStripMenuItem
		{
			get
			{
				return this._LoadBotsToolStripMenuItem;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.LoadBotsToolStripMenuItem_Click);
				if (this._LoadBotsToolStripMenuItem != null)
				{
					this._LoadBotsToolStripMenuItem.Click -= value2;
				}
				this._LoadBotsToolStripMenuItem = value;
				if (this._LoadBotsToolStripMenuItem != null)
				{
					this._LoadBotsToolStripMenuItem.Click += value2;
				}
			}
		}

		internal virtual NotifyIcon NotifyIcon1
		{
			get
			{
				return this._NotifyIcon1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
				if (this._NotifyIcon1 != null)
				{
					this._NotifyIcon1.MouseDoubleClick -= value2;
				}
				this._NotifyIcon1 = value;
				if (this._NotifyIcon1 != null)
				{
					this._NotifyIcon1.MouseDoubleClick += value2;
				}
			}
		}

		internal virtual Label Label7
		{
			get
			{
				return this._Label7;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Label7_Click);
				if (this._Label7 != null)
				{
					this._Label7.Click -= value2;
				}
				this._Label7 = value;
				if (this._Label7 != null)
				{
					this._Label7.Click += value2;
				}
			}
		}

		internal virtual Label Label8
		{
			get
			{
				return this._Label8;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Label8_Click);
				if (this._Label8 != null)
				{
					this._Label8.Click -= value2;
				}
				this._Label8 = value;
				if (this._Label8 != null)
				{
					this._Label8.Click += value2;
				}
			}
		}

		internal virtual Timer Timer2
		{
			get
			{
				return this._Timer2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Timer2_Tick);
				if (this._Timer2 != null)
				{
					this._Timer2.Tick -= value2;
				}
				this._Timer2 = value;
				if (this._Timer2 != null)
				{
					this._Timer2.Tick += value2;
				}
			}
		}

		internal virtual Label Label9
		{
			get
			{
				return this._Label9;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label9 = value;
			}
		}

		internal virtual TextBox TextBox1
		{
			get
			{
				return this._TextBox1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TextBox1 = value;
			}
		}

		internal virtual Label Label10
		{
			get
			{
				return this._Label10;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label10 = value;
			}
		}

		public bool Minimized
		{
			get
			{
				return this._Minimized;
			}
			set
			{
				this._Minimized = value;
			}
		}

		public MainForm()
		{
			base.Closing += new CancelEventHandler(this.Form1_Closing);
			base.Load += new EventHandler(this.MainForm_Load);
			base.Disposed += new EventHandler(this.MainForm_Disposed);
			this.CountList = new List<Counter>();
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
			this.Panel1 = new Panel();
			this.Label8 = new Label();
			this.Label7 = new Label();
			this.Label3 = new Label();
			this.Label2 = new Label();
			this.Label1 = new Label();
			this.Timer1 = new Timer(this.components);
			this.Panel2 = new Panel();
			this.Label5 = new Label();
			this.Label6 = new Label();
			this.Panel3 = new Panel();
			this.Label4 = new Label();
			this.Panel4 = new Panel();
			this.ContextMenuStrip1 = new ContextMenuStrip(this.components);
			this.SettingsToolStripMenuItem = new ToolStripMenuItem();
			this.ShowBotsToolStripMenuItem = new ToolStripMenuItem();
			this.LoadBotsToolStripMenuItem = new ToolStripMenuItem();
			this.NotifyIcon1 = new NotifyIcon(this.components);
			this.Timer2 = new Timer(this.components);
			this.MyGroupBox1 = new myGroupBox();
			this.Label9 = new Label();
			this.TextBox1 = new TextBox();
			this.CAccounts = new ComboBox();
			this.CRealm = new ComboBox();
			this.CPlugin = new ComboBox();
			this.LRealm = new Label();
			this.Label15 = new Label();
			this.LPlugin = new Label();
			this.MyGroupBox2 = new myGroupBox();
			this.CProxyPass = new TextBox();
			this.CProxyPort = new TextBox();
			this.CProxyUser = new TextBox();
			this.LProxy = new Label();
			this.Label21 = new Label();
			this.Label18 = new Label();
			this.CProxy = new ComboBox();
			this.Label19 = new Label();
			this.MainTab = new CTabControl();
			this.Label10 = new Label();
			this.Panel1.SuspendLayout();
			this.Panel2.SuspendLayout();
			this.Panel3.SuspendLayout();
			this.Panel4.SuspendLayout();
			this.ContextMenuStrip1.SuspendLayout();
			this.MyGroupBox1.SuspendLayout();
			this.MyGroupBox2.SuspendLayout();
			this.SuspendLayout();
			this.Panel1.BackColor = Color.FromArgb(35, 35, 35);
			this.Panel1.BorderStyle = BorderStyle.FixedSingle;
			this.Panel1.Controls.Add(this.Label10);
			this.Panel1.Controls.Add(this.Label8);
			this.Panel1.Controls.Add(this.Label7);
			this.Panel1.Controls.Add(this.Label3);
			this.Panel1.Controls.Add(this.Label2);
			this.Panel1.Controls.Add(this.Label1);
			Control arg_2E9_0 = this.Panel1;
			Point location = new Point(0, 0);
			arg_2E9_0.Location = location;
			this.Panel1.Name = "Panel1";
			Control arg_314_0 = this.Panel1;
			Size size = new Size(551, 28);
			arg_314_0.Size = size;
			this.Panel1.TabIndex = 27;
			this.Label8.AutoSize = true;
			this.Label8.BorderStyle = BorderStyle.FixedSingle;
			this.Label8.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Label8.ForeColor = Color.Gray;
			Control arg_37F_0 = this.Label8;
			location = new Point(498, -1);
			arg_37F_0.Location = location;
			this.Label8.Name = "Label8";
			Control arg_3A7_0 = this.Label8;
			size = new Size(27, 22);
			arg_3A7_0.Size = size;
			this.Label8.TabIndex = 30;
			this.Label8.Text = " - ";
			this.Label7.AutoSize = true;
			this.Label7.BorderStyle = BorderStyle.FixedSingle;
			this.Label7.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.Label7.ForeColor = Color.Gray;
			Control arg_422_0 = this.Label7;
			location = new Point(527, -1);
			arg_422_0.Location = location;
			this.Label7.Name = "Label7";
			Control arg_44A_0 = this.Label7;
			size = new Size(23, 22);
			arg_44A_0.Size = size;
			this.Label7.TabIndex = 29;
			this.Label7.Text = "X";
			this.Label3.AutoSize = true;
			this.Label3.ForeColor = Color.Gray;
			Control arg_49A_0 = this.Label3;
			location = new Point(50, 13);
			arg_49A_0.Location = location;
			this.Label3.Name = "Label3";
			Control arg_4C2_0 = this.Label3;
			size = new Size(54, 13);
			arg_4C2_0.Size = size;
			this.Label3.TabIndex = 28;
			this.Label3.Text = "Attempts: ";
			this.Label2.AutoSize = true;
			this.Label2.BorderStyle = BorderStyle.FixedSingle;
			this.Label2.ForeColor = Color.Gray;
			Control arg_51C_0 = this.Label2;
			location = new Point(-1, -1);
			arg_51C_0.Location = location;
			this.Label2.Name = "Label2";
			Control arg_544_0 = this.Label2;
			size = new Size(45, 15);
			arg_544_0.Size = size;
			this.Label2.TabIndex = 27;
			this.Label2.Text = "Options";
			this.Label1.AutoSize = true;
			this.Label1.Font = new Font("Buxton Sketch", 20.25f, FontStyle.Italic, GraphicsUnit.Point, 0);
			this.Label1.ForeColor = Color.Gray;
			Control arg_5B4_0 = this.Label1;
			location = new Point(239, -4);
			arg_5B4_0.Location = location;
			this.Label1.Name = "Label1";
			Control arg_5DC_0 = this.Label1;
			size = new Size(85, 33);
			arg_5DC_0.Size = size;
			this.Label1.TabIndex = 0;
			this.Label1.Text = "BioBot";
			this.Timer1.Enabled = true;
			this.Timer1.Interval = 1000;
			this.Panel2.BorderStyle = BorderStyle.FixedSingle;
			this.Panel2.Controls.Add(this.Label5);
			this.Panel2.Controls.Add(this.MyGroupBox1);
			this.Panel2.Controls.Add(this.Label6);
			this.Panel2.Controls.Add(this.MyGroupBox2);
			Control arg_691_0 = this.Panel2;
			location = new Point(-194, 5);
			arg_691_0.Location = location;
			this.Panel2.Name = "Panel2";
			Control arg_6BF_0 = this.Panel2;
			size = new Size(194, 251);
			arg_6BF_0.Size = size;
			this.Panel2.TabIndex = 27;
			this.Label5.AutoSize = true;
			this.Label5.Font = new Font("Buxton Sketch", 14.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			this.Label5.ForeColor = Color.Gray;
			Control arg_71B_0 = this.Label5;
			location = new Point(57, -1);
			arg_71B_0.Location = location;
			this.Label5.Name = "Label5";
			Control arg_743_0 = this.Label5;
			size = new Size(78, 23);
			arg_743_0.Size = size;
			this.Label5.TabIndex = 29;
			this.Label5.Text = "Settings";
			this.Label6.AutoSize = true;
			this.Label6.Font = new Font("Buxton Sketch", 14.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			this.Label6.ForeColor = Color.Gray;
			Control arg_7B0_0 = this.Label6;
			location = new Point(71, 122);
			arg_7B0_0.Location = location;
			this.Label6.Name = "Label6";
			Control arg_7D8_0 = this.Label6;
			size = new Size(54, 23);
			arg_7D8_0.Size = size;
			this.Label6.TabIndex = 30;
			this.Label6.Text = "Proxy";
			this.Panel3.BorderStyle = BorderStyle.FixedSingle;
			this.Panel3.Controls.Add(this.Label4);
			this.Panel3.Controls.Add(this.MainTab);
			Control arg_846_0 = this.Panel3;
			location = new Point(550, 5);
			arg_846_0.Location = location;
			this.Panel3.Name = "Panel3";
			Control arg_874_0 = this.Panel3;
			size = new Size(200, 250);
			arg_874_0.Size = size;
			this.Panel3.TabIndex = 28;
			this.Label4.AutoSize = true;
			this.Label4.Font = new Font("Buxton Sketch", 21.75f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
			this.Label4.ForeColor = Color.Gray;
			Control arg_8D1_0 = this.Label4;
			location = new Point(-2, 101);
			arg_8D1_0.Location = location;
			this.Label4.Name = "Label4";
			Control arg_8FC_0 = this.Label4;
			size = new Size(195, 36);
			arg_8FC_0.Size = size;
			this.Label4.TabIndex = 29;
			this.Label4.Text = "No Bots Loaded";
			this.Panel4.BorderStyle = BorderStyle.FixedSingle;
			this.Panel4.Controls.Add(this.Panel2);
			this.Panel4.Controls.Add(this.Panel3);
			Control arg_967_0 = this.Panel4;
			location = new Point(1, 27);
			arg_967_0.Location = location;
			this.Panel4.Name = "Panel4";
			Control arg_995_0 = this.Panel4;
			size = new Size(550, 262);
			arg_995_0.Size = size;
			this.Panel4.TabIndex = 29;
			this.ContextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.SettingsToolStripMenuItem,
				this.ShowBotsToolStripMenuItem,
				this.LoadBotsToolStripMenuItem
			});
			this.ContextMenuStrip1.Name = "ContextMenuStrip1";
			this.ContextMenuStrip1.ShowImageMargin = false;
			Control arg_A0E_0 = this.ContextMenuStrip1;
			size = new Size(127, 76);
			arg_A0E_0.Size = size;
			this.SettingsToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
			this.SettingsToolStripMenuItem.Font = new Font("Buxton Sketch", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.SettingsToolStripMenuItem.ForeColor = Color.Gray;
			this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
			ToolStripItem arg_A79_0 = this.SettingsToolStripMenuItem;
			size = new Size(126, 24);
			arg_A79_0.Size = size;
			this.SettingsToolStripMenuItem.Text = "Settings";
			this.ShowBotsToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
			this.ShowBotsToolStripMenuItem.Font = new Font("Buxton Sketch", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.ShowBotsToolStripMenuItem.ForeColor = Color.Gray;
			this.ShowBotsToolStripMenuItem.Name = "ShowBotsToolStripMenuItem";
			ToolStripItem arg_AF4_0 = this.ShowBotsToolStripMenuItem;
			size = new Size(126, 24);
			arg_AF4_0.Size = size;
			this.ShowBotsToolStripMenuItem.Text = "Show Bots";
			this.LoadBotsToolStripMenuItem.BackColor = Color.FromArgb(43, 43, 43);
			this.LoadBotsToolStripMenuItem.Font = new Font("Buxton Sketch", 12f, FontStyle.Bold);
			this.LoadBotsToolStripMenuItem.ForeColor = Color.Gray;
			this.LoadBotsToolStripMenuItem.Name = "LoadBotsToolStripMenuItem";
			ToolStripItem arg_B6D_0 = this.LoadBotsToolStripMenuItem;
			size = new Size(126, 24);
			arg_B6D_0.Size = size;
			this.LoadBotsToolStripMenuItem.Text = "Load Bots";
			this.NotifyIcon1.Icon = (Icon)componentResourceManager.GetObject("NotifyIcon1.Icon");
			this.NotifyIcon1.Text = "BioBot";
			this.NotifyIcon1.Visible = true;
			this.Timer2.Enabled = true;
			this.MyGroupBox1.BorderColorz = Color.Gray;
			this.MyGroupBox1.Controls.Add(this.Label9);
			this.MyGroupBox1.Controls.Add(this.TextBox1);
			this.MyGroupBox1.Controls.Add(this.CAccounts);
			this.MyGroupBox1.Controls.Add(this.CRealm);
			this.MyGroupBox1.Controls.Add(this.CPlugin);
			this.MyGroupBox1.Controls.Add(this.LRealm);
			this.MyGroupBox1.Controls.Add(this.Label15);
			this.MyGroupBox1.Controls.Add(this.LPlugin);
			Control arg_C96_0 = this.MyGroupBox1;
			location = new Point(3, 11);
			arg_C96_0.Location = location;
			this.MyGroupBox1.Name = "MyGroupBox1";
			Control arg_CC1_0 = this.MyGroupBox1;
			size = new Size(186, 114);
			arg_CC1_0.Size = size;
			this.MyGroupBox1.TabIndex = 29;
			this.MyGroupBox1.TabStop = false;
			this.Label9.AutoSize = true;
			this.Label9.ForeColor = Color.Gray;
			Control arg_D0D_0 = this.Label9;
			location = new Point(12, 93);
			arg_D0D_0.Location = location;
			this.Label9.Name = "Label9";
			Control arg_D35_0 = this.Label9;
			size = new Size(34, 13);
			arg_D35_0.Size = size;
			this.Label9.TabIndex = 20;
			this.Label9.Text = "Delay";
			this.TextBox1.BackColor = Color.FromArgb(43, 43, 43);
			this.TextBox1.BorderStyle = BorderStyle.FixedSingle;
			this.TextBox1.ForeColor = Color.Gray;
			Control arg_D9B_0 = this.TextBox1;
			location = new Point(59, 91);
			arg_D9B_0.Location = location;
			this.TextBox1.Name = "TextBox1";
			Control arg_DC3_0 = this.TextBox1;
			size = new Size(100, 20);
			arg_DC3_0.Size = size;
			this.TextBox1.TabIndex = 19;
			this.CAccounts.BackColor = Color.FromArgb(43, 43, 43);
			this.CAccounts.FlatStyle = FlatStyle.Flat;
			this.CAccounts.ForeColor = Color.Gray;
			this.CAccounts.FormattingEnabled = true;
			Control arg_E25_0 = this.CAccounts;
			location = new Point(59, 67);
			arg_E25_0.Location = location;
			Control arg_E3A_0 = this.CAccounts;
			Padding margin = new Padding(2);
			arg_E3A_0.Margin = margin;
			this.CAccounts.Name = "CAccounts";
			Control arg_E62_0 = this.CAccounts;
			size = new Size(91, 21);
			arg_E62_0.Size = size;
			this.CAccounts.TabIndex = 18;
			this.CRealm.AutoCompleteMode = AutoCompleteMode.Append;
			this.CRealm.AutoCompleteSource = AutoCompleteSource.ListItems;
			this.CRealm.BackColor = Color.FromArgb(43, 43, 43);
			this.CRealm.FlatStyle = FlatStyle.Flat;
			this.CRealm.ForeColor = Color.Gray;
			this.CRealm.FormattingEnabled = true;
			this.CRealm.Items.AddRange(new object[]
			{
				"UsEast",
				"UsWest",
				"Europe",
				"Asia"
			});
			Control arg_F1E_0 = this.CRealm;
			location = new Point(59, 39);
			arg_F1E_0.Location = location;
			Control arg_F33_0 = this.CRealm;
			margin = new Padding(2);
			arg_F33_0.Margin = margin;
			this.CRealm.Name = "CRealm";
			Control arg_F5B_0 = this.CRealm;
			size = new Size(91, 21);
			arg_F5B_0.Size = size;
			this.CRealm.TabIndex = 12;
			this.CPlugin.BackColor = Color.FromArgb(43, 43, 43);
			this.CPlugin.FlatStyle = FlatStyle.Flat;
			this.CPlugin.ForeColor = Color.Gray;
			this.CPlugin.FormattingEnabled = true;
			Control arg_FBD_0 = this.CPlugin;
			location = new Point(59, 13);
			arg_FBD_0.Location = location;
			Control arg_FD2_0 = this.CPlugin;
			margin = new Padding(2);
			arg_FD2_0.Margin = margin;
			this.CPlugin.Name = "CPlugin";
			Control arg_FFA_0 = this.CPlugin;
			size = new Size(91, 21);
			arg_FFA_0.Size = size;
			this.CPlugin.TabIndex = 4;
			this.LRealm.AutoSize = true;
			this.LRealm.ForeColor = Color.Gray;
			Control arg_1039_0 = this.LRealm;
			location = new Point(11, 42);
			arg_1039_0.Location = location;
			Control arg_1051_0 = this.LRealm;
			margin = new Padding(2, 0, 2, 0);
			arg_1051_0.Margin = margin;
			this.LRealm.Name = "LRealm";
			Control arg_1079_0 = this.LRealm;
			size = new Size(37, 13);
			arg_1079_0.Size = size;
			this.LRealm.TabIndex = 11;
			this.LRealm.Text = "Realm";
			this.Label15.AutoSize = true;
			this.Label15.ForeColor = Color.Gray;
			Control arg_10C8_0 = this.Label15;
			location = new Point(5, 70);
			arg_10C8_0.Location = location;
			Control arg_10E0_0 = this.Label15;
			margin = new Padding(2, 0, 2, 0);
			arg_10E0_0.Margin = margin;
			this.Label15.Name = "Label15";
			Control arg_1108_0 = this.Label15;
			size = new Size(52, 13);
			arg_1108_0.Size = size;
			this.Label15.TabIndex = 17;
			this.Label15.Text = "Accounts";
			this.LPlugin.AutoSize = true;
			this.LPlugin.ForeColor = Color.Gray;
			Control arg_1158_0 = this.LPlugin;
			location = new Point(11, 16);
			arg_1158_0.Location = location;
			Control arg_1170_0 = this.LPlugin;
			margin = new Padding(2, 0, 2, 0);
			arg_1170_0.Margin = margin;
			this.LPlugin.Name = "LPlugin";
			Control arg_1198_0 = this.LPlugin;
			size = new Size(29, 13);
			arg_1198_0.Size = size;
			this.LPlugin.TabIndex = 3;
			this.LPlugin.Text = "Core";
			this.MyGroupBox2.BorderColorz = Color.Gray;
			this.MyGroupBox2.Controls.Add(this.CProxyPass);
			this.MyGroupBox2.Controls.Add(this.CProxyPort);
			this.MyGroupBox2.Controls.Add(this.CProxyUser);
			this.MyGroupBox2.Controls.Add(this.LProxy);
			this.MyGroupBox2.Controls.Add(this.Label21);
			this.MyGroupBox2.Controls.Add(this.Label18);
			this.MyGroupBox2.Controls.Add(this.CProxy);
			this.MyGroupBox2.Controls.Add(this.Label19);
			Control arg_128D_0 = this.MyGroupBox2;
			location = new Point(3, 131);
			arg_128D_0.Location = location;
			this.MyGroupBox2.Name = "MyGroupBox2";
			Control arg_12B8_0 = this.MyGroupBox2;
			size = new Size(186, 116);
			arg_12B8_0.Size = size;
			this.MyGroupBox2.TabIndex = 29;
			this.MyGroupBox2.TabStop = false;
			this.CProxyPass.BackColor = Color.FromArgb(43, 43, 43);
			this.CProxyPass.BorderStyle = BorderStyle.FixedSingle;
			this.CProxyPass.ForeColor = Color.Gray;
			Control arg_131A_0 = this.CProxyPass;
			location = new Point(55, 92);
			arg_131A_0.Location = location;
			Control arg_132F_0 = this.CProxyPass;
			margin = new Padding(2);
			arg_132F_0.Margin = margin;
			this.CProxyPass.Name = "CProxyPass";
			Control arg_1357_0 = this.CProxyPass;
			size = new Size(99, 20);
			arg_1357_0.Size = size;
			this.CProxyPass.TabIndex = 15;
			this.CProxyPass.UseSystemPasswordChar = true;
			this.CProxyPort.BackColor = Color.FromArgb(43, 43, 43);
			this.CProxyPort.BorderStyle = BorderStyle.FixedSingle;
			this.CProxyPort.ForeColor = Color.Gray;
			Control arg_13B9_0 = this.CProxyPort;
			location = new Point(55, 44);
			arg_13B9_0.Location = location;
			Control arg_13CE_0 = this.CProxyPort;
			margin = new Padding(2);
			arg_13CE_0.Margin = margin;
			this.CProxyPort.Name = "CProxyPort";
			Control arg_13F6_0 = this.CProxyPort;
			size = new Size(38, 20);
			arg_13F6_0.Size = size;
			this.CProxyPort.TabIndex = 17;
			this.CProxyUser.BackColor = Color.FromArgb(43, 43, 43);
			this.CProxyUser.BorderStyle = BorderStyle.FixedSingle;
			this.CProxyUser.ForeColor = Color.Gray;
			Control arg_144C_0 = this.CProxyUser;
			location = new Point(55, 68);
			arg_144C_0.Location = location;
			Control arg_1461_0 = this.CProxyUser;
			margin = new Padding(2);
			arg_1461_0.Margin = margin;
			this.CProxyUser.Name = "CProxyUser";
			Control arg_1489_0 = this.CProxyUser;
			size = new Size(99, 20);
			arg_1489_0.Size = size;
			this.CProxyUser.TabIndex = 14;
			this.LProxy.AutoSize = true;
			this.LProxy.ForeColor = Color.Gray;
			Control arg_14C9_0 = this.LProxy;
			location = new Point(12, 22);
			arg_14C9_0.Location = location;
			Control arg_14E1_0 = this.LProxy;
			margin = new Padding(2, 0, 2, 0);
			arg_14E1_0.Margin = margin;
			this.LProxy.Name = "LProxy";
			Control arg_1509_0 = this.LProxy;
			size = new Size(32, 13);
			arg_1509_0.Size = size;
			this.LProxy.TabIndex = 5;
			this.LProxy.Text = "Addr:";
			this.Label21.AutoSize = true;
			this.Label21.ForeColor = Color.Gray;
			Control arg_1558_0 = this.Label21;
			location = new Point(12, 46);
			arg_1558_0.Location = location;
			Control arg_1570_0 = this.Label21;
			margin = new Padding(2, 0, 2, 0);
			arg_1570_0.Margin = margin;
			this.Label21.Name = "Label21";
			Control arg_1598_0 = this.Label21;
			size = new Size(29, 13);
			arg_1598_0.Size = size;
			this.Label21.TabIndex = 16;
			this.Label21.Text = "Port:";
			this.Label18.AutoSize = true;
			this.Label18.ForeColor = Color.Gray;
			Control arg_15E8_0 = this.Label18;
			location = new Point(12, 70);
			arg_15E8_0.Location = location;
			Control arg_1600_0 = this.Label18;
			margin = new Padding(2, 0, 2, 0);
			arg_1600_0.Margin = margin;
			this.Label18.Name = "Label18";
			Control arg_1628_0 = this.Label18;
			size = new Size(32, 13);
			arg_1628_0.Size = size;
			this.Label18.TabIndex = 6;
			this.Label18.Text = "User:";
			this.CProxy.BackColor = Color.FromArgb(43, 43, 43);
			this.CProxy.FlatStyle = FlatStyle.Flat;
			this.CProxy.ForeColor = Color.Gray;
			this.CProxy.FormattingEnabled = true;
			Control arg_1699_0 = this.CProxy;
			location = new Point(55, 19);
			arg_1699_0.Location = location;
			Control arg_16AE_0 = this.CProxy;
			margin = new Padding(2);
			arg_16AE_0.Margin = margin;
			this.CProxy.Name = "CProxy";
			Control arg_16D6_0 = this.CProxy;
			size = new Size(98, 21);
			arg_16D6_0.Size = size;
			this.CProxy.TabIndex = 7;
			this.Label19.AutoSize = true;
			this.Label19.ForeColor = Color.Gray;
			Control arg_1715_0 = this.Label19;
			location = new Point(11, 94);
			arg_1715_0.Location = location;
			Control arg_172D_0 = this.Label19;
			margin = new Padding(2, 0, 2, 0);
			arg_172D_0.Margin = margin;
			this.Label19.Name = "Label19";
			Control arg_1755_0 = this.Label19;
			size = new Size(33, 13);
			arg_1755_0.Size = size;
			this.Label19.TabIndex = 8;
			this.Label19.Text = "Pass:";
			this.MainTab.Alignment = TabAlignment.Bottom;
			TabControl arg_1795_0 = this.MainTab;
			size = new Size(47, 20);
			arg_1795_0.ItemSize = size;
			Control arg_17AA_0 = this.MainTab;
			location = new Point(2, 2);
			arg_17AA_0.Location = location;
			Control arg_17BF_0 = this.MainTab;
			margin = new Padding(2);
			arg_17BF_0.Margin = margin;
			this.MainTab.Multiline = true;
			this.MainTab.Name = "MainTab";
			this.MainTab.SelectedIndex = 0;
			Control arg_1805_0 = this.MainTab;
			size = new Size(194, 251);
			arg_1805_0.Size = size;
			this.MainTab.SizeMode = TabSizeMode.Fixed;
			this.MainTab.TabIndex = 0;
			this.Label10.AutoSize = true;
			this.Label10.ForeColor = Color.Gray;
			Control arg_184F_0 = this.Label10;
			location = new Point(50, 0);
			arg_184F_0.Location = location;
			this.Label10.Name = "Label10";
			Control arg_1877_0 = this.Label10;
			size = new Size(65, 13);
			arg_1877_0.Size = size;
			this.Label10.TabIndex = 31;
			this.Label10.Text = "Connected: ";
			SizeF autoScaleDimensions = new SizeF(6f, 13f);
			this.AutoScaleDimensions = autoScaleDimensions;
			this.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.FromArgb(43, 43, 43);
			size = new Size(551, 289);
			this.ClientSize = size;
			this.Controls.Add(this.Panel4);
			this.Controls.Add(this.Panel1);
			this.FormBorderStyle = FormBorderStyle.None;
			this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			margin = new Padding(2);
			this.Margin = margin;
			this.Name = "MainForm";
			this.Text = "Dbot";
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			this.Panel2.ResumeLayout(false);
			this.Panel2.PerformLayout();
			this.Panel3.ResumeLayout(false);
			this.Panel3.PerformLayout();
			this.Panel4.ResumeLayout(false);
			this.ContextMenuStrip1.ResumeLayout(false);
			this.MyGroupBox1.ResumeLayout(false);
			this.MyGroupBox1.PerformLayout();
			this.MyGroupBox2.ResumeLayout(false);
			this.MyGroupBox2.PerformLayout();
			this.ResumeLayout(false);
		}

		private void MainForm_Disposed(object sender, EventArgs e)
		{
		}

		private void Form1_Closing(object sender, CancelEventArgs e)
		{
			MySettingsProperty.Settings.Realm = this.CRealm.SelectedIndex;
			MySettingsProperty.Settings.Accounts = this.CAccounts.SelectedIndex;
			MySettingsProperty.Settings.Delay = this.TextBox1.Text;
			MySettingsProperty.Settings.Plugin = this.CPlugin.SelectedIndex;
			MySettingsProperty.Settings.Save();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.MainTab.BackColor = Color.FromArgb(43, 43, 43);
			this.CRealm.Text = Conversions.ToString(this.CRealm.Items[0]);
			this.Minimized = false;
			if (!Directory.Exists(Application.StartupPath + "\\Cores"))
			{
				Directory.CreateDirectory(Application.StartupPath + "\\Cores");
			}
			this.Plugins = PluginServices.FindPlugins(Application.StartupPath + "\\Cores", "BioBot.IPluginModule");
			if (this.Plugins == null)
			{
				Interaction.MsgBox("No valid cores found", MsgBoxStyle.Critical, null);
				this.Close();
			}
			int arg_B1_0 = 0;
			checked
			{
				int num = this.Plugins.Length - 1;
				for (int i = arg_B1_0; i <= num; i++)
				{
					this.CPlugin.Items.Add(this.Plugins[i].AssemblyPath.Replace(Application.StartupPath + "\\Cores\\", ""));
				}
				this.CPlugin.Text = Conversions.ToString(this.CPlugin.Items[0]);
				List<string> fileStrings = MainForm.GetFileStrings("Settings\\cdkeys.txt");
				string[] array = new string[fileStrings.Count - 1 + 1];
				fileStrings.CopyTo(array);
				this.KeyListEast = new List<string>(array);
				this.KeyListWest = new List<string>(array);
				this.KeyListAsia = new List<string>(array);
				this.KeyListEurope = new List<string>(array);
				this.Proxies = MainForm.GetFileStrings("Settings\\proxies.txt");
                foreach (var proxy in this.Proxies)
                    this.CProxy.Items.Add(proxy.Split(':')[0]);
				this.Proxies.Insert(0, "");
				this.CProxy.Items.Insert(0, "");
				this.CProxy.SelectedIndex = 0;
				if (!Directory.Exists(Application.StartupPath + "\\Settings\\Accounts"))
				{
					Directory.CreateDirectory(Application.StartupPath + "\\Settings\\Accounts");
				}
				else
				{
					string[] files = Directory.GetFiles(Application.StartupPath + "\\Settings\\Accounts");
					string[] array2 = files;
					for (int j = 0; j < array2.Length; j++)
					{
						string text = array2[j];
						string text2 = text.Replace(Application.StartupPath + "\\Settings\\Accounts\\", "");
						this.CAccounts.Items.Add(text2);
						List<string> fileStrings2 = MainForm.GetFileStrings("Settings\\Accounts\\" + text2);
						int arg_2AF_0 = 0;
						int num2 = fileStrings2.Count - 1;
						for (int k = arg_2AF_0; k <= num2; k++)
						{
							fileStrings2.Remove(Conversions.ToString(k));
						}
						MainForm.Accounts.Add(text2, fileStrings2);
					}
					this.CAccounts.Text = Conversions.ToString(this.CAccounts.Items[0]);
				}
				this.Label1.Text = "BioBot - " + this.CPlugin.Text.Replace(".dll", "");
				Control arg_397_0 = this.Label1;
				Point location = new Point((int)Math.Round(unchecked((double)this.Size.Width / 2.0 - (double)this.Label1.Width / 2.0)), this.Label1.Location.Y);
				arg_397_0.Location = location;
				this.CRealm.SelectedIndex = MySettingsProperty.Settings.Realm;
				this.CPlugin.SelectedIndex = MySettingsProperty.Settings.Plugin;
				this.TextBox1.Text = MySettingsProperty.Settings.Delay;
				this.CAccounts.SelectedIndex = MySettingsProperty.Settings.Accounts;
			}
		}

		public static List<string> GetFileStrings(string File)
		{
			List<string> list = new List<string>();
			FileStream fileStream = new FileStream(File, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamReader streamReader = new StreamReader(fileStream);
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				if (Operators.CompareString(text, "", false) != 0 | Operators.CompareString(text, " ", false) == 0)
				{
					list.Add(text);
				}
			}
			streamReader.Close();
			fileStream.Close();
			return list;
		}

		private void CProxy_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CProxyPort.Text = "";
			this.CProxyUser.Text = "";
			this.CProxyPass.Text = "";
			string text = this.Proxies[this.CProxy.SelectedIndex];
			if (Operators.CompareString(text, "", false) != 0)
			{
				this.CProxy.Text = text.Split(new char[]
				{
					':'
				})[0];
				this.CProxyPort.Text = text.Split(new char[]
				{
					'/'
				})[0].Split(new char[]
				{
					':'
				})[1];
				this.CProxyUser.Text = text.Split(new char[]
				{
					'/'
				})[1];
				this.CProxyPass.Text = text.Split(new char[]
				{
					'/'
				})[2];
			}
		}

		private void Label2_Click(object sender, EventArgs e)
		{
			ToolStripDropDown arg_2D_0 = this.ContextMenuStrip1;
			Point screenLocation = new Point(this.Location.X, checked(this.Location.Y + 16));
			arg_2D_0.Show(screenLocation);
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			this.Label3.Text = "Attempts: " + Conversions.ToString(Counter.Count);
			this.Label10.Text = "Connected: " + Conversions.ToString(MainForm.BotsConnected);
		}

		private void ConnectToolStripMenuItem1_Click(object sender, EventArgs e)
		{
		}

		private void Label4_Click(object sender, EventArgs e)
		{
		}

		private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
		}

		private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.Panel2.Location.X == -194)
			{
				Control arg_3C_0 = this.Panel2;
				int arg_36_1 = -1;
				Point location = this.Panel2.Location;
				Point location2 = new Point(arg_36_1, location.Y);
				arg_3C_0.Location = location2;
				this.SettingsToolStripMenuItem.Text = "Hide Settings";
			}
			else
			{
				Control arg_79_0 = this.Panel2;
				int arg_73_1 = -194;
				Point location2 = this.Panel2.Location;
				Point location = new Point(arg_73_1, location2.Y);
				arg_79_0.Location = location;
				this.SettingsToolStripMenuItem.Text = "Settings";
			}
		}

		private void LoadBotsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Label4.Hide();
			MainForm.Realm = this.CRealm.SelectedItem.ToString();
			MainForm.Delay = Conversions.ToInteger(this.TextBox1.Text);
			string left = ConnectInfo.RealmList.AddressFromName(this.CRealm.Text);
			checked
			{
				List<string> cdKeys = null;
				if (Operators.CompareString(left, "useast.battle.net", false) == 0)
				{
					string[] array = new string[MainForm.Accounts[this.CAccounts.Text].Count - 1 + 1];
					try
					{
						this.KeyListEast.CopyTo(0, array, 0, array.Length);
						cdKeys = new List<string>(array);
						this.KeyListEast.RemoveRange(0, array.Length);
						goto IL_24F;
					}
					catch (Exception expr_A7)
					{
						ProjectData.SetProjectError(expr_A7);
						Interaction.MsgBox("Not enough Keys Available", MsgBoxStyle.OkOnly, null);
						ProjectData.ClearProjectError();
						return;
					}
				}
				if (Operators.CompareString(left, "uswest.battle.net", false) == 0)
				{
					string[] array2 = new string[MainForm.Accounts[this.CAccounts.Text].Count - 1 + 1];
					try
					{
						this.KeyListWest.CopyTo(0, array2, 0, array2.Length);
						cdKeys = new List<string>(array2);
						this.KeyListWest.RemoveRange(0, array2.Length);
						goto IL_24F;
					}
					catch (Exception expr_12B)
					{
						ProjectData.SetProjectError(expr_12B);
						Interaction.MsgBox("Not enough Keys Available", MsgBoxStyle.OkOnly, null);
						ProjectData.ClearProjectError();
						return;
					}
				}
				if (Operators.CompareString(left, "asia.battle.net", false) == 0)
				{
					string[] array3 = new string[MainForm.Accounts[this.CAccounts.Text].Count - 1 + 1];
					try
					{
						this.KeyListAsia.CopyTo(0, array3, 0, array3.Length);
						cdKeys = new List<string>(array3);
						this.KeyListAsia.RemoveRange(0, array3.Length);
						goto IL_24F;
					}
					catch (Exception expr_1AF)
					{
						ProjectData.SetProjectError(expr_1AF);
						Interaction.MsgBox("Not enough Keys Available", MsgBoxStyle.OkOnly, null);
						ProjectData.ClearProjectError();
						return;
					}
				}
				if (Operators.CompareString(left, "europe.battle.net", false) == 0)
				{
					string[] array4 = new string[MainForm.Accounts[this.CAccounts.Text].Count - 1 + 1];
					try
					{
						this.KeyListEurope.CopyTo(0, array4, 0, array4.Length);
						cdKeys = new List<string>(array4);
						this.KeyListEurope.RemoveRange(0, array4.Length);
					}
					catch (Exception expr_230)
					{
						ProjectData.SetProjectError(expr_230);
						Interaction.MsgBox("Not enough Keys Available", MsgBoxStyle.OkOnly, null);
						ProjectData.ClearProjectError();
						return;
					}
				}
				IL_24F:
				string proxy = "";
				if (Operators.CompareString(this.CProxy.Text, "", false) != 0)
				{
					proxy = string.Concat(new string[]
					{
						this.CProxy.Text,
						":",
						this.CProxyPort.Text,
						"/",
						this.CProxyUser.Text,
						"/",
						this.CProxyPass.Text
					});
				}
				Plugintab value = new Plugintab(this.Plugins[this.CPlugin.SelectedIndex], ConnectInfo.RealmList.AddressFromName(this.CRealm.Text), cdKeys, MainForm.Accounts[this.CAccounts.Text], proxy);
				this.MainTab.TabPages.Add(value);
			}
		}

		private void Panel1_MouseDown(object sender, MouseEventArgs e)
		{
			this.drag = true;
			checked
			{
				this.mousex = Cursor.Position.X - this.Left;
				this.mousey = Cursor.Position.Y - this.Top;
			}
		}

		private void Panel1_MouseMove(object sender, MouseEventArgs e)
		{
			checked
			{
				if (this.drag)
				{
					this.Top = Cursor.Position.Y - this.mousey;
					this.Left = Cursor.Position.X - this.mousex;
				}
			}
		}

		private void Panel1_MouseUp(object sender, MouseEventArgs e)
		{
			this.drag = false;
		}

		private void ShowBotsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.Panel3.Location.X == 550)
			{
				Control arg_40_0 = this.Panel3;
				int arg_3A_1 = 349;
				Point location = this.Panel3.Location;
				Point location2 = new Point(arg_3A_1, location.Y);
				arg_40_0.Location = location2;
				this.ShowBotsToolStripMenuItem.Text = "Hide Bots";
			}
			else
			{
				Control arg_7D_0 = this.Panel3;
				int arg_77_1 = 550;
				Point location2 = this.Panel3.Location;
				Point location = new Point(arg_77_1, location2.Y);
				arg_7D_0.Location = location;
				this.ShowBotsToolStripMenuItem.Text = "Show Bots";
			}
		}

		private void Label7_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Label8_Click(object sender, EventArgs e)
		{
			this.Hide();
			this.Minimized = true;
		}

		private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
			this.Minimized = false;
		}

		private void Timer2_Tick(object sender, EventArgs e)
		{
			if (this.Minimized & !string.IsNullOrEmpty(Counter.CreatzString))
			{
				this.NotifyIcon1.ShowBalloonTip(3600000, "BioBot", Counter.CreatzString, ToolTipIcon.Info);
				Counter.CreatzString = "";
			}
		}
	}
}

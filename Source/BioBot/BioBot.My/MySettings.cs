using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BioBot.My
{
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0"), EditorBrowsable(EditorBrowsableState.Advanced), CompilerGenerated]
	internal sealed class MySettings : ApplicationSettingsBase
	{
		private static MySettings defaultInstance = (MySettings)SettingsBase.Synchronized(new MySettings());

		private static bool addedHandler;

		private static object addedHandlerLockObject = RuntimeHelpers.GetObjectValue(new object());

		public static MySettings Default
		{
			get
			{
				if (!MySettings.addedHandler)
				{
					object obj = MySettings.addedHandlerLockObject;
					ObjectFlowControl.CheckForSyncLockOnValueType(obj);
					lock (obj)
					{
						if (!MySettings.addedHandler)
						{
							MyProject.Application.Shutdown += new ShutdownEventHandler(MySettings.AutoSaveSettings);
							MySettings.addedHandler = true;
						}
					}
				}
				return MySettings.defaultInstance;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int Realm
		{
			get
			{
				return Conversions.ToInteger(this["Realm"]);
			}
			set
			{
				this["Realm"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int Accounts
		{
			get
			{
				return Conversions.ToInteger(this["Accounts"]);
			}
			set
			{
				this["Accounts"] = value;
			}
		}

		[DefaultSettingValue(""), UserScopedSetting, DebuggerNonUserCode]
		public string Delay
		{
			get
			{
				return Conversions.ToString(this["Delay"]);
			}
			set
			{
				this["Delay"] = value;
			}
		}

		[DefaultSettingValue("0"), UserScopedSetting, DebuggerNonUserCode]
		public int Plugin
		{
			get
			{
				return Conversions.ToInteger(this["Plugin"]);
			}
			set
			{
				this["Plugin"] = value;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced), DebuggerNonUserCode]
		private static void AutoSaveSettings(object sender, EventArgs e)
		{
			if (MyProject.Application.SaveMySettingsOnExit)
			{
				MySettingsProperty.Settings.Save();
			}
		}
	}
}

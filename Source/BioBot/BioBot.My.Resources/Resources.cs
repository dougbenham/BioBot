using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace BioBot.My.Resources
{
	[StandardModule, HideModuleName, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal sealed class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("BioBot.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static string Accounts
		{
			get
			{
				return Resources.ResourceManager.GetString("Accounts", Resources.resourceCulture);
			}
		}

		internal static string Delay
		{
			get
			{
				return Resources.ResourceManager.GetString("Delay", Resources.resourceCulture);
			}
		}

		internal static string Plugin
		{
			get
			{
				return Resources.ResourceManager.GetString("Plugin", Resources.resourceCulture);
			}
		}

		internal static string Realm
		{
			get
			{
				return Resources.ResourceManager.GetString("Realm", Resources.resourceCulture);
			}
		}

		internal static string strhg
		{
			get
			{
				return Resources.ResourceManager.GetString("strhg", Resources.resourceCulture);
			}
		}
	}
}

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BioBot
{
	public class PluginServices
	{
		public struct AvailablePlugin
		{
			public string AssemblyPath;

			public string ClassName;
		}

		public static PluginServices.AvailablePlugin[] FindPlugins(string strPath, string strInterface)
		{
			ArrayList arrayList = new ArrayList();
			string[] fileSystemEntries = Directory.GetFileSystemEntries(strPath, "*.dll");
			int arg_1C_0 = 0;
			checked
			{
				int num = fileSystemEntries.Length - 1;
				for (int i = arg_1C_0; i <= num; i++)
				{
					try
					{
						Assembly objDLL = Assembly.LoadFrom(fileSystemEntries[i]);
						PluginServices.ExamineAssembly(objDLL, strInterface, arrayList);
					}
					catch (Exception expr_33)
					{
						ProjectData.SetProjectError(expr_33);
						ProjectData.ClearProjectError();
					}
				}
				PluginServices.AvailablePlugin[] array = new PluginServices.AvailablePlugin[arrayList.Count - 1 + 1];
				if (arrayList.Count != 0)
				{
					arrayList.CopyTo(array);
					return array;
				}
				return null;
			}
		}

		private static void ExamineAssembly(Assembly objDLL, string strInterface, ArrayList Plugins)
		{
			Type[] types = objDLL.GetTypes();
			checked
			{
				for (int i = 0; i < types.Length; i++)
				{
					Type type = types[i];
					if (type.IsPublic && (type.Attributes & TypeAttributes.Abstract) != TypeAttributes.Abstract)
					{
						Type @interface = type.GetInterface(strInterface, true);
						if (@interface != null)
						{
							Plugins.Add(new PluginServices.AvailablePlugin
							{
								AssemblyPath = objDLL.Location,
								ClassName = type.FullName
							});
						}
					}
				}
			}
		}

		public static object CreateInstance(PluginServices.AvailablePlugin Plugin)
		{
			object objectValue;
			try
			{
				Assembly assembly = Assembly.LoadFrom(Plugin.AssemblyPath);
				objectValue = RuntimeHelpers.GetObjectValue(assembly.CreateInstance(Plugin.ClassName));
			}
			catch (Exception expr_22)
			{
				ProjectData.SetProjectError(expr_22);
				Exception ex = expr_22;
				Interaction.MsgBox(ex.Message, MsgBoxStyle.OkOnly, null);
				object result = null;
				ProjectData.ClearProjectError();
				return result;
			}
			return objectValue;
		}
	}
}

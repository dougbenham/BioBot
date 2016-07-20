using BioBot.My;
using System;
using System.Runtime.CompilerServices;

namespace BioBot
{
	public class Counter
	{
		public static int Count = 0;

		public static string CreatzString = "";

		public static void AddNumbers()
		{
			checked
			{
				Counter.Count++;
			}
		}

		public static object ShowString(string Name, string Account)
		{
			string creatzString = string.Concat(new string[]
			{
				"Created name ",
				Name,
				" on account ",
				Account,
				"."
			});
			Counter.CreatzString = creatzString;
			return true;
		}

		public static object CurrentCount()
		{
			return Counter.Count;
		}

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static object PlaySound()
		{
			MyProject.Computer.Audio.Play(MyProject.Application.Info.DirectoryPath + "\\Settings\\Sound.wav");
			return true;
		}
	}
}

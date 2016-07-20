using BioBot.My;
using System;
using System.Runtime.CompilerServices;

namespace BioBot
{
	public class BattleNetClient
	{
		public string g_sProductID;

		public string g_sProductName;

		public string g_sHashPath;

		public string[] g_sHashes;

		public const byte g_bVerbyte = 14;

		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public BattleNetClient(bool Exp = false)
		{
			string str = MyProject.Application.Info.DirectoryPath + "\\Hashes\\";
			if (Exp)
			{
				this.g_sProductID = "D2DV";
				this.g_sProductName = "Diablo II: Lord of Destruction";
				this.g_sHashPath = str + "D2XP\\";
			}
			else
			{
				this.g_sProductID = "D2XP";
				this.g_sProductName = "Diablo II";
				this.g_sHashPath = str + "D2DV\\";
			}
			this.g_sHashes = new string[]
			{
				this.g_sHashPath + "Game.exe"
			};
		}

		public bool Equals(string a_sA)
		{
			string text = this.g_sProductID;
			char[] array = text.ToCharArray();
			char c = array[3];
			string arg_3E_0 = c.ToString();
			char c2 = array[2];
			string arg_3E_1 = c2.ToString();
			char c3 = array[1];
			string arg_3E_2 = c3.ToString();
			char c4 = array[0];
			string text2 = arg_3E_0 + arg_3E_1 + arg_3E_2 + c4;
			return text.Equals(a_sA) || text2.Equals(a_sA);
		}
	}
}

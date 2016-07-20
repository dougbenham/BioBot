using D2Data;
using System;
using System.Drawing;

namespace BioBot.Game
{
	public struct Hero_t
	{
		public string Name;

		public string Account;

		public int Level;

		public BattleNetCharacter Class;

		public Point Position;

		public uint UID;

		public int Mana;

		public int Life;

		public GameDifficulty Difficulty;

		public bool Expansion;

		public bool Ladder;

		public bool HardCore;
	}
}

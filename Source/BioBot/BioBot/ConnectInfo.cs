using Microsoft.VisualBasic;
using System;

namespace BioBot
{
	public class ConnectInfo
	{
		public class RealmList
		{
			public const string UsEast = "useast.battle.net";

			public const string UsWest = "uswest.battle.net";

			public const string Asia = "asia.battle.net";

			public const string Europe = "europe.battle.net";

			public static string NameFromAddress(string Address)
			{
				return Strings.Split(Address, ".", -1, CompareMethod.Binary)[0];
			}

			public static string AddressFromName(string Name)
			{
				return Name.ToLower() + ".battle.net";
			}
		}

		public string ClassicCdKey;

		public string ExpCdKey;

		public string CdKeyOwner;

		public string BnetUserName;

		public string BnetPassword;

		public string CharName;

		public bool Expansion;

		public string Realm;

		public int Port;

		public ConnectInfo()
		{
			this.Expansion = true;
			this.Port = 6112;
		}
	}
}

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace BioBot
{
	[StandardModule]
	public sealed class Compression
	{
		private static uint[] CharIndex = new uint[]
		{
			583u,
			566u,
			549u,
			532u,
			515u,
			498u,
			481u,
			464u,
			447u,
			430u,
			413u,
			396u,
			379u,
			362u,
			353u,
			344u,
			335u,
			326u,
			317u,
			308u,
			299u,
			290u,
			281u,
			272u,
			263u,
			254u,
			245u,
			236u,
			227u,
			218u,
			209u,
			200u,
			191u,
			182u,
			173u,
			168u,
			163u,
			158u,
			153u,
			148u,
			143u,
			138u,
			133u,
			128u,
			123u,
			118u,
			113u,
			108u,
			105u,
			102u,
			99u,
			96u,
			93u,
			90u,
			87u,
			84u,
			81u,
			78u,
			75u,
			72u,
			69u,
			66u,
			63u,
			63u,
			60u,
			60u,
			57u,
			57u,
			54u,
			54u,
			51u,
			51u,
			48u,
			48u,
			45u,
			45u,
			42u,
			42u,
			39u,
			39u,
			36u,
			36u,
			33u,
			33u,
			30u,
			30u,
			27u,
			27u,
			24u,
			24u,
			21u,
			21u,
			18u,
			18u,
			18u,
			18u,
			15u,
			15u,
			15u,
			15u,
			12u,
			12u,
			12u,
			12u,
			9u,
			9u,
			9u,
			9u,
			6u,
			6u,
			6u,
			6u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			3u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u,
			0u
		};

		private static byte[] CharTable = new byte[]
		{
			0,
			0,
			1,
			0,
			1,
			4,
			0,
			255,
			6,
			0,
			20,
			6,
			0,
			19,
			6,
			0,
			5,
			6,
			0,
			2,
			6,
			0,
			128,
			7,
			0,
			109,
			7,
			0,
			105,
			7,
			0,
			104,
			7,
			0,
			103,
			7,
			0,
			30,
			7,
			0,
			21,
			7,
			0,
			18,
			7,
			0,
			13,
			7,
			0,
			10,
			7,
			0,
			8,
			7,
			0,
			7,
			7,
			0,
			6,
			7,
			0,
			4,
			7,
			0,
			3,
			7,
			0,
			108,
			8,
			0,
			81,
			8,
			0,
			32,
			8,
			0,
			31,
			8,
			0,
			29,
			8,
			0,
			24,
			8,
			0,
			23,
			8,
			0,
			22,
			8,
			0,
			17,
			8,
			0,
			16,
			8,
			0,
			15,
			8,
			0,
			12,
			8,
			0,
			11,
			8,
			0,
			9,
			8,
			1,
			150,
			9,
			151,
			9,
			1,
			144,
			9,
			149,
			9,
			1,
			100,
			9,
			107,
			9,
			1,
			98,
			9,
			99,
			9,
			1,
			86,
			9,
			88,
			9,
			1,
			82,
			9,
			85,
			9,
			1,
			77,
			9,
			80,
			9,
			1,
			69,
			9,
			76,
			9,
			1,
			64,
			9,
			67,
			9,
			1,
			49,
			9,
			59,
			9,
			1,
			40,
			9,
			48,
			9,
			1,
			26,
			9,
			37,
			9,
			1,
			14,
			9,
			25,
			9,
			2,
			226,
			10,
			232,
			10,
			240,
			10,
			248,
			10,
			2,
			192,
			10,
			194,
			10,
			206,
			10,
			224,
			10,
			2,
			160,
			10,
			162,
			10,
			176,
			10,
			184,
			10,
			2,
			138,
			10,
			143,
			10,
			147,
			10,
			152,
			10,
			2,
			129,
			10,
			130,
			10,
			131,
			10,
			137,
			10,
			2,
			124,
			10,
			125,
			10,
			126,
			10,
			127,
			10,
			2,
			119,
			10,
			120,
			10,
			121,
			10,
			122,
			10,
			2,
			115,
			10,
			116,
			10,
			117,
			10,
			118,
			10,
			2,
			110,
			10,
			111,
			10,
			112,
			10,
			114,
			10,
			2,
			97,
			10,
			101,
			10,
			102,
			10,
			106,
			10,
			2,
			93,
			10,
			94,
			10,
			95,
			10,
			96,
			10,
			2,
			87,
			10,
			89,
			10,
			90,
			10,
			91,
			10,
			2,
			74,
			10,
			75,
			10,
			78,
			10,
			83,
			10,
			2,
			70,
			10,
			71,
			10,
			72,
			10,
			73,
			10,
			2,
			63,
			10,
			65,
			10,
			66,
			10,
			68,
			10,
			2,
			58,
			10,
			60,
			10,
			61,
			10,
			62,
			10,
			2,
			54,
			10,
			55,
			10,
			56,
			10,
			57,
			10,
			2,
			50,
			10,
			51,
			10,
			52,
			10,
			53,
			10,
			2,
			43,
			10,
			44,
			10,
			45,
			10,
			46,
			10,
			2,
			38,
			10,
			39,
			10,
			41,
			10,
			42,
			10,
			2,
			33,
			10,
			34,
			10,
			35,
			10,
			36,
			10,
			3,
			251,
			11,
			252,
			11,
			253,
			11,
			254,
			11,
			27,
			10,
			27,
			10,
			28,
			10,
			28,
			10,
			3,
			242,
			11,
			243,
			11,
			244,
			11,
			245,
			11,
			246,
			11,
			247,
			11,
			249,
			11,
			250,
			11,
			3,
			233,
			11,
			234,
			11,
			235,
			11,
			236,
			11,
			237,
			11,
			238,
			11,
			239,
			11,
			241,
			11,
			3,
			222,
			11,
			223,
			11,
			225,
			11,
			227,
			11,
			228,
			11,
			229,
			11,
			230,
			11,
			231,
			11,
			3,
			214,
			11,
			215,
			11,
			216,
			11,
			217,
			11,
			218,
			11,
			219,
			11,
			220,
			11,
			221,
			11,
			3,
			205,
			11,
			207,
			11,
			208,
			11,
			209,
			11,
			210,
			11,
			211,
			11,
			212,
			11,
			213,
			11,
			3,
			197,
			11,
			198,
			11,
			199,
			11,
			200,
			11,
			201,
			11,
			202,
			11,
			203,
			11,
			204,
			11,
			3,
			187,
			11,
			188,
			11,
			189,
			11,
			190,
			11,
			191,
			11,
			193,
			11,
			195,
			11,
			196,
			11,
			3,
			178,
			11,
			179,
			11,
			180,
			11,
			181,
			11,
			182,
			11,
			183,
			11,
			185,
			11,
			186,
			11,
			3,
			169,
			11,
			170,
			11,
			171,
			11,
			172,
			11,
			173,
			11,
			174,
			11,
			175,
			11,
			177,
			11,
			3,
			159,
			11,
			161,
			11,
			163,
			11,
			164,
			11,
			165,
			11,
			166,
			11,
			167,
			11,
			168,
			11,
			3,
			146,
			11,
			148,
			11,
			153,
			11,
			154,
			11,
			155,
			11,
			156,
			11,
			157,
			11,
			158,
			11,
			3,
			134,
			11,
			135,
			11,
			136,
			11,
			139,
			11,
			140,
			11,
			141,
			11,
			142,
			11,
			145,
			11,
			3,
			47,
			11,
			79,
			11,
			84,
			11,
			92,
			11,
			113,
			11,
			123,
			11,
			132,
			11,
			133,
			11
		};

		private static uint[] BitMasks = new uint[]
		{
			0u,
			1u,
			3u,
			7u,
			15u,
			31u,
			63u,
			127u,
			255u,
			511u,
			1023u,
			2047u,
			4095u,
			8191u,
			16383u,
			32767u
		};

		private static int[] PacketSizes = new int[]
		{
			1,
			8,
			1,
			12,
			1,
			1,
			1,
			6,
			6,
			11,
			6,
			6,
			9,
			13,
			12,
			16,
			16,
			8,
			26,
			14,
			18,
			11,
			-1,
			-1,
			15,
			2,
			2,
			3,
			5,
			3,
			4,
			6,
			10,
			12,
			12,
			13,
			90,
			90,
			-1,
			40,
			103,
			97,
			15,
			0,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			34,
			8,
			13,
			0,
			6,
			0,
			0,
			13,
			0,
			11,
			11,
			0,
			0,
			0,
			16,
			17,
			7,
			1,
			15,
			14,
			42,
			10,
			3,
			0,
			0,
			14,
			7,
			26,
			40,
			-1,
			5,
			6,
			38,
			5,
			7,
			2,
			7,
			21,
			0,
			7,
			7,
			16,
			21,
			12,
			12,
			16,
			16,
			10,
			1,
			1,
			1,
			1,
			1,
			32,
			10,
			13,
			6,
			2,
			21,
			6,
			13,
			8,
			6,
			18,
			5,
			10,
			4,
			20,
			29,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			6,
			6,
			11,
			7,
			10,
			33,
			13,
			26,
			6,
			8,
			-1,
			13,
			9,
			1,
			7,
			16,
			17,
			7,
			-1,
			-1,
			7,
			8,
			10,
			7,
			8,
			24,
			3,
			8,
			-1,
			7,
			-1,
			7,
			-1,
			7,
			-1,
			0,
			-1,
			0,
			1
		};

		private static uint[] CompressionTable = new uint[]
		{
			2147549184u,
			1879310336u,
			1543897088u,
			1040646144u,
			1074200576u,
			1611005952u,
			1107755008u,
			1141309440u,
			1174863872u,
			805830656u,
			1208418304u,
			822607872u,
			839385088u,
			1241972736u,
			587727104u,
			856162304u,
			872939520u,
			889716736u,
			1275527168u,
			1678114816u,
			1745223680u,
			1309081600u,
			906493952u,
			923271168u,
			940048384u,
			587727105u,
			604504320u,
			218563334u,
			218563335u,
			956825600u,
			1342636032u,
			973602816u,
			990380032u,
			235405824u,
			235405825u,
			235405826u,
			235405827u,
			604504321u,
			252183040u,
			252183041u,
			621281536u,
			252183042u,
			252183043u,
			268960256u,
			268960257u,
			268960258u,
			268960259u,
			525056u,
			621281537u,
			638058752u,
			285737472u,
			285737473u,
			285737474u,
			285737475u,
			302514688u,
			302514689u,
			302514690u,
			302514691u,
			319291904u,
			638058753u,
			319291905u,
			319291906u,
			319291907u,
			336069120u,
			654835968u,
			336069121u,
			336069122u,
			654835969u,
			336069123u,
			671613184u,
			352846336u,
			352846337u,
			352846338u,
			352846339u,
			369623552u,
			369623553u,
			671613185u,
			688390400u,
			369623554u,
			525057u,
			688390401u,
			1007157248u,
			705167616u,
			369623555u,
			525058u,
			705167617u,
			721944832u,
			386400768u,
			721944833u,
			386400769u,
			386400770u,
			386400771u,
			525059u,
			403177984u,
			403177985u,
			403177986u,
			403177987u,
			419955200u,
			738722048u,
			738722049u,
			755499264u,
			419955201u,
			419955202u,
			1376190464u,
			1409744896u,
			1443299328u,
			419955203u,
			755499265u,
			1023934464u,
			1476853760u,
			436732416u,
			436732417u,
			436732418u,
			525060u,
			436732419u,
			453509632u,
			453509633u,
			453509634u,
			453509635u,
			470286848u,
			470286849u,
			470286850u,
			470286851u,
			525061u,
			487064064u,
			487064065u,
			487064066u,
			487064067u,
			1510408192u,
			503841280u,
			503841281u,
			503841282u,
			525062u,
			525063u,
			17302272u,
			17302273u,
			17302274u,
			503841283u,
			520618496u,
			17302275u,
			17302276u,
			17302277u,
			17302278u,
			520618497u,
			772276480u,
			17302279u,
			34079488u,
			520618498u,
			34079489u,
			772276481u,
			789053696u,
			789053697u,
			520618499u,
			34079490u,
			34079491u,
			34079492u,
			34079493u,
			34079494u,
			34079495u,
			50856704u,
			537395712u,
			50856705u,
			537395713u,
			50856706u,
			50856707u,
			50856708u,
			50856709u,
			50856710u,
			50856711u,
			67633920u,
			67633921u,
			67633922u,
			67633923u,
			67633924u,
			67633925u,
			67633926u,
			537395714u,
			67633927u,
			84411136u,
			84411137u,
			84411138u,
			84411139u,
			84411140u,
			84411141u,
			537395715u,
			84411142u,
			84411143u,
			101188352u,
			101188353u,
			101188354u,
			101188355u,
			101188356u,
			554172928u,
			101188357u,
			554172929u,
			101188358u,
			101188359u,
			117965568u,
			117965569u,
			117965570u,
			117965571u,
			117965572u,
			117965573u,
			117965574u,
			117965575u,
			134742784u,
			554172930u,
			134742785u,
			134742786u,
			134742787u,
			134742788u,
			134742789u,
			134742790u,
			134742791u,
			151520000u,
			151520001u,
			151520002u,
			151520003u,
			151520004u,
			151520005u,
			151520006u,
			151520007u,
			168297216u,
			168297217u,
			554172931u,
			168297218u,
			570950144u,
			168297219u,
			168297220u,
			168297221u,
			168297222u,
			168297223u,
			570950145u,
			185074432u,
			185074433u,
			185074434u,
			185074435u,
			185074436u,
			185074437u,
			185074438u,
			570950146u,
			185074439u,
			201851648u,
			201851649u,
			201851650u,
			201851651u,
			201851652u,
			201851653u,
			570950147u,
			201851654u,
			201851655u,
			218628864u,
			218628865u,
			218628866u,
			218628867u,
			1812332544u
		};

		private static int lastPacket;

		public static int ComputeDataHeaderLength(byte[] Packet)
		{
			if (Packet[0] < 240)
			{
				return 1;
			}
			return 2;
		}

		public static int ComputeDataLength(byte[] Packet)
		{
			if (Packet[0] < 240)
			{
				return (int)Packet[0];
			}
			return checked(((int)(Packet[0] & 15) << 8) + (int)Packet[1]);
		}

		public static int DecompressDataContent(byte[] Packet, int HeaderLength, int PacketLength, ref byte[] DecompressedBytes, int DecompressionBufferLength)
		{
			uint num = 0u;
			checked
			{
				uint num2 = (uint)HeaderLength;
				uint num3 = 0u;
				uint num4 = (uint)(PacketLength - HeaderLength);
				uint num5 = (uint)DecompressionBufferLength;
				uint num6 = 32u;
				while (true)
				{
					uint num7;
					if (unchecked((ulong)num6) >= 8uL)
					{
						while (unchecked((ulong)num4 > 0uL & (ulong)num6 >= 8uL))
						{
							num6 = (uint)(unchecked((ulong)num6) - 8uL);
							num4 = (uint)(unchecked((ulong)num4) - 1uL);
							num7 = (uint)((uint)Packet[(int)num2] << (int)num6);
							num2 = (uint)(unchecked((ulong)num2) + 1uL);
							num |= num7;
						}
					}
					uint num8 = Compression.CharIndex[(int)(num >> 24)];
					num7 = (uint)Compression.CharTable[(int)num8];
					uint num9 = num >> (int)(24uL - unchecked((ulong)num7)) & Compression.BitMasks[(int)num7];
					uint num10 = (uint)Compression.CharTable[(int)(unchecked((ulong)num8) + 2uL * unchecked((ulong)num9) + 2uL)];
					num6 += num10;
					if (unchecked((ulong)num6) > 32uL)
					{
						break;
					}
					if (unchecked((ulong)num5) - 1uL == 0uL)
					{
						return -1;
					}
					num5 = (uint)(unchecked((ulong)num5) - 1uL);
					num7 = (uint)Compression.CharTable[(int)(unchecked((ulong)num8) + 2uL * unchecked((ulong)num9) + 1uL)];
					DecompressedBytes[(int)num3] = (byte)num7;
					num3 = (uint)(unchecked((ulong)num3) + 1uL);
					num <<= (int)(unchecked((ulong)num10) & 255uL);
				}
				return (int)(unchecked((long)DecompressionBufferLength) - (long)(unchecked((ulong)num5)));
			}
		}

		public static int DecompressPacket(byte[] Packet, ref byte[] DecompressedBytes, int DecompressionBufferLength)
		{
			int headerLength = Compression.ComputeDataHeaderLength(Packet);
			int num = Compression.ComputeDataLength(Packet);
			if (num == 0)
			{
				return 0;
			}
			return Compression.DecompressDataContent(Packet, headerLength, num, ref DecompressedBytes, DecompressionBufferLength);
		}

		public static int CompressPacket(byte[] Packet, int PacketLength, ref byte[] CompressedBytes, int CompressionBufferLength)
		{
			int num = Compression.CompressDataContent(Packet, PacketLength, ref CompressedBytes, 0, CompressionBufferLength);
			if (num == -1)
			{
				return -1;
			}
			int num2 = Compression.CreateDataHeader(num, ref CompressedBytes, CompressionBufferLength);
			if (num2 == -1)
			{
				return -1;
			}
			checked
			{
				int num3 = Compression.CompressDataContent(Packet, PacketLength, ref CompressedBytes, num2, CompressionBufferLength - num2);
				if (num3 != -1)
				{
					num3 += num2;
				}
				return num3 - 1;
			}
		}

		public static int CompressDataContent(byte[] Packet, int PacketLength, ref byte[] CompressedBytes, int StartPosition, int CompressionBufferLength)
		{
			int i = 0;
			int num = StartPosition;
			checked
			{
				uint num5;
				while (PacketLength > 0)
				{
					int num3;
					uint num2 = Compression.CompressionTable[(int)Packet[num3]];
					num3++;
					uint num4 = (uint)((unchecked((ulong)num2) & 65280uL) >> 8);
					num5 |= num2 >> 24 << 24 - i;
					i = (int)(unchecked((long)i) + (long)((unchecked((ulong)num2) & 16711680uL) >> 16));
					if (num4 > 0u)
					{
						num5 = (uint)(unchecked((ulong)num5) | (unchecked((ulong)num2) & 255uL) << (int)(8uL - unchecked((ulong)num4)) << 24 - i);
						i = (int)(unchecked((long)i) + (long)(unchecked((ulong)num4)));
					}
					while (i > 8)
					{
						CompressedBytes[num] = (byte)(num5 >> 24);
						num++;
						if (num >= CompressionBufferLength)
						{
							return -1;
						}
						i -= 8;
						num5 <<= 8;
					}
					PacketLength--;
				}
				while (i > 0)
				{
					CompressedBytes[num] = (byte)(num5 >> 24);
					num++;
					if (num >= CompressionBufferLength)
					{
						return -1;
					}
					num5 <<= 8;
					i -= 8;
				}
				return num;
			}
		}

		public static int CreateDataHeader(int contentLength, ref byte[] bytes, int maxHeaderLength)
		{
			checked
			{
				int num;
				if (contentLength > 238)
				{
					num = 2;
					if (maxHeaderLength < num)
					{
						return -1;
					}
					int num2 = contentLength + num;
					num2 |= 61440;
					bytes[0] = (byte)(num2 >> 8);
					bytes[1] = (byte)(num2 & 255);
				}
				else
				{
					num = 1;
					if (maxHeaderLength < num)
					{
						return -1;
					}
					int num2 = contentLength + num;
					bytes[0] = (byte)num2;
				}
				return num;
			}
		}

		public static List<byte[]> SplitPackets(byte[] buffer, int length)
		{
			int i = 0;
			List<byte[]> list = new List<byte[]>();
			checked
			{
				while (i < length)
				{
					int decompressedPacketSize = Compression.GetDecompressedPacketSize(buffer, i, length - i);
					if (decompressedPacketSize < 0)
					{
						string str = "";
						int arg_2B_0 = 0;
						int num = length - 1;
						for (int j = arg_2B_0; j <= num; j++)
						{
							str = str + buffer[j].ToString("x") + " ";
						}
						break;
					}
					if (decompressedPacketSize <= 0 | length - i < 0)
					{
						break;
					}
					byte[] array = new byte[decompressedPacketSize + 1];
					Array.Copy(buffer, i, array, 0, decompressedPacketSize);
					byte[] item = array;
					list.Add(item);
					i += decompressedPacketSize;
				}
				if (i == length)
				{
					return list;
				}
				return null;
			}
		}

		public static int GetDecompressedPacketSize(byte[] bytes, int offset, int size)
		{
			int num = (int)bytes[offset];
			int num2 = num;
			if (num2 == 38)
			{
				Compression.lastPacket = num;
				return Compression.GetChatPacketSize(bytes, offset, size);
			}
			checked
			{
				if (num2 == 91)
				{
					Compression.lastPacket = num;
					return (int)BitConverter.ToUInt16(bytes, offset + 1);
				}
				if (num2 == 148)
				{
					if (size >= 2)
					{
						Compression.lastPacket = num;
						return (int)(6 + bytes[offset + 1] * 3);
					}
				}
				else if (num2 == 168 || num2 == 170)
				{
					if (size >= 7)
					{
						Compression.lastPacket = num;
						return (int)bytes[offset + 6];
					}
				}
				else if (num2 == 172)
				{
					if (size >= 13)
					{
						Compression.lastPacket = num;
						return (int)bytes[offset + 12];
					}
				}
				else if (num2 == 174)
				{
					if (size >= 3)
					{
						Compression.lastPacket = num;
						return (int)(3 + BitConverter.ToUInt16(bytes, offset + 1));
					}
				}
				else if (num2 == 156 || num2 == 157)
				{
					if (size >= 3)
					{
						Compression.lastPacket = num;
						return (int)bytes[offset + 2];
					}
				}
				else if (num < Compression.PacketSizes.Length)
				{
					Compression.lastPacket = num;
					return Compression.PacketSizes[num];
				}
				return -1;
			}
		}

		public static int GetChatPacketSize(byte[] bytes, int index, int size)
		{
			UTF7Encoding uTF7Encoding = new UTF7Encoding();
			string text = "";
			string text2 = "";
			checked
			{
				int num = index + 10;
				while (bytes[num] != 0)
				{
					text += uTF7Encoding.GetString(bytes, num, 1);
					num++;
				}
				num++;
				while (bytes[num] != 0)
				{
					text2 += uTF7Encoding.GetString(bytes, num, 1);
					num++;
				}
				return 10 + text.Length + 1 + text2.Length + 1;
			}
		}
	}
}
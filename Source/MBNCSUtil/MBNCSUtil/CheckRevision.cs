using MBNCSUtil.Util;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Permissions;

namespace MBNCSUtil
{
	[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
	public static class CheckRevision
	{
		private static readonly uint[] hashcodes = new uint[]
		{
			3891579746u,
			4137766908u,
			2857698479u,
			2267008450u,
			297757208u,
			3312620262u,
			2032652926u,
			804030259u
		};

		public static int ExtractMPQNumber(string mpqName)
		{
			if (mpqName == null)
			{
				throw new ArgumentNullException("mpqName", Resources.crMpqNameNull);
			}
			if (mpqName.ToUpperInvariant().StartsWith("LOCKDOWN", StringComparison.Ordinal))
			{
				throw new NotSupportedException(Resources.crevExtrMpqNum_NoLockdown);
			}
			if (mpqName.Length < 7)
			{
				throw new ArgumentException(Resources.crMpqNameArgShort);
			}
			string text = mpqName.ToUpperInvariant();
			int result;
			if (text.StartsWith("VER", StringComparison.Ordinal))
			{
				result = int.Parse(mpqName[9].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
			}
			else
			{
				result = int.Parse(mpqName[7].ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
			}
			return result;
		}

		public static int DoCheckRevision(string valueString, string[] files, int mpqNumber)
		{
			if (valueString == null)
			{
				throw new ArgumentNullException("valueString", Resources.crValstringNull);
			}
			if (files == null)
			{
				throw new ArgumentNullException("files", Resources.crFileListNull);
			}
			if (files.Length != 3)
			{
				throw new ArgumentOutOfRangeException("files", files, Resources.crFileListInvalid);
			}
			uint[] array = new uint[4];
			int[] array2 = new int[4];
			int[] array3 = new int[4];
			char[] array4 = new char[4];
			int[] array5 = new int[4];
			string[] array6 = valueString.Split(new char[]
			{
				' '
			});
			int num = 0;
			for (int i = 0; i < array6.Length; i++)
			{
				string text = array6[i];
				if (text.IndexOf('=') != -1)
				{
					string[] array7 = text.Split(new char[]
					{
						'='
					});
					if (array7.Length != 2)
					{
						return 0;
					}
					int num2 = CheckRevision.getNum(array7[0][0]);
					string text2 = array7[1];
					if (char.IsDigit(text2[0]))
					{
						array[num2] = uint.Parse(text2, CultureInfo.InvariantCulture);
					}
					else
					{
						array2[num] = num2;
						array3[num] = CheckRevision.getNum(text2[0]);
						array4[num] = text2[1];
						array5[num] = CheckRevision.getNum(text2[2]);
						num++;
					}
				}
			}
			array[0] ^= CheckRevision.hashcodes[mpqNumber];
			for (int j = 0; j < files.Length; j++)
			{
				FileStream fileStream = new FileStream(files[j], FileMode.Open, FileAccess.Read, FileShare.Read);
				int num3 = (int)fileStream.Length;
				int num4;
				byte[] array8;
				if (num3 % 1024 == 0)
				{
					num4 = num3;
					array8 = new byte[num4];
					fileStream.Read(array8, 0, num4);
				}
				else
				{
					int num5 = 1024 - num3 % 1024;
					num4 = num3 + num5;
					array8 = new byte[num4];
					fileStream.Read(array8, 0, num3);
					byte b = 255;
					for (int k = num3; k < num4; k++)
					{
						byte[] arg_1EF_0 = array8;
						int arg_1EF_1 = k;
						byte expr_1E9 = b;
						b = expr_1E9 - 1;
						arg_1EF_0[arg_1EF_1] = expr_1E9;
					}
				}
				MemoryStream input = new MemoryStream(array8, 0, num4, false);
				BinaryReader binaryReader = new BinaryReader(input);
				for (int l = 0; l < num4; l += 4)
				{
					array[3] = binaryReader.ReadUInt32();
					for (int m = 0; m < num; m++)
					{
						char c = array4[m];
						switch (c)
						{
						case '*':
							array[array2[m]] = array[array3[m]] * array[array5[m]];
							break;
						case '+':
							array[array2[m]] = array[array3[m]] + array[array5[m]];
							break;
						case ',':
						case '.':
							break;
						case '-':
							array[array2[m]] = array[array3[m]] - array[array5[m]];
							break;
						case '/':
							array[array2[m]] = array[array3[m]] / array[array5[m]];
							break;
						default:
							if (c == '^')
							{
								array[array2[m]] = (array[array3[m]] ^ array[array5[m]]);
							}
							break;
						}
					}
				}
			}
			return (int)array[2];
		}

		public static byte[] DoLockdownCheckRevision(byte[] valueString, string[] gameFiles, string lockdownFile, string imageFile, ref int version, ref int checksum)
		{
			byte[] result;
			LockdownCrev.CheckRevision(gameFiles[0], gameFiles[1], gameFiles[2], valueString, ref version, ref checksum, out result, lockdownFile, imageFile);
			return result;
		}

		private static int getNum(char c)
		{
			c = char.ToUpper(c, CultureInfo.InvariantCulture);
			if (c == 'S')
			{
				return 3;
			}
			return (int)(c - 'A');
		}

		public static int GetExeInfo(string fileName, out string exeInfoString)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException(Resources.param_fileName, Resources.crExeFileNull);
			}
			string text = fileName.Substring(fileName.LastIndexOf('\\') + 1);
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			uint num = (uint)fileStream.Length;
			DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(fileName);
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileName);
			int result = versionInfo.FileMajorPart << 24 | versionInfo.FileMinorPart << 16 | versionInfo.FileBuildPart << 8 | versionInfo.FilePrivatePart;
			exeInfoString = string.Format(CultureInfo.InvariantCulture, Resources.exeInfoFmt, new object[]
			{
				text,
				lastWriteTimeUtc.Month,
				lastWriteTimeUtc.Day,
				lastWriteTimeUtc.Year % 100,
				lastWriteTimeUtc.Hour,
				lastWriteTimeUtc.Minute,
				lastWriteTimeUtc.Second,
				num
			});
			return result;
		}

		[Obsolete("Use of the MBNCSUtil web service for retrieving version bytes is no longer possible.", true)]
		public static byte GetVersionByte(string productID)
		{
			throw new NotSupportedException();
		}
	}
}

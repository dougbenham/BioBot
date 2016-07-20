using System;
using System.Diagnostics;
using System.Text;

namespace MBNCSUtil
{
	public static class DataFormatter
	{
		public static string Format(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException(Resources.param_data, Resources.dataNull);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("0000   ");
			if (data.Length == 0)
			{
				stringBuilder.Append("(empty)");
				return stringBuilder.ToString();
			}
			StringBuilder stringBuilder2 = new StringBuilder(16, 16);
			for (int i = 0; i < data.Length; i++)
			{
				char c = (char)data[i];
				if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ')
				{
					stringBuilder2.Append(c);
				}
				else
				{
					stringBuilder2.Append('.');
				}
				stringBuilder.AppendFormat("{0:x2} ", data[i]);
				if ((i + 1) % 8 == 0)
				{
					stringBuilder.Append(" ");
				}
				if ((i + 1) % 16 == 0 || i + 1 == data.Length)
				{
					if (i + 1 == data.Length && (i + 1) % 16 != 0)
					{
						int num = i % 16 * 3;
						if (i % 16 > 8)
						{
							num++;
						}
						for (int j = 0; j < 47 - num; j++)
						{
							stringBuilder.Append(' ');
						}
					}
					stringBuilder.AppendFormat("  {0}", stringBuilder2.ToString());
					stringBuilder2 = new StringBuilder(16, 16);
					stringBuilder.Append(Environment.NewLine);
					if (data.Length > i + 1)
					{
						stringBuilder.AppendFormat("{0:x4}   ", i + 1);
					}
				}
			}
			return stringBuilder.ToString();
		}

		public static string Format(byte[] data, int startIndex, int length)
		{
			if (data == null)
			{
				throw new ArgumentNullException(Resources.param_data, Resources.dataNull);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("0000   ");
			if (data.Length == 0)
			{
				stringBuilder.Append("(empty)");
				return stringBuilder.ToString();
			}
			StringBuilder stringBuilder2 = new StringBuilder(16, 16);
			int num = startIndex;
			while (num < data.Length && num < startIndex + length)
			{
				char c = (char)data[num];
				if (char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ')
				{
					stringBuilder2.Append(c);
				}
				else
				{
					stringBuilder2.Append('.');
				}
				stringBuilder.AppendFormat("{0:x2} ", data[num]);
				if ((num + 1) % 8 == 0)
				{
					stringBuilder.Append(" ");
				}
				if ((num + 1) % 16 == 0 || num + 1 == data.Length)
				{
					if (num + 1 == data.Length && (num + 1) % 16 != 0)
					{
						int num2 = num % 16 * 3;
						if (num % 16 > 8)
						{
							num2++;
						}
						for (int i = 0; i < 47 - num2; i++)
						{
							stringBuilder.Append(' ');
						}
					}
					stringBuilder.AppendFormat("  {0}", stringBuilder2.ToString());
					stringBuilder2 = new StringBuilder(16, 16);
					stringBuilder.Append(Environment.NewLine);
					if (data.Length > num + 1)
					{
						stringBuilder.AppendFormat("{0:x4}   ", num + 1);
					}
				}
				num++;
			}
			return stringBuilder.ToString();
		}

		public static void WriteToConsole(byte[] data)
		{
			Console.WriteLine(DataFormatter.Format(data));
		}

		public static void WriteToTrace(byte[] data)
		{
			Trace.WriteLine(DataFormatter.Format(data));
		}

		public static void WriteToTrace(byte[] data, string category)
		{
			Trace.WriteLine(DataFormatter.Format(data), category);
		}
	}
}

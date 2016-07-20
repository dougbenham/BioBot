using System;

namespace MBNCSUtil.Data
{
	internal static class DXTCFormat
	{
		public static int[] DecodeDXT1(int w, int h, byte[] b)
		{
			int[] array = new int[w * h * 3];
			int num = 0;
			int[][] array2 = new int[4][];
			for (int i = 0; i < h / 4; i++)
			{
				for (int j = 0; j < w / 4; j++)
				{
					int num2 = (int)b[num] + (int)b[num + 1] * 256;
					int num3 = (int)b[num + 2] + (int)b[num + 3] * 256;
					array2[0] = DXTCFormat.decodeColor(num2);
					array2[1] = DXTCFormat.decodeColor(num3);
					array2[2] = new int[3];
					array2[3] = new int[3];
					if (num2 > num3)
					{
						for (int k = 0; k < 3; k++)
						{
							array2[2][k] = (2 * array2[0][k] + array2[1][k] + 1) / 3;
						}
						for (int k = 0; k < 3; k++)
						{
							array2[3][k] = (array2[0][k] + 2 * array2[1][k] + 1) / 3;
						}
					}
					else
					{
						for (int k = 0; k < 3; k++)
						{
							array2[2][k] = (array2[0][k] + array2[1][k]) / 2;
						}
						array2[3] = DXTCFormat.decodeColor(0);
					}
					for (int l = 0; l < 4; l++)
					{
						for (int k = 0; k < 4; k++)
						{
							int num4 = DXTCFormat.dxtcPixel(b, num + 4, k, l);
							array[((i * 4 + l) * w + j * 4 + k) * 3] = array2[num4][0];
							array[((i * 4 + l) * w + j * 4 + k) * 3 + 1] = array2[num4][1];
							array[((i * 4 + l) * w + j * 4 + k) * 3 + 2] = array2[num4][2];
						}
					}
					num += 8;
				}
			}
			return array;
		}

		public static int[] DecodeDXT2(int w, int h, byte[] b)
		{
			int[] array = new int[w * h * 4];
			int num = 0;
			int[][] array2 = new int[4][];
			int[] array3 = new int[4];
			for (int i = 0; i < h / 4; i++)
			{
				for (int j = 0; j < w / 4; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						array3[k] = (int)b[num + 2 * k] + (int)b[num + 2 * k + 1] * 256;
					}
					int colorCode = (int)b[num + 8] + (int)b[num + 9] * 256;
					int colorCode2 = (int)b[num + 10] + (int)b[num + 11] * 256;
					array2[0] = DXTCFormat.decodeColor(colorCode);
					array2[1] = DXTCFormat.decodeColor(colorCode2);
					array2[2] = new int[3];
					array2[3] = new int[3];
					for (int k = 0; k < 3; k++)
					{
						array2[2][k] = (2 * array2[0][k] + array2[1][k] + 1) / 3;
					}
					for (int k = 0; k < 3; k++)
					{
						array2[3][k] = (array2[0][k] + 2 * array2[1][k] + 1) / 3;
					}
					for (int l = 0; l < 4; l++)
					{
						for (int k = 0; k < 4; k++)
						{
							int num2 = DXTCFormat.dxtcPixel(b, num + 12, k, l);
							array[((i * 4 + l) * w + j * 4 + k) * 4] = array2[num2][0];
							array[((i * 4 + l) * w + j * 4 + k) * 4 + 1] = array2[num2][1];
							array[((i * 4 + l) * w + j * 4 + k) * 4 + 2] = array2[num2][2];
							int num3 = DXTCFormat.dxtcAlpha(array3, k, l);
							array[((i * 4 + l) * w + j * 4 + k) * 4 + 3] = ((num3 == 15) ? 255 : num3);
						}
					}
					num += 16;
				}
			}
			return array;
		}

		private static int dxtcPixel(byte[] b, int s, int x, int y)
		{
			x *= 2;
			int num = (int)b[s + y];
			return (num & 3 << x) >> x;
		}

		private static int dxtcAlpha(int[] a, int x, int y)
		{
			x *= 4;
			int num = a[y];
			return (num & 15 << x) >> x;
		}

		private static int[] decodeColor(int colorCode)
		{
			int num = colorCode & 31;
			int num2 = (colorCode & 2016) / 32;
			int num3 = (colorCode & 63488) / 2048;
			return new int[]
			{
				num3 * 8,
				num2 * 4,
				num * 8
			};
		}
	}
}

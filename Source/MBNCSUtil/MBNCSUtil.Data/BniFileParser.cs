using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace MBNCSUtil.Data
{
	public sealed class BniFileParser : IDisposable
	{
		private struct IconHeader
		{
			public uint flagValue;

			public uint width;

			public uint height;

			public uint[] software;

			public IconHeader(BinaryReader br)
			{
				this.flagValue = br.ReadUInt32();
				this.width = br.ReadUInt32();
				this.height = br.ReadUInt32();
				List<uint> list = new List<uint>();
				uint num;
				do
				{
					num = br.ReadUInt32();
					if (num != 0u)
					{
						list.Add(num);
					}
				}
				while (num != 0u);
				this.software = list.ToArray();
			}
		}

		private enum StartDescriptor : byte
		{
			BottomLeft,
			BottomRight = 16,
			TopLeft = 32,
			TopRight = 48
		}

		private Image m_fullImage;

		private List<BniIcon> m_icons = new List<BniIcon>();

		public Image FullImage
		{
			get
			{
				return this.m_fullImage;
			}
		}

		public BniIcon[] AllIcons
		{
			get
			{
				return this.m_icons.ToArray();
			}
		}

		public BniFileParser(string filePath)
		{
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException("The specified file does not exist.", filePath);
			}
			using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				this.Parse(fileStream);
			}
		}

		public BniFileParser(Stream bniFileStream)
		{
			if (object.ReferenceEquals(null, bniFileStream))
			{
				throw new ArgumentNullException("bniFileStream");
			}
			this.Parse(bniFileStream);
		}

		private unsafe void Parse(Stream str)
		{
			using (BinaryReader binaryReader = new BinaryReader(str))
			{
				binaryReader.ReadUInt32();
				ushort num = binaryReader.ReadUInt16();
				if (num != 1)
				{
					throw new InvalidDataException("Only version 1 of BNI files is supported.");
				}
				binaryReader.ReadUInt16();
				uint num2 = binaryReader.ReadUInt32();
				binaryReader.ReadUInt32();
				List<BniFileParser.IconHeader> list = new List<BniFileParser.IconHeader>((int)num2);
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					list.Add(new BniFileParser.IconHeader(binaryReader));
					num3++;
				}
				byte count = binaryReader.ReadByte();
				binaryReader.ReadByte();
				if (binaryReader.ReadByte() != 10)
				{
					throw new InvalidDataException("Only run-length true-color TGA icons are supported.");
				}
				binaryReader.ReadBytes(5);
				binaryReader.ReadUInt16();
				binaryReader.ReadUInt16();
				ushort num4 = binaryReader.ReadUInt16();
				ushort num5 = binaryReader.ReadUInt16();
				byte b = binaryReader.ReadByte();
				if (b != 24)
				{
					throw new InvalidDataException("Only 24-bit TGA is supported.");
				}
				BniFileParser.StartDescriptor startDescriptor = (BniFileParser.StartDescriptor)binaryReader.ReadByte();
				byte[] bytes = binaryReader.ReadBytes((int)count);
				Trace.WriteLine(Encoding.ASCII.GetString(bytes), "BNI header: information");
				int num6 = (int)(num4 * num5);
				using (Bitmap bitmap = new Bitmap((int)num4, (int)num5))
				{
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(Point.Empty, new Size((int)num4, (int)num5)), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
					int* ptr = (int*)bitmapData.Scan0.ToPointer();
					byte b3;
					for (int i = 0; i < num6; i += (int)b3)
					{
						byte b2 = binaryReader.ReadByte();
						b3 = (b2 & 127) + 1;
						if ((b2 & 128) == 128)
						{
							byte blue = binaryReader.ReadByte();
							byte green = binaryReader.ReadByte();
							byte red = binaryReader.ReadByte();
							Color color = Color.FromArgb(255, (int)red, (int)green, (int)blue);
							for (int j = 0; j < (int)b3; j++)
							{
								*ptr = color.ToArgb();
								ptr++;
							}
						}
						else
						{
							for (int k = 0; k < (int)b3; k++)
							{
								byte blue2 = binaryReader.ReadByte();
								byte green2 = binaryReader.ReadByte();
								byte red2 = binaryReader.ReadByte();
								*ptr = Color.FromArgb(255, (int)red2, (int)green2, (int)blue2).ToArgb();
								ptr++;
							}
						}
					}
					ptr = null;
					bitmap.UnlockBits(bitmapData);
					this.m_fullImage = new Bitmap((int)num4, (int)num5);
					if (startDescriptor == BniFileParser.StartDescriptor.TopRight)
					{
						bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
					}
					else if (startDescriptor == BniFileParser.StartDescriptor.BottomLeft)
					{
						bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
					}
					else if (startDescriptor == BniFileParser.StartDescriptor.BottomRight)
					{
						bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
					}
					using (Graphics graphics = Graphics.FromImage(this.m_fullImage))
					{
						graphics.DrawImage(bitmap, Point.Empty);
					}
					int num7 = 0;
					int num8 = 0;
					while ((long)num8 < (long)((ulong)num2))
					{
						BniFileParser.IconHeader iconHeader = list[num8];
						Bitmap bitmap2 = new Bitmap(bitmap, (int)num4, (int)iconHeader.height);
						using (Graphics graphics2 = Graphics.FromImage(bitmap2))
						{
							Size size = new Size((int)num4, (int)iconHeader.height);
							graphics2.DrawImage(bitmap, new Rectangle(Point.Empty, size), new Rectangle(new Point(0, num7), size), GraphicsUnit.Pixel);
						}
						BniIcon item = new BniIcon(bitmap2, (int)iconHeader.flagValue, iconHeader.software);
						this.m_icons.Add(item);
						num7 += (int)iconHeader.height;
						num8++;
					}
				}
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && this.m_fullImage != null)
			{
				this.m_fullImage.Dispose();
				this.m_fullImage = null;
			}
		}
	}
}

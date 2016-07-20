using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace MBNCSUtil.Data
{
	internal class Blp2Parser : ImageParser
	{
		private class Blp2MipMap
		{
			private Size _size;

			private int _index;

			private byte[] _data;

			public Size Size
			{
				get
				{
					return this._size;
				}
			}

			public int Index
			{
				get
				{
					return this._index;
				}
			}

			public byte[] Data
			{
				get
				{
					byte[] array = new byte[this._data.Length];
					Buffer.BlockCopy(this._data, 0, array, 0, array.Length);
					return array;
				}
			}

			public Blp2MipMap(Size size, int index, byte[] data)
			{
				this._size = size;
				this._index = index;
				this._data = data;
			}
		}

		private const int MAX_MIPMAP_COUNT = 16;

		private const int BLP2_PALETTE_SIZE = 256;

		private const int BLP1_JPG_HEADER_OFFSET = 160;

		private const int BLP1_JPG_HEADER_SIZE = 624;

		private Size m_mainSize;

		private Blp2CompressionType m_cmpType;

		private Color[] m_palette;

		private int m_alphaBits;

		private List<Blp2Parser.Blp2MipMap> m_mipmaps;

		private byte[] m_jpgHeader;

		public override int NumberOfMipmaps
		{
			get
			{
				return this.m_mipmaps.Count;
			}
		}

		public Blp2Parser(Stream stream)
		{
			if (!stream.CanSeek)
			{
				throw new ArgumentException("The specified stream cannot seek.", "stream");
			}
			this.m_mipmaps = new List<Blp2Parser.Blp2MipMap>();
			this.ParseInternal(stream);
		}

		private void ParseInternal(Stream stream)
		{
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				binaryReader.ReadInt32();
				this.m_cmpType = (Blp2CompressionType)binaryReader.ReadByte();
				this.m_alphaBits = (int)binaryReader.ReadByte();
				int num = (int)binaryReader.ReadByte();
				binaryReader.ReadByte();
				int width = binaryReader.ReadInt32();
				int height = binaryReader.ReadInt32();
				Size size = new Size(width, height);
				this.m_mainSize = size;
				int[] array = new int[16];
				for (int i = 0; i < 16; i++)
				{
					array[i] = binaryReader.ReadInt32();
				}
				int[] array2 = new int[16];
				for (int j = 0; j < 16; j++)
				{
					array2[j] = binaryReader.ReadInt32();
				}
				if (this.m_cmpType == Blp2CompressionType.Palette)
				{
					if (num != 8)
					{
						throw new ArgumentException("Palettized data can only have 8 bits per pixel.", "stream");
					}
				}
				else if (this.m_cmpType == Blp2CompressionType.DirectX)
				{
					if (this.m_alphaBits != 0 && this.m_alphaBits != 8 && this.m_alphaBits != 1)
					{
						throw new ArgumentException("DXT2/DXT4-compressed images are not yet supported.");
					}
				}
				else if (this.m_cmpType != Blp2CompressionType.Jpeg)
				{
					throw new ArgumentException("Unknown file format.");
				}
				if (this.m_cmpType == Blp2CompressionType.Palette)
				{
					this.ParsePalette(stream, binaryReader);
				}
				else if (this.m_cmpType == Blp2CompressionType.Jpeg)
				{
					this.ParseJpegHeader(stream, binaryReader);
				}
				for (int k = 0; k < 16; k++)
				{
					int num2 = array[k];
					int num3 = array2[k];
					if (num2 == 0 || num3 == 0)
					{
						break;
					}
					Size size2 = Blp1Parser.GetSize(size, k);
					byte[] array3 = new byte[num3];
					stream.Seek((long)num2, SeekOrigin.Begin);
					binaryReader.Read(array3, 0, num3);
					Blp2Parser.Blp2MipMap item = new Blp2Parser.Blp2MipMap(size2, k, array3);
					this.m_mipmaps.Add(item);
				}
			}
		}

		private void ParsePalette(Stream stream, BinaryReader br)
		{
			stream.Seek(148L, SeekOrigin.Begin);
			this.m_palette = new Color[256];
			if (this.m_alphaBits == 0)
			{
				for (int i = 0; i < 256; i++)
				{
					this.m_palette[i] = Color.FromArgb(255, Color.FromArgb(br.ReadInt32()));
				}
				return;
			}
			if (this.m_alphaBits == 8)
			{
				for (int j = 0; j < 256; j++)
				{
					int argb = br.ReadInt32();
					this.m_palette[j] = Color.FromArgb(255, Color.FromArgb(argb));
				}
				return;
			}
			throw new InvalidDataException("Unexpected number of bits per alpha channel; only 0 or 8 are allowed for palettized images.");
		}

		private void ParseJpegHeader(Stream stream, BinaryReader br)
		{
			long position = stream.Position;
			stream.Seek(160L, SeekOrigin.Begin);
			byte[] array = new byte[624];
			br.Read(array, 0, 624);
			stream.Seek(position, SeekOrigin.Begin);
			this.m_jpgHeader = array;
		}

		private Image FromPalette(Blp2Parser.Blp2MipMap mipmap)
		{
			Bitmap bitmap = new Bitmap(mipmap.Size.Width, mipmap.Size.Height, PixelFormat.Format32bppArgb);
			byte[] data = mipmap.Data;
			int[] array = new int[mipmap.Size.Width * mipmap.Size.Height];
			for (int i = 0; i < array.Length; i++)
			{
				Color color = this.m_palette[(int)data[i]];
				array[i] = color.ToArgb();
			}
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(Point.Empty, mipmap.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			Marshal.Copy(array, 0, bitmapData.Scan0, array.Length);
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		private Image FromJpeg(Blp2Parser.Blp2MipMap mipmap)
		{
			byte[] array = new byte[624 + mipmap.Data.Length];
			Buffer.BlockCopy(this.m_jpgHeader, 0, array, 0, this.m_jpgHeader.Length);
			Buffer.BlockCopy(mipmap.Data, 0, array, 624, mipmap.Data.Length);
			Image result;
			using (MemoryStream memoryStream = new MemoryStream(array, false))
			{
				result = Image.FromStream(memoryStream, false, false);
			}
			return result;
		}

		private Image FromDxt5(Blp2Parser.Blp2MipMap mipmap)
		{
			int[] bmpData = DXTCFormat.DecodeDXT2(mipmap.Size.Width, mipmap.Size.Height, mipmap.Data);
			return this.FromBinary(mipmap.Size.Width, mipmap.Size.Height, bmpData);
		}

		private Image FromDxt1(Blp2Parser.Blp2MipMap mipmap)
		{
			int[] bmpData = DXTCFormat.DecodeDXT1(mipmap.Size.Width, mipmap.Size.Height, mipmap.Data);
			return this.FromBinary(mipmap.Size.Width, mipmap.Size.Height, bmpData);
		}

		private unsafe Image FromBinary(int width, int height, int[] bmpData)
		{
			Bitmap bitmap = new Bitmap(width, height);
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(Point.Empty, new Size(width, height)), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			uint* ptr = (uint*)((void*)bitmapData.Scan0);
			if (this.m_alphaBits > 0)
			{
				for (int i = 0; i < width * height; i++)
				{
					uint num = (uint)((long)((bmpData[i * 4] << 16 & 16711680) | (bmpData[i * 4 + 1] << 8 & 65280) | (bmpData[i * 4 + 2] & 255)) | ((long)((long)(bmpData[i * 4 + 3] & 255) << 24) & (long)((ulong)-1)));
					*(ptr++) = num;
				}
			}
			else
			{
				for (int j = 0; j < width * height; j++)
				{
					uint num2 = (uint)((long)((bmpData[j * 3] << 16 & 16711680) | (bmpData[j * 3 + 1] << 8 & 65280) | (bmpData[j * 3 + 2] & 255)) | (long)((ulong)-16777216)) & 4294967295u;
					*(ptr++) = num2;
				}
			}
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		public override Size GetSizeOfMipmap(int mipmapIndex)
		{
			return Blp1Parser.GetSize(this.m_mainSize, mipmapIndex);
		}

		public override Image GetMipmapImage(int mipmapIndex)
		{
			if (mipmapIndex < 0 || mipmapIndex >= this.m_mipmaps.Count)
			{
				throw new ArgumentOutOfRangeException("mipmapIndex", mipmapIndex, "Index must be non-negative and less than the number of mipmaps in this BLP file.");
			}
			Blp2Parser.Blp2MipMap mipmap = this.m_mipmaps[mipmapIndex];
			Image result = null;
			if (this.m_cmpType == Blp2CompressionType.Palette)
			{
				result = this.FromPalette(mipmap);
			}
			else if (this.m_cmpType == Blp2CompressionType.DirectX)
			{
				if (this.m_alphaBits == 0 || this.m_alphaBits == 1)
				{
					result = this.FromDxt1(mipmap);
				}
				else if (this.m_alphaBits == 8)
				{
					result = this.FromDxt5(mipmap);
				}
			}
			else
			{
				result = this.FromJpeg(mipmap);
			}
			return result;
		}
	}
}

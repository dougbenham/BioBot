using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace MBNCSUtil.Data
{
	internal class Blp1Parser : ImageParser
	{
		private class Blp1MipMap
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

			public Blp1MipMap(Size size, int index, byte[] data)
			{
				this._size = size;
				this._index = index;
				this._data = data;
			}
		}

		private const int MAX_BLP1_MIPMAP_COUNT = 16;

		private const int BLP1_PALETTE_SIZE = 256;

		private const int BLP1_JPG_HEADER_OFFSET = 160;

		private const int BLP1_JPG_HEADER_SIZE = 624;

		private List<Blp1Parser.Blp1MipMap> _mipmaps;

		private Blp1ImageType _compressionType;

		private Color[] _palette;

		private byte[] _jpgHeader;

		private Size _mainSize;

		public override int NumberOfMipmaps
		{
			get
			{
				return this._mipmaps.Count;
			}
		}

		public Blp1Parser(Stream stream)
		{
			if (!stream.CanSeek)
			{
				throw new ArgumentException("Cannot seek on the specified stream.", "stream");
			}
			this._mipmaps = new List<Blp1Parser.Blp1MipMap>();
			this.ParseInternal(stream);
		}

		private void ParseInternal(Stream s)
		{
			using (BinaryReader binaryReader = new BinaryReader(s))
			{
				Blp1ImageType blp1ImageType = (Blp1ImageType)binaryReader.ReadInt32();
				int num = binaryReader.ReadInt32();
				if (!Enum.IsDefined(typeof(Blp1ImageType), blp1ImageType))
				{
					throw new InvalidDataException("The file specified an unknown type of BLP1 compression.");
				}
				if (num < 0)
				{
					throw new InvalidDataException("The file specified a negative number of mipmaps, which is invalid.");
				}
				this._compressionType = blp1ImageType;
				int width = binaryReader.ReadInt32();
				int height = binaryReader.ReadInt32();
				Size size = new Size(width, height);
				this._mainSize = size;
				binaryReader.ReadInt64();
				int[] array = new int[16];
				int[] array2 = new int[16];
				for (int i = 0; i < 16; i++)
				{
					array[i] = binaryReader.ReadInt32();
				}
				for (int j = 0; j < 16; j++)
				{
					array2[j] = binaryReader.ReadInt32();
				}
				if (blp1ImageType == Blp1ImageType.Palette)
				{
					this.ParsePalette(binaryReader);
				}
				else
				{
					this.ParseJpgHeader(s, binaryReader);
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
					s.Seek((long)num2, SeekOrigin.Begin);
					binaryReader.Read(array3, 0, num3);
					Blp1Parser.Blp1MipMap item = new Blp1Parser.Blp1MipMap(size2, k, array3);
					this._mipmaps.Add(item);
				}
			}
		}

		private void ParseJpgHeader(Stream s, BinaryReader br)
		{
			long position = s.Position;
			s.Seek(160L, SeekOrigin.Begin);
			byte[] array = new byte[624];
			br.Read(array, 0, 624);
			s.Seek(position, SeekOrigin.Begin);
			this._jpgHeader = array;
		}

		internal static Size GetSize(Size originalSize, int mipmapIndex)
		{
			Size result = originalSize;
			for (int i = 0; i < mipmapIndex; i++)
			{
				result = new Size((int)Math.Ceiling((double)result.Width / 2.0), (int)Math.Ceiling((double)result.Height / 2.0));
			}
			return result;
		}

		private void ParsePalette(BinaryReader br)
		{
			this._palette = new Color[256];
			for (int i = 0; i < 256; i++)
			{
				Color color = Color.FromArgb(br.ReadInt32());
				color = Color.FromArgb(255, color);
				this._palette[i] = color;
			}
		}

		private Image FromPalette(Blp1Parser.Blp1MipMap mipmap)
		{
			Bitmap bitmap = new Bitmap(mipmap.Size.Width, mipmap.Size.Height, PixelFormat.Format32bppArgb);
			byte[] data = mipmap.Data;
			int[] array = new int[mipmap.Size.Width * mipmap.Size.Height];
			for (int i = 0; i < array.Length; i++)
			{
				Color color = this._palette[(int)data[i]];
				array[i] = color.ToArgb();
			}
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(Point.Empty, mipmap.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			Marshal.Copy(array, 0, bitmapData.Scan0, array.Length);
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		private Image FromJpeg(Blp1Parser.Blp1MipMap mipmap)
		{
			byte[] array = new byte[624 + mipmap.Data.Length];
			Buffer.BlockCopy(this._jpgHeader, 0, array, 0, this._jpgHeader.Length);
			Buffer.BlockCopy(mipmap.Data, 0, array, 624, mipmap.Data.Length);
			Image result;
			using (MemoryStream memoryStream = new MemoryStream(array, false))
			{
				result = Image.FromStream(memoryStream, false, false);
			}
			return result;
		}

		public override Size GetSizeOfMipmap(int mipmapIndex)
		{
			return Blp1Parser.GetSize(this._mainSize, mipmapIndex);
		}

		public override Image GetMipmapImage(int mipmapIndex)
		{
			if (mipmapIndex < 0 || mipmapIndex >= this._mipmaps.Count)
			{
				throw new ArgumentOutOfRangeException("mipmapIndex", mipmapIndex, "Index must be non-negative and less than the number of mipmaps in this BLP file.");
			}
			Blp1Parser.Blp1MipMap mipmap = this._mipmaps[mipmapIndex];
			Image result;
			if (this._compressionType == Blp1ImageType.Palette)
			{
				result = this.FromPalette(mipmap);
			}
			else
			{
				result = this.FromJpeg(mipmap);
			}
			return result;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this._jpgHeader = null;
				if (this._mipmaps != null)
				{
					this._mipmaps.Clear();
					this._mipmaps = null;
				}
				this._palette = null;
			}
		}
	}
}

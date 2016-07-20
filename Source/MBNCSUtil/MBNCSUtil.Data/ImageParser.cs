using System;
using System.Drawing;
using System.IO;

namespace MBNCSUtil.Data
{
	public abstract class ImageParser : IDisposable
	{
		private const int BLP1 = 827345986;

		private const int BLP2 = 844123202;

		public abstract int NumberOfMipmaps
		{
			get;
		}

		public abstract Size GetSizeOfMipmap(int mipmapIndex);

		public abstract Image GetMipmapImage(int mipmapIndex);

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		public static ImageParser Create(string path)
		{
			ImageParser result;
			using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				result = ImageParser.Create(fileStream);
			}
			return result;
		}

		public static ImageParser Create(Stream stream)
		{
			ImageParser result;
			using (BinaryReader binaryReader = new BinaryReader(stream))
			{
				int num = binaryReader.ReadInt32();
				int num2 = num;
				if (num2 != 827345986)
				{
					if (num2 != 844123202)
					{
						throw new InvalidDataException("Invalid file format.");
					}
					result = new Blp2Parser(stream);
				}
				else
				{
					result = new Blp1Parser(stream);
				}
			}
			return result;
		}
	}
}

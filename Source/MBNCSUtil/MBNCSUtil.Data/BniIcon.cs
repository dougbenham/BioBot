using System;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace MBNCSUtil.Data
{
	public sealed class BniIcon : IDisposable
	{
		private Image m_img;

		private int m_flags;

		private string[] m_softwareList;

		public Image Image
		{
			get
			{
				return this.m_img;
			}
		}

		public int UserFlags
		{
			get
			{
				return this.m_flags;
			}
		}

		public string[] SoftwareProductCodes
		{
			get
			{
				string[] array = new string[this.m_softwareList.Length];
				Array.Copy(this.m_softwareList, array, array.Length);
				return array;
			}
		}

		internal BniIcon(Image img, int flags, uint[] softwareList)
		{
			this.m_img = img;
			this.m_flags = flags;
			this.m_softwareList = new string[softwareList.Length];
			for (int i = 0; i < softwareList.Length; i++)
			{
				byte[] bytes = BitConverter.GetBytes(softwareList[i]);
				byte b = bytes[0];
				bytes[0] = bytes[3];
				bytes[3] = b;
				b = bytes[1];
				bytes[1] = bytes[2];
				bytes[2] = b;
				this.m_softwareList[i] = Encoding.ASCII.GetString(bytes);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_softwareList.Length > 0)
			{
				stringBuilder.Append(this.m_softwareList[0]);
				for (int i = 1; i < this.m_softwareList.Length; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.CurrentCulture, ",{0}", this.m_softwareList);
				}
			}
			return string.Format(CultureInfo.CurrentCulture, "Flags: 0x{0:x8}, Products: {1}", new object[]
			{
				this.m_flags,
				stringBuilder
			});
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing && this.m_img != null)
			{
				this.m_img.Dispose();
				this.m_img = null;
			}
		}
	}
}

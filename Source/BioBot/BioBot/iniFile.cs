using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BioBot
{
	public class iniFile
	{
		private string strFilename;

		public string FileName
		{
			get
			{
				return this.strFilename;
			}
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetPrivateProfileStringA", ExactSpelling = true, SetLastError = true)]
		private static extern int GetPrivateProfileString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpApplicationName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDefault, StringBuilder lpReturnedString, int nSize, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "WritePrivateProfileStringA", ExactSpelling = true, SetLastError = true)]
		private static extern int WritePrivateProfileString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpApplicationName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetPrivateProfileIntA", ExactSpelling = true, SetLastError = true)]
		private static extern int GetPrivateProfileInt([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpApplicationName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, int nDefault, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "WritePrivateProfileStringA", ExactSpelling = true, SetLastError = true)]
		private static extern int FlushPrivateProfileString(int lpApplicationName, int lpKeyName, int lpString, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

		public iniFile(string Filename)
		{
			this.strFilename = Filename;
		}

		public string GetString(string Section, string Key, string Default)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			int privateProfileString = iniFile.GetPrivateProfileString(ref Section, ref Key, ref Default, stringBuilder, stringBuilder.Capacity, ref this.strFilename);
			if (privateProfileString > 0)
			{
				return Strings.Left(stringBuilder.ToString(), privateProfileString);
			}
			return null;
		}

		public int GetInteger(string Section, string Key, int Default)
		{
			return iniFile.GetPrivateProfileInt(ref Section, ref Key, Default, ref this.strFilename);
		}

		public bool GetBoolean(string Section, string Key, bool Default)
		{
			return iniFile.GetPrivateProfileInt(ref Section, ref Key, (-((Default > false) ? 1 : 0)) ? 1 : 0, ref this.strFilename) == 1;
		}

		public void WriteString(string Section, string Key, string Value)
		{
			iniFile.WritePrivateProfileString(ref Section, ref Key, ref Value, ref this.strFilename);
			this.Flush();
		}

		public void WriteInteger(string Section, string Key, int Value)
		{
			this.WriteString(Section, Key, Conversions.ToString(Value));
			this.Flush();
		}

		public void WriteBoolean(string Section, string Key, bool Value)
		{
			this.WriteString(Section, Key, Conversions.ToString((-((Value > false) ? 1 : 0)) ? 1 : 0));
			this.Flush();
		}

		private void Flush()
		{
			iniFile.FlushPrivateProfileString(0, 0, 0, ref this.strFilename);
		}
	}
}

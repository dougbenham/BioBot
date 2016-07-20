using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace MBNCSUtil.Util
{
	internal static class PeFileReader
	{
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal struct DosImageHeader
		{
			public short e_magic;

			public short e_cblp;

			public short e_cp;

			public short e_crlc;

			public short e_cparhdr;

			public short e_minalloc;

			public short e_maxalloc;

			public short e_ss;

			public short e_sp;

			public short e_csum;

			public short e_ip;

			public short e_cs;

			public short e_lfarlc;

			public short e_ovno;

			public short e_res0;

			public short e_res1;

			public short e_res2;

			public short e_res3;

			public short e_oemid;

			public short e_oeminfo;

			public short e_res2_0;

			public short e_res2_1;

			public short e_res2_2;

			public short e_res2_3;

			public short e_res2_4;

			public short e_res2_5;

			public short e_res2_6;

			public short e_res2_7;

			public short e_res2_8;

			public short e_res2_9;

			public int e_lfanew;
		}

		[StructLayout(LayoutKind.Explicit)]
		internal struct NtHeaders
		{
			[StructLayout(LayoutKind.Sequential, Pack = 1)]
			internal struct OptionalHeader32
			{
				public short Magic;

				public byte MajorLinkerVersion;

				public byte MinorLinkerVersion;

				public int SizeOfCode;

				public int SizeOfInitializedData;

				public int SizeOfUninitializedData;

				public int AddressOfEntryPoint;

				public int BaseOfCode;

				public int BaseOfData;

				public int ImageBase;

				public int SectionAlignment;

				public int FileAlignment;

				public short MajorOSVersion;

				public short MinorOSVersion;

				public short MajorImageVersion;

				public short MinorImageVersion;

				public short MajorSubsystemVersion;

				public short MinorSubsystemVersion;

				public int Win32VersionValue;

				public int SizeOfImage;

				public int SizeOfHeaders;

				public int CheckSum;

				public short Subsystem;

				public short DllCharacteristics;

				public int SizeOfStackReserve;

				public int SizeOfStackCommit;

				public int SizeOfHeapReserve;

				public int SizeOfHeapCommit;

				public int LoaderFlags;

				public int NumberOfRvaAndSizes;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD0;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD1;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD2;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD3;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD4;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD5;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD6;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD7;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD8;

				public PeFileReader.NtHeaders.ImageDataDirectory IDD9;

				public PeFileReader.NtHeaders.ImageDataDirectory IDDA;

				public PeFileReader.NtHeaders.ImageDataDirectory IDDB;

				public PeFileReader.NtHeaders.ImageDataDirectory IDDC;

				public PeFileReader.NtHeaders.ImageDataDirectory IDDD;

				public PeFileReader.NtHeaders.ImageDataDirectory IDDE;

				public PeFileReader.NtHeaders.ImageDataDirectory IDDF;
			}

			internal struct ImageDataDirectory
			{
				public int VirtualAddress;

				public int Size;
			}

			[FieldOffset(0)]
			public int Signature;

			[FieldOffset(4)]
			public short Machine;

			[FieldOffset(6)]
			public short NumberOfSections;

			[FieldOffset(8)]
			public int TimeDateStamp;

			[FieldOffset(12)]
			public int PointerToSymbolTable;

			[FieldOffset(16)]
			public int NumberOfSymbols;

			[FieldOffset(20)]
			public short SizeOfOptionalHeader;

			[FieldOffset(22)]
			public short Characteristics;

			[FieldOffset(24)]
			public PeFileReader.NtHeaders.OptionalHeader32 OptionalHeader;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct ImageSectionHeader
		{
			[FieldOffset(0)]
			public byte Name;

			[FieldOffset(8)]
			public int PhysicalAddress;

			[FieldOffset(8)]
			public int VirtualSize;

			[FieldOffset(12)]
			public int VirtualAddress;

			[FieldOffset(16)]
			public int SizeOfRawData;

			[FieldOffset(20)]
			public int PointerToRawData;

			[FieldOffset(24)]
			public int PointerToRelocations;

			[FieldOffset(28)]
			public int PointerToLinenumbers;

			[FieldOffset(32)]
			public short NumberOfRelocations;

			[FieldOffset(34)]
			public short NumberOfLinenumbers;

			[FieldOffset(36)]
			public int Characteristics;
		}

		public struct ImageBaseRelocation
		{
			public int VirtualAddress;

			public int SizeOfBlock;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct ImageResourceDirectoryEntry
		{
			[FieldOffset(0)]
			[MarshalAs(UnmanagedType.I4)]
			public BitVector32 NameOffsetVector;

			[FieldOffset(0)]
			public int Name;

			[FieldOffset(0)]
			public short Id;

			[FieldOffset(4)]
			public int OffsetToData;

			[FieldOffset(4)]
			public BitVector32 DirectoryOffsetVector;
		}

		public struct ImageResourceDirectory
		{
			public int Characteristics;

			public int TimeDateStamp;

			public short MajorVersion;

			public short MinorVersion;

			public short NumberOfNamedEntries;

			public short NumberOfIdEntries;
		}
	}
}

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace MBNCSUtil.Util
{
	internal class PeFileLoader : IDisposable
	{
		private const short DOS_SIGNATURE = 23117;

		private const int NT_SIGNATURE = 17744;

		private const int IMAGE_SIZEOF_BASE_RELOCATION = 8;

		private const int IMAGE_REL_BASED_HIGHLOW = 3;

		private IntPtr m_ptr;

		private IntPtr m_baseAddrPtr;

		public unsafe byte* BaseAddress
		{
			get
			{
				return (byte*)this.m_baseAddrPtr.ToPointer();
			}
		}

		public unsafe PeFileLoader(string path)
		{
			byte[] array = File.ReadAllBytes(path);
			this.m_ptr = Marshal.AllocHGlobal(array.Length);
			Marshal.Copy(array, 0, this.m_ptr, array.Length);
			byte* ptr = (byte*)this.m_ptr.ToPointer();
			PeFileReader.DosImageHeader* ptr2 = (PeFileReader.DosImageHeader*)ptr;
			if (ptr2->e_magic != 23117)
			{
				Marshal.FreeHGlobal(this.m_ptr);
				this.m_ptr = IntPtr.Zero;
				throw new FileLoadException("Invalid DOS signature.");
			}
			PeFileReader.NtHeaders* ptr3 = (PeFileReader.NtHeaders*)(ptr + ptr2->e_lfanew);
			if (ptr3->Signature != 17744)
			{
				Marshal.FreeHGlobal(this.m_ptr);
				this.m_ptr = IntPtr.Zero;
				throw new FileLoadException("Invalid NT signature.");
			}
			this.m_baseAddrPtr = Marshal.AllocHGlobal(ptr3->OptionalHeader.SizeOfImage);
			byte* ptr4 = (byte*)this.m_baseAddrPtr.ToPointer();
			Native.Memcpy((void*)ptr4, (void*)ptr2, ptr2->e_lfanew + ptr3->OptionalHeader.SizeOfHeaders);
			int imageBase = ptr3->OptionalHeader.ImageBase;
			PeFileLoader.CopySections(ptr, ptr3, ptr4);
			int num = ptr4 - imageBase;
			if (num != 0)
			{
				PeFileLoader.PerformBaseReloc(ptr4, ptr3, num);
			}
		}

		private unsafe static void PerformBaseReloc(byte* baseaddr, PeFileReader.NtHeaders* ntheader, int relocOffset)
		{
			PeFileReader.NtHeaders.ImageDataDirectory* ptr = &ntheader->OptionalHeader.IDD5;
			if (ptr->Size > 0)
			{
				PeFileReader.ImageBaseRelocation* ptr2 = (PeFileReader.ImageBaseRelocation*)(baseaddr + ptr->VirtualAddress);
				while (ptr2->VirtualAddress > 0)
				{
					byte* ptr3 = baseaddr + ptr2->VirtualAddress;
					ushort* ptr4 = (ushort*)(ptr2 + 8 / sizeof(PeFileReader.ImageBaseRelocation));
					int i = 0;
					while (i < (ptr2->SizeOfBlock - 8) / 2)
					{
						int num = *ptr4 >> 12;
						int num2 = (int)(*ptr4 & 4095);
						if (num == 3)
						{
							uint* ptr5 = (uint*)(ptr3 + num2);
							*ptr5 += (uint)relocOffset;
						}
						i++;
						ptr4++;
					}
					ptr2 += (long)ptr2->SizeOfBlock / (long)sizeof(PeFileReader.ImageBaseRelocation);
				}
			}
		}

		private unsafe static void CopySections(byte* data, PeFileReader.NtHeaders* header, byte* baseaddr)
		{
			PeFileReader.ImageSectionHeader* ptr = (PeFileReader.ImageSectionHeader*)(header + 24 / sizeof(PeFileReader.NtHeaders) + (int)header->SizeOfOptionalHeader / sizeof(PeFileReader.NtHeaders));
			int i = 0;
			while (i < (int)header->NumberOfSections)
			{
				if (ptr->SizeOfRawData == 0)
				{
					int sectionAlignment = header->OptionalHeader.SectionAlignment;
					if (sectionAlignment > 0)
					{
						byte* ptr2 = baseaddr + ptr->VirtualAddress;
						ptr->PhysicalAddress = ptr2;
						Native.Memset((void*)ptr2, 0, sectionAlignment);
					}
				}
				else
				{
					byte* ptr2 = baseaddr + ptr->VirtualAddress;
					Native.Memcpy((void*)ptr2, (void*)(data + ptr->PointerToRawData), ptr->SizeOfRawData);
					ptr->PhysicalAddress = ptr2;
				}
				i++;
				ptr++;
			}
		}

		public static int GetVersion(string filename)
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filename);
			return (versionInfo.ProductMajorPart & 255) << 24 | (versionInfo.ProductMinorPart & 255) << 16 | (versionInfo.ProductBuildPart & 255) << 8 | (versionInfo.ProductPrivatePart & 255);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this.m_ptr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_ptr);
				this.m_ptr = IntPtr.Zero;
			}
			if (this.m_baseAddrPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_baseAddrPtr);
				this.m_baseAddrPtr = IntPtr.Zero;
			}
		}

		~PeFileLoader()
		{
			this.Dispose(false);
		}
	}
}

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MBNCSUtil.Util
{
	internal static class LockdownCrev
	{
		private const int IMAGE_DIRECTORY_ENTRY_IMPORT = 1;

		private const int IMAGE_DIRECTORY_ENTRY_BASERELOC = 5;

		private const int IMAGE_SIZEOF_BASE_RELOCATION = 8;

		private const int IMAGE_REL_BASED_LOW = 2;

		private const int IMAGE_REL_BASED_HIGHLOW = 3;

		private const int IMAGE_REL_BASED_DIR64 = 10;

		private static readonly int[] seeds = new int[]
		{
			-1577908902,
			1448546892,
			394308423,
			-2135710704,
			-1356760598,
			137869320,
			1864701638,
			-484998840,
			265261238,
			-219114218,
			932023692,
			133750915,
			-1326540991,
			2032388527,
			-904814498,
			-685522922,
			-44804720,
			-77591506,
			1749845893,
			1488903691
		};

		public unsafe static bool CheckRevision(string file1, string file2, string file3, byte[] valueString, ref int version, ref int checksum, out byte[] digest, string lockdownFile, string imageDump)
		{
			int num = 1;
			int num2 = 0;
			digest = null;
			int digit = LockdownCrev.GetDigit(lockdownFile);
			int seed = LockdownCrev.seeds[digit];
			version = PeFileLoader.GetVersion(file1);
			using (HeapPtr heapPtr = new HeapPtr(20, AllocMethod.HGlobal))
			{
				using (HeapPtr heapPtr2 = new HeapPtr(20, AllocMethod.HGlobal))
				{
					using (HeapPtr heapPtr3 = new HeapPtr(valueString.Length, AllocMethod.HGlobal))
					{
						using (HeapPtr heapPtr4 = new HeapPtr(valueString.Length, AllocMethod.HGlobal))
						{
							using (HeapPtr heapPtr5 = new HeapPtr(64, AllocMethod.HGlobal))
							{
								using (HeapPtr heapPtr6 = new HeapPtr(64, AllocMethod.HGlobal))
								{
									using (HeapPtr heapPtr7 = new HeapPtr(16, AllocMethod.HGlobal))
									{
										using (HeapPtr heapPtr8 = new HeapPtr(17, AllocMethod.HGlobal))
										{
											heapPtr3.MarshalData(valueString);
											if (!LockdownCrev.ShuffleValueString(heapPtr3, valueString.Length, heapPtr4))
											{
												bool result = false;
												return result;
											}
											Native.Memset(heapPtr5.ToPointer(), 54, 64);
											Native.Memset(heapPtr6.ToPointer(), 92, 64);
											byte* ptr = heapPtr5;
											byte* ptr2 = heapPtr6;
											byte* ptr3 = heapPtr4;
											for (int i = 0; i < 16; i++)
											{
												byte* expr_E0 = ptr + i;
												*expr_E0 ^= ptr3[i];
												byte* expr_EE = ptr2 + i;
												*expr_EE ^= ptr3[i];
											}
											LockdownSha1.Context context = LockdownSha1.Init();
											LockdownSha1.Update(context, ptr, 64);
											if (!LockdownCrev.HashFile(context, lockdownFile, seed))
											{
												bool result = false;
												return result;
											}
											if (!LockdownCrev.HashFile(context, file1, seed))
											{
												bool result = false;
												return result;
											}
											if (!LockdownCrev.HashFile(context, file2, seed))
											{
												bool result = false;
												return result;
											}
											if (!LockdownCrev.HashFile(context, file3, seed))
											{
												bool result = false;
												return result;
											}
											LockdownSha1.HashFile(context, imageDump);
											LockdownSha1.Update(context, (byte*)(&num), 4);
											LockdownSha1.Update(context, (byte*)(&num2), 4);
											LockdownSha1.Final(context, heapPtr);
											context = LockdownSha1.Init();
											LockdownSha1.Update(context, heapPtr6, 64);
											LockdownSha1.Update(context, heapPtr, 20);
											LockdownSha1.Final(context, heapPtr2);
											checksum = *(int*)heapPtr2;
											Native.Memmove(heapPtr7, heapPtr2 + 4, 16);
											int num3 = 255;
											if (!LockdownCrev.CalculateDigest(heapPtr8, ref num3, heapPtr7))
											{
												bool result = false;
												return result;
											}
											heapPtr8[num3] = 0;
											digest = new byte[num3];
											Marshal.Copy(new IntPtr(heapPtr8.ToPointer()), digest, 0, num3);
										}
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		internal unsafe static int GetDigit(string filename)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(filename);
			int num;
			fixed (byte* ptr = bytes)
			{
				byte* ptr2 = ptr + filename.Length - 4;
				num = (int)(*(ptr2 - 1) - 48);
				int num2 = (int)(*(ptr2 - 2) - 48);
				if (num2 == 1)
				{
					num += 10;
				}
				if (num < 0 || num > 19)
				{
					return 0;
				}
			}
			return num;
		}

		internal unsafe static bool ShuffleValueString(byte* str, int len, byte* buffer)
		{
			int num = 0;
			while (len != 0)
			{
				byte b = 0;
				for (int i = 0; i < num; i++)
				{
					byte b2 = buffer[i];
					buffer[i] = b - buffer[i];
					b = (byte)(((int)b2 << 8) - (int)b2 + (int)b >> 8);
				}
				if (b != 0)
				{
					if (num >= 16)
					{
						return false;
					}
					buffer[num++] = b;
				}
				byte b3 = str[len - 1] - 1;
				for (int i = 0; i < num; i++)
				{
					byte* expr_57 = buffer + i;
					*expr_57 += b3;
					b3 = ((buffer[i] < b3) ? 1 : 0);
				}
				if (b3 != 0)
				{
					if (num >= 16)
					{
						return false;
					}
					buffer[num++] = b3;
				}
				len--;
			}
			Native.Memset((void*)(buffer + num), 0, 16 - num);
			return true;
		}

		internal unsafe static bool CalculateDigest(byte* str1, ref int length, byte* str2)
		{
			byte* ptr = str1;
			bool result = true;
			int i = 16;
			while (i > 0)
			{
				while (i > 0 && str2[i - 1] == 0)
				{
					i--;
				}
				if (i != 0)
				{
					ushort num = 0;
					for (int j = i - 1; j >= 0; j--)
					{
						ushort num2 = (ushort)(num << 8) + (ushort)(str2[j] & 255);
						LockdownCrev.WordShift(ref num2, ref num);
						str2[j] = (byte)num2;
					}
					if (16 - i >= length)
					{
						result = false;
					}
					else
					{
						*ptr = (byte)(num + 1);
					}
					ptr++;
				}
			}
			length = (int)((long)(ptr - str1));
			return result;
		}

		internal static void WordShift(ref ushort str1, ref ushort str2)
		{
			ushort num = (ushort)(str1 >> 8);
			ushort num2 = str1 & 255;
			str2 = (ushort)(num + num2 >> 8) + (num + num2 & 255);
			num = (str2 & 65280);
			num2 = (str2 + 1 & 255);
			ushort num3 = ((str2 & 255) != 255) ? 1 : 0;
			str2 = (num | num2 - num3);
			num = str1 - str2;
			num2 = (((str1 - str2 >> 8 & 255) + 1 != 0) ? 0 : 256);
			num3 = (ushort)((int)num & -65281);
			str1 = (num3 | num2);
			num = (ushort)((int)str1 & -256);
			num2 = (-str1 & 255);
			str1 = (num | num2);
		}

		internal unsafe static bool HashFile(LockdownSha1.Context context, string filename, int seed)
		{
			LockdownHeap heap = new LockdownHeap();
			PeFileLoader peFileLoader = new PeFileLoader(filename);
			byte* baseAddress = peFileLoader.BaseAddress;
			PeFileReader.DosImageHeader* ptr = (PeFileReader.DosImageHeader*)baseAddress;
			PeFileReader.NtHeaders* ptr2 = (PeFileReader.NtHeaders*)(baseAddress + ptr->e_lfanew);
			int sectionAlignment = ptr2->OptionalHeader.SectionAlignment;
			byte* preferredBaseAddr = ptr2->OptionalHeader.ImageBase;
			PeFileReader.NtHeaders.ImageDataDirectory* ptr3 = &ptr2->OptionalHeader.IDD5;
			byte* ptr4 = (byte*)(ptr2 + 24 / sizeof(PeFileReader.NtHeaders) + (int)ptr2->SizeOfOptionalHeader / sizeof(PeFileReader.NtHeaders));
			int sizeOfHeaders = ptr2->OptionalHeader.SizeOfHeaders;
			LockdownSha1.Update(context, baseAddress, sizeOfHeaders);
			if (ptr3->VirtualAddress != 0 && ptr3->Size != 0 && !LockdownCrev.ProcessRelocDir(heap, baseAddress, ptr3))
			{
				peFileLoader.Dispose();
				return false;
			}
			for (int i = 0; i < (int)ptr2->NumberOfSections; i++)
			{
				if (!LockdownCrev.ProcessSection(context, heap, baseAddress, preferredBaseAddr, (PeFileReader.ImageSectionHeader*)(ptr4 + (IntPtr)i * 40), sectionAlignment, seed))
				{
					peFileLoader.Dispose();
					return false;
				}
			}
			peFileLoader.Dispose();
			return true;
		}

		private unsafe static bool ProcessSection(LockdownSha1.Context context, LockdownHeap heap, byte* baseaddr, byte* preferredBaseAddr, PeFileReader.ImageSectionHeader* section, int sectionAlignment, int seed)
		{
			using (HeapPtr heapPtr = heap.ToPointer())
			{
				int* ptr = (int*)heapPtr.ToPointer();
				int num = baseaddr - preferredBaseAddr;
				int virtualAddress = section->VirtualAddress;
				int virtualSize = section->VirtualSize;
				int num2 = (virtualSize + sectionAlignment - 1 & ~(sectionAlignment - 1)) - virtualSize;
				if (section->Characteristics < 0)
				{
					LockdownSha1.Pad(context, num2 + virtualSize);
				}
				else
				{
					int num3 = 0;
					if (heap.CurrentLength > 0)
					{
						int num4 = 0;
						while (num3 < heap.CurrentLength && ptr[num4] < virtualAddress)
						{
							num3++;
							num4 += 4;
						}
					}
					if (virtualSize > 0)
					{
						byte* ptr2 = baseaddr + virtualAddress;
						byte* ptr3 = ptr2;
						int num5 = num3 * 4;
						do
						{
							int num6 = (int)((long)(ptr2 - ptr3) + (long)virtualSize);
							int num7 = 0;
							if (num3 < heap.CurrentLength)
							{
								num7 = ptr[num5] + ptr2 - virtualAddress;
							}
							if (num7 != 0)
							{
								num7 -= ptr3;
								if (num7 < num6)
								{
									num6 = num7;
								}
							}
							if (num6 != 0)
							{
								LockdownSha1.Update(context, ptr3, num6);
								ptr3 += num6;
							}
							else
							{
								int* ptr4 = stackalloc int[4 * 16 / 4];
								Native.Memcpy((void*)ptr4, (void*)(ptr + num5), 16);
								int num8 = *(int*)ptr3 - num ^ seed;
								LockdownSha1.Update(context, (byte*)(&num8), 4);
								ptr3 += ptr4[1];
								num3++;
								num5 += 4;
							}
						}
						while ((long)(ptr3 - ptr2) < (long)virtualSize);
					}
					if (num2 > 0)
					{
						int num9 = 0;
						IntPtr hglobal = Marshal.AllocHGlobal(num2);
						byte* ptr5 = (byte*)hglobal.ToPointer();
						Native.Memset((void*)ptr5, 0, num2);
						do
						{
							int num7 = 0;
							if (num3 < heap.CurrentLength)
							{
								int num8 = ptr[(IntPtr)num3 * 16 / 4];
								num7 = num8 - virtualSize - virtualAddress + ptr5;
							}
							num2 += num9;
							if (num7 != 0)
							{
								num7 -= *(int*)(ptr5 + (IntPtr)(num9 / 4) * 4);
								if (num7 < num2)
								{
									num2 = num7;
								}
							}
							if (num2 != 0)
							{
								LockdownSha1.Update(context, ptr5 + num9, num2);
								num9 += num2;
							}
						}
						while (num9 < num2);
						Marshal.FreeHGlobal(hglobal);
					}
				}
			}
			return true;
		}

		private unsafe static bool ProcessRelocDir(LockdownHeap heap, byte* baseaddr, PeFileReader.NtHeaders.ImageDataDirectory* relocDir)
		{
			int[] array = new int[4];
			PeFileReader.ImageBaseRelocation* ptr = (PeFileReader.ImageBaseRelocation*)(baseaddr + relocDir->VirtualAddress);
			while (ptr->VirtualAddress > 0)
			{
				short* ptr2 = (short*)(ptr + 8 / sizeof(PeFileReader.ImageBaseRelocation));
				int i = 0;
				while (i < (ptr->SizeOfBlock - 8) / 2)
				{
					int num = *ptr2 >> 12;
					int num2 = (int)(*ptr2 & 4095);
					if (num != 0)
					{
						int num3 = num;
						int num4;
						switch (num3)
						{
						case 2:
							num4 = 2;
							break;
						case 3:
							num4 = 4;
							break;
						default:
							if (num3 != 10)
							{
								return false;
							}
							num4 = 8;
							break;
						}
						array[0] = ptr->VirtualAddress + num2;
						array[1] = num4;
						array[2] = 2;
						array[3] = num;
						heap.Add(array);
					}
					i++;
					ptr2++;
				}
				ptr += ptr->SizeOfBlock / sizeof(PeFileReader.ImageBaseRelocation);
			}
			return true;
		}
	}
}

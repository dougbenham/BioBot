using System;

namespace MBNCSUtil.Data
{
	internal enum MpqErrorCodes
	{
		Okay = 1,
		MpqInvalid = -2061500315,
		FileNotFound,
		DiskFull = -2061500312,
		HashTableFull,
		AlreadyExists,
		BadOpenMode = -2061500308,
		CompactError = -2060451839
	}
}

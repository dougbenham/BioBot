using System;
using System.IO;
using System.Runtime.Serialization;

namespace MBNCSUtil.Data
{
	[Serializable]
	public class MpqException : IOException
	{
		public MpqException()
		{
		}

		public MpqException(string message) : base(message)
		{
		}

		public MpqException(string message, Exception inner) : base(message, inner)
		{
		}

		protected MpqException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

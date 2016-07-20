using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MBNCSUtil
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("MBNCSUtil.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static string bnftp_filenotfound
		{
			get
			{
				return Resources.ResourceManager.GetString("bnftp_filenotfound", Resources.resourceCulture);
			}
		}

		internal static string bnftp_ver1invalidProduct
		{
			get
			{
				return Resources.ResourceManager.GetString("bnftp_ver1invalidProduct", Resources.resourceCulture);
			}
		}

		internal static string bnftp_ver2invalidProduct
		{
			get
			{
				return Resources.ResourceManager.GetString("bnftp_ver2invalidProduct", Resources.resourceCulture);
			}
		}

		internal static string cdKeyArgNull
		{
			get
			{
				return Resources.ResourceManager.GetString("cdKeyArgNull", Resources.resourceCulture);
			}
		}

		internal static string crevExtrMpqNum_NoLockdown
		{
			get
			{
				return Resources.ResourceManager.GetString("crevExtrMpqNum_NoLockdown", Resources.resourceCulture);
			}
		}

		internal static string crExeFileNull
		{
			get
			{
				return Resources.ResourceManager.GetString("crExeFileNull", Resources.resourceCulture);
			}
		}

		internal static string crFileListInvalid
		{
			get
			{
				return Resources.ResourceManager.GetString("crFileListInvalid", Resources.resourceCulture);
			}
		}

		internal static string crFileListNull
		{
			get
			{
				return Resources.ResourceManager.GetString("crFileListNull", Resources.resourceCulture);
			}
		}

		internal static string crMpqNameArgShort
		{
			get
			{
				return Resources.ResourceManager.GetString("crMpqNameArgShort", Resources.resourceCulture);
			}
		}

		internal static string crMpqNameNull
		{
			get
			{
				return Resources.ResourceManager.GetString("crMpqNameNull", Resources.resourceCulture);
			}
		}

		internal static string crValstringNull
		{
			get
			{
				return Resources.ResourceManager.GetString("crValstringNull", Resources.resourceCulture);
			}
		}

		internal static string d2dv
		{
			get
			{
				return Resources.ResourceManager.GetString("d2dv", Resources.resourceCulture);
			}
		}

		internal static string d2xp
		{
			get
			{
				return Resources.ResourceManager.GetString("d2xp", Resources.resourceCulture);
			}
		}

		internal static string dataNull
		{
			get
			{
				return Resources.ResourceManager.GetString("dataNull", Resources.resourceCulture);
			}
		}

		internal static string encNull
		{
			get
			{
				return Resources.ResourceManager.GetString("encNull", Resources.resourceCulture);
			}
		}

		internal static string exeInfoFmt
		{
			get
			{
				return Resources.ResourceManager.GetString("exeInfoFmt", Resources.resourceCulture);
			}
		}

		internal static string fileNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("fileNotFound", Resources.resourceCulture);
			}
		}

		internal static string fileNull
		{
			get
			{
				return Resources.ResourceManager.GetString("fileNull", Resources.resourceCulture);
			}
		}

		internal static string invalidCdKeyGeneral
		{
			get
			{
				return Resources.ResourceManager.GetString("invalidCdKeyGeneral", Resources.resourceCulture);
			}
		}

		internal static string invalidCdKeyHashed
		{
			get
			{
				return Resources.ResourceManager.GetString("invalidCdKeyHashed", Resources.resourceCulture);
			}
		}

		internal static string invalidCdKeySc
		{
			get
			{
				return Resources.ResourceManager.GetString("invalidCdKeySc", Resources.resourceCulture);
			}
		}

		internal static string invalidCdKeyWar2
		{
			get
			{
				return Resources.ResourceManager.GetString("invalidCdKeyWar2", Resources.resourceCulture);
			}
		}

		internal static string invalidCdKeyWar3
		{
			get
			{
				return Resources.ResourceManager.GetString("invalidCdKeyWar3", Resources.resourceCulture);
			}
		}

		internal static string mpq_badOpenMode
		{
			get
			{
				return Resources.ResourceManager.GetString("mpq_badOpenMode", Resources.resourceCulture);
			}
		}

		internal static string mpq_fileNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("mpq_fileNotFound", Resources.resourceCulture);
			}
		}

		internal static string mpq_mpqArchiveCorrupt
		{
			get
			{
				return Resources.ResourceManager.GetString("mpq_mpqArchiveCorrupt", Resources.resourceCulture);
			}
		}

		internal static string mpq_UnknownErrorType
		{
			get
			{
				return Resources.ResourceManager.GetString("mpq_UnknownErrorType", Resources.resourceCulture);
			}
		}

		internal static string mpqFilePathArgNull
		{
			get
			{
				return Resources.ResourceManager.GetString("mpqFilePathArgNull", Resources.resourceCulture);
			}
		}

		internal static string nlsAcctCreateSpace
		{
			get
			{
				return Resources.ResourceManager.GetString("nlsAcctCreateSpace", Resources.resourceCulture);
			}
		}

		internal static string nlsAcctLoginSpace
		{
			get
			{
				return Resources.ResourceManager.GetString("nlsAcctLoginSpace", Resources.resourceCulture);
			}
		}

		internal static string nlsLoginProofSpace
		{
			get
			{
				return Resources.ResourceManager.GetString("nlsLoginProofSpace", Resources.resourceCulture);
			}
		}

		internal static string nlsSalt32
		{
			get
			{
				return Resources.ResourceManager.GetString("nlsSalt32", Resources.resourceCulture);
			}
		}

		internal static string nlsServerKey32
		{
			get
			{
				return Resources.ResourceManager.GetString("nlsServerKey32", Resources.resourceCulture);
			}
		}

		internal static string nlsServerProof20
		{
			get
			{
				return Resources.ResourceManager.GetString("nlsServerProof20", Resources.resourceCulture);
			}
		}

		internal static string nlsSrvSig128
		{
			get
			{
				return Resources.ResourceManager.GetString("nlsSrvSig128", Resources.resourceCulture);
			}
		}

		internal static string notInitialized
		{
			get
			{
				return Resources.ResourceManager.GetString("notInitialized", Resources.resourceCulture);
			}
		}

		internal static string objAlreadyInited
		{
			get
			{
				return Resources.ResourceManager.GetString("objAlreadyInited", Resources.resourceCulture);
			}
		}

		internal static string objNotInited
		{
			get
			{
				return Resources.ResourceManager.GetString("objNotInited", Resources.resourceCulture);
			}
		}

		internal static string param_cdKey
		{
			get
			{
				return Resources.ResourceManager.GetString("param_cdKey", Resources.resourceCulture);
			}
		}

		internal static string param_data
		{
			get
			{
				return Resources.ResourceManager.GetString("param_data", Resources.resourceCulture);
			}
		}

		internal static string param_enc
		{
			get
			{
				return Resources.ResourceManager.GetString("param_enc", Resources.resourceCulture);
			}
		}

		internal static string param_fileName
		{
			get
			{
				return Resources.ResourceManager.GetString("param_fileName", Resources.resourceCulture);
			}
		}

		internal static string param_len
		{
			get
			{
				return Resources.ResourceManager.GetString("param_len", Resources.resourceCulture);
			}
		}

		internal static string param_mpqFilePath
		{
			get
			{
				return Resources.ResourceManager.GetString("param_mpqFilePath", Resources.resourceCulture);
			}
		}

		internal static string param_productId
		{
			get
			{
				return Resources.ResourceManager.GetString("param_productId", Resources.resourceCulture);
			}
		}

		internal static string param_salt
		{
			get
			{
				return Resources.ResourceManager.GetString("param_salt", Resources.resourceCulture);
			}
		}

		internal static string param_serverKey
		{
			get
			{
				return Resources.ResourceManager.GetString("param_serverKey", Resources.resourceCulture);
			}
		}

		internal static string param_str
		{
			get
			{
				return Resources.ResourceManager.GetString("param_str", Resources.resourceCulture);
			}
		}

		internal static string param_value
		{
			get
			{
				return Resources.ResourceManager.GetString("param_value", Resources.resourceCulture);
			}
		}

		internal static string sexp
		{
			get
			{
				return Resources.ResourceManager.GetString("sexp", Resources.resourceCulture);
			}
		}

		internal static string star
		{
			get
			{
				return Resources.ResourceManager.GetString("star", Resources.resourceCulture);
			}
		}

		internal static byte[] StormLib32
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("StormLib32", Resources.resourceCulture);
				return (byte[])@object;
			}
		}

		internal static byte[] StormLib64
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("StormLib64", Resources.resourceCulture);
				return (byte[])@object;
			}
		}

		internal static string streamNull
		{
			get
			{
				return Resources.ResourceManager.GetString("streamNull", Resources.resourceCulture);
			}
		}

		internal static string strNull
		{
			get
			{
				return Resources.ResourceManager.GetString("strNull", Resources.resourceCulture);
			}
		}

		internal static string strTooLongFmt
		{
			get
			{
				return Resources.ResourceManager.GetString("strTooLongFmt", Resources.resourceCulture);
			}
		}

		internal static string valMustBeGTZero
		{
			get
			{
				return Resources.ResourceManager.GetString("valMustBeGTZero", Resources.resourceCulture);
			}
		}

		internal static string w2bn
		{
			get
			{
				return Resources.ResourceManager.GetString("w2bn", Resources.resourceCulture);
			}
		}

		internal static string w3xp
		{
			get
			{
				return Resources.ResourceManager.GetString("w3xp", Resources.resourceCulture);
			}
		}

		internal static string war3
		{
			get
			{
				return Resources.ResourceManager.GetString("war3", Resources.resourceCulture);
			}
		}

		internal static string xshaMaxHash1024
		{
			get
			{
				return Resources.ResourceManager.GetString("xshaMaxHash1024", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace GSPcLocalViewer
{
	public static class DataSize
	{
		public const long c_spaceBuffer = 10485760L;

		public static long spaceLeft;

		public static long spaceAllowed;

		public static int miliSecInterval
		{
			get;
			set;
		}

		static DataSize()
		{
			DataSize.spaceLeft = (long)99999999;
			DataSize.spaceAllowed = (long)99999999;
			DataSize.miliSecInterval = 0;
		}

		public static string ExtractAlphabets(string str, bool allowDots)
		{
			char[] charArray = str.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			char[] chrArray = charArray;
			for (int i = 0; i < (int)chrArray.Length; i++)
			{
				char chr = chrArray[i];
				if ((allowDots || !object.Equals(chr, '.')) && !char.IsNumber(chr))
				{
					stringBuilder.Append(chr);
				}
			}
			return stringBuilder.ToString();
		}

		public static string ExtractNumbers(string str, bool allowDecimals)
		{
			char[] charArray = str.ToCharArray();
			StringBuilder stringBuilder = new StringBuilder();
			char[] chrArray = charArray;
			for (int i = 0; i < (int)chrArray.Length; i++)
			{
				char chr = chrArray[i];
				if (char.IsNumber(chr))
				{
					stringBuilder.Append(chr);
				}
				else if (allowDecimals && object.Equals(chr, '.'))
				{
					stringBuilder.Append(chr);
				}
			}
			return stringBuilder.ToString();
		}

		public static string FormattedSize(long size)
		{
			double num = (double)size / Math.Pow(1024, 3);
			if (num >= 1)
			{
				num = Math.Round(num, 2, MidpointRounding.AwayFromZero);
				return string.Concat(num.ToString(), " GB");
			}
			num = (double)size / Math.Pow(1024, 2);
			if (num >= 1)
			{
				num = Math.Round(num, 2, MidpointRounding.AwayFromZero);
				return string.Concat(num.ToString(), " MB");
			}
			num = (double)(size / (long)1024);
			if (num < 1)
			{
				return string.Concat(size.ToString(), " Bytes");
			}
			num = Math.Round(num, 2, MidpointRounding.AwayFromZero);
			return string.Concat(num.ToString(), " KB");
		}

		public static long FormattedSize(string size)
		{
			long num;
			string str = size.ToUpper().Trim();
			long num1 = (long)0;
			try
			{
				if (str.EndsWith("GB"))
				{
					double num2 = double.Parse(str.Replace("GB", string.Empty).Trim());
					num = (long)(num2 * Math.Pow(1024, 3));
				}
				else if (str.EndsWith("MB"))
				{
					double num3 = double.Parse(str.Replace("MB", string.Empty).Trim());
					num = (long)(num3 * Math.Pow(1024, 2));
				}
				else if (str.EndsWith("KB"))
				{
					double num4 = double.Parse(str.Replace("KB", string.Empty).Trim());
					num = (long)(num4 * 1024);
				}
				else if (!str.EndsWith("BYTES"))
				{
					num = (long)0;
				}
				else
				{
					double num5 = double.Parse(str.Replace("BYTES", string.Empty).Trim());
					num = (long)num5;
				}
			}
			catch
			{
				num = (long)0;
			}
			return num;
		}

		public static long GetDataSizeLong()
		{
			long num = (long)0;
			string empty = string.Empty;
			if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] != null)
			{
				empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"];
			}
			if (empty == string.Empty)
			{
				return (long)0;
			}
			num = DataSize.FormattedSize(empty);
			if (num == (long)0)
			{
				return (long)0;
			}
			if (num > (long)10485760)
			{
				return num;
			}
			return (long)0;
		}

		public static string GetDataSizeString()
		{
			long num = (long)0;
			string empty = string.Empty;
			if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] != null)
			{
				empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"];
			}
			if (empty == string.Empty)
			{
				return string.Empty;
			}
			num = DataSize.FormattedSize(empty);
			if (num == (long)0)
			{
				return string.Empty;
			}
			if (num > (long)10485760)
			{
				return empty;
			}
			return string.Empty;
		}

		public static long GetDirSize(DirectoryInfo dir)
		{
			long length = (long)0;
			DirectoryInfo[] directories = dir.GetDirectories();
			FileInfo[] files = dir.GetFiles();
			for (int i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				try
				{
					length += fileInfo.Length;
				}
				catch
				{
				}
			}
			DirectoryInfo[] directoryInfoArray = directories;
			for (int j = 0; j < (int)directoryInfoArray.Length; j++)
			{
				DirectoryInfo directoryInfo = directoryInfoArray[j];
				try
				{
					length += DataSize.GetDirSize(directoryInfo);
				}
				catch
				{
				}
			}
			return length;
		}

		public static long GetFreeSpace(string driveLetter)
		{
			return (new DriveInfo(driveLetter)).AvailableFreeSpace;
		}

		public static bool IsDataSizeApplied()
		{
			long num = (long)0;
			string empty = string.Empty;
			if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] != null)
			{
				empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"];
			}
			if (empty == string.Empty)
			{
				return false;
			}
			num = DataSize.FormattedSize(empty);
			if (num == (long)0)
			{
				return false;
			}
			if (num > (long)10485760)
			{
				return true;
			}
			return false;
		}

		public static void ReInitialize()
		{
			DataSize.spaceLeft = (long)99999999;
			DataSize.spaceAllowed = (long)99999999;
			DataSize.miliSecInterval = 0;
		}

		public static void UpdateSpaceVars()
		{
			try
			{
				string empty = string.Empty;
				string str = string.Empty;
				long freeSpace = (long)0;
				long dirSize = (long)0;
				string item = string.Empty;
				string empty1 = string.Empty;
				if (Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] != null)
				{
					empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
					str = empty.Substring(0, 1);
					if (!Directory.Exists(empty))
					{
						Directory.CreateDirectory(empty);
					}
					if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] != null)
					{
						item = Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"];
						long num = DataSize.FormattedSize(item);
						if (num > (long)0)
						{
							freeSpace = DataSize.GetFreeSpace(str);
							dirSize = DataSize.GetDirSize(new DirectoryInfo(empty));
							DataSize.spaceAllowed = num;
							if (num > freeSpace + dirSize)
							{
								num = freeSpace + dirSize;
							}
							DataSize.spaceLeft = num - dirSize;
							if (DataSize.spaceLeft < (long)0)
							{
								DataSize.spaceLeft = (long)0;
							}
						}
					}
				}
			}
			catch
			{
			}
		}
	}
}
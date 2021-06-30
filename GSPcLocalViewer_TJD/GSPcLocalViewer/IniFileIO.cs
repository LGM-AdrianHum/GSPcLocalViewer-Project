using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace GSPcLocalViewer
{
	public class IniFileIO
	{
		public IniFileIO()
		{
		}

		public ArrayList GetKeys(string iniFile, string category)
		{
			char[] charArray = (new string(' ', 65535)).ToCharArray();
			IniFileIO.GetPrivateProfileString(category, null, null, charArray, 65536, iniFile);
			string str = new string(charArray);
			ArrayList arrayLists = new ArrayList(str.Split(new char[1]));
			arrayLists.RemoveRange(arrayLists.Count - 2, 2);
			return arrayLists;
		}

		public string GetKeyValue(string Section, string KeyName, string FileName)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			IniFileIO.GetKeyValueA(Section, KeyName, "", stringBuilder, 255, FileName);
			return stringBuilder.ToString();
		}

		[DllImport("kernel32.dll", CharSet=CharSet.Unicode, EntryPoint="GetPrivateProfileString", ExactSpelling=false)]
		private static extern int GetKeyValueA(string strSection, string strKeyName, string strNull, StringBuilder RetVal, int nSize, string strFileName);

		[DllImport("kernel32", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern int GetPrivateProfileString(string section, string key, string defaultValue, [In][Out] char[] actualValue, int size, string ini_path);

		[DllImport("kernel32.dll", CharSet=CharSet.None, EntryPoint="GetPrivateProfileSectionNamesA", ExactSpelling=false)]
		private static extern int GetSectionNamesListA(byte[] lpszReturnBuffer, int nSize, string lpFileName);

		public ArrayList GetSectionsList(string FileName)
		{
			ArrayList arrayLists = new ArrayList();
			byte[] numArray = new byte[1024];
			IniFileIO.GetSectionNamesListA(numArray, (int)numArray.Length, FileName);
			string str = Encoding.Default.GetString(numArray);
			string[] strArrays = str.Split(new char[1]);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				string str1 = strArrays[i];
				if (str1 != string.Empty)
				{
					arrayLists.Add(str1);
				}
			}
			return arrayLists;
		}

		public void WriteValue(string Section, string KeyName, string KeyValue, string FileName)
		{
			IniFileIO.WriteValueA(Section, KeyName, KeyValue, FileName);
		}

		[DllImport("kernel32.dll", CharSet=CharSet.Unicode, EntryPoint="WritePrivateProfileString", ExactSpelling=false)]
		private static extern long WriteValueA(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);
	}
}
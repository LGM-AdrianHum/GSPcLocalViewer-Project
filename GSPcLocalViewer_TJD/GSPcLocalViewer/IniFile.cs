using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace GSPcLocalViewer
{
	public class IniFile
	{
		private IniDictonary<string, string, string> upItems;

		private string sIniPath;

		public IniDictonary<string, string, string> items
		{
			get;
			set;
		}

		public string sIniKey
		{
			get;
			set;
		}

		public IniFile(string sFilePath, string sFileKey)
		{
			this.sIniPath = sFilePath;
			this.sIniKey = sFileKey;
			this.items = new IniDictonary<string, string, string>();
			string end = "";
			string str = "";
			string str1 = "";
			string str2 = "";
			char[] chrArray = new char[] { '\r', '\n' };
			FileStream fileStream = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.None);
			StreamReader streamReader = new StreamReader(fileStream, true);
			end = streamReader.ReadToEnd();
			streamReader.Close();
			fileStream.Close();
			string[] strArrays = end.Split(chrArray, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (strArrays[i].StartsWith("[") && strArrays[i].EndsWith("]"))
				{
					str = strArrays[i].Substring(1, strArrays[i].Length - 2);
				}
				else if (strArrays[i].Contains("="))
				{
					str1 = strArrays[i].Substring(0, strArrays[i].IndexOf("="));
					str2 = strArrays[i].Substring(strArrays[i].IndexOf("=") + 1, strArrays[i].Length - (strArrays[i].IndexOf("=") + 1));
					if (str != "" && str1 != "")
					{
						this.items[str.ToUpper().Trim(), str1.ToUpper().Trim()] = str2.Trim();
					}
				}
			}
		}

		~IniFile()
		{
			if (this.upItems != null)
			{
				IniFileIO iniFileIO = new IniFileIO();
				ArrayList arrayLists = new ArrayList(this.upItems.Values);
				ArrayList arrayLists1 = new ArrayList(this.upItems.Keys);
				for (int i = 0; i < this.upItems.Count; i++)
				{
					SectionKey<string, string> item = (SectionKey<string, string>)arrayLists1[i];
					iniFileIO.WriteValue(item.Key1.ToString(), item.Key2.ToString(), arrayLists[i].ToString(), this.sIniPath);
				}
			}
		}

		public void UpdateItem(string section, string key, string value)
		{
			this.items[section.ToUpper(), key.ToUpper()] = value;
			if (this.upItems == null)
			{
				this.upItems = new IniDictonary<string, string, string>();
			}
			this.upItems[section.ToUpper(), key.ToUpper()] = value;
		}
	}
}
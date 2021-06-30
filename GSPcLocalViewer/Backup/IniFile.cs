// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.IniFile
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Collections;
using System.IO;

namespace GSPcLocalViewer
{
  public class IniFile
  {
    private IniDictonary<string, string, string> upItems;
    private string sIniPath;

    public IniDictonary<string, string, string> items { get; set; }

    public string sIniKey { get; set; }

    public IniFile(string sFilePath, string sFileKey)
    {
      this.sIniPath = sFilePath;
      this.sIniKey = sFileKey;
      this.items = new IniDictonary<string, string, string>();
      string str1 = "";
      char[] separator = new char[2]{ '\r', '\n' };
      FileStream fileStream = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.None);
      StreamReader streamReader = new StreamReader((Stream) fileStream, true);
      string end = streamReader.ReadToEnd();
      streamReader.Close();
      fileStream.Close();
      string[] strArray = end.Split(separator, StringSplitOptions.RemoveEmptyEntries);
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (strArray[index].StartsWith("[") && strArray[index].EndsWith("]"))
          str1 = strArray[index].Substring(1, strArray[index].Length - 2);
        else if (strArray[index].Contains("="))
        {
          string str2 = strArray[index].Substring(0, strArray[index].IndexOf("="));
          string str3 = strArray[index].Substring(strArray[index].IndexOf("=") + 1, strArray[index].Length - (strArray[index].IndexOf("=") + 1));
          if (str1 != "" && str2 != "")
            this.items[str1.ToUpper().Trim(), str2.ToUpper().Trim()] = str3.Trim();
        }
      }
    }

    public void UpdateItem(string section, string key, string value)
    {
      this.items[section.ToUpper(), key.ToUpper()] = value;
      if (this.upItems == null)
        this.upItems = new IniDictonary<string, string, string>();
      this.upItems[section.ToUpper(), key.ToUpper()] = value;
    }

    ~IniFile()
    {
      if (this.upItems == null)
        return;
      IniFileIO iniFileIo = new IniFileIO();
      ArrayList arrayList1 = new ArrayList((ICollection) this.upItems.Values);
      ArrayList arrayList2 = new ArrayList((ICollection) this.upItems.Keys);
      for (int index = 0; index < this.upItems.Count; ++index)
      {
        SectionKey<string, string> sectionKey = (SectionKey<string, string>) arrayList2[index];
        iniFileIo.WriteValue(sectionKey.Key1.ToString(), sectionKey.Key2.ToString(), arrayList1[index].ToString(), this.sIniPath);
      }
    }
  }
}

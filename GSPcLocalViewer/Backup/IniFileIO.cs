// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.IniFileIO
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace GSPcLocalViewer
{
  public class IniFileIO
  {
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string defaultValue, [In, Out] char[] actualValue, int size, string ini_path);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNamesA")]
    private static extern int GetSectionNamesListA(byte[] lpszReturnBuffer, int nSize, string lpFileName);

    [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Unicode)]
    private static extern int GetKeyValueA(string strSection, string strKeyName, string strNull, StringBuilder RetVal, int nSize, string strFileName);

    [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Unicode)]
    private static extern long WriteValueA(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

    public ArrayList GetSectionsList(string FileName)
    {
      ArrayList arrayList = new ArrayList();
      byte[] numArray = new byte[1024];
      IniFileIO.GetSectionNamesListA(numArray, numArray.Length, FileName);
      foreach (string str in Encoding.Default.GetString(numArray).Split(new char[1]))
      {
        if (str != string.Empty)
          arrayList.Add((object) str);
      }
      return arrayList;
    }

    public ArrayList GetKeys(string iniFile, string category)
    {
      char[] charArray = new string(' ', (int) ushort.MaxValue).ToCharArray();
      IniFileIO.GetPrivateProfileString(category, (string) null, (string) null, charArray, 65536, iniFile);
      ArrayList arrayList = new ArrayList((ICollection) new string(charArray).Split(new char[1]));
      arrayList.RemoveRange(arrayList.Count - 2, 2);
      return arrayList;
    }

    public string GetKeyValue(string Section, string KeyName, string FileName)
    {
      StringBuilder RetVal = new StringBuilder((int) byte.MaxValue);
      IniFileIO.GetKeyValueA(Section, KeyName, "", RetVal, (int) byte.MaxValue, FileName);
      return RetVal.ToString();
    }

    public void WriteValue(string Section, string KeyName, string KeyValue, string FileName)
    {
      IniFileIO.WriteValueA(Section, KeyName, KeyValue, FileName);
    }
  }
}

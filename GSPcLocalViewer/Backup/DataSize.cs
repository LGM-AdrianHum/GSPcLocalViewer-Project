// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.DataSize
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.IO;
using System.Text;

namespace GSPcLocalViewer
{
  public static class DataSize
  {
    public static long spaceLeft = 99999999;
    public static long spaceAllowed = 99999999;
    public const long c_spaceBuffer = 10485760;

    public static int miliSecInterval { get; set; }

    static DataSize()
    {
      DataSize.miliSecInterval = 0;
    }

    public static void ReInitialize()
    {
      DataSize.spaceLeft = 99999999L;
      DataSize.spaceAllowed = 99999999L;
      DataSize.miliSecInterval = 0;
    }

    public static bool IsDataSizeApplied()
    {
      string empty = string.Empty;
      if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] != null)
        empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"];
      if (!(empty != string.Empty))
        return false;
      long num = DataSize.FormattedSize(empty);
      return num != 0L && num > 10485760L;
    }

    public static long GetDataSizeLong()
    {
      string empty = string.Empty;
      if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] != null)
        empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"];
      if (!(empty != string.Empty))
        return 0;
      long num = DataSize.FormattedSize(empty);
      if (num != 0L && num > 10485760L)
        return num;
      return 0;
    }

    public static string GetDataSizeString()
    {
      string empty = string.Empty;
      if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] != null)
        empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"];
      if (!(empty != string.Empty))
        return string.Empty;
      long num = DataSize.FormattedSize(empty);
      if (num != 0L && num > 10485760L)
        return empty;
      return string.Empty;
    }

    public static void UpdateSpaceVars()
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        if (Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] == null)
          return;
        string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
        string driveLetter = path.Substring(0, 1);
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        if (Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"] == null)
          return;
        long num = DataSize.FormattedSize(Program.iniGSPcLocal.items["SETTINGS", "DATA_SIZE"]);
        if (num <= 0L)
          return;
        long freeSpace = DataSize.GetFreeSpace(driveLetter);
        long dirSize = DataSize.GetDirSize(new DirectoryInfo(path));
        DataSize.spaceAllowed = num;
        if (num > freeSpace + dirSize)
          num = freeSpace + dirSize;
        DataSize.spaceLeft = num - dirSize;
        if (DataSize.spaceLeft >= 0L)
          return;
        DataSize.spaceLeft = 0L;
      }
      catch
      {
      }
    }

    public static long GetDirSize(DirectoryInfo dir)
    {
      long num = 0;
      DirectoryInfo[] directories = dir.GetDirectories();
      foreach (FileInfo file in dir.GetFiles())
      {
        try
        {
          num += file.Length;
        }
        catch
        {
        }
      }
      foreach (DirectoryInfo dir1 in directories)
      {
        try
        {
          num += DataSize.GetDirSize(dir1);
        }
        catch
        {
        }
      }
      return num;
    }

    public static long GetFreeSpace(string driveLetter)
    {
      return new DriveInfo(driveLetter).AvailableFreeSpace;
    }

    public static string FormattedSize(long size)
    {
      double num1 = (double) size / Math.Pow(1024.0, 3.0);
      if (num1 >= 1.0)
        return Math.Round(num1, 2, MidpointRounding.AwayFromZero).ToString() + " GB";
      double num2 = (double) size / Math.Pow(1024.0, 2.0);
      if (num2 >= 1.0)
        return Math.Round(num2, 2, MidpointRounding.AwayFromZero).ToString() + " MB";
      double num3 = (double) (size / 1024L);
      if (num3 >= 1.0)
        return Math.Round(num3, 2, MidpointRounding.AwayFromZero).ToString() + " KB";
      return size.ToString() + " Bytes";
    }

    public static long FormattedSize(string size)
    {
      string str = size.ToUpper().Trim();
      try
      {
        if (str.EndsWith("GB"))
          return (long) (double.Parse(str.Replace("GB", string.Empty).Trim()) * Math.Pow(1024.0, 3.0));
        if (str.EndsWith("MB"))
          return (long) (double.Parse(str.Replace("MB", string.Empty).Trim()) * Math.Pow(1024.0, 2.0));
        if (str.EndsWith("KB"))
          return (long) (double.Parse(str.Replace("KB", string.Empty).Trim()) * 1024.0);
        if (str.EndsWith("BYTES"))
          return (long) double.Parse(str.Replace("BYTES", string.Empty).Trim());
        return 0;
      }
      catch
      {
        return 0;
      }
    }

    public static string ExtractNumbers(string str, bool allowDecimals)
    {
      char[] charArray = str.ToCharArray();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char c in charArray)
      {
        if (char.IsNumber(c))
          stringBuilder.Append(c);
        else if (allowDecimals && object.Equals((object) c, (object) '.'))
          stringBuilder.Append(c);
      }
      return stringBuilder.ToString();
    }

    public static string ExtractAlphabets(string str, bool allowDots)
    {
      char[] charArray = str.ToCharArray();
      StringBuilder stringBuilder = new StringBuilder();
      foreach (char c in charArray)
      {
        if ((allowDots || !object.Equals((object) c, (object) '.')) && !char.IsNumber(c))
          stringBuilder.Append(c);
      }
      return stringBuilder.ToString();
    }
  }
}

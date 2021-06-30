// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Global
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  internal static class Global
  {
    public static string sApplangEngName = string.Empty;
    private const string dllZipper = "ZIPPER.dll";

    [DllImport("ZIPPER.dll")]
    internal static extern IntPtr UnZipFile(string sFilePath);

    public static DateTime GetLocalDateOfFile(string sFilePath, bool bCompressed, bool bEncrypted)
    {
      try
      {
        if (!File.Exists(sFilePath))
          return new DateTime();
        XmlDocument xmlDocument = new XmlDocument();
        if (bCompressed)
        {
          try
          {
            string str = sFilePath.ToLower().Replace(".zip", ".xml");
            Global.Unzip(sFilePath);
            if (File.Exists(str))
              xmlDocument.Load(str);
          }
          catch
          {
          }
        }
        else
          xmlDocument.Load(sFilePath);
        if (bEncrypted)
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//PDate");
        if (xmlNode != null)
          return DateTime.Parse(xmlNode.InnerText, (IFormatProvider) new CultureInfo("fr-FR", false));
        return File.GetLastWriteTime(sFilePath);
      }
      catch
      {
        if (File.Exists(sFilePath))
          return File.GetLastWriteTime(sFilePath);
        return new DateTime();
      }
    }

    public static DateTime GetLocalDateOfFile(string sFilePath, int iServerId)
    {
      bool flag1 = false;
      bool flag2 = false;
      try
      {
        if (!File.Exists(sFilePath))
          return new DateTime();
        if (Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          flag2 = true;
        if (Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
          flag1 = true;
        XmlDocument xmlDocument = new XmlDocument();
        if (flag1)
        {
          try
          {
            string str = sFilePath.ToLower().Replace(".zip", ".xml");
            Global.Unzip(sFilePath);
            if (File.Exists(str))
              xmlDocument.Load(str);
          }
          catch
          {
          }
        }
        else
          xmlDocument.Load(sFilePath);
        if (flag2)
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//PDate");
        if (xmlNode != null)
          return DateTime.Parse(xmlNode.InnerText, (IFormatProvider) new CultureInfo("fr-FR", false));
        return File.GetLastWriteTime(sFilePath);
      }
      catch
      {
        if (File.Exists(sFilePath))
          return File.GetLastWriteTime(sFilePath);
        return new DateTime();
      }
    }

    public static DateTime GetLocalDateOfFile(string sFilePath)
    {
      try
      {
        if (File.Exists(sFilePath))
          return File.GetLastWriteTime(sFilePath);
        return new DateTime();
      }
      catch
      {
        if (File.Exists(sFilePath))
          return File.GetLastWriteTime(sFilePath);
        return new DateTime();
      }
    }

    public static DateTime GetServerUpdateDateFromXmlNode(XmlNode xSchemaNode, XmlNode objXmlNode)
    {
      try
      {
        string str = string.Empty;
        DateTime dateTime = new DateTime();
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
        {
          if (attribute.Value.ToUpper() == "UPDATEDATE")
            str = attribute.Name;
        }
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) objXmlNode.Attributes)
        {
          if (attribute.Name.Equals(str))
          {
            dateTime = DateTime.Parse(objXmlNode.Attributes[attribute.Name].Value, (IFormatProvider) new CultureInfo("fr-FR", false));
            break;
          }
        }
        return dateTime;
      }
      catch
      {
        return new DateTime();
      }
    }

    public static bool IntervalElapsed(DateTime dtLocal, DateTime dtServer, int iInterval)
    {
      try
      {
        return (dtServer.Date - dtLocal.Date).TotalDays >= (double) iInterval;
      }
      catch
      {
        return true;
      }
    }

    public static DateTime DataUpdateDate(string sDataUpdateFilePath)
    {
      try
      {
        if (!File.Exists(sDataUpdateFilePath))
          return DateTime.Now;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(sDataUpdateFilePath);
        return DateTime.Parse(xmlDocument.SelectSingleNode("//filelastmodified").InnerText, (IFormatProvider) new CultureInfo("fr-FR", false));
      }
      catch
      {
        return DateTime.Now;
      }
    }

    public static bool SecurityLocksOpen(XmlNode bookNode, XmlNode schemaNode, int ServerId, frmViewer frmParent)
    {
      try
      {
        string index1 = string.Empty;
        string[] sLocks1 = (string[]) null;
        string str1 = string.Empty;
        bool flag1 = false;
        AES aes = new AES();
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) schemaNode.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("SECURITYLOCKS"))
            index1 = attribute.Name;
        }
        if (index1 == string.Empty)
          flag1 = true;
        if (bookNode.Attributes[index1] == null || !(bookNode.Attributes[index1].Value != string.Empty))
          return true;
        bool flag2;
        if (bookNode.Attributes[index1].Value != string.Empty)
        {
          string str2 = aes.DLLDecode(bookNode.Attributes[index1].Value, "0123456789ABCDEF");
          if (str2 == string.Empty)
            str2 = bookNode.Attributes[index1].Value;
          sLocks1 = str2.Split(',');
          if (Program.iniServers[ServerId].items["SETTINGS", "SECURITY"] == null)
          {
            flag2 = false;
          }
          else
          {
            string str3 = Program.iniServers[ServerId].items["SETTINGS", "SECURITY"];
            str1 = aes.DLLDecode(str3, "0123456789ABCDEF");
            string[] strArray = str1.Split(',');
            flag2 = false;
            for (int index2 = 0; index2 < strArray.Length; ++index2)
            {
              for (int index3 = 0; index3 < sLocks1.Length; ++index3)
              {
                if (strArray[index2] == sLocks1[index3])
                {
                  flag2 = true;
                  break;
                }
              }
            }
          }
        }
        else
          flag2 = true;
        if (flag2)
          return true;
        frmSecurity frmSecurity = new frmSecurity(sLocks1);
        if (frmSecurity.ShowDialog((IWin32Window) frmParent) != DialogResult.OK)
          return false;
        string str4 = !(str1 == string.Empty) ? str1 + "," + frmSecurity.Key : str1 + frmSecurity.Key;
        string str5 = aes.DLLEncode(str4, "0123456789ABCDEF");
        Program.iniServers[ServerId].UpdateItem("SETTINGS", "SECURITY", str5);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static string GetDGHeaderCellValue(string sSearchScreen, string sKey, string sDefaultHeaderValue, int iServerId)
    {
      try
      {
        string empty = string.Empty;
        IniFileIO iniFileIo = new IniFileIO();
        string FileName = Application.StartupPath + "\\Language XMLs\\" + Settings.Default.appLanguage + "_GSP_" + Program.iniServers[iServerId].sIniKey + ".ini";
        string keyValue = iniFileIo.GetKeyValue(sSearchScreen, sKey.ToUpper(), FileName);
        if (keyValue == null || keyValue == string.Empty)
          return sDefaultHeaderValue;
        return keyValue;
      }
      catch
      {
        return sDefaultHeaderValue;
      }
    }

    public static void SaveToServerIni(string sSearchScreen, string sKey, string sHeaderValue, int iServerId)
    {
      try
      {
        string empty = string.Empty;
        IniFileIO iniFileIo = new IniFileIO();
        string FileName = Application.StartupPath + "\\GSP_" + Program.iniServers[iServerId].sIniKey + ".ini";
        iniFileIo.WriteValue(sSearchScreen, sKey.ToUpper(), sHeaderValue, FileName);
      }
      catch
      {
      }
    }

    public static string GetEngHeaderVal(string sSearchScreen, string sKey, int iServerId)
    {
      string str = string.Empty;
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        string FileName = Application.StartupPath + "\\GSP_" + Program.iniServers[iServerId].sIniKey + ".ini";
        str = iniFileIo.GetKeyValue(sSearchScreen, sKey, FileName);
      }
      catch
      {
      }
      if (str == string.Empty)
        return "||" + sKey;
      return str;
    }

    public static void SaveToLanguageIni(string sSearchScreen, string sKey, string sHeaderValue, int iServerId)
    {
      try
      {
        string empty = string.Empty;
        IniFileIO iniFileIo = new IniFileIO();
        string FileName = Application.StartupPath + "\\Language XMLs\\" + Global.sApplangEngName + "_GSP_" + Program.iniServers[iServerId].sIniKey + ".ini";
        iniFileIo.WriteValue(sSearchScreen, sKey.ToUpper(), sHeaderValue, FileName);
      }
      catch
      {
      }
    }

    public static void AddSearchCol(string sVal, string sKey, string sScreen, int iServerId, ref DataGridView dgViewSearch)
    {
      try
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        string[] strArray = sVal.Split('|');
        if (!(strArray[0].ToString().ToUpper() == "TRUE"))
          return;
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.Name = sKey;
        try
        {
          int num = int.Parse(strArray[3].ToString());
          viewTextBoxColumn.Width = num;
        }
        catch
        {
        }
        string empty = string.Empty;
        if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
          viewTextBoxColumn.HeaderText = Global.GetDGHeaderCellValue(sScreen, sKey, strArray[1], iServerId);
        else
          viewTextBoxColumn.HeaderText = strArray[1];
        try
        {
          string upper = strArray[2].ToUpper();
          if (upper.Equals("L"))
          {
            viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            viewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
          }
          else if (upper.Equals("R"))
          {
            viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            viewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
          }
          else if (upper.Equals("C"))
          {
            viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            viewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
          }
        }
        catch
        {
        }
        dgViewSearch.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
      }
      catch
      {
      }
    }

    public static void SetDGHeaderCellValue(string sSearchScreen, string sKey, string sHeaderValue, int iServerId)
    {
      try
      {
        string empty = string.Empty;
        IniFileIO iniFileIo = new IniFileIO();
        string FileName = Application.StartupPath + "\\Language XMLs\\" + Global.sApplangEngName + "_GSP_" + Program.iniServers[iServerId].sIniKey + ".ini";
        iniFileIo.WriteValue(sSearchScreen, sKey.ToUpper(), sHeaderValue, FileName);
      }
      catch
      {
      }
    }

    public static void ChangeDjVUModifiedDate(string sFilePath, string sDate)
    {
      try
      {
        DateTime exact = DateTime.ParseExact(sDate, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) null);
        int iRetryAttempts = Program.iRetryAttempts;
        int iRetryInterval = Program.iRetryInterval;
        int num = Program.iRetryAttempts != 0 ? Program.iRetryAttempts : 2;
        int millisecondsTimeout = Program.iRetryInterval != 0 ? Program.iRetryInterval : 500;
        bool flag = Global.RetryChangeDjVUModifiedDate(sFilePath, exact);
        if (num <= 1 || flag)
          return;
        for (int index = 0; index < num; ++index)
        {
          Thread.Sleep(millisecondsTimeout);
          if (Global.RetryChangeDjVUModifiedDate(sFilePath, exact))
            break;
        }
      }
      catch
      {
      }
    }

    public static void ChangeDjVUModifiedDate(string sFilePath, DateTime dServerDate)
    {
      try
      {
        int iRetryAttempts = Program.iRetryAttempts;
        int iRetryInterval = Program.iRetryInterval;
        int num = Program.iRetryAttempts != 0 ? Program.iRetryAttempts : 2;
        int millisecondsTimeout = Program.iRetryInterval != 0 ? Program.iRetryInterval : 500;
        bool flag = Global.RetryChangeDjVUModifiedDate(sFilePath, dServerDate);
        if (num <= 1 || flag)
          return;
        for (int index = 0; index < num; ++index)
        {
          Thread.Sleep(millisecondsTimeout);
          if (Global.RetryChangeDjVUModifiedDate(sFilePath, dServerDate))
            break;
        }
      }
      catch
      {
      }
    }

    private static bool RetryChangeDjVUModifiedDate(string sFilePath, DateTime dServerDate)
    {
      try
      {
        File.SetLastWriteTime(sFilePath, dServerDate);
        return true;
      }
      catch
      {
        return false;
      }
    }

    public static void Unzip(string sZipFilePath)
    {
      try
      {
        if (File.Exists(sZipFilePath.ToLower().Replace(".zip", ".xml")))
          return;
        int num = Program.iRetryAttempts != 0 ? Program.iRetryAttempts : 2;
        int millisecondsTimeout = Program.iRetryInterval != 0 ? Program.iRetryInterval : 500;
        if (Path.GetExtension(sZipFilePath).ToLower() != ".zip")
          return;
        for (int index = 0; index < num; ++index)
        {
          try
          {
            if (!File.Exists(sZipFilePath) || Global.UnZipFile(sZipFilePath).ToInt32() == 1 && File.Exists(sZipFilePath.ToLower().Replace(".zip", ".xml")))
              break;
            Thread.Sleep(millisecondsTimeout);
          }
          catch
          {
          }
        }
      }
      catch
      {
      }
    }

    public struct OkMsg
    {
      public bool ok;
      public string msg;

      public OkMsg(bool ok, string msg)
      {
        this.ok = ok;
        this.msg = msg;
      }
    }

    public class ComboBoxItem
    {
      private string _Contents;
      private object _Tag;

      public string Contents
      {
        get
        {
          return this._Contents;
        }
        set
        {
          this._Contents = value;
        }
      }

      public object Tag
      {
        get
        {
          return this._Tag;
        }
        set
        {
          this._Tag = value;
        }
      }

      public ComboBoxItem(string contents, object tag)
      {
        this._Contents = contents;
        this._Tag = tag;
      }

      public override string ToString()
      {
        return this._Contents;
      }
    }
  }
}

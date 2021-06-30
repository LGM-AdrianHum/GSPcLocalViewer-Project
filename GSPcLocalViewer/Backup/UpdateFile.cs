// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.UpdateFile
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  internal class UpdateFile
  {
    public string sFileName;
    public string sSourcePath;
    public string sTargetPath;
    public string sLastModifiedDate;
    public UpdateFile.Operations objOperation;
    public Version iVersion;
    public bool bForceFullUpdate;
    private string sFileExtension;

    public UpdateFile(XmlNode xFileNode)
    {
      this.sFileName = string.Empty;
      this.sSourcePath = string.Empty;
      this.sTargetPath = string.Empty;
      this.sLastModifiedDate = string.Empty;
      this.iVersion = (Version) null;
      this.bForceFullUpdate = false;
      this.objOperation = new UpdateFile.Operations();
      this.sFileExtension = string.Empty;
      try
      {
        if (xFileNode.Attributes["name"] != null)
          this.sFileName = xFileNode.Attributes["name"].Value;
        foreach (XmlNode childNode in xFileNode.ChildNodes)
        {
          if (childNode.Name.ToUpper().Equals("SOURCEPATH"))
          {
            if (!childNode.InnerText.Contains("://"))
            {
              this.sSourcePath = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
              this.sSourcePath = this.sSourcePath.Substring(0, this.sSourcePath.LastIndexOf("/"));
              UpdateFile updateFile = this;
              updateFile.sSourcePath = updateFile.sSourcePath + "/" + childNode.InnerText;
            }
            else
              this.sSourcePath = childNode.InnerText;
          }
          if (childNode.Name.ToUpper().Equals("DESTINATIONPATH"))
          {
            try
            {
              this.sTargetPath = childNode.InnerText;
              this.sTargetPath = this.sTargetPath.Replace("/", "\\");
            }
            catch
            {
            }
          }
          if (childNode.Name.ToUpper().Equals("LASTMODIFIED"))
            this.sLastModifiedDate = childNode.InnerText;
          if (childNode.Name.ToUpper().Equals("VERSION"))
          {
            try
            {
              this.iVersion = new Version(childNode.InnerText.Replace(".", string.Empty));
            }
            catch
            {
              this.iVersion = new Version();
            }
          }
          if (childNode.Name.ToUpper().Equals("FORCEFULLYUPDATE"))
            this.bForceFullUpdate = bool.Parse(childNode.InnerText);
          if (childNode.Name.ToUpper().Equals("OPERATIONS"))
          {
            string str = childNode.InnerText;
            if (str.Length != 8)
            {
              int num = 8 - str.Length;
              for (int index = 0; index < num; ++index)
                str = "0" + str;
            }
            char[] charArray = str.ToCharArray();
            this.objOperation.Activate = charArray[charArray.Length - 1] == '1';
            this.objOperation.Execute = charArray[charArray.Length - 2] == '1';
            this.objOperation.Delete = charArray[charArray.Length - 3] == '1';
            this.objOperation.Register = charArray[charArray.Length - 4] == '1';
            this.objOperation.UnRegister = charArray[charArray.Length - 5] == '1';
            this.objOperation.Update = charArray[charArray.Length - 6] == '1';
            this.sFileExtension = new FileInfo(this.sFileName).Extension.ToUpper();
            if (this.sFileExtension.Equals(".OCX") && this.objOperation.Delete)
              this.objOperation.UnRegister = true;
            if (this.sFileExtension.Equals(".OCX") && this.objOperation.Activate)
              this.objOperation.Register = true;
            if ((this.sFileExtension.Equals(".INI") || this.sFileExtension.Equals(".CSV") || this.sFileExtension.Equals(".TXT")) && (this.objOperation.Activate && this.objOperation.Update))
            {
              if (File.Exists(this.sTargetPath))
                this.objOperation.Activate = false;
              else
                this.objOperation.Update = false;
            }
            if (this.sFileExtension.Equals(".EXE"))
              this.objOperation.Update = false;
          }
        }
      }
      catch
      {
      }
    }

    public Version GetVersion(string sfileName)
    {
      try
      {
        return new Version(FileVersionInfo.GetVersionInfo(sfileName).FileVersion);
      }
      catch
      {
        return this.iVersion;
      }
    }

    public bool fDownloadRequired()
    {
      try
      {
        TimeSpan timeSpan = new TimeSpan();
        if (!this.sTargetPath.Contains(":\\"))
          this.sTargetPath = Application.StartupPath + "\\" + this.sTargetPath;
        if (File.Exists(this.sTargetPath))
        {
          try
          {
            timeSpan = DateTime.Parse(this.sLastModifiedDate, (IFormatProvider) new CultureInfo("fr-FR", false)) - File.GetLastWriteTime(this.sTargetPath);
          }
          catch
          {
          }
        }
        Version version = this.GetVersion(this.sTargetPath);
        return !File.Exists(this.sTargetPath) && this.objOperation.Activate || File.Exists(this.sTargetPath) && timeSpan.TotalSeconds > 0.0 || (this.bForceFullUpdate || version < this.iVersion);
      }
      catch
      {
        return false;
      }
    }

    public class Operations
    {
      public bool Activate;
      public bool Execute;
      public bool Delete;
      public bool Register;
      public bool UnRegister;
      public bool Update;
    }
  }
}

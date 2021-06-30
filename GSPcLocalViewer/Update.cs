// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Update
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class Update
  {
    private const string c_UpdateManager = "UpdateManager";
    private string sUpdateManageFolder;
    private XmlDocument xUpdateXml;
    private XmlNodeList xnlFileList;
    public string sAppFolderPath;
    private Download objDownloader;

    public Update()
    {
      this.objDownloader = new Download();
    }

    private bool ApplicationFolderExist()
    {
      this.sAppFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      this.sAppFolderPath += "\\UpdateManager";
      try
      {
        if (Directory.Exists(this.sAppFolderPath) || Directory.CreateDirectory(this.sAppFolderPath).Exists)
          return true;
        int num = (int) MessageBox.Show("Cannot create application folder at path: " + this.sAppFolderPath, "Update Manager", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      catch
      {
        int num = (int) MessageBox.Show("Cannot create application folder at path: " + this.sAppFolderPath, "Update Manager", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
    }

    public bool CheckApplicationUpdates(string[] args)
    {
      bool flag1 = false;
      foreach (string str in args)
      {
        if (str.ToUpper().Equals("VIEWERISUPDATED"))
          return false;
      }
      if (!this.ApplicationFolderExist())
        return false;
      bool flag2 = false;
      string surl1;
      string str1;
      string name;
      try
      {
        surl1 = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
        str1 = surl1.Substring(0, surl1.LastIndexOf("/"));
        name = new FileInfo(surl1.Substring(surl1.LastIndexOf("/"))).Name;
      }
      catch
      {
        return false;
      }
      if (surl1 != null && str1 != null && this.UpdateManagerFolderExist())
      {
        if (this.objDownloader.DownloadFile(surl1, this.sAppFolderPath + "\\" + name))
        {
          flag1 = true;
          File.Copy(this.sAppFolderPath + "\\" + name, Application.StartupPath + "\\" + name, true);
        }
        if (!flag1)
          return false;
        if (File.Exists(Application.StartupPath + "\\" + name) && this.LoadUpdateXML(Application.StartupPath + "\\" + name))
        {
          this.xnlFileList = this.xUpdateXml.SelectNodes("//file");
          foreach (XmlNode xnlFile in this.xnlFileList)
          {
            UpdateFile updateFile = new UpdateFile(xnlFile);
            if (!(xnlFile.Attributes["name"].Value.ToUpper() == "FILEDOWNLOADERDLL.DLL"))
            {
              if (xnlFile.Attributes["name"].Value.ToUpper() != "UPDATEMANAGER.EXE" && !xnlFile.Attributes["name"].Value.ToUpper().Contains(".INI"))
              {
                if (updateFile.fDownloadRequired())
                  flag2 = true;
              }
              else if (xnlFile.Attributes["name"].Value.ToUpper().Contains(".INI"))
              {
                if (xnlFile.Attributes["name"].Value.ToUpper() == Program.iniGSPcLocal.sIniKey + ".INI")
                {
                  string s = Program.iniGSPcLocal.items["INI_INFO", "LAST_MODIFIED_DATE"];
                  if (s == null)
                  {
                    flag2 = true;
                  }
                  else
                  {
                    try
                    {
                      TimeSpan timeSpan = new TimeSpan();
                      if ((DateTime.Parse(updateFile.sLastModifiedDate, (IFormatProvider) new CultureInfo("fr-FR", false)) - DateTime.Parse(s, (IFormatProvider) new CultureInfo("fr-FR", false))).TotalSeconds > 0.0)
                        flag2 = true;
                    }
                    catch
                    {
                    }
                  }
                }
                else if (xnlFile.Attributes["name"].Value.ToUpper() == Program.iniUserSet.sIniKey + ".INI")
                {
                  string s = Program.iniUserSet.items["INI_INFO", "LAST_MODIFIED_DATE"];
                  if (s == null)
                  {
                    flag2 = true;
                  }
                  else
                  {
                    try
                    {
                      TimeSpan timeSpan = new TimeSpan();
                      if ((DateTime.Parse(updateFile.sLastModifiedDate, (IFormatProvider) new CultureInfo("fr-FR", false)) - DateTime.Parse(s, (IFormatProvider) new CultureInfo("fr-FR", false))).TotalSeconds > 0.0)
                        flag2 = true;
                    }
                    catch
                    {
                    }
                  }
                }
                else
                {
                  foreach (IniFile iniServer in Program.iniServers)
                  {
                    if (xnlFile.Attributes["name"].Value.ToUpper() == "GSP_" + iniServer.sIniKey + ".INI")
                    {
                      string s = iniServer.items["INI_INFO", "LAST_MODIFIED_DATE"];
                      if (s == null)
                      {
                        flag2 = true;
                      }
                      else
                      {
                        try
                        {
                          TimeSpan timeSpan = new TimeSpan();
                          if ((DateTime.Parse(updateFile.sLastModifiedDate, (IFormatProvider) new CultureInfo("fr-FR", false)) - DateTime.Parse(s, (IFormatProvider) new CultureInfo("fr-FR", false))).TotalSeconds > 0.0)
                            flag2 = true;
                        }
                        catch
                        {
                        }
                      }
                    }
                  }
                }
              }
              else if (updateFile.fDownloadRequired())
              {
                if (this.objDownloader.DownloadFile(updateFile.sSourcePath, this.sAppFolderPath + "\\" + updateFile.sFileName))
                {
                  try
                  {
                    File.Copy(this.sAppFolderPath + "\\" + updateFile.sFileName, Application.StartupPath + "\\" + updateFile.sFileName, true);
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
        }
      }
      return flag2 && File.Exists(Application.StartupPath + "\\UpdateManager.EXE");
    }

    private bool UpdateManagerFolderExist()
    {
      this.sUpdateManageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
      this.sUpdateManageFolder += "\\UpdateManager";
      try
      {
        return Directory.Exists(this.sUpdateManageFolder) || Directory.CreateDirectory(this.sUpdateManageFolder).Exists;
      }
      catch
      {
        return false;
      }
    }

    private bool LoadUpdateXML(string sUpdateFileLocalPath)
    {
      try
      {
        if (!File.Exists(sUpdateFileLocalPath))
          return false;
        this.xUpdateXml = new XmlDocument();
        this.xUpdateXml.Load(sUpdateFileLocalPath);
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}

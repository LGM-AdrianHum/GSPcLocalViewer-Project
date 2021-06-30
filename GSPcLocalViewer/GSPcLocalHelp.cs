// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.GSPcLocalHelp
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class GSPcLocalHelp
  {
    private string sHelpFileName = string.Empty;
    private string sHelpFileServerPath = string.Empty;
    private string sHelpFileLocalPath = string.Empty;
    private string sHelpUrlPath = string.Empty;
    private string sHelpType = "URL";
    private frmViewer frmParent;
    private Download objDownloader;

    public GSPcLocalHelp(frmViewer objFrmViewer)
    {
      try
      {
        this.frmParent = objFrmViewer;
        this.objDownloader = new Download();
      }
      catch
      {
      }
    }

    private void GetHelpFileType()
    {
      try
      {
        if (Program.iniGSPcLocal.items["HELP_FILE", "TYPE"] == null)
          return;
        if (Program.iniGSPcLocal.items["HELP_FILE", "TYPE"].ToUpper() == "FILE")
          this.sHelpType = "FILE";
        else
          this.sHelpType = "URL";
      }
      catch
      {
      }
    }

    private void DownloadHelpFile()
    {
      try
      {
        if (File.Exists(this.sHelpFileLocalPath))
          return;
        this.objDownloader.DownloadFile(this.sHelpFileServerPath, this.sHelpFileLocalPath);
      }
      catch
      {
      }
    }

    private void InitializePaths()
    {
      try
      {
        if (Program.iniGSPcLocal.items["HELP_FILE", "FILENAME"] != null)
        {
          this.sHelpFileName = Program.iniGSPcLocal.items["HELP_FILE", "FILENAME"];
          this.sHelpFileLocalPath = Application.StartupPath + "\\" + this.sHelpFileName;
          this.sHelpFileServerPath = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
          this.sHelpFileServerPath = this.sHelpFileServerPath.Substring(0, this.sHelpFileServerPath.LastIndexOf("/")) + "/" + this.sHelpFileName;
          this.sHelpUrlPath = Program.iniGSPcLocal.items["HELP_FILE", "URL"];
        }
        else
          this.sHelpUrlPath = Program.iniGSPcLocal.items["HELP_FILE", "URL"];
      }
      catch
      {
      }
    }

    public void OpenHelpFile()
    {
      try
      {
        this.GetHelpFileType();
        this.InitializePaths();
        if (this.sHelpType.ToUpper() == "FILE")
        {
          this.DownloadHelpFile();
          if (!File.Exists(this.sHelpFileLocalPath))
            return;
          Process.Start(this.sHelpFileLocalPath);
        }
        else
          this.frmParent.OpenSpecifiedBrowser(this.sHelpUrlPath);
      }
      catch
      {
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Download
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using FileDownloaderDLL;
using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  internal class Download
  {
    private frmViewer frmViewer;
    private frmOpenBook frmOpenBook;
    private Control objParent;
    public XmlDocument xmlDocument;

    public Download(frmViewer objFrmViewer)
    {
      this.frmViewer = objFrmViewer;
      this.frmOpenBook = (frmOpenBook) null;
    }

    public Download(frmOpenBook objFrmOpenBook)
    {
      this.frmOpenBook = objFrmOpenBook;
      this.frmViewer = (frmViewer) null;
    }

    public Download()
    {
      this.frmOpenBook = (frmOpenBook) null;
      this.frmViewer = (frmViewer) null;
    }

    private bool RetryDownloadFile(string surl1, string sLocalPath)
    {
      if (DataSize.spaceLeft < 10485760L && !Path.GetFileName(surl1).ToLower().Equals("dataupdate.xml"))
      {
        if (this.frmViewer != null)
          this.frmViewer.ShowNotification();
        else if (this.frmOpenBook != null)
          this.frmOpenBook.frmParent.ShowNotification();
        return false;
      }
      FileDownloader fileDownloader = new FileDownloader();
      try
      {
        if (Settings.Default.appProxyType == "0")
        {
          Application.DoEvents();
          if (fileDownloader.DownloadingFile(surl1, sLocalPath, Settings.Default.appProxyTimeOut + "000") == 1)
            return false;
          long fileLength = (long) fileDownloader.FileLength;
          long bytesDownloaded;
          do
          {
            Application.DoEvents();
            bytesDownloaded = (long) fileDownloader.BytesDownloaded;
            int num = (int) (bytesDownloaded * 100L / fileLength);
            if (this.frmViewer != null)
              this.frmViewer.UpdateStatus(num.ToString() + "% Downloaded");
            if (this.frmOpenBook != null)
              this.frmOpenBook.UpdateStatus(num.ToString() + "% Downloaded");
            Application.DoEvents();
          }
          while (bytesDownloaded < fileLength);
          return true;
        }
        Program.bAvoidAppUpdateXML = surl1.ToUpper().EndsWith("APPUPDATE.XML");
        switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
        {
          case 1:
            return false;
          case 407:
            Program.bShowProxyScreen = true;
            if (!Dashbord.bShowProxy || Program.bAvoidAppUpdateXML)
              return false;
            this.ShowProxyAuthWarning();
            while (Program.bShowProxyScreen && !Program.bAvoidAppUpdateXML)
            {
              switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
              {
                case 1:
                  Program.bShowProxyScreen = false;
                  return false;
                case 407:
                  if (!Dashbord.bShowProxy)
                    return false;
                  this.ShowProxyAuthWarning();
                  continue;
                default:
                  Program.bShowProxyScreen = false;
                  long fileLength1 = (long) fileDownloader.FileLength;
                  long bytesDownloaded1;
                  do
                  {
                    bytesDownloaded1 = (long) fileDownloader.BytesDownloaded;
                    int num = (int) (bytesDownloaded1 * 100L / fileLength1);
                    if (this.frmViewer != null)
                      this.frmViewer.UpdateStatus(num.ToString() + "% Downloaded");
                    if (this.frmOpenBook != null)
                      this.frmOpenBook.UpdateStatus(num.ToString() + "% Downloaded");
                    Application.DoEvents();
                  }
                  while (bytesDownloaded1 < fileLength1);
                  return true;
              }
            }
            return false;
          default:
            long fileLength2 = (long) fileDownloader.FileLength;
            long bytesDownloaded2;
            do
            {
              bytesDownloaded2 = (long) fileDownloader.BytesDownloaded;
              int num = (int) (bytesDownloaded2 * 100L / fileLength2);
              if (this.frmViewer != null)
                this.frmViewer.UpdateStatus(num.ToString() + "% Downloaded");
              if (this.frmOpenBook != null)
                this.frmOpenBook.UpdateStatus(num.ToString() + "% Downloaded");
              Application.DoEvents();
            }
            while (bytesDownloaded2 < fileLength2);
            return true;
        }
      }
      catch
      {
        try
        {
          fileDownloader.StopDownloadingFile();
        }
        catch
        {
        }
        return false;
      }
    }

    public bool DownloadFile(string surl1, string sLocalPath)
    {
      int iRetryAttempts = Program.iRetryAttempts;
      int iRetryInterval = Program.iRetryInterval;
      bool flag = this.RetryDownloadFile(surl1, sLocalPath);
      try
      {
        if (flag)
        {
          if (Path.GetExtension(sLocalPath).ToLower() == ".zip")
          {
            string path = sLocalPath.ToLower().Replace(".zip", ".xml");
            try
            {
              if (File.Exists(path))
                File.Delete(path);
            }
            catch
            {
            }
            try
            {
              Global.Unzip(sLocalPath);
            }
            catch
            {
            }
          }
        }
      }
      catch
      {
      }
      if (iRetryAttempts > 1 && !flag)
      {
        for (int index = 0; index < iRetryAttempts; ++index)
        {
          Thread.Sleep(iRetryInterval);
          flag = this.RetryDownloadFile(surl1, sLocalPath);
          if (flag)
          {
            try
            {
              if (Path.GetExtension(sLocalPath).ToLower() == ".zip")
              {
                Global.Unzip(sLocalPath);
                break;
              }
              break;
            }
            catch
            {
              break;
            }
          }
        }
      }
      return flag;
    }

    public bool DownloadFile(string surl1, string sLocalPath, string sProxyType, string sProxyIP, string sProxyPort, string sProxyLogin, string sProxyPassword, string sProxyTimeOut)
    {
      FileDownloader fileDownloader = new FileDownloader();
      try
      {
        if (sProxyType == "0")
        {
          if (fileDownloader.DownloadingFile(surl1, sLocalPath, sProxyTimeOut + "000") == 1)
            return false;
          long fileLength = (long) fileDownloader.FileLength;
          long bytesDownloaded;
          do
          {
            Application.DoEvents();
            bytesDownloaded = (long) fileDownloader.BytesDownloaded;
            Application.DoEvents();
          }
          while (bytesDownloaded < fileLength);
          return true;
        }
        switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
        {
          case 1:
            return false;
          case 407:
            try
            {
              Program.bAvoidAppUpdateXML = surl1.ToUpper().EndsWith("APPUPDATE.XML");
              Program.bShowProxyScreen = true;
              if (!Dashbord.bShowProxy || Program.bAvoidAppUpdateXML)
                return false;
              this.ShowProxyAuthWarning();
              while (Program.bShowProxyScreen)
              {
                if (!Program.bAvoidAppUpdateXML)
                {
                  switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
                  {
                    case 1:
                      Program.bShowProxyScreen = false;
                      return false;
                    case 407:
                      if (!Dashbord.bShowProxy)
                        return false;
                      this.ShowProxyAuthWarning();
                      continue;
                    default:
                      Program.bShowProxyScreen = false;
                      long fileLength = (long) fileDownloader.FileLength;
                      long bytesDownloaded;
                      do
                      {
                        Application.DoEvents();
                        bytesDownloaded = (long) fileDownloader.BytesDownloaded;
                        Application.DoEvents();
                      }
                      while (bytesDownloaded < fileLength);
                      return true;
                  }
                }
                else
                  break;
              }
            }
            catch (Exception ex)
            {
              return false;
            }
            return true;
          default:
            long fileLength1 = (long) fileDownloader.FileLength;
            long bytesDownloaded1;
            do
            {
              Application.DoEvents();
              bytesDownloaded1 = (long) fileDownloader.BytesDownloaded;
              Application.DoEvents();
            }
            while (bytesDownloaded1 < fileLength1);
            return true;
        }
      }
      catch
      {
        try
        {
          fileDownloader.StopDownloadingFile();
        }
        catch
        {
        }
        return false;
      }
    }

    private void ShowProxyAuthWarning()
    {
      this.LoadXMLFirstTime();
      this.objParent = this.frmViewer == null ? (Control) this.frmOpenBook : (Control) this.frmViewer;
      if (this.objParent == null)
        return;
      if (this.objParent.InvokeRequired)
        this.objParent.Invoke((Delegate) new Download.ShowProxyAuthWarningDelegate(this.ShowProxyAuthWarning));
      else
        this.ShowProxyAuthenticationError();
    }

    private void ShowProxyAuthenticationError()
    {
      int num = (int) new frmProxyAuthentication().ShowDialog();
    }

    public string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string xQuery1 = "" + "/Screen[@Name='MAIN_FORM']";
        switch (rType)
        {
          case ResourceType.BUTTON:
            xQuery1 += "/Resources[@Name='BUTTON']";
            break;
          case ResourceType.LABEL:
            xQuery1 += "/Resources[@Name='LABEL']";
            break;
          case ResourceType.CHECK_BOX:
            xQuery1 += "/Resources[@Name='CHECK_BOX']";
            break;
          case ResourceType.RADIO_BUTTON:
            xQuery1 += "/Resources[@Name='RADIO_BUTTON']";
            break;
          case ResourceType.TITLE:
            return this.GetResourceValue(sDefaultValue, xQuery1);
          case ResourceType.COMBO_BOX:
            xQuery1 += "/Resources[@Name='COMBO_BOX']";
            break;
          case ResourceType.LIST_VIEW:
            xQuery1 += "/Resources[@Name='LIST_VIEW']";
            break;
          case ResourceType.GRID_VIEW:
            xQuery1 += "/Resources[@Name='GRID_VIEW']";
            break;
          case ResourceType.TOOLSTRIP:
            xQuery1 += "/Resources[@Name='TOOLSTRIP']";
            break;
          case ResourceType.MENU_BAR:
            xQuery1 += "/Resources[@Name='MENU_BAR']";
            break;
          case ResourceType.CONTEXT_MENU:
            xQuery1 += "/Resources[@Name='CONTEXT_MENU']";
            break;
          case ResourceType.STATUS_MESSAGE:
            xQuery1 += "/Resources[@Name='STATUS_MESSAGE']";
            break;
          case ResourceType.POPUP_MESSAGE:
            xQuery1 += "/Resources[@Name='POPUP_MESSAGE']";
            break;
        }
        string xQuery2 = xQuery1 + "/Resource[@Name='" + sKey + "']";
        return this.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    public string GetResourceValue(string sDefaultValue, string xQuery)
    {
      try
      {
        XmlNode xmlNode = this.xmlDocument.SelectSingleNode("/GSPcLocalViewer" + xQuery);
        if (xmlNode == null || this.xmlDocument == null || (xmlNode.Attributes.Count <= 0 || xmlNode.Attributes["Value"] == null))
          return sDefaultValue;
        string str = xmlNode.Attributes["Value"].Value;
        if (!string.IsNullOrEmpty(str))
          return str;
        return sDefaultValue;
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    public void LoadXMLFirstTime()
    {
      this.xmlDocument = (XmlDocument) null;
      try
      {
        if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
        {
          this.GetOSLanguage();
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            try
            {
              if (File.Exists(files[index]))
              {
                int num1 = files[index].IndexOf(".xml");
                int num2 = files[index].LastIndexOf("\\");
                string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                string str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value;
                if (str3.Length != 0)
                {
                  if (str3 != null)
                  {
                    if (str3 == Settings.Default.appLanguage)
                    {
                      if (File.Exists(str2))
                      {
                        this.xmlDocument = new XmlDocument();
                        this.xmlDocument.Load(str2);
                        break;
                      }
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
        else
        {
          if (Settings.Default.appLanguage != null)
          {
            if (Settings.Default.appLanguage.Length != 0)
              goto label_27;
          }
          Settings.Default.appLanguage = "English";
          string osLanguage = this.GetOSLanguage();
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            try
            {
              if (File.Exists(files[index]))
              {
                int num1 = files[index].IndexOf(".xml");
                int num2 = files[index].LastIndexOf("\\");
                string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                string str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value;
                if (str3 == osLanguage)
                {
                  if (File.Exists(str2))
                  {
                    this.xmlDocument.Load(str2);
                    break;
                  }
                }
                else if (str3 == Settings.Default.appLanguage)
                {
                  if (File.Exists(str2))
                  {
                    this.xmlDocument.Load(str2);
                    break;
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
      catch
      {
      }
label_27:
      if (this.xmlDocument != null)
        return;
      this.xmlDocument = new XmlDocument();
      Settings.Default.appLanguage = "English";
    }

    public string GetOSLanguage()
    {
      CultureInfo.CurrentCulture.ClearCachedData();
      return CultureInfo.CurrentCulture.DisplayName.Split(' ')[0].ToString();
    }

    private delegate void ShowProxyAuthWarningDelegate();

    private delegate void ShowProxyAuthenticationDelegate();
  }
}

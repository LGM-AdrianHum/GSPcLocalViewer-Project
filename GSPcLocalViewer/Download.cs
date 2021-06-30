// NAME : Adrian Hum (Casandra)
// FILE : GSPcLocalViewer/GSPcLocalViewer [Download.cs]
// ----------------------------------------------------------------------------------------------------------
// Created  :  2018-08-06  11:57 AM
// Modified :  2018-08-07  10:22 AM
// ----------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using FileDownloaderDLL;
using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using NLog;

namespace GSPcLocalViewer
{
    internal class Download
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly frmOpenBook frmOpenBook;
        private readonly frmViewer frmViewer;
        private Control objParent;
        public XmlDocument xmlDocument;

        public Download(frmViewer objFrmViewer)
        {
            frmViewer = objFrmViewer;
            frmOpenBook = (frmOpenBook) null;
        }

        public Download(frmOpenBook objFrmOpenBook)
        {
            frmOpenBook = objFrmOpenBook;
            frmViewer = (frmViewer) null;
        }

        public Download()
        {
            frmOpenBook = (frmOpenBook) null;
            frmViewer = (frmViewer) null;
        }

        private bool RetryDownloadFile(string surl1, string sLocalPath)
        {
            Logger.Debug($"{surl1} into {sLocalPath}");

            if (DataSize.spaceLeft < 10485760L && !Path.GetFileName(surl1).ToLower().Equals("dataupdate.xml"))
            {
                if (frmViewer != null)
                    frmViewer.ShowNotification();
                else
                    frmOpenBook?.frmParent.ShowNotification();

                return false;
            }

            var fileDownloader = new FileDownloader();
            try
            {
                if (Settings.Default.appProxyType == "0")
                {
                    Application.DoEvents();
                    if (fileDownloader.DownloadingFile(surl1, sLocalPath, Settings.Default.appProxyTimeOut + "000") ==
                        1)
                        return false;
                    var fileLength = (long) fileDownloader.FileLength;
                    long bytesDownloaded;
                    do
                    {
                        Application.DoEvents();
                        bytesDownloaded = (long) fileDownloader.BytesDownloaded;
                        var num = (int) (bytesDownloaded * 100L / fileLength);
                        frmViewer?.UpdateStatus(num.ToString() + "% Downloaded");
                        frmOpenBook?.UpdateStatus(num.ToString() + "% Downloaded");
                        Application.DoEvents();
                    } while (bytesDownloaded < fileLength);

                    return true;
                }

                Program.bAvoidAppUpdateXML = surl1.ToUpper().EndsWith("APPUPDATE.XML");
                switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP,
                    Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword,
                    Settings.Default.appProxyTimeOut + "000"))
                {
                    case 1:
                        return false;
                    case 407:
                        Program.bShowProxyScreen = true;
                        if (!Dashbord.bShowProxy || Program.bAvoidAppUpdateXML)
                            return false;
                        ShowProxyAuthWarning();
                        while (Program.bShowProxyScreen && !Program.bAvoidAppUpdateXML)
                            switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath,
                                Settings.Default.appProxyIP, Settings.Default.appProxyPort,
                                Settings.Default.appProxyLogin, Settings.Default.appProxyPassword,
                                Settings.Default.appProxyTimeOut + "000"))
                            {
                                case 1:
                                    Program.bShowProxyScreen = false;
                                    return false;
                                case 407:
                                    if (!Dashbord.bShowProxy)
                                        return false;
                                    ShowProxyAuthWarning();
                                    continue;
                                default:
                                    Program.bShowProxyScreen = false;
                                    var fileLength1 = (long) fileDownloader.FileLength;
                                    long bytesDownloaded1;
                                    do
                                    {
                                        bytesDownloaded1 = (long) fileDownloader.BytesDownloaded;
                                        var num = (int) (bytesDownloaded1 * 100L / fileLength1);
                                        frmViewer?.UpdateStatus(num.ToString() + "% Downloaded");
                                        frmOpenBook?.UpdateStatus(num.ToString() + "% Downloaded");
                                        Application.DoEvents();
                                    } while (bytesDownloaded1 < fileLength1);

                                    return true;
                            }
                        return false;
                    default:
                        var fileLength2 = (long) fileDownloader.FileLength;
                        long bytesDownloaded2;
                        do
                        {
                            bytesDownloaded2 = (long) fileDownloader.BytesDownloaded;
                            var num = (int) (bytesDownloaded2 * 100L / fileLength2);
                            frmViewer?.UpdateStatus(num.ToString() + "% Downloaded");
                            frmOpenBook?.UpdateStatus(num.ToString() + "% Downloaded");
                            Application.DoEvents();
                        } while (bytesDownloaded2 < fileLength2);

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
            var iRetryAttempts = Program.iRetryAttempts;
            var iRetryInterval = Program.iRetryInterval;
            var flag = RetryDownloadFile(surl1, sLocalPath);
            try
            {
                if (flag)
                    if (Path.GetExtension(sLocalPath).ToLower() == ".zip")
                    {
                        
                        var path = sLocalPath.ToLower().Replace(".zip", ".xml");
                        Logger.Debug($"Unpacking {sLocalPath} into {path}");
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
            catch
            {
            }

            if (iRetryAttempts > 1 && !flag)
                for (var index = 0; index < iRetryAttempts; ++index)
                {
                    Thread.Sleep(iRetryInterval);
                    flag = RetryDownloadFile(surl1, sLocalPath);
                    if (flag)
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

            return flag;
        }

        public bool DownloadFile(string surl1, string sLocalPath, string sProxyType, string sProxyIP, string sProxyPort,
            string sProxyLogin, string sProxyPassword, string sProxyTimeOut)
        {
            var fileDownloader = new FileDownloader();
            try
            {
                if (sProxyType == "0")
                {
                    if (fileDownloader.DownloadingFile(surl1, sLocalPath, sProxyTimeOut + "000") == 1)
                        return false;
                    var fileLength = (long) fileDownloader.FileLength;
                    long bytesDownloaded;
                    do
                    {
                        Application.DoEvents();
                        bytesDownloaded = (long) fileDownloader.BytesDownloaded;
                        Application.DoEvents();
                    } while (bytesDownloaded < fileLength);

                    return true;
                }

                switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP,
                    Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword,
                    Settings.Default.appProxyTimeOut + "000"))
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
                            ShowProxyAuthWarning();
                            while (Program.bShowProxyScreen)
                                if (!Program.bAvoidAppUpdateXML)
                                    switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath,
                                        Settings.Default.appProxyIP, Settings.Default.appProxyPort,
                                        Settings.Default.appProxyLogin, Settings.Default.appProxyPassword,
                                        Settings.Default.appProxyTimeOut + "000"))
                                    {
                                        case 1:
                                            Program.bShowProxyScreen = false;
                                            return false;
                                        case 407:
                                            if (!Dashbord.bShowProxy)
                                                return false;
                                            ShowProxyAuthWarning();
                                            continue;
                                        default:
                                            Program.bShowProxyScreen = false;
                                            var fileLength = (long) fileDownloader.FileLength;
                                            long bytesDownloaded;
                                            do
                                            {
                                                Application.DoEvents();
                                                bytesDownloaded = (long) fileDownloader.BytesDownloaded;
                                                Application.DoEvents();
                                            } while (bytesDownloaded < fileLength);

                                            return true;
                                    }
                                else
                                    break;
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }

                        return true;
                    default:
                        var fileLength1 = (long) fileDownloader.FileLength;
                        long bytesDownloaded1;
                        do
                        {
                            Application.DoEvents();
                            bytesDownloaded1 = (long) fileDownloader.BytesDownloaded;
                            Application.DoEvents();
                        } while (bytesDownloaded1 < fileLength1);

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
            LoadXMLFirstTime();
            objParent = frmViewer == null ? (Control) frmOpenBook : (Control) frmViewer;
            if (objParent == null)
                return;
            if (objParent.InvokeRequired)
                objParent.Invoke((Delegate) new ShowProxyAuthWarningDelegate(ShowProxyAuthWarning));
            else
                ShowProxyAuthenticationError();
        }

        private void ShowProxyAuthenticationError()
        {
            var num = (int) new frmProxyAuthentication().ShowDialog();
        }

        public string GetResource(string sDefaultValue, string sKey, ResourceType rType)
        {
            try
            {
                var xQuery1 = "" + "/Screen[@Name='MAIN_FORM']";
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
                        return GetResourceValue(sDefaultValue, xQuery1);
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

                var xQuery2 = xQuery1 + "/Resource[@Name='" + sKey + "']";
                return GetResourceValue(sDefaultValue, xQuery2);
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
                var xmlNode = xmlDocument.SelectSingleNode("/GSPcLocalViewer" + xQuery);
                if (xmlNode == null || xmlDocument == null || xmlNode.Attributes.Count <= 0 ||
                    xmlNode.Attributes["Value"] == null)
                    return sDefaultValue;
                var str = xmlNode.Attributes["Value"].Value;
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
                    GetOSLanguage();
                    var files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
                    for (var index = 0; index < files.Length; ++index)
                        try
                        {
                            if (File.Exists(files[index]))
                            {
                                var num1 = files[index].IndexOf(".xml");
                                var num2 = files[index].LastIndexOf("\\");
                                var str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                                var str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                                var xmlDocument = new XmlDocument();
                                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                                var str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"]
                                    .Value;
                                if (str3.Length != 0)
                                    if (str3 != null)
                                        if (str3 == Settings.Default.appLanguage)
                                            if (File.Exists(str2))
                                            {
                                                this.xmlDocument = new XmlDocument();
                                                this.xmlDocument.Load(str2);
                                                break;
                                            }
                            }
                        }
                        catch
                        {
                        }
                }
                else
                {
                    if (Settings.Default.appLanguage != null)
                        if (Settings.Default.appLanguage.Length != 0)
                            goto label_27;
                    Settings.Default.appLanguage = "English";
                    var osLanguage = GetOSLanguage();
                    var files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
                    for (var index = 0; index < files.Length; ++index)
                        try
                        {
                            if (File.Exists(files[index]))
                            {
                                var num1 = files[index].IndexOf(".xml");
                                var num2 = files[index].LastIndexOf("\\");
                                var str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                                var str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                                var xmlDocument = new XmlDocument();
                                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                                var str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"]
                                    .Value;
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
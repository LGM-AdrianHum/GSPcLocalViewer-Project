// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Program
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using AxDjVuCtrlLib;
using GSPcLocalViewer.Properties;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  internal static class Program
  {
    public static XmlDocument xmlDocument = new XmlDocument();
    public static AutoCompleteStringCollection objPartNumberSearchHistroyCollection = new AutoCompleteStringCollection();
    public static AutoCompleteStringCollection objPartNameSearchHistroyCollection = new AutoCompleteStringCollection();
    public static AutoCompleteStringCollection objPageNameSearchHistroyCollection = new AutoCompleteStringCollection();
    public static AutoCompleteStringCollection objTextSearchHistroyCollection = new AutoCompleteStringCollection();
    public static bool bShowProxyScreen = true;
    public static bool bAvoidAppUpdateXML = false;
    public const int c_ServerKey = 0;
    public const int c_BookPublishingId = 1;
    public const int c_PageId = 2;
    public const int c_ImageIndex = 3;
    public const int c_ListIndex = 4;
    public const int c_PartNumber = 5;
    public const string ENCRYPTION_KEY = "0123456789ABCDEF";
    public const string DJVU_ENCRYPTION_KEY = "0123456789abcdef";
    public static bool bNoViewerOpen;
    public static IniFile iniGSPcLocal;
    public static IniFile iniDjVuCtl;
    public static IniFile iniUserSet;
    private static IniFile iniLicense;
    public static AES objAES;
    public static DjVuFeatures objDjVuFeatures;
    public static ApplicationFeatures objAppFeatures;
    public static ApplicationMode objAppMode;
    public static IniFile[] iniServers;
    public static Hashtable iniKeys;
    public static string configPath;
    public static string defaultsPath;
    public static MemoSession objMemoSession;
    public static string DjVuPageNumber;
    public static string HighLightText;
    public static int iRetryInterval;
    public static int iRetryAttempts;

    [DllImport("DjVuCTL.ocx")]
    private static extern int DllRegisterServer();

    [STAThread]
    public static void Main(string[] args)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      try
      {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        if (Program.DjVuRegistered())
        {
          new Program.SingleInstanceApp().Run(args);
        }
        else
        {
          string str = "";
          if (!(str == string.Empty) && str != null)
            return;
          MessageHandler.ShowError(Program.LoadResource("DjVu control is not registered on this machine", "DJVU_UNREGISTERED", ResourceType.POPUP_MESSAGE), Program.LoadResource("GSPcLocal Viewer 3.0", "GSPCLOCAL", ResourceType.LABEL));
        }
      }
      catch (Exception ex)
      {
        if (ex.Message == "update")
        {
          MessageHandler.ShowInformation(Program.LoadResource("Updates are available. Application is going to update.", "UPDATES_AVAILABLE", ResourceType.POPUP_MESSAGE), Program.LoadResource("Update Manager", "UPDATE_MANAGER", ResourceType.LABEL));
          Program.ExecuteUpdateManager(args);
        }
        if (ex.Message == "LiscenceError")
          MessageHandler.ShowInformation(Program.LoadResource("Application configuration is incorrect. Please re-install it or contact your system administrator.", "INCORRECT_CONFIGURATION", ResourceType.POPUP_MESSAGE), Program.LoadResource("GSPcLocal Viewer 3.0", "GSPCLOCAL", ResourceType.LABEL));
        Environment.Exit(0);
      }
    }

    private static void ReadRetrySettings()
    {
      try
      {
        int result = 0;
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        Program.iRetryAttempts = 0;
        Program.iRetryInterval = 0;
        if (Program.iniGSPcLocal.items["RETRY", "RETRY_ATTEMPTS"] != null)
          empty1 = Program.iniGSPcLocal.items["RETRY", "RETRY_ATTEMPTS"].ToString();
        if (Program.iniGSPcLocal.items["RETRY", "RETRY_INTERVAL"] != null)
          empty2 = Program.iniGSPcLocal.items["RETRY", "RETRY_INTERVAL"].ToString();
        if (empty1 != null && int.TryParse(empty1, out result))
        {
          Program.iRetryAttempts = result;
          if (Program.iRetryAttempts > 10)
            Program.iRetryAttempts = 10;
        }
        if (empty2 == null)
          return;
        if (int.TryParse(empty2, out result))
          Program.iRetryInterval = result;
        if (Program.iRetryInterval <= 5000)
          return;
        Program.iRetryInterval = 5000;
      }
      catch
      {
      }
    }

    private static void LoadIniFiles()
    {
      if (System.IO.File.Exists(Application.StartupPath + "\\GSPcLocal.ini"))
        Program.iniGSPcLocal = new IniFile(Application.StartupPath + "\\GSPcLocal.ini", "GSPCLOCAL");
      else
        MessageHandler.ShowInformation(Program.LoadResource("GSPcLocal.ini file not found", "INI_NOT_FOUND", ResourceType.POPUP_MESSAGE));
      if (System.IO.File.Exists(Application.StartupPath + "\\CSSDjVuCtl.ini"))
        Program.iniDjVuCtl = new IniFile(Application.StartupPath + "\\CSSDjVuCtl.ini", "CSSDjVuCtl");
      if (System.IO.File.Exists(Application.StartupPath + "\\License.ini"))
        Program.iniLicense = new IniFile(Application.StartupPath + "\\License.ini", "LICENSE");
      if (System.IO.File.Exists(Application.StartupPath + "\\UserSet.ini"))
        Program.iniUserSet = new IniFile(Application.StartupPath + "\\UserSet.ini", "USERSET");
      string str1 = Program.iniGSPcLocal.items["SETTINGS", "SERVER_KEYS"];
      string[] strArray;
      if (str1 != null)
        strArray = str1.Split(',');
      else
        strArray = Directory.GetFiles(Application.StartupPath, "GSP_*.ini", SearchOption.TopDirectoryOnly);
      Array.Resize<IniFile>(ref Program.iniServers, strArray.Length);
      int index1 = 0;
      if (strArray.Length > 0)
      {
        Program.iniKeys = new Hashtable();
        for (int index2 = 0; index2 < strArray.Length; ++index2)
        {
          if (str1 != null)
            strArray[index2] = Application.StartupPath + "\\GSP_" + strArray[index2] + ".ini";
          string str2 = strArray[index2].ToUpper().Substring(strArray[index2].LastIndexOf("\\") + 5);
          string sFileKey = str2.Substring(0, str2.LastIndexOf(".INI"));
          if (System.IO.File.Exists(strArray[index2]))
          {
            Program.iniServers[index1] = new IniFile(strArray[index2], sFileKey);
            Program.iniKeys.Add((object) sFileKey, (object) index1);
            ++index1;
          }
          else
            Array.Resize<IniFile>(ref Program.iniServers, Program.iniServers.Length - 1);
        }
      }
      else
        MessageHandler.ShowInformation(Program.LoadResource("No server ini file found", "NO_SERVER_INI", ResourceType.POPUP_MESSAGE));
    }

    private static bool DjVuRegistered()
    {
      try
      {
        new AxDjVuCtrl().CreateControl();
        return true;
      }
      catch(Exception ex)
      {
          Console.WriteLine(ex);
      }
      try
      {
        return Program.DllRegisterServer() == 0;
      }
      catch
      {
      }
      return false;
    }

    public static void ExecuteUpdateManager(string[] sViewerArguments)
    {
      try
      {
        string arguments = string.Empty;
        if (sViewerArguments != null)
        {
          foreach (string sViewerArgument in sViewerArguments)
            arguments = !(arguments == string.Empty) ? arguments + " " + sViewerArgument : arguments + sViewerArgument;
        }
        if (arguments == "" && arguments.Length == 0 && Program.objAppFeatures.bDirExeute)
          arguments = "DIREXE";
        new Process()
        {
          StartInfo = new ProcessStartInfo(Application.StartupPath + "\\UpdateManager.exe", arguments)
          {
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Maximized,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true
          }
        }.Start();
      }
      catch
      {
      }
    }

    public static void ReadSearchHistory(string sFileName, string sSearchName, AutoCompleteStringCollection objHistoryCollection)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        FileSystemInfo fileSystemInfo = (FileSystemInfo) new FileInfo(sFileName);
        if (!fileSystemInfo.Exists || objHistoryCollection.Count != 0)
          return;
        xmlDocument.Load(fileSystemInfo.FullName);
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//" + sSearchName + "//Value");
        if (xmlNodeList == null)
          return;
        foreach (XmlNode xmlNode in xmlNodeList)
        {
          string empty = string.Empty;
          if (xmlNode.Attributes["Text"] != null)
            empty = xmlNode.Attributes["Text"].Value;
          if (!Program.objPartNumberSearchHistroyCollection.Contains(empty) && empty != string.Empty)
            objHistoryCollection.Add(empty);
        }
      }
      catch
      {
      }
    }

    public static void WriteSearchHistory(string sFileName, string sSearchName, AutoCompleteStringCollection objHistoryCollection)
    {
      try
      {
        FileSystemInfo fileSystemInfo = (FileSystemInfo) new FileInfo(sFileName);
        XmlDocument xmlDocument = new XmlDocument();
        if (!fileSystemInfo.Exists)
          return;
        xmlDocument.Load(fileSystemInfo.FullName);
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("//SEARCH/" + sSearchName);
        if (xmlNode1 == null)
          return;
        XmlNodeList xmlNodeList = xmlNode1.SelectNodes("//Value");
        for (int index = 0; index < objHistoryCollection.Count; ++index)
        {
          string objHistory = objHistoryCollection[index];
          bool flag = false;
          foreach (XmlNode xmlNode2 in xmlNodeList)
          {
            string empty = string.Empty;
            if (xmlNode2.Attributes["Text"] != null)
              empty = xmlNode2.Attributes["Text"].Value;
            if (objHistory == empty)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
          {
            XmlNode node = xmlDocument.CreateNode(XmlNodeType.Element, "Value", (string) null);
            XmlAttribute attribute = xmlDocument.CreateAttribute("Text");
            attribute.Value = objHistory;
            node.Attributes.Append(attribute);
            xmlNode1.AppendChild(node);
          }
        }
        if (objHistoryCollection.Count == 0)
          xmlNode1.RemoveAll();
        xmlDocument.Save(fileSystemInfo.FullName);
      }
      catch
      {
      }
    }

    public static string GetOSLanguage()
    {
      CultureInfo.CurrentCulture.ClearCachedData();
      return CultureInfo.CurrentCulture.DisplayName.Split(' ')[0].ToString();
    }

    public static void LoadXML()
    {
      try
      {
        if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
        {
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            if (System.IO.File.Exists(files[index]))
            {
              int num1 = files[index].IndexOf(".xml");
              int num2 = files[index].LastIndexOf("\\");
              string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
              string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
              XmlDocument xmlDocument = new XmlDocument();
              xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
              if (xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value == Settings.Default.appLanguage && System.IO.File.Exists(str2))
                Program.xmlDocument.Load(str2);
            }
          }
        }
        else
        {
          if (Settings.Default.appLanguage != null && Settings.Default.appLanguage.Length != 0)
            return;
          Settings.Default.appLanguage = "English";
          string osLanguage = Program.GetOSLanguage();
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            if (System.IO.File.Exists(files[index]))
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
                if (System.IO.File.Exists(str2))
                  Program.xmlDocument.Load(str2);
              }
              else if (str3 == Settings.Default.appLanguage && System.IO.File.Exists(str2))
                Program.xmlDocument.Load(str2);
            }
          }
        }
      }
      catch
      {
        Program.xmlDocument = (XmlDocument) null;
      }
    }

    private static string LoadResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      Program.LoadXML();
      return Program.GetResource(sDefaultValue, sKey, rType);
    }

    private static string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str1 = "" + "/Screen[@Name='APPLICATION']";
        switch (rType)
        {
          case ResourceType.LABEL:
            str1 += "/Resources[@Name='LABEL']";
            break;
          case ResourceType.POPUP_MESSAGE:
            str1 += "/Resources[@Name='POPUP_MESSAGE']";
            break;
        }
        string xpath = "/GSPcLocalViewer" + (str1 + "/Resource[@Name='" + sKey + "']");
        XmlNode xmlNode = Program.xmlDocument.SelectSingleNode(xpath);
        if (xmlNode == null || (xmlNode.Attributes.Count <= 0 || xmlNode.Attributes["Value"] == null))
          return sDefaultValue;
        string str2 = xmlNode.Attributes["Value"].Value;
        if (!string.IsNullOrEmpty(str2))
          return str2;
        return sDefaultValue;
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private class SingleInstanceApp : WindowsFormsApplicationBase
    {
      public SingleInstanceApp()
      {
        this.IsSingleInstance = true;
        this.EnableVisualStyles = true;
        this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
        this.StartupNextInstance += new StartupNextInstanceEventHandler(this.SIApp_StartupNextInstance);
      }

      protected override void OnCreateMainForm()
      {
        Program.LoadIniFiles();
        Program.ReadRetrySettings();
        Program.objAES = new AES();
        try
        {
          if (Program.iniLicense == null)
          {
            this.SplashScreen.Visible = false;
            throw new Exception("LiscenceError");
          }
          if (Program.iniLicense.items["SETTINGS", "PRODUCT_ID"] == null || Program.iniLicense.items["SETTINGS", "PRODUCT_ID"] == string.Empty)
          {
            this.SplashScreen.Visible = false;
            throw new Exception("LiscenceError");
          }
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          if (new RegistryReader().Read("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\", "ProductId") != Program.objAES.Decode(Program.iniLicense.items["SETTINGS", "PRODUCT_ID"], "0123456789ABCDEF"))
          {
            this.SplashScreen.Visible = false;
            throw new Exception("LiscenceError");
          }
        }
        catch
        {
          this.SplashScreen.Visible = false;
          throw new Exception("LiscenceError");
        }
        string str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] ?? "Data";
        if (!Path.IsPathRooted(str))
        {
          string fullPath = Path.GetFullPath(Path.Combine(Application.StartupPath, str));
          Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] = fullPath;
        }
        string sFeatures = string.Empty;
        try
        {
          sFeatures = Program.objAES.DLLDecode(Program.iniGSPcLocal.items["SETTINGS", "GSP_APPLICATION_SETTINGS"], "0123456789ABCDEF");
        }
        catch
        {
        }
        Program.objAppFeatures = new ApplicationFeatures(sFeatures);
        string[] strArray = new string[this.CommandLineArgs.Count];
        this.CommandLineArgs.CopyTo(strArray, 0);
        bool flag = false;
        Program.objAppMode = new ApplicationMode();
        this.CheckFileDownloaderUpdates();
        Update update = new Update();
        if (strArray.Length != 0)
        {
          Program.objAppFeatures.bDcMode = false;
        }
        else
        {
          if (!Program.objAppFeatures.bDirExeute)
          {
            this.SplashScreen.Visible = false;
            MessageHandler.ShowInformation(Program.LoadResource("Direct execution of application is not allowed", "DIRECT_EXECUTION", ResourceType.POPUP_MESSAGE));
            return;
          }
          if (Program.objAppFeatures.bDirExeute && !Program.objAppFeatures.bOpenBookScreen)
          {
            this.SplashScreen.Visible = false;
            MessageHandler.ShowInformation(Program.LoadResource("(E-OB-EM001) Cannot open book.", "E-OB-EM001", ResourceType.POPUP_MESSAGE));
            return;
          }
        }
        if (!Program.objAppFeatures.bDcMode && update.CheckApplicationUpdates(strArray))
          flag = true;
        if (flag)
        {
          if (Program.objAppMode.InternetConnected)
          {
            try
            {
              this.SplashScreen.Visible = false;
              throw new Exception("update");
            }
            catch
            {
              throw new Exception("update");
            }
          }
        }
        Program.objMemoSession = new MemoSession();
        Program.objDjVuFeatures = new DjVuFeatures();
        this.MainForm = (Form) new Dashbord();
        if (this.MainForm.InvokeRequired)
          this.MainForm.Invoke((Delegate) new Dashbord.FirstTimeDelegate(((Dashbord) this.MainForm).FirstTime));
        else
          ((Dashbord) this.MainForm).FirstTime(strArray);
      }

      protected override void OnCreateSplashScreen()
      {
        this.SplashScreen = (Form) new Splash();
      }

      private void CheckFileDownloaderUpdates()
      {
        try
        {
          string sSourcePath = (string) null;
          string str1 = (string) null;
          string str2 = (string) null;
          string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\UpdateManager";
          try
          {
            if (!Directory.Exists(path))
              Directory.CreateDirectory(path);
            sSourcePath = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
            str2 = sSourcePath.Substring(0, sSourcePath.LastIndexOf("/"));
            str1 = new FileInfo(sSourcePath.Substring(sSourcePath.LastIndexOf("/"))).Name;
          }
          catch
          {
          }
          if (sSourcePath != null && str2 != null && this.DownloadingFileDirect(sSourcePath, path + "\\" + str1))
            System.IO.File.Copy(path + "\\" + str1, Application.StartupPath + "\\" + str1, true);
          if (!System.IO.File.Exists(Application.StartupPath + "\\" + str1))
            return;
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.Load(Application.StartupPath + "\\" + str1);
          foreach (XmlNode selectNode in xmlDocument.SelectNodes("(/update/file[@name ='FileDownloaderDLL.dll'])[1]"))
          {
            UpdateFile updateFile = new UpdateFile(selectNode);
            if (selectNode.Attributes["name"].Value.ToUpper() == "FILEDOWNLOADERDLL.DLL" && updateFile.fDownloadRequired())
            {
              if (this.DownloadingFileDirect(updateFile.sSourcePath, path + "\\" + updateFile.sFileName))
              {
                try
                {
                  System.IO.File.Copy(path + "\\" + updateFile.sFileName, Application.StartupPath + "\\" + updateFile.sFileName, true);
                  break;
                }
                catch (Exception ex)
                {
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }

      public bool DownloadingFileDirect(string sSourcePath, string sDestPath)
      {
        try
        {
          if (Settings.Default.appProxyType == "0")
          {
            using (WebClient webClient = new WebClient())
              webClient.DownloadFile(sSourcePath, sDestPath);
          }
          else
          {
            string userName = Settings.Default.appProxyLogin.ToString().Trim();
            string password = Settings.Default.appProxyPassword.ToString().Trim();
            string uriString = "http://" + Settings.Default.appProxyIP.ToString().Trim() + ":" + Settings.Default.appProxyPort.ToString().Trim();
            WebProxy webProxy = new WebProxy();
            webProxy.Address = new Uri(uriString);
            webProxy.Credentials = !(userName != string.Empty) ? CredentialCache.DefaultCredentials : (ICredentials) new NetworkCredential(userName, password);
            using (WebClient webClient = new WebClient())
            {
              webClient.Proxy = (IWebProxy) webProxy;
              webClient.DownloadFile(sSourcePath, sDestPath);
            }
          }
        }
        catch (Exception ex)
        {
          return false;
        }
        return true;
      }

      protected void SIApp_StartupNextInstance(object sender, StartupNextInstanceEventArgs eventArgs)
      {
        try
        {
          Program.objAppMode.bFirstTime = false;
          if (eventArgs.CommandLine.Count == 0)
            return;
          string[] strArray = new string[eventArgs.CommandLine.Count];
          eventArgs.CommandLine.CopyTo(strArray, 0);
          if (this.MainForm.InvokeRequired)
            this.MainForm.Invoke((Delegate) new Dashbord.NextTimeDelegate(((Dashbord) this.MainForm).NextTime));
          else
            ((Dashbord) this.MainForm).NextTime(strArray);
        }
        catch (Exception ex)
        {
        }
      }

      protected void SetArguments(string[] args)
      {
        if (this.MainForm.InvokeRequired)
          this.MainForm.Invoke((Delegate) new frmViewer.SetArgumentsDelegate(((frmViewer) this.MainForm).SetArguments));
        else
          ((frmViewer) this.MainForm).SetArguments(args);
      }

      protected void OpenViewer()
      {
        if (this.MainForm.InvokeRequired)
          this.MainForm.Invoke((Delegate) new frmViewer.LoadDataDirectDelegate(((frmViewer) this.MainForm).LoadDataDirect));
        else
          ((frmViewer) this.MainForm).LoadDataDirect();
      }
    }
  }
}

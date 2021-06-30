using AxDjVuCtrlLib;
using GSPcLocalViewer.Properties;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections;
using System.Collections.ObjectModel;
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
		public const int c_ServerKey = 0;

		public const int c_BookPublishingId = 1;

		public const int c_PageId = 2;

		public const int c_ImageIndex = 3;

		public const int c_ListIndex = 4;

		public const int c_PartNumber = 5;

		public const string ENCRYPTION_KEY = "0123456789ABCDEF";

		public const string DJVU_ENCRYPTION_KEY = "0123456789abcdef";

		public static XmlDocument xmlDocument;

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

		public static AutoCompleteStringCollection objPartNumberSearchHistroyCollection;

		public static AutoCompleteStringCollection objPartNameSearchHistroyCollection;

		public static AutoCompleteStringCollection objPageNameSearchHistroyCollection;

		public static AutoCompleteStringCollection objTextSearchHistroyCollection;

		public static string DjVuPageNumber;

		public static string HighLightText;

		public static int iRetryInterval;

		public static int iRetryAttempts;

		public static bool bShowProxyScreen;

		public static bool bAvoidAppUpdateXML;

		static Program()
		{
			Program.xmlDocument = new XmlDocument();
			Program.objPartNumberSearchHistroyCollection = new AutoCompleteStringCollection();
			Program.objPartNameSearchHistroyCollection = new AutoCompleteStringCollection();
			Program.objPageNameSearchHistroyCollection = new AutoCompleteStringCollection();
			Program.objTextSearchHistroyCollection = new AutoCompleteStringCollection();
			Program.bShowProxyScreen = true;
			Program.bAvoidAppUpdateXML = false;
		}

		private static bool DjVuRegistered()
		{
			bool flag;
			try
			{
				(new AxDjVuCtrl()).CreateControl();
				flag = true;
				return flag;
			}
			catch
			{
			}
			try
			{
				flag = (Program.DllRegisterServer() != 0 ? false : true);
			}
			catch
			{
				return false;
			}
			return flag;
		}

		[DllImport("DjVuCTL.ocx", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern int DllRegisterServer();

		public static void ExecuteUpdateManager(string[] sViewerArguments)
		{
			try
			{
				string empty = string.Empty;
				if (sViewerArguments != null)
				{
					string[] strArrays = sViewerArguments;
					for (int i = 0; i < (int)strArrays.Length; i++)
					{
						string str = strArrays[i];
						empty = (empty != string.Empty ? string.Concat(empty, " ", str) : string.Concat(empty, str));
					}
				}
				if (empty == "" && empty.Length == 0 && Program.objAppFeatures.bDirExeute)
				{
					empty = "DIREXE";
				}
				Process process = new Process();
				ProcessStartInfo processStartInfo = new ProcessStartInfo(string.Concat(Application.StartupPath, "\\UpdateManager.exe"), empty)
				{
					CreateNoWindow = true,
					WindowStyle = ProcessWindowStyle.Maximized,
					UseShellExecute = false,
					RedirectStandardInput = true,
					RedirectStandardError = true,
					RedirectStandardOutput = true
				};
				process.StartInfo = processStartInfo;
				process.Start();
			}
			catch
			{
			}
		}

		public static string GetOSLanguage()
		{
			CultureInfo.CurrentCulture.ClearCachedData();
			string displayName = CultureInfo.CurrentCulture.DisplayName;
			char[] chrArray = new char[] { ' ' };
			return displayName.Split(chrArray)[0].ToString();
		}

		private static string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string str;
			try
			{
				string str1 = "";
				str1 = string.Concat(str1, "/Screen[@Name='APPLICATION']");
				if (rType == ResourceType.LABEL)
				{
					str1 = string.Concat(str1, "/Resources[@Name='LABEL']");
				}
				else if (rType == ResourceType.POPUP_MESSAGE)
				{
					str1 = string.Concat(str1, "/Resources[@Name='POPUP_MESSAGE']");
				}
				str1 = string.Concat(str1, "/Resource[@Name='", sKey, "']");
				string str2 = string.Concat("/GSPcLocalViewer", str1);
				XmlNode xmlNodes = Program.xmlDocument.SelectSingleNode(str2);
				if (xmlNodes == null)
				{
					str = sDefaultValue;
				}
				else if (xmlNodes.Attributes.Count <= 0 || xmlNodes.Attributes["Value"] == null)
				{
					str = sDefaultValue;
				}
				else
				{
					string value = xmlNodes.Attributes["Value"].Value;
					str = (string.IsNullOrEmpty(value) ? sDefaultValue : value);
				}
			}
			catch (Exception exception)
			{
				str = sDefaultValue;
			}
			return str;
		}

		private static void LoadIniFiles()
		{
			string[] strArrays;
			if (!File.Exists(string.Concat(Application.StartupPath, "\\GSPcLocal.ini")))
			{
				MessageHandler.ShowInformation(Program.LoadResource("GSPcLocal.ini file not found", "INI_NOT_FOUND", ResourceType.POPUP_MESSAGE));
			}
			else
			{
				Program.iniGSPcLocal = new IniFile(string.Concat(Application.StartupPath, "\\GSPcLocal.ini"), "GSPCLOCAL");
			}
			if (File.Exists(string.Concat(Application.StartupPath, "\\CSSDjVuCtl.ini")))
			{
				Program.iniDjVuCtl = new IniFile(string.Concat(Application.StartupPath, "\\CSSDjVuCtl.ini"), "CSSDjVuCtl");
			}
			if (File.Exists(string.Concat(Application.StartupPath, "\\License.ini")))
			{
				Program.iniLicense = new IniFile(string.Concat(Application.StartupPath, "\\License.ini"), "LICENSE");
			}
			if (File.Exists(string.Concat(Application.StartupPath, "\\UserSet.ini")))
			{
				Program.iniUserSet = new IniFile(string.Concat(Application.StartupPath, "\\UserSet.ini"), "USERSET");
			}
			string item = Program.iniGSPcLocal.items["SETTINGS", "SERVER_KEYS"];
			strArrays = (item == null ? Directory.GetFiles(Application.StartupPath, "GSP_*.ini", SearchOption.TopDirectoryOnly) : item.Split(new char[] { ',' }));
			Array.Resize<IniFile>(ref Program.iniServers, (int)strArrays.Length);
			int num = 0;
			if ((int)strArrays.Length <= 0)
			{
				MessageHandler.ShowInformation(Program.LoadResource("No server ini file found", "NO_SERVER_INI", ResourceType.POPUP_MESSAGE));
				return;
			}
			Program.iniKeys = new Hashtable();
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				if (item != null)
				{
					strArrays[i] = string.Concat(Application.StartupPath, "\\GSP_", strArrays[i], ".ini");
				}
				string str = strArrays[i].ToUpper().Substring(strArrays[i].LastIndexOf("\\") + 5);
				str = str.Substring(0, str.LastIndexOf(".INI"));
				if (!File.Exists(strArrays[i]))
				{
					Array.Resize<IniFile>(ref Program.iniServers, (int)Program.iniServers.Length - 1);
				}
				else
				{
					Program.iniServers[num] = new IniFile(strArrays[i], str);
					Program.iniKeys.Add(str, num);
					num++;
				}
			}
		}

		private static string LoadResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			Program.LoadXML();
			return Program.GetResource(sDefaultValue, sKey, rType);
		}

		public static void LoadXML()
		{
			try
			{
				if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
				{
					string str = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] files = Directory.GetFiles(str, "*.xml");
					for (int i = 0; i < (int)files.Length; i++)
					{
						if (File.Exists(files[i]))
						{
							int num = files[i].IndexOf(".xml");
							int num1 = files[i].LastIndexOf("\\");
							string str1 = files[i].Substring(num1 + 1, num - num1 - 1);
							string str2 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml");
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml"));
							if (xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value == Settings.Default.appLanguage && File.Exists(str2))
							{
								Program.xmlDocument.Load(str2);
							}
						}
					}
				}
				else if (Settings.Default.appLanguage == null || Settings.Default.appLanguage.Length == 0)
				{
					Settings.Default.appLanguage = "English";
					string oSLanguage = Program.GetOSLanguage();
					string str3 = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] strArrays = Directory.GetFiles(str3, "*.xml");
					for (int j = 0; j < (int)strArrays.Length; j++)
					{
						if (File.Exists(strArrays[j]))
						{
							int num2 = strArrays[j].IndexOf(".xml");
							int num3 = strArrays[j].LastIndexOf("\\");
							string str4 = strArrays[j].Substring(num3 + 1, num2 - num3 - 1);
							string str5 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml");
							XmlDocument xmlDocument1 = new XmlDocument();
							xmlDocument1.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml"));
							XmlNode xmlNodes = xmlDocument1.SelectSingleNode("//GSPcLocalViewer");
							string value = xmlNodes.Attributes["EN_NAME"].Value;
							if (value == oSLanguage)
							{
								if (File.Exists(str5))
								{
									Program.xmlDocument.Load(str5);
								}
							}
							else if (value == Settings.Default.appLanguage && File.Exists(str5))
							{
								Program.xmlDocument.Load(str5);
							}
						}
					}
				}
			}
			catch
			{
				Program.xmlDocument = null;
			}
		}

		[STAThread]
		public static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				if (!Program.DjVuRegistered())
				{
					string str = "";
					if (str == string.Empty || str == null)
					{
						MessageHandler.ShowError(Program.LoadResource("DjVu control is not registered on this machine", "DJVU_UNREGISTERED", ResourceType.POPUP_MESSAGE), Program.LoadResource("GSPcLocal Viewer 3.0", "GSPCLOCAL", ResourceType.LABEL));
					}
				}
				else
				{
					(new Program.SingleInstanceApp()).Run(args);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				if (exception.Message == "update")
				{
					MessageHandler.ShowInformation(Program.LoadResource("Updates are available. Application is going to update.", "UPDATES_AVAILABLE", ResourceType.POPUP_MESSAGE), Program.LoadResource("Update Manager", "UPDATE_MANAGER", ResourceType.LABEL));
					Program.ExecuteUpdateManager(args);
				}
				if (exception.Message == "LiscenceError")
				{
					MessageHandler.ShowInformation(Program.LoadResource("Application configuration is incorrect. Please re-install it or contact your system administrator.", "INCORRECT_CONFIGURATION", ResourceType.POPUP_MESSAGE), Program.LoadResource("GSPcLocal Viewer 3.0", "GSPCLOCAL", ResourceType.LABEL));
				}
				Environment.Exit(0);
			}
		}

		private static void ReadRetrySettings()
		{
			try
			{
				int num = 0;
				string empty = string.Empty;
				string str = string.Empty;
				Program.iRetryAttempts = 0;
				Program.iRetryInterval = 0;
				if (Program.iniGSPcLocal.items["RETRY", "RETRY_ATTEMPTS"] != null)
				{
					empty = Program.iniGSPcLocal.items["RETRY", "RETRY_ATTEMPTS"].ToString();
				}
				if (Program.iniGSPcLocal.items["RETRY", "RETRY_INTERVAL"] != null)
				{
					str = Program.iniGSPcLocal.items["RETRY", "RETRY_INTERVAL"].ToString();
				}
				if (empty != null && int.TryParse(empty, out num))
				{
					Program.iRetryAttempts = num;
					if (Program.iRetryAttempts > 10)
					{
						Program.iRetryAttempts = 10;
					}
				}
				if (str != null)
				{
					if (int.TryParse(str, out num))
					{
						Program.iRetryInterval = num;
					}
					if (Program.iRetryInterval > 5000)
					{
						Program.iRetryInterval = 5000;
					}
				}
			}
			catch
			{
			}
		}

		public static void ReadSearchHistory(string sFileName, string sSearchName, AutoCompleteStringCollection objHistoryCollection)
		{
			try
			{
				XmlNodeList xmlNodeLists = null;
				XmlDocument xmlDocument = new XmlDocument();
				FileSystemInfo fileInfo = new FileInfo(sFileName);
				if (fileInfo.Exists && objHistoryCollection.Count == 0)
				{
					xmlDocument.Load(fileInfo.FullName);
					xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//", sSearchName, "//Value"));
					if (xmlNodeLists != null)
					{
						foreach (XmlNode xmlNodes in xmlNodeLists)
						{
							string empty = string.Empty;
							if (xmlNodes.Attributes["Text"] != null)
							{
								empty = xmlNodes.Attributes["Text"].Value;
							}
							if (Program.objPartNumberSearchHistroyCollection.Contains(empty) || !(empty != string.Empty))
							{
								continue;
							}
							objHistoryCollection.Add(empty);
						}
					}
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
				FileSystemInfo fileInfo = new FileInfo(sFileName);
				XmlNode xmlNodes = null;
				XmlNodeList xmlNodeLists = null;
				XmlDocument xmlDocument = new XmlDocument();
				if (fileInfo.Exists)
				{
					xmlDocument.Load(fileInfo.FullName);
					xmlNodes = xmlDocument.SelectSingleNode(string.Concat("//SEARCH/", sSearchName));
					if (xmlNodes != null)
					{
						xmlNodeLists = xmlNodes.SelectNodes("//Value");
						for (int i = 0; i < objHistoryCollection.Count; i++)
						{
							string item = objHistoryCollection[i];
							bool flag = false;
							foreach (XmlNode xmlNodes1 in xmlNodeLists)
							{
								string empty = string.Empty;
								if (xmlNodes1.Attributes["Text"] != null)
								{
									empty = xmlNodes1.Attributes["Text"].Value;
								}
								if (item != empty)
								{
									continue;
								}
								flag = true;
								break;
							}
							if (!flag)
							{
								XmlNode xmlNodes2 = xmlDocument.CreateNode(XmlNodeType.Element, "Value", null);
								XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("Text");
								xmlAttribute.Value = item;
								xmlNodes2.Attributes.Append(xmlAttribute);
								xmlNodes.AppendChild(xmlNodes2);
							}
						}
						if (objHistoryCollection.Count == 0)
						{
							xmlNodes.RemoveAll();
						}
						xmlDocument.Save(fileInfo.FullName);
					}
				}
			}
			catch
			{
			}
		}

		private class SingleInstanceApp : WindowsFormsApplicationBase
		{
			public SingleInstanceApp()
			{
				base.IsSingleInstance = true;
				base.EnableVisualStyles = true;
				base.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
				base.StartupNextInstance += new StartupNextInstanceEventHandler(this.SIApp_StartupNextInstance);
			}

			private void CheckFileDownloaderUpdates()
			{
				try
				{
					string item = null;
					string name = null;
					string str = null;
					string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					folderPath = string.Concat(folderPath, "\\UpdateManager");
					try
					{
						if (!Directory.Exists(folderPath))
						{
							Directory.CreateDirectory(folderPath);
						}
						item = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
						str = item.Substring(0, item.LastIndexOf("/"));
						name = (new FileInfo(item.Substring(item.LastIndexOf("/")))).Name;
					}
					catch
					{
					}
					if (item != null && str != null && this.DownloadingFileDirect(item, string.Concat(folderPath, "\\", name)))
					{
						File.Copy(string.Concat(folderPath, "\\", name), string.Concat(Application.StartupPath, "\\", name), true);
					}
					if (File.Exists(string.Concat(Application.StartupPath, "\\", name)))
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.Load(string.Concat(Application.StartupPath, "\\", name));
						foreach (XmlNode xmlNodes in xmlDocument.SelectNodes("(/update/file[@name ='FileDownloaderDLL.dll'])[1]"))
						{
							UpdateFile updateFile = new UpdateFile(xmlNodes);
							if (!(xmlNodes.Attributes["name"].Value.ToUpper() == "FILEDOWNLOADERDLL.DLL") || !updateFile.fDownloadRequired() || !this.DownloadingFileDirect(updateFile.sSourcePath, string.Concat(folderPath, "\\", updateFile.sFileName)))
							{
								continue;
							}
							try
							{
								File.Copy(string.Concat(folderPath, "\\", updateFile.sFileName), string.Concat(Application.StartupPath, "\\", updateFile.sFileName), true);
								break;
							}
							catch (Exception exception)
							{
							}
						}
					}
				}
				catch (Exception exception1)
				{
				}
			}

			public bool DownloadingFileDirect(string sSourcePath, string sDestPath)
			{
				bool flag;
				try
				{
					if (Settings.Default.appProxyType != "0")
					{
						string str = Settings.Default.appProxyLogin.ToString().Trim();
						string str1 = Settings.Default.appProxyPassword.ToString().Trim();
						string str2 = string.Concat("http://", Settings.Default.appProxyIP.ToString().Trim(), ":", Settings.Default.appProxyPort.ToString().Trim());
						WebProxy webProxy = new WebProxy()
						{
							Address = new Uri(str2)
						};
						if (str == string.Empty)
						{
							webProxy.Credentials = CredentialCache.DefaultCredentials;
						}
						else
						{
							webProxy.Credentials = new NetworkCredential(str, str1);
						}
						using (WebClient webClient = new WebClient())
						{
							webClient.Proxy = webProxy;
							webClient.DownloadFile(sSourcePath, sDestPath);
						}
					}
					else
					{
						using (WebClient webClient1 = new WebClient())
						{
							webClient1.DownloadFile(sSourcePath, sDestPath);
						}
					}
					return true;
				}
				catch (Exception exception)
				{
					flag = false;
				}
				return flag;
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
						base.SplashScreen.Visible = false;
						throw new Exception("LiscenceError");
					}
					if (Program.iniLicense.items["SETTINGS", "PRODUCT_ID"] == null || Program.iniLicense.items["SETTINGS", "PRODUCT_ID"] == string.Empty)
					{
						base.SplashScreen.Visible = false;
						throw new Exception("LiscenceError");
					}
					string empty = string.Empty;
					string str = string.Empty;
					string empty1 = string.Empty;
					if ((new RegistryReader()).Read("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\", "ProductId") != Program.objAES.Decode(Program.iniLicense.items["SETTINGS", "PRODUCT_ID"], "0123456789ABCDEF"))
					{
						base.SplashScreen.Visible = false;
						throw new Exception("LiscenceError");
					}
				}
				catch
				{
					base.SplashScreen.Visible = false;
					throw new Exception("LiscenceError");
				}
				string item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] ?? "Data";
				if (!Path.IsPathRooted(item))
				{
					item = Path.Combine(Application.StartupPath, item);
					item = Path.GetFullPath(item);
					Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] = item;
				}
				string str1 = string.Empty;
				try
				{
					str1 = Program.objAES.DLLDecode(Program.iniGSPcLocal.items["SETTINGS", "GSP_APPLICATION_SETTINGS"], "0123456789ABCDEF");
				}
				catch
				{
				}
				Program.objAppFeatures = new ApplicationFeatures(str1);
				string[] strArrays = new string[base.CommandLineArgs.Count];
				base.CommandLineArgs.CopyTo(strArrays, 0);
				bool flag = false;
				Program.objAppMode = new ApplicationMode();
				this.CheckFileDownloaderUpdates();
				Update update = new Update();
				if ((int)strArrays.Length == 0)
				{
					if (!Program.objAppFeatures.bDirExeute)
					{
						base.SplashScreen.Visible = false;
						MessageHandler.ShowInformation(Program.LoadResource("Direct execution of application is not allowed", "DIRECT_EXECUTION", ResourceType.POPUP_MESSAGE));
						return;
					}
					if (Program.objAppFeatures.bDirExeute && !Program.objAppFeatures.bOpenBookScreen)
					{
						base.SplashScreen.Visible = false;
						MessageHandler.ShowInformation(Program.LoadResource("(E-OB-EM001) Cannot open book.", "E-OB-EM001", ResourceType.POPUP_MESSAGE));
						return;
					}
				}
				else
				{
					Program.objAppFeatures.bDcMode = false;
				}
				if (!Program.objAppFeatures.bDcMode && update.CheckApplicationUpdates(strArrays))
				{
					flag = true;
				}
				if (flag && Program.objAppMode.InternetConnected)
				{
					try
					{
						base.SplashScreen.Visible = false;
						throw new Exception("update");
					}
					catch
					{
						throw new Exception("update");
					}
				}
				Program.objMemoSession = new MemoSession();
				Program.objDjVuFeatures = new DjVuFeatures();
				base.MainForm = new Dashbord();
				if (!base.MainForm.InvokeRequired)
				{
					((Dashbord)base.MainForm).FirstTime(strArrays);
					return;
				}
				base.MainForm.Invoke(new Dashbord.FirstTimeDelegate((Dashbord)base.MainForm.FirstTime));
			}

			protected override void OnCreateSplashScreen()
			{
				base.SplashScreen = new Splash();
			}

			protected void OpenViewer()
			{
				if (!base.MainForm.InvokeRequired)
				{
					((frmViewer)base.MainForm).LoadDataDirect();
					return;
				}
				base.MainForm.Invoke(new frmViewer.LoadDataDirectDelegate((frmViewer)base.MainForm.LoadDataDirect));
			}

			protected void SetArguments(string[] args)
			{
				if (!base.MainForm.InvokeRequired)
				{
					((frmViewer)base.MainForm).SetArguments(args);
					return;
				}
				base.MainForm.Invoke(new frmViewer.SetArgumentsDelegate((frmViewer)base.MainForm.SetArguments));
			}

			protected void SIApp_StartupNextInstance(object sender, StartupNextInstanceEventArgs eventArgs)
			{
				try
				{
					Program.objAppMode.bFirstTime = false;
					if (eventArgs.CommandLine.Count != 0)
					{
						string[] strArrays = new string[eventArgs.CommandLine.Count];
						eventArgs.CommandLine.CopyTo(strArrays, 0);
						if (!base.MainForm.InvokeRequired)
						{
							((Dashbord)base.MainForm).NextTime(strArrays);
						}
						else
						{
							base.MainForm.Invoke(new Dashbord.NextTimeDelegate((Dashbord)base.MainForm.NextTime));
						}
					}
				}
				catch (Exception exception)
				{
				}
			}
		}
	}
}
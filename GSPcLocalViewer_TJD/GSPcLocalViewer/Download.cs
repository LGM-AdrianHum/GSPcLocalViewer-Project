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
		private GSPcLocalViewer.frmViewer frmViewer;

		private GSPcLocalViewer.frmOpenBook frmOpenBook;

		private Control objParent;

		public XmlDocument xmlDocument;

		public Download(GSPcLocalViewer.frmViewer objFrmViewer)
		{
			this.frmViewer = objFrmViewer;
			this.frmOpenBook = null;
		}

		public Download(GSPcLocalViewer.frmOpenBook objFrmOpenBook)
		{
			this.frmOpenBook = objFrmOpenBook;
			this.frmViewer = null;
		}

		public Download()
		{
			this.frmOpenBook = null;
			this.frmViewer = null;
		}

		public bool DownloadFile(string surl1, string sLocalPath)
		{
			int num = Program.iRetryAttempts;
			int num1 = Program.iRetryInterval;
			bool flag = false;
			flag = this.RetryDownloadFile(surl1, sLocalPath);
			try
			{
				if (flag && Path.GetExtension(sLocalPath).ToLower() == ".zip")
				{
					string str = sLocalPath.ToLower().Replace(".zip", ".xml");
					try
					{
						if (File.Exists(str))
						{
							File.Delete(str);
						}
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
			if (num > 1 && !flag)
			{
				int num2 = 0;
				while (num2 < num)
				{
					Thread.Sleep(num1);
					flag = this.RetryDownloadFile(surl1, sLocalPath);
					if (!flag)
					{
						num2++;
					}
					else
					{
						try
						{
							if (Path.GetExtension(sLocalPath).ToLower() == ".zip")
							{
								Global.Unzip(sLocalPath);
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
			bool flag;
			FileDownloader fileDownloader = new FileDownloader();
			long fileLength = (long)0;
			long bytesDownloaded = (long)0;
			try
			{
				if (sProxyType != "0")
				{
					int num = fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
					if (num == 407)
					{
						try
						{
							if (!surl1.ToUpper().EndsWith("APPUPDATE.XML"))
							{
								Program.bAvoidAppUpdateXML = false;
							}
							else
							{
								Program.bAvoidAppUpdateXML = true;
							}
							Program.bShowProxyScreen = true;
							if (!Dashbord.bShowProxy || Program.bAvoidAppUpdateXML)
							{
								flag = false;
								return flag;
							}
							else
							{
								this.ShowProxyAuthWarning();
								while (Program.bShowProxyScreen && !Program.bAvoidAppUpdateXML)
								{
									num = fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
									if (num == 407)
									{
										if (!Dashbord.bShowProxy)
										{
											flag = false;
											return flag;
										}
										else
										{
											this.ShowProxyAuthWarning();
										}
									}
									else if (num != 1)
									{
										Program.bShowProxyScreen = false;
										fileLength = (long)fileDownloader.get_FileLength();
										do
										{
											Application.DoEvents();
											bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
											Application.DoEvents();
										}
										while (bytesDownloaded < fileLength);
										flag = true;
										return flag;
									}
									else
									{
										Program.bShowProxyScreen = false;
										flag = false;
										return flag;
									}
								}
							}
						}
						catch (Exception exception)
						{
							flag = false;
							return flag;
						}
						flag = true;
					}
					else if (num != 1)
					{
						fileLength = (long)fileDownloader.get_FileLength();
						do
						{
							Application.DoEvents();
							bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
							Application.DoEvents();
						}
						while (bytesDownloaded < fileLength);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else if (fileDownloader.DownloadingFile(surl1, sLocalPath, string.Concat(sProxyTimeOut, "000")) != 1)
				{
					fileLength = (long)fileDownloader.get_FileLength();
					do
					{
						Application.DoEvents();
						bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
						Application.DoEvents();
					}
					while (bytesDownloaded < fileLength);
					flag = true;
				}
				else
				{
					flag = false;
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
				flag = false;
			}
			return flag;
		}

		public string GetOSLanguage()
		{
			CultureInfo.CurrentCulture.ClearCachedData();
			string displayName = CultureInfo.CurrentCulture.DisplayName;
			char[] chrArray = new char[] { ' ' };
			return displayName.Split(chrArray)[0].ToString();
		}

		public string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MAIN_FORM']");
				if (rType != ResourceType.TITLE)
				{
					if (rType == ResourceType.LABEL)
					{
						str = string.Concat(str, "/Resources[@Name='LABEL']");
					}
					else if (rType == ResourceType.BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='BUTTON']");
					}
					else if (rType == ResourceType.CHECK_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='CHECK_BOX']");
					}
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
					}
					else if (rType == ResourceType.COMBO_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='COMBO_BOX']");
					}
					else if (rType == ResourceType.GRID_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='GRID_VIEW']");
					}
					else if (rType == ResourceType.LIST_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='LIST_VIEW']");
					}
					else if (rType == ResourceType.MENU_BAR)
					{
						str = string.Concat(str, "/Resources[@Name='MENU_BAR']");
					}
					else if (rType == ResourceType.RADIO_BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='RADIO_BUTTON']");
					}
					else if (rType == ResourceType.CONTEXT_MENU)
					{
						str = string.Concat(str, "/Resources[@Name='CONTEXT_MENU']");
					}
					else if (rType == ResourceType.TOOLSTRIP)
					{
						str = string.Concat(str, "/Resources[@Name='TOOLSTRIP']");
					}
					str = string.Concat(str, "/Resource[@Name='", sKey, "']");
					resourceValue = this.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					resourceValue = this.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		public string GetResourceValue(string sDefaultValue, string xQuery)
		{
			string str;
			try
			{
				string str1 = string.Concat("/GSPcLocalViewer", xQuery);
				XmlNode xmlNodes = this.xmlDocument.SelectSingleNode(str1);
				if (xmlNodes == null)
				{
					str = sDefaultValue;
				}
				else if (this.xmlDocument == null)
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

		public void LoadXMLFirstTime()
		{
			this.xmlDocument = null;
			try
			{
				if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
				{
					this.GetOSLanguage();
					string str = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] files = Directory.GetFiles(str, "*.xml");
					for (int i = 0; i < (int)files.Length; i++)
					{
						try
						{
							if (File.Exists(files[i]))
							{
								int num = files[i].IndexOf(".xml");
								int num1 = files[i].LastIndexOf("\\");
								string str1 = files[i].Substring(num1 + 1, num - num1 - 1);
								string str2 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml");
								XmlDocument xmlDocument = new XmlDocument();
								xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml"));
								XmlNode xmlNodes = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
								string value = xmlNodes.Attributes["EN_NAME"].Value;
								if (value.Length != 0 && value != null && value == Settings.Default.appLanguage && File.Exists(str2))
								{
									this.xmlDocument = new XmlDocument();
									this.xmlDocument.Load(str2);
									goto Label0;
								}
							}
						}
						catch
						{
						}
					}
				}
				else if (Settings.Default.appLanguage == null || Settings.Default.appLanguage.Length == 0)
				{
					Settings.Default.appLanguage = "English";
					string oSLanguage = this.GetOSLanguage();
					string str3 = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] strArrays = Directory.GetFiles(str3, "*.xml");
					for (int j = 0; j < (int)strArrays.Length; j++)
					{
						try
						{
							if (File.Exists(strArrays[j]))
							{
								int num2 = strArrays[j].IndexOf(".xml");
								int num3 = strArrays[j].LastIndexOf("\\");
								string str4 = strArrays[j].Substring(num3 + 1, num2 - num3 - 1);
								string str5 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml");
								XmlDocument xmlDocument1 = new XmlDocument();
								xmlDocument1.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml"));
								XmlNode xmlNodes1 = xmlDocument1.SelectSingleNode("//GSPcLocalViewer");
								string value1 = xmlNodes1.Attributes["EN_NAME"].Value;
								if (value1 == oSLanguage)
								{
									if (File.Exists(str5))
									{
										this.xmlDocument.Load(str5);
										break;
									}
								}
								else if (value1 == Settings.Default.appLanguage && File.Exists(str5))
								{
									this.xmlDocument.Load(str5);
									break;
								}
							}
						}
						catch
						{
						}
					}
				}
			Label0:
			}
			catch
			{
			}
			if (this.xmlDocument == null)
			{
				this.xmlDocument = new XmlDocument();
				Settings.Default.appLanguage = "English";
			}
		}

		private bool RetryDownloadFile(string surl1, string sLocalPath)
		{
			bool flag;
			if (DataSize.spaceLeft < (long)10485760 && !Path.GetFileName(surl1).ToLower().Equals("dataupdate.xml"))
			{
				if (this.frmViewer != null)
				{
					this.frmViewer.ShowNotification();
				}
				else if (this.frmOpenBook != null)
				{
					this.frmOpenBook.frmParent.ShowNotification();
				}
				return false;
			}
			FileDownloader fileDownloader = new FileDownloader();
			long fileLength = (long)0;
			long bytesDownloaded = (long)0;
			int num = 0;
			try
			{
				if (Settings.Default.appProxyType != "0")
				{
					if (!surl1.ToUpper().EndsWith("APPUPDATE.XML"))
					{
						Program.bAvoidAppUpdateXML = false;
					}
					else
					{
						Program.bAvoidAppUpdateXML = true;
					}
					int num1 = fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
					if (num1 == 407)
					{
						Program.bShowProxyScreen = true;
						if (!Dashbord.bShowProxy || Program.bAvoidAppUpdateXML)
						{
							flag = false;
						}
						else
						{
							this.ShowProxyAuthWarning();
							while (Program.bShowProxyScreen && !Program.bAvoidAppUpdateXML)
							{
								num1 = fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
								if (num1 == 407)
								{
									if (!Dashbord.bShowProxy)
									{
										flag = false;
										return flag;
									}
									else
									{
										this.ShowProxyAuthWarning();
									}
								}
								else if (num1 != 1)
								{
									Program.bShowProxyScreen = false;
									fileLength = (long)fileDownloader.get_FileLength();
									do
									{
										bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
										num = (int)(bytesDownloaded * (long)100 / fileLength);
										if (this.frmViewer != null)
										{
											this.frmViewer.UpdateStatus(string.Concat(num.ToString(), "% Downloaded"));
										}
										if (this.frmOpenBook != null)
										{
											this.frmOpenBook.UpdateStatus(string.Concat(num.ToString(), "% Downloaded"));
										}
										Application.DoEvents();
									}
									while (bytesDownloaded < fileLength);
									flag = true;
									return flag;
								}
								else
								{
									Program.bShowProxyScreen = false;
									flag = false;
									return flag;
								}
							}
							flag = false;
						}
					}
					else if (num1 != 1)
					{
						fileLength = (long)fileDownloader.get_FileLength();
						do
						{
							bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
							num = (int)(bytesDownloaded * (long)100 / fileLength);
							if (this.frmViewer != null)
							{
								this.frmViewer.UpdateStatus(string.Concat(num.ToString(), "% Downloaded"));
							}
							if (this.frmOpenBook != null)
							{
								this.frmOpenBook.UpdateStatus(string.Concat(num.ToString(), "% Downloaded"));
							}
							Application.DoEvents();
						}
						while (bytesDownloaded < fileLength);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else
				{
					Application.DoEvents();
					if (fileDownloader.DownloadingFile(surl1, sLocalPath, string.Concat(Settings.Default.appProxyTimeOut, "000")) != 1)
					{
						fileLength = (long)fileDownloader.get_FileLength();
						do
						{
							Application.DoEvents();
							bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
							num = (int)(bytesDownloaded * (long)100 / fileLength);
							if (this.frmViewer != null)
							{
								this.frmViewer.UpdateStatus(string.Concat(num.ToString(), "% Downloaded"));
							}
							if (this.frmOpenBook != null)
							{
								this.frmOpenBook.UpdateStatus(string.Concat(num.ToString(), "% Downloaded"));
							}
							Application.DoEvents();
						}
						while (bytesDownloaded < fileLength);
						flag = true;
					}
					else
					{
						flag = false;
					}
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
				flag = false;
			}
			return flag;
		}

		private void ShowProxyAuthenticationError()
		{
			(new frmProxyAuthentication()).ShowDialog();
		}

		private void ShowProxyAuthWarning()
		{
			this.LoadXMLFirstTime();
			if (this.frmViewer == null)
			{
				this.objParent = this.frmOpenBook;
			}
			else
			{
				this.objParent = this.frmViewer;
			}
			if (this.objParent != null)
			{
				if (this.objParent.InvokeRequired)
				{
					this.objParent.Invoke(new Download.ShowProxyAuthWarningDelegate(this.ShowProxyAuthWarning));
					return;
				}
				this.ShowProxyAuthenticationError();
			}
		}

		private delegate void ShowProxyAuthenticationDelegate();

		private delegate void ShowProxyAuthWarningDelegate();
	}
}
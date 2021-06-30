using System;
using System.Collections;
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
			bool flag;
			this.sAppFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			this.sAppFolderPath = string.Concat(this.sAppFolderPath, "\\UpdateManager");
			try
			{
				if (Directory.Exists(this.sAppFolderPath))
				{
					flag = true;
				}
				else if (!Directory.CreateDirectory(this.sAppFolderPath).Exists)
				{
					MessageBox.Show(string.Concat("Cannot create application folder at path: ", this.sAppFolderPath), "Update Manager", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					flag = false;
				}
				else
				{
					flag = true;
				}
			}
			catch
			{
				MessageBox.Show(string.Concat("Cannot create application folder at path: ", this.sAppFolderPath), "Update Manager", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				flag = false;
			}
			return flag;
		}

		public bool CheckApplicationUpdates(string[] args)
		{
			bool flag;
			bool flag1 = false;
			string[] strArrays = args;
			int num = 0;
			while (true)
			{
				if (num >= (int)strArrays.Length)
				{
					if (!this.ApplicationFolderExist())
					{
						return false;
					}
					string item = null;
					string name = null;
					string str = null;
					bool flag2 = false;
					try
					{
						item = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
						str = item.Substring(0, item.LastIndexOf("/"));
						name = (new FileInfo(item.Substring(item.LastIndexOf("/")))).Name;
					}
					catch
					{
						flag = false;
						break;
					}
					if (item != null && str != null && this.UpdateManagerFolderExist())
					{
						if (this.objDownloader.DownloadFile(item, string.Concat(this.sAppFolderPath, "\\", name)))
						{
							flag1 = true;
							File.Copy(string.Concat(this.sAppFolderPath, "\\", name), string.Concat(Application.StartupPath, "\\", name), true);
						}
						if (!flag1)
						{
							return false;
						}
						if (File.Exists(string.Concat(Application.StartupPath, "\\", name)) && this.LoadUpdateXML(string.Concat(Application.StartupPath, "\\", name)))
						{
							this.xnlFileList = this.xUpdateXml.SelectNodes("//file");
							foreach (XmlNode xmlNodes in this.xnlFileList)
							{
								UpdateFile updateFile = new UpdateFile(xmlNodes);
								if (xmlNodes.Attributes["name"].Value.ToUpper() == "FILEDOWNLOADERDLL.DLL")
								{
									continue;
								}
								if (xmlNodes.Attributes["name"].Value.ToUpper() != "UPDATEMANAGER.EXE" && !xmlNodes.Attributes["name"].Value.ToUpper().Contains(".INI"))
								{
									if (updateFile.fDownloadRequired())
									{
										flag2 = true;
									}
								}
								else if (!xmlNodes.Attributes["name"].Value.ToUpper().Contains(".INI"))
								{
									if (updateFile.fDownloadRequired() && this.objDownloader.DownloadFile(updateFile.sSourcePath, string.Concat(this.sAppFolderPath, "\\", updateFile.sFileName)))
									{
										try
										{
											File.Copy(string.Concat(this.sAppFolderPath, "\\", updateFile.sFileName), string.Concat(Application.StartupPath, "\\", updateFile.sFileName), true);
										}
										catch
										{
										}
									}
								}
								else if (xmlNodes.Attributes["name"].Value.ToUpper() == string.Concat(Program.iniGSPcLocal.sIniKey, ".INI"))
								{
									string item1 = Program.iniGSPcLocal.items["INI_INFO", "LAST_MODIFIED_DATE"];
									if (item1 != null)
									{
										try
										{
											TimeSpan timeSpan = new TimeSpan();
											DateTime dateTime = DateTime.Parse(updateFile.sLastModifiedDate, new CultureInfo("fr-FR", false));
											DateTime dateTime1 = DateTime.Parse(item1, new CultureInfo("fr-FR", false));
											timeSpan = dateTime - dateTime1;
											if (timeSpan.TotalSeconds > 0)
											{
												flag2 = true;
											}
										}
										catch
										{
										}
									}
									else
									{
										flag2 = true;
									}
								}
								else if (xmlNodes.Attributes["name"].Value.ToUpper() != string.Concat(Program.iniUserSet.sIniKey, ".INI"))
								{
									IniFile[] iniFileArray = Program.iniServers;
									for (int i = 0; i < (int)iniFileArray.Length; i++)
									{
										IniFile iniFile = iniFileArray[i];
										if (xmlNodes.Attributes["name"].Value.ToUpper() == string.Concat("GSP_", iniFile.sIniKey, ".INI"))
										{
											string str1 = iniFile.items["INI_INFO", "LAST_MODIFIED_DATE"];
											if (str1 != null)
											{
												try
												{
													TimeSpan timeSpan1 = new TimeSpan();
													DateTime dateTime2 = DateTime.Parse(updateFile.sLastModifiedDate, new CultureInfo("fr-FR", false));
													DateTime dateTime3 = DateTime.Parse(str1, new CultureInfo("fr-FR", false));
													timeSpan1 = dateTime2 - dateTime3;
													if (timeSpan1.TotalSeconds > 0)
													{
														flag2 = true;
													}
												}
												catch
												{
												}
											}
											else
											{
												flag2 = true;
											}
										}
									}
								}
								else
								{
									string item2 = Program.iniUserSet.items["INI_INFO", "LAST_MODIFIED_DATE"];
									if (item2 != null)
									{
										try
										{
											TimeSpan timeSpan2 = new TimeSpan();
											DateTime dateTime4 = DateTime.Parse(updateFile.sLastModifiedDate, new CultureInfo("fr-FR", false));
											DateTime dateTime5 = DateTime.Parse(item2, new CultureInfo("fr-FR", false));
											timeSpan2 = dateTime4 - dateTime5;
											if (timeSpan2.TotalSeconds > 0)
											{
												flag2 = true;
											}
										}
										catch
										{
										}
									}
									else
									{
										flag2 = true;
									}
								}
								updateFile = null;
							}
						}
					}
					if (flag2 && File.Exists(string.Concat(Application.StartupPath, "\\UpdateManager.EXE")))
					{
						return true;
					}
					return false;
				}
				else if (!strArrays[num].ToUpper().Equals("VIEWERISUPDATED"))
				{
					num++;
				}
				else
				{
					flag = false;
					break;
				}
			}
			return flag;
		}

		private bool LoadUpdateXML(string sUpdateFileLocalPath)
		{
			bool flag;
			try
			{
				if (!File.Exists(sUpdateFileLocalPath))
				{
					flag = false;
				}
				else
				{
					this.xUpdateXml = new XmlDocument();
					this.xUpdateXml.Load(sUpdateFileLocalPath);
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private bool UpdateManagerFolderExist()
		{
			bool flag;
			this.sUpdateManageFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			this.sUpdateManageFolder = string.Concat(this.sUpdateManageFolder, "\\UpdateManager");
			try
			{
				if (Directory.Exists(this.sUpdateManageFolder))
				{
					flag = true;
				}
				else
				{
					flag = (!Directory.CreateDirectory(this.sUpdateManageFolder).Exists ? false : true);
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}
	}
}
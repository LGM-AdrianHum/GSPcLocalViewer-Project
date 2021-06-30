using GSPcLocalViewer.Properties;
using System;
using System.Collections;
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
		private const string dllZipper = "ZIPPER.dll";

		public static string sApplangEngName;

		static Global()
		{
			Global.sApplangEngName = string.Empty;
		}

		public static void AddSearchCol(string sVal, string sKey, string sScreen, int iServerId, ref DataGridView dgViewSearch)
		{
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				string[] strArrays = sVal.Split(new char[] { '|' });
				if (strArrays[0].ToString().ToUpper() == "TRUE")
				{
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
					{
						Name = sKey
					};
					try
					{
						int num = 100;
						num = int.Parse(strArrays[3].ToString());
						dataGridViewTextBoxColumn.Width = num;
					}
					catch
					{
					}
					string empty = string.Empty;
					if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
					{
						dataGridViewTextBoxColumn.HeaderText = strArrays[1];
					}
					else
					{
						dataGridViewTextBoxColumn.HeaderText = Global.GetDGHeaderCellValue(sScreen, sKey, strArrays[1], iServerId);
					}
					try
					{
						string upper = strArrays[2].ToUpper();
						if (upper.Equals("L"))
						{
							dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
							dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
						}
						else if (upper.Equals("R"))
						{
							dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
							dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
						}
						else if (upper.Equals("C"))
						{
							dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
							dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
						}
					}
					catch
					{
					}
					dgViewSearch.Columns.Add(dataGridViewTextBoxColumn);
				}
			}
			catch
			{
			}
		}

		public static void ChangeDjVUModifiedDate(string sFilePath, string sDate)
		{
			try
			{
				DateTime dateTime = DateTime.ParseExact(sDate, "dd/MM/yyyy HH:mm:ss", null);
				int num = Program.iRetryAttempts;
				int num1 = Program.iRetryInterval;
				num = (Program.iRetryAttempts != 0 ? Program.iRetryAttempts : 2);
				num1 = (Program.iRetryInterval != 0 ? Program.iRetryInterval : 500);
				bool flag = false;
				flag = Global.RetryChangeDjVUModifiedDate(sFilePath, dateTime);
				if (num > 1 && !flag)
				{
					for (int i = 0; i < num; i++)
					{
						Thread.Sleep(num1);
						flag = Global.RetryChangeDjVUModifiedDate(sFilePath, dateTime);
						if (flag)
						{
							break;
						}
					}
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
				int num = Program.iRetryAttempts;
				int num1 = Program.iRetryInterval;
				bool flag = false;
				num = (Program.iRetryAttempts != 0 ? Program.iRetryAttempts : 2);
				num1 = (Program.iRetryInterval != 0 ? Program.iRetryInterval : 500);
				flag = Global.RetryChangeDjVUModifiedDate(sFilePath, dServerDate);
				if (num > 1 && !flag)
				{
					for (int i = 0; i < num; i++)
					{
						Thread.Sleep(num1);
						flag = Global.RetryChangeDjVUModifiedDate(sFilePath, dServerDate);
						if (flag)
						{
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		public static DateTime DataUpdateDate(string sDataUpdateFilePath)
		{
			DateTime now;
			try
			{
				if (File.Exists(sDataUpdateFilePath))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(sDataUpdateFilePath);
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//filelastmodified");
					now = DateTime.Parse(xmlNodes.InnerText, new CultureInfo("fr-FR", false));
				}
				else
				{
					now = DateTime.Now;
				}
			}
			catch
			{
				now = DateTime.Now;
			}
			return now;
		}

		public static string GetDGHeaderCellValue(string sSearchScreen, string sKey, string sDefaultHeaderValue, int iServerId)
		{
			string str;
			try
			{
				string empty = string.Empty;
				IniFileIO iniFileIO = new IniFileIO();
				string[] startupPath = new string[] { Application.StartupPath, "\\Language XMLs\\", Settings.Default.appLanguage, "_GSP_", Program.iniServers[iServerId].sIniKey, ".ini" };
				string str1 = string.Concat(startupPath);
				empty = iniFileIO.GetKeyValue(sSearchScreen, sKey.ToUpper(), str1);
				str = (empty == null || empty == string.Empty ? sDefaultHeaderValue : empty);
			}
			catch
			{
				str = sDefaultHeaderValue;
			}
			return str;
		}

		public static string GetEngHeaderVal(string sSearchScreen, string sKey, int iServerId)
		{
			string empty = string.Empty;
			try
			{
				IniFileIO iniFileIO = new IniFileIO();
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[iServerId].sIniKey, ".ini");
				empty = iniFileIO.GetKeyValue(sSearchScreen, sKey, str);
			}
			catch
			{
			}
			if (empty != string.Empty)
			{
				return empty;
			}
			return string.Concat("||", sKey);
		}

		public static DateTime GetLocalDateOfFile(string sFilePath, bool bCompressed, bool bEncrypted)
		{
			DateTime dateTime;
			try
			{
				if (!File.Exists(sFilePath))
				{
					dateTime = new DateTime();
				}
				else
				{
					XmlDocument xmlDocument = new XmlDocument();
					if (!bCompressed)
					{
						xmlDocument.Load(sFilePath);
					}
					else
					{
						try
						{
							string str = sFilePath.ToLower().Replace(".zip", ".xml");
							Global.Unzip(sFilePath);
							if (File.Exists(str))
							{
								xmlDocument.Load(str);
							}
						}
						catch
						{
						}
					}
					if (bEncrypted)
					{
						string str1 = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = str1;
					}
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//PDate");
					dateTime = (xmlNodes == null ? File.GetLastWriteTime(sFilePath) : DateTime.Parse(xmlNodes.InnerText, new CultureInfo("fr-FR", false)));
				}
			}
			catch
			{
				dateTime = (!File.Exists(sFilePath) ? new DateTime() : File.GetLastWriteTime(sFilePath));
			}
			return dateTime;
		}

		public static DateTime GetLocalDateOfFile(string sFilePath, int iServerId)
		{
			DateTime dateTime;
			bool flag = false;
			bool flag1 = false;
			try
			{
				if (!File.Exists(sFilePath))
				{
					dateTime = new DateTime();
				}
				else
				{
					if (Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
					{
						flag1 = true;
					}
					if (Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
					{
						flag = true;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (!flag)
					{
						xmlDocument.Load(sFilePath);
					}
					else
					{
						try
						{
							string str = sFilePath.ToLower().Replace(".zip", ".xml");
							Global.Unzip(sFilePath);
							if (File.Exists(str))
							{
								xmlDocument.Load(str);
							}
						}
						catch
						{
						}
					}
					if (flag1)
					{
						string str1 = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = str1;
					}
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//PDate");
					dateTime = (xmlNodes == null ? File.GetLastWriteTime(sFilePath) : DateTime.Parse(xmlNodes.InnerText, new CultureInfo("fr-FR", false)));
				}
			}
			catch
			{
				dateTime = (!File.Exists(sFilePath) ? new DateTime() : File.GetLastWriteTime(sFilePath));
			}
			return dateTime;
		}

		public static DateTime GetLocalDateOfFile(string sFilePath)
		{
			DateTime dateTime;
			try
			{
				dateTime = (!File.Exists(sFilePath) ? new DateTime() : File.GetLastWriteTime(sFilePath));
			}
			catch
			{
				dateTime = (!File.Exists(sFilePath) ? new DateTime() : File.GetLastWriteTime(sFilePath));
			}
			return dateTime;
		}

		public static DateTime GetServerUpdateDateFromXmlNode(XmlNode xSchemaNode, XmlNode objXmlNode)
		{
			DateTime dateTime;
			try
			{
				string empty = string.Empty;
				DateTime dateTime1 = new DateTime();
				foreach (XmlAttribute attribute in xSchemaNode.Attributes)
				{
					if (attribute.Value.ToUpper() != "UPDATEDATE")
					{
						continue;
					}
					empty = attribute.Name;
				}
				foreach (XmlAttribute xmlAttribute in objXmlNode.Attributes)
				{
					if (!xmlAttribute.Name.Equals(empty))
					{
						continue;
					}
					dateTime1 = DateTime.Parse(objXmlNode.Attributes[xmlAttribute.Name].Value, new CultureInfo("fr-FR", false));
					break;
				}
				dateTime = dateTime1;
			}
			catch
			{
				dateTime = new DateTime();
			}
			return dateTime;
		}

		public static bool IntervalElapsed(DateTime dtLocal, DateTime dtServer, int iInterval)
		{
			bool flag;
			try
			{
				flag = ((dtServer.Date - dtLocal.Date).TotalDays < (double)iInterval ? false : true);
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		private static bool RetryChangeDjVUModifiedDate(string sFilePath, DateTime dServerDate)
		{
			bool flag;
			try
			{
				File.SetLastWriteTime(sFilePath, dServerDate);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public static void SaveToLanguageIni(string sSearchScreen, string sKey, string sHeaderValue, int iServerId)
		{
			try
			{
				string empty = string.Empty;
				IniFileIO iniFileIO = new IniFileIO();
				string[] startupPath = new string[] { Application.StartupPath, "\\Language XMLs\\", Global.sApplangEngName, "_GSP_", Program.iniServers[iServerId].sIniKey, ".ini" };
				string str = string.Concat(startupPath);
				iniFileIO.WriteValue(sSearchScreen, sKey.ToUpper(), sHeaderValue, str);
			}
			catch
			{
			}
		}

		public static void SaveToServerIni(string sSearchScreen, string sKey, string sHeaderValue, int iServerId)
		{
			try
			{
				string empty = string.Empty;
				IniFileIO iniFileIO = new IniFileIO();
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[iServerId].sIniKey, ".ini");
				iniFileIO.WriteValue(sSearchScreen, sKey.ToUpper(), sHeaderValue, str);
			}
			catch
			{
			}
		}

		public static bool SecurityLocksOpen(XmlNode bookNode, XmlNode schemaNode, int ServerId, frmViewer frmParent)
		{
			bool flag;
			try
			{
				string empty = string.Empty;
				string[] strArrays = null;
				string item = string.Empty;
				bool flag1 = false;
				AES aE = new AES();
				foreach (XmlAttribute attribute in schemaNode.Attributes)
				{
					if (!attribute.Value.ToUpper().Equals("SECURITYLOCKS"))
					{
						continue;
					}
					empty = attribute.Name;
				}
				if (empty == string.Empty)
				{
					flag1 = true;
				}
				if (bookNode.Attributes[empty] == null || !(bookNode.Attributes[empty].Value != string.Empty))
				{
					flag = true;
				}
				else
				{
					string value = bookNode.Attributes[empty].Value;
					if (value == string.Empty)
					{
						flag1 = true;
					}
					else
					{
						value = aE.DLLDecode(bookNode.Attributes[empty].Value, "0123456789ABCDEF");
						if (value == string.Empty)
						{
							value = bookNode.Attributes[empty].Value;
						}
						strArrays = value.Split(new char[] { ',' });
						if (Program.iniServers[ServerId].items["SETTINGS", "SECURITY"] != null)
						{
							item = Program.iniServers[ServerId].items["SETTINGS", "SECURITY"];
							item = aE.DLLDecode(item, "0123456789ABCDEF");
							string[] strArrays1 = item.Split(new char[] { ',' });
							flag1 = false;
							for (int i = 0; i < (int)strArrays1.Length; i++)
							{
								int num = 0;
								while (num < (int)strArrays.Length)
								{
									if (strArrays1[i] != strArrays[num])
									{
										num++;
									}
									else
									{
										flag1 = true;
										break;
									}
								}
							}
						}
						else
						{
							flag1 = false;
						}
					}
					if (flag1)
					{
						flag = true;
					}
					else
					{
						frmSecurity _frmSecurity = new frmSecurity(strArrays);
						if (_frmSecurity.ShowDialog(frmParent) != DialogResult.OK)
						{
							flag = false;
						}
						else
						{
							item = (item != string.Empty ? string.Concat(item, ",", _frmSecurity.Key) : string.Concat(item, _frmSecurity.Key));
							item = aE.DLLEncode(item, "0123456789ABCDEF");
							Program.iniServers[ServerId].UpdateItem("SETTINGS", "SECURITY", item);
							flag = true;
						}
					}
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public static void SetDGHeaderCellValue(string sSearchScreen, string sKey, string sHeaderValue, int iServerId)
		{
			try
			{
				string empty = string.Empty;
				IniFileIO iniFileIO = new IniFileIO();
				string[] startupPath = new string[] { Application.StartupPath, "\\Language XMLs\\", Global.sApplangEngName, "_GSP_", Program.iniServers[iServerId].sIniKey, ".ini" };
				string str = string.Concat(startupPath);
				iniFileIO.WriteValue(sSearchScreen, sKey.ToUpper(), sHeaderValue, str);
			}
			catch
			{
			}
		}

		public static void Unzip(string sZipFilePath)
		{
			int num;
			int num1;
			int num2 = -11;
			try
			{
				if (!File.Exists(sZipFilePath.ToLower().Replace(".zip", ".xml")))
				{
					num1 = (Program.iRetryAttempts != 0 ? Program.iRetryAttempts : 2);
					num = (Program.iRetryInterval != 0 ? Program.iRetryInterval : 500);
					if (Path.GetExtension(sZipFilePath).ToLower() == ".zip")
					{
						for (int i = 0; i < num1; i++)
						{
							try
							{
								if (!File.Exists(sZipFilePath))
								{
									break;
								}
								else
								{
									num2 = Global.UnZipFile(sZipFilePath).ToInt32();
									if (num2 != 1 || !File.Exists(sZipFilePath.ToLower().Replace(".zip", ".xml")))
									{
										Thread.Sleep(num);
									}
									else
									{
										break;
									}
								}
							}
							catch
							{
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		[DllImport("ZIPPER.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern IntPtr UnZipFile(string sFilePath);

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
	}
}
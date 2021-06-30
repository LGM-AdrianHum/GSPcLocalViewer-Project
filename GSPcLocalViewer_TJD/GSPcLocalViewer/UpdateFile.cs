using System;
using System.Collections;
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
			this.iVersion = null;
			this.bForceFullUpdate = false;
			this.objOperation = new UpdateFile.Operations();
			this.sFileExtension = string.Empty;
			try
			{
				if (xFileNode.Attributes["name"] != null)
				{
					this.sFileName = xFileNode.Attributes["name"].Value;
				}
				foreach (XmlNode childNode in xFileNode.ChildNodes)
				{
					if (childNode.Name.ToUpper().Equals("SOURCEPATH"))
					{
						if (childNode.InnerText.Contains("://"))
						{
							this.sSourcePath = childNode.InnerText;
						}
						else
						{
							this.sSourcePath = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
							this.sSourcePath = this.sSourcePath.Substring(0, this.sSourcePath.LastIndexOf("/"));
							UpdateFile updateFile = this;
							updateFile.sSourcePath = string.Concat(updateFile.sSourcePath, "/", childNode.InnerText);
						}
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
					{
						this.sLastModifiedDate = childNode.InnerText;
					}
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
					{
						this.bForceFullUpdate = bool.Parse(childNode.InnerText);
					}
					if (!childNode.Name.ToUpper().Equals("OPERATIONS"))
					{
						continue;
					}
					string innerText = childNode.InnerText;
					if (innerText.Length != 8)
					{
						int length = 8 - innerText.Length;
						for (int i = 0; i < length; i++)
						{
							innerText = string.Concat("0", innerText);
						}
					}
					char[] charArray = innerText.ToCharArray();
					this.objOperation.Activate = (charArray[(int)charArray.Length - 1] == '1' ? true : false);
					this.objOperation.Execute = (charArray[(int)charArray.Length - 2] == '1' ? true : false);
					this.objOperation.Delete = (charArray[(int)charArray.Length - 3] == '1' ? true : false);
					this.objOperation.Register = (charArray[(int)charArray.Length - 4] == '1' ? true : false);
					this.objOperation.UnRegister = (charArray[(int)charArray.Length - 5] == '1' ? true : false);
					this.objOperation.Update = (charArray[(int)charArray.Length - 6] == '1' ? true : false);
					this.sFileExtension = (new FileInfo(this.sFileName)).Extension.ToUpper();
					if (this.sFileExtension.Equals(".OCX") && this.objOperation.Delete)
					{
						this.objOperation.UnRegister = true;
					}
					if (this.sFileExtension.Equals(".OCX") && this.objOperation.Activate)
					{
						this.objOperation.Register = true;
					}
					if ((this.sFileExtension.Equals(".INI") || this.sFileExtension.Equals(".CSV") || this.sFileExtension.Equals(".TXT")) && this.objOperation.Activate && this.objOperation.Update)
					{
						if (!File.Exists(this.sTargetPath))
						{
							this.objOperation.Update = false;
						}
						else
						{
							this.objOperation.Activate = false;
						}
					}
					if (!this.sFileExtension.Equals(".EXE"))
					{
						continue;
					}
					this.objOperation.Update = false;
				}
			}
			catch
			{
			}
		}

		public bool fDownloadRequired()
		{
			bool flag;
			try
			{
				TimeSpan lastWriteTime = new TimeSpan();
				if (!this.sTargetPath.Contains(":\\"))
				{
					this.sTargetPath = string.Concat(Application.StartupPath, "\\", this.sTargetPath);
				}
				if (File.Exists(this.sTargetPath))
				{
					try
					{
						DateTime dateTime = DateTime.Parse(this.sLastModifiedDate, new CultureInfo("fr-FR", false));
						lastWriteTime = dateTime - File.GetLastWriteTime(this.sTargetPath);
					}
					catch
					{
					}
				}
				Version version = this.GetVersion(this.sTargetPath);
				if (!File.Exists(this.sTargetPath) && this.objOperation.Activate)
				{
					flag = true;
				}
				else if (File.Exists(this.sTargetPath) && lastWriteTime.TotalSeconds > 0)
				{
					flag = true;
				}
				else if (!this.bForceFullUpdate)
				{
					flag = (version >= this.iVersion ? false : true);
				}
				else
				{
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public Version GetVersion(string sfileName)
		{
			Version version;
			try
			{
				version = new Version(FileVersionInfo.GetVersionInfo(sfileName).FileVersion);
			}
			catch
			{
				version = this.iVersion;
			}
			return version;
		}

		public class Operations
		{
			public bool Activate;

			public bool Execute;

			public bool Delete;

			public bool Register;

			public bool UnRegister;

			public bool Update;

			public Operations()
			{
			}
		}
	}
}
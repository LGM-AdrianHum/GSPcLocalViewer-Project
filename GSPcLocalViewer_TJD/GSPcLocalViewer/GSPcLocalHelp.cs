using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class GSPcLocalHelp
	{
		private frmViewer frmParent;

		private string sHelpFileName = string.Empty;

		private string sHelpFileServerPath = string.Empty;

		private string sHelpFileLocalPath = string.Empty;

		private string sHelpUrlPath = string.Empty;

		private string sHelpType = "URL";

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

		private void DownloadHelpFile()
		{
			try
			{
				if (!File.Exists(this.sHelpFileLocalPath))
				{
					this.objDownloader.DownloadFile(this.sHelpFileServerPath, this.sHelpFileLocalPath);
				}
			}
			catch
			{
			}
		}

		private void GetHelpFileType()
		{
			try
			{
				if (Program.iniGSPcLocal.items["HELP_FILE", "TYPE"] != null)
				{
					if (Program.iniGSPcLocal.items["HELP_FILE", "TYPE"].ToUpper() != "FILE")
					{
						this.sHelpType = "URL";
					}
					else
					{
						this.sHelpType = "FILE";
					}
				}
			}
			catch
			{
			}
		}

		private void InitializePaths()
		{
			try
			{
				if (Program.iniGSPcLocal.items["HELP_FILE", "FILENAME"] == null)
				{
					this.sHelpUrlPath = Program.iniGSPcLocal.items["HELP_FILE", "URL"];
				}
				else
				{
					this.sHelpFileName = Program.iniGSPcLocal.items["HELP_FILE", "FILENAME"];
					this.sHelpFileLocalPath = string.Concat(Application.StartupPath, "\\", this.sHelpFileName);
					this.sHelpFileServerPath = Program.iniGSPcLocal.items["SETTINGS", "GSP_UPDATE_PATH"];
					this.sHelpFileServerPath = string.Concat(this.sHelpFileServerPath.Substring(0, this.sHelpFileServerPath.LastIndexOf("/")), "/", this.sHelpFileName);
					this.sHelpUrlPath = Program.iniGSPcLocal.items["HELP_FILE", "URL"];
				}
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
				if (this.sHelpType.ToUpper() != "FILE")
				{
					this.frmParent.OpenSpecifiedBrowser(this.sHelpUrlPath);
				}
				else
				{
					this.DownloadHelpFile();
					if (File.Exists(this.sHelpFileLocalPath))
					{
						Process.Start(this.sHelpFileLocalPath);
					}
				}
			}
			catch
			{
			}
		}
	}
}
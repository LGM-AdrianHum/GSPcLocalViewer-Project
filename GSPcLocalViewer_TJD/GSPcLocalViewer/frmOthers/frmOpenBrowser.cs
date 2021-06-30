using GSPcLocalViewer;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmOthers
{
	public class frmOpenBrowser : Form
	{
		public int ServerId;

		private string strMyURL = string.Empty;

		private IContainer components;

		public frmOpenBrowser(string strURL, int iServerId)
		{
			this.InitializeComponent();
			this.strMyURL = strURL;
			this.ServerId = iServerId;
		}

		public frmOpenBrowser()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmOpenBrowser_Shown(object sender, EventArgs e)
		{
			(new Thread(() => this.OpenURLInBrowser(this.strMyURL))).Start();
			base.Close();
		}

		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(322, 79);
			base.Name = "frmOpenBrowser";
			base.Opacity = 0;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.Shown += new EventHandler(this.frmOpenBrowser_Shown);
			base.ResumeLayout(false);
		}

		private void OpenURLInBrowser(string sSeed)
		{
			try
			{
				if (sSeed != string.Empty && sSeed != null)
				{
					string item = Program.iniServers[this.ServerId].items["SETTINGS", "REF_URL"];
					if (!(item != string.Empty) || item == null)
					{
						MessageBox.Show("(E-VWR-EM013) URL not found");
					}
					else
					{
						item = string.Concat(item, sSeed);
						string str = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
						if (str == string.Empty || str == null)
						{
							str = "iexplore";
						}
						string empty = string.Empty;
						RegistryReader registryReader = new RegistryReader();
						empty = registryReader.Read(string.Concat("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", str, ".exe"), string.Empty);
						if (empty != string.Empty && empty != null)
						{
							if (!(item != string.Empty) || item == null)
							{
								MessageBox.Show("(E-VWR-EM013) URL not found");
							}
							else
							{
								using (Process process = Process.Start(empty, item))
								{
									if (process != null)
									{
										IntPtr handle = process.Handle;
										frmOpenBrowser.SetForegroundWindow(process.Handle);
										frmOpenBrowser.SetActiveWindow(process.Handle);
									}
									process.WaitForExit();
								}
							}
						}
						else if (registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty) != null)
						{
							using (Process process1 = Process.Start(empty, item))
							{
								if (process1 != null)
								{
									IntPtr intPtr = process1.Handle;
									frmOpenBrowser.SetForegroundWindow(process1.Handle);
									frmOpenBrowser.SetActiveWindow(process1.Handle);
								}
								process1.WaitForExit();
							}
						}
						else
						{
							using (Process process2 = Process.Start(empty, item))
							{
								if (process2 != null)
								{
									IntPtr handle1 = process2.Handle;
									frmOpenBrowser.SetForegroundWindow(process2.Handle);
									frmOpenBrowser.SetActiveWindow(process2.Handle);
								}
								process2.WaitForExit();
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetActiveWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);
	}
}
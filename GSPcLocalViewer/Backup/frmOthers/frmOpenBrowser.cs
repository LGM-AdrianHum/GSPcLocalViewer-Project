// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOthers.frmOpenBrowser
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmOthers
{
  public class frmOpenBrowser : Form
  {
    private string strMyURL = string.Empty;
    public int ServerId;
    private IContainer components;

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool SetActiveWindow(IntPtr hWnd);

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

    private void frmOpenBrowser_Shown(object sender, EventArgs e)
    {
      new Thread((ThreadStart) (() => this.OpenURLInBrowser(this.strMyURL))).Start();
      this.Close();
    }

    private void OpenURLInBrowser(string sSeed)
    {
      try
      {
        if (!(sSeed != string.Empty) || sSeed == null)
          return;
        string str1 = Program.iniServers[this.ServerId].items["SETTINGS", "REF_URL"];
        if (str1 != string.Empty && str1 != null)
        {
          string arguments = str1 + sSeed;
          string str2 = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
          if (str2 == string.Empty || str2 == null)
            str2 = "iexplore";
          string empty = string.Empty;
          RegistryReader registryReader = new RegistryReader();
          string fileName = registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + str2 + ".exe", string.Empty);
          if (fileName != string.Empty && fileName != null)
          {
            if (arguments != string.Empty && arguments != null)
            {
              using (Process process = Process.Start(fileName, arguments))
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
            else
            {
              int num = (int) MessageBox.Show("(E-VWR-EM013) URL not found");
            }
          }
          else if (registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty) == null)
          {
            using (Process process = Process.Start(fileName, arguments))
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
          else
          {
            using (Process process = Process.Start(fileName, arguments))
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
        else
        {
          int num1 = (int) MessageBox.Show("(E-VWR-EM013) URL not found");
        }
      }
      catch
      {
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(322, 79);
      this.Name = nameof (frmOpenBrowser);
      this.Opacity = 0.0;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Shown += new EventHandler(this.frmOpenBrowser_Shown);
      this.ResumeLayout(false);
    }
  }
}

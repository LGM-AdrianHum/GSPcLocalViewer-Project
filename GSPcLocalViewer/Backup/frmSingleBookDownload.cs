// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmSingleBookDownload
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using FileDownloaderDLL;
using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class frmSingleBookDownload : Form
  {
    private frmViewer frmParent;
    private string sLocalPath;
    private string sServerPath;
    private bool isWorking;
    private bool isListLoaded;
    private int iSuccessCount;
    private FileDownloader objFileDownloader;
    public frmSingleBookDownload.UpdateProgressbarsDel progrDelegate;
    private IContainer components;
    private Panel pnlForm;
    private BackgroundWorker bgWorker;
    private Panel pnlControl;
    private ProgressBar progressOverall;
    private ProgressBar progressCurrentFile;
    private Panel pnlFiles;
    private Button btnCancel;
    private Button btnDownload;
    private ListView listBooks;
    private ColumnHeader colHeadFileName;
    private ColumnHeader colHeadStatus;
    private Label lblDownloaded;
    private BackgroundWorker bgLoader;
    private PictureBox picLoading;
    private StatusStrip ssStatus;
    private ToolStripStatusLabel lblStatus;
    private Label lblOverallProgress;
    private Label lblImageProgress;
    private Label lblCurrentProgress;

    public frmSingleBookDownload(frmViewer frm, string _sLocalPath, string _sServerPath)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.sLocalPath = _sLocalPath;
      this.sServerPath = _sServerPath;
      this.progressOverall.Maximum = 100;
      this.progressOverall.Minimum = 0;
      this.progressOverall.Value = 0;
      this.progressCurrentFile.Value = 0;
      this.UpdateFont();
      this.UpdateStatus(string.Empty);
      this.isWorking = false;
      this.isListLoaded = false;
      this.LoadResources();
    }

    private void frmBookDownload_Load(object sender, EventArgs e)
    {
      this.listBooks.Columns[0].Width = 300;
      this.listBooks.Columns[1].Width = 100;
      this.btnDownload.Enabled = false;
      this.btnCancel.Enabled = false;
      this.UpdateStatus(this.GetResource("Checking files to download……", "CHECKING_TO_DOWNLOAD", ResourceType.STATUS_MESSAGE));
      this.ShowLoading(this.pnlFiles);
      this.bgLoader.RunWorkerAsync();
      this.isListLoaded = true;
    }

    private void frmBookDownload_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      this.frmParent.HideDimmer();
    }

    private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        ArrayList arrayList = this.frmParent.SingleBookDownloadList(this.sLocalPath);
        this.ListClearItems();
        for (int index = 0; index < arrayList.Count; ++index)
        {
          try
          {
            string[] strArray = arrayList[index].ToString().Split(new string[1]
            {
              "|*|*|"
            }, StringSplitOptions.None);
            ListViewItem listViewItem = new ListViewItem(new string[2]
            {
              strArray[0],
              string.Empty
            });
            if (strArray.Length == 2)
              listViewItem.Tag = (object) strArray[1];
            this.ListAddItem(listViewItem);
          }
          catch
          {
          }
        }
      }
      catch
      {
      }
    }

    private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.progressCurrentFile.Minimum = 0;
      this.progressCurrentFile.Maximum = 100;
      this.progressCurrentFile.Value = 0;
      this.progressOverall.Minimum = 0;
      this.progressOverall.Maximum = this.listBooks.Items.Count;
      this.progressOverall.Value = 0;
      this.HideLoading(this.pnlFiles);
      this.btnDownload.Enabled = true;
      this.btnCancel.Enabled = true;
      if (this.listBooks.Items.Count == 0)
        this.UpdateStatus(this.GetResource("Book is already updated.", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
      else
        this.UpdateStatus(this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE));
      if (this.isListLoaded)
        return;
      this.isListLoaded = true;
      this.progressOverall.Value = 0;
      this.progressCurrentFile.Value = 0;
      this.btnDownload.Enabled = false;
      this.UpdateStatus(string.Empty);
      this.ShowLoading(this.pnlFiles);
      if (this.isWorking)
        return;
      this.isWorking = true;
      this.bgWorker.RunWorkerAsync();
    }

    private void btnDownload_Click(object sender, EventArgs e)
    {
      if (!this.isListLoaded)
      {
        this.btnDownload.Enabled = false;
        this.btnCancel.Enabled = false;
        this.UpdateStatus(this.GetResource("Checking files to download……", "CHECKING_TO_DOWNLOAD", ResourceType.STATUS_MESSAGE));
        this.bgLoader.RunWorkerAsync();
        this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      }
      else
      {
        this.progressOverall.Value = 0;
        this.progressCurrentFile.Value = 0;
        this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
        this.btnDownload.Enabled = false;
        this.UpdateStatus(string.Empty);
        if (this.isWorking)
          return;
        this.isWorking = true;
        new Thread(new ThreadStart(this.DownloadFilesList))
        {
          Name = "DownloadThread"
        }.Start();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      if (this.isWorking)
      {
        this.isWorking = false;
        Application.DoEvents();
        this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
      }
      else
        this.Close();
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        this.picLoading.Parent = (Control) parentPanel;
        this.picLoading.Left = parentPanel.Width / 2 - this.picLoading.Width / 2;
        this.picLoading.Top = parentPanel.Height / 2 - this.picLoading.Height / 2;
        this.picLoading.BringToFront();
        this.picLoading.Show();
      }
      catch
      {
      }
    }

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        this.picLoading.Hide();
      }
      catch
      {
      }
    }

    private void ListAddItem(ListViewItem item)
    {
      if (this.listBooks.InvokeRequired)
        this.listBooks.Invoke((Delegate) new frmSingleBookDownload.ListAddItemDelegate(this.ListAddItem), (object) item);
      else
        this.listBooks.Items.Add(item);
    }

    private void ListClearItems()
    {
      if (this.listBooks.InvokeRequired)
        this.listBooks.Invoke((Delegate) new frmSingleBookDownload.ListClearItemsDelegate(this.ListClearItems));
      else
        this.listBooks.Items.Clear();
    }

    private void ListEditStatus(int index, string status)
    {
      if (this.listBooks.InvokeRequired)
      {
        this.listBooks.Invoke((Delegate) new frmSingleBookDownload.ListEditStatusDelegate(this.ListEditStatus), (object) index, (object) status);
      }
      else
      {
        if (this.listBooks.Columns.Count > 1)
          this.listBooks.Items[index].SubItems[1].Text = status;
        try
        {
          this.listBooks.EnsureVisible(index);
        }
        catch
        {
        }
      }
    }

    private string ListGetFileName(int index)
    {
      if (this.listBooks.InvokeRequired)
        return (string) this.listBooks.Invoke((Delegate) new frmSingleBookDownload.getFileNameDelegate(this.ListGetFileName), (object) index);
      if (this.listBooks.Columns.Count > 0)
        return this.listBooks.Items[index].SubItems[0].Text;
      return string.Empty;
    }

    private string ListGetFileDateFromListNodeTag(int index)
    {
      if (this.listBooks.InvokeRequired)
        return (string) this.listBooks.Invoke((Delegate) new frmSingleBookDownload.getFileDateFromListNodeTagDelegate(this.ListGetFileDateFromListNodeTag), (object) index);
      try
      {
        if (this.listBooks.Columns.Count > 0)
          return this.listBooks.Items[index].Tag.ToString();
        return string.Empty;
      }
      catch
      {
        return string.Empty;
      }
    }

    private void OverallProgressUpdate(int value)
    {
      if (this.progressOverall.InvokeRequired)
      {
        this.progressOverall.Invoke((Delegate) new frmSingleBookDownload.OverallProgressUpdateDelegate(this.OverallProgressUpdate), (object) value);
      }
      else
      {
        this.lblOverallProgress.Text = this.GetResource("Overall Progress:", "OVERALL_PROGRESS", ResourceType.LABEL) + value.ToString() + " / " + (object) this.listBooks.Items.Count;
        this.progressOverall.Value = value;
      }
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }

    private void UpdateStatus(string status)
    {
      if (this.ssStatus.InvokeRequired)
        this.ssStatus.Invoke((Delegate) new frmSingleBookDownload.StatusDelegate(this.UpdateStatus), (object) status);
      else
        this.lblStatus.Text = status;
    }

    private void UpdateProgress(string progressStatus)
    {
      if (this.lblImageProgress.InvokeRequired)
        this.lblImageProgress.Invoke((Delegate) new frmSingleBookDownload.StatusDelegate(this.UpdateProgress), (object) progressStatus);
      else
        this.lblImageProgress.Text = progressStatus;
    }

    private bool DownloadFile(string surl1, string sLocalPath)
    {
      Application.DoEvents();
      FileDownloader fileDownloader = new FileDownloader();
      this.bgWorker.ReportProgress(0);
      try
      {
        int result = 0;
        int.TryParse(Settings.Default.appProxyTimeOut, out result);
        if (Settings.Default.appProxyType == "0")
        {
          if (fileDownloader.DownloadingFile(surl1, sLocalPath, Settings.Default.appProxyTimeOut + "000") == 1)
            return false;
          long fileLength = (long) fileDownloader.FileLength;
          this.progressCurrentFile.Minimum = 0;
          this.progressCurrentFile.Maximum = 100;
          int num = 0;
          DateTime now = DateTime.Now;
          long bytesDownloaded;
          int percentProgress;
          TimeSpan timeSpan;
          do
          {
            bytesDownloaded = (long) fileDownloader.BytesDownloaded;
            percentProgress = (int) (bytesDownloaded * 100L / fileLength);
            if (this.frmParent.IsDisposed)
            {
              fileDownloader.StopDownloadingFile();
              return false;
            }
            if (num != percentProgress)
            {
              now = DateTime.Now;
              num = percentProgress;
              this.bgWorker.ReportProgress(percentProgress);
            }
            timeSpan = DateTime.Now - now;
          }
          while (bytesDownloaded < fileLength && timeSpan.Seconds < result);
          this.bgWorker.ReportProgress(percentProgress);
          return true;
        }
        switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
        {
          case 1:
            return false;
          case 407:
            try
            {
              Program.bShowProxyScreen = true;
              if (!Dashbord.bShowProxy)
                return false;
              this.ShowProxyAuthWarning();
              while (Program.bShowProxyScreen)
              {
                switch (fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
                {
                  case 1:
                    Program.bShowProxyScreen = false;
                    return false;
                  case 407:
                    if (!Dashbord.bShowProxy)
                      return false;
                    this.ShowProxyAuthWarning();
                    continue;
                  default:
                    Program.bShowProxyScreen = false;
                    long fileLength = (long) fileDownloader.FileLength;
                    this.progressCurrentFile.Minimum = 0;
                    this.progressCurrentFile.Maximum = 100;
                    int num = 0;
                    DateTime now = DateTime.Now;
                    long bytesDownloaded;
                    int percentProgress;
                    TimeSpan timeSpan;
                    do
                    {
                      bytesDownloaded = (long) fileDownloader.BytesDownloaded;
                      percentProgress = (int) (bytesDownloaded * 100L / fileLength);
                      if (this.frmParent.IsDisposed)
                      {
                        fileDownloader.StopDownloadingFile();
                        return false;
                      }
                      if (num != percentProgress)
                      {
                        now = DateTime.Now;
                        num = percentProgress;
                        this.bgWorker.ReportProgress(percentProgress);
                      }
                      timeSpan = DateTime.Now - now;
                    }
                    while (bytesDownloaded < fileLength && timeSpan.Seconds < result);
                    this.bgWorker.ReportProgress(percentProgress);
                    return true;
                }
              }
              return true;
            }
            catch (Exception ex)
            {
              return false;
            }
          default:
            long fileLength1 = (long) fileDownloader.FileLength;
            this.progressCurrentFile.Minimum = 0;
            this.progressCurrentFile.Maximum = 100;
            int num1 = 0;
            DateTime now1 = DateTime.Now;
            long bytesDownloaded1;
            int percentProgress1;
            TimeSpan timeSpan1;
            do
            {
              bytesDownloaded1 = (long) fileDownloader.BytesDownloaded;
              percentProgress1 = (int) (bytesDownloaded1 * 100L / fileLength1);
              if (this.frmParent.IsDisposed)
              {
                fileDownloader.StopDownloadingFile();
                return false;
              }
              if (num1 != percentProgress1)
              {
                now1 = DateTime.Now;
                num1 = percentProgress1;
                this.bgWorker.ReportProgress(percentProgress1);
              }
              timeSpan1 = DateTime.Now - now1;
            }
            while (bytesDownloaded1 < fileLength1 && timeSpan1.Seconds < result);
            this.bgWorker.ReportProgress(percentProgress1);
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

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      string empty = string.Empty;
      this.iSuccessCount = 0;
      DataSize.miliSecInterval = 2000;
      for (int index = 0; index < this.listBooks.Items.Count && this.isWorking; ++index)
      {
        if (!this.IsDisposed)
        {
          string fileName = this.ListGetFileName(index);
          this.UpdateProgress(this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL) + " " + fileName);
          Console.WriteLine(this.sLocalPath + "\\" + fileName);
          if (this.DownloadFile(this.sServerPath + "/" + fileName, this.sLocalPath + "\\" + fileName))
          {
            this.ListEditStatus(index, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
            ++this.iSuccessCount;
          }
          else
            this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
          this.OverallProgressUpdate(index + 1);
          if (DataSize.spaceLeft < 10485760L)
            this.btnCancel_Click((object) null, (EventArgs) null);
        }
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (this.isWorking)
      {
        this.isWorking = false;
        if (this.listBooks.Items.Count == 0)
        {
          this.UpdateStatus(this.GetResource("Book is already updated.", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
        }
        else
        {
          this.UpdateProgress("");
          this.lblCurrentProgress.Text = "";
        }
      }
      else
        this.UpdateStatus(this.GetResource("Downloading cancelled.", "DOWNLOADING_CANCELLED", ResourceType.STATUS_MESSAGE));
      this.isListLoaded = false;
      this.HideLoading(this.pnlFiles);
      this.btnDownload.Enabled = true;
      this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
      if (DataSize.spaceLeft >= 10485760L)
        return;
      int num = (int) MessageBox.Show((IWin32Window) this.frmParent, this.GetResource("Book download canceled because maximum download limit was reached", "MAXIMUM_LIMIT_REACHED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.btnCancel_Click((object) null, (EventArgs) null);
      DataSize.miliSecInterval = 20000;
    }

    private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      if (this.progressCurrentFile.Value == e.ProgressPercentage)
        return;
      this.lblCurrentProgress.Text = e.ProgressPercentage.ToString() + "%";
      this.progressCurrentFile.Value = e.ProgressPercentage;
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.TITLE);
      this.btnDownload.Text = this.GetResource("Download", "DOWNLOAD", ResourceType.BUTTON);
      this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
      this.listBooks.Columns[0].Text = this.GetResource("File Name", "FILE_NAME", ResourceType.LIST_VIEW);
      this.listBooks.Columns[1].Text = this.GetResource("Status", "STATUS", ResourceType.LIST_VIEW);
      this.lblStatus.Text = this.GetResource("Ready", "READY", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='SINGLE_BOOK_DOWNLOAD']";
        switch (rType)
        {
          case ResourceType.BUTTON:
            str += "/Resources[@Name='BUTTON']";
            break;
          case ResourceType.LABEL:
            str += "/Resources[@Name='LABEL']";
            break;
          case ResourceType.CHECK_BOX:
            str += "/Resources[@Name='CHECK_BOX']";
            break;
          case ResourceType.RADIO_BUTTON:
            str += "/Resources[@Name='RADIO_BUTTON']";
            break;
          case ResourceType.TITLE:
            string xQuery1 = str + "[@Name='" + sKey + "']";
            return this.frmParent.GetResourceValue(sDefaultValue, xQuery1);
          case ResourceType.COMBO_BOX:
            str += "/Resources[@Name='COMBO_BOX']";
            break;
          case ResourceType.LIST_VIEW:
            str += "/Resources[@Name='LIST_VIEW']";
            break;
          case ResourceType.GRID_VIEW:
            str += "/Resources[@Name='GRID_VIEW']";
            break;
          case ResourceType.TOOLSTRIP:
            str += "/Resources[@Name='TOOLSTRIP']";
            break;
          case ResourceType.MENU_BAR:
            str += "/Resources[@Name='MENU_BAR']";
            break;
          case ResourceType.CONTEXT_MENU:
            str += "/Resources[@Name='CONTEXT_MENU']";
            break;
          case ResourceType.STATUS_MESSAGE:
            str += "/Resources[@Name='STATUS_MESSAGE']";
            break;
          case ResourceType.POPUP_MESSAGE:
            str += "/Resources[@Name='POPUP_MESSAGE']";
            break;
        }
        string xQuery2 = str + "/Resource[@Name='" + sKey + "']";
        return this.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private void DownloadFilesList()
    {
      int index = 0;
      try
      {
        int result = 0;
        int.TryParse(Settings.Default.appProxyTimeOut, out result);
        string empty = string.Empty;
        this.iSuccessCount = 0;
        this.objFileDownloader = new FileDownloader();
        int num1 = 0;
label_70:
        for (index = 0; index < this.listBooks.Items.Count && this.isWorking; ++index)
        {
          if (!this.IsDisposed)
          {
            string fileName = this.ListGetFileName(index);
            this.UpdateProgress(this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL) + " " + fileName);
            if (Settings.Default.appProxyType == "0")
            {
              if (this.objFileDownloader.DownloadingFile(this.sServerPath + "/" + fileName, this.sLocalPath + "\\" + fileName, Settings.Default.appProxyTimeOut + "000") == 1)
              {
                this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
              }
              else
              {
                this.Invoke((Delegate) new frmSingleBookDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
                int num2 = 0;
                DateTime now = DateTime.Now;
                TimeSpan timeSpan;
                do
                {
                  if (this.frmParent.IsDisposed)
                  {
                    this.objFileDownloader.StopDownloadingFile();
                    if (Thread.CurrentThread.Name == "DownloadThread")
                      Thread.CurrentThread.Abort();
                  }
                  long bytesDownloaded = (long) this.objFileDownloader.BytesDownloaded;
                  num1 = (int) (bytesDownloaded * 100L / (long) this.objFileDownloader.FileLength);
                  if (num2 != num1)
                  {
                    now = DateTime.Now;
                    num2 = num1;
                    this.progrDelegate = new frmSingleBookDownload.UpdateProgressbarsDel(this.UpdateProgress);
                    this.Invoke((Delegate) this.progrDelegate, (object) num1);
                  }
                  Application.DoEvents();
                  timeSpan = DateTime.Now - now;
                  if (timeSpan.Seconds >= result)
                    Console.WriteLine(this.sServerPath + "/" + fileName);
                  if (bytesDownloaded >= (long) this.objFileDownloader.FileLength)
                    break;
                }
                while (timeSpan.Seconds < result);
                try
                {
                  if (!fileName.ToUpper().EndsWith("XML"))
                  {
                    if (!fileName.ToUpper().EndsWith("ZIP"))
                      Global.ChangeDjVUModifiedDate(this.sLocalPath + "\\" + fileName, this.ListGetFileDateFromListNodeTag(index));
                  }
                }
                catch
                {
                }
                this.ListEditStatus(index, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
                ++this.iSuccessCount;
                this.OverallProgressUpdate(index + 1);
                if (DataSize.spaceLeft < 10485760L)
                  this.btnCancel_Click((object) null, (EventArgs) null);
              }
            }
            else
            {
              switch (this.objFileDownloader.DownloadingFileWithProxy(this.sServerPath + "/" + fileName, this.sLocalPath + "\\" + fileName, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
              {
                case 1:
                  this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
                  continue;
                case 407:
                  try
                  {
                    Program.bShowProxyScreen = true;
                    if (Dashbord.bShowProxy)
                      this.ShowProxyAuthWarning();
                    else
                      this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
                    while (Program.bShowProxyScreen)
                    {
                      if (Dashbord.bShowProxy)
                      {
                        switch (this.objFileDownloader.DownloadingFileWithProxy(this.sServerPath + "/" + fileName, this.sLocalPath + "\\" + fileName, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, Settings.Default.appProxyTimeOut + "000"))
                        {
                          case 1:
                            Program.bShowProxyScreen = false;
                            this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
                            continue;
                          case 407:
                            if (Dashbord.bShowProxy)
                            {
                              this.ShowProxyAuthWarning();
                              continue;
                            }
                            this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
                            continue;
                          default:
                            Program.bShowProxyScreen = false;
                            this.Invoke((Delegate) new frmSingleBookDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
                            int num2 = 0;
                            DateTime now = DateTime.Now;
                            TimeSpan timeSpan;
                            do
                            {
                              if (this.frmParent.IsDisposed)
                              {
                                this.objFileDownloader.StopDownloadingFile();
                                if (Thread.CurrentThread.Name == "DownloadThread")
                                  Thread.CurrentThread.Abort();
                              }
                              long bytesDownloaded = (long) this.objFileDownloader.BytesDownloaded;
                              try
                              {
                                num1 = (int) (bytesDownloaded * 100L / (long) this.objFileDownloader.FileLength);
                              }
                              catch (Exception ex)
                              {
                              }
                              if (num2 != num1)
                              {
                                num2 = num1;
                                now = DateTime.Now;
                                this.progrDelegate = new frmSingleBookDownload.UpdateProgressbarsDel(this.UpdateProgress);
                                this.Invoke((Delegate) this.progrDelegate, (object) num1);
                              }
                              Application.DoEvents();
                              timeSpan = DateTime.Now - now;
                              if (bytesDownloaded >= (long) this.objFileDownloader.FileLength)
                                break;
                            }
                            while (timeSpan.Seconds < result);
                            try
                            {
                              if (!fileName.ToUpper().EndsWith("XML"))
                              {
                                if (!fileName.ToUpper().EndsWith("ZIP"))
                                  Global.ChangeDjVUModifiedDate(this.sLocalPath + "\\" + fileName, this.ListGetFileDateFromListNodeTag(index));
                              }
                            }
                            catch
                            {
                            }
                            this.ListEditStatus(index, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
                            ++this.iSuccessCount;
                            this.OverallProgressUpdate(index + 1);
                            if (DataSize.spaceLeft < 10485760L)
                            {
                              this.btnCancel_Click((object) null, (EventArgs) null);
                              goto label_70;
                            }
                            else
                              goto label_70;
                        }
                      }
                      else
                        break;
                    }
                    continue;
                  }
                  catch (Exception ex)
                  {
                    continue;
                  }
                default:
                  this.Invoke((Delegate) new frmSingleBookDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
                  int num3 = 0;
                  DateTime now1 = DateTime.Now;
                  TimeSpan timeSpan1;
                  do
                  {
                    if (this.frmParent.IsDisposed)
                    {
                      this.objFileDownloader.StopDownloadingFile();
                      if (Thread.CurrentThread.Name == "DownloadThread")
                        Thread.CurrentThread.Abort();
                    }
                    long bytesDownloaded = (long) this.objFileDownloader.BytesDownloaded;
                    try
                    {
                      num1 = (int) (bytesDownloaded * 100L / (long) this.objFileDownloader.FileLength);
                    }
                    catch (Exception ex)
                    {
                    }
                    if (num3 != num1)
                    {
                      num3 = num1;
                      now1 = DateTime.Now;
                      this.progrDelegate = new frmSingleBookDownload.UpdateProgressbarsDel(this.UpdateProgress);
                      this.Invoke((Delegate) this.progrDelegate, (object) num1);
                    }
                    Application.DoEvents();
                    timeSpan1 = DateTime.Now - now1;
                    if (bytesDownloaded >= (long) this.objFileDownloader.FileLength)
                      break;
                  }
                  while (timeSpan1.Seconds < result);
                  try
                  {
                    if (!fileName.ToUpper().EndsWith("XML"))
                    {
                      if (!fileName.ToUpper().EndsWith("ZIP"))
                        Global.ChangeDjVUModifiedDate(this.sLocalPath + "\\" + fileName, this.ListGetFileDateFromListNodeTag(index));
                    }
                  }
                  catch
                  {
                  }
                  this.ListEditStatus(index, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
                  ++this.iSuccessCount;
                  this.OverallProgressUpdate(index + 1);
                  if (DataSize.spaceLeft < 10485760L)
                  {
                    this.btnCancel_Click((object) null, (EventArgs) null);
                    continue;
                  }
                  continue;
              }
            }
          }
        }
        this.Invoke((Delegate) new frmSingleBookDownload.UpdateProgressDelegate(this.DownloadingComplete));
      }
      catch
      {
        try
        {
          this.objFileDownloader.StopDownloadingFile();
        }
        catch
        {
        }
        this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
      }
    }

    public void UpdateProgress(int ProgressValue)
    {
      this.lblCurrentProgress.Text = ProgressValue.ToString() + "%";
      this.progressCurrentFile.Value = ProgressValue;
      this.progressCurrentFile.PerformStep();
    }

    public void InitliazeProgressbars()
    {
      this.progressCurrentFile.Minimum = 0;
      this.progressCurrentFile.Maximum = 100;
    }

    private void DownloadingComplete()
    {
      if (this.isWorking)
      {
        this.isWorking = false;
        if (this.listBooks.Items.Count == 0)
        {
          this.UpdateStatus(this.GetResource("Book is already updated.", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
        }
        else
        {
          this.UpdateProgress("");
          this.lblCurrentProgress.Text = "";
        }
      }
      else
        this.UpdateStatus(this.GetResource("Downloading cancelled", "DOWNLOADING_CANCELLED", ResourceType.STATUS_MESSAGE));
      this.isListLoaded = false;
      this.btnDownload.Enabled = true;
      this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
      if (DataSize.spaceLeft >= 10485760L)
        return;
      int num = (int) MessageBox.Show((IWin32Window) this.frmParent, this.GetResource("Book download canceled because maximum download limit was reached", "MAXIMUM_LIMIT_REACHED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.btnCancel_Click((object) null, (EventArgs) null);
      DataSize.miliSecInterval = 20000;
    }

    private void ShowProxyAuthWarning()
    {
      if (this.InvokeRequired)
        this.Invoke((Delegate) new frmSingleBookDownload.ShowProxyAuthWarningDelegate(this.ShowProxyAuthWarning));
      else
        this.ShowProxyAuthenticationError();
    }

    private void ShowProxyAuthenticationError()
    {
      frmProxyAuthentication proxyAuthentication = new frmProxyAuthentication();
      proxyAuthentication.Owner = (Form) this.frmParent;
      if (this.frmParent.InvokeRequired)
      {
        this.frmParent.Invoke((Delegate) new frmSingleBookDownload.ShowProxyAuthenticationDelegate(this.ShowProxyAuthenticationError));
      }
      else
      {
        int num = (int) proxyAuthentication.ShowDialog();
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
      this.pnlForm = new Panel();
      this.pnlFiles = new Panel();
      this.listBooks = new ListView();
      this.colHeadFileName = new ColumnHeader();
      this.colHeadStatus = new ColumnHeader();
      this.picLoading = new PictureBox();
      this.pnlControl = new Panel();
      this.lblCurrentProgress = new Label();
      this.lblOverallProgress = new Label();
      this.lblImageProgress = new Label();
      this.lblDownloaded = new Label();
      this.btnCancel = new Button();
      this.btnDownload = new Button();
      this.progressOverall = new ProgressBar();
      this.progressCurrentFile = new ProgressBar();
      this.bgWorker = new BackgroundWorker();
      this.bgLoader = new BackgroundWorker();
      this.ssStatus = new StatusStrip();
      this.lblStatus = new ToolStripStatusLabel();
      this.pnlForm.SuspendLayout();
      this.pnlFiles.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.pnlControl.SuspendLayout();
      this.ssStatus.SuspendLayout();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlFiles);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(2, 2);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(459, 344);
      this.pnlForm.TabIndex = 0;
      this.pnlFiles.Controls.Add((Control) this.listBooks);
      this.pnlFiles.Controls.Add((Control) this.picLoading);
      this.pnlFiles.Dock = DockStyle.Bottom;
      this.pnlFiles.Location = new Point(0, 88);
      this.pnlFiles.Name = "pnlFiles";
      this.pnlFiles.Padding = new Padding(10);
      this.pnlFiles.Size = new Size(457, 254);
      this.pnlFiles.TabIndex = 0;
      this.listBooks.BackColor = Color.White;
      this.listBooks.Columns.AddRange(new ColumnHeader[2]
      {
        this.colHeadFileName,
        this.colHeadStatus
      });
      this.listBooks.Location = new Point(12, 7);
      this.listBooks.Name = "listBooks";
      this.listBooks.Size = new Size(444, 234);
      this.listBooks.TabIndex = 2;
      this.listBooks.UseCompatibleStateImageBehavior = false;
      this.listBooks.View = View.Details;
      this.colHeadFileName.Text = "File Name";
      this.colHeadFileName.Width = 206;
      this.colHeadStatus.Text = "Status";
      this.colHeadStatus.Width = 102;
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(215, 111);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(34, 34);
      this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
      this.picLoading.TabIndex = 26;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.pnlControl.Controls.Add((Control) this.lblCurrentProgress);
      this.pnlControl.Controls.Add((Control) this.lblOverallProgress);
      this.pnlControl.Controls.Add((Control) this.lblImageProgress);
      this.pnlControl.Controls.Add((Control) this.lblDownloaded);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Controls.Add((Control) this.btnDownload);
      this.pnlControl.Controls.Add((Control) this.progressOverall);
      this.pnlControl.Controls.Add((Control) this.progressCurrentFile);
      this.pnlControl.Dock = DockStyle.Fill;
      this.pnlControl.Location = new Point(0, 0);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(10);
      this.pnlControl.Size = new Size(457, 342);
      this.pnlControl.TabIndex = 1;
      this.lblCurrentProgress.AutoSize = true;
      this.lblCurrentProgress.Location = new Point(321, 8);
      this.lblCurrentProgress.Name = "lblCurrentProgress";
      this.lblCurrentProgress.Size = new Size(0, 13);
      this.lblCurrentProgress.TabIndex = 8;
      this.lblOverallProgress.AutoSize = true;
      this.lblOverallProgress.Location = new Point(14, 45);
      this.lblOverallProgress.Name = "lblOverallProgress";
      this.lblOverallProgress.Size = new Size(0, 13);
      this.lblOverallProgress.TabIndex = 7;
      this.lblImageProgress.AutoSize = true;
      this.lblImageProgress.Location = new Point(14, 8);
      this.lblImageProgress.Name = "lblImageProgress";
      this.lblImageProgress.Size = new Size(0, 13);
      this.lblImageProgress.TabIndex = 6;
      this.lblDownloaded.AutoSize = true;
      this.lblDownloaded.Location = new Point(262, 2);
      this.lblDownloaded.Name = "lblDownloaded";
      this.lblDownloaded.Size = new Size(0, 13);
      this.lblDownloaded.TabIndex = 5;
      this.btnCancel.Location = new Point(376, 59);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnDownload.Location = new Point(376, 22);
      this.btnDownload.Name = "btnDownload";
      this.btnDownload.Size = new Size(75, 23);
      this.btnDownload.TabIndex = 3;
      this.btnDownload.Text = "Download";
      this.btnDownload.UseVisualStyleBackColor = true;
      this.btnDownload.Click += new EventHandler(this.btnDownload_Click);
      this.progressOverall.Location = new Point(12, 61);
      this.progressOverall.Name = "progressOverall";
      this.progressOverall.Size = new Size(344, 19);
      this.progressOverall.TabIndex = 1;
      this.progressCurrentFile.Location = new Point(12, 24);
      this.progressCurrentFile.Name = "progressCurrentFile";
      this.progressCurrentFile.Size = new Size(344, 19);
      this.progressCurrentFile.TabIndex = 0;
      this.bgWorker.WorkerReportsProgress = true;
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.bgWorker.ProgressChanged += new ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
      this.bgLoader.DoWork += new DoWorkEventHandler(this.bgLoader_DoWork);
      this.bgLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgLoader_RunWorkerCompleted);
      this.ssStatus.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.lblStatus
      });
      this.ssStatus.Location = new Point(2, 346);
      this.ssStatus.Name = "ssStatus";
      this.ssStatus.Size = new Size(459, 22);
      this.ssStatus.TabIndex = 27;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(38, 17);
      this.lblStatus.Text = "Ready";
      this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(463, 370);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.ssStatus);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmSingleBookDownload);
      this.Padding = new Padding(2);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Single Book Download";
      this.Load += new EventHandler(this.frmBookDownload_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmBookDownload_FormClosing);
      this.pnlForm.ResumeLayout(false);
      this.pnlFiles.ResumeLayout(false);
      this.pnlFiles.PerformLayout();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.pnlControl.ResumeLayout(false);
      this.pnlControl.PerformLayout();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public delegate void UpdateProgressDelegate();

    public delegate void UpdateProgressbarsDel(int iProcess);

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void ListAddItemDelegate(ListViewItem item);

    private delegate void ListClearItemsDelegate();

    private delegate void ListEditStatusDelegate(int index, string status);

    private delegate string getFileNameDelegate(int index);

    private delegate string getFileDateFromListNodeTagDelegate(int index);

    private delegate void OverallProgressUpdateDelegate(int value);

    private delegate void StatusDelegate(string status);

    private delegate void ProgressStatusDelegate(string progressStatus);

    private delegate void ShowProxyAuthWarningDelegate();

    private delegate void ShowProxyAuthenticationDelegate();
  }
}

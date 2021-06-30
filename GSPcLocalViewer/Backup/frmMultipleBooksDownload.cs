// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmMultipleBooksDownload
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
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmMultipleBooksDownload : Form
  {
    private frmViewer frmParent;
    private string sLocalPath;
    private string sServerPath;
    private bool isWorking;
    private bool isListLoaded;
    private int iSuccessCount;
    private ArrayList BooksList;
    private int bookCounter;
    private FileDownloader objFileDownloader;
    public frmMultipleBooksDownload.UpdateProgressbarsDel progrDelegate;
    private IContainer components;
    private Button btnDownload;
    private BackgroundWorker bgWorker;
    private ProgressBar progressOverall;
    private ProgressBar progressCurrentFile;
    private StatusStrip ssStatus;
    private ToolStripStatusLabel lblStatus;
    private BackgroundWorker bgLoader;
    private Button btnCancel;
    private Label lblDownloaded;
    private Panel pnlControl;
    private Panel pnlForm;
    private ListView bookListView;
    private Label lblCurrentProgress;
    private Label lblOverallProgress;
    private Label lblCurrentPictureDownload;
    private SplitContainer splitContainer1;
    private ListView listBooks;
    private ColumnHeader colHeadFileName;
    private ColumnHeader colHeadStatus;
    private PictureBox picLoading;
    private CheckBox chkboxSelectAll;
    private ColumnHeader columnHeader1;

    public frmMultipleBooksDownload(frmViewer frm, string _sLocalPath, string _sServerPath)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.sLocalPath = _sLocalPath;
      if (this.sLocalPath.EndsWith("\\"))
        this.sLocalPath = this.sLocalPath.Remove(this.sLocalPath.LastIndexOf("\\"));
      this.sServerPath = _sServerPath;
      if (this.sServerPath.EndsWith("/"))
        this.sServerPath = this.sServerPath.Remove(this.sServerPath.LastIndexOf("/"));
      this.progressOverall.Maximum = 100;
      this.progressOverall.Minimum = 0;
      this.progressOverall.Value = 0;
      this.progressCurrentFile.Value = 0;
      this.bookCounter = 0;
      this.UpdateFont();
      this.UpdateStatus(string.Empty);
      this.isWorking = false;
      this.isListLoaded = false;
      this.LoadResources();
    }

    private void frmAllBooksDownload_Load(object sender, EventArgs e)
    {
      this.listBooks.Columns[0].Width = 200;
      this.listBooks.Columns[1].Width = 105;
      this.btnDownload.Enabled = true;
      this.btnCancel.Enabled = true;
      if (this.ReadSeriesFile())
        return;
      this.UpdateStatus(this.GetResource("Books not Loaded", "BOOKS_NOT_LOADED", ResourceType.STATUS_MESSAGE));
      this.btnDownload.Enabled = false;
      this.btnCancel.Enabled = false;
    }

    private void frmAllBooksDownload_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      this.frmParent.HideDimmer();
    }

    private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.ListClearItems();
      string empty1 = string.Empty;
      string str1 = !this.frmParent.CompressedDownload ? ".xml" : ".zip";
      for (int index1 = 0; index1 < this.bookListView.Items.Count; ++index1)
      {
        string[] strArray1 = this.BooksList[index1].ToString().Split('|');
        if (this.BookListGetBookStatus(index1) && strArray1.Length == 3)
        {
          string sBookPublishingID = strArray1[0].ToString();
          string str2 = this.sLocalPath + "\\" + sBookPublishingID + "\\";
          if (!Directory.Exists(str2))
            Directory.CreateDirectory(str2);
          if (!File.Exists(str2 + sBookPublishingID + str1))
          {
            this.DownloadFile(this.sServerPath + "/" + sBookPublishingID + "/" + sBookPublishingID + str1, str2 + sBookPublishingID + str1);
          }
          else
          {
            string empty2 = string.Empty;
            string s = strArray1[2];
            DateTime dtServer;
            try
            {
              dtServer = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) null);
            }
            catch
            {
              try
              {
                dtServer = DateTime.ParseExact(s, "dd/MM/yyyy", (IFormatProvider) null);
              }
              catch
              {
                dtServer = new DateTime();
              }
            }
            int result = 0;
            if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
              result = 0;
            if (result == 0)
              this.DownloadFile(this.sServerPath + "/" + sBookPublishingID + "/" + sBookPublishingID + str1, str2 + sBookPublishingID + str1);
            else if (result < 1000)
            {
              if (Global.IntervalElapsed(Global.GetLocalDateOfFile(str2 + sBookPublishingID + str1, this.frmParent.p_ServerId), dtServer, result))
                this.DownloadFile(this.sServerPath + "/" + sBookPublishingID + "/" + sBookPublishingID + str1, str2 + sBookPublishingID + str1);
            }
          }
          try
          {
            if (this.frmParent.CompressedDownload)
            {
              if (File.Exists(str2 + sBookPublishingID + str1))
                Global.Unzip(str2 + sBookPublishingID + str1);
            }
          }
          catch
          {
          }
          ArrayList arrList = new ArrayList();
          if (File.Exists(str2 + sBookPublishingID + ".xml"))
            arrList = this.GetAllPagesToDownload(str2 + sBookPublishingID + ".xml");
          this.frmParent.BookDowloadAddSearchXmlFile(ref arrList, str2, sBookPublishingID, strArray1[1]);
          if (arrList.Count > 0)
          {
            this.ListAddItem(new ListViewItem(new string[2]
            {
              "[" + sBookPublishingID + "]",
              string.Empty
            }));
            ++this.bookCounter;
          }
          for (int index2 = 0; index2 < arrList.Count; ++index2)
          {
            string[] strArray2 = arrList[index2].ToString().Split(new string[1]
            {
              "|*|*|"
            }, StringSplitOptions.None);
            ListViewItem listViewItem = new ListViewItem(new string[2]
            {
              strArray2[0],
              string.Empty
            });
            if (strArray2.Length == 2)
              listViewItem.Tag = (object) strArray2[1];
            this.ListAddItem(listViewItem);
          }
        }
      }
    }

    private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.progressCurrentFile.Minimum = 0;
      this.progressCurrentFile.Maximum = 100;
      this.progressCurrentFile.Value = 0;
      this.progressOverall.Minimum = 0;
      this.progressOverall.Maximum = this.listBooks.Items.Count - this.bookCounter <= 0 ? 0 : this.listBooks.Items.Count - this.bookCounter;
      this.progressOverall.Value = 0;
      this.HideLoading((Panel) this.splitContainer1.Panel2);
      this.btnDownload.Enabled = true;
      this.btnCancel.Enabled = true;
      this.btnCancel.Text = "Cancel";
      bool flag = false;
      for (int index = 0; index < this.bookListView.Items.Count; ++index)
      {
        if (this.BookListGetBookStatus(index))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        this.UpdateStatus(this.GetResource("(W-MBD-WM001) Please select at least 1 book to download", "(W-MBD-WM001)_SELECT", ResourceType.STATUS_MESSAGE));
      else if (this.listBooks.Items.Count == 0)
        this.UpdateStatus(this.GetResource("Books are already updated", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
      else
        this.UpdateStatus(this.GetResource("Ready……", "READY", ResourceType.STATUS_MESSAGE));
      if (this.isListLoaded)
        return;
      this.isListLoaded = true;
      this.progressOverall.Value = 0;
      this.progressCurrentFile.Value = 0;
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

    private void btnDownload_Click(object sender, EventArgs e)
    {
      if (this.isListLoaded)
        return;
      this.bookCounter = 0;
      this.btnDownload.Enabled = false;
      this.btnCancel.Enabled = false;
      this.chkboxSelectAll.Enabled = false;
      this.bookListView.Enabled = false;
      this.UpdateStatus(this.GetResource("Checking Books to download……", "CHECKING_TO_DOWNLOAD", ResourceType.STATUS_MESSAGE));
      this.ShowLoading((Panel) this.splitContainer1.Panel2);
      this.bgLoader.RunWorkerAsync();
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

    private void bookListView_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      try
      {
        if (!e.Item.Checked)
        {
          this.chkboxSelectAll.Checked = false;
        }
        else
        {
          if (!e.Item.Checked)
            return;
          bool flag = true;
          for (int index = 0; index < this.bookListView.Items.Count; ++index)
          {
            if (!this.bookListView.Items[index].Checked)
            {
              flag = false;
              break;
            }
          }
          if (!flag)
            return;
          this.chkboxSelectAll.Checked = true;
        }
      }
      catch
      {
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      string empty = string.Empty;
      this.iSuccessCount = 0;
      string str = string.Empty;
      int num = 0;
      DataSize.miliSecInterval = 2000;
      for (int index = 0; index < this.listBooks.Items.Count && this.isWorking; ++index)
      {
        if (!this.IsDisposed)
        {
          string fileName = this.ListGetFileName(index);
          if (fileName.Contains("["))
          {
            str = fileName.Substring(fileName.IndexOf("[") + 1, fileName.Length - 2);
          }
          else
          {
            this.UpdateProgress(this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL) + " " + str + " >> " + fileName);
            if (this.DownloadFile(this.sServerPath + "/" + str + "/" + fileName, this.sLocalPath + "\\" + str + "\\" + fileName))
            {
              this.ListEditStatus(index, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
              ++this.iSuccessCount;
            }
            else
              this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
            ++num;
          }
          this.OverallProgressUpdate(num);
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
        bool flag = false;
        for (int index = 0; index < this.bookListView.Items.Count; ++index)
        {
          if (this.BookListGetBookStatus(index))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.UpdateStatus(this.GetResource("(W-MBD-WM001) Please select at least 1 book to download", "(W-MBD-WM001)_SELECT", ResourceType.STATUS_MESSAGE));
        else if (this.listBooks.Items.Count == 0)
        {
          this.UpdateStatus(this.GetResource("Books are already updated", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
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
      this.HideLoading((Panel) this.splitContainer1.Panel2);
      this.btnDownload.Enabled = true;
      this.bookListView.Enabled = true;
      this.chkboxSelectAll.Enabled = true;
      this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
      if (DataSize.spaceLeft >= 10485760L)
        return;
      int num = (int) MessageBox.Show((IWin32Window) this.frmParent, this.GetResource("Book download canceled because maximum download limit was reached", "MAXIMUM_LIMIT_REACHED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.btnCancel_Click((object) null, (EventArgs) null);
      DataSize.miliSecInterval = 20000;
    }

    private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.UpdateProgressLabel(e.ProgressPercentage);
      this.UpdateFileProgress(e.ProgressPercentage);
    }

    private void bookListView_MouseMove(object sender, MouseEventArgs e)
    {
    }

    private void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkboxSelectAll.Tag.ToString() == "BYMOUSE")
      {
        this.chkboxSelectAll.Tag = (object) string.Empty;
        if (this.chkboxSelectAll.Checked)
        {
          for (int index = 0; index < this.bookListView.Items.Count; ++index)
            this.bookListView.Items[index].Checked = true;
        }
        else if (!this.chkboxSelectAll.Checked)
        {
          for (int index = 0; index < this.bookListView.Items.Count; ++index)
            this.bookListView.Items[index].Checked = false;
        }
      }
      this.chkboxSelectAll.Tag = (object) string.Empty;
    }

    private void chkboxSelectAll_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        this.chkboxSelectAll.Tag = (object) "BYMOUSE";
      }
      catch
      {
      }
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }

    private void UpdateStatus(string status)
    {
      if (this.ssStatus.InvokeRequired)
        this.ssStatus.Invoke((Delegate) new frmMultipleBooksDownload.StatusDelegate(this.UpdateStatus), (object) status);
      else
        this.lblStatus.Text = status;
    }

    private void UpdateProgress(string progressStatus)
    {
      if (this.lblCurrentPictureDownload.InvokeRequired)
        this.lblCurrentPictureDownload.Invoke((Delegate) new frmMultipleBooksDownload.StatusDelegate(this.UpdateProgress), (object) progressStatus);
      else
        this.lblCurrentPictureDownload.Text = progressStatus;
    }

    private bool ReadSeriesFile()
    {
      this.BooksList = new ArrayList();
      string empty1 = string.Empty;
      string str1 = !this.frmParent.CompressedDownload ? ".xml" : ".zip";
      try
      {
        if (this.frmParent.CompressedDownload)
        {
          if (File.Exists(this.sLocalPath + "\\Series" + str1))
            Global.Unzip(this.sLocalPath + "\\Series" + str1);
        }
      }
      catch
      {
      }
      string str2 = this.sLocalPath + "\\Series.xml";
      XmlDocument xmlDocument = new XmlDocument();
      if (File.Exists(str2))
      {
        try
        {
          xmlDocument.Load(str2);
        }
        catch
        {
          return false;
        }
        if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null)
        {
          if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          {
            try
            {
              string str3 = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
              xmlDocument.DocumentElement.InnerXml = str3;
            }
            catch
            {
              return false;
            }
          }
        }
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//Schema");
        if (xmlNode == null)
          return false;
        string index1 = string.Empty;
        string index2 = string.Empty;
        string index3 = string.Empty;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode.Attributes)
        {
          try
          {
            if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
              index1 = attribute.Name;
            else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
              index2 = attribute.Name;
            else if (attribute.Value.ToUpper().Equals("BOOKTYPE"))
              index3 = attribute.Name;
          }
          catch
          {
          }
        }
        if (index1 == string.Empty || index2 == string.Empty)
          return false;
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Book");
        if (xmlNodeList.Count <= 0)
          return false;
        for (int index4 = 0; index4 < xmlNodeList.Count; ++index4)
        {
          try
          {
            string empty2 = string.Empty;
            string str3;
            if (xmlNodeList[index4].Attributes[index1] != null)
            {
              str3 = empty2 + xmlNodeList[index4].Attributes[index1].Value.ToString() + "|";
              this.bookListView.Items.Add(str3.ToString().Substring(0, str3.Length - 1));
            }
            else
              str3 = empty2 + "|";
            string str4 = xmlNodeList[index4].Attributes[index3] == null ? str3 + "|" : str3 + xmlNodeList[index4].Attributes[index3].Value.ToString() + "|";
            if (xmlNodeList[index4].Attributes[index2] != null)
              str4 += xmlNodeList[index4].Attributes[index2].Value.ToString();
            this.BooksList.Add((object) str4.ToString());
          }
          catch
          {
          }
        }
      }
      return true;
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

    private void ListClearItems()
    {
      if (this.listBooks.InvokeRequired)
        this.listBooks.Invoke((Delegate) new frmMultipleBooksDownload.ListClearItemsDelegate(this.ListClearItems));
      else
        this.listBooks.Items.Clear();
    }

    private void BooksListClearItems()
    {
      if (this.bookListView.InvokeRequired)
        this.bookListView.Invoke((Delegate) new frmMultipleBooksDownload.ListClearItemsDelegate(this.BooksListClearItems));
      else
        this.bookListView.Items.Clear();
    }

    private void ListAddItem(ListViewItem item)
    {
      if (this.listBooks.InvokeRequired)
      {
        this.listBooks.Invoke((Delegate) new frmMultipleBooksDownload.ListAddItemDelegate(this.ListAddItem), (object) item);
      }
      else
      {
        if (item.Text.Contains("["))
          item.Font = new Font(this.listBooks.Font.FontFamily, this.listBooks.Font.Size, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
        this.listBooks.Items.Add(item);
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

    private void ListEditStatus(int index, string status)
    {
      if (this.listBooks.InvokeRequired)
      {
        this.listBooks.Invoke((Delegate) new frmMultipleBooksDownload.ListEditStatusDelegate(this.ListEditStatus), (object) index, (object) status);
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
        return (string) this.listBooks.Invoke((Delegate) new frmMultipleBooksDownload.getFileNameDelegate(this.ListGetFileName), (object) index);
      if (this.listBooks.Columns.Count > 0)
        return this.listBooks.Items[index].SubItems[0].Text;
      return string.Empty;
    }

    private string ListGetFileDateFromListNodeTag(int index)
    {
      if (this.listBooks.InvokeRequired)
        return (string) this.listBooks.Invoke((Delegate) new frmMultipleBooksDownload.getFileDateFromListNodeTagDelegate(this.ListGetFileDateFromListNodeTag), (object) index);
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

    private bool BookListGetBookStatus(int index)
    {
      if (!this.bookListView.InvokeRequired)
        return this.bookListView.Items[index].Checked;
      return (bool) this.bookListView.Invoke((Delegate) new frmMultipleBooksDownload.getBookCheckedStatusDelegate(this.BookListGetBookStatus), (object) index);
    }

    private void OverallProgressUpdate(int value)
    {
      if (this.progressOverall.InvokeRequired)
      {
        this.progressOverall.Invoke((Delegate) new frmMultipleBooksDownload.OverallProgressUpdateDelegate(this.OverallProgressUpdate), (object) value);
      }
      else
      {
        if (value > this.progressOverall.Maximum || value < this.progressOverall.Minimum)
          return;
        this.lblOverallProgress.Text = this.GetResource("Overall Progress:", "OVERALL_PROGRESS", ResourceType.LABEL) + " " + value.ToString() + " / " + (this.listBooks.Items.Count - this.bookCounter).ToString();
        this.progressOverall.Value = value;
      }
    }

    private bool DownloadFile(string surl1, string sLocalPath)
    {
      Application.DoEvents();
      FileDownloader fileDownloader = new FileDownloader();
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
          DateTime now = DateTime.Now;
          int num = 0;
          long bytesDownloaded;
          int percentProgress;
          TimeSpan timeSpan;
          do
          {
            bytesDownloaded = (long) fileDownloader.BytesDownloaded;
            percentProgress = bytesDownloaded != 0L ? (int) (bytesDownloaded * 100L / fileLength) : 0;
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
          return bytesDownloaded == fileLength;
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
                    DateTime now = DateTime.Now;
                    int num = 0;
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
            DateTime now1 = DateTime.Now;
            int num1 = 0;
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

    private void UpdateProgressLabel(int prog)
    {
      if (this.lblCurrentProgress.InvokeRequired)
        this.progressCurrentFile.Invoke((Delegate) new frmMultipleBooksDownload.UpdateProgressLabelDelegate(this.UpdateProgressLabel), (object) prog);
      else
        this.lblCurrentProgress.Text = prog.ToString() + "%";
    }

    private void UpdateFileProgress(int prog)
    {
      if (this.progressCurrentFile.InvokeRequired)
      {
        this.progressCurrentFile.Invoke((Delegate) new frmMultipleBooksDownload.UpdateFileProgressDelegate(this.UpdateFileProgress), (object) prog);
      }
      else
      {
        if (prog < this.progressCurrentFile.Minimum || prog > this.progressCurrentFile.Maximum)
          return;
        this.progressCurrentFile.Value = prog;
      }
    }

    public ArrayList GetAllPagesToDownload(string sLocalPath)
    {
      ArrayList arrayList = new ArrayList();
      XmlDocument xmlDocument = new XmlDocument();
      DateTime dtServer = new DateTime();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      try
      {
        xmlDocument.Load(sLocalPath);
        if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        XmlElement documentElement = xmlDocument.DocumentElement;
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("//Schema");
        if (xmlNode1 != null)
        {
          string index1 = string.Empty;
          string index2 = string.Empty;
          string str1 = string.Empty;
          string index3 = string.Empty;
          string index4 = string.Empty;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode1.Attributes)
          {
            if (attribute.Value.ToUpper() == "PICTUREFILE")
              index1 = attribute.Name;
            if (attribute.Value.ToUpper() == "PARTSLISTFILE")
              index2 = attribute.Name;
            if (attribute.Value.ToUpper() == "UPDATEDATE")
              str1 = attribute.Name;
            if (attribute.Value.ToUpper() == "UPDATEDATEPIC")
              index3 = attribute.Name;
            if (attribute.Value.ToUpper() == "UPDATEDATEPL")
              index4 = attribute.Name;
          }
          if (index3 == string.Empty)
            index3 = str1;
          if (index4 == string.Empty)
            index4 = str1;
          XmlNodeList xmlNodeList1 = xmlDocument.SelectNodes("//Pg");
          for (int index5 = 0; index5 < xmlNodeList1.Count; ++index5)
          {
            XmlNodeList xmlNodeList2 = xmlNodeList1[index5].SelectNodes("//Pic");
            int result = 0;
            if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
              result = 0;
            string str2 = sLocalPath.Substring(0, sLocalPath.LastIndexOf("\\"));
            foreach (XmlNode xmlNode2 in xmlNodeList2)
            {
              if (xmlNode2.Attributes[index1] != null && xmlNode2.Attributes[index3] != null)
              {
                string str3 = xmlNode2.Attributes[index1].Value;
                if (!str3.ToUpper().EndsWith(".DJVU") && !str3.ToUpper().EndsWith(".PDF") && !str3.ToUpper().EndsWith(".TIF"))
                  str3 += ".djvu";
                empty2 = xmlNode2.Attributes[index3].Value;
                try
                {
                  dtServer = Convert.ToDateTime(empty2, (IFormatProvider) new CultureInfo("en-GB"));
                }
                catch
                {
                }
                bool flag = false;
                if (File.Exists(str2 + "\\" + str3))
                {
                  if (result == 0)
                    flag = true;
                  else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str2 + "/" + str3, this.frmParent.ServerId), dtServer, result))
                    flag = true;
                }
                else
                  flag = true;
                if (!arrayList.Contains((object) (str3 + "|*|*|" + empty2)) && flag)
                  arrayList.Add((object) (str3 + "|*|*|" + empty2));
              }
              else if (xmlNode2.Attributes[index1] != null)
              {
                string str3 = xmlNode2.Attributes[index1].Value;
                if (!str3.ToUpper().EndsWith(".DJVU") && !str3.ToUpper().EndsWith(".PDF") && !str3.ToUpper().EndsWith(".TIF"))
                  str3 += ".djvu";
                if (!arrayList.Contains((object) (str3 + "|*|*|" + empty2)))
                  arrayList.Add((object) (str3 + "|*|*|" + empty2));
              }
              if (xmlNode2.Attributes[index2] != null && xmlNode2.Attributes[index4] != null)
              {
                string str3 = xmlNode2.Attributes[index2].Value;
                if (this.frmParent.CompressedDownload)
                {
                  if (!str3.ToUpper().EndsWith(".XML") && !str3.ToUpper().EndsWith(".ZIP"))
                    str3 += ".zip";
                  else if (str3.ToUpper().EndsWith(".XML"))
                    str3 = str3.Replace(".xml", ".zip");
                }
                else if (!str3.ToUpper().EndsWith(".XML") && !str3.ToUpper().EndsWith(".PDF") && !str3.ToUpper().EndsWith(".TIF"))
                  str3 += ".xml";
                empty2 = xmlNode2.Attributes[index4].Value;
                try
                {
                  dtServer = Convert.ToDateTime(empty2, (IFormatProvider) new CultureInfo("en-GB"));
                }
                catch
                {
                }
                bool flag = false;
                if (File.Exists(str2 + "/" + str3))
                {
                  if (result == 0)
                    flag = true;
                  else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str2 + "/" + str3, this.frmParent.ServerId), dtServer, result))
                    flag = true;
                }
                else
                  flag = true;
                if (!arrayList.Contains((object) (str3 + "|*|*|" + empty2)) && flag)
                  arrayList.Add((object) (str3 + "|*|*|" + empty2));
              }
              else if (xmlNode2.Attributes[index2] != null)
              {
                string str3 = xmlNode2.Attributes[index2].Value;
                if (this.frmParent.CompressedDownload)
                {
                  if (!str3.ToUpper().EndsWith(".XML") && !str3.ToUpper().EndsWith(".ZIP"))
                    str3 += ".zip";
                  else if (str3.ToUpper().EndsWith(".XML"))
                    str3 = str3.Replace(".xml", ".zip");
                }
                if (!str3.ToUpper().EndsWith(".XML") && !str3.ToUpper().EndsWith(".PDF") && !str3.ToUpper().EndsWith(".TIF"))
                  str3 += ".xml";
                if (!arrayList.Contains((object) (str3 + "|*|*|" + empty2)))
                  arrayList.Add((object) (str3 + "|*|*|" + empty2));
              }
            }
          }
        }
      }
      catch (XmlException ex)
      {
        try
        {
          File.Delete(sLocalPath);
        }
        catch
        {
        }
      }
      catch
      {
      }
      return arrayList;
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Multiple Books Download", "MULTIPLE_BOOKS_DOWNLOAD", ResourceType.TITLE);
      this.btnDownload.Text = this.GetResource("Download", "DOWNLOAD", ResourceType.BUTTON);
      this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
      this.chkboxSelectAll.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.LIST_VIEW);
      this.listBooks.Columns[0].Text = this.GetResource("File Name", "FILE_NAME", ResourceType.LIST_VIEW);
      this.listBooks.Columns[1].Text = this.GetResource("Status", "STATUS", ResourceType.LIST_VIEW);
      this.lblStatus.Text = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MULTIPLE_BOOKS_DOWNLOAD']";
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

    private void bookListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
    {
      e.Cancel = true;
      e.NewWidth = this.bookListView.Columns[0].Width;
    }

    private void bookListView_Resize(object sender, EventArgs e)
    {
      this.bookListView.Columns[0].Width = this.bookListView.Width - 10;
    }

    private void listBooks_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
    {
      if (e.ColumnIndex != 1)
        return;
      e.Cancel = true;
      e.NewWidth = this.listBooks.Columns[1].Width;
    }

    private void DownloadFilesList()
    {
      int index = 0;
      try
      {
        int result = 0;
        int.TryParse(Settings.Default.appProxyTimeOut, out result);
        DataSize.miliSecInterval = 2000;
        string empty = string.Empty;
        string str = string.Empty;
        this.iSuccessCount = 0;
        this.objFileDownloader = new FileDownloader();
        int num1 = 0;
        this.Invoke((Delegate) new frmMultipleBooksDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
        for (index = 0; index < this.listBooks.Items.Count; ++index)
        {
          int num2 = 0;
          if (this.isWorking)
          {
            if (!this.IsDisposed)
            {
              this.Invoke((Delegate) new frmMultipleBooksDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
              string fileName = this.ListGetFileName(index);
              if (fileName.Contains("["))
              {
                str = fileName.Substring(fileName.IndexOf("[") + 1, fileName.Length - 2);
              }
              else
              {
                this.UpdateProgress(this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL) + " " + str + " >> " + fileName);
                if (this.objFileDownloader.DownloadingFile(this.sServerPath + "/" + str + "/" + fileName, this.sLocalPath + "\\" + str + "\\" + fileName, Settings.Default.appProxyTimeOut + "000") == 1)
                {
                  this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
                }
                else
                {
                  int num3 = 0;
                  DateTime now = DateTime.Now;
                  long num4;
                  TimeSpan timeSpan;
                  do
                  {
                    if (this.frmParent.IsDisposed)
                    {
                      this.objFileDownloader.StopDownloadingFile();
                      if (Thread.CurrentThread.Name == "DownloadThread")
                        Thread.CurrentThread.Abort();
                    }
                    num4 = (long) this.objFileDownloader.BytesDownloaded;
                    int num5;
                    if (num4 == 0L)
                    {
                      num5 = 0;
                      num4 = -1L;
                    }
                    else
                      num5 = (int) (num4 * 100L / (long) this.objFileDownloader.FileLength);
                    if (num3 != num5)
                    {
                      now = DateTime.Now;
                      num3 = num5;
                      this.progrDelegate = new frmMultipleBooksDownload.UpdateProgressbarsDel(this.UpdateProgress);
                      this.Invoke((Delegate) this.progrDelegate, (object) num5);
                    }
                    timeSpan = DateTime.Now - now;
                    if (timeSpan.Seconds >= result && num2 < 3)
                    {
                      ++num2;
                      Application.DoEvents();
                      this.objFileDownloader.DownloadingFile(this.sServerPath + "/" + str + "/" + fileName, this.sLocalPath + "\\" + str + "\\" + fileName, Settings.Default.appProxyTimeOut + "000");
                      now = DateTime.Now;
                      timeSpan = DateTime.Now - now;
                      num4 = (long) this.objFileDownloader.BytesDownloaded;
                      int num6;
                      if (num4 != 0L)
                      {
                        num6 = (int) (num4 * 100L / (long) this.objFileDownloader.FileLength);
                      }
                      else
                      {
                        num4 = -1L;
                        num6 = 0;
                      }
                      num3 = num6;
                      this.progrDelegate = new frmMultipleBooksDownload.UpdateProgressbarsDel(this.UpdateProgress);
                      this.Invoke((Delegate) this.progrDelegate, (object) num6);
                    }
                  }
                  while (num4 < (long) this.objFileDownloader.FileLength && timeSpan.Seconds < result);
                  if (num4 == (long) this.objFileDownloader.FileLength)
                  {
                    try
                    {
                      if (!fileName.ToUpper().EndsWith("XML"))
                      {
                        if (!fileName.ToUpper().EndsWith("ZIP"))
                          Global.ChangeDjVUModifiedDate(this.sLocalPath + "\\" + str + "\\" + fileName, this.ListGetFileDateFromListNodeTag(index));
                      }
                    }
                    catch
                    {
                    }
                    this.ListEditStatus(index, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
                    ++this.iSuccessCount;
                  }
                  else
                  {
                    try
                    {
                      this.ListEditStatus(index, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
                      if (File.Exists(this.sLocalPath + "/" + str + "/" + fileName))
                        File.Delete(this.sLocalPath + "\\" + str + "\\" + fileName);
                    }
                    catch
                    {
                    }
                  }
                  if (DataSize.spaceLeft < 10485760L)
                    this.btnCancel_Click((object) null, (EventArgs) null);
                }
                ++num1;
                this.OverallProgressUpdate(num1);
              }
            }
          }
          else
            break;
        }
        this.Invoke((Delegate) new frmMultipleBooksDownload.UpdateProgressDelegate(this.DownloadingComplete));
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
        this.ListEditStatus(index, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
      }
    }

    public void UpdateProgress(int ProgressValue)
    {
      try
      {
        if (ProgressValue > this.progressCurrentFile.Maximum || ProgressValue < this.progressCurrentFile.Minimum)
          return;
        this.lblCurrentProgress.Text = ProgressValue.ToString() + "%";
        this.progressCurrentFile.Value = ProgressValue;
      }
      catch
      {
      }
    }

    public void InitliazeProgressbars()
    {
      this.progressCurrentFile.Minimum = 0;
      this.progressCurrentFile.Maximum = 100;
      this.progressCurrentFile.Value = 0;
    }

    private void DownloadingComplete()
    {
      if (this.isWorking)
      {
        this.isWorking = false;
        bool flag = false;
        for (int index = 0; index < this.bookListView.Items.Count; ++index)
        {
          if (this.BookListGetBookStatus(index))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.UpdateStatus(this.GetResource("(W-MBD-WM001) Please select at least 1 book to download", "(W-MBD-WM001)_SELECT", ResourceType.STATUS_MESSAGE));
        else if (this.listBooks.Items.Count == 0)
        {
          this.UpdateStatus(this.GetResource("Books are already updated", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
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
      this.bookListView.Enabled = true;
      this.chkboxSelectAll.Enabled = true;
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
        this.Invoke((Delegate) new frmMultipleBooksDownload.ShowProxyAuthWarningDelegate(this.ShowProxyAuthWarning));
      else
        this.ShowProxyAuthenticationError();
    }

    private void ShowProxyAuthenticationError()
    {
      frmProxyAuthentication proxyAuthentication = new frmProxyAuthentication();
      proxyAuthentication.Owner = (Form) this.frmParent;
      if (this.frmParent.InvokeRequired)
      {
        this.frmParent.Invoke((Delegate) new frmMultipleBooksDownload.ShowProxyAuthenticationDelegate(this.ShowProxyAuthenticationError));
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
      this.pnlControl = new Panel();
      this.splitContainer1 = new SplitContainer();
      this.chkboxSelectAll = new CheckBox();
      this.bookListView = new ListView();
      this.columnHeader1 = new ColumnHeader();
      this.picLoading = new PictureBox();
      this.listBooks = new ListView();
      this.colHeadFileName = new ColumnHeader();
      this.colHeadStatus = new ColumnHeader();
      this.lblCurrentProgress = new Label();
      this.lblOverallProgress = new Label();
      this.lblCurrentPictureDownload = new Label();
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
      this.pnlControl.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.ssStatus.SuspendLayout();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(2, 2);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(500, 344);
      this.pnlForm.TabIndex = 0;
      this.pnlControl.Controls.Add((Control) this.splitContainer1);
      this.pnlControl.Controls.Add((Control) this.lblCurrentProgress);
      this.pnlControl.Controls.Add((Control) this.lblOverallProgress);
      this.pnlControl.Controls.Add((Control) this.lblCurrentPictureDownload);
      this.pnlControl.Controls.Add((Control) this.lblDownloaded);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Controls.Add((Control) this.btnDownload);
      this.pnlControl.Controls.Add((Control) this.progressOverall);
      this.pnlControl.Controls.Add((Control) this.progressCurrentFile);
      this.pnlControl.Dock = DockStyle.Fill;
      this.pnlControl.Location = new Point(0, 0);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(10);
      this.pnlControl.Size = new Size(498, 342);
      this.pnlControl.TabIndex = 1;
      this.splitContainer1.Location = new Point(13, 93);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Panel1.Controls.Add((Control) this.chkboxSelectAll);
      this.splitContainer1.Panel1.Controls.Add((Control) this.bookListView);
      this.splitContainer1.Panel2.Controls.Add((Control) this.picLoading);
      this.splitContainer1.Panel2.Controls.Add((Control) this.listBooks);
      this.splitContainer1.Size = new Size(471, 233);
      this.splitContainer1.SplitterDistance = 157;
      this.splitContainer1.TabIndex = 35;
      this.chkboxSelectAll.AutoSize = true;
      this.chkboxSelectAll.BackColor = SystemColors.Control;
      this.chkboxSelectAll.Location = new Point(8, 3);
      this.chkboxSelectAll.Name = "chkboxSelectAll";
      this.chkboxSelectAll.Size = new Size(70, 17);
      this.chkboxSelectAll.TabIndex = 36;
      this.chkboxSelectAll.Text = "Select All";
      this.chkboxSelectAll.UseVisualStyleBackColor = false;
      this.chkboxSelectAll.MouseDown += new MouseEventHandler(this.chkboxSelectAll_MouseDown);
      this.chkboxSelectAll.CheckedChanged += new EventHandler(this.chkboxSelectAll_CheckedChanged);
      this.bookListView.BackColor = Color.White;
      this.bookListView.CheckBoxes = true;
      this.bookListView.Columns.AddRange(new ColumnHeader[1]
      {
        this.columnHeader1
      });
      this.bookListView.Dock = DockStyle.Fill;
      this.bookListView.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.bookListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      this.bookListView.Location = new Point(0, 0);
      this.bookListView.Name = "bookListView";
      this.bookListView.ShowGroups = false;
      this.bookListView.Size = new Size(157, 233);
      this.bookListView.TabIndex = 30;
      this.bookListView.UseCompatibleStateImageBehavior = false;
      this.bookListView.View = View.Details;
      this.bookListView.Resize += new EventHandler(this.bookListView_Resize);
      this.bookListView.ItemChecked += new ItemCheckedEventHandler(this.bookListView_ItemChecked);
      this.bookListView.MouseMove += new MouseEventHandler(this.bookListView_MouseMove);
      this.bookListView.ColumnWidthChanging += new ColumnWidthChangingEventHandler(this.bookListView_ColumnWidthChanging);
      this.columnHeader1.Text = "";
      this.columnHeader1.Width = 153;
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(143, 103);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(34, 34);
      this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
      this.picLoading.TabIndex = 26;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.listBooks.BackColor = Color.White;
      this.listBooks.Columns.AddRange(new ColumnHeader[2]
      {
        this.colHeadFileName,
        this.colHeadStatus
      });
      this.listBooks.Dock = DockStyle.Fill;
      this.listBooks.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.listBooks.HeaderStyle = ColumnHeaderStyle.Nonclickable;
      this.listBooks.Location = new Point(0, 0);
      this.listBooks.Name = "listBooks";
      this.listBooks.Size = new Size(310, 233);
      this.listBooks.TabIndex = 27;
      this.listBooks.UseCompatibleStateImageBehavior = false;
      this.listBooks.View = View.Details;
      this.listBooks.ColumnWidthChanging += new ColumnWidthChangingEventHandler(this.listBooks_ColumnWidthChanging);
      this.colHeadFileName.Text = "File Name";
      this.colHeadFileName.Width = 204;
      this.colHeadStatus.Text = "Status";
      this.colHeadStatus.Width = 102;
      this.lblCurrentProgress.AutoSize = true;
      this.lblCurrentProgress.Location = new Point(348, 5);
      this.lblCurrentProgress.Name = "lblCurrentProgress";
      this.lblCurrentProgress.Size = new Size(0, 13);
      this.lblCurrentProgress.TabIndex = 34;
      this.lblOverallProgress.AutoSize = true;
      this.lblOverallProgress.Location = new Point(13, 41);
      this.lblOverallProgress.Name = "lblOverallProgress";
      this.lblOverallProgress.Size = new Size(0, 13);
      this.lblOverallProgress.TabIndex = 33;
      this.lblCurrentPictureDownload.AutoSize = true;
      this.lblCurrentPictureDownload.Location = new Point(13, 3);
      this.lblCurrentPictureDownload.Name = "lblCurrentPictureDownload";
      this.lblCurrentPictureDownload.Size = new Size(0, 13);
      this.lblCurrentPictureDownload.TabIndex = 32;
      this.lblDownloaded.AutoSize = true;
      this.lblDownloaded.Location = new Point(262, 2);
      this.lblDownloaded.Name = "lblDownloaded";
      this.lblDownloaded.Size = new Size(0, 13);
      this.lblDownloaded.TabIndex = 5;
      this.btnCancel.Location = new Point(409, 55);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnDownload.Location = new Point(409, 15);
      this.btnDownload.Name = "btnDownload";
      this.btnDownload.Size = new Size(75, 23);
      this.btnDownload.TabIndex = 3;
      this.btnDownload.Text = "Download";
      this.btnDownload.UseVisualStyleBackColor = true;
      this.btnDownload.Click += new EventHandler(this.btnDownload_Click);
      this.progressOverall.Location = new Point(12, 55);
      this.progressOverall.Name = "progressOverall";
      this.progressOverall.Size = new Size(375, 19);
      this.progressOverall.TabIndex = 1;
      this.progressCurrentFile.Location = new Point(12, 19);
      this.progressCurrentFile.Name = "progressCurrentFile";
      this.progressCurrentFile.Size = new Size(375, 19);
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
      this.ssStatus.Size = new Size(500, 22);
      this.ssStatus.TabIndex = 27;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(38, 17);
      this.lblStatus.Text = "Ready";
      this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(504, 370);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.ssStatus);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmMultipleBooksDownload);
      this.Padding = new Padding(2);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Multiple Books Download";
      this.Load += new EventHandler(this.frmAllBooksDownload_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmAllBooksDownload_FormClosing);
      this.pnlForm.ResumeLayout(false);
      this.pnlControl.ResumeLayout(false);
      this.pnlControl.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public delegate void UpdateProgressDelegate();

    public delegate void UpdateProgressbarsDel(int iProcess);

    private delegate void StatusDelegate(string status);

    private delegate void ProgressStatusDelegate(string progressStatus);

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void ListClearItemsDelegate();

    private delegate void BooksListClearItemsDelegate();

    private delegate void ListAddItemDelegate(ListViewItem item);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void ListEditStatusDelegate(int index, string status);

    private delegate string getFileNameDelegate(int index);

    private delegate string getFileDateFromListNodeTagDelegate(int index);

    private delegate bool getBookCheckedStatusDelegate(int index);

    private delegate void OverallProgressUpdateDelegate(int value);

    private delegate void UpdateProgressLabelDelegate(int prog);

    private delegate void UpdateFileProgressDelegate(int prog);

    private delegate void ShowProxyAuthWarningDelegate();

    private delegate void ShowProxyAuthenticationDelegate();
  }
}

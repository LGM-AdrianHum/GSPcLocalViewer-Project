// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmViewer
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
  public class frmViewer : Form
  {
    public static bool MiniMapChk = false;
    private static string iniValueMiniMap = "";
    public Dictionary<string, string> dicPLSettings = new Dictionary<string, string>();
    public Dictionary<string, string> dicSLSettings = new Dictionary<string, string>();
    public string sFirstPageTitle = string.Empty;
    public List<string> lstFilteredPages = new List<string>();
    private string MessageDjVu = string.Empty;
    private const string dllZipper = "ZIPPER.dll";
    private const int WM_NCLBUTTONDBLCLK = 163;
    private const int WM_NCRBUTTONUP = 165;
    private const int WM_NCHITTEST = 132;
    private const int WM_SYSCOMMAND = 274;
    private const int WM_NCLBUTTONDOWN = 161;
    private const int WM_NCRBUTTONDOWN = 164;
    private const int WM_MOUSEACTIVATE = 33;
    private const int MA_NOACTIVATEANDEAT = 4;
    private const int WM_ENABLE = 10;
    public XmlDocument xmlDocument;
    private bool bPicMemoDelete;
    public bool bIsOldINI;
    public bool bFromPopup;
    public bool bSaveMemoOnBookLevel;
    public bool bSaveMemoOnBookLevelChecked;
    public int intMemoType;
    public bool bObjFrmTreeviewClosed;
    public bool bObjFrmPictureClosed;
    public bool bObjFrmPartlistClosed;
    public bool bObjFrmSelectionlistClosed;
    public frmViewerTreeview objFrmTreeview;
    public frmViewerPicture objFrmPicture;
    public frmViewerPartslist objFrmPartlist;
    public frmSelectionList objFrmSelectionlist;
    public frmInfo objFrmInfo;
    private Dimmer objDimmer;
    public History objHistory;
    public bool bPartsListClosed;
    public bool bPictureClosed;
    public bool ISPDF;
    private string[] p_ArgsO;
    public string[] p_ArgsF;
    private string[] p_ArgsS;
    public int p_ServerId;
    private string p_BookId;
    private XmlNode p_BookNode;
    private XmlNode p_SchemaNode;
    private string CurrentLanguage;
    private string p_BookType;
    public XmlDocument xDocLocalMemo;
    public XmlDocument xDocGlobalMemo;
    private Dashbord frmParent;
    public bool p_Encrypted;
    public bool p_Compressed;
    private int iPageJumpImageIndex;
    public string sBookType;
    public frmMemo objFrmMemo;
    public string sSearchHistoryPath;
    public bool bImageClosed;
    private Download objDownloader;
    public bool bIsSelectionListPrint;
    public DataTable gdtselectionListTable;
    private IContainer components;
    private ToolStripContainer toolStripContainer1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private StatusStrip ssStatus;
    private ToolStripStatusLabel lblStatus;
    private ToolStripMenuItem closeToolStripMenuItem;
    private ToolStripMenuItem viewToolStripMenuItem;
    private ToolStripMenuItem restoreDefaultsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    public ToolStripMenuItem contentsToolStripMenuItem;
    public ToolStripMenuItem pictureToolStripMenuItem;
    public ToolStripMenuItem partslistToolStripMenuItem;
    public ToolStripMenuItem informationToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem settingsToolStripMenuItem;
    private Panel pnlForm2;
    private Panel pnlForm;
    private DockPanel objDocPanel;
    private PictureBox picDisable;
    private ToolStripSeparator toolStripSeparator3;
    private BackgroundWorker bgWorker;
    private ToolStripMenuItem bookmarksToolStripMenuItem;
    private ToolStripMenuItem addBookmarksToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem searchToolStripMenuItem;
    private ToolStripMenuItem pageNameToolStripMenuItem;
    private ToolStripMenuItem partNumberToolStripMenuItem;
    private ToolStrip tsHistory;
    private ToolStripButton tsbHistoryBack;
    private ToolStripButton tsbHistoryForward;
    private ToolStrip tsFunctions;
    private ToolStripButton tsbPrint;
    private ToolStripButton tsbAddBookmarks;
    private ToolStripButton tsbConfiguration;
    private ToolStripButton tsbAbout;
    private ToolStrip tsView;
    private ToolStripButton tsbViewContents;
    private ToolStripButton tsbViewPicture;
    private ToolStripButton tsbViewPartslist;
    private ToolStripSeparator tsbViewSeparator;
    private ToolStripButton tsbViewInfo;
    private ToolStrip tsSearch;
    private ToolStripButton tsbSearchPageName;
    private ToolStripButton tsbSearchPartNumber;
    private ToolStripMenuItem printToolStripMenuItem;
    private ToolStripMenuItem partNameToolStripMenuItem;
    private ToolStripMenuItem textSearchToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStrip tsNavigate;
    private ToolStripButton tsbNavigateFirst;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripButton tsbNavigatePrevious;
    private ToolStripButton tsbNavigateNext;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripButton tsbNavigateLast;
    private ToolStripButton tsbSearchPartName;
    private ToolStripButton tsbSearchText;
    public ToolStripMenuItem selectionListToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem aboutGSPcLocalToolStripMenuItem;
    private ToolStripMenuItem memoSettingsToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripButton tsbPicNext;
    private ToolStripTextBox tstPicNo;
    private ToolStripButton tsbPicPrev;
    private ToolStripLabel tslPic;
    private ToolStripDropDownButton tsbHistoryList;
    private ToolStripButton tsbPicZoomIn;
    private ToolStripButton tsbPicZoomOut;
    public ToolStripButton tsbPicZoomSelect;
    public ToolStripButton tsbPicCopy;
    private ToolStripSeparator toolStripSeparator11;
    private ToolStripMenuItem closeAllToolStripMenuItem;
    private ToolStripMenuItem addOnToolStripMenuItem;
    public ToolStripButton tsbPicPanMode;
    private ToolStripButton tsbMemoList;
    private ToolStrip tsPortal;
    private ToolStripButton tsbPortal;
    private ToolStripButton tsbOpenBook;
    private ToolStripMenuItem goToPortalToolStripMenuItem;
    private ToolStripMenuItem navigationToolStripMenuItem;
    private ToolStripMenuItem firstPageToolStripMenuItem;
    private ToolStripMenuItem previousPageToolStripMenuItem;
    private ToolStripMenuItem nextPageToolStripMenuItem;
    private ToolStripMenuItem lastPageToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator12;
    private ToolStripMenuItem previousViewToolStripMenuItem;
    private ToolStripMenuItem nextViewToolStripMenuItem;
    private ToolStripMenuItem memoDetailsToolStripMenuItem;
    private ToolStripMenuItem addPictureMemoToolStripMenuItem;
    private ToolStripMenuItem addPartMemoToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator13;
    private ToolStripMenuItem memoRecoveryToolStripMenuItem;
    private ToolStripMenuItem advancedSearchToolStripMenuItem;
    private ToolStripMenuItem fontToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator14;
    private ToolStripMenuItem generalToolStripMenuItem;
    private ToolStripMenuItem pageNameSearchSettingsToolStripMenuItem;
    private ToolStripMenuItem partNameSearchSettingsToolStripMenuItem;
    private ToolStripMenuItem partNumberSearchSettingsToolStripMenuItem;
    private ToolStripMenuItem toolsToolStripMenuItem;
    public ToolStripMenuItem singleBookToolStripMenuItem;
    public ToolStripMenuItem multipleBooksToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator16;
    private ToolStripMenuItem viewMemoListToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator17;
    private ToolStripButton tsbRestoreDefaults;
    private ToolStripButton tsbAddPictureMemo;
    private ToolStripButton tsbMemoRecovery;
    private ToolStrip tsTools;
    public ToolStripButton tsbSingleBookDownload;
    public ToolStripButton tsbMultipleBooksDownload;
    private ToolStripSeparator toolStripSeparator18;
    private ToolStripButton tsbDataCleanup;
    private ToolStripSeparator toolStripSeparator19;
    private ToolStripSeparator toolStripSeparator20;
    public ToolStripStatusLabel lblMode;
    public ToolStripMenuItem workOffLineMenuItem;
    private ToolStripSeparator toolStripSeparator21;
    private ToolStripButton tsbConnection;
    private ToolStripSeparator toolStripSeparator15;
    private ToolStripMenuItem manageDiskSpaceToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem connectionToolStripMenuItem;
    private ToolStripMenuItem textSearceNameSearchSettingsToolStripMenuItem;
    public ToolStripMenuItem miniMapToolStripMenuItem;
    private ToolStripButton tsbFitPage;
    private ToolStripButton tsBRotateLeft;
    private ToolStripButton tsBRotateRight;
    private ToolStripSeparator toolStripSeparator22;
    public ToolStripButton tsbPicSelectText;
    private ToolStripMenuItem printPageToolStripMenuItem;
    private ToolStripMenuItem printPictureToolStripMenuItem;
    private ToolStripMenuItem printListToolStripMenuItem;
    private ToolStripMenuItem printSelectionListToolStripMenuItem;
    private ToolStripMenuItem saveDefaultsToolStripMenuItem;
    private ToolStripButton tsbSearchPartAdvance;
    private ToolStripButton tsbHelp;
    private ToolStripMenuItem gSPcLocalHelpToolStripMenuItem;
    private ToolStripMenuItem advanceSearchSettingsToolStripMenuItem;
    private ToolStripMenuItem ColorToolStripMenuItem;
    private ToolStripButton tsbThirdPartyBasket;
    public ToolStripButton tsbFindText;
    public ToolStripButton tsbThumbnail;
    private ToolStripSeparator toolStripSeparator10;
    internal ToolStrip tsPic;
    private ToolStripSeparator toolStripSeparator23;
    private ToolStripMenuItem partsListSettingsToolStripMenuItem;
    private ToolStripMenuItem selectionListSettingsToolStripMenuItem;

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    public static extern bool SetActiveWindow(IntPtr hWnd);

    public frmViewer(Dashbord frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.objDimmer = new Dimmer();
      this.xmlDocument = new XmlDocument();
      this.bObjFrmTreeviewClosed = this.bObjFrmPictureClosed = this.bObjFrmPartlistClosed = this.bObjFrmSelectionlistClosed = true;
      Program.configPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\docking.config";
      Program.defaultsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\defaults.config";
      if (Program.bNoViewerOpen)
        this.ReadUserSettingINI();
      this.objHistory = new History();
      this.bPartsListClosed = false;
      this.bPictureClosed = false;
      this.p_ArgsO = (string[]) null;
      this.p_ArgsF = (string[]) null;
      this.p_ServerId = 0;
      this.p_BookId = string.Empty;
      this.p_BookNode = (XmlNode) null;
      this.p_SchemaNode = (XmlNode) null;
      this.sBookType = "GSP";
      this.sSearchHistoryPath = string.Empty;
      this.iPageJumpImageIndex = 0;
      this.UpdateFont();
      frmViewer.SetWindowSizeFromSettings(Settings.Default.appCurrentSize, (Form) this);
      this.InitializeAddOnsGUI();
      this.objDownloader = new Download(this);
      this.ReadSearchHistoryPath();
      this.gdtselectionListTable = new DataTable();
      this.LoadXMLFirstTime();
      this.SetText();
      this.intMemoType = this.GetMemoType();
    }

    public void SetCurrentServerID(int curServerId)
    {
      this.p_ServerId = curServerId;
    }

    public void SetArguments(string[] args)
    {
      if (args != null && args.Length > 0)
      {
        bool flag1 = false;
        string str1 = string.Empty;
        string str2 = string.Empty;
        string str3 = string.Empty;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        for (int index = 0; index < args.Length; ++index)
        {
          if (args[index].Equals("-o"))
          {
            flag2 = true;
            flag3 = false;
            flag4 = false;
          }
          else if (args[index].Equals("-f"))
          {
            flag2 = false;
            flag3 = true;
            flag4 = false;
          }
          else if (args[index].Equals("-s"))
          {
            flag2 = false;
            flag3 = false;
            flag4 = true;
          }
          else if (args[index].Equals("-p"))
          {
            flag2 = false;
            flag3 = false;
            flag4 = false;
          }
          else
          {
            if (flag2 && !args[index].Equals(" "))
              str1 = !str1.Equals(string.Empty) ? str1 + ":::" + args[index].Replace(":", "$#$") : args[index].Replace(":", "$#$");
            if (flag3 && !args[index].Equals(" ") && (args[index].Contains("=") && args[index].Substring(0, args[index].IndexOf("=")).Trim() != string.Empty) && args[index].Substring(args[index].IndexOf("=") + 1, args[index].Length - (args[index].IndexOf("=") + 1)).Trim() != string.Empty)
              str2 = !str2.Equals(string.Empty) ? str2 + ":::" + args[index] : args[index];
            if (flag4 && !args[index].Equals(" ") && (args[index].Contains("=") && args[index].Substring(0, args[index].IndexOf("=")).Trim() != string.Empty) && args[index].Substring(args[index].IndexOf("=") + 1, args[index].Length - (args[index].IndexOf("=") + 1)).Trim() != string.Empty)
            {
              str3 = !str3.Equals(string.Empty) ? str3 + ":::" + args[index] : args[index];
              if (!flag1 && (str3.Trim().ToUpper().Equals("OPT=OFF") || str3.Trim().ToUpper().Contains(":::OPT=OFF") || str3.Trim().ToUpper().Contains("OPT=OFF:::")))
                flag1 = true;
              Program.objAppMode.bFromPortal = args[index].ToLower().Contains("fromportal=true");
            }
          }
        }
        this.p_ArgsO = str1.Split(new string[1]{ ":::" }, StringSplitOptions.RemoveEmptyEntries);
        try
        {
          if (str1.ToUpper().Contains("VIEWERISUPDATED"))
          {
            for (int index = 0; index < this.p_ArgsO.Length; ++index)
            {
              if (this.p_ArgsO[index].Contains("VIEWERISUPDATED"))
                this.p_ArgsO[index] = this.p_ArgsO[index].Replace("VIEWERISUPDATED", "1");
            }
          }
        }
        catch
        {
        }
        if (str1.Contains("$#$"))
        {
          for (int index = 0; index < this.p_ArgsO.Length; ++index)
          {
            if (this.p_ArgsO[index].Contains("$#$"))
              this.p_ArgsO[index] = this.p_ArgsO[index].Replace("$#$", ":");
          }
        }
        this.p_ArgsS = str3.Split(new string[1]{ ":::" }, StringSplitOptions.RemoveEmptyEntries);
        if (flag1)
          return;
        this.p_ArgsF = str2.Split(new string[1]{ ":::" }, StringSplitOptions.RemoveEmptyEntries);
      }
      else
      {
        this.p_ArgsO = (string[]) null;
        this.p_ArgsF = (string[]) null;
        this.p_ArgsS = (string[]) null;
      }
    }

    private void frmViewer_Load(object sender, EventArgs e)
    {
      this.WindowState = FormWindowState.Maximized;
      this.TopMost = true;
      this.Focus();
      string filename = string.Empty;
      if (this.p_ArgsS != null)
      {
        foreach (string str in this.p_ArgsS)
        {
          if (str.ToUpper().Contains("LNG="))
          {
            filename = str.Substring(str.IndexOf("=") + 1, str.Length - (str.IndexOf("=") + 1)).Trim().ToUpper();
            break;
          }
        }
      }
      if (filename != string.Empty)
        Settings.Default.appLanguage = filename;
      this.LoadXML(filename);
      ToolStripManager.LoadSettings((Form) this);
      this.EnableMenuAndToolbar(false);
      this.CreateForms();
      this.ShowHidePictureToolbar();
      this.ShowHidePartslistToolbar();
      this.OnOffFeatures();
      if (this.p_ArgsO == null || this.p_ArgsO.Length == 0)
      {
        frmOpenBook frmOpenBook = new frmOpenBook(this);
        this.ShowDimmer();
        int num = (int) frmOpenBook.ShowDialog();
      }
      else
        this.LoadDataDirect();
      if (Program.objAppMode.bFromPortal)
      {
        Program.objAppFeatures.bDcMode = false;
        if (Program.objAppMode.bFirstTime)
        {
          Program.objAppMode.bWorkingOffline = !Program.objAppMode.InternetConnected;
          this.workOffLineMenuItem.Checked = Program.objAppMode.bWorkingOffline;
        }
        else
          this.workOffLineMenuItem.Checked = Program.objAppMode.bWorkingOffline;
      }
      else if (Program.objAppFeatures.bDcMode)
      {
        Program.objAppMode.bWorkingOffline = true;
        this.workOffLineMenuItem.Enabled = false;
      }
      else
      {
        Program.objAppMode.bWorkingOffline = !Program.objAppMode.InternetConnected;
        this.workOffLineMenuItem.Checked = Program.objAppMode.bWorkingOffline;
      }
      if (Program.objAppMode.bWorkingOffline)
      {
        this.lblMode.Text = "Working Offline";
        this.lblMode.Image = (Image) GSPcLocalViewer.Properties.Resources.offline;
      }
      else
      {
        this.lblMode.Text = "Working Online";
        this.lblMode.Image = (Image) GSPcLocalViewer.Properties.Resources.online;
      }
      this.contentsToolStripMenuItem.Checked = this.objFrmTreeview.Visible;
      this.pictureToolStripMenuItem.Checked = this.objFrmPicture.Visible;
      this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
      this.partslistToolStripMenuItem.Checked = this.objFrmPartlist.Visible;
      this.informationToolStripMenuItem.Checked = this.objFrmInfo.Visible;
      if (Program.objAppFeatures.bMiniMap && this.miniMapToolStripMenuItem.Checked && frmViewer.iniValueMiniMap == "true")
      {
        frmViewer.MiniMapChk = true;
      }
      else
      {
        frmViewer.MiniMapChk = false;
        this.miniMapToolStripMenuItem.Checked = false;
      }
      this.GetPicMemoDeleteValue();
    }

    [DllImport("ole32.dll")]
    private static extern void CoFreeUnusedLibraries();

    private void frmViewer_FormClosing(object sender, FormClosingEventArgs e)
    {
      while (!this.bObjFrmTreeviewClosed || !this.bObjFrmPictureClosed || (!this.bObjFrmPartlistClosed || !this.bObjFrmSelectionlistClosed))
      {
        Thread.Sleep(300);
        Application.DoEvents();
      }
      this.objFrmPicture.DisposeDjVuControl();
      try
      {
        this.objFrmPartlist.IsHidden = this.bPartsListClosed;
      }
      catch
      {
      }
      this.WriteUserSettingINI();
      ToolStripManager.SaveSettings((Form) this);
      this.CopyConfigurationFile();
      Settings.Default.Save();
      if (this.objFrmTreeview.DockPanel != null && this.objFrmPicture.DockPanel != null && (this.objFrmSelectionlist.DockPanel != null && this.objFrmPartlist.DockPanel != null) && this.objFrmInfo.DockPanel != null)
        this.objDocPanel.SaveAsXml(Program.configPath);
      this.objFrmSelectionlist.SaveSelectionListColumnSizes();
      this.objFrmPartlist.SavePartsListColumnSizes();
      try
      {
        this.objHistory = (History) null;
        if (this.objFrmPicture.wbPDF != null)
        {
          this.objFrmPicture.wbPDF.Dispose();
          Application.DoEvents();
          frmViewer.CoFreeUnusedLibraries();
        }
      }
      catch
      {
      }
      if (!this.bPicMemoDelete || !Settings.Default.EnableLocalMemo)
        return;
      this.DeletePicLocalMemos();
    }

    public void UpdatePartAndSelectionList()
    {
      try
      {
        this.objFrmSelectionlist.selListInitialize();
        this.objFrmPartlist.Set_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
        this.objFrmPartlist.Set_MemoCols_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
      }
      catch (Exception ex)
      {
      }
    }

    private void frmViewer_Activated(object sender, EventArgs e)
    {
      bool flag = false;
      if (this.objFrmPartlist != null && (this.objFrmPartlist.dgPartslist.RowsDefaultCellStyle.SelectionBackColor != Settings.Default.appHighlightBackColor || this.objFrmPartlist.dgPartslist.RowsDefaultCellStyle.SelectionForeColor != Settings.Default.appHighlightForeColor || (this.objFrmPartlist.pnlInfo.BackColor != Settings.Default.PartsListInfoBackColor || this.objFrmPartlist.pnlInfo.ForeColor != Settings.Default.PartsListInfoForeColor)))
        flag = true;
      if (!flag && !(this.Font.Name != Settings.Default.appFont.Name) && (double) this.Font.Size == (double) Settings.Default.appFont.Size)
        return;
      this.pnlForm.Visible = false;
      this.UpdateFont();
      this.pnlForm.Visible = true;
    }

    private void CreateForms()
    {
      this.objFrmTreeview = new frmViewerTreeview(this);
      this.objFrmTreeview.HideOnClose = true;
      this.objFrmPicture = new frmViewerPicture(this);
      this.objFrmPicture.HideOnClose = true;
      this.objFrmPartlist = new frmViewerPartslist(this);
      this.objFrmPartlist.HideOnClose = false;
      this.objFrmSelectionlist = new frmSelectionList(this);
      this.objFrmSelectionlist.HideOnClose = true;
      this.objFrmInfo = new frmInfo(this);
      this.objFrmInfo.HideOnClose = true;
    }

    private IDockContent GetContentFromPersistString(string persistString)
    {
      if (persistString == typeof (frmViewerTreeview).ToString())
        return (IDockContent) this.objFrmTreeview;
      if (persistString == typeof (frmViewerPicture).ToString())
        return (IDockContent) this.objFrmPicture;
      if (persistString == typeof (frmSelectionList).ToString())
        return (IDockContent) this.objFrmSelectionlist;
      if (persistString == typeof (frmViewerPartslist).ToString())
        return (IDockContent) this.objFrmPartlist;
      if (persistString == typeof (frmInfo).ToString())
        return (IDockContent) this.objFrmInfo;
      return (IDockContent) null;
    }

    private void ShowForms()
    {
      if (this.objFrmTreeview.IsDisposed && this.objFrmPicture.IsDisposed && (this.objFrmSelectionlist.IsDisposed && this.objFrmPartlist.IsDisposed) && this.objFrmInfo.IsDisposed)
        this.CreateForms();
      this.EnableMenuAndToolbar(true);
      this.pnlForm2.BringToFront();
      bool flag = false;
      if (File.Exists(Program.configPath))
      {
        try
        {
          DeserializeDockContent deserializeContent = new DeserializeDockContent(this.GetContentFromPersistString);
          this.objDocPanel.LoadFromXml(Program.configPath, deserializeContent);
          try
          {
            this.bPartsListClosed = this.objFrmPartlist.IsHidden;
          }
          catch
          {
          }
        }
        catch
        {
          flag = true;
        }
      }
      else
        flag = true;
      if (flag)
      {
        this.objFrmTreeview.Show(this.objDocPanel);
        this.objFrmPicture.Show(this.objDocPanel);
        this.objFrmSelectionlist.Show(this.objDocPanel);
        this.objFrmPartlist.Show(this.objDocPanel);
        this.objFrmInfo.Show(this.objDocPanel);
        this.objFrmPartlist.DockState = DockState.DockBottom;
        this.objFrmTreeview.DockState = DockState.DockLeft;
        this.objFrmInfo.DockState = DockState.DockRightAutoHide;
        this.objFrmSelectionlist.DockState = DockState.DockRightAutoHide;
      }
      this.objFrmPartlist.Hide();
      this.ShowHideSelectionList();
      this.pnlForm2.SendToBack();
      this.contentsToolStripMenuItem.Checked = this.objFrmTreeview.Visible;
      this.pictureToolStripMenuItem.Checked = this.objFrmPicture.Visible;
      this.informationToolStripMenuItem.Checked = this.objFrmInfo.Visible;
      this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      if (this.objFrmTreeview != null)
        this.objFrmTreeview.UpdateFont();
      if (this.objFrmPartlist != null)
        this.objFrmPartlist.UpdateFont();
      if (this.objFrmInfo != null)
        this.objFrmInfo.UpdateFont();
      if (this.objFrmSelectionlist != null)
        this.objFrmSelectionlist.UpdateFont();
      this.menuStrip1.Font = Settings.Default.appFont;
    }

    public void LoadData(int serverId, string bookPublishingId, XmlNode bookNode, XmlNode schemaNode, string bookType)
    {
      if (Program.bNoViewerOpen || this.objFrmTreeview.GetNodesCount() <= 0 || Settings.Default.OpenInCurrentInstance)
      {
        this.p_ServerId = serverId;
        this.p_BookId = bookPublishingId;
        this.p_BookNode = bookNode;
        this.p_SchemaNode = schemaNode;
        this.p_BookType = bookType;
        if (this.objFrmTreeview.DockPanel == null && this.objFrmPicture.DockPanel == null && (this.objFrmSelectionlist.DockPanel == null && this.objFrmPartlist.DockPanel == null) && this.objFrmInfo.DockPanel == null)
          this.ShowForms();
        this.objFrmTreeview.ShowLoading();
        this.objFrmPicture.ShowLoading();
        this.objFrmInfo.ShowLoading();
        if (this.objFrmPartlist != null)
          this.HidePartsList();
        this.objFrmTreeview.LoadBook();
        Program.bNoViewerOpen = false;
      }
      else
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) "-o");
        arrayList.Add((object) Program.iniServers[serverId].sIniKey);
        arrayList.Add((object) bookPublishingId);
        if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        {
          arrayList.Add((object) "-f");
          foreach (string str in this.p_ArgsF)
            arrayList.Add((object) str);
        }
        if (this.p_ArgsS != null && this.p_ArgsS.Length > 0)
        {
          arrayList.Add((object) "-s");
          foreach (string str in this.p_ArgsS)
          {
            if (!str.ToLower().Contains("fromportal="))
              arrayList.Add((object) str);
          }
          arrayList.Add((object) ("fromportal=" + Program.objAppMode.bFromPortal.ToString()));
        }
        else
        {
          arrayList.Add((object) "-s");
          arrayList.Add((object) ("fromportal=" + Program.objAppMode.bFromPortal.ToString()));
        }
        this.frmParent.NextTime((string[]) arrayList.ToArray(typeof (string)));
      }
    }

    private object CloneControls(object o)
    {
      System.Type type = o.GetType();
      PropertyInfo[] properties = type.GetProperties();
      object obj = type.InvokeMember("", BindingFlags.CreateInstance, (Binder) null, o, (object[]) null);
      foreach (PropertyInfo propertyInfo in properties)
      {
        if (propertyInfo.CanWrite)
          propertyInfo.SetValue(obj, propertyInfo.GetValue(o, (object[]) null), (object[]) null);
      }
      return obj;
    }

    public void SelectTreeNode()
    {
      if (this.p_ArgsO != null && this.p_ArgsO.Length > 2 && this.p_ArgsO[2] != "nil")
      {
        int result = 1;
        if (!int.TryParse(this.p_ArgsO[2], out result))
        {
          try
          {
            TreeNode treeNodeByPageName = this.objFrmTreeview.FindTreeNodeByPageName(this.objFrmTreeview.tvBook.Nodes, this.p_ArgsO[2]);
            this.p_ArgsO[2] = "nil";
            if (treeNodeByPageName != null)
            {
              if (this.objFrmTreeview.GetNodesCount() > 0)
                this.objFrmTreeview.tvBook.SelectedNode = treeNodeByPageName;
            }
            else
            {
              this.objFrmPicture.HideLoading1();
              this.objFrmPicture.LoadBlankPage(string.Empty);
            }
          }
          catch
          {
          }
        }
        else if (this.objFrmTreeview.GetNodesCount() > 0)
          this.objFrmTreeview.SelectTreeNode(this.p_ArgsO[2]);
      }
      else if (this.objFrmTreeview.GetNodesCount() > 0)
        this.objFrmTreeview.SelectTreeNode(string.Empty);
      if (this.objFrmTreeview.IsActivated && !this.objFrmTreeview.IsHidden || this.objFrmTreeview.GetNodesCount() <= 0)
        return;
      this.objFrmTreeview.LoadPictureInTree();
    }

    public void SelectTreeNodeByName(string pageName)
    {
      if (!pageName.Equals(string.Empty))
        this.objFrmTreeview.SelectTreeNodeByPageName(pageName);
      else
        this.objFrmTreeview.SelectTreeNode(string.Empty);
    }

    public void LoadPicture(XmlNode schemaNode, XmlNode pageNode)
    {
      if (this.p_ArgsO != null && this.p_ArgsO.Length > 3 && this.p_ArgsO[3] != "nil")
      {
        int result1 = 0;
        if (int.TryParse(this.p_ArgsO[3], out result1))
        {
          if (result1 <= 0)
            result1 = 1;
          int picIndex = result1 - 1;
          if (this.p_ArgsO != null && this.p_ArgsO.Length > 4 && this.p_ArgsO[4] != "nil")
          {
            int result2 = 0;
            if (int.TryParse(this.p_ArgsO[4], out result2))
            {
              if (result2 <= 0)
                result2 = 1;
              int listIndex = result2 - 1;
              this.objFrmPicture.LoadPicture(schemaNode, pageNode, picIndex, listIndex);
            }
            else
              this.objFrmPicture.LoadPicture(schemaNode, pageNode, picIndex, 0);
          }
          else
            this.objFrmPicture.LoadPicture(schemaNode, pageNode, picIndex, 0);
        }
        else
          this.objFrmPicture.LoadPicture(schemaNode, pageNode, 0, 0);
      }
      else if (this.iPageJumpImageIndex != 0)
      {
        this.objFrmPicture.LoadPicture(schemaNode, pageNode, this.iPageJumpImageIndex, 0);
        this.iPageJumpImageIndex = 0;
      }
      else
        this.objFrmPicture.LoadPicture(schemaNode, pageNode, 0, 0);
    }

    public void UpdateStatus(string status)
    {
      if (this.ssStatus.InvokeRequired)
        this.ssStatus.Invoke((Delegate) new frmViewer.UpdateStatusDelegate(this.UpdateStatus), (object) status);
      else
        this.lblStatus.Text = status;
    }

    public void UpdateCurrentPageForPartslist(bool picLoaded, XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
    {
      this.objFrmPartlist.UpdateCurrentPageForPartslist(picLoaded, schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
    }

    public void LoadPartsList(XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
    {
      try
      {
        this.objFrmPartlist.highlightPartNo = this.p_ArgsO == null || this.p_ArgsO.Length <= 5 || !(this.p_ArgsO[5] != "nil") ? string.Empty : this.p_ArgsO[5];
        this.objFrmPartlist.changePartList(schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
        if (this.p_ArgsO == null)
          return;
        this.p_ArgsO = (string[]) null;
      }
      catch
      {
        this.HidePartsList();
      }
    }

    public void ShowPartsList()
    {
      if (this.objFrmPartlist.InvokeRequired)
      {
        this.objFrmPartlist.Invoke((Delegate) new frmViewer.ShowPartsListDelegate(this.ShowPartsList));
      }
      else
      {
        try
        {
          string focusedWindow = this.GetFocusedWindow();
          this.EnablePartslistShowHideButton(true);
          if (!this.objFrmPartlist.Visible)
          {
            this.objFrmPartlist.Show(this.objDocPanel);
            this.objFrmPartlist.ShowPartsList();
          }
          this.setFocusedWindow(focusedWindow);
        }
        catch
        {
        }
      }
    }

    public void ShowPicture()
    {
      if (this.objFrmPicture.InvokeRequired)
      {
        this.objFrmPicture.Invoke((Delegate) new frmViewer.ShowPartsListDelegate(this.ShowPicture));
      }
      else
      {
        try
        {
          this.EnablePictureShowHideButton(true);
          if (this.bImageClosed || this.bPictureClosed)
            return;
          string focusedWindow = this.GetFocusedWindow();
          if (!this.objFrmPicture.Visible)
            this.objFrmPicture.Show(this.objDocPanel);
          this.miniMapToolStripMenuItem.Enabled = true;
          if (frmViewer.MiniMapChk)
            this.objFrmPicture.ShowHideMiniMap(true);
          this.setFocusedWindow(focusedWindow);
        }
        catch
        {
        }
      }
    }

    private string GetFocusedWindow()
    {
      string str = string.Empty;
      if (this.objFrmPicture.IsActivated)
        str = "Picture";
      else if (this.objFrmTreeview.IsActivated)
        str = "Content";
      else if (this.objFrmSelectionlist.IsActivated)
        str = "SelectionList";
      else if (this.objFrmPartlist.IsActivated)
        str = "Partlist";
      else if (this.objFrmInfo.IsActivated)
        str = "Info";
      return str;
    }

    public void setFocusedWindow(string strActivePane)
    {
      if (this.objFrmPicture.IsHidden)
        return;
      this.objFrmPicture.Activate();
    }

    public void HidePartsList()
    {
      if (this.objFrmPartlist.InvokeRequired)
      {
        this.objFrmPartlist.Invoke((Delegate) new frmViewer.HidePartsListDelegate(this.HidePartsList));
      }
      else
      {
        this.EnablePartslistShowHideButton(false);
        this.objFrmPartlist.Hide();
      }
    }

    public void HidePicture()
    {
      if (this.objFrmPicture.InvokeRequired)
      {
        this.objFrmPicture.Invoke((Delegate) new frmViewer.HidePartsListDelegate(this.HidePicture));
      }
      else
      {
        this.EnablePictureShowHideButton(false);
        this.objFrmPicture.Hide();
        this.bPictureClosed = true;
        this.miniMapToolStripMenuItem.Enabled = false;
        this.objFrmPicture.ShowHideMiniMap(false);
      }
    }

    public void ScalePicture(int x, int y, int width, int height)
    {
      this.objFrmPicture.ScalePicture((float) x, (float) y, width, height);
    }

    public void HighlightPicture(int x, int y, int width, int height)
    {
      this.objFrmPicture.HighlightPicture(x, y, width, height);
    }

    public void HighlightPartslist(string argName, string argValue)
    {
      this.objFrmPartlist.HighlightPartslist(argName, argValue);
    }

    public void RemoveHighlightOnPicture()
    {
      this.objFrmPicture.RemoveHighlightOnPicture();
    }

    public void ZoomFitPage(bool bFitPage)
    {
      this.objFrmPicture.ZoomFitPage(bFitPage);
    }

    private void EnableMenuAndToolbar(bool value)
    {
      this.viewToolStripMenuItem.Enabled = value;
      this.settingsToolStripMenuItem.Enabled = value;
      this.bookmarksToolStripMenuItem.Enabled = value;
      this.searchToolStripMenuItem.Enabled = value;
      this.singleBookToolStripMenuItem.Enabled = value;
      this.multipleBooksToolStripMenuItem.Enabled = value;
      this.addOnToolStripMenuItem.Enabled = value;
      this.printToolStripMenuItem.Enabled = value;
      this.goToPortalToolStripMenuItem.Enabled = value;
      this.navigationToolStripMenuItem.Enabled = value;
      this.memoDetailsToolStripMenuItem.Enabled = value;
      this.tsView.Enabled = value;
      this.tsSearch.Enabled = value;
      this.tsNavigate.Enabled = value;
      this.tsbPrint.Enabled = value;
      this.tsbAddBookmarks.Enabled = value;
      this.tsbConfiguration.Enabled = value;
      this.tsbAddPictureMemo.Enabled = value;
      this.tsbMemoList.Enabled = value;
      this.tsbPortal.Enabled = value;
      this.tsbMemoRecovery.Enabled = value;
      this.tsbSingleBookDownload.Enabled = value;
      this.tsbMultipleBooksDownload.Enabled = value;
      this.tsbThirdPartyBasket.Enabled = value;
      this.tsPic.Enabled = false;
    }

    public void EnablePartslistShowHideButton(bool value)
    {
      if (this.sBookType.ToUpper() == "GSC")
      {
        this.partslistToolStripMenuItem.Visible = false;
        this.tsbViewPartslist.Visible = false;
      }
      else
      {
        this.partslistToolStripMenuItem.Enabled = value;
        this.tsbViewPartslist.Enabled = value;
      }
    }

    public void EnablePictureShowHideButton(bool value)
    {
      this.pictureToolStripMenuItem.Enabled = value;
      this.tsbViewPicture.Enabled = value;
    }

    public void UpdatePicToolstrip(bool enablePrev, bool enableNext, string picNo)
    {
      this.tsPic.Enabled = true;
      this.tslPic.Enabled = true;
      this.tsbPicPrev.Enabled = enablePrev;
      this.tsbPicNext.Enabled = enableNext;
      this.tstPicNo.Text = picNo;
    }

    private void ReloadSamePage()
    {
      this.p_ArgsO = new string[6];
      this.p_ArgsO[0] = Program.iniServers[this.p_ServerId].sIniKey;
      this.p_ArgsO[1] = this.p_BookId;
      this.p_ArgsO[2] = this.objFrmPicture.CurrentPageId;
      this.p_ArgsO[3] = this.objFrmPartlist.CurrentPicIndex;
      this.p_ArgsO[4] = this.objFrmPartlist.CurrentListIndex;
      this.p_ArgsO[5] = this.objFrmPartlist.CurrentPartNumber;
      this.objFrmTreeview.TreeViewClearSelection();
      this.SelectTreeNode();
    }

    private void frmViewer_FormClosed(object sender, FormClosedEventArgs e)
    {
      try
      {
        this.frmParent.CloseViewer(this);
      }
      catch
      {
      }
    }

    private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        this.frmParent.CloseAll();
      }
      catch
      {
      }
    }

    public void OpenSearchResults(string sServerKey, string sBookCode, string sPageId, string sPicIndex, string sListIndex, string sPartNumber)
    {
      if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
      {
        this.p_ArgsO = new string[6];
        this.p_ArgsO[0] = sServerKey;
        this.p_ArgsO[1] = sBookCode;
        this.p_ArgsO[2] = sPageId;
        this.p_ArgsO[3] = sPicIndex;
        this.p_ArgsO[4] = sListIndex;
        this.p_ArgsO[5] = sPartNumber;
        this.LoadDataDirect();
      }
      else
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) "-o");
        arrayList.Add((object) sServerKey);
        arrayList.Add((object) sBookCode);
        arrayList.Add((object) sPageId);
        arrayList.Add((object) sPicIndex);
        arrayList.Add((object) sListIndex);
        arrayList.Add((object) sPartNumber);
        if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        {
          arrayList.Add((object) "-f");
          foreach (string str in this.p_ArgsF)
            arrayList.Add((object) str);
        }
        if (this.p_ArgsS != null && this.p_ArgsS.Length > 0)
        {
          arrayList.Add((object) "-s");
          foreach (string str in this.p_ArgsS)
            arrayList.Add((object) str);
        }
        this.frmParent.NextTime((string[]) arrayList.ToArray(typeof (string)));
      }
    }

    public void OpenSearchResults(string sServerKey, string sBookCode, string sPageId, string sPicIndex, string sListIndex, string sPartNumber, string sHighlightText)
    {
      if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
      {
        this.p_ArgsO = new string[6];
        this.p_ArgsO[0] = sServerKey;
        this.p_ArgsO[1] = sBookCode;
        this.p_ArgsO[2] = sPageId;
        this.p_ArgsO[3] = sPicIndex;
        this.p_ArgsO[4] = sListIndex;
        this.p_ArgsO[5] = sPartNumber;
        this.LoadDataDirect();
      }
      else
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) "-o");
        arrayList.Add((object) sServerKey);
        arrayList.Add((object) sBookCode);
        arrayList.Add((object) sPageId);
        arrayList.Add((object) sPicIndex);
        arrayList.Add((object) sListIndex);
        arrayList.Add((object) sPartNumber);
        if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        {
          arrayList.Add((object) "-f");
          foreach (string str in this.p_ArgsF)
            arrayList.Add((object) str);
        }
        if (this.p_ArgsS != null && this.p_ArgsS.Length > 0)
        {
          arrayList.Add((object) "-s");
          foreach (string str in this.p_ArgsS)
            arrayList.Add((object) str);
        }
        this.frmParent.NextTime((string[]) arrayList.ToArray(typeof (string)));
      }
    }

    private void ReadSearchHistoryPath()
    {
      try
      {
        this.sSearchHistoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        frmViewer frmViewer = this;
        frmViewer.sSearchHistoryPath = frmViewer.sSearchHistoryPath + "\\" + Application.ProductName;
        this.sSearchHistoryPath += "\\SearchHistory.xml";
        if (File.Exists(this.sSearchHistoryPath))
          return;
        XmlDocument xmlDocument = new XmlDocument();
        string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" + "<SEARCH>\r\n                    <PAGENAMESEARCH></PAGENAMESEARCH>\r\n                    <TEXTSEARCH></TEXTSEARCH>\r\n                    <PARTNUMBERSEARCH></PARTNUMBERSEARCH>\r\n                    <PARTNAMESEARCH></PARTNAMESEARCH>\r\n                    </SEARCH>";
        xmlDocument.LoadXml(xml);
        xmlDocument.Save(this.sSearchHistoryPath);
      }
      catch
      {
      }
    }

    private void workOffLineMenuItem_Click(object sender, EventArgs e)
    {
      this.ChangeApplicationMode();
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void restoreDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        this.pnlForm2.BringToFront();
        if (File.Exists(Program.defaultsPath))
        {
          this.objDocPanel.SuspendLayout();
          DeserializeDockContent deserializeContent = new DeserializeDockContent(this.GetContentFromPersistString);
          this.objFrmSelectionlist.DockPanel = (DockPanel) null;
          this.objFrmPartlist.DockPanel = (DockPanel) null;
          this.objFrmTreeview.DockPanel = (DockPanel) null;
          this.objFrmPicture.DockPanel = (DockPanel) null;
          this.objFrmInfo.DockPanel = (DockPanel) null;
          this.objDocPanel.LoadFromXml(Program.defaultsPath, deserializeContent);
          try
          {
            this.bPartsListClosed = this.objFrmPartlist.IsHidden;
          }
          catch
          {
          }
          if (this.sBookType.ToUpper() == "GSC")
          {
            this.objFrmSelectionlist.Hide();
            this.selectionListToolStripMenuItem.Visible = false;
            this.objFrmPartlist.Hide();
            if (this.bObjFrmPartlistClosed)
              this.objFrmPartlist.Hide();
          }
          else
          {
            this.selectionListToolStripMenuItem.Visible = true;
            if (this.objFrmPartlist.objXmlNodeList.Count == 0)
            {
              this.objFrmPartlist.lblPartsListInfo.Text = string.Empty;
              this.objFrmPartlist.dgPartslist.Visible = false;
              this.objFrmPartlist.picLoading.Visible = false;
            }
          }
        }
        else if (this.objFrmTreeview.DockPanel != null && this.objFrmPicture.DockPanel != null && (this.objFrmSelectionlist.DockPanel != null && this.objFrmPartlist.DockPanel != null))
        {
          this.objDocPanel.DockBottomPortion = 0.25;
          this.objDocPanel.DockLeftPortion = 0.25;
          this.objDocPanel.DockRightPortion = 0.25;
          this.objDocPanel.DockTopPortion = 0.25;
          this.objFrmPartlist.DockState = DockState.DockBottom;
          this.objFrmTreeview.DockState = DockState.DockLeft;
          this.objFrmPicture.DockState = DockState.Document;
          this.bPartsListClosed = false;
          if (!this.tsbViewPartslist.Enabled)
            this.objFrmPartlist.Hide();
          this.contentsToolStripMenuItem.Checked = this.objFrmTreeview.Visible;
          this.pictureToolStripMenuItem.Checked = this.objFrmPicture.Visible;
          this.partslistToolStripMenuItem.Checked = this.objFrmPartlist.Visible;
          if (this.sBookType.ToUpper() == "GSP")
          {
            this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
            this.objFrmSelectionlist.DockState = DockState.DockBottom;
            if (this.objFrmPartlist.objXmlNodeList.Count == 0)
            {
              this.objFrmPartlist.lblPartsListInfo.Text = string.Empty;
              this.objFrmPartlist.dgPartslist.Visible = false;
              this.objFrmPartlist.picLoading.Visible = false;
            }
          }
          else if (this.sBookType.ToUpper() == "GSC" && this.bObjFrmPartlistClosed)
            this.objFrmPartlist.Hide();
          if (!this.tsbViewPicture.Enabled)
            this.objFrmPicture.Hide();
        }
        this.objDocPanel.ResumeLayout(true, true);
        this.pnlForm2.SendToBack();
      }
      catch
      {
      }
    }

    private void saveDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.objFrmTreeview.DockPanel == null || this.objFrmPicture.DockPanel == null || (this.objFrmSelectionlist.DockPanel == null || this.objFrmPartlist.DockPanel == null) || this.objFrmInfo.DockPanel == null)
          return;
        this.objDocPanel.SaveAsXml(Program.defaultsPath);
      }
      catch
      {
      }
    }

    private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.contentsToolStripMenuItem.Checked = !this.contentsToolStripMenuItem.Checked;
      if (!this.contentsToolStripMenuItem.Checked)
        this.objFrmTreeview.Hide();
      else
        this.objFrmTreeview.Show(this.objDocPanel);
    }

    private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.pictureToolStripMenuItem.Checked = !this.pictureToolStripMenuItem.Checked;
      if (!this.pictureToolStripMenuItem.Checked)
      {
        this.bImageClosed = true;
        this.bPictureClosed = true;
        this.objFrmPicture.Hide();
      }
      else
      {
        this.bImageClosed = false;
        this.bPictureClosed = false;
        this.objFrmPicture.Show(this.objDocPanel);
      }
    }

    private void informationToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.informationToolStripMenuItem.Checked = !this.informationToolStripMenuItem.Checked;
      if (!this.informationToolStripMenuItem.Checked)
        this.objFrmInfo.Hide();
      else
        this.objFrmInfo.Show(this.objDocPanel);
    }

    private void selectionListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.selectionListToolStripMenuItem.Checked = !this.selectionListToolStripMenuItem.Checked;
      if (!this.selectionListToolStripMenuItem.Checked)
        this.objFrmSelectionlist.Hide();
      else
        this.objFrmSelectionlist.Show(this.objDocPanel);
    }

    private void partslistToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.partslistToolStripMenuItem.Checked = !this.partslistToolStripMenuItem.Checked;
      if (!this.partslistToolStripMenuItem.Checked)
      {
        this.objFrmPartlist.Hide();
        this.bPartsListClosed = true;
      }
      else
      {
        this.objFrmPartlist.Show(this.objDocPanel);
        this.bPartsListClosed = false;
      }
    }

    private void openToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmOpenBook frmOpenBook = new frmOpenBook(this);
      frmOpenBook.Owner = (Form) this;
      this.ShowDimmer();
      frmOpenBook.Show();
    }

    private void printToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbPrint_Click((object) null, (EventArgs) null);
    }

    private void aboutGSPcLocalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        frmAbout frmAbout = new frmAbout(this);
        frmAbout.Owner = (Form) this;
        this.ShowDimmer();
        frmAbout.Show();
      }
      catch
      {
        this.HideDimmer();
      }
    }

    private void gSPcLocalHelpToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        new GSPcLocalHelp(this).OpenHelpFile();
      }
      catch
      {
      }
    }

    public void OpenPrintDialogue(int iPrintType)
    {
      GSPcLocalViewer.frmPrint.frmPrint frmPrint = new GSPcLocalViewer.frmPrint.frmPrint(this, iPrintType);
      this.Enabled = false;
      frmPrint.Owner = (Form) this;
      frmPrint.Show();
    }

    private void memoRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmMemoRecovery frmMemoRecovery = new frmMemoRecovery(this);
      frmMemoRecovery.Owner = (Form) this;
      this.ShowDimmer();
      frmMemoRecovery.Show();
    }

    private void fontAndColorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Viewer_Font);
    }

    private void partsListInfoFontAndColorToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Viewer_Color);
    }

    private void generalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Viewer_General);
    }

    private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConnection frmConnection = new frmConnection(this);
      frmConnection.Owner = (Form) this;
      this.ShowDimmer();
      frmConnection.Show();
    }

    private void memosToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Memo_Settings);
    }

    private void pageNameSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Search_PageName);
    }

    private void textSearceNameSearchSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Search_Text);
    }

    private void partNameSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Search_PartName);
    }

    private void partNumberSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Search_PartNumber);
    }

    private void advanceSearchSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show(ConfigTasks.Search_Advance);
    }

    private void goToPortalToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbPortal_Click((object) null, (EventArgs) null);
    }

    private void viewMemoListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ShowAllMemos();
    }

    private void firstPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbNavigateFirst_Click((object) null, (EventArgs) null);
    }

    private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbNavigatePrevious_Click((object) null, (EventArgs) null);
    }

    private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbNavigateNext_Click((object) null, (EventArgs) null);
    }

    private void lastPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbNavigateLast_Click((object) null, (EventArgs) null);
    }

    private void previousViewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbHistoryBack_Click((object) null, (EventArgs) null);
    }

    private void nextViewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsbHistoryForward_Click((object) null, (EventArgs) null);
    }

    private void addPictureMemoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ShowPictureMemos(false);
    }

    private void addPartMemoToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.objFrmPartlist.ShowPartMemos();
    }

    private void memoDetailsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Settings.Default.EnableLocalMemo || Settings.Default.EnableAdminMemo || Settings.Default.EnableGlobalMemo)
        this.viewMemoListToolStripMenuItem.Visible = true;
      else
        this.viewMemoListToolStripMenuItem.Visible = false;
    }

    private void printPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.OpenPrintDialogue(0);
    }

    private void printPictureToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.OpenPrintDialogue(1);
    }

    private void printListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.OpenPrintDialogue(2);
    }

    private void printSelectionListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.OpenPrintDialogue(3);
    }

    private void partsListSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        frmConfig frmConfig = new frmConfig(this);
        frmConfig.Owner = (Form) this;
        this.ShowDimmer();
        frmConfig.Show(ConfigTasks.PartListSettings);
      }
      catch (Exception ex)
      {
      }
    }

    private void selectionListSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        frmConfig frmConfig = new frmConfig(this);
        frmConfig.Owner = (Form) this;
        this.ShowDimmer();
        frmConfig.Show(ConfigTasks.SelectionListSettings);
      }
      catch (Exception ex)
      {
      }
    }

    private void tsbOpenBook_Click(object sender, EventArgs e)
    {
      this.openToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbAddBookmarks_Click(object sender, EventArgs e)
    {
      this.AddBookmarksToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbConfiguration_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this);
      frmConfig.Owner = (Form) this;
      this.ShowDimmer();
      frmConfig.Show();
    }

    private void tsbThirdPartyBasket_Click(object sender, EventArgs e)
    {
      this.objFrmSelectionlist.tsBtnThirdPartyBasket_Click((object) null, (EventArgs) null);
    }

    private void tsbViewContents_Click(object sender, EventArgs e)
    {
      this.contentsToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbViewPicture_Click(object sender, EventArgs e)
    {
      this.pictureToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbViewPartslist_Click(object sender, EventArgs e)
    {
      this.partslistToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbViewInfo_Click(object sender, EventArgs e)
    {
      this.informationToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbSearchPageName_Click(object sender, EventArgs e)
    {
      this.pageNameToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbSearchPartNumber_Click(object sender, EventArgs e)
    {
      this.partNumberToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbSearchPartAdvance_Click(object sender, EventArgs e)
    {
      this.advancedSearchToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbAbout_Click(object sender, EventArgs e)
    {
      this.aboutGSPcLocalToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbHelp_Click(object sender, EventArgs e)
    {
      this.gSPcLocalHelpToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbPrint_Click(object sender, EventArgs e)
    {
      this.OpenPrintDialogue(-1);
    }

    private void tsbSearchPartName_Click(object sender, EventArgs e)
    {
      this.partNameToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbSearchText_Click(object sender, EventArgs e)
    {
      this.textSearchToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbPicPrev_Click(object sender, EventArgs e)
    {
      if (this.objFrmPicture.IsDisposed)
        return;
      this.objFrmPicture.tsBtnPrev_Click((object) null, (EventArgs) null);
    }

    private void tsbPicNext_Click(object sender, EventArgs e)
    {
      if (this.objFrmPicture.IsDisposed)
        return;
      this.objFrmPicture.tsBtnNext_Click((object) null, (EventArgs) null);
    }

    private void tsbPicZoomSelect_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.SelectZoom();
    }

    private void tsbPicZoomIn_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.ZoomIn();
    }

    private void tsbPicZoomOut_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.ZoomOut();
    }

    private void tsbPicCopy_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.CopyImage();
    }

    private void tsbPicPanMode_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.SetPanMode();
    }

    private void tsbAddPictureMemo_Click(object sender, EventArgs e)
    {
      this.ShowPictureMemos(false);
    }

    private void tsbFitPage_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.FitPage();
    }

    private void tsbPortal_Click(object sender, EventArgs e)
    {
      string sUrl = Program.iniServers[this.p_ServerId].items["SETTINGS", "PORTAL_PATH"];
      if (sUrl != null && sUrl != string.Empty)
      {
        this.OpenSpecifiedBrowser(sUrl);
      }
      else
      {
        int num = (int) MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void tsbDataCleanup_Click(object sender, EventArgs e)
    {
      this.manageDiskSpaceToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbMemoRecovery_Click(object sender, EventArgs e)
    {
      this.memoRecoveryToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbRestoreDefaults_Click(object sender, EventArgs e)
    {
      this.restoreDefaultsToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbSingleBookDownload_Click(object sender, EventArgs e)
    {
      this.singleBookToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsbMultipleBooksDownload_Click(object sender, EventArgs e)
    {
      this.multipleBooksToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      this.connectionToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void tsBRotateRight_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.tsBRotateRight_Click((object) null, (EventArgs) null);
    }

    private void tsBRotateLeft_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.tsBRotateLeft_Click((object) null, (EventArgs) null);
    }

    private void tsbPicSelectText_Click(object sender, EventArgs e)
    {
      this.objFrmPicture.SelectText();
    }

    private void tsHistory_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
    }

    public string AppCurrentLanguage
    {
      get
      {
        return this.CurrentLanguage;
      }
    }

    public XmlNode BookNode
    {
      get
      {
        return this.p_BookNode;
      }
    }

    public XmlNode SchemaNode
    {
      get
      {
        return this.p_SchemaNode;
      }
    }

    public int ServerId
    {
      get
      {
        return this.p_ServerId;
      }
    }

    public string BookPublishingId
    {
      get
      {
        return this.p_BookId;
      }
    }

    public string TssString
    {
      get
      {
        try
        {
          if (this.p_ArgsS == null)
            return string.Empty;
          foreach (string str in this.p_ArgsS)
          {
            if (str.ToUpper().Trim().StartsWith("TSS="))
              return str.ToUpper().Replace("TSS=", string.Empty);
          }
          return string.Empty;
        }
        catch
        {
          return string.Empty;
        }
      }
    }

    public DataGridView PartListGridView
    {
      get
      {
        return this.objFrmPartlist.Partlist;
      }
    }

    public string PicturePath
    {
      get
      {
        return this.objFrmPicture.PicturePath;
      }
    }

    public XmlNode PageSchemaNode
    {
      get
      {
        return this.objFrmTreeview.PageSchemaNode;
      }
    }

    public XmlNode PageNode
    {
      get
      {
        return this.objFrmTreeview.PageNode;
      }
    }

    public bool PartslistExists
    {
      get
      {
        return this.partslistToolStripMenuItem.Enabled;
      }
    }

    public string BookType
    {
      get
      {
        return this.p_BookType;
      }
    }

    public bool CompressedDownload
    {
      get
      {
        return this.p_Compressed;
      }
      set
      {
        this.p_Compressed = value;
      }
    }

    public string LocalSearchSettingsPath
    {
      get
      {
        string empty = string.Empty;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.ServerId].sIniKey;
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        return path + "\\SearchSettings.xml";
      }
    }

    public string ServerSearchSettingsFile
    {
      get
      {
        string empty = string.Empty;
        string str = Program.iniServers[this.ServerId].items["SETTINGS", "CONTENT_PATH"];
        if (!str.EndsWith("/"))
          str += "/";
        return str + "SearchSettings.xml";
      }
    }

    public void LoadDataDirect()
    {
      try
      {
        this.SendToBack();
        this.BringToFront();
        if (Program.iniKeys[(object) this.p_ArgsO[0].ToUpper()] == null)
          throw new Exception();
        int iniKey = (int) Program.iniKeys[(object) this.p_ArgsO[0].ToUpper()];
        this.p_ServerId = iniKey;
        if (Program.iniKeys.Count > 0)
        {
          if (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
            this.p_Encrypted = true;
          if (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
            this.p_Compressed = true;
          if (this.p_ArgsO != null)
          {
            if (this.p_ArgsO.Length >= 2)
            {
              string empty1 = string.Empty;
              string empty2 = string.Empty;
              if (Program.iniKeys.ContainsKey((object) this.p_ArgsO[0].ToUpper()))
              {
                if (this.objFrmTreeview.DockPanel == null && this.objFrmPicture.DockPanel == null && (this.objFrmSelectionlist.DockPanel == null && this.objFrmPartlist.DockPanel == null) && this.objFrmInfo.DockPanel == null)
                  this.ShowForms();
                this.objFrmTreeview.ShowLoading();
                this.objFrmPicture.ShowLoading();
                this.objFrmInfo.ShowLoading();
                if (this.objFrmPartlist != null)
                  this.HidePartsList();
                try
                {
                  string str1 = Program.iniServers[iniKey].items["SETTINGS", "CONTENT_PATH"];
                  if (!str1.EndsWith("/"))
                    str1 += "/";
                  string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[iniKey].sIniKey;
                  if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                  string str2;
                  string str3;
                  if (this.p_Compressed)
                  {
                    str2 = str1 + "Series.zip";
                    str3 = path + "\\Series.zip";
                  }
                  else
                  {
                    str2 = str1 + "Series.xml";
                    str3 = path + "\\Series.xml";
                  }
                  Program.bNoViewerOpen = false;
                  this.bgWorker.RunWorkerAsync((object) new object[2]
                  {
                    (object) str2,
                    (object) str3
                  });
                }
                catch
                {
                  MessageHandler.ShowError(this.GetResource("(E-VWR-EM001) Failed to create file/folder specified", "(E-VWR-EM001)_FILE/FOLDER", ResourceType.POPUP_MESSAGE));
                }
              }
              else
                MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
            }
            else
              MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
          }
          else
            MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
        }
        else
          MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
      }
      catch
      {
        this.p_ArgsO = (string[]) null;
        MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      object[] objArray = (object[]) e.Argument;
      string surl1_1 = (string) objArray[0];
      string str1 = (string) objArray[1];
      this.UpdateStatus(this.GetResource("Downloading...", "DOWNLOADING", ResourceType.STATUS_MESSAGE));
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      int result = 0;
      bool flag = false;
      string surl1_2 = ((string) objArray[0]).Remove(surl1_1.LastIndexOf("/") + 1) + "DataUpdate.xml";
      string str2 = ((string) objArray[1]).Remove(str1.LastIndexOf("\\")) + "\\DataUpdate.xml";
      if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
        result = 0;
      this.objDownloader.DownloadFile(surl1_2, str2);
      if (File.Exists(str1))
      {
        if (result == 0)
          flag = true;
        else if (result < 1000)
        {
          DateTime dtServer = Global.DataUpdateDate(str2);
          if (Global.IntervalElapsed(Global.GetLocalDateOfFile(str1, this.p_ServerId), dtServer, result))
            flag = true;
        }
      }
      else
        flag = true;
      if (flag && this.objDownloader.DownloadFile(surl1_1, str1) && !this.IsDisposed)
      {
        this.UpdateStatus(this.GetResource("Searching Series.xml", "SEARCING_SERIES_XML", ResourceType.STATUS_MESSAGE));
        switch (this.SearchSeries(str1, this.p_ArgsO[1]))
        {
          case 0:
            e.Result = (object) "Command does not return any results";
            break;
          case 1:
            e.Result = (object) "ok";
            break;
          case 2:
            e.Result = (object) this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_NOINFO", ResourceType.POPUP_MESSAGE);
            break;
        }
      }
      if (File.Exists(str1))
      {
        if (this.IsDisposed)
          return;
        this.UpdateStatus(this.GetResource("Searching Series.xml", "SEARCING_SERIES_XML", ResourceType.STATUS_MESSAGE));
        switch (this.SearchSeries(str1, this.p_ArgsO[1]))
        {
          case 0:
            e.Result = (object) "Command does not return any results";
            break;
          case 1:
            e.Result = (object) "ok";
            break;
          case 2:
            e.Result = (object) this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_NOINFO", ResourceType.POPUP_MESSAGE);
            break;
        }
      }
      else
      {
        if (this.IsDisposed)
          return;
        this.UpdateStatus(this.GetResource("(E-VWR-EM004) Failed to download specified object", "(E-VWR-EM004)_OBJECT", ResourceType.STATUS_MESSAGE));
        e.Result = (object) this.GetResource("(E-VWR-EM004) Failed to download specified object", "(E-VWR-EM004)_OBJECT", ResourceType.POPUP_MESSAGE);
        if (this.p_ArgsO == null)
          return;
        this.p_ArgsO = (string[]) null;
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      string result = (string) e.Result;
      if (this.IsDisposed)
        return;
      if (result.Equals("ok"))
      {
        this.objFrmTreeview.LoadBook();
      }
      else
      {
        if (!this.objFrmTreeview.IsDisposed)
          this.objFrmTreeview.Dispose();
        if (!this.objFrmPicture.IsDisposed)
          this.objFrmPicture.Dispose();
        if (!this.objFrmSelectionlist.IsDisposed)
          this.objFrmSelectionlist.Dispose();
        if (!this.objFrmPartlist.IsDisposed)
          this.objFrmPartlist.Dispose();
        if (!this.objFrmInfo.IsDisposed)
          this.objFrmInfo.Dispose();
        this.EnableMenuAndToolbar(false);
        if (!(result != "SecureBook"))
          return;
        MessageHandler.ShowInformation(result);
      }
    }

    private int SearchSeries(string sFilePath, string sBookPublishingId)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (!File.Exists(sFilePath))
        return 0;
      if (this.p_Compressed)
      {
        try
        {
          string str = sFilePath.ToLower().Replace(".zip", ".xml");
          Global.Unzip(sFilePath);
          if (File.Exists(str))
            xmlDocument.Load(str);
        }
        catch
        {
        }
      }
      else
      {
        try
        {
          xmlDocument.Load(sFilePath);
        }
        catch
        {
          return 0;
        }
      }
      if (this.p_Encrypted)
      {
        try
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        catch
        {
          return 0;
        }
      }
      XmlNode xSchemaNode = xmlDocument.SelectSingleNode("//Schema");
      if (xSchemaNode == null)
        return 0;
      string str1 = string.Empty;
      string index1 = string.Empty;
      string index2 = string.Empty;
      string empty = string.Empty;
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          str1 = attribute.Name;
        else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
          index1 = attribute.Name;
        else if (attribute.Value.ToUpper().Equals("BOOKTYPE"))
          index2 = attribute.Name;
        else if (attribute.Value.ToUpper().Equals("COVERPICTURE"))
        {
          string name = attribute.Name;
        }
      }
      if (str1 == "" || index1 == "")
        return 0;
      this.p_BookId = sBookPublishingId;
      this.p_SchemaNode = xSchemaNode;
      XmlNodeList xNodeList = xmlDocument.SelectNodes("//Books/Book[translate(@" + index1 + ", 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + sBookPublishingId.ToUpper() + "']");
      if (xNodeList.Count > 0)
      {
        foreach (XmlNode filterBooks in this.FilterBooksList(xSchemaNode, xNodeList))
        {
          string str2 = filterBooks.Attributes[index1].Value;
          if (filterBooks.Attributes.Count > 0)
          {
            this.p_BookNode = filterBooks;
            try
            {
              if (filterBooks.Attributes[index2] != null)
              {
                this.p_BookType = filterBooks.Attributes[index2].Value;
              }
              else
              {
                this.UpdateStatus(this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_NOINFO", ResourceType.POPUP_MESSAGE));
                return 2;
              }
            }
            catch
            {
            }
            this.ShowHideSelectionList();
            return 1;
          }
        }
        this.UpdateStatus(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.STATUS_MESSAGE));
        return 0;
      }
      this.UpdateStatus(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.STATUS_MESSAGE));
      return 0;
    }

    private void AddBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      try
      {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        string str = path + "\\BookMarks.xml";
        XmlDocument xmlDocument = new XmlDocument();
        XmlNode node = xmlDocument.CreateNode(XmlNodeType.Element, "BookMark", (string) null);
        XmlAttribute attribute1 = xmlDocument.CreateAttribute("ServerKey");
        attribute1.Value = Program.iniServers[this.p_ServerId].sIniKey;
        node.Attributes.Append(attribute1);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("BookId");
        attribute2.Value = this.p_BookId;
        node.Attributes.Append(attribute2);
        XmlAttribute attribute3 = xmlDocument.CreateAttribute("PageId");
        attribute3.Value = this.objFrmPicture.CurrentPageId;
        node.Attributes.Append(attribute3);
        XmlAttribute attribute4 = xmlDocument.CreateAttribute("PageName");
        attribute4.Value = this.objFrmPicture.CurrentPageName;
        node.Attributes.Append(attribute4);
        XmlAttribute attribute5 = xmlDocument.CreateAttribute("PicIndex");
        attribute5.Value = this.objFrmPartlist.CurrentPicIndex;
        node.Attributes.Append(attribute5);
        XmlAttribute attribute6 = xmlDocument.CreateAttribute("ListIndex");
        attribute6.Value = this.objFrmPartlist.CurrentListIndex;
        node.Attributes.Append(attribute6);
        XmlAttribute attribute7 = xmlDocument.CreateAttribute("PartNo");
        attribute7.Value = this.objFrmPartlist.CurrentPartNumber;
        node.Attributes.Append(attribute7);
        if (File.Exists(str))
        {
          xmlDocument.Load(str);
        }
        else
        {
          string xml = "<?xml version='1.0' encoding='utf-8'?><BookMarks></BookMarks>";
          xmlDocument.LoadXml(xml);
        }
        string xpath = "//BookMark";
        foreach (XmlAttribute attribute8 in (XmlNamedNodeMap) node.Attributes)
        {
          xpath = xpath + "[@" + attribute8.Name + "='" + attribute8.Value + "']";
          if (attribute8.Value.Equals(string.Empty) && attribute8.Name != "PartNo")
          {
            xmlDocument = (XmlDocument) null;
            MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM003) Invalid command", "(E-VWR-EM003)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
            return;
          }
        }
        CustomToolStripMenuItem toolStripMenuItem = new CustomToolStripMenuItem(node.Attributes["PageName"].Value);
        toolStripMenuItem.Name = node.Attributes["BookId"].Value + " [" + node.Attributes["PageId"].Value + "] [" + node.Attributes["PicIndex"].Value + "][" + node.Attributes["ListIndex"].Value + "][" + node.Attributes["PartNo"].Value + "]";
        toolStripMenuItem.Tag = (object) node.OuterXml;
        toolStripMenuItem.ToolTipText = this.GetResource("Picture Index", "PICTURE_INDEX", ResourceType.TOOLSTRIP) + " = " + node.Attributes["PicIndex"].Value + "\n" + this.GetResource("List Index", "LIST_INDEX", ResourceType.TOOLSTRIP) + " = " + node.Attributes["ListIndex"].Value;
        if (node.Attributes["PartNo"].Value != string.Empty)
          toolStripMenuItem.ToolTipText = toolStripMenuItem.ToolTipText + "\n" + this.GetResource("Part Number", "PART_NUMBER", ResourceType.TOOLSTRIP) + " = " + node.Attributes["PartNo"].Value;
        XmlNode oldChild = xmlDocument.SelectSingleNode(xpath);
        if (oldChild != null)
        {
          oldChild.ParentNode.RemoveChild(oldChild);
          this.bookmarksToolStripMenuItem.DropDown.Items.RemoveByKey(toolStripMenuItem.Name);
        }
        if (xmlDocument.ChildNodes.Count == 2)
        {
          xmlDocument.ChildNodes[1].AppendChild(node);
          xmlDocument.Save(str);
          this.bookmarksToolStripMenuItem.DropDown.Items.Add((ToolStripItem) toolStripMenuItem);
          toolStripMenuItem.OnOpen += new CustomToolStripMenuItem.ClickHandler(this.OpenBookmarkPage);
          toolStripMenuItem.OnDelete += new CustomToolStripMenuItem.ClickHandler(this.DeleteBookmarkPage);
        }
        this.toolStripSeparator.Visible = true;
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM005) Failed to save specified object", "(E-VWR-EM005)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
      }
    }

    public void LoadBookmarks()
    {
      string empty = string.Empty;
      try
      {
        string str = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.p_ServerId].sIniKey + "\\BookMarks.xml";
        if (File.Exists(str))
        {
          XmlDocument xmlDocument = new XmlDocument();
          xmlDocument.Load(str);
          XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//BookMarks/BookMark[translate(@BookId, 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + this.p_BookId.ToUpper() + "']");
          if (xmlNodeList != null)
          {
            for (int index = 0; index < xmlNodeList.Count; ++index)
            {
              CustomToolStripMenuItem toolStripMenuItem = new CustomToolStripMenuItem(xmlNodeList[index].Attributes["PageName"].Value);
              toolStripMenuItem.Name = xmlNodeList[index].Attributes["BookId"].Value + " [" + xmlNodeList[index].Attributes["PageId"].Value + "] [" + xmlNodeList[index].Attributes["PicIndex"].Value + "][" + xmlNodeList[index].Attributes["ListIndex"].Value + "][" + xmlNodeList[index].Attributes["PartNo"].Value + "]";
              toolStripMenuItem.Tag = (object) xmlNodeList[index].OuterXml;
              toolStripMenuItem.ToolTipText = this.GetResource("Picture Index", "PICTURE_INDEX", ResourceType.TOOLSTRIP) + " = " + xmlNodeList[index].Attributes["PicIndex"].Value + "\n" + this.GetResource("List Index", "LIST_INDEX", ResourceType.TOOLSTRIP) + " = " + xmlNodeList[index].Attributes["ListIndex"].Value;
              if (xmlNodeList[index].Attributes["PartNo"].Value != string.Empty)
                toolStripMenuItem.ToolTipText = toolStripMenuItem.ToolTipText + "\n" + this.GetResource("Part Number", "PART_NUMBER", ResourceType.TOOLSTRIP) + " = " + xmlNodeList[index].Attributes["PartNo"].Value;
              toolStripMenuItem.OnOpen += new CustomToolStripMenuItem.ClickHandler(this.OpenBookmarkPage);
              toolStripMenuItem.OnDelete += new CustomToolStripMenuItem.ClickHandler(this.DeleteBookmarkPage);
              this.bookmarksToolStripMenuItem.DropDown.Items.Add((ToolStripItem) toolStripMenuItem);
            }
          }
        }
        if (this.bookmarksToolStripMenuItem.DropDown.Items.Count != 2)
          return;
        this.toolStripSeparator.Visible = false;
      }
      catch
      {
      }
    }

    public void ClearBookmarks()
    {
      try
      {
        for (int index = this.bookmarksToolStripMenuItem.DropDown.Items.Count - 1; index >= 0; --index)
        {
          if (this.bookmarksToolStripMenuItem.DropDown.Items[index].GetType().ToString() == typeof (CustomToolStripMenuItem).ToString())
            this.bookmarksToolStripMenuItem.DropDown.Items.RemoveAt(index);
        }
      }
      catch
      {
      }
    }

    private void OpenBookmarkPage(CustomToolStripMenuItem sender)
    {
      XmlNode xmlNode = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(sender.Tag.ToString())));
      if (Settings.Default.OpenInCurrentInstance)
      {
        this.p_ArgsO = new string[6];
        this.p_ArgsO[0] = xmlNode.Attributes["ServerKey"].Value;
        this.p_ArgsO[1] = xmlNode.Attributes["BookId"].Value;
        this.p_ArgsO[2] = xmlNode.Attributes["PageId"].Value;
        this.p_ArgsO[3] = xmlNode.Attributes["PicIndex"].Value;
        this.p_ArgsO[4] = xmlNode.Attributes["ListIndex"].Value;
        this.p_ArgsO[5] = xmlNode.Attributes["PartNo"].Value;
        this.objFrmTreeview.TreeViewClearSelection();
        this.SelectTreeNode();
      }
      else
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) "-o");
        arrayList.Add((object) xmlNode.Attributes["ServerKey"].Value);
        arrayList.Add((object) xmlNode.Attributes["BookId"].Value);
        arrayList.Add((object) xmlNode.Attributes["PageId"].Value);
        arrayList.Add((object) xmlNode.Attributes["PicIndex"].Value);
        arrayList.Add((object) xmlNode.Attributes["ListIndex"].Value);
        arrayList.Add((object) xmlNode.Attributes["PartNo"].Value);
        if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        {
          arrayList.Add((object) "-f");
          foreach (string str in this.p_ArgsF)
            arrayList.Add((object) str);
        }
        if (this.p_ArgsS != null && this.p_ArgsS.Length > 0)
        {
          arrayList.Add((object) "-s");
          foreach (string str in this.p_ArgsS)
            arrayList.Add((object) str);
        }
        this.frmParent.NextTime((string[]) arrayList.ToArray(typeof (string)));
      }
    }

    public void OpenBookmarkPage(XmlNode xNode)
    {
      if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
      {
        this.p_ArgsO = new string[6];
        this.p_ArgsO[0] = xNode.Attributes["ServerKey"].Value;
        this.p_ArgsO[1] = xNode.Attributes["BookId"].Value;
        this.p_ArgsO[2] = xNode.Attributes["PageId"].Value;
        this.p_ArgsO[3] = xNode.Attributes["PicIndex"].Value;
        this.p_ArgsO[4] = xNode.Attributes["ListIndex"].Value;
        this.p_ArgsO[5] = xNode.Attributes["PartNo"].Value;
        this.LoadDataDirect();
      }
      else
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) "-o");
        arrayList.Add((object) xNode.Attributes["ServerKey"].Value);
        arrayList.Add((object) xNode.Attributes["BookId"].Value);
        arrayList.Add((object) xNode.Attributes["PageId"].Value);
        arrayList.Add((object) xNode.Attributes["PicIndex"].Value);
        arrayList.Add((object) xNode.Attributes["ListIndex"].Value);
        arrayList.Add((object) xNode.Attributes["PartNo"].Value);
        if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        {
          arrayList.Add((object) "-f");
          foreach (string str in this.p_ArgsF)
            arrayList.Add((object) str);
        }
        if (this.p_ArgsS != null && this.p_ArgsS.Length > 0)
        {
          arrayList.Add((object) "-s");
          foreach (string str in this.p_ArgsS)
            arrayList.Add((object) str);
        }
        this.frmParent.NextTime((string[]) arrayList.ToArray(typeof (string)));
      }
    }

    private void DeleteBookmarkPage(CustomToolStripMenuItem sender)
    {
      string empty = string.Empty;
      try
      {
        string str = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.p_ServerId].sIniKey + "\\BookMarks.xml";
        XmlDocument xmlDocument = new XmlDocument();
        XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(sender.Tag.ToString()));
        XmlNode xmlNode = xmlDocument.ReadNode((XmlReader) xmlTextReader);
        if (!File.Exists(str))
          return;
        xmlDocument.Load(str);
        string xpath = "//BookMark";
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode.Attributes)
          xpath = xpath + "[@" + attribute.Name + "='" + attribute.Value + "']";
        XmlNode oldChild = xmlDocument.SelectSingleNode(xpath);
        if (oldChild != null && MessageBox.Show(this.GetResource("Are you sure you want to delete this bookmark?", "DELETE_BOOKMARK?", ResourceType.POPUP_MESSAGE), this.Text.Trim(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          oldChild.ParentNode.RemoveChild(oldChild);
          this.bookmarksToolStripMenuItem.DropDown.Items.RemoveByKey(sender.Name);
          if (this.bookmarksToolStripMenuItem.DropDown.Items.Count < 3)
            this.toolStripSeparator.Visible = false;
        }
        if (xmlDocument.ChildNodes.Count != 2)
          return;
        xmlDocument.Save(str);
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM006) Failed to delete specified object", "(E-VWR-EM006)_FAILED_DELETE", ResourceType.POPUP_MESSAGE));
      }
    }

    public void ResetHistory()
    {
      this.objHistory.ResetHistory();
      this.AddToHistory();
    }

    public void AddToHistory()
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        XmlNode node = xmlDocument.CreateNode(XmlNodeType.Element, "History", (string) null);
        XmlAttribute attribute1 = xmlDocument.CreateAttribute("ServerKey");
        attribute1.Value = Program.iniServers[this.p_ServerId].sIniKey;
        node.Attributes.Append(attribute1);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("BookId");
        attribute2.Value = this.p_BookId;
        node.Attributes.Append(attribute2);
        XmlAttribute attribute3 = xmlDocument.CreateAttribute("PageName");
        attribute3.Value = this.objFrmPicture.CurrentPageName;
        node.Attributes.Append(attribute3);
        XmlAttribute attribute4 = xmlDocument.CreateAttribute("PageId");
        attribute4.Value = this.objFrmPicture.CurrentPageId;
        node.Attributes.Append(attribute4);
        XmlAttribute attribute5 = xmlDocument.CreateAttribute("PicIndex");
        attribute5.Value = this.objFrmPartlist.CurrentPicIndex;
        node.Attributes.Append(attribute5);
        XmlAttribute attribute6 = xmlDocument.CreateAttribute("ListIndex");
        attribute6.Value = this.objFrmPartlist.CurrentListIndex;
        node.Attributes.Append(attribute6);
        XmlAttribute attribute7 = xmlDocument.CreateAttribute("PartNo");
        attribute7.Value = this.objFrmPartlist.CurrentPartNumber;
        node.Attributes.Append(attribute7);
        if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        {
          int index;
          for (index = 0; index < this.p_ArgsF.Length; ++index)
          {
            XmlAttribute attribute8 = xmlDocument.CreateAttribute(index.ToString());
            attribute8.Value = this.p_ArgsF[index];
            node.Attributes.Append(attribute8);
          }
          XmlAttribute attribute9 = xmlDocument.CreateAttribute("Filters");
          attribute9.Value = index.ToString();
          node.Attributes.Append(attribute9);
        }
        this.objHistory.Add(node);
        this.tsbHistoryList.DropDownItems.Clear();
        Hashtable getHistoryList = this.objHistory.GetHistoryList;
        for (int index = 0; index < getHistoryList.Count; ++index)
        {
          ToolStripItem toolStripItem1 = (ToolStripItem) new ToolStripMenuItem(((XmlNode) getHistoryList[(object) index]).Attributes["BookId"].Value + "-" + ((XmlNode) getHistoryList[(object) index]).Attributes["PageName"].Value);
          toolStripItem1.Tag = (object) index;
          toolStripItem1.Click += new EventHandler(this.HistoryToolStripItem_Click);
          bool flag = false;
          foreach (ToolStripItem toolStripItem2 in (ArrangedElementCollection) this.tsbHistoryList.DropDown.Items)
          {
            if (toolStripItem2.Text == toolStripItem1.Text)
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            this.tsbHistoryList.DropDown.Items.Add(toolStripItem1);
        }
        this.tsbHistoryBack.Enabled = this.objHistory.BackEnable;
        this.previousViewToolStripMenuItem.Enabled = this.objHistory.BackEnable;
        this.tsbHistoryForward.Enabled = this.objHistory.ForwardEnable;
        this.nextViewToolStripMenuItem.Enabled = this.objHistory.ForwardEnable;
        this.tsbHistoryList.Enabled = this.objHistory.ListEnable;
      }
      catch
      {
      }
    }

    public void LoadDataFromNode(XmlNode xNode)
    {
      if (this.tsHistory.InvokeRequired)
        this.tsHistory.Invoke((Delegate) new frmViewer.LoadDataFromNodeDelegate(this.LoadDataFromNode), (object) xNode);
      else if (xNode != null && xNode != null)
      {
        this.tsbHistoryBack.Enabled = false;
        this.tsbHistoryForward.Enabled = false;
        this.tsbHistoryList.Enabled = false;
        if (xNode == null || xNode == null)
          return;
        if (xNode.Attributes["Filters"] != null)
        {
          int result = 0;
          if (int.TryParse(xNode.Attributes["Filters"].Value, out result))
          {
            this.p_ArgsF = new string[result];
            for (int index = 0; index < result; ++index)
              this.p_ArgsF[index] = xNode.Attributes[index.ToString()].Value;
          }
        }
        else
          this.p_ArgsF = (string[]) null;
        this.p_ArgsO = new string[6];
        this.p_ArgsO[0] = xNode.Attributes["ServerKey"].Value;
        this.p_ArgsO[1] = xNode.Attributes["BookId"].Value;
        this.p_ArgsO[2] = xNode.Attributes["PageId"].Value;
        this.p_ArgsO[3] = xNode.Attributes["PicIndex"].Value;
        this.p_ArgsO[4] = xNode.Attributes["ListIndex"].Value;
        this.p_ArgsO[5] = xNode.Attributes["PartNo"].Value;
        if (this.p_ServerId == (int) Program.iniKeys[(object) xNode.Attributes["ServerKey"].Value] && this.p_BookId == xNode.Attributes["BookId"].Value)
        {
          this.objFrmTreeview.TreeViewClearSelection();
          this.SelectTreeNode();
        }
        else
          this.LoadDataDirect();
      }
      else
      {
        if (!this.objFrmTreeview.IsDisposed)
          this.objFrmTreeview.Dispose();
        if (!this.objFrmPicture.IsDisposed)
          this.objFrmPicture.Dispose();
        if (!this.objFrmSelectionlist.IsDisposed)
          this.objFrmSelectionlist.Dispose();
        if (!this.objFrmPartlist.IsDisposed)
          this.objFrmPartlist.Dispose();
        if (!this.objFrmInfo.IsDisposed)
          this.objFrmInfo.Dispose();
        this.EnableMenuAndToolbar(false);
      }
    }

    private void HistoryToolStripItem_Click(object sender, EventArgs e)
    {
      try
      {
        this.tsbHistoryBack.Enabled = false;
        this.tsbHistoryForward.Enabled = false;
        this.LoadDataFromNode(this.objHistory.Open(int.Parse(((ToolStripItem) sender).Tag.ToString())));
      }
      catch
      {
      }
    }

    private void tsbHistoryBack_Click(object sender, EventArgs e)
    {
      this.tsbHistoryBack.Enabled = false;
      this.tsbHistoryForward.Enabled = false;
      this.LoadDataFromNode(this.objHistory.Back());
    }

    private void tsbHistoryForward_Click(object sender, EventArgs e)
    {
      this.tsbHistoryBack.Enabled = false;
      this.tsbHistoryForward.Enabled = false;
      this.LoadDataFromNode(this.objHistory.Forward());
    }

    public XmlNodeList FilterBooksList(XmlNode xSchemaNode, XmlNodeList xNodeList)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        return this.FilterNodeList(xNodeList, arrMandatory);
      return xNodeList;
    }

    public XmlNode FilterPage(XmlNode xSchemaNode, XmlNode xNode)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("PAGENAME"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        return this.FilterNode(xNode, arrMandatory);
      return xNode;
    }

    public XmlNodeList FilterPicsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("UPDATEDATEPIC"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        return this.FilterNodeList(xNodeList, arrMandatory);
      return xNodeList;
    }

    public XmlNodeList FilterPartsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
    {
      ArrayList arrMandatory = new ArrayList();
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("LINKNUMBER"))
          arrMandatory.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("PARTNUMBER"))
          arrMandatory.Add((object) attribute.Name);
      }
      if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        return this.FilterNodeList(xNodeList, arrMandatory);
      return xNodeList;
    }

    private XmlNodeList FilterNodeList(XmlNodeList xNodeList, ArrayList arrMandatory)
    {
      foreach (XmlNode xNode in xNodeList)
      {
        if (xNode.HasChildNodes)
        {
          foreach (XmlNode childNode in xNode.ChildNodes)
          {
            if (childNode.Name.ToUpper() == "SEL")
            {
              if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
              {
                string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
                {
                  ","
                }, StringSplitOptions.RemoveEmptyEntries);
                bool flag = false;
                foreach (string str in strArray1)
                {
                  if (arrMandatory.Contains((object) str))
                  {
                    flag = true;
                    break;
                  }
                }
                foreach (string str in this.p_ArgsF)
                {
                  string[] separator = new string[1]{ "=" };
                  int num = 1;
                  string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                  if (strArray2.Length == 2)
                  {
                    foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                    {
                      if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueMatchesSelectFilter(strArray2[1], attribute.Value, false))
                      {
                        if (flag)
                        {
                          xNode.RemoveAll();
                          break;
                        }
                        foreach (string index in strArray1)
                        {
                          if (xNode.Attributes[index] != null)
                            xNode.Attributes.Remove(xNode.Attributes[index]);
                        }
                      }
                    }
                  }
                }
              }
            }
            else if (childNode.Name.ToUpper() == "SWT")
            {
              if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
              {
                string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
                {
                  ","
                }, StringSplitOptions.RemoveEmptyEntries);
                bool flag = false;
                foreach (string str in strArray1)
                {
                  if (arrMandatory.Contains((object) str))
                  {
                    flag = true;
                    break;
                  }
                }
                foreach (string str in this.p_ArgsF)
                {
                  string[] separator = new string[1]{ "=" };
                  int num = 1;
                  string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                  if (strArray2.Length == 2)
                  {
                    foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                    {
                      if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && strArray2[1].ToUpper() == "ON" && attribute.Value.ToUpper() == "ON")
                      {
                        if (flag)
                        {
                          xNode.RemoveAll();
                          break;
                        }
                        foreach (string index in strArray1)
                        {
                          if (xNode.Attributes[index] != null)
                            xNode.Attributes.Remove(xNode.Attributes[index]);
                        }
                      }
                    }
                  }
                }
              }
            }
            else if (childNode.Name.ToUpper() == "RNG" && childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
            {
              string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
              {
                ","
              }, StringSplitOptions.RemoveEmptyEntries);
              bool flag = false;
              foreach (string str in strArray1)
              {
                if (arrMandatory.Contains((object) str))
                {
                  flag = true;
                  break;
                }
              }
              foreach (string str in this.p_ArgsF)
              {
                string[] separator = new string[1]{ "=" };
                int num = 1;
                string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                if (strArray2.Length == 2)
                {
                  foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                  {
                    if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueInRangeFilter(attribute.Value, strArray2[1]))
                    {
                      if (flag)
                      {
                        xNode.RemoveAll();
                        break;
                      }
                      foreach (string index in strArray1)
                      {
                        if (xNode.Attributes[index] != null)
                          xNode.Attributes.Remove(xNode.Attributes[index]);
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      return xNodeList;
    }

    private XmlNode FilterNode(XmlNode xNode, ArrayList arrMandatory)
    {
      if (xNode.HasChildNodes)
      {
        foreach (XmlNode childNode in xNode.ChildNodes)
        {
          if (childNode.Name.ToUpper() == "SEL")
          {
            if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
            {
              string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
              {
                ","
              }, StringSplitOptions.RemoveEmptyEntries);
              bool flag = false;
              foreach (string str in strArray1)
              {
                if (arrMandatory.Contains((object) str))
                {
                  flag = true;
                  break;
                }
              }
              foreach (string str in this.p_ArgsF)
              {
                string[] separator = new string[1]{ "=" };
                int num = 1;
                string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                if (strArray2.Length == 2)
                {
                  foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                  {
                    if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueMatchesSelectFilter(strArray2[1], attribute.Value, false))
                    {
                      if (flag)
                      {
                        xNode.RemoveAll();
                        break;
                      }
                      foreach (string index in strArray1)
                      {
                        if (xNode.Attributes[index] != null)
                          xNode.Attributes.Remove(xNode.Attributes[index]);
                      }
                    }
                  }
                }
              }
            }
          }
          else if (childNode.Name.ToUpper() == "SWT")
          {
            if (childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
            {
              string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
              {
                ","
              }, StringSplitOptions.RemoveEmptyEntries);
              bool flag = false;
              foreach (string str in strArray1)
              {
                if (arrMandatory.Contains((object) str))
                {
                  flag = true;
                  break;
                }
              }
              foreach (string str in this.p_ArgsF)
              {
                string[] separator = new string[1]{ "=" };
                int num = 1;
                string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
                if (strArray2.Length == 2)
                {
                  foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                  {
                    if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && strArray2[1].ToUpper() == "ON" && attribute.Value.ToUpper() == "ON")
                    {
                      if (flag)
                      {
                        xNode.RemoveAll();
                        break;
                      }
                      foreach (string index in strArray1)
                      {
                        if (xNode.Attributes[index] != null)
                          xNode.Attributes.Remove(xNode.Attributes[index]);
                      }
                    }
                  }
                }
              }
            }
          }
          else if (childNode.Name.ToUpper() == "RNG" && childNode.Attributes.Count > 0 && childNode.Attributes[0].Name.ToUpper() == "TG")
          {
            string[] strArray1 = childNode.Attributes[0].Value.Split(new string[1]
            {
              ","
            }, StringSplitOptions.RemoveEmptyEntries);
            bool flag = false;
            foreach (string str in strArray1)
            {
              if (arrMandatory.Contains((object) str))
              {
                flag = true;
                break;
              }
            }
            foreach (string str in this.p_ArgsF)
            {
              string[] separator = new string[1]{ "=" };
              int num = 1;
              string[] strArray2 = str.Split(separator, (StringSplitOptions) num);
              if (strArray2.Length == 2)
              {
                foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
                {
                  if (attribute.Name.ToUpper() == strArray2[0].ToUpper() && !this.ValueInRangeFilter(attribute.Value, strArray2[1]))
                  {
                    if (flag)
                    {
                      xNode.RemoveAll();
                      break;
                    }
                    foreach (string index in strArray1)
                    {
                      if (xNode.Attributes[index] != null)
                        xNode.Attributes.Remove(xNode.Attributes[index]);
                    }
                  }
                }
              }
            }
          }
        }
      }
      return xNode;
    }

    private bool ValueMatchesSelectFilter(string values1, string values2, bool caseSensitive)
    {
      string[] strArray1 = values1.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      string[] strArray2 = values2.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      foreach (string str1 in strArray1)
      {
        foreach (string str2 in strArray2)
        {
          if (caseSensitive)
          {
            if (str1.Equals(str2))
              return true;
          }
          else if (str1.ToUpper().Equals(str2.ToUpper()))
            return true;
        }
      }
      return false;
    }

    private bool ValueInRangeFilter(string range, string value)
    {
      string[] strArray = range.Split(new string[1]{ "**" }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length != 2)
        return false;
      int result1;
      if (int.TryParse(strArray[0], out result1))
      {
        int result2;
        if (int.TryParse(strArray[1], out result2))
        {
          if (result1 > result2)
          {
            int num = result1;
            result1 = result2;
            result2 = num;
          }
          int result3;
          return int.TryParse(value, out result3) && result3 >= result1 && result3 <= result2;
        }
      }
      try
      {
        DateTime dateTime1 = DateTime.ParseExact(strArray[0], "dd/MM/yyyy", (IFormatProvider) null);
        DateTime dateTime2 = DateTime.ParseExact(strArray[1], "dd/MM/yyyy", (IFormatProvider) null);
        DateTime exact = DateTime.ParseExact(value, "dd/MM/yyyy", (IFormatProvider) null);
        if (dateTime1 > dateTime2)
        {
          DateTime dateTime3 = dateTime1;
          dateTime1 = dateTime2;
          dateTime2 = dateTime3;
        }
        return exact >= dateTime1 && exact <= dateTime2;
      }
      catch
      {
        return false;
      }
    }

    public void OpenSearch(string _ServerKey, string _BookPublishingId, string _PageId, string _ImageIndex, string _ListIndex, string _PartNumber)
    {
      this.p_ArgsO = new string[6];
      this.p_ArgsO[0] = _ServerKey;
      this.p_ArgsO[1] = _BookPublishingId;
      this.p_ArgsO[2] = _PageId;
      this.p_ArgsO[3] = _ImageIndex;
      this.p_ArgsO[4] = _ListIndex;
      this.p_ArgsO[5] = _PartNumber;
      if (this.p_ServerId == (int) Program.iniKeys[(object) _ServerKey] && this.p_BookId == _BookPublishingId)
      {
        this.objFrmTreeview.TreeViewClearSelection();
        this.SelectTreeNode();
      }
      else
        this.LoadDataDirect();
    }

    public void OpenTextSearch(string _PageName, string _picIndex, string _Text)
    {
      if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
      {
        this.objFrmPicture.HighLightText = Program.HighLightText;
        this.objFrmPicture.DjVuPageNumber = Program.DjVuPageNumber;
        this.p_ArgsO = new string[6];
        this.p_ArgsO[0] = this.ServerId.ToString();
        this.p_ArgsO[1] = this.BookPublishingId;
        this.p_ArgsO[2] = "1";
        this.p_ArgsO[3] = _picIndex;
        this.p_ArgsO[4] = "1";
        this.p_ArgsO[5] = string.Empty;
        this.objFrmTreeview.TreeViewClearSelection();
        this.SelectTreeNodeByName(_PageName);
        this.objFrmPicture.HighLightText = _Text;
      }
      else
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) "-o");
        arrayList.Add((object) Program.iniServers[this.ServerId].sIniKey);
        arrayList.Add((object) this.BookPublishingId);
        XmlDocument xmlDocument = new XmlDocument();
        TreeNode treeNodeByPageName = this.objFrmTreeview.FindTreeNodeByPageName(this.objFrmTreeview.tvBook.Nodes, _PageName);
        if (treeNodeByPageName != null)
        {
          XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(this.objFrmTreeview.tvBook.Tag.ToString()));
          xmlDocument.ReadNode((XmlReader) xmlTextReader1);
          XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(treeNodeByPageName.Tag.ToString()));
          xmlDocument.ReadNode((XmlReader) xmlTextReader2);
          arrayList.Add((object) treeNodeByPageName.Text);
        }
        else
          arrayList.Add((object) "1");
        arrayList.Add((object) _picIndex);
        arrayList.Add((object) "1");
        arrayList.Add((object) string.Empty);
        if (this.p_ArgsF != null && this.p_ArgsF.Length > 0)
        {
          arrayList.Add((object) "-f");
          foreach (string str in this.p_ArgsF)
            arrayList.Add((object) str);
        }
        if (this.p_ArgsS != null && this.p_ArgsS.Length > 0)
        {
          arrayList.Add((object) "-s");
          foreach (string str in this.p_ArgsS)
            arrayList.Add((object) str);
        }
        this.frmParent.NextTime((string[]) arrayList.ToArray(typeof (string)));
      }
    }

    private void pageNameToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmSearch frmSearch = new frmSearch(this);
      frmSearch.Owner = (Form) this;
      frmSearch.TopMost = true;
      this.ShowDimmer();
      frmSearch.Show(SearchTasks.PageName);
    }

    private void partNumberToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmSearch frmSearch = new frmSearch(this);
      frmSearch.Owner = (Form) this;
      this.ShowDimmer();
      frmSearch.Show(SearchTasks.PartNumber);
    }

    private void advancedSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmSearch frmSearch = new frmSearch(this);
      frmSearch.Owner = (Form) this;
      this.ShowDimmer();
      frmSearch.Show(SearchTasks.Advanced);
    }

    private void partNameToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmSearch frmSearch = new frmSearch(this);
      frmSearch.Owner = (Form) this;
      this.ShowDimmer();
      frmSearch.Show(SearchTasks.PartName);
    }

    private void textSearchToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmSearch frmSearch = new frmSearch(this);
      frmSearch.Owner = (Form) this;
      this.ShowDimmer();
      frmSearch.Show(SearchTasks.TextSearch);
    }

    public void EnableAddPicMemoTSB(bool value)
    {
      if (this.tsFunctions.InvokeRequired)
        this.tsFunctions.Invoke((Delegate) new frmViewer.EnableAddMemoDelegate(this.EnableAddPicMemoTSB), (object) value);
      else
        this.tsbAddPictureMemo.Enabled = value;
    }

    public void RecoverLocalMemos(string recoveryFile)
    {
      this.xDocLocalMemo = (XmlDocument) null;
      this.xDocLocalMemo = new XmlDocument();
      try
      {
        this.xDocLocalMemo.Load(recoveryFile);
        this.ReloadSamePage();
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM007) Failed to recover specified object", "(E-VWR-EM007)_FAILED_RECOVER", ResourceType.POPUP_MESSAGE));
      }
    }

    public void ChangeGlobalMemoPath(string oldPath, string newPath)
    {
      if (oldPath == string.Empty)
      {
        oldPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        oldPath = oldPath + "\\" + Application.ProductName;
        oldPath = oldPath + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
        if (!Directory.Exists(oldPath))
          Directory.CreateDirectory(oldPath);
      }
      if (this.xDocGlobalMemo != null)
      {
        try
        {
          this.xDocGlobalMemo.Save(oldPath + "\\GlobalMemo.xml");
        }
        catch
        {
          MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM008) Failed to save specified object", "(E-VWR-EM008)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
        }
        this.xDocGlobalMemo = (XmlDocument) null;
      }
      this.xDocGlobalMemo = new XmlDocument();
      if (newPath == string.Empty)
      {
        newPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        newPath = newPath + "\\" + Application.ProductName;
        newPath = newPath + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
        if (!Directory.Exists(newPath))
          Directory.CreateDirectory(newPath);
      }
      try
      {
        if (File.Exists(newPath + "\\GlobalMemo.xml"))
          this.xDocGlobalMemo.Load(newPath + "\\GlobalMemo.xml");
        else
          this.xDocGlobalMemo.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos/>");
        this.ReloadSamePage();
      }
      catch
      {
        try
        {
          this.xDocGlobalMemo = (XmlDocument) null;
          this.xDocGlobalMemo.Load(oldPath + "\\GlobalMemo.xml");
          MessageHandler.ShowWarning(this.GetResource("E-VWR-EM009) Failed to load specified object", "(E-VWR-EM009)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
        }
        catch
        {
        }
      }
    }

    public void LoadMemos()
    {
      if (Program.objMemoSession.getLocalMemoDoc(this.p_ServerId) == null)
      {
        this.xDocLocalMemo = new XmlDocument();
        string empty = string.Empty;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        string str = path + "\\LocalMemo.xml";
        try
        {
          if (File.Exists(str))
            this.xDocLocalMemo.Load(str);
          else
            this.xDocLocalMemo.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos/>");
        }
        catch
        {
          MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM010) Failed to load specified object", "(E-VWR-EM010)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
        }
        Program.objMemoSession.addLocalMemo(this.p_ServerId, this.xDocLocalMemo);
      }
      else
        this.xDocLocalMemo = Program.objMemoSession.getLocalMemoDoc(this.p_ServerId);
      this.xDocGlobalMemo = new XmlDocument();
      string empty1 = string.Empty;
      string path1 = Settings.Default.GlobalMemoFolder;
      if (path1 == string.Empty)
        path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
      if (!Directory.Exists(path1))
      {
        try
        {
          Directory.CreateDirectory(path1);
        }
        catch
        {
          path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
        }
      }
      string str1 = path1 + "\\GlobalMemo.xml";
      try
      {
        if (File.Exists(str1))
          this.xDocGlobalMemo.Load(str1);
        else
          this.xDocGlobalMemo.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos/>");
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM010) Failed to load specified object", "(E-VWR-EM010)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
      }
      Program.objMemoSession.addGlobalMemo(this.p_ServerId, this.xDocGlobalMemo);
    }

    public XmlNodeList reloadGlobalMemos(string sPartNumber)
    {
      XmlNodeList xmlNodeList = (XmlNodeList) null;
      try
      {
        xmlNodeList = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@PartNo='" + sPartNumber + "']");
      }
      catch
      {
      }
      return xmlNodeList;
    }

    public void SaveMemos()
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string path1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
        if (!Directory.Exists(path1))
          Directory.CreateDirectory(path1);
        string path2 = Settings.Default.GlobalMemoFolder;
        if (path2 == string.Empty)
          path2 = path1;
        if (!Directory.Exists(path2))
        {
          try
          {
            Directory.CreateDirectory(path2);
          }
          catch
          {
            path2 = path1;
          }
        }
        string filename1 = path1 + "\\LocalMemo.xml";
        string filename2 = path2 + "\\GlobalMemo.xml";
        if (this.xDocGlobalMemo != null)
        {
          if (Settings.Default.EnableGlobalMemo)
          {
            try
            {
              this.xDocGlobalMemo.Save(filename2);
            }
            catch
            {
              MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM011) Failed to save specified object", "(E-VWR-EM011)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
            }
          }
        }
        if (this.xDocLocalMemo == null)
          return;
        if (!Settings.Default.EnableLocalMemo)
          return;
        try
        {
          this.xDocLocalMemo.Save(filename1);
          if (!Settings.Default.EnableLocalMemo || !Settings.Default.LocalMemoBackup)
            return;
          if (File.Exists(Settings.Default.LocalMemoBackupFile))
          {
            try
            {
              this.xDocLocalMemo.Save(Settings.Default.LocalMemoBackupFile);
            }
            catch
            {
              MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM012) Failed to save specified object", "(E-VWR-EM012)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
            }
          }
          else
            MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM012) Failed to save specified object", "(E-VWR-EM012)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
        }
        catch
        {
          MessageHandler.ShowWarning("Problems in saving local memos");
        }
      }
      catch
      {
      }
    }

    public void ShowPictureMemos(bool popup)
    {
      XmlNodeList xLocalMemoList = (XmlNodeList) null;
      XmlNodeList xGlobalMemoList = (XmlNodeList) null;
      XmlNodeList xAdminMemoList = (XmlNodeList) null;
      if (Settings.Default.EnableLocalMemo)
      {
        xLocalMemoList = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@PartNo='']");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xLocalMemoList)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else if (this.GetMemoSortType().ToUpper() == "ASC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument.AppendChild(element1);
            xLocalMemoList = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xLocalMemoList = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
      }
      if (Settings.Default.EnableGlobalMemo)
      {
        xGlobalMemoList = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@PartNo='']");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xGlobalMemoList)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else if (this.GetMemoSortType().ToUpper() == "ASC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument.AppendChild(element1);
            xGlobalMemoList = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xGlobalMemoList = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
      }
      if (Settings.Default.EnableAdminMemo && Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"] != null)
      {
        XmlNode pageSchemaNode = this.objFrmTreeview.PageSchemaNode;
        XmlNode picNode = this.objFrmTreeview.PicNode;
        string[] strArray1 = Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"].Split(new string[1]
        {
          ","
        }, StringSplitOptions.None);
        ArrayList arrayList = new ArrayList();
        for (int index = 0; index < strArray1.Length; ++index)
        {
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) pageSchemaNode.Attributes)
          {
            if (attribute.Value.ToUpper() == strArray1[index].ToUpper())
            {
              arrayList.Add((object) attribute.Name);
              break;
            }
          }
        }
        string str1 = string.Empty;
        foreach (string index in arrayList)
        {
          if (picNode.Attributes[index] != null && picNode.Attributes[index].Value.Trim() != string.Empty)
            str1 = str1 + picNode.Attributes[index].Value + "**";
        }
        XmlDocument xmlDocument1 = new XmlDocument();
        xmlDocument1.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
        string[] strArray2 = str1.Split(new string[1]
        {
          "**"
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray2.Length > 0)
        {
          foreach (string str2 in strArray2)
          {
            if (str2.Contains("||"))
            {
              string[] strArray3 = str2.Split(new string[1]
              {
                "||"
              }, StringSplitOptions.RemoveEmptyEntries);
              string str3 = strArray3[0];
              string type = str3.ToUpper().Equals("TXT") || str3.ToUpper().Equals("REF") || (str3.ToUpper().Equals("HYP") || str3.ToUpper().Equals("PRG")) ? str3.ToLower() : "Unknown";
              string str4 = strArray3[1];
              string index = string.Empty;
              foreach (XmlAttribute attribute in (XmlNamedNodeMap) pageSchemaNode.Attributes)
              {
                if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
                {
                  index = attribute.Name;
                  break;
                }
              }
              string empty = string.Empty;
              if (picNode.Attributes[index] != null)
                empty = picNode.Attributes[index].Value;
              xmlDocument1.DocumentElement.AppendChild(xmlDocument1.ImportNode(this.CreateAdminMemoNode(type, str4, string.Empty, empty), true));
            }
          }
        }
        xAdminMemoList = xmlDocument1.SelectNodes("//Memo");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xAdminMemoList)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument2 = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument2.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument2.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument2.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else if (this.GetMemoSortType().ToUpper() == "ASC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument2.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument2.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument2.AppendChild(element1);
            xAdminMemoList = xmlDocument2.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xAdminMemoList = xmlDocument1.SelectNodes("//Memo");
        }
      }
      if (popup)
      {
        if ((xLocalMemoList == null || xLocalMemoList.Count <= 0) && (xGlobalMemoList == null || xGlobalMemoList.Count <= 0) && (xAdminMemoList == null || xAdminMemoList.Count <= 0))
          return;
        this.objFrmMemo = new frmMemo(this, xLocalMemoList, xGlobalMemoList, xAdminMemoList, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, string.Empty, string.Empty, string.Empty);
        this.objFrmMemo.Owner = (Form) this;
        this.bFromPopup = true;
        this.ShowDimmer();
        this.objFrmMemo.Show();
      }
      else if (Settings.Default.EnableLocalMemo || Settings.Default.EnableGlobalMemo || xAdminMemoList != null && xAdminMemoList.Count > 0)
      {
        this.objFrmMemo = new frmMemo(this, xLocalMemoList, xGlobalMemoList, xAdminMemoList, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, string.Empty, string.Empty, string.Empty);
        this.objFrmMemo.Owner = (Form) this;
        this.bFromPopup = false;
        this.ShowDimmer();
        this.objFrmMemo.Show();
      }
      else
        MessageHandler.ShowInformation(this.GetResource("All memos are disabled. Enable memo from settings screen", "MEMO_DISABLED", ResourceType.POPUP_MESSAGE));
    }

    private XmlNode CreateAdminMemoNode(string type, string value, string sPartNumber, string date)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode node = xmlDocument.CreateNode(XmlNodeType.Element, "Memo", (string) null);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("ServerKey");
      attribute1.Value = Program.iniServers[this.p_ServerId].sIniKey;
      node.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("BookId");
      attribute2.Value = this.p_BookId;
      node.Attributes.Append(attribute2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("PageId");
      attribute3.Value = this.objFrmPicture.CurrentPageId;
      node.Attributes.Append(attribute3);
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("PicIndex");
      attribute4.Value = this.objFrmPartlist.CurrentPicIndex;
      node.Attributes.Append(attribute4);
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("ListIndex");
      attribute5.Value = string.Empty;
      node.Attributes.Append(attribute5);
      XmlAttribute attribute6 = xmlDocument.CreateAttribute("PartNo");
      attribute6.Value = sPartNumber;
      node.Attributes.Append(attribute6);
      XmlAttribute attribute7 = xmlDocument.CreateAttribute("Type");
      attribute7.Value = type;
      node.Attributes.Append(attribute7);
      XmlAttribute attribute8 = xmlDocument.CreateAttribute("Value");
      attribute8.Value = value;
      node.Attributes.Append(attribute8);
      if (type.ToUpper() == "HYP")
      {
        if (value.Contains("|"))
        {
          string[] strArray = value.Split('|');
          if (strArray.Length > 1)
          {
            XmlAttribute attribute9 = xmlDocument.CreateAttribute("Value");
            attribute9.Value = strArray[0];
            node.Attributes.Append(attribute9);
            XmlAttribute attribute10 = xmlDocument.CreateAttribute("Description");
            attribute10.Value = strArray[1];
            node.Attributes.Append(attribute10);
          }
        }
        else
        {
          XmlAttribute attribute9 = xmlDocument.CreateAttribute("Value");
          attribute9.Value = value;
          node.Attributes.Append(attribute9);
        }
      }
      XmlAttribute attribute11 = xmlDocument.CreateAttribute("Update");
      attribute11.Value = date;
      node.Attributes.Append(attribute11);
      return node;
    }

    public void ShowAllMemos()
    {
      this.LoadMemos();
      XmlNodeList xLocalMemoList = (XmlNodeList) null;
      XmlNodeList xGlobalMemoList = (XmlNodeList) null;
      XmlNodeList xAdminMemoList = (XmlNodeList) null;
      string str1 = string.Empty;
      if (Settings.Default.EnableLocalMemo)
      {
        xLocalMemoList = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xLocalMemoList)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else if (this.GetMemoSortType().ToUpper() == "ASC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument.AppendChild(element1);
            xLocalMemoList = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xLocalMemoList = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
      }
      if (Settings.Default.EnableGlobalMemo)
      {
        xGlobalMemoList = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xGlobalMemoList)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else if (this.GetMemoSortType().ToUpper() == "ASC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument.AppendChild(element1);
            xGlobalMemoList = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xGlobalMemoList = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
      }
      if (Settings.Default.EnableAdminMemo)
      {
        string str2 = string.Empty;
        try
        {
          str2 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
          str2 = str2 + "\\" + Program.iniServers[this.p_ServerId].sIniKey;
          str2 = str2 + "\\" + this.p_BookId;
          if (!Directory.Exists(str2))
            Directory.CreateDirectory(str2);
          str1 = str2;
          str2 = str2 + "\\" + this.p_BookId + ".xml";
        }
        catch
        {
        }
        XmlDocument xmlDocument1 = new XmlDocument();
        XmlDocument xmlDocument2 = new XmlDocument();
        xmlDocument2.Load(str2);
        if (Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null)
        {
          if (Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          {
            try
            {
              string empty = string.Empty;
              string str3 = new AES().Decode(xmlDocument2.InnerText, "0123456789ABCDEF");
              xmlDocument2.DocumentElement.InnerXml = str3;
            }
            catch
            {
            }
          }
        }
        if (Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"] != null)
        {
          XmlNode pageSchemaNode = this.objFrmTreeview.PageSchemaNode;
          XmlNodeList xmlNodeList = xmlDocument2.SelectNodes("//Pic");
          string[] strArray1 = Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.None);
          ArrayList arrayList = new ArrayList();
          for (int index = 0; index < strArray1.Length; ++index)
          {
            foreach (XmlAttribute attribute in (XmlNamedNodeMap) pageSchemaNode.Attributes)
            {
              if (attribute.Value.ToUpper() == strArray1[index].ToUpper())
              {
                arrayList.Add((object) attribute.Name);
                break;
              }
            }
          }
          xmlDocument1.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
          foreach (XmlNode xmlNode in xmlNodeList)
          {
            string str3 = string.Empty;
            foreach (string index in arrayList)
            {
              if (xmlNode.Attributes[index] != null && xmlNode.Attributes[index].Value.Trim() != string.Empty)
                str3 = str3 + xmlNode.Attributes[index].Value + "**";
            }
            string[] strArray2 = str3.Split(new string[1]
            {
              "**"
            }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray2.Length > 0)
            {
              foreach (string str4 in strArray2)
              {
                if (str4.Contains("||"))
                {
                  string[] strArray3 = str4.Split(new string[1]
                  {
                    "||"
                  }, StringSplitOptions.RemoveEmptyEntries);
                  string str5 = strArray3[0];
                  string type = str5.ToUpper().Equals("TXT") || str5.ToUpper().Equals("REF") || (str5.ToUpper().Equals("HYP") || str5.ToUpper().Equals("PRG")) ? str5.ToLower() : "Unknown";
                  string str6 = strArray3[1];
                  string index = string.Empty;
                  foreach (XmlAttribute attribute in (XmlNamedNodeMap) pageSchemaNode.Attributes)
                  {
                    if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
                    {
                      index = attribute.Name;
                      break;
                    }
                  }
                  string empty = string.Empty;
                  if (xmlNode.Attributes[index] != null)
                    empty = xmlNode.Attributes[index].Value;
                  xmlDocument1.DocumentElement.AppendChild(xmlDocument1.ImportNode(this.CreateAdminMemoNode(type, str6, string.Empty, empty), true));
                }
              }
            }
          }
        }
        XmlNode pageSchemaNode1 = this.objFrmTreeview.PageSchemaNode;
        Hashtable hashtable = new Hashtable();
        ArrayList arrayList1 = new ArrayList();
        string str7 = string.Empty;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) pageSchemaNode1.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
          {
            str7 = attribute.Name;
            break;
          }
        }
        if (str7 != string.Empty)
        {
          foreach (XmlNode selectNode in xmlDocument2.SelectNodes("//@" + str7))
          {
            if (!hashtable.ContainsKey((object) selectNode.Value))
            {
              hashtable.Add((object) selectNode.Value, (object) selectNode.Value);
              arrayList1.Add((object) selectNode.Value);
            }
          }
          for (int index1 = 0; index1 < arrayList1.Count; ++index1)
          {
            string str3 = str1 + "\\" + arrayList1[index1].ToString() + ".xml";
            XmlDocument xmlDocument3 = new XmlDocument();
            if (File.Exists(str3))
            {
              xmlDocument3.Load(str3);
              if (Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null)
              {
                if (Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
                {
                  try
                  {
                    string empty = string.Empty;
                    string str4 = new AES().Decode(xmlDocument3.InnerText, "0123456789ABCDEF");
                    xmlDocument3.DocumentElement.InnerXml = str4;
                  }
                  catch
                  {
                  }
                }
              }
              this.objFrmPartlist.GetCurListSchema();
              XmlNodeList xmlNodeList = xmlDocument3.SelectNodes("//Part");
              XmlNode xmlNode1 = xmlDocument3.SelectSingleNode("//Schema");
              if (xmlNode1 != null)
              {
                try
                {
                  string[] strArray1 = Program.iniServers[this.ServerId].items["PLIST_SETTINGS", "MEM"].Split(new string[1]
                  {
                    "|"
                  }, StringSplitOptions.None);
                  ArrayList arrayList2 = new ArrayList();
                  for (int index2 = 0; index2 < strArray1.Length; ++index2)
                  {
                    foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode1.Attributes)
                    {
                      if (attribute.Value.ToUpper() == strArray1[index2].ToUpper())
                      {
                        arrayList2.Add((object) attribute.Name);
                        break;
                      }
                    }
                  }
                  foreach (XmlNode xmlNode2 in xmlNodeList)
                  {
                    string str4 = string.Empty;
                    foreach (string index2 in arrayList2)
                    {
                      if (xmlNode2.Attributes[index2] != null && xmlNode2.Attributes[index2].Value.Trim() != string.Empty)
                        str4 = str4 + xmlNode2.Attributes[index2].Value + "**";
                    }
                    string[] strArray2 = str4.Split(new string[1]
                    {
                      "**"
                    }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray2.Length > 0)
                    {
                      foreach (string str5 in strArray2)
                      {
                        if (str5.Contains("||"))
                        {
                          string[] strArray3 = str5.Split(new string[1]
                          {
                            "||"
                          }, StringSplitOptions.RemoveEmptyEntries);
                          string str6 = strArray3[0];
                          string type = str6.ToUpper().Equals("TXT") || str6.ToUpper().Equals("REF") || (str6.ToUpper().Equals("HYP") || str6.ToUpper().Equals("PRG")) ? str6.ToLower() : "Unknown";
                          string str8 = strArray3[1];
                          string index2 = string.Empty;
                          string index3 = string.Empty;
                          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode1.Attributes)
                          {
                            if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
                              index2 = attribute.Name;
                            if (attribute.Value.ToUpper().Equals("PARTNUMBER"))
                              index3 = attribute.Name;
                          }
                          string empty1 = string.Empty;
                          string empty2 = string.Empty;
                          if (xmlNode2.Attributes[index2] != null)
                          {
                            string date = xmlNode2.Attributes[index2].Value;
                            if (xmlNode2.Attributes[index3] != null)
                            {
                              string sPartNumber = xmlNode2.Attributes[index3].Value;
                              xmlDocument1.DocumentElement.AppendChild(xmlDocument1.ImportNode(this.CreateAdminMemoNode(type, str8, sPartNumber, date), true));
                            }
                          }
                          else
                          {
                            string date = "Unknown";
                            if (xmlNode2.Attributes[index3] != null)
                            {
                              string sPartNumber = xmlNode2.Attributes[index3].Value;
                              xmlDocument1.DocumentElement.AppendChild(xmlDocument1.ImportNode(this.CreateAdminMemoNode(type, str8, sPartNumber, date), true));
                            }
                          }
                        }
                      }
                    }
                  }
                }
                catch (Exception ex)
                {
                }
              }
            }
          }
        }
        xAdminMemoList = xmlDocument1.SelectNodes("//Memo");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xAdminMemoList)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument3 = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument3.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument3.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument3.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else if (this.GetMemoSortType().ToUpper() == "ASC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument3.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument3.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument3.AppendChild(element1);
            xAdminMemoList = xmlDocument3.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xAdminMemoList = xmlDocument1.SelectNodes("//Memo");
        }
      }
      frmMemoList frmMemoList = new frmMemoList(this, xLocalMemoList, xGlobalMemoList, xAdminMemoList, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, string.Empty, string.Empty);
      frmMemoList.Owner = (Form) this;
      this.ShowDimmer();
      frmMemoList.Show();
    }

    public void updateGlobalMemo()
    {
      XmlNodeList xmlNodeList1 = (XmlNodeList) null;
      string empty = string.Empty;
      if (Settings.Default.EnableLocalMemo)
        this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
      if (!Settings.Default.EnableGlobalMemo)
        return;
      XmlNodeList xmlNodeList2 = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@PartNo='']");
      try
      {
        SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
        foreach (XmlNode xNode in xmlNodeList2)
        {
          DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
          this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
        }
        XmlDocument xmlDocument = new XmlDocument();
        XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
        if (this.GetMemoSortType().ToUpper() == "DESC")
        {
          foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
          {
            XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
            XmlNode xmlNode = keyValuePair.Value;
            for (int index = 0; index < xmlNode.Attributes.Count; ++index)
            {
              XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
              attribute.Value = xmlNode.Attributes[index].Value;
              element2.Attributes.Append(attribute);
            }
            element1.AppendChild(element2);
          }
        }
        else
        {
          foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
          {
            XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
            XmlNode xmlNode = keyValuePair.Value;
            for (int index = 0; index < xmlNode.Attributes.Count; ++index)
            {
              XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
              attribute.Value = xmlNode.Attributes[index].Value;
              element2.Attributes.Append(attribute);
            }
            element1.AppendChild(element2);
          }
        }
        xmlDocument.AppendChild(element1);
        xmlNodeList1 = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
      }
      catch (Exception ex)
      {
        xmlNodeList1 = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
      }
    }

    public void PartMemoExists(string partNumber, string sAdminMemoValues, int iGridRowIndex)
    {
      XmlNodeList xmlNodeList1 = (XmlNodeList) null;
      try
      {
        this.GetSaveMemoValue();
        if (this.bSaveMemoOnBookLevel && this.bSaveMemoOnBookLevelChecked)
        {
          int plPartNumColIndex = this.GetPlPartNumColIndex();
          foreach (DataGridViewRow row in (IEnumerable) this.objFrmPartlist.dgPartslist.Rows)
          {
            if (row.Cells[plPartNumColIndex].Value.ToString().Trim().ToUpper() == partNumber.ToUpper().Trim())
              this.objFrmPartlist.ClearMemoIcons(row.Index);
          }
        }
        else
          this.objFrmPartlist.ClearMemoIcons(iGridRowIndex);
      }
      catch (Exception ex)
      {
      }
      if (Settings.Default.EnableLocalMemo)
      {
        XmlNodeList xmlNodeList2 = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@ListIndex='" + this.objFrmPartlist.CurrentListIndex + "'][@PartNo='" + partNumber + "']");
        try
        {
          if (xmlNodeList2.Count > 0)
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xmlNodeList2)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument.AppendChild(element1);
            xmlNodeList2 = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xmlNodeList2 = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
        try
        {
          if (xmlNodeList2.Count > 0)
          {
            List<int> intList = new List<int>();
            int index1 = 0;
            if (this.bSaveMemoOnBookLevel && this.bSaveMemoOnBookLevelChecked)
            {
              for (int index2 = 0; index2 < this.objFrmPartlist.dgPartslist.Columns.Count; ++index2)
              {
                if (this.objFrmPartlist.dgPartslist.Columns[index2].Tag != null && this.objFrmPartlist.dgPartslist.Columns[index2].Tag.ToString() == "PartNumber")
                {
                  index1 = index2;
                  break;
                }
              }
              for (int index2 = 0; index2 < this.objFrmPartlist.dgPartslist.Rows.Count; ++index2)
              {
                try
                {
                  if (this.objFrmPartlist.dgPartslist.Rows[index2].Cells[index1].Value.ToString() == partNumber)
                    intList.Add(index2);
                }
                catch (Exception ex)
                {
                }
              }
              if (xmlNodeList2.Count > 0)
              {
                for (int index2 = 0; index2 < xmlNodeList2.Count; ++index2)
                {
                  this.objFrmPartlist.AddMemoIcon("MEM", intList[index2]);
                  this.objFrmPartlist.AddMemoIcon("LOCMEM", intList[index2]);
                }
              }
              for (int index2 = 0; index2 < xmlNodeList2.Count; ++index2)
              {
                foreach (XmlNode xmlNode in xmlNodeList2)
                {
                  try
                  {
                    if (xmlNode.Attributes["Type"] != null)
                    {
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "TXT")
                      {
                        this.objFrmPartlist.AddMemoIcon("LOCTXTMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
                      }
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "HYP")
                      {
                        this.objFrmPartlist.AddMemoIcon("LOCHYPMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
                      }
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "REF")
                      {
                        this.objFrmPartlist.AddMemoIcon("LOCREFMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
                      }
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "PRG")
                      {
                        this.objFrmPartlist.AddMemoIcon("LOCPRGMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
                      }
                    }
                  }
                  catch
                  {
                  }
                }
              }
            }
            else if (xmlNodeList2.Count > 0)
            {
              this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
              this.objFrmPartlist.AddMemoIcon("LOCMEM", iGridRowIndex);
              try
              {
                if (this.bSaveMemoOnBookLevel)
                {
                  if (this.bSaveMemoOnBookLevelChecked)
                  {
                    int plPartNumColIndex = this.GetPlPartNumColIndex();
                    foreach (DataGridViewRow row in (IEnumerable) this.objFrmPartlist.dgPartslist.Rows)
                    {
                      if (row.Cells[plPartNumColIndex].Value.ToString().Trim().ToUpper() == partNumber.ToUpper().Trim())
                      {
                        this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("LOCMEM", iGridRowIndex);
                      }
                    }
                  }
                }
              }
              catch (Exception ex)
              {
              }
              foreach (XmlNode xmlNode in xmlNodeList2)
              {
                try
                {
                  if (xmlNode.Attributes["Type"] != null)
                  {
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "TXT")
                    {
                      this.objFrmPartlist.AddMemoIcon("LOCTXTMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
                    }
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "HYP")
                    {
                      this.objFrmPartlist.AddMemoIcon("LOCHYPMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
                    }
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "REF")
                    {
                      this.objFrmPartlist.AddMemoIcon("LOCREFMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
                    }
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "PRG")
                    {
                      this.objFrmPartlist.AddMemoIcon("LOCPRGMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
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
        catch (Exception ex)
        {
        }
      }
      if (Settings.Default.EnableGlobalMemo)
      {
        XmlNodeList xmlNodeList2 = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@ListIndex='" + this.objFrmPartlist.CurrentListIndex + "'][@PartNo='" + partNumber + "']");
        try
        {
          if (xmlNodeList2.Count > 0)
          {
            SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
            foreach (XmlNode xNode in xmlNodeList2)
            {
              DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
              this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
            }
            XmlDocument xmlDocument = new XmlDocument();
            XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
            if (this.GetMemoSortType().ToUpper() == "DESC")
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            else
            {
              foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
              {
                XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                XmlNode xmlNode = keyValuePair.Value;
                for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                {
                  XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                  attribute.Value = xmlNode.Attributes[index].Value;
                  element2.Attributes.Append(attribute);
                }
                element1.AppendChild(element2);
              }
            }
            xmlDocument.AppendChild(element1);
            xmlNodeList2 = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
          }
        }
        catch (Exception ex)
        {
          xmlNodeList2 = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
        try
        {
          if (xmlNodeList2.Count > 0)
          {
            List<int> intList = new List<int>();
            int index1 = 0;
            if (this.bSaveMemoOnBookLevel && this.bSaveMemoOnBookLevelChecked)
            {
              for (int index2 = 0; index2 < this.objFrmPartlist.dgPartslist.Columns.Count; ++index2)
              {
                if (this.objFrmPartlist.dgPartslist.Columns[index2].Tag != null && this.objFrmPartlist.dgPartslist.Columns[index2].Tag.ToString() == "PartNumber")
                {
                  index1 = index2;
                  break;
                }
              }
              for (int index2 = 0; index2 < this.objFrmPartlist.dgPartslist.Rows.Count; ++index2)
              {
                try
                {
                  if (this.objFrmPartlist.dgPartslist.Rows[index2].Cells[index1].Value.ToString() == partNumber)
                    intList.Add(index2);
                }
                catch (Exception ex)
                {
                }
              }
              if (xmlNodeList2.Count > 0)
              {
                for (int index2 = 0; index2 < xmlNodeList2.Count; ++index2)
                {
                  this.objFrmPartlist.AddMemoIcon("MEM", intList[index2]);
                  this.objFrmPartlist.AddMemoIcon("GBLMEM", intList[index2]);
                }
              }
              for (int index2 = 0; index2 < xmlNodeList2.Count; ++index2)
              {
                foreach (XmlNode xmlNode in xmlNodeList2)
                {
                  try
                  {
                    if (xmlNode.Attributes["Type"] != null)
                    {
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "TXT")
                      {
                        this.objFrmPartlist.AddMemoIcon("GBLTXTMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
                      }
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "HYP")
                      {
                        this.objFrmPartlist.AddMemoIcon("GBLHYPMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
                      }
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "REF")
                      {
                        this.objFrmPartlist.AddMemoIcon("GBLREFMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
                      }
                      if (xmlNode.Attributes["Type"].Value.ToUpper() == "PRG")
                      {
                        this.objFrmPartlist.AddMemoIcon("GBLPRGMEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
                      }
                    }
                  }
                  catch
                  {
                  }
                }
              }
            }
            else
            {
              if (xmlNodeList2.Count > 0)
              {
                this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
                this.objFrmPartlist.AddMemoIcon("GBLMEM", iGridRowIndex);
              }
              try
              {
                if (this.bSaveMemoOnBookLevel)
                {
                  if (this.bSaveMemoOnBookLevelChecked)
                  {
                    int plPartNumColIndex = this.GetPlPartNumColIndex();
                    foreach (DataGridViewRow row in (IEnumerable) this.objFrmPartlist.dgPartslist.Rows)
                    {
                      if (row.Cells[plPartNumColIndex].Value.ToString().Trim().ToUpper() == partNumber.ToUpper().Trim())
                      {
                        this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
                        this.objFrmPartlist.AddMemoIcon("GBLMEM", iGridRowIndex);
                      }
                    }
                  }
                }
              }
              catch (Exception ex)
              {
              }
              foreach (XmlNode xmlNode in xmlNodeList2)
              {
                try
                {
                  if (xmlNode.Attributes["Type"] != null)
                  {
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "TXT")
                    {
                      this.objFrmPartlist.AddMemoIcon("GBLTXTMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
                    }
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "HYP")
                    {
                      this.objFrmPartlist.AddMemoIcon("GBLHYPMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
                    }
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "REF")
                    {
                      this.objFrmPartlist.AddMemoIcon("GBLREFMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
                    }
                    if (xmlNode.Attributes["Type"].Value.ToUpper() == "PRG")
                    {
                      this.objFrmPartlist.AddMemoIcon("GBLPRGMEM", iGridRowIndex);
                      this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
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
        catch (Exception ex)
        {
        }
      }
      if (!Settings.Default.EnableAdminMemo)
        return;
      XmlDocument xmlDocument1 = new XmlDocument();
      xmlDocument1.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
      string[] strArray1 = sAdminMemoValues.Split(new string[1]
      {
        "**"
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray1.Length > 0)
      {
        string type = string.Empty;
        foreach (string str1 in strArray1)
        {
          if (str1.Contains("||"))
          {
            string[] strArray2 = str1.Split(new string[1]
            {
              "||"
            }, StringSplitOptions.RemoveEmptyEntries);
            string str2 = strArray2[0];
            type = str2.ToUpper().Equals("TXT") || str2.ToUpper().Equals("REF") || (str2.ToUpper().Equals("HYP") || str2.ToUpper().Equals("PRG")) ? str2.ToLower() : "Unknown";
            if (type != "Unknown")
            {
              if (type == "txt")
              {
                this.objFrmPartlist.AddMemoIcon("ADMTXTMEM", iGridRowIndex);
                this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
              }
              if (type == "hyp")
              {
                this.objFrmPartlist.AddMemoIcon("ADMHYPMEM", iGridRowIndex);
                this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
              }
              if (type == "ref")
              {
                this.objFrmPartlist.AddMemoIcon("ADMREFMEM", iGridRowIndex);
                this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
              }
              if (type == "prg")
              {
                this.objFrmPartlist.AddMemoIcon("ADMPRGMEM", iGridRowIndex);
                this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
              }
            }
            string str3 = strArray2[1];
            xmlDocument1.DocumentElement.AppendChild(xmlDocument1.ImportNode(this.CreateAdminMemoNode(type, str3, string.Empty, string.Empty), true));
          }
        }
        if (type != string.Empty && type.ToLower() != "unknown")
        {
          this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
          this.objFrmPartlist.AddMemoIcon("ADMMEM", iGridRowIndex);
        }
      }
      XmlNodeList xmlNodeList3 = xmlDocument1.SelectNodes("//Memo");
      try
      {
        if (xmlNodeList3.Count <= 0)
          return;
        SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
        foreach (XmlNode xNode in xmlNodeList3)
        {
          DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
          this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
        }
        XmlDocument xmlDocument2 = new XmlDocument();
        XmlNode element1 = (XmlNode) xmlDocument2.CreateElement("Memos");
        if (this.GetMemoSortType().ToUpper() == "DESC")
        {
          foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
          {
            XmlNode element2 = (XmlNode) xmlDocument2.CreateElement("Memo");
            XmlNode xmlNode = keyValuePair.Value;
            for (int index = 0; index < xmlNode.Attributes.Count; ++index)
            {
              XmlAttribute attribute = xmlDocument2.CreateAttribute(xmlNode.Attributes[index].Name);
              attribute.Value = xmlNode.Attributes[index].Value;
              element2.Attributes.Append(attribute);
            }
            element1.AppendChild(element2);
          }
        }
        else
        {
          foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
          {
            XmlNode element2 = (XmlNode) xmlDocument2.CreateElement("Memo");
            XmlNode xmlNode = keyValuePair.Value;
            for (int index = 0; index < xmlNode.Attributes.Count; ++index)
            {
              XmlAttribute attribute = xmlDocument2.CreateAttribute(xmlNode.Attributes[index].Name);
              attribute.Value = xmlNode.Attributes[index].Value;
              element2.Attributes.Append(attribute);
            }
            element1.AppendChild(element2);
          }
        }
        xmlDocument2.AppendChild(element1);
        xmlNodeList1 = xmlDocument2.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
      }
      catch (Exception ex)
      {
        xmlNodeList1 = xmlDocument1.SelectNodes("//Memo");
      }
    }

    public void ShowPartMemos(string partNumber, string sAdminMemoValues, string sUpdateDate, string sScope)
    {
      XmlNodeList xGlobalMemoList = (XmlNodeList) null;
      XmlNodeList xLocalMemoList = (XmlNodeList) null;
      XmlNodeList xAdminMemoList = (XmlNodeList) null;
      if (Settings.Default.EnableGlobalMemo)
      {
        xGlobalMemoList = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@ListIndex='" + this.objFrmPartlist.CurrentListIndex + "'][@PartNo='" + partNumber + "']");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            if (xGlobalMemoList.Count > 0)
            {
              SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
              foreach (XmlNode xNode in xGlobalMemoList)
              {
                DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
              }
              XmlDocument xmlDocument = new XmlDocument();
              XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
              if (this.GetMemoSortType().ToUpper() == "DESC")
              {
                foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
                {
                  XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                  XmlNode xmlNode = keyValuePair.Value;
                  for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                  {
                    XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                    attribute.Value = xmlNode.Attributes[index].Value;
                    element2.Attributes.Append(attribute);
                  }
                  element1.AppendChild(element2);
                }
              }
              else if (this.GetMemoSortType().ToUpper() == "ASC")
              {
                foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
                {
                  XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                  XmlNode xmlNode = keyValuePair.Value;
                  for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                  {
                    XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                    attribute.Value = xmlNode.Attributes[index].Value;
                    element2.Attributes.Append(attribute);
                  }
                  element1.AppendChild(element2);
                }
              }
              xmlDocument.AppendChild(element1);
              xGlobalMemoList = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
            }
          }
        }
        catch (Exception ex)
        {
          xGlobalMemoList = this.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
      }
      if (Settings.Default.EnableLocalMemo)
      {
        xLocalMemoList = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "'][@PageId='" + this.objFrmPicture.CurrentPageId + "'][@PicIndex='" + this.objFrmPartlist.CurrentPicIndex + "'][@ListIndex='" + this.objFrmPartlist.CurrentListIndex + "'][@PartNo='" + partNumber + "']");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            if (xLocalMemoList.Count > 0)
            {
              SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
              foreach (XmlNode xNode in xLocalMemoList)
              {
                DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
              }
              XmlDocument xmlDocument = new XmlDocument();
              XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
              if (this.GetMemoSortType().ToUpper() == "DESC")
              {
                foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
                {
                  XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                  XmlNode xmlNode = keyValuePair.Value;
                  for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                  {
                    XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                    attribute.Value = xmlNode.Attributes[index].Value;
                    element2.Attributes.Append(attribute);
                  }
                  element1.AppendChild(element2);
                }
              }
              else if (this.GetMemoSortType().ToUpper() == "ASC")
              {
                foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
                {
                  XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                  XmlNode xmlNode = keyValuePair.Value;
                  for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                  {
                    XmlAttribute attribute = xmlDocument.CreateAttribute(xmlNode.Attributes[index].Name);
                    attribute.Value = xmlNode.Attributes[index].Value;
                    element2.Attributes.Append(attribute);
                  }
                  element1.AppendChild(element2);
                }
              }
              xmlDocument.AppendChild(element1);
              xLocalMemoList = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
            }
          }
        }
        catch (Exception ex)
        {
          xLocalMemoList = this.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
        }
      }
      if (Settings.Default.EnableAdminMemo)
      {
        XmlDocument xmlDocument1 = new XmlDocument();
        xmlDocument1.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
        string[] strArray1 = sAdminMemoValues.Split(new string[1]
        {
          "**"
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray1.Length > 0)
        {
          foreach (string str1 in strArray1)
          {
            if (str1.Contains("||"))
            {
              string[] strArray2 = str1.Split(new string[1]
              {
                "||"
              }, StringSplitOptions.RemoveEmptyEntries);
              string str2 = strArray2[0];
              string type = str2.ToUpper().Equals("TXT") || str2.ToUpper().Equals("REF") || (str2.ToUpper().Equals("HYP") || str2.ToUpper().Equals("PRG")) ? str2.ToLower() : "Unknown";
              string str3 = strArray2[1];
              xmlDocument1.DocumentElement.AppendChild(xmlDocument1.ImportNode(this.CreateAdminMemoNode(type, str3, string.Empty, sUpdateDate), true));
            }
          }
        }
        xAdminMemoList = xmlDocument1.SelectNodes("//Memo");
        try
        {
          if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
          {
            if (xAdminMemoList.Count > 0)
            {
              SortedDictionary<DateTime, XmlNode> sortedDictionary = new SortedDictionary<DateTime, XmlNode>();
              foreach (XmlNode xNode in xAdminMemoList)
              {
                DateTime exact = DateTime.ParseExact(xNode.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                this.UpdateMemoDictionary(sortedDictionary, exact, xNode);
              }
              XmlDocument xmlDocument2 = new XmlDocument();
              XmlNode element1 = (XmlNode) xmlDocument2.CreateElement("Memos");
              if (this.GetMemoSortType().ToUpper() == "DESC")
              {
                foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary.Reverse<KeyValuePair<DateTime, XmlNode>>())
                {
                  XmlNode element2 = (XmlNode) xmlDocument2.CreateElement("Memo");
                  XmlNode xmlNode = keyValuePair.Value;
                  for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                  {
                    XmlAttribute attribute = xmlDocument2.CreateAttribute(xmlNode.Attributes[index].Name);
                    attribute.Value = xmlNode.Attributes[index].Value;
                    element2.Attributes.Append(attribute);
                  }
                  element1.AppendChild(element2);
                }
              }
              else if (this.GetMemoSortType().ToUpper() == "ASC")
              {
                foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in sortedDictionary)
                {
                  XmlNode element2 = (XmlNode) xmlDocument2.CreateElement("Memo");
                  XmlNode xmlNode = keyValuePair.Value;
                  for (int index = 0; index < xmlNode.Attributes.Count; ++index)
                  {
                    XmlAttribute attribute = xmlDocument2.CreateAttribute(xmlNode.Attributes[index].Name);
                    attribute.Value = xmlNode.Attributes[index].Value;
                    element2.Attributes.Append(attribute);
                  }
                  element1.AppendChild(element2);
                }
              }
              xmlDocument2.AppendChild(element1);
              xAdminMemoList = xmlDocument2.SelectNodes("//Memos/Memo[@ServerKey='" + Program.iniServers[this.p_ServerId].sIniKey + "'][@BookId='" + this.p_BookId + "']");
            }
          }
        }
        catch (Exception ex)
        {
          xAdminMemoList = xmlDocument1.SelectNodes("//Memo");
        }
      }
      if (Settings.Default.EnableLocalMemo || Settings.Default.EnableGlobalMemo)
      {
        frmMemo frmMemo = new frmMemo(this, xLocalMemoList, xGlobalMemoList, xAdminMemoList, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, this.objFrmPartlist.CurrentListIndex, partNumber, sScope);
        frmMemo.Owner = (Form) this;
        this.bFromPopup = false;
        this.ShowDimmer();
        frmMemo.Show();
      }
      else
        MessageHandler.ShowInformation(this.GetResource("All memos are disabled. Enable memo from settings screen", "MEMO_DISABLED", ResourceType.POPUP_MESSAGE));
    }

    public void OpenBookFromString(string str)
    {
      string[] strArray = str.Split(new string[1]{ " " }, StringSplitOptions.None);
      string[] args = new string[7];
      args[0] = "-o";
      for (int index = 1; index < 7; ++index)
        args[index] = index >= strArray.Length + 1 ? "nil" : (!(strArray[index - 1].Trim() != string.Empty) ? "nil" : strArray[index - 1]);
      int num;
      try
      {
        num = (int) Program.iniKeys[(object) args[1].ToUpper()];
      }
      catch
      {
        num = 9999;
      }
      XmlNode bookNode = this.GetBookNode(args[2], num);
      if (bookNode == null)
        return;
      XmlNode schemaNode = bookNode.SelectSingleNode("//Schema");
      if (schemaNode == null || !Global.SecurityLocksOpen(bookNode, schemaNode, num, this))
        return;
      this.frmParent.NextTime(args);
    }

    private void tsbMemoList_Click(object sender, EventArgs e)
    {
      this.ShowAllMemos();
    }

    public void EnableAddPartMemoMenu(bool value)
    {
      if (this.sBookType.ToUpper() == "GSC")
        this.addPartMemoToolStripMenuItem.Visible = false;
      else
        this.addPartMemoToolStripMenuItem.Enabled = value;
    }

    public void EnableAddPicMemoMenu(bool value)
    {
      this.addPictureMemoToolStripMenuItem.Visible = value;
    }

    public void EnableNavigationItems(bool bFirst, bool bPrev, bool bNext, bool bLast)
    {
      this.tsbNavigateFirst.Enabled = bFirst;
      this.tsbNavigatePrevious.Enabled = bPrev;
      this.tsbNavigateNext.Enabled = bNext;
      this.tsbNavigateLast.Enabled = bLast;
      this.firstPageToolStripMenuItem.Enabled = bFirst;
      this.previousPageToolStripMenuItem.Enabled = bPrev;
      this.nextPageToolStripMenuItem.Enabled = bNext;
      this.lastPageToolStripMenuItem.Enabled = bLast;
    }

    private void tsbNavigateFirst_Click(object sender, EventArgs e)
    {
      if (this.objFrmPicture.isWorking || this.objFrmPartlist.isWorking)
        return;
      this.objFrmTreeview.SelectFirstNode();
    }

    private void tsbNavigatePrevious_Click(object sender, EventArgs e)
    {
      if (this.objFrmPicture.isWorking || this.objFrmPartlist.isWorking)
        return;
      this.objFrmTreeview.SelectPreviousNode();
    }

    private void tsbNavigateNext_Click(object sender, EventArgs e)
    {
      if (this.objFrmPicture.isWorking || this.objFrmPartlist.isWorking)
        return;
      this.objFrmTreeview.SelectNextNode();
    }

    private void tsbNavigateLast_Click(object sender, EventArgs e)
    {
      if (this.objFrmPicture.isWorking || this.objFrmPartlist.isWorking)
        return;
      this.objFrmTreeview.SelectLastNode();
    }

    public void ShowHidePictureToolbar()
    {
      if (this.objFrmPicture.IsDisposed)
        return;
      this.tsPic.Visible = !Settings.Default.ShowPicToolbar && !Program.objAppFeatures.bDjVuToolbar;
      this.objFrmPicture.ShowHidePictureToolbar();
    }

    public void ShowHidePartslistToolbar()
    {
      if (this.objFrmPartlist.IsDisposed)
        return;
      this.objFrmPartlist.ShowHidePartslistToolbar();
    }

    private void singleBookToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (DataSize.spaceLeft < 10485760L)
      {
        this.ShowNotification();
        int num = (int) MessageBox.Show((IWin32Window) this, this.GetResource("Low Disk Space", "LOW_DISK", ResourceType.POPUP_MESSAGE) + "\n" + this.GetResource("Manage disk to free some space", "FREE_SPACE", ResourceType.POPUP_MESSAGE), this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string str1 = Program.iniServers[this.ServerId].items["SETTINGS", "CONTENT_PATH"];
        if (!str1.EndsWith("/"))
          str1 += "/";
        string _sServerPath = str1 + this.BookPublishingId;
        string str2 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.ServerId].sIniKey + "\\" + this.BookPublishingId;
        if (!Directory.Exists(str2))
          Directory.CreateDirectory(str2);
        frmSingleBookDownload singleBookDownload = new frmSingleBookDownload(this, str2, _sServerPath);
        singleBookDownload.Owner = (Form) this;
        this.ShowDimmer();
        singleBookDownload.Show();
        singleBookDownload.Visible = true;
      }
    }

    private void multipleBooksToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (DataSize.spaceLeft < 10485760L)
      {
        this.ShowNotification();
        int num = (int) MessageBox.Show((IWin32Window) this, this.GetResource("Low Disk Space", "LOW_DISK", ResourceType.POPUP_MESSAGE) + "\n" + this.GetResource("Manage disk to free some space", "FREE_SPACE", ResourceType.POPUP_MESSAGE), this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string _sServerPath = Program.iniServers[this.ServerId].items["SETTINGS", "CONTENT_PATH"];
        if (!_sServerPath.EndsWith("/"))
          _sServerPath += "/";
        string str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.ServerId].sIniKey + "\\";
        if (!Directory.Exists(str))
          Directory.CreateDirectory(str);
        frmMultipleBooksDownload multipleBooksDownload = new frmMultipleBooksDownload(this, str, _sServerPath);
        multipleBooksDownload.Owner = (Form) this;
        this.ShowDimmer();
        multipleBooksDownload.Show();
      }
    }

    public ArrayList SingleBookDownloadList(string sLocalPath)
    {
      ArrayList arrayList = new ArrayList();
      this.objFrmTreeview.GetPagesToDownload(ref arrayList, sLocalPath, this.p_Compressed);
      this.BookDowloadAddSearchXmlFile(ref arrayList, sLocalPath, this.BookPublishingId, this.BookType);
      return arrayList;
    }

    public void BookDowloadAddSearchXmlFile(ref ArrayList arrList, string sBookPath, string sBookPublishingID, string sBookType)
    {
      try
      {
        bool flag = false;
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        string str1;
        string str2;
        if (sBookType.ToUpper() == "GSP")
        {
          str1 = "Search.xml";
          str2 = "Search.zip";
        }
        else
        {
          str1 = "TextSearch.xml";
          str2 = "TextSearch.zip";
        }
        string str3 = sBookPath + "\\" + sBookPublishingID + str1;
        string str4 = sBookPath + "\\" + sBookPublishingID + str2;
        if (!File.Exists(str3))
        {
          if (File.Exists(str4))
          {
            try
            {
              Global.Unzip(str4);
            }
            catch
            {
            }
          }
        }
        if (!File.Exists(str3))
        {
          flag = true;
        }
        else
        {
          int result = 0;
          if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
            result = 0;
          if (result == 0)
            flag = true;
          else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str3, this.p_Compressed, this.p_Encrypted), Global.GetServerUpdateDateFromXmlNode(this.SchemaNode, this.BookNode), result))
            flag = true;
        }
        if (!flag)
          return;
        if (this.p_Compressed)
          arrList.Insert(0, (object) (sBookPublishingID + str2));
        else
          arrList.Insert(0, (object) (sBookPublishingID + str1));
      }
      catch
      {
      }
    }

    public void UncheckAllRows()
    {
      try
      {
        this.frmParent.UncheckAllRows();
      }
      catch
      {
      }
    }

    public void AddNewRecord(DataGridViewRow dRow)
    {
      this.frmParent.AddNewRecord(dRow);
    }

    public void SelListAddRemoveRow(int ServerId, XmlNode xSchemaNode, DataGridViewRow dr, bool bAddRow)
    {
      try
      {
        string empty = string.Empty;
        string sTag = Program.iniServers[this.p_ServerId].sIniKey + "**" + this.p_BookId + "**" + this.objFrmPicture.CurrentPageId + "**" + this.objFrmPartlist.CurrentPicIndex + "**" + this.objFrmPartlist.CurrentListIndex + "**";
        if (dr.Cells["PART_SLIST_KEY"] != null && dr.Cells["PART_SLIST_KEY"].Value.ToString() != string.Empty)
          sTag += dr.Cells["PART_SLIST_KEY"].Value.ToString();
        this.frmParent.SelListAddRemoveRow(ServerId, xSchemaNode, dr, bAddRow, sTag);
      }
      catch
      {
      }
    }

    public void SelListInitialize()
    {
      try
      {
        this.objFrmSelectionlist.selListInitialize();
        this.objFrmSelectionlist.ReLoadPartlistColumns();
        this.objFrmSelectionlist.SynSelectionList();
      }
      catch
      {
      }
    }

    public void ChangeSelListQuantity(string sPartNumber, string sQuantity)
    {
      try
      {
        this.frmParent.ChangeQuantity(sPartNumber, sQuantity);
      }
      catch
      {
      }
    }

    public void DeleteSelListRow(string sPartNumber)
    {
      try
      {
        this.frmParent.DeleteRow(sPartNumber);
        this.frmParent.CheckUncheckRow(sPartNumber, false);
      }
      catch
      {
      }
    }

    public void ShowQuantityScreen(string sPartNumber)
    {
      try
      {
        this.objFrmSelectionlist.ShowQuantityScreen(sPartNumber);
      }
      catch
      {
      }
    }

    public void ClearSelectionList()
    {
      try
      {
        this.frmParent.ClearSelectionList();
      }
      catch
      {
      }
    }

    public void OpenParentPage(string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex, string sPartNumber)
    {
      try
      {
        this.p_ArgsO = new string[6];
        this.p_ArgsO[0] = sServerKey;
        this.p_ArgsO[1] = sBookPubId;
        this.p_ArgsO[2] = sPageId;
        this.p_ArgsO[3] = sImageIndex;
        this.p_ArgsO[4] = sListIndex;
        this.p_ArgsO[5] = sPartNumber;
        this.LoadDataDirect();
      }
      catch
      {
      }
    }

    public bool PartInSelectionList(string sPartNumber)
    {
      try
      {
        return this.objFrmSelectionlist.PartInSelectionList(sPartNumber, Program.iniServers[this.p_ServerId].sIniKey, this.p_BookId, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, this.objFrmPartlist.CurrentListIndex);
      }
      catch
      {
        return false;
      }
    }

    public bool PartInSelectionListA(string sPartNumber)
    {
      try
      {
        return this.frmParent.PartInSelectionListA(sPartNumber, Program.iniServers[this.p_ServerId].sIniKey, this.p_BookId, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, this.objFrmPartlist.CurrentListIndex);
      }
      catch
      {
        return false;
      }
    }

    public void CheckUncheckRow(string iRowNumber, string sPartArguments, bool bCheck)
    {
      try
      {
        sPartArguments.Split(new string[1]{ "**" }, StringSplitOptions.RemoveEmptyEntries);
        this.frmParent.CheckUncheckRow(iRowNumber, sPartArguments, bCheck);
      }
      catch
      {
      }
    }

    public void CheckUncheckRow(string sPartNumber, bool bCheck)
    {
      try
      {
        this.frmParent.CheckUncheckRow(sPartNumber, bCheck);
      }
      catch
      {
      }
    }

    public CustomTreeView CurrentTreeView
    {
      get
      {
        return this.objFrmTreeview.CurrentTreeView;
      }
    }

    public int[] GetImageZoom()
    {
      return this.objFrmPicture.GetImageZoom();
    }

    public string GetDjVuZoom()
    {
      return this.objFrmPicture.GetDjVuZoom();
    }

    public void EnableTreeView(bool value)
    {
      if (value)
      {
        this.Enabled = true;
        this.toolStripContainer1.TopToolStripPanel.Enabled = false;
        this.toolStripContainer1.LeftToolStripPanel.Enabled = false;
        this.toolStripContainer1.RightToolStripPanel.Enabled = false;
        this.toolStripContainer1.BottomToolStripPanel.Enabled = false;
        if (!this.objFrmTreeview.IsDisposed)
          this.objFrmTreeview.Enabled = true;
        if (!this.objFrmPicture.IsDisposed)
          this.objFrmPicture.Enabled = false;
        if (!this.objFrmPartlist.IsDisposed)
          this.objFrmPartlist.Enabled = false;
        if (!this.objFrmInfo.IsDisposed)
          this.objFrmInfo.Enabled = false;
        if (this.objFrmSelectionlist.IsDisposed)
          return;
        this.objFrmSelectionlist.Enabled = false;
      }
      else
      {
        this.toolStripContainer1.TopToolStripPanel.Enabled = true;
        this.toolStripContainer1.LeftToolStripPanel.Enabled = true;
        this.toolStripContainer1.RightToolStripPanel.Enabled = true;
        this.toolStripContainer1.BottomToolStripPanel.Enabled = true;
        if (!this.objFrmPicture.IsDisposed)
          this.objFrmPicture.Enabled = true;
        if (!this.objFrmPartlist.IsDisposed)
          this.objFrmPartlist.Enabled = true;
        if (!this.objFrmInfo.IsDisposed)
          this.objFrmInfo.Enabled = true;
        if (!this.objFrmSelectionlist.IsDisposed)
          this.objFrmSelectionlist.Enabled = true;
        this.Enabled = false;
      }
    }

    public int ExportCurrentImage(string filename, string fmt, bool bNeedAnno, int pageIndex, bool bCurZoom, int full, int fullT, int fullR, int fullB, int viewL, int viewT, int viewR, int viewB)
    {
      return this.objFrmPicture.ExportCurrentImage(filename, fmt, bNeedAnno, pageIndex, bCurZoom, full, fullT, fullR, fullB, viewL, viewT, viewR, viewB);
    }

    public string CurrentImageSource()
    {
      try
      {
        return this.objFrmPicture.CurrentImageSource();
      }
      catch
      {
        return string.Empty;
      }
    }

    public static void SetWindowSizeFromSettings(string thisWindowGeometry, Form formViewer)
    {
      try
      {
        if (string.IsNullOrEmpty(thisWindowGeometry))
          return;
        string[] strArray = thisWindowGeometry.Split('|');
        string str = strArray[4];
        if (str == "Normal")
        {
          Point loc = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
          Size size = new Size(int.Parse(strArray[2]), int.Parse(strArray[3]));
          bool flag1 = frmViewer.IsBizarreLocation(loc, size);
          bool flag2 = frmViewer.IsBizarreSize(size);
          if (flag1 && flag2)
          {
            formViewer.Location = loc;
            formViewer.Size = size;
            formViewer.StartPosition = FormStartPosition.Manual;
            formViewer.WindowState = FormWindowState.Normal;
          }
          else
          {
            if (!flag2)
              return;
            formViewer.Size = size;
          }
        }
        else
        {
          if (!(str == "Maximized"))
            return;
          formViewer.Location = new Point(100, 100);
          formViewer.StartPosition = FormStartPosition.Manual;
          formViewer.WindowState = FormWindowState.Maximized;
        }
      }
      catch
      {
      }
    }

    private static bool IsBizarreLocation(Point loc, Size size)
    {
      return loc.X >= 0 && loc.Y >= 0 && (loc.X + size.Width <= Screen.PrimaryScreen.WorkingArea.Width && loc.Y + size.Height <= Screen.PrimaryScreen.WorkingArea.Height);
    }

    private static bool IsBizarreSize(Size size)
    {
      if (size.Height <= Screen.PrimaryScreen.WorkingArea.Height)
        return size.Width <= Screen.PrimaryScreen.WorkingArea.Width;
      return false;
    }

    public static string SavefrmViewerSizeSettings(Form frmViewer)
    {
      return frmViewer.Location.X.ToString() + "|" + frmViewer.Location.Y.ToString() + "|" + frmViewer.Size.Width.ToString() + "|" + frmViewer.Size.Height.ToString() + "|" + frmViewer.WindowState.ToString();
    }

    private void frmViewer_SizeChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.WindowState == FormWindowState.Minimized)
          return;
        Settings.Default.appCurrentSize = frmViewer.SavefrmViewerSizeSettings((Form) this);
        Settings.Default.Save();
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    public void ShowNotification()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      this.frmParent.ShowNotification("Viewer 3.0 - " + this.GetResource("Low Disk Space", "LOW_DISK", ResourceType.POPUP_MESSAGE), this.GetResource("Less Then", "LESS_THEN", ResourceType.POPUP_MESSAGE) + " " + DataSize.FormattedSize(10485760L) + " " + this.GetResource("Left From", "LEFT_FROM", ResourceType.POPUP_MESSAGE) + " " + DataSize.FormattedSize(DataSize.spaceAllowed) + " " + this.GetResource("of allowed space", "OF_ALLOWED", ResourceType.POPUP_MESSAGE) + "\n\n" + this.GetResource("Manage disk to free some space", "MANAGE_FREE_SPACE", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLocal Viewer 3.0", ResourceType.TITLE));
    }

    public void DisposeNotification()
    {
      this.frmParent.DisposeNotification();
    }

    public void RunDataSizeChecking()
    {
      this.frmParent.RunDataSizeChecking();
    }

    private void manageDiskSpaceToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmDataSize frmDataSize = new frmDataSize(this);
      frmDataSize.Owner = (Form) this;
      this.ShowDimmer();
      frmDataSize.Show();
    }

    private void lblMode_Click(object sender, EventArgs e)
    {
      this.ChangeApplicationMode();
    }

    private void miniMapToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.miniMapToolStripMenuItem.Checked = !this.miniMapToolStripMenuItem.Checked;
      if (this.miniMapToolStripMenuItem.Checked)
      {
        this.objFrmPicture.ShowHideMiniMap(true);
        frmViewer.MiniMapChk = true;
      }
      else
      {
        this.objFrmPicture.ShowHideMiniMap(false);
        frmViewer.MiniMapChk = false;
      }
    }

    private void frmViewer_Deactivate(object sender, EventArgs e)
    {
      this.TopMost = false;
    }

    private void tsbFindText_Click(object sender, EventArgs e)
    {
      try
      {
        this.objFrmPicture.TextSearch();
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void tsbThumbnail_Click(object sender, EventArgs e)
    {
      try
      {
        this.objFrmPicture.ShowHideThumbnail();
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    public void OnOffFeatures()
    {
      try
      {
        if (Program.objAppFeatures.bDcMode)
        {
          Program.objAppFeatures.bDownloadBook = false;
          Program.objAppFeatures.bDownloadBookAll = false;
        }
        this.tsbSearchPageName.Visible = Program.objAppFeatures.bPageNameSearch;
        this.tsbSearchPartName.Visible = Program.objAppFeatures.bPartNameSearch;
        this.tsbSearchPartNumber.Visible = Program.objAppFeatures.bPartNumberSearch;
        this.tsbSearchPartAdvance.Visible = Program.objAppFeatures.bPartAdvanceSearch;
        this.tsbSearchText.Visible = Program.objAppFeatures.bTextSearch;
        this.pageNameToolStripMenuItem.Visible = Program.objAppFeatures.bPageNameSearch;
        this.partNameToolStripMenuItem.Visible = Program.objAppFeatures.bPartNameSearch;
        this.partNumberToolStripMenuItem.Visible = Program.objAppFeatures.bPartNumberSearch;
        this.textSearchToolStripMenuItem.Visible = Program.objAppFeatures.bTextSearch;
        this.advancedSearchToolStripMenuItem.Visible = Program.objAppFeatures.bPartAdvanceSearch;
        if (!Program.objAppFeatures.bPageNameSearch && !Program.objAppFeatures.bPartNameSearch && (!Program.objAppFeatures.bPartNumberSearch && !Program.objAppFeatures.bTextSearch) && !Program.objAppFeatures.bPartAdvanceSearch)
        {
          this.searchToolStripMenuItem.Visible = false;
          this.tsSearch.Visible = false;
        }
        else
        {
          this.searchToolStripMenuItem.Visible = true;
          this.tsSearch.Visible = true;
        }
        this.pageNameSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPageNameSearch;
        this.partNameSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartNameSearch;
        this.partNumberSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartNumberSearch;
        this.textSearceNameSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bTextSearch;
        this.advanceSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartAdvanceSearch;
        if (!Program.objAppFeatures.bPageNameSearch && !Program.objAppFeatures.bPartNameSearch && !Program.objAppFeatures.bPartNumberSearch)
          this.toolStripSeparator9.Visible = false;
        else
          this.toolStripSeparator9.Visible = true;
        this.toolStripSeparator5.Visible = false;
        if (Program.objAppFeatures.bPartNameSearch || Program.objAppFeatures.bPartNumberSearch || Program.objAppFeatures.bPartAdvanceSearch)
          this.toolStripSeparator5.Visible = true;
        if (!Program.objAppFeatures.bPartNameSearch && !Program.objAppFeatures.bPartNumberSearch || !Program.objAppFeatures.bPartAdvanceSearch)
          this.toolStripSeparator6.Visible = false;
        else
          this.toolStripSeparator6.Visible = true;
        this.memoSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bMemo;
        this.toolStripSeparator14.Visible = Program.objAppFeatures.bMemo;
        this.memoDetailsToolStripMenuItem.Visible = Program.objAppFeatures.bMemo;
        this.tsbMemoRecovery.Visible = Program.objAppFeatures.bMemo;
        this.singleBookToolStripMenuItem.Visible = Program.objAppFeatures.bDownloadBook;
        this.multipleBooksToolStripMenuItem.Visible = Program.objAppFeatures.bDownloadBookAll;
        this.toolsToolStripMenuItem.Visible = Program.objAppFeatures.bDownloadBook || Program.objAppFeatures.bDownloadBookAll;
        this.toolStripSeparator18.Visible = Program.objAppFeatures.bDownloadBook || Program.objAppFeatures.bDownloadBookAll;
        this.tsbSingleBookDownload.Visible = Program.objAppFeatures.bDownloadBook;
        this.tsbMultipleBooksDownload.Visible = Program.objAppFeatures.bDownloadBookAll;
        if (!Program.objAppFeatures.bDownloadBook && !Program.objAppFeatures.bDownloadBookAll)
          this.toolStripSeparator15.Visible = false;
        else
          this.toolStripSeparator15.Visible = true;
        this.printToolStripMenuItem.Visible = Program.objAppFeatures.bPrint;
        this.toolStripSeparator11.Visible = Program.objAppFeatures.bPrint;
        this.tsbPrint.Visible = Program.objAppFeatures.bPrint;
        this.tsbAddBookmarks.Visible = Program.objAppFeatures.bBookMark;
        this.bookmarksToolStripMenuItem.Visible = Program.objAppFeatures.bBookMark;
        this.tsbAddPictureMemo.Visible = Program.objAppFeatures.bMemo;
        this.tsbMemoList.Visible = Program.objAppFeatures.bMemo;
        this.tsHistory.Visible = Program.objAppFeatures.bHistory;
        this.previousViewToolStripMenuItem.Visible = Program.objAppFeatures.bHistory;
        this.nextViewToolStripMenuItem.Visible = Program.objAppFeatures.bHistory;
        this.toolStripSeparator12.Visible = Program.objAppFeatures.bHistory;
        this.tsbPicZoomSelect.Visible = Program.objAppFeatures.bSelectiveZoom;
        this.tsbPicCopy.Visible = Program.objAppFeatures.bCopyRegion;
        this.tsbFitPage.Visible = Program.objAppFeatures.bFitPage;
        this.tsbPicPanMode.Visible = Program.objAppFeatures.bDjVuPan;
        this.tsbFindText.Visible = Program.objAppFeatures.bDjVuSearch;
        this.tsbPicSelectText.Visible = Program.objAppFeatures.bDjVuSelectText;
        this.tsbPicZoomIn.Visible = Program.objAppFeatures.bDjVuZoomIn;
        this.tsbPicZoomOut.Visible = Program.objAppFeatures.bDjVuZoomOut;
        this.tsBRotateLeft.Visible = Program.objAppFeatures.bDjVuRotateLeft;
        this.tsBRotateRight.Visible = Program.objAppFeatures.bDjVuRotateRight;
        this.tsbThumbnail.Visible = Program.objAppFeatures.bDjVuNavPan;
        this.toolStripSeparator10.Visible = Program.objAppFeatures.bDjVuSearch || Program.objAppFeatures.bDjVuPan || (Program.objAppFeatures.bDjVuSelectZoom || Program.objAppFeatures.bFitPage) || Program.objAppFeatures.bCopyRegion || Program.objAppFeatures.bDjVuSelectText;
        this.toolStripSeparator22.Visible = Program.objAppFeatures.bDjVuZoomIn || Program.objAppFeatures.bDjVuZoomOut;
        this.toolStripSeparator20.Visible = Program.objAppFeatures.bDjVuRotateLeft || Program.objAppFeatures.bDjVuRotateRight;
        this.helpToolStripMenuItem.Visible = Program.objAppFeatures.bHelpMenu || Program.objAppFeatures.bAboutMenu;
        this.aboutGSPcLocalToolStripMenuItem.Visible = Program.objAppFeatures.bAboutMenu;
        this.helpToolStripMenuItem.Visible = Program.objAppFeatures.bHelpMenu;
        this.tsbAbout.Visible = Program.objAppFeatures.bAboutMenu;
        this.tsbHelp.Visible = Program.objAppFeatures.bHelpMenu;
        this.miniMapToolStripMenuItem.Visible = Program.objAppFeatures.bMiniMap;
        this.menuStrip1.Visible = Program.objAppFeatures.bMenu;
        this.tsbOpenBook.Visible = Program.objAppFeatures.bOpenBookScreen;
        this.openToolStripMenuItem.Visible = Program.objAppFeatures.bOpenBookScreen;
        this.tsbThirdPartyBasket.Visible = Program.objAppFeatures.bThirdPartyBasket;
        this.partsListSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartsList;
        this.selectionListSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bSelectionList;
        if (Program.objAppFeatures.bPartsList || Program.objAppFeatures.bSelectionList)
          return;
        this.toolStripSeparator23.Visible = false;
      }
      catch
      {
      }
    }

    private void ChangeApplicationMode()
    {
      try
      {
        if (!this.workOffLineMenuItem.Enabled)
          return;
        if (Program.objAppMode.bWorkingOffline)
        {
          if (Program.objAppMode.InternetConnected)
          {
            Program.objAppMode.bWorkingOffline = false;
            this.lblMode.ToolTipText = "Working Online";
            this.lblMode.Image = (Image) GSPcLocalViewer.Properties.Resources.online;
            this.tsbSingleBookDownload.Enabled = true;
            this.tsbMultipleBooksDownload.Enabled = true;
            this.singleBookToolStripMenuItem.Enabled = true;
            this.multipleBooksToolStripMenuItem.Enabled = true;
          }
        }
        else
        {
          Program.objAppMode.bWorkingOffline = true;
          this.lblMode.ToolTipText = "Working Offline";
          this.lblMode.Image = (Image) GSPcLocalViewer.Properties.Resources.offline;
          this.tsbSingleBookDownload.Enabled = false;
          this.tsbMultipleBooksDownload.Enabled = false;
          this.singleBookToolStripMenuItem.Enabled = false;
          this.multipleBooksToolStripMenuItem.Enabled = false;
        }
        this.frmParent.ChangeApplicationMode(Program.objAppMode.bWorkingOffline);
      }
      catch
      {
      }
    }

    public void OpenSpecifiedBrowser(string sUrl)
    {
      try
      {
        string str = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
        if (str == string.Empty || str == null)
          str = "iexplore";
        string empty = string.Empty;
        string fileName = new RegistryReader().Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + str + ".exe", string.Empty);
        if (fileName != string.Empty && fileName != null)
        {
          if (sUrl != string.Empty && sUrl != null)
          {
            using (Process process = Process.Start(fileName, sUrl))
            {
              if (process == null)
                return;
              IntPtr handle = process.Handle;
              frmViewer.SetForegroundWindow(process.Handle);
              frmViewer.SetActiveWindow(process.Handle);
            }
          }
          else
          {
            int num = (int) MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
        }
        else
          this.OpenDefaultBrowser(sUrl);
      }
      catch
      {
      }
    }

    public void OpenDefaultBrowser(string sUrl)
    {
      try
      {
        if (sUrl == string.Empty)
        {
          int num = (int) MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          string fileName = new RegistryReader().Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty);
          if (fileName == null)
          {
            using (Process process = Process.Start(sUrl))
            {
              if (process == null)
                return;
              IntPtr handle = process.Handle;
              frmViewer.SetForegroundWindow(process.Handle);
              frmViewer.SetActiveWindow(process.Handle);
            }
          }
          else
          {
            using (Process process = Process.Start(fileName, sUrl))
            {
              if (process == null)
                return;
              IntPtr handle = process.Handle;
              frmViewer.SetForegroundWindow(process.Handle);
              frmViewer.SetActiveWindow(process.Handle);
            }
          }
        }
      }
      catch
      {
      }
    }

    public void InitializeAddOnsGUI()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      IniFileIO iniFileIo = new IniFileIO();
      ArrayList arrayList = new ArrayList();
      ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSPcLocal.ini", "ADDON");
      int num;
      try
      {
        num = Math.IEEERemainder((double) keys.Count, 2.0) != 0.0 ? (keys.Count - 1) / 2 : keys.Count / 2;
      }
      catch
      {
        num = 0;
      }
      for (int index = 1; index <= num; ++index)
      {
        string empty4 = string.Empty;
        try
        {
          string text = Program.iniGSPcLocal.items["ADDON", "ADDON" + (object) index + "_NAME"];
          string str = Program.iniGSPcLocal.items["ADDON", "ADDON" + (object) index + "_PATH"];
          if (text != string.Empty)
          {
            if (str != string.Empty)
            {
              this.addOnToolStripMenuItem.Visible = true;
              this.addOnToolStripMenuItem.DropDown.Items.Add(text);
              this.addOnToolStripMenuItem.DropDown.Items[this.addOnToolStripMenuItem.DropDown.Items.Count - 1].Tag = (object) str;
              this.addOnToolStripMenuItem.DropDown.Items[this.addOnToolStripMenuItem.DropDown.Items.Count - 1].Click += new EventHandler(this.LaunchAddOn);
            }
          }
        }
        catch
        {
        }
      }
    }

    public void LaunchAddOn(object sender, EventArgs e)
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string str = ((ToolStripItem) sender).Tag.ToString();
        if (!(str != string.Empty))
          return;
        string[] strArray = str.Split(new string[1]{ "|" }, StringSplitOptions.RemoveEmptyEntries);
        string fullPath = strArray[0];
        if (strArray.Length > 1)
          empty3 = strArray[1];
        if (!Path.IsPathRooted(fullPath))
          fullPath = Path.GetFullPath(Path.Combine(Application.StartupPath, fullPath));
        Process.Start(fullPath, empty3);
      }
      catch
      {
      }
    }

    public void BookJump(string[] sArgs)
    {
      this.frmParent.NextTime(sArgs);
    }

    public void PageJump(string[] sArgs)
    {
      try
      {
        string str = Program.iniGSPcLocal.items["SETTINGS", "PAGE_JUMP"];
        string empty = string.Empty;
        if (str == null)
          str = "SAME";
        if (str.ToUpper() == "SAME")
        {
          string sArg1 = sArgs[3];
          string sArg2 = sArgs[4];
          TreeNode treeNodeByPageName = this.objFrmTreeview.FindTreeNodeByPageName(this.objFrmTreeview.tvBook.Nodes, sArg1);
          if (treeNodeByPageName != null)
          {
            int result = 0;
            if (int.TryParse(sArgs[4], out result))
              this.iPageJumpImageIndex = result - 1;
            this.objFrmTreeview.tvBook.SelectedNode = treeNodeByPageName;
          }
          else
          {
            if (this.objFrmTreeview.tvBook.Nodes.Count <= 0)
              return;
            this.objFrmTreeview.tvBook.SelectedNode = this.objFrmTreeview.tvBook.Nodes[0];
          }
        }
        else
          this.frmParent.NextTime(sArgs);
      }
      catch
      {
      }
    }

    public XmlNode GetBookNode(string sBookPublishingId, int iServerId)
    {
      XmlDocument xmlDocument = new XmlDocument();
      bool flag1 = false;
      bool flag2 = false;
      string str1 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
      string str2;
      if (iServerId != -1 && iServerId != 9999)
      {
        str2 = str1 + "\\" + Program.iniServers[iServerId].sIniKey + "\\Series.xml";
        if (Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          flag2 = true;
        if (Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
          flag1 = true;
      }
      else
      {
        str2 = str1 + "\\" + Program.iniServers[this.p_ServerId].sIniKey + "\\Series.xml";
        flag1 = this.p_Compressed;
        flag2 = this.p_Encrypted;
      }
      if (!File.Exists(str2) && !Program.objAppMode.bWorkingOffline)
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string str3 = Program.iniServers[iServerId].items["SETTINGS", "CONTENT_PATH"];
        if (!str3.EndsWith("/"))
          str3 += "/";
        string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[iServerId].sIniKey;
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        string surl1;
        string sLocalPath;
        if (this.p_Compressed)
        {
          surl1 = str3 + "Series.zip";
          sLocalPath = path + "\\Series.zip";
        }
        else
        {
          surl1 = str3 + "Series.xml";
          sLocalPath = path + "\\Series.xml";
        }
        this.objDownloader.DownloadFile(surl1, sLocalPath);
      }
      if (!File.Exists(str2))
        return (XmlNode) null;
      if (flag1)
      {
        try
        {
          string str3 = str2.ToLower().Replace(".zip", ".xml");
          Global.Unzip(str3);
          if (File.Exists(str3))
            xmlDocument.Load(str3);
        }
        catch
        {
        }
      }
      else
      {
        try
        {
          xmlDocument.Load(str2);
        }
        catch
        {
          return (XmlNode) null;
        }
      }
      if (flag2)
      {
        try
        {
          string str3 = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str3;
        }
        catch
        {
          return (XmlNode) null;
        }
      }
      XmlNode xmlNode = xmlDocument.SelectSingleNode("//Schema");
      if (xmlNode == null)
        return (XmlNode) null;
      string str4 = "";
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
          str4 = attribute.Name;
      }
      if (str4 == "")
        return (XmlNode) null;
      return xmlDocument.SelectSingleNode("//Books/Book[translate(@" + str4 + ", 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + sBookPublishingId.ToUpper() + "']");
    }

    public void HideSelectionList()
    {
      if (this.objFrmSelectionlist.InvokeRequired)
      {
        this.objFrmSelectionlist.Invoke((Delegate) new frmViewer.HideSelectionListDelegate(this.HideSelectionList));
      }
      else
      {
        this.EnableSelectionlistShowHideButton(false);
        this.objFrmSelectionlist.Hide();
      }
    }

    public void GSCMenuItems()
    {
      if (this.menuStrip1.InvokeRequired)
      {
        this.menuStrip1.Invoke((Delegate) new frmViewer.GSCMenuItemsDelegate(this.GSCMenuItems));
      }
      else
      {
        this.partNameSearchSettingsToolStripMenuItem.Visible = false;
        this.partNumberSearchSettingsToolStripMenuItem.Visible = false;
        this.partNameToolStripMenuItem.Visible = false;
        this.partNumberToolStripMenuItem.Visible = false;
        this.advancedSearchToolStripMenuItem.Visible = false;
        this.printListToolStripMenuItem.Visible = false;
        this.printSelectionListToolStripMenuItem.Visible = false;
        this.toolStripSeparator6.Visible = false;
        this.toolStripSeparator5.Visible = false;
      }
    }

    public void GSCToolBarItems()
    {
      if (this.tsSearch.InvokeRequired)
      {
        this.tsSearch.Invoke((Delegate) new frmViewer.GSCToolBarItemsDelegate(this.GSCToolBarItems));
      }
      else
      {
        this.tsbSearchPartName.Visible = false;
        this.tsbSearchPartNumber.Visible = false;
        this.tsbSearchPartAdvance.Visible = false;
        this.tsbThirdPartyBasket.Visible = false;
      }
    }

    public void ShowSelectionList()
    {
      if (this.objFrmSelectionlist.InvokeRequired)
      {
        this.objFrmSelectionlist.Invoke((Delegate) new frmViewer.ShowSelectionListDelegate(this.ShowSelectionList));
      }
      else
      {
        this.EnableSelectionlistShowHideButton(true);
        this.objFrmSelectionlist.Show();
      }
    }

    public void EnableSelectionlistShowHideButton(bool value)
    {
      if (this.sBookType.ToUpper() == "GSC")
        this.selectionListToolStripMenuItem.Visible = false;
      else
        this.selectionListToolStripMenuItem.Enabled = value;
    }

    public void ShowHideSelectionList()
    {
      try
      {
        string index = string.Empty;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.p_SchemaNode.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("BOOKTYPE"))
            index = attribute.Name;
        }
        if (index != string.Empty && this.p_SchemaNode.Attributes[index] != null && this.p_SchemaNode.Attributes[index].Value != string.Empty)
        {
          string empty = string.Empty;
          this.frmParent.SetBookType(this.p_BookNode.Attributes[index].Value);
        }
        if (this.sBookType.ToUpper() == "GSP")
        {
          this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
          if (this.objFrmSelectionlist.Visible)
          {
            this.selectionListToolStripMenuItem.Visible = true;
            this.ShowSelectionList();
          }
        }
        else
        {
          this.selectionListToolStripMenuItem.Visible = false;
          this.HideSelectionList();
          this.GSCToolBarItems();
          this.GSCMenuItems();
        }
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList = new ArrayList();
        if (iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.ServerId].sIniKey + ".ini", "PLIST_SEARCH").Count == 0 || !Program.objAppFeatures.bPartAdvanceSearch)
        {
          this.tsbSearchPartAdvance.Visible = false;
          this.advancedSearchToolStripMenuItem.Visible = false;
          this.toolStripSeparator6.Visible = false;
        }
        if (this.tsbSearchPageName.Visible || this.tsbSearchPartName.Visible || (this.tsbSearchPartNumber.Visible || this.tsbSearchPartAdvance.Visible) || this.tsbSearchText.Visible)
          return;
        this.tsSearch.Visible = false;
        this.searchToolStripMenuItem.Visible = false;
      }
      catch
      {
      }
    }

    public DataGridViewRowCollection GetSelectionList()
    {
      try
      {
        return this.frmParent.GetSelectionList();
      }
      catch
      {
        return (DataGridViewRowCollection) null;
      }
    }

    public DataGridViewColumnCollection GetSelectionListColumns()
    {
      try
      {
        return this.frmParent.GetSelectionListColumns();
      }
      catch
      {
        return (DataGridViewColumnCollection) null;
      }
    }

    public void CopyConfigurationFile()
    {
      try
      {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName;
        if (!Directory.Exists(path))
          return;
        string destFileName = path + "\\user.config";
        System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
        if (!File.Exists(configuration.FilePath))
          return;
        File.Copy(configuration.FilePath, destFileName, true);
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    public void checkConfigFileExist()
    {
      try
      {
        System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
        if (File.Exists(configuration.FilePath))
          return;
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName;
        if (!Directory.Exists(path))
          return;
        string str = path + "\\user.config";
        if (!File.Exists(str))
          return;
        File.Copy(str, configuration.FilePath, false);
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    private void ReadUserSettingINI()
    {
      try
      {
        IniFile iniFile = new IniFile(Application.StartupPath + "\\UserSet.ini", "UserSet");
        if (iniFile.items["PROXY_SETTINGS", "PROXY_IP"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_IP"] != null)
          Settings.Default.appProxyIP = iniFile.items["PROXY_SETTINGS", "PROXY_IP"];
        if (iniFile.items["PROXY_SETTINGS", "PROXY_PORT"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_PORT"] != null)
          Settings.Default.appProxyPort = iniFile.items["PROXY_SETTINGS", "PROXY_PORT"];
        if (iniFile.items["PROXY_SETTINGS", "PROXY_TYPE"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_TYPE"] != null)
          Settings.Default.appProxyType = iniFile.items["PROXY_SETTINGS", "PROXY_TYPE"];
        if (iniFile.items["PROXY_SETTINGS", "PROXY_TIMEOUT"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_TIMEOUT"] != null)
          Settings.Default.appProxyTimeOut = iniFile.items["PROXY_SETTINGS", "PROXY_TIMEOUT"];
        if (iniFile.items["PROXY_SETTINGS", "USERNAME"] != "" && iniFile.items["PROXY_SETTINGS", "USERNAME"] != null)
          Settings.Default.appProxyLogin = iniFile.items["PROXY_SETTINGS", "USERNAME"];
        if (iniFile.items["PROXY_SETTINGS", "PASSWORD"] != "" && iniFile.items["PROXY_SETTINGS", "PASSWORD"] != null)
          Settings.Default.appProxyPassword = iniFile.items["PROXY_SETTINGS", "PASSWORD"];
        if (iniFile.items["FONT", "FONT_NAME"] != "" && iniFile.items["FONT", "FONT_NAME"] != null && new Font(iniFile.items["FONT", "FONT_NAME"], float.Parse(iniFile.items["FONT", "FONT_SIZE"].ToString())).Name == iniFile.items["FONT", "FONT_NAME"])
          Settings.Default.appFont = new Font(iniFile.items["FONT", "FONT_NAME"], float.Parse(iniFile.items["FONT", "FONT_SIZE"].ToString()));
        if (iniFile.items["FONT_PRINT", "FONT_NAME"] != "" && iniFile.items["FONT_PRINT", "FONT_NAME"] != null)
        {
          if (new Font(iniFile.items["FONT_PRINT", "FONT_NAME"], float.Parse(iniFile.items["FONT_PRINT", "FONT_SIZE"].ToString())).Name == iniFile.items["FONT_PRINT", "FONT_NAME"])
            Settings.Default.printFont = new Font(iniFile.items["FONT_PRINT", "FONT_NAME"], float.Parse(iniFile.items["FONT_PRINT", "FONT_SIZE"].ToString()));
        }
        else if (new Font(iniFile.items["FONT", "FONT_NAME"], float.Parse(iniFile.items["FONT", "FONT_SIZE"].ToString())).Name == iniFile.items["FONT", "FONT_NAME"])
          Settings.Default.printFont = new Font(iniFile.items["FONT", "FONT_NAME"], float.Parse("8"));
        if (iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != null)
          Settings.Default.appHighlightBackColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"]);
        if (iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != null)
          Settings.Default.appHighlightForeColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "FORE_COLOR"]);
        if (iniFile.items["CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR"] != null)
          Settings.Default.PartsListInfoBackColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR"]);
        if (iniFile.items["CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR"] != null)
          Settings.Default.PartsListInfoForeColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR"]);
        if (iniFile.items["OPEN_BOOK", "FRM_LOCATION"] != "" && iniFile.items["OPEN_BOOK", "FRM_LOCATION"] != null)
        {
          string[] strArray = iniFile.items["OPEN_BOOK", "FRM_LOCATION"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmOpenBookLocation = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["OPEN_BOOK", "FRM_SIZE"] != "" && iniFile.items["OPEN_BOOK", "FRM_SIZE"] != null)
        {
          string[] strArray = iniFile.items["OPEN_BOOK", "FRM_SIZE"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmOpenBookSize = new Size(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["OPEN_BOOK", "FRM_STATE"] != "" && iniFile.items["OPEN_BOOK", "FRM_STATE"] != null)
        {
          string str = iniFile.items["OPEN_BOOK", "FRM_STATE"];
          if (str.ToUpper() == "NORMAL")
            Settings.Default.frmOpenBookState = FormWindowState.Normal;
          else if (str.ToUpper() == "MAXIMIZED")
            Settings.Default.frmOpenBookState = FormWindowState.Maximized;
          else if (str.ToUpper() == "MINIMIZED")
            Settings.Default.frmOpenBookState = FormWindowState.Minimized;
        }
        if (iniFile.items["OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE"] != "" && iniFile.items["OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE"] != null)
          Settings.Default.OpenInCurrentInstance = iniFile.items["OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE"].ToUpper() == "TRUE";
        if (iniFile.items["MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER"] != "" && iniFile.items["MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER"] != null)
          Settings.Default.GlobalMemoFolder = iniFile.items["MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER"];
        if (iniFile.items["MEMO_SETTINGS", "ENABLE_ADMIN_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "ENABLE_ADMIN_MEMO"] != null)
          Settings.Default.EnableAdminMemo = iniFile.items["MEMO_SETTINGS", "ENABLE_ADMIN_MEMO"].ToUpper() == "TRUE";
        if (iniFile.items["MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO"] != null)
          Settings.Default.EnableGlobalMemo = iniFile.items["MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO"].ToUpper() == "TRUE";
        if (iniFile.items["MEMO_SETTINGS", "ENABLE_LOCAL_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "ENABLE_LOCAL_MEMO"] != null)
          Settings.Default.EnableLocalMemo = iniFile.items["MEMO_SETTINGS", "ENABLE_LOCAL_MEMO"].ToUpper() == "TRUE";
        if (iniFile.items["MEMO_SETTINGS", "POPUP_PICTURE_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "POPUP_PICTURE_MEMO"] != null)
          Settings.Default.PopupPictureMemo = iniFile.items["MEMO_SETTINGS", "POPUP_PICTURE_MEMO"].ToUpper() == "TRUE";
        if (iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP"] != "" && iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP"] != null)
          Settings.Default.LocalMemoBackup = iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP"].ToUpper() == "TRUE";
        if (iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE"] != "" && iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE"] != null)
          Settings.Default.LocalMemoBackupFile = iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE"];
        if (iniFile.items["MEMO_SETTINGS", "MEMO_LIST_LOCATION"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LIST_LOCATION"] != null)
        {
          string[] strArray = iniFile.items["MEMO_SETTINGS", "MEMO_LIST_LOCATION"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmMemoListLocation = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["MEMO_SETTINGS", "MEMO_LIST_SIZE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LIST_SIZE"] != null)
        {
          string[] strArray = iniFile.items["MEMO_SETTINGS", "MEMO_LIST_SIZE"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmMemoListSize = new Size(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["MEMO_SETTINGS", "MEMO_LIST_STATE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LIST_STATE"] != null)
        {
          string str = iniFile.items["MEMO_SETTINGS", "MEMO_LIST_STATE"];
          if (str.ToUpper() == "NORMAL")
            Settings.Default.frmMemoListState = FormWindowState.Normal;
          else if (str.ToUpper() == "MAXIMIZED")
            Settings.Default.frmMemoListState = FormWindowState.Maximized;
          else if (str.ToUpper() == "MINIMIZED")
            Settings.Default.frmMemoListState = FormWindowState.Minimized;
        }
        if (iniFile.items["MEMO_SETTINGS", "MEMO_LOCATION"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LOCATION"] != null)
        {
          string[] strArray = iniFile.items["MEMO_SETTINGS", "MEMO_LOCATION"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmMemoLocation = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["MEMO_SETTINGS", "MEMO_SIZE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_SIZE"] != null)
        {
          string[] strArray = iniFile.items["MEMO_SETTINGS", "MEMO_SIZE"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmMemoSize = new Size(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["MEMO_SETTINGS", "MEMO_STATE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_STATE"] != null)
        {
          string str = iniFile.items["MEMO_SETTINGS", "MEMO_STATE"];
          if (str.ToUpper() == "NORMAL")
            Settings.Default.frmMemoState = FormWindowState.Normal;
          else if (str.ToUpper() == "MAXIMIZED")
            Settings.Default.frmMemoState = FormWindowState.Maximized;
          else if (str.ToUpper() == "MINIMIZED")
            Settings.Default.frmMemoState = FormWindowState.Minimized;
        }
        if (iniFile.items["ZOOM", "DEFAULT_PICTURE_ZOOM"] != "" && iniFile.items["ZOOM", "DEFAULT_PICTURE_ZOOM"] != null)
          Settings.Default.DefaultPictureZoom = iniFile.items["ZOOM", "DEFAULT_PICTURE_ZOOM"];
        if (iniFile.items["ZOOM", "MAINTAIN_ZOOM"] != "" && iniFile.items["ZOOM", "MAINTAIN_ZOOM"] != null)
          Settings.Default.MaintainZoom = iniFile.items["ZOOM", "MAINTAIN_ZOOM"].ToUpper() == "TRUE";
        if (iniFile.items["ZOOM", "ZOOM_STEP"] != "" && iniFile.items["ZOOM", "ZOOM_STEP"] != null)
          Settings.Default.appZoomStep = int.Parse(iniFile.items["ZOOM", "ZOOM_STEP"]);
        if (iniFile.items["ZOOM", "CURRENT_ZOOM"] != "" && iniFile.items["ZOOM", "CURRENT_ZOOM"] != null)
          Settings.Default.appCurrentZoom = iniFile.items["ZOOM", "CURRENT_ZOOM"];
        if (iniFile.items["TOOLBARS", "SHOW_PIC_TOOLBAR"] != "" && iniFile.items["TOOLBARS", "SHOW_PIC_TOOLBAR"] != null)
          Settings.Default.ShowPicToolbar = iniFile.items["TOOLBARS", "SHOW_PIC_TOOLBAR"].ToUpper() == "TRUE";
        if (iniFile.items["TOOLBARS", "SHOW_LIST_TOOLBAR"] != "" && iniFile.items["TOOLBARS", "SHOW_LIST_TOOLBAR"] != null)
          Settings.Default.ShowListToolbar = iniFile.items["TOOLBARS", "SHOW_LIST_TOOLBAR"].ToUpper() == "TRUE";
        if (iniFile.items["SEARCH", "FRM_LOCATION"] != "" && iniFile.items["SEARCH", "FRM_LOCATION"] != null)
        {
          string[] strArray = iniFile.items["SEARCH", "FRM_LOCATION"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmSearchLocation = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["SEARCH", "FRM_SIZE"] != "" && iniFile.items["SEARCH", "FRM_SIZE"] != null)
        {
          string[] strArray = iniFile.items["SEARCH", "FRM_SIZE"].Split(new string[1]
          {
            ","
          }, StringSplitOptions.RemoveEmptyEntries);
          Settings.Default.frmSearchSize = new Size(int.Parse(strArray[0]), int.Parse(strArray[1]));
        }
        if (iniFile.items["SEARCH", "FRM_STATE"] != "" && iniFile.items["SEARCH", "FRM_STATE"] != null)
        {
          string str = iniFile.items["SEARCH", "FRM_STATE"];
          if (str.ToUpper() == "NORMAL")
            Settings.Default.frmSearchState = FormWindowState.Normal;
          else if (str.ToUpper() == "MAXIMIZED")
            Settings.Default.frmSearchState = FormWindowState.Maximized;
          else if (str.ToUpper() == "MINIMIZED")
            Settings.Default.frmSearchState = FormWindowState.Minimized;
        }
        if (iniFile.items["LANGUAGE", "APP_LANGUAGE"] != "" && iniFile.items["LANGUAGE", "APP_LANGUAGE"] != null)
          Settings.Default.appLanguage = iniFile.items["LANGUAGE", "APP_LANGUAGE"];
        if (iniFile.items["HISTORY", "SIZE"] != "" && iniFile.items["HISTORY", "SIZE"] != null)
          Settings.Default.HistorySize = int.Parse(iniFile.items["HISTORY", "SIZE"]);
        if (iniFile.items["MULTIPARTS", "FITPAGE"] != "" && iniFile.items["MULTIPARTS", "FITPAGE"] != null)
          Settings.Default.appFitPageForMultiParts = iniFile.items["MULTIPARTS", "FITPAGE"].ToUpper() == "TRUE";
        if (iniFile.items["APPLICATION_SETTINGS", "WEB_BROWSER"] != "" && iniFile.items["APPLICATION_SETTINGS", "WEB_BROWSER"] != null)
          Settings.Default.appWebBrowser = iniFile.items["APPLICATION_SETTINGS", "WEB_BROWSER"];
        if (iniFile.items["APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE"] != "" && iniFile.items["APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE"] != null)
          Settings.Default.appCurrentSize = iniFile.items["APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE"];
        if (iniFile.items["MINIMAP_SETTINGS", "SHOW_MINIMAP"] != "" && iniFile.items["MINIMAP_SETTINGS", "SHOW_MINIMAP"] != null)
          frmViewer.iniValueMiniMap = iniFile.items["MINIMAP_SETTINGS", "SHOW_MINIMAP"].ToString().ToLower();
        if (iniFile.items["PRINT_SETTINGS", "SIDE_BY_SIDE"] != "" && iniFile.items["PRINT_SETTINGS", "SIDE_BY_SIDE"] != null)
          Settings.Default.SideBySidePrinting = iniFile.items["PRINT_SETTINGS", "SIDE_BY_SIDE"].ToUpper() == "TRUE";
        if (Program.objAppFeatures.bDjVuScroll)
        {
          if (iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS"] != "" && iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS"] != null)
            Settings.Default.MouseScrollContents = iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS"].ToUpper() == "TRUE";
          if (iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE"] != "")
          {
            if (iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE"] != null)
              Settings.Default.MouseScrollPicture = iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE"].ToUpper() == "TRUE";
          }
        }
        else
        {
          Settings.Default.MouseScrollContents = false;
          Settings.Default.MouseScrollPicture = false;
        }
        try
        {
          if (iniFile.items["APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN"] != "")
          {
            if (iniFile.items["APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN"] != null)
              Settings.Default.appPartsListInfoVisible = Convert.ToBoolean(iniFile.items["APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN"]);
          }
        }
        catch
        {
        }
        try
        {
          if (iniFile.items["APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION"] != string.Empty)
          {
            if (iniFile.items["APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION"] != null)
              Settings.Default.RowSelectionMode = Convert.ToBoolean(iniFile.items["APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION"]);
          }
        }
        catch
        {
        }
        try
        {
          if (iniFile.items["APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE"] != string.Empty)
          {
            if (iniFile.items["APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE"] != null)
              Settings.Default.ListSplitterDistance = int.Parse(iniFile.items["APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE"]);
          }
        }
        catch
        {
        }
        if (iniFile.items["APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS"] != null && iniFile.items["APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS"] != "")
          Settings.Default.ExpandAllContents = iniFile.items["APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS"].ToUpper() == "TRUE";
        if (iniFile.items["APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL"] != null && iniFile.items["APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL"] != "")
          Settings.Default.ExpandContentsLevel = int.Parse(iniFile.items["APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL"]);
        if (iniFile.items["APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU"] == null || !(iniFile.items["APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU"] != ""))
          return;
        if (iniFile.items["APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU"].ToUpper() == "TRUE")
          Settings.Default.HankakuZenkakuFlag = true;
        else
          Settings.Default.HankakuZenkakuFlag = false;
      }
      catch
      {
      }
    }

    private void WriteUserSettingINI()
    {
      try
      {
        string str1 = string.Empty;
        bool flag = false;
        string str2 = Application.StartupPath + "\\UserSet.ini";
        try
        {
          if (File.Exists(str2))
          {
            IniFile iniFile = new IniFile(str2, "UserSet");
            if (iniFile.items["INI_INFO", "LAST_MODIFIED_DATE"] != null)
            {
              flag = true;
              if (!string.IsNullOrEmpty(iniFile.items["INI_INFO", "LAST_MODIFIED_DATE"]))
              {
                str1 = iniFile.items["INI_INFO", "LAST_MODIFIED_DATE"].ToString();
                if (!this.IsValidDateTime(str1))
                  str1 = "";
              }
            }
            File.Delete(str2);
          }
          using (new StreamWriter(str2, true, Encoding.Unicode))
            ;
        }
        catch (IOException ex)
        {
        }
        IniFileIO iniFileIo = new IniFileIO();
        iniFileIo.WriteValue("PROXY_SETTINGS", "PROXY_IP", Settings.Default.appProxyIP.ToString(), str2);
        iniFileIo.WriteValue("PROXY_SETTINGS", "PROXY_PORT", Settings.Default.appProxyPort.ToString(), str2);
        iniFileIo.WriteValue("PROXY_SETTINGS", "PROXY_TYPE", Settings.Default.appProxyType.ToString(), str2);
        iniFileIo.WriteValue("PROXY_SETTINGS", "PROXY_TIMEOUT", Settings.Default.appProxyTimeOut.ToString(), str2);
        iniFileIo.WriteValue("PROXY_SETTINGS", "USERNAME", Settings.Default.appProxyLogin.ToString(), str2);
        iniFileIo.WriteValue("PROXY_SETTINGS", "PASSWORD", Settings.Default.appProxyPassword.ToString(), str2);
        iniFileIo.WriteValue("FONT", "FONT_NAME", Settings.Default.appFont.FontFamily.Name.ToString(), str2);
        iniFileIo.WriteValue("FONT", "FONT_SIZE", Settings.Default.appFont.Size.ToString(), str2);
        iniFileIo.WriteValue("FONT_PRINT", "FONT_NAME", Settings.Default.printFont.FontFamily.Name.ToString(), str2);
        iniFileIo.WriteValue("FONT_PRINT", "FONT_SIZE", Settings.Default.printFont.Size.ToString(), str2);
        iniFileIo.WriteValue("CUSTOM_COLOR", "BACKGROUND_COLOR", ColorTranslator.ToHtml(Settings.Default.appHighlightBackColor).ToString(), str2);
        iniFileIo.WriteValue("CUSTOM_COLOR", "FORE_COLOR", ColorTranslator.ToHtml(Settings.Default.appHighlightForeColor).ToString(), str2);
        iniFileIo.WriteValue("CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR", ColorTranslator.ToHtml(Settings.Default.PartsListInfoBackColor).ToString(), str2);
        iniFileIo.WriteValue("CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR", ColorTranslator.ToHtml(Settings.Default.PartsListInfoForeColor).ToString(), str2);
        iniFileIo.WriteValue("OPEN_BOOK", "FRM_LOCATION", Settings.Default.frmOpenBookLocation.X.ToString() + "," + Settings.Default.frmOpenBookLocation.Y.ToString(), str2);
        iniFileIo.WriteValue("OPEN_BOOK", "FRM_SIZE", Settings.Default.frmOpenBookSize.Width.ToString() + "," + Settings.Default.frmOpenBookSize.Height.ToString(), str2);
        iniFileIo.WriteValue("OPEN_BOOK", "FRM_STATE", Settings.Default.frmOpenBookState.ToString(), str2);
        iniFileIo.WriteValue("OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE", Settings.Default.OpenInCurrentInstance.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER", Settings.Default.GlobalMemoFolder.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "ENABLE_ADMIN_MEMO", Settings.Default.EnableAdminMemo.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO", Settings.Default.EnableGlobalMemo.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "ENABLE_LOCAL_MEMO", Settings.Default.EnableLocalMemo.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "POPUP_PICTURE_MEMO", Settings.Default.PopupPictureMemo.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "LOCAL_MEMO_BACKUP", Settings.Default.LocalMemoBackup.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE", Settings.Default.LocalMemoBackupFile.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "MEMO_LIST_LOCATION", Settings.Default.frmMemoListLocation.X.ToString() + "," + Settings.Default.frmMemoListLocation.Y.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "MEMO_LIST_SIZE", Settings.Default.frmMemoListSize.Width.ToString() + "," + Settings.Default.frmMemoListSize.Height.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "MEMO_LIST_STATE", Settings.Default.frmMemoListState.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "MEMO_LOCATION", Settings.Default.frmMemoLocation.X.ToString() + "," + Settings.Default.frmMemoLocation.Y.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "MEMO_SIZE", Settings.Default.frmMemoSize.Width.ToString() + "," + Settings.Default.frmMemoSize.Height.ToString(), str2);
        iniFileIo.WriteValue("MEMO_SETTINGS", "MEMO_STATE", Settings.Default.frmMemoState.ToString(), str2);
        iniFileIo.WriteValue("ZOOM", "DEFAULT_PICTURE_ZOOM", Settings.Default.DefaultPictureZoom.ToString(), str2);
        iniFileIo.WriteValue("ZOOM", "MAINTAIN_ZOOM", Settings.Default.MaintainZoom.ToString(), str2);
        iniFileIo.WriteValue("ZOOM", "ZOOM_STEP", Settings.Default.appZoomStep.ToString(), str2);
        iniFileIo.WriteValue("ZOOM", "CURRENT_ZOOM", Settings.Default.appCurrentZoom.ToString(), str2);
        iniFileIo.WriteValue("TOOLBARS", "SHOW_PIC_TOOLBAR", Settings.Default.ShowPicToolbar.ToString(), str2);
        iniFileIo.WriteValue("TOOLBARS", "SHOW_LIST_TOOLBAR", Settings.Default.ShowListToolbar.ToString(), str2);
        iniFileIo.WriteValue("SEARCH", "FRM_LOCATION", Settings.Default.frmSearchLocation.X.ToString() + "," + Settings.Default.frmSearchLocation.Y.ToString(), str2);
        iniFileIo.WriteValue("SEARCH", "FRM_SIZE", Settings.Default.frmSearchSize.Width.ToString() + "," + Settings.Default.frmSearchSize.Height.ToString(), str2);
        iniFileIo.WriteValue("SEARCH", "FRM_STATE", Settings.Default.frmSearchState.ToString(), str2);
        iniFileIo.WriteValue("DATASIZE", "FRM_LOCATION", Settings.Default.frmDataSizeLocation.X.ToString() + "," + Settings.Default.frmDataSizeLocation.Y.ToString(), str2);
        iniFileIo.WriteValue("DATASIZE", "FRM_SIZE", Settings.Default.frmDataSizeSize.Width.ToString() + "," + Settings.Default.frmDataSizeSize.Height.ToString(), str2);
        iniFileIo.WriteValue("DATASIZE", "FRM_STATE", Settings.Default.frmDataSizeState.ToString(), str2);
        iniFileIo.WriteValue("LANGUAGE", "APP_LANGUAGE", Settings.Default.appLanguage.ToString(), str2);
        iniFileIo.WriteValue("HISTORY", "SIZE", Settings.Default.HistorySize.ToString(), str2);
        iniFileIo.WriteValue("MULTIPARTS", "FITPAGE", Settings.Default.appFitPageForMultiParts.ToString(), str2);
        iniFileIo.WriteValue("APPLICATION_SETTINGS", "WEB_BROWSER", Settings.Default.appWebBrowser.ToString(), str2);
        iniFileIo.WriteValue("APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE", Settings.Default.appCurrentSize.ToString(), str2);
        iniFileIo.WriteValue("MINIMAP_SETTINGS", "SHOW_MINIMAP", this.miniMapToolStripMenuItem.Checked.ToString(), str2);
        iniFileIo.WriteValue("PRINT_SETTINGS", "SIDE_BY_SIDE", Settings.Default.SideBySidePrinting.ToString(), str2);
        if (Program.objAppFeatures.bDjVuScroll)
        {
          iniFileIo.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS", Settings.Default.MouseScrollContents.ToString(), str2);
          iniFileIo.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE", Settings.Default.MouseScrollPicture.ToString(), str2);
        }
        else
        {
          iniFileIo.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS", "FALSE", str2);
          iniFileIo.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE", "FALSE", str2);
        }
        iniFileIo.WriteValue("APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN", Settings.Default.appPartsListInfoVisible.ToString(), str2);
        try
        {
          iniFileIo.WriteValue("APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION", Settings.Default.RowSelectionMode.ToString(), str2);
        }
        catch
        {
        }
        try
        {
          iniFileIo.WriteValue("APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE", Settings.Default.ListSplitterDistance.ToString(), str2);
        }
        catch
        {
        }
        iniFileIo.WriteValue("APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS", Settings.Default.ExpandAllContents.ToString(), str2);
        iniFileIo.WriteValue("APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL", Settings.Default.ExpandContentsLevel.ToString(), str2);
        iniFileIo.WriteValue("APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU", Settings.Default.HankakuZenkakuFlag.ToString(), str2);
        if (!flag)
          return;
        if (!string.IsNullOrEmpty(str1))
          iniFileIo.WriteValue("INI_INFO", "LAST_MODIFIED_DATE", str1, str2);
        else
          iniFileIo.WriteValue("INI_INFO", "LAST_MODIFIED_DATE", "", str2);
      }
      catch
      {
      }
    }

    protected override void WndProc(ref Message m)
    {
      if (this.objDimmer != null && this.objDimmer.Visible)
      {
        if (m.Msg == 10 || m.Msg == 163 || (m.Msg == 165 || m.Msg == 132) || (m.Msg == 274 || m.Msg == 161 || m.Msg == 164))
          return;
        if (m.Msg == 33)
        {
          m.Result = (IntPtr) 4;
          return;
        }
      }
      base.WndProc(ref m);
    }

    public void ShowDimmer()
    {
      try
      {
        Control control = (Control) this.toolStripContainer1;
        Rectangle bounds = control.Bounds;
        try
        {
          control.Focus();
        }
        catch
        {
        }
        while (control.Parent.GetType() != typeof (frmViewer))
        {
          control = control.Parent;
          bounds.X += control.Left;
          bounds.Y += control.Top;
        }
        Control parent = control.Parent;
        bounds.Intersect(parent.ClientRectangle);
        this.objDimmer.Location = parent.PointToScreen(new Point(bounds.Left, bounds.Top));
        this.objDimmer.Height = bounds.Height;
        this.objDimmer.Width = bounds.Width;
        this.objDimmer.Show((IWin32Window) this);
        this.objDimmer.Height = bounds.Height;
        this.objDimmer.Enabled = false;
      }
      catch
      {
      }
    }

    public void HideDimmer()
    {
      this.objDimmer.Enabled = true;
      this.objDimmer.Hide();
      this.BringToFront();
    }

    public ArrayList ListOfInUseBooks()
    {
      return this.frmParent.ListOfInUseBooks();
    }

    public string GetOSLanguage()
    {
      CultureInfo.CurrentCulture.ClearCachedData();
      return CultureInfo.CurrentCulture.DisplayName.Split(' ')[0].ToString();
    }

    public void ChangeApplicationLanguage()
    {
      try
      {
        this.objFrmInfo.LoadResources();
        this.objFrmPartlist.LoadResources();
        this.objFrmPartlist.SetGridHeaderText();
        this.objFrmSelectionlist.SetGridHeaderText();
        this.objFrmSelectionlist.LoadTitle();
        this.objFrmPicture.LoadResources();
        this.objFrmSelectionlist.LoadResources();
        this.objFrmTreeview.LoadResources();
        this.ResetBookMarkTooltipLanguage();
        this.objFrmPartlist.SetJumpGridHeaderText();
      }
      catch
      {
      }
    }

    public void LoadXML(string filename)
    {
      string appLanguage = Settings.Default.appLanguage;
      string str1 = "";
      try
      {
        if (filename == "English")
        {
          this.LoadEnglish();
        }
        else
        {
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          if (files.Length <= 0)
            return;
          for (int index = 0; index < files.Length; ++index)
          {
            try
            {
              if (File.Exists(files[index]))
              {
                int num1 = files[index].IndexOf(".xml");
                int num2 = files[index].LastIndexOf("\\");
                string str2 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                string str3 = Application.StartupPath + "\\Language XMLs\\" + str2 + ".xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str2 + ".xml");
                XmlNode xmlNode = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
                str1 = xmlNode.Attributes["Language"].Value;
                string str4 = xmlNode.Attributes["EN_NAME"].Value;
                if (!(str1.ToLower() == filename.ToLower()))
                {
                  if (!(str4.ToLower() == filename.ToLower()))
                    continue;
                }
                this.xmlDocument = new XmlDocument();
                this.xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str2 + ".xml");
                if (str4 != null || str4.Length != 0)
                {
                  Settings.Default.appLanguage = str4;
                  this.CurrentLanguage = str4;
                }
                this.SetText();
                this.ChangeApplicationLanguage();
                break;
              }
            }
            catch
            {
              if (filename.Length != 0)
              {
                if (str1.Length != 0)
                {
                  if (filename == str1)
                  {
                    int num = (int) MessageBox.Show(this.GetResource("Failed to load Language XML.", "FAILED_XML", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    Settings.Default.appLanguage = appLanguage;
                    break;
                  }
                }
              }
            }
          }
        }
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("Failed to load Language XML.", "FAILED_XML", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    public void LoadEnglish()
    {
      try
      {
        this.xmlDocument = (XmlDocument) null;
        string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
        for (int index = 0; index < files.Length; ++index)
        {
          try
          {
            if (File.Exists(files[index]))
            {
              int num1 = files[index].IndexOf(".xml");
              int num2 = files[index].LastIndexOf("\\");
              string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
              string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
              XmlDocument xmlDocument = new XmlDocument();
              xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
              string str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value;
              if (str3.Contains("English"))
              {
                if (File.Exists(str2))
                {
                  this.xmlDocument = new XmlDocument();
                  this.xmlDocument.Load(str2);
                  this.CurrentLanguage = "English";
                  this.SetText();
                  this.ChangeApplicationLanguage();
                  break;
                }
              }
              else if (!str3.Contains(str2))
              {
                if (index == str1.Length)
                {
                  this.xmlDocument = new XmlDocument();
                  this.CurrentLanguage = "English";
                  this.SetText();
                  this.ChangeApplicationLanguage();
                }
              }
            }
          }
          catch
          {
            this.xmlDocument = new XmlDocument();
            this.SetText();
            this.ChangeApplicationLanguage();
            break;
          }
        }
        if (this.xmlDocument != null)
          return;
        this.xmlDocument = new XmlDocument();
        this.SetText();
        this.ChangeApplicationLanguage();
      }
      catch
      {
      }
    }

    public void LoadXMLFirstTime()
    {
      this.xmlDocument = (XmlDocument) null;
      try
      {
        if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
        {
          this.GetOSLanguage();
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            try
            {
              if (File.Exists(files[index]))
              {
                int num1 = files[index].IndexOf(".xml");
                int num2 = files[index].LastIndexOf("\\");
                string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                string str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value;
                if (str3.Length != 0)
                {
                  if (str3 != null)
                  {
                    if (str3 == Settings.Default.appLanguage)
                    {
                      if (File.Exists(str2))
                      {
                        this.xmlDocument = new XmlDocument();
                        this.xmlDocument.Load(str2);
                        Settings.Default.appLanguage = str3;
                        this.CurrentLanguage = str3;
                        break;
                      }
                    }
                  }
                }
              }
            }
            catch
            {
            }
          }
        }
        else
        {
          if (Settings.Default.appLanguage != null)
          {
            if (Settings.Default.appLanguage.Length != 0)
              goto label_27;
          }
          Settings.Default.appLanguage = "English";
          string osLanguage = this.GetOSLanguage();
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            try
            {
              if (File.Exists(files[index]))
              {
                int num1 = files[index].IndexOf(".xml");
                int num2 = files[index].LastIndexOf("\\");
                string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                string str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value;
                if (str3 == osLanguage)
                {
                  if (File.Exists(str2))
                  {
                    this.xmlDocument.Load(str2);
                    break;
                  }
                }
                else if (str3 == Settings.Default.appLanguage)
                {
                  if (File.Exists(str2))
                  {
                    this.xmlDocument.Load(str2);
                    break;
                  }
                }
              }
            }
            catch
            {
            }
          }
        }
      }
      catch
      {
      }
label_27:
      if (this.xmlDocument != null)
        return;
      this.xmlDocument = new XmlDocument();
      Settings.Default.appLanguage = "English";
    }

    public string GetResourceValue(string sDefaultValue, string xQuery)
    {
      try
      {
        XmlNode xmlNode = this.xmlDocument.SelectSingleNode("/GSPcLocalViewer" + xQuery);
        if (xmlNode == null || this.xmlDocument == null || (xmlNode.Attributes.Count <= 0 || xmlNode.Attributes["Value"] == null))
          return sDefaultValue;
        string str = xmlNode.Attributes["Value"].Value;
        if (!string.IsNullOrEmpty(str))
          return str;
        return sDefaultValue;
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private void ResetBookMarkTooltipLanguage()
    {
      for (int index1 = 0; index1 < this.bookmarksToolStripMenuItem.DropDown.Items.Count; ++index1)
      {
        string toolTipText = this.bookmarksToolStripMenuItem.DropDown.Items[index1].ToolTipText;
        if (toolTipText != null)
        {
          string[] strArray1 = toolTipText.Split('\n');
          string[] strArray2 = new string[strArray1.Length];
          for (int index2 = 0; index2 < strArray1.Length; ++index2)
            strArray2[index2] = strArray1[index2].Substring(strArray1[index2].IndexOf("=") + 1);
          this.bookmarksToolStripMenuItem.DropDown.Items[index1].ToolTipText = string.Empty;
          if (strArray1.Length >= 1)
            this.bookmarksToolStripMenuItem.DropDown.Items[index1].ToolTipText = this.GetResource("Picture Index", "PICTURE_INDEX", ResourceType.TOOLSTRIP) + " = " + strArray2[0].ToString().Trim();
          if (strArray1.Length >= 2)
          {
            ToolStripItem toolStripItem = this.bookmarksToolStripMenuItem.DropDown.Items[index1];
            toolStripItem.ToolTipText = toolStripItem.ToolTipText + "\n" + this.GetResource("List Index", "LIST_INDEX", ResourceType.TOOLSTRIP) + " = " + strArray2[1].ToString().Trim();
          }
          if (strArray1.Length == 3)
          {
            ToolStripItem toolStripItem = this.bookmarksToolStripMenuItem.DropDown.Items[index1];
            toolStripItem.ToolTipText = toolStripItem.ToolTipText + "\n" + this.GetResource("Part Number", "PART_NUMBER", ResourceType.TOOLSTRIP) + " = " + strArray2[2].ToString().Trim();
          }
        }
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MAIN_FORM']";
        if (rType == ResourceType.TITLE)
        {
          string xQuery = str + "[@Name='" + sKey + "']";
          return this.GetResourceValue(sDefaultValue, xQuery);
        }
        if (rType == ResourceType.LABEL)
          str += "/Resources[@Name='LABEL']";
        else if (rType == ResourceType.BUTTON)
          str += "/Resources[@Name='BUTTON']";
        else if (rType == ResourceType.CHECK_BOX)
          str += "/Resources[@Name='CHECK_BOX']";
        else if (rType == ResourceType.STATUS_MESSAGE)
          str += "/Resources[@Name='STATUS_MESSAGE']";
        else if (rType == ResourceType.POPUP_MESSAGE)
          str += "/Resources[@Name='POPUP_MESSAGE']";
        else if (rType == ResourceType.COMBO_BOX)
          str += "/Resources[@Name='COMBO_BOX']";
        else if (rType == ResourceType.GRID_VIEW)
          str += "/Resources[@Name='GRID_VIEW']";
        else if (rType == ResourceType.LIST_VIEW)
          str += "/Resources[@Name='LIST_VIEW']";
        else if (rType == ResourceType.MENU_BAR)
          str += "/Resources[@Name='MENU_BAR']";
        else if (rType == ResourceType.RADIO_BUTTON)
          str += "/Resources[@Name='RADIO_BUTTON']";
        else if (rType == ResourceType.CONTEXT_MENU)
          str += "/Resources[@Name='CONTEXT_MENU']";
        else if (rType == ResourceType.TOOLSTRIP)
          str += "/Resources[@Name='TOOLSTRIP']";
        else if (rType == ResourceType.POPUP_MESSAGE)
          str += "/Resources[@Name='POPUP_MESSAGE']";
        else if (rType == ResourceType.STATUS_MESSAGE)
          str += "/Resources[@Name='STATUS_MESSAGE']";
        string xQuery1 = str + "/Resource[@Name='" + sKey + "']";
        return this.GetResourceValue(sDefaultValue, xQuery1);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private void SetText()
    {
      this.fileToolStripMenuItem.Text = this.GetResource("File", "FILE", ResourceType.MENU_BAR);
      this.openToolStripMenuItem.Text = this.GetResource("Open", "OPEN", ResourceType.MENU_BAR);
      this.printToolStripMenuItem.Text = this.GetResource("Print …", "PRINT", ResourceType.MENU_BAR);
      this.printPageToolStripMenuItem.Text = this.GetResource("Print Page …", "PRINT_PAGE", ResourceType.MENU_BAR);
      this.printPictureToolStripMenuItem.Text = this.GetResource("Print Picture …", "PRINT_PICTURE", ResourceType.MENU_BAR);
      this.printListToolStripMenuItem.Text = this.GetResource("Print Parts List …", "PRINT_PARTSLIST", ResourceType.MENU_BAR);
      this.printSelectionListToolStripMenuItem.Text = this.GetResource("Print SelectionList…", "PRINT_SELECTIONLIST", ResourceType.MENU_BAR);
      this.closeToolStripMenuItem.Text = this.GetResource("Close", "CLOSE", ResourceType.MENU_BAR);
      this.closeAllToolStripMenuItem.Text = this.GetResource("Close All", "CLOSE_ALL", ResourceType.MENU_BAR);
      this.viewToolStripMenuItem.Text = this.GetResource("View", "VIEW", ResourceType.MENU_BAR);
      this.restoreDefaultsToolStripMenuItem.Text = this.GetResource("Restore Defaults", "RESTORE_DEFAULTS", ResourceType.MENU_BAR);
      this.saveDefaultsToolStripMenuItem.Text = this.GetResource("Save Defaults", "SAVE_DEFAULTS", ResourceType.MENU_BAR);
      this.contentsToolStripMenuItem.Text = this.GetResource("Contents", "CONTENTS", ResourceType.MENU_BAR);
      this.pictureToolStripMenuItem.Text = this.GetResource("Picture", "PICTURE", ResourceType.MENU_BAR);
      this.partslistToolStripMenuItem.Text = this.GetResource("Parts List", "PARTS_LIST", ResourceType.MENU_BAR);
      this.selectionListToolStripMenuItem.Text = this.GetResource("Selection List", "SELECTION_LIST", ResourceType.MENU_BAR);
      this.informationToolStripMenuItem.Text = this.GetResource("Information", "INFORMATION", ResourceType.MENU_BAR);
      this.settingsToolStripMenuItem.Text = this.GetResource("Settings", "SETTINGS", ResourceType.MENU_BAR);
      this.fontToolStripMenuItem.Text = this.GetResource("Font …", "FONT", ResourceType.MENU_BAR);
      this.ColorToolStripMenuItem.Text = this.GetResource("Color …", "COLOR", ResourceType.MENU_BAR);
      this.generalToolStripMenuItem.Text = this.GetResource("General …", "GENERAL", ResourceType.MENU_BAR);
      this.connectionToolStripMenuItem.Text = this.GetResource("Connection Settings …", "CONNECTION_SETTINGS", ResourceType.MENU_BAR);
      this.memoSettingsToolStripMenuItem.Text = this.GetResource("Memos …", "MEMOS_SETTINGS", ResourceType.MENU_BAR);
      this.pageNameSearchSettingsToolStripMenuItem.Text = this.GetResource("Page Name …", "PAGE_NAME", ResourceType.MENU_BAR);
      this.partNameSearchSettingsToolStripMenuItem.Text = this.GetResource("Part Name …", "PART_NAME", ResourceType.MENU_BAR);
      this.partNumberSearchSettingsToolStripMenuItem.Text = this.GetResource("Page Number …", "PART_NUMBER", ResourceType.MENU_BAR);
      this.memoRecoveryToolStripMenuItem.Text = this.GetResource("Memo Recovery …", "MEMO_RECOVERY", ResourceType.MENU_BAR);
      this.bookmarksToolStripMenuItem.Text = this.GetResource("Bookmarks", "BOOKMARKS", ResourceType.MENU_BAR);
      this.addBookmarksToolStripMenuItem.Text = this.GetResource("Add Bookmarks", "ADD_BOOKMARKS", ResourceType.MENU_BAR);
      this.searchToolStripMenuItem.Text = this.GetResource("Search", "SEARCH", ResourceType.MENU_BAR);
      this.pageNameToolStripMenuItem.Text = this.GetResource("Page Name …", "PAGENAME_SETTINGS", ResourceType.MENU_BAR);
      this.partNameToolStripMenuItem.Text = this.GetResource("Part Name …", "PARTNAME_SETTINGS", ResourceType.MENU_BAR);
      this.partNumberToolStripMenuItem.Text = this.GetResource("Part Number …", "PARTNUMBER_SETTINGS", ResourceType.MENU_BAR);
      this.textSearchToolStripMenuItem.Text = this.GetResource("Text Search …", "TEXT_SEARCH", ResourceType.MENU_BAR);
      this.textSearceNameSearchSettingsToolStripMenuItem.Text = this.GetResource("Text Search …", "TEXTSEARCH_SETTINGS", ResourceType.MENU_BAR);
      this.singleBookToolStripMenuItem.Text = this.GetResource("Single Book Download …", "SINGLE_BOOK_DOWNLOAD", ResourceType.MENU_BAR);
      this.multipleBooksToolStripMenuItem.Text = this.GetResource("Multiple Books Download …", "MULTIPLE_BOOKS_DOWNLOAD", ResourceType.MENU_BAR);
      this.helpToolStripMenuItem.Text = this.GetResource("Help", "HELP", ResourceType.MENU_BAR);
      this.aboutGSPcLocalToolStripMenuItem.Text = this.GetResource("About GSPcLocal", "ABOUT_GSPCLOCAL", ResourceType.MENU_BAR);
      this.gSPcLocalHelpToolStripMenuItem.Text = this.GetResource("GSPcLocal Help", "GSPCLOCALHELP", ResourceType.MENU_BAR);
      this.manageDiskSpaceToolStripMenuItem.Text = this.GetResource("Manage Disk Space …", "MANAGE_DISK_SPACE", ResourceType.MENU_BAR);
      this.goToPortalToolStripMenuItem.Text = this.GetResource("Go To Portal …", "GO_TO_PORTAL", ResourceType.MENU_BAR);
      this.navigationToolStripMenuItem.Text = this.GetResource("Navigation", "NAVIGATION", ResourceType.MENU_BAR);
      this.firstPageToolStripMenuItem.Text = this.GetResource("First Page", "FIRST_PAGE", ResourceType.MENU_BAR);
      this.previousPageToolStripMenuItem.Text = this.GetResource("Previous Page", "PREVIOUS_PAGE", ResourceType.MENU_BAR);
      this.nextPageToolStripMenuItem.Text = this.GetResource("Next Page", "NEXT_PAGE", ResourceType.MENU_BAR);
      this.lastPageToolStripMenuItem.Text = this.GetResource("Last Page", "LAST_PAGE", ResourceType.MENU_BAR);
      this.previousViewToolStripMenuItem.Text = this.GetResource("Previous View", "PREVIOUS_VIEW", ResourceType.MENU_BAR);
      this.nextViewToolStripMenuItem.Text = this.GetResource("Next View", "NEXT_VIEW", ResourceType.MENU_BAR);
      this.memoDetailsToolStripMenuItem.Text = this.GetResource("Memos", "MEMOS", ResourceType.MENU_BAR);
      this.addPictureMemoToolStripMenuItem.Text = this.GetResource("Add Picture Memo …", "ADD_PICTURE_MEMO", ResourceType.MENU_BAR);
      this.addPartMemoToolStripMenuItem.Text = this.GetResource("Add Part Memo …", "ADD_PART_MEMO", ResourceType.MENU_BAR);
      this.viewMemoListToolStripMenuItem.Text = this.GetResource("View Memo List …", "VIEW_MEMO_LIST", ResourceType.MENU_BAR);
      this.advancedSearchToolStripMenuItem.Text = this.GetResource("Advanced Search …", "ADVANCED_SEARCH", ResourceType.MENU_BAR);
      this.partsListSettingsToolStripMenuItem.Text = this.GetResource("Parts List Settings …", "PARTSLIST_SETTINGS", ResourceType.MENU_BAR);
      this.selectionListSettingsToolStripMenuItem.Text = this.GetResource("Selection List Settings …", "SELECTIONLIST_SETTINGS", ResourceType.MENU_BAR);
      this.advanceSearchSettingsToolStripMenuItem.Text = this.GetResource("Advanced Search …", "ADVANCEDSEARCH_SETTINGS", ResourceType.MENU_BAR);
      this.toolsToolStripMenuItem.Text = this.GetResource("Tools", "TOOLS", ResourceType.MENU_BAR);
      this.addOnToolStripMenuItem.Text = this.GetResource("Add On", "ADD_ON", ResourceType.MENU_BAR);
      this.tsbPortal.Text = this.GetResource("Go To Portal …", "GO_TO_PORTAL", ResourceType.TOOLSTRIP);
      this.tsbHistoryBack.Text = this.GetResource("Backward", "BACKWARD", ResourceType.TOOLSTRIP);
      this.tsbHistoryForward.Text = this.GetResource("Forward", "FORWARD", ResourceType.TOOLSTRIP);
      this.tsbNavigateFirst.Text = this.GetResource("First Page", "FIRST_PAGE", ResourceType.TOOLSTRIP);
      this.tsbNavigatePrevious.Text = this.GetResource("Previous Page", "PREVIOUS_PAGE", ResourceType.TOOLSTRIP);
      this.tsbNavigateNext.Text = this.GetResource("Next Page", "NEXT_PAGE", ResourceType.TOOLSTRIP);
      this.tsbNavigateLast.Text = this.GetResource("Last Page", "LAST_PAGE", ResourceType.TOOLSTRIP);
      this.tsbOpenBook.Text = this.GetResource("Open New Book", "OPEN_NEW_BOOK", ResourceType.TOOLSTRIP);
      this.tsbPrint.Text = this.GetResource("Print", "PRINT", ResourceType.TOOLSTRIP);
      this.tsbAddBookmarks.Text = this.GetResource("Add Bookmarks", "ADD_BOOKMARKS", ResourceType.TOOLSTRIP);
      this.tsbMemoList.Text = this.GetResource("View Memo List", "VIEW_MEMO_LIST", ResourceType.TOOLSTRIP);
      this.tsbConfiguration.Text = this.GetResource("Configuration", "CONFIGURATION", ResourceType.TOOLSTRIP);
      this.tsbConnection.Text = this.GetResource("Connection Settings", "CONNECTION_SETTINGS", ResourceType.TOOLSTRIP);
      this.tsbAbout.Text = this.GetResource("About GSPcLocal", "ABOUT_GSPCLOCAL", ResourceType.TOOLSTRIP);
      this.tsbHelp.Text = this.GetResource("GSPcLocal Help", "GSPCLOCAL_HELP", ResourceType.TOOLSTRIP);
      this.tsbViewContents.Text = this.GetResource("View Contents", "VIEW_CONTENTS", ResourceType.TOOLSTRIP);
      this.tsbViewPicture.Text = this.GetResource("View Picture", "VIEW_PICTURE", ResourceType.TOOLSTRIP);
      this.tsbViewPartslist.Text = this.GetResource("View PartsList", "VIEW_PARTSLIST", ResourceType.TOOLSTRIP);
      this.tsbViewInfo.Text = this.GetResource("View Information", "VIEW_INFORMATION", ResourceType.TOOLSTRIP);
      this.tsbSearchPageName.Text = this.GetResource("Page Name Search", "PAGE_NAME_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbSearchPartName.Text = this.GetResource("Part Name Search", "PART_NAME_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbSearchPartNumber.Text = this.GetResource("Part Number Search", "PART_NUMBER_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbSearchPartAdvance.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbThirdPartyBasket.Text = this.GetResource("Basket", "BASKET", ResourceType.TOOLSTRIP);
      this.tsbSearchText.Text = this.GetResource("Text Search", "TEXT_SEARCH", ResourceType.TOOLSTRIP);
      this.tslPic.Text = this.GetResource("Picture", "PICTURE", ResourceType.TOOLSTRIP);
      this.tsbPicPrev.Text = this.GetResource("Previous Picture", "PREVIOUS_PICTURE", ResourceType.TOOLSTRIP);
      this.tsbPicNext.Text = this.GetResource("Next Picture", "NEXT_PICTURE", ResourceType.TOOLSTRIP);
      this.tsbHistoryList.Text = this.GetResource("History List", "HISTORY_LIST", ResourceType.TOOLSTRIP);
      this.tsbDataCleanup.Text = this.GetResource("Manage Disk Space", "MANAGE_DISK_SPACE", ResourceType.TOOLSTRIP);
      this.tsbRestoreDefaults.Text = this.GetResource("Restore Defaults", "RESTORE_DEFAULTS", ResourceType.TOOLSTRIP);
      this.tsbMemoRecovery.Text = this.GetResource("Recover Memos", "RECOVER_MEMOS", ResourceType.TOOLSTRIP);
      this.tsbSingleBookDownload.Text = this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.TOOLSTRIP);
      this.tsbMultipleBooksDownload.Text = this.GetResource("Multiple Books Download", "MULTIPLE_BOOKS_DOWNLOAD", ResourceType.TOOLSTRIP);
      this.tsbAddPictureMemo.Text = this.GetResource("Add Picture Memo", "ADD_PICTURE_MEMO", ResourceType.TOOLSTRIP);
      this.tsbPicZoomIn.Text = this.GetResource("Zoom In", "ZOOM_IN", ResourceType.TOOLSTRIP);
      this.tsbPicZoomOut.Text = this.GetResource("Zoom Out", "ZOOM_OUT", ResourceType.TOOLSTRIP);
      this.tsbPicZoomSelect.Text = this.GetResource("Select Zoom", "SELECT_ZOOM", ResourceType.TOOLSTRIP);
      this.tsbPicCopy.Text = this.GetResource("Copy Image", "COPY_IMAGE", ResourceType.TOOLSTRIP);
      this.tsbFitPage.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.TOOLSTRIP);
      this.tsbPicPanMode.Text = this.GetResource("Pan Mode", "PAN_MODE", ResourceType.TOOLSTRIP);
      this.tsBRotateLeft.Text = this.GetResource("Rotate Left", "ROTATE_LEFT", ResourceType.TOOLSTRIP);
      this.tsBRotateRight.Text = this.GetResource("Rotate Right", "ROTATE_RIGHT", ResourceType.TOOLSTRIP);
      this.tsbPicSelectText.Text = this.GetResource("Select Text", "SELECT_TEXT", ResourceType.TOOLSTRIP);
      this.tsbThumbnail.Text = this.GetResource("Show Thumbnail", "SHOW_THUMBNAIL", ResourceType.TOOLSTRIP);
      this.tsbFindText.Text = this.GetResource("Find Text", "FIND_TEXT", ResourceType.TOOLSTRIP);
      this.lblStatus.Text = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
      this.workOffLineMenuItem.Text = this.GetResource("Work Offline", "WORK_OFFLINE", ResourceType.TOOLSTRIP);
      this.miniMapToolStripMenuItem.Text = this.GetResource("Mini Map", "MINI_MAP", ResourceType.MENU_BAR);
      this.UpdateViewerTitle();
    }

    private void englishToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.LoadXML("English");
      this.objFrmInfo.LoadResources();
      this.objFrmPartlist.LoadResources();
      this.objFrmPicture.LoadResources();
      this.objFrmSelectionlist.LoadResources();
      this.objFrmTreeview.LoadResources();
      this.ResetBookMarkTooltipLanguage();
    }

    public void UpdateViewerTitle()
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new frmViewer.UpdateViewerTitleDelegate(this.UpdateViewerTitle));
      }
      else
      {
        string str1 = " : ";
        string empty1 = string.Empty;
        string index1 = string.Empty;
        string empty2 = string.Empty;
        string index2 = string.Empty;
        string empty3 = string.Empty;
        try
        {
          string str2 = Program.iniServers[this.ServerId].items["SETTINGS", "DISPLAY_NAME"];
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.SchemaNode.Attributes)
          {
            if (attribute.Value != null && attribute.Value.ToUpper() == "DESCRIPTION1")
              index1 = attribute.Name;
            if (attribute.Value != null && attribute.Value.ToUpper() == "DESCRIPTION2")
              index2 = attribute.Name;
          }
          if (this.BookNode.Attributes[index1] != null)
            empty1 = this.BookNode.Attributes[index1].Value;
          if (this.BookNode.Attributes[index2] != null)
            empty2 = this.BookNode.Attributes[index2].Value;
          if (empty1 != string.Empty && empty2 != string.Empty && (this.sBookType != string.Empty && this.BookPublishingId != string.Empty) && this.sFirstPageTitle != string.Empty)
            str2 = str2 + str1 + this.sBookType + str1 + empty1 + str1 + empty2 + " ( " + this.BookPublishingId + str1 + this.sFirstPageTitle + " )";
          else if ((empty1 == string.Empty || empty2 == string.Empty) && (this.sBookType != string.Empty && this.BookPublishingId != string.Empty) && this.sFirstPageTitle != string.Empty)
            str2 = str2 + str1 + this.sBookType + " ( " + this.BookPublishingId + str1 + this.sFirstPageTitle + " )";
          this.Text = str2;
        }
        catch
        {
          this.Text = this.GetResource("GSPcLocal Viewer 3.0", "GSPcLocal Viewer 3.0", ResourceType.TITLE);
        }
      }
    }

    public void miniMapToolStripChkUnchk(bool chkMiniMap)
    {
      if (chkMiniMap)
      {
        this.miniMapToolStripMenuItem.Checked = true;
        if (!this.miniMapToolStripMenuItem.Enabled)
          return;
        frmViewer.MiniMapChk = true;
      }
      else
      {
        this.miniMapToolStripMenuItem.Checked = false;
        if (!this.miniMapToolStripMenuItem.Enabled)
          return;
        frmViewer.MiniMapChk = false;
      }
    }

    public string[] getFilterArgs()
    {
      return this.p_ArgsF;
    }

    public void ApplyPrintSettings(bool bSLColumnsVisible)
    {
      try
      {
        this.bIsSelectionListPrint = bSLColumnsVisible;
        this.printSelectionListToolStripMenuItem.Enabled = bSLColumnsVisible;
      }
      catch
      {
      }
    }

    private bool IsValidDateTime(string strInput)
    {
      try
      {
        DateTime.ParseExact(strInput.Trim(), "dd/MM/yyyy HH:mm", (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private void GetPicMemoDeleteValue()
    {
      try
      {
        string str = Application.StartupPath + "\\GSP_" + Program.iniServers[this.p_ServerId].sIniKey + ".ini";
        ArrayList arrayList = new ArrayList();
        ArrayList keys = new IniFileIO().GetKeys(str, "PIC_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          string keyValue = new IniFileIO().GetKeyValue("PIC_SETTINGS", keys[index].ToString(), str);
          if (keys[index].ToString() == "LOCAL_TEXT_MEM_DELETE")
          {
            if (keyValue.ToString().ToUpper() == "ON")
            {
              this.bPicMemoDelete = true;
              break;
            }
            if (!(keyValue.ToString().ToUpper() == "OFF"))
              break;
            this.bPicMemoDelete = false;
            break;
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void DeletePicLocalMemos()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string empty3 = string.Empty;
        string str1 = "GSPcLocalViewer";
        string str2 = folderPath + "\\" + str1 + "\\" + Program.iniServers[this.p_ServerId].sIniKey + "\\LocalMemo.xml";
        if (!File.Exists(str2))
          return;
        xmlDocument.Load(str2);
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Memos/Memo");
        if (xmlNodeList.Count > 0)
        {
          foreach (XmlNode oldChild in xmlNodeList)
          {
            XmlAttribute attribute = oldChild.Attributes["PartNo"];
            if (oldChild.Attributes["BookId"].Value.ToString() == this.p_BookId && attribute.Value.ToString() == "" && oldChild.Attributes["Type"].Value.ToString().ToUpper() == "TXT")
              oldChild.ParentNode.RemoveChild(oldChild);
          }
        }
        xmlDocument.Save(str2);
      }
      catch (Exception ex)
      {
      }
    }

    private int GetMemoType()
    {
      try
      {
        return Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"] != null && Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"].ToString() == "2" ? 2 : 1;
      }
      catch (Exception ex)
      {
        return 1;
      }
    }

    public string GetMemoSortType()
    {
      try
      {
        if (Program.iniServers[this.p_ServerId].items["MEMO_SETTING", "MEMO_SORT"] == null)
          return "UNKNOWN";
        if (Program.iniServers[this.p_ServerId].items["MEMO_SETTING", "MEMO_SORT"].ToString().ToUpper() == "DESC")
          return "DESC";
        return Program.iniServers[this.p_ServerId].items["MEMO_SETTING", "MEMO_SORT"].ToString().ToUpper() == "ASC" ? "ASC" : "UNKNOWN";
      }
      catch (Exception ex)
      {
        return "UNKNOWN";
      }
    }

    private void UpdateMemoDictionary(SortedDictionary<DateTime, XmlNode> dic, DateTime dt, XmlNode xNode)
    {
      try
      {
        dic.Add(dt, xNode);
      }
      catch (Exception ex)
      {
        dt = dt.AddSeconds(1.0);
        this.UpdateMemoDictionary(dic, dt, xNode);
      }
    }

    private void GetSaveMemoValue()
    {
      try
      {
        if (Program.iniServers[this.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"] == null || !(Program.iniServers[this.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"].ToString().ToUpper() == "ON"))
          return;
        this.bSaveMemoOnBookLevel = true;
      }
      catch (Exception ex)
      {
        this.bSaveMemoOnBookLevel = false;
      }
    }

    private int GetPlPartNumColIndex()
    {
      int num = 0;
      for (int index = 0; index < this.objFrmPartlist.dgPartslist.Columns.Count; ++index)
      {
        if (this.objFrmPartlist.dgPartslist.Columns[index].Tag != null && this.objFrmPartlist.dgPartslist.Columns[index].Tag.ToString() == "PartNumber")
          num = index;
      }
      return num;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmViewer));
      DockPanelSkin dockPanelSkin = new DockPanelSkin();
      AutoHideStripSkin autoHideStripSkin = new AutoHideStripSkin();
      DockPanelGradient dockPanelGradient1 = new DockPanelGradient();
      TabGradient tabGradient1 = new TabGradient();
      DockPaneStripSkin dockPaneStripSkin = new DockPaneStripSkin();
      DockPaneStripGradient paneStripGradient = new DockPaneStripGradient();
      TabGradient tabGradient2 = new TabGradient();
      DockPanelGradient dockPanelGradient2 = new DockPanelGradient();
      TabGradient tabGradient3 = new TabGradient();
      DockPaneStripToolWindowGradient toolWindowGradient = new DockPaneStripToolWindowGradient();
      TabGradient tabGradient4 = new TabGradient();
      TabGradient tabGradient5 = new TabGradient();
      DockPanelGradient dockPanelGradient3 = new DockPanelGradient();
      TabGradient tabGradient6 = new TabGradient();
      TabGradient tabGradient7 = new TabGradient();
      this.toolStripContainer1 = new ToolStripContainer();
      this.ssStatus = new StatusStrip();
      this.lblStatus = new ToolStripStatusLabel();
      this.lblMode = new ToolStripStatusLabel();
      this.pnlForm = new Panel();
      this.objDocPanel = new DockPanel();
      this.picDisable = new PictureBox();
      this.pnlForm2 = new Panel();
      this.menuStrip1 = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.openToolStripMenuItem = new ToolStripMenuItem();
      this.goToPortalToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator11 = new ToolStripSeparator();
      this.printToolStripMenuItem = new ToolStripMenuItem();
      this.printPageToolStripMenuItem = new ToolStripMenuItem();
      this.printPictureToolStripMenuItem = new ToolStripMenuItem();
      this.printListToolStripMenuItem = new ToolStripMenuItem();
      this.printSelectionListToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.workOffLineMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator21 = new ToolStripSeparator();
      this.closeToolStripMenuItem = new ToolStripMenuItem();
      this.closeAllToolStripMenuItem = new ToolStripMenuItem();
      this.navigationToolStripMenuItem = new ToolStripMenuItem();
      this.firstPageToolStripMenuItem = new ToolStripMenuItem();
      this.previousPageToolStripMenuItem = new ToolStripMenuItem();
      this.nextPageToolStripMenuItem = new ToolStripMenuItem();
      this.lastPageToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator12 = new ToolStripSeparator();
      this.previousViewToolStripMenuItem = new ToolStripMenuItem();
      this.nextViewToolStripMenuItem = new ToolStripMenuItem();
      this.viewToolStripMenuItem = new ToolStripMenuItem();
      this.contentsToolStripMenuItem = new ToolStripMenuItem();
      this.pictureToolStripMenuItem = new ToolStripMenuItem();
      this.miniMapToolStripMenuItem = new ToolStripMenuItem();
      this.partslistToolStripMenuItem = new ToolStripMenuItem();
      this.selectionListToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.informationToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.restoreDefaultsToolStripMenuItem = new ToolStripMenuItem();
      this.saveDefaultsToolStripMenuItem = new ToolStripMenuItem();
      this.bookmarksToolStripMenuItem = new ToolStripMenuItem();
      this.addBookmarksToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator = new ToolStripSeparator();
      this.memoDetailsToolStripMenuItem = new ToolStripMenuItem();
      this.addPictureMemoToolStripMenuItem = new ToolStripMenuItem();
      this.addPartMemoToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator16 = new ToolStripSeparator();
      this.viewMemoListToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator13 = new ToolStripSeparator();
      this.memoRecoveryToolStripMenuItem = new ToolStripMenuItem();
      this.searchToolStripMenuItem = new ToolStripMenuItem();
      this.pageNameToolStripMenuItem = new ToolStripMenuItem();
      this.textSearchToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.partNameToolStripMenuItem = new ToolStripMenuItem();
      this.partNumberToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator6 = new ToolStripSeparator();
      this.advancedSearchToolStripMenuItem = new ToolStripMenuItem();
      this.settingsToolStripMenuItem = new ToolStripMenuItem();
      this.fontToolStripMenuItem = new ToolStripMenuItem();
      this.ColorToolStripMenuItem = new ToolStripMenuItem();
      this.generalToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator14 = new ToolStripSeparator();
      this.memoSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator23 = new ToolStripSeparator();
      this.partsListSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.selectionListSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator9 = new ToolStripSeparator();
      this.pageNameSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.textSearceNameSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.partNameSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.partNumberSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.advanceSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator15 = new ToolStripSeparator();
      this.manageDiskSpaceToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.connectionToolStripMenuItem = new ToolStripMenuItem();
      this.toolsToolStripMenuItem = new ToolStripMenuItem();
      this.singleBookToolStripMenuItem = new ToolStripMenuItem();
      this.multipleBooksToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.gSPcLocalHelpToolStripMenuItem = new ToolStripMenuItem();
      this.aboutGSPcLocalToolStripMenuItem = new ToolStripMenuItem();
      this.addOnToolStripMenuItem = new ToolStripMenuItem();
      this.tsPortal = new ToolStrip();
      this.tsbPortal = new ToolStripButton();
      this.tsbOpenBook = new ToolStripButton();
      this.tsHistory = new ToolStrip();
      this.tsbHistoryBack = new ToolStripButton();
      this.tsbHistoryForward = new ToolStripButton();
      this.tsbHistoryList = new ToolStripDropDownButton();
      this.tsNavigate = new ToolStrip();
      this.tsbNavigateFirst = new ToolStripButton();
      this.toolStripSeparator7 = new ToolStripSeparator();
      this.tsbNavigatePrevious = new ToolStripButton();
      this.tsbNavigateNext = new ToolStripButton();
      this.toolStripSeparator8 = new ToolStripSeparator();
      this.tsbNavigateLast = new ToolStripButton();
      this.tsView = new ToolStrip();
      this.tsbViewContents = new ToolStripButton();
      this.tsbViewPicture = new ToolStripButton();
      this.tsbViewPartslist = new ToolStripButton();
      this.tsbViewSeparator = new ToolStripSeparator();
      this.tsbViewInfo = new ToolStripButton();
      this.toolStripSeparator17 = new ToolStripSeparator();
      this.tsbRestoreDefaults = new ToolStripButton();
      this.tsSearch = new ToolStrip();
      this.tsbSearchPageName = new ToolStripButton();
      this.tsbSearchText = new ToolStripButton();
      this.tsbSearchPartName = new ToolStripButton();
      this.tsbSearchPartNumber = new ToolStripButton();
      this.tsbSearchPartAdvance = new ToolStripButton();
      this.tsTools = new ToolStrip();
      this.tsbSingleBookDownload = new ToolStripButton();
      this.tsbMultipleBooksDownload = new ToolStripButton();
      this.toolStripSeparator18 = new ToolStripSeparator();
      this.tsbDataCleanup = new ToolStripButton();
      this.tsbConnection = new ToolStripButton();
      this.tsFunctions = new ToolStrip();
      this.tsbPrint = new ToolStripButton();
      this.tsbAddBookmarks = new ToolStripButton();
      this.tsbMemoList = new ToolStripButton();
      this.tsbMemoRecovery = new ToolStripButton();
      this.tsbConfiguration = new ToolStripButton();
      this.tsbThirdPartyBasket = new ToolStripButton();
      this.tsbAbout = new ToolStripButton();
      this.tsbHelp = new ToolStripButton();
      this.tsPic = new ToolStrip();
      this.tslPic = new ToolStripLabel();
      this.tsbPicPrev = new ToolStripButton();
      this.tstPicNo = new ToolStripTextBox();
      this.tsbPicNext = new ToolStripButton();
      this.toolStripSeparator10 = new ToolStripSeparator();
      this.tsbFindText = new ToolStripButton();
      this.tsbPicPanMode = new ToolStripButton();
      this.tsbPicZoomSelect = new ToolStripButton();
      this.tsbFitPage = new ToolStripButton();
      this.tsbPicCopy = new ToolStripButton();
      this.tsbPicSelectText = new ToolStripButton();
      this.toolStripSeparator19 = new ToolStripSeparator();
      this.tsbPicZoomIn = new ToolStripButton();
      this.tsbPicZoomOut = new ToolStripButton();
      this.toolStripSeparator22 = new ToolStripSeparator();
      this.tsBRotateLeft = new ToolStripButton();
      this.tsBRotateRight = new ToolStripButton();
      this.toolStripSeparator20 = new ToolStripSeparator();
      this.tsbAddPictureMemo = new ToolStripButton();
      this.tsbThumbnail = new ToolStripButton();
      this.bgWorker = new BackgroundWorker();
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.ssStatus.SuspendLayout();
      this.pnlForm.SuspendLayout();
      ((ISupportInitialize) this.picDisable).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.tsPortal.SuspendLayout();
      this.tsHistory.SuspendLayout();
      this.tsNavigate.SuspendLayout();
      this.tsView.SuspendLayout();
      this.tsSearch.SuspendLayout();
      this.tsTools.SuspendLayout();
      this.tsFunctions.SuspendLayout();
      this.tsPic.SuspendLayout();
      this.SuspendLayout();
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add((Control) this.ssStatus);
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.pnlForm);
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.pnlForm2);
      this.toolStripContainer1.ContentPanel.Size = new Size(1161, 369);
      this.toolStripContainer1.Dock = DockStyle.Fill;
      this.toolStripContainer1.Location = new Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new Size(1161, 440);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.menuStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsNavigate);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsPic);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsPortal);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsHistory);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsView);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsSearch);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsTools);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsFunctions);
      this.ssStatus.Dock = DockStyle.None;
      this.ssStatus.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.lblStatus,
        (ToolStripItem) this.lblMode
      });
      this.ssStatus.Location = new Point(0, 0);
      this.ssStatus.Name = "ssStatus";
      this.ssStatus.Size = new Size(1161, 22);
      this.ssStatus.TabIndex = 1;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(1130, 17);
      this.lblStatus.Spring = true;
      this.lblStatus.Text = "Ready";
      this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
      this.lblMode.AccessibleRole = AccessibleRole.SpinButton;
      this.lblMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.lblMode.Image = (Image) componentResourceManager.GetObject("lblMode.Image");
      this.lblMode.ImageTransparentColor = Color.White;
      this.lblMode.Name = "lblMode";
      this.lblMode.Size = new Size(16, 17);
      this.lblMode.Text = "toolStripStatusLabel1";
      this.lblMode.ToolTipText = "change mode";
      this.lblMode.Click += new EventHandler(this.lblMode_Click);
      this.pnlForm.BackColor = SystemColors.Control;
      this.pnlForm.Controls.Add((Control) this.objDocPanel);
      this.pnlForm.Controls.Add((Control) this.picDisable);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(2);
      this.pnlForm.Size = new Size(1161, 369);
      this.pnlForm.TabIndex = 2;
      this.objDocPanel.ActiveAutoHideContent = (IDockContent) null;
      this.objDocPanel.BackColor = SystemColors.Control;
      this.objDocPanel.Dock = DockStyle.Fill;
      this.objDocPanel.DockBackColor = SystemColors.ControlDark;
      this.objDocPanel.DocumentStyle = DocumentStyle.DockingWindow;
      this.objDocPanel.Location = new Point(2, 2);
      this.objDocPanel.Name = "objDocPanel";
      this.objDocPanel.Size = new Size(1157, 365);
      dockPanelGradient1.EndColor = SystemColors.ControlLight;
      dockPanelGradient1.StartColor = SystemColors.ControlLight;
      autoHideStripSkin.DockStripGradient = dockPanelGradient1;
      tabGradient1.EndColor = SystemColors.Control;
      tabGradient1.StartColor = SystemColors.Control;
      tabGradient1.TextColor = SystemColors.ControlDarkDark;
      autoHideStripSkin.TabGradient = tabGradient1;
      dockPanelSkin.AutoHideStripSkin = autoHideStripSkin;
      tabGradient2.EndColor = SystemColors.ControlLightLight;
      tabGradient2.StartColor = SystemColors.ControlLightLight;
      tabGradient2.TextColor = SystemColors.ControlText;
      paneStripGradient.ActiveTabGradient = tabGradient2;
      dockPanelGradient2.EndColor = SystemColors.Control;
      dockPanelGradient2.StartColor = SystemColors.Control;
      paneStripGradient.DockStripGradient = dockPanelGradient2;
      tabGradient3.EndColor = SystemColors.ControlLight;
      tabGradient3.StartColor = SystemColors.ControlLight;
      tabGradient3.TextColor = SystemColors.ControlText;
      paneStripGradient.InactiveTabGradient = tabGradient3;
      dockPaneStripSkin.DocumentGradient = paneStripGradient;
      tabGradient4.EndColor = SystemColors.ActiveCaption;
      tabGradient4.LinearGradientMode = LinearGradientMode.Vertical;
      tabGradient4.StartColor = SystemColors.GradientActiveCaption;
      tabGradient4.TextColor = SystemColors.ActiveCaptionText;
      toolWindowGradient.ActiveCaptionGradient = tabGradient4;
      tabGradient5.EndColor = SystemColors.Control;
      tabGradient5.StartColor = SystemColors.Control;
      tabGradient5.TextColor = SystemColors.ControlText;
      toolWindowGradient.ActiveTabGradient = tabGradient5;
      dockPanelGradient3.EndColor = SystemColors.ControlLight;
      dockPanelGradient3.StartColor = SystemColors.ControlLight;
      toolWindowGradient.DockStripGradient = dockPanelGradient3;
      tabGradient6.EndColor = SystemColors.GradientInactiveCaption;
      tabGradient6.LinearGradientMode = LinearGradientMode.Vertical;
      tabGradient6.StartColor = SystemColors.GradientInactiveCaption;
      tabGradient6.TextColor = SystemColors.ControlText;
      toolWindowGradient.InactiveCaptionGradient = tabGradient6;
      tabGradient7.EndColor = Color.Transparent;
      tabGradient7.StartColor = Color.Transparent;
      tabGradient7.TextColor = SystemColors.ControlDarkDark;
      toolWindowGradient.InactiveTabGradient = tabGradient7;
      dockPaneStripSkin.ToolWindowGradient = toolWindowGradient;
      dockPanelSkin.DockPaneStripSkin = dockPaneStripSkin;
      this.objDocPanel.Skin = dockPanelSkin;
      this.objDocPanel.TabIndex = 2;
      this.picDisable.Dock = DockStyle.Fill;
      this.picDisable.Location = new Point(2, 2);
      this.picDisable.Name = "picDisable";
      this.picDisable.Size = new Size(1157, 365);
      this.picDisable.TabIndex = 1;
      this.picDisable.TabStop = false;
      this.pnlForm2.Dock = DockStyle.Fill;
      this.pnlForm2.Location = new Point(0, 0);
      this.pnlForm2.Name = "pnlForm2";
      this.pnlForm2.Size = new Size(1161, 369);
      this.pnlForm2.TabIndex = 1;
      this.menuStrip1.Dock = DockStyle.None;
      this.menuStrip1.Items.AddRange(new ToolStripItem[10]
      {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.navigationToolStripMenuItem,
        (ToolStripItem) this.viewToolStripMenuItem,
        (ToolStripItem) this.bookmarksToolStripMenuItem,
        (ToolStripItem) this.memoDetailsToolStripMenuItem,
        (ToolStripItem) this.searchToolStripMenuItem,
        (ToolStripItem) this.settingsToolStripMenuItem,
        (ToolStripItem) this.toolsToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem,
        (ToolStripItem) this.addOnToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(1161, 24);
      this.menuStrip1.TabIndex = 2;
      this.menuStrip1.Text = "menuStrip1";
      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[9]
      {
        (ToolStripItem) this.openToolStripMenuItem,
        (ToolStripItem) this.goToPortalToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator11,
        (ToolStripItem) this.printToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.workOffLineMenuItem,
        (ToolStripItem) this.toolStripSeparator21,
        (ToolStripItem) this.closeToolStripMenuItem,
        (ToolStripItem) this.closeAllToolStripMenuItem
      });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.Size = new Size(141, 22);
      this.openToolStripMenuItem.Text = "Open ...";
      this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
      this.goToPortalToolStripMenuItem.Name = "goToPortalToolStripMenuItem";
      this.goToPortalToolStripMenuItem.Size = new Size(141, 22);
      this.goToPortalToolStripMenuItem.Text = "Go to Portal";
      this.goToPortalToolStripMenuItem.Click += new EventHandler(this.goToPortalToolStripMenuItem_Click);
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new Size(138, 6);
      this.printToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.printPageToolStripMenuItem,
        (ToolStripItem) this.printPictureToolStripMenuItem,
        (ToolStripItem) this.printListToolStripMenuItem,
        (ToolStripItem) this.printSelectionListToolStripMenuItem
      });
      this.printToolStripMenuItem.Name = "printToolStripMenuItem";
      this.printToolStripMenuItem.Size = new Size(141, 22);
      this.printToolStripMenuItem.Text = "Print";
      this.printToolStripMenuItem.Click += new EventHandler(this.printToolStripMenuItem_Click);
      this.printPageToolStripMenuItem.Name = "printPageToolStripMenuItem";
      this.printPageToolStripMenuItem.Size = new Size(180, 22);
      this.printPageToolStripMenuItem.Text = "Print Page...";
      this.printPageToolStripMenuItem.Click += new EventHandler(this.printPageToolStripMenuItem_Click);
      this.printPictureToolStripMenuItem.Name = "printPictureToolStripMenuItem";
      this.printPictureToolStripMenuItem.Size = new Size(180, 22);
      this.printPictureToolStripMenuItem.Text = "Print Picture...";
      this.printPictureToolStripMenuItem.Click += new EventHandler(this.printPictureToolStripMenuItem_Click);
      this.printListToolStripMenuItem.Name = "printListToolStripMenuItem";
      this.printListToolStripMenuItem.Size = new Size(180, 22);
      this.printListToolStripMenuItem.Text = "Print List...";
      this.printListToolStripMenuItem.Click += new EventHandler(this.printListToolStripMenuItem_Click);
      this.printSelectionListToolStripMenuItem.Name = "printSelectionListToolStripMenuItem";
      this.printSelectionListToolStripMenuItem.Size = new Size(180, 22);
      this.printSelectionListToolStripMenuItem.Text = "Print Selection List...";
      this.printSelectionListToolStripMenuItem.Click += new EventHandler(this.printSelectionListToolStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(138, 6);
      this.workOffLineMenuItem.Name = "workOffLineMenuItem";
      this.workOffLineMenuItem.Size = new Size(141, 22);
      this.workOffLineMenuItem.Text = "Work Offline";
      this.workOffLineMenuItem.Click += new EventHandler(this.workOffLineMenuItem_Click);
      this.toolStripSeparator21.Name = "toolStripSeparator21";
      this.toolStripSeparator21.Size = new Size(138, 6);
      this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
      this.closeToolStripMenuItem.Size = new Size(141, 22);
      this.closeToolStripMenuItem.Text = "Close";
      this.closeToolStripMenuItem.Click += new EventHandler(this.closeToolStripMenuItem_Click);
      this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
      this.closeAllToolStripMenuItem.Size = new Size(141, 22);
      this.closeAllToolStripMenuItem.Text = "Close All";
      this.closeAllToolStripMenuItem.Click += new EventHandler(this.closeAllToolStripMenuItem_Click);
      this.navigationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.firstPageToolStripMenuItem,
        (ToolStripItem) this.previousPageToolStripMenuItem,
        (ToolStripItem) this.nextPageToolStripMenuItem,
        (ToolStripItem) this.lastPageToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator12,
        (ToolStripItem) this.previousViewToolStripMenuItem,
        (ToolStripItem) this.nextViewToolStripMenuItem
      });
      this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
      this.navigationToolStripMenuItem.Size = new Size(77, 20);
      this.navigationToolStripMenuItem.Text = "Navigation";
      this.firstPageToolStripMenuItem.Name = "firstPageToolStripMenuItem";
      this.firstPageToolStripMenuItem.Size = new Size(148, 22);
      this.firstPageToolStripMenuItem.Text = "First Page";
      this.firstPageToolStripMenuItem.Click += new EventHandler(this.firstPageToolStripMenuItem_Click);
      this.previousPageToolStripMenuItem.Name = "previousPageToolStripMenuItem";
      this.previousPageToolStripMenuItem.Size = new Size(148, 22);
      this.previousPageToolStripMenuItem.Text = "Previous Page";
      this.previousPageToolStripMenuItem.Click += new EventHandler(this.previousPageToolStripMenuItem_Click);
      this.nextPageToolStripMenuItem.Name = "nextPageToolStripMenuItem";
      this.nextPageToolStripMenuItem.Size = new Size(148, 22);
      this.nextPageToolStripMenuItem.Text = "Next Page";
      this.nextPageToolStripMenuItem.Click += new EventHandler(this.nextPageToolStripMenuItem_Click);
      this.lastPageToolStripMenuItem.Name = "lastPageToolStripMenuItem";
      this.lastPageToolStripMenuItem.Size = new Size(148, 22);
      this.lastPageToolStripMenuItem.Text = "Last Page";
      this.lastPageToolStripMenuItem.Click += new EventHandler(this.lastPageToolStripMenuItem_Click);
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new Size(145, 6);
      this.previousViewToolStripMenuItem.Name = "previousViewToolStripMenuItem";
      this.previousViewToolStripMenuItem.Size = new Size(148, 22);
      this.previousViewToolStripMenuItem.Text = "Previous View";
      this.previousViewToolStripMenuItem.Click += new EventHandler(this.previousViewToolStripMenuItem_Click);
      this.nextViewToolStripMenuItem.Name = "nextViewToolStripMenuItem";
      this.nextViewToolStripMenuItem.Size = new Size(148, 22);
      this.nextViewToolStripMenuItem.Text = "Next View";
      this.nextViewToolStripMenuItem.Click += new EventHandler(this.nextViewToolStripMenuItem_Click);
      this.viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[10]
      {
        (ToolStripItem) this.contentsToolStripMenuItem,
        (ToolStripItem) this.pictureToolStripMenuItem,
        (ToolStripItem) this.miniMapToolStripMenuItem,
        (ToolStripItem) this.partslistToolStripMenuItem,
        (ToolStripItem) this.selectionListToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.informationToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.restoreDefaultsToolStripMenuItem,
        (ToolStripItem) this.saveDefaultsToolStripMenuItem
      });
      this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
      this.viewToolStripMenuItem.Size = new Size(44, 20);
      this.viewToolStripMenuItem.Text = "View";
      this.contentsToolStripMenuItem.Checked = true;
      this.contentsToolStripMenuItem.CheckState = CheckState.Checked;
      this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
      this.contentsToolStripMenuItem.ShowShortcutKeys = false;
      this.contentsToolStripMenuItem.Size = new Size(159, 22);
      this.contentsToolStripMenuItem.Text = "Contents";
      this.contentsToolStripMenuItem.Click += new EventHandler(this.contentsToolStripMenuItem_Click);
      this.pictureToolStripMenuItem.Checked = true;
      this.pictureToolStripMenuItem.CheckState = CheckState.Checked;
      this.pictureToolStripMenuItem.Name = "pictureToolStripMenuItem";
      this.pictureToolStripMenuItem.Size = new Size(159, 22);
      this.pictureToolStripMenuItem.Text = "Picture";
      this.pictureToolStripMenuItem.Click += new EventHandler(this.pictureToolStripMenuItem_Click);
      this.miniMapToolStripMenuItem.Name = "miniMapToolStripMenuItem";
      this.miniMapToolStripMenuItem.Size = new Size(159, 22);
      this.miniMapToolStripMenuItem.Text = "Mini Map";
      this.miniMapToolStripMenuItem.Click += new EventHandler(this.miniMapToolStripMenuItem_Click);
      this.partslistToolStripMenuItem.Checked = true;
      this.partslistToolStripMenuItem.CheckState = CheckState.Checked;
      this.partslistToolStripMenuItem.Name = "partslistToolStripMenuItem";
      this.partslistToolStripMenuItem.Size = new Size(159, 22);
      this.partslistToolStripMenuItem.Text = "Partslist";
      this.partslistToolStripMenuItem.Click += new EventHandler(this.partslistToolStripMenuItem_Click);
      this.selectionListToolStripMenuItem.Checked = true;
      this.selectionListToolStripMenuItem.CheckState = CheckState.Checked;
      this.selectionListToolStripMenuItem.Name = "selectionListToolStripMenuItem";
      this.selectionListToolStripMenuItem.Size = new Size(159, 22);
      this.selectionListToolStripMenuItem.Text = "Selection List";
      this.selectionListToolStripMenuItem.Click += new EventHandler(this.selectionListToolStripMenuItem_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(156, 6);
      this.informationToolStripMenuItem.Checked = true;
      this.informationToolStripMenuItem.CheckState = CheckState.Checked;
      this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
      this.informationToolStripMenuItem.Size = new Size(159, 22);
      this.informationToolStripMenuItem.Text = "Information";
      this.informationToolStripMenuItem.Click += new EventHandler(this.informationToolStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(156, 6);
      this.restoreDefaultsToolStripMenuItem.Name = "restoreDefaultsToolStripMenuItem";
      this.restoreDefaultsToolStripMenuItem.Size = new Size(159, 22);
      this.restoreDefaultsToolStripMenuItem.Text = "Restore Defaults";
      this.restoreDefaultsToolStripMenuItem.Click += new EventHandler(this.restoreDefaultsToolStripMenuItem_Click);
      this.saveDefaultsToolStripMenuItem.Name = "saveDefaultsToolStripMenuItem";
      this.saveDefaultsToolStripMenuItem.Size = new Size(159, 22);
      this.saveDefaultsToolStripMenuItem.Text = "Save Defaults";
      this.saveDefaultsToolStripMenuItem.Click += new EventHandler(this.saveDefaultsToolStripMenuItem_Click);
      this.bookmarksToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.addBookmarksToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator
      });
      this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
      this.bookmarksToolStripMenuItem.Size = new Size(78, 20);
      this.bookmarksToolStripMenuItem.Text = "Bookmarks";
      this.addBookmarksToolStripMenuItem.Name = "addBookmarksToolStripMenuItem";
      this.addBookmarksToolStripMenuItem.Size = new Size(172, 22);
      this.addBookmarksToolStripMenuItem.Text = "Add to Bookmarks";
      this.addBookmarksToolStripMenuItem.Click += new EventHandler(this.AddBookmarksToolStripMenuItem_Click);
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new Size(169, 6);
      this.memoDetailsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.addPictureMemoToolStripMenuItem,
        (ToolStripItem) this.addPartMemoToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator16,
        (ToolStripItem) this.viewMemoListToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator13,
        (ToolStripItem) this.memoRecoveryToolStripMenuItem
      });
      this.memoDetailsToolStripMenuItem.Name = "memoDetailsToolStripMenuItem";
      this.memoDetailsToolStripMenuItem.Size = new Size(59, 20);
      this.memoDetailsToolStripMenuItem.Text = "Memos";
      this.memoDetailsToolStripMenuItem.Click += new EventHandler(this.memoDetailsToolStripMenuItem_Click);
      this.addPictureMemoToolStripMenuItem.Name = "addPictureMemoToolStripMenuItem";
      this.addPictureMemoToolStripMenuItem.Size = new Size(174, 22);
      this.addPictureMemoToolStripMenuItem.Text = "Add Picture Memo";
      this.addPictureMemoToolStripMenuItem.Click += new EventHandler(this.addPictureMemoToolStripMenuItem_Click);
      this.addPartMemoToolStripMenuItem.Name = "addPartMemoToolStripMenuItem";
      this.addPartMemoToolStripMenuItem.Size = new Size(174, 22);
      this.addPartMemoToolStripMenuItem.Text = "Add Part Memo";
      this.addPartMemoToolStripMenuItem.Click += new EventHandler(this.addPartMemoToolStripMenuItem_Click);
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      this.toolStripSeparator16.Size = new Size(171, 6);
      this.viewMemoListToolStripMenuItem.Name = "viewMemoListToolStripMenuItem";
      this.viewMemoListToolStripMenuItem.Size = new Size(174, 22);
      this.viewMemoListToolStripMenuItem.Text = "View Memo List...";
      this.viewMemoListToolStripMenuItem.Click += new EventHandler(this.viewMemoListToolStripMenuItem_Click);
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new Size(171, 6);
      this.memoRecoveryToolStripMenuItem.Name = "memoRecoveryToolStripMenuItem";
      this.memoRecoveryToolStripMenuItem.Size = new Size(174, 22);
      this.memoRecoveryToolStripMenuItem.Text = "Recovery ...";
      this.memoRecoveryToolStripMenuItem.Click += new EventHandler(this.memoRecoveryToolStripMenuItem_Click);
      this.searchToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.pageNameToolStripMenuItem,
        (ToolStripItem) this.textSearchToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.partNameToolStripMenuItem,
        (ToolStripItem) this.partNumberToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator6,
        (ToolStripItem) this.advancedSearchToolStripMenuItem
      });
      this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
      this.searchToolStripMenuItem.Size = new Size(54, 20);
      this.searchToolStripMenuItem.Text = "Search";
      this.pageNameToolStripMenuItem.Name = "pageNameToolStripMenuItem";
      this.pageNameToolStripMenuItem.Size = new Size(174, 22);
      this.pageNameToolStripMenuItem.Text = "Page Name ...";
      this.pageNameToolStripMenuItem.Click += new EventHandler(this.pageNameToolStripMenuItem_Click);
      this.textSearchToolStripMenuItem.Name = "textSearchToolStripMenuItem";
      this.textSearchToolStripMenuItem.Size = new Size(174, 22);
      this.textSearchToolStripMenuItem.Text = "Text Search ...";
      this.textSearchToolStripMenuItem.Click += new EventHandler(this.textSearchToolStripMenuItem_Click);
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(171, 6);
      this.partNameToolStripMenuItem.Name = "partNameToolStripMenuItem";
      this.partNameToolStripMenuItem.Size = new Size(174, 22);
      this.partNameToolStripMenuItem.Text = "Part Name ...";
      this.partNameToolStripMenuItem.Click += new EventHandler(this.partNameToolStripMenuItem_Click);
      this.partNumberToolStripMenuItem.Name = "partNumberToolStripMenuItem";
      this.partNumberToolStripMenuItem.Size = new Size(174, 22);
      this.partNumberToolStripMenuItem.Text = "Part Number ...";
      this.partNumberToolStripMenuItem.Click += new EventHandler(this.partNumberToolStripMenuItem_Click);
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new Size(171, 6);
      this.advancedSearchToolStripMenuItem.Name = "advancedSearchToolStripMenuItem";
      this.advancedSearchToolStripMenuItem.Size = new Size(174, 22);
      this.advancedSearchToolStripMenuItem.Text = "Advanced Search...";
      this.advancedSearchToolStripMenuItem.Click += new EventHandler(this.advancedSearchToolStripMenuItem_Click);
      this.settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[18]
      {
        (ToolStripItem) this.fontToolStripMenuItem,
        (ToolStripItem) this.ColorToolStripMenuItem,
        (ToolStripItem) this.generalToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator14,
        (ToolStripItem) this.memoSettingsToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator23,
        (ToolStripItem) this.partsListSettingsToolStripMenuItem,
        (ToolStripItem) this.selectionListSettingsToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator9,
        (ToolStripItem) this.pageNameSearchSettingsToolStripMenuItem,
        (ToolStripItem) this.textSearceNameSearchSettingsToolStripMenuItem,
        (ToolStripItem) this.partNameSearchSettingsToolStripMenuItem,
        (ToolStripItem) this.partNumberSearchSettingsToolStripMenuItem,
        (ToolStripItem) this.advanceSearchSettingsToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator15,
        (ToolStripItem) this.manageDiskSpaceToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.connectionToolStripMenuItem
      });
      this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
      this.settingsToolStripMenuItem.Size = new Size(61, 20);
      this.settingsToolStripMenuItem.Text = "Settings";
      this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
      this.fontToolStripMenuItem.Size = new Size(189, 22);
      this.fontToolStripMenuItem.Text = "Font ...";
      this.fontToolStripMenuItem.Click += new EventHandler(this.fontAndColorToolStripMenuItem_Click);
      this.ColorToolStripMenuItem.Name = "ColorToolStripMenuItem";
      this.ColorToolStripMenuItem.Size = new Size(189, 22);
      this.ColorToolStripMenuItem.Text = "Color...";
      this.ColorToolStripMenuItem.Click += new EventHandler(this.partsListInfoFontAndColorToolStripMenuItem_Click);
      this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
      this.generalToolStripMenuItem.Size = new Size(189, 22);
      this.generalToolStripMenuItem.Text = "General ...";
      this.generalToolStripMenuItem.Click += new EventHandler(this.generalToolStripMenuItem_Click);
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new Size(186, 6);
      this.memoSettingsToolStripMenuItem.Name = "memoSettingsToolStripMenuItem";
      this.memoSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.memoSettingsToolStripMenuItem.Text = "Memos ...";
      this.memoSettingsToolStripMenuItem.Click += new EventHandler(this.memosToolStripMenuItem_Click);
      this.toolStripSeparator23.Name = "toolStripSeparator23";
      this.toolStripSeparator23.Size = new Size(186, 6);
      this.partsListSettingsToolStripMenuItem.Name = "partsListSettingsToolStripMenuItem";
      this.partsListSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.partsListSettingsToolStripMenuItem.Text = "Parts List Settings";
      this.partsListSettingsToolStripMenuItem.Click += new EventHandler(this.partsListSettingsToolStripMenuItem_Click);
      this.selectionListSettingsToolStripMenuItem.Name = "selectionListSettingsToolStripMenuItem";
      this.selectionListSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.selectionListSettingsToolStripMenuItem.Text = "Selection List Settings";
      this.selectionListSettingsToolStripMenuItem.Click += new EventHandler(this.selectionListSettingsToolStripMenuItem_Click);
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new Size(186, 6);
      this.pageNameSearchSettingsToolStripMenuItem.Name = "pageNameSearchSettingsToolStripMenuItem";
      this.pageNameSearchSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.pageNameSearchSettingsToolStripMenuItem.Text = "Page Name Search...";
      this.pageNameSearchSettingsToolStripMenuItem.Click += new EventHandler(this.pageNameSearchToolStripMenuItem_Click);
      this.textSearceNameSearchSettingsToolStripMenuItem.Name = "textSearceNameSearchSettingsToolStripMenuItem";
      this.textSearceNameSearchSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.textSearceNameSearchSettingsToolStripMenuItem.Text = "Text Search...";
      this.textSearceNameSearchSettingsToolStripMenuItem.Click += new EventHandler(this.textSearceNameSearchSettingsToolStripMenuItem_Click);
      this.partNameSearchSettingsToolStripMenuItem.Name = "partNameSearchSettingsToolStripMenuItem";
      this.partNameSearchSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.partNameSearchSettingsToolStripMenuItem.Text = "Part Name Search...";
      this.partNameSearchSettingsToolStripMenuItem.Click += new EventHandler(this.partNameSearchToolStripMenuItem_Click);
      this.partNumberSearchSettingsToolStripMenuItem.Name = "partNumberSearchSettingsToolStripMenuItem";
      this.partNumberSearchSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.partNumberSearchSettingsToolStripMenuItem.Text = "Part Number Search...";
      this.partNumberSearchSettingsToolStripMenuItem.Click += new EventHandler(this.partNumberSearchToolStripMenuItem_Click);
      this.advanceSearchSettingsToolStripMenuItem.Name = "advanceSearchSettingsToolStripMenuItem";
      this.advanceSearchSettingsToolStripMenuItem.Size = new Size(189, 22);
      this.advanceSearchSettingsToolStripMenuItem.Text = "Advance Search";
      this.advanceSearchSettingsToolStripMenuItem.Click += new EventHandler(this.advanceSearchSettingsToolStripMenuItem_Click);
      this.toolStripSeparator15.Name = "toolStripSeparator15";
      this.toolStripSeparator15.Size = new Size(186, 6);
      this.manageDiskSpaceToolStripMenuItem.Name = "manageDiskSpaceToolStripMenuItem";
      this.manageDiskSpaceToolStripMenuItem.Size = new Size(189, 22);
      this.manageDiskSpaceToolStripMenuItem.Text = "Manage Disk Space...";
      this.manageDiskSpaceToolStripMenuItem.Click += new EventHandler(this.manageDiskSpaceToolStripMenuItem_Click);
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(186, 6);
      this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
      this.connectionToolStripMenuItem.Size = new Size(189, 22);
      this.connectionToolStripMenuItem.Text = "Connection ...";
      this.connectionToolStripMenuItem.Click += new EventHandler(this.connectionToolStripMenuItem_Click);
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.singleBookToolStripMenuItem,
        (ToolStripItem) this.multipleBooksToolStripMenuItem
      });
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new Size(48, 20);
      this.toolsToolStripMenuItem.Text = "Tools";
      this.singleBookToolStripMenuItem.Name = "singleBookToolStripMenuItem";
      this.singleBookToolStripMenuItem.Size = new Size(219, 22);
      this.singleBookToolStripMenuItem.Text = "Single Book Download...";
      this.singleBookToolStripMenuItem.Click += new EventHandler(this.singleBookToolStripMenuItem_Click);
      this.multipleBooksToolStripMenuItem.Name = "multipleBooksToolStripMenuItem";
      this.multipleBooksToolStripMenuItem.Size = new Size(219, 22);
      this.multipleBooksToolStripMenuItem.Text = "Multiple Books Download...";
      this.multipleBooksToolStripMenuItem.Click += new EventHandler(this.multipleBooksToolStripMenuItem_Click);
      this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.gSPcLocalHelpToolStripMenuItem,
        (ToolStripItem) this.aboutGSPcLocalToolStripMenuItem
      });
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new Size(44, 20);
      this.helpToolStripMenuItem.Text = "Help";
      this.gSPcLocalHelpToolStripMenuItem.Name = "gSPcLocalHelpToolStripMenuItem";
      this.gSPcLocalHelpToolStripMenuItem.Size = new Size(177, 22);
      this.gSPcLocalHelpToolStripMenuItem.Text = "GSPcLocal Help";
      this.gSPcLocalHelpToolStripMenuItem.Click += new EventHandler(this.gSPcLocalHelpToolStripMenuItem_Click);
      this.aboutGSPcLocalToolStripMenuItem.Name = "aboutGSPcLocalToolStripMenuItem";
      this.aboutGSPcLocalToolStripMenuItem.Size = new Size(177, 22);
      this.aboutGSPcLocalToolStripMenuItem.Text = "About GSPcLocal ...";
      this.aboutGSPcLocalToolStripMenuItem.Click += new EventHandler(this.aboutGSPcLocalToolStripMenuItem_Click);
      this.addOnToolStripMenuItem.Name = "addOnToolStripMenuItem";
      this.addOnToolStripMenuItem.Size = new Size(57, 20);
      this.addOnToolStripMenuItem.Text = "AddOn";
      this.addOnToolStripMenuItem.Visible = false;
      this.tsPortal.Dock = DockStyle.None;
      this.tsPortal.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.tsbPortal,
        (ToolStripItem) this.tsbOpenBook
      });
      this.tsPortal.Location = new Point(3, 24);
      this.tsPortal.Name = "tsPortal";
      this.tsPortal.Size = new Size(58, 25);
      this.tsPortal.TabIndex = 25;
      this.tsbPortal.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPortal.Image = (Image) GSPcLocalViewer.Properties.Resources.portal;
      this.tsbPortal.ImageTransparentColor = Color.Magenta;
      this.tsbPortal.Name = "tsbPortal";
      this.tsbPortal.Size = new Size(23, 22);
      this.tsbPortal.Text = "Jump to Portal";
      this.tsbPortal.Click += new EventHandler(this.tsbPortal_Click);
      this.tsbOpenBook.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbOpenBook.Image = (Image) GSPcLocalViewer.Properties.Resources.Open_New;
      this.tsbOpenBook.ImageTransparentColor = Color.Magenta;
      this.tsbOpenBook.Name = "tsbOpenBook";
      this.tsbOpenBook.Size = new Size(23, 22);
      this.tsbOpenBook.Text = "Open New Book";
      this.tsbOpenBook.Click += new EventHandler(this.tsbOpenBook_Click);
      this.tsHistory.Dock = DockStyle.None;
      this.tsHistory.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.tsbHistoryBack,
        (ToolStripItem) this.tsbHistoryForward,
        (ToolStripItem) this.tsbHistoryList
      });
      this.tsHistory.Location = new Point(61, 24);
      this.tsHistory.Name = "tsHistory";
      this.tsHistory.Size = new Size(71, 25);
      this.tsHistory.TabIndex = 4;
      this.tsbHistoryBack.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbHistoryBack.Enabled = false;
      this.tsbHistoryBack.Image = (Image) GSPcLocalViewer.Properties.Resources.History_Backward;
      this.tsbHistoryBack.ImageTransparentColor = Color.Magenta;
      this.tsbHistoryBack.Name = "tsbHistoryBack";
      this.tsbHistoryBack.Size = new Size(23, 22);
      this.tsbHistoryBack.Text = "Backward";
      this.tsbHistoryBack.Click += new EventHandler(this.tsbHistoryBack_Click);
      this.tsbHistoryForward.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbHistoryForward.Enabled = false;
      this.tsbHistoryForward.Image = (Image) GSPcLocalViewer.Properties.Resources.History_Forward;
      this.tsbHistoryForward.ImageTransparentColor = Color.Magenta;
      this.tsbHistoryForward.Name = "tsbHistoryForward";
      this.tsbHistoryForward.Size = new Size(23, 22);
      this.tsbHistoryForward.Text = "Forward";
      this.tsbHistoryForward.Click += new EventHandler(this.tsbHistoryForward_Click);
      this.tsbHistoryList.DisplayStyle = ToolStripItemDisplayStyle.None;
      this.tsbHistoryList.Image = (Image) componentResourceManager.GetObject("tsbHistoryList.Image");
      this.tsbHistoryList.ImageTransparentColor = Color.Magenta;
      this.tsbHistoryList.Name = "tsbHistoryList";
      this.tsbHistoryList.Size = new Size(13, 22);
      this.tsbHistoryList.Text = "History List";
      this.tsNavigate.Dock = DockStyle.None;
      this.tsNavigate.Items.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.tsbNavigateFirst,
        (ToolStripItem) this.toolStripSeparator7,
        (ToolStripItem) this.tsbNavigatePrevious,
        (ToolStripItem) this.tsbNavigateNext,
        (ToolStripItem) this.toolStripSeparator8,
        (ToolStripItem) this.tsbNavigateLast
      });
      this.tsNavigate.Location = new Point(132, 24);
      this.tsNavigate.Name = "tsNavigate";
      this.tsNavigate.Size = new Size(116, 25);
      this.tsNavigate.TabIndex = 8;
      this.tsbNavigateFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbNavigateFirst.Enabled = false;
      this.tsbNavigateFirst.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_First;
      this.tsbNavigateFirst.ImageTransparentColor = Color.Magenta;
      this.tsbNavigateFirst.Name = "tsbNavigateFirst";
      this.tsbNavigateFirst.Size = new Size(23, 22);
      this.tsbNavigateFirst.Text = "First Page";
      this.tsbNavigateFirst.Click += new EventHandler(this.tsbNavigateFirst_Click);
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new Size(6, 25);
      this.tsbNavigatePrevious.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbNavigatePrevious.Enabled = false;
      this.tsbNavigatePrevious.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Prev;
      this.tsbNavigatePrevious.ImageTransparentColor = Color.Magenta;
      this.tsbNavigatePrevious.Name = "tsbNavigatePrevious";
      this.tsbNavigatePrevious.Size = new Size(23, 22);
      this.tsbNavigatePrevious.Text = "Previous Page";
      this.tsbNavigatePrevious.Click += new EventHandler(this.tsbNavigatePrevious_Click);
      this.tsbNavigateNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbNavigateNext.Enabled = false;
      this.tsbNavigateNext.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Next;
      this.tsbNavigateNext.ImageTransparentColor = Color.Magenta;
      this.tsbNavigateNext.Name = "tsbNavigateNext";
      this.tsbNavigateNext.Size = new Size(23, 22);
      this.tsbNavigateNext.Text = "Next Page";
      this.tsbNavigateNext.Click += new EventHandler(this.tsbNavigateNext_Click);
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new Size(6, 25);
      this.tsbNavigateLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbNavigateLast.Enabled = false;
      this.tsbNavigateLast.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Last;
      this.tsbNavigateLast.ImageTransparentColor = Color.Magenta;
      this.tsbNavigateLast.Name = "tsbNavigateLast";
      this.tsbNavigateLast.Size = new Size(23, 22);
      this.tsbNavigateLast.Text = "Last Page";
      this.tsbNavigateLast.Click += new EventHandler(this.tsbNavigateLast_Click);
      this.tsView.Dock = DockStyle.None;
      this.tsView.Items.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.tsbViewContents,
        (ToolStripItem) this.tsbViewPicture,
        (ToolStripItem) this.tsbViewPartslist,
        (ToolStripItem) this.tsbViewSeparator,
        (ToolStripItem) this.tsbViewInfo,
        (ToolStripItem) this.toolStripSeparator17,
        (ToolStripItem) this.tsbRestoreDefaults
      });
      this.tsView.Location = new Point(248, 24);
      this.tsView.Name = "tsView";
      this.tsView.Size = new Size(139, 25);
      this.tsView.TabIndex = 6;
      this.tsbViewContents.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbViewContents.Image = (Image) GSPcLocalViewer.Properties.Resources.View_Contents;
      this.tsbViewContents.ImageTransparentColor = Color.Magenta;
      this.tsbViewContents.Name = "tsbViewContents";
      this.tsbViewContents.Size = new Size(23, 22);
      this.tsbViewContents.Text = "View Contents";
      this.tsbViewContents.Click += new EventHandler(this.tsbViewContents_Click);
      this.tsbViewPicture.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbViewPicture.Image = (Image) GSPcLocalViewer.Properties.Resources.View_Picture;
      this.tsbViewPicture.ImageTransparentColor = Color.Magenta;
      this.tsbViewPicture.Name = "tsbViewPicture";
      this.tsbViewPicture.Size = new Size(23, 22);
      this.tsbViewPicture.Text = "View Picture";
      this.tsbViewPicture.Click += new EventHandler(this.tsbViewPicture_Click);
      this.tsbViewPartslist.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbViewPartslist.Image = (Image) GSPcLocalViewer.Properties.Resources.View_Partslist;
      this.tsbViewPartslist.ImageTransparentColor = Color.Magenta;
      this.tsbViewPartslist.Name = "tsbViewPartslist";
      this.tsbViewPartslist.Size = new Size(23, 22);
      this.tsbViewPartslist.Text = "View Partslist";
      this.tsbViewPartslist.Click += new EventHandler(this.tsbViewPartslist_Click);
      this.tsbViewSeparator.Name = "tsbViewSeparator";
      this.tsbViewSeparator.Size = new Size(6, 25);
      this.tsbViewInfo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbViewInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.View_Information;
      this.tsbViewInfo.ImageTransparentColor = Color.Magenta;
      this.tsbViewInfo.Name = "tsbViewInfo";
      this.tsbViewInfo.Size = new Size(23, 22);
      this.tsbViewInfo.Text = "View Information";
      this.tsbViewInfo.Click += new EventHandler(this.tsbViewInfo_Click);
      this.toolStripSeparator17.Name = "toolStripSeparator17";
      this.toolStripSeparator17.Size = new Size(6, 25);
      this.tsbRestoreDefaults.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbRestoreDefaults.Image = (Image) GSPcLocalViewer.Properties.Resources.View_RestoreDefaults;
      this.tsbRestoreDefaults.ImageTransparentColor = Color.Magenta;
      this.tsbRestoreDefaults.Name = "tsbRestoreDefaults";
      this.tsbRestoreDefaults.Size = new Size(23, 22);
      this.tsbRestoreDefaults.Text = "toolStripButton1";
      this.tsbRestoreDefaults.Click += new EventHandler(this.tsbRestoreDefaults_Click);
      this.tsSearch.Dock = DockStyle.None;
      this.tsSearch.Items.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.tsbSearchPageName,
        (ToolStripItem) this.tsbSearchText,
        (ToolStripItem) this.tsbSearchPartName,
        (ToolStripItem) this.tsbSearchPartNumber,
        (ToolStripItem) this.tsbSearchPartAdvance
      });
      this.tsSearch.Location = new Point(387, 24);
      this.tsSearch.Name = "tsSearch";
      this.tsSearch.Size = new Size((int) sbyte.MaxValue, 25);
      this.tsSearch.TabIndex = 7;
      this.tsbSearchPageName.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchPageName.Image = (Image) GSPcLocalViewer.Properties.Resources.Search_Page;
      this.tsbSearchPageName.ImageTransparentColor = Color.Magenta;
      this.tsbSearchPageName.Name = "tsbSearchPageName";
      this.tsbSearchPageName.Size = new Size(23, 22);
      this.tsbSearchPageName.Text = "Page Name Search";
      this.tsbSearchPageName.Click += new EventHandler(this.tsbSearchPageName_Click);
      this.tsbSearchText.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchText.Image = (Image) GSPcLocalViewer.Properties.Resources.Search_Text;
      this.tsbSearchText.ImageTransparentColor = Color.Magenta;
      this.tsbSearchText.Name = "tsbSearchText";
      this.tsbSearchText.Size = new Size(23, 22);
      this.tsbSearchText.Text = "Text Search";
      this.tsbSearchText.Click += new EventHandler(this.tsbSearchText_Click);
      this.tsbSearchPartName.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchPartName.Image = (Image) GSPcLocalViewer.Properties.Resources.Search_Parts;
      this.tsbSearchPartName.ImageTransparentColor = Color.Magenta;
      this.tsbSearchPartName.Name = "tsbSearchPartName";
      this.tsbSearchPartName.Size = new Size(23, 22);
      this.tsbSearchPartName.Text = "Part Name Search";
      this.tsbSearchPartName.Click += new EventHandler(this.tsbSearchPartName_Click);
      this.tsbSearchPartNumber.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchPartNumber.Image = (Image) GSPcLocalViewer.Properties.Resources.Search_Parts2;
      this.tsbSearchPartNumber.ImageTransparentColor = Color.Magenta;
      this.tsbSearchPartNumber.Name = "tsbSearchPartNumber";
      this.tsbSearchPartNumber.Size = new Size(23, 22);
      this.tsbSearchPartNumber.Text = "Part Number Search";
      this.tsbSearchPartNumber.Click += new EventHandler(this.tsbSearchPartNumber_Click);
      this.tsbSearchPartAdvance.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchPartAdvance.Image = (Image) GSPcLocalViewer.Properties.Resources.Search_PartsAdvance;
      this.tsbSearchPartAdvance.ImageTransparentColor = Color.Magenta;
      this.tsbSearchPartAdvance.Name = "tsbSearchPartAdvance";
      this.tsbSearchPartAdvance.Size = new Size(23, 22);
      this.tsbSearchPartAdvance.Text = "toolStripButton1";
      this.tsbSearchPartAdvance.Click += new EventHandler(this.tsbSearchPartAdvance_Click);
      this.tsTools.Dock = DockStyle.None;
      this.tsTools.Items.AddRange(new ToolStripItem[5]
      {
        (ToolStripItem) this.tsbSingleBookDownload,
        (ToolStripItem) this.tsbMultipleBooksDownload,
        (ToolStripItem) this.toolStripSeparator18,
        (ToolStripItem) this.tsbDataCleanup,
        (ToolStripItem) this.tsbConnection
      });
      this.tsTools.Location = new Point(741, 24);
      this.tsTools.Name = "tsTools";
      this.tsTools.Size = new Size(110, 25);
      this.tsTools.TabIndex = 26;
      this.tsbSingleBookDownload.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSingleBookDownload.Image = (Image) componentResourceManager.GetObject("tsbSingleBookDownload.Image");
      this.tsbSingleBookDownload.ImageTransparentColor = Color.Magenta;
      this.tsbSingleBookDownload.Name = "tsbSingleBookDownload";
      this.tsbSingleBookDownload.Size = new Size(23, 22);
      this.tsbSingleBookDownload.Text = "toolStripButton1";
      this.tsbSingleBookDownload.Click += new EventHandler(this.tsbSingleBookDownload_Click);
      this.tsbMultipleBooksDownload.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbMultipleBooksDownload.Image = (Image) componentResourceManager.GetObject("tsbMultipleBooksDownload.Image");
      this.tsbMultipleBooksDownload.ImageTransparentColor = Color.Magenta;
      this.tsbMultipleBooksDownload.Name = "tsbMultipleBooksDownload";
      this.tsbMultipleBooksDownload.Size = new Size(23, 22);
      this.tsbMultipleBooksDownload.Text = "toolStripButton2";
      this.tsbMultipleBooksDownload.Click += new EventHandler(this.tsbMultipleBooksDownload_Click);
      this.toolStripSeparator18.Name = "toolStripSeparator18";
      this.toolStripSeparator18.Size = new Size(6, 25);
      this.tsbDataCleanup.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbDataCleanup.Image = (Image) GSPcLocalViewer.Properties.Resources.Manage_Download;
      this.tsbDataCleanup.ImageTransparentColor = Color.Magenta;
      this.tsbDataCleanup.Name = "tsbDataCleanup";
      this.tsbDataCleanup.Size = new Size(23, 22);
      this.tsbDataCleanup.Text = "Data Cleanup...";
      this.tsbDataCleanup.Click += new EventHandler(this.tsbDataCleanup_Click);
      this.tsbConnection.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbConnection.Image = (Image) GSPcLocalViewer.Properties.Resources.Viewer_Connection;
      this.tsbConnection.ImageTransparentColor = Color.Magenta;
      this.tsbConnection.Name = "tsbConnection";
      this.tsbConnection.Size = new Size(23, 22);
      this.tsbConnection.Text = "Connection";
      this.tsbConnection.Click += new EventHandler(this.toolStripButton1_Click);
      this.tsFunctions.Dock = DockStyle.None;
      this.tsFunctions.Items.AddRange(new ToolStripItem[8]
      {
        (ToolStripItem) this.tsbPrint,
        (ToolStripItem) this.tsbAddBookmarks,
        (ToolStripItem) this.tsbMemoList,
        (ToolStripItem) this.tsbMemoRecovery,
        (ToolStripItem) this.tsbConfiguration,
        (ToolStripItem) this.tsbThirdPartyBasket,
        (ToolStripItem) this.tsbAbout,
        (ToolStripItem) this.tsbHelp
      });
      this.tsFunctions.Location = new Point(514, 24);
      this.tsFunctions.Name = "tsFunctions";
      this.tsFunctions.Size = new Size(227, 25);
      this.tsFunctions.TabIndex = 5;
      this.tsbPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPrint.Image = (Image) GSPcLocalViewer.Properties.Resources.Print;
      this.tsbPrint.ImageTransparentColor = Color.Magenta;
      this.tsbPrint.Name = "tsbPrint";
      this.tsbPrint.Size = new Size(23, 22);
      this.tsbPrint.Text = "Print";
      this.tsbPrint.Click += new EventHandler(this.tsbPrint_Click);
      this.tsbAddBookmarks.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbAddBookmarks.Image = (Image) GSPcLocalViewer.Properties.Resources.Add_Bookmarks;
      this.tsbAddBookmarks.ImageTransparentColor = Color.Magenta;
      this.tsbAddBookmarks.Name = "tsbAddBookmarks";
      this.tsbAddBookmarks.Size = new Size(23, 22);
      this.tsbAddBookmarks.Text = "Add Bookmarks";
      this.tsbAddBookmarks.Click += new EventHandler(this.tsbAddBookmarks_Click);
      this.tsbMemoList.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbMemoList.Image = (Image) GSPcLocalViewer.Properties.Resources.Memo_list;
      this.tsbMemoList.ImageTransparentColor = Color.Magenta;
      this.tsbMemoList.Name = "tsbMemoList";
      this.tsbMemoList.Size = new Size(23, 22);
      this.tsbMemoList.Text = "Memo List";
      this.tsbMemoList.Click += new EventHandler(this.tsbMemoList_Click);
      this.tsbMemoRecovery.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbMemoRecovery.Image = (Image) GSPcLocalViewer.Properties.Resources.Memo_Recovery;
      this.tsbMemoRecovery.ImageTransparentColor = Color.Magenta;
      this.tsbMemoRecovery.Name = "tsbMemoRecovery";
      this.tsbMemoRecovery.Size = new Size(23, 22);
      this.tsbMemoRecovery.Text = "toolStripButton1";
      this.tsbMemoRecovery.Click += new EventHandler(this.tsbMemoRecovery_Click);
      this.tsbConfiguration.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbConfiguration.Image = (Image) GSPcLocalViewer.Properties.Resources.Configuration;
      this.tsbConfiguration.ImageTransparentColor = Color.Magenta;
      this.tsbConfiguration.Name = "tsbConfiguration";
      this.tsbConfiguration.Size = new Size(23, 22);
      this.tsbConfiguration.Text = "Configuration";
      this.tsbConfiguration.Click += new EventHandler(this.tsbConfiguration_Click);
      this.tsbThirdPartyBasket.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbThirdPartyBasket.Image = (Image) GSPcLocalViewer.Properties.Resources.basket;
      this.tsbThirdPartyBasket.ImageTransparentColor = Color.Magenta;
      this.tsbThirdPartyBasket.Name = "tsbThirdPartyBasket";
      this.tsbThirdPartyBasket.Size = new Size(23, 22);
      this.tsbThirdPartyBasket.Text = "Basket";
      this.tsbThirdPartyBasket.Click += new EventHandler(this.tsbThirdPartyBasket_Click);
      this.tsbAbout.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbAbout.Image = (Image) GSPcLocalViewer.Properties.Resources.About;
      this.tsbAbout.ImageTransparentColor = Color.Magenta;
      this.tsbAbout.Name = "tsbAbout";
      this.tsbAbout.Size = new Size(23, 22);
      this.tsbAbout.Text = "About GSPcLocal";
      this.tsbAbout.Click += new EventHandler(this.tsbAbout_Click);
      this.tsbHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbHelp.Image = (Image) GSPcLocalViewer.Properties.Resources.Help;
      this.tsbHelp.ImageTransparentColor = Color.Magenta;
      this.tsbHelp.Name = "tsbHelp";
      this.tsbHelp.Size = new Size(23, 22);
      this.tsbHelp.Text = "GSPcLocal Help";
      this.tsbHelp.Click += new EventHandler(this.tsbHelp_Click);
      this.tsPic.Dock = DockStyle.None;
      this.tsPic.Items.AddRange(new ToolStripItem[20]
      {
        (ToolStripItem) this.tslPic,
        (ToolStripItem) this.tsbPicPrev,
        (ToolStripItem) this.tstPicNo,
        (ToolStripItem) this.tsbPicNext,
        (ToolStripItem) this.toolStripSeparator10,
        (ToolStripItem) this.tsbFindText,
        (ToolStripItem) this.tsbPicPanMode,
        (ToolStripItem) this.tsbPicZoomSelect,
        (ToolStripItem) this.tsbFitPage,
        (ToolStripItem) this.tsbPicCopy,
        (ToolStripItem) this.tsbPicSelectText,
        (ToolStripItem) this.toolStripSeparator19,
        (ToolStripItem) this.tsbPicZoomIn,
        (ToolStripItem) this.tsbPicZoomOut,
        (ToolStripItem) this.toolStripSeparator22,
        (ToolStripItem) this.tsBRotateLeft,
        (ToolStripItem) this.tsBRotateRight,
        (ToolStripItem) this.toolStripSeparator20,
        (ToolStripItem) this.tsbAddPictureMemo,
        (ToolStripItem) this.tsbThumbnail
      });
      this.tsPic.Location = new Point(851, 24);
      this.tsPic.Name = "tsPic";
      this.tsPic.Size = new Size(310, 25);
      this.tsPic.TabIndex = 24;
      this.tslPic.Name = "tslPic";
      this.tslPic.Size = new Size(44, 22);
      this.tslPic.Text = "Picture";
      this.tsbPicPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicPrev.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Prev;
      this.tsbPicPrev.ImageTransparentColor = Color.Magenta;
      this.tsbPicPrev.Name = "tsbPicPrev";
      this.tsbPicPrev.Size = new Size(23, 22);
      this.tsbPicPrev.Text = "Previous Picture";
      this.tsbPicPrev.Click += new EventHandler(this.tsbPicPrev_Click);
      this.tstPicNo.AutoSize = false;
      this.tstPicNo.BorderStyle = BorderStyle.FixedSingle;
      this.tstPicNo.Name = "tstPicNo";
      this.tstPicNo.ReadOnly = true;
      this.tstPicNo.ShortcutsEnabled = false;
      this.tstPicNo.Size = new Size(50, 23);
      this.tstPicNo.TextBoxTextAlign = HorizontalAlignment.Center;
      this.tsbPicNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicNext.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Next;
      this.tsbPicNext.ImageTransparentColor = Color.Magenta;
      this.tsbPicNext.Name = "tsbPicNext";
      this.tsbPicNext.Overflow = ToolStripItemOverflow.Never;
      this.tsbPicNext.Size = new Size(23, 22);
      this.tsbPicNext.Text = "Next Picture";
      this.tsbPicNext.Click += new EventHandler(this.tsbPicNext_Click);
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new Size(6, 25);
      this.tsbFindText.BackgroundImageLayout = ImageLayout.None;
      this.tsbFindText.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbFindText.Image = (Image) GSPcLocalViewer.Properties.Resources.Text_Search2;
      this.tsbFindText.ImageTransparentColor = Color.Magenta;
      this.tsbFindText.Name = "tsbFindText";
      this.tsbFindText.Size = new Size(23, 22);
      this.tsbFindText.Text = "Find Text";
      this.tsbFindText.Click += new EventHandler(this.tsbFindText_Click);
      this.tsbPicPanMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicPanMode.Image = (Image) GSPcLocalViewer.Properties.Resources.Pan_Mode;
      this.tsbPicPanMode.ImageTransparentColor = Color.Magenta;
      this.tsbPicPanMode.Name = "tsbPicPanMode";
      this.tsbPicPanMode.Size = new Size(23, 22);
      this.tsbPicPanMode.Text = "Pan Mode";
      this.tsbPicPanMode.Click += new EventHandler(this.tsbPicPanMode_Click);
      this.tsbPicZoomSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicZoomSelect.Image = (Image) GSPcLocalViewer.Properties.Resources.zoom_select;
      this.tsbPicZoomSelect.ImageTransparentColor = Color.Magenta;
      this.tsbPicZoomSelect.Name = "tsbPicZoomSelect";
      this.tsbPicZoomSelect.Size = new Size(23, 22);
      this.tsbPicZoomSelect.Text = "Select Zoom";
      this.tsbPicZoomSelect.Click += new EventHandler(this.tsbPicZoomSelect_Click);
      this.tsbFitPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbFitPage.Image = (Image) GSPcLocalViewer.Properties.Resources.zoom_fitpage;
      this.tsbFitPage.ImageTransparentColor = Color.Magenta;
      this.tsbFitPage.Name = "tsbFitPage";
      this.tsbFitPage.Size = new Size(23, 22);
      this.tsbFitPage.Text = "Zoom In";
      this.tsbFitPage.Click += new EventHandler(this.tsbFitPage_Click);
      this.tsbPicCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicCopy.Image = (Image) GSPcLocalViewer.Properties.Resources.copy_over;
      this.tsbPicCopy.ImageTransparentColor = Color.Magenta;
      this.tsbPicCopy.Name = "tsbPicCopy";
      this.tsbPicCopy.Size = new Size(23, 22);
      this.tsbPicCopy.Text = "Copy Image";
      this.tsbPicCopy.Click += new EventHandler(this.tsbPicCopy_Click);
      this.tsbPicSelectText.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicSelectText.Image = (Image) GSPcLocalViewer.Properties.Resources.Text_Selection;
      this.tsbPicSelectText.ImageTransparentColor = Color.Magenta;
      this.tsbPicSelectText.Name = "tsbPicSelectText";
      this.tsbPicSelectText.Size = new Size(23, 22);
      this.tsbPicSelectText.Text = "Copy Image";
      this.tsbPicSelectText.Click += new EventHandler(this.tsbPicSelectText_Click);
      this.toolStripSeparator19.Name = "toolStripSeparator19";
      this.toolStripSeparator19.Size = new Size(6, 25);
      this.tsbPicZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicZoomIn.Image = (Image) GSPcLocalViewer.Properties.Resources.zoom_in;
      this.tsbPicZoomIn.ImageTransparentColor = Color.Magenta;
      this.tsbPicZoomIn.Name = "tsbPicZoomIn";
      this.tsbPicZoomIn.Size = new Size(23, 22);
      this.tsbPicZoomIn.Text = "Zoom In";
      this.tsbPicZoomIn.Click += new EventHandler(this.tsbPicZoomIn_Click);
      this.tsbPicZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicZoomOut.Image = (Image) GSPcLocalViewer.Properties.Resources.zoon_out;
      this.tsbPicZoomOut.ImageTransparentColor = Color.Magenta;
      this.tsbPicZoomOut.Name = "tsbPicZoomOut";
      this.tsbPicZoomOut.Size = new Size(23, 22);
      this.tsbPicZoomOut.Text = "Zoom Out";
      this.tsbPicZoomOut.Click += new EventHandler(this.tsbPicZoomOut_Click);
      this.toolStripSeparator22.Name = "toolStripSeparator22";
      this.toolStripSeparator22.Size = new Size(6, 25);
      this.tsBRotateLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBRotateLeft.Image = (Image) GSPcLocalViewer.Properties.Resources.Rotate_Left;
      this.tsBRotateLeft.ImageTransparentColor = Color.Magenta;
      this.tsBRotateLeft.Name = "tsBRotateLeft";
      this.tsBRotateLeft.Size = new Size(23, 22);
      this.tsBRotateLeft.Text = "Rotate Left";
      this.tsBRotateLeft.Click += new EventHandler(this.tsBRotateLeft_Click);
      this.tsBRotateRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBRotateRight.Image = (Image) GSPcLocalViewer.Properties.Resources.Rotate_Right;
      this.tsBRotateRight.ImageTransparentColor = Color.Magenta;
      this.tsBRotateRight.Name = "tsBRotateRight";
      this.tsBRotateRight.Size = new Size(23, 22);
      this.tsBRotateRight.Text = "Rotate Right";
      this.tsBRotateRight.Click += new EventHandler(this.tsBRotateRight_Click);
      this.toolStripSeparator20.Name = "toolStripSeparator20";
      this.toolStripSeparator20.Size = new Size(6, 25);
      this.tsbAddPictureMemo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbAddPictureMemo.Image = (Image) GSPcLocalViewer.Properties.Resources.Add_Memo;
      this.tsbAddPictureMemo.ImageTransparentColor = Color.Magenta;
      this.tsbAddPictureMemo.Name = "tsbAddPictureMemo";
      this.tsbAddPictureMemo.Size = new Size(23, 22);
      this.tsbAddPictureMemo.Text = "Add Picture Memo";
      this.tsbAddPictureMemo.Click += new EventHandler(this.tsbAddPictureMemo_Click);
      this.tsbThumbnail.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbThumbnail.Image = (Image) GSPcLocalViewer.Properties.Resources.Thumbnail;
      this.tsbThumbnail.ImageTransparentColor = Color.Magenta;
      this.tsbThumbnail.Name = "tsbThumbnail";
      this.tsbThumbnail.Size = new Size(23, 22);
      this.tsbThumbnail.Text = "Show Thumbnail";
      this.tsbThumbnail.Click += new EventHandler(this.tsbThumbnail_Click);
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(1161, 440);
      this.Controls.Add((Control) this.toolStripContainer1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.IsMdiContainer = true;
      this.KeyPreview = true;
      this.Name = nameof (frmViewer);
      this.Text = "GSPcLocal Viewer 3.0";
      this.WindowState = FormWindowState.Maximized;
      this.Deactivate += new EventHandler(this.frmViewer_Deactivate);
      this.Load += new EventHandler(this.frmViewer_Load);
      this.SizeChanged += new EventHandler(this.frmViewer_SizeChanged);
      this.Activated += new EventHandler(this.frmViewer_Activated);
      this.FormClosed += new FormClosedEventHandler(this.frmViewer_FormClosed);
      this.FormClosing += new FormClosingEventHandler(this.frmViewer_FormClosing);
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.pnlForm.ResumeLayout(false);
      ((ISupportInitialize) this.picDisable).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tsPortal.ResumeLayout(false);
      this.tsPortal.PerformLayout();
      this.tsHistory.ResumeLayout(false);
      this.tsHistory.PerformLayout();
      this.tsNavigate.ResumeLayout(false);
      this.tsNavigate.PerformLayout();
      this.tsView.ResumeLayout(false);
      this.tsView.PerformLayout();
      this.tsSearch.ResumeLayout(false);
      this.tsSearch.PerformLayout();
      this.tsTools.ResumeLayout(false);
      this.tsTools.PerformLayout();
      this.tsFunctions.ResumeLayout(false);
      this.tsFunctions.PerformLayout();
      this.tsPic.ResumeLayout(false);
      this.tsPic.PerformLayout();
      this.ResumeLayout(false);
    }

    public delegate void SetCurrentServerIDDelegate(int curServerId);

    public delegate void SetArgumentsDelegate(string[] args);

    private delegate void UpdateStatusDelegate(string status);

    private delegate void ShowPartsListDelegate();

    private delegate void ShowPictureDelegate();

    private delegate void HidePartsListDelegate();

    private delegate void HidePictureDelegate();

    public delegate void LoadDataDirectDelegate();

    private delegate void LoadDataFromNodeDelegate(XmlNode xNode);

    private delegate void EnableAddMemoDelegate(bool value);

    private delegate void HideSelectionListDelegate();

    private delegate void GSCMenuItemsDelegate();

    private delegate void GSCToolBarItemsDelegate();

    private delegate void ShowSelectionListDelegate();

    private delegate void UpdateViewerTitleDelegate();
  }
}

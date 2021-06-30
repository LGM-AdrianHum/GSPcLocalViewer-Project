// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOpenBook
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmOpenBook : Form
  {
    public frmOpenBookTasks objFrmTasks;
    public frmOpenBookBrowse objFrmBrowse;
    public frmOpenBookBookmarks objFrmBookmark;
    public frmOpenBookSearch objFrmSearch;
    public frmPageNameAdvSrch objFrmPageNameAdvSrch;
    public frmPartNameAdvSrch objFrmPartNameAdvSrch;
    public frmPartNumberAdvSrch objFrmPartNumberAdvSrch;
    public frmViewer frmParent;
    private string language;
    private IContainer components;
    private ToolStripContainer toolStripContainer1;
    private StatusStrip ssStatus;
    private ToolStrip toolStrip1;
    private ToolStripButton tsbBrowse;
    private ToolStripButton tsbBookmarks;
    private ToolStripStatusLabel lblStatus;
    private Panel pnlContents;
    private Panel pnlTasks;
    private ToolStripButton tsbSearch;
    private ToolStrip toolStrip2;
    private ToolStripButton toolStripButton4;
    private ToolStripButton toolStripButton5;
    private ToolStripButton toolStripButton6;
    private ToolStripButton toolStripButton7;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton tsbConnection;
    private ToolStripButton tsbDataCleanup;
    private ToolStripButton tsbSearchPageName;
    private ToolStripButton tsbSearchPartName;
    private ToolStripButton tsbSearchPartNumber;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton tsbSearchText;

    public frmOpenBook(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.language = Settings.Default.appLanguage;
      this.Font = Settings.Default.appFont;
    }

    private void frmOpenBook_Load(object sender, EventArgs e)
    {
      this.LoadResources();
      this.tsbBrowse.Visible = Program.objAppFeatures.bOpenBookScreen;
      this.LoadUserSettings();
      this.CreateForms();
      this.tsbSearchPageName.Visible = Program.objAppFeatures.bAdvanceSearchPageName;
      this.tsbSearchPartName.Visible = Program.objAppFeatures.bAdvanceSearchPartName;
      this.tsbSearchPartNumber.Visible = Program.objAppFeatures.bAdvanceSearchPartNumber;
      this.toolStripSeparator3.Visible = this.tsbSearchPageName.Visible || this.tsbSearchPartName.Visible || this.tsbSearchPartNumber.Visible;
      this.objFrmTasks.Show();
    }

    private void frmOpenBook_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      this.SaveUserSettings();
      this.frmParent.HideDimmer();
      if (!this.objFrmBrowse.IsDisposed)
        this.objFrmBrowse.Close();
      if (!this.objFrmBookmark.IsDisposed)
        this.objFrmBookmark.Close();
      if (!this.objFrmSearch.IsDisposed)
        this.objFrmSearch.Close();
      if (!this.objFrmPageNameAdvSrch.IsDisposed)
        this.objFrmPageNameAdvSrch.Close();
      if (!this.objFrmPartNameAdvSrch.IsDisposed)
        this.objFrmPartNameAdvSrch.Close();
      if (this.objFrmPartNumberAdvSrch.IsDisposed)
        return;
      this.objFrmPartNumberAdvSrch.Close();
    }

    public void CloseAndLoadData(int serverId, string bookPublishingId, XmlNode bookNode, XmlNode schemaNode, string bookType)
    {
      if (!Global.SecurityLocksOpen(bookNode, schemaNode, serverId, this.frmParent))
        return;
      this.Close();
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      this.frmParent.LoadData(serverId, bookPublishingId, bookNode, schemaNode, bookType);
    }

    public void CloseAndLoadFavourite(XmlNode objXmlNode)
    {
      this.Close();
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      this.frmParent.OpenBookmarkPage(objXmlNode);
    }

    public void CloseAndLoadSearch(string sServerKey, string sBookCode, string sPageId, string sPicIndex, string sListIndex, string sPartNumber)
    {
      this.Close();
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      this.frmParent.OpenSearchResults(sServerKey, sBookCode, sPageId, sPicIndex, sListIndex, sPartNumber);
    }

    private void CreateForms()
    {
      this.objFrmBrowse = new frmOpenBookBrowse(this);
      this.objFrmBrowse.TopLevel = false;
      this.objFrmBrowse.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmBrowse);
      this.objFrmBookmark = new frmOpenBookBookmarks(this);
      this.objFrmBookmark.TopLevel = false;
      this.objFrmBookmark.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmBookmark);
      this.objFrmSearch = new frmOpenBookSearch(this);
      this.objFrmSearch.TopLevel = false;
      this.objFrmSearch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmSearch);
      this.objFrmPageNameAdvSrch = new frmPageNameAdvSrch(this);
      this.objFrmPageNameAdvSrch.TopLevel = false;
      this.objFrmPageNameAdvSrch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmPageNameAdvSrch);
      this.objFrmPartNameAdvSrch = new frmPartNameAdvSrch(this);
      this.objFrmPartNameAdvSrch.TopLevel = false;
      this.objFrmPartNameAdvSrch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmPartNameAdvSrch);
      this.objFrmPartNumberAdvSrch = new frmPartNumberAdvSrch(this);
      this.objFrmPartNumberAdvSrch.TopLevel = false;
      this.objFrmPartNumberAdvSrch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmPartNumberAdvSrch);
      this.objFrmTasks = new frmOpenBookTasks(this);
      this.objFrmTasks.TopLevel = false;
      this.objFrmTasks.Dock = DockStyle.Fill;
      this.pnlTasks.Controls.Add((Control) this.objFrmTasks);
    }

    public void HideForms()
    {
      foreach (Control control in (ArrangedElementCollection) this.pnlContents.Controls)
        control?.Hide();
    }

    public void UpdateStatus(string status)
    {
      if (this.ssStatus.InvokeRequired)
        this.ssStatus.Invoke((Delegate) new frmOpenBook.UpdateStatusDelegate(this.UpdateStatus), (object) status);
      else
        this.lblStatus.Text = status;
    }

    public Control GetCurrentChildForm()
    {
      foreach (Control control in (ArrangedElementCollection) this.pnlContents.Controls)
      {
        if (control.Visible)
          return control;
      }
      return (Control) null;
    }

    private void LoadUserSettings()
    {
      this.Location = Settings.Default.frmOpenBookLocation;
      this.Size = Settings.Default.frmOpenBookSize;
      if (Settings.Default.frmOpenBookState == FormWindowState.Minimized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = Settings.Default.frmOpenBookState;
      this.frmParent.checkConfigFileExist();
      ToolStripManager.LoadSettings((Form) this);
    }

    private void SaveUserSettings()
    {
      Settings.Default.frmOpenBookLocation = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Location : this.Location;
      Settings.Default.frmOpenBookSize = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Size : this.Size;
      Settings.Default.frmOpenBookState = this.WindowState;
      ToolStripManager.SaveSettings((Form) this);
      this.frmParent.CopyConfigurationFile();
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.objFrmTasks.UpdateFont();
      this.objFrmBrowse.UpdateFont();
      this.objFrmBookmark.UpdateFont();
      this.objFrmSearch.UpdateFont();
      this.objFrmPageNameAdvSrch.UpdateFont();
      this.objFrmPartNameAdvSrch.UpdateFont();
      this.objFrmPartNumberAdvSrch.UpdateFont();
    }

    private void frmOpenBook_Activated(object sender, EventArgs e)
    {
    }

    public XmlNodeList FilterBooksList(XmlNode xSchemaNode, XmlNodeList xNodeList)
    {
      return this.frmParent.FilterBooksList(xSchemaNode, xNodeList);
    }

    private void tsbBrowse_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblOpenBooksBrowse_Click((object) null, (EventArgs) null);
    }

    private void tsbBookmarks_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblOpenBooksBookmarks_Click((object) null, (EventArgs) null);
    }

    private void tsbSearch_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblOpenBooksSearch_Click((object) null, (EventArgs) null);
    }

    private void tsbSearchPageName_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblAdvSrchPageName_Click((object) null, (EventArgs) null);
    }

    private void tsbSearchPartName_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblAdvSrchPartName_Click((object) null, (EventArgs) null);
    }

    private void tsbSearchPartNumber_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblAdvSrchPartNo_Click((object) null, (EventArgs) null);
    }

    private void tsbConfiguration_Click(object sender, EventArgs e)
    {
      frmConfig frmConfig = new frmConfig(this.frmParent);
      frmConfig.Owner = (Form) this;
      frmConfig.Show();
    }

    private void tsbConnection_Click(object sender, EventArgs e)
    {
      frmConnection frmConnection = new frmConnection(this.frmParent);
      frmConnection.Owner = (Form) this;
      frmConnection.Show();
    }

    private void tsbDataCleanup_Click(object sender, EventArgs e)
    {
      frmDataSize frmDataSize = new frmDataSize(this.frmParent);
      frmDataSize.Owner = (Form) this;
      frmDataSize.Show();
    }

    private void LoadResources()
    {
      this.tsbBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.TOOLSTRIP);
      this.tsbBookmarks.Text = this.GetResource("Bookmarks", "BOOKMARKS", ResourceType.TOOLSTRIP);
      this.tsbSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.TOOLSTRIP);
      this.tsbDataCleanup.Text = this.GetResource("Manage Disk Space", "MANAGE_DISK_SPACE", ResourceType.TOOLSTRIP);
      this.tsbConnection.Text = this.GetResource("Connection Settings", "CONNECTION_SETTINGS", ResourceType.TOOLSTRIP);
      this.tsbSearchPageName.Text = this.GetResource("Page Name Search", "PAGE_NAME_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbSearchPartName.Text = this.GetResource("Part Name Search", "PART_NAME_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbSearchPartNumber.Text = this.GetResource("Part Number Search", "PART_NUMBER_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbSearchText.Text = this.GetResource("Search", "SEARCH", ResourceType.TOOLSTRIP);
      this.toolStripButton4.Text = this.GetResource("Tree View", "TREE_VIEW", ResourceType.TOOLSTRIP);
      this.toolStripButton5.Text = this.GetResource("Grid View", "GRID_VIEW", ResourceType.TOOLSTRIP);
      this.toolStripButton6.Text = this.GetResource("Explorer View", "EXPLORER_VIEW", ResourceType.TOOLSTRIP);
      this.toolStripButton7.Text = this.GetResource("Image Preview", "IMAGE_PREVIEW", ResourceType.TOOLSTRIP);
      this.Text = this.GetResource("Open Books", "OPEN_BOOKS", ResourceType.TITLE);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='OPEN_BOOKS']";
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

    private void tsbSearchText_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmOpenBook));
      this.toolStripContainer1 = new ToolStripContainer();
      this.ssStatus = new StatusStrip();
      this.lblStatus = new ToolStripStatusLabel();
      this.pnlContents = new Panel();
      this.pnlTasks = new Panel();
      this.toolStrip1 = new ToolStrip();
      this.tsbBrowse = new ToolStripButton();
      this.tsbBookmarks = new ToolStripButton();
      this.tsbSearch = new ToolStripButton();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.tsbSearchPageName = new ToolStripButton();
      this.tsbSearchPartName = new ToolStripButton();
      this.tsbSearchPartNumber = new ToolStripButton();
      this.tsbSearchText = new ToolStripButton();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.tsbDataCleanup = new ToolStripButton();
      this.tsbConnection = new ToolStripButton();
      this.toolStrip2 = new ToolStrip();
      this.toolStripButton4 = new ToolStripButton();
      this.toolStripButton5 = new ToolStripButton();
      this.toolStripButton6 = new ToolStripButton();
      this.toolStripButton7 = new ToolStripButton();
      this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.ssStatus.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this.SuspendLayout();
      this.toolStripContainer1.BottomToolStripPanel.Controls.Add((Control) this.ssStatus);
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.pnlContents);
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.pnlTasks);
      this.toolStripContainer1.ContentPanel.Size = new Size(699, 424);
      this.toolStripContainer1.Dock = DockStyle.Fill;
      this.toolStripContainer1.Location = new Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new Size(699, 471);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.toolStrip1);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.toolStrip2);
      this.ssStatus.Dock = DockStyle.None;
      this.ssStatus.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.lblStatus
      });
      this.ssStatus.Location = new Point(0, 0);
      this.ssStatus.Name = "ssStatus";
      this.ssStatus.Size = new Size(699, 22);
      this.ssStatus.TabIndex = 0;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(38, 17);
      this.lblStatus.Text = "Ready";
      this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlContents.Dock = DockStyle.Fill;
      this.pnlContents.Location = new Point(161, 0);
      this.pnlContents.Name = "pnlContents";
      this.pnlContents.Padding = new Padding(2);
      this.pnlContents.Size = new Size(538, 424);
      this.pnlContents.TabIndex = 16;
      this.pnlTasks.Dock = DockStyle.Left;
      this.pnlTasks.Location = new Point(0, 0);
      this.pnlTasks.Name = "pnlTasks";
      this.pnlTasks.Padding = new Padding(2);
      this.pnlTasks.Size = new Size(161, 424);
      this.pnlTasks.TabIndex = 15;
      this.toolStrip1.Dock = DockStyle.None;
      this.toolStrip1.Items.AddRange(new ToolStripItem[11]
      {
        (ToolStripItem) this.tsbBrowse,
        (ToolStripItem) this.tsbBookmarks,
        (ToolStripItem) this.tsbSearch,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.tsbSearchPageName,
        (ToolStripItem) this.tsbSearchPartName,
        (ToolStripItem) this.tsbSearchPartNumber,
        (ToolStripItem) this.tsbSearchText,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.tsbDataCleanup,
        (ToolStripItem) this.tsbConnection
      });
      this.toolStrip1.Location = new Point(3, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new Size(260, 25);
      this.toolStrip1.TabIndex = 1;
      this.tsbBrowse.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbBrowse.Image = (Image) componentResourceManager.GetObject("tsbBrowse.Image");
      this.tsbBrowse.ImageTransparentColor = Color.Magenta;
      this.tsbBrowse.Name = "tsbBrowse";
      this.tsbBrowse.Size = new Size(23, 22);
      this.tsbBrowse.Text = "Browse";
      this.tsbBrowse.Click += new EventHandler(this.tsbBrowse_Click);
      this.tsbBookmarks.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbBookmarks.Image = (Image) Resources.Bookmarks;
      this.tsbBookmarks.ImageTransparentColor = Color.Magenta;
      this.tsbBookmarks.Name = "tsbBookmarks";
      this.tsbBookmarks.Size = new Size(23, 22);
      this.tsbBookmarks.Text = "Bookmarks";
      this.tsbBookmarks.Click += new EventHandler(this.tsbBookmarks_Click);
      this.tsbSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearch.Image = (Image) componentResourceManager.GetObject("tsbSearch.Image");
      this.tsbSearch.ImageTransparentColor = Color.Magenta;
      this.tsbSearch.Name = "tsbSearch";
      this.tsbSearch.Size = new Size(23, 22);
      this.tsbSearch.Text = "Search";
      this.tsbSearch.Click += new EventHandler(this.tsbSearch_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(6, 25);
      this.tsbSearchPageName.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchPageName.Image = (Image) Resources.Search_Page;
      this.tsbSearchPageName.ImageTransparentColor = Color.Magenta;
      this.tsbSearchPageName.Name = "tsbSearchPageName";
      this.tsbSearchPageName.Size = new Size(23, 22);
      this.tsbSearchPageName.Text = "Page Name Search";
      this.tsbSearchPageName.Click += new EventHandler(this.tsbSearchPageName_Click);
      this.tsbSearchPartName.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchPartName.Image = (Image) Resources.Search_Parts;
      this.tsbSearchPartName.ImageTransparentColor = Color.Magenta;
      this.tsbSearchPartName.Name = "tsbSearchPartName";
      this.tsbSearchPartName.Size = new Size(23, 22);
      this.tsbSearchPartName.Text = "Part Name Search";
      this.tsbSearchPartName.Click += new EventHandler(this.tsbSearchPartName_Click);
      this.tsbSearchPartNumber.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchPartNumber.Image = (Image) Resources.Search_Parts2;
      this.tsbSearchPartNumber.ImageTransparentColor = Color.Magenta;
      this.tsbSearchPartNumber.Name = "tsbSearchPartNumber";
      this.tsbSearchPartNumber.Size = new Size(23, 22);
      this.tsbSearchPartNumber.Text = "Part Number Search";
      this.tsbSearchPartNumber.Click += new EventHandler(this.tsbSearchPartNumber_Click);
      this.tsbSearchText.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchText.Image = (Image) Resources.Search_Text;
      this.tsbSearchText.ImageTransparentColor = Color.Magenta;
      this.tsbSearchText.Name = "tsbSearchText";
      this.tsbSearchText.Size = new Size(23, 22);
      this.tsbSearchText.Text = "Text Search";
      this.tsbSearchText.Visible = false;
      this.tsbSearchText.Click += new EventHandler(this.tsbSearchText_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(6, 25);
      this.tsbDataCleanup.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbDataCleanup.Image = (Image) Resources.Manage_Download;
      this.tsbDataCleanup.ImageTransparentColor = Color.Magenta;
      this.tsbDataCleanup.Name = "tsbDataCleanup";
      this.tsbDataCleanup.Size = new Size(23, 22);
      this.tsbDataCleanup.Text = "Data Cleanup...";
      this.tsbDataCleanup.Click += new EventHandler(this.tsbDataCleanup_Click);
      this.tsbConnection.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbConnection.Image = (Image) Resources.Viewer_Connection;
      this.tsbConnection.ImageTransparentColor = Color.Magenta;
      this.tsbConnection.Name = "tsbConnection";
      this.tsbConnection.Size = new Size(23, 22);
      this.tsbConnection.Text = "Connection Settings";
      this.tsbConnection.Click += new EventHandler(this.tsbConnection_Click);
      this.toolStrip2.Dock = DockStyle.None;
      this.toolStrip2.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.toolStripButton4,
        (ToolStripItem) this.toolStripButton5,
        (ToolStripItem) this.toolStripButton6,
        (ToolStripItem) this.toolStripButton7
      });
      this.toolStrip2.Location = new Point(113, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.Size = new Size(102, 25);
      this.toolStrip2.TabIndex = 2;
      this.toolStrip2.Visible = false;
      this.toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButton4.Enabled = false;
      this.toolStripButton4.Image = (Image) componentResourceManager.GetObject("toolStripButton4.Image");
      this.toolStripButton4.ImageTransparentColor = Color.Magenta;
      this.toolStripButton4.Name = "toolStripButton4";
      this.toolStripButton4.Size = new Size(23, 22);
      this.toolStripButton4.Text = "Tree View";
      this.toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButton5.Enabled = false;
      this.toolStripButton5.Image = (Image) componentResourceManager.GetObject("toolStripButton5.Image");
      this.toolStripButton5.ImageTransparentColor = Color.Magenta;
      this.toolStripButton5.Name = "toolStripButton5";
      this.toolStripButton5.Size = new Size(23, 22);
      this.toolStripButton5.Text = "Grid View";
      this.toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButton6.Enabled = false;
      this.toolStripButton6.Image = (Image) componentResourceManager.GetObject("toolStripButton6.Image");
      this.toolStripButton6.ImageTransparentColor = Color.Magenta;
      this.toolStripButton6.Name = "toolStripButton6";
      this.toolStripButton6.Size = new Size(23, 22);
      this.toolStripButton6.Text = "Explorer View";
      this.toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.toolStripButton7.Enabled = false;
      this.toolStripButton7.Image = (Image) componentResourceManager.GetObject("toolStripButton7.Image");
      this.toolStripButton7.ImageTransparentColor = Color.Magenta;
      this.toolStripButton7.Name = "toolStripButton7";
      this.toolStripButton7.Size = new Size(23, 22);
      this.toolStripButton7.Text = "Image Preview";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(699, 471);
      this.Controls.Add((Control) this.toolStripContainer1);
      this.IsMdiContainer = true;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(580, 380);
      this.Name = nameof (frmOpenBook);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Open Books";
      this.Load += new EventHandler(this.frmOpenBook_Load);
      this.Activated += new EventHandler(this.frmOpenBook_Activated);
      this.FormClosing += new FormClosingEventHandler(this.frmOpenBook_FormClosing);
      this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this.ResumeLayout(false);
    }

    private delegate void UpdateStatusDelegate(string status);
  }
}

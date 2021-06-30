// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOpenBookTasks
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class frmOpenBookTasks : Form
  {
    private IContainer components;
    private Panel pnlTasks;
    private PictureBox picLoading;
    private Panel pnlTasksSelectViews;
    private Label lblSelectViewsImagePreview;
    private Label lblSelectViewsExplorerView;
    private Label lblSelectViewsGridView;
    private Label lblSelectViewsTreeView;
    private Panel pnlTasksOpenBooks;
    private Label lblOpenBooksSearch;
    private Label lblOpenBooksBookmarks;
    private Label lblOpenBooksBrowse;
    private Label lblTasksTitle;
    private Panel pnlTasksAdvSrch;
    private Label lblAdvSrchPageName;
    private Panel pnlLblOpenBooks;
    private Label lblOpenBooksLine;
    private Label lblOpenBooks;
    private Panel pnlLblSelectViews;
    private Label lblSelectViewsLine;
    private Label lblSelectViews;
    private Panel pnlLblAdvancedSearch;
    private Label lblAdvancedSearchLine;
    private Label lblAdvancedSearch;
    private Label lblAdvSrchPartNo;
    private Label lblAdvSrchPartName;
    private frmOpenBook frmParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlTasks = new Panel();
      this.pnlTasksSelectViews = new Panel();
      this.lblSelectViewsImagePreview = new Label();
      this.lblSelectViewsExplorerView = new Label();
      this.lblSelectViewsGridView = new Label();
      this.lblSelectViewsTreeView = new Label();
      this.pnlLblSelectViews = new Panel();
      this.lblSelectViewsLine = new Label();
      this.lblSelectViews = new Label();
      this.pnlTasksAdvSrch = new Panel();
      this.lblAdvSrchPartNo = new Label();
      this.lblAdvSrchPartName = new Label();
      this.lblAdvSrchPageName = new Label();
      this.pnlLblAdvancedSearch = new Panel();
      this.lblAdvancedSearchLine = new Label();
      this.lblAdvancedSearch = new Label();
      this.picLoading = new PictureBox();
      this.pnlTasksOpenBooks = new Panel();
      this.lblOpenBooksSearch = new Label();
      this.lblOpenBooksBookmarks = new Label();
      this.lblOpenBooksBrowse = new Label();
      this.pnlLblOpenBooks = new Panel();
      this.lblOpenBooksLine = new Label();
      this.lblOpenBooks = new Label();
      this.lblTasksTitle = new Label();
      this.pnlTasks.SuspendLayout();
      this.pnlTasksSelectViews.SuspendLayout();
      this.pnlLblSelectViews.SuspendLayout();
      this.pnlTasksAdvSrch.SuspendLayout();
      this.pnlLblAdvancedSearch.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.pnlTasksOpenBooks.SuspendLayout();
      this.pnlLblOpenBooks.SuspendLayout();
      this.SuspendLayout();
      this.pnlTasks.BackColor = Color.White;
      this.pnlTasks.BorderStyle = BorderStyle.FixedSingle;
      this.pnlTasks.Controls.Add((Control) this.pnlTasksSelectViews);
      this.pnlTasks.Controls.Add((Control) this.pnlTasksAdvSrch);
      this.pnlTasks.Controls.Add((Control) this.picLoading);
      this.pnlTasks.Controls.Add((Control) this.pnlTasksOpenBooks);
      this.pnlTasks.Controls.Add((Control) this.lblTasksTitle);
      this.pnlTasks.Dock = DockStyle.Fill;
      this.pnlTasks.ForeColor = Color.Black;
      this.pnlTasks.Location = new Point(0, 0);
      this.pnlTasks.Name = "pnlTasks";
      this.pnlTasks.Size = new Size(151, 398);
      this.pnlTasks.TabIndex = 8;
      this.pnlTasksSelectViews.Controls.Add((Control) this.lblSelectViewsImagePreview);
      this.pnlTasksSelectViews.Controls.Add((Control) this.lblSelectViewsExplorerView);
      this.pnlTasksSelectViews.Controls.Add((Control) this.lblSelectViewsGridView);
      this.pnlTasksSelectViews.Controls.Add((Control) this.lblSelectViewsTreeView);
      this.pnlTasksSelectViews.Controls.Add((Control) this.pnlLblSelectViews);
      this.pnlTasksSelectViews.Dock = DockStyle.Top;
      this.pnlTasksSelectViews.Location = new Point(0, 225);
      this.pnlTasksSelectViews.Name = "pnlTasksSelectViews";
      this.pnlTasksSelectViews.Padding = new Padding(15, 0, 15, 0);
      this.pnlTasksSelectViews.Size = new Size(149, 112);
      this.pnlTasksSelectViews.TabIndex = 12;
      this.pnlTasksSelectViews.Tag = (object) "";
      this.pnlTasksSelectViews.Visible = false;
      this.lblSelectViewsImagePreview.Cursor = Cursors.Hand;
      this.lblSelectViewsImagePreview.Dock = DockStyle.Top;
      this.lblSelectViewsImagePreview.Enabled = false;
      this.lblSelectViewsImagePreview.Location = new Point(15, 76);
      this.lblSelectViewsImagePreview.Name = "lblSelectViewsImagePreview";
      this.lblSelectViewsImagePreview.Padding = new Padding(20, 0, 0, 0);
      this.lblSelectViewsImagePreview.Size = new Size(119, 16);
      this.lblSelectViewsImagePreview.TabIndex = 18;
      this.lblSelectViewsImagePreview.Text = "Image Preview";
      this.lblSelectViewsImagePreview.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectViewsImagePreview.Click += new EventHandler(this.lblSelectViewsImagePreview_Click);
      this.lblSelectViewsExplorerView.Cursor = Cursors.Hand;
      this.lblSelectViewsExplorerView.Dock = DockStyle.Top;
      this.lblSelectViewsExplorerView.Enabled = false;
      this.lblSelectViewsExplorerView.Location = new Point(15, 60);
      this.lblSelectViewsExplorerView.Name = "lblSelectViewsExplorerView";
      this.lblSelectViewsExplorerView.Padding = new Padding(20, 0, 0, 0);
      this.lblSelectViewsExplorerView.Size = new Size(119, 16);
      this.lblSelectViewsExplorerView.TabIndex = 17;
      this.lblSelectViewsExplorerView.Text = "Explorer View";
      this.lblSelectViewsExplorerView.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectViewsExplorerView.Click += new EventHandler(this.lblSelectViewsExplorerView_Click);
      this.lblSelectViewsGridView.Cursor = Cursors.Hand;
      this.lblSelectViewsGridView.Dock = DockStyle.Top;
      this.lblSelectViewsGridView.Enabled = false;
      this.lblSelectViewsGridView.Location = new Point(15, 44);
      this.lblSelectViewsGridView.Name = "lblSelectViewsGridView";
      this.lblSelectViewsGridView.Padding = new Padding(20, 0, 0, 0);
      this.lblSelectViewsGridView.Size = new Size(119, 16);
      this.lblSelectViewsGridView.TabIndex = 16;
      this.lblSelectViewsGridView.Text = "Grid View";
      this.lblSelectViewsGridView.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectViewsGridView.Click += new EventHandler(this.lblSelectViewsGridView_Click);
      this.lblSelectViewsTreeView.Cursor = Cursors.Hand;
      this.lblSelectViewsTreeView.Dock = DockStyle.Top;
      this.lblSelectViewsTreeView.Enabled = false;
      this.lblSelectViewsTreeView.Location = new Point(15, 28);
      this.lblSelectViewsTreeView.Name = "lblSelectViewsTreeView";
      this.lblSelectViewsTreeView.Padding = new Padding(20, 0, 0, 0);
      this.lblSelectViewsTreeView.Size = new Size(119, 16);
      this.lblSelectViewsTreeView.TabIndex = 15;
      this.lblSelectViewsTreeView.Text = "Tree View";
      this.lblSelectViewsTreeView.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectViewsTreeView.Click += new EventHandler(this.lblSelectViewsTreeView_Click);
      this.pnlLblSelectViews.Controls.Add((Control) this.lblSelectViewsLine);
      this.pnlLblSelectViews.Controls.Add((Control) this.lblSelectViews);
      this.pnlLblSelectViews.Dock = DockStyle.Top;
      this.pnlLblSelectViews.Location = new Point(15, 0);
      this.pnlLblSelectViews.Name = "pnlLblSelectViews";
      this.pnlLblSelectViews.Size = new Size(119, 28);
      this.pnlLblSelectViews.TabIndex = 22;
      this.lblSelectViewsLine.BackColor = Color.Transparent;
      this.lblSelectViewsLine.Dock = DockStyle.Fill;
      this.lblSelectViewsLine.ForeColor = Color.Blue;
      this.lblSelectViewsLine.Image = (Image) Resources.GroupLine0;
      this.lblSelectViewsLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSelectViewsLine.Location = new Point(66, 0);
      this.lblSelectViewsLine.Name = "lblSelectViewsLine";
      this.lblSelectViewsLine.Size = new Size(53, 28);
      this.lblSelectViewsLine.TabIndex = 15;
      this.lblSelectViewsLine.Tag = (object) "";
      this.lblSelectViewsLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectViews.BackColor = Color.Transparent;
      this.lblSelectViews.Dock = DockStyle.Left;
      this.lblSelectViews.ForeColor = Color.Blue;
      this.lblSelectViews.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSelectViews.Location = new Point(0, 0);
      this.lblSelectViews.Name = "lblSelectViews";
      this.lblSelectViews.Size = new Size(66, 28);
      this.lblSelectViews.TabIndex = 13;
      this.lblSelectViews.Tag = (object) "";
      this.lblSelectViews.Text = "Select Views";
      this.lblSelectViews.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlTasksAdvSrch.Controls.Add((Control) this.lblAdvSrchPartNo);
      this.pnlTasksAdvSrch.Controls.Add((Control) this.lblAdvSrchPartName);
      this.pnlTasksAdvSrch.Controls.Add((Control) this.lblAdvSrchPageName);
      this.pnlTasksAdvSrch.Controls.Add((Control) this.pnlLblAdvancedSearch);
      this.pnlTasksAdvSrch.Dock = DockStyle.Top;
      this.pnlTasksAdvSrch.Location = new Point(0, 131);
      this.pnlTasksAdvSrch.Name = "pnlTasksAdvSrch";
      this.pnlTasksAdvSrch.Padding = new Padding(15, 0, 15, 0);
      this.pnlTasksAdvSrch.Size = new Size(149, 94);
      this.pnlTasksAdvSrch.TabIndex = 18;
      this.pnlTasksAdvSrch.Tag = (object) "";
      this.lblAdvSrchPartNo.Cursor = Cursors.Hand;
      this.lblAdvSrchPartNo.Dock = DockStyle.Top;
      this.lblAdvSrchPartNo.Location = new Point(15, 60);
      this.lblAdvSrchPartNo.Name = "lblAdvSrchPartNo";
      this.lblAdvSrchPartNo.Padding = new Padding(20, 0, 0, 0);
      this.lblAdvSrchPartNo.Size = new Size(119, 16);
      this.lblAdvSrchPartNo.TabIndex = 24;
      this.lblAdvSrchPartNo.Text = "Part Number";
      this.lblAdvSrchPartNo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdvSrchPartNo.Click += new EventHandler(this.lblAdvSrchPartNo_Click);
      this.lblAdvSrchPartName.Cursor = Cursors.Hand;
      this.lblAdvSrchPartName.Dock = DockStyle.Top;
      this.lblAdvSrchPartName.Location = new Point(15, 44);
      this.lblAdvSrchPartName.Name = "lblAdvSrchPartName";
      this.lblAdvSrchPartName.Padding = new Padding(20, 0, 0, 0);
      this.lblAdvSrchPartName.Size = new Size(119, 16);
      this.lblAdvSrchPartName.TabIndex = 23;
      this.lblAdvSrchPartName.Text = "Part Name";
      this.lblAdvSrchPartName.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdvSrchPartName.Click += new EventHandler(this.lblAdvSrchPartName_Click);
      this.lblAdvSrchPageName.Cursor = Cursors.Hand;
      this.lblAdvSrchPageName.Dock = DockStyle.Top;
      this.lblAdvSrchPageName.Location = new Point(15, 28);
      this.lblAdvSrchPageName.Name = "lblAdvSrchPageName";
      this.lblAdvSrchPageName.Padding = new Padding(20, 0, 0, 0);
      this.lblAdvSrchPageName.Size = new Size(119, 16);
      this.lblAdvSrchPageName.TabIndex = 15;
      this.lblAdvSrchPageName.Text = "Page Name";
      this.lblAdvSrchPageName.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdvSrchPageName.Click += new EventHandler(this.lblAdvSrchPageName_Click);
      this.pnlLblAdvancedSearch.Controls.Add((Control) this.lblAdvancedSearchLine);
      this.pnlLblAdvancedSearch.Controls.Add((Control) this.lblAdvancedSearch);
      this.pnlLblAdvancedSearch.Dock = DockStyle.Top;
      this.pnlLblAdvancedSearch.Location = new Point(15, 0);
      this.pnlLblAdvancedSearch.Name = "pnlLblAdvancedSearch";
      this.pnlLblAdvancedSearch.Size = new Size(119, 28);
      this.pnlLblAdvancedSearch.TabIndex = 22;
      this.lblAdvancedSearchLine.BackColor = Color.Transparent;
      this.lblAdvancedSearchLine.Dock = DockStyle.Fill;
      this.lblAdvancedSearchLine.ForeColor = Color.Blue;
      this.lblAdvancedSearchLine.Image = (Image) Resources.GroupLine0;
      this.lblAdvancedSearchLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblAdvancedSearchLine.Location = new Point(96, 0);
      this.lblAdvancedSearchLine.Name = "lblAdvancedSearchLine";
      this.lblAdvancedSearchLine.Size = new Size(23, 28);
      this.lblAdvancedSearchLine.TabIndex = 15;
      this.lblAdvancedSearchLine.Tag = (object) "";
      this.lblAdvancedSearchLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdvancedSearch.BackColor = Color.Transparent;
      this.lblAdvancedSearch.Dock = DockStyle.Left;
      this.lblAdvancedSearch.ForeColor = Color.Blue;
      this.lblAdvancedSearch.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblAdvancedSearch.Location = new Point(0, 0);
      this.lblAdvancedSearch.Name = "lblAdvancedSearch";
      this.lblAdvancedSearch.Size = new Size(96, 28);
      this.lblAdvancedSearch.TabIndex = 13;
      this.lblAdvancedSearch.Tag = (object) "";
      this.lblAdvancedSearch.Text = "Advanced Search";
      this.lblAdvancedSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(49, 233);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 16;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.pnlTasksOpenBooks.BackColor = Color.White;
      this.pnlTasksOpenBooks.Controls.Add((Control) this.lblOpenBooksSearch);
      this.pnlTasksOpenBooks.Controls.Add((Control) this.lblOpenBooksBookmarks);
      this.pnlTasksOpenBooks.Controls.Add((Control) this.lblOpenBooksBrowse);
      this.pnlTasksOpenBooks.Controls.Add((Control) this.pnlLblOpenBooks);
      this.pnlTasksOpenBooks.Dock = DockStyle.Top;
      this.pnlTasksOpenBooks.Location = new Point(0, 27);
      this.pnlTasksOpenBooks.Name = "pnlTasksOpenBooks";
      this.pnlTasksOpenBooks.Padding = new Padding(15, 10, 15, 0);
      this.pnlTasksOpenBooks.Size = new Size(149, 104);
      this.pnlTasksOpenBooks.TabIndex = 11;
      this.pnlTasksOpenBooks.Tag = (object) "";
      this.lblOpenBooksSearch.Cursor = Cursors.Hand;
      this.lblOpenBooksSearch.Dock = DockStyle.Top;
      this.lblOpenBooksSearch.Location = new Point(15, 70);
      this.lblOpenBooksSearch.Name = "lblOpenBooksSearch";
      this.lblOpenBooksSearch.Padding = new Padding(20, 0, 0, 0);
      this.lblOpenBooksSearch.Size = new Size(119, 16);
      this.lblOpenBooksSearch.TabIndex = 17;
      this.lblOpenBooksSearch.Text = "Search";
      this.lblOpenBooksSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.lblOpenBooksSearch.Click += new EventHandler(this.lblOpenBooksSearch_Click);
      this.lblOpenBooksBookmarks.Cursor = Cursors.Hand;
      this.lblOpenBooksBookmarks.Dock = DockStyle.Top;
      this.lblOpenBooksBookmarks.Location = new Point(15, 54);
      this.lblOpenBooksBookmarks.Name = "lblOpenBooksBookmarks";
      this.lblOpenBooksBookmarks.Padding = new Padding(20, 0, 0, 0);
      this.lblOpenBooksBookmarks.Size = new Size(119, 16);
      this.lblOpenBooksBookmarks.TabIndex = 16;
      this.lblOpenBooksBookmarks.Text = "Bookmarks";
      this.lblOpenBooksBookmarks.TextAlign = ContentAlignment.MiddleLeft;
      this.lblOpenBooksBookmarks.Click += new EventHandler(this.lblOpenBooksBookmarks_Click);
      this.lblOpenBooksBrowse.Cursor = Cursors.Hand;
      this.lblOpenBooksBrowse.Dock = DockStyle.Top;
      this.lblOpenBooksBrowse.Location = new Point(15, 38);
      this.lblOpenBooksBrowse.Name = "lblOpenBooksBrowse";
      this.lblOpenBooksBrowse.Padding = new Padding(20, 0, 0, 0);
      this.lblOpenBooksBrowse.Size = new Size(119, 16);
      this.lblOpenBooksBrowse.TabIndex = 15;
      this.lblOpenBooksBrowse.Text = "Browse";
      this.lblOpenBooksBrowse.TextAlign = ContentAlignment.MiddleLeft;
      this.lblOpenBooksBrowse.Click += new EventHandler(this.lblOpenBooksBrowse_Click);
      this.pnlLblOpenBooks.Controls.Add((Control) this.lblOpenBooksLine);
      this.pnlLblOpenBooks.Controls.Add((Control) this.lblOpenBooks);
      this.pnlLblOpenBooks.Dock = DockStyle.Top;
      this.pnlLblOpenBooks.Location = new Point(15, 10);
      this.pnlLblOpenBooks.Name = "pnlLblOpenBooks";
      this.pnlLblOpenBooks.Size = new Size(119, 28);
      this.pnlLblOpenBooks.TabIndex = 20;
      this.lblOpenBooksLine.BackColor = Color.Transparent;
      this.lblOpenBooksLine.Dock = DockStyle.Fill;
      this.lblOpenBooksLine.ForeColor = Color.Blue;
      this.lblOpenBooksLine.Image = (Image) Resources.GroupLine0;
      this.lblOpenBooksLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblOpenBooksLine.Location = new Point(66, 0);
      this.lblOpenBooksLine.Name = "lblOpenBooksLine";
      this.lblOpenBooksLine.Size = new Size(53, 28);
      this.lblOpenBooksLine.TabIndex = 15;
      this.lblOpenBooksLine.Tag = (object) "";
      this.lblOpenBooksLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblOpenBooks.BackColor = Color.Transparent;
      this.lblOpenBooks.Dock = DockStyle.Left;
      this.lblOpenBooks.ForeColor = Color.Blue;
      this.lblOpenBooks.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblOpenBooks.Location = new Point(0, 0);
      this.lblOpenBooks.Name = "lblOpenBooks";
      this.lblOpenBooks.Size = new Size(66, 28);
      this.lblOpenBooks.TabIndex = 13;
      this.lblOpenBooks.Tag = (object) "";
      this.lblOpenBooks.Text = "Open Books";
      this.lblOpenBooks.TextAlign = ContentAlignment.MiddleLeft;
      this.lblTasksTitle.BackColor = Color.White;
      this.lblTasksTitle.Dock = DockStyle.Top;
      this.lblTasksTitle.ForeColor = Color.Black;
      this.lblTasksTitle.Location = new Point(0, 0);
      this.lblTasksTitle.Name = "lblTasksTitle";
      this.lblTasksTitle.Padding = new Padding(3, 7, 0, 0);
      this.lblTasksTitle.Size = new Size(149, 27);
      this.lblTasksTitle.TabIndex = 6;
      this.lblTasksTitle.Text = "Tasks";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(151, 398);
      this.Controls.Add((Control) this.pnlTasks);
      this.Font = new Font("Tahoma", 8.25f);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmOpenBookTasks);
      this.Text = "Form3";
      this.Load += new EventHandler(this.frmOpenBookTasks_Load);
      this.pnlTasks.ResumeLayout(false);
      this.pnlTasksSelectViews.ResumeLayout(false);
      this.pnlLblSelectViews.ResumeLayout(false);
      this.pnlTasksAdvSrch.ResumeLayout(false);
      this.pnlLblAdvancedSearch.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.pnlTasksOpenBooks.ResumeLayout(false);
      this.pnlLblOpenBooks.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmOpenBookTasks(frmOpenBook frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmOpenBookTasks_Load(object sender, EventArgs e)
    {
      try
      {
        this.lblOpenBooksBrowse.Visible = Program.objAppFeatures.bOpenBookScreen;
        if (this.lblOpenBooksBrowse.Visible)
          this.lblOpenBooksBrowse_Click((object) null, (EventArgs) null);
        else
          this.lblOpenBooksBookmarks_Click((object) null, (EventArgs) null);
        this.lblAdvSrchPageName.Visible = Program.objAppFeatures.bAdvanceSearchPageName;
        this.lblAdvSrchPartName.Visible = Program.objAppFeatures.bAdvanceSearchPartName;
        this.lblAdvSrchPartNo.Visible = Program.objAppFeatures.bAdvanceSearchPartNumber;
        this.pnlTasksAdvSrch.Visible = Program.objAppFeatures.bAdvanceSearchPageName || Program.objAppFeatures.bAdvanceSearchPartName || Program.objAppFeatures.bAdvanceSearchPartNumber;
      }
      catch
      {
      }
    }

    private void HighlightList(ref Label lbl)
    {
      this.pnlTasks.SuspendLayout();
      this.lblOpenBooksSearch.BackColor = this.pnlTasksOpenBooks.BackColor;
      this.lblOpenBooksSearch.ForeColor = this.pnlTasksOpenBooks.ForeColor;
      this.lblOpenBooksBrowse.BackColor = this.pnlTasksOpenBooks.BackColor;
      this.lblOpenBooksBrowse.ForeColor = this.pnlTasksOpenBooks.ForeColor;
      this.lblOpenBooksBookmarks.BackColor = this.pnlTasksOpenBooks.BackColor;
      this.lblOpenBooksBookmarks.ForeColor = this.pnlTasksOpenBooks.ForeColor;
      this.lblAdvSrchPageName.BackColor = this.pnlTasksAdvSrch.BackColor;
      this.lblAdvSrchPageName.ForeColor = this.pnlTasksAdvSrch.ForeColor;
      this.lblAdvSrchPartName.BackColor = this.pnlTasksAdvSrch.BackColor;
      this.lblAdvSrchPartName.ForeColor = this.pnlTasksAdvSrch.ForeColor;
      this.lblAdvSrchPartNo.BackColor = this.pnlTasksAdvSrch.BackColor;
      this.lblAdvSrchPartNo.ForeColor = this.pnlTasksAdvSrch.ForeColor;
      lbl.BackColor = Settings.Default.appHighlightBackColor;
      lbl.ForeColor = Settings.Default.appHighlightForeColor;
      this.pnlTasks.ResumeLayout();
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblTasksTitle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      if (this.lblOpenBooksBrowse.BackColor != this.pnlTasksOpenBooks.BackColor && this.lblOpenBooksBrowse.BackColor != this.pnlTasksOpenBooks.ForeColor)
      {
        this.lblOpenBooksBrowse.BackColor = Settings.Default.appHighlightBackColor;
        this.lblOpenBooksBrowse.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblOpenBooksBookmarks.BackColor != this.pnlTasksOpenBooks.BackColor && this.lblOpenBooksBookmarks.BackColor != this.pnlTasksOpenBooks.ForeColor)
      {
        this.lblOpenBooksBookmarks.BackColor = Settings.Default.appHighlightBackColor;
        this.lblOpenBooksBookmarks.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblOpenBooksSearch.BackColor != this.pnlTasksOpenBooks.BackColor && this.lblOpenBooksSearch.BackColor != this.pnlTasksOpenBooks.ForeColor)
      {
        this.lblOpenBooksSearch.BackColor = Settings.Default.appHighlightBackColor;
        this.lblOpenBooksSearch.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblSelectViewsTreeView.BackColor != this.pnlTasksSelectViews.BackColor && this.lblSelectViewsTreeView.BackColor != this.pnlTasksSelectViews.ForeColor)
      {
        this.lblSelectViewsTreeView.BackColor = Settings.Default.appHighlightBackColor;
        this.lblSelectViewsTreeView.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblSelectViewsGridView.BackColor != this.pnlTasksSelectViews.BackColor && this.lblSelectViewsGridView.BackColor != this.pnlTasksSelectViews.ForeColor)
      {
        this.lblSelectViewsGridView.BackColor = Settings.Default.appHighlightBackColor;
        this.lblSelectViewsGridView.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblSelectViewsExplorerView.BackColor != this.pnlTasksSelectViews.BackColor && this.lblSelectViewsExplorerView.BackColor != this.pnlTasksSelectViews.ForeColor)
      {
        this.lblSelectViewsExplorerView.BackColor = Settings.Default.appHighlightBackColor;
        this.lblSelectViewsExplorerView.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblSelectViewsImagePreview.BackColor != this.pnlTasksSelectViews.BackColor && this.lblSelectViewsImagePreview.BackColor != this.pnlTasksSelectViews.ForeColor)
      {
        this.lblSelectViewsImagePreview.BackColor = Settings.Default.appHighlightBackColor;
        this.lblSelectViewsImagePreview.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblAdvSrchPageName.BackColor != this.pnlTasksAdvSrch.BackColor && this.lblAdvSrchPageName.BackColor != this.pnlTasksAdvSrch.ForeColor)
      {
        this.lblAdvSrchPageName.BackColor = Settings.Default.appHighlightBackColor;
        this.lblAdvSrchPageName.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else if (this.lblAdvSrchPartName.BackColor != this.pnlTasksAdvSrch.BackColor && this.lblAdvSrchPartName.BackColor != this.pnlTasksAdvSrch.ForeColor)
      {
        this.lblAdvSrchPartName.BackColor = Settings.Default.appHighlightBackColor;
        this.lblAdvSrchPartName.ForeColor = Settings.Default.appHighlightForeColor;
      }
      else
      {
        if (!(this.lblAdvSrchPartNo.BackColor != this.pnlTasksAdvSrch.BackColor) || !(this.lblAdvSrchPartNo.BackColor != this.pnlTasksAdvSrch.ForeColor))
          return;
        this.lblAdvSrchPartNo.BackColor = Settings.Default.appHighlightBackColor;
        this.lblAdvSrchPartNo.ForeColor = Settings.Default.appHighlightForeColor;
      }
    }

    public void lblOpenBooksBrowse_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblOpenBooksBrowse);
      this.frmParent.HideForms();
      this.frmParent.objFrmBrowse.Show();
    }

    public void lblOpenBooksBookmarks_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblOpenBooksBookmarks);
      this.frmParent.HideForms();
      this.frmParent.objFrmBookmark.Show();
    }

    public void lblOpenBooksSearch_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblOpenBooksSearch);
      this.frmParent.HideForms();
      this.frmParent.objFrmSearch.Show();
    }

    public void lblAdvSrchPageName_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAdvSrchPageName);
      this.frmParent.HideForms();
      this.frmParent.objFrmPageNameAdvSrch.Show();
    }

    public void lblAdvSrchPartName_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAdvSrchPartName);
      this.frmParent.HideForms();
      this.frmParent.objFrmPartNameAdvSrch.Show();
    }

    public void lblAdvSrchPartNo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAdvSrchPartNo);
      this.frmParent.HideForms();
      this.frmParent.objFrmPartNumberAdvSrch.Show();
    }

    private void lblSelectViewsTreeView_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblSelectViewsTreeView);
    }

    private void lblSelectViewsGridView_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblSelectViewsGridView);
    }

    private void lblSelectViewsExplorerView_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblSelectViewsExplorerView);
    }

    private void lblSelectViewsImagePreview_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblSelectViewsImagePreview);
    }

    public void LoadResources()
    {
      this.lblTasksTitle.Text = this.GetResource("Tasks", "TASKS", ResourceType.LABEL);
      this.lblOpenBooksBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.LABEL);
      this.lblOpenBooksBookmarks.Text = this.GetResource("Bookmarks", "BOOKMARKS", ResourceType.LABEL);
      this.lblOpenBooksSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.LABEL);
      this.lblSelectViewsTreeView.Text = this.GetResource("Tree View", "TREE_VIEW", ResourceType.LABEL);
      this.lblSelectViewsGridView.Text = this.GetResource("Grid View", "GRID_VIEW", ResourceType.LABEL);
      this.lblSelectViewsExplorerView.Text = this.GetResource("Explorer View", "EXPLORER_VIEW", ResourceType.LABEL);
      this.lblSelectViewsImagePreview.Text = this.GetResource("Image View", "IMAGE_VIEW", ResourceType.LABEL);
      this.lblOpenBooks.Text = this.GetResource("Open Books", "OPEN_BOOKS", ResourceType.LABEL);
      this.lblSelectViews.Text = this.GetResource("Select Views", "SELECT_VIEWS", ResourceType.LABEL);
      this.lblAdvancedSearch.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.LABEL);
      this.lblAdvSrchPageName.Text = this.GetResource("Page Name", "PAGE_NAME", ResourceType.LABEL);
      this.lblAdvSrchPartName.Text = this.GetResource("Part Name", "PART_NAME", ResourceType.LABEL);
      this.lblAdvSrchPartNo.Text = this.GetResource("Part Number", "PART_NUMBER", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='OPEN_BOOKS']" + "/Screen[@Name='OPEN_BOOK_TASKS']";
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
            return this.frmParent.frmParent.GetResourceValue(sDefaultValue, xQuery1);
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
        return this.frmParent.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmViewerPartslist
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
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
  public class frmViewerPartslist : DockContent
  {
    private const string dllZipper = "ZIPPER.dll";
    private IContainer components;
    protected Panel pnlForm;
    public PictureBox picLoading;
    private BackgroundWorker bgWorker;
    private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
    private ContextMenuStrip cmsPartslist;
    private ToolStripMenuItem tsmAddPartMemo;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem tsmClearSelection;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private ToolStripMenuItem tsmAddToSelectionList;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem selectAllToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem copyToClipboardToolStripMenuItem;
    private ToolStripMenuItem commaSeparatedToolStripMenuItem;
    private ToolStripMenuItem tabSeparatedToolStripMenuItem;
    private ToolStripMenuItem exportToFileToolStripMenuItem;
    private ToolStripMenuItem commaSeparatedToolStripMenuItem1;
    private ToolStripMenuItem tabSeparatedToolStripMenuItem1;
    private SaveFileDialog dlgSaveFile;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripMenuItem nextListToolStripMenuItem;
    private ToolStripMenuItem previousListToolStripMenuItem;
    private ContextMenuStrip cmsReference;
    private ToolStrip toolStrip1;
    private ToolStripButton tsBtnNext;
    private ToolStripTextBox tsTxtList;
    private ToolStripButton tsBtnPrev;
    private ToolStripButton tsbClearSelection;
    private ToolStripButton tsbSelectAll;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton tsbAddPartMemo;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripButton tsbAddToSelectionList;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton tsbPartistInfo;
    public Panel pnlInfo;
    public Label lblPartsListInfo;
    private TabControl tabControl1;
    private Panel pnlGrids;
    private ToolStripMenuItem rowSelectionModeToolStripMenuItem;
    private SplitContainer splitPnlGrids;
    public DataGridView dgPartslist;
    private DataGridViewTextBoxColumn Column1;
    public DataGridView dgJumps;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private frmViewer frmParent;
    private List<int> lstDisableRows;
    private int curServerId;
    private XmlNode curListSchema;
    private XmlNode curPageSchema;
    private XmlNode curPageNode;
    private int curPicIndex;
    private int curListIndex;
    private string attPicElement;
    private string attListElement;
    private string attUpdateDateElement;
    private bool picLoadedSuccessfully;
    public bool isWorking;
    public string highlightPartNo;
    private string attPartNoElement;
    private string attPartNameElement;
    private string attPictureFileElement;
    private string attPartStatusElement;
    private string sPListStatusColName;
    private string curPictureFileName;
    private string statusText;
    private string partlistInfoMsg;
    private bool p_Encrypted;
    private bool p_Compressed;
    public ArrayList attAdminMemList;
    public string attListUpdateDateElement;
    private string BookPublishingId;
    private string sPLTitle;
    private Download objDownloader;
    public XmlNodeList objXmlNodeList;
    private bool bSelectionMode;
    public int intMemoType;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmViewerPartslist));
      this.pnlForm = new Panel();
      this.pnlGrids = new Panel();
      this.splitPnlGrids = new SplitContainer();
      this.dgPartslist = new DataGridView();
      this.Column1 = new DataGridViewTextBoxColumn();
      this.cmsPartslist = new ContextMenuStrip(this.components);
      this.tsmAddPartMemo = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.tsmAddToSelectionList = new ToolStripMenuItem();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.tsmClearSelection = new ToolStripMenuItem();
      this.selectAllToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator6 = new ToolStripSeparator();
      this.copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
      this.commaSeparatedToolStripMenuItem = new ToolStripMenuItem();
      this.tabSeparatedToolStripMenuItem = new ToolStripMenuItem();
      this.exportToFileToolStripMenuItem = new ToolStripMenuItem();
      this.commaSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
      this.tabSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
      this.toolStripSeparator7 = new ToolStripSeparator();
      this.nextListToolStripMenuItem = new ToolStripMenuItem();
      this.previousListToolStripMenuItem = new ToolStripMenuItem();
      this.rowSelectionModeToolStripMenuItem = new ToolStripMenuItem();
      this.dgJumps = new DataGridView();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.picLoading = new PictureBox();
      this.tabControl1 = new TabControl();
      this.pnlInfo = new Panel();
      this.lblPartsListInfo = new Label();
      this.toolStrip1 = new ToolStrip();
      this.tsBtnNext = new ToolStripButton();
      this.tsTxtList = new ToolStripTextBox();
      this.tsBtnPrev = new ToolStripButton();
      this.tsbClearSelection = new ToolStripButton();
      this.tsbSelectAll = new ToolStripButton();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.tsbAddPartMemo = new ToolStripButton();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.tsbAddToSelectionList = new ToolStripButton();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.tsbPartistInfo = new ToolStripButton();
      this.bgWorker = new BackgroundWorker();
      this.dlgSaveFile = new SaveFileDialog();
      this.cmsReference = new ContextMenuStrip(this.components);
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewCheckBoxColumn2 = new DataGridViewCheckBoxColumn();
      this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
      this.pnlForm.SuspendLayout();
      this.pnlGrids.SuspendLayout();
      this.splitPnlGrids.Panel1.SuspendLayout();
      this.splitPnlGrids.Panel2.SuspendLayout();
      this.splitPnlGrids.SuspendLayout();
      ((ISupportInitialize) this.dgPartslist).BeginInit();
      this.cmsPartslist.SuspendLayout();
      ((ISupportInitialize) this.dgJumps).BeginInit();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.pnlInfo.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlGrids);
      this.pnlForm.Controls.Add((Control) this.pnlInfo);
      this.pnlForm.Controls.Add((Control) this.toolStrip1);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(667, 366);
      this.pnlForm.TabIndex = 3;
      this.pnlGrids.Controls.Add((Control) this.splitPnlGrids);
      this.pnlGrids.Controls.Add((Control) this.picLoading);
      this.pnlGrids.Controls.Add((Control) this.tabControl1);
      this.pnlGrids.Dock = DockStyle.Fill;
      this.pnlGrids.Location = new Point(0, 46);
      this.pnlGrids.Name = "pnlGrids";
      this.pnlGrids.Size = new Size(665, 318);
      this.pnlGrids.TabIndex = 25;
      this.splitPnlGrids.BorderStyle = BorderStyle.FixedSingle;
      this.splitPnlGrids.Dock = DockStyle.Fill;
      this.splitPnlGrids.Location = new Point(0, 0);
      this.splitPnlGrids.Margin = new Padding(0);
      this.splitPnlGrids.Name = "splitPnlGrids";
      this.splitPnlGrids.Orientation = Orientation.Horizontal;
      this.splitPnlGrids.Panel1.Controls.Add((Control) this.dgPartslist);
      this.splitPnlGrids.Panel2.Controls.Add((Control) this.dgJumps);
      this.splitPnlGrids.Size = new Size(665, 318);
      this.splitPnlGrids.SplitterDistance = 148;
      this.splitPnlGrids.SplitterWidth = 3;
      this.splitPnlGrids.TabIndex = 25;
      this.splitPnlGrids.SplitterMoved += new SplitterEventHandler(this.splitPnlGrids_SplitterMoved);
      this.dgPartslist.AllowUserToAddRows = false;
      this.dgPartslist.AllowUserToDeleteRows = false;
      this.dgPartslist.AllowUserToResizeRows = false;
      this.dgPartslist.BackgroundColor = Color.White;
      this.dgPartslist.BorderStyle = BorderStyle.None;
      this.dgPartslist.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
      this.dgPartslist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgPartslist.Columns.AddRange((DataGridViewColumn) this.Column1);
      this.dgPartslist.ContextMenuStrip = this.cmsPartslist;
      this.dgPartslist.Dock = DockStyle.Fill;
      this.dgPartslist.Location = new Point(0, 0);
      this.dgPartslist.Name = "dgPartslist";
      this.dgPartslist.RowHeadersVisible = false;
      this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgPartslist.Size = new Size(663, 146);
      this.dgPartslist.TabIndex = 22;
      this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
      this.dgPartslist.CellMouseClick += new DataGridViewCellMouseEventHandler(this.dgPartslist_CellMouseClick);
      this.dgPartslist.SortCompare += new DataGridViewSortCompareEventHandler(this.dgPartslist_SortCompare);
      this.dgPartslist.Sorted += new EventHandler(this.dgPartslist_Sorted);
      this.dgPartslist.CellDoubleClick += new DataGridViewCellEventHandler(this.dgPartslist_CellDoubleClick);
      this.dgPartslist.MouseMove += new MouseEventHandler(this.dgPartslist_MouseMove);
      this.dgPartslist.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgPartslist_RowsAdded);
      this.dgPartslist.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgPartslist_CellPainting);
      this.dgPartslist.CellClick += new DataGridViewCellEventHandler(this.dgPartslist_CellClick);
      this.dgPartslist.CurrentCellDirtyStateChanged += new EventHandler(this.dgPartslist_CurrentCellDirtyStateChanged);
      this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
      this.Column1.HeaderText = "PartsDetails";
      this.Column1.Name = "Column1";
      this.Column1.Width = 1000;
      this.cmsPartslist.Items.AddRange(new ToolStripItem[13]
      {
        (ToolStripItem) this.tsmAddPartMemo,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.tsmAddToSelectionList,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.tsmClearSelection,
        (ToolStripItem) this.selectAllToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator6,
        (ToolStripItem) this.copyToClipboardToolStripMenuItem,
        (ToolStripItem) this.exportToFileToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator7,
        (ToolStripItem) this.nextListToolStripMenuItem,
        (ToolStripItem) this.previousListToolStripMenuItem,
        (ToolStripItem) this.rowSelectionModeToolStripMenuItem
      });
      this.cmsPartslist.Name = "cmsPartslist";
      this.cmsPartslist.Size = new Size(186, 226);
      this.cmsPartslist.Opening += new CancelEventHandler(this.cmsPartslist_Opening);
      this.tsmAddPartMemo.Name = "tsmAddPartMemo";
      this.tsmAddPartMemo.Size = new Size(185, 22);
      this.tsmAddPartMemo.Text = "Add Part Memo";
      this.tsmAddPartMemo.Click += new EventHandler(this.tsmAddPartMemo_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(182, 6);
      this.tsmAddToSelectionList.Name = "tsmAddToSelectionList";
      this.tsmAddToSelectionList.Size = new Size(185, 22);
      this.tsmAddToSelectionList.Text = "Add To Selection List";
      this.tsmAddToSelectionList.Click += new EventHandler(this.tsmAddToSelectionList_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(182, 6);
      this.tsmClearSelection.Name = "tsmClearSelection";
      this.tsmClearSelection.Size = new Size(185, 22);
      this.tsmClearSelection.Text = "Clear Selection";
      this.tsmClearSelection.Click += new EventHandler(this.tsmClearSelection_Click);
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.Size = new Size(185, 22);
      this.selectAllToolStripMenuItem.Text = "Select All";
      this.selectAllToolStripMenuItem.Click += new EventHandler(this.selectAllToolStripMenuItem_Click);
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new Size(182, 6);
      this.copyToClipboardToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.commaSeparatedToolStripMenuItem,
        (ToolStripItem) this.tabSeparatedToolStripMenuItem
      });
      this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
      this.copyToClipboardToolStripMenuItem.Size = new Size(185, 22);
      this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
      this.commaSeparatedToolStripMenuItem.Name = "commaSeparatedToolStripMenuItem";
      this.commaSeparatedToolStripMenuItem.Size = new Size(172, 22);
      this.commaSeparatedToolStripMenuItem.Text = "Comma Separated";
      this.commaSeparatedToolStripMenuItem.Click += new EventHandler(this.commaSeparatedToolStripMenuItem_Click);
      this.tabSeparatedToolStripMenuItem.Name = "tabSeparatedToolStripMenuItem";
      this.tabSeparatedToolStripMenuItem.Size = new Size(172, 22);
      this.tabSeparatedToolStripMenuItem.Text = "Tab Separated";
      this.tabSeparatedToolStripMenuItem.Click += new EventHandler(this.tabSeparatedToolStripMenuItem_Click);
      this.exportToFileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.commaSeparatedToolStripMenuItem1,
        (ToolStripItem) this.tabSeparatedToolStripMenuItem1
      });
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new Size(185, 22);
      this.exportToFileToolStripMenuItem.Text = "Export To File";
      this.commaSeparatedToolStripMenuItem1.Name = "commaSeparatedToolStripMenuItem1";
      this.commaSeparatedToolStripMenuItem1.Size = new Size(172, 22);
      this.commaSeparatedToolStripMenuItem1.Text = "Comma Separated";
      this.commaSeparatedToolStripMenuItem1.Click += new EventHandler(this.commaSeparatedToolStripMenuItem1_Click);
      this.tabSeparatedToolStripMenuItem1.Name = "tabSeparatedToolStripMenuItem1";
      this.tabSeparatedToolStripMenuItem1.Size = new Size(172, 22);
      this.tabSeparatedToolStripMenuItem1.Text = "Tab Separated";
      this.tabSeparatedToolStripMenuItem1.Click += new EventHandler(this.tabSeparatedToolStripMenuItem1_Click);
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new Size(182, 6);
      this.nextListToolStripMenuItem.Name = "nextListToolStripMenuItem";
      this.nextListToolStripMenuItem.Size = new Size(185, 22);
      this.nextListToolStripMenuItem.Text = "Next List";
      this.nextListToolStripMenuItem.Click += new EventHandler(this.nextListToolStripMenuItem_Click);
      this.previousListToolStripMenuItem.Name = "previousListToolStripMenuItem";
      this.previousListToolStripMenuItem.Size = new Size(185, 22);
      this.previousListToolStripMenuItem.Text = "Previous List";
      this.previousListToolStripMenuItem.Click += new EventHandler(this.previousListToolStripMenuItem_Click);
      this.rowSelectionModeToolStripMenuItem.Name = "rowSelectionModeToolStripMenuItem";
      this.rowSelectionModeToolStripMenuItem.Size = new Size(185, 22);
      this.rowSelectionModeToolStripMenuItem.Text = "RowSelectionMode";
      this.rowSelectionModeToolStripMenuItem.Click += new EventHandler(this.rowSelectionModeToolStripMenuItem_Click);
      this.dgJumps.AllowUserToAddRows = false;
      this.dgJumps.AllowUserToDeleteRows = false;
      this.dgJumps.AllowUserToResizeRows = false;
      this.dgJumps.BackgroundColor = Color.White;
      this.dgJumps.BorderStyle = BorderStyle.None;
      this.dgJumps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgJumps.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn2);
      this.dgJumps.Dock = DockStyle.Fill;
      this.dgJumps.Location = new Point(0, 0);
      this.dgJumps.Name = "dgJumps";
      this.dgJumps.RowHeadersVisible = false;
      this.dgJumps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgJumps.Size = new Size(663, 165);
      this.dgJumps.TabIndex = 23;
      this.dgJumps.CellDoubleClick += new DataGridViewCellEventHandler(this.dgJumps_CellDoubleClick);
      this.dataGridViewTextBoxColumn2.HeaderText = "Jumps";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.Width = 1000;
      this.picLoading.BackColor = Color.White;
      this.picLoading.Dock = DockStyle.Fill;
      this.picLoading.Image = (Image) GSPcLocalViewer.Properties.Resources.Loading1;
      this.picLoading.Location = new Point(0, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(665, 318);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 19;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.tabControl1.Dock = DockStyle.Top;
      this.tabControl1.HotTrack = true;
      this.tabControl1.Location = new Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(665, 0);
      this.tabControl1.TabIndex = 24;
      this.pnlInfo.AutoSize = true;
      this.pnlInfo.BackColor = SystemColors.Control;
      this.pnlInfo.BorderStyle = BorderStyle.FixedSingle;
      this.pnlInfo.Controls.Add((Control) this.lblPartsListInfo);
      this.pnlInfo.Dock = DockStyle.Top;
      this.pnlInfo.Location = new Point(0, 25);
      this.pnlInfo.Name = "pnlInfo";
      this.pnlInfo.Padding = new Padding(3);
      this.pnlInfo.Size = new Size(665, 21);
      this.pnlInfo.TabIndex = 23;
      this.lblPartsListInfo.AutoSize = true;
      this.lblPartsListInfo.Dock = DockStyle.Fill;
      this.lblPartsListInfo.Location = new Point(3, 3);
      this.lblPartsListInfo.Name = "lblPartsListInfo";
      this.lblPartsListInfo.Size = new Size(119, 13);
      this.lblPartsListInfo.TabIndex = 0;
      this.lblPartsListInfo.Text = "Parts List Info Message";
      this.toolStrip1.BackColor = SystemColors.Control;
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new ToolStripItem[11]
      {
        (ToolStripItem) this.tsBtnNext,
        (ToolStripItem) this.tsTxtList,
        (ToolStripItem) this.tsBtnPrev,
        (ToolStripItem) this.tsbClearSelection,
        (ToolStripItem) this.tsbSelectAll,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.tsbAddPartMemo,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.tsbAddToSelectionList,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.tsbPartistInfo
      });
      this.toolStrip1.Location = new Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RightToLeft = RightToLeft.Yes;
      this.toolStrip1.Size = new Size(665, 25);
      this.toolStrip1.TabIndex = 22;
      this.tsBtnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnNext.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Next;
      this.tsBtnNext.ImageTransparentColor = Color.Magenta;
      this.tsBtnNext.Name = "tsBtnNext";
      this.tsBtnNext.Size = new Size(23, 22);
      this.tsBtnNext.Text = "Next List";
      this.tsBtnNext.Click += new EventHandler(this.tsBtnNext_Click);
      this.tsTxtList.AutoSize = false;
      this.tsTxtList.BorderStyle = BorderStyle.FixedSingle;
      this.tsTxtList.Name = "tsTxtList";
      this.tsTxtList.ReadOnly = true;
      this.tsTxtList.ShortcutsEnabled = false;
      this.tsTxtList.Size = new Size(50, 23);
      this.tsTxtList.TextBoxTextAlign = HorizontalAlignment.Center;
      this.tsBtnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnPrev.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Prev;
      this.tsBtnPrev.ImageTransparentColor = Color.Magenta;
      this.tsBtnPrev.Name = "tsBtnPrev";
      this.tsBtnPrev.Size = new Size(23, 22);
      this.tsBtnPrev.Text = "Previous List";
      this.tsBtnPrev.Click += new EventHandler(this.tsBtnPrev_Click);
      this.tsbClearSelection.Alignment = ToolStripItemAlignment.Right;
      this.tsbClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbClearSelection.Image = (Image) GSPcLocalViewer.Properties.Resources.PartsList_clear_selection;
      this.tsbClearSelection.ImageTransparentColor = Color.Magenta;
      this.tsbClearSelection.Name = "tsbClearSelection";
      this.tsbClearSelection.RightToLeft = RightToLeft.No;
      this.tsbClearSelection.Size = new Size(23, 22);
      this.tsbClearSelection.Text = "Clear Selection";
      this.tsbClearSelection.Click += new EventHandler(this.tsbClearSelection_Click);
      this.tsbSelectAll.Alignment = ToolStripItemAlignment.Right;
      this.tsbSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSelectAll.Image = (Image) componentResourceManager.GetObject("tsbSelectAll.Image");
      this.tsbSelectAll.ImageTransparentColor = Color.Magenta;
      this.tsbSelectAll.Name = "tsbSelectAll";
      this.tsbSelectAll.RightToLeft = RightToLeft.No;
      this.tsbSelectAll.Size = new Size(23, 22);
      this.tsbSelectAll.Text = "Select All";
      this.tsbSelectAll.Click += new EventHandler(this.tsbSelectAll_Click);
      this.toolStripSeparator4.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(6, 25);
      this.tsbAddPartMemo.Alignment = ToolStripItemAlignment.Right;
      this.tsbAddPartMemo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbAddPartMemo.Enabled = false;
      this.tsbAddPartMemo.Image = (Image) GSPcLocalViewer.Properties.Resources.Add_Memo;
      this.tsbAddPartMemo.ImageTransparentColor = Color.Magenta;
      this.tsbAddPartMemo.Name = "tsbAddPartMemo";
      this.tsbAddPartMemo.RightToLeft = RightToLeft.No;
      this.tsbAddPartMemo.Size = new Size(23, 22);
      this.tsbAddPartMemo.Text = "Add Part Memo";
      this.tsbAddPartMemo.Click += new EventHandler(this.tsmAddPartMemo_Click);
      this.toolStripSeparator5.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(6, 25);
      this.tsbAddToSelectionList.Alignment = ToolStripItemAlignment.Right;
      this.tsbAddToSelectionList.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbAddToSelectionList.Enabled = false;
      this.tsbAddToSelectionList.Image = (Image) componentResourceManager.GetObject("tsbAddToSelectionList.Image");
      this.tsbAddToSelectionList.ImageTransparentColor = Color.Magenta;
      this.tsbAddToSelectionList.Name = "tsbAddToSelectionList";
      this.tsbAddToSelectionList.RightToLeft = RightToLeft.No;
      this.tsbAddToSelectionList.Size = new Size(23, 22);
      this.tsbAddToSelectionList.Text = "Add To Selection List";
      this.tsbAddToSelectionList.Click += new EventHandler(this.tsbAddToSelectionList_Click);
      this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(6, 25);
      this.tsbPartistInfo.Alignment = ToolStripItemAlignment.Right;
      this.tsbPartistInfo.CheckOnClick = true;
      this.tsbPartistInfo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPartistInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.PartsList_Info;
      this.tsbPartistInfo.ImageTransparentColor = Color.Magenta;
      this.tsbPartistInfo.Name = "tsbPartistInfo";
      this.tsbPartistInfo.Size = new Size(23, 22);
      this.tsbPartistInfo.Click += new EventHandler(this.tsbPartistInfo_Click);
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.cmsReference.Name = "cmsReference";
      this.cmsReference.Size = new Size(61, 4);
      this.cmsReference.ItemClicked += new ToolStripItemClickedEventHandler(this.cmsReference_ItemClicked);
      this.dataGridViewTextBoxColumn1.HeaderText = "PartsDetails";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.Width = 1000;
      this.dataGridViewCheckBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewCheckBoxColumn2.Frozen = true;
      this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
      this.dataGridViewCheckBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn13.HeaderText = "temp";
      this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(667, 366);
      this.Controls.Add((Control) this.pnlForm);
      this.DoubleBuffered = true;
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.HideOnClose = true;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmViewerPartslist);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Partslist";
      this.Load += new EventHandler(this.frmViewerPartslist_Load);
      this.VisibleChanged += new EventHandler(this.frmViewerPartslist_VisibleChanged);
      this.FormClosing += new FormClosingEventHandler(this.frmViewerPartslist_FormClosing);
      this.pnlForm.ResumeLayout(false);
      this.pnlForm.PerformLayout();
      this.pnlGrids.ResumeLayout(false);
      this.splitPnlGrids.Panel1.ResumeLayout(false);
      this.splitPnlGrids.Panel2.ResumeLayout(false);
      this.splitPnlGrids.ResumeLayout(false);
      ((ISupportInitialize) this.dgPartslist).EndInit();
      this.cmsPartslist.ResumeLayout(false);
      ((ISupportInitialize) this.dgJumps).EndInit();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.pnlInfo.ResumeLayout(false);
      this.pnlInfo.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
    }

    [DllImport("user32.dll")]
    public static extern long HideCaret(IntPtr hwnd);

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool SetActiveWindow(IntPtr hWnd);

    public frmViewerPartslist(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.curServerId = 9999;
      this.curListSchema = (XmlNode) null;
      this.curPageSchema = (XmlNode) null;
      this.curPageNode = (XmlNode) null;
      this.curPicIndex = 0;
      this.curListIndex = 0;
      this.attPicElement = string.Empty;
      this.attListElement = string.Empty;
      this.attUpdateDateElement = string.Empty;
      this.picLoadedSuccessfully = false;
      this.isWorking = false;
      this.highlightPartNo = string.Empty;
      this.attPartNoElement = string.Empty;
      this.attPartNameElement = string.Empty;
      this.attPartStatusElement = string.Empty;
      this.attPictureFileElement = string.Empty;
      this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
      this.curPictureFileName = string.Empty;
      this.attAdminMemList = new ArrayList();
      this.tsbAddPartMemo.Enabled = false;
      this.tsmAddPartMemo.Enabled = false;
      this.frmParent.EnableAddPartMemoMenu(false);
      this.BookPublishingId = string.Empty;
      this.objDownloader = new Download(this.frmParent);
      this.UpdateFont();
      this.LoadResources();
      this.tsbAddPartMemo.Visible = Program.objAppFeatures.bMemo;
      this.toolStripSeparator5.Visible = Program.objAppFeatures.bMemo;
      this.rowSelectionModeToolStripMenuItem.Visible = Program.objAppFeatures.bPListSelMode;
    }

    private void frmViewerPartslist_Load(object sender, EventArgs e)
    {
      this.OnOffFeatures();
      this.rowSelectionModeToolStripMenuItem.Checked = Settings.Default.RowSelectionMode;
      this.intMemoType = this.GetMemoType();
      try
      {
        if (Program.iniGSPcLocal.items["SETTINGS", "SELECTION_MODE"] != null)
        {
          if (Program.iniGSPcLocal.items["SETTINGS", "SELECTION_MODE"].ToUpper() == "DIVIDE")
            this.bSelectionMode = true;
          else
            this.bSelectionMode = false;
        }
        else
          this.bSelectionMode = false;
      }
      catch (Exception ex)
      {
        this.bSelectionMode = false;
      }
    }

    private void frmViewerPartslist_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason == CloseReason.FormOwnerClosing)
        return;
      this.frmParent.bPartsListClosed = true;
      this.Hide();
      e.Cancel = true;
    }

    private void frmViewerPartslist_VisibleChanged(object sender, EventArgs e)
    {
      this.frmParent.partslistToolStripMenuItem.Checked = this.Visible;
    }

    public void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.BookPublishingId = this.frmParent.BookPublishingId;
      this.frmParent.bObjFrmPartlistClosed = false;
      object[] objArray = (object[]) e.Argument;
      XmlNode xmlNode1 = (XmlNode) objArray[0];
      XmlNode xmlNode2 = (XmlNode) objArray[1];
      int num = (int) objArray[2];
      int listIndex = (int) objArray[3];
      XmlNodeList listNodes = (XmlNodeList) objArray[4];
      string surl1 = string.Empty;
      string str1 = string.Empty;
      XmlDocument objXmlDoc = new XmlDocument();
      bool flag = false;
      DateTime dtServer = new DateTime();
      try
      {
        this.statusText = this.GetResource("Loading Parts List……", "LOADING_PARTSLIST", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (listIndex >= listNodes.Count)
          listIndex = 0;
        this.curListIndex = listIndex;
        this.SetListIndex(listNodes, listIndex);
        this.partlistInfoMsg = string.Empty;
        string str2 = Program.iniServers[this.frmParent.ServerId].items["PLIST", "INFO"];
        if (str2 != string.Empty)
        {
          string index = string.Empty;
          if (str2 != null && str2 != string.Empty)
          {
            foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode1.Attributes)
            {
              if (attribute.Value != null && attribute.Value.ToUpper() == str2.ToUpper())
              {
                index = attribute.Name;
                break;
              }
            }
          }
          if (listNodes[this.curListIndex].Attributes[index] != null)
            this.partlistInfoMsg = listNodes[this.curListIndex].Attributes[index].Value;
        }
        else
          this.partlistInfoMsg = Program.iniServers[this.frmParent.ServerId].items["PLIST", "INFO_MESSAGE"];
        this.UpdatePListInfoTextBox();
        this.EnableDisablePListInfoBtn();
        if (this.partlistInfoMsg == string.Empty || !Settings.Default.appPartsListInfoVisible)
          this.ShowHidePListInfoPanel(false);
        else
          this.ShowHidePListInfoPanel(true);
        this.CheckPListInfoBtn(Settings.Default.appPartsListInfoVisible);
        if (!listNodes[this.curListIndex].Attributes[this.attListElement].Value.Equals(string.Empty))
        {
          if (!listNodes[this.curListIndex].Attributes[this.attListElement].Value.ToUpper().EndsWith(".XML"))
            listNodes[this.curListIndex].Attributes[this.attListElement].Value += ".XML";
          string str3 = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
          if (!str3.EndsWith("/"))
            str3 += "/";
          try
          {
            this.sPLTitle = listNodes[this.curListIndex].Attributes[this.sPLTitle].Value;
          }
          catch
          {
            this.sPLTitle = this.GetResource("Parts List", "PARTS_LIST", ResourceType.TITLE) + "      ";
          }
          surl1 = str3 + this.BookPublishingId + "/" + listNodes[this.curListIndex].Attributes[this.attListElement].Value;
          string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey + "\\" + this.BookPublishingId;
          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
          str1 = path + "\\" + listNodes[this.curListIndex].Attributes[this.attListElement].Value;
          if (this.p_Compressed)
          {
            surl1 = surl1.ToUpper().Replace(".XML", ".ZIP");
            str1 = str1.ToUpper().Replace(".XML", ".ZIP");
          }
          try
          {
            dtServer = DateTime.Parse(listNodes[this.curListIndex].Attributes[this.attUpdateDateElement].Value, (IFormatProvider) new CultureInfo("fr-FR", false));
          }
          catch
          {
          }
        }
      }
      catch
      {
        this.bgWorker.CancelAsync();
        this.frmParent.HidePartsList();
        MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM003) Failed to create file/folder specified", "(E-VPL-EM003)_FAILED", ResourceType.POPUP_MESSAGE));
        return;
      }
      if (surl1 != string.Empty && str1 != string.Empty)
      {
        int result = 0;
        if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
          result = 0;
        if (File.Exists(str1))
        {
          if (result == 0)
            flag = true;
          else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str1, this.p_Compressed, this.p_Encrypted), dtServer, result))
            flag = true;
        }
        else
          flag = true;
        if (flag && !Program.objAppMode.bWorkingOffline)
          this.objDownloader.DownloadFile(surl1, str1);
        if (File.Exists(str1))
        {
          if (!this.frmParent.IsDisposed)
          {
            this.statusText = this.GetResource("Loading Parts List……", "LOADING_PARTSLIST", ResourceType.STATUS_MESSAGE);
            this.UpdateStatus();
            if (File.Exists(str1))
            {
              if (this.p_Compressed)
              {
                try
                {
                  Global.Unzip(str1);
                  if (File.Exists(str1.ToLower().Replace(".zip", ".xml")))
                    objXmlDoc.Load(str1.ToLower().Replace(".zip", ".xml"));
                }
                catch
                {
                }
              }
              else
              {
                try
                {
                  objXmlDoc.Load(str1);
                }
                catch
                {
                  this.bgWorker.CancelAsync();
                  this.frmParent.HidePartsList();
                  return;
                }
              }
              if (this.p_Encrypted)
              {
                try
                {
                  string str2 = new AES().Decode(objXmlDoc.InnerText, "0123456789ABCDEF");
                  objXmlDoc.DocumentElement.InnerXml = str2;
                }
                catch
                {
                }
              }
              XmlNode schemaNode = objXmlDoc.SelectSingleNode("//Schema");
              if (schemaNode == null)
              {
                this.frmParent.HidePartsList();
              }
              else
              {
                this.curListSchema = schemaNode;
                if (this.curServerId == 9999 || this.curServerId != this.frmParent.ServerId)
                {
                  this.curServerId = this.frmParent.ServerId;
                  this.InitializePartsList(schemaNode);
                  this.InitializeJumpsList();
                }
                this.LoadPartsListInGrid(objXmlDoc);
                this.LoadJumpsInGrid(objXmlDoc);
                this.AdjustGridHeights();
              }
              this.GetUpdateDateElement();
            }
            else
              this.frmParent.HidePartsList();
          }
        }
        else
          this.frmParent.HidePartsList();
      }
      else
        this.frmParent.HidePartsList();
      e.Result = (object) new object[4]
      {
        (object) xmlNode1,
        (object) xmlNode2,
        (object) num,
        (object) listIndex
      };
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (this.dgJumps.Rows.Count == 0)
      {
        this.splitPnlGrids.Panel2Collapsed = true;
        this.splitPnlGrids.Panel2.Hide();
      }
      else
      {
        this.splitPnlGrids.Panel2Collapsed = false;
        this.splitPnlGrids.Panel2.Show();
      }
      bool flag = false;
      for (int index = 1; index < this.dgPartslist.Columns.Count; ++index)
      {
        if (this.dgPartslist.Columns[index].Visible)
        {
          flag = true;
          break;
        }
      }
      if (this.dgJumps.Rows.Count == 0 && this.dgPartslist.Rows.Count == 1 && !flag)
        this.frmParent.HidePartsList();
      this.frmParent.bObjFrmPartlistClosed = true;
      try
      {
        if (this.dgPartslist.SelectedRows.Count > 0)
          this.dgPartslist.SelectedRows[0].Selected = false;
      }
      catch
      {
      }
      this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
      this.HideLoading(this.pnlForm);
      this.Text = this.sPLTitle;
      try
      {
        object[] result = (object[]) e.Result;
        XmlNode xmlNode1 = (XmlNode) result[0];
        XmlNode xmlNode2 = (XmlNode) result[1];
        int num1 = (int) result[2];
        int num2 = (int) result[3];
        if (this.frmParent.IsDisposed)
          return;
        if (this.curPageSchema != xmlNode1 || this.curPageNode != xmlNode2 || (this.curPicIndex != num1 || this.curListIndex != num2))
        {
          this.isWorking = false;
          this.highlightPartNo = string.Empty;
          if (!this.picLoadedSuccessfully)
            return;
          this.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, 0, this.attPicElement, this.attListElement, this.attUpdateDateElement);
        }
        else
        {
          int num3 = 0;
          int num4 = 0;
          string text = this.tsTxtList.Text;
          try
          {
            if (text.Contains("/"))
            {
              num3 = int.Parse(text.Substring(0, text.IndexOf("/")));
              num4 = int.Parse(text.Substring(text.IndexOf("/") + 1, text.Length - (text.IndexOf("/") + 1)));
            }
            if (num3 == 1)
              this.tsBtnPrev.Enabled = false;
            else
              this.tsBtnPrev.Enabled = true;
            if (num3 == num4)
              this.tsBtnNext.Enabled = false;
            else
              this.tsBtnNext.Enabled = true;
          }
          catch
          {
          }
          if (this.dgPartslist.Rows.Count > 0 && this.dgPartslist.SelectedRows.Count == 0)
            this.SelectPart();
          this.frmParent.AddToHistory();
          this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          this.isWorking = false;
          this.dgPartslist.Visible = true;
        }
      }
      catch
      {
      }
      finally
      {
        this.isWorking = false;
      }
    }

    private void chkHeader_OnCheckBoxClicked(bool state)
    {
      this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
      if (this.dgPartslist.Columns.Count > 0)
      {
        foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
        {
          if (row.Cells[0] is DataGridViewCheckBoxCell)
          {
            if (!this.lstDisableRows.Contains(row.Index))
            {
              try
              {
                if (Convert.ToBoolean(row.Cells[0].Value) != state)
                {
                  if (row.Cells["PART_SLIST_KEY"].Value != null)
                  {
                    this.frmParent.CheckUncheckRow(row.Cells["PART_SLIST_KEY"].Value.ToString(), state);
                    if (state)
                    {
                      if (!this.frmParent.PartInSelectionList(row.Cells["PART_SLIST_KEY"].Value.ToString()))
                        this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, row, true);
                    }
                    else
                      this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, row, false);
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
      this.dgPartslist.EndEdit();
      this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
    }

    private void dgPartslist_SelectionChanged(object sender, EventArgs e)
    {
      this.dgPartslist.Focus();
      string listAttributeName = this.GetListAttributeName("Position");
      string empty = string.Empty;
      try
      {
        foreach (DataGridViewRow selectedRow in (BaseCollection) this.dgPartslist.SelectedRows)
        {
          if (selectedRow.ReadOnly)
          {
            selectedRow.Selected = false;
            return;
          }
        }
      }
      catch
      {
      }
      try
      {
        if (this.dgPartslist.Rows.Count > 0)
        {
          this.frmParent.RemoveHighlightOnPicture();
          if (this.dgPartslist.Columns.Contains(listAttributeName))
          {
            for (int index1 = 0; index1 < this.dgPartslist.SelectedRows.Count; ++index1)
            {
              if (this.dgPartslist[listAttributeName, this.dgPartslist.SelectedRows[index1].Index].Value != null)
              {
                string[] strArray1 = this.dgPartslist[listAttributeName, this.dgPartslist.SelectedRows[index1].Index].Value.ToString().Split(new string[1]
                {
                  "**"
                }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                  if (Settings.Default.appFitPageForMultiParts && strArray1.Length > 1)
                  {
                    this.frmParent.ZoomFitPage(true);
                    if (!this.Focused)
                      this.Focus();
                  }
                  else
                  {
                    this.frmParent.ZoomFitPage(false);
                    if (!this.Focused)
                      this.Focus();
                  }
                }
                catch
                {
                }
                for (int index2 = 0; index2 < strArray1.Length; ++index2)
                {
                  string[] strArray2 = strArray1[index2].Split(new string[1]
                  {
                    ","
                  }, StringSplitOptions.RemoveEmptyEntries);
                  if (strArray2.Length == 4)
                  {
                    if (strArray2[0].Contains("^"))
                      strArray2[0] = strArray2[0].Substring(strArray2[0].IndexOf("^") + 1, strArray2[0].Length - (strArray2[0].IndexOf("^") + 1));
                    int result1;
                    int result2;
                    int result3;
                    int result4;
                    if (int.TryParse(strArray2[0], out result1) && int.TryParse(strArray2[1], out result2) && (int.TryParse(strArray2[2], out result3) && int.TryParse(strArray2[3], out result4)))
                    {
                      this.frmParent.HighlightPicture(result1, result2, result3 - result1, result4 - result2);
                      this.frmParent.ScalePicture(result1, result2, result3 - result1, result4 - result2);
                    }
                  }
                }
              }
            }
          }
        }
      }
      catch
      {
      }
      try
      {
        if (this.dgPartslist.SelectedRows.Count == 1)
        {
          this.tsbAddPartMemo.Enabled = true;
          this.tsmAddPartMemo.Enabled = true;
          this.frmParent.EnableAddPartMemoMenu(true);
        }
        else
        {
          this.tsbAddPartMemo.Enabled = false;
          this.tsmAddPartMemo.Enabled = false;
          this.frmParent.EnableAddPartMemoMenu(false);
        }
        if (this.dgPartslist.SelectedRows.Count > 0)
        {
          this.tsbAddToSelectionList.Enabled = true;
          this.tsbClearSelection.Enabled = true;
        }
        else
        {
          this.tsbAddToSelectionList.Enabled = false;
          this.tsbClearSelection.Enabled = false;
        }
      }
      catch
      {
      }
    }

    private void dgPartslist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (this.lstDisableRows != null && this.lstDisableRows.Contains(e.RowIndex))
          return;
        string empty = string.Empty;
        if (this.dgPartslist.Columns.Count <= 0 || this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value == null || (e.RowIndex == -1 || e.ColumnIndex != this.dgPartslist.Columns["CHK"].Index))
          return;
        if (!(bool) this.dgPartslist.Rows[e.RowIndex].Cells[this.dgPartslist.Columns["CHK"].Index].Value)
        {
          this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, this.dgPartslist.Rows[e.RowIndex], false);
          this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
          string sPartNumber = this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value.ToString();
          for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
          {
            if (this.dgPartslist.Rows[index].Cells["PART_SLIST_KEY"].Value != null && this.dgPartslist.Rows[index].Cells["PART_SLIST_KEY"].Value.ToString() == sPartNumber)
              this.frmParent.CheckUncheckRow(sPartNumber, false);
          }
          this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
        }
        else if (this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value != null && !this.frmParent.PartInSelectionList(this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value.ToString()))
        {
          this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, this.dgPartslist.Rows[e.RowIndex], true);
          this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
          string sPartNumber = this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value.ToString();
          if (sPartNumber == string.Empty)
            sPartNumber = this.dgPartslist[this.attPartNameElement, e.RowIndex].Value.ToString();
          for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
          {
            if (this.dgPartslist.Rows[index].Cells["PART_SLIST_KEY"].Value != null && this.dgPartslist.Rows[index].Cells["PART_SLIST_KEY"].Value.ToString() == sPartNumber)
              this.frmParent.CheckUncheckRow(sPartNumber, true);
          }
          this.dgPartslist.EndEdit();
          this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
        }
        int index1 = 0;
        while (index1 < this.dgPartslist.Rows.Count && (this.lstDisableRows.Contains(index1) || !(this.dgPartslist.Rows[index1].Cells[0] is DataGridViewCheckBoxCell) || (this.dgPartslist.Rows[index1].Cells["CHK"].Value == null || (bool) this.dgPartslist.Rows[index1].Cells["CHK"].Value) || this.dgPartslist.Rows[index1].Cells["PART_SLIST_KEY"].Value == null))
          ++index1;
        DatagridViewCheckBoxHeaderCell headerCell = (DatagridViewCheckBoxHeaderCell) this.dgPartslist.Columns[0].HeaderCell;
        if (index1 < this.dgPartslist.Rows.Count)
          headerCell.Checked = false;
        else
          headerCell.Checked = true;
      }
      catch
      {
      }
    }

    private void dgPartslist_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (!(this.dgPartslist.CurrentCell is DataGridViewCheckBoxCell))
          return;
        this.dgPartslist.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
      catch
      {
      }
    }

    private void dgPartslist_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.lstDisableRows != null && this.lstDisableRows.Contains(e.RowIndex))
      {
        this.dgPartslist.Focus();
      }
      else
      {
        if (e.RowIndex != -1 && e.ColumnIndex == this.dgPartslist.Columns["CHK"].Index)
          this.dgPartslist.CurrentCell.Value = (object) !(bool) this.dgPartslist.CurrentCell.Value;
        this.PopUpMemo(e.ColumnIndex, e.RowIndex);
        if (this.dgPartslist.Columns["REF"] != null && this.dgPartslist.Columns[e.ColumnIndex].Name == "REF" && (e.RowIndex >= 0 && this.dgPartslist[e.ColumnIndex, e.RowIndex].Value != null))
        {
          string toolTipText = this.dgPartslist[e.ColumnIndex, e.RowIndex].ToolTipText;
          if (toolTipText.Contains("**"))
          {
            string[] strArray = toolTipText.Split(new string[1]
            {
              "**"
            }, StringSplitOptions.RemoveEmptyEntries);
            this.cmsReference.Items.Clear();
            for (int index = 0; index < strArray.Length; ++index)
              this.cmsReference.Items.Add(strArray[index]);
            this.cmsReference.Show(Control.MousePosition.X, Control.MousePosition.Y);
          }
          else
          {
            int num = (int) new frmOpenBrowser(toolTipText, this.frmParent.ServerId).ShowDialog();
          }
        }
        if (this.dgPartslist.Columns["INF"] == null || e.ColumnIndex != this.dgPartslist.Columns["INF"].Index || (e.RowIndex < 0 || this.dgPartslist[e.ColumnIndex, e.RowIndex].Value == null))
          return;
        int num1 = (int) MessageBox.Show(this.dgPartslist[e.ColumnIndex, e.RowIndex].ToolTipText, "Part Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void tsBtnNext_Click(object sender, EventArgs e)
    {
      if (this.curPageSchema == null || this.curPageNode == null)
        return;
      string text = this.tsTxtList.Text;
      if (!text.Contains("/"))
        return;
      string s = text.Substring(0, text.IndexOf("/"));
      try
      {
        frmViewerPartslist.ChangeBuffer((Control) this.dgPartslist);
        this.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, int.Parse(s), this.attPicElement, this.attListElement, this.attUpdateDateElement);
      }
      catch
      {
      }
    }

    private void tsBtnPrev_Click(object sender, EventArgs e)
    {
      if (this.curPageSchema == null || this.curPageNode == null)
        return;
      string text = this.tsTxtList.Text;
      if (!text.Contains("/"))
        return;
      string s = text.Substring(0, text.IndexOf("/"));
      try
      {
        frmViewerPartslist.ChangeBuffer((Control) this.dgPartslist);
        this.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, int.Parse(s) - 2, this.attPicElement, this.attListElement, this.attUpdateDateElement);
      }
      catch
      {
      }
    }

    private void tsbClearSelection_Click(object sender, EventArgs e)
    {
      if (this.dgPartslist.Rows.Count <= 0)
        return;
      this.dgPartslist.ClearSelection();
      this.tsbAddPartMemo.Enabled = false;
      this.tsbAddToSelectionList.Enabled = false;
      this.frmParent.EnableAddPartMemoMenu(false);
      this.tsbClearSelection.Enabled = false;
    }

    private void cmsPartslist_Opening(object sender, CancelEventArgs e)
    {
      this.nextListToolStripMenuItem.Enabled = this.tsBtnNext.Enabled;
      this.previousListToolStripMenuItem.Enabled = this.tsBtnPrev.Enabled;
      if (!Settings.Default.RowSelectionMode)
      {
        if (this.dgPartslist.SelectedCells.Count > 0)
        {
          this.tsmAddPartMemo.Enabled = false;
          this.tsmAddToSelectionList.Enabled = false;
        }
        this.tsmAddPartMemo.Enabled = false;
        this.tsmAddToSelectionList.Enabled = false;
      }
      else if (this.dgPartslist.Columns.Count > 1 && this.CurrentPartNumber != string.Empty)
      {
        foreach (DataGridViewBand selectedRow in (BaseCollection) this.dgPartslist.SelectedRows)
        {
          if (this.lstDisableRows != null && this.lstDisableRows.Contains(selectedRow.Index))
          {
            this.tsmAddPartMemo.Enabled = false;
            this.tsmAddToSelectionList.Enabled = false;
            this.copyToClipboardToolStripMenuItem.Enabled = true;
            this.exportToFileToolStripMenuItem.Enabled = true;
          }
          else
            this.tsmAddToSelectionList.Enabled = true;
          this.copyToClipboardToolStripMenuItem.Enabled = true;
          this.exportToFileToolStripMenuItem.Enabled = true;
        }
      }
      else
      {
        this.tsmAddToSelectionList.Enabled = false;
        this.copyToClipboardToolStripMenuItem.Enabled = false;
        this.exportToFileToolStripMenuItem.Enabled = false;
      }
    }

    private void tsmAddPartMemo_Click(object sender, EventArgs e)
    {
      this.ShowPartMemos();
    }

    private void tsmClearSelection_Click(object sender, EventArgs e)
    {
      this.tsbClearSelection_Click((object) null, (EventArgs) null);
    }

    private void tsbAddToSelectionList_Click(object sender, EventArgs e)
    {
      this.tsmAddToSelectionList_Click((object) null, (EventArgs) null);
    }

    private void tsbAddPartMemo_Click(object sender, EventArgs e)
    {
      this.tsmAddPartMemo_Click((object) null, (EventArgs) null);
    }

    private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.dgPartslist.Rows.Count <= 0)
        return;
      this.dgPartslist.SelectAll();
      this.tsbAddToSelectionList.Enabled = true;
    }

    private void commaSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        string empty = string.Empty;
        string text = this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, ",") : this.GetDataGridViewText(ref this.dgPartslist, true, true, ",");
        if (!(text != string.Empty))
          return;
        Clipboard.SetText(text, TextDataFormat.UnicodeText);
      }
      catch
      {
      }
    }

    private void tabSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        string empty = string.Empty;
        string text = this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, "\t") : this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t");
        if (!(text != string.Empty))
          return;
        Clipboard.SetText(text, TextDataFormat.UnicodeText);
      }
      catch
      {
      }
    }

    private void tsbSelectAll_Click(object sender, EventArgs e)
    {
      this.selectAllToolStripMenuItem_Click((object) null, (EventArgs) null);
    }

    private void commaSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
      this.dlgSaveFile.RestoreDirectory = true;
      string empty = string.Empty;
      string str = this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, ",") : this.GetDataGridViewText(ref this.dgPartslist, true, true, ",");
      if (!(str != string.Empty))
        return;
      if (this.dlgSaveFile.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        StreamWriter text = File.CreateText(this.dlgSaveFile.FileName);
        text.Write(str);
        text.Close();
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM002) Failed to export specified object", "(E-VPL-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
      }
    }

    private void tabSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
      this.dlgSaveFile.RestoreDirectory = true;
      string empty = string.Empty;
      string str = this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, "\t") : this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t");
      if (!(str != string.Empty))
        return;
      if (this.dlgSaveFile.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        StreamWriter text = File.CreateText(this.dlgSaveFile.FileName);
        text.Write(str);
        text.Close();
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM002) Failed to export specified object", "(E-VPL-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
      }
    }

    private void nextListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsBtnNext_Click((object) null, (EventArgs) null);
    }

    private void previousListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsBtnPrev_Click((object) null, (EventArgs) null);
    }

    private void dgPartslist_MouseMove(object sender, MouseEventArgs e)
    {
      this.dgPartslist.Cursor = Cursors.Arrow;
      try
      {
        int rowIndex = this.dgPartslist.HitTest(e.X, e.Y).RowIndex;
        int columnIndex = this.dgPartslist.HitTest(e.X, e.Y).ColumnIndex;
        if (this.dgPartslist[columnIndex, rowIndex].Value == null || !(this.dgPartslist[columnIndex, rowIndex].Value.ToString() == "System.Drawing.Bitmap"))
          return;
        this.dgPartslist.Cursor = Cursors.Hand;
      }
      catch
      {
        this.dgPartslist.Cursor = Cursors.Arrow;
      }
    }

    private void dgPartslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
        return;
      try
      {
        if (this.lstDisableRows != null)
        {
          if (this.lstDisableRows.Contains(e.RowIndex))
            return;
        }
      }
      catch
      {
      }
      try
      {
        if (e.RowIndex == -1)
          return;
        if (this.dgPartslist.CurrentRow.Cells["CHK"].Value.ToString().ToUpper() == "FALSE")
        {
          this.dgPartslist.CurrentRow.Cells["CHK"].Value = (object) true;
        }
        else
        {
          if (this.dgPartslist.CurrentCell.ColumnIndex != this.dgPartslist.Columns["QTY"].Index)
            return;
          this.frmParent.ShowQuantityScreen(this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.CurrentRow.Index].Value.ToString());
        }
      }
      catch
      {
      }
    }

    private void tsbClearSelection_EnabledChanged(object sender, EventArgs e)
    {
      this.tsmClearSelection.Enabled = this.tsbClearSelection.Enabled;
    }

    private void tsbPartistInfo_Click(object sender, EventArgs e)
    {
      Settings.Default.appPartsListInfoVisible = !Settings.Default.appPartsListInfoVisible;
      this.ShowHidePListInfoPanel(Settings.Default.appPartsListInfoVisible);
    }

    private void cmsReference_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      this.OpenURLInBrowser(e.ClickedItem.Text);
    }

    private void dgPartslist_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
    {
      try
      {
        if (e.Column.Tag.ToString().ToUpper() == "LINKNUMBER")
        {
          int num1 = int.Parse(this.dgPartslist.Rows[e.RowIndex1].Cells["AutoIndexColumn"].Value.ToString());
          int num2 = int.Parse(this.dgPartslist.Rows[e.RowIndex2].Cells["AutoIndexColumn"].Value.ToString());
          e.SortResult = num1 <= num2 ? (num1 >= num2 ? 0 : -1) : 1;
        }
        else
          e.SortResult = string.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
        e.Handled = true;
      }
      catch
      {
      }
    }

    private void dgPartslist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      try
      {
        this.dgPartslist["AutoIndexColumn", e.RowIndex].Value = (object) e.RowIndex;
      }
      catch
      {
      }
    }

    private void dgPartslist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
      try
      {
        if (this.lstDisableRows == null || !this.lstDisableRows.Contains(e.RowIndex) || (this.dgPartslist[this.attPartStatusElement, e.RowIndex].Value == null || !(this.dgPartslist[this.attPartStatusElement, e.RowIndex].Value.ToString() == "0")) || e.ColumnIndex != 0)
          return;
        e.PaintBackground(e.ClipBounds, true);
        e.Handled = true;
      }
      catch
      {
      }
    }

    private void dgPartslist_Sorted(object sender, EventArgs e)
    {
      try
      {
        if (this.lstDisableRows == null || this.lstDisableRows.Count <= 0)
          return;
        this.lstDisableRows.Clear();
        if (string.IsNullOrEmpty(this.attPartStatusElement))
          return;
        for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
        {
          try
          {
            if (this.dgPartslist[this.attPartStatusElement, index].Value != null)
            {
              if (this.dgPartslist[this.attPartStatusElement, index].Value.ToString() == "0")
                this.lstDisableRows.Add(index);
            }
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

    private void rowSelectionModeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        Settings.Default.RowSelectionMode = !Settings.Default.RowSelectionMode;
        this.rowSelectionModeToolStripMenuItem.Checked = Settings.Default.RowSelectionMode;
        if (Settings.Default.RowSelectionMode)
        {
          for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
            this.dgPartslist.Columns[index].SortMode = DataGridViewColumnSortMode.Programmatic;
          this.dgPartslist.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }
        else
        {
          this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
          for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
            this.dgPartslist.Columns[index].SortMode = DataGridViewColumnSortMode.Automatic;
        }
        this.dgPartslist.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
      }
      catch
      {
      }
    }

    private void dgPartslist_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      try
      {
        if (!Settings.Default.RowSelectionMode)
        {
          for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
            this.dgPartslist.Columns[index].SortMode = DataGridViewColumnSortMode.Programmatic;
          if (e.RowIndex != -1)
          {
            this.dgPartslist.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgPartslist[e.ColumnIndex, e.RowIndex].Selected = true;
          }
          else
          {
            this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            this.dgPartslist.Columns[e.ColumnIndex].Selected = true;
          }
        }
        else if (Settings.Default.RowSelectionMode && e.RowIndex != -1)
        {
          this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
          for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
            this.dgPartslist.Columns[index].SortMode = DataGridViewColumnSortMode.Automatic;
          if (this.dgPartslist.Columns[e.ColumnIndex].CellType.Name.ToString() == "DataGridViewImageColumn" || this.dgPartslist.Columns[e.ColumnIndex].CellType.Name.ToString() == "DataGridViewImageCell")
          {
            for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
              this.dgPartslist.Rows[index].Selected = false;
          }
          this.dgPartslist.Rows[e.RowIndex].Selected = true;
        }
        this.dgPartslist.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
      }
      catch
      {
      }
    }

    public void UpdateCurrentPageForPartslist(bool picLoaded, XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
    {
      this.picLoadedSuccessfully = picLoaded;
      this.curPageSchema = schemaNode;
      this.curPageNode = pageNode;
      this.curPicIndex = picIndex;
      this.curListIndex = listIndex;
      this.attPicElement = attPic;
      this.attListElement = attList;
      this.attUpdateDateElement = attUpdateDate;
    }

    public void LoadPartsList(XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
    {
      this.dgPartslist.Visible = true;
      this.picLoading.Visible = true;
      this.ShowLoading(this.pnlForm);
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) schemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("PARTSLISTTITLE"))
        {
          this.sPLTitle = attribute.Name;
          break;
        }
      }
      this.dgPartslist.SelectionChanged -= new EventHandler(this.dgPartslist_SelectionChanged);
      this.UpdateCurrentPageForPartslist(true, schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
      if (this.isWorking)
        return;
      this.isWorking = true;
      this.objXmlNodeList = (XmlNodeList) null;
      try
      {
        this.p_Encrypted = Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON";
        this.p_Compressed = Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON";
        XmlDocument xmlDocument1 = new XmlDocument();
        xmlDocument1.LoadXml(this.curPageNode.OuterXml);
        try
        {
          string str = xmlDocument1.SelectNodes("//Pic[not(@" + this.attPicElement + " = preceding-sibling::Pic/@" + this.attPicElement + ")]")[this.curPicIndex].Attributes[this.attPicElement].Value;
          this.curPictureFileName = str;
          if (str.Trim() == string.Empty)
            throw new Exception();
          this.objXmlNodeList = xmlDocument1.SelectNodes("//Pic[@" + this.attPicElement + "='" + str + "' and @" + this.attListElement + "]");
        }
        catch
        {
          XmlDocument xmlDocument2 = new XmlDocument();
          xmlDocument2.LoadXml(this.curPageNode.OuterXml);
          this.objXmlNodeList = xmlDocument2.SelectNodes("//Pic[(not(@" + this.attPicElement + ") or @" + this.attPicElement + "='') and @" + this.attListElement + "]");
        }
      }
      catch
      {
        this.frmParent.HidePartsList();
        MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM001) Failed to load specified object", "(E-VPL-EM001)_FAILED", ResourceType.POPUP_MESSAGE));
      }
      if (this.objXmlNodeList != null && this.objXmlNodeList.Count > 0)
      {
        this.frmParent.EnablePartslistShowHideButton(true);
        if (!this.frmParent.bPartsListClosed)
          this.frmParent.ShowPartsList();
        this.bgWorker.RunWorkerAsync((object) new object[5]
        {
          (object) this.curPageSchema,
          (object) this.curPageNode,
          (object) this.curPicIndex,
          (object) this.curListIndex,
          (object) this.objXmlNodeList
        });
      }
      else
      {
        this.curListSchema = (XmlNode) null;
        this.frmParent.AddToHistory();
        this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        this.frmParent.HidePartsList();
        this.isWorking = false;
      }
    }

    private void SelectPart()
    {
      if (!(this.highlightPartNo != string.Empty) || !(this.attPartNoElement != string.Empty))
        return;
      if (this.bSelectionMode && this.highlightPartNo.Contains("^"))
      {
        string[] strArray = this.highlightPartNo.Split('^');
        string s = strArray[strArray.Length - 2].ToString();
        string str = strArray[strArray.Length - 1].ToString();
        for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
        {
          if (index == int.Parse(s) && this.dgPartslist[this.attPartNoElement, index].Value.ToString().ToUpper() == str.ToUpper())
          {
            this.dgPartslist.Rows[index].Selected = true;
            this.dgPartslist.FirstDisplayedScrollingRowIndex = index;
            this.highlightPartNo = string.Empty;
            break;
          }
        }
      }
      else
      {
        for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
        {
          if (this.dgPartslist[this.attPartNoElement, index].Value.ToString().ToUpper() == this.highlightPartNo.ToUpper())
          {
            this.dgPartslist.Rows[index].Selected = true;
            this.dgPartslist.FirstDisplayedScrollingRowIndex = index;
            this.highlightPartNo = string.Empty;
            break;
          }
        }
      }
    }

    private string GetDGHeaderCellValue(string sKey, string sDefaultHeaderValue)
    {
      string str1 = string.Empty;
      bool flag = false;
      if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
      {
        string str2 = Settings.Default.appLanguage + "_GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini";
        if (File.Exists(Application.StartupPath + "\\Language XMLs\\" + str2))
        {
          TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str2);
          string str3;
          while ((str3 = textReader.ReadLine()) != null)
          {
            if (str3.ToUpper() == "[PLIST_SETTINGS]")
              flag = true;
            else if (str3.Contains("=") && flag)
            {
              string[] strArray = str3.Split(new string[1]
              {
                "="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray[0].ToString().ToUpper() == sKey.ToUpper())
              {
                str1 = strArray[1];
                break;
              }
            }
            else if (str3.Contains("["))
              flag = false;
          }
          if (str1 == string.Empty)
            str1 = sDefaultHeaderValue;
          textReader.Close();
        }
        else
          str1 = sDefaultHeaderValue;
      }
      else
        str1 = sDefaultHeaderValue;
      return str1;
    }

    private string FindAttributeKey(string attVal)
    {
      try
      {
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.curListSchema.Attributes)
        {
          if (attribute.Value == attVal)
            return attribute.Name;
        }
        return string.Empty;
      }
      catch
      {
        return string.Empty;
      }
    }

    private void LoadPartsListInGrid(XmlDocument objXmlDoc)
    {
      if (this.dgPartslist.InvokeRequired)
      {
        this.dgPartslist.Invoke((Delegate) new frmViewerPartslist.LoadPartsListInGridDelegate(this.LoadPartsListInGrid), (object) objXmlDoc);
      }
      else
      {
        this.lstDisableRows = new List<int>();
        this.tsbAddToSelectionList.Enabled = false;
        this.dgPartslist.AllowUserToAddRows = true;
        this.dgPartslist.CurrentCell = this.dgPartslist[0, 0];
        this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
        this.dgPartslist.BeginEdit(true);
        ((DatagridViewCheckBoxHeaderCell) this.dgPartslist.Columns[0].HeaderCell).Checked = true;
        this.dgPartslist.Rows.Clear();
        this.dgPartslist.AllowUserToAddRows = false;
        XmlNodeList xNodeList;
        if (this.attPictureFileElement.Equals(string.Empty))
        {
          xNodeList = objXmlDoc.SelectNodes("//Parts/Part");
        }
        else
        {
          xNodeList = objXmlDoc.SelectNodes("//Parts/Part[@" + this.attPictureFileElement + " = '" + this.curPictureFileName + "']");
          if (xNodeList.Count == 0)
            xNodeList = objXmlDoc.SelectNodes("//Parts/Part");
        }
        XmlNodeList xmlNodeList = this.frmParent.FilterPartsList(this.curListSchema, xNodeList);
        try
        {
          foreach (XmlNode xmlNode in xmlNodeList)
          {
            bool flag1 = false;
            bool flag2 = false;
            try
            {
              if (xmlNode.Attributes[this.attPartNoElement] != null)
              {
                if (xmlNode.Attributes[this.attPartNoElement].Value != string.Empty)
                  flag1 = true;
              }
            }
            catch
            {
            }
            try
            {
              if (xmlNode.Attributes[this.attPartNameElement] != null)
                flag2 = true;
            }
            catch
            {
            }
            if (xmlNode.Attributes.Count > 1 && (flag2 || flag1))
            {
              this.dgPartslist.Rows.Add();
              int num = 0;
              string sAdminMemoValues = string.Empty;
              for (int index = 0; index < this.attAdminMemList.Count; ++index)
              {
                if (xmlNode.Attributes[this.attAdminMemList[index].ToString()] != null)
                  sAdminMemoValues = sAdminMemoValues + xmlNode.Attributes[this.attAdminMemList[index].ToString()].Value + "**";
              }
              try
              {
                foreach (DataGridViewColumn column in (BaseCollection) this.dgPartslist.Columns)
                {
                  if (column.Name == "REF")
                  {
                    string attributeKey = this.FindAttributeKey(column.Tag.ToString());
                    if (attributeKey != string.Empty && xmlNode.Attributes[attributeKey] != null)
                    {
                      string str = xmlNode.Attributes[attributeKey].Value;
                      if (str != null)
                      {
                        this.dgPartslist[column.Index, this.dgPartslist.Rows.Count - 1].Value = (object) GSPcLocalViewer.Properties.Resources.Reference;
                        this.dgPartslist[column.Index, this.dgPartslist.Rows.Count - 1].ToolTipText = str;
                      }
                    }
                  }
                }
              }
              catch
              {
              }
              try
              {
                if (this.dgPartslist.Columns.Contains("INF"))
                {
                  string attributeKey = this.FindAttributeKey(this.dgPartslist.Columns["REF"].Tag.ToString());
                  if (attributeKey != string.Empty)
                  {
                    if (xmlNode.Attributes[attributeKey] != null)
                    {
                      string str = xmlNode.Attributes[attributeKey].Value;
                      if (str != null)
                      {
                        this.dgPartslist["INF", this.dgPartslist.Rows.Count - 1].Value = (object) GSPcLocalViewer.Properties.Resources.info;
                        this.dgPartslist["INF", this.dgPartslist.Rows.Count - 1].ToolTipText = str;
                      }
                    }
                  }
                }
              }
              catch
              {
              }
              try
              {
                if (this.dgPartslist.Columns.Contains("QTY"))
                {
                  string attributeKey = this.FindAttributeKey(this.dgPartslist.Columns["QTY"].Tag.ToString());
                  if (xmlNode.Attributes[attributeKey] != null)
                    this.dgPartslist["QTY", this.dgPartslist.Rows.Count - 1].Value = (object) xmlNode.Attributes[attributeKey].Value;
                }
              }
              catch
              {
              }
              for (int index = 0; index < xmlNode.Attributes.Count; ++index)
              {
                if (!this.frmParent.IsDisposed)
                {
                  try
                  {
                    if (this.dgPartslist.Columns.Contains(xmlNode.Attributes[index].Name))
                    {
                      this.dgPartslist[xmlNode.Attributes[index].Name, this.dgPartslist.Rows.Count - 1].Value = (object) xmlNode.Attributes[index].Value;
                      if (this.dgPartslist.Columns[xmlNode.Attributes[index].Name].Visible)
                        ++num;
                    }
                  }
                  catch
                  {
                  }
                  Application.DoEvents();
                }
              }
              if (xmlNode.Attributes[this.attPartNoElement] != null)
                this.frmParent.PartMemoExists(xmlNode.Attributes[this.attPartNoElement].Value, sAdminMemoValues, this.dgPartslist.Rows.Count - 1);
              if (this.dgPartslist.Columns.Count > 0)
              {
                try
                {
                  bool flag3 = false;
                  if (!string.IsNullOrEmpty(this.attPartStatusElement))
                    flag3 = this.dgPartslist[this.attPartStatusElement, this.dgPartslist.Rows.Count - 1].Value != null && this.dgPartslist[this.attPartStatusElement, this.dgPartslist.Rows.Count - 1].Value.ToString() == "0";
                  object obj1 = this.dgPartslist[this.attPartNoElement, this.dgPartslist.Rows.Count - 1].Value;
                  object obj2 = this.dgPartslist[this.attPartNameElement, this.dgPartslist.Rows.Count - 1].Value;
                  if (this.bSelectionMode)
                  {
                    StringBuilder stringBuilder = new StringBuilder(this.BookPublishingId);
                    stringBuilder.Append("^");
                    stringBuilder.Append(this.frmParent.objFrmPicture.CurrentPageId.ToString());
                    stringBuilder.Append("^");
                    stringBuilder.Append(this.curListIndex.ToString());
                    stringBuilder.Append("^");
                    stringBuilder.Append(this.dgPartslist.Rows.Count - 1);
                    stringBuilder.Append("^");
                    stringBuilder.Append(obj1);
                    obj1 = (object) stringBuilder;
                  }
                  if (obj1 != null && obj1.ToString() != string.Empty)
                    this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.Rows.Count - 1].Value = obj1;
                  else if (obj2 != null)
                    this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.Rows.Count - 1].Value = obj2;
                  if (!flag3)
                  {
                    if (this.frmParent.PartInSelectionList(this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.Rows.Count - 1].Value.ToString()))
                    {
                      this.dgPartslist[0, this.dgPartslist.Rows.Count - 1].Value = (object) true;
                    }
                    else
                    {
                      this.dgPartslist[0, this.dgPartslist.Rows.Count - 1].Value = (object) false;
                      ((DatagridViewCheckBoxHeaderCell) this.dgPartslist.Columns[0].HeaderCell).Checked = false;
                    }
                  }
                }
                catch
                {
                  this.dgPartslist[0, this.dgPartslist.Rows.Count - 1].Value = (object) false;
                  ((DatagridViewCheckBoxHeaderCell) this.dgPartslist.Columns[0].HeaderCell).Checked = true;
                }
              }
              if (num == 0)
              {
                this.HidePartsList();
                return;
              }
            }
          }
          if (this.dgPartslist.Rows.Count == 0)
            this.HidePartsList();
          else
            this.ShowPartsList();
        }
        catch
        {
          this.HidePartsList();
        }
        this.dgPartslist.Dock = DockStyle.None;
        this.dgPartslist.Dock = DockStyle.Fill;
        this.dgPartslist.AllowUserToResizeColumns = true;
        try
        {
          for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
          {
            this.dgPartslist[0, index].Value = !this.frmParent.PartInSelectionListA(this.dgPartslist["PART_SLIST_KEY", index].Value.ToString()) ? (object) false : (object) true;
            try
            {
              if (this.dgPartslist[this.attPartStatusElement, index].Value != null)
              {
                if (this.dgPartslist[this.attPartStatusElement, index].Value.ToString() == "0")
                  this.lstDisableRows.Add(index);
              }
            }
            catch
            {
            }
          }
        }
        catch
        {
        }
        this.dgPartslist.EndEdit();
        this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
        frmViewerPartslist.ChangeBuffer((Control) this.dgPartslist);
      }
    }

    private void SetListIndex(XmlNodeList listNodes, int listIndex)
    {
      if (this.toolStrip1.InvokeRequired)
        this.toolStrip1.Invoke((Delegate) new frmViewerPartslist.SetListIndexDelegate(this.SetListIndex), (object) listNodes, (object) listIndex);
      else if (listNodes.Count > 0)
        this.tsTxtList.Text = (listIndex + 1).ToString() + "/" + listNodes.Count.ToString();
      else
        this.tsTxtList.Text = "1/1";
    }

    public void HighlightPartslist(string argName, string argValue)
    {
      this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
        this.dgPartslist.Columns[index].SortMode = DataGridViewColumnSortMode.Automatic;
      this.dgPartslist.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
      string listAttributeName = this.GetListAttributeName(argName);
      this.dgPartslist.SelectionChanged -= new EventHandler(this.dgPartslist_SelectionChanged);
      this.dgPartslist.BeginEdit(true);
      try
      {
        for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
        {
          bool flag = true;
          try
          {
            flag = this.dgPartslist.Rows[index].ReadOnly;
          }
          catch
          {
          }
          if (this.dgPartslist[listAttributeName, index].Value != null && this.dgPartslist[listAttributeName, index].Value.ToString() == argValue && !flag)
            this.dgPartslist.Rows[index].Selected = true;
          else
            this.dgPartslist.Rows[index].Selected = false;
        }
        if (this.dgPartslist.SelectedRows.Count > 0)
          this.dgPartslist.FirstDisplayedScrollingRowIndex = this.dgPartslist.SelectedRows[this.dgPartslist.SelectedRows.Count - 1].Index;
      }
      catch
      {
      }
      this.dgPartslist.EndEdit();
      this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
    }

    private string GetListAttributeName(string attValue)
    {
      if (this.curListSchema == null)
        return string.Empty;
      for (int index = 0; index < this.curListSchema.Attributes.Count; ++index)
      {
        if (this.curListSchema.Attributes[index].Value.ToUpper().Equals(attValue.ToUpper()))
          return this.curListSchema.Attributes[index].Name;
      }
      return string.Empty;
    }

    private void OpenURLInBrowser(string sSeed)
    {
      try
      {
        if (!(sSeed != string.Empty) || sSeed == null)
          return;
        string str1 = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "REF_URL"];
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
                  frmViewerPartslist.SetForegroundWindow(process.Handle);
                  frmViewerPartslist.SetActiveWindow(process.Handle);
                }
                process.WaitForExit();
              }
            }
            else
            {
              int num = (int) MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
          }
          else if (registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty) == null)
          {
            using (Process process = Process.Start(fileName, arguments))
            {
              if (process != null)
              {
                IntPtr handle = process.Handle;
                frmViewerPartslist.SetForegroundWindow(process.Handle);
                frmViewerPartslist.SetActiveWindow(process.Handle);
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
                frmViewerPartslist.SetForegroundWindow(process.Handle);
                frmViewerPartslist.SetActiveWindow(process.Handle);
              }
              process.WaitForExit();
            }
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      catch
      {
      }
    }

    public void UpdateFont()
    {
      this.dgPartslist.Font = Settings.Default.appFont;
      this.dgPartslist.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgPartslist.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
      this.dgJumps.Font = Settings.Default.appFont;
      this.dgJumps.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgJumps.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
      this.pnlInfo.Font = Settings.Default.appFont;
      this.pnlInfo.BackColor = Settings.Default.PartsListInfoBackColor;
      this.pnlInfo.ForeColor = Settings.Default.PartsListInfoForeColor;
      try
      {
        this.dgPartslist_SelectionChanged((object) null, (EventArgs) null);
      }
      catch
      {
      }
    }

    private void UpdateStatus()
    {
      if (this.frmParent.InvokeRequired)
        this.frmParent.Invoke((Delegate) new frmViewerPartslist.StatusDelegate(this.frmParent.UpdateStatus), (object) this.statusText);
      else
        this.frmParent.UpdateStatus(this.statusText);
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerPartslist.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = false;
          }
          this.picLoading.Parent = (Control) parentPanel;
          this.picLoading.BringToFront();
          this.picLoading.Show();
        }
      }
      catch
      {
      }
    }

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerPartslist.HideLoadingDelegate(this.HideLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = true;
          }
          this.picLoading.Hide();
          this.picLoading.Size = new Size(32, 32);
          this.picLoading.Parent = (Control) this.pnlForm;
        }
      }
      catch
      {
      }
    }

    public string CurrentPicIndex
    {
      get
      {
        return (this.curPicIndex + 1).ToString();
      }
    }

    public string CurrentListIndex
    {
      get
      {
        return (this.curListIndex + 1).ToString();
      }
    }

    public string CurrentPartNumber
    {
      get
      {
        if (this.attPartNoElement.Equals(string.Empty) || this.curListSchema == null || this.dgPartslist.SelectedRows.Count <= 0)
          return string.Empty;
        if (this.dgPartslist.SelectedRows.Count > 1 && this.intMemoType == 2)
          return this.dgPartslist[this.attPartNoElement, this.dgPartslist.SelectedRows[this.dgPartslist.SelectedRows.Count - 1].Index].Value.ToString();
        return this.dgPartslist[this.attPartNoElement, this.dgPartslist.SelectedRows[0].Index].Value.ToString();
      }
    }

    public DataGridView Partlist
    {
      get
      {
        return this.dgPartslist;
      }
    }

    public void ShowPartMemos()
    {
      try
      {
        if (!Program.objAppFeatures.bMemo || this.dgPartslist.Columns.Count <= 1 || (!(this.CurrentPartNumber != string.Empty) || this.dgPartslist.SelectedRows.Count <= 0))
          return;
        int index1 = this.dgPartslist.SelectedRows[0].Index;
        if (this.lstDisableRows != null && this.lstDisableRows.Contains(index1))
          return;
        string sAdminMemoValues = string.Empty;
        for (int index2 = 0; index2 < this.attAdminMemList.Count; ++index2)
        {
          if (this.dgPartslist.Columns.Contains(this.attAdminMemList[index2].ToString()) && this.dgPartslist[this.attAdminMemList[index2].ToString(), index1] != null)
            sAdminMemoValues = sAdminMemoValues + this.dgPartslist[this.attAdminMemList[index2].ToString(), index1].Value + "**";
        }
        string empty = string.Empty;
        string str;
        try
        {
          if (this.dgPartslist[this.attListUpdateDateElement, index1] != null)
            str = this.dgPartslist[this.attListUpdateDateElement, index1].Value.ToString();
        }
        catch
        {
          if (empty != null && !(empty == ""))
          {
            if (empty.Length != 0)
              goto label_15;
          }
          str = DateTime.Now.ToString("M/d/yyyy").Replace('-', '/');
        }
label_15:
        this.frmParent.ShowPartMemos(this.CurrentPartNumber, sAdminMemoValues, string.Empty, string.Empty);
      }
      catch
      {
      }
    }

    public void UpdateMemoIconOnSelectedRow()
    {
      try
      {
        string sAdminMemoValues = string.Empty;
        if (this.dgPartslist.SelectedRows.Count > 0)
        {
          for (int index = 0; index < this.attAdminMemList.Count; ++index)
          {
            if (this.dgPartslist.Columns.Contains(this.attAdminMemList[index].ToString()) && this.dgPartslist[this.attAdminMemList[index].ToString(), this.dgPartslist.SelectedRows[0].Index] != null)
              sAdminMemoValues = sAdminMemoValues + this.dgPartslist[this.attAdminMemList[index].ToString(), this.dgPartslist.SelectedRows[0].Index].Value + "**";
          }
        }
        this.frmParent.PartMemoExists(this.CurrentPartNumber, sAdminMemoValues, this.dgPartslist.SelectedRows[0].Index);
      }
      catch
      {
      }
    }

    private string GetDataGridViewText(ref DataGridView GridView, bool IncludeHeader, bool SelectedRows, string Delimiter)
    {
      if (GridView.Rows.Count == 0 || SelectedRows && GridView.SelectedRows.Count == 0)
        return string.Empty;
      List<string> stringList = new List<string>();
      SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>();
      for (int index = 0; index < GridView.Columns.Count; ++index)
      {
        if (GridView.Columns[index].Visible && GridView.Columns[index].GetType().ToString() == typeof (DataGridViewTextBoxColumn).ToString())
        {
          int displayIndex = GridView.Columns[index].DisplayIndex;
          sortedDictionary.Add(displayIndex, GridView.Columns[index].Name);
        }
      }
      string str = "";
      if (IncludeHeader)
      {
        foreach (KeyValuePair<int, string> keyValuePair in sortedDictionary)
          str = str + this.GetWriteableValue((object) GridView.Columns[keyValuePair.Value].HeaderText) + Delimiter;
        str = str.Remove(str.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
      }
      for (int index = 0; index < GridView.Rows.Count; ++index)
      {
        if (SelectedRows)
        {
          if (GridView.SelectedRows.Contains(GridView.Rows[index]))
          {
            foreach (KeyValuePair<int, string> keyValuePair in sortedDictionary)
              str = str + this.GetWriteableValue(GridView.Rows[index].Cells[keyValuePair.Value].Value) + Delimiter;
            str = str.Remove(str.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
          }
        }
        else if (GridView.SelectedRows.Contains(GridView.Rows[index]))
        {
          foreach (KeyValuePair<int, string> keyValuePair in sortedDictionary)
            str = str + this.GetWriteableValue(GridView.Rows[index].Cells[keyValuePair.Value].Value) + Delimiter;
          str = str.Remove(str.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
        }
      }
      return str;
    }

    private string GetDataGridViewCellsText(ref DataGridView GridView, bool IncludeHeader, string Delimiter)
    {
      string str1 = string.Empty;
      try
      {
        if (GridView.Rows.Count == 0)
          return string.Empty;
        List<string> stringList = new List<string>();
        SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>();
        int num1 = 0;
        int num2 = GridView.Rows.Count;
        for (int index1 = 0; index1 < GridView.Columns.Count; ++index1)
        {
          for (int index2 = 0; index2 < GridView.Rows.Count; ++index2)
          {
            if (GridView[index1, index2].Selected && index2 > num1)
              num1 = index2;
            if (GridView[index1, index2].Selected && index2 < num2)
              num2 = index2;
            if (!sortedDictionary.ContainsValue(GridView.Columns[index1].Name) && GridView[index1, index2].Selected && (GridView.Columns[index1].Visible && GridView.Columns[index1].GetType().ToString() == typeof (DataGridViewTextBoxColumn).ToString()))
            {
              int displayIndex = GridView.Columns[index1].DisplayIndex;
              sortedDictionary.Add(displayIndex, GridView.Columns[index1].Name);
            }
          }
        }
        if (IncludeHeader)
        {
          foreach (KeyValuePair<int, string> keyValuePair in sortedDictionary)
            str1 = str1 + this.GetWriteableValue((object) GridView.Columns[keyValuePair.Value].HeaderText) + Delimiter;
          str1 = str1.Remove(str1.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
        }
        for (int index = num2; index <= num1; ++index)
        {
          string str2 = string.Empty;
          bool flag = false;
          foreach (KeyValuePair<int, string> keyValuePair in sortedDictionary)
          {
            if (GridView.Rows[index].Cells[keyValuePair.Value].Selected)
            {
              flag = true;
              str2 = str2 + this.GetWriteableValue(GridView.Rows[index].Cells[keyValuePair.Value].Value) + Delimiter;
            }
            else
              str2 += Delimiter;
          }
          if (flag)
          {
            str1 += str2;
            str1 = str1.Remove(str1.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
          }
        }
      }
      catch
      {
      }
      return str1;
    }

    private string GetWriteableValue(object o)
    {
      if (o == null || o == Convert.DBNull)
        return string.Empty;
      if (o.ToString().IndexOf(",") == -1)
        return o.ToString();
      return "\"" + o.ToString() + "\"";
    }

    public void ShowHidePartslistToolbar()
    {
      this.toolStrip1.Visible = Settings.Default.ShowListToolbar;
    }

    private string GetJumpsHeaderCellValue(string sKey, string sDefaultHeaderValue)
    {
      string str1 = string.Empty;
      bool flag = false;
      if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
      {
        string str2 = Settings.Default.appLanguage + "_GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini";
        if (File.Exists(Application.StartupPath + "\\Language XMLs\\" + str2))
        {
          TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str2);
          string str3;
          while ((str3 = textReader.ReadLine()) != null)
          {
            if (str3.ToUpper() == "[PLIST_JUMP_SETTINGS]")
              flag = true;
            else if (str3.Contains("=") && flag)
            {
              string[] strArray = str3.Split(new string[1]
              {
                "="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray[0].ToString().ToUpper() == sKey.ToUpper())
              {
                str1 = strArray[1];
                break;
              }
            }
            else if (str3.Contains("["))
              flag = false;
          }
          if (str1 == string.Empty)
            str1 = sDefaultHeaderValue;
          textReader.Close();
        }
        else
          str1 = sDefaultHeaderValue;
      }
      else
        str1 = sDefaultHeaderValue;
      return str1;
    }

    private void InitializePartsList(XmlNode schemaNode)
    {
      if (this.dgPartslist.InvokeRequired)
      {
        this.dgPartslist.Invoke((Delegate) new frmViewerPartslist.InitializePartsListDelegate(this.InitializePartsList), (object) schemaNode);
      }
      else
      {
        try
        {
          this.dgPartslist.AllowUserToAddRows = true;
          this.dgPartslist.CurrentCell = this.dgPartslist[0, 0];
          this.dgPartslist.BeginEdit(true);
          this.dgPartslist.Columns.Clear();
          this.dgPartslist.DefaultCellStyle.NullValue = (object) null;
          this.AddCheckBoxColumn();
          this.AddSchemaColumns(schemaNode);
          this.ReadMandatoryAttribKeysFromSchema(schemaNode);
          this.AddMemoColumns();
          this.Set_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
          this.MapSpecialColumns(schemaNode);
          this.MapDefaultMemoColumn(schemaNode);
          this.Set_MemoCols_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
          this.SetGridHeaderText();
          this.addSortingColumn();
          this.addSelectionListKeyCol();
        }
        catch
        {
        }
      }
    }

    public void Set_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni()
    {
      try
      {
        this.frmParent.dicPLSettings.Clear();
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PLIST_SETTINGS");
        for (int index1 = 0; index1 < keys.Count; ++index1)
        {
          for (int index2 = 1; index2 < this.dgPartslist.Columns.Count; ++index2)
          {
            try
            {
              if (this.dgPartslist.Columns[index2].Tag != null)
              {
                if (keys[index1].ToString().ToUpper() == this.dgPartslist.Columns[index2].Tag.ToString().ToUpper())
                {
                  this.dgPartslist.Columns[index2].DisplayIndex = index1 + 1;
                  string empty1 = string.Empty;
                  string empty2 = string.Empty;
                  string keyValue = iniFileIo.GetKeyValue("PLIST_SETTINGS", keys[index1].ToString().ToUpper(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
                  string[] strArray1 = keyValue.Split(new string[1]
                  {
                    "|"
                  }, StringSplitOptions.RemoveEmptyEntries);
                  string str1 = "|True|True|" + strArray1[1] + "|" + strArray1[2];
                  if (strArray1.Length == 3)
                  {
                    keyValue += str1;
                    this.frmParent.dicPLSettings.Add(keys[index1].ToString(), str1);
                  }
                  else if (strArray1.Length == 4)
                  {
                    keyValue += str1;
                    this.frmParent.dicPLSettings.Add(keys[index1].ToString(), str1);
                  }
                  string[] strArray2 = keyValue.Split(new string[1]
                  {
                    "|"
                  }, StringSplitOptions.RemoveEmptyEntries);
                  if (strArray2 != null)
                  {
                    if (strArray2.Length != 0)
                    {
                      if (strArray2.Length > 7)
                      {
                        if (strArray2[4] == "True")
                        {
                          string str2 = strArray2[1];
                          if (strArray2.Length >= 5)
                          {
                            if (str2.Equals("L"))
                            {
                              this.dgPartslist.Columns[index2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                              this.dgPartslist.Columns[index2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            }
                            else if (str2.Equals("R"))
                            {
                              this.dgPartslist.Columns[index2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                              this.dgPartslist.Columns[index2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                            else if (str2.Equals("C"))
                            {
                              this.dgPartslist.Columns[index2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                              this.dgPartslist.Columns[index2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                            int num = int.Parse(strArray2[2]);
                            if (num > 0)
                            {
                              this.dgPartslist.Columns[index2].Width = num;
                              this.dgPartslist.Columns[index2].Visible = true;
                            }
                            this.dgPartslist.Columns[index2].HeaderText = strArray2[0];
                            break;
                          }
                          break;
                        }
                        this.dgPartslist.Columns[index2].Visible = false;
                        break;
                      }
                      if (strArray2[3] == "True")
                      {
                        string str2 = strArray2[1];
                        if (strArray2.Length >= 5)
                        {
                          if (str2.Equals("L"))
                          {
                            this.dgPartslist.Columns[index2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            this.dgPartslist.Columns[index2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                          }
                          else if (str2.Equals("R"))
                          {
                            this.dgPartslist.Columns[index2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            this.dgPartslist.Columns[index2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                          }
                          else if (str2.Equals("C"))
                          {
                            this.dgPartslist.Columns[index2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            this.dgPartslist.Columns[index2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                          }
                          int num = int.Parse(strArray2[2]);
                          if (num > 0)
                          {
                            this.dgPartslist.Columns[index2].Width = num;
                            this.dgPartslist.Columns[index2].Visible = true;
                          }
                          this.dgPartslist.Columns[index2].HeaderText = strArray2[0];
                          break;
                        }
                        break;
                      }
                      this.dgPartslist.Columns[index2].Visible = false;
                      break;
                    }
                    break;
                  }
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
      catch
      {
      }
    }

    private void AddSchemaColumns(XmlNode schemaNode)
    {
      try
      {
        for (int index = 0; index < schemaNode.Attributes.Count; ++index)
        {
          DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
          viewTextBoxColumn.HeaderCell = new DataGridViewColumnHeaderCell();
          viewTextBoxColumn.Name = schemaNode.Attributes[index].Name;
          viewTextBoxColumn.Tag = (object) schemaNode.Attributes[index].Value;
          viewTextBoxColumn.DefaultCellStyle.NullValue = (object) string.Empty;
          viewTextBoxColumn.HeaderCell.Style.Alignment = viewTextBoxColumn.DefaultCellStyle.Alignment;
          viewTextBoxColumn.Visible = false;
          if (!this.dgPartslist.Columns.Contains((DataGridViewColumn) viewTextBoxColumn))
            this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
        }
      }
      catch
      {
      }
    }

    private void AddCheckBoxColumn()
    {
      try
      {
        DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
        DatagridViewCheckBoxHeaderCell checkBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
        checkBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(this.chkHeader_OnCheckBoxClicked);
        checkBoxHeaderCell.Value = (object) string.Empty;
        viewCheckBoxColumn.HeaderCell = (DataGridViewColumnHeaderCell) checkBoxHeaderCell;
        viewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        viewCheckBoxColumn.Frozen = true;
        viewCheckBoxColumn.TrueValue = (object) true;
        viewCheckBoxColumn.FalseValue = (object) false;
        viewCheckBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        viewCheckBoxColumn.Name = "CHK";
        this.dgPartslist.Columns.Add((DataGridViewColumn) viewCheckBoxColumn);
        this.dgPartslist.Columns[0].Visible = true;
      }
      catch
      {
      }
    }

    private void ReadMandatoryAttribKeysFromSchema(XmlNode schemaNode)
    {
      try
      {
        this.sPListStatusColName = Program.iniServers[this.frmParent.ServerId].items["PLIST", "PART_STATUS"];
        for (int index = 0; index < schemaNode.Attributes.Count; ++index)
        {
          if (schemaNode.Attributes[index].Value.ToUpper().Equals("PARTNUMBER"))
            this.attPartNoElement = schemaNode.Attributes[index].Name;
          if (schemaNode.Attributes[index].Value.ToUpper().Equals("PARTNAME"))
            this.attPartNameElement = schemaNode.Attributes[index].Name;
          if (schemaNode.Attributes[index].Value.ToUpper().Equals("PICTUREFILE"))
            this.attPictureFileElement = schemaNode.Attributes[index].Name;
          if (this.sPListStatusColName != null && schemaNode.Attributes[index].Value.ToUpper().Equals(this.sPListStatusColName.ToUpper()))
            this.attPartStatusElement = schemaNode.Attributes[index].Name;
        }
      }
      catch
      {
      }
    }

    private void MapDefaultMemoColumn(XmlNode schemaNode)
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string str1 = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", "MEM"];
        if (str1 == null || !(str1 != string.Empty))
          return;
        string[] strArray1 = str1.Split(new string[1]{ "|" }, StringSplitOptions.RemoveEmptyEntries);
        string str2 = strArray1[0];
        if (!this.dgPartslist.Columns.Contains("Mem"))
          return;
        DataGridViewColumn column = this.dgPartslist.Columns["Mem"];
        if (strArray1.Length <= 3 || strArray1[3] == null || !(strArray1[3] != string.Empty))
          return;
        string[] strArray2 = strArray1[3].Split(new string[1]
        {
          ","
        }, StringSplitOptions.RemoveEmptyEntries);
        for (int index = 0; index < strArray2.Length; ++index)
        {
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) schemaNode.Attributes)
          {
            if (attribute.Value.ToUpper() == strArray2[index].ToUpper())
            {
              this.attAdminMemList.Add((object) attribute.Name);
              break;
            }
          }
          if (column.Tag.ToString().ToUpper() == "MEM")
            column.Tag = (object) string.Empty;
          DataGridViewColumn dataGridViewColumn1 = column;
          dataGridViewColumn1.Tag = (object) (dataGridViewColumn1.Tag.ToString() + strArray2[index]);
          if (index < strArray2.Length - 1)
          {
            DataGridViewColumn dataGridViewColumn2 = column;
            dataGridViewColumn2.Tag = (object) (dataGridViewColumn2.Tag.ToString() + ",");
          }
        }
      }
      catch
      {
      }
    }

    private void MapSpecialColumns(XmlNode schemaNode)
    {
      IniFileIO iniFileIo = new IniFileIO();
      ArrayList arrayList = new ArrayList();
      ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PLIST_SETTINGS");
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string empty5 = string.Empty;
      int num1 = 0;
      string empty6 = string.Empty;
      string empty7 = string.Empty;
      int num2 = 0;
      List<SpecialRefColumn> lstSpecialColumn = new List<SpecialRefColumn>();
      for (int index = 0; index < keys.Count; ++index)
      {
        try
        {
          empty1 = string.Empty;
          string[] strArray = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", keys[index].ToString().ToUpper()].Split(new string[1]
          {
            "|"
          }, StringSplitOptions.RemoveEmptyEntries);
          if (strArray.Length != 4)
          {
            if (strArray.Length != 8)
              continue;
          }
          if (strArray[3].ToUpper() == "REF")
          {
            SpecialRefColumn specialRefColumn = new SpecialRefColumn();
            specialRefColumn.sRefKey = keys[index].ToString();
            specialRefColumn.sRefValue = strArray[0];
            try
            {
              specialRefColumn.iRefWidth = int.Parse(strArray[2].ToString());
            }
            catch
            {
              specialRefColumn.iRefWidth = 0;
            }
            lstSpecialColumn.Add(specialRefColumn);
          }
          if (strArray[3].ToUpper() == "INF")
          {
            empty6 = keys[index].ToString();
            empty7 = strArray[0];
            try
            {
              num2 = int.Parse(strArray[2].ToString());
            }
            catch
            {
              num2 = 0;
            }
          }
          if (strArray[3].ToUpper() == "QTY")
          {
            empty5 = strArray[0];
            empty4 = keys[index].ToString();
            try
            {
              num1 = int.Parse(strArray[2].ToString());
            }
            catch
            {
              num1 = 0;
            }
          }
        }
        catch
        {
        }
      }
      try
      {
        DataGridViewImageColumn gridViewImageColumn1 = (DataGridViewImageColumn) null;
        for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
        {
          if (this.dgPartslist.Columns[index].Tag != null)
          {
            if (this.dgPartslist.Columns[index].Tag.ToString().ToUpper() == empty4.ToUpper())
            {
              this.dgPartslist.Columns[index].HeaderText = empty5;
              this.dgPartslist.Columns[index].Name = "QTY";
              if (num1 > 0)
                this.dgPartslist.Columns[index].Visible = true;
            }
            if (this.dgPartslist.Columns[index].Tag.ToString().ToUpper() == empty6.ToUpper())
            {
              this.dgPartslist.Columns[index].HeaderText = empty7;
              this.dgPartslist.Columns[index].Name = "INF1";
              gridViewImageColumn1 = new DataGridViewImageColumn();
              gridViewImageColumn1.Name = "INF";
              gridViewImageColumn1.Tag = (object) empty6;
              gridViewImageColumn1.Visible = true;
              gridViewImageColumn1.HeaderText = empty7;
              gridViewImageColumn1.DefaultCellStyle.NullValue = (object) null;
              gridViewImageColumn1.DefaultCellStyle.Alignment = this.dgPartslist.Columns[index].DefaultCellStyle.Alignment;
              gridViewImageColumn1.HeaderCell.Style.Alignment = gridViewImageColumn1.DefaultCellStyle.Alignment;
              if (num2 > 0)
                this.dgPartslist.Columns[index].Visible = true;
            }
            SpecialRefColumn specialRefColumn = SpecialRefColumn.CheckRefKeyExist(lstSpecialColumn, this.dgPartslist.Columns[index].Tag.ToString().ToUpper());
            if (specialRefColumn != null)
            {
              this.dgPartslist.Columns[index].HeaderText = specialRefColumn.sRefValue;
              this.dgPartslist.Columns[index].Name = "REF1";
              DataGridViewImageColumn gridViewImageColumn2 = new DataGridViewImageColumn();
              gridViewImageColumn2.Name = "REF";
              gridViewImageColumn2.Tag = (object) specialRefColumn.sRefKey;
              gridViewImageColumn2.Visible = true;
              gridViewImageColumn2.HeaderText = specialRefColumn.sRefValue;
              gridViewImageColumn2.DefaultCellStyle.NullValue = (object) null;
              gridViewImageColumn2.DefaultCellStyle.Alignment = this.dgPartslist.Columns[index].DefaultCellStyle.Alignment;
              gridViewImageColumn2.HeaderCell.Style.Alignment = gridViewImageColumn2.DefaultCellStyle.Alignment;
              if (specialRefColumn.iRefWidth > 0)
                this.dgPartslist.Columns[index].Visible = true;
              int displayIndex = this.dgPartslist.Columns["REF1"].DisplayIndex;
              this.dgPartslist.Columns.Remove("REF1");
              gridViewImageColumn2.DisplayIndex = displayIndex;
              this.dgPartslist.Columns.Add((DataGridViewColumn) gridViewImageColumn2);
            }
          }
        }
        if (gridViewImageColumn1 == null)
          return;
        int displayIndex1 = this.dgPartslist.Columns["INF1"].DisplayIndex;
        this.dgPartslist.Columns.Remove("INF1");
        gridViewImageColumn1.DisplayIndex = displayIndex1;
        this.dgPartslist.Columns.Add((DataGridViewColumn) gridViewImageColumn1);
      }
      catch
      {
      }
    }

    private void AddMemoColumns()
    {
      try
      {
        ArrayList arrayList = this.LoadMemoTypeKeys();
        DataGridViewImageColumn[] gridViewImageColumnArray = new DataGridViewImageColumn[19];
        for (int index = 0; index < arrayList.Count; ++index)
        {
          gridViewImageColumnArray[index] = new DataGridViewImageColumn();
          gridViewImageColumnArray[index].HeaderCell = new DataGridViewColumnHeaderCell();
          gridViewImageColumnArray[index].DefaultCellStyle.NullValue = (object) string.Empty;
          gridViewImageColumnArray[index].HeaderCell.Style.Alignment = gridViewImageColumnArray[index].DefaultCellStyle.Alignment;
          gridViewImageColumnArray[index].Visible = false;
          gridViewImageColumnArray[index].Tag = (object) arrayList[index].ToString();
          gridViewImageColumnArray[index].Name = arrayList[index].ToString();
          gridViewImageColumnArray[index].HeaderText = arrayList[index].ToString();
          this.dgPartslist.Columns.Add((DataGridViewColumn) gridViewImageColumnArray[index]);
        }
      }
      catch
      {
      }
    }

    private ArrayList LoadMemoTypeKeys()
    {
      return new ArrayList()
      {
        (object) "MEM",
        (object) "LOCMEM",
        (object) "GBLMEM",
        (object) "ADMMEM",
        (object) "TXTMEM",
        (object) "REFMEM",
        (object) "HYPMEM",
        (object) "PRGMEM",
        (object) "LOCTXTMEM",
        (object) "LOCREFMEM",
        (object) "LOCHYPMEM",
        (object) "LOCPRGMEM",
        (object) "GBLTXTMEM",
        (object) "GBLREFMEM",
        (object) "GBLHYPMEM",
        (object) "GBLPRGMEM",
        (object) "ADMTXTMEM",
        (object) "ADMREFMEM",
        (object) "ADMHYPMEM",
        (object) "ADMPRGMEM"
      };
    }

    public void AddMemoIcon(string sColName, int iRowIndex)
    {
      try
      {
        if (!Program.objAppFeatures.bMemo)
          return;
        if (sColName.ToUpper().Contains("TXT"))
          this.dgPartslist[sColName, iRowIndex].Value = (object) GSPcLocalViewer.Properties.Resources.Memo_Txt;
        else if (sColName.ToUpper().Contains("HYP"))
          this.dgPartslist[sColName, iRowIndex].Value = (object) GSPcLocalViewer.Properties.Resources.Memo_Hyp;
        else if (sColName.ToUpper().Contains("REF"))
          this.dgPartslist[sColName, iRowIndex].Value = (object) GSPcLocalViewer.Properties.Resources.Memo_Ref;
        else if (sColName.ToUpper().Contains("PRG"))
          this.dgPartslist[sColName, iRowIndex].Value = (object) GSPcLocalViewer.Properties.Resources.Memo_Prg;
        else
          this.dgPartslist[sColName, iRowIndex].Value = (object) GSPcLocalViewer.Properties.Resources.Memo_Image;
      }
      catch
      {
      }
    }

    public void ClearMemoIcons(int iRowIndex)
    {
      try
      {
        this.dgPartslist["MEM", iRowIndex].Value = (object) null;
        this.dgPartslist["LOCMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["GBLMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["ADMMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["TXTMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["REFMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["HYPMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["PRGMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["LOCTXTMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["LOCREFMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["LOCHYPMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["LOCPRGMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["GBLTXTMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["GBLREFMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["GBLHYPMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["GBLPRGMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["ADMTXTMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["ADMREFMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["ADMHYPMEM", iRowIndex].Value = (object) null;
        this.dgPartslist["ADMPRGMEM", iRowIndex].Value = (object) null;
      }
      catch (Exception ex)
      {
      }
    }

    private void PopUpMemo(int iColumnIndex, int iRowIndex)
    {
      try
      {
        if (!Program.objAppFeatures.bMemo)
          return;
        string sScope = string.Empty;
        switch (this.dgPartslist.Columns[iColumnIndex].Name.ToUpper())
        {
          case "MEM":
          case "LOCMEM":
          case "GBLMEM":
          case "ADMMEM":
          case "TXTMEM":
          case "REFMEM":
          case "HYPMEM":
          case "PRGMEM":
          case "LOCTXTMEM":
          case "LOCREFMEM":
          case "LOCHYPMEM":
          case "LOCPRGMEM":
          case "GBLTXTMEM":
          case "GBLREFMEM":
          case "GBLHYPMEM":
          case "GBLPRGMEM":
          case "ADMTXTMEM":
          case "ADMREFMEM":
          case "ADMHYPMEM":
          case "ADMPRGMEM":
            if (this.dgPartslist[iColumnIndex, iRowIndex] == null || iRowIndex < 0)
              break;
            string sAdminMemoValues = string.Empty;
            try
            {
              for (int index = 0; index < this.attAdminMemList.Count; ++index)
              {
                if (this.dgPartslist.Columns.Contains(this.attAdminMemList[index].ToString()) && this.dgPartslist[this.attAdminMemList[index].ToString(), iRowIndex] != null)
                  sAdminMemoValues = sAdminMemoValues + this.dgPartslist[this.attAdminMemList[index].ToString(), iRowIndex].Value + "**";
              }
            }
            catch
            {
            }
            try
            {
              if (this.dgPartslist[iColumnIndex, iRowIndex].Value == null)
                break;
              string sUpdateDate = string.Empty;
              try
              {
                if (this.dgPartslist[this.attListUpdateDateElement, iRowIndex] != null)
                  sUpdateDate = this.dgPartslist[this.attListUpdateDateElement, iRowIndex].Value.ToString();
              }
              catch
              {
                if (sUpdateDate != null && !(sUpdateDate == ""))
                {
                  if (sUpdateDate.Length != 0)
                    goto label_20;
                }
                sUpdateDate = DateTime.Now.ToString("M/d/yyyy").Replace('-', '/');
              }
label_20:
              try
              {
                sScope = this.dgPartslist.Columns[iColumnIndex].Name.ToUpper();
                sScope = !sScope.Contains("LOC") ? (!sScope.Contains("ADM") ? (!sScope.Contains("GBL") ? string.Empty : "GBL") : "ADM") : "LOC";
              }
              catch
              {
              }
              this.frmParent.ShowPartMemos(this.CurrentPartNumber, sAdminMemoValues, sUpdateDate, sScope);
              this.frmParent.PartMemoExists(this.CurrentPartNumber, sAdminMemoValues, iRowIndex);
              break;
            }
            catch
            {
              break;
            }
        }
      }
      catch
      {
      }
    }

    public void changePartList(XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
    {
      this.dgPartslist.Visible = false;
      this.LoadPartsList(schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
    }

    private static void ChangeBuffer(Control ctrl)
    {
      if (SystemInformation.TerminalServerSession)
        return;
      typeof (Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue((object) ctrl, (object) true, (object[]) null);
    }

    public void OnOffFeatures()
    {
      try
      {
        this.tsmAddPartMemo.Visible = Program.objAppFeatures.bMemo;
        this.toolStripSeparator2.Visible = Program.objAppFeatures.bMemo;
      }
      catch
      {
      }
    }

    public void SetGridHeaderText()
    {
      try
      {
        if (this.IsDisposed)
          return;
        foreach (DataGridViewColumn column in (BaseCollection) this.dgPartslist.Columns)
        {
          if (this.curListSchema.Attributes[column.Name] != null)
          {
            try
            {
              if (Program.iniServers[this.curServerId].items["PLIST_SETTINGS", this.curListSchema.Attributes[column.Name].Value.ToUpper()] != null)
              {
                IniFileIO iniFileIo = new IniFileIO();
                ArrayList arrayList = new ArrayList();
                ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PLIST_SETTINGS");
                for (int index = 0; index < keys.Count; ++index)
                {
                  if (keys[index].ToString().ToUpper() == this.curListSchema.Attributes[column.Name].Value.ToUpper())
                  {
                    string keyValue = iniFileIo.GetKeyValue("PLIST_SETTINGS", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
                    string sDefaultHeaderValue = keyValue.Substring(0, keyValue.IndexOf("|"));
                    column.HeaderText = this.GetDGHeaderCellValue(this.curListSchema.Attributes[column.Name].Value.ToUpper(), sDefaultHeaderValue);
                    break;
                  }
                }
              }
            }
            catch
            {
            }
          }
          if (column.Name.ToUpper().Contains("MEM"))
          {
            try
            {
              string str = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", column.Name.ToString().ToUpper()];
              string sDefaultHeaderValue = str.Substring(0, str.IndexOf("|"));
              column.HeaderText = this.GetDGHeaderCellValue(column.Name.ToString().ToUpper(), sDefaultHeaderValue);
            }
            catch
            {
            }
          }
          if (!(column.Name.ToUpper() == "REF"))
          {
            if (!column.Name.ToUpper().Contains("QTY"))
            {
              if (!column.Name.ToUpper().Contains("INF"))
                continue;
            }
          }
          try
          {
            string attributeKey = this.FindAttributeKey(column.Tag.ToString());
            if (this.curListSchema.Attributes[attributeKey] != null)
            {
              if (Program.iniServers[this.curServerId].items["PLIST_SETTINGS", this.curListSchema.Attributes[attributeKey].Value.ToUpper()] != null)
              {
                string str = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", this.curListSchema.Attributes[attributeKey].Value.ToUpper()];
                string sDefaultHeaderValue = str.Substring(0, str.IndexOf("|"));
                column.HeaderText = this.GetDGHeaderCellValue(this.curListSchema.Attributes[attributeKey].Value.ToUpper(), sDefaultHeaderValue);
              }
            }
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

    public void SetJumpGridHeaderText()
    {
      try
      {
        if (this.IsDisposed)
          return;
        foreach (DataGridViewColumn column in (BaseCollection) this.dgJumps.Columns)
        {
          try
          {
            if (Program.iniServers[this.curServerId].items["PLIST_JUMP_SETTINGS", column.Name.ToUpper()] != null)
            {
              string str = Program.iniServers[this.frmParent.ServerId].items["PLIST_JUMP_SETTINGS", column.Name.ToUpper()];
              string sDefaultHeaderValue = str.Substring(0, str.IndexOf("|"));
              column.HeaderText = this.GetJumpsHeaderCellValue(column.Name.ToUpper(), sDefaultHeaderValue);
            }
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

    private void addSortingColumn()
    {
      try
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.HeaderCell = new DataGridViewColumnHeaderCell();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        viewTextBoxColumn.HeaderCell.Value = (object) "AutoIndexColumn";
        viewTextBoxColumn.Name = "AutoIndexColumn";
        viewTextBoxColumn.Tag = (object) "AutoIndexColumn";
        viewTextBoxColumn.Visible = false;
        viewTextBoxColumn.ReadOnly = true;
        viewTextBoxColumn.DefaultCellStyle.NullValue = (object) string.Empty;
        this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
      }
      catch
      {
      }
    }

    private void addSelectionListKeyCol()
    {
      try
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.HeaderCell = new DataGridViewColumnHeaderCell();
        viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        viewTextBoxColumn.HeaderCell.Value = (object) "PART_SLIST_KEY";
        viewTextBoxColumn.Name = "PART_SLIST_KEY";
        viewTextBoxColumn.Tag = (object) "PART_SLIST_KEY";
        viewTextBoxColumn.Visible = false;
        viewTextBoxColumn.ReadOnly = true;
        viewTextBoxColumn.DefaultCellStyle.NullValue = (object) string.Empty;
        this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
      }
      catch
      {
      }
    }

    public void SavePartsListColumnSizes()
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PLIST_JUMP_SETTINGS");
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (string str in keys)
          dictionary.Add(str, iniFileIo.GetKeyValue("PLIST_JUMP_SETTINGS", str, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini"));
        if (!this.IsDisposed)
        {
          foreach (DataGridViewColumn column in (BaseCollection) this.dgJumps.Columns)
          {
            if (keys.Count > 0)
            {
              string str1 = dictionary[column.Name].ToString();
              if (str1.Split('|')[2] != "0")
              {
                string[] strArray = str1.Split('|');
                strArray[2] = column.Width.ToString();
                string str2 = "";
                foreach (string str3 in strArray)
                  str2 = str2 + str3 + "|";
                str1 = str2.TrimEnd('|');
              }
              Program.iniServers[this.frmParent.ServerId].UpdateItem("PLIST_JUMP_SETTINGS", column.Name, str1);
            }
          }
        }
      }
      catch
      {
      }
      try
      {
        if (this.IsDisposed)
          return;
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PLIST_SETTINGS");
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (string KeyName in keys)
          dictionary.Add(KeyName.ToUpper(), iniFileIo.GetKeyValue("PLIST_SETTINGS", KeyName, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini"));
        foreach (DataGridViewColumn column in (BaseCollection) this.dgPartslist.Columns)
        {
          try
          {
            if (keys.Count > 0 && this.curListSchema.Attributes[column.Name] != null)
            {
              string upper = this.curListSchema.Attributes[column.Name].Value.ToString().ToUpper();
              string KeyValue = dictionary[upper].ToString();
              if (KeyValue.Split('|')[2] != "0")
              {
                string[] strArray = KeyValue.Split('|');
                strArray[2] = column.Width.ToString();
                string str1 = "";
                foreach (string str2 in strArray)
                  str1 = str1 + str2 + "|";
                KeyValue = str1.TrimEnd('|');
              }
              iniFileIo.WriteValue("PLIST_SETTINGS", upper, KeyValue, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
            }
            if (column.Name.ToUpper() == "MEM" || column.Name.ToUpper() == "LOCMEM" || (column.Name.ToUpper() == "HYPMEM" || column.Name.ToUpper() == "LOCPRGMEM") || (column.Name.ToUpper() == "GBLMEM" || column.Name.ToUpper() == "PRGMEM" || (column.Name.ToUpper() == "GBLTXTMEM" || column.Name.ToUpper() == "ADMMEM")) || (column.Name.ToUpper() == "LOCTXTMEM" || column.Name.ToUpper() == "GBLREFMEM" || (column.Name.ToUpper() == "TXTMEM" || column.Name.ToUpper() == "LOCREFMEM") || (column.Name.ToUpper() == "GBLHYPMEM" || column.Name.ToUpper() == "REFMEM" || (column.Name.ToUpper() == "LOCHYPMEM" || column.Name.ToUpper() == "GBLPRGMEM"))) || (column.Name.ToUpper() == "ADMTXTMEM" || column.Name.ToUpper() == "ADMREFMEM" || (column.Name.ToUpper() == "ADMHYPMEM" || column.Name.ToUpper() == "ADMPRGMEM")))
            {
              string KeyValue = dictionary[column.Name].ToString();
              if (KeyValue.Split('|')[2] != "0")
              {
                string[] strArray = KeyValue.Split('|');
                strArray[2] = column.Width.ToString();
                string str1 = "";
                foreach (string str2 in strArray)
                  str1 = str1 + str2 + "|";
                KeyValue = str1.TrimEnd('|');
              }
              iniFileIo.WriteValue("PLIST_SETTINGS", column.Name, KeyValue, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
            }
            if (column.Name.ToUpper() == "REF")
            {
              string upper = this.curListSchema.Attributes[this.FindAttributeKey(this.dgPartslist.Columns["REF"].Tag.ToString())].Value.ToString().ToUpper();
              if (this.curListSchema.Attributes[upper] != null)
              {
                string KeyValue = dictionary[upper].ToString();
                if (KeyValue.Split('|')[2] != "0")
                {
                  string[] strArray = KeyValue.Split('|');
                  strArray[2] = column.Width.ToString();
                  string str1 = "";
                  foreach (string str2 in strArray)
                    str1 = str1 + str2 + "|";
                  KeyValue = str1.TrimEnd('|');
                }
                iniFileIo.WriteValue("PLIST_SETTINGS", upper, KeyValue, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
              }
            }
            if (column.Name.ToUpper() == "QTY")
            {
              string attributeKey = this.FindAttributeKey(this.dgPartslist.Columns["QTY"].Tag.ToString());
              string upper = this.curListSchema.Attributes[attributeKey].Value.ToString().ToUpper();
              if (this.curListSchema.Attributes[attributeKey] != null)
              {
                string KeyValue = dictionary[upper].ToString();
                if (KeyValue.Split('|')[2] != "0")
                {
                  string[] strArray = KeyValue.Split('|');
                  strArray[2] = column.Width.ToString();
                  string str1 = "";
                  foreach (string str2 in strArray)
                    str1 = str1 + str2 + "|";
                  KeyValue = str1.TrimEnd('|');
                }
                iniFileIo.WriteValue("PLIST_SETTINGS", upper, KeyValue, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
              }
            }
            if (column.Name.ToUpper() == "INF")
            {
              string attributeKey = this.FindAttributeKey(this.dgPartslist.Columns["INF"].Tag.ToString());
              string upper = this.curListSchema.Attributes[attributeKey].Value.ToString().ToUpper();
              if (this.curListSchema.Attributes[attributeKey] != null)
              {
                string KeyValue = dictionary[upper].ToString();
                if (KeyValue.Split('|')[2] != "0")
                {
                  string[] strArray = KeyValue.Split('|');
                  strArray[2] = column.Width.ToString();
                  string str1 = "";
                  foreach (string str2 in strArray)
                    str1 = str1 + str2 + "|";
                  KeyValue = str1.TrimEnd('|');
                }
                iniFileIo.WriteValue("PLIST_SETTINGS", upper, KeyValue, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
              }
            }
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

    private void GetUpdateDateElement()
    {
      try
      {
        string str1 = string.Empty;
        string str2 = string.Empty;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.curListSchema.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
            str1 = attribute.Name;
          if (attribute.Value.ToUpper().Equals("UPDATEDATEPL"))
            str2 = attribute.Name;
          if (attribute.Value.ToUpper().Equals("PARTSLISTTITLE"))
            this.sPLTitle = attribute.Name;
        }
        if (str2 != string.Empty)
          this.attListUpdateDateElement = str2;
        else
          this.attListUpdateDateElement = str1;
      }
      catch
      {
      }
    }

    private bool IntervalElapsed(DateTime dtLocal, DateTime dtServer)
    {
      try
      {
        int num = int.Parse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"]);
        return (dtServer.Date - dtLocal.Date).TotalDays >= (double) num;
      }
      catch
      {
        return true;
      }
    }

    private DateTime DataUpdateDate(string sDataUpdateFilePath)
    {
      try
      {
        if (!File.Exists(sDataUpdateFilePath))
          return DateTime.Now;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(sDataUpdateFilePath);
        return DateTime.Parse(xmlDocument.SelectSingleNode("//filelastmodified").InnerText, (IFormatProvider) new CultureInfo("fr-FR", false));
      }
      catch
      {
        return DateTime.Now;
      }
    }

    public void CheckUncheckRow(string sPartNumber, bool bCheck)
    {
      try
      {
        foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
        {
          try
          {
            if (this.lstDisableRows.Contains(row.Index))
              row.Cells["CHK"].ReadOnly = true;
            if (row.Cells[this.attPartNoElement].Value != null && row.Cells[this.attPartNoElement].Value.ToString() != string.Empty)
            {
              if (this.bSelectionMode)
              {
                if (row.Cells["PART_SLIST_KEY"].Value.ToString().ToUpper().Trim() == sPartNumber.Trim().ToUpper())
                  row.Cells["CHK"].Value = (object) bCheck;
              }
              else if (row.Cells[this.attPartNoElement].Value.ToString() == sPartNumber)
                row.Cells["CHK"].Value = (object) bCheck;
            }
            else if (row.Cells[this.attPartNameElement].Value.ToString() == sPartNumber)
              row.Cells["CHK"].Value = (object) bCheck;
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

    public void UncheckAllRows()
    {
      try
      {
        foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
        {
          try
          {
            row.Cells["CHK"].Value = (object) false;
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

    private void tsmAddToSelectionList_Click(object sender, EventArgs e)
    {
      try
      {
        foreach (DataGridViewRow selectedRow in (BaseCollection) this.dgPartslist.SelectedRows)
        {
          if (!this.lstDisableRows.Contains(selectedRow.Index))
          {
            if (selectedRow.Cells["CHK"].Value.ToString().ToUpper() == "FALSE")
              selectedRow.Cells["CHK"].Value = (object) true;
            else if (this.dgPartslist.SelectedRows.Count == 1)
              this.frmParent.ShowQuantityScreen(this.dgPartslist.CurrentRow.Cells["PART_SLIST_KEY"].Value.ToString());
          }
        }
      }
      catch
      {
      }
    }

    public XmlNode GetCurListSchema()
    {
      XmlNode xmlNode = (XmlNode) null;
      try
      {
        xmlNode = this.curListSchema;
      }
      catch
      {
      }
      return xmlNode;
    }

    public void LoadResources()
    {
      this.tsbAddToSelectionList.Text = this.GetResource("Add to Selection List", "ADD_TO_SELECTION_LIST", ResourceType.TOOLSTRIP);
      this.tsbAddPartMemo.Text = this.GetResource("Add Part Memo", "ADD_PART_MEMO", ResourceType.TOOLSTRIP);
      this.tsbSelectAll.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.TOOLSTRIP);
      this.tsbClearSelection.Text = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.TOOLSTRIP);
      this.tsBtnPrev.Text = this.GetResource("Previous List", "PREVIOUS_LIST", ResourceType.TOOLSTRIP);
      this.tsBtnNext.Text = this.GetResource("Next List", "NEXT_LIST", ResourceType.TOOLSTRIP);
      this.tsmAddPartMemo.Text = this.GetResource("Add Part Memo", "ADD_PART_MEMO", ResourceType.CONTEXT_MENU);
      this.tsmAddToSelectionList.Text = this.GetResource("Add To Selection List", "ADD_TO_SELECTION_LIST", ResourceType.CONTEXT_MENU);
      this.tsmClearSelection.Text = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.CONTEXT_MENU);
      this.selectAllToolStripMenuItem.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.CONTEXT_MENU);
      this.copyToClipboardToolStripMenuItem.Text = this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.CONTEXT_MENU);
      this.commaSeparatedToolStripMenuItem.Text = this.GetResource("Comma Separated", "COMMA_SEPERATED", ResourceType.CONTEXT_MENU);
      this.tabSeparatedToolStripMenuItem.Text = this.GetResource("Tab Seperated", "TAB_SEPERATED", ResourceType.CONTEXT_MENU);
      this.exportToFileToolStripMenuItem.Text = this.GetResource("Export To File", "EXPORT_TO_FILE", ResourceType.CONTEXT_MENU);
      this.commaSeparatedToolStripMenuItem1.Text = this.GetResource("Comma Separated", "COMMA_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
      this.tabSeparatedToolStripMenuItem1.Text = this.GetResource("Tab Seperated", "TAB_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
      this.nextListToolStripMenuItem.Text = this.GetResource("Next List", "NEXT_LIST", ResourceType.CONTEXT_MENU);
      this.previousListToolStripMenuItem.Text = this.GetResource("Previous List", "PREVIOUS_LIST", ResourceType.CONTEXT_MENU);
      this.tsbPartistInfo.Text = this.GetResource("Information", "INFORMATION", ResourceType.CONTEXT_MENU);
      this.rowSelectionModeToolStripMenuItem.Text = this.GetResource("Row Selection Mode", "Row_Selection_Mode", ResourceType.CONTEXT_MENU);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MAIN_FORM']" + "/Screen[@Name='PARTS_LIST']";
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

    private void UpdatePListInfoTextBox()
    {
      if (this.lblPartsListInfo.InvokeRequired)
        this.lblPartsListInfo.Invoke((Delegate) new frmViewerPartslist.UpdatePListInfoTextBoxDelegate(this.UpdatePListInfoTextBox));
      else
        this.lblPartsListInfo.Text = this.partlistInfoMsg;
    }

    private void EnableDisablePListInfoBtn()
    {
      if (this.toolStrip1.InvokeRequired)
        this.toolStrip1.Invoke((Delegate) new frmViewerPartslist.EnableDisablePListInfoBtnDelegate(this.EnableDisablePListInfoBtn));
      else if (this.partlistInfoMsg == string.Empty)
        this.tsbPartistInfo.Enabled = false;
      else
        this.tsbPartistInfo.Enabled = true;
    }

    private void CheckPListInfoBtn(bool bState)
    {
      if (this.toolStrip1.InvokeRequired)
        this.toolStrip1.Invoke((Delegate) new frmViewerPartslist.CheckPListInfoBtnDelegate(this.CheckPListInfoBtn), (object) bState);
      else if (bState)
        this.tsbPartistInfo.CheckState = CheckState.Checked;
      else
        this.tsbPartistInfo.CheckState = CheckState.Unchecked;
    }

    private void ShowHidePListInfoPanel(bool bState)
    {
      if (this.pnlInfo.InvokeRequired)
        this.pnlInfo.Invoke((Delegate) new frmViewerPartslist.ShowHidePListInfoPanelBoxDelegate(this.ShowHidePListInfoPanel), (object) bState);
      else
        this.pnlInfo.Visible = bState;
    }

    private void InitializeJumpsList()
    {
      if (this.dgJumps.InvokeRequired)
      {
        this.dgJumps.Invoke((Delegate) new frmViewerPartslist.InitializeJumpsListDelegate(this.InitializeJumpsList));
      }
      else
      {
        try
        {
          this.addJumpColumns();
        }
        catch
        {
        }
      }
    }

    private void addJumpCol(string sColKey, string sDefaultColHeader, int iDefaultColWidth, DataGridViewContentAlignment dgDefaultContentAlignment, bool bVisible)
    {
      try
      {
        DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
        viewTextBoxColumn.HeaderCell = new DataGridViewColumnHeaderCell();
        string str = Program.iniServers[this.frmParent.ServerId].items["PLIST_JUMP_SETTINGS", sColKey];
        if (str != null && str != string.Empty)
        {
          string[] strArray = str.Split(new string[1]{ "|" }, StringSplitOptions.RemoveEmptyEntries);
          if (str[0].ToString() != string.Empty)
            viewTextBoxColumn.HeaderCell.Value = (object) this.GetJumpsHeaderCellValue(sDefaultColHeader, strArray[0]);
          if (str[0].ToString() != string.Empty)
          {
            try
            {
              if (strArray[1].ToString().ToUpper() == "C")
              {
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                viewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              }
              if (strArray[1].ToString().ToUpper() == "L")
              {
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                viewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              }
              if (strArray[1].ToString().ToUpper() == "R")
              {
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                viewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              }
            }
            catch
            {
              viewTextBoxColumn.DefaultCellStyle.Alignment = dgDefaultContentAlignment;
            }
          }
          if (str[0].ToString() != string.Empty)
          {
            int result = 0;
            if (!int.TryParse(strArray[2], out result))
              viewTextBoxColumn.Width = iDefaultColWidth;
            else
              viewTextBoxColumn.Width = result;
          }
        }
        else
        {
          viewTextBoxColumn.HeaderCell.Value = (object) sDefaultColHeader;
          viewTextBoxColumn.Width = iDefaultColWidth;
          viewTextBoxColumn.Tag = (object) sColKey;
          viewTextBoxColumn.DefaultCellStyle.Alignment = dgDefaultContentAlignment;
          viewTextBoxColumn.HeaderCell.Style.Alignment = dgDefaultContentAlignment;
        }
        viewTextBoxColumn.Name = sColKey;
        viewTextBoxColumn.Visible = bVisible;
        viewTextBoxColumn.ReadOnly = true;
        viewTextBoxColumn.DefaultCellStyle.NullValue = (object) string.Empty;
        this.dgJumps.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
      }
      catch
      {
      }
    }

    private void addJumpColumns()
    {
      try
      {
        this.dgJumps.Columns.Clear();
        this.AddImageColumn();
        this.addJumpCol("KEY", "Key", 100, DataGridViewContentAlignment.MiddleLeft, true);
        this.addJumpCol("TITLE", "Title", 100, DataGridViewContentAlignment.MiddleLeft, true);
        this.addJumpCol("JUMPSTRING", "JumpString", 100, DataGridViewContentAlignment.MiddleLeft, false);
      }
      catch
      {
      }
    }

    private void AddImageColumn()
    {
      try
      {
        DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
        gridViewImageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        gridViewImageColumn.Frozen = true;
        gridViewImageColumn.HeaderText = string.Empty;
        gridViewImageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        gridViewImageColumn.Name = "arrow";
        this.dgJumps.Columns.Add((DataGridViewColumn) gridViewImageColumn);
      }
      catch
      {
      }
    }

    private void LoadJumpsInGrid(XmlDocument objXmlDoc)
    {
      if (this.dgJumps.InvokeRequired)
      {
        this.dgJumps.Invoke((Delegate) new frmViewerPartslist.LoadJumpsInGridDelegate(this.LoadJumpsInGrid), (object) objXmlDoc);
      }
      else
      {
        List<string> stringList = new List<string>();
        this.dgJumps.AllowUserToAddRows = true;
        this.dgJumps.CurrentCell = this.dgJumps[0, 0];
        this.dgJumps.BeginEdit(true);
        try
        {
          this.dgJumps.Rows.Clear();
          this.dgJumps.AllowUserToAddRows = false;
          XmlNodeList xmlNodeList = objXmlDoc.SelectNodes("//Jumps/Jump");
          if (xmlNodeList.Count != 0)
          {
            List<string> filteredBooks = this.GetFilteredBooks();
            foreach (XmlNode xmlNode in xmlNodeList)
            {
              string str1 = string.Empty;
              string str2 = string.Empty;
              string str3 = string.Empty;
              try
              {
                if (xmlNode.Attributes["DpTxt"] != null)
                  str1 = xmlNode.Attributes["DpTxt"].Value;
                if (xmlNode.Attributes["PgName"] != null)
                  str3 = xmlNode.Attributes["PgName"].Value;
                if (str1.StartsWith("\"") && str1.EndsWith("\""))
                  str1 = str1.Trim('"');
                if (str3.StartsWith("\"") && str3.EndsWith("\""))
                  str3 = str3.Trim('"');
                if (xmlNode.Attributes["BkId"] != null)
                  str2 = xmlNode.Attributes["BkId"].Value;
                if (str2 == string.Empty)
                  str2 = this.BookPublishingId;
              }
              catch
              {
              }
              if (xmlNode.Attributes["Type"].Value.ToUpper().Trim() == "PAGEJUMP")
              {
                if (!this.frmParent.lstFilteredPages.Contains(str3))
                {
                  this.dgJumps.Rows.Add();
                  this.dgJumps["arrow", this.dgJumps.Rows.Count - 1].Value = (object) GSPcLocalViewer.Properties.Resources.arrow;
                  this.dgJumps["TITLE", this.dgJumps.Rows.Count - 1].Value = (object) str1;
                  this.dgJumps["KEY", this.dgJumps.Rows.Count - 1].Value = (object) xmlNode.Attributes["LinkNum"].Value;
                  this.dgJumps["JUMPSTRING", this.dgJumps.Rows.Count - 1].Value = (object) (str2 + "|" + str3 + "|" + xmlNode.Attributes["PartNum"].Value + "|" + xmlNode.Attributes["Type"].Value);
                }
              }
              else
              {
                try
                {
                  if (filteredBooks.Contains(str2))
                  {
                    this.dgJumps.Rows.Add();
                    this.dgJumps["arrow", this.dgJumps.Rows.Count - 1].Value = (object) GSPcLocalViewer.Properties.Resources.arrow;
                    this.dgJumps["TITLE", this.dgJumps.Rows.Count - 1].Value = (object) str1;
                    this.dgJumps["KEY", this.dgJumps.Rows.Count - 1].Value = (object) xmlNode.Attributes["LinkNum"].Value;
                    this.dgJumps["JUMPSTRING", this.dgJumps.Rows.Count - 1].Value = (object) (str2 + "|" + xmlNode.Attributes["PgName"].Value + "|" + xmlNode.Attributes["PartNum"].Value + "|" + xmlNode.Attributes["Type"].Value);
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
        this.dgJumps.AllowUserToResizeColumns = true;
        this.dgJumps.EndEdit();
        this.dgJumps.Visible = true;
      }
    }

    private void HidePartsList()
    {
      try
      {
        this.splitPnlGrids.Panel1.Hide();
        this.splitPnlGrids.Panel1Collapsed = true;
        this.dgPartslist.Visible = false;
        this.Column1.Visible = false;
        this.HidePartsListToolbar();
        if (!this.splitPnlGrids.Panel1Collapsed || !this.splitPnlGrids.Panel2Collapsed)
          return;
        this.frmParent.HidePartsList();
      }
      catch
      {
      }
    }

    public void ShowPartsList()
    {
      try
      {
        this.splitPnlGrids.Panel1.Show();
        this.splitPnlGrids.Panel1Collapsed = false;
        this.ShowPartsListToolbar();
      }
      catch
      {
      }
    }

    private void AdjustGridHeights()
    {
      if (this.splitPnlGrids.InvokeRequired)
      {
        this.splitPnlGrids.Invoke((Delegate) new frmViewerPartslist.AdjustGridHeightsDelegate(this.AdjustGridHeights));
      }
      else
      {
        try
        {
          this.splitPnlGrids.SplitterDistance = Settings.Default.ListSplitterDistance;
        }
        catch
        {
        }
      }
    }

    private void dgJumps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        string[] strArray1 = this.dgJumps["JUMPSTRING", e.RowIndex].Value.ToString().Split('|');
        string str1 = strArray1[1];
        if (str1.StartsWith("\"") && str1.EndsWith("\""))
          str1 = str1.Trim('"');
        string str2 = strArray1[strArray1.Length - 1];
        string[] sArgs = new string[6]
        {
          "-o",
          Program.iniServers[this.frmParent.p_ServerId].sIniKey,
          strArray1[0],
          str1.Trim(),
          "1",
          "-f"
        };
        if (this.frmParent.p_ArgsF != null)
        {
          string[] strArray2 = new string[sArgs.Length + this.frmParent.p_ArgsF.Length];
          Array.Copy((Array) sArgs, (Array) strArray2, sArgs.Length);
          Array.Copy((Array) this.frmParent.p_ArgsF, 0, (Array) strArray2, sArgs.Length, this.frmParent.p_ArgsF.Length);
          sArgs = strArray2;
        }
        if (!Global.SecurityLocksOpen(this.frmParent.GetBookNode(strArray1[0], this.frmParent.p_ServerId), this.frmParent.SchemaNode, this.frmParent.ServerId, this.frmParent))
          return;
        if (str2.ToUpper() == "BOOKJUMP")
          this.frmParent.BookJump(sArgs);
        else
          this.frmParent.PageJump(sArgs);
      }
      catch
      {
      }
    }

    private void splitPnlGrids_SplitterMoved(object sender, SplitterEventArgs e)
    {
      try
      {
        if (this.dgPartslist.RowCount == 0 || this.dgJumps.RowCount == 0)
          return;
        Settings.Default.ListSplitterDistance = this.splitPnlGrids.SplitterDistance;
      }
      catch
      {
      }
    }

    private void ShowPartsListToolbar()
    {
      if (this.dgJumps.InvokeRequired)
      {
        this.dgJumps.Invoke((Delegate) new frmViewerPartslist.ShowPartsListToolbarDelegate(this.ShowPartsListToolbar));
      }
      else
      {
        try
        {
          this.toolStrip1.Visible = true;
        }
        catch
        {
        }
      }
    }

    private void HidePartsListToolbar()
    {
      if (this.dgJumps.InvokeRequired)
      {
        this.dgJumps.Invoke((Delegate) new frmViewerPartslist.HidePartsListToolbarDelegate(this.HidePartsListToolbar));
      }
      else
      {
        try
        {
          this.toolStrip1.Visible = false;
        }
        catch
        {
        }
      }
    }

    private List<string> GetFilteredBooks()
    {
      List<string> stringList = new List<string>();
      try
      {
        string str1 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.frmParent.p_ServerId].sIniKey + "\\Series.xml";
        XmlDocument xmlDocument = new XmlDocument();
        if (File.Exists(str1))
        {
          if (this.frmParent.p_Compressed)
          {
            try
            {
              string str2 = str1.ToLower().Replace(".zip", ".xml");
              Global.Unzip(str1);
              if (File.Exists(str2))
                xmlDocument.Load(str2);
            }
            catch
            {
            }
          }
          else
          {
            try
            {
              xmlDocument.Load(str1);
            }
            catch
            {
              return stringList;
            }
          }
          if (this.frmParent.p_Encrypted)
          {
            try
            {
              string str2 = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
              xmlDocument.DocumentElement.InnerXml = str2;
            }
            catch
            {
              return stringList;
            }
          }
          XmlNode xSchemaNode = xmlDocument.SelectSingleNode("//Schema");
          if (xSchemaNode == null)
            return stringList;
          string str3 = string.Empty;
          string index = string.Empty;
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
          {
            if (attribute.Value.ToUpper().Equals("ID"))
              str3 = attribute.Name;
            else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
              index = attribute.Name;
            else if (attribute.Value.ToUpper().Equals("BOOKTYPE"))
            {
              string name1 = attribute.Name;
            }
            else if (attribute.Value.ToUpper().Equals("COVERPICTURE"))
            {
              string name2 = attribute.Name;
            }
          }
          if (str3 == "" || index == "")
            return stringList;
          XmlNodeList xNodeList = xmlDocument.SelectNodes("//Books/Book");
          if (xNodeList.Count > 0)
          {
            XmlNodeList xmlNodeList = this.frmParent.FilterBooksList(xSchemaNode, xNodeList);
            if (xmlNodeList.Count > 0)
            {
              foreach (XmlNode xmlNode in xmlNodeList)
              {
                if (xmlNode.Attributes.Count > 0)
                  stringList.Add(xmlNode.Attributes[index].Value);
              }
            }
          }
        }
        return stringList;
      }
      catch
      {
        return stringList;
      }
    }

    public void Set_MemoCols_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni()
    {
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList1 = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PLIST_SETTINGS");
        ArrayList arrayList2 = this.LoadMemoTypeKeys();
        for (int index1 = 0; index1 < arrayList2.Count; ++index1)
        {
          try
          {
            string index2 = arrayList2[index1].ToString();
            if (this.dgPartslist.Columns.Contains(index2))
            {
              DataGridViewColumn column = this.dgPartslist.Columns[index2];
              string empty1 = string.Empty;
              string[] strArray = iniFileIo.GetKeyValue("PLIST_SETTINGS", index2, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini").Split(new string[1]
              {
                "|"
              }, StringSplitOptions.RemoveEmptyEntries);
              column.HeaderText = strArray[0];
              string empty2 = string.Empty;
              string str = strArray[1];
              if (str.Equals("L"))
              {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
              }
              else if (str.Equals("R"))
              {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
              }
              else if (str.Equals("C"))
              {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
              }
              if (int.Parse(strArray[2]) > 0)
              {
                if (keys.Contains((object) index2))
                {
                  if (strArray.Length > 4)
                  {
                    if (strArray.Length == 7)
                    {
                      if (strArray[3].ToUpper() == "TRUE")
                      {
                        column.Visible = true;
                        column.Width = int.Parse(strArray[2]);
                      }
                      else
                        column.Visible = false;
                    }
                    else if (strArray[4].ToUpper() == "TRUE")
                    {
                      column.Visible = true;
                      column.Width = int.Parse(strArray[2]);
                    }
                    else
                      column.Visible = false;
                  }
                  else
                  {
                    column.Visible = true;
                    column.Width = int.Parse(strArray[2]);
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
      catch
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

    private delegate void LoadPartsListInGridDelegate(XmlDocument objXmlDoc);

    private delegate void SetListIndexDelegate(XmlNodeList listNodes, int listIndex);

    private delegate void StatusDelegate(string status);

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void InitializePartsListDelegate(XmlNode schemaNode);

    private delegate void UpdatePListInfoTextBoxDelegate();

    private delegate void EnableDisablePListInfoBtnDelegate();

    private delegate void CheckPListInfoBtnDelegate(bool bState);

    private delegate void ShowHidePListInfoPanelBoxDelegate(bool bState);

    private delegate void InitializeJumpsListDelegate();

    private delegate void LoadJumpsInGridDelegate(XmlDocument objXmlDoc);

    private delegate void AdjustGridHeightsDelegate();

    private delegate void ShowPartsListToolbarDelegate();

    private delegate void HidePartsListToolbarDelegate();
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmDataSize
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GSPcLocalViewer
{
  public class frmDataSize : Form
  {
    private IContainer components;
    private Panel pnlForm;
    private PictureBox picLoading;
    private Panel pnlContents;
    private BackgroundWorker bgWorker;
    private ColumnHeader columnHeader1;
    private ColumnHeader columnHeader2;
    private ColumnHeader columnHeader3;
    private ColumnHeader columnHeader4;
    private ColumnHeader columnHeader5;
    private ColumnHeader columnHeader6;
    private TreeListView tlvFolders;
    private ColumnHeader colName;
    private ColumnHeader colSize;
    private ColumnHeader colDate;
    private ImageList ilTreeList;
    private ToolStrip toolStrip1;
    private ToolStripButton tsbClearSelection;
    private ToolStripButton tsbSelectAll;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripButton tsBtnDeleteSelection;
    private Panel pnlDiskSpace;
    private Panel pnlControl;
    private Button btnCancel;
    private Panel pnlDownloadedDataHeader;
    private Label lblDownloadedDataLine;
    private Label lblDownloadedDataHeader;
    private Label lblStatus;
    private Panel pnlDiskSpaceHeader;
    private Label lblDiskSpaceHeaderLine;
    private Label lblDiskSpaceHeader;
    private Panel pnlDiskSpace1;
    private Panel pnlDiskSpaceValue;
    private NumericTextBox txtDiskSpaceValue;
    private ComboBox cmbDiskSpaceValue;
    private CheckBox chkDiskSpace;
    private StatusStrip ssStatus;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripStatusLabel tsStatus;
    private Panel pnlSplitter1;
    private Button btnOK;
    private Panel pnltlvFolders;
    private Panel pnlDiskSpaceAndControl;
    private BackgroundWorker bgChangeDataSize;
    private Label lblInUseBooks;
    private ColumnHeader colStatus;
    private frmViewer frmParent;
    private bool currentlyLoadingData;
    private bool currentlyCalculatingSpace;
    private long stAllocatedSize;
    private long stFolderSize;
    private long stFreeSpace;
    private string sBookInUseMsg;
    private Thread thCalculateSpace;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      TreeListViewItemCollection.TreeListViewItemCollectionComparer collectionComparer = new TreeListViewItemCollection.TreeListViewItemCollectionComparer();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmDataSize));
      this.pnlForm = new Panel();
      this.pnlContents = new Panel();
      this.pnltlvFolders = new Panel();
      this.tlvFolders = new TreeListView();
      this.colName = new ColumnHeader();
      this.colStatus = new ColumnHeader();
      this.colSize = new ColumnHeader();
      this.colDate = new ColumnHeader();
      this.ilTreeList = new ImageList(this.components);
      this.lblStatus = new Label();
      this.pnlDownloadedDataHeader = new Panel();
      this.lblDownloadedDataLine = new Label();
      this.lblDownloadedDataHeader = new Label();
      this.picLoading = new PictureBox();
      this.pnlDiskSpaceAndControl = new Panel();
      this.pnlControl = new Panel();
      this.lblInUseBooks = new Label();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.pnlDiskSpace = new Panel();
      this.pnlDiskSpace1 = new Panel();
      this.pnlDiskSpaceValue = new Panel();
      this.txtDiskSpaceValue = new NumericTextBox();
      this.cmbDiskSpaceValue = new ComboBox();
      this.pnlSplitter1 = new Panel();
      this.chkDiskSpace = new CheckBox();
      this.pnlDiskSpaceHeader = new Panel();
      this.lblDiskSpaceHeaderLine = new Label();
      this.lblDiskSpaceHeader = new Label();
      this.bgWorker = new BackgroundWorker();
      this.columnHeader1 = new ColumnHeader();
      this.columnHeader2 = new ColumnHeader();
      this.columnHeader3 = new ColumnHeader();
      this.columnHeader4 = new ColumnHeader();
      this.columnHeader5 = new ColumnHeader();
      this.columnHeader6 = new ColumnHeader();
      this.toolStrip1 = new ToolStrip();
      this.tsbClearSelection = new ToolStripButton();
      this.tsbSelectAll = new ToolStripButton();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.tsBtnDeleteSelection = new ToolStripButton();
      this.ssStatus = new StatusStrip();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      this.tsStatus = new ToolStripStatusLabel();
      this.bgChangeDataSize = new BackgroundWorker();
      this.pnlForm.SuspendLayout();
      this.pnlContents.SuspendLayout();
      this.pnltlvFolders.SuspendLayout();
      this.pnlDownloadedDataHeader.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.pnlDiskSpaceAndControl.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.pnlDiskSpace.SuspendLayout();
      this.pnlDiskSpace1.SuspendLayout();
      this.pnlDiskSpaceValue.SuspendLayout();
      this.pnlDiskSpaceHeader.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.ssStatus.SuspendLayout();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlContents);
      this.pnlForm.Controls.Add((Control) this.pnlDiskSpaceAndControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(2, 27);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(540, 457);
      this.pnlForm.TabIndex = 2;
      this.pnlContents.Controls.Add((Control) this.pnltlvFolders);
      this.pnlContents.Controls.Add((Control) this.lblStatus);
      this.pnlContents.Controls.Add((Control) this.pnlDownloadedDataHeader);
      this.pnlContents.Controls.Add((Control) this.picLoading);
      this.pnlContents.Dock = DockStyle.Fill;
      this.pnlContents.Location = new Point(0, 0);
      this.pnlContents.Name = "pnlContents";
      this.pnlContents.Padding = new Padding(2);
      this.pnlContents.Size = new Size(538, 368);
      this.pnlContents.TabIndex = 21;
      this.pnltlvFolders.Controls.Add((Control) this.tlvFolders);
      this.pnltlvFolders.Dock = DockStyle.Fill;
      this.pnltlvFolders.Location = new Point(2, 30);
      this.pnltlvFolders.Name = "pnltlvFolders";
      this.pnltlvFolders.Size = new Size(534, 336);
      this.pnltlvFolders.TabIndex = 20;
      this.tlvFolders.AllowColumnReorder = true;
      this.tlvFolders.CheckBoxes = CheckBoxesTypes.Recursive;
      this.tlvFolders.Columns.AddRange(new ColumnHeader[4]
      {
        this.colName,
        this.colStatus,
        this.colSize,
        this.colDate
      });
      collectionComparer.Column = 0;
      collectionComparer.SortOrder = SortOrder.Ascending;
      this.tlvFolders.Comparer = (ITreeListViewItemComparer) collectionComparer;
      this.tlvFolders.Dock = DockStyle.Fill;
      this.tlvFolders.HideSelection = false;
      this.tlvFolders.HighlightBackColor = Color.Blue;
      this.tlvFolders.HighlightForeColor = Color.White;
      this.tlvFolders.Location = new Point(0, 0);
      this.tlvFolders.MultiSelect = false;
      this.tlvFolders.Name = "tlvFolders";
      this.tlvFolders.Size = new Size(534, 336);
      this.tlvFolders.SmallImageList = this.ilTreeList;
      this.tlvFolders.TabIndex = 1;
      this.tlvFolders.UseCompatibleStateImageBehavior = false;
      this.tlvFolders.ItemChecked += new ItemCheckedEventHandler(this.tlvFolders_ItemChecked);
      this.tlvFolders.BeforeExpand += new TreeListViewCancelEventHandler(this.tlvFolders_BeforeExpand);
      this.tlvFolders.BeforeCollapse += new TreeListViewCancelEventHandler(this.tlvFolders_BeforeCollapse);
      this.colName.Text = "Folder Name";
      this.colName.Width = 201;
      this.colStatus.Text = "Status";
      this.colStatus.Width = 93;
      this.colSize.Text = "Size";
      this.colSize.TextAlign = HorizontalAlignment.Right;
      this.colSize.Width = 93;
      this.colDate.Text = "Modified Date";
      this.colDate.TextAlign = HorizontalAlignment.Right;
      this.colDate.Width = 135;
      this.ilTreeList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilTreeList.ImageStream");
      this.ilTreeList.TransparentColor = Color.Transparent;
      this.ilTreeList.Images.SetKeyName(0, "");
      this.ilTreeList.Images.SetKeyName(1, "directory.png");
      this.ilTreeList.Images.SetKeyName(2, "directory_opened.png");
      this.ilTreeList.Images.SetKeyName(3, "book_blue.png");
      this.lblStatus.BorderStyle = BorderStyle.FixedSingle;
      this.lblStatus.Dock = DockStyle.Fill;
      this.lblStatus.ForeColor = Color.Gray;
      this.lblStatus.Location = new Point(2, 30);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Padding = new Padding(20, 20, 0, 0);
      this.lblStatus.Size = new Size(534, 336);
      this.lblStatus.TabIndex = 2;
      this.lblStatus.Text = "label1";
      this.pnlDownloadedDataHeader.Controls.Add((Control) this.lblDownloadedDataLine);
      this.pnlDownloadedDataHeader.Controls.Add((Control) this.lblDownloadedDataHeader);
      this.pnlDownloadedDataHeader.Dock = DockStyle.Top;
      this.pnlDownloadedDataHeader.Location = new Point(2, 2);
      this.pnlDownloadedDataHeader.Name = "pnlDownloadedDataHeader";
      this.pnlDownloadedDataHeader.Padding = new Padding(7, 0, 15, 0);
      this.pnlDownloadedDataHeader.Size = new Size(534, 28);
      this.pnlDownloadedDataHeader.TabIndex = 19;
      this.lblDownloadedDataLine.BackColor = Color.Transparent;
      this.lblDownloadedDataLine.Dock = DockStyle.Fill;
      this.lblDownloadedDataLine.ForeColor = Color.Blue;
      this.lblDownloadedDataLine.Image = (Image) Resources.GroupLine0;
      this.lblDownloadedDataLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblDownloadedDataLine.Location = new Point(139, 0);
      this.lblDownloadedDataLine.Name = "lblDownloadedDataLine";
      this.lblDownloadedDataLine.Size = new Size(380, 28);
      this.lblDownloadedDataLine.TabIndex = 15;
      this.lblDownloadedDataLine.Tag = (object) "";
      this.lblDownloadedDataLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblDownloadedDataHeader.BackColor = Color.Transparent;
      this.lblDownloadedDataHeader.Dock = DockStyle.Left;
      this.lblDownloadedDataHeader.ForeColor = Color.Blue;
      this.lblDownloadedDataHeader.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblDownloadedDataHeader.Location = new Point(7, 0);
      this.lblDownloadedDataHeader.Name = "lblDownloadedDataHeader";
      this.lblDownloadedDataHeader.Size = new Size(132, 28);
      this.lblDownloadedDataHeader.TabIndex = 12;
      this.lblDownloadedDataHeader.Tag = (object) "";
      this.lblDownloadedDataHeader.Text = "Downloaded Data";
      this.lblDownloadedDataHeader.TextAlign = ContentAlignment.MiddleLeft;
      this.picLoading.BackColor = Color.White;
      this.picLoading.Dock = DockStyle.Fill;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(2, 2);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(534, 364);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 18;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.pnlDiskSpaceAndControl.Controls.Add((Control) this.pnlControl);
      this.pnlDiskSpaceAndControl.Controls.Add((Control) this.pnlDiskSpace);
      this.pnlDiskSpaceAndControl.Dock = DockStyle.Bottom;
      this.pnlDiskSpaceAndControl.Location = new Point(0, 368);
      this.pnlDiskSpaceAndControl.Name = "pnlDiskSpaceAndControl";
      this.pnlDiskSpaceAndControl.Size = new Size(538, 87);
      this.pnlDiskSpaceAndControl.TabIndex = 24;
      this.pnlControl.Controls.Add((Control) this.lblInUseBooks);
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Fill;
      this.pnlControl.Location = new Point(0, 56);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(7, 4, 15, 4);
      this.pnlControl.Size = new Size(538, 31);
      this.pnlControl.TabIndex = 22;
      this.lblInUseBooks.BackColor = Color.Transparent;
      this.lblInUseBooks.Dock = DockStyle.Left;
      this.lblInUseBooks.ForeColor = Color.Black;
      this.lblInUseBooks.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblInUseBooks.Location = new Point(7, 4);
      this.lblInUseBooks.Name = "lblInUseBooks";
      this.lblInUseBooks.Size = new Size(257, 23);
      this.lblInUseBooks.TabIndex = 13;
      this.lblInUseBooks.Tag = (object) "";
      this.lblInUseBooks.Text = "Books you are viewing can not be deleted.";
      this.lblInUseBooks.TextAlign = ContentAlignment.MiddleLeft;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(373, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 2;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(448, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlDiskSpace.Controls.Add((Control) this.pnlDiskSpace1);
      this.pnlDiskSpace.Controls.Add((Control) this.pnlDiskSpaceHeader);
      this.pnlDiskSpace.Dock = DockStyle.Top;
      this.pnlDiskSpace.Location = new Point(0, 0);
      this.pnlDiskSpace.Name = "pnlDiskSpace";
      this.pnlDiskSpace.Size = new Size(538, 56);
      this.pnlDiskSpace.TabIndex = 23;
      this.pnlDiskSpace1.Controls.Add((Control) this.pnlDiskSpaceValue);
      this.pnlDiskSpace1.Controls.Add((Control) this.pnlSplitter1);
      this.pnlDiskSpace1.Controls.Add((Control) this.chkDiskSpace);
      this.pnlDiskSpace1.Dock = DockStyle.Top;
      this.pnlDiskSpace1.Location = new Point(0, 28);
      this.pnlDiskSpace1.Name = "pnlDiskSpace1";
      this.pnlDiskSpace1.Padding = new Padding(30, 0, 0, 0);
      this.pnlDiskSpace1.Size = new Size(538, 22);
      this.pnlDiskSpace1.TabIndex = 21;
      this.pnlDiskSpaceValue.BorderStyle = BorderStyle.FixedSingle;
      this.pnlDiskSpaceValue.Controls.Add((Control) this.txtDiskSpaceValue);
      this.pnlDiskSpaceValue.Controls.Add((Control) this.cmbDiskSpaceValue);
      this.pnlDiskSpaceValue.Dock = DockStyle.Left;
      this.pnlDiskSpaceValue.Location = new Point(236, 0);
      this.pnlDiskSpaceValue.Name = "pnlDiskSpaceValue";
      this.pnlDiskSpaceValue.Size = new Size(75, 22);
      this.pnlDiskSpaceValue.TabIndex = 19;
      this.txtDiskSpaceValue.AllowSpace = false;
      this.txtDiskSpaceValue.BorderStyle = BorderStyle.None;
      this.txtDiskSpaceValue.Location = new Point(1, 4);
      this.txtDiskSpaceValue.MaxLength = 4;
      this.txtDiskSpaceValue.Name = "txtDiskSpaceValue";
      this.txtDiskSpaceValue.Size = new Size(34, 13);
      this.txtDiskSpaceValue.TabIndex = 15;
      this.txtDiskSpaceValue.Text = "1.0";
      this.txtDiskSpaceValue.TextChanged += new EventHandler(this.txtDiskSpaceValue_TextChanged);
      this.cmbDiskSpaceValue.BackColor = SystemColors.Window;
      this.cmbDiskSpaceValue.Dock = DockStyle.Right;
      this.cmbDiskSpaceValue.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbDiskSpaceValue.FlatStyle = FlatStyle.Flat;
      this.cmbDiskSpaceValue.FormattingEnabled = true;
      this.cmbDiskSpaceValue.Items.AddRange(new object[2]
      {
        (object) "GB",
        (object) "MB"
      });
      this.cmbDiskSpaceValue.Location = new Point(34, 0);
      this.cmbDiskSpaceValue.Name = "cmbDiskSpaceValue";
      this.cmbDiskSpaceValue.Size = new Size(39, 21);
      this.cmbDiskSpaceValue.TabIndex = 14;
      this.cmbDiskSpaceValue.SelectedIndexChanged += new EventHandler(this.cmbDiskSpaceValue_SelectedIndexChanged);
      this.pnlSplitter1.Dock = DockStyle.Left;
      this.pnlSplitter1.Location = new Point(214, 0);
      this.pnlSplitter1.Name = "pnlSplitter1";
      this.pnlSplitter1.Size = new Size(22, 22);
      this.pnlSplitter1.TabIndex = 20;
      this.chkDiskSpace.AutoSize = true;
      this.chkDiskSpace.Checked = true;
      this.chkDiskSpace.CheckState = CheckState.Checked;
      this.chkDiskSpace.Dock = DockStyle.Left;
      this.chkDiskSpace.Location = new Point(30, 0);
      this.chkDiskSpace.Name = "chkDiskSpace";
      this.chkDiskSpace.Size = new Size(184, 22);
      this.chkDiskSpace.TabIndex = 18;
      this.chkDiskSpace.Text = "Allocate space for data download";
      this.chkDiskSpace.UseVisualStyleBackColor = true;
      this.chkDiskSpace.CheckedChanged += new EventHandler(this.chkDiskSpace_CheckedChanged);
      this.pnlDiskSpaceHeader.Controls.Add((Control) this.lblDiskSpaceHeaderLine);
      this.pnlDiskSpaceHeader.Controls.Add((Control) this.lblDiskSpaceHeader);
      this.pnlDiskSpaceHeader.Dock = DockStyle.Top;
      this.pnlDiskSpaceHeader.Location = new Point(0, 0);
      this.pnlDiskSpaceHeader.Name = "pnlDiskSpaceHeader";
      this.pnlDiskSpaceHeader.Padding = new Padding(7, 0, 15, 0);
      this.pnlDiskSpaceHeader.Size = new Size(538, 28);
      this.pnlDiskSpaceHeader.TabIndex = 20;
      this.lblDiskSpaceHeaderLine.BackColor = Color.Transparent;
      this.lblDiskSpaceHeaderLine.Dock = DockStyle.Fill;
      this.lblDiskSpaceHeaderLine.ForeColor = Color.Blue;
      this.lblDiskSpaceHeaderLine.Image = (Image) Resources.GroupLine0;
      this.lblDiskSpaceHeaderLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblDiskSpaceHeaderLine.Location = new Point(141, 0);
      this.lblDiskSpaceHeaderLine.Name = "lblDiskSpaceHeaderLine";
      this.lblDiskSpaceHeaderLine.Size = new Size(382, 28);
      this.lblDiskSpaceHeaderLine.TabIndex = 15;
      this.lblDiskSpaceHeaderLine.Tag = (object) "";
      this.lblDiskSpaceHeaderLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblDiskSpaceHeader.BackColor = Color.Transparent;
      this.lblDiskSpaceHeader.Dock = DockStyle.Left;
      this.lblDiskSpaceHeader.ForeColor = Color.Blue;
      this.lblDiskSpaceHeader.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblDiskSpaceHeader.Location = new Point(7, 0);
      this.lblDiskSpaceHeader.Name = "lblDiskSpaceHeader";
      this.lblDiskSpaceHeader.Size = new Size(134, 28);
      this.lblDiskSpaceHeader.TabIndex = 12;
      this.lblDiskSpaceHeader.Tag = (object) "";
      this.lblDiskSpaceHeader.Text = "Disk Space";
      this.lblDiskSpaceHeader.TextAlign = ContentAlignment.MiddleLeft;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.columnHeader1.Text = "FolderName";
      this.columnHeader1.Width = 257;
      this.columnHeader2.Text = "Size";
      this.columnHeader2.Width = 120;
      this.columnHeader3.Text = "Modified Date";
      this.columnHeader3.Width = 152;
      this.columnHeader4.Text = "FolderName";
      this.columnHeader4.Width = 257;
      this.columnHeader5.Text = "Size";
      this.columnHeader5.Width = 120;
      this.columnHeader6.Text = "Modified Date";
      this.columnHeader6.Width = 152;
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.tsbClearSelection,
        (ToolStripItem) this.tsbSelectAll,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.tsBtnDeleteSelection
      });
      this.toolStrip1.Location = new Point(2, 2);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new Size(540, 25);
      this.toolStrip1.TabIndex = 22;
      this.tsbClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbClearSelection.Image = (Image) Resources.SelectionList_clear_selection;
      this.tsbClearSelection.ImageTransparentColor = Color.Magenta;
      this.tsbClearSelection.Name = "tsbClearSelection";
      this.tsbClearSelection.RightToLeft = RightToLeft.No;
      this.tsbClearSelection.Size = new Size(23, 22);
      this.tsbClearSelection.Text = "Clear Selection";
      this.tsbClearSelection.Click += new EventHandler(this.tsbClearSelection_Click);
      this.tsbSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSelectAll.Image = (Image) Resources.SelectionList_select_all;
      this.tsbSelectAll.ImageTransparentColor = Color.Magenta;
      this.tsbSelectAll.Name = "tsbSelectAll";
      this.tsbSelectAll.RightToLeft = RightToLeft.No;
      this.tsbSelectAll.Size = new Size(23, 22);
      this.tsbSelectAll.Text = "Select All";
      this.tsbSelectAll.Click += new EventHandler(this.tsbSelectAll_Click);
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(6, 25);
      this.tsBtnDeleteSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnDeleteSelection.Image = (Image) Resources.SelectionList_DeleteSelection;
      this.tsBtnDeleteSelection.ImageTransparentColor = Color.Magenta;
      this.tsBtnDeleteSelection.Name = "tsBtnDeleteSelection";
      this.tsBtnDeleteSelection.RightToLeft = RightToLeft.No;
      this.tsBtnDeleteSelection.Size = new Size(23, 22);
      this.tsBtnDeleteSelection.Text = "Delete Selection";
      this.tsBtnDeleteSelection.Click += new EventHandler(this.tsBtnDeleteSelection_Click);
      this.ssStatus.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.toolStripStatusLabel1,
        (ToolStripItem) this.tsStatus
      });
      this.ssStatus.Location = new Point(2, 484);
      this.ssStatus.Name = "ssStatus";
      this.ssStatus.Size = new Size(540, 22);
      this.ssStatus.TabIndex = 23;
      this.toolStripStatusLabel1.BackColor = SystemColors.Control;
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new Size(0, 17);
      this.tsStatus.Name = "tsStatus";
      this.tsStatus.Size = new Size(87, 17);
      this.tsStatus.Text = "Remaining space";
      this.bgChangeDataSize.DoWork += new DoWorkEventHandler(this.bgChangeDataSize_DoWork);
      this.bgChangeDataSize.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgChangeDataSize_RunWorkerCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(544, 508);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.toolStrip1);
      this.Controls.Add((Control) this.ssStatus);
      this.MinimizeBox = false;
      this.MinimumSize = new Size(480, 535);
      this.Name = nameof (frmDataSize);
      this.Padding = new Padding(2);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Manage Disk Space";
      this.Load += new EventHandler(this.frmDataSize_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmDataSize_FormClosing);
      this.pnlForm.ResumeLayout(false);
      this.pnlContents.ResumeLayout(false);
      this.pnltlvFolders.ResumeLayout(false);
      this.pnlDownloadedDataHeader.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.pnlDiskSpaceAndControl.ResumeLayout(false);
      this.pnlControl.ResumeLayout(false);
      this.pnlDiskSpace.ResumeLayout(false);
      this.pnlDiskSpace1.ResumeLayout(false);
      this.pnlDiskSpace1.PerformLayout();
      this.pnlDiskSpaceValue.ResumeLayout(false);
      this.pnlDiskSpaceValue.PerformLayout();
      this.pnlDiskSpaceHeader.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public frmDataSize(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.sBookInUseMsg = string.Empty;
      this.stAllocatedSize = 0L;
      this.stFolderSize = 0L;
      this.stFreeSpace = 0L;
      this.currentlyLoadingData = true;
      this.currentlyCalculatingSpace = false;
      this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
      this.UpdateFont();
      this.LoadResources();
      this.tsStatus.Text = string.Empty;
      this.cmbDiskSpaceValue.SelectedItem = (object) "GB";
      this.chkDiskSpace.Checked = false;
      this.pnlDiskSpaceValue.Visible = false;
      this.tlvFolders.HighlightBackColor = Settings.Default.appHighlightBackColor;
      this.tlvFolders.HighlightForeColor = Settings.Default.appHighlightForeColor;
    }

    private void frmDataSize_Load(object sender, EventArgs e)
    {
      this.LoadUserSettings();
      string empty = string.Empty;
      string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
      if (Directory.Exists(path))
      {
        if (Directory.GetDirectories(path).Length > 0)
        {
          if (this.thCalculateSpace.IsAlive)
            this.thCalculateSpace.Abort();
          do
            ;
          while (this.thCalculateSpace.IsAlive);
          this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
          this.thCalculateSpace.Start();
          this.toolStrip1.Enabled = false;
          this.ShowLoading(this.pnltlvFolders);
          this.bgWorker.RunWorkerAsync();
          if (DataSize.IsDataSizeApplied())
          {
            this.chkDiskSpace.Checked = true;
            this.pnlDiskSpaceValue.Visible = true;
            string dataSizeString = DataSize.GetDataSizeString();
            this.txtDiskSpaceValue.Text = DataSize.ExtractNumbers(dataSizeString, true);
            this.cmbDiskSpaceValue.SelectedItem = (object) DataSize.ExtractAlphabets(dataSizeString, false).ToUpper().Trim();
          }
          else
          {
            this.chkDiskSpace.Checked = false;
            this.pnlDiskSpaceValue.Visible = false;
          }
        }
        else
        {
          this.lblStatus.Text = this.GetResource("No books exist", "NO_BOOKS_EXIST", ResourceType.STATUS_MESSAGE);
          this.lblStatus.BringToFront();
          this.tlvFolders.Enabled = false;
          this.tsBtnDeleteSelection.Enabled = false;
        }
      }
      else
      {
        this.lblStatus.Text = this.GetResource("Data path does not exist", "NO_PATH", ResourceType.STATUS_MESSAGE);
        this.lblStatus.BringToFront();
        this.tlvFolders.Enabled = false;
        this.tsBtnDeleteSelection.Enabled = false;
      }
    }

    private void frmDataSize_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.SaveUserSettings();
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      if (this.Owner.GetType() != typeof (frmViewer))
        return;
      this.frmParent.HideDimmer();
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.currentlyLoadingData = true;
      string empty = string.Empty;
      string path1 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
      try
      {
        foreach (string directory in Directory.GetDirectories(path1))
        {
          try
          {
            TreeListViewItem treeListViewItem1 = new TreeListViewItem();
            treeListViewItem1.Tag = (object) directory;
            DirectoryInfo directoryInfo1 = new DirectoryInfo(directory);
            treeListViewItem1.Text = directoryInfo1.Name;
            treeListViewItem1.Expand();
            treeListViewItem1.ImageIndex = 2;
            string[] directories = Directory.GetDirectories(directory);
            ArrayList arrayList = this.frmParent.ListOfInUseBooks();
            foreach (string path2 in directories)
            {
              TreeListViewItem treeListViewItem2 = new TreeListViewItem();
              treeListViewItem2.Tag = (object) path2;
              DirectoryInfo directoryInfo2 = new DirectoryInfo(path2);
              treeListViewItem2.Text = directoryInfo2.Name;
              treeListViewItem2.ImageIndex = 3;
              treeListViewItem1.Items.Add(treeListViewItem2);
              ListViewItem.ListViewSubItem listViewSubItem;
              if (arrayList.Contains((object) directoryInfo2.Name))
              {
                listViewSubItem = treeListViewItem2.SubItems.Add(this.sBookInUseMsg);
                treeListViewItem2.Checked = false;
              }
              else
                listViewSubItem = treeListViewItem2.SubItems.Add(string.Empty);
              listViewSubItem.Name = "Status";
              DirectoryInfo dir = new DirectoryInfo(path2);
              string text1 = DataSize.FormattedSize(DataSize.GetDirSize(dir));
              treeListViewItem2.SubItems.Add(text1).Name = "Size";
              string text2 = dir.LastWriteTime.ToString();
              treeListViewItem2.SubItems.Add(text2).Name = "Date";
            }
            if (treeListViewItem1.ChildrenCount > 0)
              this.TreeListAddItem(treeListViewItem1);
          }
          catch
          {
          }
        }
      }
      catch (Exception ex)
      {
        this.SetMessage(ex.Message);
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.HideLoading(this.pnltlvFolders);
      this.toolStrip1.Enabled = true;
      this.currentlyLoadingData = false;
      if (this.tlvFolders.Items.Count != 0)
        return;
      this.lblStatus.Text = this.GetResource("No books exist", "NO_BOOKS_EXIST", ResourceType.STATUS_MESSAGE);
      this.lblStatus.BringToFront();
      this.tlvFolders.Enabled = false;
      this.tsBtnDeleteSelection.Enabled = false;
    }

    private void SetMessage(string msg)
    {
      if (this.lblStatus.InvokeRequired)
      {
        this.lblStatus.Invoke((Delegate) new frmDataSize.SetMessageDelegate(this.SetMessage), (object) msg);
      }
      else
      {
        this.lblStatus.Text = msg;
        this.lblStatus.BringToFront();
      }
    }

    private void TreeListAddItem(TreeListViewItem item)
    {
      if (this.tlvFolders.InvokeRequired)
        this.tlvFolders.Invoke((Delegate) new frmDataSize.TreeListAddItemDelegate(this.TreeListAddItem), (object) item);
      else
        this.tlvFolders.Items.Add(item);
    }

    private void tlvFolders_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
    {
      if (e.Item.ImageIndex != 1)
        return;
      e.Item.ImageIndex = 2;
    }

    private void tlvFolders_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
    {
      if (e.Item.ImageIndex != 2)
        return;
      e.Item.ImageIndex = 1;
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }

    private void LoadUserSettings()
    {
      if (Settings.Default.frmDataSizeLocation == new Point(0, 0))
        this.StartPosition = FormStartPosition.CenterScreen;
      else
        this.Location = Settings.Default.frmDataSizeLocation;
      this.Size = Settings.Default.frmDataSizeSize;
      if (Settings.Default.frmDataSizeState == FormWindowState.Minimized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = Settings.Default.frmDataSizeState;
    }

    private void SaveUserSettings()
    {
      Settings.Default.frmDataSizeLocation = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Location : this.Location;
      Settings.Default.frmDataSizeSize = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Size : this.Size;
      Settings.Default.frmDataSizeState = this.WindowState;
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Data Cleanup", "DATA_SIZE", ResourceType.TITLE);
      this.tsbSelectAll.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.TOOLSTRIP);
      this.tsbClearSelection.Text = this.GetResource("Unselect All", "UNSELECT_ALL", ResourceType.TOOLSTRIP);
      this.tsBtnDeleteSelection.Text = this.GetResource("Delete Selected", "DELETE_SELECTED", ResourceType.TOOLSTRIP);
      this.lblDiskSpaceHeader.Text = this.GetResource("Disk Space", "DISK_SPACE", ResourceType.LABEL);
      this.chkDiskSpace.Text = this.GetResource("Allocate space for data download", "ALLOCATE_SPACE", ResourceType.CHECK_BOX);
      this.lblDownloadedDataHeader.Text = this.GetResource("Downloaded Data", "DOWNLOADED_DATA", ResourceType.LABEL);
      this.sBookInUseMsg = this.GetResource("Book in use", "BOOK_IN_USE", ResourceType.LABEL) == null ? "Book in use" : this.GetResource("Book in use", "BOOK_IN_USE", ResourceType.LABEL);
      this.lblInUseBooks.Text = this.GetResource("Books you are viewing can not be deleted.", "CANNOT_DELETE_BOOK", ResourceType.LABEL) == null ? "Books you are viewing can not be deleted." : this.GetResource("Books you are viewing can not be deleted.", "CANNOT_DELETE_BOOK", ResourceType.LABEL);
      this.tlvFolders.Columns[0].Text = this.GetResource("Folder Name", "FOLDER_NAME", ResourceType.TREE_VIEW);
      this.tlvFolders.Columns[1].Text = this.GetResource("Status", "STATUS", ResourceType.TREE_VIEW) == null ? "Status" : this.GetResource("Status", "STATUS", ResourceType.TREE_VIEW);
      this.tlvFolders.Columns[2].Text = this.GetResource("Size", "SIZE", ResourceType.TREE_VIEW);
      this.tlvFolders.Columns[3].Text = this.GetResource("Modified Date", "MODIFIED_DATE", ResourceType.TREE_VIEW);
      this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='DATA_SIZE']";
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
          case ResourceType.TREE_VIEW:
            str += "/Resources[@Name='TREE_VIEW']";
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

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (parentPanel.InvokeRequired)
        {
          parentPanel.Invoke((Delegate) new frmDataSize.ShowLoadingDelegate(this.ShowLoading), (object) parentPanel);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Visible = false;
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
        if (parentPanel.InvokeRequired)
        {
          parentPanel.Invoke((Delegate) new frmDataSize.HideLoadingDelegate(this.HideLoading), (object) parentPanel);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Visible = true;
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

    public void UpdateStatus(string status)
    {
      if (this.ssStatus.InvokeRequired)
        this.ssStatus.Invoke((Delegate) new frmDataSize.UpdateStatusDelegate(this.UpdateStatus), (object) status);
      else
        this.tsStatus.Text = status;
    }

    public string GetStatusText()
    {
      if (this.ssStatus.InvokeRequired)
        return (string) this.ssStatus.Invoke((Delegate) new frmDataSize.GetStatusTextDelegate(this.GetStatusText));
      return this.tsStatus.Text;
    }

    private string GetCmbDiskSpace()
    {
      if (this.cmbDiskSpaceValue.InvokeRequired)
        return (string) this.cmbDiskSpaceValue.Invoke((Delegate) new frmDataSize.GetCmbDiskSpaceDelegate(this.GetCmbDiskSpace));
      if (this.cmbDiskSpaceValue.SelectedItem != null)
        return this.cmbDiskSpaceValue.SelectedItem.ToString();
      return string.Empty;
    }

    private string GetTxtDiskSpace()
    {
      if (this.cmbDiskSpaceValue.InvokeRequired)
        return (string) this.txtDiskSpaceValue.Invoke((Delegate) new frmDataSize.GetTxtDiskSpaceDelegate(this.GetTxtDiskSpace));
      return this.txtDiskSpaceValue.Text.Trim();
    }

    private void tsbSelectAll_Click(object sender, EventArgs e)
    {
      foreach (TreeListViewItem treeListViewItem in (CollectionBase) this.tlvFolders.Items)
      {
        treeListViewItem.Checked = false;
        treeListViewItem.Checked = true;
      }
    }

    private void tsbClearSelection_Click(object sender, EventArgs e)
    {
      foreach (TreeListViewItem treeListViewItem in (CollectionBase) this.tlvFolders.Items)
        treeListViewItem.Checked = false;
    }

    private void tsBtnDeleteSelection_Click(object sender, EventArgs e)
    {
      if (this.tlvFolders.CheckedItems.Length <= 0)
        return;
      try
      {
        if (MessageBox.Show(this.GetResource("Are you sure you want to delete the selected data", "DELETE_DATA", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          this.tlvFolders.SuspendLayout();
          TreeListViewItem[] checkedItems1 = this.tlvFolders.CheckedItems;
          for (int index = checkedItems1.Length - 1; index >= 0; --index)
          {
            if (checkedItems1[index].SubItems.Count == 4)
            {
              Directory.Delete(checkedItems1[index].Tag.ToString(), true);
              checkedItems1[index].Remove();
            }
          }
          TreeListViewItem[] checkedItems2 = this.tlvFolders.CheckedItems;
          for (int index = checkedItems2.Length - 1; index >= 0; --index)
          {
            if (checkedItems2[index].SubItems.Count == 1)
            {
              if (checkedItems2[index].ChildrenCount > 0)
                checkedItems2[index].Checked = false;
              else
                checkedItems2[index].Remove();
            }
          }
          this.tlvFolders.ResumeLayout();
        }
        if (this.tlvFolders.Items.Count != 0)
          return;
        this.lblStatus.Text = this.GetResource("No books exist", "NO_BOOKS_EXIST", ResourceType.STATUS_MESSAGE);
        this.lblStatus.BringToFront();
        this.tlvFolders.Enabled = false;
        this.tsBtnDeleteSelection.Enabled = false;
      }
      catch (Exception ex)
      {
        int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(E-VDS-EM001) Failed to delete specified object", "(E-VDS-EM001)_FAILED", ResourceType.POPUP_MESSAGE) + "\r\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      string size = string.Empty;
      if (this.chkDiskSpace.Checked)
      {
        try
        {
          size = this.txtDiskSpaceValue.Text + this.cmbDiskSpaceValue.SelectedItem.ToString();
        }
        catch
        {
          size = string.Empty;
        }
      }
      long dataSizeLong = DataSize.GetDataSizeLong();
      long num1 = DataSize.FormattedSize(size);
      string empty = string.Empty;
      if (this.GetStatusText().Contains(this.GetResource("Overflow", "OVERFLOW", ResourceType.LABEL)))
      {
        int num2 = (int) MessageBox.Show((IWin32Window) this, this.GetResource("Allocated data size should be greater than existing data space", "ALLOCATE_MORE", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else if (dataSizeLong != num1)
        this.bgChangeDataSize.RunWorkerAsync((object) new object[1]
        {
          (object) num1
        });
      else
        this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void chkDiskSpace_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkDiskSpace.Checked)
      {
        string empty = string.Empty;
        if (!Directory.Exists(Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"]))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("Data path does not exist", "NO_PATH", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          this.chkDiskSpace.Checked = false;
        }
        else
          this.pnlDiskSpaceValue.Visible = true;
      }
      else
        this.pnlDiskSpaceValue.Visible = false;
      if (this.thCalculateSpace.IsAlive)
        this.thCalculateSpace.Abort();
      do
        ;
      while (this.thCalculateSpace.IsAlive);
      this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
      this.thCalculateSpace.Start();
    }

    private void bgChangeDataSize_DoWork(object sender, DoWorkEventArgs e)
    {
      long size1 = (long) ((object[]) e.Argument)[0];
      if (size1 > 10485760L || size1 == 0L)
      {
        if (size1 == 0L)
        {
          Program.iniGSPcLocal.UpdateItem("SETTINGS", "DATA_SIZE", string.Empty);
          e.Result = (object) new Global.OkMsg(true, string.Empty);
        }
        else if (Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] != null)
        {
          this.ShowLoading(this.pnlDiskSpaceAndControl);
          string text = this.tsStatus.Text;
          this.UpdateStatus(this.GetResource("Processing disk space...", "PROCESSING_DISK_SPACE", ResourceType.STATUS_MESSAGE));
          string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
          long size2 = DataSize.GetFreeSpace(path.Substring(0, 1)) + DataSize.GetDirSize(new DirectoryInfo(path));
          if (size1 > size2)
          {
            string str = DataSize.FormattedSize(size2);
            e.Result = (object) new Global.OkMsg(false, this.GetResource("Allocated data size should be less than available space", "ALLOCATE_LESS", ResourceType.POPUP_MESSAGE) + " [" + str + "]");
          }
          else
          {
            string str = DataSize.FormattedSize(size1);
            Program.iniGSPcLocal.UpdateItem("SETTINGS", "DATA_SIZE", str);
            e.Result = (object) new Global.OkMsg(true, string.Empty);
          }
          this.UpdateStatus(text);
        }
        else
          e.Result = (object) new Global.OkMsg(false, this.GetResource("Data path does not exist", "NO_PATH", ResourceType.POPUP_MESSAGE));
      }
      else
        e.Result = (object) new Global.OkMsg(false, this.GetResource("Allocated data size should be greater than 10MB", "ALLOCATE_MORE_SPACE", ResourceType.POPUP_MESSAGE));
    }

    private void bgChangeDataSize_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      Global.OkMsg okMsg;
      try
      {
        okMsg = (Global.OkMsg) e.Result;
      }
      catch
      {
        okMsg = new Global.OkMsg(false, "Err");
      }
      this.HideLoading(this.pnlDiskSpaceAndControl);
      MessageBoxIcon icon = !okMsg.ok ? MessageBoxIcon.Hand : MessageBoxIcon.Asterisk;
      if (okMsg.msg.Trim() != string.Empty)
      {
        int num = (int) MessageHandler.ShowMessage((IWin32Window) this, okMsg.msg, this.Text, MessageBoxButtons.OK, icon);
      }
      if (!okMsg.ok)
        return;
      DataSize.ReInitialize();
      this.frmParent.DisposeNotification();
      if (this.chkDiskSpace.Checked)
        this.frmParent.RunDataSizeChecking();
      this.Close();
    }

    private void tlvFolders_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      try
      {
        if (e.Item.SubItems.Count == 4)
        {
          if (e.Item.SubItems["Status"].Text == this.sBookInUseMsg)
          {
            e.Item.Checked = false;
            return;
          }
        }
      }
      catch
      {
      }
      if (this.currentlyLoadingData || this.currentlyCalculatingSpace)
        return;
      this.SetRemainingSpaceStatus();
    }

    private void CalculateRemainingSpaceStatus()
    {
      this.stAllocatedSize = 0L;
      this.stFolderSize = 0L;
      this.stFreeSpace = 0L;
      string empty = string.Empty;
      this.currentlyCalculatingSpace = true;
      this.UpdateStatus(this.GetResource("Processing disk space...", "PROCESSING_DISK_SPACE", ResourceType.STATUS_MESSAGE));
      if (this.chkDiskSpace.Checked)
      {
        try
        {
          this.stAllocatedSize = DataSize.FormattedSize(this.GetTxtDiskSpace() + this.GetCmbDiskSpace());
        }
        catch
        {
        }
        try
        {
          string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
          if (path != null)
          {
            if (Directory.Exists(path))
              this.stFolderSize = DataSize.GetDirSize(new DirectoryInfo(path));
          }
        }
        catch
        {
        }
        try
        {
          this.stFreeSpace = this.stAllocatedSize - this.stFolderSize;
        }
        catch
        {
        }
      }
      else
      {
        try
        {
          this.stFreeSpace = DataSize.GetFreeSpace(Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"].Substring(0, 1));
        }
        catch
        {
        }
        try
        {
          string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
          if (path != null)
          {
            if (Directory.Exists(path))
              this.stFolderSize = DataSize.GetDirSize(new DirectoryInfo(path));
          }
        }
        catch
        {
        }
        try
        {
          this.stAllocatedSize = this.stFreeSpace + this.stFolderSize;
        }
        catch
        {
        }
      }
      while (this.currentlyLoadingData)
        Thread.Sleep(1);
      this.SetRemainingSpaceStatus();
      this.currentlyCalculatingSpace = false;
    }

    private void SetRemainingSpaceStatus()
    {
      long num = 0;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      foreach (TreeListViewItem treeListViewItem1 in (CollectionBase) this.tlvFolders.Items)
      {
        if (treeListViewItem1.Checked)
        {
          foreach (TreeListViewItem treeListViewItem2 in (CollectionBase) treeListViewItem1.Items)
          {
            if (treeListViewItem2.Checked)
            {
              ListViewItem.ListViewSubItem subItem = treeListViewItem2.SubItems["Size"];
              if (subItem != null)
              {
                string text = subItem.Text;
                num += DataSize.FormattedSize(text);
              }
            }
          }
        }
      }
      string str1 = DataSize.FormattedSize(num + this.stFreeSpace);
      string str2 = DataSize.FormattedSize(this.stAllocatedSize);
      if (this.stFreeSpace > 0L)
        this.UpdateStatus(str1 + "/" + str2 + " " + this.GetResource("Remaining", "REMAINING", ResourceType.STATUS_MESSAGE));
      else
        this.UpdateStatus(DataSize.FormattedSize(this.stFreeSpace * -1L) + " " + this.GetResource("Overflow", "OVERFLOW", ResourceType.STATUS_MESSAGE));
    }

    private void txtDiskSpaceValue_TextChanged(object sender, EventArgs e)
    {
      if (this.thCalculateSpace.IsAlive)
        this.thCalculateSpace.Abort();
      do
        ;
      while (this.thCalculateSpace.IsAlive);
      this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
      this.thCalculateSpace.Start();
    }

    private void cmbDiskSpaceValue_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.thCalculateSpace.IsAlive)
        this.thCalculateSpace.Abort();
      do
        ;
      while (this.thCalculateSpace.IsAlive);
      this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
      this.thCalculateSpace.Start();
    }

    private delegate void SetMessageDelegate(string msg);

    private delegate void TreeListAddItemDelegate(TreeListViewItem item);

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void UpdateStatusDelegate(string status);

    private delegate string GetStatusTextDelegate();

    private delegate string GetCmbDiskSpaceDelegate();

    private delegate string GetTxtDiskSpaceDelegate();
  }
}

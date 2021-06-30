// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmMemoListAll
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmMemoListAll : Form
  {
    private string strMemoPriority = "LOCAL";
    private string strDateFormat = string.Empty;
    private IContainer components;
    private Panel pnlControl;
    private Button btnOK;
    private Button btnCancel;
    private Panel pnlForm;
    private Panel pnlBottom;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private Panel pnlTop;
    private Panel pnlGrid;
    private DataGridView dgMemoList;
    private Panel pnlSplitter2;
    private Panel pnlSplitter1;
    private Panel pnlMemos;
    private Panel pnlTxtMemo;
    private Panel pnlTxtMemoContents;
    private RichTextBox rtbTxtMemo;
    private Panel pnlTxtMemoTop;
    private Label lblTxtMemoDate;
    private Label lblTxtMemoTitle;
    private Panel pnlRefMemo;
    private Panel pnlRefMemoTop;
    private Label lblRefMemoDate;
    private Label lblRefMemoTitle;
    private Panel pnlPrgMemo;
    private Panel pnlPrgMemoContents;
    private Button btnPrgMemoOpen;
    private Label lblPrgMemoExePath;
    private TextBox txtPrgMemoExePath;
    private Panel pnlPrgMemoTop;
    private Label lblPrgMemoDate;
    private Label lblPrgMemoTitle;
    private Panel pnlHypMemo;
    private Panel pnlHypMemoContents;
    private Panel pnlHypMemoTop;
    private Label lblHypMemoDate;
    private Label lblHypMemoTitle;
    private Button btnPrgMemoExePathBrowse;
    private Label lblPrgMemoCmdLine;
    private TextBox txtPrgMemoCmdLine;
    private Panel pnlRtbTxtMemo;
    private Panel pnlError;
    private Label lblError;
    private OpenFileDialog ofd;
    private Panel pnlOptions;
    private Label lblAllMemo;
    private DataGridViewTextBoxColumn Column1;
    private DataGridViewTextBoxColumn Column2;
    private DataGridViewTextBoxColumn Column3;
    private DataGridViewTextBoxColumn Column4;
    private ContextMenuStrip cmsAllMemo;
    private ToolStripMenuItem goToPageToolStripMenuItem;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private Panel pnlRefMemoContents;
    private Button btnRefMemoOpen;
    private Label lblRefMemoNote;
    private Label lblRefMemoOtherRef;
    private Label lblRefMemoBookId;
    private TextBox txtRefMemoOtherRef;
    private Label lblRefMemoServerKey;
    private TextBox txtRefMemoBookId;
    private TextBox txtRefMemoServerKey;
    public Button btnHypMemoOpen;
    public Label lblHypMemoNote;
    public Label lblHypMemoUrl;
    public TextBox txtHypMemoUrl;
    private frmMemoList frmParent;
    private bool bMemoChanged;
    public int intMemoType;
    public bool bSaveMemoOnBookLevel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.pnlControl = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.pnlForm = new Panel();
      this.pnlTop = new Panel();
      this.pnlGrid = new Panel();
      this.dgMemoList = new DataGridView();
      this.Column1 = new DataGridViewTextBoxColumn();
      this.Column2 = new DataGridViewTextBoxColumn();
      this.Column3 = new DataGridViewTextBoxColumn();
      this.Column4 = new DataGridViewTextBoxColumn();
      this.cmsAllMemo = new ContextMenuStrip(this.components);
      this.goToPageToolStripMenuItem = new ToolStripMenuItem();
      this.pnlBottom = new Panel();
      this.pnlMemos = new Panel();
      this.pnlTxtMemo = new Panel();
      this.pnlTxtMemoContents = new Panel();
      this.pnlRtbTxtMemo = new Panel();
      this.rtbTxtMemo = new RichTextBox();
      this.pnlTxtMemoTop = new Panel();
      this.lblTxtMemoDate = new Label();
      this.lblTxtMemoTitle = new Label();
      this.pnlRefMemo = new Panel();
      this.pnlRefMemoContents = new Panel();
      this.btnRefMemoOpen = new Button();
      this.lblRefMemoNote = new Label();
      this.lblRefMemoOtherRef = new Label();
      this.lblRefMemoBookId = new Label();
      this.txtRefMemoOtherRef = new TextBox();
      this.lblRefMemoServerKey = new Label();
      this.txtRefMemoBookId = new TextBox();
      this.txtRefMemoServerKey = new TextBox();
      this.pnlRefMemoTop = new Panel();
      this.lblRefMemoDate = new Label();
      this.lblRefMemoTitle = new Label();
      this.pnlHypMemo = new Panel();
      this.pnlHypMemoContents = new Panel();
      this.btnHypMemoOpen = new Button();
      this.lblHypMemoNote = new Label();
      this.lblHypMemoUrl = new Label();
      this.txtHypMemoUrl = new TextBox();
      this.pnlHypMemoTop = new Panel();
      this.lblHypMemoDate = new Label();
      this.lblHypMemoTitle = new Label();
      this.pnlPrgMemo = new Panel();
      this.pnlPrgMemoContents = new Panel();
      this.btnPrgMemoExePathBrowse = new Button();
      this.btnPrgMemoOpen = new Button();
      this.lblPrgMemoCmdLine = new Label();
      this.txtPrgMemoCmdLine = new TextBox();
      this.lblPrgMemoExePath = new Label();
      this.txtPrgMemoExePath = new TextBox();
      this.pnlPrgMemoTop = new Panel();
      this.lblPrgMemoDate = new Label();
      this.lblPrgMemoTitle = new Label();
      this.pnlError = new Panel();
      this.lblError = new Label();
      this.pnlSplitter2 = new Panel();
      this.pnlSplitter1 = new Panel();
      this.pnlOptions = new Panel();
      this.lblAllMemo = new Label();
      this.ofd = new OpenFileDialog();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.pnlControl.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.pnlTop.SuspendLayout();
      this.pnlGrid.SuspendLayout();
      ((ISupportInitialize) this.dgMemoList).BeginInit();
      this.cmsAllMemo.SuspendLayout();
      this.pnlBottom.SuspendLayout();
      this.pnlMemos.SuspendLayout();
      this.pnlTxtMemo.SuspendLayout();
      this.pnlTxtMemoContents.SuspendLayout();
      this.pnlRtbTxtMemo.SuspendLayout();
      this.pnlTxtMemoTop.SuspendLayout();
      this.pnlRefMemo.SuspendLayout();
      this.pnlRefMemoContents.SuspendLayout();
      this.pnlRefMemoTop.SuspendLayout();
      this.pnlHypMemo.SuspendLayout();
      this.pnlHypMemoContents.SuspendLayout();
      this.pnlHypMemoTop.SuspendLayout();
      this.pnlPrgMemo.SuspendLayout();
      this.pnlPrgMemoContents.SuspendLayout();
      this.pnlPrgMemoTop.SuspendLayout();
      this.pnlError.SuspendLayout();
      this.pnlOptions.SuspendLayout();
      this.SuspendLayout();
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 317);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 22, 4);
      this.pnlControl.Size = new Size(448, 31);
      this.pnlControl.TabIndex = 18;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(276, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(351, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlTop);
      this.pnlForm.Controls.Add((Control) this.pnlBottom);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Controls.Add((Control) this.pnlOptions);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(450, 350);
      this.pnlForm.TabIndex = 19;
      this.pnlTop.Controls.Add((Control) this.pnlGrid);
      this.pnlTop.Dock = DockStyle.Fill;
      this.pnlTop.Location = new Point(0, 33);
      this.pnlTop.Name = "pnlTop";
      this.pnlTop.Padding = new Padding(10, 10, 10, 0);
      this.pnlTop.Size = new Size(448, (int) sbyte.MaxValue);
      this.pnlTop.TabIndex = 20;
      this.pnlGrid.BorderStyle = BorderStyle.FixedSingle;
      this.pnlGrid.Controls.Add((Control) this.dgMemoList);
      this.pnlGrid.Dock = DockStyle.Fill;
      this.pnlGrid.Location = new Point(10, 10);
      this.pnlGrid.Name = "pnlGrid";
      this.pnlGrid.Size = new Size(428, 117);
      this.pnlGrid.TabIndex = 20;
      this.dgMemoList.AllowUserToAddRows = false;
      this.dgMemoList.AllowUserToDeleteRows = false;
      this.dgMemoList.AllowUserToResizeRows = false;
      this.dgMemoList.BackgroundColor = Color.White;
      this.dgMemoList.BorderStyle = BorderStyle.None;
      this.dgMemoList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgMemoList.Columns.AddRange((DataGridViewColumn) this.Column1, (DataGridViewColumn) this.Column2, (DataGridViewColumn) this.Column3, (DataGridViewColumn) this.Column4);
      this.dgMemoList.ContextMenuStrip = this.cmsAllMemo;
      this.dgMemoList.Dock = DockStyle.Fill;
      this.dgMemoList.EditMode = DataGridViewEditMode.EditProgrammatically;
      this.dgMemoList.Location = new Point(0, 0);
      this.dgMemoList.MultiSelect = false;
      this.dgMemoList.Name = "dgMemoList";
      this.dgMemoList.RowHeadersVisible = false;
      this.dgMemoList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgMemoList.Size = new Size(426, 115);
      this.dgMemoList.TabIndex = 21;
      this.dgMemoList.TabStop = false;
      this.dgMemoList.SelectionChanged += new EventHandler(this.dgMemoList_SelectionChanged);
      this.Column1.HeaderText = "Description";
      this.Column1.Name = "Column1";
      this.Column1.Width = 178;
      this.Column2.HeaderText = "Type";
      this.Column2.Name = "Column2";
      this.Column2.Width = 90;
      this.Column3.HeaderText = "UpdateDate";
      this.Column3.Name = "Column3";
      this.Column3.Width = 140;
      this.Column4.HeaderText = "Scope";
      this.Column4.Name = "Column4";
      this.Column4.Visible = false;
      this.cmsAllMemo.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.goToPageToolStripMenuItem
      });
      this.cmsAllMemo.Name = "cmsAllMemo";
      this.cmsAllMemo.Size = new Size(136, 26);
      this.goToPageToolStripMenuItem.Name = "goToPageToolStripMenuItem";
      this.goToPageToolStripMenuItem.Size = new Size(135, 22);
      this.goToPageToolStripMenuItem.Text = "Go To Page";
      this.goToPageToolStripMenuItem.Click += new EventHandler(this.goToPageToolStripMenuItem_Click);
      this.pnlBottom.Controls.Add((Control) this.pnlMemos);
      this.pnlBottom.Controls.Add((Control) this.pnlSplitter2);
      this.pnlBottom.Controls.Add((Control) this.pnlSplitter1);
      this.pnlBottom.Dock = DockStyle.Bottom;
      this.pnlBottom.Location = new Point(0, 160);
      this.pnlBottom.Name = "pnlBottom";
      this.pnlBottom.Padding = new Padding(10, 0, 10, 0);
      this.pnlBottom.Size = new Size(448, 157);
      this.pnlBottom.TabIndex = 20;
      this.pnlMemos.Controls.Add((Control) this.pnlTxtMemo);
      this.pnlMemos.Controls.Add((Control) this.pnlRefMemo);
      this.pnlMemos.Controls.Add((Control) this.pnlHypMemo);
      this.pnlMemos.Controls.Add((Control) this.pnlPrgMemo);
      this.pnlMemos.Controls.Add((Control) this.pnlError);
      this.pnlMemos.Dock = DockStyle.Fill;
      this.pnlMemos.Location = new Point(10, 8);
      this.pnlMemos.Name = "pnlMemos";
      this.pnlMemos.Size = new Size(428, 149);
      this.pnlMemos.TabIndex = 28;
      this.pnlTxtMemo.BorderStyle = BorderStyle.FixedSingle;
      this.pnlTxtMemo.Controls.Add((Control) this.pnlTxtMemoContents);
      this.pnlTxtMemo.Controls.Add((Control) this.pnlTxtMemoTop);
      this.pnlTxtMemo.Dock = DockStyle.Fill;
      this.pnlTxtMemo.Location = new Point(0, 0);
      this.pnlTxtMemo.Name = "pnlTxtMemo";
      this.pnlTxtMemo.Padding = new Padding(10, 0, 10, 10);
      this.pnlTxtMemo.Size = new Size(428, 149);
      this.pnlTxtMemo.TabIndex = 4;
      this.pnlTxtMemoContents.Controls.Add((Control) this.pnlRtbTxtMemo);
      this.pnlTxtMemoContents.Dock = DockStyle.Fill;
      this.pnlTxtMemoContents.Location = new Point(10, 26);
      this.pnlTxtMemoContents.Name = "pnlTxtMemoContents";
      this.pnlTxtMemoContents.Padding = new Padding(2, 6, 2, 5);
      this.pnlTxtMemoContents.Size = new Size(406, 111);
      this.pnlTxtMemoContents.TabIndex = 3;
      this.pnlRtbTxtMemo.BorderStyle = BorderStyle.FixedSingle;
      this.pnlRtbTxtMemo.Controls.Add((Control) this.rtbTxtMemo);
      this.pnlRtbTxtMemo.Dock = DockStyle.Fill;
      this.pnlRtbTxtMemo.Location = new Point(2, 6);
      this.pnlRtbTxtMemo.Name = "pnlRtbTxtMemo";
      this.pnlRtbTxtMemo.Size = new Size(402, 100);
      this.pnlRtbTxtMemo.TabIndex = 3;
      this.rtbTxtMemo.BackColor = SystemColors.Window;
      this.rtbTxtMemo.BorderStyle = BorderStyle.None;
      this.rtbTxtMemo.Dock = DockStyle.Fill;
      this.rtbTxtMemo.Location = new Point(0, 0);
      this.rtbTxtMemo.Name = "rtbTxtMemo";
      this.rtbTxtMemo.ReadOnly = true;
      this.rtbTxtMemo.Size = new Size(400, 98);
      this.rtbTxtMemo.TabIndex = 2;
      this.rtbTxtMemo.TabStop = false;
      this.rtbTxtMemo.Text = "";
      this.pnlTxtMemoTop.Controls.Add((Control) this.lblTxtMemoDate);
      this.pnlTxtMemoTop.Controls.Add((Control) this.lblTxtMemoTitle);
      this.pnlTxtMemoTop.Dock = DockStyle.Top;
      this.pnlTxtMemoTop.Location = new Point(10, 0);
      this.pnlTxtMemoTop.Name = "pnlTxtMemoTop";
      this.pnlTxtMemoTop.Padding = new Padding(0, 0, 0, 5);
      this.pnlTxtMemoTop.Size = new Size(406, 26);
      this.pnlTxtMemoTop.TabIndex = 2;
      this.lblTxtMemoDate.Dock = DockStyle.Fill;
      this.lblTxtMemoDate.Location = new Point(143, 0);
      this.lblTxtMemoDate.Name = "lblTxtMemoDate";
      this.lblTxtMemoDate.Size = new Size(263, 21);
      this.lblTxtMemoDate.TabIndex = 0;
      this.lblTxtMemoDate.Text = "Updated on: 14/02/2010 21:26";
      this.lblTxtMemoDate.TextAlign = ContentAlignment.MiddleRight;
      this.lblTxtMemoTitle.Dock = DockStyle.Left;
      this.lblTxtMemoTitle.Location = new Point(0, 0);
      this.lblTxtMemoTitle.Name = "lblTxtMemoTitle";
      this.lblTxtMemoTitle.Size = new Size(143, 21);
      this.lblTxtMemoTitle.TabIndex = 0;
      this.lblTxtMemoTitle.Text = "Text Memo";
      this.lblTxtMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlRefMemo.BorderStyle = BorderStyle.FixedSingle;
      this.pnlRefMemo.Controls.Add((Control) this.pnlRefMemoContents);
      this.pnlRefMemo.Controls.Add((Control) this.pnlRefMemoTop);
      this.pnlRefMemo.Dock = DockStyle.Fill;
      this.pnlRefMemo.Location = new Point(0, 0);
      this.pnlRefMemo.Name = "pnlRefMemo";
      this.pnlRefMemo.Padding = new Padding(10, 0, 10, 10);
      this.pnlRefMemo.Size = new Size(428, 149);
      this.pnlRefMemo.TabIndex = 5;
      this.pnlRefMemoContents.Controls.Add((Control) this.btnRefMemoOpen);
      this.pnlRefMemoContents.Controls.Add((Control) this.lblRefMemoNote);
      this.pnlRefMemoContents.Controls.Add((Control) this.lblRefMemoOtherRef);
      this.pnlRefMemoContents.Controls.Add((Control) this.lblRefMemoBookId);
      this.pnlRefMemoContents.Controls.Add((Control) this.txtRefMemoOtherRef);
      this.pnlRefMemoContents.Controls.Add((Control) this.lblRefMemoServerKey);
      this.pnlRefMemoContents.Controls.Add((Control) this.txtRefMemoBookId);
      this.pnlRefMemoContents.Controls.Add((Control) this.txtRefMemoServerKey);
      this.pnlRefMemoContents.Dock = DockStyle.Fill;
      this.pnlRefMemoContents.Location = new Point(10, 21);
      this.pnlRefMemoContents.Name = "pnlRefMemoContents";
      this.pnlRefMemoContents.Size = new Size(406, 116);
      this.pnlRefMemoContents.TabIndex = 4;
      this.btnRefMemoOpen.Location = new Point(329, 70);
      this.btnRefMemoOpen.Name = "btnRefMemoOpen";
      this.btnRefMemoOpen.Size = new Size(75, 23);
      this.btnRefMemoOpen.TabIndex = 2;
      this.btnRefMemoOpen.TabStop = false;
      this.btnRefMemoOpen.Text = "Go";
      this.btnRefMemoOpen.UseVisualStyleBackColor = true;
      this.btnRefMemoOpen.Click += new EventHandler(this.btnRefMemoOpen_Click);
      this.lblRefMemoNote.AutoSize = true;
      this.lblRefMemoNote.Location = new Point(6, 75);
      this.lblRefMemoNote.Name = "lblRefMemoNote";
      this.lblRefMemoNote.Size = new Size(129, 13);
      this.lblRefMemoNote.TabIndex = 3;
      this.lblRefMemoNote.Text = "(space separated values)";
      this.lblRefMemoOtherRef.AutoSize = true;
      this.lblRefMemoOtherRef.Location = new Point(281, 13);
      this.lblRefMemoOtherRef.Name = "lblRefMemoOtherRef";
      this.lblRefMemoOtherRef.Size = new Size(55, 13);
      this.lblRefMemoOtherRef.TabIndex = 1;
      this.lblRefMemoOtherRef.Text = "Other Ref";
      this.lblRefMemoBookId.AutoSize = true;
      this.lblRefMemoBookId.Location = new Point(143, 13);
      this.lblRefMemoBookId.Name = "lblRefMemoBookId";
      this.lblRefMemoBookId.Size = new Size(93, 13);
      this.lblRefMemoBookId.TabIndex = 1;
      this.lblRefMemoBookId.Text = "Book Publishing Id";
      this.txtRefMemoOtherRef.BorderStyle = BorderStyle.FixedSingle;
      this.txtRefMemoOtherRef.Location = new Point(281, 30);
      this.txtRefMemoOtherRef.Name = "txtRefMemoOtherRef";
      this.txtRefMemoOtherRef.Size = new Size(123, 21);
      this.txtRefMemoOtherRef.TabIndex = 0;
      this.txtRefMemoOtherRef.TabStop = false;
      this.lblRefMemoServerKey.AutoSize = true;
      this.lblRefMemoServerKey.Location = new Point(6, 13);
      this.lblRefMemoServerKey.Name = "lblRefMemoServerKey";
      this.lblRefMemoServerKey.Size = new Size(60, 13);
      this.lblRefMemoServerKey.TabIndex = 1;
      this.lblRefMemoServerKey.Text = "Server Key";
      this.txtRefMemoBookId.BorderStyle = BorderStyle.FixedSingle;
      this.txtRefMemoBookId.Location = new Point(143, 30);
      this.txtRefMemoBookId.Name = "txtRefMemoBookId";
      this.txtRefMemoBookId.Size = new Size(123, 21);
      this.txtRefMemoBookId.TabIndex = 0;
      this.txtRefMemoBookId.TabStop = false;
      this.txtRefMemoServerKey.BorderStyle = BorderStyle.FixedSingle;
      this.txtRefMemoServerKey.Location = new Point(6, 30);
      this.txtRefMemoServerKey.Name = "txtRefMemoServerKey";
      this.txtRefMemoServerKey.Size = new Size(123, 21);
      this.txtRefMemoServerKey.TabIndex = 0;
      this.txtRefMemoServerKey.TabStop = false;
      this.pnlRefMemoTop.Controls.Add((Control) this.lblRefMemoDate);
      this.pnlRefMemoTop.Controls.Add((Control) this.lblRefMemoTitle);
      this.pnlRefMemoTop.Dock = DockStyle.Top;
      this.pnlRefMemoTop.Location = new Point(10, 0);
      this.pnlRefMemoTop.Name = "pnlRefMemoTop";
      this.pnlRefMemoTop.Size = new Size(406, 21);
      this.pnlRefMemoTop.TabIndex = 2;
      this.lblRefMemoDate.Dock = DockStyle.Fill;
      this.lblRefMemoDate.Location = new Point(165, 0);
      this.lblRefMemoDate.Name = "lblRefMemoDate";
      this.lblRefMemoDate.Size = new Size(241, 21);
      this.lblRefMemoDate.TabIndex = 0;
      this.lblRefMemoDate.Text = "Updated on: 14/02/2010 21:26";
      this.lblRefMemoDate.TextAlign = ContentAlignment.MiddleRight;
      this.lblRefMemoTitle.Dock = DockStyle.Left;
      this.lblRefMemoTitle.Location = new Point(0, 0);
      this.lblRefMemoTitle.Name = "lblRefMemoTitle";
      this.lblRefMemoTitle.Size = new Size(165, 21);
      this.lblRefMemoTitle.TabIndex = 0;
      this.lblRefMemoTitle.Text = "Reference Memo";
      this.lblRefMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlHypMemo.BorderStyle = BorderStyle.FixedSingle;
      this.pnlHypMemo.Controls.Add((Control) this.pnlHypMemoContents);
      this.pnlHypMemo.Controls.Add((Control) this.pnlHypMemoTop);
      this.pnlHypMemo.Dock = DockStyle.Fill;
      this.pnlHypMemo.Location = new Point(0, 0);
      this.pnlHypMemo.Name = "pnlHypMemo";
      this.pnlHypMemo.Padding = new Padding(10, 0, 10, 10);
      this.pnlHypMemo.Size = new Size(428, 149);
      this.pnlHypMemo.TabIndex = 5;
      this.pnlHypMemoContents.Controls.Add((Control) this.btnHypMemoOpen);
      this.pnlHypMemoContents.Controls.Add((Control) this.lblHypMemoNote);
      this.pnlHypMemoContents.Controls.Add((Control) this.lblHypMemoUrl);
      this.pnlHypMemoContents.Controls.Add((Control) this.txtHypMemoUrl);
      this.pnlHypMemoContents.Dock = DockStyle.Fill;
      this.pnlHypMemoContents.Location = new Point(10, 21);
      this.pnlHypMemoContents.Name = "pnlHypMemoContents";
      this.pnlHypMemoContents.Size = new Size(406, 116);
      this.pnlHypMemoContents.TabIndex = 3;
      this.btnHypMemoOpen.Location = new Point(329, 70);
      this.btnHypMemoOpen.Name = "btnHypMemoOpen";
      this.btnHypMemoOpen.Size = new Size(75, 23);
      this.btnHypMemoOpen.TabIndex = 2;
      this.btnHypMemoOpen.TabStop = false;
      this.btnHypMemoOpen.Text = "Go";
      this.btnHypMemoOpen.UseVisualStyleBackColor = true;
      this.btnHypMemoOpen.Click += new EventHandler(this.btnHypMemoOpen_Click);
      this.lblHypMemoNote.AutoSize = true;
      this.lblHypMemoNote.Location = new Point(76, 45);
      this.lblHypMemoNote.Name = "lblHypMemoNote";
      this.lblHypMemoNote.Size = new Size(328, 13);
      this.lblHypMemoNote.TabIndex = 1;
      this.lblHypMemoNote.Text = "Provide the web page address (URL) in the above field to hyperlink";
      this.lblHypMemoUrl.AutoSize = true;
      this.lblHypMemoUrl.Location = new Point(0, 14);
      this.lblHypMemoUrl.Name = "lblHypMemoUrl";
      this.lblHypMemoUrl.Size = new Size(26, 13);
      this.lblHypMemoUrl.TabIndex = 1;
      this.lblHypMemoUrl.Text = "URL";
      this.txtHypMemoUrl.BorderStyle = BorderStyle.FixedSingle;
      this.txtHypMemoUrl.Location = new Point(31, 11);
      this.txtHypMemoUrl.Name = "txtHypMemoUrl";
      this.txtHypMemoUrl.Size = new Size(373, 21);
      this.txtHypMemoUrl.TabIndex = 0;
      this.txtHypMemoUrl.TabStop = false;
      this.pnlHypMemoTop.Controls.Add((Control) this.lblHypMemoDate);
      this.pnlHypMemoTop.Controls.Add((Control) this.lblHypMemoTitle);
      this.pnlHypMemoTop.Dock = DockStyle.Top;
      this.pnlHypMemoTop.Location = new Point(10, 0);
      this.pnlHypMemoTop.Name = "pnlHypMemoTop";
      this.pnlHypMemoTop.Size = new Size(406, 21);
      this.pnlHypMemoTop.TabIndex = 2;
      this.lblHypMemoDate.Dock = DockStyle.Fill;
      this.lblHypMemoDate.Location = new Point(165, 0);
      this.lblHypMemoDate.Name = "lblHypMemoDate";
      this.lblHypMemoDate.Size = new Size(241, 21);
      this.lblHypMemoDate.TabIndex = 0;
      this.lblHypMemoDate.Text = "Updated on: 14/02/2010 21:26";
      this.lblHypMemoDate.TextAlign = ContentAlignment.MiddleRight;
      this.lblHypMemoTitle.Dock = DockStyle.Left;
      this.lblHypMemoTitle.Location = new Point(0, 0);
      this.lblHypMemoTitle.Name = "lblHypMemoTitle";
      this.lblHypMemoTitle.Size = new Size(165, 21);
      this.lblHypMemoTitle.TabIndex = 0;
      this.lblHypMemoTitle.Text = "Hyperlink Memo";
      this.lblHypMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlPrgMemo.BorderStyle = BorderStyle.FixedSingle;
      this.pnlPrgMemo.Controls.Add((Control) this.pnlPrgMemoContents);
      this.pnlPrgMemo.Controls.Add((Control) this.pnlPrgMemoTop);
      this.pnlPrgMemo.Dock = DockStyle.Fill;
      this.pnlPrgMemo.Location = new Point(0, 0);
      this.pnlPrgMemo.Name = "pnlPrgMemo";
      this.pnlPrgMemo.Padding = new Padding(10, 0, 10, 10);
      this.pnlPrgMemo.Size = new Size(428, 149);
      this.pnlPrgMemo.TabIndex = 5;
      this.pnlPrgMemoContents.Controls.Add((Control) this.btnPrgMemoExePathBrowse);
      this.pnlPrgMemoContents.Controls.Add((Control) this.btnPrgMemoOpen);
      this.pnlPrgMemoContents.Controls.Add((Control) this.lblPrgMemoCmdLine);
      this.pnlPrgMemoContents.Controls.Add((Control) this.txtPrgMemoCmdLine);
      this.pnlPrgMemoContents.Controls.Add((Control) this.lblPrgMemoExePath);
      this.pnlPrgMemoContents.Controls.Add((Control) this.txtPrgMemoExePath);
      this.pnlPrgMemoContents.Dock = DockStyle.Fill;
      this.pnlPrgMemoContents.Location = new Point(10, 21);
      this.pnlPrgMemoContents.Name = "pnlPrgMemoContents";
      this.pnlPrgMemoContents.Size = new Size(406, 116);
      this.pnlPrgMemoContents.TabIndex = 3;
      this.btnPrgMemoExePathBrowse.Location = new Point(329, 10);
      this.btnPrgMemoExePathBrowse.Name = "btnPrgMemoExePathBrowse";
      this.btnPrgMemoExePathBrowse.Size = new Size(75, 23);
      this.btnPrgMemoExePathBrowse.TabIndex = 2;
      this.btnPrgMemoExePathBrowse.TabStop = false;
      this.btnPrgMemoExePathBrowse.Text = "Browse";
      this.btnPrgMemoExePathBrowse.UseVisualStyleBackColor = true;
      this.btnPrgMemoExePathBrowse.Click += new EventHandler(this.btnPrgMemoExePathBrowse_Click);
      this.btnPrgMemoOpen.Location = new Point(329, 70);
      this.btnPrgMemoOpen.Name = "btnPrgMemoOpen";
      this.btnPrgMemoOpen.Size = new Size(75, 23);
      this.btnPrgMemoOpen.TabIndex = 2;
      this.btnPrgMemoOpen.TabStop = false;
      this.btnPrgMemoOpen.Text = "Go";
      this.btnPrgMemoOpen.UseVisualStyleBackColor = true;
      this.btnPrgMemoOpen.Click += new EventHandler(this.btnPrgMemoOpen_Click);
      this.lblPrgMemoCmdLine.AutoSize = true;
      this.lblPrgMemoCmdLine.Location = new Point(-1, 45);
      this.lblPrgMemoCmdLine.Name = "lblPrgMemoCmdLine";
      this.lblPrgMemoCmdLine.Size = new Size(76, 13);
      this.lblPrgMemoCmdLine.TabIndex = 1;
      this.lblPrgMemoCmdLine.Text = "Command Line";
      this.txtPrgMemoCmdLine.BorderStyle = BorderStyle.FixedSingle;
      this.txtPrgMemoCmdLine.Location = new Point(90, 43);
      this.txtPrgMemoCmdLine.Name = "txtPrgMemoCmdLine";
      this.txtPrgMemoCmdLine.Size = new Size(314, 21);
      this.txtPrgMemoCmdLine.TabIndex = 0;
      this.txtPrgMemoCmdLine.TabStop = false;
      this.lblPrgMemoExePath.AutoSize = true;
      this.lblPrgMemoExePath.Location = new Point(-1, 15);
      this.lblPrgMemoExePath.Name = "lblPrgMemoExePath";
      this.lblPrgMemoExePath.Size = new Size(85, 13);
      this.lblPrgMemoExePath.TabIndex = 1;
      this.lblPrgMemoExePath.Text = "Executable Path";
      this.txtPrgMemoExePath.BorderStyle = BorderStyle.FixedSingle;
      this.txtPrgMemoExePath.Location = new Point(90, 11);
      this.txtPrgMemoExePath.Name = "txtPrgMemoExePath";
      this.txtPrgMemoExePath.Size = new Size(233, 21);
      this.txtPrgMemoExePath.TabIndex = 0;
      this.txtPrgMemoExePath.TabStop = false;
      this.pnlPrgMemoTop.Controls.Add((Control) this.lblPrgMemoDate);
      this.pnlPrgMemoTop.Controls.Add((Control) this.lblPrgMemoTitle);
      this.pnlPrgMemoTop.Dock = DockStyle.Top;
      this.pnlPrgMemoTop.Location = new Point(10, 0);
      this.pnlPrgMemoTop.Name = "pnlPrgMemoTop";
      this.pnlPrgMemoTop.Size = new Size(406, 21);
      this.pnlPrgMemoTop.TabIndex = 2;
      this.lblPrgMemoDate.Dock = DockStyle.Fill;
      this.lblPrgMemoDate.Location = new Point(165, 0);
      this.lblPrgMemoDate.Name = "lblPrgMemoDate";
      this.lblPrgMemoDate.Size = new Size(241, 21);
      this.lblPrgMemoDate.TabIndex = 0;
      this.lblPrgMemoDate.Text = "Updated on: 14/02/2010 21:26";
      this.lblPrgMemoDate.TextAlign = ContentAlignment.MiddleRight;
      this.lblPrgMemoTitle.Dock = DockStyle.Left;
      this.lblPrgMemoTitle.Location = new Point(0, 0);
      this.lblPrgMemoTitle.Name = "lblPrgMemoTitle";
      this.lblPrgMemoTitle.Size = new Size(165, 21);
      this.lblPrgMemoTitle.TabIndex = 0;
      this.lblPrgMemoTitle.Text = "Program Memo";
      this.lblPrgMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlError.BorderStyle = BorderStyle.FixedSingle;
      this.pnlError.Controls.Add((Control) this.lblError);
      this.pnlError.Dock = DockStyle.Fill;
      this.pnlError.Location = new Point(0, 0);
      this.pnlError.Name = "pnlError";
      this.pnlError.Padding = new Padding(10, 0, 10, 10);
      this.pnlError.Size = new Size(428, 149);
      this.pnlError.TabIndex = 6;
      this.lblError.Dock = DockStyle.Fill;
      this.lblError.Location = new Point(10, 0);
      this.lblError.Name = "lblError";
      this.lblError.Size = new Size(406, 137);
      this.lblError.TabIndex = 1;
      this.lblError.Text = "Memo is not in valid format. Details cannot be shown.";
      this.lblError.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlSplitter2.Dock = DockStyle.Top;
      this.pnlSplitter2.Location = new Point(10, 4);
      this.pnlSplitter2.Name = "pnlSplitter2";
      this.pnlSplitter2.Size = new Size(428, 4);
      this.pnlSplitter2.TabIndex = 26;
      this.pnlSplitter1.Dock = DockStyle.Top;
      this.pnlSplitter1.Location = new Point(10, 0);
      this.pnlSplitter1.Name = "pnlSplitter1";
      this.pnlSplitter1.Size = new Size(428, 4);
      this.pnlSplitter1.TabIndex = 27;
      this.pnlOptions.BackColor = Color.White;
      this.pnlOptions.Controls.Add((Control) this.lblAllMemo);
      this.pnlOptions.Dock = DockStyle.Top;
      this.pnlOptions.Location = new Point(0, 0);
      this.pnlOptions.Name = "pnlOptions";
      this.pnlOptions.Size = new Size(448, 33);
      this.pnlOptions.TabIndex = 21;
      this.lblAllMemo.BackColor = Color.White;
      this.lblAllMemo.ForeColor = Color.Black;
      this.lblAllMemo.Location = new Point(0, 3);
      this.lblAllMemo.Name = "lblAllMemo";
      this.lblAllMemo.Padding = new Padding(3, 7, 0, 0);
      this.lblAllMemo.Size = new Size(129, 27);
      this.lblAllMemo.TabIndex = 21;
      this.lblAllMemo.Text = "Local Memo";
      this.ofd.FileName = "openFileDialog1";
      this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.Width = 150;
      this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.Width = 150;
      this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.Width = 140;
      this.dataGridViewTextBoxColumn4.HeaderText = "Scope";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(450, 350);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmMemoListAll);
      this.Load += new EventHandler(this.frmMemoListAll_Load);
      this.pnlControl.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.pnlTop.ResumeLayout(false);
      this.pnlGrid.ResumeLayout(false);
      ((ISupportInitialize) this.dgMemoList).EndInit();
      this.cmsAllMemo.ResumeLayout(false);
      this.pnlBottom.ResumeLayout(false);
      this.pnlMemos.ResumeLayout(false);
      this.pnlTxtMemo.ResumeLayout(false);
      this.pnlTxtMemoContents.ResumeLayout(false);
      this.pnlRtbTxtMemo.ResumeLayout(false);
      this.pnlTxtMemoTop.ResumeLayout(false);
      this.pnlRefMemo.ResumeLayout(false);
      this.pnlRefMemoContents.ResumeLayout(false);
      this.pnlRefMemoContents.PerformLayout();
      this.pnlRefMemoTop.ResumeLayout(false);
      this.pnlHypMemo.ResumeLayout(false);
      this.pnlHypMemoContents.ResumeLayout(false);
      this.pnlHypMemoContents.PerformLayout();
      this.pnlHypMemoTop.ResumeLayout(false);
      this.pnlPrgMemo.ResumeLayout(false);
      this.pnlPrgMemoContents.ResumeLayout(false);
      this.pnlPrgMemoContents.PerformLayout();
      this.pnlPrgMemoTop.ResumeLayout(false);
      this.pnlError.ResumeLayout(false);
      this.pnlOptions.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public frmMemoListAll(frmMemoList frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.bMemoChanged = false;
      this.intMemoType = this.GetMemoType();
      this.GetSaveMemoValue();
      this.UpdateFont();
      this.lblTxtMemoDate.Text = string.Empty;
      this.lblRefMemoDate.Text = string.Empty;
      this.lblHypMemoDate.Text = string.Empty;
      this.lblPrgMemoDate.Text = string.Empty;
      this.LoadResources();
    }

    private void frmMemoListAll_Load(object sender, EventArgs e)
    {
      this.strMemoPriority = this.GetMemoPriority();
      this.strDateFormat = this.GetDateFormat();
    }

    private void dgMemoList_SelectionChanged(object sender, EventArgs e)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (this.dgMemoList.SelectedRows.Count > 0)
      {
        this.ClearItems(true, false, true);
        try
        {
          XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(this.dgMemoList.SelectedRows[0].Tag.ToString()));
          this.ShowMemoDetails(xmlDocument.ReadNode((XmlReader) xmlTextReader));
        }
        catch
        {
          this.ShowMemoDetails((XmlNode) null);
        }
      }
      else
        this.ClearItems(true, false, false);
    }

    private void btnHypMemoOpen_Click(object sender, EventArgs e)
    {
      if (!(this.txtHypMemoUrl.Text.Trim() != string.Empty))
        return;
      try
      {
        string str = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
        if (str != string.Empty && str != null)
        {
          RegistryReader registryReader = new RegistryReader();
          string fileName = registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + str + ".exe", string.Empty);
          if (fileName == null)
          {
            Process process = Process.Start(registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty), this.txtHypMemoUrl.Text);
            if (process == null)
              return;
            IntPtr handle = process.Handle;
            frmMemoListAll.SetForegroundWindow(process.Handle);
          }
          else
          {
            Process process = Process.Start(fileName, this.txtHypMemoUrl.Text);
            if (process == null)
              return;
            IntPtr handle = process.Handle;
            frmMemoListAll.SetForegroundWindow(process.Handle);
          }
        }
        else
        {
          Process process = Process.Start("IExplore.exe", this.txtHypMemoUrl.Text);
          if (process == null)
            return;
          IntPtr handle = process.Handle;
          frmMemoListAll.SetForegroundWindow(process.Handle);
        }
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("(E-MLC-EM007) Can not open Internet Explorer", "(E-MLC-EM007)_IE", ResourceType.POPUP_MESSAGE), this.frmParent.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void btnRefMemoOpen_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      if (this.txtRefMemoServerKey.Text.Trim().Equals(string.Empty) || this.txtRefMemoBookId.Text.Trim().Equals(string.Empty))
        return;
      this.frmParent.OpenBookFromString(this.txtRefMemoServerKey.Text + " " + this.txtRefMemoBookId.Text + " " + this.txtRefMemoOtherRef.Text);
    }

    private void btnPrgMemoOpen_Click(object sender, EventArgs e)
    {
      try
      {
        if (File.Exists(this.txtPrgMemoExePath.Text) && this.txtPrgMemoExePath.Text.ToUpper().EndsWith(".EXE"))
        {
          Process.Start(new ProcessStartInfo()
          {
            FileName = this.txtPrgMemoExePath.Text,
            Arguments = this.txtPrgMemoCmdLine.Text,
            UseShellExecute = false
          });
        }
        else
        {
          if (!(this.txtPrgMemoExePath.Text.Trim() != string.Empty))
            return;
          int num = (int) MessageBox.Show(this.GetResource("(E-MLC-EM008) Specified information does not exist", "(E-MLC-EM008)_INFORMATION", ResourceType.POPUP_MESSAGE), this.frmParent.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("(E-MLC-EM009) Specified information does not exist", "(E-MLC-EM009)_INFORMATION", ResourceType.POPUP_MESSAGE), this.frmParent.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void btnPrgMemoExePathBrowse_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.txtPrgMemoExePath.Text.Trim() != string.Empty && Directory.Exists(Path.GetDirectoryName(this.txtPrgMemoExePath.Text)))
          this.ofd.InitialDirectory = Path.GetDirectoryName(this.txtPrgMemoExePath.Text);
        else
          this.ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        this.ofd.Filter = "Executable Files (*.exe)|*.exe";
        this.ofd.RestoreDirectory = false;
        if (this.ofd.ShowDialog() != DialogResult.OK)
          return;
        this.txtPrgMemoExePath.Text = this.ofd.FileName;
      }
      catch
      {
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.frmParent.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.frmParent.Close();
    }

    private void goToPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(this.dgMemoList.SelectedRows[0].Tag.ToString()));
          XmlNode xmlNode = xmlDocument.ReadNode((XmlReader) xmlTextReader);
          if (Settings.Default.OpenInCurrentInstance)
          {
            this.frmParent.OpenParentPage(xmlNode.Attributes["ServerKey"].Value, xmlNode.Attributes["BookId"].Value, xmlNode.Attributes["PageId"].Value, xmlNode.Attributes["PicIndex"].Value, xmlNode.Attributes["ListIndex"].Value, xmlNode.Attributes["PartNo"].Value);
            this.frmParent.Close();
          }
          else
          {
            string[] sArgs = new string[5]
            {
              "-o",
              xmlNode.Attributes["ServerKey"].Value,
              xmlNode.Attributes["BookId"].Value,
              xmlNode.Attributes["PageId"].Value,
              xmlNode.Attributes["PicIndex"].Value
            };
            if (!Global.SecurityLocksOpen(this.frmParent.frmParent.GetBookNode(xmlNode.Attributes["BookId"].Value, this.frmParent.frmParent.p_ServerId), this.frmParent.frmParent.SchemaNode, this.frmParent.frmParent.ServerId, this.frmParent.frmParent))
              return;
            this.frmParent.frmParent.BookJump(sArgs);
          }
        }
        catch
        {
        }
      }
      catch
      {
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblAllMemo.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgMemoList.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgMemoList.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    private void AddMemoToList(string type, string value)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string str1 = value.Length <= 25 ? value : value.Substring(0, 25) + "...";
      if (str1.Contains("||"))
        str1 = str1.Replace("||", " ");
      string str2 = !type.ToUpper().Equals("TXT") ? (!type.ToUpper().Equals("REF") ? (!type.ToUpper().Equals("HYP") ? (!type.ToUpper().Equals("PRG") ? "Unknown" : "Program") : "Hyperlink") : "Reference") : "Text";
      string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
      DataGridViewRow dataGridViewRow = new DataGridViewRow();
      DataGridViewCell dataGridViewCell1 = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewCell1.Value = (object) str1;
      dataGridViewRow.Cells.Add(dataGridViewCell1);
      DataGridViewCell dataGridViewCell2 = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewCell2.Value = (object) str2;
      dataGridViewRow.Cells.Add(dataGridViewCell2);
      DataGridViewCell dataGridViewCell3 = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewCell3.Value = (object) date;
      dataGridViewRow.Cells.Add(dataGridViewCell3);
      dataGridViewRow.Tag = (object) this.CreateMemoNode(type, value, date).OuterXml;
      this.dgMemoList.SelectionChanged -= new EventHandler(this.dgMemoList_SelectionChanged);
      this.dgMemoList.Rows.Add(dataGridViewRow);
      this.dgMemoList.ClearSelection();
      this.dgMemoList.SelectionChanged += new EventHandler(this.dgMemoList_SelectionChanged);
      this.dgMemoList.FirstDisplayedScrollingRowIndex = this.dgMemoList.Rows.Count - 1;
      this.bMemoChanged = true;
    }

    private void AddMemoToList(XmlNode xNode, string sScope)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      if (xNode.Attributes["Value"] == null || !(xNode.Attributes["Value"].Value.Trim() != string.Empty))
        return;
      string str1 = xNode.Attributes["Value"].Value.Trim().Length <= 25 ? xNode.Attributes["Value"].Value.Trim() : xNode.Attributes["Value"].Value.Trim().Substring(0, 25) + "...";
      if (xNode.Attributes["Type"] == null || !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "TXT") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "REF") && (!(xNode.Attributes["Type"].Value.Trim().ToUpper() == "HYP") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "PRG")))
        return;
      string str2 = !xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("TXT") ? (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("REF") ? (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("HYP") ? (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("PRG") ? "Unknown" : "Program") : "Hyperlink") : "Reference") : "Text";
      if (!str2.ToUpper().Equals("TEXT"))
      {
        if (str1.Contains("||"))
          str1 = str1.Replace("||", " ");
        if (str1.Contains("|"))
          str1 = str1.Replace("|", " ");
      }
      string s;
      if (xNode.Attributes["Update"] != null)
      {
        if (xNode.Attributes["Update"].Value.Trim() != string.Empty)
        {
          s = xNode.Attributes["Update"].Value.Trim();
          try
          {
            DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
            string[] strArray = new string[4]
            {
              "yyyy/MM/dd hh:mm:ss",
              "MM/dd hh:mm",
              "yyyy/MM/dd",
              "hh:mm:ss"
            };
            if (this.strDateFormat != string.Empty)
            {
              if (this.strDateFormat.ToUpper() == "INVALID")
              {
                s = "Unknown";
              }
              else
              {
                foreach (string str3 in strArray)
                {
                  if (this.strDateFormat == str3)
                    s = exact.ToString(this.strDateFormat);
                }
              }
            }
          }
          catch
          {
          }
        }
        else
          s = "Unknown";
      }
      else
        s = "Unknown";
      DataGridViewRow dataGridViewRow = new DataGridViewRow();
      DataGridViewCell dataGridViewCell1 = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewCell1.Value = (object) str1;
      dataGridViewRow.Cells.Add(dataGridViewCell1);
      DataGridViewCell dataGridViewCell2 = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewCell2.Value = (object) str2;
      dataGridViewRow.Cells.Add(dataGridViewCell2);
      DataGridViewCell dataGridViewCell3 = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewCell3.Value = (object) s;
      dataGridViewRow.Cells.Add(dataGridViewCell3);
      DataGridViewCell dataGridViewCell4 = (DataGridViewCell) new DataGridViewTextBoxCell();
      dataGridViewCell4.Value = (object) sScope;
      dataGridViewRow.Cells.Add(dataGridViewCell4);
      dataGridViewRow.Tag = (object) xNode.OuterXml;
      this.dgMemoList.Rows.Add(dataGridViewRow);
    }

    private void UpdateMemoToList(DataGridViewRow row)
    {
      string attValue = string.Empty;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      if (this.dgMemoList.Columns.Count != 4)
        return;
      string type = row.Cells[1].Value.ToString();
      if (type == "Text")
      {
        if (!this.rtbTxtMemo.Text.Trim().Equals(string.Empty))
        {
          attValue = this.rtbTxtMemo.Text;
          type = "txt";
          empty3 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
      }
      else if (type == "Reference")
      {
        if (!this.txtRefMemoServerKey.Text.Trim().Equals(string.Empty) && !this.txtRefMemoBookId.Text.Trim().Equals(string.Empty))
        {
          attValue = this.txtRefMemoServerKey.Text + " " + this.txtRefMemoBookId.Text + " " + this.txtRefMemoOtherRef.Text;
          type = "ref";
          empty3 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
      }
      else if (type == "Hyperlink")
      {
        if (!this.txtHypMemoUrl.Text.Trim().Equals(string.Empty))
        {
          attValue = this.txtHypMemoUrl.Text;
          type = "hyp";
          empty3 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
      }
      else if (type == "Program" && !this.txtPrgMemoExePath.Text.Trim().Equals(string.Empty))
      {
        attValue = this.txtPrgMemoExePath.Text + "|" + this.txtPrgMemoCmdLine.Text;
        type = "prg";
        empty3 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
      }
      if (attValue.Trim().Equals(string.Empty) || this.MatchXmlAttribute("Value", attValue, row.Tag.ToString()))
        return;
      string str = attValue.Trim().Length <= 25 ? attValue.Trim() : attValue.Trim().Substring(0, 25) + "...";
      if (str.Contains("||"))
        str = str.Replace("||", " ");
      if (type == "txt")
        this.lblTxtMemoDate.Text = empty3;
      else if (type == "ref")
        this.lblRefMemoDate.Text = empty3;
      else if (type == "hyp")
        this.lblHypMemoDate.Text = empty3;
      else if (type == "prg")
        this.lblPrgMemoDate.Text = empty3;
      this.dgMemoList[0, row.Index].Value = (object) str;
      this.dgMemoList[2, row.Index].Value = (object) empty3;
      this.dgMemoList.SelectedRows[0].Tag = (object) this.CreateMemoNode(type, attValue, empty3).OuterXml;
      this.bMemoChanged = true;
    }

    private bool MatchXmlAttribute(string attName, string attValue, string nodeXML)
    {
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(nodeXML));
        XmlNode xmlNode = xmlDocument.ReadNode((XmlReader) xmlTextReader);
        return xmlNode.Attributes[attName] != null && xmlNode.Attributes[attName].Value.Trim() == attValue.Trim();
      }
      catch
      {
        return false;
      }
    }

    private XmlNode CreateMemoNode(string type, string value, string date)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode node = xmlDocument.CreateNode(XmlNodeType.Element, "Memo", (string) null);
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("ServerKey");
      attribute1.Value = this.frmParent.sServerKey;
      node.Attributes.Append(attribute1);
      XmlAttribute attribute2 = xmlDocument.CreateAttribute("BookId");
      attribute2.Value = this.frmParent.sBookId;
      node.Attributes.Append(attribute2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("PageId");
      attribute3.Value = this.frmParent.sPageId;
      node.Attributes.Append(attribute3);
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("PicIndex");
      attribute4.Value = this.frmParent.sPicIndex;
      node.Attributes.Append(attribute4);
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("ListIndex");
      attribute5.Value = this.frmParent.sListIndex;
      node.Attributes.Append(attribute5);
      XmlAttribute attribute6 = xmlDocument.CreateAttribute("PartNo");
      attribute6.Value = this.frmParent.sPartNumber;
      node.Attributes.Append(attribute6);
      XmlAttribute attribute7 = xmlDocument.CreateAttribute("Type");
      attribute7.Value = type;
      node.Attributes.Append(attribute7);
      XmlAttribute attribute8 = xmlDocument.CreateAttribute("Value");
      attribute8.Value = value;
      node.Attributes.Append(attribute8);
      XmlAttribute attribute9 = xmlDocument.CreateAttribute("Update");
      attribute9.Value = date;
      node.Attributes.Append(attribute9);
      return node;
    }

    private void ShowMemoDetails(XmlNode xNode)
    {
      if (xNode != null && xNode.Attributes.Count > 0 && (xNode.Attributes["Value"] != null && xNode.Attributes["Type"] != null) && (xNode.Attributes["Value"].Value.Trim() != string.Empty && xNode.Attributes["Type"].Value.Trim() != string.Empty))
      {
        if (xNode.Attributes["Type"].Value.Trim().ToUpper() == "TXT")
        {
          this.SetTabProperty("TEXT");
          string empty = string.Empty;
          string s;
          if (xNode.Attributes["Update"] != null)
          {
            if (xNode.Attributes["Update"].Value.Trim() != string.Empty)
            {
              s = xNode.Attributes["Update"].Value.Trim();
              try
              {
                DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                string[] strArray = new string[4]
                {
                  "yyyy/MM/dd hh:mm:ss",
                  "MM/dd hh:mm",
                  "yyyy/MM/dd",
                  "hh:mm:ss"
                };
                if (this.strDateFormat != string.Empty)
                {
                  if (this.strDateFormat.ToUpper() == "INVALID")
                  {
                    s = "Unknown";
                  }
                  else
                  {
                    foreach (string str in strArray)
                    {
                      if (this.strDateFormat == str)
                        s = exact.ToString(this.strDateFormat);
                    }
                  }
                }
              }
              catch
              {
              }
            }
            else
              s = "Unknown";
          }
          else
            s = "Unknown";
          this.lblTxtMemoDate.Text = s;
          this.rtbTxtMemo.Text = xNode.Attributes["Value"].Value;
          this.pnlTxtMemo.BringToFront();
        }
        else if (xNode.Attributes["Type"].Value.Trim().ToUpper() == "REF")
        {
          this.SetTabProperty("REFRENCE");
          string empty = string.Empty;
          string s;
          if (xNode.Attributes["Update"] != null)
          {
            if (xNode.Attributes["Update"].Value.Trim() != string.Empty)
            {
              s = xNode.Attributes["Update"].Value.Trim();
              try
              {
                DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                string[] strArray = new string[4]
                {
                  "yyyy/MM/dd hh:mm:ss",
                  "MM/dd hh:mm",
                  "yyyy/MM/dd",
                  "hh:mm:ss"
                };
                if (this.strDateFormat != string.Empty)
                {
                  if (this.strDateFormat.ToUpper() == "INVALID")
                  {
                    s = "Unknown";
                  }
                  else
                  {
                    foreach (string str in strArray)
                    {
                      if (this.strDateFormat == str)
                        s = exact.ToString(this.strDateFormat);
                    }
                  }
                }
              }
              catch
              {
              }
            }
            else
              s = "Unknown";
          }
          else
            s = "Unknown";
          this.lblRefMemoDate.Text = s;
          string[] strArray1 = xNode.Attributes["Value"].Value.Split(new string[1]
          {
            " "
          }, StringSplitOptions.None);
          if (strArray1.Length >= 2)
          {
            this.txtRefMemoServerKey.Text = strArray1[0];
            this.txtRefMemoBookId.Text = strArray1[1];
            this.txtRefMemoOtherRef.Text = string.Empty;
            for (int index = 2; index < strArray1.Length; ++index)
            {
              this.txtRefMemoOtherRef.Text += strArray1[index];
              if (index < strArray1.Length - 1)
                this.txtRefMemoOtherRef.Text += " ";
            }
            this.pnlRefMemo.BringToFront();
          }
          else
            this.pnlError.BringToFront();
        }
        else if (xNode.Attributes["Type"].Value.Trim().ToUpper() == "HYP")
        {
          this.SetTabProperty("HYPERLINK");
          string empty = string.Empty;
          string s;
          if (xNode.Attributes["Update"] != null)
          {
            if (xNode.Attributes["Update"].Value.Trim() != string.Empty)
            {
              s = xNode.Attributes["Update"].Value.Trim();
              try
              {
                DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                string[] strArray = new string[4]
                {
                  "yyyy/MM/dd hh:mm:ss",
                  "MM/dd hh:mm",
                  "yyyy/MM/dd",
                  "hh:mm:ss"
                };
                if (this.strDateFormat != string.Empty)
                {
                  if (this.strDateFormat.ToUpper() == "INVALID")
                  {
                    s = "Unknown";
                  }
                  else
                  {
                    foreach (string str in strArray)
                    {
                      if (this.strDateFormat == str)
                        s = exact.ToString(this.strDateFormat);
                    }
                  }
                }
              }
              catch
              {
              }
            }
            else
              s = "Unknown";
          }
          else
            s = "Unknown";
          this.lblHypMemoDate.Text = s;
          this.txtHypMemoUrl.Text = xNode.Attributes["Value"].Value;
          this.pnlHypMemo.BringToFront();
        }
        else if (xNode.Attributes["Type"].Value.Trim().ToUpper() == "PRG")
        {
          this.SetTabProperty("PROGRAME");
          string empty = string.Empty;
          string s;
          if (xNode.Attributes["Update"] != null)
          {
            if (xNode.Attributes["Update"].Value.Trim() != string.Empty)
            {
              s = xNode.Attributes["Update"].Value.Trim();
              try
              {
                DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                string[] strArray = new string[4]
                {
                  "yyyy/MM/dd hh:mm:ss",
                  "MM/dd hh:mm",
                  "yyyy/MM/dd",
                  "hh:mm:ss"
                };
                if (this.strDateFormat != string.Empty)
                {
                  if (this.strDateFormat.ToUpper() == "INVALID")
                  {
                    s = "Unknown";
                  }
                  else
                  {
                    foreach (string str in strArray)
                    {
                      if (this.strDateFormat == str)
                        s = exact.ToString(this.strDateFormat);
                    }
                  }
                }
              }
              catch
              {
              }
            }
            else
              s = "Unknown";
          }
          else
            s = "Unknown";
          this.lblPrgMemoDate.Text = s;
          string[] strArray1 = xNode.Attributes["Value"].Value.Split(new string[1]
          {
            "|"
          }, StringSplitOptions.None);
          this.txtPrgMemoExePath.Text = strArray1[0];
          if (strArray1.Length > 1)
            this.txtPrgMemoCmdLine.Text = strArray1[1];
          this.pnlPrgMemo.BringToFront();
        }
        else
          this.pnlError.BringToFront();
      }
      else
        this.pnlError.BringToFront();
    }

    private void ClearItems(bool clearPanels, bool clearList, bool clearButtonCheck)
    {
      if (clearPanels)
      {
        this.lblTxtMemoDate.Text = string.Empty;
        this.rtbTxtMemo.Clear();
        this.lblRefMemoDate.Text = string.Empty;
        this.txtRefMemoServerKey.Clear();
        this.txtRefMemoBookId.Clear();
        this.txtRefMemoOtherRef.Clear();
        this.lblHypMemoDate.Text = string.Empty;
        this.txtHypMemoUrl.Clear();
        this.lblPrgMemoDate.Text = string.Empty;
        this.txtPrgMemoExePath.Clear();
        this.txtPrgMemoCmdLine.Clear();
      }
      if (!clearList)
        return;
      this.dgMemoList.Rows.Clear();
    }

    public bool isMemoChanged
    {
      get
      {
        return this.bMemoChanged;
      }
    }

    public string[] getGlobalMemos
    {
      get
      {
        try
        {
          if (this.dgMemoList.Rows.Count <= 0)
            return (string[]) null;
          string[] strArray = new string[this.dgMemoList.Rows.Count];
          for (int index = 0; index < this.dgMemoList.Rows.Count; ++index)
          {
            if (this.dgMemoList[3, index].Value != null && this.dgMemoList[3, index].Value.ToString() == "GLOBAL")
              strArray[index] = this.dgMemoList.Rows[index].Tag.ToString();
          }
          return strArray;
        }
        catch
        {
          return (string[]) null;
        }
      }
    }

    public string[] getLocalMemos
    {
      get
      {
        try
        {
          if (this.dgMemoList.Rows.Count <= 0)
            return (string[]) null;
          string[] strArray = new string[this.dgMemoList.Rows.Count];
          for (int index = 0; index < this.dgMemoList.Rows.Count; ++index)
          {
            if (this.dgMemoList[3, index].Value != null && this.dgMemoList[3, index].Value.ToString() == "LOCAL")
              strArray[index] = this.dgMemoList.Rows[index].Tag.ToString();
          }
          return strArray;
        }
        catch
        {
          return (string[]) null;
        }
      }
    }

    private void LoadResources()
    {
      this.lblAllMemo.Text = this.GetResource("All Memo", "ALL_MEMO", ResourceType.LABEL);
      this.dgMemoList.Columns[0].HeaderText = this.GetResource("Description", "DESCRIPTION", ResourceType.GRID_VIEW);
      this.dgMemoList.Columns[1].HeaderText = this.GetResource("Type", "TYPE", ResourceType.GRID_VIEW);
      this.dgMemoList.Columns[2].HeaderText = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.GRID_VIEW);
      this.lblPrgMemoTitle.Text = this.GetResource("Program Memo", "PROGRAM_MEMO", ResourceType.LABEL);
      this.lblPrgMemoDate.Text = this.GetResource("Updated ON:", "UPDATED_ON", ResourceType.LABEL);
      this.lblPrgMemoExePath.Text = this.GetResource("Executable Path", "EXECUTABLE_PATH", ResourceType.LABEL);
      this.lblPrgMemoCmdLine.Text = this.GetResource("Command Line", "COMMAND_LINE", ResourceType.LABEL);
      this.btnPrgMemoExePathBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.BUTTON);
      this.btnPrgMemoOpen.Text = this.GetResource("Go", "GO", ResourceType.BUTTON);
      this.btnOK.Text = this.GetResource("Ok", "OK", ResourceType.BUTTON);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      this.lblHypMemoTitle.Text = this.GetResource("Hyperlink Memo", "HYPERLINK_MEMO", ResourceType.LABEL);
      this.lblHypMemoDate.Text = this.GetResource("Updated On:", "UPDATED_ON_HYP", ResourceType.LABEL);
      this.lblHypMemoUrl.Text = this.GetResource("URL", "URL", ResourceType.LABEL);
      this.lblHypMemoNote.Text = this.GetResource("Provide the web page address (URL) in the above field to hyperlink", "PROVIDE_URL", ResourceType.LABEL);
      this.btnHypMemoOpen.Text = this.GetResource("GO", "GO_HYP", ResourceType.BUTTON);
      this.lblRefMemoTitle.Text = this.GetResource("Reference Memo", "REFERENCE_MEMO", ResourceType.LABEL);
      this.lblRefMemoDate.Text = this.GetResource("Updated on:", "UPDATED_ON_REF", ResourceType.LABEL);
      this.lblRefMemoServerKey.Text = this.GetResource("Server Key", "SERVER_KEY", ResourceType.LABEL);
      this.lblRefMemoBookId.Text = this.GetResource("Book Publishing Id", "BOOK_ID", ResourceType.LABEL);
      this.lblRefMemoOtherRef.Text = this.GetResource("Other Ref", "OTHER_REF", ResourceType.LABEL);
      this.lblRefMemoNote.Text = this.GetResource("(space separated values)", "SPACE_SEPRATED", ResourceType.LABEL);
      this.btnRefMemoOpen.Text = this.GetResource("GO", "GO_REF", ResourceType.BUTTON);
      this.lblTxtMemoTitle.Text = this.GetResource("Text Memo", "TEXT_MEMO", ResourceType.LABEL);
      this.lblTxtMemoDate.Text = this.GetResource("Updated On:", "UPDATED_ON_TXT", ResourceType.LABEL);
      this.goToPageToolStripMenuItem.Text = this.GetResource("Go to page", "GO_TO_PAGE", ResourceType.TOOLSTRIP);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MEMO_LIST']" + "/Screen[@Name='MEMOLIST_ALL']";
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

    public void LoadMemos(bool bPictureMemos, bool bPartMemos)
    {
      try
      {
        this.dgMemoList.Rows.Clear();
        if (this.strMemoPriority.ToUpper() == "GLOBAL")
        {
          this.LoadGlobalMemos(bPictureMemos, bPartMemos);
          this.LoadLocalMemos(bPictureMemos, bPartMemos);
          this.LoadAdminMemos(bPictureMemos, bPartMemos);
        }
        else if (this.strMemoPriority.ToUpper() == "ADMIN")
        {
          this.LoadAdminMemos(bPictureMemos, bPartMemos);
          this.LoadLocalMemos(bPictureMemos, bPartMemos);
          this.LoadGlobalMemos(bPictureMemos, bPartMemos);
        }
        else
        {
          this.LoadLocalMemos(bPictureMemos, bPartMemos);
          this.LoadGlobalMemos(bPictureMemos, bPartMemos);
          this.LoadAdminMemos(bPictureMemos, bPartMemos);
        }
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("(E-MLC-EM001) Failed to load specified object", "(E-MLC-EM001)_FAILED", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    public void SetTabProperty(string strMemoType)
    {
      if (strMemoType == "TEXT")
      {
        this.rtbTxtMemo.TabStop = true;
        this.rtbTxtMemo.TabIndex = 1;
        this.txtRefMemoServerKey.TabStop = false;
        this.txtRefMemoBookId.TabStop = false;
        this.txtRefMemoOtherRef.TabStop = false;
        this.btnRefMemoOpen.TabStop = false;
        this.txtHypMemoUrl.TabStop = false;
        this.btnHypMemoOpen.TabStop = false;
        this.txtPrgMemoExePath.TabStop = false;
        this.btnPrgMemoExePathBrowse.TabStop = false;
        this.txtPrgMemoCmdLine.TabStop = false;
        this.btnPrgMemoOpen.TabStop = false;
        this.btnOK.TabIndex = 2;
        this.btnCancel.TabIndex = 3;
      }
      if (strMemoType == "REFRENCE")
      {
        this.rtbTxtMemo.TabStop = false;
        this.txtRefMemoServerKey.TabStop = true;
        this.txtRefMemoBookId.TabStop = true;
        this.txtRefMemoOtherRef.TabStop = true;
        this.btnRefMemoOpen.TabStop = true;
        this.txtRefMemoServerKey.TabIndex = 1;
        this.txtRefMemoBookId.TabIndex = 2;
        this.txtRefMemoOtherRef.TabIndex = 3;
        this.btnRefMemoOpen.TabIndex = 4;
        this.txtHypMemoUrl.TabStop = false;
        this.btnHypMemoOpen.TabStop = false;
        this.txtPrgMemoExePath.TabStop = false;
        this.btnPrgMemoExePathBrowse.TabStop = false;
        this.txtPrgMemoCmdLine.TabStop = false;
        this.btnPrgMemoOpen.TabStop = false;
        this.btnOK.TabIndex = 5;
        this.btnCancel.TabIndex = 6;
      }
      if (strMemoType == "HYPERLINK")
      {
        this.rtbTxtMemo.TabStop = false;
        this.txtRefMemoServerKey.TabStop = false;
        this.txtRefMemoBookId.TabStop = false;
        this.txtRefMemoOtherRef.TabStop = false;
        this.btnRefMemoOpen.TabStop = false;
        this.txtHypMemoUrl.TabStop = true;
        this.btnHypMemoOpen.TabStop = true;
        this.txtHypMemoUrl.TabIndex = 2;
        this.btnHypMemoOpen.TabIndex = 3;
        this.txtPrgMemoExePath.TabStop = false;
        this.btnPrgMemoExePathBrowse.TabStop = false;
        this.txtPrgMemoCmdLine.TabStop = false;
        this.btnPrgMemoOpen.TabStop = false;
        this.btnOK.TabIndex = 4;
        this.btnCancel.TabIndex = 5;
      }
      if (!(strMemoType == "PROGRAME"))
        return;
      this.rtbTxtMemo.TabStop = false;
      this.txtRefMemoServerKey.TabStop = false;
      this.txtRefMemoBookId.TabStop = false;
      this.txtRefMemoOtherRef.TabStop = false;
      this.btnRefMemoOpen.TabStop = false;
      this.txtHypMemoUrl.TabStop = false;
      this.btnHypMemoOpen.TabStop = false;
      this.txtPrgMemoExePath.TabStop = true;
      this.btnPrgMemoExePathBrowse.TabStop = true;
      this.txtPrgMemoCmdLine.TabStop = true;
      this.btnPrgMemoOpen.TabStop = true;
      this.txtPrgMemoExePath.TabIndex = 1;
      this.btnPrgMemoExePathBrowse.TabIndex = 2;
      this.txtPrgMemoCmdLine.TabIndex = 3;
      this.btnPrgMemoOpen.TabIndex = 4;
      this.btnOK.TabIndex = 5;
      this.btnCancel.TabIndex = 6;
    }

    private void LoadAdminMemos(bool bPictureMemos, bool bPartMemos)
    {
      try
      {
        if (this.frmParent.xnlAdminMemo == null || this.frmParent.xnlAdminMemo.Count <= 0)
          return;
        foreach (XmlNode xNode in this.frmParent.xnlAdminMemo)
        {
          if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value != string.Empty)
          {
            if (bPartMemos)
              this.AddMemoToList(xNode, "ADMIN");
          }
          else if (bPictureMemos)
            this.AddMemoToList(xNode, "ADMIN");
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void LoadGlobalMemos(bool bPictureMemos, bool bPartMemos)
    {
      try
      {
        if (this.frmParent.xnlGlobalMemo == null || this.frmParent.xnlGlobalMemo.Count <= 0)
          return;
        foreach (XmlNode xNode in this.frmParent.xnlGlobalMemo)
        {
          if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value != string.Empty)
          {
            if (bPartMemos)
            {
              if (this.bSaveMemoOnBookLevel && this.intMemoType != 2)
              {
                if (!this.DuplicateMemoNode(xNode))
                  this.AddMemoToList(xNode, "GLOBAL");
              }
              else
                this.AddMemoToList(xNode, "GLOBAL");
            }
          }
          else if (bPictureMemos)
            this.AddMemoToList(xNode, "GLOBAL");
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void LoadLocalMemos(bool bPictureMemos, bool bPartMemos)
    {
      try
      {
        if (this.frmParent.xnlLocalMemo == null || this.frmParent.xnlLocalMemo.Count <= 0)
          return;
        foreach (XmlNode xNode in this.frmParent.xnlLocalMemo)
        {
          if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value != string.Empty)
          {
            if (bPartMemos)
            {
              if (this.bSaveMemoOnBookLevel && this.intMemoType != 2)
              {
                if (!this.DuplicateMemoNode(xNode))
                  this.AddMemoToList(xNode, "LOCAL");
              }
              else
                this.AddMemoToList(xNode, "LOCAL");
            }
          }
          else if (bPictureMemos)
            this.AddMemoToList(xNode, "LOCAL");
        }
      }
      catch (Exception ex)
      {
      }
    }

    private string GetMemoPriority()
    {
      try
      {
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"] == null)
          return "LOCAL";
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"].ToString().ToUpper() == "ADMIN")
        {
          this.strMemoPriority = "ADMIN";
          return this.strMemoPriority;
        }
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"].ToString().ToUpper() == "GLOBAL")
        {
          this.strMemoPriority = "GLOBAL";
          return this.strMemoPriority;
        }
        this.strMemoPriority = "LOCAL";
        return this.strMemoPriority;
      }
      catch (Exception ex)
      {
        return "LOCAL";
      }
    }

    private string GetDateFormat()
    {
      try
      {
        string str = string.Empty;
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"] != null)
        {
          if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() != "HIDDEN")
            str = Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString();
          else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() == "HIDDEN")
            str = "INVALID";
        }
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"] != null && Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString().ToUpper() != "HIDDEN")
          str = !(str.ToUpper() == "INVALID") ? str + " " + Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString() : Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString();
        this.strDateFormat = str;
        return this.strDateFormat;
      }
      catch (Exception ex)
      {
        return string.Empty;
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

    private bool DuplicateMemoNode(XmlNode xNode)
    {
      try
      {
        if (xNode != null && xNode.Attributes.Count > 0)
        {
          foreach (DataGridViewRow row in (IEnumerable) this.dgMemoList.Rows)
          {
            string s = row.Tag.ToString();
            if (row.Tag.ToString().Contains("^"))
              s = row.Tag.ToString().Split('^')[0];
            if (s != string.Empty)
            {
              XmlNode xmlNode = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
              if (xNode.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() && xNode.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["BookId"].Value.ToString().Trim().ToUpper() && (xNode.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["PartNo"].Value.ToString().Trim().ToUpper() && xNode.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["Type"].Value.ToString().Trim().ToUpper()) && (xNode.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["Value"].Value.ToString().Trim().ToUpper() && xNode.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["Update"].Value.ToString().Trim().ToUpper()))
                return true;
            }
          }
        }
        return false;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private void GetSaveMemoValue()
    {
      try
      {
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"] == null || !(Program.iniServers[this.frmParent.frmParent.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"].ToString().ToUpper() == "ON"))
          return;
        this.bSaveMemoOnBookLevel = true;
      }
      catch (Exception ex)
      {
        this.bSaveMemoOnBookLevel = false;
      }
    }
  }
}

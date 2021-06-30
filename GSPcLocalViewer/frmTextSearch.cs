// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmTextSearch
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using System.Xml.Linq;

namespace GSPcLocalViewer
{
  public class frmTextSearch : Form
  {
    private const string dllZipper = "ZIPPER.dll";
    private IContainer components;
    private Label lblTextSearch;
    private Panel pnlControl;
    private Button btnSearch;
    private Panel pnlForm;
    private DataGridView dgViewSearch;
    private Panel pnlSearch;
    private TextBox txtPartNumber;
    private CheckBox checkBoxExactMatch;
    private CheckBox checkBoxMatchCase;
    private BackgroundWorker bgWorker;
    private Panel pnlSearchResults;
    private PictureBox picLoading;
    private StatusStrip ssStatus;
    private ToolStripStatusLabel lblStatus;
    private Button btnClearHistory;
    private frmViewer frmParent;
    private frmSearch frmContainer;
    private string statusText;
    private string searchString;
    public bool ISPDF;
    public string sBookType;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      this.lblTextSearch = new Label();
      this.pnlControl = new Panel();
      this.btnClearHistory = new Button();
      this.btnSearch = new Button();
      this.pnlForm = new Panel();
      this.pnlSearchResults = new Panel();
      this.dgViewSearch = new DataGridView();
      this.picLoading = new PictureBox();
      this.pnlSearch = new Panel();
      this.checkBoxExactMatch = new CheckBox();
      this.checkBoxMatchCase = new CheckBox();
      this.txtPartNumber = new TextBox();
      this.bgWorker = new BackgroundWorker();
      this.ssStatus = new StatusStrip();
      this.lblStatus = new ToolStripStatusLabel();
      this.pnlControl.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.pnlSearchResults.SuspendLayout();
      ((ISupportInitialize) this.dgViewSearch).BeginInit();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.pnlSearch.SuspendLayout();
      this.ssStatus.SuspendLayout();
      this.SuspendLayout();
      this.lblTextSearch.BackColor = Color.White;
      this.lblTextSearch.Dock = DockStyle.Top;
      this.lblTextSearch.ForeColor = Color.Black;
      this.lblTextSearch.Location = new Point(10, 0);
      this.lblTextSearch.Name = "lblTextSearch";
      this.lblTextSearch.Padding = new Padding(0, 7, 0, 0);
      this.lblTextSearch.Size = new Size(478, 27);
      this.lblTextSearch.TabIndex = 16;
      this.lblTextSearch.Text = "Text";
      this.pnlControl.Controls.Add((Control) this.btnClearHistory);
      this.pnlControl.Controls.Add((Control) this.btnSearch);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(10, 395);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 15, 4);
      this.pnlControl.Size = new Size(478, 31);
      this.pnlControl.TabIndex = 18;
      this.btnClearHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClearHistory.Location = new Point(211, 5);
      this.btnClearHistory.Name = "btnClearHistory";
      this.btnClearHistory.Size = new Size(114, 23);
      this.btnClearHistory.TabIndex = 5;
      this.btnClearHistory.Text = "Clear Search History";
      this.btnClearHistory.UseVisualStyleBackColor = true;
      this.btnClearHistory.Click += new EventHandler(this.btnClearHistory_Click);
      this.btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnSearch.Enabled = false;
      this.btnSearch.Location = new Point(350, 5);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new Size(114, 23);
      this.btnSearch.TabIndex = 6;
      this.btnSearch.Text = "Search";
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlSearchResults);
      this.pnlForm.Controls.Add((Control) this.pnlSearch);
      this.pnlForm.Controls.Add((Control) this.lblTextSearch);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(10, 0, 10, 0);
      this.pnlForm.Size = new Size(500, 428);
      this.pnlForm.TabIndex = 19;
      this.pnlSearchResults.Controls.Add((Control) this.dgViewSearch);
      this.pnlSearchResults.Controls.Add((Control) this.picLoading);
      this.pnlSearchResults.Dock = DockStyle.Fill;
      this.pnlSearchResults.Location = new Point(10, 82);
      this.pnlSearchResults.Name = "pnlSearchResults";
      this.pnlSearchResults.Size = new Size(478, 313);
      this.pnlSearchResults.TabIndex = 23;
      this.dgViewSearch.AllowDrop = true;
      this.dgViewSearch.AllowUserToAddRows = false;
      this.dgViewSearch.AllowUserToDeleteRows = false;
      this.dgViewSearch.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.White;
      this.dgViewSearch.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgViewSearch.BackgroundColor = Color.White;
      this.dgViewSearch.BorderStyle = BorderStyle.Fixed3D;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Control;
      gridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.Black;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgViewSearch.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgViewSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgViewSearch.Dock = DockStyle.Fill;
      this.dgViewSearch.Location = new Point(0, 0);
      this.dgViewSearch.Name = "dgViewSearch";
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle3.BackColor = SystemColors.Control;
      gridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle3.ForeColor = Color.Black;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.dgViewSearch.RowHeadersDefaultCellStyle = gridViewCellStyle3;
      this.dgViewSearch.RowHeadersVisible = false;
      this.dgViewSearch.RowHeadersWidth = 32;
      this.dgViewSearch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      gridViewCellStyle4.BackColor = Color.White;
      gridViewCellStyle4.ForeColor = Color.Black;
      gridViewCellStyle4.SelectionBackColor = Color.SteelBlue;
      gridViewCellStyle4.SelectionForeColor = Color.White;
      this.dgViewSearch.RowsDefaultCellStyle = gridViewCellStyle4;
      this.dgViewSearch.RowTemplate.Height = 16;
      this.dgViewSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgViewSearch.ShowRowErrors = false;
      this.dgViewSearch.Size = new Size(478, 313);
      this.dgViewSearch.TabIndex = 4;
      this.dgViewSearch.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.dgViewSearch_CellMouseDoubleClick);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(0, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 24;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.pnlSearch.Controls.Add((Control) this.checkBoxExactMatch);
      this.pnlSearch.Controls.Add((Control) this.checkBoxMatchCase);
      this.pnlSearch.Controls.Add((Control) this.txtPartNumber);
      this.pnlSearch.Dock = DockStyle.Top;
      this.pnlSearch.Location = new Point(10, 27);
      this.pnlSearch.Name = "pnlSearch";
      this.pnlSearch.Size = new Size(478, 55);
      this.pnlSearch.TabIndex = 21;
      this.checkBoxExactMatch.AutoSize = true;
      this.checkBoxExactMatch.Location = new Point(240, 19);
      this.checkBoxExactMatch.Name = "checkBoxExactMatch";
      this.checkBoxExactMatch.Size = new Size(85, 17);
      this.checkBoxExactMatch.TabIndex = 2;
      this.checkBoxExactMatch.Text = "Exact Match";
      this.checkBoxExactMatch.UseVisualStyleBackColor = true;
      this.checkBoxMatchCase.AutoSize = true;
      this.checkBoxMatchCase.Location = new Point(355, 19);
      this.checkBoxMatchCase.Name = "checkBoxMatchCase";
      this.checkBoxMatchCase.Size = new Size(82, 17);
      this.checkBoxMatchCase.TabIndex = 3;
      this.checkBoxMatchCase.Text = "Match Case";
      this.checkBoxMatchCase.UseVisualStyleBackColor = true;
      this.txtPartNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      this.txtPartNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;
      this.txtPartNumber.Location = new Point(4, 17);
      this.txtPartNumber.Name = "txtPartNumber";
      this.txtPartNumber.Size = new Size(200, 21);
      this.txtPartNumber.TabIndex = 1;
      this.txtPartNumber.TextChanged += new EventHandler(this.txtBookPublishingId_TextChanged);
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.ssStatus.BackColor = SystemColors.Control;
      this.ssStatus.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.lblStatus
      });
      this.ssStatus.Location = new Point(0, 428);
      this.ssStatus.Name = "ssStatus";
      this.ssStatus.Size = new Size(500, 22);
      this.ssStatus.TabIndex = 21;
      this.lblStatus.BackColor = SystemColors.Control;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(0, 17);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(500, 450);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.ssStatus);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.KeyPreview = true;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(450, 450);
      this.Name = nameof (frmTextSearch);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Search";
      this.Load += new EventHandler(this.frmTextSearch_Load);
      this.FormClosed += new FormClosedEventHandler(this.frmTextSearch_FormClosed);
      this.FormClosing += new FormClosingEventHandler(this.frmTextSearch_FormClosing);
      this.KeyDown += new KeyEventHandler(this.frmTextSearch_KeyDown);
      this.pnlControl.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.pnlSearchResults.ResumeLayout(false);
      ((ISupportInitialize) this.dgViewSearch).EndInit();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.pnlSearch.ResumeLayout(false);
      this.pnlSearch.PerformLayout();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void frmTextSearch_Load(object sender, EventArgs e)
    {
      Program.ReadSearchHistory(this.frmParent.sSearchHistoryPath, "TEXTSEARCH", Program.objTextSearchHistroyCollection);
      this.txtPartNumber.AutoCompleteCustomSource = Program.objTextSearchHistroyCollection;
      this.txtPartNumber.Focus();
    }

    private void frmTextSearch_FormClosing(object sender, FormClosingEventArgs e)
    {
      Program.WriteSearchHistory(this.frmParent.sSearchHistoryPath, "TEXTSEARCH", Program.objPartNameSearchHistroyCollection);
    }

    private void frmTextSearch_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.frmParent.Enabled = true;
    }

    private void txtBookPublishingId_TextChanged(object sender, EventArgs e)
    {
      if (this.txtPartNumber.Text.Trim().Equals(string.Empty))
      {
        this.btnSearch.Enabled = false;
        this.statusText = string.Empty;
        this.UpdateStatus();
      }
      else
      {
        this.btnSearch.Enabled = true;
        this.searchString = this.txtPartNumber.Text;
      }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      this.txtPartNumber.AutoCompleteCustomSource.Add(this.txtPartNumber.Text);
      this.ShowLoading(this.pnlSearchResults);
      this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
      this.bgWorker.RunWorkerAsync();
    }

    private void btnClearHistory_Click(object sender, EventArgs e)
    {
      this.txtPartNumber.AutoCompleteCustomSource.Clear();
    }

    private void frmTextSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.txtPartNumber.Text.Trim() != string.Empty && e.KeyCode == Keys.Return)
        this.btnSearch_Click((object) null, (EventArgs) null);
      if (e.KeyCode != Keys.Escape)
        return;
      this.frmContainer.Close();
    }

    private void dgViewSearch_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      try
      {
        if (this.dgViewSearch.Rows.Count == 0 || e.RowIndex < 0)
          return;
        Program.DjVuPageNumber = this.dgViewSearch["ImageIndex", this.dgViewSearch.CurrentRow.Index].Value.ToString();
        Program.HighLightText = this.dgViewSearch["Text", this.dgViewSearch.CurrentRow.Index].Value.ToString();
        this.frmParent.OpenTextSearch(this.dgViewSearch["PageName", this.dgViewSearch.CurrentRow.Index].Value.ToString(), this.dgViewSearch["PicIndex", this.dgViewSearch.CurrentRow.Index].Value.ToString(), this.dgViewSearch["Text", this.dgViewSearch.CurrentRow.Index].Value.ToString());
        this.frmParent.Enabled = true;
        if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
          this.frmContainer.CloseContainer();
        Application.DoEvents();
      }
      catch
      {
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.UpdateStatus();
      this.UpdadetControls();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string empty5 = string.Empty;
      string empty6 = string.Empty;
      bool flag = false;
      string str1 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey + "\\" + this.frmParent.BookPublishingId;
      string str2 = str1 + "\\" + this.frmParent.BookPublishingId + "TextSearch.zip";
      string str3 = str1 + "\\" + this.frmParent.BookPublishingId + "TextSearch.xml";
      string str4 = str1 + "\\" + this.frmParent.BookPublishingId + "PDFTextSearch.xml";
      bool bEncrypted = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON";
      bool bCompressed = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON";
      if (!File.Exists(str3))
      {
        if (File.Exists(str2))
        {
          try
          {
            Global.Unzip(str2);
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
        else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str3, bCompressed, bEncrypted), Global.GetServerUpdateDateFromXmlNode(this.frmParent.SchemaNode, this.frmParent.BookNode), result))
          flag = true;
      }
      if (flag && !Program.objAppMode.bWorkingOffline)
      {
        this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        string str5 = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
        string empty7 = string.Empty;
        string empty8 = string.Empty;
        string surl1;
        string sLocalPath;
        if (bCompressed)
        {
          surl1 = str5 + "/" + this.frmParent.BookPublishingId + "/" + this.frmParent.BookPublishingId + "TextSearch.zip";
          sLocalPath = str2;
        }
        else
        {
          surl1 = str5 + "/" + this.frmParent.BookPublishingId + "/" + this.frmParent.BookPublishingId + "TextSearch.xml";
          sLocalPath = str3;
        }
        new Download().DownloadFile(surl1, sLocalPath);
        if (bCompressed && File.Exists(str2))
          Global.Unzip(str2);
      }
      this.GridViewClearRows();
      if (this.sBookType.ToUpper() == "PDF" || this.frmParent.ISPDF)
      {
        if (!File.Exists(str4) || this.frmParent.IsDisposed)
          return;
        this.statusText = "Searching";
        this.UpdateStatus();
        if (this.LoadPDFSearchResultsInGrid(str4))
        {
          this.statusText = this.dgViewSearch.Rows.Count.ToString() + " " + this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
        else
        {
          this.statusText = "No result found";
          this.UpdateStatus();
        }
      }
      else if (File.Exists(str3))
      {
        if (this.LoadSearchResultsInGrid(str3))
        {
          this.statusText = this.dgViewSearch.Rows.Count.ToString() + " " + this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
        else
        {
          this.statusText = "No result found";
          this.UpdateStatus();
        }
      }
      else
      {
        if (File.Exists(str4) || File.Exists(str3) || this.frmParent.IsDisposed)
          return;
        this.statusText = this.frmParent.BookPublishingId + "TextSearch.xml not found";
        this.UpdateStatus();
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.dgViewSearch.Dock = DockStyle.None;
      this.dgViewSearch.Dock = DockStyle.Fill;
      this.HideLoading(this.pnlSearchResults);
      this.UpdadetControls();
    }

    public frmTextSearch(frmViewer frm, frmSearch frmSearch)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.frmContainer = frmSearch;
      this.UpdateFont();
      try
      {
        this.InitializeSearchGrid();
        this.LoadDataGridView();
      }
      catch
      {
      }
      this.LoadResources();
      this.sBookType = this.frmParent.objFrmTreeview.sDataType;
    }

    private void InitializeSearchGrid()
    {
      if (this.dgViewSearch.InvokeRequired)
      {
        this.dgViewSearch.Invoke((Delegate) new frmTextSearch.InitializeSearchGridDelegate(this.InitializeSearchGrid));
      }
      else
      {
        try
        {
          DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn1.Name = "FieldId";
          viewTextBoxColumn1.HeaderText = "Field ID";
          viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn1.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
          DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn2.Name = "DisplayName";
          viewTextBoxColumn2.HeaderText = "Display Name";
          viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn2.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn3.Name = "Width";
          viewTextBoxColumn3.HeaderText = "Col Width";
          viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn3.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
          DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
          viewComboBoxColumn.Name = "Alignment";
          viewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewComboBoxColumn.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewComboBoxColumn);
        }
        catch
        {
        }
      }
    }

    private void LoadDataGridView()
    {
      if (this.dgViewSearch.InvokeRequired)
      {
        this.dgViewSearch.Invoke((Delegate) new frmTextSearch.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
      }
      else
      {
        this.dgViewSearch.Columns.Clear();
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        IniFileIO iniFileIo = new IniFileIO();
        arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "TEXT_SEARCH");
        for (int index = 0; index < keys.Count; ++index)
        {
          try
          {
            Global.AddSearchCol(new IniFileIO().GetKeyValue("TEXT_SEARCH", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini"), keys[index].ToString(), "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
          }
          catch
          {
          }
        }
        if (keys.Count != 0)
          return;
        Global.AddSearchCol("true|Text|C|100", "Text", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
        Global.AddSearchCol("true|Page Name|C|100", "PageName", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
        Global.AddSearchCol("true|DjVu Page Index|C|100", "ImageIndex", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
        Global.AddSearchCol("true|Update Date|C|100", "UpdateDate", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmTextSearch.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = false;
          }
          this.picLoading.Parent = (Control) parentPanel;
          this.picLoading.Left = parentPanel.Width / 2 - this.picLoading.Width / 2;
          this.picLoading.Top = parentPanel.Height / 2 - this.picLoading.Height / 2;
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
          this.Invoke((Delegate) new frmTextSearch.HideLoadingDelegate(this.HideLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = true;
          }
          this.picLoading.Hide();
        }
      }
      catch
      {
      }
    }

    private void GridViewClearRows()
    {
      if (this.dgViewSearch.InvokeRequired)
        this.dgViewSearch.Invoke((Delegate) new frmTextSearch.GridViewClearRowsDelegate(this.GridViewClearRows));
      else
        this.dgViewSearch.Rows.Clear();
    }

    private void GridViewAddRow(DataGridViewRow tempRow)
    {
      if (this.dgViewSearch.InvokeRequired)
        this.dgViewSearch.Invoke((Delegate) new frmTextSearch.GridViewAddRowDelegate(this.GridViewAddRow), (object) tempRow);
      else
        this.dgViewSearch.Rows.Add(tempRow);
    }

    private void UpdadetControls()
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmTextSearch.UpdadetControlsDelegate(this.UpdadetControls));
        }
        else
        {
          this.pnlSearch.Enabled = !this.pnlSearch.Enabled;
          this.pnlControl.Enabled = !this.pnlControl.Enabled;
        }
      }
      catch
      {
      }
    }

    private void LoadResources()
    {
      this.lblTextSearch.Text = this.GetResource("Text Search", "TEXT", ResourceType.LABEL);
      this.checkBoxMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
      this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
      this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
      this.btnClearHistory.Text = this.GetResource("Clear Search History", "CLEAR_SEARCH_HISTORY", ResourceType.BUTTON);
      this.Text = this.GetResource("Text Search", "TEXT_SEARCH", ResourceType.TITLE);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='SEARCH']" + "/Screen[@Name='TEXT_SEARCH']";
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

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblTextSearch.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgViewSearch.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgViewSearch.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    private void UpdateStatus()
    {
      this.lblStatus.Text = this.statusText;
    }

    private bool LoadSearchResultsInGrid(string sFilePath)
    {
      try
      {
        XmlDocument xmlDocument1 = new XmlDocument();
        string sDecryptedInnerXML = string.Empty;
        if (!File.Exists(sFilePath))
          return false;
        try
        {
          xmlDocument1.Load(sFilePath);
        }
        catch
        {
          return false;
        }
        bool bEncrypted = false;
        if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          bEncrypted = true;
        if (bEncrypted)
        {
          sDecryptedInnerXML = new AES().Decode(xmlDocument1.InnerText, "0123456789ABCDEF");
          xmlDocument1.DocumentElement.InnerXml = sDecryptedInnerXML;
        }
        XmlNodeList xmlNodeList1 = xmlDocument1.SelectNodes("//WORD[CHAR]");
        if (xmlNodeList1.Count > 0)
        {
          for (int index = 0; index < xmlNodeList1.Count; ++index)
          {
            string str = "";
            foreach (XmlNode childNode in xmlNodeList1[index].ChildNodes)
              str += childNode.InnerText;
            xmlNodeList1[index].InnerText = str;
          }
        }
        bool hankakuZenkakuFlag = Settings.Default.HankakuZenkakuFlag;
        string[] strArray = this.frmContainer.ConvertStringWidth(this.searchString);
        XmlNodeList xmlNodeList2;
        if (this.checkBoxMatchCase.Checked)
        {
          if (this.checkBoxExactMatch.Checked)
          {
            if (hankakuZenkakuFlag && strArray.Length == 2)
            {
              string xpath = "//WORD[text()=\"" + strArray[0] + "\" or text()=\"" + strArray[1] + "\"]";
              xmlNodeList2 = xmlDocument1.SelectNodes(xpath);
            }
            else
              xmlNodeList2 = xmlDocument1.SelectNodes("//WORD[text()=\"" + this.searchString + "\"]");
          }
          else if (hankakuZenkakuFlag && strArray.Length == 2)
          {
            string xpath = "//WORD[contains(text(),\"" + strArray[0] + "\") or contains(text(),\"" + strArray[1] + "\")]";
            xmlNodeList2 = xmlDocument1.SelectNodes(xpath);
          }
          else
            xmlNodeList2 = xmlDocument1.SelectNodes("//WORD[contains(text(),\"" + this.searchString + "\")]");
        }
        else if (this.checkBoxExactMatch.Checked)
        {
          if (hankakuZenkakuFlag && strArray.Length == 2)
          {
            string xpath = "//WORD[translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + strArray[0].ToUpper() + "\" or translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + strArray[1].ToUpper() + "\"]";
            xmlNodeList2 = xmlDocument1.SelectNodes(xpath);
          }
          else
          {
            xmlNodeList2 = xmlDocument1.SelectNodes("//WORD[translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + this.searchString.ToUpper() + "\"]");
            if (xmlNodeList2 == null || xmlNodeList2.Count == 0)
              xmlNodeList2 = this.SearchWithLINQ(sFilePath, bEncrypted, sDecryptedInnerXML, "", "BODY");
          }
        }
        else if (hankakuZenkakuFlag && strArray.Length == 2)
        {
          string xpath = "//WORD[contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + strArray[0].ToUpper() + "\") or contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + strArray[1].ToUpper() + "\")]";
          xmlNodeList2 = xmlDocument1.SelectNodes(xpath);
        }
        else
        {
          xmlNodeList2 = xmlDocument1.SelectNodes("//WORD[contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + this.searchString.ToUpper() + "\")]");
          if (xmlNodeList2 == null || xmlNodeList2.Count == 0)
            xmlNodeList2 = this.SearchWithLINQ(sFilePath, bEncrypted, sDecryptedInnerXML, "", "BODY");
        }
        foreach (XmlNode xmlNode1 in xmlNodeList2)
        {
          new XmlDocument().LoadXml(xmlNode1.OuterXml);
          DataGridViewRow tempRow = new DataGridViewRow();
          for (int index1 = 0; index1 < this.dgViewSearch.Columns.Count; ++index1)
          {
            DataGridViewTextBoxCell gridViewTextBoxCell = new DataGridViewTextBoxCell();
            XmlNode xmlNode2 = xmlNode1;
            if (xmlNode2.Name.ToUpper() == "BODY")
            {
              xmlNode2 = xmlNode2.SelectSingleNode("//OBJECT");
            }
            else
            {
              while (xmlNode2.ParentNode != null)
              {
                xmlNode2 = xmlNode2.ParentNode;
                if (xmlNode2.ParentNode.Name.ToUpper() == "BODY")
                  break;
              }
            }
            if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("TEXT"))
              gridViewTextBoxCell.Value = (object) xmlNode1.InnerText;
            else if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("COORDS"))
              gridViewTextBoxCell.Value = (object) xmlNode1.Attributes["coords"].Value;
            else if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("PAGENAME"))
            {
              if (xmlNode2.Attributes["PageName"] != null)
                gridViewTextBoxCell.Value = (object) xmlNode2.Attributes["PageName"].Value;
            }
            else if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("PUBLISHINGID"))
            {
              if (xmlNode2.Attributes["PublishingId"] != null)
                gridViewTextBoxCell.Value = (object) xmlNode2.Attributes["PublishingId"].Value;
            }
            else if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("BOOKCODE"))
            {
              if (xmlNode2.Attributes["BookCode"] != null)
                gridViewTextBoxCell.Value = (object) xmlNode2.Attributes["BookCode"].Value;
            }
            else if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("UPDATEDATE"))
            {
              if (xmlNode2.Attributes["UpdateDate"] != null)
                gridViewTextBoxCell.Value = (object) xmlNode2.Attributes["UpdateDate"].Value;
            }
            else if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("IMAGEINDEX"))
            {
              XmlNode xmlNode3 = xmlNode2;
              int num = 0;
              for (; xmlNode3.Name != "BODY"; xmlNode3 = xmlNode3.ParentNode)
              {
                if (xmlNode3.PreviousSibling != null)
                {
                  while (xmlNode3.PreviousSibling.Name == "MAP" || xmlNode3.PreviousSibling.Name == "OBJECT")
                  {
                    if (xmlNode3.PreviousSibling.Name != "MAP")
                      ++num;
                    xmlNode3 = xmlNode3.PreviousSibling;
                    if (xmlNode3.PreviousSibling == null)
                    {
                      ++num;
                      break;
                    }
                  }
                }
                else
                  num = 1;
              }
              gridViewTextBoxCell.Value = (object) num;
            }
            else if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("PICINDEX"))
            {
              gridViewTextBoxCell.Value = (object) "1";
              string empty = string.Empty;
              string str1 = xmlNode2.Attributes["data"].Value;
              string str2 = str1.Substring(str1.LastIndexOf("/") + 1);
              XmlDocument xmlDocument2 = new XmlDocument();
              TreeNode treeNodeByPageName = this.frmParent.objFrmTreeview.FindTreeNodeByPageName(this.frmParent.objFrmTreeview.tvBook.Nodes, xmlNode2.Attributes["PageName"].Value);
              if (treeNodeByPageName != null)
              {
                XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(this.frmParent.objFrmTreeview.tvBook.Tag.ToString()));
                XmlNode xmlNode3 = xmlDocument2.ReadNode((XmlReader) xmlTextReader1);
                string index2 = string.Empty;
                for (int index3 = 0; index3 < xmlNode3.Attributes.Count; ++index3)
                {
                  if (xmlNode3.Attributes[index3].Value.ToUpper().Equals("PICTUREFILE"))
                    index2 = xmlNode3.Attributes[index3].Name;
                }
                XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(treeNodeByPageName.Tag.ToString()));
                XmlNode xmlNode4 = xmlDocument2.ReadNode((XmlReader) xmlTextReader2);
                int num = 1;
                foreach (XmlNode childNode in xmlNode4.ChildNodes)
                {
                  if (childNode.Attributes[index2].Value == str2)
                    gridViewTextBoxCell.Value = (object) num;
                  ++num;
                }
              }
            }
            tempRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell);
          }
          this.GridViewAddRow(tempRow);
          Application.DoEvents();
        }
        return true;
      }
      catch
      {
        return false;
      }
    }

    private bool LoadPDFSearchResultsInGrid(string sFilePath)
    {
      XmlDocument xmlDocument1 = new XmlDocument();
      if (!File.Exists(sFilePath))
        return false;
      try
      {
        xmlDocument1.Load(sFilePath);
      }
      catch
      {
        return false;
      }
      bool flag = false;
      if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
        flag = true;
      if (flag)
      {
        string str = new AES().Decode(xmlDocument1.InnerText, "0123456789ABCDEF");
        xmlDocument1.DocumentElement.InnerXml = str;
      }
      foreach (XmlNode xmlNode1 in !this.checkBoxMatchCase.Checked ? (!this.checkBoxExactMatch.Checked ? xmlDocument1.SelectNodes("//PAGE[contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + this.searchString.ToUpper() + "\")]") : xmlDocument1.SelectNodes("//PDFXML[translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + this.searchString.ToUpper() + "\"]")) : (!this.checkBoxExactMatch.Checked ? xmlDocument1.SelectNodes("//PDFXML[contains(text(),\"" + this.searchString + "\")]") : xmlDocument1.SelectNodes("//PDFXML[text()=\"" + this.searchString + "\"]")))
      {
        new XmlDocument().LoadXml(xmlNode1.OuterXml);
        DataGridViewRow tempRow = new DataGridViewRow();
        for (int index1 = 0; index1 < this.dgViewSearch.Columns.Count; ++index1)
        {
          DataGridViewTextBoxCell gridViewTextBoxCell = new DataGridViewTextBoxCell();
          XmlNode parentNode = xmlNode1.ParentNode;
          if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("TEXT"))
            gridViewTextBoxCell.Value = (object) this.txtPartNumber.Text;
          if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("PAGENAME"))
          {
            if (parentNode.Attributes["pagename"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["pagename"].Value;
            else if (parentNode.Attributes["PageName"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["PageName"].Value;
          }
          if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("PUBLISHINGID"))
          {
            if (parentNode.Attributes["publishingid"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["publishingid"].Value;
            else if (parentNode.Attributes["PublishingId"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["PublishingId"].Value;
          }
          if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("BOOKCODE"))
          {
            if (parentNode.Attributes["bookcode"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["bookcode"].Value;
            else if (parentNode.Attributes["BookCode"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["BookCode"].Value;
          }
          if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("UPDATEDATE"))
          {
            if (parentNode.Attributes["updatedate"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["updatedate"].Value;
            else if (parentNode.Attributes["UpdateDate"] != null)
              gridViewTextBoxCell.Value = (object) parentNode.Attributes["UpdateDate"].Value;
          }
          if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("IMAGEINDEX"))
            gridViewTextBoxCell.Value = (object) xmlNode1.Attributes["number"].Value;
          if (this.dgViewSearch.Columns[index1].Name.ToUpper().Equals("PICINDEX"))
          {
            gridViewTextBoxCell.Value = (object) "1";
            string empty = string.Empty;
            string str1 = parentNode.Attributes["data"].Value;
            string str2 = str1.Substring(str1.LastIndexOf("/") + 1);
            XmlDocument xmlDocument2 = new XmlDocument();
            TreeNode treeNodeByPageName = this.frmParent.objFrmTreeview.FindTreeNodeByPageName(this.frmParent.objFrmTreeview.tvBook.Nodes, parentNode.Attributes["pagename"].Value);
            if (treeNodeByPageName != null)
            {
              XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(this.frmParent.objFrmTreeview.tvBook.Tag.ToString()));
              XmlNode xmlNode2 = xmlDocument2.ReadNode((XmlReader) xmlTextReader1);
              string index2 = string.Empty;
              for (int index3 = 0; index3 < xmlNode2.Attributes.Count; ++index3)
              {
                if (xmlNode2.Attributes[index3].Value.ToUpper().Equals("PICTUREFILE"))
                  index2 = xmlNode2.Attributes[index3].Name;
              }
              XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(treeNodeByPageName.Tag.ToString()));
              XmlNode xmlNode3 = xmlDocument2.ReadNode((XmlReader) xmlTextReader2);
              int num = 1;
              foreach (XmlNode childNode in xmlNode3.ChildNodes)
              {
                if (childNode.Attributes[index2].Value == str2)
                  gridViewTextBoxCell.Value = (object) num;
                ++num;
              }
            }
          }
          tempRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell);
        }
        this.GridViewAddRow(tempRow);
        Application.DoEvents();
      }
      return true;
    }

    private XmlNodeList SearchWithLINQ(string XmlFilePath, bool bEncrypted, string sDecryptedInnerXML, string attIDElement, string searchType)
    {
      try
      {
        XElement xelement1;
        if (bEncrypted)
        {
          sDecryptedInnerXML = "<Parent>" + sDecryptedInnerXML + "</Parent>";
          xelement1 = XElement.Parse(sDecryptedInnerXML);
        }
        else
          xelement1 = XElement.Load(XmlFilePath);
        IEnumerable<XElement> xelements = !this.checkBoxExactMatch.Checked ? xelement1.DescendantsAndSelf((XName) searchType).Where<XElement>((Func<XElement, bool>) (h => h.Value.ToUpper().Contains(this.searchString.ToUpper()))) : xelement1.DescendantsAndSelf((XName) searchType).Where<XElement>((Func<XElement, bool>) (h => h.Value.ToUpper() == this.searchString.ToUpper()));
        XmlDocument xmlDocument = new XmlDocument();
        string str = string.Empty + "<Parent>";
        foreach (XElement xelement2 in xelements)
          str = str + xelement2.ToString() + "\n";
        string xml = str + "</Parent>";
        xmlDocument.LoadXml(xml);
        return xmlDocument.SelectNodes("//" + searchType);
      }
      catch (Exception ex)
      {
        return (XmlNodeList) null;
      }
    }

    private delegate void InitializeSearchGridDelegate();

    private delegate void LoadDataGridViewXmlDelegate();

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void GridViewClearRowsDelegate();

    private delegate void GridViewAddRowDelegate(DataGridViewRow tempRow);

    private delegate void UpdadetControlsDelegate();
  }
}

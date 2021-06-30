// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmPageNameSearch
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
  public class frmPageNameSearch : Form
  {
    private IContainer components;
    private Label lblPageName;
    private Panel pnlControl;
    private Button btnSearch;
    private Panel pnlForm;
    private DataGridView dgViewSearch;
    private Panel pnlSearch;
    private TextBox txtPageName;
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
      this.lblPageName = new Label();
      this.pnlControl = new Panel();
      this.btnClearHistory = new Button();
      this.btnSearch = new Button();
      this.pnlForm = new Panel();
      this.pnlSearchResults = new Panel();
      this.dgViewSearch = new DataGridView();
      this.pnlSearch = new Panel();
      this.checkBoxExactMatch = new CheckBox();
      this.checkBoxMatchCase = new CheckBox();
      this.txtPageName = new TextBox();
      this.picLoading = new PictureBox();
      this.bgWorker = new BackgroundWorker();
      this.ssStatus = new StatusStrip();
      this.lblStatus = new ToolStripStatusLabel();
      this.pnlControl.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.pnlSearchResults.SuspendLayout();
      ((ISupportInitialize) this.dgViewSearch).BeginInit();
      this.pnlSearch.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.ssStatus.SuspendLayout();
      this.SuspendLayout();
      this.lblPageName.BackColor = Color.White;
      this.lblPageName.Dock = DockStyle.Top;
      this.lblPageName.ForeColor = Color.Black;
      this.lblPageName.Location = new Point(10, 0);
      this.lblPageName.Name = "lblPageName";
      this.lblPageName.Padding = new Padding(0, 7, 0, 0);
      this.lblPageName.Size = new Size(478, 27);
      this.lblPageName.TabIndex = 16;
      this.lblPageName.Text = "Page Name";
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
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Controls.Add((Control) this.lblPageName);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(10, 0, 10, 0);
      this.pnlForm.Size = new Size(500, 428);
      this.pnlForm.TabIndex = 19;
      this.pnlSearchResults.Controls.Add((Control) this.dgViewSearch);
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
      this.pnlSearch.Controls.Add((Control) this.checkBoxExactMatch);
      this.pnlSearch.Controls.Add((Control) this.checkBoxMatchCase);
      this.pnlSearch.Controls.Add((Control) this.txtPageName);
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
      this.txtPageName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      this.txtPageName.AutoCompleteSource = AutoCompleteSource.CustomSource;
      this.txtPageName.Location = new Point(4, 17);
      this.txtPageName.Name = "txtPageName";
      this.txtPageName.Size = new Size(200, 21);
      this.txtPageName.TabIndex = 1;
      this.txtPageName.TextChanged += new EventHandler(this.txtPageName_TextChanged);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(379, 246);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 24;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
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
      this.MinimumSize = new Size(500, 450);
      this.Name = nameof (frmPageNameSearch);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Search";
      this.Load += new EventHandler(this.frmPageNameSearch_Load);
      this.FormClosed += new FormClosedEventHandler(this.frmPageNameSearch_FormClosed);
      this.FormClosing += new FormClosingEventHandler(this.frmPageNameSearch_FormClosing);
      this.KeyDown += new KeyEventHandler(this.frmPageNameSearch_KeyDown);
      this.pnlControl.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.pnlSearchResults.ResumeLayout(false);
      ((ISupportInitialize) this.dgViewSearch).EndInit();
      this.pnlSearch.ResumeLayout(false);
      this.pnlSearch.PerformLayout();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void frmPageNameSearch_Load(object sender, EventArgs e)
    {
      Program.ReadSearchHistory(this.frmParent.sSearchHistoryPath, "PAGENAMESEARCH", Program.objPageNameSearchHistroyCollection);
      this.txtPageName.AutoCompleteCustomSource = Program.objPageNameSearchHistroyCollection;
      this.txtPageName.Focus();
    }

    private void frmPageNameSearch_FormClosing(object sender, FormClosingEventArgs e)
    {
      Program.WriteSearchHistory(this.frmParent.sSearchHistoryPath, "PAGENAMESEARCH", Program.objPartNameSearchHistroyCollection);
    }

    private void frmPageNameSearch_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.frmParent.Enabled = true;
    }

    private void txtPageName_TextChanged(object sender, EventArgs e)
    {
      if (this.txtPageName.Text.Trim().Equals(string.Empty))
      {
        this.btnSearch.Enabled = false;
      }
      else
      {
        this.btnSearch.Enabled = true;
        this.searchString = this.txtPageName.Text;
      }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      this.txtPageName.AutoCompleteCustomSource.Add(this.txtPageName.Text);
      this.ShowLoading(this.pnlSearchResults);
      this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
      this.bgWorker.RunWorkerAsync();
    }

    private void btnClearHistory_Click(object sender, EventArgs e)
    {
      this.txtPageName.AutoCompleteCustomSource.Clear();
    }

    private void frmPageNameSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.txtPageName.Text.Trim() != string.Empty && e.KeyCode == Keys.Return)
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
        string empty = string.Empty;
        if (this.dgViewSearch.CurrentRow.Tag != null)
          empty = this.dgViewSearch.CurrentRow.Tag.ToString();
        this.frmParent.OpenSearch(Program.iniServers[this.frmParent.ServerId].sIniKey, this.frmParent.BookPublishingId, empty, "1", "1", "");
        this.frmParent.Enabled = true;
        this.frmContainer.CloseContainer();
      }
      catch
      {
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      string str = string.Empty;
      this.UpdateStatus();
      this.UpdadetControls();
      try
      {
        str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
        str = str + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey;
        str = str + "\\" + this.frmParent.BookPublishingId;
        if (!Directory.Exists(str))
          Directory.CreateDirectory(str);
        str = str + "\\" + this.frmParent.BookPublishingId + ".xml";
      }
      catch
      {
        MessageHandler.ShowError1(this.GetResource("(E-PGS-EM002) Failed to create file/folder specified", "(E-PGS-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
      }
      this.GridViewClearRows();
      if (File.Exists(str))
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (this.LoadSearchResultsInGrid(str))
        {
          this.statusText = this.dgViewSearch.Rows.Count.ToString() + " " + this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
        else
        {
          this.statusText = this.GetResource("No result found", "NO_RESULT", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
      }
      else
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.frmParent.BookPublishingId + this.GetResource("(E-PGS-EM001) Specified information does not exist", "(E-PGS-EM001)_NO_INFORMATION", ResourceType.STATUS_MESSAGE);
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

    public frmPageNameSearch(frmViewer frm, frmSearch frmSearch)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.frmContainer = frmSearch;
      this.UpdateFont();
      this.InitializeSearchGrid();
      this.LoadDataGridView();
      this.LoadResources();
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmPageNameSearch.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
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
          this.Invoke((Delegate) new frmPageNameSearch.HideLoadingDelegate(this.HideLoading), (object) this.pnlForm);
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
        this.dgViewSearch.Invoke((Delegate) new frmPageNameSearch.GridViewClearRowsDelegate(this.GridViewClearRows));
      else
        this.dgViewSearch.Rows.Clear();
    }

    private void GridViewAddRow(DataGridViewRow tempRow)
    {
      if (this.dgViewSearch.InvokeRequired)
        this.dgViewSearch.Invoke((Delegate) new frmPageNameSearch.GridViewAddRowDelegate(this.GridViewAddRow), (object) tempRow);
      else
        this.dgViewSearch.Rows.Add(tempRow);
    }

    private void InitializeSearchGrid()
    {
      if (this.dgViewSearch.InvokeRequired)
      {
        this.dgViewSearch.Invoke((Delegate) new frmPageNameSearch.InitializeSearchGridDelegate(this.InitializeSearchGrid));
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
        this.dgViewSearch.Invoke((Delegate) new frmPageNameSearch.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
      }
      else
      {
        this.dgViewSearch.Columns.Clear();
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        IniFileIO iniFileIo = new IniFileIO();
        arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PAGENAME_SEARCH");
        for (int index = 0; index < keys.Count; ++index)
        {
          try
          {
            Global.AddSearchCol(new IniFileIO().GetKeyValue("PAGENAME_SEARCH", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini"), keys[index].ToString(), "PAGENAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
          }
          catch
          {
          }
        }
        if (keys.Count != 0)
          return;
        Global.AddSearchCol("true|Page Name|C|91", "PageName", "PAGENAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
        Global.AddSearchCol("true|Picture File|C|91", "PictureFile", "PAGENAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
      }
    }

    private void LoadResources()
    {
      this.lblPageName.Text = this.GetResource("Page Name", "PAGE_NAME", ResourceType.LABEL);
      this.checkBoxMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
      this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
      this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
      this.btnClearHistory.Text = this.GetResource("Clear Search History", "CLEAR_SEARCH_HISTORY", ResourceType.BUTTON);
      this.Text = this.GetResource("Search", "PAGE_NAME_SEARCH", ResourceType.TITLE);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='SEARCH']" + "/Screen[@Name='PAGE_NAME_SEARCH']";
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
        XmlNode xSchemaNode = xmlDocument1.SelectSingleNode("//Schema");
        if (xSchemaNode == null)
          return false;
        string index1 = "";
        string attIDElement = "";
        string index2 = "";
        string index3 = "";
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("ID"))
            index1 = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PAGENAME"))
            attIDElement = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
            index2 = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
            index3 = attribute.Name;
        }
        if (index1 == "" || attIDElement == "")
          return false;
        GSPcLocalViewer.Classes.Filter filter = new GSPcLocalViewer.Classes.Filter(this.frmParent);
        bool hankakuZenkakuFlag = Settings.Default.HankakuZenkakuFlag;
        string[] strArray = this.frmContainer.ConvertStringWidth(this.searchString);
        XmlNodeList xmlNodeList;
        if (this.checkBoxMatchCase.Checked)
        {
          if (this.checkBoxExactMatch.Checked)
          {
            if (hankakuZenkakuFlag && strArray.Length == 2)
            {
              string xpath = "//Pg[@" + attIDElement + "=\"" + strArray[0] + "\" or @" + attIDElement + "=\"" + strArray[1] + "\"]";
              xmlNodeList = xmlDocument1.SelectNodes(xpath);
            }
            else
              xmlNodeList = xmlDocument1.SelectNodes("//Pg[@" + attIDElement + "=\"" + this.searchString + "\"]");
          }
          else if (hankakuZenkakuFlag && strArray.Length == 2)
          {
            string xpath = "//Pg[contains(@" + attIDElement + ",\"" + strArray[0] + "\") or contains(@" + attIDElement + ",\"" + strArray[1] + "\")]";
            xmlNodeList = xmlDocument1.SelectNodes(xpath);
          }
          else
            xmlNodeList = xmlDocument1.SelectNodes("//Pg[contains(@" + attIDElement + ",\"" + this.searchString + "\")]");
        }
        else if (this.checkBoxExactMatch.Checked)
        {
          if (hankakuZenkakuFlag && strArray.Length == 2)
          {
            string xpath = "//Pg[translate(@" + attIDElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + strArray[0].ToUpper() + "\" or translate(@" + attIDElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + strArray[1].ToUpper() + "\"]";
            xmlNodeList = xmlDocument1.SelectNodes(xpath);
          }
          else
          {
            xmlNodeList = xmlDocument1.SelectNodes("//Pg[translate(@" + attIDElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + this.searchString.ToUpper() + "\"]");
            if (xmlNodeList == null || xmlNodeList.Count == 0)
              xmlNodeList = this.SearchWithLINQ(sFilePath, bEncrypted, sDecryptedInnerXML, attIDElement, "Pg");
          }
        }
        else if (hankakuZenkakuFlag && strArray.Length == 2)
        {
          string xpath = "//Pg[contains(translate(@" + attIDElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + strArray[0].ToUpper() + "\") or contains(translate(@" + attIDElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + strArray[1].ToUpper() + "\")]";
          xmlNodeList = xmlDocument1.SelectNodes(xpath);
        }
        else
        {
          xmlNodeList = xmlDocument1.SelectNodes("//Pg[contains(translate(@" + attIDElement + ",'abcdefghijklmnopqrstuvwxyzççğıöşü','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + this.searchString.ToUpper() + "\")]");
          if (xmlNodeList == null || xmlNodeList.Count == 0)
            xmlNodeList = this.SearchWithLINQ(sFilePath, bEncrypted, sDecryptedInnerXML, attIDElement, "Pg");
        }
        foreach (XmlNode xNode in xmlNodeList)
        {
          if (filter.FilterPage(xSchemaNode, xNode).Attributes.Count > 0)
          {
            string str = xNode.Attributes[attIDElement].Value;
            XmlDocument xmlDocument2 = new XmlDocument();
            if (xNode.OuterXml.ToUpper().IndexOf("<PG", 3) > 0)
              xmlDocument2.LoadXml(xNode.OuterXml.Substring(0, xNode.OuterXml.IndexOf("<Pg", 3)) + "</Pg>");
            else
              xmlDocument2.LoadXml(xNode.OuterXml);
            XmlNode xmlNode = xmlDocument2.SelectSingleNode("//Pic");
            DataGridViewRow tempRow = new DataGridViewRow();
            for (int index4 = 0; index4 < this.dgViewSearch.Columns.Count; ++index4)
            {
              DataGridViewTextBoxCell gridViewTextBoxCell = new DataGridViewTextBoxCell();
              if (this.dgViewSearch.Columns[index4].Name.ToUpper().Equals("PAGENAME"))
              {
                if (xNode.Attributes[attIDElement] != null)
                  gridViewTextBoxCell.Value = (object) xNode.Attributes[attIDElement].Value;
              }
              else if (this.dgViewSearch.Columns[index4].Name.ToUpper().Equals("ID"))
              {
                if (xNode.Attributes[index1] != null)
                  gridViewTextBoxCell.Value = (object) xNode.Attributes[index1].Value;
              }
              else if (this.dgViewSearch.Columns[index4].Name.ToUpper().Equals("PICTUREFILE"))
              {
                if (xmlNode != null && xmlNode.Attributes[index2] != null)
                  gridViewTextBoxCell.Value = (object) xmlNode.Attributes[index2].Value;
              }
              else if (this.dgViewSearch.Columns[index4].Name.ToUpper().Equals("PARTSLISTFILE") && xmlNode != null && xmlNode.Attributes[index3] != null)
                gridViewTextBoxCell.Value = (object) xmlNode.Attributes[index3].Value;
              tempRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell);
            }
            DataGridViewTextBoxCell gridViewTextBoxCell1 = new DataGridViewTextBoxCell();
            if (xNode.Attributes[index1] != null && !this.frmParent.lstFilteredPages.Contains(str))
            {
              tempRow.Tag = (object) xNode.Attributes[index1].Value;
              this.GridViewAddRow(tempRow);
            }
            Application.DoEvents();
          }
        }
        return true;
      }
      catch
      {
        return false;
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblPageName.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgViewSearch.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgViewSearch.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    private void UpdateStatus()
    {
      this.lblStatus.Text = this.statusText;
    }

    private void UpdadetControls()
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
          this.Invoke((Delegate) new frmPageNameSearch.UpdadetControlsDelegate(this.UpdadetControls));
        else
          this.pnlForm.Enabled = !this.pnlForm.Enabled;
      }
      catch
      {
      }
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
        IEnumerable<XElement> xelements = !this.checkBoxExactMatch.Checked ? xelement1.DescendantsAndSelf((XName) searchType).Where<XElement>((Func<XElement, bool>) (h => h.Attribute((XName) attIDElement).Value.ToUpper().Contains(this.searchString.ToUpper()))) : xelement1.DescendantsAndSelf((XName) searchType).Where<XElement>((Func<XElement, bool>) (h => h.Attribute((XName) attIDElement).Value.ToUpper() == this.searchString.ToUpper()));
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

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void GridViewClearRowsDelegate();

    private delegate void GridViewAddRowDelegate(DataGridViewRow tempRow);

    private delegate void InitializeSearchGridDelegate();

    private delegate void LoadDataGridViewXmlDelegate();

    private delegate void UpdadetControlsDelegate();
  }
}

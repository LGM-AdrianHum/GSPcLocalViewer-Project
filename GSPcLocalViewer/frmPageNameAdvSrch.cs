// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmPageNameAdvSrch
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using GSPcLocalViewer.ServerSearch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmPageNameAdvSrch : Form
  {
    private frmOpenBook frmParent;
    private string statusText;
    private int p_ServerId;
    private Thread thGetResults;
    private bool bFormClosing;
    private string sServerKey;
    private Download objDownloader;
    private IContainer components;
    private SplitContainer pnlForm;
    private Panel pnltvSearch;
    private Label lblSearch;
    private Panel pnlDetails;
    private Panel pnlSearchResults;
    private Label lblDetails;
    private BackgroundWorker bgWorker;
    private Panel pnlSearchCriteria;
    private DataGridView dgvSearchResults;
    private Panel pnlControl;
    private Button btnSearch;
    private CheckBox chkMatchCase;
    private Panel pnlrtbNoDetails;
    private RichTextBox rtbNoDetails;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private BackgroundWorker bgLoader;
    private LabledTextBox ltbKeyword;
    private Panel pnlBooks;
    private PictureBox picLoading;
    private CustomComboBox cmbServers;
    private DataGridView dgvBooks;
    private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private Panel pnlLoading;
    public CheckBox chkExactMatch;

    public frmPageNameAdvSrch(frmOpenBook frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.statusText = this.GetResource("Search pages on server", "SEARCH_PAGES", ResourceType.STATUS_MESSAGE);
      this.cmbServers.Items.Add((object) new Global.ComboBoxItem(this.GetResource("Select a server…", "SELECT_SERVER", ResourceType.LABEL), (object) null));
      this.cmbServers.SelectedItem = this.cmbServers.Items[0];
      this.bFormClosing = false;
      this.p_ServerId = 99999;
      this.objDownloader = new Download(this.frmParent);
      this.UpdateFont();
      this.LoadResources();
      this.pnlLoading.Visible = false;
      this.thGetResults = new Thread(new ParameterizedThreadStart(this.GetResults));
      this.btnSearch.Tag = (object) false;
    }

    private void frmPageNameAdvSrch_Load(object sender, EventArgs e)
    {
      try
      {
        this.cmbServers.BeginUpdate();
        for (int index = 0; index < Program.iniServers.Length; ++index)
        {
          string contents = Program.iniServers[index].items["SETTINGS", "DISPLAY_NAME"];
          object tag = (object) index;
          if (contents != string.Empty)
            this.cmbServers.Items.Add((object) new Global.ComboBoxItem(contents, tag));
        }
        this.cmbServers.EndUpdate();
      }
      catch
      {
        this.Hide();
        int num = (int) MessageHandler.ShowMessage((IWin32Window) this.frmParent, this.GetResource("(E-TV-LD001) Failed to load specified object.", "(E-TV-LD001)_FAILED_LOAD", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.Show();
      }
      this.ShowResultMessage(this.GetResource("Search any books", "SEARCH_ANY_BOOKS", ResourceType.LABEL));
      try
      {
        if (this.cmbServers.Items.Count != 2)
          return;
        this.cmbServers.SelectedIndex = 1;
        this.cmbServers.Enabled = false;
      }
      catch
      {
      }
    }

    private void frmPageNameAdvSrch_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.bFormClosing = true;
      if (!this.thGetResults.IsAlive)
        return;
      this.thGetResults.Interrupt();
    }

    private void frmPageNameAdvSrch_VisibleChanged(object sender, EventArgs e)
    {
      this.UpdateStatus();
    }

    private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.statusText = this.GetResource("Search pages on server", "SEARCH_PAGES", ResourceType.STATUS_MESSAGE);
      this.UpdateStatus();
      Global.ComboBoxItem selectedItem = (Global.ComboBoxItem) this.cmbServers.SelectedItem;
      int result = 99999;
      if (selectedItem != null && selectedItem.Tag != null)
        int.TryParse(selectedItem.Tag.ToString(), out result);
      this.p_ServerId = result;
      this.HideLoading(this.pnlSearchResults);
      this.ShowResultMessage(this.GetResource("Search any books", "SEARCH_ANY_BOOKS", ResourceType.STATUS_MESSAGE));
      this.dgvBooks.Rows.Clear();
      this.dgvSearchResults.Rows.Clear();
      this.pnlBooks.SuspendLayout();
      if (((ListControl) sender).SelectedIndex > 0)
      {
        if (this.p_ServerId != 99999 && Program.iniServers[this.p_ServerId].items["SETTINGS", "SERVICE_PATH"] != null)
        {
          this.ShowHideSearchControls(true);
          this.LoadBooks();
        }
        else
        {
          this.ShowHideSearchControls(false);
          this.ShowResultMessage(this.GetResource("Web service path not found", "WEBSERVICE_NOT_FOUND", ResourceType.LABEL));
        }
      }
      else
        this.ShowHideSearchControls(false);
      this.pnlBooks.ResumeLayout();
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      bool result = false;
      bool.TryParse(this.btnSearch.Tag.ToString(), out result);
      if (!result)
      {
        this.statusText = this.GetResource("Search pages on server", "SEARCH_PAGES", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (this.ltbKeyword._Text.Trim() != string.Empty)
        {
          this.EnableDisableSearchControls(false);
          this.ChangeSearchButtonText(this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON), true);
          this.dgvSearchResults.Rows.Clear();
          this.ShowLoading(this.pnlLoading);
          this.pnlrtbNoDetails.SendToBack();
          if (this.thGetResults.IsAlive)
            return;
          this.thGetResults = new Thread(new ParameterizedThreadStart(this.GetResults));
          this.thGetResults.Start();
        }
        else
          this.ShowResultMessage(this.GetResource("Enter page name to search", "ENTER_PAGENAME", ResourceType.LABEL));
      }
      else
      {
        if (!this.thGetResults.IsAlive)
          return;
        this.thGetResults.Interrupt();
      }
    }

    private void chkBooksHeader_OnCheckBoxClicked(bool state)
    {
      this.dgvBooks.CellValueChanged -= new DataGridViewCellEventHandler(this.dgvBooks_CellValueChanged);
      this.dgvBooks.BeginEdit(true);
      if (this.dgvBooks.Columns.Count > 0)
      {
        foreach (DataGridViewRow row in (IEnumerable) this.dgvBooks.Rows)
        {
          if (row.Cells[0] is DataGridViewCheckBoxCell)
          {
            try
            {
              if (Convert.ToBoolean(row.Cells[0].Value) != state)
                row.Cells["CHK"].Value = (object) state;
            }
            catch
            {
            }
          }
        }
      }
      this.dgvBooks.EndEdit();
      this.dgvBooks.CellValueChanged += new DataGridViewCellEventHandler(this.dgvBooks_CellValueChanged);
    }

    private void dgvBooks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        string empty = string.Empty;
        if (this.dgvBooks.Columns.Count <= 0 || e.RowIndex == -1 || e.ColumnIndex != this.dgvBooks.Columns["CHK"].Index)
          return;
        int index = 0;
        while (index < this.dgvBooks.Rows.Count && (!(this.dgvBooks.Rows[index].Cells[0] is DataGridViewCheckBoxCell) || (bool) this.dgvBooks.Rows[index].Cells["CHK"].Value))
          ++index;
        DatagridViewCheckBoxHeaderCell headerCell = (DatagridViewCheckBoxHeaderCell) this.dgvBooks.Columns[0].HeaderCell;
        if (index < this.dgvBooks.Rows.Count)
          headerCell.Checked = false;
        else
          headerCell.Checked = true;
      }
      catch
      {
      }
    }

    private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex == -1 || this.dgvBooks.Columns["CHK"] == null)
        return;
      if (e.ColumnIndex != this.dgvBooks.Columns["CHK"].Index)
        return;
      try
      {
        if (this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
          this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object) true;
        else if (!bool.Parse(this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
        {
          this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object) true;
        }
        else
        {
          if (!bool.Parse(this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
            return;
          this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (object) false;
        }
      }
      catch
      {
      }
    }

    private void dgvBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.RowIndex == -1)
          return;
        if (this.dgvBooks.CurrentRow.Cells["CHK"].Value == null || this.dgvBooks.CurrentRow.Cells["CHK"].Value.ToString().ToUpper() == "FALSE")
          this.dgvBooks.CurrentRow.Cells["CHK"].Value = (object) true;
        else
          this.dgvBooks.CurrentRow.Cells["CHK"].Value = (object) false;
      }
      catch
      {
      }
    }

    private void dgvSearchResults_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if (this.dgvSearchResults.Rows.Count == 0 || e.RowIndex < 0)
        return;
      this.bFormClosing = true;
      if (this.thGetResults.IsAlive)
        this.thGetResults.Interrupt();
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        string empty5 = string.Empty;
        string empty6 = string.Empty;
        Hashtable tag = (Hashtable) this.dgvSearchResults.Tag;
        string sIniKey = Program.iniServers[this.p_ServerId].sIniKey;
        if (tag.Contains((object) "BookCode"))
        {
          empty6 = tag[(object) "BookCode"].ToString();
          if (this.dgvSearchResults.Columns.Contains(empty6))
            empty2 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty6].Value.ToString();
        }
        if (tag.Contains((object) "PageId"))
        {
          empty6 = tag[(object) "PageId"].ToString();
          if (this.dgvSearchResults.Columns.Contains(empty6))
            empty3 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty6].Value.ToString();
        }
        if (tag.Contains((object) "PicIndex"))
        {
          empty6 = tag[(object) "PicIndex"].ToString();
          if (this.dgvSearchResults.Columns.Contains(empty6))
            empty4 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty6].Value.ToString();
        }
        if (tag.Contains((object) "ListIndex"))
        {
          empty6 = tag[(object) "ListIndex"].ToString();
          if (this.dgvSearchResults.Columns.Contains(empty6))
            empty5 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty6].Value.ToString();
        }
        if (sIniKey != string.Empty && empty2 != string.Empty && (empty3 != string.Empty && empty4 != string.Empty) && (empty5 != string.Empty && empty6 != string.Empty))
        {
          this.frmParent.CloseAndLoadSearch(sIniKey, empty2, empty3, empty4, empty5, string.Empty);
        }
        else
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this.frmParent, this.GetResource("(E-RG-OP001) Cannot open book.", "(E-RG-OP001)_NO_BOOK", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      catch
      {
        int num = (int) MessageHandler.ShowMessage((IWin32Window) this.frmParent, this.GetResource("(E-RG-OP002) Cannot open book.", "(E-RG-OP002)_BOOK", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      object[] objArray = (object[]) e.Argument;
      string surl1_1 = (string) objArray[0] + "Series.xml";
      string str1 = (string) objArray[1] + "\\Series.xml";
      string surl1_2 = (string) objArray[0] + "DataUpdate.xml";
      string str2 = (string) objArray[1] + "\\DataUpdate.xml";
      Global.ComboBoxItem comboBoxItem = (Global.ComboBoxItem) objArray[2];
      bool flag = false;
      this.statusText = this.GetResource("Checking for data updates", "CHECKING_UPDATES", ResourceType.STATUS_MESSAGE);
      this.UpdateStatus();
      int result = 0;
      if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
        result = 0;
      this.objDownloader.DownloadFile(surl1_2, str2);
      if (System.IO.File.Exists(str1))
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
      if (flag)
      {
        this.statusText = this.GetResource("Downloading Series.xml", "DOWNLOADING_SERIES", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (!this.objDownloader.DownloadFile(surl1_1, str1) && !this.frmParent.IsDisposed)
        {
          this.statusText = this.GetResource("Series.xml could not be downloaded", "NO_SERIES", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          e.Result = (object) false;
        }
      }
      if (System.IO.File.Exists(str1))
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.GetResource("Loading Series.xml", "LOADING_SERIES", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (this.LoadBooksInGrid(str1))
        {
          this.statusText = this.GetResource("Series.xml loaded completely", "SERIES_LOADED", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          e.Result = (object) true;
        }
        else
        {
          this.statusText = this.GetResource("Series.xml could not be loaded", "SERIES_NOT_LOADED", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          e.Result = (object) false;
        }
      }
      else
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.GetResource("Specified information does not exist", "NO_INFORMATION", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        e.Result = (object) false;
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.HideLoading(this.pnlSearchCriteria);
      if (this.cmbServers.Items.Count != 2)
        this.cmbServers.Enabled = true;
      this.btnSearch.Enabled = true;
    }

    private void EnableDisableSearchControls(bool value)
    {
      if (this.pnlSearchCriteria.InvokeRequired)
      {
        this.pnlSearchCriteria.Invoke((Delegate) new frmPageNameAdvSrch.EnableDisableSearchControlsDelegate(this.EnableDisableSearchControls), (object) value);
      }
      else
      {
        if (this.cmbServers.Items.Count != 2)
          this.cmbServers.Enabled = value;
        this.chkExactMatch.Enabled = value;
        this.chkMatchCase.Enabled = value;
        this.dgvBooks.Enabled = value;
        this.ltbKeyword.Enabled = value;
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (parentPanel.InvokeRequired)
        {
          parentPanel.Invoke((Delegate) new frmPageNameAdvSrch.ShowLoadingDelegate(this.ShowLoading), (object) parentPanel);
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
          if (parentPanel.Tag == null || !(parentPanel.Tag.ToString() == "loading"))
            return;
          parentPanel.Show();
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
          parentPanel.Invoke((Delegate) new frmPageNameAdvSrch.HideLoadingDelegate(this.HideLoading), (object) parentPanel);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Visible = true;
          }
          this.picLoading.Hide();
          this.picLoading.Parent = (Control) this;
          if (parentPanel.Tag == null || !(parentPanel.Tag.ToString() == "loading"))
            return;
          parentPanel.Hide();
        }
      }
      catch
      {
      }
    }

    private void UpdateStatus()
    {
      if (this != this.frmParent.GetCurrentChildForm())
        return;
      if (this.frmParent.InvokeRequired)
        this.frmParent.Invoke((Delegate) new frmPageNameAdvSrch.StatusDelegate(this.frmParent.UpdateStatus), (object) this.statusText);
      else
        this.frmParent.UpdateStatus(this.statusText);
    }

    private void AddRowToBooksGrid()
    {
      if (this.dgvBooks.InvokeRequired)
      {
        this.dgvBooks.Invoke((Delegate) new frmPageNameAdvSrch.AddRowToBooksGridDelegate(this.AddRowToBooksGrid));
      }
      else
      {
        this.dgvBooks.Rows.Add();
        if (this.dgvBooks.Rows.Count != 1)
          return;
        this.dgvBooks.Rows[0].Selected = false;
      }
    }

    private void AddRowToResultsGrid()
    {
      if (this.dgvSearchResults.InvokeRequired)
      {
        this.dgvSearchResults.Invoke((Delegate) new frmPageNameAdvSrch.AddRowToResultsGridDelegate(this.AddRowToResultsGrid));
      }
      else
      {
        this.dgvSearchResults.Rows.Add();
        if (this.dgvSearchResults.Rows.Count != 1)
          return;
        this.dgvSearchResults.Rows[0].Selected = false;
      }
    }

    private void ChangeSearchButtonText(string caption, bool isSearching)
    {
      if (this.btnSearch.InvokeRequired)
      {
        this.btnSearch.Invoke((Delegate) new frmPageNameAdvSrch.ChangeSearchButtonTextDelegate(this.ChangeSearchButtonText), (object) caption, (object) isSearching);
      }
      else
      {
        this.btnSearch.Text = caption;
        this.btnSearch.Tag = (object) isSearching;
      }
    }

    private void ShowResultMessage(string msg)
    {
      if (this.pnlrtbNoDetails.InvokeRequired)
      {
        this.pnlrtbNoDetails.Invoke((Delegate) new frmPageNameAdvSrch.ShowResultMessageDelegate(this.ShowResultMessage), (object) msg);
      }
      else
      {
        this.pnlrtbNoDetails.BringToFront();
        this.rtbNoDetails.Clear();
        this.rtbNoDetails.SelectionColor = Color.Gray;
        if (msg == null || !(msg != string.Empty))
          return;
        this.rtbNoDetails.SelectedText = msg;
      }
    }

    private void ShowResultGrid()
    {
      if (this.pnlSearchResults.InvokeRequired)
        this.pnlSearchResults.Invoke((Delegate) new frmPageNameAdvSrch.ShowResultGridDelegate(this.ShowResultGrid));
      else
        this.pnlSearchResults.BringToFront();
    }

    private void InitializeResultsGrid(XmlNode xPageSchema)
    {
      if (this.dgvSearchResults.InvokeRequired)
      {
        this.dgvSearchResults.Invoke((Delegate) new frmPageNameAdvSrch.InitializeResultsGridDelegate(this.InitializeResultsGrid), (object) xPageSchema);
      }
      else
      {
        this.dgvSearchResults.Rows.Clear();
        this.dgvSearchResults.Columns.Clear();
        Hashtable hashtable = new Hashtable();
        ArrayList arrayList = new ArrayList();
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini", "BOOKS_PAGENAME_ADVSEARCH");
        List<DataGridViewTextBoxColumn> viewTextBoxColumnList = new List<DataGridViewTextBoxColumn>();
        if (keys.Count > 0)
        {
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xPageSchema.Attributes)
          {
            if (attribute.Name != null && attribute.Value != null && (attribute.Name.Trim() != string.Empty && attribute.Value.Trim() != string.Empty))
            {
              DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
              viewTextBoxColumn.Name = "PAGE::" + attribute.Name.ToUpper();
              viewTextBoxColumn.Tag = (object) attribute.Value.ToUpper();
              viewTextBoxColumn.HeaderText = this.GetGridLanguage(attribute.Value, this.sServerKey);
              viewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
              string upper = attribute.Value.ToUpper();
              if (keys.Count > 0)
              {
                viewTextBoxColumn.Visible = false;
                for (int index = 0; index < keys.Count; ++index)
                {
                  if (upper == keys[index].ToString().Trim())
                  {
                    string[] strArray = iniFileIo.GetKeyValue("BOOKS_PAGENAME_ADVSEARCH", keys[index].ToString(), Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini").Split('|');
                    try
                    {
                      if (strArray[0].ToString().ToUpper() == "TRUE")
                        viewTextBoxColumn.Visible = true;
                    }
                    catch (Exception ex)
                    {
                      if (!(upper == "PAGENAME"))
                      {
                        if (!(upper == "UPDATEDATE"))
                          goto label_15;
                      }
                      viewTextBoxColumn.Visible = true;
                    }
label_15:
                    try
                    {
                      if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
                        viewTextBoxColumn.HeaderText = Global.GetDGHeaderCellValue("BOOKS_PAGENAME_ADVSEARCH", upper, strArray[1], this.frmParent.frmParent.ServerId);
                      else
                        viewTextBoxColumn.HeaderText = strArray[1];
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                      switch (strArray[2])
                      {
                        case "L":
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                          break;
                        case "R":
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                          break;
                        case "C":
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                          break;
                        default:
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                          break;
                      }
                    }
                    catch (Exception ex)
                    {
                      viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                    try
                    {
                      viewTextBoxColumn.Width = Convert.ToInt32(strArray[3]);
                    }
                    catch (Exception ex)
                    {
                      if (upper == "PAGENAME")
                        viewTextBoxColumn.Width = 200;
                      else
                        viewTextBoxColumn.Width = 80;
                    }
                  }
                }
              }
              viewTextBoxColumnList.Add(viewTextBoxColumn);
              if (upper == "BOOKCODE")
                hashtable.Add((object) "BookCode", (object) ("PAGE::" + attribute.Name.ToUpper()));
              if (upper == "ID")
                hashtable.Add((object) "PageId", (object) ("PAGE::" + attribute.Name.ToUpper()));
              if (upper == "PICINDEX")
                hashtable.Add((object) "PicIndex", (object) ("PAGE::" + attribute.Name.ToUpper()));
              if (upper == "LISTINDEX")
                hashtable.Add((object) "ListIndex", (object) ("PAGE::" + attribute.Name.ToUpper()));
            }
          }
          DataGridViewColumnCollection booksColumns = this.GetBooksColumns();
          if (booksColumns != null)
          {
            if (booksColumns.Count > 0)
            {
              foreach (DataGridViewColumn dataGridViewColumn1 in (BaseCollection) booksColumns)
              {
                if (dataGridViewColumn1.Name.ToUpper() != "CHK")
                {
                  DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                  viewTextBoxColumn.Name = "BOOK::" + dataGridViewColumn1.Name;
                  viewTextBoxColumn.Tag = (object) dataGridViewColumn1.Tag.ToString();
                  viewTextBoxColumn.HeaderText = dataGridViewColumn1.HeaderText;
                  viewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                  viewTextBoxColumn.Width = 80;
                  string upper = dataGridViewColumn1.Tag.ToString().ToUpper();
                  try
                  {
                    if (keys.Count > 0)
                    {
                      viewTextBoxColumn.Visible = false;
                      for (int index = 0; index < keys.Count; ++index)
                      {
                        if (upper == keys[index].ToString().Trim())
                        {
                          string[] strArray = iniFileIo.GetKeyValue("BOOKS_PAGENAME_ADVSEARCH", keys[index].ToString(), Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini").Split('|');
                          try
                          {
                            if (strArray[0].ToString().ToUpper() == "TRUE")
                              viewTextBoxColumn.Visible = true;
                          }
                          catch (Exception ex)
                          {
                            if (!(upper == "PAGENAME"))
                            {
                              if (!(upper == "UPDATEDATE"))
                                goto label_58;
                            }
                            viewTextBoxColumn.Visible = true;
                          }
label_58:
                          try
                          {
                            if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
                              viewTextBoxColumn.HeaderText = Global.GetDGHeaderCellValue("BOOKS_PAGENAME_ADVSEARCH", upper, strArray[1], this.p_ServerId);
                            else
                              viewTextBoxColumn.HeaderText = strArray[1];
                          }
                          catch (Exception ex)
                          {
                          }
                          try
                          {
                            switch (strArray[2])
                            {
                              case "L":
                                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                break;
                              case "R":
                                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                break;
                              case "C":
                                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                break;
                              default:
                                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                break;
                            }
                          }
                          catch (Exception ex)
                          {
                            viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                          }
                          try
                          {
                            viewTextBoxColumn.Width = Convert.ToInt32(strArray[3]);
                          }
                          catch (Exception ex)
                          {
                            if (upper == "PAGENAME")
                              viewTextBoxColumn.Width = 200;
                            else
                              viewTextBoxColumn.Width = 80;
                          }
                        }
                      }
                    }
                    else if (upper == "PUBLISHINGID")
                      viewTextBoxColumn.Visible = true;
                    else
                      viewTextBoxColumn.Visible = false;
                    bool flag = false;
                    foreach (DataGridViewColumn dataGridViewColumn2 in viewTextBoxColumnList)
                    {
                      if (dataGridViewColumn2.Tag.ToString() != string.Empty && dataGridViewColumn2.Visible && dataGridViewColumn2.Tag.ToString().ToUpper() == viewTextBoxColumn.Tag.ToString().ToUpper())
                      {
                        flag = true;
                        break;
                      }
                    }
                    if (!flag)
                      viewTextBoxColumnList.Add(viewTextBoxColumn);
                  }
                  catch (Exception ex)
                  {
                  }
                }
              }
            }
          }
          try
          {
            for (int index = 0; index < keys.Count; ++index)
            {
              foreach (DataGridViewColumn dataGridViewColumn in viewTextBoxColumnList)
              {
                if (keys[index].ToString().ToUpper() == dataGridViewColumn.Tag.ToString().ToUpper())
                {
                  if (this.dgvSearchResults.Columns.Count > 0)
                  {
                    bool flag = false;
                    foreach (DataGridViewColumn column in (BaseCollection) this.dgvSearchResults.Columns)
                    {
                      if (column.Tag.ToString() != string.Empty && column.Visible && column.Tag.ToString().ToUpper() == dataGridViewColumn.Tag.ToString().ToUpper())
                      {
                        flag = true;
                        break;
                      }
                    }
                    if (!flag)
                      this.dgvSearchResults.Columns.Add(dataGridViewColumn);
                  }
                  else
                    this.dgvSearchResults.Columns.Add(dataGridViewColumn);
                }
              }
            }
            foreach (DataGridViewColumn dataGridViewColumn in viewTextBoxColumnList)
            {
              bool flag = false;
              foreach (DataGridViewColumn column in (BaseCollection) this.dgvSearchResults.Columns)
              {
                if (column.HeaderText != string.Empty && column.Visible && column.HeaderText.ToUpper() == dataGridViewColumn.HeaderText.ToUpper())
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
                this.dgvSearchResults.Columns.Add(dataGridViewColumn);
            }
          }
          catch (Exception ex)
          {
          }
          this.dgvSearchResults.Tag = (object) hashtable;
        }
        else
        {
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xPageSchema.Attributes)
          {
            if (attribute.Name != null && attribute.Value != null && (attribute.Name.Trim() != string.Empty && attribute.Value.Trim() != string.Empty))
            {
              DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
              viewTextBoxColumn.Name = "PAGE::" + attribute.Name.ToUpper();
              viewTextBoxColumn.Tag = (object) attribute.Value.ToUpper();
              viewTextBoxColumn.HeaderText = this.GetGridLanguage(attribute.Value, this.sServerKey);
              viewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
              string upper = attribute.Value.ToUpper();
              if (upper == "PAGENAME" || upper == "UPDATEDATE")
                viewTextBoxColumn.Visible = true;
              else
                viewTextBoxColumn.Visible = false;
              if (upper == "PAGENAME")
                viewTextBoxColumn.Width = 200;
              else
                viewTextBoxColumn.Width = 80;
              this.dgvSearchResults.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
              if (upper == "BOOKCODE")
                hashtable.Add((object) "BookCode", (object) ("PAGE::" + attribute.Name.ToUpper()));
              if (upper == "ID")
                hashtable.Add((object) "PageId", (object) ("PAGE::" + attribute.Name.ToUpper()));
              if (upper == "PICINDEX")
                hashtable.Add((object) "PicIndex", (object) ("PAGE::" + attribute.Name.ToUpper()));
              if (upper == "LISTINDEX")
                hashtable.Add((object) "ListIndex", (object) ("PAGE::" + attribute.Name.ToUpper()));
            }
          }
          DataGridViewColumnCollection booksColumns = this.GetBooksColumns();
          if (booksColumns != null && booksColumns.Count > 0)
          {
            foreach (DataGridViewColumn dataGridViewColumn in (BaseCollection) booksColumns)
            {
              if (dataGridViewColumn.Name.ToUpper() != "CHK")
              {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.Name = "BOOK::" + dataGridViewColumn.Name;
                viewTextBoxColumn.Tag = (object) dataGridViewColumn.Tag.ToString();
                viewTextBoxColumn.HeaderText = dataGridViewColumn.HeaderText;
                viewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                viewTextBoxColumn.Width = 80;
                if (dataGridViewColumn.Tag.ToString().ToUpper() == "PUBLISHINGID")
                  viewTextBoxColumn.Visible = true;
                else
                  viewTextBoxColumn.Visible = false;
                this.dgvSearchResults.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
              }
            }
          }
          this.dgvSearchResults.Tag = (object) hashtable;
        }
      }
    }

    private void InitializeBooksGrid(XmlNode xSchemaNode)
    {
      if (this.dgvBooks.InvokeRequired)
      {
        this.dgvBooks.Invoke((Delegate) new frmPageNameAdvSrch.InitializeBooksGridDelegate(this.InitializeBooksGrid), (object) xSchemaNode);
      }
      else
      {
        try
        {
          this.dgvBooks.Rows.Clear();
          this.dgvBooks.Columns.Clear();
          DatagridViewCheckBoxHeaderCell checkBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
          checkBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(this.chkBooksHeader_OnCheckBoxClicked);
          DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
          viewCheckBoxColumn.Name = "CHK";
          viewCheckBoxColumn.Tag = (object) "CHK";
          viewCheckBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewCheckBoxColumn.Frozen = true;
          viewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          checkBoxHeaderCell.Value = (object) "";
          viewCheckBoxColumn.HeaderCell = (DataGridViewColumnHeaderCell) checkBoxHeaderCell;
          this.dgvBooks.Columns.Add((DataGridViewColumn) viewCheckBoxColumn);
          List<DataGridViewTextBoxColumn> viewTextBoxColumnList = new List<DataGridViewTextBoxColumn>();
          ArrayList arrayList = new ArrayList();
          IniFileIO iniFileIo = new IniFileIO();
          ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini", "BOOKS_ADVSEARCH");
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
          {
            if (attribute.Name != null && attribute.Value != null && (attribute.Name.Trim() != string.Empty && attribute.Value.Trim() != string.Empty))
            {
              DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
              viewTextBoxColumn.Name = attribute.Name.ToUpper();
              viewTextBoxColumn.Tag = (object) attribute.Value.ToUpper();
              viewTextBoxColumn.HeaderText = this.GetGridLanguage(attribute.Value, this.sServerKey);
              viewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
              viewTextBoxColumn.Width = 80;
              string upper = attribute.Value.ToUpper();
              if (keys.Count > 0)
              {
                viewTextBoxColumn.Visible = false;
                for (int index = 0; index < keys.Count; ++index)
                {
                  if (upper == keys[index].ToString().Trim())
                  {
                    string[] strArray = iniFileIo.GetKeyValue("BOOKS_ADVSEARCH", keys[index].ToString(), Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini").Split('|');
                    try
                    {
                      if (strArray[0].ToString().ToUpper() == "TRUE")
                        viewTextBoxColumn.Visible = true;
                    }
                    catch (Exception ex)
                    {
                      if (!(upper == "BOOKCODE") && !(upper == "BOOKTYPE"))
                      {
                        if (!(upper == "PUBLISHINGID"))
                          goto label_14;
                      }
                      viewTextBoxColumn.Visible = true;
                    }
label_14:
                    try
                    {
                      if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
                        viewTextBoxColumn.HeaderText = Global.GetDGHeaderCellValue("BOOKS_ADVSEARCH", upper, strArray[1], this.p_ServerId);
                      else
                        viewTextBoxColumn.HeaderText = strArray[1];
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                      switch (strArray[2])
                      {
                        case "L":
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                          break;
                        case "R":
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                          break;
                        case "C":
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                          break;
                        default:
                          viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                          break;
                      }
                    }
                    catch (Exception ex)
                    {
                      viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                    try
                    {
                      viewTextBoxColumn.Width = Convert.ToInt32(strArray[3]);
                      break;
                    }
                    catch (Exception ex)
                    {
                      break;
                    }
                  }
                }
                if (viewTextBoxColumn.Visible || viewTextBoxColumn.Tag.ToString() == "BOOKCODE" || (viewTextBoxColumn.Tag.ToString() == "BOOKTYPE" || viewTextBoxColumn.Tag.ToString() == "PUBLISHINGID"))
                  viewTextBoxColumnList.Add(viewTextBoxColumn);
              }
              else
              {
                if (upper == "BOOKCODE" || upper == "BOOKTYPE" || upper == "PUBLISHINGID")
                  viewTextBoxColumn.Visible = true;
                else
                  viewTextBoxColumn.Visible = false;
                if (viewTextBoxColumn.Visible || viewTextBoxColumn.Tag.ToString() == "BOOKCODE" || (viewTextBoxColumn.Tag.ToString() == "BOOKTYPE" || viewTextBoxColumn.Tag.ToString() == "PUBLISHINGID"))
                  this.dgvBooks.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
              }
            }
          }
          if (keys.Count <= 0)
            return;
          try
          {
            for (int index = 0; index < keys.Count; ++index)
            {
              foreach (DataGridViewColumn dataGridViewColumn in viewTextBoxColumnList)
              {
                if (keys[index].ToString().ToUpper() == dataGridViewColumn.Tag.ToString().ToUpper())
                {
                  this.dgvBooks.Columns.Add(dataGridViewColumn);
                  break;
                }
              }
            }
            foreach (DataGridViewColumn dataGridViewColumn in viewTextBoxColumnList)
            {
              bool flag = false;
              foreach (DataGridViewColumn column in (BaseCollection) this.dgvBooks.Columns)
              {
                if (column.Tag.ToString() != string.Empty && column.Visible && column.Tag.ToString().ToUpper() == dataGridViewColumn.Tag.ToString().ToUpper())
                {
                  flag = true;
                  break;
                }
              }
              if (!flag)
                this.dgvBooks.Columns.Add(dataGridViewColumn);
            }
          }
          catch (Exception ex)
          {
          }
        }
        catch (Exception ex)
        {
        }
      }
    }

    private string GetKeyword()
    {
      if (this.ltbKeyword.InvokeRequired)
        return (string) this.ltbKeyword.Invoke((Delegate) new frmPageNameAdvSrch.GetKeywordDelegate(this.GetKeyword));
      return this.ltbKeyword._Text;
    }

    private bool GetMatchCase()
    {
      if (this.chkMatchCase.InvokeRequired)
        return (bool) this.chkMatchCase.Invoke((Delegate) new frmPageNameAdvSrch.GetMatchCaseDelegate(this.GetMatchCase));
      return this.chkMatchCase.Checked;
    }

    private bool GetExactMatch()
    {
      if (this.chkExactMatch.InvokeRequired)
        return (bool) this.chkExactMatch.Invoke((Delegate) new frmPageNameAdvSrch.GetExaceMatchDelegate(this.GetExactMatch));
      return this.chkExactMatch.Checked;
    }

    private DataGridViewColumnCollection GetBooksColumns()
    {
      if (this.dgvBooks.InvokeRequired)
        return (DataGridViewColumnCollection) this.dgvBooks.Invoke((Delegate) new frmPageNameAdvSrch.GetBooksColumnsDelegate(this.GetBooksColumns));
      return this.dgvBooks.Columns;
    }

    private ArrayList GetSelectedBookRows()
    {
      if (this.dgvBooks.InvokeRequired)
        return (ArrayList) this.dgvBooks.Invoke((Delegate) new frmPageNameAdvSrch.GetSelectedBookRowsDelegate(this.GetSelectedBookRows));
      ArrayList arrayList = new ArrayList();
      foreach (DataGridViewRow row in (IEnumerable) this.dgvBooks.Rows)
      {
        bool result = false;
        if (this.dgvBooks.Columns.Contains("CHK") && row.Cells["CHK"].Value != null)
          bool.TryParse(row.Cells["CHK"].Value.ToString(), out result);
        if (result)
          arrayList.Add((object) row);
      }
      return arrayList;
    }

    private void ShowHideSearchControls(bool value)
    {
      this.pnlBooks.Visible = value;
      this.pnlControl.Visible = value;
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblSearch.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.lblDetails.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgvSearchResults.Font = Settings.Default.appFont;
      this.dgvSearchResults.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgvSearchResults.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
      this.dgvBooks.Font = Settings.Default.appFont;
      this.dgvBooks.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgvBooks.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
      foreach (Control control in (ArrangedElementCollection) this.pnlBooks.Controls)
      {
        if (control.GetType() == typeof (LabledTextBox))
          control.Font = Settings.Default.appFont;
      }
    }

    private void LoadBooks()
    {
      Global.ComboBoxItem selectedItem = (Global.ComboBoxItem) this.cmbServers.SelectedItem;
      string empty = string.Empty;
      string path = string.Empty;
      try
      {
        int index = int.Parse(selectedItem.Tag.ToString());
        empty = Program.iniServers[index].items["SETTINGS", "CONTENT_PATH"];
        if (!empty.EndsWith("/"))
          empty += "/";
        path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
        path = path + "\\" + Program.iniServers[index].sIniKey;
        this.sServerKey = Program.iniServers[index].sIniKey;
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
      }
      catch
      {
        int num = (int) MessageHandler.ShowMessage((IWin32Window) this.frmParent, this.GetResource("(E-PT-CF001) Failed to create file/folder specified.", "(E-PT-CF001)_FAILED", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      this.cmbServers.Enabled = false;
      this.btnSearch.Enabled = false;
      this.ShowLoading(this.pnlSearchCriteria);
      this.bgWorker.RunWorkerAsync((object) new object[3]
      {
        (object) empty,
        (object) path,
        (object) selectedItem
      });
    }

    private bool LoadBooksInGrid(string sFilePath)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (!System.IO.File.Exists(sFilePath))
        return false;
      if (this.p_ServerId != 99999 && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
      {
        try
        {
          string empty = string.Empty;
          Global.Unzip(sFilePath);
          string filename = sFilePath.ToLower().Replace(".zip", ".xml");
          xmlDocument.Load(filename);
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
          return false;
        }
      }
      if (this.p_ServerId != 99999 && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
      {
        string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
        xmlDocument.DocumentElement.InnerXml = str;
      }
      XmlNode xSchemaNode = xmlDocument.SelectSingleNode("//Schema");
      if (xSchemaNode == null)
        return false;
      string index1 = "";
      string index2 = "";
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          index1 = attribute.Name;
        else if (attribute.Value.ToUpper().Equals("BOOKCODE"))
        {
          string name = attribute.Name;
        }
        else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
          index2 = attribute.Name;
      }
      if (index1 == "" || index2 == "")
        return false;
      this.dgvBooks.Tag = (object) xSchemaNode;
      this.InitializeBooksGrid(xSchemaNode);
      XmlNodeList xNodeList = xmlDocument.SelectNodes("//Books/Book");
      foreach (XmlNode filterBooks in this.frmParent.FilterBooksList(xSchemaNode, xNodeList))
      {
        if (filterBooks.Attributes[index1] != null && filterBooks.Attributes[index2] != null)
        {
          this.AddRowToBooksGrid();
          this.dgvBooks.Rows[this.dgvBooks.Rows.Count - 1].Tag = (object) filterBooks.Attributes[index2].Value;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) filterBooks.Attributes)
          {
            if (attribute.Name != null && attribute.Value != null && (attribute.Name.Trim() != string.Empty && attribute.Value.Trim() != string.Empty) && this.dgvBooks.Columns.Contains(attribute.Name))
              this.dgvBooks.Rows[this.dgvBooks.Rows.Count - 1].Cells[attribute.Name].Value = (object) attribute.Value;
          }
        }
      }
      return true;
    }

    private void GetResults(object arguments)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      this.statusText = this.GetResource("Searching pages", "SEARCHING_PAGES", ResourceType.STATUS_MESSAGE);
      this.UpdateStatus();
      try
      {
        Exception exception = new Exception();
        exception.Source = "custom";
        string keyword = this.GetKeyword();
        string FieldToSearch = "PageName";
        bool matchCase = this.GetMatchCase();
        bool exactMatch = this.GetExactMatch();
        ArrayList selectedBookRows = this.GetSelectedBookRows();
        if (selectedBookRows == null || selectedBookRows.Count == 0)
        {
          exception.HelpLink = this.GetResource("Select books to search from", "SELECT_BOOKS", ResourceType.LABEL);
          throw exception;
        }
        Search search = new Search();
        search.Url = Program.iniServers[this.p_ServerId].items["SETTINGS", "SERVICE_PATH"] == null ? string.Empty : Program.iniServers[this.p_ServerId].items["SETTINGS", "SERVICE_PATH"].ToLower();
        DataGridViewRow dataGridViewRow1 = (DataGridViewRow) selectedBookRows[0];
        XmlNode xPageSchema = search.SearchPageSchema(dataGridViewRow1.Tag.ToString(), Program.iniServers[this.p_ServerId].sIniKey);
        if (xPageSchema == null && selectedBookRows.Count > 1)
        {
          DataGridViewRow dataGridViewRow2 = (DataGridViewRow) selectedBookRows[1];
          xPageSchema = search.SearchPageSchema(dataGridViewRow2.Tag.ToString(), Program.iniServers[this.p_ServerId].sIniKey);
        }
        if (xPageSchema == null || xPageSchema.Attributes.Count == 0)
        {
          exception.HelpLink = this.GetResource("File schema not found on server", "NO_SCHEMA", ResourceType.LABEL);
          throw exception;
        }
        this.InitializeResultsGrid(xPageSchema);
        this.ShowResultGrid();
        foreach (DataGridViewRow dataGridViewRow2 in selectedBookRows)
        {
          XmlNode xmlNode = search.SearchPages(keyword, FieldToSearch, (matchCase ? 1 : 0) != 0, (exactMatch ? 1 : 0) != 0, new string[1]
          {
            dataGridViewRow2.Tag.ToString()
          }, Program.iniServers[this.p_ServerId].sIniKey, this.frmParent.frmParent.p_ArgsF);
          if (xmlNode != null && xmlNode.ChildNodes.Count > 0)
          {
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
              this.AddRowToResultsGrid();
              foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.Attributes)
              {
                try
                {
                  this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells["PAGE::" + attribute.Name].Value = (object) attribute.Value;
                }
                catch
                {
                }
              }
              if (childNode.ChildNodes.Count > 0)
              {
                foreach (XmlAttribute attribute in (XmlNamedNodeMap) childNode.ChildNodes[0].Attributes)
                {
                  try
                  {
                    this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells["PAGE::" + attribute.Name].Value = (object) attribute.Value;
                  }
                  catch
                  {
                  }
                }
              }
              for (int index1 = 0; index1 < this.dgvSearchResults.Columns.Count; ++index1)
              {
                if (this.dgvSearchResults.Columns[index1].Name.Contains("BOOK::"))
                {
                  try
                  {
                    string index2 = this.dgvSearchResults.Columns[index1].Name.Replace("BOOK::", string.Empty);
                    this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[index1].Value = dataGridViewRow2.Cells[index2].Value;
                  }
                  catch
                  {
                  }
                }
              }
            }
          }
        }
        if (this.dgvSearchResults.Rows.Count == 0)
          this.ShowResultMessage(this.GetResource("No result found", "NO_RESULT_FOUND", ResourceType.LABEL));
        this.statusText = this.GetResource("Search complete", "SEARCH_COMPLETE", ResourceType.STATUS_MESSAGE);
      }
      catch (ThreadInterruptedException ex)
      {
        if (this.bFormClosing)
          return;
        this.statusText = this.GetResource("Search Canceled", "SEARCH_CANCELED", ResourceType.STATUS_MESSAGE);
      }
      catch (WebException ex)
      {
        if (this.bFormClosing)
          return;
        this.statusText = this.GetResource("Search failed", "SEARCH_FAILED", ResourceType.STATUS_MESSAGE);
        this.ShowResultMessage(this.GetResource("Web service not found at specified path", "NO_WEBSERVICE", ResourceType.LABEL));
      }
      catch (UriFormatException ex)
      {
        if (this.bFormClosing)
          return;
        this.statusText = this.GetResource("Search failed", "SEARCH_FAILED", ResourceType.STATUS_MESSAGE);
        this.ShowResultMessage(this.GetResource("Web service path specified is not valid", "PATH_INCORRECT", ResourceType.LABEL));
      }
      catch (Exception ex)
      {
        if (this.bFormClosing)
          return;
        if (ex.Source == "custom")
          this.ShowResultMessage(ex.HelpLink);
        else
          this.ShowResultMessage(this.GetResource("Can not connect to specified address.", "CANNOT_CONNECT", ResourceType.LABEL));
        this.statusText = this.GetResource("Search failed", "SEARCH_FAILED", ResourceType.STATUS_MESSAGE);
      }
      finally
      {
        if (!this.bFormClosing)
        {
          this.ChangeSearchButtonText(this.GetResource("Search", "SEARCH", ResourceType.BUTTON), false);
          this.UpdateStatus();
          this.EnableDisableSearchControls(true);
          this.HideLoading(this.pnlLoading);
        }
      }
    }

    public void LoadResources()
    {
      this.lblSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.LABEL);
      this.chkMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
      this.chkExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
      this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
      this.ltbKeyword._Caption = this.GetResource("Keyword", "KEYWORD", ResourceType.LABEL);
      this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='OPEN_BOOKS']" + "/Screen[@Name='PAGE_NAME_ADVSEARCH']";
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
            string xQuery1 = str + "[@name='" + sKey + "']";
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

    private string GetGridLanguage(string sKey, string sServerKey)
    {
      bool flag = false;
      string str1 = Settings.Default.appLanguage + "_GSP_" + sServerKey + ".ini";
      if (System.IO.File.Exists(Application.StartupPath + "\\Language XMLs\\" + str1))
      {
        TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str1);
        string str2;
        while ((str2 = textReader.ReadLine()) != null)
        {
          if (str2.ToUpper() == "[OPENBOOK]")
            flag = true;
          else if (str2.Contains("=") && flag)
          {
            string[] strArray = str2.Split(new string[1]
            {
              "="
            }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
              if (strArray[0].ToString().ToUpper() == sKey.ToUpper())
                return strArray[1].ToString();
            }
            catch
            {
              return sKey;
            }
          }
          else if (str2.Contains("["))
            flag = false;
        }
      }
      return sKey;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlForm = new SplitContainer();
      this.pnlSearchCriteria = new Panel();
      this.pnlBooks = new Panel();
      this.dgvBooks = new DataGridView();
      this.pnlControl = new Panel();
      this.ltbKeyword = new LabledTextBox();
      this.btnSearch = new Button();
      this.chkExactMatch = new CheckBox();
      this.chkMatchCase = new CheckBox();
      this.pnltvSearch = new Panel();
      this.cmbServers = new CustomComboBox();
      this.lblSearch = new Label();
      this.pnlDetails = new Panel();
      this.pnlSearchResults = new Panel();
      this.dgvSearchResults = new DataGridView();
      this.pnlrtbNoDetails = new Panel();
      this.rtbNoDetails = new RichTextBox();
      this.pnlLoading = new Panel();
      this.lblDetails = new Label();
      this.bgWorker = new BackgroundWorker();
      this.bgLoader = new BackgroundWorker();
      this.picLoading = new PictureBox();
      this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.pnlForm.Panel1.SuspendLayout();
      this.pnlForm.Panel2.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.pnlSearchCriteria.SuspendLayout();
      this.pnlBooks.SuspendLayout();
      ((ISupportInitialize) this.dgvBooks).BeginInit();
      this.pnlControl.SuspendLayout();
      this.pnltvSearch.SuspendLayout();
      this.pnlDetails.SuspendLayout();
      this.pnlSearchResults.SuspendLayout();
      ((ISupportInitialize) this.dgvSearchResults).BeginInit();
      this.pnlrtbNoDetails.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Panel1.BackColor = SystemColors.Control;
      this.pnlForm.Panel1.Controls.Add((Control) this.pnlSearchCriteria);
      this.pnlForm.Panel1.Controls.Add((Control) this.pnltvSearch);
      this.pnlForm.Panel1.Controls.Add((Control) this.lblSearch);
      this.pnlForm.Panel1MinSize = 270;
      this.pnlForm.Panel2.Controls.Add((Control) this.pnlDetails);
      this.pnlForm.Panel2.Controls.Add((Control) this.lblDetails);
      this.pnlForm.Panel2MinSize = 75;
      this.pnlForm.Size = new Size(584, 409);
      this.pnlForm.SplitterDistance = 270;
      this.pnlForm.TabIndex = 18;
      this.pnlSearchCriteria.BackColor = Color.White;
      this.pnlSearchCriteria.Controls.Add((Control) this.pnlBooks);
      this.pnlSearchCriteria.Controls.Add((Control) this.pnlControl);
      this.pnlSearchCriteria.Dock = DockStyle.Fill;
      this.pnlSearchCriteria.Location = new Point(0, 70);
      this.pnlSearchCriteria.Name = "pnlSearchCriteria";
      this.pnlSearchCriteria.Size = new Size(268, 337);
      this.pnlSearchCriteria.TabIndex = 16;
      this.pnlBooks.Controls.Add((Control) this.dgvBooks);
      this.pnlBooks.Dock = DockStyle.Fill;
      this.pnlBooks.Location = new Point(0, 100);
      this.pnlBooks.Name = "pnlBooks";
      this.pnlBooks.Size = new Size(268, 237);
      this.pnlBooks.TabIndex = 23;
      this.dgvBooks.AllowUserToAddRows = false;
      this.dgvBooks.AllowUserToDeleteRows = false;
      this.dgvBooks.AllowUserToResizeRows = false;
      this.dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgvBooks.BackgroundColor = Color.White;
      this.dgvBooks.BorderStyle = BorderStyle.None;
      this.dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvBooks.Dock = DockStyle.Fill;
      this.dgvBooks.Location = new Point(0, 0);
      this.dgvBooks.MultiSelect = false;
      this.dgvBooks.Name = "dgvBooks";
      this.dgvBooks.ReadOnly = true;
      this.dgvBooks.RowHeadersVisible = false;
      this.dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvBooks.Size = new Size(268, 237);
      this.dgvBooks.TabIndex = 1;
      this.dgvBooks.CellValueChanged += new DataGridViewCellEventHandler(this.dgvBooks_CellValueChanged);
      this.dgvBooks.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvBooks_CellDoubleClick);
      this.dgvBooks.CellClick += new DataGridViewCellEventHandler(this.dgvBooks_CellClick);
      this.pnlControl.Controls.Add((Control) this.ltbKeyword);
      this.pnlControl.Controls.Add((Control) this.btnSearch);
      this.pnlControl.Controls.Add((Control) this.chkExactMatch);
      this.pnlControl.Controls.Add((Control) this.chkMatchCase);
      this.pnlControl.Dock = DockStyle.Top;
      this.pnlControl.Location = new Point(0, 0);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(15, 4, 15, 4);
      this.pnlControl.Size = new Size(268, 100);
      this.pnlControl.TabIndex = 22;
      this.pnlControl.Visible = false;
      this.ltbKeyword._Caption = "Keyword";
      this.ltbKeyword._ID = "lbl";
      this.ltbKeyword._Name = "Keyword";
      this.ltbKeyword._Text = "";
      this.ltbKeyword.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.ltbKeyword.Location = new Point(15, 59);
      this.ltbKeyword.Name = "ltbKeyword";
      this.ltbKeyword.Size = new Size(215, 26);
      this.ltbKeyword.TabIndex = 5;
      this.btnSearch.Image = (Image) Resources.Search2;
      this.btnSearch.ImageAlign = ContentAlignment.MiddleRight;
      this.btnSearch.Location = new Point(145, 7);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new Size(80, 23);
      this.btnSearch.TabIndex = 1;
      this.btnSearch.Text = "Search";
      this.btnSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
      this.chkExactMatch.AutoSize = true;
      this.chkExactMatch.Location = new Point(15, 11);
      this.chkExactMatch.Name = "chkExactMatch";
      this.chkExactMatch.Size = new Size(85, 17);
      this.chkExactMatch.TabIndex = 4;
      this.chkExactMatch.Text = "Exact Match";
      this.chkExactMatch.UseVisualStyleBackColor = true;
      this.chkMatchCase.AutoSize = true;
      this.chkMatchCase.Location = new Point(15, 34);
      this.chkMatchCase.Name = "chkMatchCase";
      this.chkMatchCase.Size = new Size(82, 17);
      this.chkMatchCase.TabIndex = 3;
      this.chkMatchCase.Text = "Match Case";
      this.chkMatchCase.UseVisualStyleBackColor = true;
      this.pnltvSearch.BackColor = Color.White;
      this.pnltvSearch.Controls.Add((Control) this.cmbServers);
      this.pnltvSearch.Dock = DockStyle.Top;
      this.pnltvSearch.Location = new Point(0, 27);
      this.pnltvSearch.Name = "pnltvSearch";
      this.pnltvSearch.Padding = new Padding(15, 15, 15, 0);
      this.pnltvSearch.Size = new Size(268, 43);
      this.pnltvSearch.TabIndex = 15;
      this.cmbServers.DrawMode = DrawMode.OwnerDrawVariable;
      this.cmbServers.DropDownHeight = 150;
      this.cmbServers.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbServers.FormattingEnabled = true;
      this.cmbServers.IntegralHeight = false;
      this.cmbServers.Location = new Point(15, 15);
      this.cmbServers.Name = "cmbServers";
      this.cmbServers.Size = new Size(210, 22);
      this.cmbServers.TabIndex = 12;
      this.cmbServers.SelectedIndexChanged += new EventHandler(this.cmbServers_SelectedIndexChanged);
      this.lblSearch.BackColor = Color.White;
      this.lblSearch.Dock = DockStyle.Top;
      this.lblSearch.ForeColor = Color.Black;
      this.lblSearch.Location = new Point(0, 0);
      this.lblSearch.Name = "lblSearch";
      this.lblSearch.Padding = new Padding(3, 7, 0, 0);
      this.lblSearch.Size = new Size(268, 27);
      this.lblSearch.TabIndex = 14;
      this.lblSearch.Text = "Search";
      this.pnlDetails.BackColor = Color.White;
      this.pnlDetails.Controls.Add((Control) this.pnlSearchResults);
      this.pnlDetails.Controls.Add((Control) this.pnlLoading);
      this.pnlDetails.Dock = DockStyle.Fill;
      this.pnlDetails.Location = new Point(0, 27);
      this.pnlDetails.Name = "pnlDetails";
      this.pnlDetails.Size = new Size(308, 380);
      this.pnlDetails.TabIndex = 15;
      this.pnlSearchResults.BackColor = Color.White;
      this.pnlSearchResults.Controls.Add((Control) this.dgvSearchResults);
      this.pnlSearchResults.Controls.Add((Control) this.pnlrtbNoDetails);
      this.pnlSearchResults.Dock = DockStyle.Fill;
      this.pnlSearchResults.Location = new Point(0, 0);
      this.pnlSearchResults.Name = "pnlSearchResults";
      this.pnlSearchResults.Size = new Size(308, 346);
      this.pnlSearchResults.TabIndex = 13;
      this.pnlSearchResults.Tag = (object) "";
      this.dgvSearchResults.AllowUserToAddRows = false;
      this.dgvSearchResults.AllowUserToDeleteRows = false;
      this.dgvSearchResults.AllowUserToResizeRows = false;
      this.dgvSearchResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgvSearchResults.BackgroundColor = Color.White;
      this.dgvSearchResults.BorderStyle = BorderStyle.None;
      this.dgvSearchResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvSearchResults.Dock = DockStyle.Fill;
      this.dgvSearchResults.Location = new Point(0, 0);
      this.dgvSearchResults.MultiSelect = false;
      this.dgvSearchResults.Name = "dgvSearchResults";
      this.dgvSearchResults.ReadOnly = true;
      this.dgvSearchResults.RowHeadersVisible = false;
      this.dgvSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvSearchResults.Size = new Size(308, 346);
      this.dgvSearchResults.TabIndex = 0;
      this.dgvSearchResults.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.dgvSearchResults_CellMouseDoubleClick);
      this.pnlrtbNoDetails.BackColor = Color.White;
      this.pnlrtbNoDetails.Controls.Add((Control) this.rtbNoDetails);
      this.pnlrtbNoDetails.Dock = DockStyle.Fill;
      this.pnlrtbNoDetails.Location = new Point(0, 0);
      this.pnlrtbNoDetails.Name = "pnlrtbNoDetails";
      this.pnlrtbNoDetails.Padding = new Padding(25, 10, 0, 0);
      this.pnlrtbNoDetails.Size = new Size(308, 346);
      this.pnlrtbNoDetails.TabIndex = 16;
      this.pnlrtbNoDetails.Tag = (object) "";
      this.rtbNoDetails.BackColor = Color.White;
      this.rtbNoDetails.BorderStyle = BorderStyle.None;
      this.rtbNoDetails.Dock = DockStyle.Fill;
      this.rtbNoDetails.Location = new Point(25, 10);
      this.rtbNoDetails.Name = "rtbNoDetails";
      this.rtbNoDetails.ReadOnly = true;
      this.rtbNoDetails.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbNoDetails.Size = new Size(283, 336);
      this.rtbNoDetails.TabIndex = 13;
      this.rtbNoDetails.TabStop = false;
      this.rtbNoDetails.Text = "";
      this.pnlLoading.Dock = DockStyle.Bottom;
      this.pnlLoading.Location = new Point(0, 346);
      this.pnlLoading.Name = "pnlLoading";
      this.pnlLoading.Size = new Size(308, 34);
      this.pnlLoading.TabIndex = 18;
      this.pnlLoading.Tag = (object) "loading";
      this.pnlLoading.Visible = false;
      this.lblDetails.BackColor = Color.White;
      this.lblDetails.Dock = DockStyle.Top;
      this.lblDetails.ForeColor = Color.Black;
      this.lblDetails.Location = new Point(0, 0);
      this.lblDetails.Name = "lblDetails";
      this.lblDetails.Padding = new Padding(3, 7, 0, 0);
      this.lblDetails.Size = new Size(308, 27);
      this.lblDetails.TabIndex = 14;
      this.lblDetails.Text = "Details";
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.bgLoader.WorkerSupportsCancellation = true;
      this.picLoading.BackColor = Color.Transparent;
      this.picLoading.Dock = DockStyle.Fill;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(0, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(584, 409);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 20;
      this.picLoading.TabStop = false;
      this.dataGridViewCheckBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewCheckBoxColumn1.FalseValue = (object) "0";
      this.dataGridViewCheckBoxColumn1.FillWeight = 15.22843f;
      this.dataGridViewCheckBoxColumn1.Frozen = true;
      this.dataGridViewCheckBoxColumn1.HeaderText = "";
      this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
      this.dataGridViewCheckBoxColumn1.ReadOnly = true;
      this.dataGridViewCheckBoxColumn1.Resizable = DataGridViewTriState.False;
      this.dataGridViewCheckBoxColumn1.TrueValue = (object) "1";
      this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn1.FillWeight = 142.3858f;
      this.dataGridViewTextBoxColumn1.HeaderText = "Book Publishing Id";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn2.FillWeight = 142.3858f;
      this.dataGridViewTextBoxColumn2.HeaderText = "UpdateDate";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.HeaderText = "UpdateDate";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 107;
      this.dataGridViewTextBoxColumn4.HeaderText = "Publishing Id";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.Width = 107;
      this.dataGridViewTextBoxColumn5.HeaderText = "UpdateDate";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.Width = 107;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(584, 409);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.picLoading);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmPageNameAdvSrch);
      this.Text = "frmOpenBookSearch";
      this.Load += new EventHandler(this.frmPageNameAdvSrch_Load);
      this.VisibleChanged += new EventHandler(this.frmPageNameAdvSrch_VisibleChanged);
      this.FormClosing += new FormClosingEventHandler(this.frmPageNameAdvSrch_FormClosing);
      this.pnlForm.Panel1.ResumeLayout(false);
      this.pnlForm.Panel2.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.pnlSearchCriteria.ResumeLayout(false);
      this.pnlBooks.ResumeLayout(false);
      ((ISupportInitialize) this.dgvBooks).EndInit();
      this.pnlControl.ResumeLayout(false);
      this.pnlControl.PerformLayout();
      this.pnltvSearch.ResumeLayout(false);
      this.pnlDetails.ResumeLayout(false);
      this.pnlSearchResults.ResumeLayout(false);
      ((ISupportInitialize) this.dgvSearchResults).EndInit();
      this.pnlrtbNoDetails.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    private delegate void EnableDisableSearchControlsDelegate(bool value);

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void StatusDelegate(string status);

    private delegate void AddRowToBooksGridDelegate();

    private delegate void AddRowToResultsGridDelegate();

    private delegate void ChangeSearchButtonTextDelegate(string caption, bool isSearching);

    private delegate void ShowResultMessageDelegate(string msg);

    private delegate void ShowResultGridDelegate();

    private delegate void InitializeResultsGridDelegate(XmlNode xPageSchema);

    private delegate void InitializeBooksGridDelegate(XmlNode xSchemaNode);

    private delegate string GetKeywordDelegate();

    private delegate bool GetMatchCaseDelegate();

    private delegate bool GetExaceMatchDelegate();

    private delegate DataGridViewColumnCollection GetBooksColumnsDelegate();

    private delegate ArrayList GetSelectedBookRowsDelegate();
  }
}

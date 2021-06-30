// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConfigSearchPartName
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
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GSPcLocalViewer
{
  public class frmConfigSearchPartName : Form
  {
    private string sLocalFile = string.Empty;
    private string sServerFile = string.Empty;
    private frmConfig frmParent;
    private int serverId;
    private Rectangle dragBoxFromMouseDown;
    private int rowIndexFromMouseDown;
    private int rowIndexOfItemUnderMouseToDrop;
    private Download objDownloader;
    private IContainer components;
    private Label lblSearchSettings;
    private Panel pnlControl;
    private Button btnOk;
    private Button btnCancel;
    private Panel pnlForm;
    private DataGridView dgViewSearchSettings;
    private BackgroundWorker bgWorker;
    private PictureBox picLoading;

    public frmConfigSearchPartName(frmConfig frm, int _serverId)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.serverId = _serverId;
      this.objDownloader = new Download();
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmConfigBookPublishingIdSearch_Load(object sender, EventArgs e)
    {
      this.ShowLoading(this.pnlForm);
      this.bgWorker.RunWorkerAsync();
    }

    private void InitializeSearchGrid()
    {
      if (this.dgViewSearchSettings.InvokeRequired)
      {
        this.dgViewSearchSettings.Invoke((Delegate) new frmConfigSearchPartName.InitializeSearchGridDelegate(this.InitializeSearchGrid));
      }
      else
      {
        try
        {
          DataGridViewCheckBoxColumn viewCheckBoxColumn = new DataGridViewCheckBoxColumn();
          viewCheckBoxColumn.Name = "select";
          DatagridViewCheckBoxHeaderCell checkBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
          checkBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(this.chkHeader_OnCheckBoxClicked);
          checkBoxHeaderCell.Value = (object) string.Empty;
          viewCheckBoxColumn.HeaderCell = (DataGridViewColumnHeaderCell) checkBoxHeaderCell;
          viewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          viewCheckBoxColumn.Frozen = true;
          viewCheckBoxColumn.TrueValue = (object) true;
          viewCheckBoxColumn.FalseValue = (object) false;
          viewCheckBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
          this.dgViewSearchSettings.Columns.Add((DataGridViewColumn) viewCheckBoxColumn);
          DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn1.Name = "FieldId";
          viewTextBoxColumn1.HeaderText = "Field ID";
          viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn1.Width = 100;
          this.dgViewSearchSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
          DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn2.Name = "DisplayName";
          viewTextBoxColumn2.HeaderText = "Display Name";
          viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn2.Width = 100;
          this.dgViewSearchSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn3.Name = "Width";
          viewTextBoxColumn3.HeaderText = "Col Width";
          viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn3.Width = 100;
          this.dgViewSearchSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
          DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
          viewComboBoxColumn.Name = "Alignment";
          viewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewComboBoxColumn.Width = 100;
          this.dgViewSearchSettings.Columns.Add((DataGridViewColumn) viewComboBoxColumn);
        }
        catch
        {
        }
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.InitializeSearchGrid();
      this.LoadDataGridView();
      this.SetControlsText();
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.HideLoading(this.pnlForm);
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.frmParent.CloseAndSaveSettings();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.frmParent.Close();
    }

    private void SetControlsText()
    {
      this.lblSearchSettings.Text = this.GetResource("Search Settings", "SEARCH_SETTINGS", ResourceType.LABEL);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      try
      {
        this.dgViewSearchSettings.Columns["FieldId"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSearchSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSearchSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSearchSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
    }

    public void SaveSettings()
    {
      try
      {
        string empty = string.Empty;
        if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
        {
          for (int index = 0; index < this.dgViewSearchSettings.Rows.Count; ++index)
            Global.SaveToLanguageIni("PARTNAME_SEARCH", this.dgViewSearchSettings[1, index].Value.ToString(), this.dgViewSearchSettings[2, index].Value.ToString(), this.serverId);
        }
        for (int index = 0; index < this.dgViewSearchSettings.Rows.Count; ++index)
        {
          string str;
          if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
            str = Global.GetEngHeaderVal("PARTNAME_SEARCH", this.dgViewSearchSettings[1, index].Value.ToString(), this.serverId).Split('|')[1];
          else
            str = this.dgViewSearchSettings[2, index].Value.ToString();
          Global.SaveToServerIni("PARTNAME_SEARCH", this.dgViewSearchSettings[1, index].Value.ToString(), this.dgViewSearchSettings[0, index].Value.ToString() + "|" + str + "|" + this.dgViewSearchSettings[4, index].Value.ToString() + "|" + this.dgViewSearchSettings[3, index].Value.ToString(), this.serverId);
        }
      }
      catch
      {
      }
    }

    private void AddSearchSettingsRow(string sVal, string sKey)
    {
      try
      {
        string[] strArray1 = sVal.Split('|');
        DataGridViewRow dataGridViewRow = new DataGridViewRow();
        DataGridViewCheckBoxCell viewCheckBoxCell = new DataGridViewCheckBoxCell();
        viewCheckBoxCell.Value = (object) strArray1[0];
        dataGridViewRow.Cells.Add((DataGridViewCell) viewCheckBoxCell);
        DataGridViewTextBoxCell gridViewTextBoxCell1 = new DataGridViewTextBoxCell();
        gridViewTextBoxCell1.Value = (object) sKey;
        dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell1);
        DataGridViewTextBoxCell gridViewTextBoxCell2 = new DataGridViewTextBoxCell();
        gridViewTextBoxCell2.Value = (object) Global.GetDGHeaderCellValue("PARTNAME_SEARCH", sKey, strArray1[1], this.serverId);
        dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell2);
        DataGridViewTextBoxCell gridViewTextBoxCell3 = new DataGridViewTextBoxCell();
        gridViewTextBoxCell3.Value = (object) strArray1[3].ToString();
        dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell3);
        DataGridViewComboBoxCell viewComboBoxCell = new DataGridViewComboBoxCell();
        string[] strArray2 = "L,R,C".ToString().Split(',');
        viewComboBoxCell.Items.AddRange((object[]) strArray2);
        switch (strArray1[2].ToString())
        {
          case "L":
            viewComboBoxCell.Value = (object) strArray2[0];
            break;
          case "R":
            viewComboBoxCell.Value = (object) strArray2[1];
            break;
          case "C":
            viewComboBoxCell.Value = (object) strArray2[2];
            break;
          default:
            viewComboBoxCell.Value = (object) strArray2[0];
            break;
        }
        dataGridViewRow.Cells.Add((DataGridViewCell) viewComboBoxCell);
        dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
        this.dgViewSearchSettings.Rows.Add(dataGridViewRow);
      }
      catch
      {
      }
    }

    private void LoadDataGridView()
    {
      if (this.dgViewSearchSettings.InvokeRequired)
      {
        this.dgViewSearchSettings.Invoke((Delegate) new frmConfigSearchPartName.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
      }
      else
      {
        this.dgViewSearchSettings.Rows.Clear();
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        IniFileIO iniFileIo = new IniFileIO();
        arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini", "PARTNAME_SEARCH");
        for (int index = 0; index < keys.Count; ++index)
        {
          try
          {
            this.AddSearchSettingsRow(new IniFileIO().GetKeyValue("PARTNAME_SEARCH", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini"), keys[index].ToString());
          }
          catch
          {
          }
        }
        if (keys.Count != 0)
          return;
        this.AddSearchSettingsRow("true|Part Name|C|100", "PartName");
        this.AddSearchSettingsRow("true|Part Number|C|100", "PartNumber");
        this.AddSearchSettingsRow("true|Page Name|C|100", "PageName");
        this.AddSearchSettingsRow("true|Page ID|C|100", "ID");
      }
    }

    private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
    {
      for (int index = 0; index < this.dgViewSearchSettings.RowCount; ++index)
        this.dgViewSearchSettings[0, index].Value = (object) ((CheckBox) this.dgViewSearchSettings.Controls.Find("checkboxHeader", true)[0]).Checked;
      this.dgViewSearchSettings.EndEdit();
    }

    private void chkHeader_OnCheckBoxClicked(bool state)
    {
      this.dgViewSearchSettings.CellValueChanged -= new DataGridViewCellEventHandler(this.dgViewSearchSettings_CellValueChanged);
      this.dgViewSearchSettings.BeginEdit(true);
      if (this.dgViewSearchSettings.Columns.Count > 0)
      {
        foreach (DataGridViewRow row in (IEnumerable) this.dgViewSearchSettings.Rows)
        {
          if (row.Cells[0] is DataGridViewCheckBoxCell)
            row.Cells[0].Value = (object) state;
        }
      }
      this.dgViewSearchSettings.EndEdit();
      this.dgViewSearchSettings.CellValueChanged += new DataGridViewCellEventHandler(this.dgViewSearchSettings_CellValueChanged);
    }

    private void dgViewSearchSettings_MouseMove(object sender, MouseEventArgs e)
    {
      if ((e.Button & MouseButtons.Left) != MouseButtons.Left || !(this.dragBoxFromMouseDown != Rectangle.Empty) || this.dragBoxFromMouseDown.Contains(e.X, e.Y))
        return;
      int num = (int) this.dgViewSearchSettings.DoDragDrop((object) this.dgViewSearchSettings.Rows[this.rowIndexFromMouseDown], DragDropEffects.Move);
    }

    private void dgViewSearchSettings_DragDrop(object sender, DragEventArgs e)
    {
      Point client = this.dgViewSearchSettings.PointToClient(new Point(e.X, e.Y));
      this.rowIndexOfItemUnderMouseToDrop = this.dgViewSearchSettings.HitTest(client.X, client.Y).RowIndex;
      if (this.rowIndexOfItemUnderMouseToDrop == -1 || e.Effect != DragDropEffects.Move)
        return;
      DataGridViewRow data = e.Data.GetData(typeof (DataGridViewRow)) as DataGridViewRow;
      this.dgViewSearchSettings.Rows.RemoveAt(this.rowIndexFromMouseDown);
      this.dgViewSearchSettings.Rows.Insert(this.rowIndexOfItemUnderMouseToDrop, data);
      try
      {
        this.dgViewSearchSettings.CurrentCell = this.dgViewSearchSettings.Rows[this.rowIndexOfItemUnderMouseToDrop].Cells[1];
      }
      catch
      {
      }
      finally
      {
        this.dragBoxFromMouseDown = new Rectangle();
      }
    }

    private void dgViewSearchSettings_DragOver(object sender, DragEventArgs e)
    {
      e.Effect = DragDropEffects.Move;
    }

    private void dgViewSearchSettings_MouseDown(object sender, MouseEventArgs e)
    {
      this.rowIndexFromMouseDown = this.dgViewSearchSettings.HitTest(e.X, e.Y).RowIndex;
      int columnIndex = this.dgViewSearchSettings.HitTest(e.X, e.Y).ColumnIndex;
      if (columnIndex != -1 && this.rowIndexFromMouseDown != -1)
      {
        this.dgViewSearchSettings.CurrentCell = this.dgViewSearchSettings.Rows[this.rowIndexFromMouseDown].Cells[columnIndex];
        this.dgViewSearchSettings.Rows[this.rowIndexFromMouseDown].Cells[columnIndex].Selected = true;
        this.dragBoxFromMouseDown = Rectangle.Empty;
      }
      else if (this.rowIndexFromMouseDown != -1)
      {
        Size dragSize = SystemInformation.DragSize;
        this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
      }
      else
        this.dragBoxFromMouseDown = Rectangle.Empty;
    }

    private void dgViewSearchSettings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (this.dgViewSearchSettings.Columns.Count <= 0 || e.RowIndex == -1 || e.ColumnIndex != this.dgViewSearchSettings.Columns["select"].Index)
          return;
        int index = 0;
        while (index < this.dgViewSearchSettings.Rows.Count && (!(this.dgViewSearchSettings.Rows[index].Cells[0] is DataGridViewCheckBoxCell) || (bool) this.dgViewSearchSettings.Rows[index].Cells[0].Value))
          ++index;
        DatagridViewCheckBoxHeaderCell headerCell = (DatagridViewCheckBoxHeaderCell) this.dgViewSearchSettings.Columns[0].HeaderCell;
        if (index < this.dgViewSearchSettings.Rows.Count)
          headerCell.Checked = false;
        else
          headerCell.Checked = true;
      }
      catch
      {
      }
    }

    private void dgViewSearchSettings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      try
      {
        if (!(this.dgViewSearchSettings.CurrentCell is DataGridViewCheckBoxCell))
          return;
        this.dgViewSearchSettings.CommitEdit(DataGridViewDataErrorContexts.Commit);
      }
      catch
      {
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblSearchSettings.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgViewSearchSettings.Font = Settings.Default.appFont;
      this.dgViewSearchSettings.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgViewSearchSettings.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading && control != this.lblSearchSettings)
            control.Visible = false;
        }
        this.picLoading.Left = parentPanel.Left + parentPanel.Width / 2 - this.picLoading.Width / 2;
        this.picLoading.Top = parentPanel.Top + parentPanel.Height / 2 - this.picLoading.Height / 2;
        this.picLoading.Parent = (Control) parentPanel;
        this.picLoading.BringToFront();
        this.picLoading.Show();
      }
      catch
      {
      }
    }

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading)
            control.Visible = true;
        }
        this.picLoading.Hide();
      }
      catch
      {
      }
    }

    private void LoadResources()
    {
      this.lblSearchSettings.Text = this.GetResource("Search Settings", "SEARCH_SETTINGS", ResourceType.LABEL);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      try
      {
        this.dgViewSearchSettings.Columns["Field Id"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSearchSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSearchSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSearchSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONFIGURATION']" + "/Screen[@Name='PART_NAME']";
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
      this.lblSearchSettings = new Label();
      this.pnlControl = new Panel();
      this.btnOk = new Button();
      this.btnCancel = new Button();
      this.pnlForm = new Panel();
      this.dgViewSearchSettings = new DataGridView();
      this.picLoading = new PictureBox();
      this.bgWorker = new BackgroundWorker();
      this.pnlControl.SuspendLayout();
      this.pnlForm.SuspendLayout();
      ((ISupportInitialize) this.dgViewSearchSettings).BeginInit();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.lblSearchSettings.BackColor = Color.White;
      this.lblSearchSettings.Dock = DockStyle.Top;
      this.lblSearchSettings.ForeColor = Color.Black;
      this.lblSearchSettings.Location = new Point(0, 0);
      this.lblSearchSettings.Name = "lblSearchSettings";
      this.lblSearchSettings.Padding = new Padding(3, 7, 0, 0);
      this.lblSearchSettings.Size = new Size(448, 27);
      this.lblSearchSettings.TabIndex = 16;
      this.lblSearchSettings.Text = "Search Settings";
      this.pnlControl.Controls.Add((Control) this.btnOk);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 417);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(15, 4, 15, 4);
      this.pnlControl.Size = new Size(448, 31);
      this.pnlControl.TabIndex = 18;
      this.btnOk.Dock = DockStyle.Right;
      this.btnOk.Location = new Point(283, 4);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new Size(75, 23);
      this.btnOk.TabIndex = 1;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new EventHandler(this.btnOk_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(358, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.dgViewSearchSettings);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Controls.Add((Control) this.lblSearchSettings);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(450, 450);
      this.pnlForm.TabIndex = 19;
      this.dgViewSearchSettings.AllowDrop = true;
      this.dgViewSearchSettings.AllowUserToAddRows = false;
      this.dgViewSearchSettings.AllowUserToDeleteRows = false;
      this.dgViewSearchSettings.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.White;
      this.dgViewSearchSettings.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgViewSearchSettings.BackgroundColor = Color.White;
      this.dgViewSearchSettings.BorderStyle = BorderStyle.Fixed3D;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Control;
      gridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.Black;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgViewSearchSettings.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgViewSearchSettings.ColumnHeadersHeight = 26;
      this.dgViewSearchSettings.Dock = DockStyle.Fill;
      this.dgViewSearchSettings.Location = new Point(0, 27);
      this.dgViewSearchSettings.Name = "dgViewSearchSettings";
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle3.BackColor = SystemColors.Control;
      gridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle3.ForeColor = Color.Black;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.dgViewSearchSettings.RowHeadersDefaultCellStyle = gridViewCellStyle3;
      this.dgViewSearchSettings.RowHeadersWidth = 32;
      this.dgViewSearchSettings.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      gridViewCellStyle4.BackColor = Color.White;
      gridViewCellStyle4.ForeColor = Color.Black;
      gridViewCellStyle4.SelectionBackColor = Color.SteelBlue;
      gridViewCellStyle4.SelectionForeColor = Color.White;
      this.dgViewSearchSettings.RowsDefaultCellStyle = gridViewCellStyle4;
      this.dgViewSearchSettings.RowTemplate.Height = 16;
      this.dgViewSearchSettings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgViewSearchSettings.ShowRowErrors = false;
      this.dgViewSearchSettings.Size = new Size(448, 390);
      this.dgViewSearchSettings.TabIndex = 20;
      this.dgViewSearchSettings.CellValueChanged += new DataGridViewCellEventHandler(this.dgViewSearchSettings_CellValueChanged);
      this.dgViewSearchSettings.MouseDown += new MouseEventHandler(this.dgViewSearchSettings_MouseDown);
      this.dgViewSearchSettings.MouseMove += new MouseEventHandler(this.dgViewSearchSettings_MouseMove);
      this.dgViewSearchSettings.DragOver += new DragEventHandler(this.dgViewSearchSettings_DragOver);
      this.dgViewSearchSettings.CurrentCellDirtyStateChanged += new EventHandler(this.dgViewSearchSettings_CurrentCellDirtyStateChanged);
      this.dgViewSearchSettings.DragDrop += new DragEventHandler(this.dgViewSearchSettings_DragDrop);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(2, 2);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 23;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(450, 450);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmConfigSearchPartName);
      this.Load += new EventHandler(this.frmConfigBookPublishingIdSearch_Load);
      this.pnlControl.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      ((ISupportInitialize) this.dgViewSearchSettings).EndInit();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    private delegate void InitializeSearchGridDelegate();

    private delegate void LoadDataGridViewXmlDelegate();
  }
}

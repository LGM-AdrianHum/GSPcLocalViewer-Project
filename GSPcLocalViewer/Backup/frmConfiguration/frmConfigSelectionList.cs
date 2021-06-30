// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConfiguration.frmConfigSelectionList
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

namespace GSPcLocalViewer.frmConfiguration
{
  public class frmConfigSelectionList : Form
  {
    private string sLocalFile = string.Empty;
    private string sServerFile = string.Empty;
    private Dictionary<string, string> dicSLSettings = new Dictionary<string, string>();
    private IContainer components;
    private Panel pnlFrm;
    private Panel panel1;
    private Button btnOK;
    private Button btnCancel;
    private Label lblSelectionSettings;
    private BackgroundWorker bgWorker;
    private PictureBox picLoading;
    public DataGridView dgViewSelectionListSettings;
    private frmConfig frmParent;
    public int serverId;
    private Rectangle dragBoxFromMouseDown;
    private int rowIndexFromMouseDown;
    private int rowIndexOfItemUnderMouseToDrop;
    private Download objDownloader;

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
      this.pnlFrm = new Panel();
      this.picLoading = new PictureBox();
      this.dgViewSelectionListSettings = new DataGridView();
      this.lblSelectionSettings = new Label();
      this.panel1 = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.bgWorker = new BackgroundWorker();
      this.pnlFrm.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      ((ISupportInitialize) this.dgViewSelectionListSettings).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      this.pnlFrm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlFrm.Controls.Add((Control) this.picLoading);
      this.pnlFrm.Controls.Add((Control) this.dgViewSelectionListSettings);
      this.pnlFrm.Controls.Add((Control) this.lblSelectionSettings);
      this.pnlFrm.Controls.Add((Control) this.panel1);
      this.pnlFrm.Dock = DockStyle.Fill;
      this.pnlFrm.Location = new Point(0, 0);
      this.pnlFrm.Name = "pnlFrm";
      this.pnlFrm.Size = new Size(450, 450);
      this.pnlFrm.TabIndex = 0;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(417, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 3;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.dgViewSelectionListSettings.AllowDrop = true;
      this.dgViewSelectionListSettings.AllowUserToAddRows = false;
      this.dgViewSelectionListSettings.AllowUserToDeleteRows = false;
      this.dgViewSelectionListSettings.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.White;
      this.dgViewSelectionListSettings.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgViewSelectionListSettings.BackgroundColor = Color.White;
      this.dgViewSelectionListSettings.BorderStyle = BorderStyle.Fixed3D;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Control;
      gridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.Black;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgViewSelectionListSettings.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgViewSelectionListSettings.ColumnHeadersHeight = 26;
      this.dgViewSelectionListSettings.Dock = DockStyle.Fill;
      this.dgViewSelectionListSettings.Location = new Point(0, 27);
      this.dgViewSelectionListSettings.Name = "dgViewSelectionListSettings";
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle3.BackColor = SystemColors.Control;
      gridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle3.ForeColor = Color.Black;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.dgViewSelectionListSettings.RowHeadersDefaultCellStyle = gridViewCellStyle3;
      this.dgViewSelectionListSettings.RowHeadersWidth = 32;
      this.dgViewSelectionListSettings.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      gridViewCellStyle4.BackColor = Color.White;
      gridViewCellStyle4.ForeColor = Color.Black;
      gridViewCellStyle4.SelectionBackColor = Color.SteelBlue;
      gridViewCellStyle4.SelectionForeColor = Color.White;
      this.dgViewSelectionListSettings.RowsDefaultCellStyle = gridViewCellStyle4;
      this.dgViewSelectionListSettings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgViewSelectionListSettings.ShowRowErrors = false;
      this.dgViewSelectionListSettings.Size = new Size(448, 390);
      this.dgViewSelectionListSettings.TabIndex = 2;
      this.lblSelectionSettings.Dock = DockStyle.Top;
      this.lblSelectionSettings.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblSelectionSettings.Location = new Point(0, 0);
      this.lblSelectionSettings.Name = "lblSelectionSettings";
      this.lblSelectionSettings.Padding = new Padding(3, 7, 0, 0);
      this.lblSelectionSettings.Size = new Size(448, 27);
      this.lblSelectionSettings.TabIndex = 1;
      this.lblSelectionSettings.Text = "List Settings";
      this.panel1.Controls.Add((Control) this.btnOK);
      this.panel1.Controls.Add((Control) this.btnCancel);
      this.panel1.Dock = DockStyle.Bottom;
      this.panel1.Location = new Point(0, 417);
      this.panel1.Name = "panel1";
      this.panel1.Padding = new Padding(15, 4, 15, 4);
      this.panel1.Size = new Size(448, 31);
      this.panel1.TabIndex = 0;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(283, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(358, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(450, 450);
      this.Controls.Add((Control) this.pnlFrm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmConfigSelectionList);
      this.Text = "Selection List Configurations";
      this.Load += new EventHandler(this.frmConfigSelectionList_Load);
      this.pnlFrm.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      ((ISupportInitialize) this.dgViewSelectionListSettings).EndInit();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmConfigSelectionList(frmConfig frm, int _serverId)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.serverId = _serverId;
      this.objDownloader = new Download();
      this.UpdateFont();
    }

    private void frmConfigSelectionList_Load(object sender, EventArgs e)
    {
      this.ShowLoading(this.pnlFrm);
      this.bgWorker.RunWorkerAsync();
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.InitializeSelectionListGrid();
      this.LoadDataGridView();
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.HideLoading(this.pnlFrm);
      this.SetControlsText();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.frmParent.CloseAndSaveSettings();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.frmParent.Close();
    }

    private void InitializeSelectionListGrid()
    {
      if (this.dgViewSelectionListSettings.InvokeRequired)
      {
        this.dgViewSelectionListSettings.Invoke((Delegate) new frmConfigSelectionList.InitializeSelectionListGridDelegate(this.InitializeSelectionListGrid));
      }
      else
      {
        try
        {
          DataGridViewCheckBoxColumn viewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
          viewCheckBoxColumn1.Name = "EnableDisplay";
          viewCheckBoxColumn1.HeaderText = "Disp";
          viewCheckBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewCheckBoxColumn1.Width = 50;
          this.dgViewSelectionListSettings.Columns.Add((DataGridViewColumn) viewCheckBoxColumn1);
          DataGridViewCheckBoxColumn viewCheckBoxColumn2 = new DataGridViewCheckBoxColumn();
          viewCheckBoxColumn2.Name = "EnablePrint";
          viewCheckBoxColumn2.HeaderText = "Prt";
          viewCheckBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewCheckBoxColumn2.Width = 50;
          this.dgViewSelectionListSettings.Columns.Add((DataGridViewColumn) viewCheckBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn1.Name = "FieldId";
          viewTextBoxColumn1.HeaderText = "Field Id";
          viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn1.Width = 100;
          viewTextBoxColumn1.ReadOnly = true;
          this.dgViewSelectionListSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
          DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn2.Name = "DisplayName";
          viewTextBoxColumn2.HeaderText = "Display Name";
          viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn2.Width = 100;
          viewTextBoxColumn2.ReadOnly = true;
          this.dgViewSelectionListSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn3.Name = "Width";
          viewTextBoxColumn3.HeaderText = "Print Width";
          viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn3.Width = 100;
          this.dgViewSelectionListSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
          DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
          viewComboBoxColumn.Name = "Alignment";
          viewComboBoxColumn.HeaderText = "Print Position";
          viewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewComboBoxColumn.Width = 100;
          this.dgViewSelectionListSettings.Columns.Add((DataGridViewColumn) viewComboBoxColumn);
        }
        catch
        {
        }
      }
    }

    private void LoadDataGridView()
    {
      if (this.dgViewSelectionListSettings.InvokeRequired)
      {
        this.dgViewSelectionListSettings.Invoke((Delegate) new frmConfigSelectionList.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
      }
      else
      {
        this.dgViewSelectionListSettings.Rows.Clear();
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        ArrayList keys = new IniFileIO().GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini", "SLIST_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          try
          {
            string keyValue = new IniFileIO().GetKeyValue("SLIST_SETTINGS", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini");
            this.AddSelectionListSettingsRow(keyValue, keys[index].ToString());
            string key = keys[index].ToString();
            string[] strArray = keyValue.Split('|');
            if (strArray.Length > 0)
            {
              string str = strArray[1] + "," + strArray[2];
              this.dicSLSettings.Add(key, str);
            }
          }
          catch (Exception ex)
          {
          }
        }
      }
    }

    private void AddSelectionListSettingsRow(string sVal, string sKey)
    {
      try
      {
        string[] strArray1 = sVal.Split('|');
        if (strArray1.Length == 3)
          sVal = sVal + "|True|True|" + strArray1[1] + "|" + strArray1[2];
        else if (strArray1.Length == 4)
          sVal = sVal + "|True|True|" + strArray1[1] + "|" + strArray1[2];
        string[] strArray2 = sVal.Split('|');
        DataGridViewRow dataGridViewRow = new DataGridViewRow();
        DataGridViewCheckBoxCell viewCheckBoxCell1 = new DataGridViewCheckBoxCell();
        viewCheckBoxCell1.Value = (object) strArray2[3];
        dataGridViewRow.Cells.Add((DataGridViewCell) viewCheckBoxCell1);
        DataGridViewCheckBoxCell viewCheckBoxCell2 = new DataGridViewCheckBoxCell();
        viewCheckBoxCell2.Value = (object) strArray2[4];
        dataGridViewRow.Cells.Add((DataGridViewCell) viewCheckBoxCell2);
        DataGridViewTextBoxCell gridViewTextBoxCell1 = new DataGridViewTextBoxCell();
        gridViewTextBoxCell1.Value = (object) sKey;
        dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell1);
        DataGridViewTextBoxCell gridViewTextBoxCell2 = new DataGridViewTextBoxCell();
        if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
          gridViewTextBoxCell2.Value = (object) Global.GetDGHeaderCellValue("SLIST_SETTINGS", sKey, strArray2[0], this.serverId);
        else
          gridViewTextBoxCell2.Value = (object) strArray2[0];
        dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell2);
        DataGridViewTextBoxCell gridViewTextBoxCell3 = new DataGridViewTextBoxCell();
        gridViewTextBoxCell3.Value = (object) strArray2[6];
        dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell3);
        DataGridViewComboBoxCell viewComboBoxCell = new DataGridViewComboBoxCell();
        string[] strArray3 = "L,R,C".ToString().Split(',');
        viewComboBoxCell.Items.AddRange((object[]) strArray3);
        switch (strArray2[5].ToString())
        {
          case "L":
            viewComboBoxCell.Value = (object) strArray3[0];
            break;
          case "R":
            viewComboBoxCell.Value = (object) strArray3[1];
            break;
          case "C":
            viewComboBoxCell.Value = (object) strArray3[2];
            break;
          default:
            viewComboBoxCell.Value = (object) strArray3[0];
            break;
        }
        dataGridViewRow.Cells.Add((DataGridViewCell) viewComboBoxCell);
        dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
        this.dgViewSelectionListSettings.Rows.Add(dataGridViewRow);
      }
      catch (Exception ex)
      {
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblSelectionSettings.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgViewSelectionListSettings.Font = Settings.Default.appFont;
      this.dgViewSelectionListSettings.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgViewSelectionListSettings.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
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

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading && control != this.lblSelectionSettings)
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

    public bool SaveSettings()
    {
      try
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (DataGridViewRow row in (IEnumerable) this.dgViewSelectionListSettings.Rows)
        {
          DataGridViewCheckBoxCell cell1 = (DataGridViewCheckBoxCell) row.Cells[0];
          DataGridViewCheckBoxCell cell2 = (DataGridViewCheckBoxCell) row.Cells[1];
          if (Convert.ToBoolean(cell1.Value))
            flag1 = true;
          if (Convert.ToBoolean(cell2.Value))
            flag2 = true;
          if (flag2)
          {
            if (flag1)
              break;
          }
        }
        if (flag1 && flag2)
        {
          string empty = string.Empty;
          if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
          {
            for (int index = 0; index < this.dgViewSelectionListSettings.Rows.Count; ++index)
              Global.SaveToLanguageIni("SLIST_SETTINGS", this.dgViewSelectionListSettings[2, index].Value.ToString(), this.dgViewSelectionListSettings[3, index].Value.ToString(), this.serverId);
          }
          for (int index = 0; index < this.dgViewSelectionListSettings.Rows.Count; ++index)
          {
            string str1 = this.dgViewSelectionListSettings[0, index].Value.ToString();
            string str2 = this.dgViewSelectionListSettings[1, index].Value.ToString();
            string sKey = this.dgViewSelectionListSettings[2, index].Value.ToString();
            string str3 = Global.GetEngHeaderVal("SLIST_SETTINGS", this.dgViewSelectionListSettings[2, index].Value.ToString(), this.serverId).Split('|')[0];
            string str4 = this.dgViewSelectionListSettings[4, index].Value.ToString();
            string str5 = this.dgViewSelectionListSettings[5, index].Value.ToString();
            string str6 = this.dicSLSettings[sKey].ToString();
            string str7 = str6.Split(',')[0].ToString();
            string str8 = str6.Split(',')[1].ToString();
            string sHeaderValue = str3 + "|" + str7 + "|" + str8 + "|" + str1 + "|" + str2 + "|" + str5 + "|" + str4;
            if (!(Settings.Default.appLanguage.ToUpper() != "ENGLISH"))
              this.dgViewSelectionListSettings[3, index].Value.ToString();
            Global.SaveToServerIni("SLIST_SETTINGS", sKey, sHeaderValue, this.serverId);
          }
          return true;
        }
        int num = (int) MessageBox.Show(this.GetResource("Please select always one or more Disp and Prt.", "SELECT_ONE_CHECK_BOX", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private void SetControlsText()
    {
      this.Text = this.GetResource("Selection List Configurations", "SELECTION_LIST_CONFIG", ResourceType.LABEL);
      this.lblSelectionSettings.Text = this.GetResource("List Settings", "LIST_SETTINGS", ResourceType.LABEL);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
      try
      {
        this.dgViewSelectionListSettings.Columns["EnableDisplay"].HeaderText = this.GetResource("Enable Display", "ENABLE_DISPALY", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSelectionListSettings.Columns["EnablePrint"].HeaderText = this.GetResource("Enable Print", "ENABLE_PRINT", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSelectionListSettings.Columns["FieldId"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSelectionListSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSelectionListSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewSelectionListSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONFIGURATION']" + "/Screen[@Name='SELECTIONLIST_SETTINGS']";
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

    private delegate void InitializeSelectionListGridDelegate();

    private delegate void LoadDataGridViewXmlDelegate();
  }
}

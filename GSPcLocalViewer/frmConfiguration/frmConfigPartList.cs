// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConfiguration.frmConfigPartList
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
  public class frmConfigPartList : Form
  {
    private string sLocalFile = string.Empty;
    private string sServerFile = string.Empty;
    private Dictionary<string, string> dicPLSettings = new Dictionary<string, string>();
    private frmConfig frmParent;
    private int serverId;
    private Rectangle dragBoxFromMouseDown;
    private int rowIndexFromMouseDown;
    private int rowIndexOfItemUnderMouseToDrop;
    private Download objDownloader;
    private IContainer components;
    private Panel pnlFrm;
    private Panel PnlControl;
    private Button btnOK;
    private Button btnCancel;
    private Label lblPartListSettings;
    private PictureBox picLoading;
    private BackgroundWorker bgWorker;
    public DataGridView dgViewPartListSettings;

    public frmConfigPartList(frmConfig frm, int _serverId)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.serverId = _serverId;
      this.objDownloader = new Download();
      this.UpdateFont();
    }

    private void frmConfigPartList_Load(object sender, EventArgs e)
    {
      this.ShowLoading(this.pnlFrm);
      this.bgWorker.RunWorkerAsync();
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.InitializePartsListGrid();
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

    private void InitializePartsListGrid()
    {
      if (this.dgViewPartListSettings.InvokeRequired)
      {
        this.dgViewPartListSettings.Invoke((Delegate) new frmConfigPartList.InitializePartsListGridDelegate(this.InitializePartsListGrid));
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
          this.dgViewPartListSettings.Columns.Add((DataGridViewColumn) viewCheckBoxColumn1);
          DataGridViewCheckBoxColumn viewCheckBoxColumn2 = new DataGridViewCheckBoxColumn();
          viewCheckBoxColumn2.Name = "EnablePrint";
          viewCheckBoxColumn2.HeaderText = "Prt";
          viewCheckBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewCheckBoxColumn2.Width = 50;
          this.dgViewPartListSettings.Columns.Add((DataGridViewColumn) viewCheckBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn1.Name = "FieldId";
          viewTextBoxColumn1.HeaderText = "Field Id";
          viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn1.Width = 100;
          viewTextBoxColumn1.ReadOnly = true;
          this.dgViewPartListSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
          DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn2.Name = "DisplayName";
          viewTextBoxColumn2.HeaderText = "Display Name";
          viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn2.Width = 100;
          viewTextBoxColumn2.ReadOnly = true;
          this.dgViewPartListSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn3.Name = "Width";
          viewTextBoxColumn3.HeaderText = "Print Width";
          viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn3.Width = 100;
          this.dgViewPartListSettings.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
          DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
          viewComboBoxColumn.Name = "Alignment";
          viewComboBoxColumn.HeaderText = "Print Position";
          viewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewComboBoxColumn.Width = 100;
          this.dgViewPartListSettings.Columns.Add((DataGridViewColumn) viewComboBoxColumn);
        }
        catch
        {
        }
      }
    }

    private void LoadDataGridView()
    {
      if (this.dgViewPartListSettings.InvokeRequired)
      {
        this.dgViewPartListSettings.Invoke((Delegate) new frmConfigPartList.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
      }
      else
      {
        this.dgViewPartListSettings.Rows.Clear();
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        ArrayList keys = new IniFileIO().GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini", "PLIST_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          try
          {
            string keyValue = new IniFileIO().GetKeyValue("PLIST_SETTINGS", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini");
            this.AddPartListSettingsRow(keyValue, keys[index].ToString());
            string key = keys[index].ToString();
            string[] strArray = keyValue.Split('|');
            if (strArray.Length > 0)
            {
              string str = strArray[1] + "," + strArray[2];
              this.dicPLSettings.Add(key, str);
            }
          }
          catch (Exception ex)
          {
          }
        }
      }
    }

    private void AddPartListSettingsRow(string sVal, string sKey)
    {
      try
      {
        string[] strArray1 = sVal.Split('|');
        if (strArray1.Length == 3)
          sVal = sVal + "|True|True|" + strArray1[1] + "|" + strArray1[2];
        else if (strArray1.Length == 4)
          sVal = sVal + "|True|True|" + strArray1[1] + "|" + strArray1[2];
        string[] strArray2 = sVal.Split('|');
        if (strArray2.Length == 7)
        {
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
            gridViewTextBoxCell2.Value = (object) Global.GetDGHeaderCellValue("PLIST_SETTINGS", sKey, strArray2[0], this.serverId);
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
          this.dgViewPartListSettings.Rows.Add(dataGridViewRow);
        }
        else
        {
          if (strArray2.Length != 8)
            return;
          DataGridViewRow dataGridViewRow = new DataGridViewRow();
          DataGridViewCheckBoxCell viewCheckBoxCell1 = new DataGridViewCheckBoxCell();
          viewCheckBoxCell1.Value = (object) strArray2[4];
          dataGridViewRow.Cells.Add((DataGridViewCell) viewCheckBoxCell1);
          DataGridViewCheckBoxCell viewCheckBoxCell2 = new DataGridViewCheckBoxCell();
          viewCheckBoxCell2.Value = (object) strArray2[5];
          dataGridViewRow.Cells.Add((DataGridViewCell) viewCheckBoxCell2);
          DataGridViewTextBoxCell gridViewTextBoxCell1 = new DataGridViewTextBoxCell();
          gridViewTextBoxCell1.Value = (object) sKey;
          dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell1);
          DataGridViewTextBoxCell gridViewTextBoxCell2 = new DataGridViewTextBoxCell();
          gridViewTextBoxCell2.Value = (object) strArray2[0];
          dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell2);
          DataGridViewTextBoxCell gridViewTextBoxCell3 = new DataGridViewTextBoxCell();
          gridViewTextBoxCell3.Value = (object) strArray2[7];
          dataGridViewRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell3);
          DataGridViewComboBoxCell viewComboBoxCell = new DataGridViewComboBoxCell();
          string[] strArray3 = "L,R,C".ToString().Split(',');
          viewComboBoxCell.Items.AddRange((object[]) strArray3);
          switch (strArray2[6].ToString())
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
          this.dgViewPartListSettings.Rows.Add(dataGridViewRow);
        }
      }
      catch (Exception ex)
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

    public bool SaveSettings()
    {
      try
      {
        bool flag1 = false;
        bool flag2 = false;
        foreach (DataGridViewRow row in (IEnumerable) this.dgViewPartListSettings.Rows)
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
          string empty1 = string.Empty;
          if (this.dgViewPartListSettings.Rows.Count > 0 && Settings.Default.appLanguage.ToUpper() != "ENGLISH")
          {
            for (int index = 0; index < this.dgViewPartListSettings.Rows.Count; ++index)
              Global.SaveToLanguageIni("PLIST_SETTINGS", this.dgViewPartListSettings[2, index].Value.ToString(), this.dgViewPartListSettings[3, index].Value.ToString(), this.serverId);
          }
          for (int index = 0; index < this.dgViewPartListSettings.Rows.Count; ++index)
          {
            string empty2 = string.Empty;
            string str1 = this.dgViewPartListSettings[0, index].Value.ToString();
            string str2 = this.dgViewPartListSettings[1, index].Value.ToString();
            string sKey = this.dgViewPartListSettings[2, index].Value.ToString();
            string[] strArray = Global.GetEngHeaderVal("PLIST_SETTINGS", this.dgViewPartListSettings[2, index].Value.ToString(), this.serverId).Split('|');
            string str3 = strArray[0];
            string str4 = this.dgViewPartListSettings[4, index].Value.ToString();
            string str5 = this.dgViewPartListSettings[5, index].Value.ToString();
            string str6 = this.dicPLSettings[sKey].ToString();
            string str7 = str6.Split(',')[0].ToString();
            string str8 = str6.Split(',')[1].ToString();
            string sHeaderValue1;
            if (strArray.Length == 4 || strArray.Length == 8)
              sHeaderValue1 = str3 + "|" + str7 + "|" + str8 + "|" + strArray[3] + "|" + str1 + "|" + str2 + "|" + str5 + "|" + str4;
            else
              sHeaderValue1 = str3 + "|" + str7 + "|" + str8 + "|" + str1 + "|" + str2 + "|" + str5 + "|" + str4;
            string sHeaderValue2 = str3 + "|" + str7 + "|" + str8 + "|MEM|" + str1 + "|" + str2 + "|" + str5 + "|" + str4;
            if (!(Settings.Default.appLanguage.ToUpper() != "ENGLISH"))
              this.dgViewPartListSettings[3, index].Value.ToString();
            if (sKey.ToUpper() == "MEM")
              Global.SaveToServerIni("PLIST_SETTINGS", sKey, sHeaderValue2, this.serverId);
            else
              Global.SaveToServerIni("PLIST_SETTINGS", sKey, sHeaderValue1, this.serverId);
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

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblPartListSettings.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgViewPartListSettings.Font = Settings.Default.appFont;
      this.dgViewPartListSettings.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgViewPartListSettings.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading && control != this.lblPartListSettings)
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

    private void SetControlsText()
    {
      this.Text = this.GetResource("Parts List Configurations", "PARTS_LIST_CONFIG", ResourceType.LABEL);
      this.lblPartListSettings.Text = this.GetResource("List Settings", "LIST_SETTINGS", ResourceType.LABEL);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
      try
      {
        this.dgViewPartListSettings.Columns["EnableDisplay"].HeaderText = this.GetResource("Enable Display", "ENABLE_DISPALY", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewPartListSettings.Columns["EnablePrint"].HeaderText = this.GetResource("Enable Print", "ENABLE_PRINT", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewPartListSettings.Columns["FieldId"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewPartListSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewPartListSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewPartListSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONFIGURATION']" + "/Screen[@Name='PARTSLIST_SETTINGS']";
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
      this.pnlFrm = new Panel();
      this.picLoading = new PictureBox();
      this.dgViewPartListSettings = new DataGridView();
      this.lblPartListSettings = new Label();
      this.PnlControl = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.bgWorker = new BackgroundWorker();
      this.pnlFrm.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      ((ISupportInitialize) this.dgViewPartListSettings).BeginInit();
      this.PnlControl.SuspendLayout();
      this.SuspendLayout();
      this.pnlFrm.BackColor = Color.White;
      this.pnlFrm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlFrm.Controls.Add((Control) this.picLoading);
      this.pnlFrm.Controls.Add((Control) this.dgViewPartListSettings);
      this.pnlFrm.Controls.Add((Control) this.lblPartListSettings);
      this.pnlFrm.Controls.Add((Control) this.PnlControl);
      this.pnlFrm.Dock = DockStyle.Fill;
      this.pnlFrm.Location = new Point(0, 0);
      this.pnlFrm.Name = "pnlFrm";
      this.pnlFrm.Size = new Size(450, 450);
      this.pnlFrm.TabIndex = 0;
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(2, 2);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 3;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.dgViewPartListSettings.AllowDrop = true;
      this.dgViewPartListSettings.AllowUserToAddRows = false;
      this.dgViewPartListSettings.AllowUserToDeleteRows = false;
      this.dgViewPartListSettings.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.White;
      this.dgViewPartListSettings.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgViewPartListSettings.BackgroundColor = Color.White;
      this.dgViewPartListSettings.BorderStyle = BorderStyle.Fixed3D;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Control;
      gridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.Black;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgViewPartListSettings.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgViewPartListSettings.ColumnHeadersHeight = 26;
      this.dgViewPartListSettings.Dock = DockStyle.Fill;
      this.dgViewPartListSettings.Location = new Point(0, 27);
      this.dgViewPartListSettings.Name = "dgViewPartListSettings";
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle3.BackColor = SystemColors.Control;
      gridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle3.ForeColor = Color.Black;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.dgViewPartListSettings.RowHeadersDefaultCellStyle = gridViewCellStyle3;
      this.dgViewPartListSettings.RowHeadersWidth = 32;
      this.dgViewPartListSettings.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      gridViewCellStyle4.BackColor = Color.White;
      gridViewCellStyle4.ForeColor = Color.Black;
      gridViewCellStyle4.SelectionBackColor = Color.SteelBlue;
      gridViewCellStyle4.SelectionForeColor = Color.White;
      this.dgViewPartListSettings.RowsDefaultCellStyle = gridViewCellStyle4;
      this.dgViewPartListSettings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgViewPartListSettings.ShowRowErrors = false;
      this.dgViewPartListSettings.Size = new Size(448, 390);
      this.dgViewPartListSettings.TabIndex = 2;
      this.lblPartListSettings.BackColor = Color.White;
      this.lblPartListSettings.Dock = DockStyle.Top;
      this.lblPartListSettings.ForeColor = Color.Black;
      this.lblPartListSettings.Location = new Point(0, 0);
      this.lblPartListSettings.Name = "lblPartListSettings";
      this.lblPartListSettings.Padding = new Padding(3, 7, 0, 0);
      this.lblPartListSettings.Size = new Size(448, 27);
      this.lblPartListSettings.TabIndex = 1;
      this.lblPartListSettings.Text = "List Settings";
      this.PnlControl.Controls.Add((Control) this.btnOK);
      this.PnlControl.Controls.Add((Control) this.btnCancel);
      this.PnlControl.Dock = DockStyle.Bottom;
      this.PnlControl.Location = new Point(0, 417);
      this.PnlControl.Name = "PnlControl";
      this.PnlControl.Padding = new Padding(15, 4, 15, 4);
      this.PnlControl.Size = new Size(448, 31);
      this.PnlControl.TabIndex = 0;
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
      this.Name = nameof (frmConfigPartList);
      this.Text = "Parts List Configurations";
      this.Load += new EventHandler(this.frmConfigPartList_Load);
      this.pnlFrm.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      ((ISupportInitialize) this.dgViewPartListSettings).EndInit();
      this.PnlControl.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private delegate void InitializePartsListGridDelegate();

    private delegate void LoadDataGridViewXmlDelegate();
  }
}

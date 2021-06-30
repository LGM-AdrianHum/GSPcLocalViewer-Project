// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmAddRecord
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class frmAddRecord : Form
  {
    private IContainer components;
    private Panel pnlForm;
    private DataGridView dgPartslist;
    private Panel pnlControl;
    private Button btnSave;
    private Button btnCancel;
    private frmViewer frmParent;
    private DataGridViewColumnCollection dgCols;
    private int serverId;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlForm = new Panel();
      this.dgPartslist = new DataGridView();
      this.pnlControl = new Panel();
      this.btnSave = new Button();
      this.btnCancel = new Button();
      this.pnlForm.SuspendLayout();
      ((ISupportInitialize) this.dgPartslist).BeginInit();
      this.pnlControl.SuspendLayout();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.Controls.Add((Control) this.dgPartslist);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(5);
      this.pnlForm.Size = new Size(342, 256);
      this.pnlForm.TabIndex = 0;
      this.dgPartslist.AccessibleRole = AccessibleRole.None;
      this.dgPartslist.AllowUserToAddRows = false;
      this.dgPartslist.AllowUserToDeleteRows = false;
      this.dgPartslist.AllowUserToResizeRows = false;
      this.dgPartslist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgPartslist.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
      this.dgPartslist.BackgroundColor = Color.White;
      this.dgPartslist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgPartslist.ColumnHeadersVisible = false;
      this.dgPartslist.Dock = DockStyle.Fill;
      this.dgPartslist.Location = new Point(5, 5);
      this.dgPartslist.Margin = new Padding(60);
      this.dgPartslist.Name = "dgPartslist";
      this.dgPartslist.RowHeadersVisible = false;
      this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgPartslist.Size = new Size(332, 215);
      this.dgPartslist.TabIndex = 21;
      this.dgPartslist.KeyDown += new KeyEventHandler(this.dgPartslist_KeyDown);
      this.dgPartslist.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dgPartslist_ColumnWidthChanged);
      this.pnlControl.Controls.Add((Control) this.btnSave);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(5, 220);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 6, 0, 2);
      this.pnlControl.Size = new Size(332, 31);
      this.pnlControl.TabIndex = 22;
      this.btnSave.Dock = DockStyle.Right;
      this.btnSave.Location = new Point(182, 6);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new Size(75, 23);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new EventHandler(this.btnSave_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(257, 6);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(342, 256);
      this.Controls.Add((Control) this.pnlForm);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(350, 290);
      this.Name = nameof (frmAddRecord);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Add Record";
      this.pnlForm.ResumeLayout(false);
      ((ISupportInitialize) this.dgPartslist).EndInit();
      this.pnlControl.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmAddRecord(frmViewer objFrmViewer, DataGridViewColumnCollection dgCols1, int _serverId)
    {
      this.InitializeComponent();
      try
      {
        this.frmParent = objFrmViewer;
        this.dgCols = dgCols1;
        this.serverId = _serverId;
        DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        viewTextBoxColumn1.ReadOnly = true;
        viewTextBoxColumn1.Name = "Name";
        viewTextBoxColumn1.DefaultCellStyle.BackColor = Color.WhiteSmoke;
        DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        viewTextBoxColumn2.Name = "Value";
        this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
        this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        ArrayList keys = new IniFileIO().GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini", "SLIST_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          new IniFileIO().GetKeyValue("SLIST_SETTINGS", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.serverId].sIniKey + ".ini");
          this.dgPartslist.Rows.Add();
        }
        DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
        viewTextBoxColumn3.Name = "PART_SLIST_KEY";
        viewTextBoxColumn3.Visible = false;
        this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
        int index1 = 0;
        foreach (DataGridViewColumn dgCol in (BaseCollection) this.dgCols)
        {
          this.dgPartslist["Name", index1].Value = (object) dgCol.HeaderText;
          this.dgPartslist["Name", index1].Tag = (object) dgCol.Name;
          ++index1;
        }
        this.Height = this.dgPartslist.Rows.Count * 22 + this.pnlControl.Height + 35;
      }
      catch
      {
      }
      this.LoadResources();
      this.UpdateFont();
    }

    private void dgPartslist_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.btnSave_Click((object) null, (EventArgs) null);
    }

    private void dgPartslist_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
    {
      this.Refresh();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        this.dgPartslist.EndEdit();
        bool flag = false;
        if (this.dgPartslist.Rows.Count > 0)
        {
          for (int index = 0; index < this.dgPartslist.Rows.Count; ++index)
          {
            if (this.dgPartslist["Name", index].Tag.ToString().ToUpper() == "PARTNUMBER")
            {
              if (this.dgPartslist["Value", index].Value == null)
              {
                int num = (int) MessageBox.Show(this.GetResource("(W-ARD-WM001) Part number field must not be empty", "(W-ARD-WM001)_EMPTY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
              }
              if (this.frmParent.PartInSelectionList(this.dgPartslist["Value", index].Value.ToString().Trim()))
              {
                int num = (int) MessageBox.Show(this.GetResource("(W-ARD-WM004) Part already exists in selection list", "(W-ARD-WM004)_PART", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
              }
            }
            if (this.dgPartslist["Name", index].Tag.ToString().ToUpper() == "QTY")
            {
              if (this.dgPartslist["Value", index].Value == null)
              {
                int num = (int) MessageBox.Show(this.GetResource("(W-ARD-WM002) Quantity field must not be empty", "(W-ARD-WM002)_QUANTITY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
              }
              long result = -1;
              if (!long.TryParse(this.dgPartslist["Value", index].Value.ToString(), out result))
              {
                int num = (int) MessageBox.Show(this.GetResource("(W-ARD-WM003) Quantity must be numeric", "(W-ARD-WM003)_NUMERIC", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
              }
            }
            if (this.dgPartslist["Value", index].Value != null)
              flag = true;
          }
          if (!flag)
          {
            int num1 = (int) MessageBox.Show(this.GetResource("(W-ARD-WM005) All fields must not be empty", "(W-ARD-WM005)_EMPTY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          else
          {
            DataGridView dataGridView = new DataGridView();
            int num2 = 9999;
            foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
            {
              DataGridViewCell cell = row.Cells[1];
              dataGridView.Columns.Add(row.Cells[0].Tag.ToString(), row.Cells[0].Value.ToString());
              if (row.Cells[0].Tag.ToString().ToUpper() == "PARTNUMBER")
                num2 = row.Index;
            }
            dataGridView.Columns.Add("PART_SLIST_KEY", "PART_SLIST_KEY");
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(dataGridView);
            int index = 0;
            foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
            {
              if (index == num2)
              {
                dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].Value = row.Cells[1].Value;
                dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].ToolTipText = "HIDDEN";
                num2 = 8888;
              }
              dataGridViewRow.Cells[index].Value = row.Cells[1].Value;
              ++index;
              if (num2 != 8888 && row.Cells[1].Value != null && row.Cells[1].Value.ToString() != "")
              {
                dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].Value = row.Cells[1].Value;
                dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].ToolTipText = "HIDDEN";
              }
            }
            dataGridView.Rows.Add(dataGridViewRow);
            this.frmParent.AddNewRecord(dataGridView.Rows[0]);
            this.Close();
          }
        }
        else
          this.Close();
      }
      catch
      {
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    public void LoadResources()
    {
      this.Text = this.GetResource("Add Record", "ADD_RECORD", ResourceType.TITLE);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      this.btnSave.Text = this.GetResource("Save", "SAVE", ResourceType.BUTTON);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MAIN_FORM']" + "/Screen[@Name='ADD_RECORD']";
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

    public void UpdateFont()
    {
      this.dgPartslist.Font = Settings.Default.appFont;
      this.dgPartslist.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgPartslist.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }
  }
}

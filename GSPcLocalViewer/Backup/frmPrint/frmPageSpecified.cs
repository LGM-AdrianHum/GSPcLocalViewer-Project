// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmPrint.frmPageSpecified
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmPrint
{
  public class frmPageSpecified : Form
  {
    private IContainer components;
    private Panel pnlGridView;
    private Panel pnlButtons;
    private DataGridView dgViewPrintList;
    private Button btnCancel;
    private Button btnOK;
    private BackgroundWorker bgWorker;
    private GSPcLocalViewer.frmPrint.frmPrint frmParent;
    private int intServerId;
    private bool bClearSelection;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlGridView = new Panel();
      this.dgViewPrintList = new DataGridView();
      this.pnlButtons = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.bgWorker = new BackgroundWorker();
      this.pnlGridView.SuspendLayout();
      ((ISupportInitialize) this.dgViewPrintList).BeginInit();
      this.pnlButtons.SuspendLayout();
      this.SuspendLayout();
      this.pnlGridView.Controls.Add((Control) this.dgViewPrintList);
      this.pnlGridView.Dock = DockStyle.Top;
      this.pnlGridView.Location = new Point(0, 0);
      this.pnlGridView.Name = "pnlGridView";
      this.pnlGridView.Size = new Size(518, 333);
      this.pnlGridView.TabIndex = 0;
      this.dgViewPrintList.AllowUserToAddRows = false;
      this.dgViewPrintList.AllowUserToResizeRows = false;
      this.dgViewPrintList.BackgroundColor = Color.White;
      this.dgViewPrintList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgViewPrintList.Dock = DockStyle.Fill;
      this.dgViewPrintList.Location = new Point(0, 0);
      this.dgViewPrintList.MultiSelect = false;
      this.dgViewPrintList.Name = "dgViewPrintList";
      this.dgViewPrintList.RowHeadersVisible = false;
      this.dgViewPrintList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgViewPrintList.Size = new Size(518, 333);
      this.dgViewPrintList.TabIndex = 0;
      this.dgViewPrintList.TabStop = false;
      this.dgViewPrintList.MouseUp += new MouseEventHandler(this.dgViewPrintList_MouseUp);
      this.pnlButtons.Controls.Add((Control) this.btnOK);
      this.pnlButtons.Controls.Add((Control) this.btnCancel);
      this.pnlButtons.Dock = DockStyle.Bottom;
      this.pnlButtons.Location = new Point(0, 333);
      this.pnlButtons.Name = "pnlButtons";
      this.pnlButtons.Padding = new Padding(15, 4, 15, 4);
      this.pnlButtons.Size = new Size(518, 31);
      this.pnlButtons.TabIndex = 1;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(353, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(428, 4);
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
      this.ClientSize = new Size(518, 364);
      this.Controls.Add((Control) this.pnlButtons);
      this.Controls.Add((Control) this.pnlGridView);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.Fixed3D;
      this.MaximizeBox = false;
      this.MaximumSize = new Size(528, 400);
      this.MinimizeBox = false;
      this.Name = nameof (frmPageSpecified);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Page Specified";
      this.Load += new EventHandler(this.frmPageSpecified_Load);
      this.pnlGridView.ResumeLayout(false);
      ((ISupportInitialize) this.dgViewPrintList).EndInit();
      this.pnlButtons.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmPageSpecified(GSPcLocalViewer.frmPrint.frmPrint frm, int intTempServerID)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.intServerId = intTempServerID;
    }

    private void frmPageSpecified_Load(object sender, EventArgs e)
    {
      this.UpdateFont();
      this.initilizePageSpecifiedGrid();
      this.EnableAndDisableButtons();
      this.bgWorker.RunWorkerAsync();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      try
      {
        this.frmParent.frmParent.EnableTreeView(false);
        this.frmParent.Enabled = true;
        e.Cancel = false;
        this.Dispose();
      }
      catch (Exception ex)
      {
      }
    }

    private void dgViewPrintList_DeleteClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.ColumnIndex == 3)
        {
          this.dgViewPrintList.Rows.RemoveAt(e.RowIndex);
          this.bClearSelection = true;
        }
        this.dgViewPrintList.ClearSelection();
        if (this.dgViewPrintList.Rows.Count > 0)
          this.updateGridSerialNo();
        this.dgViewPrintList.Update();
      }
      catch (Exception ex)
      {
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.CheckToAndFromColumns() == 1)
      {
        this.CopyGridView();
        this.EnablePrintControls();
        this.frmParent.Enabled = true;
        this.Close();
      }
      else
      {
        int num = (int) MessageBox.Show(this.GetResource("Please be sure to set the From and To.", "FILL_FROM_AND_TO", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.frmParent.Enabled = true;
      this.Close();
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.SetControlsText();
    }

    private void dgViewPrintList_MouseUp(object sender, MouseEventArgs e)
    {
      try
      {
        if (e.Button != MouseButtons.Left)
          return;
        if (this.dgViewPrintList.HitTest(e.X, e.Y) == DataGridView.HitTestInfo.Nowhere)
        {
          this.dgViewPrintList.ClearSelection();
          this.dgViewPrintList.CurrentCell = (DataGridViewCell) null;
        }
        else
        {
          if (this.dgViewPrintList.Rows.Count <= 0)
            return;
          this.dgViewPrintList.Rows[this.dgViewPrintList.HitTest(e.X, e.Y).RowIndex].Selected = true;
          if (!this.bClearSelection)
            return;
          this.bClearSelection = false;
          this.dgViewPrintList.ClearSelection();
        }
      }
      catch (Exception ex)
      {
      }
    }

    private int CheckToAndFromColumns()
    {
      int num = 1;
      try
      {
        int count = this.dgViewPrintList.Rows.Count;
        num = !(this.dgViewPrintList.Rows[count - 1].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[count - 1].Cells[2].Value.ToString() != "") ? 0 : 1;
      }
      catch (Exception ex)
      {
      }
      return num;
    }

    private void EnableAndDisableButtons()
    {
      if (this.dgViewPrintList.Rows.Count == 0)
        this.btnOK.Enabled = false;
      else
        this.btnOK.Enabled = true;
    }

    private void initilizePageSpecifiedGrid()
    {
      try
      {
        DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        viewTextBoxColumn1.Name = "no";
        viewTextBoxColumn1.HeaderText = "No";
        viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
        viewTextBoxColumn1.Width = 40;
        viewTextBoxColumn1.ReadOnly = true;
        this.dgViewPrintList.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
        DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
        viewTextBoxColumn2.Name = "pageFrom";
        viewTextBoxColumn2.HeaderText = "From";
        viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
        viewTextBoxColumn2.Width = 180;
        viewTextBoxColumn2.ReadOnly = true;
        this.dgViewPrintList.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
        DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
        viewTextBoxColumn3.Name = "pageTo";
        viewTextBoxColumn3.HeaderText = "To";
        viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
        viewTextBoxColumn3.Width = 180;
        viewTextBoxColumn3.ReadOnly = true;
        this.dgViewPrintList.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
        DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
        viewButtonColumn.Name = "Delete";
        viewButtonColumn.HeaderText = "";
        viewButtonColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        viewButtonColumn.Width = 115;
        this.dgViewPrintList.Columns.Add((DataGridViewColumn) viewButtonColumn);
        this.dgViewPrintList.CellClick += new DataGridViewCellEventHandler(this.dgViewPrintList_DeleteClick);
        if (this.frmParent.dgvPrintListPrintFrm.Rows.Count <= 0)
          return;
        for (int index1 = 0; index1 < this.frmParent.dgvPrintListPrintFrm.Rows.Count; ++index1)
        {
          int index2 = this.dgViewPrintList.Rows.Add();
          this.dgViewPrintList.Rows[index2].Cells[0].Value = this.frmParent.dgvPrintListPrintFrm.Rows[index1].Cells[0].Value;
          this.dgViewPrintList.Rows[index2].Cells[1].Value = this.frmParent.dgvPrintListPrintFrm.Rows[index1].Cells[1].Value;
          this.dgViewPrintList.Rows[index2].Cells[2].Value = this.frmParent.dgvPrintListPrintFrm.Rows[index1].Cells[2].Value;
          this.dgViewPrintList.Rows[index2].Cells[3].Value = (object) "Delete";
          this.dgViewPrintList.Rows[index2].Cells[0].Tag = this.frmParent.dgvPrintListPrintFrm.Rows[index1].Cells[0].Tag;
          this.dgViewPrintList.Rows[index2].Cells[1].Tag = this.frmParent.dgvPrintListPrintFrm.Rows[index1].Cells[1].Tag;
          this.dgViewPrintList.Rows[index2].Cells[2].Tag = this.frmParent.dgvPrintListPrintFrm.Rows[index1].Cells[2].Tag;
        }
        this.dgViewPrintList.ClearSelection();
      }
      catch (Exception ex)
      {
      }
    }

    private void SetControlsText()
    {
      this.Text = this.GetResource("Page Specified", "PAGE_SPECIFIED", ResourceType.TITLE);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
      try
      {
        this.dgViewPrintList.Columns["no"].HeaderText = this.GetResource("No", "NO", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewPrintList.Columns["pageFrom"].HeaderText = this.GetResource("From", "PAGE_FROM", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
      try
      {
        this.dgViewPrintList.Columns["pageTo"].HeaderText = this.GetResource("To", "PAGE_TO", ResourceType.GRID_VIEW);
      }
      catch
      {
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='PRINT']" + "/Screen[@Name='PAGE_SPECIFIED']";
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

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.dgViewPrintList.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgViewPrintList.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    public void UpdateFromGridColumn(string strFrom, string sFromIndex)
    {
      try
      {
        if (this.dgViewPrintList.SelectedCells.Count > 0)
        {
          int rowIndex = this.dgViewPrintList.CurrentCell.RowIndex;
          if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            DataGridViewCell cell1 = row.Cells[1];
            DataGridViewCell cell2 = row.Cells[2];
            if (Convert.ToInt32(sFromIndex) > Convert.ToInt32(cell2.Tag.ToString()))
            {
              cell1.Value = cell2.Value;
              cell1.Tag = cell2.Tag;
              cell2.Value = (object) strFrom;
              cell2.Tag = (object) sFromIndex;
            }
            else
            {
              cell1.Value = (object) strFrom;
              cell1.Tag = (object) sFromIndex;
            }
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() == "")
          {
            DataGridViewCell cell = this.dgViewPrintList.Rows[rowIndex].Cells[1];
            cell.Value = (object) strFrom;
            cell.Tag = (object) sFromIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            DataGridViewCell cell1 = row.Cells[1];
            DataGridViewCell cell2 = row.Cells[2];
            if (Convert.ToInt32(sFromIndex) > Convert.ToInt32(cell2.Tag.ToString()))
            {
              cell1.Value = cell2.Value;
              cell1.Tag = cell2.Tag;
              cell2.Value = (object) strFrom;
              cell2.Tag = (object) sFromIndex;
            }
            else
            {
              cell1.Value = (object) strFrom;
              cell1.Tag = (object) sFromIndex;
            }
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          if (!(this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "") || !(this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != ""))
            return;
          string sRngStart = sFromIndex;
          string sRngEnd = this.dgViewPrintList.Rows[rowIndex].Cells[2].Tag.ToString();
          if (this.SelectPrintRange(ref sRngStart, ref sRngEnd, rowIndex) == 1)
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            row.Cells[1].Value = row.Cells[2].Value;
            row.Cells[1].Tag = row.Cells[2].Tag;
            row.Cells[2].Value = (object) strFrom;
            row.Cells[2].Tag = (object) sFromIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          else
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            row.Cells[1].Value = (object) strFrom;
            row.Cells[1].Tag = (object) sFromIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
        }
        else
        {
          int num1 = 0;
          int intRowIndex = 0;
          if (this.dgViewPrintList.Rows.Count > 0)
            intRowIndex = this.dgViewPrintList.Rows.Count - 1;
          int count = this.dgViewPrintList.Rows.Count;
          if (count == 0)
          {
            if (this.dgViewPrintList.Columns.Count == 0)
              this.GetPageSpecifiedGrid();
            int num2 = 1;
            this.dgViewPrintList.Rows.Add();
            DataGridViewRow row = this.dgViewPrintList.Rows[0];
            row.Cells[0].Value = (object) num2.ToString();
            DataGridViewCell cell = row.Cells[1];
            cell.Value = (object) strFrom;
            cell.Tag = (object) sFromIndex;
            row.Cells[2].Value = (object) "";
            row.Cells[3].Value = (object) "Delete";
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
            num1 = num2 + 1;
            this.dgViewPrintList.ClearSelection();
          }
          else if (count > 0 && count < 10)
          {
            if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() != "")
            {
              int index = intRowIndex + 1;
              int num2 = count + 1;
              this.dgViewPrintList.Rows.Add();
              DataGridViewRow row = this.dgViewPrintList.Rows[index];
              row.Cells[0].Value = (object) num2.ToString();
              DataGridViewCell cell = row.Cells[1];
              cell.Value = (object) strFrom;
              cell.Tag = (object) sFromIndex;
              row.Cells[2].Value = (object) "";
              row.Cells[3].Value = (object) "Delete";
              this.dgViewPrintList.Update();
              this.dgViewPrintList.Refresh();
              this.dgViewPrintList.ClearSelection();
            }
            else if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() != "")
            {
              DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
              DataGridViewCell cell1 = row.Cells[1];
              DataGridViewCell cell2 = row.Cells[2];
              if (Convert.ToInt32(sFromIndex) > Convert.ToInt32(cell2.Tag.ToString()))
              {
                cell1.Value = (object) cell2.Value.ToString();
                cell1.Tag = (object) cell2.Tag.ToString();
                cell2.Value = (object) strFrom;
                cell2.Tag = (object) sFromIndex;
              }
              else
              {
                cell1.Value = (object) strFrom;
                cell1.Tag = (object) sFromIndex;
              }
              this.dgViewPrintList.Update();
              this.dgViewPrintList.Refresh();
              this.dgViewPrintList.ClearSelection();
            }
            else
            {
              if (!(this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() == ""))
                return;
              DataGridViewCell cell = this.dgViewPrintList.Rows[intRowIndex].Cells[1];
              cell.Value = (object) strFrom;
              cell.Tag = (object) sFromIndex;
              this.dgViewPrintList.Update();
              this.dgViewPrintList.Refresh();
              this.dgViewPrintList.ClearSelection();
            }
          }
          else
          {
            if (count <= 0 || count != 10 || intRowIndex == 10)
              return;
            if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() != "")
            {
              string sRngStart = sFromIndex;
              string sRngEnd = this.dgViewPrintList.Rows[intRowIndex].Cells[2].Tag.ToString();
              if (this.SelectPrintRange(ref sRngStart, ref sRngEnd, intRowIndex) == 1)
              {
                DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
                int num2 = count;
                row.Cells[1].Value = row.Cells[2].Value;
                row.Cells[1].Tag = row.Cells[2].Tag;
                row.Cells[2].Value = (object) strFrom;
                row.Cells[2].Tag = (object) sFromIndex;
                this.dgViewPrintList.Update();
                this.dgViewPrintList.Refresh();
                num1 = num2 + 1;
              }
              else
              {
                DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
                int num2 = count;
                row.Cells[1].Value = (object) strFrom;
                row.Cells[1].Tag = (object) sFromIndex;
                this.dgViewPrintList.Update();
                this.dgViewPrintList.Refresh();
                num1 = num2 + 1;
              }
            }
            else if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() == "")
            {
              DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
              int num2 = count;
              row.Cells[1].Value = (object) strFrom.ToString();
              row.Cells[1].Tag = (object) sFromIndex;
              this.dgViewPrintList.Update();
              this.dgViewPrintList.Refresh();
              num1 = num2 + 1;
            }
            else
            {
              if (!(this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() != ""))
                return;
              int num2 = (int) MessageBox.Show(this.GetResource("The possible number is up to 10.  Please specify again after DELETE or print.", "RANGE_LIMIT", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (this.dgViewPrintList.Rows.Count > 0)
          this.btnOK.Enabled = true;
      }
    }

    private void GetPageSpecifiedGrid()
    {
      if (this.dgViewPrintList == null || this.dgViewPrintList.Columns.Count != 0)
        return;
      this.dgViewPrintList.Columns.Add(new DataGridViewColumn());
    }

    private int SelectPrintRange(ref string sRngStart, ref string sRngEnd, int intRowIndex)
    {
      try
      {
        return int.Parse(sRngStart) <= int.Parse(sRngEnd) ? 0 : 1;
      }
      catch
      {
        return 0;
      }
    }

    public void UpdateToGridCol(string strTo, string sToIndex)
    {
      try
      {
        if (this.dgViewPrintList.SelectedCells.Count > 0)
        {
          int rowIndex = this.dgViewPrintList.CurrentCell.RowIndex;
          if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            DataGridViewCell cell1 = row.Cells[2];
            DataGridViewCell cell2 = row.Cells[1];
            int int32 = Convert.ToInt32(cell2.Tag);
            if (Convert.ToInt32(sToIndex) < int32)
            {
              cell1.Value = cell2.Value;
              cell1.Tag = cell2.Tag;
              cell2.Value = (object) strTo;
              cell2.Tag = (object) sToIndex;
            }
            else
            {
              cell1.Value = (object) strTo.ToString();
              cell1.Tag = (object) sToIndex;
            }
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
          {
            DataGridViewCell cell = this.dgViewPrintList.Rows[rowIndex].Cells[2];
            cell.Value = (object) strTo;
            cell.Tag = (object) sToIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          if (!(this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() == ""))
            return;
          string sRngStart = this.dgViewPrintList.Rows[rowIndex].Cells[1].Tag.ToString();
          string sRngEnd = sToIndex;
          if (this.SelectPrintRange(ref sRngStart, ref sRngEnd, rowIndex) == 1)
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            row.Cells[2].Value = (object) row.Cells[1].Value.ToString();
            row.Cells[2].Tag = (object) row.Cells[1].Tag.ToString();
            row.Cells[1].Value = (object) strTo.ToString();
            row.Cells[1].Tag = (object) sToIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          else
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            row.Cells[2].Value = (object) strTo;
            row.Cells[2].Tag = (object) sToIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
        }
        else
        {
          int intRowIndex = 0;
          if (this.dgViewPrintList.Rows.Count > 0)
            intRowIndex = this.dgViewPrintList.Rows.Count - 1;
          int num1 = 0;
          int count = this.dgViewPrintList.Rows.Count;
          if (count == 0)
          {
            if (this.dgViewPrintList.Columns.Count == 0)
              this.GetPageSpecifiedGrid();
            int num2 = 1;
            this.dgViewPrintList.Rows.Add();
            DataGridViewRow row = this.dgViewPrintList.Rows[0];
            row.Cells[0].Value = (object) num2.ToString();
            row.Cells[1].Value = (object) "";
            DataGridViewCell cell = row.Cells[2];
            cell.Value = (object) strTo.ToString();
            cell.Tag = (object) sToIndex;
            row.Cells[3].Value = (object) "Delete";
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
            this.dgViewPrintList.ClearSelection();
            num1 = num2 + 1;
          }
          else if (count > 0 && count < 10)
          {
            if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() == "")
            {
              int index = intRowIndex + 1;
              int num2 = count + 1;
              this.dgViewPrintList.Rows.Add();
              DataGridViewRow row = this.dgViewPrintList.Rows[index];
              row.Cells[0].Value = (object) num2.ToString();
              row.Cells[1].Value = (object) "";
              DataGridViewCell cell = row.Cells[2];
              cell.Value = (object) strTo.ToString();
              cell.Tag = (object) sToIndex;
              row.Cells[3].Value = (object) "Delete";
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
            }
            else if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() != "")
            {
              DataGridViewCell cell = this.dgViewPrintList.Rows[intRowIndex].Cells[2];
              cell.Value = (object) strTo.ToString();
              cell.Tag = (object) sToIndex;
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
            }
            else if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() == "")
            {
              DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
              DataGridViewCell cell1 = row.Cells[2];
              DataGridViewCell cell2 = row.Cells[1];
              int int32 = Convert.ToInt32(cell2.Tag);
              if (Convert.ToInt32(sToIndex) < int32)
              {
                cell1.Value = cell2.Value;
                cell1.Tag = cell2.Tag;
                cell2.Value = (object) strTo;
                cell2.Tag = (object) sToIndex;
              }
              else
              {
                cell1.Value = (object) strTo.ToString();
                cell1.Tag = (object) sToIndex;
              }
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
            }
            else
            {
              int num2 = count + 1;
              this.dgViewPrintList.Rows.Add();
              DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex + 1];
              row.Cells[0].Value = (object) num2.ToString();
              row.Cells[1].Value = (object) "";
              DataGridViewCell cell = row.Cells[2];
              cell.Value = (object) strTo.ToString();
              cell.Tag = (object) sToIndex;
              row.Cells[3].Value = (object) "Delete";
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
            }
          }
          else
          {
            if (count <= 0 || count != 10 || intRowIndex == 10)
              return;
            if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() != "")
            {
              DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
              int num2 = count;
              row.Cells[2].Value = (object) strTo.ToString();
              row.Cells[2].Tag = (object) sToIndex;
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
              num1 = num2 + 1;
            }
            else if (this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() == "")
            {
              string sRngStart = this.dgViewPrintList.Rows[intRowIndex].Cells[1].Tag.ToString();
              string sRngEnd = sToIndex;
              if (this.SelectPrintRange(ref sRngStart, ref sRngEnd, intRowIndex) == 1)
              {
                DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
                int num2 = count;
                row.Cells[2].Value = (object) row.Cells[1].Value.ToString();
                row.Cells[2].Tag = (object) row.Cells[1].Tag.ToString();
                row.Cells[1].Value = (object) strTo.ToString();
                row.Cells[1].Tag = (object) sToIndex;
                this.dgViewPrintList.Update();
                this.dgViewPrintList.ClearSelection();
                this.dgViewPrintList.Refresh();
                num1 = num2 + 1;
              }
              else
              {
                DataGridViewRow row = this.dgViewPrintList.Rows[intRowIndex];
                int num2 = count;
                row.Cells[2].Value = (object) strTo.ToString();
                row.Cells[2].Tag = (object) sToIndex;
                this.dgViewPrintList.Update();
                this.dgViewPrintList.ClearSelection();
                this.dgViewPrintList.Refresh();
                num1 = num2 + 1;
              }
            }
            else
            {
              if (!(this.dgViewPrintList.Rows[intRowIndex].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[intRowIndex].Cells[2].Value.ToString() != ""))
                return;
              int num2 = (int) MessageBox.Show(this.GetResource("The possible number is up to 10.  Please specify again after DELETE or print.", "RANGE_LIMIT", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (this.dgViewPrintList.Rows.Count > 0)
          this.btnOK.Enabled = true;
      }
    }

    public void UpdatePrintThisGridCol(string strPrintThis, string sPrintThisIndex)
    {
      try
      {
        if (this.dgViewPrintList.SelectedCells.Count > 0)
        {
          int rowIndex = this.dgViewPrintList.CurrentCell.RowIndex;
          if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() == "")
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            DataGridViewCell cell1 = row.Cells[1];
            cell1.Value = (object) strPrintThis;
            cell1.Tag = (object) sPrintThisIndex;
            DataGridViewCell cell2 = row.Cells[2];
            cell2.Value = (object) strPrintThis;
            cell2.Tag = (object) sPrintThisIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            DataGridViewCell cell1 = row.Cells[1];
            cell1.Value = (object) strPrintThis;
            cell1.Tag = (object) sPrintThisIndex;
            DataGridViewCell cell2 = row.Cells[2];
            cell2.Value = (object) strPrintThis;
            cell2.Tag = (object) sPrintThisIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
          {
            DataGridViewRow row = this.dgViewPrintList.Rows[rowIndex];
            DataGridViewCell cell1 = row.Cells[1];
            cell1.Value = (object) strPrintThis;
            cell1.Tag = (object) sPrintThisIndex;
            DataGridViewCell cell2 = row.Cells[2];
            cell2.Value = (object) strPrintThis;
            cell2.Tag = (object) sPrintThisIndex;
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
          }
          if (!(this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != ""))
            return;
          DataGridViewRow row1 = this.dgViewPrintList.Rows[rowIndex];
          DataGridViewCell cell3 = row1.Cells[1];
          cell3.Value = (object) strPrintThis;
          cell3.Tag = (object) sPrintThisIndex;
          DataGridViewCell cell4 = row1.Cells[2];
          cell4.Value = (object) strPrintThis;
          cell4.Tag = (object) sPrintThisIndex;
          this.dgViewPrintList.Update();
          this.dgViewPrintList.Refresh();
        }
        else
        {
          int num1 = 0;
          int index1 = 0;
          if (this.dgViewPrintList.Rows.Count > 0)
            index1 = this.dgViewPrintList.Rows.Count - 1;
          int count = this.dgViewPrintList.Rows.Count;
          if (count == 0)
          {
            int num2 = 1;
            this.dgViewPrintList.Rows.Add();
            DataGridViewRow row = this.dgViewPrintList.Rows[0];
            row.Cells[0].Value = (object) num2.ToString();
            DataGridViewCell cell1 = row.Cells[1];
            cell1.Value = (object) strPrintThis;
            cell1.Tag = (object) sPrintThisIndex;
            DataGridViewCell cell2 = row.Cells[2];
            cell2.Value = (object) strPrintThis;
            cell2.Tag = (object) sPrintThisIndex;
            row.Cells[3].Value = (object) "Delete";
            this.dgViewPrintList.Update();
            this.dgViewPrintList.Refresh();
            this.dgViewPrintList.ClearSelection();
            num1 = num2 + 1;
          }
          else if (count > 0 && count < 10)
          {
            if (this.dgViewPrintList.Rows[index1].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[index1].Cells[2].Value.ToString() != "")
            {
              int index2 = index1 + 1;
              int num2 = count + 1;
              this.dgViewPrintList.Rows.Add();
              DataGridViewRow row = this.dgViewPrintList.Rows[index2];
              row.Cells[0].Value = (object) num2.ToString();
              DataGridViewCell cell1 = row.Cells[1];
              cell1.Value = (object) strPrintThis;
              cell1.Tag = (object) sPrintThisIndex;
              DataGridViewCell cell2 = row.Cells[2];
              cell2.Value = (object) strPrintThis;
              cell2.Tag = (object) sPrintThisIndex;
              row.Cells[3].Value = (object) "Delete";
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
            }
            else if (this.dgViewPrintList.Rows[index1].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[index1].Cells[2].Value.ToString() != "")
            {
              DataGridViewRow row = this.dgViewPrintList.Rows[index1];
              int num2 = count;
              row.Cells[1].Value = (object) strPrintThis.ToString();
              row.Cells[1].Tag = (object) sPrintThisIndex.ToString();
              row.Cells[2].Value = (object) strPrintThis.ToString();
              row.Cells[2].Tag = (object) sPrintThisIndex.ToString();
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
              num1 = num2 + 1;
            }
            else
            {
              if (!(this.dgViewPrintList.Rows[index1].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[index1].Cells[2].Value.ToString() == ""))
                return;
              DataGridViewRow row = this.dgViewPrintList.Rows[index1];
              int num2 = count;
              row.Cells[1].Value = (object) strPrintThis.ToString();
              row.Cells[1].Tag = (object) sPrintThisIndex.ToString();
              row.Cells[2].Value = (object) strPrintThis.ToString();
              row.Cells[2].Tag = (object) sPrintThisIndex.ToString();
              this.dgViewPrintList.Update();
              this.dgViewPrintList.ClearSelection();
              this.dgViewPrintList.Refresh();
              num1 = num2 + 1;
            }
          }
          else
          {
            if (count <= 0 || count != 10 || index1 == 10)
              return;
            if (this.dgViewPrintList.Rows[index1].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[index1].Cells[2].Value.ToString() != "")
            {
              DataGridViewRow row = this.dgViewPrintList.Rows[index1];
              int num2 = count;
              row.Cells[1].Value = (object) strPrintThis.ToString();
              row.Cells[1].Tag = (object) sPrintThisIndex.ToString();
              row.Cells[2].Value = (object) strPrintThis.ToString();
              row.Cells[2].Tag = (object) sPrintThisIndex.ToString();
              this.dgViewPrintList.Update();
              this.dgViewPrintList.Refresh();
              this.dgViewPrintList.ClearSelection();
              num1 = num2 + 1;
            }
            else if (this.dgViewPrintList.Rows[index1].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[index1].Cells[2].Value.ToString() == "")
            {
              DataGridViewRow row = this.dgViewPrintList.Rows[index1];
              int num2 = count;
              row.Cells[1].Value = (object) strPrintThis.ToString();
              row.Cells[1].Tag = (object) sPrintThisIndex.ToString();
              row.Cells[2].Value = (object) strPrintThis.ToString();
              row.Cells[2].Tag = (object) sPrintThisIndex.ToString();
              this.dgViewPrintList.Update();
              this.dgViewPrintList.Refresh();
              this.dgViewPrintList.ClearSelection();
              num1 = num2 + 1;
            }
            else
            {
              if (!(this.dgViewPrintList.Rows[index1].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[index1].Cells[2].Value.ToString() != ""))
                return;
              int num2 = (int) MessageBox.Show(this.GetResource("The possible number is up to 10.  Please specify again after DELETE or print.", "RANGE_LIMIT", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (this.dgViewPrintList.Rows.Count > 0)
          this.btnOK.Enabled = true;
      }
    }

    private void updateGridSerialNo()
    {
      int count = this.dgViewPrintList.Rows.Count;
      int num = 1;
      for (int index = 0; index < count; ++index)
        this.dgViewPrintList.Rows[index].Cells[0].Value = (object) (num + index);
    }

    private void CopyGridView()
    {
      try
      {
        if (this.frmParent.dgvPrintListPrintFrm.Rows.Count > 0)
          this.frmParent.dgvPrintListPrintFrm.Rows.Clear();
        int num = 0;
        for (int index1 = 0; index1 < this.dgViewPrintList.Rows.Count; ++index1)
        {
          int index2 = this.frmParent.dgvPrintListPrintFrm.Rows.Add();
          this.frmParent.dgvPrintListPrintFrm.Rows[index2].Cells[0].Value = this.dgViewPrintList.Rows[index1].Cells[0].Value;
          this.frmParent.dgvPrintListPrintFrm.Rows[index2].Cells[1].Value = this.dgViewPrintList.Rows[index1].Cells[1].Value;
          this.frmParent.dgvPrintListPrintFrm.Rows[index2].Cells[2].Value = this.dgViewPrintList.Rows[index1].Cells[2].Value;
          this.frmParent.dgvPrintListPrintFrm.Rows[index2].Cells[0].Tag = this.dgViewPrintList.Rows[index1].Cells[0].Tag;
          this.frmParent.dgvPrintListPrintFrm.Rows[index2].Cells[1].Tag = this.dgViewPrintList.Rows[index1].Cells[1].Tag;
          this.frmParent.dgvPrintListPrintFrm.Rows[index2].Cells[2].Tag = this.dgViewPrintList.Rows[index1].Cells[2].Tag;
          num += this.frmParent.dgvPrintListPrintFrm.Rows[index2].Height;
        }
        this.frmParent.dgvPrintListPrintFrm.Height = num;
      }
      catch (Exception ex)
      {
      }
    }

    private DataTable GetDataTableFromDGV(DataGridView dgv)
    {
      DataTable dataTable = new DataTable();
      try
      {
        foreach (DataGridViewColumn column in (BaseCollection) dgv.Columns)
        {
          if (column.Visible)
            dataTable.Columns.Add(column.Name);
        }
        object[] objArray = new object[dgv.Columns.Count];
        foreach (DataGridViewRow row in (IEnumerable) dgv.Rows)
        {
          for (int index = 0; index < row.Cells.Count; ++index)
            objArray[index] = row.Cells[index].Value;
          dataTable.Rows.Add(objArray);
        }
      }
      catch (Exception ex)
      {
      }
      return dataTable;
    }

    private void EnablePrintControls()
    {
      try
      {
        if (this.frmParent.dgvPrintListPrintFrm.Rows.Count == 0)
        {
          this.frmParent.btnPreview.Enabled = false;
          this.frmParent.btnPrint.Enabled = false;
        }
        else if (this.frmParent.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
        {
          this.frmParent.btnPreview.Enabled = false;
          this.frmParent.btnPrint.Enabled = true;
        }
        else
        {
          if (this.frmParent.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
            return;
          this.frmParent.btnPreview.Enabled = true;
          this.frmParent.btnPrint.Enabled = true;
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}

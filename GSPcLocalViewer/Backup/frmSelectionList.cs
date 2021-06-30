// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmSelectionList
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
  public class frmSelectionList : DockContent
  {
    private frmViewer frmParent;
    private string attPartNoElement;
    private string sParentPageArguments;
    private string sGoToPageArgs;
    private string gstrSelectionListHeader;
    private string gstrSelectionListColSequence;
    private IContainer components;
    private Panel pnlForm;
    public DataGridView dgPartslist;
    private PictureBox picLoading;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private ToolStrip toolStrip1;
    private ToolStripButton tsBtnAdd;
    private ToolStripButton tsBtnDeleteSelection;
    private FolderBrowserDialog FolderBrowserDialog;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator2;
    private ContextMenuStrip cmsSelectionlist;
    private ToolStripMenuItem copyToClipboardToolStripMenuItem;
    private ToolStripMenuItem commaSeparatedToolStripMenuItem;
    private ToolStripMenuItem tabSeparatedToolStripMenuItem;
    private ToolStripMenuItem exportToFileToolStripMenuItem;
    private ToolStripMenuItem commaSeparatedToolStripMenuItem1;
    private ToolStripMenuItem tabSeparatedToolStripMenuItem1;
    private SaveFileDialog dlgSaveFile;
    private ToolStripButton tsbSelectAll;
    private ToolStripButton tsbClearSelection;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem selectAllToolStripMenuItem;
    private ToolStripMenuItem tsmClearSelection;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripMenuItem deleteSelectionToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem goToPageToolStripMenuItem;
    private ToolStripButton tsBtnGoToPage;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripButton tsBtnDeleteAll;
    private DataGridViewTextBoxColumn Column1;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripButton tsBtnThirdPartyBasket;
    private ToolStripButton tsPrint;

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    public static extern bool SetActiveWindow(IntPtr hWnd);

    public frmSelectionList(frmViewer frm)
    {
      this.InitializeComponent();
      this.OnOffFeatures();
      this.frmParent = frm;
      this.UpdateFont();
      this.LoadResources();
      this.LoadTitle();
      this.sGoToPageArgs = string.Empty;
      this.gstrSelectionListHeader = string.Empty;
      this.gstrSelectionListColSequence = string.Empty;
      this.tsPrint.Visible = Program.objAppFeatures.bPrint;
    }

    private void frmSelectionList_Load(object sender, EventArgs e)
    {
    }

    private void frmSelectionList_VisibleChanged(object sender, EventArgs e)
    {
      this.frmParent.selectionListToolStripMenuItem.Checked = this.Visible;
    }

    private void commaSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, ",");
      if (!(dataGridViewText != string.Empty))
        return;
      Clipboard.SetText(dataGridViewText);
    }

    private void tabSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t");
      if (!(dataGridViewText != string.Empty))
        return;
      Clipboard.SetText(dataGridViewText);
    }

    private void commaSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
      this.dlgSaveFile.RestoreDirectory = true;
      string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, ",");
      if (!(dataGridViewText != string.Empty))
        return;
      if (this.dlgSaveFile.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        StreamWriter text = File.CreateText(this.dlgSaveFile.FileName);
        text.Write(dataGridViewText);
        text.Close();
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("(E-SLT-EM001) Failed to export specified object", "(E-SLT-EM001)_EXPORT", ResourceType.POPUP_MESSAGE));
      }
    }

    private void tabSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
      this.dlgSaveFile.RestoreDirectory = true;
      string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t");
      if (!(dataGridViewText != string.Empty))
        return;
      if (this.dlgSaveFile.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        StreamWriter text = File.CreateText(this.dlgSaveFile.FileName);
        text.Write(dataGridViewText);
        text.Close();
      }
      catch
      {
        MessageHandler.ShowWarning("Problems in exporting data to file");
      }
    }

    private void tsbClearSelection_Click(object sender, EventArgs e)
    {
      if (this.dgPartslist.Rows.Count <= 0)
        return;
      this.dgPartslist.ClearSelection();
      this.tsBtnDeleteSelection.Enabled = false;
      this.tsbClearSelection.Enabled = false;
    }

    private void tsbSelectAll_Click(object sender, EventArgs e)
    {
      this.dgPartslist.SelectAll();
    }

    private void cmsSelectionlist_Opening(object sender, CancelEventArgs e)
    {
      if (this.dgPartslist.SelectedRows.Count > 0)
      {
        this.deleteSelectionToolStripMenuItem.Enabled = true;
        this.copyToClipboardToolStripMenuItem.Enabled = true;
        this.exportToFileToolStripMenuItem.Enabled = true;
      }
      else
      {
        this.exportToFileToolStripMenuItem.Enabled = false;
        this.deleteSelectionToolStripMenuItem.Enabled = false;
        this.copyToClipboardToolStripMenuItem.Enabled = false;
      }
      if (this.dgPartslist.SelectedRows.Count <= 1 && this.dgPartslist.Rows.Count > 0)
        this.goToPageToolStripMenuItem.Enabled = true;
      else
        this.goToPageToolStripMenuItem.Enabled = false;
    }

    private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.dgPartslist.SelectAll();
    }

    private void tsmClearSelection_Click(object sender, EventArgs e)
    {
      this.dgPartslist.ClearSelection();
    }

    private void deleteSelectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsBtnDeleteSelection_Click((object) null, (EventArgs) null);
    }

    private void openPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tsBtnOpenParentPage_Click((object) null, (EventArgs) null);
    }

    private void tsBtnOpenParentPage_Click(object sender, EventArgs e)
    {
      try
      {
        string[] strArray = this.sGoToPageArgs.Split(new string[1]
        {
          "**"
        }, StringSplitOptions.RemoveEmptyEntries);
        this.frmParent.OpenParentPage(strArray[0], strArray[1], strArray[2], strArray[3], strArray[4], strArray[5]);
      }
      catch
      {
      }
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      this.frmParent.UncheckAllRows();
      try
      {
        this.frmParent.ClearSelectionList();
      }
      catch
      {
      }
    }

    private void dgPartslist_SelectionChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.dgPartslist.SelectedRows.Count == 0)
        {
          this.tsBtnDeleteSelection.Enabled = false;
          this.tsbClearSelection.Enabled = false;
        }
        else
        {
          this.tsBtnDeleteSelection.Enabled = true;
          this.tsbClearSelection.Enabled = true;
        }
        if (this.dgPartslist.SelectedRows.Count == 1)
          this.tsBtnGoToPage.Enabled = true;
        else
          this.tsBtnGoToPage.Enabled = false;
      }
      catch
      {
      }
    }

    private void dgPartslist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
      try
      {
        if (this.dgPartslist.Rows.Count > 0)
        {
          this.tsBtnDeleteAll.Enabled = true;
          this.tsbSelectAll.Enabled = true;
          this.tsBtnDeleteAll.Enabled = true;
          this.tsBtnGoToPage.Enabled = true;
          this.tsPrint.Enabled = true;
        }
        else
        {
          this.tsBtnDeleteAll.Enabled = false;
          this.tsbSelectAll.Enabled = false;
          this.tsBtnDeleteAll.Enabled = false;
          this.tsBtnGoToPage.Enabled = false;
          this.tsPrint.Enabled = false;
        }
        if (this.frmParent.bIsSelectionListPrint)
          this.tsPrint.Enabled = true;
        else
          this.tsPrint.Enabled = false;
        this.dgPartslist["AutoIndexColumn", e.RowIndex].Value = (object) e.RowIndex;
      }
      catch
      {
      }
    }

    private void dgPartslist_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
    {
      try
      {
        if (this.dgPartslist.Rows.Count > 0)
        {
          this.tsBtnDeleteAll.Enabled = true;
          this.tsbSelectAll.Enabled = true;
          this.tsBtnGoToPage.Enabled = true;
          this.tsPrint.Enabled = true;
        }
        else
        {
          this.tsBtnDeleteAll.Enabled = false;
          this.tsbSelectAll.Enabled = false;
          this.tsBtnGoToPage.Enabled = false;
          this.tsPrint.Enabled = false;
        }
        if (this.frmParent.bIsSelectionListPrint)
          this.tsPrint.Enabled = true;
        else
          this.tsPrint.Enabled = false;
      }
      catch
      {
      }
    }

    private void dgPartslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      try
      {
        if (e.RowIndex == -1 || !(this.dgPartslist.Columns[this.dgPartslist.CurrentCell.ColumnIndex].Name.ToUpper() == "QTY") || this.dgPartslist.Rows.Count <= 0)
          return;
        this.frmParent.ShowQuantityScreen(this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.CurrentRow.Index].Value.ToString());
      }
      catch
      {
      }
    }

    private void dgPartslist_MouseDown(object sender, MouseEventArgs e)
    {
      try
      {
        this.sGoToPageArgs = this.dgPartslist.Rows[this.dgPartslist.HitTest(e.X, e.Y).RowIndex].Tag.ToString();
      }
      catch
      {
      }
    }

    private void tsbClearSelection_EnabledChanged(object sender, EventArgs e)
    {
      this.tsmClearSelection.Enabled = this.tsbClearSelection.Enabled;
    }

    private void tsBtnDeleteSelection_EnabledChanged(object sender, EventArgs e)
    {
      this.deleteSelectionToolStripMenuItem.Enabled = this.tsBtnDeleteSelection.Enabled;
    }

    private void tsBtnGoToPage_EnabledChanged(object sender, EventArgs e)
    {
      this.goToPageToolStripMenuItem.Enabled = this.tsBtnGoToPage.Enabled;
    }

    private void tsbSelectAll_EnabledChanged(object sender, EventArgs e)
    {
      this.selectAllToolStripMenuItem.Enabled = this.tsbSelectAll.Enabled;
    }

    private void dgPartslist_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
    {
      try
      {
        if (e.Column.Name.ToUpper() == "LINKNUMBER")
        {
          int num1 = int.Parse(this.dgPartslist.Rows[e.RowIndex1].Cells["AutoIndexColumn"].Value.ToString());
          int num2 = int.Parse(this.dgPartslist.Rows[e.RowIndex2].Cells["AutoIndexColumn"].Value.ToString());
          e.SortResult = num1 <= num2 ? (num1 >= num2 ? 0 : -1) : 1;
        }
        else
          e.SortResult = string.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
        e.Handled = true;
      }
      catch
      {
      }
    }

    private void tsPrint_Click(object sender, EventArgs e)
    {
      try
      {
        this.picLoading.BringToFront();
        this.picLoading.Show();
        this.frmParent.OpenPrintDialogue(3);
        this.picLoading.SendToBack();
        this.picLoading.Hide();
      }
      catch
      {
      }
    }

    private void tsBtnPrint_Click(object sender, EventArgs e)
    {
      try
      {
        this.picLoading.BringToFront();
        this.picLoading.Show();
        this.frmParent.OpenPrintDialogue(3);
        this.picLoading.SendToBack();
        this.picLoading.Hide();
      }
      catch
      {
      }
    }

    public void LoadSelectionList()
    {
      try
      {
        this.frmParent.gdtselectionListTable.Clear();
        if (this.frmParent.gdtselectionListTable.Columns.Count == 0)
          this.AddColumnsSelectionListTable();
        string[] strArray1 = this.gstrSelectionListHeader.TrimEnd(',').Split(',');
        string[] strArray2 = this.gstrSelectionListColSequence.TrimEnd(',').Split(',');
        foreach (DataGridViewRow row1 in (IEnumerable) this.dgPartslist.Rows)
        {
          DataRow row2 = this.frmParent.gdtselectionListTable.NewRow();
          for (int index = 0; index < strArray2.Length; ++index)
          {
            if (row1.Cells[strArray2[index]].Value != null)
              row2[strArray1[index]] = (object) row1.Cells[strArray2[index]].Value.ToString();
          }
          this.frmParent.gdtselectionListTable.Rows.Add(row2);
        }
      }
      catch
      {
      }
    }

    private void AddColumnsSelectionListTable()
    {
      try
      {
        string[] strArray = this.gstrSelectionListHeader.TrimEnd(',').Split(',');
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (strArray[index] != string.Empty)
            this.frmParent.gdtselectionListTable.Columns.Add(new DataColumn()
            {
              ColumnName = strArray[index]
            });
        }
      }
      catch
      {
      }
    }

    public void tsBtnThirdPartyBasket_Click(object sender, EventArgs e)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      try
      {
        if (Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL_TITLE"] != null)
          empty2 = Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL_TITLE"].ToString();
        if (Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"] != null)
        {
          if (Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"] != string.Empty)
          {
            this.OpenURLInBrowser(Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"].ToString(), empty2);
            return;
          }
        }
      }
      catch
      {
        MessageHandler.ShowError1(this.GetResource("(E-SLT-EM002) Failed to load specified object", "(E-SLT-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
        return;
      }
      try
      {
        if (Program.iniServers[this.frmParent.ServerId].items["BASKET", "EXE"] != null)
        {
          string str = Program.iniServers[this.frmParent.ServerId].items["BASKET", "EXE"].ToString();
          if (!Path.IsPathRooted(str))
          {
            Process.Start(Path.Combine(Application.StartupPath, str));
            return;
          }
          Process.Start(str);
          return;
        }
      }
      catch
      {
        MessageHandler.ShowError1(this.GetResource("(E-SLT-EM003) Failed to load specified object", "(E-SLT-EM003)_FAILED", ResourceType.POPUP_MESSAGE));
        return;
      }
      try
      {
        if ((!(empty1 == string.Empty) || !(empty3 == string.Empty)) && Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"] == null)
          return;
        MessageHandler.ShowError1(this.GetResource("(E-SLT-EM004) Failed to load specified object", "(E-SLT-EM004)_FAILED", ResourceType.POPUP_MESSAGE));
      }
      catch
      {
      }
    }

    private void OpenURLInBrowser(string sUrl, string sWebPageTitle)
    {
      string str = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
      if (str != string.Empty && str != null)
      {
        if (str.ToUpper() == "IEXPLORE")
        {
          RegistryReader registryReader = new RegistryReader();
          string empty = string.Empty;
          string fileName = registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + str + ".exe", string.Empty);
          if (fileName == null)
          {
            using (Process process = Process.Start(sUrl))
            {
              if (process == null)
                return;
              IntPtr handle = process.Handle;
              frmSelectionList.SetForegroundWindow(process.Handle);
              frmSelectionList.SetActiveWindow(process.Handle);
            }
          }
          else
          {
            using (Process process = Process.Start(fileName, sUrl))
            {
              if (process == null)
                return;
              IntPtr handle = process.Handle;
              frmSelectionList.SetForegroundWindow(process.Handle);
              frmSelectionList.SetActiveWindow(process.Handle);
            }
          }
        }
        else
        {
          try
          {
            string fileName = new RegistryReader().Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + str + ".exe", string.Empty);
            if (fileName == null)
            {
              using (Process process = Process.Start(sUrl))
              {
                if (process == null)
                  return;
                IntPtr handle = process.Handle;
                frmSelectionList.SetForegroundWindow(process.Handle);
                frmSelectionList.SetActiveWindow(process.Handle);
              }
            }
            else
            {
              using (Process process = Process.Start(fileName, sUrl))
              {
                if (process == null)
                  return;
                IntPtr handle = process.Handle;
                frmSelectionList.SetForegroundWindow(process.Handle);
                frmSelectionList.SetActiveWindow(process.Handle);
              }
            }
          }
          catch (Exception ex)
          {
          }
        }
      }
      else
      {
        using (Process process = Process.Start("IExplore.exe", sUrl))
        {
          if (process == null)
            return;
          IntPtr handle = process.Handle;
          frmSelectionList.SetForegroundWindow(process.Handle);
          frmSelectionList.SetActiveWindow(process.Handle);
        }
      }
    }

    public void selListInitialize()
    {
      if (this.dgPartslist.InvokeRequired)
        this.dgPartslist.Invoke((Delegate) new frmSelectionList.selListInitializeDelegate(this.selListInitialize));
      else if (this.dgPartslist.Rows.Count > 0 && this.dgPartslist.Columns.Count > 1)
      {
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "SLIST_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          string str = string.Empty;
          try
          {
            str = iniFileIo.GetKeyValue("SLIST_SETTINGS", keys[index].ToString().ToUpper(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
          }
          catch
          {
          }
          if (str != null && str != string.Empty)
          {
            if (str.Split(new string[1]{ "|" }, StringSplitOptions.RemoveEmptyEntries)[3].ToUpper() != "TRUE")
              this.dgPartslist.Columns[keys[index].ToString()].Visible = false;
            else
              this.dgPartslist.Columns[keys[index].ToString()].Visible = true;
          }
        }
      }
      else
      {
        this.dgPartslist.Rows.Clear();
        this.dgPartslist.Columns.Clear();
        this.dgPartslist.AllowUserToAddRows = false;
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "SLIST_SETTINGS");
        this.frmParent.dicSLSettings = new Dictionary<string, string>();
        for (int index = 0; index < keys.Count; ++index)
        {
          DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
          viewTextBoxColumn.HeaderCell = new DataGridViewColumnHeaderCell();
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string empty3 = string.Empty;
          string str1 = string.Empty;
          string[] strArray1 = (string[]) null;
          try
          {
            str1 = iniFileIo.GetKeyValue("SLIST_SETTINGS", keys[index].ToString().ToUpper(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
          }
          catch
          {
          }
          if (str1 != null && str1 != string.Empty)
          {
            string[] strArray2 = str1.Split(new string[1]
            {
              "|"
            }, StringSplitOptions.RemoveEmptyEntries);
            string str2 = "|True|True|" + strArray2[1] + "|" + strArray2[2];
            if (strArray2.Length == 3)
              str1 += str2;
            else if (strArray2.Length == 4)
              str1 += str2;
            strArray1 = str1.Split(new string[1]{ "|" }, StringSplitOptions.RemoveEmptyEntries);
            this.frmParent.dicSLSettings.Add(keys[index].ToString().ToUpper(), str2);
            try
            {
              empty1 = strArray1[0];
              frmSelectionList frmSelectionList1 = this;
              frmSelectionList1.gstrSelectionListHeader = frmSelectionList1.gstrSelectionListHeader + empty1 + ",";
              frmSelectionList frmSelectionList2 = this;
              frmSelectionList2.gstrSelectionListColSequence = frmSelectionList2.gstrSelectionListColSequence + keys[index].ToString() + ",";
              empty2 = strArray1[1];
              empty3 = strArray1[2];
            }
            catch
            {
            }
          }
          if (empty1 != string.Empty)
          {
            viewTextBoxColumn.HeaderCell.Value = (object) this.GetDGHeaderCellValue(keys[index].ToString(), empty1);
            viewTextBoxColumn.Name = keys[index].ToString();
            viewTextBoxColumn.Tag = (object) keys[index].ToString();
            viewTextBoxColumn.Visible = true;
            if (empty2.ToUpper().Equals("R"))
              viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            else if (empty2.ToUpper().Equals("C"))
              viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            else
              viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            if (keys[index].ToString().ToUpper() == "PART_SLIST_KEY")
            {
              viewTextBoxColumn.Name = "PART_SLIST_KEY";
              viewTextBoxColumn.ToolTipText = "HIDDEN";
            }
            else
              viewTextBoxColumn.Name = keys[index].ToString();
            viewTextBoxColumn.DefaultCellStyle.NullValue = (object) string.Empty;
            viewTextBoxColumn.HeaderCell.Style.Alignment = viewTextBoxColumn.DefaultCellStyle.Alignment;
            if (viewTextBoxColumn.Name == "QTY")
              viewTextBoxColumn.ReadOnly = true;
            if (empty3 != null)
            {
              if (empty3 != string.Empty)
              {
                try
                {
                  if (int.Parse(empty3) > 0)
                  {
                    viewTextBoxColumn.Width = int.Parse(empty3);
                    if (strArray1[3].ToString() != "True")
                      viewTextBoxColumn.Visible = false;
                    this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
                  }
                  else
                  {
                    viewTextBoxColumn.Width = 0;
                    viewTextBoxColumn.Visible = false;
                    this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
                  }
                }
                catch
                {
                }
              }
            }
          }
        }
        DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
        viewTextBoxColumn1.HeaderCell = new DataGridViewColumnHeaderCell();
        viewTextBoxColumn1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        viewTextBoxColumn1.HeaderCell.Value = (object) "AutoIndexColumn";
        viewTextBoxColumn1.Name = "AutoIndexColumn";
        viewTextBoxColumn1.Tag = (object) "AutoIndexColumn";
        viewTextBoxColumn1.ToolTipText = "HIDDEN";
        viewTextBoxColumn1.Visible = false;
        viewTextBoxColumn1.ReadOnly = true;
        viewTextBoxColumn1.DefaultCellStyle.NullValue = (object) string.Empty;
        this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
        this.dgPartslist.SortCompare += new DataGridViewSortCompareEventHandler(this.dgPartslist_SortCompare);
        try
        {
          DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn2.HeaderCell = new DataGridViewColumnHeaderCell();
          viewTextBoxColumn2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          viewTextBoxColumn2.HeaderCell.Value = (object) "PART_SLIST_KEY";
          viewTextBoxColumn2.Name = "PART_SLIST_KEY";
          viewTextBoxColumn2.Tag = (object) "PART_SLIST_KEY";
          viewTextBoxColumn2.Visible = false;
          viewTextBoxColumn2.ReadOnly = true;
          viewTextBoxColumn2.ToolTipText = "HIDDEN";
          viewTextBoxColumn2.DefaultCellStyle.NullValue = (object) string.Empty;
          this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
        }
        catch
        {
        }
        this.dgPartslist.AllowUserToResizeColumns = true;
        bool bSLColumnsVisible = false;
        for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
        {
          if (this.dgPartslist.Columns[index].Visible)
          {
            bSLColumnsVisible = true;
            break;
          }
        }
        if (bSLColumnsVisible)
        {
          this.frmParent.ApplyPrintSettings(bSLColumnsVisible);
          this.tsBtnAdd.Enabled = true;
          this.tsPrint.Enabled = true;
        }
        else
        {
          this.frmParent.ApplyPrintSettings(bSLColumnsVisible);
          this.tsBtnAdd.Enabled = false;
          this.tsPrint.Enabled = false;
        }
      }
    }

    public void LoadTitle()
    {
      this.Text = this.GetResource("Selection List", "SELECTION_LIST", ResourceType.TITLE) + "      ";
    }

    public void ReLoadPartlistColumns()
    {
      if (this.dgPartslist.InvokeRequired)
      {
        this.dgPartslist.Invoke((Delegate) new frmSelectionList.selListReloadeDelegate(this.ReLoadPartlistColumns));
      }
      else
      {
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "SLIST_SETTINGS");
        for (int index1 = 0; index1 < this.frmParent.PartListGridView.Columns.Count; ++index1)
        {
          try
          {
            if (this.frmParent.PartListGridView.Columns[index1].GetType().ToString() == typeof (DataGridViewTextBoxColumn).ToString())
            {
              DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
              viewTextBoxColumn.HeaderCell = new DataGridViewColumnHeaderCell();
              bool flag = false;
              if (!keys.Contains(this.frmParent.PartListGridView.Columns[index1].Tag) || !keys.Contains((object) this.frmParent.PartListGridView.Columns[index1].Name.ToUpper().ToString()))
              {
                for (int index2 = 0; index2 < this.dgPartslist.Columns.Count; ++index2)
                {
                  if (this.dgPartslist.Columns[index2].HeaderText.ToUpper() == this.frmParent.PartListGridView.Columns[index1].HeaderText.ToUpper() || this.dgPartslist.Columns[index2].Tag.ToString().ToUpper() == this.frmParent.PartListGridView.Columns[index1].Tag.ToString().ToUpper() || this.dgPartslist.Columns[index2].Name.ToUpper() == this.frmParent.PartListGridView.Columns[index1].Name.ToUpper())
                  {
                    flag = true;
                    break;
                  }
                }
              }
              if (!flag)
              {
                viewTextBoxColumn.HeaderText = this.frmParent.PartListGridView.Columns[index1].HeaderText.ToString();
                viewTextBoxColumn.Name = this.frmParent.PartListGridView.Columns[index1].Tag.ToString();
                viewTextBoxColumn.Tag = this.frmParent.PartListGridView.Columns[index1].Tag;
                viewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                viewTextBoxColumn.Visible = false;
                viewTextBoxColumn.ToolTipText = "HIDDEN";
                this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
              }
            }
          }
          catch
          {
          }
        }
        try
        {
          foreach (DataGridViewColumn selectionListColumn in (BaseCollection) this.frmParent.GetSelectionListColumns())
          {
            try
            {
              if (selectionListColumn.GetType().ToString() == typeof (DataGridViewTextBoxColumn).ToString())
              {
                DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
                viewTextBoxColumn.HeaderCell = new DataGridViewColumnHeaderCell();
                bool flag = false;
                for (int index = 0; index < this.dgPartslist.Columns.Count; ++index)
                {
                  if (this.dgPartslist.Columns[index].Name.ToUpper() == selectionListColumn.Name.ToUpper())
                  {
                    flag = true;
                    break;
                  }
                }
                if (!flag)
                {
                  viewTextBoxColumn.HeaderText = selectionListColumn.HeaderText.ToString();
                  viewTextBoxColumn.Name = selectionListColumn.Name;
                  viewTextBoxColumn.Tag = selectionListColumn.Tag;
                  viewTextBoxColumn.Visible = false;
                  this.dgPartslist.Columns.Add((DataGridViewColumn) viewTextBoxColumn);
                }
              }
            }
            catch
            {
            }
          }
        }
        catch
        {
        }
        this.dgPartslist.AllowUserToResizeColumns = true;
      }
    }

    public void selListAddRemoveRecord(int ServerId, XmlNode xSchemaNode, DataGridViewRow dr, bool bAdd, string sParentPageArguments1)
    {
      try
      {
        this.ReLoadPartlistColumns();
        this.sParentPageArguments = sParentPageArguments1;
        object[] objArray = new object[this.dgPartslist.Columns.Count + 1];
        foreach (DataGridViewColumn column in (BaseCollection) this.dgPartslist.Columns)
        {
          string empty = string.Empty;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
          {
            try
            {
              if (attribute.Value.ToUpper() == "PART_SLIST_KEY")
                this.attPartNoElement = attribute.Name;
              if (attribute.Value.ToUpper() == column.Tag.ToString().ToUpper())
              {
                empty = attribute.Value;
                break;
              }
            }
            catch
            {
            }
          }
          if (column.Name.ToUpper() != "QTY" && column.Name.ToUpper() != "PART_SLIST_KEY")
          {
            if (empty != string.Empty)
            {
              try
              {
                string attributeKey = this.FindAttributeKey(empty, xSchemaNode);
                if (dr.Cells[attributeKey] != null)
                {
                  objArray[column.Index] = dr.Cells[attributeKey].Value;
                  continue;
                }
                continue;
              }
              catch
              {
                continue;
              }
            }
          }
          if (column.Name.ToUpper() == "PART_SLIST_KEY")
            objArray[column.Index] = dr.Cells["PART_SLIST_KEY"].Value;
        }
        DataGridViewRow dataGridViewRow = new DataGridViewRow();
        dataGridViewRow.CreateCells(this.dgPartslist, objArray);
        if (bAdd)
        {
          try
          {
            if (this.dgPartslist.Columns.Contains("QTY"))
            {
              int index = this.dgPartslist.Columns["QTY"].Index;
              if (dr.Cells["QTY"].Value != null)
              {
                int.Parse(dr.Cells["QTY"].Value.ToString());
                dataGridViewRow.Cells[index].Value = dr.Cells["QTY"].Value;
              }
              else
                dataGridViewRow.Cells[index].Value = (object) "1";
            }
          }
          catch
          {
            try
            {
              dataGridViewRow.Cells[this.dgPartslist.Columns["QTY"].Index].Value = (object) "1";
            }
            catch
            {
            }
          }
          dataGridViewRow.Tag = (object) this.sParentPageArguments;
          this.dgPartslist.Rows.Add(dataGridViewRow);
          this.dgPartslist.Rows[dataGridViewRow.Index].Selected = false;
          this.dgPartslist.FirstDisplayedScrollingRowIndex = dataGridViewRow.Index;
        }
        else
        {
          foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
          {
            bool flag = true;
            if (row.Cells["PART_SLIST_KEY"].Value != null)
            {
              if (dr.Cells["PART_SLIST_KEY"].Value.ToString() != row.Cells["PART_SLIST_KEY"].Value.ToString())
                flag = false;
            }
            else
              flag = false;
            if (flag)
            {
              this.dgPartslist.Rows.Remove(row);
              break;
            }
          }
        }
      }
      catch
      {
      }
    }

    private string FindAttributeKey(string attVal, XmlNode xListSchema)
    {
      try
      {
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xListSchema.Attributes)
        {
          if (attribute.Value == attVal)
            return attribute.Name;
        }
        return string.Empty;
      }
      catch
      {
        return string.Empty;
      }
    }

    public void ShowQuantityScreen(string sPartNumber)
    {
      string sQuantity = "1";
      try
      {
        if (this.dgPartslist.Rows.Count > 0)
        {
          foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
          {
            if (row.Cells["PART_SLIST_KEY"].Value.ToString() == sPartNumber)
            {
              object obj = row.Cells["QTY"].Value;
              if (obj != null)
              {
                sQuantity = obj.ToString();
                break;
              }
              break;
            }
          }
        }
        int num = (int) new frmSelectionListQuantity(this.frmParent, sPartNumber, sQuantity).ShowDialog((IWin32Window) this.frmParent);
      }
      catch
      {
      }
    }

    public void ChangeQuantity(string sPartNumber, string sQuantity)
    {
      try
      {
        if (this.dgPartslist.Rows.Count <= 0)
          return;
        foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
        {
          if (row.Cells["PART_SLIST_KEY"].Value.ToString() == sPartNumber)
          {
            row.Cells["QTY"].Value = (object) sQuantity;
            break;
          }
        }
      }
      catch
      {
      }
    }

    public void DeleteRow(string sPartNumber)
    {
      try
      {
        if (this.dgPartslist.Rows.Count <= 0)
          return;
        foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
        {
          if (row.Cells["PART_SLIST_KEY"].Value.ToString() == sPartNumber)
          {
            this.dgPartslist.Rows.Remove(row);
            break;
          }
        }
      }
      catch
      {
      }
    }

    public bool PartInSelectionList(string sPartNumber, string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex)
    {
      try
      {
        if (this.dgPartslist.Rows.Count > 0)
        {
          foreach (DataGridViewRow row in (IEnumerable) this.dgPartslist.Rows)
          {
            if (row.Cells["PART_SLIST_KEY"].Value != null && row.Cells["PART_SLIST_KEY"].Value.ToString() == sPartNumber)
            {
              row.Tag.ToString().Split(new string[1]{ "**" }, StringSplitOptions.RemoveEmptyEntries);
              return true;
            }
          }
        }
      }
      catch
      {
      }
      return false;
    }

    public void ClearSelectionList()
    {
      this.dgPartslist.Rows.Clear();
    }

    public void UpdateFont()
    {
      this.dgPartslist.Font = Settings.Default.appFont;
      this.dgPartslist.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgPartslist.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    private void tsBtnDeleteSelection_Click(object sender, EventArgs e)
    {
      foreach (DataGridViewRow selectedRow in (BaseCollection) this.dgPartslist.SelectedRows)
      {
        try
        {
          if (selectedRow.Tag != null && selectedRow.Tag.ToString() != "Manual")
            this.frmParent.CheckUncheckRow(selectedRow.Cells["PART_SLIST_KEY"].Value.ToString(), selectedRow.Tag.ToString(), false);
          else if (selectedRow.Tag != null && selectedRow.Tag.ToString() == "Manual")
            this.frmParent.DeleteSelListRow(selectedRow.Cells["PART_SLIST_KEY"].Value.ToString());
          if (this.dgPartslist.Rows.Contains(selectedRow))
            this.dgPartslist.Rows.Remove(selectedRow);
        }
        catch
        {
        }
      }
    }

    public void ShowLoading()
    {
      this.ShowLoading(this.pnlForm);
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmSelectionList.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
        }
        else
        {
          this.picLoading.Left = parentPanel.Left + parentPanel.Width / 2 - this.picLoading.Width / 2;
          this.picLoading.Top = parentPanel.Top + parentPanel.Height / 2 - this.picLoading.Height / 2;
          this.picLoading.Parent = (Control) parentPanel;
          this.picLoading.BringToFront();
          this.picLoading.Show();
        }
      }
      catch
      {
      }
    }

    private void tsBtnAdd_Click(object sender, EventArgs e)
    {
      int num = (int) new frmAddRecord(this.frmParent, this.dgPartslist.Columns, this.frmParent.ServerId).ShowDialog();
    }

    public void AddNewRecord(DataGridViewRow dRow)
    {
      try
      {
        this.dgPartslist.Rows.Add();
        foreach (DataGridViewColumn column in (BaseCollection) this.dgPartslist.Columns)
        {
          this.dgPartslist[column.Name, this.dgPartslist.Rows.Count - 1].Value = dRow.Cells[column.Name].Value;
          try
          {
            if (column.Name == "PART_SLIST_KEY")
              this.dgPartslist[column.Name, this.dgPartslist.Rows.Count - 1].Value = dRow.Cells[column.Name].Value;
          }
          catch
          {
          }
        }
        this.dgPartslist.Rows[this.dgPartslist.Rows.Count - 1].Tag = (object) "Manual";
        this.dgPartslist.Rows[this.dgPartslist.Rows.Count - 1].Selected = false;
        this.dgPartslist.FirstDisplayedScrollingRowIndex = this.dgPartslist.Rows.Count - 1;
      }
      catch
      {
      }
    }

    private string GetDataGridViewText(ref DataGridView GridView, bool IncludeHeader, bool SelectedRows, string Delimiter)
    {
      string str = string.Empty;
      bool flag = false;
      for (int index = 0; index < GridView.Columns.Count; ++index)
      {
        if (GridView.Columns[index].Visible)
        {
          flag = true;
          break;
        }
      }
      if (!flag || GridView.Rows.Count == 0 || SelectedRows && GridView.SelectedRows.Count == 0)
        return string.Empty;
      if (IncludeHeader)
      {
        for (int index = 0; index < GridView.Columns.Count; ++index)
        {
          if (GridView.Columns[index].Visible && GridView.Columns[index].GetType().ToString() == typeof (DataGridViewTextBoxColumn).ToString())
            str = str + this.GetWriteableValue((object) GridView.Columns[index].HeaderText) + Delimiter;
        }
        str = str.Remove(str.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
      }
      for (int index1 = 0; index1 < GridView.Rows.Count; ++index1)
      {
        if (SelectedRows)
        {
          if (GridView.SelectedRows.Contains(GridView.Rows[index1]))
          {
            for (int index2 = 0; index2 < GridView.Columns.Count; ++index2)
            {
              if (GridView.Columns[index2].Visible && GridView.Columns[index2].GetType().ToString() == typeof (DataGridViewTextBoxColumn).ToString())
                str = str + this.GetWriteableValue(GridView.Rows[index1].Cells[index2].Value) + Delimiter;
            }
            str = str.Remove(str.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
          }
        }
        else
        {
          for (int index2 = 0; index2 < GridView.Columns.Count; ++index2)
          {
            if (GridView.Columns[index2].Visible && GridView.Columns[index2].GetType().ToString() == typeof (DataGridViewTextBoxColumn).ToString())
              str = str + this.GetWriteableValue(GridView.Rows[index1].Cells[index2].Value) + Delimiter;
          }
          str = str.Remove(str.Length - Delimiter.Length, Delimiter.Length) + "\r\n";
        }
      }
      return str;
    }

    private string GetWriteableValue(object o)
    {
      if (o == null || o == Convert.DBNull)
        return "";
      if (o.ToString().IndexOf(",") == -1)
        return o.ToString();
      return "\"" + o.ToString() + "\"";
    }

    public void LoadResources()
    {
      this.Text = this.GetResource("Selection List", "SELECTION_LIST", ResourceType.TITLE) + "      ";
      this.tsBtnDeleteSelection.Text = this.Text = this.GetResource("Delete Selection", "DELETE_SELECTION", ResourceType.TOOLSTRIP);
      this.tsbSelectAll.Text = this.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.TOOLSTRIP);
      this.tsbClearSelection.Text = this.Text = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.TOOLSTRIP);
      this.tsBtnAdd.Text = this.Text = this.GetResource("Add Record", "ADD_RECORD", ResourceType.TOOLSTRIP);
      this.selectAllToolStripMenuItem.Text = this.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.CONTEXT_MENU);
      this.tsmClearSelection.Text = this.Text = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.CONTEXT_MENU);
      this.copyToClipboardToolStripMenuItem.Text = this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.CONTEXT_MENU);
      this.commaSeparatedToolStripMenuItem.Text = this.GetResource("Comma Seperated", "COMMA_SEPERATED", ResourceType.CONTEXT_MENU);
      this.tabSeparatedToolStripMenuItem.Text = this.GetResource("Tab Seperated", "TAB_SEPERATED", ResourceType.CONTEXT_MENU);
      this.exportToFileToolStripMenuItem.Text = this.GetResource("Export To File", "EXPORT_TO_FILE", ResourceType.CONTEXT_MENU);
      this.commaSeparatedToolStripMenuItem1.Text = this.GetResource("Comma Seperated", "COMMA_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
      this.tabSeparatedToolStripMenuItem1.Text = this.GetResource("Tab Seperated", "TAB_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
      this.deleteSelectionToolStripMenuItem.Text = this.GetResource("Delete Selection", "DELETE_SELECTION", ResourceType.CONTEXT_MENU);
      this.tsBtnDeleteAll.Text = this.Text = this.GetResource("Delete All", "DELETE_ALL", ResourceType.TOOLSTRIP);
      this.tsBtnGoToPage.Text = this.Text = this.GetResource("Go to Page", "GO_TO_PAGE", ResourceType.TOOLSTRIP);
      this.goToPageToolStripMenuItem.Text = this.Text = this.GetResource("Go to Page", "GO_TO_PAGE", ResourceType.CONTEXT_MENU);
      this.tsPrint.Text = this.Text = this.GetResource("Print", "PRINT", ResourceType.TOOLSTRIP);
      this.tsBtnThirdPartyBasket.Text = this.GetResource("Basket", "BASKET", ResourceType.TOOLSTRIP);
      this.LoadTitle();
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string xQuery1 = "" + "/Screen[@Name='MAIN_FORM']" + "/Screen[@Name='SELECTION_LIST']";
        switch (rType)
        {
          case ResourceType.BUTTON:
            xQuery1 += "/Resources[@Name='BUTTON']";
            break;
          case ResourceType.LABEL:
            xQuery1 += "/Resources[@Name='LABEL']";
            break;
          case ResourceType.CHECK_BOX:
            xQuery1 += "/Resources[@Name='CHECK_BOX']";
            break;
          case ResourceType.RADIO_BUTTON:
            xQuery1 += "/Resources[@Name='RADIO_BUTTON']";
            break;
          case ResourceType.TITLE:
            return this.frmParent.GetResourceValue(sDefaultValue, xQuery1);
          case ResourceType.COMBO_BOX:
            xQuery1 += "/Resources[@Name='COMBO_BOX']";
            break;
          case ResourceType.LIST_VIEW:
            xQuery1 += "/Resources[@Name='LIST_VIEW']";
            break;
          case ResourceType.GRID_VIEW:
            xQuery1 += "/Resources[@Name='GRID_VIEW']";
            break;
          case ResourceType.TOOLSTRIP:
            xQuery1 += "/Resources[@Name='TOOLSTRIP']";
            break;
          case ResourceType.MENU_BAR:
            xQuery1 += "/Resources[@Name='MENU_BAR']";
            break;
          case ResourceType.CONTEXT_MENU:
            xQuery1 += "/Resources[@Name='CONTEXT_MENU']";
            break;
          case ResourceType.STATUS_MESSAGE:
            xQuery1 += "/Resources[@Name='STATUS_MESSAGE']";
            break;
          case ResourceType.POPUP_MESSAGE:
            xQuery1 += "/Resources[@Name='POPUP_MESSAGE']";
            break;
        }
        string xQuery2 = xQuery1 + "/Resource[@Name='" + sKey + "']";
        return this.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    public DataGridView GetSelectionList()
    {
      return this.dgPartslist;
    }

    public void SaveSelectionListColumnSizes()
    {
      IniFileIO iniFileIo = new IniFileIO();
      ArrayList arrayList = new ArrayList();
      ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "SLIST_SETTINGS");
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (string str in keys)
        dictionary.Add(str, iniFileIo.GetKeyValue("SLIST_SETTINGS", str, Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini"));
      try
      {
        if (this.IsDisposed || keys.Count <= 0)
          return;
        foreach (DataGridViewColumn column in (BaseCollection) this.dgPartslist.Columns)
        {
          string str1 = dictionary[column.Name].ToString();
          if (str1.Split('|')[2] != "0")
          {
            string[] strArray = str1.Split('|');
            strArray[2] = column.Width.ToString();
            string str2 = "";
            foreach (string str3 in strArray)
              str2 = str2 + str3 + "|";
            str1 = str2.TrimEnd('|');
          }
          Program.iniServers[this.frmParent.ServerId].UpdateItem("SLIST_SETTINGS", column.Name, str1);
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void OnOffFeatures()
    {
      try
      {
        this.tsBtnThirdPartyBasket.Visible = Program.objAppFeatures.bThirdPartyBasket;
        this.toolStripSeparator1.Visible = Program.objAppFeatures.bThirdPartyBasket;
      }
      catch
      {
      }
    }

    public void SynSelectionList()
    {
      try
      {
        DataGridViewRowCollection selectionList = this.frmParent.GetSelectionList();
        if (selectionList.Count == this.dgPartslist.Rows.Count)
          return;
        for (int index1 = 0; index1 < selectionList.Count; ++index1)
        {
          this.dgPartslist.Rows.Add();
          DataGridViewRow row = this.dgPartslist.Rows[this.dgPartslist.Rows.Count - 1];
          for (int index2 = 0; index2 < this.dgPartslist.Columns.Count; ++index2)
          {
            string name = this.dgPartslist.Columns[index2].Name;
            try
            {
              row.Cells[name].Value = selectionList[index1].Cells[name].Value;
            }
            catch
            {
            }
          }
          row.Tag = selectionList[index1].Tag;
        }
      }
      catch
      {
      }
    }

    private string GetDGHeaderCellValue(string sKey, string sDefaultHeaderValue)
    {
      string str1 = string.Empty;
      bool flag = false;
      if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
      {
        string str2 = Settings.Default.appLanguage + "_GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini";
        if (File.Exists(Application.StartupPath + "\\Language XMLs\\" + str2))
        {
          TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str2);
          string str3;
          while ((str3 = textReader.ReadLine()) != null)
          {
            if (str3.ToUpper() == "[SLIST_SETTINGS]")
              flag = true;
            else if (str3.Contains("=") && flag)
            {
              string[] strArray = str3.Split(new string[1]
              {
                "="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray[0].ToString().ToUpper() == sKey.ToUpper())
              {
                str1 = strArray[1];
                break;
              }
            }
            else if (str3.Contains("["))
              flag = false;
          }
          if (str1 == "")
            str1 = sDefaultHeaderValue;
          textReader.Close();
        }
        else
          str1 = sDefaultHeaderValue;
      }
      else
        str1 = sDefaultHeaderValue;
      return str1;
    }

    public void SetGridHeaderText()
    {
      try
      {
        if (this.IsDisposed)
          return;
        foreach (DataGridViewColumn column in (BaseCollection) this.dgPartslist.Columns)
        {
          try
          {
            if (Program.iniServers[this.frmParent.p_ServerId].items["SLIST_SETTINGS", column.Name.ToString().ToUpper()] != null)
            {
              IniFileIO iniFileIo = new IniFileIO();
              ArrayList arrayList = new ArrayList();
              ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "SLIST_SETTINGS");
              for (int index = 0; index < keys.Count; ++index)
              {
                if (keys[index].ToString().ToUpper() == column.Name.ToUpper())
                {
                  string keyValue = iniFileIo.GetKeyValue("SLIST_SETTINGS", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
                  string sDefaultHeaderValue = keyValue.Substring(0, keyValue.IndexOf("|"));
                  column.HeaderText = this.GetDGHeaderCellValue(column.Name.ToUpper(), sDefaultHeaderValue);
                  break;
                }
              }
            }
          }
          catch
          {
          }
        }
      }
      catch
      {
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
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmSelectionList));
      this.pnlForm = new Panel();
      this.dgPartslist = new DataGridView();
      this.Column1 = new DataGridViewTextBoxColumn();
      this.cmsSelectionlist = new ContextMenuStrip(this.components);
      this.selectAllToolStripMenuItem = new ToolStripMenuItem();
      this.tsmClearSelection = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
      this.commaSeparatedToolStripMenuItem = new ToolStripMenuItem();
      this.tabSeparatedToolStripMenuItem = new ToolStripMenuItem();
      this.exportToFileToolStripMenuItem = new ToolStripMenuItem();
      this.commaSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
      this.tabSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
      this.toolStripSeparator3 = new ToolStripSeparator();
      this.deleteSelectionToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator6 = new ToolStripSeparator();
      this.goToPageToolStripMenuItem = new ToolStripMenuItem();
      this.toolStrip1 = new ToolStrip();
      this.tsBtnGoToPage = new ToolStripButton();
      this.toolStripSeparator7 = new ToolStripSeparator();
      this.tsBtnAdd = new ToolStripButton();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.tsbClearSelection = new ToolStripButton();
      this.tsbSelectAll = new ToolStripButton();
      this.toolStripSeparator4 = new ToolStripSeparator();
      this.tsBtnDeleteSelection = new ToolStripButton();
      this.toolStripSeparator8 = new ToolStripSeparator();
      this.tsBtnDeleteAll = new ToolStripButton();
      this.tsPrint = new ToolStripButton();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.tsBtnThirdPartyBasket = new ToolStripButton();
      this.picLoading = new PictureBox();
      this.FolderBrowserDialog = new FolderBrowserDialog();
      this.dlgSaveFile = new SaveFileDialog();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.pnlForm.SuspendLayout();
      ((ISupportInitialize) this.dgPartslist).BeginInit();
      this.cmsSelectionlist.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.dgPartslist);
      this.pnlForm.Controls.Add((Control) this.toolStrip1);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(625, 266);
      this.pnlForm.TabIndex = 4;
      this.dgPartslist.AccessibleRole = AccessibleRole.None;
      this.dgPartslist.AllowUserToAddRows = false;
      this.dgPartslist.AllowUserToDeleteRows = false;
      this.dgPartslist.AllowUserToResizeRows = false;
      this.dgPartslist.BackgroundColor = Color.White;
      this.dgPartslist.BorderStyle = BorderStyle.None;
      this.dgPartslist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgPartslist.Columns.AddRange((DataGridViewColumn) this.Column1);
      this.dgPartslist.ContextMenuStrip = this.cmsSelectionlist;
      this.dgPartslist.Dock = DockStyle.Fill;
      this.dgPartslist.Location = new Point(0, 25);
      this.dgPartslist.Name = "dgPartslist";
      this.dgPartslist.RowHeadersVisible = false;
      this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgPartslist.Size = new Size(623, 239);
      this.dgPartslist.TabIndex = 20;
      this.dgPartslist.MouseDown += new MouseEventHandler(this.dgPartslist_MouseDown);
      this.dgPartslist.SortCompare += new DataGridViewSortCompareEventHandler(this.dgPartslist_SortCompare);
      this.dgPartslist.CellDoubleClick += new DataGridViewCellEventHandler(this.dgPartslist_CellDoubleClick);
      this.dgPartslist.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgPartslist_RowsAdded);
      this.dgPartslist.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgPartslist_RowsRemoved);
      this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
      this.Column1.HeaderText = "SelectionList";
      this.Column1.Name = "Column1";
      this.Column1.Visible = false;
      this.Column1.Width = 92;
      this.cmsSelectionlist.Items.AddRange(new ToolStripItem[9]
      {
        (ToolStripItem) this.selectAllToolStripMenuItem,
        (ToolStripItem) this.tsmClearSelection,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.copyToClipboardToolStripMenuItem,
        (ToolStripItem) this.exportToFileToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.deleteSelectionToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator6,
        (ToolStripItem) this.goToPageToolStripMenuItem
      });
      this.cmsSelectionlist.Name = "cmsPartslist";
      this.cmsSelectionlist.Size = new Size(163, 154);
      this.cmsSelectionlist.Opening += new CancelEventHandler(this.cmsSelectionlist_Opening);
      this.selectAllToolStripMenuItem.Enabled = false;
      this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
      this.selectAllToolStripMenuItem.Size = new Size(162, 22);
      this.selectAllToolStripMenuItem.Text = "Select All";
      this.selectAllToolStripMenuItem.Click += new EventHandler(this.selectAllToolStripMenuItem_Click);
      this.tsmClearSelection.Enabled = false;
      this.tsmClearSelection.Name = "tsmClearSelection";
      this.tsmClearSelection.Size = new Size(162, 22);
      this.tsmClearSelection.Text = "Clear Selection";
      this.tsmClearSelection.Click += new EventHandler(this.tsmClearSelection_Click);
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(159, 6);
      this.copyToClipboardToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.commaSeparatedToolStripMenuItem,
        (ToolStripItem) this.tabSeparatedToolStripMenuItem
      });
      this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
      this.copyToClipboardToolStripMenuItem.Size = new Size(162, 22);
      this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
      this.commaSeparatedToolStripMenuItem.Name = "commaSeparatedToolStripMenuItem";
      this.commaSeparatedToolStripMenuItem.Size = new Size(162, 22);
      this.commaSeparatedToolStripMenuItem.Text = "Comma Separated";
      this.commaSeparatedToolStripMenuItem.Click += new EventHandler(this.commaSeparatedToolStripMenuItem_Click);
      this.tabSeparatedToolStripMenuItem.Name = "tabSeparatedToolStripMenuItem";
      this.tabSeparatedToolStripMenuItem.Size = new Size(162, 22);
      this.tabSeparatedToolStripMenuItem.Text = "Tab Separated";
      this.tabSeparatedToolStripMenuItem.Click += new EventHandler(this.tabSeparatedToolStripMenuItem_Click);
      this.exportToFileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.commaSeparatedToolStripMenuItem1,
        (ToolStripItem) this.tabSeparatedToolStripMenuItem1
      });
      this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
      this.exportToFileToolStripMenuItem.Size = new Size(162, 22);
      this.exportToFileToolStripMenuItem.Text = "Export To File";
      this.commaSeparatedToolStripMenuItem1.Name = "commaSeparatedToolStripMenuItem1";
      this.commaSeparatedToolStripMenuItem1.Size = new Size(162, 22);
      this.commaSeparatedToolStripMenuItem1.Text = "Comma Separated";
      this.commaSeparatedToolStripMenuItem1.Click += new EventHandler(this.commaSeparatedToolStripMenuItem1_Click);
      this.tabSeparatedToolStripMenuItem1.Name = "tabSeparatedToolStripMenuItem1";
      this.tabSeparatedToolStripMenuItem1.Size = new Size(162, 22);
      this.tabSeparatedToolStripMenuItem1.Text = "Tab Separated";
      this.tabSeparatedToolStripMenuItem1.Click += new EventHandler(this.tabSeparatedToolStripMenuItem1_Click);
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(159, 6);
      this.deleteSelectionToolStripMenuItem.Name = "deleteSelectionToolStripMenuItem";
      this.deleteSelectionToolStripMenuItem.Size = new Size(162, 22);
      this.deleteSelectionToolStripMenuItem.Text = "Delete Selection";
      this.deleteSelectionToolStripMenuItem.Click += new EventHandler(this.deleteSelectionToolStripMenuItem_Click);
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new Size(159, 6);
      this.goToPageToolStripMenuItem.Enabled = false;
      this.goToPageToolStripMenuItem.Name = "goToPageToolStripMenuItem";
      this.goToPageToolStripMenuItem.Size = new Size(162, 22);
      this.goToPageToolStripMenuItem.Text = "Go To Page";
      this.goToPageToolStripMenuItem.Click += new EventHandler(this.openPageToolStripMenuItem_Click);
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new ToolStripItem[13]
      {
        (ToolStripItem) this.tsBtnGoToPage,
        (ToolStripItem) this.toolStripSeparator7,
        (ToolStripItem) this.tsBtnAdd,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.tsbClearSelection,
        (ToolStripItem) this.tsbSelectAll,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.tsBtnDeleteSelection,
        (ToolStripItem) this.toolStripSeparator8,
        (ToolStripItem) this.tsBtnDeleteAll,
        (ToolStripItem) this.tsPrint,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.tsBtnThirdPartyBasket
      });
      this.toolStrip1.Location = new Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RightToLeft = RightToLeft.Yes;
      this.toolStrip1.Size = new Size(623, 25);
      this.toolStrip1.TabIndex = 21;
      this.tsBtnGoToPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnGoToPage.Enabled = false;
      this.tsBtnGoToPage.Image = (Image) GSPcLocalViewer.Properties.Resources.SelectionListGotopage;
      this.tsBtnGoToPage.ImageTransparentColor = Color.Magenta;
      this.tsBtnGoToPage.Name = "tsBtnGoToPage";
      this.tsBtnGoToPage.RightToLeft = RightToLeft.No;
      this.tsBtnGoToPage.Size = new Size(23, 22);
      this.tsBtnGoToPage.Text = "Go To Page";
      this.tsBtnGoToPage.EnabledChanged += new EventHandler(this.tsBtnGoToPage_EnabledChanged);
      this.tsBtnGoToPage.Click += new EventHandler(this.tsBtnOpenParentPage_Click);
      this.toolStripSeparator7.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new Size(6, 25);
      this.tsBtnAdd.Alignment = ToolStripItemAlignment.Right;
      this.tsBtnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnAdd.Image = (Image) GSPcLocalViewer.Properties.Resources.SelectionList_Add_record;
      this.tsBtnAdd.ImageTransparentColor = Color.Magenta;
      this.tsBtnAdd.Name = "tsBtnAdd";
      this.tsBtnAdd.RightToLeft = RightToLeft.No;
      this.tsBtnAdd.Size = new Size(23, 22);
      this.tsBtnAdd.Text = "Add Record";
      this.tsBtnAdd.Click += new EventHandler(this.tsBtnAdd_Click);
      this.toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(6, 25);
      this.tsbClearSelection.Alignment = ToolStripItemAlignment.Right;
      this.tsbClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbClearSelection.Enabled = false;
      this.tsbClearSelection.Image = (Image) GSPcLocalViewer.Properties.Resources.SelectionList_clear_selection;
      this.tsbClearSelection.ImageTransparentColor = Color.Magenta;
      this.tsbClearSelection.Name = "tsbClearSelection";
      this.tsbClearSelection.RightToLeft = RightToLeft.No;
      this.tsbClearSelection.Size = new Size(23, 22);
      this.tsbClearSelection.Text = "Clear Selection";
      this.tsbClearSelection.EnabledChanged += new EventHandler(this.tsbClearSelection_EnabledChanged);
      this.tsbClearSelection.Click += new EventHandler(this.tsbClearSelection_Click);
      this.tsbSelectAll.Alignment = ToolStripItemAlignment.Right;
      this.tsbSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSelectAll.Enabled = false;
      this.tsbSelectAll.Image = (Image) GSPcLocalViewer.Properties.Resources.SelectionList_select_all;
      this.tsbSelectAll.ImageTransparentColor = Color.Magenta;
      this.tsbSelectAll.Name = "tsbSelectAll";
      this.tsbSelectAll.RightToLeft = RightToLeft.No;
      this.tsbSelectAll.Size = new Size(23, 22);
      this.tsbSelectAll.Text = "Select All";
      this.tsbSelectAll.EnabledChanged += new EventHandler(this.tsbSelectAll_EnabledChanged);
      this.tsbSelectAll.Click += new EventHandler(this.tsbSelectAll_Click);
      this.toolStripSeparator4.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(6, 25);
      this.tsBtnDeleteSelection.Alignment = ToolStripItemAlignment.Right;
      this.tsBtnDeleteSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnDeleteSelection.Enabled = false;
      this.tsBtnDeleteSelection.Image = (Image) GSPcLocalViewer.Properties.Resources.SelectionList_DeleteSelection;
      this.tsBtnDeleteSelection.ImageTransparentColor = Color.Magenta;
      this.tsBtnDeleteSelection.Name = "tsBtnDeleteSelection";
      this.tsBtnDeleteSelection.RightToLeft = RightToLeft.No;
      this.tsBtnDeleteSelection.Size = new Size(23, 22);
      this.tsBtnDeleteSelection.Text = "Delete Selection";
      this.tsBtnDeleteSelection.EnabledChanged += new EventHandler(this.tsBtnDeleteSelection_EnabledChanged);
      this.tsBtnDeleteSelection.Click += new EventHandler(this.tsBtnDeleteSelection_Click);
      this.toolStripSeparator8.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new Size(6, 25);
      this.tsBtnDeleteAll.Alignment = ToolStripItemAlignment.Right;
      this.tsBtnDeleteAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnDeleteAll.Enabled = false;
      this.tsBtnDeleteAll.Image = (Image) GSPcLocalViewer.Properties.Resources.SelectionList__DeleteAll;
      this.tsBtnDeleteAll.ImageTransparentColor = Color.Magenta;
      this.tsBtnDeleteAll.Name = "tsBtnDeleteAll";
      this.tsBtnDeleteAll.Size = new Size(23, 22);
      this.tsBtnDeleteAll.Text = "toolStripButton1";
      this.tsBtnDeleteAll.Click += new EventHandler(this.toolStripButton1_Click);
      this.tsPrint.Alignment = ToolStripItemAlignment.Right;
      this.tsPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsPrint.Enabled = false;
      this.tsPrint.Image = (Image) GSPcLocalViewer.Properties.Resources.Print;
      this.tsPrint.ImageTransparentColor = Color.Magenta;
      this.tsPrint.Name = "tsPrint";
      this.tsPrint.Size = new Size(23, 22);
      this.tsPrint.Text = "Print";
      this.tsPrint.Click += new EventHandler(this.tsPrint_Click);
      this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(6, 25);
      this.tsBtnThirdPartyBasket.Alignment = ToolStripItemAlignment.Right;
      this.tsBtnThirdPartyBasket.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnThirdPartyBasket.Image = (Image) GSPcLocalViewer.Properties.Resources.basket;
      this.tsBtnThirdPartyBasket.ImageTransparentColor = Color.Magenta;
      this.tsBtnThirdPartyBasket.Name = "tsBtnThirdPartyBasket";
      this.tsBtnThirdPartyBasket.Size = new Size(23, 22);
      this.tsBtnThirdPartyBasket.Text = "Basket";
      this.tsBtnThirdPartyBasket.Click += new EventHandler(this.tsBtnThirdPartyBasket_Click);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Dock = DockStyle.Fill;
      this.picLoading.Image = (Image) GSPcLocalViewer.Properties.Resources.Loading1;
      this.picLoading.Location = new Point(0, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(623, 264);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 19;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.dataGridViewTextBoxColumn1.HeaderText = "SelectionList";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Visible = false;
      this.dataGridViewTextBoxColumn1.Width = 1000;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(625, 266);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmSelectionList);
      this.Text = "Selection List";
      this.VisibleChanged += new EventHandler(this.frmSelectionList_VisibleChanged);
      this.pnlForm.ResumeLayout(false);
      this.pnlForm.PerformLayout();
      ((ISupportInitialize) this.dgPartslist).EndInit();
      this.cmsSelectionlist.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    public delegate void selListInitializeDelegate();

    public delegate void selListReloadeDelegate();

    private delegate void ShowLoadingDelegate(Panel parentPanel);
  }
}

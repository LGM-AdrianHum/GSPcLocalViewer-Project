// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Dashbord
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class Dashbord : Form
  {
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
    public static bool bShowProxy = true;
    private string strMessage = "";
    private string strPageInfo = "";
    private int nodeid = 1;
    private const uint SWP_NOSIZE = 1;
    private const uint SWP_NOMOVE = 2;
    private const uint TOPMOST_FLAGS = 3;
    private IContainer components;
    private Label lblDashbord;
    private BackgroundWorker bgWorker_PIPES;
    private NotifyIcon notifyIcon1;
    private BackgroundWorker bgWorker_SLWebInterface;
    private frmViewer objFrmViewer;
    private ArrayList arrViewer;
    private NamedPipeServerStream pipeServer;
    private Thread thDataSize;
    private NamedPipeServerStream pipeServerPage;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Dashbord));
      this.lblDashbord = new Label();
      this.bgWorker_PIPES = new BackgroundWorker();
      this.notifyIcon1 = new NotifyIcon(this.components);
      this.bgWorker_SLWebInterface = new BackgroundWorker();
      this.SuspendLayout();
      this.lblDashbord.AutoSize = true;
      this.lblDashbord.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblDashbord.Location = new Point(12, 9);
      this.lblDashbord.Name = "lblDashbord";
      this.lblDashbord.Size = new Size(254, 48);
      this.lblDashbord.TabIndex = 0;
      this.lblDashbord.Text = "MAIN Form\r\nused to launch multiple instances of viewer\r\nthis form is never shown\r\n";
      this.bgWorker_PIPES.DoWork += new DoWorkEventHandler(this.bgWorker_PIPES_DoWork);
      this.bgWorker_PIPES.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_PIPES_RunWorkerCompleted);
      this.notifyIcon1.Text = "notifyIcon1";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.Click += new EventHandler(this.notifyIcon1_Click);
      this.bgWorker_SLWebInterface.DoWork += new DoWorkEventHandler(this.bgWorker_SLWebInterface_DoWork);
      this.bgWorker_SLWebInterface.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_SLWebInterface_RunWorkerCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(285, 73);
      this.Controls.Add((Control) this.lblDashbord);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Dashbord);
      this.Opacity = 0.0;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (Dashbord);
      this.WindowState = FormWindowState.Minimized;
      this.FormClosing += new FormClosingEventHandler(this.Dashbord_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr SetFocus(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool BringWindowToTop(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool BringWindowToTop(HandleRef hWnd);

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

    public Dashbord()
    {
      this.InitializeComponent();
      this.Visible = false;
      this.arrViewer = new ArrayList();
      try
      {
        this.bgWorker_PIPES.RunWorkerAsync();
        this.bgWorker_SLWebInterface.RunWorkerAsync();
        if (!DataSize.IsDataSizeApplied())
          return;
        this.thDataSize = new Thread(new ThreadStart(this.CheckDataSize));
        this.thDataSize.Start();
      }
      catch
      {
      }
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 128;
        return createParams;
      }
    }

    public void FirstTime(string[] args)
    {
      Program.bNoViewerOpen = true;
      this.objFrmViewer = new frmViewer(this);
      this.arrViewer.Add((object) this.objFrmViewer);
      this.objFrmViewer.SetArguments(args);
      this.objFrmViewer.Show();
      uint windowThreadProcessId = Dashbord.GetWindowThreadProcessId(Dashbord.GetForegroundWindow(), IntPtr.Zero);
      uint currentThreadId = Dashbord.GetCurrentThreadId();
      if ((int) windowThreadProcessId != (int) currentThreadId)
      {
        Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, true);
        Dashbord.BringWindowToTop(this.objFrmViewer.Handle);
        Dashbord.ShowWindow(this.objFrmViewer.Handle, 5U);
        Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, false);
      }
      else
      {
        Dashbord.BringWindowToTop(this.objFrmViewer.Handle);
        Dashbord.ShowWindow(this.objFrmViewer.Handle, 5U);
      }
      this.objFrmViewer.Activate();
    }

    public void NextTime(string[] args)
    {
      Program.bNoViewerOpen = false;
      frmViewer frmViewer = new frmViewer(this);
      this.arrViewer.Add((object) frmViewer);
      frmViewer.SetArguments(args);
      int curServerId = -1;
      try
      {
        for (int index = 0; index < Program.iniServers.Length; ++index)
        {
          if (Program.iniServers[index].sIniKey.ToUpper() == args[1].ToUpper())
          {
            curServerId = index;
            break;
          }
        }
      }
      catch
      {
      }
      if (curServerId != -1)
        frmViewer.SetCurrentServerID(curServerId);
      frmViewer.Show();
      frmViewer.objFrmPicture.HighLightText = Program.HighLightText;
      frmViewer.objFrmPicture.DjVuPageNumber = Program.DjVuPageNumber;
      uint windowThreadProcessId = Dashbord.GetWindowThreadProcessId(Dashbord.GetForegroundWindow(), IntPtr.Zero);
      uint currentThreadId = Dashbord.GetCurrentThreadId();
      if ((int) windowThreadProcessId != (int) currentThreadId)
      {
        Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, true);
        Dashbord.BringWindowToTop(frmViewer.Handle);
        Dashbord.ShowWindow(frmViewer.Handle, 5U);
        Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, false);
      }
      else
      {
        Dashbord.BringWindowToTop(frmViewer.Handle);
        Dashbord.ShowWindow(frmViewer.Handle, 5U);
      }
      frmViewer.Activate();
    }

    public void CloseAll()
    {
      try
      {
        do
        {
          try
          {
            ((Form) this.arrViewer[0]).Close();
          }
          catch
          {
          }
        }
        while (this.arrViewer.Count != 0);
        this.arrViewer.Clear();
        this.Close();
      }
      catch
      {
      }
    }

    public void CloseViewer(frmViewer f)
    {
      if (f != null)
      {
        if (this.arrViewer.Count > 0)
          this.arrViewer.Remove((object) f);
        if (this.arrViewer.Count != 0)
          return;
        this.Close();
      }
      else
        this.Close();
    }

    public void SetBookType(string SBookType)
    {
      try
      {
        ((frmViewer) this.arrViewer[this.arrViewer.Count - 1]).sBookType = SBookType;
      }
      catch
      {
      }
    }

    public bool PartInSelectionListA(string sPartNumber, string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex)
    {
      try
      {
        return ((frmViewer) this.arrViewer[0]).objFrmSelectionlist.PartInSelectionList(sPartNumber, sServerKey, sBookPubId, sPageId, sImageIndex, sListIndex);
      }
      catch
      {
        return false;
      }
    }

    public void SelListAddRemoveRow(int ServerId, XmlNode xSchemaNode, DataGridViewRow dr, bool bAddRow, string sTag)
    {
      try
      {
        if (this.arrViewer.Count <= 0)
          return;
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmSelectionlist.selListAddRemoveRecord(ServerId, xSchemaNode, dr, bAddRow, sTag);
      }
      catch
      {
      }
    }

    public void UncheckAllRows()
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmPartlist.UncheckAllRows();
      }
      catch
      {
      }
    }

    public void AddNewRecord(DataGridViewRow dRow)
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmSelectionlist.AddNewRecord(dRow);
      }
      catch
      {
      }
    }

    public void ChangeSelListQuantity(string sPartNumber, string sQuantity)
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmSelectionlist.ChangeQuantity(sPartNumber, sQuantity);
      }
      catch
      {
      }
    }

    public void ClearSelectionList()
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmSelectionlist.ClearSelectionList();
      }
      catch
      {
      }
    }

    public void CheckUncheckRow(string iRowNumber, string sPartArguments, bool bCheck)
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmPartlist.CheckUncheckRow(iRowNumber, bCheck);
      }
      catch
      {
      }
    }

    public void ChangeQuantity(string sPartNumber, string sQuantity)
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmSelectionlist.ChangeQuantity(sPartNumber, sQuantity);
      }
      catch
      {
      }
    }

    public void DeleteRow(string sPartNumber)
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmSelectionlist.DeleteRow(sPartNumber);
      }
      catch
      {
      }
    }

    public void CheckUncheckRow(string sPartNumber, bool bCheck)
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          ((frmViewer) this.arrViewer[index]).objFrmPartlist.CheckUncheckRow(sPartNumber, bCheck);
      }
      catch
      {
      }
    }

    public DataGridViewRowCollection GetSelectionList()
    {
      try
      {
        int index1 = -1;
        for (int index2 = 0; index2 < this.arrViewer.Count - 1; ++index2)
        {
          if (((frmViewer) this.arrViewer[index2]).sBookType.ToUpper() == "GSP")
          {
            index1 = index2;
            break;
          }
        }
        return ((frmViewer) this.arrViewer[index1]).objFrmSelectionlist.dgPartslist.Rows;
      }
      catch
      {
        return (DataGridViewRowCollection) null;
      }
    }

    public DataGridViewColumnCollection GetSelectionListColumns()
    {
      try
      {
        return ((frmViewer) this.arrViewer[0]).objFrmSelectionlist.dgPartslist.Columns;
      }
      catch
      {
        return (DataGridViewColumnCollection) null;
      }
    }

    private void bgWorker_PIPES_DoWork(object sender, DoWorkEventArgs e)
    {
      try
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        using (this.pipeServer = new NamedPipeServerStream("0123456789ABCDEF", PipeDirection.Out))
        {
          this.pipeServer.WaitForConnection();
          try
          {
            DataGridView dGridView = new DataGridView();
            DataGridView dataGridView = new DataGridView();
            DataGridView selectionList = ((frmViewer) this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
            foreach (DataGridViewColumn column in (BaseCollection) selectionList.Columns)
              dGridView.Columns.Add(column.Clone() as DataGridViewColumn);
            foreach (DataGridViewRow row in (IEnumerable) selectionList.Rows)
            {
              int index = dGridView.Rows.Add(row.Clone() as DataGridViewRow);
              foreach (DataGridViewCell cell in (BaseCollection) row.Cells)
                dGridView.Rows[index].Cells[cell.ColumnIndex].Value = cell.Value;
            }
            int count = dGridView.Columns.Count;
            for (int index1 = 0; index1 < count; ++index1)
            {
              IniFileIO iniFileIo = new IniFileIO();
              ArrayList arrayList = new ArrayList();
              string empty = string.Empty;
              string str1 = Application.StartupPath + "\\GSP_" + Program.iniServers[this.objFrmViewer.ServerId].sIniKey + ".ini";
              ArrayList keys = iniFileIo.GetKeys(str1, "SLIST_SETTINGS");
              for (int index2 = 0; index2 < keys.Count; ++index2)
              {
                if (keys[index2].ToString() == dGridView.Columns[index1].Tag.ToString())
                {
                  dGridView.Columns[index1].Visible = true;
                  string str2 = iniFileIo.GetKeyValue("SLIST_SETTINGS", keys[index2].ToString().ToUpper(), str1);
                  if (str2.Split('|').Length == 3)
                    str2 = str2 + "|True|True|" + str2.Split('|')[1] + "|" + str2.Split('|')[2];
                  else if (str2.Split('|').Length == 4)
                    str2 = str2 + "|True|True|" + str2.Split('|')[1] + "|" + str2.Split('|')[2];
                  if (str2.Split('|')[4].ToString() == "False")
                    dGridView.Columns[index1].Visible = false;
                }
              }
            }
            DataSet dataSet = new DataSet();
            using (StreamWriter streamWriter = new StreamWriter((Stream) this.pipeServer))
              this.SLDataGridViewToDataTable(dGridView).WriteXml((TextWriter) streamWriter);
          }
          catch (IOException ex)
          {
            Console.WriteLine("ERROR: {0}", (object) ex.Message);
          }
        }
      }
      catch
      {
      }
    }

    private void bgWorker_PIPES_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      try
      {
        this.bgWorker_PIPES.RunWorkerAsync();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
    }

    private DataSet DataGridViewToDataTable(DataGridView dGridView)
    {
      DataSet dataSet = new DataSet();
      dataSet.Tables.Add();
      try
      {
        for (int index = 0; index < dGridView.Columns.Count; ++index)
        {
          if (dGridView.Columns[index].ToolTipText != "HIDDEN")
          {
            string empty = string.Empty;
            string columnName;
            try
            {
              columnName = dGridView.Columns[index].HeaderText == null || dataSet.Tables[0].Columns.Contains(dGridView.Columns[index].HeaderText) ? dGridView.Columns[index].Name : dGridView.Columns[index].HeaderText;
            }
            catch
            {
              columnName = string.Empty;
            }
            try
            {
              if (columnName != null)
                dataSet.Tables[0].Columns.Add(columnName);
            }
            catch
            {
            }
          }
        }
        for (int index1 = 0; index1 < dGridView.Rows.Count; ++index1)
        {
          DataRow row = dataSet.Tables[0].NewRow();
          for (int index2 = 0; index2 < dGridView.Columns.Count; ++index2)
          {
            string empty = string.Empty;
            string index3;
            try
            {
              index3 = dGridView.Columns[index2].HeaderText == null ? dGridView.Columns[index2].Name : dGridView.Columns[index2].HeaderText;
            }
            catch
            {
              index3 = string.Empty;
            }
            try
            {
              row[index3] = dGridView[index2, index1].Value == null ? (object) string.Empty : (object) dGridView[index2, index1].Value.ToString();
            }
            catch
            {
            }
          }
          dataSet.Tables[0].Rows.Add(row);
        }
      }
      catch
      {
      }
      return dataSet;
    }

    private DataSet SLDataGridViewToDataTable(DataGridView dGridView)
    {
      DataSet dataSet = new DataSet();
      dataSet.Tables.Add();
      List<string> stringList = new List<string>();
      try
      {
        for (int index = 0; index < dGridView.Columns.Count; ++index)
        {
          if (dGridView.Columns[index].Visible)
          {
            string columnName = string.Empty;
            try
            {
              if (dGridView.Columns[index].HeaderText != null && dataSet.Tables[0].Columns.Count == 0)
                columnName = dGridView.Columns[index].HeaderText;
              else if (dGridView.Columns[index].HeaderText != null)
              {
                if (dataSet.Tables[0].Columns.Count > 0)
                {
                  foreach (DataColumn column in (InternalDataCollectionBase) dataSet.Tables[0].Columns)
                  {
                    if (column.ColumnName.ToUpper().Trim() != dGridView.Columns[index].HeaderText.ToUpper().Trim())
                    {
                      columnName = dGridView.Columns[index].HeaderText;
                      break;
                    }
                  }
                }
              }
            }
            catch
            {
              columnName = string.Empty;
            }
            try
            {
              if (columnName != null)
              {
                dataSet.Tables[0].Columns.Add(columnName);
                stringList.Add(columnName);
              }
            }
            catch
            {
            }
          }
        }
        int count = dataSet.Tables[0].Columns.Count;
        for (int index1 = 0; index1 < dGridView.Rows.Count - 1; ++index1)
        {
          DataRow row = dataSet.Tables[0].NewRow();
          for (int index2 = 0; index2 < dGridView.Columns.Count; ++index2)
          {
            bool flag = false;
            string index3 = string.Empty;
            for (int index4 = 0; index4 < stringList.Count; ++index4)
            {
              if (dGridView.Columns[index2].HeaderText.ToUpper() == stringList[index4].ToUpper())
              {
                index3 = dGridView.Columns[index2].HeaderText;
                flag = true;
                break;
              }
            }
            if (flag)
            {
              try
              {
                row[index3] = dGridView[index2, index1].Value == null ? (object) string.Empty : (object) dGridView[index2, index1].Value.ToString();
              }
              catch
              {
              }
            }
          }
          dataSet.Tables[0].Rows.Add(row);
        }
      }
      catch
      {
      }
      return dataSet;
    }

    private void CheckDataSize()
    {
      try
      {
        while (true)
        {
          Thread.Sleep(DataSize.miliSecInterval);
          DataSize.UpdateSpaceVars();
          DataSize.miliSecInterval = 20000;
        }
      }
      catch
      {
      }
    }

    public void RunDataSizeChecking()
    {
      if (this.thDataSize != null && this.thDataSize.IsAlive)
      {
        this.thDataSize.Interrupt();
        while (this.thDataSize.IsAlive)
          ;
      }
      if (this.thDataSize != null && this.thDataSize.IsAlive || !DataSize.IsDataSizeApplied())
        return;
      this.thDataSize = new Thread(new ThreadStart(this.CheckDataSize));
      this.thDataSize.Start();
    }

    private void Dashbord_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.thDataSize != null)
      {
        if (this.thDataSize.IsAlive)
          this.thDataSize.Interrupt();
      }
      try
      {
        string str1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName;
        foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
        {
          string str2 = Uri.EscapeDataString(installedPrinter);
          if (File.Exists(str1 + "\\" + str2 + ".bin"))
            File.Delete(str1 + "\\" + str2 + ".bin");
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void ShowNotification(string title, string text, string tooltip)
    {
      if (this.notifyIcon1.Container == null)
      {
        this.notifyIcon1 = new NotifyIcon(this.components);
        this.notifyIcon1.Click += new EventHandler(this.notifyIcon1_Click);
      }
      this.notifyIcon1.Icon = this.Icon;
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.Text = tooltip;
      this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
      this.notifyIcon1.BalloonTipTitle = title;
      this.notifyIcon1.BalloonTipText = text;
      this.notifyIcon1.ShowBalloonTip(5000);
    }

    public void DisposeNotification()
    {
      this.notifyIcon1.Dispose();
    }

    private void notifyIcon1_Click(object sender, EventArgs e)
    {
      this.notifyIcon1.ShowBalloonTip(5000);
    }

    public void ChangeApplicationMode(bool bWorkOffline)
    {
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
        {
          ((frmViewer) this.arrViewer[index]).workOffLineMenuItem.Checked = bWorkOffline;
          if (bWorkOffline)
          {
            ((frmViewer) this.arrViewer[index]).lblMode.ToolTipText = "Working Offline";
            ((frmViewer) this.arrViewer[index]).lblMode.Image = (Image) Resources.offline;
          }
          else
          {
            ((frmViewer) this.arrViewer[index]).lblMode.ToolTipText = "Working Online";
            ((frmViewer) this.arrViewer[index]).lblMode.Image = (Image) Resources.online;
          }
          ((frmViewer) this.arrViewer[index]).tsbSingleBookDownload.Enabled = !bWorkOffline;
          ((frmViewer) this.arrViewer[index]).tsbMultipleBooksDownload.Enabled = !bWorkOffline;
          ((frmViewer) this.arrViewer[index]).singleBookToolStripMenuItem.Enabled = !bWorkOffline;
          ((frmViewer) this.arrViewer[index]).multipleBooksToolStripMenuItem.Enabled = !bWorkOffline;
        }
      }
      catch
      {
      }
    }

    public ArrayList ListOfInUseBooks()
    {
      ArrayList arrayList = new ArrayList();
      try
      {
        for (int index = 0; index < this.arrViewer.Count; ++index)
          arrayList.Add((object) ((frmViewer) this.arrViewer[index]).BookPublishingId);
      }
      catch
      {
      }
      return arrayList;
    }

    private void SLWebDataSend()
    {
      this.strMessage = "";
      string strDatatoSend = string.Empty;
      try
      {
        using (this.pipeServerPage = new NamedPipeServerStream("universal", PipeDirection.InOut))
        {
          this.pipeServerPage.WaitForConnection();
          byte[] numArray = new byte[256];
          int count = 256;
          this.pipeServerPage.Read(numArray, 0, count);
          this.strMessage = Encoding.Unicode.GetString(numArray).TrimEnd(new char[1]);
          try
          {
            string[] strArray1 = this.strMessage.Split('|');
            switch (strArray1[0].ToUpper())
            {
              case "SLST":
                strDatatoSend = this.GetSelectionListData();
                break;
              case "BINF":
                strDatatoSend = this.getBookString();
                break;
              case "PINF":
                strDatatoSend = this.getPageString();
                break;
              case "FLTR":
                strDatatoSend = this.getFilters();
                break;
              case "ADDP":
                bool flag = false;
                for (int index = 0; index < this.arrViewer.Count; ++index)
                {
                  if (((frmViewer) this.arrViewer[index]).BookType == "GSP")
                  {
                    flag = true;
                    break;
                  }
                }
                if (flag)
                {
                  string[] strArray2 = this.strMessage.Split('|');
                  strDatatoSend = this.AddPartToSelectionInterface(strArray2[1], strArray2[2]);
                  break;
                }
                strDatatoSend = "0";
                break;
              case "PNAV":
              case "GTPI":
              case "GTPN":
                int num1 = -1;
                for (int index = 0; index < this.arrViewer.Count; ++index)
                {
                  if (Program.iniServers[((frmViewer) this.arrViewer[index]).ServerId].sIniKey.ToUpper().Equals(strArray1[1].ToUpper()))
                  {
                    num1 = index;
                    break;
                  }
                }
                if (num1 > -1)
                {
                  if (num1 < this.arrViewer.Count)
                  {
                    int index1 = 0;
                    ArrayList tempArray = new ArrayList();
                    for (int index2 = 0; index2 < this.arrViewer.Count; ++index2)
                    {
                      for (int index3 = 0; index3 < ((frmViewer) this.arrViewer[index2]).SchemaNode.Attributes.Count; ++index3)
                      {
                        if (((frmViewer) this.arrViewer[index2]).SchemaNode.Attributes[index3].Value.ToUpper().Equals("PUBLISHINGID") && ((frmViewer) this.arrViewer[index2]).BookNode.Attributes[((frmViewer) this.arrViewer[index2]).SchemaNode.Attributes[index3].Name].Value.ToUpper().Equals(strArray1[2].ToUpper()))
                        {
                          tempArray.Add((object) null);
                          tempArray[index1] = this.arrViewer[index2];
                          ++index1;
                        }
                      }
                    }
                    for (int i = 0; i < tempArray.Count; ++i)
                    {
                      if (!(strArray1[3] != "") || i + 1 == int.Parse(strArray1[3]))
                      {
                        switch (strArray1[4].ToUpper())
                        {
                          case "FSRT":
                            ((frmViewer) tempArray[i]).objFrmTreeview.SelectFirstNode();
                            strDatatoSend = this.PageIDRecursive((TreeView) ((frmViewer) tempArray[i]).CurrentTreeView, this.GetCurrentTVNode(tempArray, i)).ToString();
                            continue;
                          case "PREV":
                            ((frmViewer) tempArray[i]).objFrmTreeview.SelectPreviousNode();
                            strDatatoSend = this.PageIDRecursive((TreeView) ((frmViewer) tempArray[i]).CurrentTreeView, this.GetCurrentTVNode(tempArray, i)).ToString();
                            continue;
                          case "NEXT":
                            ((frmViewer) tempArray[i]).objFrmTreeview.SelectNextNode();
                            strDatatoSend = this.PageIDRecursive((TreeView) ((frmViewer) tempArray[i]).CurrentTreeView, this.GetCurrentTVNode(tempArray, i)).ToString();
                            continue;
                          case "LAST":
                            ((frmViewer) tempArray[i]).objFrmTreeview.SelectLastNode();
                            strDatatoSend = this.PageIDRecursive((TreeView) ((frmViewer) tempArray[i]).CurrentTreeView, this.GetCurrentTVNode(tempArray, i)).ToString();
                            continue;
                          default:
                            if (strArray1[0].ToUpper().Equals("GTPI"))
                            {
                              int num2 = int.Parse(strArray1[4]);
                              if (num2 > 0)
                              {
                                ((frmViewer) tempArray[i]).objFrmTreeview.SelectTreeNode(num2.ToString());
                                string currentTvNode = this.GetCurrentTVNode(tempArray, i);
                                strDatatoSend = this.PageIDRecursive((TreeView) ((frmViewer) tempArray[i]).CurrentTreeView, currentTvNode).ToString();
                                continue;
                              }
                              strDatatoSend = "0";
                              continue;
                            }
                            if (strArray1[0].ToUpper().Equals("GTPN"))
                            {
                              int num2 = this.PageIDRecursive((TreeView) ((frmViewer) tempArray[i]).CurrentTreeView, strArray1[4]);
                              if (num2 > 0)
                              {
                                ((frmViewer) tempArray[i]).objFrmTreeview.SelectTreeNode(num2.ToString());
                                strDatatoSend = num2.ToString();
                                continue;
                              }
                              strDatatoSend = "0";
                              continue;
                            }
                            continue;
                        }
                      }
                    }
                    break;
                  }
                  break;
                }
                break;
              default:
                strDatatoSend = "0";
                break;
            }
          }
          catch
          {
          }
          if (strDatatoSend == "")
            strDatatoSend = "0";
          this.WriteToPipe(strDatatoSend, this.strMessage);
        }
      }
      catch
      {
      }
    }

    private void WriteToPipe(string strDatatoSend, string strMessage)
    {
      if (strMessage.ToUpper().Equals("SLST"))
      {
        DataSet dataSet = new DataSet();
        int num = (int) dataSet.ReadXml((XmlReader) new XmlTextReader((TextReader) new StringReader(strDatatoSend)));
        using (StreamWriter streamWriter = new StreamWriter((Stream) this.pipeServerPage))
        {
          dataSet.WriteXml(Application.StartupPath + "\\1.xml");
          dataSet.WriteXml((TextWriter) streamWriter);
        }
      }
      else
      {
        byte[] bytes = Encoding.Unicode.GetBytes(strDatatoSend);
        this.pipeServerPage.Write(bytes, 0, bytes.Length);
      }
    }

    private string getXMLString(XmlNode xSchema, XmlNode xNode)
    {
      string str = "";
      try
      {
        for (int index1 = 0; index1 < xNode.Attributes.Count; ++index1)
        {
          for (int index2 = 0; index2 < xSchema.Attributes.Count; ++index2)
          {
            if (xNode.Attributes[index1].Name.Equals(xSchema.Attributes[index2].Name))
            {
              str = str + xSchema.Attributes[index2].Value + "=\"" + SecurityElement.Escape(xNode.Attributes[xSchema.Attributes[index2].Name].Value) + "\" ";
              break;
            }
          }
        }
      }
      catch
      {
      }
      return str;
    }

    private string getFilters()
    {
      string str1 = "<Filters>\n";
      for (int index1 = 0; index1 < this.arrViewer.Count; ++index1)
      {
        string str2 = str1 + "<Book ";
        if (((frmViewer) this.arrViewer[index1]).BookNode == null)
        {
          str1 = str2 + ">\n<Filter />\n</Book>\n";
        }
        else
        {
          for (int index2 = 0; index2 < ((frmViewer) this.arrViewer[index1]).SchemaNode.Attributes.Count; ++index2)
          {
            if (((frmViewer) this.arrViewer[index1]).SchemaNode.Attributes[index2].Value.ToUpper().Equals("PUBLISHINGID"))
            {
              string str3 = SecurityElement.Escape(((frmViewer) this.arrViewer[index1]).BookNode.Attributes[((frmViewer) this.arrViewer[index1]).SchemaNode.Attributes[index2].Name].Value);
              if (str3 != "")
              {
                str2 = str2 + ((frmViewer) this.arrViewer[index1]).SchemaNode.Attributes[index2].Value + "=\"" + str3 + "\"";
                break;
              }
              break;
            }
          }
          string str4 = str2 + " ServerKey=\"" + Program.iniServers[((frmViewer) this.arrViewer[index1]).ServerId].sIniKey + "\">\n" + "<Filter";
          string[] filterArgs = ((frmViewer) this.arrViewer[index1]).getFilterArgs();
          if (filterArgs != null && filterArgs.Length > 0)
          {
            foreach (string str3 in filterArgs)
            {
              string[] strArray = str3.Replace("\"", "").Split('=');
              str4 = str4 + " " + strArray[0] + "=\"" + strArray[1] + "\"";
            }
          }
          str1 = str4 + " />\n" + "</Book>\n";
        }
      }
      return str1 + "</Filters>";
    }

    private string GetSelectionListData()
    {
      string empty1 = string.Empty;
      DataGridView dataGridView = new DataGridView();
      DataGridView selectionList = ((frmViewer) this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
      string str1 = empty1 + "<Parts>\n";
      for (int index1 = 0; index1 < selectionList.RowCount; ++index1)
      {
        string str2 = str1 + "<Part ";
        for (int index2 = 0; index2 < selectionList.Columns.Count; ++index2)
        {
          if (selectionList.Columns[index2].ToolTipText != "HIDDEN" && selectionList[index2, index1].Value != null && !(selectionList[index2, index1].Value.ToString() == ""))
          {
            string empty2 = string.Empty;
            string str3 = selectionList[index2, index1].Value.ToString().Replace("&", "&amp;").Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;");
            if (selectionList.Columns[index2].Name.ToUpper().Equals("QTY"))
              str2 = str2 + "Quantity=\"" + str3 + "\" ";
            else
              str2 = str2 + selectionList.Columns[index2].Name + "=\"" + str3 + "\" ";
          }
        }
        str1 = str2 + " />\n";
      }
      if (selectionList.RowCount == 0)
        str1 += "<Part />";
      return str1 + "</Parts>";
    }

    private string getBookString()
    {
      string str1 = string.Empty;
      if (this.arrViewer.Count != 0)
      {
        string str2 = "<Books>\n";
        ArrayList arrViewer = this.arrViewer;
        for (int index = 0; index < arrViewer.Count; ++index)
        {
          XmlNode bookNode = ((frmViewer) arrViewer[index]).BookNode;
          if (bookNode == null)
          {
            str2 += "<Book />\n";
          }
          else
          {
            XmlNode schemaNode = ((frmViewer) arrViewer[index]).SchemaNode;
            str2 = str2 + "<Book " + this.getXMLString(schemaNode, bookNode) + "ServerKey=\"" + Program.iniServers[((frmViewer) arrViewer[index]).ServerId].sIniKey + "\" " + "/>\n";
          }
        }
        str1 = str2 + "</Books>";
      }
      return str1;
    }

    private string getPageString()
    {
      string str1 = "";
      if (this.arrViewer.Count != 0)
      {
        try
        {
          str1 = "<Pages>\n";
          for (int i = 0; i < this.arrViewer.Count; ++i)
          {
            XmlDocument xmlDocument = new XmlDocument();
            if (((frmViewer) this.arrViewer[i]).BookNode != null)
            {
              xmlDocument.LoadXml(this.GetCurrentPageInfo(i));
              XmlNode documentElement1 = (XmlNode) xmlDocument.DocumentElement;
              string innerXml = documentElement1.InnerXml;
              xmlDocument.LoadXml(((frmViewer) this.arrViewer[i]).CurrentTreeView.Tag.ToString());
              XmlNode documentElement2 = (XmlNode) xmlDocument.DocumentElement;
              str1 += "<Page ";
              str1 += this.getXMLString(documentElement2, documentElement1);
              for (int index = 0; index < ((frmViewer) this.arrViewer[i]).SchemaNode.Attributes.Count; ++index)
              {
                if (((frmViewer) this.arrViewer[i]).SchemaNode.Attributes[index].Value.ToUpper().Equals("PUBLISHINGID"))
                {
                  string str2 = SecurityElement.Escape(((frmViewer) this.arrViewer[i]).BookNode.Attributes[((frmViewer) this.arrViewer[i]).SchemaNode.Attributes[index].Name].Value);
                  if (str2 != "")
                  {
                    str1 = str1 + "Book" + ((frmViewer) this.arrViewer[i]).SchemaNode.Attributes[index].Value + "=\"" + str2 + "\" ";
                    break;
                  }
                  break;
                }
              }
              str1 = str1 + "ServerKey=\"" + Program.iniServers[((frmViewer) this.arrViewer[i]).ServerId].sIniKey + "\" ";
              str1 += ">\n";
              foreach (XmlNode childNode in documentElement1.ChildNodes)
              {
                if (childNode.Name.ToUpper().Equals("PIC"))
                {
                  str1 = str1 + "<" + childNode.Name + " ";
                  str1 += this.getXMLString(documentElement2, childNode);
                  str1 += "/>\n";
                }
              }
              str1 += "</Page>\n";
            }
          }
        }
        catch
        {
        }
        if (str1.Equals("<Pages>\n"))
          str1 += "<Page />\n";
        str1 += "</Pages>";
      }
      return str1;
    }

    private void bgWorker_SLWebInterface_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      try
      {
        this.SLWebDataSend();
      }
      catch
      {
      }
    }

    private void bgWorker_SLWebInterface_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      try
      {
        this.bgWorker_SLWebInterface.RunWorkerAsync();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
    }

    private string AddPartToSelectionInterface(string sPart, string sQuantity)
    {
      DataGridViewColumnCollection columns = this.GetSelectionListGrid().Columns;
      DataGridView dataGridView = new DataGridView();
      for (int index = 0; index < columns.Count; ++index)
        dataGridView.Columns.Add(columns[index].Name, columns[index].HeaderText);
      DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
      dataGridViewColumn.ToolTipText = "HIDDEN";
      dataGridViewColumn.Name = "PART_SLIST_KEY";
      dataGridViewColumn.Tag = (object) "PART_SLIST_KEY";
      dataGridView.Columns.Add(dataGridViewColumn);
      DataGridViewRow dataGridViewRow = new DataGridViewRow();
      dataGridViewRow.CreateCells(dataGridView);
      for (int index = 0; index < dataGridViewRow.Cells.Count; ++index)
      {
        if (dataGridView.Columns[index].Name.ToUpper().Equals("PARTNUMBER"))
        {
          if (((frmViewer) this.arrViewer[0]).PartInSelectionList(sPart))
          {
            string s = "";
            foreach (DataGridViewRow row in (IEnumerable) this.GetSelectionListGrid().Rows)
            {
              if (row.Cells["PARTNUMBER"].Value.ToString() == sPart)
                s = row.Cells["QTY"].Value.ToString();
            }
            sQuantity = string.Concat((object) (int.Parse(s) + int.Parse(sQuantity)));
            this.ChangeQuantity(sPart, sQuantity);
            return sQuantity;
          }
          dataGridViewRow.Cells["PART_SLIST_KEY"].Value = (object) sPart;
          dataGridViewRow.Cells[index].Value = (object) sPart;
        }
        else
          dataGridViewRow.Cells[index].Value = !dataGridView.Columns[index].Name.ToUpper().Equals("QTY") ? (object) string.Empty : (object) sQuantity;
      }
      dataGridView.Rows.Add(dataGridViewRow);
      dataGridView.EndEdit();
      this.AddNewRecord(dataGridView.Rows[0]);
      return sQuantity;
    }

    public string GetCurrentPageInfo(int i)
    {
      if (((frmViewer) this.arrViewer[i]).CurrentTreeView.InvokeRequired)
        ((frmViewer) this.arrViewer[i]).CurrentTreeView.Invoke((Delegate) new Dashbord.GetCurrentPageInfoDelegate(this.GetCurrentPageInfo), (object) i);
      else
        this.strPageInfo = ((frmViewer) this.arrViewer[i]).CurrentTreeView.SelectedNode.Tag.ToString();
      return this.strPageInfo;
    }

    public string GetCurrentPageID(ArrayList tempArray, int i)
    {
      if (((frmViewer) tempArray[i]).CurrentTreeView.InvokeRequired)
        ((frmViewer) tempArray[i]).CurrentTreeView.Invoke((Delegate) new Dashbord.GetCurrentPageIDDelegate(this.GetCurrentPageID), (object) tempArray, (object) i);
      else
        this.strPageInfo = ((frmViewer) tempArray[i]).CurrentTreeView.SelectedNode.Index.ToString();
      return this.strPageInfo;
    }

    public DataGridView GetSelectionListGrid()
    {
      if (((frmViewer) this.arrViewer[0]).objFrmSelectionlist.InvokeRequired)
        ((frmViewer) this.arrViewer[0]).objFrmSelectionlist.Invoke((Delegate) new Dashbord.GetSelectionListGridDelegate(this.GetSelectionListGrid), new object[0]);
      else
        ((frmViewer) this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
      return ((frmViewer) this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
    }

    public string GetCurrentTVNode(ArrayList tempArray, int i)
    {
      if (((frmViewer) tempArray[i]).CurrentTreeView.InvokeRequired)
        ((frmViewer) tempArray[i]).CurrentTreeView.Invoke((Delegate) new Dashbord.GetCurrentTVNodeDelegate(this.GetCurrentTVNode), (object) tempArray, (object) i);
      else
        this.strPageInfo = ((frmViewer) tempArray[i]).CurrentTreeView.SelectedNode.Text.ToString();
      return this.strPageInfo;
    }

    private int GetNodeRecursive(TreeNode treeNode, string name)
    {
      if (treeNode.Text.ToUpper().Equals(name.ToUpper()))
        return treeNode.Index;
      foreach (TreeNode node in treeNode.Nodes)
      {
        ++this.nodeid;
        if (this.GetNodeRecursive(node, name) != -1)
          return this.nodeid;
      }
      return -1;
    }

    private int PageIDRecursive(TreeView treeView, string name)
    {
      this.nodeid = 0;
      foreach (TreeNode node in treeView.Nodes)
      {
        ++this.nodeid;
        if (this.GetNodeRecursive(node, name) != -1)
          return this.nodeid;
      }
      return -1;
    }

    public delegate void FirstTimeDelegate(string[] args);

    public delegate void NextTimeDelegate(string[] args);

    public delegate string GetCurrentPageInfoDelegate(int i);

    public delegate string GetCurrentPageIDDelegate(ArrayList tempArray, int i);

    public delegate DataGridView GetSelectionListGridDelegate();

    public delegate string GetCurrentTVNodeDelegate(ArrayList tempArray, int i);
  }
}

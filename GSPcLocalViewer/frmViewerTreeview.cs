// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmViewerTreeview
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.frmPrint;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
  public class frmViewerTreeview : DockContent
  {
    public string sDataType = string.Empty;
    private const string dllZipper = "ZIPPER.dll";
    private IContainer components;
    private Panel pnlForm;
    public CustomTreeView tvBook;
    private PictureBox picLoading;
    private BackgroundWorker bgWorker;
    private ContextMenuStrip cMenuStripPrintRange;
    private ToolStripMenuItem FromMenu;
    private ToolStripMenuItem toMenu;
    private ContextMenuStrip cmsTreeview;
    private ToolStripMenuItem copyToClipboardToolStripMenuItem;
    private ToolStripMenuItem exportToFileToolStripMenuItem;
    private SaveFileDialog dlgSaveFile;
    private ToolStripMenuItem commaSeparatedToolStripMenuItem;
    private ToolStripMenuItem tabSeparatedToolStripMenuItem;
    private ToolStripMenuItem commaSeparatedToolStripMenuItem1;
    private ToolStripMenuItem tabSeparatedToolStripMenuItem1;
    private ToolStripMenuItem tsmiPrintThisContent;
    private frmViewer frmParent;
    private string statusText;
    private TreeNode lastTreeNode;
    public static string ToSelectedNode;
    private string selectedNodeText;
    private string selectedNodeTag;
    public string rangePrintAttId;
    public static string FromSelectedNode;
    private bool p_Encrypted;
    private bool p_Compressed;
    private XmlNode objXmlSchemaNode;
    private Download objDownloader;
    public bool isPDF;
    public bool isDJVU;
    public bool isTiff;
    private bool bMuliRageKey;
    private bool bMultiRange;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmViewerTreeview));
      this.pnlForm = new Panel();
      this.tvBook = new CustomTreeView();
      this.picLoading = new PictureBox();
      this.bgWorker = new BackgroundWorker();
      this.cMenuStripPrintRange = new ContextMenuStrip(this.components);
      this.FromMenu = new ToolStripMenuItem();
      this.toMenu = new ToolStripMenuItem();
      this.tsmiPrintThisContent = new ToolStripMenuItem();
      this.cmsTreeview = new ContextMenuStrip(this.components);
      this.copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
      this.commaSeparatedToolStripMenuItem = new ToolStripMenuItem();
      this.tabSeparatedToolStripMenuItem = new ToolStripMenuItem();
      this.exportToFileToolStripMenuItem = new ToolStripMenuItem();
      this.commaSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
      this.tabSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
      this.dlgSaveFile = new SaveFileDialog();
      this.pnlForm.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.cMenuStripPrintRange.SuspendLayout();
      this.cmsTreeview.SuspendLayout();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.tvBook);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(296, 456);
      this.pnlForm.TabIndex = 1;
      this.tvBook.BorderStyle = BorderStyle.None;
      this.tvBook.Dock = DockStyle.Fill;
      this.tvBook.DrawMode = TreeViewDrawMode.OwnerDrawText;
      this.tvBook.Location = new Point(0, 0);
      this.tvBook.Name = "tvBook";
      this.tvBook.Size = new Size(294, 454);
      this.tvBook.TabIndex = 0;
      this.tvBook.AfterSelect += new TreeViewEventHandler(this.tvBook_AfterSelect);
      this.tvBook.MouseDown += new MouseEventHandler(this.tvBook_MouseDown_1);
      this.tvBook.BeforeSelect += new TreeViewCancelEventHandler(this.tvBook_BeforeSelect);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) GSPcLocalViewer.Properties.Resources.Loading1;
      this.picLoading.Location = new Point(3, 3);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 18;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.cMenuStripPrintRange.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.FromMenu,
        (ToolStripItem) this.toMenu,
        (ToolStripItem) this.tsmiPrintThisContent
      });
      this.cMenuStripPrintRange.Name = "cMenuStripPrintRange";
      this.cMenuStripPrintRange.Size = new Size(166, 92);
      this.cMenuStripPrintRange.Opening += new CancelEventHandler(this.cMenuStripPrintRange_Opening);
      this.FromMenu.Name = "FromMenu";
      this.FromMenu.Size = new Size(165, 22);
      this.FromMenu.Text = "Print From";
      this.FromMenu.Click += new EventHandler(this.FromMenu_Click);
      this.toMenu.Name = "toMenu";
      this.toMenu.Size = new Size(165, 22);
      this.toMenu.Text = "Print To";
      this.toMenu.Click += new EventHandler(this.toMenu_Click_1);
      this.tsmiPrintThisContent.Name = "tsmiPrintThisContent";
      this.tsmiPrintThisContent.Size = new Size(165, 22);
      this.tsmiPrintThisContent.Text = "Print This Contents";
      this.tsmiPrintThisContent.Click += new EventHandler(this.coToolStripMenuItem_Click);
      this.cmsTreeview.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.copyToClipboardToolStripMenuItem,
        (ToolStripItem) this.exportToFileToolStripMenuItem
      });
      this.cmsTreeview.Name = "cmsPartslist";
      this.cmsTreeview.Size = new Size(163, 48);
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
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(296, 456);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.HideOnClose = true;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmViewerTreeview);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Contents";
      this.VisibleChanged += new EventHandler(this.frmViewerTreeview_VisibleChanged);
      this.pnlForm.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.cMenuStripPrintRange.ResumeLayout(false);
      this.cmsTreeview.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public string From
    {
      get
      {
        return frmViewerTreeview.FromSelectedNode;
      }
      set
      {
        frmViewerTreeview.FromSelectedNode = value;
      }
    }

    public string To
    {
      get
      {
        return frmViewerTreeview.ToSelectedNode;
      }
      set
      {
        frmViewerTreeview.ToSelectedNode = value;
      }
    }

    public frmViewerTreeview(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.lastTreeNode = (TreeNode) null;
      this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
      this.objDownloader = new Download(this.frmParent);
      this.UpdateFont();
      this.LoadResources();
      this.isPDF = false;
      this.isDJVU = false;
      this.isTiff = false;
    }

    private void ReadINI()
    {
      try
      {
        string str = Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.p_ServerId].sIniKey + ".ini";
        ArrayList arrayList = new ArrayList();
        ArrayList keys = new IniFileIO().GetKeys(str, "PRINTER_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          string keyValue = new IniFileIO().GetKeyValue("PRINTER_SETTINGS", keys[index].ToString(), str);
          if (keys[index].ToString() == "PAGE_SPECIFED_ACTION")
          {
            this.bMuliRageKey = true;
            if (keyValue.ToString().ToUpper() == "MULTI")
            {
              this.bMultiRange = true;
              break;
            }
            if (!(keyValue.ToString().ToUpper() == "SINGLE"))
              break;
            this.bMultiRange = false;
            break;
          }
          this.bMuliRageKey = false;
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void HideMenuStripItem()
    {
      this.cMenuStripPrintRange.Items["FromMenu"].Visible = false;
    }

    private void FromMenu_Click(object sender, EventArgs e)
    {
      frmViewerTreeview.FromSelectedNode = this.selectedNodeText;
      string sFromIndex = "1";
      try
      {
        sFromIndex = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(this.selectedNodeTag))).Attributes[this.rangePrintAttId].Value.ToString();
      }
      catch
      {
      }
      foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
      {
        if (this.bMuliRageKey && this.bMultiRange)
        {
          if (openForm.GetType() == typeof (frmPageSpecified))
          {
            ((frmPageSpecified) openForm).UpdateFromGridColumn(this.selectedNodeText, sFromIndex);
            break;
          }
        }
        else if (openForm.GetType() == typeof (GSPcLocalViewer.frmPrint.frmPrint) && ((GSPcLocalViewer.frmPrint.frmPrint) openForm).printRangePages)
        {
          ((GSPcLocalViewer.frmPrint.frmPrint) openForm).UpdateFrom(this.selectedNodeText, sFromIndex);
          break;
        }
      }
    }

    private void toMenu_Click_1(object sender, EventArgs e)
    {
      frmViewerTreeview.ToSelectedNode = this.selectedNodeText;
      string sToIndex = "1";
      try
      {
        sToIndex = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(this.selectedNodeTag))).Attributes[this.rangePrintAttId].Value.ToString();
      }
      catch
      {
      }
      foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
      {
        if (this.bMuliRageKey && this.bMultiRange)
        {
          if (openForm.GetType() == typeof (frmPageSpecified))
          {
            ((frmPageSpecified) openForm).UpdateToGridCol(this.selectedNodeText, sToIndex);
            break;
          }
        }
        else if (openForm.GetType() == typeof (GSPcLocalViewer.frmPrint.frmPrint) && ((GSPcLocalViewer.frmPrint.frmPrint) openForm).printRangePages)
        {
          ((GSPcLocalViewer.frmPrint.frmPrint) openForm).UpdateTo(this.selectedNodeText, sToIndex);
          break;
        }
      }
    }

    private void coToolStripMenuItem_Click(object sender, EventArgs e)
    {
      frmViewerTreeview.ToSelectedNode = this.selectedNodeText;
      string sPrintThisIndex = "1";
      try
      {
        sPrintThisIndex = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(this.selectedNodeTag))).Attributes[this.rangePrintAttId].Value.ToString();
      }
      catch
      {
      }
      foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
      {
        if (this.bMuliRageKey && this.bMultiRange && openForm.GetType() == typeof (frmPageSpecified))
        {
          ((frmPageSpecified) openForm).UpdatePrintThisGridCol(this.selectedNodeText, sPrintThisIndex);
          break;
        }
      }
    }

    private void commaSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, ",");
      Clipboard.SetText(empty);
    }

    private void tabSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
    {
      string empty = string.Empty;
      this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, "\t");
      Clipboard.SetText(empty);
    }

    private void commaSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
      this.dlgSaveFile.RestoreDirectory = true;
      string empty = string.Empty;
      this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, ",");
      if (!(empty != string.Empty))
        return;
      if (this.dlgSaveFile.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        StreamWriter text = File.CreateText(this.dlgSaveFile.FileName);
        text.Write(empty);
        text.Close();
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.POPUP_MESSAGE));
      }
    }

    private void tabSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
      this.dlgSaveFile.RestoreDirectory = true;
      string empty = string.Empty;
      this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, "\t");
      if (!(empty != string.Empty))
        return;
      if (this.dlgSaveFile.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        StreamWriter text = File.CreateText(this.dlgSaveFile.FileName);
        text.Write(empty);
        text.Close();
      }
      catch
      {
        MessageHandler.ShowWarning(this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.POPUP_MESSAGE));
      }
    }

    private void tvBook_BeforeSelect(object sender, TreeViewCancelEventArgs e)
    {
      if (!this.frmParent.objFrmPartlist.isWorking && !this.frmParent.objFrmPicture.isWorking)
        return;
      e.Cancel = true;
    }

    private void tvBook_MouseDown_1(object sender, MouseEventArgs e)
    {
      bool flag1 = false;
      try
      {
        string str = Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.p_ServerId].sIniKey + ".ini";
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        ArrayList keys = new IniFileIO().GetKeys(str, "PRINTER_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          string keyValue = new IniFileIO().GetKeyValue("PRINTER_SETTINGS", keys[index].ToString(), str);
          if (keys[index].ToString() == "PAGE_SPECIFED_ACTION")
          {
            if (keyValue.ToString().ToUpper() == "MULTI")
              flag1 = true;
            else if (keyValue.ToString().ToUpper() == "SINGLE")
              flag1 = false;
          }
        }
      }
      catch (Exception ex)
      {
      }
      try
      {
        if (e.Button != MouseButtons.Right)
          return;
        bool flag2 = false;
        foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
        {
          if (openForm.GetType() == typeof (GSPcLocalViewer.frmPrint.frmPrint) && ((GSPcLocalViewer.frmPrint.frmPrint) openForm).printRangePages)
          {
            flag2 = true;
            break;
          }
        }
        if (flag2)
        {
          foreach (Form openForm in (ReadOnlyCollectionBase) Application.OpenForms)
          {
            if (openForm.GetType() == typeof (frmPageSpecified))
              break;
          }
          if (flag1)
            this.cMenuStripPrintRange.Items[2].Visible = true;
          else
            this.cMenuStripPrintRange.Items[2].Visible = false;
          this.tvBook.ContextMenuStrip = this.cMenuStripPrintRange;
          this.selectedNodeText = this.tvBook.HitTest(e.X, e.Y).Node.Text;
          this.selectedNodeTag = this.tvBook.HitTest(e.X, e.Y).Node.Tag.ToString();
        }
        else
          this.tvBook.ContextMenuStrip = this.cmsTreeview;
      }
      catch
      {
      }
    }

    private void tvBook_AfterSelect(object sender, TreeViewEventArgs e)
    {
      this.LoadPictureInTree();
      this.frmParent.LoadMemos();
    }

    private void frmViewerTreeview_VisibleChanged(object sender, EventArgs e)
    {
      this.frmParent.contentsToolStripMenuItem.Checked = this.Visible;
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.frmParent.bObjFrmTreeviewClosed = false;
      string surl1 = string.Empty;
      string str = string.Empty;
      try
      {
        surl1 = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
        if (!surl1.EndsWith("/"))
          surl1 += "/";
        str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
        str = str + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey;
        str = str + "\\" + this.frmParent.BookPublishingId;
        if (!Directory.Exists(str))
          Directory.CreateDirectory(str);
        if (this.p_Compressed)
        {
          surl1 = surl1 + this.frmParent.BookPublishingId + "/" + this.frmParent.BookPublishingId + ".zip";
          str = str + "\\" + this.frmParent.BookPublishingId + ".zip";
        }
        else
        {
          surl1 = surl1 + this.frmParent.BookPublishingId + "/" + this.frmParent.BookPublishingId + ".xml";
          str = str + "\\" + this.frmParent.BookPublishingId + ".xml";
        }
      }
      catch
      {
        MessageHandler.ShowError(this.GetResource("(E-VTV-EM004) Failed to create file/folder specified", "(E-VTV-EM004)_FAILED", ResourceType.POPUP_MESSAGE));
      }
      this.TreeViewVisible(false);
      this.TreeViewClearNodes();
      int result = 0;
      if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
        result = 0;
      bool flag = false;
      if (File.Exists(str))
      {
        if (result == 0)
          flag = true;
        else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str, this.p_Compressed, this.p_Encrypted), Global.GetServerUpdateDateFromXmlNode(this.frmParent.SchemaNode, this.frmParent.BookNode), result))
          flag = true;
      }
      else
        flag = true;
      if (flag)
        this.objDownloader.DownloadFile(surl1, str);
      if (File.Exists(str))
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = "Loading " + this.frmParent.BookPublishingId + ".xml";
        this.UpdateStatus();
        if (this.LoadBookInTree(str))
        {
          this.statusText = this.frmParent.BookPublishingId + " " + this.GetResource("Finished loading", "FINISHED_LOADING", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
        else
        {
          this.statusText = this.frmParent.BookPublishingId + this.GetResource("(E-VTV-EM005) Failed to load specified object", "(E-VTV-EM005)_FAILED", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
      }
      else
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.frmParent.BookPublishingId + this.GetResource("(E-VTV-EM006) Failed to download specified object", "(E-VTV-EM006)_FAILED", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        MessageHandler.ShowWarning(this.frmParent.BookPublishingId + this.GetResource("(E-VTV-EM005) Failed to load specified object", "(E-VTV-EM005)_FAILED", ResourceType.POPUP_MESSAGE));
        this.frmParent.LoadDataFromNode(this.frmParent.objHistory.Current());
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.frmParent.bObjFrmTreeviewClosed = true;
      this.frmParent.LoadBookmarks();
      this.frmParent.LoadMemos();
      this.HideLoading(this.pnlForm);
      this.frmParent.SelectTreeNode();
      this.TreeViewVisible(true);
      this.frmParent.SelListInitialize();
      this.ReadINI();
    }

    private void GetDataTreeViewText(TreeNodeCollection nodes, ref string str, string delimiter)
    {
      for (int index1 = 0; index1 < nodes.Count; ++index1)
      {
        if (str != string.Empty)
          str += "\r\n";
        for (int index2 = 0; index2 < nodes[index1].Level; ++index2)
          str += delimiter;
        str += nodes[index1].Text.Replace("&&", "&");
        if (nodes[index1].Nodes.Count > 0)
          this.GetDataTreeViewText(nodes[index1].Nodes, ref str, delimiter);
      }
    }

    public void UpdateFont()
    {
      this.tvBook.Font = Settings.Default.appFont;
    }

    public void LoadBook()
    {
      if (!Global.SecurityLocksOpen(this.frmParent.BookNode, this.frmParent.SchemaNode, this.frmParent.ServerId, this.frmParent))
      {
        this.frmParent.Close();
      }
      else
      {
        this.p_Encrypted = Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON";
        if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null)
        {
          if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
          {
            this.p_Compressed = true;
            this.frmParent.CompressedDownload = true;
          }
          else
          {
            this.p_Compressed = false;
            this.frmParent.CompressedDownload = false;
          }
        }
        else
        {
          this.p_Compressed = false;
          this.frmParent.CompressedDownload = false;
        }
        this.statusText = this.GetResource("Downloading", "DOWNLOADING", ResourceType.STATUS_MESSAGE) + " " + this.frmParent.BookPublishingId + ".xml";
        this.UpdateStatus();
        this.frmParent.ClearBookmarks();
        this.bgWorker.RunWorkerAsync();
      }
    }

    public void SelectTreeNodeByPageName(string pageName)
    {
      if (pageName == string.Empty)
      {
        if (this.tvBook.Nodes.Count <= 0)
          return;
        this.tvBook.SelectedNode = this.tvBook.Nodes[0];
      }
      else
      {
        TreeNode treeNodeByPageName = this.FindTreeNodeByPageName(this.tvBook.Nodes, pageName);
        if (treeNodeByPageName != null)
        {
          this.tvBook.SelectedNode = treeNodeByPageName;
        }
        else
        {
          if (this.tvBook.Nodes.Count <= 0)
            return;
          this.tvBook.SelectedNode = this.tvBook.Nodes[0];
        }
      }
    }

    public void SelectTreeNode(string pageId)
    {
      if (pageId == string.Empty)
      {
        if (this.tvBook.Nodes.Count <= 0)
          return;
        this.TVBookSelectFirstNode();
      }
      else
      {
        XmlDocument xmlDocument = new XmlDocument();
        string attIdElement = "";
        XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(this.tvBook.Tag.ToString()));
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlDocument.ReadNode((XmlReader) xmlTextReader).Attributes)
        {
          if (attribute.Value.ToUpper().Equals("ID"))
            attIdElement = attribute.Name;
        }
        if (attIdElement == "")
        {
          if (this.tvBook.Nodes.Count <= 0)
            return;
          this.tvBook.SelectedNode = this.tvBook.Nodes[0];
        }
        else
        {
          TreeNode treeNode = this.FindTreeNode(this.tvBook.Nodes, attIdElement, pageId);
          if (treeNode != null)
          {
            this.TVBookGotoPage(treeNode);
          }
          else
          {
            if (this.tvBook.Nodes.Count <= 0 || treeNode == null)
              return;
            this.TVBookGotoPage(treeNode);
          }
        }
      }
    }

    public TreeNode FindTreeNodeByPageName(TreeNodeCollection nodes, string pageName)
    {
      XmlDocument xmlDocument = new XmlDocument();
      foreach (TreeNode node in nodes)
      {
        if (node.Text.ToUpper() == pageName.ToUpper())
          return node;
        TreeNode treeNodeByPageName = this.FindTreeNodeByPageName(node.Nodes, pageName);
        if (treeNodeByPageName != null)
          return treeNodeByPageName;
      }
      return (TreeNode) null;
    }

    private TreeNode FindTreeNode(TreeNodeCollection nodes, string attIdElement, string id)
    {
      XmlDocument xmlDocument = new XmlDocument();
      foreach (TreeNode node in nodes)
      {
        XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(node.Tag.ToString()));
        if (xmlDocument.ReadNode((XmlReader) xmlTextReader).Attributes[attIdElement].Value.ToUpper() == id.ToUpper())
          return node;
        TreeNode treeNode = this.FindTreeNode(node.Nodes, attIdElement, id);
        if (treeNode != null)
          return treeNode;
      }
      return (TreeNode) null;
    }

    private bool LoadBookInTree(string sFilePath)
    {
      XmlDocument xmlDocument = new XmlDocument();
      this.frmParent.lstFilteredPages = new List<string>();
      if (!File.Exists(sFilePath))
        return false;
      if (this.p_Compressed)
      {
        try
        {
          Global.Unzip(sFilePath);
          xmlDocument.Load(sFilePath.ToLower().Replace(".zip", ".xml"));
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
      if (this.p_Encrypted)
      {
        try
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        catch
        {
          return false;
        }
      }
      this.objXmlSchemaNode = xmlDocument.SelectSingleNode("//Schema");
      if (this.objXmlSchemaNode == null)
        return false;
      string sIdElement = string.Empty;
      string sDisplayElement = string.Empty;
      string PicType = string.Empty;
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.objXmlSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
        {
          sIdElement = attribute.Name;
          this.rangePrintAttId = attribute.Name;
        }
        else if (attribute.Value.ToUpper().Equals("PAGENAME"))
          sDisplayElement = attribute.Name;
        else if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
          PicType = attribute.Name;
      }
      if (sIdElement == string.Empty || sDisplayElement == string.Empty)
        return false;
      this.tvBook.Tag = (object) this.objXmlSchemaNode.OuterXml;
      foreach (XmlNode selectNode in xmlDocument.SelectNodes("//Book/Pg"))
        this.AddNode((TreeNode) null, this.objXmlSchemaNode, selectNode, sDisplayElement, sIdElement, PicType);
      if (this.tvBook.Nodes.Count > 0)
        this.FindLastTreeNode(this.tvBook.Nodes[this.tvBook.Nodes.Count - 1]);
      try
      {
        this.frmParent.sFirstPageTitle = this.tvBook.Nodes[0].Text;
        this.frmParent.UpdateViewerTitle();
      }
      catch
      {
      }
      if (Settings.Default.ExpandAllContents)
      {
        this.TreeViewExpandAllNodes();
      }
      else
      {
        try
        {
          foreach (TreeNode node in this.tvBook.Nodes)
            this.ExpandTreeNode(node, Settings.Default.ExpandContentsLevel - 1);
        }
        catch
        {
        }
      }
      return true;
    }

    private void ExpandTreeNode(TreeNode tNode, int iExpandLevel)
    {
      try
      {
        if (this.tvBook.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerTreeview.ExpandTreeNodeDelegate(this.ExpandTreeNode), (object) tNode, (object) iExpandLevel);
        }
        else
        {
          try
          {
            if (tNode.Nodes.Count >= 0 && tNode.Level < iExpandLevel)
            {
              foreach (TreeNode node in tNode.Nodes)
                this.ExpandTreeNode(node, iExpandLevel);
            }
            if (tNode.Level >= iExpandLevel)
              return;
            tNode.Expand();
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

    private void AddNode(TreeNode tNode, XmlNode xNodeSchama, XmlNode xNode, string sDisplayElement, string sIdElement, string PicType)
    {
      string str = xNode.Attributes[sDisplayElement].Value;
      xNode = this.frmParent.FilterPage(xNodeSchama, xNode);
      if (xNode.Attributes.Count == 0)
      {
        this.frmParent.lstFilteredPages.Add(str);
      }
      else
      {
        TreeNode tnChild = new TreeNode();
        tnChild.Text = xNode.Attributes[sDisplayElement].Value.Replace("&", "&&");
        xNode.SelectSingleNode("//Pic");
        tnChild.Tag = xNode.OuterXml.ToUpper().IndexOf("<PG", 3) <= 0 ? (object) xNode.OuterXml : (object) (xNode.OuterXml.Substring(0, xNode.OuterXml.IndexOf("<Pg", 3)) + "</Pg>");
        this.TreeViewAddNode(tNode, tnChild);
        tNode = tnChild;
        if (!xNode.HasChildNodes)
          return;
        foreach (XmlNode childNode in xNode.ChildNodes)
        {
          if (childNode.Name.ToUpper().Equals("PG"))
          {
            XmlAttributeCollection attributes = xNode.Attributes;
            this.AddNode(tNode, xNodeSchama, childNode, sDisplayElement, sIdElement, PicType);
          }
        }
      }
    }

    public void LoadPictureInTree()
    {
      XmlDocument xmlDocument = new XmlDocument();
      this.frmParent.EnableAddPicMemoTSB(false);
      this.frmParent.EnableAddPartMemoMenu(false);
      this.frmParent.objFrmPicture.EnableAddPicMemoTSB(false);
      this.EnableDisableNavigationItems();
      try
      {
        XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(this.tvBook.Tag.ToString()));
        XmlNode schemaNode = xmlDocument.ReadNode((XmlReader) xmlTextReader1);
        XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(this.tvBook.SelectedNode.Tag.ToString()));
        XmlNode pageNode = xmlDocument.ReadNode((XmlReader) xmlTextReader2);
        if (!this.frmParent.objFrmInfo.IsDisposed)
          this.frmParent.objFrmInfo.LoadData(schemaNode, pageNode);
        this.frmParent.LoadPicture(schemaNode, pageNode);
      }
      catch (Exception ex)
      {
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerTreeview.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = false;
          }
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

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerTreeview.HideLoadingDelegate(this.HideLoading), (object) this.pnlForm);
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

    private void UpdateStatus()
    {
      if (this.frmParent.InvokeRequired)
        this.frmParent.Invoke((Delegate) new frmViewerTreeview.StatusDelegate(this.frmParent.UpdateStatus), (object) this.statusText);
      else
        this.frmParent.UpdateStatus(this.statusText);
    }

    public void TreeViewClearSelection()
    {
      if (this.tvBook.InvokeRequired)
        this.tvBook.Invoke((Delegate) new frmViewerTreeview.TreeViewClearSelectionDelegate(this.TreeViewClearSelection));
      else
        this.tvBook.SelectedNode = (TreeNode) null;
    }

    private void TreeViewClearNodes()
    {
      if (this.tvBook.InvokeRequired)
        this.tvBook.Invoke((Delegate) new frmViewerTreeview.TreeViewClearNodesDelegate(this.TreeViewClearNodes));
      else
        this.tvBook.Nodes.Clear();
    }

    private void TreeViewExpandAllNodes()
    {
      if (this.tvBook.InvokeRequired)
        this.tvBook.Invoke((Delegate) new frmViewerTreeview.TreeViewExpandAllNodesDelegate(this.TreeViewExpandAllNodes));
      else
        this.tvBook.ExpandAll();
    }

    private void TreeViewAddNode(TreeNode tnParent, TreeNode tnChild)
    {
      if (this.tvBook.InvokeRequired)
        this.tvBook.Invoke((Delegate) new frmViewerTreeview.TreeViewAddNodeDelegate(this.TreeViewAddNode), (object) tnParent, (object) tnChild);
      else if (tnParent == null)
        this.tvBook.Nodes.Add(tnChild);
      else
        tnParent.Nodes.Add(tnChild);
    }

    private void TreeViewVisible(bool visible)
    {
      if (this.tvBook.InvokeRequired)
        this.tvBook.Invoke((Delegate) new frmViewerTreeview.TreeViewVisibleDelegate(this.TreeViewVisible), (object) visible);
      else
        this.tvBook.Visible = visible;
    }

    public void ShowLoading()
    {
      this.ShowLoading(this.pnlForm);
    }

    public void GetPagesToDownload(ref ArrayList arrayPages, string sLocalPath, bool bCompressed)
    {
      XmlNode xmlNode = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(this.tvBook.Tag.ToString())));
      if (xmlNode == null)
        return;
      string attPicElement = string.Empty;
      string attListElement = string.Empty;
      string str = string.Empty;
      string attUpdateDatePICElement = string.Empty;
      string attUpdateDatePLElement = string.Empty;
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode.Attributes)
      {
        if (attribute.Value.ToUpper() == "PICTUREFILE")
          attPicElement = attribute.Name;
        if (attribute.Value.ToUpper() == "PARTSLISTFILE")
          attListElement = attribute.Name;
        if (attribute.Value.ToUpper() == "UPDATEDATE")
          str = attribute.Name;
        if (attribute.Value.ToUpper() == "UPDATEDATEPIC")
          attUpdateDatePICElement = attribute.Name;
        if (attribute.Value.ToUpper() == "UPDATEDATEPL")
          attUpdateDatePLElement = attribute.Name;
      }
      if (attUpdateDatePICElement == string.Empty)
        attUpdateDatePICElement = str;
      if (attUpdateDatePLElement == string.Empty)
        attUpdateDatePLElement = str;
      for (int index = 0; index < this.tvBook.Nodes.Count; ++index)
        this.GetPagesToDownloadRec(ref arrayPages, this.tvBook.Nodes[index], sLocalPath, attPicElement, attListElement, attUpdateDatePICElement, attUpdateDatePLElement, bCompressed);
    }

    public void GetPagesToDownloadRec(ref ArrayList arrayPages, TreeNode objTreeNode, string sLocalPath, string attPicElement, string attListElement, string attUpdateDatePICElement, string attUpdateDatePLElement, bool bCompressed)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(objTreeNode.Tag.ToString());
      XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Pic");
      int result = 0;
      if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
        result = 0;
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        if (xmlNode.Attributes[attPicElement] != null && xmlNode.Attributes[attUpdateDatePICElement] != null)
        {
          string str = xmlNode.Attributes[attPicElement].Value;
          if (!str.ToUpper().EndsWith(".DJVU") && !str.ToUpper().EndsWith(".PDF") && !str.ToUpper().EndsWith(".TIF"))
            str += ".djvu";
          string s = xmlNode.Attributes[attUpdateDatePICElement].Value;
          DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) new CultureInfo("en-GB"));
          bool flag = false;
          if (str.Trim().ToLower() != ".djvu")
          {
            if (File.Exists(sLocalPath + "/" + str))
            {
              if (result == 0)
                flag = true;
              else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(sLocalPath + "/" + str, this.p_Compressed, this.p_Encrypted), exact, result))
                flag = true;
            }
            else
              flag = true;
            if (!arrayPages.Contains((object) (str + "|*|*|" + s)) && flag)
              arrayPages.Add((object) (str + "|*|*|" + s));
          }
        }
        if (xmlNode.Attributes[attListElement] != null && xmlNode.Attributes[attUpdateDatePLElement] != null)
        {
          string str = xmlNode.Attributes[attListElement].Value;
          if (this.frmParent.CompressedDownload)
          {
            if (!str.ToUpper().EndsWith(".XML") && !str.ToUpper().EndsWith(".ZIP"))
              str += ".zip";
            else if (str.ToUpper().EndsWith(".XML"))
              str = str.Replace(".xml", ".zip");
          }
          else if (!str.ToUpper().EndsWith(".XML"))
            str += ".xml";
          string s = xmlNode.Attributes[attUpdateDatePLElement].Value;
          DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) new CultureInfo("en-GB"));
          bool flag = false;
          if (File.Exists(sLocalPath + "/" + str))
          {
            if (result == 0)
              flag = true;
            else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(sLocalPath + "/" + str, this.p_Compressed, this.p_Encrypted), exact, result))
              flag = true;
          }
          else
            flag = true;
          if (!arrayPages.Contains((object) str) && flag)
            arrayPages.Add((object) (str + "|*|*|" + s));
        }
      }
      if (objTreeNode.Nodes.Count == 0)
        return;
      for (int index = 0; index < objTreeNode.Nodes.Count; ++index)
        this.GetPagesToDownloadRec(ref arrayPages, objTreeNode.Nodes[index], sLocalPath, attPicElement, attListElement, attUpdateDatePICElement, attUpdateDatePLElement, bCompressed);
    }

    private void EnableDisableNavigationItems()
    {
      bool bFirst;
      bool bPrev;
      if (this.tvBook.SelectedNode == this.tvBook.Nodes[0])
      {
        bFirst = false;
        bPrev = false;
      }
      else
      {
        bFirst = true;
        bPrev = true;
      }
      bool bLast;
      bool bNext;
      if (this.tvBook.SelectedNode == this.lastTreeNode)
      {
        bLast = false;
        bNext = false;
      }
      else
      {
        bLast = true;
        bNext = true;
      }
      this.frmParent.EnableNavigationItems(bFirst, bPrev, bNext, bLast);
    }

    private void FindLastTreeNode(TreeNode treeNode)
    {
      if (treeNode.Nodes.Count > 0)
        this.FindLastTreeNode(treeNode.Nodes[treeNode.Nodes.Count - 1]);
      else
        this.lastTreeNode = treeNode;
    }

    public void SelectFirstNode()
    {
      if (this.tvBook.Nodes.Count <= 0)
        return;
      this.TVBookSelectFirstNode();
    }

    public void SelectPreviousNode()
    {
      try
      {
        this.TVBookSelectPreviousNode();
      }
      catch
      {
      }
    }

    public void SelectNextNode()
    {
      try
      {
        this.TVBookSelectNextNode();
      }
      catch
      {
      }
    }

    public void SelectLastNode()
    {
      this.TVBookSelectLastNode();
    }

    public CustomTreeView CurrentTreeView
    {
      get
      {
        return this.tvBook;
      }
    }

    public XmlNode PageSchemaNode
    {
      get
      {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(this.tvBook.Tag.ToString()));
          return xmlDocument.ReadNode((XmlReader) xmlTextReader);
        }
        catch
        {
          return (XmlNode) null;
        }
      }
    }

    public XmlNode PageNode
    {
      get
      {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(this.tvBook.SelectedNode.Tag.ToString()));
          return xmlDocument.ReadNode((XmlReader) xmlTextReader).FirstChild;
        }
        catch
        {
          return (XmlNode) null;
        }
      }
    }

    public XmlNode PicNode
    {
      get
      {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          string str = string.Empty;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.PageSchemaNode.Attributes)
          {
            if (attribute.Value.ToUpper() == "PICTUREFILE")
            {
              str = attribute.Name;
              break;
            }
          }
          XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(this.tvBook.SelectedNode.Tag.ToString()));
          XmlNode xmlNode = xmlDocument.ReadNode((XmlReader) xmlTextReader);
          string currentPicName = this.frmParent.objFrmPicture.CurrentPicName;
          return xmlNode.SelectSingleNode("//Pic[@" + str + "='" + currentPicName + "']");
        }
        catch
        {
          return (XmlNode) null;
        }
      }
    }

    public string CurrentNodeText
    {
      get
      {
        try
        {
          return this.tvBook.SelectedNode.Text.Trim();
        }
        catch
        {
          return string.Empty;
        }
      }
    }

    public void LoadResources()
    {
      this.Text = this.GetResource("Contents", "CONTENTS", ResourceType.TITLE) + "      ";
      this.FromMenu.Text = this.GetResource("Print From", "PRINT_FROM", ResourceType.CONTEXT_MENU);
      this.toMenu.Text = this.GetResource("Print To", "PRINT_TO", ResourceType.CONTEXT_MENU);
      this.tsmiPrintThisContent.Text = this.GetResource("Print This Contents", "PRINT_THIS_CONTENTS", ResourceType.CONTEXT_MENU);
      this.tabSeparatedToolStripMenuItem.Text = this.GetResource("Tab Separated", "TAB_SEPARATED", ResourceType.CONTEXT_MENU);
      this.tabSeparatedToolStripMenuItem1.Text = this.GetResource("Tab Separated", "TAB_SEPARATED_FILE", ResourceType.CONTEXT_MENU);
      this.copyToClipboardToolStripMenuItem.Text = this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.CONTEXT_MENU);
      this.commaSeparatedToolStripMenuItem.Text = this.GetResource("Comma Separated", "COMMA_SEPARATED", ResourceType.CONTEXT_MENU);
      this.commaSeparatedToolStripMenuItem1.Text = this.GetResource("Comma Separated", "COMMA_SEPARATED_FILE", ResourceType.CONTEXT_MENU);
      this.exportToFileToolStripMenuItem.Text = this.GetResource("Export To File", "EXPORT_TO_FILE", ResourceType.CONTEXT_MENU);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string xQuery1 = "" + "/Screen[@Name='MAIN_FORM']" + "/Screen[@Name='CONTENTS']";
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

    public int GetNodesCount()
    {
      return this.tvBook.Nodes.Count;
    }

    private void TVBookSelectFirstNode()
    {
      try
      {
        if (this.tvBook.InvokeRequired)
          this.tvBook.Invoke((Delegate) new frmViewerTreeview.TVBookSelectFirstNodeDeligate(this.TVBookSelectFirstNode), new object[0]);
        else
          this.tvBook.SelectedNode = this.tvBook.Nodes[0];
      }
      catch
      {
      }
    }

    private void TVBookSelectLastNode()
    {
      try
      {
        if (this.tvBook.InvokeRequired)
          this.tvBook.Invoke((Delegate) new frmViewerTreeview.TVBookSelectLastNodeDeligate(this.TVBookSelectLastNode), new object[0]);
        else
          this.tvBook.SelectedNode = this.lastTreeNode;
      }
      catch
      {
      }
    }

    private void TVBookSelectPreviousNode()
    {
      try
      {
        if (this.tvBook.InvokeRequired)
        {
          this.tvBook.Invoke((Delegate) new frmViewerTreeview.TVBookSelectPreviousNodeDeligate(this.TVBookSelectPreviousNode), new object[0]);
        }
        else
        {
          try
          {
            if (this.tvBook.SelectedNode == this.tvBook.Nodes[0] || this.tvBook.SelectedNode.PrevVisibleNode == null)
              return;
            this.tvBook.SelectedNode = this.tvBook.SelectedNode.PrevVisibleNode;
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

    private void TVBookSelectNextNode()
    {
      try
      {
        if (this.tvBook.InvokeRequired)
        {
          this.tvBook.Invoke((Delegate) new frmViewerTreeview.TVBookSelectNextNodeDeligate(this.TVBookSelectNextNode), new object[0]);
        }
        else
        {
          try
          {
            if (this.tvBook.SelectedNode == this.lastTreeNode || this.tvBook.SelectedNode.NextVisibleNode == null)
              return;
            this.tvBook.SelectedNode = this.tvBook.SelectedNode.NextVisibleNode;
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

    private void TVBookGotoPage(TreeNode pageID)
    {
      try
      {
        if (this.tvBook.InvokeRequired)
          this.tvBook.Invoke((Delegate) new frmViewerTreeview.TVBookGotoPageDeligate(this.TVBookGotoPage), (object) pageID);
        else
          this.tvBook.SelectedNode = pageID;
      }
      catch
      {
      }
    }

    private void cMenuStripPrintRange_Opening(object sender, CancelEventArgs e)
    {
    }

    private delegate void ExpandTreeNodeDelegate(TreeNode tNode, int iExpandLevel);

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void StatusDelegate(string status);

    private delegate void TreeViewClearSelectionDelegate();

    private delegate void TreeViewClearNodesDelegate();

    private delegate void TreeViewExpandAllNodesDelegate();

    private delegate void TreeViewAddNodeDelegate(TreeNode tnParent, TreeNode tnChild);

    private delegate void TreeViewVisibleDelegate(bool visible);

    private delegate void TVBookSelectFirstNodeDeligate();

    private delegate void TVBookSelectLastNodeDeligate();

    private delegate void TVBookSelectPreviousNodeDeligate();

    private delegate void TVBookSelectNextNodeDeligate();

    private delegate void TVBookGotoPageDeligate(TreeNode pageID);
  }
}

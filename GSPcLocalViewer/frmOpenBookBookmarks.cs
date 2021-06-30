// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOpenBookBookmarks
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmOpenBookBookmarks : Form
  {
    private IContainer components;
    private SplitContainer pnlForm;
    private Panel pnlDetails;
    private Panel pnlBookInfo;
    private Panel pnlrtbBookInfo;
    private RichTextBox rtbBookInfo;
    private Label lblBookmarksInfo;
    private Panel pnlServerInfo;
    private Panel pnlrtbServerInfo;
    private RichTextBox rtbServerInfo;
    private Label lblServerInfo;
    private Label lblDetails;
    private Panel pnltvBrowse;
    private CustomTreeView tvBrowse;
    private Label lblBrowse;
    private BackgroundWorker bgWorker;
    private PictureBox picLoading;
    private frmOpenBook frmParent;
    private string statusText;
    private int p_ServerId;
    private string p_BookId;
    private XmlNode p_BookNode;
    private XmlNode p_SchemaNode;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmOpenBookBookmarks));
      this.pnlForm = new SplitContainer();
      this.pnltvBrowse = new Panel();
      this.tvBrowse = new CustomTreeView();
      this.lblBrowse = new Label();
      this.pnlDetails = new Panel();
      this.pnlBookInfo = new Panel();
      this.pnlrtbBookInfo = new Panel();
      this.rtbBookInfo = new RichTextBox();
      this.lblBookmarksInfo = new Label();
      this.pnlServerInfo = new Panel();
      this.pnlrtbServerInfo = new Panel();
      this.rtbServerInfo = new RichTextBox();
      this.lblServerInfo = new Label();
      this.lblDetails = new Label();
      this.bgWorker = new BackgroundWorker();
      this.picLoading = new PictureBox();
      this.pnlForm.Panel1.SuspendLayout();
      this.pnlForm.Panel2.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.pnltvBrowse.SuspendLayout();
      this.pnlDetails.SuspendLayout();
      this.pnlBookInfo.SuspendLayout();
      this.pnlrtbBookInfo.SuspendLayout();
      this.pnlServerInfo.SuspendLayout();
      this.pnlrtbServerInfo.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Panel1.BackColor = SystemColors.Control;
      this.pnlForm.Panel1.Controls.Add((Control) this.pnltvBrowse);
      this.pnlForm.Panel1.Controls.Add((Control) this.lblBrowse);
      this.pnlForm.Panel1MinSize = 270;
      this.pnlForm.Panel2.Controls.Add((Control) this.pnlDetails);
      this.pnlForm.Panel2.Controls.Add((Control) this.lblDetails);
      this.pnlForm.Panel2MinSize = 75;
      this.pnlForm.Size = new Size(578, 409);
      this.pnlForm.SplitterDistance = 270;
      this.pnlForm.TabIndex = 10;
      this.pnltvBrowse.BackColor = Color.White;
      this.pnltvBrowse.Controls.Add((Control) this.tvBrowse);
      this.pnltvBrowse.Dock = DockStyle.Fill;
      this.pnltvBrowse.Location = new Point(0, 27);
      this.pnltvBrowse.Name = "pnltvBrowse";
      this.pnltvBrowse.Padding = new Padding(5, 15, 0, 0);
      this.pnltvBrowse.Size = new Size(268, 380);
      this.pnltvBrowse.TabIndex = 15;
      this.tvBrowse.BorderStyle = BorderStyle.None;
      this.tvBrowse.Cursor = Cursors.Default;
      this.tvBrowse.Dock = DockStyle.Fill;
      this.tvBrowse.DrawMode = TreeViewDrawMode.OwnerDrawText;
      this.tvBrowse.ForeColor = SystemColors.WindowText;
      this.tvBrowse.ItemHeight = 16;
      this.tvBrowse.Location = new Point(5, 15);
      this.tvBrowse.Name = "tvBrowse";
      this.tvBrowse.Size = new Size(263, 365);
      this.tvBrowse.TabIndex = 11;
      this.tvBrowse.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.tvBrowse_NodeMouseDoubleClick);
      this.tvBrowse.AfterSelect += new TreeViewEventHandler(this.tvBrowse_AfterSelect);
      this.lblBrowse.BackColor = Color.White;
      this.lblBrowse.Dock = DockStyle.Top;
      this.lblBrowse.ForeColor = Color.Black;
      this.lblBrowse.Location = new Point(0, 0);
      this.lblBrowse.Name = "lblBrowse";
      this.lblBrowse.Padding = new Padding(3, 7, 0, 0);
      this.lblBrowse.Size = new Size(268, 27);
      this.lblBrowse.TabIndex = 14;
      this.lblBrowse.Text = "Browse";
      this.pnlDetails.BackColor = Color.White;
      this.pnlDetails.Controls.Add((Control) this.pnlBookInfo);
      this.pnlDetails.Controls.Add((Control) this.pnlServerInfo);
      this.pnlDetails.Dock = DockStyle.Fill;
      this.pnlDetails.Location = new Point(0, 27);
      this.pnlDetails.Name = "pnlDetails";
      this.pnlDetails.Size = new Size(302, 380);
      this.pnlDetails.TabIndex = 15;
      this.pnlBookInfo.BackColor = Color.White;
      this.pnlBookInfo.Controls.Add((Control) this.pnlrtbBookInfo);
      this.pnlBookInfo.Controls.Add((Control) this.lblBookmarksInfo);
      this.pnlBookInfo.Dock = DockStyle.Fill;
      this.pnlBookInfo.Location = new Point(0, 101);
      this.pnlBookInfo.Name = "pnlBookInfo";
      this.pnlBookInfo.Padding = new Padding(15, 10, 0, 0);
      this.pnlBookInfo.Size = new Size(302, 279);
      this.pnlBookInfo.TabIndex = 14;
      this.pnlBookInfo.Tag = (object) "";
      this.pnlrtbBookInfo.BackColor = Color.White;
      this.pnlrtbBookInfo.Controls.Add((Control) this.rtbBookInfo);
      this.pnlrtbBookInfo.Dock = DockStyle.Fill;
      this.pnlrtbBookInfo.Location = new Point(15, 38);
      this.pnlrtbBookInfo.Name = "pnlrtbBookInfo";
      this.pnlrtbBookInfo.Padding = new Padding(10, 0, 0, 0);
      this.pnlrtbBookInfo.Size = new Size(287, 241);
      this.pnlrtbBookInfo.TabIndex = 15;
      this.rtbBookInfo.BackColor = Color.White;
      this.rtbBookInfo.BorderStyle = BorderStyle.None;
      this.rtbBookInfo.Dock = DockStyle.Fill;
      this.rtbBookInfo.Location = new Point(10, 0);
      this.rtbBookInfo.Name = "rtbBookInfo";
      this.rtbBookInfo.ReadOnly = true;
      this.rtbBookInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbBookInfo.Size = new Size(277, 241);
      this.rtbBookInfo.TabIndex = 12;
      this.rtbBookInfo.Text = "";
      this.rtbBookInfo.MouseDown += new MouseEventHandler(this.rtbBookInfo_MouseDown);
      this.lblBookmarksInfo.BackColor = Color.Transparent;
      this.lblBookmarksInfo.Cursor = Cursors.Hand;
      this.lblBookmarksInfo.Dock = DockStyle.Top;
      this.lblBookmarksInfo.ForeColor = Color.Blue;
      this.lblBookmarksInfo.Image = (Image) Resources.GroupLine2;
      this.lblBookmarksInfo.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblBookmarksInfo.Location = new Point(15, 10);
      this.lblBookmarksInfo.Name = "lblBookmarksInfo";
      this.lblBookmarksInfo.Size = new Size(287, 28);
      this.lblBookmarksInfo.TabIndex = 11;
      this.lblBookmarksInfo.Tag = (object) "";
      this.lblBookmarksInfo.Text = "Bookmarks Information";
      this.lblBookmarksInfo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblBookmarksInfo.Click += new EventHandler(this.lblBookmarksInfo_Click);
      this.pnlServerInfo.BackColor = Color.White;
      this.pnlServerInfo.Controls.Add((Control) this.pnlrtbServerInfo);
      this.pnlServerInfo.Controls.Add((Control) this.lblServerInfo);
      this.pnlServerInfo.Dock = DockStyle.Top;
      this.pnlServerInfo.Location = new Point(0, 0);
      this.pnlServerInfo.Name = "pnlServerInfo";
      this.pnlServerInfo.Padding = new Padding(15, 10, 0, 0);
      this.pnlServerInfo.Size = new Size(302, 101);
      this.pnlServerInfo.TabIndex = 13;
      this.pnlServerInfo.Tag = (object) "";
      this.pnlrtbServerInfo.BackColor = Color.White;
      this.pnlrtbServerInfo.Controls.Add((Control) this.rtbServerInfo);
      this.pnlrtbServerInfo.Dock = DockStyle.Fill;
      this.pnlrtbServerInfo.Location = new Point(15, 38);
      this.pnlrtbServerInfo.Name = "pnlrtbServerInfo";
      this.pnlrtbServerInfo.Padding = new Padding(10, 0, 0, 0);
      this.pnlrtbServerInfo.Size = new Size(287, 63);
      this.pnlrtbServerInfo.TabIndex = 14;
      this.rtbServerInfo.BackColor = Color.White;
      this.rtbServerInfo.BorderStyle = BorderStyle.None;
      this.rtbServerInfo.Dock = DockStyle.Fill;
      this.rtbServerInfo.Location = new Point(10, 0);
      this.rtbServerInfo.Name = "rtbServerInfo";
      this.rtbServerInfo.ReadOnly = true;
      this.rtbServerInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbServerInfo.Size = new Size(277, 63);
      this.rtbServerInfo.TabIndex = 12;
      this.rtbServerInfo.Text = "";
      this.rtbServerInfo.MouseDown += new MouseEventHandler(this.rtbServerInfo_MouseDown);
      this.lblServerInfo.BackColor = Color.Transparent;
      this.lblServerInfo.Cursor = Cursors.Hand;
      this.lblServerInfo.Dock = DockStyle.Top;
      this.lblServerInfo.ForeColor = Color.Blue;
      this.lblServerInfo.Image = (Image) componentResourceManager.GetObject("lblServerInfo.Image");
      this.lblServerInfo.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblServerInfo.Location = new Point(15, 10);
      this.lblServerInfo.Name = "lblServerInfo";
      this.lblServerInfo.Size = new Size(287, 28);
      this.lblServerInfo.TabIndex = 11;
      this.lblServerInfo.Tag = (object) "";
      this.lblServerInfo.Text = "Server Information";
      this.lblServerInfo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblServerInfo.Click += new EventHandler(this.lblServerInfo_Click);
      this.lblDetails.BackColor = Color.White;
      this.lblDetails.Dock = DockStyle.Top;
      this.lblDetails.ForeColor = Color.Black;
      this.lblDetails.Location = new Point(0, 0);
      this.lblDetails.Name = "lblDetails";
      this.lblDetails.Padding = new Padding(3, 7, 0, 0);
      this.lblDetails.Size = new Size(302, 27);
      this.lblDetails.TabIndex = 14;
      this.lblDetails.Text = "Details";
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.picLoading.BackColor = Color.Transparent;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(4, 4);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 17;
      this.picLoading.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(578, 409);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.picLoading);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmOpenBookBookmarks);
      this.Text = "frmOpenFavourites";
      this.Load += new EventHandler(this.frmOpenFavourites_Load);
      this.VisibleChanged += new EventHandler(this.frmOpenFavourites_VisibleChanged);
      this.pnlForm.Panel1.ResumeLayout(false);
      this.pnlForm.Panel2.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.pnltvBrowse.ResumeLayout(false);
      this.pnlDetails.ResumeLayout(false);
      this.pnlBookInfo.ResumeLayout(false);
      this.pnlrtbBookInfo.ResumeLayout(false);
      this.pnlServerInfo.ResumeLayout(false);
      this.pnlrtbServerInfo.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    [DllImport("user32.dll")]
    public static extern long HideCaret(IntPtr hwnd);

    public frmOpenBookBookmarks(frmOpenBook frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.statusText = this.GetResource("Open Bookmarks by browsing", "OPEN_BOOKMARKS_BROWSING", ResourceType.LABEL);
      this.p_BookId = string.Empty;
      this.p_ServerId = 0;
      this.p_BookNode = (XmlNode) null;
      this.p_SchemaNode = (XmlNode) null;
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmOpenFavourites_Load(object sender, EventArgs e)
    {
      try
      {
        this.tvBrowse.BeginUpdate();
        for (int index = 0; index < Program.iniServers.Length; ++index)
        {
          TreeNode node = new TreeNode();
          node.Text = Program.iniServers[index].items["SETTINGS", "DISPLAY_NAME"];
          node.Tag = (object) index;
          if (node.Text != string.Empty)
            this.tvBrowse.Nodes.Add(node);
        }
        this.tvBrowse.EndUpdate();
        try
        {
          if (this.tvBrowse.Nodes.Count == 1)
          {
            this.tvBrowse.SelectedNode = this.tvBrowse.Nodes[0];
            this.SelectTreeNode();
          }
        }
        catch
        {
        }
      }
      catch
      {
        this.Hide();
        MessageHandler.ShowQuestion(this.GetResource("(E-OBM-EM001) Failed to load specified object", "(E-OBM-EM001)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
        this.Show();
      }
      try
      {
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetResource("Select a bookmark", "SELECT_A_BOOKMARK", ResourceType.LABEL);
        this.rtbServerInfo.Clear();
        this.rtbServerInfo.SelectionColor = Color.Gray;
        this.rtbServerInfo.SelectedText = this.GetResource("Select A Server", "SELECT_A_SERVER", ResourceType.LABEL);
      }
      catch
      {
      }
    }

    private void frmOpenFavourites_VisibleChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.Visible)
          return;
        this.UpdateStatus();
      }
      catch
      {
      }
    }

    private void lblServerInfo_Click(object sender, EventArgs e)
    {
      if (this.pnlrtbServerInfo.Visible)
      {
        this.pnlrtbServerInfo.Visible = false;
        this.pnlServerInfo.Height -= this.pnlrtbServerInfo.Height;
        this.lblServerInfo.Image = (Image) Resources.GroupLine3;
      }
      else
      {
        this.pnlServerInfo.Height += this.pnlrtbServerInfo.Height;
        this.pnlrtbServerInfo.Visible = true;
        this.lblServerInfo.Image = (Image) Resources.GroupLine2;
      }
    }

    private void lblBookmarksInfo_Click(object sender, EventArgs e)
    {
      if (this.pnlrtbBookInfo.Visible)
      {
        this.pnlrtbBookInfo.Visible = false;
        this.pnlBookInfo.Height -= this.pnlrtbBookInfo.Height;
        this.lblBookmarksInfo.Image = (Image) Resources.GroupLine3;
      }
      else
      {
        this.pnlBookInfo.Height += this.pnlrtbBookInfo.Height;
        this.pnlrtbBookInfo.Visible = true;
        this.lblBookmarksInfo.Image = (Image) Resources.GroupLine2;
      }
    }

    private void rtbServerInfo_MouseDown(object sender, MouseEventArgs e)
    {
      frmOpenBookBookmarks.HideCaret(this.rtbServerInfo.Handle);
    }

    private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
    {
      frmOpenBookBookmarks.HideCaret(this.rtbBookInfo.Handle);
    }

    private void tvBrowse_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      this.SelectTreeNode();
    }

    private void tvBrowse_AfterSelect(object sender, TreeViewEventArgs e)
    {
      XmlDocument xmlDocument = new XmlDocument();
      this.rtbServerInfo.SelectionFont = this.rtbServerInfo.Font;
      this.rtbBookInfo.SelectionFont = this.rtbBookInfo.Font;
      TreeNode treeNode = this.tvBrowse.SelectedNode;
      while (treeNode.Level > 0)
        treeNode = treeNode.Parent;
      string s = treeNode.Tag.ToString();
      if (s.Contains("::"))
        s = s.Substring(0, s.IndexOf("::"));
      int index;
      try
      {
        index = int.Parse(s);
      }
      catch
      {
        this.rtbServerInfo.Clear();
        this.rtbServerInfo.SelectionColor = Color.Red;
        this.rtbServerInfo.SelectedText = this.GetResource("(E-OBM-EM002) Failed to load specified object", "(E-OBM-EM002)_FAILED_LOAD", ResourceType.LABEL);
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Red;
        this.rtbBookInfo.SelectedText = this.GetResource("(E-OBM-EM003) Failed to load specified object", "(E-OBM-EM003)_FAILED_LOAD", ResourceType.LABEL);
        return;
      }
      this.rtbServerInfo.Clear();
      this.rtbServerInfo.SelectionColor = Color.Gray;
      this.rtbServerInfo.SelectedText = this.GetResource("Server Key", "SERVER_KEY", ResourceType.LABEL) + ": ";
      this.rtbServerInfo.SelectionColor = Color.Black;
      this.rtbServerInfo.SelectedText = Program.iniServers[index].sIniKey + " \n";
      this.rtbServerInfo.SelectionColor = Color.Gray;
      this.rtbBookInfo.Clear();
      this.rtbBookInfo.SelectionColor = Color.Gray;
      this.rtbBookInfo.SelectedText = this.GetResource("Select a bookmark", "SELECT_A_BOOKMARK", ResourceType.LABEL);
      if (this.tvBrowse.SelectedNode.Nodes.Count != 0 || !(this.tvBrowse.SelectedNode.Tag.ToString() != "") || (!(this.tvBrowse.SelectedNode.Tag.ToString().ToUpper() != "NIL") || !treeNode.Tag.ToString().Contains("::")))
        return;
      string empty = string.Empty;
      try
      {
        string str = treeNode.Tag.ToString();
        XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(str.Substring(str.IndexOf("::") + 2, str.Length - (str.IndexOf("::") + 2))));
        XmlNode xmlNode1 = xmlDocument.ReadNode((XmlReader) xmlTextReader1);
        XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(this.tvBrowse.SelectedNode.Tag.ToString()));
        XmlNode xmlNode2 = xmlDocument.ReadNode((XmlReader) xmlTextReader2);
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetBookInfoLanguage("PublishingID", Program.iniServers[index].sIniKey) + ": ";
        this.rtbBookInfo.SelectionColor = Color.Black;
        this.rtbBookInfo.SelectedText = xmlNode2.Attributes["BookId"].Value + "\n";
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetBookInfoLanguage("PageName", Program.iniServers[index].sIniKey) + ": ";
        this.rtbBookInfo.SelectionColor = Color.Black;
        this.rtbBookInfo.SelectedText = xmlNode2.Attributes["PageName"].Value + "\n";
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetBookInfoLanguage("PicIndex", Program.iniServers[index].sIniKey) + ": ";
        this.rtbBookInfo.SelectionColor = Color.Black;
        this.rtbBookInfo.SelectedText = xmlNode2.Attributes["PicIndex"].Value + "\n";
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetBookInfoLanguage("ListIndex", Program.iniServers[index].sIniKey) + ": ";
        this.rtbBookInfo.SelectionColor = Color.Black;
        this.rtbBookInfo.SelectedText = xmlNode2.Attributes["ListIndex"].Value + "\n";
        if (!xmlNode2.Attributes["PartNo"].Value.Equals(string.Empty))
        {
          this.rtbBookInfo.SelectionColor = Color.Gray;
          this.rtbBookInfo.SelectedText = this.GetBookInfoLanguage("PartNumber", Program.iniServers[index].sIniKey) + ": ";
          this.rtbBookInfo.SelectionColor = Color.Black;
          this.rtbBookInfo.SelectedText = xmlNode2.Attributes["PartNo"].Value + "\n";
        }
        if (xmlNode2.Attributes["BookId"].Value != null)
          empty = xmlNode2.Attributes["BookId"].Value;
        this.p_ServerId = index;
        this.p_BookId = empty;
        this.p_BookNode = xmlNode2;
        this.p_SchemaNode = xmlNode1;
      }
      catch
      {
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Red;
        this.rtbBookInfo.SelectedText = this.GetResource("(E-OBM-EM003) Failed to load specified object", "(E-OBM-EM003)_FAILED_LOAD", ResourceType.LABEL);
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      object[] objArray = (object[]) e.Argument;
      string sFilePath = (string) objArray[0];
      TreeNode objTreeNode = (TreeNode) objArray[1];
      this.statusText = this.GetResource("Loading bookmarks", "LOADING_BOOKMARKS", ResourceType.STATUS_MESSAGE);
      this.UpdateStatus();
      if (this.frmParent.IsDisposed)
        return;
      this.statusText = this.GetResource("Loading bookmarks", "LOADING_BOOKMARKS", ResourceType.STATUS_MESSAGE);
      this.UpdateStatus();
      if (this.LoadFavouriteBooks(sFilePath, objTreeNode))
        e.Result = (object) "ok";
      else
        e.Result = (object) this.GetResource("Loading bookmarks", "(E-OBM-EM004)_FAILED_LOAD", ResourceType.POPUP_MESSAGE);
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      string result = (string) e.Result;
      if (this.frmParent.IsDisposed)
        return;
      if (result.Equals("ok"))
      {
        this.statusText = this.GetResource("Bookmarks loaded completely", "BOOKMARKS_LOADED_COMPLETELY", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        this.tvBrowse.SelectedNode.Expand();
      }
      else
      {
        this.statusText = result;
        this.UpdateStatus();
        MessageHandler.ShowInformation(result);
      }
      if (this.tvBrowse.SelectedNode.Nodes.Count == 0)
      {
        this.tvBrowse.SelectedNode.Nodes.Add(new TreeNode()
        {
          Text = "NIL",
          Name = "NIL",
          Tag = (object) "NIL"
        });
        this.tvBrowse.SelectedNode.Expand();
      }
      this.HideLoading(this.pnltvBrowse);
    }

    private void SelectTreeNode()
    {
      try
      {
        if (this.tvBrowse.SelectedNode.Level == 0 && this.tvBrowse.SelectedNode.Nodes.Count == 0)
        {
          string path = string.Empty;
          string empty = string.Empty;
          try
          {
            int index = int.Parse(this.tvBrowse.SelectedNode.Tag.ToString());
            path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = path + "\\" + Application.ProductName;
            path = path + "\\" + Program.iniServers[index].sIniKey;
            path += "\\BookMarks.xml";
          }
          catch
          {
          }
          if (File.Exists(path))
          {
            this.ShowLoading(this.pnltvBrowse);
            this.bgWorker.RunWorkerAsync((object) new object[2]
            {
              (object) path,
              (object) this.tvBrowse.SelectedNode
            });
          }
          else if (this.tvBrowse.SelectedNode.Nodes.Count == 0)
          {
            this.tvBrowse.SelectedNode.Nodes.Add(new TreeNode()
            {
              Text = "NIL",
              Name = "NIL",
              Tag = (object) "NIL"
            });
            this.tvBrowse.SelectedNode.Expand();
          }
        }
        if (this.tvBrowse.SelectedNode.Nodes.Count != 0 || !(this.tvBrowse.SelectedNode.Tag.ToString() != "") || (this.tvBrowse.SelectedNode.Level != 2 || !(this.tvBrowse.SelectedNode.Text != "NIL")))
          return;
        XmlDocument xmlDocument = new XmlDocument();
        string empty1 = string.Empty;
        XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(this.tvBrowse.SelectedNode.Tag.ToString()));
        this.frmParent.CloseAndLoadFavourite(xmlDocument.ReadNode((XmlReader) xmlTextReader));
      }
      catch
      {
      }
    }

    private bool LoadFavouriteBooks(string sFilePath, TreeNode objTreeNode)
    {
      XmlDocument objXmlDoc = new XmlDocument();
      if (!File.Exists(sFilePath))
        return false;
      try
      {
        objXmlDoc.Load(sFilePath);
      }
      catch
      {
        return false;
      }
      XmlNode xmlNode1 = objXmlDoc.SelectSingleNode("//BookMarks");
      TreeNode tnParent = (TreeNode) null;
      objTreeNode.Tag = (object) (objTreeNode.Tag.ToString() + "::" + xmlNode1.OuterXml);
      XmlNodeList xmlNodeList = objXmlDoc.SelectNodes("/BookMarks/BookMark[not(@BookId=preceding-sibling::BookMark/@BookId)]/@BookId");
      string str = string.Empty;
      foreach (XmlNode xmlNode2 in xmlNodeList)
      {
        if (!str.Contains("||" + xmlNode2.Value.ToUpper() + "||"))
        {
          str = str + "||" + xmlNode2.Value.ToUpper() + "||";
          tnParent = objTreeNode;
          TreeNode treeNode = new TreeNode();
          treeNode.Text = xmlNode2.Value.Replace("&", "&&");
          treeNode.Name = xmlNode2.Value;
          treeNode.Tag = (object) xmlNode2.OuterXml;
          this.LoadFavouritePages(objXmlDoc, xmlNode2.Value, treeNode);
          this.AddNode(tnParent, treeNode);
        }
      }
      objTreeNode = tnParent;
      if (objTreeNode != null)
      {
        if (objTreeNode.Nodes.Count == 0)
          objTreeNode.Tag = (object) objTreeNode.Tag.ToString().Substring(0, objTreeNode.Tag.ToString().IndexOf("::"));
      }
      else
      {
        try
        {
          File.Delete(sFilePath);
        }
        catch
        {
        }
      }
      return true;
    }

    private void LoadFavouritePages(XmlDocument objXmlDoc, string objBookID, TreeNode objTreeNode)
    {
      try
      {
        foreach (XmlNode selectNode in objXmlDoc.SelectNodes("//BookMarks/BookMark[translate(@BookId, 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + objBookID.ToUpper() + "']"))
          this.AddNode(objTreeNode, new TreeNode()
          {
            Text = selectNode.Attributes["PageName"].Value.Replace("&", "&&"),
            Name = selectNode.Attributes["PageName"].Value,
            Tag = (object) selectNode.OuterXml
          });
      }
      catch
      {
      }
    }

    private void AddNode(TreeNode tnParent, TreeNode tnChild)
    {
      if (this.tvBrowse.InvokeRequired)
        this.tvBrowse.Invoke((Delegate) new frmOpenBookBookmarks.AddNodeDelegate(this.AddNode), (object) tnParent, (object) tnChild);
      else
        tnParent.Nodes.Add(tnChild);
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
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
            control.Enabled = true;
        }
        this.picLoading.Hide();
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
        this.frmParent.Invoke((Delegate) new frmOpenBookBookmarks.StatusDelegate(this.frmParent.UpdateStatus), (object) this.statusText);
      else
        this.frmParent.UpdateStatus(this.statusText);
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblBrowse.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.lblDetails.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    public void LoadResources()
    {
      this.lblBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.LABEL);
      this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
      this.lblServerInfo.Text = this.GetResource("Server Information", "SERVER_INFORMATION", ResourceType.LABEL);
      this.lblBookmarksInfo.Text = this.GetResource("Bookmarks Information", "BOOKMARKS_INFORMATION", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='OPEN_BOOKS']" + "/Screen[@Name='OPENBOOKBOOKMARKS']";
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

    protected override void WndProc(ref Message m)
    {
      try
      {
        base.WndProc(ref m);
        frmOpenBookBookmarks.HideCaret(this.rtbBookInfo.Handle);
        frmOpenBookBookmarks.HideCaret(this.rtbServerInfo.Handle);
      }
      catch
      {
      }
    }

    private string GetBookInfoLanguage(string sKey, string sServerKey)
    {
      bool flag = false;
      string str1 = Settings.Default.appLanguage + "_GSP_" + sServerKey + ".ini";
      if (File.Exists(Application.StartupPath + "\\Language XMLs\\" + str1))
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

    private delegate void AddNodeDelegate(TreeNode tnParent, TreeNode tnChild);

    private delegate void StatusDelegate(string status);
  }
}

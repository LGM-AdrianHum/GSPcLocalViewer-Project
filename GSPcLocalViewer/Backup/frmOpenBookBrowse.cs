// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOpenBookBrowse
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
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
  public class frmOpenBookBrowse : Form
  {
    private const string dllZipper = "ZIPPER.dll";
    private IContainer components;
    private SplitContainer pnlForm;
    private Panel pnlDetails;
    private Panel pnlBookInfo;
    private Panel pnlrtbBookInfo;
    private RichTextBox rtbBookInfo;
    private Label lblBookInfo;
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
    private bool p_Encrypted;
    private bool p_Compressed;
    private string p_BookType;
    private Download objDownloader;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmOpenBookBrowse));
      this.pnlForm = new SplitContainer();
      this.pnltvBrowse = new Panel();
      this.tvBrowse = new CustomTreeView();
      this.lblBrowse = new Label();
      this.pnlDetails = new Panel();
      this.pnlBookInfo = new Panel();
      this.pnlrtbBookInfo = new Panel();
      this.rtbBookInfo = new RichTextBox();
      this.lblBookInfo = new Label();
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
      this.pnlForm.Panel2MinSize = 80;
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
      this.pnlBookInfo.Controls.Add((Control) this.lblBookInfo);
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
      this.lblBookInfo.BackColor = Color.Transparent;
      this.lblBookInfo.Cursor = Cursors.Hand;
      this.lblBookInfo.Dock = DockStyle.Top;
      this.lblBookInfo.ForeColor = Color.Blue;
      this.lblBookInfo.Image = (Image) Resources.GroupLine2;
      this.lblBookInfo.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblBookInfo.Location = new Point(15, 10);
      this.lblBookInfo.Name = "lblBookInfo";
      this.lblBookInfo.Size = new Size(287, 28);
      this.lblBookInfo.TabIndex = 11;
      this.lblBookInfo.Tag = (object) "";
      this.lblBookInfo.Text = "Book Information";
      this.lblBookInfo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblBookInfo.Click += new EventHandler(this.lblBookInfo_Click);
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
      this.picLoading.Size = new Size(34, 34);
      this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
      this.picLoading.TabIndex = 17;
      this.picLoading.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(578, 409);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.picLoading);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmOpenBookBrowse);
      this.Text = nameof (frmOpenBookBrowse);
      this.Load += new EventHandler(this.frmOpenBookBrowse_Load);
      this.VisibleChanged += new EventHandler(this.frmOpenBookBrowse_VisibleChanged);
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
      this.PerformLayout();
    }

    [DllImport("user32.dll")]
    public static extern long HideCaret(IntPtr hwnd);

    public frmOpenBookBrowse(frmOpenBook frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.statusText = this.GetResource("Open books by browsing", "OPEN_BY_BROWSING", ResourceType.STATUS_MESSAGE);
      this.p_BookId = string.Empty;
      this.p_ServerId = 0;
      this.p_BookNode = (XmlNode) null;
      this.p_SchemaNode = (XmlNode) null;
      this.p_Encrypted = false;
      this.p_Compressed = false;
      this.objDownloader = new Download(this.frmParent);
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmOpenBookBrowse_Load(object sender, EventArgs e)
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
        MessageHandler.ShowQuestion(this.GetResource("(E-OBB-EM001) Failed to load specified object", "(E-OBB-EM001)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
        this.Show();
      }
      try
      {
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetResource("Select A Book", "SELECT_A_BOOK", ResourceType.LABEL);
        this.rtbServerInfo.Clear();
        this.rtbServerInfo.SelectionColor = Color.Gray;
        this.rtbServerInfo.SelectedText = this.GetResource("Select A Server", "SELECT_A_SERVER", ResourceType.LABEL);
      }
      catch
      {
      }
    }

    private void frmOpenBookBrowse_VisibleChanged(object sender, EventArgs e)
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

    private void tvBrowse_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      if (this.tvBrowse.SelectedNode == null || e.Node.Text == "(Not Found)")
        return;
      this.SelectTreeNode();
    }

    private void tvBrowse_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (e.Node.Text == "(Not Found)")
        return;
      XmlDocument xmlDocument = new XmlDocument();
      bool flag = false;
      this.rtbServerInfo.SelectionFont = this.rtbServerInfo.Font;
      this.rtbBookInfo.SelectionFont = this.rtbBookInfo.Font;
      TreeNode treeNode = this.tvBrowse.SelectedNode;
      while (treeNode.Level > 0)
        treeNode = treeNode.Parent;
      string s = treeNode.Tag.ToString();
      if (s.Contains("::"))
        s = s.Substring(0, s.IndexOf("::"));
      int index1;
      try
      {
        index1 = int.Parse(s);
      }
      catch
      {
        this.rtbServerInfo.Clear();
        this.rtbServerInfo.SelectionColor = Color.Red;
        this.rtbServerInfo.SelectedText = this.GetResource("(E-OBB-EM003) Failed to load specified object", "(E-OBB-EM003)_FAILED_LOAD", ResourceType.LABEL);
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Red;
        this.rtbBookInfo.SelectedText = this.GetResource("(E-OBB-EM004) Failed to load specified object", "E-OBB-EM004)_FAILED_LOAD", ResourceType.LABEL);
        return;
      }
      this.p_Encrypted = Program.iniServers[index1].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[index1].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON";
      this.p_Compressed = Program.iniServers[index1].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[index1].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON";
      this.rtbServerInfo.Clear();
      this.rtbServerInfo.SelectionColor = Color.Gray;
      this.rtbServerInfo.SelectedText = this.GetResource("Server Key", "SERVER_KEY", ResourceType.LABEL) + ": ";
      this.rtbServerInfo.SelectionColor = Color.Black;
      this.rtbServerInfo.SelectedText = Program.iniServers[index1].sIniKey + " \n";
      this.rtbServerInfo.SelectionColor = Color.Gray;
      this.rtbBookInfo.Clear();
      this.rtbBookInfo.SelectionColor = Color.Gray;
      this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
      if (this.tvBrowse.SelectedNode.Nodes.Count != 0 || !(this.tvBrowse.SelectedNode.Tag.ToString() != "") || !treeNode.Tag.ToString().Contains("::"))
        return;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      try
      {
        string str = treeNode.Tag.ToString();
        XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(str.Substring(str.IndexOf("::") + 2, str.Length - (str.IndexOf("::") + 2))));
        XmlNode xmlNode1 = xmlDocument.ReadNode((XmlReader) xmlTextReader1);
        XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(this.tvBrowse.SelectedNode.Tag.ToString()));
        XmlNode xmlNode2 = xmlDocument.ReadNode((XmlReader) xmlTextReader2);
        this.rtbBookInfo.Clear();
        for (int index2 = 0; index2 < xmlNode1.Attributes.Count; ++index2)
        {
          if (!xmlNode1.Attributes[index2].Value.ToUpper().StartsWith("LEVEL") && xmlNode2.Attributes[xmlNode1.Attributes[index2].Name] != null && (xmlNode1.Attributes[index2].Value.ToUpper() != "BOOKCODE" && xmlNode1.Attributes[index2].Value.ToUpper() != "SECURITYLOCKS") && xmlNode1.Attributes[index2].Value.ToUpper() != "ID")
          {
            this.rtbBookInfo.SelectionColor = Color.Gray;
            this.rtbBookInfo.SelectedText = this.GetBookInfoLanguage(xmlNode1.Attributes[index2].Value, Program.iniServers[index1].sIniKey) + ": ";
            this.rtbBookInfo.SelectionColor = Color.Black;
            this.rtbBookInfo.SelectedText = xmlNode2.Attributes[xmlNode1.Attributes[index2].Name].Value + "\n";
          }
          if (xmlNode1.Attributes[index2].Value.ToUpper().StartsWith("PUBLISHINGID"))
            empty1 = xmlNode2.Attributes[xmlNode1.Attributes[index2].Name].Value;
          if (xmlNode1.Attributes[index2].Value.ToUpper().StartsWith("BOOKTYPE"))
          {
            if (xmlNode2.Attributes[xmlNode1.Attributes[index2].Name] == null)
            {
              this.rtbBookInfo.Clear();
              this.rtbBookInfo.SelectionColor = Color.Red;
              this.rtbBookInfo.SelectedText = this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_INFORMATION_NOEXIST", ResourceType.LABEL);
              break;
            }
            empty2 = xmlNode2.Attributes[xmlNode1.Attributes[index2].Name].Value;
            flag = true;
          }
        }
        if (!flag)
          return;
        this.p_ServerId = index1;
        this.p_BookId = empty1;
        this.p_BookNode = xmlNode2;
        this.p_SchemaNode = xmlNode1;
        this.p_BookType = empty2;
      }
      catch
      {
        if (this.tvBrowse.SelectedNode.Level == 0 && this.tvBrowse.SelectedNode.Nodes.Count == 0 && Program.objAppFeatures.bDcMode)
          return;
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Red;
        this.rtbBookInfo.SelectedText = this.GetResource("(E-OBM-EM003) Failed to load specified object", "(E-OBM-EM003)_FAILED_LOAD", ResourceType.LABEL);
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

    private void lblBookInfo_Click(object sender, EventArgs e)
    {
      if (this.pnlrtbBookInfo.Visible)
      {
        this.pnlrtbBookInfo.Visible = false;
        this.pnlBookInfo.Height -= this.pnlrtbBookInfo.Height;
        this.lblBookInfo.Image = (Image) Resources.GroupLine3;
      }
      else
      {
        this.pnlBookInfo.Height += this.pnlrtbBookInfo.Height;
        this.pnlrtbBookInfo.Visible = true;
        this.lblBookInfo.Image = (Image) Resources.GroupLine2;
      }
    }

    private void rtbServerInfo_MouseDown(object sender, MouseEventArgs e)
    {
      frmOpenBookBrowse.HideCaret(this.rtbServerInfo.Handle);
    }

    private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
    {
      frmOpenBookBrowse.HideCaret(this.rtbBookInfo.Handle);
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      object[] objArray = (object[]) e.Argument;
      string surl1_1;
      string str1;
      if (this.p_Compressed)
      {
        surl1_1 = (string) objArray[0] + "Series.zip";
        str1 = (string) objArray[1] + "\\Series.zip";
      }
      else
      {
        surl1_1 = (string) objArray[0] + "Series.xml";
        str1 = (string) objArray[1] + "\\Series.xml";
      }
      string surl1_2 = (string) objArray[0] + "DataUpdate.xml";
      string str2 = (string) objArray[1] + "\\DataUpdate.xml";
      bool flag = false;
      TreeNode objTreeNode = (TreeNode) objArray[2];
      this.statusText = this.GetResource("Checking for data updates……", "CHECKING_UPDATES", ResourceType.STATUS_MESSAGE);
      this.UpdateStatus();
      int result = 0;
      if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
        result = 0;
      if (!Program.objAppMode.bWorkingOffline)
      {
        if (!Program.objAppFeatures.bDcMode)
          this.objDownloader.DownloadFile(surl1_2, str2);
      }
      try
      {
        string s = objTreeNode.Tag.ToString();
        if (s.Contains("::"))
          s = s.Substring(0, s.IndexOf("::"));
        this.p_ServerId = int.Parse(s);
      }
      catch
      {
      }
      if (File.Exists(str1))
      {
        if (result == 0)
          flag = true;
        else if (result < 1000)
        {
          DateTime dtServer = Global.DataUpdateDate(str2);
          if (Global.IntervalElapsed(Global.GetLocalDateOfFile(str1, this.p_Compressed, this.p_Encrypted), dtServer, result))
            flag = true;
        }
      }
      else
        flag = true;
      if (flag && !Program.objAppMode.bWorkingOffline && !Program.objAppFeatures.bDcMode)
      {
        this.statusText = this.GetResource("Downloading……", "DOWNLOADING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (!this.objDownloader.DownloadFile(surl1_1, str1) && !this.frmParent.IsDisposed)
        {
          this.statusText = this.GetResource("(E-OBB-EM006) Failed to load specified object", "(E-OBB-EM006)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          e.Result = (object) this.GetResource("(E-OBB-EM006) Failed to load specified object", "(E-OBB-EM006)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
        }
      }
      if (File.Exists(str1))
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.GetResource("Loading..", "LOADING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (this.LoadSeries(str1, objTreeNode))
          e.Result = (object) "ok";
        else
          e.Result = (object) this.GetResource("(E-OBB-EM006) Failed to load specified object", "(E-OBB-EM006)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
      }
      else
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.GetResource("(E-OBB-EM007) Specified information does not exist", "(E-OBB-EM007)_INFORMATION_NOEXIST", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        e.Result = (object) this.GetResource("(E-OBB-EM007) Specified information does not exist", "(E-OBB-EM007)_INFORMATION_NOEXIST", ResourceType.STATUS_MESSAGE);
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      string result = (string) e.Result;
      if (this.frmParent.IsDisposed)
        return;
      if (this.tvBrowse.SelectedNode.Nodes.Count == 0)
      {
        if (this.tvBrowse.SelectedNode.Level == 0)
        {
          try
          {
            this.tvBrowse.SelectedNode.Nodes.Add("(Not Found)");
            this.tvBrowse.SelectedNode.Expand();
            goto label_9;
          }
          catch
          {
            goto label_9;
          }
        }
      }
      if (result.Equals("ok"))
      {
        this.statusText = this.GetResource("Finished loading", "FINISHED_LOADING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        this.tvBrowse.SelectedNode.Expand();
      }
      else if (!Program.objAppFeatures.bDcMode)
      {
        this.statusText = result;
        this.UpdateStatus();
        MessageHandler.ShowInformation(result);
      }
label_9:
      this.HideLoading(this.pnltvBrowse);
    }

    private void SelectTreeNode()
    {
      try
      {
        if (this.tvBrowse.SelectedNode.Level == 0 && this.tvBrowse.SelectedNode.Nodes.Count == 0)
        {
          string empty = string.Empty;
          string path = string.Empty;
          try
          {
            int index;
            try
            {
              if (this.tvBrowse.SelectedNode.Tag.ToString().Contains("::"))
                this.tvBrowse.SelectedNode.Tag = (object) this.tvBrowse.SelectedNode.Tag.ToString().Substring(0, this.tvBrowse.SelectedNode.Tag.ToString().IndexOf("::"));
              index = int.Parse(this.tvBrowse.SelectedNode.Tag.ToString());
            }
            catch
            {
              return;
            }
            empty = Program.iniServers[index].items["SETTINGS", "CONTENT_PATH"];
            if (!empty.EndsWith("/"))
              empty += "/";
            path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
            path = path + "\\" + Program.iniServers[index].sIniKey;
            if (!Directory.Exists(path))
              Directory.CreateDirectory(path);
          }
          catch
          {
            MessageHandler.ShowError(this.GetResource("(E-OBB-EM002) Failed to create file/folder specified", "(E-OBB-EM002)_FAILED_FOLDER", ResourceType.POPUP_MESSAGE));
          }
          if (!Program.objAppMode.bWorkingOffline)
          {
            this.ShowLoading(this.pnltvBrowse);
            this.bgWorker.RunWorkerAsync((object) new object[3]
            {
              (object) empty,
              (object) path,
              (object) this.tvBrowse.SelectedNode
            });
          }
          else
          {
            string str = path + "\\Series.xml";
            if (!File.Exists(str))
            {
              this.tvBrowse.SelectedNode.Nodes.Add("(Not Found)");
              this.tvBrowse.SelectedNode.Expand();
            }
            else
            {
              if (this.LoadSeries(str, this.tvBrowse.SelectedNode))
                this.tvBrowse.SelectedNode.Expand();
              if (this.tvBrowse.SelectedNode.Level == 0 && this.tvBrowse.SelectedNode.Nodes.Count == 0)
              {
                this.tvBrowse.SelectedNode.Nodes.Add("(Not Found)");
                this.tvBrowse.SelectedNode.Expand();
              }
            }
          }
        }
        if (this.tvBrowse.SelectedNode.Nodes.Count != 0 || !(this.tvBrowse.SelectedNode.Tag.ToString() != "") || this.tvBrowse.SelectedNode.Level <= 0)
          return;
        if (Program.objAppFeatures.bDcMode)
        {
          string empty = string.Empty;
          if (!File.Exists(Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.p_ServerId].sIniKey + "\\" + this.p_BookId + "\\" + this.p_BookId + ".xml"))
          {
            if (this.tvBrowse.SelectedNode.Text.EndsWith("(Not Found)"))
              return;
            this.tvBrowse.SelectedNode.Text += "(Not Found)";
            return;
          }
        }
        this.frmParent.CloseAndLoadData(this.p_ServerId, this.p_BookId, this.p_BookNode, this.p_SchemaNode, this.p_BookType);
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

    private bool LoadSeries(string sFilePath, TreeNode objTreeNode)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (!File.Exists(sFilePath))
        return false;
      if (this.p_Compressed)
      {
        try
        {
          string str = sFilePath.ToLower().Replace(".zip", ".xml");
          Global.Unzip(sFilePath);
          if (File.Exists(str))
            xmlDocument.Load(str);
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
        }
      }
      XmlNode xSchemaNode = xmlDocument.SelectSingleNode("//Schema");
      if (xSchemaNode == null)
        return false;
      string index1 = "";
      ArrayList arrayList = new ArrayList();
      string index2 = "";
      TreeNode treeNode = (TreeNode) null;
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          index1 = attribute.Name;
        else if (attribute.Value.ToUpper().StartsWith("LEVEL"))
          arrayList.Add((object) attribute.Name);
        else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
          index2 = attribute.Name;
      }
      if (index1 == "" || arrayList.Count == 0 || index2 == "")
        return false;
      objTreeNode.Tag = (object) (objTreeNode.Tag.ToString() + "::" + xSchemaNode.OuterXml);
      XmlNodeList xNodeList = xmlDocument.SelectNodes("//Books/Book");
      XmlNodeList xmlNodeList = this.frmParent.FilterBooksList(xSchemaNode, xNodeList);
      this.BeginUpdate();
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        treeNode = objTreeNode;
        if (xmlNode.Attributes[index1] != null && xmlNode.Attributes[index2] != null)
        {
          for (int index3 = 0; index3 < arrayList.Count; ++index3)
          {
            if (xmlNode.Attributes[arrayList[index3].ToString()] != null)
            {
              TreeNode tnChild = new TreeNode();
              tnChild.Text = xmlNode.Attributes[arrayList[index3].ToString()].Value.Replace("&", "&&");
              tnChild.Name = xmlNode.Attributes[arrayList[index3].ToString()].Value;
              if (treeNode.Nodes.ContainsKey(tnChild.Name))
              {
                treeNode = treeNode.Nodes[tnChild.Name];
              }
              else
              {
                this.AddNode(treeNode, tnChild);
                int num = treeNode.Text.ToLower() != "english" ? 1 : 0;
                treeNode = treeNode.Nodes[tnChild.Name];
              }
            }
          }
          if (xmlNode.Attributes[index2] != null)
          {
            FileInfo fileInfo = new FileInfo(sFilePath);
            if (File.Exists(string.Empty + fileInfo.DirectoryName + "\\" + xmlNode.Attributes[index2].Value + "\\" + xmlNode.Attributes[index2].Value + ".xml") || !Program.objAppFeatures.bDcMode)
              this.AddNode(treeNode, new TreeNode()
              {
                Text = xmlNode.Attributes[index2].Value.Replace("&", "&&"),
                Name = xmlNode.Attributes[index1].Value,
                Tag = (object) xmlNode.OuterXml
              });
            else if (treeNode.Nodes.Count < 1 && treeNode.Parent != null)
              this.RemoveNode(treeNode);
          }
        }
      }
      this.EndUpdate();
      objTreeNode = treeNode;
      if (objTreeNode.Nodes.Count == 0 && objTreeNode.Tag != null)
        objTreeNode.Tag = (object) objTreeNode.Tag.ToString().Substring(0, objTreeNode.Tag.ToString().IndexOf("::"));
      return true;
    }

    private void RemoveNode(TreeNode tnNode)
    {
      if (this.tvBrowse.InvokeRequired)
        this.tvBrowse.Invoke((Delegate) new frmOpenBookBrowse.RemoveNodeDelegate(this.RemoveNode), (object) tnNode);
      else if (tnNode.Parent.Nodes.Count < 2)
      {
        while (tnNode.Parent != null && tnNode.Parent.Nodes.Count < 2)
          tnNode = tnNode.Parent;
        if (tnNode.Parent != null)
          tnNode.Remove();
        else
          tnNode.Nodes.Clear();
      }
      else
        tnNode.Remove();
    }

    private void AddNode(TreeNode tnParent, TreeNode tnChild)
    {
      if (this.tvBrowse.InvokeRequired)
        this.tvBrowse.Invoke((Delegate) new frmOpenBookBrowse.AddNodeDelegate(this.AddNode), (object) tnParent, (object) tnChild);
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
        this.frmParent.Invoke((Delegate) new frmOpenBookBrowse.StatusDelegate(this.frmParent.UpdateStatus), (object) this.statusText);
      else
        this.frmParent.UpdateStatus(this.statusText);
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblBrowse.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.lblDetails.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private bool IntervalElapsed(DateTime dtLocal, DateTime dtServer)
    {
      try
      {
        int num = int.Parse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"]);
        return (dtServer.Date - dtLocal.Date).TotalDays >= (double) num;
      }
      catch
      {
        return true;
      }
    }

    private DateTime DataUpdateDate(string sDataUpdateFilePath)
    {
      try
      {
        if (!File.Exists(sDataUpdateFilePath))
          return DateTime.Now;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(sDataUpdateFilePath);
        return DateTime.Parse(xmlDocument.SelectSingleNode("//filelastmodified").InnerText, (IFormatProvider) new CultureInfo("fr-FR", false));
      }
      catch
      {
        return DateTime.Now;
      }
    }

    private void LoadResources()
    {
      this.lblBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.LABEL);
      this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
      this.lblServerInfo.Text = this.GetResource("Server Information", "SERVER_INFORMATION", ResourceType.LABEL);
      this.lblBookInfo.Text = this.GetResource("Book Information", "BOOK_INFORMATION", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='OPEN_BOOKS']" + "/Screen[@Name='BROWSE_BOOK']";
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
        frmOpenBookBrowse.HideCaret(this.rtbBookInfo.Handle);
        frmOpenBookBrowse.HideCaret(this.rtbServerInfo.Handle);
      }
      catch
      {
      }
    }

    private void BeginUpdate()
    {
      if (this.tvBrowse.InvokeRequired)
        this.tvBrowse.Invoke((Delegate) new frmOpenBookBrowse.BeginUpdateDelegate(this.BeginUpdate));
      else
        this.tvBrowse.BeginUpdate();
    }

    private void EndUpdate()
    {
      if (this.tvBrowse.InvokeRequired)
        this.tvBrowse.Invoke((Delegate) new frmOpenBookBrowse.EndUpdateDelegate(this.EndUpdate));
      else
        this.tvBrowse.EndUpdate();
    }

    private delegate void RemoveNodeDelegate(TreeNode tnNode);

    private delegate void AddNodeDelegate(TreeNode tnParent, TreeNode tnChild);

    private delegate void StatusDelegate(string status);

    private delegate void BeginUpdateDelegate();

    private delegate void EndUpdateDelegate();
  }
}

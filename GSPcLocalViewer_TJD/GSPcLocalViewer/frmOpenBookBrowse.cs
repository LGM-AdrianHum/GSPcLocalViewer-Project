using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
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

		public frmOpenBookBrowse(frmOpenBook frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.statusText = this.GetResource("Open books by browsing", "OPEN_BY_BROWSING", ResourceType.STATUS_MESSAGE);
			this.p_BookId = string.Empty;
			this.p_ServerId = 0;
			this.p_BookNode = null;
			this.p_SchemaNode = null;
			this.p_Encrypted = false;
			this.p_Compressed = false;
			this.objDownloader = new Download(this.frmParent);
			this.UpdateFont();
			this.LoadResources();
		}

		private void AddNode(TreeNode tnParent, TreeNode tnChild)
		{
			if (!this.tvBrowse.InvokeRequired)
			{
				tnParent.Nodes.Add(tnChild);
				return;
			}
			CustomTreeView customTreeView = this.tvBrowse;
			frmOpenBookBrowse.AddNodeDelegate addNodeDelegate = new frmOpenBookBrowse.AddNodeDelegate(this.AddNode);
			object[] objArray = new object[] { tnParent, tnChild };
			customTreeView.Invoke(addNodeDelegate, objArray);
		}

		private void BeginUpdate()
		{
			if (!this.tvBrowse.InvokeRequired)
			{
				this.tvBrowse.BeginUpdate();
				return;
			}
			this.tvBrowse.Invoke(new frmOpenBookBrowse.BeginUpdateDelegate(this.BeginUpdate));
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string str;
			string str1;
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			object[] argument = (object[])e.Argument;
			if (!this.p_Compressed)
			{
				str = string.Concat((string)argument[0], "Series.xml");
				str1 = string.Concat((string)argument[1], "\\Series.xml");
			}
			else
			{
				str = string.Concat((string)argument[0], "Series.zip");
				str1 = string.Concat((string)argument[1], "\\Series.zip");
			}
			string str2 = string.Concat((string)argument[0], "DataUpdate.xml");
			string str3 = string.Concat((string)argument[1], "\\DataUpdate.xml");
			bool flag = false;
			TreeNode treeNode = (TreeNode)argument[2];
			this.statusText = this.GetResource("Checking for data updates……", "CHECKING_UPDATES", ResourceType.STATUS_MESSAGE);
			this.UpdateStatus();
			int num = 0;
			if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
			{
				num = 0;
			}
			if (!Program.objAppMode.bWorkingOffline && !Program.objAppFeatures.bDcMode)
			{
				this.objDownloader.DownloadFile(str2, str3);
			}
			try
			{
				string str4 = "";
				str4 = treeNode.Tag.ToString();
				if (str4.Contains("::"))
				{
					str4 = str4.Substring(0, str4.IndexOf("::"));
				}
				this.p_ServerId = int.Parse(str4);
			}
			catch
			{
			}
			if (!File.Exists(str1))
			{
				flag = true;
			}
			else if (num == 0)
			{
				flag = true;
			}
			else if (num < 1000)
			{
				DateTime dateTime = Global.DataUpdateDate(str3);
				if (Global.IntervalElapsed(Global.GetLocalDateOfFile(str1, this.p_Compressed, this.p_Encrypted), dateTime, num))
				{
					flag = true;
				}
			}
			if (flag && !Program.objAppMode.bWorkingOffline && !Program.objAppFeatures.bDcMode)
			{
				this.statusText = this.GetResource("Downloading……", "DOWNLOADING", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				if (!this.objDownloader.DownloadFile(str, str1) && !this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("(E-OBB-EM006) Failed to load specified object", "(E-OBB-EM006)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					e.Result = this.GetResource("(E-OBB-EM006) Failed to load specified object", "(E-OBB-EM006)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
				}
			}
			if (File.Exists(str1))
			{
				if (!this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("Loading..", "LOADING", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					if (this.LoadSeries(str1, treeNode))
					{
						e.Result = "ok";
						return;
					}
					e.Result = this.GetResource("(E-OBB-EM006) Failed to load specified object", "(E-OBB-EM006)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
					return;
				}
			}
			else if (!this.frmParent.IsDisposed)
			{
				this.statusText = this.GetResource("(E-OBB-EM007) Specified information does not exist", "(E-OBB-EM007)_INFORMATION_NOEXIST", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				e.Result = this.GetResource("(E-OBB-EM007) Specified information does not exist", "(E-OBB-EM007)_INFORMATION_NOEXIST", ResourceType.STATUS_MESSAGE);
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			string result = (string)e.Result;
			if (!this.frmParent.IsDisposed)
			{
				if (this.tvBrowse.SelectedNode.Nodes.Count == 0 && this.tvBrowse.SelectedNode.Level == 0)
				{
					try
					{
						this.tvBrowse.SelectedNode.Nodes.Add("(Not Found)");
						this.tvBrowse.SelectedNode.Expand();
					}
					catch
					{
					}
				}
				else if (result.Equals("ok"))
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
				this.HideLoading(this.pnltvBrowse);
			}
		}

		private DateTime DataUpdateDate(string sDataUpdateFilePath)
		{
			DateTime now;
			try
			{
				if (File.Exists(sDataUpdateFilePath))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(sDataUpdateFilePath);
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//filelastmodified");
					now = DateTime.Parse(xmlNodes.InnerText, new CultureInfo("fr-FR", false));
				}
				else
				{
					now = DateTime.Now;
				}
			}
			catch
			{
				now = DateTime.Now;
			}
			return now;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void EndUpdate()
		{
			if (!this.tvBrowse.InvokeRequired)
			{
				this.tvBrowse.EndUpdate();
				return;
			}
			this.tvBrowse.Invoke(new frmOpenBookBrowse.EndUpdateDelegate(this.EndUpdate));
		}

		private void frmOpenBookBrowse_Load(object sender, EventArgs e)
		{
			try
			{
				this.tvBrowse.BeginUpdate();
				for (int i = 0; i < (int)Program.iniServers.Length; i++)
				{
					TreeNode treeNode = new TreeNode()
					{
						Text = Program.iniServers[i].items["SETTINGS", "DISPLAY_NAME"],
						Tag = i
					};
					if (treeNode.Text != string.Empty)
					{
						this.tvBrowse.Nodes.Add(treeNode);
					}
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
				base.Hide();
				MessageHandler.ShowQuestion(this.GetResource("(E-OBB-EM001) Failed to load specified object", "(E-OBB-EM001)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
				base.Show();
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
				if (base.Visible)
				{
					this.UpdateStatus();
				}
			}
			catch
			{
			}
		}

		private string GetBookInfoLanguage(string sKey, string sServerKey)
		{
			string str;
			bool flag = false;
			string str1 = string.Concat(Settings.Default.appLanguage, "_GSP_", sServerKey, ".ini");
			if (File.Exists(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1)))
			{
				TextReader streamReader = new StreamReader(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1));
				while (true)
				{
					string str2 = streamReader.ReadLine();
					string str3 = str2;
					if (str2 == null)
					{
						return sKey;
					}
					if (str3.ToUpper() == "[OPENBOOK]")
					{
						flag = true;
					}
					else if (str3.Contains("=") && flag)
					{
						string[] strArrays = new string[] { "=" };
						string[] strArrays1 = str3.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
						try
						{
							if (strArrays1[0].ToString().ToUpper() == sKey.ToUpper())
							{
								flag = false;
								str = strArrays1[1].ToString();
								break;
							}
						}
						catch
						{
							str = sKey;
							break;
						}
					}
					else if (str3.Contains("["))
					{
						flag = false;
					}
				}
				return str;
			}
			return sKey;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='OPEN_BOOKS']");
				str = string.Concat(str, "/Screen[@Name='BROWSE_BOOK']");
				if (rType != ResourceType.TITLE)
				{
					if (rType == ResourceType.LABEL)
					{
						str = string.Concat(str, "/Resources[@Name='LABEL']");
					}
					else if (rType == ResourceType.BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='BUTTON']");
					}
					else if (rType == ResourceType.CHECK_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='CHECK_BOX']");
					}
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
					}
					else if (rType == ResourceType.COMBO_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='COMBO_BOX']");
					}
					else if (rType == ResourceType.GRID_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='GRID_VIEW']");
					}
					else if (rType == ResourceType.LIST_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='LIST_VIEW']");
					}
					else if (rType == ResourceType.MENU_BAR)
					{
						str = string.Concat(str, "/Resources[@Name='MENU_BAR']");
					}
					else if (rType == ResourceType.RADIO_BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='RADIO_BUTTON']");
					}
					else if (rType == ResourceType.CONTEXT_MENU)
					{
						str = string.Concat(str, "/Resources[@Name='CONTEXT_MENU']");
					}
					else if (rType == ResourceType.TOOLSTRIP)
					{
						str = string.Concat(str, "/Resources[@Name='TOOLSTRIP']");
					}
					str = string.Concat(str, "/Resource[@Name='", sKey, "']");
					resourceValue = this.frmParent.frmParent.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.frmParent.frmParent.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern long HideCaret(IntPtr hwnd);

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				foreach (Control control in parentPanel.Controls)
				{
					if (control == this.picLoading)
					{
						continue;
					}
					control.Enabled = true;
				}
				this.picLoading.Hide();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmOpenBookBrowse));
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
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Panel1.BackColor = SystemColors.Control;
			this.pnlForm.Panel1.Controls.Add(this.pnltvBrowse);
			this.pnlForm.Panel1.Controls.Add(this.lblBrowse);
			this.pnlForm.Panel1MinSize = 270;
			this.pnlForm.Panel2.Controls.Add(this.pnlDetails);
			this.pnlForm.Panel2.Controls.Add(this.lblDetails);
			this.pnlForm.Panel2MinSize = 80;
			this.pnlForm.Size = new System.Drawing.Size(578, 409);
			this.pnlForm.SplitterDistance = 270;
			this.pnlForm.TabIndex = 10;
			this.pnltvBrowse.BackColor = Color.White;
			this.pnltvBrowse.Controls.Add(this.tvBrowse);
			this.pnltvBrowse.Dock = DockStyle.Fill;
			this.pnltvBrowse.Location = new Point(0, 27);
			this.pnltvBrowse.Name = "pnltvBrowse";
			this.pnltvBrowse.Padding = new System.Windows.Forms.Padding(5, 15, 0, 0);
			this.pnltvBrowse.Size = new System.Drawing.Size(268, 380);
			this.pnltvBrowse.TabIndex = 15;
			this.tvBrowse.BorderStyle = BorderStyle.None;
			this.tvBrowse.Cursor = Cursors.Default;
			this.tvBrowse.Dock = DockStyle.Fill;
			this.tvBrowse.DrawMode = TreeViewDrawMode.OwnerDrawText;
			this.tvBrowse.ForeColor = SystemColors.WindowText;
			this.tvBrowse.ItemHeight = 16;
			this.tvBrowse.Location = new Point(5, 15);
			this.tvBrowse.Name = "tvBrowse";
			this.tvBrowse.Size = new System.Drawing.Size(263, 365);
			this.tvBrowse.TabIndex = 11;
			this.tvBrowse.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.tvBrowse_NodeMouseDoubleClick);
			this.tvBrowse.AfterSelect += new TreeViewEventHandler(this.tvBrowse_AfterSelect);
			this.lblBrowse.BackColor = Color.White;
			this.lblBrowse.Dock = DockStyle.Top;
			this.lblBrowse.ForeColor = Color.Black;
			this.lblBrowse.Location = new Point(0, 0);
			this.lblBrowse.Name = "lblBrowse";
			this.lblBrowse.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblBrowse.Size = new System.Drawing.Size(268, 27);
			this.lblBrowse.TabIndex = 14;
			this.lblBrowse.Text = "Browse";
			this.pnlDetails.BackColor = Color.White;
			this.pnlDetails.Controls.Add(this.pnlBookInfo);
			this.pnlDetails.Controls.Add(this.pnlServerInfo);
			this.pnlDetails.Dock = DockStyle.Fill;
			this.pnlDetails.Location = new Point(0, 27);
			this.pnlDetails.Name = "pnlDetails";
			this.pnlDetails.Size = new System.Drawing.Size(302, 380);
			this.pnlDetails.TabIndex = 15;
			this.pnlBookInfo.BackColor = Color.White;
			this.pnlBookInfo.Controls.Add(this.pnlrtbBookInfo);
			this.pnlBookInfo.Controls.Add(this.lblBookInfo);
			this.pnlBookInfo.Dock = DockStyle.Fill;
			this.pnlBookInfo.Location = new Point(0, 101);
			this.pnlBookInfo.Name = "pnlBookInfo";
			this.pnlBookInfo.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
			this.pnlBookInfo.Size = new System.Drawing.Size(302, 279);
			this.pnlBookInfo.TabIndex = 14;
			this.pnlBookInfo.Tag = "";
			this.pnlrtbBookInfo.BackColor = Color.White;
			this.pnlrtbBookInfo.Controls.Add(this.rtbBookInfo);
			this.pnlrtbBookInfo.Dock = DockStyle.Fill;
			this.pnlrtbBookInfo.Location = new Point(15, 38);
			this.pnlrtbBookInfo.Name = "pnlrtbBookInfo";
			this.pnlrtbBookInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnlrtbBookInfo.Size = new System.Drawing.Size(287, 241);
			this.pnlrtbBookInfo.TabIndex = 15;
			this.rtbBookInfo.BackColor = Color.White;
			this.rtbBookInfo.BorderStyle = BorderStyle.None;
			this.rtbBookInfo.Dock = DockStyle.Fill;
			this.rtbBookInfo.Location = new Point(10, 0);
			this.rtbBookInfo.Name = "rtbBookInfo";
			this.rtbBookInfo.ReadOnly = true;
			this.rtbBookInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtbBookInfo.Size = new System.Drawing.Size(277, 241);
			this.rtbBookInfo.TabIndex = 12;
			this.rtbBookInfo.Text = "";
			this.rtbBookInfo.MouseDown += new MouseEventHandler(this.rtbBookInfo_MouseDown);
			this.lblBookInfo.BackColor = Color.Transparent;
			this.lblBookInfo.Cursor = Cursors.Hand;
			this.lblBookInfo.Dock = DockStyle.Top;
			this.lblBookInfo.ForeColor = Color.Blue;
			this.lblBookInfo.Image = Resources.GroupLine2;
			this.lblBookInfo.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblBookInfo.Location = new Point(15, 10);
			this.lblBookInfo.Name = "lblBookInfo";
			this.lblBookInfo.Size = new System.Drawing.Size(287, 28);
			this.lblBookInfo.TabIndex = 11;
			this.lblBookInfo.Tag = "";
			this.lblBookInfo.Text = "Book Information";
			this.lblBookInfo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblBookInfo.Click += new EventHandler(this.lblBookInfo_Click);
			this.pnlServerInfo.BackColor = Color.White;
			this.pnlServerInfo.Controls.Add(this.pnlrtbServerInfo);
			this.pnlServerInfo.Controls.Add(this.lblServerInfo);
			this.pnlServerInfo.Dock = DockStyle.Top;
			this.pnlServerInfo.Location = new Point(0, 0);
			this.pnlServerInfo.Name = "pnlServerInfo";
			this.pnlServerInfo.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
			this.pnlServerInfo.Size = new System.Drawing.Size(302, 101);
			this.pnlServerInfo.TabIndex = 13;
			this.pnlServerInfo.Tag = "";
			this.pnlrtbServerInfo.BackColor = Color.White;
			this.pnlrtbServerInfo.Controls.Add(this.rtbServerInfo);
			this.pnlrtbServerInfo.Dock = DockStyle.Fill;
			this.pnlrtbServerInfo.Location = new Point(15, 38);
			this.pnlrtbServerInfo.Name = "pnlrtbServerInfo";
			this.pnlrtbServerInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnlrtbServerInfo.Size = new System.Drawing.Size(287, 63);
			this.pnlrtbServerInfo.TabIndex = 14;
			this.rtbServerInfo.BackColor = Color.White;
			this.rtbServerInfo.BorderStyle = BorderStyle.None;
			this.rtbServerInfo.Dock = DockStyle.Fill;
			this.rtbServerInfo.Location = new Point(10, 0);
			this.rtbServerInfo.Name = "rtbServerInfo";
			this.rtbServerInfo.ReadOnly = true;
			this.rtbServerInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtbServerInfo.Size = new System.Drawing.Size(277, 63);
			this.rtbServerInfo.TabIndex = 12;
			this.rtbServerInfo.Text = "";
			this.rtbServerInfo.MouseDown += new MouseEventHandler(this.rtbServerInfo_MouseDown);
			this.lblServerInfo.BackColor = Color.Transparent;
			this.lblServerInfo.Cursor = Cursors.Hand;
			this.lblServerInfo.Dock = DockStyle.Top;
			this.lblServerInfo.ForeColor = Color.Blue;
			this.lblServerInfo.Image = (Image)componentResourceManager.GetObject("lblServerInfo.Image");
			this.lblServerInfo.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblServerInfo.Location = new Point(15, 10);
			this.lblServerInfo.Name = "lblServerInfo";
			this.lblServerInfo.Size = new System.Drawing.Size(287, 28);
			this.lblServerInfo.TabIndex = 11;
			this.lblServerInfo.Tag = "";
			this.lblServerInfo.Text = "Server Information";
			this.lblServerInfo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblServerInfo.Click += new EventHandler(this.lblServerInfo_Click);
			this.lblDetails.BackColor = Color.White;
			this.lblDetails.Dock = DockStyle.Top;
			this.lblDetails.ForeColor = Color.Black;
			this.lblDetails.Location = new Point(0, 0);
			this.lblDetails.Name = "lblDetails";
			this.lblDetails.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblDetails.Size = new System.Drawing.Size(302, 27);
			this.lblDetails.TabIndex = 14;
			this.lblDetails.Text = "Details";
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			this.picLoading.BackColor = Color.Transparent;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(4, 4);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(34, 34);
			this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
			this.picLoading.TabIndex = 17;
			this.picLoading.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(578, 409);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.picLoading);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmOpenBookBrowse";
			this.Text = "frmOpenBookBrowse";
			base.Load += new EventHandler(this.frmOpenBookBrowse_Load);
			base.VisibleChanged += new EventHandler(this.frmOpenBookBrowse_VisibleChanged);
			this.pnlForm.Panel1.ResumeLayout(false);
			this.pnlForm.Panel2.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnltvBrowse.ResumeLayout(false);
			this.pnlDetails.ResumeLayout(false);
			this.pnlBookInfo.ResumeLayout(false);
			this.pnlrtbBookInfo.ResumeLayout(false);
			this.pnlServerInfo.ResumeLayout(false);
			this.pnlrtbServerInfo.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private bool IntervalElapsed(DateTime dtLocal, DateTime dtServer)
		{
			bool flag;
			try
			{
				int num = int.Parse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"]);
				flag = ((dtServer.Date - dtLocal.Date).TotalDays < (double)num ? false : true);
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		private void lblBookInfo_Click(object sender, EventArgs e)
		{
			if (this.pnlrtbBookInfo.Visible)
			{
				this.pnlrtbBookInfo.Visible = false;
				this.pnlBookInfo.Height = this.pnlBookInfo.Height - this.pnlrtbBookInfo.Height;
				this.lblBookInfo.Image = Resources.GroupLine3;
				return;
			}
			this.pnlBookInfo.Height = this.pnlBookInfo.Height + this.pnlrtbBookInfo.Height;
			this.pnlrtbBookInfo.Visible = true;
			this.lblBookInfo.Image = Resources.GroupLine2;
		}

		private void lblServerInfo_Click(object sender, EventArgs e)
		{
			if (this.pnlrtbServerInfo.Visible)
			{
				this.pnlrtbServerInfo.Visible = false;
				this.pnlServerInfo.Height = this.pnlServerInfo.Height - this.pnlrtbServerInfo.Height;
				this.lblServerInfo.Image = Resources.GroupLine3;
				return;
			}
			this.pnlServerInfo.Height = this.pnlServerInfo.Height + this.pnlrtbServerInfo.Height;
			this.pnlrtbServerInfo.Visible = true;
			this.lblServerInfo.Image = Resources.GroupLine2;
		}

		private void LoadResources()
		{
			this.lblBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.LABEL);
			this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
			this.lblServerInfo.Text = this.GetResource("Server Information", "SERVER_INFORMATION", ResourceType.LABEL);
			this.lblBookInfo.Text = this.GetResource("Book Information", "BOOK_INFORMATION", ResourceType.LABEL);
		}

		private bool LoadSeries(string sFilePath, TreeNode objTreeNode)
		{
			bool flag;
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(sFilePath))
			{
				return false;
			}
			if (!this.p_Compressed)
			{
				try
				{
					xmlDocument.Load(sFilePath);
					goto Label0;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
			else
			{
				try
				{
					string str = sFilePath.ToLower().Replace(".zip", ".xml");
					Global.Unzip(sFilePath);
					if (File.Exists(str))
					{
						xmlDocument.Load(str);
					}
				}
				catch
				{
				}
			}
		Label0:
			if (this.p_Encrypted)
			{
				try
				{
					string str1 = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
					xmlDocument.DocumentElement.InnerXml = str1;
				}
				catch
				{
				}
			}
			XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
			if (xmlNodes == null)
			{
				return false;
			}
			string name = "";
			ArrayList arrayLists = new ArrayList();
			string name1 = "";
			TreeNode item = null;
			TreeNode treeNode = null;
			foreach (XmlAttribute attribute in xmlNodes.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("ID"))
				{
					name = attribute.Name;
				}
				else if (!attribute.Value.ToUpper().StartsWith("LEVEL"))
				{
					if (!attribute.Value.ToUpper().Equals("PUBLISHINGID"))
					{
						continue;
					}
					name1 = attribute.Name;
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (name == "" || arrayLists.Count == 0 || name1 == "")
			{
				return false;
			}
			objTreeNode.Tag = string.Concat(objTreeNode.Tag, "::", xmlNodes.OuterXml);
			XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//Books/Book");
			xmlNodeLists = this.frmParent.FilterBooksList(xmlNodes, xmlNodeLists);
			this.BeginUpdate();
			foreach (XmlNode xmlNodes1 in xmlNodeLists)
			{
				item = objTreeNode;
				if (xmlNodes1.Attributes[name] == null || xmlNodes1.Attributes[name1] == null)
				{
					continue;
				}
				for (int i = 0; i < arrayLists.Count; i++)
				{
					if (xmlNodes1.Attributes[arrayLists[i].ToString()] != null)
					{
						treeNode = new TreeNode()
						{
							Text = xmlNodes1.Attributes[arrayLists[i].ToString()].Value.Replace("&", "&&"),
							Name = xmlNodes1.Attributes[arrayLists[i].ToString()].Value
						};
						if (!item.Nodes.ContainsKey(treeNode.Name))
						{
							this.AddNode(item, treeNode);
							item = item.Nodes[treeNode.Name];
						}
						else
						{
							item = item.Nodes[treeNode.Name];
						}
					}
				}
				if (xmlNodes1.Attributes[name1] == null)
				{
					continue;
				}
				FileInfo fileInfo = new FileInfo(sFilePath);
				string empty = string.Empty;
				string[] directoryName = new string[] { empty, fileInfo.DirectoryName, "\\", xmlNodes1.Attributes[name1].Value, "\\", xmlNodes1.Attributes[name1].Value, ".xml" };
				if (File.Exists(string.Concat(directoryName)) || !Program.objAppFeatures.bDcMode)
				{
					treeNode = new TreeNode()
					{
						Text = xmlNodes1.Attributes[name1].Value.Replace("&", "&&"),
						Name = xmlNodes1.Attributes[name].Value,
						Tag = xmlNodes1.OuterXml
					};
					this.AddNode(item, treeNode);
				}
				else
				{
					if (item.Nodes.Count >= 1 || item.Parent == null)
					{
						continue;
					}
					this.RemoveNode(item);
				}
			}
			this.EndUpdate();
			objTreeNode = item;
			if (objTreeNode.Nodes.Count == 0 && objTreeNode.Tag != null)
			{
				objTreeNode.Tag = objTreeNode.Tag.ToString().Substring(0, objTreeNode.Tag.ToString().IndexOf("::"));
			}
			return true;
		}

		private void RemoveNode(TreeNode tnNode)
		{
			if (this.tvBrowse.InvokeRequired)
			{
				CustomTreeView customTreeView = this.tvBrowse;
				frmOpenBookBrowse.RemoveNodeDelegate removeNodeDelegate = new frmOpenBookBrowse.RemoveNodeDelegate(this.RemoveNode);
				object[] objArray = new object[] { tnNode };
				customTreeView.Invoke(removeNodeDelegate, objArray);
				return;
			}
			if (tnNode.Parent.Nodes.Count >= 2)
			{
				tnNode.Remove();
				return;
			}
			while (tnNode.Parent != null && tnNode.Parent.Nodes.Count < 2)
			{
				tnNode = tnNode.Parent;
			}
			if (tnNode.Parent != null)
			{
				tnNode.Remove();
				return;
			}
			tnNode.Nodes.Clear();
		}

		private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
		{
			frmOpenBookBrowse.HideCaret(this.rtbBookInfo.Handle);
		}

		private void rtbServerInfo_MouseDown(object sender, MouseEventArgs e)
		{
			frmOpenBookBrowse.HideCaret(this.rtbServerInfo.Handle);
		}

		private void SelectTreeNode()
		{
			try
			{
				if (this.tvBrowse.SelectedNode.Level == 0 && this.tvBrowse.SelectedNode.Nodes.Count == 0)
				{
					string empty = string.Empty;
					string item = string.Empty;
					try
					{
						int num = -1;
						try
						{
							if (this.tvBrowse.SelectedNode.Tag.ToString().Contains("::"))
							{
								this.tvBrowse.SelectedNode.Tag = this.tvBrowse.SelectedNode.Tag.ToString().Substring(0, this.tvBrowse.SelectedNode.Tag.ToString().IndexOf("::"));
							}
							num = int.Parse(this.tvBrowse.SelectedNode.Tag.ToString());
						}
						catch
						{
							return;
						}
						empty = Program.iniServers[num].items["SETTINGS", "CONTENT_PATH"];
						if (!empty.EndsWith("/"))
						{
							empty = string.Concat(empty, "/");
						}
						item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
						item = string.Concat(item, "\\", Program.iniServers[num].sIniKey);
						if (!Directory.Exists(item))
						{
							Directory.CreateDirectory(item);
						}
					}
					catch
					{
						MessageHandler.ShowError(this.GetResource("(E-OBB-EM002) Failed to create file/folder specified", "(E-OBB-EM002)_FAILED_FOLDER", ResourceType.POPUP_MESSAGE));
					}
					if (Program.objAppMode.bWorkingOffline)
					{
						item = string.Concat(item, "\\Series.xml");
						if (File.Exists(item))
						{
							if (this.LoadSeries(item, this.tvBrowse.SelectedNode))
							{
								this.tvBrowse.SelectedNode.Expand();
							}
							if (this.tvBrowse.SelectedNode.Level == 0 && this.tvBrowse.SelectedNode.Nodes.Count == 0)
							{
								this.tvBrowse.SelectedNode.Nodes.Add("(Not Found)");
								this.tvBrowse.SelectedNode.Expand();
							}
						}
						else
						{
							this.tvBrowse.SelectedNode.Nodes.Add("(Not Found)");
							this.tvBrowse.SelectedNode.Expand();
						}
					}
					else
					{
						this.ShowLoading(this.pnltvBrowse);
						BackgroundWorker backgroundWorker = this.bgWorker;
						object[] selectedNode = new object[] { empty, item, this.tvBrowse.SelectedNode };
						backgroundWorker.RunWorkerAsync(selectedNode);
					}
				}
				if (this.tvBrowse.SelectedNode.Nodes.Count == 0 && this.tvBrowse.SelectedNode.Tag.ToString() != "" && this.tvBrowse.SelectedNode.Level > 0)
				{
					if (Program.objAppFeatures.bDcMode)
					{
						string str = string.Empty;
						str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
						str = string.Concat(str, "\\", Program.iniServers[this.p_ServerId].sIniKey);
						string str1 = str;
						string[] pBookId = new string[] { str1, "\\", this.p_BookId, "\\", this.p_BookId, ".xml" };
						if (!File.Exists(string.Concat(pBookId)))
						{
							if (!this.tvBrowse.SelectedNode.Text.EndsWith("(Not Found)"))
							{
								this.tvBrowse.SelectedNode.Text = string.Concat(this.tvBrowse.SelectedNode.Text, "(Not Found)");
							}
							return;
						}
					}
					this.frmParent.CloseAndLoadData(this.p_ServerId, this.p_BookId, this.p_BookNode, this.p_SchemaNode, this.p_BookType);
				}
			}
			catch
			{
			}
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				foreach (Control control in parentPanel.Controls)
				{
					if (control == this.picLoading)
					{
						continue;
					}
					control.Enabled = false;
				}
				this.picLoading.Left = parentPanel.Left + parentPanel.Width / 2 - this.picLoading.Width / 2;
				this.picLoading.Top = parentPanel.Top + parentPanel.Height / 2 - this.picLoading.Height / 2;
				this.picLoading.Parent = parentPanel;
				this.picLoading.BringToFront();
				this.picLoading.Show();
			}
			catch
			{
			}
		}

		private void tvBrowse_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Text == "(Not Found)")
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			bool flag = false;
			this.rtbServerInfo.SelectionFont = this.rtbServerInfo.Font;
			this.rtbBookInfo.SelectionFont = this.rtbBookInfo.Font;
			int num = 0;
			string str = "";
			TreeNode selectedNode = this.tvBrowse.SelectedNode;
			while (selectedNode.Level > 0)
			{
				selectedNode = selectedNode.Parent;
			}
			str = selectedNode.Tag.ToString();
			if (str.Contains("::"))
			{
				str = str.Substring(0, str.IndexOf("::"));
			}
			try
			{
				num = int.Parse(str);
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
			if (Program.iniServers[num].items["SETTINGS", "DATA_ENCRYPTION"] == null)
			{
				this.p_Encrypted = false;
			}
			else if (Program.iniServers[num].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() != "ON")
			{
				this.p_Encrypted = false;
			}
			else
			{
				this.p_Encrypted = true;
			}
			if (Program.iniServers[num].items["SETTINGS", "DATA_COMPRESSION"] == null)
			{
				this.p_Compressed = false;
			}
			else if (Program.iniServers[num].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() != "ON")
			{
				this.p_Compressed = false;
			}
			else
			{
				this.p_Compressed = true;
			}
			this.rtbServerInfo.Clear();
			this.rtbServerInfo.SelectionColor = Color.Gray;
			this.rtbServerInfo.SelectedText = string.Concat(this.GetResource("Server Key", "SERVER_KEY", ResourceType.LABEL), ": ");
			this.rtbServerInfo.SelectionColor = Color.Black;
			this.rtbServerInfo.SelectedText = string.Concat(Program.iniServers[num].sIniKey, " \n");
			this.rtbServerInfo.SelectionColor = Color.Gray;
			this.rtbBookInfo.Clear();
			this.rtbBookInfo.SelectionColor = Color.Gray;
			this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
			if (this.tvBrowse.SelectedNode.Nodes.Count == 0 && this.tvBrowse.SelectedNode.Tag.ToString() != "" && selectedNode.Tag.ToString().Contains("::"))
			{
				string empty = string.Empty;
				string value = string.Empty;
				try
				{
					str = selectedNode.Tag.ToString();
					str = str.Substring(str.IndexOf("::") + 2, str.Length - (str.IndexOf("::") + 2));
					XmlNode xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(str)));
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.tvBrowse.SelectedNode.Tag.ToString()));
					XmlNode xmlNodes1 = xmlDocument.ReadNode(xmlTextReader);
					this.rtbBookInfo.Clear();
					for (int i = 0; i < xmlNodes.Attributes.Count; i++)
					{
						if (!xmlNodes.Attributes[i].Value.ToUpper().StartsWith("LEVEL") && xmlNodes1.Attributes[xmlNodes.Attributes[i].Name] != null && xmlNodes.Attributes[i].Value.ToUpper() != "BOOKCODE" && xmlNodes.Attributes[i].Value.ToUpper() != "SECURITYLOCKS" && xmlNodes.Attributes[i].Value.ToUpper() != "ID")
						{
							this.rtbBookInfo.SelectionColor = Color.Gray;
							this.rtbBookInfo.SelectedText = string.Concat(this.GetBookInfoLanguage(xmlNodes.Attributes[i].Value, Program.iniServers[num].sIniKey), ": ");
							this.rtbBookInfo.SelectionColor = Color.Black;
							this.rtbBookInfo.SelectedText = string.Concat(xmlNodes1.Attributes[xmlNodes.Attributes[i].Name].Value, "\n");
						}
						if (xmlNodes.Attributes[i].Value.ToUpper().StartsWith("PUBLISHINGID"))
						{
							empty = xmlNodes1.Attributes[xmlNodes.Attributes[i].Name].Value;
						}
						if (xmlNodes.Attributes[i].Value.ToUpper().StartsWith("BOOKTYPE"))
						{
							if (xmlNodes1.Attributes[xmlNodes.Attributes[i].Name] != null)
							{
								value = xmlNodes1.Attributes[xmlNodes.Attributes[i].Name].Value;
								flag = true;
							}
							else
							{
								this.rtbBookInfo.Clear();
								this.rtbBookInfo.SelectionColor = Color.Red;
								this.rtbBookInfo.SelectedText = this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_INFORMATION_NOEXIST", ResourceType.LABEL);
								break;
							}
						}
					}
					if (flag)
					{
						this.p_ServerId = num;
						this.p_BookId = empty;
						this.p_BookNode = xmlNodes1;
						this.p_SchemaNode = xmlNodes;
						this.p_BookType = value;
					}
				}
				catch
				{
					if (this.tvBrowse.SelectedNode.Level != 0 || this.tvBrowse.SelectedNode.Nodes.Count != 0 || !Program.objAppFeatures.bDcMode)
					{
						this.rtbBookInfo.Clear();
						this.rtbBookInfo.SelectionColor = Color.Red;
						this.rtbBookInfo.SelectedText = this.GetResource("(E-OBM-EM003) Failed to load specified object", "(E-OBM-EM003)_FAILED_LOAD", ResourceType.LABEL);
					}
				}
			}
		}

		private void tvBrowse_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			if (this.tvBrowse.SelectedNode == null)
			{
				return;
			}
			if (e.Node.Text == "(Not Found)")
			{
				return;
			}
			this.SelectTreeNode();
		}

		public void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblBrowse.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.lblDetails.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}

		private void UpdateStatus()
		{
			if (this == this.frmParent.GetCurrentChildForm())
			{
				if (this.frmParent.InvokeRequired)
				{
					frmOpenBook _frmOpenBook = this.frmParent;
					frmOpenBookBrowse.StatusDelegate statusDelegate = new frmOpenBookBrowse.StatusDelegate(this.frmParent.UpdateStatus);
					object[] objArray = new object[] { this.statusText };
					_frmOpenBook.Invoke(statusDelegate, objArray);
					return;
				}
				this.frmParent.UpdateStatus(this.statusText);
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

		private delegate void AddNodeDelegate(TreeNode tnParent, TreeNode tnChild);

		private delegate void BeginUpdateDelegate();

		private delegate void EndUpdateDelegate();

		private delegate void RemoveNodeDelegate(TreeNode tnNode);

		private delegate void StatusDelegate(string status);
	}
}
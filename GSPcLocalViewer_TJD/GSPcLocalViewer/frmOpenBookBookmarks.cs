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

		public frmOpenBookBookmarks(frmOpenBook frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.statusText = this.GetResource("Open Bookmarks by browsing", "OPEN_BOOKMARKS_BROWSING", ResourceType.LABEL);
			this.p_BookId = string.Empty;
			this.p_ServerId = 0;
			this.p_BookNode = null;
			this.p_SchemaNode = null;
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
			frmOpenBookBookmarks.AddNodeDelegate addNodeDelegate = new frmOpenBookBookmarks.AddNodeDelegate(this.AddNode);
			object[] objArray = new object[] { tnParent, tnChild };
			customTreeView.Invoke(addNodeDelegate, objArray);
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			object[] argument = (object[])e.Argument;
			string str = (string)argument[0];
			TreeNode treeNode = (TreeNode)argument[1];
			this.statusText = this.GetResource("Loading bookmarks", "LOADING_BOOKMARKS", ResourceType.STATUS_MESSAGE);
			this.UpdateStatus();
			if (!this.frmParent.IsDisposed)
			{
				this.statusText = this.GetResource("Loading bookmarks", "LOADING_BOOKMARKS", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				if (this.LoadFavouriteBooks(str, treeNode))
				{
					e.Result = "ok";
					return;
				}
				e.Result = this.GetResource("Loading bookmarks", "(E-OBM-EM004)_FAILED_LOAD", ResourceType.POPUP_MESSAGE);
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			string result = (string)e.Result;
			if (!this.frmParent.IsDisposed)
			{
				if (!result.Equals("ok"))
				{
					this.statusText = result;
					this.UpdateStatus();
					MessageHandler.ShowInformation(result);
				}
				else
				{
					this.statusText = this.GetResource("Bookmarks loaded completely", "BOOKMARKS_LOADED_COMPLETELY", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					this.tvBrowse.SelectedNode.Expand();
				}
				if (this.tvBrowse.SelectedNode.Nodes.Count == 0)
				{
					TreeNode treeNode = new TreeNode()
					{
						Text = "NIL",
						Name = "NIL",
						Tag = "NIL"
					};
					this.tvBrowse.SelectedNode.Nodes.Add(treeNode);
					this.tvBrowse.SelectedNode.Expand();
				}
				this.HideLoading(this.pnltvBrowse);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmOpenFavourites_Load(object sender, EventArgs e)
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
				MessageHandler.ShowQuestion(this.GetResource("(E-OBM-EM001) Failed to load specified object", "(E-OBM-EM001)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
				base.Show();
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
				str = string.Concat(str, "/Screen[@Name='OPENBOOKBOOKMARKS']");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmOpenBookBookmarks));
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
			this.pnlForm.Panel2MinSize = 75;
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
			this.pnlBookInfo.Controls.Add(this.lblBookmarksInfo);
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
			this.lblBookmarksInfo.BackColor = Color.Transparent;
			this.lblBookmarksInfo.Cursor = Cursors.Hand;
			this.lblBookmarksInfo.Dock = DockStyle.Top;
			this.lblBookmarksInfo.ForeColor = Color.Blue;
			this.lblBookmarksInfo.Image = Resources.GroupLine2;
			this.lblBookmarksInfo.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblBookmarksInfo.Location = new Point(15, 10);
			this.lblBookmarksInfo.Name = "lblBookmarksInfo";
			this.lblBookmarksInfo.Size = new System.Drawing.Size(287, 28);
			this.lblBookmarksInfo.TabIndex = 11;
			this.lblBookmarksInfo.Tag = "";
			this.lblBookmarksInfo.Text = "Bookmarks Information";
			this.lblBookmarksInfo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblBookmarksInfo.Click += new EventHandler(this.lblBookmarksInfo_Click);
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
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.TabIndex = 17;
			this.picLoading.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(578, 409);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.picLoading);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmOpenBookBookmarks";
			this.Text = "frmOpenFavourites";
			base.Load += new EventHandler(this.frmOpenFavourites_Load);
			base.VisibleChanged += new EventHandler(this.frmOpenFavourites_VisibleChanged);
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
		}

		private void lblBookmarksInfo_Click(object sender, EventArgs e)
		{
			if (this.pnlrtbBookInfo.Visible)
			{
				this.pnlrtbBookInfo.Visible = false;
				this.pnlBookInfo.Height = this.pnlBookInfo.Height - this.pnlrtbBookInfo.Height;
				this.lblBookmarksInfo.Image = Resources.GroupLine3;
				return;
			}
			this.pnlBookInfo.Height = this.pnlBookInfo.Height + this.pnlrtbBookInfo.Height;
			this.pnlrtbBookInfo.Visible = true;
			this.lblBookmarksInfo.Image = Resources.GroupLine2;
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

		private bool LoadFavouriteBooks(string sFilePath, TreeNode objTreeNode)
		{
			bool flag;
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(sFilePath))
			{
				return false;
			}
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
		Label0:
			XmlNode xmlNodes = xmlDocument.SelectSingleNode("//BookMarks");
			TreeNode treeNode = null;
			TreeNode treeNode1 = null;
			objTreeNode.Tag = string.Concat(objTreeNode.Tag, "::", xmlNodes.OuterXml);
			XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("/BookMarks/BookMark[not(@BookId=preceding-sibling::BookMark/@BookId)]/@BookId");
			string empty = string.Empty;
			foreach (XmlNode xmlNodes1 in xmlNodeLists)
			{
				if (empty.Contains(string.Concat("||", xmlNodes1.Value.ToUpper(), "||")))
				{
					continue;
				}
				empty = string.Concat(empty, "||", xmlNodes1.Value.ToUpper(), "||");
				treeNode = objTreeNode;
				treeNode1 = new TreeNode()
				{
					Text = xmlNodes1.Value.Replace("&", "&&"),
					Name = xmlNodes1.Value,
					Tag = xmlNodes1.OuterXml
				};
				this.LoadFavouritePages(xmlDocument, xmlNodes1.Value, treeNode1);
				this.AddNode(treeNode, treeNode1);
			}
			objTreeNode = treeNode;
			if (objTreeNode == null)
			{
				try
				{
					File.Delete(sFilePath);
				}
				catch
				{
				}
			}
			else if (objTreeNode.Nodes.Count == 0)
			{
				objTreeNode.Tag = objTreeNode.Tag.ToString().Substring(0, objTreeNode.Tag.ToString().IndexOf("::"));
			}
			return true;
		}

		private void LoadFavouritePages(XmlDocument objXmlDoc, string objBookID, TreeNode objTreeNode)
		{
			try
			{
				XmlNodeList xmlNodeLists = objXmlDoc.SelectNodes(string.Concat("//BookMarks/BookMark[translate(@BookId, 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", objBookID.ToUpper(), "']"));
				TreeNode treeNode = null;
				TreeNode treeNode1 = null;
				foreach (XmlNode xmlNodes in xmlNodeLists)
				{
					treeNode = objTreeNode;
					treeNode1 = new TreeNode()
					{
						Text = xmlNodes.Attributes["PageName"].Value.Replace("&", "&&"),
						Name = xmlNodes.Attributes["PageName"].Value,
						Tag = xmlNodes.OuterXml
					};
					this.AddNode(treeNode, treeNode1);
				}
			}
			catch
			{
			}
		}

		public void LoadResources()
		{
			this.lblBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.LABEL);
			this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
			this.lblServerInfo.Text = this.GetResource("Server Information", "SERVER_INFORMATION", ResourceType.LABEL);
			this.lblBookmarksInfo.Text = this.GetResource("Bookmarks Information", "BOOKMARKS_INFORMATION", ResourceType.LABEL);
		}

		private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
		{
			frmOpenBookBookmarks.HideCaret(this.rtbBookInfo.Handle);
		}

		private void rtbServerInfo_MouseDown(object sender, MouseEventArgs e)
		{
			frmOpenBookBookmarks.HideCaret(this.rtbServerInfo.Handle);
		}

		private void SelectTreeNode()
		{
			try
			{
				if (this.tvBrowse.SelectedNode.Level == 0 && this.tvBrowse.SelectedNode.Nodes.Count == 0)
				{
					string empty = string.Empty;
					string str = string.Empty;
					try
					{
						int num = int.Parse(this.tvBrowse.SelectedNode.Tag.ToString());
						empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
						empty = string.Concat(empty, "\\", Application.ProductName);
						empty = string.Concat(empty, "\\", Program.iniServers[num].sIniKey);
						empty = string.Concat(empty, "\\BookMarks.xml");
					}
					catch
					{
					}
					if (File.Exists(empty))
					{
						this.ShowLoading(this.pnltvBrowse);
						BackgroundWorker backgroundWorker = this.bgWorker;
						object[] selectedNode = new object[] { empty, this.tvBrowse.SelectedNode };
						backgroundWorker.RunWorkerAsync(selectedNode);
					}
					else if (this.tvBrowse.SelectedNode.Nodes.Count == 0)
					{
						TreeNode treeNode = new TreeNode()
						{
							Text = "NIL",
							Name = "NIL",
							Tag = "NIL"
						};
						this.tvBrowse.SelectedNode.Nodes.Add(treeNode);
						this.tvBrowse.SelectedNode.Expand();
					}
				}
				if (this.tvBrowse.SelectedNode.Nodes.Count == 0 && this.tvBrowse.SelectedNode.Tag.ToString() != "" && this.tvBrowse.SelectedNode.Level == 2 && this.tvBrowse.SelectedNode.Text != "NIL")
				{
					XmlDocument xmlDocument = new XmlDocument();
					string empty1 = string.Empty;
					empty1 = this.tvBrowse.SelectedNode.Tag.ToString();
					XmlNode xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(empty1)));
					this.frmParent.CloseAndLoadFavourite(xmlNodes);
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
			XmlDocument xmlDocument = new XmlDocument();
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
				this.rtbServerInfo.SelectedText = this.GetResource("(E-OBM-EM002) Failed to load specified object", "(E-OBM-EM002)_FAILED_LOAD", ResourceType.LABEL);
				this.rtbBookInfo.Clear();
				this.rtbBookInfo.SelectionColor = Color.Red;
				this.rtbBookInfo.SelectedText = this.GetResource("(E-OBM-EM003) Failed to load specified object", "(E-OBM-EM003)_FAILED_LOAD", ResourceType.LABEL);
				return;
			}
			this.rtbServerInfo.Clear();
			this.rtbServerInfo.SelectionColor = Color.Gray;
			this.rtbServerInfo.SelectedText = string.Concat(this.GetResource("Server Key", "SERVER_KEY", ResourceType.LABEL), ": ");
			this.rtbServerInfo.SelectionColor = Color.Black;
			this.rtbServerInfo.SelectedText = string.Concat(Program.iniServers[num].sIniKey, " \n");
			this.rtbServerInfo.SelectionColor = Color.Gray;
			this.rtbBookInfo.Clear();
			this.rtbBookInfo.SelectionColor = Color.Gray;
			this.rtbBookInfo.SelectedText = this.GetResource("Select a bookmark", "SELECT_A_BOOKMARK", ResourceType.LABEL);
			if (this.tvBrowse.SelectedNode.Nodes.Count == 0 && this.tvBrowse.SelectedNode.Tag.ToString() != "" && this.tvBrowse.SelectedNode.Tag.ToString().ToUpper() != "NIL" && selectedNode.Tag.ToString().Contains("::"))
			{
				string empty = string.Empty;
				try
				{
					str = selectedNode.Tag.ToString();
					str = str.Substring(str.IndexOf("::") + 2, str.Length - (str.IndexOf("::") + 2));
					XmlNode xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(str)));
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.tvBrowse.SelectedNode.Tag.ToString()));
					XmlNode xmlNodes1 = xmlDocument.ReadNode(xmlTextReader);
					this.rtbBookInfo.Clear();
					this.rtbBookInfo.SelectionColor = Color.Gray;
					this.rtbBookInfo.SelectedText = string.Concat(this.GetBookInfoLanguage("PublishingID", Program.iniServers[num].sIniKey), ": ");
					this.rtbBookInfo.SelectionColor = Color.Black;
					this.rtbBookInfo.SelectedText = string.Concat(xmlNodes1.Attributes["BookId"].Value, "\n");
					this.rtbBookInfo.SelectionColor = Color.Gray;
					this.rtbBookInfo.SelectedText = string.Concat(this.GetBookInfoLanguage("PageName", Program.iniServers[num].sIniKey), ": ");
					this.rtbBookInfo.SelectionColor = Color.Black;
					this.rtbBookInfo.SelectedText = string.Concat(xmlNodes1.Attributes["PageName"].Value, "\n");
					this.rtbBookInfo.SelectionColor = Color.Gray;
					this.rtbBookInfo.SelectedText = string.Concat(this.GetBookInfoLanguage("PicIndex", Program.iniServers[num].sIniKey), ": ");
					this.rtbBookInfo.SelectionColor = Color.Black;
					this.rtbBookInfo.SelectedText = string.Concat(xmlNodes1.Attributes["PicIndex"].Value, "\n");
					this.rtbBookInfo.SelectionColor = Color.Gray;
					this.rtbBookInfo.SelectedText = string.Concat(this.GetBookInfoLanguage("ListIndex", Program.iniServers[num].sIniKey), ": ");
					this.rtbBookInfo.SelectionColor = Color.Black;
					this.rtbBookInfo.SelectedText = string.Concat(xmlNodes1.Attributes["ListIndex"].Value, "\n");
					if (!xmlNodes1.Attributes["PartNo"].Value.Equals(string.Empty))
					{
						this.rtbBookInfo.SelectionColor = Color.Gray;
						this.rtbBookInfo.SelectedText = string.Concat(this.GetBookInfoLanguage("PartNumber", Program.iniServers[num].sIniKey), ": ");
						this.rtbBookInfo.SelectionColor = Color.Black;
						this.rtbBookInfo.SelectedText = string.Concat(xmlNodes1.Attributes["PartNo"].Value, "\n");
					}
					if (xmlNodes1.Attributes["BookId"].Value != null)
					{
						empty = xmlNodes1.Attributes["BookId"].Value;
					}
					this.p_ServerId = num;
					this.p_BookId = empty;
					this.p_BookNode = xmlNodes1;
					this.p_SchemaNode = xmlNodes;
				}
				catch
				{
					this.rtbBookInfo.Clear();
					this.rtbBookInfo.SelectionColor = Color.Red;
					this.rtbBookInfo.SelectedText = this.GetResource("(E-OBM-EM003) Failed to load specified object", "(E-OBM-EM003)_FAILED_LOAD", ResourceType.LABEL);
				}
			}
		}

		private void tvBrowse_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
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
					frmOpenBookBookmarks.StatusDelegate statusDelegate = new frmOpenBookBookmarks.StatusDelegate(this.frmParent.UpdateStatus);
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
				frmOpenBookBookmarks.HideCaret(this.rtbBookInfo.Handle);
				frmOpenBookBookmarks.HideCaret(this.rtbServerInfo.Handle);
			}
			catch
			{
			}
		}

		private delegate void AddNodeDelegate(TreeNode tnParent, TreeNode tnChild);

		private delegate void StatusDelegate(string status);
	}
}
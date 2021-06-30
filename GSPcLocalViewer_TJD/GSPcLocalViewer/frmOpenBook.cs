using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmOpenBook : Form
	{
		public frmOpenBookTasks objFrmTasks;

		public frmOpenBookBrowse objFrmBrowse;

		public frmOpenBookBookmarks objFrmBookmark;

		public frmOpenBookSearch objFrmSearch;

		public frmPageNameAdvSrch objFrmPageNameAdvSrch;

		public frmPartNameAdvSrch objFrmPartNameAdvSrch;

		public frmPartNumberAdvSrch objFrmPartNumberAdvSrch;

		public frmViewer frmParent;

		private string language;

		private IContainer components;

		private ToolStripContainer toolStripContainer1;

		private StatusStrip ssStatus;

		private ToolStrip toolStrip1;

		private ToolStripButton tsbBrowse;

		private ToolStripButton tsbBookmarks;

		private ToolStripStatusLabel lblStatus;

		private Panel pnlContents;

		private Panel pnlTasks;

		private ToolStripButton tsbSearch;

		private ToolStrip toolStrip2;

		private ToolStripButton toolStripButton4;

		private ToolStripButton toolStripButton5;

		private ToolStripButton toolStripButton6;

		private ToolStripButton toolStripButton7;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton tsbConnection;

		private ToolStripButton tsbDataCleanup;

		private ToolStripButton tsbSearchPageName;

		private ToolStripButton tsbSearchPartName;

		private ToolStripButton tsbSearchPartNumber;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton tsbSearchText;

		public frmOpenBook(frmViewer frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.language = Settings.Default.appLanguage;
			this.Font = Settings.Default.appFont;
		}

		public void CloseAndLoadData(int serverId, string bookPublishingId, XmlNode bookNode, XmlNode schemaNode, string bookType)
		{
			if (Global.SecurityLocksOpen(bookNode, schemaNode, serverId, this.frmParent))
			{
				base.Close();
				if (!this.frmParent.Enabled)
				{
					this.frmParent.Enabled = true;
				}
				this.frmParent.LoadData(serverId, bookPublishingId, bookNode, schemaNode, bookType);
			}
		}

		public void CloseAndLoadFavourite(XmlNode objXmlNode)
		{
			base.Close();
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			this.frmParent.OpenBookmarkPage(objXmlNode);
		}

		public void CloseAndLoadSearch(string sServerKey, string sBookCode, string sPageId, string sPicIndex, string sListIndex, string sPartNumber)
		{
			base.Close();
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			this.frmParent.OpenSearchResults(sServerKey, sBookCode, sPageId, sPicIndex, sListIndex, sPartNumber);
		}

		private void CreateForms()
		{
			this.objFrmBrowse = new frmOpenBookBrowse(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmBrowse);
			this.objFrmBookmark = new frmOpenBookBookmarks(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmBookmark);
			this.objFrmSearch = new frmOpenBookSearch(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmSearch);
			this.objFrmPageNameAdvSrch = new frmPageNameAdvSrch(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmPageNameAdvSrch);
			this.objFrmPartNameAdvSrch = new frmPartNameAdvSrch(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmPartNameAdvSrch);
			this.objFrmPartNumberAdvSrch = new frmPartNumberAdvSrch(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmPartNumberAdvSrch);
			this.objFrmTasks = new frmOpenBookTasks(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlTasks.Controls.Add(this.objFrmTasks);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public XmlNodeList FilterBooksList(XmlNode xSchemaNode, XmlNodeList xNodeList)
		{
			return this.frmParent.FilterBooksList(xSchemaNode, xNodeList);
		}

		private void frmOpenBook_Activated(object sender, EventArgs e)
		{
		}

		private void frmOpenBook_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			this.SaveUserSettings();
			this.frmParent.HideDimmer();
			if (!this.objFrmBrowse.IsDisposed)
			{
				this.objFrmBrowse.Close();
			}
			if (!this.objFrmBookmark.IsDisposed)
			{
				this.objFrmBookmark.Close();
			}
			if (!this.objFrmSearch.IsDisposed)
			{
				this.objFrmSearch.Close();
			}
			if (!this.objFrmPageNameAdvSrch.IsDisposed)
			{
				this.objFrmPageNameAdvSrch.Close();
			}
			if (!this.objFrmPartNameAdvSrch.IsDisposed)
			{
				this.objFrmPartNameAdvSrch.Close();
			}
			if (!this.objFrmPartNumberAdvSrch.IsDisposed)
			{
				this.objFrmPartNumberAdvSrch.Close();
			}
		}

		private void frmOpenBook_Load(object sender, EventArgs e)
		{
			this.LoadResources();
			this.tsbBrowse.Visible = Program.objAppFeatures.bOpenBookScreen;
			this.LoadUserSettings();
			this.CreateForms();
			this.tsbSearchPageName.Visible = Program.objAppFeatures.bAdvanceSearchPageName;
			this.tsbSearchPartName.Visible = Program.objAppFeatures.bAdvanceSearchPartName;
			this.tsbSearchPartNumber.Visible = Program.objAppFeatures.bAdvanceSearchPartNumber;
			this.toolStripSeparator3.Visible = (this.tsbSearchPageName.Visible || this.tsbSearchPartName.Visible ? true : this.tsbSearchPartNumber.Visible);
			this.objFrmTasks.Show();
		}

		public Control GetCurrentChildForm()
		{
			Control control;
			IEnumerator enumerator = this.pnlContents.Controls.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Control current = (Control)enumerator.Current;
					if (!current.Visible)
					{
						continue;
					}
					control = current;
					return control;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return control;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='OPEN_BOOKS']");
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
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
					}
					str = string.Concat(str, "/Resource[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		public void HideForms()
		{
			foreach (Control control in this.pnlContents.Controls)
			{
				if (control == null)
				{
					continue;
				}
				control.Hide();
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmOpenBook));
			this.toolStripContainer1 = new ToolStripContainer();
			this.ssStatus = new StatusStrip();
			this.lblStatus = new ToolStripStatusLabel();
			this.pnlContents = new Panel();
			this.pnlTasks = new Panel();
			this.toolStrip1 = new ToolStrip();
			this.tsbBrowse = new ToolStripButton();
			this.tsbBookmarks = new ToolStripButton();
			this.tsbSearch = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsbSearchPageName = new ToolStripButton();
			this.tsbSearchPartName = new ToolStripButton();
			this.tsbSearchPartNumber = new ToolStripButton();
			this.tsbSearchText = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.tsbDataCleanup = new ToolStripButton();
			this.tsbConnection = new ToolStripButton();
			this.toolStrip2 = new ToolStrip();
			this.toolStripButton4 = new ToolStripButton();
			this.toolStripButton5 = new ToolStripButton();
			this.toolStripButton6 = new ToolStripButton();
			this.toolStripButton7 = new ToolStripButton();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.ssStatus.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			base.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.ssStatus);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContents);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlTasks);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(699, 424);
			this.toolStripContainer1.Dock = DockStyle.Fill;
			this.toolStripContainer1.Location = new Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(699, 471);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
			this.ssStatus.Dock = DockStyle.None;
			this.ssStatus.Items.AddRange(new ToolStripItem[] { this.lblStatus });
			this.ssStatus.Location = new Point(0, 0);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new System.Drawing.Size(699, 22);
			this.ssStatus.TabIndex = 0;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(38, 17);
			this.lblStatus.Text = "Ready";
			this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlContents.Dock = DockStyle.Fill;
			this.pnlContents.Location = new Point(161, 0);
			this.pnlContents.Name = "pnlContents";
			this.pnlContents.Padding = new System.Windows.Forms.Padding(2);
			this.pnlContents.Size = new System.Drawing.Size(538, 424);
			this.pnlContents.TabIndex = 16;
			this.pnlTasks.Dock = DockStyle.Left;
			this.pnlTasks.Location = new Point(0, 0);
			this.pnlTasks.Name = "pnlTasks";
			this.pnlTasks.Padding = new System.Windows.Forms.Padding(2);
			this.pnlTasks.Size = new System.Drawing.Size(161, 424);
			this.pnlTasks.TabIndex = 15;
			this.toolStrip1.Dock = DockStyle.None;
			ToolStripItemCollection items = this.toolStrip1.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.tsbBrowse, this.tsbBookmarks, this.tsbSearch, this.toolStripSeparator1, this.tsbSearchPageName, this.tsbSearchPartName, this.tsbSearchPartNumber, this.tsbSearchText, this.toolStripSeparator3, this.tsbDataCleanup, this.tsbConnection };
			items.AddRange(toolStripItemArray);
			this.toolStrip1.Location = new Point(3, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(260, 25);
			this.toolStrip1.TabIndex = 1;
			this.tsbBrowse.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbBrowse.Image = (Image)componentResourceManager.GetObject("tsbBrowse.Image");
			this.tsbBrowse.ImageTransparentColor = Color.Magenta;
			this.tsbBrowse.Name = "tsbBrowse";
			this.tsbBrowse.Size = new System.Drawing.Size(23, 22);
			this.tsbBrowse.Text = "Browse";
			this.tsbBrowse.Click += new EventHandler(this.tsbBrowse_Click);
			this.tsbBookmarks.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbBookmarks.Image = Resources.Bookmarks;
			this.tsbBookmarks.ImageTransparentColor = Color.Magenta;
			this.tsbBookmarks.Name = "tsbBookmarks";
			this.tsbBookmarks.Size = new System.Drawing.Size(23, 22);
			this.tsbBookmarks.Text = "Bookmarks";
			this.tsbBookmarks.Click += new EventHandler(this.tsbBookmarks_Click);
			this.tsbSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearch.Image = (Image)componentResourceManager.GetObject("tsbSearch.Image");
			this.tsbSearch.ImageTransparentColor = Color.Magenta;
			this.tsbSearch.Name = "tsbSearch";
			this.tsbSearch.Size = new System.Drawing.Size(23, 22);
			this.tsbSearch.Text = "Search";
			this.tsbSearch.Click += new EventHandler(this.tsbSearch_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.tsbSearchPageName.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchPageName.Image = Resources.Search_Page;
			this.tsbSearchPageName.ImageTransparentColor = Color.Magenta;
			this.tsbSearchPageName.Name = "tsbSearchPageName";
			this.tsbSearchPageName.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchPageName.Text = "Page Name Search";
			this.tsbSearchPageName.Click += new EventHandler(this.tsbSearchPageName_Click);
			this.tsbSearchPartName.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchPartName.Image = Resources.Search_Parts;
			this.tsbSearchPartName.ImageTransparentColor = Color.Magenta;
			this.tsbSearchPartName.Name = "tsbSearchPartName";
			this.tsbSearchPartName.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchPartName.Text = "Part Name Search";
			this.tsbSearchPartName.Click += new EventHandler(this.tsbSearchPartName_Click);
			this.tsbSearchPartNumber.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchPartNumber.Image = Resources.Search_Parts2;
			this.tsbSearchPartNumber.ImageTransparentColor = Color.Magenta;
			this.tsbSearchPartNumber.Name = "tsbSearchPartNumber";
			this.tsbSearchPartNumber.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchPartNumber.Text = "Part Number Search";
			this.tsbSearchPartNumber.Click += new EventHandler(this.tsbSearchPartNumber_Click);
			this.tsbSearchText.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchText.Image = Resources.Search_Text;
			this.tsbSearchText.ImageTransparentColor = Color.Magenta;
			this.tsbSearchText.Name = "tsbSearchText";
			this.tsbSearchText.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchText.Text = "Text Search";
			this.tsbSearchText.Visible = false;
			this.tsbSearchText.Click += new EventHandler(this.tsbSearchText_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			this.tsbDataCleanup.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbDataCleanup.Image = Resources.Manage_Download;
			this.tsbDataCleanup.ImageTransparentColor = Color.Magenta;
			this.tsbDataCleanup.Name = "tsbDataCleanup";
			this.tsbDataCleanup.Size = new System.Drawing.Size(23, 22);
			this.tsbDataCleanup.Text = "Data Cleanup...";
			this.tsbDataCleanup.Click += new EventHandler(this.tsbDataCleanup_Click);
			this.tsbConnection.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbConnection.Image = Resources.Viewer_Connection;
			this.tsbConnection.ImageTransparentColor = Color.Magenta;
			this.tsbConnection.Name = "tsbConnection";
			this.tsbConnection.Size = new System.Drawing.Size(23, 22);
			this.tsbConnection.Text = "Connection Settings";
			this.tsbConnection.Click += new EventHandler(this.tsbConnection_Click);
			this.toolStrip2.Dock = DockStyle.None;
			ToolStripItemCollection toolStripItemCollections = this.toolStrip2.Items;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.toolStripButton4, this.toolStripButton5, this.toolStripButton6, this.toolStripButton7 };
			toolStripItemCollections.AddRange(toolStripItemArray1);
			this.toolStrip2.Location = new Point(113, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(102, 25);
			this.toolStrip2.TabIndex = 2;
			this.toolStrip2.Visible = false;
			this.toolStripButton4.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Enabled = false;
			this.toolStripButton4.Image = (Image)componentResourceManager.GetObject("toolStripButton4.Image");
			this.toolStripButton4.ImageTransparentColor = Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton4.Text = "Tree View";
			this.toolStripButton5.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Enabled = false;
			this.toolStripButton5.Image = (Image)componentResourceManager.GetObject("toolStripButton5.Image");
			this.toolStripButton5.ImageTransparentColor = Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton5.Text = "Grid View";
			this.toolStripButton6.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton6.Enabled = false;
			this.toolStripButton6.Image = (Image)componentResourceManager.GetObject("toolStripButton6.Image");
			this.toolStripButton6.ImageTransparentColor = Color.Magenta;
			this.toolStripButton6.Name = "toolStripButton6";
			this.toolStripButton6.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton6.Text = "Explorer View";
			this.toolStripButton7.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.toolStripButton7.Enabled = false;
			this.toolStripButton7.Image = (Image)componentResourceManager.GetObject("toolStripButton7.Image");
			this.toolStripButton7.ImageTransparentColor = Color.Magenta;
			this.toolStripButton7.Name = "toolStripButton7";
			this.toolStripButton7.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton7.Text = "Image Preview";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(699, 471);
			base.Controls.Add(this.toolStripContainer1);
			base.IsMdiContainer = true;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(580, 380);
			base.Name = "frmOpenBook";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Open Books";
			base.Load += new EventHandler(this.frmOpenBook_Load);
			base.Activated += new EventHandler(this.frmOpenBook_Activated);
			base.FormClosing += new FormClosingEventHandler(this.frmOpenBook_FormClosing);
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			base.ResumeLayout(false);
		}

		private void LoadResources()
		{
			this.tsbBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.TOOLSTRIP);
			this.tsbBookmarks.Text = this.GetResource("Bookmarks", "BOOKMARKS", ResourceType.TOOLSTRIP);
			this.tsbSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.TOOLSTRIP);
			this.tsbDataCleanup.Text = this.GetResource("Manage Disk Space", "MANAGE_DISK_SPACE", ResourceType.TOOLSTRIP);
			this.tsbConnection.Text = this.GetResource("Connection Settings", "CONNECTION_SETTINGS", ResourceType.TOOLSTRIP);
			this.tsbSearchPageName.Text = this.GetResource("Page Name Search", "PAGE_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchPartName.Text = this.GetResource("Part Name Search", "PART_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchPartNumber.Text = this.GetResource("Part Number Search", "PART_NUMBER_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchText.Text = this.GetResource("Search", "SEARCH", ResourceType.TOOLSTRIP);
			this.toolStripButton4.Text = this.GetResource("Tree View", "TREE_VIEW", ResourceType.TOOLSTRIP);
			this.toolStripButton5.Text = this.GetResource("Grid View", "GRID_VIEW", ResourceType.TOOLSTRIP);
			this.toolStripButton6.Text = this.GetResource("Explorer View", "EXPLORER_VIEW", ResourceType.TOOLSTRIP);
			this.toolStripButton7.Text = this.GetResource("Image Preview", "IMAGE_PREVIEW", ResourceType.TOOLSTRIP);
			this.Text = this.GetResource("Open Books", "OPEN_BOOKS", ResourceType.TITLE);
		}

		private void LoadUserSettings()
		{
			base.Location = Settings.Default.frmOpenBookLocation;
			base.Size = Settings.Default.frmOpenBookSize;
			if (Settings.Default.frmOpenBookState != FormWindowState.Minimized)
			{
				base.WindowState = Settings.Default.frmOpenBookState;
			}
			else
			{
				base.WindowState = FormWindowState.Normal;
			}
			this.frmParent.checkConfigFileExist();
			ToolStripManager.LoadSettings(this);
		}

		private void SaveUserSettings()
		{
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmOpenBookLocation = base.RestoreBounds.Location;
			}
			else
			{
				Settings.Default.frmOpenBookLocation = base.Location;
			}
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmOpenBookSize = base.RestoreBounds.Size;
			}
			else
			{
				Settings.Default.frmOpenBookSize = base.Size;
			}
			Settings.Default.frmOpenBookState = base.WindowState;
			ToolStripManager.SaveSettings(this);
			this.frmParent.CopyConfigurationFile();
		}

		private void tsbBookmarks_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblOpenBooksBookmarks_Click(null, null);
		}

		private void tsbBrowse_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblOpenBooksBrowse_Click(null, null);
		}

		private void tsbConfiguration_Click(object sender, EventArgs e)
		{
			(new frmConfig(this.frmParent)
			{
				Owner = this
			}).Show();
		}

		private void tsbConnection_Click(object sender, EventArgs e)
		{
			(new frmConnection(this.frmParent)
			{
				Owner = this
			}).Show();
		}

		private void tsbDataCleanup_Click(object sender, EventArgs e)
		{
			(new frmDataSize(this.frmParent)
			{
				Owner = this
			}).Show();
		}

		private void tsbSearch_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblOpenBooksSearch_Click(null, null);
		}

		private void tsbSearchPageName_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblAdvSrchPageName_Click(null, null);
		}

		private void tsbSearchPartName_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblAdvSrchPartName_Click(null, null);
		}

		private void tsbSearchPartNumber_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblAdvSrchPartNo_Click(null, null);
		}

		private void tsbSearchText_Click(object sender, EventArgs e)
		{
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.objFrmTasks.UpdateFont();
			this.objFrmBrowse.UpdateFont();
			this.objFrmBookmark.UpdateFont();
			this.objFrmSearch.UpdateFont();
			this.objFrmPageNameAdvSrch.UpdateFont();
			this.objFrmPartNameAdvSrch.UpdateFont();
			this.objFrmPartNumberAdvSrch.UpdateFont();
		}

		public void UpdateStatus(string status)
		{
			if (!this.ssStatus.InvokeRequired)
			{
				this.lblStatus.Text = status;
				return;
			}
			StatusStrip statusStrip = this.ssStatus;
			frmOpenBook.UpdateStatusDelegate updateStatusDelegate = new frmOpenBook.UpdateStatusDelegate(this.UpdateStatus);
			object[] objArray = new object[] { status };
			statusStrip.Invoke(updateStatusDelegate, objArray);
		}

		private delegate void UpdateStatusDelegate(string status);
	}
}
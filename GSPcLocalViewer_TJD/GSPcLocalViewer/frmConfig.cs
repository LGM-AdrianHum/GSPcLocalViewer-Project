using GSPcLocalViewer.frmConfiguration;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmConfig : Form
	{
		public frmConfigTasks objFrmTasks;

		public frmConfigViewerFont objFrmViewerFont;

		public frmConfigViewerColor objFrmViewerColor;

		public frmConfigViewerGeneral objFrmViewerGeneral;

		public frmConfigMemoSettings objFrmMemoSettings;

		public frmConfigSearchPageName objFrmSearchPageName;

		public frmConfigSearchText objFrmSearchText;

		public frmConfigSearchPartNumber objFrmSearchPartNumber;

		public frmConfigSearchPartName objFrmSearchPartName;

		public frmConfigSearchAdvance objFrmSearchAdvance;

		public frmViewer frmParent;

		private ConfigTasks task;

		public frmConfigSelectionList objFrmSelectionList;

		public frmConfigPartList objFrmPartList;

		private IContainer components;

		private Panel pnlContents;

		private Panel pnlTasks;

		private ToolStripContainer toolStripContainer1;

		private ToolStrip tsViewer;

		private ToolStripButton tsbViewerGeneral;

		private ToolStrip tsSearch;

		private ToolStripButton tsbSearchPageName;

		private ToolStripButton tsbSearchPartNumber;

		private ToolStripButton tsbViewerFont;

		private ToolStripButton tsbSearchPartName;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton tsbMemoSettings;

		private ToolStripButton tsbViewerColor;

		public frmConfig(frmViewer frm)
		{
			this.InitializeComponent();
			this.task = ConfigTasks.Viewer_Font;
			this.frmParent = frm;
			this.UpdateFont();
			this.LoadResources();
		}

		public void ChangeGlobalMemoPath(string oldPath, string newPath)
		{
			this.frmParent.ChangeGlobalMemoPath(oldPath, newPath);
		}

		public void CloseAndSaveSettings()
		{
			try
			{
				if (this.objFrmMemoSettings.CheckSettings())
				{
					Settings.Default.appFont = this.objFrmViewerFont.GetFont;
					Settings.Default.printFont = this.objFrmViewerFont.GetPrintFont;
					Settings.Default.appHighlightBackColor = this.objFrmViewerColor.GetHighlightBackColorGeneral;
					Settings.Default.appHighlightForeColor = this.objFrmViewerColor.GetHighlightForeColorGeneral;
					Settings.Default.PartsListInfoBackColor = this.objFrmViewerColor.GetHighlightBackColorPartsList;
					Settings.Default.PartsListInfoForeColor = this.objFrmViewerColor.GetHighlightForeColorPartsList;
					if (this.objFrmViewerGeneral.GetApplicationLanguage != Settings.Default.appLanguage)
					{
						Global.sApplangEngName = Settings.Default.appLanguage;
						Settings.Default.appLanguage = this.objFrmViewerGeneral.GetApplicationLanguage;
					}
					if (this.objFrmViewerGeneral.GetHistorySize != Settings.Default.HistorySize)
					{
						Settings.Default.HistorySize = this.objFrmViewerGeneral.GetHistorySize;
						this.frmParent.ResetHistory();
					}
					if (this.objFrmViewerGeneral.GetSideBySide != Settings.Default.SideBySidePrinting)
					{
						Settings.Default.SideBySidePrinting = this.objFrmViewerGeneral.GetSideBySide;
					}
					if (!Program.objAppFeatures.bDjVuScroll)
					{
						Settings.Default.MouseScrollContents = false;
						Settings.Default.MouseScrollPicture = false;
					}
					else
					{
						Settings.Default.MouseScrollContents = this.objFrmViewerGeneral.GetMouseWheelScrollContents;
						Settings.Default.MouseScrollPicture = this.objFrmViewerGeneral.GetMouseWheelScrollPicture;
					}
					if (this.objFrmViewerGeneral.GetOpenBookinCurrentWindow != Settings.Default.OpenInCurrentInstance)
					{
						Settings.Default.OpenInCurrentInstance = this.objFrmViewerGeneral.GetOpenBookinCurrentWindow;
					}
					if (this.objFrmViewerGeneral.GetMaintainZoom != Settings.Default.MaintainZoom)
					{
						Settings.Default.MaintainZoom = this.objFrmViewerGeneral.GetMaintainZoom;
					}
					if (this.objFrmViewerGeneral.GetDefaultZoom.ToUpper() != Settings.Default.DefaultPictureZoom.ToUpper())
					{
						Settings.Default.DefaultPictureZoom = this.objFrmViewerGeneral.GetDefaultZoom;
					}
					if (this.objFrmViewerGeneral.GetShowPicToolbar != Settings.Default.ShowPicToolbar)
					{
						Settings.Default.ShowPicToolbar = this.objFrmViewerGeneral.GetShowPicToolbar;
						this.frmParent.ShowHidePictureToolbar();
					}
					if (this.objFrmViewerGeneral.GetShowListToolbar != Settings.Default.ShowListToolbar)
					{
						Settings.Default.ShowListToolbar = this.objFrmViewerGeneral.GetShowListToolbar;
						this.frmParent.ShowHidePartslistToolbar();
					}
					if (this.objFrmViewerGeneral.GetZoomStep != Settings.Default.appZoomStep)
					{
						Settings.Default.appZoomStep = this.objFrmViewerGeneral.GetZoomStep;
					}
					if (this.objFrmViewerGeneral.GetFitPageForMultiParts != Settings.Default.appFitPageForMultiParts)
					{
						Settings.Default.appFitPageForMultiParts = this.objFrmViewerGeneral.GetFitPageForMultiParts;
					}
					Settings.Default.HankakuZenkakuFlag = this.objFrmViewerGeneral.GetHankakuZenkakuFlag;
					Settings.Default.ExpandAllContents = this.objFrmViewerGeneral.GetExpandAllContentsFlag;
					Settings.Default.ExpandContentsLevel = this.objFrmViewerGeneral.GetExpandContentsLevel;
					this.objFrmMemoSettings.SaveSettings();
					this.objFrmSearchPageName.SaveSettings();
					this.objFrmSearchPartNumber.SaveSettings();
					this.objFrmSearchPartName.SaveSettings();
					this.objFrmSearchText.SaveSettings();
					if (this.objFrmPartList.dgViewPartListSettings.Rows.Count <= 0 || this.objFrmPartList.SaveSettings())
					{
						if (this.objFrmSelectionList.dgViewSelectionListSettings.Rows.Count <= 0 || this.objFrmSelectionList.SaveSettings())
						{
							this.frmParent.UpdatePartAndSelectionList();
							this.objFrmSearchAdvance.SaveSettings();
							this.frmParent.LoadXML(this.objFrmViewerGeneral.cmbLanguagesList.SelectedItem.ToString());
							base.Close();
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void CreateForms()
		{
			this.objFrmViewerFont = new frmConfigViewerFont(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmViewerFont);
			this.objFrmViewerColor = new frmConfigViewerColor(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmViewerColor);
			this.objFrmViewerGeneral = new frmConfigViewerGeneral(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmViewerGeneral);
			this.objFrmMemoSettings = new frmConfigMemoSettings(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmMemoSettings);
			this.objFrmSearchPageName = new frmConfigSearchPageName(this, this.frmParent.ServerId)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmSearchPageName);
			this.objFrmSearchText = new frmConfigSearchText(this, this.frmParent.ServerId)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmSearchText);
			this.objFrmSearchPartNumber = new frmConfigSearchPartNumber(this, this.frmParent.ServerId)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmSearchPartNumber);
			this.objFrmSearchPartName = new frmConfigSearchPartName(this, this.frmParent.ServerId)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmSearchPartName);
			this.objFrmSearchAdvance = new frmConfigSearchAdvance(this, this.frmParent.ServerId)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmSearchAdvance);
			this.objFrmSelectionList = new frmConfigSelectionList(this, this.frmParent.ServerId)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmSelectionList);
			this.objFrmPartList = new frmConfigPartList(this, this.frmParent.ServerId)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmPartList);
			this.objFrmTasks = new frmConfigTasks(this)
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

		private void frmConfig_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			if (!this.objFrmViewerFont.IsDisposed)
			{
				this.objFrmViewerFont.Close();
			}
			if (!this.objFrmViewerColor.IsDisposed)
			{
				this.objFrmViewerColor.Close();
			}
			if (!this.objFrmViewerGeneral.IsDisposed)
			{
				this.objFrmViewerGeneral.Close();
			}
			if (!this.objFrmMemoSettings.IsDisposed)
			{
				this.objFrmMemoSettings.Close();
			}
			if (!this.objFrmSearchPageName.IsDisposed)
			{
				this.objFrmSearchPageName.Close();
			}
			if (!this.objFrmSearchText.IsDisposed)
			{
				this.objFrmSearchText.Close();
			}
			if (!this.objFrmSearchPartNumber.IsDisposed)
			{
				this.objFrmSearchPartNumber.Close();
			}
			if (!this.objFrmSearchPartName.IsDisposed)
			{
				this.objFrmSearchPartName.Close();
			}
			if (!this.objFrmSearchAdvance.IsDisposed)
			{
				this.objFrmSearchAdvance.Close();
			}
			if (!this.objFrmTasks.IsDisposed)
			{
				this.objFrmTasks.Close();
			}
			if (!this.objFrmPartList.IsDisposed)
			{
				this.objFrmPartList.Close();
			}
			if (!this.objFrmSelectionList.IsDisposed)
			{
				this.objFrmSelectionList.Close();
			}
			if (base.Owner.GetType() == typeof(frmViewer))
			{
				this.frmParent.HideDimmer();
			}
		}

		private void frmConfig_Load(object sender, EventArgs e)
		{
			this.CreateForms();
			this.objFrmTasks.Show();
			this.objFrmTasks.ShowTask(this.task);
			this.OnOffFeatures();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='CONFIGURATION']");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmConfig));
			this.pnlContents = new Panel();
			this.pnlTasks = new Panel();
			this.toolStripContainer1 = new ToolStripContainer();
			this.tsViewer = new ToolStrip();
			this.tsbViewerFont = new ToolStripButton();
			this.tsbViewerColor = new ToolStripButton();
			this.tsbViewerGeneral = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsbMemoSettings = new ToolStripButton();
			this.tsSearch = new ToolStrip();
			this.tsbSearchPageName = new ToolStripButton();
			this.tsbSearchPartName = new ToolStripButton();
			this.tsbSearchPartNumber = new ToolStripButton();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tsViewer.SuspendLayout();
			this.tsSearch.SuspendLayout();
			base.SuspendLayout();
			this.pnlContents.Dock = DockStyle.Fill;
			this.pnlContents.Location = new Point(160, 0);
			this.pnlContents.Name = "pnlContents";
			this.pnlContents.Padding = new System.Windows.Forms.Padding(2);
			this.pnlContents.Size = new System.Drawing.Size(460, 486);
			this.pnlContents.TabIndex = 18;
			this.pnlTasks.Dock = DockStyle.Left;
			this.pnlTasks.Location = new Point(0, 0);
			this.pnlTasks.Name = "pnlTasks";
			this.pnlTasks.Padding = new System.Windows.Forms.Padding(2);
			this.pnlTasks.Size = new System.Drawing.Size(160, 486);
			this.pnlTasks.TabIndex = 17;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContents);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlTasks);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(620, 486);
			this.toolStripContainer1.Dock = DockStyle.Fill;
			this.toolStripContainer1.Location = new Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(620, 511);
			this.toolStripContainer1.TabIndex = 4;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsViewer);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsSearch);
			this.tsViewer.Dock = DockStyle.None;
			ToolStripItemCollection items = this.tsViewer.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.tsbViewerFont, this.tsbViewerColor, this.tsbViewerGeneral, this.toolStripSeparator2, this.tsbMemoSettings };
			items.AddRange(toolStripItemArray);
			this.tsViewer.Location = new Point(3, 0);
			this.tsViewer.Name = "tsViewer";
			this.tsViewer.Size = new System.Drawing.Size(108, 25);
			this.tsViewer.TabIndex = 0;
			this.tsbViewerFont.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbViewerFont.Image = Resources.Font;
			this.tsbViewerFont.ImageTransparentColor = Color.Magenta;
			this.tsbViewerFont.Name = "tsbViewerFont";
			this.tsbViewerFont.Size = new System.Drawing.Size(23, 22);
			this.tsbViewerFont.Text = "Viewer Font";
			this.tsbViewerFont.Click += new EventHandler(this.tsbViewerFont_Click);
			this.tsbViewerColor.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbViewerColor.Image = Resources.Font;
			this.tsbViewerColor.ImageTransparentColor = Color.Magenta;
			this.tsbViewerColor.Name = "tsbPartsListInfoFont";
			this.tsbViewerColor.Size = new System.Drawing.Size(23, 22);
			this.tsbViewerColor.Text = "PartsListInfo Font";
			this.tsbViewerColor.Click += new EventHandler(this.tsbPartsListInfoFont_Click);
			this.tsbViewerGeneral.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbViewerGeneral.Image = (Image)componentResourceManager.GetObject("tsbViewerGeneral.Image");
			this.tsbViewerGeneral.ImageTransparentColor = Color.Magenta;
			this.tsbViewerGeneral.Name = "tsbViewerGeneral";
			this.tsbViewerGeneral.Size = new System.Drawing.Size(23, 22);
			this.tsbViewerGeneral.Text = "Viewer General";
			this.tsbViewerGeneral.Click += new EventHandler(this.tsbViewerGeneral_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.tsbMemoSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbMemoSettings.Image = Resources.Memo_Settings;
			this.tsbMemoSettings.ImageTransparentColor = Color.Magenta;
			this.tsbMemoSettings.Name = "tsbMemoSettings";
			this.tsbMemoSettings.Size = new System.Drawing.Size(23, 22);
			this.tsbMemoSettings.Text = "Memo Settings";
			this.tsbMemoSettings.Click += new EventHandler(this.tsbMemoSettings_Click);
			this.tsSearch.Dock = DockStyle.None;
			ToolStripItemCollection toolStripItemCollections = this.tsSearch.Items;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.tsbSearchPageName, this.tsbSearchPartName, this.tsbSearchPartNumber };
			toolStripItemCollections.AddRange(toolStripItemArray1);
			this.tsSearch.Location = new Point(111, 0);
			this.tsSearch.Name = "tsSearch";
			this.tsSearch.Size = new System.Drawing.Size(110, 25);
			this.tsSearch.TabIndex = 1;
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
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(620, 511);
			base.Controls.Add(this.toolStripContainer1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.IsMdiContainer = true;
			base.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(626, 536);
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(626, 536);
			base.Name = "frmConfig";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Configuration";
			base.Load += new EventHandler(this.frmConfig_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmConfig_FormClosing);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tsViewer.ResumeLayout(false);
			this.tsViewer.PerformLayout();
			this.tsSearch.ResumeLayout(false);
			this.tsSearch.PerformLayout();
			base.ResumeLayout(false);
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Configuration", "CONFIGURATION", ResourceType.TITLE);
			this.tsbViewerColor.Text = this.GetResource("Color", "COLOR", ResourceType.TOOLSTRIP);
			this.tsbSearchPageName.Text = this.GetResource("Page Name Search", "PAGE_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchPartNumber.Text = this.GetResource("Part Number Search", "PART_NUMBER_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchPartName.Text = this.GetResource("Part Name Search", "PART_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbViewerFont.Text = this.GetResource("Font", "FONT", ResourceType.TOOLSTRIP);
			this.tsbViewerGeneral.Text = this.GetResource("Viewer General", "VIEWER_GENERAL", ResourceType.TOOLSTRIP);
			this.tsbMemoSettings.Text = this.GetResource("Memo Settings", "MEMO_SETTINGS", ResourceType.TOOLSTRIP);
		}

		public void OnOffFeatures()
		{
			try
			{
				this.tsbSearchPageName.Visible = Program.objAppFeatures.bPageNameSearch;
				this.tsbSearchPartName.Visible = Program.objAppFeatures.bPartNameSearch;
				this.tsbSearchPartNumber.Visible = Program.objAppFeatures.bPartNumberSearch;
				this.tsbMemoSettings.Visible = Program.objAppFeatures.bMemo;
				if (!Program.objAppFeatures.bPageNameSearch && !Program.objAppFeatures.bTextSearch && !Program.objAppFeatures.bPartNameSearch && !Program.objAppFeatures.bPartNumberSearch)
				{
					this.tsSearch.Visible = false;
				}
				if (this.frmParent.BookType.ToUpper().Trim() == "GSC")
				{
					this.tsbSearchPartName.Visible = false;
					this.tsbSearchPartNumber.Visible = false;
				}
			}
			catch
			{
			}
		}

		public void Show(ConfigTasks task)
		{
			this.task = task;
			base.Show();
		}

		private void tsbMemoSettings_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblMemosSettings_Click(null, null);
		}

		private void tsbPartsListInfoFont_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblPartsListInfo_Click(null, null);
		}

		private void tsbSearchPageName_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblPageName_Click(null, null);
		}

		private void tsbSearchPartName_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblPartName_Click(null, null);
		}

		private void tsbSearchPartNumber_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblPartNumber_Click(null, null);
		}

		private void tsbViewerFont_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblViewerFont_Click(null, null);
		}

		private void tsbViewerGeneral_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblViewerGeneral_Click(null, null);
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}
	}
}
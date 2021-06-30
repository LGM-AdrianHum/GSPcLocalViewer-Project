using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmSearch : Form
	{
		private const uint LOCALE_SYSTEM_DEFAULT = 2048;

		private const uint LCMAP_HALFWIDTH = 4194304;

		private const uint LCMAP_FULLWIDTH = 8388608;

		private IContainer components;

		private Panel pnlContents;

		private Panel pnlTasks;

		private ToolStripContainer toolStripContainer1;

		private ToolStrip tsPage;

		private ToolStripButton tsbPageName;

		private ToolStrip tsPart;

		private ToolStripButton tsbPartNumber;

		private ToolStripButton tsbText;

		private ToolStripButton tsbPartName;

		private ToolStripButton tsbPartAdvance;

		public frmSearchTasks objFrmTasks;

		public frmPageNameSearch objFrmPageNameSearch;

		public frmPartNameSearch objFrmPartNameSearch;

		public frmPartNumberSearch objFrmPartNumberSearch;

		public frmTextSearch objFrmTextSearch;

		public frmAdvanceSearch objFrmAdvanceSearch;

		public frmViewer frmParent;

		private SearchTasks task;

		public frmSearch(frmViewer frm)
		{
			this.InitializeComponent();
			this.task = SearchTasks.PageName;
			this.frmParent = frm;
			this.UpdateFont();
			this.LoadResources();
		}

		public void ChangeGlobalMemoPath(string oldPath, string newPath)
		{
			this.frmParent.ChangeGlobalMemoPath(oldPath, newPath);
		}

		public void CloseContainer()
		{
			base.Close();
		}

		public string[] ConvertStringWidth(string sInput)
		{
			string[] strArrays;
			try
			{
				string empty = string.Empty;
				string halfWidth = string.Empty;
				empty = frmSearch.ToFullWidth(sInput);
				halfWidth = frmSearch.ToHalfWidth(sInput);
				strArrays = (!(empty == string.Empty) || !(halfWidth == string.Empty) ? new string[] { empty, halfWidth } : new string[] { sInput });
			}
			catch (Exception exception)
			{
				strArrays = new string[] { sInput };
			}
			return strArrays;
		}

		private void CreateForms()
		{
			this.objFrmPageNameSearch = new frmPageNameSearch(this.frmParent, this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmPageNameSearch);
			this.objFrmPartNumberSearch = new frmPartNumberSearch(this.frmParent, this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmPartNumberSearch);
			this.objFrmPartNameSearch = new frmPartNameSearch(this.frmParent, this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmPartNameSearch);
			this.objFrmTextSearch = new frmTextSearch(this.frmParent, this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmTextSearch);
			this.objFrmAdvanceSearch = new frmAdvanceSearch(this.frmParent, this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.pnlContents.Controls.Add(this.objFrmAdvanceSearch);
			this.objFrmTasks = new frmSearchTasks(this)
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

		private void frmSearch_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			this.SaveUserSettings();
			if (!this.objFrmPageNameSearch.IsDisposed)
			{
				this.objFrmPageNameSearch.Close();
			}
			if (!this.objFrmAdvanceSearch.IsDisposed)
			{
				this.objFrmAdvanceSearch.Close();
			}
			if (!this.objFrmPartNameSearch.IsDisposed)
			{
				this.objFrmPartNameSearch.Close();
			}
			if (!this.objFrmPartNumberSearch.IsDisposed)
			{
				this.objFrmPartNumberSearch.Close();
			}
			if (!this.objFrmTextSearch.IsDisposed)
			{
				this.objFrmTextSearch.Close();
			}
			if (!this.objFrmTasks.IsDisposed)
			{
				this.objFrmTasks.Close();
			}
			this.frmParent.HideDimmer();
		}

		private void frmSearch_Load(object sender, EventArgs e)
		{
			this.LoadUserSettings();
			this.CreateForms();
			this.objFrmTasks.Show();
			this.objFrmTasks.ShowTask(this.task);
			this.OnOffFeatures();
		}

		private void frmSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='SEARCH']");
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
			this.pnlContents = new Panel();
			this.pnlTasks = new Panel();
			this.toolStripContainer1 = new ToolStripContainer();
			this.tsPage = new ToolStrip();
			this.tsbPageName = new ToolStripButton();
			this.tsbText = new ToolStripButton();
			this.tsPart = new ToolStrip();
			this.tsbPartName = new ToolStripButton();
			this.tsbPartNumber = new ToolStripButton();
			this.tsbPartAdvance = new ToolStripButton();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.tsPage.SuspendLayout();
			this.tsPart.SuspendLayout();
			base.SuspendLayout();
			this.pnlContents.Dock = DockStyle.Fill;
			this.pnlContents.Location = new Point(160, 0);
			this.pnlContents.Name = "pnlContents";
			this.pnlContents.Padding = new System.Windows.Forms.Padding(2);
			this.pnlContents.Size = new System.Drawing.Size(624, 498);
			this.pnlContents.TabIndex = 18;
			this.pnlTasks.Dock = DockStyle.Left;
			this.pnlTasks.Location = new Point(0, 0);
			this.pnlTasks.Name = "pnlTasks";
			this.pnlTasks.Padding = new System.Windows.Forms.Padding(2);
			this.pnlTasks.Size = new System.Drawing.Size(160, 498);
			this.pnlTasks.TabIndex = 17;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlContents);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlTasks);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(784, 498);
			this.toolStripContainer1.Dock = DockStyle.Fill;
			this.toolStripContainer1.Location = new Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(784, 523);
			this.toolStripContainer1.TabIndex = 4;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsPage);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsPart);
			this.tsPage.Dock = DockStyle.None;
			ToolStripItemCollection items = this.tsPage.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.tsbPageName, this.tsbText };
			items.AddRange(toolStripItemArray);
			this.tsPage.Location = new Point(3, 0);
			this.tsPage.Name = "tsPage";
			this.tsPage.Size = new System.Drawing.Size(56, 25);
			this.tsPage.TabIndex = 0;
			this.tsbPageName.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPageName.Image = Resources.Search_Page;
			this.tsbPageName.ImageTransparentColor = Color.Magenta;
			this.tsbPageName.Name = "tsbPageName";
			this.tsbPageName.Size = new System.Drawing.Size(23, 22);
			this.tsbPageName.Text = "Page Name Search";
			this.tsbPageName.Click += new EventHandler(this.tsbPageName_Click);
			this.tsbText.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbText.Image = Resources.Search_Text;
			this.tsbText.ImageTransparentColor = Color.Magenta;
			this.tsbText.Name = "tsbText";
			this.tsbText.Size = new System.Drawing.Size(23, 22);
			this.tsbText.Text = "Text Search";
			this.tsbText.Click += new EventHandler(this.tsbText_Click);
			this.tsPart.Dock = DockStyle.None;
			ToolStripItemCollection toolStripItemCollections = this.tsPart.Items;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.tsbPartName, this.tsbPartNumber, this.tsbPartAdvance };
			toolStripItemCollections.AddRange(toolStripItemArray1);
			this.tsPart.Location = new Point(61, 0);
			this.tsPart.Name = "tsPart";
			this.tsPart.Size = new System.Drawing.Size(79, 25);
			this.tsPart.TabIndex = 1;
			this.tsbPartName.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPartName.Image = Resources.Search_Parts;
			this.tsbPartName.ImageTransparentColor = Color.Magenta;
			this.tsbPartName.Name = "tsbPartName";
			this.tsbPartName.Size = new System.Drawing.Size(23, 22);
			this.tsbPartName.Text = "Part Name Search";
			this.tsbPartName.Click += new EventHandler(this.tsbPartName_Click);
			this.tsbPartNumber.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPartNumber.Image = Resources.Search_Parts2;
			this.tsbPartNumber.ImageTransparentColor = Color.Magenta;
			this.tsbPartNumber.Name = "tsbPartNumber";
			this.tsbPartNumber.Size = new System.Drawing.Size(23, 22);
			this.tsbPartNumber.Text = "Part Number Search";
			this.tsbPartNumber.Click += new EventHandler(this.tsbPartNumber_Click);
			this.tsbPartAdvance.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPartAdvance.Image = Resources.Search_PartsAdvance;
			this.tsbPartAdvance.ImageTransparentColor = Color.Magenta;
			this.tsbPartAdvance.Name = "tsbSearchPartAdvance";
			this.tsbPartAdvance.Size = new System.Drawing.Size(23, 22);
			this.tsbPartAdvance.Text = "Advance Search";
			this.tsbPartAdvance.Click += new EventHandler(this.tsbPartAdvance_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(784, 523);
			base.Controls.Add(this.toolStripContainer1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.IsMdiContainer = true;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(750, 550);
			base.Name = "frmSearch";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Search";
			base.Load += new EventHandler(this.frmSearch_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmSearch_FormClosing);
			base.PreviewKeyDown += new PreviewKeyDownEventHandler(this.frmSearch_PreviewKeyDown);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.tsPage.ResumeLayout(false);
			this.tsPage.PerformLayout();
			this.tsPart.ResumeLayout(false);
			this.tsPart.PerformLayout();
			base.ResumeLayout(false);
		}

		[DllImport("kernel32.dll", CharSet=CharSet.Unicode, ExactSpelling=false)]
		private static extern int LCMapString(uint Locale, uint dwMapFlags, string lpSrcStr, int cchSrc, StringBuilder lpDestStr, int cchDest);

		private void LoadResources()
		{
			this.Text = this.GetResource("Search", "SEARCH", ResourceType.TITLE);
			this.tsbPageName.Text = this.GetResource("Page Name Search", "PAGE_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbText.Text = this.GetResource("Text Search", "TEXT_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbPartName.Text = this.GetResource("Part Name Search", "PART_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbPartNumber.Text = this.GetResource("Part Number Search", "PART_NUMBER_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbPartAdvance.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.TOOLSTRIP);
		}

		private void LoadUserSettings()
		{
			base.Location = Settings.Default.frmSearchLocation;
			base.Size = Settings.Default.frmSearchSize;
			if (Settings.Default.frmSearchState != FormWindowState.Minimized)
			{
				base.WindowState = Settings.Default.frmSearchState;
			}
			else
			{
				base.WindowState = FormWindowState.Normal;
			}
			this.frmParent.checkConfigFileExist();
			ToolStripManager.LoadSettings(this);
		}

		public void OnOffFeatures()
		{
			try
			{
				IniFileIO iniFileIO = new IniFileIO();
				bool flag = false;
				ArrayList arrayLists = new ArrayList();
				if (iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PLIST_SEARCH").Count > 0 && Program.objAppFeatures.bPartAdvanceSearch)
				{
					flag = true;
				}
				this.tsbPageName.Visible = Program.objAppFeatures.bPageNameSearch;
				this.tsbText.Visible = Program.objAppFeatures.bTextSearch;
				this.tsbPartName.Visible = Program.objAppFeatures.bPartNameSearch;
				this.tsbPartNumber.Visible = Program.objAppFeatures.bPartNumberSearch;
				this.tsbPartAdvance.Visible = flag;
				if (Program.objAppFeatures.bPageNameSearch || Program.objAppFeatures.bTextSearch)
				{
					this.tsPage.Visible = true;
				}
				else
				{
					this.tsPage.Visible = false;
				}
				if (Program.objAppFeatures.bPartNameSearch || Program.objAppFeatures.bPartNumberSearch || flag)
				{
					this.tsPart.Visible = true;
				}
				else
				{
					this.tsPart.Visible = false;
				}
			}
			catch
			{
			}
		}

		private void SaveUserSettings()
		{
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmSearchLocation = base.RestoreBounds.Location;
			}
			else
			{
				Settings.Default.frmSearchLocation = base.Location;
			}
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmSearchSize = base.RestoreBounds.Size;
			}
			else
			{
				Settings.Default.frmSearchSize = base.Size;
			}
			Settings.Default.frmSearchState = base.WindowState;
			ToolStripManager.SaveSettings(this);
			this.frmParent.CopyConfigurationFile();
		}

		public void Show(SearchTasks search)
		{
			this.task = search;
			base.Show();
		}

		public static string ToFullWidth(string halfWidth)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			frmSearch.LCMapString(2048, 8388608, halfWidth, -1, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		public static string ToHalfWidth(string fullWidth)
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			frmSearch.LCMapString(2048, 4194304, fullWidth, -1, stringBuilder, stringBuilder.Capacity);
			return stringBuilder.ToString();
		}

		private void tsbPageName_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lbPageName_Click(null, null);
		}

		private void tsbPartAdvance_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblAdvance_Click(null, null);
		}

		private void tsbPartName_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblPartName_Click(null, null);
		}

		private void tsbPartNumber_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblPartNumber_Click(null, null);
		}

		private void tsbText_Click(object sender, EventArgs e)
		{
			this.objFrmTasks.lblTextSearch_Click(null, null);
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}
	}
}
using GSPcLocalViewer.Classes;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace GSPcLocalViewer
{
	public class frmPageNameSearch : Form
	{
		private IContainer components;

		private Label lblPageName;

		private Panel pnlControl;

		private Button btnSearch;

		private Panel pnlForm;

		private DataGridView dgViewSearch;

		private Panel pnlSearch;

		private TextBox txtPageName;

		private CheckBox checkBoxExactMatch;

		private CheckBox checkBoxMatchCase;

		private BackgroundWorker bgWorker;

		private Panel pnlSearchResults;

		private PictureBox picLoading;

		private StatusStrip ssStatus;

		private ToolStripStatusLabel lblStatus;

		private Button btnClearHistory;

		private frmViewer frmParent;

		private frmSearch frmContainer;

		private string statusText;

		private string searchString;

		public frmPageNameSearch(frmViewer frm, GSPcLocalViewer.frmSearch frmSearch)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.frmContainer = frmSearch;
			this.UpdateFont();
			this.InitializeSearchGrid();
			this.LoadDataGridView();
			this.LoadResources();
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			string empty = string.Empty;
			this.UpdateStatus();
			this.UpdadetControls();
			try
			{
				empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				empty = string.Concat(empty, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
				empty = string.Concat(empty, "\\", this.frmParent.BookPublishingId);
				if (!Directory.Exists(empty))
				{
					Directory.CreateDirectory(empty);
				}
				empty = string.Concat(empty, "\\", this.frmParent.BookPublishingId, ".xml");
			}
			catch
			{
				MessageHandler.ShowError1(this.GetResource("(E-PGS-EM002) Failed to create file/folder specified", "(E-PGS-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
			}
			this.GridViewClearRows();
			if (File.Exists(empty))
			{
				if (!this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					if (!this.LoadSearchResultsInGrid(empty))
					{
						this.statusText = this.GetResource("No result found", "NO_RESULT", ResourceType.STATUS_MESSAGE);
						this.UpdateStatus();
						return;
					}
					this.statusText = string.Concat(this.dgViewSearch.Rows.Count, " ", this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE));
					this.UpdateStatus();
					return;
				}
			}
			else if (!this.frmParent.IsDisposed)
			{
				this.statusText = string.Concat(this.frmParent.BookPublishingId, this.GetResource("(E-PGS-EM001) Specified information does not exist", "(E-PGS-EM001)_NO_INFORMATION", ResourceType.STATUS_MESSAGE));
				this.UpdateStatus();
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.dgViewSearch.Dock = DockStyle.None;
			this.dgViewSearch.Dock = DockStyle.Fill;
			this.HideLoading(this.pnlSearchResults);
			this.UpdadetControls();
		}

		private void btnClearHistory_Click(object sender, EventArgs e)
		{
			this.txtPageName.AutoCompleteCustomSource.Clear();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.txtPageName.AutoCompleteCustomSource.Add(this.txtPageName.Text);
			this.ShowLoading(this.pnlSearchResults);
			this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
			this.bgWorker.RunWorkerAsync();
		}

		private void dgViewSearch_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (this.dgViewSearch.Rows.Count != 0 && e.RowIndex >= 0)
				{
					string empty = string.Empty;
					if (this.dgViewSearch.CurrentRow.Tag != null)
					{
						empty = this.dgViewSearch.CurrentRow.Tag.ToString();
					}
					this.frmParent.OpenSearch(Program.iniServers[this.frmParent.ServerId].sIniKey, this.frmParent.BookPublishingId, empty, "1", "1", "");
					this.frmParent.Enabled = true;
					this.frmContainer.CloseContainer();
				}
			}
			catch
			{
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

		private void frmPageNameSearch_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.frmParent.Enabled = true;
		}

		private void frmPageNameSearch_FormClosing(object sender, FormClosingEventArgs e)
		{
			Program.WriteSearchHistory(this.frmParent.sSearchHistoryPath, "PAGENAMESEARCH", Program.objPartNameSearchHistroyCollection);
		}

		private void frmPageNameSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.txtPageName.Text.Trim() != string.Empty && e.KeyCode == Keys.Return)
			{
				this.btnSearch_Click(null, null);
			}
			if (e.KeyCode == Keys.Escape)
			{
				this.frmContainer.Close();
			}
		}

		private void frmPageNameSearch_Load(object sender, EventArgs e)
		{
			Program.ReadSearchHistory(this.frmParent.sSearchHistoryPath, "PAGENAMESEARCH", Program.objPageNameSearchHistroyCollection);
			this.txtPageName.AutoCompleteCustomSource = Program.objPageNameSearchHistroyCollection;
			this.txtPageName.Focus();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='SEARCH']");
				str = string.Concat(str, "/Screen[@Name='PAGE_NAME_SEARCH']");
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

		private void GridViewAddRow(DataGridViewRow tempRow)
		{
			if (!this.dgViewSearch.InvokeRequired)
			{
				this.dgViewSearch.Rows.Add(tempRow);
				return;
			}
			DataGridView dataGridView = this.dgViewSearch;
			frmPageNameSearch.GridViewAddRowDelegate gridViewAddRowDelegate = new frmPageNameSearch.GridViewAddRowDelegate(this.GridViewAddRow);
			object[] objArray = new object[] { tempRow };
			dataGridView.Invoke(gridViewAddRowDelegate, objArray);
		}

		private void GridViewClearRows()
		{
			if (!this.dgViewSearch.InvokeRequired)
			{
				this.dgViewSearch.Rows.Clear();
				return;
			}
			this.dgViewSearch.Invoke(new frmPageNameSearch.GridViewClearRowsDelegate(this.GridViewClearRows));
		}

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
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
				else
				{
					frmPageNameSearch.HideLoadingDelegate hideLoadingDelegate = new frmPageNameSearch.HideLoadingDelegate(this.HideLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(hideLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle control = new DataGridViewCellStyle();
			DataGridViewCellStyle font = new DataGridViewCellStyle();
			DataGridViewCellStyle white = new DataGridViewCellStyle();
			this.lblPageName = new Label();
			this.pnlControl = new Panel();
			this.btnClearHistory = new Button();
			this.btnSearch = new Button();
			this.pnlForm = new Panel();
			this.pnlSearchResults = new Panel();
			this.dgViewSearch = new DataGridView();
			this.pnlSearch = new Panel();
			this.checkBoxExactMatch = new CheckBox();
			this.checkBoxMatchCase = new CheckBox();
			this.txtPageName = new TextBox();
			this.picLoading = new PictureBox();
			this.bgWorker = new BackgroundWorker();
			this.ssStatus = new StatusStrip();
			this.lblStatus = new ToolStripStatusLabel();
			this.pnlControl.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.pnlSearchResults.SuspendLayout();
			((ISupportInitialize)this.dgViewSearch).BeginInit();
			this.pnlSearch.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			this.ssStatus.SuspendLayout();
			base.SuspendLayout();
			this.lblPageName.BackColor = Color.White;
			this.lblPageName.Dock = DockStyle.Top;
			this.lblPageName.ForeColor = Color.Black;
			this.lblPageName.Location = new Point(10, 0);
			this.lblPageName.Name = "lblPageName";
			this.lblPageName.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblPageName.Size = new System.Drawing.Size(478, 27);
			this.lblPageName.TabIndex = 16;
			this.lblPageName.Text = "Page Name";
			this.pnlControl.Controls.Add(this.btnClearHistory);
			this.pnlControl.Controls.Add(this.btnSearch);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(10, 395);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(478, 31);
			this.pnlControl.TabIndex = 18;
			this.btnClearHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnClearHistory.Location = new Point(211, 5);
			this.btnClearHistory.Name = "btnClearHistory";
			this.btnClearHistory.Size = new System.Drawing.Size(114, 23);
			this.btnClearHistory.TabIndex = 5;
			this.btnClearHistory.Text = "Clear Search History";
			this.btnClearHistory.UseVisualStyleBackColor = true;
			this.btnClearHistory.Click += new EventHandler(this.btnClearHistory_Click);
			this.btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnSearch.Enabled = false;
			this.btnSearch.Location = new Point(350, 5);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(114, 23);
			this.btnSearch.TabIndex = 6;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlSearchResults);
			this.pnlForm.Controls.Add(this.pnlSearch);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Controls.Add(this.lblPageName);
			this.pnlForm.Controls.Add(this.picLoading);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.pnlForm.Size = new System.Drawing.Size(500, 428);
			this.pnlForm.TabIndex = 19;
			this.pnlSearchResults.Controls.Add(this.dgViewSearch);
			this.pnlSearchResults.Dock = DockStyle.Fill;
			this.pnlSearchResults.Location = new Point(10, 82);
			this.pnlSearchResults.Name = "pnlSearchResults";
			this.pnlSearchResults.Size = new System.Drawing.Size(478, 313);
			this.pnlSearchResults.TabIndex = 23;
			this.dgViewSearch.AllowDrop = true;
			this.dgViewSearch.AllowUserToAddRows = false;
			this.dgViewSearch.AllowUserToDeleteRows = false;
			this.dgViewSearch.AllowUserToResizeRows = false;
			dataGridViewCellStyle.BackColor = Color.White;
			this.dgViewSearch.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dgViewSearch.BackgroundColor = Color.White;
			this.dgViewSearch.BorderStyle = BorderStyle.Fixed3D;
			control.Alignment = DataGridViewContentAlignment.MiddleLeft;
			control.BackColor = SystemColors.Control;
			control.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			control.ForeColor = Color.Black;
			control.SelectionBackColor = SystemColors.Highlight;
			control.SelectionForeColor = SystemColors.HighlightText;
			control.WrapMode = DataGridViewTriState.True;
			this.dgViewSearch.ColumnHeadersDefaultCellStyle = control;
			this.dgViewSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgViewSearch.Dock = DockStyle.Fill;
			this.dgViewSearch.Location = new Point(0, 0);
			this.dgViewSearch.Name = "dgViewSearch";
			font.Alignment = DataGridViewContentAlignment.MiddleLeft;
			font.BackColor = SystemColors.Control;
			font.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			font.ForeColor = Color.Black;
			font.SelectionBackColor = SystemColors.Highlight;
			font.SelectionForeColor = SystemColors.HighlightText;
			font.WrapMode = DataGridViewTriState.True;
			this.dgViewSearch.RowHeadersDefaultCellStyle = font;
			this.dgViewSearch.RowHeadersVisible = false;
			this.dgViewSearch.RowHeadersWidth = 32;
			this.dgViewSearch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			white.BackColor = Color.White;
			white.ForeColor = Color.Black;
			white.SelectionBackColor = Color.SteelBlue;
			white.SelectionForeColor = Color.White;
			this.dgViewSearch.RowsDefaultCellStyle = white;
			this.dgViewSearch.RowTemplate.Height = 16;
			this.dgViewSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgViewSearch.ShowRowErrors = false;
			this.dgViewSearch.Size = new System.Drawing.Size(478, 313);
			this.dgViewSearch.TabIndex = 4;
			this.dgViewSearch.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.dgViewSearch_CellMouseDoubleClick);
			this.pnlSearch.Controls.Add(this.checkBoxExactMatch);
			this.pnlSearch.Controls.Add(this.checkBoxMatchCase);
			this.pnlSearch.Controls.Add(this.txtPageName);
			this.pnlSearch.Dock = DockStyle.Top;
			this.pnlSearch.Location = new Point(10, 27);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Size = new System.Drawing.Size(478, 55);
			this.pnlSearch.TabIndex = 21;
			this.checkBoxExactMatch.AutoSize = true;
			this.checkBoxExactMatch.Location = new Point(240, 19);
			this.checkBoxExactMatch.Name = "checkBoxExactMatch";
			this.checkBoxExactMatch.Size = new System.Drawing.Size(85, 17);
			this.checkBoxExactMatch.TabIndex = 2;
			this.checkBoxExactMatch.Text = "Exact Match";
			this.checkBoxExactMatch.UseVisualStyleBackColor = true;
			this.checkBoxMatchCase.AutoSize = true;
			this.checkBoxMatchCase.Location = new Point(355, 19);
			this.checkBoxMatchCase.Name = "checkBoxMatchCase";
			this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
			this.checkBoxMatchCase.TabIndex = 3;
			this.checkBoxMatchCase.Text = "Match Case";
			this.checkBoxMatchCase.UseVisualStyleBackColor = true;
			this.txtPageName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.txtPageName.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.txtPageName.Location = new Point(4, 17);
			this.txtPageName.Name = "txtPageName";
			this.txtPageName.Size = new System.Drawing.Size(200, 21);
			this.txtPageName.TabIndex = 1;
			this.txtPageName.TextChanged += new EventHandler(this.txtPageName_TextChanged);
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(379, 246);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.TabIndex = 24;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			this.ssStatus.BackColor = SystemColors.Control;
			this.ssStatus.Items.AddRange(new ToolStripItem[] { this.lblStatus });
			this.ssStatus.Location = new Point(0, 428);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new System.Drawing.Size(500, 22);
			this.ssStatus.TabIndex = 21;
			this.lblStatus.BackColor = SystemColors.Control;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 17);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(500, 450);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.ssStatus);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.KeyPreview = true;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(500, 450);
			base.Name = "frmPageNameSearch";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Search";
			base.Load += new EventHandler(this.frmPageNameSearch_Load);
			base.FormClosed += new FormClosedEventHandler(this.frmPageNameSearch_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.frmPageNameSearch_FormClosing);
			base.KeyDown += new KeyEventHandler(this.frmPageNameSearch_KeyDown);
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnlSearchResults.ResumeLayout(false);
			((ISupportInitialize)this.dgViewSearch).EndInit();
			this.pnlSearch.ResumeLayout(false);
			this.pnlSearch.PerformLayout();
			((ISupportInitialize)this.picLoading).EndInit();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void InitializeSearchGrid()
		{
			if (this.dgViewSearch.InvokeRequired)
			{
				this.dgViewSearch.Invoke(new frmPageNameSearch.InitializeSearchGridDelegate(this.InitializeSearchGrid));
				return;
			}
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					Name = "FieldId",
					HeaderText = "Field ID",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearch.Columns.Add(dataGridViewTextBoxColumn);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
				{
					Name = "DisplayName",
					HeaderText = "Display Name",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearch.Columns.Add(dataGridViewTextBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					Name = "Width",
					HeaderText = "Col Width",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearch.Columns.Add(dataGridViewTextBoxColumn2);
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn()
				{
					Name = "Alignment",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearch.Columns.Add(dataGridViewComboBoxColumn);
			}
			catch
			{
			}
		}

		private void LoadDataGridView()
		{
			if (this.dgViewSearch.InvokeRequired)
			{
				this.dgViewSearch.Invoke(new frmPageNameSearch.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
				return;
			}
			this.dgViewSearch.Columns.Clear();
			ArrayList arrayLists = new ArrayList();
			List<string> strs = new List<string>();
			IniFileIO iniFileIO = new IniFileIO();
			arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PAGENAME_SEARCH");
			for (int i = 0; i < arrayLists.Count; i++)
			{
				try
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					string keyValue = iniFileIO1.GetKeyValue("PAGENAME_SEARCH", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
					Global.AddSearchCol(keyValue, arrayLists[i].ToString(), "PAGENAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				}
				catch
				{
				}
			}
			if (arrayLists.Count == 0)
			{
				Global.AddSearchCol("true|Page Name|C|91", "PageName", "PAGENAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				Global.AddSearchCol("true|Picture File|C|91", "PictureFile", "PAGENAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
			}
		}

		private void LoadResources()
		{
			this.lblPageName.Text = this.GetResource("Page Name", "PAGE_NAME", ResourceType.LABEL);
			this.checkBoxMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
			this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
			this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
			this.btnClearHistory.Text = this.GetResource("Clear Search History", "CLEAR_SEARCH_HISTORY", ResourceType.BUTTON);
			this.Text = this.GetResource("Search", "PAGE_NAME_SEARCH", ResourceType.TITLE);
		}

		private bool LoadSearchResultsInGrid(string sFilePath)
		{
			XmlNodeList xmlNodeLists;
			bool flag;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlNode xmlNodes = null;
				string empty = string.Empty;
				if (!File.Exists(sFilePath))
				{
					flag = false;
				}
				else
				{
					try
					{
						xmlDocument.Load(sFilePath);
					}
					catch
					{
						flag = false;
						return flag;
					}
					bool flag1 = false;
					if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
					{
						flag1 = true;
					}
					if (flag1)
					{
						empty = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = empty;
					}
					xmlNodes = xmlDocument.SelectSingleNode("//Schema");
					if (xmlNodes != null)
					{
						string name = "";
						string str = "";
						string name1 = "";
						string str1 = "";
						foreach (XmlAttribute attribute in xmlNodes.Attributes)
						{
							if (attribute.Value.ToUpper().Equals("ID"))
							{
								name = attribute.Name;
							}
							else if (attribute.Value.ToUpper().Equals("PAGENAME"))
							{
								str = attribute.Name;
							}
							else if (!attribute.Value.ToUpper().Equals("PICTUREFILE"))
							{
								if (!attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
								{
									continue;
								}
								str1 = attribute.Name;
							}
							else
							{
								name1 = attribute.Name;
							}
						}
						if (name == "" || str == "")
						{
							flag = false;
						}
						else
						{
							Filter filter = new Filter(this.frmParent);
							bool hankakuZenkakuFlag = Settings.Default.HankakuZenkakuFlag;
							string[] strArrays = this.frmContainer.ConvertStringWidth(this.searchString);
							if (this.checkBoxMatchCase.Checked)
							{
								if (this.checkBoxExactMatch.Checked)
								{
									if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
									{
										string[] strArrays1 = new string[] { "//Pg[@", str, "=\"", this.searchString, "\"]" };
										xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays1));
									}
									else
									{
										string[] strArrays2 = new string[] { "//Pg[@", str, "=\"", strArrays[0], "\" or @", str, "=\"", strArrays[1], "\"]" };
										xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays2));
									}
								}
								else if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
								{
									string[] strArrays3 = new string[] { "//Pg[contains(@", str, ",\"", this.searchString, "\")]" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays3));
								}
								else
								{
									string[] strArrays4 = new string[] { "//Pg[contains(@", str, ",\"", strArrays[0], "\") or contains(@", str, ",\"", strArrays[1], "\")]" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays4));
								}
							}
							else if (this.checkBoxExactMatch.Checked)
							{
								if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
								{
									string[] upper = new string[] { "//Pg[translate(@", str, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", this.searchString.ToUpper(), "\"]" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper));
									if (xmlNodeLists == null || xmlNodeLists.Count == 0)
									{
										xmlNodeLists = this.SearchWithLINQ(sFilePath, flag1, empty, str, "Pg");
									}
								}
								else
								{
									string[] upper1 = new string[] { "//Pg[translate(@", str, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", strArrays[0].ToUpper(), "\" or translate(@", str, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", strArrays[1].ToUpper(), "\"]" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper1));
								}
							}
							else if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
							{
								string[] upper2 = new string[] { "//Pg[contains(translate(@", str, ",'abcdefghijklmnopqrstuvwxyzççğıöşü','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", this.searchString.ToUpper(), "\")]" };
								xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper2));
								if (xmlNodeLists == null || xmlNodeLists.Count == 0)
								{
									xmlNodeLists = this.SearchWithLINQ(sFilePath, flag1, empty, str, "Pg");
								}
							}
							else
							{
								string[] upper3 = new string[] { "//Pg[contains(translate(@", str, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", strArrays[0].ToUpper(), "\") or contains(translate(@", str, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", strArrays[1].ToUpper(), "\")]" };
								xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper3));
							}
							foreach (XmlNode xmlNodes1 in xmlNodeLists)
							{
								if (filter.FilterPage(xmlNodes, xmlNodes1).Attributes.Count <= 0)
								{
									continue;
								}
								string value = xmlNodes1.Attributes[str].Value;
								XmlDocument xmlDocument1 = new XmlDocument();
								if (xmlNodes1.OuterXml.ToUpper().IndexOf("<PG", 3) <= 0)
								{
									xmlDocument1.LoadXml(xmlNodes1.OuterXml);
								}
								else
								{
									xmlDocument1.LoadXml(string.Concat(xmlNodes1.OuterXml.Substring(0, xmlNodes1.OuterXml.IndexOf("<Pg", 3)), "</Pg>"));
								}
								XmlNode xmlNodes2 = xmlDocument1.SelectSingleNode("//Pic");
								DataGridViewRow dataGridViewRow = new DataGridViewRow();
								for (int i = 0; i < this.dgViewSearch.Columns.Count; i++)
								{
									DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
									if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PAGENAME"))
									{
										if (xmlNodes1.Attributes[str] != null)
										{
											dataGridViewTextBoxCell.Value = xmlNodes1.Attributes[str].Value;
										}
									}
									else if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("ID"))
									{
										if (xmlNodes1.Attributes[name] != null)
										{
											dataGridViewTextBoxCell.Value = xmlNodes1.Attributes[name].Value;
										}
									}
									else if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PICTUREFILE"))
									{
										if (xmlNodes2 != null && xmlNodes2.Attributes[name1] != null)
										{
											dataGridViewTextBoxCell.Value = xmlNodes2.Attributes[name1].Value;
										}
									}
									else if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PARTSLISTFILE") && xmlNodes2 != null && xmlNodes2.Attributes[str1] != null)
									{
										dataGridViewTextBoxCell.Value = xmlNodes2.Attributes[str1].Value;
									}
									dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
								}
								DataGridViewTextBoxCell dataGridViewTextBoxCell1 = new DataGridViewTextBoxCell();
								if (xmlNodes1.Attributes[name] != null && !this.frmParent.lstFilteredPages.Contains(value))
								{
									dataGridViewRow.Tag = xmlNodes1.Attributes[name].Value;
									this.GridViewAddRow(dataGridViewRow);
								}
								Application.DoEvents();
							}
							flag = true;
						}
					}
					else
					{
						flag = false;
					}
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private XmlNodeList SearchWithLINQ(string XmlFilePath, bool bEncrypted, string sDecryptedInnerXML, string attIDElement, string searchType)
		{
			XElement xElement;
			XmlNodeList xmlNodeLists;
			IEnumerable<XElement> xElements = null;
			try
			{
				if (!bEncrypted)
				{
					xElement = XElement.Load(XmlFilePath);
				}
				else
				{
					sDecryptedInnerXML = string.Concat("<Parent>", sDecryptedInnerXML, "</Parent>");
					xElement = XElement.Parse(sDecryptedInnerXML);
				}
				xElements = (!this.checkBoxExactMatch.Checked ? 
					from h in xElement.DescendantsAndSelf(searchType)
					where h.Attribute(attIDElement).Value.ToUpper().Contains(this.searchString.ToUpper())
					select h : 
					from h in xElement.DescendantsAndSelf(searchType)
					where h.Attribute(attIDElement).Value.ToUpper() == this.searchString.ToUpper()
					select h);
				XmlDocument xmlDocument = new XmlDocument();
				string empty = string.Empty;
				empty = string.Concat(empty, "<Parent>");
				foreach (XElement xElement1 in xElements)
				{
					empty = string.Concat(empty, xElement1.ToString(), "\n");
				}
				empty = string.Concat(empty, "</Parent>");
				xmlDocument.LoadXml(empty);
				xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//", searchType));
			}
			catch (Exception exception)
			{
				xmlNodeLists = null;
			}
			return xmlNodeLists;
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					foreach (Control control in parentPanel.Controls)
					{
						if (control == this.picLoading)
						{
							continue;
						}
						control.Enabled = false;
					}
					this.picLoading.Parent = parentPanel;
					this.picLoading.Left = parentPanel.Width / 2 - this.picLoading.Width / 2;
					this.picLoading.Top = parentPanel.Height / 2 - this.picLoading.Height / 2;
					this.picLoading.BringToFront();
					this.picLoading.Show();
				}
				else
				{
					frmPageNameSearch.ShowLoadingDelegate showLoadingDelegate = new frmPageNameSearch.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void txtPageName_TextChanged(object sender, EventArgs e)
		{
			if (this.txtPageName.Text.Trim().Equals(string.Empty))
			{
				this.btnSearch.Enabled = false;
				return;
			}
			this.btnSearch.Enabled = true;
			this.searchString = this.txtPageName.Text;
		}

		private void UpdadetControls()
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					this.pnlForm.Enabled = !this.pnlForm.Enabled;
				}
				else
				{
					base.Invoke(new frmPageNameSearch.UpdadetControlsDelegate(this.UpdadetControls));
				}
			}
			catch
			{
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblPageName.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgViewSearch.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgViewSearch.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		private void UpdateStatus()
		{
			this.lblStatus.Text = this.statusText;
		}

		private delegate void GridViewAddRowDelegate(DataGridViewRow tempRow);

		private delegate void GridViewClearRowsDelegate();

		private delegate void HideLoadingDelegate(Panel parentPanel);

		private delegate void InitializeSearchGridDelegate();

		private delegate void LoadDataGridViewXmlDelegate();

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void UpdadetControlsDelegate();
	}
}
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
	public class frmTextSearch : Form
	{
		private const string dllZipper = "ZIPPER.dll";

		private IContainer components;

		private Label lblTextSearch;

		private Panel pnlControl;

		private Button btnSearch;

		private Panel pnlForm;

		private DataGridView dgViewSearch;

		private Panel pnlSearch;

		private TextBox txtPartNumber;

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

		public bool ISPDF;

		public string sBookType;

		public frmTextSearch(frmViewer frm, GSPcLocalViewer.frmSearch frmSearch)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.frmContainer = frmSearch;
			this.UpdateFont();
			try
			{
				this.InitializeSearchGrid();
				this.LoadDataGridView();
			}
			catch
			{
			}
			this.LoadResources();
			this.sBookType = this.frmParent.objFrmTreeview.sDataType;
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.UpdateStatus();
			this.UpdadetControls();
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			string str1 = string.Empty;
			string item = string.Empty;
			string item1 = string.Empty;
			bool flag = false;
			bool flag1 = false;
			bool flag2 = false;
			item1 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
			item1 = string.Concat(item1, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
			empty = string.Concat(item1, "\\", this.frmParent.BookPublishingId);
			str1 = string.Concat(empty, "\\", this.frmParent.BookPublishingId, "TextSearch.zip");
			str = string.Concat(empty, "\\", this.frmParent.BookPublishingId, "TextSearch.xml");
			empty1 = string.Concat(empty, "\\", this.frmParent.BookPublishingId, "PDFTextSearch.xml");
			if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] == null)
			{
				flag2 = false;
			}
			else
			{
				flag2 = (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() != "ON" ? false : true);
			}
			if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"] == null)
			{
				flag1 = false;
			}
			else
			{
				flag1 = (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() != "ON" ? false : true);
			}
			if (!File.Exists(str) && File.Exists(str1))
			{
				try
				{
					Global.Unzip(str1);
				}
				catch
				{
				}
			}
			if (File.Exists(str))
			{
				int num = 0;
				if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
				{
					num = 0;
				}
				if (num == 0)
				{
					flag = true;
				}
				else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str, flag1, flag2), Global.GetServerUpdateDateFromXmlNode(this.frmParent.SchemaNode, this.frmParent.BookNode), num))
				{
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (flag && !Program.objAppMode.bWorkingOffline)
			{
				this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				item = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
				string empty2 = string.Empty;
				string str2 = string.Empty;
				if (!flag1)
				{
					string[] bookPublishingId = new string[] { item, "/", this.frmParent.BookPublishingId, "/", this.frmParent.BookPublishingId, "TextSearch.xml" };
					empty2 = string.Concat(bookPublishingId);
					str2 = str;
				}
				else
				{
					string[] strArrays = new string[] { item, "/", this.frmParent.BookPublishingId, "/", this.frmParent.BookPublishingId, "TextSearch.zip" };
					empty2 = string.Concat(strArrays);
					str2 = str1;
				}
				(new Download()).DownloadFile(empty2, str2);
				if (flag1 && File.Exists(str1))
				{
					Global.Unzip(str1);
				}
			}
			this.GridViewClearRows();
			if (this.sBookType.ToUpper() != "PDF" && !this.frmParent.ISPDF)
			{
				if (File.Exists(str))
				{
					if (!this.LoadSearchResultsInGrid(str))
					{
						this.statusText = "No result found";
						this.UpdateStatus();
						return;
					}
					this.statusText = string.Concat(this.dgViewSearch.Rows.Count, " ", this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE));
					this.UpdateStatus();
					return;
				}
				if (!File.Exists(empty1) && !File.Exists(str) && !this.frmParent.IsDisposed)
				{
					this.statusText = string.Concat(this.frmParent.BookPublishingId, "TextSearch.xml not found");
					this.UpdateStatus();
				}
			}
			else if (File.Exists(empty1) && !this.frmParent.IsDisposed)
			{
				this.statusText = "Searching";
				this.UpdateStatus();
				if (!this.LoadPDFSearchResultsInGrid(empty1))
				{
					this.statusText = "No result found";
					this.UpdateStatus();
					return;
				}
				this.statusText = string.Concat(this.dgViewSearch.Rows.Count, " ", this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE));
				this.UpdateStatus();
				return;
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
			this.txtPartNumber.AutoCompleteCustomSource.Clear();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.txtPartNumber.AutoCompleteCustomSource.Add(this.txtPartNumber.Text);
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
					Program.DjVuPageNumber = this.dgViewSearch["ImageIndex", this.dgViewSearch.CurrentRow.Index].Value.ToString();
					Program.HighLightText = this.dgViewSearch["Text", this.dgViewSearch.CurrentRow.Index].Value.ToString();
					this.frmParent.OpenTextSearch(this.dgViewSearch["PageName", this.dgViewSearch.CurrentRow.Index].Value.ToString(), this.dgViewSearch["PicIndex", this.dgViewSearch.CurrentRow.Index].Value.ToString(), this.dgViewSearch["Text", this.dgViewSearch.CurrentRow.Index].Value.ToString());
					this.frmParent.Enabled = true;
					if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
					{
						this.frmContainer.CloseContainer();
					}
					Application.DoEvents();
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

		private void frmTextSearch_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.frmParent.Enabled = true;
		}

		private void frmTextSearch_FormClosing(object sender, FormClosingEventArgs e)
		{
			Program.WriteSearchHistory(this.frmParent.sSearchHistoryPath, "TEXTSEARCH", Program.objPartNameSearchHistroyCollection);
		}

		private void frmTextSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.txtPartNumber.Text.Trim() != string.Empty && e.KeyCode == Keys.Return)
			{
				this.btnSearch_Click(null, null);
			}
			if (e.KeyCode == Keys.Escape)
			{
				this.frmContainer.Close();
			}
		}

		private void frmTextSearch_Load(object sender, EventArgs e)
		{
			Program.ReadSearchHistory(this.frmParent.sSearchHistoryPath, "TEXTSEARCH", Program.objTextSearchHistroyCollection);
			this.txtPartNumber.AutoCompleteCustomSource = Program.objTextSearchHistroyCollection;
			this.txtPartNumber.Focus();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='SEARCH']");
				str = string.Concat(str, "/Screen[@Name='TEXT_SEARCH']");
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
			frmTextSearch.GridViewAddRowDelegate gridViewAddRowDelegate = new frmTextSearch.GridViewAddRowDelegate(this.GridViewAddRow);
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
			this.dgViewSearch.Invoke(new frmTextSearch.GridViewClearRowsDelegate(this.GridViewClearRows));
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
					frmTextSearch.HideLoadingDelegate hideLoadingDelegate = new frmTextSearch.HideLoadingDelegate(this.HideLoading);
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
			this.lblTextSearch = new Label();
			this.pnlControl = new Panel();
			this.btnClearHistory = new Button();
			this.btnSearch = new Button();
			this.pnlForm = new Panel();
			this.pnlSearchResults = new Panel();
			this.dgViewSearch = new DataGridView();
			this.picLoading = new PictureBox();
			this.pnlSearch = new Panel();
			this.checkBoxExactMatch = new CheckBox();
			this.checkBoxMatchCase = new CheckBox();
			this.txtPartNumber = new TextBox();
			this.bgWorker = new BackgroundWorker();
			this.ssStatus = new StatusStrip();
			this.lblStatus = new ToolStripStatusLabel();
			this.pnlControl.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.pnlSearchResults.SuspendLayout();
			((ISupportInitialize)this.dgViewSearch).BeginInit();
			((ISupportInitialize)this.picLoading).BeginInit();
			this.pnlSearch.SuspendLayout();
			this.ssStatus.SuspendLayout();
			base.SuspendLayout();
			this.lblTextSearch.BackColor = Color.White;
			this.lblTextSearch.Dock = DockStyle.Top;
			this.lblTextSearch.ForeColor = Color.Black;
			this.lblTextSearch.Location = new Point(10, 0);
			this.lblTextSearch.Name = "lblTextSearch";
			this.lblTextSearch.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblTextSearch.Size = new System.Drawing.Size(478, 27);
			this.lblTextSearch.TabIndex = 16;
			this.lblTextSearch.Text = "Text";
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
			this.pnlForm.Controls.Add(this.lblTextSearch);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.pnlForm.Size = new System.Drawing.Size(500, 428);
			this.pnlForm.TabIndex = 19;
			this.pnlSearchResults.Controls.Add(this.dgViewSearch);
			this.pnlSearchResults.Controls.Add(this.picLoading);
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
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(0, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.TabIndex = 24;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.pnlSearch.Controls.Add(this.checkBoxExactMatch);
			this.pnlSearch.Controls.Add(this.checkBoxMatchCase);
			this.pnlSearch.Controls.Add(this.txtPartNumber);
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
			this.txtPartNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.txtPartNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.txtPartNumber.Location = new Point(4, 17);
			this.txtPartNumber.Name = "txtPartNumber";
			this.txtPartNumber.Size = new System.Drawing.Size(200, 21);
			this.txtPartNumber.TabIndex = 1;
			this.txtPartNumber.TextChanged += new EventHandler(this.txtBookPublishingId_TextChanged);
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
			this.MinimumSize = new System.Drawing.Size(450, 450);
			base.Name = "frmTextSearch";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Search";
			base.Load += new EventHandler(this.frmTextSearch_Load);
			base.FormClosed += new FormClosedEventHandler(this.frmTextSearch_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.frmTextSearch_FormClosing);
			base.KeyDown += new KeyEventHandler(this.frmTextSearch_KeyDown);
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnlSearchResults.ResumeLayout(false);
			((ISupportInitialize)this.dgViewSearch).EndInit();
			((ISupportInitialize)this.picLoading).EndInit();
			this.pnlSearch.ResumeLayout(false);
			this.pnlSearch.PerformLayout();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void InitializeSearchGrid()
		{
			if (this.dgViewSearch.InvokeRequired)
			{
				this.dgViewSearch.Invoke(new frmTextSearch.InitializeSearchGridDelegate(this.InitializeSearchGrid));
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
				this.dgViewSearch.Invoke(new frmTextSearch.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
				return;
			}
			this.dgViewSearch.Columns.Clear();
			ArrayList arrayLists = new ArrayList();
			List<string> strs = new List<string>();
			IniFileIO iniFileIO = new IniFileIO();
			arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "TEXT_SEARCH");
			for (int i = 0; i < arrayLists.Count; i++)
			{
				try
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					string keyValue = iniFileIO1.GetKeyValue("TEXT_SEARCH", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
					Global.AddSearchCol(keyValue, arrayLists[i].ToString(), "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				}
				catch
				{
				}
			}
			if (arrayLists.Count == 0)
			{
				Global.AddSearchCol("true|Text|C|100", "Text", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				Global.AddSearchCol("true|Page Name|C|100", "PageName", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				Global.AddSearchCol("true|DjVu Page Index|C|100", "ImageIndex", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				Global.AddSearchCol("true|Update Date|C|100", "UpdateDate", "TEXT_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
			}
		}

		private bool LoadPDFSearchResultsInGrid(string sFilePath)
		{
			XmlNodeList xmlNodeLists;
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
			bool flag1 = false;
			if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
			{
				flag1 = true;
			}
			if (flag1)
			{
				string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
				xmlDocument.DocumentElement.InnerXml = str;
			}
			if (!this.checkBoxMatchCase.Checked)
			{
				xmlNodeLists = (!this.checkBoxExactMatch.Checked ? xmlDocument.SelectNodes(string.Concat("//PAGE[contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", this.searchString.ToUpper(), "\")]")) : xmlDocument.SelectNodes(string.Concat("//PDFXML[translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", this.searchString.ToUpper(), "\"]")));
			}
			else
			{
				xmlNodeLists = (!this.checkBoxExactMatch.Checked ? xmlDocument.SelectNodes(string.Concat("//PDFXML[contains(text(),\"", this.searchString, "\")]")) : xmlDocument.SelectNodes(string.Concat("//PDFXML[text()=\"", this.searchString, "\"]")));
			}
			foreach (XmlNode xmlNodes in xmlNodeLists)
			{
				(new XmlDocument()).LoadXml(xmlNodes.OuterXml);
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				for (int i = 0; i < this.dgViewSearch.Columns.Count; i++)
				{
					DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
					XmlNode parentNode = null;
					parentNode = xmlNodes.ParentNode;
					if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("TEXT"))
					{
						dataGridViewTextBoxCell.Value = this.txtPartNumber.Text;
					}
					if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PAGENAME"))
					{
						if (parentNode.Attributes["pagename"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["pagename"].Value;
						}
						else if (parentNode.Attributes["PageName"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["PageName"].Value;
						}
					}
					if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PUBLISHINGID"))
					{
						if (parentNode.Attributes["publishingid"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["publishingid"].Value;
						}
						else if (parentNode.Attributes["PublishingId"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["PublishingId"].Value;
						}
					}
					if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("BOOKCODE"))
					{
						if (parentNode.Attributes["bookcode"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["bookcode"].Value;
						}
						else if (parentNode.Attributes["BookCode"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["BookCode"].Value;
						}
					}
					if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("UPDATEDATE"))
					{
						if (parentNode.Attributes["updatedate"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["updatedate"].Value;
						}
						else if (parentNode.Attributes["UpdateDate"] != null)
						{
							dataGridViewTextBoxCell.Value = parentNode.Attributes["UpdateDate"].Value;
						}
					}
					if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("IMAGEINDEX"))
					{
						dataGridViewTextBoxCell.Value = xmlNodes.Attributes["number"].Value;
					}
					if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PICINDEX"))
					{
						dataGridViewTextBoxCell.Value = "1";
						string empty = string.Empty;
						empty = parentNode.Attributes["data"].Value;
						empty = empty.Substring(empty.LastIndexOf("/") + 1);
						XmlDocument xmlDocument1 = new XmlDocument();
						TreeNode treeNode = this.frmParent.objFrmTreeview.FindTreeNodeByPageName(this.frmParent.objFrmTreeview.tvBook.Nodes, parentNode.Attributes["pagename"].Value);
						if (treeNode != null)
						{
							XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.frmParent.objFrmTreeview.tvBook.Tag.ToString()));
							XmlNode xmlNodes1 = xmlDocument1.ReadNode(xmlTextReader);
							string name = string.Empty;
							for (int j = 0; j < xmlNodes1.Attributes.Count; j++)
							{
								if (xmlNodes1.Attributes[j].Value.ToUpper().Equals("PICTUREFILE"))
								{
									name = xmlNodes1.Attributes[j].Name;
								}
							}
							xmlTextReader = new XmlTextReader(new StringReader(treeNode.Tag.ToString()));
							XmlNode xmlNodes2 = xmlDocument1.ReadNode(xmlTextReader);
							int num = 1;
							foreach (XmlNode childNode in xmlNodes2.ChildNodes)
							{
								if (childNode.Attributes[name].Value == empty)
								{
									dataGridViewTextBoxCell.Value = num;
								}
								num++;
							}
						}
					}
					dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
				}
				this.GridViewAddRow(dataGridViewRow);
				Application.DoEvents();
			}
			return true;
		}

		private void LoadResources()
		{
			this.lblTextSearch.Text = this.GetResource("Text Search", "TEXT", ResourceType.LABEL);
			this.checkBoxMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
			this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
			this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
			this.btnClearHistory.Text = this.GetResource("Clear Search History", "CLEAR_SEARCH_HISTORY", ResourceType.BUTTON);
			this.Text = this.GetResource("Text Search", "TEXT_SEARCH", ResourceType.TITLE);
		}

		private bool LoadSearchResultsInGrid(string sFilePath)
		{
			XmlNodeList xmlNodeLists;
			bool flag;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
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
					XmlNodeList xmlNodeLists1 = xmlDocument.SelectNodes("//WORD[CHAR]");
					if (xmlNodeLists1.Count > 0)
					{
						for (int i = 0; i < xmlNodeLists1.Count; i++)
						{
							string str = "";
							foreach (XmlNode childNode in xmlNodeLists1[i].ChildNodes)
							{
								str = string.Concat(str, childNode.InnerText);
							}
							xmlNodeLists1[i].InnerText = str;
						}
					}
					bool hankakuZenkakuFlag = Settings.Default.HankakuZenkakuFlag;
					string[] strArrays = this.frmContainer.ConvertStringWidth(this.searchString);
					if (this.checkBoxMatchCase.Checked)
					{
						if (this.checkBoxExactMatch.Checked)
						{
							if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
							{
								xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//WORD[text()=\"", this.searchString, "\"]"));
							}
							else
							{
								string[] strArrays1 = new string[] { "//WORD[text()=\"", strArrays[0], "\" or text()=\"", strArrays[1], "\"]" };
								xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays1));
							}
						}
						else if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
						{
							xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//WORD[contains(text(),\"", this.searchString, "\")]"));
						}
						else
						{
							string[] strArrays2 = new string[] { "//WORD[contains(text(),\"", strArrays[0], "\") or contains(text(),\"", strArrays[1], "\")]" };
							xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays2));
						}
					}
					else if (this.checkBoxExactMatch.Checked)
					{
						if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
						{
							xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//WORD[translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", this.searchString.ToUpper(), "\"]"));
							if (xmlNodeLists == null || xmlNodeLists.Count == 0)
							{
								xmlNodeLists = this.SearchWithLINQ(sFilePath, flag1, empty, "", "BODY");
							}
						}
						else
						{
							string[] upper = new string[] { "//WORD[translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", strArrays[0].ToUpper(), "\" or translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", strArrays[1].ToUpper(), "\"]" };
							xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper));
						}
					}
					else if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
					{
						xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//WORD[contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", this.searchString.ToUpper(), "\")]"));
						if (xmlNodeLists == null || xmlNodeLists.Count == 0)
						{
							xmlNodeLists = this.SearchWithLINQ(sFilePath, flag1, empty, "", "BODY");
						}
					}
					else
					{
						string[] upper1 = new string[] { "//WORD[contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", strArrays[0].ToUpper(), "\") or contains(translate(text(),'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", strArrays[1].ToUpper(), "\")]" };
						xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper1));
					}
					foreach (XmlNode xmlNodes in xmlNodeLists)
					{
						(new XmlDocument()).LoadXml(xmlNodes.OuterXml);
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						for (int j = 0; j < this.dgViewSearch.Columns.Count; j++)
						{
							DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
							XmlNode parentNode = null;
							parentNode = xmlNodes;
							if (parentNode.Name.ToUpper() != "BODY")
							{
								do
								{
									if (parentNode.ParentNode == null)
									{
										break;
									}
									parentNode = parentNode.ParentNode;
								}
								while (parentNode.ParentNode.Name.ToUpper() != "BODY");
							}
							else
							{
								parentNode = parentNode.SelectSingleNode("//OBJECT");
							}
							if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("TEXT"))
							{
								dataGridViewTextBoxCell.Value = xmlNodes.InnerText;
							}
							else if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("COORDS"))
							{
								dataGridViewTextBoxCell.Value = xmlNodes.Attributes["coords"].Value;
							}
							else if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("PAGENAME"))
							{
								if (parentNode.Attributes["PageName"] != null)
								{
									dataGridViewTextBoxCell.Value = parentNode.Attributes["PageName"].Value;
								}
							}
							else if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("PUBLISHINGID"))
							{
								if (parentNode.Attributes["PublishingId"] != null)
								{
									dataGridViewTextBoxCell.Value = parentNode.Attributes["PublishingId"].Value;
								}
							}
							else if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("BOOKCODE"))
							{
								if (parentNode.Attributes["BookCode"] != null)
								{
									dataGridViewTextBoxCell.Value = parentNode.Attributes["BookCode"].Value;
								}
							}
							else if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("UPDATEDATE"))
							{
								if (parentNode.Attributes["UpdateDate"] != null)
								{
									dataGridViewTextBoxCell.Value = parentNode.Attributes["UpdateDate"].Value;
								}
							}
							else if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("IMAGEINDEX"))
							{
								XmlNode previousSibling = parentNode;
								int num = 0;
								while (previousSibling.Name != "BODY")
								{
									if (previousSibling.PreviousSibling == null)
									{
										num = 1;
									}
									else
									{
										while (previousSibling.PreviousSibling.Name == "MAP" || previousSibling.PreviousSibling.Name == "OBJECT")
										{
											if (previousSibling.PreviousSibling.Name != "MAP")
											{
												num++;
											}
											previousSibling = previousSibling.PreviousSibling;
											if (previousSibling.PreviousSibling != null)
											{
												continue;
											}
											num++;
											goto Label1;
										}
									}
								Label1:
									previousSibling = previousSibling.ParentNode;
								}
								dataGridViewTextBoxCell.Value = num;
							}
							else if (this.dgViewSearch.Columns[j].Name.ToUpper().Equals("PICINDEX"))
							{
								dataGridViewTextBoxCell.Value = "1";
								string value = string.Empty;
								value = parentNode.Attributes["data"].Value;
								value = value.Substring(value.LastIndexOf("/") + 1);
								XmlDocument xmlDocument1 = new XmlDocument();
								TreeNode treeNode = this.frmParent.objFrmTreeview.FindTreeNodeByPageName(this.frmParent.objFrmTreeview.tvBook.Nodes, parentNode.Attributes["PageName"].Value);
								if (treeNode != null)
								{
									XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.frmParent.objFrmTreeview.tvBook.Tag.ToString()));
									XmlNode xmlNodes1 = xmlDocument1.ReadNode(xmlTextReader);
									string name = string.Empty;
									for (int k = 0; k < xmlNodes1.Attributes.Count; k++)
									{
										if (xmlNodes1.Attributes[k].Value.ToUpper().Equals("PICTUREFILE"))
										{
											name = xmlNodes1.Attributes[k].Name;
										}
									}
									xmlTextReader = new XmlTextReader(new StringReader(treeNode.Tag.ToString()));
									XmlNode xmlNodes2 = xmlDocument1.ReadNode(xmlTextReader);
									int num1 = 1;
									foreach (XmlNode childNode1 in xmlNodes2.ChildNodes)
									{
										if (childNode1.Attributes[name].Value == value)
										{
											dataGridViewTextBoxCell.Value = num1;
										}
										num1++;
									}
								}
							}
							dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
						}
						this.GridViewAddRow(dataGridViewRow);
						Application.DoEvents();
					}
					flag = true;
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
					where h.Value.ToUpper().Contains(this.searchString.ToUpper())
					select h : 
					from h in xElement.DescendantsAndSelf(searchType)
					where h.Value.ToUpper() == this.searchString.ToUpper()
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
					frmTextSearch.ShowLoadingDelegate showLoadingDelegate = new frmTextSearch.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void txtBookPublishingId_TextChanged(object sender, EventArgs e)
		{
			if (this.txtPartNumber.Text.Trim().Equals(string.Empty))
			{
				this.btnSearch.Enabled = false;
				this.statusText = string.Empty;
				this.UpdateStatus();
				return;
			}
			this.btnSearch.Enabled = true;
			this.searchString = this.txtPartNumber.Text;
		}

		private void UpdadetControls()
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					this.pnlSearch.Enabled = !this.pnlSearch.Enabled;
					this.pnlControl.Enabled = !this.pnlControl.Enabled;
				}
				else
				{
					base.Invoke(new frmTextSearch.UpdadetControlsDelegate(this.UpdadetControls));
				}
			}
			catch
			{
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblTextSearch.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
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
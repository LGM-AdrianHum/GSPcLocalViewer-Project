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
	public class frmPartNameSearch : Form
	{
		private frmViewer frmParent;

		private frmSearch frmContainer;

		private XmlNodeList xnlPartlist;

		private string statusText;

		private string searchString;

		private string attPartIdElement;

		private string attPartNameElement;

		private string attPartsListItem = string.Empty;

		private string attPageIdItem = string.Empty;

		private string attPageNameItem = string.Empty;

		private string attPartNumber = string.Empty;

		private string attPartName = string.Empty;

		private IContainer components;

		private Label lblPartName;

		private Panel pnlControl;

		private Button btnSearch;

		private Panel pnlForm;

		private DataGridView dgViewSearch;

		private Panel pnlSearch;

		private TextBox txtPartName;

		private CheckBox checkBoxExactMatch;

		private CheckBox checkBoxMatchCase;

		private BackgroundWorker bgWorker;

		private Panel pnlSearchResults;

		private PictureBox picLoading;

		private StatusStrip ssStatus;

		private ToolStripStatusLabel lblStatus;

		private Button btnClearHistory;

		private CheckBox checkBoxSearchWholeBook;

		public frmPartNameSearch(frmViewer frm, GSPcLocalViewer.frmSearch frmSearch)
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
			this.UpdateStatus();
			this.UpdadetControls();
			if (!this.checkBoxSearchWholeBook.Checked)
			{
				this.SearchPartsListXMLs();
			}
			else if (!this.SearchInBookSearchXML())
			{
				this.SearchPartsListXMLs();
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
			this.txtPartName.AutoCompleteCustomSource.Clear();
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.GridViewClearRows();
			this.txtPartName.AutoCompleteCustomSource.Add(this.txtPartName.Text);
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
					string str = string.Empty;
					if (this.dgViewSearch["PageId", this.dgViewSearch.CurrentRow.Index].Value != null)
					{
						empty = this.dgViewSearch["PageId", this.dgViewSearch.CurrentRow.Index].Value.ToString();
					}
					if (this.dgViewSearch["PartName", this.dgViewSearch.CurrentRow.Index].Value != null)
					{
						str = this.dgViewSearch["PartNumber", this.dgViewSearch.CurrentRow.Index].Value.ToString();
					}
					this.frmParent.OpenSearch(Program.iniServers[this.frmParent.ServerId].sIniKey, this.frmParent.BookPublishingId, empty, "1", "1", str);
					this.frmParent.Enabled = true;
					this.frmContainer.CloseContainer();
				}
			}
			catch (Exception exception)
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

		private void frmPartNameSearch_FormClosing(object sender, FormClosingEventArgs e)
		{
			Program.WriteSearchHistory(this.frmParent.sSearchHistoryPath, "PARTNAMESEARCH", Program.objPartNameSearchHistroyCollection);
		}

		private void frmPartNameSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.txtPartName.Text.Trim() != string.Empty && e.KeyCode == Keys.Return)
			{
				this.btnSearch_Click(null, null);
			}
			if (e.KeyCode == Keys.Escape)
			{
				this.frmContainer.Close();
			}
		}

		private void frmPartNameSearch_Load(object sender, EventArgs e)
		{
			Program.ReadSearchHistory(this.frmParent.sSearchHistoryPath, "PARTNAMESEARCH", Program.objPartNameSearchHistroyCollection);
			this.txtPartName.AutoCompleteCustomSource = Program.objPartNameSearchHistroyCollection;
			this.txtPartName.Focus();
		}

		private void frmPartSearch_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.frmParent.Enabled = true;
		}

		private string GetAdvSearchMatchKeys(bool bIsBookXML, XmlNode xndSchema)
		{
			string str;
			string empty = string.Empty;
			try
			{
				ArrayList arrayLists = new ArrayList();
				IniFileIO iniFileIO = new IniFileIO();
				arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "ADVANCE_SEARCH_MATCHING_KEYS");
				if (arrayLists.Count <= 0)
				{
					empty = (bIsBookXML ? "a4" : "a3");
				}
				else
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					if (!bIsBookXML)
					{
						int num = 0;
						while (num < arrayLists.Count)
						{
							string keyValue = iniFileIO1.GetKeyValue("ADVANCE_SEARCH_MATCHING_KEYS", arrayLists[num].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
							if (arrayLists[num].ToString().ToUpper() != "BOOK_SEARCH_XML_MATCHING_KEY")
							{
								num++;
							}
							else
							{
								empty = keyValue;
								if (this.ValidateAdvSearchKey(empty, xndSchema))
								{
									break;
								}
								empty = (bIsBookXML ? "a4" : "a3");
								break;
							}
						}
						if (empty == string.Empty)
						{
							empty = (bIsBookXML ? "a4" : "a3");
						}
					}
					else
					{
						int num1 = 0;
						while (num1 < arrayLists.Count)
						{
							string keyValue1 = iniFileIO1.GetKeyValue("ADVANCE_SEARCH_MATCHING_KEYS", arrayLists[num1].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
							if (arrayLists[num1].ToString().ToUpper() != "BOOK_XML_MATCHING_KEY")
							{
								num1++;
							}
							else
							{
								empty = keyValue1;
								if (this.ValidateAdvSearchKey(empty, xndSchema))
								{
									break;
								}
								empty = (bIsBookXML ? "a4" : "a3");
								break;
							}
						}
						if (empty == string.Empty)
						{
							empty = (bIsBookXML ? "a4" : "a3");
						}
					}
				}
				str = empty;
			}
			catch (Exception exception)
			{
				str = (bIsBookXML ? "a4" : "a3");
			}
			return str;
		}

		private XmlDocument GetBookXmlDoc(string sBookXmlPath, bool bEncrypted, ref XmlNode xSchemaNode)
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				if (File.Exists(sBookXmlPath))
				{
					try
					{
						xmlDocument.Load(sBookXmlPath);
					}
					catch
					{
					}
					if (bEncrypted)
					{
						string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = str;
					}
					xSchemaNode = xmlDocument.SelectSingleNode("//Schema");
				}
			}
			catch
			{
			}
			return xmlDocument;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='SEARCH']");
				str = string.Concat(str, "/Screen[@Name='PART_NAME_SEARCH']");
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
			frmPartNameSearch.GridViewAddRowDelegate gridViewAddRowDelegate = new frmPartNameSearch.GridViewAddRowDelegate(this.GridViewAddRow);
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
			this.dgViewSearch.Invoke(new frmPartNameSearch.GridViewClearRowsDelegate(this.GridViewClearRows));
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
					frmPartNameSearch.HideLoadingDelegate hideLoadingDelegate = new frmPartNameSearch.HideLoadingDelegate(this.HideLoading);
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
			this.lblPartName = new Label();
			this.pnlControl = new Panel();
			this.btnClearHistory = new Button();
			this.btnSearch = new Button();
			this.pnlForm = new Panel();
			this.pnlSearchResults = new Panel();
			this.dgViewSearch = new DataGridView();
			this.pnlSearch = new Panel();
			this.checkBoxSearchWholeBook = new CheckBox();
			this.checkBoxExactMatch = new CheckBox();
			this.checkBoxMatchCase = new CheckBox();
			this.txtPartName = new TextBox();
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
			this.lblPartName.BackColor = Color.White;
			this.lblPartName.Dock = DockStyle.Top;
			this.lblPartName.ForeColor = Color.Black;
			this.lblPartName.Location = new Point(10, 0);
			this.lblPartName.Name = "lblPartName";
			this.lblPartName.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblPartName.Size = new System.Drawing.Size(478, 27);
			this.lblPartName.TabIndex = 16;
			this.lblPartName.Text = "Part Name";
			this.pnlControl.Controls.Add(this.checkBoxSearchWholeBook);
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
			this.btnClearHistory.TabIndex = 4;
			this.btnClearHistory.Text = "Clear Search History";
			this.btnClearHistory.UseVisualStyleBackColor = true;
			this.btnClearHistory.Click += new EventHandler(this.btnClearHistory_Click);
			this.btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnSearch.Enabled = false;
			this.btnSearch.Location = new Point(350, 5);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(114, 23);
			this.btnSearch.TabIndex = 5;
			this.btnSearch.Text = "Search";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlSearchResults);
			this.pnlForm.Controls.Add(this.pnlSearch);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Controls.Add(this.lblPartName);
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
			this.pnlSearch.Controls.Add(this.txtPartName);
			this.pnlSearch.Dock = DockStyle.Top;
			this.pnlSearch.Location = new Point(10, 27);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Size = new System.Drawing.Size(478, 55);
			this.pnlSearch.TabIndex = 21;
			this.checkBoxSearchWholeBook.AutoSize = true;
			this.checkBoxSearchWholeBook.Checked = true;
			this.checkBoxSearchWholeBook.CheckState = CheckState.Checked;
			this.checkBoxSearchWholeBook.Location = new Point(4, 9);
			this.checkBoxSearchWholeBook.Name = "checkBoxSearchWholeBook";
			this.checkBoxSearchWholeBook.Size = new System.Drawing.Size(116, 17);
			this.checkBoxSearchWholeBook.TabIndex = 4;
			this.checkBoxSearchWholeBook.Text = "Search whole book";
			this.checkBoxSearchWholeBook.UseVisualStyleBackColor = true;
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
			this.txtPartName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			this.txtPartName.AutoCompleteSource = AutoCompleteSource.CustomSource;
			this.txtPartName.Location = new Point(4, 17);
			this.txtPartName.Name = "txtPartName";
			this.txtPartName.Size = new System.Drawing.Size(200, 21);
			this.txtPartName.TabIndex = 1;
			this.txtPartName.TextChanged += new EventHandler(this.txtPartName_TextChanged);
			this.txtPartName.KeyDown += new KeyEventHandler(this.txtPartName_KeyDown);
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(379, 246);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(34, 34);
			this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
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
			this.ssStatus.TabIndex = 20;
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
			base.Name = "frmPartNameSearch";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Search";
			base.Load += new EventHandler(this.frmPartNameSearch_Load);
			base.FormClosed += new FormClosedEventHandler(this.frmPartSearch_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.frmPartNameSearch_FormClosing);
			base.KeyDown += new KeyEventHandler(this.frmPartNameSearch_KeyDown);
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			this.pnlForm.ResumeLayout(false);
			this.pnlForm.PerformLayout();
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
				this.dgViewSearch.Invoke(new frmPartNameSearch.InitializeSearchGridDelegate(this.InitializeSearchGrid));
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
				this.dgViewSearch.Invoke(new frmPartNameSearch.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
				return;
			}
			this.dgViewSearch.Columns.Clear();
			ArrayList arrayLists = new ArrayList();
			List<string> strs = new List<string>();
			IniFileIO iniFileIO = new IniFileIO();
			arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PARTNAME_SEARCH");
			for (int i = 0; i < arrayLists.Count; i++)
			{
				try
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					string keyValue = iniFileIO1.GetKeyValue("PARTNAME_SEARCH", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
					Global.AddSearchCol(keyValue, arrayLists[i].ToString(), "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				}
				catch
				{
				}
			}
			if (arrayLists.Count == 0)
			{
				Global.AddSearchCol("true|Part Name|C|100", "PartName", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				Global.AddSearchCol("true|Part Number|C|100", "PartNumber", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				Global.AddSearchCol("true|Page Name|C|100", "PageName", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
				Global.AddSearchCol("true|Page Id|C|100", "PageId", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
			}
		}

		private XmlNodeList LoadPartsListNames(string BookXMLPath)
		{
			XmlNodeList xmlNodeLists;
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(BookXMLPath))
			{
				return null;
			}
			try
			{
				xmlDocument.Load(BookXMLPath);
				goto Label0;
			}
			catch
			{
				xmlNodeLists = null;
			}
			return xmlNodeLists;
		Label0:
			bool flag = false;
			if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
			{
				flag = true;
			}
			if (flag)
			{
				string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
				xmlDocument.DocumentElement.InnerXml = str;
			}
			XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
			if (xmlNodes == null)
			{
				return null;
			}
			string empty = string.Empty;
			string name = string.Empty;
			foreach (XmlAttribute attribute in xmlNodes.Attributes)
			{
				if (!attribute.Value.ToUpper().Equals("ID"))
				{
					if (!attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
					{
						continue;
					}
					name = attribute.Name;
				}
				else
				{
					empty = attribute.Name;
				}
			}
			if (empty == "" || name == "")
			{
				return null;
			}
			return xmlDocument.SelectNodes("//Pic");
		}

		private void LoadResources()
		{
			this.lblPartName.Text = this.GetResource("Part Name", "PART_NAME", ResourceType.LABEL);
			this.checkBoxMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
			this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
			this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
			this.btnClearHistory.Text = this.GetResource("Clear Search History", "CLEAR_SEARCH_HISTORY", ResourceType.BUTTON);
			this.checkBoxSearchWholeBook.Text = this.GetResource("Search Whole Book", "SEARCH_WHOLE_BOOK", ResourceType.CHECK_BOX);
			this.Text = this.GetResource("Search", "PART_NAME_SEARCH", ResourceType.TITLE);
		}

		private void LoadSchemaValues(string _BookXmlPath)
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(_BookXmlPath);
				bool flag = false;
				if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
				{
					flag = true;
				}
				if (flag)
				{
					string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
					xmlDocument.DocumentElement.InnerXml = str;
				}
				XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
				if (xmlNodes != null)
				{
					foreach (XmlAttribute attribute in xmlNodes.Attributes)
					{
						if (attribute.Value.ToUpper().Equals("PAGENAME"))
						{
							this.attPageNameItem = attribute.Name;
						}
						if (attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
						{
							this.attPartsListItem = attribute.Name;
						}
						if (attribute.Value.ToUpper().Equals("ID"))
						{
							this.attPageIdItem = attribute.Name;
						}
						if (attribute.Value.ToUpper().Equals("PARTNAME"))
						{
							this.attPartName = attribute.Name;
						}
						if (!attribute.Value.ToUpper().Equals("PARTNUMBER"))
						{
							continue;
						}
						this.attPartNumber = attribute.Name;
					}
				}
			}
			catch
			{
			}
		}

		private bool LoadSearchResultsInGrid(string sFilePath, string PageName, string PageId)
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
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
					if (xmlNodes != null)
					{
						this.attPartIdElement = string.Empty;
						this.attPartNameElement = string.Empty;
						foreach (XmlAttribute attribute in xmlNodes.Attributes)
						{
							if (!attribute.Value.ToUpper().Equals("ID"))
							{
								if (!attribute.Value.ToUpper().Equals("PARTNAME"))
								{
									continue;
								}
								this.attPartNameElement = attribute.Name;
							}
							else
							{
								this.attPartIdElement = attribute.Name;
							}
						}
						if (this.attPartIdElement == string.Empty || this.attPartNameElement == string.Empty)
						{
							flag = false;
						}
						else
						{
							bool hankakuZenkakuFlag = Settings.Default.HankakuZenkakuFlag;
							string[] strArrays = this.frmContainer.ConvertStringWidth(this.searchString);
							if (this.checkBoxMatchCase.Checked)
							{
								if (this.checkBoxExactMatch.Checked)
								{
									if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
									{
										string[] strArrays1 = new string[] { "//Part[@", this.attPartNameElement, "='", this.searchString, "']" };
										xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays1));
									}
									else
									{
										string[] strArrays2 = new string[] { "//Part[@", this.attPartNameElement, "='", strArrays[0], "' or @", this.attPartNameElement, "='", strArrays[1], "']" };
										xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays2));
									}
								}
								else if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
								{
									string[] strArrays3 = new string[] { "//Part[contains(@", this.attPartNameElement, ",'", this.searchString, "')]" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays3));
								}
								else
								{
									string[] strArrays4 = new string[] { "//Part[contains(@", this.attPartNameElement, ",'", strArrays[0], "') or contains(@", this.attPartNameElement, ",'", strArrays[1], "')]" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays4));
								}
							}
							else if (this.checkBoxExactMatch.Checked)
							{
								if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
								{
									string[] upper = new string[] { "//Part[translate(@", this.attPartNameElement, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", this.searchString.ToUpper(), "']" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper));
									if (xmlNodeLists == null || xmlNodeLists.Count == 0)
									{
										xmlNodeLists = this.SearchWithLINQ(sFilePath, flag1, empty, this.attPartNameElement, "Part");
									}
								}
								else
								{
									string[] upper1 = new string[] { "//Part[translate(@", this.attPartNameElement, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", strArrays[0].ToUpper(), "' or translate(@", this.attPartNameElement, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", strArrays[1].ToUpper(), "']" };
									xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper1));
								}
							}
							else if (!hankakuZenkakuFlag || (int)strArrays.Length != 2)
							{
								string[] upper2 = new string[] { "//Part[contains(translate(@", this.attPartNameElement, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'", this.searchString.ToUpper(), "')]" };
								xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper2));
								if (xmlNodeLists == null || xmlNodeLists.Count == 0)
								{
									xmlNodeLists = this.SearchWithLINQ(sFilePath, flag1, empty, this.attPartNameElement, "Part");
								}
							}
							else
							{
								string[] upper3 = new string[] { "//Part[contains(translate(@", this.attPartNameElement, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'", strArrays[0].ToUpper(), "') or contains(translate(@", this.attPartNameElement, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'", strArrays[1].ToUpper(), "')]" };
								xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper3));
							}
							xmlNodeLists = (new Filter(this.frmParent)).FilterPartsList(xmlNodes, xmlNodeLists);
							foreach (XmlNode xmlNodes1 in xmlNodeLists)
							{
								if (xmlNodes1.Attributes.Count <= 0)
								{
									continue;
								}
								(new XmlDocument()).LoadXml(xmlNodes1.OuterXml);
								DataGridViewRow dataGridViewRow = new DataGridViewRow();
								for (int i = 0; i < this.dgViewSearch.Columns.Count; i++)
								{
									DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
									foreach (XmlAttribute xmlAttribute in xmlNodes.Attributes)
									{
										try
										{
											if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals(xmlAttribute.Value.ToUpper()) && xmlNodes1 != null && xmlNodes1.Attributes[xmlAttribute.Name] != null)
											{
												dataGridViewTextBoxCell.Value = xmlNodes1.Attributes[xmlAttribute.Name].Value;
											}
										}
										catch
										{
										}
									}
									try
									{
										if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PAGENAME"))
										{
											dataGridViewTextBoxCell.Value = PageName;
										}
										else if (this.dgViewSearch.Columns[i].Name.ToUpper().Equals("PAGEID"))
										{
											dataGridViewTextBoxCell.Value = PageId;
										}
										dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
									}
									catch
									{
									}
								}
								if (!this.frmParent.lstFilteredPages.Contains(PageName))
								{
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

		private bool LoadSearchResultsInGridA(string sFilePath, bool bEncrypted, string attBookXmlPageIdElement, string attBookXmlPageNameElement, XmlDocument objBookXmlDoc)
		{
			XmlNodeList xmlNodeLists;
			bool flag;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
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
					if (bEncrypted)
					{
						string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = str;
					}
					if (this.checkBoxMatchCase.Checked)
					{
						if (!this.checkBoxExactMatch.Checked)
						{
							string[] strArrays = new string[] { "//Part[contains(@", this.attPartName, ",\"", this.searchString, "\")]" };
							xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays));
						}
						else
						{
							string[] strArrays1 = new string[] { "//Part[@", this.attPartName, "=\"", this.searchString, "\"]" };
							xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays1));
						}
					}
					else if (!this.checkBoxExactMatch.Checked)
					{
						string[] upper = new string[] { "//Part[contains(translate(@", this.attPartName, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"", this.searchString.ToUpper(), "\")]" };
						xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper));
					}
					else
					{
						string[] upper1 = new string[] { "//Part[translate(@", this.attPartName, ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"", this.searchString.ToUpper(), "\"]" };
						xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper1));
					}
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
					xmlNodeLists = (new Filter(this.frmParent)).FilterPartsList(xmlNodes, xmlNodeLists);
					string empty = string.Empty;
					foreach (XmlNode xmlNodes1 in xmlNodeLists)
					{
						if (xmlNodes1.Attributes.Count <= 0)
						{
							continue;
						}
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						for (int i = 0; i < this.dgViewSearch.Columns.Count; i++)
						{
							DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell();
							foreach (XmlAttribute attribute in xmlNodes1.Attributes)
							{
								string value = string.Empty;
								if (xmlNodes.Attributes[attribute.Name] == null)
								{
									continue;
								}
								value = xmlNodes.Attributes[attribute.Name].Value;
								if (this.dgViewSearch.Columns[i].Name.ToUpper() == "PAGEID")
								{
									try
									{
										string[] strArrays2 = new string[] { "//Pg[@", attBookXmlPageNameElement, "=\"", xmlNodes1.Attributes[this.attPageNameItem].Value, "\"]" };
										XmlNode parentNode = objBookXmlDoc.SelectSingleNode(string.Concat(strArrays2));
										if (parentNode == null)
										{
											XmlNode xmlNodes2 = objBookXmlDoc.SelectSingleNode("//Schema");
											this.attPageNameItem = this.GetAdvSearchMatchKeys(false, xmlNodes);
											attBookXmlPageNameElement = this.GetAdvSearchMatchKeys(true, xmlNodes2);
											string[] strArrays3 = new string[] { "//Pic[@", attBookXmlPageNameElement, "=\"", xmlNodes1.Attributes[this.attPageNameItem].Value, "\"]" };
											XmlNode xmlNodes3 = objBookXmlDoc.SelectSingleNode(string.Concat(strArrays3));
											if (xmlNodes3 != null)
											{
												parentNode = xmlNodes3.ParentNode;
											}
											dataGridViewTextBoxCell.Value = parentNode.Attributes[attBookXmlPageIdElement].Value;
										}
										else
										{
											empty = parentNode.Attributes[attBookXmlPageNameElement].Value;
											dataGridViewTextBoxCell.Value = parentNode.Attributes[attBookXmlPageIdElement].Value;
										}
									}
									catch
									{
									}
								}
								else
								{
									if (!this.dgViewSearch.Columns[i].Name.ToUpper().Equals(value.ToUpper()))
									{
										continue;
									}
									dataGridViewTextBoxCell.Value = xmlNodes1.Attributes[attribute.Name].Value;
								}
							}
							dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
						}
						if (!this.frmParent.lstFilteredPages.Contains(empty))
						{
							this.GridViewAddRow(dataGridViewRow);
						}
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

		private bool SearchInBookSearchXML()
		{
			bool flag;
			try
			{
				string empty = string.Empty;
				string str = string.Empty;
				string empty1 = string.Empty;
				string item = string.Empty;
				string item1 = string.Empty;
				bool flag1 = false;
				bool flag2 = false;
				bool flag3 = false;
				item1 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				item1 = string.Concat(item1, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
				empty = string.Concat(item1, "\\", this.frmParent.BookPublishingId);
				empty1 = string.Concat(empty, "\\", this.frmParent.BookPublishingId, "Search.zip");
				str = string.Concat(empty, "\\", this.frmParent.BookPublishingId, "Search.xml");
				if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] == null)
				{
					flag3 = false;
				}
				else
				{
					flag3 = (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() != "ON" ? false : true);
				}
				if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"] == null)
				{
					flag2 = false;
				}
				else
				{
					flag2 = (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() != "ON" ? false : true);
				}
				if (!File.Exists(str) && File.Exists(empty1))
				{
					try
					{
						Global.Unzip(empty1);
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
						flag1 = true;
					}
					else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str, flag2, flag3), Global.GetServerUpdateDateFromXmlNode(this.frmParent.SchemaNode, this.frmParent.BookNode), num))
					{
						flag1 = true;
					}
				}
				else
				{
					flag1 = true;
				}
				if (flag1 && !Program.objAppMode.bWorkingOffline)
				{
					this.GetResource("No result found", "NO_RESULT", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					item = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
					string str1 = string.Empty;
					string empty2 = string.Empty;
					if (!flag2)
					{
						string[] bookPublishingId = new string[] { item, "/", this.frmParent.BookPublishingId, "/", this.frmParent.BookPublishingId, "Search.xml" };
						str1 = string.Concat(bookPublishingId);
						empty2 = str;
					}
					else
					{
						string[] strArrays = new string[] { item, "/", this.frmParent.BookPublishingId, "/", this.frmParent.BookPublishingId, "Search.zip" };
						str1 = string.Concat(strArrays);
						empty2 = empty1;
					}
					if ((new Download()).DownloadFile(str1, empty2))
					{
						if (flag2 && File.Exists(empty1))
						{
							Global.Unzip(empty1);
						}
						if (!File.Exists(str))
						{
							flag = false;
							return flag;
						}
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				if (!File.Exists(str))
				{
					flag = false;
				}
				else
				{
					XmlNode xmlNodes = null;
					XmlDocument bookXmlDoc = this.GetBookXmlDoc(string.Concat(empty, "\\", this.frmParent.BookPublishingId, ".xml"), flag3, ref xmlNodes);
					string name = string.Empty;
					string name1 = string.Empty;
					try
					{
						foreach (XmlAttribute attribute in xmlNodes.Attributes)
						{
							if (attribute.Value.ToUpper() == "ID")
							{
								name1 = attribute.Name;
							}
							if (attribute.Value.ToUpper() != "PAGENAME")
							{
								continue;
							}
							name = attribute.Name;
						}
						this.statusText = this.GetResource("Searching..", "SEARCHING", ResourceType.STATUS_MESSAGE);
						this.UpdateStatus();
					}
					catch
					{
					}
					this.LoadSchemaValues(str);
					this.LoadSearchResultsInGridA(str, flag3, name1, name, bookXmlDoc);
					this.statusText = string.Concat(this.dgViewSearch.Rows.Count, " ", this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE));
					this.UpdateStatus();
					flag = true;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private void SearchPartsListXMLs()
		{
			try
			{
				string empty = string.Empty;
				empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				empty = string.Concat(empty, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
				empty = string.Concat(empty, "\\", this.frmParent.BookPublishingId);
				this.GridViewClearRows();
				if (File.Exists(string.Concat(empty, "\\", this.frmParent.BookPublishingId, ".xml")))
				{
					if (!this.frmParent.IsDisposed)
					{
						this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
						this.UpdateStatus();
						this.LoadSchemaValues(string.Concat(empty, "\\", this.frmParent.BookPublishingId, ".xml"));
						XmlNodeList xmlNodeLists = this.LoadPartsListNames(string.Concat(empty, "\\", this.frmParent.BookPublishingId, ".xml"));
						foreach (XmlNode xmlNodes in xmlNodeLists)
						{
							if (xmlNodes.Attributes[this.attPartsListItem] == null)
							{
								continue;
							}
							string str = string.Concat(empty, "\\", xmlNodes.Attributes[this.attPartsListItem].Value, ".xml");
							if (xmlNodes.ParentNode.Attributes[this.attPageNameItem] == null || xmlNodes.ParentNode.Attributes[this.attPageIdItem] == null)
							{
								continue;
							}
							this.LoadSearchResultsInGrid(str, xmlNodes.ParentNode.Attributes[this.attPageNameItem].Value, xmlNodes.ParentNode.Attributes[this.attPageIdItem].Value);
						}
						this.statusText = string.Concat(this.dgViewSearch.Rows.Count, " ", this.GetResource("result(s) found", "RESULTS_FOUND", ResourceType.STATUS_MESSAGE));
						this.UpdateStatus();
					}
				}
				else if (!this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("No result found", "NO_RESULT", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
				}
			}
			catch
			{
			}
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
					frmPartNameSearch.ShowLoadingDelegate showLoadingDelegate = new frmPartNameSearch.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void txtPartName_KeyDown(object sender, KeyEventArgs e)
		{
		}

		private void txtPartName_TextChanged(object sender, EventArgs e)
		{
			if (this.txtPartName.Text.Trim().Equals(string.Empty))
			{
				this.btnSearch.Enabled = false;
				return;
			}
			this.btnSearch.Enabled = true;
			this.searchString = this.txtPartName.Text;
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
					base.Invoke(new frmPartNameSearch.UpdadetControlsDelegate(this.UpdadetControls));
				}
			}
			catch
			{
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblPartName.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgViewSearch.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgViewSearch.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		private void UpdateStatus()
		{
			this.lblStatus.Text = this.statusText;
		}

		private bool ValidateAdvSearchKey(string strInputVal, XmlNode xndSchema)
		{
			bool flag;
			try
			{
				foreach (XmlAttribute attribute in xndSchema.Attributes)
				{
					if (attribute.Name.ToUpper().Trim() != strInputVal.ToUpper().Trim())
					{
						continue;
					}
					flag = true;
					return flag;
				}
				flag = false;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
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
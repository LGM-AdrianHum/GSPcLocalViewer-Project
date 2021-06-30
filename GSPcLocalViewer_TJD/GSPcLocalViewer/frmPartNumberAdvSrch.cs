using GSPcLocalViewer.Properties;
using GSPcLocalViewer.ServerSearch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmPartNumberAdvSrch : Form
	{
		private IContainer components;

		private SplitContainer pnlForm;

		private Panel pnltvSearch;

		private Label lblSearch;

		private Panel pnlDetails;

		private Panel pnlSearchResults;

		private Label lblDetails;

		private BackgroundWorker bgWorker;

		private Panel pnlSearchCriteria;

		private DataGridView dgvSearchResults;

		private Panel pnlControl;

		private Button btnSearch;

		private CheckBox chkExactMatch;

		private CheckBox chkMatchCase;

		private Panel pnlrtbNoDetails;

		private RichTextBox rtbNoDetails;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private BackgroundWorker bgLoader;

		private LabledTextBox ltbKeyword;

		private Panel pnlBooks;

		private PictureBox picLoading;

		private CustomComboBox cmbServers;

		private DataGridView dgvBooks;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

		private Panel pnlLoading;

		private frmOpenBook frmParent;

		private string statusText;

		private int p_ServerId;

		private Thread thGetResults;

		private bool bFormClosing;

		private Download objDownloader;

		private string sServerKey;

		public frmPartNumberAdvSrch(frmOpenBook frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.statusText = this.GetResource("Search parts on server", "SEARCH_ON_SERVER", ResourceType.STATUS_MESSAGE);
			string resource = this.GetResource("Select a server…", "SELECT_A_SERVER", ResourceType.LABEL);
			this.cmbServers.Items.Add(new Global.ComboBoxItem(resource, null));
			this.cmbServers.SelectedItem = this.cmbServers.Items[0];
			this.bFormClosing = false;
			this.p_ServerId = 99999;
			this.objDownloader = new Download(this.frmParent);
			this.UpdateFont();
			this.LoadResources();
			this.pnlLoading.Visible = false;
			this.thGetResults = new Thread(new ParameterizedThreadStart(this.GetResults));
			this.btnSearch.Tag = false;
		}

		private void AddRowToBooksGrid()
		{
			if (this.dgvBooks.InvokeRequired)
			{
				this.dgvBooks.Invoke(new frmPartNumberAdvSrch.AddRowToBooksGridDelegate(this.AddRowToBooksGrid));
				return;
			}
			this.dgvBooks.Rows.Add();
			if (this.dgvBooks.Rows.Count == 1)
			{
				this.dgvBooks.Rows[0].Selected = false;
			}
		}

		private void AddRowToResultsGrid()
		{
			if (this.dgvSearchResults.InvokeRequired)
			{
				this.dgvSearchResults.Invoke(new frmPartNumberAdvSrch.AddRowToResultsGridDelegate(this.AddRowToResultsGrid));
				return;
			}
			this.dgvSearchResults.Rows.Add();
			if (this.dgvSearchResults.Rows.Count == 1)
			{
				this.dgvSearchResults.Rows[0].Selected = false;
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			object[] argument = (object[])e.Argument;
			string str = string.Concat((string)argument[0], "Series.xml");
			string str1 = string.Concat((string)argument[1], "\\Series.xml");
			string str2 = string.Concat((string)argument[0], "DataUpdate.xml");
			string str3 = string.Concat((string)argument[1], "\\DataUpdate.xml");
			Global.ComboBoxItem comboBoxItem = (Global.ComboBoxItem)argument[2];
			bool flag = false;
			this.statusText = this.GetResource("Checking for data updates", "CHECKING_UPDATES", ResourceType.STATUS_MESSAGE);
			this.UpdateStatus();
			int num = 0;
			if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
			{
				num = 0;
			}
			this.objDownloader.DownloadFile(str2, str3);
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
				if (Global.IntervalElapsed(Global.GetLocalDateOfFile(str1, this.p_ServerId), dateTime, num))
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.statusText = this.GetResource("Downloading Series.xml", "DOWNLOADING_SERIES", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				if (!this.objDownloader.DownloadFile(str, str1) && !this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("Series.xml could not be downloaded", "NO_SERIES", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					e.Result = false;
				}
			}
			if (File.Exists(str1))
			{
				if (!this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("Loading Series.xml", "LOADING_SERIES", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					if (this.LoadBooksInGrid(str1))
					{
						this.statusText = this.GetResource("Series.xml loaded completely", "SERIES_LOADED", ResourceType.STATUS_MESSAGE);
						this.UpdateStatus();
						e.Result = true;
						return;
					}
					this.statusText = this.GetResource("Series.xml could not be loaded", "SERIES_NOT_LOADED", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					e.Result = false;
					return;
				}
			}
			else if (!this.frmParent.IsDisposed)
			{
				this.statusText = this.GetResource("Specified information does not exist", "NO_INFORMATION", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				e.Result = false;
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.HideLoading(this.pnlSearchCriteria);
			if (this.cmbServers.Items.Count != 2)
			{
				this.cmbServers.Enabled = true;
			}
			this.btnSearch.Enabled = true;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			bool flag = false;
			bool.TryParse(this.btnSearch.Tag.ToString(), out flag);
			if (!flag)
			{
				this.statusText = this.GetResource("Search parts on server", "SEARCH_ON_SERVER", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				if (this.ltbKeyword._Text.Trim() == string.Empty)
				{
					this.ShowResultMessage(this.GetResource("Enter part number to search", "ENTER_PARTNUMBER", ResourceType.LABEL));
					return;
				}
				this.EnableDisableSearchControls(false);
				this.ChangeSearchButtonText(this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON), true);
				this.dgvSearchResults.Rows.Clear();
				this.ShowLoading(this.pnlLoading);
				this.pnlrtbNoDetails.SendToBack();
				if (!this.thGetResults.IsAlive)
				{
					this.thGetResults = new Thread(new ParameterizedThreadStart(this.GetResults));
					this.thGetResults.Start();
					return;
				}
			}
			else if (this.thGetResults.IsAlive)
			{
				this.thGetResults.Interrupt();
			}
		}

		private void ChangeSearchButtonText(string caption, bool isSearching)
		{
			if (!this.btnSearch.InvokeRequired)
			{
				this.btnSearch.Text = caption;
				this.btnSearch.Tag = isSearching;
				return;
			}
			Button button = this.btnSearch;
			frmPartNumberAdvSrch.ChangeSearchButtonTextDelegate changeSearchButtonTextDelegate = new frmPartNumberAdvSrch.ChangeSearchButtonTextDelegate(this.ChangeSearchButtonText);
			object[] objArray = new object[] { caption, isSearching };
			button.Invoke(changeSearchButtonTextDelegate, objArray);
		}

		private void chkBooksHeader_OnCheckBoxClicked(bool state)
		{
			this.dgvBooks.CellValueChanged -= new DataGridViewCellEventHandler(this.dgvBooks_CellValueChanged);
			this.dgvBooks.BeginEdit(true);
			if (this.dgvBooks.Columns.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)this.dgvBooks.Rows)
				{
					if (!(row.Cells[0] is DataGridViewCheckBoxCell))
					{
						continue;
					}
					try
					{
						if (Convert.ToBoolean(row.Cells[0].Value) != state)
						{
							row.Cells["CHK"].Value = state;
						}
					}
					catch
					{
					}
				}
			}
			this.dgvBooks.EndEdit();
			this.dgvBooks.CellValueChanged += new DataGridViewCellEventHandler(this.dgvBooks_CellValueChanged);
		}

		private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.statusText = this.GetResource("Search parts on server", "SEARCH_ON_SERVER", ResourceType.STATUS_MESSAGE);
			this.UpdateStatus();
			Global.ComboBoxItem selectedItem = (Global.ComboBoxItem)this.cmbServers.SelectedItem;
			int num = 99999;
			if (selectedItem.Tag != null)
			{
				int.TryParse(selectedItem.Tag.ToString(), out num);
			}
			this.p_ServerId = num;
			this.HideLoading(this.pnlSearchResults);
			this.ShowResultMessage(this.GetResource("Search any books", "SEARCH_ANY_BOOKS", ResourceType.STATUS_MESSAGE));
			this.dgvBooks.Rows.Clear();
			this.dgvSearchResults.Rows.Clear();
			this.pnlBooks.SuspendLayout();
			if (((ComboBox)sender).SelectedIndex <= 0)
			{
				this.ShowHideSearchControls(false);
			}
			else if (this.p_ServerId == 99999 || Program.iniServers[this.p_ServerId].items["SETTINGS", "SERVICE_PATH"] == null)
			{
				this.ShowHideSearchControls(false);
				this.ShowResultMessage(this.GetResource("Web service path not found", "WEBSERVICE_NOT_FOUND", ResourceType.LABEL));
			}
			else
			{
				this.ShowHideSearchControls(true);
				this.LoadBooks();
			}
			this.pnlBooks.ResumeLayout();
		}

		private void dgvBooks_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1 && this.dgvBooks.Columns["CHK"] != null && e.ColumnIndex == this.dgvBooks.Columns["CHK"].Index)
			{
				try
				{
					if (this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
					{
						this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
					}
					else if (!bool.Parse(this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
					{
						this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
					}
					else if (bool.Parse(this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
					{
						this.dgvBooks.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
					}
				}
				catch
				{
				}
			}
		}

		private void dgvBooks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex != -1)
				{
					if (this.dgvBooks.CurrentRow.Cells["CHK"].Value == null || this.dgvBooks.CurrentRow.Cells["CHK"].Value.ToString().ToUpper() == "FALSE")
					{
						this.dgvBooks.CurrentRow.Cells["CHK"].Value = true;
					}
					else
					{
						this.dgvBooks.CurrentRow.Cells["CHK"].Value = false;
					}
				}
			}
			catch
			{
			}
		}

		private void dgvBooks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				string empty = string.Empty;
				if (this.dgvBooks.Columns.Count > 0 && e.RowIndex != -1 && e.ColumnIndex == this.dgvBooks.Columns["CHK"].Index)
				{
					int num = 0;
					num = 0;
					while (num < this.dgvBooks.Rows.Count && (!(this.dgvBooks.Rows[num].Cells[0] is DataGridViewCheckBoxCell) || (bool)this.dgvBooks.Rows[num].Cells["CHK"].Value))
					{
						num++;
					}
					DatagridViewCheckBoxHeaderCell headerCell = (DatagridViewCheckBoxHeaderCell)this.dgvBooks.Columns[0].HeaderCell;
					if (num >= this.dgvBooks.Rows.Count)
					{
						headerCell.Checked = true;
					}
					else
					{
						headerCell.Checked = false;
					}
				}
			}
			catch
			{
			}
		}

		private void dgvSearchResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dgvSearchResults_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (this.dgvSearchResults.Rows.Count != 0 && e.RowIndex >= 0)
			{
				this.bFormClosing = true;
				if (this.thGetResults.IsAlive)
				{
					this.thGetResults.Interrupt();
				}
				try
				{
					string empty = string.Empty;
					string str = string.Empty;
					string empty1 = string.Empty;
					string str1 = string.Empty;
					string empty2 = string.Empty;
					string str2 = string.Empty;
					string empty3 = string.Empty;
					Hashtable tag = (Hashtable)this.dgvSearchResults.Tag;
					empty = Program.iniServers[this.p_ServerId].sIniKey;
					if (tag.Contains("BookCode"))
					{
						empty3 = tag["BookCode"].ToString();
						if (this.dgvSearchResults.Columns.Contains(empty3))
						{
							str = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty3].Value.ToString();
						}
					}
					if (tag.Contains("PageId"))
					{
						empty3 = tag["PageId"].ToString();
						if (this.dgvSearchResults.Columns.Contains(empty3))
						{
							empty1 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty3].Value.ToString();
						}
					}
					if (tag.Contains("PicIndex"))
					{
						empty3 = tag["PicIndex"].ToString();
						if (this.dgvSearchResults.Columns.Contains(empty3))
						{
							str1 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty3].Value.ToString();
						}
					}
					if (tag.Contains("ListIndex"))
					{
						empty3 = tag["ListIndex"].ToString();
						if (this.dgvSearchResults.Columns.Contains(empty3))
						{
							empty2 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty3].Value.ToString();
						}
					}
					if (tag.Contains("PartNumber"))
					{
						empty3 = tag["PartNumber"].ToString();
						if (this.dgvSearchResults.Columns.Contains(empty3))
						{
							str2 = this.dgvSearchResults.Rows[e.RowIndex].Cells[empty3].Value.ToString();
						}
					}
					if (!(empty != string.Empty) || !(str != string.Empty) || !(empty1 != string.Empty) || !(str1 != string.Empty) || !(empty2 != string.Empty) || !(str2 != string.Empty) || !(empty3 != string.Empty))
					{
						MessageHandler.ShowMessage(this.frmParent, this.GetResource("(E-RG-OP001) Cannot open book.", "(E-RG-OP001)_NO_BOOK", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						this.frmParent.CloseAndLoadSearch(empty, str, empty1, str1, empty2, str2);
					}
				}
				catch
				{
					MessageHandler.ShowMessage(this.frmParent, this.GetResource("(E-RG-OP002) Cannot open book.", "(E-RG-OP002)_BOOK", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
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

		private void EnableDisableSearchControls(bool value)
		{
			if (this.pnlSearchCriteria.InvokeRequired)
			{
				Panel panel = this.pnlSearchCriteria;
				frmPartNumberAdvSrch.EnableDisableSearchControlsDelegate enableDisableSearchControlsDelegate = new frmPartNumberAdvSrch.EnableDisableSearchControlsDelegate(this.EnableDisableSearchControls);
				object[] objArray = new object[] { value };
				panel.Invoke(enableDisableSearchControlsDelegate, objArray);
				return;
			}
			if (this.cmbServers.Items.Count != 2)
			{
				this.cmbServers.Enabled = value;
			}
			this.chkExactMatch.Enabled = value;
			this.chkMatchCase.Enabled = value;
			this.dgvBooks.Enabled = value;
			this.ltbKeyword.Enabled = value;
		}

		private void frmPageNameAdvSrch_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.bFormClosing = true;
			if (this.thGetResults.IsAlive)
			{
				this.thGetResults.Interrupt();
			}
		}

		private void frmPageNameAdvSrch_Load(object sender, EventArgs e)
		{
			try
			{
				this.cmbServers.BeginUpdate();
				for (int i = 0; i < (int)Program.iniServers.Length; i++)
				{
					string item = Program.iniServers[i].items["SETTINGS", "DISPLAY_NAME"];
					object obj = i;
					if (item != string.Empty)
					{
						this.cmbServers.Items.Add(new Global.ComboBoxItem(item, obj));
					}
				}
				this.cmbServers.EndUpdate();
			}
			catch
			{
				base.Hide();
				MessageHandler.ShowMessage(this.frmParent, this.GetResource("(E-TV-LD001) Failed to load specified object.", "(E-TV-LD001)_FAILED_LOAD", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				base.Show();
			}
			this.ShowResultMessage(this.GetResource("Search any books", "SEARCH_ANY_BOOKS", ResourceType.LABEL));
			try
			{
				if (this.cmbServers.Items.Count == 2)
				{
					this.cmbServers.SelectedIndex = 1;
					this.cmbServers.Enabled = false;
				}
			}
			catch
			{
			}
		}

		private void frmPageNameAdvSrch_VisibleChanged(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		private DataGridViewColumnCollection GetBooksColumns()
		{
			if (!this.dgvBooks.InvokeRequired)
			{
				return this.dgvBooks.Columns;
			}
			return (DataGridViewColumnCollection)this.dgvBooks.Invoke(new frmPartNumberAdvSrch.GetBooksColumnsDelegate(this.GetBooksColumns));
		}

		private bool GetExactMatch()
		{
			if (!this.chkExactMatch.InvokeRequired)
			{
				return this.chkExactMatch.Checked;
			}
			return (bool)this.chkExactMatch.Invoke(new frmPartNumberAdvSrch.GetExaceMatchDelegate(this.GetExactMatch));
		}

		private string GetGridLanguage(string sKey, string sServerKey)
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

		private string GetKeyword()
		{
			if (!this.ltbKeyword.InvokeRequired)
			{
				return this.ltbKeyword._Text;
			}
			return (string)this.ltbKeyword.Invoke(new frmPartNumberAdvSrch.GetKeywordDelegate(this.GetKeyword));
		}

		private bool GetMatchCase()
		{
			if (!this.chkMatchCase.InvokeRequired)
			{
				return this.chkMatchCase.Checked;
			}
			return (bool)this.chkMatchCase.Invoke(new frmPartNumberAdvSrch.GetMatchCaseDelegate(this.GetMatchCase));
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='OPEN_BOOKS']");
				str = string.Concat(str, "/Screen[@Name='PART_NUMBER_ADVSEARCH']");
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
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
					}
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
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

		private void GetResults(object arguments)
		{
			string empty = string.Empty;
			string str = string.Empty;
			bool matchCase = false;
			bool exactMatch = false;
			this.statusText = this.GetResource("Searching parts", "SEARCHING_PARTS", ResourceType.STATUS_MESSAGE);
			this.UpdateStatus();
			try
			{
				try
				{
					Exception exception = new Exception()
					{
						Source = "custom"
					};
					empty = this.GetKeyword();
					str = "PartNumber";
					matchCase = this.GetMatchCase();
					exactMatch = this.GetExactMatch();
					ArrayList selectedBookRows = this.GetSelectedBookRows();
					if (selectedBookRows == null || selectedBookRows.Count == 0)
					{
						exception.HelpLink = this.GetResource("Select books to search from", "SELECT_BOOKS", ResourceType.LABEL);
						throw exception;
					}
					Search search = new Search();
					if (Program.iniServers[this.p_ServerId].items["SETTINGS", "SERVICE_PATH"] == null)
					{
						search.Url = string.Empty;
					}
					else
					{
						search.Url = Program.iniServers[this.p_ServerId].items["SETTINGS", "SERVICE_PATH"].ToLower();
					}
					DataGridViewRow item = (DataGridViewRow)selectedBookRows[0];
					XmlNode xmlNodes = search.SearchPageSchema(item.Tag.ToString(), Program.iniServers[this.p_ServerId].sIniKey);
					XmlNode xmlNodes1 = search.SearchPartSchema(item.Tag.ToString(), Program.iniServers[this.p_ServerId].sIniKey);
					if (xmlNodes == null && selectedBookRows.Count > 1)
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)selectedBookRows[1];
						xmlNodes = search.SearchPageSchema(dataGridViewRow.Tag.ToString(), Program.iniServers[this.p_ServerId].sIniKey);
					}
					if (xmlNodes1 == null && selectedBookRows.Count > 1)
					{
						DataGridViewRow item1 = (DataGridViewRow)selectedBookRows[1];
						xmlNodes1 = search.SearchPartSchema(item1.Tag.ToString(), Program.iniServers[this.p_ServerId].sIniKey);
					}
					if (xmlNodes == null || xmlNodes.Attributes.Count == 0 || xmlNodes1 == null || xmlNodes1.Attributes.Count == 0)
					{
						exception.HelpLink = this.GetResource("File schema not found on server", "NO_SCHEMA", ResourceType.STATUS_MESSAGE);
						throw exception;
					}
					this.InitializeResultsGrid(xmlNodes, xmlNodes1);
					this.ShowResultGrid();
					foreach (DataGridViewRow selectedBookRow in selectedBookRows)
					{
						XmlNode xmlNodes2 = null;
						string str1 = Program.iniServers[this.p_ServerId].sIniKey;
						string[] strArrays = new string[] { selectedBookRow.Tag.ToString() };
						xmlNodes2 = search.SearchParts(empty, str, matchCase, exactMatch, str1, strArrays);
						if (xmlNodes2 == null || xmlNodes2.ChildNodes.Count <= 0)
						{
							continue;
						}
						foreach (XmlNode childNode in xmlNodes2.ChildNodes)
						{
							this.AddRowToResultsGrid();
							foreach (XmlAttribute attribute in childNode.Attributes)
							{
								try
								{
									this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[string.Concat("PAGE::", attribute.Name)].Value = attribute.Value;
								}
								catch
								{
								}
							}
							if (childNode.ChildNodes.Count > 0)
							{
								XmlNode itemOf = childNode.ChildNodes[0];
								foreach (XmlAttribute value in itemOf.Attributes)
								{
									try
									{
										this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[string.Concat("PAGE::", value.Name)].Value = value.Value;
									}
									catch
									{
									}
								}
								if (itemOf.ChildNodes.Count > 0)
								{
									foreach (XmlAttribute xmlAttribute in itemOf.ChildNodes[0].Attributes)
									{
										try
										{
											this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[string.Concat("PART::", xmlAttribute.Name)].Value = xmlAttribute.Value;
										}
										catch
										{
										}
									}
								}
							}
							for (int i = 0; i < this.dgvSearchResults.Columns.Count; i++)
							{
								if (this.dgvSearchResults.Columns[i].Name.Contains("BOOK::"))
								{
									try
									{
										string str2 = this.dgvSearchResults.Columns[i].Name.Replace("BOOK::", string.Empty);
										this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[i].Value = selectedBookRow.Cells[str2].Value;
									}
									catch
									{
									}
								}
							}
						}
					}
					if (this.dgvSearchResults.Rows.Count == 0)
					{
						this.ShowResultMessage(this.GetResource("No result found", "NO_RESULT_FOUND", ResourceType.LABEL));
					}
					this.statusText = this.GetResource("Search complete", "SEARCH_COMPLETE", ResourceType.STATUS_MESSAGE);
				}
				catch (ThreadInterruptedException threadInterruptedException)
				{
					if (!this.bFormClosing)
					{
						this.statusText = this.GetResource("Search Canceled", "SEARCH_CANCELED", ResourceType.STATUS_MESSAGE);
					}
				}
				catch (WebException webException)
				{
					if (!this.bFormClosing)
					{
						this.statusText = this.GetResource("Search failed", "SEARCH_FAILED", ResourceType.STATUS_MESSAGE);
						this.ShowResultMessage(this.GetResource("Web service not found at specified path", "NO_WEBSERVICE", ResourceType.LABEL));
					}
				}
				catch (UriFormatException uriFormatException)
				{
					if (!this.bFormClosing)
					{
						this.statusText = this.GetResource("Search failed", "SEARCH_FAILED", ResourceType.STATUS_MESSAGE);
						this.ShowResultMessage(this.GetResource("Web service path specified is not valid", "PATH_INCORRECT", ResourceType.LABEL));
					}
				}
				catch (Exception exception2)
				{
					Exception exception1 = exception2;
					if (!this.bFormClosing)
					{
						if (exception1.Source != "custom")
						{
							this.ShowResultMessage(this.GetResource("Can not connect to specified address.", "CANNOT_CONNECT", ResourceType.LABEL));
						}
						else
						{
							this.ShowResultMessage(exception1.HelpLink);
						}
						this.statusText = this.GetResource("Search failed", "SEARCH_FAILED", ResourceType.STATUS_MESSAGE);
					}
				}
			}
			finally
			{
				if (!this.bFormClosing)
				{
					this.ChangeSearchButtonText(this.GetResource("Search", "SEARCH", ResourceType.BUTTON), false);
					this.UpdateStatus();
					this.EnableDisableSearchControls(true);
					this.HideLoading(this.pnlLoading);
				}
			}
		}

		private ArrayList GetSelectedBookRows()
		{
			if (this.dgvBooks.InvokeRequired)
			{
				return (ArrayList)this.dgvBooks.Invoke(new frmPartNumberAdvSrch.GetSelectedBookRowsDelegate(this.GetSelectedBookRows));
			}
			ArrayList arrayLists = new ArrayList();
			foreach (DataGridViewRow row in (IEnumerable)this.dgvBooks.Rows)
			{
				bool flag = false;
				if (this.dgvBooks.Columns.Contains("CHK") && row.Cells["CHK"].Value != null)
				{
					bool.TryParse(row.Cells["CHK"].Value.ToString(), out flag);
				}
				if (!flag)
				{
					continue;
				}
				arrayLists.Add(row);
			}
			return arrayLists;
		}

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				if (!parentPanel.InvokeRequired)
				{
					foreach (Control control in parentPanel.Controls)
					{
						if (control == this.picLoading)
						{
							continue;
						}
						control.Visible = true;
					}
					this.picLoading.Hide();
					this.picLoading.Parent = this;
					if (parentPanel.Tag != null && parentPanel.Tag.ToString() == "loading")
					{
						parentPanel.Hide();
					}
				}
				else
				{
					frmPartNumberAdvSrch.HideLoadingDelegate hideLoadingDelegate = new frmPartNumberAdvSrch.HideLoadingDelegate(this.HideLoading);
					object[] objArray = new object[] { parentPanel };
					parentPanel.Invoke(hideLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void InitializeBooksGrid(XmlNode xSchemaNode)
		{
			if (this.dgvBooks.InvokeRequired)
			{
				DataGridView dataGridView = this.dgvBooks;
				frmPartNumberAdvSrch.InitializeBooksGridDelegate initializeBooksGridDelegate = new frmPartNumberAdvSrch.InitializeBooksGridDelegate(this.InitializeBooksGrid);
				object[] objArray = new object[] { xSchemaNode };
				dataGridView.Invoke(initializeBooksGridDelegate, objArray);
				return;
			}
			try
			{
				this.dgvBooks.Rows.Clear();
				this.dgvBooks.Columns.Clear();
				DatagridViewCheckBoxHeaderCell datagridViewCheckBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
				datagridViewCheckBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(this.chkBooksHeader_OnCheckBoxClicked);
				DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn()
				{
					Name = "CHK",
					Tag = "CHK",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Frozen = true,
					AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
				};
				datagridViewCheckBoxHeaderCell.Value = "";
				dataGridViewCheckBoxColumn.HeaderCell = datagridViewCheckBoxHeaderCell;
				this.dgvBooks.Columns.Add(dataGridViewCheckBoxColumn);
				List<DataGridViewTextBoxColumn> dataGridViewTextBoxColumns = new List<DataGridViewTextBoxColumn>();
				ArrayList arrayLists = new ArrayList();
				IniFileIO iniFileIO = new IniFileIO();
				arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini"), "BOOKS_ADVSEARCH");
				foreach (XmlAttribute attribute in xSchemaNode.Attributes)
				{
					if (attribute.Name == null || attribute.Value == null || !(attribute.Name.Trim() != string.Empty) || !(attribute.Value.Trim() != string.Empty))
					{
						continue;
					}
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
					{
						Name = attribute.Name.ToUpper(),
						Tag = attribute.Value.ToUpper(),
						HeaderText = this.GetGridLanguage(attribute.Value, this.sServerKey),
						AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
						Width = 80
					};
					string upper = attribute.Value.ToUpper();
					if (arrayLists.Count > 0)
					{
						dataGridViewTextBoxColumn.Visible = false;
						int num = 0;
						while (num < arrayLists.Count)
						{
							if (upper != arrayLists[num].ToString().Trim())
							{
								num++;
							}
							else
							{
								string keyValue = iniFileIO.GetKeyValue("BOOKS_ADVSEARCH", arrayLists[num].ToString(), string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini"));
								string[] strArrays = keyValue.Split(new char[] { '|' });
								try
								{
									if (strArrays[0].ToString().ToUpper() == "TRUE")
									{
										dataGridViewTextBoxColumn.Visible = true;
									}
								}
								catch (Exception exception)
								{
									if (upper == "BOOKCODE" || upper == "BOOKTYPE" || upper == "PUBLISHINGID")
									{
										dataGridViewTextBoxColumn.Visible = true;
									}
								}
								try
								{
									if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
									{
										dataGridViewTextBoxColumn.HeaderText = strArrays[1];
									}
									else
									{
										dataGridViewTextBoxColumn.HeaderText = Global.GetDGHeaderCellValue("BOOKS_ADVSEARCH", upper, strArrays[1], this.p_ServerId);
									}
								}
								catch (Exception exception1)
								{
								}
								try
								{
									string str = strArrays[2];
									string str1 = str;
									if (str != null)
									{
										if (str1 == "L")
										{
											dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
											goto yoyo1;
										}
										else if (str1 == "R")
										{
											dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
											goto yoyo1;
										}
										else
										{
											if (str1 != "C")
											{
												goto yoyo3;
											}
											dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
											goto yoyo1;
										}
									}
								yoyo3:
									dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								Label1:
								}
								catch (Exception exception2)
								{
									dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								}
								try
								{
									dataGridViewTextBoxColumn.Width = Convert.ToInt32(strArrays[3]);
									goto yoyo0;
								}
								catch (Exception exception3)
								{
									goto yoyo0;
								}
							}
						}
					}
					else if (upper == "BOOKCODE" || upper == "BOOKTYPE" || upper == "PUBLISHINGID")
					{
						dataGridViewTextBoxColumn.Visible = true;
					}
					else
					{
						dataGridViewTextBoxColumn.Visible = false;
					}
				yoyo0:
					if (!dataGridViewTextBoxColumn.Visible && !(dataGridViewTextBoxColumn.Tag.ToString() == "BOOKCODE") && !(dataGridViewTextBoxColumn.Tag.ToString() == "BOOKTYPE") && !(dataGridViewTextBoxColumn.Tag.ToString() == "PUBLISHINGID"))
					{
						continue;
					}
					if (arrayLists.Count <= 0)
					{
						this.dgvBooks.Columns.Add(dataGridViewTextBoxColumn);
					}
					else
					{
						dataGridViewTextBoxColumns.Add(dataGridViewTextBoxColumn);
					}
				}
				if (arrayLists.Count > 0)
				{
					try
					{
						for (int i = 0; i < arrayLists.Count; i++)
						{
							foreach (DataGridViewColumn dataGridViewColumn in dataGridViewTextBoxColumns)
							{
								if (arrayLists[i].ToString().ToUpper() != dataGridViewColumn.Tag.ToString().ToUpper())
								{
									continue;
								}
								this.dgvBooks.Columns.Add(dataGridViewColumn);
							}
						}
						foreach (DataGridViewColumn dataGridViewTextBoxColumn1 in dataGridViewTextBoxColumns)
						{
							bool flag = false;
							foreach (DataGridViewColumn column in this.dgvBooks.Columns)
							{
								if (!(column.HeaderText != string.Empty) || !column.Visible || !(column.HeaderText.ToUpper() == dataGridViewTextBoxColumn1.HeaderText.ToUpper()))
								{
									continue;
								}
								flag = true;
								break;
							}
							if (flag)
							{
								continue;
							}
							this.dgvBooks.Columns.Add(dataGridViewTextBoxColumn1);
						}
					}
					catch (Exception exception4)
					{
					}
				}
			}
			catch (Exception exception5)
			{
			}
		}

		private void InitializeComponent()
		{
			this.pnlForm = new SplitContainer();
			this.pnlSearchCriteria = new Panel();
			this.pnlBooks = new Panel();
			this.dgvBooks = new DataGridView();
			this.pnlControl = new Panel();
			this.ltbKeyword = new LabledTextBox();
			this.btnSearch = new Button();
			this.chkExactMatch = new CheckBox();
			this.chkMatchCase = new CheckBox();
			this.pnltvSearch = new Panel();
			this.cmbServers = new CustomComboBox();
			this.lblSearch = new Label();
			this.pnlDetails = new Panel();
			this.pnlSearchResults = new Panel();
			this.dgvSearchResults = new DataGridView();
			this.pnlrtbNoDetails = new Panel();
			this.rtbNoDetails = new RichTextBox();
			this.pnlLoading = new Panel();
			this.lblDetails = new Label();
			this.bgWorker = new BackgroundWorker();
			this.bgLoader = new BackgroundWorker();
			this.picLoading = new PictureBox();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
			this.pnlForm.Panel1.SuspendLayout();
			this.pnlForm.Panel2.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.pnlSearchCriteria.SuspendLayout();
			this.pnlBooks.SuspendLayout();
			((ISupportInitialize)this.dgvBooks).BeginInit();
			this.pnlControl.SuspendLayout();
			this.pnltvSearch.SuspendLayout();
			this.pnlDetails.SuspendLayout();
			this.pnlSearchResults.SuspendLayout();
			((ISupportInitialize)this.dgvSearchResults).BeginInit();
			this.pnlrtbNoDetails.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Panel1.BackColor = SystemColors.Control;
			this.pnlForm.Panel1.Controls.Add(this.pnlSearchCriteria);
			this.pnlForm.Panel1.Controls.Add(this.pnltvSearch);
			this.pnlForm.Panel1.Controls.Add(this.lblSearch);
			this.pnlForm.Panel1MinSize = 270;
			this.pnlForm.Panel2.Controls.Add(this.pnlDetails);
			this.pnlForm.Panel2.Controls.Add(this.lblDetails);
			this.pnlForm.Panel2MinSize = 75;
			this.pnlForm.Size = new System.Drawing.Size(584, 409);
			this.pnlForm.SplitterDistance = 270;
			this.pnlForm.TabIndex = 18;
			this.pnlSearchCriteria.BackColor = Color.White;
			this.pnlSearchCriteria.Controls.Add(this.pnlBooks);
			this.pnlSearchCriteria.Controls.Add(this.pnlControl);
			this.pnlSearchCriteria.Dock = DockStyle.Fill;
			this.pnlSearchCriteria.Location = new Point(0, 70);
			this.pnlSearchCriteria.Name = "pnlSearchCriteria";
			this.pnlSearchCriteria.Size = new System.Drawing.Size(268, 337);
			this.pnlSearchCriteria.TabIndex = 16;
			this.pnlBooks.Controls.Add(this.dgvBooks);
			this.pnlBooks.Dock = DockStyle.Fill;
			this.pnlBooks.Location = new Point(0, 100);
			this.pnlBooks.Name = "pnlBooks";
			this.pnlBooks.Size = new System.Drawing.Size(268, 237);
			this.pnlBooks.TabIndex = 23;
			this.dgvBooks.AllowUserToAddRows = false;
			this.dgvBooks.AllowUserToDeleteRows = false;
			this.dgvBooks.AllowUserToResizeRows = false;
			this.dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvBooks.BackgroundColor = Color.White;
			this.dgvBooks.BorderStyle = BorderStyle.None;
			this.dgvBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvBooks.Dock = DockStyle.Fill;
			this.dgvBooks.Location = new Point(0, 0);
			this.dgvBooks.MultiSelect = false;
			this.dgvBooks.Name = "dgvBooks";
			this.dgvBooks.ReadOnly = true;
			this.dgvBooks.RowHeadersVisible = false;
			this.dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvBooks.Size = new System.Drawing.Size(268, 237);
			this.dgvBooks.TabIndex = 1;
			this.dgvBooks.CellValueChanged += new DataGridViewCellEventHandler(this.dgvBooks_CellValueChanged);
			this.dgvBooks.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvBooks_CellDoubleClick);
			this.dgvBooks.CellClick += new DataGridViewCellEventHandler(this.dgvBooks_CellClick);
			this.pnlControl.Controls.Add(this.ltbKeyword);
			this.pnlControl.Controls.Add(this.btnSearch);
			this.pnlControl.Controls.Add(this.chkExactMatch);
			this.pnlControl.Controls.Add(this.chkMatchCase);
			this.pnlControl.Dock = DockStyle.Top;
			this.pnlControl.Location = new Point(0, 0);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(268, 100);
			this.pnlControl.TabIndex = 22;
			this.pnlControl.Visible = false;
			this.ltbKeyword._Caption = "Keyword";
			this.ltbKeyword._Name = "Keyword";
			this.ltbKeyword._Text = "";
			this.ltbKeyword.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ltbKeyword.Location = new Point(15, 59);
			this.ltbKeyword.Name = "ltbKeyword";
			this.ltbKeyword.Size = new System.Drawing.Size(215, 26);
			this.ltbKeyword.TabIndex = 5;
			this.btnSearch.Image = Resources.Search2;
			this.btnSearch.ImageAlign = ContentAlignment.MiddleRight;
			this.btnSearch.Location = new Point(145, 7);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(80, 23);
			this.btnSearch.TabIndex = 1;
			this.btnSearch.Text = "Search";
			this.btnSearch.TextAlign = ContentAlignment.MiddleLeft;
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
			this.chkExactMatch.AutoSize = true;
			this.chkExactMatch.Location = new Point(15, 11);
			this.chkExactMatch.Name = "chkExactMatch";
			this.chkExactMatch.Size = new System.Drawing.Size(85, 17);
			this.chkExactMatch.TabIndex = 4;
			this.chkExactMatch.Text = "Exact Match";
			this.chkExactMatch.UseVisualStyleBackColor = true;
			this.chkMatchCase.AutoSize = true;
			this.chkMatchCase.Location = new Point(15, 34);
			this.chkMatchCase.Name = "chkMatchCase";
			this.chkMatchCase.Size = new System.Drawing.Size(82, 17);
			this.chkMatchCase.TabIndex = 3;
			this.chkMatchCase.Text = "Match Case";
			this.chkMatchCase.UseVisualStyleBackColor = true;
			this.pnltvSearch.BackColor = Color.White;
			this.pnltvSearch.Controls.Add(this.cmbServers);
			this.pnltvSearch.Dock = DockStyle.Top;
			this.pnltvSearch.Location = new Point(0, 27);
			this.pnltvSearch.Name = "pnltvSearch";
			this.pnltvSearch.Padding = new System.Windows.Forms.Padding(15, 15, 15, 0);
			this.pnltvSearch.Size = new System.Drawing.Size(268, 43);
			this.pnltvSearch.TabIndex = 15;
			this.cmbServers.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmbServers.DropDownHeight = 150;
			this.cmbServers.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbServers.FormattingEnabled = true;
			this.cmbServers.IntegralHeight = false;
			this.cmbServers.Location = new Point(15, 15);
			this.cmbServers.Name = "cmbServers";
			this.cmbServers.Size = new System.Drawing.Size(210, 22);
			this.cmbServers.TabIndex = 12;
			this.cmbServers.SelectedIndexChanged += new EventHandler(this.cmbServers_SelectedIndexChanged);
			this.lblSearch.BackColor = Color.White;
			this.lblSearch.Dock = DockStyle.Top;
			this.lblSearch.ForeColor = Color.Black;
			this.lblSearch.Location = new Point(0, 0);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblSearch.Size = new System.Drawing.Size(268, 27);
			this.lblSearch.TabIndex = 14;
			this.lblSearch.Text = "Search";
			this.pnlDetails.BackColor = Color.White;
			this.pnlDetails.Controls.Add(this.pnlSearchResults);
			this.pnlDetails.Controls.Add(this.pnlLoading);
			this.pnlDetails.Dock = DockStyle.Fill;
			this.pnlDetails.Location = new Point(0, 27);
			this.pnlDetails.Name = "pnlDetails";
			this.pnlDetails.Size = new System.Drawing.Size(308, 380);
			this.pnlDetails.TabIndex = 15;
			this.pnlSearchResults.BackColor = Color.White;
			this.pnlSearchResults.Controls.Add(this.dgvSearchResults);
			this.pnlSearchResults.Controls.Add(this.pnlrtbNoDetails);
			this.pnlSearchResults.Dock = DockStyle.Fill;
			this.pnlSearchResults.Location = new Point(0, 0);
			this.pnlSearchResults.Name = "pnlSearchResults";
			this.pnlSearchResults.Size = new System.Drawing.Size(308, 346);
			this.pnlSearchResults.TabIndex = 13;
			this.pnlSearchResults.Tag = "";
			this.dgvSearchResults.AllowUserToAddRows = false;
			this.dgvSearchResults.AllowUserToDeleteRows = false;
			this.dgvSearchResults.AllowUserToResizeRows = false;
			this.dgvSearchResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvSearchResults.BackgroundColor = Color.White;
			this.dgvSearchResults.BorderStyle = BorderStyle.None;
			this.dgvSearchResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvSearchResults.Dock = DockStyle.Fill;
			this.dgvSearchResults.Location = new Point(0, 0);
			this.dgvSearchResults.MultiSelect = false;
			this.dgvSearchResults.Name = "dgvSearchResults";
			this.dgvSearchResults.ReadOnly = true;
			this.dgvSearchResults.RowHeadersVisible = false;
			this.dgvSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSearchResults.Size = new System.Drawing.Size(308, 346);
			this.dgvSearchResults.TabIndex = 0;
			this.dgvSearchResults.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.dgvSearchResults_CellMouseDoubleClick);
			this.pnlrtbNoDetails.BackColor = Color.White;
			this.pnlrtbNoDetails.Controls.Add(this.rtbNoDetails);
			this.pnlrtbNoDetails.Dock = DockStyle.Fill;
			this.pnlrtbNoDetails.Location = new Point(0, 0);
			this.pnlrtbNoDetails.Name = "pnlrtbNoDetails";
			this.pnlrtbNoDetails.Padding = new System.Windows.Forms.Padding(25, 10, 0, 0);
			this.pnlrtbNoDetails.Size = new System.Drawing.Size(308, 346);
			this.pnlrtbNoDetails.TabIndex = 16;
			this.pnlrtbNoDetails.Tag = "";
			this.rtbNoDetails.BackColor = Color.White;
			this.rtbNoDetails.BorderStyle = BorderStyle.None;
			this.rtbNoDetails.Dock = DockStyle.Fill;
			this.rtbNoDetails.Location = new Point(25, 10);
			this.rtbNoDetails.Name = "rtbNoDetails";
			this.rtbNoDetails.ReadOnly = true;
			this.rtbNoDetails.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtbNoDetails.Size = new System.Drawing.Size(283, 336);
			this.rtbNoDetails.TabIndex = 13;
			this.rtbNoDetails.TabStop = false;
			this.rtbNoDetails.Text = "";
			this.pnlLoading.Dock = DockStyle.Bottom;
			this.pnlLoading.Location = new Point(0, 346);
			this.pnlLoading.Name = "pnlLoading";
			this.pnlLoading.Size = new System.Drawing.Size(308, 34);
			this.pnlLoading.TabIndex = 18;
			this.pnlLoading.Tag = "loading";
			this.pnlLoading.Visible = false;
			this.lblDetails.BackColor = Color.White;
			this.lblDetails.Dock = DockStyle.Top;
			this.lblDetails.ForeColor = Color.Black;
			this.lblDetails.Location = new Point(0, 0);
			this.lblDetails.Name = "lblDetails";
			this.lblDetails.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblDetails.Size = new System.Drawing.Size(308, 27);
			this.lblDetails.TabIndex = 14;
			this.lblDetails.Text = "Details";
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			this.bgLoader.WorkerSupportsCancellation = true;
			this.picLoading.BackColor = Color.Transparent;
			this.picLoading.Dock = DockStyle.Fill;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(0, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(584, 409);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 20;
			this.picLoading.TabStop = false;
			this.dataGridViewCheckBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewCheckBoxColumn1.FalseValue = "0";
			this.dataGridViewCheckBoxColumn1.FillWeight = 15.22843f;
			this.dataGridViewCheckBoxColumn1.Frozen = true;
			this.dataGridViewCheckBoxColumn1.HeaderText = "";
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			this.dataGridViewCheckBoxColumn1.Resizable = DataGridViewTriState.False;
			this.dataGridViewCheckBoxColumn1.TrueValue = "1";
			this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.FillWeight = 142.3858f;
			this.dataGridViewTextBoxColumn1.HeaderText = "Book Publishing Id";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewTextBoxColumn2.FillWeight = 142.3858f;
			this.dataGridViewTextBoxColumn2.HeaderText = "UpdateDate";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.HeaderText = "UpdateDate";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 107;
			this.dataGridViewTextBoxColumn4.HeaderText = "Publishing Id";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			this.dataGridViewTextBoxColumn4.Width = 107;
			this.dataGridViewTextBoxColumn5.HeaderText = "UpdateDate";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			this.dataGridViewTextBoxColumn5.ReadOnly = true;
			this.dataGridViewTextBoxColumn5.Width = 107;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(584, 409);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.picLoading);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmPartNumberAdvSrch";
			this.Text = "frmOpenBookSearch";
			base.Load += new EventHandler(this.frmPageNameAdvSrch_Load);
			base.VisibleChanged += new EventHandler(this.frmPageNameAdvSrch_VisibleChanged);
			base.FormClosing += new FormClosingEventHandler(this.frmPageNameAdvSrch_FormClosing);
			this.pnlForm.Panel1.ResumeLayout(false);
			this.pnlForm.Panel2.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnlSearchCriteria.ResumeLayout(false);
			this.pnlBooks.ResumeLayout(false);
			((ISupportInitialize)this.dgvBooks).EndInit();
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			this.pnltvSearch.ResumeLayout(false);
			this.pnlDetails.ResumeLayout(false);
			this.pnlSearchResults.ResumeLayout(false);
			((ISupportInitialize)this.dgvSearchResults).EndInit();
			this.pnlrtbNoDetails.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
		}

		private void InitializeResultsGrid(XmlNode xPageSchema, XmlNode xPartSchema)
		{
			if (this.dgvSearchResults.InvokeRequired)
			{
				DataGridView dataGridView = this.dgvSearchResults;
				frmPartNumberAdvSrch.InitializeResultsGridDelegate initializeResultsGridDelegate = new frmPartNumberAdvSrch.InitializeResultsGridDelegate(this.InitializeResultsGrid);
				object[] objArray = new object[] { xPageSchema, xPartSchema };
				dataGridView.Invoke(initializeResultsGridDelegate, objArray);
				return;
			}
			this.dgvSearchResults.Rows.Clear();
			this.dgvSearchResults.Columns.Clear();
			Hashtable hashtables = new Hashtable();
			ArrayList arrayLists = new ArrayList();
			IniFileIO iniFileIO = new IniFileIO();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini"), "BOOKS_PARTNUMBER_ADVSEARCH");
			List<DataGridViewTextBoxColumn> dataGridViewTextBoxColumns = new List<DataGridViewTextBoxColumn>();
			if (arrayLists.Count <= 0)
			{
				foreach (XmlAttribute attribute in xPartSchema.Attributes)
				{
					if (attribute.Name == null || attribute.Value == null || !(attribute.Name.Trim() != string.Empty) || !(attribute.Value.Trim() != string.Empty))
					{
						continue;
					}
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
					{
						Name = string.Concat("PART::", attribute.Name.ToUpper()),
						Tag = attribute.Value.ToUpper(),
						HeaderText = this.GetGridLanguage(attribute.Value, this.sServerKey),
						AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
						Width = 80
					};
					string upper = attribute.Value.ToUpper();
					if (upper == "PARTNAME" || upper == "PARTNUMBER")
					{
						dataGridViewTextBoxColumn.Visible = true;
					}
					else
					{
						dataGridViewTextBoxColumn.Visible = false;
					}
					this.dgvSearchResults.Columns.Add(dataGridViewTextBoxColumn);
					if (upper != "PARTNUMBER")
					{
						continue;
					}
					hashtables.Add("PartNumber", string.Concat("PART::", attribute.Name.ToUpper()));
				}
				foreach (XmlAttribute xmlAttribute in xPageSchema.Attributes)
				{
					if (xmlAttribute.Name == null || xmlAttribute.Value == null || !(xmlAttribute.Name.Trim() != string.Empty) || !(xmlAttribute.Value.Trim() != string.Empty))
					{
						continue;
					}
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
					{
						Name = string.Concat("PAGE::", xmlAttribute.Name.ToUpper()),
						Tag = xmlAttribute.Value.ToUpper(),
						HeaderText = this.GetGridLanguage(xmlAttribute.Value, this.sServerKey),
						AutoSizeMode = DataGridViewAutoSizeColumnMode.None
					};
					string str = xmlAttribute.Value.ToUpper();
					if (str != "PAGENAME")
					{
						dataGridViewTextBoxColumn1.Visible = false;
					}
					else
					{
						dataGridViewTextBoxColumn1.Visible = true;
					}
					if (str != "PAGENAME")
					{
						dataGridViewTextBoxColumn1.Width = 80;
					}
					else
					{
						dataGridViewTextBoxColumn1.Width = 200;
					}
					this.dgvSearchResults.Columns.Add(dataGridViewTextBoxColumn1);
					if (str == "BOOKCODE")
					{
						hashtables.Add("BookCode", string.Concat("PAGE::", xmlAttribute.Name.ToUpper()));
					}
					if (str == "ID")
					{
						hashtables.Add("PageId", string.Concat("PAGE::", xmlAttribute.Name.ToUpper()));
					}
					if (str == "PICINDEX")
					{
						hashtables.Add("PicIndex", string.Concat("PAGE::", xmlAttribute.Name.ToUpper()));
					}
					if (str != "LISTINDEX")
					{
						continue;
					}
					hashtables.Add("ListIndex", string.Concat("PAGE::", xmlAttribute.Name.ToUpper()));
				}
				DataGridViewColumnCollection booksColumns = this.GetBooksColumns();
				if (booksColumns != null && booksColumns.Count > 0)
				{
					foreach (DataGridViewColumn booksColumn in booksColumns)
					{
						if (booksColumn.Name.ToUpper() == "CHK")
						{
							continue;
						}
						DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
						{
							Name = string.Concat("BOOK::", booksColumn.Name),
							Tag = booksColumn.Tag.ToString(),
							HeaderText = booksColumn.HeaderText,
							AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
							Width = 80
						};
						if (booksColumn.Tag.ToString().ToUpper() != "PUBLISHINGID")
						{
							dataGridViewTextBoxColumn2.Visible = false;
						}
						else
						{
							dataGridViewTextBoxColumn2.Visible = true;
						}
						this.dgvSearchResults.Columns.Add(dataGridViewTextBoxColumn2);
					}
				}
				this.dgvSearchResults.Tag = hashtables;
				return;
			}
			foreach (XmlAttribute attribute1 in xPartSchema.Attributes)
			{
				if (attribute1.Name == null || attribute1.Value == null || !(attribute1.Name.Trim() != string.Empty) || !(attribute1.Value.Trim() != string.Empty))
				{
					continue;
				}
				DataGridViewTextBoxColumn dGHeaderCellValue = new DataGridViewTextBoxColumn()
				{
					Name = string.Concat("PART::", attribute1.Name.ToUpper()),
					Tag = attribute1.Value.ToUpper(),
					HeaderText = this.GetGridLanguage(attribute1.Value, this.sServerKey),
					AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
					Width = 80
				};
				string upper1 = attribute1.Value.ToUpper();
				dGHeaderCellValue.Visible = false;
				for (int i = 0; i < arrayLists.Count; i++)
				{
					if (upper1 == arrayLists[i].ToString().Trim())
					{
						string keyValue = iniFileIO.GetKeyValue("BOOKS_PARTNUMBER_ADVSEARCH", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini"));
						string[] strArrays = keyValue.Split(new char[] { '|' });
						try
						{
							if (strArrays[0].ToString().ToUpper() == "TRUE")
							{
								dGHeaderCellValue.Visible = true;
							}
						}
						catch (Exception exception)
						{
							if (upper1 == "PARTNAME" || upper1 == "PARTNUMBER")
							{
								dGHeaderCellValue.Visible = true;
							}
						}
						try
						{
							if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
							{
								dGHeaderCellValue.HeaderText = strArrays[1];
							}
							else
							{
								dGHeaderCellValue.HeaderText = Global.GetDGHeaderCellValue("BOOKS_PARTNUMBER_ADVSEARCH", upper1, strArrays[1], this.p_ServerId);
							}
						}
						catch (Exception exception1)
						{
						}
						try
						{
							string str1 = strArrays[2];
							string str2 = str1;
							if (str1 != null)
							{
								if (str2 == "L")
								{
									dGHeaderCellValue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
									goto yoyo0;
								}
								else if (str2 == "R")
								{
									dGHeaderCellValue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
									goto yoyo0;
								}
								else
								{
									if (str2 != "C")
									{
										goto yoyo6;
									}
									dGHeaderCellValue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
									goto yoyo0;
								}
							}
						yoyo6:
							dGHeaderCellValue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
						yoyo0:
						}
						catch (Exception exception2)
						{
							dGHeaderCellValue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
						}
						try
						{
							dGHeaderCellValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
							dGHeaderCellValue.Width = Convert.ToInt32(strArrays[3]);
						}
						catch (Exception exception3)
						{
						}
					}
				}
				dataGridViewTextBoxColumns.Add(dGHeaderCellValue);
				if (upper1 != "PARTNUMBER")
				{
					continue;
				}
				hashtables.Add("PartNumber", string.Concat("PART::", attribute1.Name.ToUpper()));
			}
			foreach (XmlAttribute xmlAttribute1 in xPageSchema.Attributes)
			{
				if (xmlAttribute1.Name == null || xmlAttribute1.Value == null || !(xmlAttribute1.Name.Trim() != string.Empty) || !(xmlAttribute1.Value.Trim() != string.Empty))
				{
					continue;
				}
				DataGridViewTextBoxColumn gridLanguage = new DataGridViewTextBoxColumn()
				{
					Name = string.Concat("PAGE::", xmlAttribute1.Name.ToUpper()),
					Tag = xmlAttribute1.Value.ToUpper()
				};
				string upper2 = xmlAttribute1.Value.ToUpper();
				gridLanguage.HeaderText = this.GetGridLanguage(xmlAttribute1.Value, this.sServerKey);
				gridLanguage.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
				if (upper2 != "PAGENAME")
				{
					gridLanguage.Width = 80;
				}
				else
				{
					gridLanguage.Width = 200;
				}
				gridLanguage.Visible = false;
				for (int j = 0; j < arrayLists.Count; j++)
				{
					if (upper2 == arrayLists[j].ToString().Trim())
					{
						string keyValue1 = iniFileIO.GetKeyValue("BOOKS_PARTNUMBER_ADVSEARCH", arrayLists[j].ToString(), string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini"));
						string[] strArrays1 = keyValue1.Split(new char[] { '|' });
						try
						{
							if (strArrays1[0].ToString().ToUpper() == "TRUE")
							{
								gridLanguage.Visible = true;
							}
						}
						catch (Exception exception4)
						{
							if (upper2 == "PAGENAME")
							{
								gridLanguage.Visible = true;
							}
						}
						try
						{
							if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
							{
								gridLanguage.HeaderText = strArrays1[1];
							}
							else
							{
								gridLanguage.HeaderText = Global.GetDGHeaderCellValue("BOOKS_PARTNUMBER_ADVSEARCH", upper2, strArrays1[1], this.frmParent.frmParent.ServerId);
							}
						}
						catch (Exception exception5)
						{
						}
						try
						{
							string str3 = strArrays1[2];
							string str4 = str3;
							if (str3 != null)
							{
								if (str4 == "L")
								{
									gridLanguage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
									goto yoyo2;
								}
								else if (str4 == "R")
								{
									gridLanguage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
									goto yoyo2;
								}
								else
								{
									if (str4 != "C")
									{
										goto yoyo7;
									}
									gridLanguage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
									goto yoyo2;
								}
							}
						yoyo7:
							gridLanguage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
						yoyo2:
						}
						catch (Exception exception6)
						{
							gridLanguage.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
						}
						try
						{
							gridLanguage.Width = Convert.ToInt32(strArrays1[3]);
						}
						catch (Exception exception7)
						{
						}
					}
				}
				bool flag = false;
				foreach (DataGridViewColumn column in this.dgvSearchResults.Columns)
				{
					if (!(column.HeaderText != string.Empty) || !column.Visible || !(column.HeaderText.ToUpper() == gridLanguage.HeaderText.ToUpper()))
					{
						continue;
					}
					flag = true;
					break;
				}
				if (!flag)
				{
					dataGridViewTextBoxColumns.Add(gridLanguage);
				}
				if (upper2 == "BOOKCODE")
				{
					hashtables.Add("BookCode", string.Concat("PAGE::", xmlAttribute1.Name.ToUpper()));
				}
				if (upper2 == "ID")
				{
					hashtables.Add("PageId", string.Concat("PAGE::", xmlAttribute1.Name.ToUpper()));
				}
				if (upper2 == "PICINDEX")
				{
					hashtables.Add("PicIndex", string.Concat("PAGE::", xmlAttribute1.Name.ToUpper()));
				}
				if (upper2 != "LISTINDEX")
				{
					continue;
				}
				hashtables.Add("ListIndex", string.Concat("PAGE::", xmlAttribute1.Name.ToUpper()));
			}
			DataGridViewColumnCollection dataGridViewColumnCollections = this.GetBooksColumns();
			if (dataGridViewColumnCollections != null && dataGridViewColumnCollections.Count > 0)
			{
				foreach (DataGridViewColumn dataGridViewColumn in dataGridViewColumnCollections)
				{
					if (dataGridViewColumn.Name.ToUpper() == "CHK")
					{
						continue;
					}
					DataGridViewTextBoxColumn headerText = new DataGridViewTextBoxColumn()
					{
						Name = string.Concat("BOOK::", dataGridViewColumn.Name),
						Tag = dataGridViewColumn.Tag.ToString()
					};
					string upper3 = dataGridViewColumn.Tag.ToString().ToUpper();
					headerText.HeaderText = dataGridViewColumn.HeaderText;
					headerText.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
					headerText.Width = 80;
					headerText.Visible = false;
					for (int k = 0; k < arrayLists.Count; k++)
					{
						if (upper3 == arrayLists[k].ToString().Trim())
						{
							string keyValue2 = iniFileIO.GetKeyValue("BOOKS_PARTNUMBER_ADVSEARCH", arrayLists[k].ToString(), string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini"));
							string[] strArrays2 = keyValue2.Split(new char[] { '|' });
							try
							{
								if (strArrays2[0].ToString().ToUpper() == "TRUE")
								{
									headerText.Visible = true;
								}
							}
							catch (Exception exception8)
							{
								if (upper3 == "PUBLISHINGID")
								{
									headerText.Visible = true;
								}
							}
							try
							{
								if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
								{
									headerText.HeaderText = strArrays2[1];
								}
								else
								{
									headerText.HeaderText = Global.GetDGHeaderCellValue("BOOKS_PARTNUMBER_ADVSEARCH", upper3, strArrays2[1], this.frmParent.frmParent.ServerId);
								}
							}
							catch (Exception exception9)
							{
							}
							try
							{
								string str5 = strArrays2[2];
								string str6 = str5;
								if (str5 != null)
								{
									if (str6 == "L")
									{
										headerText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
										goto yoyo4;
									}
									else if (str6 == "R")
									{
										headerText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
										goto yoyo4;
									}
									else
									{
										if (str6 != "C")
										{
											goto yoyo8;
										}
										headerText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
										goto yoyo4;
									}
								}
							yoyo8:
								headerText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
							yoyo4:
							}
							catch (Exception exception10)
							{
								headerText.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
							}
							try
							{
								headerText.Width = Convert.ToInt32(strArrays2[3]);
							}
							catch (Exception exception11)
							{
							}
						}
					}
					bool flag1 = false;
					foreach (DataGridViewColumn column1 in this.dgvSearchResults.Columns)
					{
						if (!(column1.Tag.ToString() != string.Empty) || !column1.Visible || !(column1.Tag.ToString().ToUpper() == headerText.Tag.ToString().ToUpper()))
						{
							continue;
						}
						flag1 = true;
						break;
					}
					if (flag1)
					{
						continue;
					}
					dataGridViewTextBoxColumns.Add(headerText);
				}
			}
			try
			{
				for (int l = 0; l < arrayLists.Count; l++)
				{
					foreach (DataGridViewColumn dataGridViewColumn1 in dataGridViewTextBoxColumns)
					{
						if (arrayLists[l].ToString().ToUpper() != dataGridViewColumn1.Tag.ToString().ToUpper())
						{
							continue;
						}
						if (this.dgvSearchResults.Columns.Count <= 0)
						{
							this.dgvSearchResults.Columns.Add(dataGridViewColumn1);
						}
						else
						{
							bool flag2 = false;
							foreach (DataGridViewColumn column2 in this.dgvSearchResults.Columns)
							{
								if (!(column2.Tag.ToString() != string.Empty) || !column2.Visible || !(column2.Tag.ToString().ToUpper() == dataGridViewColumn1.Tag.ToString().ToUpper()))
								{
									continue;
								}
								flag2 = true;
								break;
							}
							if (flag2)
							{
								continue;
							}
							this.dgvSearchResults.Columns.Add(dataGridViewColumn1);
						}
					}
				}
				foreach (DataGridViewColumn dataGridViewColumn2 in dataGridViewTextBoxColumns)
				{
					bool flag3 = false;
					foreach (DataGridViewColumn column3 in this.dgvSearchResults.Columns)
					{
						if (!(column3.Tag.ToString() != string.Empty) || !column3.Visible || !(column3.Tag.ToString().ToUpper() == dataGridViewColumn2.Tag.ToString().ToUpper()))
						{
							continue;
						}
						flag3 = true;
						break;
					}
					if (flag3)
					{
						continue;
					}
					this.dgvSearchResults.Columns.Add(dataGridViewColumn2);
				}
			}
			catch (Exception exception12)
			{
			}
			this.dgvSearchResults.Tag = hashtables;
		}

		private void LoadBooks()
		{
			Global.ComboBoxItem selectedItem = (Global.ComboBoxItem)this.cmbServers.SelectedItem;
			string empty = string.Empty;
			string item = string.Empty;
			try
			{
				int num = int.Parse(selectedItem.Tag.ToString());
				empty = Program.iniServers[num].items["SETTINGS", "CONTENT_PATH"];
				if (!empty.EndsWith("/"))
				{
					empty = string.Concat(empty, "/");
				}
				item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				item = string.Concat(item, "\\", Program.iniServers[num].sIniKey);
				this.sServerKey = Program.iniServers[num].sIniKey;
				if (!Directory.Exists(item))
				{
					Directory.CreateDirectory(item);
				}
			}
			catch
			{
				MessageHandler.ShowMessage(this.frmParent, this.GetResource("(E-PT-CF001) Failed to create file/folder specified.", "(E-PT-CF001)_FAILED", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			this.cmbServers.Enabled = false;
			this.btnSearch.Enabled = false;
			this.ShowLoading(this.pnlSearchCriteria);
			this.bgWorker.RunWorkerAsync(new object[] { empty, item, selectedItem });
		}

		private bool LoadBooksInGrid(string sFilePath)
		{
			bool flag;
			XmlDocument xmlDocument = new XmlDocument();
			bool flag1 = false;
			bool flag2 = false;
			if (!File.Exists(sFilePath))
			{
				return false;
			}
			if (this.p_ServerId == 99999 || Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] == null)
			{
				flag1 = false;
			}
			else
			{
				flag1 = (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() != "ON" ? false : true);
			}
			if (!flag1)
			{
				try
				{
					xmlDocument.Load(sFilePath);
					goto yoyo0;
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
					string empty = string.Empty;
					Global.Unzip(sFilePath);
					xmlDocument.Load(sFilePath.ToLower().Replace(".zip", ".xml"));
				}
				catch
				{
				}
			}
		yoyo0:
			if (this.p_ServerId == 99999 || Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] == null)
			{
				flag2 = false;
			}
			else
			{
				flag2 = (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() != "ON" ? false : true);
			}
			if (flag2)
			{
				string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
				xmlDocument.DocumentElement.InnerXml = str;
			}
			XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
			if (xmlNodes == null)
			{
				return false;
			}
			string name = "";
			string name1 = "";
			string str1 = "";
			foreach (XmlAttribute attribute in xmlNodes.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("ID"))
				{
					name = attribute.Name;
				}
				else if (attribute.Value.ToUpper().Equals("BOOKCODE"))
				{
					string name2 = attribute.Name;
				}
				else if (!attribute.Value.ToUpper().Equals("PUBLISHINGID"))
				{
					if (!attribute.Value.ToUpper().Equals("BOOKTYPE"))
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
			if (name == "" || name1 == "")
			{
				return false;
			}
			this.dgvBooks.Tag = xmlNodes;
			this.InitializeBooksGrid(xmlNodes);
			XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//Books/Book");
			foreach (XmlNode xmlNodes1 in this.frmParent.FilterBooksList(xmlNodes, xmlNodeLists))
			{
				if (str1 != "" && xmlNodes1.Attributes[str1] != null && xmlNodes1.Attributes[str1].Value.ToUpper().Trim() == "GSC" || xmlNodes1.Attributes[name] == null || xmlNodes1.Attributes[name1] == null)
				{
					continue;
				}
				this.AddRowToBooksGrid();
				this.dgvBooks.Rows[this.dgvBooks.Rows.Count - 1].Tag = xmlNodes1.Attributes[name1].Value;
				foreach (XmlAttribute value in xmlNodes1.Attributes)
				{
					if (value.Name == null || value.Value == null || !(value.Name.Trim() != string.Empty) || !(value.Value.Trim() != string.Empty) || !this.dgvBooks.Columns.Contains(value.Name))
					{
						continue;
					}
					this.dgvBooks.Rows[this.dgvBooks.Rows.Count - 1].Cells[value.Name].Value = value.Value;
				}
			}
			return true;
		}

		public void LoadResources()
		{
			this.lblSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.LABEL);
			this.chkMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
			this.chkExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
			this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
			this.ltbKeyword._Caption = this.GetResource("Keyword", "KEYWORD", ResourceType.LABEL);
			this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
		}

		private void ShowHideSearchControls(bool value)
		{
			this.pnlBooks.Visible = value;
			this.pnlControl.Visible = value;
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				if (!parentPanel.InvokeRequired)
				{
					foreach (Control control in parentPanel.Controls)
					{
						if (control == this.picLoading)
						{
							continue;
						}
						control.Visible = false;
					}
					this.picLoading.Parent = parentPanel;
					this.picLoading.BringToFront();
					this.picLoading.Show();
					if (parentPanel.Tag != null && parentPanel.Tag.ToString() == "loading")
					{
						parentPanel.Show();
					}
				}
				else
				{
					frmPartNumberAdvSrch.ShowLoadingDelegate showLoadingDelegate = new frmPartNumberAdvSrch.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { parentPanel };
					parentPanel.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void ShowResultGrid()
		{
			if (!this.pnlSearchResults.InvokeRequired)
			{
				this.pnlSearchResults.BringToFront();
				return;
			}
			this.pnlSearchResults.Invoke(new frmPartNumberAdvSrch.ShowResultGridDelegate(this.ShowResultGrid));
		}

		private void ShowResultMessage(string msg)
		{
			if (!this.pnlrtbNoDetails.InvokeRequired)
			{
				this.pnlrtbNoDetails.BringToFront();
				this.rtbNoDetails.Clear();
				this.rtbNoDetails.SelectionColor = Color.Gray;
				this.rtbNoDetails.SelectedText = msg;
				return;
			}
			Panel panel = this.pnlrtbNoDetails;
			frmPartNumberAdvSrch.ShowResultMessageDelegate showResultMessageDelegate = new frmPartNumberAdvSrch.ShowResultMessageDelegate(this.ShowResultMessage);
			object[] objArray = new object[] { msg };
			panel.Invoke(showResultMessageDelegate, objArray);
		}

		public void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblSearch.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.lblDetails.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgvSearchResults.Font = Settings.Default.appFont;
			this.dgvSearchResults.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgvSearchResults.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
			this.dgvBooks.Font = Settings.Default.appFont;
			this.dgvBooks.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgvBooks.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
			foreach (Control control in this.pnlBooks.Controls)
			{
				if (control.GetType() != typeof(LabledTextBox))
				{
					continue;
				}
				control.Font = Settings.Default.appFont;
			}
		}

		private void UpdateStatus()
		{
			if (this == this.frmParent.GetCurrentChildForm())
			{
				if (this.frmParent.InvokeRequired)
				{
					frmOpenBook _frmOpenBook = this.frmParent;
					frmPartNumberAdvSrch.StatusDelegate statusDelegate = new frmPartNumberAdvSrch.StatusDelegate(this.frmParent.UpdateStatus);
					object[] objArray = new object[] { this.statusText };
					_frmOpenBook.Invoke(statusDelegate, objArray);
					return;
				}
				this.frmParent.UpdateStatus(this.statusText);
			}
		}

		private delegate void AddRowToBooksGridDelegate();

		private delegate void AddRowToResultsGridDelegate();

		private delegate void ChangeSearchButtonTextDelegate(string caption, bool isSearching);

		private delegate void EnableDisableSearchControlsDelegate(bool value);

		private delegate DataGridViewColumnCollection GetBooksColumnsDelegate();

		private delegate bool GetExaceMatchDelegate();

		private delegate string GetKeywordDelegate();

		private delegate bool GetMatchCaseDelegate();

		private delegate ArrayList GetSelectedBookRowsDelegate();

		private delegate void HideLoadingDelegate(Panel parentPanel);

		private delegate void InitializeBooksGridDelegate(XmlNode xSchemaNode);

		private delegate void InitializeResultsGridDelegate(XmlNode xPageSchema, XmlNode xPartSchema);

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void ShowResultGridDelegate();

		private delegate void ShowResultMessageDelegate(string msg);

		private delegate void StatusDelegate(string status);
	}
}
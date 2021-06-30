using GSPcLocalViewer.Classes;
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
	public class frmOpenBookSearch : Form
	{
		private const string dllZipper = "ZIPPER.dll";

		private frmOpenBook frmParent;

		private string statusText;

		private int p_ServerId;

		private Download objDownloader;

		private bool bEncrypted;

		private bool bCompressed;

		private string sServerKey;

		private IContainer components;

		private SplitContainer pnlForm;

		private Panel pnltvSearch;

		private Label lblSearch;

		private Panel pnlDetails;

		private Panel pnlBookInfo;

		private Panel pnlrtbBookInfo;

		private RichTextBox rtbBookInfo;

		private Label lblBookInfo;

		private Panel pnlSearchResults;

		private Label lblDetails;

		private BackgroundWorker bgWorker;

		private Panel pnlSearchCriteria;

		private DataGridView dgvSearchResults;

		private Panel pnlSearch;

		private Panel pnlControl;

		private Button btnSearch;

		private CheckBox checkBoxExactMatch;

		private CheckBox checkBoxMatchCase;

		private LabledTextBox ltbBookId;

		private LabledTextBox ltbBookCode;

		private Label lblAdvancedSearch;

		private Panel pnlrtbNoDetails;

		private RichTextBox rtbNoDetails;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private BackgroundWorker bgLoader;

		private PictureBox picLoading;

		private CustomComboBox cmbServers;

		private DataGridViewTextBoxColumn dgvcBookCode;

		private DataGridViewTextBoxColumn dgvcBookId;

		private DataGridViewTextBoxColumn dgvcUpdateDate;

		public frmOpenBookSearch(frmOpenBook frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.statusText = this.GetResource("Open books by searching", "OPEN_BY_SEARCHING", ResourceType.STATUS_MESSAGE);
			this.lblAdvancedSearch.Image.Tag = "Closed";
			this.cmbServers.Items.Add(new Global.ComboBoxItem("Select a server...", null));
			this.cmbServers.SelectedItem = this.cmbServers.Items[0];
			this.p_ServerId = 0;
			this.bEncrypted = false;
			this.bCompressed = false;
			this.objDownloader = new Download(this.frmParent);
			this.UpdateFont();
			this.LoadResources();
		}

		private void AddAdvancedSearchControls()
		{
			Global.ComboBoxItem selectedItem = (Global.ComboBoxItem)this.cmbServers.SelectedItem;
			if (selectedItem.Tag.ToString().Contains("::"))
			{
				if (!this.LoadAdvanceSearch(string.Empty, selectedItem))
				{
					this.statusText = this.GetResource("(E-OBS-EM006) Failed to create file/folder specified", "(E-OBS-EM006)_FAILED_FOLDER", ResourceType.STATUS_MESSAGE);
				}
				else
				{
					this.statusText = this.GetResource("Enter the search criteria", "ENTER_SEARCH_CRITERIA", ResourceType.STATUS_MESSAGE);
				}
				this.UpdateStatus();
				this.pnlControl.Visible = true;
				this.pnlSearch.Visible = true;
				return;
			}
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
				if (!Directory.Exists(item))
				{
					Directory.CreateDirectory(item);
				}
			}
			catch
			{
				MessageHandler.ShowError(this.GetResource("(E-OBS-EM002) Failed to create file/folder specified", "(E-OBS-EM002)_FAILED_FOLDER", ResourceType.POPUP_MESSAGE));
			}
			this.ShowLoading(this.pnlSearchCriteria);
			this.bgWorker.RunWorkerAsync(new object[] { empty, item, selectedItem });
		}

		private void AddControl(Panel pnl, Control ctl)
		{
			if (pnl.InvokeRequired)
			{
				frmOpenBookSearch.AddControlDelegate addControlDelegate = new frmOpenBookSearch.AddControlDelegate(this.AddControl);
				object[] objArray = new object[] { pnl, ctl };
				pnl.Invoke(addControlDelegate, objArray);
				return;
			}
			ctl.Dock = DockStyle.Top;
			pnl.Controls.Add(ctl);
			ctl.BringToFront();
			if (this.lblAdvancedSearch.Image.Tag.ToString() == "Closed")
			{
				ctl.Hide();
			}
		}

		private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			Hashtable argument = (Hashtable)((object[])e.Argument)[0];
			string empty = string.Empty;
			string selectedNodeTag = "";
			try
			{
				selectedNodeTag = this.GetSelectedNodeTag();
				if (selectedNodeTag.Contains("::"))
				{
					selectedNodeTag = selectedNodeTag.Substring(0, selectedNodeTag.IndexOf("::"));
				}
				int num = int.Parse(selectedNodeTag);
				empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				empty = string.Concat(empty, "\\", Program.iniServers[num].sIniKey);
				empty = string.Concat(empty, "\\Series.xml");
			}
			catch
			{
				e.Result = this.GetResource("(E-OBS-EM006) Failed to create file/folder specified", "(E-OBS-EM006)_FAILED_FOLDER", ResourceType.STATUS_MESSAGE);
				return;
			}
			if (File.Exists(empty))
			{
				e.Result = this.LoadSearchResults(empty, argument);
				return;
			}
			e.Result = this.GetResource("(E-OBS-EM007) Specified information does not exist", "(E-OBS-EM007)_INFORMATION_NOTEXIST", ResourceType.STATUS_MESSAGE);
		}

		private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			string result = (string)e.Result;
			this.HideLoading(this.pnlSearchResults);
			this.pnlSearchCriteria.Enabled = true;
			if (result != "ok")
			{
				this.pnlrtbNoDetails.BringToFront();
				this.rtbNoDetails.Clear();
				this.rtbNoDetails.SelectionColor = Color.Red;
				this.rtbNoDetails.SelectedText = result;
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			object[] argument = (object[])e.Argument;
			string str = (string)argument[0];
			string str1 = (string)argument[1];
			string str2 = string.Concat((string)argument[0], "DataUpdate.xml");
			string str3 = string.Concat((string)argument[1], "\\DataUpdate.xml");
			Global.ComboBoxItem comboBoxItem = (Global.ComboBoxItem)argument[2];
			try
			{
				if (!comboBoxItem.Tag.ToString().Contains("::"))
				{
					this.p_ServerId = int.Parse(comboBoxItem.Tag.ToString());
				}
				else
				{
					this.p_ServerId = int.Parse(comboBoxItem.Tag.ToString().Substring(0, comboBoxItem.Tag.ToString().IndexOf("::")));
				}
			}
			catch
			{
			}
			this.bCompressed = false;
			if (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
			{
				this.bCompressed = true;
			}
			if (!this.bCompressed)
			{
				str = string.Concat(str, "Series.xml");
				str1 = string.Concat(str1, "\\Series.xml");
			}
			else
			{
				str = string.Concat(str, "Series.zip");
				str1 = string.Concat(str1, "\\Series.zip");
			}
			bool flag = false;
			if (!Program.objAppMode.bWorkingOffline)
			{
				this.statusText = this.GetResource("Downloading……", "DOWNLOADING", ResourceType.STATUS_MESSAGE);
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
			}
			if (flag && !Program.objAppMode.bWorkingOffline)
			{
				this.statusText = this.GetResource("Loading……", "LOADING", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				if (!this.objDownloader.DownloadFile(str, str1) && !this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("(E-OBS-EM005) Specified information does not exist", "(E-OBS-EM005)_INFORMATION_NOTEXIST", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					e.Result = false;
				}
			}
			if (File.Exists(str1))
			{
				if (!this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("Finished Loading", "FINISHED_LOADING", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					if (this.LoadAdvanceSearch(str1, comboBoxItem))
					{
						this.statusText = this.GetResource("Enter the search criteria", "ENTER_SEARCH_CRITERIA", ResourceType.STATUS_MESSAGE);
						this.UpdateStatus();
						e.Result = true;
						return;
					}
					this.statusText = this.GetResource("(E-OBS-EM006) Failed to create file/folder specified", "(E-OBS-EM006)_FAILED_FOLDER", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					e.Result = false;
					return;
				}
			}
			else if (!this.frmParent.IsDisposed)
			{
				this.statusText = this.GetResource("(E-OBS-EM007) Specified information does not exist", "(E-OBS-EM007)_INFORMATION_NOTEXIST", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				e.Result = false;
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			bool result = (bool)e.Result;
			if (!this.frmParent.IsDisposed)
			{
				this.HideLoading(this.pnlSearchCriteria);
				if (!result)
				{
					this.pnlControl.Visible = false;
					this.pnlSearch.Visible = false;
				}
			}
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			Hashtable searchCriteria = this.GetSearchCriteria();
			if (searchCriteria.Count > 0)
			{
				this.pnlSearchCriteria.Enabled = false;
				this.ShowLoading(this.pnlSearchResults);
				this.bgLoader.RunWorkerAsync(new object[] { searchCriteria });
				return;
			}
			this.pnlrtbNoDetails.BringToFront();
			this.rtbNoDetails.Clear();
			this.rtbNoDetails.SelectionColor = Color.Gray;
			this.rtbNoDetails.SelectedText = this.GetResource("Enter the search criteria", "ENTER_SEARCH_CRITERIA", ResourceType.LABEL);
			this.rtbBookInfo.Clear();
			this.rtbBookInfo.SelectionColor = Color.Gray;
			this.rtbBookInfo.SelectedText = this.GetResource("Downloading……", "DOWNLOADING", ResourceType.LABEL);
		}

		private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.pnlrtbNoDetails.BringToFront();
				this.rtbNoDetails.Clear();
				this.rtbNoDetails.SelectionColor = Color.Gray;
				this.rtbNoDetails.SelectedText = this.GetResource("Search a book", "SEARCH_A_BOOK", ResourceType.LABEL);
				this.rtbBookInfo.Clear();
				this.rtbBookInfo.SelectionColor = Color.Gray;
				this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
				this.pnlSearch.SuspendLayout();
				if (((ComboBox)sender).SelectedIndex <= 0)
				{
					this.ShowHideSearchControls(false);
				}
				else
				{
					this.ShowHideSearchControls(true);
					this.RemoveAdvancedSearchControls();
					this.AddAdvancedSearchControls();
				}
				this.pnlSearch.ResumeLayout();
			}
			catch
			{
			}
		}

		private void dgvSearchResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			if (this.dgvSearchResults.Tag != null && this.dgvSearchResults.SelectedRows[0].Tag != null)
			{
				try
				{
					empty = this.dgvSearchResults.SelectedRows[0].Cells["dgvcBookId"].Value.ToString();
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.dgvSearchResults.Tag.ToString()));
					XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
					xmlTextReader = new XmlTextReader(new StringReader(this.dgvSearchResults.SelectedRows[0].Tag.ToString()));
					XmlNode xmlNodes1 = xmlDocument.ReadNode(xmlTextReader);
					int num = 0;
					while (num < xmlNodes.Attributes.Count)
					{
						if (xmlNodes.Attributes[num].Value.ToUpper().ToString().Trim() != "BOOKTYPE")
						{
							num++;
						}
						else
						{
							empty1 = xmlNodes.Attributes[num].Name.ToString();
							break;
						}
					}
					if (xmlNodes1.Attributes[empty1] == null)
					{
						this.statusText = this.GetResource("E-OBS-EM008) Not in required format", "(E-OBS-EM008)_NOT_FORMAT", ResourceType.STATUS_MESSAGE);
						this.UpdateStatus();
						MessageHandler.ShowInformation(this.statusText);
					}
					else
					{
						str = xmlNodes1.Attributes[empty1].Value.ToString();
						this.frmParent.CloseAndLoadData(this.p_ServerId, empty, xmlNodes1, xmlNodes, str);
					}
				}
				catch
				{
				}
			}
		}

		private void dgvSearchResults_SelectionChanged(object sender, EventArgs e)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string empty = string.Empty;
			this.rtbBookInfo.SelectionFont = this.rtbBookInfo.Font;
			if (this.dgvSearchResults.SelectedRows == null || this.dgvSearchResults.SelectedRows.Count == 0 || this.dgvSearchResults.Tag == null || this.dgvSearchResults.SelectedRows[0].Tag == null)
			{
				this.rtbBookInfo.Clear();
				this.rtbBookInfo.SelectionColor = Color.Gray;
				this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
			}
			else
			{
				try
				{
					empty = this.dgvSearchResults.Tag.ToString();
					XmlNode xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(empty)));
					empty = this.dgvSearchResults.SelectedRows[0].Tag.ToString();
					XmlNode xmlNodes1 = xmlDocument.ReadNode(new XmlTextReader(new StringReader(empty)));
					this.rtbBookInfo.Clear();
					for (int i = 0; i < xmlNodes.Attributes.Count; i++)
					{
						if (!xmlNodes.Attributes[i].Value.ToUpper().StartsWith("LEVEL") && xmlNodes1.Attributes[xmlNodes.Attributes[i].Name] != null && xmlNodes.Attributes[i].Value.ToUpper() != "BOOKCODE" && xmlNodes.Attributes[i].Value.ToUpper() != "ID")
						{
							this.rtbBookInfo.SelectionColor = Color.Gray;
							this.rtbBookInfo.SelectedText = string.Concat(this.GetLanguage(xmlNodes.Attributes[i].Value, this.sServerKey), ": ");
							this.rtbBookInfo.SelectionColor = Color.Black;
							this.rtbBookInfo.SelectedText = string.Concat(xmlNodes1.Attributes[xmlNodes.Attributes[i].Name].Value, "\n");
						}
					}
				}
				catch
				{
					this.rtbBookInfo.Clear();
					this.rtbBookInfo.SelectionColor = Color.Red;
					this.rtbBookInfo.SelectedText = this.GetResource("(E-OBS-EM011) Failed to load specified object", "(E-OBS-EM011)_FAILED_LOAD", ResourceType.LABEL);
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

		private void EnableDisableLabledTextBox(LabledTextBox ltb, bool enable)
		{
			if (!ltb.InvokeRequired)
			{
				ltb.Enabled = enable;
				return;
			}
			frmOpenBookSearch.EnableDisableLabledTextBoxDelegate enableDisableLabledTextBoxDelegate = new frmOpenBookSearch.EnableDisableLabledTextBoxDelegate(this.EnableDisableLabledTextBox);
			object[] objArray = new object[] { ltb, enable };
			ltb.Invoke(enableDisableLabledTextBoxDelegate, objArray);
		}

		private void frmOpenBookSearch_Load(object sender, EventArgs e)
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
			catch
			{
				base.Hide();
				MessageHandler.ShowQuestion(this.GetResource("(E-OBS-EM001) Failed to load specified object", "(E-OBS-EM001) Failed to load specified object", ResourceType.POPUP_MESSAGE));
				base.Show();
			}
			this.pnlrtbNoDetails.BringToFront();
			this.rtbNoDetails.Clear();
			this.rtbNoDetails.SelectionColor = Color.Gray;
			this.rtbNoDetails.SelectedText = this.GetResource("Search a book", "SEARCH_A_BOOK", ResourceType.LABEL);
			this.rtbBookInfo.Clear();
			this.rtbBookInfo.SelectionColor = Color.Gray;
			this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
		}

		private void frmOpenBookSearch_VisibleChanged(object sender, EventArgs e)
		{
			this.UpdateStatus();
		}

		private void GetAdvSearchFiledTitleFrmServerINI(LabledTextBox ltbxCurField, string strKeyID)
		{
			try
			{
				IniFileIO iniFileIO = new IniFileIO();
				string keyValue = iniFileIO.GetKeyValue("OPENBOOK_ADVSEARCH_SETTINGS", strKeyID.ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.p_ServerId].sIniKey, ".ini"));
				if (string.IsNullOrEmpty(keyValue))
				{
					ltbxCurField.Show();
				}
				else
				{
					char[] chrArray = new char[] { '|' };
					if (!Convert.ToBoolean(keyValue.Split(chrArray)[0]))
					{
						ltbxCurField.Hide();
					}
					else
					{
						string @default = Settings.Default.appLanguage;
						if (@default.ToUpper() != "ENGLISH")
						{
							string str = strKeyID.ToString();
							string[] startupPath = new string[] { Application.StartupPath, "\\Language XMLs\\", @default, "_GSP_", Program.iniServers[this.p_ServerId].sIniKey, ".ini" };
							string keyValue1 = iniFileIO.GetKeyValue("OPENBOOK_ADVSEARCH_SETTINGS", str, string.Concat(startupPath));
							ltbxCurField._Caption = keyValue1;
							ltbxCurField.Show();
						}
						else
						{
							char[] chrArray1 = new char[] { '|' };
							ltbxCurField._Caption = keyValue.Split(chrArray1)[1];
							ltbxCurField.Show();
						}
					}
				}
			}
			catch (Exception exception)
			{
				ltbxCurField.Show();
			}
		}

		private string GetLanguage(string sKey, string sServerKey)
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
				str = string.Concat(str, "/Screen[@Name='OPENBOOKSEARCH']");
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

		private Hashtable GetSearchCriteria()
		{
			Hashtable hashtables = new Hashtable();
			for (int i = this.pnlSearch.Controls.Count - 1; i >= 0; i--)
			{
				if (this.pnlSearch.Controls[i].GetType() == typeof(LabledTextBox) && ((LabledTextBox)this.pnlSearch.Controls[i])._Text.Trim() != string.Empty && ((LabledTextBox)this.pnlSearch.Controls[i]).Enabled)
				{
					try
					{
						hashtables.Add(((LabledTextBox)this.pnlSearch.Controls[i])._Name, ((LabledTextBox)this.pnlSearch.Controls[i])._Text.Trim());
					}
					catch
					{
					}
				}
			}
			return hashtables;
		}

		private string GetSelectedNodeTag()
		{
			if (this.cmbServers.InvokeRequired)
			{
				return (string)this.cmbServers.Invoke(new frmOpenBookSearch.GetSelectedNodeTagDelegate(this.GetSelectedNodeTag));
			}
			return ((Global.ComboBoxItem)this.cmbServers.SelectedItem).Tag.ToString();
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern long HideCaret(IntPtr hwnd);

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				if (this.cmbServers.Items.Count != 2)
				{
					this.cmbServers.Enabled = true;
				}
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
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.pnlForm = new SplitContainer();
			this.pnlSearchCriteria = new Panel();
			this.pnlSearch = new Panel();
			this.lblAdvancedSearch = new Label();
			this.ltbBookId = new LabledTextBox();
			this.ltbBookCode = new LabledTextBox();
			this.pnlControl = new Panel();
			this.btnSearch = new Button();
			this.checkBoxExactMatch = new CheckBox();
			this.checkBoxMatchCase = new CheckBox();
			this.pnltvSearch = new Panel();
			this.cmbServers = new CustomComboBox();
			this.lblSearch = new Label();
			this.pnlDetails = new Panel();
			this.pnlSearchResults = new Panel();
			this.dgvSearchResults = new DataGridView();
			this.pnlrtbNoDetails = new Panel();
			this.rtbNoDetails = new RichTextBox();
			this.pnlBookInfo = new Panel();
			this.pnlrtbBookInfo = new Panel();
			this.rtbBookInfo = new RichTextBox();
			this.lblBookInfo = new Label();
			this.lblDetails = new Label();
			this.bgWorker = new BackgroundWorker();
			this.bgLoader = new BackgroundWorker();
			this.picLoading = new PictureBox();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dgvcBookCode = new DataGridViewTextBoxColumn();
			this.dgvcBookId = new DataGridViewTextBoxColumn();
			this.dgvcUpdateDate = new DataGridViewTextBoxColumn();
			this.pnlForm.Panel1.SuspendLayout();
			this.pnlForm.Panel2.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.pnlSearchCriteria.SuspendLayout();
			this.pnlSearch.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.pnltvSearch.SuspendLayout();
			this.pnlDetails.SuspendLayout();
			this.pnlSearchResults.SuspendLayout();
			((ISupportInitialize)this.dgvSearchResults).BeginInit();
			this.pnlrtbNoDetails.SuspendLayout();
			this.pnlBookInfo.SuspendLayout();
			this.pnlrtbBookInfo.SuspendLayout();
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
			this.pnlForm.Size = new System.Drawing.Size(578, 409);
			this.pnlForm.SplitterDistance = 270;
			this.pnlForm.TabIndex = 18;
			this.pnlSearchCriteria.BackColor = Color.White;
			this.pnlSearchCriteria.Controls.Add(this.pnlSearch);
			this.pnlSearchCriteria.Controls.Add(this.pnlControl);
			this.pnlSearchCriteria.Dock = DockStyle.Fill;
			this.pnlSearchCriteria.Location = new Point(0, 70);
			this.pnlSearchCriteria.Name = "pnlSearchCriteria";
			this.pnlSearchCriteria.Size = new System.Drawing.Size(268, 337);
			this.pnlSearchCriteria.TabIndex = 16;
			this.pnlSearch.AutoScroll = true;
			this.pnlSearch.BackColor = Color.White;
			this.pnlSearch.Controls.Add(this.lblAdvancedSearch);
			this.pnlSearch.Controls.Add(this.ltbBookId);
			this.pnlSearch.Controls.Add(this.ltbBookCode);
			this.pnlSearch.Dock = DockStyle.Fill;
			this.pnlSearch.Location = new Point(0, 59);
			this.pnlSearch.Name = "pnlSearch";
			this.pnlSearch.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
			this.pnlSearch.Size = new System.Drawing.Size(268, 278);
			this.pnlSearch.TabIndex = 23;
			this.pnlSearch.Visible = false;
			this.lblAdvancedSearch.BackColor = Color.Transparent;
			this.lblAdvancedSearch.Cursor = Cursors.Hand;
			this.lblAdvancedSearch.Dock = DockStyle.Top;
			this.lblAdvancedSearch.ForeColor = Color.Blue;
			this.lblAdvancedSearch.Image = Resources.GroupLine3;
			this.lblAdvancedSearch.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblAdvancedSearch.Location = new Point(15, 48);
			this.lblAdvancedSearch.Name = "lblAdvancedSearch";
			this.lblAdvancedSearch.Size = new System.Drawing.Size(253, 41);
			this.lblAdvancedSearch.TabIndex = 12;
			this.lblAdvancedSearch.Tag = "";
			this.lblAdvancedSearch.Text = "Advanced Search";
			this.lblAdvancedSearch.TextAlign = ContentAlignment.MiddleLeft;
			this.lblAdvancedSearch.Click += new EventHandler(this.lblAdvancedSearch_Click);
			this.ltbBookId._Caption = "Publishing Id";
			this.ltbBookId._Name = "PublishingID";
			this.ltbBookId._Text = "";
			this.ltbBookId.Dock = DockStyle.Top;
			this.ltbBookId.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ltbBookId.Location = new Point(15, 24);
			this.ltbBookId.Name = "ltbBookId";
			this.ltbBookId.Size = new System.Drawing.Size(253, 24);
			this.ltbBookId.TabIndex = 1;
			this.ltbBookCode._Caption = "Book Code";
			this.ltbBookCode._Name = "BookCode";
			this.ltbBookCode._Text = "";
			this.ltbBookCode.Dock = DockStyle.Top;
			this.ltbBookCode.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ltbBookCode.Location = new Point(15, 0);
			this.ltbBookCode.Name = "ltbBookCode";
			this.ltbBookCode.Size = new System.Drawing.Size(253, 24);
			this.ltbBookCode.TabIndex = 0;
			this.pnlControl.Controls.Add(this.btnSearch);
			this.pnlControl.Controls.Add(this.checkBoxExactMatch);
			this.pnlControl.Controls.Add(this.checkBoxMatchCase);
			this.pnlControl.Dock = DockStyle.Top;
			this.pnlControl.Location = new Point(0, 0);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(268, 59);
			this.pnlControl.TabIndex = 22;
			this.pnlControl.Visible = false;
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
			this.checkBoxExactMatch.AutoSize = true;
			this.checkBoxExactMatch.Location = new Point(15, 11);
			this.checkBoxExactMatch.Name = "checkBoxExactMatch";
			this.checkBoxExactMatch.Size = new System.Drawing.Size(85, 17);
			this.checkBoxExactMatch.TabIndex = 4;
			this.checkBoxExactMatch.Text = "Exact Match";
			this.checkBoxExactMatch.UseVisualStyleBackColor = true;
			this.checkBoxMatchCase.AutoSize = true;
			this.checkBoxMatchCase.Location = new Point(15, 34);
			this.checkBoxMatchCase.Name = "checkBoxMatchCase";
			this.checkBoxMatchCase.Size = new System.Drawing.Size(82, 17);
			this.checkBoxMatchCase.TabIndex = 3;
			this.checkBoxMatchCase.Text = "Match Case";
			this.checkBoxMatchCase.UseVisualStyleBackColor = true;
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
			this.pnlDetails.Controls.Add(this.pnlBookInfo);
			this.pnlDetails.Dock = DockStyle.Fill;
			this.pnlDetails.Location = new Point(0, 27);
			this.pnlDetails.Name = "pnlDetails";
			this.pnlDetails.Size = new System.Drawing.Size(302, 380);
			this.pnlDetails.TabIndex = 15;
			this.pnlSearchResults.BackColor = Color.White;
			this.pnlSearchResults.Controls.Add(this.dgvSearchResults);
			this.pnlSearchResults.Controls.Add(this.pnlrtbNoDetails);
			this.pnlSearchResults.Dock = DockStyle.Fill;
			this.pnlSearchResults.Location = new Point(0, 0);
			this.pnlSearchResults.Name = "pnlSearchResults";
			this.pnlSearchResults.Size = new System.Drawing.Size(302, 206);
			this.pnlSearchResults.TabIndex = 13;
			this.pnlSearchResults.Tag = "";
			this.dgvSearchResults.AllowUserToAddRows = false;
			this.dgvSearchResults.AllowUserToDeleteRows = false;
			this.dgvSearchResults.AllowUserToResizeRows = false;
			this.dgvSearchResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvSearchResults.BackgroundColor = Color.White;
			this.dgvSearchResults.BorderStyle = BorderStyle.None;
			this.dgvSearchResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DataGridViewColumnCollection columns = this.dgvSearchResults.Columns;
			DataGridViewColumn[] dataGridViewColumnArray = new DataGridViewColumn[] { this.dgvcBookCode, this.dgvcBookId, this.dgvcUpdateDate };
			columns.AddRange(dataGridViewColumnArray);
			this.dgvSearchResults.Dock = DockStyle.Fill;
			this.dgvSearchResults.Location = new Point(0, 0);
			this.dgvSearchResults.MultiSelect = false;
			this.dgvSearchResults.Name = "dgvSearchResults";
			this.dgvSearchResults.ReadOnly = true;
			this.dgvSearchResults.RowHeadersVisible = false;
			this.dgvSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvSearchResults.Size = new System.Drawing.Size(302, 206);
			this.dgvSearchResults.TabIndex = 0;
			this.dgvSearchResults.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvSearchResults_CellDoubleClick);
			this.dgvSearchResults.SelectionChanged += new EventHandler(this.dgvSearchResults_SelectionChanged);
			this.pnlrtbNoDetails.BackColor = Color.White;
			this.pnlrtbNoDetails.Controls.Add(this.rtbNoDetails);
			this.pnlrtbNoDetails.Dock = DockStyle.Fill;
			this.pnlrtbNoDetails.Location = new Point(0, 0);
			this.pnlrtbNoDetails.Name = "pnlrtbNoDetails";
			this.pnlrtbNoDetails.Padding = new System.Windows.Forms.Padding(25, 10, 0, 0);
			this.pnlrtbNoDetails.Size = new System.Drawing.Size(302, 206);
			this.pnlrtbNoDetails.TabIndex = 16;
			this.pnlrtbNoDetails.Tag = "";
			this.rtbNoDetails.BackColor = Color.White;
			this.rtbNoDetails.BorderStyle = BorderStyle.None;
			this.rtbNoDetails.Dock = DockStyle.Fill;
			this.rtbNoDetails.Location = new Point(25, 10);
			this.rtbNoDetails.Name = "rtbNoDetails";
			this.rtbNoDetails.ReadOnly = true;
			this.rtbNoDetails.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtbNoDetails.Size = new System.Drawing.Size(277, 196);
			this.rtbNoDetails.TabIndex = 13;
			this.rtbNoDetails.TabStop = false;
			this.rtbNoDetails.Text = "";
			this.pnlBookInfo.BackColor = Color.White;
			this.pnlBookInfo.Controls.Add(this.pnlrtbBookInfo);
			this.pnlBookInfo.Controls.Add(this.lblBookInfo);
			this.pnlBookInfo.Dock = DockStyle.Bottom;
			this.pnlBookInfo.Location = new Point(0, 206);
			this.pnlBookInfo.Name = "pnlBookInfo";
			this.pnlBookInfo.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
			this.pnlBookInfo.Size = new System.Drawing.Size(302, 174);
			this.pnlBookInfo.TabIndex = 14;
			this.pnlBookInfo.Tag = "";
			this.pnlrtbBookInfo.BackColor = Color.White;
			this.pnlrtbBookInfo.Controls.Add(this.rtbBookInfo);
			this.pnlrtbBookInfo.Dock = DockStyle.Fill;
			this.pnlrtbBookInfo.Location = new Point(15, 38);
			this.pnlrtbBookInfo.Name = "pnlrtbBookInfo";
			this.pnlrtbBookInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnlrtbBookInfo.Size = new System.Drawing.Size(287, 136);
			this.pnlrtbBookInfo.TabIndex = 15;
			this.rtbBookInfo.BackColor = Color.White;
			this.rtbBookInfo.BorderStyle = BorderStyle.None;
			this.rtbBookInfo.Dock = DockStyle.Fill;
			this.rtbBookInfo.Location = new Point(10, 0);
			this.rtbBookInfo.Name = "rtbBookInfo";
			this.rtbBookInfo.ReadOnly = true;
			this.rtbBookInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtbBookInfo.Size = new System.Drawing.Size(277, 136);
			this.rtbBookInfo.TabIndex = 12;
			this.rtbBookInfo.TabStop = false;
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
			this.bgLoader.WorkerSupportsCancellation = true;
			this.bgLoader.DoWork += new DoWorkEventHandler(this.bgLoader_DoWork);
			this.bgLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgLoader_RunWorkerCompleted);
			this.picLoading.BackColor = Color.Transparent;
			this.picLoading.Dock = DockStyle.Fill;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(0, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(578, 409);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 20;
			this.picLoading.TabStop = false;
			this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.HeaderText = "Book Publishing Id";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewTextBoxColumn2.HeaderText = "UpdateDate";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.HeaderText = "UpdateDate";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			this.dataGridViewTextBoxColumn3.Width = 107;
			this.dgvcBookCode.HeaderText = "BookCode";
			this.dgvcBookCode.Name = "dgvcBookCode";
			this.dgvcBookCode.ReadOnly = true;
			this.dgvcBookId.HeaderText = "PublishingId";
			this.dgvcBookId.Name = "dgvcBookId";
			this.dgvcBookId.ReadOnly = true;
			this.dgvcUpdateDate.HeaderText = "UpdateDate";
			this.dgvcUpdateDate.Name = "dgvcUpdateDate";
			this.dgvcUpdateDate.ReadOnly = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(578, 409);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.picLoading);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmOpenBookSearch";
			this.Text = "frmOpenBookSearch";
			base.Load += new EventHandler(this.frmOpenBookSearch_Load);
			base.VisibleChanged += new EventHandler(this.frmOpenBookSearch_VisibleChanged);
			this.pnlForm.Panel1.ResumeLayout(false);
			this.pnlForm.Panel2.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnlSearchCriteria.ResumeLayout(false);
			this.pnlSearch.ResumeLayout(false);
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			this.pnltvSearch.ResumeLayout(false);
			this.pnlDetails.ResumeLayout(false);
			this.pnlSearchResults.ResumeLayout(false);
			((ISupportInitialize)this.dgvSearchResults).EndInit();
			this.pnlrtbNoDetails.ResumeLayout(false);
			this.pnlBookInfo.ResumeLayout(false);
			this.pnlrtbBookInfo.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
		}

		private void lblAdvancedSearch_Click(object sender, EventArgs e)
		{
			if (this.lblAdvancedSearch.Image.Tag.ToString() == "Opened")
			{
				this.lblAdvancedSearch.Image = Resources.GroupLine3;
				this.lblAdvancedSearch.Image.Tag = "Closed";
				foreach (Control control in this.pnlSearch.Controls)
				{
					if (control.GetType() != typeof(LabledTextBox) || control == this.ltbBookCode || control == this.ltbBookId)
					{
						continue;
					}
					control.Hide();
				}
			}
			else if (this.lblAdvancedSearch.Image.Tag.ToString() == "Closed")
			{
				this.lblAdvancedSearch.Image = Resources.GroupLine2;
				this.lblAdvancedSearch.Image.Tag = "Opened";
				for (int i = 0; i < this.pnlSearch.Controls.Count; i++)
				{
					Control item = this.pnlSearch.Controls[i];
					if (item.GetType() == typeof(LabledTextBox) && item != this.ltbBookCode && item != this.ltbBookId)
					{
						LabledTextBox labledTextBox = (LabledTextBox)item;
						this.GetAdvSearchFiledTitleFrmServerINI(labledTextBox, labledTextBox._ID);
					}
				}
			}
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

		private bool LoadAdvanceSearch(string sFilePath, Global.ComboBoxItem objComboBoxItem)
		{
			int num;
			bool flag;
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodes = null;
			if (!objComboBoxItem.Tag.ToString().Contains("::"))
			{
				if (!File.Exists(sFilePath))
				{
					return false;
				}
				try
				{
					this.p_ServerId = int.Parse(objComboBoxItem.Tag.ToString());
				}
				catch
				{
					flag = false;
					return flag;
				}
				try
				{
					this.LoadSeries(ref xmlDocument, this.p_ServerId, sFilePath);
					xmlNodes = xmlDocument.SelectSingleNode("//Schema");
					goto Label0;
				}
				catch
				{
					flag = false;
				}
			}
			else
			{
				try
				{
					this.p_ServerId = int.Parse(objComboBoxItem.Tag.ToString().Substring(0, objComboBoxItem.Tag.ToString().IndexOf("::")));
				}
				catch
				{
					flag = false;
					return flag;
				}
				try
				{
					string empty = string.Empty;
					empty = objComboBoxItem.Tag.ToString();
					empty = empty.Substring(empty.IndexOf("::") + 2, empty.Length - (empty.IndexOf("::") + 2));
					xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(empty)));
					goto Label0;
				}
				catch
				{
					flag = false;
				}
			}
			return flag;
		Label0:
			if (xmlNodes == null || xmlNodes.Attributes.Count == 0)
			{
				return false;
			}
			this.EnableDisableLabledTextBox(this.ltbBookCode, false);
			this.EnableDisableLabledTextBox(this.ltbBookId, false);
			if (!objComboBoxItem.Tag.ToString().Contains("::"))
			{
				num = int.Parse(objComboBoxItem.Tag.ToString());
			}
			else
			{
				string str = objComboBoxItem.Tag.ToString();
				num = int.Parse(str.Substring(0, 1));
			}
			this.sServerKey = Program.iniServers[num].sIniKey;
			foreach (XmlAttribute attribute in xmlNodes.Attributes)
			{
				if (!attribute.Value.ToUpper().StartsWith("LEVEL") && !attribute.Value.ToUpper().Equals("BOOKCODE") && !attribute.Value.ToUpper().Equals("PUBLISHINGID"))
				{
					LabledTextBox labledTextBox = new LabledTextBox(attribute.Value)
					{
						_Name = attribute.Name,
						_Caption = this.GetLanguage(attribute.Value, this.sServerKey),
						_ID = this.GetLanguage(attribute.Value, this.sServerKey)
					};
					this.AddControl(this.pnlSearch, labledTextBox);
				}
				if (attribute.Value.ToUpper().Equals("BOOKCODE"))
				{
					this.EnableDisableLabledTextBox(this.ltbBookCode, true);
					this.ltbBookCode._Name = attribute.Name;
				}
				if (!attribute.Value.ToUpper().Equals("PUBLISHINGID"))
				{
					continue;
				}
				this.EnableDisableLabledTextBox(this.ltbBookId, true);
				this.ltbBookId._Name = attribute.Name;
			}
			if (!objComboBoxItem.Tag.ToString().Contains("::"))
			{
				objComboBoxItem.Tag = string.Concat(objComboBoxItem.Tag, "::", xmlNodes.OuterXml);
			}
			return true;
		}

		public void LoadResources()
		{
			this.lblSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.LABEL);
			this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
			this.checkBoxMatchCase.Text = this.GetResource("Match Case", "EXACT_CASE", ResourceType.CHECK_BOX);
			this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
			this.ltbBookCode._Caption = this.GetResource("Book Code", "BOOK_CODE", ResourceType.LABEL);
			this.ltbBookId._Caption = this.GetResource("Publishing Id", "PUBLISHING_ID", ResourceType.LABEL);
			this.lblAdvancedSearch.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.LABEL);
			this.lblBookInfo.Text = this.GetResource("Book Information", "BOOK_INFORMATION", ResourceType.LABEL);
			this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
		}

		private string LoadSearchResults(string sSeriesFile, Hashtable hsCriteria)
		{
			string resource;
			if (this.dgvSearchResults.InvokeRequired)
			{
				DataGridView dataGridView = this.dgvSearchResults;
				frmOpenBookSearch.LoadSearchResultsDelegate loadSearchResultsDelegate = new frmOpenBookSearch.LoadSearchResultsDelegate(this.LoadSearchResults);
				object[] objArray = new object[] { sSeriesFile, hsCriteria };
				return (string)dataGridView.Invoke(loadSearchResultsDelegate, objArray);
			}
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				string empty = string.Empty;
				string name = string.Empty;
				string str = string.Empty;
				this.dgvSearchResults.Rows.Clear();
				if (this.LoadSeries(ref xmlDocument, this.p_ServerId, sSeriesFile))
				{
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
					if (xmlNodes == null)
					{
						resource = this.GetResource("(E-OBS-EM009) Failed to load specified object", "(E-OBS-EM009)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
					}
					else
					{
						this.dgvSearchResults.Tag = xmlNodes.OuterXml;
						foreach (XmlAttribute attribute in xmlNodes.Attributes)
						{
							if (attribute.Value.ToUpper().Equals("BOOKCODE"))
							{
								empty = attribute.Name;
							}
							else if (!attribute.Value.ToUpper().Equals("PUBLISHINGID"))
							{
								if (!attribute.Value.ToUpper().Equals("UPDATEDATE"))
								{
									continue;
								}
								str = attribute.Name;
							}
							else
							{
								name = attribute.Name;
							}
						}
						string str1 = "//Book";
						IDictionaryEnumerator enumerator = hsCriteria.GetEnumerator();
						while (enumerator.MoveNext())
						{
							try
							{
								if (this.checkBoxMatchCase.Checked)
								{
									if (!this.checkBoxExactMatch.Checked)
									{
										string str2 = str1;
										string[] strArrays = new string[] { str2, "[contains(@", enumerator.Key.ToString(), ",'", enumerator.Value.ToString(), "')]" };
										str1 = string.Concat(strArrays);
									}
									else
									{
										string str3 = str1;
										string[] strArrays1 = new string[] { str3, "[@", enumerator.Key.ToString(), "='", enumerator.Value.ToString(), "']" };
										str1 = string.Concat(strArrays1);
									}
								}
								else if (!this.checkBoxExactMatch.Checked)
								{
									string str4 = str1;
									string[] strArrays2 = new string[] { str4, "[contains(translate(@", enumerator.Key.ToString(), ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'", enumerator.Value.ToString().ToUpper(), "')]" };
									str1 = string.Concat(strArrays2);
								}
								else
								{
									string str5 = str1;
									string[] strArrays3 = new string[] { str5, "[translate(@", enumerator.Key.ToString(), ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", enumerator.Value.ToString().ToUpper(), "']" };
									str1 = string.Concat(strArrays3);
								}
							}
							catch
							{
							}
						}
						XmlNodeList xmlNodeLists = xmlDocument.SelectNodes(str1);
						if (xmlNodeLists == null || xmlNodeLists.Count == 0)
						{
							resource = "No results found";
						}
						else
						{
							Filter filter = new Filter(this.frmParent.frmParent);
							xmlNodeLists = filter.FilterBooksList(xmlNodes, xmlNodeLists);
							foreach (XmlNode xmlNodes1 in xmlNodeLists)
							{
								if (xmlNodes1.Attributes.Count <= 0)
								{
									continue;
								}
								this.dgvSearchResults.Rows.Add();
								this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Selected = false;
								this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Tag = xmlNodes1.OuterXml;
								this.dgvcBookCode.HeaderText = this.GetLanguage(this.dgvcBookCode.HeaderText, this.sServerKey);
								this.dgvcBookId.HeaderText = this.GetLanguage(this.dgvcBookId.HeaderText, this.sServerKey);
								this.dgvcUpdateDate.HeaderText = this.GetLanguage(this.dgvcUpdateDate.HeaderText, this.sServerKey);
								this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[0].Value = xmlNodes1.Attributes[empty].Value;
								this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[1].Value = xmlNodes1.Attributes[name].Value;
								this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[2].Value = xmlNodes1.Attributes[str].Value;
								Application.DoEvents();
							}
							this.dgvSearchResults.BringToFront();
							resource = "ok";
						}
					}
				}
				else
				{
					resource = this.GetResource("(E-OBS-EM008) Not in required format", "(E-OBS-EM008)_NOT_FORMAT", ResourceType.STATUS_MESSAGE);
				}
			}
			catch
			{
				resource = this.GetResource("(E-OBS-EM010) Encountered problem while searching", "(E-OBS-EM010)_ENCOUNTERED_PROBLEM", ResourceType.STATUS_MESSAGE);
			}
			return resource;
		}

		private bool LoadSeries(ref XmlDocument xmlDoc, int iServerId, string sFilePath)
		{
			bool flag = true;
			this.bCompressed = false;
			this.bEncrypted = false;
			try
			{
				if (Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
				{
					this.bEncrypted = true;
				}
				if (Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
				{
					this.bCompressed = true;
				}
				if (!this.bCompressed)
				{
					xmlDoc.Load(sFilePath);
				}
				else
				{
					string empty = string.Empty;
					Global.Unzip(sFilePath);
					empty = sFilePath.ToLower().Replace(".zip", ".xml");
					xmlDoc.Load(empty);
				}
				if (this.bEncrypted)
				{
					AES aE = new AES();
					string str = aE.Decode(xmlDoc.InnerText, "0123456789ABCDEF");
					xmlDoc.DocumentElement.InnerXml = str;
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private void RemoveAdvancedSearchControls()
		{
			try
			{
				for (int i = this.pnlSearch.Controls.Count - 1; i >= 0; i--)
				{
					if (this.pnlSearch.Controls[i].GetType() == typeof(LabledTextBox) && this.pnlSearch.Controls[i] != this.ltbBookCode && this.pnlSearch.Controls[i] != this.ltbBookId)
					{
						this.pnlSearch.Controls.Remove(this.pnlSearch.Controls[i]);
					}
				}
			}
			catch
			{
			}
		}

		private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
		{
			frmOpenBookSearch.HideCaret(this.rtbBookInfo.Handle);
		}

		private void ShowHideSearchControls(bool value)
		{
			this.pnlSearch.Visible = value;
			this.pnlControl.Visible = value;
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				this.cmbServers.Enabled = false;
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
			}
			catch
			{
			}
		}

		public void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblSearch.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.lblDetails.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgvSearchResults.Font = Settings.Default.appFont;
			this.dgvSearchResults.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgvSearchResults.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
			foreach (Control control in this.pnlSearch.Controls)
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
					frmOpenBookSearch.StatusDelegate statusDelegate = new frmOpenBookSearch.StatusDelegate(this.frmParent.UpdateStatus);
					object[] objArray = new object[] { this.statusText };
					_frmOpenBook.Invoke(statusDelegate, objArray);
					return;
				}
				this.frmParent.UpdateStatus(this.statusText);
			}
		}

		private delegate void AddControlDelegate(Panel pnl, Control ctl);

		private delegate void EnableDisableLabledTextBoxDelegate(LabledTextBox ltb, bool enable);

		private delegate string GetSelectedNodeTagDelegate();

		private delegate string LoadSearchResultsDelegate(string sSeriesFile, Hashtable hsCriteria);

		private delegate void StatusDelegate(string status);
	}
}
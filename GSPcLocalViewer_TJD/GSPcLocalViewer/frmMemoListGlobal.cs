using AxDjVuCtrlLib;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmMemoListGlobal : Form
	{
		private frmMemoList frmParent;

		private bool bMemoChanged;

		public int intMemoType;

		private string strDateFormat = string.Empty;

		public List<string> lstExportedGlobalMemoListPictures = new List<string>();

		private string strDjVuPicPath = string.Empty;

		private BackgroundWorker bgWorker;

		private int intSeconds;

		private bool bPageChanged;

		public bool bSaveMemoOnBookLevel;

		private IContainer components;

		public Panel pnlControl;

		public Button btnOK;

		public Button btnCancel;

		public Panel pnlForm;

		public DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		public DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		public DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		public OpenFileDialog ofd;

		public Panel pnlOptions;

		public Label lblGlobalMemo;

		public System.Windows.Forms.ContextMenuStrip cmsGlobalMemo;

		public ToolStripMenuItem goToPageToolStripMenuItem;

		public Panel pnlBottom;

		public Panel pnlMemos;

		public Panel pnlTxtMemo;

		public Panel pnlTxtMemoContents;

		public Panel pnlRtbTxtMemo;

		public RichTextBox rtbTxtMemo;

		public Panel pnlTxtMemoTop;

		public Label lblTxtMemoDate;

		public Label lblTxtMemoTitle;

		public Panel pnlRefMemo;

		public Panel pnlRefMemoContents;

		public Button btnRefMemoOpen;

		public Label lblRefMemoNote;

		public Label lblRefMemoOtherRef;

		public Label lblRefMemoBookId;

		public TextBox txtRefMemoOtherRef;

		public Label lblRefMemoServerKey;

		public TextBox txtRefMemoBookId;

		public TextBox txtRefMemoServerKey;

		public Panel pnlRefMemoTop;

		public Label lblRefMemoDate;

		public Label lblRefMemoTitle;

		public Panel pnlPrgMemo;

		public Panel pnlPrgMemoContents;

		public Button btnPrgMemoExePathBrowse;

		public Button btnPrgMemoOpen;

		public Label lblPrgMemoCmdLine;

		public TextBox txtPrgMemoCmdLine;

		public Label lblPrgMemoExePath;

		public TextBox txtPrgMemoExePath;

		public Panel pnlPrgMemoTop;

		public Label lblPrgMemoDate;

		public Label lblPrgMemoTitle;

		public Panel pnlError;

		public Label lblError;

		public Panel pnlSplitter2;

		public Panel pnlSplitter1;

		public Panel pnlHypMemo;

		private Panel pnlHypMemoPreview;

		private PictureBox picBoxHypPreview;

		public AxDjVuCtrl objDjVuCtlMemoListGlobal;

		private Panel pnlHypMemoContents;

		public Label lblDescription;

		public TextBox txtDescription;

		private Button btnHypMemoOpen;

		public Label lblHypMemoNote;

		public Label lblHypMemoUrl;

		public TextBox txtHypMemoUrl;

		private Panel pnlHypMemoTop;

		public Label lblHypMemoDate;

		public Label lblHypMemoTitle;

		public Panel pnlTop;

		public Panel pnlGrid;

		public DataGridView dgMemoList;

		public DataGridViewTextBoxColumn Column1;

		public DataGridViewTextBoxColumn Column2;

		public DataGridViewTextBoxColumn Column3;

		public string[] getMemos
		{
			get
			{
				if (this.dgMemoList.Rows.Count <= 0)
				{
					return null;
				}
				string[] str = new string[this.dgMemoList.Rows.Count];
				for (int i = 0; i < this.dgMemoList.Rows.Count; i++)
				{
					str[i] = this.dgMemoList.Rows[i].Tag.ToString();
				}
				return str;
			}
		}

		public bool isMemoChanged
		{
			get
			{
				return this.bMemoChanged;
			}
		}

		public frmMemoListGlobal(frmMemoList frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.intMemoType = this.GetMemoType();
			this.strDateFormat = this.GetDateFormat();
			if (this.intMemoType == 2)
			{
				this.pnlTop.Visible = false;
				this.pnlBottom.Dock = DockStyle.Fill;
			}
			if (this.intMemoType != 2)
			{
				try
				{
					Point location = this.txtHypMemoUrl.Location;
					Point point = this.lblHypMemoNote.Location;
					this.txtDescription.Hide();
					this.lblDescription.Hide();
					this.txtHypMemoUrl.Location = this.txtDescription.Location;
					this.lblHypMemoUrl.Location = this.lblDescription.Location;
					this.lblHypMemoNote.Location = location;
					this.btnHypMemoOpen.Location = point;
					this.btnHypMemoOpen.Location = this.btnPrgMemoOpen.Location;
				}
				catch (Exception exception)
				{
				}
			}
			else
			{
				this.txtDescription.Show();
				this.lblDescription.Show();
			}
			this.GetSaveMemoValue();
			this.bMemoChanged = false;
			this.UpdateFont();
			this.lblTxtMemoDate.Text = string.Empty;
			this.lblRefMemoDate.Text = string.Empty;
			this.lblHypMemoDate.Text = string.Empty;
			this.lblPrgMemoDate.Text = string.Empty;
			this.LoadResources();
		}

		private void AddMemoToList(string type, string value)
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			empty = (value.Length <= 25 ? value : string.Concat(value.Substring(0, 25), "..."));
			if (empty.Contains("||"))
			{
				empty = empty.Replace("||", " ");
			}
			if (type.ToUpper().Equals("TXT"))
			{
				str = "Text";
			}
			else if (type.ToUpper().Equals("REF"))
			{
				str = "Reference";
			}
			else if (!type.ToUpper().Equals("HYP"))
			{
				str = (!type.ToUpper().Equals("PRG") ? "Unknown" : "Program");
			}
			else
			{
				str = "Hyperlink";
			}
			empty1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			DataGridViewCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
			{
				Value = empty
			};
			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
			{
				Value = str
			};
			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
			{
				Value = empty1
			};
			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			dataGridViewRow.Tag = this.CreateMemoNode(type, value, empty1).OuterXml;
			this.dgMemoList.SelectionChanged -= new EventHandler(this.dgMemoList_SelectionChanged);
			this.dgMemoList.Rows.Add(dataGridViewRow);
			this.dgMemoList.ClearSelection();
			this.dgMemoList.SelectionChanged += new EventHandler(this.dgMemoList_SelectionChanged);
			this.dgMemoList.FirstDisplayedScrollingRowIndex = this.dgMemoList.Rows.Count - 1;
			this.bMemoChanged = true;
		}

		private void AddMemoToList(XmlNode xNode)
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			if (xNode.Attributes["Value"] == null || !(xNode.Attributes["Value"].Value.Trim() != string.Empty))
			{
				return;
			}
			empty = (xNode.Attributes["Value"].Value.Trim().Length <= 25 ? xNode.Attributes["Value"].Value.Trim() : string.Concat(xNode.Attributes["Value"].Value.Trim().Substring(0, 25), "..."));
			if (xNode.Attributes["Type"] == null || !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "TXT") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "REF") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "HYP") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "PRG"))
			{
				return;
			}
			if (xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("TXT"))
			{
				str = "Text";
			}
			else if (xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("REF"))
			{
				str = "Reference";
			}
			else if (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("HYP"))
			{
				str = (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("PRG") ? "Unknown" : "Program");
			}
			else
			{
				str = "Hyperlink";
			}
			if (!str.ToUpper().Equals("TEXT"))
			{
				if (empty.Contains("||"))
				{
					empty = empty.Replace("||", " ");
				}
				if (empty.Contains("|"))
				{
					empty = empty.Replace("|", " ");
				}
			}
			if (xNode.Attributes["Update"] == null)
			{
				empty1 = "Unknown";
			}
			else if (xNode.Attributes["Update"].Value.Trim() == string.Empty)
			{
				empty1 = "Unknown";
			}
			else
			{
				empty1 = xNode.Attributes["Update"].Value.Trim();
				try
				{
					DateTime dateTime = DateTime.ParseExact(empty1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					string[] strArrays = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
					if (this.strDateFormat != string.Empty)
					{
						if (this.strDateFormat.ToUpper() != "INVALID")
						{
							string[] strArrays1 = strArrays;
							for (int i = 0; i < (int)strArrays1.Length; i++)
							{
								string str1 = strArrays1[i];
								if (this.strDateFormat == str1)
								{
									empty1 = dateTime.ToString(this.strDateFormat);
								}
							}
						}
						else
						{
							empty1 = "Unknown";
						}
					}
				}
				catch
				{
				}
			}
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			DataGridViewCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
			{
				Value = empty
			};
			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
			{
				Value = str
			};
			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
			{
				Value = empty1
			};
			dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
			dataGridViewRow.Tag = xNode.OuterXml;
			this.dgMemoList.Rows.Add(dataGridViewRow);
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.ShowDJVU(true, this.strDjVuPicPath);
			if (!this.lblHypMemoNote.InvokeRequired)
			{
				this.lblHypMemoNote.Hide();
				return;
			}
			this.lblHypMemoNote.Invoke(new MethodInvoker(() => this.lblHypMemoNote.Hide()));
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.Close();
		}

		private void btnHypMemoOpen_Click(object sender, EventArgs e)
		{
			if (this.txtHypMemoUrl.Text.Trim() != string.Empty)
			{
				try
				{
					string item = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
					if (!(item != string.Empty) || item == null)
					{
						Process process = Process.Start("IExplore.exe", this.txtHypMemoUrl.Text);
						if (process != null)
						{
							IntPtr handle = process.Handle;
							frmMemoListGlobal.SetForegroundWindow(process.Handle);
						}
					}
					else
					{
						RegistryReader registryReader = new RegistryReader();
						string str = registryReader.Read(string.Concat("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", item, ".exe"), string.Empty);
						if (str != null)
						{
							Process process1 = Process.Start(str, this.txtHypMemoUrl.Text);
							if (process1 != null)
							{
								IntPtr intPtr = process1.Handle;
								frmMemoListGlobal.SetForegroundWindow(process1.Handle);
							}
						}
						else
						{
							string str1 = registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty);
							Process process2 = Process.Start(str1, this.txtHypMemoUrl.Text);
							if (process2 != null)
							{
								IntPtr handle1 = process2.Handle;
								frmMemoListGlobal.SetForegroundWindow(process2.Handle);
							}
						}
					}
				}
				catch (Exception exception)
				{
					MessageBox.Show(this.GetResource("(E-MLC-EM007) Can not open Internet Explorer", "(E-MLC-EM007)_IE", ResourceType.POPUP_MESSAGE), this.frmParent.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.frmParent.Close();
		}

		private void btnPrgMemoExePathBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				if (!(this.txtPrgMemoExePath.Text.Trim() != string.Empty) || !Directory.Exists(Path.GetDirectoryName(this.txtPrgMemoExePath.Text)))
				{
					this.ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				}
				else
				{
					this.ofd.InitialDirectory = Path.GetDirectoryName(this.txtPrgMemoExePath.Text);
				}
				this.ofd.Filter = "Executable Files (*.exe)|*.exe";
				this.ofd.RestoreDirectory = false;
				if (this.ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					this.txtPrgMemoExePath.Text = this.ofd.FileName;
				}
			}
			catch
			{
			}
		}

		private void btnPrgMemoOpen_Click(object sender, EventArgs e)
		{
			try
			{
				if (File.Exists(this.txtPrgMemoExePath.Text) && this.txtPrgMemoExePath.Text.ToUpper().EndsWith(".EXE"))
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo()
					{
						FileName = this.txtPrgMemoExePath.Text,
						Arguments = this.txtPrgMemoCmdLine.Text,
						UseShellExecute = false
					};
					Process.Start(processStartInfo);
				}
				else if (this.txtPrgMemoExePath.Text.Trim() != string.Empty)
				{
					MessageBox.Show(this.GetResource("(E-MLC-EM008) Specified information does not exist", "(E-MLC-EM008)_INFORMATION", ResourceType.POPUP_MESSAGE), this.frmParent.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			catch
			{
				MessageBox.Show(this.GetResource("(E-MLC-EM009) Specified information does not exist", "(E-MLC-EM009)_INFORMATION", ResourceType.POPUP_MESSAGE), this.frmParent.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void btnRefMemoOpen_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			if (!this.txtRefMemoServerKey.Text.Trim().Equals(string.Empty) && !this.txtRefMemoBookId.Text.Trim().Equals(string.Empty))
			{
				string[] text = new string[] { this.txtRefMemoServerKey.Text, " ", this.txtRefMemoBookId.Text, " ", this.txtRefMemoOtherRef.Text };
				empty = string.Concat(text);
				this.frmParent.OpenBookFromString(empty);
			}
		}

		public void ClearItems(bool clearPanels, bool clearList, bool clearButtonCheck)
		{
			if (clearPanels)
			{
				this.lblTxtMemoDate.Text = string.Empty;
				this.rtbTxtMemo.Clear();
				this.lblRefMemoDate.Text = string.Empty;
				this.txtRefMemoServerKey.Clear();
				this.txtRefMemoBookId.Clear();
				this.txtRefMemoOtherRef.Clear();
				this.lblHypMemoDate.Text = string.Empty;
				this.txtHypMemoUrl.Clear();
				if (this.frmParent.objFrmTasks.intMemoType == 2)
				{
					this.pnlHypMemoPreview.Visible = false;
					this.picBoxHypPreview.Image = null;
					this.objDjVuCtlMemoListGlobal.SRC = null;
				}
				this.txtDescription.Text = string.Empty;
				this.lblPrgMemoDate.Text = string.Empty;
				this.txtPrgMemoExePath.Clear();
				this.txtPrgMemoCmdLine.Clear();
			}
			if (clearList)
			{
				this.dgMemoList.Rows.Clear();
			}
		}

		private XmlNode CreateMemoNode(string type, string value, string date)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodes = xmlDocument.CreateNode(XmlNodeType.Element, "Memo", null);
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("ServerKey");
			xmlAttribute.Value = this.frmParent.sServerKey;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("BookId");
			xmlAttribute.Value = this.frmParent.sBookId;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("PageId");
			xmlAttribute.Value = this.frmParent.sPageId;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("PicIndex");
			xmlAttribute.Value = this.frmParent.sPicIndex;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("ListIndex");
			xmlAttribute.Value = this.frmParent.sListIndex;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("PartNo");
			xmlAttribute.Value = this.frmParent.sPartNumber;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("Type");
			xmlAttribute.Value = type;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("Value");
			xmlAttribute.Value = value;
			xmlNodes.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("Update");
			xmlAttribute.Value = date;
			xmlNodes.Attributes.Append(xmlAttribute);
			return xmlNodes;
		}

		public XmlNode CreateMemoNodeHyperlink(string type, string value, string date, string HypDescription)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodes = xmlDocument.CreateNode(XmlNodeType.Element, "Memo", null);
			XmlAttribute hypDescription = xmlDocument.CreateAttribute("ServerKey");
			hypDescription.Value = this.frmParent.sServerKey;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("BookId");
			hypDescription.Value = this.frmParent.sBookId;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("PageId");
			hypDescription.Value = this.frmParent.sPageId;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("PicIndex");
			hypDescription.Value = this.frmParent.sPicIndex;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("ListIndex");
			hypDescription.Value = this.frmParent.sListIndex;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("PartNo");
			hypDescription.Value = this.frmParent.sPartNumber;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("Type");
			hypDescription.Value = type;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("Description");
			hypDescription.Value = HypDescription;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("Value");
			hypDescription.Value = value;
			xmlNodes.Attributes.Append(hypDescription);
			hypDescription = xmlDocument.CreateAttribute("Update");
			hypDescription.Value = date;
			xmlNodes.Attributes.Append(hypDescription);
			return xmlNodes;
		}

		private void dgMemoList_SelectionChanged(object sender, EventArgs e)
		{
			XmlDocument xmlDocument = new XmlDocument();
			if (this.dgMemoList.SelectedRows.Count <= 0)
			{
				this.ClearItems(true, false, false);
			}
			else
			{
				this.ClearItems(true, false, true);
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.dgMemoList.SelectedRows[0].Tag.ToString()));
					this.ShowMemoDetails(xmlDocument.ReadNode(xmlTextReader));
				}
				catch
				{
					this.ShowMemoDetails(null);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				this.objDjVuCtlMemoListGlobal.SRC = string.Empty;
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
				base.Dispose(disposing);
			}
			catch
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
				base.Dispose(disposing);
			}
		}

		private string DownloadPicture(string strPictureURL)
		{
			string empty;
			try
			{
				string folderPath = string.Empty;
				DateTime now = DateTime.Now;
				string str = now.ToLongTimeString().Replace(":", string.Empty);
				folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				folderPath = string.Concat(folderPath, "\\", Application.ProductName);
				folderPath = string.Concat(folderPath, "\\tmpMemoPreview");
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}
				folderPath = string.Concat(folderPath, "\\tmpImage_ImgPreview", str, ".djvu");
				(new Download()).DownloadFile(strPictureURL, folderPath);
				empty = folderPath;
			}
			catch (Exception exception)
			{
				empty = string.Empty;
			}
			return empty;
		}

		private bool DuplicateMemoNode(XmlNode xNode)
		{
			bool flag;
			try
			{
				if (xNode != null && xNode.Attributes.Count > 0)
				{
					foreach (DataGridViewRow row in (IEnumerable)this.dgMemoList.Rows)
					{
						string str = row.Tag.ToString();
						if (row.Tag.ToString().Contains("^"))
						{
							string str1 = row.Tag.ToString();
							char[] chrArray = new char[] { '\u005E' };
							str = str1.Split(chrArray)[0];
						}
						if (str == string.Empty)
						{
							continue;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlNode xmlNodes = null;
						xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(str)));
						if (!(xNode.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["ServerKey"].Value.ToString().Trim().ToUpper()) || !(xNode.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["BookId"].Value.ToString().Trim().ToUpper()) || !(xNode.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["PartNo"].Value.ToString().Trim().ToUpper()) || !(xNode.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["Type"].Value.ToString().Trim().ToUpper()) || !(xNode.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["Value"].Value.ToString().Trim().ToUpper()) || !(xNode.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["Update"].Value.ToString().Trim().ToUpper()))
						{
							continue;
						}
						flag = true;
						return flag;
					}
				}
				flag = false;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		private void frmMemoGlobal_Load(object sender, EventArgs e)
		{
			this.frmParent.frmParent.LoadMemos();
		}

		private void frmMemoListGlobal_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.lstExportedGlobalMemoListPictures.Count > 0)
			{
				for (int i = 0; i < this.lstExportedGlobalMemoListPictures.Count; i++)
				{
					if (File.Exists(this.lstExportedGlobalMemoListPictures[i]))
					{
						File.Delete(this.lstExportedGlobalMemoListPictures[i]);
					}
				}
			}
		}

		private string GetDateFormat()
		{
			string empty;
			try
			{
				string str = string.Empty;
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"] != null)
				{
					if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() != "HIDDEN")
					{
						str = Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString();
					}
					else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() == "HIDDEN")
					{
						str = "INVALID";
					}
				}
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"] != null && Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString().ToUpper() != "HIDDEN")
				{
					str = (str.ToUpper() != "INVALID" ? string.Concat(str, " ", Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString()) : Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString());
				}
				this.strDateFormat = str;
				empty = this.strDateFormat;
			}
			catch (Exception exception)
			{
				empty = string.Empty;
			}
			return empty;
		}

		private int GetDJVULoadTime()
		{
			int num;
			int num1 = 1;
			try
			{
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "DJVULOADTIME"] != null)
				{
					string str = Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "DJVULOADTIME"].ToString();
					num1 = int.Parse(str);
					if (num1 == 0)
					{
						num1 = 1;
					}
				}
				num = num1;
			}
			catch (Exception exception)
			{
				num1 = 1;
				num = num1;
			}
			return num;
		}

		private string GetFileExtension(string strURL)
		{
			string empty = string.Empty;
			try
			{
				empty = Path.GetExtension((new Uri(strURL)).LocalPath);
				empty = empty.TrimStart(new char[] { '.' });
			}
			catch (Exception exception)
			{
			}
			return empty;
		}

		private int GetMemoType()
		{
			int num;
			try
			{
				num = (Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"] == null || !(Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"].ToString() == "2") ? 1 : 2);
			}
			catch (Exception exception)
			{
				num = 1;
			}
			return num;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MEMO_LIST']");
				str = string.Concat(str, "/Screen[@Name='MEMOLIST_GLOBAL']");
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

		private void GetSaveMemoValue()
		{
			try
			{
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"] != null && Program.iniServers[this.frmParent.frmParent.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"].ToString().ToUpper() == "ON")
				{
					this.bSaveMemoOnBookLevel = true;
				}
			}
			catch (Exception exception)
			{
				this.bSaveMemoOnBookLevel = false;
			}
		}

		private void goToPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.dgMemoList.SelectedRows[0].Tag.ToString()));
					XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
					if (!Settings.Default.OpenInCurrentInstance)
					{
						string[] value = new string[] { "-o", xmlNodes.Attributes["ServerKey"].Value, xmlNodes.Attributes["BookId"].Value, xmlNodes.Attributes["PageId"].Value, xmlNodes.Attributes["PicIndex"].Value };
						string[] strArrays = value;
						if (Global.SecurityLocksOpen(this.frmParent.frmParent.GetBookNode(xmlNodes.Attributes["BookId"].Value, this.frmParent.frmParent.p_ServerId), this.frmParent.frmParent.SchemaNode, this.frmParent.frmParent.ServerId, this.frmParent.frmParent))
						{
							this.frmParent.frmParent.BookJump(strArrays);
						}
					}
					else
					{
						this.frmParent.OpenParentPage(xmlNodes.Attributes["ServerKey"].Value, xmlNodes.Attributes["BookId"].Value, xmlNodes.Attributes["PageId"].Value, xmlNodes.Attributes["PicIndex"].Value, xmlNodes.Attributes["ListIndex"].Value, xmlNodes.Attributes["PartNo"].Value);
						this.frmParent.Close();
					}
				}
				catch
				{
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmMemoListGlobal));
			this.objDjVuCtlMemoListGlobal = new AxDjVuCtrl();
			this.pnlControl = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlForm = new Panel();
			this.pnlTop = new Panel();
			this.pnlGrid = new Panel();
			this.dgMemoList = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.Column2 = new DataGridViewTextBoxColumn();
			this.Column3 = new DataGridViewTextBoxColumn();
			this.cmsGlobalMemo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.goToPageToolStripMenuItem = new ToolStripMenuItem();
			this.pnlBottom = new Panel();
			this.pnlMemos = new Panel();
			this.pnlTxtMemo = new Panel();
			this.pnlTxtMemoContents = new Panel();
			this.pnlRtbTxtMemo = new Panel();
			this.rtbTxtMemo = new RichTextBox();
			this.pnlTxtMemoTop = new Panel();
			this.lblTxtMemoDate = new Label();
			this.lblTxtMemoTitle = new Label();
			this.pnlRefMemo = new Panel();
			this.pnlRefMemoContents = new Panel();
			this.btnRefMemoOpen = new Button();
			this.lblRefMemoNote = new Label();
			this.lblRefMemoOtherRef = new Label();
			this.lblRefMemoBookId = new Label();
			this.txtRefMemoOtherRef = new TextBox();
			this.lblRefMemoServerKey = new Label();
			this.txtRefMemoBookId = new TextBox();
			this.txtRefMemoServerKey = new TextBox();
			this.pnlRefMemoTop = new Panel();
			this.lblRefMemoDate = new Label();
			this.lblRefMemoTitle = new Label();
			this.pnlPrgMemo = new Panel();
			this.pnlPrgMemoContents = new Panel();
			this.btnPrgMemoExePathBrowse = new Button();
			this.btnPrgMemoOpen = new Button();
			this.lblPrgMemoCmdLine = new Label();
			this.txtPrgMemoCmdLine = new TextBox();
			this.lblPrgMemoExePath = new Label();
			this.txtPrgMemoExePath = new TextBox();
			this.pnlPrgMemoTop = new Panel();
			this.lblPrgMemoDate = new Label();
			this.lblPrgMemoTitle = new Label();
			this.pnlError = new Panel();
			this.lblError = new Label();
			this.pnlHypMemo = new Panel();
			this.pnlHypMemoPreview = new Panel();
			this.picBoxHypPreview = new PictureBox();
			this.pnlHypMemoContents = new Panel();
			this.lblDescription = new Label();
			this.txtDescription = new TextBox();
			this.btnHypMemoOpen = new Button();
			this.lblHypMemoNote = new Label();
			this.lblHypMemoUrl = new Label();
			this.txtHypMemoUrl = new TextBox();
			this.pnlHypMemoTop = new Panel();
			this.lblHypMemoDate = new Label();
			this.lblHypMemoTitle = new Label();
			this.pnlSplitter2 = new Panel();
			this.pnlSplitter1 = new Panel();
			this.pnlOptions = new Panel();
			this.lblGlobalMemo = new Label();
			this.ofd = new OpenFileDialog();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.objDjVuCtlMemoListGlobal).BeginInit();
			this.pnlControl.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.pnlTop.SuspendLayout();
			this.pnlGrid.SuspendLayout();
			((ISupportInitialize)this.dgMemoList).BeginInit();
			this.cmsGlobalMemo.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			this.pnlMemos.SuspendLayout();
			this.pnlTxtMemo.SuspendLayout();
			this.pnlTxtMemoContents.SuspendLayout();
			this.pnlRtbTxtMemo.SuspendLayout();
			this.pnlTxtMemoTop.SuspendLayout();
			this.pnlRefMemo.SuspendLayout();
			this.pnlRefMemoContents.SuspendLayout();
			this.pnlRefMemoTop.SuspendLayout();
			this.pnlPrgMemo.SuspendLayout();
			this.pnlPrgMemoContents.SuspendLayout();
			this.pnlPrgMemoTop.SuspendLayout();
			this.pnlError.SuspendLayout();
			this.pnlHypMemo.SuspendLayout();
			this.pnlHypMemoPreview.SuspendLayout();
			((ISupportInitialize)this.picBoxHypPreview).BeginInit();
			this.pnlHypMemoContents.SuspendLayout();
			this.pnlHypMemoTop.SuspendLayout();
			this.pnlOptions.SuspendLayout();
			base.SuspendLayout();
			this.objDjVuCtlMemoListGlobal.Dock = DockStyle.Fill;
			this.objDjVuCtlMemoListGlobal.Enabled = true;
			this.objDjVuCtlMemoListGlobal.Location = new Point(0, 0);
			this.objDjVuCtlMemoListGlobal.Name = "objDjVuCtlMemoListGlobal";
			this.objDjVuCtlMemoListGlobal.OcxState = (AxHost.State)componentResourceManager.GetObject("objDjVuCtlMemoListGlobal.OcxState");
			this.objDjVuCtlMemoListGlobal.Size = new System.Drawing.Size(406, 0);
			this.objDjVuCtlMemoListGlobal.TabIndex = 0;
			this.objDjVuCtlMemoListGlobal.PageChange += new _DDjVuCtrlEvents_PageChangeEventHandler(this.objDjVuCtlMemoListGlobal_PageChange);
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 317);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 4, 22, 4);
			this.pnlControl.Size = new System.Drawing.Size(448, 31);
			this.pnlControl.TabIndex = 0;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(276, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(351, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlTop);
			this.pnlForm.Controls.Add(this.pnlBottom);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Controls.Add(this.pnlOptions);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(450, 350);
			this.pnlForm.TabIndex = 0;
			this.pnlTop.Controls.Add(this.pnlGrid);
			this.pnlTop.Dock = DockStyle.Fill;
			this.pnlTop.Location = new Point(0, 33);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
			this.pnlTop.Size = new System.Drawing.Size(448, 127);
			this.pnlTop.TabIndex = 0;
			this.pnlGrid.BorderStyle = BorderStyle.FixedSingle;
			this.pnlGrid.Controls.Add(this.dgMemoList);
			this.pnlGrid.Dock = DockStyle.Fill;
			this.pnlGrid.Location = new Point(10, 10);
			this.pnlGrid.Name = "pnlGrid";
			this.pnlGrid.Size = new System.Drawing.Size(428, 117);
			this.pnlGrid.TabIndex = 0;
			this.dgMemoList.AllowUserToAddRows = false;
			this.dgMemoList.AllowUserToDeleteRows = false;
			this.dgMemoList.AllowUserToResizeRows = false;
			this.dgMemoList.BackgroundColor = Color.White;
			this.dgMemoList.BorderStyle = BorderStyle.None;
			this.dgMemoList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			DataGridViewColumnCollection columns = this.dgMemoList.Columns;
			DataGridViewColumn[] column1 = new DataGridViewColumn[] { this.Column1, this.Column2, this.Column3 };
			columns.AddRange(column1);
			this.dgMemoList.ContextMenuStrip = this.cmsGlobalMemo;
			this.dgMemoList.Dock = DockStyle.Fill;
			this.dgMemoList.EditMode = DataGridViewEditMode.EditProgrammatically;
			this.dgMemoList.Location = new Point(0, 0);
			this.dgMemoList.MultiSelect = false;
			this.dgMemoList.Name = "dgMemoList";
			this.dgMemoList.RowHeadersVisible = false;
			this.dgMemoList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgMemoList.Size = new System.Drawing.Size(426, 115);
			this.dgMemoList.TabIndex = 0;
			this.dgMemoList.TabStop = false;
			this.dgMemoList.SelectionChanged += new EventHandler(this.dgMemoList_SelectionChanged);
			this.Column1.HeaderText = "Description";
			this.Column1.Name = "Column1";
			this.Column1.Width = 178;
			this.Column2.HeaderText = "Type";
			this.Column2.Name = "Column2";
			this.Column2.Width = 90;
			this.Column3.HeaderText = "UpdateDate";
			this.Column3.Name = "Column3";
			this.Column3.Width = 140;
			this.cmsGlobalMemo.Items.AddRange(new ToolStripItem[] { this.goToPageToolStripMenuItem });
			this.cmsGlobalMemo.Name = "cmsAllMemo";
			this.cmsGlobalMemo.Size = new System.Drawing.Size(136, 26);
			this.goToPageToolStripMenuItem.Name = "goToPageToolStripMenuItem";
			this.goToPageToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
			this.goToPageToolStripMenuItem.Text = "Go To Page";
			this.goToPageToolStripMenuItem.Click += new EventHandler(this.goToPageToolStripMenuItem_Click);
			this.pnlBottom.Controls.Add(this.pnlMemos);
			this.pnlBottom.Controls.Add(this.pnlSplitter2);
			this.pnlBottom.Controls.Add(this.pnlSplitter1);
			this.pnlBottom.Dock = DockStyle.Bottom;
			this.pnlBottom.Location = new Point(0, 160);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.pnlBottom.Size = new System.Drawing.Size(448, 157);
			this.pnlBottom.TabIndex = 0;
			this.pnlMemos.Controls.Add(this.pnlTxtMemo);
			this.pnlMemos.Controls.Add(this.pnlRefMemo);
			this.pnlMemos.Controls.Add(this.pnlPrgMemo);
			this.pnlMemos.Controls.Add(this.pnlError);
			this.pnlMemos.Controls.Add(this.pnlHypMemo);
			this.pnlMemos.Dock = DockStyle.Fill;
			this.pnlMemos.Location = new Point(10, 8);
			this.pnlMemos.Name = "pnlMemos";
			this.pnlMemos.Size = new System.Drawing.Size(428, 149);
			this.pnlMemos.TabIndex = 0;
			this.pnlTxtMemo.BorderStyle = BorderStyle.FixedSingle;
			this.pnlTxtMemo.Controls.Add(this.pnlTxtMemoContents);
			this.pnlTxtMemo.Controls.Add(this.pnlTxtMemoTop);
			this.pnlTxtMemo.Dock = DockStyle.Fill;
			this.pnlTxtMemo.Location = new Point(0, 0);
			this.pnlTxtMemo.Name = "pnlTxtMemo";
			this.pnlTxtMemo.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.pnlTxtMemo.Size = new System.Drawing.Size(428, 149);
			this.pnlTxtMemo.TabIndex = 0;
			this.pnlTxtMemoContents.Controls.Add(this.pnlRtbTxtMemo);
			this.pnlTxtMemoContents.Dock = DockStyle.Fill;
			this.pnlTxtMemoContents.Location = new Point(10, 26);
			this.pnlTxtMemoContents.Name = "pnlTxtMemoContents";
			this.pnlTxtMemoContents.Padding = new System.Windows.Forms.Padding(2, 6, 2, 5);
			this.pnlTxtMemoContents.Size = new System.Drawing.Size(406, 111);
			this.pnlTxtMemoContents.TabIndex = 0;
			this.pnlRtbTxtMemo.BorderStyle = BorderStyle.FixedSingle;
			this.pnlRtbTxtMemo.Controls.Add(this.rtbTxtMemo);
			this.pnlRtbTxtMemo.Dock = DockStyle.Fill;
			this.pnlRtbTxtMemo.Location = new Point(2, 6);
			this.pnlRtbTxtMemo.Name = "pnlRtbTxtMemo";
			this.pnlRtbTxtMemo.Size = new System.Drawing.Size(402, 100);
			this.pnlRtbTxtMemo.TabIndex = 0;
			this.rtbTxtMemo.BackColor = SystemColors.Window;
			this.rtbTxtMemo.BorderStyle = BorderStyle.None;
			this.rtbTxtMemo.Dock = DockStyle.Fill;
			this.rtbTxtMemo.Location = new Point(0, 0);
			this.rtbTxtMemo.Name = "rtbTxtMemo";
			this.rtbTxtMemo.ReadOnly = true;
			this.rtbTxtMemo.Size = new System.Drawing.Size(400, 98);
			this.rtbTxtMemo.TabIndex = 0;
			this.rtbTxtMemo.TabStop = false;
			this.pnlTxtMemoTop.Controls.Add(this.lblTxtMemoDate);
			this.pnlTxtMemoTop.Controls.Add(this.lblTxtMemoTitle);
			this.pnlTxtMemoTop.Dock = DockStyle.Top;
			this.pnlTxtMemoTop.Location = new Point(10, 0);
			this.pnlTxtMemoTop.Name = "pnlTxtMemoTop";
			this.pnlTxtMemoTop.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
			this.pnlTxtMemoTop.Size = new System.Drawing.Size(406, 26);
			this.pnlTxtMemoTop.TabIndex = 0;
			this.lblTxtMemoDate.Dock = DockStyle.Fill;
			this.lblTxtMemoDate.Location = new Point(143, 0);
			this.lblTxtMemoDate.Name = "lblTxtMemoDate";
			this.lblTxtMemoDate.Size = new System.Drawing.Size(263, 21);
			this.lblTxtMemoDate.TabIndex = 0;
			this.lblTxtMemoDate.Text = "Updated on: 14/02/2010 21:26";
			this.lblTxtMemoDate.TextAlign = ContentAlignment.MiddleRight;
			this.lblTxtMemoTitle.Dock = DockStyle.Left;
			this.lblTxtMemoTitle.Location = new Point(0, 0);
			this.lblTxtMemoTitle.Name = "lblTxtMemoTitle";
			this.lblTxtMemoTitle.Size = new System.Drawing.Size(143, 21);
			this.lblTxtMemoTitle.TabIndex = 0;
			this.lblTxtMemoTitle.Text = "Text Memo";
			this.lblTxtMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlRefMemo.BorderStyle = BorderStyle.FixedSingle;
			this.pnlRefMemo.Controls.Add(this.pnlRefMemoContents);
			this.pnlRefMemo.Controls.Add(this.pnlRefMemoTop);
			this.pnlRefMemo.Dock = DockStyle.Fill;
			this.pnlRefMemo.Location = new Point(0, 0);
			this.pnlRefMemo.Name = "pnlRefMemo";
			this.pnlRefMemo.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.pnlRefMemo.Size = new System.Drawing.Size(428, 149);
			this.pnlRefMemo.TabIndex = 0;
			this.pnlRefMemoContents.Controls.Add(this.btnRefMemoOpen);
			this.pnlRefMemoContents.Controls.Add(this.lblRefMemoNote);
			this.pnlRefMemoContents.Controls.Add(this.lblRefMemoOtherRef);
			this.pnlRefMemoContents.Controls.Add(this.lblRefMemoBookId);
			this.pnlRefMemoContents.Controls.Add(this.txtRefMemoOtherRef);
			this.pnlRefMemoContents.Controls.Add(this.lblRefMemoServerKey);
			this.pnlRefMemoContents.Controls.Add(this.txtRefMemoBookId);
			this.pnlRefMemoContents.Controls.Add(this.txtRefMemoServerKey);
			this.pnlRefMemoContents.Dock = DockStyle.Fill;
			this.pnlRefMemoContents.Location = new Point(10, 21);
			this.pnlRefMemoContents.Name = "pnlRefMemoContents";
			this.pnlRefMemoContents.Size = new System.Drawing.Size(406, 116);
			this.pnlRefMemoContents.TabIndex = 0;
			this.btnRefMemoOpen.Location = new Point(329, 70);
			this.btnRefMemoOpen.Name = "btnRefMemoOpen";
			this.btnRefMemoOpen.Size = new System.Drawing.Size(75, 23);
			this.btnRefMemoOpen.TabIndex = 0;
			this.btnRefMemoOpen.TabStop = false;
			this.btnRefMemoOpen.Text = "Go";
			this.btnRefMemoOpen.UseVisualStyleBackColor = true;
			this.btnRefMemoOpen.Click += new EventHandler(this.btnRefMemoOpen_Click);
			this.lblRefMemoNote.AutoSize = true;
			this.lblRefMemoNote.Location = new Point(6, 75);
			this.lblRefMemoNote.Name = "lblRefMemoNote";
			this.lblRefMemoNote.Size = new System.Drawing.Size(129, 13);
			this.lblRefMemoNote.TabIndex = 0;
			this.lblRefMemoNote.Text = "(space separated values)";
			this.lblRefMemoOtherRef.AutoSize = true;
			this.lblRefMemoOtherRef.Location = new Point(281, 13);
			this.lblRefMemoOtherRef.Name = "lblRefMemoOtherRef";
			this.lblRefMemoOtherRef.Size = new System.Drawing.Size(55, 13);
			this.lblRefMemoOtherRef.TabIndex = 0;
			this.lblRefMemoOtherRef.Text = "Other Ref";
			this.lblRefMemoBookId.AutoSize = true;
			this.lblRefMemoBookId.Location = new Point(143, 13);
			this.lblRefMemoBookId.Name = "lblRefMemoBookId";
			this.lblRefMemoBookId.Size = new System.Drawing.Size(93, 13);
			this.lblRefMemoBookId.TabIndex = 0;
			this.lblRefMemoBookId.Text = "Book Publishing Id";
			this.txtRefMemoOtherRef.BorderStyle = BorderStyle.FixedSingle;
			this.txtRefMemoOtherRef.Location = new Point(281, 30);
			this.txtRefMemoOtherRef.Name = "txtRefMemoOtherRef";
			this.txtRefMemoOtherRef.Size = new System.Drawing.Size(123, 21);
			this.txtRefMemoOtherRef.TabIndex = 0;
			this.txtRefMemoOtherRef.TabStop = false;
			this.lblRefMemoServerKey.AutoSize = true;
			this.lblRefMemoServerKey.Location = new Point(6, 13);
			this.lblRefMemoServerKey.Name = "lblRefMemoServerKey";
			this.lblRefMemoServerKey.Size = new System.Drawing.Size(60, 13);
			this.lblRefMemoServerKey.TabIndex = 0;
			this.lblRefMemoServerKey.Text = "Server Key";
			this.txtRefMemoBookId.BorderStyle = BorderStyle.FixedSingle;
			this.txtRefMemoBookId.Location = new Point(143, 30);
			this.txtRefMemoBookId.Name = "txtRefMemoBookId";
			this.txtRefMemoBookId.Size = new System.Drawing.Size(123, 21);
			this.txtRefMemoBookId.TabIndex = 0;
			this.txtRefMemoBookId.TabStop = false;
			this.txtRefMemoServerKey.BorderStyle = BorderStyle.FixedSingle;
			this.txtRefMemoServerKey.Location = new Point(6, 30);
			this.txtRefMemoServerKey.Name = "txtRefMemoServerKey";
			this.txtRefMemoServerKey.Size = new System.Drawing.Size(123, 21);
			this.txtRefMemoServerKey.TabIndex = 0;
			this.txtRefMemoServerKey.TabStop = false;
			this.pnlRefMemoTop.Controls.Add(this.lblRefMemoDate);
			this.pnlRefMemoTop.Controls.Add(this.lblRefMemoTitle);
			this.pnlRefMemoTop.Dock = DockStyle.Top;
			this.pnlRefMemoTop.Location = new Point(10, 0);
			this.pnlRefMemoTop.Name = "pnlRefMemoTop";
			this.pnlRefMemoTop.Size = new System.Drawing.Size(406, 21);
			this.pnlRefMemoTop.TabIndex = 0;
			this.lblRefMemoDate.Dock = DockStyle.Fill;
			this.lblRefMemoDate.Location = new Point(165, 0);
			this.lblRefMemoDate.Name = "lblRefMemoDate";
			this.lblRefMemoDate.Size = new System.Drawing.Size(241, 21);
			this.lblRefMemoDate.TabIndex = 0;
			this.lblRefMemoDate.Text = "Updated on: 14/02/2010 21:26";
			this.lblRefMemoDate.TextAlign = ContentAlignment.MiddleRight;
			this.lblRefMemoTitle.Dock = DockStyle.Left;
			this.lblRefMemoTitle.Location = new Point(0, 0);
			this.lblRefMemoTitle.Name = "lblRefMemoTitle";
			this.lblRefMemoTitle.Size = new System.Drawing.Size(165, 21);
			this.lblRefMemoTitle.TabIndex = 0;
			this.lblRefMemoTitle.Text = "Reference Memo";
			this.lblRefMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlPrgMemo.BorderStyle = BorderStyle.FixedSingle;
			this.pnlPrgMemo.Controls.Add(this.pnlPrgMemoContents);
			this.pnlPrgMemo.Controls.Add(this.pnlPrgMemoTop);
			this.pnlPrgMemo.Dock = DockStyle.Fill;
			this.pnlPrgMemo.Location = new Point(0, 0);
			this.pnlPrgMemo.Name = "pnlPrgMemo";
			this.pnlPrgMemo.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.pnlPrgMemo.Size = new System.Drawing.Size(428, 149);
			this.pnlPrgMemo.TabIndex = 0;
			this.pnlPrgMemoContents.Controls.Add(this.btnPrgMemoExePathBrowse);
			this.pnlPrgMemoContents.Controls.Add(this.btnPrgMemoOpen);
			this.pnlPrgMemoContents.Controls.Add(this.lblPrgMemoCmdLine);
			this.pnlPrgMemoContents.Controls.Add(this.txtPrgMemoCmdLine);
			this.pnlPrgMemoContents.Controls.Add(this.lblPrgMemoExePath);
			this.pnlPrgMemoContents.Controls.Add(this.txtPrgMemoExePath);
			this.pnlPrgMemoContents.Dock = DockStyle.Fill;
			this.pnlPrgMemoContents.Location = new Point(10, 21);
			this.pnlPrgMemoContents.Name = "pnlPrgMemoContents";
			this.pnlPrgMemoContents.Size = new System.Drawing.Size(406, 116);
			this.pnlPrgMemoContents.TabIndex = 0;
			this.btnPrgMemoExePathBrowse.Location = new Point(329, 10);
			this.btnPrgMemoExePathBrowse.Name = "btnPrgMemoExePathBrowse";
			this.btnPrgMemoExePathBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnPrgMemoExePathBrowse.TabIndex = 0;
			this.btnPrgMemoExePathBrowse.TabStop = false;
			this.btnPrgMemoExePathBrowse.Text = "Browse";
			this.btnPrgMemoExePathBrowse.UseVisualStyleBackColor = true;
			this.btnPrgMemoExePathBrowse.Click += new EventHandler(this.btnPrgMemoExePathBrowse_Click);
			this.btnPrgMemoOpen.Location = new Point(329, 70);
			this.btnPrgMemoOpen.Name = "btnPrgMemoOpen";
			this.btnPrgMemoOpen.Size = new System.Drawing.Size(75, 23);
			this.btnPrgMemoOpen.TabIndex = 0;
			this.btnPrgMemoOpen.TabStop = false;
			this.btnPrgMemoOpen.Text = "Go";
			this.btnPrgMemoOpen.UseVisualStyleBackColor = true;
			this.btnPrgMemoOpen.Click += new EventHandler(this.btnPrgMemoOpen_Click);
			this.lblPrgMemoCmdLine.AutoSize = true;
			this.lblPrgMemoCmdLine.Location = new Point(-1, 45);
			this.lblPrgMemoCmdLine.Name = "lblPrgMemoCmdLine";
			this.lblPrgMemoCmdLine.Size = new System.Drawing.Size(76, 13);
			this.lblPrgMemoCmdLine.TabIndex = 0;
			this.lblPrgMemoCmdLine.Text = "Command Line";
			this.txtPrgMemoCmdLine.BorderStyle = BorderStyle.FixedSingle;
			this.txtPrgMemoCmdLine.Location = new Point(90, 43);
			this.txtPrgMemoCmdLine.Name = "txtPrgMemoCmdLine";
			this.txtPrgMemoCmdLine.Size = new System.Drawing.Size(314, 21);
			this.txtPrgMemoCmdLine.TabIndex = 0;
			this.txtPrgMemoCmdLine.TabStop = false;
			this.lblPrgMemoExePath.AutoSize = true;
			this.lblPrgMemoExePath.Location = new Point(-1, 15);
			this.lblPrgMemoExePath.Name = "lblPrgMemoExePath";
			this.lblPrgMemoExePath.Size = new System.Drawing.Size(85, 13);
			this.lblPrgMemoExePath.TabIndex = 0;
			this.lblPrgMemoExePath.Text = "Executable Path";
			this.txtPrgMemoExePath.BorderStyle = BorderStyle.FixedSingle;
			this.txtPrgMemoExePath.Location = new Point(90, 11);
			this.txtPrgMemoExePath.Name = "txtPrgMemoExePath";
			this.txtPrgMemoExePath.Size = new System.Drawing.Size(233, 21);
			this.txtPrgMemoExePath.TabIndex = 0;
			this.txtPrgMemoExePath.TabStop = false;
			this.pnlPrgMemoTop.Controls.Add(this.lblPrgMemoDate);
			this.pnlPrgMemoTop.Controls.Add(this.lblPrgMemoTitle);
			this.pnlPrgMemoTop.Dock = DockStyle.Top;
			this.pnlPrgMemoTop.Location = new Point(10, 0);
			this.pnlPrgMemoTop.Name = "pnlPrgMemoTop";
			this.pnlPrgMemoTop.Size = new System.Drawing.Size(406, 21);
			this.pnlPrgMemoTop.TabIndex = 0;
			this.lblPrgMemoDate.Dock = DockStyle.Fill;
			this.lblPrgMemoDate.Location = new Point(165, 0);
			this.lblPrgMemoDate.Name = "lblPrgMemoDate";
			this.lblPrgMemoDate.Size = new System.Drawing.Size(241, 21);
			this.lblPrgMemoDate.TabIndex = 0;
			this.lblPrgMemoDate.Text = "Updated on: 14/02/2010 21:26";
			this.lblPrgMemoDate.TextAlign = ContentAlignment.MiddleRight;
			this.lblPrgMemoTitle.Dock = DockStyle.Left;
			this.lblPrgMemoTitle.Location = new Point(0, 0);
			this.lblPrgMemoTitle.Name = "lblPrgMemoTitle";
			this.lblPrgMemoTitle.Size = new System.Drawing.Size(165, 21);
			this.lblPrgMemoTitle.TabIndex = 0;
			this.lblPrgMemoTitle.Text = "Program Memo";
			this.lblPrgMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlError.BorderStyle = BorderStyle.FixedSingle;
			this.pnlError.Controls.Add(this.lblError);
			this.pnlError.Dock = DockStyle.Fill;
			this.pnlError.Location = new Point(0, 0);
			this.pnlError.Name = "pnlError";
			this.pnlError.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.pnlError.Size = new System.Drawing.Size(428, 149);
			this.pnlError.TabIndex = 0;
			this.lblError.Dock = DockStyle.Fill;
			this.lblError.Location = new Point(10, 0);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(406, 137);
			this.lblError.TabIndex = 0;
			this.lblError.Text = "Memo is not in valid format. Details cannot be shown.";
			this.lblError.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlHypMemo.BorderStyle = BorderStyle.FixedSingle;
			this.pnlHypMemo.Controls.Add(this.pnlHypMemoPreview);
			this.pnlHypMemo.Controls.Add(this.pnlHypMemoContents);
			this.pnlHypMemo.Controls.Add(this.pnlHypMemoTop);
			this.pnlHypMemo.Dock = DockStyle.Fill;
			this.pnlHypMemo.Location = new Point(0, 0);
			this.pnlHypMemo.Name = "pnlHypMemo";
			this.pnlHypMemo.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
			this.pnlHypMemo.Size = new System.Drawing.Size(428, 149);
			this.pnlHypMemo.TabIndex = 0;
			this.pnlHypMemoPreview.AutoScroll = true;
			this.pnlHypMemoPreview.Controls.Add(this.picBoxHypPreview);
			this.pnlHypMemoPreview.Controls.Add(this.objDjVuCtlMemoListGlobal);
			this.pnlHypMemoPreview.Dock = DockStyle.Fill;
			this.pnlHypMemoPreview.Location = new Point(10, 156);
			this.pnlHypMemoPreview.Name = "pnlHypMemoPreview";
			this.pnlHypMemoPreview.Size = new System.Drawing.Size(406, 0);
			this.pnlHypMemoPreview.TabIndex = 0;
			this.pnlHypMemoPreview.Visible = false;
			this.picBoxHypPreview.Dock = DockStyle.Fill;
			this.picBoxHypPreview.InitialImage = Resources.Loading1;
			this.picBoxHypPreview.Location = new Point(0, 0);
			this.picBoxHypPreview.Name = "picBoxHypPreview";
			this.picBoxHypPreview.Size = new System.Drawing.Size(406, 0);
			this.picBoxHypPreview.TabIndex = 0;
			this.picBoxHypPreview.TabStop = false;
			this.picBoxHypPreview.LoadCompleted += new AsyncCompletedEventHandler(this.picBoxHypPreview_LoadCompleted);
			this.pnlHypMemoContents.Controls.Add(this.lblDescription);
			this.pnlHypMemoContents.Controls.Add(this.txtDescription);
			this.pnlHypMemoContents.Controls.Add(this.btnHypMemoOpen);
			this.pnlHypMemoContents.Controls.Add(this.lblHypMemoNote);
			this.pnlHypMemoContents.Controls.Add(this.lblHypMemoUrl);
			this.pnlHypMemoContents.Controls.Add(this.txtHypMemoUrl);
			this.pnlHypMemoContents.Dock = DockStyle.Top;
			this.pnlHypMemoContents.Location = new Point(10, 21);
			this.pnlHypMemoContents.Name = "pnlHypMemoContents";
			this.pnlHypMemoContents.Size = new System.Drawing.Size(406, 135);
			this.pnlHypMemoContents.TabIndex = 0;
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new Point(2, 9);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(60, 13);
			this.lblDescription.TabIndex = 0;
			this.lblDescription.Text = "Description";
			this.txtDescription.BorderStyle = BorderStyle.FixedSingle;
			this.txtDescription.Location = new Point(64, 6);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(339, 21);
			this.txtDescription.TabIndex = 0;
			this.txtDescription.TabStop = false;
			this.btnHypMemoOpen.Location = new Point(329, 99);
			this.btnHypMemoOpen.Name = "btnHypMemoOpen";
			this.btnHypMemoOpen.Size = new System.Drawing.Size(75, 23);
			this.btnHypMemoOpen.TabIndex = 0;
			this.btnHypMemoOpen.TabStop = false;
			this.btnHypMemoOpen.Text = "Go";
			this.btnHypMemoOpen.UseVisualStyleBackColor = true;
			this.btnHypMemoOpen.Click += new EventHandler(this.btnHypMemoOpen_Click);
			this.lblHypMemoNote.AutoSize = true;
			this.lblHypMemoNote.Location = new Point(76, 74);
			this.lblHypMemoNote.Name = "lblHypMemoNote";
			this.lblHypMemoNote.Size = new System.Drawing.Size(328, 13);
			this.lblHypMemoNote.TabIndex = 0;
			this.lblHypMemoNote.Text = "Provide the web page address (URL) in the above field to hyperlink";
			this.lblHypMemoUrl.AutoSize = true;
			this.lblHypMemoUrl.Location = new Point(0, 43);
			this.lblHypMemoUrl.Name = "lblHypMemoUrl";
			this.lblHypMemoUrl.Size = new System.Drawing.Size(26, 13);
			this.lblHypMemoUrl.TabIndex = 0;
			this.lblHypMemoUrl.Text = "URL";
			this.txtHypMemoUrl.BorderStyle = BorderStyle.FixedSingle;
			this.txtHypMemoUrl.Location = new Point(64, 40);
			this.txtHypMemoUrl.Name = "txtHypMemoUrl";
			this.txtHypMemoUrl.Size = new System.Drawing.Size(340, 21);
			this.txtHypMemoUrl.TabIndex = 0;
			this.txtHypMemoUrl.TabStop = false;
			this.txtHypMemoUrl.TextChanged += new EventHandler(this.txtHypMemoUrl_TextChanged);
			this.pnlHypMemoTop.Controls.Add(this.lblHypMemoDate);
			this.pnlHypMemoTop.Controls.Add(this.lblHypMemoTitle);
			this.pnlHypMemoTop.Dock = DockStyle.Top;
			this.pnlHypMemoTop.Location = new Point(10, 0);
			this.pnlHypMemoTop.Name = "pnlHypMemoTop";
			this.pnlHypMemoTop.Size = new System.Drawing.Size(406, 21);
			this.pnlHypMemoTop.TabIndex = 0;
			this.lblHypMemoDate.Dock = DockStyle.Fill;
			this.lblHypMemoDate.Location = new Point(165, 0);
			this.lblHypMemoDate.Name = "lblHypMemoDate";
			this.lblHypMemoDate.Size = new System.Drawing.Size(241, 21);
			this.lblHypMemoDate.TabIndex = 0;
			this.lblHypMemoDate.Text = "Updated on: 14/02/2010 21:26";
			this.lblHypMemoDate.TextAlign = ContentAlignment.MiddleRight;
			this.lblHypMemoTitle.Dock = DockStyle.Left;
			this.lblHypMemoTitle.Location = new Point(0, 0);
			this.lblHypMemoTitle.Name = "lblHypMemoTitle";
			this.lblHypMemoTitle.Size = new System.Drawing.Size(165, 21);
			this.lblHypMemoTitle.TabIndex = 0;
			this.lblHypMemoTitle.Text = "Hyperlink Memo";
			this.lblHypMemoTitle.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlSplitter2.Dock = DockStyle.Top;
			this.pnlSplitter2.Location = new Point(10, 4);
			this.pnlSplitter2.Name = "pnlSplitter2";
			this.pnlSplitter2.Size = new System.Drawing.Size(428, 4);
			this.pnlSplitter2.TabIndex = 0;
			this.pnlSplitter1.Dock = DockStyle.Top;
			this.pnlSplitter1.Location = new Point(10, 0);
			this.pnlSplitter1.Name = "pnlSplitter1";
			this.pnlSplitter1.Size = new System.Drawing.Size(428, 4);
			this.pnlSplitter1.TabIndex = 0;
			this.pnlOptions.BackColor = Color.White;
			this.pnlOptions.Controls.Add(this.lblGlobalMemo);
			this.pnlOptions.Dock = DockStyle.Top;
			this.pnlOptions.Location = new Point(0, 0);
			this.pnlOptions.Name = "pnlOptions";
			this.pnlOptions.Size = new System.Drawing.Size(448, 33);
			this.pnlOptions.TabIndex = 0;
			this.lblGlobalMemo.BackColor = Color.White;
			this.lblGlobalMemo.ForeColor = Color.Black;
			this.lblGlobalMemo.Location = new Point(0, 3);
			this.lblGlobalMemo.Name = "lblGlobalMemo";
			this.lblGlobalMemo.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblGlobalMemo.Size = new System.Drawing.Size(135, 27);
			this.lblGlobalMemo.TabIndex = 0;
			this.lblGlobalMemo.Text = "Global Memo";
			this.ofd.FileName = "openFileDialog1";
			this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Width = 150;
			this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Width = 150;
			this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.Width = 140;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(450, 350);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmMemoListGlobal";
			base.Load += new EventHandler(this.frmMemoGlobal_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmMemoListGlobal_FormClosing);
			((ISupportInitialize)this.objDjVuCtlMemoListGlobal).EndInit();
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnlTop.ResumeLayout(false);
			this.pnlGrid.ResumeLayout(false);
			((ISupportInitialize)this.dgMemoList).EndInit();
			this.cmsGlobalMemo.ResumeLayout(false);
			this.pnlBottom.ResumeLayout(false);
			this.pnlMemos.ResumeLayout(false);
			this.pnlTxtMemo.ResumeLayout(false);
			this.pnlTxtMemoContents.ResumeLayout(false);
			this.pnlRtbTxtMemo.ResumeLayout(false);
			this.pnlTxtMemoTop.ResumeLayout(false);
			this.pnlRefMemo.ResumeLayout(false);
			this.pnlRefMemoContents.ResumeLayout(false);
			this.pnlRefMemoContents.PerformLayout();
			this.pnlRefMemoTop.ResumeLayout(false);
			this.pnlPrgMemo.ResumeLayout(false);
			this.pnlPrgMemoContents.ResumeLayout(false);
			this.pnlPrgMemoContents.PerformLayout();
			this.pnlPrgMemoTop.ResumeLayout(false);
			this.pnlError.ResumeLayout(false);
			this.pnlHypMemo.ResumeLayout(false);
			this.pnlHypMemoPreview.ResumeLayout(false);
			((ISupportInitialize)this.picBoxHypPreview).EndInit();
			this.pnlHypMemoContents.ResumeLayout(false);
			this.pnlHypMemoContents.PerformLayout();
			this.pnlHypMemoTop.ResumeLayout(false);
			this.pnlOptions.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public void LoadMemos(bool bPictureMemos, bool bPartMemos)
		{
			try
			{
				this.dgMemoList.Rows.Clear();
				if (this.frmParent.xnlGlobalMemo != null && this.frmParent.xnlGlobalMemo.Count > 0)
				{
					foreach (XmlNode xmlNodes in this.frmParent.xnlGlobalMemo)
					{
						if (xmlNodes.Attributes["PartNo"] == null || !(xmlNodes.Attributes["PartNo"].Value != string.Empty))
						{
							if (!bPictureMemos)
							{
								continue;
							}
							this.AddMemoToList(xmlNodes);
						}
						else
						{
							if (!bPartMemos)
							{
								continue;
							}
							if (!this.bSaveMemoOnBookLevel || this.intMemoType == 2)
							{
								this.AddMemoToList(xmlNodes);
							}
							else
							{
								if (this.DuplicateMemoNode(xmlNodes))
								{
									continue;
								}
								this.AddMemoToList(xmlNodes);
							}
						}
					}
				}
				if (this.frmParent.frmParent.GetMemoSortType() != string.Empty)
				{
					if (this.frmParent.frmParent.GetMemoSortType().ToUpper() == "DESC")
					{
						this.dgMemoList.Sort(this.dgMemoList.Columns[2], ListSortDirection.Descending);
					}
					else if (this.frmParent.frmParent.GetMemoSortType().ToUpper() == "ASC")
					{
						this.dgMemoList.Sort(this.dgMemoList.Columns[2], ListSortDirection.Ascending);
					}
					if (this.dgMemoList.Rows.Count > 0)
					{
						this.dgMemoList.Rows[0].Selected = true;
					}
				}
			}
			catch
			{
				MessageBox.Show(this.GetResource("(E-MGL-EM001) Failed to load specified object", "(E-MLC-EM001)_FAILED", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void LoadResources()
		{
			this.lblGlobalMemo.Text = this.GetResource("Global Memo", "GLOBAL_MEMO", ResourceType.LABEL);
			this.dgMemoList.Columns[0].HeaderText = this.GetResource("Description", "DESCRIPTION", ResourceType.GRID_VIEW);
			this.dgMemoList.Columns[1].HeaderText = this.GetResource("Type", "TYPE", ResourceType.GRID_VIEW);
			this.dgMemoList.Columns[2].HeaderText = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.GRID_VIEW);
			this.lblPrgMemoTitle.Text = this.GetResource("Program Memo", "PROGRAM_MEMO", ResourceType.LABEL);
			this.lblPrgMemoDate.Text = this.GetResource("Updated ON:", "UPDATED_ON", ResourceType.LABEL);
			this.lblPrgMemoExePath.Text = this.GetResource("Executable Path", "EXECUTABLE_PATH", ResourceType.LABEL);
			this.lblPrgMemoCmdLine.Text = this.GetResource("Command Line", "COMMAND_LINE", ResourceType.LABEL);
			this.btnPrgMemoExePathBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.BUTTON);
			this.btnPrgMemoOpen.Text = this.GetResource("Go", "GO", ResourceType.BUTTON);
			this.btnOK.Text = this.GetResource("Ok", "OK", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.lblHypMemoTitle.Text = this.GetResource("Hyperlink Memo", "HYPERLINK_MEMO", ResourceType.LABEL);
			this.lblHypMemoDate.Text = this.GetResource("Updated On:", "UPDATED_ON_HYP", ResourceType.LABEL);
			this.lblHypMemoUrl.Text = this.GetResource("URL", "URL", ResourceType.LABEL);
			this.lblHypMemoNote.Text = this.GetResource("Provide the web page address (URL) in the above field to hyperlink", "PROVIDE_URL", ResourceType.LABEL);
			this.lblDescription.Text = this.GetResource("Description", "URL_DESCRIPTION", ResourceType.LABEL);
			if (this.intMemoType == 2)
			{
				this.btnHypMemoOpen.Text = this.GetResource("Browse", "BROWSE_HYP", ResourceType.BUTTON);
			}
			else
			{
				this.btnHypMemoOpen.Text = this.GetResource("GO", "GO_HYP", ResourceType.BUTTON);
			}
			this.lblRefMemoTitle.Text = this.GetResource("Reference Memo", "REFERENCE_MEMO", ResourceType.LABEL);
			this.lblRefMemoDate.Text = this.GetResource("Updated on:", "UPDATED_ON_REF", ResourceType.LABEL);
			this.lblRefMemoServerKey.Text = this.GetResource("Server Key", "SERVER_KEY", ResourceType.LABEL);
			this.lblRefMemoBookId.Text = this.GetResource("Book Publishing Id", "BOOK_ID", ResourceType.LABEL);
			this.lblRefMemoOtherRef.Text = this.GetResource("Other Ref", "OTHER_REF", ResourceType.LABEL);
			this.lblRefMemoNote.Text = this.GetResource("(space separated values)", "SPACE_SEPRATED", ResourceType.LABEL);
			this.btnRefMemoOpen.Text = this.GetResource("GO", "GO_REF", ResourceType.BUTTON);
			this.lblTxtMemoTitle.Text = this.GetResource("Text Memo", "TEXT_MEMO", ResourceType.LABEL);
			this.lblTxtMemoDate.Text = this.GetResource("Updated On:", "UPDATED_ON_TXT", ResourceType.LABEL);
			this.goToPageToolStripMenuItem.Text = this.GetResource("Go to page", "GO_TO_PAGE", ResourceType.TOOLSTRIP);
		}

		private bool MatchXmlAttribute(string attName, string attValue, string nodeXML)
		{
			bool flag;
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				XmlNode xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(nodeXML)));
				if (xmlNodes.Attributes[attName] == null)
				{
					flag = false;
				}
				else
				{
					flag = (xmlNodes.Attributes[attName].Value.Trim() != attValue.Trim() ? false : true);
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private void objDjVuCtlMemoListGlobal_PageChange(object sender, _DDjVuCtrlEvents_PageChangeEvent e)
		{
			try
			{
				this.bPageChanged = true;
			}
			catch (Exception exception)
			{
			}
		}

		private void picBoxHypPreview_LoadCompleted(object sender, AsyncCompletedEventArgs e)
		{
			try
			{
				if (e.Error != null)
				{
					this.picBoxHypPreview.Image = null;
					MessageBox.Show(e.Error.Message.ToString(), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				if (this.picBoxHypPreview.Image != null)
				{
					System.Drawing.Size size = this.picBoxHypPreview.Image.Size;
					this.pnlHypMemoPreview.AutoScrollMinSize = size;
				}
			}
			catch (Exception exception)
			{
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		public void SetTabProperty(string strMemoType)
		{
			if (strMemoType == "TEXT")
			{
				this.rtbTxtMemo.TabStop = true;
				this.rtbTxtMemo.TabIndex = 1;
				this.txtRefMemoServerKey.TabStop = false;
				this.txtRefMemoBookId.TabStop = false;
				this.txtRefMemoOtherRef.TabStop = false;
				this.btnRefMemoOpen.TabStop = false;
				this.txtDescription.TabStop = false;
				this.txtHypMemoUrl.TabStop = false;
				this.btnHypMemoOpen.TabStop = false;
				this.txtPrgMemoExePath.TabStop = false;
				this.btnPrgMemoExePathBrowse.TabStop = false;
				this.txtPrgMemoCmdLine.TabStop = false;
				this.btnPrgMemoOpen.TabStop = false;
				this.btnOK.TabIndex = 2;
				this.btnCancel.TabIndex = 3;
			}
			if (strMemoType == "REFRENCE")
			{
				this.rtbTxtMemo.TabStop = false;
				this.txtRefMemoServerKey.TabStop = true;
				this.txtRefMemoBookId.TabStop = true;
				this.txtRefMemoOtherRef.TabStop = true;
				this.btnRefMemoOpen.TabStop = true;
				this.txtRefMemoServerKey.TabIndex = 1;
				this.txtRefMemoBookId.TabIndex = 2;
				this.txtRefMemoOtherRef.TabIndex = 3;
				this.btnRefMemoOpen.TabIndex = 4;
				this.txtDescription.TabStop = false;
				this.txtHypMemoUrl.TabStop = false;
				this.btnHypMemoOpen.TabStop = false;
				this.txtPrgMemoExePath.TabStop = false;
				this.btnPrgMemoExePathBrowse.TabStop = false;
				this.txtPrgMemoCmdLine.TabStop = false;
				this.btnPrgMemoOpen.TabStop = false;
				this.btnOK.TabIndex = 5;
				this.btnCancel.TabIndex = 6;
			}
			if (strMemoType == "HYPERLINK")
			{
				this.rtbTxtMemo.TabStop = false;
				this.txtRefMemoServerKey.TabStop = false;
				this.txtRefMemoBookId.TabStop = false;
				this.txtRefMemoOtherRef.TabStop = false;
				this.btnRefMemoOpen.TabStop = false;
				this.txtDescription.TabStop = true;
				this.txtHypMemoUrl.TabStop = true;
				this.btnHypMemoOpen.TabStop = true;
				this.txtDescription.TabIndex = 1;
				this.txtHypMemoUrl.TabIndex = 2;
				this.btnHypMemoOpen.TabIndex = 3;
				this.txtPrgMemoExePath.TabStop = false;
				this.btnPrgMemoExePathBrowse.TabStop = false;
				this.txtPrgMemoCmdLine.TabStop = false;
				this.btnPrgMemoOpen.TabStop = false;
				this.btnOK.TabIndex = 4;
				this.btnCancel.TabIndex = 5;
			}
			if (strMemoType == "PROGRAME")
			{
				this.rtbTxtMemo.TabStop = false;
				this.txtRefMemoServerKey.TabStop = false;
				this.txtRefMemoBookId.TabStop = false;
				this.txtRefMemoOtherRef.TabStop = false;
				this.btnRefMemoOpen.TabStop = false;
				this.txtDescription.TabStop = false;
				this.txtHypMemoUrl.TabStop = false;
				this.btnHypMemoOpen.TabStop = false;
				this.txtPrgMemoExePath.TabStop = true;
				this.btnPrgMemoExePathBrowse.TabStop = true;
				this.txtPrgMemoCmdLine.TabStop = true;
				this.btnPrgMemoOpen.TabStop = true;
				this.txtPrgMemoExePath.TabIndex = 1;
				this.btnPrgMemoExePathBrowse.TabIndex = 2;
				this.txtPrgMemoCmdLine.TabIndex = 3;
				this.btnPrgMemoOpen.TabIndex = 4;
				this.btnOK.TabIndex = 5;
				this.btnCancel.TabIndex = 6;
			}
		}

		private void ShowDJVU(bool bState, string sSource)
		{
			if (this.objDjVuCtlMemoListGlobal.InvokeRequired)
			{
				AxDjVuCtrl axDjVuCtrl = this.objDjVuCtlMemoListGlobal;
				frmMemoListGlobal.ShowDJVUDelegate showDJVUDelegate = new frmMemoListGlobal.ShowDJVUDelegate(this.ShowDJVU);
				object[] objArray = new object[] { bState, sSource };
				axDjVuCtrl.Invoke(showDJVUDelegate, objArray);
				return;
			}
			if (!bState)
			{
				this.objDjVuCtlMemoListGlobal.SendToBack();
				this.picBoxHypPreview.BringToFront();
				return;
			}
			this.objDjVuCtlMemoListGlobal.BringToFront();
			this.picBoxHypPreview.SendToBack();
			this.pnlHypMemoPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.objDjVuCtlMemoListGlobal.Zoom = "100";
			this.objDjVuCtlMemoListGlobal.Toolbar = "1";
			this.objDjVuCtlMemoListGlobal.SRC = sSource;
		}

		private void ShowMemoDetails(XmlNode xNode)
		{
			if (xNode == null || xNode.Attributes.Count <= 0 || xNode.Attributes["Value"] == null || xNode.Attributes["Type"] == null || !(xNode.Attributes["Value"].Value.Trim() != string.Empty) || !(xNode.Attributes["Type"].Value.Trim() != string.Empty))
			{
				this.pnlError.BringToFront();
				return;
			}
			if (xNode.Attributes["Type"].Value.Trim().ToUpper() == "TXT")
			{
				this.SetTabProperty("TEXT");
				string empty = string.Empty;
				if (xNode.Attributes["Update"] == null)
				{
					empty = "Unknown";
				}
				else if (xNode.Attributes["Update"].Value.Trim() == string.Empty)
				{
					empty = "Unknown";
				}
				else
				{
					empty = xNode.Attributes["Update"].Value.Trim();
					try
					{
						DateTime dateTime = DateTime.ParseExact(empty, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
						string[] strArrays = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
						if (this.strDateFormat != string.Empty)
						{
							if (this.strDateFormat.ToUpper() != "INVALID")
							{
								string[] strArrays1 = strArrays;
								for (int i = 0; i < (int)strArrays1.Length; i++)
								{
									string str = strArrays1[i];
									if (this.strDateFormat == str)
									{
										empty = dateTime.ToString(this.strDateFormat);
									}
								}
							}
							else
							{
								empty = "Unknown";
							}
						}
					}
					catch
					{
					}
				}
				this.lblTxtMemoDate.Text = empty;
				this.rtbTxtMemo.Text = xNode.Attributes["Value"].Value;
				this.pnlTxtMemo.BringToFront();
				return;
			}
			if (xNode.Attributes["Type"].Value.Trim().ToUpper() == "REF")
			{
				this.SetTabProperty("REFRENCE");
				string empty1 = string.Empty;
				if (xNode.Attributes["Update"] == null)
				{
					empty1 = "Unknown";
				}
				else if (xNode.Attributes["Update"].Value.Trim() == string.Empty)
				{
					empty1 = "Unknown";
				}
				else
				{
					empty1 = xNode.Attributes["Update"].Value.Trim();
					try
					{
						DateTime dateTime1 = DateTime.ParseExact(empty1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
						string[] strArrays2 = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
						if (this.strDateFormat != string.Empty)
						{
							if (this.strDateFormat.ToUpper() != "INVALID")
							{
								string[] strArrays3 = strArrays2;
								for (int j = 0; j < (int)strArrays3.Length; j++)
								{
									string str1 = strArrays3[j];
									if (this.strDateFormat == str1)
									{
										empty1 = dateTime1.ToString(this.strDateFormat);
									}
								}
							}
							else
							{
								empty1 = "Unknown";
							}
						}
					}
					catch
					{
					}
				}
				this.lblRefMemoDate.Text = empty1;
				string value = xNode.Attributes["Value"].Value;
				string[] strArrays4 = new string[] { " " };
				string[] strArrays5 = value.Split(strArrays4, StringSplitOptions.None);
				if ((int)strArrays5.Length < 2)
				{
					this.pnlError.BringToFront();
					return;
				}
				this.txtRefMemoServerKey.Text = strArrays5[0];
				this.txtRefMemoBookId.Text = strArrays5[1];
				this.txtRefMemoOtherRef.Text = string.Empty;
				for (int k = 2; k < (int)strArrays5.Length; k++)
				{
					TextBox textBox = this.txtRefMemoOtherRef;
					textBox.Text = string.Concat(textBox.Text, strArrays5[k]);
					if (k < (int)strArrays5.Length - 1)
					{
						TextBox textBox1 = this.txtRefMemoOtherRef;
						textBox1.Text = string.Concat(textBox1.Text, " ");
					}
				}
				this.pnlRefMemo.BringToFront();
				return;
			}
			if (xNode.Attributes["Type"].Value.Trim().ToUpper() == "HYP")
			{
				this.SetTabProperty("HYPERLINK");
				string empty2 = string.Empty;
				if (xNode.Attributes["Update"] == null)
				{
					empty2 = "Unknown";
				}
				else if (xNode.Attributes["Update"].Value.Trim() == string.Empty)
				{
					empty2 = "Unknown";
				}
				else
				{
					empty2 = xNode.Attributes["Update"].Value.Trim();
					try
					{
						DateTime dateTime2 = DateTime.ParseExact(empty2, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
						string[] strArrays6 = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
						if (this.strDateFormat != string.Empty)
						{
							if (this.strDateFormat.ToUpper() != "INVALID")
							{
								string[] strArrays7 = strArrays6;
								for (int l = 0; l < (int)strArrays7.Length; l++)
								{
									string str2 = strArrays7[l];
									if (this.strDateFormat == str2)
									{
										empty2 = dateTime2.ToString(this.strDateFormat);
									}
								}
							}
							else
							{
								empty2 = "Unknown";
							}
						}
					}
					catch
					{
					}
				}
				this.lblHypMemoDate.Text = empty2;
				this.txtHypMemoUrl.Text = xNode.Attributes["Value"].Value;
				this.pnlHypMemo.BringToFront();
				return;
			}
			if (xNode.Attributes["Type"].Value.Trim().ToUpper() != "PRG")
			{
				this.pnlError.BringToFront();
				return;
			}
			this.SetTabProperty("PROGRAME");
			string empty3 = string.Empty;
			if (xNode.Attributes["Update"] == null)
			{
				empty3 = "Unknown";
			}
			else if (xNode.Attributes["Update"].Value.Trim() == string.Empty)
			{
				empty3 = "Unknown";
			}
			else
			{
				empty3 = xNode.Attributes["Update"].Value.Trim();
				try
				{
					DateTime dateTime3 = DateTime.ParseExact(empty3, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					string[] strArrays8 = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
					if (this.strDateFormat != string.Empty)
					{
						if (this.strDateFormat.ToUpper() != "INVALID")
						{
							string[] strArrays9 = strArrays8;
							for (int m = 0; m < (int)strArrays9.Length; m++)
							{
								string str3 = strArrays9[m];
								if (this.strDateFormat == str3)
								{
									empty3 = dateTime3.ToString(this.strDateFormat);
								}
							}
						}
						else
						{
							empty3 = "Unknown";
						}
					}
				}
				catch
				{
				}
			}
			this.lblPrgMemoDate.Text = empty3;
			string value1 = xNode.Attributes["Value"].Value;
			string[] strArrays10 = new string[] { "|" };
			string[] strArrays11 = value1.Split(strArrays10, StringSplitOptions.None);
			this.txtPrgMemoExePath.Text = strArrays11[0];
			if ((int)strArrays11.Length > 1)
			{
				this.txtPrgMemoCmdLine.Text = strArrays11[1];
			}
			this.pnlPrgMemo.BringToFront();
		}

		private void txtHypMemoUrl_TextChanged(object sender, EventArgs e)
		{
			if (this.txtHypMemoUrl.Text.Trim() != string.Empty)
			{
				try
				{
					if (this.frmParent.objFrmTasks.intMemoType == 2)
					{
						string fileExtension = this.GetFileExtension(this.txtHypMemoUrl.Text.ToString().Trim().ToUpper());
						bool flag = false;
						string str = fileExtension;
						string str1 = str;
						if (str != null)
						{
							switch (str1)
							{
								case "DJVU":
								case "JPG":
								case "JPEG":
								case "PNG":
								case "BMP":
								case "GIF":
								{
									goto Label0;
								}
							}
						}
						this.lblHypMemoNote.Text = this.GetResource("Can not preview. Please refer directly by pressing the Browse button.", "PROVIDE_URL_NEW", ResourceType.LABEL);
						this.lblHypMemoNote.Show();
						flag = true;
					Label0:
						if (flag)
						{
							this.picBoxHypPreview.Image = null;
							this.objDjVuCtlMemoListGlobal.SRC = null;
							this.lblHypMemoNote.Text = this.GetResource("Can not preview. Please refer directly by pressing the Browse button.", "PROVIDE_URL_NEW", ResourceType.LABEL);
							this.lblHypMemoNote.Show();
						}
						else
						{
							this.lblHypMemoNote.Hide();
							this.pnlHypMemoPreview.Visible = true;
							this.pnlHypMemoContents.Dock = DockStyle.Top;
							this.pnlHypMemoPreview.Dock = DockStyle.Fill;
							this.pnlHypMemoPreview.AutoScroll = true;
							if (!this.txtHypMemoUrl.Text.ToUpper().EndsWith("DJVU"))
							{
								this.ShowDJVU(false, "");
								this.picBoxHypPreview.Image = null;
								this.pnlBottom.Dock = DockStyle.Fill;
								this.pnlMemos.Dock = DockStyle.Fill;
								this.pnlHypMemo.Dock = DockStyle.Fill;
								this.picBoxHypPreview.LoadAsync(this.txtHypMemoUrl.Text);
								System.Drawing.Size size = this.picBoxHypPreview.InitialImage.Size;
								this.pnlHypMemoPreview.AutoScrollMinSize = size;
							}
							else
							{
								this.picBoxHypPreview.Image = null;
								if (this.txtHypMemoUrl.Text.Trim().StartsWith("http://") || this.txtHypMemoUrl.Text.Trim().StartsWith("https://"))
								{
									this.strDjVuPicPath = this.DownloadPicture(this.txtHypMemoUrl.Text);
									this.lstExportedGlobalMemoListPictures.Add(this.strDjVuPicPath);
								}
								else
								{
									this.strDjVuPicPath = this.txtHypMemoUrl.Text;
								}
								if (!(this.strDjVuPicPath != "") || !File.Exists(this.strDjVuPicPath))
								{
									this.objDjVuCtlMemoListGlobal.SRC = null;
									MessageBox.Show(this.GetResource("Information: Image is not available at specified path.", "HYP_DJVU_LOAD_ERROR", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
								else
								{
									this.bgWorker = new BackgroundWorker()
									{
										WorkerSupportsCancellation = true
									};
									this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
									this.bgWorker.RunWorkerAsync();
									TimeSpan timeSpan = TimeSpan.FromSeconds((double)this.GetDJVULoadTime());
									DateTime now = DateTime.Now;
									while ((DateTime.Now - now) < timeSpan)
									{
										if (!this.bPageChanged)
										{
											continue;
										}
										this.bPageChanged = false;
										this.bgWorker.CancelAsync();
									}
									if (!this.bPageChanged)
									{
										this.bgWorker.CancelAsync();
									}
									if (this.objDjVuCtlMemoListGlobal.SRC == "")
									{
										this.lblHypMemoNote.Text = this.GetResource("Can not preview. Please refer directly by pressing the Browse button.", "PROVIDE_URL_NEW", ResourceType.LABEL);
										this.lblHypMemoNote.Show();
									}
								}
							}
						}
					}
					this.frmParent.objFrmTasks.tvMemoTypes.Refresh();
				}
				catch (Exception exception)
				{
				}
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblGlobalMemo.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgMemoList.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgMemoList.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		private void UpdateMemoToList(DataGridViewRow row)
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			string str1 = string.Empty;
			if (this.dgMemoList.Columns.Count == 3)
			{
				empty1 = row.Cells[1].Value.ToString();
				if (empty1 == "Text")
				{
					if (!this.rtbTxtMemo.Text.Trim().Equals(string.Empty))
					{
						empty = this.rtbTxtMemo.Text;
						empty1 = "txt";
						str1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
					}
				}
				else if (empty1 == "Reference")
				{
					if (!this.txtRefMemoServerKey.Text.Trim().Equals(string.Empty) && !this.txtRefMemoBookId.Text.Trim().Equals(string.Empty))
					{
						string[] text = new string[] { this.txtRefMemoServerKey.Text, " ", this.txtRefMemoBookId.Text, " ", this.txtRefMemoOtherRef.Text };
						empty = string.Concat(text);
						empty1 = "ref";
						str1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
					}
				}
				else if (empty1 == "Hyperlink")
				{
					if (!this.txtHypMemoUrl.Text.Trim().Equals(string.Empty))
					{
						empty = this.txtHypMemoUrl.Text;
						empty1 = "hyp";
						str1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
					}
				}
				else if (empty1 == "Program" && !this.txtPrgMemoExePath.Text.Trim().Equals(string.Empty))
				{
					empty = string.Concat(this.txtPrgMemoExePath.Text, "|", this.txtPrgMemoCmdLine.Text);
					empty1 = "prg";
					str1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
				}
				if (!empty.Trim().Equals(string.Empty) && !this.MatchXmlAttribute("Value", empty, row.Tag.ToString()))
				{
					str = (empty.Trim().Length <= 25 ? empty.Trim() : string.Concat(empty.Trim().Substring(0, 25), "..."));
					if (str.Contains("||"))
					{
						str = str.Replace("||", " ");
					}
					if (empty1 == "txt")
					{
						this.lblTxtMemoDate.Text = str1;
					}
					else if (empty1 == "ref")
					{
						this.lblRefMemoDate.Text = str1;
					}
					else if (empty1 == "hyp")
					{
						this.lblHypMemoDate.Text = str1;
					}
					else if (empty1 == "prg")
					{
						this.lblPrgMemoDate.Text = str1;
					}
					this.dgMemoList[0, row.Index].Value = str;
					this.dgMemoList[2, row.Index].Value = str1;
					this.dgMemoList.SelectedRows[0].Tag = this.CreateMemoNode(empty1, empty, str1).OuterXml;
					this.bMemoChanged = true;
				}
			}
		}

		private delegate void ShowDJVUDelegate(bool bState, string sSource);
	}
}
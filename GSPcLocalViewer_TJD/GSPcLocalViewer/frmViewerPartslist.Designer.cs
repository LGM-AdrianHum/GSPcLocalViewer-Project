using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
	public class frmViewerPartslist : DockContent
	{
		private const string dllZipper = "ZIPPER.dll";

		private IContainer components;

		protected Panel pnlForm;

		public PictureBox picLoading;

		private BackgroundWorker bgWorker;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

		private System.Windows.Forms.ContextMenuStrip cmsPartslist;

		private ToolStripMenuItem tsmAddPartMemo;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem tsmClearSelection;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private ToolStripMenuItem tsmAddToSelectionList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem selectAllToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem copyToClipboardToolStripMenuItem;

		private ToolStripMenuItem commaSeparatedToolStripMenuItem;

		private ToolStripMenuItem tabSeparatedToolStripMenuItem;

		private ToolStripMenuItem exportToFileToolStripMenuItem;

		private ToolStripMenuItem commaSeparatedToolStripMenuItem1;

		private ToolStripMenuItem tabSeparatedToolStripMenuItem1;

		private SaveFileDialog dlgSaveFile;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripMenuItem nextListToolStripMenuItem;

		private ToolStripMenuItem previousListToolStripMenuItem;

		private System.Windows.Forms.ContextMenuStrip cmsReference;

		private ToolStrip toolStrip1;

		private ToolStripButton tsBtnNext;

		private ToolStripTextBox tsTxtList;

		private ToolStripButton tsBtnPrev;

		private ToolStripButton tsbClearSelection;

		private ToolStripButton tsbSelectAll;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton tsbAddPartMemo;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton tsbAddToSelectionList;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton tsbPartistInfo;

		public Panel pnlInfo;

		public Label lblPartsListInfo;

		private TabControl tabControl1;

		private Panel pnlGrids;

		private ToolStripMenuItem rowSelectionModeToolStripMenuItem;

		private SplitContainer splitPnlGrids;

		public DataGridView dgPartslist;

		private DataGridViewTextBoxColumn Column1;

		public DataGridView dgJumps;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private frmViewer frmParent;

		private List<int> lstDisableRows;

		private int curServerId;

		private XmlNode curListSchema;

		private XmlNode curPageSchema;

		private XmlNode curPageNode;

		private int curPicIndex;

		private int curListIndex;

		private string attPicElement;

		private string attListElement;

		private string attUpdateDateElement;

		private bool picLoadedSuccessfully;

		public bool isWorking;

		public string highlightPartNo;

		private string attPartNoElement;

		private string attPartNameElement;

		private string attPictureFileElement;

		private string attPartStatusElement;

		private string sPListStatusColName;

		private string curPictureFileName;

		private string statusText;

		private string partlistInfoMsg;

		private bool p_Encrypted;

		private bool p_Compressed;

		public ArrayList attAdminMemList;

		public string attListUpdateDateElement;

		private string BookPublishingId;

		private string sPLTitle;

		private Download objDownloader;

		public XmlNodeList objXmlNodeList;

		private bool bSelectionMode;

		public int intMemoType;

		public string CurrentListIndex
		{
			get
			{
				return (this.curListIndex + 1).ToString();
			}
		}

		public string CurrentPartNumber
		{
			get
			{
				if (this.attPartNoElement.Equals(string.Empty) || this.curListSchema == null)
				{
					return string.Empty;
				}
				if (this.dgPartslist.SelectedRows.Count <= 0)
				{
					return string.Empty;
				}
				if (this.dgPartslist.SelectedRows.Count <= 1 || this.intMemoType != 2)
				{
					return this.dgPartslist[this.attPartNoElement, this.dgPartslist.SelectedRows[0].Index].Value.ToString();
				}
				return this.dgPartslist[this.attPartNoElement, this.dgPartslist.SelectedRows[this.dgPartslist.SelectedRows.Count - 1].Index].Value.ToString();
			}
		}

		public string CurrentPicIndex
		{
			get
			{
				return (this.curPicIndex + 1).ToString();
			}
		}

		public DataGridView Partlist
		{
			get
			{
				return this.dgPartslist;
			}
		}

		public frmViewerPartslist(frmViewer frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.curServerId = 9999;
			this.curListSchema = null;
			this.curPageSchema = null;
			this.curPageNode = null;
			this.curPicIndex = 0;
			this.curListIndex = 0;
			this.attPicElement = string.Empty;
			this.attListElement = string.Empty;
			this.attUpdateDateElement = string.Empty;
			this.picLoadedSuccessfully = false;
			this.isWorking = false;
			this.highlightPartNo = string.Empty;
			this.attPartNoElement = string.Empty;
			this.attPartNameElement = string.Empty;
			this.attPartStatusElement = string.Empty;
			this.attPictureFileElement = string.Empty;
			this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
			this.curPictureFileName = string.Empty;
			this.attAdminMemList = new ArrayList();
			this.tsbAddPartMemo.Enabled = false;
			this.tsmAddPartMemo.Enabled = false;
			this.frmParent.EnableAddPartMemoMenu(false);
			this.BookPublishingId = string.Empty;
			this.objDownloader = new Download(this.frmParent);
			this.UpdateFont();
			this.LoadResources();
			this.tsbAddPartMemo.Visible = Program.objAppFeatures.bMemo;
			this.toolStripSeparator5.Visible = Program.objAppFeatures.bMemo;
			this.rowSelectionModeToolStripMenuItem.Visible = Program.objAppFeatures.bPListSelMode;
		}

		private void AddCheckBoxColumn()
		{
			try
			{
				DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
				DatagridViewCheckBoxHeaderCell datagridViewCheckBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
				datagridViewCheckBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(this.chkHeader_OnCheckBoxClicked);
				datagridViewCheckBoxHeaderCell.Value = string.Empty;
				dataGridViewCheckBoxColumn.HeaderCell = datagridViewCheckBoxHeaderCell;
				dataGridViewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewCheckBoxColumn.Frozen = true;
				dataGridViewCheckBoxColumn.TrueValue = true;
				dataGridViewCheckBoxColumn.FalseValue = false;
				dataGridViewCheckBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dataGridViewCheckBoxColumn.Name = "CHK";
				this.dgPartslist.Columns.Add(dataGridViewCheckBoxColumn);
				this.dgPartslist.Columns[0].Visible = true;
			}
			catch
			{
			}
		}

		private void AddImageColumn()
		{
			try
			{
				DataGridViewImageColumn dataGridViewImageColumn = new DataGridViewImageColumn()
				{
					AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
					Frozen = true,
					HeaderText = string.Empty
				};
				dataGridViewImageColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				dataGridViewImageColumn.Name = "arrow";
				this.dgJumps.Columns.Add(dataGridViewImageColumn);
			}
			catch
			{
			}
		}

		private void addJumpCol(string sColKey, string sDefaultColHeader, int iDefaultColWidth, DataGridViewContentAlignment dgDefaultContentAlignment, bool bVisible)
		{
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					HeaderCell = new DataGridViewColumnHeaderCell()
				};
				string item = Program.iniServers[this.frmParent.ServerId].items["PLIST_JUMP_SETTINGS", sColKey];
				if (item == null || !(item != string.Empty))
				{
					dataGridViewTextBoxColumn.HeaderCell.Value = sDefaultColHeader;
					dataGridViewTextBoxColumn.Width = iDefaultColWidth;
					dataGridViewTextBoxColumn.Tag = sColKey;
					dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = dgDefaultContentAlignment;
					dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = dgDefaultContentAlignment;
				}
				else
				{
					string[] strArrays = new string[] { "|" };
					string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					if (item[0].ToString() != string.Empty)
					{
						dataGridViewTextBoxColumn.HeaderCell.Value = this.GetJumpsHeaderCellValue(sDefaultColHeader, strArrays1[0]);
					}
					if (item[0].ToString() != string.Empty)
					{
						try
						{
							if (strArrays1[1].ToString().ToUpper() == "C")
							{
								dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
								dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
							}
							if (strArrays1[1].ToString().ToUpper() == "L")
							{
								dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
							}
							if (strArrays1[1].ToString().ToUpper() == "R")
							{
								dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
								dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
							}
						}
						catch
						{
							dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = dgDefaultContentAlignment;
						}
					}
					if (item[0].ToString() != string.Empty)
					{
						int num = 0;
						if (int.TryParse(strArrays1[2], out num))
						{
							dataGridViewTextBoxColumn.Width = num;
						}
						else
						{
							dataGridViewTextBoxColumn.Width = iDefaultColWidth;
						}
					}
				}
				dataGridViewTextBoxColumn.Name = sColKey;
				dataGridViewTextBoxColumn.Visible = bVisible;
				dataGridViewTextBoxColumn.ReadOnly = true;
				dataGridViewTextBoxColumn.DefaultCellStyle.NullValue = string.Empty;
				this.dgJumps.Columns.Add(dataGridViewTextBoxColumn);
			}
			catch
			{
			}
		}

		private void addJumpColumns()
		{
			try
			{
				this.dgJumps.Columns.Clear();
				this.AddImageColumn();
				this.addJumpCol("KEY", "Key", 100, DataGridViewContentAlignment.MiddleLeft, true);
				this.addJumpCol("TITLE", "Title", 100, DataGridViewContentAlignment.MiddleLeft, true);
				this.addJumpCol("JUMPSTRING", "JumpString", 100, DataGridViewContentAlignment.MiddleLeft, false);
			}
			catch
			{
			}
		}

		private void AddMemoColumns()
		{
			try
			{
				ArrayList arrayLists = this.LoadMemoTypeKeys();
				DataGridViewImageColumn[] dataGridViewImageColumn = new DataGridViewImageColumn[19];
				for (int i = 0; i < arrayLists.Count; i++)
				{
					dataGridViewImageColumn[i] = new DataGridViewImageColumn();
					dataGridViewImageColumn[i].HeaderCell = new DataGridViewColumnHeaderCell();
					dataGridViewImageColumn[i].DefaultCellStyle.NullValue = string.Empty;
					dataGridViewImageColumn[i].HeaderCell.Style.Alignment = dataGridViewImageColumn[i].DefaultCellStyle.Alignment;
					dataGridViewImageColumn[i].Visible = false;
					dataGridViewImageColumn[i].Tag = arrayLists[i].ToString();
					dataGridViewImageColumn[i].Name = arrayLists[i].ToString();
					dataGridViewImageColumn[i].HeaderText = arrayLists[i].ToString();
					this.dgPartslist.Columns.Add(dataGridViewImageColumn[i]);
				}
			}
			catch
			{
			}
		}

		public void AddMemoIcon(string sColName, int iRowIndex)
		{
			try
			{
				if (Program.objAppFeatures.bMemo)
				{
					if (sColName.ToUpper().Contains("TXT"))
					{
						this.dgPartslist[sColName, iRowIndex].Value = GSPcLocalViewer.Properties.Resources.Memo_Txt;
					}
					else if (sColName.ToUpper().Contains("HYP"))
					{
						this.dgPartslist[sColName, iRowIndex].Value = GSPcLocalViewer.Properties.Resources.Memo_Hyp;
					}
					else if (sColName.ToUpper().Contains("REF"))
					{
						this.dgPartslist[sColName, iRowIndex].Value = GSPcLocalViewer.Properties.Resources.Memo_Ref;
					}
					else if (!sColName.ToUpper().Contains("PRG"))
					{
						this.dgPartslist[sColName, iRowIndex].Value = GSPcLocalViewer.Properties.Resources.Memo_Image;
					}
					else
					{
						this.dgPartslist[sColName, iRowIndex].Value = GSPcLocalViewer.Properties.Resources.Memo_Prg;
					}
				}
			}
			catch
			{
			}
		}

		private void AddSchemaColumns(XmlNode schemaNode)
		{
			try
			{
				for (int i = 0; i < schemaNode.Attributes.Count; i++)
				{
					DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
					{
						HeaderCell = new DataGridViewColumnHeaderCell(),
						Name = schemaNode.Attributes[i].Name,
						Tag = schemaNode.Attributes[i].Value
					};
					dataGridViewTextBoxColumn.DefaultCellStyle.NullValue = string.Empty;
					dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = dataGridViewTextBoxColumn.DefaultCellStyle.Alignment;
					dataGridViewTextBoxColumn.Visible = false;
					if (!this.dgPartslist.Columns.Contains(dataGridViewTextBoxColumn))
					{
						this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn);
					}
				}
			}
			catch
			{
			}
		}

		private void addSelectionListKeyCol()
		{
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					HeaderCell = new DataGridViewColumnHeaderCell()
				};
				dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridViewTextBoxColumn.HeaderCell.Value = "PART_SLIST_KEY";
				dataGridViewTextBoxColumn.Name = "PART_SLIST_KEY";
				dataGridViewTextBoxColumn.Tag = "PART_SLIST_KEY";
				dataGridViewTextBoxColumn.Visible = false;
				dataGridViewTextBoxColumn.ReadOnly = true;
				dataGridViewTextBoxColumn.DefaultCellStyle.NullValue = string.Empty;
				this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn);
			}
			catch
			{
			}
		}

		private void addSortingColumn()
		{
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					HeaderCell = new DataGridViewColumnHeaderCell()
				};
				dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridViewTextBoxColumn.HeaderCell.Value = "AutoIndexColumn";
				dataGridViewTextBoxColumn.Name = "AutoIndexColumn";
				dataGridViewTextBoxColumn.Tag = "AutoIndexColumn";
				dataGridViewTextBoxColumn.Visible = false;
				dataGridViewTextBoxColumn.ReadOnly = true;
				dataGridViewTextBoxColumn.DefaultCellStyle.NullValue = string.Empty;
				this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn);
			}
			catch
			{
			}
		}

		private void AdjustGridHeights()
		{
			if (this.splitPnlGrids.InvokeRequired)
			{
				this.splitPnlGrids.Invoke(new frmViewerPartslist.AdjustGridHeightsDelegate(this.AdjustGridHeights));
				return;
			}
			try
			{
				this.splitPnlGrids.SplitterDistance = Settings.Default.ListSplitterDistance;
			}
			catch
			{
			}
		}

		public void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.BookPublishingId = this.frmParent.BookPublishingId;
			this.frmParent.bObjFrmPartlistClosed = false;
			object[] argument = (object[])e.Argument;
			XmlNode xmlNodes = (XmlNode)argument[0];
			XmlNode xmlNodes1 = (XmlNode)argument[1];
			int num = (int)argument[2];
			int num1 = (int)argument[3];
			XmlNodeList xmlNodeLists = (XmlNodeList)argument[4];
			string empty = string.Empty;
			string item = string.Empty;
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodes2 = null;
			bool flag = false;
			DateTime dateTime = new DateTime();
			try
			{
				this.statusText = this.GetResource("Loading Parts List……", "LOADING_PARTSLIST", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				if (num1 >= xmlNodeLists.Count)
				{
					num1 = 0;
				}
				this.curListIndex = num1;
				this.SetListIndex(xmlNodeLists, num1);
				this.partlistInfoMsg = string.Empty;
				string str = Program.iniServers[this.frmParent.ServerId].items["PLIST", "INFO"];
				if (str == string.Empty)
				{
					this.partlistInfoMsg = Program.iniServers[this.frmParent.ServerId].items["PLIST", "INFO_MESSAGE"];
				}
				else
				{
					string name = string.Empty;
					if (str != null && str != string.Empty)
					{
						foreach (XmlAttribute attribute in xmlNodes.Attributes)
						{
							if (attribute.Value == null || !(attribute.Value.ToUpper() == str.ToUpper()))
							{
								continue;
							}
							name = attribute.Name;
							break;
						}
					}
					if (xmlNodeLists[this.curListIndex].Attributes[name] != null)
					{
						this.partlistInfoMsg = xmlNodeLists[this.curListIndex].Attributes[name].Value;
					}
				}
				this.UpdatePListInfoTextBox();
				this.EnableDisablePListInfoBtn();
				if (this.partlistInfoMsg == string.Empty || !Settings.Default.appPartsListInfoVisible)
				{
					this.ShowHidePListInfoPanel(false);
				}
				else
				{
					this.ShowHidePListInfoPanel(true);
				}
				this.CheckPListInfoBtn(Settings.Default.appPartsListInfoVisible);
				if (!xmlNodeLists[this.curListIndex].Attributes[this.attListElement].Value.Equals(string.Empty))
				{
					if (!xmlNodeLists[this.curListIndex].Attributes[this.attListElement].Value.ToUpper().EndsWith(".XML"))
					{
						xmlNodeLists[this.curListIndex].Attributes[this.attListElement].Value = string.Concat(xmlNodeLists[this.curListIndex].Attributes[this.attListElement].Value, ".XML");
					}
					empty = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
					if (!empty.EndsWith("/"))
					{
						empty = string.Concat(empty, "/");
					}
					try
					{
						this.sPLTitle = xmlNodeLists[this.curListIndex].Attributes[this.sPLTitle].Value;
					}
					catch
					{
						this.sPLTitle = string.Concat(this.GetResource("Parts List", "PARTS_LIST", ResourceType.TITLE), "      ");
					}
					empty = string.Concat(empty, this.BookPublishingId, "/", xmlNodeLists[this.curListIndex].Attributes[this.attListElement].Value);
					item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
					item = string.Concat(item, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
					item = string.Concat(item, "\\", this.BookPublishingId);
					if (!Directory.Exists(item))
					{
						Directory.CreateDirectory(item);
					}
					item = string.Concat(item, "\\", xmlNodeLists[this.curListIndex].Attributes[this.attListElement].Value);
					if (this.p_Compressed)
					{
						empty = empty.ToUpper().Replace(".XML", ".ZIP");
						item = item.ToUpper().Replace(".XML", ".ZIP");
					}
					try
					{
						dateTime = DateTime.Parse(xmlNodeLists[this.curListIndex].Attributes[this.attUpdateDateElement].Value, new CultureInfo("fr-FR", false));
					}
					catch
					{
					}
				}
			}
			catch
			{
				this.bgWorker.CancelAsync();
				this.frmParent.HidePartsList();
				MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM003) Failed to create file/folder specified", "(E-VPL-EM003)_FAILED", ResourceType.POPUP_MESSAGE));
				return;
			}
			if (!(empty != string.Empty) || !(item != string.Empty))
			{
				this.frmParent.HidePartsList();
			}
			else
			{
				int num2 = 0;
				if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num2))
				{
					num2 = 0;
				}
				if (!File.Exists(item))
				{
					flag = true;
				}
				else if (num2 == 0)
				{
					flag = true;
				}
				else if (num2 < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(item, this.p_Compressed, this.p_Encrypted), dateTime, num2))
				{
					flag = true;
				}
				if (flag && !Program.objAppMode.bWorkingOffline)
				{
					this.objDownloader.DownloadFile(empty, item);
				}
				if (!File.Exists(item))
				{
					this.frmParent.HidePartsList();
				}
				else if (!this.frmParent.IsDisposed)
				{
					this.statusText = this.GetResource("Loading Parts List……", "LOADING_PARTSLIST", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					if (!File.Exists(item))
					{
						this.frmParent.HidePartsList();
					}
					else
					{
						if (!this.p_Compressed)
						{
							try
							{
								xmlDocument.Load(item);
							}
							catch
							{
								this.bgWorker.CancelAsync();
								this.frmParent.HidePartsList();
								return;
							}
						}
						else
						{
							try
							{
								Global.Unzip(item);
								if (File.Exists(item.ToLower().Replace(".zip", ".xml")))
								{
									xmlDocument.Load(item.ToLower().Replace(".zip", ".xml"));
								}
							}
							catch
							{
							}
						}
						if (this.p_Encrypted)
						{
							try
							{
								string str1 = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
								xmlDocument.DocumentElement.InnerXml = str1;
							}
							catch
							{
							}
						}
						xmlNodes2 = xmlDocument.SelectSingleNode("//Schema");
						if (xmlNodes2 != null)
						{
							this.curListSchema = xmlNodes2;
							if (this.curServerId == 9999 || this.curServerId != this.frmParent.ServerId)
							{
								this.curServerId = this.frmParent.ServerId;
								this.InitializePartsList(xmlNodes2);
								this.InitializeJumpsList();
							}
							this.LoadPartsListInGrid(xmlDocument);
							this.LoadJumpsInGrid(xmlDocument);
							this.AdjustGridHeights();
						}
						else
						{
							this.frmParent.HidePartsList();
						}
						this.GetUpdateDateElement();
					}
				}
			}
			object[] objArray = new object[] { xmlNodes, xmlNodes1, num, num1 };
			e.Result = objArray;
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (this.dgJumps.Rows.Count != 0)
			{
				this.splitPnlGrids.Panel2Collapsed = false;
				this.splitPnlGrids.Panel2.Show();
			}
			else
			{
				this.splitPnlGrids.Panel2Collapsed = true;
				this.splitPnlGrids.Panel2.Hide();
			}
			bool flag = false;
			int num = 1;
			while (num < this.dgPartslist.Columns.Count)
			{
				if (!this.dgPartslist.Columns[num].Visible)
				{
					num++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			if (this.dgJumps.Rows.Count == 0 && this.dgPartslist.Rows.Count == 1 && !flag)
			{
				this.frmParent.HidePartsList();
			}
			this.frmParent.bObjFrmPartlistClosed = true;
			try
			{
				if (this.dgPartslist.SelectedRows.Count > 0)
				{
					this.dgPartslist.SelectedRows[0].Selected = false;
				}
			}
			catch
			{
			}
			this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
			this.HideLoading(this.pnlForm);
			this.Text = this.sPLTitle;
			try
			{
				try
				{
					object[] result = (object[])e.Result;
					XmlNode xmlNodes = (XmlNode)result[0];
					XmlNode xmlNodes1 = (XmlNode)result[1];
					int num1 = (int)result[2];
					int num2 = (int)result[3];
					if (!this.frmParent.IsDisposed)
					{
						if (this.curPageSchema != xmlNodes || this.curPageNode != xmlNodes1 || this.curPicIndex != num1 || this.curListIndex != num2)
						{
							this.isWorking = false;
							this.highlightPartNo = string.Empty;
							if (this.picLoadedSuccessfully)
							{
								this.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, 0, this.attPicElement, this.attListElement, this.attUpdateDateElement);
							}
							return;
						}
						else
						{
							int num3 = 0;
							int num4 = 0;
							string text = this.tsTxtList.Text;
							try
							{
								if (text.Contains("/"))
								{
									num3 = int.Parse(text.Substring(0, text.IndexOf("/")));
									num4 = int.Parse(text.Substring(text.IndexOf("/") + 1, text.Length - (text.IndexOf("/") + 1)));
								}
								if (num3 != 1)
								{
									this.tsBtnPrev.Enabled = true;
								}
								else
								{
									this.tsBtnPrev.Enabled = false;
								}
								if (num3 != num4)
								{
									this.tsBtnNext.Enabled = true;
								}
								else
								{
									this.tsBtnNext.Enabled = false;
								}
							}
							catch
							{
							}
							if (this.dgPartslist.Rows.Count > 0 && this.dgPartslist.SelectedRows.Count == 0)
							{
								this.SelectPart();
							}
							this.frmParent.AddToHistory();
							this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
							this.UpdateStatus();
							this.isWorking = false;
							this.dgPartslist.Visible = true;
						}
					}
				}
				catch
				{
				}
			}
			finally
			{
				this.isWorking = false;
			}
		}

		private static void ChangeBuffer(Control ctrl)
		{
			if (SystemInformation.TerminalServerSession)
			{
				return;
			}
			PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
			property.SetValue(ctrl, true, null);
		}

		public void changePartList(XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
		{
			this.dgPartslist.Visible = false;
			this.LoadPartsList(schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
		}

		private void CheckPListInfoBtn(bool bState)
		{
			if (!this.toolStrip1.InvokeRequired)
			{
				if (bState)
				{
					this.tsbPartistInfo.CheckState = CheckState.Checked;
					return;
				}
				this.tsbPartistInfo.CheckState = CheckState.Unchecked;
				return;
			}
			ToolStrip toolStrip = this.toolStrip1;
			frmViewerPartslist.CheckPListInfoBtnDelegate checkPListInfoBtnDelegate = new frmViewerPartslist.CheckPListInfoBtnDelegate(this.CheckPListInfoBtn);
			object[] objArray = new object[] { bState };
			toolStrip.Invoke(checkPListInfoBtnDelegate, objArray);
		}

		public void CheckUncheckRow(string sPartNumber, bool bCheck)
		{
			try
			{
				foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
				{
					try
					{
						if (this.lstDisableRows.Contains(row.Index))
						{
							row.Cells["CHK"].ReadOnly = true;
						}
						if (row.Cells[this.attPartNoElement].Value == null || !(row.Cells[this.attPartNoElement].Value.ToString() != string.Empty))
						{
							if (row.Cells[this.attPartNameElement].Value.ToString() == sPartNumber)
							{
								row.Cells["CHK"].Value = bCheck;
							}
						}
						else if (this.bSelectionMode)
						{
							if (row.Cells["PART_SLIST_KEY"].Value.ToString().ToUpper().Trim() == sPartNumber.Trim().ToUpper())
							{
								row.Cells["CHK"].Value = bCheck;
							}
						}
						else if (row.Cells[this.attPartNoElement].Value.ToString() == sPartNumber)
						{
							row.Cells["CHK"].Value = bCheck;
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		private void chkHeader_OnCheckBoxClicked(bool state)
		{
			this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
			if (this.dgPartslist.Columns.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
				{
					if (!(row.Cells[0] is DataGridViewCheckBoxCell) || this.lstDisableRows.Contains(row.Index))
					{
						continue;
					}
					try
					{
						if (Convert.ToBoolean(row.Cells[0].Value) != state && row.Cells["PART_SLIST_KEY"].Value != null)
						{
							this.frmParent.CheckUncheckRow(row.Cells["PART_SLIST_KEY"].Value.ToString(), state);
							if (!state)
							{
								this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, row, false);
							}
							else if (!this.frmParent.PartInSelectionList(row.Cells["PART_SLIST_KEY"].Value.ToString()))
							{
								this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, row, true);
							}
						}
					}
					catch
					{
					}
				}
			}
			this.dgPartslist.EndEdit();
			this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
		}

		public void ClearMemoIcons(int iRowIndex)
		{
			try
			{
				this.dgPartslist["MEM", iRowIndex].Value = null;
				this.dgPartslist["LOCMEM", iRowIndex].Value = null;
				this.dgPartslist["GBLMEM", iRowIndex].Value = null;
				this.dgPartslist["ADMMEM", iRowIndex].Value = null;
				this.dgPartslist["TXTMEM", iRowIndex].Value = null;
				this.dgPartslist["REFMEM", iRowIndex].Value = null;
				this.dgPartslist["HYPMEM", iRowIndex].Value = null;
				this.dgPartslist["PRGMEM", iRowIndex].Value = null;
				this.dgPartslist["LOCTXTMEM", iRowIndex].Value = null;
				this.dgPartslist["LOCREFMEM", iRowIndex].Value = null;
				this.dgPartslist["LOCHYPMEM", iRowIndex].Value = null;
				this.dgPartslist["LOCPRGMEM", iRowIndex].Value = null;
				this.dgPartslist["GBLTXTMEM", iRowIndex].Value = null;
				this.dgPartslist["GBLREFMEM", iRowIndex].Value = null;
				this.dgPartslist["GBLHYPMEM", iRowIndex].Value = null;
				this.dgPartslist["GBLPRGMEM", iRowIndex].Value = null;
				this.dgPartslist["ADMTXTMEM", iRowIndex].Value = null;
				this.dgPartslist["ADMREFMEM", iRowIndex].Value = null;
				this.dgPartslist["ADMHYPMEM", iRowIndex].Value = null;
				this.dgPartslist["ADMPRGMEM", iRowIndex].Value = null;
			}
			catch (Exception exception)
			{
			}
		}

		private void cmsPartslist_Opening(object sender, CancelEventArgs e)
		{
			this.nextListToolStripMenuItem.Enabled = this.tsBtnNext.Enabled;
			this.previousListToolStripMenuItem.Enabled = this.tsBtnPrev.Enabled;
			if (!Settings.Default.RowSelectionMode)
			{
				if (this.dgPartslist.SelectedCells.Count > 0)
				{
					this.tsmAddPartMemo.Enabled = false;
					this.tsmAddToSelectionList.Enabled = false;
				}
				this.tsmAddPartMemo.Enabled = false;
				this.tsmAddToSelectionList.Enabled = false;
				return;
			}
			if (this.dgPartslist.Columns.Count <= 1 || !(this.CurrentPartNumber != string.Empty))
			{
				this.tsmAddToSelectionList.Enabled = false;
				this.copyToClipboardToolStripMenuItem.Enabled = false;
				this.exportToFileToolStripMenuItem.Enabled = false;
			}
			else
			{
				foreach (DataGridViewRow selectedRow in this.dgPartslist.SelectedRows)
				{
					if (this.lstDisableRows == null || !this.lstDisableRows.Contains(selectedRow.Index))
					{
						this.tsmAddToSelectionList.Enabled = true;
					}
					else
					{
						this.tsmAddPartMemo.Enabled = false;
						this.tsmAddToSelectionList.Enabled = false;
						this.copyToClipboardToolStripMenuItem.Enabled = true;
						this.exportToFileToolStripMenuItem.Enabled = true;
					}
					this.copyToClipboardToolStripMenuItem.Enabled = true;
					this.exportToFileToolStripMenuItem.Enabled = true;
				}
			}
		}

		private void cmsReference_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			this.OpenURLInBrowser(e.ClickedItem.Text);
		}

		private void commaSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string empty = string.Empty;
				empty = (this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, ",") : this.GetDataGridViewText(ref this.dgPartslist, true, true, ","));
				if (empty != string.Empty)
				{
					Clipboard.SetText(empty, TextDataFormat.UnicodeText);
				}
			}
			catch
			{
			}
		}

		private void commaSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
			this.dlgSaveFile.RestoreDirectory = true;
			string empty = string.Empty;
			empty = (this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, ",") : this.GetDataGridViewText(ref this.dgPartslist, true, true, ","));
			if (empty != string.Empty && this.dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					StreamWriter streamWriter = File.CreateText(this.dlgSaveFile.FileName);
					streamWriter.Write(empty);
					streamWriter.Close();
				}
				catch
				{
					MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM002) Failed to export specified object", "(E-VPL-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
				}
			}
		}

		private DateTime DataUpdateDate(string sDataUpdateFilePath)
		{
			DateTime now;
			try
			{
				if (File.Exists(sDataUpdateFilePath))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(sDataUpdateFilePath);
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//filelastmodified");
					now = DateTime.Parse(xmlNodes.InnerText, new CultureInfo("fr-FR", false));
				}
				else
				{
					now = DateTime.Now;
				}
			}
			catch
			{
				now = DateTime.Now;
			}
			return now;
		}

		private void dgJumps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				string empty = string.Empty;
				string str = string.Empty;
				string empty1 = string.Empty;
				string str1 = string.Empty;
				string[] strArrays = this.dgJumps["JUMPSTRING", e.RowIndex].Value.ToString().Split(new char[] { '|' });
				string str2 = strArrays[1];
				if (str2.StartsWith("\"") && str2.EndsWith("\""))
				{
					str2 = str2.Trim(new char[] { '\"' });
				}
				string str3 = strArrays[(int)strArrays.Length - 1];
				string[] strArrays1 = new string[] { "-o", Program.iniServers[this.frmParent.p_ServerId].sIniKey, strArrays[0], str2.Trim(), "1", "-f" };
				string[] strArrays2 = strArrays1;
				if (this.frmParent.p_ArgsF != null)
				{
					string[] strArrays3 = new string[(int)strArrays2.Length + (int)this.frmParent.p_ArgsF.Length];
					Array.Copy(strArrays2, strArrays3, (int)strArrays2.Length);
					Array.Copy(this.frmParent.p_ArgsF, 0, strArrays3, (int)strArrays2.Length, (int)this.frmParent.p_ArgsF.Length);
					strArrays2 = strArrays3;
				}
				if (Global.SecurityLocksOpen(this.frmParent.GetBookNode(strArrays[0], this.frmParent.p_ServerId), this.frmParent.SchemaNode, this.frmParent.ServerId, this.frmParent))
				{
					if (str3.ToUpper() != "BOOKJUMP")
					{
						this.frmParent.PageJump(strArrays2);
					}
					else
					{
						this.frmParent.BookJump(strArrays2);
					}
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (this.lstDisableRows != null && this.lstDisableRows.Contains(e.RowIndex))
			{
				this.dgPartslist.Focus();
				return;
			}
			if (e.RowIndex != -1 && e.ColumnIndex == this.dgPartslist.Columns["CHK"].Index)
			{
				this.dgPartslist.CurrentCell.Value = !(bool)this.dgPartslist.CurrentCell.Value;
			}
			this.PopUpMemo(e.ColumnIndex, e.RowIndex);
			if (this.dgPartslist.Columns["REF"] != null && this.dgPartslist.Columns[e.ColumnIndex].Name == "REF" && e.RowIndex >= 0 && this.dgPartslist[e.ColumnIndex, e.RowIndex].Value != null)
			{
				string toolTipText = this.dgPartslist[e.ColumnIndex, e.RowIndex].ToolTipText;
				if (!toolTipText.Contains("**"))
				{
					frmOpenBrowser _frmOpenBrowser = new frmOpenBrowser(toolTipText, this.frmParent.ServerId);
					_frmOpenBrowser.ShowDialog();
				}
				else
				{
					string[] strArrays = new string[] { "**" };
					string[] strArrays1 = toolTipText.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					this.cmsReference.Items.Clear();
					for (int i = 0; i < (int)strArrays1.Length; i++)
					{
						this.cmsReference.Items.Add(strArrays1[i]);
					}
					this.cmsReference.Show(Control.MousePosition.X, Control.MousePosition.Y);
				}
			}
			if (this.dgPartslist.Columns["INF"] != null && e.ColumnIndex == this.dgPartslist.Columns["INF"].Index && e.RowIndex >= 0 && this.dgPartslist[e.ColumnIndex, e.RowIndex].Value != null)
			{
				string str = this.dgPartslist[e.ColumnIndex, e.RowIndex].ToolTipText;
				MessageBox.Show(str, "Part Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private void dgPartslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect)
			{
				return;
			}
			try
			{
				if (this.lstDisableRows != null && this.lstDisableRows.Contains(e.RowIndex))
				{
					return;
				}
			}
			catch
			{
			}
			try
			{
				if (e.RowIndex != -1)
				{
					if (this.dgPartslist.CurrentRow.Cells["CHK"].Value.ToString().ToUpper() == "FALSE")
					{
						this.dgPartslist.CurrentRow.Cells["CHK"].Value = true;
					}
					else if (this.dgPartslist.CurrentCell.ColumnIndex == this.dgPartslist.Columns["QTY"].Index)
					{
						this.frmParent.ShowQuantityScreen(this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.CurrentRow.Index].Value.ToString());
					}
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			try
			{
				if (!Settings.Default.RowSelectionMode)
				{
					for (int i = 0; i < this.dgPartslist.Columns.Count; i++)
					{
						this.dgPartslist.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
					}
					if (e.RowIndex == -1)
					{
						this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
						this.dgPartslist.Columns[e.ColumnIndex].Selected = true;
					}
					else
					{
						this.dgPartslist.SelectionMode = DataGridViewSelectionMode.CellSelect;
						this.dgPartslist[e.ColumnIndex, e.RowIndex].Selected = true;
					}
				}
				else if (Settings.Default.RowSelectionMode && e.RowIndex != -1)
				{
					this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					for (int j = 0; j < this.dgPartslist.Columns.Count; j++)
					{
						this.dgPartslist.Columns[j].SortMode = DataGridViewColumnSortMode.Automatic;
					}
					if (this.dgPartslist.Columns[e.ColumnIndex].CellType.Name.ToString() == "DataGridViewImageColumn" || this.dgPartslist.Columns[e.ColumnIndex].CellType.Name.ToString() == "DataGridViewImageCell")
					{
						for (int k = 0; k < this.dgPartslist.Rows.Count; k++)
						{
							this.dgPartslist.Rows[k].Selected = false;
						}
					}
					this.dgPartslist.Rows[e.RowIndex].Selected = true;
				}
				this.dgPartslist.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			catch
			{
			}
		}

		private void dgPartslist_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			try
			{
				if (this.lstDisableRows != null && this.lstDisableRows.Contains(e.RowIndex) && this.dgPartslist[this.attPartStatusElement, e.RowIndex].Value != null && this.dgPartslist[this.attPartStatusElement, e.RowIndex].Value.ToString() == "0" && e.ColumnIndex == 0)
				{
					e.PaintBackground(e.ClipBounds, true);
					e.Handled = true;
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (this.lstDisableRows == null || !this.lstDisableRows.Contains(e.RowIndex))
				{
					string empty = string.Empty;
					if (this.dgPartslist.Columns.Count > 0 && this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value != null && e.RowIndex != -1 && e.ColumnIndex == this.dgPartslist.Columns["CHK"].Index)
					{
						if (!(bool)this.dgPartslist.Rows[e.RowIndex].Cells[this.dgPartslist.Columns["CHK"].Index].Value)
						{
							this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, this.dgPartslist.Rows[e.RowIndex], false);
							this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
							empty = this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value.ToString();
							int i = 0;
							for (i = 0; i < this.dgPartslist.Rows.Count; i++)
							{
								if (this.dgPartslist.Rows[i].Cells["PART_SLIST_KEY"].Value != null && this.dgPartslist.Rows[i].Cells["PART_SLIST_KEY"].Value.ToString() == empty)
								{
									this.frmParent.CheckUncheckRow(empty, false);
								}
							}
							this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
						}
						else if (this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value != null && !this.frmParent.PartInSelectionList(this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value.ToString()))
						{
							this.frmParent.SelListAddRemoveRow(this.curServerId, this.curListSchema, this.dgPartslist.Rows[e.RowIndex], true);
							this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
							empty = this.dgPartslist["PART_SLIST_KEY", e.RowIndex].Value.ToString();
							if (empty == string.Empty)
							{
								empty = this.dgPartslist[this.attPartNameElement, e.RowIndex].Value.ToString();
							}
							int j = 0;
							for (j = 0; j < this.dgPartslist.Rows.Count; j++)
							{
								if (this.dgPartslist.Rows[j].Cells["PART_SLIST_KEY"].Value != null && this.dgPartslist.Rows[j].Cells["PART_SLIST_KEY"].Value.ToString() == empty)
								{
									this.frmParent.CheckUncheckRow(empty, true);
								}
							}
							this.dgPartslist.EndEdit();
							this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
						}
						int num = 0;
						num = 0;
						while (num < this.dgPartslist.Rows.Count && (this.lstDisableRows.Contains(num) || !(this.dgPartslist.Rows[num].Cells[0] is DataGridViewCheckBoxCell) || this.dgPartslist.Rows[num].Cells["CHK"].Value == null || (bool)this.dgPartslist.Rows[num].Cells["CHK"].Value || this.dgPartslist.Rows[num].Cells["PART_SLIST_KEY"].Value == null))
						{
							num++;
						}
						DatagridViewCheckBoxHeaderCell headerCell = (DatagridViewCheckBoxHeaderCell)this.dgPartslist.Columns[0].HeaderCell;
						if (num >= this.dgPartslist.Rows.Count)
						{
							headerCell.Checked = true;
						}
						else
						{
							headerCell.Checked = false;
						}
					}
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dgPartslist.CurrentCell is DataGridViewCheckBoxCell)
				{
					this.dgPartslist.CommitEdit(DataGridViewDataErrorContexts.Commit);
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_MouseMove(object sender, MouseEventArgs e)
		{
			this.dgPartslist.Cursor = Cursors.Arrow;
			try
			{
				int rowIndex = this.dgPartslist.HitTest(e.X, e.Y).RowIndex;
				int columnIndex = this.dgPartslist.HitTest(e.X, e.Y).ColumnIndex;
				if (this.dgPartslist[columnIndex, rowIndex].Value != null && this.dgPartslist[columnIndex, rowIndex].Value.ToString() == "System.Drawing.Bitmap")
				{
					this.dgPartslist.Cursor = Cursors.Hand;
				}
			}
			catch
			{
				this.dgPartslist.Cursor = Cursors.Arrow;
			}
		}

		private void dgPartslist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			try
			{
				this.dgPartslist["AutoIndexColumn", e.RowIndex].Value = e.RowIndex;
			}
			catch
			{
			}
		}

		private void dgPartslist_SelectionChanged(object sender, EventArgs e)
		{
			int num;
			int num1;
			int num2;
			int num3;
			this.dgPartslist.Focus();
			string listAttributeName = this.GetListAttributeName("Position");
			string empty = string.Empty;
			try
			{
				foreach (DataGridViewRow selectedRow in this.dgPartslist.SelectedRows)
				{
					if (!selectedRow.ReadOnly)
					{
						continue;
					}
					selectedRow.Selected = false;
					return;
				}
			}
			catch
			{
			}
			try
			{
				if (this.dgPartslist.Rows.Count > 0)
				{
					this.frmParent.RemoveHighlightOnPicture();
					if (this.dgPartslist.Columns.Contains(listAttributeName))
					{
						for (int i = 0; i < this.dgPartslist.SelectedRows.Count; i++)
						{
							if (this.dgPartslist[listAttributeName, this.dgPartslist.SelectedRows[i].Index].Value != null)
							{
								empty = this.dgPartslist[listAttributeName, this.dgPartslist.SelectedRows[i].Index].Value.ToString();
								string[] strArrays = new string[] { "**" };
								string[] strArrays1 = empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
								try
								{
									if (!Settings.Default.appFitPageForMultiParts || (int)strArrays1.Length <= 1)
									{
										this.frmParent.ZoomFitPage(false);
										if (!this.Focused)
										{
											base.Focus();
										}
									}
									else
									{
										this.frmParent.ZoomFitPage(true);
										if (!this.Focused)
										{
											base.Focus();
										}
									}
								}
								catch
								{
								}
								for (int j = 0; j < (int)strArrays1.Length; j++)
								{
									string str = strArrays1[j];
									string[] strArrays2 = new string[] { "," };
									string[] strArrays3 = str.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
									if ((int)strArrays3.Length == 4)
									{
										if (strArrays3[0].Contains("^"))
										{
											strArrays3[0] = strArrays3[0].Substring(strArrays3[0].IndexOf("^") + 1, strArrays3[0].Length - (strArrays3[0].IndexOf("^") + 1));
										}
										if (int.TryParse(strArrays3[0], out num) && int.TryParse(strArrays3[1], out num1) && int.TryParse(strArrays3[2], out num2) && int.TryParse(strArrays3[3], out num3))
										{
											this.frmParent.HighlightPicture(num, num1, num2 - num, num3 - num1);
											this.frmParent.ScalePicture(num, num1, num2 - num, num3 - num1);
										}
									}
								}
							}
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				if (this.dgPartslist.SelectedRows.Count != 1)
				{
					this.tsbAddPartMemo.Enabled = false;
					this.tsmAddPartMemo.Enabled = false;
					this.frmParent.EnableAddPartMemoMenu(false);
				}
				else
				{
					this.tsbAddPartMemo.Enabled = true;
					this.tsmAddPartMemo.Enabled = true;
					this.frmParent.EnableAddPartMemoMenu(true);
				}
				if (this.dgPartslist.SelectedRows.Count <= 0)
				{
					this.tsbAddToSelectionList.Enabled = false;
					this.tsbClearSelection.Enabled = false;
				}
				else
				{
					this.tsbAddToSelectionList.Enabled = true;
					this.tsbClearSelection.Enabled = true;
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			try
			{
				if (e.Column.Tag.ToString().ToUpper() != "LINKNUMBER")
				{
					e.SortResult = string.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
				}
				else
				{
					int num = 0;
					int num1 = 0;
					num = int.Parse(this.dgPartslist.Rows[e.RowIndex1].Cells["AutoIndexColumn"].Value.ToString());
					num1 = int.Parse(this.dgPartslist.Rows[e.RowIndex2].Cells["AutoIndexColumn"].Value.ToString());
					if (num > num1)
					{
						e.SortResult = 1;
					}
					else if (num >= num1)
					{
						e.SortResult = 0;
					}
					else
					{
						e.SortResult = -1;
					}
				}
				e.Handled = true;
			}
			catch
			{
			}
		}

		private void dgPartslist_Sorted(object sender, EventArgs e)
		{
			try
			{
				if (this.lstDisableRows != null && this.lstDisableRows.Count > 0)
				{
					this.lstDisableRows.Clear();
					if (!string.IsNullOrEmpty(this.attPartStatusElement))
					{
						for (int i = 0; i < this.dgPartslist.Rows.Count; i++)
						{
							try
							{
								if (this.dgPartslist[this.attPartStatusElement, i].Value != null && this.dgPartslist[this.attPartStatusElement, i].Value.ToString() == "0")
								{
									this.lstDisableRows.Add(i);
								}
							}
							catch
							{
							}
						}
					}
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

		private void EnableDisablePListInfoBtn()
		{
			if (this.toolStrip1.InvokeRequired)
			{
				this.toolStrip1.Invoke(new frmViewerPartslist.EnableDisablePListInfoBtnDelegate(this.EnableDisablePListInfoBtn));
				return;
			}
			if (this.partlistInfoMsg == string.Empty)
			{
				this.tsbPartistInfo.Enabled = false;
				return;
			}
			this.tsbPartistInfo.Enabled = true;
		}

		private string FindAttributeKey(string attVal)
		{
			string name;
			try
			{
				foreach (XmlAttribute attribute in this.curListSchema.Attributes)
				{
					if (attribute.Value != attVal)
					{
						continue;
					}
					name = attribute.Name;
					return name;
				}
				name = string.Empty;
			}
			catch
			{
				name = string.Empty;
			}
			return name;
		}

		private void frmViewerPartslist_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason != System.Windows.Forms.CloseReason.FormOwnerClosing)
			{
				this.frmParent.bPartsListClosed = true;
				base.Hide();
				e.Cancel = true;
			}
		}

		private void frmViewerPartslist_Load(object sender, EventArgs e)
		{
			this.OnOffFeatures();
			this.rowSelectionModeToolStripMenuItem.Checked = Settings.Default.RowSelectionMode;
			this.intMemoType = this.GetMemoType();
			try
			{
				if (Program.iniGSPcLocal.items["SETTINGS", "SELECTION_MODE"] == null)
				{
					this.bSelectionMode = false;
				}
				else if (Program.iniGSPcLocal.items["SETTINGS", "SELECTION_MODE"].ToUpper() != "DIVIDE")
				{
					this.bSelectionMode = false;
				}
				else
				{
					this.bSelectionMode = true;
				}
			}
			catch (Exception exception)
			{
				this.bSelectionMode = false;
			}
		}

		private void frmViewerPartslist_VisibleChanged(object sender, EventArgs e)
		{
			this.frmParent.partslistToolStripMenuItem.Checked = base.Visible;
		}

		public XmlNode GetCurListSchema()
		{
			XmlNode xmlNodes = null;
			try
			{
				xmlNodes = this.curListSchema;
			}
			catch
			{
			}
			return xmlNodes;
		}

		private string GetDataGridViewCellsText(ref DataGridView GridView, bool IncludeHeader, string Delimiter)
		{
			string empty = string.Empty;
			try
			{
				if (GridView.Rows.Count != 0)
				{
					List<string> strs = new List<string>();
					SortedDictionary<int, string> nums = new SortedDictionary<int, string>();
					int num = 0;
					int count = GridView.Rows.Count;
					for (int i = 0; i < GridView.Columns.Count; i++)
					{
						for (int j = 0; j < GridView.Rows.Count; j++)
						{
							if (GridView[i, j].Selected && j > num)
							{
								num = j;
							}
							if (GridView[i, j].Selected && j < count)
							{
								count = j;
							}
							if (!nums.ContainsValue(GridView.Columns[i].Name) && GridView[i, j].Selected && GridView.Columns[i].Visible && GridView.Columns[i].GetType().ToString() == typeof(DataGridViewTextBoxColumn).ToString())
							{
								int displayIndex = GridView.Columns[i].DisplayIndex;
								nums.Add(displayIndex, GridView.Columns[i].Name);
							}
						}
					}
					if (IncludeHeader)
					{
						foreach (KeyValuePair<int, string> keyValuePair in nums)
						{
							empty = string.Concat(empty, this.GetWriteableValue(GridView.Columns[keyValuePair.Value].HeaderText), Delimiter);
						}
						empty = string.Concat(empty.Remove(empty.Length - Delimiter.Length, Delimiter.Length), "\r\n");
					}
					for (int k = count; k <= num; k++)
					{
						string str = string.Empty;
						bool flag = false;
						foreach (KeyValuePair<int, string> num1 in nums)
						{
							if (!GridView.Rows[k].Cells[num1.Value].Selected)
							{
								str = string.Concat(str, Delimiter);
							}
							else
							{
								flag = true;
								str = string.Concat(str, this.GetWriteableValue(GridView.Rows[k].Cells[num1.Value].Value), Delimiter);
							}
						}
						if (flag)
						{
							empty = string.Concat(empty, str);
							empty = string.Concat(empty.Remove(empty.Length - Delimiter.Length, Delimiter.Length), "\r\n");
						}
					}
				}
				else
				{
					return string.Empty;
				}
			}
			catch
			{
			}
			return empty;
		}

		private string GetDataGridViewText(ref DataGridView GridView, bool IncludeHeader, bool SelectedRows, string Delimiter)
		{
			if (GridView.Rows.Count == 0)
			{
				return string.Empty;
			}
			if (SelectedRows && GridView.SelectedRows.Count == 0)
			{
				return string.Empty;
			}
			List<string> strs = new List<string>();
			SortedDictionary<int, string> nums = new SortedDictionary<int, string>();
			for (int i = 0; i < GridView.Columns.Count; i++)
			{
				if (GridView.Columns[i].Visible && GridView.Columns[i].GetType().ToString() == typeof(DataGridViewTextBoxColumn).ToString())
				{
					int displayIndex = GridView.Columns[i].DisplayIndex;
					nums.Add(displayIndex, GridView.Columns[i].Name);
				}
			}
			string str = "";
			if (IncludeHeader)
			{
				foreach (KeyValuePair<int, string> num in nums)
				{
					str = string.Concat(str, this.GetWriteableValue(GridView.Columns[num.Value].HeaderText), Delimiter);
				}
				str = string.Concat(str.Remove(str.Length - Delimiter.Length, Delimiter.Length), "\r\n");
			}
			for (int j = 0; j < GridView.Rows.Count; j++)
			{
				if (SelectedRows)
				{
					if (GridView.SelectedRows.Contains(GridView.Rows[j]))
					{
						foreach (KeyValuePair<int, string> keyValuePair in nums)
						{
							str = string.Concat(str, this.GetWriteableValue(GridView.Rows[j].Cells[keyValuePair.Value].Value), Delimiter);
						}
						str = string.Concat(str.Remove(str.Length - Delimiter.Length, Delimiter.Length), "\r\n");
					}
				}
				else if (GridView.SelectedRows.Contains(GridView.Rows[j]))
				{
					foreach (KeyValuePair<int, string> num1 in nums)
					{
						str = string.Concat(str, this.GetWriteableValue(GridView.Rows[j].Cells[num1.Value].Value), Delimiter);
					}
					str = string.Concat(str.Remove(str.Length - Delimiter.Length, Delimiter.Length), "\r\n");
				}
			}
			return str;
		}

		private string GetDGHeaderCellValue(string sKey, string sDefaultHeaderValue)
		{
			string empty = string.Empty;
			bool flag = false;
			if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
			{
				empty = sDefaultHeaderValue;
			}
			else
			{
				string str = string.Concat(Settings.Default.appLanguage, "_GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini");
				if (!File.Exists(string.Concat(Application.StartupPath, "\\Language XMLs\\", str)))
				{
					empty = sDefaultHeaderValue;
				}
				else
				{
					TextReader streamReader = new StreamReader(string.Concat(Application.StartupPath, "\\Language XMLs\\", str));
					while (true)
					{
						string str1 = streamReader.ReadLine();
						string str2 = str1;
						if (str1 == null)
						{
							break;
						}
						if (str2.ToUpper() == "[PLIST_SETTINGS]")
						{
							flag = true;
						}
						else if (str2.Contains("=") && flag)
						{
							string[] strArrays = new string[] { "=" };
							string[] strArrays1 = str2.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
							if (strArrays1[0].ToString().ToUpper() == sKey.ToUpper())
							{
								empty = strArrays1[1];
								flag = false;
								break;
							}
						}
						else if (str2.Contains("["))
						{
							flag = false;
						}
					}
					if (empty == string.Empty)
					{
						empty = sDefaultHeaderValue;
					}
					streamReader.Close();
				}
			}
			return empty;
		}

		private List<string> GetFilteredBooks()
		{
			List<string> strs;
			List<string> strs1 = new List<string>();
			try
			{
				string item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				item = string.Concat(item, "\\", Program.iniServers[this.frmParent.p_ServerId].sIniKey);
				item = string.Concat(item, "\\Series.xml");
				XmlDocument xmlDocument = new XmlDocument();
				if (File.Exists(item))
				{
					if (!this.frmParent.p_Compressed)
					{
						try
						{
							xmlDocument.Load(item);
						}
						catch
						{
							strs = strs1;
							return strs;
						}
					}
					else
					{
						try
						{
							string str = item.ToLower().Replace(".zip", ".xml");
							Global.Unzip(item);
							if (File.Exists(str))
							{
								xmlDocument.Load(str);
							}
						}
						catch
						{
						}
					}
					if (this.frmParent.p_Encrypted)
					{
						try
						{
							string str1 = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
							xmlDocument.DocumentElement.InnerXml = str1;
						}
						catch
						{
							strs = strs1;
							return strs;
						}
					}
					XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
					if (xmlNodes != null)
					{
						string empty = string.Empty;
						string name = string.Empty;
						string empty1 = string.Empty;
						string empty2 = string.Empty;
						foreach (XmlAttribute attribute in xmlNodes.Attributes)
						{
							if (attribute.Value.ToUpper().Equals("ID"))
							{
								empty = attribute.Name;
							}
							else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
							{
								name = attribute.Name;
							}
							else if (!attribute.Value.ToUpper().Equals("BOOKTYPE"))
							{
								if (!attribute.Value.ToUpper().Equals("COVERPICTURE"))
								{
									continue;
								}
								string name1 = attribute.Name;
							}
							else
							{
								string name2 = attribute.Name;
							}
						}
						if (empty == "" || name == "")
						{
							strs = strs1;
							return strs;
						}
						else
						{
							XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//Books/Book");
							if (xmlNodeLists.Count > 0)
							{
								xmlNodeLists = this.frmParent.FilterBooksList(xmlNodes, xmlNodeLists);
								if (xmlNodeLists.Count > 0)
								{
									foreach (XmlNode xmlNodes1 in xmlNodeLists)
									{
										if (xmlNodes1.Attributes.Count <= 0)
										{
											continue;
										}
										strs1.Add(xmlNodes1.Attributes[name].Value);
									}
								}
							}
						}
					}
					else
					{
						strs = strs1;
						return strs;
					}
				}
				strs = strs1;
			}
			catch
			{
				strs = strs1;
			}
			return strs;
		}

		private string GetJumpsHeaderCellValue(string sKey, string sDefaultHeaderValue)
		{
			string empty = string.Empty;
			bool flag = false;
			if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
			{
				empty = sDefaultHeaderValue;
			}
			else
			{
				string str = string.Concat(Settings.Default.appLanguage, "_GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini");
				if (!File.Exists(string.Concat(Application.StartupPath, "\\Language XMLs\\", str)))
				{
					empty = sDefaultHeaderValue;
				}
				else
				{
					TextReader streamReader = new StreamReader(string.Concat(Application.StartupPath, "\\Language XMLs\\", str));
					while (true)
					{
						string str1 = streamReader.ReadLine();
						string str2 = str1;
						if (str1 == null)
						{
							break;
						}
						if (str2.ToUpper() == "[PLIST_JUMP_SETTINGS]")
						{
							flag = true;
						}
						else if (str2.Contains("=") && flag)
						{
							string[] strArrays = new string[] { "=" };
							string[] strArrays1 = str2.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
							if (strArrays1[0].ToString().ToUpper() == sKey.ToUpper())
							{
								empty = strArrays1[1];
								flag = false;
								break;
							}
						}
						else if (str2.Contains("["))
						{
							flag = false;
						}
					}
					if (empty == string.Empty)
					{
						empty = sDefaultHeaderValue;
					}
					streamReader.Close();
				}
			}
			return empty;
		}

		private string GetListAttributeName(string attValue)
		{
			if (this.curListSchema == null)
			{
				return string.Empty;
			}
			for (int i = 0; i < this.curListSchema.Attributes.Count; i++)
			{
				if (this.curListSchema.Attributes[i].Value.ToUpper().Equals(attValue.ToUpper()))
				{
					return this.curListSchema.Attributes[i].Name;
				}
			}
			return string.Empty;
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
				str = string.Concat(str, "/Screen[@Name='MAIN_FORM']");
				str = string.Concat(str, "/Screen[@Name='PARTS_LIST']");
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

		private void GetUpdateDateElement()
		{
			try
			{
				string empty = string.Empty;
				string name = string.Empty;
				foreach (XmlAttribute attribute in this.curListSchema.Attributes)
				{
					if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
					{
						empty = attribute.Name;
					}
					if (attribute.Value.ToUpper().Equals("UPDATEDATEPL"))
					{
						name = attribute.Name;
					}
					if (!attribute.Value.ToUpper().Equals("PARTSLISTTITLE"))
					{
						continue;
					}
					this.sPLTitle = attribute.Name;
				}
				if (name == string.Empty)
				{
					this.attListUpdateDateElement = empty;
				}
				else
				{
					this.attListUpdateDateElement = name;
				}
			}
			catch
			{
			}
		}

		private string GetWriteableValue(object o)
		{
			if (o == null || o == Convert.DBNull)
			{
				return string.Empty;
			}
			if (o.ToString().IndexOf(",") == -1)
			{
				return o.ToString();
			}
			return string.Concat("\"", o.ToString(), "\"");
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern long HideCaret(IntPtr hwnd);

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
					this.picLoading.Size = new System.Drawing.Size(32, 32);
					this.picLoading.Parent = this.pnlForm;
				}
				else
				{
					frmViewerPartslist.HideLoadingDelegate hideLoadingDelegate = new frmViewerPartslist.HideLoadingDelegate(this.HideLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(hideLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void HidePartsList()
		{
			try
			{
				this.splitPnlGrids.Panel1.Hide();
				this.splitPnlGrids.Panel1Collapsed = true;
				this.dgPartslist.Visible = false;
				this.Column1.Visible = false;
				this.HidePartsListToolbar();
				if (this.splitPnlGrids.Panel1Collapsed && this.splitPnlGrids.Panel2Collapsed)
				{
					this.frmParent.HidePartsList();
				}
			}
			catch
			{
			}
		}

		private void HidePartsListToolbar()
		{
			if (this.dgJumps.InvokeRequired)
			{
				this.dgJumps.Invoke(new frmViewerPartslist.HidePartsListToolbarDelegate(this.HidePartsListToolbar));
				return;
			}
			try
			{
				this.toolStrip1.Visible = false;
			}
			catch
			{
			}
		}

		public void HighlightPartslist(string argName, string argValue)
		{
			this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			for (int i = 0; i < this.dgPartslist.Columns.Count; i++)
			{
				this.dgPartslist.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
			}
			this.dgPartslist.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
			string listAttributeName = this.GetListAttributeName(argName);
			this.dgPartslist.SelectionChanged -= new EventHandler(this.dgPartslist_SelectionChanged);
			this.dgPartslist.BeginEdit(true);
			try
			{
				for (int j = 0; j < this.dgPartslist.Rows.Count; j++)
				{
					bool readOnly = true;
					try
					{
						readOnly = this.dgPartslist.Rows[j].ReadOnly;
					}
					catch
					{
					}
					if (this.dgPartslist[listAttributeName, j].Value == null || !(this.dgPartslist[listAttributeName, j].Value.ToString() == argValue) || readOnly)
					{
						this.dgPartslist.Rows[j].Selected = false;
					}
					else
					{
						this.dgPartslist.Rows[j].Selected = true;
					}
				}
				if (this.dgPartslist.SelectedRows.Count > 0)
				{
					this.dgPartslist.FirstDisplayedScrollingRowIndex = this.dgPartslist.SelectedRows[this.dgPartslist.SelectedRows.Count - 1].Index;
				}
			}
			catch
			{
			}
			this.dgPartslist.EndEdit();
			this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmViewerPartslist));
			this.pnlForm = new Panel();
			this.pnlGrids = new Panel();
			this.splitPnlGrids = new SplitContainer();
			this.dgPartslist = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.cmsPartslist = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmAddPartMemo = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsmAddToSelectionList = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.tsmClearSelection = new ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
			this.commaSeparatedToolStripMenuItem = new ToolStripMenuItem();
			this.tabSeparatedToolStripMenuItem = new ToolStripMenuItem();
			this.exportToFileToolStripMenuItem = new ToolStripMenuItem();
			this.commaSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
			this.tabSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.nextListToolStripMenuItem = new ToolStripMenuItem();
			this.previousListToolStripMenuItem = new ToolStripMenuItem();
			this.rowSelectionModeToolStripMenuItem = new ToolStripMenuItem();
			this.dgJumps = new DataGridView();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.picLoading = new PictureBox();
			this.tabControl1 = new TabControl();
			this.pnlInfo = new Panel();
			this.lblPartsListInfo = new Label();
			this.toolStrip1 = new ToolStrip();
			this.tsBtnNext = new ToolStripButton();
			this.tsTxtList = new ToolStripTextBox();
			this.tsBtnPrev = new ToolStripButton();
			this.tsbClearSelection = new ToolStripButton();
			this.tsbSelectAll = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.tsbAddPartMemo = new ToolStripButton();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.tsbAddToSelectionList = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsbPartistInfo = new ToolStripButton();
			this.bgWorker = new BackgroundWorker();
			this.dlgSaveFile = new SaveFileDialog();
			this.cmsReference = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn2 = new DataGridViewCheckBoxColumn();
			this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
			this.pnlForm.SuspendLayout();
			this.pnlGrids.SuspendLayout();
			this.splitPnlGrids.Panel1.SuspendLayout();
			this.splitPnlGrids.Panel2.SuspendLayout();
			this.splitPnlGrids.SuspendLayout();
			((ISupportInitialize)this.dgPartslist).BeginInit();
			this.cmsPartslist.SuspendLayout();
			((ISupportInitialize)this.dgJumps).BeginInit();
			((ISupportInitialize)this.picLoading).BeginInit();
			this.pnlInfo.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlGrids);
			this.pnlForm.Controls.Add(this.pnlInfo);
			this.pnlForm.Controls.Add(this.toolStrip1);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(667, 366);
			this.pnlForm.TabIndex = 3;
			this.pnlGrids.Controls.Add(this.splitPnlGrids);
			this.pnlGrids.Controls.Add(this.picLoading);
			this.pnlGrids.Controls.Add(this.tabControl1);
			this.pnlGrids.Dock = DockStyle.Fill;
			this.pnlGrids.Location = new Point(0, 46);
			this.pnlGrids.Name = "pnlGrids";
			this.pnlGrids.Size = new System.Drawing.Size(665, 318);
			this.pnlGrids.TabIndex = 25;
			this.splitPnlGrids.BorderStyle = BorderStyle.FixedSingle;
			this.splitPnlGrids.Dock = DockStyle.Fill;
			this.splitPnlGrids.Location = new Point(0, 0);
			this.splitPnlGrids.Margin = new System.Windows.Forms.Padding(0);
			this.splitPnlGrids.Name = "splitPnlGrids";
			this.splitPnlGrids.Orientation = Orientation.Horizontal;
			this.splitPnlGrids.Panel1.Controls.Add(this.dgPartslist);
			this.splitPnlGrids.Panel2.Controls.Add(this.dgJumps);
			this.splitPnlGrids.Size = new System.Drawing.Size(665, 318);
			this.splitPnlGrids.SplitterDistance = 148;
			this.splitPnlGrids.SplitterWidth = 3;
			this.splitPnlGrids.TabIndex = 25;
			this.splitPnlGrids.SplitterMoved += new SplitterEventHandler(this.splitPnlGrids_SplitterMoved);
			this.dgPartslist.AllowUserToAddRows = false;
			this.dgPartslist.AllowUserToDeleteRows = false;
			this.dgPartslist.AllowUserToResizeRows = false;
			this.dgPartslist.BackgroundColor = Color.White;
			this.dgPartslist.BorderStyle = BorderStyle.None;
			this.dgPartslist.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
			this.dgPartslist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgPartslist.Columns.AddRange(new DataGridViewColumn[] { this.Column1 });
			this.dgPartslist.ContextMenuStrip = this.cmsPartslist;
			this.dgPartslist.Dock = DockStyle.Fill;
			this.dgPartslist.Location = new Point(0, 0);
			this.dgPartslist.Name = "dgPartslist";
			this.dgPartslist.RowHeadersVisible = false;
			this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgPartslist.Size = new System.Drawing.Size(663, 146);
			this.dgPartslist.TabIndex = 22;
			this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
			this.dgPartslist.CellMouseClick += new DataGridViewCellMouseEventHandler(this.dgPartslist_CellMouseClick);
			this.dgPartslist.SortCompare += new DataGridViewSortCompareEventHandler(this.dgPartslist_SortCompare);
			this.dgPartslist.Sorted += new EventHandler(this.dgPartslist_Sorted);
			this.dgPartslist.CellDoubleClick += new DataGridViewCellEventHandler(this.dgPartslist_CellDoubleClick);
			this.dgPartslist.MouseMove += new MouseEventHandler(this.dgPartslist_MouseMove);
			this.dgPartslist.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgPartslist_RowsAdded);
			this.dgPartslist.CellPainting += new DataGridViewCellPaintingEventHandler(this.dgPartslist_CellPainting);
			this.dgPartslist.CellClick += new DataGridViewCellEventHandler(this.dgPartslist_CellClick);
			this.dgPartslist.CurrentCellDirtyStateChanged += new EventHandler(this.dgPartslist_CurrentCellDirtyStateChanged);
			this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
			this.Column1.HeaderText = "PartsDetails";
			this.Column1.Name = "Column1";
			this.Column1.Width = 1000;
			ToolStripItemCollection items = this.cmsPartslist.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.tsmAddPartMemo, this.toolStripSeparator2, this.tsmAddToSelectionList, this.toolStripSeparator3, this.tsmClearSelection, this.selectAllToolStripMenuItem, this.toolStripSeparator6, this.copyToClipboardToolStripMenuItem, this.exportToFileToolStripMenuItem, this.toolStripSeparator7, this.nextListToolStripMenuItem, this.previousListToolStripMenuItem, this.rowSelectionModeToolStripMenuItem };
			items.AddRange(toolStripItemArray);
			this.cmsPartslist.Name = "cmsPartslist";
			this.cmsPartslist.Size = new System.Drawing.Size(186, 226);
			this.cmsPartslist.Opening += new CancelEventHandler(this.cmsPartslist_Opening);
			this.tsmAddPartMemo.Name = "tsmAddPartMemo";
			this.tsmAddPartMemo.Size = new System.Drawing.Size(185, 22);
			this.tsmAddPartMemo.Text = "Add Part Memo";
			this.tsmAddPartMemo.Click += new EventHandler(this.tsmAddPartMemo_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(182, 6);
			this.tsmAddToSelectionList.Name = "tsmAddToSelectionList";
			this.tsmAddToSelectionList.Size = new System.Drawing.Size(185, 22);
			this.tsmAddToSelectionList.Text = "Add To Selection List";
			this.tsmAddToSelectionList.Click += new EventHandler(this.tsmAddToSelectionList_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(182, 6);
			this.tsmClearSelection.Name = "tsmClearSelection";
			this.tsmClearSelection.Size = new System.Drawing.Size(185, 22);
			this.tsmClearSelection.Text = "Clear Selection";
			this.tsmClearSelection.Click += new EventHandler(this.tsmClearSelection_Click);
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new EventHandler(this.selectAllToolStripMenuItem_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(182, 6);
			ToolStripItemCollection dropDownItems = this.copyToClipboardToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.commaSeparatedToolStripMenuItem, this.tabSeparatedToolStripMenuItem };
			dropDownItems.AddRange(toolStripItemArray1);
			this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
			this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
			this.commaSeparatedToolStripMenuItem.Name = "commaSeparatedToolStripMenuItem";
			this.commaSeparatedToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.commaSeparatedToolStripMenuItem.Text = "Comma Separated";
			this.commaSeparatedToolStripMenuItem.Click += new EventHandler(this.commaSeparatedToolStripMenuItem_Click);
			this.tabSeparatedToolStripMenuItem.Name = "tabSeparatedToolStripMenuItem";
			this.tabSeparatedToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.tabSeparatedToolStripMenuItem.Text = "Tab Separated";
			this.tabSeparatedToolStripMenuItem.Click += new EventHandler(this.tabSeparatedToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections = this.exportToFileToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray2 = new ToolStripItem[] { this.commaSeparatedToolStripMenuItem1, this.tabSeparatedToolStripMenuItem1 };
			toolStripItemCollections.AddRange(toolStripItemArray2);
			this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
			this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.exportToFileToolStripMenuItem.Text = "Export To File";
			this.commaSeparatedToolStripMenuItem1.Name = "commaSeparatedToolStripMenuItem1";
			this.commaSeparatedToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.commaSeparatedToolStripMenuItem1.Text = "Comma Separated";
			this.commaSeparatedToolStripMenuItem1.Click += new EventHandler(this.commaSeparatedToolStripMenuItem1_Click);
			this.tabSeparatedToolStripMenuItem1.Name = "tabSeparatedToolStripMenuItem1";
			this.tabSeparatedToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.tabSeparatedToolStripMenuItem1.Text = "Tab Separated";
			this.tabSeparatedToolStripMenuItem1.Click += new EventHandler(this.tabSeparatedToolStripMenuItem1_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(182, 6);
			this.nextListToolStripMenuItem.Name = "nextListToolStripMenuItem";
			this.nextListToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.nextListToolStripMenuItem.Text = "Next List";
			this.nextListToolStripMenuItem.Click += new EventHandler(this.nextListToolStripMenuItem_Click);
			this.previousListToolStripMenuItem.Name = "previousListToolStripMenuItem";
			this.previousListToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.previousListToolStripMenuItem.Text = "Previous List";
			this.previousListToolStripMenuItem.Click += new EventHandler(this.previousListToolStripMenuItem_Click);
			this.rowSelectionModeToolStripMenuItem.Name = "rowSelectionModeToolStripMenuItem";
			this.rowSelectionModeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.rowSelectionModeToolStripMenuItem.Text = "RowSelectionMode";
			this.rowSelectionModeToolStripMenuItem.Click += new EventHandler(this.rowSelectionModeToolStripMenuItem_Click);
			this.dgJumps.AllowUserToAddRows = false;
			this.dgJumps.AllowUserToDeleteRows = false;
			this.dgJumps.AllowUserToResizeRows = false;
			this.dgJumps.BackgroundColor = Color.White;
			this.dgJumps.BorderStyle = BorderStyle.None;
			this.dgJumps.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgJumps.Columns.AddRange(new DataGridViewColumn[] { this.dataGridViewTextBoxColumn2 });
			this.dgJumps.Dock = DockStyle.Fill;
			this.dgJumps.Location = new Point(0, 0);
			this.dgJumps.Name = "dgJumps";
			this.dgJumps.RowHeadersVisible = false;
			this.dgJumps.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgJumps.Size = new System.Drawing.Size(663, 165);
			this.dgJumps.TabIndex = 23;
			this.dgJumps.CellDoubleClick += new DataGridViewCellEventHandler(this.dgJumps_CellDoubleClick);
			this.dataGridViewTextBoxColumn2.HeaderText = "Jumps";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Width = 1000;
			this.picLoading.BackColor = Color.White;
			this.picLoading.Dock = DockStyle.Fill;
			this.picLoading.Image = GSPcLocalViewer.Properties.Resources.Loading1;
			this.picLoading.Location = new Point(0, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(665, 318);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 19;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.tabControl1.Dock = DockStyle.Top;
			this.tabControl1.HotTrack = true;
			this.tabControl1.Location = new Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(665, 0);
			this.tabControl1.TabIndex = 24;
			this.pnlInfo.AutoSize = true;
			this.pnlInfo.BackColor = SystemColors.Control;
			this.pnlInfo.BorderStyle = BorderStyle.FixedSingle;
			this.pnlInfo.Controls.Add(this.lblPartsListInfo);
			this.pnlInfo.Dock = DockStyle.Top;
			this.pnlInfo.Location = new Point(0, 25);
			this.pnlInfo.Name = "pnlInfo";
			this.pnlInfo.Padding = new System.Windows.Forms.Padding(3);
			this.pnlInfo.Size = new System.Drawing.Size(665, 21);
			this.pnlInfo.TabIndex = 23;
			this.lblPartsListInfo.AutoSize = true;
			this.lblPartsListInfo.Dock = DockStyle.Fill;
			this.lblPartsListInfo.Location = new Point(3, 3);
			this.lblPartsListInfo.Name = "lblPartsListInfo";
			this.lblPartsListInfo.Size = new System.Drawing.Size(119, 13);
			this.lblPartsListInfo.TabIndex = 0;
			this.lblPartsListInfo.Text = "Parts List Info Message";
			this.toolStrip1.BackColor = SystemColors.Control;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			ToolStripItemCollection items1 = this.toolStrip1.Items;
			ToolStripItem[] toolStripItemArray3 = new ToolStripItem[] { this.tsBtnNext, this.tsTxtList, this.tsBtnPrev, this.tsbClearSelection, this.tsbSelectAll, this.toolStripSeparator4, this.tsbAddPartMemo, this.toolStripSeparator5, this.tsbAddToSelectionList, this.toolStripSeparator1, this.tsbPartistInfo };
			items1.AddRange(toolStripItemArray3);
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip1.Size = new System.Drawing.Size(665, 25);
			this.toolStrip1.TabIndex = 22;
			this.tsBtnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnNext.Image = GSPcLocalViewer.Properties.Resources.Nav_Next;
			this.tsBtnNext.ImageTransparentColor = Color.Magenta;
			this.tsBtnNext.Name = "tsBtnNext";
			this.tsBtnNext.Size = new System.Drawing.Size(23, 22);
			this.tsBtnNext.Text = "Next List";
			this.tsBtnNext.Click += new EventHandler(this.tsBtnNext_Click);
			this.tsTxtList.AutoSize = false;
			this.tsTxtList.BorderStyle = BorderStyle.FixedSingle;
			this.tsTxtList.Name = "tsTxtList";
			this.tsTxtList.ReadOnly = true;
			this.tsTxtList.ShortcutsEnabled = false;
			this.tsTxtList.Size = new System.Drawing.Size(50, 23);
			this.tsTxtList.TextBoxTextAlign = HorizontalAlignment.Center;
			this.tsBtnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnPrev.Image = GSPcLocalViewer.Properties.Resources.Nav_Prev;
			this.tsBtnPrev.ImageTransparentColor = Color.Magenta;
			this.tsBtnPrev.Name = "tsBtnPrev";
			this.tsBtnPrev.Size = new System.Drawing.Size(23, 22);
			this.tsBtnPrev.Text = "Previous List";
			this.tsBtnPrev.Click += new EventHandler(this.tsBtnPrev_Click);
			this.tsbClearSelection.Alignment = ToolStripItemAlignment.Right;
			this.tsbClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbClearSelection.Image = GSPcLocalViewer.Properties.Resources.PartsList_clear_selection;
			this.tsbClearSelection.ImageTransparentColor = Color.Magenta;
			this.tsbClearSelection.Name = "tsbClearSelection";
			this.tsbClearSelection.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbClearSelection.Size = new System.Drawing.Size(23, 22);
			this.tsbClearSelection.Text = "Clear Selection";
			this.tsbClearSelection.Click += new EventHandler(this.tsbClearSelection_Click);
			this.tsbSelectAll.Alignment = ToolStripItemAlignment.Right;
			this.tsbSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSelectAll.Image = (Image)componentResourceManager.GetObject("tsbSelectAll.Image");
			this.tsbSelectAll.ImageTransparentColor = Color.Magenta;
			this.tsbSelectAll.Name = "tsbSelectAll";
			this.tsbSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbSelectAll.Size = new System.Drawing.Size(23, 22);
			this.tsbSelectAll.Text = "Select All";
			this.tsbSelectAll.Click += new EventHandler(this.tsbSelectAll_Click);
			this.toolStripSeparator4.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			this.tsbAddPartMemo.Alignment = ToolStripItemAlignment.Right;
			this.tsbAddPartMemo.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbAddPartMemo.Enabled = false;
			this.tsbAddPartMemo.Image = GSPcLocalViewer.Properties.Resources.Add_Memo;
			this.tsbAddPartMemo.ImageTransparentColor = Color.Magenta;
			this.tsbAddPartMemo.Name = "tsbAddPartMemo";
			this.tsbAddPartMemo.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbAddPartMemo.Size = new System.Drawing.Size(23, 22);
			this.tsbAddPartMemo.Text = "Add Part Memo";
			this.tsbAddPartMemo.Click += new EventHandler(this.tsmAddPartMemo_Click);
			this.toolStripSeparator5.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			this.tsbAddToSelectionList.Alignment = ToolStripItemAlignment.Right;
			this.tsbAddToSelectionList.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbAddToSelectionList.Enabled = false;
			this.tsbAddToSelectionList.Image = (Image)componentResourceManager.GetObject("tsbAddToSelectionList.Image");
			this.tsbAddToSelectionList.ImageTransparentColor = Color.Magenta;
			this.tsbAddToSelectionList.Name = "tsbAddToSelectionList";
			this.tsbAddToSelectionList.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbAddToSelectionList.Size = new System.Drawing.Size(23, 22);
			this.tsbAddToSelectionList.Text = "Add To Selection List";
			this.tsbAddToSelectionList.Click += new EventHandler(this.tsbAddToSelectionList_Click);
			this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.tsbPartistInfo.Alignment = ToolStripItemAlignment.Right;
			this.tsbPartistInfo.CheckOnClick = true;
			this.tsbPartistInfo.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPartistInfo.Image = GSPcLocalViewer.Properties.Resources.PartsList_Info;
			this.tsbPartistInfo.ImageTransparentColor = Color.Magenta;
			this.tsbPartistInfo.Name = "tsbPartistInfo";
			this.tsbPartistInfo.Size = new System.Drawing.Size(23, 22);
			this.tsbPartistInfo.Click += new EventHandler(this.tsbPartistInfo_Click);
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			this.cmsReference.Name = "cmsReference";
			this.cmsReference.Size = new System.Drawing.Size(61, 4);
			this.cmsReference.ItemClicked += new ToolStripItemClickedEventHandler(this.cmsReference_ItemClicked);
			this.dataGridViewTextBoxColumn1.HeaderText = "PartsDetails";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Width = 1000;
			this.dataGridViewCheckBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewCheckBoxColumn2.Frozen = true;
			this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
			this.dataGridViewCheckBoxColumn2.ReadOnly = true;
			this.dataGridViewTextBoxColumn13.HeaderText = "temp";
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(667, 366);
			base.Controls.Add(this.pnlForm);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.HideOnClose = true;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "frmViewerPartslist";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Partslist";
			base.Load += new EventHandler(this.frmViewerPartslist_Load);
			base.VisibleChanged += new EventHandler(this.frmViewerPartslist_VisibleChanged);
			base.FormClosing += new FormClosingEventHandler(this.frmViewerPartslist_FormClosing);
			this.pnlForm.ResumeLayout(false);
			this.pnlForm.PerformLayout();
			this.pnlGrids.ResumeLayout(false);
			this.splitPnlGrids.Panel1.ResumeLayout(false);
			this.splitPnlGrids.Panel2.ResumeLayout(false);
			this.splitPnlGrids.ResumeLayout(false);
			((ISupportInitialize)this.dgPartslist).EndInit();
			this.cmsPartslist.ResumeLayout(false);
			((ISupportInitialize)this.dgJumps).EndInit();
			((ISupportInitialize)this.picLoading).EndInit();
			this.pnlInfo.ResumeLayout(false);
			this.pnlInfo.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
		}

		private void InitializeJumpsList()
		{
			if (!this.dgJumps.InvokeRequired)
			{
				try
				{
					this.addJumpColumns();
				}
				catch
				{
				}
				return;
			}
			this.dgJumps.Invoke(new frmViewerPartslist.InitializeJumpsListDelegate(this.InitializeJumpsList));
		}

		private void InitializePartsList(XmlNode schemaNode)
		{
			if (this.dgPartslist.InvokeRequired)
			{
				DataGridView dataGridView = this.dgPartslist;
				frmViewerPartslist.InitializePartsListDelegate initializePartsListDelegate = new frmViewerPartslist.InitializePartsListDelegate(this.InitializePartsList);
				object[] objArray = new object[] { schemaNode };
				dataGridView.Invoke(initializePartsListDelegate, objArray);
				return;
			}
			try
			{
				this.dgPartslist.AllowUserToAddRows = true;
				this.dgPartslist.CurrentCell = this.dgPartslist[0, 0];
				this.dgPartslist.BeginEdit(true);
				this.dgPartslist.Columns.Clear();
				this.dgPartslist.DefaultCellStyle.NullValue = null;
				this.AddCheckBoxColumn();
				this.AddSchemaColumns(schemaNode);
				this.ReadMandatoryAttribKeysFromSchema(schemaNode);
				this.AddMemoColumns();
				this.Set_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
				this.MapSpecialColumns(schemaNode);
				this.MapDefaultMemoColumn(schemaNode);
				this.Set_MemoCols_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
				this.SetGridHeaderText();
				this.addSortingColumn();
				this.addSelectionListKeyCol();
			}
			catch
			{
			}
		}

		private bool IntervalElapsed(DateTime dtLocal, DateTime dtServer)
		{
			bool flag;
			try
			{
				int num = int.Parse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"]);
				flag = ((dtServer.Date - dtLocal.Date).TotalDays < (double)num ? false : true);
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		private void LoadJumpsInGrid(XmlDocument objXmlDoc)
		{
			if (this.dgJumps.InvokeRequired)
			{
				DataGridView dataGridView = this.dgJumps;
				frmViewerPartslist.LoadJumpsInGridDelegate loadJumpsInGridDelegate = new frmViewerPartslist.LoadJumpsInGridDelegate(this.LoadJumpsInGrid);
				object[] objArray = new object[] { objXmlDoc };
				dataGridView.Invoke(loadJumpsInGridDelegate, objArray);
				return;
			}
			List<string> strs = new List<string>();
			this.dgJumps.AllowUserToAddRows = true;
			this.dgJumps.CurrentCell = this.dgJumps[0, 0];
			this.dgJumps.BeginEdit(true);
			try
			{
				this.dgJumps.Rows.Clear();
				this.dgJumps.AllowUserToAddRows = false;
				XmlNodeList xmlNodeLists = objXmlDoc.SelectNodes("//Jumps/Jump");
				if (xmlNodeLists.Count != 0)
				{
					strs = this.GetFilteredBooks();
					foreach (XmlNode xmlNodes in xmlNodeLists)
					{
						string empty = string.Empty;
						string value = string.Empty;
						string str = string.Empty;
						try
						{
							if (xmlNodes.Attributes["DpTxt"] != null)
							{
								empty = xmlNodes.Attributes["DpTxt"].Value;
							}
							if (xmlNodes.Attributes["PgName"] != null)
							{
								str = xmlNodes.Attributes["PgName"].Value;
							}
							if (empty.StartsWith("\"") && empty.EndsWith("\""))
							{
								empty = empty.Trim(new char[] { '\"' });
							}
							if (str.StartsWith("\"") && str.EndsWith("\""))
							{
								str = str.Trim(new char[] { '\"' });
							}
							if (xmlNodes.Attributes["BkId"] != null)
							{
								value = xmlNodes.Attributes["BkId"].Value;
							}
							if (value == string.Empty)
							{
								value = this.BookPublishingId;
							}
						}
						catch
						{
						}
						if (xmlNodes.Attributes["Type"].Value.ToUpper().Trim() != "PAGEJUMP")
						{
							try
							{
								if (strs.Contains(value))
								{
									this.dgJumps.Rows.Add();
									this.dgJumps["arrow", this.dgJumps.Rows.Count - 1].Value = GSPcLocalViewer.Properties.Resources.arrow;
									this.dgJumps["TITLE", this.dgJumps.Rows.Count - 1].Value = empty;
									this.dgJumps["KEY", this.dgJumps.Rows.Count - 1].Value = xmlNodes.Attributes["LinkNum"].Value;
									DataGridViewCell item = this.dgJumps["JUMPSTRING", this.dgJumps.Rows.Count - 1];
									string[] strArrays = new string[] { value, "|", xmlNodes.Attributes["PgName"].Value, "|", xmlNodes.Attributes["PartNum"].Value, "|", xmlNodes.Attributes["Type"].Value };
									item.Value = string.Concat(strArrays);
								}
							}
							catch
							{
							}
						}
						else
						{
							if (this.frmParent.lstFilteredPages.Contains(str))
							{
								continue;
							}
							this.dgJumps.Rows.Add();
							this.dgJumps["arrow", this.dgJumps.Rows.Count - 1].Value = GSPcLocalViewer.Properties.Resources.arrow;
							this.dgJumps["TITLE", this.dgJumps.Rows.Count - 1].Value = empty;
							this.dgJumps["KEY", this.dgJumps.Rows.Count - 1].Value = xmlNodes.Attributes["LinkNum"].Value;
							DataGridViewCell dataGridViewCell = this.dgJumps["JUMPSTRING", this.dgJumps.Rows.Count - 1];
							string[] value1 = new string[] { value, "|", str, "|", xmlNodes.Attributes["PartNum"].Value, "|", xmlNodes.Attributes["Type"].Value };
							dataGridViewCell.Value = string.Concat(value1);
						}
					}
				}
			}
			catch
			{
			}
			this.dgJumps.AllowUserToResizeColumns = true;
			this.dgJumps.EndEdit();
			this.dgJumps.Visible = true;
		}

		private ArrayList LoadMemoTypeKeys()
		{
			ArrayList arrayLists = new ArrayList();
			arrayLists.Add("MEM");
			arrayLists.Add("LOCMEM");
			arrayLists.Add("GBLMEM");
			arrayLists.Add("ADMMEM");
			arrayLists.Add("TXTMEM");
			arrayLists.Add("REFMEM");
			arrayLists.Add("HYPMEM");
			arrayLists.Add("PRGMEM");
			arrayLists.Add("LOCTXTMEM");
			arrayLists.Add("LOCREFMEM");
			arrayLists.Add("LOCHYPMEM");
			arrayLists.Add("LOCPRGMEM");
			arrayLists.Add("GBLTXTMEM");
			arrayLists.Add("GBLREFMEM");
			arrayLists.Add("GBLHYPMEM");
			arrayLists.Add("GBLPRGMEM");
			arrayLists.Add("ADMTXTMEM");
			arrayLists.Add("ADMREFMEM");
			arrayLists.Add("ADMHYPMEM");
			arrayLists.Add("ADMPRGMEM");
			return arrayLists;
		}

		public void LoadPartsList(XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
		{
			this.dgPartslist.Visible = true;
			this.picLoading.Visible = true;
			this.ShowLoading(this.pnlForm);
			foreach (XmlAttribute attribute in schemaNode.Attributes)
			{
				if (!attribute.Value.ToUpper().Equals("PARTSLISTTITLE"))
				{
					continue;
				}
				this.sPLTitle = attribute.Name;
				break;
			}
			this.dgPartslist.SelectionChanged -= new EventHandler(this.dgPartslist_SelectionChanged);
			this.UpdateCurrentPageForPartslist(true, schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
			if (!this.isWorking)
			{
				this.isWorking = true;
				XmlDocument xmlDocument = null;
				this.objXmlNodeList = null;
				try
				{
					if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] == null)
					{
						this.p_Encrypted = false;
					}
					else if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() != "ON")
					{
						this.p_Encrypted = false;
					}
					else
					{
						this.p_Encrypted = true;
					}
					if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] == null)
					{
						this.p_Compressed = false;
					}
					else if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() != "ON")
					{
						this.p_Compressed = false;
					}
					else
					{
						this.p_Compressed = true;
					}
					xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(this.curPageNode.OuterXml);
					try
					{
						string[] strArrays = new string[] { "//Pic[not(@", this.attPicElement, " = preceding-sibling::Pic/@", this.attPicElement, ")]" };
						string value = xmlDocument.SelectNodes(string.Concat(strArrays))[this.curPicIndex].Attributes[this.attPicElement].Value;
						this.curPictureFileName = value;
						if (value.Trim() == string.Empty)
						{
							throw new Exception();
						}
						string[] strArrays1 = new string[] { "//Pic[@", this.attPicElement, "='", value, "' and @", this.attListElement, "]" };
						this.objXmlNodeList = xmlDocument.SelectNodes(string.Concat(strArrays1));
					}
					catch
					{
						xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(this.curPageNode.OuterXml);
						string[] strArrays2 = new string[] { "//Pic[(not(@", this.attPicElement, ") or @", this.attPicElement, "='') and @", this.attListElement, "]" };
						this.objXmlNodeList = xmlDocument.SelectNodes(string.Concat(strArrays2));
					}
				}
				catch
				{
					this.frmParent.HidePartsList();
					MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM001) Failed to load specified object", "(E-VPL-EM001)_FAILED", ResourceType.POPUP_MESSAGE));
				}
				if (this.objXmlNodeList != null && this.objXmlNodeList.Count > 0)
				{
					this.frmParent.EnablePartslistShowHideButton(true);
					if (!this.frmParent.bPartsListClosed)
					{
						this.frmParent.ShowPartsList();
					}
					BackgroundWorker backgroundWorker = this.bgWorker;
					object[] objArray = new object[] { this.curPageSchema, this.curPageNode, this.curPicIndex, this.curListIndex, this.objXmlNodeList };
					backgroundWorker.RunWorkerAsync(objArray);
					return;
				}
				this.curListSchema = null;
				this.frmParent.AddToHistory();
				this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
				this.UpdateStatus();
				this.frmParent.HidePartsList();
				this.isWorking = false;
			}
		}

		private void LoadPartsListInGrid(XmlDocument objXmlDoc)
		{
			XmlNodeList xmlNodeLists;
			if (this.dgPartslist.InvokeRequired)
			{
				DataGridView dataGridView = this.dgPartslist;
				frmViewerPartslist.LoadPartsListInGridDelegate loadPartsListInGridDelegate = new frmViewerPartslist.LoadPartsListInGridDelegate(this.LoadPartsListInGrid);
				object[] objArray = new object[] { objXmlDoc };
				dataGridView.Invoke(loadPartsListInGridDelegate, objArray);
				return;
			}
			this.lstDisableRows = new List<int>();
			this.tsbAddToSelectionList.Enabled = false;
			this.dgPartslist.AllowUserToAddRows = true;
			this.dgPartslist.CurrentCell = this.dgPartslist[0, 0];
			this.dgPartslist.CellValueChanged -= new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
			this.dgPartslist.BeginEdit(true);
			((DatagridViewCheckBoxHeaderCell)this.dgPartslist.Columns[0].HeaderCell).Checked = true;
			this.dgPartslist.Rows.Clear();
			this.dgPartslist.AllowUserToAddRows = false;
			if (!this.attPictureFileElement.Equals(string.Empty))
			{
				string[] strArrays = new string[] { "//Parts/Part[@", this.attPictureFileElement, " = '", this.curPictureFileName, "']" };
				xmlNodeLists = objXmlDoc.SelectNodes(string.Concat(strArrays));
				if (xmlNodeLists.Count == 0)
				{
					xmlNodeLists = objXmlDoc.SelectNodes("//Parts/Part");
				}
			}
			else
			{
				xmlNodeLists = objXmlDoc.SelectNodes("//Parts/Part");
			}
			xmlNodeLists = this.frmParent.FilterPartsList(this.curListSchema, xmlNodeLists);
			try
			{
				foreach (XmlNode value in xmlNodeLists)
				{
					bool flag = false;
					bool flag1 = false;
					try
					{
						if (value.Attributes[this.attPartNoElement] != null && value.Attributes[this.attPartNoElement].Value != string.Empty)
						{
							flag = true;
						}
					}
					catch
					{
					}
					try
					{
						if (value.Attributes[this.attPartNameElement] != null)
						{
							flag1 = true;
						}
					}
					catch
					{
					}
					if (value.Attributes.Count <= 1 || !flag1 && !flag)
					{
						continue;
					}
					this.dgPartslist.Rows.Add();
					int num = 0;
					string empty = string.Empty;
					for (int i = 0; i < this.attAdminMemList.Count; i++)
					{
						if (value.Attributes[this.attAdminMemList[i].ToString()] != null)
						{
							empty = string.Concat(empty, value.Attributes[this.attAdminMemList[i].ToString()].Value, "**");
						}
					}
					try
					{
						foreach (DataGridViewColumn column in this.dgPartslist.Columns)
						{
							if (column.Name != "REF")
							{
								continue;
							}
							string str = this.FindAttributeKey(column.Tag.ToString());
							if (!(str != string.Empty) || value.Attributes[str] == null)
							{
								continue;
							}
							string value1 = value.Attributes[str].Value;
							if (value1 == null)
							{
								continue;
							}
							this.dgPartslist[column.Index, this.dgPartslist.Rows.Count - 1].Value = GSPcLocalViewer.Properties.Resources.Reference;
							this.dgPartslist[column.Index, this.dgPartslist.Rows.Count - 1].ToolTipText = value1;
						}
					}
					catch
					{
					}
					try
					{
						if (this.dgPartslist.Columns.Contains("INF"))
						{
							string str1 = this.FindAttributeKey(this.dgPartslist.Columns["REF"].Tag.ToString());
							if (str1 != string.Empty && value.Attributes[str1] != null)
							{
								string value2 = value.Attributes[str1].Value;
								if (value2 != null)
								{
									this.dgPartslist["INF", this.dgPartslist.Rows.Count - 1].Value = GSPcLocalViewer.Properties.Resources.info;
									this.dgPartslist["INF", this.dgPartslist.Rows.Count - 1].ToolTipText = value2;
								}
							}
						}
					}
					catch
					{
					}
					try
					{
						if (this.dgPartslist.Columns.Contains("QTY"))
						{
							string str2 = this.FindAttributeKey(this.dgPartslist.Columns["QTY"].Tag.ToString());
							if (value.Attributes[str2] != null)
							{
								this.dgPartslist["QTY", this.dgPartslist.Rows.Count - 1].Value = value.Attributes[str2].Value;
							}
						}
					}
					catch
					{
					}
					for (int j = 0; j < value.Attributes.Count; j++)
					{
						if (!this.frmParent.IsDisposed)
						{
							try
							{
								if (this.dgPartslist.Columns.Contains(value.Attributes[j].Name))
								{
									this.dgPartslist[value.Attributes[j].Name, this.dgPartslist.Rows.Count - 1].Value = value.Attributes[j].Value;
									if (this.dgPartslist.Columns[value.Attributes[j].Name].Visible)
									{
										num++;
									}
								}
							}
							catch
							{
							}
							Application.DoEvents();
						}
					}
					if (value.Attributes[this.attPartNoElement] != null)
					{
						this.frmParent.PartMemoExists(value.Attributes[this.attPartNoElement].Value, empty, this.dgPartslist.Rows.Count - 1);
					}
					if (this.dgPartslist.Columns.Count > 0)
					{
						try
						{
							bool flag2 = false;
							if (!string.IsNullOrEmpty(this.attPartStatusElement))
							{
								flag2 = (this.dgPartslist[this.attPartStatusElement, this.dgPartslist.Rows.Count - 1].Value == null ? false : this.dgPartslist[this.attPartStatusElement, this.dgPartslist.Rows.Count - 1].Value.ToString() == "0");
							}
							object value3 = null;
							object value4 = null;
							value3 = this.dgPartslist[this.attPartNoElement, this.dgPartslist.Rows.Count - 1].Value;
							value4 = this.dgPartslist[this.attPartNameElement, this.dgPartslist.Rows.Count - 1].Value;
							if (this.bSelectionMode)
							{
								StringBuilder stringBuilder = new StringBuilder(this.BookPublishingId);
								stringBuilder.Append("^");
								stringBuilder.Append(this.frmParent.objFrmPicture.CurrentPageId.ToString());
								stringBuilder.Append("^");
								stringBuilder.Append(this.curListIndex.ToString());
								stringBuilder.Append("^");
								stringBuilder.Append(this.dgPartslist.Rows.Count - 1);
								stringBuilder.Append("^");
								stringBuilder.Append(value3);
								value3 = stringBuilder;
							}
							if (value3 != null && value3.ToString() != string.Empty)
							{
								this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.Rows.Count - 1].Value = value3;
							}
							else if (value4 != null)
							{
								this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.Rows.Count - 1].Value = value4;
							}
							if (!flag2)
							{
								if (!this.frmParent.PartInSelectionList(this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.Rows.Count - 1].Value.ToString()))
								{
									this.dgPartslist[0, this.dgPartslist.Rows.Count - 1].Value = false;
									((DatagridViewCheckBoxHeaderCell)this.dgPartslist.Columns[0].HeaderCell).Checked = false;
								}
								else
								{
									this.dgPartslist[0, this.dgPartslist.Rows.Count - 1].Value = true;
								}
							}
						}
						catch
						{
							this.dgPartslist[0, this.dgPartslist.Rows.Count - 1].Value = false;
							((DatagridViewCheckBoxHeaderCell)this.dgPartslist.Columns[0].HeaderCell).Checked = true;
						}
					}
					if (num != 0)
					{
						continue;
					}
					this.HidePartsList();
					return;
				}
				if (this.dgPartslist.Rows.Count != 0)
				{
					this.ShowPartsList();
				}
				else
				{
					this.HidePartsList();
				}
			}
			catch
			{
				this.HidePartsList();
			}
			this.dgPartslist.Dock = DockStyle.None;
			this.dgPartslist.Dock = DockStyle.Fill;
			this.dgPartslist.AllowUserToResizeColumns = true;
			try
			{
				for (int k = 0; k < this.dgPartslist.Rows.Count; k++)
				{
					if (!this.frmParent.PartInSelectionListA(this.dgPartslist["PART_SLIST_KEY", k].Value.ToString()))
					{
						this.dgPartslist[0, k].Value = false;
					}
					else
					{
						this.dgPartslist[0, k].Value = true;
					}
					try
					{
						if (this.dgPartslist[this.attPartStatusElement, k].Value != null && this.dgPartslist[this.attPartStatusElement, k].Value.ToString() == "0")
						{
							this.lstDisableRows.Add(k);
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			this.dgPartslist.EndEdit();
			this.dgPartslist.CellValueChanged += new DataGridViewCellEventHandler(this.dgPartslist_CellValueChanged);
			frmViewerPartslist.ChangeBuffer(this.dgPartslist);
		}

		public void LoadResources()
		{
			this.tsbAddToSelectionList.Text = this.GetResource("Add to Selection List", "ADD_TO_SELECTION_LIST", ResourceType.TOOLSTRIP);
			this.tsbAddPartMemo.Text = this.GetResource("Add Part Memo", "ADD_PART_MEMO", ResourceType.TOOLSTRIP);
			this.tsbSelectAll.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.TOOLSTRIP);
			this.tsbClearSelection.Text = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.TOOLSTRIP);
			this.tsBtnPrev.Text = this.GetResource("Previous List", "PREVIOUS_LIST", ResourceType.TOOLSTRIP);
			this.tsBtnNext.Text = this.GetResource("Next List", "NEXT_LIST", ResourceType.TOOLSTRIP);
			this.tsmAddPartMemo.Text = this.GetResource("Add Part Memo", "ADD_PART_MEMO", ResourceType.CONTEXT_MENU);
			this.tsmAddToSelectionList.Text = this.GetResource("Add To Selection List", "ADD_TO_SELECTION_LIST", ResourceType.CONTEXT_MENU);
			this.tsmClearSelection.Text = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.CONTEXT_MENU);
			this.selectAllToolStripMenuItem.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.CONTEXT_MENU);
			this.copyToClipboardToolStripMenuItem.Text = this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.CONTEXT_MENU);
			this.commaSeparatedToolStripMenuItem.Text = this.GetResource("Comma Separated", "COMMA_SEPERATED", ResourceType.CONTEXT_MENU);
			this.tabSeparatedToolStripMenuItem.Text = this.GetResource("Tab Seperated", "TAB_SEPERATED", ResourceType.CONTEXT_MENU);
			this.exportToFileToolStripMenuItem.Text = this.GetResource("Export To File", "EXPORT_TO_FILE", ResourceType.CONTEXT_MENU);
			this.commaSeparatedToolStripMenuItem1.Text = this.GetResource("Comma Separated", "COMMA_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
			this.tabSeparatedToolStripMenuItem1.Text = this.GetResource("Tab Seperated", "TAB_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
			this.nextListToolStripMenuItem.Text = this.GetResource("Next List", "NEXT_LIST", ResourceType.CONTEXT_MENU);
			this.previousListToolStripMenuItem.Text = this.GetResource("Previous List", "PREVIOUS_LIST", ResourceType.CONTEXT_MENU);
			this.tsbPartistInfo.Text = this.GetResource("Information", "INFORMATION", ResourceType.CONTEXT_MENU);
			this.rowSelectionModeToolStripMenuItem.Text = this.GetResource("Row Selection Mode", "Row_Selection_Mode", ResourceType.CONTEXT_MENU);
		}

		private void MapDefaultMemoColumn(XmlNode schemaNode)
		{
			try
			{
				string empty = string.Empty;
				string str = string.Empty;
				string item = string.Empty;
				item = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", "MEM"];
				if (item != null && item != string.Empty)
				{
					string[] strArrays = new string[] { "|" };
					string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					string str1 = strArrays1[0];
					if (this.dgPartslist.Columns.Contains("Mem"))
					{
						DataGridViewColumn dataGridViewColumn = this.dgPartslist.Columns["Mem"];
						if ((int)strArrays1.Length > 3 && strArrays1[3] != null && strArrays1[3] != string.Empty)
						{
							string str2 = strArrays1[3];
							string[] strArrays2 = new string[] { "," };
							string[] strArrays3 = str2.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
							for (int i = 0; i < (int)strArrays3.Length; i++)
							{
								foreach (XmlAttribute attribute in schemaNode.Attributes)
								{
									if (attribute.Value.ToUpper() != strArrays3[i].ToUpper())
									{
										continue;
									}
									this.attAdminMemList.Add(attribute.Name);
									break;
								}
								if (dataGridViewColumn.Tag.ToString().ToUpper() == "MEM")
								{
									dataGridViewColumn.Tag = string.Empty;
								}
								DataGridViewColumn dataGridViewColumn1 = dataGridViewColumn;
								dataGridViewColumn1.Tag = string.Concat(dataGridViewColumn1.Tag, strArrays3[i]);
								if (i < (int)strArrays3.Length - 1)
								{
									DataGridViewColumn dataGridViewColumn2 = dataGridViewColumn;
									dataGridViewColumn2.Tag = string.Concat(dataGridViewColumn2.Tag, ",");
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		private void MapSpecialColumns(XmlNode schemaNode)
		{
			IniFileIO iniFileIO = new IniFileIO();
			ArrayList arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PLIST_SETTINGS");
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			string str1 = string.Empty;
			string empty2 = string.Empty;
			int num = 0;
			string str2 = string.Empty;
			string empty3 = string.Empty;
			int num1 = 0;
			List<SpecialRefColumn> specialRefColumns = new List<SpecialRefColumn>();
			for (int i = 0; i < arrayLists.Count; i++)
			{
				try
				{
					empty = string.Empty;
					empty = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", arrayLists[i].ToString().ToUpper()];
					string[] strArrays = new string[] { "|" };
					string[] strArrays1 = empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					if ((int)strArrays1.Length == 4 || (int)strArrays1.Length == 8)
					{
						if (strArrays1[3].ToUpper() == "REF")
						{
							SpecialRefColumn specialRefColumn = new SpecialRefColumn()
							{
								sRefKey = arrayLists[i].ToString(),
								sRefValue = strArrays1[0]
							};
							try
							{
								specialRefColumn.iRefWidth = int.Parse(strArrays1[2].ToString());
							}
							catch
							{
								specialRefColumn.iRefWidth = 0;
							}
							specialRefColumns.Add(specialRefColumn);
						}
						if (strArrays1[3].ToUpper() == "INF")
						{
							str2 = arrayLists[i].ToString();
							empty3 = strArrays1[0];
							try
							{
								num1 = int.Parse(strArrays1[2].ToString());
							}
							catch
							{
								num1 = 0;
							}
						}
						if (strArrays1[3].ToUpper() == "QTY")
						{
							empty2 = strArrays1[0];
							str1 = arrayLists[i].ToString();
							try
							{
								num = int.Parse(strArrays1[2].ToString());
							}
							catch
							{
								num = 0;
							}
						}
					}
				}
				catch
				{
				}
			}
			try
			{
				DataGridViewImageColumn dataGridViewImageColumn = null;
				DataGridViewImageColumn alignment = null;
				for (int j = 0; j < this.dgPartslist.Columns.Count; j++)
				{
					if (this.dgPartslist.Columns[j].Tag != null)
					{
						if (this.dgPartslist.Columns[j].Tag.ToString().ToUpper() == str1.ToUpper())
						{
							this.dgPartslist.Columns[j].HeaderText = empty2;
							this.dgPartslist.Columns[j].Name = "QTY";
							if (num > 0)
							{
								this.dgPartslist.Columns[j].Visible = true;
							}
						}
						if (this.dgPartslist.Columns[j].Tag.ToString().ToUpper() == str2.ToUpper())
						{
							this.dgPartslist.Columns[j].HeaderText = empty3;
							this.dgPartslist.Columns[j].Name = "INF1";
							dataGridViewImageColumn = new DataGridViewImageColumn()
							{
								Name = "INF",
								Tag = str2,
								Visible = true,
								HeaderText = empty3
							};
							dataGridViewImageColumn.DefaultCellStyle.NullValue = null;
							dataGridViewImageColumn.DefaultCellStyle.Alignment = this.dgPartslist.Columns[j].DefaultCellStyle.Alignment;
							dataGridViewImageColumn.HeaderCell.Style.Alignment = dataGridViewImageColumn.DefaultCellStyle.Alignment;
							if (num1 > 0)
							{
								this.dgPartslist.Columns[j].Visible = true;
							}
						}
						SpecialRefColumn specialRefColumn1 = SpecialRefColumn.CheckRefKeyExist(specialRefColumns, this.dgPartslist.Columns[j].Tag.ToString().ToUpper());
						if (specialRefColumn1 != null)
						{
							this.dgPartslist.Columns[j].HeaderText = specialRefColumn1.sRefValue;
							this.dgPartslist.Columns[j].Name = "REF1";
							alignment = new DataGridViewImageColumn()
							{
								Name = "REF",
								Tag = specialRefColumn1.sRefKey,
								Visible = true,
								HeaderText = specialRefColumn1.sRefValue
							};
							alignment.DefaultCellStyle.NullValue = null;
							alignment.DefaultCellStyle.Alignment = this.dgPartslist.Columns[j].DefaultCellStyle.Alignment;
							alignment.HeaderCell.Style.Alignment = alignment.DefaultCellStyle.Alignment;
							if (specialRefColumn1.iRefWidth > 0)
							{
								this.dgPartslist.Columns[j].Visible = true;
							}
							int displayIndex = this.dgPartslist.Columns["REF1"].DisplayIndex;
							this.dgPartslist.Columns.Remove("REF1");
							alignment.DisplayIndex = displayIndex;
							this.dgPartslist.Columns.Add(alignment);
						}
					}
				}
				if (dataGridViewImageColumn != null)
				{
					int displayIndex1 = this.dgPartslist.Columns["INF1"].DisplayIndex;
					this.dgPartslist.Columns.Remove("INF1");
					dataGridViewImageColumn.DisplayIndex = displayIndex1;
					this.dgPartslist.Columns.Add(dataGridViewImageColumn);
				}
			}
			catch
			{
			}
		}

		private void nextListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsBtnNext_Click(null, null);
		}

		public void OnOffFeatures()
		{
			try
			{
				this.tsmAddPartMemo.Visible = Program.objAppFeatures.bMemo;
				this.toolStripSeparator2.Visible = Program.objAppFeatures.bMemo;
			}
			catch
			{
			}
		}

		private void OpenURLInBrowser(string sSeed)
		{
			try
			{
				if (sSeed != string.Empty && sSeed != null)
				{
					string item = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "REF_URL"];
					if (!(item != string.Empty) || item == null)
					{
						MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						item = string.Concat(item, sSeed);
						string str = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
						if (str == string.Empty || str == null)
						{
							str = "iexplore";
						}
						string empty = string.Empty;
						RegistryReader registryReader = new RegistryReader();
						empty = registryReader.Read(string.Concat("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", str, ".exe"), string.Empty);
						if (empty != string.Empty && empty != null)
						{
							if (!(item != string.Empty) || item == null)
							{
								MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
							else
							{
								using (Process process = Process.Start(empty, item))
								{
									if (process != null)
									{
										IntPtr handle = process.Handle;
										frmViewerPartslist.SetForegroundWindow(process.Handle);
										frmViewerPartslist.SetActiveWindow(process.Handle);
									}
									process.WaitForExit();
								}
							}
						}
						else if (registryReader.Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty) != null)
						{
							using (Process process1 = Process.Start(empty, item))
							{
								if (process1 != null)
								{
									IntPtr intPtr = process1.Handle;
									frmViewerPartslist.SetForegroundWindow(process1.Handle);
									frmViewerPartslist.SetActiveWindow(process1.Handle);
								}
								process1.WaitForExit();
							}
						}
						else
						{
							using (Process process2 = Process.Start(empty, item))
							{
								if (process2 != null)
								{
									IntPtr handle1 = process2.Handle;
									frmViewerPartslist.SetForegroundWindow(process2.Handle);
									frmViewerPartslist.SetActiveWindow(process2.Handle);
								}
								process2.WaitForExit();
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		private void PopUpMemo(int iColumnIndex, int iRowIndex)
		{
			try
			{
				if (Program.objAppFeatures.bMemo)
				{
					string empty = string.Empty;
					string upper = this.dgPartslist.Columns[iColumnIndex].Name.ToUpper();
					string str = upper;
					if (upper != null)
					{
						switch (str)
						{
							case "MEM":
							case "LOCMEM":
							case "GBLMEM":
							case "ADMMEM":
							case "TXTMEM":
							case "REFMEM":
							case "HYPMEM":
							case "PRGMEM":
							case "LOCTXTMEM":
							case "LOCREFMEM":
							case "LOCHYPMEM":
							case "LOCPRGMEM":
							case "GBLTXTMEM":
							case "GBLREFMEM":
							case "GBLHYPMEM":
							case "GBLPRGMEM":
							case "ADMTXTMEM":
							case "ADMREFMEM":
							case "ADMHYPMEM":
							case "ADMPRGMEM":
							{
								if (this.dgPartslist[iColumnIndex, iRowIndex] == null || iRowIndex < 0)
								{
									goto yoyo1;
								}
								string empty1 = string.Empty;
								try
								{
									for (int i = 0; i < this.attAdminMemList.Count; i++)
									{
										if (this.dgPartslist.Columns.Contains(this.attAdminMemList[i].ToString()) && this.dgPartslist[this.attAdminMemList[i].ToString(), iRowIndex] != null)
										{
											empty1 = string.Concat(empty1, this.dgPartslist[this.attAdminMemList[i].ToString(), iRowIndex].Value, "**");
										}
									}
								}
								catch
								{
								}
								try
								{
									if (this.dgPartslist[iColumnIndex, iRowIndex].Value != null)
									{
										string str1 = string.Empty;
										try
										{
											if (this.dgPartslist[this.attListUpdateDateElement, iRowIndex] != null)
											{
												str1 = this.dgPartslist[this.attListUpdateDateElement, iRowIndex].Value.ToString();
											}
										}
										catch
										{
											if (str1 == null || str1 == "" || str1.Length == 0)
											{
												string str2 = DateTime.Now.ToString("M/d/yyyy");
												str1 = str2.Replace('-', '/');
											}
										}
										try
										{
											empty = this.dgPartslist.Columns[iColumnIndex].Name.ToUpper();
											if (empty.Contains("LOC"))
											{
												empty = "LOC";
											}
											else if (!empty.Contains("ADM"))
											{
												empty = (!empty.Contains("GBL") ? string.Empty : "GBL");
											}
											else
											{
												empty = "ADM";
											}
										}
										catch
										{
										}
										this.frmParent.ShowPartMemos(this.CurrentPartNumber, empty1, str1, empty);
										this.frmParent.PartMemoExists(this.CurrentPartNumber, empty1, iRowIndex);
									}
									goto yoyo1;
								}
								catch
								{
									goto yoyo1;
								}
								break;
							}
						}
					}
					return;
				}
			yoyo1:
			}
			catch
			{
			}
		}

		private void previousListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsBtnPrev_Click(null, null);
		}

		private void ReadMandatoryAttribKeysFromSchema(XmlNode schemaNode)
		{
			try
			{
				this.sPListStatusColName = Program.iniServers[this.frmParent.ServerId].items["PLIST", "PART_STATUS"];
				for (int i = 0; i < schemaNode.Attributes.Count; i++)
				{
					if (schemaNode.Attributes[i].Value.ToUpper().Equals("PARTNUMBER"))
					{
						this.attPartNoElement = schemaNode.Attributes[i].Name;
					}
					if (schemaNode.Attributes[i].Value.ToUpper().Equals("PARTNAME"))
					{
						this.attPartNameElement = schemaNode.Attributes[i].Name;
					}
					if (schemaNode.Attributes[i].Value.ToUpper().Equals("PICTUREFILE"))
					{
						this.attPictureFileElement = schemaNode.Attributes[i].Name;
					}
					if (this.sPListStatusColName != null && schemaNode.Attributes[i].Value.ToUpper().Equals(this.sPListStatusColName.ToUpper()))
					{
						this.attPartStatusElement = schemaNode.Attributes[i].Name;
					}
				}
			}
			catch
			{
			}
		}

		private void rowSelectionModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Settings.Default.RowSelectionMode = !Settings.Default.RowSelectionMode;
				this.rowSelectionModeToolStripMenuItem.Checked = Settings.Default.RowSelectionMode;
				if (!Settings.Default.RowSelectionMode)
				{
					this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					for (int i = 0; i < this.dgPartslist.Columns.Count; i++)
					{
						this.dgPartslist.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
					}
				}
				else
				{
					for (int j = 0; j < this.dgPartslist.Columns.Count; j++)
					{
						this.dgPartslist.Columns[j].SortMode = DataGridViewColumnSortMode.Programmatic;
					}
					this.dgPartslist.SelectionMode = DataGridViewSelectionMode.CellSelect;
				}
				this.dgPartslist.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
			}
			catch
			{
			}
		}

		public void SavePartsListColumnSizes()
		{
			char[] chrArray;
			int width;
			string[] strArrays;
			int i;
			string empty = string.Empty;
			string str = string.Empty;
			try
			{
				IniFileIO iniFileIO = new IniFileIO();
				ArrayList arrayLists = new ArrayList();
				arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PLIST_JUMP_SETTINGS");
				Dictionary<string, string> strs = new Dictionary<string, string>();
				foreach (string arrayList in arrayLists)
				{
					strs.Add(arrayList, iniFileIO.GetKeyValue("PLIST_JUMP_SETTINGS", arrayList, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini")));
				}
				if (!base.IsDisposed)
				{
					foreach (DataGridViewColumn column in this.dgJumps.Columns)
					{
						if (arrayLists.Count <= 0)
						{
							continue;
						}
						string str1 = strs[column.Name].ToString();
						chrArray = new char[] { '|' };
						if (str1.Split(chrArray)[2] != "0")
						{
							string[] strArrays1 = str1.Split(new char[] { '|' });
							width = column.Width;
							strArrays1[2] = width.ToString();
							str1 = "";
							strArrays = strArrays1;
							for (i = 0; i < (int)strArrays.Length; i++)
							{
								str1 = string.Concat(str1, strArrays[i], "|");
							}
							str1 = str1.TrimEnd(new char[] { '|' });
						}
						Program.iniServers[this.frmParent.ServerId].UpdateItem("PLIST_JUMP_SETTINGS", column.Name, str1);
					}
				}
			}
			catch
			{
			}
			try
			{
				if (!base.IsDisposed)
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					ArrayList keys = new ArrayList();
					keys = iniFileIO1.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PLIST_SETTINGS");
					Dictionary<string, string> strs1 = new Dictionary<string, string>();
					foreach (string arrayList1 in keys)
					{
						strs1.Add(arrayList1.ToUpper(), iniFileIO1.GetKeyValue("PLIST_SETTINGS", arrayList1, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini")));
					}
					foreach (DataGridViewColumn dataGridViewColumn in this.dgPartslist.Columns)
					{
						try
						{
							if (keys.Count > 0 && this.curListSchema.Attributes[dataGridViewColumn.Name] != null)
							{
								string upper = this.curListSchema.Attributes[dataGridViewColumn.Name].Value.ToString().ToUpper();
								string str2 = strs1[upper].ToString();
								char[] chrArray1 = new char[] { '|' };
								if (str2.Split(chrArray1)[2] != "0")
								{
									string[] strArrays2 = str2.Split(new char[] { '|' });
									strArrays2[2] = dataGridViewColumn.Width.ToString();
									str2 = "";
									string[] strArrays3 = strArrays2;
									for (int j = 0; j < (int)strArrays3.Length; j++)
									{
										str2 = string.Concat(str2, strArrays3[j], "|");
									}
									str2 = str2.TrimEnd(new char[] { '|' });
								}
								iniFileIO1.WriteValue("PLIST_SETTINGS", upper, str2, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
							}
							if (dataGridViewColumn.Name.ToUpper() == "MEM" || dataGridViewColumn.Name.ToUpper() == "LOCMEM" || dataGridViewColumn.Name.ToUpper() == "HYPMEM" || dataGridViewColumn.Name.ToUpper() == "LOCPRGMEM" || dataGridViewColumn.Name.ToUpper() == "GBLMEM" || dataGridViewColumn.Name.ToUpper() == "PRGMEM" || dataGridViewColumn.Name.ToUpper() == "GBLTXTMEM" || dataGridViewColumn.Name.ToUpper() == "ADMMEM" || dataGridViewColumn.Name.ToUpper() == "LOCTXTMEM" || dataGridViewColumn.Name.ToUpper() == "GBLREFMEM" || dataGridViewColumn.Name.ToUpper() == "TXTMEM" || dataGridViewColumn.Name.ToUpper() == "LOCREFMEM" || dataGridViewColumn.Name.ToUpper() == "GBLHYPMEM" || dataGridViewColumn.Name.ToUpper() == "REFMEM" || dataGridViewColumn.Name.ToUpper() == "LOCHYPMEM" || dataGridViewColumn.Name.ToUpper() == "GBLPRGMEM" || dataGridViewColumn.Name.ToUpper() == "ADMTXTMEM" || dataGridViewColumn.Name.ToUpper() == "ADMREFMEM" || dataGridViewColumn.Name.ToUpper() == "ADMHYPMEM" || dataGridViewColumn.Name.ToUpper() == "ADMPRGMEM")
							{
								string str3 = strs1[dataGridViewColumn.Name].ToString();
								char[] chrArray2 = new char[] { '|' };
								if (str3.Split(chrArray2)[2] != "0")
								{
									chrArray = new char[] { '|' };
									string[] str4 = str3.Split(chrArray);
									width = dataGridViewColumn.Width;
									str4[2] = width.ToString();
									str3 = "";
									strArrays = str4;
									for (i = 0; i < (int)strArrays.Length; i++)
									{
										str3 = string.Concat(str3, strArrays[i], "|");
									}
									chrArray = new char[] { '|' };
									str3 = str3.TrimEnd(chrArray);
								}
								iniFileIO1.WriteValue("PLIST_SETTINGS", dataGridViewColumn.Name, str3, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
							}
							if (dataGridViewColumn.Name.ToUpper() == "REF")
							{
								string str5 = this.FindAttributeKey(this.dgPartslist.Columns["REF"].Tag.ToString());
								string upper1 = this.curListSchema.Attributes[str5].Value.ToString().ToUpper();
								if (this.curListSchema.Attributes[upper1] != null)
								{
									string str6 = strs1[upper1].ToString();
									chrArray = new char[] { '|' };
									if (str6.Split(chrArray)[2] != "0")
									{
										chrArray = new char[] { '|' };
										string[] strArrays4 = str6.Split(chrArray);
										width = dataGridViewColumn.Width;
										strArrays4[2] = width.ToString();
										str6 = "";
										strArrays = strArrays4;
										for (i = 0; i < (int)strArrays.Length; i++)
										{
											str6 = string.Concat(str6, strArrays[i], "|");
										}
										chrArray = new char[] { '|' };
										str6 = str6.TrimEnd(chrArray);
									}
									iniFileIO1.WriteValue("PLIST_SETTINGS", upper1, str6, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
								}
							}
							if (dataGridViewColumn.Name.ToUpper() == "QTY")
							{
								string str7 = this.FindAttributeKey(this.dgPartslist.Columns["QTY"].Tag.ToString());
								string upper2 = this.curListSchema.Attributes[str7].Value.ToString().ToUpper();
								if (this.curListSchema.Attributes[str7] != null)
								{
									string str8 = strs1[upper2].ToString();
									chrArray = new char[] { '|' };
									if (str8.Split(chrArray)[2] != "0")
									{
										chrArray = new char[] { '|' };
										string[] strArrays5 = str8.Split(chrArray);
										width = dataGridViewColumn.Width;
										strArrays5[2] = width.ToString();
										str8 = "";
										strArrays = strArrays5;
										for (i = 0; i < (int)strArrays.Length; i++)
										{
											str8 = string.Concat(str8, strArrays[i], "|");
										}
										chrArray = new char[] { '|' };
										str8 = str8.TrimEnd(chrArray);
									}
									iniFileIO1.WriteValue("PLIST_SETTINGS", upper2, str8, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
								}
							}
							if (dataGridViewColumn.Name.ToUpper() == "INF")
							{
								string str9 = this.FindAttributeKey(this.dgPartslist.Columns["INF"].Tag.ToString());
								string upper3 = this.curListSchema.Attributes[str9].Value.ToString().ToUpper();
								if (this.curListSchema.Attributes[str9] != null)
								{
									string str10 = strs1[upper3].ToString();
									chrArray = new char[] { '|' };
									if (str10.Split(chrArray)[2] != "0")
									{
										chrArray = new char[] { '|' };
										string[] strArrays6 = str10.Split(chrArray);
										width = dataGridViewColumn.Width;
										strArrays6[2] = width.ToString();
										str10 = "";
										strArrays = strArrays6;
										for (i = 0; i < (int)strArrays.Length; i++)
										{
											str10 = string.Concat(str10, strArrays[i], "|");
										}
										chrArray = new char[] { '|' };
										str10 = str10.TrimEnd(chrArray);
									}
									iniFileIO1.WriteValue("PLIST_SETTINGS", upper3, str10, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
								}
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgPartslist.Rows.Count > 0)
			{
				this.dgPartslist.SelectAll();
				this.tsbAddToSelectionList.Enabled = true;
			}
		}

		private void SelectPart()
		{
			if (this.highlightPartNo != string.Empty && this.attPartNoElement != string.Empty)
			{
				if (this.bSelectionMode && this.highlightPartNo.Contains("^"))
				{
					string[] strArrays = this.highlightPartNo.Split(new char[] { '\u005E' });
					string str = strArrays[(int)strArrays.Length - 2].ToString();
					string str1 = strArrays[(int)strArrays.Length - 1].ToString();
					for (int i = 0; i < this.dgPartslist.Rows.Count; i++)
					{
						if (i == int.Parse(str) && this.dgPartslist[this.attPartNoElement, i].Value.ToString().ToUpper() == str1.ToUpper())
						{
							this.dgPartslist.Rows[i].Selected = true;
							this.dgPartslist.FirstDisplayedScrollingRowIndex = i;
							this.highlightPartNo = string.Empty;
							return;
						}
					}
					return;
				}
				for (int j = 0; j < this.dgPartslist.Rows.Count; j++)
				{
					if (this.dgPartslist[this.attPartNoElement, j].Value.ToString().ToUpper() == this.highlightPartNo.ToUpper())
					{
						this.dgPartslist.Rows[j].Selected = true;
						this.dgPartslist.FirstDisplayedScrollingRowIndex = j;
						this.highlightPartNo = string.Empty;
						return;
					}
				}
			}
		}

		public void Set_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni()
		{
			try
			{
				this.frmParent.dicPLSettings.Clear();
				IniFileIO iniFileIO = new IniFileIO();
				ArrayList arrayLists = new ArrayList();
				arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PLIST_SETTINGS");
				for (int i = 0; i < arrayLists.Count; i++)
				{
					for (int j = 1; j < this.dgPartslist.Columns.Count; j++)
					{
						try
						{
							if (this.dgPartslist.Columns[j].Tag != null && arrayLists[i].ToString().ToUpper() == this.dgPartslist.Columns[j].Tag.ToString().ToUpper())
							{
								this.dgPartslist.Columns[j].DisplayIndex = i + 1;
								string empty = string.Empty;
								string str = string.Empty;
								empty = iniFileIO.GetKeyValue("PLIST_SETTINGS", arrayLists[i].ToString().ToUpper(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
								string[] strArrays = new string[] { "|" };
								string[] strArrays1 = empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
								string str1 = string.Concat("|True|True|", strArrays1[1], "|", strArrays1[2]);
								if ((int)strArrays1.Length == 3)
								{
									empty = string.Concat(empty, str1);
									this.frmParent.dicPLSettings.Add(arrayLists[i].ToString(), str1);
								}
								else if ((int)strArrays1.Length == 4)
								{
									empty = string.Concat(empty, str1);
									this.frmParent.dicPLSettings.Add(arrayLists[i].ToString(), str1);
								}
								string[] strArrays2 = new string[] { "|" };
								strArrays1 = empty.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
								if (strArrays1 != null && (int)strArrays1.Length != 0)
								{
									if ((int)strArrays1.Length > 7)
									{
										if (strArrays1[4] != "True")
										{
											this.dgPartslist.Columns[j].Visible = false;
										}
										else
										{
											str = strArrays1[1];
											if ((int)strArrays1.Length >= 5)
											{
												if (str.Equals("L"))
												{
													this.dgPartslist.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
													this.dgPartslist.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
												}
												else if (str.Equals("R"))
												{
													this.dgPartslist.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
													this.dgPartslist.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
												}
												else if (str.Equals("C"))
												{
													this.dgPartslist.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
													this.dgPartslist.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
												}
												int num = int.Parse(strArrays1[2]);
												if (num > 0)
												{
													this.dgPartslist.Columns[j].Width = num;
													this.dgPartslist.Columns[j].Visible = true;
												}
												this.dgPartslist.Columns[j].HeaderText = strArrays1[0];
											}
										}
									}
									else if (strArrays1[3] != "True")
									{
										this.dgPartslist.Columns[j].Visible = false;
									}
									else
									{
										str = strArrays1[1];
										if ((int)strArrays1.Length >= 5)
										{
											if (str.Equals("L"))
											{
												this.dgPartslist.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
												this.dgPartslist.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
											}
											else if (str.Equals("R"))
											{
												this.dgPartslist.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
												this.dgPartslist.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
											}
											else if (str.Equals("C"))
											{
												this.dgPartslist.Columns[j].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
												this.dgPartslist.Columns[j].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
											}
											int num1 = int.Parse(strArrays1[2]);
											if (num1 > 0)
											{
												this.dgPartslist.Columns[j].Width = num1;
												this.dgPartslist.Columns[j].Visible = true;
											}
											this.dgPartslist.Columns[j].HeaderText = strArrays1[0];
										}
									}
								}
								break;
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		public void Set_MemoCols_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni()
		{
			try
			{
				IniFileIO iniFileIO = new IniFileIO();
				ArrayList arrayLists = new ArrayList();
				arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PLIST_SETTINGS");
				ArrayList arrayLists1 = this.LoadMemoTypeKeys();
				for (int i = 0; i < arrayLists1.Count; i++)
				{
					try
					{
						string str = arrayLists1[i].ToString();
						if (this.dgPartslist.Columns.Contains(str))
						{
							DataGridViewColumn item = this.dgPartslist.Columns[str];
							string empty = string.Empty;
							empty = iniFileIO.GetKeyValue("PLIST_SETTINGS", str, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
							string[] strArrays = new string[] { "|" };
							string[] strArrays1 = empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
							item.HeaderText = strArrays1[0];
							string empty1 = string.Empty;
							empty1 = strArrays1[1];
							if (empty1.Equals("L"))
							{
								item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
								item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
							}
							else if (empty1.Equals("R"))
							{
								item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
								item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
							}
							else if (empty1.Equals("C"))
							{
								item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
								item.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
							}
							if (int.Parse(strArrays1[2]) > 0 && arrayLists.Contains(str))
							{
								if ((int)strArrays1.Length <= 4)
								{
									item.Visible = true;
									item.Width = int.Parse(strArrays1[2]);
								}
								else if ((int)strArrays1.Length == 7)
								{
									if (strArrays1[3].ToUpper() != "TRUE")
									{
										item.Visible = false;
									}
									else
									{
										item.Visible = true;
										item.Width = int.Parse(strArrays1[2]);
									}
								}
								else if (strArrays1[4].ToUpper() != "TRUE")
								{
									item.Visible = false;
								}
								else
								{
									item.Visible = true;
									item.Width = int.Parse(strArrays1[2]);
								}
							}
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetActiveWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		public void SetGridHeaderText()
		{
			try
			{
				if (!base.IsDisposed)
				{
					foreach (DataGridViewColumn column in this.dgPartslist.Columns)
					{
						if (this.curListSchema.Attributes[column.Name] != null)
						{
							try
							{
								if (Program.iniServers[this.curServerId].items["PLIST_SETTINGS", this.curListSchema.Attributes[column.Name].Value.ToUpper()] != null)
								{
									IniFileIO iniFileIO = new IniFileIO();
									ArrayList arrayLists = new ArrayList();
									arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "PLIST_SETTINGS");
									int num = 0;
									while (num < arrayLists.Count)
									{
										if (arrayLists[num].ToString().ToUpper() != this.curListSchema.Attributes[column.Name].Value.ToUpper())
										{
											num++;
										}
										else
										{
											string keyValue = iniFileIO.GetKeyValue("PLIST_SETTINGS", arrayLists[num].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
											keyValue = keyValue.Substring(0, keyValue.IndexOf("|"));
											column.HeaderText = this.GetDGHeaderCellValue(this.curListSchema.Attributes[column.Name].Value.ToUpper(), keyValue);
											break;
										}
									}
								}
							}
							catch
							{
							}
						}
						if (column.Name.ToUpper().Contains("MEM"))
						{
							try
							{
								string item = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", column.Name.ToString().ToUpper()];
								item = item.Substring(0, item.IndexOf("|"));
								column.HeaderText = this.GetDGHeaderCellValue(column.Name.ToString().ToUpper(), item);
							}
							catch
							{
							}
						}
						if (!(column.Name.ToUpper() == "REF") && !column.Name.ToUpper().Contains("QTY") && !column.Name.ToUpper().Contains("INF"))
						{
							continue;
						}
						try
						{
							string str = this.FindAttributeKey(column.Tag.ToString());
							if (this.curListSchema.Attributes[str] != null && Program.iniServers[this.curServerId].items["PLIST_SETTINGS", this.curListSchema.Attributes[str].Value.ToUpper()] != null)
							{
								string item1 = Program.iniServers[this.frmParent.ServerId].items["PLIST_SETTINGS", this.curListSchema.Attributes[str].Value.ToUpper()];
								item1 = item1.Substring(0, item1.IndexOf("|"));
								column.HeaderText = this.GetDGHeaderCellValue(this.curListSchema.Attributes[str].Value.ToUpper(), item1);
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		public void SetJumpGridHeaderText()
		{
			try
			{
				if (!base.IsDisposed)
				{
					foreach (DataGridViewColumn column in this.dgJumps.Columns)
					{
						try
						{
							if (Program.iniServers[this.curServerId].items["PLIST_JUMP_SETTINGS", column.Name.ToUpper()] != null)
							{
								string item = Program.iniServers[this.frmParent.ServerId].items["PLIST_JUMP_SETTINGS", column.Name.ToUpper()];
								item = item.Substring(0, item.IndexOf("|"));
								column.HeaderText = this.GetJumpsHeaderCellValue(column.Name.ToUpper(), item);
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		private void SetListIndex(XmlNodeList listNodes, int listIndex)
		{
			if (this.toolStrip1.InvokeRequired)
			{
				ToolStrip toolStrip = this.toolStrip1;
				frmViewerPartslist.SetListIndexDelegate setListIndexDelegate = new frmViewerPartslist.SetListIndexDelegate(this.SetListIndex);
				object[] objArray = new object[] { listNodes, listIndex };
				toolStrip.Invoke(setListIndexDelegate, objArray);
				return;
			}
			if (listNodes.Count <= 0)
			{
				this.tsTxtList.Text = "1/1";
				return;
			}
			ToolStripTextBox toolStripTextBox = this.tsTxtList;
			string str = (listIndex + 1).ToString();
			int count = listNodes.Count;
			toolStripTextBox.Text = string.Concat(str, "/", count.ToString());
		}

		public void ShowHidePartslistToolbar()
		{
			this.toolStrip1.Visible = Settings.Default.ShowListToolbar;
		}

		private void ShowHidePListInfoPanel(bool bState)
		{
			if (!this.pnlInfo.InvokeRequired)
			{
				this.pnlInfo.Visible = bState;
				return;
			}
			Panel panel = this.pnlInfo;
			frmViewerPartslist.ShowHidePListInfoPanelBoxDelegate showHidePListInfoPanelBoxDelegate = new frmViewerPartslist.ShowHidePListInfoPanelBoxDelegate(this.ShowHidePListInfoPanel);
			object[] objArray = new object[] { bState };
			panel.Invoke(showHidePListInfoPanelBoxDelegate, objArray);
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
					this.picLoading.BringToFront();
					this.picLoading.Show();
				}
				else
				{
					frmViewerPartslist.ShowLoadingDelegate showLoadingDelegate = new frmViewerPartslist.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		public void ShowPartMemos()
		{
			try
			{
				if (Program.objAppFeatures.bMemo && this.dgPartslist.Columns.Count > 1 && this.CurrentPartNumber != string.Empty && this.dgPartslist.SelectedRows.Count > 0)
				{
					int index = this.dgPartslist.SelectedRows[0].Index;
					if (this.lstDisableRows == null || !this.lstDisableRows.Contains(index))
					{
						string empty = string.Empty;
						for (int i = 0; i < this.attAdminMemList.Count; i++)
						{
							if (this.dgPartslist.Columns.Contains(this.attAdminMemList[i].ToString()) && this.dgPartslist[this.attAdminMemList[i].ToString(), index] != null)
							{
								empty = string.Concat(empty, this.dgPartslist[this.attAdminMemList[i].ToString(), index].Value, "**");
							}
						}
						string str = string.Empty;
						try
						{
							if (this.dgPartslist[this.attListUpdateDateElement, index] != null)
							{
								str = this.dgPartslist[this.attListUpdateDateElement, index].Value.ToString();
							}
						}
						catch
						{
							if (str == null || str == "" || str.Length == 0)
							{
								string str1 = DateTime.Now.ToString("M/d/yyyy");
								str = str1.Replace('-', '/');
							}
						}
						this.frmParent.ShowPartMemos(this.CurrentPartNumber, empty, string.Empty, string.Empty);
					}
					else
					{
						return;
					}
				}
			}
			catch
			{
			}
		}

		public void ShowPartsList()
		{
			try
			{
				this.splitPnlGrids.Panel1.Show();
				this.splitPnlGrids.Panel1Collapsed = false;
				this.ShowPartsListToolbar();
			}
			catch
			{
			}
		}

		private void ShowPartsListToolbar()
		{
			if (this.dgJumps.InvokeRequired)
			{
				this.dgJumps.Invoke(new frmViewerPartslist.ShowPartsListToolbarDelegate(this.ShowPartsListToolbar));
				return;
			}
			try
			{
				this.toolStrip1.Visible = true;
			}
			catch
			{
			}
		}

		private void splitPnlGrids_SplitterMoved(object sender, SplitterEventArgs e)
		{
			try
			{
				if (this.dgPartslist.RowCount != 0 && this.dgJumps.RowCount != 0)
				{
					Settings.Default.ListSplitterDistance = this.splitPnlGrids.SplitterDistance;
				}
			}
			catch
			{
			}
		}

		private void tabSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string empty = string.Empty;
				empty = (this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, "\t") : this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t"));
				if (empty != string.Empty)
				{
					Clipboard.SetText(empty, TextDataFormat.UnicodeText);
				}
			}
			catch
			{
			}
		}

		private void tabSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
			this.dlgSaveFile.RestoreDirectory = true;
			string empty = string.Empty;
			empty = (this.dgPartslist.SelectionMode != DataGridViewSelectionMode.FullRowSelect ? this.GetDataGridViewCellsText(ref this.dgPartslist, true, "\t") : this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t"));
			if (empty != string.Empty && this.dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					StreamWriter streamWriter = File.CreateText(this.dlgSaveFile.FileName);
					streamWriter.Write(empty);
					streamWriter.Close();
				}
				catch
				{
					MessageHandler.ShowWarning(this.GetResource("(E-VPL-EM002) Failed to export specified object", "(E-VPL-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
				}
			}
		}

		private void tsbAddPartMemo_Click(object sender, EventArgs e)
		{
			this.tsmAddPartMemo_Click(null, null);
		}

		private void tsbAddToSelectionList_Click(object sender, EventArgs e)
		{
			this.tsmAddToSelectionList_Click(null, null);
		}

		private void tsbClearSelection_Click(object sender, EventArgs e)
		{
			if (this.dgPartslist.Rows.Count > 0)
			{
				this.dgPartslist.ClearSelection();
				this.tsbAddPartMemo.Enabled = false;
				this.tsbAddToSelectionList.Enabled = false;
				this.frmParent.EnableAddPartMemoMenu(false);
				this.tsbClearSelection.Enabled = false;
			}
		}

		private void tsbClearSelection_EnabledChanged(object sender, EventArgs e)
		{
			this.tsmClearSelection.Enabled = this.tsbClearSelection.Enabled;
		}

		private void tsbPartistInfo_Click(object sender, EventArgs e)
		{
			Settings.Default.appPartsListInfoVisible = !Settings.Default.appPartsListInfoVisible;
			this.ShowHidePListInfoPanel(Settings.Default.appPartsListInfoVisible);
		}

		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
			this.selectAllToolStripMenuItem_Click(null, null);
		}

		private void tsBtnNext_Click(object sender, EventArgs e)
		{
			if (this.curPageSchema != null && this.curPageNode != null)
			{
				string text = this.tsTxtList.Text;
				if (text.Contains("/"))
				{
					text = text.Substring(0, text.IndexOf("/"));
					try
					{
						frmViewerPartslist.ChangeBuffer(this.dgPartslist);
						this.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, int.Parse(text), this.attPicElement, this.attListElement, this.attUpdateDateElement);
					}
					catch
					{
					}
				}
			}
		}

		private void tsBtnPrev_Click(object sender, EventArgs e)
		{
			if (this.curPageSchema != null && this.curPageNode != null)
			{
				string text = this.tsTxtList.Text;
				if (text.Contains("/"))
				{
					text = text.Substring(0, text.IndexOf("/"));
					try
					{
						frmViewerPartslist.ChangeBuffer(this.dgPartslist);
						this.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, int.Parse(text) - 2, this.attPicElement, this.attListElement, this.attUpdateDateElement);
					}
					catch
					{
					}
				}
			}
		}

		private void tsmAddPartMemo_Click(object sender, EventArgs e)
		{
			this.ShowPartMemos();
		}

		private void tsmAddToSelectionList_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (DataGridViewRow selectedRow in this.dgPartslist.SelectedRows)
				{
					if (this.lstDisableRows.Contains(selectedRow.Index))
					{
						continue;
					}
					if (selectedRow.Cells["CHK"].Value.ToString().ToUpper() != "FALSE")
					{
						if (this.dgPartslist.SelectedRows.Count != 1)
						{
							continue;
						}
						this.frmParent.ShowQuantityScreen(this.dgPartslist.CurrentRow.Cells["PART_SLIST_KEY"].Value.ToString());
					}
					else
					{
						selectedRow.Cells["CHK"].Value = true;
					}
				}
			}
			catch
			{
			}
		}

		private void tsmClearSelection_Click(object sender, EventArgs e)
		{
			this.tsbClearSelection_Click(null, null);
		}

		public void UncheckAllRows()
		{
			try
			{
				foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
				{
					try
					{
						row.Cells["CHK"].Value = false;
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		public void UpdateCurrentPageForPartslist(bool picLoaded, XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
		{
			this.picLoadedSuccessfully = picLoaded;
			this.curPageSchema = schemaNode;
			this.curPageNode = pageNode;
			this.curPicIndex = picIndex;
			this.curListIndex = listIndex;
			this.attPicElement = attPic;
			this.attListElement = attList;
			this.attUpdateDateElement = attUpdateDate;
		}

		public void UpdateFont()
		{
			this.dgPartslist.Font = Settings.Default.appFont;
			this.dgPartslist.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgPartslist.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
			this.dgJumps.Font = Settings.Default.appFont;
			this.dgJumps.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgJumps.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
			this.pnlInfo.Font = Settings.Default.appFont;
			this.pnlInfo.BackColor = Settings.Default.PartsListInfoBackColor;
			this.pnlInfo.ForeColor = Settings.Default.PartsListInfoForeColor;
			try
			{
				this.dgPartslist_SelectionChanged(null, null);
			}
			catch
			{
			}
		}

		public void UpdateMemoIconOnSelectedRow()
		{
			try
			{
				string empty = string.Empty;
				if (this.dgPartslist.SelectedRows.Count > 0)
				{
					for (int i = 0; i < this.attAdminMemList.Count; i++)
					{
						if (this.dgPartslist.Columns.Contains(this.attAdminMemList[i].ToString()) && this.dgPartslist[this.attAdminMemList[i].ToString(), this.dgPartslist.SelectedRows[0].Index] != null)
						{
							empty = string.Concat(empty, this.dgPartslist[this.attAdminMemList[i].ToString(), this.dgPartslist.SelectedRows[0].Index].Value, "**");
						}
					}
				}
				this.frmParent.PartMemoExists(this.CurrentPartNumber, empty, this.dgPartslist.SelectedRows[0].Index);
			}
			catch
			{
			}
		}

		private void UpdatePListInfoTextBox()
		{
			if (!this.lblPartsListInfo.InvokeRequired)
			{
				this.lblPartsListInfo.Text = this.partlistInfoMsg;
				return;
			}
			this.lblPartsListInfo.Invoke(new frmViewerPartslist.UpdatePListInfoTextBoxDelegate(this.UpdatePListInfoTextBox));
		}

		private void UpdateStatus()
		{
			if (!this.frmParent.InvokeRequired)
			{
				this.frmParent.UpdateStatus(this.statusText);
				return;
			}
			frmViewer _frmViewer = this.frmParent;
			frmViewerPartslist.StatusDelegate statusDelegate = new frmViewerPartslist.StatusDelegate(this.frmParent.UpdateStatus);
			object[] objArray = new object[] { this.statusText };
			_frmViewer.Invoke(statusDelegate, objArray);
		}

		private delegate void AdjustGridHeightsDelegate();

		private delegate void CheckPListInfoBtnDelegate(bool bState);

		private delegate void EnableDisablePListInfoBtnDelegate();

		private delegate void HideLoadingDelegate(Panel parentPanel);

		private delegate void HidePartsListToolbarDelegate();

		private delegate void InitializeJumpsListDelegate();

		private delegate void InitializePartsListDelegate(XmlNode schemaNode);

		private delegate void LoadJumpsInGridDelegate(XmlDocument objXmlDoc);

		private delegate void LoadPartsListInGridDelegate(XmlDocument objXmlDoc);

		private delegate void SetListIndexDelegate(XmlNodeList listNodes, int listIndex);

		private delegate void ShowHidePListInfoPanelBoxDelegate(bool bState);

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void ShowPartsListToolbarDelegate();

		private delegate void StatusDelegate(string status);

		private delegate void UpdatePListInfoTextBoxDelegate();
	}
}
using GSPcLocalViewer.frmPrint;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
	public class frmViewer : Form
	{
		private const string dllZipper = "ZIPPER.dll";

		private const int WM_NCLBUTTONDBLCLK = 163;

		private const int WM_NCRBUTTONUP = 165;

		private const int WM_NCHITTEST = 132;

		private const int WM_SYSCOMMAND = 274;

		private const int WM_NCLBUTTONDOWN = 161;

		private const int WM_NCRBUTTONDOWN = 164;

		private const int WM_MOUSEACTIVATE = 33;

		private const int MA_NOACTIVATEANDEAT = 4;

		private const int WM_ENABLE = 10;

		public XmlDocument xmlDocument;

		public Dictionary<string, string> dicPLSettings = new Dictionary<string, string>();

		public Dictionary<string, string> dicSLSettings = new Dictionary<string, string>();

		private bool bPicMemoDelete;

		public bool bIsOldINI;

		public bool bFromPopup;

		public bool bSaveMemoOnBookLevel;

		public bool bSaveMemoOnBookLevelChecked;

		public int intMemoType;

		public bool bObjFrmTreeviewClosed;

		public bool bObjFrmPictureClosed;

		public bool bObjFrmPartlistClosed;

		public bool bObjFrmSelectionlistClosed;

		public frmViewerTreeview objFrmTreeview;

		public frmViewerPicture objFrmPicture;

		public frmViewerPartslist objFrmPartlist;

		public frmSelectionList objFrmSelectionlist;

		public frmInfo objFrmInfo;

		private Dimmer objDimmer;

		public History objHistory;

		public bool bPartsListClosed;

		public bool bPictureClosed;

		public bool ISPDF;

		private string[] p_ArgsO;

		public string[] p_ArgsF;

		private string[] p_ArgsS;

		public int p_ServerId;

		private string p_BookId;

		private XmlNode p_BookNode;

		private XmlNode p_SchemaNode;

		private string CurrentLanguage;

		private string p_BookType;

		public XmlDocument xDocLocalMemo;

		public XmlDocument xDocGlobalMemo;

		private Dashbord frmParent;

		public bool p_Encrypted;

		public bool p_Compressed;

		private int iPageJumpImageIndex;

		public string sBookType;

		public frmMemo objFrmMemo;

		public string sSearchHistoryPath;

		public string sFirstPageTitle = string.Empty;

		public bool bImageClosed;

		public static bool MiniMapChk;

		private static string iniValueMiniMap;

		private Download objDownloader;

		public List<string> lstFilteredPages = new List<string>();

		public bool bIsSelectionListPrint;

		public DataTable gdtselectionListTable;

		private string MessageDjVu = string.Empty;

		private IContainer components;

		private ToolStripContainer toolStripContainer1;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem fileToolStripMenuItem;

		private StatusStrip ssStatus;

		private ToolStripStatusLabel lblStatus;

		private ToolStripMenuItem closeToolStripMenuItem;

		private ToolStripMenuItem viewToolStripMenuItem;

		private ToolStripMenuItem restoreDefaultsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		public ToolStripMenuItem contentsToolStripMenuItem;

		public ToolStripMenuItem pictureToolStripMenuItem;

		public ToolStripMenuItem partslistToolStripMenuItem;

		public ToolStripMenuItem informationToolStripMenuItem;

		private ToolStripMenuItem openToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem settingsToolStripMenuItem;

		private Panel pnlForm2;

		private Panel pnlForm;

		private DockPanel objDocPanel;

		private PictureBox picDisable;

		private ToolStripSeparator toolStripSeparator3;

		private BackgroundWorker bgWorker;

		private ToolStripMenuItem bookmarksToolStripMenuItem;

		private ToolStripMenuItem addBookmarksToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator;

		private ToolStripMenuItem searchToolStripMenuItem;

		private ToolStripMenuItem pageNameToolStripMenuItem;

		private ToolStripMenuItem partNumberToolStripMenuItem;

		private ToolStrip tsHistory;

		private ToolStripButton tsbHistoryBack;

		private ToolStripButton tsbHistoryForward;

		private ToolStrip tsFunctions;

		private ToolStripButton tsbPrint;

		private ToolStripButton tsbAddBookmarks;

		private ToolStripButton tsbConfiguration;

		private ToolStripButton tsbAbout;

		private ToolStrip tsView;

		private ToolStripButton tsbViewContents;

		private ToolStripButton tsbViewPicture;

		private ToolStripButton tsbViewPartslist;

		private ToolStripSeparator tsbViewSeparator;

		private ToolStripButton tsbViewInfo;

		private ToolStrip tsSearch;

		private ToolStripButton tsbSearchPageName;

		private ToolStripButton tsbSearchPartNumber;

		private ToolStripMenuItem printToolStripMenuItem;

		private ToolStripMenuItem partNameToolStripMenuItem;

		private ToolStripMenuItem textSearchToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStrip tsNavigate;

		private ToolStripButton tsbNavigateFirst;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton tsbNavigatePrevious;

		private ToolStripButton tsbNavigateNext;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripButton tsbNavigateLast;

		private ToolStripButton tsbSearchPartName;

		private ToolStripButton tsbSearchText;

		public ToolStripMenuItem selectionListToolStripMenuItem;

		private ToolStripMenuItem helpToolStripMenuItem;

		private ToolStripMenuItem aboutGSPcLocalToolStripMenuItem;

		private ToolStripMenuItem memoSettingsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator9;

		private ToolStripButton tsbPicNext;

		private ToolStripTextBox tstPicNo;

		private ToolStripButton tsbPicPrev;

		private ToolStripLabel tslPic;

		private ToolStripDropDownButton tsbHistoryList;

		private ToolStripButton tsbPicZoomIn;

		private ToolStripButton tsbPicZoomOut;

		public ToolStripButton tsbPicZoomSelect;

		public ToolStripButton tsbPicCopy;

		private ToolStripSeparator toolStripSeparator11;

		private ToolStripMenuItem closeAllToolStripMenuItem;

		private ToolStripMenuItem addOnToolStripMenuItem;

		public ToolStripButton tsbPicPanMode;

		private ToolStripButton tsbMemoList;

		private ToolStrip tsPortal;

		private ToolStripButton tsbPortal;

		private ToolStripButton tsbOpenBook;

		private ToolStripMenuItem goToPortalToolStripMenuItem;

		private ToolStripMenuItem navigationToolStripMenuItem;

		private ToolStripMenuItem firstPageToolStripMenuItem;

		private ToolStripMenuItem previousPageToolStripMenuItem;

		private ToolStripMenuItem nextPageToolStripMenuItem;

		private ToolStripMenuItem lastPageToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator12;

		private ToolStripMenuItem previousViewToolStripMenuItem;

		private ToolStripMenuItem nextViewToolStripMenuItem;

		private ToolStripMenuItem memoDetailsToolStripMenuItem;

		private ToolStripMenuItem addPictureMemoToolStripMenuItem;

		private ToolStripMenuItem addPartMemoToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator13;

		private ToolStripMenuItem memoRecoveryToolStripMenuItem;

		private ToolStripMenuItem advancedSearchToolStripMenuItem;

		private ToolStripMenuItem fontToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator14;

		private ToolStripMenuItem generalToolStripMenuItem;

		private ToolStripMenuItem pageNameSearchSettingsToolStripMenuItem;

		private ToolStripMenuItem partNameSearchSettingsToolStripMenuItem;

		private ToolStripMenuItem partNumberSearchSettingsToolStripMenuItem;

		private ToolStripMenuItem toolsToolStripMenuItem;

		public ToolStripMenuItem singleBookToolStripMenuItem;

		public ToolStripMenuItem multipleBooksToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator16;

		private ToolStripMenuItem viewMemoListToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator17;

		private ToolStripButton tsbRestoreDefaults;

		private ToolStripButton tsbAddPictureMemo;

		private ToolStripButton tsbMemoRecovery;

		private ToolStrip tsTools;

		public ToolStripButton tsbSingleBookDownload;

		public ToolStripButton tsbMultipleBooksDownload;

		private ToolStripSeparator toolStripSeparator18;

		private ToolStripButton tsbDataCleanup;

		private ToolStripSeparator toolStripSeparator19;

		private ToolStripSeparator toolStripSeparator20;

		public ToolStripStatusLabel lblMode;

		public ToolStripMenuItem workOffLineMenuItem;

		private ToolStripSeparator toolStripSeparator21;

		private ToolStripButton tsbConnection;

		private ToolStripSeparator toolStripSeparator15;

		private ToolStripMenuItem manageDiskSpaceToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem connectionToolStripMenuItem;

		private ToolStripMenuItem textSearceNameSearchSettingsToolStripMenuItem;

		public ToolStripMenuItem miniMapToolStripMenuItem;

		private ToolStripButton tsbFitPage;

		private ToolStripButton tsBRotateLeft;

		private ToolStripButton tsBRotateRight;

		private ToolStripSeparator toolStripSeparator22;

		public ToolStripButton tsbPicSelectText;

		private ToolStripMenuItem printPageToolStripMenuItem;

		private ToolStripMenuItem printPictureToolStripMenuItem;

		private ToolStripMenuItem printListToolStripMenuItem;

		private ToolStripMenuItem printSelectionListToolStripMenuItem;

		private ToolStripMenuItem saveDefaultsToolStripMenuItem;

		private ToolStripButton tsbSearchPartAdvance;

		private ToolStripButton tsbHelp;

		private ToolStripMenuItem gSPcLocalHelpToolStripMenuItem;

		private ToolStripMenuItem advanceSearchSettingsToolStripMenuItem;

		private ToolStripMenuItem ColorToolStripMenuItem;

		private ToolStripButton tsbThirdPartyBasket;

		public ToolStripButton tsbFindText;

		public ToolStripButton tsbThumbnail;

		private ToolStripSeparator toolStripSeparator10;

		internal ToolStrip tsPic;

		private ToolStripSeparator toolStripSeparator23;

		private ToolStripMenuItem partsListSettingsToolStripMenuItem;

		private ToolStripMenuItem selectionListSettingsToolStripMenuItem;

		public string AppCurrentLanguage
		{
			get
			{
				return this.CurrentLanguage;
			}
		}

		public XmlNode BookNode
		{
			get
			{
				return this.p_BookNode;
			}
		}

		public string BookPublishingId
		{
			get
			{
				return this.p_BookId;
			}
		}

		public string BookType
		{
			get
			{
				return this.p_BookType;
			}
		}

		public bool CompressedDownload
		{
			get
			{
				return this.p_Compressed;
			}
			set
			{
				this.p_Compressed = value;
			}
		}

		public CustomTreeView CurrentTreeView
		{
			get
			{
				return this.objFrmTreeview.CurrentTreeView;
			}
		}

		public string LocalSearchSettingsPath
		{
			get
			{
				string empty = string.Empty;
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				empty = string.Concat(empty, "\\", Application.ProductName);
				empty = string.Concat(empty, "\\", Program.iniServers[this.ServerId].sIniKey);
				if (!Directory.Exists(empty))
				{
					Directory.CreateDirectory(empty);
				}
				empty = string.Concat(empty, "\\SearchSettings.xml");
				return empty;
			}
		}

		public XmlNode PageNode
		{
			get
			{
				return this.objFrmTreeview.PageNode;
			}
		}

		public XmlNode PageSchemaNode
		{
			get
			{
				return this.objFrmTreeview.PageSchemaNode;
			}
		}

		public DataGridView PartListGridView
		{
			get
			{
				return this.objFrmPartlist.Partlist;
			}
		}

		public bool PartslistExists
		{
			get
			{
				return this.partslistToolStripMenuItem.Enabled;
			}
		}

		public string PicturePath
		{
			get
			{
				return this.objFrmPicture.PicturePath;
			}
		}

		public XmlNode SchemaNode
		{
			get
			{
				return this.p_SchemaNode;
			}
		}

		public int ServerId
		{
			get
			{
				return this.p_ServerId;
			}
		}

		public string ServerSearchSettingsFile
		{
			get
			{
				string empty = string.Empty;
				empty = Program.iniServers[this.ServerId].items["SETTINGS", "CONTENT_PATH"];
				if (!empty.EndsWith("/"))
				{
					empty = string.Concat(empty, "/");
				}
				empty = string.Concat(empty, "SearchSettings.xml");
				return empty;
			}
		}

		public string TssString
		{
			get
			{
				string empty;
				try
				{
					if (this.p_ArgsS == null)
					{
						empty = string.Empty;
					}
					else
					{
						string[] pArgsS = this.p_ArgsS;
						int num = 0;
						while (num < (int)pArgsS.Length)
						{
							string str = pArgsS[num];
							if (!str.ToUpper().Trim().StartsWith("TSS="))
							{
								num++;
							}
							else
							{
								empty = str.ToUpper().Replace("TSS=", string.Empty);
								return empty;
							}
						}
						empty = string.Empty;
					}
				}
				catch
				{
					empty = string.Empty;
				}
				return empty;
			}
		}

		static frmViewer()
		{
			frmViewer.MiniMapChk = false;
			frmViewer.iniValueMiniMap = "";
		}

		public frmViewer(Dashbord frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.objDimmer = new Dimmer();
			this.xmlDocument = new XmlDocument();
			int num = 1;
			bool flag = (bool)num;
			this.bObjFrmSelectionlistClosed = (bool)num;
			bool flag1 = flag;
			bool flag2 = flag1;
			this.bObjFrmPartlistClosed = flag1;
			bool flag3 = flag2;
			bool flag4 = flag3;
			this.bObjFrmPictureClosed = flag3;
			this.bObjFrmTreeviewClosed = flag4;
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			folderPath = string.Concat(folderPath, "\\", Application.ProductName, "\\docking.config");
			Program.configPath = folderPath;
			string str = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			str = string.Concat(str, "\\", Application.ProductName, "\\defaults.config");
			Program.defaultsPath = str;
			if (Program.bNoViewerOpen)
			{
				this.ReadUserSettingINI();
			}
			this.objHistory = new History();
			this.bPartsListClosed = false;
			this.bPictureClosed = false;
			this.p_ArgsO = null;
			this.p_ArgsF = null;
			this.p_ServerId = 0;
			this.p_BookId = string.Empty;
			this.p_BookNode = null;
			this.p_SchemaNode = null;
			this.sBookType = "GSP";
			this.sSearchHistoryPath = string.Empty;
			this.iPageJumpImageIndex = 0;
			this.UpdateFont();
			frmViewer.SetWindowSizeFromSettings(Settings.Default.appCurrentSize, this);
			this.InitializeAddOnsGUI();
			this.objDownloader = new Download(this);
			this.ReadSearchHistoryPath();
			this.gdtselectionListTable = new DataTable();
			this.LoadXMLFirstTime();
			this.SetText();
			this.intMemoType = this.GetMemoType();
		}

		private void aboutGSPcLocalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				frmAbout _frmAbout = new frmAbout(this)
				{
					Owner = this
				};
				this.ShowDimmer();
				_frmAbout.Show();
			}
			catch
			{
				this.HideDimmer();
			}
		}

		private void AddBookmarksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			try
			{
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				empty = string.Concat(empty, "\\", Application.ProductName);
				empty = string.Concat(empty, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				if (!Directory.Exists(empty))
				{
					Directory.CreateDirectory(empty);
				}
				empty = string.Concat(empty, "\\BookMarks.xml");
				XmlDocument xmlDocument = new XmlDocument();
				XmlNode xmlNodes = xmlDocument.CreateNode(XmlNodeType.Element, "BookMark", null);
				XmlAttribute pBookId = xmlDocument.CreateAttribute("ServerKey");
				pBookId.Value = Program.iniServers[this.p_ServerId].sIniKey;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("BookId");
				pBookId.Value = this.p_BookId;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PageId");
				pBookId.Value = this.objFrmPicture.CurrentPageId;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PageName");
				pBookId.Value = this.objFrmPicture.CurrentPageName;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PicIndex");
				pBookId.Value = this.objFrmPartlist.CurrentPicIndex;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("ListIndex");
				pBookId.Value = this.objFrmPartlist.CurrentListIndex;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PartNo");
				pBookId.Value = this.objFrmPartlist.CurrentPartNumber;
				xmlNodes.Attributes.Append(pBookId);
				if (!File.Exists(empty))
				{
					xmlDocument.LoadXml("<?xml version='1.0' encoding='utf-8'?><BookMarks></BookMarks>");
				}
				else
				{
					xmlDocument.Load(empty);
				}
				string str = "//BookMark";
				foreach (XmlAttribute attribute in xmlNodes.Attributes)
				{
					string[] name = new string[] { str, "[@", attribute.Name, "='", attribute.Value, "']" };
					str = string.Concat(name);
					if (!attribute.Value.Equals(string.Empty) || !(attribute.Name != "PartNo"))
					{
						continue;
					}
					xmlDocument = null;
					MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM003) Invalid command", "(E-VWR-EM003)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
					return;
				}
				CustomToolStripMenuItem customToolStripMenuItem = new CustomToolStripMenuItem(xmlNodes.Attributes["PageName"].Value);
				string[] value = new string[] { xmlNodes.Attributes["BookId"].Value, " [", xmlNodes.Attributes["PageId"].Value, "] [", xmlNodes.Attributes["PicIndex"].Value, "][", xmlNodes.Attributes["ListIndex"].Value, "][", xmlNodes.Attributes["PartNo"].Value, "]" };
				customToolStripMenuItem.Name = string.Concat(value);
				customToolStripMenuItem.Tag = xmlNodes.OuterXml;
				string[] resource = new string[] { this.GetResource("Picture Index", "PICTURE_INDEX", ResourceType.TOOLSTRIP), " = ", xmlNodes.Attributes["PicIndex"].Value, "\n", this.GetResource("List Index", "LIST_INDEX", ResourceType.TOOLSTRIP), " = ", xmlNodes.Attributes["ListIndex"].Value };
				customToolStripMenuItem.ToolTipText = string.Concat(resource);
				if (xmlNodes.Attributes["PartNo"].Value != string.Empty)
				{
					string[] toolTipText = new string[] { customToolStripMenuItem.ToolTipText, "\n", this.GetResource("Part Number", "PART_NUMBER", ResourceType.TOOLSTRIP), " = ", xmlNodes.Attributes["PartNo"].Value };
					customToolStripMenuItem.ToolTipText = string.Concat(toolTipText);
				}
				XmlNode xmlNodes1 = xmlDocument.SelectSingleNode(str);
				if (xmlNodes1 != null)
				{
					xmlNodes1.ParentNode.RemoveChild(xmlNodes1);
					this.bookmarksToolStripMenuItem.DropDown.Items.RemoveByKey(customToolStripMenuItem.Name);
				}
				if (xmlDocument.ChildNodes.Count == 2)
				{
					xmlDocument.ChildNodes[1].AppendChild(xmlNodes);
					xmlDocument.Save(empty);
					xmlDocument = null;
					this.bookmarksToolStripMenuItem.DropDown.Items.Add(customToolStripMenuItem);
					customToolStripMenuItem.OnOpen += new CustomToolStripMenuItem.ClickHandler(this.OpenBookmarkPage);
					customToolStripMenuItem.OnDelete += new CustomToolStripMenuItem.ClickHandler(this.DeleteBookmarkPage);
				}
				this.toolStripSeparator.Visible = true;
			}
			catch
			{
				MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM005) Failed to save specified object", "(E-VWR-EM005)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
			}
		}

		public void AddNewRecord(DataGridViewRow dRow)
		{
			this.frmParent.AddNewRecord(dRow);
		}

		private void addPartMemoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.objFrmPartlist.ShowPartMemos();
		}

		private void addPictureMemoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ShowPictureMemos(false);
		}

		public void AddToHistory()
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlNode xmlNodes = xmlDocument.CreateNode(XmlNodeType.Element, "History", null);
				XmlAttribute pBookId = xmlDocument.CreateAttribute("ServerKey");
				pBookId.Value = Program.iniServers[this.p_ServerId].sIniKey;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("BookId");
				pBookId.Value = this.p_BookId;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PageName");
				pBookId.Value = this.objFrmPicture.CurrentPageName;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PageId");
				pBookId.Value = this.objFrmPicture.CurrentPageId;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PicIndex");
				pBookId.Value = this.objFrmPartlist.CurrentPicIndex;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("ListIndex");
				pBookId.Value = this.objFrmPartlist.CurrentListIndex;
				xmlNodes.Attributes.Append(pBookId);
				pBookId = xmlDocument.CreateAttribute("PartNo");
				pBookId.Value = this.objFrmPartlist.CurrentPartNumber;
				xmlNodes.Attributes.Append(pBookId);
				if (this.p_ArgsF != null && (int)this.p_ArgsF.Length > 0)
				{
					int i = 0;
					for (i = 0; i < (int)this.p_ArgsF.Length; i++)
					{
						pBookId = xmlDocument.CreateAttribute(i.ToString());
						pBookId.Value = this.p_ArgsF[i];
						xmlNodes.Attributes.Append(pBookId);
					}
					pBookId = xmlDocument.CreateAttribute("Filters");
					pBookId.Value = i.ToString();
					xmlNodes.Attributes.Append(pBookId);
				}
				this.objHistory.Add(xmlNodes);
				this.tsbHistoryList.DropDownItems.Clear();
				Hashtable getHistoryList = this.objHistory.GetHistoryList;
				for (int j = 0; j < getHistoryList.Count; j++)
				{
					ToolStripItem toolStripMenuItem = new ToolStripMenuItem(string.Concat(((XmlNode)getHistoryList[j]).Attributes["BookId"].Value, "-", ((XmlNode)getHistoryList[j]).Attributes["PageName"].Value))
					{
						Tag = j
					};
					toolStripMenuItem.Click += new EventHandler(this.HistoryToolStripItem_Click);
					bool flag = false;
					foreach (ToolStripMenuItem item in this.tsbHistoryList.DropDown.Items)
					{
						if (item.Text != toolStripMenuItem.Text)
						{
							continue;
						}
						flag = true;
						break;
					}
					if (!flag)
					{
						this.tsbHistoryList.DropDown.Items.Add(toolStripMenuItem);
					}
				}
				this.tsbHistoryBack.Enabled = this.objHistory.BackEnable;
				this.previousViewToolStripMenuItem.Enabled = this.objHistory.BackEnable;
				this.tsbHistoryForward.Enabled = this.objHistory.ForwardEnable;
				this.nextViewToolStripMenuItem.Enabled = this.objHistory.ForwardEnable;
				this.tsbHistoryList.Enabled = this.objHistory.ListEnable;
			}
			catch
			{
			}
		}

		private void advancedSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSearch _frmSearch = new frmSearch(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmSearch.Show(SearchTasks.Advanced);
		}

		private void advanceSearchSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Search_Advance);
		}

		public void ApplyPrintSettings(bool bSLColumnsVisible)
		{
			try
			{
				this.bIsSelectionListPrint = bSLColumnsVisible;
				this.printSelectionListToolStripMenuItem.Enabled = bSLColumnsVisible;
			}
			catch
			{
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			object[] argument = (object[])e.Argument;
			string str = (string)argument[0];
			string str1 = (string)argument[1];
			this.UpdateStatus(this.GetResource("Downloading...", "DOWNLOADING", ResourceType.STATUS_MESSAGE));
			string empty = string.Empty;
			string empty1 = string.Empty;
			int num = 0;
			bool flag = false;
			empty = string.Concat(((string)argument[0]).Remove(str.LastIndexOf("/") + 1), "DataUpdate.xml");
			empty1 = string.Concat(((string)argument[1]).Remove(str1.LastIndexOf("\\")), "\\DataUpdate.xml");
			if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
			{
				num = 0;
			}
			this.objDownloader.DownloadFile(empty, empty1);
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
				DateTime dateTime = Global.DataUpdateDate(empty1);
				if (Global.IntervalElapsed(Global.GetLocalDateOfFile(str1, this.p_ServerId), dateTime, num))
				{
					flag = true;
				}
			}
			if (flag && this.objDownloader.DownloadFile(str, str1) && !base.IsDisposed)
			{
				this.UpdateStatus(this.GetResource("Searching Series.xml", "SEARCING_SERIES_XML", ResourceType.STATUS_MESSAGE));
				int num1 = this.SearchSeries(str1, this.p_ArgsO[1]);
				if (num1 == 1)
				{
					e.Result = "ok";
				}
				else if (num1 == 0)
				{
					e.Result = "Command does not return any results";
				}
				else if (num1 == 2)
				{
					e.Result = this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_NOINFO", ResourceType.POPUP_MESSAGE);
				}
			}
			if (File.Exists(str1))
			{
				if (!base.IsDisposed)
				{
					this.UpdateStatus(this.GetResource("Searching Series.xml", "SEARCING_SERIES_XML", ResourceType.STATUS_MESSAGE));
					int num2 = this.SearchSeries(str1, this.p_ArgsO[1]);
					if (num2 == 1)
					{
						e.Result = "ok";
						return;
					}
					if (num2 == 0)
					{
						e.Result = "Command does not return any results";
						return;
					}
					if (num2 == 2)
					{
						e.Result = this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_NOINFO", ResourceType.POPUP_MESSAGE);
						return;
					}
				}
			}
			else if (!base.IsDisposed)
			{
				this.UpdateStatus(this.GetResource("(E-VWR-EM004) Failed to download specified object", "(E-VWR-EM004)_OBJECT", ResourceType.STATUS_MESSAGE));
				e.Result = this.GetResource("(E-VWR-EM004) Failed to download specified object", "(E-VWR-EM004)_OBJECT", ResourceType.POPUP_MESSAGE);
				if (this.p_ArgsO != null)
				{
					this.p_ArgsO = null;
				}
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			string result = (string)e.Result;
			if (!base.IsDisposed)
			{
				if (result.Equals("ok"))
				{
					this.objFrmTreeview.LoadBook();
					return;
				}
				if (!this.objFrmTreeview.IsDisposed)
				{
					this.objFrmTreeview.Dispose();
				}
				if (!this.objFrmPicture.IsDisposed)
				{
					this.objFrmPicture.Dispose();
				}
				if (!this.objFrmSelectionlist.IsDisposed)
				{
					this.objFrmSelectionlist.Dispose();
				}
				if (!this.objFrmPartlist.IsDisposed)
				{
					this.objFrmPartlist.Dispose();
				}
				if (!this.objFrmInfo.IsDisposed)
				{
					this.objFrmInfo.Dispose();
				}
				this.EnableMenuAndToolbar(false);
				if (result != "SecureBook")
				{
					MessageHandler.ShowInformation(result);
				}
			}
		}

		public void BookDowloadAddSearchXmlFile(ref ArrayList arrList, string sBookPath, string sBookPublishingID, string sBookType)
		{
			try
			{
				bool flag = false;
				string empty = string.Empty;
				string str = string.Empty;
				string empty1 = string.Empty;
				string str1 = string.Empty;
				if (sBookType.ToUpper() != "GSP")
				{
					empty1 = "TextSearch.xml";
					str1 = "TextSearch.zip";
				}
				else
				{
					empty1 = "Search.xml";
					str1 = "Search.zip";
				}
				str = string.Concat(sBookPath, "\\", sBookPublishingID, empty1);
				empty = string.Concat(sBookPath, "\\", sBookPublishingID, str1);
				if (!File.Exists(str) && File.Exists(empty))
				{
					try
					{
						Global.Unzip(empty);
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
					else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str, this.p_Compressed, this.p_Encrypted), Global.GetServerUpdateDateFromXmlNode(this.SchemaNode, this.BookNode), num))
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					if (!this.p_Compressed)
					{
						arrList.Insert(0, string.Concat(sBookPublishingID, empty1));
					}
					else
					{
						arrList.Insert(0, string.Concat(sBookPublishingID, str1));
					}
				}
			}
			catch
			{
			}
		}

		public void BookJump(string[] sArgs)
		{
			this.frmParent.NextTime(sArgs);
		}

		public void ChangeApplicationLanguage()
		{
			try
			{
				this.objFrmInfo.LoadResources();
				this.objFrmPartlist.LoadResources();
				this.objFrmPartlist.SetGridHeaderText();
				this.objFrmSelectionlist.SetGridHeaderText();
				this.objFrmSelectionlist.LoadTitle();
				this.objFrmPicture.LoadResources();
				this.objFrmSelectionlist.LoadResources();
				this.objFrmTreeview.LoadResources();
				this.ResetBookMarkTooltipLanguage();
				this.objFrmPartlist.SetJumpGridHeaderText();
			}
			catch
			{
			}
		}

		private void ChangeApplicationMode()
		{
			try
			{
				if (this.workOffLineMenuItem.Enabled)
				{
					if (!Program.objAppMode.bWorkingOffline)
					{
						Program.objAppMode.bWorkingOffline = true;
						this.lblMode.ToolTipText = "Working Offline";
						this.lblMode.Image = GSPcLocalViewer.Properties.Resources.offline;
						this.tsbSingleBookDownload.Enabled = false;
						this.tsbMultipleBooksDownload.Enabled = false;
						this.singleBookToolStripMenuItem.Enabled = false;
						this.multipleBooksToolStripMenuItem.Enabled = false;
					}
					else if (Program.objAppMode.InternetConnected)
					{
						Program.objAppMode.bWorkingOffline = false;
						this.lblMode.ToolTipText = "Working Online";
						this.lblMode.Image = GSPcLocalViewer.Properties.Resources.online;
						this.tsbSingleBookDownload.Enabled = true;
						this.tsbMultipleBooksDownload.Enabled = true;
						this.singleBookToolStripMenuItem.Enabled = true;
						this.multipleBooksToolStripMenuItem.Enabled = true;
					}
					this.frmParent.ChangeApplicationMode(Program.objAppMode.bWorkingOffline);
				}
			}
			catch
			{
			}
		}

		public void ChangeGlobalMemoPath(string oldPath, string newPath)
		{
			if (oldPath == string.Empty)
			{
				oldPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				oldPath = string.Concat(oldPath, "\\", Application.ProductName);
				oldPath = string.Concat(oldPath, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				if (!Directory.Exists(oldPath))
				{
					Directory.CreateDirectory(oldPath);
				}
			}
			if (this.xDocGlobalMemo != null)
			{
				try
				{
					this.xDocGlobalMemo.Save(string.Concat(oldPath, "\\GlobalMemo.xml"));
				}
				catch
				{
					MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM008) Failed to save specified object", "(E-VWR-EM008)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
				}
				this.xDocGlobalMemo = null;
			}
			this.xDocGlobalMemo = new XmlDocument();
			if (newPath == string.Empty)
			{
				newPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				newPath = string.Concat(newPath, "\\", Application.ProductName);
				newPath = string.Concat(newPath, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				if (!Directory.Exists(newPath))
				{
					Directory.CreateDirectory(newPath);
				}
			}
			try
			{
				if (!File.Exists(string.Concat(newPath, "\\GlobalMemo.xml")))
				{
					this.xDocGlobalMemo.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos/>");
				}
				else
				{
					this.xDocGlobalMemo.Load(string.Concat(newPath, "\\GlobalMemo.xml"));
				}
				this.ReloadSamePage();
			}
			catch
			{
				try
				{
					this.xDocGlobalMemo = null;
					this.xDocGlobalMemo.Load(string.Concat(oldPath, "\\GlobalMemo.xml"));
					MessageHandler.ShowWarning(this.GetResource("E-VWR-EM009) Failed to load specified object", "(E-VWR-EM009)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
				}
				catch
				{
				}
			}
		}

		public void ChangeSelListQuantity(string sPartNumber, string sQuantity)
		{
			try
			{
				this.frmParent.ChangeQuantity(sPartNumber, sQuantity);
			}
			catch
			{
			}
		}

		public void checkConfigFileExist()
		{
			try
			{
				Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
				if (!File.Exists(configuration.FilePath))
				{
					string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					folderPath = string.Concat(folderPath, "\\", Application.ProductName);
					if (Directory.Exists(folderPath))
					{
						folderPath = string.Concat(folderPath, "\\user.config");
						if (File.Exists(folderPath))
						{
							File.Copy(folderPath, configuration.FilePath, false);
						}
					}
				}
			}
			catch (Exception exception)
			{
				string message = exception.Message;
			}
		}

		public void CheckUncheckRow(string iRowNumber, string sPartArguments, bool bCheck)
		{
			try
			{
				string[] strArrays = new string[] { "**" };
				sPartArguments.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				this.frmParent.CheckUncheckRow(iRowNumber, sPartArguments, bCheck);
			}
			catch
			{
			}
		}

		public void CheckUncheckRow(string sPartNumber, bool bCheck)
		{
			try
			{
				this.frmParent.CheckUncheckRow(sPartNumber, bCheck);
			}
			catch
			{
			}
		}

		public void ClearBookmarks()
		{
			try
			{
				for (int i = this.bookmarksToolStripMenuItem.DropDown.Items.Count - 1; i >= 0; i--)
				{
					if (this.bookmarksToolStripMenuItem.DropDown.Items[i].GetType().ToString() == typeof(CustomToolStripMenuItem).ToString())
					{
						this.bookmarksToolStripMenuItem.DropDown.Items.RemoveAt(i);
					}
				}
			}
			catch
			{
			}
		}

		public void ClearSelectionList()
		{
			try
			{
				this.frmParent.ClearSelectionList();
			}
			catch
			{
			}
		}

		private object CloneControls(object o)
		{
			Type type = o.GetType();
			PropertyInfo[] properties = type.GetProperties();
			object obj = type.InvokeMember("", BindingFlags.CreateInstance, null, o, null);
			PropertyInfo[] propertyInfoArray = properties;
			for (int i = 0; i < (int)propertyInfoArray.Length; i++)
			{
				PropertyInfo propertyInfo = propertyInfoArray[i];
				if (propertyInfo.CanWrite)
				{
					propertyInfo.SetValue(obj, propertyInfo.GetValue(o, null), null);
				}
			}
			return obj;
		}

		private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.frmParent.CloseAll();
			}
			catch
			{
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		[DllImport("ole32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern void CoFreeUnusedLibraries();

		private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConnection _frmConnection = new frmConnection(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConnection.Show();
		}

		private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.contentsToolStripMenuItem.Checked = !this.contentsToolStripMenuItem.Checked;
			if (!this.contentsToolStripMenuItem.Checked)
			{
				this.objFrmTreeview.Hide();
				return;
			}
			this.objFrmTreeview.Show(this.objDocPanel);
		}

		public void CopyConfigurationFile()
		{
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				folderPath = string.Concat(folderPath, "\\", Application.ProductName);
				if (Directory.Exists(folderPath))
				{
					folderPath = string.Concat(folderPath, "\\user.config");
					Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
					if (File.Exists(configuration.FilePath))
					{
						File.Copy(configuration.FilePath, folderPath, true);
					}
				}
			}
			catch (Exception exception)
			{
				string message = exception.Message;
			}
		}

		private XmlNode CreateAdminMemoNode(string type, string value, string sPartNumber, string date)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodes = xmlDocument.CreateNode(XmlNodeType.Element, "Memo", null);
			XmlAttribute pBookId = xmlDocument.CreateAttribute("ServerKey");
			pBookId.Value = Program.iniServers[this.p_ServerId].sIniKey;
			xmlNodes.Attributes.Append(pBookId);
			pBookId = xmlDocument.CreateAttribute("BookId");
			pBookId.Value = this.p_BookId;
			xmlNodes.Attributes.Append(pBookId);
			pBookId = xmlDocument.CreateAttribute("PageId");
			pBookId.Value = this.objFrmPicture.CurrentPageId;
			xmlNodes.Attributes.Append(pBookId);
			pBookId = xmlDocument.CreateAttribute("PicIndex");
			pBookId.Value = this.objFrmPartlist.CurrentPicIndex;
			xmlNodes.Attributes.Append(pBookId);
			pBookId = xmlDocument.CreateAttribute("ListIndex");
			pBookId.Value = string.Empty;
			xmlNodes.Attributes.Append(pBookId);
			pBookId = xmlDocument.CreateAttribute("PartNo");
			pBookId.Value = sPartNumber;
			xmlNodes.Attributes.Append(pBookId);
			pBookId = xmlDocument.CreateAttribute("Type");
			pBookId.Value = type;
			xmlNodes.Attributes.Append(pBookId);
			pBookId = xmlDocument.CreateAttribute("Value");
			pBookId.Value = value;
			xmlNodes.Attributes.Append(pBookId);
			if (type.ToUpper() == "HYP")
			{
				if (!value.Contains("|"))
				{
					pBookId = xmlDocument.CreateAttribute("Value");
					pBookId.Value = value;
					xmlNodes.Attributes.Append(pBookId);
				}
				else
				{
					string[] strArrays = value.Split(new char[] { '|' });
					if ((int)strArrays.Length > 1)
					{
						pBookId = xmlDocument.CreateAttribute("Value");
						pBookId.Value = strArrays[0];
						xmlNodes.Attributes.Append(pBookId);
						pBookId = xmlDocument.CreateAttribute("Description");
						pBookId.Value = strArrays[1];
						xmlNodes.Attributes.Append(pBookId);
					}
				}
			}
			pBookId = xmlDocument.CreateAttribute("Update");
			pBookId.Value = date;
			xmlNodes.Attributes.Append(pBookId);
			return xmlNodes;
		}

		private void CreateForms()
		{
			this.objFrmTreeview = new frmViewerTreeview(this)
			{
				HideOnClose = true
			};
			this.objFrmPicture = new frmViewerPicture(this)
			{
				HideOnClose = true
			};
			this.objFrmPartlist = new frmViewerPartslist(this)
			{
				HideOnClose = false
			};
			this.objFrmSelectionlist = new frmSelectionList(this)
			{
				HideOnClose = true
			};
			this.objFrmInfo = new frmInfo(this)
			{
				HideOnClose = true
			};
		}

		public string CurrentImageSource()
		{
			string empty;
			try
			{
				empty = this.objFrmPicture.CurrentImageSource();
			}
			catch
			{
				empty = string.Empty;
			}
			return empty;
		}

		private void DeleteBookmarkPage(CustomToolStripMenuItem sender)
		{
			string empty = string.Empty;
			try
			{
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				empty = string.Concat(empty, "\\", Application.ProductName);
				empty = string.Concat(empty, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				empty = string.Concat(empty, "\\BookMarks.xml");
				XmlDocument xmlDocument = new XmlDocument();
				XmlNode xmlNodes = null;
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(sender.Tag.ToString()));
				xmlNodes = xmlDocument.ReadNode(xmlTextReader);
				if (File.Exists(empty))
				{
					xmlDocument.Load(empty);
					string str = "//BookMark";
					foreach (XmlAttribute attribute in xmlNodes.Attributes)
					{
						string[] name = new string[] { str, "[@", attribute.Name, "='", attribute.Value, "']" };
						str = string.Concat(name);
					}
					XmlNode xmlNodes1 = xmlDocument.SelectSingleNode(str);
					if (xmlNodes1 != null && MessageBox.Show(this.GetResource("Are you sure you want to delete this bookmark?", "DELETE_BOOKMARK?", ResourceType.POPUP_MESSAGE), this.Text.Trim(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						xmlNodes1.ParentNode.RemoveChild(xmlNodes1);
						this.bookmarksToolStripMenuItem.DropDown.Items.RemoveByKey(sender.Name);
						if (this.bookmarksToolStripMenuItem.DropDown.Items.Count < 3)
						{
							this.toolStripSeparator.Visible = false;
						}
					}
					if (xmlDocument.ChildNodes.Count == 2)
					{
						xmlDocument.Save(empty);
						xmlDocument = null;
					}
				}
			}
			catch
			{
				MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM006) Failed to delete specified object", "(E-VWR-EM006)_FAILED_DELETE", ResourceType.POPUP_MESSAGE));
			}
		}

		private void DeletePicLocalMemos()
		{
			string empty = string.Empty;
			string str = string.Empty;
			XmlDocument xmlDocument = new XmlDocument();
			XmlNodeList xmlNodeLists = null;
			try
			{
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				string empty1 = string.Empty;
				empty = string.Concat(empty, "\\", "GSPcLocalViewer");
				empty = string.Concat(empty, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				empty = string.Concat(empty, "\\LocalMemo.xml");
				if (File.Exists(empty))
				{
					xmlDocument.Load(empty);
					xmlNodeLists = xmlDocument.SelectNodes("//Memos/Memo");
					if (xmlNodeLists.Count > 0)
					{
						foreach (XmlNode xmlNodes in xmlNodeLists)
						{
							XmlAttribute itemOf = xmlNodes.Attributes["PartNo"];
							string str1 = xmlNodes.Attributes["BookId"].Value.ToString();
							string str2 = itemOf.Value.ToString();
							string str3 = xmlNodes.Attributes["Type"].Value.ToString();
							if (!(str1 == this.p_BookId) || !(str2 == "") || !(str3.ToUpper() == "TXT"))
							{
								continue;
							}
							xmlNodes.ParentNode.RemoveChild(xmlNodes);
						}
					}
					xmlDocument.Save(empty);
				}
			}
			catch (Exception exception)
			{
			}
		}

		public void DeleteSelListRow(string sPartNumber)
		{
			try
			{
				this.frmParent.DeleteRow(sPartNumber);
				this.frmParent.CheckUncheckRow(sPartNumber, false);
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

		public void DisposeNotification()
		{
			this.frmParent.DisposeNotification();
		}

		public void EnableAddPartMemoMenu(bool value)
		{
			if (this.sBookType.ToUpper() == "GSC")
			{
				this.addPartMemoToolStripMenuItem.Visible = false;
				return;
			}
			this.addPartMemoToolStripMenuItem.Enabled = value;
		}

		public void EnableAddPicMemoMenu(bool value)
		{
			this.addPictureMemoToolStripMenuItem.Visible = value;
		}

		public void EnableAddPicMemoTSB(bool value)
		{
			if (!this.tsFunctions.InvokeRequired)
			{
				this.tsbAddPictureMemo.Enabled = value;
				return;
			}
			ToolStrip toolStrip = this.tsFunctions;
			frmViewer.EnableAddMemoDelegate enableAddMemoDelegate = new frmViewer.EnableAddMemoDelegate(this.EnableAddPicMemoTSB);
			object[] objArray = new object[] { value };
			toolStrip.Invoke(enableAddMemoDelegate, objArray);
		}

		private void EnableMenuAndToolbar(bool value)
		{
			this.viewToolStripMenuItem.Enabled = value;
			this.settingsToolStripMenuItem.Enabled = value;
			this.bookmarksToolStripMenuItem.Enabled = value;
			this.searchToolStripMenuItem.Enabled = value;
			this.singleBookToolStripMenuItem.Enabled = value;
			this.multipleBooksToolStripMenuItem.Enabled = value;
			this.addOnToolStripMenuItem.Enabled = value;
			this.printToolStripMenuItem.Enabled = value;
			this.goToPortalToolStripMenuItem.Enabled = value;
			this.navigationToolStripMenuItem.Enabled = value;
			this.memoDetailsToolStripMenuItem.Enabled = value;
			this.tsView.Enabled = value;
			this.tsSearch.Enabled = value;
			this.tsNavigate.Enabled = value;
			this.tsbPrint.Enabled = value;
			this.tsbAddBookmarks.Enabled = value;
			this.tsbConfiguration.Enabled = value;
			this.tsbAddPictureMemo.Enabled = value;
			this.tsbMemoList.Enabled = value;
			this.tsbPortal.Enabled = value;
			this.tsbMemoRecovery.Enabled = value;
			this.tsbSingleBookDownload.Enabled = value;
			this.tsbMultipleBooksDownload.Enabled = value;
			this.tsbThirdPartyBasket.Enabled = value;
			this.tsPic.Enabled = false;
		}

		public void EnableNavigationItems(bool bFirst, bool bPrev, bool bNext, bool bLast)
		{
			this.tsbNavigateFirst.Enabled = bFirst;
			this.tsbNavigatePrevious.Enabled = bPrev;
			this.tsbNavigateNext.Enabled = bNext;
			this.tsbNavigateLast.Enabled = bLast;
			this.firstPageToolStripMenuItem.Enabled = bFirst;
			this.previousPageToolStripMenuItem.Enabled = bPrev;
			this.nextPageToolStripMenuItem.Enabled = bNext;
			this.lastPageToolStripMenuItem.Enabled = bLast;
		}

		public void EnablePartslistShowHideButton(bool value)
		{
			if (this.sBookType.ToUpper() == "GSC")
			{
				this.partslistToolStripMenuItem.Visible = false;
				this.tsbViewPartslist.Visible = false;
				return;
			}
			this.partslistToolStripMenuItem.Enabled = value;
			this.tsbViewPartslist.Enabled = value;
		}

		public void EnablePictureShowHideButton(bool value)
		{
			this.pictureToolStripMenuItem.Enabled = value;
			this.tsbViewPicture.Enabled = value;
		}

		public void EnableSelectionlistShowHideButton(bool value)
		{
			if (this.sBookType.ToUpper() == "GSC")
			{
				this.selectionListToolStripMenuItem.Visible = false;
				return;
			}
			this.selectionListToolStripMenuItem.Enabled = value;
		}

		public void EnableTreeView(bool value)
		{
			if (!value)
			{
				this.toolStripContainer1.TopToolStripPanel.Enabled = true;
				this.toolStripContainer1.LeftToolStripPanel.Enabled = true;
				this.toolStripContainer1.RightToolStripPanel.Enabled = true;
				this.toolStripContainer1.BottomToolStripPanel.Enabled = true;
				if (!this.objFrmPicture.IsDisposed)
				{
					this.objFrmPicture.Enabled = true;
				}
				if (!this.objFrmPartlist.IsDisposed)
				{
					this.objFrmPartlist.Enabled = true;
				}
				if (!this.objFrmInfo.IsDisposed)
				{
					this.objFrmInfo.Enabled = true;
				}
				if (!this.objFrmSelectionlist.IsDisposed)
				{
					this.objFrmSelectionlist.Enabled = true;
				}
				base.Enabled = false;
			}
			else
			{
				base.Enabled = true;
				this.toolStripContainer1.TopToolStripPanel.Enabled = false;
				this.toolStripContainer1.LeftToolStripPanel.Enabled = false;
				this.toolStripContainer1.RightToolStripPanel.Enabled = false;
				this.toolStripContainer1.BottomToolStripPanel.Enabled = false;
				if (!this.objFrmTreeview.IsDisposed)
				{
					this.objFrmTreeview.Enabled = true;
				}
				if (!this.objFrmPicture.IsDisposed)
				{
					this.objFrmPicture.Enabled = false;
				}
				if (!this.objFrmPartlist.IsDisposed)
				{
					this.objFrmPartlist.Enabled = false;
				}
				if (!this.objFrmInfo.IsDisposed)
				{
					this.objFrmInfo.Enabled = false;
				}
				if (!this.objFrmSelectionlist.IsDisposed)
				{
					this.objFrmSelectionlist.Enabled = false;
					return;
				}
			}
		}

		private void englishToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LoadXML("English");
			this.objFrmInfo.LoadResources();
			this.objFrmPartlist.LoadResources();
			this.objFrmPicture.LoadResources();
			this.objFrmSelectionlist.LoadResources();
			this.objFrmTreeview.LoadResources();
			this.ResetBookMarkTooltipLanguage();
		}

		public int ExportCurrentImage(string filename, string fmt, bool bNeedAnno, int pageIndex, bool bCurZoom, int full, int fullT, int fullR, int fullB, int viewL, int viewT, int viewR, int viewB)
		{
			return this.objFrmPicture.ExportCurrentImage(filename, fmt, bNeedAnno, pageIndex, bCurZoom, full, fullT, fullR, fullB, viewL, viewT, viewR, viewB);
		}

		public XmlNodeList FilterBooksList(XmlNode xSchemaNode, XmlNodeList xNodeList)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("ID"))
				{
					arrayLists.Add(attribute.Name);
				}
				else if (!attribute.Value.ToUpper().Equals("PUBLISHINGID"))
				{
					if (!attribute.Value.ToUpper().Equals("UPDATEDATE"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.p_ArgsF == null || (int)this.p_ArgsF.Length <= 0)
			{
				return xNodeList;
			}
			return this.FilterNodeList(xNodeList, arrayLists);
		}

		private XmlNode FilterNode(XmlNode xNode, ArrayList arrMandatory)
		{
			string[] strArrays;
			bool flag = false;
			if (xNode.HasChildNodes)
			{
				foreach (XmlNode childNode in xNode.ChildNodes)
				{
					if (childNode.Name.ToUpper() == "SEL")
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value = childNode.Attributes[0].Value;
						string[] strArrays1 = new string[] { "," };
						strArrays = value.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays2 = strArrays;
						int num = 0;
						while (num < (int)strArrays2.Length)
						{
							if (!arrMandatory.Contains(strArrays2[num]))
							{
								num++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF = this.p_ArgsF;
						for (int i = 0; i < (int)pArgsF.Length; i++)
						{
							string str = pArgsF[i];
							string[] strArrays3 = new string[] { "=" };
							string[] strArrays4 = str.Split(strArrays3, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays4.Length == 2)
							{
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									if (!(attribute.Name.ToUpper() == strArrays4[0].ToUpper()) || this.ValueMatchesSelectFilter(strArrays4[1], attribute.Value, false))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays5 = strArrays;
										for (int j = 0; j < (int)strArrays5.Length; j++)
										{
											string str1 = strArrays5[j];
											if (xNode.Attributes[str1] != null)
											{
												xNode.Attributes.Remove(xNode.Attributes[str1]);
											}
										}
									}
									else
									{
										xNode.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else if (childNode.Name.ToUpper() != "SWT")
					{
						if (!(childNode.Name.ToUpper() == "RNG") || childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value1 = childNode.Attributes[0].Value;
						string[] strArrays6 = new string[] { "," };
						strArrays = value1.Split(strArrays6, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays7 = strArrays;
						int num1 = 0;
						while (num1 < (int)strArrays7.Length)
						{
							if (!arrMandatory.Contains(strArrays7[num1]))
							{
								num1++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF1 = this.p_ArgsF;
						for (int k = 0; k < (int)pArgsF1.Length; k++)
						{
							string str2 = pArgsF1[k];
							string[] strArrays8 = new string[] { "=" };
							string[] strArrays9 = str2.Split(strArrays8, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays9.Length == 2)
							{
								foreach (XmlAttribute xmlAttribute in childNode.Attributes)
								{
									if (!(xmlAttribute.Name.ToUpper() == strArrays9[0].ToUpper()) || this.ValueInRangeFilter(xmlAttribute.Value, strArrays9[1]))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays10 = strArrays;
										for (int l = 0; l < (int)strArrays10.Length; l++)
										{
											string str3 = strArrays10[l];
											if (xNode.Attributes[str3] != null)
											{
												xNode.Attributes.Remove(xNode.Attributes[str3]);
											}
										}
									}
									else
									{
										xNode.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value2 = childNode.Attributes[0].Value;
						string[] strArrays11 = new string[] { "," };
						strArrays = value2.Split(strArrays11, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays12 = strArrays;
						int num2 = 0;
						while (num2 < (int)strArrays12.Length)
						{
							if (!arrMandatory.Contains(strArrays12[num2]))
							{
								num2++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF2 = this.p_ArgsF;
						for (int m = 0; m < (int)pArgsF2.Length; m++)
						{
							string str4 = pArgsF2[m];
							string[] strArrays13 = new string[] { "=" };
							string[] strArrays14 = str4.Split(strArrays13, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays14.Length == 2)
							{
								foreach (XmlAttribute attribute1 in childNode.Attributes)
								{
									if (!(attribute1.Name.ToUpper() == strArrays14[0].ToUpper()) || !(strArrays14[1].ToUpper() == "ON") || !(attribute1.Value.ToUpper() == "ON"))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays15 = strArrays;
										for (int n = 0; n < (int)strArrays15.Length; n++)
										{
											string str5 = strArrays15[n];
											if (xNode.Attributes[str5] != null)
											{
												xNode.Attributes.Remove(xNode.Attributes[str5]);
											}
										}
									}
									else
									{
										xNode.RemoveAll();
										break;
									}
								}
							}
						}
					}
				}
			}
			return xNode;
		}

		private XmlNodeList FilterNodeList(XmlNodeList xNodeList, ArrayList arrMandatory)
		{
			string[] strArrays;
			bool flag = false;
			foreach (XmlNode xmlNodes in xNodeList)
			{
				if (!xmlNodes.HasChildNodes)
				{
					continue;
				}
				foreach (XmlNode childNode in xmlNodes.ChildNodes)
				{
					if (childNode.Name.ToUpper() == "SEL")
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value = childNode.Attributes[0].Value;
						string[] strArrays1 = new string[] { "," };
						strArrays = value.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays2 = strArrays;
						int num = 0;
						while (num < (int)strArrays2.Length)
						{
							if (!arrMandatory.Contains(strArrays2[num]))
							{
								num++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF = this.p_ArgsF;
						for (int i = 0; i < (int)pArgsF.Length; i++)
						{
							string str = pArgsF[i];
							string[] strArrays3 = new string[] { "=" };
							string[] strArrays4 = str.Split(strArrays3, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays4.Length == 2)
							{
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									if (!(attribute.Name.ToUpper() == strArrays4[0].ToUpper()) || this.ValueMatchesSelectFilter(strArrays4[1], attribute.Value, false))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays5 = strArrays;
										for (int j = 0; j < (int)strArrays5.Length; j++)
										{
											string str1 = strArrays5[j];
											if (xmlNodes.Attributes[str1] != null)
											{
												xmlNodes.Attributes.Remove(xmlNodes.Attributes[str1]);
											}
										}
									}
									else
									{
										xmlNodes.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else if (childNode.Name.ToUpper() != "SWT")
					{
						if (!(childNode.Name.ToUpper() == "RNG") || childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value1 = childNode.Attributes[0].Value;
						string[] strArrays6 = new string[] { "," };
						strArrays = value1.Split(strArrays6, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays7 = strArrays;
						int num1 = 0;
						while (num1 < (int)strArrays7.Length)
						{
							if (!arrMandatory.Contains(strArrays7[num1]))
							{
								num1++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF1 = this.p_ArgsF;
						for (int k = 0; k < (int)pArgsF1.Length; k++)
						{
							string str2 = pArgsF1[k];
							string[] strArrays8 = new string[] { "=" };
							string[] strArrays9 = str2.Split(strArrays8, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays9.Length == 2)
							{
								foreach (XmlAttribute xmlAttribute in childNode.Attributes)
								{
									if (!(xmlAttribute.Name.ToUpper() == strArrays9[0].ToUpper()) || this.ValueInRangeFilter(xmlAttribute.Value, strArrays9[1]))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays10 = strArrays;
										for (int l = 0; l < (int)strArrays10.Length; l++)
										{
											string str3 = strArrays10[l];
											if (xmlNodes.Attributes[str3] != null)
											{
												xmlNodes.Attributes.Remove(xmlNodes.Attributes[str3]);
											}
										}
									}
									else
									{
										xmlNodes.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value2 = childNode.Attributes[0].Value;
						string[] strArrays11 = new string[] { "," };
						strArrays = value2.Split(strArrays11, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays12 = strArrays;
						int num2 = 0;
						while (num2 < (int)strArrays12.Length)
						{
							if (!arrMandatory.Contains(strArrays12[num2]))
							{
								num2++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF2 = this.p_ArgsF;
						for (int m = 0; m < (int)pArgsF2.Length; m++)
						{
							string str4 = pArgsF2[m];
							string[] strArrays13 = new string[] { "=" };
							string[] strArrays14 = str4.Split(strArrays13, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays14.Length == 2)
							{
								foreach (XmlAttribute attribute1 in childNode.Attributes)
								{
									if (!(attribute1.Name.ToUpper() == strArrays14[0].ToUpper()) || !(strArrays14[1].ToUpper() == "ON") || !(attribute1.Value.ToUpper() == "ON"))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays15 = strArrays;
										for (int n = 0; n < (int)strArrays15.Length; n++)
										{
											string str5 = strArrays15[n];
											if (xmlNodes.Attributes[str5] != null)
											{
												xmlNodes.Attributes.Remove(xmlNodes.Attributes[str5]);
											}
										}
									}
									else
									{
										xmlNodes.RemoveAll();
										break;
									}
								}
							}
						}
					}
				}
			}
			return xNodeList;
		}

		public XmlNode FilterPage(XmlNode xSchemaNode, XmlNode xNode)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (!attribute.Value.ToUpper().Equals("ID"))
				{
					if (!attribute.Value.ToUpper().Equals("PAGENAME"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.p_ArgsF == null || (int)this.p_ArgsF.Length <= 0)
			{
				return xNode;
			}
			return this.FilterNode(xNode, arrayLists);
		}

		public XmlNodeList FilterPartsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("ID"))
				{
					arrayLists.Add(attribute.Name);
				}
				else if (!attribute.Value.ToUpper().Equals("LINKNUMBER"))
				{
					if (!attribute.Value.ToUpper().Equals("PARTNUMBER"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.p_ArgsF == null || (int)this.p_ArgsF.Length <= 0)
			{
				return xNodeList;
			}
			return this.FilterNodeList(xNodeList, arrayLists);
		}

		public XmlNodeList FilterPicsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
				{
					arrayLists.Add(attribute.Name);
				}
				else if (!attribute.Value.ToUpper().Equals("UPDATEDATE"))
				{
					if (!attribute.Value.ToUpper().Equals("UPDATEDATEPIC"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.p_ArgsF == null || (int)this.p_ArgsF.Length <= 0)
			{
				return xNodeList;
			}
			return this.FilterNodeList(xNodeList, arrayLists);
		}

		private void firstPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbNavigateFirst_Click(null, null);
		}

		private void fontAndColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Viewer_Font);
		}

		private void frmViewer_Activated(object sender, EventArgs e)
		{
			bool flag = false;
			if (this.objFrmPartlist != null && (this.objFrmPartlist.dgPartslist.RowsDefaultCellStyle.SelectionBackColor != Settings.Default.appHighlightBackColor || this.objFrmPartlist.dgPartslist.RowsDefaultCellStyle.SelectionForeColor != Settings.Default.appHighlightForeColor || this.objFrmPartlist.pnlInfo.BackColor != Settings.Default.PartsListInfoBackColor || this.objFrmPartlist.pnlInfo.ForeColor != Settings.Default.PartsListInfoForeColor))
			{
				flag = true;
			}
			if (flag || this.Font.Name != Settings.Default.appFont.Name || this.Font.Size != Settings.Default.appFont.Size)
			{
				this.pnlForm.Visible = false;
				this.UpdateFont();
				this.pnlForm.Visible = true;
			}
		}

		private void frmViewer_Deactivate(object sender, EventArgs e)
		{
			base.TopMost = false;
		}

		private void frmViewer_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				this.frmParent.CloseViewer(this);
			}
			catch
			{
			}
		}

		private void frmViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
			while (!this.bObjFrmTreeviewClosed || !this.bObjFrmPictureClosed || !this.bObjFrmPartlistClosed || !this.bObjFrmSelectionlistClosed)
			{
				Thread.Sleep(300);
				Application.DoEvents();
			}
			this.objFrmPicture.DisposeDjVuControl();
			try
			{
				this.objFrmPartlist.IsHidden = this.bPartsListClosed;
			}
			catch
			{
			}
			this.WriteUserSettingINI();
			ToolStripManager.SaveSettings(this);
			this.CopyConfigurationFile();
			Settings.Default.Save();
			if (this.objFrmTreeview.DockPanel != null && this.objFrmPicture.DockPanel != null && this.objFrmSelectionlist.DockPanel != null && this.objFrmPartlist.DockPanel != null && this.objFrmInfo.DockPanel != null)
			{
				this.objDocPanel.SaveAsXml(Program.configPath);
			}
			this.objFrmSelectionlist.SaveSelectionListColumnSizes();
			this.objFrmPartlist.SavePartsListColumnSizes();
			try
			{
				this.objHistory = null;
				if (this.objFrmPicture.wbPDF != null)
				{
					this.objFrmPicture.wbPDF.Dispose();
					Application.DoEvents();
					frmViewer.CoFreeUnusedLibraries();
				}
			}
			catch
			{
			}
			if (this.bPicMemoDelete && Settings.Default.EnableLocalMemo)
			{
				this.DeletePicLocalMemos();
			}
		}

		private void frmViewer_Load(object sender, EventArgs e)
		{
			base.WindowState = FormWindowState.Maximized;
			base.TopMost = true;
			base.Focus();
			string empty = string.Empty;
			if (this.p_ArgsS != null)
			{
				string[] pArgsS = this.p_ArgsS;
				int num = 0;
				while (num < (int)pArgsS.Length)
				{
					string str = pArgsS[num];
					if (!str.ToUpper().Contains("LNG="))
					{
						num++;
					}
					else
					{
						empty = str.Substring(str.IndexOf("=") + 1, str.Length - (str.IndexOf("=") + 1)).Trim().ToUpper();
						break;
					}
				}
			}
			if (empty != string.Empty)
			{
				Settings.Default.appLanguage = empty;
			}
			this.LoadXML(empty);
			ToolStripManager.LoadSettings(this);
			this.EnableMenuAndToolbar(false);
			this.CreateForms();
			this.ShowHidePictureToolbar();
			this.ShowHidePartslistToolbar();
			this.OnOffFeatures();
			if (this.p_ArgsO == null || (int)this.p_ArgsO.Length == 0)
			{
				frmOpenBook _frmOpenBook = new frmOpenBook(this);
				this.ShowDimmer();
				_frmOpenBook.ShowDialog();
			}
			else
			{
				this.LoadDataDirect();
			}
			if (Program.objAppMode.bFromPortal)
			{
				Program.objAppFeatures.bDcMode = false;
				if (!Program.objAppMode.bFirstTime)
				{
					this.workOffLineMenuItem.Checked = Program.objAppMode.bWorkingOffline;
				}
				else
				{
					Program.objAppMode.bWorkingOffline = !Program.objAppMode.InternetConnected;
					this.workOffLineMenuItem.Checked = Program.objAppMode.bWorkingOffline;
				}
			}
			else if (!Program.objAppFeatures.bDcMode)
			{
				Program.objAppMode.bWorkingOffline = !Program.objAppMode.InternetConnected;
				this.workOffLineMenuItem.Checked = Program.objAppMode.bWorkingOffline;
			}
			else
			{
				Program.objAppMode.bWorkingOffline = true;
				this.workOffLineMenuItem.Enabled = false;
			}
			if (!Program.objAppMode.bWorkingOffline)
			{
				this.lblMode.Text = "Working Online";
				this.lblMode.Image = GSPcLocalViewer.Properties.Resources.online;
			}
			else
			{
				this.lblMode.Text = "Working Offline";
				this.lblMode.Image = GSPcLocalViewer.Properties.Resources.offline;
			}
			this.contentsToolStripMenuItem.Checked = this.objFrmTreeview.Visible;
			this.pictureToolStripMenuItem.Checked = this.objFrmPicture.Visible;
			this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
			this.partslistToolStripMenuItem.Checked = this.objFrmPartlist.Visible;
			this.informationToolStripMenuItem.Checked = this.objFrmInfo.Visible;
			if (!Program.objAppFeatures.bMiniMap || !this.miniMapToolStripMenuItem.Checked || !(frmViewer.iniValueMiniMap == "true"))
			{
				frmViewer.MiniMapChk = false;
				this.miniMapToolStripMenuItem.Checked = false;
			}
			else
			{
				frmViewer.MiniMapChk = true;
			}
			this.GetPicMemoDeleteValue();
		}

		private void frmViewer_SizeChanged(object sender, EventArgs e)
		{
			try
			{
				if (base.WindowState != FormWindowState.Minimized)
				{
					Settings.Default.appCurrentSize = frmViewer.SavefrmViewerSizeSettings(this);
					Settings.Default.Save();
				}
			}
			catch (Exception exception)
			{
				string message = exception.Message;
			}
		}

		private void generalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Viewer_General);
		}

		public XmlNode GetBookNode(string sBookPublishingId, int iServerId)
		{
			XmlNode xmlNodes;
			XmlDocument xmlDocument = new XmlDocument();
			bool pCompressed = false;
			bool pEncrypted = false;
			string item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
			if (iServerId == -1 || iServerId == 9999)
			{
				item = string.Concat(item, "\\", Program.iniServers[this.p_ServerId].sIniKey, "\\Series.xml");
				pCompressed = this.p_Compressed;
				pEncrypted = this.p_Encrypted;
			}
			else
			{
				item = string.Concat(item, "\\", Program.iniServers[iServerId].sIniKey, "\\Series.xml");
				if (Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
				{
					pEncrypted = true;
				}
				if (Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
				{
					pCompressed = true;
				}
			}
			if (!File.Exists(item) && !Program.objAppMode.bWorkingOffline)
			{
				string empty = string.Empty;
				string str = string.Empty;
				empty = Program.iniServers[iServerId].items["SETTINGS", "CONTENT_PATH"];
				if (!empty.EndsWith("/"))
				{
					empty = string.Concat(empty, "/");
				}
				str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				str = string.Concat(str, "\\", Program.iniServers[iServerId].sIniKey);
				if (!Directory.Exists(str))
				{
					Directory.CreateDirectory(str);
				}
				if (!this.p_Compressed)
				{
					empty = string.Concat(empty, "Series.xml");
					str = string.Concat(str, "\\Series.xml");
				}
				else
				{
					empty = string.Concat(empty, "Series.zip");
					str = string.Concat(str, "\\Series.zip");
				}
				this.objDownloader.DownloadFile(empty, str);
			}
			if (!File.Exists(item))
			{
				return null;
			}
			if (!pCompressed)
			{
				try
				{
					xmlDocument.Load(item);
				}
				catch
				{
					xmlNodes = null;
					return xmlNodes;
				}
			}
			else
			{
				try
				{
					string str1 = item.ToLower().Replace(".zip", ".xml");
					Global.Unzip(str1);
					if (File.Exists(str1))
					{
						xmlDocument.Load(str1);
					}
				}
				catch
				{
				}
			}
			if (pEncrypted)
			{
				try
				{
					string str2 = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
					xmlDocument.DocumentElement.InnerXml = str2;
				}
				catch
				{
					xmlNodes = null;
					return xmlNodes;
				}
			}
			XmlNode xmlNodes1 = xmlDocument.SelectSingleNode("//Schema");
			if (xmlNodes1 == null)
			{
				return null;
			}
			string name = "";
			foreach (XmlAttribute attribute in xmlNodes1.Attributes)
			{
				if (!attribute.Value.ToUpper().Equals("PUBLISHINGID"))
				{
					continue;
				}
				name = attribute.Name;
			}
			if (name == "")
			{
				return null;
			}
			string[] upper = new string[] { "//Books/Book[translate(@", name, ", 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", sBookPublishingId.ToUpper(), "']" };
			return xmlDocument.SelectSingleNode(string.Concat(upper));
		}

		private IDockContent GetContentFromPersistString(string persistString)
		{
			if (persistString == typeof(frmViewerTreeview).ToString())
			{
				return this.objFrmTreeview;
			}
			if (persistString == typeof(frmViewerPicture).ToString())
			{
				return this.objFrmPicture;
			}
			if (persistString == typeof(frmSelectionList).ToString())
			{
				return this.objFrmSelectionlist;
			}
			if (persistString == typeof(frmViewerPartslist).ToString())
			{
				return this.objFrmPartlist;
			}
			if (persistString != typeof(frmInfo).ToString())
			{
				return null;
			}
			return this.objFrmInfo;
		}

		public string GetDjVuZoom()
		{
			return this.objFrmPicture.GetDjVuZoom();
		}

		public string[] getFilterArgs()
		{
			return this.p_ArgsF;
		}

		private string GetFocusedWindow()
		{
			string empty = string.Empty;
			if (this.objFrmPicture.IsActivated)
			{
				empty = "Picture";
			}
			else if (this.objFrmTreeview.IsActivated)
			{
				empty = "Content";
			}
			else if (this.objFrmSelectionlist.IsActivated)
			{
				empty = "SelectionList";
			}
			else if (this.objFrmPartlist.IsActivated)
			{
				empty = "Partlist";
			}
			else if (this.objFrmInfo.IsActivated)
			{
				empty = "Info";
			}
			return empty;
		}

		public int[] GetImageZoom()
		{
			return this.objFrmPicture.GetImageZoom();
		}

		public string GetMemoSortType()
		{
			string str;
			try
			{
				if (Program.iniServers[this.p_ServerId].items["MEMO_SETTING", "MEMO_SORT"] == null)
				{
					str = "UNKNOWN";
				}
				else if (Program.iniServers[this.p_ServerId].items["MEMO_SETTING", "MEMO_SORT"].ToString().ToUpper() != "DESC")
				{
					str = (Program.iniServers[this.p_ServerId].items["MEMO_SETTING", "MEMO_SORT"].ToString().ToUpper() != "ASC" ? "UNKNOWN" : "ASC");
				}
				else
				{
					str = "DESC";
				}
			}
			catch (Exception exception)
			{
				str = "UNKNOWN";
			}
			return str;
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

		public string GetOSLanguage()
		{
			CultureInfo.CurrentCulture.ClearCachedData();
			string displayName = CultureInfo.CurrentCulture.DisplayName;
			char[] chrArray = new char[] { ' ' };
			return displayName.Split(chrArray)[0].ToString();
		}

		private void GetPicMemoDeleteValue()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.p_ServerId].sIniKey, ".ini");
				ArrayList arrayLists = new ArrayList();
				arrayLists = (new IniFileIO()).GetKeys(str, "PIC_SETTINGS");
				int num = 0;
				while (num < arrayLists.Count)
				{
					IniFileIO iniFileIO = new IniFileIO();
					string keyValue = iniFileIO.GetKeyValue("PIC_SETTINGS", arrayLists[num].ToString(), str);
					if (arrayLists[num].ToString() != "LOCAL_TEXT_MEM_DELETE")
					{
						num++;
					}
					else if (keyValue.ToString().ToUpper() != "ON")
					{
						if (keyValue.ToString().ToUpper() != "OFF")
						{
							break;
						}
						this.bPicMemoDelete = false;
						break;
					}
					else
					{
						this.bPicMemoDelete = true;
						break;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private int GetPlPartNumColIndex()
		{
			int num = 0;
			for (int i = 0; i < this.objFrmPartlist.dgPartslist.Columns.Count; i++)
			{
				if (this.objFrmPartlist.dgPartslist.Columns[i].Tag != null && this.objFrmPartlist.dgPartslist.Columns[i].Tag.ToString() == "PartNumber")
				{
					num = i;
				}
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
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
					}
					str = string.Concat(str, "/Resource[@Name='", sKey, "']");
					resourceValue = this.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		public string GetResourceValue(string sDefaultValue, string xQuery)
		{
			string str;
			try
			{
				string str1 = string.Concat("/GSPcLocalViewer", xQuery);
				XmlNode xmlNodes = this.xmlDocument.SelectSingleNode(str1);
				if (xmlNodes == null)
				{
					str = sDefaultValue;
				}
				else if (this.xmlDocument == null)
				{
					str = sDefaultValue;
				}
				else if (xmlNodes.Attributes.Count <= 0 || xmlNodes.Attributes["Value"] == null)
				{
					str = sDefaultValue;
				}
				else
				{
					string value = xmlNodes.Attributes["Value"].Value;
					str = (string.IsNullOrEmpty(value) ? sDefaultValue : value);
				}
			}
			catch (Exception exception)
			{
				str = sDefaultValue;
			}
			return str;
		}

		private void GetSaveMemoValue()
		{
			try
			{
				if (Program.iniServers[this.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"] != null && Program.iniServers[this.p_ServerId].items["PART_MEMO", "SAME_PART_NUMBER_TARGET"].ToString().ToUpper() == "ON")
				{
					this.bSaveMemoOnBookLevel = true;
				}
			}
			catch (Exception exception)
			{
				this.bSaveMemoOnBookLevel = false;
			}
		}

		public DataGridViewRowCollection GetSelectionList()
		{
			DataGridViewRowCollection selectionList;
			try
			{
				selectionList = this.frmParent.GetSelectionList();
			}
			catch
			{
				selectionList = null;
			}
			return selectionList;
		}

		public DataGridViewColumnCollection GetSelectionListColumns()
		{
			DataGridViewColumnCollection selectionListColumns;
			try
			{
				selectionListColumns = this.frmParent.GetSelectionListColumns();
			}
			catch
			{
				selectionListColumns = null;
			}
			return selectionListColumns;
		}

		private void goToPortalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbPortal_Click(null, null);
		}

		public void GSCMenuItems()
		{
			if (this.menuStrip1.InvokeRequired)
			{
				this.menuStrip1.Invoke(new frmViewer.GSCMenuItemsDelegate(this.GSCMenuItems));
				return;
			}
			this.partNameSearchSettingsToolStripMenuItem.Visible = false;
			this.partNumberSearchSettingsToolStripMenuItem.Visible = false;
			this.partNameToolStripMenuItem.Visible = false;
			this.partNumberToolStripMenuItem.Visible = false;
			this.advancedSearchToolStripMenuItem.Visible = false;
			this.printListToolStripMenuItem.Visible = false;
			this.printSelectionListToolStripMenuItem.Visible = false;
			this.toolStripSeparator6.Visible = false;
			this.toolStripSeparator5.Visible = false;
		}

		public void GSCToolBarItems()
		{
			if (this.tsSearch.InvokeRequired)
			{
				this.tsSearch.Invoke(new frmViewer.GSCToolBarItemsDelegate(this.GSCToolBarItems));
				return;
			}
			this.tsbSearchPartName.Visible = false;
			this.tsbSearchPartNumber.Visible = false;
			this.tsbSearchPartAdvance.Visible = false;
			this.tsbThirdPartyBasket.Visible = false;
		}

		private void gSPcLocalHelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				(new GSPcLocalHelp(this)).OpenHelpFile();
			}
			catch
			{
			}
		}

		public void HideDimmer()
		{
			this.objDimmer.Enabled = true;
			this.objDimmer.Hide();
			base.BringToFront();
		}

		public void HidePartsList()
		{
			if (!this.objFrmPartlist.InvokeRequired)
			{
				this.EnablePartslistShowHideButton(false);
				this.objFrmPartlist.Hide();
				return;
			}
			this.objFrmPartlist.Invoke(new frmViewer.HidePartsListDelegate(this.HidePartsList));
		}

		public void HidePicture()
		{
			if (this.objFrmPicture.InvokeRequired)
			{
				this.objFrmPicture.Invoke(new frmViewer.HidePartsListDelegate(this.HidePicture));
				return;
			}
			this.EnablePictureShowHideButton(false);
			this.objFrmPicture.Hide();
			this.bPictureClosed = true;
			this.miniMapToolStripMenuItem.Enabled = false;
			this.objFrmPicture.ShowHideMiniMap(false);
		}

		public void HideSelectionList()
		{
			if (!this.objFrmSelectionlist.InvokeRequired)
			{
				this.EnableSelectionlistShowHideButton(false);
				this.objFrmSelectionlist.Hide();
				return;
			}
			this.objFrmSelectionlist.Invoke(new frmViewer.HideSelectionListDelegate(this.HideSelectionList));
		}

		public void HighlightPartslist(string argName, string argValue)
		{
			this.objFrmPartlist.HighlightPartslist(argName, argValue);
		}

		public void HighlightPicture(int x, int y, int width, int height)
		{
			this.objFrmPicture.HighlightPicture(x, y, width, height);
		}

		private void HistoryToolStripItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.tsbHistoryBack.Enabled = false;
				this.tsbHistoryForward.Enabled = false;
				this.LoadDataFromNode(this.objHistory.Open(int.Parse(((ToolStripMenuItem)sender).Tag.ToString())));
			}
			catch
			{
			}
		}

		private void informationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.informationToolStripMenuItem.Checked = !this.informationToolStripMenuItem.Checked;
			if (!this.informationToolStripMenuItem.Checked)
			{
				this.objFrmInfo.Hide();
				return;
			}
			this.objFrmInfo.Show(this.objDocPanel);
		}

		public void InitializeAddOnsGUI()
		{
			string empty = string.Empty;
			string item = string.Empty;
			string str = string.Empty;
			int num = 0;
			IniFileIO iniFileIO = new IniFileIO();
			ArrayList arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSPcLocal.ini"), "ADDON");
			try
			{
				num = (Math.IEEERemainder((double)arrayLists.Count, 2) != 0 ? (arrayLists.Count - 1) / 2 : arrayLists.Count / 2);
			}
			catch
			{
				num = 0;
			}
			for (int i = 1; i <= num; i++)
			{
				string empty1 = string.Empty;
				try
				{
					empty = Program.iniGSPcLocal.items["ADDON", string.Concat("ADDON", i, "_NAME")];
					item = Program.iniGSPcLocal.items["ADDON", string.Concat("ADDON", i, "_PATH")];
					if (empty != string.Empty && item != string.Empty)
					{
						this.addOnToolStripMenuItem.Visible = true;
						this.addOnToolStripMenuItem.DropDown.Items.Add(empty);
						this.addOnToolStripMenuItem.DropDown.Items[this.addOnToolStripMenuItem.DropDown.Items.Count - 1].Tag = item;
						this.addOnToolStripMenuItem.DropDown.Items[this.addOnToolStripMenuItem.DropDown.Items.Count - 1].Click += new EventHandler(this.LaunchAddOn);
					}
				}
				catch
				{
				}
			}
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmViewer));
			DockPanelSkin dockPanelSkin = new DockPanelSkin();
			AutoHideStripSkin autoHideStripSkin = new AutoHideStripSkin();
			DockPanelGradient dockPanelGradient = new DockPanelGradient();
			TabGradient tabGradient = new TabGradient();
			DockPaneStripSkin dockPaneStripSkin = new DockPaneStripSkin();
			DockPaneStripGradient dockPaneStripGradient = new DockPaneStripGradient();
			TabGradient controlLightLight = new TabGradient();
			DockPanelGradient control = new DockPanelGradient();
			TabGradient controlLight = new TabGradient();
			DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient = new DockPaneStripToolWindowGradient();
			TabGradient activeCaption = new TabGradient();
			TabGradient controlText = new TabGradient();
			DockPanelGradient dockPanelGradient1 = new DockPanelGradient();
			TabGradient gradientInactiveCaption = new TabGradient();
			TabGradient transparent = new TabGradient();
			this.toolStripContainer1 = new ToolStripContainer();
			this.ssStatus = new StatusStrip();
			this.lblStatus = new ToolStripStatusLabel();
			this.lblMode = new ToolStripStatusLabel();
			this.pnlForm = new Panel();
			this.objDocPanel = new DockPanel();
			this.picDisable = new PictureBox();
			this.pnlForm2 = new Panel();
			this.menuStrip1 = new MenuStrip();
			this.fileToolStripMenuItem = new ToolStripMenuItem();
			this.openToolStripMenuItem = new ToolStripMenuItem();
			this.goToPortalToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator11 = new ToolStripSeparator();
			this.printToolStripMenuItem = new ToolStripMenuItem();
			this.printPageToolStripMenuItem = new ToolStripMenuItem();
			this.printPictureToolStripMenuItem = new ToolStripMenuItem();
			this.printListToolStripMenuItem = new ToolStripMenuItem();
			this.printSelectionListToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.workOffLineMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator21 = new ToolStripSeparator();
			this.closeToolStripMenuItem = new ToolStripMenuItem();
			this.closeAllToolStripMenuItem = new ToolStripMenuItem();
			this.navigationToolStripMenuItem = new ToolStripMenuItem();
			this.firstPageToolStripMenuItem = new ToolStripMenuItem();
			this.previousPageToolStripMenuItem = new ToolStripMenuItem();
			this.nextPageToolStripMenuItem = new ToolStripMenuItem();
			this.lastPageToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator12 = new ToolStripSeparator();
			this.previousViewToolStripMenuItem = new ToolStripMenuItem();
			this.nextViewToolStripMenuItem = new ToolStripMenuItem();
			this.viewToolStripMenuItem = new ToolStripMenuItem();
			this.contentsToolStripMenuItem = new ToolStripMenuItem();
			this.pictureToolStripMenuItem = new ToolStripMenuItem();
			this.miniMapToolStripMenuItem = new ToolStripMenuItem();
			this.partslistToolStripMenuItem = new ToolStripMenuItem();
			this.selectionListToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.informationToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.restoreDefaultsToolStripMenuItem = new ToolStripMenuItem();
			this.saveDefaultsToolStripMenuItem = new ToolStripMenuItem();
			this.bookmarksToolStripMenuItem = new ToolStripMenuItem();
			this.addBookmarksToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator = new ToolStripSeparator();
			this.memoDetailsToolStripMenuItem = new ToolStripMenuItem();
			this.addPictureMemoToolStripMenuItem = new ToolStripMenuItem();
			this.addPartMemoToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator16 = new ToolStripSeparator();
			this.viewMemoListToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator13 = new ToolStripSeparator();
			this.memoRecoveryToolStripMenuItem = new ToolStripMenuItem();
			this.searchToolStripMenuItem = new ToolStripMenuItem();
			this.pageNameToolStripMenuItem = new ToolStripMenuItem();
			this.textSearchToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.partNameToolStripMenuItem = new ToolStripMenuItem();
			this.partNumberToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.advancedSearchToolStripMenuItem = new ToolStripMenuItem();
			this.settingsToolStripMenuItem = new ToolStripMenuItem();
			this.fontToolStripMenuItem = new ToolStripMenuItem();
			this.ColorToolStripMenuItem = new ToolStripMenuItem();
			this.generalToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator14 = new ToolStripSeparator();
			this.memoSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator23 = new ToolStripSeparator();
			this.partsListSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.selectionListSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator9 = new ToolStripSeparator();
			this.pageNameSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.textSearceNameSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.partNameSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.partNumberSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.advanceSearchSettingsToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator15 = new ToolStripSeparator();
			this.manageDiskSpaceToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.connectionToolStripMenuItem = new ToolStripMenuItem();
			this.toolsToolStripMenuItem = new ToolStripMenuItem();
			this.singleBookToolStripMenuItem = new ToolStripMenuItem();
			this.multipleBooksToolStripMenuItem = new ToolStripMenuItem();
			this.helpToolStripMenuItem = new ToolStripMenuItem();
			this.gSPcLocalHelpToolStripMenuItem = new ToolStripMenuItem();
			this.aboutGSPcLocalToolStripMenuItem = new ToolStripMenuItem();
			this.addOnToolStripMenuItem = new ToolStripMenuItem();
			this.tsPortal = new ToolStrip();
			this.tsbPortal = new ToolStripButton();
			this.tsbOpenBook = new ToolStripButton();
			this.tsHistory = new ToolStrip();
			this.tsbHistoryBack = new ToolStripButton();
			this.tsbHistoryForward = new ToolStripButton();
			this.tsbHistoryList = new ToolStripDropDownButton();
			this.tsNavigate = new ToolStrip();
			this.tsbNavigateFirst = new ToolStripButton();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.tsbNavigatePrevious = new ToolStripButton();
			this.tsbNavigateNext = new ToolStripButton();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.tsbNavigateLast = new ToolStripButton();
			this.tsView = new ToolStrip();
			this.tsbViewContents = new ToolStripButton();
			this.tsbViewPicture = new ToolStripButton();
			this.tsbViewPartslist = new ToolStripButton();
			this.tsbViewSeparator = new ToolStripSeparator();
			this.tsbViewInfo = new ToolStripButton();
			this.toolStripSeparator17 = new ToolStripSeparator();
			this.tsbRestoreDefaults = new ToolStripButton();
			this.tsSearch = new ToolStrip();
			this.tsbSearchPageName = new ToolStripButton();
			this.tsbSearchText = new ToolStripButton();
			this.tsbSearchPartName = new ToolStripButton();
			this.tsbSearchPartNumber = new ToolStripButton();
			this.tsbSearchPartAdvance = new ToolStripButton();
			this.tsTools = new ToolStrip();
			this.tsbSingleBookDownload = new ToolStripButton();
			this.tsbMultipleBooksDownload = new ToolStripButton();
			this.toolStripSeparator18 = new ToolStripSeparator();
			this.tsbDataCleanup = new ToolStripButton();
			this.tsbConnection = new ToolStripButton();
			this.tsFunctions = new ToolStrip();
			this.tsbPrint = new ToolStripButton();
			this.tsbAddBookmarks = new ToolStripButton();
			this.tsbMemoList = new ToolStripButton();
			this.tsbMemoRecovery = new ToolStripButton();
			this.tsbConfiguration = new ToolStripButton();
			this.tsbThirdPartyBasket = new ToolStripButton();
			this.tsbAbout = new ToolStripButton();
			this.tsbHelp = new ToolStripButton();
			this.tsPic = new ToolStrip();
			this.tslPic = new ToolStripLabel();
			this.tsbPicPrev = new ToolStripButton();
			this.tstPicNo = new ToolStripTextBox();
			this.tsbPicNext = new ToolStripButton();
			this.toolStripSeparator10 = new ToolStripSeparator();
			this.tsbFindText = new ToolStripButton();
			this.tsbPicPanMode = new ToolStripButton();
			this.tsbPicZoomSelect = new ToolStripButton();
			this.tsbFitPage = new ToolStripButton();
			this.tsbPicCopy = new ToolStripButton();
			this.tsbPicSelectText = new ToolStripButton();
			this.toolStripSeparator19 = new ToolStripSeparator();
			this.tsbPicZoomIn = new ToolStripButton();
			this.tsbPicZoomOut = new ToolStripButton();
			this.toolStripSeparator22 = new ToolStripSeparator();
			this.tsBRotateLeft = new ToolStripButton();
			this.tsBRotateRight = new ToolStripButton();
			this.toolStripSeparator20 = new ToolStripSeparator();
			this.tsbAddPictureMemo = new ToolStripButton();
			this.tsbThumbnail = new ToolStripButton();
			this.bgWorker = new BackgroundWorker();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.ssStatus.SuspendLayout();
			this.pnlForm.SuspendLayout();
			((ISupportInitialize)this.picDisable).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.tsPortal.SuspendLayout();
			this.tsHistory.SuspendLayout();
			this.tsNavigate.SuspendLayout();
			this.tsView.SuspendLayout();
			this.tsSearch.SuspendLayout();
			this.tsTools.SuspendLayout();
			this.tsFunctions.SuspendLayout();
			this.tsPic.SuspendLayout();
			base.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.ssStatus);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlForm);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.pnlForm2);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1161, 369);
			this.toolStripContainer1.Dock = DockStyle.Fill;
			this.toolStripContainer1.Location = new Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(1161, 440);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsNavigate);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsPic);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsPortal);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsHistory);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsView);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsSearch);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsTools);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tsFunctions);
			this.ssStatus.Dock = DockStyle.None;
			ToolStripItemCollection items = this.ssStatus.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.lblStatus, this.lblMode };
			items.AddRange(toolStripItemArray);
			this.ssStatus.Location = new Point(0, 0);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new System.Drawing.Size(1161, 22);
			this.ssStatus.TabIndex = 1;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(1130, 17);
			this.lblStatus.Spring = true;
			this.lblStatus.Text = "Ready";
			this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
			this.lblMode.AccessibleRole = System.Windows.Forms.AccessibleRole.SpinButton;
			this.lblMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.lblMode.Image = (Image)componentResourceManager.GetObject("lblMode.Image");
			this.lblMode.ImageTransparentColor = Color.White;
			this.lblMode.Name = "lblMode";
			this.lblMode.Size = new System.Drawing.Size(16, 17);
			this.lblMode.Text = "toolStripStatusLabel1";
			this.lblMode.ToolTipText = "change mode";
			this.lblMode.Click += new EventHandler(this.lblMode_Click);
			this.pnlForm.BackColor = SystemColors.Control;
			this.pnlForm.Controls.Add(this.objDocPanel);
			this.pnlForm.Controls.Add(this.picDisable);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(2);
			this.pnlForm.Size = new System.Drawing.Size(1161, 369);
			this.pnlForm.TabIndex = 2;
			this.objDocPanel.ActiveAutoHideContent = null;
			this.objDocPanel.BackColor = SystemColors.Control;
			this.objDocPanel.Dock = DockStyle.Fill;
			this.objDocPanel.DockBackColor = SystemColors.ControlDark;
			this.objDocPanel.DocumentStyle = DocumentStyle.DockingWindow;
			this.objDocPanel.Location = new Point(2, 2);
			this.objDocPanel.Name = "objDocPanel";
			this.objDocPanel.Size = new System.Drawing.Size(1157, 365);
			dockPanelGradient.EndColor = SystemColors.ControlLight;
			dockPanelGradient.StartColor = SystemColors.ControlLight;
			autoHideStripSkin.DockStripGradient = dockPanelGradient;
			tabGradient.EndColor = SystemColors.Control;
			tabGradient.StartColor = SystemColors.Control;
			tabGradient.TextColor = SystemColors.ControlDarkDark;
			autoHideStripSkin.TabGradient = tabGradient;
			dockPanelSkin.AutoHideStripSkin = autoHideStripSkin;
			controlLightLight.EndColor = SystemColors.ControlLightLight;
			controlLightLight.StartColor = SystemColors.ControlLightLight;
			controlLightLight.TextColor = SystemColors.ControlText;
			dockPaneStripGradient.ActiveTabGradient = controlLightLight;
			control.EndColor = SystemColors.Control;
			control.StartColor = SystemColors.Control;
			dockPaneStripGradient.DockStripGradient = control;
			controlLight.EndColor = SystemColors.ControlLight;
			controlLight.StartColor = SystemColors.ControlLight;
			controlLight.TextColor = SystemColors.ControlText;
			dockPaneStripGradient.InactiveTabGradient = controlLight;
			dockPaneStripSkin.DocumentGradient = dockPaneStripGradient;
			activeCaption.EndColor = SystemColors.ActiveCaption;
			activeCaption.LinearGradientMode = LinearGradientMode.Vertical;
			activeCaption.StartColor = SystemColors.GradientActiveCaption;
			activeCaption.TextColor = SystemColors.ActiveCaptionText;
			dockPaneStripToolWindowGradient.ActiveCaptionGradient = activeCaption;
			controlText.EndColor = SystemColors.Control;
			controlText.StartColor = SystemColors.Control;
			controlText.TextColor = SystemColors.ControlText;
			dockPaneStripToolWindowGradient.ActiveTabGradient = controlText;
			dockPanelGradient1.EndColor = SystemColors.ControlLight;
			dockPanelGradient1.StartColor = SystemColors.ControlLight;
			dockPaneStripToolWindowGradient.DockStripGradient = dockPanelGradient1;
			gradientInactiveCaption.EndColor = SystemColors.GradientInactiveCaption;
			gradientInactiveCaption.LinearGradientMode = LinearGradientMode.Vertical;
			gradientInactiveCaption.StartColor = SystemColors.GradientInactiveCaption;
			gradientInactiveCaption.TextColor = SystemColors.ControlText;
			dockPaneStripToolWindowGradient.InactiveCaptionGradient = gradientInactiveCaption;
			transparent.EndColor = Color.Transparent;
			transparent.StartColor = Color.Transparent;
			transparent.TextColor = SystemColors.ControlDarkDark;
			dockPaneStripToolWindowGradient.InactiveTabGradient = transparent;
			dockPaneStripSkin.ToolWindowGradient = dockPaneStripToolWindowGradient;
			dockPanelSkin.DockPaneStripSkin = dockPaneStripSkin;
			this.objDocPanel.Skin = dockPanelSkin;
			this.objDocPanel.TabIndex = 2;
			this.picDisable.Dock = DockStyle.Fill;
			this.picDisable.Location = new Point(2, 2);
			this.picDisable.Name = "picDisable";
			this.picDisable.Size = new System.Drawing.Size(1157, 365);
			this.picDisable.TabIndex = 1;
			this.picDisable.TabStop = false;
			this.pnlForm2.Dock = DockStyle.Fill;
			this.pnlForm2.Location = new Point(0, 0);
			this.pnlForm2.Name = "pnlForm2";
			this.pnlForm2.Size = new System.Drawing.Size(1161, 369);
			this.pnlForm2.TabIndex = 1;
			this.menuStrip1.Dock = DockStyle.None;
			ToolStripItemCollection toolStripItemCollections = this.menuStrip1.Items;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.fileToolStripMenuItem, this.navigationToolStripMenuItem, this.viewToolStripMenuItem, this.bookmarksToolStripMenuItem, this.memoDetailsToolStripMenuItem, this.searchToolStripMenuItem, this.settingsToolStripMenuItem, this.toolsToolStripMenuItem, this.helpToolStripMenuItem, this.addOnToolStripMenuItem };
			toolStripItemCollections.AddRange(toolStripItemArray1);
			this.menuStrip1.Location = new Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1161, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			ToolStripItemCollection dropDownItems = this.fileToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray2 = new ToolStripItem[] { this.openToolStripMenuItem, this.goToPortalToolStripMenuItem, this.toolStripSeparator11, this.printToolStripMenuItem, this.toolStripSeparator2, this.workOffLineMenuItem, this.toolStripSeparator21, this.closeToolStripMenuItem, this.closeAllToolStripMenuItem };
			dropDownItems.AddRange(toolStripItemArray2);
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.openToolStripMenuItem.Text = "Open ...";
			this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
			this.goToPortalToolStripMenuItem.Name = "goToPortalToolStripMenuItem";
			this.goToPortalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.goToPortalToolStripMenuItem.Text = "Go to Portal";
			this.goToPortalToolStripMenuItem.Click += new EventHandler(this.goToPortalToolStripMenuItem_Click);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new System.Drawing.Size(138, 6);
			ToolStripItemCollection dropDownItems1 = this.printToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray3 = new ToolStripItem[] { this.printPageToolStripMenuItem, this.printPictureToolStripMenuItem, this.printListToolStripMenuItem, this.printSelectionListToolStripMenuItem };
			dropDownItems1.AddRange(toolStripItemArray3);
			this.printToolStripMenuItem.Name = "printToolStripMenuItem";
			this.printToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.printToolStripMenuItem.Text = "Print";
			this.printToolStripMenuItem.Click += new EventHandler(this.printToolStripMenuItem_Click);
			this.printPageToolStripMenuItem.Name = "printPageToolStripMenuItem";
			this.printPageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.printPageToolStripMenuItem.Text = "Print Page...";
			this.printPageToolStripMenuItem.Click += new EventHandler(this.printPageToolStripMenuItem_Click);
			this.printPictureToolStripMenuItem.Name = "printPictureToolStripMenuItem";
			this.printPictureToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.printPictureToolStripMenuItem.Text = "Print Picture...";
			this.printPictureToolStripMenuItem.Click += new EventHandler(this.printPictureToolStripMenuItem_Click);
			this.printListToolStripMenuItem.Name = "printListToolStripMenuItem";
			this.printListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.printListToolStripMenuItem.Text = "Print List...";
			this.printListToolStripMenuItem.Click += new EventHandler(this.printListToolStripMenuItem_Click);
			this.printSelectionListToolStripMenuItem.Name = "printSelectionListToolStripMenuItem";
			this.printSelectionListToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.printSelectionListToolStripMenuItem.Text = "Print Selection List...";
			this.printSelectionListToolStripMenuItem.Click += new EventHandler(this.printSelectionListToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(138, 6);
			this.workOffLineMenuItem.Name = "workOffLineMenuItem";
			this.workOffLineMenuItem.Size = new System.Drawing.Size(141, 22);
			this.workOffLineMenuItem.Text = "Work Offline";
			this.workOffLineMenuItem.Click += new EventHandler(this.workOffLineMenuItem_Click);
			this.toolStripSeparator21.Name = "toolStripSeparator21";
			this.toolStripSeparator21.Size = new System.Drawing.Size(138, 6);
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new EventHandler(this.closeToolStripMenuItem_Click);
			this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
			this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
			this.closeAllToolStripMenuItem.Text = "Close All";
			this.closeAllToolStripMenuItem.Click += new EventHandler(this.closeAllToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections1 = this.navigationToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray4 = new ToolStripItem[] { this.firstPageToolStripMenuItem, this.previousPageToolStripMenuItem, this.nextPageToolStripMenuItem, this.lastPageToolStripMenuItem, this.toolStripSeparator12, this.previousViewToolStripMenuItem, this.nextViewToolStripMenuItem };
			toolStripItemCollections1.AddRange(toolStripItemArray4);
			this.navigationToolStripMenuItem.Name = "navigationToolStripMenuItem";
			this.navigationToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
			this.navigationToolStripMenuItem.Text = "Navigation";
			this.firstPageToolStripMenuItem.Name = "firstPageToolStripMenuItem";
			this.firstPageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.firstPageToolStripMenuItem.Text = "First Page";
			this.firstPageToolStripMenuItem.Click += new EventHandler(this.firstPageToolStripMenuItem_Click);
			this.previousPageToolStripMenuItem.Name = "previousPageToolStripMenuItem";
			this.previousPageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.previousPageToolStripMenuItem.Text = "Previous Page";
			this.previousPageToolStripMenuItem.Click += new EventHandler(this.previousPageToolStripMenuItem_Click);
			this.nextPageToolStripMenuItem.Name = "nextPageToolStripMenuItem";
			this.nextPageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.nextPageToolStripMenuItem.Text = "Next Page";
			this.nextPageToolStripMenuItem.Click += new EventHandler(this.nextPageToolStripMenuItem_Click);
			this.lastPageToolStripMenuItem.Name = "lastPageToolStripMenuItem";
			this.lastPageToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.lastPageToolStripMenuItem.Text = "Last Page";
			this.lastPageToolStripMenuItem.Click += new EventHandler(this.lastPageToolStripMenuItem_Click);
			this.toolStripSeparator12.Name = "toolStripSeparator12";
			this.toolStripSeparator12.Size = new System.Drawing.Size(145, 6);
			this.previousViewToolStripMenuItem.Name = "previousViewToolStripMenuItem";
			this.previousViewToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.previousViewToolStripMenuItem.Text = "Previous View";
			this.previousViewToolStripMenuItem.Click += new EventHandler(this.previousViewToolStripMenuItem_Click);
			this.nextViewToolStripMenuItem.Name = "nextViewToolStripMenuItem";
			this.nextViewToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.nextViewToolStripMenuItem.Text = "Next View";
			this.nextViewToolStripMenuItem.Click += new EventHandler(this.nextViewToolStripMenuItem_Click);
			ToolStripItemCollection dropDownItems2 = this.viewToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray5 = new ToolStripItem[] { this.contentsToolStripMenuItem, this.pictureToolStripMenuItem, this.miniMapToolStripMenuItem, this.partslistToolStripMenuItem, this.selectionListToolStripMenuItem, this.toolStripSeparator3, this.informationToolStripMenuItem, this.toolStripSeparator1, this.restoreDefaultsToolStripMenuItem, this.saveDefaultsToolStripMenuItem };
			dropDownItems2.AddRange(toolStripItemArray5);
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			this.contentsToolStripMenuItem.Checked = true;
			this.contentsToolStripMenuItem.CheckState = CheckState.Checked;
			this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
			this.contentsToolStripMenuItem.ShowShortcutKeys = false;
			this.contentsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.contentsToolStripMenuItem.Text = "Contents";
			this.contentsToolStripMenuItem.Click += new EventHandler(this.contentsToolStripMenuItem_Click);
			this.pictureToolStripMenuItem.Checked = true;
			this.pictureToolStripMenuItem.CheckState = CheckState.Checked;
			this.pictureToolStripMenuItem.Name = "pictureToolStripMenuItem";
			this.pictureToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.pictureToolStripMenuItem.Text = "Picture";
			this.pictureToolStripMenuItem.Click += new EventHandler(this.pictureToolStripMenuItem_Click);
			this.miniMapToolStripMenuItem.Name = "miniMapToolStripMenuItem";
			this.miniMapToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.miniMapToolStripMenuItem.Text = "Mini Map";
			this.miniMapToolStripMenuItem.Click += new EventHandler(this.miniMapToolStripMenuItem_Click);
			this.partslistToolStripMenuItem.Checked = true;
			this.partslistToolStripMenuItem.CheckState = CheckState.Checked;
			this.partslistToolStripMenuItem.Name = "partslistToolStripMenuItem";
			this.partslistToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.partslistToolStripMenuItem.Text = "Partslist";
			this.partslistToolStripMenuItem.Click += new EventHandler(this.partslistToolStripMenuItem_Click);
			this.selectionListToolStripMenuItem.Checked = true;
			this.selectionListToolStripMenuItem.CheckState = CheckState.Checked;
			this.selectionListToolStripMenuItem.Name = "selectionListToolStripMenuItem";
			this.selectionListToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.selectionListToolStripMenuItem.Text = "Selection List";
			this.selectionListToolStripMenuItem.Click += new EventHandler(this.selectionListToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(156, 6);
			this.informationToolStripMenuItem.Checked = true;
			this.informationToolStripMenuItem.CheckState = CheckState.Checked;
			this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
			this.informationToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.informationToolStripMenuItem.Text = "Information";
			this.informationToolStripMenuItem.Click += new EventHandler(this.informationToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(156, 6);
			this.restoreDefaultsToolStripMenuItem.Name = "restoreDefaultsToolStripMenuItem";
			this.restoreDefaultsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.restoreDefaultsToolStripMenuItem.Text = "Restore Defaults";
			this.restoreDefaultsToolStripMenuItem.Click += new EventHandler(this.restoreDefaultsToolStripMenuItem_Click);
			this.saveDefaultsToolStripMenuItem.Name = "saveDefaultsToolStripMenuItem";
			this.saveDefaultsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.saveDefaultsToolStripMenuItem.Text = "Save Defaults";
			this.saveDefaultsToolStripMenuItem.Click += new EventHandler(this.saveDefaultsToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections2 = this.bookmarksToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray6 = new ToolStripItem[] { this.addBookmarksToolStripMenuItem, this.toolStripSeparator };
			toolStripItemCollections2.AddRange(toolStripItemArray6);
			this.bookmarksToolStripMenuItem.Name = "bookmarksToolStripMenuItem";
			this.bookmarksToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
			this.bookmarksToolStripMenuItem.Text = "Bookmarks";
			this.addBookmarksToolStripMenuItem.Name = "addBookmarksToolStripMenuItem";
			this.addBookmarksToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.addBookmarksToolStripMenuItem.Text = "Add to Bookmarks";
			this.addBookmarksToolStripMenuItem.Click += new EventHandler(this.AddBookmarksToolStripMenuItem_Click);
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(169, 6);
			ToolStripItemCollection dropDownItems3 = this.memoDetailsToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray7 = new ToolStripItem[] { this.addPictureMemoToolStripMenuItem, this.addPartMemoToolStripMenuItem, this.toolStripSeparator16, this.viewMemoListToolStripMenuItem, this.toolStripSeparator13, this.memoRecoveryToolStripMenuItem };
			dropDownItems3.AddRange(toolStripItemArray7);
			this.memoDetailsToolStripMenuItem.Name = "memoDetailsToolStripMenuItem";
			this.memoDetailsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
			this.memoDetailsToolStripMenuItem.Text = "Memos";
			this.memoDetailsToolStripMenuItem.Click += new EventHandler(this.memoDetailsToolStripMenuItem_Click);
			this.addPictureMemoToolStripMenuItem.Name = "addPictureMemoToolStripMenuItem";
			this.addPictureMemoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.addPictureMemoToolStripMenuItem.Text = "Add Picture Memo";
			this.addPictureMemoToolStripMenuItem.Click += new EventHandler(this.addPictureMemoToolStripMenuItem_Click);
			this.addPartMemoToolStripMenuItem.Name = "addPartMemoToolStripMenuItem";
			this.addPartMemoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.addPartMemoToolStripMenuItem.Text = "Add Part Memo";
			this.addPartMemoToolStripMenuItem.Click += new EventHandler(this.addPartMemoToolStripMenuItem_Click);
			this.toolStripSeparator16.Name = "toolStripSeparator16";
			this.toolStripSeparator16.Size = new System.Drawing.Size(171, 6);
			this.viewMemoListToolStripMenuItem.Name = "viewMemoListToolStripMenuItem";
			this.viewMemoListToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.viewMemoListToolStripMenuItem.Text = "View Memo List...";
			this.viewMemoListToolStripMenuItem.Click += new EventHandler(this.viewMemoListToolStripMenuItem_Click);
			this.toolStripSeparator13.Name = "toolStripSeparator13";
			this.toolStripSeparator13.Size = new System.Drawing.Size(171, 6);
			this.memoRecoveryToolStripMenuItem.Name = "memoRecoveryToolStripMenuItem";
			this.memoRecoveryToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.memoRecoveryToolStripMenuItem.Text = "Recovery ...";
			this.memoRecoveryToolStripMenuItem.Click += new EventHandler(this.memoRecoveryToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections3 = this.searchToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray8 = new ToolStripItem[] { this.pageNameToolStripMenuItem, this.textSearchToolStripMenuItem, this.toolStripSeparator5, this.partNameToolStripMenuItem, this.partNumberToolStripMenuItem, this.toolStripSeparator6, this.advancedSearchToolStripMenuItem };
			toolStripItemCollections3.AddRange(toolStripItemArray8);
			this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
			this.searchToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.searchToolStripMenuItem.Text = "Search";
			this.pageNameToolStripMenuItem.Name = "pageNameToolStripMenuItem";
			this.pageNameToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.pageNameToolStripMenuItem.Text = "Page Name ...";
			this.pageNameToolStripMenuItem.Click += new EventHandler(this.pageNameToolStripMenuItem_Click);
			this.textSearchToolStripMenuItem.Name = "textSearchToolStripMenuItem";
			this.textSearchToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.textSearchToolStripMenuItem.Text = "Text Search ...";
			this.textSearchToolStripMenuItem.Click += new EventHandler(this.textSearchToolStripMenuItem_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(171, 6);
			this.partNameToolStripMenuItem.Name = "partNameToolStripMenuItem";
			this.partNameToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.partNameToolStripMenuItem.Text = "Part Name ...";
			this.partNameToolStripMenuItem.Click += new EventHandler(this.partNameToolStripMenuItem_Click);
			this.partNumberToolStripMenuItem.Name = "partNumberToolStripMenuItem";
			this.partNumberToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.partNumberToolStripMenuItem.Text = "Part Number ...";
			this.partNumberToolStripMenuItem.Click += new EventHandler(this.partNumberToolStripMenuItem_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(171, 6);
			this.advancedSearchToolStripMenuItem.Name = "advancedSearchToolStripMenuItem";
			this.advancedSearchToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.advancedSearchToolStripMenuItem.Text = "Advanced Search...";
			this.advancedSearchToolStripMenuItem.Click += new EventHandler(this.advancedSearchToolStripMenuItem_Click);
			ToolStripItemCollection dropDownItems4 = this.settingsToolStripMenuItem.DropDownItems;
			ToolStripItem[] colorToolStripMenuItem = new ToolStripItem[] { this.fontToolStripMenuItem, this.ColorToolStripMenuItem, this.generalToolStripMenuItem, this.toolStripSeparator14, this.memoSettingsToolStripMenuItem, this.toolStripSeparator23, this.partsListSettingsToolStripMenuItem, this.selectionListSettingsToolStripMenuItem, this.toolStripSeparator9, this.pageNameSearchSettingsToolStripMenuItem, this.textSearceNameSearchSettingsToolStripMenuItem, this.partNameSearchSettingsToolStripMenuItem, this.partNumberSearchSettingsToolStripMenuItem, this.advanceSearchSettingsToolStripMenuItem, this.toolStripSeparator15, this.manageDiskSpaceToolStripMenuItem, this.toolStripSeparator4, this.connectionToolStripMenuItem };
			dropDownItems4.AddRange(colorToolStripMenuItem);
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.settingsToolStripMenuItem.Text = "Settings";
			this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
			this.fontToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.fontToolStripMenuItem.Text = "Font ...";
			this.fontToolStripMenuItem.Click += new EventHandler(this.fontAndColorToolStripMenuItem_Click);
			this.ColorToolStripMenuItem.Name = "ColorToolStripMenuItem";
			this.ColorToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.ColorToolStripMenuItem.Text = "Color...";
			this.ColorToolStripMenuItem.Click += new EventHandler(this.partsListInfoFontAndColorToolStripMenuItem_Click);
			this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
			this.generalToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.generalToolStripMenuItem.Text = "General ...";
			this.generalToolStripMenuItem.Click += new EventHandler(this.generalToolStripMenuItem_Click);
			this.toolStripSeparator14.Name = "toolStripSeparator14";
			this.toolStripSeparator14.Size = new System.Drawing.Size(186, 6);
			this.memoSettingsToolStripMenuItem.Name = "memoSettingsToolStripMenuItem";
			this.memoSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.memoSettingsToolStripMenuItem.Text = "Memos ...";
			this.memoSettingsToolStripMenuItem.Click += new EventHandler(this.memosToolStripMenuItem_Click);
			this.toolStripSeparator23.Name = "toolStripSeparator23";
			this.toolStripSeparator23.Size = new System.Drawing.Size(186, 6);
			this.partsListSettingsToolStripMenuItem.Name = "partsListSettingsToolStripMenuItem";
			this.partsListSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.partsListSettingsToolStripMenuItem.Text = "Parts List Settings";
			this.partsListSettingsToolStripMenuItem.Click += new EventHandler(this.partsListSettingsToolStripMenuItem_Click);
			this.selectionListSettingsToolStripMenuItem.Name = "selectionListSettingsToolStripMenuItem";
			this.selectionListSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.selectionListSettingsToolStripMenuItem.Text = "Selection List Settings";
			this.selectionListSettingsToolStripMenuItem.Click += new EventHandler(this.selectionListSettingsToolStripMenuItem_Click);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new System.Drawing.Size(186, 6);
			this.pageNameSearchSettingsToolStripMenuItem.Name = "pageNameSearchSettingsToolStripMenuItem";
			this.pageNameSearchSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.pageNameSearchSettingsToolStripMenuItem.Text = "Page Name Search...";
			this.pageNameSearchSettingsToolStripMenuItem.Click += new EventHandler(this.pageNameSearchToolStripMenuItem_Click);
			this.textSearceNameSearchSettingsToolStripMenuItem.Name = "textSearceNameSearchSettingsToolStripMenuItem";
			this.textSearceNameSearchSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.textSearceNameSearchSettingsToolStripMenuItem.Text = "Text Search...";
			this.textSearceNameSearchSettingsToolStripMenuItem.Click += new EventHandler(this.textSearceNameSearchSettingsToolStripMenuItem_Click);
			this.partNameSearchSettingsToolStripMenuItem.Name = "partNameSearchSettingsToolStripMenuItem";
			this.partNameSearchSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.partNameSearchSettingsToolStripMenuItem.Text = "Part Name Search...";
			this.partNameSearchSettingsToolStripMenuItem.Click += new EventHandler(this.partNameSearchToolStripMenuItem_Click);
			this.partNumberSearchSettingsToolStripMenuItem.Name = "partNumberSearchSettingsToolStripMenuItem";
			this.partNumberSearchSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.partNumberSearchSettingsToolStripMenuItem.Text = "Part Number Search...";
			this.partNumberSearchSettingsToolStripMenuItem.Click += new EventHandler(this.partNumberSearchToolStripMenuItem_Click);
			this.advanceSearchSettingsToolStripMenuItem.Name = "advanceSearchSettingsToolStripMenuItem";
			this.advanceSearchSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.advanceSearchSettingsToolStripMenuItem.Text = "Advance Search";
			this.advanceSearchSettingsToolStripMenuItem.Click += new EventHandler(this.advanceSearchSettingsToolStripMenuItem_Click);
			this.toolStripSeparator15.Name = "toolStripSeparator15";
			this.toolStripSeparator15.Size = new System.Drawing.Size(186, 6);
			this.manageDiskSpaceToolStripMenuItem.Name = "manageDiskSpaceToolStripMenuItem";
			this.manageDiskSpaceToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.manageDiskSpaceToolStripMenuItem.Text = "Manage Disk Space...";
			this.manageDiskSpaceToolStripMenuItem.Click += new EventHandler(this.manageDiskSpaceToolStripMenuItem_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(186, 6);
			this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
			this.connectionToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.connectionToolStripMenuItem.Text = "Connection ...";
			this.connectionToolStripMenuItem.Click += new EventHandler(this.connectionToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections4 = this.toolsToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray9 = new ToolStripItem[] { this.singleBookToolStripMenuItem, this.multipleBooksToolStripMenuItem };
			toolStripItemCollections4.AddRange(toolStripItemArray9);
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			this.singleBookToolStripMenuItem.Name = "singleBookToolStripMenuItem";
			this.singleBookToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.singleBookToolStripMenuItem.Text = "Single Book Download...";
			this.singleBookToolStripMenuItem.Click += new EventHandler(this.singleBookToolStripMenuItem_Click);
			this.multipleBooksToolStripMenuItem.Name = "multipleBooksToolStripMenuItem";
			this.multipleBooksToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
			this.multipleBooksToolStripMenuItem.Text = "Multiple Books Download...";
			this.multipleBooksToolStripMenuItem.Click += new EventHandler(this.multipleBooksToolStripMenuItem_Click);
			ToolStripItemCollection dropDownItems5 = this.helpToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray10 = new ToolStripItem[] { this.gSPcLocalHelpToolStripMenuItem, this.aboutGSPcLocalToolStripMenuItem };
			dropDownItems5.AddRange(toolStripItemArray10);
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			this.gSPcLocalHelpToolStripMenuItem.Name = "gSPcLocalHelpToolStripMenuItem";
			this.gSPcLocalHelpToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.gSPcLocalHelpToolStripMenuItem.Text = "GSPcLocal Help";
			this.gSPcLocalHelpToolStripMenuItem.Click += new EventHandler(this.gSPcLocalHelpToolStripMenuItem_Click);
			this.aboutGSPcLocalToolStripMenuItem.Name = "aboutGSPcLocalToolStripMenuItem";
			this.aboutGSPcLocalToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			this.aboutGSPcLocalToolStripMenuItem.Text = "About GSPcLocal ...";
			this.aboutGSPcLocalToolStripMenuItem.Click += new EventHandler(this.aboutGSPcLocalToolStripMenuItem_Click);
			this.addOnToolStripMenuItem.Name = "addOnToolStripMenuItem";
			this.addOnToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			this.addOnToolStripMenuItem.Text = "AddOn";
			this.addOnToolStripMenuItem.Visible = false;
			this.tsPortal.Dock = DockStyle.None;
			ToolStripItemCollection items1 = this.tsPortal.Items;
			ToolStripItem[] toolStripItemArray11 = new ToolStripItem[] { this.tsbPortal, this.tsbOpenBook };
			items1.AddRange(toolStripItemArray11);
			this.tsPortal.Location = new Point(3, 24);
			this.tsPortal.Name = "tsPortal";
			this.tsPortal.Size = new System.Drawing.Size(58, 25);
			this.tsPortal.TabIndex = 25;
			this.tsbPortal.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPortal.Image = GSPcLocalViewer.Properties.Resources.portal;
			this.tsbPortal.ImageTransparentColor = Color.Magenta;
			this.tsbPortal.Name = "tsbPortal";
			this.tsbPortal.Size = new System.Drawing.Size(23, 22);
			this.tsbPortal.Text = "Jump to Portal";
			this.tsbPortal.Click += new EventHandler(this.tsbPortal_Click);
			this.tsbOpenBook.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbOpenBook.Image = GSPcLocalViewer.Properties.Resources.Open_New;
			this.tsbOpenBook.ImageTransparentColor = Color.Magenta;
			this.tsbOpenBook.Name = "tsbOpenBook";
			this.tsbOpenBook.Size = new System.Drawing.Size(23, 22);
			this.tsbOpenBook.Text = "Open New Book";
			this.tsbOpenBook.Click += new EventHandler(this.tsbOpenBook_Click);
			this.tsHistory.Dock = DockStyle.None;
			ToolStripItemCollection items2 = this.tsHistory.Items;
			ToolStripItem[] toolStripItemArray12 = new ToolStripItem[] { this.tsbHistoryBack, this.tsbHistoryForward, this.tsbHistoryList };
			items2.AddRange(toolStripItemArray12);
			this.tsHistory.Location = new Point(61, 24);
			this.tsHistory.Name = "tsHistory";
			this.tsHistory.Size = new System.Drawing.Size(71, 25);
			this.tsHistory.TabIndex = 4;
			this.tsbHistoryBack.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbHistoryBack.Enabled = false;
			this.tsbHistoryBack.Image = GSPcLocalViewer.Properties.Resources.History_Backward;
			this.tsbHistoryBack.ImageTransparentColor = Color.Magenta;
			this.tsbHistoryBack.Name = "tsbHistoryBack";
			this.tsbHistoryBack.Size = new System.Drawing.Size(23, 22);
			this.tsbHistoryBack.Text = "Backward";
			this.tsbHistoryBack.Click += new EventHandler(this.tsbHistoryBack_Click);
			this.tsbHistoryForward.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbHistoryForward.Enabled = false;
			this.tsbHistoryForward.Image = GSPcLocalViewer.Properties.Resources.History_Forward;
			this.tsbHistoryForward.ImageTransparentColor = Color.Magenta;
			this.tsbHistoryForward.Name = "tsbHistoryForward";
			this.tsbHistoryForward.Size = new System.Drawing.Size(23, 22);
			this.tsbHistoryForward.Text = "Forward";
			this.tsbHistoryForward.Click += new EventHandler(this.tsbHistoryForward_Click);
			this.tsbHistoryList.DisplayStyle = ToolStripItemDisplayStyle.None;
			this.tsbHistoryList.Image = (Image)componentResourceManager.GetObject("tsbHistoryList.Image");
			this.tsbHistoryList.ImageTransparentColor = Color.Magenta;
			this.tsbHistoryList.Name = "tsbHistoryList";
			this.tsbHistoryList.Size = new System.Drawing.Size(13, 22);
			this.tsbHistoryList.Text = "History List";
			this.tsNavigate.Dock = DockStyle.None;
			ToolStripItemCollection items3 = this.tsNavigate.Items;
			ToolStripItem[] toolStripItemArray13 = new ToolStripItem[] { this.tsbNavigateFirst, this.toolStripSeparator7, this.tsbNavigatePrevious, this.tsbNavigateNext, this.toolStripSeparator8, this.tsbNavigateLast };
			items3.AddRange(toolStripItemArray13);
			this.tsNavigate.Location = new Point(132, 24);
			this.tsNavigate.Name = "tsNavigate";
			this.tsNavigate.Size = new System.Drawing.Size(116, 25);
			this.tsNavigate.TabIndex = 8;
			this.tsbNavigateFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbNavigateFirst.Enabled = false;
			this.tsbNavigateFirst.Image = GSPcLocalViewer.Properties.Resources.Nav_First;
			this.tsbNavigateFirst.ImageTransparentColor = Color.Magenta;
			this.tsbNavigateFirst.Name = "tsbNavigateFirst";
			this.tsbNavigateFirst.Size = new System.Drawing.Size(23, 22);
			this.tsbNavigateFirst.Text = "First Page";
			this.tsbNavigateFirst.Click += new EventHandler(this.tsbNavigateFirst_Click);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			this.tsbNavigatePrevious.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbNavigatePrevious.Enabled = false;
			this.tsbNavigatePrevious.Image = GSPcLocalViewer.Properties.Resources.Nav_Prev;
			this.tsbNavigatePrevious.ImageTransparentColor = Color.Magenta;
			this.tsbNavigatePrevious.Name = "tsbNavigatePrevious";
			this.tsbNavigatePrevious.Size = new System.Drawing.Size(23, 22);
			this.tsbNavigatePrevious.Text = "Previous Page";
			this.tsbNavigatePrevious.Click += new EventHandler(this.tsbNavigatePrevious_Click);
			this.tsbNavigateNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbNavigateNext.Enabled = false;
			this.tsbNavigateNext.Image = GSPcLocalViewer.Properties.Resources.Nav_Next;
			this.tsbNavigateNext.ImageTransparentColor = Color.Magenta;
			this.tsbNavigateNext.Name = "tsbNavigateNext";
			this.tsbNavigateNext.Size = new System.Drawing.Size(23, 22);
			this.tsbNavigateNext.Text = "Next Page";
			this.tsbNavigateNext.Click += new EventHandler(this.tsbNavigateNext_Click);
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			this.tsbNavigateLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbNavigateLast.Enabled = false;
			this.tsbNavigateLast.Image = GSPcLocalViewer.Properties.Resources.Nav_Last;
			this.tsbNavigateLast.ImageTransparentColor = Color.Magenta;
			this.tsbNavigateLast.Name = "tsbNavigateLast";
			this.tsbNavigateLast.Size = new System.Drawing.Size(23, 22);
			this.tsbNavigateLast.Text = "Last Page";
			this.tsbNavigateLast.Click += new EventHandler(this.tsbNavigateLast_Click);
			this.tsView.Dock = DockStyle.None;
			ToolStripItemCollection items4 = this.tsView.Items;
			ToolStripItem[] toolStripItemArray14 = new ToolStripItem[] { this.tsbViewContents, this.tsbViewPicture, this.tsbViewPartslist, this.tsbViewSeparator, this.tsbViewInfo, this.toolStripSeparator17, this.tsbRestoreDefaults };
			items4.AddRange(toolStripItemArray14);
			this.tsView.Location = new Point(248, 24);
			this.tsView.Name = "tsView";
			this.tsView.Size = new System.Drawing.Size(139, 25);
			this.tsView.TabIndex = 6;
			this.tsbViewContents.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbViewContents.Image = GSPcLocalViewer.Properties.Resources.View_Contents;
			this.tsbViewContents.ImageTransparentColor = Color.Magenta;
			this.tsbViewContents.Name = "tsbViewContents";
			this.tsbViewContents.Size = new System.Drawing.Size(23, 22);
			this.tsbViewContents.Text = "View Contents";
			this.tsbViewContents.Click += new EventHandler(this.tsbViewContents_Click);
			this.tsbViewPicture.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbViewPicture.Image = GSPcLocalViewer.Properties.Resources.View_Picture;
			this.tsbViewPicture.ImageTransparentColor = Color.Magenta;
			this.tsbViewPicture.Name = "tsbViewPicture";
			this.tsbViewPicture.Size = new System.Drawing.Size(23, 22);
			this.tsbViewPicture.Text = "View Picture";
			this.tsbViewPicture.Click += new EventHandler(this.tsbViewPicture_Click);
			this.tsbViewPartslist.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbViewPartslist.Image = GSPcLocalViewer.Properties.Resources.View_Partslist;
			this.tsbViewPartslist.ImageTransparentColor = Color.Magenta;
			this.tsbViewPartslist.Name = "tsbViewPartslist";
			this.tsbViewPartslist.Size = new System.Drawing.Size(23, 22);
			this.tsbViewPartslist.Text = "View Partslist";
			this.tsbViewPartslist.Click += new EventHandler(this.tsbViewPartslist_Click);
			this.tsbViewSeparator.Name = "tsbViewSeparator";
			this.tsbViewSeparator.Size = new System.Drawing.Size(6, 25);
			this.tsbViewInfo.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbViewInfo.Image = GSPcLocalViewer.Properties.Resources.View_Information;
			this.tsbViewInfo.ImageTransparentColor = Color.Magenta;
			this.tsbViewInfo.Name = "tsbViewInfo";
			this.tsbViewInfo.Size = new System.Drawing.Size(23, 22);
			this.tsbViewInfo.Text = "View Information";
			this.tsbViewInfo.Click += new EventHandler(this.tsbViewInfo_Click);
			this.toolStripSeparator17.Name = "toolStripSeparator17";
			this.toolStripSeparator17.Size = new System.Drawing.Size(6, 25);
			this.tsbRestoreDefaults.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbRestoreDefaults.Image = GSPcLocalViewer.Properties.Resources.View_RestoreDefaults;
			this.tsbRestoreDefaults.ImageTransparentColor = Color.Magenta;
			this.tsbRestoreDefaults.Name = "tsbRestoreDefaults";
			this.tsbRestoreDefaults.Size = new System.Drawing.Size(23, 22);
			this.tsbRestoreDefaults.Text = "toolStripButton1";
			this.tsbRestoreDefaults.Click += new EventHandler(this.tsbRestoreDefaults_Click);
			this.tsSearch.Dock = DockStyle.None;
			ToolStripItemCollection items5 = this.tsSearch.Items;
			ToolStripItem[] toolStripItemArray15 = new ToolStripItem[] { this.tsbSearchPageName, this.tsbSearchText, this.tsbSearchPartName, this.tsbSearchPartNumber, this.tsbSearchPartAdvance };
			items5.AddRange(toolStripItemArray15);
			this.tsSearch.Location = new Point(387, 24);
			this.tsSearch.Name = "tsSearch";
			this.tsSearch.Size = new System.Drawing.Size(127, 25);
			this.tsSearch.TabIndex = 7;
			this.tsbSearchPageName.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchPageName.Image = GSPcLocalViewer.Properties.Resources.Search_Page;
			this.tsbSearchPageName.ImageTransparentColor = Color.Magenta;
			this.tsbSearchPageName.Name = "tsbSearchPageName";
			this.tsbSearchPageName.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchPageName.Text = "Page Name Search";
			this.tsbSearchPageName.Click += new EventHandler(this.tsbSearchPageName_Click);
			this.tsbSearchText.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchText.Image = GSPcLocalViewer.Properties.Resources.Search_Text;
			this.tsbSearchText.ImageTransparentColor = Color.Magenta;
			this.tsbSearchText.Name = "tsbSearchText";
			this.tsbSearchText.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchText.Text = "Text Search";
			this.tsbSearchText.Click += new EventHandler(this.tsbSearchText_Click);
			this.tsbSearchPartName.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchPartName.Image = GSPcLocalViewer.Properties.Resources.Search_Parts;
			this.tsbSearchPartName.ImageTransparentColor = Color.Magenta;
			this.tsbSearchPartName.Name = "tsbSearchPartName";
			this.tsbSearchPartName.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchPartName.Text = "Part Name Search";
			this.tsbSearchPartName.Click += new EventHandler(this.tsbSearchPartName_Click);
			this.tsbSearchPartNumber.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchPartNumber.Image = GSPcLocalViewer.Properties.Resources.Search_Parts2;
			this.tsbSearchPartNumber.ImageTransparentColor = Color.Magenta;
			this.tsbSearchPartNumber.Name = "tsbSearchPartNumber";
			this.tsbSearchPartNumber.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchPartNumber.Text = "Part Number Search";
			this.tsbSearchPartNumber.Click += new EventHandler(this.tsbSearchPartNumber_Click);
			this.tsbSearchPartAdvance.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchPartAdvance.Image = GSPcLocalViewer.Properties.Resources.Search_PartsAdvance;
			this.tsbSearchPartAdvance.ImageTransparentColor = Color.Magenta;
			this.tsbSearchPartAdvance.Name = "tsbSearchPartAdvance";
			this.tsbSearchPartAdvance.Size = new System.Drawing.Size(23, 22);
			this.tsbSearchPartAdvance.Text = "toolStripButton1";
			this.tsbSearchPartAdvance.Click += new EventHandler(this.tsbSearchPartAdvance_Click);
			this.tsTools.Dock = DockStyle.None;
			ToolStripItemCollection toolStripItemCollections5 = this.tsTools.Items;
			ToolStripItem[] toolStripItemArray16 = new ToolStripItem[] { this.tsbSingleBookDownload, this.tsbMultipleBooksDownload, this.toolStripSeparator18, this.tsbDataCleanup, this.tsbConnection };
			toolStripItemCollections5.AddRange(toolStripItemArray16);
			this.tsTools.Location = new Point(741, 24);
			this.tsTools.Name = "tsTools";
			this.tsTools.Size = new System.Drawing.Size(110, 25);
			this.tsTools.TabIndex = 26;
			this.tsbSingleBookDownload.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSingleBookDownload.Image = (Image)componentResourceManager.GetObject("tsbSingleBookDownload.Image");
			this.tsbSingleBookDownload.ImageTransparentColor = Color.Magenta;
			this.tsbSingleBookDownload.Name = "tsbSingleBookDownload";
			this.tsbSingleBookDownload.Size = new System.Drawing.Size(23, 22);
			this.tsbSingleBookDownload.Text = "toolStripButton1";
			this.tsbSingleBookDownload.Click += new EventHandler(this.tsbSingleBookDownload_Click);
			this.tsbMultipleBooksDownload.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbMultipleBooksDownload.Image = (Image)componentResourceManager.GetObject("tsbMultipleBooksDownload.Image");
			this.tsbMultipleBooksDownload.ImageTransparentColor = Color.Magenta;
			this.tsbMultipleBooksDownload.Name = "tsbMultipleBooksDownload";
			this.tsbMultipleBooksDownload.Size = new System.Drawing.Size(23, 22);
			this.tsbMultipleBooksDownload.Text = "toolStripButton2";
			this.tsbMultipleBooksDownload.Click += new EventHandler(this.tsbMultipleBooksDownload_Click);
			this.toolStripSeparator18.Name = "toolStripSeparator18";
			this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
			this.tsbDataCleanup.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbDataCleanup.Image = GSPcLocalViewer.Properties.Resources.Manage_Download;
			this.tsbDataCleanup.ImageTransparentColor = Color.Magenta;
			this.tsbDataCleanup.Name = "tsbDataCleanup";
			this.tsbDataCleanup.Size = new System.Drawing.Size(23, 22);
			this.tsbDataCleanup.Text = "Data Cleanup...";
			this.tsbDataCleanup.Click += new EventHandler(this.tsbDataCleanup_Click);
			this.tsbConnection.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbConnection.Image = GSPcLocalViewer.Properties.Resources.Viewer_Connection;
			this.tsbConnection.ImageTransparentColor = Color.Magenta;
			this.tsbConnection.Name = "tsbConnection";
			this.tsbConnection.Size = new System.Drawing.Size(23, 22);
			this.tsbConnection.Text = "Connection";
			this.tsbConnection.Click += new EventHandler(this.toolStripButton1_Click);
			this.tsFunctions.Dock = DockStyle.None;
			ToolStripItemCollection items6 = this.tsFunctions.Items;
			ToolStripItem[] toolStripItemArray17 = new ToolStripItem[] { this.tsbPrint, this.tsbAddBookmarks, this.tsbMemoList, this.tsbMemoRecovery, this.tsbConfiguration, this.tsbThirdPartyBasket, this.tsbAbout, this.tsbHelp };
			items6.AddRange(toolStripItemArray17);
			this.tsFunctions.Location = new Point(514, 24);
			this.tsFunctions.Name = "tsFunctions";
			this.tsFunctions.Size = new System.Drawing.Size(227, 25);
			this.tsFunctions.TabIndex = 5;
			this.tsbPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPrint.Image = GSPcLocalViewer.Properties.Resources.Print;
			this.tsbPrint.ImageTransparentColor = Color.Magenta;
			this.tsbPrint.Name = "tsbPrint";
			this.tsbPrint.Size = new System.Drawing.Size(23, 22);
			this.tsbPrint.Text = "Print";
			this.tsbPrint.Click += new EventHandler(this.tsbPrint_Click);
			this.tsbAddBookmarks.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbAddBookmarks.Image = GSPcLocalViewer.Properties.Resources.Add_Bookmarks;
			this.tsbAddBookmarks.ImageTransparentColor = Color.Magenta;
			this.tsbAddBookmarks.Name = "tsbAddBookmarks";
			this.tsbAddBookmarks.Size = new System.Drawing.Size(23, 22);
			this.tsbAddBookmarks.Text = "Add Bookmarks";
			this.tsbAddBookmarks.Click += new EventHandler(this.tsbAddBookmarks_Click);
			this.tsbMemoList.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbMemoList.Image = GSPcLocalViewer.Properties.Resources.Memo_list;
			this.tsbMemoList.ImageTransparentColor = Color.Magenta;
			this.tsbMemoList.Name = "tsbMemoList";
			this.tsbMemoList.Size = new System.Drawing.Size(23, 22);
			this.tsbMemoList.Text = "Memo List";
			this.tsbMemoList.Click += new EventHandler(this.tsbMemoList_Click);
			this.tsbMemoRecovery.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbMemoRecovery.Image = GSPcLocalViewer.Properties.Resources.Memo_Recovery;
			this.tsbMemoRecovery.ImageTransparentColor = Color.Magenta;
			this.tsbMemoRecovery.Name = "tsbMemoRecovery";
			this.tsbMemoRecovery.Size = new System.Drawing.Size(23, 22);
			this.tsbMemoRecovery.Text = "toolStripButton1";
			this.tsbMemoRecovery.Click += new EventHandler(this.tsbMemoRecovery_Click);
			this.tsbConfiguration.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbConfiguration.Image = GSPcLocalViewer.Properties.Resources.Configuration;
			this.tsbConfiguration.ImageTransparentColor = Color.Magenta;
			this.tsbConfiguration.Name = "tsbConfiguration";
			this.tsbConfiguration.Size = new System.Drawing.Size(23, 22);
			this.tsbConfiguration.Text = "Configuration";
			this.tsbConfiguration.Click += new EventHandler(this.tsbConfiguration_Click);
			this.tsbThirdPartyBasket.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbThirdPartyBasket.Image = GSPcLocalViewer.Properties.Resources.basket;
			this.tsbThirdPartyBasket.ImageTransparentColor = Color.Magenta;
			this.tsbThirdPartyBasket.Name = "tsbThirdPartyBasket";
			this.tsbThirdPartyBasket.Size = new System.Drawing.Size(23, 22);
			this.tsbThirdPartyBasket.Text = "Basket";
			this.tsbThirdPartyBasket.Click += new EventHandler(this.tsbThirdPartyBasket_Click);
			this.tsbAbout.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbAbout.Image = GSPcLocalViewer.Properties.Resources.About;
			this.tsbAbout.ImageTransparentColor = Color.Magenta;
			this.tsbAbout.Name = "tsbAbout";
			this.tsbAbout.Size = new System.Drawing.Size(23, 22);
			this.tsbAbout.Text = "About GSPcLocal";
			this.tsbAbout.Click += new EventHandler(this.tsbAbout_Click);
			this.tsbHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbHelp.Image = GSPcLocalViewer.Properties.Resources.Help;
			this.tsbHelp.ImageTransparentColor = Color.Magenta;
			this.tsbHelp.Name = "tsbHelp";
			this.tsbHelp.Size = new System.Drawing.Size(23, 22);
			this.tsbHelp.Text = "GSPcLocal Help";
			this.tsbHelp.Click += new EventHandler(this.tsbHelp_Click);
			this.tsPic.Dock = DockStyle.None;
			ToolStripItemCollection toolStripItemCollections6 = this.tsPic.Items;
			ToolStripItem[] toolStripItemArray18 = new ToolStripItem[] { this.tslPic, this.tsbPicPrev, this.tstPicNo, this.tsbPicNext, this.toolStripSeparator10, this.tsbFindText, this.tsbPicPanMode, this.tsbPicZoomSelect, this.tsbFitPage, this.tsbPicCopy, this.tsbPicSelectText, this.toolStripSeparator19, this.tsbPicZoomIn, this.tsbPicZoomOut, this.toolStripSeparator22, this.tsBRotateLeft, this.tsBRotateRight, this.toolStripSeparator20, this.tsbAddPictureMemo, this.tsbThumbnail };
			toolStripItemCollections6.AddRange(toolStripItemArray18);
			this.tsPic.Location = new Point(851, 24);
			this.tsPic.Name = "tsPic";
			this.tsPic.Size = new System.Drawing.Size(310, 25);
			this.tsPic.TabIndex = 24;
			this.tslPic.Name = "tslPic";
			this.tslPic.Size = new System.Drawing.Size(44, 22);
			this.tslPic.Text = "Picture";
			this.tsbPicPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicPrev.Image = GSPcLocalViewer.Properties.Resources.Nav_Prev;
			this.tsbPicPrev.ImageTransparentColor = Color.Magenta;
			this.tsbPicPrev.Name = "tsbPicPrev";
			this.tsbPicPrev.Size = new System.Drawing.Size(23, 22);
			this.tsbPicPrev.Text = "Previous Picture";
			this.tsbPicPrev.Click += new EventHandler(this.tsbPicPrev_Click);
			this.tstPicNo.AutoSize = false;
			this.tstPicNo.BorderStyle = BorderStyle.FixedSingle;
			this.tstPicNo.Name = "tstPicNo";
			this.tstPicNo.ReadOnly = true;
			this.tstPicNo.ShortcutsEnabled = false;
			this.tstPicNo.Size = new System.Drawing.Size(50, 23);
			this.tstPicNo.TextBoxTextAlign = HorizontalAlignment.Center;
			this.tsbPicNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicNext.Image = GSPcLocalViewer.Properties.Resources.Nav_Next;
			this.tsbPicNext.ImageTransparentColor = Color.Magenta;
			this.tsbPicNext.Name = "tsbPicNext";
			this.tsbPicNext.Overflow = ToolStripItemOverflow.Never;
			this.tsbPicNext.Size = new System.Drawing.Size(23, 22);
			this.tsbPicNext.Text = "Next Picture";
			this.tsbPicNext.Click += new EventHandler(this.tsbPicNext_Click);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
			this.tsbFindText.BackgroundImageLayout = ImageLayout.None;
			this.tsbFindText.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbFindText.Image = GSPcLocalViewer.Properties.Resources.Text_Search2;
			this.tsbFindText.ImageTransparentColor = Color.Magenta;
			this.tsbFindText.Name = "tsbFindText";
			this.tsbFindText.Size = new System.Drawing.Size(23, 22);
			this.tsbFindText.Text = "Find Text";
			this.tsbFindText.Click += new EventHandler(this.tsbFindText_Click);
			this.tsbPicPanMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicPanMode.Image = GSPcLocalViewer.Properties.Resources.Pan_Mode;
			this.tsbPicPanMode.ImageTransparentColor = Color.Magenta;
			this.tsbPicPanMode.Name = "tsbPicPanMode";
			this.tsbPicPanMode.Size = new System.Drawing.Size(23, 22);
			this.tsbPicPanMode.Text = "Pan Mode";
			this.tsbPicPanMode.Click += new EventHandler(this.tsbPicPanMode_Click);
			this.tsbPicZoomSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicZoomSelect.Image = GSPcLocalViewer.Properties.Resources.zoom_select;
			this.tsbPicZoomSelect.ImageTransparentColor = Color.Magenta;
			this.tsbPicZoomSelect.Name = "tsbPicZoomSelect";
			this.tsbPicZoomSelect.Size = new System.Drawing.Size(23, 22);
			this.tsbPicZoomSelect.Text = "Select Zoom";
			this.tsbPicZoomSelect.Click += new EventHandler(this.tsbPicZoomSelect_Click);
			this.tsbFitPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbFitPage.Image = GSPcLocalViewer.Properties.Resources.zoom_fitpage;
			this.tsbFitPage.ImageTransparentColor = Color.Magenta;
			this.tsbFitPage.Name = "tsbFitPage";
			this.tsbFitPage.Size = new System.Drawing.Size(23, 22);
			this.tsbFitPage.Text = "Zoom In";
			this.tsbFitPage.Click += new EventHandler(this.tsbFitPage_Click);
			this.tsbPicCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicCopy.Image = GSPcLocalViewer.Properties.Resources.copy_over;
			this.tsbPicCopy.ImageTransparentColor = Color.Magenta;
			this.tsbPicCopy.Name = "tsbPicCopy";
			this.tsbPicCopy.Size = new System.Drawing.Size(23, 22);
			this.tsbPicCopy.Text = "Copy Image";
			this.tsbPicCopy.Click += new EventHandler(this.tsbPicCopy_Click);
			this.tsbPicSelectText.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicSelectText.Image = GSPcLocalViewer.Properties.Resources.Text_Selection;
			this.tsbPicSelectText.ImageTransparentColor = Color.Magenta;
			this.tsbPicSelectText.Name = "tsbPicSelectText";
			this.tsbPicSelectText.Size = new System.Drawing.Size(23, 22);
			this.tsbPicSelectText.Text = "Copy Image";
			this.tsbPicSelectText.Click += new EventHandler(this.tsbPicSelectText_Click);
			this.toolStripSeparator19.Name = "toolStripSeparator19";
			this.toolStripSeparator19.Size = new System.Drawing.Size(6, 25);
			this.tsbPicZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicZoomIn.Image = GSPcLocalViewer.Properties.Resources.zoom_in;
			this.tsbPicZoomIn.ImageTransparentColor = Color.Magenta;
			this.tsbPicZoomIn.Name = "tsbPicZoomIn";
			this.tsbPicZoomIn.Size = new System.Drawing.Size(23, 22);
			this.tsbPicZoomIn.Text = "Zoom In";
			this.tsbPicZoomIn.Click += new EventHandler(this.tsbPicZoomIn_Click);
			this.tsbPicZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicZoomOut.Image = GSPcLocalViewer.Properties.Resources.zoon_out;
			this.tsbPicZoomOut.ImageTransparentColor = Color.Magenta;
			this.tsbPicZoomOut.Name = "tsbPicZoomOut";
			this.tsbPicZoomOut.Size = new System.Drawing.Size(23, 22);
			this.tsbPicZoomOut.Text = "Zoom Out";
			this.tsbPicZoomOut.Click += new EventHandler(this.tsbPicZoomOut_Click);
			this.toolStripSeparator22.Name = "toolStripSeparator22";
			this.toolStripSeparator22.Size = new System.Drawing.Size(6, 25);
			this.tsBRotateLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBRotateLeft.Image = GSPcLocalViewer.Properties.Resources.Rotate_Left;
			this.tsBRotateLeft.ImageTransparentColor = Color.Magenta;
			this.tsBRotateLeft.Name = "tsBRotateLeft";
			this.tsBRotateLeft.Size = new System.Drawing.Size(23, 22);
			this.tsBRotateLeft.Text = "Rotate Left";
			this.tsBRotateLeft.Click += new EventHandler(this.tsBRotateLeft_Click);
			this.tsBRotateRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBRotateRight.Image = GSPcLocalViewer.Properties.Resources.Rotate_Right;
			this.tsBRotateRight.ImageTransparentColor = Color.Magenta;
			this.tsBRotateRight.Name = "tsBRotateRight";
			this.tsBRotateRight.Size = new System.Drawing.Size(23, 22);
			this.tsBRotateRight.Text = "Rotate Right";
			this.tsBRotateRight.Click += new EventHandler(this.tsBRotateRight_Click);
			this.toolStripSeparator20.Name = "toolStripSeparator20";
			this.toolStripSeparator20.Size = new System.Drawing.Size(6, 25);
			this.tsbAddPictureMemo.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbAddPictureMemo.Image = GSPcLocalViewer.Properties.Resources.Add_Memo;
			this.tsbAddPictureMemo.ImageTransparentColor = Color.Magenta;
			this.tsbAddPictureMemo.Name = "tsbAddPictureMemo";
			this.tsbAddPictureMemo.Size = new System.Drawing.Size(23, 22);
			this.tsbAddPictureMemo.Text = "Add Picture Memo";
			this.tsbAddPictureMemo.Click += new EventHandler(this.tsbAddPictureMemo_Click);
			this.tsbThumbnail.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbThumbnail.Image = GSPcLocalViewer.Properties.Resources.Thumbnail;
			this.tsbThumbnail.ImageTransparentColor = Color.Magenta;
			this.tsbThumbnail.Name = "tsbThumbnail";
			this.tsbThumbnail.Size = new System.Drawing.Size(23, 22);
			this.tsbThumbnail.Text = "Show Thumbnail";
			this.tsbThumbnail.Click += new EventHandler(this.tsbThumbnail_Click);
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1161, 440);
			base.Controls.Add(this.toolStripContainer1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.IsMdiContainer = true;
			base.KeyPreview = true;
			base.Name = "frmViewer";
			this.Text = "GSPcLocal Viewer 3.0";
			base.WindowState = FormWindowState.Maximized;
			base.Deactivate += new EventHandler(this.frmViewer_Deactivate);
			base.Load += new EventHandler(this.frmViewer_Load);
			base.SizeChanged += new EventHandler(this.frmViewer_SizeChanged);
			base.Activated += new EventHandler(this.frmViewer_Activated);
			base.FormClosed += new FormClosedEventHandler(this.frmViewer_FormClosed);
			base.FormClosing += new FormClosingEventHandler(this.frmViewer_FormClosing);
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			this.pnlForm.ResumeLayout(false);
			((ISupportInitialize)this.picDisable).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tsPortal.ResumeLayout(false);
			this.tsPortal.PerformLayout();
			this.tsHistory.ResumeLayout(false);
			this.tsHistory.PerformLayout();
			this.tsNavigate.ResumeLayout(false);
			this.tsNavigate.PerformLayout();
			this.tsView.ResumeLayout(false);
			this.tsView.PerformLayout();
			this.tsSearch.ResumeLayout(false);
			this.tsSearch.PerformLayout();
			this.tsTools.ResumeLayout(false);
			this.tsTools.PerformLayout();
			this.tsFunctions.ResumeLayout(false);
			this.tsFunctions.PerformLayout();
			this.tsPic.ResumeLayout(false);
			this.tsPic.PerformLayout();
			base.ResumeLayout(false);
		}

		private static bool IsBizarreLocation(Point loc, System.Drawing.Size size)
		{
			bool flag;
			if (loc.X < 0 || loc.Y < 0)
			{
				flag = false;
			}
			else if (loc.X + size.Width <= Screen.PrimaryScreen.WorkingArea.Width)
			{
				flag = (loc.Y + size.Height <= Screen.PrimaryScreen.WorkingArea.Height ? true : false);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private static bool IsBizarreSize(System.Drawing.Size size)
		{
			if (size.Height > Screen.PrimaryScreen.WorkingArea.Height)
			{
				return false;
			}
			return size.Width <= Screen.PrimaryScreen.WorkingArea.Width;
		}

		private bool IsValidDateTime(string strInput)
		{
			bool flag;
			try
			{
				DateTime.ParseExact(strInput.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		private void lastPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbNavigateLast_Click(null, null);
		}

		public void LaunchAddOn(object sender, EventArgs e)
		{
			try
			{
				string empty = string.Empty;
				string fullPath = string.Empty;
				string str = string.Empty;
				empty = ((ToolStripMenuItem)sender).Tag.ToString();
				if (empty != string.Empty)
				{
					string[] strArrays = new string[] { "|" };
					string[] strArrays1 = empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					fullPath = strArrays1[0];
					if ((int)strArrays1.Length > 1)
					{
						str = strArrays1[1];
					}
					if (!Path.IsPathRooted(fullPath))
					{
						fullPath = Path.Combine(Application.StartupPath, fullPath);
						fullPath = Path.GetFullPath(fullPath);
					}
					Process.Start(fullPath, str);
				}
			}
			catch
			{
			}
		}

		private void lblMode_Click(object sender, EventArgs e)
		{
			this.ChangeApplicationMode();
		}

		public ArrayList ListOfInUseBooks()
		{
			return this.frmParent.ListOfInUseBooks();
		}

		public void LoadBookmarks()
		{
			string empty = string.Empty;
			try
			{
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				empty = string.Concat(empty, "\\", Application.ProductName);
				empty = string.Concat(empty, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				empty = string.Concat(empty, "\\BookMarks.xml");
				if (File.Exists(empty))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(empty);
					XmlNodeList xmlNodeLists = xmlDocument.SelectNodes(string.Concat("//BookMarks/BookMark[translate(@BookId, 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", this.p_BookId.ToUpper(), "']"));
					if (xmlNodeLists != null)
					{
						for (int i = 0; i < xmlNodeLists.Count; i++)
						{
							CustomToolStripMenuItem customToolStripMenuItem = new CustomToolStripMenuItem(xmlNodeLists[i].Attributes["PageName"].Value);
							string[] value = new string[] { xmlNodeLists[i].Attributes["BookId"].Value, " [", xmlNodeLists[i].Attributes["PageId"].Value, "] [", xmlNodeLists[i].Attributes["PicIndex"].Value, "][", xmlNodeLists[i].Attributes["ListIndex"].Value, "][", xmlNodeLists[i].Attributes["PartNo"].Value, "]" };
							customToolStripMenuItem.Name = string.Concat(value);
							customToolStripMenuItem.Tag = xmlNodeLists[i].OuterXml;
							string[] resource = new string[] { this.GetResource("Picture Index", "PICTURE_INDEX", ResourceType.TOOLSTRIP), " = ", xmlNodeLists[i].Attributes["PicIndex"].Value, "\n", this.GetResource("List Index", "LIST_INDEX", ResourceType.TOOLSTRIP), " = ", xmlNodeLists[i].Attributes["ListIndex"].Value };
							customToolStripMenuItem.ToolTipText = string.Concat(resource);
							if (xmlNodeLists[i].Attributes["PartNo"].Value != string.Empty)
							{
								string[] toolTipText = new string[] { customToolStripMenuItem.ToolTipText, "\n", this.GetResource("Part Number", "PART_NUMBER", ResourceType.TOOLSTRIP), " = ", xmlNodeLists[i].Attributes["PartNo"].Value };
								customToolStripMenuItem.ToolTipText = string.Concat(toolTipText);
							}
							customToolStripMenuItem.OnOpen += new CustomToolStripMenuItem.ClickHandler(this.OpenBookmarkPage);
							customToolStripMenuItem.OnDelete += new CustomToolStripMenuItem.ClickHandler(this.DeleteBookmarkPage);
							this.bookmarksToolStripMenuItem.DropDown.Items.Add(customToolStripMenuItem);
						}
					}
				}
				if (this.bookmarksToolStripMenuItem.DropDown.Items.Count == 2)
				{
					this.toolStripSeparator.Visible = false;
				}
			}
			catch
			{
			}
		}

		public void LoadData(int serverId, string bookPublishingId, XmlNode bookNode, XmlNode schemaNode, string bookType)
		{
			if (Program.bNoViewerOpen || this.objFrmTreeview.GetNodesCount() <= 0 || Settings.Default.OpenInCurrentInstance)
			{
				this.p_ServerId = serverId;
				this.p_BookId = bookPublishingId;
				this.p_BookNode = bookNode;
				this.p_SchemaNode = schemaNode;
				this.p_BookType = bookType;
				if (this.objFrmTreeview.DockPanel == null && this.objFrmPicture.DockPanel == null && this.objFrmSelectionlist.DockPanel == null && this.objFrmPartlist.DockPanel == null && this.objFrmInfo.DockPanel == null)
				{
					this.ShowForms();
				}
				this.objFrmTreeview.ShowLoading();
				this.objFrmPicture.ShowLoading();
				this.objFrmInfo.ShowLoading();
				if (this.objFrmPartlist != null)
				{
					this.HidePartsList();
				}
				this.objFrmTreeview.LoadBook();
				Program.bNoViewerOpen = false;
				return;
			}
			ArrayList arrayLists = new ArrayList();
			arrayLists.Add("-o");
			arrayLists.Add(Program.iniServers[serverId].sIniKey);
			arrayLists.Add(bookPublishingId);
			if (this.p_ArgsF != null && (int)this.p_ArgsF.Length > 0)
			{
				arrayLists.Add("-f");
				string[] pArgsF = this.p_ArgsF;
				for (int i = 0; i < (int)pArgsF.Length; i++)
				{
					arrayLists.Add(pArgsF[i]);
				}
			}
			if (this.p_ArgsS == null || (int)this.p_ArgsS.Length <= 0)
			{
				arrayLists.Add("-s");
				arrayLists.Add(string.Concat("fromportal=", Program.objAppMode.bFromPortal.ToString()));
			}
			else
			{
				arrayLists.Add("-s");
				string[] pArgsS = this.p_ArgsS;
				for (int j = 0; j < (int)pArgsS.Length; j++)
				{
					string str = pArgsS[j];
					if (!str.ToLower().Contains("fromportal="))
					{
						arrayLists.Add(str);
					}
				}
				arrayLists.Add(string.Concat("fromportal=", Program.objAppMode.bFromPortal.ToString()));
			}
			this.frmParent.NextTime((string[])arrayLists.ToArray(typeof(string)));
		}

		public void LoadDataDirect()
		{
			try
			{
				base.SendToBack();
				base.BringToFront();
				int item = -1;
				if (Program.iniKeys[this.p_ArgsO[0].ToUpper()] == null)
				{
					throw new Exception();
				}
				item = (int)Program.iniKeys[this.p_ArgsO[0].ToUpper()];
				this.p_ServerId = item;
				if (Program.iniKeys.Count <= 0)
				{
					MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
				}
				else
				{
					if (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
					{
						this.p_Encrypted = true;
					}
					if (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
					{
						this.p_Compressed = true;
					}
					if (this.p_ArgsO == null)
					{
						MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
					}
					else if ((int)this.p_ArgsO.Length < 2)
					{
						MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
					}
					else
					{
						string empty = string.Empty;
						string str = string.Empty;
						if (!Program.iniKeys.ContainsKey(this.p_ArgsO[0].ToUpper()))
						{
							MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
						}
						else
						{
							if (this.objFrmTreeview.DockPanel == null && this.objFrmPicture.DockPanel == null && this.objFrmSelectionlist.DockPanel == null && this.objFrmPartlist.DockPanel == null && this.objFrmInfo.DockPanel == null)
							{
								this.ShowForms();
							}
							this.objFrmTreeview.ShowLoading();
							this.objFrmPicture.ShowLoading();
							this.objFrmInfo.ShowLoading();
							if (this.objFrmPartlist != null)
							{
								this.HidePartsList();
							}
							try
							{
								empty = Program.iniServers[item].items["SETTINGS", "CONTENT_PATH"];
								if (!empty.EndsWith("/"))
								{
									empty = string.Concat(empty, "/");
								}
								str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
								str = string.Concat(str, "\\", Program.iniServers[item].sIniKey);
								if (!Directory.Exists(str))
								{
									Directory.CreateDirectory(str);
								}
								if (!this.p_Compressed)
								{
									empty = string.Concat(empty, "Series.xml");
									str = string.Concat(str, "\\Series.xml");
								}
								else
								{
									empty = string.Concat(empty, "Series.zip");
									str = string.Concat(str, "\\Series.zip");
								}
								Program.bNoViewerOpen = false;
								this.bgWorker.RunWorkerAsync(new object[] { empty, str });
							}
							catch
							{
								MessageHandler.ShowError(this.GetResource("(E-VWR-EM001) Failed to create file/folder specified", "(E-VWR-EM001)_FILE/FOLDER", ResourceType.POPUP_MESSAGE));
							}
						}
					}
				}
			}
			catch
			{
				this.p_ArgsO = null;
				MessageHandler.ShowInformation(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.POPUP_MESSAGE));
			}
		}

		public void LoadDataFromNode(XmlNode xNode)
		{
			if (this.tsHistory.InvokeRequired)
			{
				ToolStrip toolStrip = this.tsHistory;
				frmViewer.LoadDataFromNodeDelegate loadDataFromNodeDelegate = new frmViewer.LoadDataFromNodeDelegate(this.LoadDataFromNode);
				object[] objArray = new object[] { xNode };
				toolStrip.Invoke(loadDataFromNodeDelegate, objArray);
				return;
			}
			if (xNode == null || xNode == null)
			{
				if (!this.objFrmTreeview.IsDisposed)
				{
					this.objFrmTreeview.Dispose();
				}
				if (!this.objFrmPicture.IsDisposed)
				{
					this.objFrmPicture.Dispose();
				}
				if (!this.objFrmSelectionlist.IsDisposed)
				{
					this.objFrmSelectionlist.Dispose();
				}
				if (!this.objFrmPartlist.IsDisposed)
				{
					this.objFrmPartlist.Dispose();
				}
				if (!this.objFrmInfo.IsDisposed)
				{
					this.objFrmInfo.Dispose();
				}
				this.EnableMenuAndToolbar(false);
			}
			else
			{
				this.tsbHistoryBack.Enabled = false;
				this.tsbHistoryForward.Enabled = false;
				this.tsbHistoryList.Enabled = false;
				if (xNode != null && xNode != null)
				{
					if (xNode.Attributes["Filters"] == null)
					{
						this.p_ArgsF = null;
					}
					else
					{
						int num = 0;
						if (int.TryParse(xNode.Attributes["Filters"].Value, out num))
						{
							this.p_ArgsF = new string[num];
							for (int i = 0; i < num; i++)
							{
								this.p_ArgsF[i] = xNode.Attributes[i.ToString()].Value;
							}
						}
					}
					this.p_ArgsO = new string[] { xNode.Attributes["ServerKey"].Value, xNode.Attributes["BookId"].Value, xNode.Attributes["PageId"].Value, xNode.Attributes["PicIndex"].Value, xNode.Attributes["ListIndex"].Value, xNode.Attributes["PartNo"].Value };
					if (this.p_ServerId != (int)Program.iniKeys[xNode.Attributes["ServerKey"].Value] || !(this.p_BookId == xNode.Attributes["BookId"].Value))
					{
						this.LoadDataDirect();
						return;
					}
					this.objFrmTreeview.TreeViewClearSelection();
					this.SelectTreeNode();
					return;
				}
			}
		}

		public void LoadEnglish()
		{
			try
			{
				this.xmlDocument = null;
				string str = string.Concat(Application.StartupPath, "\\Language XMLs\\");
				string[] files = Directory.GetFiles(str, "*.xml");
				for (int i = 0; i < (int)files.Length; i++)
				{
					try
					{
						if (File.Exists(files[i]))
						{
							int num = files[i].IndexOf(".xml");
							int num1 = files[i].LastIndexOf("\\");
							string str1 = files[i].Substring(num1 + 1, num - num1 - 1);
							string str2 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml");
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml"));
							XmlNode xmlNodes = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
							string value = xmlNodes.Attributes["EN_NAME"].Value;
							if (value.Contains("English"))
							{
								if (File.Exists(str2))
								{
									this.xmlDocument = new XmlDocument();
									this.xmlDocument.Load(str2);
									this.CurrentLanguage = "English";
									this.SetText();
									this.ChangeApplicationLanguage();
									break;
								}
							}
							else if (!value.Contains(str2) && i == str1.Length)
							{
								this.xmlDocument = new XmlDocument();
								this.CurrentLanguage = "English";
								this.SetText();
								this.ChangeApplicationLanguage();
							}
						}
					}
					catch
					{
						this.xmlDocument = new XmlDocument();
						this.SetText();
						this.ChangeApplicationLanguage();
						break;
					}
				}
				if (this.xmlDocument == null)
				{
					this.xmlDocument = new XmlDocument();
					this.SetText();
					this.ChangeApplicationLanguage();
				}
			}
			catch
			{
			}
		}

		public void LoadMemos()
		{
			if (Program.objMemoSession.getLocalMemoDoc(this.p_ServerId) != null)
			{
				this.xDocLocalMemo = Program.objMemoSession.getLocalMemoDoc(this.p_ServerId);
			}
			else
			{
				this.xDocLocalMemo = new XmlDocument();
				string empty = string.Empty;
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				empty = string.Concat(empty, "\\", Application.ProductName);
				empty = string.Concat(empty, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				if (!Directory.Exists(empty))
				{
					Directory.CreateDirectory(empty);
				}
				empty = string.Concat(empty, "\\LocalMemo.xml");
				try
				{
					if (!File.Exists(empty))
					{
						this.xDocLocalMemo.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos/>");
					}
					else
					{
						this.xDocLocalMemo.Load(empty);
					}
				}
				catch
				{
					MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM010) Failed to load specified object", "(E-VWR-EM010)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
				}
				Program.objMemoSession.addLocalMemo(this.p_ServerId, this.xDocLocalMemo);
			}
			this.xDocGlobalMemo = new XmlDocument();
			string globalMemoFolder = string.Empty;
			globalMemoFolder = Settings.Default.GlobalMemoFolder;
			if (globalMemoFolder == string.Empty)
			{
				globalMemoFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				globalMemoFolder = string.Concat(globalMemoFolder, "\\", Application.ProductName);
				globalMemoFolder = string.Concat(globalMemoFolder, "\\", Program.iniServers[this.p_ServerId].sIniKey);
			}
			if (!Directory.Exists(globalMemoFolder))
			{
				try
				{
					Directory.CreateDirectory(globalMemoFolder);
				}
				catch
				{
					globalMemoFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					globalMemoFolder = string.Concat(globalMemoFolder, "\\", Application.ProductName);
					globalMemoFolder = string.Concat(globalMemoFolder, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				}
			}
			globalMemoFolder = string.Concat(globalMemoFolder, "\\GlobalMemo.xml");
			try
			{
				if (!File.Exists(globalMemoFolder))
				{
					this.xDocGlobalMemo.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos/>");
				}
				else
				{
					this.xDocGlobalMemo.Load(globalMemoFolder);
				}
			}
			catch
			{
				MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM010) Failed to load specified object", "(E-VWR-EM010)_FAILED_LOAD", ResourceType.POPUP_MESSAGE));
			}
			Program.objMemoSession.addGlobalMemo(this.p_ServerId, this.xDocGlobalMemo);
		}

		public void LoadPartsList(XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
		{
			try
			{
				if (this.p_ArgsO == null || (int)this.p_ArgsO.Length <= 5 || !(this.p_ArgsO[5] != "nil"))
				{
					this.objFrmPartlist.highlightPartNo = string.Empty;
				}
				else
				{
					this.objFrmPartlist.highlightPartNo = this.p_ArgsO[5];
				}
				this.objFrmPartlist.changePartList(schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
				if (this.p_ArgsO != null)
				{
					this.p_ArgsO = null;
				}
			}
			catch
			{
				this.HidePartsList();
			}
		}

		public void LoadPicture(XmlNode schemaNode, XmlNode pageNode)
		{
			if (this.p_ArgsO == null || (int)this.p_ArgsO.Length <= 3 || !(this.p_ArgsO[3] != "nil"))
			{
				if (this.iPageJumpImageIndex == 0)
				{
					this.objFrmPicture.LoadPicture(schemaNode, pageNode, 0, 0);
					return;
				}
				this.objFrmPicture.LoadPicture(schemaNode, pageNode, this.iPageJumpImageIndex, 0);
				this.iPageJumpImageIndex = 0;
				return;
			}
			int num = 0;
			if (!int.TryParse(this.p_ArgsO[3], out num))
			{
				this.objFrmPicture.LoadPicture(schemaNode, pageNode, 0, 0);
				return;
			}
			if (num <= 0)
			{
				num = 1;
			}
			num--;
			if (this.p_ArgsO == null || (int)this.p_ArgsO.Length <= 4 || !(this.p_ArgsO[4] != "nil"))
			{
				this.objFrmPicture.LoadPicture(schemaNode, pageNode, num, 0);
				return;
			}
			int num1 = 0;
			if (!int.TryParse(this.p_ArgsO[4], out num1))
			{
				this.objFrmPicture.LoadPicture(schemaNode, pageNode, num, 0);
				return;
			}
			if (num1 <= 0)
			{
				num1 = 1;
			}
			num1--;
			this.objFrmPicture.LoadPicture(schemaNode, pageNode, num, num1);
		}

		public void LoadXML(string filename)
		{
			string @default = Settings.Default.appLanguage;
			string value = "";
			string str = "";
			try
			{
				if (filename != "English")
				{
					string str1 = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] files = Directory.GetFiles(str1, "*.xml");
					if ((int)files.Length > 0)
					{
						for (int i = 0; i < (int)files.Length; i++)
						{
							try
							{
								if (File.Exists(files[i]))
								{
									int num = files[i].IndexOf(".xml");
									int num1 = files[i].LastIndexOf("\\");
									string str2 = files[i].Substring(num1 + 1, num - num1 - 1);
									string.Concat(Application.StartupPath, "\\Language XMLs\\", str2, ".xml");
									XmlDocument xmlDocument = new XmlDocument();
									xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str2, ".xml"));
									XmlNode xmlNodes = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
									str = xmlNodes.Attributes["Language"].Value;
									value = xmlNodes.Attributes["EN_NAME"].Value;
									if (str.ToLower() == filename.ToLower() || value.ToLower() == filename.ToLower())
									{
										this.xmlDocument = new XmlDocument();
										this.xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str2, ".xml"));
										if (value != null || value.Length != 0)
										{
											Settings.Default.appLanguage = value;
											this.CurrentLanguage = value;
										}
										this.SetText();
										this.ChangeApplicationLanguage();
										break;
									}
								}
							}
							catch
							{
								if (filename.Length != 0 && str.Length != 0 && filename == str)
								{
									MessageBox.Show(this.GetResource("Failed to load Language XML.", "FAILED_XML", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
									Settings.Default.appLanguage = @default;
									break;
								}
							}
						}
					}
				}
				else
				{
					this.LoadEnglish();
				}
			}
			catch
			{
				MessageBox.Show(this.GetResource("Failed to load Language XML.", "FAILED_XML", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public void LoadXMLFirstTime()
		{
			this.xmlDocument = null;
			try
			{
				if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
				{
					this.GetOSLanguage();
					string str = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] files = Directory.GetFiles(str, "*.xml");
					for (int i = 0; i < (int)files.Length; i++)
					{
						try
						{
							if (File.Exists(files[i]))
							{
								int num = files[i].IndexOf(".xml");
								int num1 = files[i].LastIndexOf("\\");
								string str1 = files[i].Substring(num1 + 1, num - num1 - 1);
								string str2 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml");
								XmlDocument xmlDocument = new XmlDocument();
								xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml"));
								XmlNode xmlNodes = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
								string value = xmlNodes.Attributes["EN_NAME"].Value;
								if (value.Length != 0 && value != null && value == Settings.Default.appLanguage && File.Exists(str2))
								{
									this.xmlDocument = new XmlDocument();
									this.xmlDocument.Load(str2);
									Settings.Default.appLanguage = value;
									this.CurrentLanguage = value;
									goto yoyo0;
								}
							}
						}
						catch
						{
						}
					}
				}
				else if (Settings.Default.appLanguage == null || Settings.Default.appLanguage.Length == 0)
				{
					Settings.Default.appLanguage = "English";
					string oSLanguage = this.GetOSLanguage();
					string str3 = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] strArrays = Directory.GetFiles(str3, "*.xml");
					for (int j = 0; j < (int)strArrays.Length; j++)
					{
						try
						{
							if (File.Exists(strArrays[j]))
							{
								int num2 = strArrays[j].IndexOf(".xml");
								int num3 = strArrays[j].LastIndexOf("\\");
								string str4 = strArrays[j].Substring(num3 + 1, num2 - num3 - 1);
								string str5 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml");
								XmlDocument xmlDocument1 = new XmlDocument();
								xmlDocument1.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml"));
								XmlNode xmlNodes1 = xmlDocument1.SelectSingleNode("//GSPcLocalViewer");
								string value1 = xmlNodes1.Attributes["EN_NAME"].Value;
								if (value1 == oSLanguage)
								{
									if (File.Exists(str5))
									{
										this.xmlDocument.Load(str5);
										break;
									}
								}
								else if (value1 == Settings.Default.appLanguage && File.Exists(str5))
								{
									this.xmlDocument.Load(str5);
									break;
								}
							}
						}
						catch
						{
						}
					}
				}
			yoyo0:
			}
			catch
			{
			}
			if (this.xmlDocument == null)
			{
				this.xmlDocument = new XmlDocument();
				Settings.Default.appLanguage = "English";
			}
		}

		private void manageDiskSpaceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmDataSize _frmDataSize = new frmDataSize(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmDataSize.Show();
		}

		private void memoDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!Settings.Default.EnableLocalMemo && !Settings.Default.EnableAdminMemo && !Settings.Default.EnableGlobalMemo)
			{
				this.viewMemoListToolStripMenuItem.Visible = false;
				return;
			}
			this.viewMemoListToolStripMenuItem.Visible = true;
		}

		private void memoRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmMemoRecovery _frmMemoRecovery = new frmMemoRecovery(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmMemoRecovery.Show();
		}

		private void memosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Memo_Settings);
		}

		public void miniMapToolStripChkUnchk(bool chkMiniMap)
		{
			if (!chkMiniMap)
			{
				this.miniMapToolStripMenuItem.Checked = false;
				if (this.miniMapToolStripMenuItem.Enabled)
				{
					frmViewer.MiniMapChk = false;
				}
			}
			else
			{
				this.miniMapToolStripMenuItem.Checked = true;
				if (this.miniMapToolStripMenuItem.Enabled)
				{
					frmViewer.MiniMapChk = true;
					return;
				}
			}
		}

		private void miniMapToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.miniMapToolStripMenuItem.Checked = !this.miniMapToolStripMenuItem.Checked;
			if (this.miniMapToolStripMenuItem.Checked)
			{
				this.objFrmPicture.ShowHideMiniMap(true);
				frmViewer.MiniMapChk = true;
				return;
			}
			this.objFrmPicture.ShowHideMiniMap(false);
			frmViewer.MiniMapChk = false;
		}

		private void multipleBooksToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (DataSize.spaceLeft < (long)10485760)
			{
				this.ShowNotification();
				MessageBox.Show(this, string.Concat(this.GetResource("Low Disk Space", "LOW_DISK", ResourceType.POPUP_MESSAGE), "\n", this.GetResource("Manage disk to free some space", "FREE_SPACE", ResourceType.POPUP_MESSAGE)), this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			string empty = string.Empty;
			string item = string.Empty;
			item = Program.iniServers[this.ServerId].items["SETTINGS", "CONTENT_PATH"];
			if (!item.EndsWith("/"))
			{
				item = string.Concat(item, "/");
			}
			empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
			empty = string.Concat(empty, "\\", Program.iniServers[this.ServerId].sIniKey, "\\");
			if (!Directory.Exists(empty))
			{
				Directory.CreateDirectory(empty);
			}
			frmMultipleBooksDownload _frmMultipleBooksDownload = new frmMultipleBooksDownload(this, empty, item)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmMultipleBooksDownload.Show();
		}

		private void nextPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbNavigateNext_Click(null, null);
		}

		private void nextViewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbHistoryForward_Click(null, null);
		}

		public void OnOffFeatures()
		{
			try
			{
				if (Program.objAppFeatures.bDcMode)
				{
					Program.objAppFeatures.bDownloadBook = false;
					Program.objAppFeatures.bDownloadBookAll = false;
				}
				this.tsbSearchPageName.Visible = Program.objAppFeatures.bPageNameSearch;
				this.tsbSearchPartName.Visible = Program.objAppFeatures.bPartNameSearch;
				this.tsbSearchPartNumber.Visible = Program.objAppFeatures.bPartNumberSearch;
				this.tsbSearchPartAdvance.Visible = Program.objAppFeatures.bPartAdvanceSearch;
				this.tsbSearchText.Visible = Program.objAppFeatures.bTextSearch;
				this.pageNameToolStripMenuItem.Visible = Program.objAppFeatures.bPageNameSearch;
				this.partNameToolStripMenuItem.Visible = Program.objAppFeatures.bPartNameSearch;
				this.partNumberToolStripMenuItem.Visible = Program.objAppFeatures.bPartNumberSearch;
				this.textSearchToolStripMenuItem.Visible = Program.objAppFeatures.bTextSearch;
				this.advancedSearchToolStripMenuItem.Visible = Program.objAppFeatures.bPartAdvanceSearch;
				if (Program.objAppFeatures.bPageNameSearch || Program.objAppFeatures.bPartNameSearch || Program.objAppFeatures.bPartNumberSearch || Program.objAppFeatures.bTextSearch || Program.objAppFeatures.bPartAdvanceSearch)
				{
					this.searchToolStripMenuItem.Visible = true;
					this.tsSearch.Visible = true;
				}
				else
				{
					this.searchToolStripMenuItem.Visible = false;
					this.tsSearch.Visible = false;
				}
				this.pageNameSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPageNameSearch;
				this.partNameSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartNameSearch;
				this.partNumberSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartNumberSearch;
				this.textSearceNameSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bTextSearch;
				this.advanceSearchSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartAdvanceSearch;
				if (Program.objAppFeatures.bPageNameSearch || Program.objAppFeatures.bPartNameSearch || Program.objAppFeatures.bPartNumberSearch)
				{
					this.toolStripSeparator9.Visible = true;
				}
				else
				{
					this.toolStripSeparator9.Visible = false;
				}
				this.toolStripSeparator5.Visible = false;
				if (Program.objAppFeatures.bPartNameSearch || Program.objAppFeatures.bPartNumberSearch || Program.objAppFeatures.bPartAdvanceSearch)
				{
					this.toolStripSeparator5.Visible = true;
				}
				if ((Program.objAppFeatures.bPartNameSearch || Program.objAppFeatures.bPartNumberSearch) && Program.objAppFeatures.bPartAdvanceSearch)
				{
					this.toolStripSeparator6.Visible = true;
				}
				else
				{
					this.toolStripSeparator6.Visible = false;
				}
				this.memoSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bMemo;
				this.toolStripSeparator14.Visible = Program.objAppFeatures.bMemo;
				this.memoDetailsToolStripMenuItem.Visible = Program.objAppFeatures.bMemo;
				this.tsbMemoRecovery.Visible = Program.objAppFeatures.bMemo;
				this.singleBookToolStripMenuItem.Visible = Program.objAppFeatures.bDownloadBook;
				this.multipleBooksToolStripMenuItem.Visible = Program.objAppFeatures.bDownloadBookAll;
				this.toolsToolStripMenuItem.Visible = (Program.objAppFeatures.bDownloadBook ? true : Program.objAppFeatures.bDownloadBookAll);
				this.toolStripSeparator18.Visible = (Program.objAppFeatures.bDownloadBook ? true : Program.objAppFeatures.bDownloadBookAll);
				this.tsbSingleBookDownload.Visible = Program.objAppFeatures.bDownloadBook;
				this.tsbMultipleBooksDownload.Visible = Program.objAppFeatures.bDownloadBookAll;
				if (Program.objAppFeatures.bDownloadBook || Program.objAppFeatures.bDownloadBookAll)
				{
					this.toolStripSeparator15.Visible = true;
				}
				else
				{
					this.toolStripSeparator15.Visible = false;
				}
				this.printToolStripMenuItem.Visible = Program.objAppFeatures.bPrint;
				this.toolStripSeparator11.Visible = Program.objAppFeatures.bPrint;
				this.tsbPrint.Visible = Program.objAppFeatures.bPrint;
				this.tsbAddBookmarks.Visible = Program.objAppFeatures.bBookMark;
				this.bookmarksToolStripMenuItem.Visible = Program.objAppFeatures.bBookMark;
				this.tsbAddPictureMemo.Visible = Program.objAppFeatures.bMemo;
				this.tsbMemoList.Visible = Program.objAppFeatures.bMemo;
				this.tsHistory.Visible = Program.objAppFeatures.bHistory;
				this.previousViewToolStripMenuItem.Visible = Program.objAppFeatures.bHistory;
				this.nextViewToolStripMenuItem.Visible = Program.objAppFeatures.bHistory;
				this.toolStripSeparator12.Visible = Program.objAppFeatures.bHistory;
				this.tsbPicZoomSelect.Visible = Program.objAppFeatures.bSelectiveZoom;
				this.tsbPicCopy.Visible = Program.objAppFeatures.bCopyRegion;
				this.tsbFitPage.Visible = Program.objAppFeatures.bFitPage;
				this.tsbPicPanMode.Visible = Program.objAppFeatures.bDjVuPan;
				this.tsbFindText.Visible = Program.objAppFeatures.bDjVuSearch;
				this.tsbPicSelectText.Visible = Program.objAppFeatures.bDjVuSelectText;
				this.tsbPicZoomIn.Visible = Program.objAppFeatures.bDjVuZoomIn;
				this.tsbPicZoomOut.Visible = Program.objAppFeatures.bDjVuZoomOut;
				this.tsBRotateLeft.Visible = Program.objAppFeatures.bDjVuRotateLeft;
				this.tsBRotateRight.Visible = Program.objAppFeatures.bDjVuRotateRight;
				this.tsbThumbnail.Visible = Program.objAppFeatures.bDjVuNavPan;
				this.toolStripSeparator10.Visible = (Program.objAppFeatures.bDjVuSearch || Program.objAppFeatures.bDjVuPan || Program.objAppFeatures.bDjVuSelectZoom || Program.objAppFeatures.bFitPage || Program.objAppFeatures.bCopyRegion ? true : Program.objAppFeatures.bDjVuSelectText);
				this.toolStripSeparator22.Visible = (Program.objAppFeatures.bDjVuZoomIn ? true : Program.objAppFeatures.bDjVuZoomOut);
				this.toolStripSeparator20.Visible = (Program.objAppFeatures.bDjVuRotateLeft ? true : Program.objAppFeatures.bDjVuRotateRight);
				this.helpToolStripMenuItem.Visible = (Program.objAppFeatures.bHelpMenu ? true : Program.objAppFeatures.bAboutMenu);
				this.aboutGSPcLocalToolStripMenuItem.Visible = Program.objAppFeatures.bAboutMenu;
				this.helpToolStripMenuItem.Visible = Program.objAppFeatures.bHelpMenu;
				this.tsbAbout.Visible = Program.objAppFeatures.bAboutMenu;
				this.tsbHelp.Visible = Program.objAppFeatures.bHelpMenu;
				this.miniMapToolStripMenuItem.Visible = Program.objAppFeatures.bMiniMap;
				this.menuStrip1.Visible = Program.objAppFeatures.bMenu;
				this.tsbOpenBook.Visible = Program.objAppFeatures.bOpenBookScreen;
				this.openToolStripMenuItem.Visible = Program.objAppFeatures.bOpenBookScreen;
				this.tsbThirdPartyBasket.Visible = Program.objAppFeatures.bThirdPartyBasket;
				this.partsListSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bPartsList;
				this.selectionListSettingsToolStripMenuItem.Visible = Program.objAppFeatures.bSelectionList;
				if (!Program.objAppFeatures.bPartsList && !Program.objAppFeatures.bSelectionList)
				{
					this.toolStripSeparator23.Visible = false;
				}
			}
			catch
			{
			}
		}

		public void OpenBookFromString(string str)
		{
			int item;
			string[] strArrays = new string[] { " " };
			string[] strArrays1 = str.Split(strArrays, StringSplitOptions.None);
			string[] strArrays2 = new string[] { "-o", null, null, null, null, null, null };
			for (int i = 1; i < 7; i++)
			{
				if (i >= (int)strArrays1.Length + 1)
				{
					strArrays2[i] = "nil";
				}
				else if (strArrays1[i - 1].Trim() == string.Empty)
				{
					strArrays2[i] = "nil";
				}
				else
				{
					strArrays2[i] = strArrays1[i - 1];
				}
			}
			try
			{
				item = (int)Program.iniKeys[strArrays2[1].ToUpper()];
			}
			catch
			{
				item = 9999;
			}
			XmlNode bookNode = this.GetBookNode(strArrays2[2], item);
			if (bookNode != null)
			{
				XmlNode xmlNodes = bookNode.SelectSingleNode("//Schema");
				if (xmlNodes != null && Global.SecurityLocksOpen(bookNode, xmlNodes, item, this))
				{
					this.frmParent.NextTime(strArrays2);
				}
			}
		}

		private void OpenBookmarkPage(CustomToolStripMenuItem sender)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodes = null;
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(sender.Tag.ToString()));
			xmlNodes = xmlDocument.ReadNode(xmlTextReader);
			if (Settings.Default.OpenInCurrentInstance)
			{
				this.p_ArgsO = new string[] { xmlNodes.Attributes["ServerKey"].Value, xmlNodes.Attributes["BookId"].Value, xmlNodes.Attributes["PageId"].Value, xmlNodes.Attributes["PicIndex"].Value, xmlNodes.Attributes["ListIndex"].Value, xmlNodes.Attributes["PartNo"].Value };
				this.objFrmTreeview.TreeViewClearSelection();
				this.SelectTreeNode();
				return;
			}
			ArrayList arrayLists = new ArrayList();
			arrayLists.Add("-o");
			arrayLists.Add(xmlNodes.Attributes["ServerKey"].Value);
			arrayLists.Add(xmlNodes.Attributes["BookId"].Value);
			arrayLists.Add(xmlNodes.Attributes["PageId"].Value);
			arrayLists.Add(xmlNodes.Attributes["PicIndex"].Value);
			arrayLists.Add(xmlNodes.Attributes["ListIndex"].Value);
			arrayLists.Add(xmlNodes.Attributes["PartNo"].Value);
			if (this.p_ArgsF != null && (int)this.p_ArgsF.Length > 0)
			{
				arrayLists.Add("-f");
				string[] pArgsF = this.p_ArgsF;
				for (int i = 0; i < (int)pArgsF.Length; i++)
				{
					arrayLists.Add(pArgsF[i]);
				}
			}
			if (this.p_ArgsS != null && (int)this.p_ArgsS.Length > 0)
			{
				arrayLists.Add("-s");
				string[] pArgsS = this.p_ArgsS;
				for (int j = 0; j < (int)pArgsS.Length; j++)
				{
					arrayLists.Add(pArgsS[j]);
				}
			}
			this.frmParent.NextTime((string[])arrayLists.ToArray(typeof(string)));
		}

		public void OpenBookmarkPage(XmlNode xNode)
		{
			if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
			{
				this.p_ArgsO = new string[] { xNode.Attributes["ServerKey"].Value, xNode.Attributes["BookId"].Value, xNode.Attributes["PageId"].Value, xNode.Attributes["PicIndex"].Value, xNode.Attributes["ListIndex"].Value, xNode.Attributes["PartNo"].Value };
				this.LoadDataDirect();
				return;
			}
			ArrayList arrayLists = new ArrayList();
			arrayLists.Add("-o");
			arrayLists.Add(xNode.Attributes["ServerKey"].Value);
			arrayLists.Add(xNode.Attributes["BookId"].Value);
			arrayLists.Add(xNode.Attributes["PageId"].Value);
			arrayLists.Add(xNode.Attributes["PicIndex"].Value);
			arrayLists.Add(xNode.Attributes["ListIndex"].Value);
			arrayLists.Add(xNode.Attributes["PartNo"].Value);
			if (this.p_ArgsF != null && (int)this.p_ArgsF.Length > 0)
			{
				arrayLists.Add("-f");
				string[] pArgsF = this.p_ArgsF;
				for (int i = 0; i < (int)pArgsF.Length; i++)
				{
					arrayLists.Add(pArgsF[i]);
				}
			}
			if (this.p_ArgsS != null && (int)this.p_ArgsS.Length > 0)
			{
				arrayLists.Add("-s");
				string[] pArgsS = this.p_ArgsS;
				for (int j = 0; j < (int)pArgsS.Length; j++)
				{
					arrayLists.Add(pArgsS[j]);
				}
			}
			this.frmParent.NextTime((string[])arrayLists.ToArray(typeof(string)));
		}

		public void OpenDefaultBrowser(string sUrl)
		{
			try
			{
				if (sUrl != string.Empty)
				{
					string str = (new RegistryReader()).Read("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\iexplore.exe", string.Empty);
					if (str != null)
					{
						using (Process process = Process.Start(str, sUrl))
						{
							if (process != null)
							{
								IntPtr handle = process.Handle;
								frmViewer.SetForegroundWindow(process.Handle);
								frmViewer.SetActiveWindow(process.Handle);
							}
						}
					}
					else
					{
						using (Process process1 = Process.Start(sUrl))
						{
							if (process1 != null)
							{
								IntPtr intPtr = process1.Handle;
								frmViewer.SetForegroundWindow(process1.Handle);
								frmViewer.SetActiveWindow(process1.Handle);
							}
						}
					}
				}
				else
				{
					MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			catch
			{
			}
		}

		public void OpenParentPage(string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex, string sPartNumber)
		{
			try
			{
				this.p_ArgsO = new string[] { sServerKey, sBookPubId, sPageId, sImageIndex, sListIndex, sPartNumber };
				this.LoadDataDirect();
			}
			catch
			{
			}
		}

		public void OpenPrintDialogue(int iPrintType)
		{
			GSPcLocalViewer.frmPrint.frmPrint _frmPrint = new GSPcLocalViewer.frmPrint.frmPrint(this, iPrintType);
			base.Enabled = false;
			_frmPrint.Owner = this;
			_frmPrint.Show();
		}

		public void OpenSearch(string _ServerKey, string _BookPublishingId, string _PageId, string _ImageIndex, string _ListIndex, string _PartNumber)
		{
			this.p_ArgsO = new string[] { _ServerKey, _BookPublishingId, _PageId, _ImageIndex, _ListIndex, _PartNumber };
			if (this.p_ServerId != (int)Program.iniKeys[_ServerKey] || !(this.p_BookId == _BookPublishingId))
			{
				this.LoadDataDirect();
				return;
			}
			this.objFrmTreeview.TreeViewClearSelection();
			this.SelectTreeNode();
		}

		public void OpenSearchResults(string sServerKey, string sBookCode, string sPageId, string sPicIndex, string sListIndex, string sPartNumber)
		{
			if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
			{
				this.p_ArgsO = new string[] { sServerKey, sBookCode, sPageId, sPicIndex, sListIndex, sPartNumber };
				this.LoadDataDirect();
				return;
			}
			ArrayList arrayLists = new ArrayList();
			arrayLists.Add("-o");
			arrayLists.Add(sServerKey);
			arrayLists.Add(sBookCode);
			arrayLists.Add(sPageId);
			arrayLists.Add(sPicIndex);
			arrayLists.Add(sListIndex);
			arrayLists.Add(sPartNumber);
			if (this.p_ArgsF != null && (int)this.p_ArgsF.Length > 0)
			{
				arrayLists.Add("-f");
				string[] pArgsF = this.p_ArgsF;
				for (int i = 0; i < (int)pArgsF.Length; i++)
				{
					arrayLists.Add(pArgsF[i]);
				}
			}
			if (this.p_ArgsS != null && (int)this.p_ArgsS.Length > 0)
			{
				arrayLists.Add("-s");
				string[] pArgsS = this.p_ArgsS;
				for (int j = 0; j < (int)pArgsS.Length; j++)
				{
					arrayLists.Add(pArgsS[j]);
				}
			}
			this.frmParent.NextTime((string[])arrayLists.ToArray(typeof(string)));
		}

		public void OpenSearchResults(string sServerKey, string sBookCode, string sPageId, string sPicIndex, string sListIndex, string sPartNumber, string sHighlightText)
		{
			if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
			{
				this.p_ArgsO = new string[] { sServerKey, sBookCode, sPageId, sPicIndex, sListIndex, sPartNumber };
				this.LoadDataDirect();
				return;
			}
			ArrayList arrayLists = new ArrayList();
			arrayLists.Add("-o");
			arrayLists.Add(sServerKey);
			arrayLists.Add(sBookCode);
			arrayLists.Add(sPageId);
			arrayLists.Add(sPicIndex);
			arrayLists.Add(sListIndex);
			arrayLists.Add(sPartNumber);
			if (this.p_ArgsF != null && (int)this.p_ArgsF.Length > 0)
			{
				arrayLists.Add("-f");
				string[] pArgsF = this.p_ArgsF;
				for (int i = 0; i < (int)pArgsF.Length; i++)
				{
					arrayLists.Add(pArgsF[i]);
				}
			}
			if (this.p_ArgsS != null && (int)this.p_ArgsS.Length > 0)
			{
				arrayLists.Add("-s");
				string[] pArgsS = this.p_ArgsS;
				for (int j = 0; j < (int)pArgsS.Length; j++)
				{
					arrayLists.Add(pArgsS[j]);
				}
			}
			this.frmParent.NextTime((string[])arrayLists.ToArray(typeof(string)));
		}

		public void OpenSpecifiedBrowser(string sUrl)
		{
			try
			{
				string item = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
				if (item == string.Empty || item == null)
				{
					item = "iexplore";
				}
				string empty = string.Empty;
				RegistryReader registryReader = new RegistryReader();
				empty = registryReader.Read(string.Concat("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", item, ".exe"), string.Empty);
				if (!(empty != string.Empty) || empty == null)
				{
					this.OpenDefaultBrowser(sUrl);
				}
				else if (!(sUrl != string.Empty) || sUrl == null)
				{
					MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					using (Process process = Process.Start(empty, sUrl))
					{
						if (process != null)
						{
							IntPtr handle = process.Handle;
							frmViewer.SetForegroundWindow(process.Handle);
							frmViewer.SetActiveWindow(process.Handle);
						}
					}
				}
			}
			catch
			{
			}
		}

		public void OpenTextSearch(string _PageName, string _picIndex, string _Text)
		{
			if (Program.bNoViewerOpen || Settings.Default.OpenInCurrentInstance)
			{
				this.objFrmPicture.HighLightText = Program.HighLightText;
				this.objFrmPicture.DjVuPageNumber = Program.DjVuPageNumber;
				this.p_ArgsO = new string[] { this.ServerId.ToString(), this.BookPublishingId, "1", _picIndex, "1", string.Empty };
				this.objFrmTreeview.TreeViewClearSelection();
				this.SelectTreeNodeByName(_PageName);
				this.objFrmPicture.HighLightText = _Text;
				return;
			}
			ArrayList arrayLists = new ArrayList();
			arrayLists.Add("-o");
			arrayLists.Add(Program.iniServers[this.ServerId].sIniKey);
			arrayLists.Add(this.BookPublishingId);
			XmlDocument xmlDocument = new XmlDocument();
			TreeNode treeNode = this.objFrmTreeview.FindTreeNodeByPageName(this.objFrmTreeview.tvBook.Nodes, _PageName);
			if (treeNode == null)
			{
				arrayLists.Add("1");
			}
			else
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.objFrmTreeview.tvBook.Tag.ToString()));
				xmlDocument.ReadNode(xmlTextReader);
				xmlTextReader = new XmlTextReader(new StringReader(treeNode.Tag.ToString()));
				xmlDocument.ReadNode(xmlTextReader);
				arrayLists.Add(treeNode.Text);
			}
			arrayLists.Add(_picIndex);
			arrayLists.Add("1");
			arrayLists.Add(string.Empty);
			if (this.p_ArgsF != null && (int)this.p_ArgsF.Length > 0)
			{
				arrayLists.Add("-f");
				string[] pArgsF = this.p_ArgsF;
				for (int i = 0; i < (int)pArgsF.Length; i++)
				{
					arrayLists.Add(pArgsF[i]);
				}
			}
			if (this.p_ArgsS != null && (int)this.p_ArgsS.Length > 0)
			{
				arrayLists.Add("-s");
				string[] pArgsS = this.p_ArgsS;
				for (int j = 0; j < (int)pArgsS.Length; j++)
				{
					arrayLists.Add(pArgsS[j]);
				}
			}
			this.frmParent.NextTime((string[])arrayLists.ToArray(typeof(string)));
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmOpenBook _frmOpenBook = new frmOpenBook(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmOpenBook.Show();
		}

		public void PageJump(string[] sArgs)
		{
			try
			{
				string item = Program.iniGSPcLocal.items["SETTINGS", "PAGE_JUMP"];
				string empty = string.Empty;
				if (item == null)
				{
					item = "SAME";
				}
				if (item.ToUpper() != "SAME")
				{
					this.frmParent.NextTime(sArgs);
				}
				else
				{
					empty = sArgs[3];
					string str = sArgs[4];
					TreeNode treeNode = this.objFrmTreeview.FindTreeNodeByPageName(this.objFrmTreeview.tvBook.Nodes, empty);
					if (treeNode != null)
					{
						int num = 0;
						if (int.TryParse(sArgs[4], out num))
						{
							this.iPageJumpImageIndex = num - 1;
						}
						this.objFrmTreeview.tvBook.SelectedNode = treeNode;
					}
					else if (this.objFrmTreeview.tvBook.Nodes.Count > 0)
					{
						this.objFrmTreeview.tvBook.SelectedNode = this.objFrmTreeview.tvBook.Nodes[0];
					}
				}
			}
			catch
			{
			}
		}

		private void pageNameSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Search_PageName);
		}

		private void pageNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSearch _frmSearch = new frmSearch(this)
			{
				Owner = this,
				TopMost = true
			};
			this.ShowDimmer();
			_frmSearch.Show(SearchTasks.PageName);
		}

		public bool PartInSelectionList(string sPartNumber)
		{
			bool flag;
			try
			{
				flag = this.objFrmSelectionlist.PartInSelectionList(sPartNumber, Program.iniServers[this.p_ServerId].sIniKey, this.p_BookId, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, this.objFrmPartlist.CurrentListIndex);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public bool PartInSelectionListA(string sPartNumber)
		{
			bool flag;
			try
			{
				flag = this.frmParent.PartInSelectionListA(sPartNumber, Program.iniServers[this.p_ServerId].sIniKey, this.p_BookId, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, this.objFrmPartlist.CurrentListIndex);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public void PartMemoExists(string partNumber, string sAdminMemoValues, int iGridRowIndex)
		{
			string[] pBookId;
			XmlNodeList xmlNodeLists = null;
			XmlNodeList xmlNodeLists1 = null;
			XmlNodeList xmlNodeLists2 = null;
			try
			{
				this.GetSaveMemoValue();
				if (!this.bSaveMemoOnBookLevel || !this.bSaveMemoOnBookLevelChecked)
				{
					this.objFrmPartlist.ClearMemoIcons(iGridRowIndex);
				}
				else
				{
					int plPartNumColIndex = this.GetPlPartNumColIndex();
					foreach (DataGridViewRow row in (IEnumerable)this.objFrmPartlist.dgPartslist.Rows)
					{
						if (row.Cells[plPartNumColIndex].Value.ToString().Trim().ToUpper() != partNumber.ToUpper().Trim())
						{
							continue;
						}
						this.objFrmPartlist.ClearMemoIcons(row.Index);
					}
				}
			}
			catch (Exception exception)
			{
			}
			if (Settings.Default.EnableLocalMemo)
			{
				XmlDocument xmlDocument = this.xDocLocalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@ListIndex='", this.objFrmPartlist.CurrentListIndex, "'][@PartNo='", partNumber, "']" };
				xmlNodeLists1 = xmlDocument.SelectNodes(string.Concat(pBookId));
				try
				{
					if (xmlNodeLists1.Count > 0)
					{
						SortedDictionary<DateTime, XmlNode> dateTimes = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes in xmlNodeLists1)
						{
							DateTime dateTime = DateTime.ParseExact(xmlNodes.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes, dateTime, xmlNodes);
						}
						XmlDocument xmlDocument1 = new XmlDocument();
						XmlNode xmlNodes1 = xmlDocument1.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() != "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in dateTimes)
							{
								XmlNode xmlNodes2 = xmlDocument1.CreateElement("Memo");
								XmlNode value = keyValuePair.Value;
								for (int i = 0; i < value.Attributes.Count; i++)
								{
									XmlAttribute xmlAttribute = xmlDocument1.CreateAttribute(value.Attributes[i].Name);
									xmlAttribute.Value = value.Attributes[i].Value;
									xmlNodes2.Attributes.Append(xmlAttribute);
								}
								xmlNodes1.AppendChild(xmlNodes2);
							}
						}
						else
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair1 in dateTimes.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes3 = xmlDocument1.CreateElement("Memo");
								XmlNode value1 = keyValuePair1.Value;
								for (int j = 0; j < value1.Attributes.Count; j++)
								{
									XmlAttribute xmlAttribute1 = xmlDocument1.CreateAttribute(value1.Attributes[j].Name);
									xmlAttribute1.Value = value1.Attributes[j].Value;
									xmlNodes3.Attributes.Append(xmlAttribute1);
								}
								xmlNodes1.AppendChild(xmlNodes3);
							}
						}
						xmlDocument1.AppendChild(xmlNodes1);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists1 = xmlDocument1.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception1)
				{
					XmlDocument xmlDocument2 = this.xDocLocalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists1 = xmlDocument2.SelectNodes(string.Concat(pBookId));
				}
				try
				{
					if (xmlNodeLists1.Count > 0)
					{
						List<int> nums = new List<int>();
						int num = 0;
						if (this.bSaveMemoOnBookLevel && this.bSaveMemoOnBookLevelChecked)
						{
							int num1 = 0;
							while (num1 < this.objFrmPartlist.dgPartslist.Columns.Count)
							{
								if (this.objFrmPartlist.dgPartslist.Columns[num1].Tag == null || !(this.objFrmPartlist.dgPartslist.Columns[num1].Tag.ToString() == "PartNumber"))
								{
									num1++;
								}
								else
								{
									num = num1;
									break;
								}
							}
							for (int k = 0; k < this.objFrmPartlist.dgPartslist.Rows.Count; k++)
							{
								try
								{
									if (this.objFrmPartlist.dgPartslist.Rows[k].Cells[num].Value.ToString() == partNumber)
									{
										nums.Add(k);
									}
								}
								catch (Exception exception2)
								{
								}
							}
							if (xmlNodeLists1.Count > 0)
							{
								for (int l = 0; l < xmlNodeLists1.Count; l++)
								{
									this.objFrmPartlist.AddMemoIcon("MEM", nums[l]);
									this.objFrmPartlist.AddMemoIcon("LOCMEM", nums[l]);
								}
							}
							for (int m = 0; m < xmlNodeLists1.Count; m++)
							{
								foreach (XmlNode xmlNodes4 in xmlNodeLists1)
								{
									try
									{
										if (xmlNodes4.Attributes["Type"] != null)
										{
											if (xmlNodes4.Attributes["Type"].Value.ToUpper() == "TXT")
											{
												this.objFrmPartlist.AddMemoIcon("LOCTXTMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
											}
											if (xmlNodes4.Attributes["Type"].Value.ToUpper() == "HYP")
											{
												this.objFrmPartlist.AddMemoIcon("LOCHYPMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
											}
											if (xmlNodes4.Attributes["Type"].Value.ToUpper() == "REF")
											{
												this.objFrmPartlist.AddMemoIcon("LOCREFMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
											}
											if (xmlNodes4.Attributes["Type"].Value.ToUpper() == "PRG")
											{
												this.objFrmPartlist.AddMemoIcon("LOCPRGMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
											}
										}
									}
									catch
									{
									}
								}
							}
						}
						else if (xmlNodeLists1.Count > 0)
						{
							this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
							this.objFrmPartlist.AddMemoIcon("LOCMEM", iGridRowIndex);
							try
							{
								if (this.bSaveMemoOnBookLevel && this.bSaveMemoOnBookLevelChecked)
								{
									int plPartNumColIndex1 = this.GetPlPartNumColIndex();
									foreach (DataGridViewRow dataGridViewRow in (IEnumerable)this.objFrmPartlist.dgPartslist.Rows)
									{
										if (dataGridViewRow.Cells[plPartNumColIndex1].Value.ToString().Trim().ToUpper() != partNumber.ToUpper().Trim())
										{
											continue;
										}
										this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
										this.objFrmPartlist.AddMemoIcon("LOCMEM", iGridRowIndex);
									}
								}
							}
							catch (Exception exception3)
							{
							}
							foreach (XmlNode xmlNodes5 in xmlNodeLists1)
							{
								try
								{
									if (xmlNodes5.Attributes["Type"] != null)
									{
										if (xmlNodes5.Attributes["Type"].Value.ToUpper() == "TXT")
										{
											this.objFrmPartlist.AddMemoIcon("LOCTXTMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
										}
										if (xmlNodes5.Attributes["Type"].Value.ToUpper() == "HYP")
										{
											this.objFrmPartlist.AddMemoIcon("LOCHYPMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
										}
										if (xmlNodes5.Attributes["Type"].Value.ToUpper() == "REF")
										{
											this.objFrmPartlist.AddMemoIcon("LOCREFMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
										}
										if (xmlNodes5.Attributes["Type"].Value.ToUpper() == "PRG")
										{
											this.objFrmPartlist.AddMemoIcon("LOCPRGMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
										}
									}
								}
								catch
								{
								}
							}
						}
					}
				}
				catch (Exception exception4)
				{
				}
			}
			if (Settings.Default.EnableGlobalMemo)
			{
				XmlDocument xmlDocument3 = this.xDocGlobalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@ListIndex='", this.objFrmPartlist.CurrentListIndex, "'][@PartNo='", partNumber, "']" };
				xmlNodeLists = xmlDocument3.SelectNodes(string.Concat(pBookId));
				try
				{
					if (xmlNodeLists.Count > 0)
					{
						SortedDictionary<DateTime, XmlNode> dateTimes1 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes6 in xmlNodeLists)
						{
							DateTime dateTime1 = DateTime.ParseExact(xmlNodes6.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes1, dateTime1, xmlNodes6);
						}
						XmlDocument xmlDocument4 = new XmlDocument();
						XmlNode xmlNodes7 = xmlDocument4.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() != "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> dateTime2 in dateTimes1)
							{
								XmlNode xmlNodes8 = xmlDocument4.CreateElement("Memo");
								XmlNode value2 = dateTime2.Value;
								for (int n = 0; n < value2.Attributes.Count; n++)
								{
									XmlAttribute xmlAttribute2 = xmlDocument4.CreateAttribute(value2.Attributes[n].Name);
									xmlAttribute2.Value = value2.Attributes[n].Value;
									xmlNodes8.Attributes.Append(xmlAttribute2);
								}
								xmlNodes7.AppendChild(xmlNodes8);
							}
						}
						else
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair2 in dateTimes1.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes9 = xmlDocument4.CreateElement("Memo");
								XmlNode value3 = keyValuePair2.Value;
								for (int o = 0; o < value3.Attributes.Count; o++)
								{
									XmlAttribute xmlAttribute3 = xmlDocument4.CreateAttribute(value3.Attributes[o].Name);
									xmlAttribute3.Value = value3.Attributes[o].Value;
									xmlNodes9.Attributes.Append(xmlAttribute3);
								}
								xmlNodes7.AppendChild(xmlNodes9);
							}
						}
						xmlDocument4.AppendChild(xmlNodes7);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists = xmlDocument4.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception5)
				{
					XmlDocument xmlDocument5 = this.xDocGlobalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists = xmlDocument5.SelectNodes(string.Concat(pBookId));
				}
				try
				{
					if (xmlNodeLists.Count > 0)
					{
						List<int> nums1 = new List<int>();
						int num2 = 0;
						if (!this.bSaveMemoOnBookLevel || !this.bSaveMemoOnBookLevelChecked)
						{
							if (xmlNodeLists.Count > 0)
							{
								this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
								this.objFrmPartlist.AddMemoIcon("GBLMEM", iGridRowIndex);
							}
							try
							{
								if (this.bSaveMemoOnBookLevel && this.bSaveMemoOnBookLevelChecked)
								{
									int plPartNumColIndex2 = this.GetPlPartNumColIndex();
									foreach (DataGridViewRow row1 in (IEnumerable)this.objFrmPartlist.dgPartslist.Rows)
									{
										if (row1.Cells[plPartNumColIndex2].Value.ToString().Trim().ToUpper() != partNumber.ToUpper().Trim())
										{
											continue;
										}
										this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
										this.objFrmPartlist.AddMemoIcon("GBLMEM", iGridRowIndex);
									}
								}
							}
							catch (Exception exception6)
							{
							}
							foreach (XmlNode xmlNodes10 in xmlNodeLists)
							{
								try
								{
									if (xmlNodes10.Attributes["Type"] != null)
									{
										if (xmlNodes10.Attributes["Type"].Value.ToUpper() == "TXT")
										{
											this.objFrmPartlist.AddMemoIcon("GBLTXTMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
										}
										if (xmlNodes10.Attributes["Type"].Value.ToUpper() == "HYP")
										{
											this.objFrmPartlist.AddMemoIcon("GBLHYPMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
										}
										if (xmlNodes10.Attributes["Type"].Value.ToUpper() == "REF")
										{
											this.objFrmPartlist.AddMemoIcon("GBLREFMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
										}
										if (xmlNodes10.Attributes["Type"].Value.ToUpper() == "PRG")
										{
											this.objFrmPartlist.AddMemoIcon("GBLPRGMEM", iGridRowIndex);
											this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
										}
									}
								}
								catch
								{
								}
							}
						}
						else
						{
							int num3 = 0;
							while (num3 < this.objFrmPartlist.dgPartslist.Columns.Count)
							{
								if (this.objFrmPartlist.dgPartslist.Columns[num3].Tag == null || !(this.objFrmPartlist.dgPartslist.Columns[num3].Tag.ToString() == "PartNumber"))
								{
									num3++;
								}
								else
								{
									num2 = num3;
									break;
								}
							}
							for (int p = 0; p < this.objFrmPartlist.dgPartslist.Rows.Count; p++)
							{
								try
								{
									if (this.objFrmPartlist.dgPartslist.Rows[p].Cells[num2].Value.ToString() == partNumber)
									{
										nums1.Add(p);
									}
								}
								catch (Exception exception7)
								{
								}
							}
							if (xmlNodeLists.Count > 0)
							{
								for (int q = 0; q < xmlNodeLists.Count; q++)
								{
									this.objFrmPartlist.AddMemoIcon("MEM", nums1[q]);
									this.objFrmPartlist.AddMemoIcon("GBLMEM", nums1[q]);
								}
							}
							for (int r = 0; r < xmlNodeLists.Count; r++)
							{
								foreach (XmlNode xmlNodes11 in xmlNodeLists)
								{
									try
									{
										if (xmlNodes11.Attributes["Type"] != null)
										{
											if (xmlNodes11.Attributes["Type"].Value.ToUpper() == "TXT")
											{
												this.objFrmPartlist.AddMemoIcon("GBLTXTMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
											}
											if (xmlNodes11.Attributes["Type"].Value.ToUpper() == "HYP")
											{
												this.objFrmPartlist.AddMemoIcon("GBLHYPMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
											}
											if (xmlNodes11.Attributes["Type"].Value.ToUpper() == "REF")
											{
												this.objFrmPartlist.AddMemoIcon("GBLREFMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
											}
											if (xmlNodes11.Attributes["Type"].Value.ToUpper() == "PRG")
											{
												this.objFrmPartlist.AddMemoIcon("GBLPRGMEM", iGridRowIndex);
												this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
											}
										}
									}
									catch
									{
									}
								}
							}
						}
					}
				}
				catch (Exception exception8)
				{
				}
			}
			if (Settings.Default.EnableAdminMemo)
			{
				XmlDocument xmlDocument6 = new XmlDocument();
				xmlDocument6.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
				pBookId = new string[] { "**" };
				string[] strArrays = sAdminMemoValues.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
				if ((int)strArrays.Length > 0)
				{
					string empty = string.Empty;
					string[] strArrays1 = strArrays;
					for (int s = 0; s < (int)strArrays1.Length; s++)
					{
						string str = strArrays1[s];
						if (str.Contains("||"))
						{
							pBookId = new string[] { "||" };
							string[] strArrays2 = str.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
							empty = strArrays2[0];
							empty = (empty.ToUpper().Equals("TXT") || empty.ToUpper().Equals("REF") || empty.ToUpper().Equals("HYP") || empty.ToUpper().Equals("PRG") ? empty.ToLower() : "Unknown");
							if (empty != "Unknown")
							{
								if (empty == "txt")
								{
									this.objFrmPartlist.AddMemoIcon("ADMTXTMEM", iGridRowIndex);
									this.objFrmPartlist.AddMemoIcon("TXTMEM", iGridRowIndex);
								}
								if (empty == "hyp")
								{
									this.objFrmPartlist.AddMemoIcon("ADMHYPMEM", iGridRowIndex);
									this.objFrmPartlist.AddMemoIcon("HYPMEM", iGridRowIndex);
								}
								if (empty == "ref")
								{
									this.objFrmPartlist.AddMemoIcon("ADMREFMEM", iGridRowIndex);
									this.objFrmPartlist.AddMemoIcon("REFMEM", iGridRowIndex);
								}
								if (empty == "prg")
								{
									this.objFrmPartlist.AddMemoIcon("ADMPRGMEM", iGridRowIndex);
									this.objFrmPartlist.AddMemoIcon("PRGMEM", iGridRowIndex);
								}
							}
							string str1 = strArrays2[1];
							xmlDocument6.DocumentElement.AppendChild(xmlDocument6.ImportNode(this.CreateAdminMemoNode(empty, str1, string.Empty, string.Empty), true));
						}
					}
					if (empty != string.Empty && empty.ToLower() != "unknown")
					{
						this.objFrmPartlist.AddMemoIcon("MEM", iGridRowIndex);
						this.objFrmPartlist.AddMemoIcon("ADMMEM", iGridRowIndex);
					}
				}
				xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				try
				{
					if (xmlNodeLists2.Count > 0)
					{
						SortedDictionary<DateTime, XmlNode> dateTimes2 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes12 in xmlNodeLists2)
						{
							DateTime dateTime3 = DateTime.ParseExact(xmlNodes12.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes2, dateTime3, xmlNodes12);
						}
						XmlDocument xmlDocument7 = new XmlDocument();
						XmlNode xmlNodes13 = xmlDocument7.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() != "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair3 in dateTimes2)
							{
								XmlNode xmlNodes14 = xmlDocument7.CreateElement("Memo");
								XmlNode value4 = keyValuePair3.Value;
								for (int t = 0; t < value4.Attributes.Count; t++)
								{
									XmlAttribute xmlAttribute4 = xmlDocument7.CreateAttribute(value4.Attributes[t].Name);
									xmlAttribute4.Value = value4.Attributes[t].Value;
									xmlNodes14.Attributes.Append(xmlAttribute4);
								}
								xmlNodes13.AppendChild(xmlNodes14);
							}
						}
						else
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair4 in dateTimes2.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes15 = xmlDocument7.CreateElement("Memo");
								XmlNode value5 = keyValuePair4.Value;
								for (int u = 0; u < value5.Attributes.Count; u++)
								{
									XmlAttribute xmlAttribute5 = xmlDocument7.CreateAttribute(value5.Attributes[u].Name);
									xmlAttribute5.Value = value5.Attributes[u].Value;
									xmlNodes15.Attributes.Append(xmlAttribute5);
								}
								xmlNodes13.AppendChild(xmlNodes15);
							}
						}
						xmlDocument7.AppendChild(xmlNodes13);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists2 = xmlDocument7.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception9)
				{
					xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				}
			}
		}

		private void partNameSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Search_PartName);
		}

		private void partNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSearch _frmSearch = new frmSearch(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmSearch.Show(SearchTasks.PartName);
		}

		private void partNumberSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Search_PartNumber);
		}

		private void partNumberToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSearch _frmSearch = new frmSearch(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmSearch.Show(SearchTasks.PartNumber);
		}

		private void partsListInfoFontAndColorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Viewer_Color);
		}

		private void partsListSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				frmConfig _frmConfig = new frmConfig(this)
				{
					Owner = this
				};
				this.ShowDimmer();
				_frmConfig.Show(ConfigTasks.PartListSettings);
			}
			catch (Exception exception)
			{
			}
		}

		private void partslistToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.partslistToolStripMenuItem.Checked = !this.partslistToolStripMenuItem.Checked;
			if (!this.partslistToolStripMenuItem.Checked)
			{
				this.objFrmPartlist.Hide();
				this.bPartsListClosed = true;
				return;
			}
			this.objFrmPartlist.Show(this.objDocPanel);
			this.bPartsListClosed = false;
		}

		private void pictureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.pictureToolStripMenuItem.Checked = !this.pictureToolStripMenuItem.Checked;
			if (!this.pictureToolStripMenuItem.Checked)
			{
				this.bImageClosed = true;
				this.bPictureClosed = true;
				this.objFrmPicture.Hide();
				return;
			}
			this.bImageClosed = false;
			this.bPictureClosed = false;
			this.objFrmPicture.Show(this.objDocPanel);
		}

		private void previousPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbNavigatePrevious_Click(null, null);
		}

		private void previousViewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbHistoryBack_Click(null, null);
		}

		private void printListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.OpenPrintDialogue(2);
		}

		private void printPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.OpenPrintDialogue(0);
		}

		private void printPictureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.OpenPrintDialogue(1);
		}

		private void printSelectionListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.OpenPrintDialogue(3);
		}

		private void printToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsbPrint_Click(null, null);
		}

		private void ReadSearchHistoryPath()
		{
			try
			{
				this.sSearchHistoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				frmViewer _frmViewer = this;
				_frmViewer.sSearchHistoryPath = string.Concat(_frmViewer.sSearchHistoryPath, "\\", Application.ProductName);
				frmViewer _frmViewer1 = this;
				_frmViewer1.sSearchHistoryPath = string.Concat(_frmViewer1.sSearchHistoryPath, "\\SearchHistory.xml");
				if (!File.Exists(this.sSearchHistoryPath))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(string.Concat("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", "<SEARCH>\r\n                    <PAGENAMESEARCH></PAGENAMESEARCH>\r\n                    <TEXTSEARCH></TEXTSEARCH>\r\n                    <PARTNUMBERSEARCH></PARTNUMBERSEARCH>\r\n                    <PARTNAMESEARCH></PARTNAMESEARCH>\r\n                    </SEARCH>"));
					xmlDocument.Save(this.sSearchHistoryPath);
				}
			}
			catch
			{
			}
		}

		private void ReadUserSettingINI()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\UserSet.ini");
				IniFile iniFile = new IniFile(str, "UserSet");
				if (iniFile.items["PROXY_SETTINGS", "PROXY_IP"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_IP"] != null)
				{
					Settings.Default.appProxyIP = iniFile.items["PROXY_SETTINGS", "PROXY_IP"];
				}
				if (iniFile.items["PROXY_SETTINGS", "PROXY_PORT"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_PORT"] != null)
				{
					Settings.Default.appProxyPort = iniFile.items["PROXY_SETTINGS", "PROXY_PORT"];
				}
				if (iniFile.items["PROXY_SETTINGS", "PROXY_TYPE"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_TYPE"] != null)
				{
					Settings.Default.appProxyType = iniFile.items["PROXY_SETTINGS", "PROXY_TYPE"];
				}
				if (iniFile.items["PROXY_SETTINGS", "PROXY_TIMEOUT"] != "" && iniFile.items["PROXY_SETTINGS", "PROXY_TIMEOUT"] != null)
				{
					Settings.Default.appProxyTimeOut = iniFile.items["PROXY_SETTINGS", "PROXY_TIMEOUT"];
				}
				if (iniFile.items["PROXY_SETTINGS", "USERNAME"] != "" && iniFile.items["PROXY_SETTINGS", "USERNAME"] != null)
				{
					Settings.Default.appProxyLogin = iniFile.items["PROXY_SETTINGS", "USERNAME"];
				}
				if (iniFile.items["PROXY_SETTINGS", "PASSWORD"] != "" && iniFile.items["PROXY_SETTINGS", "PASSWORD"] != null)
				{
					Settings.Default.appProxyPassword = iniFile.items["PROXY_SETTINGS", "PASSWORD"];
				}
				if (iniFile.items["FONT", "FONT_NAME"] != "" && iniFile.items["FONT", "FONT_NAME"] != null && (new System.Drawing.Font(iniFile.items["FONT", "FONT_NAME"], float.Parse(iniFile.items["FONT", "FONT_SIZE"].ToString()))).Name == iniFile.items["FONT", "FONT_NAME"])
				{
					Settings.Default.appFont = new System.Drawing.Font(iniFile.items["FONT", "FONT_NAME"], float.Parse(iniFile.items["FONT", "FONT_SIZE"].ToString()));
				}
				if (iniFile.items["FONT_PRINT", "FONT_NAME"] != "" && iniFile.items["FONT_PRINT", "FONT_NAME"] != null)
				{
					if ((new System.Drawing.Font(iniFile.items["FONT_PRINT", "FONT_NAME"], float.Parse(iniFile.items["FONT_PRINT", "FONT_SIZE"].ToString()))).Name == iniFile.items["FONT_PRINT", "FONT_NAME"])
					{
						Settings.Default.printFont = new System.Drawing.Font(iniFile.items["FONT_PRINT", "FONT_NAME"], float.Parse(iniFile.items["FONT_PRINT", "FONT_SIZE"].ToString()));
					}
				}
				else if ((new System.Drawing.Font(iniFile.items["FONT", "FONT_NAME"], float.Parse(iniFile.items["FONT", "FONT_SIZE"].ToString()))).Name == iniFile.items["FONT", "FONT_NAME"])
				{
					Settings.Default.printFont = new System.Drawing.Font(iniFile.items["FONT", "FONT_NAME"], float.Parse("8"));
				}
				if (iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != null)
				{
					Settings.Default.appHighlightBackColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"]);
				}
				if (iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "BACKGROUND_COLOR"] != null)
				{
					Settings.Default.appHighlightForeColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "FORE_COLOR"]);
				}
				if (iniFile.items["CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR"] != null)
				{
					Settings.Default.PartsListInfoBackColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR"]);
				}
				if (iniFile.items["CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR"] != "" && iniFile.items["CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR"] != null)
				{
					Settings.Default.PartsListInfoForeColor = ColorTranslator.FromHtml(iniFile.items["CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR"]);
				}
				if (iniFile.items["OPEN_BOOK", "FRM_LOCATION"] != "" && iniFile.items["OPEN_BOOK", "FRM_LOCATION"] != null)
				{
					string item = iniFile.items["OPEN_BOOK", "FRM_LOCATION"];
					string[] strArrays = new string[] { "," };
					string[] strArrays1 = item.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmOpenBookLocation = new Point(int.Parse(strArrays1[0]), int.Parse(strArrays1[1]));
				}
				if (iniFile.items["OPEN_BOOK", "FRM_SIZE"] != "" && iniFile.items["OPEN_BOOK", "FRM_SIZE"] != null)
				{
					string item1 = iniFile.items["OPEN_BOOK", "FRM_SIZE"];
					string[] strArrays2 = new string[] { "," };
					string[] strArrays3 = item1.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmOpenBookSize = new System.Drawing.Size(int.Parse(strArrays3[0]), int.Parse(strArrays3[1]));
				}
				if (iniFile.items["OPEN_BOOK", "FRM_STATE"] != "" && iniFile.items["OPEN_BOOK", "FRM_STATE"] != null)
				{
					string str1 = iniFile.items["OPEN_BOOK", "FRM_STATE"];
					if (str1.ToUpper() == "NORMAL")
					{
						Settings.Default.frmOpenBookState = FormWindowState.Normal;
					}
					else if (str1.ToUpper() == "MAXIMIZED")
					{
						Settings.Default.frmOpenBookState = FormWindowState.Maximized;
					}
					else if (str1.ToUpper() == "MINIMIZED")
					{
						Settings.Default.frmOpenBookState = FormWindowState.Minimized;
					}
				}
				if (iniFile.items["OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE"] != "" && iniFile.items["OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE"] != null)
				{
					if (iniFile.items["OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE"].ToUpper() != "TRUE")
					{
						Settings.Default.OpenInCurrentInstance = false;
					}
					else
					{
						Settings.Default.OpenInCurrentInstance = true;
					}
				}
				if (iniFile.items["MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER"] != "" && iniFile.items["MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER"] != null)
				{
					Settings.Default.GlobalMemoFolder = iniFile.items["MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER"];
				}
				if (iniFile.items["MEMO_SETTINGS", "ENABLE_ADMIN_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "ENABLE_ADMIN_MEMO"] != null)
				{
					if (iniFile.items["MEMO_SETTINGS", "ENABLE_ADMIN_MEMO"].ToUpper() != "TRUE")
					{
						Settings.Default.EnableAdminMemo = false;
					}
					else
					{
						Settings.Default.EnableAdminMemo = true;
					}
				}
				if (iniFile.items["MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO"] != null)
				{
					if (iniFile.items["MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO"].ToUpper() != "TRUE")
					{
						Settings.Default.EnableGlobalMemo = false;
					}
					else
					{
						Settings.Default.EnableGlobalMemo = true;
					}
				}
				if (iniFile.items["MEMO_SETTINGS", "ENABLE_LOCAL_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "ENABLE_LOCAL_MEMO"] != null)
				{
					if (iniFile.items["MEMO_SETTINGS", "ENABLE_LOCAL_MEMO"].ToUpper() != "TRUE")
					{
						Settings.Default.EnableLocalMemo = false;
					}
					else
					{
						Settings.Default.EnableLocalMemo = true;
					}
				}
				if (iniFile.items["MEMO_SETTINGS", "POPUP_PICTURE_MEMO"] != "" && iniFile.items["MEMO_SETTINGS", "POPUP_PICTURE_MEMO"] != null)
				{
					if (iniFile.items["MEMO_SETTINGS", "POPUP_PICTURE_MEMO"].ToUpper() != "TRUE")
					{
						Settings.Default.PopupPictureMemo = false;
					}
					else
					{
						Settings.Default.PopupPictureMemo = true;
					}
				}
				if (iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP"] != "" && iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP"] != null)
				{
					if (iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP"].ToUpper() != "TRUE")
					{
						Settings.Default.LocalMemoBackup = false;
					}
					else
					{
						Settings.Default.LocalMemoBackup = true;
					}
				}
				if (iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE"] != "" && iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE"] != null)
				{
					Settings.Default.LocalMemoBackupFile = iniFile.items["MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE"];
				}
				if (iniFile.items["MEMO_SETTINGS", "MEMO_LIST_LOCATION"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LIST_LOCATION"] != null)
				{
					string item2 = iniFile.items["MEMO_SETTINGS", "MEMO_LIST_LOCATION"];
					string[] strArrays4 = new string[] { "," };
					string[] strArrays5 = item2.Split(strArrays4, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmMemoListLocation = new Point(int.Parse(strArrays5[0]), int.Parse(strArrays5[1]));
				}
				if (iniFile.items["MEMO_SETTINGS", "MEMO_LIST_SIZE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LIST_SIZE"] != null)
				{
					string str2 = iniFile.items["MEMO_SETTINGS", "MEMO_LIST_SIZE"];
					string[] strArrays6 = new string[] { "," };
					string[] strArrays7 = str2.Split(strArrays6, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmMemoListSize = new System.Drawing.Size(int.Parse(strArrays7[0]), int.Parse(strArrays7[1]));
				}
				if (iniFile.items["MEMO_SETTINGS", "MEMO_LIST_STATE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LIST_STATE"] != null)
				{
					string item3 = iniFile.items["MEMO_SETTINGS", "MEMO_LIST_STATE"];
					if (item3.ToUpper() == "NORMAL")
					{
						Settings.Default.frmMemoListState = FormWindowState.Normal;
					}
					else if (item3.ToUpper() == "MAXIMIZED")
					{
						Settings.Default.frmMemoListState = FormWindowState.Maximized;
					}
					else if (item3.ToUpper() == "MINIMIZED")
					{
						Settings.Default.frmMemoListState = FormWindowState.Minimized;
					}
				}
				if (iniFile.items["MEMO_SETTINGS", "MEMO_LOCATION"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_LOCATION"] != null)
				{
					string str3 = iniFile.items["MEMO_SETTINGS", "MEMO_LOCATION"];
					string[] strArrays8 = new string[] { "," };
					string[] strArrays9 = str3.Split(strArrays8, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmMemoLocation = new Point(int.Parse(strArrays9[0]), int.Parse(strArrays9[1]));
				}
				if (iniFile.items["MEMO_SETTINGS", "MEMO_SIZE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_SIZE"] != null)
				{
					string item4 = iniFile.items["MEMO_SETTINGS", "MEMO_SIZE"];
					string[] strArrays10 = new string[] { "," };
					string[] strArrays11 = item4.Split(strArrays10, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmMemoSize = new System.Drawing.Size(int.Parse(strArrays11[0]), int.Parse(strArrays11[1]));
				}
				if (iniFile.items["MEMO_SETTINGS", "MEMO_STATE"] != "" && iniFile.items["MEMO_SETTINGS", "MEMO_STATE"] != null)
				{
					string str4 = iniFile.items["MEMO_SETTINGS", "MEMO_STATE"];
					if (str4.ToUpper() == "NORMAL")
					{
						Settings.Default.frmMemoState = FormWindowState.Normal;
					}
					else if (str4.ToUpper() == "MAXIMIZED")
					{
						Settings.Default.frmMemoState = FormWindowState.Maximized;
					}
					else if (str4.ToUpper() == "MINIMIZED")
					{
						Settings.Default.frmMemoState = FormWindowState.Minimized;
					}
				}
				if (iniFile.items["ZOOM", "DEFAULT_PICTURE_ZOOM"] != "" && iniFile.items["ZOOM", "DEFAULT_PICTURE_ZOOM"] != null)
				{
					Settings.Default.DefaultPictureZoom = iniFile.items["ZOOM", "DEFAULT_PICTURE_ZOOM"];
				}
				if (iniFile.items["ZOOM", "MAINTAIN_ZOOM"] != "" && iniFile.items["ZOOM", "MAINTAIN_ZOOM"] != null)
				{
					if (iniFile.items["ZOOM", "MAINTAIN_ZOOM"].ToUpper() != "TRUE")
					{
						Settings.Default.MaintainZoom = false;
					}
					else
					{
						Settings.Default.MaintainZoom = true;
					}
				}
				if (iniFile.items["ZOOM", "ZOOM_STEP"] != "" && iniFile.items["ZOOM", "ZOOM_STEP"] != null)
				{
					Settings.Default.appZoomStep = int.Parse(iniFile.items["ZOOM", "ZOOM_STEP"]);
				}
				if (iniFile.items["ZOOM", "CURRENT_ZOOM"] != "" && iniFile.items["ZOOM", "CURRENT_ZOOM"] != null)
				{
					Settings.Default.appCurrentZoom = iniFile.items["ZOOM", "CURRENT_ZOOM"];
				}
				if (iniFile.items["TOOLBARS", "SHOW_PIC_TOOLBAR"] != "" && iniFile.items["TOOLBARS", "SHOW_PIC_TOOLBAR"] != null)
				{
					if (iniFile.items["TOOLBARS", "SHOW_PIC_TOOLBAR"].ToUpper() != "TRUE")
					{
						Settings.Default.ShowPicToolbar = false;
					}
					else
					{
						Settings.Default.ShowPicToolbar = true;
					}
				}
				if (iniFile.items["TOOLBARS", "SHOW_LIST_TOOLBAR"] != "" && iniFile.items["TOOLBARS", "SHOW_LIST_TOOLBAR"] != null)
				{
					if (iniFile.items["TOOLBARS", "SHOW_LIST_TOOLBAR"].ToUpper() != "TRUE")
					{
						Settings.Default.ShowListToolbar = false;
					}
					else
					{
						Settings.Default.ShowListToolbar = true;
					}
				}
				if (iniFile.items["SEARCH", "FRM_LOCATION"] != "" && iniFile.items["SEARCH", "FRM_LOCATION"] != null)
				{
					string item5 = iniFile.items["SEARCH", "FRM_LOCATION"];
					string[] strArrays12 = new string[] { "," };
					string[] strArrays13 = item5.Split(strArrays12, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmSearchLocation = new Point(int.Parse(strArrays13[0]), int.Parse(strArrays13[1]));
				}
				if (iniFile.items["SEARCH", "FRM_SIZE"] != "" && iniFile.items["SEARCH", "FRM_SIZE"] != null)
				{
					string str5 = iniFile.items["SEARCH", "FRM_SIZE"];
					string[] strArrays14 = new string[] { "," };
					string[] strArrays15 = str5.Split(strArrays14, StringSplitOptions.RemoveEmptyEntries);
					Settings.Default.frmSearchSize = new System.Drawing.Size(int.Parse(strArrays15[0]), int.Parse(strArrays15[1]));
				}
				if (iniFile.items["SEARCH", "FRM_STATE"] != "" && iniFile.items["SEARCH", "FRM_STATE"] != null)
				{
					string item6 = iniFile.items["SEARCH", "FRM_STATE"];
					if (item6.ToUpper() == "NORMAL")
					{
						Settings.Default.frmSearchState = FormWindowState.Normal;
					}
					else if (item6.ToUpper() == "MAXIMIZED")
					{
						Settings.Default.frmSearchState = FormWindowState.Maximized;
					}
					else if (item6.ToUpper() == "MINIMIZED")
					{
						Settings.Default.frmSearchState = FormWindowState.Minimized;
					}
				}
				if (iniFile.items["LANGUAGE", "APP_LANGUAGE"] != "" && iniFile.items["LANGUAGE", "APP_LANGUAGE"] != null)
				{
					Settings.Default.appLanguage = iniFile.items["LANGUAGE", "APP_LANGUAGE"];
				}
				if (iniFile.items["HISTORY", "SIZE"] != "" && iniFile.items["HISTORY", "SIZE"] != null)
				{
					Settings.Default.HistorySize = int.Parse(iniFile.items["HISTORY", "SIZE"]);
				}
				if (iniFile.items["MULTIPARTS", "FITPAGE"] != "" && iniFile.items["MULTIPARTS", "FITPAGE"] != null)
				{
					if (iniFile.items["MULTIPARTS", "FITPAGE"].ToUpper() != "TRUE")
					{
						Settings.Default.appFitPageForMultiParts = false;
					}
					else
					{
						Settings.Default.appFitPageForMultiParts = true;
					}
				}
				if (iniFile.items["APPLICATION_SETTINGS", "WEB_BROWSER"] != "" && iniFile.items["APPLICATION_SETTINGS", "WEB_BROWSER"] != null)
				{
					Settings.Default.appWebBrowser = iniFile.items["APPLICATION_SETTINGS", "WEB_BROWSER"];
				}
				if (iniFile.items["APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE"] != "" && iniFile.items["APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE"] != null)
				{
					Settings.Default.appCurrentSize = iniFile.items["APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE"];
				}
				if (iniFile.items["MINIMAP_SETTINGS", "SHOW_MINIMAP"] != "" && iniFile.items["MINIMAP_SETTINGS", "SHOW_MINIMAP"] != null)
				{
					frmViewer.iniValueMiniMap = iniFile.items["MINIMAP_SETTINGS", "SHOW_MINIMAP"].ToString().ToLower();
				}
				if (iniFile.items["PRINT_SETTINGS", "SIDE_BY_SIDE"] != "" && iniFile.items["PRINT_SETTINGS", "SIDE_BY_SIDE"] != null)
				{
					if (iniFile.items["PRINT_SETTINGS", "SIDE_BY_SIDE"].ToUpper() != "TRUE")
					{
						Settings.Default.SideBySidePrinting = false;
					}
					else
					{
						Settings.Default.SideBySidePrinting = true;
					}
				}
				if (!Program.objAppFeatures.bDjVuScroll)
				{
					Settings.Default.MouseScrollContents = false;
					Settings.Default.MouseScrollPicture = false;
				}
				else
				{
					if (iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS"] != "" && iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS"] != null)
					{
						if (iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS"].ToUpper() != "TRUE")
						{
							Settings.Default.MouseScrollContents = false;
						}
						else
						{
							Settings.Default.MouseScrollContents = true;
						}
					}
					if (iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE"] != "" && iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE"] != null)
					{
						if (iniFile.items["APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE"].ToUpper() != "TRUE")
						{
							Settings.Default.MouseScrollPicture = false;
						}
						else
						{
							Settings.Default.MouseScrollPicture = true;
						}
					}
				}
				try
				{
					if (iniFile.items["APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN"] != "" && iniFile.items["APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN"] != null)
					{
						Settings.Default.appPartsListInfoVisible = Convert.ToBoolean(iniFile.items["APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN"]);
					}
				}
				catch
				{
				}
				try
				{
					if (iniFile.items["APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION"] != string.Empty && iniFile.items["APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION"] != null)
					{
						Settings.Default.RowSelectionMode = Convert.ToBoolean(iniFile.items["APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION"]);
					}
				}
				catch
				{
				}
				try
				{
					if (iniFile.items["APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE"] != string.Empty && iniFile.items["APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE"] != null)
					{
						Settings.Default.ListSplitterDistance = int.Parse(iniFile.items["APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE"]);
					}
				}
				catch
				{
				}
				if (iniFile.items["APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS"] != null && iniFile.items["APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS"] != "")
				{
					if (iniFile.items["APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS"].ToUpper() != "TRUE")
					{
						Settings.Default.ExpandAllContents = false;
					}
					else
					{
						Settings.Default.ExpandAllContents = true;
					}
				}
				if (iniFile.items["APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL"] != null && iniFile.items["APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL"] != "")
				{
					Settings.Default.ExpandContentsLevel = int.Parse(iniFile.items["APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL"]);
				}
				if (iniFile.items["APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU"] != null && iniFile.items["APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU"] != "")
				{
					if (iniFile.items["APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU"].ToUpper() != "TRUE")
					{
						Settings.Default.HankakuZenkakuFlag = false;
					}
					else
					{
						Settings.Default.HankakuZenkakuFlag = true;
					}
				}
			}
			catch
			{
			}
		}

		public void RecoverLocalMemos(string recoveryFile)
		{
			this.xDocLocalMemo = null;
			this.xDocLocalMemo = new XmlDocument();
			try
			{
				this.xDocLocalMemo.Load(recoveryFile);
				this.ReloadSamePage();
			}
			catch
			{
				MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM007) Failed to recover specified object", "(E-VWR-EM007)_FAILED_RECOVER", ResourceType.POPUP_MESSAGE));
			}
		}

		public XmlNodeList reloadGlobalMemos(string sPartNumber)
		{
			XmlNodeList xmlNodeLists = null;
			try
			{
				XmlDocument xmlDocument = this.xDocGlobalMemo;
				string[] pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@PartNo='", sPartNumber, "']" };
				xmlNodeLists = xmlDocument.SelectNodes(string.Concat(pBookId));
			}
			catch
			{
			}
			return xmlNodeLists;
		}

		private void ReloadSamePage()
		{
			this.p_ArgsO = new string[] { Program.iniServers[this.p_ServerId].sIniKey, this.p_BookId, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, this.objFrmPartlist.CurrentListIndex, this.objFrmPartlist.CurrentPartNumber };
			this.objFrmTreeview.TreeViewClearSelection();
			this.SelectTreeNode();
		}

		public void RemoveHighlightOnPicture()
		{
			this.objFrmPicture.RemoveHighlightOnPicture();
		}

		private void ResetBookMarkTooltipLanguage()
		{
			for (int i = 0; i < this.bookmarksToolStripMenuItem.DropDown.Items.Count; i++)
			{
				string toolTipText = this.bookmarksToolStripMenuItem.DropDown.Items[i].ToolTipText;
				if (toolTipText != null)
				{
					string[] strArrays = toolTipText.Split(new char[] { '\n' });
					string[] strArrays1 = new string[(int)strArrays.Length];
					for (int j = 0; j < (int)strArrays.Length; j++)
					{
						strArrays1[j] = strArrays[j].Substring(strArrays[j].IndexOf("=") + 1);
					}
					this.bookmarksToolStripMenuItem.DropDown.Items[i].ToolTipText = string.Empty;
					if ((int)strArrays.Length >= 1)
					{
						this.bookmarksToolStripMenuItem.DropDown.Items[i].ToolTipText = string.Concat(this.GetResource("Picture Index", "PICTURE_INDEX", ResourceType.TOOLSTRIP), " = ", strArrays1[0].ToString().Trim());
					}
					if ((int)strArrays.Length >= 2)
					{
						ToolStripItem item = this.bookmarksToolStripMenuItem.DropDown.Items[i];
						string str = item.ToolTipText;
						string[] resource = new string[] { str, "\n", this.GetResource("List Index", "LIST_INDEX", ResourceType.TOOLSTRIP), " = ", strArrays1[1].ToString().Trim() };
						item.ToolTipText = string.Concat(resource);
					}
					if ((int)strArrays.Length == 3)
					{
						ToolStripItem toolStripItem = this.bookmarksToolStripMenuItem.DropDown.Items[i];
						string toolTipText1 = toolStripItem.ToolTipText;
						string[] resource1 = new string[] { toolTipText1, "\n", this.GetResource("Part Number", "PART_NUMBER", ResourceType.TOOLSTRIP), " = ", strArrays1[2].ToString().Trim() };
						toolStripItem.ToolTipText = string.Concat(resource1);
					}
				}
			}
		}

		public void ResetHistory()
		{
			this.objHistory.ResetHistory();
			this.AddToHistory();
		}

		private void restoreDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.pnlForm2.BringToFront();
				if (File.Exists(Program.defaultsPath))
				{
					this.objDocPanel.SuspendLayout();
					DeserializeDockContent deserializeDockContent = new DeserializeDockContent(this.GetContentFromPersistString);
					this.objFrmSelectionlist.DockPanel = null;
					this.objFrmPartlist.DockPanel = null;
					this.objFrmTreeview.DockPanel = null;
					this.objFrmPicture.DockPanel = null;
					this.objFrmInfo.DockPanel = null;
					this.objDocPanel.LoadFromXml(Program.defaultsPath, deserializeDockContent);
					try
					{
						this.bPartsListClosed = this.objFrmPartlist.IsHidden;
					}
					catch
					{
					}
					if (this.sBookType.ToUpper() != "GSC")
					{
						this.selectionListToolStripMenuItem.Visible = true;
						if (this.objFrmPartlist.objXmlNodeList.Count == 0)
						{
							this.objFrmPartlist.lblPartsListInfo.Text = string.Empty;
							this.objFrmPartlist.dgPartslist.Visible = false;
							this.objFrmPartlist.picLoading.Visible = false;
						}
					}
					else
					{
						this.objFrmSelectionlist.Hide();
						this.selectionListToolStripMenuItem.Visible = false;
						this.objFrmPartlist.Hide();
						if (this.bObjFrmPartlistClosed)
						{
							this.objFrmPartlist.Hide();
						}
					}
				}
				else if (this.objFrmTreeview.DockPanel != null && this.objFrmPicture.DockPanel != null && this.objFrmSelectionlist.DockPanel != null && this.objFrmPartlist.DockPanel != null)
				{
					this.objDocPanel.DockBottomPortion = 0.25;
					this.objDocPanel.DockLeftPortion = 0.25;
					this.objDocPanel.DockRightPortion = 0.25;
					this.objDocPanel.DockTopPortion = 0.25;
					this.objFrmPartlist.DockState = DockState.DockBottom;
					this.objFrmTreeview.DockState = DockState.DockLeft;
					this.objFrmPicture.DockState = DockState.Document;
					this.bPartsListClosed = false;
					if (!this.tsbViewPartslist.Enabled)
					{
						this.objFrmPartlist.Hide();
					}
					this.contentsToolStripMenuItem.Checked = this.objFrmTreeview.Visible;
					this.pictureToolStripMenuItem.Checked = this.objFrmPicture.Visible;
					this.partslistToolStripMenuItem.Checked = this.objFrmPartlist.Visible;
					if (this.sBookType.ToUpper() == "GSP")
					{
						this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
						this.objFrmSelectionlist.DockState = DockState.DockBottom;
						if (this.objFrmPartlist.objXmlNodeList.Count == 0)
						{
							this.objFrmPartlist.lblPartsListInfo.Text = string.Empty;
							this.objFrmPartlist.dgPartslist.Visible = false;
							this.objFrmPartlist.picLoading.Visible = false;
						}
					}
					else if (this.sBookType.ToUpper() == "GSC" && this.bObjFrmPartlistClosed)
					{
						this.objFrmPartlist.Hide();
					}
					if (!this.tsbViewPicture.Enabled)
					{
						this.objFrmPicture.Hide();
					}
				}
				this.objDocPanel.ResumeLayout(true, true);
				this.pnlForm2.SendToBack();
			}
			catch
			{
			}
		}

		public void RunDataSizeChecking()
		{
			this.frmParent.RunDataSizeChecking();
		}

		private void saveDefaultsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.objFrmTreeview.DockPanel != null && this.objFrmPicture.DockPanel != null && this.objFrmSelectionlist.DockPanel != null && this.objFrmPartlist.DockPanel != null && this.objFrmInfo.DockPanel != null)
				{
					this.objDocPanel.SaveAsXml(Program.defaultsPath);
				}
			}
			catch
			{
			}
		}

		public static string SavefrmViewerSizeSettings(Form frmViewer)
		{
			string[] str = new string[] { frmViewer.Location.X.ToString(), "|", frmViewer.Location.Y.ToString(), "|", frmViewer.Size.Width.ToString(), "|", frmViewer.Size.Height.ToString(), "|", frmViewer.WindowState.ToString() };
			return string.Concat(str);
		}

		public void SaveMemos()
		{
			try
			{
				string empty = string.Empty;
				string globalMemoFolder = string.Empty;
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				empty = string.Concat(empty, "\\", Application.ProductName);
				empty = string.Concat(empty, "\\", Program.iniServers[this.p_ServerId].sIniKey);
				if (!Directory.Exists(empty))
				{
					Directory.CreateDirectory(empty);
				}
				globalMemoFolder = Settings.Default.GlobalMemoFolder;
				if (globalMemoFolder == string.Empty)
				{
					globalMemoFolder = empty;
				}
				if (!Directory.Exists(globalMemoFolder))
				{
					try
					{
						Directory.CreateDirectory(globalMemoFolder);
					}
					catch
					{
						globalMemoFolder = empty;
					}
				}
				empty = string.Concat(empty, "\\LocalMemo.xml");
				globalMemoFolder = string.Concat(globalMemoFolder, "\\GlobalMemo.xml");
				if (this.xDocGlobalMemo != null && Settings.Default.EnableGlobalMemo)
				{
					try
					{
						this.xDocGlobalMemo.Save(globalMemoFolder);
					}
					catch
					{
						MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM011) Failed to save specified object", "(E-VWR-EM011)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
					}
				}
				if (this.xDocLocalMemo != null && Settings.Default.EnableLocalMemo)
				{
					try
					{
						this.xDocLocalMemo.Save(empty);
						if (Settings.Default.EnableLocalMemo && Settings.Default.LocalMemoBackup)
						{
							if (!File.Exists(Settings.Default.LocalMemoBackupFile))
							{
								MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM012) Failed to save specified object", "(E-VWR-EM012)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
							}
							else
							{
								try
								{
									this.xDocLocalMemo.Save(Settings.Default.LocalMemoBackupFile);
								}
								catch
								{
									MessageHandler.ShowWarning(this.GetResource("(E-VWR-EM012) Failed to save specified object", "(E-VWR-EM012)_FAILED_SAVE", ResourceType.POPUP_MESSAGE));
								}
							}
						}
					}
					catch
					{
						MessageHandler.ShowWarning("Problems in saving local memos");
					}
				}
			}
			catch
			{
			}
		}

		public void ScalePicture(int x, int y, int width, int height)
		{
			this.objFrmPicture.ScalePicture((float)x, (float)y, width, height);
		}

		private int SearchSeries(string sFilePath, string sBookPublishingId)
		{
			int num;
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(sFilePath))
			{
				return 0;
			}
			if (!this.p_Compressed)
			{
				try
				{
					xmlDocument.Load(sFilePath);
				}
				catch
				{
					num = 0;
					return num;
				}
			}
			else
			{
				try
				{
					string str = sFilePath.ToLower().Replace(".zip", ".xml");
					Global.Unzip(sFilePath);
					if (File.Exists(str))
					{
						xmlDocument.Load(str);
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
					num = 0;
					return num;
				}
			}
			XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
			if (xmlNodes == null)
			{
				return 0;
			}
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
					empty1 = attribute.Name;
				}
			}
			if (empty == "" || name == "")
			{
				return 0;
			}
			this.p_BookId = sBookPublishingId;
			this.p_SchemaNode = xmlNodes;
			string[] upper = new string[] { "//Books/Book[translate(@", name, ", 'abcdefghijklmnopqrstuvwxyz', 'ABCDEFGHIJKLMNOPQRSTUVWXYZ')='", sBookPublishingId.ToUpper(), "']" };
			XmlNodeList xmlNodeLists = xmlDocument.SelectNodes(string.Concat(upper));
			if (xmlNodeLists.Count <= 0)
			{
				this.UpdateStatus(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.STATUS_MESSAGE));
				return 0;
			}
			xmlNodeLists = this.FilterBooksList(xmlNodes, xmlNodeLists);
			IEnumerator enumerator = xmlNodeLists.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlNode current = (XmlNode)enumerator.Current;
					string value = current.Attributes[name].Value;
					if (current.Attributes.Count <= 0)
					{
						continue;
					}
					this.p_BookNode = current;
					try
					{
						if (current.Attributes[empty1] == null)
						{
							this.UpdateStatus(this.GetResource("(E-OBB-EM008) Specified information does not exist", "(E-OBB-EM008)_NOINFO", ResourceType.POPUP_MESSAGE));
							num = 2;
							return num;
						}
						else
						{
							this.p_BookType = current.Attributes[empty1].Value;
						}
					}
					catch
					{
					}
					this.ShowHideSelectionList();
					num = 1;
					return num;
				}
				this.UpdateStatus(this.GetResource("(E-VWR-EM002) Invalid command", "(E-VWR-EM002)_INVALIDCOMMAND", ResourceType.STATUS_MESSAGE));
				return 0;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return num;
		}

		private void selectionListSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				frmConfig _frmConfig = new frmConfig(this)
				{
					Owner = this
				};
				this.ShowDimmer();
				_frmConfig.Show(ConfigTasks.SelectionListSettings);
			}
			catch (Exception exception)
			{
			}
		}

		private void selectionListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.selectionListToolStripMenuItem.Checked = !this.selectionListToolStripMenuItem.Checked;
			if (!this.selectionListToolStripMenuItem.Checked)
			{
				this.objFrmSelectionlist.Hide();
				return;
			}
			this.objFrmSelectionlist.Show(this.objDocPanel);
		}

		public void SelectTreeNode()
		{
			if (this.p_ArgsO != null && (int)this.p_ArgsO.Length > 2 && this.p_ArgsO[2] != "nil")
			{
				int num = 1;
				if (!int.TryParse(this.p_ArgsO[2], out num))
				{
					try
					{
						TreeNode treeNode = this.objFrmTreeview.FindTreeNodeByPageName(this.objFrmTreeview.tvBook.Nodes, this.p_ArgsO[2]);
						this.p_ArgsO[2] = "nil";
						if (treeNode == null)
						{
							this.objFrmPicture.HideLoading1();
							this.objFrmPicture.LoadBlankPage(string.Empty);
						}
						else if (this.objFrmTreeview.GetNodesCount() > 0)
						{
							this.objFrmTreeview.tvBook.SelectedNode = treeNode;
						}
					}
					catch
					{
					}
				}
				else if (this.objFrmTreeview.GetNodesCount() > 0)
				{
					this.objFrmTreeview.SelectTreeNode(this.p_ArgsO[2]);
				}
			}
			else if (this.objFrmTreeview.GetNodesCount() > 0)
			{
				this.objFrmTreeview.SelectTreeNode(string.Empty);
			}
			if ((!this.objFrmTreeview.IsActivated || this.objFrmTreeview.IsHidden) && this.objFrmTreeview.GetNodesCount() > 0)
			{
				this.objFrmTreeview.LoadPictureInTree();
			}
		}

		public void SelectTreeNodeByName(string pageName)
		{
			if (!pageName.Equals(string.Empty))
			{
				this.objFrmTreeview.SelectTreeNodeByPageName(pageName);
				return;
			}
			this.objFrmTreeview.SelectTreeNode(string.Empty);
		}

		public void SelListAddRemoveRow(int ServerId, XmlNode xSchemaNode, DataGridViewRow dr, bool bAddRow)
		{
			try
			{
				string empty = string.Empty;
				empty = string.Concat(Program.iniServers[this.p_ServerId].sIniKey, "**");
				empty = string.Concat(empty, this.p_BookId, "**");
				empty = string.Concat(empty, this.objFrmPicture.CurrentPageId, "**");
				empty = string.Concat(empty, this.objFrmPartlist.CurrentPicIndex, "**");
				empty = string.Concat(empty, this.objFrmPartlist.CurrentListIndex, "**");
				if (dr.Cells["PART_SLIST_KEY"] != null && dr.Cells["PART_SLIST_KEY"].Value.ToString() != string.Empty)
				{
					empty = string.Concat(empty, dr.Cells["PART_SLIST_KEY"].Value.ToString());
				}
				this.frmParent.SelListAddRemoveRow(ServerId, xSchemaNode, dr, bAddRow, empty);
			}
			catch
			{
			}
		}

		public void SelListInitialize()
		{
			try
			{
				this.objFrmSelectionlist.selListInitialize();
				this.objFrmSelectionlist.ReLoadPartlistColumns();
				this.objFrmSelectionlist.SynSelectionList();
			}
			catch
			{
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetActiveWindow(IntPtr hWnd);

		public void SetArguments(string[] args)
		{
			if (args == null || (int)args.Length <= 0)
			{
				this.p_ArgsO = null;
				this.p_ArgsF = null;
				this.p_ArgsS = null;
			}
			else
			{
				bool flag = false;
				string empty = string.Empty;
				string str = string.Empty;
				string empty1 = string.Empty;
				bool flag1 = false;
				bool flag2 = false;
				bool flag3 = false;
				for (int i = 0; i < (int)args.Length; i++)
				{
					if (args[i].Equals("-o"))
					{
						flag1 = true;
						flag2 = false;
						flag3 = false;
					}
					else if (args[i].Equals("-f"))
					{
						flag1 = false;
						flag2 = true;
						flag3 = false;
					}
					else if (args[i].Equals("-s"))
					{
						flag1 = false;
						flag2 = false;
						flag3 = true;
					}
					else if (!args[i].Equals("-p"))
					{
						if (flag1 && !args[i].Equals(" "))
						{
							empty = (!empty.Equals(string.Empty) ? string.Concat(empty, ":::", args[i].Replace(":", "$#$")) : args[i].Replace(":", "$#$"));
						}
						if (flag2 && !args[i].Equals(" ") && args[i].Contains("=") && args[i].Substring(0, args[i].IndexOf("=")).Trim() != string.Empty && args[i].Substring(args[i].IndexOf("=") + 1, args[i].Length - (args[i].IndexOf("=") + 1)).Trim() != string.Empty)
						{
							str = (!str.Equals(string.Empty) ? string.Concat(str, ":::", args[i]) : args[i]);
						}
						if (flag3 && !args[i].Equals(" ") && args[i].Contains("=") && args[i].Substring(0, args[i].IndexOf("=")).Trim() != string.Empty && args[i].Substring(args[i].IndexOf("=") + 1, args[i].Length - (args[i].IndexOf("=") + 1)).Trim() != string.Empty)
						{
							empty1 = (!empty1.Equals(string.Empty) ? string.Concat(empty1, ":::", args[i]) : args[i]);
							if (!flag && (empty1.Trim().ToUpper().Equals("OPT=OFF") || empty1.Trim().ToUpper().Contains(":::OPT=OFF") || empty1.Trim().ToUpper().Contains("OPT=OFF:::")))
							{
								flag = true;
							}
							if (!args[i].ToLower().Contains("fromportal=true"))
							{
								Program.objAppMode.bFromPortal = false;
							}
							else
							{
								Program.objAppMode.bFromPortal = true;
							}
						}
					}
					else
					{
						flag1 = false;
						flag2 = false;
						flag3 = false;
					}
				}
				string[] strArrays = new string[] { ":::" };
				this.p_ArgsO = empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				try
				{
					if (empty.ToUpper().Contains("VIEWERISUPDATED"))
					{
						for (int j = 0; j < (int)this.p_ArgsO.Length; j++)
						{
							if (this.p_ArgsO[j].Contains("VIEWERISUPDATED"))
							{
								this.p_ArgsO[j] = this.p_ArgsO[j].Replace("VIEWERISUPDATED", "1");
							}
						}
					}
				}
				catch
				{
				}
				if (empty.Contains("$#$"))
				{
					for (int k = 0; k < (int)this.p_ArgsO.Length; k++)
					{
						if (this.p_ArgsO[k].Contains("$#$"))
						{
							this.p_ArgsO[k] = this.p_ArgsO[k].Replace("$#$", ":");
						}
					}
				}
				string[] strArrays1 = new string[] { ":::" };
				this.p_ArgsS = empty1.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
				if (!flag)
				{
					string[] strArrays2 = new string[] { ":::" };
					this.p_ArgsF = str.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
					return;
				}
			}
		}

		public void SetCurrentServerID(int curServerId)
		{
			this.p_ServerId = curServerId;
		}

		public void setFocusedWindow(string strActivePane)
		{
			if (!this.objFrmPicture.IsHidden)
			{
				this.objFrmPicture.Activate();
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		private void SetText()
		{
			this.fileToolStripMenuItem.Text = this.GetResource("File", "FILE", ResourceType.MENU_BAR);
			this.openToolStripMenuItem.Text = this.GetResource("Open", "OPEN", ResourceType.MENU_BAR);
			this.printToolStripMenuItem.Text = this.GetResource("Print …", "PRINT", ResourceType.MENU_BAR);
			this.printPageToolStripMenuItem.Text = this.GetResource("Print Page …", "PRINT_PAGE", ResourceType.MENU_BAR);
			this.printPictureToolStripMenuItem.Text = this.GetResource("Print Picture …", "PRINT_PICTURE", ResourceType.MENU_BAR);
			this.printListToolStripMenuItem.Text = this.GetResource("Print Parts List …", "PRINT_PARTSLIST", ResourceType.MENU_BAR);
			this.printSelectionListToolStripMenuItem.Text = this.GetResource("Print SelectionList…", "PRINT_SELECTIONLIST", ResourceType.MENU_BAR);
			this.closeToolStripMenuItem.Text = this.GetResource("Close", "CLOSE", ResourceType.MENU_BAR);
			this.closeAllToolStripMenuItem.Text = this.GetResource("Close All", "CLOSE_ALL", ResourceType.MENU_BAR);
			this.viewToolStripMenuItem.Text = this.GetResource("View", "VIEW", ResourceType.MENU_BAR);
			this.restoreDefaultsToolStripMenuItem.Text = this.GetResource("Restore Defaults", "RESTORE_DEFAULTS", ResourceType.MENU_BAR);
			this.saveDefaultsToolStripMenuItem.Text = this.GetResource("Save Defaults", "SAVE_DEFAULTS", ResourceType.MENU_BAR);
			this.contentsToolStripMenuItem.Text = this.GetResource("Contents", "CONTENTS", ResourceType.MENU_BAR);
			this.pictureToolStripMenuItem.Text = this.GetResource("Picture", "PICTURE", ResourceType.MENU_BAR);
			this.partslistToolStripMenuItem.Text = this.GetResource("Parts List", "PARTS_LIST", ResourceType.MENU_BAR);
			this.selectionListToolStripMenuItem.Text = this.GetResource("Selection List", "SELECTION_LIST", ResourceType.MENU_BAR);
			this.informationToolStripMenuItem.Text = this.GetResource("Information", "INFORMATION", ResourceType.MENU_BAR);
			this.settingsToolStripMenuItem.Text = this.GetResource("Settings", "SETTINGS", ResourceType.MENU_BAR);
			this.fontToolStripMenuItem.Text = this.GetResource("Font …", "FONT", ResourceType.MENU_BAR);
			this.ColorToolStripMenuItem.Text = this.GetResource("Color …", "COLOR", ResourceType.MENU_BAR);
			this.generalToolStripMenuItem.Text = this.GetResource("General …", "GENERAL", ResourceType.MENU_BAR);
			this.connectionToolStripMenuItem.Text = this.GetResource("Connection Settings …", "CONNECTION_SETTINGS", ResourceType.MENU_BAR);
			this.memoSettingsToolStripMenuItem.Text = this.GetResource("Memos …", "MEMOS_SETTINGS", ResourceType.MENU_BAR);
			this.pageNameSearchSettingsToolStripMenuItem.Text = this.GetResource("Page Name …", "PAGE_NAME", ResourceType.MENU_BAR);
			this.partNameSearchSettingsToolStripMenuItem.Text = this.GetResource("Part Name …", "PART_NAME", ResourceType.MENU_BAR);
			this.partNumberSearchSettingsToolStripMenuItem.Text = this.GetResource("Page Number …", "PART_NUMBER", ResourceType.MENU_BAR);
			this.memoRecoveryToolStripMenuItem.Text = this.GetResource("Memo Recovery …", "MEMO_RECOVERY", ResourceType.MENU_BAR);
			this.bookmarksToolStripMenuItem.Text = this.GetResource("Bookmarks", "BOOKMARKS", ResourceType.MENU_BAR);
			this.addBookmarksToolStripMenuItem.Text = this.GetResource("Add Bookmarks", "ADD_BOOKMARKS", ResourceType.MENU_BAR);
			this.searchToolStripMenuItem.Text = this.GetResource("Search", "SEARCH", ResourceType.MENU_BAR);
			this.pageNameToolStripMenuItem.Text = this.GetResource("Page Name …", "PAGENAME_SETTINGS", ResourceType.MENU_BAR);
			this.partNameToolStripMenuItem.Text = this.GetResource("Part Name …", "PARTNAME_SETTINGS", ResourceType.MENU_BAR);
			this.partNumberToolStripMenuItem.Text = this.GetResource("Part Number …", "PARTNUMBER_SETTINGS", ResourceType.MENU_BAR);
			this.textSearchToolStripMenuItem.Text = this.GetResource("Text Search …", "TEXT_SEARCH", ResourceType.MENU_BAR);
			this.textSearceNameSearchSettingsToolStripMenuItem.Text = this.GetResource("Text Search …", "TEXTSEARCH_SETTINGS", ResourceType.MENU_BAR);
			this.singleBookToolStripMenuItem.Text = this.GetResource("Single Book Download …", "SINGLE_BOOK_DOWNLOAD", ResourceType.MENU_BAR);
			this.multipleBooksToolStripMenuItem.Text = this.GetResource("Multiple Books Download …", "MULTIPLE_BOOKS_DOWNLOAD", ResourceType.MENU_BAR);
			this.helpToolStripMenuItem.Text = this.GetResource("Help", "HELP", ResourceType.MENU_BAR);
			this.aboutGSPcLocalToolStripMenuItem.Text = this.GetResource("About GSPcLocal", "ABOUT_GSPCLOCAL", ResourceType.MENU_BAR);
			this.gSPcLocalHelpToolStripMenuItem.Text = this.GetResource("GSPcLocal Help", "GSPCLOCALHELP", ResourceType.MENU_BAR);
			this.manageDiskSpaceToolStripMenuItem.Text = this.GetResource("Manage Disk Space …", "MANAGE_DISK_SPACE", ResourceType.MENU_BAR);
			this.goToPortalToolStripMenuItem.Text = this.GetResource("Go To Portal …", "GO_TO_PORTAL", ResourceType.MENU_BAR);
			this.navigationToolStripMenuItem.Text = this.GetResource("Navigation", "NAVIGATION", ResourceType.MENU_BAR);
			this.firstPageToolStripMenuItem.Text = this.GetResource("First Page", "FIRST_PAGE", ResourceType.MENU_BAR);
			this.previousPageToolStripMenuItem.Text = this.GetResource("Previous Page", "PREVIOUS_PAGE", ResourceType.MENU_BAR);
			this.nextPageToolStripMenuItem.Text = this.GetResource("Next Page", "NEXT_PAGE", ResourceType.MENU_BAR);
			this.lastPageToolStripMenuItem.Text = this.GetResource("Last Page", "LAST_PAGE", ResourceType.MENU_BAR);
			this.previousViewToolStripMenuItem.Text = this.GetResource("Previous View", "PREVIOUS_VIEW", ResourceType.MENU_BAR);
			this.nextViewToolStripMenuItem.Text = this.GetResource("Next View", "NEXT_VIEW", ResourceType.MENU_BAR);
			this.memoDetailsToolStripMenuItem.Text = this.GetResource("Memos", "MEMOS", ResourceType.MENU_BAR);
			this.addPictureMemoToolStripMenuItem.Text = this.GetResource("Add Picture Memo …", "ADD_PICTURE_MEMO", ResourceType.MENU_BAR);
			this.addPartMemoToolStripMenuItem.Text = this.GetResource("Add Part Memo …", "ADD_PART_MEMO", ResourceType.MENU_BAR);
			this.viewMemoListToolStripMenuItem.Text = this.GetResource("View Memo List …", "VIEW_MEMO_LIST", ResourceType.MENU_BAR);
			this.advancedSearchToolStripMenuItem.Text = this.GetResource("Advanced Search …", "ADVANCED_SEARCH", ResourceType.MENU_BAR);
			this.partsListSettingsToolStripMenuItem.Text = this.GetResource("Parts List Settings …", "PARTSLIST_SETTINGS", ResourceType.MENU_BAR);
			this.selectionListSettingsToolStripMenuItem.Text = this.GetResource("Selection List Settings …", "SELECTIONLIST_SETTINGS", ResourceType.MENU_BAR);
			this.advanceSearchSettingsToolStripMenuItem.Text = this.GetResource("Advanced Search …", "ADVANCEDSEARCH_SETTINGS", ResourceType.MENU_BAR);
			this.toolsToolStripMenuItem.Text = this.GetResource("Tools", "TOOLS", ResourceType.MENU_BAR);
			this.addOnToolStripMenuItem.Text = this.GetResource("Add On", "ADD_ON", ResourceType.MENU_BAR);
			this.tsbPortal.Text = this.GetResource("Go To Portal …", "GO_TO_PORTAL", ResourceType.TOOLSTRIP);
			this.tsbHistoryBack.Text = this.GetResource("Backward", "BACKWARD", ResourceType.TOOLSTRIP);
			this.tsbHistoryForward.Text = this.GetResource("Forward", "FORWARD", ResourceType.TOOLSTRIP);
			this.tsbNavigateFirst.Text = this.GetResource("First Page", "FIRST_PAGE", ResourceType.TOOLSTRIP);
			this.tsbNavigatePrevious.Text = this.GetResource("Previous Page", "PREVIOUS_PAGE", ResourceType.TOOLSTRIP);
			this.tsbNavigateNext.Text = this.GetResource("Next Page", "NEXT_PAGE", ResourceType.TOOLSTRIP);
			this.tsbNavigateLast.Text = this.GetResource("Last Page", "LAST_PAGE", ResourceType.TOOLSTRIP);
			this.tsbOpenBook.Text = this.GetResource("Open New Book", "OPEN_NEW_BOOK", ResourceType.TOOLSTRIP);
			this.tsbPrint.Text = this.GetResource("Print", "PRINT", ResourceType.TOOLSTRIP);
			this.tsbAddBookmarks.Text = this.GetResource("Add Bookmarks", "ADD_BOOKMARKS", ResourceType.TOOLSTRIP);
			this.tsbMemoList.Text = this.GetResource("View Memo List", "VIEW_MEMO_LIST", ResourceType.TOOLSTRIP);
			this.tsbConfiguration.Text = this.GetResource("Configuration", "CONFIGURATION", ResourceType.TOOLSTRIP);
			this.tsbConnection.Text = this.GetResource("Connection Settings", "CONNECTION_SETTINGS", ResourceType.TOOLSTRIP);
			this.tsbAbout.Text = this.GetResource("About GSPcLocal", "ABOUT_GSPCLOCAL", ResourceType.TOOLSTRIP);
			this.tsbHelp.Text = this.GetResource("GSPcLocal Help", "GSPCLOCAL_HELP", ResourceType.TOOLSTRIP);
			this.tsbViewContents.Text = this.GetResource("View Contents", "VIEW_CONTENTS", ResourceType.TOOLSTRIP);
			this.tsbViewPicture.Text = this.GetResource("View Picture", "VIEW_PICTURE", ResourceType.TOOLSTRIP);
			this.tsbViewPartslist.Text = this.GetResource("View PartsList", "VIEW_PARTSLIST", ResourceType.TOOLSTRIP);
			this.tsbViewInfo.Text = this.GetResource("View Information", "VIEW_INFORMATION", ResourceType.TOOLSTRIP);
			this.tsbSearchPageName.Text = this.GetResource("Page Name Search", "PAGE_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchPartName.Text = this.GetResource("Part Name Search", "PART_NAME_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchPartNumber.Text = this.GetResource("Part Number Search", "PART_NUMBER_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbSearchPartAdvance.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.TOOLSTRIP);
			this.tsbThirdPartyBasket.Text = this.GetResource("Basket", "BASKET", ResourceType.TOOLSTRIP);
			this.tsbSearchText.Text = this.GetResource("Text Search", "TEXT_SEARCH", ResourceType.TOOLSTRIP);
			this.tslPic.Text = this.GetResource("Picture", "PICTURE", ResourceType.TOOLSTRIP);
			this.tsbPicPrev.Text = this.GetResource("Previous Picture", "PREVIOUS_PICTURE", ResourceType.TOOLSTRIP);
			this.tsbPicNext.Text = this.GetResource("Next Picture", "NEXT_PICTURE", ResourceType.TOOLSTRIP);
			this.tsbHistoryList.Text = this.GetResource("History List", "HISTORY_LIST", ResourceType.TOOLSTRIP);
			this.tsbDataCleanup.Text = this.GetResource("Manage Disk Space", "MANAGE_DISK_SPACE", ResourceType.TOOLSTRIP);
			this.tsbRestoreDefaults.Text = this.GetResource("Restore Defaults", "RESTORE_DEFAULTS", ResourceType.TOOLSTRIP);
			this.tsbMemoRecovery.Text = this.GetResource("Recover Memos", "RECOVER_MEMOS", ResourceType.TOOLSTRIP);
			this.tsbSingleBookDownload.Text = this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.TOOLSTRIP);
			this.tsbMultipleBooksDownload.Text = this.GetResource("Multiple Books Download", "MULTIPLE_BOOKS_DOWNLOAD", ResourceType.TOOLSTRIP);
			this.tsbAddPictureMemo.Text = this.GetResource("Add Picture Memo", "ADD_PICTURE_MEMO", ResourceType.TOOLSTRIP);
			this.tsbPicZoomIn.Text = this.GetResource("Zoom In", "ZOOM_IN", ResourceType.TOOLSTRIP);
			this.tsbPicZoomOut.Text = this.GetResource("Zoom Out", "ZOOM_OUT", ResourceType.TOOLSTRIP);
			this.tsbPicZoomSelect.Text = this.GetResource("Select Zoom", "SELECT_ZOOM", ResourceType.TOOLSTRIP);
			this.tsbPicCopy.Text = this.GetResource("Copy Image", "COPY_IMAGE", ResourceType.TOOLSTRIP);
			this.tsbFitPage.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.TOOLSTRIP);
			this.tsbPicPanMode.Text = this.GetResource("Pan Mode", "PAN_MODE", ResourceType.TOOLSTRIP);
			this.tsBRotateLeft.Text = this.GetResource("Rotate Left", "ROTATE_LEFT", ResourceType.TOOLSTRIP);
			this.tsBRotateRight.Text = this.GetResource("Rotate Right", "ROTATE_RIGHT", ResourceType.TOOLSTRIP);
			this.tsbPicSelectText.Text = this.GetResource("Select Text", "SELECT_TEXT", ResourceType.TOOLSTRIP);
			this.tsbThumbnail.Text = this.GetResource("Show Thumbnail", "SHOW_THUMBNAIL", ResourceType.TOOLSTRIP);
			this.tsbFindText.Text = this.GetResource("Find Text", "FIND_TEXT", ResourceType.TOOLSTRIP);
			this.lblStatus.Text = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
			this.workOffLineMenuItem.Text = this.GetResource("Work Offline", "WORK_OFFLINE", ResourceType.TOOLSTRIP);
			this.miniMapToolStripMenuItem.Text = this.GetResource("Mini Map", "MINI_MAP", ResourceType.MENU_BAR);
			this.UpdateViewerTitle();
		}

		public static void SetWindowSizeFromSettings(string thisWindowGeometry, Form formViewer)
		{
			try
			{
				if (!string.IsNullOrEmpty(thisWindowGeometry))
				{
					string[] strArrays = thisWindowGeometry.Split(new char[] { '|' });
					string str = strArrays[4];
					if (str == "Normal")
					{
						Point point = new Point(int.Parse(strArrays[0]), int.Parse(strArrays[1]));
						System.Drawing.Size size = new System.Drawing.Size(int.Parse(strArrays[2]), int.Parse(strArrays[3]));
						bool flag = frmViewer.IsBizarreLocation(point, size);
						bool flag1 = frmViewer.IsBizarreSize(size);
						if (flag && flag1)
						{
							formViewer.Location = point;
							formViewer.Size = size;
							formViewer.StartPosition = FormStartPosition.Manual;
							formViewer.WindowState = FormWindowState.Normal;
						}
						else if (flag1)
						{
							formViewer.Size = size;
						}
					}
					else if (str == "Maximized")
					{
						formViewer.Location = new Point(100, 100);
						formViewer.StartPosition = FormStartPosition.Manual;
						formViewer.WindowState = FormWindowState.Maximized;
					}
				}
			}
			catch
			{
			}
		}

		public void ShowAllMemos()
		{
			string[] pBookId;
			string[] strArrays;
			int n;
			this.LoadMemos();
			XmlNodeList xmlNodeLists = null;
			XmlNodeList xmlNodeLists1 = null;
			XmlNodeList xmlNodeLists2 = null;
			string empty = string.Empty;
			if (Settings.Default.EnableLocalMemo)
			{
				XmlDocument xmlDocument = this.xDocLocalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
				xmlNodeLists = xmlDocument.SelectNodes(string.Concat(pBookId));
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
					{
						SortedDictionary<DateTime, XmlNode> dateTimes = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes in xmlNodeLists)
						{
							DateTime dateTime = DateTime.ParseExact(xmlNodes.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes, dateTime, xmlNodes);
						}
						XmlDocument xmlDocument1 = new XmlDocument();
						XmlNode xmlNodes1 = xmlDocument1.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in dateTimes.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes2 = xmlDocument1.CreateElement("Memo");
								XmlNode value = keyValuePair.Value;
								for (int i = 0; i < value.Attributes.Count; i++)
								{
									XmlAttribute xmlAttribute = xmlDocument1.CreateAttribute(value.Attributes[i].Name);
									xmlAttribute.Value = value.Attributes[i].Value;
									xmlNodes2.Attributes.Append(xmlAttribute);
								}
								xmlNodes1.AppendChild(xmlNodes2);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> dateTime1 in dateTimes)
							{
								XmlNode xmlNodes3 = xmlDocument1.CreateElement("Memo");
								XmlNode value1 = dateTime1.Value;
								for (int j = 0; j < value1.Attributes.Count; j++)
								{
									XmlAttribute xmlAttribute1 = xmlDocument1.CreateAttribute(value1.Attributes[j].Name);
									xmlAttribute1.Value = value1.Attributes[j].Value;
									xmlNodes3.Attributes.Append(xmlAttribute1);
								}
								xmlNodes1.AppendChild(xmlNodes3);
							}
						}
						xmlDocument1.AppendChild(xmlNodes1);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists = xmlDocument1.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception)
				{
					XmlDocument xmlDocument2 = this.xDocLocalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists = xmlDocument2.SelectNodes(string.Concat(pBookId));
				}
			}
			if (Settings.Default.EnableGlobalMemo)
			{
				XmlDocument xmlDocument3 = this.xDocGlobalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
				xmlNodeLists1 = xmlDocument3.SelectNodes(string.Concat(pBookId));
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
					{
						SortedDictionary<DateTime, XmlNode> dateTimes1 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes4 in xmlNodeLists1)
						{
							DateTime dateTime2 = DateTime.ParseExact(xmlNodes4.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes1, dateTime2, xmlNodes4);
						}
						XmlDocument xmlDocument4 = new XmlDocument();
						XmlNode xmlNodes5 = xmlDocument4.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair1 in dateTimes1.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes6 = xmlDocument4.CreateElement("Memo");
								XmlNode value2 = keyValuePair1.Value;
								for (int k = 0; k < value2.Attributes.Count; k++)
								{
									XmlAttribute xmlAttribute2 = xmlDocument4.CreateAttribute(value2.Attributes[k].Name);
									xmlAttribute2.Value = value2.Attributes[k].Value;
									xmlNodes6.Attributes.Append(xmlAttribute2);
								}
								xmlNodes5.AppendChild(xmlNodes6);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair2 in dateTimes1)
							{
								XmlNode xmlNodes7 = xmlDocument4.CreateElement("Memo");
								XmlNode value3 = keyValuePair2.Value;
								for (int l = 0; l < value3.Attributes.Count; l++)
								{
									XmlAttribute xmlAttribute3 = xmlDocument4.CreateAttribute(value3.Attributes[l].Name);
									xmlAttribute3.Value = value3.Attributes[l].Value;
									xmlNodes7.Attributes.Append(xmlAttribute3);
								}
								xmlNodes5.AppendChild(xmlNodes7);
							}
						}
						xmlDocument4.AppendChild(xmlNodes5);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists1 = xmlDocument4.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception1)
				{
					XmlDocument xmlDocument5 = this.xDocGlobalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists1 = xmlDocument5.SelectNodes(string.Concat(pBookId));
				}
			}
			if (Settings.Default.EnableAdminMemo)
			{
				string item = string.Empty;
				try
				{
					item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
					item = string.Concat(item, "\\", Program.iniServers[this.p_ServerId].sIniKey);
					item = string.Concat(item, "\\", this.p_BookId);
					if (!Directory.Exists(item))
					{
						Directory.CreateDirectory(item);
					}
					empty = item;
					item = string.Concat(item, "\\", this.p_BookId, ".xml");
				}
				catch
				{
				}
				XmlDocument xmlDocument6 = new XmlDocument();
				XmlDocument xmlDocument7 = new XmlDocument();
				xmlDocument7.Load(item);
				if (Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
				{
					try
					{
						string str = string.Empty;
						str = (new AES()).Decode(xmlDocument7.InnerText, "0123456789ABCDEF");
						xmlDocument7.DocumentElement.InnerXml = str;
					}
					catch
					{
					}
				}
				if (Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"] != null)
				{
					XmlNode pageSchemaNode = this.objFrmTreeview.PageSchemaNode;
					XmlNodeList xmlNodeLists3 = xmlDocument7.SelectNodes("//Pic");
					string item1 = Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"];
					pBookId = new string[] { "," };
					string[] strArrays1 = item1.Split(pBookId, StringSplitOptions.None);
					ArrayList arrayLists = new ArrayList();
					for (int m = 0; m < (int)strArrays1.Length; m++)
					{
						foreach (XmlAttribute attribute in pageSchemaNode.Attributes)
						{
							if (attribute.Value.ToUpper() != strArrays1[m].ToUpper())
							{
								continue;
							}
							arrayLists.Add(attribute.Name);
							break;
						}
					}
					xmlDocument6.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
					foreach (XmlNode xmlNodes8 in xmlNodeLists3)
					{
						string empty1 = string.Empty;
						foreach (string arrayList in arrayLists)
						{
							if (xmlNodes8.Attributes[arrayList] == null || !(xmlNodes8.Attributes[arrayList].Value.Trim() != string.Empty))
							{
								continue;
							}
							empty1 = string.Concat(empty1, xmlNodes8.Attributes[arrayList].Value, "**");
						}
						pBookId = new string[] { "**" };
						string[] strArrays2 = empty1.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
						if ((int)strArrays2.Length <= 0)
						{
							continue;
						}
						strArrays = strArrays2;
						for (n = 0; n < (int)strArrays.Length; n++)
						{
							string str1 = strArrays[n];
							if (str1.Contains("||"))
							{
								pBookId = new string[] { "||" };
								string[] strArrays3 = str1.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
								string str2 = strArrays3[0];
								str2 = (str2.ToUpper().Equals("TXT") || str2.ToUpper().Equals("REF") || str2.ToUpper().Equals("HYP") || str2.ToUpper().Equals("PRG") ? str2.ToLower() : "Unknown");
								string str3 = strArrays3[1];
								string name = string.Empty;
								foreach (XmlAttribute attribute1 in pageSchemaNode.Attributes)
								{
									if (!attribute1.Value.ToUpper().Equals("UPDATEDATE"))
									{
										continue;
									}
									name = attribute1.Name;
									break;
								}
								string empty2 = string.Empty;
								if (xmlNodes8.Attributes[name] != null)
								{
									empty2 = xmlNodes8.Attributes[name].Value;
								}
								xmlDocument6.DocumentElement.AppendChild(xmlDocument6.ImportNode(this.CreateAdminMemoNode(str2, str3, string.Empty, empty2), true));
							}
						}
					}
				}
				XmlNode pageSchemaNode1 = this.objFrmTreeview.PageSchemaNode;
				XmlNodeList xmlNodeLists4 = null;
				Hashtable hashtables = new Hashtable();
				ArrayList arrayLists1 = new ArrayList();
				string name1 = string.Empty;
				foreach (XmlAttribute attribute2 in pageSchemaNode1.Attributes)
				{
					if (!attribute2.Value.ToUpper().Equals("PARTSLISTFILE"))
					{
						continue;
					}
					name1 = attribute2.Name;
					break;
				}
				if (name1 != string.Empty)
				{
					xmlNodeLists4 = xmlDocument7.SelectNodes(string.Concat("//@", name1));
					foreach (XmlNode xmlNodes9 in xmlNodeLists4)
					{
						if (hashtables.ContainsKey(xmlNodes9.Value))
						{
							continue;
						}
						hashtables.Add(xmlNodes9.Value, xmlNodes9.Value);
						arrayLists1.Add(xmlNodes9.Value);
					}
					for (int o = 0; o < arrayLists1.Count; o++)
					{
						string str4 = string.Concat(empty, "\\", arrayLists1[o].ToString(), ".xml");
						XmlDocument xmlDocument8 = new XmlDocument();
						if (File.Exists(str4))
						{
							xmlDocument8.Load(str4);
							if (Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
							{
								try
								{
									string empty3 = string.Empty;
									empty3 = (new AES()).Decode(xmlDocument8.InnerText, "0123456789ABCDEF");
									xmlDocument8.DocumentElement.InnerXml = empty3;
								}
								catch
								{
								}
							}
							XmlNode curListSchema = this.objFrmPartlist.GetCurListSchema();
							XmlNodeList xmlNodeLists5 = xmlDocument8.SelectNodes("//Part");
							curListSchema = xmlDocument8.SelectSingleNode("//Schema");
							if (curListSchema != null)
							{
								try
								{
									string item2 = Program.iniServers[this.ServerId].items["PLIST_SETTINGS", "MEM"];
									pBookId = new string[] { "|" };
									string[] strArrays4 = item2.Split(pBookId, StringSplitOptions.None);
									ArrayList arrayLists2 = new ArrayList();
									for (int p = 0; p < (int)strArrays4.Length; p++)
									{
										foreach (XmlAttribute attribute3 in curListSchema.Attributes)
										{
											if (attribute3.Value.ToUpper() != strArrays4[p].ToUpper())
											{
												continue;
											}
											arrayLists2.Add(attribute3.Name);
											break;
										}
									}
									foreach (XmlNode xmlNodes10 in xmlNodeLists5)
									{
										string empty4 = string.Empty;
										foreach (string arrayList1 in arrayLists2)
										{
											if (xmlNodes10.Attributes[arrayList1] == null || !(xmlNodes10.Attributes[arrayList1].Value.Trim() != string.Empty))
											{
												continue;
											}
											empty4 = string.Concat(empty4, xmlNodes10.Attributes[arrayList1].Value, "**");
										}
										pBookId = new string[] { "**" };
										string[] strArrays5 = empty4.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
										if ((int)strArrays5.Length <= 0)
										{
											continue;
										}
										strArrays = strArrays5;
										for (n = 0; n < (int)strArrays.Length; n++)
										{
											string str5 = strArrays[n];
											if (str5.Contains("||"))
											{
												pBookId = new string[] { "||" };
												string[] strArrays6 = str5.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
												string str6 = strArrays6[0];
												str6 = (str6.ToUpper().Equals("TXT") || str6.ToUpper().Equals("REF") || str6.ToUpper().Equals("HYP") || str6.ToUpper().Equals("PRG") ? str6.ToLower() : "Unknown");
												string str7 = strArrays6[1];
												string name2 = string.Empty;
												string name3 = string.Empty;
												foreach (XmlAttribute attribute4 in curListSchema.Attributes)
												{
													if (attribute4.Value.ToUpper().Equals("UPDATEDATE"))
													{
														name2 = attribute4.Name;
													}
													if (!attribute4.Value.ToUpper().Equals("PARTNUMBER"))
													{
														continue;
													}
													name3 = attribute4.Name;
												}
												string value4 = string.Empty;
												string empty5 = string.Empty;
												if (xmlNodes10.Attributes[name2] == null)
												{
													value4 = "Unknown";
													if (xmlNodes10.Attributes[name3] != null)
													{
														empty5 = xmlNodes10.Attributes[name3].Value;
														xmlDocument6.DocumentElement.AppendChild(xmlDocument6.ImportNode(this.CreateAdminMemoNode(str6, str7, empty5, value4), true));
													}
												}
												else
												{
													value4 = xmlNodes10.Attributes[name2].Value;
													if (xmlNodes10.Attributes[name3] != null)
													{
														empty5 = xmlNodes10.Attributes[name3].Value;
														xmlDocument6.DocumentElement.AppendChild(xmlDocument6.ImportNode(this.CreateAdminMemoNode(str6, str7, empty5, value4), true));
													}
												}
											}
										}
									}
								}
								catch (Exception exception2)
								{
								}
							}
						}
					}
				}
				xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
					{
						SortedDictionary<DateTime, XmlNode> dateTimes2 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes11 in xmlNodeLists2)
						{
							DateTime dateTime3 = DateTime.ParseExact(xmlNodes11.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes2, dateTime3, xmlNodes11);
						}
						XmlDocument xmlDocument9 = new XmlDocument();
						XmlNode xmlNodes12 = xmlDocument9.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair3 in dateTimes2.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes13 = xmlDocument9.CreateElement("Memo");
								XmlNode value5 = keyValuePair3.Value;
								for (int q = 0; q < value5.Attributes.Count; q++)
								{
									XmlAttribute xmlAttribute4 = xmlDocument9.CreateAttribute(value5.Attributes[q].Name);
									xmlAttribute4.Value = value5.Attributes[q].Value;
									xmlNodes13.Attributes.Append(xmlAttribute4);
								}
								xmlNodes12.AppendChild(xmlNodes13);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> dateTime4 in dateTimes2)
							{
								XmlNode xmlNodes14 = xmlDocument9.CreateElement("Memo");
								XmlNode value6 = dateTime4.Value;
								for (int r = 0; r < value6.Attributes.Count; r++)
								{
									XmlAttribute xmlAttribute5 = xmlDocument9.CreateAttribute(value6.Attributes[r].Name);
									xmlAttribute5.Value = value6.Attributes[r].Value;
									xmlNodes14.Attributes.Append(xmlAttribute5);
								}
								xmlNodes12.AppendChild(xmlNodes14);
							}
						}
						xmlDocument9.AppendChild(xmlNodes12);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists2 = xmlDocument9.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception3)
				{
					xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				}
			}
			frmMemoList _frmMemoList = new frmMemoList(this, xmlNodeLists, xmlNodeLists1, xmlNodeLists2, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, string.Empty, string.Empty)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmMemoList.Show();
		}

		public void ShowDimmer()
		{
			try
			{
				Control parent = this.toolStripContainer1;
				Rectangle bounds = parent.Bounds;
				try
				{
					parent.Focus();
				}
				catch
				{
				}
				while (parent.Parent.GetType() != typeof(frmViewer))
				{
					parent = parent.Parent;
					bounds.X = bounds.X + parent.Left;
					bounds.Y = bounds.Y + parent.Top;
				}
				parent = parent.Parent;
				bounds.Intersect(parent.ClientRectangle);
				this.objDimmer.Location = parent.PointToScreen(new Point(bounds.Left, bounds.Top));
				this.objDimmer.Height = bounds.Height;
				this.objDimmer.Width = bounds.Width;
				this.objDimmer.Show(this);
				this.objDimmer.Height = bounds.Height;
				this.objDimmer.Enabled = false;
			}
			catch
			{
			}
		}

		private void ShowForms()
		{
			if (this.objFrmTreeview.IsDisposed && this.objFrmPicture.IsDisposed && this.objFrmSelectionlist.IsDisposed && this.objFrmPartlist.IsDisposed && this.objFrmInfo.IsDisposed)
			{
				this.CreateForms();
			}
			this.EnableMenuAndToolbar(true);
			this.pnlForm2.BringToFront();
			bool flag = false;
			if (!File.Exists(Program.configPath))
			{
				flag = true;
			}
			else
			{
				try
				{
					DeserializeDockContent deserializeDockContent = new DeserializeDockContent(this.GetContentFromPersistString);
					this.objDocPanel.LoadFromXml(Program.configPath, deserializeDockContent);
					try
					{
						this.bPartsListClosed = this.objFrmPartlist.IsHidden;
					}
					catch
					{
					}
				}
				catch
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.objFrmTreeview.Show(this.objDocPanel);
				this.objFrmPicture.Show(this.objDocPanel);
				this.objFrmSelectionlist.Show(this.objDocPanel);
				this.objFrmPartlist.Show(this.objDocPanel);
				this.objFrmInfo.Show(this.objDocPanel);
				this.objFrmPartlist.DockState = DockState.DockBottom;
				this.objFrmTreeview.DockState = DockState.DockLeft;
				this.objFrmInfo.DockState = DockState.DockRightAutoHide;
				this.objFrmSelectionlist.DockState = DockState.DockRightAutoHide;
			}
			this.objFrmPartlist.Hide();
			this.ShowHideSelectionList();
			this.pnlForm2.SendToBack();
			this.contentsToolStripMenuItem.Checked = this.objFrmTreeview.Visible;
			this.pictureToolStripMenuItem.Checked = this.objFrmPicture.Visible;
			this.informationToolStripMenuItem.Checked = this.objFrmInfo.Visible;
			this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
		}

		public void ShowHidePartslistToolbar()
		{
			if (!this.objFrmPartlist.IsDisposed)
			{
				this.objFrmPartlist.ShowHidePartslistToolbar();
			}
		}

		public void ShowHidePictureToolbar()
		{
			if (!this.objFrmPicture.IsDisposed)
			{
				this.tsPic.Visible = (Settings.Default.ShowPicToolbar ? false : !Program.objAppFeatures.bDjVuToolbar);
				this.objFrmPicture.ShowHidePictureToolbar();
			}
		}

		public void ShowHideSelectionList()
		{
			try
			{
				string empty = string.Empty;
				foreach (XmlAttribute attribute in this.p_SchemaNode.Attributes)
				{
					if (!attribute.Value.ToUpper().Equals("BOOKTYPE"))
					{
						continue;
					}
					empty = attribute.Name;
				}
				if (empty != string.Empty && this.p_SchemaNode.Attributes[empty] != null && this.p_SchemaNode.Attributes[empty].Value != string.Empty)
				{
					string value = string.Empty;
					value = this.p_BookNode.Attributes[empty].Value;
					this.frmParent.SetBookType(value);
				}
				if (this.sBookType.ToUpper() != "GSP")
				{
					this.selectionListToolStripMenuItem.Visible = false;
					this.HideSelectionList();
					this.GSCToolBarItems();
					this.GSCMenuItems();
				}
				else
				{
					this.selectionListToolStripMenuItem.Checked = this.objFrmSelectionlist.Visible;
					if (this.objFrmSelectionlist.Visible)
					{
						this.selectionListToolStripMenuItem.Visible = true;
						this.ShowSelectionList();
					}
				}
				IniFileIO iniFileIO = new IniFileIO();
				ArrayList arrayLists = new ArrayList();
				if (iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.ServerId].sIniKey, ".ini"), "PLIST_SEARCH").Count == 0 || !Program.objAppFeatures.bPartAdvanceSearch)
				{
					this.tsbSearchPartAdvance.Visible = false;
					this.advancedSearchToolStripMenuItem.Visible = false;
					this.toolStripSeparator6.Visible = false;
				}
				if (!this.tsbSearchPageName.Visible && !this.tsbSearchPartName.Visible && !this.tsbSearchPartNumber.Visible && !this.tsbSearchPartAdvance.Visible && !this.tsbSearchText.Visible)
				{
					this.tsSearch.Visible = false;
					this.searchToolStripMenuItem.Visible = false;
				}
			}
			catch
			{
			}
		}

		public void ShowNotification()
		{
			string empty = string.Empty;
			string str = string.Empty;
			string resource = string.Empty;
			empty = string.Concat("Viewer 3.0 - ", this.GetResource("Low Disk Space", "LOW_DISK", ResourceType.POPUP_MESSAGE));
			str = string.Concat(this.GetResource("Less Then", "LESS_THEN", ResourceType.POPUP_MESSAGE), " ");
			str = string.Concat(str, DataSize.FormattedSize((long)10485760), " ");
			str = string.Concat(str, this.GetResource("Left From", "LEFT_FROM", ResourceType.POPUP_MESSAGE), " ");
			str = string.Concat(str, DataSize.FormattedSize(DataSize.spaceAllowed), " ");
			str = string.Concat(str, this.GetResource("of allowed space", "OF_ALLOWED", ResourceType.POPUP_MESSAGE), "\n\n");
			str = string.Concat(str, this.GetResource("Manage disk to free some space", "MANAGE_FREE_SPACE", ResourceType.POPUP_MESSAGE));
			resource = this.GetResource("GSPcLocal Viewer 3.0", "GSPcLocal Viewer 3.0", ResourceType.TITLE);
			this.frmParent.ShowNotification(empty, str, resource);
		}

		public void ShowPartMemos(string partNumber, string sAdminMemoValues, string sUpdateDate, string sScope)
		{
			string[] pBookId;
			XmlNodeList xmlNodeLists = null;
			XmlNodeList xmlNodeLists1 = null;
			XmlNodeList xmlNodeLists2 = null;
			if (Settings.Default.EnableGlobalMemo)
			{
				XmlDocument xmlDocument = this.xDocGlobalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@ListIndex='", this.objFrmPartlist.CurrentListIndex, "'][@PartNo='", partNumber, "']" };
				xmlNodeLists = xmlDocument.SelectNodes(string.Concat(pBookId));
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN" && xmlNodeLists.Count > 0)
					{
						SortedDictionary<DateTime, XmlNode> dateTimes = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes in xmlNodeLists)
						{
							DateTime dateTime = DateTime.ParseExact(xmlNodes.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes, dateTime, xmlNodes);
						}
						XmlDocument xmlDocument1 = new XmlDocument();
						XmlNode xmlNodes1 = xmlDocument1.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in dateTimes.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes2 = xmlDocument1.CreateElement("Memo");
								XmlNode value = keyValuePair.Value;
								for (int i = 0; i < value.Attributes.Count; i++)
								{
									XmlAttribute xmlAttribute = xmlDocument1.CreateAttribute(value.Attributes[i].Name);
									xmlAttribute.Value = value.Attributes[i].Value;
									xmlNodes2.Attributes.Append(xmlAttribute);
								}
								xmlNodes1.AppendChild(xmlNodes2);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> dateTime1 in dateTimes)
							{
								XmlNode xmlNodes3 = xmlDocument1.CreateElement("Memo");
								XmlNode value1 = dateTime1.Value;
								for (int j = 0; j < value1.Attributes.Count; j++)
								{
									XmlAttribute xmlAttribute1 = xmlDocument1.CreateAttribute(value1.Attributes[j].Name);
									xmlAttribute1.Value = value1.Attributes[j].Value;
									xmlNodes3.Attributes.Append(xmlAttribute1);
								}
								xmlNodes1.AppendChild(xmlNodes3);
							}
						}
						xmlDocument1.AppendChild(xmlNodes1);
						string[] strArrays = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists = xmlDocument1.SelectNodes(string.Concat(strArrays));
					}
				}
				catch (Exception exception)
				{
					XmlDocument xmlDocument2 = this.xDocGlobalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists = xmlDocument2.SelectNodes(string.Concat(pBookId));
				}
			}
			if (Settings.Default.EnableLocalMemo)
			{
				XmlDocument xmlDocument3 = this.xDocLocalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@ListIndex='", this.objFrmPartlist.CurrentListIndex, "'][@PartNo='", partNumber, "']" };
				xmlNodeLists1 = xmlDocument3.SelectNodes(string.Concat(pBookId));
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN" && xmlNodeLists1.Count > 0)
					{
						SortedDictionary<DateTime, XmlNode> dateTimes1 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes4 in xmlNodeLists1)
						{
							DateTime dateTime2 = DateTime.ParseExact(xmlNodes4.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes1, dateTime2, xmlNodes4);
						}
						XmlDocument xmlDocument4 = new XmlDocument();
						XmlNode xmlNodes5 = xmlDocument4.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair1 in dateTimes1.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes6 = xmlDocument4.CreateElement("Memo");
								XmlNode value2 = keyValuePair1.Value;
								for (int k = 0; k < value2.Attributes.Count; k++)
								{
									XmlAttribute xmlAttribute2 = xmlDocument4.CreateAttribute(value2.Attributes[k].Name);
									xmlAttribute2.Value = value2.Attributes[k].Value;
									xmlNodes6.Attributes.Append(xmlAttribute2);
								}
								xmlNodes5.AppendChild(xmlNodes6);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair2 in dateTimes1)
							{
								XmlNode xmlNodes7 = xmlDocument4.CreateElement("Memo");
								XmlNode value3 = keyValuePair2.Value;
								for (int l = 0; l < value3.Attributes.Count; l++)
								{
									XmlAttribute xmlAttribute3 = xmlDocument4.CreateAttribute(value3.Attributes[l].Name);
									xmlAttribute3.Value = value3.Attributes[l].Value;
									xmlNodes7.Attributes.Append(xmlAttribute3);
								}
								xmlNodes5.AppendChild(xmlNodes7);
							}
						}
						xmlDocument4.AppendChild(xmlNodes5);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists1 = xmlDocument4.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception1)
				{
					XmlDocument xmlDocument5 = this.xDocLocalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists1 = xmlDocument5.SelectNodes(string.Concat(pBookId));
				}
			}
			if (Settings.Default.EnableAdminMemo)
			{
				XmlDocument xmlDocument6 = new XmlDocument();
				xmlDocument6.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
				pBookId = new string[] { "**" };
				string[] strArrays1 = sAdminMemoValues.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
				if ((int)strArrays1.Length > 0)
				{
					string[] strArrays2 = strArrays1;
					for (int m = 0; m < (int)strArrays2.Length; m++)
					{
						string str = strArrays2[m];
						if (str.Contains("||"))
						{
							pBookId = new string[] { "||" };
							string[] strArrays3 = str.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
							string str1 = strArrays3[0];
							str1 = (str1.ToUpper().Equals("TXT") || str1.ToUpper().Equals("REF") || str1.ToUpper().Equals("HYP") || str1.ToUpper().Equals("PRG") ? str1.ToLower() : "Unknown");
							string str2 = strArrays3[1];
							xmlDocument6.DocumentElement.AppendChild(xmlDocument6.ImportNode(this.CreateAdminMemoNode(str1, str2, string.Empty, sUpdateDate), true));
						}
					}
				}
				xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN" && xmlNodeLists2.Count > 0)
					{
						SortedDictionary<DateTime, XmlNode> dateTimes2 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes8 in xmlNodeLists2)
						{
							DateTime dateTime3 = DateTime.ParseExact(xmlNodes8.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes2, dateTime3, xmlNodes8);
						}
						XmlDocument xmlDocument7 = new XmlDocument();
						XmlNode xmlNodes9 = xmlDocument7.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair3 in dateTimes2.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes10 = xmlDocument7.CreateElement("Memo");
								XmlNode value4 = keyValuePair3.Value;
								for (int n = 0; n < value4.Attributes.Count; n++)
								{
									XmlAttribute xmlAttribute4 = xmlDocument7.CreateAttribute(value4.Attributes[n].Name);
									xmlAttribute4.Value = value4.Attributes[n].Value;
									xmlNodes10.Attributes.Append(xmlAttribute4);
								}
								xmlNodes9.AppendChild(xmlNodes10);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> dateTime4 in dateTimes2)
							{
								XmlNode xmlNodes11 = xmlDocument7.CreateElement("Memo");
								XmlNode value5 = dateTime4.Value;
								for (int o = 0; o < value5.Attributes.Count; o++)
								{
									XmlAttribute xmlAttribute5 = xmlDocument7.CreateAttribute(value5.Attributes[o].Name);
									xmlAttribute5.Value = value5.Attributes[o].Value;
									xmlNodes11.Attributes.Append(xmlAttribute5);
								}
								xmlNodes9.AppendChild(xmlNodes11);
							}
						}
						xmlDocument7.AppendChild(xmlNodes9);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists2 = xmlDocument7.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception2)
				{
					xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				}
			}
			if (!Settings.Default.EnableLocalMemo && !Settings.Default.EnableGlobalMemo)
			{
				MessageHandler.ShowInformation(this.GetResource("All memos are disabled. Enable memo from settings screen", "MEMO_DISABLED", ResourceType.POPUP_MESSAGE));
				return;
			}
			frmMemo _frmMemo = new frmMemo(this, xmlNodeLists1, xmlNodeLists, xmlNodeLists2, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, this.objFrmPartlist.CurrentListIndex, partNumber, sScope)
			{
				Owner = this
			};
			this.bFromPopup = false;
			this.ShowDimmer();
			_frmMemo.Show();
		}

		public void ShowPartsList()
		{
			if (this.objFrmPartlist.InvokeRequired)
			{
				this.objFrmPartlist.Invoke(new frmViewer.ShowPartsListDelegate(this.ShowPartsList));
				return;
			}
			try
			{
				string focusedWindow = this.GetFocusedWindow();
				this.EnablePartslistShowHideButton(true);
				if (!this.objFrmPartlist.Visible)
				{
					this.objFrmPartlist.Show(this.objDocPanel);
					this.objFrmPartlist.ShowPartsList();
				}
				this.setFocusedWindow(focusedWindow);
			}
			catch
			{
			}
		}

		public void ShowPicture()
		{
			if (this.objFrmPicture.InvokeRequired)
			{
				this.objFrmPicture.Invoke(new frmViewer.ShowPartsListDelegate(this.ShowPicture));
				return;
			}
			try
			{
				this.EnablePictureShowHideButton(true);
				if (!this.bImageClosed && !this.bPictureClosed)
				{
					string focusedWindow = this.GetFocusedWindow();
					if (!this.objFrmPicture.Visible)
					{
						this.objFrmPicture.Show(this.objDocPanel);
					}
					this.miniMapToolStripMenuItem.Enabled = true;
					if (frmViewer.MiniMapChk)
					{
						this.objFrmPicture.ShowHideMiniMap(true);
					}
					this.setFocusedWindow(focusedWindow);
				}
			}
			catch
			{
			}
		}

		public void ShowPictureMemos(bool popup)
		{
			string[] pBookId;
			XmlNodeList xmlNodeLists = null;
			XmlNodeList xmlNodeLists1 = null;
			XmlNodeList xmlNodeLists2 = null;
			if (Settings.Default.EnableLocalMemo)
			{
				XmlDocument xmlDocument = this.xDocLocalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@PartNo='']" };
				xmlNodeLists = xmlDocument.SelectNodes(string.Concat(pBookId));
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
					{
						SortedDictionary<DateTime, XmlNode> dateTimes = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes in xmlNodeLists)
						{
							DateTime dateTime = DateTime.ParseExact(xmlNodes.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes, dateTime, xmlNodes);
						}
						XmlDocument xmlDocument1 = new XmlDocument();
						XmlNode xmlNodes1 = xmlDocument1.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in dateTimes.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes2 = xmlDocument1.CreateElement("Memo");
								XmlNode value = keyValuePair.Value;
								for (int i = 0; i < value.Attributes.Count; i++)
								{
									XmlAttribute xmlAttribute = xmlDocument1.CreateAttribute(value.Attributes[i].Name);
									xmlAttribute.Value = value.Attributes[i].Value;
									xmlNodes2.Attributes.Append(xmlAttribute);
								}
								xmlNodes1.AppendChild(xmlNodes2);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> dateTime1 in dateTimes)
							{
								XmlNode xmlNodes3 = xmlDocument1.CreateElement("Memo");
								XmlNode value1 = dateTime1.Value;
								for (int j = 0; j < value1.Attributes.Count; j++)
								{
									XmlAttribute xmlAttribute1 = xmlDocument1.CreateAttribute(value1.Attributes[j].Name);
									xmlAttribute1.Value = value1.Attributes[j].Value;
									xmlNodes3.Attributes.Append(xmlAttribute1);
								}
								xmlNodes1.AppendChild(xmlNodes3);
							}
						}
						xmlDocument1.AppendChild(xmlNodes1);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists = xmlDocument1.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception)
				{
					XmlDocument xmlDocument2 = this.xDocLocalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists = xmlDocument2.SelectNodes(string.Concat(pBookId));
				}
			}
			if (Settings.Default.EnableGlobalMemo)
			{
				XmlDocument xmlDocument3 = this.xDocGlobalMemo;
				pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@PartNo='']" };
				xmlNodeLists1 = xmlDocument3.SelectNodes(string.Concat(pBookId));
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
					{
						SortedDictionary<DateTime, XmlNode> dateTimes1 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes4 in xmlNodeLists1)
						{
							DateTime dateTime2 = DateTime.ParseExact(xmlNodes4.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes1, dateTime2, xmlNodes4);
						}
						XmlDocument xmlDocument4 = new XmlDocument();
						XmlNode xmlNodes5 = xmlDocument4.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair1 in dateTimes1.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes6 = xmlDocument4.CreateElement("Memo");
								XmlNode value2 = keyValuePair1.Value;
								for (int k = 0; k < value2.Attributes.Count; k++)
								{
									XmlAttribute xmlAttribute2 = xmlDocument4.CreateAttribute(value2.Attributes[k].Name);
									xmlAttribute2.Value = value2.Attributes[k].Value;
									xmlNodes6.Attributes.Append(xmlAttribute2);
								}
								xmlNodes5.AppendChild(xmlNodes6);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair2 in dateTimes1)
							{
								XmlNode xmlNodes7 = xmlDocument4.CreateElement("Memo");
								XmlNode value3 = keyValuePair2.Value;
								for (int l = 0; l < value3.Attributes.Count; l++)
								{
									XmlAttribute xmlAttribute3 = xmlDocument4.CreateAttribute(value3.Attributes[l].Name);
									xmlAttribute3.Value = value3.Attributes[l].Value;
									xmlNodes7.Attributes.Append(xmlAttribute3);
								}
								xmlNodes5.AppendChild(xmlNodes7);
							}
						}
						xmlDocument4.AppendChild(xmlNodes5);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists1 = xmlDocument4.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception1)
				{
					XmlDocument xmlDocument5 = this.xDocGlobalMemo;
					pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists1 = xmlDocument5.SelectNodes(string.Concat(pBookId));
				}
			}
			if (Settings.Default.EnableAdminMemo && Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"] != null)
			{
				XmlNode pageSchemaNode = this.objFrmTreeview.PageSchemaNode;
				XmlNode picNode = this.objFrmTreeview.PicNode;
				string item = Program.iniServers[this.ServerId].items["PIC_SETTINGS", "MEM"];
				pBookId = new string[] { "," };
				string[] strArrays = item.Split(pBookId, StringSplitOptions.None);
				ArrayList arrayLists = new ArrayList();
				for (int m = 0; m < (int)strArrays.Length; m++)
				{
					foreach (XmlAttribute attribute in pageSchemaNode.Attributes)
					{
						if (attribute.Value.ToUpper() != strArrays[m].ToUpper())
						{
							continue;
						}
						arrayLists.Add(attribute.Name);
						break;
					}
				}
				string empty = string.Empty;
				foreach (string arrayList in arrayLists)
				{
					if (picNode.Attributes[arrayList] == null || !(picNode.Attributes[arrayList].Value.Trim() != string.Empty))
					{
						continue;
					}
					empty = string.Concat(empty, picNode.Attributes[arrayList].Value, "**");
				}
				XmlDocument xmlDocument6 = new XmlDocument();
				xmlDocument6.LoadXml("<?xml version='1.0' encoding='utf-8'?><Memos></Memos>");
				pBookId = new string[] { "**" };
				string[] strArrays1 = empty.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
				if ((int)strArrays1.Length > 0)
				{
					string[] strArrays2 = strArrays1;
					for (int n = 0; n < (int)strArrays2.Length; n++)
					{
						string str = strArrays2[n];
						if (str.Contains("||"))
						{
							pBookId = new string[] { "||" };
							string[] strArrays3 = str.Split(pBookId, StringSplitOptions.RemoveEmptyEntries);
							string str1 = strArrays3[0];
							str1 = (str1.ToUpper().Equals("TXT") || str1.ToUpper().Equals("REF") || str1.ToUpper().Equals("HYP") || str1.ToUpper().Equals("PRG") ? str1.ToLower() : "Unknown");
							string str2 = strArrays3[1];
							string name = string.Empty;
							foreach (XmlAttribute attribute1 in pageSchemaNode.Attributes)
							{
								if (!attribute1.Value.ToUpper().Equals("UPDATEDATE"))
								{
									continue;
								}
								name = attribute1.Name;
								break;
							}
							string empty1 = string.Empty;
							if (picNode.Attributes[name] != null)
							{
								empty1 = picNode.Attributes[name].Value;
							}
							xmlDocument6.DocumentElement.AppendChild(xmlDocument6.ImportNode(this.CreateAdminMemoNode(str1, str2, string.Empty, empty1), true));
						}
					}
				}
				xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				try
				{
					if (this.GetMemoSortType().ToUpper() != "UNKNOWN")
					{
						SortedDictionary<DateTime, XmlNode> dateTimes2 = new SortedDictionary<DateTime, XmlNode>();
						foreach (XmlNode xmlNodes8 in xmlNodeLists2)
						{
							DateTime dateTime3 = DateTime.ParseExact(xmlNodes8.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
							this.UpdateMemoDictionary(dateTimes2, dateTime3, xmlNodes8);
						}
						XmlDocument xmlDocument7 = new XmlDocument();
						XmlNode xmlNodes9 = xmlDocument7.CreateElement("Memos");
						if (this.GetMemoSortType().ToUpper() == "DESC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> keyValuePair3 in dateTimes2.Reverse<KeyValuePair<DateTime, XmlNode>>())
							{
								XmlNode xmlNodes10 = xmlDocument7.CreateElement("Memo");
								XmlNode value4 = keyValuePair3.Value;
								for (int o = 0; o < value4.Attributes.Count; o++)
								{
									XmlAttribute xmlAttribute4 = xmlDocument7.CreateAttribute(value4.Attributes[o].Name);
									xmlAttribute4.Value = value4.Attributes[o].Value;
									xmlNodes10.Attributes.Append(xmlAttribute4);
								}
								xmlNodes9.AppendChild(xmlNodes10);
							}
						}
						else if (this.GetMemoSortType().ToUpper() == "ASC")
						{
							foreach (KeyValuePair<DateTime, XmlNode> dateTime4 in dateTimes2)
							{
								XmlNode xmlNodes11 = xmlDocument7.CreateElement("Memo");
								XmlNode value5 = dateTime4.Value;
								for (int p = 0; p < value5.Attributes.Count; p++)
								{
									XmlAttribute xmlAttribute5 = xmlDocument7.CreateAttribute(value5.Attributes[p].Name);
									xmlAttribute5.Value = value5.Attributes[p].Value;
									xmlNodes11.Attributes.Append(xmlAttribute5);
								}
								xmlNodes9.AppendChild(xmlNodes11);
							}
						}
						xmlDocument7.AppendChild(xmlNodes9);
						pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
						xmlNodeLists2 = xmlDocument7.SelectNodes(string.Concat(pBookId));
					}
				}
				catch (Exception exception2)
				{
					xmlNodeLists2 = xmlDocument6.SelectNodes("//Memo");
				}
			}
			if (!popup)
			{
				if (Settings.Default.EnableLocalMemo || Settings.Default.EnableGlobalMemo || xmlNodeLists2 != null && xmlNodeLists2.Count > 0)
				{
					this.objFrmMemo = new frmMemo(this, xmlNodeLists, xmlNodeLists1, xmlNodeLists2, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, string.Empty, string.Empty, string.Empty)
					{
						Owner = this
					};
					this.bFromPopup = false;
					this.ShowDimmer();
					this.objFrmMemo.Show();
					return;
				}
				MessageHandler.ShowInformation(this.GetResource("All memos are disabled. Enable memo from settings screen", "MEMO_DISABLED", ResourceType.POPUP_MESSAGE));
			}
			else if (xmlNodeLists != null && xmlNodeLists.Count > 0 || xmlNodeLists1 != null && xmlNodeLists1.Count > 0 || xmlNodeLists2 != null && xmlNodeLists2.Count > 0)
			{
				this.objFrmMemo = new frmMemo(this, xmlNodeLists, xmlNodeLists1, xmlNodeLists2, this.objFrmPicture.CurrentPageId, this.objFrmPartlist.CurrentPicIndex, string.Empty, string.Empty, string.Empty)
				{
					Owner = this
				};
				this.bFromPopup = true;
				this.ShowDimmer();
				this.objFrmMemo.Show();
				return;
			}
		}

		public void ShowQuantityScreen(string sPartNumber)
		{
			try
			{
				this.objFrmSelectionlist.ShowQuantityScreen(sPartNumber);
			}
			catch
			{
			}
		}

		public void ShowSelectionList()
		{
			if (!this.objFrmSelectionlist.InvokeRequired)
			{
				this.EnableSelectionlistShowHideButton(true);
				this.objFrmSelectionlist.Show();
				return;
			}
			this.objFrmSelectionlist.Invoke(new frmViewer.ShowSelectionListDelegate(this.ShowSelectionList));
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

		public ArrayList SingleBookDownloadList(string sLocalPath)
		{
			ArrayList arrayLists = new ArrayList();
			this.objFrmTreeview.GetPagesToDownload(ref arrayLists, sLocalPath, this.p_Compressed);
			this.BookDowloadAddSearchXmlFile(ref arrayLists, sLocalPath, this.BookPublishingId, this.BookType);
			return arrayLists;
		}

		private void singleBookToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (DataSize.spaceLeft < (long)10485760)
			{
				this.ShowNotification();
				MessageBox.Show(this, string.Concat(this.GetResource("Low Disk Space", "LOW_DISK", ResourceType.POPUP_MESSAGE), "\n", this.GetResource("Manage disk to free some space", "FREE_SPACE", ResourceType.POPUP_MESSAGE)), this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			string empty = string.Empty;
			string item = string.Empty;
			item = Program.iniServers[this.ServerId].items["SETTINGS", "CONTENT_PATH"];
			if (!item.EndsWith("/"))
			{
				item = string.Concat(item, "/");
			}
			item = string.Concat(item, this.BookPublishingId);
			empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
			empty = string.Concat(empty, "\\", Program.iniServers[this.ServerId].sIniKey);
			empty = string.Concat(empty, "\\", this.BookPublishingId);
			if (!Directory.Exists(empty))
			{
				Directory.CreateDirectory(empty);
			}
			frmSingleBookDownload _frmSingleBookDownload = new frmSingleBookDownload(this, empty, item)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmSingleBookDownload.Show();
			_frmSingleBookDownload.Visible = true;
		}

		private void textSearceNameSearchSettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show(ConfigTasks.Search_Text);
		}

		private void textSearchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSearch _frmSearch = new frmSearch(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmSearch.Show(SearchTasks.TextSearch);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			this.connectionToolStripMenuItem_Click(null, null);
		}

		private void tsbAbout_Click(object sender, EventArgs e)
		{
			this.aboutGSPcLocalToolStripMenuItem_Click(null, null);
		}

		private void tsbAddBookmarks_Click(object sender, EventArgs e)
		{
			this.AddBookmarksToolStripMenuItem_Click(null, null);
		}

		private void tsbAddPictureMemo_Click(object sender, EventArgs e)
		{
			this.ShowPictureMemos(false);
		}

		private void tsbConfiguration_Click(object sender, EventArgs e)
		{
			frmConfig _frmConfig = new frmConfig(this)
			{
				Owner = this
			};
			this.ShowDimmer();
			_frmConfig.Show();
		}

		private void tsbDataCleanup_Click(object sender, EventArgs e)
		{
			this.manageDiskSpaceToolStripMenuItem_Click(null, null);
		}

		private void tsbFindText_Click(object sender, EventArgs e)
		{
			try
			{
				this.objFrmPicture.TextSearch();
			}
			catch
			{
				MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void tsbFitPage_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.FitPage();
		}

		private void tsbHelp_Click(object sender, EventArgs e)
		{
			this.gSPcLocalHelpToolStripMenuItem_Click(null, null);
		}

		private void tsbHistoryBack_Click(object sender, EventArgs e)
		{
			this.tsbHistoryBack.Enabled = false;
			this.tsbHistoryForward.Enabled = false;
			this.LoadDataFromNode(this.objHistory.Back());
		}

		private void tsbHistoryForward_Click(object sender, EventArgs e)
		{
			this.tsbHistoryBack.Enabled = false;
			this.tsbHistoryForward.Enabled = false;
			this.LoadDataFromNode(this.objHistory.Forward());
		}

		private void tsbMemoList_Click(object sender, EventArgs e)
		{
			this.ShowAllMemos();
		}

		private void tsbMemoRecovery_Click(object sender, EventArgs e)
		{
			this.memoRecoveryToolStripMenuItem_Click(null, null);
		}

		private void tsbMultipleBooksDownload_Click(object sender, EventArgs e)
		{
			this.multipleBooksToolStripMenuItem_Click(null, null);
		}

		private void tsbNavigateFirst_Click(object sender, EventArgs e)
		{
			if (!this.objFrmPicture.isWorking && !this.objFrmPartlist.isWorking)
			{
				this.objFrmTreeview.SelectFirstNode();
			}
		}

		private void tsbNavigateLast_Click(object sender, EventArgs e)
		{
			if (!this.objFrmPicture.isWorking && !this.objFrmPartlist.isWorking)
			{
				this.objFrmTreeview.SelectLastNode();
			}
		}

		private void tsbNavigateNext_Click(object sender, EventArgs e)
		{
			if (!this.objFrmPicture.isWorking && !this.objFrmPartlist.isWorking)
			{
				this.objFrmTreeview.SelectNextNode();
			}
		}

		private void tsbNavigatePrevious_Click(object sender, EventArgs e)
		{
			if (!this.objFrmPicture.isWorking && !this.objFrmPartlist.isWorking)
			{
				this.objFrmTreeview.SelectPreviousNode();
			}
		}

		private void tsbOpenBook_Click(object sender, EventArgs e)
		{
			this.openToolStripMenuItem_Click(null, null);
		}

		private void tsbPicCopy_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.CopyImage();
		}

		private void tsbPicNext_Click(object sender, EventArgs e)
		{
			if (!this.objFrmPicture.IsDisposed)
			{
				this.objFrmPicture.tsBtnNext_Click(null, null);
			}
		}

		private void tsbPicPanMode_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.SetPanMode();
		}

		private void tsbPicPrev_Click(object sender, EventArgs e)
		{
			if (!this.objFrmPicture.IsDisposed)
			{
				this.objFrmPicture.tsBtnPrev_Click(null, null);
			}
		}

		private void tsbPicSelectText_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.SelectText();
		}

		private void tsbPicZoomIn_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.ZoomIn();
		}

		private void tsbPicZoomOut_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.ZoomOut();
		}

		private void tsbPicZoomSelect_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.SelectZoom();
		}

		private void tsbPortal_Click(object sender, EventArgs e)
		{
			string item = Program.iniServers[this.p_ServerId].items["SETTINGS", "PORTAL_PATH"];
			if (item != null && item != string.Empty)
			{
				this.OpenSpecifiedBrowser(item);
				return;
			}
			MessageBox.Show(this.GetResource("(E-VWR-EM013) URL not found", "(E-VWR-EM013)_URL", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private void tsbPrint_Click(object sender, EventArgs e)
		{
			this.OpenPrintDialogue(-1);
		}

		private void tsbRestoreDefaults_Click(object sender, EventArgs e)
		{
			this.restoreDefaultsToolStripMenuItem_Click(null, null);
		}

		private void tsBRotateLeft_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.tsBRotateLeft_Click(null, null);
		}

		private void tsBRotateRight_Click(object sender, EventArgs e)
		{
			this.objFrmPicture.tsBRotateRight_Click(null, null);
		}

		private void tsbSearchPageName_Click(object sender, EventArgs e)
		{
			this.pageNameToolStripMenuItem_Click(null, null);
		}

		private void tsbSearchPartAdvance_Click(object sender, EventArgs e)
		{
			this.advancedSearchToolStripMenuItem_Click(null, null);
		}

		private void tsbSearchPartName_Click(object sender, EventArgs e)
		{
			this.partNameToolStripMenuItem_Click(null, null);
		}

		private void tsbSearchPartNumber_Click(object sender, EventArgs e)
		{
			this.partNumberToolStripMenuItem_Click(null, null);
		}

		private void tsbSearchText_Click(object sender, EventArgs e)
		{
			this.textSearchToolStripMenuItem_Click(null, null);
		}

		private void tsbSingleBookDownload_Click(object sender, EventArgs e)
		{
			this.singleBookToolStripMenuItem_Click(null, null);
		}

		private void tsbThirdPartyBasket_Click(object sender, EventArgs e)
		{
			this.objFrmSelectionlist.tsBtnThirdPartyBasket_Click(null, null);
		}

		private void tsbThumbnail_Click(object sender, EventArgs e)
		{
			try
			{
				this.objFrmPicture.ShowHideThumbnail();
			}
			catch
			{
				MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void tsbViewContents_Click(object sender, EventArgs e)
		{
			this.contentsToolStripMenuItem_Click(null, null);
		}

		private void tsbViewInfo_Click(object sender, EventArgs e)
		{
			this.informationToolStripMenuItem_Click(null, null);
		}

		private void tsbViewPartslist_Click(object sender, EventArgs e)
		{
			this.partslistToolStripMenuItem_Click(null, null);
		}

		private void tsbViewPicture_Click(object sender, EventArgs e)
		{
			this.pictureToolStripMenuItem_Click(null, null);
		}

		private void tsHistory_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		public void UncheckAllRows()
		{
			try
			{
				this.frmParent.UncheckAllRows();
			}
			catch
			{
			}
		}

		public void UpdateCurrentPageForPartslist(bool picLoaded, XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex, string attPic, string attList, string attUpdateDate)
		{
			this.objFrmPartlist.UpdateCurrentPageForPartslist(picLoaded, schemaNode, pageNode, picIndex, listIndex, attPic, attList, attUpdateDate);
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			if (this.objFrmTreeview != null)
			{
				this.objFrmTreeview.UpdateFont();
			}
			if (this.objFrmPartlist != null)
			{
				this.objFrmPartlist.UpdateFont();
			}
			if (this.objFrmInfo != null)
			{
				this.objFrmInfo.UpdateFont();
			}
			if (this.objFrmSelectionlist != null)
			{
				this.objFrmSelectionlist.UpdateFont();
			}
			this.menuStrip1.Font = Settings.Default.appFont;
		}

		public void updateGlobalMemo()
		{
			XmlNodeList xmlNodeLists = null;
			string empty = string.Empty;
			if (Settings.Default.EnableLocalMemo)
			{
				XmlDocument xmlDocument = this.xDocLocalMemo;
				string[] pBookId = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
				xmlDocument.SelectNodes(string.Concat(pBookId));
			}
			if (Settings.Default.EnableGlobalMemo)
			{
				XmlDocument xmlDocument1 = this.xDocGlobalMemo;
				string[] strArrays = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "'][@PageId='", this.objFrmPicture.CurrentPageId, "'][@PicIndex='", this.objFrmPartlist.CurrentPicIndex, "'][@PartNo='']" };
				xmlNodeLists = xmlDocument1.SelectNodes(string.Concat(strArrays));
				try
				{
					SortedDictionary<DateTime, XmlNode> dateTimes = new SortedDictionary<DateTime, XmlNode>();
					foreach (XmlNode xmlNodes in xmlNodeLists)
					{
						DateTime dateTime = DateTime.ParseExact(xmlNodes.Attributes["Update"].Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
						this.UpdateMemoDictionary(dateTimes, dateTime, xmlNodes);
					}
					XmlDocument xmlDocument2 = new XmlDocument();
					XmlNode xmlNodes1 = xmlDocument2.CreateElement("Memos");
					if (this.GetMemoSortType().ToUpper() != "DESC")
					{
						foreach (KeyValuePair<DateTime, XmlNode> keyValuePair in dateTimes)
						{
							XmlNode xmlNodes2 = xmlDocument2.CreateElement("Memo");
							XmlNode value = keyValuePair.Value;
							for (int i = 0; i < value.Attributes.Count; i++)
							{
								XmlAttribute xmlAttribute = xmlDocument2.CreateAttribute(value.Attributes[i].Name);
								xmlAttribute.Value = value.Attributes[i].Value;
								xmlNodes2.Attributes.Append(xmlAttribute);
							}
							xmlNodes1.AppendChild(xmlNodes2);
						}
					}
					else
					{
						foreach (KeyValuePair<DateTime, XmlNode> keyValuePair1 in dateTimes.Reverse<KeyValuePair<DateTime, XmlNode>>())
						{
							XmlNode xmlNodes3 = xmlDocument2.CreateElement("Memo");
							XmlNode value1 = keyValuePair1.Value;
							for (int j = 0; j < value1.Attributes.Count; j++)
							{
								XmlAttribute xmlAttribute1 = xmlDocument2.CreateAttribute(value1.Attributes[j].Name);
								xmlAttribute1.Value = value1.Attributes[j].Value;
								xmlNodes3.Attributes.Append(xmlAttribute1);
							}
							xmlNodes1.AppendChild(xmlNodes3);
						}
					}
					xmlDocument2.AppendChild(xmlNodes1);
					string[] pBookId1 = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists = xmlDocument2.SelectNodes(string.Concat(pBookId1));
				}
				catch (Exception exception)
				{
					XmlDocument xmlDocument3 = this.xDocGlobalMemo;
					string[] strArrays1 = new string[] { "//Memos/Memo[@ServerKey='", Program.iniServers[this.p_ServerId].sIniKey, "'][@BookId='", this.p_BookId, "']" };
					xmlNodeLists = xmlDocument3.SelectNodes(string.Concat(strArrays1));
				}
			}
		}

		private void UpdateMemoDictionary(SortedDictionary<DateTime, XmlNode> dic, DateTime dt, XmlNode xNode)
		{
			try
			{
				dic.Add(dt, xNode);
			}
			catch (Exception exception)
			{
				dt = dt.AddSeconds(1);
				this.UpdateMemoDictionary(dic, dt, xNode);
			}
		}

		public void UpdatePartAndSelectionList()
		{
			try
			{
				this.objFrmSelectionlist.selListInitialize();
				this.objFrmPartlist.Set_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
				this.objFrmPartlist.Set_MemoCols_HeaderText_Visibility_Width_Alignment_DisplayIndex_FromIni();
			}
			catch (Exception exception)
			{
			}
		}

		public void UpdatePicToolstrip(bool enablePrev, bool enableNext, string picNo)
		{
			this.tsPic.Enabled = true;
			this.tslPic.Enabled = true;
			this.tsbPicPrev.Enabled = enablePrev;
			this.tsbPicNext.Enabled = enableNext;
			this.tstPicNo.Text = picNo;
		}

		public void UpdateStatus(string status)
		{
			if (!this.ssStatus.InvokeRequired)
			{
				this.lblStatus.Text = status;
				return;
			}
			StatusStrip statusStrip = this.ssStatus;
			frmViewer.UpdateStatusDelegate updateStatusDelegate = new frmViewer.UpdateStatusDelegate(this.UpdateStatus);
			object[] objArray = new object[] { status };
			statusStrip.Invoke(updateStatusDelegate, objArray);
		}

		public void UpdateViewerTitle()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmViewer.UpdateViewerTitleDelegate(this.UpdateViewerTitle));
				return;
			}
			string str = " : ";
			string empty = string.Empty;
			string name = string.Empty;
			string value = string.Empty;
			string empty1 = string.Empty;
			string item = string.Empty;
			try
			{
				item = Program.iniServers[this.ServerId].items["SETTINGS", "DISPLAY_NAME"];
				foreach (XmlAttribute attribute in this.SchemaNode.Attributes)
				{
					if (attribute.Value != null && attribute.Value.ToUpper() == "DESCRIPTION1")
					{
						name = attribute.Name;
					}
					if (attribute.Value == null || !(attribute.Value.ToUpper() == "DESCRIPTION2"))
					{
						continue;
					}
					empty1 = attribute.Name;
				}
				if (this.BookNode.Attributes[name] != null)
				{
					empty = this.BookNode.Attributes[name].Value;
				}
				if (this.BookNode.Attributes[empty1] != null)
				{
					value = this.BookNode.Attributes[empty1].Value;
				}
				if (empty != string.Empty && value != string.Empty && this.sBookType != string.Empty && this.BookPublishingId != string.Empty && this.sFirstPageTitle != string.Empty)
				{
					string str1 = item;
					string[] bookPublishingId = new string[] { str1, str, this.sBookType, str, empty, str, value, " ( ", this.BookPublishingId, str, this.sFirstPageTitle, " )" };
					item = string.Concat(bookPublishingId);
				}
				else if ((empty == string.Empty || value == string.Empty) && this.sBookType != string.Empty && this.BookPublishingId != string.Empty && this.sFirstPageTitle != string.Empty)
				{
					string str2 = item;
					string[] strArrays = new string[] { str2, str, this.sBookType, " ( ", this.BookPublishingId, str, this.sFirstPageTitle, " )" };
					item = string.Concat(strArrays);
				}
				this.Text = item;
			}
			catch
			{
				this.Text = this.GetResource("GSPcLocal Viewer 3.0", "GSPcLocal Viewer 3.0", ResourceType.TITLE);
			}
		}

		private bool ValueInRangeFilter(string range, string value)
		{
			int num;
			int num1;
			int num2;
			bool flag;
			string[] strArrays = new string[] { "**" };
			string[] strArrays1 = range.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			if ((int)strArrays1.Length != 2)
			{
				return false;
			}
			if (int.TryParse(strArrays1[0], out num) && int.TryParse(strArrays1[1], out num1))
			{
				if (num > num1)
				{
					int num3 = num;
					num = num1;
					num1 = num3;
				}
				if (!int.TryParse(value, out num2))
				{
					return false;
				}
				if (num2 >= num && num2 <= num1)
				{
					return true;
				}
				return false;
			}
			try
			{
				DateTime dateTime = DateTime.ParseExact(strArrays1[0], "dd/MM/yyyy", null);
				DateTime dateTime1 = DateTime.ParseExact(strArrays1[1], "dd/MM/yyyy", null);
				DateTime dateTime2 = DateTime.ParseExact(value, "dd/MM/yyyy", null);
				if (dateTime > dateTime1)
				{
					DateTime dateTime3 = dateTime;
					dateTime = dateTime1;
					dateTime1 = dateTime3;
				}
				flag = (!(dateTime2 >= dateTime) || !(dateTime2 <= dateTime1) ? false : true);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private bool ValueMatchesSelectFilter(string values1, string values2, bool caseSensitive)
		{
			bool flag;
			string[] strArrays = new string[] { "," };
			string[] strArrays1 = values1.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			string[] strArrays2 = new string[] { "," };
			string[] strArrays3 = values2.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
			string[] strArrays4 = strArrays1;
			int num = 0;
		yoyo0:
			while (num < (int)strArrays4.Length)
			{
				string str = strArrays4[num];
				string[] strArrays5 = strArrays3;
				int num1 = 0;
				while (true)
				{
					if (num1 < (int)strArrays5.Length)
					{
						string str1 = strArrays5[num1];
						if (caseSensitive)
						{
							if (str.Equals(str1))
							{
								flag = true;
								break;
							}
						}
						else if (str.ToUpper().Equals(str1.ToUpper()))
						{
							flag = true;
							break;
						}
						num1++;
					}
					else
					{
						num++;
						goto yoyo0;
					}
				}
				return flag;
			}
			return false;
		}

		private void viewMemoListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ShowAllMemos();
		}

		protected override void WndProc(ref Message m)
		{
			if (this.objDimmer != null && this.objDimmer.Visible)
			{
				if (m.Msg == 10)
				{
					return;
				}
				if (m.Msg == 163 || m.Msg == 165 || m.Msg == 132)
				{
					return;
				}
				if (m.Msg == 274 || m.Msg == 161 || m.Msg == 164)
				{
					return;
				}
				if (m.Msg == 33)
				{
					m.Result = (IntPtr)4;
					return;
				}
			}
			base.WndProc(ref m);
		}

		private void workOffLineMenuItem_Click(object sender, EventArgs e)
		{
			this.ChangeApplicationMode();
		}

		private void WriteUserSettingINI()
		{
			try
			{
				string empty = string.Empty;
				bool flag = false;
				string str = string.Concat(Application.StartupPath, "\\UserSet.ini");
				try
				{
					if (File.Exists(str))
					{
						IniFile iniFile = new IniFile(str, "UserSet");
						if (iniFile.items["INI_INFO", "LAST_MODIFIED_DATE"] != null)
						{
							flag = true;
							if (!string.IsNullOrEmpty(iniFile.items["INI_INFO", "LAST_MODIFIED_DATE"]))
							{
								empty = iniFile.items["INI_INFO", "LAST_MODIFIED_DATE"].ToString();
								if (!this.IsValidDateTime(empty))
								{
									empty = "";
								}
							}
						}
						File.Delete(str);
					}
					using (StreamWriter streamWriter = new StreamWriter(str, true, Encoding.Unicode))
					{
					}
				}
				catch (IOException oException)
				{
				}
				IniFileIO iniFileIO = new IniFileIO();
				iniFileIO.WriteValue("PROXY_SETTINGS", "PROXY_IP", Settings.Default.appProxyIP.ToString(), str);
				iniFileIO.WriteValue("PROXY_SETTINGS", "PROXY_PORT", Settings.Default.appProxyPort.ToString(), str);
				iniFileIO.WriteValue("PROXY_SETTINGS", "PROXY_TYPE", Settings.Default.appProxyType.ToString(), str);
				iniFileIO.WriteValue("PROXY_SETTINGS", "PROXY_TIMEOUT", Settings.Default.appProxyTimeOut.ToString(), str);
				iniFileIO.WriteValue("PROXY_SETTINGS", "USERNAME", Settings.Default.appProxyLogin.ToString(), str);
				iniFileIO.WriteValue("PROXY_SETTINGS", "PASSWORD", Settings.Default.appProxyPassword.ToString(), str);
				iniFileIO.WriteValue("FONT", "FONT_NAME", Settings.Default.appFont.FontFamily.Name.ToString(), str);
				float size = Settings.Default.appFont.Size;
				iniFileIO.WriteValue("FONT", "FONT_SIZE", size.ToString(), str);
				iniFileIO.WriteValue("FONT_PRINT", "FONT_NAME", Settings.Default.printFont.FontFamily.Name.ToString(), str);
				float single = Settings.Default.printFont.Size;
				iniFileIO.WriteValue("FONT_PRINT", "FONT_SIZE", single.ToString(), str);
				iniFileIO.WriteValue("CUSTOM_COLOR", "BACKGROUND_COLOR", ColorTranslator.ToHtml(Settings.Default.appHighlightBackColor).ToString(), str);
				iniFileIO.WriteValue("CUSTOM_COLOR", "FORE_COLOR", ColorTranslator.ToHtml(Settings.Default.appHighlightForeColor).ToString(), str);
				iniFileIO.WriteValue("CUSTOM_COLOR", "PLIST_INFO_BACKGROUND_COLOR", ColorTranslator.ToHtml(Settings.Default.PartsListInfoBackColor).ToString(), str);
				iniFileIO.WriteValue("CUSTOM_COLOR", "PLIST_INFO_FORE_COLOR", ColorTranslator.ToHtml(Settings.Default.PartsListInfoForeColor).ToString(), str);
				int x = Settings.Default.frmOpenBookLocation.X;
				string str1 = x.ToString();
				int y = Settings.Default.frmOpenBookLocation.Y;
				iniFileIO.WriteValue("OPEN_BOOK", "FRM_LOCATION", string.Concat(str1, ",", y.ToString()), str);
				string str2 = Settings.Default.frmOpenBookSize.Width.ToString();
				int height = Settings.Default.frmOpenBookSize.Height;
				iniFileIO.WriteValue("OPEN_BOOK", "FRM_SIZE", string.Concat(str2, ",", height.ToString()), str);
				iniFileIO.WriteValue("OPEN_BOOK", "FRM_STATE", Settings.Default.frmOpenBookState.ToString(), str);
				bool openInCurrentInstance = Settings.Default.OpenInCurrentInstance;
				iniFileIO.WriteValue("OPEN_BOOK", "OPEN_IN_CURRENT_INSTANCE", openInCurrentInstance.ToString(), str);
				iniFileIO.WriteValue("MEMO_SETTINGS", "GLOBAL_MEMO_FOLDER", Settings.Default.GlobalMemoFolder.ToString(), str);
				bool enableAdminMemo = Settings.Default.EnableAdminMemo;
				iniFileIO.WriteValue("MEMO_SETTINGS", "ENABLE_ADMIN_MEMO", enableAdminMemo.ToString(), str);
				bool enableGlobalMemo = Settings.Default.EnableGlobalMemo;
				iniFileIO.WriteValue("MEMO_SETTINGS", "ENABLE_GLOBAL_MEMO", enableGlobalMemo.ToString(), str);
				bool enableLocalMemo = Settings.Default.EnableLocalMemo;
				iniFileIO.WriteValue("MEMO_SETTINGS", "ENABLE_LOCAL_MEMO", enableLocalMemo.ToString(), str);
				bool popupPictureMemo = Settings.Default.PopupPictureMemo;
				iniFileIO.WriteValue("MEMO_SETTINGS", "POPUP_PICTURE_MEMO", popupPictureMemo.ToString(), str);
				bool localMemoBackup = Settings.Default.LocalMemoBackup;
				iniFileIO.WriteValue("MEMO_SETTINGS", "LOCAL_MEMO_BACKUP", localMemoBackup.ToString(), str);
				iniFileIO.WriteValue("MEMO_SETTINGS", "LOCAL_MEMO_BACKUP_FILE", Settings.Default.LocalMemoBackupFile.ToString(), str);
				string str3 = Settings.Default.frmMemoListLocation.X.ToString();
				int num = Settings.Default.frmMemoListLocation.Y;
				iniFileIO.WriteValue("MEMO_SETTINGS", "MEMO_LIST_LOCATION", string.Concat(str3, ",", num.ToString()), str);
				string str4 = Settings.Default.frmMemoListSize.Width.ToString();
				int height1 = Settings.Default.frmMemoListSize.Height;
				iniFileIO.WriteValue("MEMO_SETTINGS", "MEMO_LIST_SIZE", string.Concat(str4, ",", height1.ToString()), str);
				iniFileIO.WriteValue("MEMO_SETTINGS", "MEMO_LIST_STATE", Settings.Default.frmMemoListState.ToString(), str);
				string str5 = Settings.Default.frmMemoLocation.X.ToString();
				int y1 = Settings.Default.frmMemoLocation.Y;
				iniFileIO.WriteValue("MEMO_SETTINGS", "MEMO_LOCATION", string.Concat(str5, ",", y1.ToString()), str);
				string str6 = Settings.Default.frmMemoSize.Width.ToString();
				int num1 = Settings.Default.frmMemoSize.Height;
				iniFileIO.WriteValue("MEMO_SETTINGS", "MEMO_SIZE", string.Concat(str6, ",", num1.ToString()), str);
				iniFileIO.WriteValue("MEMO_SETTINGS", "MEMO_STATE", Settings.Default.frmMemoState.ToString(), str);
				iniFileIO.WriteValue("ZOOM", "DEFAULT_PICTURE_ZOOM", Settings.Default.DefaultPictureZoom.ToString(), str);
				bool maintainZoom = Settings.Default.MaintainZoom;
				iniFileIO.WriteValue("ZOOM", "MAINTAIN_ZOOM", maintainZoom.ToString(), str);
				int @default = Settings.Default.appZoomStep;
				iniFileIO.WriteValue("ZOOM", "ZOOM_STEP", @default.ToString(), str);
				iniFileIO.WriteValue("ZOOM", "CURRENT_ZOOM", Settings.Default.appCurrentZoom.ToString(), str);
				bool showPicToolbar = Settings.Default.ShowPicToolbar;
				iniFileIO.WriteValue("TOOLBARS", "SHOW_PIC_TOOLBAR", showPicToolbar.ToString(), str);
				bool showListToolbar = Settings.Default.ShowListToolbar;
				iniFileIO.WriteValue("TOOLBARS", "SHOW_LIST_TOOLBAR", showListToolbar.ToString(), str);
				string str7 = Settings.Default.frmSearchLocation.X.ToString();
				int y2 = Settings.Default.frmSearchLocation.Y;
				iniFileIO.WriteValue("SEARCH", "FRM_LOCATION", string.Concat(str7, ",", y2.ToString()), str);
				string str8 = Settings.Default.frmSearchSize.Width.ToString();
				int height2 = Settings.Default.frmSearchSize.Height;
				iniFileIO.WriteValue("SEARCH", "FRM_SIZE", string.Concat(str8, ",", height2.ToString()), str);
				iniFileIO.WriteValue("SEARCH", "FRM_STATE", Settings.Default.frmSearchState.ToString(), str);
				string str9 = Settings.Default.frmDataSizeLocation.X.ToString();
				int num2 = Settings.Default.frmDataSizeLocation.Y;
				iniFileIO.WriteValue("DATASIZE", "FRM_LOCATION", string.Concat(str9, ",", num2.ToString()), str);
				string str10 = Settings.Default.frmDataSizeSize.Width.ToString();
				int height3 = Settings.Default.frmDataSizeSize.Height;
				iniFileIO.WriteValue("DATASIZE", "FRM_SIZE", string.Concat(str10, ",", height3.ToString()), str);
				iniFileIO.WriteValue("DATASIZE", "FRM_STATE", Settings.Default.frmDataSizeState.ToString(), str);
				iniFileIO.WriteValue("LANGUAGE", "APP_LANGUAGE", Settings.Default.appLanguage.ToString(), str);
				int historySize = Settings.Default.HistorySize;
				iniFileIO.WriteValue("HISTORY", "SIZE", historySize.ToString(), str);
				bool default1 = Settings.Default.appFitPageForMultiParts;
				iniFileIO.WriteValue("MULTIPARTS", "FITPAGE", default1.ToString(), str);
				iniFileIO.WriteValue("APPLICATION_SETTINGS", "WEB_BROWSER", Settings.Default.appWebBrowser.ToString(), str);
				iniFileIO.WriteValue("APPLICATION_SETTINGS", "APPLICATION_CURRENT_SIZE", Settings.Default.appCurrentSize.ToString(), str);
				bool @checked = this.miniMapToolStripMenuItem.Checked;
				iniFileIO.WriteValue("MINIMAP_SETTINGS", "SHOW_MINIMAP", @checked.ToString(), str);
				openInCurrentInstance = Settings.Default.SideBySidePrinting;
				iniFileIO.WriteValue("PRINT_SETTINGS", "SIDE_BY_SIDE", openInCurrentInstance.ToString(), str);
				if (!Program.objAppFeatures.bDjVuScroll)
				{
					iniFileIO.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS", "FALSE", str);
					iniFileIO.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE", "FALSE", str);
				}
				else
				{
					openInCurrentInstance = Settings.Default.MouseScrollContents;
					iniFileIO.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_CONTENTS", openInCurrentInstance.ToString(), str);
					openInCurrentInstance = Settings.Default.MouseScrollPicture;
					iniFileIO.WriteValue("APPLICATION_SETTINGS", "MOUSEWHEEL_SCROLL_PICTURE", openInCurrentInstance.ToString(), str);
				}
				openInCurrentInstance = Settings.Default.appPartsListInfoVisible;
				iniFileIO.WriteValue("APPLICATION_SETTINGS", "APPLICATION_PLISTINFO_SHOWN", openInCurrentInstance.ToString(), str);
				try
				{
					openInCurrentInstance = Settings.Default.RowSelectionMode;
					iniFileIO.WriteValue("APPLICATION_SETTINGS", "PARTSLIST_ROW_SELECTION", openInCurrentInstance.ToString(), str);
				}
				catch
				{
				}
				try
				{
					x = Settings.Default.ListSplitterDistance;
					iniFileIO.WriteValue("APPLICATION_SETTINGS", "PARTS_JUMPS_SPLITTER_DISTANCE", x.ToString(), str);
				}
				catch
				{
				}
				openInCurrentInstance = Settings.Default.ExpandAllContents;
				iniFileIO.WriteValue("APPLICATION_SETTINGS", "EXPAND_ALL_CONTENTS", openInCurrentInstance.ToString(), str);
				x = Settings.Default.ExpandContentsLevel;
				iniFileIO.WriteValue("APPLICATION_SETTINGS", "EXPAND_CONTENTS_LEVEL", x.ToString(), str);
				openInCurrentInstance = Settings.Default.HankakuZenkakuFlag;
				iniFileIO.WriteValue("APPLICATION_SETTINGS", "SEARCH_HANKAKU_ZENKAKU", openInCurrentInstance.ToString(), str);
				if (flag)
				{
					if (string.IsNullOrEmpty(empty))
					{
						iniFileIO.WriteValue("INI_INFO", "LAST_MODIFIED_DATE", "", str);
					}
					else
					{
						iniFileIO.WriteValue("INI_INFO", "LAST_MODIFIED_DATE", empty, str);
					}
				}
			}
			catch
			{
			}
		}

		public void ZoomFitPage(bool bFitPage)
		{
			this.objFrmPicture.ZoomFitPage(bFitPage);
		}

		private delegate void EnableAddMemoDelegate(bool value);

		private delegate void GSCMenuItemsDelegate();

		private delegate void GSCToolBarItemsDelegate();

		private delegate void HidePartsListDelegate();

		private delegate void HidePictureDelegate();

		private delegate void HideSelectionListDelegate();

		public delegate void LoadDataDirectDelegate();

		private delegate void LoadDataFromNodeDelegate(XmlNode xNode);

		public delegate void SetArgumentsDelegate(string[] args);

		public delegate void SetCurrentServerIDDelegate(int curServerId);

		private delegate void ShowPartsListDelegate();

		private delegate void ShowPictureDelegate();

		private delegate void ShowSelectionListDelegate();

		private delegate void UpdateStatusDelegate(string status);

		private delegate void UpdateViewerTitleDelegate();
	}
}
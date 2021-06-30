using GSPcLocalViewer;
using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer.frmPrint
{
	public class frmPrint : Form
	{
		private IContainer components;

		private PageSetupDialog pageSetupDialog1;

		private RadioButton radio_All;

		private RadioButton radio_CurrentPage;

		private RadioButton radio_pageRange;

		private HelpProvider helpProvider1;

		private ToolTip toolTip1;

		private Label lblName;

		private ComboBox cmbPrintersList;

		private Label lblStatus;

		private Label lblPrinterStatus;

		private Panel pnlForm;

		private Panel pnlControl;

		private Panel pnlPrinter;

		private Label lblPrinter;

		private Panel pnlPrintRange;

		private Panel pnlSelection;

		private Panel pnlOptions;

		private Panel pnlLblPrinter;

		private Label lblPrinterLine;

		private ProgressBar previewProgressbarbar;

		private Panel pnlOrientationZoom;

		private Panel pnlZoom;

		private Panel pnlLblZoom;

		private Label lblZoom;

		private RadioButton radio_HalfPage;

		private CheckBox chkPictureZoom;

		private RadioButton radio_FitPage;

		private Panel pnlOrientation;

		private RadioButton radioLandscape;

		private RadioButton radio_Portrait;

		private Panel pnlLblOrientation;

		private Label lblOrientationLine;

		private Label lblOrientation;

		private Panel pnlSettingsSplitting;

		private Panel pnlSplitPrinting;

		private Panel panel2;

		private Label label1;

		private Label lblSplitPrinting;

		private Label lblSplittingOption;

		private ComboBox cmbSplittingOption;

		private Panel pnlSettings;

		private Panel pnlLblSettings;

		private Label lblSettingsLine;

		private Label lblSettings;

		private ComboBox cmbPaperSize;

		private CheckBox chkHeaderAndFooter;

		private Label lblPaperSize;

		private Panel pblLblSelectionPrinting;

		private Label label3;

		private Label lblSelectionPrinting;

		private Panel pnlLblRangePrinting;

		private Label label2;

		private Label lblRangePrinting;

		private Button btnSplitPattern;

		private CheckBox chkPrintNumbers;

		private CheckBox radio_Picture;

		private CheckBox radio_PartsList;

		private CheckBox radio_SelectionList;

		private Panel pnlFromTo;

		private Button btnProperty;

		private CheckBox checkMaxUtilization;

		private Panel pnlMultiRange;

		public Button btnPreview;

		public CheckBox chkPrintPicMemo;

		private Panel pnlButtons;

		private Button btnCancel;

		public Button btnPrint;

		private Panel pnlGridView;

		public DataGridView dgvPrintListPrintFrm;

		private Panel panel3;

		private Panel panel1;

		private Panel pnlFrmAndTo;

		private TextBox txtFrom;

		private TextBox txtTo;

		private Label lblTo;

		private Label lblFrom;

		public frmViewer frmParent;

		private PreviewManager objPrintManager;

		public frmPageSpecified objPageSpecity;

		private bool bMultiRange;

		private bool bMuliRageKey;

		private string strMultiRngStart;

		private string strMultiRngEnd;

		private bool bPrintPicMemo;

		public static string selectedPrinter;

		private static bool rangePrinting;

		private string[] pStatus;

		private string str_PaperUtilization = string.Empty;

		public string PageOrientation = "Portrait";

		public int pageSplitCount = 1;

		public System.Drawing.Printing.PaperSize PaperSize;

		public string paperSizesList;

		public string defaultPaperName;

		public string paperUtilization;

		public string copyrightPrinitng;

		public string copyRightField;

		public string dateFormat;

		public string splitPrinting;

		public string PrintZoom = "FitPage";

		public static bool printCurrentPage;

		public static bool printPicture;

		public static bool printPartList;

		public static string strExortdImgPath;

		public static string strExportdImgZoom;

		public static int intExportdImgRotationAngle;

		public static int[] arrExportdImgZoomFactor;

		public static string strExportImagePathJpg;

		public frmPreviewProcessing objPreviewProcessingDlg;

		private string strDuplicatePrinting = "ON";

		private string strHeaderFooterVisibility = "ON";

		private PrinterSettings CurrentPrintSettings = new PrinterSettings();

		public string PrinterName
		{
			get
			{
				return GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter;
			}
			set
			{
				GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter = value;
			}
		}

		public bool PrintPartsList
		{
			get
			{
				return GSPcLocalViewer.frmPrint.frmPrint.printPartList;
			}
			set
			{
				GSPcLocalViewer.frmPrint.frmPrint.printPartList = value;
			}
		}

		public bool PrintPicture
		{
			get
			{
				return GSPcLocalViewer.frmPrint.frmPrint.printPicture;
			}
			set
			{
				GSPcLocalViewer.frmPrint.frmPrint.printPicture = value;
			}
		}

		public bool printRangePages
		{
			get
			{
				return GSPcLocalViewer.frmPrint.frmPrint.rangePrinting;
			}
		}

		static frmPrint()
		{
			GSPcLocalViewer.frmPrint.frmPrint.rangePrinting = false;
			GSPcLocalViewer.frmPrint.frmPrint.printCurrentPage = true;
			GSPcLocalViewer.frmPrint.frmPrint.printPicture = true;
			GSPcLocalViewer.frmPrint.frmPrint.printPartList = true;
			GSPcLocalViewer.frmPrint.frmPrint.strExortdImgPath = string.Empty;
			GSPcLocalViewer.frmPrint.frmPrint.strExportdImgZoom = string.Empty;
			GSPcLocalViewer.frmPrint.frmPrint.intExportdImgRotationAngle = 0;
			GSPcLocalViewer.frmPrint.frmPrint.strExportImagePathJpg = string.Empty;
		}

		public frmPrint(frmViewer objFrmViewer, int iPrintType)
		{
			this.InitializeComponent();
			try
			{
				this.checkMaxUtilization.Checked = false;
				this.frmParent = objFrmViewer;
				this.ReadPaperSizesFromIni();
				if (this.cmbSplittingOption.Items.Count == 0)
				{
					this.addSplittingOptions();
				}
				this.selectSplittingOption(this.pageSplitCount);
				if (iPrintType == -1)
				{
					this.radio_PartsList.Checked = true;
					this.radio_Picture.Checked = true;
					this.radio_SelectionList.Checked = true;
				}
				else if (iPrintType == 0)
				{
					if (this.frmParent.sBookType.ToLower().Trim() == "gsp")
					{
						if (this.str_PaperUtilization.ToLower().Trim() != "maximum")
						{
							this.checkMaxUtilization.Checked = false;
						}
						else
						{
							this.checkMaxUtilization.Enabled = true;
							this.checkMaxUtilization.Checked = true;
						}
					}
					if (this.frmParent.sBookType.ToLower().Trim() == "gsc")
					{
						if (this.str_PaperUtilization.ToLower().Trim() == "maximum")
						{
							this.checkMaxUtilization.Checked = true;
							this.radio_Portrait.Enabled = false;
							this.radioLandscape.Enabled = false;
						}
						else if (this.str_PaperUtilization.ToLower().Trim() == "default")
						{
							this.checkMaxUtilization.Checked = false;
							this.radio_Portrait.Enabled = true;
							this.radioLandscape.Enabled = true;
						}
					}
					this.radio_PartsList.Checked = true;
					this.radio_Picture.Checked = true;
					this.radio_SelectionList.Checked = false;
				}
				else if (iPrintType == 1)
				{
					this.radio_PartsList.Checked = false;
					this.radio_Picture.Checked = true;
					this.radio_SelectionList.Checked = false;
					this.radioLandscape.Enabled = false;
					this.radio_Portrait.Enabled = false;
				}
				if (iPrintType == 2)
				{
					this.radio_PartsList.Checked = true;
					this.radio_Picture.Checked = false;
					this.radio_SelectionList.Checked = false;
					this.checkMaxUtilization.Enabled = false;
				}
				if (iPrintType == 3)
				{
					this.radio_PartsList.Checked = false;
					this.radio_Picture.Checked = false;
					this.radio_SelectionList.Checked = true;
					this.checkMaxUtilization.Enabled = false;
				}
				if (!this.frmParent.bIsSelectionListPrint)
				{
					this.radio_SelectionList.Checked = false;
					this.radio_SelectionList.Enabled = false;
				}
				this.GetHeaderFooterSettings();
				if (this.strHeaderFooterVisibility.ToUpper() == "OFF")
				{
					this.chkHeaderAndFooter.Enabled = false;
				}
			}
			catch
			{
			}
		}

		private void addSplittingOptions()
		{
			try
			{
				if (this.cmbSplittingOption.Items.Count == 0)
				{
					this.cmbSplittingOption.Items.Add(this.GetResource("1:1", "1:1", ResourceType.COMBO_BOX));
					if (this.splitPrinting.ToLower() != "on")
					{
						this.cmbSplittingOption.Enabled = false;
					}
					else
					{
						this.cmbSplittingOption.Items.Add(this.GetResource("1:2", "1:2", ResourceType.COMBO_BOX));
						this.cmbSplittingOption.Items.Add(this.GetResource("1:4", "1:4", ResourceType.COMBO_BOX));
						this.cmbSplittingOption.Items.Add(this.GetResource("1:8", "1:8", ResourceType.COMBO_BOX));
					}
				}
			}
			catch
			{
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.EnableTreeView(false);
			this.frmParent.Enabled = true;
			base.Close();
		}

		private void btnPreview_Click(object sender, EventArgs e)
		{
			try
			{
				string str = (this.checkMaxUtilization.Checked ? "1" : "0");
				string str1 = "|";
				string empty = string.Empty;
				string str2 = Program.iniServers[this.frmParent.ServerId].sIniKey;
				string[] item = new string[] { Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"], "\\", str2, "\\", this.frmParent.BookPublishingId };
				string str3 = string.Concat(item);
				string text = this.cmbPrintersList.Text;
				string text1 = this.cmbPaperSize.Text;
				string str4 = this.cmbSplittingOption.SelectedIndex.ToString();
				string str5 = (this.chkHeaderAndFooter.Checked ? "1" : "0");
				string str6 = (this.chkPrintNumbers.Checked ? "1" : "0");
				string str7 = (this.radioLandscape.Checked ? "1" : "0");
				string str8 = (this.radio_HalfPage.Checked ? "1" : "0");
				string djVuZoom = this.frmParent.GetDjVuZoom();
				string str9 = (this.chkPictureZoom.Checked ? "1" : "0");
				string zoomFactor = this.GetZoomFactor();
				string empty1 = string.Empty;
				if (!this.radio_CurrentPage.Checked)
				{
					empty1 = (!this.radio_pageRange.Checked ? "2" : "1");
				}
				else
				{
					empty1 = "0";
				}
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				if (!this.bMuliRageKey || !this.bMultiRange || !(empty1.ToUpper() == "1"))
				{
					this.SelectPrintRange(ref empty2, ref empty3);
				}
				else
				{
					this.GetStartAndEndRanges();
					empty2 = this.strMultiRngStart;
					empty3 = this.strMultiRngEnd;
				}
				string str10 = (this.radio_Picture.Checked ? "1" : "0");
				string str11 = (this.radio_PartsList.Checked ? "1" : "0");
				string str12 = (this.radio_SelectionList.Checked ? "1" : "0");
				string str13 = (Settings.Default.SideBySidePrinting ? "1" : "0");
				string @default = Settings.Default.appProxyType;
				string default1 = Settings.Default.appProxyIP;
				string default2 = Settings.Default.appProxyPort;
				string default3 = Settings.Default.appProxyLogin;
				string default4 = Settings.Default.appProxyPassword;
				string default5 = Settings.Default.appProxyTimeOut;
				string str14 = (this.frmParent.BookType.ToUpper().Trim() != "GSP" ? "1" : "0");
				string str15 = (this.copyrightPrinitng.ToLower() == "on" ? "1" : "0");
				string default6 = Settings.Default.appLanguage;
				string empty4 = string.Empty;
				string empty5 = string.Empty;
				this.GetDjVuIdPassword(ref empty4, ref empty5);
				empty = string.Concat(empty, text, str1);
				empty = string.Concat(empty, text1, str1);
				empty = string.Concat(empty, str4, str1);
				empty = string.Concat(empty, str5, str1);
				empty = string.Concat(empty, str6, str1);
				empty = string.Concat(empty, str7, str1);
				empty = string.Concat(empty, str8, str1);
				empty = string.Concat(empty, djVuZoom, str1);
				empty = string.Concat(empty, str9, str1);
				empty = string.Concat(empty, zoomFactor, str1);
				empty = string.Concat(empty, empty1, str1);
				empty = string.Concat(empty, empty2, str1);
				empty = string.Concat(empty, empty3, str1);
				empty = string.Concat(empty, str10, str1);
				empty = string.Concat(empty, str11, str1);
				empty = string.Concat(empty, str12, str1);
				empty = string.Concat(empty, str13, str1);
				empty = string.Concat(empty, @default, str1);
				empty = string.Concat(empty, default1, str1);
				empty = string.Concat(empty, default2, str1);
				empty = string.Concat(empty, default3, str1);
				empty = string.Concat(empty, default4, str1);
				empty = string.Concat(empty, default5, str1);
				empty = string.Concat(empty, str14, str1);
				empty = string.Concat(empty, str15, str1);
				empty = string.Concat(empty, default6, str1);
				empty = string.Concat(empty, empty4, str1);
				empty = string.Concat(empty, empty5, str1);
				empty = string.Concat(empty, this.bPrintPicMemo, str1);
				empty = string.Concat(empty, str);
				empty = string.Concat(empty, str1, this.strDuplicatePrinting);
				if (str10 == "1" || str11 == "1" || str12 == "1")
				{
					if (this.objPreviewProcessingDlg == null)
					{
						this.objPreviewProcessingDlg = new frmPreviewProcessing(this);
					}
					frmViewer _frmViewer = this.frmParent;
					string[] strArrays = new string[] { empty, str3, str2 };
					this.objPrintManager = new PreviewManager(_frmViewer, strArrays, this);
				}
			}
			catch
			{
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			try
			{
				try
				{
					string str = "|";
					string empty = string.Empty;
					string str1 = Program.iniServers[this.frmParent.ServerId].sIniKey;
					string[] item = new string[] { Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"], "\\", str1, "\\", this.frmParent.BookPublishingId };
					string str2 = string.Concat(item);
					System.Drawing.Font @default = Settings.Default.printFont;
					string name = string.Empty;
					float size = 0f;
					name = @default.Name;
					size = @default.Size;
					string text = this.cmbPrintersList.Text;
					string text1 = this.cmbPaperSize.Text;
					string str3 = this.cmbSplittingOption.SelectedIndex.ToString();
					string str4 = (this.chkHeaderAndFooter.Checked ? "1" : "0");
					string str5 = (this.chkPrintNumbers.Checked ? "1" : "0");
					string str6 = (this.radioLandscape.Checked ? "1" : "0");
					string str7 = (this.radio_HalfPage.Checked ? "1" : "0");
					string djVuZoom = this.frmParent.GetDjVuZoom();
					string str8 = (this.chkPictureZoom.Checked ? "1" : "0");
					string zoomFactor = this.GetZoomFactor();
					string empty1 = string.Empty;
					if (!this.radio_CurrentPage.Checked)
					{
						empty1 = (!this.radio_pageRange.Checked ? "2" : "1");
					}
					else
					{
						empty1 = "0";
					}
					string empty2 = string.Empty;
					string empty3 = string.Empty;
					if (!this.bMuliRageKey || !this.bMultiRange || !(empty1.ToUpper() == "1"))
					{
						this.SelectPrintRange(ref empty2, ref empty3);
					}
					else
					{
						this.GetStartAndEndRanges();
						empty2 = this.strMultiRngStart;
						empty3 = this.strMultiRngEnd;
					}
					string str9 = (Program.objAppFeatures.bPrintUsingOcx ? "1" : "0");
					string str10 = (this.radio_Picture.Checked ? "1" : "0");
					string str11 = (this.radio_PartsList.Checked ? "1" : "0");
					string str12 = (this.radio_SelectionList.Checked ? "1" : "0");
					string str13 = (Settings.Default.SideBySidePrinting ? "1" : "0");
					string default1 = Settings.Default.appProxyType;
					string default2 = Settings.Default.appProxyIP;
					string default3 = Settings.Default.appProxyPort;
					string default4 = Settings.Default.appProxyLogin;
					string default5 = Settings.Default.appProxyPassword;
					string default6 = Settings.Default.appProxyTimeOut;
					string str14 = (this.frmParent.BookType.ToUpper().Trim() != "GSP" ? "1" : "0");
					string str15 = (this.copyrightPrinitng.ToLower() == "on" ? "1" : "0");
					string default7 = Settings.Default.appLanguage;
					string empty4 = string.Empty;
					string empty5 = string.Empty;
					string str16 = (this.checkMaxUtilization.Checked ? "1" : "0");
					this.GetDjVuIdPassword(ref empty4, ref empty5);
					string str17 = empty;
					string[] strArrays = new string[] { str17, "\"", text, "\"", str };
					empty = string.Concat(strArrays);
					empty = string.Concat(empty, text1, str);
					empty = string.Concat(empty, str3, str);
					empty = string.Concat(empty, str4, str);
					empty = string.Concat(empty, str5, str);
					empty = string.Concat(empty, str6, str);
					empty = string.Concat(empty, str7, str);
					string str18 = empty;
					string[] strArrays1 = new string[] { str18, "\"", djVuZoom, "\"", str };
					empty = string.Concat(strArrays1);
					empty = string.Concat(empty, str8, str);
					empty = string.Concat(empty, zoomFactor, str);
					empty = string.Concat(empty, empty1, str);
					empty = string.Concat(empty, empty2, str);
					empty = string.Concat(empty, empty3, str);
					empty = string.Concat(empty, str10, str);
					empty = string.Concat(empty, str11, str);
					empty = string.Concat(empty, str12, str);
					empty = string.Concat(empty, str13, str);
					empty = string.Concat(empty, default1, str);
					empty = string.Concat(empty, default2, str);
					empty = string.Concat(empty, default3, str);
					empty = string.Concat(empty, default4, str);
					empty = string.Concat(empty, default5, str);
					empty = string.Concat(empty, default6, str);
					empty = string.Concat(empty, str14, str);
					empty = string.Concat(empty, str15, str);
					empty = string.Concat(empty, default7, str);
					empty = string.Concat(empty, empty4, str);
					empty = string.Concat(empty, empty5, str);
					empty = string.Concat(empty, str16, str);
					string str19 = empty;
					string[] strArrays2 = new string[] { str19, "\"", name, "\"", str };
					empty = string.Concat(strArrays2);
					empty = string.Concat(empty, size, str);
					empty = string.Concat(empty, this.bPrintPicMemo, str);
					empty = string.Concat(empty, str9);
					empty = string.Concat(empty, str, this.strDuplicatePrinting);
					string str20 = empty;
					string[] strArrays3 = new string[] { str20, " \"", str2, "\" \"", str1, "\"" };
					empty = string.Concat(strArrays3);
					if (str10 == "1" || str11 == "1" || str12 == "1")
					{
						if (File.Exists(string.Concat(Application.StartupPath, "\\PrintManager.exe")))
						{
							Process process = new Process();
							ProcessStartInfo processStartInfo = new ProcessStartInfo(string.Concat(Application.StartupPath, "\\PrintManager.exe"), empty)
							{
								UseShellExecute = false,
								RedirectStandardInput = true,
								RedirectStandardError = true,
								RedirectStandardOutput = true
							};
							process.StartInfo = processStartInfo;
							process.Start();
						}
						else
						{
							MessageHandler.ShowError1("Print module not found");
						}
					}
				}
				catch
				{
				}
			}
			finally
			{
				this.btnCancel_Click(null, null);
			}
		}

		private void btnProperty_Click(object sender, EventArgs e)
		{
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				folderPath = string.Concat(folderPath, "\\", Application.ProductName);
				string str = GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter;
				str = Uri.UnescapeDataString(str);
				str = Uri.EscapeDataString(str);
				if (!File.Exists(string.Concat(folderPath, "\\", str, ".bin")))
				{
					IntPtr hdevmode = this.CurrentPrintSettings.GetHdevmode();
					this.CurrentPrintSettings.PrinterName = GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter;
					this.CurrentPrintSettings.DefaultPageSettings.CopyToHdevmode(hdevmode);
				}
				else
				{
					this.ReadDevmode(string.Concat(folderPath, "\\", str, ".bin"));
				}
				this.CurrentPrintSettings = this.OpenPrinterPropertiesDialog(this.CurrentPrintSettings);
			}
			catch
			{
			}
		}

		private void btnSplitPattern_Click(object sender, EventArgs e)
		{
			try
			{
				frmPageSetup _frmPageSetup = new frmPageSetup(this.frmParent, this.cmbSplittingOption.SelectedIndex);
				_frmPageSetup.ShowDialog(this);
			}
			catch
			{
			}
		}

		private void ChangeSelection()
		{
			if (this.checkMaxUtilization.Checked)
			{
				if (this.frmParent.sBookType.ToLower().Trim() == "gsc")
				{
					this.radioLandscape.Enabled = false;
					this.radio_Portrait.Enabled = false;
					Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Maximum");
					return;
				}
				if (this.frmParent.sBookType.ToLower().Trim() != "gsc" && !this.radio_PartsList.Checked && !this.radio_SelectionList.Checked && this.radio_Picture.Checked)
				{
					this.radioLandscape.Enabled = false;
					this.radio_Portrait.Enabled = false;
					Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Maximum");
					return;
				}
				if (!this.radio_Picture.Checked && !this.radio_PartsList.Checked && !this.radio_SelectionList.Checked)
				{
					this.radioLandscape.Enabled = false;
					this.radio_Portrait.Enabled = false;
					Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Maximum");
					return;
				}
			}
			else if (!this.checkMaxUtilization.Checked)
			{
				if (this.frmParent.sBookType.ToLower().Trim() == "gsc")
				{
					this.radioLandscape.Enabled = true;
					this.radio_Portrait.Enabled = true;
					Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Default");
					return;
				}
				if (this.frmParent.sBookType.ToLower().Trim() != "gsc" && !this.radio_PartsList.Checked && !this.radio_SelectionList.Checked && this.radio_Picture.Checked)
				{
					this.radioLandscape.Enabled = true;
					this.radio_Portrait.Enabled = true;
					Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Default");
					return;
				}
				if (this.frmParent.sBookType.ToLower().Trim() != "gsc" && !this.radio_PartsList.Checked && !this.radio_SelectionList.Checked && this.radio_Picture.Checked)
				{
					this.radioLandscape.Enabled = false;
					this.radio_Portrait.Enabled = false;
					Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Default");
					return;
				}
				if (!this.radio_Picture.Checked && !this.radio_PartsList.Checked && !this.radio_SelectionList.Checked)
				{
					this.radioLandscape.Enabled = false;
					this.radio_Portrait.Enabled = false;
					Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Maximum");
				}
			}
		}

		private void checkMaxUtilization_CheckedChanged(object sender, EventArgs e)
		{
			this.ChangeSelection();
		}

		private void chkHeaderAndFooter_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkHeaderAndFooter.Checked)
			{
				Program.iniUserSet.UpdateItem("PRINT_SETTINGS", "HEADER_FOOTER", "ON");
				return;
			}
			if (!this.chkHeaderAndFooter.Checked)
			{
				Program.iniUserSet.UpdateItem("PRINT_SETTINGS", "HEADER_FOOTER", "OFF");
			}
		}

		private void chkPictureZoom_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkPrintNumbers_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void chkPrintPicMemo_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkPrintPicMemo.Checked)
			{
				this.bPrintPicMemo = true;
				return;
			}
			this.bPrintPicMemo = false;
		}

		private void cmbPaperSize_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.GetPaperSize(this.cmbPaperSize.SelectedItem.ToString());
		}

		private void cmbPrintersList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.lblPrinterStatus.Text = this.GetPrinterStatus(this.cmbPrintersList.SelectedItem.ToString());
			GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter = this.cmbPrintersList.SelectedItem.ToString();
		}

		private void cmbSplittingOption_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.cmbSplittingOption.SelectedIndex != 0)
				{
					this.chkPrintNumbers.Checked = true;
					this.chkPrintNumbers.Enabled = true;
				}
				else
				{
					this.chkPrintNumbers.Checked = false;
					this.chkPrintNumbers.Enabled = false;
				}
				if (this.PrintPicture && this.PrintPartsList && this.cmbSplittingOption.SelectedIndex == 0 && this.paperUtilization.ToLower() == "default")
				{
					this.radio_HalfPage.Enabled = true;
				}
				else if (this.PrintPicture && this.PrintPartsList && this.cmbSplittingOption.SelectedIndex == 0 && this.paperUtilization.ToLower() == "maximum")
				{
					this.radio_HalfPage.Enabled = true;
				}
				else if (this.cmbSplittingOption.SelectedIndex == 0)
				{
					this.PrintZoom = "FitPage";
					this.radio_FitPage.Checked = true;
					this.radio_HalfPage.Enabled = false;
					this.radio_CurrentPage.Checked = true;
				}
				else
				{
					this.radio_HalfPage.Enabled = false;
					this.PrintZoom = "FitPage";
					this.radio_CurrentPage.Checked = true;
					this.radio_FitPage.Checked = true;
				}
				this.radio_All.Enabled = this.cmbSplittingOption.SelectedIndex == 0;
				this.radio_pageRange.Enabled = this.cmbSplittingOption.SelectedIndex == 0;
				if (this.cmbSplittingOption.SelectedIndex == 0)
				{
					this.pageSplitCount = 1;
				}
				else if (this.cmbSplittingOption.SelectedIndex == 1)
				{
					this.pageSplitCount = 2;
				}
				else if (this.cmbSplittingOption.SelectedIndex == 2)
				{
					this.pageSplitCount = 4;
				}
				else if (this.cmbSplittingOption.SelectedIndex == 3)
				{
					this.pageSplitCount = 8;
				}
			}
			catch (Exception exception)
			{
				exception.Message.ToString();
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

		[DllImport("winspool.Drv", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.None, EntryPoint="DocumentPropertiesW", ExactSpelling=true, SetLastError=true)]
		private static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, string pDeviceNameg, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

		private void frmPrint_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				GSPcLocalViewer.frmPrint.frmPrint.printPicture = true;
				GSPcLocalViewer.frmPrint.frmPrint.printPartList = true;
				this.frmParent.EnableTreeView(false);
				this.frmParent.Enabled = true;
				this.objPrintManager.Dispose();
				if (!this.objPageSpecity.IsDisposed)
				{
					this.objPageSpecity.Close();
				}
			}
			catch
			{
			}
		}

		private void frmPrint_Load(object sender, EventArgs e)
		{
			try
			{
				this.ShowHideMultiRange();
				if (!this.bMuliRageKey)
				{
					this.pnlFrmAndTo.Visible = true;
					this.pnlGridView.Visible = false;
				}
				else if (!this.bMultiRange)
				{
					this.pnlFrmAndTo.Visible = true;
					this.pnlGridView.Visible = false;
				}
				else
				{
					this.pnlFrmAndTo.Visible = false;
					this.pnlGridView.Visible = true;
					this.initilizePrintListGrid();
				}
				if (!Settings.Default.EnableLocalMemo)
				{
					this.chkPrintPicMemo.Visible = false;
				}
				this.GetPrintingType();
				this.LoadResources();
				this.pStatus = new string[] { this.GetResource("Other", "STATUSOTHER", ResourceType.LABEL), this.GetResource("Unknown", "STATUSUNKNOWN", ResourceType.LABEL), this.GetResource("Ready", "STATUSREADY", ResourceType.LABEL), this.GetResource("Printing", "STATUSPRINTING", ResourceType.LABEL), this.GetResource("WarmUp", "STATUSWARMUP", ResourceType.LABEL), this.GetResource("Stopped Printing", "STATUSSTOPPEDPRINTING", ResourceType.LABEL), this.GetResource("Offline", "STATUSOFFLINE", ResourceType.LABEL), null };
				if (this.frmParent.sBookType.ToLower().Trim() == "gsp")
				{
					if (this.str_PaperUtilization.ToLower().Trim() != "maximum")
					{
						this.checkMaxUtilization.Checked = false;
					}
					else
					{
						this.checkMaxUtilization.Checked = true;
					}
				}
				if (this.frmParent.sBookType.ToLower().Trim() == "gsc")
				{
					if (this.str_PaperUtilization.ToLower().Trim() == "maximum")
					{
						this.checkMaxUtilization.Checked = true;
						this.radio_Portrait.Enabled = false;
						this.radioLandscape.Enabled = false;
					}
					else if (this.str_PaperUtilization.ToLower().Trim() == "default")
					{
						this.checkMaxUtilization.Checked = false;
						this.radio_Portrait.Enabled = true;
						this.radioLandscape.Enabled = true;
					}
				}
				foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
				{
					this.cmbPrintersList.Items.Add(installedPrinter.ToString());
				}
				PrinterSettings printerSetting = new PrinterSettings();
				this.cmbPrintersList.Text = printerSetting.PrinterName;
				GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter = this.cmbPrintersList.Text;
				if (this.frmParent.BookType.ToUpper().Trim() == "GSC")
				{
					this.radio_SelectionList.Enabled = false;
					this.radio_PartsList.Enabled = false;
					this.radio_SelectionList.Checked = false;
					this.radio_PartsList.Checked = false;
					this.radio_Picture.Checked = true;
					this.radio_HalfPage.Checked = false;
					this.radio_HalfPage.Enabled = false;
				}
				if (this.cmbSplittingOption.Items.Count == 0)
				{
					this.addSplittingOptions();
				}
				this.selectSplittingOption(this.pageSplitCount);
				this.previewProgressbarbar.Value = 0;
				if (this.frmParent.BookType.ToUpper().Trim() == "GSC" && this.paperUtilization.ToUpper() == "MAXIMUM")
				{
					this.radio_Portrait.Enabled = false;
					this.radioLandscape.Enabled = false;
				}
				this.radio_All.Visible = Program.objAppFeatures.bPrintAll;
				this.radio_pageRange.Visible = Program.objAppFeatures.bPrintRange;
				this.txtFrom.Visible = Program.objAppFeatures.bPrintRange;
				this.txtTo.Visible = Program.objAppFeatures.bPrintRange;
				this.lblFrom.Visible = Program.objAppFeatures.bPrintRange;
				this.lblTo.Visible = Program.objAppFeatures.bPrintRange;
				string item = "";
				if (Settings.Default.appLanguage != null)
				{
					item = Program.iniUserSet.items["PRINT_SETTINGS", "HEADER_FOOTER"];
				}
				if (item == null || item == "")
				{
					this.chkHeaderAndFooter.Checked = true;
				}
				else if (item.ToLower() != "off")
				{
					this.chkHeaderAndFooter.Checked = true;
				}
				else
				{
					this.chkHeaderAndFooter.Checked = false;
				}
				if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
				{
					this.pnlSplitPrinting.Enabled = false;
					this.pnlZoom.Enabled = false;
					this.pnlOrientation.Enabled = false;
					this.pnlSettings.Enabled = false;
					this.pnlSplitPrinting.Enabled = false;
					this.btnPreview.Enabled = false;
				}
			}
			catch
			{
			}
		}

		private void GetDjVuIdPassword(ref string sDjVuId, ref string sDjVuPassword)
		{
			try
			{
				AES aE = new AES();
				sDjVuId = string.Empty;
				sDjVuPassword = string.Empty;
				string item = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"];
				string str = aE.DLLDecode(item, "0123456789ABCDEF");
				if (str.Contains("|"))
				{
					sDjVuId = str.Substring(0, str.IndexOf("|"));
					sDjVuPassword = str.Substring(str.IndexOf("|") + 1, str.Length - (str.IndexOf("|") + 1));
				}
			}
			catch
			{
			}
		}

		private void GetHeaderFooterSettings()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.p_ServerId].sIniKey, ".ini");
				ArrayList arrayLists = new ArrayList();
				IniFileIO iniFileIO = new IniFileIO();
				arrayLists = iniFileIO.GetKeys(str, "PRINTER_SETTINGS");
				int num = 0;
				while (num < arrayLists.Count)
				{
					string keyValue = iniFileIO.GetKeyValue("PRINTER_SETTINGS", arrayLists[num].ToString(), str);
					if (arrayLists[num].ToString() != "HEADERFOOTER_PRINTING")
					{
						num++;
					}
					else
					{
						this.strHeaderFooterVisibility = keyValue.ToString().ToUpper().Trim();
						break;
					}
				}
			}
			catch (Exception exception)
			{
				this.strHeaderFooterVisibility = "ON";
			}
		}

		public void GetPaperSize(string PageName)
		{
			if (this.PaperSize != null && this.PaperSize.PaperName == PageName)
			{
				return;
			}
			PrintDocument printDocument = new PrintDocument();
			printDocument.PrinterSettings.PrinterName = this.PrinterName;
			for (int i = 0; i < printDocument.PrinterSettings.PaperSizes.Count; i++)
			{
				if (printDocument.PrinterSettings.PaperSizes[i].PaperName.Contains(PageName))
				{
					this.PaperSize = printDocument.PrinterSettings.PaperSizes[i];
					return;
				}
			}
		}

		public string GetPrinterStatus(string PrinterName)
		{
			string str = "";
			try
			{
				string str1 = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", PrinterName);
				using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = (new ManagementObjectSearcher(str1)).Get().GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						ManagementObject current = (ManagementObject)enumerator.Current;
						str = this.pStatus[Convert.ToInt32(current["PrinterStatus"].ToString()) - 1];
					}
				}
			}
			catch
			{
			}
			return str;
		}

		private void GetPrintingType()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.p_ServerId].sIniKey, ".ini");
				ArrayList arrayLists = new ArrayList();
				arrayLists = (new IniFileIO()).GetKeys(str, "PRINTER_SETTINGS");
				int num = 0;
				while (num < arrayLists.Count)
				{
					IniFileIO iniFileIO = new IniFileIO();
					string keyValue = iniFileIO.GetKeyValue("PRINTER_SETTINGS", arrayLists[num].ToString(), str);
					if (arrayLists[num].ToString() != "DUPLICATE_PRINTING")
					{
						num++;
					}
					else
					{
						this.strDuplicatePrinting = keyValue.ToString().ToUpper().Trim();
						break;
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='PRINT']");
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

		private void GetStartAndEndRanges()
		{
			this.strMultiRngStart = string.Empty;
			this.strMultiRngEnd = string.Empty;
			try
			{
				for (int i = 0; i < this.dgvPrintListPrintFrm.Rows.Count; i++)
				{
					string empty = string.Empty;
					string str = string.Empty;
					empty = this.dgvPrintListPrintFrm.Rows[i].Cells[1].Tag.ToString();
					str = this.dgvPrintListPrintFrm.Rows[i].Cells[2].Tag.ToString();
					if (this.SwapPrintRange(ref empty, ref str) != 1)
					{
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint = this;
						_frmPrint.strMultiRngStart = string.Concat(_frmPrint.strMultiRngStart, empty);
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint1 = this;
						_frmPrint1.strMultiRngStart = string.Concat(_frmPrint1.strMultiRngStart, "*");
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint2 = this;
						_frmPrint2.strMultiRngEnd = string.Concat(_frmPrint2.strMultiRngEnd, str);
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint3 = this;
						_frmPrint3.strMultiRngEnd = string.Concat(_frmPrint3.strMultiRngEnd, "*");
					}
					else
					{
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint4 = this;
						_frmPrint4.strMultiRngStart = string.Concat(_frmPrint4.strMultiRngStart, str);
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint5 = this;
						_frmPrint5.strMultiRngStart = string.Concat(_frmPrint5.strMultiRngStart, "*");
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint6 = this;
						_frmPrint6.strMultiRngEnd = string.Concat(_frmPrint6.strMultiRngEnd, empty);
						GSPcLocalViewer.frmPrint.frmPrint _frmPrint7 = this;
						_frmPrint7.strMultiRngEnd = string.Concat(_frmPrint7.strMultiRngEnd, "*");
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private string GetZoomFactor()
		{
			string str;
			try
			{
				string empty = string.Empty;
				int[] imageZoom = this.frmParent.GetImageZoom();
				if ((int)imageZoom.Length != 8)
				{
					str = "0-0-0-0-0-0-0-0";
				}
				else
				{
					for (int i = 0; i < (int)imageZoom.Length; i++)
					{
						empty = string.Concat(empty, imageZoom[i].ToString());
						if (i != (int)imageZoom.Length - 1)
						{
							empty = string.Concat(empty, ",");
						}
					}
					str = empty;
				}
			}
			catch
			{
				str = "0-0-0-0-0-0-0-0";
			}
			return str;
		}

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=true)]
		public static extern IntPtr GlobalFree(IntPtr handle);

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=true)]
		public static extern IntPtr GlobalLock(IntPtr handle);

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=true)]
		public static extern IntPtr GlobalUnlock(IntPtr handle);

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.radio_pageRange = new RadioButton();
			this.radio_All = new RadioButton();
			this.radio_CurrentPage = new RadioButton();
			this.pageSetupDialog1 = new PageSetupDialog();
			this.helpProvider1 = new HelpProvider();
			this.toolTip1 = new ToolTip(this.components);
			this.txtFrom = new TextBox();
			this.txtTo = new TextBox();
			this.lblName = new Label();
			this.cmbPrintersList = new ComboBox();
			this.lblStatus = new Label();
			this.lblPrinterStatus = new Label();
			this.pnlForm = new Panel();
			this.pnlOrientationZoom = new Panel();
			this.pnlZoom = new Panel();
			this.pnlLblZoom = new Panel();
			this.lblZoom = new Label();
			this.radio_HalfPage = new RadioButton();
			this.chkPictureZoom = new CheckBox();
			this.radio_FitPage = new RadioButton();
			this.pnlOrientation = new Panel();
			this.checkMaxUtilization = new CheckBox();
			this.radioLandscape = new RadioButton();
			this.radio_Portrait = new RadioButton();
			this.pnlLblOrientation = new Panel();
			this.lblOrientationLine = new Label();
			this.lblOrientation = new Label();
			this.pnlSettingsSplitting = new Panel();
			this.pnlSplitPrinting = new Panel();
			this.chkPrintNumbers = new CheckBox();
			this.btnSplitPattern = new Button();
			this.panel2 = new Panel();
			this.label1 = new Label();
			this.lblSplitPrinting = new Label();
			this.lblSplittingOption = new Label();
			this.cmbSplittingOption = new ComboBox();
			this.pnlSettings = new Panel();
			this.chkPrintPicMemo = new CheckBox();
			this.pnlLblSettings = new Panel();
			this.lblSettingsLine = new Label();
			this.lblSettings = new Label();
			this.cmbPaperSize = new ComboBox();
			this.chkHeaderAndFooter = new CheckBox();
			this.lblPaperSize = new Label();
			this.pnlOptions = new Panel();
			this.pnlSelection = new Panel();
			this.radio_PartsList = new CheckBox();
			this.radio_SelectionList = new CheckBox();
			this.radio_Picture = new CheckBox();
			this.pblLblSelectionPrinting = new Panel();
			this.label3 = new Label();
			this.lblSelectionPrinting = new Label();
			this.pnlPrintRange = new Panel();
			this.pnlLblRangePrinting = new Panel();
			this.label2 = new Label();
			this.lblRangePrinting = new Label();
			this.pnlPrinter = new Panel();
			this.btnProperty = new Button();
			this.btnPreview = new Button();
			this.pnlLblPrinter = new Panel();
			this.lblPrinterLine = new Label();
			this.lblPrinter = new Label();
			this.pnlFromTo = new Panel();
			this.pnlButtons = new Panel();
			this.btnCancel = new Button();
			this.btnPrint = new Button();
			this.pnlMultiRange = new Panel();
			this.pnlGridView = new Panel();
			this.dgvPrintListPrintFrm = new DataGridView();
			this.panel3 = new Panel();
			this.panel1 = new Panel();
			this.pnlFrmAndTo = new Panel();
			this.lblTo = new Label();
			this.lblFrom = new Label();
			this.pnlControl = new Panel();
			this.previewProgressbarbar = new ProgressBar();
			Label label = new Label();
			this.pnlForm.SuspendLayout();
			this.pnlOrientationZoom.SuspendLayout();
			this.pnlZoom.SuspendLayout();
			this.pnlLblZoom.SuspendLayout();
			this.pnlOrientation.SuspendLayout();
			this.pnlLblOrientation.SuspendLayout();
			this.pnlSettingsSplitting.SuspendLayout();
			this.pnlSplitPrinting.SuspendLayout();
			this.panel2.SuspendLayout();
			this.pnlSettings.SuspendLayout();
			this.pnlLblSettings.SuspendLayout();
			this.pnlOptions.SuspendLayout();
			this.pnlSelection.SuspendLayout();
			this.pblLblSelectionPrinting.SuspendLayout();
			this.pnlPrintRange.SuspendLayout();
			this.pnlLblRangePrinting.SuspendLayout();
			this.pnlPrinter.SuspendLayout();
			this.pnlLblPrinter.SuspendLayout();
			this.pnlFromTo.SuspendLayout();
			this.pnlButtons.SuspendLayout();
			this.pnlMultiRange.SuspendLayout();
			this.pnlGridView.SuspendLayout();
			((ISupportInitialize)this.dgvPrintListPrintFrm).BeginInit();
			this.pnlFrmAndTo.SuspendLayout();
			this.pnlControl.SuspendLayout();
			base.SuspendLayout();
			label.BackColor = Color.Transparent;
			label.Dock = DockStyle.Fill;
			label.ForeColor = Color.Blue;
			label.Image = Resources.GroupLine0;
			label.ImageAlign = ContentAlignment.MiddleLeft;
			label.Location = new Point(7, 0);
			label.Name = "lblZoomLine";
			label.Size = new System.Drawing.Size(270, 28);
			label.TabIndex = 15;
			label.TextAlign = ContentAlignment.MiddleLeft;
			this.radio_pageRange.AutoSize = true;
			this.radio_pageRange.ForeColor = Color.Black;
			this.radio_pageRange.Location = new Point(29, 84);
			this.radio_pageRange.Name = "radio_pageRange";
			this.radio_pageRange.Size = new System.Drawing.Size(54, 17);
			this.radio_pageRange.TabIndex = 3;
			this.radio_pageRange.TabStop = true;
			this.radio_pageRange.Text = "Pages";
			this.radio_pageRange.UseVisualStyleBackColor = true;
			this.radio_pageRange.Click += new EventHandler(this.radio_pageRange_Click);
			this.radio_pageRange.CheckedChanged += new EventHandler(this.radio_pageRange_CheckedChanged);
			this.radio_All.AutoSize = true;
			this.radio_All.ForeColor = Color.Black;
			this.radio_All.Location = new Point(29, 58);
			this.radio_All.Name = "radio_All";
			this.radio_All.Size = new System.Drawing.Size(68, 17);
			this.radio_All.TabIndex = 1;
			this.radio_All.Text = "All Pages";
			this.radio_All.UseVisualStyleBackColor = true;
			this.radio_All.CheckedChanged += new EventHandler(this.radio_All_CheckedChanged);
			this.radio_CurrentPage.AutoSize = true;
			this.radio_CurrentPage.Checked = true;
			this.radio_CurrentPage.ForeColor = Color.Black;
			this.radio_CurrentPage.Location = new Point(29, 33);
			this.radio_CurrentPage.Name = "radio_CurrentPage";
			this.radio_CurrentPage.Size = new System.Drawing.Size(89, 17);
			this.radio_CurrentPage.TabIndex = 0;
			this.radio_CurrentPage.TabStop = true;
			this.radio_CurrentPage.Text = "Current Page";
			this.radio_CurrentPage.UseVisualStyleBackColor = true;
			this.radio_CurrentPage.CheckedChanged += new EventHandler(this.radio_CurrentPage_CheckedChanged);
			this.txtFrom.BackColor = Color.White;
			this.txtFrom.BorderStyle = BorderStyle.FixedSingle;
			this.txtFrom.Enabled = false;
			this.txtFrom.Location = new Point(85, 12);
			this.txtFrom.MaxLength = 3;
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.ReadOnly = true;
			this.txtFrom.Size = new System.Drawing.Size(212, 21);
			this.txtFrom.TabIndex = 26;
			this.toolTip1.SetToolTip(this.txtFrom, "Select From Page from List of Pages on left.");
			this.txtTo.BackColor = Color.White;
			this.txtTo.BorderStyle = BorderStyle.FixedSingle;
			this.txtTo.Enabled = false;
			this.txtTo.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtTo.Location = new Point(84, 43);
			this.txtTo.MaxLength = 3;
			this.txtTo.Name = "txtTo";
			this.txtTo.ReadOnly = true;
			this.txtTo.Size = new System.Drawing.Size(212, 21);
			this.txtTo.TabIndex = 28;
			this.toolTip1.SetToolTip(this.txtTo, "Select To Page from List of Pages on left.");
			this.lblName.AutoSize = true;
			this.lblName.ForeColor = Color.Black;
			this.lblName.Location = new Point(29, 34);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(34, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name";
			this.cmbPrintersList.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPrintersList.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.cmbPrintersList.Location = new Point(104, 30);
			this.cmbPrintersList.Name = "cmbPrintersList";
			this.cmbPrintersList.Size = new System.Drawing.Size(241, 21);
			this.cmbPrintersList.TabIndex = 1;
			this.cmbPrintersList.SelectedIndexChanged += new EventHandler(this.cmbPrintersList_SelectedIndexChanged);
			this.lblStatus.AutoSize = true;
			this.lblStatus.ForeColor = Color.Black;
			this.lblStatus.Location = new Point(29, 66);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(38, 13);
			this.lblStatus.TabIndex = 3;
			this.lblStatus.Text = "Status";
			this.lblPrinterStatus.AutoSize = true;
			this.lblPrinterStatus.ForeColor = Color.Black;
			this.lblPrinterStatus.Location = new Point(105, 66);
			this.lblPrinterStatus.Name = "lblPrinterStatus";
			this.lblPrinterStatus.Size = new System.Drawing.Size(0, 13);
			this.lblPrinterStatus.TabIndex = 4;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlOrientationZoom);
			this.pnlForm.Controls.Add(this.pnlSettingsSplitting);
			this.pnlForm.Controls.Add(this.pnlOptions);
			this.pnlForm.Controls.Add(this.pnlPrinter);
			this.pnlForm.Controls.Add(this.pnlFromTo);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(2, 2);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(510, 668);
			this.pnlForm.TabIndex = 5;
			this.pnlOrientationZoom.Controls.Add(this.pnlZoom);
			this.pnlOrientationZoom.Controls.Add(this.pnlOrientation);
			this.pnlOrientationZoom.Dock = DockStyle.Fill;
			this.pnlOrientationZoom.Location = new Point(0, 193);
			this.pnlOrientationZoom.Name = "pnlOrientationZoom";
			this.pnlOrientationZoom.Size = new System.Drawing.Size(508, 104);
			this.pnlOrientationZoom.TabIndex = 30;
			this.pnlZoom.BackColor = Color.White;
			this.pnlZoom.Controls.Add(this.pnlLblZoom);
			this.pnlZoom.Controls.Add(this.radio_HalfPage);
			this.pnlZoom.Controls.Add(this.chkPictureZoom);
			this.pnlZoom.Controls.Add(this.radio_FitPage);
			this.pnlZoom.Dock = DockStyle.Fill;
			this.pnlZoom.Location = new Point(216, 0);
			this.pnlZoom.Name = "pnlZoom";
			this.pnlZoom.Size = new System.Drawing.Size(292, 104);
			this.pnlZoom.TabIndex = 24;
			this.pnlLblZoom.Controls.Add(this.lblZoom);
			this.pnlLblZoom.Controls.Add(label);
			this.pnlLblZoom.Dock = DockStyle.Top;
			this.pnlLblZoom.Location = new Point(0, 0);
			this.pnlLblZoom.Name = "pnlLblZoom";
			this.pnlLblZoom.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblZoom.Size = new System.Drawing.Size(292, 28);
			this.pnlLblZoom.TabIndex = 20;
			this.lblZoom.AutoSize = true;
			this.lblZoom.BackColor = Color.Transparent;
			this.lblZoom.Dock = DockStyle.Left;
			this.lblZoom.ForeColor = Color.Blue;
			this.lblZoom.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblZoom.Location = new Point(7, 0);
			this.lblZoom.Name = "lblZoom";
			this.lblZoom.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblZoom.Size = new System.Drawing.Size(33, 20);
			this.lblZoom.TabIndex = 15;
			this.lblZoom.Text = "Zoom";
			this.lblZoom.TextAlign = ContentAlignment.MiddleLeft;
			this.radio_HalfPage.AutoSize = true;
			this.radio_HalfPage.Enabled = false;
			this.radio_HalfPage.Location = new Point(26, 61);
			this.radio_HalfPage.Name = "radio_HalfPage";
			this.radio_HalfPage.Size = new System.Drawing.Size(71, 17);
			this.radio_HalfPage.TabIndex = 1;
			this.radio_HalfPage.Text = "Half Page";
			this.radio_HalfPage.UseVisualStyleBackColor = true;
			this.radio_HalfPage.CheckedChanged += new EventHandler(this.radio_HalfPage_CheckedChanged);
			this.chkPictureZoom.AutoSize = true;
			this.chkPictureZoom.Location = new Point(26, 84);
			this.chkPictureZoom.Name = "chkPictureZoom";
			this.chkPictureZoom.Size = new System.Drawing.Size(131, 17);
			this.chkPictureZoom.TabIndex = 7;
			this.chkPictureZoom.Text = "Maintain Picture Zoom";
			this.chkPictureZoom.UseVisualStyleBackColor = true;
			this.chkPictureZoom.CheckedChanged += new EventHandler(this.chkPictureZoom_CheckedChanged);
			this.radio_FitPage.AutoSize = true;
			this.radio_FitPage.Checked = true;
			this.radio_FitPage.Location = new Point(26, 38);
			this.radio_FitPage.Name = "radio_FitPage";
			this.radio_FitPage.Size = new System.Drawing.Size(64, 17);
			this.radio_FitPage.TabIndex = 0;
			this.radio_FitPage.TabStop = true;
			this.radio_FitPage.Text = "Fit Page";
			this.radio_FitPage.UseVisualStyleBackColor = true;
			this.radio_FitPage.CheckedChanged += new EventHandler(this.radio_FitPage_CheckedChanged);
			this.pnlOrientation.BackColor = Color.White;
			this.pnlOrientation.Controls.Add(this.checkMaxUtilization);
			this.pnlOrientation.Controls.Add(this.radioLandscape);
			this.pnlOrientation.Controls.Add(this.radio_Portrait);
			this.pnlOrientation.Controls.Add(this.pnlLblOrientation);
			this.pnlOrientation.Dock = DockStyle.Left;
			this.pnlOrientation.Location = new Point(0, 0);
			this.pnlOrientation.Name = "pnlOrientation";
			this.pnlOrientation.Size = new System.Drawing.Size(216, 104);
			this.pnlOrientation.TabIndex = 23;
			this.checkMaxUtilization.AutoSize = true;
			this.checkMaxUtilization.Location = new Point(29, 84);
			this.checkMaxUtilization.Name = "checkMaxUtilization";
			this.checkMaxUtilization.Size = new System.Drawing.Size(150, 17);
			this.checkMaxUtilization.TabIndex = 21;
			this.checkMaxUtilization.Text = "Maximum Paper Utilization";
			this.checkMaxUtilization.UseVisualStyleBackColor = true;
			this.checkMaxUtilization.CheckedChanged += new EventHandler(this.checkMaxUtilization_CheckedChanged);
			this.radioLandscape.AutoSize = true;
			this.radioLandscape.Location = new Point(29, 61);
			this.radioLandscape.Name = "radioLandscape";
			this.radioLandscape.Size = new System.Drawing.Size(76, 17);
			this.radioLandscape.TabIndex = 1;
			this.radioLandscape.Text = "Landscape";
			this.radioLandscape.UseVisualStyleBackColor = true;
			this.radioLandscape.CheckedChanged += new EventHandler(this.radioLandscape_CheckedChanged);
			this.radio_Portrait.AutoSize = true;
			this.radio_Portrait.Checked = true;
			this.radio_Portrait.Location = new Point(29, 38);
			this.radio_Portrait.Name = "radio_Portrait";
			this.radio_Portrait.Size = new System.Drawing.Size(61, 17);
			this.radio_Portrait.TabIndex = 0;
			this.radio_Portrait.TabStop = true;
			this.radio_Portrait.Text = "Portrait";
			this.radio_Portrait.UseVisualStyleBackColor = true;
			this.radio_Portrait.CheckedChanged += new EventHandler(this.radio_Portrait_CheckedChanged);
			this.pnlLblOrientation.Controls.Add(this.lblOrientationLine);
			this.pnlLblOrientation.Controls.Add(this.lblOrientation);
			this.pnlLblOrientation.Dock = DockStyle.Top;
			this.pnlLblOrientation.Location = new Point(0, 0);
			this.pnlLblOrientation.Name = "pnlLblOrientation";
			this.pnlLblOrientation.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblOrientation.Size = new System.Drawing.Size(216, 28);
			this.pnlLblOrientation.TabIndex = 20;
			this.lblOrientationLine.BackColor = Color.Transparent;
			this.lblOrientationLine.Dock = DockStyle.Fill;
			this.lblOrientationLine.ForeColor = Color.Blue;
			this.lblOrientationLine.Image = Resources.GroupLine0;
			this.lblOrientationLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblOrientationLine.Location = new Point(68, 0);
			this.lblOrientationLine.Name = "lblOrientationLine";
			this.lblOrientationLine.Size = new System.Drawing.Size(133, 28);
			this.lblOrientationLine.TabIndex = 15;
			this.lblOrientationLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblOrientation.AutoSize = true;
			this.lblOrientation.BackColor = Color.Transparent;
			this.lblOrientation.Dock = DockStyle.Left;
			this.lblOrientation.ForeColor = Color.Blue;
			this.lblOrientation.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblOrientation.Location = new Point(7, 0);
			this.lblOrientation.Name = "lblOrientation";
			this.lblOrientation.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblOrientation.Size = new System.Drawing.Size(61, 20);
			this.lblOrientation.TabIndex = 14;
			this.lblOrientation.Text = "Orientation";
			this.lblOrientation.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlSettingsSplitting.BackColor = Color.White;
			this.pnlSettingsSplitting.Controls.Add(this.pnlSplitPrinting);
			this.pnlSettingsSplitting.Controls.Add(this.pnlSettings);
			this.pnlSettingsSplitting.Dock = DockStyle.Top;
			this.pnlSettingsSplitting.Location = new Point(0, 85);
			this.pnlSettingsSplitting.Name = "pnlSettingsSplitting";
			this.pnlSettingsSplitting.Size = new System.Drawing.Size(508, 108);
			this.pnlSettingsSplitting.TabIndex = 31;
			this.pnlSplitPrinting.Controls.Add(this.chkPrintNumbers);
			this.pnlSplitPrinting.Controls.Add(this.btnSplitPattern);
			this.pnlSplitPrinting.Controls.Add(this.panel2);
			this.pnlSplitPrinting.Controls.Add(this.lblSplittingOption);
			this.pnlSplitPrinting.Controls.Add(this.cmbSplittingOption);
			this.pnlSplitPrinting.Dock = DockStyle.Fill;
			this.pnlSplitPrinting.Location = new Point(216, 0);
			this.pnlSplitPrinting.Name = "pnlSplitPrinting";
			this.pnlSplitPrinting.Size = new System.Drawing.Size(292, 108);
			this.pnlSplitPrinting.TabIndex = 2;
			this.chkPrintNumbers.AutoSize = true;
			this.chkPrintNumbers.Checked = true;
			this.chkPrintNumbers.CheckState = CheckState.Checked;
			this.chkPrintNumbers.Location = new Point(26, 80);
			this.chkPrintNumbers.Name = "chkPrintNumbers";
			this.chkPrintNumbers.Size = new System.Drawing.Size(120, 17);
			this.chkPrintNumbers.TabIndex = 21;
			this.chkPrintNumbers.Text = "Print Page Numbers";
			this.chkPrintNumbers.UseVisualStyleBackColor = true;
			this.chkPrintNumbers.CheckedChanged += new EventHandler(this.chkPrintNumbers_CheckedChanged);
			this.btnSplitPattern.Location = new Point(174, 76);
			this.btnSplitPattern.Name = "btnSplitPattern";
			this.btnSplitPattern.Size = new System.Drawing.Size(105, 23);
			this.btnSplitPattern.TabIndex = 20;
			this.btnSplitPattern.Text = "Split Pattern";
			this.btnSplitPattern.UseVisualStyleBackColor = true;
			this.btnSplitPattern.Click += new EventHandler(this.btnSplitPattern_Click);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.lblSplitPrinting);
			this.panel2.Dock = DockStyle.Top;
			this.panel2.Location = new Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.panel2.Size = new System.Drawing.Size(292, 28);
			this.panel2.TabIndex = 19;
			this.label1.BackColor = Color.Transparent;
			this.label1.Dock = DockStyle.Fill;
			this.label1.ForeColor = Color.Blue;
			this.label1.Image = Resources.GroupLine0;
			this.label1.ImageAlign = ContentAlignment.MiddleLeft;
			this.label1.Location = new Point(73, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(204, 28);
			this.label1.TabIndex = 15;
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.lblSplitPrinting.AutoSize = true;
			this.lblSplitPrinting.BackColor = Color.Transparent;
			this.lblSplitPrinting.Dock = DockStyle.Left;
			this.lblSplitPrinting.ForeColor = Color.Blue;
			this.lblSplitPrinting.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblSplitPrinting.Location = new Point(7, 0);
			this.lblSplitPrinting.Name = "lblSplitPrinting";
			this.lblSplitPrinting.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblSplitPrinting.Size = new System.Drawing.Size(66, 20);
			this.lblSplitPrinting.TabIndex = 12;
			this.lblSplitPrinting.Text = "Split Printing";
			this.lblSplitPrinting.TextAlign = ContentAlignment.MiddleLeft;
			this.lblSplittingOption.AutoSize = true;
			this.lblSplittingOption.Location = new Point(26, 43);
			this.lblSplittingOption.Name = "lblSplittingOption";
			this.lblSplittingOption.Size = new System.Drawing.Size(84, 13);
			this.lblSplittingOption.TabIndex = 12;
			this.lblSplittingOption.Text = "Splitting Option:";
			this.cmbSplittingOption.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbSplittingOption.FormattingEnabled = true;
			this.cmbSplittingOption.Location = new Point(174, 40);
			this.cmbSplittingOption.Name = "cmbSplittingOption";
			this.cmbSplittingOption.Size = new System.Drawing.Size(105, 21);
			this.cmbSplittingOption.TabIndex = 13;
			this.cmbSplittingOption.SelectedIndexChanged += new EventHandler(this.cmbSplittingOption_SelectedIndexChanged);
			this.pnlSettings.Controls.Add(this.chkPrintPicMemo);
			this.pnlSettings.Controls.Add(this.pnlLblSettings);
			this.pnlSettings.Controls.Add(this.cmbPaperSize);
			this.pnlSettings.Controls.Add(this.chkHeaderAndFooter);
			this.pnlSettings.Controls.Add(this.lblPaperSize);
			this.pnlSettings.Dock = DockStyle.Left;
			this.pnlSettings.Location = new Point(0, 0);
			this.pnlSettings.Name = "pnlSettings";
			this.pnlSettings.Size = new System.Drawing.Size(216, 108);
			this.pnlSettings.TabIndex = 1;
			this.chkPrintPicMemo.AutoSize = true;
			this.chkPrintPicMemo.Location = new Point(29, 80);
			this.chkPrintPicMemo.Name = "chkPrintPicMemo";
			this.chkPrintPicMemo.Size = new System.Drawing.Size(115, 17);
			this.chkPrintPicMemo.TabIndex = 20;
			this.chkPrintPicMemo.Text = "Print Picture Memo";
			this.chkPrintPicMemo.UseVisualStyleBackColor = true;
			this.chkPrintPicMemo.CheckedChanged += new EventHandler(this.chkPrintPicMemo_CheckedChanged);
			this.pnlLblSettings.Controls.Add(this.lblSettingsLine);
			this.pnlLblSettings.Controls.Add(this.lblSettings);
			this.pnlLblSettings.Dock = DockStyle.Top;
			this.pnlLblSettings.Location = new Point(0, 0);
			this.pnlLblSettings.Name = "pnlLblSettings";
			this.pnlLblSettings.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblSettings.Size = new System.Drawing.Size(216, 28);
			this.pnlLblSettings.TabIndex = 19;
			this.lblSettingsLine.BackColor = Color.Transparent;
			this.lblSettingsLine.Dock = DockStyle.Fill;
			this.lblSettingsLine.ForeColor = Color.Blue;
			this.lblSettingsLine.Image = Resources.GroupLine0;
			this.lblSettingsLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblSettingsLine.Location = new Point(53, 0);
			this.lblSettingsLine.Name = "lblSettingsLine";
			this.lblSettingsLine.Size = new System.Drawing.Size(148, 28);
			this.lblSettingsLine.TabIndex = 15;
			this.lblSettingsLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblSettings.AutoSize = true;
			this.lblSettings.BackColor = Color.Transparent;
			this.lblSettings.Dock = DockStyle.Left;
			this.lblSettings.ForeColor = Color.Blue;
			this.lblSettings.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblSettings.Location = new Point(7, 0);
			this.lblSettings.Name = "lblSettings";
			this.lblSettings.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblSettings.Size = new System.Drawing.Size(46, 20);
			this.lblSettings.TabIndex = 12;
			this.lblSettings.Text = "Settings";
			this.lblSettings.TextAlign = ContentAlignment.MiddleLeft;
			this.cmbPaperSize.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbPaperSize.FormattingEnabled = true;
			this.cmbPaperSize.Location = new Point(104, 40);
			this.cmbPaperSize.Name = "cmbPaperSize";
			this.cmbPaperSize.Size = new System.Drawing.Size(72, 21);
			this.cmbPaperSize.TabIndex = 11;
			this.cmbPaperSize.Visible = false;
			this.cmbPaperSize.SelectedIndexChanged += new EventHandler(this.cmbPaperSize_SelectedIndexChanged);
			this.chkHeaderAndFooter.AutoSize = true;
			this.chkHeaderAndFooter.Checked = true;
			this.chkHeaderAndFooter.CheckState = CheckState.Checked;
			this.chkHeaderAndFooter.Location = new Point(29, 40);
			this.chkHeaderAndFooter.Name = "chkHeaderAndFooter";
			this.chkHeaderAndFooter.Size = new System.Drawing.Size(142, 17);
			this.chkHeaderAndFooter.TabIndex = 7;
			this.chkHeaderAndFooter.Text = "Print Header and Footer";
			this.chkHeaderAndFooter.UseVisualStyleBackColor = true;
			this.chkHeaderAndFooter.CheckedChanged += new EventHandler(this.chkHeaderAndFooter_CheckedChanged);
			this.lblPaperSize.AutoSize = true;
			this.lblPaperSize.Location = new Point(29, 43);
			this.lblPaperSize.Name = "lblPaperSize";
			this.lblPaperSize.Size = new System.Drawing.Size(61, 13);
			this.lblPaperSize.TabIndex = 10;
			this.lblPaperSize.Text = "Paper Size:";
			this.lblPaperSize.Visible = false;
			this.pnlOptions.Controls.Add(this.pnlSelection);
			this.pnlOptions.Controls.Add(this.pnlPrintRange);
			this.pnlOptions.Dock = DockStyle.Bottom;
			this.pnlOptions.Location = new Point(0, 297);
			this.pnlOptions.Name = "pnlOptions";
			this.pnlOptions.Size = new System.Drawing.Size(508, 114);
			this.pnlOptions.TabIndex = 24;
			this.pnlSelection.BackColor = Color.White;
			this.pnlSelection.Controls.Add(this.radio_PartsList);
			this.pnlSelection.Controls.Add(this.radio_SelectionList);
			this.pnlSelection.Controls.Add(this.radio_Picture);
			this.pnlSelection.Controls.Add(this.pblLblSelectionPrinting);
			this.pnlSelection.Dock = DockStyle.Fill;
			this.pnlSelection.Location = new Point(216, 0);
			this.pnlSelection.Name = "pnlSelection";
			this.pnlSelection.Size = new System.Drawing.Size(292, 114);
			this.pnlSelection.TabIndex = 22;
			this.radio_PartsList.AutoSize = true;
			this.radio_PartsList.Checked = true;
			this.radio_PartsList.CheckState = CheckState.Checked;
			this.radio_PartsList.Location = new Point(26, 58);
			this.radio_PartsList.Name = "radio_PartsList";
			this.radio_PartsList.Size = new System.Drawing.Size(67, 17);
			this.radio_PartsList.TabIndex = 24;
			this.radio_PartsList.Text = "PartsList";
			this.radio_PartsList.UseVisualStyleBackColor = true;
			this.radio_PartsList.CheckedChanged += new EventHandler(this.radio_PartsList_CheckedChanged_1);
			this.radio_SelectionList.AutoSize = true;
			this.radio_SelectionList.Checked = true;
			this.radio_SelectionList.CheckState = CheckState.Checked;
			this.radio_SelectionList.Location = new Point(26, 84);
			this.radio_SelectionList.Name = "radio_SelectionList";
			this.radio_SelectionList.Size = new System.Drawing.Size(85, 17);
			this.radio_SelectionList.TabIndex = 23;
			this.radio_SelectionList.Text = "SelectionList";
			this.radio_SelectionList.UseVisualStyleBackColor = true;
			this.radio_SelectionList.CheckedChanged += new EventHandler(this.radio_SelectionList_CheckedChanged);
			this.radio_Picture.AutoSize = true;
			this.radio_Picture.Checked = true;
			this.radio_Picture.CheckState = CheckState.Checked;
			this.radio_Picture.Location = new Point(26, 33);
			this.radio_Picture.Name = "radio_Picture";
			this.radio_Picture.Size = new System.Drawing.Size(59, 17);
			this.radio_Picture.TabIndex = 22;
			this.radio_Picture.Text = "Picture";
			this.radio_Picture.UseVisualStyleBackColor = true;
			this.radio_Picture.CheckedChanged += new EventHandler(this.radio_Picture_CheckedChanged_1);
			this.pblLblSelectionPrinting.Controls.Add(this.label3);
			this.pblLblSelectionPrinting.Controls.Add(this.lblSelectionPrinting);
			this.pblLblSelectionPrinting.Dock = DockStyle.Top;
			this.pblLblSelectionPrinting.Location = new Point(0, 0);
			this.pblLblSelectionPrinting.Name = "pblLblSelectionPrinting";
			this.pblLblSelectionPrinting.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pblLblSelectionPrinting.Size = new System.Drawing.Size(292, 28);
			this.pblLblSelectionPrinting.TabIndex = 21;
			this.label3.BackColor = Color.Transparent;
			this.label3.Dock = DockStyle.Fill;
			this.label3.ForeColor = Color.Blue;
			this.label3.Image = Resources.GroupLine0;
			this.label3.ImageAlign = ContentAlignment.MiddleLeft;
			this.label3.Location = new Point(96, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(181, 28);
			this.label3.TabIndex = 15;
			this.label3.TextAlign = ContentAlignment.MiddleLeft;
			this.lblSelectionPrinting.AutoSize = true;
			this.lblSelectionPrinting.BackColor = Color.Transparent;
			this.lblSelectionPrinting.Dock = DockStyle.Left;
			this.lblSelectionPrinting.ForeColor = Color.Blue;
			this.lblSelectionPrinting.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblSelectionPrinting.Location = new Point(7, 0);
			this.lblSelectionPrinting.Name = "lblSelectionPrinting";
			this.lblSelectionPrinting.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblSelectionPrinting.Size = new System.Drawing.Size(89, 20);
			this.lblSelectionPrinting.TabIndex = 14;
			this.lblSelectionPrinting.Text = "Selection Printing";
			this.lblSelectionPrinting.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlPrintRange.BackColor = Color.White;
			this.pnlPrintRange.Controls.Add(this.pnlLblRangePrinting);
			this.pnlPrintRange.Controls.Add(this.radio_CurrentPage);
			this.pnlPrintRange.Controls.Add(this.radio_All);
			this.pnlPrintRange.Controls.Add(this.radio_pageRange);
			this.pnlPrintRange.Dock = DockStyle.Left;
			this.pnlPrintRange.Location = new Point(0, 0);
			this.pnlPrintRange.Name = "pnlPrintRange";
			this.pnlPrintRange.Size = new System.Drawing.Size(216, 114);
			this.pnlPrintRange.TabIndex = 21;
			this.pnlLblRangePrinting.Controls.Add(this.label2);
			this.pnlLblRangePrinting.Controls.Add(this.lblRangePrinting);
			this.pnlLblRangePrinting.Dock = DockStyle.Top;
			this.pnlLblRangePrinting.Location = new Point(0, 0);
			this.pnlLblRangePrinting.Name = "pnlLblRangePrinting";
			this.pnlLblRangePrinting.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblRangePrinting.Size = new System.Drawing.Size(216, 28);
			this.pnlLblRangePrinting.TabIndex = 21;
			this.label2.BackColor = Color.Transparent;
			this.label2.Dock = DockStyle.Fill;
			this.label2.ForeColor = Color.Blue;
			this.label2.Image = Resources.GroupLine0;
			this.label2.ImageAlign = ContentAlignment.MiddleLeft;
			this.label2.Location = new Point(84, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(117, 28);
			this.label2.TabIndex = 15;
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.lblRangePrinting.AutoSize = true;
			this.lblRangePrinting.BackColor = Color.Transparent;
			this.lblRangePrinting.Dock = DockStyle.Left;
			this.lblRangePrinting.ForeColor = Color.Blue;
			this.lblRangePrinting.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblRangePrinting.Location = new Point(7, 0);
			this.lblRangePrinting.Name = "lblRangePrinting";
			this.lblRangePrinting.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblRangePrinting.Size = new System.Drawing.Size(77, 20);
			this.lblRangePrinting.TabIndex = 14;
			this.lblRangePrinting.Text = "Range Printing";
			this.lblRangePrinting.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlPrinter.BackColor = Color.White;
			this.pnlPrinter.Controls.Add(this.btnProperty);
			this.pnlPrinter.Controls.Add(this.btnPreview);
			this.pnlPrinter.Controls.Add(this.pnlLblPrinter);
			this.pnlPrinter.Controls.Add(this.lblPrinterStatus);
			this.pnlPrinter.Controls.Add(this.lblName);
			this.pnlPrinter.Controls.Add(this.lblStatus);
			this.pnlPrinter.Controls.Add(this.cmbPrintersList);
			this.pnlPrinter.Dock = DockStyle.Top;
			this.pnlPrinter.Location = new Point(0, 0);
			this.pnlPrinter.Name = "pnlPrinter";
			this.pnlPrinter.Size = new System.Drawing.Size(508, 85);
			this.pnlPrinter.TabIndex = 20;
			this.btnProperty.Location = new Point(367, 28);
			this.btnProperty.Name = "btnProperty";
			this.btnProperty.Size = new System.Drawing.Size(128, 23);
			this.btnProperty.TabIndex = 19;
			this.btnProperty.Text = "Property";
			this.btnProperty.UseVisualStyleBackColor = true;
			this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
			this.btnPreview.Location = new Point(367, 56);
			this.btnPreview.Name = "btnPreview";
			this.btnPreview.Size = new System.Drawing.Size(128, 23);
			this.btnPreview.TabIndex = 18;
			this.btnPreview.Text = "Preview";
			this.btnPreview.UseVisualStyleBackColor = true;
			this.btnPreview.Click += new EventHandler(this.btnPreview_Click);
			this.pnlLblPrinter.Controls.Add(this.lblPrinterLine);
			this.pnlLblPrinter.Controls.Add(this.lblPrinter);
			this.pnlLblPrinter.Dock = DockStyle.Top;
			this.pnlLblPrinter.Location = new Point(0, 0);
			this.pnlLblPrinter.Name = "pnlLblPrinter";
			this.pnlLblPrinter.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblPrinter.Size = new System.Drawing.Size(508, 28);
			this.pnlLblPrinter.TabIndex = 15;
			this.lblPrinterLine.BackColor = Color.Transparent;
			this.lblPrinterLine.Dock = DockStyle.Fill;
			this.lblPrinterLine.ForeColor = Color.Blue;
			this.lblPrinterLine.Image = Resources.GroupLine0;
			this.lblPrinterLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblPrinterLine.Location = new Point(46, 0);
			this.lblPrinterLine.Name = "lblPrinterLine";
			this.lblPrinterLine.Size = new System.Drawing.Size(447, 28);
			this.lblPrinterLine.TabIndex = 15;
			this.lblPrinterLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblPrinter.AutoSize = true;
			this.lblPrinter.BackColor = Color.Transparent;
			this.lblPrinter.Dock = DockStyle.Left;
			this.lblPrinter.ForeColor = Color.Blue;
			this.lblPrinter.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblPrinter.Location = new Point(7, 0);
			this.lblPrinter.Name = "lblPrinter";
			this.lblPrinter.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.lblPrinter.Size = new System.Drawing.Size(39, 20);
			this.lblPrinter.TabIndex = 14;
			this.lblPrinter.Text = "Printer";
			this.lblPrinter.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlFromTo.BackColor = Color.White;
			this.pnlFromTo.Controls.Add(this.pnlButtons);
			this.pnlFromTo.Controls.Add(this.pnlMultiRange);
			this.pnlFromTo.Dock = DockStyle.Bottom;
			this.pnlFromTo.Location = new Point(0, 411);
			this.pnlFromTo.Name = "pnlFromTo";
			this.pnlFromTo.Padding = new System.Windows.Forms.Padding(4);
			this.pnlFromTo.Size = new System.Drawing.Size(508, 230);
			this.pnlFromTo.TabIndex = 32;
			this.pnlButtons.Controls.Add(this.btnCancel);
			this.pnlButtons.Controls.Add(this.btnPrint);
			this.pnlButtons.Dock = DockStyle.Bottom;
			this.pnlButtons.Location = new Point(380, 157);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Size = new System.Drawing.Size(124, 69);
			this.pnlButtons.TabIndex = 33;
			this.btnCancel.Location = new Point(14, 43);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(105, 23);
			this.btnCancel.TabIndex = 31;
			this.btnCancel.Text = "Close";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnPrint.Location = new Point(14, 12);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(105, 23);
			this.btnPrint.TabIndex = 30;
			this.btnPrint.Text = "Print";
			this.btnPrint.UseVisualStyleBackColor = true;
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.pnlMultiRange.Controls.Add(this.pnlGridView);
			this.pnlMultiRange.Controls.Add(this.pnlFrmAndTo);
			this.pnlMultiRange.Dock = DockStyle.Left;
			this.pnlMultiRange.Location = new Point(4, 4);
			this.pnlMultiRange.Name = "pnlMultiRange";
			this.pnlMultiRange.Size = new System.Drawing.Size(376, 222);
			this.pnlMultiRange.TabIndex = 32;
			this.pnlGridView.Controls.Add(this.dgvPrintListPrintFrm);
			this.pnlGridView.Controls.Add(this.panel3);
			this.pnlGridView.Controls.Add(this.panel1);
			this.pnlGridView.Dock = DockStyle.Fill;
			this.pnlGridView.Location = new Point(0, 76);
			this.pnlGridView.Name = "pnlGridView";
			this.pnlGridView.Size = new System.Drawing.Size(376, 146);
			this.pnlGridView.TabIndex = 41;
			this.dgvPrintListPrintFrm.AllowUserToAddRows = false;
			this.dgvPrintListPrintFrm.AllowUserToDeleteRows = false;
			this.dgvPrintListPrintFrm.AllowUserToResizeRows = false;
			this.dgvPrintListPrintFrm.BackgroundColor = Color.White;
			this.dgvPrintListPrintFrm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = SystemColors.Window;
			dataGridViewCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = SystemColors.ControlText;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Window;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.ControlText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.False;
			this.dgvPrintListPrintFrm.DefaultCellStyle = dataGridViewCellStyle;
			this.dgvPrintListPrintFrm.Dock = DockStyle.Fill;
			this.dgvPrintListPrintFrm.Location = new Point(26, 0);
			this.dgvPrintListPrintFrm.MultiSelect = false;
			this.dgvPrintListPrintFrm.Name = "dgvPrintListPrintFrm";
			this.dgvPrintListPrintFrm.ReadOnly = true;
			this.dgvPrintListPrintFrm.RowHeadersVisible = false;
			this.dgvPrintListPrintFrm.Size = new System.Drawing.Size(350, 136);
			this.dgvPrintListPrintFrm.TabIndex = 41;
			this.panel3.Dock = DockStyle.Bottom;
			this.panel3.Location = new Point(26, 136);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(350, 10);
			this.panel3.TabIndex = 40;
			this.panel1.Dock = DockStyle.Left;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(26, 146);
			this.panel1.TabIndex = 39;
			this.pnlFrmAndTo.Controls.Add(this.txtFrom);
			this.pnlFrmAndTo.Controls.Add(this.txtTo);
			this.pnlFrmAndTo.Controls.Add(this.lblTo);
			this.pnlFrmAndTo.Controls.Add(this.lblFrom);
			this.pnlFrmAndTo.Dock = DockStyle.Top;
			this.pnlFrmAndTo.Location = new Point(0, 0);
			this.pnlFrmAndTo.Name = "pnlFrmAndTo";
			this.pnlFrmAndTo.Size = new System.Drawing.Size(376, 76);
			this.pnlFrmAndTo.TabIndex = 40;
			this.lblTo.ForeColor = Color.Black;
			this.lblTo.Location = new Point(26, 45);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(51, 19);
			this.lblTo.TabIndex = 27;
			this.lblTo.Text = "To";
			this.lblFrom.ForeColor = Color.Black;
			this.lblFrom.Location = new Point(26, 14);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(49, 19);
			this.lblFrom.TabIndex = 29;
			this.lblFrom.Text = "From";
			this.pnlControl.BackColor = Color.White;
			this.pnlControl.Controls.Add(this.previewProgressbarbar);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 641);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(508, 25);
			this.pnlControl.TabIndex = 19;
			this.previewProgressbarbar.Dock = DockStyle.Fill;
			this.previewProgressbarbar.Location = new Point(15, 4);
			this.previewProgressbarbar.Name = "previewProgressbarbar";
			this.previewProgressbarbar.Size = new System.Drawing.Size(478, 17);
			this.previewProgressbarbar.Style = ProgressBarStyle.Continuous;
			this.previewProgressbarbar.TabIndex = 25;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(514, 672);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmPrint";
			base.Padding = new System.Windows.Forms.Padding(2);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Print";
			base.Load += new EventHandler(this.frmPrint_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmPrint_FormClosing);
			this.pnlForm.ResumeLayout(false);
			this.pnlOrientationZoom.ResumeLayout(false);
			this.pnlZoom.ResumeLayout(false);
			this.pnlZoom.PerformLayout();
			this.pnlLblZoom.ResumeLayout(false);
			this.pnlLblZoom.PerformLayout();
			this.pnlOrientation.ResumeLayout(false);
			this.pnlOrientation.PerformLayout();
			this.pnlLblOrientation.ResumeLayout(false);
			this.pnlLblOrientation.PerformLayout();
			this.pnlSettingsSplitting.ResumeLayout(false);
			this.pnlSplitPrinting.ResumeLayout(false);
			this.pnlSplitPrinting.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.pnlSettings.ResumeLayout(false);
			this.pnlSettings.PerformLayout();
			this.pnlLblSettings.ResumeLayout(false);
			this.pnlLblSettings.PerformLayout();
			this.pnlOptions.ResumeLayout(false);
			this.pnlSelection.ResumeLayout(false);
			this.pnlSelection.PerformLayout();
			this.pblLblSelectionPrinting.ResumeLayout(false);
			this.pblLblSelectionPrinting.PerformLayout();
			this.pnlPrintRange.ResumeLayout(false);
			this.pnlPrintRange.PerformLayout();
			this.pnlLblRangePrinting.ResumeLayout(false);
			this.pnlLblRangePrinting.PerformLayout();
			this.pnlPrinter.ResumeLayout(false);
			this.pnlPrinter.PerformLayout();
			this.pnlLblPrinter.ResumeLayout(false);
			this.pnlLblPrinter.PerformLayout();
			this.pnlFromTo.ResumeLayout(false);
			this.pnlButtons.ResumeLayout(false);
			this.pnlMultiRange.ResumeLayout(false);
			this.pnlGridView.ResumeLayout(false);
			((ISupportInitialize)this.dgvPrintListPrintFrm).EndInit();
			this.pnlFrmAndTo.ResumeLayout(false);
			this.pnlFrmAndTo.PerformLayout();
			this.pnlControl.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void initilizePrintListGrid()
		{
			if (this.dgvPrintListPrintFrm.InvokeRequired)
			{
				this.dgvPrintListPrintFrm.Invoke(new GSPcLocalViewer.frmPrint.frmPrint.initilizePageSpecifiedGridDelegate(this.initilizePrintListGrid));
				return;
			}
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					Name = "no",
					DataPropertyName = "no",
					HeaderText = "No",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 45,
					ReadOnly = true
				};
				this.dgvPrintListPrintFrm.Columns.Add(dataGridViewTextBoxColumn);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
				{
					Name = "pageFrom",
					DataPropertyName = "pageFrom",
					HeaderText = "From",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 160,
					ReadOnly = true
				};
				this.dgvPrintListPrintFrm.Columns.Add(dataGridViewTextBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					Name = "pageTo",
					DataPropertyName = "pageTo",
					HeaderText = "To",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 160,
					ReadOnly = true
				};
				this.dgvPrintListPrintFrm.Columns.Add(dataGridViewTextBoxColumn2);
			}
			catch (Exception exception)
			{
			}
		}

		private void LoadResources()
		{
			try
			{
				this.dgvPrintListPrintFrm.Columns["no"].HeaderText = this.GetResource("No", "NO", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgvPrintListPrintFrm.Columns["pageFrom"].HeaderText = this.GetResource("Page From", "PAGE_FROM", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgvPrintListPrintFrm.Columns["pageTo"].HeaderText = this.GetResource("Page To", "PAGE_TO", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			this.chkPrintPicMemo.Text = this.GetResource("Print Picture Memo", "PRINT_PICTURE_MEMO", ResourceType.CHECK_BOX);
			this.Text = this.GetResource("Print", "PRINT", ResourceType.TITLE);
			this.lblPrinter.Text = this.GetResource("Printer", "PRINTER", ResourceType.LABEL);
			this.lblName.Text = this.GetResource("Name", "NAME", ResourceType.LABEL);
			this.lblStatus.Text = this.GetResource("Status", "STATUS", ResourceType.LABEL);
			this.lblSplitPrinting.Text = this.GetResource("Split Printing", "SPLIT_PRINTING", ResourceType.LABEL);
			this.lblSelectionPrinting.Text = this.GetResource("Selection Printing", "SELECTION_PRINTING", ResourceType.LABEL);
			this.lblRangePrinting.Text = this.GetResource("Range Printing", "RANGE_PRINTING", ResourceType.LABEL);
			this.radio_CurrentPage.Text = this.GetResource("Current Page", "CURRENT_PAGE", ResourceType.RADIO_BUTTON);
			this.radio_All.Text = this.GetResource("All Pages", "ALL_PAGE", ResourceType.RADIO_BUTTON);
			this.radio_pageRange.Text = this.GetResource("Pages", "PAGES", ResourceType.RADIO_BUTTON);
			this.checkMaxUtilization.Text = this.GetResource("Auto Rotate Picture", "AUTO_ROTATE", ResourceType.LABEL);
			this.radio_SelectionList.Text = this.GetResource("Selection List", "SELECTION_LIST", ResourceType.CHECK_BOX);
			this.radio_Picture.Text = this.GetResource("Picture", "PRINT_PICTURE", ResourceType.CHECK_BOX);
			this.radio_PartsList.Text = this.GetResource("Parts List", "PARTS_LIST", ResourceType.CHECK_BOX);
			this.lblFrom.Text = this.GetResource("From", "FROM", ResourceType.LABEL);
			this.lblTo.Text = this.GetResource("To", "TO", ResourceType.LABEL);
			this.btnPrint.Text = this.GetResource("Print", "PRINT", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
			this.btnSplitPattern.Text = this.GetResource("Split Pattern", "SPLIT_PATTERN", ResourceType.BUTTON);
			this.lblSettings.Text = this.GetResource("Settings", "SETTINGS", ResourceType.LABEL);
			this.lblPaperSize.Text = this.GetResource("Paper Size:", "PAPER_SIZE", ResourceType.LABEL);
			this.lblSplittingOption.Text = this.GetResource("Splitting Option:", "SPLITTING_OPTION", ResourceType.LABEL);
			this.chkHeaderAndFooter.Text = this.GetResource("Print Header and Footer", "PRINT_HEADER_AND_FOOTER", ResourceType.CHECK_BOX);
			this.radio_Portrait.Text = this.GetResource("Potrait", "POTRAIT", ResourceType.RADIO_BUTTON);
			this.radioLandscape.Text = this.GetResource("Landscape", "LANDSCAPE", ResourceType.RADIO_BUTTON);
			this.lblZoom.Text = this.GetResource("Zoom", "ZOOM", ResourceType.LABEL);
			this.radio_FitPage.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.RADIO_BUTTON);
			this.lblOrientation.Text = this.GetResource("Orientation", "ORIENTATION", ResourceType.LABEL);
			this.radio_HalfPage.Text = this.GetResource("Half Page", "HALF_PAGE", ResourceType.RADIO_BUTTON);
			this.chkPictureZoom.Text = this.GetResource("Maintain Picture Zoom", "MAINTAIN_PICTURE_ZOOM", ResourceType.CHECK_BOX);
			this.chkPrintNumbers.Text = this.GetResource("Print Page Numbers", "PRINT_PAGE_NUMBERS", ResourceType.CHECK_BOX);
			this.btnPreview.Text = this.GetResource("Preview", "PREVIEW", ResourceType.BUTTON);
			this.btnProperty.Text = this.GetResource("Property", "PROPERTY", ResourceType.BUTTON);
			this.checkMaxUtilization.Text = this.GetResource("Auto-Rotate Picture", "AUTO_ROTATE", ResourceType.CHECK_BOX);
		}

		private PrinterSettings OpenPrinterPropertiesDialog(PrinterSettings printerSettings)
		{
			PrinterSettings printerSetting;
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr zero1 = IntPtr.Zero;
			string printerName = printerSettings.PrinterName;
			try
			{
				try
				{
					zero = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
					IntPtr intPtr1 = GSPcLocalViewer.frmPrint.frmPrint.GlobalLock(zero);
					int num = GSPcLocalViewer.frmPrint.frmPrint.DocumentProperties(base.Handle, IntPtr.Zero, printerName, IntPtr.Zero, intPtr1, 0);
					if (num >= 0)
					{
						intPtr = Marshal.AllocHGlobal(num);
						int num1 = GSPcLocalViewer.frmPrint.frmPrint.DocumentProperties(base.Handle, IntPtr.Zero, printerName, intPtr, intPtr1, 14);
						if (num1 >= 0)
						{
							if (num1 == 2)
							{
								GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(zero);
								if (zero != IntPtr.Zero)
								{
									Marshal.FreeHGlobal(zero);
									zero = IntPtr.Zero;
								}
								if (intPtr != IntPtr.Zero)
								{
									GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(intPtr);
									intPtr = IntPtr.Zero;
								}
							}
							GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(zero);
							if (zero != IntPtr.Zero)
							{
								Marshal.FreeHGlobal(zero);
								zero = IntPtr.Zero;
							}
							if (intPtr != IntPtr.Zero)
							{
								printerSettings.SetHdevmode(intPtr);
								printerSettings.DefaultPageSettings.SetHdevmode(intPtr);
								this.SaveDevmode(printerSettings);
								GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(intPtr);
								intPtr = IntPtr.Zero;
							}
						}
						else
						{
							Marshal.FreeHGlobal(intPtr);
							Marshal.FreeHGlobal(zero);
							intPtr = IntPtr.Zero;
							zero = IntPtr.Zero;
							printerSetting = printerSettings;
							return printerSetting;
						}
					}
					else
					{
						Marshal.FreeHGlobal(intPtr);
						Marshal.FreeHGlobal(zero);
						intPtr = IntPtr.Zero;
						zero = IntPtr.Zero;
						printerSetting = printerSettings;
						return printerSetting;
					}
				}
				catch (Exception exception)
				{
				}
				return printerSettings;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(zero);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return printerSetting;
		}

		private void radio_All_CheckedChanged(object sender, EventArgs e)
		{
			this.chkPictureZoom.Enabled = false;
			this.chkPictureZoom.Checked = false;
			this.txtFrom.Enabled = false;
			this.txtTo.Enabled = false;
			GSPcLocalViewer.frmPrint.frmPrint.printCurrentPage = false;
			GSPcLocalViewer.frmPrint.frmPrint.rangePrinting = false;
			if (this.radio_PartsList.Checked || this.radio_SelectionList.Checked || this.radio_Picture.Checked)
			{
				this.btnPrint.Enabled = true;
				this.btnPreview.Enabled = true;
			}
			if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
			{
				this.btnPreview.Enabled = false;
			}
		}

		private void radio_CurrentPage_CheckedChanged(object sender, EventArgs e)
		{
			this.txtFrom.Enabled = false;
			this.txtTo.Enabled = false;
			GSPcLocalViewer.frmPrint.frmPrint.printCurrentPage = true;
			GSPcLocalViewer.frmPrint.frmPrint.rangePrinting = false;
			this.chkPictureZoom.Enabled = true;
			if (this.frmParent.Enabled)
			{
				this.frmParent.Enabled = false;
			}
			if (this.radio_PartsList.Checked || this.radio_SelectionList.Checked || this.radio_Picture.Checked)
			{
				this.btnPrint.Enabled = true;
				this.btnPreview.Enabled = true;
			}
			if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
			{
				this.btnPreview.Enabled = false;
			}
		}

		private void radio_FitPage_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radio_FitPage.Checked)
			{
				this.PrintZoom = "FitPage";
			}
		}

		private void radio_HalfPage_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radio_HalfPage.Checked)
			{
				this.PrintZoom = "HalfPage";
			}
		}

		private void radio_pageRange_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (!this.radio_pageRange.Checked)
				{
					this.dgvPrintListPrintFrm.ForeColor = Color.Gray;
					this.dgvPrintListPrintFrm.Enabled = false;
				}
				else
				{
					this.dgvPrintListPrintFrm.Enabled = true;
					this.dgvPrintListPrintFrm.ForeColor = Color.Black;
				}
			}
			catch
			{
			}
		}

		private void radio_pageRange_Click(object sender, EventArgs e)
		{
			if (!this.radio_pageRange.Checked)
			{
				this.dgvPrintListPrintFrm.ForeColor = Color.Gray;
				this.dgvPrintListPrintFrm.Enabled = false;
			}
			else
			{
				this.dgvPrintListPrintFrm.Enabled = true;
				this.dgvPrintListPrintFrm.ForeColor = Color.Black;
				if (!this.bMultiRange)
				{
					this.txtTo.Enabled = this.radio_pageRange.Checked;
					this.txtFrom.Enabled = this.radio_pageRange.Checked;
				}
				else
				{
					this.objPageSpecity = new frmPageSpecified(this, this.frmParent.ServerId);
					base.Enabled = false;
					this.objPageSpecity.Owner = base.Owner;
					this.objPageSpecity.Show();
				}
			}
			this.chkPictureZoom.Enabled = false;
			this.chkPictureZoom.Checked = false;
			if (!this.radio_pageRange.Checked)
			{
				this.frmParent.EnableTreeView(false);
			}
			else
			{
				this.frmParent.EnableTreeView(true);
			}
			GSPcLocalViewer.frmPrint.frmPrint.printCurrentPage = false;
			GSPcLocalViewer.frmPrint.frmPrint.rangePrinting = true;
			if (!this.bMultiRange)
			{
				if (this.txtFrom.Text == string.Empty || this.txtTo.Text == string.Empty)
				{
					if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
					{
						this.btnPreview.Enabled = false;
					}
					else
					{
						this.btnPreview.Enabled = false;
						this.btnPrint.Enabled = false;
					}
					this.btnPrint.Enabled = false;
				}
				else if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
				{
					this.btnPreview.Enabled = false;
					this.btnPrint.Enabled = true;
				}
				else if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
				{
					this.btnPreview.Enabled = true;
					this.btnPrint.Enabled = true;
				}
			}
			else if (this.dgvPrintListPrintFrm.Rows.Count == 0)
			{
				if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
				{
					this.btnPreview.Enabled = false;
				}
				else
				{
					this.btnPreview.Enabled = false;
					this.btnPrint.Enabled = false;
				}
				this.btnPrint.Enabled = false;
			}
			else if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
			{
				this.btnPreview.Enabled = false;
				this.btnPrint.Enabled = true;
			}
			else if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
			{
				this.btnPreview.Enabled = true;
				this.btnPrint.Enabled = true;
			}
			if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
			{
				this.btnPreview.Enabled = false;
			}
		}

		private void radio_PartsList_CheckedChanged(object sender, EventArgs e)
		{
			GSPcLocalViewer.frmPrint.frmPrint.printPartList = this.radio_PartsList.Checked;
			GSPcLocalViewer.frmPrint.frmPrint.printPicture = false;
		}

		private void radio_PartsList_CheckedChanged_1(object sender, EventArgs e)
		{
			if (this.radio_PartsList.Checked || this.radio_SelectionList.Checked)
			{
				this.radio_Portrait.Enabled = true;
				this.radioLandscape.Enabled = true;
			}
			else if (!this.radio_PartsList.Checked || !this.radio_SelectionList.Checked)
			{
				this.radio_Portrait.Enabled = false;
				this.radioLandscape.Enabled = false;
			}
			this.ChangeSelection();
			GSPcLocalViewer.frmPrint.frmPrint.printPartList = this.radio_PartsList.Checked;
			this.UpdateControls();
		}

		private void radio_Picture_CheckedChanged(object sender, EventArgs e)
		{
			GSPcLocalViewer.frmPrint.frmPrint.printPicture = this.radio_Picture.Checked;
			GSPcLocalViewer.frmPrint.frmPrint.printPartList = false;
		}

		private void radio_Picture_CheckedChanged_1(object sender, EventArgs e)
		{
			if (this.radio_Picture.Checked)
			{
				this.checkMaxUtilization.Enabled = true;
			}
			else if (!this.radio_Picture.Checked)
			{
				this.checkMaxUtilization.Enabled = false;
			}
			this.ChangeSelection();
			GSPcLocalViewer.frmPrint.frmPrint.printPicture = this.radio_Picture.Checked;
			this.UpdateControls();
		}

		private void radio_Portrait_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radio_Portrait.Checked)
			{
				this.PageOrientation = "Portrait";
			}
		}

		private void radio_SelectionList_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radio_PartsList.Checked || this.radio_SelectionList.Checked)
			{
				this.radio_Portrait.Enabled = true;
				this.radioLandscape.Enabled = true;
			}
			else if (!this.radio_PartsList.Checked || !this.radio_SelectionList.Checked)
			{
				this.radio_Portrait.Enabled = false;
				this.radioLandscape.Enabled = false;
			}
			this.ChangeSelection();
			this.UpdateControls();
		}

		private void radioLandscape_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioLandscape.Checked)
			{
				this.PageOrientation = "Landscape";
			}
		}

		private void ReadDevmode(string Filename)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				this.CurrentPrintSettings.PrinterName = GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter;
				zero = this.CurrentPrintSettings.GetHdevmode(this.CurrentPrintSettings.DefaultPageSettings);
				intPtr = GSPcLocalViewer.frmPrint.frmPrint.GlobalLock(zero);
				FileStream fileStream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
				byte[] numArray = new byte[checked((IntPtr)fileStream.Length)];
				fileStream.Read(numArray, 0, (int)numArray.Length);
				fileStream.Close();
				fileStream.Dispose();
				for (int i = 0; i < (int)numArray.Length; i++)
				{
					Marshal.WriteByte(intPtr, i, numArray[i]);
				}
				GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(zero);
				this.CurrentPrintSettings.SetHdevmode(zero);
				this.CurrentPrintSettings.DefaultPageSettings.SetHdevmode(zero);
				GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(zero);
			}
			catch (Exception exception)
			{
				if (zero != IntPtr.Zero)
				{
					GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(zero);
					GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(zero);
					zero = IntPtr.Zero;
				}
			}
		}

		public void ReadPaperSizesFromIni()
		{
			this.ReadPrinterSettingsfromIni();
		}

		private void ReadPrinterSettingsfromIni()
		{
			this.paperUtilization = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "PAPER_UTILIZATION"];
			this.str_PaperUtilization = this.paperUtilization;
			if (!(this.str_PaperUtilization.Trim().ToLower() == "default") && !(this.str_PaperUtilization.Trim().ToLower() == "maximum"))
			{
				this.str_PaperUtilization = "default";
			}
			if (this.frmParent.BookType.ToUpper().Trim() != "GSC")
			{
				this.paperUtilization = "Default";
			}
			this.dateFormat = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "DATE_FORMAT"];
			this.splitPrinting = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "SPLIT_PRINTING"];
			this.copyrightPrinitng = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "COPYRIGHT_PRINTING"];
			if (this.copyrightPrinitng != null && this.copyrightPrinitng.ToLower() == "on")
			{
				this.copyRightField = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "COPYRIGHT_FIELD"];
			}
			if (this.paperUtilization == null)
			{
				this.paperUtilization = "Default";
			}
			if (this.dateFormat == null || this.dateFormat == string.Empty)
			{
				this.dateFormat = "yyyy/MM/dd";
			}
			if (this.splitPrinting == null)
			{
				this.splitPrinting = "Off";
			}
			if (this.copyrightPrinitng == null)
			{
				this.copyrightPrinitng = "Off";
			}
			this.dateFormat = this.dateFormat.Replace("m", "M");
			this.dateFormat = this.dateFormat.Replace("D", "d");
			this.dateFormat = this.dateFormat.Replace("Y", "y");
			try
			{
				string str = DateTime.Now.ToString(this.dateFormat);
				Convert.ToDateTime(str);
			}
			catch
			{
				this.dateFormat = "yyyy/MM/dd";
			}
		}

		private void SaveDevmode(PrinterSettings printerSettings)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr handle = base.Handle;
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			folderPath = string.Concat(folderPath, "\\", Application.ProductName);
			string str = Uri.EscapeDataString(GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter);
			string str1 = string.Concat(folderPath, "\\", str, ".bin");
			try
			{
				zero = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
				intPtr = GSPcLocalViewer.frmPrint.frmPrint.GlobalLock(zero);
				int num = GSPcLocalViewer.frmPrint.frmPrint.DocumentProperties(handle, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero, intPtr, 0);
				if (num > 0)
				{
					FileStream fileStream = new FileStream(str1, FileMode.Create);
					for (int i = 0; i < num; i++)
					{
						fileStream.WriteByte(Marshal.ReadByte(intPtr, i));
					}
					fileStream.Close();
					fileStream.Dispose();
					GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(zero);
					GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(zero);
					zero = IntPtr.Zero;
				}
				else
				{
					GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(zero);
					GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(zero);
				}
			}
			catch (Exception exception)
			{
				if (zero != IntPtr.Zero)
				{
					GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(zero);
					GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(zero);
					zero = IntPtr.Zero;
				}
			}
		}

		private void SelectPrintRange(ref string sRngStart, ref string sRngEnd)
		{
			try
			{
				if (this.radio_pageRange.Checked)
				{
					sRngStart = (this.txtFrom.Tag == null || !(this.txtFrom.Tag.ToString() != string.Empty) ? "1" : this.txtFrom.Tag.ToString());
					sRngEnd = (this.txtTo.Tag == null || !(this.txtTo.Tag.ToString() != string.Empty) ? "1" : this.txtTo.Tag.ToString());
					try
					{
						int num = int.Parse(sRngStart);
						int num1 = int.Parse(sRngEnd);
						if (num > num1)
						{
							sRngStart = num1.ToString();
							sRngEnd = num.ToString();
						}
					}
					catch
					{
					}
				}
				else if (!this.radio_CurrentPage.Checked)
				{
					sRngStart = "-1";
					sRngEnd = "-1";
				}
				else
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.frmParent.objFrmTreeview.tvBook.SelectedNode.Tag.ToString()));
					XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
					sRngStart = xmlNodes.Attributes[this.frmParent.objFrmTreeview.rangePrintAttId].Value.ToString();
					sRngEnd = sRngStart;
				}
			}
			catch
			{
			}
		}

		private void selectSplittingOption(int selectedSpitOption)
		{
			try
			{
				if (selectedSpitOption == 1)
				{
					this.cmbSplittingOption.SelectedIndex = 0;
				}
				else if (selectedSpitOption == 2)
				{
					this.cmbSplittingOption.SelectedIndex = 1;
				}
				else if (selectedSpitOption == 4)
				{
					this.cmbSplittingOption.SelectedIndex = 2;
				}
				else if (selectedSpitOption == 8)
				{
					this.cmbSplittingOption.SelectedIndex = 3;
				}
			}
			catch
			{
				this.cmbSplittingOption.SelectedIndex = 0;
			}
		}

		private void ShowHideMultiRange()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.p_ServerId].sIniKey, ".ini");
				ArrayList arrayLists = new ArrayList();
				arrayLists = (new IniFileIO()).GetKeys(str, "PRINTER_SETTINGS");
				int num = 0;
				while (num < arrayLists.Count)
				{
					IniFileIO iniFileIO = new IniFileIO();
					string keyValue = iniFileIO.GetKeyValue("PRINTER_SETTINGS", arrayLists[num].ToString(), str);
					if (arrayLists[num].ToString() != "PAGE_SPECIFED_ACTION")
					{
						this.bMuliRageKey = false;
						this.pnlGridView.Visible = false;
						num++;
					}
					else
					{
						this.bMuliRageKey = true;
						if (keyValue.ToString().ToUpper() != "MULTI")
						{
							if (keyValue.ToString().ToUpper() != "SINGLE")
							{
								break;
							}
							this.bMultiRange = false;
							break;
						}
						else
						{
							this.bMultiRange = true;
							break;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
			if (!this.bMultiRange)
			{
				this.pnlGridView.Visible = false;
				this.pnlFromTo.Height = this.pnlFrmAndTo.Height;
				this.pnlFrmAndTo.Visible = true;
				GSPcLocalViewer.frmPrint.frmPrint height = this;
				height.Height = height.Height - this.pnlGridView.Height;
				return;
			}
			this.pnlFrmAndTo.Visible = false;
			Panel panel = this.pnlFromTo;
			panel.Height = panel.Height - this.pnlFrmAndTo.Height;
			this.pnlGridView.Visible = true;
			GSPcLocalViewer.frmPrint.frmPrint _frmPrint = this;
			_frmPrint.Height = _frmPrint.Height - this.pnlFrmAndTo.Height;
		}

		private int SwapPrintRange(ref string sRngStart, ref string sRngEnd)
		{
			int num;
			try
			{
				int num1 = 0;
				num1 = (int.Parse(sRngStart) <= int.Parse(sRngEnd) ? 0 : 1);
				num = num1;
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		private void txtFrom_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.radio_pageRange.Checked)
				{
					if (this.txtFrom.Text == string.Empty || this.txtTo.Text == string.Empty)
					{
						this.btnPreview.Enabled = false;
						this.btnPrint.Enabled = false;
					}
					else
					{
						if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
						{
							this.btnPreview.Enabled = true;
						}
						else
						{
							this.btnPreview.Enabled = false;
						}
						this.btnPrint.Enabled = true;
					}
				}
			}
			catch
			{
			}
		}

		private void txtTo_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.radio_pageRange.Checked)
				{
					if (this.txtFrom.Text == string.Empty || this.txtTo.Text == string.Empty)
					{
						this.btnPreview.Enabled = false;
						this.btnPrint.Enabled = false;
					}
					else
					{
						if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
						{
							this.btnPreview.Enabled = true;
						}
						else
						{
							this.btnPreview.Enabled = false;
						}
						this.btnPrint.Enabled = true;
					}
				}
			}
			catch
			{
			}
		}

		public void UpdateControls()
		{
			try
			{
				if (this.splitPrinting.ToLower() == "on")
				{
					this.cmbSplittingOption.Enabled = this.radio_Picture.Checked;
					this.btnSplitPattern.Enabled = this.radio_Picture.Checked;
				}
				if (!this.cmbSplittingOption.Enabled)
				{
					this.cmbSplittingOption.SelectedIndex = 0;
				}
				if (this.cmbSplittingOption.SelectedIndex == 0)
				{
					this.radio_HalfPage.Enabled = (!this.radio_Picture.Checked ? false : this.radio_PartsList.Checked);
				}
				if (!this.radio_HalfPage.Enabled)
				{
					this.radio_FitPage.Checked = true;
				}
				this.btnPreview.Enabled = true;
				this.btnPrint.Enabled = true;
				if (!this.radio_Picture.Checked && !this.radio_PartsList.Checked && !this.radio_SelectionList.Checked)
				{
					this.btnPreview.Enabled = false;
					this.btnPrint.Enabled = false;
				}
				if (this.cmbSplittingOption.SelectedIndex == 0 && (this.radio_Picture.Checked || this.radio_PartsList.Checked || this.radio_SelectionList.Checked))
				{
					if (this.radio_All.Visible)
					{
						this.radio_All.Enabled = true;
					}
					if (this.radio_pageRange.Visible)
					{
						this.radio_pageRange.Enabled = true;
					}
				}
				if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
				{
					this.btnPreview.Enabled = false;
				}
			}
			catch
			{
			}
		}

		public void UpdateFrom(string strFrom, string sFromIndex)
		{
			if (this.radio_pageRange.Checked && strFrom != null)
			{
				this.txtFrom.Text = strFrom;
				this.txtFrom.Tag = sFromIndex;
			}
			if (!string.IsNullOrEmpty(this.txtFrom.Text) && !string.IsNullOrEmpty(this.txtTo.Text))
			{
				this.btnPreview.Enabled = true;
				this.btnPrint.Enabled = true;
			}
		}

		public void UpdateTo(string strTo, string sToIndex)
		{
			if (this.radio_pageRange.Checked && strTo != null)
			{
				this.txtTo.Text = strTo;
				this.txtTo.Tag = sToIndex;
			}
			if (!string.IsNullOrEmpty(this.txtFrom.Text) && !string.IsNullOrEmpty(this.txtTo.Text))
			{
				this.btnPreview.Enabled = true;
				this.btnPrint.Enabled = true;
			}
		}

		private delegate void initilizePageSpecifiedGridDelegate();
	}
}
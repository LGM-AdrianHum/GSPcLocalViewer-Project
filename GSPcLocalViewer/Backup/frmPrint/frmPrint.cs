// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmPrint.frmPrint
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

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
    private static bool rangePrinting = false;
    public static bool printCurrentPage = true;
    public static bool printPicture = true;
    public static bool printPartList = true;
    public static string strExortdImgPath = string.Empty;
    public static string strExportdImgZoom = string.Empty;
    public static int intExportdImgRotationAngle = 0;
    public static string strExportImagePathJpg = string.Empty;
    private string str_PaperUtilization = string.Empty;
    public string PageOrientation = "Portrait";
    public int pageSplitCount = 1;
    public string PrintZoom = "FitPage";
    private string strDuplicatePrinting = "ON";
    private string strHeaderFooterVisibility = "ON";
    private PrinterSettings CurrentPrintSettings = new PrinterSettings();
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
    private string[] pStatus;
    public PaperSize PaperSize;
    public string paperSizesList;
    public string defaultPaperName;
    public string paperUtilization;
    public string copyrightPrinitng;
    public string copyRightField;
    public string dateFormat;
    public string splitPrinting;
    public static int[] arrExportdImgZoomFactor;
    public frmPreviewProcessing objPreviewProcessingDlg;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      DataGridViewCellStyle gridViewCellStyle = new DataGridViewCellStyle();
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
      ((ISupportInitialize) this.dgvPrintListPrintFrm).BeginInit();
      this.pnlFrmAndTo.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.SuspendLayout();
      label.BackColor = Color.Transparent;
      label.Dock = DockStyle.Fill;
      label.ForeColor = Color.Blue;
      label.Image = (Image) Resources.GroupLine0;
      label.ImageAlign = ContentAlignment.MiddleLeft;
      label.Location = new Point(7, 0);
      label.Name = "lblZoomLine";
      label.Size = new Size(270, 28);
      label.TabIndex = 15;
      label.TextAlign = ContentAlignment.MiddleLeft;
      this.radio_pageRange.AutoSize = true;
      this.radio_pageRange.ForeColor = Color.Black;
      this.radio_pageRange.Location = new Point(29, 84);
      this.radio_pageRange.Name = "radio_pageRange";
      this.radio_pageRange.Size = new Size(54, 17);
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
      this.radio_All.Size = new Size(68, 17);
      this.radio_All.TabIndex = 1;
      this.radio_All.Text = "All Pages";
      this.radio_All.UseVisualStyleBackColor = true;
      this.radio_All.CheckedChanged += new EventHandler(this.radio_All_CheckedChanged);
      this.radio_CurrentPage.AutoSize = true;
      this.radio_CurrentPage.Checked = true;
      this.radio_CurrentPage.ForeColor = Color.Black;
      this.radio_CurrentPage.Location = new Point(29, 33);
      this.radio_CurrentPage.Name = "radio_CurrentPage";
      this.radio_CurrentPage.Size = new Size(89, 17);
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
      this.txtFrom.Size = new Size(212, 21);
      this.txtFrom.TabIndex = 26;
      this.toolTip1.SetToolTip((Control) this.txtFrom, "Select From Page from List of Pages on left.");
      this.txtTo.BackColor = Color.White;
      this.txtTo.BorderStyle = BorderStyle.FixedSingle;
      this.txtTo.Enabled = false;
      this.txtTo.ImeMode = ImeMode.Off;
      this.txtTo.Location = new Point(84, 43);
      this.txtTo.MaxLength = 3;
      this.txtTo.Name = "txtTo";
      this.txtTo.ReadOnly = true;
      this.txtTo.Size = new Size(212, 21);
      this.txtTo.TabIndex = 28;
      this.toolTip1.SetToolTip((Control) this.txtTo, "Select To Page from List of Pages on left.");
      this.lblName.AutoSize = true;
      this.lblName.ForeColor = Color.Black;
      this.lblName.Location = new Point(29, 34);
      this.lblName.Name = "lblName";
      this.lblName.Size = new Size(34, 13);
      this.lblName.TabIndex = 0;
      this.lblName.Text = "Name";
      this.cmbPrintersList.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbPrintersList.ImeMode = ImeMode.Off;
      this.cmbPrintersList.Location = new Point(104, 30);
      this.cmbPrintersList.Name = "cmbPrintersList";
      this.cmbPrintersList.Size = new Size(241, 21);
      this.cmbPrintersList.TabIndex = 1;
      this.cmbPrintersList.SelectedIndexChanged += new EventHandler(this.cmbPrintersList_SelectedIndexChanged);
      this.lblStatus.AutoSize = true;
      this.lblStatus.ForeColor = Color.Black;
      this.lblStatus.Location = new Point(29, 66);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(38, 13);
      this.lblStatus.TabIndex = 3;
      this.lblStatus.Text = "Status";
      this.lblPrinterStatus.AutoSize = true;
      this.lblPrinterStatus.ForeColor = Color.Black;
      this.lblPrinterStatus.Location = new Point(105, 66);
      this.lblPrinterStatus.Name = "lblPrinterStatus";
      this.lblPrinterStatus.Size = new Size(0, 13);
      this.lblPrinterStatus.TabIndex = 4;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlOrientationZoom);
      this.pnlForm.Controls.Add((Control) this.pnlSettingsSplitting);
      this.pnlForm.Controls.Add((Control) this.pnlOptions);
      this.pnlForm.Controls.Add((Control) this.pnlPrinter);
      this.pnlForm.Controls.Add((Control) this.pnlFromTo);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(2, 2);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(510, 668);
      this.pnlForm.TabIndex = 5;
      this.pnlOrientationZoom.Controls.Add((Control) this.pnlZoom);
      this.pnlOrientationZoom.Controls.Add((Control) this.pnlOrientation);
      this.pnlOrientationZoom.Dock = DockStyle.Fill;
      this.pnlOrientationZoom.Location = new Point(0, 193);
      this.pnlOrientationZoom.Name = "pnlOrientationZoom";
      this.pnlOrientationZoom.Size = new Size(508, 104);
      this.pnlOrientationZoom.TabIndex = 30;
      this.pnlZoom.BackColor = Color.White;
      this.pnlZoom.Controls.Add((Control) this.pnlLblZoom);
      this.pnlZoom.Controls.Add((Control) this.radio_HalfPage);
      this.pnlZoom.Controls.Add((Control) this.chkPictureZoom);
      this.pnlZoom.Controls.Add((Control) this.radio_FitPage);
      this.pnlZoom.Dock = DockStyle.Fill;
      this.pnlZoom.Location = new Point(216, 0);
      this.pnlZoom.Name = "pnlZoom";
      this.pnlZoom.Size = new Size(292, 104);
      this.pnlZoom.TabIndex = 24;
      this.pnlLblZoom.Controls.Add((Control) this.lblZoom);
      this.pnlLblZoom.Controls.Add((Control) label);
      this.pnlLblZoom.Dock = DockStyle.Top;
      this.pnlLblZoom.Location = new Point(0, 0);
      this.pnlLblZoom.Name = "pnlLblZoom";
      this.pnlLblZoom.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblZoom.Size = new Size(292, 28);
      this.pnlLblZoom.TabIndex = 20;
      this.lblZoom.AutoSize = true;
      this.lblZoom.BackColor = Color.Transparent;
      this.lblZoom.Dock = DockStyle.Left;
      this.lblZoom.ForeColor = Color.Blue;
      this.lblZoom.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblZoom.Location = new Point(7, 0);
      this.lblZoom.Name = "lblZoom";
      this.lblZoom.Padding = new Padding(0, 7, 0, 0);
      this.lblZoom.Size = new Size(33, 20);
      this.lblZoom.TabIndex = 15;
      this.lblZoom.Text = "Zoom";
      this.lblZoom.TextAlign = ContentAlignment.MiddleLeft;
      this.radio_HalfPage.AutoSize = true;
      this.radio_HalfPage.Enabled = false;
      this.radio_HalfPage.Location = new Point(26, 61);
      this.radio_HalfPage.Name = "radio_HalfPage";
      this.radio_HalfPage.Size = new Size(71, 17);
      this.radio_HalfPage.TabIndex = 1;
      this.radio_HalfPage.Text = "Half Page";
      this.radio_HalfPage.UseVisualStyleBackColor = true;
      this.radio_HalfPage.CheckedChanged += new EventHandler(this.radio_HalfPage_CheckedChanged);
      this.chkPictureZoom.AutoSize = true;
      this.chkPictureZoom.Location = new Point(26, 84);
      this.chkPictureZoom.Name = "chkPictureZoom";
      this.chkPictureZoom.Size = new Size(131, 17);
      this.chkPictureZoom.TabIndex = 7;
      this.chkPictureZoom.Text = "Maintain Picture Zoom";
      this.chkPictureZoom.UseVisualStyleBackColor = true;
      this.chkPictureZoom.CheckedChanged += new EventHandler(this.chkPictureZoom_CheckedChanged);
      this.radio_FitPage.AutoSize = true;
      this.radio_FitPage.Checked = true;
      this.radio_FitPage.Location = new Point(26, 38);
      this.radio_FitPage.Name = "radio_FitPage";
      this.radio_FitPage.Size = new Size(64, 17);
      this.radio_FitPage.TabIndex = 0;
      this.radio_FitPage.TabStop = true;
      this.radio_FitPage.Text = "Fit Page";
      this.radio_FitPage.UseVisualStyleBackColor = true;
      this.radio_FitPage.CheckedChanged += new EventHandler(this.radio_FitPage_CheckedChanged);
      this.pnlOrientation.BackColor = Color.White;
      this.pnlOrientation.Controls.Add((Control) this.checkMaxUtilization);
      this.pnlOrientation.Controls.Add((Control) this.radioLandscape);
      this.pnlOrientation.Controls.Add((Control) this.radio_Portrait);
      this.pnlOrientation.Controls.Add((Control) this.pnlLblOrientation);
      this.pnlOrientation.Dock = DockStyle.Left;
      this.pnlOrientation.Location = new Point(0, 0);
      this.pnlOrientation.Name = "pnlOrientation";
      this.pnlOrientation.Size = new Size(216, 104);
      this.pnlOrientation.TabIndex = 23;
      this.checkMaxUtilization.AutoSize = true;
      this.checkMaxUtilization.Location = new Point(29, 84);
      this.checkMaxUtilization.Name = "checkMaxUtilization";
      this.checkMaxUtilization.Size = new Size(150, 17);
      this.checkMaxUtilization.TabIndex = 21;
      this.checkMaxUtilization.Text = "Maximum Paper Utilization";
      this.checkMaxUtilization.UseVisualStyleBackColor = true;
      this.checkMaxUtilization.CheckedChanged += new EventHandler(this.checkMaxUtilization_CheckedChanged);
      this.radioLandscape.AutoSize = true;
      this.radioLandscape.Location = new Point(29, 61);
      this.radioLandscape.Name = "radioLandscape";
      this.radioLandscape.Size = new Size(76, 17);
      this.radioLandscape.TabIndex = 1;
      this.radioLandscape.Text = "Landscape";
      this.radioLandscape.UseVisualStyleBackColor = true;
      this.radioLandscape.CheckedChanged += new EventHandler(this.radioLandscape_CheckedChanged);
      this.radio_Portrait.AutoSize = true;
      this.radio_Portrait.Checked = true;
      this.radio_Portrait.Location = new Point(29, 38);
      this.radio_Portrait.Name = "radio_Portrait";
      this.radio_Portrait.Size = new Size(61, 17);
      this.radio_Portrait.TabIndex = 0;
      this.radio_Portrait.TabStop = true;
      this.radio_Portrait.Text = "Portrait";
      this.radio_Portrait.UseVisualStyleBackColor = true;
      this.radio_Portrait.CheckedChanged += new EventHandler(this.radio_Portrait_CheckedChanged);
      this.pnlLblOrientation.Controls.Add((Control) this.lblOrientationLine);
      this.pnlLblOrientation.Controls.Add((Control) this.lblOrientation);
      this.pnlLblOrientation.Dock = DockStyle.Top;
      this.pnlLblOrientation.Location = new Point(0, 0);
      this.pnlLblOrientation.Name = "pnlLblOrientation";
      this.pnlLblOrientation.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblOrientation.Size = new Size(216, 28);
      this.pnlLblOrientation.TabIndex = 20;
      this.lblOrientationLine.BackColor = Color.Transparent;
      this.lblOrientationLine.Dock = DockStyle.Fill;
      this.lblOrientationLine.ForeColor = Color.Blue;
      this.lblOrientationLine.Image = (Image) Resources.GroupLine0;
      this.lblOrientationLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblOrientationLine.Location = new Point(68, 0);
      this.lblOrientationLine.Name = "lblOrientationLine";
      this.lblOrientationLine.Size = new Size(133, 28);
      this.lblOrientationLine.TabIndex = 15;
      this.lblOrientationLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblOrientation.AutoSize = true;
      this.lblOrientation.BackColor = Color.Transparent;
      this.lblOrientation.Dock = DockStyle.Left;
      this.lblOrientation.ForeColor = Color.Blue;
      this.lblOrientation.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblOrientation.Location = new Point(7, 0);
      this.lblOrientation.Name = "lblOrientation";
      this.lblOrientation.Padding = new Padding(0, 7, 0, 0);
      this.lblOrientation.Size = new Size(61, 20);
      this.lblOrientation.TabIndex = 14;
      this.lblOrientation.Text = "Orientation";
      this.lblOrientation.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlSettingsSplitting.BackColor = Color.White;
      this.pnlSettingsSplitting.Controls.Add((Control) this.pnlSplitPrinting);
      this.pnlSettingsSplitting.Controls.Add((Control) this.pnlSettings);
      this.pnlSettingsSplitting.Dock = DockStyle.Top;
      this.pnlSettingsSplitting.Location = new Point(0, 85);
      this.pnlSettingsSplitting.Name = "pnlSettingsSplitting";
      this.pnlSettingsSplitting.Size = new Size(508, 108);
      this.pnlSettingsSplitting.TabIndex = 31;
      this.pnlSplitPrinting.Controls.Add((Control) this.chkPrintNumbers);
      this.pnlSplitPrinting.Controls.Add((Control) this.btnSplitPattern);
      this.pnlSplitPrinting.Controls.Add((Control) this.panel2);
      this.pnlSplitPrinting.Controls.Add((Control) this.lblSplittingOption);
      this.pnlSplitPrinting.Controls.Add((Control) this.cmbSplittingOption);
      this.pnlSplitPrinting.Dock = DockStyle.Fill;
      this.pnlSplitPrinting.Location = new Point(216, 0);
      this.pnlSplitPrinting.Name = "pnlSplitPrinting";
      this.pnlSplitPrinting.Size = new Size(292, 108);
      this.pnlSplitPrinting.TabIndex = 2;
      this.chkPrintNumbers.AutoSize = true;
      this.chkPrintNumbers.Checked = true;
      this.chkPrintNumbers.CheckState = CheckState.Checked;
      this.chkPrintNumbers.Location = new Point(26, 80);
      this.chkPrintNumbers.Name = "chkPrintNumbers";
      this.chkPrintNumbers.Size = new Size(120, 17);
      this.chkPrintNumbers.TabIndex = 21;
      this.chkPrintNumbers.Text = "Print Page Numbers";
      this.chkPrintNumbers.UseVisualStyleBackColor = true;
      this.chkPrintNumbers.CheckedChanged += new EventHandler(this.chkPrintNumbers_CheckedChanged);
      this.btnSplitPattern.Location = new Point(174, 76);
      this.btnSplitPattern.Name = "btnSplitPattern";
      this.btnSplitPattern.Size = new Size(105, 23);
      this.btnSplitPattern.TabIndex = 20;
      this.btnSplitPattern.Text = "Split Pattern";
      this.btnSplitPattern.UseVisualStyleBackColor = true;
      this.btnSplitPattern.Click += new EventHandler(this.btnSplitPattern_Click);
      this.panel2.Controls.Add((Control) this.label1);
      this.panel2.Controls.Add((Control) this.lblSplitPrinting);
      this.panel2.Dock = DockStyle.Top;
      this.panel2.Location = new Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new Padding(7, 0, 15, 0);
      this.panel2.Size = new Size(292, 28);
      this.panel2.TabIndex = 19;
      this.label1.BackColor = Color.Transparent;
      this.label1.Dock = DockStyle.Fill;
      this.label1.ForeColor = Color.Blue;
      this.label1.Image = (Image) Resources.GroupLine0;
      this.label1.ImageAlign = ContentAlignment.MiddleLeft;
      this.label1.Location = new Point(73, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(204, 28);
      this.label1.TabIndex = 15;
      this.label1.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSplitPrinting.AutoSize = true;
      this.lblSplitPrinting.BackColor = Color.Transparent;
      this.lblSplitPrinting.Dock = DockStyle.Left;
      this.lblSplitPrinting.ForeColor = Color.Blue;
      this.lblSplitPrinting.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSplitPrinting.Location = new Point(7, 0);
      this.lblSplitPrinting.Name = "lblSplitPrinting";
      this.lblSplitPrinting.Padding = new Padding(0, 7, 0, 0);
      this.lblSplitPrinting.Size = new Size(66, 20);
      this.lblSplitPrinting.TabIndex = 12;
      this.lblSplitPrinting.Text = "Split Printing";
      this.lblSplitPrinting.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSplittingOption.AutoSize = true;
      this.lblSplittingOption.Location = new Point(26, 43);
      this.lblSplittingOption.Name = "lblSplittingOption";
      this.lblSplittingOption.Size = new Size(84, 13);
      this.lblSplittingOption.TabIndex = 12;
      this.lblSplittingOption.Text = "Splitting Option:";
      this.cmbSplittingOption.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSplittingOption.FormattingEnabled = true;
      this.cmbSplittingOption.Location = new Point(174, 40);
      this.cmbSplittingOption.Name = "cmbSplittingOption";
      this.cmbSplittingOption.Size = new Size(105, 21);
      this.cmbSplittingOption.TabIndex = 13;
      this.cmbSplittingOption.SelectedIndexChanged += new EventHandler(this.cmbSplittingOption_SelectedIndexChanged);
      this.pnlSettings.Controls.Add((Control) this.chkPrintPicMemo);
      this.pnlSettings.Controls.Add((Control) this.pnlLblSettings);
      this.pnlSettings.Controls.Add((Control) this.cmbPaperSize);
      this.pnlSettings.Controls.Add((Control) this.chkHeaderAndFooter);
      this.pnlSettings.Controls.Add((Control) this.lblPaperSize);
      this.pnlSettings.Dock = DockStyle.Left;
      this.pnlSettings.Location = new Point(0, 0);
      this.pnlSettings.Name = "pnlSettings";
      this.pnlSettings.Size = new Size(216, 108);
      this.pnlSettings.TabIndex = 1;
      this.chkPrintPicMemo.AutoSize = true;
      this.chkPrintPicMemo.Location = new Point(29, 80);
      this.chkPrintPicMemo.Name = "chkPrintPicMemo";
      this.chkPrintPicMemo.Size = new Size(115, 17);
      this.chkPrintPicMemo.TabIndex = 20;
      this.chkPrintPicMemo.Text = "Print Picture Memo";
      this.chkPrintPicMemo.UseVisualStyleBackColor = true;
      this.chkPrintPicMemo.CheckedChanged += new EventHandler(this.chkPrintPicMemo_CheckedChanged);
      this.pnlLblSettings.Controls.Add((Control) this.lblSettingsLine);
      this.pnlLblSettings.Controls.Add((Control) this.lblSettings);
      this.pnlLblSettings.Dock = DockStyle.Top;
      this.pnlLblSettings.Location = new Point(0, 0);
      this.pnlLblSettings.Name = "pnlLblSettings";
      this.pnlLblSettings.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblSettings.Size = new Size(216, 28);
      this.pnlLblSettings.TabIndex = 19;
      this.lblSettingsLine.BackColor = Color.Transparent;
      this.lblSettingsLine.Dock = DockStyle.Fill;
      this.lblSettingsLine.ForeColor = Color.Blue;
      this.lblSettingsLine.Image = (Image) Resources.GroupLine0;
      this.lblSettingsLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSettingsLine.Location = new Point(53, 0);
      this.lblSettingsLine.Name = "lblSettingsLine";
      this.lblSettingsLine.Size = new Size(148, 28);
      this.lblSettingsLine.TabIndex = 15;
      this.lblSettingsLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSettings.AutoSize = true;
      this.lblSettings.BackColor = Color.Transparent;
      this.lblSettings.Dock = DockStyle.Left;
      this.lblSettings.ForeColor = Color.Blue;
      this.lblSettings.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSettings.Location = new Point(7, 0);
      this.lblSettings.Name = "lblSettings";
      this.lblSettings.Padding = new Padding(0, 7, 0, 0);
      this.lblSettings.Size = new Size(46, 20);
      this.lblSettings.TabIndex = 12;
      this.lblSettings.Text = "Settings";
      this.lblSettings.TextAlign = ContentAlignment.MiddleLeft;
      this.cmbPaperSize.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbPaperSize.FormattingEnabled = true;
      this.cmbPaperSize.Location = new Point(104, 40);
      this.cmbPaperSize.Name = "cmbPaperSize";
      this.cmbPaperSize.Size = new Size(72, 21);
      this.cmbPaperSize.TabIndex = 11;
      this.cmbPaperSize.Visible = false;
      this.cmbPaperSize.SelectedIndexChanged += new EventHandler(this.cmbPaperSize_SelectedIndexChanged);
      this.chkHeaderAndFooter.AutoSize = true;
      this.chkHeaderAndFooter.Checked = true;
      this.chkHeaderAndFooter.CheckState = CheckState.Checked;
      this.chkHeaderAndFooter.Location = new Point(29, 40);
      this.chkHeaderAndFooter.Name = "chkHeaderAndFooter";
      this.chkHeaderAndFooter.Size = new Size(142, 17);
      this.chkHeaderAndFooter.TabIndex = 7;
      this.chkHeaderAndFooter.Text = "Print Header and Footer";
      this.chkHeaderAndFooter.UseVisualStyleBackColor = true;
      this.chkHeaderAndFooter.CheckedChanged += new EventHandler(this.chkHeaderAndFooter_CheckedChanged);
      this.lblPaperSize.AutoSize = true;
      this.lblPaperSize.Location = new Point(29, 43);
      this.lblPaperSize.Name = "lblPaperSize";
      this.lblPaperSize.Size = new Size(61, 13);
      this.lblPaperSize.TabIndex = 10;
      this.lblPaperSize.Text = "Paper Size:";
      this.lblPaperSize.Visible = false;
      this.pnlOptions.Controls.Add((Control) this.pnlSelection);
      this.pnlOptions.Controls.Add((Control) this.pnlPrintRange);
      this.pnlOptions.Dock = DockStyle.Bottom;
      this.pnlOptions.Location = new Point(0, 297);
      this.pnlOptions.Name = "pnlOptions";
      this.pnlOptions.Size = new Size(508, 114);
      this.pnlOptions.TabIndex = 24;
      this.pnlSelection.BackColor = Color.White;
      this.pnlSelection.Controls.Add((Control) this.radio_PartsList);
      this.pnlSelection.Controls.Add((Control) this.radio_SelectionList);
      this.pnlSelection.Controls.Add((Control) this.radio_Picture);
      this.pnlSelection.Controls.Add((Control) this.pblLblSelectionPrinting);
      this.pnlSelection.Dock = DockStyle.Fill;
      this.pnlSelection.Location = new Point(216, 0);
      this.pnlSelection.Name = "pnlSelection";
      this.pnlSelection.Size = new Size(292, 114);
      this.pnlSelection.TabIndex = 22;
      this.radio_PartsList.AutoSize = true;
      this.radio_PartsList.Checked = true;
      this.radio_PartsList.CheckState = CheckState.Checked;
      this.radio_PartsList.Location = new Point(26, 58);
      this.radio_PartsList.Name = "radio_PartsList";
      this.radio_PartsList.Size = new Size(67, 17);
      this.radio_PartsList.TabIndex = 24;
      this.radio_PartsList.Text = "PartsList";
      this.radio_PartsList.UseVisualStyleBackColor = true;
      this.radio_PartsList.CheckedChanged += new EventHandler(this.radio_PartsList_CheckedChanged_1);
      this.radio_SelectionList.AutoSize = true;
      this.radio_SelectionList.Checked = true;
      this.radio_SelectionList.CheckState = CheckState.Checked;
      this.radio_SelectionList.Location = new Point(26, 84);
      this.radio_SelectionList.Name = "radio_SelectionList";
      this.radio_SelectionList.Size = new Size(85, 17);
      this.radio_SelectionList.TabIndex = 23;
      this.radio_SelectionList.Text = "SelectionList";
      this.radio_SelectionList.UseVisualStyleBackColor = true;
      this.radio_SelectionList.CheckedChanged += new EventHandler(this.radio_SelectionList_CheckedChanged);
      this.radio_Picture.AutoSize = true;
      this.radio_Picture.Checked = true;
      this.radio_Picture.CheckState = CheckState.Checked;
      this.radio_Picture.Location = new Point(26, 33);
      this.radio_Picture.Name = "radio_Picture";
      this.radio_Picture.Size = new Size(59, 17);
      this.radio_Picture.TabIndex = 22;
      this.radio_Picture.Text = "Picture";
      this.radio_Picture.UseVisualStyleBackColor = true;
      this.radio_Picture.CheckedChanged += new EventHandler(this.radio_Picture_CheckedChanged_1);
      this.pblLblSelectionPrinting.Controls.Add((Control) this.label3);
      this.pblLblSelectionPrinting.Controls.Add((Control) this.lblSelectionPrinting);
      this.pblLblSelectionPrinting.Dock = DockStyle.Top;
      this.pblLblSelectionPrinting.Location = new Point(0, 0);
      this.pblLblSelectionPrinting.Name = "pblLblSelectionPrinting";
      this.pblLblSelectionPrinting.Padding = new Padding(7, 0, 15, 0);
      this.pblLblSelectionPrinting.Size = new Size(292, 28);
      this.pblLblSelectionPrinting.TabIndex = 21;
      this.label3.BackColor = Color.Transparent;
      this.label3.Dock = DockStyle.Fill;
      this.label3.ForeColor = Color.Blue;
      this.label3.Image = (Image) Resources.GroupLine0;
      this.label3.ImageAlign = ContentAlignment.MiddleLeft;
      this.label3.Location = new Point(96, 0);
      this.label3.Name = "label3";
      this.label3.Size = new Size(181, 28);
      this.label3.TabIndex = 15;
      this.label3.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectionPrinting.AutoSize = true;
      this.lblSelectionPrinting.BackColor = Color.Transparent;
      this.lblSelectionPrinting.Dock = DockStyle.Left;
      this.lblSelectionPrinting.ForeColor = Color.Blue;
      this.lblSelectionPrinting.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSelectionPrinting.Location = new Point(7, 0);
      this.lblSelectionPrinting.Name = "lblSelectionPrinting";
      this.lblSelectionPrinting.Padding = new Padding(0, 7, 0, 0);
      this.lblSelectionPrinting.Size = new Size(89, 20);
      this.lblSelectionPrinting.TabIndex = 14;
      this.lblSelectionPrinting.Text = "Selection Printing";
      this.lblSelectionPrinting.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlPrintRange.BackColor = Color.White;
      this.pnlPrintRange.Controls.Add((Control) this.pnlLblRangePrinting);
      this.pnlPrintRange.Controls.Add((Control) this.radio_CurrentPage);
      this.pnlPrintRange.Controls.Add((Control) this.radio_All);
      this.pnlPrintRange.Controls.Add((Control) this.radio_pageRange);
      this.pnlPrintRange.Dock = DockStyle.Left;
      this.pnlPrintRange.Location = new Point(0, 0);
      this.pnlPrintRange.Name = "pnlPrintRange";
      this.pnlPrintRange.Size = new Size(216, 114);
      this.pnlPrintRange.TabIndex = 21;
      this.pnlLblRangePrinting.Controls.Add((Control) this.label2);
      this.pnlLblRangePrinting.Controls.Add((Control) this.lblRangePrinting);
      this.pnlLblRangePrinting.Dock = DockStyle.Top;
      this.pnlLblRangePrinting.Location = new Point(0, 0);
      this.pnlLblRangePrinting.Name = "pnlLblRangePrinting";
      this.pnlLblRangePrinting.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblRangePrinting.Size = new Size(216, 28);
      this.pnlLblRangePrinting.TabIndex = 21;
      this.label2.BackColor = Color.Transparent;
      this.label2.Dock = DockStyle.Fill;
      this.label2.ForeColor = Color.Blue;
      this.label2.Image = (Image) Resources.GroupLine0;
      this.label2.ImageAlign = ContentAlignment.MiddleLeft;
      this.label2.Location = new Point(84, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(117, 28);
      this.label2.TabIndex = 15;
      this.label2.TextAlign = ContentAlignment.MiddleLeft;
      this.lblRangePrinting.AutoSize = true;
      this.lblRangePrinting.BackColor = Color.Transparent;
      this.lblRangePrinting.Dock = DockStyle.Left;
      this.lblRangePrinting.ForeColor = Color.Blue;
      this.lblRangePrinting.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblRangePrinting.Location = new Point(7, 0);
      this.lblRangePrinting.Name = "lblRangePrinting";
      this.lblRangePrinting.Padding = new Padding(0, 7, 0, 0);
      this.lblRangePrinting.Size = new Size(77, 20);
      this.lblRangePrinting.TabIndex = 14;
      this.lblRangePrinting.Text = "Range Printing";
      this.lblRangePrinting.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlPrinter.BackColor = Color.White;
      this.pnlPrinter.Controls.Add((Control) this.btnProperty);
      this.pnlPrinter.Controls.Add((Control) this.btnPreview);
      this.pnlPrinter.Controls.Add((Control) this.pnlLblPrinter);
      this.pnlPrinter.Controls.Add((Control) this.lblPrinterStatus);
      this.pnlPrinter.Controls.Add((Control) this.lblName);
      this.pnlPrinter.Controls.Add((Control) this.lblStatus);
      this.pnlPrinter.Controls.Add((Control) this.cmbPrintersList);
      this.pnlPrinter.Dock = DockStyle.Top;
      this.pnlPrinter.Location = new Point(0, 0);
      this.pnlPrinter.Name = "pnlPrinter";
      this.pnlPrinter.Size = new Size(508, 85);
      this.pnlPrinter.TabIndex = 20;
      this.btnProperty.Location = new Point(367, 28);
      this.btnProperty.Name = "btnProperty";
      this.btnProperty.Size = new Size(128, 23);
      this.btnProperty.TabIndex = 19;
      this.btnProperty.Text = "Property";
      this.btnProperty.UseVisualStyleBackColor = true;
      this.btnProperty.Click += new EventHandler(this.btnProperty_Click);
      this.btnPreview.Location = new Point(367, 56);
      this.btnPreview.Name = "btnPreview";
      this.btnPreview.Size = new Size(128, 23);
      this.btnPreview.TabIndex = 18;
      this.btnPreview.Text = "Preview";
      this.btnPreview.UseVisualStyleBackColor = true;
      this.btnPreview.Click += new EventHandler(this.btnPreview_Click);
      this.pnlLblPrinter.Controls.Add((Control) this.lblPrinterLine);
      this.pnlLblPrinter.Controls.Add((Control) this.lblPrinter);
      this.pnlLblPrinter.Dock = DockStyle.Top;
      this.pnlLblPrinter.Location = new Point(0, 0);
      this.pnlLblPrinter.Name = "pnlLblPrinter";
      this.pnlLblPrinter.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblPrinter.Size = new Size(508, 28);
      this.pnlLblPrinter.TabIndex = 15;
      this.lblPrinterLine.BackColor = Color.Transparent;
      this.lblPrinterLine.Dock = DockStyle.Fill;
      this.lblPrinterLine.ForeColor = Color.Blue;
      this.lblPrinterLine.Image = (Image) Resources.GroupLine0;
      this.lblPrinterLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPrinterLine.Location = new Point(46, 0);
      this.lblPrinterLine.Name = "lblPrinterLine";
      this.lblPrinterLine.Size = new Size(447, 28);
      this.lblPrinterLine.TabIndex = 15;
      this.lblPrinterLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPrinter.AutoSize = true;
      this.lblPrinter.BackColor = Color.Transparent;
      this.lblPrinter.Dock = DockStyle.Left;
      this.lblPrinter.ForeColor = Color.Blue;
      this.lblPrinter.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPrinter.Location = new Point(7, 0);
      this.lblPrinter.Name = "lblPrinter";
      this.lblPrinter.Padding = new Padding(0, 7, 0, 0);
      this.lblPrinter.Size = new Size(39, 20);
      this.lblPrinter.TabIndex = 14;
      this.lblPrinter.Text = "Printer";
      this.lblPrinter.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlFromTo.BackColor = Color.White;
      this.pnlFromTo.Controls.Add((Control) this.pnlButtons);
      this.pnlFromTo.Controls.Add((Control) this.pnlMultiRange);
      this.pnlFromTo.Dock = DockStyle.Bottom;
      this.pnlFromTo.Location = new Point(0, 411);
      this.pnlFromTo.Name = "pnlFromTo";
      this.pnlFromTo.Padding = new Padding(4);
      this.pnlFromTo.Size = new Size(508, 230);
      this.pnlFromTo.TabIndex = 32;
      this.pnlButtons.Controls.Add((Control) this.btnCancel);
      this.pnlButtons.Controls.Add((Control) this.btnPrint);
      this.pnlButtons.Dock = DockStyle.Bottom;
      this.pnlButtons.Location = new Point(380, 157);
      this.pnlButtons.Name = "pnlButtons";
      this.pnlButtons.Size = new Size(124, 69);
      this.pnlButtons.TabIndex = 33;
      this.btnCancel.Location = new Point(14, 43);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(105, 23);
      this.btnCancel.TabIndex = 31;
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnPrint.Location = new Point(14, 12);
      this.btnPrint.Name = "btnPrint";
      this.btnPrint.Size = new Size(105, 23);
      this.btnPrint.TabIndex = 30;
      this.btnPrint.Text = "Print";
      this.btnPrint.UseVisualStyleBackColor = true;
      this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
      this.pnlMultiRange.Controls.Add((Control) this.pnlGridView);
      this.pnlMultiRange.Controls.Add((Control) this.pnlFrmAndTo);
      this.pnlMultiRange.Dock = DockStyle.Left;
      this.pnlMultiRange.Location = new Point(4, 4);
      this.pnlMultiRange.Name = "pnlMultiRange";
      this.pnlMultiRange.Size = new Size(376, 222);
      this.pnlMultiRange.TabIndex = 32;
      this.pnlGridView.Controls.Add((Control) this.dgvPrintListPrintFrm);
      this.pnlGridView.Controls.Add((Control) this.panel3);
      this.pnlGridView.Controls.Add((Control) this.panel1);
      this.pnlGridView.Dock = DockStyle.Fill;
      this.pnlGridView.Location = new Point(0, 76);
      this.pnlGridView.Name = "pnlGridView";
      this.pnlGridView.Size = new Size(376, 146);
      this.pnlGridView.TabIndex = 41;
      this.dgvPrintListPrintFrm.AllowUserToAddRows = false;
      this.dgvPrintListPrintFrm.AllowUserToDeleteRows = false;
      this.dgvPrintListPrintFrm.AllowUserToResizeRows = false;
      this.dgvPrintListPrintFrm.BackgroundColor = Color.White;
      this.dgvPrintListPrintFrm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      gridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle.BackColor = SystemColors.Window;
      gridViewCellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle.ForeColor = SystemColors.ControlText;
      gridViewCellStyle.SelectionBackColor = SystemColors.Window;
      gridViewCellStyle.SelectionForeColor = SystemColors.ControlText;
      gridViewCellStyle.WrapMode = DataGridViewTriState.False;
      this.dgvPrintListPrintFrm.DefaultCellStyle = gridViewCellStyle;
      this.dgvPrintListPrintFrm.Dock = DockStyle.Fill;
      this.dgvPrintListPrintFrm.Location = new Point(26, 0);
      this.dgvPrintListPrintFrm.MultiSelect = false;
      this.dgvPrintListPrintFrm.Name = "dgvPrintListPrintFrm";
      this.dgvPrintListPrintFrm.ReadOnly = true;
      this.dgvPrintListPrintFrm.RowHeadersVisible = false;
      this.dgvPrintListPrintFrm.Size = new Size(350, 136);
      this.dgvPrintListPrintFrm.TabIndex = 41;
      this.panel3.Dock = DockStyle.Bottom;
      this.panel3.Location = new Point(26, 136);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(350, 10);
      this.panel3.TabIndex = 40;
      this.panel1.Dock = DockStyle.Left;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(26, 146);
      this.panel1.TabIndex = 39;
      this.pnlFrmAndTo.Controls.Add((Control) this.txtFrom);
      this.pnlFrmAndTo.Controls.Add((Control) this.txtTo);
      this.pnlFrmAndTo.Controls.Add((Control) this.lblTo);
      this.pnlFrmAndTo.Controls.Add((Control) this.lblFrom);
      this.pnlFrmAndTo.Dock = DockStyle.Top;
      this.pnlFrmAndTo.Location = new Point(0, 0);
      this.pnlFrmAndTo.Name = "pnlFrmAndTo";
      this.pnlFrmAndTo.Size = new Size(376, 76);
      this.pnlFrmAndTo.TabIndex = 40;
      this.lblTo.ForeColor = Color.Black;
      this.lblTo.Location = new Point(26, 45);
      this.lblTo.Name = "lblTo";
      this.lblTo.Size = new Size(51, 19);
      this.lblTo.TabIndex = 27;
      this.lblTo.Text = "To";
      this.lblFrom.ForeColor = Color.Black;
      this.lblFrom.Location = new Point(26, 14);
      this.lblFrom.Name = "lblFrom";
      this.lblFrom.Size = new Size(49, 19);
      this.lblFrom.TabIndex = 29;
      this.lblFrom.Text = "From";
      this.pnlControl.BackColor = Color.White;
      this.pnlControl.Controls.Add((Control) this.previewProgressbarbar);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 641);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(15, 4, 15, 4);
      this.pnlControl.Size = new Size(508, 25);
      this.pnlControl.TabIndex = 19;
      this.previewProgressbarbar.Dock = DockStyle.Fill;
      this.previewProgressbarbar.Location = new Point(15, 4);
      this.previewProgressbarbar.Name = "previewProgressbarbar";
      this.previewProgressbarbar.Size = new Size(478, 17);
      this.previewProgressbarbar.Style = ProgressBarStyle.Continuous;
      this.previewProgressbarbar.TabIndex = 25;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(514, 672);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmPrint);
      this.Padding = new Padding(2);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Print";
      this.Load += new EventHandler(this.frmPrint_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmPrint_FormClosing);
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
      ((ISupportInitialize) this.dgvPrintListPrintFrm).EndInit();
      this.pnlFrmAndTo.ResumeLayout(false);
      this.pnlFrmAndTo.PerformLayout();
      this.pnlControl.ResumeLayout(false);
      this.ResumeLayout(false);
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

    public bool printRangePages
    {
      get
      {
        return GSPcLocalViewer.frmPrint.frmPrint.rangePrinting;
      }
    }

    [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    private static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceNameg, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GlobalFree(IntPtr handle);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GlobalLock(IntPtr handle);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GlobalUnlock(IntPtr handle);

    public frmPrint(frmViewer objFrmViewer, int iPrintType)
    {
      this.InitializeComponent();
      try
      {
        this.checkMaxUtilization.Checked = false;
        this.frmParent = objFrmViewer;
        this.ReadPaperSizesFromIni();
        if (this.cmbSplittingOption.Items.Count == 0)
          this.addSplittingOptions();
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
            if (this.str_PaperUtilization.ToLower().Trim() == "maximum")
            {
              this.checkMaxUtilization.Enabled = true;
              this.checkMaxUtilization.Checked = true;
            }
            else
              this.checkMaxUtilization.Checked = false;
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
        if (!(this.strHeaderFooterVisibility.ToUpper() == "OFF"))
          return;
        this.chkHeaderAndFooter.Enabled = false;
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
        if (this.bMuliRageKey)
        {
          if (this.bMultiRange)
          {
            this.pnlFrmAndTo.Visible = false;
            this.pnlGridView.Visible = true;
            this.initilizePrintListGrid();
          }
          else
          {
            this.pnlFrmAndTo.Visible = true;
            this.pnlGridView.Visible = false;
          }
        }
        else
        {
          this.pnlFrmAndTo.Visible = true;
          this.pnlGridView.Visible = false;
        }
        if (!Settings.Default.EnableLocalMemo)
          this.chkPrintPicMemo.Visible = false;
        this.GetPrintingType();
        this.LoadResources();
        this.pStatus = new string[8];
        this.pStatus[0] = this.GetResource("Other", "STATUSOTHER", ResourceType.LABEL);
        this.pStatus[1] = this.GetResource("Unknown", "STATUSUNKNOWN", ResourceType.LABEL);
        this.pStatus[2] = this.GetResource("Ready", "STATUSREADY", ResourceType.LABEL);
        this.pStatus[3] = this.GetResource("Printing", "STATUSPRINTING", ResourceType.LABEL);
        this.pStatus[4] = this.GetResource("WarmUp", "STATUSWARMUP", ResourceType.LABEL);
        this.pStatus[5] = this.GetResource("Stopped Printing", "STATUSSTOPPEDPRINTING", ResourceType.LABEL);
        this.pStatus[6] = this.GetResource("Offline", "STATUSOFFLINE", ResourceType.LABEL);
        if (this.frmParent.sBookType.ToLower().Trim() == "gsp")
          this.checkMaxUtilization.Checked = this.str_PaperUtilization.ToLower().Trim() == "maximum";
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
          this.cmbPrintersList.Items.Add((object) installedPrinter.ToString());
        this.cmbPrintersList.Text = new PrinterSettings().PrinterName;
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
          this.addSplittingOptions();
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
        string str = "";
        if (Settings.Default.appLanguage != null)
          str = Program.iniUserSet.items["PRINT_SETTINGS", "HEADER_FOOTER"];
        this.chkHeaderAndFooter.Checked = str == null || str == "" || !(str.ToLower() == "off");
        if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
          return;
        this.pnlSplitPrinting.Enabled = false;
        this.pnlZoom.Enabled = false;
        this.pnlOrientation.Enabled = false;
        this.pnlSettings.Enabled = false;
        this.pnlSplitPrinting.Enabled = false;
        this.btnPreview.Enabled = false;
      }
      catch
      {
      }
    }

    private void frmPrint_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        GSPcLocalViewer.frmPrint.frmPrint.printPicture = true;
        GSPcLocalViewer.frmPrint.frmPrint.printPartList = true;
        this.frmParent.EnableTreeView(false);
        this.frmParent.Enabled = true;
        this.objPrintManager.Dispose();
        if (this.objPageSpecity.IsDisposed)
          return;
        this.objPageSpecity.Close();
      }
      catch
      {
      }
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      try
      {
        string str1 = "|";
        string empty1 = string.Empty;
        string sIniKey = Program.iniServers[this.frmParent.ServerId].sIniKey;
        string str2 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + sIniKey + "\\" + this.frmParent.BookPublishingId;
        Font printFont = Settings.Default.printFont;
        string empty2 = string.Empty;
        string name = printFont.Name;
        float size = printFont.Size;
        string text1 = this.cmbPrintersList.Text;
        string text2 = this.cmbPaperSize.Text;
        string str3 = this.cmbSplittingOption.SelectedIndex.ToString();
        string str4 = this.chkHeaderAndFooter.Checked ? "1" : "0";
        string str5 = this.chkPrintNumbers.Checked ? "1" : "0";
        string str6 = this.radioLandscape.Checked ? "1" : "0";
        string str7 = this.radio_HalfPage.Checked ? "1" : "0";
        string djVuZoom = this.frmParent.GetDjVuZoom();
        string str8 = this.chkPictureZoom.Checked ? "1" : "0";
        string zoomFactor = this.GetZoomFactor();
        string empty3 = string.Empty;
        string str9 = !this.radio_CurrentPage.Checked ? (!this.radio_pageRange.Checked ? "2" : "1") : "0";
        string sRngStart = string.Empty;
        string sRngEnd = string.Empty;
        if (this.bMuliRageKey && this.bMultiRange && str9.ToUpper() == "1")
        {
          this.GetStartAndEndRanges();
          sRngStart = this.strMultiRngStart;
          sRngEnd = this.strMultiRngEnd;
        }
        else
          this.SelectPrintRange(ref sRngStart, ref sRngEnd);
        string str10 = Program.objAppFeatures.bPrintUsingOcx ? "1" : "0";
        string str11 = this.radio_Picture.Checked ? "1" : "0";
        string str12 = this.radio_PartsList.Checked ? "1" : "0";
        string str13 = this.radio_SelectionList.Checked ? "1" : "0";
        string str14 = Settings.Default.SideBySidePrinting ? "1" : "0";
        string appProxyType = Settings.Default.appProxyType;
        string appProxyIp = Settings.Default.appProxyIP;
        string appProxyPort = Settings.Default.appProxyPort;
        string appProxyLogin = Settings.Default.appProxyLogin;
        string appProxyPassword = Settings.Default.appProxyPassword;
        string appProxyTimeOut = Settings.Default.appProxyTimeOut;
        string str15 = this.frmParent.BookType.ToUpper().Trim() != "GSP" ? "1" : "0";
        string str16 = this.copyrightPrinitng.ToLower() == "on" ? "1" : "0";
        string appLanguage = Settings.Default.appLanguage;
        string empty4 = string.Empty;
        string empty5 = string.Empty;
        string str17 = this.checkMaxUtilization.Checked ? "1" : "0";
        this.GetDjVuIdPassword(ref empty4, ref empty5);
        string arguments = empty1 + "\"" + text1 + "\"" + str1 + text2 + str1 + str3 + str1 + str4 + str1 + str5 + str1 + str6 + str1 + str7 + str1 + "\"" + djVuZoom + "\"" + str1 + str8 + str1 + zoomFactor + str1 + str9 + str1 + sRngStart + str1 + sRngEnd + str1 + str11 + str1 + str12 + str1 + str13 + str1 + str14 + str1 + appProxyType + str1 + appProxyIp + str1 + appProxyPort + str1 + appProxyLogin + str1 + appProxyPassword + str1 + appProxyTimeOut + str1 + str15 + str1 + str16 + str1 + appLanguage + str1 + empty4 + str1 + empty5 + str1 + str17 + str1 + "\"" + name + "\"" + str1 + (object) size + str1 + (object) this.bPrintPicMemo + str1 + str10 + str1 + this.strDuplicatePrinting + " \"" + str2 + "\" \"" + sIniKey + "\"";
        if (!(str11 == "1") && !(str12 == "1") && !(str13 == "1"))
          return;
        if (!File.Exists(Application.StartupPath + "\\PrintManager.exe"))
          MessageHandler.ShowError1("Print module not found");
        else
          new Process()
          {
            StartInfo = new ProcessStartInfo(Application.StartupPath + "\\PrintManager.exe", arguments)
            {
              UseShellExecute = false,
              RedirectStandardInput = true,
              RedirectStandardError = true,
              RedirectStandardOutput = true
            }
          }.Start();
      }
      catch
      {
      }
      finally
      {
        this.btnCancel_Click((object) null, (EventArgs) null);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.frmParent.EnableTreeView(false);
      this.frmParent.Enabled = true;
      this.Close();
    }

    private void cmbPrintersList_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.lblPrinterStatus.Text = this.GetPrinterStatus(this.cmbPrintersList.SelectedItem.ToString());
      GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter = this.cmbPrintersList.SelectedItem.ToString();
    }

    private void radio_pageRange_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.radio_pageRange.Checked)
        {
          this.dgvPrintListPrintFrm.Enabled = true;
          this.dgvPrintListPrintFrm.ForeColor = Color.Black;
        }
        else
        {
          this.dgvPrintListPrintFrm.ForeColor = Color.Gray;
          this.dgvPrintListPrintFrm.Enabled = false;
        }
      }
      catch
      {
      }
    }

    private void radio_pageRange_Click(object sender, EventArgs e)
    {
      if (this.radio_pageRange.Checked)
      {
        this.dgvPrintListPrintFrm.Enabled = true;
        this.dgvPrintListPrintFrm.ForeColor = Color.Black;
        if (this.bMultiRange)
        {
          this.objPageSpecity = new frmPageSpecified(this, this.frmParent.ServerId);
          this.Enabled = false;
          this.objPageSpecity.Owner = this.Owner;
          this.objPageSpecity.Show();
        }
        else
        {
          this.txtTo.Enabled = this.radio_pageRange.Checked;
          this.txtFrom.Enabled = this.radio_pageRange.Checked;
        }
      }
      else
      {
        this.dgvPrintListPrintFrm.ForeColor = Color.Gray;
        this.dgvPrintListPrintFrm.Enabled = false;
      }
      this.chkPictureZoom.Enabled = false;
      this.chkPictureZoom.Checked = false;
      if (this.radio_pageRange.Checked)
        this.frmParent.EnableTreeView(true);
      else
        this.frmParent.EnableTreeView(false);
      GSPcLocalViewer.frmPrint.frmPrint.printCurrentPage = false;
      GSPcLocalViewer.frmPrint.frmPrint.rangePrinting = true;
      if (!this.bMultiRange)
      {
        if (this.txtFrom.Text == string.Empty || this.txtTo.Text == string.Empty)
        {
          if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
          {
            this.btnPreview.Enabled = false;
            this.btnPrint.Enabled = false;
          }
          else
            this.btnPreview.Enabled = false;
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
        if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
        {
          this.btnPreview.Enabled = false;
          this.btnPrint.Enabled = false;
        }
        else
          this.btnPreview.Enabled = false;
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
      if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
        return;
      this.btnPreview.Enabled = false;
    }

    private void radio_CurrentPage_CheckedChanged(object sender, EventArgs e)
    {
      this.txtFrom.Enabled = false;
      this.txtTo.Enabled = false;
      GSPcLocalViewer.frmPrint.frmPrint.printCurrentPage = true;
      GSPcLocalViewer.frmPrint.frmPrint.rangePrinting = false;
      this.chkPictureZoom.Enabled = true;
      if (this.frmParent.Enabled)
        this.frmParent.Enabled = false;
      if (this.radio_PartsList.Checked || this.radio_SelectionList.Checked || this.radio_Picture.Checked)
      {
        this.btnPrint.Enabled = true;
        this.btnPreview.Enabled = true;
      }
      if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
        return;
      this.btnPreview.Enabled = false;
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
      if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
        return;
      this.btnPreview.Enabled = false;
    }

    private void radio_Picture_CheckedChanged(object sender, EventArgs e)
    {
      GSPcLocalViewer.frmPrint.frmPrint.printPicture = this.radio_Picture.Checked;
      GSPcLocalViewer.frmPrint.frmPrint.printPartList = false;
    }

    private void radio_PartsList_CheckedChanged(object sender, EventArgs e)
    {
      GSPcLocalViewer.frmPrint.frmPrint.printPartList = this.radio_PartsList.Checked;
      GSPcLocalViewer.frmPrint.frmPrint.printPicture = false;
    }

    private void radio_Picture_CheckedChanged_1(object sender, EventArgs e)
    {
      if (this.radio_Picture.Checked)
        this.checkMaxUtilization.Enabled = true;
      else if (!this.radio_Picture.Checked)
        this.checkMaxUtilization.Enabled = false;
      this.ChangeSelection();
      GSPcLocalViewer.frmPrint.frmPrint.printPicture = this.radio_Picture.Checked;
      this.UpdateControls();
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

    private void radio_FitPage_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.radio_FitPage.Checked)
        return;
      this.PrintZoom = "FitPage";
    }

    private void radio_HalfPage_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.radio_HalfPage.Checked)
        return;
      this.PrintZoom = "HalfPage";
    }

    private void chkPictureZoom_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void chkPrintNumbers_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void txtFrom_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (!this.radio_pageRange.Checked)
          return;
        if (this.txtFrom.Text == string.Empty || this.txtTo.Text == string.Empty)
        {
          this.btnPreview.Enabled = false;
          this.btnPrint.Enabled = false;
        }
        else
        {
          if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
            this.btnPreview.Enabled = false;
          else
            this.btnPreview.Enabled = true;
          this.btnPrint.Enabled = true;
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
        if (!this.radio_pageRange.Checked)
          return;
        if (this.txtFrom.Text == string.Empty || this.txtTo.Text == string.Empty)
        {
          this.btnPreview.Enabled = false;
          this.btnPrint.Enabled = false;
        }
        else
        {
          if (this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
            this.btnPreview.Enabled = false;
          else
            this.btnPreview.Enabled = true;
          this.btnPrint.Enabled = true;
        }
      }
      catch
      {
      }
    }

    private void checkMaxUtilization_CheckedChanged(object sender, EventArgs e)
    {
      this.ChangeSelection();
    }

    public string GetPrinterStatus(string PrinterName)
    {
      string str = "";
      try
      {
        using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher(string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", (object) PrinterName)).Get().GetEnumerator())
        {
          if (enumerator.MoveNext())
            str = this.pStatus[Convert.ToInt32(enumerator.Current["PrinterStatus"].ToString()) - 1];
        }
      }
      catch
      {
      }
      return str;
    }

    private void SelectPrintRange(ref string sRngStart, ref string sRngEnd)
    {
      try
      {
        if (this.radio_pageRange.Checked)
        {
          sRngStart = this.txtFrom.Tag == null || !(this.txtFrom.Tag.ToString() != string.Empty) ? "1" : this.txtFrom.Tag.ToString();
          sRngEnd = this.txtTo.Tag == null || !(this.txtTo.Tag.ToString() != string.Empty) ? "1" : this.txtTo.Tag.ToString();
          try
          {
            int num1 = int.Parse(sRngStart);
            int num2 = int.Parse(sRngEnd);
            if (num1 <= num2)
              return;
            sRngStart = num2.ToString();
            sRngEnd = num1.ToString();
          }
          catch
          {
          }
        }
        else if (this.radio_CurrentPage.Checked)
        {
          XmlNode xmlNode = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(this.frmParent.objFrmTreeview.tvBook.SelectedNode.Tag.ToString())));
          sRngStart = xmlNode.Attributes[this.frmParent.objFrmTreeview.rangePrintAttId].Value.ToString();
          sRngEnd = sRngStart;
        }
        else
        {
          sRngStart = "-1";
          sRngEnd = "-1";
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
        AES aes = new AES();
        sDjVuId = string.Empty;
        sDjVuPassword = string.Empty;
        string str1 = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"];
        string str2 = aes.DLLDecode(str1, "0123456789ABCDEF");
        if (!str2.Contains("|"))
          return;
        sDjVuId = str2.Substring(0, str2.IndexOf("|"));
        sDjVuPassword = str2.Substring(str2.IndexOf("|") + 1, str2.Length - (str2.IndexOf("|") + 1));
      }
      catch
      {
      }
    }

    private string GetZoomFactor()
    {
      try
      {
        string empty = string.Empty;
        int[] imageZoom = this.frmParent.GetImageZoom();
        if (imageZoom.Length != 8)
          return "0-0-0-0-0-0-0-0";
        for (int index = 0; index < imageZoom.Length; ++index)
        {
          empty += imageZoom[index].ToString();
          if (index != imageZoom.Length - 1)
            empty += ",";
        }
        return empty;
      }
      catch
      {
        return "0-0-0-0-0-0-0-0";
      }
    }

    public void UpdateFrom(string strFrom, string sFromIndex)
    {
      if (this.radio_pageRange.Checked && strFrom != null)
      {
        this.txtFrom.Text = strFrom;
        this.txtFrom.Tag = (object) sFromIndex;
      }
      if (string.IsNullOrEmpty(this.txtFrom.Text) || string.IsNullOrEmpty(this.txtTo.Text))
        return;
      this.btnPreview.Enabled = true;
      this.btnPrint.Enabled = true;
    }

    public void UpdateTo(string strTo, string sToIndex)
    {
      if (this.radio_pageRange.Checked && strTo != null)
      {
        this.txtTo.Text = strTo;
        this.txtTo.Tag = (object) sToIndex;
      }
      if (string.IsNullOrEmpty(this.txtFrom.Text) || string.IsNullOrEmpty(this.txtTo.Text))
        return;
      this.btnPreview.Enabled = true;
      this.btnPrint.Enabled = true;
    }

    private void ShowHideMultiRange()
    {
      try
      {
        string str = Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.p_ServerId].sIniKey + ".ini";
        ArrayList arrayList = new ArrayList();
        ArrayList keys = new IniFileIO().GetKeys(str, "PRINTER_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          string keyValue = new IniFileIO().GetKeyValue("PRINTER_SETTINGS", keys[index].ToString(), str);
          if (keys[index].ToString() == "PAGE_SPECIFED_ACTION")
          {
            this.bMuliRageKey = true;
            if (keyValue.ToString().ToUpper() == "MULTI")
            {
              this.bMultiRange = true;
              break;
            }
            if (keyValue.ToString().ToUpper() == "SINGLE")
            {
              this.bMultiRange = false;
              break;
            }
            break;
          }
          this.bMuliRageKey = false;
          this.pnlGridView.Visible = false;
        }
      }
      catch (Exception ex)
      {
      }
      if (this.bMultiRange)
      {
        this.pnlFrmAndTo.Visible = false;
        this.pnlFromTo.Height -= this.pnlFrmAndTo.Height;
        this.pnlGridView.Visible = true;
        this.Height -= this.pnlFrmAndTo.Height;
      }
      else
      {
        this.pnlGridView.Visible = false;
        this.pnlFromTo.Height = this.pnlFrmAndTo.Height;
        this.pnlFrmAndTo.Visible = true;
        this.Height -= this.pnlGridView.Height;
      }
    }

    private void initilizePrintListGrid()
    {
      if (this.dgvPrintListPrintFrm.InvokeRequired)
      {
        this.dgvPrintListPrintFrm.Invoke((Delegate) new GSPcLocalViewer.frmPrint.frmPrint.initilizePageSpecifiedGridDelegate(this.initilizePrintListGrid));
      }
      else
      {
        try
        {
          DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn1.Name = "no";
          viewTextBoxColumn1.DataPropertyName = "no";
          viewTextBoxColumn1.HeaderText = "No";
          viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn1.Width = 45;
          viewTextBoxColumn1.ReadOnly = true;
          this.dgvPrintListPrintFrm.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
          DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn2.Name = "pageFrom";
          viewTextBoxColumn2.DataPropertyName = "pageFrom";
          viewTextBoxColumn2.HeaderText = "From";
          viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn2.Width = 160;
          viewTextBoxColumn2.ReadOnly = true;
          this.dgvPrintListPrintFrm.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn3.Name = "pageTo";
          viewTextBoxColumn3.DataPropertyName = "pageTo";
          viewTextBoxColumn3.HeaderText = "To";
          viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn3.Width = 160;
          viewTextBoxColumn3.ReadOnly = true;
          this.dgvPrintListPrintFrm.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
        }
        catch (Exception ex)
        {
        }
      }
    }

    private void GetStartAndEndRanges()
    {
      this.strMultiRngStart = string.Empty;
      this.strMultiRngEnd = string.Empty;
      try
      {
        for (int index = 0; index < this.dgvPrintListPrintFrm.Rows.Count; ++index)
        {
          string empty1 = string.Empty;
          string empty2 = string.Empty;
          string sRngStart = this.dgvPrintListPrintFrm.Rows[index].Cells[1].Tag.ToString();
          empty2 = this.dgvPrintListPrintFrm.Rows[index].Cells[2].Tag.ToString();
          if (this.SwapPrintRange(ref sRngStart, ref empty2) == 1)
          {
            this.strMultiRngStart += empty2;
            this.strMultiRngStart += "*";
            this.strMultiRngEnd += sRngStart;
            this.strMultiRngEnd += "*";
          }
          else
          {
            this.strMultiRngStart += sRngStart;
            this.strMultiRngStart += "*";
            this.strMultiRngEnd += empty2;
            this.strMultiRngEnd += "*";
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private int SwapPrintRange(ref string sRngStart, ref string sRngEnd)
    {
      try
      {
        return int.Parse(sRngStart) <= int.Parse(sRngEnd) ? 0 : 1;
      }
      catch
      {
        return 0;
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='PRINT']";
        switch (rType)
        {
          case ResourceType.BUTTON:
            str += "/Resources[@Name='BUTTON']";
            break;
          case ResourceType.LABEL:
            str += "/Resources[@Name='LABEL']";
            break;
          case ResourceType.CHECK_BOX:
            str += "/Resources[@Name='CHECK_BOX']";
            break;
          case ResourceType.RADIO_BUTTON:
            str += "/Resources[@Name='RADIO_BUTTON']";
            break;
          case ResourceType.TITLE:
            string xQuery1 = str + "[@Name='" + sKey + "']";
            return this.frmParent.GetResourceValue(sDefaultValue, xQuery1);
          case ResourceType.COMBO_BOX:
            str += "/Resources[@Name='COMBO_BOX']";
            break;
          case ResourceType.LIST_VIEW:
            str += "/Resources[@Name='LIST_VIEW']";
            break;
          case ResourceType.GRID_VIEW:
            str += "/Resources[@Name='GRID_VIEW']";
            break;
          case ResourceType.TOOLSTRIP:
            str += "/Resources[@Name='TOOLSTRIP']";
            break;
          case ResourceType.MENU_BAR:
            str += "/Resources[@Name='MENU_BAR']";
            break;
          case ResourceType.CONTEXT_MENU:
            str += "/Resources[@Name='CONTEXT_MENU']";
            break;
          case ResourceType.STATUS_MESSAGE:
            str += "/Resources[@Name='STATUS_MESSAGE']";
            break;
          case ResourceType.POPUP_MESSAGE:
            str += "/Resources[@Name='POPUP_MESSAGE']";
            break;
        }
        string xQuery2 = str + "/Resource[@Name='" + sKey + "']";
        return this.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
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

    private void btnPreview_Click(object sender, EventArgs e)
    {
      try
      {
        string str1 = this.checkMaxUtilization.Checked ? "1" : "0";
        string str2 = "|";
        string empty1 = string.Empty;
        string sIniKey = Program.iniServers[this.frmParent.ServerId].sIniKey;
        string str3 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + sIniKey + "\\" + this.frmParent.BookPublishingId;
        string text1 = this.cmbPrintersList.Text;
        string text2 = this.cmbPaperSize.Text;
        string str4 = this.cmbSplittingOption.SelectedIndex.ToString();
        string str5 = this.chkHeaderAndFooter.Checked ? "1" : "0";
        string str6 = this.chkPrintNumbers.Checked ? "1" : "0";
        string str7 = this.radioLandscape.Checked ? "1" : "0";
        string str8 = this.radio_HalfPage.Checked ? "1" : "0";
        string djVuZoom = this.frmParent.GetDjVuZoom();
        string str9 = this.chkPictureZoom.Checked ? "1" : "0";
        string zoomFactor = this.GetZoomFactor();
        string empty2 = string.Empty;
        string str10 = !this.radio_CurrentPage.Checked ? (!this.radio_pageRange.Checked ? "2" : "1") : "0";
        string sRngStart = string.Empty;
        string sRngEnd = string.Empty;
        if (this.bMuliRageKey && this.bMultiRange && str10.ToUpper() == "1")
        {
          this.GetStartAndEndRanges();
          sRngStart = this.strMultiRngStart;
          sRngEnd = this.strMultiRngEnd;
        }
        else
          this.SelectPrintRange(ref sRngStart, ref sRngEnd);
        string str11 = this.radio_Picture.Checked ? "1" : "0";
        string str12 = this.radio_PartsList.Checked ? "1" : "0";
        string str13 = this.radio_SelectionList.Checked ? "1" : "0";
        string str14 = Settings.Default.SideBySidePrinting ? "1" : "0";
        string appProxyType = Settings.Default.appProxyType;
        string appProxyIp = Settings.Default.appProxyIP;
        string appProxyPort = Settings.Default.appProxyPort;
        string appProxyLogin = Settings.Default.appProxyLogin;
        string appProxyPassword = Settings.Default.appProxyPassword;
        string appProxyTimeOut = Settings.Default.appProxyTimeOut;
        string str15 = this.frmParent.BookType.ToUpper().Trim() != "GSP" ? "1" : "0";
        string str16 = this.copyrightPrinitng.ToLower() == "on" ? "1" : "0";
        string appLanguage = Settings.Default.appLanguage;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        this.GetDjVuIdPassword(ref empty3, ref empty4);
        string str17 = empty1 + text1 + str2 + text2 + str2 + str4 + str2 + str5 + str2 + str6 + str2 + str7 + str2 + str8 + str2 + djVuZoom + str2 + str9 + str2 + zoomFactor + str2 + str10 + str2 + sRngStart + str2 + sRngEnd + str2 + str11 + str2 + str12 + str2 + str13 + str2 + str14 + str2 + appProxyType + str2 + appProxyIp + str2 + appProxyPort + str2 + appProxyLogin + str2 + appProxyPassword + str2 + appProxyTimeOut + str2 + str15 + str2 + str16 + str2 + appLanguage + str2 + empty3 + str2 + empty4 + str2 + (object) this.bPrintPicMemo + str2 + str1 + str2 + this.strDuplicatePrinting;
        if (!(str11 == "1") && !(str12 == "1") && !(str13 == "1"))
          return;
        if (this.objPreviewProcessingDlg == null)
          this.objPreviewProcessingDlg = new frmPreviewProcessing(this);
        this.objPrintManager = new PreviewManager(this.frmParent, new string[3]
        {
          str17,
          str3,
          sIniKey
        }, this);
      }
      catch
      {
      }
    }

    public void GetPaperSize(string PageName)
    {
      if (this.PaperSize != null && this.PaperSize.PaperName == PageName)
        return;
      PrintDocument printDocument = new PrintDocument();
      printDocument.PrinterSettings.PrinterName = this.PrinterName;
      for (int index = 0; index < printDocument.PrinterSettings.PaperSizes.Count; ++index)
      {
        if (printDocument.PrinterSettings.PaperSizes[index].PaperName.Contains(PageName))
        {
          this.PaperSize = printDocument.PrinterSettings.PaperSizes[index];
          break;
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
        this.str_PaperUtilization = "default";
      if (this.frmParent.BookType.ToUpper().Trim() != "GSC")
        this.paperUtilization = "Default";
      this.dateFormat = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "DATE_FORMAT"];
      this.splitPrinting = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "SPLIT_PRINTING"];
      this.copyrightPrinitng = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "COPYRIGHT_PRINTING"];
      if (this.copyrightPrinitng != null && this.copyrightPrinitng.ToLower() == "on")
        this.copyRightField = Program.iniServers[this.frmParent.ServerId].items["PRINTER_SETTINGS", "COPYRIGHT_FIELD"];
      if (this.paperUtilization == null)
        this.paperUtilization = "Default";
      if (this.dateFormat == null || this.dateFormat == string.Empty)
        this.dateFormat = "yyyy/MM/dd";
      if (this.splitPrinting == null)
        this.splitPrinting = "Off";
      if (this.copyrightPrinitng == null)
        this.copyrightPrinitng = "Off";
      this.dateFormat = this.dateFormat.Replace("m", "M");
      this.dateFormat = this.dateFormat.Replace("D", "d");
      this.dateFormat = this.dateFormat.Replace("Y", "y");
      try
      {
        Convert.ToDateTime(DateTime.Now.ToString(this.dateFormat));
      }
      catch
      {
        this.dateFormat = "yyyy/MM/dd";
      }
    }

    private void selectSplittingOption(int selectedSpitOption)
    {
      try
      {
        switch (selectedSpitOption)
        {
          case 1:
            this.cmbSplittingOption.SelectedIndex = 0;
            break;
          case 2:
            this.cmbSplittingOption.SelectedIndex = 1;
            break;
          case 4:
            this.cmbSplittingOption.SelectedIndex = 2;
            break;
          case 8:
            this.cmbSplittingOption.SelectedIndex = 3;
            break;
        }
      }
      catch
      {
        this.cmbSplittingOption.SelectedIndex = 0;
      }
    }

    private void btnSplitPattern_Click(object sender, EventArgs e)
    {
      try
      {
        int num = (int) new frmPageSetup(this.frmParent, this.cmbSplittingOption.SelectedIndex).ShowDialog((IWin32Window) this);
      }
      catch
      {
      }
    }

    private void radio_Portrait_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.radio_Portrait.Checked)
        return;
      this.PageOrientation = "Portrait";
    }

    private void radioLandscape_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.radioLandscape.Checked)
        return;
      this.PageOrientation = "Landscape";
    }

    private void chkHeaderAndFooter_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkHeaderAndFooter.Checked)
      {
        Program.iniUserSet.UpdateItem("PRINT_SETTINGS", "HEADER_FOOTER", "ON");
      }
      else
      {
        if (this.chkHeaderAndFooter.Checked)
          return;
        Program.iniUserSet.UpdateItem("PRINT_SETTINGS", "HEADER_FOOTER", "OFF");
      }
    }

    private void cmbPaperSize_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.GetPaperSize(this.cmbPaperSize.SelectedItem.ToString());
    }

    private void cmbSplittingOption_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (this.cmbSplittingOption.SelectedIndex == 0)
        {
          this.chkPrintNumbers.Checked = false;
          this.chkPrintNumbers.Enabled = false;
        }
        else
        {
          this.chkPrintNumbers.Checked = true;
          this.chkPrintNumbers.Enabled = true;
        }
        if (this.PrintPicture && this.PrintPartsList && (this.cmbSplittingOption.SelectedIndex == 0 && this.paperUtilization.ToLower() == "default"))
          this.radio_HalfPage.Enabled = true;
        else if (this.PrintPicture && this.PrintPartsList && (this.cmbSplittingOption.SelectedIndex == 0 && this.paperUtilization.ToLower() == "maximum"))
          this.radio_HalfPage.Enabled = true;
        else if (this.cmbSplittingOption.SelectedIndex != 0)
        {
          this.radio_HalfPage.Enabled = false;
          this.PrintZoom = "FitPage";
          this.radio_CurrentPage.Checked = true;
          this.radio_FitPage.Checked = true;
        }
        else
        {
          this.PrintZoom = "FitPage";
          this.radio_FitPage.Checked = true;
          this.radio_HalfPage.Enabled = false;
          this.radio_CurrentPage.Checked = true;
        }
        this.radio_All.Enabled = this.cmbSplittingOption.SelectedIndex == 0;
        this.radio_pageRange.Enabled = this.cmbSplittingOption.SelectedIndex == 0;
        if (this.cmbSplittingOption.SelectedIndex == 0)
          this.pageSplitCount = 1;
        else if (this.cmbSplittingOption.SelectedIndex == 1)
          this.pageSplitCount = 2;
        else if (this.cmbSplittingOption.SelectedIndex == 2)
        {
          this.pageSplitCount = 4;
        }
        else
        {
          if (this.cmbSplittingOption.SelectedIndex != 3)
            return;
          this.pageSplitCount = 8;
        }
      }
      catch (Exception ex)
      {
        ex.Message.ToString();
      }
    }

    private void addSplittingOptions()
    {
      try
      {
        if (this.cmbSplittingOption.Items.Count != 0)
          return;
        this.cmbSplittingOption.Items.Add((object) this.GetResource("1:1", "1:1", ResourceType.COMBO_BOX));
        if (this.splitPrinting.ToLower() == "on")
        {
          this.cmbSplittingOption.Items.Add((object) this.GetResource("1:2", "1:2", ResourceType.COMBO_BOX));
          this.cmbSplittingOption.Items.Add((object) this.GetResource("1:4", "1:4", ResourceType.COMBO_BOX));
          this.cmbSplittingOption.Items.Add((object) this.GetResource("1:8", "1:8", ResourceType.COMBO_BOX));
        }
        else
          this.cmbSplittingOption.Enabled = false;
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
          this.cmbSplittingOption.SelectedIndex = 0;
        if (this.cmbSplittingOption.SelectedIndex == 0)
          this.radio_HalfPage.Enabled = this.radio_Picture.Checked && this.radio_PartsList.Checked;
        if (!this.radio_HalfPage.Enabled)
          this.radio_FitPage.Checked = true;
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
            this.radio_All.Enabled = true;
          if (this.radio_pageRange.Visible)
            this.radio_pageRange.Enabled = true;
        }
        if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
          return;
        this.btnPreview.Enabled = false;
      }
      catch
      {
      }
    }

    private void btnProperty_Click(object sender, EventArgs e)
    {
      try
      {
        string str1 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName;
        string str2 = Uri.EscapeDataString(Uri.UnescapeDataString(GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter));
        if (File.Exists(str1 + "\\" + str2 + ".bin"))
        {
          this.ReadDevmode(str1 + "\\" + str2 + ".bin");
        }
        else
        {
          IntPtr hdevmode = this.CurrentPrintSettings.GetHdevmode();
          this.CurrentPrintSettings.PrinterName = GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter;
          this.CurrentPrintSettings.DefaultPageSettings.CopyToHdevmode(hdevmode);
        }
        this.CurrentPrintSettings = this.OpenPrinterPropertiesDialog(this.CurrentPrintSettings);
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
        }
        else if (this.frmParent.sBookType.ToLower().Trim() != "gsc" && !this.radio_PartsList.Checked && (!this.radio_SelectionList.Checked && this.radio_Picture.Checked))
        {
          this.radioLandscape.Enabled = false;
          this.radio_Portrait.Enabled = false;
          Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Maximum");
        }
        else
        {
          if (this.radio_Picture.Checked || this.radio_PartsList.Checked || this.radio_SelectionList.Checked)
            return;
          this.radioLandscape.Enabled = false;
          this.radio_Portrait.Enabled = false;
          Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Maximum");
        }
      }
      else
      {
        if (this.checkMaxUtilization.Checked)
          return;
        if (this.frmParent.sBookType.ToLower().Trim() == "gsc")
        {
          this.radioLandscape.Enabled = true;
          this.radio_Portrait.Enabled = true;
          Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Default");
        }
        else if (this.frmParent.sBookType.ToLower().Trim() != "gsc" && !this.radio_PartsList.Checked && (!this.radio_SelectionList.Checked && this.radio_Picture.Checked))
        {
          this.radioLandscape.Enabled = true;
          this.radio_Portrait.Enabled = true;
          Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Default");
        }
        else if (this.frmParent.sBookType.ToLower().Trim() != "gsc" && !this.radio_PartsList.Checked && (!this.radio_SelectionList.Checked && this.radio_Picture.Checked))
        {
          this.radioLandscape.Enabled = false;
          this.radio_Portrait.Enabled = false;
          Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Default");
        }
        else
        {
          if (this.radio_Picture.Checked || this.radio_PartsList.Checked || this.radio_SelectionList.Checked)
            return;
          this.radioLandscape.Enabled = false;
          this.radio_Portrait.Enabled = false;
          Program.iniServers[this.frmParent.ServerId].UpdateItem("PRINTER_SETTINGS", "PAPER_UTILIZATION", "Maximum");
        }
      }
    }

    private void chkPrintPicMemo_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkPrintPicMemo.Checked)
        this.bPrintPicMemo = true;
      else
        this.bPrintPicMemo = false;
    }

    private void GetPrintingType()
    {
      try
      {
        string str = Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.p_ServerId].sIniKey + ".ini";
        ArrayList arrayList = new ArrayList();
        ArrayList keys = new IniFileIO().GetKeys(str, "PRINTER_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          string keyValue = new IniFileIO().GetKeyValue("PRINTER_SETTINGS", keys[index].ToString(), str);
          if (keys[index].ToString() == "DUPLICATE_PRINTING")
          {
            this.strDuplicatePrinting = keyValue.ToString().ToUpper().Trim();
            break;
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void GetHeaderFooterSettings()
    {
      try
      {
        string str = Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.p_ServerId].sIniKey + ".ini";
        ArrayList arrayList = new ArrayList();
        IniFileIO iniFileIo = new IniFileIO();
        ArrayList keys = iniFileIo.GetKeys(str, "PRINTER_SETTINGS");
        for (int index = 0; index < keys.Count; ++index)
        {
          string keyValue = iniFileIo.GetKeyValue("PRINTER_SETTINGS", keys[index].ToString(), str);
          if (keys[index].ToString() == "HEADERFOOTER_PRINTING")
          {
            this.strHeaderFooterVisibility = keyValue.ToString().ToUpper().Trim();
            break;
          }
        }
      }
      catch (Exception ex)
      {
        this.strHeaderFooterVisibility = "ON";
      }
    }

    private PrinterSettings OpenPrinterPropertiesDialog(PrinterSettings printerSettings)
    {
      IntPtr num1 = IntPtr.Zero;
      IntPtr num2 = IntPtr.Zero;
      IntPtr zero = IntPtr.Zero;
      string printerName = printerSettings.PrinterName;
      try
      {
        num1 = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
        IntPtr pDevModeInput = GSPcLocalViewer.frmPrint.frmPrint.GlobalLock(num1);
        int cb = GSPcLocalViewer.frmPrint.frmPrint.DocumentProperties(this.Handle, IntPtr.Zero, printerName, IntPtr.Zero, pDevModeInput, 0);
        if (cb < 0)
        {
          Marshal.FreeHGlobal(num2);
          Marshal.FreeHGlobal(num1);
          num2 = IntPtr.Zero;
          num1 = IntPtr.Zero;
          return printerSettings;
        }
        num2 = Marshal.AllocHGlobal(cb);
        int num3 = GSPcLocalViewer.frmPrint.frmPrint.DocumentProperties(this.Handle, IntPtr.Zero, printerName, num2, pDevModeInput, 14);
        if (num3 < 0)
        {
          Marshal.FreeHGlobal(num2);
          Marshal.FreeHGlobal(num1);
          num2 = IntPtr.Zero;
          num1 = IntPtr.Zero;
          return printerSettings;
        }
        if (num3 == 2)
        {
          GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(num1);
          if (num1 != IntPtr.Zero)
          {
            Marshal.FreeHGlobal(num1);
            num1 = IntPtr.Zero;
          }
          if (num2 != IntPtr.Zero)
          {
            GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(num2);
            num2 = IntPtr.Zero;
          }
        }
        GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(num1);
        if (num1 != IntPtr.Zero)
        {
          Marshal.FreeHGlobal(num1);
          num1 = IntPtr.Zero;
        }
        if (num2 != IntPtr.Zero)
        {
          printerSettings.SetHdevmode(num2);
          printerSettings.DefaultPageSettings.SetHdevmode(num2);
          this.SaveDevmode(printerSettings);
          GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(num2);
          num2 = IntPtr.Zero;
        }
      }
      catch (Exception ex)
      {
      }
      finally
      {
        if (num1 != IntPtr.Zero)
          Marshal.FreeHGlobal(num1);
        if (num2 != IntPtr.Zero)
          Marshal.FreeHGlobal(num2);
      }
      return printerSettings;
    }

    private void ReadDevmode(string Filename)
    {
      IntPtr num = IntPtr.Zero;
      IntPtr zero1 = IntPtr.Zero;
      try
      {
        this.CurrentPrintSettings.PrinterName = GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter;
        num = this.CurrentPrintSettings.GetHdevmode(this.CurrentPrintSettings.DefaultPageSettings);
        IntPtr ptr = GSPcLocalViewer.frmPrint.frmPrint.GlobalLock(num);
        FileStream fileStream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
        byte[] buffer = new byte[fileStream.Length];
        fileStream.Read(buffer, 0, buffer.Length);
        fileStream.Close();
        fileStream.Dispose();
        for (int ofs = 0; ofs < buffer.Length; ++ofs)
          Marshal.WriteByte(ptr, ofs, buffer[ofs]);
        GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(num);
        this.CurrentPrintSettings.SetHdevmode(num);
        this.CurrentPrintSettings.DefaultPageSettings.SetHdevmode(num);
        GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(num);
      }
      catch (Exception ex)
      {
        if (!(num != IntPtr.Zero))
          return;
        GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(num);
        GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(num);
        IntPtr zero2 = IntPtr.Zero;
      }
    }

    private void SaveDevmode(PrinterSettings printerSettings)
    {
      IntPtr handle1 = IntPtr.Zero;
      IntPtr zero1 = IntPtr.Zero;
      IntPtr handle2 = this.Handle;
      string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Uri.EscapeDataString(GSPcLocalViewer.frmPrint.frmPrint.selectedPrinter) + ".bin";
      IntPtr zero2;
      try
      {
        handle1 = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
        IntPtr num1 = GSPcLocalViewer.frmPrint.frmPrint.GlobalLock(handle1);
        int num2 = GSPcLocalViewer.frmPrint.frmPrint.DocumentProperties(handle2, IntPtr.Zero, printerSettings.PrinterName, IntPtr.Zero, num1, 0);
        if (num2 <= 0)
        {
          GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(handle1);
          GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(handle1);
        }
        else
        {
          FileStream fileStream = new FileStream(path, FileMode.Create);
          for (int ofs = 0; ofs < num2; ++ofs)
            fileStream.WriteByte(Marshal.ReadByte(num1, ofs));
          fileStream.Close();
          fileStream.Dispose();
          GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(handle1);
          GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(handle1);
          zero2 = IntPtr.Zero;
        }
      }
      catch (Exception ex)
      {
        if (!(handle1 != IntPtr.Zero))
          return;
        GSPcLocalViewer.frmPrint.frmPrint.GlobalUnlock(handle1);
        GSPcLocalViewer.frmPrint.frmPrint.GlobalFree(handle1);
        zero2 = IntPtr.Zero;
      }
    }

    private delegate void initilizePageSpecifiedGridDelegate();
  }
}

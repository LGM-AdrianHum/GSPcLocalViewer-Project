// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.PreviewManager
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using AxDjVuCtrlLib;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class PreviewManager : Form
  {
    public bool bPreviewImageNotExported = true;
    private string sPrinterName = string.Empty;
    private string sOrientation = string.Empty;
    private string sPaperSize = string.Empty;
    private string sUserId = string.Empty;
    private string sPassword = string.Empty;
    private string sSplittingOption = string.Empty;
    private string sPrintHeaderFooter = string.Empty;
    private string sPrintPgNos = string.Empty;
    private string sZoom = string.Empty;
    private string sCurrentImageZoom = string.Empty;
    private string sMaintainZoom = string.Empty;
    private string sZoomFactor = string.Empty;
    private string sPrintRng = string.Empty;
    private string sRngStart = string.Empty;
    private string sRngEnd = string.Empty;
    private string sPrintPic = string.Empty;
    private string sPrintList = string.Empty;
    private string sPrintSelList = string.Empty;
    private string sPrintSideBySide = string.Empty;
    private string sBookPath = string.Empty;
    private string sBookCode = string.Empty;
    private string sServerKey = string.Empty;
    private string sContentPath = string.Empty;
    private string sProxyType = string.Empty;
    private string sTimeOut = string.Empty;
    private string sProxyIP = string.Empty;
    private string sProxyPort = string.Empty;
    private string sProxyLogin = string.Empty;
    private string sProxyPassword = string.Empty;
    private string sCompression = string.Empty;
    private string sEncryption = string.Empty;
    private string sCopyRight = string.Empty;
    private string sLanguage = string.Empty;
    private string attUpdateDateElement = string.Empty;
    private string attUpdateDatePICElement = string.Empty;
    private string attUpdateDatePLElement = string.Empty;
    public string spaperUtilization = string.Empty;
    public string sBookType = string.Empty;
    public string strExportedImageName = string.Empty;
    private string sPreviousPicName = string.Empty;
    private int pageSplitCount = 1;
    private string copyrightPrinitng = string.Empty;
    private string copyRightField = string.Empty;
    private string dateFormat = string.Empty;
    private Margins PrintMargins = new Margins(50, 50, 50, 50);
    private int headerPgRowCounter = 1;
    private int headerPgColCounter = 1;
    private string sLocalListPath = string.Empty;
    private string sServerListPath = string.Empty;
    private string sLocalPicPath = string.Empty;
    private string sServerPicPath = string.Empty;
    private int PageCounter = 1;
    private int iMultiImgCounter = 1;
    private Font previewFont = Settings.Default.printFont;
    private Dictionary<string, string> dicPLColSettings = new Dictionary<string, string>();
    private Dictionary<string, string> dicSLColSettings = new Dictionary<string, string>();
    private Dictionary<string, string> dicPLColAlignments = new Dictionary<string, string>();
    private Dictionary<string, string> dicSLColAlignments = new Dictionary<string, string>();
    private Dictionary<string, string> dicSLTemp = new Dictionary<string, string>();
    private List<string> lstPicPath = new List<string>();
    private List<PrintDocument> lstDocuments = new List<PrintDocument>();
    private List<string> lstExportedImagesPath = new List<string>();
    private List<DataTable> lstPLTable = new List<DataTable>();
    private List<string> lstListPath = new List<string>();
    private List<PreviewManager.PrintJob> lstPrintJob = new List<PreviewManager.PrintJob>();
    public string sExportedImagePath = string.Empty;
    private string strPrintViaOcx = string.Empty;
    private Font MemoFont = Settings.Default.printFont;
    private string strDuplicatePrinting = string.Empty;
    private DataGridView dgPartsList = new DataGridView();
    private const string dllZipper = "ZIPPER.dll";
    private const string dllDjVuDecoder = "DjVuDecoder.dll";
    private IContainer components;
    public AxDjVuCtrl objDjVuCtl;
    private PrintPreviewDialog printPreviewDialog1;
    public frmViewer frmParent;
    private StringFormat objStringFormat;
    private ArrayList listColSequence;
    private XmlNode objXmlSchemaNode;
    private XmlNodeList objXmlNodeList;
    private string attPgNameElement;
    private string attPicElement;
    private string attListElement;
    public ArrayList arrPrintJobs;
    private PreviewManager.PrintJob objCurrentPrintJob;
    private PaperSize PaperSize;
    private int srcX;
    private int srcY;
    private int splitPageCounter;
    private int iPartsListRowCount;
    private int iSelListRowCount;
    private Image imagetoPrint;
    private PrintDocument printDocFitPage;
    private PrintDocument printDocSList;
    private DataTable PartListTable;
    private ArrayList attributeNames;
    private DataGridView dgSelList;
    private int ImagePrinted;
    private string strMultiRngStart;
    private string strMultiRngEnd;
    private bool bMultiRange;
    private bool bMuliRageKey;
    private List<XmlNodeList> lstMultiRange;
    private bool bPrintPicMemo;
    private int intPreviousPageId;
    private bool bIsOldINIPL;
    private bool bIsOldINISL;
    private StringFormat strFormat;
    private int iListCounter;
    private PreviewManager.MultiPrintDocument objMultiPrintDocument;
    private int iImgCounter;
    private int iPLTableCounter;
    private int iPrintJobCounter;
    private PrintDocument printDocImg;
    private PrintDocument printDocList;
    private PrintDocument printDocHalfPage;
    private int iTotalPageWidth;
    public bool bHasPages;
    public GSPcLocalViewer.frmPrint.frmPrint objParentPrintDlg;
    private bool bOfflineMode;
    private int MemoPrintLines;
    public frmViewerPartslist objPartList;
    public bool bNewPageForListLoaded;
    public ResourceManager rm;

    protected override void Dispose(bool disposing)
    {
      try
      {
        this.objDjVuCtl.SRC = string.Empty;
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }
      catch
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PreviewManager));
      this.objDjVuCtl = new AxDjVuCtrl();
      this.printPreviewDialog1 = new PrintPreviewDialog();
      this.objDjVuCtl.BeginInit();
      this.SuspendLayout();
      this.objDjVuCtl.Dock = DockStyle.Fill;
      this.objDjVuCtl.Enabled = true;
      this.objDjVuCtl.Location = new Point(0, 0);
      this.objDjVuCtl.Name = "objDjVuCtl";
      this.objDjVuCtl.OcxState = (AxHost.State) componentResourceManager.GetObject("objDjVuCtl.OcxState");
      this.objDjVuCtl.Size = new Size(292, 273);
      this.objDjVuCtl.TabIndex = 29;
      this.printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
      this.printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
      this.printPreviewDialog1.ClientSize = new Size(400, 300);
      this.printPreviewDialog1.Enabled = true;
      this.printPreviewDialog1.Icon = (Icon) componentResourceManager.GetObject("printPreviewDialog1.Icon");
      this.printPreviewDialog1.Name = "printPreviewDialog1";
      this.printPreviewDialog1.Visible = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 273);
      this.Controls.Add((Control) this.objDjVuCtl);
      this.Name = nameof (PreviewManager);
      this.Text = nameof (PreviewManager);
      this.FormClosing += new FormClosingEventHandler(this.PreviewManager_FormClosing);
      this.objDjVuCtl.EndInit();
      this.ResumeLayout(false);
    }

    [DllImport("ZIPPER.dll")]
    internal static extern IntPtr UnZipFile(string sFilePath);

    [DllImport("DjVuDecoder.dll")]
    internal static extern bool DjVuToJPEG(string sSourceFilePath, string sDestinationFilePath);

    [DllImport("DjVuDecoder.dll")]
    internal static extern int UnSecureDjVu(string source, string destination, string userId, string password);

    [DllImport("DjVuDecoder.dll")]
    internal static extern int SecureDjVu(string source, string destination, string userId, string password);

    [DllImport("DjVuDecoder.dll")]
    internal static extern int IsDjVuSecured(string source);

    public PreviewManager(frmViewer objFrmViewer, string[] args, GSPcLocalViewer.frmPrint.frmPrint objPrintDlg)
    {
      this.InitializeComponent();
      this.objStringFormat = new StringFormat();
      this.objStringFormat.Alignment = StringAlignment.Center;
      this.objStringFormat.LineAlignment = StringAlignment.Center;
      this.objStringFormat.Trimming = StringTrimming.EllipsisCharacter;
      this.arrPrintJobs = new ArrayList();
      this.frmParent = objFrmViewer;
      this.PrintJobRecieved(args);
      this.objDjVuCtl.CurrentLanguage = this.frmParent.AppCurrentLanguage;
      this.objParentPrintDlg = objPrintDlg;
      float memoPrintFontSize = this.GetMemoPrintFontSize();
      if ((double) memoPrintFontSize != 0.0)
        this.MemoFont = new Font(this.previewFont.Name, memoPrintFontSize);
      this.MemoPrintLines = this.GetMemoPrintLines();
      if (this.spaperUtilization == "1" && this.sPrintPic == "1" && this.sZoom == "0")
      {
        if (this.arrPrintJobs.Count > 1)
        {
          this.objParentPrintDlg.objPreviewProcessingDlg.Opacity = 0.0;
          this.objParentPrintDlg.objPreviewProcessingDlg.Show();
        }
        this.PrintManager();
      }
      else
        this.StartPrinting();
    }

    private void PreviewManager_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        this.objDjVuCtl.SRC = string.Empty;
        try
        {
          if (this.imagetoPrint == null)
            return;
          this.imagetoPrint.Dispose();
        }
        catch
        {
        }
      }
      catch
      {
      }
      finally
      {
        this.objDjVuCtl.SRC = string.Empty;
        this.objDjVuCtl.Dispose();
      }
    }

    private void doc_PrintImage(object sender, PrintPageEventArgs e)
    {
      try
      {
        if (!this.bHasPages)
        {
          this.objCurrentPrintJob = this.lstPrintJob[this.iPrintJobCounter];
          if (this.lstPrintJob[this.iPrintJobCounter].sLocalPicPath != string.Empty && this.ExportImage(this.lstPrintJob[this.iPrintJobCounter].sLocalPicPath, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
            this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
        }
        StringFormat format = new StringFormat()
        {
          Alignment = StringAlignment.Center
        };
        int num1 = 0;
        if (num1 == 0)
        {
          int left1 = this.PrintMargins.Left;
          int y1 = this.PrintMargins.Top;
          Size size1 = new Size(0, 0);
          Size size2 = new Size();
          string empty1 = string.Empty;
          string strPicMemoValue = this.objCurrentPrintJob.strPicMemoValue;
          string empty2 = string.Empty;
          if (this.objCurrentPrintJob.pageSplitCount == 1)
          {
            if (this.imagetoPrint.Height > this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0" && this.sOrientation == "0" || this.imagetoPrint.Height > this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "1")
            {
              int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size3.Height + size4.Height + size2.Height;
                height = height - size3.Height - size4.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num2 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num3 = size3.Height + size4.Height + size2.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) num2, (float) size2.Height);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) top, (float) num2, (float) num3);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
              {
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num2 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                y1 += size2.Height;
                height -= size2.Height;
                RectangleF layoutRectangle = new RectangleF((float) left2, (float) top, (float) num2, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
              }
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                height -= size1.Height;
              }
              Decimal num4 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = (int) Math.Round((Decimal) height * num4, 4);
              if (width > intPrintWidth)
              {
                Decimal num2 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Height, (Decimal) this.imagetoPrint.Width), 4);
                int num3 = height;
                width = intPrintWidth;
                height = (int) ((Decimal) width * num2);
                y1 += (num3 - height) / 2;
              }
              int x = left1 + (intPrintWidth - width) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y1, width, height));
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height > this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0")
            {
              int height1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size3.Height + size4.Height + size2.Height;
                height1 = height1 - size3.Height - size4.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num2 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num3 = size3.Height + size4.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num2, (float) num3);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) num2, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
              {
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num2 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                y1 += size2.Height;
                height1 -= size2.Height;
                RectangleF layoutRectangle = new RectangleF((float) left2, (float) top, (float) num2, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
              }
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                height1 -= size1.Height;
              }
              Decimal num4 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              Math.Min((double) intPrintWidth / (double) this.imagetoPrint.Width, (double) height1 / (double) this.imagetoPrint.Height);
              int width1 = this.imagetoPrint.Width;
              int height2 = this.imagetoPrint.Height;
              int width2 = (int) Math.Round((Decimal) height1 * num4, 4);
              int x = left1 + (intPrintWidth - width2) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y1, width2, height1));
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0")
            {
              int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int num3 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num3);
                }
              }
              catch (Exception ex)
              {
              }
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size3.Height + size4.Height + size2.Height;
                num2 = num2 - size3.Height - size4.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size3.Height + size4.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num3 - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
              {
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                y1 += size2.Height;
                num2 -= size2.Height;
                RectangleF layoutRectangle = new RectangleF((float) left2, (float) top, (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
              }
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int height = (int) Math.Round((Decimal) num3 / num6, 4);
              int y2 = y1 + (num2 - height) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y2, num3, height));
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num3 - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.imagetoPrint.Height < this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0" || this.objCurrentPrintJob.spaperUtilization == "1" && this.imagetoPrint.Width > this.imagetoPrint.Height)
            {
              int left2 = this.PrintMargins.Left;
              int y2 = this.PrintMargins.Top;
              int height1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                y2 = y2 + size3.Height + size4.Height + size2.Height;
                height1 = height1 - size3.Height - size4.Height - size2.Height;
                int left3 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int width = e.PageBounds.Width;
                int left4 = this.PrintMargins.Left;
                int right = this.PrintMargins.Right;
                int height2 = size3.Height;
                int height3 = size4.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) intPrintWidth, (float) size3.Height);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size3.Height), (float) intPrintWidth, (float) size4.Height);
                RectangleF layoutRectangle3 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) intPrintWidth, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle3, format);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
              {
                y2 += size2.Height;
                height1 -= size2.Height;
                RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
              }
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                height1 -= size1.Height;
              }
              Decimal num2 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int num3 = (int) Math.Round((Decimal) intPrintWidth / num2, 4);
              int width1 = intPrintWidth;
              if (num3 > height1)
              {
                width1 = (int) ((Decimal) height1 * num2);
                left2 += (intPrintWidth - width1) / 2;
              }
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left2, y2, width1, height1));
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Top - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int left2 = this.PrintMargins.Left;
              int y2 = this.PrintMargins.Top;
              int height1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                y2 = y2 + size3.Height + size4.Height + size2.Height;
                height1 = height1 - size3.Height - size4.Height - size2.Height;
                int left3 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int width = e.PageBounds.Width;
                int left4 = this.PrintMargins.Left;
                int right = this.PrintMargins.Right;
                int height2 = size3.Height;
                int height3 = size4.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) intPrintWidth, (float) size3.Height);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size3.Height), (float) intPrintWidth, (float) size4.Height);
                RectangleF layoutRectangle3 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) intPrintWidth, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle3, format);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
              {
                y2 += size2.Height;
                height1 -= size2.Height;
                RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
              }
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                height1 -= size1.Height;
              }
              Decimal num2 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int num3 = (int) Math.Round((Decimal) intPrintWidth / num2, 4);
              int width1 = intPrintWidth;
              if (num3 > height1)
              {
                width1 = (int) ((Decimal) height1 * num2);
                left2 += (intPrintWidth - width1) / 2;
              }
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left2, y2, width1, height1));
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Top - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int num3 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num3);
                }
              }
              catch (Exception ex)
              {
              }
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size3.Height + size4.Height + size2.Height;
                num2 = num2 - size3.Height - size4.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size3.Height + size4.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num3 - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
              {
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                y1 += size2.Height;
                num2 -= size2.Height;
                RectangleF layoutRectangle = new RectangleF((float) left2, (float) top, (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
              }
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int height = (int) Math.Round((Decimal) num3 / num6, 4);
              int y2 = y1 + (num2 - height) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y2, num3, height));
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num3 - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
            }
          }
          else if (this.objCurrentPrintJob.pageSplitCount == 2)
          {
            if (this.imagetoPrint.Width < this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int num7 = num2;
              int width = (int) Math.Round((Decimal) num2 * num6, 4) * 2;
              int num8 = num7 * 2;
              while (width > intPrintWidth || num8 > num2 * 2)
              {
                num8 -= 10;
                width -= 10;
              }
              if (this.srcY == 0)
                y1 += (num2 * 2 - num8) / 2;
              int height = num8 / 2;
              int x = left1 + (intPrintWidth - width) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
              this.srcY = this.imagetoPrint.Height / 2;
            }
            else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = intPrintWidth;
              int height = (int) Math.Round((Decimal) intPrintWidth * num6, 4);
              if (height > num2)
                height = num2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
              this.srcX = this.imagetoPrint.Width / 2;
            }
            else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = intPrintWidth;
              int height = (int) Math.Round((Decimal) intPrintWidth * num6, 4);
              if (height < num2)
                height = num2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
              this.srcX = this.imagetoPrint.Width / 2;
            }
            else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int num7 = num2;
              if ((int) Math.Round((Decimal) num2 / num6, 4) > intPrintWidth)
                num7 = (int) ((Decimal) intPrintWidth * num6);
              int width = intPrintWidth;
              int height = (int) ((Decimal) width * num6) / 2;
              int y2 = y1 + (num2 - height) / 2;
              int x = left1 + (intPrintWidth - width) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y2, width, height), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
              this.srcY = this.imagetoPrint.Height / 2;
            }
            ++this.splitPageCounter;
            if (this.splitPageCounter < this.objCurrentPrintJob.pageSplitCount)
            {
              if (this.imagetoPrint.Height == this.imagetoPrint.Width)
              {
                if (this.objCurrentPrintJob.sOrientation == "0")
                  ++this.headerPgRowCounter;
                else if (this.objCurrentPrintJob.sOrientation == "1")
                  ++this.headerPgColCounter;
              }
              else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
                ++this.headerPgColCounter;
              else if (this.imagetoPrint.Height > this.imagetoPrint.Width)
                ++this.headerPgRowCounter;
              e.HasMorePages = true;
              this.bHasPages = true;
              return;
            }
            e.HasMorePages = false;
            this.bHasPages = false;
            this.splitPageCounter = 0;
            this.headerPgColCounter = 1;
            this.headerPgRowCounter = 1;
          }
          else if (this.objCurrentPrintJob.pageSplitCount == 4)
          {
            if (this.imagetoPrint.Width < this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num2);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                height = height - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  height -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                height -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              if ((int) Math.Round((Decimal) height * num6, 4) > num2)
              {
                int num7 = (int) ((Decimal) num2 / num6);
              }
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, num2, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
            {
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = intPrintWidth;
              int height = (int) Math.Round((Decimal) width / num6, 4);
              if (height > num2)
              {
                height = num2;
                width = (int) ((Decimal) height * num6);
              }
              if (this.splitPageCounter == 0)
              {
                y1 += num2 - height;
                left1 += intPrintWidth - width;
              }
              else if (this.splitPageCounter == 1)
                y1 += num2 - height;
              else if (this.splitPageCounter == 2)
                left1 += intPrintWidth - width;
              else if (this.splitPageCounter == 3)
                y1 += num2 - height;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = intPrintWidth;
              int height = (int) Math.Round((Decimal) width / num6, 4);
              if (height > num2)
              {
                height = num2;
                width = (int) ((Decimal) height * num6);
              }
              if (this.splitPageCounter == 0)
              {
                y1 += num2 - height;
                left1 += intPrintWidth - width;
              }
              else if (this.splitPageCounter == 1)
                y1 += num2 - height;
              else if (this.splitPageCounter == 2)
                left1 += intPrintWidth - width;
              else if (this.splitPageCounter == 3)
                y1 += num2 - height;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num2);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                height = height - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  height -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                height -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              if ((int) Math.Round((Decimal) height * num6, 4) > num2)
              {
                int num7 = (int) ((Decimal) num2 / num6);
              }
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, num2, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
            }
            ++this.splitPageCounter;
            if (this.splitPageCounter < this.objCurrentPrintJob.pageSplitCount)
            {
              if (this.splitPageCounter % 2 == 1)
                this.srcX = this.imagetoPrint.Width / 2;
              ++this.headerPgColCounter;
              if (this.splitPageCounter == 2)
              {
                ++this.headerPgRowCounter;
                this.headerPgColCounter = 1;
                this.srcY = this.imagetoPrint.Height / 2;
                this.srcX = 0;
              }
              e.HasMorePages = true;
              this.bHasPages = true;
              return;
            }
            e.HasMorePages = false;
            this.bHasPages = false;
            this.splitPageCounter = 0;
            this.headerPgColCounter = 1;
            this.headerPgRowCounter = 1;
          }
          else if (this.objCurrentPrintJob.pageSplitCount == 8)
          {
            if (this.imagetoPrint.Width < this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int height = num2;
              int width = (int) Math.Round((Decimal) num2 / num6, 4);
              if (width > intPrintWidth)
              {
                width = intPrintWidth;
                height = (int) ((Decimal) width * num6);
              }
              if (this.splitPageCounter % 2 == 0)
                left1 += intPrintWidth - width;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = intPrintWidth;
              int height = (int) Math.Round((Decimal) intPrintWidth * num6, 4);
              if (height > num2)
                height = num2;
              if (this.splitPageCounter < 4)
                y1 += num2 - height;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = intPrintWidth;
              int height = (int) Math.Round((Decimal) intPrintWidth * num6, 4);
              if (height > num2)
                height = num2;
              if (this.splitPageCounter < 4)
                y1 += num2 - height;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
            }
            else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
            {
              int num2 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
              int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
              try
              {
                if (strPicMemoValue.Length > 0)
                {
                  string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                  size2.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                }
              }
              catch (Exception ex)
              {
              }
              Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
              int num3;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                y1 = y1 + size4.Height + size5.Height + size2.Height;
                num2 = num2 - size4.Height - size5.Height - size2.Height;
                int left2 = this.PrintMargins.Left;
                int top = this.PrintMargins.Top;
                int num4 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num5 = size4.Height + size5.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num4, (float) num5);
                RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num4, (float) size2.Height);
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format);
                e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format);
                num3 = this.PrintMargins.Top;
              }
              else
              {
                if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                {
                  y1 += size2.Height;
                  num2 -= size2.Height;
                  RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size2.Height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format);
                }
                num3 = size3.Height;
              }
              if (this.objCurrentPrintJob.sPrintPgNos == "1")
                e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num3);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
              {
                size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                num2 -= size1.Height;
              }
              Decimal num6 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int num7 = num2;
              if ((int) Math.Round((Decimal) num2 / num6, 4) > intPrintWidth)
                num7 = (int) ((Decimal) intPrintWidth * num6);
              int width = intPrintWidth;
              int height = (int) ((Decimal) width * num6) / 2;
              int y2 = y1 + (num2 - height) / 2;
              if (this.splitPageCounter % 2 == 0)
                left1 += intPrintWidth - width;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y2, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
            }
            ++this.splitPageCounter;
            if (this.splitPageCounter < this.objCurrentPrintJob.pageSplitCount)
            {
              if (this.imagetoPrint.Width < this.imagetoPrint.Height)
              {
                ++this.headerPgColCounter;
                if (this.splitPageCounter % 2 == 0)
                {
                  this.headerPgColCounter = 1;
                  ++this.headerPgRowCounter;
                  this.srcY += this.imagetoPrint.Height / 4;
                  this.srcX = 0;
                }
                else
                  this.srcX = this.imagetoPrint.Width / 2;
              }
              else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
              {
                ++this.headerPgColCounter;
                if (this.splitPageCounter % 4 == 0)
                {
                  this.headerPgColCounter = 1;
                  ++this.headerPgRowCounter;
                  this.srcY += this.imagetoPrint.Height / 2;
                  this.srcX = 0;
                }
                else
                  this.srcX += this.imagetoPrint.Width / 4;
              }
              else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
              {
                if (this.objCurrentPrintJob.sOrientation == "1")
                {
                  ++this.headerPgColCounter;
                  if (this.splitPageCounter % 4 == 0)
                  {
                    this.headerPgColCounter = 1;
                    ++this.headerPgRowCounter;
                    this.srcY += this.imagetoPrint.Height / 2;
                    this.srcX = 0;
                  }
                  else
                    this.srcX += this.imagetoPrint.Width / 4;
                }
                else if (this.objCurrentPrintJob.sOrientation == "0")
                {
                  ++this.headerPgColCounter;
                  if (this.splitPageCounter % 2 == 0)
                  {
                    this.headerPgColCounter = 1;
                    ++this.headerPgRowCounter;
                    this.srcY += this.imagetoPrint.Height / 4;
                    this.srcX = 0;
                  }
                  else
                    this.srcX = this.imagetoPrint.Width / 2;
                }
              }
              e.HasMorePages = true;
              this.bHasPages = true;
              return;
            }
            e.HasMorePages = false;
            this.bHasPages = false;
            this.splitPageCounter = 0;
            this.headerPgColCounter = 1;
            this.headerPgRowCounter = 1;
          }
          if (this.objCurrentPrintJob.sPrintPic == "1")
          {
            if (this.objCurrentPrintJob.sPrintList == "1")
            {
              if (this.objCurrentPrintJob.sPrintList != "0")
              {
                if (this.objCurrentPrintJob.pageSplitCount == 1)
                {
                  int num2 = num1 + 1;
                  e.HasMorePages = false;
                }
              }
            }
          }
        }
        try
        {
          if (!e.HasMorePages)
          {
            ++this.iImgCounter;
            if (this.objCurrentPrintJob.sPrintList == "0" || this.objCurrentPrintJob.sLocalListPath == string.Empty)
              ++this.iPrintJobCounter;
            if (this.imagetoPrint != null)
              this.imagetoPrint.Dispose();
          }
          if (this.imagetoPrint == null)
            return;
          this.imagetoPrint = (Image) null;
        }
        catch (Exception ex)
        {
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void doc_PrintFitPage(object sender, PrintPageEventArgs e)
    {
      this.iTotalPageWidth = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
      Size size1 = new Size(0, 0);
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string strPicMemoValue = this.objCurrentPrintJob.strPicMemoValue;
      StringFormat format1 = new StringFormat()
      {
        Alignment = StringAlignment.Center
      };
      StringFormat format2 = new StringFormat()
      {
        FormatFlags = StringFormatFlags.NoWrap,
        Trimming = StringTrimming.Character
      };
      if (this.objCurrentPrintJob.sPrintPic == "1")
      {
        try
        {
          if (this.ImagePrinted == 0)
          {
            if (this.arrPrintJobs.Count > 0 && this.bPreviewImageNotExported)
            {
              this.bPreviewImageNotExported = false;
              this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
              this.arrPrintJobs.RemoveAt(0);
              this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
              string sPicPath1 = string.Empty;
              if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalPicPath))
                sPicPath1 = this.objCurrentPrintJob.sLocalPicPath;
              else if (this.objCurrentPrintJob.sServerPicPath != string.Empty)
              {
                new Download().DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
                sPicPath1 = this.objCurrentPrintJob.sLocalPicPath;
              }
              if (this.ExportImage(sPicPath1, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
                this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
            }
            if (this.iMultiImgCounter > 1)
            {
              this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
              string sPicPath1 = string.Empty;
              if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalPicPath))
                sPicPath1 = this.objCurrentPrintJob.sLocalPicPath;
              else if (this.objCurrentPrintJob.sServerPicPath != string.Empty)
              {
                new Download().DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
                sPicPath1 = this.objCurrentPrintJob.sLocalPicPath;
              }
              if (this.ExportImage(sPicPath1, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
                this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
            }
            if (this.imagetoPrint != null)
            {
              int left1 = this.PrintMargins.Left;
              int y1 = this.PrintMargins.Top;
              Size size2 = new Size(0, 0);
              if (this.objCurrentPrintJob.pageSplitCount == 1)
              {
                if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height > this.imagetoPrint.Width)
                {
                  int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                    int num = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    y1 = y1 + size3.Height + size4.Height + size1.Height;
                    height = height - size3.Height - size4.Height - size1.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) num, (float) size1.Height);
                    RectangleF layoutRectangle2 = new RectangleF((float) left1, (float) this.PrintMargins.Top, (float) num, (float) size3.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                  }
                  else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  {
                    int num = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    y1 += size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) num, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    height -= size2.Height;
                  }
                  Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = (int) Math.Round((Decimal) height * num1, 4);
                  if (width > intPrintWidth)
                  {
                    Decimal num2 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Height, (Decimal) this.imagetoPrint.Width), 4);
                    int num3 = height;
                    width = intPrintWidth;
                    height = (int) ((Decimal) width * num2);
                    y1 += (num3 - height) / 2;
                  }
                  int x = left1 + (intPrintWidth - width) / 2;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y1, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
                  left1 = this.PrintMargins.Left;
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width)
                {
                  int num1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num2);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat + Environment.NewLine);
                    Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size3.Height + size4.Height + size1.Height;
                    num1 = num1 - size3.Height - size4.Height - size1.Height;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    RectangleF layoutRectangle1 = new RectangleF((float) left1, (float) this.PrintMargins.Top, (float) num3, (float) size3.Height);
                    RectangleF layoutRectangle2 = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                  }
                  else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  {
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    y1 += size1.Height;
                    num1 -= size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num4 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int height = (int) Math.Round((Decimal) num2 / num4, 4);
                  y1 += (num1 - height) / 2;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, num2, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                }
                if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height > this.imagetoPrint.Width)
                {
                  int height = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size3.Height + size4.Height + size1.Height;
                    height = height - size3.Height - size4.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size3.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  {
                    y1 += size1.Height;
                    height -= size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    height -= size2.Height;
                  }
                  Decimal num = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = (int) Math.Round((Decimal) height * num, 4);
                  int x = left1 + (intPrintWidth - width) / 2;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y1, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (intPrintWidth - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height < this.imagetoPrint.Width)
                {
                  int num1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size3.Height + size4.Height + size1.Height;
                    num1 = num1 - size3.Height - size4.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size3.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  {
                    y1 += size1.Height;
                    num1 -= size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num2 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int height = (int) Math.Round((Decimal) intPrintWidth / num2, 4);
                  int width = intPrintWidth;
                  if (height > num1)
                  {
                    height = num1;
                    width = (int) ((Decimal) height * num2);
                    left1 += (intPrintWidth - width) / 2;
                  }
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (width - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Top - size2.Height));
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
                {
                  int left2 = this.PrintMargins.Left;
                  int y2 = this.PrintMargins.Top;
                  int height1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                    y2 = y2 + size3.Height + size4.Height + size1.Height;
                    height1 = height1 - size3.Height - size4.Height - size1.Height;
                    int left3 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int width = e.PageBounds.Width;
                    int left4 = this.PrintMargins.Left;
                    int right = this.PrintMargins.Right;
                    int height2 = size3.Height;
                    int height3 = size4.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) intPrintWidth, (float) size3.Height);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size3.Height), (float) intPrintWidth, (float) size4.Height);
                    RectangleF layoutRectangle3 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle3, format1);
                  }
                  else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  {
                    y2 += size1.Height;
                    height1 -= size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    height1 -= size2.Height;
                  }
                  Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int num2 = (int) Math.Round((Decimal) intPrintWidth / num1, 4);
                  int width1 = intPrintWidth;
                  if (num2 > height1)
                  {
                    width1 = (int) ((Decimal) height1 * num1);
                    left2 += (intPrintWidth - width1) / 2;
                  }
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left2, y2, width1, height1));
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Top - size2.Height));
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
                {
                  int num1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num2);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size3.Height + size4.Height + size1.Height;
                    num1 = num1 - size3.Height - size4.Height - size1.Height;
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    int num4 = size3.Height + size4.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num3, (float) num4);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size3.Height + size4.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size4.Width) / 2), (float) (this.PrintMargins.Top + size3.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                  }
                  else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  {
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    y1 += size1.Height;
                    num1 -= size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left2, (float) top, (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  }
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num5 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int height = (int) Math.Round((Decimal) num2 / num5, 4);
                  int y2 = y1 + (num1 - height) / 2;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y2, num2, height));
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                }
              }
              else if (this.objCurrentPrintJob.pageSplitCount == 2)
              {
                if (this.imagetoPrint.Width < this.imagetoPrint.Height)
                {
                  int num1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size4.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num3 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int num4 = num1;
                  int width = (int) Math.Round((Decimal) num1 * num3, 4) * 2;
                  int num5 = num4 * 2;
                  while (width > intPrintWidth || num5 > num1 * 2)
                  {
                    num5 -= 10;
                    width -= 10;
                  }
                  if (this.srcY == 0)
                    y1 += (num1 * 2 - num5) / 2;
                  int height = num5 / 2;
                  int x = left1 + (intPrintWidth - width) / 2;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
                  this.srcY = this.imagetoPrint.Height / 2;
                }
                else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
                {
                  int num1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num3 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = intPrintWidth;
                  int height = (int) Math.Round((Decimal) intPrintWidth * num3, 4);
                  if (height > num1)
                    height = num1;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (intPrintWidth - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                  this.srcX = this.imagetoPrint.Width / 2;
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
                {
                  int num1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    int num4 = size4.Height + size5.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num3, (float) num4);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num5 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = intPrintWidth;
                  int height = (int) Math.Round((Decimal) intPrintWidth * num5, 4);
                  if (height < num1)
                    height = num1;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                  this.srcX = this.imagetoPrint.Width / 2;
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
                {
                  int num1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    int num4 = size4.Height + size5.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num3, (float) num4);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num5 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int num6 = num1;
                  if ((int) Math.Round((Decimal) num1 / num5, 4) > intPrintWidth)
                    num6 = (int) ((Decimal) intPrintWidth * num5);
                  int width = intPrintWidth;
                  int height = (int) ((Decimal) width * num5) / 2;
                  int y2 = y1 + (num1 - height) / 2;
                  int x = left1 + (intPrintWidth - width) / 2;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x, y2, width, height), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
                  this.srcY = this.imagetoPrint.Height / 2;
                }
                ++this.splitPageCounter;
                if (this.splitPageCounter < this.objCurrentPrintJob.pageSplitCount)
                {
                  if (this.imagetoPrint.Height == this.imagetoPrint.Width)
                  {
                    if (this.objCurrentPrintJob.sOrientation == "0")
                      ++this.headerPgRowCounter;
                    else if (this.objCurrentPrintJob.sOrientation == "1")
                      ++this.headerPgColCounter;
                  }
                  else if (this.imagetoPrint.Width < this.imagetoPrint.Height)
                    ++this.headerPgRowCounter;
                  else
                    ++this.headerPgColCounter;
                  e.HasMorePages = true;
                  return;
                }
                e.HasMorePages = false;
                this.splitPageCounter = 0;
                this.headerPgColCounter = 1;
                this.headerPgRowCounter = 1;
              }
              else if (this.objCurrentPrintJob.pageSplitCount == 4)
              {
                if (this.imagetoPrint.Width < this.imagetoPrint.Height)
                {
                  int num1 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num1);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    height = height - size4.Height - size5.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) num1, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num1 - size4.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num1 - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      height -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) num1, (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    height -= size2.Height;
                  }
                  Decimal num3 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  if ((int) Math.Round((Decimal) height * num3, 4) > num1)
                  {
                    int num4 = (int) ((Decimal) num1 / num3);
                  }
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, num1, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
                {
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int num1 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num3 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = intPrintWidth;
                  int height = (int) Math.Round((Decimal) width / num3, 4);
                  if (height > num1)
                  {
                    height = num1;
                    width = (int) ((Decimal) height * num3);
                  }
                  if (this.splitPageCounter == 0)
                  {
                    y1 += num1 - height;
                    left1 += intPrintWidth - width;
                  }
                  else if (this.splitPageCounter == 1)
                    y1 += num1 - height;
                  else if (this.splitPageCounter == 2)
                    left1 += intPrintWidth - width;
                  else if (this.splitPageCounter == 3)
                    y1 += num1 - height;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (intPrintWidth - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
                {
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int num1 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    int num4 = size4.Height + size5.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num3, (float) num4);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num5 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = intPrintWidth;
                  int height = (int) Math.Round((Decimal) width / num5, 4);
                  if (height > num1)
                  {
                    height = num1;
                    width = (int) ((Decimal) height * num5);
                  }
                  if (this.splitPageCounter == 0)
                  {
                    y1 += num1 - height;
                    left1 += intPrintWidth - width;
                  }
                  else if (this.splitPageCounter == 1)
                    y1 += num1 - height;
                  else if (this.splitPageCounter == 2)
                    left1 += intPrintWidth - width;
                  else if (this.splitPageCounter == 3)
                    y1 += num1 - height;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
                {
                  int num1 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, num1);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    height = height - size4.Height - size5.Height - size1.Height;
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    int num4 = size4.Height + size5.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num3, (float) num4);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num1 - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      height -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    height -= size2.Height;
                  }
                  Decimal num5 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  if ((int) Math.Round((Decimal) height * num5, 4) > num1)
                  {
                    int num6 = (int) ((Decimal) num1 / num5);
                  }
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, num1, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                }
                ++this.splitPageCounter;
                if (this.splitPageCounter < this.objCurrentPrintJob.pageSplitCount)
                {
                  if (this.splitPageCounter % 2 == 1)
                    this.srcX = this.imagetoPrint.Width / 2;
                  ++this.headerPgColCounter;
                  if (this.splitPageCounter == 2)
                  {
                    ++this.headerPgRowCounter;
                    this.headerPgColCounter = 1;
                    this.srcY = this.imagetoPrint.Height / 2;
                    this.srcX = 0;
                  }
                  e.HasMorePages = true;
                  return;
                }
                e.HasMorePages = false;
                this.splitPageCounter = 0;
                this.headerPgColCounter = 1;
                this.headerPgRowCounter = 1;
                this.srcX = 0;
                this.srcY = 0;
              }
              else if (this.objCurrentPrintJob.pageSplitCount == 8)
              {
                if (this.imagetoPrint.Width < this.imagetoPrint.Height)
                {
                  int num1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num3 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int height = num1;
                  int width = (int) Math.Round((Decimal) num1 / num3, 4);
                  if (width > intPrintWidth)
                  {
                    width = intPrintWidth;
                    height = (int) ((Decimal) width * num3);
                  }
                  if (this.splitPageCounter % 2 == 0)
                    left1 += intPrintWidth - width;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
                {
                  int num1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size4.Width) / 2), (float) this.PrintMargins.Top);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) left1, (float) (y1 - size1.Height), (float) intPrintWidth, (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num3 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = intPrintWidth;
                  int height = (int) Math.Round((Decimal) intPrintWidth * num3, 4);
                  if (height > num1)
                    height = num1;
                  if (this.splitPageCounter < 4)
                    y1 += num1 - height;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (intPrintWidth - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
                {
                  int num1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    int num4 = size4.Height + size5.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num3, (float) num4);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Width - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num5 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int width = intPrintWidth;
                  int height = (int) Math.Round((Decimal) intPrintWidth * num5, 4);
                  if (height > num1)
                    height = num1;
                  if (this.splitPageCounter < 4)
                    y1 += num1 - height;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y1, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size2.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
                }
                else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
                {
                  int num1 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
                  int intPrintWidth = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
                  try
                  {
                    if (strPicMemoValue.Length > 0)
                    {
                      string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
                      size1.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, intPrintWidth);
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  Size size3 = TextRenderer.MeasureText("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont);
                  int num2;
                  if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                  {
                    string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                    Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                    Size size5 = TextRenderer.MeasureText(str, this.previewFont);
                    y1 = y1 + size4.Height + size5.Height + size1.Height;
                    num1 = num1 - size4.Height - size5.Height - size1.Height;
                    int left2 = this.PrintMargins.Left;
                    int top = this.PrintMargins.Top;
                    int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                    int num4 = size4.Height + size5.Height;
                    RectangleF layoutRectangle1 = new RectangleF((float) left2, (float) top, (float) num3, (float) num4);
                    RectangleF layoutRectangle2 = new RectangleF((float) left2, (float) (top + size4.Height + size5.Height), (float) num3, (float) size1.Height);
                    e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                    e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (intPrintWidth - size5.Width) / 2), (float) (this.PrintMargins.Top + size4.Height));
                    e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                    num2 = this.PrintMargins.Top;
                  }
                  else
                  {
                    if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                    {
                      y1 += size1.Height;
                      num1 -= size1.Height;
                      RectangleF layoutRectangle = new RectangleF((float) this.PrintMargins.Left, (float) this.PrintMargins.Top, (float) (e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right), (float) size1.Height);
                      e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                    }
                    num2 = size3.Height;
                  }
                  if (this.objCurrentPrintJob.sPrintPgNos == "1")
                    e.Graphics.DrawString("Row = " + (object) this.headerPgRowCounter + ", Col = " + (object) this.headerPgColCounter, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PaperSize.Height - this.PrintMargins.Right - size3.Width), (float) num2);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  {
                    size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
                    num1 -= size2.Height;
                  }
                  Decimal num5 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                  int num6 = num1;
                  if ((int) Math.Round((Decimal) num1 / num5, 4) > intPrintWidth)
                    num6 = (int) ((Decimal) intPrintWidth * num5);
                  int width = intPrintWidth;
                  int height = (int) ((Decimal) width * num5) / 2;
                  int y2 = y1 + (num1 - height) / 2;
                  if (this.splitPageCounter % 2 == 0)
                    left1 += intPrintWidth - width;
                  e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left1, y2, width, height), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
                  if (this.objCurrentPrintJob.copyRightField != string.Empty)
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size2.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
                }
                ++this.splitPageCounter;
                if (this.splitPageCounter < this.objCurrentPrintJob.pageSplitCount)
                {
                  if (this.imagetoPrint.Width < this.imagetoPrint.Height)
                  {
                    ++this.headerPgColCounter;
                    if (this.splitPageCounter % 2 == 0)
                    {
                      this.headerPgColCounter = 1;
                      ++this.headerPgRowCounter;
                      this.srcY += this.imagetoPrint.Height / 4;
                      this.srcX = 0;
                    }
                    else
                      this.srcX = this.imagetoPrint.Width / 2;
                  }
                  else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
                  {
                    ++this.headerPgColCounter;
                    if (this.splitPageCounter % 4 == 0)
                    {
                      this.headerPgColCounter = 1;
                      ++this.headerPgRowCounter;
                      this.srcY += this.imagetoPrint.Height / 2;
                      this.srcX = 0;
                    }
                    else
                      this.srcX += this.imagetoPrint.Width / 4;
                  }
                  else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
                  {
                    if (this.objCurrentPrintJob.sOrientation == "1")
                    {
                      ++this.headerPgColCounter;
                      if (this.splitPageCounter % 4 == 0)
                      {
                        this.headerPgColCounter = 1;
                        ++this.headerPgRowCounter;
                        this.srcY += this.imagetoPrint.Height / 2;
                        this.srcX = 0;
                      }
                      else
                        this.srcX += this.imagetoPrint.Width / 4;
                    }
                    else if (this.objCurrentPrintJob.sOrientation == "0")
                    {
                      ++this.headerPgColCounter;
                      if (this.splitPageCounter % 2 == 0)
                      {
                        this.headerPgColCounter = 1;
                        ++this.headerPgRowCounter;
                        this.srcY += this.imagetoPrint.Height / 4;
                        this.srcX = 0;
                      }
                      else
                        this.srcX = this.imagetoPrint.Width / 2;
                    }
                  }
                  e.HasMorePages = true;
                  return;
                }
                e.HasMorePages = false;
                this.splitPageCounter = 0;
                this.headerPgColCounter = 1;
                this.headerPgRowCounter = 1;
                this.srcX = 0;
                this.srcY = 0;
              }
              if (this.objDjVuCtl.GetPageCount() > this.iMultiImgCounter)
              {
                ++this.iMultiImgCounter;
                e.HasMorePages = true;
                return;
              }
              ++this.ImagePrinted;
              e.HasMorePages = false;
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
      if (this.objCurrentPrintJob.sPrintPic == "0" && this.arrPrintJobs.Count > 0 && !this.bHasPages)
      {
        this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
        this.arrPrintJobs.RemoveAt(0);
        while (this.objCurrentPrintJob.sLocalListPath == string.Empty)
        {
          if (this.arrPrintJobs.Count > 0)
          {
            this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
            this.arrPrintJobs.RemoveAt(0);
          }
          else
          {
            e.HasMorePages = false;
            return;
          }
        }
      }
      this.bHasPages = false;
      if (this.objCurrentPrintJob.sPrintList == "1")
      {
        if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
        {
          try
          {
            if (this.PartListTable == null)
            {
              this.iPartsListRowCount = 0;
              if (!System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath) && this.objCurrentPrintJob.sLocalListPath != string.Empty)
              {
                new Download().DownloadFile(this.objCurrentPrintJob.sServerListPath, this.objCurrentPrintJob.sLocalListPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
                int num = 2;
                int millisecondsTimeout = 500;
                for (int index = 0; index < num; ++index)
                {
                  try
                  {
                    if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath))
                    {
                      if (PreviewManager.UnZipFile(this.objCurrentPrintJob.sLocalListPath).ToInt32() == 1)
                      {
                        if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath.ToLower().Replace(".zip", ".xml")))
                          break;
                      }
                      Thread.Sleep(millisecondsTimeout);
                    }
                    else
                      break;
                  }
                  catch
                  {
                  }
                }
              }
              this.LoadPartsListData(this.objCurrentPrintJob.sLocalListPath);
            }
            if (this.PartListTable != null)
            {
              if (this.PartListTable.Rows.Count > 0)
              {
                if (!this.bNewPageForListLoaded && this.objCurrentPrintJob.sPrintPic == "1")
                {
                  this.bNewPageForListLoaded = true;
                  e.HasMorePages = true;
                  e.PageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
                  return;
                }
                int iCellHeight = 23;
                Size size2 = new Size(0, 0);
                int num1 = 0;
                int left1 = this.PrintMargins.Left;
                int y1 = this.PrintMargins.Top;
                float left2 = (float) this.PrintMargins.Left;
                float top = (float) this.PrintMargins.Top;
                int count = this.PartListTable.Columns.Count;
                int num2 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                int num3 = num2 / count;
                int num4 = 0;
                int iColWidth = 100;
                foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
                {
                  if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                  {
                    int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                    if (int32 < iColWidth)
                      iColWidth = int32;
                    num4 += int32;
                  }
                }
                int num5 = 0;
                int num6 = 100;
                foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
                {
                  if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                  {
                    int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                    if (num2 < num5 + int32)
                    {
                      num6 = Math.Abs(num2 - num5);
                      int num7 = num5 + int32;
                      break;
                    }
                    num5 += int32;
                  }
                }
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDatePL.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
                  Size size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
                  Size size4 = TextRenderer.MeasureText(str, this.previewFont);
                  y1 = y1 + size3.Height + size4.Height;
                  float height = (float) y1;
                  RectangleF layoutRectangle = new RectangleF(left2, top, (float) num2, height);
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                  e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size4.Width) / 2), (float) (y1 - size4.Height));
                }
                if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  size2 = TextRenderer.MeasureText(this.copyRightField, this.previewFont);
                if (this.bIsOldINIPL)
                  this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
                else
                  this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                int num8 = 0;
                int num9;
                for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                {
                  StringFormat format3 = new StringFormat();
                  try
                  {
                    format3.Alignment = StringAlignment.Center;
                    format3.LineAlignment = StringAlignment.Center;
                    if (!this.bIsOldINIPL)
                    {
                      this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                      format2.Alignment = StringAlignment.Center;
                      format2.LineAlignment = StringAlignment.Center;
                      num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                    }
                  }
                  catch (Exception ex)
                  {
                    format3.Alignment = StringAlignment.Center;
                  }
                  if (num8 + num3 <= num2)
                  {
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num3, iCellHeight));
                    e.Graphics.DrawRectangle(Pens.Black, left1, y1, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num3, (float) iCellHeight), format3);
                    left1 += num3;
                    num8 += num3;
                  }
                  else
                  {
                    num3 = num6;
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num3, iCellHeight));
                    e.Graphics.DrawRectangle(Pens.Black, left1, y1, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num3, (float) iCellHeight), format2);
                    num9 = left1 + num3;
                    break;
                  }
                }
                int left3 = this.PrintMargins.Left;
                int y2 = y1 + iCellHeight;
                while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
                {
                  if (this.bIsOldINIPL)
                    this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
                  else
                    this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                  if (this.objCurrentPrintJob.sOrientation == "0")
                  {
                    if (y2 + iCellHeight > this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height)
                    {
                      int left4 = this.PrintMargins.Left;
                      num1 = this.PrintMargins.Top;
                      e.HasMorePages = true;
                      this.bHasPages = true;
                      this.objCurrentPrintJob.sZoom = "0";
                      if (!(this.objCurrentPrintJob.copyRightField != string.Empty))
                        return;
                      e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left4 + (num2 - size2.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size2.Height));
                      return;
                    }
                  }
                  else if (y2 + iCellHeight > this.PaperSize.Width - this.PrintMargins.Left - size2.Height)
                  {
                    int left4 = this.PrintMargins.Left;
                    num1 = this.PrintMargins.Top;
                    e.HasMorePages = true;
                    this.bHasPages = true;
                    this.objCurrentPrintJob.sZoom = "0";
                    if (!(this.objCurrentPrintJob.copyRightField != string.Empty))
                      return;
                    e.Graphics.DrawString(this.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left4 + (num2 - size2.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size2.Height));
                    return;
                  }
                  int num7 = 0;
                  for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                  {
                    StringFormat format3 = new StringFormat();
                    try
                    {
                      format3.Alignment = StringAlignment.Center;
                      format3.LineAlignment = StringAlignment.Center;
                      if (!this.bIsOldINIPL)
                      {
                        string str = this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                        format2.Alignment = StringAlignment.Center;
                        format2.LineAlignment = StringAlignment.Center;
                        if (str == "L")
                        {
                          format3.Alignment = StringAlignment.Near;
                          format2.Alignment = StringAlignment.Near;
                        }
                        else if (str == "R")
                        {
                          format3.Alignment = StringAlignment.Far;
                          format2.Alignment = StringAlignment.Far;
                        }
                        num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                      }
                    }
                    catch (Exception ex)
                    {
                      format3.Alignment = StringAlignment.Center;
                    }
                    if (num7 + num3 <= num2)
                    {
                      e.Graphics.DrawRectangle(Pens.Black, left3, y2, num3, iCellHeight);
                      e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left3, (float) y2, (float) num3, (float) iCellHeight), format3);
                      left3 += num3;
                      num7 += num3;
                    }
                    else
                    {
                      num3 = num6;
                      this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
                      e.Graphics.DrawRectangle(Pens.Black, left3, y2, num3, iCellHeight);
                      e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left3, (float) y2, (float) num3, (float) iCellHeight), format2);
                      num9 = left3 + num3;
                      break;
                    }
                  }
                  left3 = this.PrintMargins.Left;
                  ++this.iPartsListRowCount;
                  y2 += iCellHeight;
                }
                if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left3 + (num2 - size2.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size2.Height));
                e.HasMorePages = false;
                this.PartListTable = (DataTable) null;
              }
            }
          }
          catch
          {
          }
        }
      }
      if (this.arrPrintJobs.Count > 0)
      {
        if (this.objCurrentPrintJob.pageSplitCount != 1)
        {
          if (this.objCurrentPrintJob.pageSplitCount == 2 || this.objCurrentPrintJob.pageSplitCount == 8)
            e.PageSettings.Landscape = this.imagetoPrint.Width < this.imagetoPrint.Height;
          if (this.objCurrentPrintJob.pageSplitCount == 4)
            e.PageSettings.Landscape = this.imagetoPrint.Width >= this.imagetoPrint.Height;
        }
        try
        {
          if (this.imagetoPrint != null)
          {
            this.imagetoPrint.Dispose();
            System.IO.File.Delete(this.sExportedImagePath);
          }
        }
        catch
        {
        }
        this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
        if (this.arrPrintJobs.Count == 1 && this.objCurrentPrintJob.sPrintPic == "0" && this.objCurrentPrintJob.sLocalListPath == string.Empty)
        {
          e.HasMorePages = false;
        }
        else
        {
          e.HasMorePages = true;
          this.bNewPageForListLoaded = false;
          this.bPreviewImageNotExported = true;
          this.ImagePrinted = 0;
        }
      }
      else
      {
        try
        {
          if (this.imagetoPrint == null)
            return;
          this.imagetoPrint.Dispose();
          System.IO.File.Delete(this.sExportedImagePath);
        }
        catch
        {
        }
      }
    }

    private void doc_PrinthalfPage(object sender, PrintPageEventArgs e)
    {
      try
      {
        this.iTotalPageWidth = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
        Size size1 = new Size(0, 0);
        Size size2 = new Size(0, 0);
        Size size3 = new Size(0, 0);
        Size size4 = new Size(0, 0);
        StringFormat format1 = new StringFormat();
        format1.Alignment = StringAlignment.Center;
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string str1 = string.Empty;
        if (this.ImagePrinted == 0)
        {
          int x1 = this.PrintMargins.Left;
          int y1 = this.PrintMargins.Top;
          if (this.arrPrintJobs.Count > 0 && this.bPreviewImageNotExported)
          {
            this.bPreviewImageNotExported = false;
            this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
            this.arrPrintJobs.RemoveAt(0);
            this.sExportedImagePath = string.Empty;
            this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
            if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalPicPath))
            {
              this.objDjVuCtl.SRC = this.objCurrentPrintJob.sLocalPicPath;
            }
            else
            {
              new Download().DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
              this.objDjVuCtl.SRC = this.objCurrentPrintJob.sLocalPicPath;
            }
            if (this.ExportImage(this.objDjVuCtl.SRC, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
              this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
          }
          string strPicMemoValue = this.objCurrentPrintJob.strPicMemoValue;
          try
          {
            if (strPicMemoValue.Length > 0)
            {
              string strTrimedMemo = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Substring(0, 1);
              size4.Height = this.GetPicMemoDrawingHeight(strPicMemoValue, strTrimedMemo, this.MemoFont, this.iTotalPageWidth);
            }
          }
          catch (Exception ex)
          {
          }
          if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
          {
            str1 = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
            size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
            size2 = TextRenderer.MeasureText(str1, this.previewFont);
          }
          else
            string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue);
          if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
            size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
          if (this.imagetoPrint != null)
          {
            if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height > this.imagetoPrint.Width)
            {
              Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
              y1 = y1 + size1.Height + size2.Height + size4.Height;
              RectangleF layoutRectangle = new RectangleF((float) x1, (float) (y1 - size4.Height), (float) num2, (float) size4.Height);
              if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                height /= 2;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) this.PrintMargins.Top);
                e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size2.Width) / 2), (float) (this.PrintMargins.Top + size1.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
              int width = (int) Math.Round((Decimal) height * num1, 4);
              x1 += (num2 - width) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x1, y1, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
            }
            if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width)
            {
              Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int width = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
              int num3 = y1 + size1.Height + size2.Height + size4.Height;
              RectangleF layoutRectangle = new RectangleF((float) x1, (float) (num3 - size4.Height), (float) width, (float) size4.Height);
              if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                num2 /= 2;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (width - size1.Width) / 2), (float) this.PrintMargins.Top);
                e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (width - size2.Width) / 2), (float) (this.PrintMargins.Top + size1.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
              int height = (int) Math.Round((Decimal) width / num1, 4);
              int num4 = width;
              if (height > num2)
              {
                height = num2;
                width = (int) Math.Round((Decimal) height * num1, 4);
              }
              y1 = num3 + (num2 - height) / 2;
              x1 += (num4 - width) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x1, y1, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
            }
            if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height > this.imagetoPrint.Width)
            {
              if (this.objCurrentPrintJob.sPrintSideBySide == "0")
              {
                Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                int height = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
                y1 = y1 + size1.Height + size2.Height + size4.Height;
                RectangleF layoutRectangle = new RectangleF((float) x1, (float) (y1 - size4.Height), (float) num2, (float) size4.Height);
                if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                  height /= 2;
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size1.Width) / 2), (float) this.PrintMargins.Top);
                  e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size2.Width) / 2), (float) (this.PrintMargins.Top + size1.Height));
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                }
                else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                int width = (int) Math.Round((Decimal) height * num1, 4);
                x1 += (num2 - width) / 2;
                e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x1, y1, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              }
              else
              {
                Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                int num2 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
                int num3 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
                if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                  num2 /= 2;
                int width = num2 - 20;
                int num4 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
                int num5 = y1 + size1.Height + size2.Height + size4.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) x1, (float) this.PrintMargins.Top, (float) num3, (float) size1.Height);
                RectangleF layoutRectangle2 = new RectangleF((float) x1, (float) (this.PrintMargins.Top + size1.Height), (float) num3, (float) size2.Height);
                RectangleF layoutRectangle3 = new RectangleF((float) x1, (float) (num5 - size4.Height), (float) num3, (float) size4.Height);
                int height;
                for (height = (int) Math.Round((Decimal) width / num1, 4); num4 <= height; height = (int) Math.Round((Decimal) width / num1, 4))
                  width -= 10;
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                  e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle3, format1);
                }
                else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle3, format1);
                if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
                if (this.objCurrentPrintJob.sLocalListPath == string.Empty || !System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath))
                  x1 = (this.PaperSize.Height - width) / 2;
                y1 = num5 + (num4 - height) / 2;
                e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x1, y1, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              }
            }
            if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height < this.imagetoPrint.Width)
            {
              if (this.objCurrentPrintJob.sPrintSideBySide == "0")
              {
                Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                int height = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size1.Height - size2.Height - size3.Height - size4.Height;
                int y2 = y1 + size1.Height + size2.Height + size4.Height;
                RectangleF layoutRectangle = new RectangleF((float) x1, (float) (y2 - size4.Height), (float) num2, (float) size4.Height);
                if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                  height /= 2;
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size1.Width) / 2), (float) this.PrintMargins.Top);
                  e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size2.Width) / 2), (float) (this.PrintMargins.Top + size1.Height));
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                }
                else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                int width = (int) Math.Round((Decimal) height * num1, 4);
                int x2 = x1 + (num2 - width) / 2;
                e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x2, y2, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              }
              else
              {
                Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                int width = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
                int num2 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
                if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                  width = width / 2 - 20;
                int num3 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
                int num4 = y1 + size1.Height + size2.Height + size4.Height;
                RectangleF layoutRectangle = new RectangleF((float) x1, (float) (num4 - size4.Height), (float) num2, (float) size4.Height);
                int height;
                for (height = (int) Math.Round((Decimal) width / num1, 4); num3 <= height; height = (int) Math.Round((Decimal) width / num1, 4))
                  width -= 10;
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size1.Width) / 2), (float) this.PrintMargins.Top);
                  e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size2.Width) / 2), (float) (this.PrintMargins.Top + size1.Height));
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                }
                else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  e.Graphics.DrawString(this.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
                int y2 = (num4 + (num3 - height) / 2) / 2;
                if (this.objCurrentPrintJob.sLocalListPath != string.Empty && this.objCurrentPrintJob.sLocalListPath == string.Empty)
                  x1 = this.PaperSize.Height - width;
                e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x1, y2, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              }
            }
            else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height == this.imagetoPrint.Width)
            {
              if (this.objCurrentPrintJob.sPrintSideBySide == "0")
              {
                Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                int num2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
                int height = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
                int y2 = y1 + size1.Height + size2.Height + size4.Height;
                RectangleF layoutRectangle = new RectangleF((float) x1, (float) (y2 - size4.Height), (float) num2, (float) size4.Height);
                if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                  height /= 2;
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size1.Width) / 2), (float) this.PrintMargins.Top);
                  e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (x1 + (num2 - size2.Width) / 2), (float) (this.PrintMargins.Top + size1.Height));
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                }
                else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
                int width = (int) Math.Round((Decimal) height * num1, 4);
                int x2 = x1 + (num2 - width) / 2;
                e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x2, y2, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              }
              else
              {
                Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
                int num2 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
                int num3 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
                if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                  num2 /= 2;
                int width = num2 - 20;
                int num4 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
                int num5 = y1 + size1.Height + size2.Height + size4.Height;
                RectangleF layoutRectangle1 = new RectangleF((float) x1, (float) this.PrintMargins.Top, (float) num3, (float) size1.Height);
                RectangleF layoutRectangle2 = new RectangleF((float) x1, (float) (this.PrintMargins.Top + size1.Height), (float) num3, (float) size2.Height);
                RectangleF layoutRectangle3 = new RectangleF((float) x1, (float) (num5 - size4.Height), (float) num3, (float) size4.Height);
                int height;
                for (height = (int) Math.Round((Decimal) width / num1, 4); num4 <= height; height = (int) Math.Round((Decimal) width / num1, 4))
                  width -= 10;
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle1, format1);
                  e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle2, format1);
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle3, format1);
                }
                else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                  e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle3, format1);
                if (this.objCurrentPrintJob.copyRightField != string.Empty)
                  e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
                if (this.objCurrentPrintJob.sLocalListPath == string.Empty || !System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath))
                  x1 = (this.PaperSize.Height - width) / 2;
                int y2 = num5 + (num4 - height) / 2;
                e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x1, y2, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
              }
            }
            else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height == this.imagetoPrint.Width)
            {
              Decimal num1 = Math.Round(Decimal.Divide((Decimal) this.imagetoPrint.Width, (Decimal) this.imagetoPrint.Height), 4);
              int num2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height;
              int y2 = y1 + size1.Height + size2.Height + size4.Height;
              RectangleF layoutRectangle = new RectangleF((float) x1, (float) (y2 - size4.Height), (float) num2, (float) size4.Height);
              if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
                height /= 2;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size1.Width) / 2), (float) this.PrintMargins.Top);
                e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Width - size2.Width) / 2), (float) (this.PrintMargins.Top + size1.Height));
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
              }
              else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
                e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format1);
              int width = (int) Math.Round((Decimal) height * num1, 4);
              int x2 = x1 + (num2 - width) / 2;
              e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(x2, y2, width, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
            }
            ++this.ImagePrinted;
            try
            {
              this.imagetoPrint.Dispose();
              System.IO.File.Delete(this.sExportedImagePath);
            }
            catch
            {
            }
          }
        }
        if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
        {
          StringFormat format2 = new StringFormat()
          {
            FormatFlags = StringFormatFlags.NoWrap,
            Trimming = StringTrimming.Character
          };
          if (this.PartListTable == null)
          {
            this.iPartsListRowCount = 0;
            if (!System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath))
            {
              new Download().DownloadFile(this.objCurrentPrintJob.sServerListPath, this.objCurrentPrintJob.sLocalListPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
              int num = 2;
              int millisecondsTimeout = 500;
              for (int index = 0; index < num; ++index)
              {
                try
                {
                  if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath))
                  {
                    if (PreviewManager.UnZipFile(this.objCurrentPrintJob.sLocalListPath).ToInt32() == 1)
                    {
                      if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath.ToLower().Replace(".zip", ".xml")))
                        break;
                    }
                    Thread.Sleep(millisecondsTimeout);
                  }
                  else
                    break;
                }
                catch
                {
                }
              }
            }
            this.LoadPartsListData(this.objCurrentPrintJob.sLocalListPath);
          }
          if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
          {
            str1 = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
            size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
            size2 = TextRenderer.MeasureText(str1, this.previewFont);
          }
          if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
            size3 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
          if (this.objCurrentPrintJob.sPrintSideBySide == "1" && this.objCurrentPrintJob.sOrientation == "1" && this.PageCounter == 1)
          {
            if (this.PartListTable.Rows.Count > 0)
            {
              int num1 = 0;
              int x1 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
              int iCellHeight = 23;
              int y1;
              if (this.PageCounter > 1)
              {
                y1 = this.PrintMargins.Top + size1.Height + size2.Height + 20;
              }
              else
              {
                int width = this.PaperSize.Width;
                int left = this.PrintMargins.Left;
                int right = this.PrintMargins.Right;
                int height1 = size1.Height;
                int height2 = size2.Height;
                int height3 = size3.Height;
                int height4 = size4.Height;
                y1 = this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height;
              }
              int num2 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 - 20;
              StringFormat stringFormat = new StringFormat()
              {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
              };
              int count = this.PartListTable.Columns.Count;
              int num3 = num2 / count;
              int num4 = 0;
              int iColWidth = 100;
              foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
              {
                if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                {
                  int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                  if (int32 < iColWidth)
                    iColWidth = int32;
                  num4 += int32;
                }
              }
              int num5 = 0;
              int num6 = 0;
              foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
              {
                if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                {
                  int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                  if (num2 < num5 + int32)
                  {
                    num6 = Math.Abs(num2 - num5);
                    int num7 = num5 + int32;
                    break;
                  }
                  num5 += int32;
                }
              }
              if (this.bIsOldINIPL)
                this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
              else
                this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
              int num8 = 0;
              int num9;
              for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
              {
                StringFormat format3 = new StringFormat();
                try
                {
                  format3.Alignment = StringAlignment.Center;
                  format3.LineAlignment = StringAlignment.Center;
                  if (!this.bIsOldINIPL)
                  {
                    this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                    format2.Alignment = StringAlignment.Center;
                    format2.LineAlignment = StringAlignment.Center;
                    num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                  }
                }
                catch (Exception ex)
                {
                }
                if (num8 + num3 <= num2)
                {
                  e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(x1, y1, num3, iCellHeight));
                  e.Graphics.DrawRectangle(Pens.Black, x1, y1, num3, iCellHeight);
                  e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) x1, (float) y1, (float) num3, (float) iCellHeight), format3);
                  x1 += num3;
                  num8 += num3;
                }
                else
                {
                  num3 = num6;
                  e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(x1, y1, num3, iCellHeight));
                  e.Graphics.DrawRectangle(Pens.Black, x1, y1, num3, iCellHeight);
                  e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) x1, (float) y1, (float) num3, (float) iCellHeight), format2);
                  num9 = x1 + num3;
                  break;
                }
              }
              int x2 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
              int num10;
              if (this.PageCounter > 1)
              {
                num10 = this.PrintMargins.Top + size1.Height + size2.Height + 20;
              }
              else
              {
                int width = this.PaperSize.Width;
                int left = this.PrintMargins.Left;
                int right = this.PrintMargins.Right;
                int height1 = size1.Height;
                int height2 = size2.Height;
                int height3 = size3.Height;
                int height4 = size4.Height;
                num10 = this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height;
              }
              int y2 = num10 + iCellHeight;
              while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
              {
                if (this.bIsOldINIPL)
                  this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
                else
                  this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                if (y2 + iCellHeight > this.PaperSize.Width - this.PrintMargins.Left - size3.Height)
                {
                  num9 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
                  num1 = this.PrintMargins.Top + size1.Height + size2.Height + 20;
                  e.HasMorePages = true;
                  ++this.PageCounter;
                  return;
                }
                int num7 = 0;
                for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                {
                  StringFormat format3 = new StringFormat();
                  try
                  {
                    format3.Alignment = StringAlignment.Center;
                    format3.LineAlignment = StringAlignment.Center;
                    if (!this.bIsOldINIPL)
                    {
                      string str2 = this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                      format2.Alignment = StringAlignment.Center;
                      format2.LineAlignment = StringAlignment.Center;
                      if (str2 == "L")
                      {
                        format3.Alignment = StringAlignment.Near;
                        format2.Alignment = StringAlignment.Near;
                      }
                      else if (str2 == "R")
                      {
                        format3.Alignment = StringAlignment.Far;
                        format2.Alignment = StringAlignment.Far;
                      }
                      num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (num7 + num3 <= num2)
                  {
                    e.Graphics.DrawRectangle(Pens.Black, x2, y2, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) x2, (float) y2, (float) num3, (float) iCellHeight), format3);
                    x2 += num3;
                    num7 += num3;
                  }
                  else
                  {
                    num3 = num6;
                    e.Graphics.DrawRectangle(Pens.Black, x2, y2, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) x2, (float) y2, (float) num3, (float) iCellHeight), format2);
                    num9 = x2 + num3;
                    break;
                  }
                }
                x2 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
                ++this.iPartsListRowCount;
                y2 += iCellHeight;
              }
              e.HasMorePages = false;
              this.PageCounter = 1;
            }
          }
          else if (this.objCurrentPrintJob.sOrientation == "0" || this.PageCounter != 1)
          {
            if (this.objCurrentPrintJob.sPrintSideBySide == "0" || this.objCurrentPrintJob.sOrientation == "0")
            {
              if (this.PartListTable.Rows.Count > 0)
              {
                int num1 = 0;
                int left1 = this.PrintMargins.Left;
                int iCellHeight = 23;
                int y1 = this.PageCounter <= 1 ? (!(this.objCurrentPrintJob.sOrientation == "0") ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size1.Height - size2.Height - size3.Height - size4.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height) / 2 + this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height : this.PrintMargins.Top + size1.Height + size2.Height;
                int num2 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
                if (this.PageCounter > 1 && this.objCurrentPrintJob.sPrintHeaderFooter == "1")
                {
                  e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size1.Width) / 2), (float) (y1 - size1.Height - size2.Height));
                  e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size2.Width) / 2), (float) (y1 - size2.Height));
                }
                StringFormat stringFormat = new StringFormat()
                {
                  Alignment = StringAlignment.Center,
                  LineAlignment = StringAlignment.Center,
                  Trimming = StringTrimming.EllipsisCharacter
                };
                int count = this.PartListTable.Columns.Count;
                int num3 = num2 / count;
                int num4 = 0;
                int iColWidth = 100;
                foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
                {
                  if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                  {
                    int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                    if (int32 < iColWidth)
                      iColWidth = int32;
                    num4 += int32;
                  }
                }
                int num5 = 0;
                int num6 = 0;
                foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
                {
                  if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                  {
                    int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                    if (num2 < num5 + int32)
                    {
                      num6 = Math.Abs(num2 - num5);
                      int num7 = num5 + int32;
                      break;
                    }
                    num5 += int32;
                  }
                }
                if (this.bIsOldINIPL)
                  this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
                else
                  this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                int num8 = 0;
                int num9;
                for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                {
                  StringFormat format3 = new StringFormat();
                  try
                  {
                    format3.Alignment = StringAlignment.Center;
                    format3.LineAlignment = StringAlignment.Center;
                    if (!this.bIsOldINIPL)
                    {
                      this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                      format2.Alignment = StringAlignment.Center;
                      format2.LineAlignment = StringAlignment.Center;
                      num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (num8 + num3 <= num2)
                  {
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num3, iCellHeight));
                    e.Graphics.DrawRectangle(Pens.Black, left1, y1, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num3, (float) iCellHeight), format3);
                    left1 += num3;
                    num8 += num3;
                  }
                  else
                  {
                    num3 = num6;
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num3, iCellHeight));
                    e.Graphics.DrawRectangle(Pens.Black, left1, y1, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num3, (float) iCellHeight), format2);
                    num9 = left1 + num3;
                    break;
                  }
                }
                int left2 = this.PrintMargins.Left;
                int y2 = (this.PageCounter <= 1 ? (!(this.objCurrentPrintJob.sOrientation == "0") ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size1.Height - size2.Height - size3.Height - size4.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height) / 2 + this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height : this.PrintMargins.Top + size1.Height + size2.Height) + iCellHeight;
                while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
                {
                  if (this.bIsOldINIPL)
                    this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
                  else
                    this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                  if (this.objCurrentPrintJob.sOrientation == "0")
                  {
                    if (y2 + iCellHeight > this.PaperSize.Height - this.PrintMargins.Bottom - size3.Height)
                    {
                      num9 = this.PrintMargins.Left;
                      num1 = this.PrintMargins.Top + size1.Height + size2.Height;
                      e.HasMorePages = true;
                      ++this.PageCounter;
                      if (!(this.objCurrentPrintJob.sPrintHeaderFooter == "1"))
                        return;
                      e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size3.Height));
                      return;
                    }
                  }
                  else if (y2 + iCellHeight > this.PaperSize.Width - this.PrintMargins.Left - size3.Height)
                  {
                    num9 = this.PrintMargins.Left;
                    num1 = this.PrintMargins.Top;
                    e.HasMorePages = true;
                    ++this.PageCounter;
                    if (!(this.objCurrentPrintJob.sPrintHeaderFooter == "1"))
                      return;
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
                    return;
                  }
                  int num7 = 0;
                  for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                  {
                    StringFormat format3 = new StringFormat();
                    try
                    {
                      format3.Alignment = StringAlignment.Center;
                      format3.LineAlignment = StringAlignment.Center;
                      if (!this.bIsOldINIPL)
                      {
                        string str2 = this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                        format2.Alignment = StringAlignment.Center;
                        format2.LineAlignment = StringAlignment.Center;
                        if (str2 == "L")
                        {
                          format3.Alignment = StringAlignment.Near;
                          format2.Alignment = StringAlignment.Near;
                        }
                        else if (str2 == "R")
                        {
                          format3.Alignment = StringAlignment.Far;
                          format2.Alignment = StringAlignment.Far;
                        }
                        num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                      }
                    }
                    catch (Exception ex)
                    {
                    }
                    if (num7 + num3 <= num2)
                    {
                      e.Graphics.DrawRectangle(Pens.Black, left2, y2, num3, iCellHeight);
                      e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y2, (float) num3, (float) iCellHeight), format3);
                      left2 += num3;
                      num7 += num3;
                    }
                    else
                    {
                      num3 = num6;
                      e.Graphics.DrawRectangle(Pens.Black, left2, y2, num3, iCellHeight);
                      e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y2, (float) num3, (float) iCellHeight), format2);
                      num9 = left2 + num3;
                      break;
                    }
                  }
                  left2 = this.PrintMargins.Left;
                  ++this.iPartsListRowCount;
                  y2 += iCellHeight;
                }
                if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "0")
                  e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size3.Height));
                else if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "1")
                  e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
                e.HasMorePages = false;
                this.ImagePrinted = 0;
                this.PageCounter = 1;
              }
            }
            else
            {
              int num1 = this.PrintMargins.Top + size1.Height + size2.Height;
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size1.Width) / 2), (float) (num1 - size1.Height - size2.Height));
                e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size2.Width) / 2), (float) (num1 - size2.Height));
              }
              if (this.objCurrentPrintJob.copyRightField != string.Empty)
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) ((this.PaperSize.Height - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
              if (this.PartListTable.Rows.Count > 0)
              {
                int num2 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
                int left1 = this.PrintMargins.Left;
                int iCellHeight = 23;
                bool flag = false;
                int y1;
                if (this.PageCounter > 1)
                {
                  y1 = this.PrintMargins.Top + size1.Height + size2.Height + 20;
                }
                else
                {
                  int width = this.PaperSize.Width;
                  int left2 = this.PrintMargins.Left;
                  int right = this.PrintMargins.Right;
                  int height1 = size1.Height;
                  int height2 = size2.Height;
                  int height3 = size3.Height;
                  int height4 = size4.Height;
                  y1 = this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height;
                }
                int num3 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 - 20;
                StringFormat stringFormat = new StringFormat()
                {
                  Alignment = StringAlignment.Center,
                  LineAlignment = StringAlignment.Center,
                  Trimming = StringTrimming.EllipsisCharacter
                };
                int count = this.PartListTable.Columns.Count;
                int num4 = num3 / count;
                int num5 = 0;
                int iColWidth = 100;
                foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
                {
                  if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                  {
                    int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                    if (int32 < iColWidth)
                      iColWidth = int32;
                    num5 += int32;
                  }
                }
                int num6 = 0;
                int num7 = 0;
                foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
                {
                  if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                  {
                    int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                    if (num3 < num6 + int32)
                    {
                      num7 = Math.Abs(num3 - num6);
                      int num8 = num6 + int32;
                      break;
                    }
                    num6 += int32;
                  }
                }
                if (this.bIsOldINIPL)
                  this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num4);
                else
                  this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                int num9 = 0;
                for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                {
                  StringFormat format3 = new StringFormat();
                  try
                  {
                    format3.Alignment = StringAlignment.Center;
                    format3.LineAlignment = StringAlignment.Center;
                    if (!this.bIsOldINIPL)
                    {
                      this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                      format2.Alignment = StringAlignment.Center;
                      format2.LineAlignment = StringAlignment.Center;
                      num4 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (num9 + num4 <= num3)
                  {
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num4, iCellHeight));
                    e.Graphics.DrawRectangle(Pens.Black, left1, y1, num4, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num4, (float) iCellHeight), format3);
                    left1 += num4;
                    num9 += num4;
                  }
                  else
                  {
                    num4 = num7;
                    e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num4, iCellHeight));
                    e.Graphics.DrawRectangle(Pens.Black, left1, y1, num4, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num4, (float) iCellHeight), format2);
                    num2 = left1 + num4;
                    break;
                  }
                }
                int x1 = !flag ? this.PrintMargins.Left : (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
                int num10;
                if (this.PageCounter > 1)
                {
                  num10 = this.PrintMargins.Top + size1.Height + size2.Height + 20;
                }
                else
                {
                  int width = this.PaperSize.Width;
                  int left2 = this.PrintMargins.Left;
                  int right = this.PrintMargins.Right;
                  int height1 = size1.Height;
                  int height2 = size2.Height;
                  int height3 = size3.Height;
                  int height4 = size4.Height;
                  num10 = this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height;
                }
                int y2 = num10 + iCellHeight;
                while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
                {
                  if (this.bIsOldINIPL)
                    this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num4);
                  else
                    this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                  if (y2 + iCellHeight > this.PaperSize.Width - this.PrintMargins.Left - size3.Height)
                  {
                    int y3 = this.PrintMargins.Top + size1.Height + size2.Height + 20;
                    int num8 = y3;
                    if (flag)
                    {
                      e.HasMorePages = true;
                      ++this.PageCounter;
                      return;
                    }
                    flag = true;
                    int x2 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
                    iCellHeight = 23;
                    if (this.bIsOldINIPL)
                      this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num4);
                    else
                      this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                    int num11 = 0;
                    for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                    {
                      StringFormat format3 = new StringFormat();
                      try
                      {
                        format3.Alignment = StringAlignment.Center;
                        format3.LineAlignment = StringAlignment.Center;
                        if (!this.bIsOldINIPL)
                        {
                          this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                          format2.Alignment = StringAlignment.Center;
                          format2.LineAlignment = StringAlignment.Center;
                          num4 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                        }
                      }
                      catch (Exception ex)
                      {
                      }
                      if (num11 + num4 <= num3)
                      {
                        e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(x2, y3, num4, iCellHeight));
                        e.Graphics.DrawRectangle(Pens.Black, x2, y3, num4, iCellHeight);
                        e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) x2, (float) y3, (float) num4, (float) iCellHeight), format3);
                        x2 += num4;
                        num11 += num4;
                      }
                      else
                      {
                        num4 = num7;
                        e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(x2, y3, num4, iCellHeight));
                        e.Graphics.DrawRectangle(Pens.Black, x2, y3, num4, iCellHeight);
                        e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) x2, (float) y3, (float) num4, (float) iCellHeight), format2);
                        num2 = x2 + num4;
                        break;
                      }
                    }
                    x1 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
                    y2 = num8 + iCellHeight;
                  }
                  int num12 = 0;
                  for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                  {
                    StringFormat format3 = new StringFormat();
                    try
                    {
                      format3.Alignment = StringAlignment.Center;
                      format3.LineAlignment = StringAlignment.Center;
                      if (!this.bIsOldINIPL)
                      {
                        string str2 = this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                        format2.Alignment = StringAlignment.Center;
                        format2.LineAlignment = StringAlignment.Center;
                        if (str2 == "L")
                        {
                          format3.Alignment = StringAlignment.Near;
                          format2.Alignment = StringAlignment.Near;
                        }
                        else if (str2 == "R")
                        {
                          format3.Alignment = StringAlignment.Far;
                          format2.Alignment = StringAlignment.Far;
                        }
                        num4 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                      }
                    }
                    catch (Exception ex)
                    {
                    }
                    if (num12 + num4 <= num3)
                    {
                      e.Graphics.DrawRectangle(Pens.Black, x1, y2, num4, iCellHeight);
                      e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) x1, (float) y2, (float) num4, (float) iCellHeight), format3);
                      x1 += num4;
                      num12 += num4;
                    }
                    else
                    {
                      num4 = num7;
                      e.Graphics.DrawRectangle(Pens.Black, x1, y2, num4, iCellHeight);
                      e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) x1, (float) y2, (float) num4, (float) iCellHeight), format2);
                      num2 = x1 + num4;
                      break;
                    }
                  }
                  x1 = !flag ? this.PrintMargins.Left : (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
                  ++this.iPartsListRowCount;
                  y2 += iCellHeight;
                }
                e.HasMorePages = false;
                this.PageCounter = 1;
              }
            }
          }
          else
          {
            if (!(this.objCurrentPrintJob.sPrintSideBySide == "0"))
            {
              if (!(this.objCurrentPrintJob.sOrientation == "0"))
                goto label_401;
            }
            if (this.PartListTable.Rows.Count > 0)
            {
              int num1 = 0;
              int left1 = this.PrintMargins.Left;
              int iCellHeight = 23;
              int y1 = this.PageCounter <= 1 ? (!(this.objCurrentPrintJob.sOrientation == "0") ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size1.Height - size2.Height - size3.Height - size4.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height) / 2 + this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height : this.PrintMargins.Top + size1.Height + size2.Height;
              int num2 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
              if (this.PageCounter > 1 && this.objCurrentPrintJob.sPrintHeaderFooter == "1")
              {
                e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size1.Width) / 2), (float) (y1 - size1.Height - size2.Height));
                e.Graphics.DrawString(str1, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num2 - size2.Width) / 2), (float) (y1 - size2.Height));
              }
              StringFormat stringFormat = new StringFormat()
              {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
              };
              int count = this.PartListTable.Columns.Count;
              int num3 = num2 / count;
              int num4 = 0;
              int iColWidth = 100;
              foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
              {
                if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                {
                  int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                  if (int32 < iColWidth)
                    iColWidth = int32;
                  num4 += int32;
                }
              }
              int num5 = 0;
              int num6 = 0;
              foreach (KeyValuePair<string, string> dicPlColSetting in this.dicPLColSettings)
              {
                if (dicPlColSetting.Key.ToString().EndsWith("_WIDTH"))
                {
                  int int32 = Convert.ToInt32(dicPlColSetting.Value.ToString());
                  if (num2 < num5 + int32)
                  {
                    num6 = Math.Abs(num2 - num5);
                    int num7 = num5 + int32;
                    break;
                  }
                  num5 += int32;
                }
              }
              if (this.bIsOldINIPL)
                this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
              else
                this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
              int num8 = 0;
              int num9;
              for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
              {
                StringFormat format3 = new StringFormat();
                try
                {
                  format3.Alignment = StringAlignment.Center;
                  format3.LineAlignment = StringAlignment.Center;
                  if (!this.bIsOldINIPL)
                  {
                    this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                    format2.Alignment = StringAlignment.Center;
                    format2.LineAlignment = StringAlignment.Center;
                    num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                  }
                }
                catch (Exception ex)
                {
                }
                if (num8 + num3 <= num2)
                {
                  e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num3, iCellHeight));
                  e.Graphics.DrawRectangle(Pens.Black, left1, y1, num3, iCellHeight);
                  e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num3, (float) iCellHeight), format3);
                  left1 += num3;
                  num8 += num3;
                }
                else
                {
                  num3 = num6;
                  e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num3, iCellHeight));
                  e.Graphics.DrawRectangle(Pens.Black, left1, y1, num3, iCellHeight);
                  e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num3, (float) iCellHeight), format2);
                  num9 = left1 + num3;
                  break;
                }
              }
              int left2 = this.PrintMargins.Left;
              int y2 = (this.PageCounter <= 1 ? (!(this.objCurrentPrintJob.sOrientation == "0") ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size1.Height - size2.Height - size3.Height - size4.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size1.Height - size2.Height - size3.Height - size4.Height) / 2 + this.PrintMargins.Top + size1.Height + size2.Height + 20 + size4.Height : this.PrintMargins.Top + size1.Height + size2.Height) + iCellHeight;
              while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
              {
                if (this.bIsOldINIPL)
                  this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, num3);
                else
                  this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.objStringFormat, iColWidth);
                if (this.objCurrentPrintJob.sOrientation == "0")
                {
                  if (y2 + iCellHeight > this.PaperSize.Height - this.PrintMargins.Bottom - size3.Height)
                  {
                    num9 = this.PrintMargins.Left;
                    num1 = this.PrintMargins.Top + size1.Height + size2.Height;
                    e.HasMorePages = true;
                    ++this.PageCounter;
                    if (!(this.objCurrentPrintJob.sPrintHeaderFooter == "1"))
                      return;
                    e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size3.Height));
                    return;
                  }
                }
                else if (y2 + iCellHeight > this.PaperSize.Width - this.PrintMargins.Left - size3.Height)
                {
                  num9 = this.PrintMargins.Left;
                  num1 = this.PrintMargins.Top;
                  e.HasMorePages = true;
                  ++this.PageCounter;
                  if (!(this.objCurrentPrintJob.sPrintHeaderFooter == "1"))
                    return;
                  e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
                  return;
                }
                int num7 = 0;
                for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
                {
                  StringFormat format3 = new StringFormat();
                  try
                  {
                    format3.Alignment = StringAlignment.Center;
                    format3.LineAlignment = StringAlignment.Center;
                    if (!this.bIsOldINIPL)
                    {
                      string str2 = this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_ALIGN"].ToString();
                      format2.Alignment = StringAlignment.Center;
                      format2.LineAlignment = StringAlignment.Center;
                      if (str2 == "L")
                      {
                        format3.Alignment = StringAlignment.Near;
                        format2.Alignment = StringAlignment.Near;
                      }
                      else if (str2 == "R")
                      {
                        format3.Alignment = StringAlignment.Far;
                        format2.Alignment = StringAlignment.Far;
                      }
                      num3 = Convert.ToInt32(this.dicPLColSettings[this.PartListTable.Columns[index].ToString() + "_WIDTH"].ToString());
                    }
                  }
                  catch (Exception ex)
                  {
                  }
                  if (num7 + num3 <= num2)
                  {
                    e.Graphics.DrawRectangle(Pens.Black, left2, y2, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y2, (float) num3, (float) iCellHeight), format3);
                    left2 += num3;
                    num7 += num3;
                  }
                  else
                  {
                    num3 = num6;
                    e.Graphics.DrawRectangle(Pens.Black, left2, y2, num3, iCellHeight);
                    e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y2, (float) num3, (float) iCellHeight), format2);
                    num9 = left2 + num3;
                    break;
                  }
                }
                left2 = this.PrintMargins.Left;
                ++this.iPartsListRowCount;
                y2 += iCellHeight;
              }
              if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "0")
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size3.Height));
              else if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "1")
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num2 - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
              e.HasMorePages = false;
              this.ImagePrinted = 0;
              this.PageCounter = 1;
            }
          }
        }
        else if (this.objCurrentPrintJob.sOrientation == "0")
        {
          int num = this.PaperSize.Width - this.PrintMargins.Right - this.PrintMargins.Left;
          e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num - size3.Width) / 2), (float) (this.PaperSize.Height - this.PrintMargins.Bottom - size3.Height));
        }
        else
        {
          int num = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
          e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (this.PrintMargins.Left + (num - size3.Width) / 2), (float) (this.PaperSize.Width - this.PrintMargins.Bottom - size3.Height));
        }
      }
      catch
      {
      }
label_401:
      if (this.arrPrintJobs.Count <= 0)
        return;
      e.HasMorePages = true;
      this.bNewPageForListLoaded = false;
      this.bPreviewImageNotExported = true;
      this.ImagePrinted = 0;
      this.PartListTable = (DataTable) null;
    }

    private void doc_PrintList(object sender, PrintPageEventArgs e)
    {
      try
      {
        if (this.objCurrentPrintJob.sPrintList == "1" && this.objCurrentPrintJob.sPrintPic == "0" && !this.bHasPages)
          this.objCurrentPrintJob = this.lstPrintJob[this.iPrintJobCounter];
        this.bHasPages = false;
        if (!System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath) && this.objCurrentPrintJob.sLocalListPath != string.Empty)
        {
          new Download().DownloadFile(this.objCurrentPrintJob.sServerListPath, this.objCurrentPrintJob.sLocalListPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
          int num = 2;
          int millisecondsTimeout = 500;
          for (int index = 0; index < num; ++index)
          {
            try
            {
              if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath))
              {
                if (PreviewManager.UnZipFile(this.objCurrentPrintJob.sLocalListPath).ToInt32() == 1)
                {
                  if (System.IO.File.Exists(this.objCurrentPrintJob.sLocalListPath.ToLower().Replace(".zip", ".xml")))
                    break;
                }
                Thread.Sleep(millisecondsTimeout);
              }
              else
                break;
            }
            catch
            {
            }
          }
        }
        this.LoadPartsListData(this.lstPrintJob[this.iPrintJobCounter].sLocalListPath);
        this.iTotalPageWidth = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
        StringFormat format1 = new StringFormat()
        {
          FormatFlags = StringFormatFlags.NoWrap,
          Trimming = StringTrimming.Character
        };
        int num1 = 0;
        int iColWidth = 100;
        foreach (KeyValuePair<string, string> dicPlColAlignment in this.dicPLColAlignments)
        {
          int num2 = int.Parse(dicPlColAlignment.Value.Split('|')[1].ToString());
          if (num2 < iColWidth)
            iColWidth = num2;
          num1 += num2;
        }
        StringFormat format2 = new StringFormat();
        format2.Alignment = StringAlignment.Center;
        if (this.PartListTable.Rows.Count > 0)
        {
          e.PageSettings.Landscape = this.objCurrentPrintJob.pageSplitCount > 1 && this.objCurrentPrintJob.sOrientation == "1";
          int iCellHeight = 23;
          Size size1 = new Size(0, 0);
          int num2 = 0;
          int left1 = this.PrintMargins.Left;
          int top1 = this.PrintMargins.Top;
          int left2 = this.PrintMargins.Left;
          int y1 = this.PrintMargins.Top;
          int count = this.PartListTable.Columns.Count;
          int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
          int num4 = num3 / count;
          if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
          {
            string str = this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL) + " : " + this.objCurrentPrintJob.UpdateDatePL.ToString(this.objCurrentPrintJob.dateFormat) + "   " + this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
            Size size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
            Size size3 = TextRenderer.MeasureText(str, this.previewFont);
            y1 = y1 + size2.Height + size3.Height;
            int left3 = this.PrintMargins.Left;
            int top2 = this.PrintMargins.Top;
            int num5 = size2.Height + size3.Height;
            int num6 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
            RectangleF layoutRectangle = new RectangleF((float) left3, (float) top2, (float) num6, (float) num5);
            e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, (Brush) new SolidBrush(Color.Black), layoutRectangle, format2);
            e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left2 + (num3 - size3.Width) / 2), (float) (y1 - size3.Height));
          }
          if (this.objCurrentPrintJob.copyRightField != string.Empty)
            size1 = TextRenderer.MeasureText(this.copyRightField, this.previewFont);
          int num7 = 0;
          int num8 = 0;
          foreach (KeyValuePair<string, string> dicPlColAlignment in this.dicPLColAlignments)
          {
            int num5 = int.Parse(dicPlColAlignment.Value.Split('|')[1].ToString());
            if (num3 < num7 + num5)
            {
              num8 = Math.Abs(num3 - num7);
              int num6 = num7 + num5;
              break;
            }
            num7 += num5;
          }
          if (this.bIsOldINIPL)
            this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.strFormat, num4);
          else
            this.ResizeCellHeight(true, 0, ref iCellHeight, this.PartListTable, e, this.strFormat, iColWidth);
          int num9 = 0;
          int num10;
          for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
          {
            StringFormat format3 = new StringFormat();
            try
            {
              format3.Alignment = StringAlignment.Center;
              format3.LineAlignment = StringAlignment.Center;
              if (!this.bIsOldINIPL)
              {
                string str = this.dicPLColAlignments[this.PartListTable.Columns[index].ColumnName.ToString()].ToString();
                str.Split('|')[0].ToString();
                int num5 = int.Parse(str.Split('|')[1].ToString());
                format1.Alignment = StringAlignment.Center;
                format1.LineAlignment = StringAlignment.Center;
                num4 = num5;
              }
            }
            catch (Exception ex)
            {
            }
            if (num9 + num4 <= num3)
            {
              e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left2, y1, num4, iCellHeight));
              e.Graphics.DrawRectangle(Pens.Black, left2, y1, num4, iCellHeight);
              e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y1, (float) num4, (float) iCellHeight), format3);
              left2 += num4;
              num9 += num4;
            }
            else
            {
              num4 = num8;
              e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left2, y1, num4, iCellHeight));
              e.Graphics.DrawRectangle(Pens.Black, left2, y1, num4, iCellHeight);
              e.Graphics.DrawString(this.PartListTable.Columns[index].Caption, this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y1, (float) num4, (float) iCellHeight), format1);
              num10 = left2 + num4;
              break;
            }
          }
          int left4 = this.PrintMargins.Left;
          int y2 = y1 + iCellHeight;
          while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
          {
            if (this.bIsOldINIPL)
              this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.strFormat, num4);
            else
              this.ResizeCellHeight(false, this.iPartsListRowCount, ref iCellHeight, this.PartListTable, e, this.strFormat, iColWidth);
            if (this.objCurrentPrintJob.sOrientation == "0")
            {
              if (y2 + iCellHeight > this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height)
              {
                int left3 = this.PrintMargins.Left;
                num2 = this.PrintMargins.Top;
                e.HasMorePages = true;
                this.bHasPages = true;
                e.PageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
                this.objCurrentPrintJob.sZoom = "0";
                if (!(this.objCurrentPrintJob.copyRightField != string.Empty))
                  return;
                e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left3 + (num3 - size1.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size1.Height));
                return;
              }
            }
            else if (y2 + iCellHeight > this.PaperSize.Width - this.PrintMargins.Left - size1.Height)
            {
              int left3 = this.PrintMargins.Left;
              num2 = this.PrintMargins.Top;
              e.HasMorePages = true;
              this.bHasPages = true;
              e.PageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
              this.objCurrentPrintJob.sZoom = "0";
              if (!(this.objCurrentPrintJob.copyRightField != string.Empty))
                return;
              e.Graphics.DrawString(this.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left3 + (num3 - size1.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size1.Height));
              return;
            }
            int num5 = 0;
            for (int index = 0; index < this.PartListTable.Columns.Count; ++index)
            {
              StringFormat format3 = new StringFormat();
              try
              {
                format3.Alignment = StringAlignment.Center;
                format3.LineAlignment = StringAlignment.Center;
                if (!this.bIsOldINIPL)
                {
                  string str1 = this.dicPLColAlignments[this.PartListTable.Columns[index].ColumnName.ToString()].ToString();
                  string str2 = str1.Split('|')[0].ToString();
                  int num6 = int.Parse(str1.Split('|')[1].ToString());
                  format1.Alignment = StringAlignment.Center;
                  format1.LineAlignment = StringAlignment.Center;
                  if (str2 == "L")
                  {
                    format3.Alignment = StringAlignment.Near;
                    format1.Alignment = StringAlignment.Near;
                  }
                  else if (str2 == "R")
                  {
                    format3.Alignment = StringAlignment.Far;
                    format1.Alignment = StringAlignment.Near;
                  }
                  num4 = num6;
                }
              }
              catch (Exception ex)
              {
              }
              if (num5 + num4 <= num3)
              {
                e.Graphics.DrawRectangle(Pens.Black, left4, y2, num4, iCellHeight);
                e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left4, (float) y2, (float) num4, (float) iCellHeight), format3);
                left4 += num4;
                num5 += num4;
              }
              else
              {
                num4 = num8;
                e.Graphics.DrawRectangle(Pens.Black, left4, y2, num4, iCellHeight);
                e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][index].ToString(), this.previewFont, Brushes.Black, new RectangleF((float) left4, (float) y2, (float) num4, (float) iCellHeight), format1);
                num10 = left4 + num4;
                break;
              }
            }
            left4 = this.PrintMargins.Left;
            ++this.iPartsListRowCount;
            y2 += iCellHeight;
          }
          if (this.objCurrentPrintJob.copyRightField != string.Empty)
            e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left4 + (num3 - size1.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size1.Height));
          e.HasMorePages = false;
        }
        this.iPartsListRowCount = 0;
        if (e.HasMorePages)
          return;
        ++this.iPrintJobCounter;
        ++this.iListCounter;
      }
      catch
      {
      }
    }

    private void doc_PrintSelList(object sender, PrintPageEventArgs e)
    {
      try
      {
        this.iTotalPageWidth = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
        StringFormat format1 = new StringFormat()
        {
          FormatFlags = StringFormatFlags.NoWrap,
          Trimming = StringTrimming.Character
        };
        if (this.dgSelList.Rows.Count <= 0)
          return;
        int iCellHeight = 23;
        Size size1 = new Size(0, 0);
        int num1 = 0;
        int left1 = this.PrintMargins.Left;
        int top = this.PrintMargins.Top;
        int num2 = 0;
        for (int index = 0; index < this.dgSelList.Columns.Count; ++index)
        {
          if (this.dgSelList.Columns[index].Visible)
            ++num2;
        }
        int num3 = e.PageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
        int num4 = num3 / num2;
        if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
        {
          string str = this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL) + " : " + DateTime.Now.ToString(this.objCurrentPrintJob.dateFormat) + Environment.NewLine;
          Size size2 = TextRenderer.MeasureText(str, this.previewFont);
          top += size2.Height;
          e.Graphics.DrawString(str, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left1 + (num3 - size2.Width) / 2), (float) (top - size2.Height));
        }
        int y1 = top + 5;
        if (this.objCurrentPrintJob.copyRightField != string.Empty)
          size1 = TextRenderer.MeasureText(this.copyRightField, this.previewFont);
        int num5 = 0;
        int iColWidth = 100;
        foreach (KeyValuePair<string, string> dicSlColSetting in this.dicSLColSettings)
        {
          if (dicSlColSetting.Key.ToString().EndsWith("_WIDTH"))
          {
            int int32 = Convert.ToInt32(dicSlColSetting.Value.ToString());
            if (int32 < iColWidth)
              iColWidth = int32;
            num5 += int32;
          }
        }
        int num6 = 0;
        int num7 = 0;
        foreach (KeyValuePair<string, string> dicSlColSetting in this.dicSLColSettings)
        {
          if (dicSlColSetting.Key.ToString().EndsWith("_WIDTH"))
          {
            int int32 = Convert.ToInt32(dicSlColSetting.Value.ToString());
            if (num3 < num6 + int32)
            {
              num7 = Math.Abs(num3 - num6);
              int num8 = num6 + int32;
              break;
            }
            num6 += int32;
          }
        }
        if (this.bIsOldINISL)
          this.ResizeCellHeight(true, 0, ref iCellHeight, this.dgSelList, e, this.objStringFormat, num4);
        else
          this.ResizeCellHeight(true, 0, ref iCellHeight, this.dgSelList, e, this.objStringFormat, iColWidth);
        int num9 = 0;
        int num10;
        for (int index = 0; index < this.dgSelList.Columns.Count; ++index)
        {
          StringFormat format2 = new StringFormat();
          if (this.dgSelList.Columns[index].Visible)
          {
            try
            {
              this.dicSLColSettings[this.dgSelList.Columns[index].Name.ToString() + "_ALIGN"].ToString();
              format2.Alignment = StringAlignment.Center;
              format2.LineAlignment = StringAlignment.Center;
              if (!this.bIsOldINISL)
              {
                format1.Alignment = StringAlignment.Center;
                format1.LineAlignment = StringAlignment.Center;
                num4 = Convert.ToInt32(this.dicSLColSettings[this.dgSelList.Columns[index].Name.ToString() + "_WIDTH"].ToString());
              }
            }
            catch (Exception ex)
            {
            }
            if (num9 + num4 <= num3)
            {
              e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num4, iCellHeight));
              e.Graphics.DrawRectangle(Pens.Black, left1, y1, num4, iCellHeight);
              e.Graphics.DrawString(this.dgSelList.Columns[index].HeaderText, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num4, (float) iCellHeight), format2);
              left1 += num4;
              num9 += num4;
            }
            else
            {
              if (num7 == 0)
                num7 = 4;
              num4 = num7;
              e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, y1, num4, iCellHeight));
              e.Graphics.DrawRectangle(Pens.Black, left1, y1, num4, iCellHeight);
              e.Graphics.DrawString(this.dgSelList.Columns[index].HeaderText, this.previewFont, Brushes.Black, new RectangleF((float) left1, (float) y1, (float) num4, (float) iCellHeight), format1);
              num10 = left1 + num4;
              break;
            }
          }
        }
        int left2 = this.PrintMargins.Left;
        int y2 = y1 + iCellHeight;
        while (this.iSelListRowCount < this.dgSelList.Rows.Count)
        {
          if (this.bIsOldINISL)
            this.ResizeCellHeight(false, this.iSelListRowCount, ref iCellHeight, this.dgSelList, e, this.objStringFormat, num4);
          else
            this.ResizeCellHeight(false, this.iSelListRowCount, ref iCellHeight, this.dgSelList, e, this.objStringFormat, iColWidth);
          if (this.objCurrentPrintJob.sOrientation == "0")
          {
            if (y2 + iCellHeight > this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height)
            {
              int left3 = this.PrintMargins.Left;
              num1 = this.PrintMargins.Top;
              e.HasMorePages = true;
              e.PageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
              if (!(this.objCurrentPrintJob.copyRightField != string.Empty))
                return;
              e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left3 + (num3 - size1.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size1.Height));
              return;
            }
          }
          else if (y2 + iCellHeight > this.PaperSize.Width - this.PrintMargins.Left - size1.Height)
          {
            int left3 = this.PrintMargins.Left;
            num1 = this.PrintMargins.Top;
            e.HasMorePages = true;
            e.PageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
            if (!(this.objCurrentPrintJob.copyRightField != string.Empty))
              return;
            e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left3 + (num3 - size1.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size1.Height));
            return;
          }
          int num8 = 0;
          for (int index = 0; index < this.dgSelList.Columns.Count; ++index)
          {
            StringFormat format2 = new StringFormat();
            if (this.dgSelList.Columns[index].Visible)
            {
              try
              {
                string str = this.dicSLColSettings[this.dgSelList.Columns[index].Name.ToString() + "_ALIGN"].ToString();
                format2.Alignment = StringAlignment.Center;
                format2.LineAlignment = StringAlignment.Center;
                if (!this.bIsOldINISL)
                {
                  format1.Alignment = StringAlignment.Center;
                  format1.LineAlignment = StringAlignment.Center;
                  if (str == "L")
                  {
                    format2.Alignment = StringAlignment.Near;
                    format1.Alignment = StringAlignment.Near;
                  }
                  else if (str == "R")
                  {
                    format2.Alignment = StringAlignment.Far;
                    format1.Alignment = StringAlignment.Far;
                  }
                  num4 = Convert.ToInt32(this.dicSLColSettings[this.dgSelList.Columns[index].Name.ToString() + "_WIDTH"].ToString());
                }
              }
              catch (Exception ex)
              {
              }
              if (num8 + num4 <= num3)
              {
                e.Graphics.DrawRectangle(Pens.Black, left2, y2, num4, iCellHeight);
                string empty = string.Empty;
                if (this.dgSelList.Rows[this.iSelListRowCount].Cells[index].Value != null)
                  empty = this.dgSelList.Rows[this.iSelListRowCount].Cells[index].Value.ToString();
                e.Graphics.DrawString(empty, this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y2, (float) num4, (float) iCellHeight), format2);
                left2 += num4;
                num8 += num4;
              }
              else
              {
                num4 = num7;
                e.Graphics.DrawRectangle(Pens.Black, left2, y2, num4, iCellHeight);
                string empty = string.Empty;
                if (this.dgSelList.Rows[this.iSelListRowCount].Cells[index].Value != null)
                  empty = this.dgSelList.Rows[this.iSelListRowCount].Cells[index].Value.ToString();
                e.Graphics.DrawString(empty, this.previewFont, Brushes.Black, new RectangleF((float) left2, (float) y2, (float) num4, (float) iCellHeight), format1);
                num10 = left2 + num4;
                break;
              }
            }
          }
          left2 = this.PrintMargins.Left;
          ++this.iSelListRowCount;
          y2 += iCellHeight;
        }
        if (this.objCurrentPrintJob.copyRightField != string.Empty)
          e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, (Brush) new SolidBrush(Color.Black), (float) (left2 + (num3 - size1.Width) / 2), (float) (e.PageBounds.Bottom - this.PrintMargins.Right - size1.Height));
        e.HasMorePages = false;
      }
      catch
      {
      }
    }

    public void StartPrinting()
    {
      try
      {
        this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
        if (this.arrPrintJobs.Count == 1 && this.sPrintRng == "0" && (this.objCurrentPrintJob.sPrintPic == "1" && this.objCurrentPrintJob.sPrintList == "0") && (this.objCurrentPrintJob.sPrintSelList == "0" && !System.IO.File.Exists(this.objCurrentPrintJob.sLocalPicPath)))
          return;
        CustomPrintPreviewDialog printPreviewDialog = new CustomPrintPreviewDialog(this);
        this.InitializePrintVariablesOld(this.objCurrentPrintJob);
        this.printDocSList.DefaultPageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
        this.ReadSelectionList();
        this.ExportImageForSplitPrinting();
        this.ImagePrinted = 0;
        bool flag = false;
        if (this.bOfflineMode && this.arrPrintJobs.Count == 1 && (this.objCurrentPrintJob.sLocalPicPath == string.Empty && this.objCurrentPrintJob.sLocalListPath == string.Empty) && (this.dgSelList != null && this.dgSelList.Rows.Count > 0))
        {
          flag = true;
          printPreviewDialog.Document = this.printDocSList;
        }
        else if (this.objCurrentPrintJob.sZoom == "0")
        {
          this.changePaperOrientation();
          if ((this.objCurrentPrintJob.sPrintPic == "1" || this.objCurrentPrintJob.sPrintList == "1") && (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null) && this.dgSelList.Rows.Count > 0)
          {
            flag = true;
            this.iSelListRowCount = 0;
            PreviewManager.MultiPrintDocument multiPrintDocument = new PreviewManager.MultiPrintDocument(this.printDocFitPage, this.printDocSList);
            printPreviewDialog.Document = (PrintDocument) multiPrintDocument;
          }
          else if ((this.objCurrentPrintJob.sPrintPic == "1" || this.objCurrentPrintJob.sPrintList == "1") && (this.dgSelList == null || this.dgSelList.Rows.Count == 0))
          {
            flag = true;
            printPreviewDialog.Document = this.printDocFitPage;
          }
          else if (this.objCurrentPrintJob.sPrintPic == "0" && this.objCurrentPrintJob.sPrintList == "0" && (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null) && this.dgSelList.Rows.Count != 0)
          {
            flag = true;
            printPreviewDialog.Document = this.printDocSList;
          }
        }
        else
        {
          this.printDocHalfPage.DefaultPageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
          if (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
          {
            this.iSelListRowCount = 0;
            PreviewManager.MultiPrintDocument multiPrintDocument = new PreviewManager.MultiPrintDocument(this.printDocHalfPage, this.printDocSList);
            printPreviewDialog.Document = (PrintDocument) multiPrintDocument;
          }
          else
            printPreviewDialog.Document = this.printDocHalfPage;
          flag = true;
        }
        if (!flag)
          return;
        int num = (int) printPreviewDialog.ShowDialog((IWin32Window) this.frmParent);
      }
      catch
      {
      }
      finally
      {
        this.objDjVuCtl.SRC = string.Empty;
      }
    }

    private void changePaperOrientation()
    {
      try
      {
        if (this.objCurrentPrintJob.sSplittingOption == "0")
          this.printDocFitPage.DefaultPageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
        else if (this.objCurrentPrintJob.sSplittingOption == "2")
        {
          if (this.imagetoPrint.Width == this.imagetoPrint.Height)
          {
            if (this.sOrientation == "0")
              this.printDocFitPage.DefaultPageSettings.Landscape = false;
            else
              this.printDocFitPage.DefaultPageSettings.Landscape = true;
          }
          else
            this.printDocFitPage.DefaultPageSettings.Landscape = this.imagetoPrint.Width > this.imagetoPrint.Height;
        }
        else if (this.imagetoPrint.Width == this.imagetoPrint.Height)
        {
          if (this.sOrientation == "0")
            this.printDocFitPage.DefaultPageSettings.Landscape = true;
          else
            this.printDocFitPage.DefaultPageSettings.Landscape = false;
        }
        else
          this.printDocFitPage.DefaultPageSettings.Landscape = this.imagetoPrint.Width < this.imagetoPrint.Height;
      }
      catch
      {
      }
    }

    private void ExportImageForSplitPrinting()
    {
      try
      {
        if (this.objCurrentPrintJob.pageSplitCount == 1)
          return;
        this.sExportedImagePath = string.Empty;
        if (this.arrPrintJobs.Count <= 0 || !this.bPreviewImageNotExported)
          return;
        this.bPreviewImageNotExported = false;
        this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
        this.arrPrintJobs.RemoveAt(0);
        this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
        string empty = string.Empty;
        if (this.ExportImage(!System.IO.File.Exists(this.objCurrentPrintJob.sLocalPicPath) ? Application.StartupPath + "\\blank.djvu" : this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) != 0)
          return;
        this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
      }
      catch
      {
      }
    }

    public void PrintJobRecieved(string[] args)
    {
      try
      {
        if (args.Length != 3)
          return;
        this.SetArguments(args);
        if (this.sPrintRng == "1")
        {
          this.GetMultiRangeVales();
          this.SetMultiRangeAruguments();
        }
        this.ReadSelectionList();
        if (!this.LoadBookXml(this.sBookPath))
          return;
        if (!string.IsNullOrEmpty(this.strMultiRngStart) && !string.IsNullOrEmpty(this.strMultiRngEnd))
        {
          for (int index = 0; index < this.lstMultiRange.Count; ++index)
          {
            this.objXmlNodeList = this.lstMultiRange[index];
            int count = this.objXmlNodeList.Count;
            foreach (XmlNode objXmlNode in this.objXmlNodeList)
            {
              --count;
              string sDescription1 = string.Empty;
              int intPicIndex = 1;
              string strPicMemoValue1 = string.Empty;
              foreach (XmlNode childNode in objXmlNode.ChildNodes)
              {
                if (this.bPrintPicMemo)
                {
                  try
                  {
                    strPicMemoValue1 = this.GetPicLocalMemos(objXmlNode.Attributes[0].Value.ToString(), intPicIndex);
                    ++intPicIndex;
                  }
                  catch (Exception ex)
                  {
                  }
                }
                string empty1 = string.Empty;
                if (args.Length == 4)
                {
                  string str1 = sDescription1 + this.sBookCode + " >> ";
                }
                else
                {
                  string str2 = this.sBookCode + " >> ";
                }
                string s1 = string.Empty;
                string empty2 = string.Empty;
                string s2 = string.Empty;
                string empty3 = string.Empty;
                string empty4 = string.Empty;
                this.sLocalListPath = string.Empty;
                this.sServerListPath = string.Empty;
                this.sLocalPicPath = string.Empty;
                this.sServerPicPath = string.Empty;
                if (childNode.Name.ToUpper() == "PIC")
                {
                  if (childNode.Attributes[this.attPicElement] != null)
                    empty3 = childNode.Attributes[this.attPicElement].Value;
                  if (childNode.Attributes[this.attListElement] != null)
                    empty4 = childNode.Attributes[this.attListElement].Value;
                  try
                  {
                    XmlElement parentNode = (XmlElement) childNode.ParentNode;
                    string str3 = string.Empty;
                    while (parentNode.Name.ToUpper() != "BOOK")
                    {
                      if (parentNode.Attributes[this.attPgNameElement] != null)
                        str3 = !(str3 == string.Empty) ? parentNode.Attributes[this.attPgNameElement].Value + " >> " + str3 : parentNode.Attributes[this.attPgNameElement].Value;
                      if (parentNode.ParentNode != null)
                        parentNode = (XmlElement) parentNode.ParentNode;
                    }
                    sDescription1 = this.sBookCode + " >> " + str3 + Environment.NewLine;
                  }
                  catch
                  {
                    sDescription1 = this.sBookCode + " >> " + Environment.NewLine;
                  }
                  if (childNode.Attributes[this.attUpdateDatePICElement] != null)
                    empty2 = childNode.Attributes[this.attUpdateDatePICElement].Value;
                  if (childNode.Attributes[this.attUpdateDatePLElement] != null)
                    s2 = childNode.Attributes[this.attUpdateDatePLElement].Value;
                  if (childNode.Attributes[this.attUpdateDateElement] != null)
                  {
                    s1 = childNode.Attributes[this.attUpdateDateElement].Value;
                    try
                    {
                      if (s2 == string.Empty)
                        s2 = s1;
                    }
                    catch
                    {
                    }
                  }
                  if (empty2 != null && empty2 != string.Empty)
                    s1 = empty2;
                  if (empty3 != string.Empty)
                  {
                    this.sLocalPicPath = string.Empty;
                    this.sServerPicPath = string.Empty;
                    this.sLocalPicPath = this.sBookPath + "\\" + empty3;
                    this.sServerPicPath = this.sContentPath + "/" + this.sBookCode + "/" + empty3;
                  }
                  if (empty4 != string.Empty)
                  {
                    this.sLocalListPath = string.Empty;
                    this.sServerListPath = string.Empty;
                    if (this.sCompression == "1")
                    {
                      this.sLocalListPath = this.sBookPath + "\\" + empty4 + ".zip";
                      this.sServerListPath = this.sContentPath + "/" + this.sBookCode + "/" + empty4 + ".zip";
                    }
                    else
                    {
                      this.sLocalListPath = this.sBookPath + "\\" + empty4 + ".xml";
                      this.sServerListPath = this.sContentPath + "/" + this.sBookCode + "/" + empty4 + ".xml";
                    }
                  }
                  DateTime dateTime1 = new DateTime();
                  DateTime sUpdateDate1;
                  try
                  {
                    sUpdateDate1 = !(s1 == string.Empty) ? DateTime.Parse(s1, (IFormatProvider) new CultureInfo("en-GB", false)) : DateTime.Now;
                  }
                  catch
                  {
                    sUpdateDate1 = Convert.ToDateTime(DateTime.Now.ToString(this.dateFormat), (IFormatProvider) new DateTimeFormatInfo()
                    {
                      ShortDatePattern = this.dateFormat
                    });
                  }
                  DateTime dateTime2 = new DateTime();
                  DateTime dateTime3 = new DateTime();
                  DateTime sUpdateDatePIC1;
                  try
                  {
                    sUpdateDatePIC1 = !(empty2 == string.Empty) ? DateTime.Parse(empty2, (IFormatProvider) new CultureInfo("en-GB", false)) : DateTime.Now;
                  }
                  catch
                  {
                    sUpdateDatePIC1 = Convert.ToDateTime(DateTime.Now.ToString(this.dateFormat), (IFormatProvider) new DateTimeFormatInfo()
                    {
                      ShortDatePattern = this.dateFormat
                    });
                  }
                  DateTime sUpdateDatePL1;
                  try
                  {
                    sUpdateDatePL1 = !(s2 == string.Empty) ? DateTime.Parse(s2, (IFormatProvider) new CultureInfo("en-GB", false)) : DateTime.Now;
                  }
                  catch
                  {
                    sUpdateDatePL1 = Convert.ToDateTime(DateTime.Now.ToString(this.dateFormat), (IFormatProvider) new DateTimeFormatInfo()
                    {
                      ShortDatePattern = this.dateFormat
                    });
                  }
                  PreviewManager.PrintJob printJob = new PreviewManager.PrintJob(this, this.sPrinterName, this.sOrientation, this.sPaperSize, this.sUserId, this.sPassword, this.sSplittingOption, this.sPrintHeaderFooter, sDescription1, sUpdateDate1, sUpdateDatePIC1, sUpdateDatePL1, this.sPrintPgNos, this.sZoom, this.sCurrentImageZoom, this.sMaintainZoom, this.sZoomFactor, this.pageSplitCount, this.sPrintPic, this.sPrintList, this.sPrintSelList, this.sPrintSideBySide, this.sPrintRng, this.sBookType, this.sLocalListPath, this.sServerListPath, this.sLocalPicPath, this.sServerPicPath, this.spaperUtilization, this.sProxyType, this.sProxyIP, this.sProxyPort, this.sProxyLogin, this.sProxyPassword, this.sTimeOut, this.sCompression, this.sEncryption, this.listColSequence, this.dateFormat, this.copyRightField, this.sLanguage, strPicMemoValue1);
                  int[] numArray;
                  if (printJob.sMaintainZoom == "1" && printJob.sPrintRng == "0")
                  {
                    string[] strArray = printJob.sZoomFactor.Split(',');
                    numArray = new int[strArray.Length];
                    numArray[0] = int.Parse(strArray[0]);
                    numArray[1] = int.Parse(strArray[1]);
                    numArray[2] = int.Parse(strArray[2]);
                    numArray[3] = int.Parse(strArray[3]);
                    numArray[4] = int.Parse(strArray[4]);
                    numArray[5] = int.Parse(strArray[5]);
                    numArray[6] = int.Parse(strArray[6]);
                    numArray[7] = int.Parse(strArray[7]);
                  }
                  else
                  {
                    printJob.sZoomFactor.Split('-');
                    numArray = new int[8];
                  }
                  printJob.CurrentZoomFactors = numArray;
                  if (this.strDuplicatePrinting.ToUpper() == "OFF")
                  {
                    if (this.sPreviousPicName != printJob.sLocalPicPath)
                    {
                      this.arrPrintJobs.Add((object) printJob);
                      this.sPreviousPicName = printJob.sLocalPicPath;
                    }
                    else
                      this.sPreviousPicName = printJob.sLocalPicPath;
                  }
                  else
                  {
                    int int32 = Convert.ToInt32(objXmlNode.Attributes[0].Value.ToString());
                    if (this.sPreviousPicName != printJob.sLocalPicPath || int32 != this.intPreviousPageId)
                    {
                      if (this.bOfflineMode)
                      {
                        if (!System.IO.File.Exists(printJob.sLocalPicPath))
                          printJob.sLocalPicPath = string.Empty;
                        if (!System.IO.File.Exists(printJob.sLocalListPath))
                          printJob.sLocalListPath = string.Empty;
                        if (printJob.sLocalListPath != string.Empty || printJob.sLocalPicPath != string.Empty)
                        {
                          this.arrPrintJobs.Add((object) printJob);
                          this.sPreviousPicName = printJob.sLocalPicPath;
                          this.intPreviousPageId = int32;
                        }
                        else if (count == 0 && this.arrPrintJobs.Count == 0 && (printJob.sPrintSelList == "1" && this.dgSelList != null) && this.dgSelList.RowCount != 0)
                        {
                          this.arrPrintJobs.Add((object) printJob);
                          this.sPreviousPicName = printJob.sLocalPicPath;
                          this.intPreviousPageId = int32;
                        }
                      }
                      else
                      {
                        this.arrPrintJobs.Add((object) printJob);
                        this.sPreviousPicName = printJob.sLocalPicPath;
                        this.intPreviousPageId = int32;
                      }
                    }
                    else
                    {
                      this.sPreviousPicName = printJob.sLocalPicPath;
                      this.intPreviousPageId = int32;
                    }
                  }
                }
              }
            }
          }
        }
        else
        {
          int count = this.objXmlNodeList.Count;
          foreach (XmlNode objXmlNode in this.objXmlNodeList)
          {
            --count;
            string sDescription1 = string.Empty;
            int intPicIndex = 1;
            string strPicMemoValue1 = string.Empty;
            foreach (XmlNode childNode in objXmlNode.ChildNodes)
            {
              if (this.bPrintPicMemo)
              {
                try
                {
                  strPicMemoValue1 = this.GetPicLocalMemos(objXmlNode.Attributes[0].Value.ToString(), intPicIndex);
                  ++intPicIndex;
                }
                catch (Exception ex)
                {
                }
              }
              string empty1 = string.Empty;
              if (args.Length == 4)
              {
                string str1 = sDescription1 + this.sBookCode + " >> ";
              }
              else
              {
                string str2 = this.sBookCode + " >> ";
              }
              string s1 = string.Empty;
              string empty2 = string.Empty;
              string s2 = string.Empty;
              string empty3 = string.Empty;
              string empty4 = string.Empty;
              this.sLocalListPath = string.Empty;
              this.sServerListPath = string.Empty;
              this.sLocalPicPath = string.Empty;
              this.sServerPicPath = string.Empty;
              if (childNode.Name.ToUpper() == "PIC")
              {
                if (childNode.Attributes[this.attPicElement] != null)
                  empty3 = childNode.Attributes[this.attPicElement].Value;
                if (childNode.Attributes[this.attListElement] != null)
                  empty4 = childNode.Attributes[this.attListElement].Value;
                try
                {
                  XmlElement parentNode = (XmlElement) childNode.ParentNode;
                  string str3 = string.Empty;
                  while (parentNode.Name.ToUpper() != "BOOK")
                  {
                    if (parentNode.Attributes[this.attPgNameElement] != null)
                      str3 = !(str3 == string.Empty) ? parentNode.Attributes[this.attPgNameElement].Value + " >> " + str3 : parentNode.Attributes[this.attPgNameElement].Value;
                    if (parentNode.ParentNode != null)
                      parentNode = (XmlElement) parentNode.ParentNode;
                  }
                  sDescription1 = this.sBookCode + " >> " + str3 + Environment.NewLine;
                }
                catch
                {
                  sDescription1 = this.sBookCode + " >> " + Environment.NewLine;
                }
                if (childNode.Attributes[this.attUpdateDatePICElement] != null)
                  empty2 = childNode.Attributes[this.attUpdateDatePICElement].Value;
                if (childNode.Attributes[this.attUpdateDatePLElement] != null)
                  s2 = childNode.Attributes[this.attUpdateDatePLElement].Value;
                if (childNode.Attributes[this.attUpdateDateElement] != null)
                {
                  s1 = childNode.Attributes[this.attUpdateDateElement].Value;
                  try
                  {
                    if (s2 == string.Empty)
                      s2 = s1;
                  }
                  catch
                  {
                  }
                }
                if (empty2 != null && empty2 != string.Empty)
                  s1 = empty2;
                if (empty3 != string.Empty)
                {
                  this.sLocalPicPath = string.Empty;
                  this.sServerPicPath = string.Empty;
                  this.sLocalPicPath = this.sBookPath + "\\" + empty3;
                  this.sServerPicPath = this.sContentPath + "/" + this.sBookCode + "/" + empty3;
                }
                if (empty4 != string.Empty)
                {
                  this.sLocalListPath = string.Empty;
                  this.sServerListPath = string.Empty;
                  if (this.sCompression == "1")
                  {
                    this.sLocalListPath = this.sBookPath + "\\" + empty4 + ".zip";
                    this.sServerListPath = this.sContentPath + "/" + this.sBookCode + "/" + empty4 + ".zip";
                  }
                  else
                  {
                    this.sLocalListPath = this.sBookPath + "\\" + empty4 + ".xml";
                    this.sServerListPath = this.sContentPath + "/" + this.sBookCode + "/" + empty4 + ".xml";
                  }
                }
                DateTime dateTime1 = new DateTime();
                DateTime sUpdateDate1;
                try
                {
                  sUpdateDate1 = !(s1 == string.Empty) ? DateTime.Parse(s1, (IFormatProvider) new CultureInfo("en-GB", false)) : DateTime.Now;
                }
                catch
                {
                  sUpdateDate1 = Convert.ToDateTime(DateTime.Now.ToString(this.dateFormat), (IFormatProvider) new DateTimeFormatInfo()
                  {
                    ShortDatePattern = this.dateFormat
                  });
                }
                DateTime dateTime2 = new DateTime();
                DateTime dateTime3 = new DateTime();
                DateTime sUpdateDatePIC1;
                try
                {
                  sUpdateDatePIC1 = !(empty2 == string.Empty) ? DateTime.Parse(empty2, (IFormatProvider) new CultureInfo("en-GB", false)) : DateTime.Now;
                }
                catch
                {
                  sUpdateDatePIC1 = Convert.ToDateTime(DateTime.Now.ToString(this.dateFormat), (IFormatProvider) new DateTimeFormatInfo()
                  {
                    ShortDatePattern = this.dateFormat
                  });
                }
                DateTime sUpdateDatePL1;
                try
                {
                  sUpdateDatePL1 = !(s2 == string.Empty) ? DateTime.Parse(s2, (IFormatProvider) new CultureInfo("en-GB", false)) : DateTime.Now;
                }
                catch
                {
                  sUpdateDatePL1 = Convert.ToDateTime(DateTime.Now.ToString(this.dateFormat), (IFormatProvider) new DateTimeFormatInfo()
                  {
                    ShortDatePattern = this.dateFormat
                  });
                }
                PreviewManager.PrintJob printJob = new PreviewManager.PrintJob(this, this.sPrinterName, this.sOrientation, this.sPaperSize, this.sUserId, this.sPassword, this.sSplittingOption, this.sPrintHeaderFooter, sDescription1, sUpdateDate1, sUpdateDatePIC1, sUpdateDatePL1, this.sPrintPgNos, this.sZoom, this.sCurrentImageZoom, this.sMaintainZoom, this.sZoomFactor, this.pageSplitCount, this.sPrintPic, this.sPrintList, this.sPrintSelList, this.sPrintSideBySide, this.sPrintRng, this.sBookType, this.sLocalListPath, this.sServerListPath, this.sLocalPicPath, this.sServerPicPath, this.spaperUtilization, this.sProxyType, this.sProxyIP, this.sProxyPort, this.sProxyLogin, this.sProxyPassword, this.sTimeOut, this.sCompression, this.sEncryption, this.listColSequence, this.dateFormat, this.copyRightField, this.sLanguage, strPicMemoValue1);
                int[] numArray;
                if (printJob.sMaintainZoom == "1" && printJob.sPrintRng == "0")
                {
                  string[] strArray = printJob.sZoomFactor.Split(',');
                  numArray = new int[strArray.Length];
                  numArray[0] = int.Parse(strArray[0]);
                  numArray[1] = int.Parse(strArray[1]);
                  numArray[2] = int.Parse(strArray[2]);
                  numArray[3] = int.Parse(strArray[3]);
                  numArray[4] = int.Parse(strArray[4]);
                  numArray[5] = int.Parse(strArray[5]);
                  numArray[6] = int.Parse(strArray[6]);
                  numArray[7] = int.Parse(strArray[7]);
                }
                else
                {
                  printJob.sZoomFactor.Split('-');
                  numArray = new int[8];
                }
                printJob.CurrentZoomFactors = numArray;
                if (this.strDuplicatePrinting.ToUpper() == "OFF")
                {
                  if (this.sPreviousPicName != printJob.sLocalPicPath)
                  {
                    this.arrPrintJobs.Add((object) printJob);
                    this.sPreviousPicName = printJob.sLocalPicPath;
                  }
                  else
                    this.sPreviousPicName = printJob.sLocalPicPath;
                }
                else
                {
                  int int32 = Convert.ToInt32(objXmlNode.Attributes[0].Value.ToString());
                  if (this.sPreviousPicName != printJob.sLocalPicPath || int32 != this.intPreviousPageId)
                  {
                    if (this.bOfflineMode)
                    {
                      if (!System.IO.File.Exists(printJob.sLocalPicPath))
                        printJob.sLocalPicPath = string.Empty;
                      if (!System.IO.File.Exists(printJob.sLocalListPath))
                        printJob.sLocalListPath = string.Empty;
                      if (printJob.sLocalListPath != string.Empty || printJob.sLocalPicPath != string.Empty)
                      {
                        this.arrPrintJobs.Add((object) printJob);
                        this.sPreviousPicName = printJob.sLocalPicPath;
                        this.intPreviousPageId = int32;
                      }
                      else if (count == 0 && this.arrPrintJobs.Count == 0 && (printJob.sPrintSelList == "1" && this.dgSelList != null) && this.dgSelList.RowCount != 0)
                      {
                        this.arrPrintJobs.Add((object) printJob);
                        this.sPreviousPicName = printJob.sLocalPicPath;
                        this.intPreviousPageId = int32;
                      }
                    }
                    else
                    {
                      this.arrPrintJobs.Add((object) printJob);
                      this.sPreviousPicName = printJob.sLocalPicPath;
                      this.intPreviousPageId = int32;
                    }
                  }
                  else
                  {
                    this.sPreviousPicName = printJob.sLocalPicPath;
                    this.intPreviousPageId = int32;
                  }
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    public void SetArguments(string[] args)
    {
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        string empty = string.Empty;
        string[] strArray = args[0].Split('|');
        this.sBookPath = args[1];
        this.sServerKey = args[2];
        string str1 = Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini";
        this.sPrinterName = strArray[0];
        this.sPaperSize = strArray[1];
        this.sSplittingOption = strArray[2];
        this.sPrintHeaderFooter = strArray[3];
        this.sPrintPgNos = strArray[4];
        this.sOrientation = strArray[5];
        this.sZoom = strArray[6];
        this.sCurrentImageZoom = strArray[7];
        this.sMaintainZoom = strArray[8];
        this.sZoomFactor = strArray[9];
        this.sPrintRng = strArray[10];
        this.sRngStart = strArray[11];
        this.sRngEnd = strArray[12];
        this.sPrintPic = strArray[13];
        this.sPrintList = strArray[14];
        this.sPrintSelList = strArray[15];
        this.sPrintSideBySide = strArray[16];
        this.sProxyType = strArray[17];
        this.sProxyIP = strArray[18];
        this.sProxyPort = strArray[19];
        this.sProxyLogin = strArray[20];
        this.sProxyPassword = strArray[21];
        this.sTimeOut = strArray[22];
        this.sBookType = strArray[23];
        this.copyrightPrinitng = strArray[24];
        this.sLanguage = strArray[25];
        this.sUserId = strArray[26];
        this.sPassword = strArray[27];
        this.bPrintPicMemo = Convert.ToBoolean(strArray[28]);
        this.spaperUtilization = strArray[29];
        if (new ApplicationMode().bWorkingOffline || Program.objAppFeatures.bDcMode)
          this.bOfflineMode = true;
        else if (Program.objAppMode.bWorkingOffline)
        {
          this.bOfflineMode = true;
        }
        else
        {
          string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName;
          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
          string str2 = path + "\\DataUpdate.XML";
          str1 = Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini";
          string surl1 = iniFileIo.GetKeyValue("SETTINGS", "CONTENT_PATH", str1).ToLower() + "/DataUpdate.XML";
          try
          {
            if (System.IO.File.Exists(str2))
              System.IO.File.Delete(str2);
          }
          catch (Exception ex)
          {
            System.IO.File.Delete(str2);
          }
          new Download().DownloadFile(surl1, str2);
          if (!System.IO.File.Exists(str2))
          {
            this.bOfflineMode = true;
          }
          else
          {
            this.bOfflineMode = false;
            try
            {
              System.IO.File.Delete(str2);
            }
            catch (Exception ex)
            {
              System.IO.File.Delete(str2);
            }
          }
        }
        this.strDuplicatePrinting = strArray[30];
        this.sCompression = !(iniFileIo.GetKeyValue("SETTINGS", "DATA_COMPRESSION", str1).ToLower() == "on") ? "0" : "1";
        this.sEncryption = !(iniFileIo.GetKeyValue("SETTINGS", "DATA_ENCRYPTION", str1).ToLower() == "on") ? "0" : "1";
        this.sContentPath = iniFileIo.GetKeyValue("SETTINGS", "CONTENT_PATH", str1);
        if (this.sPrintList == "1")
        {
          this.listColSequence = new ArrayList();
          this.listColSequence = iniFileIo.GetKeys(str1, "PLIST_SETTINGS");
        }
        try
        {
          string keyValue = iniFileIo.GetKeyValue("PRINTER_SETTINGS", "COPYRIGHT_FIELD", str1);
          this.copyRightField = !(this.copyrightPrinitng == "1") || !(this.sPrintHeaderFooter == "1") ? string.Empty : (!(this.sLanguage.ToUpper() == "ENGLISH") ? this.GetCopyRightText(this.sLanguage, keyValue) : keyValue);
        }
        catch
        {
        }
        if (this.sSplittingOption == "0")
          this.pageSplitCount = 1;
        else if (this.sSplittingOption == "1")
          this.pageSplitCount = 2;
        else if (this.sSplittingOption == "2")
          this.pageSplitCount = 4;
        else if (this.sSplittingOption == "3")
          this.pageSplitCount = 8;
        this.dateFormat = iniFileIo.GetKeyValue("PRINTER_SETTINGS", "DATE_FORMAT", str1);
        if (this.dateFormat == null || this.dateFormat == string.Empty)
          this.dateFormat = "yyyy/MM/dd";
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
      catch
      {
      }
    }

    private bool LoadBookXml(string sBookPath)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        DirectoryInfo directoryInfo = new DirectoryInfo(sBookPath);
        string empty1 = string.Empty;
        this.sBookCode = directoryInfo.Name;
        string str1 = sBookPath + "\\" + this.sBookCode + ".xml";
        if (!System.IO.File.Exists(str1))
          return false;
        try
        {
          xmlDocument.Load(str1);
        }
        catch
        {
          return false;
        }
        if (this.sEncryption == "1")
        {
          string str2 = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str2;
        }
        this.objXmlSchemaNode = xmlDocument.SelectSingleNode("//Schema");
        if (this.objXmlSchemaNode == null)
          return false;
        string empty2 = string.Empty;
        this.attPgNameElement = string.Empty;
        string str3 = string.Empty;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.objXmlSchemaNode.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("ID"))
            str3 = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PAGENAME"))
            this.attPgNameElement = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
            this.attPicElement = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
            this.attListElement = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
            this.attUpdateDateElement = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("UPDATEDATEPIC"))
            this.attUpdateDatePICElement = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("UPDATEDATEPL"))
            this.attUpdateDatePLElement = attribute.Name;
        }
        if (str3 == string.Empty || this.attPgNameElement == string.Empty)
          return false;
        if (this.bMuliRageKey && this.bMultiRange)
        {
          if (!string.IsNullOrEmpty(this.strMultiRngStart) && !string.IsNullOrEmpty(this.strMultiRngEnd))
          {
            this.lstMultiRange = new List<XmlNodeList>();
            string[] strArray1 = this.strMultiRngStart.Split('*');
            string[] strArray2 = this.strMultiRngEnd.Split('*');
            for (int index = 0; index < strArray1.Length; ++index)
            {
              if (strArray1[index].Trim() != string.Empty && strArray2[index].Trim() != string.Empty)
              {
                this.objXmlNodeList = xmlDocument.SelectNodes("//Pg[@" + str3 + ">='" + strArray1[index] + "'][@" + str3 + "<='" + strArray2[index] + "']");
                this.lstMultiRange.Add(this.objXmlNodeList);
              }
            }
          }
          else if (this.sRngStart != "-1" && this.sRngEnd != "-1")
            this.objXmlNodeList = xmlDocument.SelectNodes("//Pg[@" + str3 + ">='" + this.sRngStart + "'][@" + str3 + "<='" + this.sRngEnd + "']");
          else
            this.objXmlNodeList = xmlDocument.SelectNodes("//Pg");
        }
        else if (this.sRngStart != "-1" && this.sRngEnd != "-1")
          this.objXmlNodeList = xmlDocument.SelectNodes("//Pg[@" + str3 + ">='" + this.sRngStart + "'][@" + str3 + "<='" + this.sRngEnd + "']");
        else
          this.objXmlNodeList = xmlDocument.SelectNodes("//Pg");
        return true;
      }
      catch
      {
        return false;
      }
    }

    public void LoadPartsListData(string partListFilePath)
    {
      try
      {
        if (this.objCurrentPrintJob.sCompression == "1")
          partListFilePath = partListFilePath.ToLower().Replace(".zip", ".xml");
        XmlDocument xmlDocument = new XmlDocument();
        this.attributeNames = new ArrayList();
        if (!System.IO.File.Exists(partListFilePath))
        {
          if (this.objCurrentPrintJob.sCompression == "1")
          {
            try
            {
              if (System.IO.File.Exists(partListFilePath.ToLower().Replace(".xml", ".zip")))
                Global.Unzip(partListFilePath.ToLower().Replace(".xml", ".zip"));
            }
            catch
            {
              return;
            }
          }
        }
        if (!System.IO.File.Exists(partListFilePath))
          return;
        xmlDocument.Load(partListFilePath);
        if (this.objCurrentPrintJob.sEncryption == "1")
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        XmlElement documentElement = xmlDocument.DocumentElement;
        this.InitializePartsList(xmlDocument.SelectSingleNode("//Schema"));
        if (this.PartListTable.Columns.Count <= 0)
          return;
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Part");
        for (int index1 = 0; index1 < xmlNodeList.Count; ++index1)
        {
          DataRow row = this.PartListTable.NewRow();
          for (int index2 = 0; index2 < this.attributeNames.Count; ++index2)
          {
            if (xmlNodeList[index1].Attributes[this.attributeNames[index2].ToString()] != null)
              row[index2] = (object) xmlNodeList[index1].Attributes[this.attributeNames[index2].ToString()].Value.ToString();
          }
          this.PartListTable.Rows.Add(row);
        }
      }
      catch
      {
      }
    }

    private void ReadSelectionList()
    {
      this.dicSLColSettings.Clear();
      try
      {
        if (!(this.sPrintSelList == "1"))
          return;
        this.dgSelList = new DataGridView();
        foreach (DataGridViewBand column in (BaseCollection) this.frmParent.objFrmSelectionlist.dgPartslist.Columns)
          this.dgSelList.Columns.Add(column.Clone() as DataGridViewColumn);
        foreach (DataGridViewRow row in (IEnumerable) this.frmParent.objFrmSelectionlist.dgPartslist.Rows)
        {
          int index = this.dgSelList.Rows.Add(row.Clone() as DataGridViewRow);
          foreach (DataGridViewCell cell in (BaseCollection) row.Cells)
            this.dgSelList.Rows[index].Cells[cell.ColumnIndex].Value = cell.Value;
        }
        for (int index1 = 0; index1 < this.dgSelList.Columns.Count; ++index1)
        {
          IniFileIO iniFileIo = new IniFileIO();
          ArrayList arrayList = new ArrayList();
          string empty = string.Empty;
          string str1 = Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini";
          ArrayList keys = iniFileIo.GetKeys(str1, "SLIST_SETTINGS");
          for (int index2 = 0; index2 < keys.Count; ++index2)
          {
            if (keys[index2].ToString() == this.dgSelList.Columns[index1].Tag.ToString())
            {
              this.dgSelList.Columns[index1].Visible = true;
              string str2 = iniFileIo.GetKeyValue("SLIST_SETTINGS", keys[index2].ToString().ToUpper(), str1);
              if (str2.Split('|').Length == 3)
              {
                str2 = str2 + "|True|True|" + str2.Split('|')[1] + "|" + str2.Split('|')[2];
                this.bIsOldINISL = true;
              }
              else if (str2.Split('|').Length == 4)
                str2 = str2 + "|True|True|" + str2.Split('|')[1] + "|" + str2.Split('|')[2];
              string[] strArray1 = str2.Split('|');
              if (strArray1[4].ToString().ToUpper() == "FALSE")
              {
                this.dgSelList.Columns[index1].Visible = false;
                break;
              }
              if (strArray1[4].ToString().ToUpper() == "TRUE")
              {
                try
                {
                  str2.Split('|')[0].ToString();
                  string[] strArray2 = str2.Split('|');
                  string str3 = strArray2[5].ToString();
                  string str4 = strArray2[6].ToString();
                  if (Convert.ToInt32(str4) > 0)
                  {
                    this.dicSLColSettings.Add(this.dgSelList.Columns[index1].Name + "_ALIGN", str3);
                    this.dicSLColSettings.Add(this.dgSelList.Columns[index1].Name + "_WIDTH", str4);
                    break;
                  }
                  this.dgSelList.Columns[index1].Visible = false;
                  break;
                }
                catch (Exception ex)
                {
                  break;
                }
              }
              else
                break;
            }
          }
        }
        this.dgSelList.AllowUserToAddRows = false;
      }
      catch
      {
      }
    }

    public int ExportImage(string sPicPath1, string sUserId1, string sPassword1, int iPageIndex, out string strExportedImagePath, int[] CurrentZoomFactors1)
    {
      this.strExportedImageName = DateTime.Now.ToLongTimeString().Replace(":", string.Empty);
      strExportedImagePath = string.Empty;
      this.objDjVuCtl.SetNameAndPass(sUserId1, sPassword1, 1, 0);
      this.objDjVuCtl.SRC = (string) null;
      this.objDjVuCtl.SRC = sPicPath1;
      this.srcX = 0;
      this.srcY = 0;
      int num;
      try
      {
        strExportedImagePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        strExportedImagePath = strExportedImagePath + "\\" + Application.ProductName;
        strExportedImagePath += "\\tmpPrint";
        if (!Directory.Exists(strExportedImagePath))
          Directory.CreateDirectory(strExportedImagePath);
        strExportedImagePath = strExportedImagePath + "\\tmpPrintImage_" + this.strExportedImageName + ".jpg";
        if (this.objCurrentPrintJob.sMaintainZoom == "0")
        {
          if (System.IO.File.Exists(Application.StartupPath.ToString() + "\\DjVuDecoder.dll"))
          {
            bool flag = false;
            if (PreviewManager.IsDjVuSecured(sPicPath1) == 1)
            {
              PreviewManager.UnSecureDjVu(sPicPath1, sPicPath1, sUserId1, sPassword1);
              flag = true;
            }
            bool jpeg = PreviewManager.DjVuToJPEG(sPicPath1, strExportedImagePath);
            if (flag)
              PreviewManager.SecureDjVu(sPicPath1, sPicPath1, sUserId1, sPassword1);
            num = jpeg ? 0 : 1;
          }
          else
            num = this.objDjVuCtl.ExportImageAs1(strExportedImagePath, "jpeg", false, iPageIndex, false, CurrentZoomFactors1[0], CurrentZoomFactors1[1], CurrentZoomFactors1[2], CurrentZoomFactors1[3], CurrentZoomFactors1[4], CurrentZoomFactors1[5], CurrentZoomFactors1[6], CurrentZoomFactors1[7]);
        }
        else
        {
          this.objDjVuCtl.Zoom = this.objCurrentPrintJob.sCurrentImageZoom;
          bool flag = this.IsExportedImageReq(this.objDjVuCtl.SRC, this.objDjVuCtl.Zoom, this.objDjVuCtl.Rotation, this.objCurrentPrintJob.CurrentZoomFactors);
          if (!System.IO.File.Exists(GSPcLocalViewer.frmPrint.frmPrint.strExportImagePathJpg))
            flag = true;
          if (flag)
          {
            num = this.objDjVuCtl.ExportImageAs1(strExportedImagePath, "jpeg", false, iPageIndex, true, CurrentZoomFactors1[0], CurrentZoomFactors1[1], CurrentZoomFactors1[2], CurrentZoomFactors1[3], CurrentZoomFactors1[4], CurrentZoomFactors1[5], CurrentZoomFactors1[6], CurrentZoomFactors1[7]);
            GSPcLocalViewer.frmPrint.frmPrint.strExportImagePathJpg = strExportedImagePath;
            GSPcLocalViewer.frmPrint.frmPrint.strExortdImgPath = this.objDjVuCtl.SRC;
            GSPcLocalViewer.frmPrint.frmPrint.strExportdImgZoom = this.objDjVuCtl.Zoom;
            GSPcLocalViewer.frmPrint.frmPrint.intExportdImgRotationAngle = this.objDjVuCtl.Rotation;
            GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor = this.objCurrentPrintJob.CurrentZoomFactors;
          }
          else
          {
            strExportedImagePath = GSPcLocalViewer.frmPrint.frmPrint.strExportImagePathJpg;
            num = 0;
          }
        }
      }
      catch
      {
        num = 1;
      }
      return num;
    }

    public void GetPaperSize(string PageName)
    {
      if (this.PaperSize != null && this.PaperSize.PaperName == PageName)
        return;
      PrintDocument printDocument = new PrintDocument();
      for (int index = 0; index < printDocument.PrinterSettings.PaperSizes.Count; ++index)
      {
        if (printDocument.PrinterSettings.PaperSizes[index].PaperName.Contains(PageName))
        {
          this.PaperSize = printDocument.PrinterSettings.PaperSizes[index];
          break;
        }
      }
    }

    private string GetDGHeaderCellValue(string sKey, string sDefaultHeaderValue)
    {
      try
      {
        string str1 = string.Empty;
        bool flag = false;
        if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
        {
          string str2 = Settings.Default.appLanguage + "_GSP_" + this.sServerKey + ".ini";
          if (System.IO.File.Exists(Application.StartupPath + "\\Language XMLs\\" + str2))
          {
            TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str2);
            string str3;
            while ((str3 = textReader.ReadLine()) != null)
            {
              if (str3.ToUpper() == "[PLIST_SETTINGS]")
                flag = true;
              else if (str3.Contains("=") && flag)
              {
                string[] strArray = str3.Split(new string[1]
                {
                  "="
                }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray[0].ToString().ToUpper() == sKey.ToUpper())
                {
                  str1 = strArray[1];
                  break;
                }
              }
              else if (str3.Contains("["))
                flag = false;
            }
            if (str1 == string.Empty)
              str1 = sDefaultHeaderValue;
            textReader.Close();
          }
          else
            str1 = sDefaultHeaderValue;
        }
        else
          str1 = sDefaultHeaderValue;
        return str1;
      }
      catch
      {
        return sDefaultHeaderValue;
      }
    }

    private string GetCopyRightText(string sLanguage, string sDefaultCopyRightText)
    {
      try
      {
        string str1 = string.Empty;
        string str2 = "COPYRIGHT_FIELD";
        bool flag = false;
        string str3 = sLanguage + "_GSP_" + this.sServerKey + ".ini";
        if (System.IO.File.Exists(Application.StartupPath + "\\Language XMLs\\" + str3))
        {
          TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str3);
          string str4;
          while ((str4 = textReader.ReadLine()) != null)
          {
            if (str4.ToUpper() == "[PRINTER_SETTINGS]")
              flag = true;
            else if (str4.Contains("=") && flag)
            {
              string[] strArray = str4.Split(new string[1]
              {
                "="
              }, StringSplitOptions.RemoveEmptyEntries);
              if (strArray[0].ToString().ToUpper() == str2.ToUpper())
              {
                str1 = strArray[1];
                break;
              }
            }
            else if (str4.Contains("["))
              flag = false;
          }
          if (str1 == string.Empty)
            str1 = sDefaultCopyRightText;
          textReader.Close();
        }
        else
          str1 = sDefaultCopyRightText;
        return str1;
      }
      catch
      {
        return sDefaultCopyRightText;
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = string.Empty + "/Screen[@Name='PRINT']" + "/Screen[@Name='PRINT_MANAGER']";
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

    private void InitializePartsList(XmlNode schemaNode)
    {
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        string empty1 = string.Empty;
        string str1 = Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini";
        this.PartListTable = new DataTable("Data");
        this.PartListTable.Rows.Clear();
        this.PartListTable.Columns.Clear();
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        ArrayList keys = iniFileIo.GetKeys(str1, "PLIST_SETTINGS");
        for (int index1 = 0; index1 < keys.Count; ++index1)
        {
          for (int index2 = 0; index2 < schemaNode.Attributes.Count; ++index2)
          {
            if (keys[index1].ToString().ToUpper() == schemaNode.Attributes[index2].Value.ToUpper())
            {
              string empty2 = string.Empty;
              string str2 = schemaNode.Attributes[index2].LocalName.ToString();
              string empty3 = string.Empty;
              string str3 = keys[index1].ToString();
              stringList.Add(str2 + (object) ',' + str3);
              break;
            }
          }
        }
        if (this.dicPLColSettings.Keys.Count > 0)
          this.dicPLColSettings.Clear();
        if (this.dicPLColAlignments.Keys.Count > 0)
          this.dicPLColAlignments.Clear();
        for (int index = 0; index < stringList.Count; ++index)
        {
          string[] strArray1 = stringList[index].ToString().Split(',');
          string str2 = strArray1[0].ToString();
          string str3 = strArray1[1].ToString();
          string str4 = iniFileIo.GetKeyValue("PLIST_SETTINGS", str3.ToUpper(), str1);
          if (str4.Split('|').Length == 3)
          {
            str4 = str4 + "|True|True|" + str4.Split('|')[1] + "|" + str4.Split('|')[2];
            this.bIsOldINIPL = true;
          }
          else if (str4.Split('|').Length == 4)
            str4 = str4 + "|True|True|" + str4.Split('|')[1] + "|" + str4.Split('|')[2];
          if (str4.Split('|')[4] == "True")
          {
            string str5 = str4.Substring(str4.LastIndexOf('|') + 1);
            if (str4 != null && str4 != string.Empty && str5 != "0")
            {
              string str6 = string.Empty;
              string empty2 = string.Empty;
              string empty3 = string.Empty;
              string empty4 = string.Empty;
              try
              {
                string[] strArray2 = str4.Split('|');
                string str7 = str4.Split('|')[0];
                string str8;
                if (strArray2.Length == 7)
                {
                  str8 = str4.Split('|')[5].ToString();
                  empty4 = str4.Split('|')[6].ToString();
                }
                else if (strArray2.Length == 8)
                {
                  str8 = str4.Split('|')[6].ToString();
                  empty4 = str4.Split('|')[7].ToString();
                }
                else
                  str8 = "C";
                if (str4 != null && str4 != string.Empty && empty4 != "0")
                {
                  str6 = this.GetDGHeaderCellValue(str3.ToUpper(), str4.Substring(0, str4.IndexOf("|")));
                  this.PartListTable.Columns.Add(str6);
                  this.attributeNames.Add((object) str2);
                }
                this.dicPLColAlignments.Add(str6, str8 + "|" + empty4);
                this.dicPLColSettings.Add(str6 + "_ALIGN", str8);
                this.dicPLColSettings.Add(str6 + "_WIDTH", empty4);
              }
              catch (Exception ex)
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

    public void ResizeCellHeight(bool bHeader, int iRowIndex, ref int iCellHeight, DataGridView dTGrid, PrintPageEventArgs e, StringFormat strFormat, int iColWidth)
    {
      try
      {
        if (!bHeader)
          iCellHeight = 23;
        int num = 0;
        for (int index = 0; index < dTGrid.Columns.Count; ++index)
        {
          int width = iColWidth;
          if (!this.bIsOldINISL)
          {
            foreach (KeyValuePair<string, string> dicSlColSetting in this.dicSLColSettings)
            {
              if (dicSlColSetting.Key.ToString().EndsWith("_WIDTH"))
              {
                string str = dTGrid.Columns[index].Name.ToUpper() + "_WIDTH";
                if (dicSlColSetting.Key.ToUpper() == str)
                {
                  width = Convert.ToInt32(dicSlColSetting.Value.ToString());
                  num += width;
                  if (num > this.iTotalPageWidth)
                    return;
                  break;
                }
              }
            }
          }
          if (dTGrid.Columns[index].Visible)
          {
            SizeF sizeF = !bHeader ? (dTGrid.Rows[iRowIndex].Cells[index].Value == null || !(dTGrid.Rows[iRowIndex].Cells[index].Value.ToString() != string.Empty) ? e.Graphics.MeasureString(string.Empty, this.previewFont, width, strFormat) : e.Graphics.MeasureString(dTGrid.Rows[iRowIndex].Cells[index].Value.ToString(), this.previewFont, width, strFormat)) : e.Graphics.MeasureString(dTGrid.Columns[index].HeaderText, this.previewFont, width, strFormat);
            if ((double) iCellHeight <= (double) sizeF.Height)
            {
              do
              {
                iCellHeight += 23;
              }
              while ((double) iCellHeight <= (double) sizeF.Height);
            }
          }
        }
      }
      catch
      {
      }
    }

    private string GetPicLocalMemos(string strPageId, int intPicIndex)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      XmlDocument xmlDocument = new XmlDocument();
      XmlNodeList xmlNodeList = (XmlNodeList) null;
      string strValue = string.Empty;
      DateTime dateTime = Convert.ToDateTime("01/01/1900 12:00:00");
      try
      {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string productName = Application.ProductName;
        string str1 = "GSPcLocalViewer";
        string str2 = folderPath + "\\" + str1 + "\\" + this.sServerKey + "\\LocalMemo.xml";
        if (System.IO.File.Exists(str2))
        {
          xmlDocument.Load(str2);
          string innerXml = xmlDocument.InnerXml;
          xmlNodeList = xmlDocument.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookCode + "'][@PageId='" + strPageId + "'][@PicIndex='" + (object) intPicIndex + "']");
        }
        foreach (XmlNode xmlNode in xmlNodeList)
        {
          XmlAttribute attribute1 = xmlNode.Attributes["PartNo"];
          XmlAttribute attribute2 = xmlNode.Attributes["Value"];
          XmlAttribute attribute3 = xmlNode.Attributes["Type"];
          XmlAttribute attribute4 = xmlNode.Attributes["Update"];
          string str3 = attribute3.Value.ToString();
          string str4 = attribute1.Value.ToString();
          DateTime exact = DateTime.ParseExact(Convert.ToString(attribute4.Value).Trim(), "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
          if (str4 == string.Empty && str3.ToUpper() == "TXT" && exact >= dateTime)
          {
            strValue = attribute2.Value.ToString();
            dateTime = exact;
          }
        }
      }
      catch (Exception ex)
      {
        return strValue + Environment.NewLine;
      }
      if (!string.IsNullOrEmpty(strValue) && !this.MemoLinesExist())
        strValue = this.GetMemoValue(strValue, 80, 40);
      return strValue;
    }

    private string GetMemoValue(string strValue, int intHalfWidthLimit, int intFullWidthLimit)
    {
      try
      {
        int num = 0;
        string empty = string.Empty;
        bool flag1 = false;
        bool flag2 = false;
        char[] chArray = new char[strValue.Length];
        char[] charArray = strValue.ToCharArray();
        for (int index = 0; index < charArray.Length; ++index)
        {
          if (Encoding.UTF8.GetBytes(charArray[index].ToString()).Length > 1)
          {
            num += 2;
            flag1 = true;
          }
          else
          {
            ++num;
            flag2 = true;
          }
          if (flag2 && flag1)
          {
            if (num <= intHalfWidthLimit)
              empty += (string) (object) charArray[index];
          }
          else if (flag2 && !flag1)
          {
            if (num <= intHalfWidthLimit)
              empty += (string) (object) charArray[index];
          }
          else if (!flag2 && flag1 && num <= intHalfWidthLimit)
            empty += (string) (object) charArray[index];
        }
        return empty;
      }
      catch (Exception ex)
      {
        return (string) null;
      }
    }

    private void GetMultiRangeVales()
    {
      try
      {
        string str = Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini";
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
            if (!(keyValue.ToString().ToUpper() == "SINGLE"))
              break;
            this.bMultiRange = false;
            break;
          }
          this.bMuliRageKey = false;
          this.bMultiRange = false;
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void SetMultiRangeAruguments()
    {
      if (!this.bMuliRageKey || !this.bMultiRange)
        return;
      this.strMultiRngStart = this.sRngStart;
      this.strMultiRngEnd = this.sRngEnd;
    }

    private bool IsExportedImageReq(string strCurImgPath, string strCurZoomLevel, int intCurRotationAngle, int[] arrCurZoomFactor)
    {
      try
      {
        return !(GSPcLocalViewer.frmPrint.frmPrint.strExortdImgPath == strCurImgPath) || !(GSPcLocalViewer.frmPrint.frmPrint.strExportdImgZoom == strCurZoomLevel) || (GSPcLocalViewer.frmPrint.frmPrint.intExportdImgRotationAngle != intCurRotationAngle || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[0] != arrCurZoomFactor[0]) || (GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[1] != arrCurZoomFactor[1] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[2] != arrCurZoomFactor[2] || (GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[3] != arrCurZoomFactor[3] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[4] != arrCurZoomFactor[4])) || (GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[5] != arrCurZoomFactor[5] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[6] != arrCurZoomFactor[6] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[7] != arrCurZoomFactor[7]);
      }
      catch (Exception ex)
      {
        return true;
      }
    }

    public void PrintManager()
    {
      CustomPrintPreviewDialog printPreviewDialog = new CustomPrintPreviewDialog(this);
      if (this.arrPrintJobs.Count > 0)
      {
        this.objCurrentPrintJob = (PreviewManager.PrintJob) this.arrPrintJobs[0];
        this.arrPrintJobs.RemoveAt(0);
        if (this.objCurrentPrintJob.sPrintPic == "1" && !System.IO.File.Exists(this.objCurrentPrintJob.sLocalPicPath) && this.objCurrentPrintJob.sLocalPicPath != string.Empty)
          new Download().DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
        this.InitializePrintVariables(this.objCurrentPrintJob);
        if (this.objCurrentPrintJob.sPrintPic == "1" || this.objCurrentPrintJob.sPrintList == "1" || this.objCurrentPrintJob.sPrintSelList == "1")
        {
          this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
          this.objDjVuCtl.SRC = this.objCurrentPrintJob.sLocalPicPath;
          if (this.arrPrintJobs.Count > 0)
            this.objParentPrintDlg.objPreviewProcessingDlg.Opacity = 100.0;
          int pageCount = this.objDjVuCtl.GetPageCount();
          for (int iPageIndex = 1; iPageIndex <= pageCount; ++iPageIndex)
          {
            if (this.objCurrentPrintJob.sPrintPic == "1")
            {
              if (this.objCurrentPrintJob.sLocalPicPath != string.Empty)
              {
                try
                {
                  if (this.sMaintainZoom == "1" && System.IO.File.Exists(this.objCurrentPrintJob.sLocalPicPath) && this.ExportImage(this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, iPageIndex, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
                  {
                    this.ImagePrinted = 0;
                    this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
                  }
                  this.InitializePrintDocObject();
                  if (this.objCurrentPrintJob.sZoom == "0" || this.objCurrentPrintJob.sLocalListPath == string.Empty)
                  {
                    this.lstDocuments.Add(this.printDocImg);
                    this.lstPrintJob.Add(this.objCurrentPrintJob);
                  }
                  else
                  {
                    this.lstDocuments.Add(this.printDocHalfPage);
                    this.lstPrintJob.Add(this.objCurrentPrintJob);
                  }
                }
                catch
                {
                }
              }
            }
          }
          if (this.objCurrentPrintJob.sZoom == "0" && this.objCurrentPrintJob.sPrintList == "1" && this.objCurrentPrintJob.sLocalListPath != string.Empty)
          {
            this.InitializePrintDocObject();
            this.lstDocuments.Add(this.printDocList);
            if (this.objCurrentPrintJob.sPrintPic == "0" && this.objCurrentPrintJob.sPrintList == "1")
              this.lstPrintJob.Add(this.objCurrentPrintJob);
          }
        }
      }
      if (this.objCurrentPrintJob.sPrintList == "0" && this.objCurrentPrintJob.sPrintPic == "1" && (this.objCurrentPrintJob.sPrintHeaderFooter == "0" && this.objCurrentPrintJob.strPicMemoValue == string.Empty))
      {
        if (this.arrPrintJobs.Count > 0)
        {
          this.PrintManager();
        }
        else
        {
          try
          {
            if (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
              this.lstDocuments.Add(this.printDocSList);
            this.objMultiPrintDocument = new PreviewManager.MultiPrintDocument(this.lstDocuments, (object) this);
            if (this.lstDocuments.Count > 0)
            {
              string printerName = this.lstDocuments[0].PrinterSettings.PrinterName;
              if (!string.IsNullOrEmpty(printerName))
                this.objMultiPrintDocument.PrinterSettings.PrinterName = printerName;
            }
            printPreviewDialog.Document = (PrintDocument) this.objMultiPrintDocument;
            int num = (int) printPreviewDialog.ShowDialog((IWin32Window) this.frmParent);
          }
          catch (Exception ex)
          {
          }
          this.Close();
        }
      }
      else if (this.arrPrintJobs.Count > 0)
      {
        this.PrintManager();
      }
      else
      {
        try
        {
          if (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
          {
            this.InitializePrintDocObject();
            this.lstDocuments.Add(this.printDocSList);
          }
          this.objMultiPrintDocument = new PreviewManager.MultiPrintDocument(this.lstDocuments, (object) this);
          if (this.lstDocuments.Count > 0)
          {
            string printerName = this.lstDocuments[0].PrinterSettings.PrinterName;
            if (!string.IsNullOrEmpty(printerName))
              this.objMultiPrintDocument.PrinterSettings.PrinterName = printerName;
          }
          printPreviewDialog.Document = (PrintDocument) this.objMultiPrintDocument;
          int num = (int) printPreviewDialog.ShowDialog((IWin32Window) this.frmParent);
        }
        catch (Exception ex)
        {
        }
        this.Close();
      }
    }

    public void InitializePrintDocObject()
    {
      try
      {
        if (this.imagetoPrint != null)
        {
          try
          {
            this.printDocList.DefaultPageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
            if (!(this.objCurrentPrintJob.sZoom == "0"))
            {
              if (!(this.objCurrentPrintJob.sLocalListPath == string.Empty))
                goto label_16;
            }
            if (this.objCurrentPrintJob.pageSplitCount == 1)
              this.printDocImg.DefaultPageSettings.Landscape = this.spaperUtilization == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width && this.sOrientation == "1" || (!(this.spaperUtilization == "0") || this.imagetoPrint.Height >= this.imagetoPrint.Width || !(this.sOrientation == "0")) && (this.spaperUtilization == "0" && this.imagetoPrint.Height > this.imagetoPrint.Width && this.sOrientation == "1" || (!(this.spaperUtilization == "0") || this.imagetoPrint.Height <= this.imagetoPrint.Width || !(this.sOrientation == "0")) && (!(this.objCurrentPrintJob.spaperUtilization == "1") || this.imagetoPrint.Height != this.imagetoPrint.Width ? (!(this.objCurrentPrintJob.spaperUtilization == "0") || this.imagetoPrint.Height != this.imagetoPrint.Width ? this.objCurrentPrintJob.spaperUtilization == "1" && this.imagetoPrint.Height < this.imagetoPrint.Width : !(this.sOrientation == "0")) : !(this.sOrientation == "0")));
            else if (this.objCurrentPrintJob.pageSplitCount == 2 || this.objCurrentPrintJob.pageSplitCount == 8)
            {
              if (this.imagetoPrint.Width < this.imagetoPrint.Height)
                this.printDocImg.DefaultPageSettings.Landscape = true;
              else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
                this.printDocImg.DefaultPageSettings.Landscape = this.sOrientation == "0";
            }
            else if (this.objCurrentPrintJob.pageSplitCount == 4)
            {
              if (this.imagetoPrint.Width > this.imagetoPrint.Height)
                this.printDocImg.DefaultPageSettings.Landscape = true;
              else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
                this.printDocImg.DefaultPageSettings.Landscape = !(this.sOrientation == "0");
            }
          }
          catch
          {
          }
label_16:
          try
          {
            this.printDocSList.DefaultPageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
            if (this.objCurrentPrintJob.sOrientation == "1")
              this.printDocHalfPage.DefaultPageSettings.Landscape = true;
            else
              this.printDocHalfPage.DefaultPageSettings.Landscape = false;
          }
          catch
          {
          }
        }
        else
        {
          try
          {
            this.printDocList.DefaultPageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
            if (!(this.objCurrentPrintJob.sZoom == "0"))
            {
              if (!(this.objCurrentPrintJob.sLocalListPath == string.Empty))
                goto label_39;
            }
            if (this.objCurrentPrintJob.pageSplitCount == 1)
              this.printDocImg.DefaultPageSettings.Landscape = this.spaperUtilization == "0" && this.objDjVuCtl.OrigHeight < this.objDjVuCtl.OrigWidth && this.sOrientation == "1" || (!(this.spaperUtilization == "0") || this.objDjVuCtl.OrigHeight >= this.objDjVuCtl.OrigWidth || !(this.sOrientation == "0")) && (this.spaperUtilization == "0" && this.objDjVuCtl.OrigHeight > this.objDjVuCtl.OrigWidth && this.sOrientation == "1" || (!(this.spaperUtilization == "0") || this.objDjVuCtl.OrigHeight <= this.objDjVuCtl.OrigWidth || !(this.sOrientation == "0")) && (!(this.objCurrentPrintJob.spaperUtilization == "1") || this.objDjVuCtl.OrigHeight != this.objDjVuCtl.OrigWidth ? this.objCurrentPrintJob.spaperUtilization == "1" && this.objDjVuCtl.OrigHeight < this.objDjVuCtl.OrigWidth : !(this.sOrientation == "0")));
            else if (this.objCurrentPrintJob.pageSplitCount == 2 || this.objCurrentPrintJob.pageSplitCount == 8)
            {
              if (this.objDjVuCtl.OrigWidth < this.objDjVuCtl.OrigHeight)
                this.printDocImg.DefaultPageSettings.Landscape = true;
              else if (this.objDjVuCtl.OrigWidth > this.objDjVuCtl.OrigHeight)
                this.printDocImg.DefaultPageSettings.Landscape = false;
              else if (this.objDjVuCtl.OrigHeight == this.objDjVuCtl.OrigWidth)
                this.printDocImg.DefaultPageSettings.Landscape = this.sOrientation == "0";
            }
            else if (this.objCurrentPrintJob.pageSplitCount == 4)
            {
              if (this.objDjVuCtl.OrigWidth > this.objDjVuCtl.OrigHeight)
                this.printDocImg.DefaultPageSettings.Landscape = true;
              else if (this.objDjVuCtl.OrigWidth < this.objDjVuCtl.OrigHeight)
                this.printDocImg.DefaultPageSettings.Landscape = false;
              else if (this.objDjVuCtl.OrigHeight == this.objDjVuCtl.OrigWidth)
                this.printDocImg.DefaultPageSettings.Landscape = !(this.sOrientation == "0");
            }
          }
          catch
          {
          }
label_39:
          try
          {
            this.printDocSList.DefaultPageSettings.Landscape = this.objCurrentPrintJob.sOrientation == "1";
            if (this.objCurrentPrintJob.sOrientation == "1")
              this.printDocHalfPage.DefaultPageSettings.Landscape = true;
            else
              this.printDocHalfPage.DefaultPageSettings.Landscape = false;
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

    private void InitializePrintVariables(PreviewManager.PrintJob objPrintJob1)
    {
      this.GetPaperSize(objPrintJob1.sPaperSize);
      this.printDocImg = new PrintDocument();
      this.printDocImg.DocumentName = "image";
      this.printDocImg.PrintPage += new PrintPageEventHandler(this.doc_PrintImage);
      this.printDocImg.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
      this.PaperSize = this.printDocImg.DefaultPageSettings.PaperSize;
      this.printDocImg.DefaultPageSettings.Margins = this.PrintMargins;
      this.printDocImg.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
      this.printDocList = new PrintDocument();
      this.printDocList.DocumentName = "list";
      this.printDocList.PrintPage += new PrintPageEventHandler(this.doc_PrintList);
      this.printDocList.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
      this.PaperSize = this.printDocImg.DefaultPageSettings.PaperSize;
      this.printDocList.DefaultPageSettings.Margins = this.PrintMargins;
      this.printDocList.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
      this.printDocSList = new PrintDocument();
      this.printDocSList.DocumentName = "sList";
      this.printDocSList.PrintPage += new PrintPageEventHandler(this.doc_PrintSelList);
      this.printDocSList.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
      this.PaperSize = this.printDocSList.DefaultPageSettings.PaperSize;
      this.printDocSList.DefaultPageSettings.Margins = this.PrintMargins;
      this.printDocSList.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
      int[] numArray;
      if (objPrintJob1.sMaintainZoom == "1" && this.objCurrentPrintJob.sPrintRng == "0")
      {
        string[] strArray = objPrintJob1.sZoomFactor.Split(',');
        numArray = new int[strArray.Length];
        numArray[0] = int.Parse(strArray[0]);
        numArray[1] = int.Parse(strArray[1]);
        numArray[2] = int.Parse(strArray[2]);
        numArray[3] = int.Parse(strArray[3]);
        numArray[4] = int.Parse(strArray[4]);
        numArray[5] = int.Parse(strArray[5]);
        numArray[6] = int.Parse(strArray[6]);
        numArray[7] = int.Parse(strArray[7]);
      }
      else
      {
        objPrintJob1.sZoomFactor.Split('-');
        numArray = new int[8];
      }
      objPrintJob1.CurrentZoomFactors = numArray;
    }

    private void PrintSelectionList()
    {
      try
      {
        if (!(this.objCurrentPrintJob.sPrintSelList == "1") || this.dgSelList == null || this.dgSelList.Rows.Count <= 0)
          return;
        this.iSelListRowCount = 0;
        this.printDocSList.Print();
      }
      catch
      {
      }
    }

    public static Image GetResizedImage(Image img, Rectangle rect)
    {
      Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      graphics.DrawImage(img, 0, 0, rect.Width, rect.Height);
      graphics.Dispose();
      try
      {
        return (Image) bitmap.Clone();
      }
      finally
      {
        bitmap.Dispose();
      }
    }

    public void ResizeCellHeight(bool bHeader, int iRowIndex, ref int iCellHeight, DataTable dTable, PrintPageEventArgs e, StringFormat strFormat, int iColWidth)
    {
      try
      {
        if (!bHeader)
          iCellHeight = 23;
        int num = 0;
        for (int index = 0; index < dTable.Columns.Count; ++index)
        {
          int width = iColWidth;
          if (!this.bIsOldINIPL)
          {
            foreach (KeyValuePair<string, string> dicPlColAlignment in this.dicPLColAlignments)
            {
              if (dicPlColAlignment.Key.ToUpper() == dTable.Columns[index].ColumnName.ToUpper())
              {
                width = Convert.ToInt32(dicPlColAlignment.Value.Split('|')[1]);
                num += width;
                if (num > this.iTotalPageWidth)
                  return;
                break;
              }
            }
          }
          SizeF sizeF = !bHeader ? (dTable.Rows[iRowIndex][index] == null || !(dTable.Rows[iRowIndex][index].ToString() != string.Empty) ? e.Graphics.MeasureString(string.Empty, this.previewFont, width, strFormat) : e.Graphics.MeasureString(dTable.Rows[iRowIndex][index].ToString(), this.previewFont, width, strFormat)) : e.Graphics.MeasureString(dTable.Columns[index].Caption, this.previewFont, width, strFormat);
          if ((double) iCellHeight <= (double) sizeF.Height)
          {
            do
            {
              iCellHeight += 23;
            }
            while ((double) iCellHeight <= (double) sizeF.Height);
          }
        }
      }
      catch
      {
      }
    }

    private void InitializePrintVariablesOld(PreviewManager.PrintJob objPrintJob1)
    {
      if (objPrintJob1.sZoom == "0")
      {
        this.printDocFitPage = new PrintDocument();
        this.printDocFitPage.PrintPage += new PrintPageEventHandler(this.doc_PrintFitPage);
        this.printDocFitPage.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
        this.PaperSize = this.printDocFitPage.DefaultPageSettings.PaperSize;
        this.printDocFitPage.DefaultPageSettings.Margins = this.PrintMargins;
        this.printDocFitPage.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
      }
      else if (objPrintJob1.sZoom == "1")
      {
        this.printDocHalfPage = new PrintDocument();
        this.printDocHalfPage.PrintPage += new PrintPageEventHandler(this.doc_PrinthalfPage);
        this.printDocHalfPage.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
        this.PaperSize = this.printDocHalfPage.DefaultPageSettings.PaperSize;
        this.printDocHalfPage.DefaultPageSettings.Margins = this.PrintMargins;
        this.printDocHalfPage.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
      }
      this.printDocSList = new PrintDocument();
      this.printDocSList.PrintPage += new PrintPageEventHandler(this.doc_PrintSelList);
      this.printDocSList.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
      this.PaperSize = this.printDocSList.DefaultPageSettings.PaperSize;
      this.printDocSList.DefaultPageSettings.Margins = this.PrintMargins;
      this.printDocSList.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
    }

    public bool GetInternetConnectionStatus()
    {
      if (string.IsNullOrEmpty(this.sServerKey))
      {
        string str = Application.StartupPath + "\\GSP_" + this.sServerKey + ".ini";
        if (System.IO.File.Exists(str))
        {
          HttpWebResponse response = (HttpWebResponse) WebRequest.Create(new IniFileIO().GetKeyValue("SETTINGS", "CONTENT_PATH", str).ToLower() + "/DataUpdate.XML").GetResponse();
          if (response == null || response.StatusCode != HttpStatusCode.OK)
            return true;
        }
      }
      return false;
    }

    private float GetMemoPrintFontSize()
    {
      float num;
      try
      {
        if (Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_TEXT_SIZE"] != null)
        {
          num = float.Parse(Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_TEXT_SIZE"].ToString());
          if ((double) num >= 6.0)
          {
            if ((double) num <= 16.0)
              goto label_7;
          }
          num = 0.0f;
        }
        else
          num = 0.0f;
      }
      catch (Exception ex)
      {
        num = 0.0f;
      }
label_7:
      return num;
    }

    private int GetMemoPrintLines()
    {
      int num;
      try
      {
        if (Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"] != null)
        {
          num = int.Parse(Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"].ToString());
          if (num >= 1)
          {
            if (num <= 5)
              goto label_7;
          }
          num = 5;
        }
        else
          num = 5;
      }
      catch (Exception ex)
      {
        num = 5;
      }
label_7:
      return num;
    }

    private bool MemoLinesExist()
    {
      bool flag;
      try
      {
        if (Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"] != null)
        {
          int num = int.Parse(Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"].ToString());
          flag = true;
          if (num >= 1)
          {
            if (num <= 5)
              goto label_7;
          }
          flag = false;
        }
        else
          flag = false;
      }
      catch (Exception ex)
      {
        flag = false;
      }
label_7:
      return flag;
    }

    private int GetPicMemoDrawingHeight(string strMemoVal, Font objMemoFont)
    {
      Size size = new Size();
      try
      {
        size = TextRenderer.MeasureText(strMemoVal, this.MemoFont);
        int num1 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
        int num2 = size.Height;
        int num3 = num1;
        while (num3 < size.Width)
        {
          num2 += size.Height;
          num3 += num1;
        }
        if (num2 > size.Height * this.MemoPrintLines)
          num2 = size.Height * this.MemoPrintLines;
        return num2;
      }
      catch (Exception ex)
      {
        return size.Height * this.MemoPrintLines;
      }
    }

    private int GetPicMemoDrawingHeight(string strMemoVal, string strTrimedMemo, Font objMemoFont, int intPrintWidth)
    {
      Size size1 = new Size();
      Size size2 = new Size();
      Size size3 = new Size();
      try
      {
        size3 = TextRenderer.MeasureText(strTrimedMemo, this.MemoFont);
        if (TextRenderer.MeasureText(strMemoVal, this.MemoFont).Height < size3.Height * this.MemoPrintLines)
        {
          Size size4 = TextRenderer.MeasureText(strMemoVal, this.MemoFont);
          int num1 = size3.Height;
          int num2 = intPrintWidth;
          if (size4.Width > num2)
          {
            while (num2 < size4.Width)
            {
              num1 += size3.Height;
              num2 += intPrintWidth;
            }
            if (num1 > size3.Height * this.MemoPrintLines)
              num1 = size3.Height * this.MemoPrintLines;
          }
          else
            num1 = size4.Height;
          return num1;
        }
        size3.Height *= this.MemoPrintLines;
        return size3.Height;
      }
      catch (Exception ex)
      {
        return size3.Height * this.MemoPrintLines;
      }
    }

    public class PrintJob
    {
      public string sPrinterName = string.Empty;
      public string sOrientation = string.Empty;
      public string sPaperSize = string.Empty;
      public string sUserId = string.Empty;
      public string sPassword = string.Empty;
      public string sSplittingOption = string.Empty;
      public string sPrintHeaderFooter = string.Empty;
      public string sDescription = string.Empty;
      public DateTime UpdateDate = new DateTime();
      public DateTime UpdateDatePIC = new DateTime();
      public DateTime UpdateDatePL = new DateTime();
      public string sPrintPgNos = string.Empty;
      public string sZoom = string.Empty;
      public string sCurrentImageZoom = string.Empty;
      public string sMaintainZoom = string.Empty;
      public string sZoomFactor = string.Empty;
      public int pageSplitCount = 1;
      public string sPrintPic = string.Empty;
      public string sPrintList = string.Empty;
      public string sPrintSelList = string.Empty;
      public string sPrintSideBySide = string.Empty;
      public string sPrintRng = string.Empty;
      public string sBookType = string.Empty;
      public string spaperUtilization = string.Empty;
      public string sProxyType = string.Empty;
      public string sTimeOut = string.Empty;
      public string sProxyIP = string.Empty;
      public string sProxyPort = string.Empty;
      public string sProxyLogin = string.Empty;
      public string sProxyPassword = string.Empty;
      public string sCompression = string.Empty;
      public string sEncryption = string.Empty;
      public string sLocalListPath = string.Empty;
      public string sServerListPath = string.Empty;
      public string sLocalPicPath = string.Empty;
      public string sServerPicPath = string.Empty;
      public string dateFormat = string.Empty;
      public string copyRightField = string.Empty;
      public string sLanguage = string.Empty;
      public string strPicMemoValue = string.Empty;
      public PreviewManager frmParent;
      public int[] CurrentZoomFactors;
      private ArrayList listColSequence;

      public PrintJob(PreviewManager frmParent1, string sPrinterName1, string sOrientation1, string sPaperSize1, string sUserId1, string sPassword1, string sSplittingOption1, string sPrintHeaderFooter1, string sDescription1, DateTime sUpdateDate1, DateTime sUpdateDatePIC1, DateTime sUpdateDatePL1, string sPrintPgNos1, string sZoom1, string sCurrentImageZoom1, string sMaintainZoom1, string sZoomFactor1, int pageSplitCount1, string sPrintPic1, string sPrintList1, string sPrintSelList1, string sPrintSideBySide1, string sPrintRng1, string sBookType1, string sLocalListPath1, string sServerListPath1, string sLocalPicPath1, string sServerPicPath1, string spaperUtilization1, string sProxyType1, string sProxyIP1, string sProxyPort1, string sProxyLogin1, string sProxyPassword1, string sTimeOut1, string sCompression1, string sEncryption1, ArrayList listColSequence1, string dateFormat1, string copyRightField1, string sLanguage1, string strPicMemoValue1)
      {
        this.frmParent = frmParent1;
        this.sPrinterName = sPrinterName1;
        this.sOrientation = sOrientation1;
        this.sPaperSize = sPaperSize1;
        this.sUserId = sUserId1;
        this.sPassword = sPassword1;
        this.sSplittingOption = sSplittingOption1;
        this.sPrintHeaderFooter = sPrintHeaderFooter1;
        this.sDescription = sDescription1;
        this.UpdateDate = sUpdateDate1;
        this.UpdateDatePIC = sUpdateDatePIC1;
        this.UpdateDatePL = sUpdateDatePL1;
        this.sPrintPgNos = sPrintPgNos1;
        this.sZoom = sZoom1;
        this.sCurrentImageZoom = sCurrentImageZoom1;
        this.sMaintainZoom = sMaintainZoom1;
        this.sZoomFactor = sZoomFactor1;
        this.pageSplitCount = pageSplitCount1;
        this.sPrintPic = sPrintPic1;
        this.sPrintList = sPrintList1;
        this.listColSequence = listColSequence1;
        this.sPrintSelList = sPrintSelList1;
        this.sPrintSideBySide = sPrintSideBySide1;
        this.sPrintRng = sPrintRng1;
        this.sBookType = sBookType1;
        this.spaperUtilization = spaperUtilization1;
        this.sLocalListPath = sLocalListPath1;
        this.sServerListPath = sServerListPath1;
        this.sLocalPicPath = sLocalPicPath1;
        this.sServerPicPath = sServerPicPath1;
        this.sProxyType = sProxyType1;
        this.sProxyIP = sProxyIP1;
        this.sProxyPort = sProxyPort1;
        this.sProxyLogin = sProxyLogin1;
        this.sProxyPassword = sProxyPassword1;
        this.sTimeOut = sTimeOut1;
        this.sCompression = sCompression1;
        this.sEncryption = sEncryption1;
        this.dateFormat = dateFormat1;
        this.copyRightField = Environment.NewLine + copyRightField1;
        this.copyRightField = copyRightField1;
        this.strPicMemoValue = strPicMemoValue1;
      }
    }

    public class MultiPrintDocument : PrintDocument
    {
      private List<PrintDocument> lstPrintDocs = new List<PrintDocument>();
      private int intPrintDocIndex;
      private PrintEventArgs peaPrintArgs;
      private PreviewManager objParent;
      private PrintDocument[] _documents;

      public MultiPrintDocument(PrintDocument document1, PrintDocument document2)
      {
        try
        {
          this._documents = new PrintDocument[2]
          {
            document1,
            document2
          };
          foreach (PrintDocument document in this._documents)
            this.lstPrintDocs.Add(document);
        }
        catch (Exception ex)
        {
        }
      }

      public MultiPrintDocument(List<PrintDocument> documents)
      {
        this.lstPrintDocs = documents;
      }

      public MultiPrintDocument(List<PrintDocument> documents, object parent)
      {
        this.lstPrintDocs = documents;
        this.objParent = (PreviewManager) parent;
      }

      protected override void OnBeginPrint(PrintEventArgs e)
      {
        base.OnBeginPrint(e);
        if (this.lstPrintDocs.Count == 0)
          e.Cancel = true;
        if (e.Cancel)
          return;
        this.peaPrintArgs = e;
        this.intPrintDocIndex = 0;
        this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], nameof (OnBeginPrint), (object) e);
      }

      protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
      {
        e.PageSettings = this.lstPrintDocs[this.intPrintDocIndex].DefaultPageSettings;
        this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], nameof (OnQueryPageSettings), (object) e);
        base.OnQueryPageSettings(e);
      }

      protected override void OnPrintPage(PrintPageEventArgs e)
      {
        this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], nameof (OnPrintPage), (object) e);
        base.OnPrintPage(e);
        if (e.Cancel || e.HasMorePages)
          return;
        this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], "OnEndPrint", (object) this.peaPrintArgs);
        if (this.peaPrintArgs.Cancel)
          return;
        ++this.intPrintDocIndex;
        if (this.intPrintDocIndex >= this.lstPrintDocs.Count)
          return;
        e.HasMorePages = true;
        this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], "OnBeginPrint", (object) this.peaPrintArgs);
      }

      private void CallMethod(PrintDocument document, string methodName, object args)
      {
        typeof (PrintDocument).InvokeMember(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) document, new object[1]
        {
          args
        });
      }
    }
  }
}

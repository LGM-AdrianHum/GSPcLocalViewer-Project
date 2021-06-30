using AxDjVuCtrlLib;
using GSPcLocalViewer.frmPrint;
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
		private const string dllZipper = "ZIPPER.dll";

		private const string dllDjVuDecoder = "DjVuDecoder.dll";

		private IContainer components;

		public AxDjVuCtrl objDjVuCtl;

		private PrintPreviewDialog printPreviewDialog1;

		public frmViewer frmParent;

		private StringFormat objStringFormat;

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

		private ArrayList listColSequence;

		private XmlNode objXmlSchemaNode;

		private XmlNodeList objXmlNodeList;

		private string attPgNameElement;

		private string attPicElement;

		private string attListElement;

		private string attUpdateDateElement = string.Empty;

		private string attUpdateDatePICElement = string.Empty;

		private string attUpdateDatePLElement = string.Empty;

		public string spaperUtilization = string.Empty;

		public string sBookType = string.Empty;

		public string strExportedImageName = string.Empty;

		public ArrayList arrPrintJobs;

		private PreviewManager.PrintJob objCurrentPrintJob;

		private string sPreviousPicName = string.Empty;

		private int pageSplitCount = 1;

		private System.Drawing.Printing.PaperSize PaperSize;

		private string copyrightPrinitng = string.Empty;

		private string copyRightField = string.Empty;

		private string dateFormat = string.Empty;

		private Margins PrintMargins = new Margins(50, 50, 50, 50);

		private int headerPgRowCounter = 1;

		private int headerPgColCounter = 1;

		private int srcX;

		private int srcY;

		private int splitPageCounter;

		private int iPartsListRowCount;

		private int iSelListRowCount;

		private Image imagetoPrint;

		private string sLocalListPath = string.Empty;

		private string sServerListPath = string.Empty;

		private string sLocalPicPath = string.Empty;

		private string sServerPicPath = string.Empty;

		private PrintDocument printDocFitPage;

		private PrintDocument printDocSList;

		private DataTable PartListTable;

		private ArrayList attributeNames;

		private DataGridView dgSelList;

		private int ImagePrinted;

		private int PageCounter = 1;

		private int iMultiImgCounter = 1;

		private System.Drawing.Font previewFont = Settings.Default.printFont;

		private Dictionary<string, string> dicPLColSettings = new Dictionary<string, string>();

		private Dictionary<string, string> dicSLColSettings = new Dictionary<string, string>();

		private string strMultiRngStart;

		private string strMultiRngEnd;

		private bool bMultiRange;

		private bool bMuliRageKey;

		private List<XmlNodeList> lstMultiRange;

		private bool bPrintPicMemo;

		private int intPreviousPageId;

		private bool bIsOldINIPL;

		private bool bIsOldINISL;

		private Dictionary<string, string> dicPLColAlignments = new Dictionary<string, string>();

		private Dictionary<string, string> dicSLColAlignments = new Dictionary<string, string>();

		private Dictionary<string, string> dicSLTemp = new Dictionary<string, string>();

		private StringFormat strFormat;

		private List<string> lstPicPath = new List<string>();

		private List<PrintDocument> lstDocuments = new List<PrintDocument>();

		private List<string> lstExportedImagesPath = new List<string>();

		private List<DataTable> lstPLTable = new List<DataTable>();

		private List<string> lstListPath = new List<string>();

		private List<PreviewManager.PrintJob> lstPrintJob = new List<PreviewManager.PrintJob>();

		public string sExportedImagePath = string.Empty;

		private int iListCounter;

		private PreviewManager.MultiPrintDocument objMultiPrintDocument;

		private int iImgCounter;

		private int iPLTableCounter;

		private int iPrintJobCounter;

		private PrintDocument printDocImg;

		private PrintDocument printDocList;

		private PrintDocument printDocHalfPage;

		private int iTotalPageWidth;

		private string strPrintViaOcx = string.Empty;

		public bool bHasPages;

		public GSPcLocalViewer.frmPrint.frmPrint objParentPrintDlg;

		private bool bOfflineMode;

		private System.Drawing.Font MemoFont = Settings.Default.printFont;

		private int MemoPrintLines;

		private string strDuplicatePrinting = string.Empty;

		public frmViewerPartslist objPartList;

		public bool bNewPageForListLoaded;

		public ResourceManager rm;

		private DataGridView dgPartsList = new DataGridView();

		public PreviewManager(frmViewer objFrmViewer, string[] args, GSPcLocalViewer.frmPrint.frmPrint objPrintDlg)
		{
			this.InitializeComponent();
			this.objStringFormat = new StringFormat()
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center,
				Trimming = StringTrimming.EllipsisCharacter
			};
			this.arrPrintJobs = new ArrayList();
			this.frmParent = objFrmViewer;
			this.PrintJobRecieved(args);
			this.objDjVuCtl.CurrentLanguage = this.frmParent.AppCurrentLanguage;
			this.objParentPrintDlg = objPrintDlg;
			float memoPrintFontSize = this.GetMemoPrintFontSize();
			if (memoPrintFontSize != 0f)
			{
				this.MemoFont = new System.Drawing.Font(this.previewFont.Name, memoPrintFontSize);
			}
			this.MemoPrintLines = this.GetMemoPrintLines();
			if (!(this.spaperUtilization == "1") || !(this.sPrintPic == "1") || !(this.sZoom == "0"))
			{
				this.StartPrinting();
				return;
			}
			if (this.arrPrintJobs.Count > 1)
			{
				this.objParentPrintDlg.objPreviewProcessingDlg.Opacity = 0;
				this.objParentPrintDlg.objPreviewProcessingDlg.Show();
			}
			this.PrintManager();
		}

		private void changePaperOrientation()
		{
			try
			{
				if (this.objCurrentPrintJob.sSplittingOption == "0")
				{
					this.printDocFitPage.DefaultPageSettings.Landscape = (this.objCurrentPrintJob.sOrientation == "1" ? true : false);
				}
				else if (this.objCurrentPrintJob.sSplittingOption == "2")
				{
					if (this.imagetoPrint.Width != this.imagetoPrint.Height)
					{
						this.printDocFitPage.DefaultPageSettings.Landscape = (this.imagetoPrint.Width > this.imagetoPrint.Height ? true : false);
					}
					else if (this.sOrientation != "0")
					{
						this.printDocFitPage.DefaultPageSettings.Landscape = true;
					}
					else
					{
						this.printDocFitPage.DefaultPageSettings.Landscape = false;
					}
				}
				else if (this.imagetoPrint.Width != this.imagetoPrint.Height)
				{
					this.printDocFitPage.DefaultPageSettings.Landscape = (this.imagetoPrint.Width < this.imagetoPrint.Height ? true : false);
				}
				else if (this.sOrientation != "0")
				{
					this.printDocFitPage.DefaultPageSettings.Landscape = false;
				}
				else
				{
					this.printDocFitPage.DefaultPageSettings.Landscape = true;
				}
			}
			catch
			{
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				this.objDjVuCtl.SRC = string.Empty;
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

		[DllImport("DjVuDecoder.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern bool DjVuToJPEG(string sSourceFilePath, string sDestinationFilePath);

		private void doc_PrintFitPage(object sender, PrintPageEventArgs e)
		{
			string[] resource;
			DateTime now;
			object[] objArray;
			Rectangle pageBounds = e.PageBounds;
			this.iTotalPageWidth = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
			System.Drawing.Size size = new System.Drawing.Size(0, 0);
			string empty = string.Empty;
			string str = string.Empty;
			empty = this.objCurrentPrintJob.strPicMemoValue;
			StringFormat stringFormat = new StringFormat()
			{
				Alignment = StringAlignment.Center
			};
			StringFormat stringFormat1 = new StringFormat()
			{
				FormatFlags = StringFormatFlags.NoWrap,
				Trimming = StringTrimming.Character
			};
			StringFormat stringFormat2 = stringFormat1;
			if (this.objCurrentPrintJob.sPrintPic == "1")
			{
				try
				{
					if (this.ImagePrinted == 0)
					{
						if (this.arrPrintJobs.Count > 0 && this.bPreviewImageNotExported)
						{
							this.bPreviewImageNotExported = false;
							this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
							this.arrPrintJobs.RemoveAt(0);
							this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
							string empty1 = string.Empty;
							if (File.Exists(this.objCurrentPrintJob.sLocalPicPath))
							{
								empty1 = this.objCurrentPrintJob.sLocalPicPath;
							}
							else if (this.objCurrentPrintJob.sServerPicPath != string.Empty)
							{
								Download download = new Download();
								download.DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
								empty1 = this.objCurrentPrintJob.sLocalPicPath;
							}
							if (this.ExportImage(empty1, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
							{
								this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
							}
						}
						if (this.iMultiImgCounter > 1)
						{
							this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
							string str1 = string.Empty;
							if (File.Exists(this.objCurrentPrintJob.sLocalPicPath))
							{
								str1 = this.objCurrentPrintJob.sLocalPicPath;
							}
							else if (this.objCurrentPrintJob.sServerPicPath != string.Empty)
							{
								Download download1 = new Download();
								download1.DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
								str1 = this.objCurrentPrintJob.sLocalPicPath;
							}
							if (this.ExportImage(str1, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
							{
								this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
							}
						}
						if (this.imagetoPrint != null)
						{
							int left = this.PrintMargins.Left;
							int top = this.PrintMargins.Top;
							System.Drawing.Size size1 = new System.Drawing.Size(0, 0);
							if (this.objCurrentPrintJob.pageSplitCount == 1)
							{
								if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height > this.imagetoPrint.Width)
								{
									int height = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width);
										}
									}
									catch (Exception exception)
									{
									}
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str2 = string.Concat(resource);
										System.Drawing.Size size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size3 = TextRenderer.MeasureText(str2, this.previewFont);
										pageBounds = e.PageBounds;
										int num = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										top = top + size2.Height + size3.Height + size.Height;
										height = height - size2.Height - size3.Height - size.Height;
										RectangleF rectangleF = new RectangleF((float)left, (float)(top - size.Height), (float)num, (float)size.Height);
										RectangleF rectangleF1 = new RectangleF((float)left, (float)this.PrintMargins.Top, (float)num, (float)size2.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF1, stringFormat);
										e.Graphics.DrawString(str2, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width - size3.Width) / 2), (float)(this.PrintMargins.Top + size2.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF, stringFormat);
									}
									else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										pageBounds = e.PageBounds;
										int width1 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										top += size.Height;
										RectangleF rectangleF2 = new RectangleF((float)left, (float)(top - size.Height), (float)width1, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF2, stringFormat);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height -= size1.Height;
									}
									decimal num1 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num2 = (int)Math.Round(height * num1, 4);
									if (num2 > width)
									{
										num1 = Math.Round(decimal.Divide(this.imagetoPrint.Height, this.imagetoPrint.Width), 4);
										int num3 = height;
										num2 = width;
										height = (int)(num2 * num1);
										top = top + (num3 - height) / 2;
									}
									left = left + (width - num2) / 2;
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num2, height), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
									left = this.PrintMargins.Left;
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width)
								{
									int height1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width2 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width2);
										}
									}
									catch (Exception exception1)
									{
									}
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null };
										now = DateTime.Now;
										resource[6] = now.ToString(string.Concat(this.objCurrentPrintJob.dateFormat, Environment.NewLine));
										string str3 = string.Concat(resource);
										System.Drawing.Size size4 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size5 = TextRenderer.MeasureText(str3, this.previewFont);
										top = top + size4.Height + size5.Height + size.Height;
										height1 = height1 - size4.Height - size5.Height - size.Height;
										pageBounds = e.PageBounds;
										int width3 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF3 = new RectangleF((float)left, (float)this.PrintMargins.Top, (float)width3, (float)size4.Height);
										RectangleF rectangleF4 = new RectangleF((float)left, (float)(top - size.Height), (float)width3, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF3, stringFormat);
										e.Graphics.DrawString(str3, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width2 - size5.Width) / 2), (float)(this.PrintMargins.Top + size4.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF4, stringFormat);
									}
									else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										pageBounds = e.PageBounds;
										int width4 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										top += size.Height;
										height1 -= size.Height;
										RectangleF rectangleF5 = new RectangleF((float)left, (float)(top - size.Height), (float)width4, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF5, stringFormat);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height1 -= size1.Height;
									}
									decimal num4 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num5 = (int)Math.Round(width2 / num4, 4);
									top = top + (height1 - num5) / 2;
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width2, num5), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width2 - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
								}
								if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height > this.imagetoPrint.Width)
								{
									int height2 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int height3 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height3);
										}
									}
									catch (Exception exception2)
									{
									}
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str4 = string.Concat(resource);
										System.Drawing.Size size6 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size7 = TextRenderer.MeasureText(str4, this.previewFont);
										top = top + size6.Height + size7.Height + size.Height;
										height2 = height2 - size6.Height - size7.Height - size.Height;
										RectangleF rectangleF6 = new RectangleF((float)left, (float)(top - size.Height), (float)height3, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height3 - size6.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str4, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height3 - size7.Width) / 2), (float)(this.PrintMargins.Top + size6.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF6, stringFormat);
									}
									else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += size.Height;
										height2 -= size.Height;
										RectangleF rectangleF7 = new RectangleF((float)left, (float)(top - size.Height), (float)height3, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF7, stringFormat);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height2 -= size1.Height;
									}
									decimal num6 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num7 = (int)Math.Round(height2 * num6, 4);
									left = left + (height3 - num7) / 2;
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num7, height2), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (height3 - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height < this.imagetoPrint.Width)
								{
									int height4 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int height5 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height5);
										}
									}
									catch (Exception exception3)
									{
									}
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str5 = string.Concat(resource);
										System.Drawing.Size size8 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size9 = TextRenderer.MeasureText(str5, this.previewFont);
										top = top + size8.Height + size9.Height + size.Height;
										height4 = height4 - size8.Height - size9.Height - size.Height;
										RectangleF rectangleF8 = new RectangleF((float)left, (float)(top - size.Height), (float)height5, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height5 - size8.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str5, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height5 - size9.Width) / 2), (float)(this.PrintMargins.Top + size8.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF8, stringFormat);
									}
									else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += size.Height;
										height4 -= size.Height;
										RectangleF rectangleF9 = new RectangleF((float)left, (float)(top - size.Height), (float)height5, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF9, stringFormat);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height4 -= size1.Height;
									}
									decimal num8 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num9 = (int)Math.Round(height5 / num8, 4);
									int num10 = height5;
									if (num9 > height4)
									{
										num9 = height4;
										num10 = (int)(num9 * num8);
										left = left + (height5 - num10) / 2;
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num10, num9), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(left + (num10 - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Top - size1.Height));
									}
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
								{
									left = this.PrintMargins.Left;
									top = this.PrintMargins.Top;
									int width5 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int height6 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height6);
										}
									}
									catch (Exception exception4)
									{
									}
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str6 = string.Concat(resource);
										System.Drawing.Size size10 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size11 = TextRenderer.MeasureText(str6, this.previewFont);
										top = top + size10.Height + size11.Height + size.Height;
										width5 = width5 - size10.Height - size11.Height - size.Height;
										int left1 = this.PrintMargins.Left;
										int top1 = this.PrintMargins.Top;
										int width6 = e.PageBounds.Width;
										int left2 = this.PrintMargins.Left;
										int right = this.PrintMargins.Right;
										int height7 = size10.Height;
										int height8 = size11.Height;
										RectangleF rectangleF10 = new RectangleF((float)left, (float)top1, (float)height6, (float)size10.Height);
										RectangleF rectangleF11 = new RectangleF((float)left, (float)(top1 + size10.Height), (float)height6, (float)size11.Height);
										RectangleF rectangleF12 = new RectangleF((float)left, (float)(top1 + size10.Height + size11.Height), (float)height6, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF10, stringFormat);
										e.Graphics.DrawString(str6, this.previewFont, new SolidBrush(Color.Black), rectangleF11, stringFormat);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF12, stringFormat);
									}
									else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += size.Height;
										width5 -= size.Height;
										int left3 = this.PrintMargins.Left;
										int top2 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width7 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF13 = new RectangleF((float)left3, (float)top2, (float)width7, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF13, stringFormat);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										width5 -= size1.Height;
									}
									decimal num11 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num12 = (int)Math.Round(height6 / num11, 4);
									int num13 = height6;
									if (num12 > width5)
									{
										num12 = width5;
										num13 = (int)(num12 * num11);
										left = left + (height6 - num13) / 2;
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num13, width5));
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Top - size1.Height));
									}
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
								{
									int height9 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width8 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width8);
										}
									}
									catch (Exception exception5)
									{
									}
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str7 = string.Concat(resource);
										System.Drawing.Size size12 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size13 = TextRenderer.MeasureText(str7, this.previewFont);
										top = top + size12.Height + size13.Height + size.Height;
										height9 = height9 - size12.Height - size13.Height - size.Height;
										int left4 = this.PrintMargins.Left;
										int top3 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width9 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										int height10 = size12.Height + size13.Height;
										RectangleF rectangleF14 = new RectangleF((float)left4, (float)top3, (float)width9, (float)height10);
										RectangleF rectangleF15 = new RectangleF((float)left4, (float)(top3 + size12.Height + size13.Height), (float)width9, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF14, stringFormat);
										e.Graphics.DrawString(str7, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width8 - size13.Width) / 2), (float)(this.PrintMargins.Top + size12.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF15, stringFormat);
									}
									else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										int left5 = this.PrintMargins.Left;
										int top4 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width10 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										top += size.Height;
										height9 -= size.Height;
										RectangleF rectangleF16 = new RectangleF((float)left5, (float)top4, (float)width10, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF16, stringFormat);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height9 -= size1.Height;
									}
									decimal num14 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num15 = (int)Math.Round(width8 / num14, 4);
									top = top + (height9 - num15) / 2;
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width8, num15));
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width8 - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
								}
							}
							else if (this.objCurrentPrintJob.pageSplitCount == 2)
							{
								if (this.imagetoPrint.Width < this.imagetoPrint.Height)
								{
									int width11 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int height11 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height11);
										}
									}
									catch (Exception exception6)
									{
									}
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size14 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									int top5 = 0;
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											width11 -= size.Height;
											RectangleF rectangleF17 = new RectangleF((float)left, (float)(top - size.Height), (float)height11, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF17, stringFormat);
										}
										top5 = size14.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str8 = string.Concat(resource);
										System.Drawing.Size size15 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size16 = TextRenderer.MeasureText(str8, this.previewFont);
										top = top + size15.Height + size16.Height + size.Height;
										width11 = width11 - size15.Height - size16.Height - size.Height;
										RectangleF rectangleF18 = new RectangleF((float)left, (float)(top - size.Height), (float)height11, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size15.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str8, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size16.Width) / 2), (float)(this.PrintMargins.Top + size15.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF18, stringFormat);
										top5 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphics = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphics.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size14.Width), (float)top5);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										width11 -= size1.Height;
									}
									decimal num16 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num17 = width11;
									int num18 = (int)Math.Round(width11 * num16, 4);
									num18 *= 2;
									num17 *= 2;
									while (num18 > height11 || num17 > width11 * 2)
									{
										num17 -= 10;
										num18 -= 10;
									}
									if (this.srcY == 0)
									{
										top = top + (width11 * 2 - num17) / 2;
									}
									num17 /= 2;
									left = left + (height11 - num18) / 2;
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num18, num17), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
									}
									this.srcY = this.imagetoPrint.Height / 2;
								}
								else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
								{
									int height12 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width12 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width12);
										}
									}
									catch (Exception exception7)
									{
									}
									int top6 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size17 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height12 -= size.Height;
											RectangleF rectangleF19 = new RectangleF((float)left, (float)(top - size.Height), (float)width12, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF19, stringFormat);
										}
										top6 = size17.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str9 = string.Concat(resource);
										System.Drawing.Size size18 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size19 = TextRenderer.MeasureText(str9, this.previewFont);
										top = top + size18.Height + size19.Height + size.Height;
										height12 = height12 - size18.Height - size19.Height - size.Height;
										RectangleF rectangleF20 = new RectangleF((float)left, (float)(top - size.Height), (float)width12, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width12 - size18.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str9, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width12 - size19.Width) / 2), (float)(this.PrintMargins.Top + size18.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF20, stringFormat);
										top6 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphic = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphic.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size17.Width), (float)top6);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height12 -= size1.Height;
									}
									decimal num19 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num20 = width12;
									int num21 = (int)Math.Round(width12 * num19, 4);
									if (num21 > height12)
									{
										num21 = height12;
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num20, num21), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width12 - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
									this.srcX = this.imagetoPrint.Width / 2;
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
								{
									int height13 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width13 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width13);
										}
									}
									catch (Exception exception8)
									{
									}
									int top7 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size20 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height13 -= size.Height;
											int left6 = this.PrintMargins.Left;
											int top8 = this.PrintMargins.Top;
											pageBounds = e.PageBounds;
											int width14 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
											RectangleF rectangleF21 = new RectangleF((float)left6, (float)top8, (float)width14, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF21, stringFormat);
										}
										top7 = size20.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str10 = string.Concat(resource);
										System.Drawing.Size size21 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size22 = TextRenderer.MeasureText(str10, this.previewFont);
										top = top + size21.Height + size22.Height + size.Height;
										height13 = height13 - size21.Height - size22.Height - size.Height;
										int left7 = this.PrintMargins.Left;
										int top9 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width15 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										int height14 = size21.Height + size22.Height;
										RectangleF rectangleF22 = new RectangleF((float)left7, (float)top9, (float)width15, (float)height14);
										RectangleF rectangleF23 = new RectangleF((float)left7, (float)(top9 + size21.Height + size22.Height), (float)width15, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF22, stringFormat);
										e.Graphics.DrawString(str10, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width13 - size22.Width) / 2), (float)(this.PrintMargins.Top + size21.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF23, stringFormat);
										top7 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphics1 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphics1.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size20.Width), (float)top7);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height13 -= size1.Height;
									}
									decimal num22 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num23 = width13;
									int num24 = (int)Math.Round(width13 * num22, 4);
									if (num24 < height13)
									{
										num24 = height13;
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num23, num24), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
									this.srcX = this.imagetoPrint.Width / 2;
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
								{
									int height15 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int height16 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height16);
										}
									}
									catch (Exception exception9)
									{
									}
									int top10 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size23 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height15 -= size.Height;
											int left8 = this.PrintMargins.Left;
											int top11 = this.PrintMargins.Top;
											pageBounds = e.PageBounds;
											int width16 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
											RectangleF rectangleF24 = new RectangleF((float)left8, (float)top11, (float)width16, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF24, stringFormat);
										}
										top10 = size23.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str11 = string.Concat(resource);
										System.Drawing.Size size24 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size25 = TextRenderer.MeasureText(str11, this.previewFont);
										top = top + size24.Height + size25.Height + size.Height;
										height15 = height15 - size24.Height - size25.Height - size.Height;
										int left9 = this.PrintMargins.Left;
										int top12 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width17 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										int height17 = size24.Height + size25.Height;
										RectangleF rectangleF25 = new RectangleF((float)left9, (float)top12, (float)width17, (float)height17);
										RectangleF rectangleF26 = new RectangleF((float)left9, (float)(top12 + size24.Height + size25.Height), (float)width17, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF25, stringFormat);
										e.Graphics.DrawString(str11, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height16 - size25.Width) / 2), (float)(this.PrintMargins.Top + size24.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF26, stringFormat);
										top10 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphic1 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphic1.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size23.Width), (float)top10);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height15 -= size1.Height;
									}
									decimal num25 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num26 = height15;
									int num27 = (int)Math.Round(height15 / num25, 4);
									if (num27 > height16)
									{
										num27 = height16;
										num26 = (int)(num27 * num25);
									}
									num27 = height16;
									num26 = (int)(num27 * num25);
									num26 /= 2;
									top = top + (height15 - num26) / 2;
									left = left + (height16 - num27) / 2;
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num27, num26), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
									}
									this.srcY = this.imagetoPrint.Height / 2;
								}
								this.splitPageCounter++;
								if (this.splitPageCounter >= this.objCurrentPrintJob.pageSplitCount)
								{
									e.HasMorePages = false;
									this.splitPageCounter = 0;
									this.headerPgColCounter = 1;
									this.headerPgRowCounter = 1;
								}
								else
								{
									if (this.imagetoPrint.Height != this.imagetoPrint.Width)
									{
										if (this.imagetoPrint.Width >= this.imagetoPrint.Height)
										{
											this.headerPgColCounter++;
										}
										else
										{
											this.headerPgRowCounter++;
										}
									}
									else if (this.objCurrentPrintJob.sOrientation == "0")
									{
										this.headerPgRowCounter++;
									}
									else if (this.objCurrentPrintJob.sOrientation == "1")
									{
										this.headerPgColCounter++;
									}
									e.HasMorePages = true;
									return;
								}
							}
							else if (this.objCurrentPrintJob.pageSplitCount == 4)
							{
								if (this.imagetoPrint.Width < this.imagetoPrint.Height)
								{
									int width18 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height18 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width18);
										}
									}
									catch (Exception exception10)
									{
									}
									int top13 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size26 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height18 -= size.Height;
											RectangleF rectangleF27 = new RectangleF((float)left, (float)(top - size.Height), (float)width18, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF27, stringFormat);
										}
										top13 = size26.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str12 = string.Concat(resource);
										System.Drawing.Size size27 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size28 = TextRenderer.MeasureText(str12, this.previewFont);
										top = top + size27.Height + size28.Height + size.Height;
										height18 = height18 - size27.Height - size28.Height - size.Height;
										RectangleF rectangleF28 = new RectangleF((float)left, (float)(top - size.Height), (float)width18, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width18 - size27.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str12, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width18 - size28.Width) / 2), (float)(this.PrintMargins.Top + size27.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF28, stringFormat);
										top13 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphics2 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphics2.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size26.Width), (float)top13);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height18 -= size1.Height;
									}
									decimal num28 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									if ((int)Math.Round(height18 * num28, 4) > width18)
									{
										int num29 = (int)(width18 / num28);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width18, height18), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
								{
									int height19 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width19 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height19);
										}
									}
									catch (Exception exception11)
									{
									}
									int top14 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size29 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											width19 -= size.Height;
											RectangleF rectangleF29 = new RectangleF((float)left, (float)(top - size.Height), (float)height19, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF29, stringFormat);
										}
										top14 = size29.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str13 = string.Concat(resource);
										System.Drawing.Size size30 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size31 = TextRenderer.MeasureText(str13, this.previewFont);
										top = top + size30.Height + size31.Height + size.Height;
										width19 = width19 - size30.Height - size31.Height - size.Height;
										RectangleF rectangleF30 = new RectangleF((float)left, (float)(top - size.Height), (float)height19, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height19 - size30.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str13, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height19 - size31.Width) / 2), (float)(this.PrintMargins.Top + size30.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF30, stringFormat);
										top14 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphic2 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphic2.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size29.Width), (float)top14);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										width19 -= size1.Height;
									}
									decimal num30 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num31 = height19;
									int num32 = (int)Math.Round(num31 / num30, 4);
									if (num32 > width19)
									{
										num32 = width19;
										num31 = (int)(num32 * num30);
									}
									if (this.splitPageCounter == 0)
									{
										top = top + (width19 - num32);
										left = left + (height19 - num31);
									}
									else if (this.splitPageCounter == 1)
									{
										top = top + (width19 - num32);
									}
									else if (this.splitPageCounter == 2)
									{
										left = left + (height19 - num31);
									}
									else if (this.splitPageCounter == 3)
									{
										top = top + (width19 - num32);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num31, num32), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (height19 - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
								{
									int height20 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width20 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height20);
										}
									}
									catch (Exception exception12)
									{
									}
									int top15 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size32 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											width20 -= size.Height;
											int left10 = this.PrintMargins.Left;
											int top16 = this.PrintMargins.Top;
											pageBounds = e.PageBounds;
											int width21 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
											RectangleF rectangleF31 = new RectangleF((float)left10, (float)top16, (float)width21, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF31, stringFormat);
										}
										top15 = size32.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str14 = string.Concat(resource);
										System.Drawing.Size size33 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size34 = TextRenderer.MeasureText(str14, this.previewFont);
										top = top + size33.Height + size34.Height + size.Height;
										width20 = width20 - size33.Height - size34.Height - size.Height;
										int left11 = this.PrintMargins.Left;
										int top17 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width22 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										int height21 = size33.Height + size34.Height;
										RectangleF rectangleF32 = new RectangleF((float)left11, (float)top17, (float)width22, (float)height21);
										RectangleF rectangleF33 = new RectangleF((float)left11, (float)(top17 + size33.Height + size34.Height), (float)width22, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF32, stringFormat);
										e.Graphics.DrawString(str14, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height20 - size34.Width) / 2), (float)(this.PrintMargins.Top + size33.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF33, stringFormat);
										top15 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphics3 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphics3.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size32.Width), (float)top15);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										width20 -= size1.Height;
									}
									decimal num33 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num34 = height20;
									int num35 = (int)Math.Round(num34 / num33, 4);
									if (num35 > width20)
									{
										num35 = width20;
										num34 = (int)(num35 * num33);
									}
									if (this.splitPageCounter == 0)
									{
										top = top + (width20 - num35);
										left = left + (height20 - num34);
									}
									else if (this.splitPageCounter == 1)
									{
										top = top + (width20 - num35);
									}
									else if (this.splitPageCounter == 2)
									{
										left = left + (height20 - num34);
									}
									else if (this.splitPageCounter == 3)
									{
										top = top + (width20 - num35);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num34, num35), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
								{
									int width23 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height22 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width23);
										}
									}
									catch (Exception exception13)
									{
									}
									int top18 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size35 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height22 -= size.Height;
											int left12 = this.PrintMargins.Left;
											int top19 = this.PrintMargins.Top;
											pageBounds = e.PageBounds;
											int width24 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
											RectangleF rectangleF34 = new RectangleF((float)left12, (float)top19, (float)width24, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF34, stringFormat);
										}
										top18 = size35.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str15 = string.Concat(resource);
										System.Drawing.Size size36 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size37 = TextRenderer.MeasureText(str15, this.previewFont);
										top = top + size36.Height + size37.Height + size.Height;
										height22 = height22 - size36.Height - size37.Height - size.Height;
										int left13 = this.PrintMargins.Left;
										int top20 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width25 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										int height23 = size36.Height + size37.Height;
										RectangleF rectangleF35 = new RectangleF((float)left13, (float)top20, (float)width25, (float)height23);
										RectangleF rectangleF36 = new RectangleF((float)left13, (float)(top20 + size36.Height + size37.Height), (float)width25, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF35, stringFormat);
										e.Graphics.DrawString(str15, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width23 - size37.Width) / 2), (float)(this.PrintMargins.Top + size36.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF36, stringFormat);
										top18 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphic3 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphic3.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size35.Width), (float)top18);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height22 -= size1.Height;
									}
									decimal num36 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									if ((int)Math.Round(height22 * num36, 4) > width23)
									{
										int num37 = (int)(width23 / num36);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width23, height22), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
								}
								this.splitPageCounter++;
								if (this.splitPageCounter >= this.objCurrentPrintJob.pageSplitCount)
								{
									e.HasMorePages = false;
									this.splitPageCounter = 0;
									this.headerPgColCounter = 1;
									this.headerPgRowCounter = 1;
									this.srcX = 0;
									this.srcY = 0;
								}
								else
								{
									if (this.splitPageCounter % 2 == 1)
									{
										this.srcX = this.imagetoPrint.Width / 2;
									}
									this.headerPgColCounter++;
									if (this.splitPageCounter == 2)
									{
										this.headerPgRowCounter++;
										this.headerPgColCounter = 1;
										this.srcY = this.imagetoPrint.Height / 2;
										this.srcX = 0;
									}
									e.HasMorePages = true;
									return;
								}
							}
							else if (this.objCurrentPrintJob.pageSplitCount == 8)
							{
								if (this.imagetoPrint.Width < this.imagetoPrint.Height)
								{
									int height24 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int height25 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height25);
										}
									}
									catch (Exception exception14)
									{
									}
									int top21 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size38 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height24 -= size.Height;
											RectangleF rectangleF37 = new RectangleF((float)left, (float)(top - size.Height), (float)height25, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF37, stringFormat);
										}
										top21 = size38.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str16 = string.Concat(resource);
										System.Drawing.Size size39 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size40 = TextRenderer.MeasureText(str16, this.previewFont);
										top = top + size39.Height + size40.Height + size.Height;
										height24 = height24 - size39.Height - size40.Height - size.Height;
										RectangleF rectangleF38 = new RectangleF((float)left, (float)(top - size.Height), (float)height25, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height25 - size39.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str16, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height25 - size40.Width) / 2), (float)(this.PrintMargins.Top + size39.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF38, stringFormat);
										top21 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphics4 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphics4.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size38.Width), (float)top21);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height24 -= size1.Height;
									}
									decimal num38 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num39 = height24;
									int num40 = (int)Math.Round(height24 / num38, 4);
									if (num40 > height25)
									{
										num40 = height25;
										num39 = (int)(num40 * num38);
									}
									if (this.splitPageCounter % 2 == 0)
									{
										left = left + (height25 - num40);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num40, num39), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
								{
									int height26 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width26 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width26);
										}
									}
									catch (Exception exception15)
									{
									}
									int top22 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size41 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height26 -= size.Height;
											RectangleF rectangleF39 = new RectangleF((float)left, (float)(top - size.Height), (float)width26, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF39, stringFormat);
										}
										top22 = size41.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str17 = string.Concat(resource);
										System.Drawing.Size size42 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size43 = TextRenderer.MeasureText(str17, this.previewFont);
										top = top + size42.Height + size43.Height + size.Height;
										height26 = height26 - size42.Height - size43.Height - size.Height;
										RectangleF rectangleF40 = new RectangleF((float)left, (float)(top - size.Height), (float)width26, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width26 - size42.Width) / 2), (float)this.PrintMargins.Top);
										e.Graphics.DrawString(str17, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width26 - size43.Width) / 2), (float)(this.PrintMargins.Top + size42.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF40, stringFormat);
										top22 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphic4 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphic4.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size41.Width), (float)top22);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height26 -= size1.Height;
									}
									decimal num41 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num42 = width26;
									int num43 = (int)Math.Round(width26 * num41, 4);
									if (num43 > height26)
									{
										num43 = height26;
									}
									if (this.splitPageCounter < 4)
									{
										top = top + (height26 - num43);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num42, num43), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width26 - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "1")
								{
									int height27 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int width27 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width27);
										}
									}
									catch (Exception exception16)
									{
									}
									int top23 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size44 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height27 -= size.Height;
											int left14 = this.PrintMargins.Left;
											int top24 = this.PrintMargins.Top;
											pageBounds = e.PageBounds;
											int width28 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
											RectangleF rectangleF41 = new RectangleF((float)left14, (float)top24, (float)width28, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF41, stringFormat);
										}
										top23 = size44.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str18 = string.Concat(resource);
										System.Drawing.Size size45 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size46 = TextRenderer.MeasureText(str18, this.previewFont);
										top = top + size45.Height + size46.Height + size.Height;
										height27 = height27 - size45.Height - size46.Height - size.Height;
										int left15 = this.PrintMargins.Left;
										int top25 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width29 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										int height28 = size45.Height + size46.Height;
										RectangleF rectangleF42 = new RectangleF((float)left15, (float)top25, (float)width29, (float)height28);
										RectangleF rectangleF43 = new RectangleF((float)left15, (float)(top25 + size45.Height + size46.Height), (float)width29, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF42, stringFormat);
										e.Graphics.DrawString(str18, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width27 - size46.Width) / 2), (float)(this.PrintMargins.Top + size45.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF43, stringFormat);
										top23 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphics5 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphics5.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size44.Width), (float)top23);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height27 -= size1.Height;
									}
									decimal num44 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num45 = width27;
									int num46 = (int)Math.Round(width27 * num44, 4);
									if (num46 > height27)
									{
										num46 = height27;
									}
									if (this.splitPageCounter < 4)
									{
										top = top + (height27 - num46);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num45, num46), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size1.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size1.Height));
									}
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width && this.objCurrentPrintJob.sOrientation == "0")
								{
									int height29 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
									int height30 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
									try
									{
										if (empty.Length > 0)
										{
											str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
											str = str.Substring(0, 1);
											size.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height30);
										}
									}
									catch (Exception exception17)
									{
									}
									int top26 = 0;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									System.Drawing.Size size47 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
									if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
									{
										if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
										{
											top += size.Height;
											height29 -= size.Height;
											int left16 = this.PrintMargins.Left;
											int top27 = this.PrintMargins.Top;
											pageBounds = e.PageBounds;
											int width30 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
											RectangleF rectangleF44 = new RectangleF((float)left16, (float)top27, (float)width30, (float)size.Height);
											e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF44, stringFormat);
										}
										top26 = size47.Height;
									}
									else
									{
										resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
										now = DateTime.Now;
										resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
										resource[7] = Environment.NewLine;
										string str19 = string.Concat(resource);
										System.Drawing.Size size48 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
										System.Drawing.Size size49 = TextRenderer.MeasureText(str19, this.previewFont);
										top = top + size48.Height + size49.Height + size.Height;
										height29 = height29 - size48.Height - size49.Height - size.Height;
										int left17 = this.PrintMargins.Left;
										int top28 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width31 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										int height31 = size48.Height + size49.Height;
										RectangleF rectangleF45 = new RectangleF((float)left17, (float)top28, (float)width31, (float)height31);
										RectangleF rectangleF46 = new RectangleF((float)left17, (float)(top28 + size48.Height + size49.Height), (float)width31, (float)size.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF45, stringFormat);
										e.Graphics.DrawString(str19, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height30 - size49.Width) / 2), (float)(this.PrintMargins.Top + size48.Height));
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF46, stringFormat);
										top26 = this.PrintMargins.Top;
									}
									if (this.objCurrentPrintJob.sPrintPgNos == "1")
									{
										Graphics graphic5 = e.Graphics;
										objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
										graphic5.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size47.Width), (float)top26);
									}
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
										height29 -= size1.Height;
									}
									decimal num47 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
									int num48 = height29;
									int num49 = (int)Math.Round(height29 / num47, 4);
									if (num49 > height30)
									{
										num49 = height30;
										num48 = (int)(num49 * num47);
									}
									num49 = height30;
									num48 = (int)(num49 * num47);
									num48 /= 2;
									top = top + (height29 - num48) / 2;
									if (this.splitPageCounter % 2 == 0)
									{
										left = left + (height30 - num49);
									}
									e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num49, num48), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size1.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size1.Height));
									}
								}
								this.splitPageCounter++;
								if (this.splitPageCounter >= this.objCurrentPrintJob.pageSplitCount)
								{
									e.HasMorePages = false;
									this.splitPageCounter = 0;
									this.headerPgColCounter = 1;
									this.headerPgRowCounter = 1;
									this.srcX = 0;
									this.srcY = 0;
								}
								else
								{
									if (this.imagetoPrint.Width < this.imagetoPrint.Height)
									{
										this.headerPgColCounter++;
										if (this.splitPageCounter % 2 != 0)
										{
											this.srcX = this.imagetoPrint.Width / 2;
										}
										else
										{
											this.headerPgColCounter = 1;
											this.headerPgRowCounter++;
											PreviewManager previewManager = this;
											previewManager.srcY = previewManager.srcY + this.imagetoPrint.Height / 4;
											this.srcX = 0;
										}
									}
									else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
									{
										this.headerPgColCounter++;
										if (this.splitPageCounter % 4 != 0)
										{
											PreviewManager previewManager1 = this;
											previewManager1.srcX = previewManager1.srcX + this.imagetoPrint.Width / 4;
										}
										else
										{
											this.headerPgColCounter = 1;
											this.headerPgRowCounter++;
											PreviewManager previewManager2 = this;
											previewManager2.srcY = previewManager2.srcY + this.imagetoPrint.Height / 2;
											this.srcX = 0;
										}
									}
									else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
									{
										if (this.objCurrentPrintJob.sOrientation == "1")
										{
											this.headerPgColCounter++;
											if (this.splitPageCounter % 4 != 0)
											{
												PreviewManager previewManager3 = this;
												previewManager3.srcX = previewManager3.srcX + this.imagetoPrint.Width / 4;
											}
											else
											{
												this.headerPgColCounter = 1;
												this.headerPgRowCounter++;
												PreviewManager previewManager4 = this;
												previewManager4.srcY = previewManager4.srcY + this.imagetoPrint.Height / 2;
												this.srcX = 0;
											}
										}
										else if (this.objCurrentPrintJob.sOrientation == "0")
										{
											this.headerPgColCounter++;
											if (this.splitPageCounter % 2 != 0)
											{
												this.srcX = this.imagetoPrint.Width / 2;
											}
											else
											{
												this.headerPgColCounter = 1;
												this.headerPgRowCounter++;
												PreviewManager previewManager5 = this;
												previewManager5.srcY = previewManager5.srcY + this.imagetoPrint.Height / 4;
												this.srcX = 0;
											}
										}
									}
									e.HasMorePages = true;
									return;
								}
							}
							if (this.objDjVuCtl.GetPageCount() <= this.iMultiImgCounter)
							{
								this.ImagePrinted++;
								e.HasMorePages = false;
							}
							else
							{
								this.iMultiImgCounter++;
								e.HasMorePages = true;
								return;
							}
						}
					}
					goto Label0;
				}
				catch (Exception exception18)
				{
					goto Label0;
				}
			}
			else
			{
				goto Label0;
			}
			return;
		Label0:
			if (this.objCurrentPrintJob.sPrintPic == "0" && this.arrPrintJobs.Count > 0 && !this.bHasPages)
			{
				this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
				this.arrPrintJobs.RemoveAt(0);
				while (this.objCurrentPrintJob.sLocalListPath == string.Empty)
				{
					if (this.arrPrintJobs.Count <= 0)
					{
						e.HasMorePages = false;
						return;
					}
					else
					{
						this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
						this.arrPrintJobs.RemoveAt(0);
					}
				}
			}
			this.bHasPages = false;
			if (this.objCurrentPrintJob.sPrintList == "1" && this.objCurrentPrintJob.sLocalListPath != string.Empty)
			{
				try
				{
					if (this.PartListTable == null)
					{
						this.iPartsListRowCount = 0;
						if (!File.Exists(this.objCurrentPrintJob.sLocalListPath) && this.objCurrentPrintJob.sLocalListPath != string.Empty)
						{
							Download download2 = new Download();
							download2.DownloadFile(this.objCurrentPrintJob.sServerListPath, this.objCurrentPrintJob.sLocalListPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
							int num50 = -11;
							int num51 = 2;
							int num52 = 500;
							for (int i = 0; i < num51; i++)
							{
								try
								{
									if (!File.Exists(this.objCurrentPrintJob.sLocalListPath))
									{
										break;
									}
									else
									{
										IntPtr intPtr = PreviewManager.UnZipFile(this.objCurrentPrintJob.sLocalListPath);
										num50 = intPtr.ToInt32();
										if (num50 != 1 || !File.Exists(this.objCurrentPrintJob.sLocalListPath.ToLower().Replace(".zip", ".xml")))
										{
											Thread.Sleep(num52);
										}
										else
										{
											break;
										}
									}
								}
								catch
								{
								}
							}
						}
						this.LoadPartsListData(this.objCurrentPrintJob.sLocalListPath);
					}
					if (this.PartListTable != null && this.PartListTable.Rows.Count > 0)
					{
						if (this.bNewPageForListLoaded || !(this.objCurrentPrintJob.sPrintPic == "1"))
						{
							int num53 = 23;
							System.Drawing.Size size50 = new System.Drawing.Size(0, 0);
							int num54 = 0;
							int num55 = 0;
							int left18 = this.PrintMargins.Left;
							int top29 = this.PrintMargins.Top;
							float single = (float)this.PrintMargins.Left;
							float single1 = (float)this.PrintMargins.Top;
							float single2 = 300f;
							int count = this.PartListTable.Columns.Count;
							pageBounds = e.PageBounds;
							int width32 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							num55 = width32 / count;
							int num56 = 0;
							int num57 = 100;
							foreach (KeyValuePair<string, string> dicPLColSetting in this.dicPLColSettings)
							{
								if (!dicPLColSetting.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num58 = Convert.ToInt32(dicPLColSetting.Value.ToString());
								if (num58 < num57)
								{
									num57 = num58;
								}
								num56 += num58;
							}
							int num59 = 0;
							int num60 = 100;
							foreach (KeyValuePair<string, string> keyValuePair in this.dicPLColSettings)
							{
								if (!keyValuePair.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num61 = Convert.ToInt32(keyValuePair.Value.ToString());
								if (width32 >= num59 + num61)
								{
									num59 += num61;
								}
								else
								{
									num60 = Math.Abs(width32 - num59);
									num59 += num61;
									break;
								}
							}
							if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
							{
								resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDatePL.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
								now = DateTime.Now;
								resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
								resource[7] = Environment.NewLine;
								string str20 = string.Concat(resource);
								System.Drawing.Size size51 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
								System.Drawing.Size size52 = TextRenderer.MeasureText(str20, this.previewFont);
								top29 = top29 + size51.Height + size52.Height;
								single2 = (float)top29;
								RectangleF rectangleF47 = new RectangleF(single, single1, (float)width32, single2);
								e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF47, stringFormat);
								e.Graphics.DrawString(str20, this.previewFont, new SolidBrush(Color.Black), (float)(left18 + (width32 - size52.Width) / 2), (float)(top29 - size52.Height));
							}
							if (this.objCurrentPrintJob.copyRightField != string.Empty)
							{
								size50 = TextRenderer.MeasureText(this.copyRightField, this.previewFont);
							}
							if (!this.bIsOldINIPL)
							{
								this.ResizeCellHeight(true, 0, ref num53, this.PartListTable, e, this.objStringFormat, num57);
							}
							else
							{
								this.ResizeCellHeight(true, 0, ref num53, this.PartListTable, e, this.objStringFormat, num55);
							}
							int num62 = 0;
							int num63 = 0;
							while (num63 < this.PartListTable.Columns.Count)
							{
								StringFormat stringFormat3 = new StringFormat();
								try
								{
									stringFormat3.Alignment = StringAlignment.Center;
									stringFormat3.LineAlignment = StringAlignment.Center;
									if (!this.bIsOldINIPL)
									{
										this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num63].ToString(), "_ALIGN")].ToString();
										stringFormat2.Alignment = StringAlignment.Center;
										stringFormat2.LineAlignment = StringAlignment.Center;
										int num64 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num63].ToString(), "_WIDTH")].ToString());
										num55 = num64;
									}
								}
								catch (Exception exception19)
								{
									stringFormat3.Alignment = StringAlignment.Center;
								}
								if (num62 + num55 > width32)
								{
									num55 = num60;
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left18, top29, num55, num53));
									e.Graphics.DrawRectangle(Pens.Black, left18, top29, num55, num53);
									e.Graphics.DrawString(this.PartListTable.Columns[num63].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left18, (float)top29, (float)num55, (float)num53), stringFormat2);
									left18 += num55;
									break;
								}
								else
								{
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left18, top29, num55, num53));
									e.Graphics.DrawRectangle(Pens.Black, left18, top29, num55, num53);
									e.Graphics.DrawString(this.PartListTable.Columns[num63].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left18, (float)top29, (float)num55, (float)num53), stringFormat3);
									left18 += num55;
									num62 += num55;
									num63++;
								}
							}
							left18 = this.PrintMargins.Left;
							num54 = top29 + num53;
							while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
							{
								if (!this.bIsOldINIPL)
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num53, this.PartListTable, e, this.objStringFormat, num57);
								}
								else
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num53, this.PartListTable, e, this.objStringFormat, num55);
								}
								if (this.objCurrentPrintJob.sOrientation == "0")
								{
									if (num54 + num53 > this.PaperSize.Height - this.PrintMargins.Bottom - size50.Height)
									{
										left18 = this.PrintMargins.Left;
										top29 = this.PrintMargins.Top;
										num54 = top29;
										e.HasMorePages = true;
										this.bHasPages = true;
										this.objCurrentPrintJob.sZoom = "0";
										if (this.objCurrentPrintJob.copyRightField != string.Empty)
										{
											Graphics graphics6 = e.Graphics;
											string str21 = this.objCurrentPrintJob.copyRightField;
											System.Drawing.Font font = this.previewFont;
											SolidBrush solidBrush = new SolidBrush(Color.Black);
											float single3 = (float)(left18 + (width32 - size50.Width) / 2);
											pageBounds = e.PageBounds;
											graphics6.DrawString(str21, font, solidBrush, single3, (float)(pageBounds.Bottom - this.PrintMargins.Right - size50.Height));
										}
										return;
									}
								}
								else if (num54 + num53 > this.PaperSize.Width - this.PrintMargins.Left - size50.Height)
								{
									left18 = this.PrintMargins.Left;
									top29 = this.PrintMargins.Top;
									num54 = top29;
									e.HasMorePages = true;
									this.bHasPages = true;
									this.objCurrentPrintJob.sZoom = "0";
									if (this.objCurrentPrintJob.copyRightField != string.Empty)
									{
										Graphics graphic6 = e.Graphics;
										string str22 = this.copyRightField;
										System.Drawing.Font font1 = this.previewFont;
										SolidBrush solidBrush1 = new SolidBrush(Color.Black);
										float single4 = (float)(left18 + (width32 - size50.Width) / 2);
										pageBounds = e.PageBounds;
										graphic6.DrawString(str22, font1, solidBrush1, single4, (float)(pageBounds.Bottom - this.PrintMargins.Right - size50.Height));
									}
									return;
								}
								int num65 = 0;
								int num66 = 0;
								while (num66 < this.PartListTable.Columns.Count)
								{
									StringFormat stringFormat4 = new StringFormat();
									try
									{
										stringFormat4.Alignment = StringAlignment.Center;
										stringFormat4.LineAlignment = StringAlignment.Center;
										if (!this.bIsOldINIPL)
										{
											string str23 = this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num66].ToString(), "_ALIGN")].ToString();
											stringFormat2.Alignment = StringAlignment.Center;
											stringFormat2.LineAlignment = StringAlignment.Center;
											if (str23 == "L")
											{
												stringFormat4.Alignment = StringAlignment.Near;
												stringFormat2.Alignment = StringAlignment.Near;
											}
											else if (str23 == "R")
											{
												stringFormat4.Alignment = StringAlignment.Far;
												stringFormat2.Alignment = StringAlignment.Far;
											}
											int num67 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num66].ToString(), "_WIDTH")].ToString());
											num55 = num67;
										}
									}
									catch (Exception exception20)
									{
										stringFormat4.Alignment = StringAlignment.Center;
									}
									if (num65 + num55 > width32)
									{
										num55 = num60;
										this.ResizeCellHeight(false, this.iPartsListRowCount, ref num53, this.PartListTable, e, this.objStringFormat, num55);
										e.Graphics.DrawRectangle(Pens.Black, left18, num54, num55, num53);
										e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num66].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left18, (float)num54, (float)num55, (float)num53), stringFormat2);
										left18 += num55;
										break;
									}
									else
									{
										e.Graphics.DrawRectangle(Pens.Black, left18, num54, num55, num53);
										e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num66].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left18, (float)num54, (float)num55, (float)num53), stringFormat4);
										left18 += num55;
										num65 += num55;
										num66++;
									}
								}
								left18 = this.PrintMargins.Left;
								this.iPartsListRowCount++;
								num54 += num53;
							}
							if (this.objCurrentPrintJob.copyRightField != string.Empty)
							{
								Graphics graphics7 = e.Graphics;
								string str24 = this.objCurrentPrintJob.copyRightField;
								System.Drawing.Font font2 = this.previewFont;
								SolidBrush solidBrush2 = new SolidBrush(Color.Black);
								float single5 = (float)(left18 + (width32 - size50.Width) / 2);
								pageBounds = e.PageBounds;
								graphics7.DrawString(str24, font2, solidBrush2, single5, (float)(pageBounds.Bottom - this.PrintMargins.Right - size50.Height));
							}
							e.HasMorePages = false;
							this.PartListTable = null;
						}
						else
						{
							this.bNewPageForListLoaded = true;
							e.HasMorePages = true;
							e.PageSettings.Landscape = (this.objCurrentPrintJob.sOrientation == "1" ? true : false);
							return;
						}
					}
				}
				catch
				{
				}
			}
			if (this.arrPrintJobs.Count <= 0)
			{
				try
				{
					if (this.imagetoPrint != null)
					{
						this.imagetoPrint.Dispose();
						File.Delete(this.sExportedImagePath);
					}
				}
				catch
				{
				}
				return;
			}
			if (this.objCurrentPrintJob.pageSplitCount != 1)
			{
				if (this.objCurrentPrintJob.pageSplitCount == 2 || this.objCurrentPrintJob.pageSplitCount == 8)
				{
					if (this.imagetoPrint.Width >= this.imagetoPrint.Height)
					{
						e.PageSettings.Landscape = false;
					}
					else
					{
						e.PageSettings.Landscape = true;
					}
				}
				if (this.objCurrentPrintJob.pageSplitCount == 4)
				{
					if (this.imagetoPrint.Width >= this.imagetoPrint.Height)
					{
						e.PageSettings.Landscape = true;
					}
					else
					{
						e.PageSettings.Landscape = false;
					}
				}
			}
			try
			{
				if (this.imagetoPrint != null)
				{
					this.imagetoPrint.Dispose();
					File.Delete(this.sExportedImagePath);
				}
			}
			catch
			{
			}
			this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
			if (this.arrPrintJobs.Count == 1 && this.objCurrentPrintJob.sPrintPic == "0" && this.objCurrentPrintJob.sLocalListPath == string.Empty)
			{
				e.HasMorePages = false;
				return;
			}
			e.HasMorePages = true;
			this.bNewPageForListLoaded = false;
			this.bPreviewImageNotExported = true;
			this.ImagePrinted = 0;
		}

		private void doc_PrinthalfPage(object sender, PrintPageEventArgs e)
		{
			int i;
			int j;
			int k;
			int top;
			int num;
			int num1;
			int top1;
			int top2;
			int num2;
			string[] resource;
			DateTime now;
			try
			{
				Rectangle pageBounds = e.PageBounds;
				this.iTotalPageWidth = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
				System.Drawing.Size size = new System.Drawing.Size(0, 0);
				System.Drawing.Size size1 = new System.Drawing.Size(0, 0);
				System.Drawing.Size size2 = new System.Drawing.Size(0, 0);
				System.Drawing.Size picMemoDrawingHeight = new System.Drawing.Size(0, 0);
				StringFormat stringFormat = new StringFormat()
				{
					Alignment = StringAlignment.Center
				};
				string empty = string.Empty;
				string str = string.Empty;
				string empty1 = string.Empty;
				if (this.ImagePrinted == 0)
				{
					int left = this.PrintMargins.Left;
					int height = this.PrintMargins.Top;
					if (this.arrPrintJobs.Count > 0 && this.bPreviewImageNotExported)
					{
						this.bPreviewImageNotExported = false;
						this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
						this.arrPrintJobs.RemoveAt(0);
						this.sExportedImagePath = string.Empty;
						this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
						if (!File.Exists(this.objCurrentPrintJob.sLocalPicPath))
						{
							Download download = new Download();
							download.DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
							this.objDjVuCtl.SRC = this.objCurrentPrintJob.sLocalPicPath;
						}
						else
						{
							this.objDjVuCtl.SRC = this.objCurrentPrintJob.sLocalPicPath;
						}
						if (this.ExportImage(this.objDjVuCtl.SRC, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
						{
							this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
						}
					}
					empty = this.objCurrentPrintJob.strPicMemoValue;
					try
					{
						if (empty.Length > 0)
						{
							str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
							str = str.Substring(0, 1);
							picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, this.iTotalPageWidth);
						}
					}
					catch (Exception exception)
					{
					}
					if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
					{
						string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue);
					}
					else
					{
						resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
						now = DateTime.Now;
						resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
						resource[7] = Environment.NewLine;
						empty1 = string.Concat(resource);
						size = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
						size1 = TextRenderer.MeasureText(empty1, this.previewFont);
					}
					if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
					{
						size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
					}
					if (this.imagetoPrint != null)
					{
						if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height > this.imagetoPrint.Width)
						{
							decimal num3 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
							int width = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							int height1 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
							height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
							RectangleF rectangleF = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)width, (float)picMemoDrawingHeight.Height);
							if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
							{
								height1 /= 2;
							}
							if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)this.PrintMargins.Top);
								e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size1.Width) / 2), (float)(this.PrintMargins.Top + size.Height));
								e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF, stringFormat);
							}
							else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF, stringFormat);
							}
							int num4 = (int)Math.Round(height1 * num3, 4);
							left = left + (width - num4) / 2;
							e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, num4, height1), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
						}
						if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width)
						{
							decimal num5 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
							int width1 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							int height2 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
							height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
							RectangleF rectangleF1 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)width1, (float)picMemoDrawingHeight.Height);
							if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
							{
								height2 /= 2;
							}
							if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width1 - size.Width) / 2), (float)this.PrintMargins.Top);
								e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width1 - size1.Width) / 2), (float)(this.PrintMargins.Top + size.Height));
								e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF1, stringFormat);
							}
							else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF1, stringFormat);
							}
							int num6 = (int)Math.Round(width1 / num5, 4);
							int num7 = width1;
							if (num6 > height2)
							{
								num6 = height2;
								width1 = (int)Math.Round(num6 * num5, 4);
							}
							height = height + (height2 - num6) / 2;
							left = left + (num7 - width1) / 2;
							e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, width1, num6), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
						}
						if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height > this.imagetoPrint.Width)
						{
							if (this.objCurrentPrintJob.sPrintSideBySide != "0")
							{
								decimal num8 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int height3 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
								int height4 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
								if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
								{
									height3 /= 2;
								}
								height3 -= 20;
								int width2 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
								height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
								RectangleF rectangleF2 = new RectangleF((float)left, (float)this.PrintMargins.Top, (float)height4, (float)size.Height);
								RectangleF rectangleF3 = new RectangleF((float)left, (float)(this.PrintMargins.Top + size.Height), (float)height4, (float)size1.Height);
								RectangleF rectangleF4 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)height4, (float)picMemoDrawingHeight.Height);
								for (i = (int)Math.Round(height3 / num8, 4); width2 <= i; i = (int)Math.Round(height3 / num8, 4))
								{
									height3 -= 10;
								}
								if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF2, stringFormat);
									e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), rectangleF3, stringFormat);
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF4, stringFormat);
								}
								else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF4, stringFormat);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
								}
								if (this.objCurrentPrintJob.sLocalListPath == string.Empty || !File.Exists(this.objCurrentPrintJob.sLocalListPath))
								{
									left = (this.PaperSize.Height - height3) / 2;
								}
								height = height + (width2 - i) / 2;
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, height3, i), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
							}
							else
							{
								decimal num9 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int height5 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width3 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
								height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
								RectangleF rectangleF5 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)height5, (float)picMemoDrawingHeight.Height);
								if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
								{
									width3 /= 2;
								}
								if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height5 - size.Width) / 2), (float)this.PrintMargins.Top);
									e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height5 - size1.Width) / 2), (float)(this.PrintMargins.Top + size.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF5, stringFormat);
								}
								else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF5, stringFormat);
								}
								int num10 = (int)Math.Round(width3 * num9, 4);
								left = left + (height5 - num10) / 2;
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, num10, width3), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
							}
						}
						if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height < this.imagetoPrint.Width)
						{
							if (this.objCurrentPrintJob.sPrintSideBySide != "0")
							{
								decimal num11 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int height6 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
								int height7 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
								if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
								{
									height6 /= 2;
									height6 -= 20;
								}
								int height8 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
								height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
								RectangleF rectangleF6 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)height7, (float)picMemoDrawingHeight.Height);
								for (j = (int)Math.Round(height6 / num11, 4); height8 <= j; j = (int)Math.Round(height6 / num11, 4))
								{
									height6 -= 10;
								}
								if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height7 - size.Width) / 2), (float)this.PrintMargins.Top);
									e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height7 - size1.Width) / 2), (float)(this.PrintMargins.Top + size.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF6, stringFormat);
								}
								else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF6, stringFormat);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
								}
								height = (height + (height8 - j) / 2) / 2;
								if (this.objCurrentPrintJob.sLocalListPath != string.Empty && this.objCurrentPrintJob.sLocalListPath == string.Empty)
								{
									left = this.PaperSize.Height - height6;
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, height6, j), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
							}
							else
							{
								decimal num12 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int height9 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width4 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
								height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
								RectangleF rectangleF7 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)height9, (float)picMemoDrawingHeight.Height);
								if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
								{
									width4 /= 2;
								}
								if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height9 - size.Width) / 2), (float)this.PrintMargins.Top);
									e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height9 - size1.Width) / 2), (float)(this.PrintMargins.Top + size.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF7, stringFormat);
								}
								else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF7, stringFormat);
								}
								int num13 = (int)Math.Round(width4 * num12, 4);
								left = left + (height9 - num13) / 2;
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, num13, width4), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
							}
						}
						else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height == this.imagetoPrint.Width)
						{
							if (this.objCurrentPrintJob.sPrintSideBySide != "0")
							{
								decimal num14 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int height10 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
								int height11 = this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left;
								if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
								{
									height10 /= 2;
								}
								height10 -= 20;
								int width5 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
								height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
								RectangleF rectangleF8 = new RectangleF((float)left, (float)this.PrintMargins.Top, (float)height11, (float)size.Height);
								RectangleF rectangleF9 = new RectangleF((float)left, (float)(this.PrintMargins.Top + size.Height), (float)height11, (float)size1.Height);
								RectangleF rectangleF10 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)height11, (float)picMemoDrawingHeight.Height);
								for (k = (int)Math.Round(height10 / num14, 4); width5 <= k; k = (int)Math.Round(height10 / num14, 4))
								{
									height10 -= 10;
								}
								if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF8, stringFormat);
									e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), rectangleF9, stringFormat);
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF10, stringFormat);
								}
								else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF10, stringFormat);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
								}
								if (this.objCurrentPrintJob.sLocalListPath == string.Empty || !File.Exists(this.objCurrentPrintJob.sLocalListPath))
								{
									left = (this.PaperSize.Height - height10) / 2;
								}
								height = height + (width5 - k) / 2;
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, height10, k), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
							}
							else
							{
								decimal num15 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int height12 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width6 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
								height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
								RectangleF rectangleF11 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)height12, (float)picMemoDrawingHeight.Height);
								if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
								{
									width6 /= 2;
								}
								if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height12 - size.Width) / 2), (float)this.PrintMargins.Top);
									e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height12 - size1.Width) / 2), (float)(this.PrintMargins.Top + size.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF11, stringFormat);
								}
								else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF11, stringFormat);
								}
								int num16 = (int)Math.Round(width6 * num15, 4);
								left = left + (height12 - num16) / 2;
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, num16, width6), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
							}
						}
						else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height == this.imagetoPrint.Width)
						{
							decimal num17 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
							int width7 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							int height13 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height;
							height = height + size.Height + size1.Height + picMemoDrawingHeight.Height;
							RectangleF rectangleF12 = new RectangleF((float)left, (float)(height - picMemoDrawingHeight.Height), (float)width7, (float)picMemoDrawingHeight.Height);
							if (this.objCurrentPrintJob.sLocalListPath != string.Empty)
							{
								height13 /= 2;
							}
							if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)this.PrintMargins.Top);
								e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size1.Width) / 2), (float)(this.PrintMargins.Top + size.Height));
								e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF12, stringFormat);
							}
							else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF12, stringFormat);
							}
							int num18 = (int)Math.Round(height13 * num17, 4);
							left = left + (width7 - num18) / 2;
							e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, height, num18, height13), 0, 0, this.imagetoPrint.Width, this.imagetoPrint.Height, GraphicsUnit.Pixel);
						}
						this.ImagePrinted++;
						try
						{
							this.imagetoPrint.Dispose();
							File.Delete(this.sExportedImagePath);
						}
						catch
						{
						}
					}
				}
				if (this.objCurrentPrintJob.sLocalListPath == string.Empty)
				{
					int width8 = 0;
					if (this.objCurrentPrintJob.sOrientation != "0")
					{
						width8 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
						e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width8 - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
					}
					else
					{
						width8 = this.PaperSize.Width - this.PrintMargins.Right - this.PrintMargins.Left;
						e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width8 - size2.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
					}
				}
				else
				{
					StringFormat stringFormat1 = new StringFormat()
					{
						FormatFlags = StringFormatFlags.NoWrap,
						Trimming = StringTrimming.Character
					};
					StringFormat stringFormat2 = stringFormat1;
					if (this.PartListTable == null)
					{
						this.iPartsListRowCount = 0;
						if (!File.Exists(this.objCurrentPrintJob.sLocalListPath))
						{
							Download download1 = new Download();
							download1.DownloadFile(this.objCurrentPrintJob.sServerListPath, this.objCurrentPrintJob.sLocalListPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
							int num19 = -11;
							int num20 = 2;
							int num21 = 500;
							for (int l = 0; l < num20; l++)
							{
								try
								{
									if (!File.Exists(this.objCurrentPrintJob.sLocalListPath))
									{
										break;
									}
									else
									{
										IntPtr intPtr = PreviewManager.UnZipFile(this.objCurrentPrintJob.sLocalListPath);
										num19 = intPtr.ToInt32();
										if (num19 != 1 || !File.Exists(this.objCurrentPrintJob.sLocalListPath.ToLower().Replace(".zip", ".xml")))
										{
											Thread.Sleep(num21);
										}
										else
										{
											break;
										}
									}
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
						resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
						now = DateTime.Now;
						resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
						resource[7] = Environment.NewLine;
						empty1 = string.Concat(resource);
						size = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
						size1 = TextRenderer.MeasureText(empty1, this.previewFont);
					}
					if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
					{
						size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
					}
					if (this.objCurrentPrintJob.sPrintSideBySide == "1" && this.objCurrentPrintJob.sOrientation == "1" && this.PageCounter == 1)
					{
						if (this.PartListTable.Rows.Count > 0)
						{
							int num22 = 0;
							int count = 0;
							int height14 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
							int num23 = 23;
							if (this.PageCounter <= 1)
							{
								int width9 = this.PaperSize.Width;
								int left1 = this.PrintMargins.Left;
								int right = this.PrintMargins.Right;
								int height15 = size.Height;
								int height16 = size1.Height;
								int height17 = size2.Height;
								int height18 = picMemoDrawingHeight.Height;
								top = this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
							}
							else
							{
								top = this.PrintMargins.Top + size.Height + size1.Height + 20;
							}
							int height19 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 - 20;
							StringFormat stringFormat3 = new StringFormat()
							{
								Alignment = StringAlignment.Center,
								LineAlignment = StringAlignment.Center,
								Trimming = StringTrimming.EllipsisCharacter
							};
							count = height19 / this.PartListTable.Columns.Count;
							int num24 = 0;
							int num25 = 100;
							foreach (KeyValuePair<string, string> dicPLColSetting in this.dicPLColSettings)
							{
								if (!dicPLColSetting.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num26 = Convert.ToInt32(dicPLColSetting.Value.ToString());
								if (num26 < num25)
								{
									num25 = num26;
								}
								num24 += num26;
							}
							int num27 = 0;
							int num28 = 0;
							foreach (KeyValuePair<string, string> keyValuePair in this.dicPLColSettings)
							{
								if (!keyValuePair.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num29 = Convert.ToInt32(keyValuePair.Value.ToString());
								if (height19 >= num27 + num29)
								{
									num27 += num29;
								}
								else
								{
									num28 = Math.Abs(height19 - num27);
									num27 += num29;
									break;
								}
							}
							if (!this.bIsOldINIPL)
							{
								this.ResizeCellHeight(true, 0, ref num23, this.PartListTable, e, this.objStringFormat, num25);
							}
							else
							{
								this.ResizeCellHeight(true, 0, ref num23, this.PartListTable, e, this.objStringFormat, count);
							}
							int num30 = 0;
							int num31 = 0;
							while (num31 < this.PartListTable.Columns.Count)
							{
								StringFormat stringFormat4 = new StringFormat();
								try
								{
									stringFormat4.Alignment = StringAlignment.Center;
									stringFormat4.LineAlignment = StringAlignment.Center;
									if (!this.bIsOldINIPL)
									{
										this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num31].ToString(), "_ALIGN")].ToString();
										stringFormat2.Alignment = StringAlignment.Center;
										stringFormat2.LineAlignment = StringAlignment.Center;
										int num32 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num31].ToString(), "_WIDTH")].ToString());
										count = num32;
									}
								}
								catch (Exception exception1)
								{
								}
								if (num30 + count > height19)
								{
									count = num28;
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(height14, top, count, num23));
									e.Graphics.DrawRectangle(Pens.Black, height14, top, count, num23);
									e.Graphics.DrawString(this.PartListTable.Columns[num31].Caption, this.previewFont, Brushes.Black, new RectangleF((float)height14, (float)top, (float)count, (float)num23), stringFormat2);
									height14 += count;
									break;
								}
								else
								{
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(height14, top, count, num23));
									e.Graphics.DrawRectangle(Pens.Black, height14, top, count, num23);
									e.Graphics.DrawString(this.PartListTable.Columns[num31].Caption, this.previewFont, Brushes.Black, new RectangleF((float)height14, (float)top, (float)count, (float)num23), stringFormat4);
									height14 += count;
									num30 += count;
									num31++;
								}
							}
							height14 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
							if (this.PageCounter <= 1)
							{
								int width10 = this.PaperSize.Width;
								int left2 = this.PrintMargins.Left;
								int right1 = this.PrintMargins.Right;
								int height20 = size.Height;
								int height21 = size1.Height;
								int height22 = size2.Height;
								int height23 = picMemoDrawingHeight.Height;
								top = this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
							}
							else
							{
								top = this.PrintMargins.Top + size.Height + size1.Height + 20;
							}
							num22 = top + num23;
							while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
							{
								if (!this.bIsOldINIPL)
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num23, this.PartListTable, e, this.objStringFormat, num25);
								}
								else
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num23, this.PartListTable, e, this.objStringFormat, count);
								}
								if (num22 + num23 <= this.PaperSize.Width - this.PrintMargins.Left - size2.Height)
								{
									num30 = 0;
									int num33 = 0;
									while (num33 < this.PartListTable.Columns.Count)
									{
										StringFormat stringFormat5 = new StringFormat();
										try
										{
											stringFormat5.Alignment = StringAlignment.Center;
											stringFormat5.LineAlignment = StringAlignment.Center;
											if (!this.bIsOldINIPL)
											{
												string str1 = this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num33].ToString(), "_ALIGN")].ToString();
												stringFormat2.Alignment = StringAlignment.Center;
												stringFormat2.LineAlignment = StringAlignment.Center;
												if (str1 == "L")
												{
													stringFormat5.Alignment = StringAlignment.Near;
													stringFormat2.Alignment = StringAlignment.Near;
												}
												else if (str1 == "R")
												{
													stringFormat5.Alignment = StringAlignment.Far;
													stringFormat2.Alignment = StringAlignment.Far;
												}
												int num34 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num33].ToString(), "_WIDTH")].ToString());
												count = num34;
											}
										}
										catch (Exception exception2)
										{
										}
										if (num30 + count > height19)
										{
											count = num28;
											e.Graphics.DrawRectangle(Pens.Black, height14, num22, count, num23);
											e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num33].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)height14, (float)num22, (float)count, (float)num23), stringFormat2);
											height14 += count;
											break;
										}
										else
										{
											e.Graphics.DrawRectangle(Pens.Black, height14, num22, count, num23);
											e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num33].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)height14, (float)num22, (float)count, (float)num23), stringFormat5);
											height14 += count;
											num30 += count;
											num33++;
										}
									}
									height14 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
									this.iPartsListRowCount++;
									num22 += num23;
								}
								else
								{
									height14 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
									top = this.PrintMargins.Top + size.Height + size1.Height + 20;
									num22 = top;
									e.HasMorePages = true;
									this.PageCounter++;
									return;
								}
							}
							e.HasMorePages = false;
							this.PageCounter = 1;
						}
					}
					else if (this.objCurrentPrintJob.sOrientation != "0" && this.PageCounter == 1)
					{
						if ((this.objCurrentPrintJob.sPrintSideBySide == "0" || this.objCurrentPrintJob.sOrientation == "0") && this.PartListTable.Rows.Count > 0)
						{
							int num35 = 0;
							int count1 = 0;
							int left3 = this.PrintMargins.Left;
							int num36 = 23;
							if (this.PageCounter <= 1)
							{
								num2 = (this.objCurrentPrintJob.sOrientation != "0" ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height);
								top2 = num2 / 2 + this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
							}
							else
							{
								top2 = this.PrintMargins.Top + size.Height + size1.Height;
							}
							pageBounds = e.PageBounds;
							int width11 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							if (this.PageCounter > 1 && this.objCurrentPrintJob.sPrintHeaderFooter == "1")
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left3 + (width11 - size.Width) / 2), (float)(top2 - size.Height - size1.Height));
								e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)(left3 + (width11 - size1.Width) / 2), (float)(top2 - size1.Height));
							}
							StringFormat stringFormat6 = new StringFormat()
							{
								Alignment = StringAlignment.Center,
								LineAlignment = StringAlignment.Center,
								Trimming = StringTrimming.EllipsisCharacter
							};
							count1 = width11 / this.PartListTable.Columns.Count;
							int num37 = 0;
							int num38 = 100;
							foreach (KeyValuePair<string, string> dicPLColSetting1 in this.dicPLColSettings)
							{
								if (!dicPLColSetting1.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num39 = Convert.ToInt32(dicPLColSetting1.Value.ToString());
								if (num39 < num38)
								{
									num38 = num39;
								}
								num37 += num39;
							}
							int num40 = 0;
							int num41 = 0;
							foreach (KeyValuePair<string, string> keyValuePair1 in this.dicPLColSettings)
							{
								if (!keyValuePair1.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num42 = Convert.ToInt32(keyValuePair1.Value.ToString());
								if (width11 >= num40 + num42)
								{
									num40 += num42;
								}
								else
								{
									num41 = Math.Abs(width11 - num40);
									num40 += num42;
									break;
								}
							}
							if (!this.bIsOldINIPL)
							{
								this.ResizeCellHeight(true, 0, ref num36, this.PartListTable, e, this.objStringFormat, num38);
							}
							else
							{
								this.ResizeCellHeight(true, 0, ref num36, this.PartListTable, e, this.objStringFormat, count1);
							}
							int num43 = 0;
							int num44 = 0;
							while (num44 < this.PartListTable.Columns.Count)
							{
								StringFormat stringFormat7 = new StringFormat();
								try
								{
									stringFormat7.Alignment = StringAlignment.Center;
									stringFormat7.LineAlignment = StringAlignment.Center;
									if (!this.bIsOldINIPL)
									{
										this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num44].ToString(), "_ALIGN")].ToString();
										stringFormat2.Alignment = StringAlignment.Center;
										stringFormat2.LineAlignment = StringAlignment.Center;
										int num45 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num44].ToString(), "_WIDTH")].ToString());
										count1 = num45;
									}
								}
								catch (Exception exception3)
								{
								}
								if (num43 + count1 > width11)
								{
									count1 = num41;
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left3, top2, count1, num36));
									e.Graphics.DrawRectangle(Pens.Black, left3, top2, count1, num36);
									e.Graphics.DrawString(this.PartListTable.Columns[num44].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left3, (float)top2, (float)count1, (float)num36), stringFormat2);
									left3 += count1;
									break;
								}
								else
								{
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left3, top2, count1, num36));
									e.Graphics.DrawRectangle(Pens.Black, left3, top2, count1, num36);
									e.Graphics.DrawString(this.PartListTable.Columns[num44].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left3, (float)top2, (float)count1, (float)num36), stringFormat7);
									left3 += count1;
									num43 += count1;
									num44++;
								}
							}
							left3 = this.PrintMargins.Left;
							if (this.PageCounter <= 1)
							{
								num2 = (this.objCurrentPrintJob.sOrientation != "0" ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height);
								top2 = num2 / 2 + this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
							}
							else
							{
								top2 = this.PrintMargins.Top + size.Height + size1.Height;
							}
							num35 = top2 + num36;
							while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
							{
								if (!this.bIsOldINIPL)
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num36, this.PartListTable, e, this.objStringFormat, num38);
								}
								else
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num36, this.PartListTable, e, this.objStringFormat, count1);
								}
								if (this.objCurrentPrintJob.sOrientation == "0")
								{
									if (num35 + num36 > this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height)
									{
										left3 = this.PrintMargins.Left;
										top2 = this.PrintMargins.Top + size.Height + size1.Height;
										num35 = top2;
										e.HasMorePages = true;
										this.PageCounter++;
										if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
										{
											e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width11 - size2.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
										}
										return;
									}
								}
								else if (num35 + num36 > this.PaperSize.Width - this.PrintMargins.Left - size2.Height)
								{
									left3 = this.PrintMargins.Left;
									top2 = this.PrintMargins.Top;
									num35 = top2;
									e.HasMorePages = true;
									this.PageCounter++;
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width11 - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
									}
									return;
								}
								num43 = 0;
								int num46 = 0;
								while (num46 < this.PartListTable.Columns.Count)
								{
									StringFormat stringFormat8 = new StringFormat();
									try
									{
										stringFormat8.Alignment = StringAlignment.Center;
										stringFormat8.LineAlignment = StringAlignment.Center;
										if (!this.bIsOldINIPL)
										{
											string str2 = this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num46].ToString(), "_ALIGN")].ToString();
											stringFormat2.Alignment = StringAlignment.Center;
											stringFormat2.LineAlignment = StringAlignment.Center;
											if (str2 == "L")
											{
												stringFormat8.Alignment = StringAlignment.Near;
												stringFormat2.Alignment = StringAlignment.Near;
											}
											else if (str2 == "R")
											{
												stringFormat8.Alignment = StringAlignment.Far;
												stringFormat2.Alignment = StringAlignment.Far;
											}
											int num47 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num46].ToString(), "_WIDTH")].ToString());
											count1 = num47;
										}
									}
									catch (Exception exception4)
									{
									}
									if (num43 + count1 > width11)
									{
										count1 = num41;
										e.Graphics.DrawRectangle(Pens.Black, left3, num35, count1, num36);
										e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num46].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left3, (float)num35, (float)count1, (float)num36), stringFormat2);
										left3 += count1;
										break;
									}
									else
									{
										e.Graphics.DrawRectangle(Pens.Black, left3, num35, count1, num36);
										e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num46].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left3, (float)num35, (float)count1, (float)num36), stringFormat8);
										left3 += count1;
										num43 += count1;
										num46++;
									}
								}
								left3 = this.PrintMargins.Left;
								this.iPartsListRowCount++;
								num35 += num36;
							}
							if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "0")
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width11 - size2.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
							}
							else if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "1")
							{
								e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width11 - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
							}
							e.HasMorePages = false;
							this.ImagePrinted = 0;
							this.PageCounter = 1;
						}
					}
					else if (this.objCurrentPrintJob.sPrintSideBySide != "0" && this.objCurrentPrintJob.sOrientation != "0")
					{
						int top3 = this.PrintMargins.Top;
						top3 = top3 + size.Height + size1.Height;
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(top3 - size.Height - size1.Height));
							e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size1.Width) / 2), (float)(top3 - size1.Height));
						}
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
						}
						if (this.PartListTable.Rows.Count > 0)
						{
							int num48 = 0;
							int count2 = 0;
							int left4 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
							left4 = this.PrintMargins.Left;
							int num49 = 23;
							bool flag = false;
							if (this.PageCounter <= 1)
							{
								int width12 = this.PaperSize.Width;
								int left5 = this.PrintMargins.Left;
								int right2 = this.PrintMargins.Right;
								int height24 = size.Height;
								int height25 = size1.Height;
								int height26 = size2.Height;
								int height27 = picMemoDrawingHeight.Height;
								top1 = this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
							}
							else
							{
								top1 = this.PrintMargins.Top + size.Height + size1.Height + 20;
							}
							int height28 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 - 20;
							StringFormat stringFormat9 = new StringFormat()
							{
								Alignment = StringAlignment.Center,
								LineAlignment = StringAlignment.Center,
								Trimming = StringTrimming.EllipsisCharacter
							};
							count2 = height28 / this.PartListTable.Columns.Count;
							int num50 = 0;
							int num51 = 100;
							foreach (KeyValuePair<string, string> dicPLColSetting2 in this.dicPLColSettings)
							{
								if (!dicPLColSetting2.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num52 = Convert.ToInt32(dicPLColSetting2.Value.ToString());
								if (num52 < num51)
								{
									num51 = num52;
								}
								num50 += num52;
							}
							int num53 = 0;
							int num54 = 0;
							foreach (KeyValuePair<string, string> keyValuePair2 in this.dicPLColSettings)
							{
								if (!keyValuePair2.Key.ToString().EndsWith("_WIDTH"))
								{
									continue;
								}
								int num55 = Convert.ToInt32(keyValuePair2.Value.ToString());
								if (height28 >= num53 + num55)
								{
									num53 += num55;
								}
								else
								{
									num54 = Math.Abs(height28 - num53);
									num53 += num55;
									break;
								}
							}
							if (!this.bIsOldINIPL)
							{
								this.ResizeCellHeight(true, 0, ref num49, this.PartListTable, e, this.objStringFormat, num51);
							}
							else
							{
								this.ResizeCellHeight(true, 0, ref num49, this.PartListTable, e, this.objStringFormat, count2);
							}
							int num56 = 0;
							int num57 = 0;
							while (num57 < this.PartListTable.Columns.Count)
							{
								StringFormat stringFormat10 = new StringFormat();
								try
								{
									stringFormat10.Alignment = StringAlignment.Center;
									stringFormat10.LineAlignment = StringAlignment.Center;
									if (!this.bIsOldINIPL)
									{
										this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num57].ToString(), "_ALIGN")].ToString();
										stringFormat2.Alignment = StringAlignment.Center;
										stringFormat2.LineAlignment = StringAlignment.Center;
										int num58 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num57].ToString(), "_WIDTH")].ToString());
										count2 = num58;
									}
								}
								catch (Exception exception5)
								{
								}
								if (num56 + count2 > height28)
								{
									count2 = num54;
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left4, top1, count2, num49));
									e.Graphics.DrawRectangle(Pens.Black, left4, top1, count2, num49);
									e.Graphics.DrawString(this.PartListTable.Columns[num57].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left4, (float)top1, (float)count2, (float)num49), stringFormat2);
									left4 += count2;
									break;
								}
								else
								{
									e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left4, top1, count2, num49));
									e.Graphics.DrawRectangle(Pens.Black, left4, top1, count2, num49);
									e.Graphics.DrawString(this.PartListTable.Columns[num57].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left4, (float)top1, (float)count2, (float)num49), stringFormat10);
									left4 += count2;
									num56 += count2;
									num57++;
								}
							}
							left4 = (!flag ? this.PrintMargins.Left : (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left);
							if (this.PageCounter <= 1)
							{
								int width13 = this.PaperSize.Width;
								int left6 = this.PrintMargins.Left;
								int right3 = this.PrintMargins.Right;
								int height29 = size.Height;
								int height30 = size1.Height;
								int height31 = size2.Height;
								int height32 = picMemoDrawingHeight.Height;
								top1 = this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
							}
							else
							{
								top1 = this.PrintMargins.Top + size.Height + size1.Height + 20;
							}
							num48 = top1 + num49;
							while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
							{
								if (!this.bIsOldINIPL)
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num49, this.PartListTable, e, this.objStringFormat, num51);
								}
								else
								{
									this.ResizeCellHeight(false, this.iPartsListRowCount, ref num49, this.PartListTable, e, this.objStringFormat, count2);
								}
								if (num48 + num49 > this.PaperSize.Width - this.PrintMargins.Left - size2.Height)
								{
									top1 = this.PrintMargins.Top + size.Height + size1.Height + 20;
									num48 = top1;
									if (!flag)
									{
										flag = true;
										left4 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
										num49 = 23;
										if (!this.bIsOldINIPL)
										{
											this.ResizeCellHeight(true, 0, ref num49, this.PartListTable, e, this.objStringFormat, num51);
										}
										else
										{
											this.ResizeCellHeight(true, 0, ref num49, this.PartListTable, e, this.objStringFormat, count2);
										}
										num56 = 0;
										int num59 = 0;
										while (num59 < this.PartListTable.Columns.Count)
										{
											StringFormat stringFormat11 = new StringFormat();
											try
											{
												stringFormat11.Alignment = StringAlignment.Center;
												stringFormat11.LineAlignment = StringAlignment.Center;
												if (!this.bIsOldINIPL)
												{
													this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num59].ToString(), "_ALIGN")].ToString();
													stringFormat2.Alignment = StringAlignment.Center;
													stringFormat2.LineAlignment = StringAlignment.Center;
													int num60 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num59].ToString(), "_WIDTH")].ToString());
													count2 = num60;
												}
											}
											catch (Exception exception6)
											{
											}
											if (num56 + count2 > height28)
											{
												count2 = num54;
												e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left4, top1, count2, num49));
												e.Graphics.DrawRectangle(Pens.Black, left4, top1, count2, num49);
												e.Graphics.DrawString(this.PartListTable.Columns[num59].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left4, (float)top1, (float)count2, (float)num49), stringFormat2);
												left4 += count2;
												break;
											}
											else
											{
												e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left4, top1, count2, num49));
												e.Graphics.DrawRectangle(Pens.Black, left4, top1, count2, num49);
												e.Graphics.DrawString(this.PartListTable.Columns[num59].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left4, (float)top1, (float)count2, (float)num49), stringFormat11);
												left4 += count2;
												num56 += count2;
												num59++;
											}
										}
										left4 = (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left;
										num48 += num49;
									}
									else
									{
										e.HasMorePages = true;
										this.PageCounter++;
										return;
									}
								}
								num56 = 0;
								int num61 = 0;
								while (num61 < this.PartListTable.Columns.Count)
								{
									StringFormat stringFormat12 = new StringFormat();
									try
									{
										stringFormat12.Alignment = StringAlignment.Center;
										stringFormat12.LineAlignment = StringAlignment.Center;
										if (!this.bIsOldINIPL)
										{
											string str3 = this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num61].ToString(), "_ALIGN")].ToString();
											stringFormat2.Alignment = StringAlignment.Center;
											stringFormat2.LineAlignment = StringAlignment.Center;
											if (str3 == "L")
											{
												stringFormat12.Alignment = StringAlignment.Near;
												stringFormat2.Alignment = StringAlignment.Near;
											}
											else if (str3 == "R")
											{
												stringFormat12.Alignment = StringAlignment.Far;
												stringFormat2.Alignment = StringAlignment.Far;
											}
											int num62 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num61].ToString(), "_WIDTH")].ToString());
											count2 = num62;
										}
									}
									catch (Exception exception7)
									{
									}
									if (num56 + count2 > height28)
									{
										count2 = num54;
										e.Graphics.DrawRectangle(Pens.Black, left4, num48, count2, num49);
										e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num61].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left4, (float)num48, (float)count2, (float)num49), stringFormat2);
										left4 += count2;
										break;
									}
									else
									{
										e.Graphics.DrawRectangle(Pens.Black, left4, num48, count2, num49);
										e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num61].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left4, (float)num48, (float)count2, (float)num49), stringFormat12);
										left4 += count2;
										num56 += count2;
										num61++;
									}
								}
								left4 = (!flag ? this.PrintMargins.Left : (this.PaperSize.Height - this.PrintMargins.Right - this.PrintMargins.Left) / 2 + this.PrintMargins.Left);
								this.iPartsListRowCount++;
								num48 += num49;
							}
							e.HasMorePages = false;
							this.PageCounter = 1;
						}
					}
					else if (this.PartListTable.Rows.Count > 0)
					{
						int num63 = 0;
						int count3 = 0;
						int left7 = this.PrintMargins.Left;
						int num64 = 23;
						if (this.PageCounter <= 1)
						{
							num1 = (this.objCurrentPrintJob.sOrientation != "0" ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height);
							num = num1 / 2 + this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
						}
						else
						{
							num = this.PrintMargins.Top + size.Height + size1.Height;
						}
						pageBounds = e.PageBounds;
						int width14 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
						if (this.PageCounter > 1 && this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), (float)(left7 + (width14 - size.Width) / 2), (float)(num - size.Height - size1.Height));
							e.Graphics.DrawString(empty1, this.previewFont, new SolidBrush(Color.Black), (float)(left7 + (width14 - size1.Width) / 2), (float)(num - size1.Height));
						}
						StringFormat stringFormat13 = new StringFormat()
						{
							Alignment = StringAlignment.Center,
							LineAlignment = StringAlignment.Center,
							Trimming = StringTrimming.EllipsisCharacter
						};
						count3 = width14 / this.PartListTable.Columns.Count;
						int num65 = 0;
						int num66 = 100;
						foreach (KeyValuePair<string, string> dicPLColSetting3 in this.dicPLColSettings)
						{
							if (!dicPLColSetting3.Key.ToString().EndsWith("_WIDTH"))
							{
								continue;
							}
							int num67 = Convert.ToInt32(dicPLColSetting3.Value.ToString());
							if (num67 < num66)
							{
								num66 = num67;
							}
							num65 += num67;
						}
						int num68 = 0;
						int num69 = 0;
						foreach (KeyValuePair<string, string> keyValuePair3 in this.dicPLColSettings)
						{
							if (!keyValuePair3.Key.ToString().EndsWith("_WIDTH"))
							{
								continue;
							}
							int num70 = Convert.ToInt32(keyValuePair3.Value.ToString());
							if (width14 >= num68 + num70)
							{
								num68 += num70;
							}
							else
							{
								num69 = Math.Abs(width14 - num68);
								num68 += num70;
								break;
							}
						}
						if (!this.bIsOldINIPL)
						{
							this.ResizeCellHeight(true, 0, ref num64, this.PartListTable, e, this.objStringFormat, num66);
						}
						else
						{
							this.ResizeCellHeight(true, 0, ref num64, this.PartListTable, e, this.objStringFormat, count3);
						}
						int num71 = 0;
						int num72 = 0;
						while (num72 < this.PartListTable.Columns.Count)
						{
							StringFormat stringFormat14 = new StringFormat();
							try
							{
								stringFormat14.Alignment = StringAlignment.Center;
								stringFormat14.LineAlignment = StringAlignment.Center;
								if (!this.bIsOldINIPL)
								{
									this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num72].ToString(), "_ALIGN")].ToString();
									stringFormat2.Alignment = StringAlignment.Center;
									stringFormat2.LineAlignment = StringAlignment.Center;
									int num73 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num72].ToString(), "_WIDTH")].ToString());
									count3 = num73;
								}
							}
							catch (Exception exception8)
							{
							}
							if (num71 + count3 > width14)
							{
								count3 = num69;
								e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left7, num, count3, num64));
								e.Graphics.DrawRectangle(Pens.Black, left7, num, count3, num64);
								e.Graphics.DrawString(this.PartListTable.Columns[num72].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left7, (float)num, (float)count3, (float)num64), stringFormat2);
								left7 += count3;
								break;
							}
							else
							{
								e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left7, num, count3, num64));
								e.Graphics.DrawRectangle(Pens.Black, left7, num, count3, num64);
								e.Graphics.DrawString(this.PartListTable.Columns[num72].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left7, (float)num, (float)count3, (float)num64), stringFormat14);
								left7 += count3;
								num71 += count3;
								num72++;
							}
						}
						left7 = this.PrintMargins.Left;
						if (this.PageCounter <= 1)
						{
							num1 = (this.objCurrentPrintJob.sOrientation != "0" ? this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height : this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom - size.Height - size1.Height - size2.Height - picMemoDrawingHeight.Height);
							num = num1 / 2 + this.PrintMargins.Top + size.Height + size1.Height + 20 + picMemoDrawingHeight.Height;
						}
						else
						{
							num = this.PrintMargins.Top + size.Height + size1.Height;
						}
						num63 = num + num64;
						while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
						{
							if (!this.bIsOldINIPL)
							{
								this.ResizeCellHeight(false, this.iPartsListRowCount, ref num64, this.PartListTable, e, this.objStringFormat, num66);
							}
							else
							{
								this.ResizeCellHeight(false, this.iPartsListRowCount, ref num64, this.PartListTable, e, this.objStringFormat, count3);
							}
							if (this.objCurrentPrintJob.sOrientation == "0")
							{
								if (num63 + num64 > this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height)
								{
									left7 = this.PrintMargins.Left;
									num = this.PrintMargins.Top + size.Height + size1.Height;
									num63 = num;
									e.HasMorePages = true;
									this.PageCounter++;
									if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
									{
										e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width14 - size2.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
									}
									return;
								}
							}
							else if (num63 + num64 > this.PaperSize.Width - this.PrintMargins.Left - size2.Height)
							{
								left7 = this.PrintMargins.Left;
								num = this.PrintMargins.Top;
								num63 = num;
								e.HasMorePages = true;
								this.PageCounter++;
								if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width14 - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
								}
								return;
							}
							num71 = 0;
							int num74 = 0;
							while (num74 < this.PartListTable.Columns.Count)
							{
								StringFormat stringFormat15 = new StringFormat();
								try
								{
									stringFormat15.Alignment = StringAlignment.Center;
									stringFormat15.LineAlignment = StringAlignment.Center;
									if (!this.bIsOldINIPL)
									{
										string str4 = this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num74].ToString(), "_ALIGN")].ToString();
										stringFormat2.Alignment = StringAlignment.Center;
										stringFormat2.LineAlignment = StringAlignment.Center;
										if (str4 == "L")
										{
											stringFormat15.Alignment = StringAlignment.Near;
											stringFormat2.Alignment = StringAlignment.Near;
										}
										else if (str4 == "R")
										{
											stringFormat15.Alignment = StringAlignment.Far;
											stringFormat2.Alignment = StringAlignment.Far;
										}
										int num75 = Convert.ToInt32(this.dicPLColSettings[string.Concat(this.PartListTable.Columns[num74].ToString(), "_WIDTH")].ToString());
										count3 = num75;
									}
								}
								catch (Exception exception9)
								{
								}
								if (num71 + count3 > width14)
								{
									count3 = num69;
									e.Graphics.DrawRectangle(Pens.Black, left7, num63, count3, num64);
									e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num74].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left7, (float)num63, (float)count3, (float)num64), stringFormat2);
									left7 += count3;
									break;
								}
								else
								{
									e.Graphics.DrawRectangle(Pens.Black, left7, num63, count3, num64);
									e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num74].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left7, (float)num63, (float)count3, (float)num64), stringFormat15);
									left7 += count3;
									num71 += count3;
									num74++;
								}
							}
							left7 = this.PrintMargins.Left;
							this.iPartsListRowCount++;
							num63 += num64;
						}
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "0")
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width14 - size2.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size2.Height));
						}
						else if (this.objCurrentPrintJob.sPrintHeaderFooter == "1" && this.objCurrentPrintJob.sOrientation == "1")
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(this.PrintMargins.Left + (width14 - size2.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size2.Height));
						}
						e.HasMorePages = false;
						this.ImagePrinted = 0;
						this.PageCounter = 1;
					}
				}
				goto Label0;
			}
			catch
			{
				goto Label0;
			}
			return;
		Label0:
			if (this.arrPrintJobs.Count > 0)
			{
				e.HasMorePages = true;
				this.bNewPageForListLoaded = false;
				this.bPreviewImageNotExported = true;
				this.ImagePrinted = 0;
				this.PartListTable = null;
				return;
			}
			else
			{
				return;
			}
		}

		private void doc_PrintImage(object sender, PrintPageEventArgs e)
		{
			string[] resource;
			DateTime now;
			Rectangle pageBounds;
			object[] objArray;
			try
			{
				if (!this.bHasPages)
				{
					this.objCurrentPrintJob = this.lstPrintJob[this.iPrintJobCounter];
					if (this.lstPrintJob[this.iPrintJobCounter].sLocalPicPath != string.Empty && this.ExportImage(this.lstPrintJob[this.iPrintJobCounter].sLocalPicPath, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
					{
						this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
					}
				}
				StringFormat stringFormat = new StringFormat()
				{
					Alignment = StringAlignment.Center
				};
				int num = 0;
				if (num == 0)
				{
					int left = this.PrintMargins.Left;
					int top = this.PrintMargins.Top;
					System.Drawing.Size size = new System.Drawing.Size(0, 0);
					System.Drawing.Size picMemoDrawingHeight = new System.Drawing.Size();
					string empty = string.Empty;
					empty = this.objCurrentPrintJob.strPicMemoValue;
					string str = string.Empty;
					if (this.objCurrentPrintJob.pageSplitCount != 1)
					{
						if (this.objCurrentPrintJob.pageSplitCount == 2)
						{
							if (this.imagetoPrint.Width < this.imagetoPrint.Height)
							{
								int width = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int height = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height);
									}
								}
								catch (Exception exception)
								{
								}
								int height1 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size1 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										width -= picMemoDrawingHeight.Height;
										int left1 = this.PrintMargins.Left;
										int top1 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width1 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF = new RectangleF((float)left1, (float)top1, (float)width1, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF, stringFormat);
									}
									height1 = size1.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str1 = string.Concat(resource);
									System.Drawing.Size size2 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size3 = TextRenderer.MeasureText(str1, this.previewFont);
									top = top + size2.Height + size3.Height + picMemoDrawingHeight.Height;
									width = width - size2.Height - size3.Height - picMemoDrawingHeight.Height;
									int num1 = this.PrintMargins.Left;
									int top2 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width2 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height2 = size2.Height + size3.Height;
									RectangleF rectangleF1 = new RectangleF((float)num1, (float)top2, (float)width2, (float)height2);
									RectangleF rectangleF2 = new RectangleF((float)num1, (float)(top2 + size2.Height + size3.Height), (float)width2, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF1, stringFormat);
									e.Graphics.DrawString(str1, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height - size3.Width) / 2), (float)(this.PrintMargins.Top + size2.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF2, stringFormat);
									height1 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphics = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphics.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size1.Width), (float)height1);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									width -= size.Height;
								}
								decimal num2 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num3 = width;
								int num4 = (int)Math.Round(width * num2, 4);
								num4 *= 2;
								num3 *= 2;
								while (num4 > height || num3 > width * 2)
								{
									num3 -= 10;
									num4 -= 10;
								}
								if (this.srcY == 0)
								{
									top = top + (width * 2 - num3) / 2;
								}
								num3 /= 2;
								left = left + (height - num4) / 2;
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num4, num3), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size.Height));
								}
								this.srcY = this.imagetoPrint.Height / 2;
							}
							else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
							{
								int height3 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width3 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width3);
									}
								}
								catch (Exception exception1)
								{
								}
								int top3 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size4 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height3 -= picMemoDrawingHeight.Height;
										int left2 = this.PrintMargins.Left;
										int top4 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width4 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF3 = new RectangleF((float)left2, (float)top4, (float)width4, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF3, stringFormat);
									}
									top3 = size4.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str2 = string.Concat(resource);
									System.Drawing.Size size5 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size6 = TextRenderer.MeasureText(str2, this.previewFont);
									top = top + size5.Height + size6.Height + picMemoDrawingHeight.Height;
									height3 = height3 - size5.Height - size6.Height - picMemoDrawingHeight.Height;
									int left3 = this.PrintMargins.Left;
									int top5 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width5 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height4 = size5.Height + size6.Height;
									RectangleF rectangleF4 = new RectangleF((float)left3, (float)top5, (float)width5, (float)height4);
									RectangleF rectangleF5 = new RectangleF((float)left3, (float)(top5 + size5.Height + size6.Height), (float)width5, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF4, stringFormat);
									e.Graphics.DrawString(str2, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width3 - size6.Width) / 2), (float)(this.PrintMargins.Top + size5.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF5, stringFormat);
									top3 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphic = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphic.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size4.Width), (float)top3);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height3 -= size.Height;
								}
								decimal num5 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num6 = width3;
								int num7 = (int)Math.Round(width3 * num5, 4);
								if (num7 > height3)
								{
									num7 = height3;
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num6, num7), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
								}
								this.srcX = this.imagetoPrint.Width / 2;
							}
							else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
							{
								int height5 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width6 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width6);
									}
								}
								catch (Exception exception2)
								{
								}
								int height6 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size7 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height5 -= picMemoDrawingHeight.Height;
										int left4 = this.PrintMargins.Left;
										int top6 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width7 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF6 = new RectangleF((float)left4, (float)top6, (float)width7, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF6, stringFormat);
									}
									height6 = size7.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str3 = string.Concat(resource);
									System.Drawing.Size size8 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size9 = TextRenderer.MeasureText(str3, this.previewFont);
									top = top + size8.Height + size9.Height + picMemoDrawingHeight.Height;
									height5 = height5 - size8.Height - size9.Height - picMemoDrawingHeight.Height;
									int left5 = this.PrintMargins.Left;
									int top7 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width8 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height7 = size8.Height + size9.Height;
									RectangleF rectangleF7 = new RectangleF((float)left5, (float)top7, (float)width8, (float)height7);
									RectangleF rectangleF8 = new RectangleF((float)left5, (float)(top7 + size8.Height + size9.Height), (float)width8, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF7, stringFormat);
									e.Graphics.DrawString(str3, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width6 - size9.Width) / 2), (float)(this.PrintMargins.Top + size8.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF8, stringFormat);
									height6 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphics1 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphics1.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size7.Width), (float)height6);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height5 -= size.Height;
								}
								decimal num8 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num9 = width6;
								int num10 = (int)Math.Round(width6 * num8, 4);
								if (num10 < height5)
								{
									num10 = height5;
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num9, num10), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
								}
								this.srcX = this.imagetoPrint.Width / 2;
							}
							else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
							{
								int height8 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int height9 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height9);
									}
								}
								catch (Exception exception3)
								{
								}
								int top8 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size10 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height8 -= picMemoDrawingHeight.Height;
										int left6 = this.PrintMargins.Left;
										int top9 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width9 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF9 = new RectangleF((float)left6, (float)top9, (float)width9, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF9, stringFormat);
									}
									top8 = size10.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str4 = string.Concat(resource);
									System.Drawing.Size size11 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size12 = TextRenderer.MeasureText(str4, this.previewFont);
									top = top + size11.Height + size12.Height + picMemoDrawingHeight.Height;
									height8 = height8 - size11.Height - size12.Height - picMemoDrawingHeight.Height;
									int left7 = this.PrintMargins.Left;
									int top10 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width10 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height10 = size11.Height + size12.Height;
									RectangleF rectangleF10 = new RectangleF((float)left7, (float)top10, (float)width10, (float)height10);
									RectangleF rectangleF11 = new RectangleF((float)left7, (float)(top10 + size11.Height + size12.Height), (float)width10, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF10, stringFormat);
									e.Graphics.DrawString(str4, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height9 - size12.Width) / 2), (float)(this.PrintMargins.Top + size11.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF11, stringFormat);
									top8 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphic1 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphic1.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size10.Width), (float)top8);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height8 -= size.Height;
								}
								decimal num11 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num12 = height8;
								int num13 = (int)Math.Round(height8 / num11, 4);
								if (num13 > height9)
								{
									num13 = height9;
									num12 = (int)(num13 * num11);
								}
								num13 = height9;
								num12 = (int)(num13 * num11);
								num12 /= 2;
								top = top + (height8 - num12) / 2;
								left = left + (height9 - num13) / 2;
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num13, num12), this.srcX, this.srcY, this.imagetoPrint.Width, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size.Height));
								}
								this.srcY = this.imagetoPrint.Height / 2;
							}
							this.splitPageCounter++;
							if (this.splitPageCounter >= this.objCurrentPrintJob.pageSplitCount)
							{
								e.HasMorePages = false;
								this.bHasPages = false;
								this.splitPageCounter = 0;
								this.headerPgColCounter = 1;
								this.headerPgRowCounter = 1;
							}
							else
							{
								if (this.imagetoPrint.Height == this.imagetoPrint.Width)
								{
									if (this.objCurrentPrintJob.sOrientation == "0")
									{
										this.headerPgRowCounter++;
									}
									else if (this.objCurrentPrintJob.sOrientation == "1")
									{
										this.headerPgColCounter++;
									}
								}
								else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
								{
									this.headerPgColCounter++;
								}
								else if (this.imagetoPrint.Height > this.imagetoPrint.Width)
								{
									this.headerPgRowCounter++;
								}
								e.HasMorePages = true;
								this.bHasPages = true;
								return;
							}
						}
						else if (this.objCurrentPrintJob.pageSplitCount == 4)
						{
							if (this.imagetoPrint.Width < this.imagetoPrint.Height)
							{
								int width11 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								int height11 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width11);
									}
								}
								catch (Exception exception4)
								{
								}
								int top11 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size13 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height11 -= picMemoDrawingHeight.Height;
										int left8 = this.PrintMargins.Left;
										int top12 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width12 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF12 = new RectangleF((float)left8, (float)top12, (float)width12, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF12, stringFormat);
									}
									top11 = size13.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str5 = string.Concat(resource);
									System.Drawing.Size size14 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size15 = TextRenderer.MeasureText(str5, this.previewFont);
									top = top + size14.Height + size15.Height + picMemoDrawingHeight.Height;
									height11 = height11 - size14.Height - size15.Height - picMemoDrawingHeight.Height;
									int left9 = this.PrintMargins.Left;
									int top13 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width13 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height12 = size14.Height + size15.Height;
									RectangleF rectangleF13 = new RectangleF((float)left9, (float)top13, (float)width13, (float)height12);
									RectangleF rectangleF14 = new RectangleF((float)left9, (float)(top13 + size14.Height + size15.Height), (float)width13, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF13, stringFormat);
									e.Graphics.DrawString(str5, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width11 - size15.Width) / 2), (float)(this.PrintMargins.Top + size14.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF14, stringFormat);
									top11 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphics2 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphics2.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size13.Width), (float)top11);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height11 -= size.Height;
								}
								decimal num14 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								if ((int)Math.Round(height11 * num14, 4) > width11)
								{
									int num15 = (int)(width11 / num14);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width11, height11), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
								}
							}
							else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
							{
								int height13 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width14 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height13);
									}
								}
								catch (Exception exception5)
								{
								}
								int height14 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size16 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										width14 -= picMemoDrawingHeight.Height;
										int left10 = this.PrintMargins.Left;
										int top14 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width15 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF15 = new RectangleF((float)left10, (float)top14, (float)width15, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF15, stringFormat);
									}
									height14 = size16.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str6 = string.Concat(resource);
									System.Drawing.Size size17 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size18 = TextRenderer.MeasureText(str6, this.previewFont);
									top = top + size17.Height + size18.Height + picMemoDrawingHeight.Height;
									width14 = width14 - size17.Height - size18.Height - picMemoDrawingHeight.Height;
									int left11 = this.PrintMargins.Left;
									int top15 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width16 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height15 = size17.Height + size18.Height;
									RectangleF rectangleF16 = new RectangleF((float)left11, (float)top15, (float)width16, (float)height15);
									RectangleF rectangleF17 = new RectangleF((float)left11, (float)(top15 + size17.Height + size18.Height), (float)width16, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF16, stringFormat);
									e.Graphics.DrawString(str6, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height13 - size18.Width) / 2), (float)(this.PrintMargins.Top + size17.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF17, stringFormat);
									height14 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphic2 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphic2.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size16.Width), (float)height14);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									width14 -= size.Height;
								}
								decimal num16 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num17 = height13;
								int num18 = (int)Math.Round(num17 / num16, 4);
								if (num18 > width14)
								{
									num18 = width14;
									num17 = (int)(num18 * num16);
								}
								if (this.splitPageCounter == 0)
								{
									top = top + (width14 - num18);
									left = left + (height13 - num17);
								}
								else if (this.splitPageCounter == 1)
								{
									top = top + (width14 - num18);
								}
								else if (this.splitPageCounter == 2)
								{
									left = left + (height13 - num17);
								}
								else if (this.splitPageCounter == 3)
								{
									top = top + (width14 - num18);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num17, num18), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size.Height));
								}
							}
							else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
							{
								int height16 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width17 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height16);
									}
								}
								catch (Exception exception6)
								{
								}
								int top16 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size19 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										width17 -= picMemoDrawingHeight.Height;
										int left12 = this.PrintMargins.Left;
										int top17 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width18 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF18 = new RectangleF((float)left12, (float)top17, (float)width18, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF18, stringFormat);
									}
									top16 = size19.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str7 = string.Concat(resource);
									System.Drawing.Size size20 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size21 = TextRenderer.MeasureText(str7, this.previewFont);
									top = top + size20.Height + size21.Height + picMemoDrawingHeight.Height;
									width17 = width17 - size20.Height - size21.Height - picMemoDrawingHeight.Height;
									int left13 = this.PrintMargins.Left;
									int top18 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width19 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height17 = size20.Height + size21.Height;
									RectangleF rectangleF19 = new RectangleF((float)left13, (float)top18, (float)width19, (float)height17);
									RectangleF rectangleF20 = new RectangleF((float)left13, (float)(top18 + size20.Height + size21.Height), (float)width19, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF19, stringFormat);
									e.Graphics.DrawString(str7, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height16 - size21.Width) / 2), (float)(this.PrintMargins.Top + size20.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF20, stringFormat);
									top16 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphics3 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphics3.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size19.Width), (float)top16);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									width17 -= size.Height;
								}
								decimal num19 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num20 = height16;
								int num21 = (int)Math.Round(num20 / num19, 4);
								if (num21 > width17)
								{
									num21 = width17;
									num20 = (int)(num21 * num19);
								}
								if (this.splitPageCounter == 0)
								{
									top = top + (width17 - num21);
									left = left + (height16 - num20);
								}
								else if (this.splitPageCounter == 1)
								{
									top = top + (width17 - num21);
								}
								else if (this.splitPageCounter == 2)
								{
									left = left + (height16 - num20);
								}
								else if (this.splitPageCounter == 3)
								{
									top = top + (width17 - num21);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num20, num21), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size.Height));
								}
							}
							else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
							{
								int width20 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								int height18 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width20);
									}
								}
								catch (Exception exception7)
								{
								}
								int height19 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size22 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height18 -= picMemoDrawingHeight.Height;
										int left14 = this.PrintMargins.Left;
										int top19 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width21 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF21 = new RectangleF((float)left14, (float)top19, (float)width21, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF21, stringFormat);
									}
									height19 = size22.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str8 = string.Concat(resource);
									System.Drawing.Size size23 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size24 = TextRenderer.MeasureText(str8, this.previewFont);
									top = top + size23.Height + size24.Height + picMemoDrawingHeight.Height;
									height18 = height18 - size23.Height - size24.Height - picMemoDrawingHeight.Height;
									int left15 = this.PrintMargins.Left;
									int top20 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width22 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height20 = size23.Height + size24.Height;
									RectangleF rectangleF22 = new RectangleF((float)left15, (float)top20, (float)width22, (float)height20);
									RectangleF rectangleF23 = new RectangleF((float)left15, (float)(top20 + size23.Height + size24.Height), (float)width22, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF22, stringFormat);
									e.Graphics.DrawString(str8, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width20 - size24.Width) / 2), (float)(this.PrintMargins.Top + size23.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF23, stringFormat);
									height19 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphic3 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphic3.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size22.Width), (float)height19);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height18 -= size.Height;
								}
								decimal num22 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								if ((int)Math.Round(height18 * num22, 4) > width20)
								{
									int num23 = (int)(width20 / num22);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width20, height18), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
								}
							}
							this.splitPageCounter++;
							if (this.splitPageCounter >= this.objCurrentPrintJob.pageSplitCount)
							{
								e.HasMorePages = false;
								this.bHasPages = false;
								this.splitPageCounter = 0;
								this.headerPgColCounter = 1;
								this.headerPgRowCounter = 1;
							}
							else
							{
								if (this.splitPageCounter % 2 == 1)
								{
									this.srcX = this.imagetoPrint.Width / 2;
								}
								this.headerPgColCounter++;
								if (this.splitPageCounter == 2)
								{
									this.headerPgRowCounter++;
									this.headerPgColCounter = 1;
									this.srcY = this.imagetoPrint.Height / 2;
									this.srcX = 0;
								}
								e.HasMorePages = true;
								this.bHasPages = true;
								return;
							}
						}
						else if (this.objCurrentPrintJob.pageSplitCount == 8)
						{
							if (this.imagetoPrint.Width < this.imagetoPrint.Height)
							{
								int height21 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int height22 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height22);
									}
								}
								catch (Exception exception8)
								{
								}
								int top21 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size25 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height21 -= picMemoDrawingHeight.Height;
										int left16 = this.PrintMargins.Left;
										int top22 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width23 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF24 = new RectangleF((float)left16, (float)top22, (float)width23, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF24, stringFormat);
									}
									top21 = size25.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str9 = string.Concat(resource);
									System.Drawing.Size size26 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size27 = TextRenderer.MeasureText(str9, this.previewFont);
									top = top + size26.Height + size27.Height + picMemoDrawingHeight.Height;
									height21 = height21 - size26.Height - size27.Height - picMemoDrawingHeight.Height;
									int left17 = this.PrintMargins.Left;
									int top23 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width24 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height23 = size26.Height + size27.Height;
									RectangleF rectangleF25 = new RectangleF((float)left17, (float)top23, (float)width24, (float)height23);
									RectangleF rectangleF26 = new RectangleF((float)left17, (float)(top23 + size26.Height + size27.Height), (float)width24, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF25, stringFormat);
									e.Graphics.DrawString(str9, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height22 - size27.Width) / 2), (float)(this.PrintMargins.Top + size26.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF26, stringFormat);
									top21 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphics4 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphics4.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size25.Width), (float)top21);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height21 -= size.Height;
								}
								decimal num24 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num25 = height21;
								int num26 = (int)Math.Round(height21 / num24, 4);
								if (num26 > height22)
								{
									num26 = height22;
									num25 = (int)(num26 * num24);
								}
								if (this.splitPageCounter % 2 == 0)
								{
									left = left + (height22 - num26);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num26, num25), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size.Height));
								}
							}
							else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
							{
								int height24 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width25 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width25);
									}
								}
								catch (Exception exception9)
								{
								}
								int top24 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size28 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height24 -= picMemoDrawingHeight.Height;
										int left18 = this.PrintMargins.Left;
										int top25 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width26 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF27 = new RectangleF((float)left18, (float)top25, (float)width26, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF27, stringFormat);
									}
									top24 = size28.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str10 = string.Concat(resource);
									System.Drawing.Size size29 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size30 = TextRenderer.MeasureText(str10, this.previewFont);
									top = top + size29.Height + size30.Height + picMemoDrawingHeight.Height;
									height24 = height24 - size29.Height - size30.Height - picMemoDrawingHeight.Height;
									int left19 = this.PrintMargins.Left;
									int top26 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width27 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height25 = size29.Height + size30.Height;
									RectangleF rectangleF28 = new RectangleF((float)left19, (float)top26, (float)width27, (float)height25);
									RectangleF rectangleF29 = new RectangleF((float)left19, (float)(top26 + size29.Height + size30.Height), (float)width27, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF28, stringFormat);
									e.Graphics.DrawString(str10, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width25 - size30.Width) / 2), (float)(this.PrintMargins.Top + size29.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF29, stringFormat);
									top24 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphic4 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphic4.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size28.Width), (float)top24);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height24 -= size.Height;
								}
								decimal num27 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num28 = width25;
								int num29 = (int)Math.Round(width25 * num27, 4);
								if (num29 > height24)
								{
									num29 = height24;
								}
								if (this.splitPageCounter < 4)
								{
									top = top + (height24 - num29);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num28, num29), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
								}
							}
							else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
							{
								int height26 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int width28 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width28);
									}
								}
								catch (Exception exception10)
								{
								}
								int height27 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size31 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height26 -= picMemoDrawingHeight.Height;
										int left20 = this.PrintMargins.Left;
										int top27 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width29 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF30 = new RectangleF((float)left20, (float)top27, (float)width29, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF30, stringFormat);
									}
									height27 = size31.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str11 = string.Concat(resource);
									System.Drawing.Size size32 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size33 = TextRenderer.MeasureText(str11, this.previewFont);
									top = top + size32.Height + size33.Height + picMemoDrawingHeight.Height;
									height26 = height26 - size32.Height - size33.Height - picMemoDrawingHeight.Height;
									int left21 = this.PrintMargins.Left;
									int top28 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width30 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height28 = size32.Height + size33.Height;
									RectangleF rectangleF31 = new RectangleF((float)left21, (float)top28, (float)width30, (float)height28);
									RectangleF rectangleF32 = new RectangleF((float)left21, (float)(top28 + size32.Height + size33.Height), (float)width30, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF31, stringFormat);
									e.Graphics.DrawString(str11, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width28 - size33.Width) / 2), (float)(this.PrintMargins.Top + size32.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF32, stringFormat);
									height27 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphics5 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphics5.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Width - this.PrintMargins.Right - size31.Width), (float)height27);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height26 -= size.Height;
								}
								decimal num30 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num31 = width28;
								int num32 = (int)Math.Round(width28 * num30, 4);
								if (num32 > height26)
								{
									num32 = height26;
								}
								if (this.splitPageCounter < 4)
								{
									top = top + (height26 - num32);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num31, num32), this.srcX, this.srcY, this.imagetoPrint.Width / 4, this.imagetoPrint.Height / 2, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
								}
							}
							else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
							{
								int height29 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
								int height30 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
								try
								{
									if (empty.Length > 0)
									{
										str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
										str = str.Substring(0, 1);
										picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height30);
									}
								}
								catch (Exception exception11)
								{
								}
								int top29 = 0;
								objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
								System.Drawing.Size size34 = TextRenderer.MeasureText(string.Concat(objArray), this.previewFont);
								if (this.objCurrentPrintJob.sPrintHeaderFooter != "1")
								{
									if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
									{
										top += picMemoDrawingHeight.Height;
										height29 -= picMemoDrawingHeight.Height;
										int left22 = this.PrintMargins.Left;
										int top30 = this.PrintMargins.Top;
										pageBounds = e.PageBounds;
										int width31 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
										RectangleF rectangleF33 = new RectangleF((float)left22, (float)top30, (float)width31, (float)picMemoDrawingHeight.Height);
										e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF33, stringFormat);
									}
									top29 = size34.Height;
								}
								else
								{
									resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
									now = DateTime.Now;
									resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
									resource[7] = Environment.NewLine;
									string str12 = string.Concat(resource);
									System.Drawing.Size size35 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
									System.Drawing.Size size36 = TextRenderer.MeasureText(str12, this.previewFont);
									top = top + size35.Height + size36.Height + picMemoDrawingHeight.Height;
									height29 = height29 - size35.Height - size36.Height - picMemoDrawingHeight.Height;
									int left23 = this.PrintMargins.Left;
									int top31 = this.PrintMargins.Top;
									pageBounds = e.PageBounds;
									int width32 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
									int height31 = size35.Height + size36.Height;
									RectangleF rectangleF34 = new RectangleF((float)left23, (float)top31, (float)width32, (float)height31);
									RectangleF rectangleF35 = new RectangleF((float)left23, (float)(top31 + size35.Height + size36.Height), (float)width32, (float)picMemoDrawingHeight.Height);
									e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF34, stringFormat);
									e.Graphics.DrawString(str12, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height30 - size36.Width) / 2), (float)(this.PrintMargins.Top + size35.Height));
									e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF35, stringFormat);
									top29 = this.PrintMargins.Top;
								}
								if (this.objCurrentPrintJob.sPrintPgNos == "1")
								{
									Graphics graphic5 = e.Graphics;
									objArray = new object[] { "Row = ", this.headerPgRowCounter, ", Col = ", this.headerPgColCounter };
									graphic5.DrawString(string.Concat(objArray), this.previewFont, new SolidBrush(Color.Black), (float)(this.PaperSize.Height - this.PrintMargins.Right - size34.Width), (float)top29);
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
									height29 -= size.Height;
								}
								decimal num33 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
								int num34 = height29;
								int num35 = (int)Math.Round(height29 / num33, 4);
								if (num35 > height30)
								{
									num35 = height30;
									num34 = (int)(num35 * num33);
								}
								num35 = height30;
								num34 = (int)(num35 * num33);
								num34 /= 2;
								top = top + (height29 - num34) / 2;
								if (this.splitPageCounter % 2 == 0)
								{
									left = left + (height30 - num35);
								}
								e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num35, num34), this.srcX, this.srcY, this.imagetoPrint.Width / 2, this.imagetoPrint.Height / 4, GraphicsUnit.Pixel);
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size.Height));
								}
							}
							this.splitPageCounter++;
							if (this.splitPageCounter >= this.objCurrentPrintJob.pageSplitCount)
							{
								e.HasMorePages = false;
								this.bHasPages = false;
								this.splitPageCounter = 0;
								this.headerPgColCounter = 1;
								this.headerPgRowCounter = 1;
							}
							else
							{
								if (this.imagetoPrint.Width < this.imagetoPrint.Height)
								{
									this.headerPgColCounter++;
									if (this.splitPageCounter % 2 != 0)
									{
										this.srcX = this.imagetoPrint.Width / 2;
									}
									else
									{
										this.headerPgColCounter = 1;
										this.headerPgRowCounter++;
										PreviewManager previewManager = this;
										previewManager.srcY = previewManager.srcY + this.imagetoPrint.Height / 4;
										this.srcX = 0;
									}
								}
								else if (this.imagetoPrint.Width > this.imagetoPrint.Height)
								{
									this.headerPgColCounter++;
									if (this.splitPageCounter % 4 != 0)
									{
										PreviewManager previewManager1 = this;
										previewManager1.srcX = previewManager1.srcX + this.imagetoPrint.Width / 4;
									}
									else
									{
										this.headerPgColCounter = 1;
										this.headerPgRowCounter++;
										PreviewManager previewManager2 = this;
										previewManager2.srcY = previewManager2.srcY + this.imagetoPrint.Height / 2;
										this.srcX = 0;
									}
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
								{
									if (this.objCurrentPrintJob.sOrientation == "1")
									{
										this.headerPgColCounter++;
										if (this.splitPageCounter % 4 != 0)
										{
											PreviewManager previewManager3 = this;
											previewManager3.srcX = previewManager3.srcX + this.imagetoPrint.Width / 4;
										}
										else
										{
											this.headerPgColCounter = 1;
											this.headerPgRowCounter++;
											PreviewManager previewManager4 = this;
											previewManager4.srcY = previewManager4.srcY + this.imagetoPrint.Height / 2;
											this.srcX = 0;
										}
									}
									else if (this.objCurrentPrintJob.sOrientation == "0")
									{
										this.headerPgColCounter++;
										if (this.splitPageCounter % 2 != 0)
										{
											this.srcX = this.imagetoPrint.Width / 2;
										}
										else
										{
											this.headerPgColCounter = 1;
											this.headerPgRowCounter++;
											PreviewManager previewManager5 = this;
											previewManager5.srcY = previewManager5.srcY + this.imagetoPrint.Height / 4;
											this.srcX = 0;
										}
									}
								}
								e.HasMorePages = true;
								this.bHasPages = true;
								return;
							}
						}
					}
					else if (this.imagetoPrint.Height > this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0" && this.sOrientation == "0" || this.imagetoPrint.Height > this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "1")
					{
						int height32 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
						int width33 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
						try
						{
							if (empty.Length > 0)
							{
								str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
								str = str.Substring(0, 1);
								picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width33);
							}
						}
						catch (Exception exception12)
						{
						}
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
							now = DateTime.Now;
							resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
							resource[7] = Environment.NewLine;
							string str13 = string.Concat(resource);
							System.Drawing.Size size37 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
							System.Drawing.Size size38 = TextRenderer.MeasureText(str13, this.previewFont);
							top = top + size37.Height + size38.Height + picMemoDrawingHeight.Height;
							height32 = height32 - size37.Height - size38.Height - picMemoDrawingHeight.Height;
							int left24 = this.PrintMargins.Left;
							int top32 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width34 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							int height33 = size37.Height + size38.Height + picMemoDrawingHeight.Height;
							RectangleF rectangleF36 = new RectangleF((float)left24, (float)(top32 + size37.Height + size38.Height), (float)width34, (float)picMemoDrawingHeight.Height);
							RectangleF rectangleF37 = new RectangleF((float)left24, (float)top32, (float)width34, (float)height33);
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF37, stringFormat);
							e.Graphics.DrawString(str13, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width33 - size38.Width) / 2), (float)(this.PrintMargins.Top + size37.Height));
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF36, stringFormat);
						}
						else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
						{
							int left25 = this.PrintMargins.Left;
							int top33 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width35 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							top += picMemoDrawingHeight.Height;
							height32 -= picMemoDrawingHeight.Height;
							RectangleF rectangleF38 = new RectangleF((float)left25, (float)top33, (float)width35, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF38, stringFormat);
						}
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
							height32 -= size.Height;
						}
						decimal num36 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
						int num37 = (int)Math.Round(height32 * num36, 4);
						if (num37 > width33)
						{
							num36 = Math.Round(decimal.Divide(this.imagetoPrint.Height, this.imagetoPrint.Width), 4);
							int num38 = height32;
							num37 = width33;
							height32 = (int)(num37 * num36);
							top = top + (num38 - height32) / 2;
						}
						left = left + (width33 - num37) / 2;
						e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num37, height32));
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Width - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
						}
					}
					else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Height > this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0")
					{
						int height34 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
						int height35 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
						try
						{
							if (empty.Length > 0)
							{
								str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
								str = str.Substring(0, 1);
								picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height35);
							}
						}
						catch (Exception exception13)
						{
						}
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
							now = DateTime.Now;
							resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
							resource[7] = Environment.NewLine;
							string str14 = string.Concat(resource);
							System.Drawing.Size size39 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
							System.Drawing.Size size40 = TextRenderer.MeasureText(str14, this.previewFont);
							top = top + size39.Height + size40.Height + picMemoDrawingHeight.Height;
							height34 = height34 - size39.Height - size40.Height - picMemoDrawingHeight.Height;
							int left26 = this.PrintMargins.Left;
							int top34 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width36 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							int height36 = size39.Height + size40.Height;
							RectangleF rectangleF39 = new RectangleF((float)left26, (float)top34, (float)width36, (float)height36);
							RectangleF rectangleF40 = new RectangleF((float)left26, (float)(top34 + size39.Height + size40.Height), (float)width36, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF39, stringFormat);
							e.Graphics.DrawString(str14, this.previewFont, new SolidBrush(Color.Black), (float)(left + (height35 - size40.Width) / 2), (float)(this.PrintMargins.Top + size39.Height));
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF40, stringFormat);
						}
						else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
						{
							int left27 = this.PrintMargins.Left;
							int top35 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width37 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							top += picMemoDrawingHeight.Height;
							height34 -= picMemoDrawingHeight.Height;
							RectangleF rectangleF41 = new RectangleF((float)left27, (float)top35, (float)width37, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF41, stringFormat);
						}
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
							height34 -= size.Height;
						}
						decimal num39 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
						Math.Min((double)height35 / (double)this.imagetoPrint.Width, (double)height34 / (double)this.imagetoPrint.Height);
						int width38 = this.imagetoPrint.Width;
						int height37 = this.imagetoPrint.Height;
						int num40 = (int)Math.Round(height34 * num39, 4);
						left = left + (height35 - num40) / 2;
						e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num40, height34));
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Bottom - size.Height));
						}
					}
					else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0")
					{
						int height38 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
						int width39 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
						try
						{
							if (empty.Length > 0)
							{
								str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
								str = str.Substring(0, 1);
								picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width39);
							}
						}
						catch (Exception exception14)
						{
						}
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
							now = DateTime.Now;
							resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
							resource[7] = Environment.NewLine;
							string str15 = string.Concat(resource);
							System.Drawing.Size size41 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
							System.Drawing.Size size42 = TextRenderer.MeasureText(str15, this.previewFont);
							top = top + size41.Height + size42.Height + picMemoDrawingHeight.Height;
							height38 = height38 - size41.Height - size42.Height - picMemoDrawingHeight.Height;
							int left28 = this.PrintMargins.Left;
							int top36 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width40 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							int height39 = size41.Height + size42.Height;
							RectangleF rectangleF42 = new RectangleF((float)left28, (float)top36, (float)width40, (float)height39);
							RectangleF rectangleF43 = new RectangleF((float)left28, (float)(top36 + size41.Height + size42.Height), (float)width40, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF42, stringFormat);
							e.Graphics.DrawString(str15, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width39 - size42.Width) / 2), (float)(this.PrintMargins.Top + size41.Height));
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF43, stringFormat);
						}
						else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
						{
							int left29 = this.PrintMargins.Left;
							int top37 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width41 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							top += picMemoDrawingHeight.Height;
							height38 -= picMemoDrawingHeight.Height;
							RectangleF rectangleF44 = new RectangleF((float)left29, (float)top37, (float)width41, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF44, stringFormat);
						}
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
							height38 -= size.Height;
						}
						decimal num41 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
						int num42 = (int)Math.Round(width39 / num41, 4);
						top = top + (height38 - num42) / 2;
						e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width39, num42));
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width39 - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
						}
					}
					else if (this.imagetoPrint.Height < this.imagetoPrint.Width && this.objCurrentPrintJob.spaperUtilization == "0" || this.objCurrentPrintJob.spaperUtilization == "1" && this.imagetoPrint.Width > this.imagetoPrint.Height)
					{
						left = this.PrintMargins.Left;
						top = this.PrintMargins.Top;
						int height40 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
						int height41 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
						try
						{
							if (empty.Length > 0)
							{
								str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
								str = str.Substring(0, 1);
								picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height41);
							}
						}
						catch (Exception exception15)
						{
						}
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
							now = DateTime.Now;
							resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
							resource[7] = Environment.NewLine;
							string str16 = string.Concat(resource);
							System.Drawing.Size size43 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
							System.Drawing.Size size44 = TextRenderer.MeasureText(str16, this.previewFont);
							top = top + size43.Height + size44.Height + picMemoDrawingHeight.Height;
							height40 = height40 - size43.Height - size44.Height - picMemoDrawingHeight.Height;
							int left30 = this.PrintMargins.Left;
							int top38 = this.PrintMargins.Top;
							int width42 = e.PageBounds.Width;
							int left31 = this.PrintMargins.Left;
							int right = this.PrintMargins.Right;
							int height42 = size43.Height;
							int height43 = size44.Height;
							RectangleF rectangleF45 = new RectangleF((float)left, (float)top38, (float)height41, (float)size43.Height);
							RectangleF rectangleF46 = new RectangleF((float)left, (float)(top38 + size43.Height), (float)height41, (float)size44.Height);
							RectangleF rectangleF47 = new RectangleF((float)left, (float)(top38 + size43.Height + size44.Height), (float)height41, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF45, stringFormat);
							e.Graphics.DrawString(str16, this.previewFont, new SolidBrush(Color.Black), rectangleF46, stringFormat);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF47, stringFormat);
						}
						else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
						{
							top += picMemoDrawingHeight.Height;
							height40 -= picMemoDrawingHeight.Height;
							int left32 = this.PrintMargins.Left;
							int top39 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width43 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							RectangleF rectangleF48 = new RectangleF((float)left32, (float)top39, (float)width43, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF48, stringFormat);
						}
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
							height40 -= size.Height;
						}
						decimal num43 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
						int num44 = (int)Math.Round(height41 / num43, 4);
						int num45 = height41;
						if (num44 > height40)
						{
							num44 = height40;
							num45 = (int)(num44 * num43);
							left = left + (height41 - num45) / 2;
						}
						e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num45, height40));
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Top - size.Height));
						}
					}
					else if (this.objCurrentPrintJob.sOrientation == "1" && this.imagetoPrint.Width == this.imagetoPrint.Height)
					{
						left = this.PrintMargins.Left;
						top = this.PrintMargins.Top;
						int width44 = this.PaperSize.Width - this.PrintMargins.Top - this.PrintMargins.Bottom;
						int height44 = this.PaperSize.Height - this.PrintMargins.Left - this.PrintMargins.Right;
						try
						{
							if (empty.Length > 0)
							{
								str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
								str = str.Substring(0, 1);
								picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, height44);
							}
						}
						catch (Exception exception16)
						{
						}
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
							now = DateTime.Now;
							resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
							resource[7] = Environment.NewLine;
							string str17 = string.Concat(resource);
							System.Drawing.Size size45 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
							System.Drawing.Size size46 = TextRenderer.MeasureText(str17, this.previewFont);
							top = top + size45.Height + size46.Height + picMemoDrawingHeight.Height;
							width44 = width44 - size45.Height - size46.Height - picMemoDrawingHeight.Height;
							int left33 = this.PrintMargins.Left;
							int top40 = this.PrintMargins.Top;
							int width45 = e.PageBounds.Width;
							int left34 = this.PrintMargins.Left;
							int right1 = this.PrintMargins.Right;
							int height45 = size45.Height;
							int height46 = size46.Height;
							RectangleF rectangleF49 = new RectangleF((float)left, (float)top40, (float)height44, (float)size45.Height);
							RectangleF rectangleF50 = new RectangleF((float)left, (float)(top40 + size45.Height), (float)height44, (float)size46.Height);
							RectangleF rectangleF51 = new RectangleF((float)left, (float)(top40 + size45.Height + size46.Height), (float)height44, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF49, stringFormat);
							e.Graphics.DrawString(str17, this.previewFont, new SolidBrush(Color.Black), rectangleF50, stringFormat);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF51, stringFormat);
						}
						else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
						{
							top += picMemoDrawingHeight.Height;
							width44 -= picMemoDrawingHeight.Height;
							int left35 = this.PrintMargins.Left;
							int top41 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width46 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							RectangleF rectangleF52 = new RectangleF((float)left35, (float)top41, (float)width46, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF52, stringFormat);
						}
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
							width44 -= size.Height;
						}
						decimal num46 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
						int num47 = (int)Math.Round(height44 / num46, 4);
						int num48 = height44;
						if (num47 > width44)
						{
							num47 = width44;
							num48 = (int)(num47 * num46);
							left = left + (height44 - num48) / 2;
						}
						e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, num48, width44));
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)((this.PaperSize.Height - size.Width) / 2), (float)(this.PaperSize.Width - this.PrintMargins.Top - size.Height));
						}
					}
					else if (this.objCurrentPrintJob.sOrientation == "0" && this.imagetoPrint.Width == this.imagetoPrint.Height)
					{
						int height47 = this.PaperSize.Height - this.PrintMargins.Top - this.PrintMargins.Bottom;
						int width47 = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
						try
						{
							if (empty.Length > 0)
							{
								str = this.objCurrentPrintJob.strPicMemoValue.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
								str = str.Substring(0, 1);
								picMemoDrawingHeight.Height = this.GetPicMemoDrawingHeight(empty, str, this.MemoFont, width47);
							}
						}
						catch (Exception exception17)
						{
						}
						if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
						{
							resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDate.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
							now = DateTime.Now;
							resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
							resource[7] = Environment.NewLine;
							string str18 = string.Concat(resource);
							System.Drawing.Size size47 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
							System.Drawing.Size size48 = TextRenderer.MeasureText(str18, this.previewFont);
							top = top + size47.Height + size48.Height + picMemoDrawingHeight.Height;
							height47 = height47 - size47.Height - size48.Height - picMemoDrawingHeight.Height;
							int left36 = this.PrintMargins.Left;
							int top42 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width48 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							int height48 = size47.Height + size48.Height;
							RectangleF rectangleF53 = new RectangleF((float)left36, (float)top42, (float)width48, (float)height48);
							RectangleF rectangleF54 = new RectangleF((float)left36, (float)(top42 + size47.Height + size48.Height), (float)width48, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF53, stringFormat);
							e.Graphics.DrawString(str18, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width47 - size48.Width) / 2), (float)(this.PrintMargins.Top + size47.Height));
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF54, stringFormat);
						}
						else if (!string.IsNullOrEmpty(this.objCurrentPrintJob.strPicMemoValue))
						{
							int left37 = this.PrintMargins.Left;
							int top43 = this.PrintMargins.Top;
							pageBounds = e.PageBounds;
							int width49 = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
							top += picMemoDrawingHeight.Height;
							height47 -= picMemoDrawingHeight.Height;
							RectangleF rectangleF55 = new RectangleF((float)left37, (float)top43, (float)width49, (float)picMemoDrawingHeight.Height);
							e.Graphics.DrawString(this.objCurrentPrintJob.strPicMemoValue, this.MemoFont, new SolidBrush(Color.Black), rectangleF55, stringFormat);
						}
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							size = TextRenderer.MeasureText(this.objCurrentPrintJob.copyRightField, this.previewFont);
							height47 -= size.Height;
						}
						decimal num49 = Math.Round(decimal.Divide(this.imagetoPrint.Width, this.imagetoPrint.Height), 4);
						int num50 = (int)Math.Round(width47 / num49, 4);
						top = top + (height47 - num50) / 2;
						e.Graphics.DrawImage(this.imagetoPrint, new Rectangle(left, top, width47, num50));
						if (this.objCurrentPrintJob.copyRightField != string.Empty)
						{
							e.Graphics.DrawString(this.objCurrentPrintJob.copyRightField, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width47 - size.Width) / 2), (float)(this.PaperSize.Height - this.PrintMargins.Bottom - size.Height));
						}
					}
					if (this.objCurrentPrintJob.sPrintPic == "1" && this.objCurrentPrintJob.sPrintList == "1" && this.objCurrentPrintJob.sPrintList != "0" && this.objCurrentPrintJob.pageSplitCount == 1)
					{
						num++;
						e.HasMorePages = false;
					}
				}
				try
				{
					if (!e.HasMorePages)
					{
						this.iImgCounter++;
						if (this.objCurrentPrintJob.sPrintList == "0" || this.objCurrentPrintJob.sLocalListPath == string.Empty)
						{
							this.iPrintJobCounter++;
						}
						if (this.imagetoPrint != null)
						{
							this.imagetoPrint.Dispose();
						}
					}
					if (this.imagetoPrint != null)
					{
						this.imagetoPrint = null;
					}
				}
				catch (Exception exception18)
				{
				}
			}
			catch (Exception exception19)
			{
			}
		}

		private void doc_PrintList(object sender, PrintPageEventArgs e)
		{
			try
			{
				if (this.objCurrentPrintJob.sPrintList == "1" && this.objCurrentPrintJob.sPrintPic == "0" && !this.bHasPages)
				{
					this.objCurrentPrintJob = this.lstPrintJob[this.iPrintJobCounter];
				}
				this.bHasPages = false;
				if (!File.Exists(this.objCurrentPrintJob.sLocalListPath) && this.objCurrentPrintJob.sLocalListPath != string.Empty)
				{
					Download download = new Download();
					download.DownloadFile(this.objCurrentPrintJob.sServerListPath, this.objCurrentPrintJob.sLocalListPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
					int num = -11;
					int num1 = 2;
					int num2 = 500;
					for (int i = 0; i < num1; i++)
					{
						try
						{
							if (!File.Exists(this.objCurrentPrintJob.sLocalListPath))
							{
								break;
							}
							else
							{
								IntPtr intPtr = PreviewManager.UnZipFile(this.objCurrentPrintJob.sLocalListPath);
								num = intPtr.ToInt32();
								if (num != 1 || !File.Exists(this.objCurrentPrintJob.sLocalListPath.ToLower().Replace(".zip", ".xml")))
								{
									Thread.Sleep(num2);
								}
								else
								{
									break;
								}
							}
						}
						catch
						{
						}
					}
				}
				this.LoadPartsListData(this.lstPrintJob[this.iPrintJobCounter].sLocalListPath);
				Rectangle pageBounds = e.PageBounds;
				this.iTotalPageWidth = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
				StringFormat stringFormat = new StringFormat()
				{
					FormatFlags = StringFormatFlags.NoWrap,
					Trimming = StringTrimming.Character
				};
				StringFormat stringFormat1 = stringFormat;
				int num3 = 0;
				int num4 = 0;
				int num5 = 100;
				foreach (KeyValuePair<string, string> dicPLColAlignment in this.dicPLColAlignments)
				{
					string value = dicPLColAlignment.Value;
					char[] chrArray = new char[] { '|' };
					num3 = int.Parse(value.Split(chrArray)[1].ToString());
					if (num3 < num5)
					{
						num5 = num3;
					}
					num4 += num3;
				}
				StringFormat stringFormat2 = new StringFormat()
				{
					Alignment = StringAlignment.Center
				};
				if (this.PartListTable.Rows.Count > 0)
				{
					if (this.objCurrentPrintJob.pageSplitCount <= 1 || !(this.objCurrentPrintJob.sOrientation == "1"))
					{
						e.PageSettings.Landscape = false;
					}
					else
					{
						e.PageSettings.Landscape = true;
					}
					int num6 = 23;
					System.Drawing.Size size = new System.Drawing.Size(0, 0);
					int num7 = 0;
					int left = this.PrintMargins.Left;
					int top = this.PrintMargins.Top;
					int num8 = 0;
					int left1 = this.PrintMargins.Left;
					int height = this.PrintMargins.Top;
					int count = this.PartListTable.Columns.Count;
					Rectangle rectangle = e.PageBounds;
					int width = rectangle.Width - this.PrintMargins.Left - this.PrintMargins.Right;
					num8 = width / count;
					if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
					{
						string[] resource = new string[] { this.GetResource("Update Date", "UPDATE_DATE", ResourceType.LABEL), " : ", this.objCurrentPrintJob.UpdateDatePL.ToString(this.objCurrentPrintJob.dateFormat), "   ", this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL), " : ", null, null };
						DateTime now = DateTime.Now;
						resource[6] = now.ToString(this.objCurrentPrintJob.dateFormat);
						resource[7] = Environment.NewLine;
						string str = string.Concat(resource);
						System.Drawing.Size size1 = TextRenderer.MeasureText(this.objCurrentPrintJob.sDescription, this.previewFont);
						System.Drawing.Size size2 = TextRenderer.MeasureText(str, this.previewFont);
						height = height + size1.Height + size2.Height;
						int left2 = this.PrintMargins.Left;
						int top1 = this.PrintMargins.Top;
						int height1 = size1.Height + size2.Height;
						Rectangle pageBounds1 = e.PageBounds;
						int width1 = pageBounds1.Width - this.PrintMargins.Left - this.PrintMargins.Right;
						RectangleF rectangleF = new RectangleF((float)left2, (float)top1, (float)width1, (float)height1);
						e.Graphics.DrawString(this.objCurrentPrintJob.sDescription, this.previewFont, new SolidBrush(Color.Black), rectangleF, stringFormat2);
						e.Graphics.DrawString(str, this.previewFont, new SolidBrush(Color.Black), (float)(left1 + (width - size2.Width) / 2), (float)(height - size2.Height));
					}
					if (this.objCurrentPrintJob.copyRightField != string.Empty)
					{
						size = TextRenderer.MeasureText(this.copyRightField, this.previewFont);
					}
					int num9 = 0;
					int num10 = 0;
					foreach (KeyValuePair<string, string> keyValuePair in this.dicPLColAlignments)
					{
						string value1 = keyValuePair.Value;
						char[] chrArray1 = new char[] { '|' };
						num3 = int.Parse(value1.Split(chrArray1)[1].ToString());
						if (width >= num9 + num3)
						{
							num9 += num3;
						}
						else
						{
							num10 = Math.Abs(width - num9);
							num9 += num3;
							break;
						}
					}
					if (!this.bIsOldINIPL)
					{
						this.ResizeCellHeight(true, 0, ref num6, this.PartListTable, e, this.strFormat, num5);
					}
					else
					{
						this.ResizeCellHeight(true, 0, ref num6, this.PartListTable, e, this.strFormat, num8);
					}
					int num11 = 0;
					int num12 = 0;
					while (num12 < this.PartListTable.Columns.Count)
					{
						int num13 = 0;
						StringFormat stringFormat3 = new StringFormat();
						try
						{
							stringFormat3.Alignment = StringAlignment.Center;
							stringFormat3.LineAlignment = StringAlignment.Center;
							if (!this.bIsOldINIPL)
							{
								string str1 = this.dicPLColAlignments[this.PartListTable.Columns[num12].ColumnName.ToString()].ToString();
								char[] chrArray2 = new char[] { '|' };
								str1.Split(chrArray2)[0].ToString();
								char[] chrArray3 = new char[] { '|' };
								num13 = int.Parse(str1.Split(chrArray3)[1].ToString());
								stringFormat1.Alignment = StringAlignment.Center;
								stringFormat1.LineAlignment = StringAlignment.Center;
								num8 = num13;
							}
						}
						catch (Exception exception)
						{
						}
						if (num11 + num8 > width)
						{
							num8 = num10;
							e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, height, num8, num6));
							e.Graphics.DrawRectangle(Pens.Black, left1, height, num8, num6);
							e.Graphics.DrawString(this.PartListTable.Columns[num12].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left1, (float)height, (float)num8, (float)num6), stringFormat1);
							left1 += num8;
							break;
						}
						else
						{
							e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left1, height, num8, num6));
							e.Graphics.DrawRectangle(Pens.Black, left1, height, num8, num6);
							e.Graphics.DrawString(this.PartListTable.Columns[num12].Caption, this.previewFont, Brushes.Black, new RectangleF((float)left1, (float)height, (float)num8, (float)num6), stringFormat3);
							left1 += num8;
							num11 += num8;
							num12++;
						}
					}
					left1 = this.PrintMargins.Left;
					num7 = height + num6;
					while (this.iPartsListRowCount < this.PartListTable.Rows.Count)
					{
						if (!this.bIsOldINIPL)
						{
							this.ResizeCellHeight(false, this.iPartsListRowCount, ref num6, this.PartListTable, e, this.strFormat, num5);
						}
						else
						{
							this.ResizeCellHeight(false, this.iPartsListRowCount, ref num6, this.PartListTable, e, this.strFormat, num8);
						}
						if (this.objCurrentPrintJob.sOrientation == "0")
						{
							if (num7 + num6 > this.PaperSize.Height - this.PrintMargins.Bottom - size.Height)
							{
								left1 = this.PrintMargins.Left;
								height = this.PrintMargins.Top;
								num7 = height;
								e.HasMorePages = true;
								this.bHasPages = true;
								if (this.objCurrentPrintJob.sOrientation != "1")
								{
									e.PageSettings.Landscape = false;
								}
								else
								{
									e.PageSettings.Landscape = true;
								}
								this.objCurrentPrintJob.sZoom = "0";
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									Graphics graphics = e.Graphics;
									string str2 = this.objCurrentPrintJob.copyRightField;
									System.Drawing.Font font = this.previewFont;
									SolidBrush solidBrush = new SolidBrush(Color.Black);
									float single = (float)(left1 + (width - size.Width) / 2);
									Rectangle rectangle1 = e.PageBounds;
									graphics.DrawString(str2, font, solidBrush, single, (float)(rectangle1.Bottom - this.PrintMargins.Right - size.Height));
								}
								return;
							}
						}
						else if (num7 + num6 > this.PaperSize.Width - this.PrintMargins.Left - size.Height)
						{
							left1 = this.PrintMargins.Left;
							height = this.PrintMargins.Top;
							num7 = height;
							e.HasMorePages = true;
							this.bHasPages = true;
							if (this.objCurrentPrintJob.sOrientation != "1")
							{
								e.PageSettings.Landscape = false;
							}
							else
							{
								e.PageSettings.Landscape = true;
							}
							this.objCurrentPrintJob.sZoom = "0";
							if (this.objCurrentPrintJob.copyRightField != string.Empty)
							{
								Graphics graphic = e.Graphics;
								string str3 = this.copyRightField;
								System.Drawing.Font font1 = this.previewFont;
								SolidBrush solidBrush1 = new SolidBrush(Color.Black);
								float single1 = (float)(left1 + (width - size.Width) / 2);
								Rectangle pageBounds2 = e.PageBounds;
								graphic.DrawString(str3, font1, solidBrush1, single1, (float)(pageBounds2.Bottom - this.PrintMargins.Right - size.Height));
							}
							return;
						}
						int num14 = 0;
						int num15 = 0;
						while (num15 < this.PartListTable.Columns.Count)
						{
							int num16 = 0;
							StringFormat stringFormat4 = new StringFormat();
							try
							{
								stringFormat4.Alignment = StringAlignment.Center;
								stringFormat4.LineAlignment = StringAlignment.Center;
								if (!this.bIsOldINIPL)
								{
									string str4 = this.dicPLColAlignments[this.PartListTable.Columns[num15].ColumnName.ToString()].ToString();
									char[] chrArray4 = new char[] { '|' };
									string str5 = str4.Split(chrArray4)[0].ToString();
									char[] chrArray5 = new char[] { '|' };
									num16 = int.Parse(str4.Split(chrArray5)[1].ToString());
									stringFormat1.Alignment = StringAlignment.Center;
									stringFormat1.LineAlignment = StringAlignment.Center;
									if (str5 == "L")
									{
										stringFormat4.Alignment = StringAlignment.Near;
										stringFormat1.Alignment = StringAlignment.Near;
									}
									else if (str5 == "R")
									{
										stringFormat4.Alignment = StringAlignment.Far;
										stringFormat1.Alignment = StringAlignment.Near;
									}
									num8 = num16;
								}
							}
							catch (Exception exception1)
							{
							}
							if (num14 + num8 > width)
							{
								num8 = num10;
								e.Graphics.DrawRectangle(Pens.Black, left1, num7, num8, num6);
								e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num15].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left1, (float)num7, (float)num8, (float)num6), stringFormat1);
								left1 += num8;
								break;
							}
							else
							{
								e.Graphics.DrawRectangle(Pens.Black, left1, num7, num8, num6);
								e.Graphics.DrawString(this.PartListTable.Rows[this.iPartsListRowCount][num15].ToString(), this.previewFont, Brushes.Black, new RectangleF((float)left1, (float)num7, (float)num8, (float)num6), stringFormat4);
								left1 += num8;
								num14 += num8;
								num15++;
							}
						}
						left1 = this.PrintMargins.Left;
						this.iPartsListRowCount++;
						num7 += num6;
					}
					if (this.objCurrentPrintJob.copyRightField != string.Empty)
					{
						Graphics graphics1 = e.Graphics;
						string str6 = this.objCurrentPrintJob.copyRightField;
						System.Drawing.Font font2 = this.previewFont;
						SolidBrush solidBrush2 = new SolidBrush(Color.Black);
						float width2 = (float)(left1 + (width - size.Width) / 2);
						Rectangle rectangle2 = e.PageBounds;
						graphics1.DrawString(str6, font2, solidBrush2, width2, (float)(rectangle2.Bottom - this.PrintMargins.Right - size.Height));
					}
					e.HasMorePages = false;
				}
				this.iPartsListRowCount = 0;
				if (!e.HasMorePages)
				{
					this.iPrintJobCounter++;
					this.iListCounter++;
				}
			}
			catch
			{
			}
		}

		private void doc_PrintSelList(object sender, PrintPageEventArgs e)
		{
			try
			{
				Rectangle pageBounds = e.PageBounds;
				this.iTotalPageWidth = pageBounds.Width - this.PrintMargins.Left - this.PrintMargins.Right;
				StringFormat stringFormat = new StringFormat()
				{
					FormatFlags = StringFormatFlags.NoWrap,
					Trimming = StringTrimming.Character
				};
				StringFormat stringFormat1 = stringFormat;
				if (this.dgSelList.Rows.Count > 0)
				{
					int num = 23;
					System.Drawing.Size size = new System.Drawing.Size(0, 0);
					int num1 = 0;
					int num2 = 0;
					int left = this.PrintMargins.Left;
					int top = this.PrintMargins.Top;
					int num3 = 0;
					for (int i = 0; i < this.dgSelList.Columns.Count; i++)
					{
						if (this.dgSelList.Columns[i].Visible)
						{
							num3++;
						}
					}
					Rectangle rectangle = e.PageBounds;
					int width = rectangle.Width - this.PrintMargins.Left - this.PrintMargins.Right;
					num2 = width / num3;
					if (this.objCurrentPrintJob.sPrintHeaderFooter == "1")
					{
						string resource = this.GetResource("Print Date", "PRINT_DATE", ResourceType.LABEL);
						DateTime now = DateTime.Now;
						string str = string.Concat(resource, " : ", now.ToString(this.objCurrentPrintJob.dateFormat), Environment.NewLine);
						System.Drawing.Size size1 = TextRenderer.MeasureText(str, this.previewFont);
						top += size1.Height;
						e.Graphics.DrawString(str, this.previewFont, new SolidBrush(Color.Black), (float)(left + (width - size1.Width) / 2), (float)(top - size1.Height));
					}
					top += 5;
					if (this.objCurrentPrintJob.copyRightField != string.Empty)
					{
						size = TextRenderer.MeasureText(this.copyRightField, this.previewFont);
					}
					int num4 = 0;
					int num5 = 100;
					foreach (KeyValuePair<string, string> dicSLColSetting in this.dicSLColSettings)
					{
						if (!dicSLColSetting.Key.ToString().EndsWith("_WIDTH"))
						{
							continue;
						}
						int num6 = Convert.ToInt32(dicSLColSetting.Value.ToString());
						if (num6 < num5)
						{
							num5 = num6;
						}
						num4 += num6;
					}
					int num7 = 0;
					int num8 = 0;
					foreach (KeyValuePair<string, string> keyValuePair in this.dicSLColSettings)
					{
						if (!keyValuePair.Key.ToString().EndsWith("_WIDTH"))
						{
							continue;
						}
						int num9 = Convert.ToInt32(keyValuePair.Value.ToString());
						if (width >= num7 + num9)
						{
							num7 += num9;
						}
						else
						{
							num8 = Math.Abs(width - num7);
							num7 += num9;
							break;
						}
					}
					if (!this.bIsOldINISL)
					{
						this.ResizeCellHeight(true, 0, ref num, this.dgSelList, e, this.objStringFormat, num5);
					}
					else
					{
						this.ResizeCellHeight(true, 0, ref num, this.dgSelList, e, this.objStringFormat, num2);
					}
					int num10 = 0;
					for (int j = 0; j < this.dgSelList.Columns.Count; j++)
					{
						StringFormat stringFormat2 = new StringFormat();
						if (this.dgSelList.Columns[j].Visible)
						{
							try
							{
								this.dicSLColSettings[string.Concat(this.dgSelList.Columns[j].Name.ToString(), "_ALIGN")].ToString();
								stringFormat2.Alignment = StringAlignment.Center;
								stringFormat2.LineAlignment = StringAlignment.Center;
								if (!this.bIsOldINISL)
								{
									stringFormat1.Alignment = StringAlignment.Center;
									stringFormat1.LineAlignment = StringAlignment.Center;
									int num11 = Convert.ToInt32(this.dicSLColSettings[string.Concat(this.dgSelList.Columns[j].Name.ToString(), "_WIDTH")].ToString());
									num2 = num11;
								}
							}
							catch (Exception exception)
							{
							}
							if (num10 + num2 > width)
							{
								if (num8 == 0)
								{
									num8 = 4;
								}
								num2 = num8;
								e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left, top, num2, num));
								e.Graphics.DrawRectangle(Pens.Black, left, top, num2, num);
								e.Graphics.DrawString(this.dgSelList.Columns[j].HeaderText, this.previewFont, Brushes.Black, new RectangleF((float)left, (float)top, (float)num2, (float)num), stringFormat1);
								left += num2;
								break;
							}
							else
							{
								e.Graphics.FillRectangle(Brushes.LightGray, new Rectangle(left, top, num2, num));
								e.Graphics.DrawRectangle(Pens.Black, left, top, num2, num);
								e.Graphics.DrawString(this.dgSelList.Columns[j].HeaderText, this.previewFont, Brushes.Black, new RectangleF((float)left, (float)top, (float)num2, (float)num), stringFormat2);
								left += num2;
								num10 += num2;
							}
						}
					}
					left = this.PrintMargins.Left;
					num1 = top + num;
					while (this.iSelListRowCount < this.dgSelList.Rows.Count)
					{
						if (!this.bIsOldINISL)
						{
							this.ResizeCellHeight(false, this.iSelListRowCount, ref num, this.dgSelList, e, this.objStringFormat, num5);
						}
						else
						{
							this.ResizeCellHeight(false, this.iSelListRowCount, ref num, this.dgSelList, e, this.objStringFormat, num2);
						}
						if (this.objCurrentPrintJob.sOrientation == "0")
						{
							if (num1 + num > this.PaperSize.Height - this.PrintMargins.Bottom - size.Height)
							{
								left = this.PrintMargins.Left;
								top = this.PrintMargins.Top;
								num1 = top;
								e.HasMorePages = true;
								if (this.objCurrentPrintJob.sOrientation != "1")
								{
									e.PageSettings.Landscape = false;
								}
								else
								{
									e.PageSettings.Landscape = true;
								}
								if (this.objCurrentPrintJob.copyRightField != string.Empty)
								{
									Graphics graphics = e.Graphics;
									string str1 = this.objCurrentPrintJob.copyRightField;
									System.Drawing.Font font = this.previewFont;
									SolidBrush solidBrush = new SolidBrush(Color.Black);
									float single = (float)(left + (width - size.Width) / 2);
									Rectangle pageBounds1 = e.PageBounds;
									graphics.DrawString(str1, font, solidBrush, single, (float)(pageBounds1.Bottom - this.PrintMargins.Right - size.Height));
								}
								return;
							}
						}
						else if (num1 + num > this.PaperSize.Width - this.PrintMargins.Left - size.Height)
						{
							left = this.PrintMargins.Left;
							top = this.PrintMargins.Top;
							num1 = top;
							e.HasMorePages = true;
							if (this.objCurrentPrintJob.sOrientation != "1")
							{
								e.PageSettings.Landscape = false;
							}
							else
							{
								e.PageSettings.Landscape = true;
							}
							if (this.objCurrentPrintJob.copyRightField != string.Empty)
							{
								Graphics graphic = e.Graphics;
								string str2 = this.objCurrentPrintJob.copyRightField;
								System.Drawing.Font font1 = this.previewFont;
								SolidBrush solidBrush1 = new SolidBrush(Color.Black);
								float width1 = (float)(left + (width - size.Width) / 2);
								Rectangle rectangle1 = e.PageBounds;
								graphic.DrawString(str2, font1, solidBrush1, width1, (float)(rectangle1.Bottom - this.PrintMargins.Right - size.Height));
							}
							return;
						}
						num10 = 0;
						for (int k = 0; k < this.dgSelList.Columns.Count; k++)
						{
							StringFormat stringFormat3 = new StringFormat();
							if (this.dgSelList.Columns[k].Visible)
							{
								try
								{
									string str3 = this.dicSLColSettings[string.Concat(this.dgSelList.Columns[k].Name.ToString(), "_ALIGN")].ToString();
									stringFormat3.Alignment = StringAlignment.Center;
									stringFormat3.LineAlignment = StringAlignment.Center;
									if (!this.bIsOldINISL)
									{
										stringFormat1.Alignment = StringAlignment.Center;
										stringFormat1.LineAlignment = StringAlignment.Center;
										if (str3 == "L")
										{
											stringFormat3.Alignment = StringAlignment.Near;
											stringFormat1.Alignment = StringAlignment.Near;
										}
										else if (str3 == "R")
										{
											stringFormat3.Alignment = StringAlignment.Far;
											stringFormat1.Alignment = StringAlignment.Far;
										}
										int num12 = Convert.ToInt32(this.dicSLColSettings[string.Concat(this.dgSelList.Columns[k].Name.ToString(), "_WIDTH")].ToString());
										num2 = num12;
									}
								}
								catch (Exception exception1)
								{
								}
								if (num10 + num2 > width)
								{
									num2 = num8;
									e.Graphics.DrawRectangle(Pens.Black, left, num1, num2, num);
									string empty = string.Empty;
									if (this.dgSelList.Rows[this.iSelListRowCount].Cells[k].Value != null)
									{
										empty = this.dgSelList.Rows[this.iSelListRowCount].Cells[k].Value.ToString();
									}
									e.Graphics.DrawString(empty, this.previewFont, Brushes.Black, new RectangleF((float)left, (float)num1, (float)num2, (float)num), stringFormat1);
									left += num2;
									break;
								}
								else
								{
									e.Graphics.DrawRectangle(Pens.Black, left, num1, num2, num);
									string empty1 = string.Empty;
									if (this.dgSelList.Rows[this.iSelListRowCount].Cells[k].Value != null)
									{
										empty1 = this.dgSelList.Rows[this.iSelListRowCount].Cells[k].Value.ToString();
									}
									e.Graphics.DrawString(empty1, this.previewFont, Brushes.Black, new RectangleF((float)left, (float)num1, (float)num2, (float)num), stringFormat3);
									left += num2;
									num10 += num2;
								}
							}
						}
						left = this.PrintMargins.Left;
						this.iSelListRowCount++;
						num1 += num;
					}
					if (this.objCurrentPrintJob.copyRightField != string.Empty)
					{
						Graphics graphics1 = e.Graphics;
						string str4 = this.objCurrentPrintJob.copyRightField;
						System.Drawing.Font font2 = this.previewFont;
						SolidBrush solidBrush2 = new SolidBrush(Color.Black);
						float single1 = (float)(left + (width - size.Width) / 2);
						Rectangle pageBounds2 = e.PageBounds;
						graphics1.DrawString(str4, font2, solidBrush2, single1, (float)(pageBounds2.Bottom - this.PrintMargins.Right - size.Height));
					}
					e.HasMorePages = false;
				}
			}
			catch
			{
			}
		}

		public int ExportImage(string sPicPath1, string sUserId1, string sPassword1, int iPageIndex, out string strExportedImagePath, int[] CurrentZoomFactors1)
		{
			int num = 1;
			DateTime now = DateTime.Now;
			this.strExportedImageName = now.ToLongTimeString().Replace(":", string.Empty);
			strExportedImagePath = string.Empty;
			this.objDjVuCtl.SetNameAndPass(sUserId1, sPassword1, 1, 0);
			this.objDjVuCtl.SRC = null;
			this.objDjVuCtl.SRC = sPicPath1;
			this.srcX = 0;
			this.srcY = 0;
			try
			{
				strExportedImagePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				strExportedImagePath = string.Concat(strExportedImagePath, "\\", Application.ProductName);
				strExportedImagePath = string.Concat(strExportedImagePath, "\\tmpPrint");
				if (!Directory.Exists(strExportedImagePath))
				{
					Directory.CreateDirectory(strExportedImagePath);
				}
				strExportedImagePath = string.Concat(strExportedImagePath, "\\tmpPrintImage_", this.strExportedImageName, ".jpg");
				if (this.objCurrentPrintJob.sMaintainZoom != "0")
				{
					bool flag = false;
					this.objDjVuCtl.Zoom = this.objCurrentPrintJob.sCurrentImageZoom;
					flag = this.IsExportedImageReq(this.objDjVuCtl.SRC, this.objDjVuCtl.Zoom, this.objDjVuCtl.Rotation, this.objCurrentPrintJob.CurrentZoomFactors);
					if (!File.Exists(GSPcLocalViewer.frmPrint.frmPrint.strExportImagePathJpg))
					{
						flag = true;
					}
					if (!flag)
					{
						strExportedImagePath = GSPcLocalViewer.frmPrint.frmPrint.strExportImagePathJpg;
						num = 0;
					}
					else
					{
						num = this.objDjVuCtl.ExportImageAs1(strExportedImagePath, "jpeg", false, iPageIndex, true, CurrentZoomFactors1[0], CurrentZoomFactors1[1], CurrentZoomFactors1[2], CurrentZoomFactors1[3], CurrentZoomFactors1[4], CurrentZoomFactors1[5], CurrentZoomFactors1[6], CurrentZoomFactors1[7]);
						GSPcLocalViewer.frmPrint.frmPrint.strExportImagePathJpg = strExportedImagePath;
						GSPcLocalViewer.frmPrint.frmPrint.strExortdImgPath = this.objDjVuCtl.SRC;
						GSPcLocalViewer.frmPrint.frmPrint.strExportdImgZoom = this.objDjVuCtl.Zoom;
						GSPcLocalViewer.frmPrint.frmPrint.intExportdImgRotationAngle = this.objDjVuCtl.Rotation;
						GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor = this.objCurrentPrintJob.CurrentZoomFactors;
					}
				}
				else
				{
					bool jPEG = false;
					if (!File.Exists(string.Concat(Application.StartupPath.ToString(), "\\DjVuDecoder.dll")))
					{
						num = this.objDjVuCtl.ExportImageAs1(strExportedImagePath, "jpeg", false, iPageIndex, false, CurrentZoomFactors1[0], CurrentZoomFactors1[1], CurrentZoomFactors1[2], CurrentZoomFactors1[3], CurrentZoomFactors1[4], CurrentZoomFactors1[5], CurrentZoomFactors1[6], CurrentZoomFactors1[7]);
					}
					else
					{
						bool flag1 = false;
						if (PreviewManager.IsDjVuSecured(sPicPath1) == 1)
						{
							PreviewManager.UnSecureDjVu(sPicPath1, sPicPath1, sUserId1, sPassword1);
							flag1 = true;
						}
						jPEG = PreviewManager.DjVuToJPEG(sPicPath1, strExportedImagePath);
						if (flag1)
						{
							PreviewManager.SecureDjVu(sPicPath1, sPicPath1, sUserId1, sPassword1);
						}
						num = (jPEG ? 0 : 1);
					}
				}
			}
			catch
			{
				num = 1;
			}
			return num;
		}

		private void ExportImageForSplitPrinting()
		{
			try
			{
				if (this.objCurrentPrintJob.pageSplitCount != 1)
				{
					this.sExportedImagePath = string.Empty;
					if (this.arrPrintJobs.Count > 0 && this.bPreviewImageNotExported)
					{
						this.bPreviewImageNotExported = false;
						this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
						this.arrPrintJobs.RemoveAt(0);
						this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
						string empty = string.Empty;
						empty = (!File.Exists(this.objCurrentPrintJob.sLocalPicPath) ? string.Concat(Application.StartupPath, "\\blank.djvu") : this.objCurrentPrintJob.sLocalPicPath);
						if (this.ExportImage(empty, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, this.iMultiImgCounter, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
						{
							this.imagetoPrint = Image.FromFile(this.sExportedImagePath);
						}
					}
				}
			}
			catch
			{
			}
		}

		private string GetCopyRightText(string sLanguage, string sDefaultCopyRightText)
		{
			string str;
			try
			{
				string empty = string.Empty;
				string str1 = "COPYRIGHT_FIELD";
				bool flag = false;
				string str2 = string.Concat(sLanguage, "_GSP_", this.sServerKey, ".ini");
				if (!File.Exists(string.Concat(Application.StartupPath, "\\Language XMLs\\", str2)))
				{
					empty = sDefaultCopyRightText;
				}
				else
				{
					TextReader streamReader = new StreamReader(string.Concat(Application.StartupPath, "\\Language XMLs\\", str2));
					while (true)
					{
						string str3 = streamReader.ReadLine();
						string str4 = str3;
						if (str3 == null)
						{
							break;
						}
						if (str4.ToUpper() == "[PRINTER_SETTINGS]")
						{
							flag = true;
						}
						else if (str4.Contains("=") && flag)
						{
							string[] strArrays = new string[] { "=" };
							string[] strArrays1 = str4.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
							if (strArrays1[0].ToString().ToUpper() == str1.ToUpper())
							{
								empty = strArrays1[1];
								flag = false;
								break;
							}
						}
						else if (str4.Contains("["))
						{
							flag = false;
						}
					}
					if (empty == string.Empty)
					{
						empty = sDefaultCopyRightText;
					}
					streamReader.Close();
				}
				str = empty;
			}
			catch
			{
				str = sDefaultCopyRightText;
			}
			return str;
		}

		private string GetDGHeaderCellValue(string sKey, string sDefaultHeaderValue)
		{
			string str;
			try
			{
				string empty = string.Empty;
				bool flag = false;
				if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
				{
					empty = sDefaultHeaderValue;
				}
				else
				{
					string str1 = string.Concat(Settings.Default.appLanguage, "_GSP_", this.sServerKey, ".ini");
					if (!File.Exists(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1)))
					{
						empty = sDefaultHeaderValue;
					}
					else
					{
						TextReader streamReader = new StreamReader(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1));
						while (true)
						{
							string str2 = streamReader.ReadLine();
							string str3 = str2;
							if (str2 == null)
							{
								break;
							}
							if (str3.ToUpper() == "[PLIST_SETTINGS]")
							{
								flag = true;
							}
							else if (str3.Contains("=") && flag)
							{
								string[] strArrays = new string[] { "=" };
								string[] strArrays1 = str3.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
								if (strArrays1[0].ToString().ToUpper() == sKey.ToUpper())
								{
									empty = strArrays1[1];
									flag = false;
									break;
								}
							}
							else if (str3.Contains("["))
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
				str = empty;
			}
			catch
			{
				str = sDefaultHeaderValue;
			}
			return str;
		}

		public bool GetInternetConnectionStatus()
		{
			if (string.IsNullOrEmpty(this.sServerKey))
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini");
				if (File.Exists(str))
				{
					IniFileIO iniFileIO = new IniFileIO();
					string lower = iniFileIO.GetKeyValue("SETTINGS", "CONTENT_PATH", str).ToLower();
					WebRequest webRequest = WebRequest.Create(string.Concat(lower, "/DataUpdate.XML"));
					HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
					if (response == null || response.StatusCode != HttpStatusCode.OK)
					{
						return true;
					}
				}
			}
			return false;
		}

		private float GetMemoPrintFontSize()
		{
			float single = 0f;
			try
			{
				if (Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_TEXT_SIZE"] == null)
				{
					single = 0f;
				}
				else
				{
					string str = Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_TEXT_SIZE"].ToString();
					single = float.Parse(str);
					if (single < 6f || single > 16f)
					{
						single = 0f;
					}
				}
			}
			catch (Exception exception)
			{
				single = 0f;
			}
			return single;
		}

		private int GetMemoPrintLines()
		{
			int num = 0;
			try
			{
				if (Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"] == null)
				{
					num = 5;
				}
				else
				{
					string str = Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"].ToString();
					num = int.Parse(str);
					if (num < 1 || num > 5)
					{
						num = 5;
					}
				}
			}
			catch (Exception exception)
			{
				num = 5;
			}
			return num;
		}

		private string GetMemoValue(string strValue, int intHalfWidthLimit, int intFullWidthLimit)
		{
			string str;
			try
			{
				int num = 0;
				string empty = string.Empty;
				bool flag = false;
				bool flag1 = false;
				char[] charArray = new char[strValue.Length];
				charArray = strValue.ToCharArray();
				for (int i = 0; i < (int)charArray.Length; i++)
				{
					if ((int)Encoding.UTF8.GetBytes(charArray[i].ToString()).Length <= 1)
					{
						num++;
						flag1 = true;
					}
					else
					{
						num += 2;
						flag = true;
					}
					if (flag1 && flag)
					{
						if (num <= intHalfWidthLimit)
						{
							empty = string.Concat(empty, charArray[i]);
						}
					}
					else if (flag1 && !flag)
					{
						if (num <= intHalfWidthLimit)
						{
							empty = string.Concat(empty, charArray[i]);
						}
					}
					else if (!flag1 && flag && num <= intHalfWidthLimit)
					{
						empty = string.Concat(empty, charArray[i]);
					}
				}
				str = empty;
			}
			catch (Exception exception)
			{
				str = null;
			}
			return str;
		}

		private void GetMultiRangeVales()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini");
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
						this.bMultiRange = false;
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
		}

		public void GetPaperSize(string PageName)
		{
			if (this.PaperSize != null && this.PaperSize.PaperName == PageName)
			{
				return;
			}
			PrintDocument printDocument = new PrintDocument();
			for (int i = 0; i < printDocument.PrinterSettings.PaperSizes.Count; i++)
			{
				if (printDocument.PrinterSettings.PaperSizes[i].PaperName.Contains(PageName))
				{
					this.PaperSize = printDocument.PrinterSettings.PaperSizes[i];
					return;
				}
			}
		}

		private string GetPicLocalMemos(string strPageId, int intPicIndex)
		{
			string str;
			string empty = string.Empty;
			string empty1 = string.Empty;
			XmlDocument xmlDocument = new XmlDocument();
			XmlNodeList xmlNodeLists = null;
			string memoValue = string.Empty;
			DateTime dateTime = Convert.ToDateTime("01/01/1900 12:00:00");
			try
			{
				empty = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				string productName = Application.ProductName;
				empty = string.Concat(empty, "\\", "GSPcLocalViewer");
				empty = string.Concat(empty, "\\", this.sServerKey);
				empty = string.Concat(empty, "\\LocalMemo.xml");
				if (File.Exists(empty))
				{
					xmlDocument.Load(empty);
					string innerXml = xmlDocument.InnerXml;
					object[] objArray = new object[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookCode, "'][@PageId='", strPageId, "'][@PicIndex='", intPicIndex, "']" };
					xmlNodeLists = xmlDocument.SelectNodes(string.Concat(objArray));
				}
				foreach (XmlNode xmlNodes in xmlNodeLists)
				{
					XmlAttribute itemOf = xmlNodes.Attributes["PartNo"];
					XmlAttribute xmlAttribute = xmlNodes.Attributes["Value"];
					XmlAttribute itemOf1 = xmlNodes.Attributes["Type"];
					XmlAttribute xmlAttribute1 = xmlNodes.Attributes["Update"];
					string str1 = itemOf1.Value.ToString();
					string str2 = itemOf.Value.ToString();
					string str3 = Convert.ToString(xmlAttribute1.Value);
					DateTime dateTime1 = DateTime.ParseExact(str3.Trim(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					if (!(str2 == string.Empty) || !(str1.ToUpper() == "TXT") || !(dateTime1 >= dateTime))
					{
						continue;
					}
					memoValue = xmlAttribute.Value.ToString();
					dateTime = dateTime1;
				}
				if (!string.IsNullOrEmpty(memoValue) && !this.MemoLinesExist())
				{
					memoValue = this.GetMemoValue(memoValue, 80, 40);
				}
				return memoValue;
			}
			catch (Exception exception)
			{
				str = string.Concat(memoValue, Environment.NewLine);
			}
			return str;
		}

		private int GetPicMemoDrawingHeight(string strMemoVal, System.Drawing.Font objMemoFont)
		{
			int height;
			System.Drawing.Size size = new System.Drawing.Size();
			try
			{
				size = TextRenderer.MeasureText(strMemoVal, this.MemoFont);
				int width = this.PaperSize.Width - this.PrintMargins.Left - this.PrintMargins.Right;
				int num = size.Height;
				for (int i = width; i < size.Width; i += width)
				{
					num += size.Height;
				}
				if (num > size.Height * this.MemoPrintLines)
				{
					num = size.Height * this.MemoPrintLines;
				}
				height = num;
			}
			catch (Exception exception)
			{
				height = size.Height * this.MemoPrintLines;
			}
			return height;
		}

		private int GetPicMemoDrawingHeight(string strMemoVal, string strTrimedMemo, System.Drawing.Font objMemoFont, int intPrintWidth)
		{
			int height;
			System.Drawing.Size size = new System.Drawing.Size();
			System.Drawing.Size size1 = new System.Drawing.Size();
			System.Drawing.Size height1 = new System.Drawing.Size();
			try
			{
				height1 = TextRenderer.MeasureText(strTrimedMemo, this.MemoFont);
				size1 = TextRenderer.MeasureText(strMemoVal, this.MemoFont);
				if (size1.Height >= height1.Height * this.MemoPrintLines)
				{
					height1.Height = height1.Height * this.MemoPrintLines;
					size = height1;
					height = size.Height;
				}
				else
				{
					size = TextRenderer.MeasureText(strMemoVal, this.MemoFont);
					int num = height1.Height;
					int num1 = intPrintWidth;
					if (size.Width <= num1)
					{
						num = size.Height;
					}
					else
					{
						while (num1 < size.Width)
						{
							num += height1.Height;
							num1 += intPrintWidth;
						}
						if (num > height1.Height * this.MemoPrintLines)
						{
							num = height1.Height * this.MemoPrintLines;
						}
					}
					height = num;
				}
			}
			catch (Exception exception)
			{
				height = height1.Height * this.MemoPrintLines;
			}
			return height;
		}

		public static Image GetResizedImage(Image img, Rectangle rect)
		{
			Image image;
			Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
			Graphics graphic = Graphics.FromImage(bitmap);
			graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphic.DrawImage(img, 0, 0, rect.Width, rect.Height);
			graphic.Dispose();
			try
			{
				image = (Image)bitmap.Clone();
			}
			finally
			{
				bitmap.Dispose();
				bitmap = null;
				graphic = null;
			}
			return image;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string empty = string.Empty;
				empty = string.Concat(empty, "/Screen[@Name='PRINT']");
				empty = string.Concat(empty, "/Screen[@Name='PRINT_MANAGER']");
				if (rType != ResourceType.TITLE)
				{
					if (rType == ResourceType.LABEL)
					{
						empty = string.Concat(empty, "/Resources[@Name='LABEL']");
					}
					else if (rType == ResourceType.BUTTON)
					{
						empty = string.Concat(empty, "/Resources[@Name='BUTTON']");
					}
					else if (rType == ResourceType.CHECK_BOX)
					{
						empty = string.Concat(empty, "/Resources[@Name='CHECK_BOX']");
					}
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						empty = string.Concat(empty, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						empty = string.Concat(empty, "/Resources[@Name='STATUS_MESSAGE']");
					}
					else if (rType == ResourceType.COMBO_BOX)
					{
						empty = string.Concat(empty, "/Resources[@Name='COMBO_BOX']");
					}
					else if (rType == ResourceType.GRID_VIEW)
					{
						empty = string.Concat(empty, "/Resources[@Name='GRID_VIEW']");
					}
					else if (rType == ResourceType.LIST_VIEW)
					{
						empty = string.Concat(empty, "/Resources[@Name='LIST_VIEW']");
					}
					else if (rType == ResourceType.MENU_BAR)
					{
						empty = string.Concat(empty, "/Resources[@Name='MENU_BAR']");
					}
					else if (rType == ResourceType.RADIO_BUTTON)
					{
						empty = string.Concat(empty, "/Resources[@Name='RADIO_BUTTON']");
					}
					else if (rType == ResourceType.CONTEXT_MENU)
					{
						empty = string.Concat(empty, "/Resources[@Name='CONTEXT_MENU']");
					}
					else if (rType == ResourceType.TOOLSTRIP)
					{
						empty = string.Concat(empty, "/Resources[@Name='TOOLSTRIP']");
					}
					empty = string.Concat(empty, "/Resource[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, empty);
				}
				else
				{
					empty = string.Concat(empty, "[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, empty);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PreviewManager));
			this.objDjVuCtl = new AxDjVuCtrl();
			this.printPreviewDialog1 = new PrintPreviewDialog();
			((ISupportInitialize)this.objDjVuCtl).BeginInit();
			base.SuspendLayout();
			this.objDjVuCtl.Dock = DockStyle.Fill;
			this.objDjVuCtl.Enabled = true;
			this.objDjVuCtl.Location = new Point(0, 0);
			this.objDjVuCtl.Name = "objDjVuCtl";
			this.objDjVuCtl.OcxState = (AxHost.State)componentResourceManager.GetObject("objDjVuCtl.OcxState");
			this.objDjVuCtl.Size = new System.Drawing.Size(292, 273);
			this.objDjVuCtl.TabIndex = 29;
			this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
			this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
			this.printPreviewDialog1.Enabled = true;
			this.printPreviewDialog1.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("printPreviewDialog1.Icon");
			this.printPreviewDialog1.Name = "printPreviewDialog1";
			this.printPreviewDialog1.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(292, 273);
			base.Controls.Add(this.objDjVuCtl);
			base.Name = "PreviewManager";
			this.Text = "PreviewManager";
			base.FormClosing += new FormClosingEventHandler(this.PreviewManager_FormClosing);
			((ISupportInitialize)this.objDjVuCtl).EndInit();
			base.ResumeLayout(false);
		}

		private void InitializePartsList(XmlNode schemaNode)
		{
			try
			{
				IniFileIO iniFileIO = new IniFileIO();
				string empty = string.Empty;
				empty = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini");
				this.PartListTable = new DataTable("Data");
				this.PartListTable.Rows.Clear();
				this.PartListTable.Columns.Clear();
				ArrayList arrayLists = new ArrayList();
				List<string> strs = new List<string>();
				arrayLists = iniFileIO.GetKeys(empty, "PLIST_SETTINGS");
				for (int i = 0; i < arrayLists.Count; i++)
				{
					int num = 0;
					while (num < schemaNode.Attributes.Count)
					{
						if (arrayLists[i].ToString().ToUpper() != schemaNode.Attributes[num].Value.ToUpper())
						{
							num++;
						}
						else
						{
							string str = string.Empty;
							str = schemaNode.Attributes[num].LocalName.ToString();
							string empty1 = string.Empty;
							empty1 = arrayLists[i].ToString();
							strs.Add(string.Concat(str, ',', empty1));
							break;
						}
					}
				}
				if (this.dicPLColSettings.Keys.Count > 0)
				{
					this.dicPLColSettings.Clear();
				}
				if (this.dicPLColAlignments.Keys.Count > 0)
				{
					this.dicPLColAlignments.Clear();
				}
				for (int j = 0; j < strs.Count; j++)
				{
					string str1 = strs[j].ToString();
					string[] strArrays = str1.Split(new char[] { ',' });
					string str2 = strArrays[0].ToString();
					string str3 = strArrays[1].ToString();
					string keyValue = iniFileIO.GetKeyValue("PLIST_SETTINGS", str3.ToUpper(), empty);
					char[] chrArray = new char[] { '|' };
					if ((int)keyValue.Split(chrArray).Length != 3)
					{
						char[] chrArray1 = new char[] { '|' };
						if ((int)keyValue.Split(chrArray1).Length == 4)
						{
							string[] strArrays1 = new string[] { keyValue, "|True|True|", null, null, null };
							char[] chrArray2 = new char[] { '|' };
							strArrays1[2] = keyValue.Split(chrArray2)[1];
							strArrays1[3] = "|";
							char[] chrArray3 = new char[] { '|' };
							strArrays1[4] = keyValue.Split(chrArray3)[2];
							keyValue = string.Concat(strArrays1);
						}
					}
					else
					{
						string[] strArrays2 = new string[] { keyValue, "|True|True|", null, null, null };
						char[] chrArray4 = new char[] { '|' };
						strArrays2[2] = keyValue.Split(chrArray4)[1];
						strArrays2[3] = "|";
						char[] chrArray5 = new char[] { '|' };
						strArrays2[4] = keyValue.Split(chrArray5)[2];
						keyValue = string.Concat(strArrays2);
						this.bIsOldINIPL = true;
					}
					char[] chrArray6 = new char[] { '|' };
					if (keyValue.Split(chrArray6)[4] == "True")
					{
						string str4 = keyValue.Substring(keyValue.LastIndexOf('|') + 1);
						if (keyValue != null && keyValue != string.Empty && str4 != "0")
						{
							string dGHeaderCellValue = string.Empty;
							string empty2 = string.Empty;
							string empty3 = string.Empty;
							string empty4 = string.Empty;
							try
							{
								string[] strArrays3 = keyValue.Split(new char[] { '|' });
								char[] chrArray7 = new char[] { '|' };
								string str5 = keyValue.Split(chrArray7)[0];
								if ((int)strArrays3.Length == 7)
								{
									char[] chrArray8 = new char[] { '|' };
									empty2 = keyValue.Split(chrArray8)[5].ToString();
									char[] chrArray9 = new char[] { '|' };
									empty4 = keyValue.Split(chrArray9)[6].ToString();
								}
								else if ((int)strArrays3.Length != 8)
								{
									empty2 = "C";
								}
								else
								{
									char[] chrArray10 = new char[] { '|' };
									empty2 = keyValue.Split(chrArray10)[6].ToString();
									char[] chrArray11 = new char[] { '|' };
									empty4 = keyValue.Split(chrArray11)[7].ToString();
								}
								if (keyValue != null && keyValue != string.Empty && empty4 != "0")
								{
									dGHeaderCellValue = this.GetDGHeaderCellValue(str3.ToUpper(), keyValue.Substring(0, keyValue.IndexOf("|")));
									this.PartListTable.Columns.Add(dGHeaderCellValue);
									this.attributeNames.Add(str2);
								}
								this.dicPLColAlignments.Add(dGHeaderCellValue, string.Concat(empty2, "|", empty4));
								this.dicPLColSettings.Add(string.Concat(dGHeaderCellValue, "_ALIGN"), empty2);
								this.dicPLColSettings.Add(string.Concat(dGHeaderCellValue, "_WIDTH"), empty4);
							}
							catch (Exception exception)
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

		public void InitializePrintDocObject()
		{
			try
			{
				if (this.imagetoPrint == null)
				{
					try
					{
						if (this.objCurrentPrintJob.sOrientation != "1")
						{
							this.printDocList.DefaultPageSettings.Landscape = false;
						}
						else
						{
							this.printDocList.DefaultPageSettings.Landscape = true;
						}
						if (this.objCurrentPrintJob.sZoom == "0" || this.objCurrentPrintJob.sLocalListPath == string.Empty)
						{
							if (this.objCurrentPrintJob.pageSplitCount != 1)
							{
								if (this.objCurrentPrintJob.pageSplitCount != 2 && this.objCurrentPrintJob.pageSplitCount != 8)
								{
									if (this.objCurrentPrintJob.pageSplitCount == 4)
									{
										if (this.objDjVuCtl.OrigWidth > this.objDjVuCtl.OrigHeight)
										{
											this.printDocImg.DefaultPageSettings.Landscape = true;
										}
										else if (this.objDjVuCtl.OrigWidth < this.objDjVuCtl.OrigHeight)
										{
											this.printDocImg.DefaultPageSettings.Landscape = false;
										}
										else if (this.objDjVuCtl.OrigHeight == this.objDjVuCtl.OrigWidth)
										{
											if (this.sOrientation != "0")
											{
												this.printDocImg.DefaultPageSettings.Landscape = true;
											}
											else
											{
												this.printDocImg.DefaultPageSettings.Landscape = false;
											}
										}
									}
								}
								else if (this.objDjVuCtl.OrigWidth < this.objDjVuCtl.OrigHeight)
								{
									this.printDocImg.DefaultPageSettings.Landscape = true;
								}
								else if (this.objDjVuCtl.OrigWidth > this.objDjVuCtl.OrigHeight)
								{
									this.printDocImg.DefaultPageSettings.Landscape = false;
								}
								else if (this.objDjVuCtl.OrigHeight == this.objDjVuCtl.OrigWidth)
								{
									if (this.sOrientation != "0")
									{
										this.printDocImg.DefaultPageSettings.Landscape = false;
									}
									else
									{
										this.printDocImg.DefaultPageSettings.Landscape = true;
									}
								}
							}
							else if (this.spaperUtilization == "0" && this.objDjVuCtl.OrigHeight < this.objDjVuCtl.OrigWidth && this.sOrientation == "1")
							{
								this.printDocImg.DefaultPageSettings.Landscape = true;
							}
							else if (this.spaperUtilization == "0" && this.objDjVuCtl.OrigHeight < this.objDjVuCtl.OrigWidth && this.sOrientation == "0")
							{
								this.printDocImg.DefaultPageSettings.Landscape = false;
							}
							else if (this.spaperUtilization == "0" && this.objDjVuCtl.OrigHeight > this.objDjVuCtl.OrigWidth && this.sOrientation == "1")
							{
								this.printDocImg.DefaultPageSettings.Landscape = true;
							}
							else if (this.spaperUtilization == "0" && this.objDjVuCtl.OrigHeight > this.objDjVuCtl.OrigWidth && this.sOrientation == "0")
							{
								this.printDocImg.DefaultPageSettings.Landscape = false;
							}
							else if (this.objCurrentPrintJob.spaperUtilization == "1" && this.objDjVuCtl.OrigHeight == this.objDjVuCtl.OrigWidth)
							{
								if (this.sOrientation != "0")
								{
									this.printDocImg.DefaultPageSettings.Landscape = true;
								}
								else
								{
									this.printDocImg.DefaultPageSettings.Landscape = false;
								}
							}
							else if (!(this.objCurrentPrintJob.spaperUtilization == "1") || this.objDjVuCtl.OrigHeight >= this.objDjVuCtl.OrigWidth)
							{
								this.printDocImg.DefaultPageSettings.Landscape = false;
							}
							else
							{
								this.printDocImg.DefaultPageSettings.Landscape = true;
							}
						}
					}
					catch
					{
					}
					try
					{
						if (this.objCurrentPrintJob.sOrientation != "1")
						{
							this.printDocSList.DefaultPageSettings.Landscape = false;
						}
						else
						{
							this.printDocSList.DefaultPageSettings.Landscape = true;
						}
						if (this.objCurrentPrintJob.sOrientation != "1")
						{
							this.printDocHalfPage.DefaultPageSettings.Landscape = false;
						}
						else
						{
							this.printDocHalfPage.DefaultPageSettings.Landscape = true;
						}
					}
					catch
					{
					}
				}
				else
				{
					try
					{
						if (this.objCurrentPrintJob.sOrientation != "1")
						{
							this.printDocList.DefaultPageSettings.Landscape = false;
						}
						else
						{
							this.printDocList.DefaultPageSettings.Landscape = true;
						}
						if (this.objCurrentPrintJob.sZoom == "0" || this.objCurrentPrintJob.sLocalListPath == string.Empty)
						{
							if (this.objCurrentPrintJob.pageSplitCount != 1)
							{
								if (this.objCurrentPrintJob.pageSplitCount != 2 && this.objCurrentPrintJob.pageSplitCount != 8)
								{
									if (this.objCurrentPrintJob.pageSplitCount == 4)
									{
										if (this.imagetoPrint.Width > this.imagetoPrint.Height)
										{
											this.printDocImg.DefaultPageSettings.Landscape = true;
										}
										else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
										{
											if (this.sOrientation != "0")
											{
												this.printDocImg.DefaultPageSettings.Landscape = true;
											}
											else
											{
												this.printDocImg.DefaultPageSettings.Landscape = false;
											}
										}
									}
								}
								else if (this.imagetoPrint.Width < this.imagetoPrint.Height)
								{
									this.printDocImg.DefaultPageSettings.Landscape = true;
								}
								else if (this.imagetoPrint.Height == this.imagetoPrint.Width)
								{
									if (this.sOrientation != "0")
									{
										this.printDocImg.DefaultPageSettings.Landscape = false;
									}
									else
									{
										this.printDocImg.DefaultPageSettings.Landscape = true;
									}
								}
							}
							else if (this.spaperUtilization == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width && this.sOrientation == "1")
							{
								this.printDocImg.DefaultPageSettings.Landscape = true;
							}
							else if (this.spaperUtilization == "0" && this.imagetoPrint.Height < this.imagetoPrint.Width && this.sOrientation == "0")
							{
								this.printDocImg.DefaultPageSettings.Landscape = false;
							}
							else if (this.spaperUtilization == "0" && this.imagetoPrint.Height > this.imagetoPrint.Width && this.sOrientation == "1")
							{
								this.printDocImg.DefaultPageSettings.Landscape = true;
							}
							else if (this.spaperUtilization == "0" && this.imagetoPrint.Height > this.imagetoPrint.Width && this.sOrientation == "0")
							{
								this.printDocImg.DefaultPageSettings.Landscape = false;
							}
							else if (this.objCurrentPrintJob.spaperUtilization == "1" && this.imagetoPrint.Height == this.imagetoPrint.Width)
							{
								if (this.sOrientation != "0")
								{
									this.printDocImg.DefaultPageSettings.Landscape = true;
								}
								else
								{
									this.printDocImg.DefaultPageSettings.Landscape = false;
								}
							}
							else if (this.objCurrentPrintJob.spaperUtilization == "0" && this.imagetoPrint.Height == this.imagetoPrint.Width)
							{
								if (this.sOrientation != "0")
								{
									this.printDocImg.DefaultPageSettings.Landscape = true;
								}
								else
								{
									this.printDocImg.DefaultPageSettings.Landscape = false;
								}
							}
							else if (!(this.objCurrentPrintJob.spaperUtilization == "1") || this.imagetoPrint.Height >= this.imagetoPrint.Width)
							{
								this.printDocImg.DefaultPageSettings.Landscape = false;
							}
							else
							{
								this.printDocImg.DefaultPageSettings.Landscape = true;
							}
						}
					}
					catch
					{
					}
					try
					{
						if (this.objCurrentPrintJob.sOrientation != "1")
						{
							this.printDocSList.DefaultPageSettings.Landscape = false;
						}
						else
						{
							this.printDocSList.DefaultPageSettings.Landscape = true;
						}
						if (this.objCurrentPrintJob.sOrientation != "1")
						{
							this.printDocHalfPage.DefaultPageSettings.Landscape = false;
						}
						else
						{
							this.printDocHalfPage.DefaultPageSettings.Landscape = true;
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

		private void InitializePrintVariables(PreviewManager.PrintJob objPrintJob1)
		{
			int[] numArray;
			this.GetPaperSize(objPrintJob1.sPaperSize);
			this.printDocImg = new PrintDocument()
			{
				DocumentName = "image"
			};
			this.printDocImg.PrintPage += new PrintPageEventHandler(this.doc_PrintImage);
			this.printDocImg.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
			this.PaperSize = this.printDocImg.DefaultPageSettings.PaperSize;
			this.printDocImg.DefaultPageSettings.Margins = this.PrintMargins;
			this.printDocImg.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
			this.printDocList = new PrintDocument()
			{
				DocumentName = "list"
			};
			this.printDocList.PrintPage += new PrintPageEventHandler(this.doc_PrintList);
			this.printDocList.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
			this.PaperSize = this.printDocImg.DefaultPageSettings.PaperSize;
			this.printDocList.DefaultPageSettings.Margins = this.PrintMargins;
			this.printDocList.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
			this.printDocSList = new PrintDocument()
			{
				DocumentName = "sList"
			};
			this.printDocSList.PrintPage += new PrintPageEventHandler(this.doc_PrintSelList);
			this.printDocSList.PrinterSettings.PrinterName = objPrintJob1.sPrinterName;
			this.PaperSize = this.printDocSList.DefaultPageSettings.PaperSize;
			this.printDocSList.DefaultPageSettings.Margins = this.PrintMargins;
			this.printDocSList.PrinterSettings.DefaultPageSettings.Margins = this.PrintMargins;
			if (!(objPrintJob1.sMaintainZoom == "1") || !(this.objCurrentPrintJob.sPrintRng == "0"))
			{
				objPrintJob1.sZoomFactor.Split(new char[] { '-' });
				numArray = new int[8];
			}
			else
			{
				string[] strArrays = objPrintJob1.sZoomFactor.Split(new char[] { ',' });
				numArray = new int[(int)strArrays.Length];
				numArray[0] = int.Parse(strArrays[0]);
				numArray[1] = int.Parse(strArrays[1]);
				numArray[2] = int.Parse(strArrays[2]);
				numArray[3] = int.Parse(strArrays[3]);
				numArray[4] = int.Parse(strArrays[4]);
				numArray[5] = int.Parse(strArrays[5]);
				numArray[6] = int.Parse(strArrays[6]);
				numArray[7] = int.Parse(strArrays[7]);
			}
			objPrintJob1.CurrentZoomFactors = numArray;
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

		[DllImport("DjVuDecoder.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern int IsDjVuSecured(string source);

		private bool IsExportedImageReq(string strCurImgPath, string strCurZoomLevel, int intCurRotationAngle, int[] arrCurZoomFactor)
		{
			bool flag;
			try
			{
				flag = (!(GSPcLocalViewer.frmPrint.frmPrint.strExortdImgPath == strCurImgPath) || !(GSPcLocalViewer.frmPrint.frmPrint.strExportdImgZoom == strCurZoomLevel) || GSPcLocalViewer.frmPrint.frmPrint.intExportdImgRotationAngle != intCurRotationAngle || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[0] != arrCurZoomFactor[0] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[1] != arrCurZoomFactor[1] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[2] != arrCurZoomFactor[2] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[3] != arrCurZoomFactor[3] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[4] != arrCurZoomFactor[4] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[5] != arrCurZoomFactor[5] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[6] != arrCurZoomFactor[6] || GSPcLocalViewer.frmPrint.frmPrint.arrExportdImgZoomFactor[7] != arrCurZoomFactor[7] ? true : false);
			}
			catch (Exception exception)
			{
				flag = true;
			}
			return flag;
		}

		private bool LoadBookXml(string sBookPath)
		{
			bool flag;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				DirectoryInfo directoryInfo = new DirectoryInfo(sBookPath);
				string empty = string.Empty;
				this.sBookCode = directoryInfo.Name;
				empty = string.Concat(sBookPath, "\\", this.sBookCode, ".xml");
				if (!File.Exists(empty))
				{
					flag = false;
				}
				else
				{
					try
					{
						xmlDocument.Load(empty);
					}
					catch
					{
						flag = false;
						return flag;
					}
					if (this.sEncryption == "1")
					{
						string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = str;
					}
					this.objXmlSchemaNode = xmlDocument.SelectSingleNode("//Schema");
					if (this.objXmlSchemaNode != null)
					{
						string name = string.Empty;
						this.attPgNameElement = string.Empty;
						name = string.Empty;
						foreach (XmlAttribute attribute in this.objXmlSchemaNode.Attributes)
						{
							if (attribute.Value.ToUpper().Equals("ID"))
							{
								name = attribute.Name;
							}
							else if (attribute.Value.ToUpper().Equals("PAGENAME"))
							{
								this.attPgNameElement = attribute.Name;
							}
							else if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
							{
								this.attPicElement = attribute.Name;
							}
							else if (attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
							{
								this.attListElement = attribute.Name;
							}
							else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
							{
								this.attUpdateDateElement = attribute.Name;
							}
							else if (!attribute.Value.ToUpper().Equals("UPDATEDATEPIC"))
							{
								if (!attribute.Value.ToUpper().Equals("UPDATEDATEPL"))
								{
									continue;
								}
								this.attUpdateDatePLElement = attribute.Name;
							}
							else
							{
								this.attUpdateDatePICElement = attribute.Name;
							}
						}
						if (name == string.Empty || this.attPgNameElement == string.Empty)
						{
							flag = false;
						}
						else
						{
							if (!this.bMuliRageKey || !this.bMultiRange)
							{
								if (!(this.sRngStart != "-1") || !(this.sRngEnd != "-1"))
								{
									this.objXmlNodeList = xmlDocument.SelectNodes("//Pg");
								}
								else
								{
									string[] strArrays = new string[] { "//Pg[@", name, ">='", this.sRngStart, "'][@", name, "<='", this.sRngEnd, "']" };
									this.objXmlNodeList = xmlDocument.SelectNodes(string.Concat(strArrays));
								}
							}
							else if (!string.IsNullOrEmpty(this.strMultiRngStart) && !string.IsNullOrEmpty(this.strMultiRngEnd))
							{
								this.lstMultiRange = new List<XmlNodeList>();
								string[] strArrays1 = this.strMultiRngStart.Split(new char[] { '*' });
								string[] strArrays2 = this.strMultiRngEnd.Split(new char[] { '*' });
								for (int i = 0; i < (int)strArrays1.Length; i++)
								{
									if (strArrays1[i].Trim() != string.Empty && strArrays2[i].Trim() != string.Empty)
									{
										string[] strArrays3 = new string[] { "//Pg[@", name, ">='", strArrays1[i], "'][@", name, "<='", strArrays2[i], "']" };
										this.objXmlNodeList = xmlDocument.SelectNodes(string.Concat(strArrays3));
										this.lstMultiRange.Add(this.objXmlNodeList);
									}
								}
							}
							else if (!(this.sRngStart != "-1") || !(this.sRngEnd != "-1"))
							{
								this.objXmlNodeList = xmlDocument.SelectNodes("//Pg");
							}
							else
							{
								string[] strArrays4 = new string[] { "//Pg[@", name, ">='", this.sRngStart, "'][@", name, "<='", this.sRngEnd, "']" };
								this.objXmlNodeList = xmlDocument.SelectNodes(string.Concat(strArrays4));
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

		public void LoadPartsListData(string partListFilePath)
		{
			try
			{
				if (this.objCurrentPrintJob.sCompression == "1")
				{
					partListFilePath = partListFilePath.ToLower().Replace(".zip", ".xml");
				}
				XmlDocument xmlDocument = new XmlDocument();
				this.attributeNames = new ArrayList();
				if (!File.Exists(partListFilePath) && this.objCurrentPrintJob.sCompression == "1")
				{
					try
					{
						if (File.Exists(partListFilePath.ToLower().Replace(".xml", ".zip")))
						{
							Global.Unzip(partListFilePath.ToLower().Replace(".xml", ".zip"));
						}
					}
					catch
					{
						return;
					}
				}
				if (File.Exists(partListFilePath))
				{
					xmlDocument.Load(partListFilePath);
					if (this.objCurrentPrintJob.sEncryption == "1")
					{
						string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = str;
					}
					XmlElement documentElement = xmlDocument.DocumentElement;
					this.InitializePartsList(xmlDocument.SelectSingleNode("//Schema"));
					if (this.PartListTable.Columns.Count > 0)
					{
						XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//Part");
						for (int i = 0; i < xmlNodeLists.Count; i++)
						{
							DataRow dataRow = this.PartListTable.NewRow();
							for (int j = 0; j < this.attributeNames.Count; j++)
							{
								if (xmlNodeLists[i].Attributes[this.attributeNames[j].ToString()] != null)
								{
									dataRow[j] = xmlNodeLists[i].Attributes[this.attributeNames[j].ToString()].Value.ToString();
								}
							}
							this.PartListTable.Rows.Add(dataRow);
						}
					}
				}
			}
			catch
			{
			}
		}

		private bool MemoLinesExist()
		{
			int num = 0;
			bool flag = false;
			try
			{
				if (Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"] == null)
				{
					flag = false;
				}
				else
				{
					string str = Program.iniServers[this.frmParent.p_ServerId].items["PRINTER_SETTINGS", "PICTURE_MEMO_MAX_LINES"].ToString();
					num = int.Parse(str);
					flag = true;
					if (num < 1 || num > 5)
					{
						flag = false;
					}
				}
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		private void PreviewManager_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				try
				{
					this.objDjVuCtl.SRC = string.Empty;
					try
					{
						if (this.imagetoPrint != null)
						{
							this.imagetoPrint.Dispose();
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
			finally
			{
				this.objDjVuCtl.SRC = string.Empty;
				this.objDjVuCtl.Dispose();
			}
		}

		public void PrintJobRecieved(string[] args)
		{
			string empty;
			int[] numArray;
			int[] numArray1;
			string[] strArrays;
			DateTime now;
			char[] chrArray;
			try
			{
				if ((int)args.Length == 3)
				{
					this.SetArguments(args);
					if (this.sPrintRng == "1")
					{
						this.GetMultiRangeVales();
						this.SetMultiRangeAruguments();
					}
					this.ReadSelectionList();
					if (this.LoadBookXml(this.sBookPath))
					{
						if (string.IsNullOrEmpty(this.strMultiRngStart) || string.IsNullOrEmpty(this.strMultiRngEnd))
						{
							int count = this.objXmlNodeList.Count;
							foreach (XmlNode xmlNodes in this.objXmlNodeList)
							{
								count--;
								empty = string.Empty;
								int num = 1;
								string picLocalMemos = string.Empty;
								foreach (XmlNode childNode in xmlNodes.ChildNodes)
								{
									if (this.bPrintPicMemo)
									{
										try
										{
											string str = xmlNodes.Attributes[0].Value.ToString();
											picLocalMemos = this.GetPicLocalMemos(str, num);
											num++;
										}
										catch (Exception exception)
										{
										}
									}
									string empty1 = string.Empty;
									if ((int)args.Length != 4)
									{
										string.Concat(this.sBookCode, " >> ");
									}
									else
									{
										string.Concat(empty, this.sBookCode, " >> ");
									}
									string value = string.Empty;
									string value1 = string.Empty;
									string str1 = string.Empty;
									string empty2 = string.Empty;
									string value2 = string.Empty;
									this.sLocalListPath = string.Empty;
									this.sServerListPath = string.Empty;
									this.sLocalPicPath = string.Empty;
									this.sServerPicPath = string.Empty;
									if (childNode.Name.ToUpper() != "PIC")
									{
										continue;
									}
									if (childNode.Attributes[this.attPicElement] != null)
									{
										empty2 = childNode.Attributes[this.attPicElement].Value;
									}
									if (childNode.Attributes[this.attListElement] != null)
									{
										value2 = childNode.Attributes[this.attListElement].Value;
									}
									try
									{
										XmlElement parentNode = (XmlElement)childNode.ParentNode;
										empty = string.Empty;
										while (parentNode.Name.ToUpper() != "BOOK")
										{
											if (parentNode.Attributes[this.attPgNameElement] != null)
											{
												empty = (empty != string.Empty ? string.Concat(parentNode.Attributes[this.attPgNameElement].Value, " >> ", empty) : parentNode.Attributes[this.attPgNameElement].Value);
											}
											if (parentNode.ParentNode == null)
											{
												continue;
											}
											parentNode = (XmlElement)parentNode.ParentNode;
										}
										empty = string.Concat(this.sBookCode, " >> ", empty, Environment.NewLine);
									}
									catch
									{
										empty = string.Concat(this.sBookCode, " >> ", Environment.NewLine);
									}
									if (childNode.Attributes[this.attUpdateDatePICElement] != null)
									{
										value1 = childNode.Attributes[this.attUpdateDatePICElement].Value;
									}
									if (childNode.Attributes[this.attUpdateDatePLElement] != null)
									{
										str1 = childNode.Attributes[this.attUpdateDatePLElement].Value;
									}
									if (childNode.Attributes[this.attUpdateDateElement] != null)
									{
										value = childNode.Attributes[this.attUpdateDateElement].Value;
										try
										{
											if (str1 == string.Empty)
											{
												str1 = value;
											}
										}
										catch
										{
										}
									}
									if (value1 != null && value1 != string.Empty)
									{
										value = value1;
									}
									if (empty2 != string.Empty)
									{
										this.sLocalPicPath = string.Empty;
										this.sServerPicPath = string.Empty;
										this.sLocalPicPath = string.Concat(this.sBookPath, "\\", empty2);
										strArrays = new string[] { this.sContentPath, "/", this.sBookCode, "/", empty2 };
										this.sServerPicPath = string.Concat(strArrays);
									}
									if (value2 != string.Empty)
									{
										this.sLocalListPath = string.Empty;
										this.sServerListPath = string.Empty;
										if (this.sCompression != "1")
										{
											this.sLocalListPath = string.Concat(this.sBookPath, "\\", value2, ".xml");
											strArrays = new string[] { this.sContentPath, "/", this.sBookCode, "/", value2, ".xml" };
											this.sServerListPath = string.Concat(strArrays);
										}
										else
										{
											this.sLocalListPath = string.Concat(this.sBookPath, "\\", value2, ".zip");
											strArrays = new string[] { this.sContentPath, "/", this.sBookCode, "/", value2, ".zip" };
											this.sServerListPath = string.Concat(strArrays);
										}
									}
									DateTime dateTime = new DateTime();
									try
									{
										dateTime = (value != string.Empty ? DateTime.Parse(value, new CultureInfo("en-GB", false)) : DateTime.Now);
									}
									catch
									{
										now = DateTime.Now;
										string str2 = now.ToString(this.dateFormat);
										DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo()
										{
											ShortDatePattern = this.dateFormat
										};
										dateTime = Convert.ToDateTime(str2, dateTimeFormatInfo);
									}
									DateTime dateTime1 = new DateTime();
									DateTime dateTime2 = new DateTime();
									try
									{
										dateTime1 = (value1 != string.Empty ? DateTime.Parse(value1, new CultureInfo("en-GB", false)) : DateTime.Now);
									}
									catch
									{
										now = DateTime.Now;
										string str3 = now.ToString(this.dateFormat);
										DateTimeFormatInfo dateTimeFormatInfo1 = new DateTimeFormatInfo()
										{
											ShortDatePattern = this.dateFormat
										};
										dateTime1 = Convert.ToDateTime(str3, dateTimeFormatInfo1);
									}
									try
									{
										dateTime2 = (str1 != string.Empty ? DateTime.Parse(str1, new CultureInfo("en-GB", false)) : DateTime.Now);
									}
									catch
									{
										now = DateTime.Now;
										string str4 = now.ToString(this.dateFormat);
										DateTimeFormatInfo dateTimeFormatInfo2 = new DateTimeFormatInfo()
										{
											ShortDatePattern = this.dateFormat
										};
										dateTime2 = Convert.ToDateTime(str4, dateTimeFormatInfo2);
									}
									PreviewManager.PrintJob printJob = new PreviewManager.PrintJob(this, this.sPrinterName, this.sOrientation, this.sPaperSize, this.sUserId, this.sPassword, this.sSplittingOption, this.sPrintHeaderFooter, empty, dateTime, dateTime1, dateTime2, this.sPrintPgNos, this.sZoom, this.sCurrentImageZoom, this.sMaintainZoom, this.sZoomFactor, this.pageSplitCount, this.sPrintPic, this.sPrintList, this.sPrintSelList, this.sPrintSideBySide, this.sPrintRng, this.sBookType, this.sLocalListPath, this.sServerListPath, this.sLocalPicPath, this.sServerPicPath, this.spaperUtilization, this.sProxyType, this.sProxyIP, this.sProxyPort, this.sProxyLogin, this.sProxyPassword, this.sTimeOut, this.sCompression, this.sEncryption, this.listColSequence, this.dateFormat, this.copyRightField, this.sLanguage, picLocalMemos);
									if (!(printJob.sMaintainZoom == "1") || !(printJob.sPrintRng == "0"))
									{
										string str5 = printJob.sZoomFactor;
										chrArray = new char[] { '-' };
										str5.Split(chrArray);
										numArray1 = new int[8];
									}
									else
									{
										string str6 = printJob.sZoomFactor;
										chrArray = new char[] { ',' };
										string[] strArrays1 = str6.Split(chrArray);
										numArray1 = new int[(int)strArrays1.Length];
										numArray1[0] = int.Parse(strArrays1[0]);
										numArray1[1] = int.Parse(strArrays1[1]);
										numArray1[2] = int.Parse(strArrays1[2]);
										numArray1[3] = int.Parse(strArrays1[3]);
										numArray1[4] = int.Parse(strArrays1[4]);
										numArray1[5] = int.Parse(strArrays1[5]);
										numArray1[6] = int.Parse(strArrays1[6]);
										numArray1[7] = int.Parse(strArrays1[7]);
									}
									printJob.CurrentZoomFactors = numArray1;
									if (this.strDuplicatePrinting.ToUpper() != "OFF")
									{
										int num1 = Convert.ToInt32(xmlNodes.Attributes[0].Value.ToString());
										if (this.sPreviousPicName == printJob.sLocalPicPath && num1 == this.intPreviousPageId)
										{
											this.sPreviousPicName = printJob.sLocalPicPath;
											this.intPreviousPageId = num1;
										}
										else if (!this.bOfflineMode)
										{
											this.arrPrintJobs.Add(printJob);
											this.sPreviousPicName = printJob.sLocalPicPath;
											this.intPreviousPageId = num1;
										}
										else
										{
											if (!File.Exists(printJob.sLocalPicPath))
											{
												printJob.sLocalPicPath = string.Empty;
											}
											if (!File.Exists(printJob.sLocalListPath))
											{
												printJob.sLocalListPath = string.Empty;
											}
											if (printJob.sLocalListPath != string.Empty || printJob.sLocalPicPath != string.Empty)
											{
												this.arrPrintJobs.Add(printJob);
												this.sPreviousPicName = printJob.sLocalPicPath;
												this.intPreviousPageId = num1;
											}
											else
											{
												if (count != 0 || this.arrPrintJobs.Count != 0 || !(printJob.sPrintSelList == "1") || this.dgSelList == null || this.dgSelList.RowCount == 0)
												{
													continue;
												}
												this.arrPrintJobs.Add(printJob);
												this.sPreviousPicName = printJob.sLocalPicPath;
												this.intPreviousPageId = num1;
											}
										}
									}
									else if (this.sPreviousPicName == printJob.sLocalPicPath)
									{
										this.sPreviousPicName = printJob.sLocalPicPath;
									}
									else
									{
										this.arrPrintJobs.Add(printJob);
										this.sPreviousPicName = printJob.sLocalPicPath;
									}
								}
							}
						}
						else
						{
							for (int i = 0; i < this.lstMultiRange.Count; i++)
							{
								this.objXmlNodeList = this.lstMultiRange[i];
								int count1 = this.objXmlNodeList.Count;
								foreach (XmlNode xmlNodes1 in this.objXmlNodeList)
								{
									count1--;
									empty = string.Empty;
									int num2 = 1;
									string picLocalMemos1 = string.Empty;
									foreach (XmlNode childNode1 in xmlNodes1.ChildNodes)
									{
										if (this.bPrintPicMemo)
										{
											try
											{
												string str7 = xmlNodes1.Attributes[0].Value.ToString();
												picLocalMemos1 = this.GetPicLocalMemos(str7, num2);
												num2++;
											}
											catch (Exception exception1)
											{
											}
										}
										string empty3 = string.Empty;
										if ((int)args.Length != 4)
										{
											string.Concat(this.sBookCode, " >> ");
										}
										else
										{
											string.Concat(empty, this.sBookCode, " >> ");
										}
										string value3 = string.Empty;
										string empty4 = string.Empty;
										string value4 = string.Empty;
										string empty5 = string.Empty;
										string value5 = string.Empty;
										this.sLocalListPath = string.Empty;
										this.sServerListPath = string.Empty;
										this.sLocalPicPath = string.Empty;
										this.sServerPicPath = string.Empty;
										if (childNode1.Name.ToUpper() != "PIC")
										{
											continue;
										}
										if (childNode1.Attributes[this.attPicElement] != null)
										{
											empty5 = childNode1.Attributes[this.attPicElement].Value;
										}
										if (childNode1.Attributes[this.attListElement] != null)
										{
											value5 = childNode1.Attributes[this.attListElement].Value;
										}
										try
										{
											XmlElement xmlElement = (XmlElement)childNode1.ParentNode;
											empty = string.Empty;
											while (xmlElement.Name.ToUpper() != "BOOK")
											{
												if (xmlElement.Attributes[this.attPgNameElement] != null)
												{
													empty = (empty != string.Empty ? string.Concat(xmlElement.Attributes[this.attPgNameElement].Value, " >> ", empty) : xmlElement.Attributes[this.attPgNameElement].Value);
												}
												if (xmlElement.ParentNode == null)
												{
													continue;
												}
												xmlElement = (XmlElement)xmlElement.ParentNode;
											}
											empty = string.Concat(this.sBookCode, " >> ", empty, Environment.NewLine);
										}
										catch
										{
											empty = string.Concat(this.sBookCode, " >> ", Environment.NewLine);
										}
										if (childNode1.Attributes[this.attUpdateDatePICElement] != null)
										{
											empty4 = childNode1.Attributes[this.attUpdateDatePICElement].Value;
										}
										if (childNode1.Attributes[this.attUpdateDatePLElement] != null)
										{
											value4 = childNode1.Attributes[this.attUpdateDatePLElement].Value;
										}
										if (childNode1.Attributes[this.attUpdateDateElement] != null)
										{
											value3 = childNode1.Attributes[this.attUpdateDateElement].Value;
											try
											{
												if (value4 == string.Empty)
												{
													value4 = value3;
												}
											}
											catch
											{
											}
										}
										if (empty4 != null && empty4 != string.Empty)
										{
											value3 = empty4;
										}
										if (empty5 != string.Empty)
										{
											this.sLocalPicPath = string.Empty;
											this.sServerPicPath = string.Empty;
											this.sLocalPicPath = string.Concat(this.sBookPath, "\\", empty5);
											strArrays = new string[] { this.sContentPath, "/", this.sBookCode, "/", empty5 };
											this.sServerPicPath = string.Concat(strArrays);
										}
										if (value5 != string.Empty)
										{
											this.sLocalListPath = string.Empty;
											this.sServerListPath = string.Empty;
											if (this.sCompression != "1")
											{
												this.sLocalListPath = string.Concat(this.sBookPath, "\\", value5, ".xml");
												string[] strArrays2 = new string[] { this.sContentPath, "/", this.sBookCode, "/", value5, ".xml" };
												this.sServerListPath = string.Concat(strArrays2);
											}
											else
											{
												this.sLocalListPath = string.Concat(this.sBookPath, "\\", value5, ".zip");
												string[] strArrays3 = new string[] { this.sContentPath, "/", this.sBookCode, "/", value5, ".zip" };
												this.sServerListPath = string.Concat(strArrays3);
											}
										}
										DateTime dateTime3 = new DateTime();
										try
										{
											dateTime3 = (value3 != string.Empty ? DateTime.Parse(value3, new CultureInfo("en-GB", false)) : DateTime.Now);
										}
										catch
										{
											now = DateTime.Now;
											string str8 = now.ToString(this.dateFormat);
											DateTimeFormatInfo dateTimeFormatInfo3 = new DateTimeFormatInfo()
											{
												ShortDatePattern = this.dateFormat
											};
											dateTime3 = Convert.ToDateTime(str8, dateTimeFormatInfo3);
										}
										DateTime dateTime4 = new DateTime();
										DateTime dateTime5 = new DateTime();
										try
										{
											dateTime4 = (empty4 != string.Empty ? DateTime.Parse(empty4, new CultureInfo("en-GB", false)) : DateTime.Now);
										}
										catch
										{
											string str9 = DateTime.Now.ToString(this.dateFormat);
											DateTimeFormatInfo dateTimeFormatInfo4 = new DateTimeFormatInfo()
											{
												ShortDatePattern = this.dateFormat
											};
											dateTime4 = Convert.ToDateTime(str9, dateTimeFormatInfo4);
										}
										try
										{
											dateTime5 = (value4 != string.Empty ? DateTime.Parse(value4, new CultureInfo("en-GB", false)) : DateTime.Now);
										}
										catch
										{
											string str10 = DateTime.Now.ToString(this.dateFormat);
											DateTimeFormatInfo dateTimeFormatInfo5 = new DateTimeFormatInfo()
											{
												ShortDatePattern = this.dateFormat
											};
											dateTime5 = Convert.ToDateTime(str10, dateTimeFormatInfo5);
										}
										PreviewManager.PrintJob printJob1 = new PreviewManager.PrintJob(this, this.sPrinterName, this.sOrientation, this.sPaperSize, this.sUserId, this.sPassword, this.sSplittingOption, this.sPrintHeaderFooter, empty, dateTime3, dateTime4, dateTime5, this.sPrintPgNos, this.sZoom, this.sCurrentImageZoom, this.sMaintainZoom, this.sZoomFactor, this.pageSplitCount, this.sPrintPic, this.sPrintList, this.sPrintSelList, this.sPrintSideBySide, this.sPrintRng, this.sBookType, this.sLocalListPath, this.sServerListPath, this.sLocalPicPath, this.sServerPicPath, this.spaperUtilization, this.sProxyType, this.sProxyIP, this.sProxyPort, this.sProxyLogin, this.sProxyPassword, this.sTimeOut, this.sCompression, this.sEncryption, this.listColSequence, this.dateFormat, this.copyRightField, this.sLanguage, picLocalMemos1);
										if (!(printJob1.sMaintainZoom == "1") || !(printJob1.sPrintRng == "0"))
										{
											string str11 = printJob1.sZoomFactor;
											chrArray = new char[] { '-' };
											str11.Split(chrArray);
											numArray = new int[8];
										}
										else
										{
											string str12 = printJob1.sZoomFactor;
											chrArray = new char[] { ',' };
											string[] strArrays4 = str12.Split(chrArray);
											numArray = new int[(int)strArrays4.Length];
											numArray[0] = int.Parse(strArrays4[0]);
											numArray[1] = int.Parse(strArrays4[1]);
											numArray[2] = int.Parse(strArrays4[2]);
											numArray[3] = int.Parse(strArrays4[3]);
											numArray[4] = int.Parse(strArrays4[4]);
											numArray[5] = int.Parse(strArrays4[5]);
											numArray[6] = int.Parse(strArrays4[6]);
											numArray[7] = int.Parse(strArrays4[7]);
										}
										printJob1.CurrentZoomFactors = numArray;
										if (this.strDuplicatePrinting.ToUpper() != "OFF")
										{
											int num3 = Convert.ToInt32(xmlNodes1.Attributes[0].Value.ToString());
											if (this.sPreviousPicName == printJob1.sLocalPicPath && num3 == this.intPreviousPageId)
											{
												this.sPreviousPicName = printJob1.sLocalPicPath;
												this.intPreviousPageId = num3;
											}
											else if (!this.bOfflineMode)
											{
												this.arrPrintJobs.Add(printJob1);
												this.sPreviousPicName = printJob1.sLocalPicPath;
												this.intPreviousPageId = num3;
											}
											else
											{
												if (!File.Exists(printJob1.sLocalPicPath))
												{
													printJob1.sLocalPicPath = string.Empty;
												}
												if (!File.Exists(printJob1.sLocalListPath))
												{
													printJob1.sLocalListPath = string.Empty;
												}
												if (printJob1.sLocalListPath != string.Empty || printJob1.sLocalPicPath != string.Empty)
												{
													this.arrPrintJobs.Add(printJob1);
													this.sPreviousPicName = printJob1.sLocalPicPath;
													this.intPreviousPageId = num3;
												}
												else
												{
													if (count1 != 0 || this.arrPrintJobs.Count != 0 || !(printJob1.sPrintSelList == "1") || this.dgSelList == null || this.dgSelList.RowCount == 0)
													{
														continue;
													}
													this.arrPrintJobs.Add(printJob1);
													this.sPreviousPicName = printJob1.sLocalPicPath;
													this.intPreviousPageId = num3;
												}
											}
										}
										else if (this.sPreviousPicName == printJob1.sLocalPicPath)
										{
											this.sPreviousPicName = printJob1.sLocalPicPath;
										}
										else
										{
											this.arrPrintJobs.Add(printJob1);
											this.sPreviousPicName = printJob1.sLocalPicPath;
										}
									}
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

		public void PrintManager()
		{
			CustomPrintPreviewDialog customPrintPreviewDialog = new CustomPrintPreviewDialog(this);
			if (this.arrPrintJobs.Count > 0)
			{
				this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
				this.arrPrintJobs.RemoveAt(0);
				if (this.objCurrentPrintJob.sPrintPic == "1" && !File.Exists(this.objCurrentPrintJob.sLocalPicPath) && this.objCurrentPrintJob.sLocalPicPath != string.Empty)
				{
					Download download = new Download();
					download.DownloadFile(this.objCurrentPrintJob.sServerPicPath, this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sProxyType, this.objCurrentPrintJob.sProxyIP, this.objCurrentPrintJob.sProxyPort, this.objCurrentPrintJob.sProxyLogin, this.objCurrentPrintJob.sProxyPassword, this.objCurrentPrintJob.sTimeOut);
				}
				this.InitializePrintVariables(this.objCurrentPrintJob);
				if (this.objCurrentPrintJob.sPrintPic == "1" || this.objCurrentPrintJob.sPrintList == "1" || this.objCurrentPrintJob.sPrintSelList == "1")
				{
					this.objDjVuCtl.SetNameAndPass(this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, 1, 0);
					this.objDjVuCtl.SRC = this.objCurrentPrintJob.sLocalPicPath;
					if (this.arrPrintJobs.Count > 0)
					{
						this.objParentPrintDlg.objPreviewProcessingDlg.Opacity = 100;
					}
					int pageCount = this.objDjVuCtl.GetPageCount();
					for (int i = 1; i <= pageCount; i++)
					{
						if (this.objCurrentPrintJob.sPrintPic == "1" && this.objCurrentPrintJob.sLocalPicPath != string.Empty)
						{
							try
							{
								if (this.sMaintainZoom == "1" && File.Exists(this.objCurrentPrintJob.sLocalPicPath) && this.ExportImage(this.objCurrentPrintJob.sLocalPicPath, this.objCurrentPrintJob.sUserId, this.objCurrentPrintJob.sPassword, i, out this.sExportedImagePath, this.objCurrentPrintJob.CurrentZoomFactors) == 0)
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
					if (this.objCurrentPrintJob.sZoom == "0" && this.objCurrentPrintJob.sPrintList == "1" && this.objCurrentPrintJob.sLocalListPath != string.Empty)
					{
						this.InitializePrintDocObject();
						this.lstDocuments.Add(this.printDocList);
						if (this.objCurrentPrintJob.sPrintPic == "0" && this.objCurrentPrintJob.sPrintList == "1")
						{
							this.lstPrintJob.Add(this.objCurrentPrintJob);
						}
					}
				}
			}
			if (this.objCurrentPrintJob.sPrintList == "0" && this.objCurrentPrintJob.sPrintPic == "1" && this.objCurrentPrintJob.sPrintHeaderFooter == "0" && this.objCurrentPrintJob.strPicMemoValue == string.Empty)
			{
				if (this.arrPrintJobs.Count > 0)
				{
					this.PrintManager();
					return;
				}
				try
				{
					if (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
					{
						this.lstDocuments.Add(this.printDocSList);
					}
					this.objMultiPrintDocument = new PreviewManager.MultiPrintDocument(this.lstDocuments, this);
					if (this.lstDocuments.Count > 0)
					{
						string printerName = this.lstDocuments[0].PrinterSettings.PrinterName;
						if (!string.IsNullOrEmpty(printerName))
						{
							this.objMultiPrintDocument.PrinterSettings.PrinterName = printerName;
						}
					}
					customPrintPreviewDialog.Document = this.objMultiPrintDocument;
					customPrintPreviewDialog.ShowDialog(this.frmParent);
				}
				catch (Exception exception)
				{
				}
				base.Close();
				return;
			}
			if (this.arrPrintJobs.Count > 0)
			{
				this.PrintManager();
				return;
			}
			try
			{
				if (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
				{
					this.InitializePrintDocObject();
					this.lstDocuments.Add(this.printDocSList);
				}
				this.objMultiPrintDocument = new PreviewManager.MultiPrintDocument(this.lstDocuments, this);
				if (this.lstDocuments.Count > 0)
				{
					string str = this.lstDocuments[0].PrinterSettings.PrinterName;
					if (!string.IsNullOrEmpty(str))
					{
						this.objMultiPrintDocument.PrinterSettings.PrinterName = str;
					}
				}
				customPrintPreviewDialog.Document = this.objMultiPrintDocument;
				customPrintPreviewDialog.ShowDialog(this.frmParent);
			}
			catch (Exception exception1)
			{
			}
			base.Close();
		}

		private void PrintSelectionList()
		{
			try
			{
				if (this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
				{
					this.iSelListRowCount = 0;
					this.printDocSList.Print();
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
				if (this.sPrintSelList == "1")
				{
					this.dgSelList = new DataGridView();
					foreach (DataGridViewColumn column in this.frmParent.objFrmSelectionlist.dgPartslist.Columns)
					{
						this.dgSelList.Columns.Add(column.Clone() as DataGridViewColumn);
					}
					foreach (DataGridViewRow row in (IEnumerable)this.frmParent.objFrmSelectionlist.dgPartslist.Rows)
					{
						int value = this.dgSelList.Rows.Add(row.Clone() as DataGridViewRow);
						foreach (DataGridViewCell cell in row.Cells)
						{
							this.dgSelList.Rows[value].Cells[cell.ColumnIndex].Value = cell.Value;
						}
					}
					for (int i = 0; i < this.dgSelList.Columns.Count; i++)
					{
						IniFileIO iniFileIO = new IniFileIO();
						ArrayList arrayLists = new ArrayList();
						string empty = string.Empty;
						string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini");
						arrayLists = iniFileIO.GetKeys(str, "SLIST_SETTINGS");
						int num = 0;
						while (num < arrayLists.Count)
						{
							if (arrayLists[num].ToString() != this.dgSelList.Columns[i].Tag.ToString())
							{
								num++;
							}
							else
							{
								this.dgSelList.Columns[i].Visible = true;
								empty = iniFileIO.GetKeyValue("SLIST_SETTINGS", arrayLists[num].ToString().ToUpper(), str);
								char[] chrArray = new char[] { '|' };
								if ((int)empty.Split(chrArray).Length != 3)
								{
									char[] chrArray1 = new char[] { '|' };
									if ((int)empty.Split(chrArray1).Length == 4)
									{
										string[] strArrays = new string[] { empty, "|True|True|", null, null, null };
										char[] chrArray2 = new char[] { '|' };
										strArrays[2] = empty.Split(chrArray2)[1];
										strArrays[3] = "|";
										char[] chrArray3 = new char[] { '|' };
										strArrays[4] = empty.Split(chrArray3)[2];
										empty = string.Concat(strArrays);
									}
								}
								else
								{
									string[] strArrays1 = new string[] { empty, "|True|True|", null, null, null };
									char[] chrArray4 = new char[] { '|' };
									strArrays1[2] = empty.Split(chrArray4)[1];
									strArrays1[3] = "|";
									char[] chrArray5 = new char[] { '|' };
									strArrays1[4] = empty.Split(chrArray5)[2];
									empty = string.Concat(strArrays1);
									this.bIsOldINISL = true;
								}
								string[] strArrays2 = empty.Split(new char[] { '|' });
								if (strArrays2[4].ToString().ToUpper() != "FALSE")
								{
									if (strArrays2[4].ToString().ToUpper() != "TRUE")
									{
										break;
									}
									try
									{
										char[] chrArray6 = new char[] { '|' };
										empty.Split(chrArray6)[0].ToString();
										string[] strArrays3 = empty.Split(new char[] { '|' });
										string str1 = strArrays3[5].ToString();
										string str2 = strArrays3[6].ToString();
										if (Convert.ToInt32(str2) <= 0)
										{
											this.dgSelList.Columns[i].Visible = false;
										}
										else
										{
											this.dicSLColSettings.Add(string.Concat(this.dgSelList.Columns[i].Name, "_ALIGN"), str1);
											this.dicSLColSettings.Add(string.Concat(this.dgSelList.Columns[i].Name, "_WIDTH"), str2);
										}
										break;
									}
									catch (Exception exception)
									{
										break;
									}
								}
								else
								{
									this.dgSelList.Columns[i].Visible = false;
									break;
								}
							}
						}
					}
					this.dgSelList.AllowUserToAddRows = false;
				}
			}
			catch
			{
			}
		}

		public void ResizeCellHeight(bool bHeader, int iRowIndex, ref int iCellHeight, DataGridView dTGrid, PrintPageEventArgs e, StringFormat strFormat, int iColWidth)
		{
			SizeF sizeF;
			try
			{
				if (!bHeader)
				{
					iCellHeight = 23;
				}
				int num = 0;
				for (int i = 0; i < dTGrid.Columns.Count; i++)
				{
					int num1 = iColWidth;
					if (!this.bIsOldINISL)
					{
						foreach (KeyValuePair<string, string> dicSLColSetting in this.dicSLColSettings)
						{
							if (!dicSLColSetting.Key.ToString().EndsWith("_WIDTH"))
							{
								continue;
							}
							string str = string.Concat(dTGrid.Columns[i].Name.ToUpper(), "_WIDTH");
							if (dicSLColSetting.Key.ToUpper() != str)
							{
								continue;
							}
							num1 = Convert.ToInt32(dicSLColSetting.Value.ToString());
							num += num1;
							if (num <= this.iTotalPageWidth)
							{
								break;
							}
							return;
						}
					}
					if (dTGrid.Columns[i].Visible)
					{
						if (!bHeader)
						{
							sizeF = (dTGrid.Rows[iRowIndex].Cells[i].Value == null || !(dTGrid.Rows[iRowIndex].Cells[i].Value.ToString() != string.Empty) ? e.Graphics.MeasureString(string.Empty, this.previewFont, num1, strFormat) : e.Graphics.MeasureString(dTGrid.Rows[iRowIndex].Cells[i].Value.ToString(), this.previewFont, num1, strFormat));
						}
						else
						{
							sizeF = e.Graphics.MeasureString(dTGrid.Columns[i].HeaderText, this.previewFont, num1, strFormat);
						}
						if ((float)iCellHeight <= sizeF.Height)
						{
							do
							{
								iCellHeight += 23;
							}
							while ((float)iCellHeight <= sizeF.Height);
						}
					}
				}
			}
			catch
			{
			}
		}

		public void ResizeCellHeight(bool bHeader, int iRowIndex, ref int iCellHeight, DataTable dTable, PrintPageEventArgs e, StringFormat strFormat, int iColWidth)
		{
			SizeF sizeF;
			try
			{
				if (!bHeader)
				{
					iCellHeight = 23;
				}
				int num = 0;
				for (int i = 0; i < dTable.Columns.Count; i++)
				{
					int num1 = iColWidth;
					if (!this.bIsOldINIPL)
					{
						foreach (KeyValuePair<string, string> dicPLColAlignment in this.dicPLColAlignments)
						{
							if (dicPLColAlignment.Key.ToUpper() != dTable.Columns[i].ColumnName.ToUpper())
							{
								continue;
							}
							string value = dicPLColAlignment.Value;
							char[] chrArray = new char[] { '|' };
							num1 = Convert.ToInt32(value.Split(chrArray)[1]);
							num += num1;
							if (num <= this.iTotalPageWidth)
							{
								break;
							}
							return;
						}
					}
					if (!bHeader)
					{
						sizeF = (dTable.Rows[iRowIndex][i] == null || !(dTable.Rows[iRowIndex][i].ToString() != string.Empty) ? e.Graphics.MeasureString(string.Empty, this.previewFont, num1, strFormat) : e.Graphics.MeasureString(dTable.Rows[iRowIndex][i].ToString(), this.previewFont, num1, strFormat));
					}
					else
					{
						sizeF = e.Graphics.MeasureString(dTable.Columns[i].Caption, this.previewFont, num1, strFormat);
					}
					if ((float)iCellHeight <= sizeF.Height)
					{
						do
						{
							iCellHeight += 23;
						}
						while ((float)iCellHeight <= sizeF.Height);
					}
				}
			}
			catch
			{
			}
		}

		[DllImport("DjVuDecoder.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern int SecureDjVu(string source, string destination, string userId, string password);

		public void SetArguments(string[] args)
		{
			try
			{
				IniFileIO iniFileIO = new IniFileIO();
				string empty = string.Empty;
				string[] strArrays = args[0].Split(new char[] { '|' });
				this.sBookPath = args[1];
				this.sServerKey = args[2];
				empty = string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini");
				this.sPrinterName = strArrays[0];
				this.sPaperSize = strArrays[1];
				this.sSplittingOption = strArrays[2];
				this.sPrintHeaderFooter = strArrays[3];
				this.sPrintPgNos = strArrays[4];
				this.sOrientation = strArrays[5];
				this.sZoom = strArrays[6];
				this.sCurrentImageZoom = strArrays[7];
				this.sMaintainZoom = strArrays[8];
				this.sZoomFactor = strArrays[9];
				this.sPrintRng = strArrays[10];
				this.sRngStart = strArrays[11];
				this.sRngEnd = strArrays[12];
				this.sPrintPic = strArrays[13];
				this.sPrintList = strArrays[14];
				this.sPrintSelList = strArrays[15];
				this.sPrintSideBySide = strArrays[16];
				this.sProxyType = strArrays[17];
				this.sProxyIP = strArrays[18];
				this.sProxyPort = strArrays[19];
				this.sProxyLogin = strArrays[20];
				this.sProxyPassword = strArrays[21];
				this.sTimeOut = strArrays[22];
				this.sBookType = strArrays[23];
				this.copyrightPrinitng = strArrays[24];
				this.sLanguage = strArrays[25];
				this.sUserId = strArrays[26];
				this.sPassword = strArrays[27];
				this.bPrintPicMemo = Convert.ToBoolean(strArrays[28]);
				this.spaperUtilization = strArrays[29];
				if ((new ApplicationMode()).bWorkingOffline || Program.objAppFeatures.bDcMode)
				{
					this.bOfflineMode = true;
				}
				else if (!Program.objAppMode.bWorkingOffline)
				{
					string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					folderPath = string.Concat(folderPath, "\\", Application.ProductName);
					if (!Directory.Exists(folderPath))
					{
						Directory.CreateDirectory(folderPath);
					}
					folderPath = string.Concat(folderPath, "\\DataUpdate.XML");
					empty = string.Concat(Application.StartupPath, "\\GSP_", this.sServerKey, ".ini");
					string lower = iniFileIO.GetKeyValue("SETTINGS", "CONTENT_PATH", empty).ToLower();
					lower = string.Concat(lower, "/DataUpdate.XML");
					try
					{
						if (File.Exists(folderPath))
						{
							File.Delete(folderPath);
						}
					}
					catch (Exception exception)
					{
						File.Delete(folderPath);
					}
					(new Download()).DownloadFile(lower, folderPath);
					if (File.Exists(folderPath))
					{
						this.bOfflineMode = false;
						try
						{
							File.Delete(folderPath);
						}
						catch (Exception exception1)
						{
							File.Delete(folderPath);
						}
					}
					else
					{
						this.bOfflineMode = true;
					}
				}
				else
				{
					this.bOfflineMode = true;
				}
				this.strDuplicatePrinting = strArrays[30];
				if (iniFileIO.GetKeyValue("SETTINGS", "DATA_COMPRESSION", empty).ToLower() != "on")
				{
					this.sCompression = "0";
				}
				else
				{
					this.sCompression = "1";
				}
				if (iniFileIO.GetKeyValue("SETTINGS", "DATA_ENCRYPTION", empty).ToLower() != "on")
				{
					this.sEncryption = "0";
				}
				else
				{
					this.sEncryption = "1";
				}
				this.sContentPath = iniFileIO.GetKeyValue("SETTINGS", "CONTENT_PATH", empty);
				if (this.sPrintList == "1")
				{
					this.listColSequence = new ArrayList();
					this.listColSequence = iniFileIO.GetKeys(empty, "PLIST_SETTINGS");
				}
				try
				{
					string keyValue = iniFileIO.GetKeyValue("PRINTER_SETTINGS", "COPYRIGHT_FIELD", empty);
					if (!(this.copyrightPrinitng == "1") || !(this.sPrintHeaderFooter == "1"))
					{
						this.copyRightField = string.Empty;
					}
					else if (this.sLanguage.ToUpper() != "ENGLISH")
					{
						this.copyRightField = this.GetCopyRightText(this.sLanguage, keyValue);
					}
					else
					{
						this.copyRightField = keyValue;
					}
				}
				catch
				{
				}
				if (this.sSplittingOption == "0")
				{
					this.pageSplitCount = 1;
				}
				else if (this.sSplittingOption == "1")
				{
					this.pageSplitCount = 2;
				}
				else if (this.sSplittingOption == "2")
				{
					this.pageSplitCount = 4;
				}
				else if (this.sSplittingOption == "3")
				{
					this.pageSplitCount = 8;
				}
				this.dateFormat = iniFileIO.GetKeyValue("PRINTER_SETTINGS", "DATE_FORMAT", empty);
				if (this.dateFormat == null || this.dateFormat == string.Empty)
				{
					this.dateFormat = "yyyy/MM/dd";
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
			catch
			{
			}
		}

		private void SetMultiRangeAruguments()
		{
			if (this.bMuliRageKey && this.bMultiRange)
			{
				this.strMultiRngStart = this.sRngStart;
				this.strMultiRngEnd = this.sRngEnd;
			}
		}

		public void StartPrinting()
		{
			try
			{
				try
				{
					this.objCurrentPrintJob = (PreviewManager.PrintJob)this.arrPrintJobs[0];
					if (this.arrPrintJobs.Count != 1 || !(this.sPrintRng == "0") || !(this.objCurrentPrintJob.sPrintPic == "1") || !(this.objCurrentPrintJob.sPrintList == "0") || !(this.objCurrentPrintJob.sPrintSelList == "0") || File.Exists(this.objCurrentPrintJob.sLocalPicPath))
					{
						CustomPrintPreviewDialog customPrintPreviewDialog = new CustomPrintPreviewDialog(this);
						this.InitializePrintVariablesOld(this.objCurrentPrintJob);
						this.printDocSList.DefaultPageSettings.Landscape = (this.objCurrentPrintJob.sOrientation == "1" ? true : false);
						this.ReadSelectionList();
						this.ExportImageForSplitPrinting();
						this.ImagePrinted = 0;
						bool flag = false;
						if (this.bOfflineMode && this.arrPrintJobs.Count == 1 && this.objCurrentPrintJob.sLocalPicPath == string.Empty && this.objCurrentPrintJob.sLocalListPath == string.Empty && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
						{
							flag = true;
							customPrintPreviewDialog.Document = this.printDocSList;
						}
						else if (this.objCurrentPrintJob.sZoom != "0")
						{
							this.printDocHalfPage.DefaultPageSettings.Landscape = (this.objCurrentPrintJob.sOrientation == "1" ? true : false);
							if (!(this.objCurrentPrintJob.sPrintSelList == "1") || this.dgSelList == null || this.dgSelList.Rows.Count <= 0)
							{
								customPrintPreviewDialog.Document = this.printDocHalfPage;
							}
							else
							{
								flag = true;
								this.iSelListRowCount = 0;
								customPrintPreviewDialog.Document = new PreviewManager.MultiPrintDocument(this.printDocHalfPage, this.printDocSList);
							}
							flag = true;
						}
						else
						{
							this.changePaperOrientation();
							if ((this.objCurrentPrintJob.sPrintPic == "1" || this.objCurrentPrintJob.sPrintList == "1") && this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count > 0)
							{
								flag = true;
								this.iSelListRowCount = 0;
								customPrintPreviewDialog.Document = new PreviewManager.MultiPrintDocument(this.printDocFitPage, this.printDocSList);
							}
							else if ((this.objCurrentPrintJob.sPrintPic == "1" || this.objCurrentPrintJob.sPrintList == "1") && (this.dgSelList == null || this.dgSelList.Rows.Count == 0))
							{
								flag = true;
								customPrintPreviewDialog.Document = this.printDocFitPage;
							}
							else if (this.objCurrentPrintJob.sPrintPic == "0" && this.objCurrentPrintJob.sPrintList == "0" && this.objCurrentPrintJob.sPrintSelList == "1" && this.dgSelList != null && this.dgSelList.Rows.Count != 0)
							{
								flag = true;
								customPrintPreviewDialog.Document = this.printDocSList;
							}
						}
						if (flag)
						{
							customPrintPreviewDialog.ShowDialog(this.frmParent);
						}
					}
					else
					{
						return;
					}
				}
				catch
				{
				}
			}
			finally
			{
				this.objDjVuCtl.SRC = string.Empty;
			}
		}

		[DllImport("DjVuDecoder.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern int UnSecureDjVu(string source, string destination, string userId, string password);

		[DllImport("ZIPPER.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern IntPtr UnZipFile(string sFilePath);

		public class MultiPrintDocument : PrintDocument
		{
			private List<PrintDocument> lstPrintDocs;

			private int intPrintDocIndex;

			private PrintEventArgs peaPrintArgs;

			private PreviewManager objParent;

			private PrintDocument[] _documents;

			public MultiPrintDocument(PrintDocument document1, PrintDocument document2)
			{
				try
				{
					this._documents = new PrintDocument[] { document1, document2 };
					PrintDocument[] printDocumentArray = this._documents;
					for (int i = 0; i < (int)printDocumentArray.Length; i++)
					{
						PrintDocument printDocument = printDocumentArray[i];
						this.lstPrintDocs.Add(printDocument);
					}
				}
				catch (Exception exception)
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
				this.objParent = (PreviewManager)parent;
			}

			private void CallMethod(PrintDocument document, string methodName, object args)
			{
				Type type = typeof(PrintDocument);
				object[] objArray = new object[] { args };
				type.InvokeMember(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, document, objArray);
			}

			protected override void OnBeginPrint(PrintEventArgs e)
			{
				base.OnBeginPrint(e);
				if (this.lstPrintDocs.Count == 0)
				{
					e.Cancel = true;
				}
				if (e.Cancel)
				{
					return;
				}
				this.peaPrintArgs = e;
				this.intPrintDocIndex = 0;
				this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], "OnBeginPrint", e);
			}

			protected override void OnPrintPage(PrintPageEventArgs e)
			{
				this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], "OnPrintPage", e);
				base.OnPrintPage(e);
				if (e.Cancel)
				{
					return;
				}
				if (!e.HasMorePages)
				{
					this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], "OnEndPrint", this.peaPrintArgs);
					if (this.peaPrintArgs.Cancel)
					{
						return;
					}
					this.intPrintDocIndex++;
					if (this.intPrintDocIndex < this.lstPrintDocs.Count)
					{
						e.HasMorePages = true;
						this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], "OnBeginPrint", this.peaPrintArgs);
					}
				}
			}

			protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
			{
				e.PageSettings = this.lstPrintDocs[this.intPrintDocIndex].DefaultPageSettings;
				this.CallMethod(this.lstPrintDocs[this.intPrintDocIndex], "OnQueryPageSettings", e);
				base.OnQueryPageSettings(e);
			}
		}

		public class PrintJob
		{
			public PreviewManager frmParent;

			public string sPrinterName;

			public string sOrientation;

			public string sPaperSize;

			public string sUserId;

			public string sPassword;

			public string sSplittingOption;

			public string sPrintHeaderFooter;

			public string sDescription;

			public DateTime UpdateDate;

			public DateTime UpdateDatePIC;

			public DateTime UpdateDatePL;

			public string sPrintPgNos;

			public string sZoom;

			public string sCurrentImageZoom;

			public string sMaintainZoom;

			public string sZoomFactor;

			public int pageSplitCount;

			public string sPrintPic;

			public string sPrintList;

			public string sPrintSelList;

			public string sPrintSideBySide;

			public string sPrintRng;

			public string sBookType;

			public string spaperUtilization;

			public string sProxyType;

			public string sTimeOut;

			public string sProxyIP;

			public string sProxyPort;

			public string sProxyLogin;

			public string sProxyPassword;

			public string sCompression;

			public string sEncryption;

			public string sLocalListPath;

			public string sServerListPath;

			public string sLocalPicPath;

			public string sServerPicPath;

			public string dateFormat;

			public string copyRightField;

			public string sLanguage;

			public string strPicMemoValue;

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
				this.copyRightField = string.Concat(Environment.NewLine, copyRightField1);
				this.copyRightField = copyRightField1;
				this.strPicMemoValue = strPicMemoValue1;
			}
		}
	}
}
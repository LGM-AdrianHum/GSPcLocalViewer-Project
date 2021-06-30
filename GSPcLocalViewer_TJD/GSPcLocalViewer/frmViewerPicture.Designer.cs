using AxDjVuCtrlLib;
using DjVuCtrlLib;
using GSPcLocalViewer.Properties;
using PDFLibNet;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
	public class frmViewerPicture : DockContent
	{
		private frmViewer frmParent;

		private XmlNode curPageSchema;

		private XmlNode curPageNode;

		private int curPicIndex;

		private int curListIndex;

		private string curPicName;

		private string attIdElement;

		private string attPicElement;

		private string attListElement;

		private string attUpdateDatePicElement;

		private string attUpdateDateListElement;

		public bool isWorking;

		private ScrollTypes ScrollType;

		private bool bAuthFailed;

		private string BookPublishingId;

		private string sPreviousImage;

		private string sPictureTitle;

		public static int djuvCntrlPagecount;

		private string statusText;

		private string highLightText;

		private string djVuPageNumber;

		private string curPicZoom;

		private string prevPicZoom;

		public string sPreviousDjVuCtrlHitEnd;

		private Download objDownloader;

		private bool loadPartslist;

		private int iSelectedIndex;

		private bool bNonNumberEntered;

		private bool bSelectiveZoom;

		private IContainer components;

		private Panel pnlForm;

		private PictureBox picLoading;

		private ToolStrip toolStrip1;

		private ToolStripButton tsBtnNext;

		private ToolStripTextBox tsTxtPics;

		private ToolStripButton tsBtnPrev;

		private BackgroundWorker bgWorker;

		private Panel pnlPic;

		private PictureBox objPicCtl;

		private ToolStripButton tsbPicZoomIn;

		private ToolStripButton tsbPicZoomOut;

		private ToolStripButton tsbAddPictureMemo;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton tsbFitPage;

		private ToolStripButton tsBRotateRight;

		private ToolStripButton tsBRotateLeft;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton tsbPicSelectText;

		private ToolStripButton tsbPicPanMode;

		private ToolStripButton tsbPicCopy;

		private ToolStripButton tsbSearchText;

		private ToolStripButton tsbPicZoomSelect;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton tsbThumbnail;

		public AxDjVuCtrl objDjVuCtl;

		internal WebBrowser wbPDF;

		private ToolStripComboBox tsCmbZoom;

		public string CurrentPageId
		{
			get
			{
				string empty;
				try
				{
					string name = string.Empty;
					string str = string.Empty;
					foreach (XmlAttribute attribute in this.curPageSchema.Attributes)
					{
						if (!attribute.Value.ToUpper().Equals("ID"))
						{
							continue;
						}
						name = attribute.Name;
						break;
					}
					if (name != string.Empty)
					{
						str = this.curPageNode.Attributes[name].Value.ToString();
					}
					empty = str;
				}
				catch
				{
					empty = string.Empty;
				}
				return empty;
			}
		}

		public string CurrentPageName
		{
			get
			{
				string empty;
				try
				{
					string name = string.Empty;
					string str = string.Empty;
					foreach (XmlAttribute attribute in this.curPageSchema.Attributes)
					{
						if (!attribute.Value.ToUpper().Equals("PAGENAME"))
						{
							continue;
						}
						name = attribute.Name;
						break;
					}
					if (name != string.Empty)
					{
						str = this.curPageNode.Attributes[name].Value.ToString();
					}
					empty = str;
				}
				catch
				{
					empty = string.Empty;
				}
				return empty;
			}
		}

		public string CurrentPicName
		{
			get
			{
				string empty;
				try
				{
					empty = this.curPicName;
				}
				catch
				{
					empty = string.Empty;
				}
				return empty;
			}
		}

		public string DjVuPageNumber
		{
			set
			{
				this.djVuPageNumber = value;
			}
		}

		public string HighLightText
		{
			set
			{
				this.highLightText = value;
			}
		}

		public string PicturePath
		{
			get
			{
				return this.objDjVuCtl.SRC;
			}
		}

		static frmViewerPicture()
		{
		}

		public frmViewerPicture(frmViewer frm)
		{
			this.InitializeComponent();
			this.curPageSchema = null;
			this.curPageNode = null;
			this.curPicIndex = 0;
			this.curListIndex = 0;
			this.curPicName = string.Empty;
			this.attIdElement = string.Empty;
			this.attPicElement = string.Empty;
			this.attListElement = string.Empty;
			this.attUpdateDatePicElement = string.Empty;
			this.attUpdateDateListElement = string.Empty;
			this.isWorking = false;
			this.ScrollType = ScrollTypes.None;
			this.BookPublishingId = string.Empty;
			this.sPreviousDjVuCtrlHitEnd = string.Empty;
			this.frmParent = frm;
			this.InitializeDjVu();
			this.curPicZoom = Settings.Default.DefaultPictureZoom;
			this.prevPicZoom = Settings.Default.DefaultPictureZoom;
			this.LoadResources();
			try
			{
				if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("WIDTH"))
				{
					this.tsCmbZoom.Text = this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX);
				}
				else if (Settings.Default.DefaultPictureZoom.ToUpper().Contains("FITPAGE"))
				{
					this.tsCmbZoom.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX);
				}
				else if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("ONE2ONE"))
				{
					this.tsCmbZoom.Text = this.GetResource("One To One", "ONE_TO_ONE", ResourceType.COMBO_BOX);
				}
				else if (!Settings.Default.DefaultPictureZoom.ToUpper().Equals("STRETCH"))
				{
					this.tsCmbZoom.Text = Settings.Default.DefaultPictureZoom;
				}
				else
				{
					this.tsCmbZoom.Text = this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX);
				}
			}
			catch (Exception exception)
			{
			}
			this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
			base.MouseWheel += new MouseEventHandler(this.MouseScroll);
			this.sPreviousImage = string.Empty;
			this.objDownloader = new Download(this.frmParent);
			this.frmParent.ISPDF = false;
			this.tsbAddPictureMemo.Visible = Program.objAppFeatures.bMemo;
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.frmParent.bObjFrmPictureClosed = false;
			object[] argument = (object[])e.Argument;
			XmlNode xmlNodes = (XmlNode)argument[0];
			XmlNode xmlNodes1 = (XmlNode)argument[1];
			int count = (int)argument[2];
			int num = (int)argument[3];
			this.loadPartslist = true;
			bool flag = false;
			string empty = string.Empty;
			string item = string.Empty;
			DateTime dateTime = new DateTime();
			string name = string.Empty;
			string str = string.Empty;
			this.attPicElement = string.Empty;
			this.attListElement = string.Empty;
			this.attUpdateDatePicElement = string.Empty;
			this.BookPublishingId = this.frmParent.BookPublishingId;
			for (int i = 0; i < xmlNodes.Attributes.Count; i++)
			{
				if (xmlNodes.Attributes[i].Value.ToUpper().Equals("PICTUREFILE"))
				{
					this.attPicElement = xmlNodes.Attributes[i].Name;
				}
				if (xmlNodes.Attributes[i].Value.ToUpper().Equals("PARTSLISTFILE"))
				{
					this.attListElement = xmlNodes.Attributes[i].Name;
				}
				if (xmlNodes.Attributes[i].Value.ToUpper().Equals("UPDATEDATE"))
				{
					this.attUpdateDatePicElement = xmlNodes.Attributes[i].Name;
					this.attUpdateDateListElement = xmlNodes.Attributes[i].Name;
				}
				if (xmlNodes.Attributes[i].Value.ToUpper().Equals("UPDATEDATEPIC"))
				{
					name = xmlNodes.Attributes[i].Name;
				}
				if (xmlNodes.Attributes[i].Value.ToUpper().Equals("UPDATEDATEPL"))
				{
					str = xmlNodes.Attributes[i].Name;
				}
				if (xmlNodes.Attributes[i].Value.ToUpper().Equals("PICTURETITLE"))
				{
					this.sPictureTitle = xmlNodes.Attributes[i].Name;
				}
			}
			if (name != string.Empty)
			{
				this.attUpdateDatePicElement = name;
			}
			if (str != string.Empty)
			{
				this.attUpdateDateListElement = str;
			}
			if (this.attPicElement == string.Empty || this.attListElement == string.Empty)
			{
				this.bgWorker.CancelAsync();
				MessageHandler.ShowWarning(this.GetResource("(E-VPC-EM001) Not in required format", "(E-VPC-EM001)_FORMAT", ResourceType.POPUP_MESSAGE));
				return;
			}
			XmlNodeList xmlNodeLists = xmlNodes1.SelectNodes(string.Concat("//Pic[@", this.attPicElement, "]"));
			xmlNodeLists = this.frmParent.FilterPicsList(xmlNodes, xmlNodeLists);
			foreach (XmlNode xmlNodes2 in xmlNodeLists)
			{
				if (xmlNodes2.Attributes.Count != 0)
				{
					continue;
				}
				xmlNodes1.RemoveChild(xmlNodes2);
			}
			string[] strArrays = new string[] { "//Pic[not(@", this.attPicElement, " = preceding-sibling::Pic/@", this.attPicElement, ")]" };
			xmlNodeLists = xmlNodes1.SelectNodes(string.Concat(strArrays));
			if (xmlNodeLists.Count <= 0)
			{
				if (!this.frmParent.IsDisposed)
				{
					this.LoadBlankPage(item);
				}
				this.loadPartslist = false;
			}
			else
			{
				try
				{
					this.statusText = this.GetResource("Loading picture..", "LOADING_PICTURE", ResourceType.STATUS_MESSAGE);
					this.UpdateStatus();
					string[] value = new string[xmlNodeLists.Count];
					for (int j = 0; j < xmlNodeLists.Count; j++)
					{
						if (xmlNodeLists[j].Attributes[this.attPicElement] == null)
						{
							value[j] = string.Empty;
						}
						else
						{
							value[j] = xmlNodeLists[j].Attributes[this.attPicElement].Value;
						}
					}
					if (Program.objAppFeatures.bDjVuScroll && this.ScrollType == ScrollTypes.Up)
					{
						count = xmlNodeLists.Count - 1;
					}
					if (count >= xmlNodeLists.Count)
					{
						count = 0;
					}
					this.curPicIndex = count;
					this.curPicName = value[count];
					this.SetPicIndex(xmlNodeLists, this.attPicElement, count);
					if (!value[count].Equals(string.Empty))
					{
						if (!value[count].ToUpper().EndsWith(".DJVU") && !value[count].ToUpper().EndsWith(".PDF") && !value[count].ToUpper().EndsWith(".TIF"))
						{
							value[count] = string.Concat(value[count], ".djvu");
						}
						empty = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
						if (!empty.EndsWith("/"))
						{
							empty = string.Concat(empty, "/");
						}
						empty = string.Concat(empty, this.BookPublishingId, "/", value[count]);
						item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
						item = string.Concat(item, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
						item = string.Concat(item, "\\", this.BookPublishingId);
						if (!Directory.Exists(item))
						{
							Directory.CreateDirectory(item);
						}
						item = string.Concat(item, "\\", value[count]);
						try
						{
							dateTime = DateTime.Parse(xmlNodeLists[count].Attributes[this.attUpdateDatePicElement].Value, new CultureInfo("fr-FR", false));
						}
						catch
						{
						}
					}
				}
				catch
				{
					this.bgWorker.CancelAsync();
					MessageHandler.ShowWarning(this.GetResource("(E-VPC-EM002) Failed to load specified object", "(E-VPC-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
					return;
				}
				if (empty != string.Empty && item != string.Empty)
				{
					int num1 = 0;
					if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num1))
					{
						num1 = 0;
					}
					if (!File.Exists(item))
					{
						flag = true;
					}
					else if (num1 == 0)
					{
						flag = true;
					}
					else if (num1 < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(item), dateTime, num1))
					{
						flag = true;
					}
					if (flag && !Program.objAppMode.bWorkingOffline && this.objDownloader.DownloadFile(empty, item))
					{
						try
						{
							if (dateTime.Year != 1)
							{
								Global.ChangeDjVUModifiedDate(item, dateTime);
							}
						}
						catch
						{
						}
					}
					if (Program.objAppMode.bWorkingOffline && !File.Exists(item))
					{
						this.loadPartslist = false;
					}
					if (File.Exists(item))
					{
						if (!this.frmParent.IsDisposed)
						{
							if (item.ToUpper().EndsWith("PDF"))
							{
								this.ShowLoading();
								this.ChangePDFSrc(item);
							}
							else if (item.ToUpper().EndsWith("DJVU"))
							{
								this.ShowDJVU(true, item);
							}
							else if (!item.ToUpper().EndsWith("TIF"))
							{
								this.ShowDJVU(true, item);
							}
							else
							{
								this.ShowDJVU(false, string.Empty);
								this.ChangeTiffSrc(item);
							}
						}
					}
					else if (!this.frmParent.IsDisposed)
					{
						this.LoadBlankPage(item);
					}
					this.frmParent.bPictureClosed = false;
					this.frmParent.ShowPicture();
				}
				else if (!this.frmParent.IsDisposed)
				{
					this.frmParent.HidePicture();
				}
			}
			object[] objArray = new object[] { xmlNodes, xmlNodes1, count, num, this.loadPartslist };
			e.Result = objArray;
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.frmParent.bObjFrmPictureClosed = true;
			this.HideLoading(this.pnlForm);
			try
			{
				try
				{
					object[] result = (object[])e.Result;
					XmlNode xmlNodes = (XmlNode)result[0];
					XmlNode xmlNodes1 = (XmlNode)result[1];
					int num = (int)result[2];
					int num1 = (int)result[3];
					bool flag = (bool)result[4];
					if (!this.frmParent.IsDisposed)
					{
						if (this.curPageSchema != xmlNodes || this.curPageNode != xmlNodes1 || this.curPicIndex != num)
						{
							this.isWorking = false;
							this.LoadPicture(this.curPageSchema, this.curPageNode, this.curPicIndex, 0);
							return;
						}
						else
						{
							int num2 = 0;
							int num3 = 0;
							string text = this.tsTxtPics.Text;
							try
							{
								if (text.Contains("/"))
								{
									num2 = int.Parse(text.Substring(0, text.IndexOf("/")));
									num3 = int.Parse(text.Substring(text.IndexOf("/") + 1, text.Length - (text.IndexOf("/") + 1)));
								}
								if (num2 != 1)
								{
									this.tsBtnPrev.Enabled = true;
								}
								else
								{
									this.tsBtnPrev.Enabled = false;
								}
								if (num2 != num3)
								{
									this.tsBtnNext.Enabled = true;
								}
								else
								{
									this.tsBtnNext.Enabled = false;
								}
								this.frmParent.UpdatePicToolstrip(this.tsBtnPrev.Enabled, this.tsBtnNext.Enabled, this.tsTxtPics.Text);
							}
							catch
							{
							}
							if (!flag)
							{
								this.frmParent.HidePartsList();
								this.frmParent.UpdateCurrentPageForPartslist(flag, this.curPageSchema, this.curPageNode, this.curPicIndex, this.curListIndex, this.attPicElement, this.attListElement, this.attUpdateDateListElement);
							}
							else
							{
								this.objDjVuCtl.Select();
								this.frmParent.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, this.curListIndex, this.attPicElement, this.attListElement, this.attUpdateDateListElement);
							}
							this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
							this.UpdateStatus();
							this.isWorking = false;
							if (Settings.Default.PopupPictureMemo)
							{
								this.frmParent.ShowPictureMemos(true);
								this.frmParent.EnableAddPicMemoTSB(true);
								this.frmParent.EnableAddPicMemoMenu(true);
								this.EnableAddPicMemoTSB(true);
							}
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
				this.frmParent.EnableAddPicMemoTSB(true);
				this.frmParent.EnableAddPicMemoMenu(true);
				this.EnableAddPicMemoTSB(true);
			}
		}

		private void CenterPicBoxImage()
		{
			this.objPicCtl.Size = new System.Drawing.Size(0, 0);
			this.objPicCtl.Size = this.objPicCtl.Image.Size;
			if (this.objPicCtl.Width >= this.pnlPic.Width)
			{
				this.objPicCtl.Left = 0;
			}
			else
			{
				this.objPicCtl.Left = (this.pnlPic.Width - this.objPicCtl.Width) / 2;
			}
			if (this.objPicCtl.Height >= this.pnlPic.Height)
			{
				this.objPicCtl.Top = 0;
				return;
			}
			this.objPicCtl.Top = (this.pnlPic.Height - this.objPicCtl.Height) / 2;
		}

		private void ChangeDJVUZoom(string sZoom)
		{
			if (base.InvokeRequired)
			{
				frmViewerPicture.ChangeDJVUZoomDelegate changeDJVUZoomDelegate = new frmViewerPicture.ChangeDJVUZoomDelegate(this.ChangeDJVUZoom);
				object[] objArray = new object[] { sZoom };
				base.Invoke(changeDJVUZoomDelegate, objArray);
				return;
			}
			if (Settings.Default.MaintainZoom)
			{
				this.objDjVuCtl.Zoom = sZoom;
			}
			else if (Settings.Default.DefaultPictureZoom == "WIDTH")
			{
				this.objDjVuCtl.Zoom = "132";
				this.objDjVuCtl.Zoom = "WIDTH";
			}
			else if (Settings.Default.DefaultPictureZoom != "ONE2ONE")
			{
				this.objDjVuCtl.Zoom = sZoom;
			}
			else
			{
				this.objDjVuCtl.Zoom = "300";
				this.objDjVuCtl.Zoom = "ONE2ONE";
			}
			this.curPicZoom = sZoom;
			this.prevPicZoom = sZoom;
			if (this.objDjVuCtl.Zoom.ToUpper().Contains("FITPAGE") && this.sPreviousImage != this.objDjVuCtl.SRC && this.frmParent.sBookType.ToUpper() == "GSP")
			{
				frmViewerPicture height = this;
				height.Height = height.Height + 1;
				frmViewerPicture _frmViewerPicture = this;
				_frmViewerPicture.Height = _frmViewerPicture.Height - 1;
				frmViewerPicture width = this;
				width.Width = width.Width + 1;
				frmViewerPicture width1 = this;
				width1.Width = width1.Width - 1;
			}
			this.sPreviousImage = this.objDjVuCtl.SRC;
			if (this.frmParent.objFrmMemo == null)
			{
				this.objDjVuCtl.Focus();
				return;
			}
			this.frmParent.objFrmMemo.Focus();
		}

		private void ChangePDFSrc(string sLocalFile)
		{
			if (this.wbPDF.InvokeRequired)
			{
				WebBrowser webBrowser = this.wbPDF;
				frmViewerPicture.ChangeTiffSrcDelegate changeTiffSrcDelegate = new frmViewerPicture.ChangeTiffSrcDelegate(this.ChangePDFSrc);
				object[] objArray = new object[] { sLocalFile };
				webBrowser.Invoke(changeTiffSrcDelegate, objArray);
				return;
			}
			string str = "";
			this.toolStrip1.Hide();
			this.frmParent.tsPic.Hide();
			this.objDjVuCtl.SendToBack();
			if (this.highLightText == null || this.djVuPageNumber == null)
			{
				string str1 = string.Concat("file:////", sLocalFile, "#toolbar=on");
				if (sLocalFile.ToUpper() != this.sPreviousImage.ToUpper() && str == "")
				{
					this.frmParent.ISPDF = true;
					this.wbPDF.BringToFront();
					str = "";
					this.wbPDF.Navigate(str1);
					if (str1 != null)
					{
						this.sPreviousImage = sLocalFile;
					}
				}
			}
			else
			{
				string[] strArrays = new string[] { "file:////", sLocalFile, "#toolbar=on&search=\"", this.highLightText, "\"&page=", this.djVuPageNumber };
				string str2 = string.Concat(strArrays);
				if (str2.ToUpper() != this.sPreviousImage.ToUpper())
				{
					this.frmParent.ISPDF = true;
					this.wbPDF.BringToFront();
					str = str2;
					this.wbPDF.Navigate(str2);
					this.sPreviousImage = sLocalFile;
					Application.DoEvents();
					this.highLightText = null;
					this.djVuPageNumber = null;
					return;
				}
			}
		}

		private void ChangeTiffSrc(string Src)
		{
			if (this.objPicCtl.InvokeRequired)
			{
				PictureBox pictureBox = this.objPicCtl;
				frmViewerPicture.ChangeTiffSrcDelegate changeTiffSrcDelegate = new frmViewerPicture.ChangeTiffSrcDelegate(this.ChangeTiffSrc);
				object[] src = new object[] { Src };
				pictureBox.Invoke(changeTiffSrcDelegate, src);
				return;
			}
			Image.GetThumbnailImageAbort getThumbnailImageAbort = new Image.GetThumbnailImageAbort(frmViewerPicture.ThumbnailCallback);
			Image thumbnailImage = Image.FromFile(Src);
			System.Drawing.Size size = this.GenerateImageDimensions(thumbnailImage.Width, thumbnailImage.Height, this.pnlPic.Width, this.pnlPic.Height, "Portrait");
			thumbnailImage = thumbnailImage.GetThumbnailImage(size.Width, size.Height, getThumbnailImageAbort, IntPtr.Zero);
			Bitmap bitmap = new Bitmap(thumbnailImage, size.Width, size.Height);
			this.objPicCtl.Image = bitmap;
			this.CenterPicBoxImage();
			this.Refresh();
		}

		public void CopyImage()
		{
			if (this.objDjVuCtl.MouseMode != "Copy")
			{
				this.objDjVuCtl.MouseMode = "Copy";
				this.tsbPicCopy.CheckState = CheckState.Checked;
				this.tsbPicPanMode.CheckState = CheckState.Unchecked;
				this.tsbPicSelectText.CheckState = CheckState.Unchecked;
				this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
			}
			else
			{
				this.objDjVuCtl.MouseMode = "Pan";
				this.tsbPicCopy.CheckState = CheckState.Unchecked;
				this.tsbPicPanMode.CheckState = CheckState.Checked;
				this.tsbPicSelectText.CheckState = CheckState.Unchecked;
				this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
			}
			this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
			this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
			this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
			this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
		}

		public string CurrentImageSource()
		{
			return this.objDjVuCtl.SRC;
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

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public void DisposeDjVuControl()
		{
			try
			{
				this.objDjVuCtl.SRC = null;
			}
			catch
			{
			}
		}

		public void EnableAddPicMemoTSB(bool value)
		{
			if (!this.toolStrip1.InvokeRequired)
			{
				this.tsbAddPictureMemo.Enabled = value;
				return;
			}
			ToolStrip toolStrip = this.toolStrip1;
			frmViewerPicture.EnableAddMemoDelegate enableAddMemoDelegate = new frmViewerPicture.EnableAddMemoDelegate(this.EnableAddPicMemoTSB);
			object[] objArray = new object[] { value };
			toolStrip.Invoke(enableAddMemoDelegate, objArray);
		}

		public int ExportCurrentImage(string filename, string fmt, bool bNeedAnno, int pageIndex, bool bCurZoom, int full, int fullT, int fullR, int fullB, int viewL, int viewT, int viewR, int viewB)
		{
			bool flag = false;
			if (Program.objDjVuFeatures.sShowAnnoOnPrint.ToUpper() == "ON")
			{
				flag = true;
			}
			return this.objDjVuCtl.ExportImageAs1(filename, fmt, flag, pageIndex, bCurZoom, full, fullT, fullR, fullB, viewL, viewT, viewR, viewB);
		}

		public void FitPage()
		{
			try
			{
				this.objDjVuCtl.Zoom = this.objDjVuCtl.FitPagePercent;
				this.curPicZoom = this.objDjVuCtl.Zoom;
				this.prevPicZoom = this.objDjVuCtl.Zoom;
			}
			catch
			{
			}
		}

		private void frmViewerPicture_Load(object sender, EventArgs e)
		{
			if (!Program.objAppFeatures.bMiniMap)
			{
				this.objDjVuCtl.ShowBirdsEyeView = false;
			}
			else
			{
				this.objDjVuCtl.ShowBirdsEyeView = true;
			}
			this.OnOffFeatures();
		}

		private void frmViewerPicture_SizeChanged(object sender, EventArgs e)
		{
			base.MouseWheel += new MouseEventHandler(this.MouseScroll);
		}

		private void frmViewerPicture_VisibleChanged(object sender, EventArgs e)
		{
			try
			{
				this.frmParent.pictureToolStripMenuItem.Checked = base.Visible;
				this.frmParent.miniMapToolStripMenuItem.Enabled = base.Visible;
				this.frmParent.bPictureClosed = !base.Visible;
				this.objDjVuCtl.ShowBirdsEyeView = (!base.Visible ? false : this.frmParent.miniMapToolStripMenuItem.Checked);
			}
			catch
			{
			}
		}

		public System.Drawing.Size GenerateImageDimensions(int currW, int currH, int destW, int destH, string layout)
		{
			double num = 0;
			string lower = layout.ToLower();
			string str = lower;
			if (lower != null)
			{
				if (str == "portrait")
				{
					num = (destH <= destW ? (double)destH / (double)currH : (double)destW / (double)currW);
				}
				else if (str == "landscape")
				{
					num = (destH <= destW ? (double)destH / (double)currH : (double)destW / (double)currW);
				}
			}
			return new System.Drawing.Size((int)((double)currW * num), (int)((double)currH * num));
		}

		public string GetDjVuZoom()
		{
			return this.objDjVuCtl.Zoom;
		}

		public int[] GetImageZoom()
		{
			int num = 0;
			int num1 = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			this.objDjVuCtl.GetCurrentZoomRects(ref num, ref num1, ref num3, ref num2, ref num4, ref num5, ref num6, ref num7);
			int[] numArray = new int[] { num, num1, num3, num2, num4, num5, num6, num7 };
			return numArray;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MAIN_FORM']");
				str = string.Concat(str, "/Screen[@Name='PICTURE']");
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
					frmViewerPicture.HideLoadingDelegate hideLoadingDelegate = new frmViewerPicture.HideLoadingDelegate(this.HideLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(hideLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		public void HideLoading1()
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					foreach (Control control in this.pnlForm.Controls)
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
					base.Invoke(new frmViewerPicture.HideLoadingDelegate1(this.HideLoading1));
				}
			}
			catch
			{
			}
		}

		public void HighlightPicture(int x, int y, int width, int height)
		{
			int num;
			try
			{
				if (!int.TryParse(ColorTranslator.ToOle(Settings.Default.appHighlightBackColor).ToString(), out num))
				{
					num = 16711680;
				}
				this.objDjVuCtl.AddHighlightRect(x, y, width, height, num);
			}
			catch
			{
			}
		}

		private bool ImageScrollBarsVisible()
		{
			bool flag;
			try
			{
				int num = int.Parse(this.objDjVuCtl.FitPagePercent.ToUpper().Replace("FITPAGE,", string.Empty));
				int num1 = num;
				string zoom = this.objDjVuCtl.Zoom;
				if (zoom.Contains("%"))
				{
					num1 = int.Parse(zoom.ToUpper().Replace("%", string.Empty));
				}
				else if (zoom.ToUpper().Contains("FITPAGE"))
				{
					num1 = int.Parse(zoom.ToUpper().Replace("FITPAGE,", string.Empty));
				}
				else if (zoom.ToUpper().Contains("WIDTH") || zoom.ToUpper().Contains("ONE2ONE"))
				{
					flag = true;
					return flag;
				}
				flag = (num1 > num ? true : false);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmViewerPicture));
			this.pnlForm = new Panel();
			this.pnlPic = new Panel();
			this.wbPDF = new WebBrowser();
			this.objDjVuCtl = new AxDjVuCtrl();
			this.objPicCtl = new PictureBox();
			this.toolStrip1 = new ToolStrip();
			this.tsbSearchText = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsbPicPanMode = new ToolStripButton();
			this.tsbPicZoomSelect = new ToolStripButton();
			this.tsbFitPage = new ToolStripButton();
			this.tsbPicCopy = new ToolStripButton();
			this.tsbPicSelectText = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsbPicZoomIn = new ToolStripButton();
			this.tsCmbZoom = new ToolStripComboBox();
			this.tsbPicZoomOut = new ToolStripButton();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.tsBRotateLeft = new ToolStripButton();
			this.tsBRotateRight = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.tsBtnNext = new ToolStripButton();
			this.tsbAddPictureMemo = new ToolStripButton();
			this.tsbThumbnail = new ToolStripButton();
			this.tsTxtPics = new ToolStripTextBox();
			this.tsBtnPrev = new ToolStripButton();
			this.picLoading = new PictureBox();
			this.bgWorker = new BackgroundWorker();
			this.pnlForm.SuspendLayout();
			this.pnlPic.SuspendLayout();
			((ISupportInitialize)this.objDjVuCtl).BeginInit();
			((ISupportInitialize)this.objPicCtl).BeginInit();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlPic);
			this.pnlForm.Controls.Add(this.toolStrip1);
			this.pnlForm.Controls.Add(this.picLoading);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(528, 381);
			this.pnlForm.TabIndex = 1;
			this.pnlPic.BackColor = Color.White;
			this.pnlPic.Controls.Add(this.wbPDF);
			this.pnlPic.Controls.Add(this.objDjVuCtl);
			this.pnlPic.Controls.Add(this.objPicCtl);
			this.pnlPic.Dock = DockStyle.Fill;
			this.pnlPic.Location = new Point(0, 31);
			this.pnlPic.Name = "pnlPic";
			this.pnlPic.Size = new System.Drawing.Size(526, 348);
			this.pnlPic.TabIndex = 2;
			this.wbPDF.Dock = DockStyle.Fill;
			this.wbPDF.IsWebBrowserContextMenuEnabled = false;
			this.wbPDF.Location = new Point(0, 0);
			this.wbPDF.MinimumSize = new System.Drawing.Size(20, 20);
			this.wbPDF.Name = "wbPDF";
			this.wbPDF.ScriptErrorsSuppressed = true;
			this.wbPDF.Size = new System.Drawing.Size(526, 348);
			this.wbPDF.TabIndex = 28;
			this.objDjVuCtl.Dock = DockStyle.Fill;
			this.objDjVuCtl.Enabled = true;
			this.objDjVuCtl.Location = new Point(0, 0);
			this.objDjVuCtl.Name = "objDjVuCtl";
			this.objDjVuCtl.OcxState = (AxHost.State)componentResourceManager.GetObject("objDjVuCtl.OcxState");
			this.objDjVuCtl.Size = new System.Drawing.Size(526, 348);
			this.objDjVuCtl.TabIndex = 27;
			this.objDjVuCtl.AuthFailed += new EventHandler(this.objDjVuCtl_AuthFailed);
			this.objDjVuCtl.ZoomChange += new AxDjVuCtrlLib._DDjVuCtrlEvents_ZoomChangeEventHandler(this.objDjVuCtl_ZoomChange);
			this.objDjVuCtl.PageChange += new AxDjVuCtrlLib._DDjVuCtrlEvents_PageChangeEventHandler(this.objDjVuCtl_PageChange);
			this.objDjVuCtl.AuthSucceeded += new EventHandler(this.objDjVuCtl_AuthSucceeded);
			this.objDjVuCtl.HyperlinkClick += new AxDjVuCtrlLib._DDjVuCtrlEvents_HyperlinkClickEventHandler(this.objDjVuCtl_HyperlinkClick);
			this.objDjVuCtl.AuthRequired += new EventHandler(this.objDjVuCtl_AuthRequired);
			this.objDjVuCtl.Scroll += new AxDjVuCtrlLib._DDjVuCtrlEvents_ScrollEventHandler(this.objDjVuCtl_Scroll);
			this.objDjVuCtl.PageRotated += new AxDjVuCtrlLib._DDjVuCtrlEvents_PageRotatedEventHandler(this.objDjVuCtl_PageRotated);
			this.objDjVuCtl.PreviewKeyDown += new PreviewKeyDownEventHandler(this.objDjVuCtl_PreviewKeyDown);
			this.objDjVuCtl.CopyRegionEvent += new AxDjVuCtrlLib._DDjVuCtrlEvents_CopyRegionEventEventHandler(this.objDjVuCtl_CopyRegionEvent);
			this.objDjVuCtl.BirdsEyeViewOpenClose += new AxDjVuCtrlLib._DDjVuCtrlEvents_BirdsEyeViewOpenCloseEventHandler(this.objDjVuCtl_BirdsEyeViewOpenClose);
			this.objPicCtl.BackColor = Color.White;
			this.objPicCtl.Cursor = Cursors.Hand;
			this.objPicCtl.Location = new Point(0, 0);
			this.objPicCtl.Name = "objPicCtl";
			this.objPicCtl.Size = new System.Drawing.Size(526, 335);
			this.objPicCtl.SizeMode = PictureBoxSizeMode.CenterImage;
			this.objPicCtl.TabIndex = 26;
			this.objPicCtl.TabStop = false;
			this.toolStrip1.BackColor = SystemColors.Control;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			ToolStripItemCollection items = this.toolStrip1.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.tsbSearchText, this.toolStripSeparator2, this.tsbPicPanMode, this.tsbPicZoomSelect, this.tsbFitPage, this.tsbPicCopy, this.tsbPicSelectText, this.toolStripSeparator1, this.tsbPicZoomIn, this.tsCmbZoom, this.tsbPicZoomOut, this.toolStripSeparator3, this.tsBRotateLeft, this.tsBRotateRight, this.toolStripSeparator4, this.tsBtnNext, this.tsbAddPictureMemo, this.tsbThumbnail, this.tsTxtPics, this.tsBtnPrev };
			items.AddRange(toolStripItemArray);
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip1.Size = new System.Drawing.Size(526, 31);
			this.toolStrip1.TabIndex = 2;
			this.tsbSearchText.Alignment = ToolStripItemAlignment.Right;
			this.tsbSearchText.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSearchText.Image = GSPcLocalViewer.Properties.Resources.Text_Search1;
			this.tsbSearchText.ImageScaling = ToolStripItemImageScaling.None;
			this.tsbSearchText.ImageTransparentColor = Color.Magenta;
			this.tsbSearchText.Name = "tsbSearchText";
			this.tsbSearchText.Size = new System.Drawing.Size(28, 28);
			this.tsbSearchText.Text = "Text Search";
			this.tsbSearchText.Click += new EventHandler(this.tsbSearchText_Click);
			this.toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			this.tsbPicPanMode.Alignment = ToolStripItemAlignment.Right;
			this.tsbPicPanMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicPanMode.Image = GSPcLocalViewer.Properties.Resources.Pan_Mode;
			this.tsbPicPanMode.ImageTransparentColor = Color.Magenta;
			this.tsbPicPanMode.Name = "tsbPicPanMode";
			this.tsbPicPanMode.Size = new System.Drawing.Size(23, 28);
			this.tsbPicPanMode.Text = "Pan Mode";
			this.tsbPicPanMode.Click += new EventHandler(this.tsbPicPanMode_Click);
			this.tsbPicZoomSelect.Alignment = ToolStripItemAlignment.Right;
			this.tsbPicZoomSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicZoomSelect.Image = GSPcLocalViewer.Properties.Resources.zoom_select;
			this.tsbPicZoomSelect.ImageTransparentColor = Color.Magenta;
			this.tsbPicZoomSelect.Name = "tsbPicZoomSelect";
			this.tsbPicZoomSelect.Size = new System.Drawing.Size(23, 28);
			this.tsbPicZoomSelect.Text = "Select Zoom";
			this.tsbPicZoomSelect.Click += new EventHandler(this.tsbPicZoomSelect_Click);
			this.tsbFitPage.Alignment = ToolStripItemAlignment.Right;
			this.tsbFitPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbFitPage.Image = GSPcLocalViewer.Properties.Resources.Picture_FitPage;
			this.tsbFitPage.ImageTransparentColor = Color.Magenta;
			this.tsbFitPage.Name = "tsbFitPage";
			this.tsbFitPage.Size = new System.Drawing.Size(23, 28);
			this.tsbFitPage.Text = "Fit Page";
			this.tsbFitPage.Click += new EventHandler(this.tsBtnFitPage_Click);
			this.tsbPicCopy.Alignment = ToolStripItemAlignment.Right;
			this.tsbPicCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicCopy.Image = GSPcLocalViewer.Properties.Resources.copy_over;
			this.tsbPicCopy.ImageTransparentColor = Color.Magenta;
			this.tsbPicCopy.Name = "tsbPicCopy";
			this.tsbPicCopy.Size = new System.Drawing.Size(23, 28);
			this.tsbPicCopy.Text = "Copy Image";
			this.tsbPicCopy.Click += new EventHandler(this.tsbPicCopy_Click);
			this.tsbPicSelectText.Alignment = ToolStripItemAlignment.Right;
			this.tsbPicSelectText.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicSelectText.Image = GSPcLocalViewer.Properties.Resources.Text_Selection;
			this.tsbPicSelectText.ImageTransparentColor = Color.Magenta;
			this.tsbPicSelectText.Name = "tsbPicSelectText";
			this.tsbPicSelectText.Size = new System.Drawing.Size(23, 28);
			this.tsbPicSelectText.Text = "Copy Image";
			this.tsbPicSelectText.Click += new EventHandler(this.tsbPicSelectText_Click);
			this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			this.tsbPicZoomIn.Alignment = ToolStripItemAlignment.Right;
			this.tsbPicZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicZoomIn.Image = GSPcLocalViewer.Properties.Resources.zoom_in;
			this.tsbPicZoomIn.ImageTransparentColor = Color.Magenta;
			this.tsbPicZoomIn.Name = "tsbPicZoomIn";
			this.tsbPicZoomIn.Size = new System.Drawing.Size(23, 28);
			this.tsbPicZoomIn.Text = "Zoom In";
			this.tsbPicZoomIn.Click += new EventHandler(this.tsbPicZoomIn_Click);
			this.tsCmbZoom.Alignment = ToolStripItemAlignment.Right;
			this.tsCmbZoom.AutoToolTip = true;
			ComboBox.ObjectCollection objectCollections = this.tsCmbZoom.Items;
			object[] objArray = new object[] { "300%", "150%", "100%", "50%", "25%" };
			objectCollections.AddRange(objArray);
			this.tsCmbZoom.MaxLength = 4;
			this.tsCmbZoom.Name = "tsCmbZoom";
			this.tsCmbZoom.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsCmbZoom.Size = new System.Drawing.Size(121, 31);
			this.tsCmbZoom.SelectedIndexChanged += new EventHandler(this.tsCmbZoom_SelectedIndexChanged);
			this.tsCmbZoom.KeyDown += new KeyEventHandler(this.tsCmbZoom_KeyDown);
			this.tsCmbZoom.Leave += new EventHandler(this.tsCmbZoom_Leave);
			this.tsCmbZoom.KeyPress += new KeyPressEventHandler(this.tsCmbZoom_KeyPress);
			this.tsbPicZoomOut.Alignment = ToolStripItemAlignment.Right;
			this.tsbPicZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbPicZoomOut.Image = GSPcLocalViewer.Properties.Resources.zoon_out;
			this.tsbPicZoomOut.ImageTransparentColor = Color.Magenta;
			this.tsbPicZoomOut.Name = "tsbPicZoomOut";
			this.tsbPicZoomOut.Size = new System.Drawing.Size(23, 28);
			this.tsbPicZoomOut.Text = "Zoom Out";
			this.tsbPicZoomOut.Click += new EventHandler(this.tsbPicZoomOut_Click);
			this.toolStripSeparator3.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			this.tsBRotateLeft.Alignment = ToolStripItemAlignment.Right;
			this.tsBRotateLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBRotateLeft.Image = GSPcLocalViewer.Properties.Resources.Rotate_Left;
			this.tsBRotateLeft.ImageTransparentColor = Color.Magenta;
			this.tsBRotateLeft.Name = "tsBRotateLeft";
			this.tsBRotateLeft.Size = new System.Drawing.Size(23, 28);
			this.tsBRotateLeft.Text = "Rotate Left";
			this.tsBRotateLeft.Click += new EventHandler(this.tsBRotateLeft_Click);
			this.tsBRotateRight.Alignment = ToolStripItemAlignment.Right;
			this.tsBRotateRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBRotateRight.Image = GSPcLocalViewer.Properties.Resources.Rotate_Right;
			this.tsBRotateRight.ImageTransparentColor = Color.Magenta;
			this.tsBRotateRight.Name = "tsBRotateRight";
			this.tsBRotateRight.Size = new System.Drawing.Size(23, 28);
			this.tsBRotateRight.Text = "Rotate Right";
			this.tsBRotateRight.Click += new EventHandler(this.tsBRotateRight_Click);
			this.toolStripSeparator4.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			this.tsBtnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnNext.Image = GSPcLocalViewer.Properties.Resources.Nav_Next;
			this.tsBtnNext.ImageTransparentColor = Color.Magenta;
			this.tsBtnNext.Name = "tsBtnNext";
			this.tsBtnNext.Overflow = ToolStripItemOverflow.Never;
			this.tsBtnNext.Size = new System.Drawing.Size(23, 28);
			this.tsBtnNext.Text = "Next Picture";
			this.tsBtnNext.Click += new EventHandler(this.tsBtnNext_Click);
			this.tsbAddPictureMemo.Alignment = ToolStripItemAlignment.Right;
			this.tsbAddPictureMemo.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbAddPictureMemo.Image = GSPcLocalViewer.Properties.Resources.Add_Memo;
			this.tsbAddPictureMemo.ImageTransparentColor = Color.Magenta;
			this.tsbAddPictureMemo.Name = "tsbAddPictureMemo";
			this.tsbAddPictureMemo.Size = new System.Drawing.Size(23, 28);
			this.tsbAddPictureMemo.Text = "Add Picture Memo";
			this.tsbAddPictureMemo.Click += new EventHandler(this.tsbAddPictureMemo_Click);
			this.tsbThumbnail.Alignment = ToolStripItemAlignment.Right;
			this.tsbThumbnail.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbThumbnail.Image = GSPcLocalViewer.Properties.Resources.Thumbnail;
			this.tsbThumbnail.ImageTransparentColor = Color.Magenta;
			this.tsbThumbnail.Name = "tsbThumbnail";
			this.tsbThumbnail.Size = new System.Drawing.Size(23, 28);
			this.tsbThumbnail.Text = "Show Thumbnail";
			this.tsbThumbnail.Click += new EventHandler(this.tsbThumbnail_Click);
			this.tsTxtPics.AutoSize = false;
			this.tsTxtPics.BorderStyle = BorderStyle.FixedSingle;
			this.tsTxtPics.Name = "tsTxtPics";
			this.tsTxtPics.ReadOnly = true;
			this.tsTxtPics.ShortcutsEnabled = false;
			this.tsTxtPics.Size = new System.Drawing.Size(50, 23);
			this.tsTxtPics.TextBoxTextAlign = HorizontalAlignment.Center;
			this.tsBtnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnPrev.Image = GSPcLocalViewer.Properties.Resources.Nav_Prev;
			this.tsBtnPrev.ImageTransparentColor = Color.Magenta;
			this.tsBtnPrev.Name = "tsBtnPrev";
			this.tsBtnPrev.Size = new System.Drawing.Size(23, 20);
			this.tsBtnPrev.Text = "Previous Picture";
			this.tsBtnPrev.Click += new EventHandler(this.tsBtnPrev_Click);
			this.picLoading.BackColor = Color.White;
			this.picLoading.Dock = DockStyle.Fill;
			this.picLoading.Image = GSPcLocalViewer.Properties.Resources.Loading1;
			this.picLoading.Location = new Point(0, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(526, 379);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 21;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(528, 381);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.HideOnClose = true;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "frmViewerPicture";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Picture";
			base.Load += new EventHandler(this.frmViewerPicture_Load);
			base.SizeChanged += new EventHandler(this.frmViewerPicture_SizeChanged);
			base.VisibleChanged += new EventHandler(this.frmViewerPicture_VisibleChanged);
			this.pnlForm.ResumeLayout(false);
			this.pnlForm.PerformLayout();
			this.pnlPic.ResumeLayout(false);
			((ISupportInitialize)this.objDjVuCtl).EndInit();
			((ISupportInitialize)this.objPicCtl).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
		}

		private void InitializeDjVu()
		{
			try
			{
				if (Program.objDjVuFeatures.sToolBar == "on")
				{
					this.objDjVuCtl.Toolbar = "yes";
				}
				else if (Program.objDjVuFeatures.sToolBar == "off")
				{
					this.objDjVuCtl.Toolbar = "no";
				}
				this.objDjVuCtl.Menu = Program.objDjVuFeatures.sPopUpMenu;
				if (Program.objDjVuFeatures.sLinks == "show")
				{
					this.objDjVuCtl.ShowAnno = "yes";
				}
				else if (Program.objDjVuFeatures.sLinks == "hide")
				{
					this.objDjVuCtl.ShowAnno = "no";
				}
				this.objDjVuCtl.NavPane = Program.objDjVuFeatures.sNavigationPane;
				this.objDjVuCtl.Rotate = Program.objDjVuFeatures.sRotationAngle;
				this.objDjVuCtl.ShowToolbarButtons = int.Parse(Program.objDjVuFeatures.nProvidedFunctions.ToString());
				this.objDjVuCtl.KeyboardShortcuts = int.Parse(Program.objDjVuFeatures.nKeyboardShortcuts.ToString());
				if (Program.objDjVuFeatures.sShowAnnoOnPrint != "on")
				{
					this.objDjVuCtl.ShowAnnoOnPrint = "no";
				}
				else
				{
					this.objDjVuCtl.ShowAnnoOnPrint = "yes";
				}
				if (Program.objDjVuFeatures.sShowAnnoOnExport != "on")
				{
					this.objDjVuCtl.ShowAnnoOnExport = "no";
				}
				else
				{
					this.objDjVuCtl.ShowAnnoOnExport = "yes";
				}
				if (Program.objDjVuFeatures.sShowAnnoOnCopy != "on")
				{
					this.objDjVuCtl.ShowAnnoOnCopy = "no";
				}
				else
				{
					this.objDjVuCtl.ShowAnnoOnCopy = "yes";
				}
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
				flag = ((dtServer - dtLocal).Days <= num ? false : true);
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		public void LoadBlankPage(string sLocalFile)
		{
			string str = string.Concat(Application.StartupPath, "\\blank.pdf#toolbar=0");
			if (sLocalFile.ToUpper().EndsWith("DJVU") && File.Exists(string.Concat(Application.StartupPath, "\\blank.djvu")))
			{
				this.ChangeDJVUZoom("PAGE");
				this.ShowDJVU(true, string.Concat(Application.StartupPath, "\\blank.djvu"));
				return;
			}
			if (sLocalFile.ToUpper().EndsWith("TIF") && File.Exists(string.Concat(Application.StartupPath, "\\blank.tif")))
			{
				this.ShowDJVU(false, string.Empty);
				this.ChangeTiffSrc(string.Concat(Application.StartupPath, "\\blank.tif"));
				return;
			}
			if (!this.frmParent.objFrmTreeview.sDataType.ToUpper().EndsWith("PDF"))
			{
				this.ChangeDJVUZoom("PAGE");
				this.ShowDJVU(true, string.Concat(Application.StartupPath, "\\blank.djvu"));
				return;
			}
			string empty = string.Empty;
			this.sPreviousImage = "";
			this.wbPDF.Navigate(empty);
			this.wbPDF.Navigate(str);
		}

		public void LoadPicture(XmlNode schemaNode, XmlNode pageNode, int picIndex, int listIndex)
		{
			this.ShowLoading(this.pnlForm);
			this.curPageSchema = schemaNode;
			this.curPageNode = pageNode;
			this.curPicIndex = picIndex;
			this.curListIndex = listIndex;
			this.attIdElement = string.Empty;
			this.attPicElement = string.Empty;
			this.attListElement = string.Empty;
			this.attUpdateDatePicElement = string.Empty;
			if (!this.isWorking)
			{
				this.isWorking = true;
				BackgroundWorker backgroundWorker = this.bgWorker;
				object[] objArray = new object[] { this.curPageSchema, this.curPageNode, this.curPicIndex, this.curListIndex };
				backgroundWorker.RunWorkerAsync(objArray);
				this.ShowHideMiniMap(frmViewer.MiniMapChk);
			}
		}

		public void LoadResources()
		{
			try
			{
				this.tsBtnPrev.Text = this.GetResource("Previous Picture", "PREVIOUS_PICTURE", ResourceType.TOOLSTRIP);
				this.tsBtnNext.Text = this.GetResource("Next Picture", "NEXT_PICTURE", ResourceType.TOOLSTRIP);
				this.tsbPicCopy.Text = this.GetResource("Copy Image", "COPY_IMAGE", ResourceType.TOOLSTRIP);
				this.tsbPicZoomIn.Text = this.GetResource("Zoom In", "ZOOM_IN", ResourceType.TOOLSTRIP);
				this.tsbPicZoomOut.Text = this.GetResource("Zoom Out", "ZOOM_OUT", ResourceType.TOOLSTRIP);
				this.tsbPicZoomSelect.Text = this.GetResource("Select Zoom", "SELECT_ZOOM", ResourceType.TOOLSTRIP);
				this.tsbAddPictureMemo.Text = this.GetResource("Add Picture Memo", "ADD_PICTURE_MEMO", ResourceType.TOOLSTRIP);
				this.tsbPicPanMode.Text = this.GetResource("Pan Mode", "PAN_MODE", ResourceType.TOOLSTRIP);
				this.tsbFitPage.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.TOOLSTRIP);
				this.tsBRotateLeft.Text = this.GetResource("Rotate Left", "ROTATE_LEFT", ResourceType.TOOLSTRIP);
				this.tsBRotateRight.Text = this.GetResource("Rotate Right", "ROTATE_RIGHT", ResourceType.TOOLSTRIP);
				this.tsbPicSelectText.Text = this.GetResource("Select Text", "SELECT_TEXT", ResourceType.TOOLSTRIP);
				this.tsbThumbnail.Text = this.GetResource("Show Thumbnail", "SHOW_THUMBNAIL", ResourceType.TOOLSTRIP);
				this.tsbSearchText.Text = this.GetResource("Find Text", "FIND_TEXT", ResourceType.TOOLSTRIP);
				while (this.tsCmbZoom.Items.Count > 5)
				{
					this.tsCmbZoom.Items.RemoveAt(this.tsCmbZoom.Items.Count - 1);
				}
				this.tsCmbZoom.Items.Add(this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX));
				this.tsCmbZoom.Items.Add(this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX));
				this.tsCmbZoom.Items.Add(this.GetResource("One to One", "ONE_TO_ONE", ResourceType.COMBO_BOX));
				this.tsCmbZoom.Items.Add(this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX));
				this.tsCmbZoom.SelectedIndex = this.iSelectedIndex;
				if (this.objDjVuCtl.CurrentLanguage.ToUpper() != this.frmParent.AppCurrentLanguage.ToUpper() && this.frmParent.objFrmMemo == null)
				{
					this.objDjVuCtl.CurrentLanguage = this.frmParent.AppCurrentLanguage;
				}
			}
			catch
			{
			}
		}

		public void MouseScroll(object sender, MouseEventArgs e)
		{
			try
			{
				string text = this.tsTxtPics.Text;
				if (Program.objAppFeatures.bDjVuScroll && !this.isWorking && !this.frmParent.objFrmPartlist.isWorking && this.ScrollType == ScrollTypes.None)
				{
					this.frmParent.objFrmTreeview.tvBook.SelectedNode.Expand();
					if (e.Delta < 0)
					{
						if (text.Contains("/") && !text.EndsWith((this.curPicIndex + 1).ToString()) && Settings.Default.MouseScrollContents)
						{
							text = text.Substring(0, text.IndexOf("/"));
							try
							{
								this.LoadPicture(this.curPageSchema, this.curPageNode, this.curPicIndex + 1, 0);
								this.ScrollType = ScrollTypes.MultiDown;
							}
							catch
							{
							}
						}
						else if (this.objDjVuCtl.PageNumber < frmViewerPicture.djuvCntrlPagecount && Settings.Default.MouseScrollPicture)
						{
							AxDjVuCtrl pageNumber = this.objDjVuCtl;
							pageNumber.PageNumber = pageNumber.PageNumber + 1;
							this.ScrollType = ScrollTypes.MultiDown;
						}
						else if (Settings.Default.MouseScrollContents && this.frmParent.objFrmTreeview.tvBook.SelectedNode.NextVisibleNode != null)
						{
							this.frmParent.objFrmTreeview.tvBook.SelectedNode.NextVisibleNode.Expand();
							this.frmParent.objFrmTreeview.SelectNextNode();
							this.ScrollType = ScrollTypes.Down;
						}
					}
					else if (text.Contains("/") && this.curPicIndex > 0)
					{
						text = text.Substring(0, text.IndexOf("/"));
						try
						{
							this.LoadPicture(this.curPageSchema, this.curPageNode, this.curPicIndex - 1, 0);
							this.ScrollType = ScrollTypes.MultiUp;
						}
						catch
						{
						}
					}
					else if (this.objDjVuCtl.PageNumber != 1 && frmViewerPicture.djuvCntrlPagecount != 1 && Settings.Default.MouseScrollPicture)
					{
						this.ScrollType = ScrollTypes.MultiUp;
						AxDjVuCtrl axDjVuCtrl = this.objDjVuCtl;
						axDjVuCtrl.PageNumber = axDjVuCtrl.PageNumber - 1;
					}
					else if (Settings.Default.MouseScrollContents && this.frmParent.objFrmTreeview.tvBook.SelectedNode.PrevVisibleNode != null)
					{
						this.frmParent.objFrmTreeview.tvBook.SelectedNode.PrevVisibleNode.Expand();
						this.frmParent.objFrmTreeview.SelectPreviousNode();
						this.ScrollType = ScrollTypes.Up;
					}
				}
			}
			catch
			{
				this.ScrollType = ScrollTypes.None;
			}
		}

		private void objDjVuCtl_AuthFailed(object sender, EventArgs e)
		{
			if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"] == null || this.bAuthFailed)
			{
				this.bAuthFailed = true;
				this.ShowAuthenticationForm();
				return;
			}
			AES aE = new AES();
			string item = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"];
			string str = aE.DLLDecode(item, "0123456789ABCDEF");
			if (!str.Contains("|"))
			{
				this.bAuthFailed = true;
				this.ShowAuthenticationForm();
				return;
			}
			string str1 = str.Substring(0, str.IndexOf("|"));
			string str2 = str.Substring(str.IndexOf("|") + 1, str.Length - (str.IndexOf("|") + 1));
			this.bAuthFailed = true;
			if (str1.Equals(string.Empty) || str2.Equals(string.Empty))
			{
				this.ShowAuthenticationForm();
				return;
			}
			this.objDjVuCtl.SetNameAndPass(str1, str2, 1, 0);
		}

		private void objDjVuCtl_AuthRequired(object sender, EventArgs e)
		{
			if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"] == null)
			{
				this.ShowAuthenticationForm();
				return;
			}
			AES aE = new AES();
			string item = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"];
			string str = aE.DLLDecode(item, "0123456789ABCDEF");
			if (!str.Contains("|"))
			{
				this.ShowAuthenticationForm();
				return;
			}
			string str1 = str.Substring(0, str.IndexOf("|"));
			string str2 = str.Substring(str.IndexOf("|") + 1, str.Length - (str.IndexOf("|") + 1));
			if (str1.Equals(string.Empty) || str2.Equals(string.Empty))
			{
				this.ShowAuthenticationForm();
				return;
			}
			this.objDjVuCtl.SetNameAndPass(str1, str2, 1, 0);
		}

		private void objDjVuCtl_AuthSucceeded(object sender, EventArgs e)
		{
			this.bAuthFailed = false;
		}

		private void objDjVuCtl_BirdsEyeViewOpenClose(object sender, _DDjVuCtrlEvents_BirdsEyeViewOpenCloseEvent e)
		{
			if (this.frmParent.miniMapToolStripMenuItem.Enabled && this.frmParent.miniMapToolStripMenuItem.Checked)
			{
				this.frmParent.miniMapToolStripChkUnchk(e.opened);
			}
		}

		private void objDjVuCtl_CopyRegionEvent(object sender, _DDjVuCtrlEvents_CopyRegionEventEvent e)
		{
			MessageBox.Show(this.GetResource("Image Copied", "IMAGE_COPIED", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			this.objDjVuCtl.MouseMode = "Pan";
			this.tsbPicCopy.CheckState = CheckState.Unchecked;
			this.tsbPicPanMode.CheckState = CheckState.Checked;
			this.tsbPicSelectText.CheckState = CheckState.Unchecked;
			this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
			this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
			this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
			this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
			this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
		}

		private void objDjVuCtl_HyperlinkClick(object sender, _DDjVuCtrlEvents_HyperlinkClickEvent e)
		{
			int num;
			int num1;
			int num2;
			int num3;
			string empty = string.Empty;
			string str = string.Empty;
			str = (new URLDecoder()).URLDecode(e.pUrl);
			this.RemoveHighlightOnPicture();
			if (str.StartsWith("./:P::::"))
			{
				empty = str.Substring(8);
				string[] strArrays = new string[] { ":" };
				string[] strArrays1 = empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				if ((int)strArrays1.Length > 2)
				{
					string str1 = strArrays1[0];
					string[] strArrays2 = new string[] { "**" };
					string[] strArrays3 = str1.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < (int)strArrays3.Length; i++)
					{
						string str2 = strArrays3[i];
						string[] strArrays4 = new string[] { "," };
						string[] strArrays5 = str2.Split(strArrays4, StringSplitOptions.RemoveEmptyEntries);
						if ((int)strArrays5.Length == 4)
						{
							if (strArrays5[0].Contains("^"))
							{
								strArrays5[0] = strArrays5[0].Substring(strArrays5[0].IndexOf("^") + 1, strArrays5[0].Length - (strArrays5[0].IndexOf("^") + 1));
							}
							if (int.TryParse(strArrays5[0], out num) && int.TryParse(strArrays5[1], out num1) && int.TryParse(strArrays5[2], out num2) && int.TryParse(strArrays5[3], out num3))
							{
								this.HighlightPicture(num, num1, num2 - num, num3 - num1);
							}
						}
					}
					this.frmParent.HighlightPartslist("LinkNumber", strArrays1[1]);
				}
			}
			if (str.ToLower().Contains("bjump="))
			{
				string empty1 = string.Empty;
				string str3 = "1";
				try
				{
					string[] strArrays6 = new string[] { "|" };
					string[] strArrays7 = str.Split(strArrays6, StringSplitOptions.RemoveEmptyEntries);
					try
					{
						if (strArrays7[(int)strArrays7.Length - 1].Contains(":"))
						{
							string str4 = strArrays7[(int)strArrays7.Length - 1];
							string[] strArrays8 = new string[] { ":" };
							str3 = str4.Split(strArrays8, StringSplitOptions.RemoveEmptyEntries).Last<string>();
							empty1 = strArrays7[(int)strArrays7.Length - 1].Substring(0, strArrays7[(int)strArrays7.Length - 1].LastIndexOf(":"));
						}
						string[] strArrays9 = new string[] { "-o", Program.iniServers[this.frmParent.p_ServerId].sIniKey, strArrays7[1], empty1, str3 };
						string[] strArrays10 = strArrays9;
						if (Global.SecurityLocksOpen(this.frmParent.GetBookNode(strArrays7[1], this.frmParent.p_ServerId), this.frmParent.SchemaNode, this.frmParent.ServerId, this.frmParent))
						{
							this.frmParent.BookJump(strArrays10);
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
			if (str.ToLower().Contains("pjump="))
			{
				string empty2 = string.Empty;
				string str5 = "1";
				try
				{
					string[] strArrays11 = new string[] { "pjump=" };
					string[] strArrays12 = str.Split(strArrays11, StringSplitOptions.RemoveEmptyEntries);
					if (strArrays12[1].Contains(":"))
					{
						string str6 = strArrays12[1];
						string[] strArrays13 = new string[] { ":" };
						str6.Split(strArrays13, StringSplitOptions.RemoveEmptyEntries);
						string str7 = strArrays12[(int)strArrays12.Length - 1];
						string[] strArrays14 = new string[] { ":" };
						str5 = str7.Split(strArrays14, StringSplitOptions.RemoveEmptyEntries).Last<string>();
						empty2 = strArrays12[(int)strArrays12.Length - 1].Substring(0, strArrays12[(int)strArrays12.Length - 1].LastIndexOf(":"));
					}
					string[] bookPublishingId = new string[] { "-o", Program.iniServers[this.frmParent.p_ServerId].sIniKey, this.BookPublishingId, empty2, str5 };
					this.frmParent.PageJump(bookPublishingId);
				}
				catch
				{
				}
			}
		}

		private void objDjVuCtl_PageChange(object sender, _DDjVuCtrlEvents_PageChangeEvent e)
		{
			try
			{
				this.bSelectiveZoom = false;
				if (!Settings.Default.MaintainZoom && this.curPicZoom == string.Empty && this.curPicZoom.Length > 0)
				{
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "300%")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 0;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "150%")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 1;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "100%")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 2;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "75")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 3;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "25")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 4;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "FITWIDTH" || Settings.Default.DefaultPictureZoom.ToUpper() == "WIDTH")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 5;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "FITPAGE")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 6;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "ONE2ONE")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 7;
					}
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "STRETCH")
					{
						this.tsCmbZoom.ComboBox.SelectedIndex = 8;
					}
					this.iSelectedIndex = this.tsCmbZoom.ComboBox.SelectedIndex;
				}
				try
				{
					if (Settings.Default.DefaultPictureZoom.ToUpper() == "PAGE")
					{
						Settings.Default.DefaultPictureZoom = "FITPAGE";
					}
				}
				catch
				{
				}
				if (!Settings.Default.MaintainZoom)
				{
					this.ChangeDJVUZoom(Settings.Default.DefaultPictureZoom);
					this.tsCmbZoom.Text = Settings.Default.DefaultPictureZoom;
				}
				else
				{
					this.ChangeDJVUZoom(this.curPicZoom);
				}
				if (this.ScrollType == ScrollTypes.None)
				{
					if (this.objDjVuCtl.GetPageCount() == 1)
					{
						this.objDjVuCtl.ViewTop = 0;
					}
				}
				else if (this.ScrollType == ScrollTypes.Up)
				{
					int pageCount = this.objDjVuCtl.GetPageCount();
					this.objDjVuCtl.ViewTop = this.objDjVuCtl.ScaledHeight;
					if (pageCount != 0 && pageCount > this.objDjVuCtl.PageNumber)
					{
						this.objDjVuCtl.PageNumber = pageCount;
					}
				}
				else if (this.ScrollType == ScrollTypes.Down)
				{
					this.objDjVuCtl.ViewTop = 0;
				}
				else if (this.ScrollType == ScrollTypes.MultiUp)
				{
					this.objDjVuCtl.ViewTop = this.objDjVuCtl.ScaledHeight;
				}
				else if (this.ScrollType == ScrollTypes.MultiDown)
				{
					this.objDjVuCtl.ViewTop = 0;
				}
				this.ScrollType = ScrollTypes.None;
				this.sPreviousDjVuCtrlHitEnd = string.Empty;
				int num = 1;
				if (this.djVuPageNumber != null)
				{
					int.TryParse(this.djVuPageNumber, out num);
				}
				if (num > 1)
				{
					this.objDjVuCtl.Page = num.ToString();
				}
				this.djVuPageNumber = string.Empty;
				int num1 = 0;
				this.objDjVuCtl.GetPageWidth(ref num1);
				int num2 = 0;
				if (!int.TryParse(ColorTranslator.ToOle(Settings.Default.appHighlightBackColor).ToString(), out num2))
				{
					num2 = 16711680;
				}
				uint num3 = uint.Parse(num2.ToString());
				if (this.highLightText == null || !(this.highLightText.Trim() != string.Empty))
				{
					string tssString = this.frmParent.TssString;
					if (tssString != string.Empty)
					{
						this.objDjVuCtl.HighlightTerm(tssString, false, false, true, num3);
					}
				}
				else
				{
					this.objDjVuCtl.HighlightTerm(this.highLightText, false, false, true, num3);
					this.highLightText = string.Empty;
				}
			}
			catch
			{
				this.ScrollType = ScrollTypes.None;
				this.sPreviousDjVuCtrlHitEnd = string.Empty;
			}
		}

		private void objDjVuCtl_PageRotated(object sender, _DDjVuCtrlEvents_PageRotatedEvent e)
		{
		}

		private void objDjVuCtl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			try
			{
				if (!this.isWorking)
				{
					if (e.KeyCode == Keys.Down)
					{
						if (!this.frmParent.objFrmTreeview.tvBook.SelectedNode.IsExpanded)
						{
							this.frmParent.objFrmTreeview.tvBook.SelectedNode.Expand();
						}
						if (this.frmParent.objFrmTreeview.tvBook.SelectedNode.NextVisibleNode != null)
						{
							this.frmParent.objFrmTreeview.SelectNextNode();
						}
					}
					else if (e.KeyCode == Keys.Up)
					{
						if (!this.frmParent.objFrmTreeview.tvBook.SelectedNode.IsExpanded)
						{
							this.frmParent.objFrmTreeview.tvBook.SelectedNode.Expand();
						}
						if (this.frmParent.objFrmTreeview.tvBook.SelectedNode.PrevVisibleNode != null)
						{
							this.frmParent.objFrmTreeview.SelectPreviousNode();
						}
					}
				}
				if (e.Control && e.KeyCode == Keys.I)
				{
					this.objDjVuCtl.MouseMode = "Copy";
				}
				if (e.Control && e.KeyCode == Keys.J)
				{
					this.objDjVuCtl.MouseMode = "Zoom";
				}
			}
			catch
			{
			}
		}

		private void objDjVuCtl_Scroll(object sender, _DDjVuCtrlEvents_ScrollEvent e)
		{
			try
			{
				if (Program.objAppFeatures.bDjVuScroll && !this.isWorking && e.scrollBy == ScrollBy.Wheel)
				{
					if (e.hitEnd == HitEnd.None && this.ImageScrollBarsVisible())
					{
						this.ScrollType = ScrollTypes.DjVuHandledScroll;
					}
					else if (!this.frmParent.objFrmPartlist.isWorking)
					{
						this.ScrollType = ScrollTypes.None;
					}
				}
			}
			catch
			{
			}
		}

		private void objDjVuCtl_ZoomChange(object sender, _DDjVuCtrlEvents_ZoomChangeEvent e)
		{
			try
			{
				this.prevPicZoom = this.curPicZoom;
				this.curPicZoom = this.objDjVuCtl.Zoom;
				if (e.newzoom.ToString() == string.Empty)
				{
					this.tsCmbZoom.Text = this.objDjVuCtl.Zoom;
				}
				else
				{
					if (this.bSelectiveZoom)
					{
						this.tsCmbZoom.Text = e.newzoom.ToString();
					}
					else if (this.tsCmbZoom.ComboBox.SelectedIndex <= 5)
					{
						this.tsCmbZoom.Text = e.newzoom.ToString();
					}
					this.curPicZoom = e.newzoom.ToString();
				}
				if (this.objDjVuCtl.MouseMode.ToLower() != "pan")
				{
					this.objDjVuCtl.MouseMode = "pan";
					this.tsbPicCopy.CheckState = CheckState.Unchecked;
					this.tsbPicPanMode.CheckState = CheckState.Checked;
					this.tsbPicSelectText.CheckState = CheckState.Unchecked;
					this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
					this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
					this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
					this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
					this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
				}
			}
			catch
			{
			}
		}

		public void OnOffFeatures()
		{
			try
			{
				this.tsbPicZoomSelect.Visible = Program.objAppFeatures.bSelectiveZoom;
				this.tsbPicCopy.Visible = Program.objAppFeatures.bCopyRegion;
				this.tsbFitPage.Visible = Program.objAppFeatures.bFitPage;
				this.tsbPicPanMode.Visible = Program.objAppFeatures.bDjVuPan;
				this.tsbSearchText.Visible = Program.objAppFeatures.bDjVuSearch;
				this.tsbPicSelectText.Visible = Program.objAppFeatures.bDjVuSelectText;
				this.tsbPicZoomIn.Visible = Program.objAppFeatures.bDjVuZoomIn;
				this.tsbPicZoomOut.Visible = Program.objAppFeatures.bDjVuZoomOut;
				this.tsBRotateLeft.Visible = Program.objAppFeatures.bDjVuRotateLeft;
				this.tsBRotateRight.Visible = Program.objAppFeatures.bDjVuRotateRight;
				this.tsbThumbnail.Visible = Program.objAppFeatures.bDjVuNavPan;
				this.toolStripSeparator1.Visible = (Program.objAppFeatures.bDjVuPan || Program.objAppFeatures.bDjVuSelectZoom || Program.objAppFeatures.bFitPage || Program.objAppFeatures.bCopyRegion ? true : Program.objAppFeatures.bDjVuSelectText);
				this.toolStripSeparator2.Visible = Program.objAppFeatures.bDjVuSearch;
				this.toolStripSeparator3.Visible = (Program.objAppFeatures.bDjVuZoomIn ? true : Program.objAppFeatures.bDjVuZoomOut);
				this.toolStripSeparator4.Visible = (Program.objAppFeatures.bDjVuRotateLeft ? true : Program.objAppFeatures.bDjVuRotateRight);
				this.tsCmbZoom.ComboBox.Visible = Program.objAppFeatures.bDjVuZoomCombobox;
				if (!Program.objAppFeatures.bDjVuZoomCombobox)
				{
					this.tsCmbZoom.Alignment = ToolStripItemAlignment.Left;
					this.tsCmbZoom.AutoSize = false;
					System.Drawing.Size size = new System.Drawing.Size(1, 1);
					this.tsCmbZoom.Size = size;
				}
			}
			catch
			{
			}
		}

		public void RemoveHighlightOnPicture()
		{
			try
			{
				this.objDjVuCtl.RemoveAllHighlightRect();
			}
			catch
			{
			}
		}

		public Bitmap Render(ref PDFWrapper pdfDoc)
		{
			Bitmap bitmap;
			try
			{
				if (pdfDoc == null)
				{
					bitmap = null;
				}
				else
				{
					Image.GetThumbnailImageAbort getThumbnailImageAbort = new Image.GetThumbnailImageAbort(frmViewerPicture.ThumbnailCallback);
					string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					folderPath = string.Concat(folderPath, "\\", Application.ProductName);
					folderPath = string.Concat(folderPath, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
					if (!Directory.Exists(folderPath))
					{
						Directory.CreateDirectory(folderPath);
					}
					folderPath = string.Concat(folderPath, "\\tmpViewImage.jpg");
					Bitmap bitmap1 = new Bitmap(pdfDoc.get_PageWidth(), pdfDoc.get_PageHeight());
					pdfDoc.set_ClientBounds(new Rectangle(0, 0, pdfDoc.get_PageWidth(), pdfDoc.get_PageHeight()));
					Graphics graphic = Graphics.FromImage(bitmap1);
					pdfDoc.DrawPageHDC(graphic.GetHdc());
					graphic.ReleaseHdc();
					graphic.Dispose();
					System.Drawing.Size size = this.GenerateImageDimensions(bitmap1.Width, bitmap1.Height, this.pnlPic.Width, this.pnlPic.Height, "Portrait");
					Image thumbnailImage = bitmap1.GetThumbnailImage(size.Width, size.Height, getThumbnailImageAbort, IntPtr.Zero);
					bitmap = new Bitmap(thumbnailImage, size.Width, size.Height);
				}
			}
			catch
			{
				bitmap = null;
			}
			return bitmap;
		}

		public void ScalePicture(float x, float y, int width, int height)
		{
			int num = 0;
			int num1 = 0;
			x += (float)(width / 2);
			y += (float)(height / 2);
			this.objDjVuCtl.GetPageLength(ref num);
			this.objDjVuCtl.GetPageWidth(ref num1);
			if ((float)num > 60f + y && num1 > 0)
			{
				y = ((float)num - y) / (float)num;
				x /= (float)num1;
			}
			x = Math.Abs(x);
			y = Math.Abs(y);
			this.objDjVuCtl.ShowPosition = string.Concat(x, ",", y);
		}

		public void SelectText()
		{
			try
			{
				this.objDjVuCtl.MouseMode = "Text";
				this.tsbPicCopy.CheckState = CheckState.Unchecked;
				this.tsbPicPanMode.CheckState = CheckState.Unchecked;
				this.tsbPicSelectText.CheckState = CheckState.Checked;
				this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
				this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
				this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
				this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
				this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
			}
			catch
			{
			}
		}

		public void SelectZoom()
		{
			this.bSelectiveZoom = true;
			if (this.objDjVuCtl.MouseMode != "Zoom")
			{
				this.objDjVuCtl.MouseMode = "Zoom";
				this.tsbPicCopy.CheckState = CheckState.Unchecked;
				this.tsbPicPanMode.CheckState = CheckState.Unchecked;
				this.tsbPicSelectText.CheckState = CheckState.Unchecked;
				this.tsbPicZoomSelect.CheckState = CheckState.Checked;
			}
			else
			{
				this.objDjVuCtl.MouseMode = "Pan";
				this.tsbPicCopy.CheckState = CheckState.Unchecked;
				this.tsbPicPanMode.CheckState = CheckState.Checked;
				this.tsbPicSelectText.CheckState = CheckState.Unchecked;
				this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
			}
			this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
			this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
			this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
			this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
		}

		public void SetPanMode()
		{
			this.objDjVuCtl.MouseMode = "Pan";
			this.tsbPicCopy.CheckState = CheckState.Unchecked;
			this.tsbPicPanMode.CheckState = CheckState.Checked;
			this.tsbPicSelectText.CheckState = CheckState.Unchecked;
			this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
			this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
			this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
			this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
			this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
		}

		private void SetPicIndex(XmlNodeList objXmlNodeList, string attPicElement, int picIndex)
		{
			if (this.toolStrip1.InvokeRequired)
			{
				ToolStrip toolStrip = this.toolStrip1;
				frmViewerPicture.SetPicIndexDelegate setPicIndexDelegate = new frmViewerPicture.SetPicIndexDelegate(this.SetPicIndex);
				object[] objArray = new object[] { objXmlNodeList, attPicElement, picIndex };
				toolStrip.Invoke(setPicIndexDelegate, objArray);
				return;
			}
			ArrayList arrayLists = new ArrayList();
			int count = 1;
			string empty = string.Empty;
			for (int i = 0; i < objXmlNodeList.Count; i++)
			{
				if (objXmlNodeList[i].Attributes[attPicElement] != null)
				{
					if (!arrayLists.Contains(objXmlNodeList[i].Attributes[attPicElement].Value))
					{
						arrayLists.Add(objXmlNodeList[i].Attributes[attPicElement].Value);
					}
					if (i == picIndex)
					{
						count = arrayLists.Count;
						try
						{
							empty = objXmlNodeList[i].Attributes[this.sPictureTitle].Value;
						}
						catch
						{
							empty = "";
						}
					}
				}
			}
			if (empty != string.Empty)
			{
				this.Text = empty;
			}
			else
			{
				this.UpdatePictureTitle();
			}
			if (arrayLists.Count <= 0)
			{
				this.tsTxtPics.Text = "1/1";
			}
			else
			{
				this.tsTxtPics.Text = string.Concat(count.ToString(), "/", arrayLists.Count);
			}
			arrayLists.Clear();
			arrayLists = null;
			this.frmParent.UpdatePicToolstrip(this.tsBtnPrev.Enabled, this.tsBtnNext.Enabled, this.tsTxtPics.Text);
		}

		private void ShowAuthenticationForm()
		{
			try
			{
				frmDjVuAuthentication _frmDjVuAuthentication = new frmDjVuAuthentication(this.frmParent);
				System.Windows.Forms.DialogResult dialogResult = _frmDjVuAuthentication.ShowDialog(this);
				string empty = string.Empty;
				string str = string.Empty;
				string empty1 = string.Empty;
				AES aE = new AES();
				if (dialogResult == System.Windows.Forms.DialogResult.OK)
				{
					string userId = _frmDjVuAuthentication.UserId;
					string password = _frmDjVuAuthentication.Password;
					empty1 = aE.DLLEncode(string.Concat(_frmDjVuAuthentication.UserId, "|", _frmDjVuAuthentication.Password), "0123456789ABCDEF");
					this.objDjVuCtl.SetNameAndPass(_frmDjVuAuthentication.UserId, _frmDjVuAuthentication.Password, 1, 0);
					Program.iniServers[this.frmParent.ServerId].UpdateItem("SETTINGS", "AUTHENTICATION", empty1);
				}
				else if (!this.frmParent.IsDisposed)
				{
					this.LoadBlankPage(string.Empty);
					this.frmParent.HidePicture();
					this.loadPartslist = false;
				}
			}
			catch
			{
			}
		}

		private void ShowDJVU(bool bState, string sSource)
		{
			if (this.objDjVuCtl.InvokeRequired)
			{
				AxDjVuCtrl axDjVuCtrl = this.objDjVuCtl;
				frmViewerPicture.ShowDJVUDelegate showDJVUDelegate = new frmViewerPicture.ShowDJVUDelegate(this.ShowDJVU);
				object[] objArray = new object[] { bState, sSource };
				axDjVuCtrl.Invoke(showDJVUDelegate, objArray);
				return;
			}
			if (!bState)
			{
				this.objDjVuCtl.SendToBack();
				this.objPicCtl.BringToFront();
				return;
			}
			this.wbPDF.SendToBack();
			this.objDjVuCtl.BringToFront();
			this.objPicCtl.SendToBack();
			this.objDjVuCtl.SRC = sSource;
			frmViewerPicture.djuvCntrlPagecount = this.objDjVuCtl.GetPageCount();
		}

		public void ShowHideMiniMap(bool MiniMapChk)
		{
			if (!MiniMapChk)
			{
				this.objDjVuCtl.ShowBirdsEyeView = false;
			}
			else if (this.frmParent.miniMapToolStripMenuItem.Enabled && !this.objDjVuCtl.ShowBirdsEyeView)
			{
				this.objDjVuCtl.ShowBirdsEyeView = true;
				AxDjVuCtrl axDjVuCtrl = this.objDjVuCtl;
				IntPtr handle = this.frmParent.Handle;
				axDjVuCtrl.SetBirdsEyeViewPos(handle.ToInt32(), 6, 0, 100, 150, 200);
				return;
			}
		}

		public void ShowHidePictureToolbar()
		{
			this.toolStrip1.Visible = (!Settings.Default.ShowPicToolbar ? false : !Program.objAppFeatures.bDjVuToolbar);
		}

		public void ShowHideThumbnail()
		{
			try
			{
				this.objDjVuCtl.ThumbnailPaneEnabled = !this.objDjVuCtl.ThumbnailPaneEnabled;
				if (!this.objDjVuCtl.ThumbnailPaneEnabled)
				{
					this.tsbThumbnail.CheckState = CheckState.Unchecked;
					this.frmParent.tsbThumbnail.CheckState = this.tsbThumbnail.CheckState;
				}
				else
				{
					this.tsbThumbnail.CheckState = CheckState.Checked;
					this.frmParent.tsbThumbnail.CheckState = this.tsbThumbnail.CheckState;
				}
			}
			catch
			{
				MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public void ShowLoading()
		{
			try
			{
				this.objDjVuCtl.SRC = string.Empty;
				this.ShowLoading(this.pnlForm);
			}
			catch
			{
			}
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
					frmViewerPicture.ShowLoadingDelegate showLoadingDelegate = new frmViewerPicture.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		public void TextSearch()
		{
			try
			{
				this.objDjVuCtl.ShowFindDialog();
			}
			catch
			{
				MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		public static bool ThumbnailCallback()
		{
			return true;
		}

		private void tsbAddPictureMemo_Click(object sender, EventArgs e)
		{
			this.frmParent.ShowPictureMemos(false);
		}

		private void tsbPicCopy_Click(object sender, EventArgs e)
		{
			this.CopyImage();
		}

		private void tsbPicPanMode_Click(object sender, EventArgs e)
		{
			this.SetPanMode();
		}

		private void tsbPicSelectText_Click(object sender, EventArgs e)
		{
			this.SelectText();
		}

		private void tsbPicZoomIn_Click(object sender, EventArgs e)
		{
			this.ZoomIn();
		}

		private void tsbPicZoomOut_Click(object sender, EventArgs e)
		{
			this.ZoomOut();
		}

		private void tsbPicZoomSelect_Click(object sender, EventArgs e)
		{
			this.SelectZoom();
		}

		public void tsBRotateLeft_Click(object sender, EventArgs e)
		{
			AxDjVuCtrl rotation = this.objDjVuCtl;
			rotation.Rotation = rotation.Rotation + 90;
			_DDjVuCtrlEvents_PageRotatedEvent _DDjVuCtrlEventsPageRotatedEvent = new _DDjVuCtrlEvents_PageRotatedEvent(1);
			this.objDjVuCtl_PageRotated(this.objDjVuCtl, _DDjVuCtrlEventsPageRotatedEvent);
		}

		public void tsBRotateRight_Click(object sender, EventArgs e)
		{
			AxDjVuCtrl rotation = this.objDjVuCtl;
			rotation.Rotation = rotation.Rotation - 90;
			_DDjVuCtrlEvents_PageRotatedEvent _DDjVuCtrlEventsPageRotatedEvent = new _DDjVuCtrlEvents_PageRotatedEvent(1);
			this.objDjVuCtl_PageRotated(this.objDjVuCtl, _DDjVuCtrlEventsPageRotatedEvent);
		}

		private void tsbSearchText_Click(object sender, EventArgs e)
		{
			try
			{
				this.TextSearch();
			}
			catch
			{
				MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void tsbThumbnail_Click(object sender, EventArgs e)
		{
			try
			{
				this.ShowHideThumbnail();
			}
			catch
			{
				MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void tsBtnFitPage_Click(object sender, EventArgs e)
		{
			this.FitPage();
		}

		public void tsBtnNext_Click(object sender, EventArgs e)
		{
			if (this.curPageSchema != null && this.curPageNode != null)
			{
				string text = this.tsTxtPics.Text;
				if (text.Contains("/"))
				{
					text = text.Substring(0, text.IndexOf("/"));
					try
					{
						this.LoadPicture(this.curPageSchema, this.curPageNode, int.Parse(text), 0);
					}
					catch
					{
					}
				}
			}
		}

		public void tsBtnPrev_Click(object sender, EventArgs e)
		{
			if (this.curPageSchema != null && this.curPageNode != null)
			{
				string text = this.tsTxtPics.Text;
				if (text.Contains("/"))
				{
					text = text.Substring(0, text.IndexOf("/"));
					try
					{
						this.LoadPicture(this.curPageSchema, this.curPageNode, int.Parse(text) - 2, 0);
					}
					catch
					{
					}
				}
			}
		}

		private void tsCmbZoom_KeyDown(object sender, KeyEventArgs e)
		{
			this.bNonNumberEntered = false;
			if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && e.KeyCode != Keys.Back && e.KeyCode != Keys.Return)
			{
				this.bNonNumberEntered = true;
			}
			if (Control.ModifierKeys == Keys.Shift)
			{
				this.bNonNumberEntered = true;
			}
		}

		private void tsCmbZoom_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (this.bNonNumberEntered)
			{
				e.Handled = true;
			}
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				this.tsCmbZoom_Leave(null, null);
			}
		}

		private void tsCmbZoom_Leave(object sender, EventArgs e)
		{
			try
			{
				string text = this.tsCmbZoom.ComboBox.Text;
				if (text.Contains<char>('%'))
				{
					text = text.Replace("%", "");
				}
				if (text == string.Empty)
				{
					this.objDjVuCtl.Zoom = Settings.Default.DefaultPictureZoom;
					this.tsCmbZoom.Text = this.objDjVuCtl.Zoom;
				}
				else
				{
					int num = Convert.ToInt32(text);
					if (num > 1200)
					{
						text = "1200";
					}
					else if (num < 25)
					{
						text = "25";
					}
					text = string.Concat(text, "%");
					this.tsCmbZoom.Text = text;
					this.objDjVuCtl.Zoom = this.tsCmbZoom.ComboBox.Text;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void tsCmbZoom_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.bSelectiveZoom = false;
				int selectedIndex = this.tsCmbZoom.ComboBox.SelectedIndex;
				if (selectedIndex == -1)
				{
					selectedIndex = this.iSelectedIndex;
				}
				if (selectedIndex == 5)
				{
					this.objDjVuCtl.Zoom = "132";
					this.objDjVuCtl.Zoom = "WIDTH";
				}
				else if (selectedIndex == 6)
				{
					this.objDjVuCtl.Zoom = "FITPAGE";
				}
				else if (selectedIndex == 7)
				{
					this.objDjVuCtl.Zoom = "300";
					this.objDjVuCtl.Zoom = "ONE2ONE";
				}
				else if (selectedIndex != 8)
				{
					this.tsCmbZoom.ComboBox.SelectedIndex = selectedIndex;
				}
				else
				{
					this.objDjVuCtl.Zoom = "STRETCH";
				}
				this.tsCmbZoom.Text = this.tsCmbZoom.ComboBox.SelectedItem.ToString();
				this.objDjVuCtl.Zoom = this.tsCmbZoom.ComboBox.SelectedItem.ToString();
				this.iSelectedIndex = this.tsCmbZoom.ComboBox.SelectedIndex;
			}
			catch (Exception exception)
			{
			}
		}

		public void UpdatePictureTitle()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new frmViewerPicture.UpdatePictureTitleDelegate(this.UpdatePictureTitle));
				return;
			}
			if (this.frmParent.objFrmTreeview != null && this.frmParent.objFrmTreeview.CurrentNodeText != string.Empty)
			{
				this.Text = this.frmParent.objFrmTreeview.CurrentNodeText;
			}
		}

		private void UpdateStatus()
		{
			if (!this.frmParent.InvokeRequired)
			{
				this.frmParent.UpdateStatus(this.statusText);
				return;
			}
			frmViewer _frmViewer = this.frmParent;
			frmViewerPicture.StatusDelegate statusDelegate = new frmViewerPicture.StatusDelegate(this.frmParent.UpdateStatus);
			object[] objArray = new object[] { this.statusText };
			_frmViewer.Invoke(statusDelegate, objArray);
		}

		public void ZoomFitPage(bool bFitPage)
		{
			try
			{
				this.bSelectiveZoom = false;
				if (bFitPage)
				{
					this.objDjVuCtl.Zoom = this.objDjVuCtl.FitPagePercent;
				}
				else if (this.objDjVuCtl.Zoom.ToUpper().Contains("FITPAGE"))
				{
					this.objDjVuCtl.Zoom = this.prevPicZoom;
				}
			}
			catch
			{
			}
		}

		public void ZoomIn()
		{
			try
			{
				this.bSelectiveZoom = false;
				string empty = string.Empty;
				int @default = 0;
				int num = 1200;
				empty = this.objDjVuCtl.Zoom;
				if (this.curPicZoom.ToUpper() == "FITPAGE" || this.curPicZoom.ToUpper() == "STRETCH")
				{
					string fitPagePercent = this.objDjVuCtl.FitPagePercent;
					string str = fitPagePercent;
					this.curPicZoom = fitPagePercent;
					empty = str;
				}
				else if (this.curPicZoom.ToUpper() == "WIDTH" || this.curPicZoom.ToUpper() == "ONE2ONE")
				{
					string str1 = this.objDjVuCtl.Zoom.Substring(this.objDjVuCtl.Zoom.IndexOf(",") + 1);
					string str2 = str1;
					this.curPicZoom = str1;
					empty = str2;
				}
				empty = empty.Substring(empty.IndexOf(",") + 1);
				empty = empty.Replace("%", string.Empty);
				@default = int.Parse(empty);
				if (@default + Settings.Default.appZoomStep > num)
				{
					this.objDjVuCtl.Zoom = num.ToString();
				}
				else
				{
					@default += Settings.Default.appZoomStep;
					this.objDjVuCtl.Zoom = @default.ToString();
				}
				this.curPicZoom = this.objDjVuCtl.Zoom;
				this.prevPicZoom = this.curPicZoom;
				this.tsCmbZoom.Text = this.curPicZoom;
			}
			catch
			{
			}
		}

		public void ZoomOut()
		{
			try
			{
				this.bSelectiveZoom = false;
				string empty = string.Empty;
				string zoom = string.Empty;
				int @default = 0;
				int num = 25;
				zoom = this.objDjVuCtl.Zoom;
				if (this.curPicZoom.ToUpper() == "FITPAGE" || this.curPicZoom.ToUpper() == "STRETCH")
				{
					string fitPagePercent = this.objDjVuCtl.FitPagePercent;
					string str = fitPagePercent;
					this.curPicZoom = fitPagePercent;
					zoom = str;
				}
				else if (this.curPicZoom.ToUpper() == "WIDTH" || this.curPicZoom.ToUpper() == "ONE2ONE")
				{
					string str1 = this.objDjVuCtl.Zoom.Substring(this.objDjVuCtl.Zoom.IndexOf(",") + 1);
					string str2 = str1;
					this.curPicZoom = str1;
					zoom = str2;
				}
				zoom = zoom.Substring(zoom.IndexOf(",") + 1);
				zoom = zoom.Replace("%", string.Empty);
				@default = int.Parse(zoom);
				if (@default - Settings.Default.appZoomStep <= num)
				{
					this.objDjVuCtl.Zoom = num.ToString();
				}
				else
				{
					@default -= Settings.Default.appZoomStep;
					this.objDjVuCtl.Zoom = @default.ToString();
				}
				this.curPicZoom = this.objDjVuCtl.Zoom;
				this.prevPicZoom = this.curPicZoom;
				this.tsCmbZoom.Text = this.curPicZoom;
			}
			catch
			{
			}
		}

		private delegate void ChangeDJVUZoomDelegate(string sZoom);

		private delegate void ChangeTiffSrcDelegate(string Src);

		private delegate void EnableAddMemoDelegate(bool value);

		private delegate void HideLoadingDelegate(Panel parentPanel);

		public delegate void HideLoadingDelegate1();

		private delegate void SetPicIndexDelegate(XmlNodeList objXmlNodeList, string attPicElement, int picIndex);

		private delegate void ShowDJVUDelegate(bool bState, string sSource);

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void ShowPDFDelegate(string sLocalFile);

		private delegate void StatusDelegate(string status);

		private delegate void UpdatePictureTitleDelegate();
	}
}
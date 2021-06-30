// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmViewerPicture
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using AxDjVuCtrlLib;
using DjVuCtrlLib;
using GSPcLocalViewer.Properties;
using PDFLibNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
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

    public frmViewerPicture(frmViewer frm)
    {
      this.InitializeComponent();
      this.curPageSchema = (XmlNode) null;
      this.curPageNode = (XmlNode) null;
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
          this.tsCmbZoom.Text = this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX);
        else if (Settings.Default.DefaultPictureZoom.ToUpper().Contains("FITPAGE"))
          this.tsCmbZoom.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX);
        else if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("ONE2ONE"))
          this.tsCmbZoom.Text = this.GetResource("One To One", "ONE_TO_ONE", ResourceType.COMBO_BOX);
        else if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("STRETCH"))
          this.tsCmbZoom.Text = this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX);
        else
          this.tsCmbZoom.Text = Settings.Default.DefaultPictureZoom;
      }
      catch (Exception ex)
      {
      }
      this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
      this.MouseWheel += new MouseEventHandler(this.MouseScroll);
      this.sPreviousImage = string.Empty;
      this.objDownloader = new Download(this.frmParent);
      this.frmParent.ISPDF = false;
      this.tsbAddPictureMemo.Visible = Program.objAppFeatures.bMemo;
    }

    private void frmViewerPicture_Load(object sender, EventArgs e)
    {
      this.objDjVuCtl.ShowBirdsEyeView = Program.objAppFeatures.bMiniMap;
      this.OnOffFeatures();
    }

    private void frmViewerPicture_VisibleChanged(object sender, EventArgs e)
    {
      try
      {
        this.frmParent.pictureToolStripMenuItem.Checked = this.Visible;
        this.frmParent.miniMapToolStripMenuItem.Enabled = this.Visible;
        this.frmParent.bPictureClosed = !this.Visible;
        this.objDjVuCtl.ShowBirdsEyeView = this.Visible && this.frmParent.miniMapToolStripMenuItem.Checked;
      }
      catch
      {
      }
    }

    private void frmViewerPicture_SizeChanged(object sender, EventArgs e)
    {
      this.MouseWheel += new MouseEventHandler(this.MouseScroll);
    }

    private void tsbPicPanMode_Click(object sender, EventArgs e)
    {
      this.SetPanMode();
    }

    private void tsbPicZoomSelect_Click(object sender, EventArgs e)
    {
      this.SelectZoom();
    }

    private void tsBtnFitPage_Click(object sender, EventArgs e)
    {
      this.FitPage();
    }

    private void tsbPicZoomIn_Click(object sender, EventArgs e)
    {
      this.ZoomIn();
    }

    private void tsbPicZoomOut_Click(object sender, EventArgs e)
    {
      this.ZoomOut();
    }

    private void tsbPicCopy_Click(object sender, EventArgs e)
    {
      this.CopyImage();
    }

    private void tsbPicSelectText_Click(object sender, EventArgs e)
    {
      this.SelectText();
    }

    public void tsBRotateRight_Click(object sender, EventArgs e)
    {
      this.objDjVuCtl.Rotation -= 90;
      this.objDjVuCtl_PageRotated((object) this.objDjVuCtl, new _DDjVuCtrlEvents_PageRotatedEvent(1));
    }

    public void tsBRotateLeft_Click(object sender, EventArgs e)
    {
      this.objDjVuCtl.Rotation += 90;
      this.objDjVuCtl_PageRotated((object) this.objDjVuCtl, new _DDjVuCtrlEvents_PageRotatedEvent(1));
    }

    public void tsBtnNext_Click(object sender, EventArgs e)
    {
      if (this.curPageSchema == null || this.curPageNode == null)
        return;
      string text = this.tsTxtPics.Text;
      if (!text.Contains("/"))
        return;
      string s = text.Substring(0, text.IndexOf("/"));
      try
      {
        this.LoadPicture(this.curPageSchema, this.curPageNode, int.Parse(s), 0);
      }
      catch
      {
      }
    }

    public void tsBtnPrev_Click(object sender, EventArgs e)
    {
      if (this.curPageSchema == null || this.curPageNode == null)
        return;
      string text = this.tsTxtPics.Text;
      if (!text.Contains("/"))
        return;
      string s = text.Substring(0, text.IndexOf("/"));
      try
      {
        this.LoadPicture(this.curPageSchema, this.curPageNode, int.Parse(s) - 2, 0);
      }
      catch
      {
      }
    }

    private void tsbAddPictureMemo_Click(object sender, EventArgs e)
    {
      this.frmParent.ShowPictureMemos(false);
    }

    public void MouseScroll(object sender, MouseEventArgs e)
    {
      try
      {
        string text = this.tsTxtPics.Text;
        if (!Program.objAppFeatures.bDjVuScroll || this.isWorking || (this.frmParent.objFrmPartlist.isWorking || this.ScrollType != ScrollTypes.None))
          return;
        this.frmParent.objFrmTreeview.tvBook.SelectedNode.Expand();
        string str;
        if (e.Delta < 0)
        {
          if (text.Contains("/") && (!text.EndsWith((this.curPicIndex + 1).ToString()) && Settings.Default.MouseScrollContents))
          {
            str = text.Substring(0, text.IndexOf("/"));
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
            ++this.objDjVuCtl.PageNumber;
            this.ScrollType = ScrollTypes.MultiDown;
          }
          else
          {
            if (!Settings.Default.MouseScrollContents || this.frmParent.objFrmTreeview.tvBook.SelectedNode.NextVisibleNode == null)
              return;
            this.frmParent.objFrmTreeview.tvBook.SelectedNode.NextVisibleNode.Expand();
            this.frmParent.objFrmTreeview.SelectNextNode();
            this.ScrollType = ScrollTypes.Down;
          }
        }
        else if (text.Contains("/") && this.curPicIndex > 0)
        {
          str = text.Substring(0, text.IndexOf("/"));
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
          --this.objDjVuCtl.PageNumber;
        }
        else
        {
          if (!Settings.Default.MouseScrollContents || this.frmParent.objFrmTreeview.tvBook.SelectedNode.PrevVisibleNode == null)
            return;
          this.frmParent.objFrmTreeview.tvBook.SelectedNode.PrevVisibleNode.Expand();
          this.frmParent.objFrmTreeview.SelectPreviousNode();
          this.ScrollType = ScrollTypes.Up;
        }
      }
      catch
      {
        this.ScrollType = ScrollTypes.None;
      }
    }

    private void objDjVuCtl_HyperlinkClick(object sender, _DDjVuCtrlEvents_HyperlinkClickEvent e)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string str1 = new URLDecoder().URLDecode(e.pUrl);
      this.RemoveHighlightOnPicture();
      if (str1.StartsWith("./:P::::"))
      {
        string[] strArray1 = str1.Substring(8).Split(new string[1]
        {
          ":"
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray1.Length > 2)
        {
          string str2 = strArray1[0];
          string[] separator1 = new string[1]{ "**" };
          int num1 = 1;
          foreach (string str3 in str2.Split(separator1, (StringSplitOptions) num1))
          {
            string[] separator2 = new string[1]{ "," };
            int num2 = 1;
            string[] strArray2 = str3.Split(separator2, (StringSplitOptions) num2);
            if (strArray2.Length == 4)
            {
              if (strArray2[0].Contains("^"))
                strArray2[0] = strArray2[0].Substring(strArray2[0].IndexOf("^") + 1, strArray2[0].Length - (strArray2[0].IndexOf("^") + 1));
              int result1;
              int result2;
              int result3;
              int result4;
              if (int.TryParse(strArray2[0], out result1) && int.TryParse(strArray2[1], out result2) && (int.TryParse(strArray2[2], out result3) && int.TryParse(strArray2[3], out result4)))
                this.HighlightPicture(result1, result2, result3 - result1, result4 - result2);
            }
          }
          this.frmParent.HighlightPartslist("LinkNumber", strArray1[1]);
        }
      }
      if (str1.ToLower().Contains("bjump="))
      {
        string str2 = string.Empty;
        string str3 = "1";
        try
        {
          string[] strArray = str1.Split(new string[1]
          {
            "|"
          }, StringSplitOptions.RemoveEmptyEntries);
          try
          {
            if (strArray[strArray.Length - 1].Contains(":"))
            {
              str3 = ((IEnumerable<string>) strArray[strArray.Length - 1].Split(new string[1]
              {
                ":"
              }, StringSplitOptions.RemoveEmptyEntries)).Last<string>();
              str2 = strArray[strArray.Length - 1].Substring(0, strArray[strArray.Length - 1].LastIndexOf(":"));
            }
            string[] sArgs = new string[5]
            {
              "-o",
              Program.iniServers[this.frmParent.p_ServerId].sIniKey,
              strArray[1],
              str2,
              str3
            };
            if (Global.SecurityLocksOpen(this.frmParent.GetBookNode(strArray[1], this.frmParent.p_ServerId), this.frmParent.SchemaNode, this.frmParent.ServerId, this.frmParent))
              this.frmParent.BookJump(sArgs);
          }
          catch
          {
          }
        }
        catch
        {
        }
      }
      if (!str1.ToLower().Contains("pjump="))
        return;
      string str4 = string.Empty;
      string str5 = "1";
      try
      {
        string[] strArray = str1.Split(new string[1]
        {
          "pjump="
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray[1].Contains(":"))
        {
          strArray[1].Split(new string[1]{ ":" }, StringSplitOptions.RemoveEmptyEntries);
          str5 = ((IEnumerable<string>) strArray[strArray.Length - 1].Split(new string[1]
          {
            ":"
          }, StringSplitOptions.RemoveEmptyEntries)).Last<string>();
          str4 = strArray[strArray.Length - 1].Substring(0, strArray[strArray.Length - 1].LastIndexOf(":"));
        }
        this.frmParent.PageJump(new string[5]
        {
          "-o",
          Program.iniServers[this.frmParent.p_ServerId].sIniKey,
          this.BookPublishingId,
          str4,
          str5
        });
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
        if (e.newzoom.ToString() != string.Empty)
        {
          if (this.bSelectiveZoom)
            this.tsCmbZoom.Text = e.newzoom.ToString();
          else if (this.tsCmbZoom.ComboBox.SelectedIndex <= 5)
            this.tsCmbZoom.Text = e.newzoom.ToString();
          this.curPicZoom = e.newzoom.ToString();
        }
        else
          this.tsCmbZoom.Text = this.objDjVuCtl.Zoom;
        if (!(this.objDjVuCtl.MouseMode.ToLower() != "pan"))
          return;
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
      catch
      {
      }
    }

    private void objDjVuCtl_CopyRegionEvent(object sender, _DDjVuCtrlEvents_CopyRegionEventEvent e)
    {
      int num = (int) MessageBox.Show(this.GetResource("Image Copied", "IMAGE_COPIED", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

    private void objDjVuCtl_AuthFailed(object sender, EventArgs e)
    {
      if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"] != null && !this.bAuthFailed)
      {
        string str = new AES().DLLDecode(Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"], "0123456789ABCDEF");
        if (str.Contains("|"))
        {
          string name = str.Substring(0, str.IndexOf("|"));
          string password = str.Substring(str.IndexOf("|") + 1, str.Length - (str.IndexOf("|") + 1));
          this.bAuthFailed = true;
          if (!name.Equals(string.Empty) && !password.Equals(string.Empty))
            this.objDjVuCtl.SetNameAndPass(name, password, 1, 0);
          else
            this.ShowAuthenticationForm();
        }
        else
        {
          this.bAuthFailed = true;
          this.ShowAuthenticationForm();
        }
      }
      else
      {
        this.bAuthFailed = true;
        this.ShowAuthenticationForm();
      }
    }

    private void objDjVuCtl_AuthRequired(object sender, EventArgs e)
    {
      if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"] != null)
      {
        string str = new AES().DLLDecode(Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "AUTHENTICATION"], "0123456789ABCDEF");
        if (str.Contains("|"))
        {
          string name = str.Substring(0, str.IndexOf("|"));
          string password = str.Substring(str.IndexOf("|") + 1, str.Length - (str.IndexOf("|") + 1));
          if (!name.Equals(string.Empty) && !password.Equals(string.Empty))
            this.objDjVuCtl.SetNameAndPass(name, password, 1, 0);
          else
            this.ShowAuthenticationForm();
        }
        else
          this.ShowAuthenticationForm();
      }
      else
        this.ShowAuthenticationForm();
    }

    private void objDjVuCtl_PageChange(object sender, _DDjVuCtrlEvents_PageChangeEvent e)
    {
      try
      {
        this.bSelectiveZoom = false;
        if (!Settings.Default.MaintainZoom)
        {
          if (this.curPicZoom == string.Empty)
          {
            if (this.curPicZoom.Length > 0)
            {
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "300%")
                this.tsCmbZoom.ComboBox.SelectedIndex = 0;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "150%")
                this.tsCmbZoom.ComboBox.SelectedIndex = 1;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "100%")
                this.tsCmbZoom.ComboBox.SelectedIndex = 2;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "75")
                this.tsCmbZoom.ComboBox.SelectedIndex = 3;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "25")
                this.tsCmbZoom.ComboBox.SelectedIndex = 4;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "FITWIDTH" || Settings.Default.DefaultPictureZoom.ToUpper() == "WIDTH")
                this.tsCmbZoom.ComboBox.SelectedIndex = 5;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "FITPAGE")
                this.tsCmbZoom.ComboBox.SelectedIndex = 6;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "ONE2ONE")
                this.tsCmbZoom.ComboBox.SelectedIndex = 7;
              if (Settings.Default.DefaultPictureZoom.ToUpper() == "STRETCH")
                this.tsCmbZoom.ComboBox.SelectedIndex = 8;
              this.iSelectedIndex = this.tsCmbZoom.ComboBox.SelectedIndex;
            }
          }
        }
        try
        {
          if (Settings.Default.DefaultPictureZoom.ToUpper() == "PAGE")
            Settings.Default.DefaultPictureZoom = "FITPAGE";
        }
        catch
        {
        }
        if (Settings.Default.MaintainZoom)
        {
          this.ChangeDJVUZoom(this.curPicZoom);
        }
        else
        {
          this.ChangeDJVUZoom(Settings.Default.DefaultPictureZoom);
          this.tsCmbZoom.Text = Settings.Default.DefaultPictureZoom;
        }
        if (this.ScrollType != ScrollTypes.None)
        {
          if (this.ScrollType == ScrollTypes.Up)
          {
            int pageCount = this.objDjVuCtl.GetPageCount();
            this.objDjVuCtl.ViewTop = this.objDjVuCtl.ScaledHeight;
            if (pageCount != 0 && pageCount > this.objDjVuCtl.PageNumber)
              this.objDjVuCtl.PageNumber = pageCount;
          }
          else if (this.ScrollType == ScrollTypes.Down)
            this.objDjVuCtl.ViewTop = 0;
          else if (this.ScrollType == ScrollTypes.MultiUp)
            this.objDjVuCtl.ViewTop = this.objDjVuCtl.ScaledHeight;
          else if (this.ScrollType == ScrollTypes.MultiDown)
            this.objDjVuCtl.ViewTop = 0;
        }
        else if (this.objDjVuCtl.GetPageCount() == 1)
          this.objDjVuCtl.ViewTop = 0;
        this.ScrollType = ScrollTypes.None;
        this.sPreviousDjVuCtrlHitEnd = string.Empty;
        int result1 = 1;
        if (this.djVuPageNumber != null)
          int.TryParse(this.djVuPageNumber, out result1);
        if (result1 > 1)
          this.objDjVuCtl.Page = result1.ToString();
        this.djVuPageNumber = string.Empty;
        int width = 0;
        this.objDjVuCtl.GetPageWidth(ref width);
        int result2 = 0;
        if (!int.TryParse(ColorTranslator.ToOle(Settings.Default.appHighlightBackColor).ToString(), out result2))
          result2 = 16711680;
        uint color = uint.Parse(result2.ToString());
        if (this.highLightText != null && this.highLightText.Trim() != string.Empty)
        {
          this.objDjVuCtl.HighlightTerm(this.highLightText, false, false, true, color);
          this.highLightText = string.Empty;
        }
        else
        {
          string tssString = this.frmParent.TssString;
          if (!(tssString != string.Empty))
            return;
          this.objDjVuCtl.HighlightTerm(tssString, false, false, true, color);
        }
      }
      catch
      {
        this.ScrollType = ScrollTypes.None;
        this.sPreviousDjVuCtrlHitEnd = string.Empty;
      }
    }

    private void objDjVuCtl_BirdsEyeViewOpenClose(object sender, _DDjVuCtrlEvents_BirdsEyeViewOpenCloseEvent e)
    {
      if (!this.frmParent.miniMapToolStripMenuItem.Enabled || !this.frmParent.miniMapToolStripMenuItem.Checked)
        return;
      this.frmParent.miniMapToolStripChkUnchk(e.opened);
    }

    private void objDjVuCtl_Scroll(object sender, _DDjVuCtrlEvents_ScrollEvent e)
    {
      try
      {
        if (!Program.objAppFeatures.bDjVuScroll || this.isWorking || e.scrollBy != ScrollBy.Wheel)
          return;
        if (e.hitEnd == HitEnd.None && this.ImageScrollBarsVisible())
        {
          this.ScrollType = ScrollTypes.DjVuHandledScroll;
        }
        else
        {
          if (this.frmParent.objFrmPartlist.isWorking)
            return;
          this.ScrollType = ScrollTypes.None;
        }
      }
      catch
      {
      }
    }

    private void objDjVuCtl_AuthSucceeded(object sender, EventArgs e)
    {
      this.bAuthFailed = false;
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
              this.frmParent.objFrmTreeview.tvBook.SelectedNode.Expand();
            if (this.frmParent.objFrmTreeview.tvBook.SelectedNode.NextVisibleNode != null)
              this.frmParent.objFrmTreeview.SelectNextNode();
          }
          else if (e.KeyCode == Keys.Up)
          {
            if (!this.frmParent.objFrmTreeview.tvBook.SelectedNode.IsExpanded)
              this.frmParent.objFrmTreeview.tvBook.SelectedNode.Expand();
            if (this.frmParent.objFrmTreeview.tvBook.SelectedNode.PrevVisibleNode != null)
              this.frmParent.objFrmTreeview.SelectPreviousNode();
          }
        }
        if (e.Control && e.KeyCode == Keys.I)
          this.objDjVuCtl.MouseMode = "Copy";
        if (!e.Control || e.KeyCode != Keys.J)
          return;
        this.objDjVuCtl.MouseMode = "Zoom";
      }
      catch
      {
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.frmParent.bObjFrmPictureClosed = false;
      object[] objArray = (object[]) e.Argument;
      XmlNode xSchemaNode = (XmlNode) objArray[0];
      XmlNode xmlNode = (XmlNode) objArray[1];
      int picIndex = (int) objArray[2];
      int num = (int) objArray[3];
      this.loadPartslist = true;
      bool flag = false;
      string surl1 = string.Empty;
      string str1 = string.Empty;
      DateTime dateTime = new DateTime();
      string str2 = string.Empty;
      string str3 = string.Empty;
      this.attPicElement = string.Empty;
      this.attListElement = string.Empty;
      this.attUpdateDatePicElement = string.Empty;
      this.BookPublishingId = this.frmParent.BookPublishingId;
      for (int index = 0; index < xSchemaNode.Attributes.Count; ++index)
      {
        if (xSchemaNode.Attributes[index].Value.ToUpper().Equals("PICTUREFILE"))
          this.attPicElement = xSchemaNode.Attributes[index].Name;
        if (xSchemaNode.Attributes[index].Value.ToUpper().Equals("PARTSLISTFILE"))
          this.attListElement = xSchemaNode.Attributes[index].Name;
        if (xSchemaNode.Attributes[index].Value.ToUpper().Equals("UPDATEDATE"))
        {
          this.attUpdateDatePicElement = xSchemaNode.Attributes[index].Name;
          this.attUpdateDateListElement = xSchemaNode.Attributes[index].Name;
        }
        if (xSchemaNode.Attributes[index].Value.ToUpper().Equals("UPDATEDATEPIC"))
          str2 = xSchemaNode.Attributes[index].Name;
        if (xSchemaNode.Attributes[index].Value.ToUpper().Equals("UPDATEDATEPL"))
          str3 = xSchemaNode.Attributes[index].Name;
        if (xSchemaNode.Attributes[index].Value.ToUpper().Equals("PICTURETITLE"))
          this.sPictureTitle = xSchemaNode.Attributes[index].Name;
      }
      if (str2 != string.Empty)
        this.attUpdateDatePicElement = str2;
      if (str3 != string.Empty)
        this.attUpdateDateListElement = str3;
      if (this.attPicElement == string.Empty || this.attListElement == string.Empty)
      {
        this.bgWorker.CancelAsync();
        MessageHandler.ShowWarning(this.GetResource("(E-VPC-EM001) Not in required format", "(E-VPC-EM001)_FORMAT", ResourceType.POPUP_MESSAGE));
      }
      else
      {
        XmlNodeList xNodeList = xmlNode.SelectNodes("//Pic[@" + this.attPicElement + "]");
        foreach (XmlNode filterPics in this.frmParent.FilterPicsList(xSchemaNode, xNodeList))
        {
          if (filterPics.Attributes.Count == 0)
            xmlNode.RemoveChild(filterPics);
        }
        XmlNodeList objXmlNodeList = xmlNode.SelectNodes("//Pic[not(@" + this.attPicElement + " = preceding-sibling::Pic/@" + this.attPicElement + ")]");
        if (objXmlNodeList.Count > 0)
        {
          try
          {
            this.statusText = this.GetResource("Loading picture..", "LOADING_PICTURE", ResourceType.STATUS_MESSAGE);
            this.UpdateStatus();
            string[] strArray = new string[objXmlNodeList.Count];
            for (int index = 0; index < objXmlNodeList.Count; ++index)
              strArray[index] = objXmlNodeList[index].Attributes[this.attPicElement] == null ? string.Empty : objXmlNodeList[index].Attributes[this.attPicElement].Value;
            if (Program.objAppFeatures.bDjVuScroll && this.ScrollType == ScrollTypes.Up)
              picIndex = objXmlNodeList.Count - 1;
            if (picIndex >= objXmlNodeList.Count)
              picIndex = 0;
            this.curPicIndex = picIndex;
            this.curPicName = strArray[picIndex];
            this.SetPicIndex(objXmlNodeList, this.attPicElement, picIndex);
            if (!strArray[picIndex].Equals(string.Empty))
            {
              if (!strArray[picIndex].ToUpper().EndsWith(".DJVU") && !strArray[picIndex].ToUpper().EndsWith(".PDF") && !strArray[picIndex].ToUpper().EndsWith(".TIF"))
                strArray[picIndex] = strArray[picIndex] + ".djvu";
              string str4 = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
              if (!str4.EndsWith("/"))
                str4 += "/";
              surl1 = str4 + this.BookPublishingId + "/" + strArray[picIndex];
              string path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey + "\\" + this.BookPublishingId;
              if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
              str1 = path + "\\" + strArray[picIndex];
              try
              {
                dateTime = DateTime.Parse(objXmlNodeList[picIndex].Attributes[this.attUpdateDatePicElement].Value, (IFormatProvider) new CultureInfo("fr-FR", false));
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
          if (surl1 != string.Empty && str1 != string.Empty)
          {
            int result = 0;
            if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
              result = 0;
            if (File.Exists(str1))
            {
              if (result == 0)
                flag = true;
              else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str1), dateTime, result))
                flag = true;
            }
            else
              flag = true;
            if (flag && !Program.objAppMode.bWorkingOffline)
            {
              if (this.objDownloader.DownloadFile(surl1, str1))
              {
                try
                {
                  if (dateTime.Year != 1)
                    Global.ChangeDjVUModifiedDate(str1, dateTime);
                }
                catch
                {
                }
              }
            }
            if (Program.objAppMode.bWorkingOffline && !File.Exists(str1))
              this.loadPartslist = false;
            if (File.Exists(str1))
            {
              if (!this.frmParent.IsDisposed)
              {
                if (str1.ToUpper().EndsWith("PDF"))
                {
                  this.ShowLoading();
                  this.ChangePDFSrc(str1);
                }
                else if (str1.ToUpper().EndsWith("DJVU"))
                  this.ShowDJVU(true, str1);
                else if (str1.ToUpper().EndsWith("TIF"))
                {
                  this.ShowDJVU(false, string.Empty);
                  this.ChangeTiffSrc(str1);
                }
                else
                  this.ShowDJVU(true, str1);
              }
            }
            else if (!this.frmParent.IsDisposed)
              this.LoadBlankPage(str1);
            this.frmParent.bPictureClosed = false;
            this.frmParent.ShowPicture();
          }
          else if (!this.frmParent.IsDisposed)
            this.frmParent.HidePicture();
        }
        else
        {
          if (!this.frmParent.IsDisposed)
            this.LoadBlankPage(str1);
          this.loadPartslist = false;
        }
        e.Result = (object) new object[5]
        {
          (object) xSchemaNode,
          (object) xmlNode,
          (object) picIndex,
          (object) num,
          (object) this.loadPartslist
        };
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.frmParent.bObjFrmPictureClosed = true;
      this.HideLoading(this.pnlForm);
      try
      {
        object[] result = (object[]) e.Result;
        XmlNode xmlNode1 = (XmlNode) result[0];
        XmlNode xmlNode2 = (XmlNode) result[1];
        int num1 = (int) result[2];
        int num2 = (int) result[3];
        bool picLoaded = (bool) result[4];
        if (this.frmParent.IsDisposed)
          return;
        if (this.curPageSchema != xmlNode1 || this.curPageNode != xmlNode2 || this.curPicIndex != num1)
        {
          this.isWorking = false;
          this.LoadPicture(this.curPageSchema, this.curPageNode, this.curPicIndex, 0);
        }
        else
        {
          int num3 = 0;
          int num4 = 0;
          string text = this.tsTxtPics.Text;
          try
          {
            if (text.Contains("/"))
            {
              num3 = int.Parse(text.Substring(0, text.IndexOf("/")));
              num4 = int.Parse(text.Substring(text.IndexOf("/") + 1, text.Length - (text.IndexOf("/") + 1)));
            }
            if (num3 == 1)
              this.tsBtnPrev.Enabled = false;
            else
              this.tsBtnPrev.Enabled = true;
            if (num3 == num4)
              this.tsBtnNext.Enabled = false;
            else
              this.tsBtnNext.Enabled = true;
            this.frmParent.UpdatePicToolstrip(this.tsBtnPrev.Enabled, this.tsBtnNext.Enabled, this.tsTxtPics.Text);
          }
          catch
          {
          }
          if (picLoaded)
          {
            this.objDjVuCtl.Select();
            this.frmParent.LoadPartsList(this.curPageSchema, this.curPageNode, this.curPicIndex, this.curListIndex, this.attPicElement, this.attListElement, this.attUpdateDateListElement);
          }
          else
          {
            this.frmParent.HidePartsList();
            this.frmParent.UpdateCurrentPageForPartslist(picLoaded, this.curPageSchema, this.curPageNode, this.curPicIndex, this.curListIndex, this.attPicElement, this.attListElement, this.attUpdateDateListElement);
          }
          this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          this.isWorking = false;
          if (!Settings.Default.PopupPictureMemo)
            return;
          this.frmParent.ShowPictureMemos(true);
          this.frmParent.EnableAddPicMemoTSB(true);
          this.frmParent.EnableAddPicMemoMenu(true);
          this.EnableAddPicMemoTSB(true);
        }
      }
      catch
      {
      }
      finally
      {
        this.isWorking = false;
        this.frmParent.EnableAddPicMemoTSB(true);
        this.frmParent.EnableAddPicMemoMenu(true);
        this.EnableAddPicMemoTSB(true);
      }
    }

    private void tsCmbZoom_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.bSelectiveZoom = false;
        int num = this.tsCmbZoom.ComboBox.SelectedIndex;
        if (num == -1)
          num = this.iSelectedIndex;
        if (num == 5)
        {
          this.objDjVuCtl.Zoom = "132";
          this.objDjVuCtl.Zoom = "WIDTH";
        }
        else if (num == 6)
          this.objDjVuCtl.Zoom = "FITPAGE";
        else if (num == 7)
        {
          this.objDjVuCtl.Zoom = "300";
          this.objDjVuCtl.Zoom = "ONE2ONE";
        }
        else if (num == 8)
          this.objDjVuCtl.Zoom = "STRETCH";
        else
          this.tsCmbZoom.ComboBox.SelectedIndex = num;
        this.tsCmbZoom.Text = this.tsCmbZoom.ComboBox.SelectedItem.ToString();
        this.objDjVuCtl.Zoom = this.tsCmbZoom.ComboBox.SelectedItem.ToString();
        this.iSelectedIndex = this.tsCmbZoom.ComboBox.SelectedIndex;
      }
      catch (Exception ex)
      {
      }
    }

    private void tsCmbZoom_Leave(object sender, EventArgs e)
    {
      try
      {
        string source = this.tsCmbZoom.ComboBox.Text;
        if (source.Contains<char>('%'))
          source = source.Replace("%", "");
        if (source != string.Empty)
        {
          int int32 = Convert.ToInt32(source);
          if (int32 > 1200)
            source = "1200";
          else if (int32 < 25)
            source = "25";
          this.tsCmbZoom.Text = source + "%";
          this.objDjVuCtl.Zoom = this.tsCmbZoom.ComboBox.Text;
        }
        else
        {
          this.objDjVuCtl.Zoom = Settings.Default.DefaultPictureZoom;
          this.tsCmbZoom.Text = this.objDjVuCtl.Zoom;
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void tsCmbZoom_KeyDown(object sender, KeyEventArgs e)
    {
      this.bNonNumberEntered = false;
      if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && (e.KeyCode != Keys.Back && e.KeyCode != Keys.Return))
        this.bNonNumberEntered = true;
      if (Control.ModifierKeys != Keys.Shift)
        return;
      this.bNonNumberEntered = true;
    }

    private void tsCmbZoom_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (this.bNonNumberEntered)
        e.Handled = true;
      if ((int) e.KeyChar != (int) Convert.ToChar((object) Keys.Return))
        return;
      this.tsCmbZoom_Leave((object) null, (EventArgs) null);
    }

    public void ShowHideMiniMap(bool MiniMapChk)
    {
      if (MiniMapChk)
      {
        if (!this.frmParent.miniMapToolStripMenuItem.Enabled || this.objDjVuCtl.ShowBirdsEyeView)
          return;
        this.objDjVuCtl.ShowBirdsEyeView = true;
        this.objDjVuCtl.SetBirdsEyeViewPos(this.frmParent.Handle.ToInt32(), 6, 0, 100, 150, 200);
      }
      else
        this.objDjVuCtl.ShowBirdsEyeView = false;
    }

    private void InitializeDjVu()
    {
      try
      {
        if (Program.objDjVuFeatures.sToolBar == "on")
          this.objDjVuCtl.Toolbar = "yes";
        else if (Program.objDjVuFeatures.sToolBar == "off")
          this.objDjVuCtl.Toolbar = "no";
        this.objDjVuCtl.Menu = Program.objDjVuFeatures.sPopUpMenu;
        if (Program.objDjVuFeatures.sLinks == "show")
          this.objDjVuCtl.ShowAnno = "yes";
        else if (Program.objDjVuFeatures.sLinks == "hide")
          this.objDjVuCtl.ShowAnno = "no";
        this.objDjVuCtl.NavPane = Program.objDjVuFeatures.sNavigationPane;
        this.objDjVuCtl.Rotate = Program.objDjVuFeatures.sRotationAngle;
        this.objDjVuCtl.ShowToolbarButtons = int.Parse(Program.objDjVuFeatures.nProvidedFunctions.ToString());
        this.objDjVuCtl.KeyboardShortcuts = int.Parse(Program.objDjVuFeatures.nKeyboardShortcuts.ToString());
        this.objDjVuCtl.ShowAnnoOnPrint = !(Program.objDjVuFeatures.sShowAnnoOnPrint == "on") ? "no" : "yes";
        this.objDjVuCtl.ShowAnnoOnExport = !(Program.objDjVuFeatures.sShowAnnoOnExport == "on") ? "no" : "yes";
        if (Program.objDjVuFeatures.sShowAnnoOnCopy == "on")
          this.objDjVuCtl.ShowAnnoOnCopy = "yes";
        else
          this.objDjVuCtl.ShowAnnoOnCopy = "no";
      }
      catch
      {
      }
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
      if (this.isWorking)
        return;
      this.isWorking = true;
      this.bgWorker.RunWorkerAsync((object) new object[4]
      {
        (object) this.curPageSchema,
        (object) this.curPageNode,
        (object) this.curPicIndex,
        (object) this.curListIndex
      });
      this.ShowHideMiniMap(frmViewer.MiniMapChk);
    }

    public void LoadBlankPage(string sLocalFile)
    {
      string urlString = Application.StartupPath + "\\blank.pdf#toolbar=0";
      if (sLocalFile.ToUpper().EndsWith("DJVU") && File.Exists(Application.StartupPath + "\\blank.djvu"))
      {
        this.ChangeDJVUZoom("PAGE");
        this.ShowDJVU(true, Application.StartupPath + "\\blank.djvu");
      }
      else if (sLocalFile.ToUpper().EndsWith("TIF") && File.Exists(Application.StartupPath + "\\blank.tif"))
      {
        this.ShowDJVU(false, string.Empty);
        this.ChangeTiffSrc(Application.StartupPath + "\\blank.tif");
      }
      else if (this.frmParent.objFrmTreeview.sDataType.ToUpper().EndsWith("PDF"))
      {
        string empty = string.Empty;
        this.sPreviousImage = "";
        this.wbPDF.Navigate(empty);
        this.wbPDF.Navigate(urlString);
      }
      else
      {
        this.ChangeDJVUZoom("PAGE");
        this.ShowDJVU(true, Application.StartupPath + "\\blank.djvu");
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

    public void HighlightPicture(int x, int y, int width, int height)
    {
      try
      {
        int result;
        if (!int.TryParse(ColorTranslator.ToOle(Settings.Default.appHighlightBackColor).ToString(), out result))
          result = 16711680;
        this.objDjVuCtl.AddHighlightRect(x, y, width, height, result);
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

    public void ScalePicture(float x, float y, int width, int height)
    {
      int length = 0;
      int width1 = 0;
      x += (float) (width / 2);
      y += (float) (height / 2);
      this.objDjVuCtl.GetPageLength(ref length);
      this.objDjVuCtl.GetPageWidth(ref width1);
      if ((double) length > 60.0 + (double) y && width1 > 0)
      {
        y = ((float) length - y) / (float) length;
        x /= (float) width1;
      }
      x = Math.Abs(x);
      y = Math.Abs(y);
      this.objDjVuCtl.ShowPosition = ((double) x).ToString() + "," + (object) y;
    }

    public string CurrentPageId
    {
      get
      {
        try
        {
          string index = string.Empty;
          string empty = string.Empty;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.curPageSchema.Attributes)
          {
            if (attribute.Value.ToUpper().Equals("ID"))
            {
              index = attribute.Name;
              break;
            }
          }
          if (index != string.Empty)
            empty = this.curPageNode.Attributes[index].Value.ToString();
          return empty;
        }
        catch
        {
          return string.Empty;
        }
      }
    }

    public string CurrentPageName
    {
      get
      {
        try
        {
          string index = string.Empty;
          string empty = string.Empty;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) this.curPageSchema.Attributes)
          {
            if (attribute.Value.ToUpper().Equals("PAGENAME"))
            {
              index = attribute.Name;
              break;
            }
          }
          if (index != string.Empty)
            empty = this.curPageNode.Attributes[index].Value.ToString();
          return empty;
        }
        catch
        {
          return string.Empty;
        }
      }
    }

    public string CurrentPicName
    {
      get
      {
        try
        {
          return this.curPicName;
        }
        catch
        {
          return string.Empty;
        }
      }
    }

    public string PicturePath
    {
      get
      {
        return this.objDjVuCtl.SRC;
      }
    }

    public void ShowHidePictureToolbar()
    {
      this.toolStrip1.Visible = Settings.Default.ShowPicToolbar && !Program.objAppFeatures.bDjVuToolbar;
    }

    private void ShowAuthenticationForm()
    {
      try
      {
        frmDjVuAuthentication vuAuthentication = new frmDjVuAuthentication(this.frmParent);
        DialogResult dialogResult = vuAuthentication.ShowDialog((IWin32Window) this);
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        AES aes = new AES();
        if (dialogResult == DialogResult.OK)
        {
          string userId = vuAuthentication.UserId;
          string password = vuAuthentication.Password;
          string str = aes.DLLEncode(vuAuthentication.UserId + "|" + vuAuthentication.Password, "0123456789ABCDEF");
          this.objDjVuCtl.SetNameAndPass(vuAuthentication.UserId, vuAuthentication.Password, 1, 0);
          Program.iniServers[this.frmParent.ServerId].UpdateItem("SETTINGS", "AUTHENTICATION", str);
        }
        else
        {
          if (this.frmParent.IsDisposed)
            return;
          this.LoadBlankPage(string.Empty);
          this.frmParent.HidePicture();
          this.loadPartslist = false;
        }
      }
      catch
      {
      }
    }

    public string HighLightText
    {
      set
      {
        this.highLightText = value;
      }
    }

    public string DjVuPageNumber
    {
      set
      {
        this.djVuPageNumber = value;
      }
    }

    private bool IntervalElapsed(DateTime dtLocal, DateTime dtServer)
    {
      try
      {
        int num = int.Parse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"]);
        return (dtServer - dtLocal).Days > num;
      }
      catch
      {
        return true;
      }
    }

    private DateTime DataUpdateDate(string sDataUpdateFilePath)
    {
      try
      {
        if (!File.Exists(sDataUpdateFilePath))
          return DateTime.Now;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(sDataUpdateFilePath);
        return DateTime.Parse(xmlDocument.SelectSingleNode("//filelastmodified").InnerText, (IFormatProvider) new CultureInfo("fr-FR", false));
      }
      catch
      {
        return DateTime.Now;
      }
    }

    public int[] GetImageZoom()
    {
      int fullL = 0;
      int fullT = 0;
      int fullB = 0;
      int fullR = 0;
      int viewL = 0;
      int viewT = 0;
      int viewR = 0;
      int viewB = 0;
      this.objDjVuCtl.GetCurrentZoomRects(ref fullL, ref fullT, ref fullR, ref fullB, ref viewL, ref viewT, ref viewR, ref viewB);
      return new int[8]
      {
        fullL,
        fullT,
        fullR,
        fullB,
        viewL,
        viewT,
        viewR,
        viewB
      };
    }

    public string GetDjVuZoom()
    {
      return this.objDjVuCtl.Zoom;
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
          this.tsCmbZoom.Items.RemoveAt(this.tsCmbZoom.Items.Count - 1);
        this.tsCmbZoom.Items.Add((object) this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX));
        this.tsCmbZoom.Items.Add((object) this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX));
        this.tsCmbZoom.Items.Add((object) this.GetResource("One to One", "ONE_TO_ONE", ResourceType.COMBO_BOX));
        this.tsCmbZoom.Items.Add((object) this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX));
        this.tsCmbZoom.SelectedIndex = this.iSelectedIndex;
        if (!(this.objDjVuCtl.CurrentLanguage.ToUpper() != this.frmParent.AppCurrentLanguage.ToUpper()) || this.frmParent.objFrmMemo != null)
          return;
        this.objDjVuCtl.CurrentLanguage = this.frmParent.AppCurrentLanguage;
      }
      catch
      {
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MAIN_FORM']" + "/Screen[@Name='PICTURE']";
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

    public void SelectZoom()
    {
      this.bSelectiveZoom = true;
      if (this.objDjVuCtl.MouseMode == "Zoom")
      {
        this.objDjVuCtl.MouseMode = "Pan";
        this.tsbPicCopy.CheckState = CheckState.Unchecked;
        this.tsbPicPanMode.CheckState = CheckState.Checked;
        this.tsbPicSelectText.CheckState = CheckState.Unchecked;
        this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
      }
      else
      {
        this.objDjVuCtl.MouseMode = "Zoom";
        this.tsbPicCopy.CheckState = CheckState.Unchecked;
        this.tsbPicPanMode.CheckState = CheckState.Unchecked;
        this.tsbPicSelectText.CheckState = CheckState.Unchecked;
        this.tsbPicZoomSelect.CheckState = CheckState.Checked;
      }
      this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
      this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
      this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
      this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
    }

    public void CopyImage()
    {
      if (this.objDjVuCtl.MouseMode == "Copy")
      {
        this.objDjVuCtl.MouseMode = "Pan";
        this.tsbPicCopy.CheckState = CheckState.Unchecked;
        this.tsbPicPanMode.CheckState = CheckState.Checked;
        this.tsbPicSelectText.CheckState = CheckState.Unchecked;
        this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
      }
      else
      {
        this.objDjVuCtl.MouseMode = "Copy";
        this.tsbPicCopy.CheckState = CheckState.Checked;
        this.tsbPicPanMode.CheckState = CheckState.Unchecked;
        this.tsbPicSelectText.CheckState = CheckState.Unchecked;
        this.tsbPicZoomSelect.CheckState = CheckState.Unchecked;
      }
      this.frmParent.tsbPicCopy.CheckState = this.tsbPicCopy.CheckState;
      this.frmParent.tsbPicPanMode.CheckState = this.tsbPicPanMode.CheckState;
      this.frmParent.tsbPicSelectText.CheckState = this.tsbPicSelectText.CheckState;
      this.frmParent.tsbPicZoomSelect.CheckState = this.tsbPicZoomSelect.CheckState;
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

    public void ZoomIn()
    {
      try
      {
        this.bSelectiveZoom = false;
        string empty = string.Empty;
        int num1 = 1200;
        string str = this.objDjVuCtl.Zoom;
        if (this.curPicZoom.ToUpper() == "FITPAGE" || this.curPicZoom.ToUpper() == "STRETCH")
          str = this.curPicZoom = this.objDjVuCtl.FitPagePercent;
        else if (this.curPicZoom.ToUpper() == "WIDTH" || this.curPicZoom.ToUpper() == "ONE2ONE")
          str = this.curPicZoom = this.objDjVuCtl.Zoom.Substring(this.objDjVuCtl.Zoom.IndexOf(",") + 1);
        int num2 = int.Parse(str.Substring(str.IndexOf(",") + 1).Replace("%", string.Empty));
        this.objDjVuCtl.Zoom = num2 + Settings.Default.appZoomStep > num1 ? num1.ToString() : (num2 + Settings.Default.appZoomStep).ToString();
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
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        int num1 = 25;
        string str = this.objDjVuCtl.Zoom;
        if (this.curPicZoom.ToUpper() == "FITPAGE" || this.curPicZoom.ToUpper() == "STRETCH")
          str = this.curPicZoom = this.objDjVuCtl.FitPagePercent;
        else if (this.curPicZoom.ToUpper() == "WIDTH" || this.curPicZoom.ToUpper() == "ONE2ONE")
          str = this.curPicZoom = this.objDjVuCtl.Zoom.Substring(this.objDjVuCtl.Zoom.IndexOf(",") + 1);
        int num2 = int.Parse(str.Substring(str.IndexOf(",") + 1).Replace("%", string.Empty));
        this.objDjVuCtl.Zoom = num2 - Settings.Default.appZoomStep <= num1 ? num1.ToString() : (num2 - Settings.Default.appZoomStep).ToString();
        this.curPicZoom = this.objDjVuCtl.Zoom;
        this.prevPicZoom = this.curPicZoom;
        this.tsCmbZoom.Text = this.curPicZoom;
      }
      catch
      {
      }
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
        else
        {
          if (!this.objDjVuCtl.Zoom.ToUpper().Contains("FITPAGE"))
            return;
          this.objDjVuCtl.Zoom = this.prevPicZoom;
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
        this.toolStripSeparator1.Visible = Program.objAppFeatures.bDjVuPan || Program.objAppFeatures.bDjVuSelectZoom || (Program.objAppFeatures.bFitPage || Program.objAppFeatures.bCopyRegion) || Program.objAppFeatures.bDjVuSelectText;
        this.toolStripSeparator2.Visible = Program.objAppFeatures.bDjVuSearch;
        this.toolStripSeparator3.Visible = Program.objAppFeatures.bDjVuZoomIn || Program.objAppFeatures.bDjVuZoomOut;
        this.toolStripSeparator4.Visible = Program.objAppFeatures.bDjVuRotateLeft || Program.objAppFeatures.bDjVuRotateRight;
        this.tsCmbZoom.ComboBox.Visible = Program.objAppFeatures.bDjVuZoomCombobox;
        if (Program.objAppFeatures.bDjVuZoomCombobox)
          return;
        this.tsCmbZoom.Alignment = ToolStripItemAlignment.Left;
        this.tsCmbZoom.AutoSize = false;
        this.tsCmbZoom.Size = new Size(1, 1);
      }
      catch
      {
      }
    }

    public int ExportCurrentImage(string filename, string fmt, bool bNeedAnno, int pageIndex, bool bCurZoom, int full, int fullT, int fullR, int fullB, int viewL, int viewT, int viewR, int viewB)
    {
      bool bNeedAnno1 = false;
      if (Program.objDjVuFeatures.sShowAnnoOnPrint.ToUpper() == "ON")
        bNeedAnno1 = true;
      return this.objDjVuCtl.ExportImageAs1(filename, fmt, bNeedAnno1, pageIndex, bCurZoom, full, fullT, fullR, fullB, viewL, viewT, viewR, viewB);
    }

    public string CurrentImageSource()
    {
      return this.objDjVuCtl.SRC;
    }

    public void DisposeDjVuControl()
    {
      try
      {
        this.objDjVuCtl.SRC = (string) null;
      }
      catch
      {
      }
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

    private bool ImageScrollBarsVisible()
    {
      try
      {
        int num1 = int.Parse(this.objDjVuCtl.FitPagePercent.ToUpper().Replace("FITPAGE,", string.Empty));
        int num2 = num1;
        string zoom = this.objDjVuCtl.Zoom;
        if (zoom.Contains("%"))
          num2 = int.Parse(zoom.ToUpper().Replace("%", string.Empty));
        else if (zoom.ToUpper().Contains("FITPAGE"))
          num2 = int.Parse(zoom.ToUpper().Replace("FITPAGE,", string.Empty));
        else if (zoom.ToUpper().Contains("WIDTH") || zoom.ToUpper().Contains("ONE2ONE"))
          return true;
        return num2 > num1;
      }
      catch
      {
        return false;
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerPicture.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = false;
          }
          this.picLoading.Parent = (Control) parentPanel;
          this.picLoading.BringToFront();
          this.picLoading.Show();
        }
      }
      catch
      {
      }
    }

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerPicture.HideLoadingDelegate(this.HideLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = true;
          }
          this.picLoading.Hide();
          this.picLoading.Size = new Size(32, 32);
          this.picLoading.Parent = (Control) this.pnlForm;
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
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmViewerPicture.HideLoadingDelegate1(this.HideLoading1));
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) this.pnlForm.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = true;
          }
          this.picLoading.Hide();
          this.picLoading.Size = new Size(32, 32);
          this.picLoading.Parent = (Control) this.pnlForm;
        }
      }
      catch
      {
      }
    }

    private void SetPicIndex(XmlNodeList objXmlNodeList, string attPicElement, int picIndex)
    {
      if (this.toolStrip1.InvokeRequired)
      {
        this.toolStrip1.Invoke((Delegate) new frmViewerPicture.SetPicIndexDelegate(this.SetPicIndex), (object) objXmlNodeList, (object) attPicElement, (object) picIndex);
      }
      else
      {
        ArrayList arrayList = new ArrayList();
        int num = 1;
        string str = string.Empty;
        for (int index = 0; index < objXmlNodeList.Count; ++index)
        {
          if (objXmlNodeList[index].Attributes[attPicElement] != null)
          {
            if (!arrayList.Contains((object) objXmlNodeList[index].Attributes[attPicElement].Value))
              arrayList.Add((object) objXmlNodeList[index].Attributes[attPicElement].Value);
            if (index == picIndex)
            {
              num = arrayList.Count;
              try
              {
                str = objXmlNodeList[index].Attributes[this.sPictureTitle].Value;
              }
              catch
              {
                str = "";
              }
            }
          }
        }
        if (str == string.Empty)
          this.UpdatePictureTitle();
        else
          this.Text = str;
        if (arrayList.Count > 0)
          this.tsTxtPics.Text = num.ToString() + "/" + (object) arrayList.Count;
        else
          this.tsTxtPics.Text = "1/1";
        arrayList.Clear();
        this.frmParent.UpdatePicToolstrip(this.tsBtnPrev.Enabled, this.tsBtnNext.Enabled, this.tsTxtPics.Text);
      }
    }

    private void UpdateStatus()
    {
      if (this.frmParent.InvokeRequired)
        this.frmParent.Invoke((Delegate) new frmViewerPicture.StatusDelegate(this.frmParent.UpdateStatus), (object) this.statusText);
      else
        this.frmParent.UpdateStatus(this.statusText);
    }

    public void EnableAddPicMemoTSB(bool value)
    {
      if (this.toolStrip1.InvokeRequired)
        this.toolStrip1.Invoke((Delegate) new frmViewerPicture.EnableAddMemoDelegate(this.EnableAddPicMemoTSB), (object) value);
      else
        this.tsbAddPictureMemo.Enabled = value;
    }

    public void UpdatePictureTitle()
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new frmViewerPicture.UpdatePictureTitleDelegate(this.UpdatePictureTitle));
      }
      else
      {
        if (this.frmParent.objFrmTreeview == null || !(this.frmParent.objFrmTreeview.CurrentNodeText != string.Empty))
          return;
        this.Text = this.frmParent.objFrmTreeview.CurrentNodeText;
      }
    }

    private void ChangeDJVUZoom(string sZoom)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new frmViewerPicture.ChangeDJVUZoomDelegate(this.ChangeDJVUZoom), (object) sZoom);
      }
      else
      {
        if (!Settings.Default.MaintainZoom)
        {
          if (Settings.Default.DefaultPictureZoom == "WIDTH")
          {
            this.objDjVuCtl.Zoom = "132";
            this.objDjVuCtl.Zoom = "WIDTH";
          }
          else if (Settings.Default.DefaultPictureZoom == "ONE2ONE")
          {
            this.objDjVuCtl.Zoom = "300";
            this.objDjVuCtl.Zoom = "ONE2ONE";
          }
          else
            this.objDjVuCtl.Zoom = sZoom;
        }
        else
          this.objDjVuCtl.Zoom = sZoom;
        this.curPicZoom = sZoom;
        this.prevPicZoom = sZoom;
        if (this.objDjVuCtl.Zoom.ToUpper().Contains("FITPAGE") && this.sPreviousImage != this.objDjVuCtl.SRC && this.frmParent.sBookType.ToUpper() == "GSP")
        {
          ++this.Height;
          --this.Height;
          ++this.Width;
          --this.Width;
        }
        this.sPreviousImage = this.objDjVuCtl.SRC;
        if (this.frmParent.objFrmMemo != null)
          this.frmParent.objFrmMemo.Focus();
        else
          this.objDjVuCtl.Focus();
      }
    }

    private void ShowDJVU(bool bState, string sSource)
    {
      if (this.objDjVuCtl.InvokeRequired)
        this.objDjVuCtl.Invoke((Delegate) new frmViewerPicture.ShowDJVUDelegate(this.ShowDJVU), (object) bState, (object) sSource);
      else if (bState)
      {
        this.wbPDF.SendToBack();
        this.objDjVuCtl.BringToFront();
        this.objPicCtl.SendToBack();
        this.objDjVuCtl.SRC = sSource;
        frmViewerPicture.djuvCntrlPagecount = this.objDjVuCtl.GetPageCount();
      }
      else
      {
        this.objDjVuCtl.SendToBack();
        this.objPicCtl.BringToFront();
      }
    }

    private void ChangeTiffSrc(string Src)
    {
      if (this.objPicCtl.InvokeRequired)
      {
        this.objPicCtl.Invoke((Delegate) new frmViewerPicture.ChangeTiffSrcDelegate(this.ChangeTiffSrc), (object) Src);
      }
      else
      {
        Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(frmViewerPicture.ThumbnailCallback);
        Image image = Image.FromFile(Src);
        Size imageDimensions = this.GenerateImageDimensions(image.Width, image.Height, this.pnlPic.Width, this.pnlPic.Height, "Portrait");
        this.objPicCtl.Image = (Image) new Bitmap(image.GetThumbnailImage(imageDimensions.Width, imageDimensions.Height, callback, IntPtr.Zero), imageDimensions.Width, imageDimensions.Height);
        this.CenterPicBoxImage();
        this.Refresh();
      }
    }

    private void ChangePDFSrc(string sLocalFile)
    {
      if (this.wbPDF.InvokeRequired)
      {
        this.wbPDF.Invoke((Delegate) new frmViewerPicture.ChangeTiffSrcDelegate(this.ChangePDFSrc), (object) sLocalFile);
      }
      else
      {
        string str1 = "";
        this.toolStrip1.Hide();
        this.frmParent.tsPic.Hide();
        this.objDjVuCtl.SendToBack();
        string str2;
        if (this.highLightText != null && this.djVuPageNumber != null)
        {
          string urlString = "file:////" + sLocalFile + "#toolbar=on&search=\"" + this.highLightText + "\"&page=" + this.djVuPageNumber;
          if (!(urlString.ToUpper() != this.sPreviousImage.ToUpper()))
            return;
          this.frmParent.ISPDF = true;
          this.wbPDF.BringToFront();
          str2 = urlString;
          this.wbPDF.Navigate(urlString);
          this.sPreviousImage = sLocalFile;
          Application.DoEvents();
          this.highLightText = (string) null;
          this.djVuPageNumber = (string) null;
        }
        else
        {
          string urlString = "file:////" + sLocalFile + "#toolbar=on";
          if (!(sLocalFile.ToUpper() != this.sPreviousImage.ToUpper()) || !(str1 == ""))
            return;
          this.frmParent.ISPDF = true;
          this.wbPDF.BringToFront();
          str2 = "";
          this.wbPDF.Navigate(urlString);
          if (urlString == null)
            return;
          this.sPreviousImage = sLocalFile;
        }
      }
    }

    private void CenterPicBoxImage()
    {
      this.objPicCtl.Size = new Size(0, 0);
      this.objPicCtl.Size = this.objPicCtl.Image.Size;
      if (this.objPicCtl.Width < this.pnlPic.Width)
        this.objPicCtl.Left = (this.pnlPic.Width - this.objPicCtl.Width) / 2;
      else
        this.objPicCtl.Left = 0;
      if (this.objPicCtl.Height < this.pnlPic.Height)
        this.objPicCtl.Top = (this.pnlPic.Height - this.objPicCtl.Height) / 2;
      else
        this.objPicCtl.Top = 0;
    }

    public Size GenerateImageDimensions(int currW, int currH, int destW, int destH, string layout)
    {
      double num = 0.0;
      switch (layout.ToLower())
      {
        case "portrait":
          num = destH <= destW ? (double) destH / (double) currH : (double) destW / (double) currW;
          break;
        case "landscape":
          num = destH <= destW ? (double) destH / (double) currH : (double) destW / (double) currW;
          break;
      }
      return new Size((int) ((double) currW * num), (int) ((double) currH * num));
    }

    public static bool ThumbnailCallback()
    {
      return true;
    }

    public Bitmap Render(ref PDFWrapper pdfDoc)
    {
      try
      {
        if (pdfDoc == null)
          return (Bitmap) null;
        Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(frmViewerPicture.ThumbnailCallback);
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Application.ProductName + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey;
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        string str = path + "\\tmpViewImage.jpg";
        Bitmap bitmap = new Bitmap(pdfDoc.PageWidth, pdfDoc.PageHeight);
        pdfDoc.ClientBounds = new Rectangle(0, 0, pdfDoc.PageWidth, pdfDoc.PageHeight);
        Graphics graphics = Graphics.FromImage((Image) bitmap);
        IntPtr hdc = graphics.GetHdc();
        pdfDoc.DrawPageHDC(hdc);
        graphics.ReleaseHdc();
        graphics.Dispose();
        Size imageDimensions = this.GenerateImageDimensions(bitmap.Width, bitmap.Height, this.pnlPic.Width, this.pnlPic.Height, "Portrait");
        return new Bitmap(bitmap.GetThumbnailImage(imageDimensions.Width, imageDimensions.Height, callback, IntPtr.Zero), imageDimensions.Width, imageDimensions.Height);
      }
      catch
      {
        return (Bitmap) null;
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
        int num = (int) MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void tsbSearchText_Click(object sender, EventArgs e)
    {
      try
      {
        this.TextSearch();
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    public void ShowHideThumbnail()
    {
      try
      {
        this.objDjVuCtl.ThumbnailPaneEnabled = !this.objDjVuCtl.ThumbnailPaneEnabled;
        if (this.objDjVuCtl.ThumbnailPaneEnabled)
        {
          this.tsbThumbnail.CheckState = CheckState.Checked;
          this.frmParent.tsbThumbnail.CheckState = this.tsbThumbnail.CheckState;
        }
        else
        {
          this.tsbThumbnail.CheckState = CheckState.Unchecked;
          this.frmParent.tsbThumbnail.CheckState = this.tsbThumbnail.CheckState;
        }
      }
      catch
      {
        int num = (int) MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
        int num = (int) MessageBox.Show(this.GetResource("The installed version of CSS DjVu Control does not support this functionality", "UPDATE_DJVU", ResourceType.POPUP_MESSAGE), this.GetResource("GSPcLocal Viewer 3.0", "GSPcLOCAL", ResourceType.POPUP_MESSAGE), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmViewerPicture));
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
      this.objDjVuCtl.BeginInit();
      ((ISupportInitialize) this.objPicCtl).BeginInit();
      this.toolStrip1.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlPic);
      this.pnlForm.Controls.Add((Control) this.toolStrip1);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(528, 381);
      this.pnlForm.TabIndex = 1;
      this.pnlPic.BackColor = Color.White;
      this.pnlPic.Controls.Add((Control) this.wbPDF);
      this.pnlPic.Controls.Add((Control) this.objDjVuCtl);
      this.pnlPic.Controls.Add((Control) this.objPicCtl);
      this.pnlPic.Dock = DockStyle.Fill;
      this.pnlPic.Location = new Point(0, 31);
      this.pnlPic.Name = "pnlPic";
      this.pnlPic.Size = new Size(526, 348);
      this.pnlPic.TabIndex = 2;
      this.wbPDF.Dock = DockStyle.Fill;
      this.wbPDF.IsWebBrowserContextMenuEnabled = false;
      this.wbPDF.Location = new Point(0, 0);
      this.wbPDF.MinimumSize = new Size(20, 20);
      this.wbPDF.Name = "wbPDF";
      this.wbPDF.ScriptErrorsSuppressed = true;
      this.wbPDF.Size = new Size(526, 348);
      this.wbPDF.TabIndex = 28;
      this.objDjVuCtl.Dock = DockStyle.Fill;
      this.objDjVuCtl.Enabled = true;
      this.objDjVuCtl.Location = new Point(0, 0);
      this.objDjVuCtl.Name = "objDjVuCtl";
      this.objDjVuCtl.OcxState = (AxHost.State) componentResourceManager.GetObject("objDjVuCtl.OcxState");
      this.objDjVuCtl.Size = new Size(526, 348);
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
      this.objPicCtl.Size = new Size(526, 335);
      this.objPicCtl.SizeMode = PictureBoxSizeMode.CenterImage;
      this.objPicCtl.TabIndex = 26;
      this.objPicCtl.TabStop = false;
      this.toolStrip1.BackColor = SystemColors.Control;
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new ToolStripItem[20]
      {
        (ToolStripItem) this.tsbSearchText,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.tsbPicPanMode,
        (ToolStripItem) this.tsbPicZoomSelect,
        (ToolStripItem) this.tsbFitPage,
        (ToolStripItem) this.tsbPicCopy,
        (ToolStripItem) this.tsbPicSelectText,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.tsbPicZoomIn,
        (ToolStripItem) this.tsCmbZoom,
        (ToolStripItem) this.tsbPicZoomOut,
        (ToolStripItem) this.toolStripSeparator3,
        (ToolStripItem) this.tsBRotateLeft,
        (ToolStripItem) this.tsBRotateRight,
        (ToolStripItem) this.toolStripSeparator4,
        (ToolStripItem) this.tsBtnNext,
        (ToolStripItem) this.tsbAddPictureMemo,
        (ToolStripItem) this.tsbThumbnail,
        (ToolStripItem) this.tsTxtPics,
        (ToolStripItem) this.tsBtnPrev
      });
      this.toolStrip1.Location = new Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RightToLeft = RightToLeft.Yes;
      this.toolStrip1.Size = new Size(526, 31);
      this.toolStrip1.TabIndex = 2;
      this.tsbSearchText.Alignment = ToolStripItemAlignment.Right;
      this.tsbSearchText.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbSearchText.Image = (Image) GSPcLocalViewer.Properties.Resources.Text_Search1;
      this.tsbSearchText.ImageScaling = ToolStripItemImageScaling.None;
      this.tsbSearchText.ImageTransparentColor = Color.Magenta;
      this.tsbSearchText.Name = "tsbSearchText";
      this.tsbSearchText.Size = new Size(28, 28);
      this.tsbSearchText.Text = "Text Search";
      this.tsbSearchText.Click += new EventHandler(this.tsbSearchText_Click);
      this.toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(6, 31);
      this.tsbPicPanMode.Alignment = ToolStripItemAlignment.Right;
      this.tsbPicPanMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicPanMode.Image = (Image) GSPcLocalViewer.Properties.Resources.Pan_Mode;
      this.tsbPicPanMode.ImageTransparentColor = Color.Magenta;
      this.tsbPicPanMode.Name = "tsbPicPanMode";
      this.tsbPicPanMode.Size = new Size(23, 28);
      this.tsbPicPanMode.Text = "Pan Mode";
      this.tsbPicPanMode.Click += new EventHandler(this.tsbPicPanMode_Click);
      this.tsbPicZoomSelect.Alignment = ToolStripItemAlignment.Right;
      this.tsbPicZoomSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicZoomSelect.Image = (Image) GSPcLocalViewer.Properties.Resources.zoom_select;
      this.tsbPicZoomSelect.ImageTransparentColor = Color.Magenta;
      this.tsbPicZoomSelect.Name = "tsbPicZoomSelect";
      this.tsbPicZoomSelect.Size = new Size(23, 28);
      this.tsbPicZoomSelect.Text = "Select Zoom";
      this.tsbPicZoomSelect.Click += new EventHandler(this.tsbPicZoomSelect_Click);
      this.tsbFitPage.Alignment = ToolStripItemAlignment.Right;
      this.tsbFitPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbFitPage.Image = (Image) GSPcLocalViewer.Properties.Resources.Picture_FitPage;
      this.tsbFitPage.ImageTransparentColor = Color.Magenta;
      this.tsbFitPage.Name = "tsbFitPage";
      this.tsbFitPage.Size = new Size(23, 28);
      this.tsbFitPage.Text = "Fit Page";
      this.tsbFitPage.Click += new EventHandler(this.tsBtnFitPage_Click);
      this.tsbPicCopy.Alignment = ToolStripItemAlignment.Right;
      this.tsbPicCopy.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicCopy.Image = (Image) GSPcLocalViewer.Properties.Resources.copy_over;
      this.tsbPicCopy.ImageTransparentColor = Color.Magenta;
      this.tsbPicCopy.Name = "tsbPicCopy";
      this.tsbPicCopy.Size = new Size(23, 28);
      this.tsbPicCopy.Text = "Copy Image";
      this.tsbPicCopy.Click += new EventHandler(this.tsbPicCopy_Click);
      this.tsbPicSelectText.Alignment = ToolStripItemAlignment.Right;
      this.tsbPicSelectText.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicSelectText.Image = (Image) GSPcLocalViewer.Properties.Resources.Text_Selection;
      this.tsbPicSelectText.ImageTransparentColor = Color.Magenta;
      this.tsbPicSelectText.Name = "tsbPicSelectText";
      this.tsbPicSelectText.Size = new Size(23, 28);
      this.tsbPicSelectText.Text = "Copy Image";
      this.tsbPicSelectText.Click += new EventHandler(this.tsbPicSelectText_Click);
      this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(6, 31);
      this.tsbPicZoomIn.Alignment = ToolStripItemAlignment.Right;
      this.tsbPicZoomIn.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicZoomIn.Image = (Image) GSPcLocalViewer.Properties.Resources.zoom_in;
      this.tsbPicZoomIn.ImageTransparentColor = Color.Magenta;
      this.tsbPicZoomIn.Name = "tsbPicZoomIn";
      this.tsbPicZoomIn.Size = new Size(23, 28);
      this.tsbPicZoomIn.Text = "Zoom In";
      this.tsbPicZoomIn.Click += new EventHandler(this.tsbPicZoomIn_Click);
      this.tsCmbZoom.Alignment = ToolStripItemAlignment.Right;
      this.tsCmbZoom.AutoToolTip = true;
      this.tsCmbZoom.Items.AddRange(new object[5]
      {
        (object) "300%",
        (object) "150%",
        (object) "100%",
        (object) "50%",
        (object) "25%"
      });
      this.tsCmbZoom.MaxLength = 4;
      this.tsCmbZoom.Name = "tsCmbZoom";
      this.tsCmbZoom.RightToLeft = RightToLeft.No;
      this.tsCmbZoom.Size = new Size(121, 31);
      this.tsCmbZoom.SelectedIndexChanged += new EventHandler(this.tsCmbZoom_SelectedIndexChanged);
      this.tsCmbZoom.KeyDown += new KeyEventHandler(this.tsCmbZoom_KeyDown);
      this.tsCmbZoom.Leave += new EventHandler(this.tsCmbZoom_Leave);
      this.tsCmbZoom.KeyPress += new KeyPressEventHandler(this.tsCmbZoom_KeyPress);
      this.tsbPicZoomOut.Alignment = ToolStripItemAlignment.Right;
      this.tsbPicZoomOut.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPicZoomOut.Image = (Image) GSPcLocalViewer.Properties.Resources.zoon_out;
      this.tsbPicZoomOut.ImageTransparentColor = Color.Magenta;
      this.tsbPicZoomOut.Name = "tsbPicZoomOut";
      this.tsbPicZoomOut.Size = new Size(23, 28);
      this.tsbPicZoomOut.Text = "Zoom Out";
      this.tsbPicZoomOut.Click += new EventHandler(this.tsbPicZoomOut_Click);
      this.toolStripSeparator3.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new Size(6, 31);
      this.tsBRotateLeft.Alignment = ToolStripItemAlignment.Right;
      this.tsBRotateLeft.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBRotateLeft.Image = (Image) GSPcLocalViewer.Properties.Resources.Rotate_Left;
      this.tsBRotateLeft.ImageTransparentColor = Color.Magenta;
      this.tsBRotateLeft.Name = "tsBRotateLeft";
      this.tsBRotateLeft.Size = new Size(23, 28);
      this.tsBRotateLeft.Text = "Rotate Left";
      this.tsBRotateLeft.Click += new EventHandler(this.tsBRotateLeft_Click);
      this.tsBRotateRight.Alignment = ToolStripItemAlignment.Right;
      this.tsBRotateRight.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBRotateRight.Image = (Image) GSPcLocalViewer.Properties.Resources.Rotate_Right;
      this.tsBRotateRight.ImageTransparentColor = Color.Magenta;
      this.tsBRotateRight.Name = "tsBRotateRight";
      this.tsBRotateRight.Size = new Size(23, 28);
      this.tsBRotateRight.Text = "Rotate Right";
      this.tsBRotateRight.Click += new EventHandler(this.tsBRotateRight_Click);
      this.toolStripSeparator4.Alignment = ToolStripItemAlignment.Right;
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new Size(6, 31);
      this.tsBtnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnNext.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Next;
      this.tsBtnNext.ImageTransparentColor = Color.Magenta;
      this.tsBtnNext.Name = "tsBtnNext";
      this.tsBtnNext.Overflow = ToolStripItemOverflow.Never;
      this.tsBtnNext.Size = new Size(23, 28);
      this.tsBtnNext.Text = "Next Picture";
      this.tsBtnNext.Click += new EventHandler(this.tsBtnNext_Click);
      this.tsbAddPictureMemo.Alignment = ToolStripItemAlignment.Right;
      this.tsbAddPictureMemo.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbAddPictureMemo.Image = (Image) GSPcLocalViewer.Properties.Resources.Add_Memo;
      this.tsbAddPictureMemo.ImageTransparentColor = Color.Magenta;
      this.tsbAddPictureMemo.Name = "tsbAddPictureMemo";
      this.tsbAddPictureMemo.Size = new Size(23, 28);
      this.tsbAddPictureMemo.Text = "Add Picture Memo";
      this.tsbAddPictureMemo.Click += new EventHandler(this.tsbAddPictureMemo_Click);
      this.tsbThumbnail.Alignment = ToolStripItemAlignment.Right;
      this.tsbThumbnail.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbThumbnail.Image = (Image) GSPcLocalViewer.Properties.Resources.Thumbnail;
      this.tsbThumbnail.ImageTransparentColor = Color.Magenta;
      this.tsbThumbnail.Name = "tsbThumbnail";
      this.tsbThumbnail.Size = new Size(23, 28);
      this.tsbThumbnail.Text = "Show Thumbnail";
      this.tsbThumbnail.Click += new EventHandler(this.tsbThumbnail_Click);
      this.tsTxtPics.AutoSize = false;
      this.tsTxtPics.BorderStyle = BorderStyle.FixedSingle;
      this.tsTxtPics.Name = "tsTxtPics";
      this.tsTxtPics.ReadOnly = true;
      this.tsTxtPics.ShortcutsEnabled = false;
      this.tsTxtPics.Size = new Size(50, 23);
      this.tsTxtPics.TextBoxTextAlign = HorizontalAlignment.Center;
      this.tsBtnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsBtnPrev.Image = (Image) GSPcLocalViewer.Properties.Resources.Nav_Prev;
      this.tsBtnPrev.ImageTransparentColor = Color.Magenta;
      this.tsBtnPrev.Name = "tsBtnPrev";
      this.tsBtnPrev.Size = new Size(23, 20);
      this.tsBtnPrev.Text = "Previous Picture";
      this.tsBtnPrev.Click += new EventHandler(this.tsBtnPrev_Click);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Dock = DockStyle.Fill;
      this.picLoading.Image = (Image) GSPcLocalViewer.Properties.Resources.Loading1;
      this.picLoading.Location = new Point(0, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(526, 379);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 21;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(528, 381);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.HideOnClose = true;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.KeyPreview = true;
      this.Name = nameof (frmViewerPicture);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Picture";
      this.Load += new EventHandler(this.frmViewerPicture_Load);
      this.SizeChanged += new EventHandler(this.frmViewerPicture_SizeChanged);
      this.VisibleChanged += new EventHandler(this.frmViewerPicture_VisibleChanged);
      this.pnlForm.ResumeLayout(false);
      this.pnlForm.PerformLayout();
      this.pnlPic.ResumeLayout(false);
      this.objDjVuCtl.EndInit();
      ((ISupportInitialize) this.objPicCtl).EndInit();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    public delegate void HideLoadingDelegate1();

    private delegate void SetPicIndexDelegate(XmlNodeList objXmlNodeList, string attPicElement, int picIndex);

    private delegate void StatusDelegate(string status);

    private delegate void EnableAddMemoDelegate(bool value);

    private delegate void UpdatePictureTitleDelegate();

    private delegate void ChangeDJVUZoomDelegate(string sZoom);

    private delegate void ShowDJVUDelegate(bool bState, string sSource);

    private delegate void ChangeTiffSrcDelegate(string Src);

    private delegate void ShowPDFDelegate(string sLocalFile);
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConfigViewerGeneral
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmConfigViewerGeneral : Form
  {
    private frmConfig frmParent;
    private IContainer components;
    private Label lblGeneral;
    private Button btnOK;
    private Button btnCancel;
    private Panel pnlForm;
    private Panel pnlPicture;
    private CheckBox chkMaintainZoomWhileNavigating;
    private Label lblPicture;
    private Label lblDefaultZoom;
    private Panel pnlcmbDefaultZoom;
    private Panel pnlLblPicture;
    private Label lblPictureLine;
    private Panel pnlPartslist;
    private Panel pnlLblPartslist;
    private Label lblPartslistLine;
    private Label lblPartslist;
    private CheckBox chkShowPartslistToolbar;
    private CheckBox chkShowPictureToolbar;
    private Panel pnlLanguage;
    private Panel pnlLang;
    public ComboBox cmbLanguagesList;
    private Label lblDefaultLanguage;
    private Panel panel2;
    private Label label2;
    private Label lblLanguage;
    private NumericTextBox txtZoomStep;
    private Label lblZoomStep;
    private CheckBox chkFitPageForMulitParts;
    private ComboBox cmbDefaultZoom;
    private Panel pnlHistory;
    private CheckBox chkOpenBookInCurrentWindow;
    private NumericTextBox txtHistorySize;
    private Label lblHistorySize;
    private Panel pnlLblHistory;
    private Label lblHistoryLine;
    private Label lblOther;
    private CheckBox chkSideBySidePrinting;
    private CheckBox chkMouseWheelScrollOnPicture;
    private CheckBox chkMouseWheelScrollOnContents;
    private CheckBox chkContentsExpandAll;
    private CheckBox chkHankakuZenKaku;
    private NumericUpDown numCmbContentsExpandLevel;
    private Label lblContentsExpandLevel;
    private Panel panel1;

    public frmConfigViewerGeneral(frmConfig frm)
    {
      this.InitializeComponent();
      try
      {
        this.frmParent = frm;
        this.MdiParent = (Form) frm;
        this.UpdateFont();
        this.LoadResources();
        this.txtHistorySize.Text = Settings.Default.HistorySize.ToString();
        this.chkOpenBookInCurrentWindow.Checked = Settings.Default.OpenInCurrentInstance;
        this.chkMaintainZoomWhileNavigating.Checked = Settings.Default.MaintainZoom;
        if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("WIDTH"))
          this.cmbDefaultZoom.Text = this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX);
        else if (Settings.Default.DefaultPictureZoom.ToUpper().Contains("FITPAGE"))
          this.cmbDefaultZoom.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX);
        else if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("ONE2ONE"))
          this.cmbDefaultZoom.Text = this.GetResource("One To One", "ONE_TO_ONE", ResourceType.COMBO_BOX);
        else if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("STRETCH"))
          this.cmbDefaultZoom.Text = this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX);
        else
          this.cmbDefaultZoom.Text = Settings.Default.DefaultPictureZoom;
        this.chkShowPictureToolbar.Checked = Settings.Default.ShowPicToolbar;
        this.chkShowPartslistToolbar.Checked = Settings.Default.ShowListToolbar;
        this.LoadXML();
        if (!this.cmbLanguagesList.Items.Contains((object) "English"))
          this.cmbLanguagesList.Items.Add((object) "English");
        this.SetDisplayNameIndex();
        this.cmbLanguagesList.SelectedItem = (object) Settings.Default.appLanguage;
        if (this.cmbLanguagesList.SelectedItem == null)
          this.cmbLanguagesList.SelectedItem = (object) "English";
        if (!Settings.Default.appZoomStep.Equals((object) string.Empty))
          this.txtZoomStep.Text = Settings.Default.appZoomStep.ToString();
        this.chkFitPageForMulitParts.Checked = Settings.Default.appFitPageForMultiParts;
        this.chkSideBySidePrinting.Checked = Settings.Default.SideBySidePrinting;
        this.chkMouseWheelScrollOnContents.Visible = Program.objAppFeatures.bDjVuScroll;
        this.chkMouseWheelScrollOnPicture.Visible = Program.objAppFeatures.bDjVuScroll;
        this.chkMouseWheelScrollOnContents.Checked = Settings.Default.MouseScrollContents;
        this.chkMouseWheelScrollOnPicture.Checked = Settings.Default.MouseScrollPicture;
        this.chkShowPictureToolbar.Visible = !Program.objAppFeatures.bDjVuToolbar;
        if (this.frmParent.frmParent.BookType == "GSC")
          this.pnlPartslist.Hide();
        this.chkContentsExpandAll.Checked = Settings.Default.ExpandAllContents;
        this.chkHankakuZenKaku.Checked = Settings.Default.HankakuZenkakuFlag;
        this.numCmbContentsExpandLevel.Value = (Decimal) Settings.Default.ExpandContentsLevel;
      }
      catch
      {
      }
    }

    private void SetDisplayNameIndex()
    {
      try
      {
        bool flag = false;
        string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
        for (int index = 0; index < files.Length; ++index)
        {
          try
          {
            if (File.Exists(files[index]))
            {
              string withoutExtension = Path.GetFileNameWithoutExtension(files[index]);
              XmlDocument xmlDocument = new XmlDocument();
              xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + withoutExtension + ".xml");
              XmlNode xmlNode = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
              string str1 = xmlNode.Attributes["EN_NAME"].Value;
              string str2 = xmlNode.Attributes["Language"].Value;
              if (str1.ToLower() == Settings.Default.appLanguage.ToLower())
              {
                this.cmbLanguagesList.SelectedItem = (object) str2;
                flag = true;
              }
            }
          }
          catch
          {
          }
        }
        if (flag)
          return;
        this.cmbLanguagesList.SelectedItem = (object) "English";
      }
      catch
      {
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblGeneral.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    public int GetHistorySize
    {
      get
      {
        int result = 0;
        if (int.TryParse(this.txtHistorySize.Text, out result) && result >= 0)
          return result;
        return Settings.Default.HistorySize;
      }
    }

    public bool GetOpenBookinCurrentWindow
    {
      get
      {
        return this.chkOpenBookInCurrentWindow.Checked;
      }
    }

    public bool GetMaintainZoom
    {
      get
      {
        return this.chkMaintainZoomWhileNavigating.Checked;
      }
    }

    public bool GetShowPicToolbar
    {
      get
      {
        return this.chkShowPictureToolbar.Checked;
      }
    }

    public bool GetShowListToolbar
    {
      get
      {
        return this.chkShowPartslistToolbar.Checked;
      }
    }

    public int GetZoomStep
    {
      get
      {
        try
        {
          return int.Parse(this.txtZoomStep.Text);
        }
        catch
        {
          return 100;
        }
      }
    }

    public bool GetFitPageForMultiParts
    {
      get
      {
        try
        {
          return this.chkFitPageForMulitParts.Checked;
        }
        catch
        {
          return true;
        }
      }
    }

    public bool GetHankakuZenkakuFlag
    {
      get
      {
        try
        {
          return this.chkHankakuZenKaku.Checked;
        }
        catch
        {
          return true;
        }
      }
    }

    public bool GetExpandAllContentsFlag
    {
      get
      {
        try
        {
          return this.chkContentsExpandAll.Checked;
        }
        catch
        {
          return true;
        }
      }
    }

    public int GetExpandContentsLevel
    {
      get
      {
        try
        {
          return int.Parse(this.numCmbContentsExpandLevel.Value.ToString());
        }
        catch
        {
          return 1;
        }
      }
    }

    public string GetApplicationLanguage
    {
      get
      {
        return this.cmbLanguagesList.SelectedItem.ToString();
      }
    }

    public string GetDefaultZoom
    {
      get
      {
        if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX)))
          return "WIDTH";
        if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX)))
          return "FITPAGE";
        if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("One to One", "ONE_TO_ONE", ResourceType.COMBO_BOX)))
          return "ONE2ONE";
        if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX)))
          return "STRETCH";
        return this.cmbDefaultZoom.Text;
      }
    }

    public bool GetSideBySide
    {
      get
      {
        return this.chkSideBySidePrinting.Checked;
      }
    }

    public bool GetMouseWheelScrollContents
    {
      get
      {
        return this.chkMouseWheelScrollOnContents.Checked;
      }
    }

    public bool GetMouseWheelScrollPicture
    {
      get
      {
        return this.chkMouseWheelScrollOnPicture.Checked;
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      this.frmParent.CloseAndSaveSettings();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.frmParent.Close();
    }

    private void LoadResources()
    {
      this.lblGeneral.Text = this.GetResource("General", "GENERAL", ResourceType.LABEL);
      this.lblPicture.Text = this.GetResource("Picture", "PICTURE", ResourceType.LABEL);
      this.chkShowPictureToolbar.Text = this.GetResource("Show Picture Toolbar in Frame", "PICTURE_TOOLBAR_FRAME", ResourceType.CHECK_BOX);
      this.chkMaintainZoomWhileNavigating.Text = this.GetResource("Maintain Zoom Level during navigation", "MAINTAIN_ZOOM", ResourceType.CHECK_BOX);
      this.lblDefaultZoom.Text = this.GetResource("Default Zoom", "DEFAULT_ZOOM", ResourceType.LABEL);
      this.lblPartslist.Text = this.GetResource("Parts List", "PARTS_LIST", ResourceType.LABEL);
      this.chkShowPartslistToolbar.Text = this.GetResource("Show Parts List Toolbar in Frame", "PARTSLIST_TOOLBAR_FRAME", ResourceType.CHECK_BOX);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      this.cmbDefaultZoom.Items.Add((object) this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX));
      this.cmbDefaultZoom.Items.Add((object) this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX));
      this.cmbDefaultZoom.Items.Add((object) this.GetResource("One to One", "ONE_TO_ONE", ResourceType.COMBO_BOX));
      this.cmbDefaultZoom.Items.Add((object) this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX));
      this.lblDefaultLanguage.Text = this.GetResource("Default Language", "DEFAULT_LANGUAGE", ResourceType.LABEL);
      this.lblLanguage.Text = this.GetResource("Language", "LANGUAGE", ResourceType.LABEL);
      this.lblOther.Text = this.GetResource("Other", "OTHER", ResourceType.LABEL);
      this.lblHistorySize.Text = this.GetResource("History", "HISTORY_SIZE", ResourceType.LABEL);
      this.chkOpenBookInCurrentWindow.Text = this.GetResource("Open Book in Current Window", "OPEN_BOOK_CURRENT", ResourceType.CHECK_BOX);
      this.chkFitPageForMulitParts.Text = this.GetResource("Zoom Fit Page for multi parts", "ZOOMFIT_MULTIPARTS", ResourceType.CHECK_BOX);
      this.lblZoomStep.Text = this.GetResource("Zoom Step", "ZOOM_STEP", ResourceType.LABEL);
      this.chkSideBySidePrinting.Text = this.GetResource("Side By Side Printing", "SIDE_BY_SIDE_PRINTING", ResourceType.CHECK_BOX);
      this.chkMouseWheelScrollOnPicture.Text = this.GetResource("Use mouse wheel scroll on Images", "WHEELSCROLL_IMAGES", ResourceType.CHECK_BOX);
      this.chkMouseWheelScrollOnContents.Text = this.GetResource("Use mouse wheel scroll on Contents", "MOUSEWHEEL_CONTENTS", ResourceType.CHECK_BOX);
      this.chkHankakuZenKaku.Text = this.GetResource("Search HanKaku <-> ZenKaku", "HANKAKU_ZENKAKU", ResourceType.CHECK_BOX);
      this.chkContentsExpandAll.Text = this.GetResource("Expand All Contents", "CONTENTS_EXPAND_ALL", ResourceType.CHECK_BOX);
      this.lblContentsExpandLevel.Text = this.GetResource("Contents Expanded to Level", "CONTENTS_EXPAND_LEVEL", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONFIGURATION']" + "/Screen[@Name='GENERAL']";
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
            return this.frmParent.frmParent.GetResourceValue(sDefaultValue, xQuery1);
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
        return this.frmParent.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private void LoadXML()
    {
      try
      {
        string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
        for (int index = 0; index < files.Length; ++index)
        {
          if (File.Exists(files[index]))
          {
            try
            {
              int num1 = files[index].IndexOf(".xml");
              int num2 = files[index].LastIndexOf("\\");
              string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
              XmlDocument xmlDocument = new XmlDocument();
              xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
              string str2 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["Language"].Value;
              if (!this.cmbLanguagesList.Items.Contains((object) str2))
                this.cmbLanguagesList.Items.Add((object) str2);
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

    private void cmbLanguagesList_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void frmConfigViewerGeneral_Load(object sender, EventArgs e)
    {
      this.OnOffFeatures();
    }

    public void OnOffFeatures()
    {
      try
      {
        this.lblHistorySize.Visible = Program.objAppFeatures.bHistory;
        this.txtHistorySize.Visible = Program.objAppFeatures.bHistory;
        this.chkMouseWheelScrollOnContents.Visible = Program.objAppFeatures.bDjVuScroll;
        this.chkMouseWheelScrollOnPicture.Visible = Program.objAppFeatures.bDjVuScroll;
        this.btnOK.Visible = true;
        this.btnCancel.Visible = true;
      }
      catch
      {
      }
    }

    private void chkContentsExpandAll_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        this.numCmbContentsExpandLevel.Enabled = !this.chkContentsExpandAll.Checked;
      }
      catch
      {
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
      this.lblGeneral = new Label();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.pnlForm = new Panel();
      this.pnlHistory = new Panel();
      this.txtHistorySize = new NumericTextBox();
      this.pnlLblHistory = new Panel();
      this.lblHistoryLine = new Label();
      this.lblOther = new Label();
      this.numCmbContentsExpandLevel = new NumericUpDown();
      this.lblContentsExpandLevel = new Label();
      this.chkContentsExpandAll = new CheckBox();
      this.chkHankakuZenKaku = new CheckBox();
      this.chkMouseWheelScrollOnPicture = new CheckBox();
      this.chkMouseWheelScrollOnContents = new CheckBox();
      this.chkSideBySidePrinting = new CheckBox();
      this.chkOpenBookInCurrentWindow = new CheckBox();
      this.lblHistorySize = new Label();
      this.panel1 = new Panel();
      this.pnlLanguage = new Panel();
      this.lblDefaultLanguage = new Label();
      this.panel2 = new Panel();
      this.label2 = new Label();
      this.lblLanguage = new Label();
      this.pnlLang = new Panel();
      this.cmbLanguagesList = new ComboBox();
      this.pnlPartslist = new Panel();
      this.pnlLblPartslist = new Panel();
      this.lblPartslistLine = new Label();
      this.lblPartslist = new Label();
      this.chkShowPartslistToolbar = new CheckBox();
      this.pnlPicture = new Panel();
      this.chkFitPageForMulitParts = new CheckBox();
      this.txtZoomStep = new NumericTextBox();
      this.lblZoomStep = new Label();
      this.pnlcmbDefaultZoom = new Panel();
      this.cmbDefaultZoom = new ComboBox();
      this.chkShowPictureToolbar = new CheckBox();
      this.chkMaintainZoomWhileNavigating = new CheckBox();
      this.lblDefaultZoom = new Label();
      this.pnlLblPicture = new Panel();
      this.lblPictureLine = new Label();
      this.lblPicture = new Label();
      this.pnlForm.SuspendLayout();
      this.pnlHistory.SuspendLayout();
      this.pnlLblHistory.SuspendLayout();
      this.numCmbContentsExpandLevel.BeginInit();
      this.panel1.SuspendLayout();
      this.pnlLanguage.SuspendLayout();
      this.panel2.SuspendLayout();
      this.pnlLang.SuspendLayout();
      this.pnlPartslist.SuspendLayout();
      this.pnlLblPartslist.SuspendLayout();
      this.pnlPicture.SuspendLayout();
      this.pnlcmbDefaultZoom.SuspendLayout();
      this.pnlLblPicture.SuspendLayout();
      this.SuspendLayout();
      this.lblGeneral.BackColor = Color.White;
      this.lblGeneral.Dock = DockStyle.Top;
      this.lblGeneral.ForeColor = Color.Black;
      this.lblGeneral.Location = new Point(0, 0);
      this.lblGeneral.Name = "lblGeneral";
      this.lblGeneral.Padding = new Padding(3, 7, 0, 0);
      this.lblGeneral.Size = new Size(448, 27);
      this.lblGeneral.TabIndex = 16;
      this.lblGeneral.Text = "General";
      this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnOK.Location = new Point(282, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnCancel.Location = new Point(358, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlHistory);
      this.pnlForm.Controls.Add((Control) this.panel1);
      this.pnlForm.Controls.Add((Control) this.pnlLanguage);
      this.pnlForm.Controls.Add((Control) this.pnlPartslist);
      this.pnlForm.Controls.Add((Control) this.pnlPicture);
      this.pnlForm.Controls.Add((Control) this.lblGeneral);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(450, 569);
      this.pnlForm.TabIndex = 19;
      this.pnlHistory.AutoScroll = true;
      this.pnlHistory.Controls.Add((Control) this.txtHistorySize);
      this.pnlHistory.Controls.Add((Control) this.pnlLblHistory);
      this.pnlHistory.Controls.Add((Control) this.numCmbContentsExpandLevel);
      this.pnlHistory.Controls.Add((Control) this.lblContentsExpandLevel);
      this.pnlHistory.Controls.Add((Control) this.chkContentsExpandAll);
      this.pnlHistory.Controls.Add((Control) this.chkHankakuZenKaku);
      this.pnlHistory.Controls.Add((Control) this.chkMouseWheelScrollOnPicture);
      this.pnlHistory.Controls.Add((Control) this.chkMouseWheelScrollOnContents);
      this.pnlHistory.Controls.Add((Control) this.chkSideBySidePrinting);
      this.pnlHistory.Controls.Add((Control) this.chkOpenBookInCurrentWindow);
      this.pnlHistory.Controls.Add((Control) this.lblHistorySize);
      this.pnlHistory.Dock = DockStyle.Fill;
      this.pnlHistory.Location = new Point(0, 292);
      this.pnlHistory.Name = "pnlHistory";
      this.pnlHistory.Size = new Size(448, 245);
      this.pnlHistory.TabIndex = 28;
      this.txtHistorySize.AllowSpace = false;
      this.txtHistorySize.BorderStyle = BorderStyle.FixedSingle;
      this.txtHistorySize.Location = new Point(134, 26);
      this.txtHistorySize.MaxLength = 2;
      this.txtHistorySize.Name = "txtHistorySize";
      this.txtHistorySize.Size = new Size(124, 21);
      this.txtHistorySize.TabIndex = 9;
      this.pnlLblHistory.Controls.Add((Control) this.lblHistoryLine);
      this.pnlLblHistory.Controls.Add((Control) this.lblOther);
      this.pnlLblHistory.Dock = DockStyle.Top;
      this.pnlLblHistory.Location = new Point(0, 0);
      this.pnlLblHistory.Name = "pnlLblHistory";
      this.pnlLblHistory.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblHistory.Size = new Size(448, 28);
      this.pnlLblHistory.TabIndex = 16;
      this.lblHistoryLine.BackColor = Color.Transparent;
      this.lblHistoryLine.Dock = DockStyle.Fill;
      this.lblHistoryLine.ForeColor = Color.Blue;
      this.lblHistoryLine.Image = (Image) Resources.GroupLine0;
      this.lblHistoryLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblHistoryLine.Location = new Point(82, 0);
      this.lblHistoryLine.Name = "lblHistoryLine";
      this.lblHistoryLine.Size = new Size(351, 28);
      this.lblHistoryLine.TabIndex = 15;
      this.lblHistoryLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblOther.BackColor = Color.Transparent;
      this.lblOther.Dock = DockStyle.Left;
      this.lblOther.ForeColor = Color.Blue;
      this.lblOther.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblOther.Location = new Point(7, 0);
      this.lblOther.Name = "lblOther";
      this.lblOther.Size = new Size(75, 28);
      this.lblOther.TabIndex = 13;
      this.lblOther.Text = "Other";
      this.lblOther.TextAlign = ContentAlignment.MiddleLeft;
      this.numCmbContentsExpandLevel.Location = new Point(217, 183);
      this.numCmbContentsExpandLevel.Maximum = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.numCmbContentsExpandLevel.Minimum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.numCmbContentsExpandLevel.Name = "numCmbContentsExpandLevel";
      this.numCmbContentsExpandLevel.Size = new Size(40, 21);
      this.numCmbContentsExpandLevel.TabIndex = 26;
      this.numCmbContentsExpandLevel.TextAlign = HorizontalAlignment.Center;
      this.numCmbContentsExpandLevel.Value = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.lblContentsExpandLevel.AutoSize = true;
      this.lblContentsExpandLevel.Location = new Point(19, 187);
      this.lblContentsExpandLevel.Name = "lblContentsExpandLevel";
      this.lblContentsExpandLevel.Size = new Size(140, 13);
      this.lblContentsExpandLevel.TabIndex = 24;
      this.lblContentsExpandLevel.Text = "Contents expanded to level";
      this.chkContentsExpandAll.AutoSize = true;
      this.chkContentsExpandAll.Location = new Point(21, 165);
      this.chkContentsExpandAll.Name = "chkContentsExpandAll";
      this.chkContentsExpandAll.Size = new Size(123, 17);
      this.chkContentsExpandAll.TabIndex = 23;
      this.chkContentsExpandAll.Text = "Expand All Contents";
      this.chkContentsExpandAll.UseVisualStyleBackColor = true;
      this.chkContentsExpandAll.CheckedChanged += new EventHandler(this.chkContentsExpandAll_CheckedChanged);
      this.chkHankakuZenKaku.AutoSize = true;
      this.chkHankakuZenKaku.Location = new Point(21, 142);
      this.chkHankakuZenKaku.Name = "chkHankakuZenKaku";
      this.chkHankakuZenKaku.Size = new Size(171, 17);
      this.chkHankakuZenKaku.TabIndex = 22;
      this.chkHankakuZenKaku.Text = "Search HanKaku <-> ZenKaku";
      this.chkHankakuZenKaku.UseVisualStyleBackColor = true;
      this.chkMouseWheelScrollOnPicture.AutoSize = true;
      this.chkMouseWheelScrollOnPicture.Location = new Point(21, 119);
      this.chkMouseWheelScrollOnPicture.Name = "chkMouseWheelScrollOnPicture";
      this.chkMouseWheelScrollOnPicture.Size = new Size(169, 17);
      this.chkMouseWheelScrollOnPicture.TabIndex = 21;
      this.chkMouseWheelScrollOnPicture.Text = "Mouse Wheel Scroll on Picture";
      this.chkMouseWheelScrollOnPicture.UseVisualStyleBackColor = true;
      this.chkMouseWheelScrollOnContents.AutoSize = true;
      this.chkMouseWheelScrollOnContents.Location = new Point(21, 96);
      this.chkMouseWheelScrollOnContents.Name = "chkMouseWheelScrollOnContents";
      this.chkMouseWheelScrollOnContents.Size = new Size(180, 17);
      this.chkMouseWheelScrollOnContents.TabIndex = 20;
      this.chkMouseWheelScrollOnContents.Text = "Mouse Wheel Scroll on Contents";
      this.chkMouseWheelScrollOnContents.UseVisualStyleBackColor = true;
      this.chkSideBySidePrinting.AutoSize = true;
      this.chkSideBySidePrinting.Location = new Point(21, 73);
      this.chkSideBySidePrinting.Name = "chkSideBySidePrinting";
      this.chkSideBySidePrinting.Size = new Size(123, 17);
      this.chkSideBySidePrinting.TabIndex = 19;
      this.chkSideBySidePrinting.Text = "Side By Side Printing";
      this.chkSideBySidePrinting.UseVisualStyleBackColor = true;
      this.chkOpenBookInCurrentWindow.AutoSize = true;
      this.chkOpenBookInCurrentWindow.Location = new Point(21, 50);
      this.chkOpenBookInCurrentWindow.Name = "chkOpenBookInCurrentWindow";
      this.chkOpenBookInCurrentWindow.Size = new Size(170, 17);
      this.chkOpenBookInCurrentWindow.TabIndex = 17;
      this.chkOpenBookInCurrentWindow.Text = "Open Book in Current Window";
      this.chkOpenBookInCurrentWindow.UseVisualStyleBackColor = true;
      this.lblHistorySize.AutoSize = true;
      this.lblHistorySize.Location = new Point(20, 28);
      this.lblHistorySize.Name = "lblHistorySize";
      this.lblHistorySize.Size = new Size(63, 13);
      this.lblHistorySize.TabIndex = 8;
      this.lblHistorySize.Text = "History Size";
      this.panel1.Controls.Add((Control) this.btnOK);
      this.panel1.Controls.Add((Control) this.btnCancel);
      this.panel1.Dock = DockStyle.Bottom;
      this.panel1.Location = new Point(0, 537);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(448, 30);
      this.panel1.TabIndex = 29;
      this.pnlLanguage.Controls.Add((Control) this.lblDefaultLanguage);
      this.pnlLanguage.Controls.Add((Control) this.panel2);
      this.pnlLanguage.Controls.Add((Control) this.pnlLang);
      this.pnlLanguage.Dock = DockStyle.Top;
      this.pnlLanguage.Location = new Point(0, 237);
      this.pnlLanguage.Name = "pnlLanguage";
      this.pnlLanguage.Size = new Size(448, 55);
      this.pnlLanguage.TabIndex = 27;
      this.lblDefaultLanguage.AccessibleRole = AccessibleRole.None;
      this.lblDefaultLanguage.AutoSize = true;
      this.lblDefaultLanguage.Location = new Point(20, 33);
      this.lblDefaultLanguage.Name = "lblDefaultLanguage";
      this.lblDefaultLanguage.Size = new Size(92, 13);
      this.lblDefaultLanguage.TabIndex = 18;
      this.lblDefaultLanguage.Text = "Default Language";
      this.panel2.Controls.Add((Control) this.label2);
      this.panel2.Controls.Add((Control) this.lblLanguage);
      this.panel2.Dock = DockStyle.Top;
      this.panel2.Location = new Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new Padding(7, 0, 15, 0);
      this.panel2.Size = new Size(448, 28);
      this.panel2.TabIndex = 17;
      this.label2.BackColor = Color.Transparent;
      this.label2.Dock = DockStyle.Fill;
      this.label2.ForeColor = Color.Blue;
      this.label2.Image = (Image) Resources.GroupLine0;
      this.label2.ImageAlign = ContentAlignment.MiddleLeft;
      this.label2.Location = new Point(82, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(351, 28);
      this.label2.TabIndex = 15;
      this.label2.TextAlign = ContentAlignment.MiddleLeft;
      this.lblLanguage.BackColor = Color.Transparent;
      this.lblLanguage.Dock = DockStyle.Left;
      this.lblLanguage.ForeColor = Color.Blue;
      this.lblLanguage.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblLanguage.Location = new Point(7, 0);
      this.lblLanguage.Name = "lblLanguage";
      this.lblLanguage.Size = new Size(75, 28);
      this.lblLanguage.TabIndex = 12;
      this.lblLanguage.Text = "Language";
      this.lblLanguage.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlLang.BorderStyle = BorderStyle.FixedSingle;
      this.pnlLang.Controls.Add((Control) this.cmbLanguagesList);
      this.pnlLang.Location = new Point(134, 31);
      this.pnlLang.Name = "pnlLang";
      this.pnlLang.Size = new Size(124, 21);
      this.pnlLang.TabIndex = 20;
      this.cmbLanguagesList.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbLanguagesList.DropDownWidth = 121;
      this.cmbLanguagesList.FlatStyle = FlatStyle.Flat;
      this.cmbLanguagesList.FormattingEnabled = true;
      this.cmbLanguagesList.Location = new Point(0, 0);
      this.cmbLanguagesList.Name = "cmbLanguagesList";
      this.cmbLanguagesList.Size = new Size(122, 21);
      this.cmbLanguagesList.TabIndex = 19;
      this.cmbLanguagesList.SelectedIndexChanged += new EventHandler(this.cmbLanguagesList_SelectedIndexChanged);
      this.pnlPartslist.Controls.Add((Control) this.pnlLblPartslist);
      this.pnlPartslist.Controls.Add((Control) this.chkShowPartslistToolbar);
      this.pnlPartslist.Dock = DockStyle.Top;
      this.pnlPartslist.Location = new Point(0, 182);
      this.pnlPartslist.Name = "pnlPartslist";
      this.pnlPartslist.Size = new Size(448, 55);
      this.pnlPartslist.TabIndex = 26;
      this.pnlLblPartslist.Controls.Add((Control) this.lblPartslistLine);
      this.pnlLblPartslist.Controls.Add((Control) this.lblPartslist);
      this.pnlLblPartslist.Dock = DockStyle.Top;
      this.pnlLblPartslist.Location = new Point(0, 0);
      this.pnlLblPartslist.Name = "pnlLblPartslist";
      this.pnlLblPartslist.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblPartslist.Size = new Size(448, 28);
      this.pnlLblPartslist.TabIndex = 17;
      this.lblPartslistLine.BackColor = Color.Transparent;
      this.lblPartslistLine.Dock = DockStyle.Fill;
      this.lblPartslistLine.ForeColor = Color.Blue;
      this.lblPartslistLine.Image = (Image) Resources.GroupLine0;
      this.lblPartslistLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPartslistLine.Location = new Point(82, 0);
      this.lblPartslistLine.Name = "lblPartslistLine";
      this.lblPartslistLine.Size = new Size(351, 28);
      this.lblPartslistLine.TabIndex = 15;
      this.lblPartslistLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPartslist.BackColor = Color.Transparent;
      this.lblPartslist.Dock = DockStyle.Left;
      this.lblPartslist.ForeColor = Color.Blue;
      this.lblPartslist.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPartslist.Location = new Point(7, 0);
      this.lblPartslist.Name = "lblPartslist";
      this.lblPartslist.Size = new Size(75, 28);
      this.lblPartslist.TabIndex = 12;
      this.lblPartslist.Text = "Partslist";
      this.lblPartslist.TextAlign = ContentAlignment.MiddleLeft;
      this.chkShowPartslistToolbar.AutoSize = true;
      this.chkShowPartslistToolbar.Location = new Point(22, 31);
      this.chkShowPartslistToolbar.Name = "chkShowPartslistToolbar";
      this.chkShowPartslistToolbar.Size = new Size(178, 17);
      this.chkShowPartslistToolbar.TabIndex = 1;
      this.chkShowPartslistToolbar.Text = "Show Partslist Toolbar In Frame";
      this.chkShowPartslistToolbar.UseVisualStyleBackColor = true;
      this.pnlPicture.Controls.Add((Control) this.chkFitPageForMulitParts);
      this.pnlPicture.Controls.Add((Control) this.txtZoomStep);
      this.pnlPicture.Controls.Add((Control) this.lblZoomStep);
      this.pnlPicture.Controls.Add((Control) this.pnlcmbDefaultZoom);
      this.pnlPicture.Controls.Add((Control) this.chkShowPictureToolbar);
      this.pnlPicture.Controls.Add((Control) this.chkMaintainZoomWhileNavigating);
      this.pnlPicture.Controls.Add((Control) this.lblDefaultZoom);
      this.pnlPicture.Controls.Add((Control) this.pnlLblPicture);
      this.pnlPicture.Dock = DockStyle.Top;
      this.pnlPicture.Location = new Point(0, 27);
      this.pnlPicture.Name = "pnlPicture";
      this.pnlPicture.Size = new Size(448, 155);
      this.pnlPicture.TabIndex = 25;
      this.chkFitPageForMulitParts.AutoSize = true;
      this.chkFitPageForMulitParts.Location = new Point(21, 77);
      this.chkFitPageForMulitParts.Name = "chkFitPageForMulitParts";
      this.chkFitPageForMulitParts.Size = new Size(162, 17);
      this.chkFitPageForMulitParts.TabIndex = 20;
      this.chkFitPageForMulitParts.Text = "Zoom fit Page for multi parts";
      this.chkFitPageForMulitParts.UseVisualStyleBackColor = true;
      this.txtZoomStep.AllowSpace = false;
      this.txtZoomStep.BorderStyle = BorderStyle.FixedSingle;
      this.txtZoomStep.Location = new Point(134, 128);
      this.txtZoomStep.MaxLength = 3;
      this.txtZoomStep.Name = "txtZoomStep";
      this.txtZoomStep.Size = new Size(124, 21);
      this.txtZoomStep.TabIndex = 19;
      this.lblZoomStep.AutoSize = true;
      this.lblZoomStep.Location = new Point(20, 130);
      this.lblZoomStep.Name = "lblZoomStep";
      this.lblZoomStep.Size = new Size(58, 13);
      this.lblZoomStep.TabIndex = 18;
      this.lblZoomStep.Text = "Zoom Step";
      this.pnlcmbDefaultZoom.BorderStyle = BorderStyle.FixedSingle;
      this.pnlcmbDefaultZoom.Controls.Add((Control) this.cmbDefaultZoom);
      this.pnlcmbDefaultZoom.Location = new Point(134, 100);
      this.pnlcmbDefaultZoom.Name = "pnlcmbDefaultZoom";
      this.pnlcmbDefaultZoom.Size = new Size(124, 21);
      this.pnlcmbDefaultZoom.TabIndex = 14;
      this.cmbDefaultZoom.BackColor = SystemColors.Window;
      this.cmbDefaultZoom.Dock = DockStyle.Fill;
      this.cmbDefaultZoom.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbDefaultZoom.DropDownWidth = 121;
      this.cmbDefaultZoom.FlatStyle = FlatStyle.Flat;
      this.cmbDefaultZoom.FormattingEnabled = true;
      this.cmbDefaultZoom.Items.AddRange(new object[5]
      {
        (object) "300%",
        (object) "150%",
        (object) "100%",
        (object) "50%",
        (object) "25%"
      });
      this.cmbDefaultZoom.Location = new Point(0, 0);
      this.cmbDefaultZoom.Name = "cmbDefaultZoom";
      this.cmbDefaultZoom.Size = new Size(122, 21);
      this.cmbDefaultZoom.TabIndex = 14;
      this.chkShowPictureToolbar.AutoSize = true;
      this.chkShowPictureToolbar.Location = new Point(21, 31);
      this.chkShowPictureToolbar.Name = "chkShowPictureToolbar";
      this.chkShowPictureToolbar.Size = new Size(173, 17);
      this.chkShowPictureToolbar.TabIndex = 1;
      this.chkShowPictureToolbar.Text = "Show Picture Toolbar In Frame";
      this.chkShowPictureToolbar.UseVisualStyleBackColor = true;
      this.chkMaintainZoomWhileNavigating.AutoSize = true;
      this.chkMaintainZoomWhileNavigating.Location = new Point(21, 54);
      this.chkMaintainZoomWhileNavigating.Name = "chkMaintainZoomWhileNavigating";
      this.chkMaintainZoomWhileNavigating.Size = new Size(264, 17);
      this.chkMaintainZoomWhileNavigating.TabIndex = 1;
      this.chkMaintainZoomWhileNavigating.Text = "Maintain Zoom Level On Pictures While Navigating";
      this.chkMaintainZoomWhileNavigating.UseVisualStyleBackColor = true;
      this.lblDefaultZoom.AutoSize = true;
      this.lblDefaultZoom.Location = new Point(20, 106);
      this.lblDefaultZoom.Name = "lblDefaultZoom";
      this.lblDefaultZoom.Size = new Size(71, 13);
      this.lblDefaultZoom.TabIndex = 8;
      this.lblDefaultZoom.Text = "Default Zoom";
      this.pnlLblPicture.Controls.Add((Control) this.lblPictureLine);
      this.pnlLblPicture.Controls.Add((Control) this.lblPicture);
      this.pnlLblPicture.Dock = DockStyle.Top;
      this.pnlLblPicture.Location = new Point(0, 0);
      this.pnlLblPicture.Name = "pnlLblPicture";
      this.pnlLblPicture.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblPicture.Size = new Size(448, 28);
      this.pnlLblPicture.TabIndex = 17;
      this.lblPictureLine.BackColor = Color.Transparent;
      this.lblPictureLine.Dock = DockStyle.Fill;
      this.lblPictureLine.ForeColor = Color.Blue;
      this.lblPictureLine.Image = (Image) Resources.GroupLine0;
      this.lblPictureLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPictureLine.Location = new Point(82, 0);
      this.lblPictureLine.Name = "lblPictureLine";
      this.lblPictureLine.Size = new Size(351, 28);
      this.lblPictureLine.TabIndex = 15;
      this.lblPictureLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPicture.BackColor = Color.Transparent;
      this.lblPicture.Dock = DockStyle.Left;
      this.lblPicture.ForeColor = Color.Blue;
      this.lblPicture.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPicture.Location = new Point(7, 0);
      this.lblPicture.Name = "lblPicture";
      this.lblPicture.Size = new Size(75, 28);
      this.lblPicture.TabIndex = 12;
      this.lblPicture.Text = "Picture";
      this.lblPicture.TextAlign = ContentAlignment.MiddleLeft;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(450, 569);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmConfigViewerGeneral);
      this.Load += new EventHandler(this.frmConfigViewerGeneral_Load);
      this.pnlForm.ResumeLayout(false);
      this.pnlHistory.ResumeLayout(false);
      this.pnlHistory.PerformLayout();
      this.pnlLblHistory.ResumeLayout(false);
      this.numCmbContentsExpandLevel.EndInit();
      this.panel1.ResumeLayout(false);
      this.pnlLanguage.ResumeLayout(false);
      this.pnlLanguage.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.pnlLang.ResumeLayout(false);
      this.pnlPartslist.ResumeLayout(false);
      this.pnlPartslist.PerformLayout();
      this.pnlLblPartslist.ResumeLayout(false);
      this.pnlPicture.ResumeLayout(false);
      this.pnlPicture.PerformLayout();
      this.pnlcmbDefaultZoom.ResumeLayout(false);
      this.pnlLblPicture.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}

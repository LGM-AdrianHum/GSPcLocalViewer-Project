// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConfigTasks
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class frmConfigTasks : Form
  {
    public frmConfig frmParent;
    private IContainer components;
    private Panel pnlTasks;
    private Label lblTasksTitle;
    private Panel pnlTasks2;
    private Label lblSelectionList;
    private Label lblPartList;
    private Label lblList;
    private Label lblMemosSettings;
    private Label lblMemos;
    private Label lblSpace1;
    private Label lblViewerGeneral;
    private Label lblViewerColor;
    private Label lblViewerFont;
    private Label lblViewer;
    private Label lblAdvanceSearch;
    private Label lblPartNumber;
    private Label lblPartName;
    private Label lblText;
    private Label lblPageName;
    private Label lblSearch;

    public frmConfigTasks(frmConfig frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmConfigTasks_Load(object sender, EventArgs e)
    {
      this.OnOffFeatures();
      if (this.frmParent.Owner.GetType() != typeof (frmOpenBook))
        return;
      this.lblSearch.Visible = false;
      this.lblPageName.Visible = false;
      this.lblPartName.Visible = false;
      this.lblPartNumber.Visible = false;
    }

    public void lblViewerFont_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblViewerFont);
      this.frmParent.HideForms();
      this.frmParent.objFrmViewerFont.Show();
    }

    public void lblPartsListInfo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblViewerColor);
      this.frmParent.HideForms();
      this.frmParent.objFrmViewerColor.Show();
    }

    public void lblViewerGeneral_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblViewerGeneral);
      this.frmParent.HideForms();
      this.frmParent.objFrmViewerGeneral.Show();
    }

    public void lblMemosSettings_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblMemosSettings);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoSettings.Show();
    }

    public void lblPageName_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPageName);
      this.frmParent.HideForms();
      this.frmParent.objFrmSearchPageName.Show();
    }

    private void lblText_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblText);
      this.frmParent.HideForms();
      this.frmParent.objFrmSearchText.Show();
    }

    public void lblPartNumber_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPartNumber);
      this.frmParent.HideForms();
      this.frmParent.objFrmSearchPartNumber.Show();
    }

    public void lblPartName_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPartName);
      this.frmParent.HideForms();
      this.frmParent.objFrmSearchPartName.Show();
    }

    private void lblAdvanceSearch_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAdvanceSearch);
      this.frmParent.HideForms();
      this.frmParent.objFrmSearchAdvance.Show();
    }

    private void lblPartList_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPartList);
      this.frmParent.HideForms();
      this.frmParent.objFrmPartList.Show();
    }

    private void lblSelectionList_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblSelectionList);
      this.frmParent.HideForms();
      this.frmParent.objFrmSelectionList.Show();
    }

    public void ShowTask(ConfigTasks task)
    {
      switch (task)
      {
        case ConfigTasks.Viewer_Font:
          this.lblViewerFont_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Viewer_Color:
          this.lblPartsListInfo_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Viewer_General:
          this.lblViewerGeneral_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Memo_Settings:
          this.lblMemosSettings_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Search_PageName:
          this.lblPageName_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Search_Text:
          this.lblText_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Search_PartName:
          this.lblPartName_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Search_PartNumber:
          this.lblPartNumber_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.Search_Advance:
          this.lblAdvanceSearch_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.SelectionListSettings:
          this.lblSelectionList_Click((object) null, (EventArgs) null);
          break;
        case ConfigTasks.PartListSettings:
          this.lblPartList_Click((object) null, (EventArgs) null);
          break;
      }
    }

    private void HighlightList(ref Label lbl)
    {
      if (!lbl.Parent.Name.Equals(this.pnlTasks2.Name))
        return;
      for (int index = 0; index < this.pnlTasks2.Controls.Count; ++index)
      {
        if (this.pnlTasks2.Controls[index] == this.lblViewerFont | this.pnlTasks2.Controls[index] == this.lblViewerGeneral | this.pnlTasks2.Controls[index] == this.lblViewerColor | this.pnlTasks2.Controls[index] == this.lblPageName | this.pnlTasks2.Controls[index] == this.lblText | this.pnlTasks2.Controls[index] == this.lblPartNumber | this.pnlTasks2.Controls[index] == this.lblPartName | this.pnlTasks2.Controls[index] == this.lblAdvanceSearch | this.pnlTasks2.Controls[index] == this.lblMemosSettings | this.pnlTasks2.Controls[index] == this.lblPartList | this.pnlTasks2.Controls[index] == this.lblSelectionList)
        {
          this.pnlTasks2.Controls[index].BackColor = this.pnlTasks2.BackColor;
          this.pnlTasks2.Controls[index].ForeColor = this.pnlTasks2.ForeColor;
        }
      }
      lbl.BackColor = Settings.Default.appHighlightBackColor;
      lbl.ForeColor = Settings.Default.appHighlightForeColor;
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblTasksTitle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private void LoadResources()
    {
      this.lblTasksTitle.Text = this.GetResource("Tasks", "TASKS", ResourceType.LABEL);
      this.lblViewer.Text = this.GetResource("Viewer", "VIEWER", ResourceType.LABEL);
      this.lblViewerFont.Text = this.GetResource("Font", "FONT", ResourceType.LABEL);
      this.lblViewerColor.Text = this.GetResource("Color", "COLOR", ResourceType.LABEL);
      this.lblViewerGeneral.Text = this.GetResource("General", "GENERAL", ResourceType.LABEL);
      this.lblMemos.Text = this.GetResource("Memos", "MEMOS", ResourceType.LABEL);
      this.lblMemosSettings.Text = this.GetResource("Settings", "SETTINGS", ResourceType.LABEL);
      this.lblSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.LABEL);
      this.lblPageName.Text = this.GetResource("Page Name", "PAGE_NAME", ResourceType.LABEL);
      this.lblPartName.Text = this.GetResource("Part Name", "PART_NAME", ResourceType.LABEL);
      this.lblPartNumber.Text = this.GetResource("Part Number", "PART_NUMBER", ResourceType.LABEL);
      this.lblAdvanceSearch.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.LABEL);
      this.lblText.Text = this.GetResource("Text", "TEXT_SEARCH", ResourceType.LABEL);
      this.lblPartList.Text = this.GetResource("Parts List", "PARTSLIST_SETTINGS", ResourceType.LABEL);
      this.lblSelectionList.Text = this.GetResource("Selection List", "SELECTIONLIST_SETTINGS", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONFIGURATION']" + "/Screen[@Name='CONFIGURATION_TASKS']";
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

    public void OnOffFeatures()
    {
      try
      {
        this.lblMemos.Visible = Program.objAppFeatures.bMemo;
        this.lblMemosSettings.Visible = Program.objAppFeatures.bMemo;
        this.lblPageName.Visible = Program.objAppFeatures.bPageNameSearch;
        this.lblPartName.Visible = Program.objAppFeatures.bPartNameSearch;
        this.lblPartNumber.Visible = Program.objAppFeatures.bPartNumberSearch;
        this.lblAdvanceSearch.Visible = Program.objAppFeatures.bPartAdvanceSearch;
        this.lblText.Visible = Program.objAppFeatures.bTextSearch;
        if (!Program.objAppFeatures.bPageNameSearch && !Program.objAppFeatures.bPartNameSearch && !Program.objAppFeatures.bPartNumberSearch)
          this.lblSearch.Visible = false;
        if (this.frmParent.frmParent.BookType.ToUpper().Trim() == "GSC")
        {
          this.lblPartName.Enabled = false;
          this.lblPartNumber.Enabled = false;
          this.lblAdvanceSearch.Enabled = false;
        }
        this.lblPartList.Visible = Program.objAppFeatures.bPartsList;
        this.lblSelectionList.Visible = Program.objAppFeatures.bSelectionList;
        if (Program.objAppFeatures.bPartsList || Program.objAppFeatures.bSelectionList)
          return;
        this.lblList.Visible = false;
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
      this.pnlTasks = new Panel();
      this.pnlTasks2 = new Panel();
      this.lblAdvanceSearch = new Label();
      this.lblPartNumber = new Label();
      this.lblPartName = new Label();
      this.lblText = new Label();
      this.lblPageName = new Label();
      this.lblSearch = new Label();
      this.lblSelectionList = new Label();
      this.lblPartList = new Label();
      this.lblList = new Label();
      this.lblMemosSettings = new Label();
      this.lblMemos = new Label();
      this.lblSpace1 = new Label();
      this.lblViewerGeneral = new Label();
      this.lblViewerColor = new Label();
      this.lblViewerFont = new Label();
      this.lblViewer = new Label();
      this.lblTasksTitle = new Label();
      this.pnlTasks.SuspendLayout();
      this.pnlTasks2.SuspendLayout();
      this.SuspendLayout();
      this.pnlTasks.BackColor = Color.White;
      this.pnlTasks.BorderStyle = BorderStyle.FixedSingle;
      this.pnlTasks.Controls.Add((Control) this.pnlTasks2);
      this.pnlTasks.Controls.Add((Control) this.lblTasksTitle);
      this.pnlTasks.Dock = DockStyle.Fill;
      this.pnlTasks.ForeColor = Color.Black;
      this.pnlTasks.Location = new Point(0, 0);
      this.pnlTasks.Name = "pnlTasks";
      this.pnlTasks.Size = new Size(151, 392);
      this.pnlTasks.TabIndex = 8;
      this.pnlTasks2.BackColor = Color.White;
      this.pnlTasks2.Controls.Add((Control) this.lblAdvanceSearch);
      this.pnlTasks2.Controls.Add((Control) this.lblPartNumber);
      this.pnlTasks2.Controls.Add((Control) this.lblPartName);
      this.pnlTasks2.Controls.Add((Control) this.lblText);
      this.pnlTasks2.Controls.Add((Control) this.lblPageName);
      this.pnlTasks2.Controls.Add((Control) this.lblSearch);
      this.pnlTasks2.Controls.Add((Control) this.lblSelectionList);
      this.pnlTasks2.Controls.Add((Control) this.lblPartList);
      this.pnlTasks2.Controls.Add((Control) this.lblList);
      this.pnlTasks2.Controls.Add((Control) this.lblMemosSettings);
      this.pnlTasks2.Controls.Add((Control) this.lblMemos);
      this.pnlTasks2.Controls.Add((Control) this.lblSpace1);
      this.pnlTasks2.Controls.Add((Control) this.lblViewerGeneral);
      this.pnlTasks2.Controls.Add((Control) this.lblViewerColor);
      this.pnlTasks2.Controls.Add((Control) this.lblViewerFont);
      this.pnlTasks2.Controls.Add((Control) this.lblViewer);
      this.pnlTasks2.Dock = DockStyle.Top;
      this.pnlTasks2.Location = new Point(0, 27);
      this.pnlTasks2.Name = "pnlTasks2";
      this.pnlTasks2.Padding = new Padding(15, 10, 15, 0);
      this.pnlTasks2.Size = new Size(149, 360);
      this.pnlTasks2.TabIndex = 11;
      this.lblAdvanceSearch.Cursor = Cursors.Hand;
      this.lblAdvanceSearch.Dock = DockStyle.Top;
      this.lblAdvanceSearch.Location = new Point(15, 293);
      this.lblAdvanceSearch.Name = "lblAdvanceSearch";
      this.lblAdvanceSearch.Padding = new Padding(20, 0, 0, 0);
      this.lblAdvanceSearch.Size = new Size(119, 16);
      this.lblAdvanceSearch.TabIndex = 48;
      this.lblAdvanceSearch.Text = "Advance Search";
      this.lblAdvanceSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdvanceSearch.Click += new EventHandler(this.lblAdvanceSearch_Click);
      this.lblPartNumber.Cursor = Cursors.Hand;
      this.lblPartNumber.Dock = DockStyle.Top;
      this.lblPartNumber.Location = new Point(15, 277);
      this.lblPartNumber.Name = "lblPartNumber";
      this.lblPartNumber.Padding = new Padding(20, 0, 0, 0);
      this.lblPartNumber.Size = new Size(119, 16);
      this.lblPartNumber.TabIndex = 45;
      this.lblPartNumber.Text = "Part Number";
      this.lblPartNumber.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPartNumber.Click += new EventHandler(this.lblPartNumber_Click);
      this.lblPartName.Cursor = Cursors.Hand;
      this.lblPartName.Dock = DockStyle.Top;
      this.lblPartName.Location = new Point(15, 261);
      this.lblPartName.Name = "lblPartName";
      this.lblPartName.Padding = new Padding(20, 0, 0, 0);
      this.lblPartName.Size = new Size(119, 16);
      this.lblPartName.TabIndex = 46;
      this.lblPartName.Text = "Part Name";
      this.lblPartName.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPartName.Click += new EventHandler(this.lblPartName_Click);
      this.lblText.Cursor = Cursors.Hand;
      this.lblText.Dock = DockStyle.Top;
      this.lblText.Location = new Point(15, 245);
      this.lblText.Name = "lblText";
      this.lblText.Padding = new Padding(20, 0, 0, 0);
      this.lblText.Size = new Size(119, 16);
      this.lblText.TabIndex = 47;
      this.lblText.Text = "Text";
      this.lblText.TextAlign = ContentAlignment.MiddleLeft;
      this.lblText.Click += new EventHandler(this.lblText_Click);
      this.lblPageName.Cursor = Cursors.Hand;
      this.lblPageName.Dock = DockStyle.Top;
      this.lblPageName.Location = new Point(15, 229);
      this.lblPageName.Name = "lblPageName";
      this.lblPageName.Padding = new Padding(20, 0, 0, 0);
      this.lblPageName.Size = new Size(119, 16);
      this.lblPageName.TabIndex = 44;
      this.lblPageName.Text = "Page Name";
      this.lblPageName.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPageName.Click += new EventHandler(this.lblPageName_Click);
      this.lblSearch.BackColor = Color.Transparent;
      this.lblSearch.Dock = DockStyle.Top;
      this.lblSearch.ForeColor = Color.Blue;
      this.lblSearch.Image = (Image) Resources.GroupLine1;
      this.lblSearch.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSearch.Location = new Point(15, 201);
      this.lblSearch.Name = "lblSearch";
      this.lblSearch.Size = new Size(119, 28);
      this.lblSearch.TabIndex = 43;
      this.lblSearch.Text = "Search";
      this.lblSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectionList.Cursor = Cursors.Hand;
      this.lblSelectionList.Dock = DockStyle.Top;
      this.lblSelectionList.Location = new Point(15, 185);
      this.lblSelectionList.Name = "lblSelectionList";
      this.lblSelectionList.Padding = new Padding(20, 0, 0, 0);
      this.lblSelectionList.Size = new Size(119, 16);
      this.lblSelectionList.TabIndex = 42;
      this.lblSelectionList.Text = "Selection List";
      this.lblSelectionList.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSelectionList.Click += new EventHandler(this.lblSelectionList_Click);
      this.lblPartList.Cursor = Cursors.Hand;
      this.lblPartList.Dock = DockStyle.Top;
      this.lblPartList.Location = new Point(15, 169);
      this.lblPartList.Name = "lblPartList";
      this.lblPartList.Padding = new Padding(20, 0, 0, 0);
      this.lblPartList.Size = new Size(119, 16);
      this.lblPartList.TabIndex = 41;
      this.lblPartList.Text = "Parts List";
      this.lblPartList.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPartList.Click += new EventHandler(this.lblPartList_Click);
      this.lblList.BackColor = Color.Transparent;
      this.lblList.Dock = DockStyle.Top;
      this.lblList.ForeColor = Color.Blue;
      this.lblList.Image = (Image) Resources.GroupLine1;
      this.lblList.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblList.Location = new Point(15, 141);
      this.lblList.Name = "lblList";
      this.lblList.Size = new Size(119, 28);
      this.lblList.TabIndex = 40;
      this.lblList.Text = "Lists";
      this.lblList.TextAlign = ContentAlignment.MiddleLeft;
      this.lblMemosSettings.Cursor = Cursors.Hand;
      this.lblMemosSettings.Dock = DockStyle.Top;
      this.lblMemosSettings.Location = new Point(15, 125);
      this.lblMemosSettings.Name = "lblMemosSettings";
      this.lblMemosSettings.Padding = new Padding(20, 0, 0, 0);
      this.lblMemosSettings.Size = new Size(119, 16);
      this.lblMemosSettings.TabIndex = 22;
      this.lblMemosSettings.Text = "Settings";
      this.lblMemosSettings.TextAlign = ContentAlignment.MiddleLeft;
      this.lblMemosSettings.Click += new EventHandler(this.lblMemosSettings_Click);
      this.lblMemos.BackColor = Color.Transparent;
      this.lblMemos.Dock = DockStyle.Top;
      this.lblMemos.ForeColor = Color.Blue;
      this.lblMemos.Image = (Image) Resources.GroupLine1;
      this.lblMemos.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblMemos.Location = new Point(15, 97);
      this.lblMemos.Name = "lblMemos";
      this.lblMemos.Size = new Size(119, 28);
      this.lblMemos.TabIndex = 20;
      this.lblMemos.Text = "Memos";
      this.lblMemos.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSpace1.Cursor = Cursors.Hand;
      this.lblSpace1.Dock = DockStyle.Top;
      this.lblSpace1.Location = new Point(15, 86);
      this.lblSpace1.Name = "lblSpace1";
      this.lblSpace1.Padding = new Padding(20, 0, 0, 0);
      this.lblSpace1.Size = new Size(119, 11);
      this.lblSpace1.TabIndex = 19;
      this.lblSpace1.TextAlign = ContentAlignment.MiddleLeft;
      this.lblViewerGeneral.Cursor = Cursors.Hand;
      this.lblViewerGeneral.Dock = DockStyle.Top;
      this.lblViewerGeneral.Location = new Point(15, 70);
      this.lblViewerGeneral.Name = "lblViewerGeneral";
      this.lblViewerGeneral.Padding = new Padding(20, 0, 0, 0);
      this.lblViewerGeneral.Size = new Size(119, 16);
      this.lblViewerGeneral.TabIndex = 18;
      this.lblViewerGeneral.Text = "General";
      this.lblViewerGeneral.TextAlign = ContentAlignment.MiddleLeft;
      this.lblViewerGeneral.Click += new EventHandler(this.lblViewerGeneral_Click);
      this.lblViewerColor.Cursor = Cursors.Hand;
      this.lblViewerColor.Dock = DockStyle.Top;
      this.lblViewerColor.Location = new Point(15, 54);
      this.lblViewerColor.Name = "lblViewerColor";
      this.lblViewerColor.Padding = new Padding(20, 0, 0, 0);
      this.lblViewerColor.Size = new Size(119, 16);
      this.lblViewerColor.TabIndex = 33;
      this.lblViewerColor.Text = "Color";
      this.lblViewerColor.TextAlign = ContentAlignment.MiddleLeft;
      this.lblViewerColor.Click += new EventHandler(this.lblPartsListInfo_Click);
      this.lblViewerFont.Cursor = Cursors.Hand;
      this.lblViewerFont.Dock = DockStyle.Top;
      this.lblViewerFont.Location = new Point(15, 38);
      this.lblViewerFont.Name = "lblViewerFont";
      this.lblViewerFont.Padding = new Padding(20, 0, 0, 0);
      this.lblViewerFont.Size = new Size(119, 16);
      this.lblViewerFont.TabIndex = 15;
      this.lblViewerFont.Text = "Font";
      this.lblViewerFont.TextAlign = ContentAlignment.MiddleLeft;
      this.lblViewerFont.Click += new EventHandler(this.lblViewerFont_Click);
      this.lblViewer.BackColor = Color.Transparent;
      this.lblViewer.Dock = DockStyle.Top;
      this.lblViewer.ForeColor = Color.Blue;
      this.lblViewer.Image = (Image) Resources.GroupLine1;
      this.lblViewer.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblViewer.Location = new Point(15, 10);
      this.lblViewer.Name = "lblViewer";
      this.lblViewer.Size = new Size(119, 28);
      this.lblViewer.TabIndex = 11;
      this.lblViewer.Text = "Viewer";
      this.lblViewer.TextAlign = ContentAlignment.MiddleLeft;
      this.lblTasksTitle.BackColor = Color.White;
      this.lblTasksTitle.Dock = DockStyle.Top;
      this.lblTasksTitle.ForeColor = Color.Black;
      this.lblTasksTitle.Location = new Point(0, 0);
      this.lblTasksTitle.Name = "lblTasksTitle";
      this.lblTasksTitle.Padding = new Padding(3, 7, 0, 0);
      this.lblTasksTitle.Size = new Size(149, 27);
      this.lblTasksTitle.TabIndex = 6;
      this.lblTasksTitle.Text = "Tasks";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(151, 392);
      this.Controls.Add((Control) this.pnlTasks);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmConfigTasks);
      this.Load += new EventHandler(this.frmConfigTasks_Load);
      this.pnlTasks.ResumeLayout(false);
      this.pnlTasks2.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmSearchTasks
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class frmSearchTasks : Form
  {
    private IContainer components;
    private Panel pnlTasks;
    private Panel pnlTasks2;
    private Label lblTasksTitle;
    private Label lblSpace1;
    private Label lblPart;
    private Label lblPartNumber;
    private Label lblPartName;
    private Label lblPageName;
    private Label lblPage;
    private Label lblTextSearch;
    private Label label1;
    private StatusStrip ssStatus;
    private Label lblAdvance;
    private frmSearch frmParent;

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
      this.lblAdvance = new Label();
      this.lblPartNumber = new Label();
      this.lblPartName = new Label();
      this.lblPart = new Label();
      this.label1 = new Label();
      this.lblTextSearch = new Label();
      this.lblPageName = new Label();
      this.lblPage = new Label();
      this.lblSpace1 = new Label();
      this.lblTasksTitle = new Label();
      this.ssStatus = new StatusStrip();
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
      this.pnlTasks.Size = new Size(151, 370);
      this.pnlTasks.TabIndex = 8;
      this.pnlTasks2.BackColor = Color.White;
      this.pnlTasks2.Controls.Add((Control) this.lblAdvance);
      this.pnlTasks2.Controls.Add((Control) this.lblPartNumber);
      this.pnlTasks2.Controls.Add((Control) this.lblPartName);
      this.pnlTasks2.Controls.Add((Control) this.lblPart);
      this.pnlTasks2.Controls.Add((Control) this.label1);
      this.pnlTasks2.Controls.Add((Control) this.lblTextSearch);
      this.pnlTasks2.Controls.Add((Control) this.lblPageName);
      this.pnlTasks2.Controls.Add((Control) this.lblPage);
      this.pnlTasks2.Controls.Add((Control) this.lblSpace1);
      this.pnlTasks2.Location = new Point(0, 27);
      this.pnlTasks2.Name = "pnlTasks2";
      this.pnlTasks2.Padding = new Padding(15, 10, 15, 0);
      this.pnlTasks2.Size = new Size(149, 315);
      this.pnlTasks2.TabIndex = 11;
      this.pnlTasks2.Tag = (object) "";
      this.lblAdvance.Cursor = Cursors.Hand;
      this.lblAdvance.Dock = DockStyle.Top;
      this.lblAdvance.Location = new Point(15, 150);
      this.lblAdvance.Name = "lblAdvance";
      this.lblAdvance.Padding = new Padding(20, 0, 0, 0);
      this.lblAdvance.Size = new Size(119, 16);
      this.lblAdvance.TabIndex = 33;
      this.lblAdvance.Text = "Advance";
      this.lblAdvance.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdvance.Click += new EventHandler(this.lblAdvance_Click);
      this.lblPartNumber.Cursor = Cursors.Hand;
      this.lblPartNumber.Dock = DockStyle.Top;
      this.lblPartNumber.Location = new Point(15, 134);
      this.lblPartNumber.Name = "lblPartNumber";
      this.lblPartNumber.Padding = new Padding(20, 0, 0, 0);
      this.lblPartNumber.Size = new Size(119, 16);
      this.lblPartNumber.TabIndex = 27;
      this.lblPartNumber.Text = "Part Number";
      this.lblPartNumber.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPartNumber.Click += new EventHandler(this.lblPartNumber_Click);
      this.lblPartName.Cursor = Cursors.Hand;
      this.lblPartName.Dock = DockStyle.Top;
      this.lblPartName.Location = new Point(15, 118);
      this.lblPartName.Name = "lblPartName";
      this.lblPartName.Padding = new Padding(20, 0, 0, 0);
      this.lblPartName.Size = new Size(119, 16);
      this.lblPartName.TabIndex = 29;
      this.lblPartName.Text = "Part Name";
      this.lblPartName.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPartName.Click += new EventHandler(this.lblPartName_Click);
      this.lblPart.BackColor = Color.Transparent;
      this.lblPart.Dock = DockStyle.Top;
      this.lblPart.ForeColor = Color.Blue;
      this.lblPart.Image = (Image) Resources.GroupLine1;
      this.lblPart.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPart.Location = new Point(15, 90);
      this.lblPart.Name = "lblPart";
      this.lblPart.Size = new Size(119, 28);
      this.lblPart.TabIndex = 23;
      this.lblPart.Tag = (object) "";
      this.lblPart.Text = "Part";
      this.lblPart.TextAlign = ContentAlignment.MiddleLeft;
      this.label1.Cursor = Cursors.Hand;
      this.label1.Dock = DockStyle.Top;
      this.label1.Location = new Point(15, 80);
      this.label1.Name = "label1";
      this.label1.Padding = new Padding(20, 0, 0, 0);
      this.label1.Size = new Size(119, 10);
      this.label1.TabIndex = 31;
      this.label1.TextAlign = ContentAlignment.MiddleLeft;
      this.lblTextSearch.Cursor = Cursors.Hand;
      this.lblTextSearch.Dock = DockStyle.Top;
      this.lblTextSearch.Location = new Point(15, 64);
      this.lblTextSearch.Name = "lblTextSearch";
      this.lblTextSearch.Padding = new Padding(20, 0, 0, 0);
      this.lblTextSearch.Size = new Size(119, 16);
      this.lblTextSearch.TabIndex = 30;
      this.lblTextSearch.Text = "Text Search";
      this.lblTextSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.lblTextSearch.Click += new EventHandler(this.lblTextSearch_Click);
      this.lblPageName.Cursor = Cursors.Hand;
      this.lblPageName.Dock = DockStyle.Top;
      this.lblPageName.Location = new Point(15, 48);
      this.lblPageName.Name = "lblPageName";
      this.lblPageName.Padding = new Padding(20, 0, 0, 0);
      this.lblPageName.Size = new Size(119, 16);
      this.lblPageName.TabIndex = 22;
      this.lblPageName.Text = "Page Name";
      this.lblPageName.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPageName.Click += new EventHandler(this.lbPageName_Click);
      this.lblPage.BackColor = Color.Transparent;
      this.lblPage.Dock = DockStyle.Top;
      this.lblPage.ForeColor = Color.Blue;
      this.lblPage.Image = (Image) Resources.GroupLine1;
      this.lblPage.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPage.Location = new Point(15, 20);
      this.lblPage.Name = "lblPage";
      this.lblPage.Size = new Size(119, 28);
      this.lblPage.TabIndex = 20;
      this.lblPage.Tag = (object) "";
      this.lblPage.Text = "Page";
      this.lblPage.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSpace1.Cursor = Cursors.Hand;
      this.lblSpace1.Dock = DockStyle.Top;
      this.lblSpace1.Location = new Point(15, 10);
      this.lblSpace1.Name = "lblSpace1";
      this.lblSpace1.Padding = new Padding(20, 0, 0, 0);
      this.lblSpace1.Size = new Size(119, 10);
      this.lblSpace1.TabIndex = 19;
      this.lblSpace1.TextAlign = ContentAlignment.MiddleLeft;
      this.lblTasksTitle.BackColor = Color.White;
      this.lblTasksTitle.Dock = DockStyle.Top;
      this.lblTasksTitle.ForeColor = Color.Black;
      this.lblTasksTitle.Location = new Point(0, 0);
      this.lblTasksTitle.Name = "lblTasksTitle";
      this.lblTasksTitle.Padding = new Padding(3, 7, 0, 0);
      this.lblTasksTitle.Size = new Size(149, 27);
      this.lblTasksTitle.TabIndex = 6;
      this.lblTasksTitle.Text = "Search";
      this.ssStatus.BackColor = SystemColors.Control;
      this.ssStatus.Location = new Point(0, 370);
      this.ssStatus.Name = "ssStatus";
      this.ssStatus.Size = new Size(151, 22);
      this.ssStatus.SizingGrip = false;
      this.ssStatus.TabIndex = 22;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(151, 392);
      this.Controls.Add((Control) this.pnlTasks);
      this.Controls.Add((Control) this.ssStatus);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.KeyPreview = true;
      this.Name = nameof (frmSearchTasks);
      this.Load += new EventHandler(this.frmSearchTasks_Load);
      this.pnlTasks.ResumeLayout(false);
      this.pnlTasks2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public frmSearchTasks(frmSearch frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.UpdateFont();
      this.LoadResources();
    }

    public void ShowTask(SearchTasks search)
    {
      switch (search)
      {
        case SearchTasks.PageName:
          this.lbPageName_Click((object) null, (EventArgs) null);
          break;
        case SearchTasks.PartName:
          this.lblPartName_Click((object) null, (EventArgs) null);
          break;
        case SearchTasks.PartNumber:
          this.lblPartNumber_Click((object) null, (EventArgs) null);
          break;
        case SearchTasks.TextSearch:
          this.lblTextSearch_Click((object) null, (EventArgs) null);
          break;
        case SearchTasks.Advanced:
          this.lblAdvance_Click((object) null, (EventArgs) null);
          break;
      }
    }

    private void HighlightList(ref Label lbl)
    {
      if (!lbl.Parent.Name.Equals(this.pnlTasks2.Name))
        return;
      for (int index = 0; index < this.pnlTasks2.Controls.Count; ++index)
      {
        if (this.pnlTasks2.Controls[index] == this.lblPartNumber | this.pnlTasks2.Controls[index] == this.lblPartName | this.pnlTasks2.Controls[index] == this.lblPageName | this.pnlTasks2.Controls[index] == this.lblTextSearch | this.pnlTasks2.Controls[index] == this.lblAdvance)
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

    public void lbPageName_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPageName);
      this.frmParent.HideForms();
      this.frmParent.objFrmPageNameSearch.Show();
    }

    public void lblPartName_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPartName);
      this.frmParent.HideForms();
      this.frmParent.objFrmPartNameSearch.Show();
    }

    public void lblPartNumber_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPartNumber);
      this.frmParent.HideForms();
      this.frmParent.objFrmPartNumberSearch.Show();
    }

    public void lblTextSearch_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblTextSearch);
      this.frmParent.HideForms();
      this.frmParent.objFrmTextSearch.Show();
    }

    public void lblAdvance_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAdvance);
      this.frmParent.HideForms();
      this.frmParent.objFrmAdvanceSearch.Show();
    }

    private void LoadResources()
    {
      this.lblTasksTitle.Text = this.GetResource("Search", "SEARCH", ResourceType.LABEL);
      this.lblPage.Text = this.GetResource("Page", "PAGE", ResourceType.LABEL);
      this.lblPageName.Text = this.GetResource("Page Name", "PAGE_NAME", ResourceType.LABEL);
      this.lblTextSearch.Text = this.GetResource("Text Search", "TEXT_SEARCH", ResourceType.LABEL);
      this.lblPart.Text = this.GetResource("Part", "PART", ResourceType.LABEL);
      this.lblPartName.Text = this.GetResource("Part Name", "PART_NAME", ResourceType.LABEL);
      this.lblPartNumber.Text = this.GetResource("Part Number", "PART_NUMBER", ResourceType.LABEL);
      this.lblAdvance.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='SEARCH']" + "/Screen[@Name='SEARCH_TASKS']";
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

    private void frmSearchTasks_Load(object sender, EventArgs e)
    {
      this.OnOffFeatures();
    }

    public void OnOffFeatures()
    {
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        bool flag = false;
        ArrayList arrayList = new ArrayList();
        if (iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.frmParent.ServerId].sIniKey + ".ini", "PLIST_SEARCH").Count > 0 && Program.objAppFeatures.bPartAdvanceSearch)
          flag = true;
        this.lblPageName.Visible = Program.objAppFeatures.bPageNameSearch;
        this.lblPartName.Visible = Program.objAppFeatures.bPartNameSearch;
        this.lblPartNumber.Visible = Program.objAppFeatures.bPartNumberSearch;
        this.lblTextSearch.Visible = Program.objAppFeatures.bTextSearch;
        this.lblAdvance.Visible = flag;
        if (!Program.objAppFeatures.bTextSearch)
          this.lblTextSearch.Visible = false;
        if (this.frmParent.frmParent.BookType.ToUpper().Trim() == "GSC")
        {
          this.lblPart.Visible = false;
          this.lblPartName.Visible = false;
          this.lblPartNumber.Visible = false;
          this.lblAdvance.Visible = false;
        }
        if (!Program.objAppFeatures.bPartNameSearch && !Program.objAppFeatures.bPartNumberSearch && !flag)
          this.lblPart.Visible = false;
        if (Program.objAppFeatures.bPageNameSearch || Program.objAppFeatures.bTextSearch)
          return;
        this.lblPage.Visible = false;
      }
      catch
      {
      }
    }
  }
}

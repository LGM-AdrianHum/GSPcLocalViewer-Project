// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmSearch
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GSPcLocalViewer
{
  public class frmSearch : Form
  {
    private const uint LOCALE_SYSTEM_DEFAULT = 2048;
    private const uint LCMAP_HALFWIDTH = 4194304;
    private const uint LCMAP_FULLWIDTH = 8388608;
    private IContainer components;
    private Panel pnlContents;
    private Panel pnlTasks;
    private ToolStripContainer toolStripContainer1;
    private ToolStrip tsPage;
    private ToolStripButton tsbPageName;
    private ToolStrip tsPart;
    private ToolStripButton tsbPartNumber;
    private ToolStripButton tsbText;
    private ToolStripButton tsbPartName;
    private ToolStripButton tsbPartAdvance;
    public frmSearchTasks objFrmTasks;
    public frmPageNameSearch objFrmPageNameSearch;
    public frmPartNameSearch objFrmPartNameSearch;
    public frmPartNumberSearch objFrmPartNumberSearch;
    public frmTextSearch objFrmTextSearch;
    public frmAdvanceSearch objFrmAdvanceSearch;
    public frmViewer frmParent;
    private SearchTasks task;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlContents = new Panel();
      this.pnlTasks = new Panel();
      this.toolStripContainer1 = new ToolStripContainer();
      this.tsPage = new ToolStrip();
      this.tsbPageName = new ToolStripButton();
      this.tsbText = new ToolStripButton();
      this.tsPart = new ToolStrip();
      this.tsbPartName = new ToolStripButton();
      this.tsbPartNumber = new ToolStripButton();
      this.tsbPartAdvance = new ToolStripButton();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.tsPage.SuspendLayout();
      this.tsPart.SuspendLayout();
      this.SuspendLayout();
      this.pnlContents.Dock = DockStyle.Fill;
      this.pnlContents.Location = new Point(160, 0);
      this.pnlContents.Name = "pnlContents";
      this.pnlContents.Padding = new Padding(2);
      this.pnlContents.Size = new Size(624, 498);
      this.pnlContents.TabIndex = 18;
      this.pnlTasks.Dock = DockStyle.Left;
      this.pnlTasks.Location = new Point(0, 0);
      this.pnlTasks.Name = "pnlTasks";
      this.pnlTasks.Padding = new Padding(2);
      this.pnlTasks.Size = new Size(160, 498);
      this.pnlTasks.TabIndex = 17;
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.pnlContents);
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.pnlTasks);
      this.toolStripContainer1.ContentPanel.Size = new Size(784, 498);
      this.toolStripContainer1.Dock = DockStyle.Fill;
      this.toolStripContainer1.Location = new Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new Size(784, 523);
      this.toolStripContainer1.TabIndex = 4;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsPage);
      this.toolStripContainer1.TopToolStripPanel.Controls.Add((Control) this.tsPart);
      this.tsPage.Dock = DockStyle.None;
      this.tsPage.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.tsbPageName,
        (ToolStripItem) this.tsbText
      });
      this.tsPage.Location = new Point(3, 0);
      this.tsPage.Name = "tsPage";
      this.tsPage.Size = new Size(56, 25);
      this.tsPage.TabIndex = 0;
      this.tsbPageName.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPageName.Image = (Image) Resources.Search_Page;
      this.tsbPageName.ImageTransparentColor = Color.Magenta;
      this.tsbPageName.Name = "tsbPageName";
      this.tsbPageName.Size = new Size(23, 22);
      this.tsbPageName.Text = "Page Name Search";
      this.tsbPageName.Click += new EventHandler(this.tsbPageName_Click);
      this.tsbText.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbText.Image = (Image) Resources.Search_Text;
      this.tsbText.ImageTransparentColor = Color.Magenta;
      this.tsbText.Name = "tsbText";
      this.tsbText.Size = new Size(23, 22);
      this.tsbText.Text = "Text Search";
      this.tsbText.Click += new EventHandler(this.tsbText_Click);
      this.tsPart.Dock = DockStyle.None;
      this.tsPart.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.tsbPartName,
        (ToolStripItem) this.tsbPartNumber,
        (ToolStripItem) this.tsbPartAdvance
      });
      this.tsPart.Location = new Point(61, 0);
      this.tsPart.Name = "tsPart";
      this.tsPart.Size = new Size(79, 25);
      this.tsPart.TabIndex = 1;
      this.tsbPartName.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPartName.Image = (Image) Resources.Search_Parts;
      this.tsbPartName.ImageTransparentColor = Color.Magenta;
      this.tsbPartName.Name = "tsbPartName";
      this.tsbPartName.Size = new Size(23, 22);
      this.tsbPartName.Text = "Part Name Search";
      this.tsbPartName.Click += new EventHandler(this.tsbPartName_Click);
      this.tsbPartNumber.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPartNumber.Image = (Image) Resources.Search_Parts2;
      this.tsbPartNumber.ImageTransparentColor = Color.Magenta;
      this.tsbPartNumber.Name = "tsbPartNumber";
      this.tsbPartNumber.Size = new Size(23, 22);
      this.tsbPartNumber.Text = "Part Number Search";
      this.tsbPartNumber.Click += new EventHandler(this.tsbPartNumber_Click);
      this.tsbPartAdvance.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.tsbPartAdvance.Image = (Image) Resources.Search_PartsAdvance;
      this.tsbPartAdvance.ImageTransparentColor = Color.Magenta;
      this.tsbPartAdvance.Name = "tsbSearchPartAdvance";
      this.tsbPartAdvance.Size = new Size(23, 22);
      this.tsbPartAdvance.Text = "Advance Search";
      this.tsbPartAdvance.Click += new EventHandler(this.tsbPartAdvance_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(784, 523);
      this.Controls.Add((Control) this.toolStripContainer1);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.IsMdiContainer = true;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(750, 550);
      this.Name = nameof (frmSearch);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Search";
      this.Load += new EventHandler(this.frmSearch_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmSearch_FormClosing);
      this.PreviewKeyDown += new PreviewKeyDownEventHandler(this.frmSearch_PreviewKeyDown);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
      this.toolStripContainer1.TopToolStripPanel.PerformLayout();
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.tsPage.ResumeLayout(false);
      this.tsPage.PerformLayout();
      this.tsPart.ResumeLayout(false);
      this.tsPart.PerformLayout();
      this.ResumeLayout(false);
    }

    public frmSearch(frmViewer frm)
    {
      this.InitializeComponent();
      this.task = SearchTasks.PageName;
      this.frmParent = frm;
      this.UpdateFont();
      this.LoadResources();
    }

    public void Show(SearchTasks search)
    {
      this.task = search;
      this.Show();
    }

    private void frmSearch_Load(object sender, EventArgs e)
    {
      this.LoadUserSettings();
      this.CreateForms();
      this.objFrmTasks.Show();
      this.objFrmTasks.ShowTask(this.task);
      this.OnOffFeatures();
    }

    private void frmSearch_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      this.SaveUserSettings();
      if (!this.objFrmPageNameSearch.IsDisposed)
        this.objFrmPageNameSearch.Close();
      if (!this.objFrmAdvanceSearch.IsDisposed)
        this.objFrmAdvanceSearch.Close();
      if (!this.objFrmPartNameSearch.IsDisposed)
        this.objFrmPartNameSearch.Close();
      if (!this.objFrmPartNumberSearch.IsDisposed)
        this.objFrmPartNumberSearch.Close();
      if (!this.objFrmTextSearch.IsDisposed)
        this.objFrmTextSearch.Close();
      if (!this.objFrmTasks.IsDisposed)
        this.objFrmTasks.Close();
      this.frmParent.HideDimmer();
    }

    private void CreateForms()
    {
      this.objFrmPageNameSearch = new frmPageNameSearch(this.frmParent, this);
      this.objFrmPageNameSearch.TopLevel = false;
      this.objFrmPageNameSearch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmPageNameSearch);
      this.objFrmPartNumberSearch = new frmPartNumberSearch(this.frmParent, this);
      this.objFrmPartNumberSearch.TopLevel = false;
      this.objFrmPartNumberSearch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmPartNumberSearch);
      this.objFrmPartNameSearch = new frmPartNameSearch(this.frmParent, this);
      this.objFrmPartNameSearch.TopLevel = false;
      this.objFrmPartNameSearch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmPartNameSearch);
      this.objFrmTextSearch = new frmTextSearch(this.frmParent, this);
      this.objFrmTextSearch.TopLevel = false;
      this.objFrmTextSearch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmTextSearch);
      this.objFrmAdvanceSearch = new frmAdvanceSearch(this.frmParent, this);
      this.objFrmAdvanceSearch.TopLevel = false;
      this.objFrmAdvanceSearch.Dock = DockStyle.Fill;
      this.pnlContents.Controls.Add((Control) this.objFrmAdvanceSearch);
      this.objFrmTasks = new frmSearchTasks(this);
      this.objFrmTasks.TopLevel = false;
      this.objFrmTasks.Dock = DockStyle.Fill;
      this.pnlTasks.Controls.Add((Control) this.objFrmTasks);
    }

    public void HideForms()
    {
      foreach (Control control in (ArrangedElementCollection) this.pnlContents.Controls)
        control?.Hide();
    }

    public void CloseContainer()
    {
      this.Close();
    }

    public void ChangeGlobalMemoPath(string oldPath, string newPath)
    {
      this.frmParent.ChangeGlobalMemoPath(oldPath, newPath);
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }

    private void tsbPageName_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lbPageName_Click((object) null, (EventArgs) null);
    }

    private void tsbText_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblTextSearch_Click((object) null, (EventArgs) null);
    }

    private void tsbPartNumber_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblPartNumber_Click((object) null, (EventArgs) null);
    }

    private void tsbPartName_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblPartName_Click((object) null, (EventArgs) null);
    }

    private void tsbPartAdvance_Click(object sender, EventArgs e)
    {
      this.objFrmTasks.lblAdvance_Click((object) null, (EventArgs) null);
    }

    private void LoadUserSettings()
    {
      this.Location = Settings.Default.frmSearchLocation;
      this.Size = Settings.Default.frmSearchSize;
      if (Settings.Default.frmSearchState == FormWindowState.Minimized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = Settings.Default.frmSearchState;
      this.frmParent.checkConfigFileExist();
      ToolStripManager.LoadSettings((Form) this);
    }

    private void SaveUserSettings()
    {
      Settings.Default.frmSearchLocation = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Location : this.Location;
      Settings.Default.frmSearchSize = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Size : this.Size;
      Settings.Default.frmSearchState = this.WindowState;
      ToolStripManager.SaveSettings((Form) this);
      this.frmParent.CopyConfigurationFile();
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Search", "SEARCH", ResourceType.TITLE);
      this.tsbPageName.Text = this.GetResource("Page Name Search", "PAGE_NAME_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbText.Text = this.GetResource("Text Search", "TEXT_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbPartName.Text = this.GetResource("Part Name Search", "PART_NAME_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbPartNumber.Text = this.GetResource("Part Number Search", "PART_NUMBER_SEARCH", ResourceType.TOOLSTRIP);
      this.tsbPartAdvance.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.TOOLSTRIP);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='SEARCH']";
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

    public void OnOffFeatures()
    {
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        bool flag = false;
        ArrayList arrayList = new ArrayList();
        if (iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PLIST_SEARCH").Count > 0 && Program.objAppFeatures.bPartAdvanceSearch)
          flag = true;
        this.tsbPageName.Visible = Program.objAppFeatures.bPageNameSearch;
        this.tsbText.Visible = Program.objAppFeatures.bTextSearch;
        this.tsbPartName.Visible = Program.objAppFeatures.bPartNameSearch;
        this.tsbPartNumber.Visible = Program.objAppFeatures.bPartNumberSearch;
        this.tsbPartAdvance.Visible = flag;
        if (!Program.objAppFeatures.bPageNameSearch && !Program.objAppFeatures.bTextSearch)
          this.tsPage.Visible = false;
        else
          this.tsPage.Visible = true;
        if (!Program.objAppFeatures.bPartNameSearch && !Program.objAppFeatures.bPartNumberSearch && !flag)
          this.tsPart.Visible = false;
        else
          this.tsPart.Visible = true;
      }
      catch
      {
      }
    }

    private void frmSearch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
    }

    public string[] ConvertStringWidth(string sInput)
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string fullWidth = frmSearch.ToFullWidth(sInput);
        string halfWidth = frmSearch.ToHalfWidth(sInput);
        if (fullWidth == string.Empty && halfWidth == string.Empty)
          return new string[1]{ sInput };
        return new string[2]{ fullWidth, halfWidth };
      }
      catch (Exception ex)
      {
        return new string[1]{ sInput };
      }
    }

    public static string ToHalfWidth(string fullWidth)
    {
      StringBuilder lpDestStr = new StringBuilder(256);
      frmSearch.LCMapString(2048U, 4194304U, fullWidth, -1, lpDestStr, lpDestStr.Capacity);
      return lpDestStr.ToString();
    }

    public static string ToFullWidth(string halfWidth)
    {
      StringBuilder lpDestStr = new StringBuilder(256);
      frmSearch.LCMapString(2048U, 8388608U, halfWidth, -1, lpDestStr, lpDestStr.Capacity);
      return lpDestStr.ToString();
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    private static extern int LCMapString(uint Locale, uint dwMapFlags, string lpSrcStr, int cchSrc, StringBuilder lpDestStr, int cchDest);
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConfigViewerFont
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace GSPcLocalViewer
{
  public class frmConfigViewerFont : Form
  {
    private IContainer components;
    private Panel pnlForm;
    private Label lblFontAndColor;
    private Panel pnlControl;
    private Button btnOK;
    private Button btnCancel;
    private BackgroundWorker bgWorker;
    private PictureBox picLoading;
    private Panel pnlFont;
    private ComboBox cmbSize;
    private Label lblSize;
    private Label lblFont;
    private ComboBox cmbFont;
    private Panel pnlPrintFont;
    private ComboBox cmbPrintFontSize;
    private ComboBox cmbPrintFont;
    private Label lblPrintFont;
    private Label lblPrintSize;
    private frmConfig frmParent;
    private ArrayList colorArray;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlForm = new Panel();
      this.pnlPrintFont = new Panel();
      this.lblPrintFont = new Label();
      this.cmbPrintFontSize = new ComboBox();
      this.cmbPrintFont = new ComboBox();
      this.pnlControl = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.pnlFont = new Panel();
      this.lblSize = new Label();
      this.lblFont = new Label();
      this.cmbFont = new ComboBox();
      this.cmbSize = new ComboBox();
      this.lblFontAndColor = new Label();
      this.picLoading = new PictureBox();
      this.bgWorker = new BackgroundWorker();
      this.lblPrintSize = new Label();
      this.pnlForm.SuspendLayout();
      this.pnlPrintFont.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.pnlFont.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlPrintFont);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Controls.Add((Control) this.pnlFont);
      this.pnlForm.Controls.Add((Control) this.lblFontAndColor);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(450, 450);
      this.pnlForm.TabIndex = 17;
      this.pnlPrintFont.AutoScroll = true;
      this.pnlPrintFont.Controls.Add((Control) this.lblPrintSize);
      this.pnlPrintFont.Controls.Add((Control) this.lblPrintFont);
      this.pnlPrintFont.Controls.Add((Control) this.cmbPrintFontSize);
      this.pnlPrintFont.Controls.Add((Control) this.cmbPrintFont);
      this.pnlPrintFont.Dock = DockStyle.Fill;
      this.pnlPrintFont.Location = new Point(0, 215);
      this.pnlPrintFont.Name = "pnlPrintFont";
      this.pnlPrintFont.Size = new Size(448, 202);
      this.pnlPrintFont.TabIndex = 23;
      this.lblPrintFont.AutoSize = true;
      this.lblPrintFont.Location = new Point(15, 8);
      this.lblPrintFont.Name = "lblPrintFont";
      this.lblPrintFont.Size = new Size(54, 13);
      this.lblPrintFont.TabIndex = 5;
      this.lblPrintFont.Text = "Print Font";
      this.cmbPrintFontSize.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPrintFontSize.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbPrintFontSize.FormattingEnabled = true;
      this.cmbPrintFontSize.IntegralHeight = false;
      this.cmbPrintFontSize.Location = new Point(295, 24);
      this.cmbPrintFontSize.Name = "cmbPrintFontSize";
      this.cmbPrintFontSize.Size = new Size(138, 146);
      this.cmbPrintFontSize.TabIndex = 1;
      this.cmbPrintFontSize.DrawItem += new DrawItemEventHandler(this.cmbPrintFontSize_DrawItem);
      this.cmbPrintFont.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPrintFont.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbPrintFont.IntegralHeight = false;
      this.cmbPrintFont.Location = new Point(15, 24);
      this.cmbPrintFont.Name = "cmbPrintFont";
      this.cmbPrintFont.Size = new Size(264, 146);
      this.cmbPrintFont.Sorted = true;
      this.cmbPrintFont.TabIndex = 0;
      this.cmbPrintFont.DrawItem += new DrawItemEventHandler(this.cmbPrintFont_DrawItem);
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 417);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 15, 4);
      this.pnlControl.Size = new Size(448, 31);
      this.pnlControl.TabIndex = 18;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(283, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(358, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 0;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlFont.AutoScroll = true;
      this.pnlFont.Controls.Add((Control) this.lblSize);
      this.pnlFont.Controls.Add((Control) this.lblFont);
      this.pnlFont.Controls.Add((Control) this.cmbFont);
      this.pnlFont.Controls.Add((Control) this.cmbSize);
      this.pnlFont.Dock = DockStyle.Top;
      this.pnlFont.Location = new Point(0, 27);
      this.pnlFont.Name = "pnlFont";
      this.pnlFont.Size = new Size(448, 188);
      this.pnlFont.TabIndex = 19;
      this.lblSize.AutoSize = true;
      this.lblSize.Location = new Point(297, 10);
      this.lblSize.Name = "lblSize";
      this.lblSize.Size = new Size(26, 13);
      this.lblSize.TabIndex = 2;
      this.lblSize.Text = "Size";
      this.lblFont.AutoSize = true;
      this.lblFont.Location = new Point(12, 10);
      this.lblFont.Name = "lblFont";
      this.lblFont.Size = new Size(66, 13);
      this.lblFont.TabIndex = 2;
      this.lblFont.Text = "Display Font";
      this.cmbFont.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbFont.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbFont.IntegralHeight = false;
      this.cmbFont.ItemHeight = 16;
      this.cmbFont.Location = new Point(15, 26);
      this.cmbFont.Name = "cmbFont";
      this.cmbFont.Size = new Size(264, 146);
      this.cmbFont.Sorted = true;
      this.cmbFont.TabIndex = 3;
      this.cmbFont.DrawItem += new DrawItemEventHandler(this.cmbFont_DrawItem);
      this.cmbSize.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSize.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbSize.FormattingEnabled = true;
      this.cmbSize.IntegralHeight = false;
      this.cmbSize.ItemHeight = 16;
      this.cmbSize.Items.AddRange(new object[3]
      {
        (object) "Small",
        (object) "Medium",
        (object) "Large"
      });
      this.cmbSize.Location = new Point(295, 26);
      this.cmbSize.Name = "cmbSize";
      this.cmbSize.Size = new Size(138, 146);
      this.cmbSize.TabIndex = 4;
      this.cmbSize.DrawItem += new DrawItemEventHandler(this.cmbSize_DrawItem);
      this.lblFontAndColor.BackColor = Color.White;
      this.lblFontAndColor.Dock = DockStyle.Top;
      this.lblFontAndColor.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblFontAndColor.ForeColor = Color.Black;
      this.lblFontAndColor.Location = new Point(0, 0);
      this.lblFontAndColor.Name = "lblFontAndColor";
      this.lblFontAndColor.Padding = new Padding(3, 7, 0, 0);
      this.lblFontAndColor.Size = new Size(448, 27);
      this.lblFontAndColor.TabIndex = 16;
      this.lblFontAndColor.Text = "Font";
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(2, 1);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(32, 32);
      this.picLoading.TabIndex = 22;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.lblPrintSize.AutoSize = true;
      this.lblPrintSize.Location = new Point(297, 8);
      this.lblPrintSize.Name = "lblPrintSize";
      this.lblPrintSize.Size = new Size(26, 13);
      this.lblPrintSize.TabIndex = 5;
      this.lblPrintSize.Text = "Size";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(450, 450);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmConfigViewerFont);
      this.Load += new EventHandler(this.frmConfigTemp_Load);
      this.pnlForm.ResumeLayout(false);
      this.pnlPrintFont.ResumeLayout(false);
      this.pnlPrintFont.PerformLayout();
      this.pnlControl.ResumeLayout(false);
      this.pnlFont.ResumeLayout(false);
      this.pnlFont.PerformLayout();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    public Font GetFont
    {
      get
      {
        float emSize;
        try
        {
          emSize = Settings.Default.appFont.Size;
          switch (this.cmbSize.SelectedIndex)
          {
            case 0:
              emSize = 6f;
              break;
            case 1:
              emSize = 7f;
              break;
            case 2:
              emSize = 8f;
              break;
            case 3:
              emSize = 9f;
              break;
            case 4:
              emSize = 10f;
              break;
          }
        }
        catch
        {
          emSize = 8f;
        }
        if (this.cmbFont.SelectedItem != null)
          return new Font(this.cmbFont.SelectedItem.ToString(), emSize);
        return new Font(Settings.Default.appFont.Name, emSize);
      }
    }

    public Font GetPrintFont
    {
      get
      {
        float emSize;
        try
        {
          emSize = Settings.Default.printFont.Size;
          switch (this.cmbPrintFontSize.SelectedIndex)
          {
            case 0:
              emSize = 6f;
              break;
            case 1:
              emSize = 7f;
              break;
            case 2:
              emSize = 8f;
              break;
            case 3:
              emSize = 9f;
              break;
            case 4:
              emSize = 10f;
              break;
          }
        }
        catch
        {
          emSize = 8f;
        }
        if (this.cmbPrintFont.SelectedItem != null)
          return new Font(this.cmbPrintFont.SelectedItem.ToString(), emSize);
        return new Font(Settings.Default.printFont.Name, emSize);
      }
    }

    public frmConfigViewerFont(frmConfig frm)
    {
      this.InitializeComponent();
      this.colorArray = new ArrayList();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.cmbFont.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSize.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPrintFont.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPrintFontSize.DrawMode = DrawMode.OwnerDrawFixed;
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmConfigTemp_Load(object sender, EventArgs e)
    {
      this.ShowLoading(this.pnlForm);
      this.bgWorker.RunWorkerAsync();
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.LoadComboBoxes();
      Thread.Sleep(500);
      this.LoadPrintComboBoxes();
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.HideLoading(this.pnlForm);
    }

    private void cmbFont_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index <= -1)
        return;
      Font font;
      try
      {
        font = new Font(new FontFamily(this.cmbFont.Items[e.Index].ToString()), this.cmbFont.Font.Size);
      }
      catch (ArgumentException ex)
      {
        font = new Font(new FontFamily("Arial"), this.cmbFont.Font.Size);
      }
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        SolidBrush solidBrush1 = new SolidBrush(Settings.Default.appHighlightBackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(Settings.Default.appHighlightForeColor);
        e.Graphics.DrawString(this.cmbFont.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
      else
      {
        SolidBrush solidBrush1 = new SolidBrush(this.cmbFont.BackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(this.cmbFont.ForeColor);
        e.Graphics.DrawString(this.cmbFont.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
    }

    private void cmbSize_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index <= -1)
        return;
      Font font = new Font(this.cmbSize.Font.FontFamily, float.Parse(this.cmbSize.Items[e.Index].ToString(), (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat));
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        SolidBrush solidBrush1 = new SolidBrush(Settings.Default.appHighlightBackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(Settings.Default.appHighlightForeColor);
        e.Graphics.DrawString(this.cmbSize.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
      else
      {
        SolidBrush solidBrush1 = new SolidBrush(this.cmbSize.BackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(this.cmbSize.ForeColor);
        e.Graphics.DrawString(this.cmbSize.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
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

    private void cmbPrintFont_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index <= -1)
        return;
      Font font;
      try
      {
        font = new Font(new FontFamily(this.cmbPrintFont.Items[e.Index].ToString()), this.cmbPrintFont.Font.Size);
      }
      catch (ArgumentException ex)
      {
        font = new Font(new FontFamily("Arial"), this.cmbPrintFont.Font.Size);
      }
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        SolidBrush solidBrush1 = new SolidBrush(Settings.Default.appHighlightBackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(Settings.Default.appHighlightForeColor);
        e.Graphics.DrawString(this.cmbPrintFont.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
      else
      {
        SolidBrush solidBrush1 = new SolidBrush(this.cmbPrintFont.BackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(this.cmbPrintFont.ForeColor);
        e.Graphics.DrawString(this.cmbPrintFont.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
    }

    private void cmbPrintFontSize_DrawItem(object sender, DrawItemEventArgs e)
    {
      if (e.Index <= -1)
        return;
      Font font = new Font(this.cmbSize.Font.FontFamily, float.Parse(this.cmbPrintFontSize.Items[e.Index].ToString(), (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat));
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        SolidBrush solidBrush1 = new SolidBrush(Settings.Default.appHighlightBackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(Settings.Default.appHighlightForeColor);
        e.Graphics.DrawString(this.cmbPrintFontSize.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
      else
      {
        SolidBrush solidBrush1 = new SolidBrush(this.cmbPrintFontSize.BackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(this.cmbPrintFontSize.ForeColor);
        e.Graphics.DrawString(this.cmbPrintFontSize.Items[e.Index].ToString(), font, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblFontAndColor.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private void LoadComboBoxes()
    {
      if (this.pnlForm.InvokeRequired)
      {
        this.pnlForm.Invoke((Delegate) new frmConfigViewerFont.LoadComboBoxesDelegate(this.LoadComboBoxes));
      }
      else
      {
        InstalledFontCollection installedFontCollection = new InstalledFontCollection();
        this.cmbFont.Items.Clear();
        for (int index = 0; index < installedFontCollection.Families.Length; ++index)
        {
          if (installedFontCollection.Families[index].IsStyleAvailable(FontStyle.Regular))
            this.cmbFont.Items.Add((object) installedFontCollection.Families[index].Name);
        }
        this.cmbFont.SelectedItem = (object) this.Font.FontFamily.Name;
        this.cmbSize.Items.Clear();
        this.cmbSize.Items.Add((object) "6");
        this.cmbSize.Items.Add((object) "7");
        this.cmbSize.Items.Add((object) "8");
        this.cmbSize.Items.Add((object) "9");
        this.cmbSize.Items.Add((object) "10");
        switch (this.Font.Size.ToString())
        {
          case "6":
            this.cmbSize.SelectedItem = this.cmbSize.Items[0];
            break;
          case "7":
            this.cmbSize.SelectedItem = this.cmbSize.Items[1];
            break;
          case "8":
            this.cmbSize.SelectedItem = this.cmbSize.Items[2];
            break;
          case "9":
            this.cmbSize.SelectedItem = this.cmbSize.Items[3];
            break;
          case "10":
            this.cmbSize.SelectedItem = this.cmbSize.Items[4];
            break;
          default:
            this.cmbSize.SelectedItem = this.cmbSize.Items[0];
            break;
        }
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading && control != this.lblFontAndColor)
            control.Visible = false;
        }
        this.picLoading.Left = parentPanel.Left + parentPanel.Width / 2 - this.picLoading.Width / 2;
        this.picLoading.Top = parentPanel.Top + parentPanel.Height / 2 - this.picLoading.Height / 2;
        this.picLoading.Parent = (Control) parentPanel;
        this.picLoading.BringToFront();
        this.picLoading.Show();
      }
      catch
      {
      }
    }

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading)
            control.Visible = true;
        }
        this.picLoading.Hide();
      }
      catch
      {
      }
    }

    private void LoadResources()
    {
      this.lblFontAndColor.Text = this.GetResource("Font", "FONT", ResourceType.LABEL) + " : " + this.GetResource("General", "GENERAL", ResourceType.LABEL);
      this.lblFont.Text = this.GetResource("Font", "FONT:", ResourceType.LABEL);
      this.lblSize.Text = this.GetResource("Size", "SIZE", ResourceType.LABEL);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
      this.lblPrintFont.Text = this.GetResource("Print Font", "PRINT_FONT", ResourceType.LABEL);
      this.lblPrintSize.Text = this.GetResource("Print Font Size", "PRINT_FONT_SIZE", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONFIGURATION']" + "/Screen[@Name='FONT']";
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

    private void LoadPrintComboBoxes()
    {
      if (this.pnlForm.InvokeRequired)
      {
        this.pnlForm.Invoke((Delegate) new frmConfigViewerFont.LoadPrintComboBoxesDelegate(this.LoadPrintComboBoxes));
      }
      else
      {
        InstalledFontCollection installedFontCollection = new InstalledFontCollection();
        this.cmbPrintFont.Items.Clear();
        for (int index = 0; index < installedFontCollection.Families.Length; ++index)
        {
          if (installedFontCollection.Families[index].IsStyleAvailable(FontStyle.Regular))
            this.cmbPrintFont.Items.Add((object) installedFontCollection.Families[index].Name);
        }
        this.cmbPrintFont.SelectedItem = (object) Settings.Default.printFont.FontFamily.Name;
        this.cmbPrintFontSize.Items.Clear();
        this.cmbPrintFontSize.Items.Add((object) "6");
        this.cmbPrintFontSize.Items.Add((object) "7");
        this.cmbPrintFontSize.Items.Add((object) "8");
        this.cmbPrintFontSize.Items.Add((object) "9");
        this.cmbPrintFontSize.Items.Add((object) "10");
        switch (Settings.Default.printFont.Size.ToString())
        {
          case "6":
            this.cmbPrintFontSize.SelectedItem = this.cmbPrintFontSize.Items[0];
            break;
          case "7":
            this.cmbPrintFontSize.SelectedItem = this.cmbPrintFontSize.Items[1];
            break;
          case "8":
            this.cmbPrintFontSize.SelectedItem = this.cmbPrintFontSize.Items[2];
            break;
          case "9":
            this.cmbPrintFontSize.SelectedItem = this.cmbPrintFontSize.Items[3];
            break;
          case "10":
            this.cmbPrintFontSize.SelectedItem = this.cmbPrintFontSize.Items[4];
            break;
          default:
            this.cmbPrintFontSize.SelectedItem = this.cmbPrintFontSize.Items[0];
            break;
        }
      }
    }

    private delegate void LoadComboBoxesDelegate();

    private delegate void LoadPrintComboBoxesDelegate();
  }
}

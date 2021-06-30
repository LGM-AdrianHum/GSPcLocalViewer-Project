// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConfigViewerColor
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
  public class frmConfigViewerColor : Form
  {
    private IContainer components;
    private Panel pnlForm;
    private Label lblColorGeneral;
    private Panel pnlControl;
    private Button btnOK;
    private Button btnCancel;
    private BackgroundWorker bgWorker;
    private PictureBox picLoading;
    private Panel pnlColor;
    private ComboBox cmbForeColorPartsList;
    private ComboBox cmbBackColorPartsList;
    private Label label1;
    private Label lblBackColor;
    private Panel pnlFont;
    private Label lblColorPartsList;
    private ComboBox cmbForeColorGeneral;
    private ComboBox cmbBackColorGeneral;
    private Label label2;
    private Label lblBackColorGeneral;
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
      this.pnlColor = new Panel();
      this.cmbForeColorPartsList = new ComboBox();
      this.cmbBackColorPartsList = new ComboBox();
      this.label1 = new Label();
      this.lblBackColor = new Label();
      this.lblColorPartsList = new Label();
      this.pnlControl = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.pnlFont = new Panel();
      this.cmbForeColorGeneral = new ComboBox();
      this.cmbBackColorGeneral = new ComboBox();
      this.label2 = new Label();
      this.lblBackColorGeneral = new Label();
      this.lblColorGeneral = new Label();
      this.picLoading = new PictureBox();
      this.bgWorker = new BackgroundWorker();
      this.pnlForm.SuspendLayout();
      this.pnlColor.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.pnlFont.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlColor);
      this.pnlForm.Controls.Add((Control) this.lblColorPartsList);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Controls.Add((Control) this.pnlFont);
      this.pnlForm.Controls.Add((Control) this.lblColorGeneral);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(450, 475);
      this.pnlForm.TabIndex = 17;
      this.pnlColor.AutoScroll = true;
      this.pnlColor.Controls.Add((Control) this.cmbForeColorPartsList);
      this.pnlColor.Controls.Add((Control) this.cmbBackColorPartsList);
      this.pnlColor.Controls.Add((Control) this.label1);
      this.pnlColor.Controls.Add((Control) this.lblBackColor);
      this.pnlColor.Dock = DockStyle.Top;
      this.pnlColor.Location = new Point(0, 242);
      this.pnlColor.Name = "pnlColor";
      this.pnlColor.Size = new Size(448, 196);
      this.pnlColor.TabIndex = 20;
      this.cmbForeColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbForeColorPartsList.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbForeColorPartsList.IntegralHeight = false;
      this.cmbForeColorPartsList.ItemHeight = 16;
      this.cmbForeColorPartsList.Location = new Point(231, 29);
      this.cmbForeColorPartsList.Name = "cmbForeColor";
      this.cmbForeColorPartsList.Size = new Size(202, 152);
      this.cmbForeColorPartsList.TabIndex = 5;
      this.cmbForeColorPartsList.DrawItem += new DrawItemEventHandler(this.cmbForeColorPartsList_DrawItem);
      this.cmbBackColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbBackColorPartsList.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbBackColorPartsList.IntegralHeight = false;
      this.cmbBackColorPartsList.ItemHeight = 16;
      this.cmbBackColorPartsList.Location = new Point(15, 29);
      this.cmbBackColorPartsList.Name = "cmbBackColor";
      this.cmbBackColorPartsList.Size = new Size(200, 152);
      this.cmbBackColorPartsList.TabIndex = 4;
      this.cmbBackColorPartsList.DrawItem += new DrawItemEventHandler(this.cmbBackColorPartsList_DrawItem);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(228, 13);
      this.label1.Name = "label1";
      this.label1.Size = new Size(97, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Highlight fore color";
      this.lblBackColor.AutoSize = true;
      this.lblBackColor.Location = new Point(12, 13);
      this.lblBackColor.Name = "lblBackColor";
      this.lblBackColor.Size = new Size(99, 13);
      this.lblBackColor.TabIndex = 2;
      this.lblBackColor.Text = "Highlight back color";
      this.lblColorPartsList.BackColor = Color.White;
      this.lblColorPartsList.Dock = DockStyle.Top;
      this.lblColorPartsList.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblColorPartsList.ForeColor = Color.Black;
      this.lblColorPartsList.Location = new Point(0, 215);
      this.lblColorPartsList.Name = "lblColorPartsList";
      this.lblColorPartsList.Padding = new Padding(3, 7, 0, 0);
      this.lblColorPartsList.Size = new Size(448, 27);
      this.lblColorPartsList.TabIndex = 23;
      this.lblColorPartsList.Text = "Color : Parts List Information";
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 442);
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
      this.pnlFont.Controls.Add((Control) this.cmbForeColorGeneral);
      this.pnlFont.Controls.Add((Control) this.cmbBackColorGeneral);
      this.pnlFont.Controls.Add((Control) this.label2);
      this.pnlFont.Controls.Add((Control) this.lblBackColorGeneral);
      this.pnlFont.Dock = DockStyle.Top;
      this.pnlFont.Location = new Point(0, 27);
      this.pnlFont.Name = "pnlFont";
      this.pnlFont.Size = new Size(448, 188);
      this.pnlFont.TabIndex = 19;
      this.cmbForeColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbForeColorGeneral.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbForeColorGeneral.IntegralHeight = false;
      this.cmbForeColorGeneral.ItemHeight = 16;
      this.cmbForeColorGeneral.Location = new Point(233, 26);
      this.cmbForeColorGeneral.Name = "cmbForeColorGeneral";
      this.cmbForeColorGeneral.Size = new Size(202, 152);
      this.cmbForeColorGeneral.TabIndex = 9;
      this.cmbForeColorGeneral.DrawItem += new DrawItemEventHandler(this.cmbForeColorGeneral_DrawItem);
      this.cmbBackColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbBackColorGeneral.DropDownStyle = ComboBoxStyle.Simple;
      this.cmbBackColorGeneral.IntegralHeight = false;
      this.cmbBackColorGeneral.ItemHeight = 16;
      this.cmbBackColorGeneral.Location = new Point(17, 26);
      this.cmbBackColorGeneral.Name = "cmbBackColorGeneral";
      this.cmbBackColorGeneral.Size = new Size(200, 152);
      this.cmbBackColorGeneral.TabIndex = 8;
      this.cmbBackColorGeneral.DrawItem += new DrawItemEventHandler(this.cmbBackColorGeneral_DrawItem);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(230, 10);
      this.label2.Name = "label2";
      this.label2.Size = new Size(97, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Highlight fore color";
      this.lblBackColorGeneral.AutoSize = true;
      this.lblBackColorGeneral.Location = new Point(14, 10);
      this.lblBackColorGeneral.Name = "lblBackColorGeneral";
      this.lblBackColorGeneral.Size = new Size(99, 13);
      this.lblBackColorGeneral.TabIndex = 7;
      this.lblBackColorGeneral.Text = "Highlight back color";
      this.lblColorGeneral.BackColor = Color.White;
      this.lblColorGeneral.Dock = DockStyle.Top;
      this.lblColorGeneral.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblColorGeneral.ForeColor = Color.Black;
      this.lblColorGeneral.Location = new Point(0, 0);
      this.lblColorGeneral.Name = "lblColorGeneral";
      this.lblColorGeneral.Padding = new Padding(3, 7, 0, 0);
      this.lblColorGeneral.Size = new Size(448, 27);
      this.lblColorGeneral.TabIndex = 16;
      this.lblColorGeneral.Text = "Color : General";
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
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(450, 475);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmConfigViewerColor);
      this.Load += new EventHandler(this.frmConfigTemp_Load);
      this.pnlForm.ResumeLayout(false);
      this.pnlColor.ResumeLayout(false);
      this.pnlColor.PerformLayout();
      this.pnlControl.ResumeLayout(false);
      this.pnlFont.ResumeLayout(false);
      this.pnlFont.PerformLayout();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    public frmConfigViewerColor(frmConfig frm)
    {
      this.InitializeComponent();
      this.colorArray = new ArrayList();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.cmbBackColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbForeColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbBackColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbForeColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
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
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.HideLoading(this.pnlForm);
    }

    private void cmbBackColorPartsList_DrawItem(object sender, DrawItemEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle rect1 = e.Bounds;
      if (e.Index < 0)
        return;
      Rectangle rect2 = new Rectangle(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Height - 4);
      rect2.Width = 50;
      rect1 = new Rectangle(rect2.Right + 4, rect1.Y, rect1.Width, rect1.Height);
      SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor) this.colorArray[e.Index]));
      graphics.FillRectangle((Brush) solidBrush, rect2);
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Near;
      Console.WriteLine(e.State.ToString());
      e.Graphics.DrawRectangle(new Pen((Brush) new SolidBrush(Color.Black), 1f), rect2);
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(Settings.Default.PartsListInfoBackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbBackColorPartsList.Font, (Brush) new SolidBrush(Settings.Default.PartsListInfoForeColor), (RectangleF) rect1, format);
      }
      else
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(this.cmbBackColorPartsList.BackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbBackColorPartsList.Font, (Brush) new SolidBrush(this.cmbBackColorPartsList.ForeColor), (RectangleF) rect1, format);
      }
    }

    private void cmbForeColorPartsList_DrawItem(object sender, DrawItemEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle rect1 = e.Bounds;
      if (e.Index < 0)
        return;
      Rectangle rect2 = new Rectangle(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Height - 4);
      rect2.Width = 50;
      rect1 = new Rectangle(rect2.Right + 4, rect1.Y, rect1.Width, rect1.Height);
      SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor) this.colorArray[e.Index]));
      graphics.FillRectangle((Brush) solidBrush, rect2);
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Near;
      Console.WriteLine(e.State.ToString());
      e.Graphics.DrawRectangle(new Pen((Brush) new SolidBrush(Color.Black), 1f), rect2);
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(Settings.Default.PartsListInfoBackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbForeColorPartsList.Font, (Brush) new SolidBrush(Settings.Default.PartsListInfoForeColor), (RectangleF) rect1, format);
      }
      else
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(this.cmbForeColorPartsList.BackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbForeColorPartsList.Font, (Brush) new SolidBrush(this.cmbForeColorPartsList.ForeColor), (RectangleF) rect1, format);
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

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblColorGeneral.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.lblColorPartsList.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private void LoadComboBoxes()
    {
      if (this.pnlForm.InvokeRequired)
      {
        this.pnlForm.Invoke((Delegate) new frmConfigViewerColor.LoadComboBoxesDelegate(this.LoadComboBoxes));
      }
      else
      {
        InstalledFontCollection installedFontCollection = new InstalledFontCollection();
        for (KnownColor knownColor = KnownColor.ActiveBorder; knownColor < KnownColor.YellowGreen; ++knownColor)
        {
          this.cmbBackColorPartsList.Items.Add((object) knownColor.ToString());
          this.cmbForeColorPartsList.Items.Add((object) knownColor.ToString());
          this.cmbBackColorGeneral.Items.Add((object) knownColor.ToString());
          this.cmbForeColorGeneral.Items.Add((object) knownColor.ToString());
          this.colorArray.Add((object) knownColor);
        }
        this.cmbBackColorGeneral.SelectedItem = (object) Settings.Default.appHighlightBackColor.Name;
        this.cmbForeColorGeneral.SelectedItem = (object) Settings.Default.appHighlightForeColor.Name;
        this.cmbBackColorPartsList.SelectedItem = (object) Settings.Default.PartsListInfoBackColor.Name;
        this.cmbForeColorPartsList.SelectedItem = (object) Settings.Default.PartsListInfoForeColor.Name;
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading && control != this.lblColorGeneral)
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

    public Color GetHighlightBackColorPartsList
    {
      get
      {
        if (this.cmbBackColorPartsList.SelectedItem != null)
          return Color.FromKnownColor((KnownColor) this.colorArray[this.cmbBackColorPartsList.SelectedIndex]);
        return Settings.Default.PartsListInfoBackColor;
      }
    }

    public Color GetHighlightForeColorPartsList
    {
      get
      {
        if (this.cmbForeColorPartsList.SelectedItem != null)
          return Color.FromKnownColor((KnownColor) this.colorArray[this.cmbForeColorPartsList.SelectedIndex]);
        return Settings.Default.PartsListInfoForeColor;
      }
    }

    public Color GetHighlightBackColorGeneral
    {
      get
      {
        if (this.cmbBackColorGeneral.SelectedItem != null)
          return Color.FromKnownColor((KnownColor) this.colorArray[this.cmbBackColorGeneral.SelectedIndex]);
        return Settings.Default.appHighlightBackColor;
      }
    }

    public Color GetHighlightForeColorGeneral
    {
      get
      {
        if (this.cmbForeColorGeneral.SelectedItem != null)
          return Color.FromKnownColor((KnownColor) this.colorArray[this.cmbForeColorGeneral.SelectedIndex]);
        return Settings.Default.appHighlightForeColor;
      }
    }

    private void LoadResources()
    {
      this.lblColorGeneral.Text = this.GetResource("Color", "COLOR", ResourceType.LABEL) + " : " + this.GetResource("General", "GENERAL", ResourceType.LABEL);
      this.lblColorPartsList.Text = this.GetResource("Color", "COLOR", ResourceType.LABEL) + " : " + this.GetResource("Partlist Information", "PARTLIST_INFORMATION", ResourceType.LABEL);
      this.lblBackColor.Text = this.GetResource("Highlight back color", "BACKCOLOR_PART", ResourceType.LABEL);
      this.lblBackColorGeneral.Text = this.GetResource("Highlight back color", "BACKCOLOR_GENERAL", ResourceType.LABEL);
      this.label2.Text = this.GetResource("Highlight fore color", "FORECOLOR_GENERAL", ResourceType.LABEL);
      this.label1.Text = this.GetResource("Highlight fore color", "FORECOLOR_PART", ResourceType.LABEL);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONFIGURATION']" + "/Screen[@Name='COLOR']";
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

    private void cmbBackColorGeneral_DrawItem(object sender, DrawItemEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle rect1 = e.Bounds;
      if (e.Index < 0)
        return;
      Rectangle rect2 = new Rectangle(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Height - 4);
      rect2.Width = 50;
      rect1 = new Rectangle(rect2.Right + 4, rect1.Y, rect1.Width, rect1.Height);
      SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor) this.colorArray[e.Index]));
      graphics.FillRectangle((Brush) solidBrush, rect2);
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Near;
      Console.WriteLine(e.State.ToString());
      e.Graphics.DrawRectangle(new Pen((Brush) new SolidBrush(Color.Black), 1f), rect2);
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(Settings.Default.appHighlightBackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbBackColorGeneral.Font, (Brush) new SolidBrush(Settings.Default.appHighlightForeColor), (RectangleF) rect1, format);
      }
      else
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(this.cmbBackColorGeneral.BackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbBackColorGeneral.Font, (Brush) new SolidBrush(this.cmbBackColorGeneral.ForeColor), (RectangleF) rect1, format);
      }
    }

    private void cmbForeColorGeneral_DrawItem(object sender, DrawItemEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle rect1 = e.Bounds;
      if (e.Index < 0)
        return;
      Rectangle rect2 = new Rectangle(rect1.X + 2, rect1.Y + 2, rect1.Width - 4, rect1.Height - 4);
      rect2.Width = 50;
      rect1 = new Rectangle(rect2.Right + 4, rect1.Y, rect1.Width, rect1.Height);
      SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor) this.colorArray[e.Index]));
      graphics.FillRectangle((Brush) solidBrush, rect2);
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Near;
      Console.WriteLine(e.State.ToString());
      e.Graphics.DrawRectangle(new Pen((Brush) new SolidBrush(Color.Black), 1f), rect2);
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(Settings.Default.appHighlightBackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbForeColorGeneral.Font, (Brush) new SolidBrush(Settings.Default.appHighlightForeColor), (RectangleF) rect1, format);
      }
      else
      {
        e.Graphics.FillRectangle((Brush) new SolidBrush(this.cmbForeColorGeneral.BackColor), rect1);
        e.Graphics.DrawString(solidBrush.Color.Name, this.cmbForeColorGeneral.Font, (Brush) new SolidBrush(this.cmbForeColorGeneral.ForeColor), (RectangleF) rect1, format);
      }
    }

    private delegate void LoadComboBoxesDelegate();
  }
}

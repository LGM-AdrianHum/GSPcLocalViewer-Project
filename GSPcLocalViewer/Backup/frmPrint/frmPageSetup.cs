// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmPrint.frmPageSetup
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using AxDjVuCtrlLib;
using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmPrint
{
  public class frmPageSetup : Form
  {
    private const string dllDjVuDecoder = "DjVuDecoder.dll";
    private PaperSize pageSize;
    private int iSplittingOption;
    private frmViewer frmParent;
    private IContainer components;
    private AxDjVuCtrl objDjVuCtl;
    private Panel pnlControl;
    private Button btnCancel;
    private Panel pnlSplitPattern;
    private Panel pnlLblSplitPattern;
    private Label lblSplitPatternLine;
    private Label lblSplitPattern;
    private Label lblPortraitPicture;
    public Panel panelLandscape;
    private Label lblLandscapePicture;
    public Panel panelPortrait;
    private Panel pnlForm;

    [DllImport("DjVuDecoder.dll")]
    internal static extern bool DjVuToJPEG(string DjVuPath, string ExportedPath);

    public frmPageSetup(frmViewer objFrmViewer, int SplittingOption)
    {
      this.frmParent = objFrmViewer;
      this.pageSize = new PaperSize();
      this.pageSize.PaperName = "A4";
      this.pageSize.Width = 827;
      this.pageSize.Height = 1169;
      this.iSplittingOption = SplittingOption;
      this.InitializeComponent();
      this.LoadResources();
      this.objDjVuCtl.CurrentLanguage = this.frmParent.AppCurrentLanguage;
    }

    private void SetPageImageResolutions()
    {
      int num1 = 5;
      int num2 = 1;
      if (this.iSplittingOption == 0)
        num2 = 1;
      else if (this.iSplittingOption == 1)
        num2 = 2;
      else if (this.iSplittingOption == 2)
        num2 = 4;
      else if (this.iSplittingOption == 3)
        num2 = 8;
      Decimal num3 = Math.Round(Decimal.Divide((Decimal) this.pageSize.Width, (Decimal) this.pageSize.Height), 2);
      SolidBrush solidBrush1 = new SolidBrush(Color.Black);
      Pen pen = new Pen((Brush) solidBrush1);
      Graphics graphics1 = this.panelPortrait.CreateGraphics();
      Graphics graphics2 = this.panelLandscape.CreateGraphics();
      this.panelPortrait.Refresh();
      this.panelLandscape.Refresh();
      if (this.iSplittingOption == 0)
      {
        int width = int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) this.panelPortrait.Height, 2)).ToString());
        int x = int.Parse(((this.panelPortrait.Width - width) / 2).ToString());
        graphics1.DrawRectangle(pen, x, 0, width, this.panelPortrait.Height - 1);
        ControlPaint.DrawBorder3D(graphics1, x, 0, width, this.panelPortrait.Height - 1, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
        ControlPaint.DrawBorder3D(graphics1, x, 0, width, this.panelPortrait.Height - 1, Border3DStyle.RaisedOuter, Border3DSide.Right);
        graphics1.DrawString("1", new Font(this.panelPortrait.Font.Name, 8f), (Brush) solidBrush1, (float) (x + (int.Parse((width / 2).ToString()) - 3)), (float) (int.Parse((this.panelPortrait.Height / 2).ToString()) - 5));
        int height = int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) this.panelLandscape.Height, 2)).ToString());
        int y = int.Parse(((this.panelLandscape.Height - height) / 2).ToString());
        graphics2.DrawRectangle(pen, 0, y, this.panelLandscape.Width - 1, height);
        ControlPaint.DrawBorder3D(graphics2, 0, y, this.panelLandscape.Width - 1, height, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
        ControlPaint.DrawBorder3D(graphics2, 0, y, this.panelLandscape.Width - 1, height, Border3DStyle.RaisedOuter, Border3DSide.Right);
        graphics2.DrawString("1", new Font(this.panelLandscape.Font.Name, 8f), (Brush) solidBrush1, (float) (int.Parse((this.panelLandscape.Width / 2).ToString()) - 6), (float) (y + (int.Parse((height / 2).ToString()) - 5)));
      }
      else if (this.iSplittingOption == 1)
      {
        int width1 = int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) this.panelPortrait.Height, 2)).ToString());
        int x1 = int.Parse(((this.panelPortrait.Width - width1) / 2).ToString());
        int height1 = int.Parse(((this.panelPortrait.Height - num1) / 2).ToString());
        int y1 = 0;
        for (int index = 0; index < num2; ++index)
        {
          graphics1.DrawRectangle(pen, x1, y1, width1, height1);
          ControlPaint.DrawBorder3D(graphics1, x1, y1, width1, height1, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
          ControlPaint.DrawBorder3D(graphics1, x1, y1, width1, height1, Border3DStyle.RaisedOuter, Border3DSide.Right);
          graphics1.DrawString((index + 1).ToString(), new Font(this.panelPortrait.Font.Name, 8f), (Brush) solidBrush1, (float) (x1 + (int.Parse((width1 / 2).ToString()) - 3)), (float) (int.Parse((height1 / 2).ToString()) + y1 - 3));
          y1 = y1 + height1 + num1;
        }
        int height2 = int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) this.panelLandscape.Height, 2)).ToString());
        int width2 = int.Parse(((this.panelLandscape.Width - num1) / 2).ToString());
        int y2 = int.Parse(((this.panelLandscape.Height - height2) / 2).ToString());
        int x2 = 0;
        for (int index = 0; index < num2; ++index)
        {
          graphics2.DrawRectangle(pen, x2, y2, width2, height2);
          ControlPaint.DrawBorder3D(graphics2, x2, y2, width2, height2, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
          ControlPaint.DrawBorder3D(graphics2, x2, y2, width2, height2, Border3DStyle.RaisedOuter, Border3DSide.Right);
          graphics2.DrawString((index + 1).ToString(), new Font(this.panelLandscape.Font.Name, 8f), (Brush) solidBrush1, (float) (int.Parse((width2 / 2).ToString()) + x2 - 3), (float) (y2 + (int.Parse((height2 / 2).ToString()) - 3)));
          x2 = x2 + width2 + num1;
        }
      }
      else if (this.iSplittingOption == 2)
      {
        int height1 = int.Parse(((this.panelPortrait.Height - num1) / 2).ToString());
        int width1 = int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) height1, 2)).ToString());
        int x1 = int.Parse(((this.panelPortrait.Width - width1 * 2 - num1) / 2).ToString());
        int y1 = 0;
        for (int index = 0; index < num2; ++index)
        {
          graphics1.DrawRectangle(pen, x1, y1, width1, height1);
          ControlPaint.DrawBorder3D(graphics1, x1, y1, width1, height1, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
          ControlPaint.DrawBorder3D(graphics1, x1, y1, width1, height1, Border3DStyle.RaisedOuter, Border3DSide.Right);
          graphics1.DrawString((index + 1).ToString(), new Font(this.panelPortrait.Font.Name, 8f), (Brush) solidBrush1, (float) (x1 + (int.Parse((width1 / 2).ToString()) - 3)), (float) (int.Parse((height1 / 2).ToString()) + y1 - 3));
          x1 = x1 + width1 + num1;
          if (index % 2 == 1)
          {
            y1 = y1 + height1 + num1;
            x1 = int.Parse(((this.panelPortrait.Width - width1 * 2 - num1) / 2).ToString());
          }
        }
        int width2 = int.Parse(((this.panelLandscape.Width - num1) / 2).ToString());
        int height2 = int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) width2, 2)).ToString());
        int y2 = int.Parse(((this.panelLandscape.Height - height2 * 2 - num1) / 2).ToString());
        int x2 = 0;
        for (int index = 0; index < num2; ++index)
        {
          graphics2.DrawRectangle(pen, x2, y2, width2, height2);
          ControlPaint.DrawBorder3D(graphics2, x2, y2, width2, height2, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
          ControlPaint.DrawBorder3D(graphics2, x2, y2, width2, height2, Border3DStyle.RaisedOuter, Border3DSide.Right);
          graphics2.DrawString((index + 1).ToString(), new Font(this.panelLandscape.Font.Name, 8f), (Brush) solidBrush1, (float) (int.Parse((width2 / 2).ToString()) + x2 - 3), (float) (y2 + (int.Parse((height2 / 2).ToString()) - 3)));
          x2 = x2 + width2 + num1;
          if (index % 2 == 1)
          {
            y2 = y2 + height2 + num1;
            x2 = 0;
          }
        }
      }
      else if (this.iSplittingOption == 3)
      {
        int width1 = (int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) this.panelPortrait.Height, 2)).ToString()) - num1) / 2;
        int x1 = int.Parse(((this.panelPortrait.Width - width1 * 2 - num1) / 2).ToString());
        int height1 = int.Parse(((this.panelPortrait.Height - num1 * 3) / 4).ToString());
        int y1 = 0;
        for (int index = 0; index < num2; ++index)
        {
          graphics1.DrawRectangle(pen, x1, y1, width1, height1);
          ControlPaint.DrawBorder3D(graphics1, x1, y1, width1, height1, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
          ControlPaint.DrawBorder3D(graphics1, x1, y1, width1, height1, Border3DStyle.RaisedOuter, Border3DSide.Right);
          graphics1.DrawString((index + 1).ToString(), new Font(this.panelPortrait.Font.Name, 8f), (Brush) solidBrush1, (float) (x1 + (int.Parse((width1 / 2).ToString()) - 5)), (float) (int.Parse((height1 / 2).ToString()) + y1 - 5));
          x1 = x1 + width1 + num1;
          if (index % 2 == 1)
          {
            y1 = y1 + height1 + num1;
            x1 = int.Parse(((this.panelPortrait.Width - width1 * 2 - num1) / 2).ToString());
          }
        }
        int height2 = (int.Parse(Math.Ceiling(Math.Round(num3 * (Decimal) this.panelLandscape.Width, 2)).ToString()) - num1) / 2;
        int y2 = int.Parse(((this.panelLandscape.Height - height2 * 2 - num1) / 2).ToString());
        int width2 = int.Parse(((this.panelLandscape.Width - num1 * 3) / 4).ToString());
        int x2 = 0;
        for (int index = 0; index < num2; ++index)
        {
          graphics2.DrawRectangle(pen, x2, y2, width2, height2);
          ControlPaint.DrawBorder3D(graphics2, x2, y2, width2, height2, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
          ControlPaint.DrawBorder3D(graphics2, x2, y2, width2, height2, Border3DStyle.RaisedOuter, Border3DSide.Right);
          Graphics graphics3 = graphics2;
          int num4 = index + 1;
          string s = num4.ToString();
          Font font = new Font(this.panelLandscape.Font.Name, 8f);
          SolidBrush solidBrush2 = solidBrush1;
          num4 = width2 / 2;
          double num5 = (double) (int.Parse(num4.ToString()) + x2 - 5);
          int num6 = y2;
          num4 = height2 / 2;
          int num7 = int.Parse(num4.ToString()) - 5;
          double num8 = (double) (num6 + num7);
          graphics3.DrawString(s, font, (Brush) solidBrush2, (float) num5, (float) num8);
          x2 = x2 + width2 + num1;
          if (index % 2 == 1 && index == num2 / 2 - 1)
          {
            y2 = y2 + height2 + num1;
            x2 = 0;
          }
        }
      }
      graphics1.Dispose();
      graphics2.Dispose();
    }

    private void frmPageSetup_Paint(object sender, PaintEventArgs e)
    {
      this.SetPageImageResolutions();
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Split Printing", "PAGE_SPLITTING", ResourceType.TITLE);
      this.lblSplitPattern.Text = this.GetResource("Preview", "PREVIEW", ResourceType.LABEL);
      this.lblPortraitPicture.Text = this.GetResource("For Potrait Picture", "POTRAIT_PICTURE", ResourceType.LABEL);
      this.lblLandscapePicture.Text = this.GetResource("For Landscape Picture", "LANDSCAPE_PICTURE", ResourceType.LABEL);
      this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='PRINT']" + "/Screen[@Name='PAGE_SPLITTING']";
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

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmPageSetup));
      this.pnlControl = new Panel();
      this.btnCancel = new Button();
      this.pnlSplitPattern = new Panel();
      this.pnlLblSplitPattern = new Panel();
      this.lblSplitPatternLine = new Label();
      this.lblSplitPattern = new Label();
      this.lblPortraitPicture = new Label();
      this.panelLandscape = new Panel();
      this.objDjVuCtl = new AxDjVuCtrl();
      this.lblLandscapePicture = new Label();
      this.panelPortrait = new Panel();
      this.pnlForm = new Panel();
      this.pnlControl.SuspendLayout();
      this.pnlSplitPattern.SuspendLayout();
      this.pnlLblSplitPattern.SuspendLayout();
      this.panelLandscape.SuspendLayout();
      this.objDjVuCtl.BeginInit();
      this.pnlForm.SuspendLayout();
      this.SuspendLayout();
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 206);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(15, 4, 15, 4);
      this.pnlControl.Size = new Size(432, 31);
      this.pnlControl.TabIndex = 19;
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(342, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 19;
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlSplitPattern.Controls.Add((Control) this.pnlLblSplitPattern);
      this.pnlSplitPattern.Controls.Add((Control) this.lblPortraitPicture);
      this.pnlSplitPattern.Controls.Add((Control) this.panelLandscape);
      this.pnlSplitPattern.Controls.Add((Control) this.lblLandscapePicture);
      this.pnlSplitPattern.Controls.Add((Control) this.panelPortrait);
      this.pnlSplitPattern.Dock = DockStyle.Fill;
      this.pnlSplitPattern.Location = new Point(0, 0);
      this.pnlSplitPattern.Name = "pnlSplitPattern";
      this.pnlSplitPattern.Size = new Size(432, 206);
      this.pnlSplitPattern.TabIndex = 2;
      this.pnlLblSplitPattern.Controls.Add((Control) this.lblSplitPatternLine);
      this.pnlLblSplitPattern.Controls.Add((Control) this.lblSplitPattern);
      this.pnlLblSplitPattern.Dock = DockStyle.Top;
      this.pnlLblSplitPattern.Location = new Point(0, 0);
      this.pnlLblSplitPattern.Name = "pnlLblSplitPattern";
      this.pnlLblSplitPattern.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblSplitPattern.Size = new Size(432, 28);
      this.pnlLblSplitPattern.TabIndex = 20;
      this.lblSplitPatternLine.BackColor = Color.Transparent;
      this.lblSplitPatternLine.Dock = DockStyle.Fill;
      this.lblSplitPatternLine.ForeColor = Color.Blue;
      this.lblSplitPatternLine.Image = (Image) Resources.GroupLine0;
      this.lblSplitPatternLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSplitPatternLine.Location = new Point(82, 0);
      this.lblSplitPatternLine.Name = "lblSplitPatternLine";
      this.lblSplitPatternLine.Size = new Size(335, 28);
      this.lblSplitPatternLine.TabIndex = 15;
      this.lblSplitPatternLine.Tag = (object) "";
      this.lblSplitPatternLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSplitPattern.BackColor = Color.Transparent;
      this.lblSplitPattern.Dock = DockStyle.Left;
      this.lblSplitPattern.ForeColor = Color.Blue;
      this.lblSplitPattern.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblSplitPattern.Location = new Point(7, 0);
      this.lblSplitPattern.Name = "lblSplitPattern";
      this.lblSplitPattern.Size = new Size(75, 28);
      this.lblSplitPattern.TabIndex = 12;
      this.lblSplitPattern.Tag = (object) "";
      this.lblSplitPattern.Text = "Preview";
      this.lblSplitPattern.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPortraitPicture.AutoSize = true;
      this.lblPortraitPicture.Location = new Point(73, 40);
      this.lblPortraitPicture.Name = "lblPortraitPicture";
      this.lblPortraitPicture.Size = new Size(98, 13);
      this.lblPortraitPicture.TabIndex = 0;
      this.lblPortraitPicture.Text = "For Portrait Picture";
      this.panelLandscape.BackColor = Color.White;
      this.panelLandscape.Controls.Add((Control) this.objDjVuCtl);
      this.panelLandscape.ImeMode = ImeMode.NoControl;
      this.panelLandscape.Location = new Point(268, 71);
      this.panelLandscape.Name = "panelLandscape";
      this.panelLandscape.Size = new Size(114, 114);
      this.panelLandscape.TabIndex = 6;
      this.objDjVuCtl.Enabled = true;
      this.objDjVuCtl.Location = new Point(0, 203);
      this.objDjVuCtl.Name = "objDjVuCtl";
      this.objDjVuCtl.OcxState = (AxHost.State) componentResourceManager.GetObject("objDjVuCtl.OcxState");
      this.objDjVuCtl.Size = new Size(279, 10);
      this.objDjVuCtl.TabIndex = 28;
      this.lblLandscapePicture.AutoSize = true;
      this.lblLandscapePicture.CausesValidation = false;
      this.lblLandscapePicture.Location = new Point(268, 40);
      this.lblLandscapePicture.Name = "lblLandscapePicture";
      this.lblLandscapePicture.Size = new Size(113, 13);
      this.lblLandscapePicture.TabIndex = 1;
      this.lblLandscapePicture.Text = "For Landscape Picture";
      this.panelPortrait.BackColor = Color.White;
      this.panelPortrait.ImeMode = ImeMode.NoControl;
      this.panelPortrait.Location = new Point(52, 71);
      this.panelPortrait.Name = "panelPortrait";
      this.panelPortrait.Size = new Size(129, 114);
      this.panelPortrait.TabIndex = 5;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlSplitPattern);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(2, 2);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(434, 239);
      this.pnlForm.TabIndex = 25;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(438, 243);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmPageSetup);
      this.Padding = new Padding(2);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Split Printing";
      this.Paint += new PaintEventHandler(this.frmPageSetup_Paint);
      this.pnlControl.ResumeLayout(false);
      this.pnlSplitPattern.ResumeLayout(false);
      this.pnlSplitPattern.PerformLayout();
      this.pnlLblSplitPattern.ResumeLayout(false);
      this.panelLandscape.ResumeLayout(false);
      this.objDjVuCtl.EndInit();
      this.pnlForm.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}

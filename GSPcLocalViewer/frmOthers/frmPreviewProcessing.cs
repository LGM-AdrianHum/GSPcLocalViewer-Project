// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOthers.frmPreviewProcessing
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmOthers
{
  public class frmPreviewProcessing : Form
  {
    private IContainer components;
    private Label lblPreviewProcessMsg;
    private PictureBox picPreviewLoading;
    private GSPcLocalViewer.frmPrint.frmPrint objParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblPreviewProcessMsg = new Label();
      this.picPreviewLoading = new PictureBox();
      ((ISupportInitialize) this.picPreviewLoading).BeginInit();
      this.SuspendLayout();
      this.lblPreviewProcessMsg.AutoSize = true;
      this.lblPreviewProcessMsg.Location = new Point(59, 84);
      this.lblPreviewProcessMsg.Name = "lblPreviewProcessMsg";
      this.lblPreviewProcessMsg.Size = new Size(137, 13);
      this.lblPreviewProcessMsg.TabIndex = 0;
      this.lblPreviewProcessMsg.Text = "It's processing. Please wait.";
      this.picPreviewLoading.Image = (Image) Resources.Loading1;
      this.picPreviewLoading.Location = new Point(108, 28);
      this.picPreviewLoading.Name = "picPreviewLoading";
      this.picPreviewLoading.Size = new Size(32, 32);
      this.picPreviewLoading.TabIndex = 1;
      this.picPreviewLoading.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.ClientSize = new Size(249, 106);
      this.ControlBox = false;
      this.Controls.Add((Control) this.picPreviewLoading);
      this.Controls.Add((Control) this.lblPreviewProcessMsg);
      this.Name = nameof (frmPreviewProcessing);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Preview";
      this.Load += new EventHandler(this.frmPreviewProcessing_Load);
      ((ISupportInitialize) this.picPreviewLoading).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public frmPreviewProcessing(GSPcLocalViewer.frmPrint.frmPrint frmPrintDlg)
    {
      this.InitializeComponent();
      this.objParent = frmPrintDlg;
      this.SetControlsText();
    }

    private void frmPreviewProcessing_Load(object sender, EventArgs e)
    {
      this.CenterToParent();
    }

    private void SetControlsText()
    {
      this.Text = this.GetResource("Preview", "PREVIEW", ResourceType.LABEL);
      this.lblPreviewProcessMsg.Text = this.GetResource("It's processing. Please wait.", "PREVIEW_PROC_MSG", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='PREVIEW_PROCESSING']";
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
            return this.objParent.frmParent.GetResourceValue(sDefaultValue, xQuery1);
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
        return this.objParent.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }
  }
}

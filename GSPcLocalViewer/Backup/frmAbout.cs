// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmAbout
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class frmAbout : Form
  {
    private frmViewer frmParent;
    private IContainer components;
    private PictureBox picLogo;
    private TextBox txtAbout1;
    private TextBox txtAbout2;
    private Panel pnlForm;

    [DllImport("user32.dll")]
    public static extern long HideCaret(IntPtr hwnd);

    public frmAbout(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.Font = Settings.Default.appFont;
      this.SetControlsText();
    }

    private void SetControlsText()
    {
      this.Text = this.GetResource("About GSPcLocal ", "ABOUT", ResourceType.TITLE);
      this.txtAbout1.Text = this.GetResource("Graphical Solution Package content Local  ", "GSP_LOCAL", ResourceType.LABEL);
      this.txtAbout1.Text = this.txtAbout1.Text.Replace("3.0.0.0", string.Empty) + Application.ProductVersion;
      this.txtAbout2.Text = this.GetResource("Warning: This program is protected by copyright law. Unauthorized reproduction of this program or any portion of it, is considered as illegal and may result in criminal penalities.", "WARNING", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='ABOUT']";
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

    private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.frmParent.HideDimmer();
    }

    private void txtAbout1_MouseDown(object sender, MouseEventArgs e)
    {
      frmAbout.HideCaret(this.txtAbout1.Handle);
    }

    private void txtAbout2_MouseDown(object sender, MouseEventArgs e)
    {
      frmAbout.HideCaret(this.txtAbout2.Handle);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.picLogo = new PictureBox();
      this.txtAbout1 = new TextBox();
      this.txtAbout2 = new TextBox();
      this.pnlForm = new Panel();
      ((ISupportInitialize) this.picLogo).BeginInit();
      this.pnlForm.SuspendLayout();
      this.SuspendLayout();
      this.picLogo.BackColor = Color.Black;
      this.picLogo.Dock = DockStyle.Top;
      this.picLogo.Image = (Image) Resources.Logo;
      this.picLogo.Location = new Point(0, 0);
      this.picLogo.Name = "picLogo";
      this.picLogo.Size = new Size(332, 27);
      this.picLogo.TabIndex = 0;
      this.picLogo.TabStop = false;
      this.txtAbout1.BackColor = Color.White;
      this.txtAbout1.BorderStyle = BorderStyle.None;
      this.txtAbout1.Dock = DockStyle.Top;
      this.txtAbout1.Location = new Point(20, 20);
      this.txtAbout1.Multiline = true;
      this.txtAbout1.Name = "txtAbout1";
      this.txtAbout1.ReadOnly = true;
      this.txtAbout1.Size = new Size(292, 46);
      this.txtAbout1.TabIndex = 1;
      this.txtAbout1.TabStop = false;
      this.txtAbout1.Text = "Graphical Solution Package content Local\r\n3.0.0.0";
      this.txtAbout1.MouseDown += new MouseEventHandler(this.txtAbout1_MouseDown);
      this.txtAbout2.BackColor = Color.White;
      this.txtAbout2.BorderStyle = BorderStyle.None;
      this.txtAbout2.Dock = DockStyle.Fill;
      this.txtAbout2.Location = new Point(20, 66);
      this.txtAbout2.Multiline = true;
      this.txtAbout2.Name = "txtAbout2";
      this.txtAbout2.ReadOnly = true;
      this.txtAbout2.Size = new Size(292, 71);
      this.txtAbout2.TabIndex = 1;
      this.txtAbout2.TabStop = false;
      this.txtAbout2.Text = "Warning: This program is protected by copyright law. Unauthorized reproduction of this program or any portion of it, is considered as illegal and may result in criminal penalities.";
      this.txtAbout2.MouseDown += new MouseEventHandler(this.txtAbout2_MouseDown);
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.Controls.Add((Control) this.txtAbout2);
      this.pnlForm.Controls.Add((Control) this.txtAbout1);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 27);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(20);
      this.pnlForm.Size = new Size(332, 157);
      this.pnlForm.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(332, 184);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.picLogo);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmAbout);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "About GSPcLocal";
      this.FormClosing += new FormClosingEventHandler(this.frmAbout_FormClosing);
      ((ISupportInitialize) this.picLogo).EndInit();
      this.pnlForm.ResumeLayout(false);
      this.pnlForm.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}

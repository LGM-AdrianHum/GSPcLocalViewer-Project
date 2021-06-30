// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmDjVuAuthentication
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
  public class frmDjVuAuthentication : Form
  {
    private IContainer components;
    private Panel pnlContents;
    private Label lblSecuredDjVu;
    private Panel pnlControl;
    private Button btnOK;
    private Button btnCancel;
    private TextBox txtUserId;
    private Label lblUserId;
    private Panel pnlForm;
    private TextBox txtPassword;
    private Label lblPassword;
    private frmViewer frmParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlContents = new Panel();
      this.lblSecuredDjVu = new Label();
      this.pnlControl = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.txtPassword = new TextBox();
      this.txtUserId = new TextBox();
      this.lblPassword = new Label();
      this.lblUserId = new Label();
      this.pnlForm = new Panel();
      this.pnlContents.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.SuspendLayout();
      this.pnlContents.BackColor = Color.White;
      this.pnlContents.BorderStyle = BorderStyle.FixedSingle;
      this.pnlContents.Controls.Add((Control) this.lblSecuredDjVu);
      this.pnlContents.Controls.Add((Control) this.pnlControl);
      this.pnlContents.Controls.Add((Control) this.txtPassword);
      this.pnlContents.Controls.Add((Control) this.txtUserId);
      this.pnlContents.Controls.Add((Control) this.lblPassword);
      this.pnlContents.Controls.Add((Control) this.lblUserId);
      this.pnlContents.Dock = DockStyle.Fill;
      this.pnlContents.Location = new Point(2, 2);
      this.pnlContents.Name = "pnlContents";
      this.pnlContents.Size = new Size(292, 116);
      this.pnlContents.TabIndex = 20;
      this.lblSecuredDjVu.BackColor = Color.White;
      this.lblSecuredDjVu.Dock = DockStyle.Top;
      this.lblSecuredDjVu.ForeColor = Color.Black;
      this.lblSecuredDjVu.Location = new Point(0, 0);
      this.lblSecuredDjVu.Name = "lblSecuredDjVu";
      this.lblSecuredDjVu.Padding = new Padding(3, 7, 0, 0);
      this.lblSecuredDjVu.Size = new Size(290, 27);
      this.lblSecuredDjVu.TabIndex = 22;
      this.lblSecuredDjVu.Text = "Secured DjVu";
      this.pnlControl.BackColor = Color.White;
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 83);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(1, 4, 10, 4);
      this.pnlControl.Size = new Size(290, 31);
      this.pnlControl.TabIndex = 20;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(130, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(205, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
      this.txtPassword.Location = new Point(89, 56);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new Size(191, 20);
      this.txtPassword.TabIndex = 2;
      this.txtUserId.BorderStyle = BorderStyle.FixedSingle;
      this.txtUserId.Location = new Point(89, 30);
      this.txtUserId.Name = "txtUserId";
      this.txtUserId.Size = new Size(191, 20);
      this.txtUserId.TabIndex = 1;
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new Point(9, 58);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new Size(53, 13);
      this.lblPassword.TabIndex = 0;
      this.lblPassword.Text = "Password";
      this.lblUserId.AutoSize = true;
      this.lblUserId.Location = new Point(9, 32);
      this.lblUserId.Name = "lblUserId";
      this.lblUserId.Size = new Size(41, 13);
      this.lblUserId.TabIndex = 0;
      this.lblUserId.Text = "User Id";
      this.pnlForm.Controls.Add((Control) this.pnlContents);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(2);
      this.pnlForm.Size = new Size(296, 120);
      this.pnlForm.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(296, 120);
      this.Controls.Add((Control) this.pnlForm);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmDjVuAuthentication);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Authentication";
      this.pnlContents.ResumeLayout(false);
      this.pnlContents.PerformLayout();
      this.pnlControl.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmDjVuAuthentication(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.UpdateFont();
      this.DialogResult = DialogResult.Cancel;
      this.LoadResources();
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblSecuredDjVu.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.txtPassword.Text != string.Empty && this.txtUserId.Text != string.Empty)
      {
        this.DialogResult = DialogResult.OK;
      }
      else
      {
        this.txtUserId.Text = string.Empty;
        this.txtPassword.Text = string.Empty;
      }
    }

    public string UserId
    {
      get
      {
        return this.txtUserId.Text.Trim();
      }
    }

    public string Password
    {
      get
      {
        return this.txtPassword.Text.Trim();
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Authentication", "DJVU_AUTHENTICATION", ResourceType.TITLE);
      this.lblSecuredDjVu.Text = this.GetResource("SecuredDjVu", "SECURED_DJVU", ResourceType.LABEL);
      this.lblUserId.Text = this.GetResource("User ID", "USER_ID", ResourceType.LABEL);
      this.lblPassword.Text = this.GetResource("Password", "PASSWORD", ResourceType.LABEL);
      this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='DJVU_AUTHENTICATION']";
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
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmSecurity
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
  public class frmSecurity : Form
  {
    private IContainer components;
    private Panel pnlContents;
    private Label lblSecuredDjVu;
    private Panel pnlControl;
    private Button btnOK;
    private Button btnCancel;
    private TextBox txtKey;
    private Label lblUserId;
    private Panel pnlForm;
    private string[] sLocks;

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
      this.txtKey = new TextBox();
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
      this.pnlContents.Controls.Add((Control) this.txtKey);
      this.pnlContents.Controls.Add((Control) this.lblUserId);
      this.pnlContents.Dock = DockStyle.Fill;
      this.pnlContents.Location = new Point(2, 2);
      this.pnlContents.Name = "pnlContents";
      this.pnlContents.Size = new Size(292, 96);
      this.pnlContents.TabIndex = 20;
      this.lblSecuredDjVu.BackColor = Color.White;
      this.lblSecuredDjVu.Dock = DockStyle.Top;
      this.lblSecuredDjVu.ForeColor = Color.Black;
      this.lblSecuredDjVu.Location = new Point(0, 0);
      this.lblSecuredDjVu.Name = "lblSecuredDjVu";
      this.lblSecuredDjVu.Padding = new Padding(3, 7, 0, 0);
      this.lblSecuredDjVu.Size = new Size(290, 27);
      this.lblSecuredDjVu.TabIndex = 22;
      this.lblSecuredDjVu.Text = "Secured Book";
      this.pnlControl.BackColor = Color.White;
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 63);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(1, 4, 10, 4);
      this.pnlControl.Size = new Size(290, 31);
      this.pnlControl.TabIndex = 20;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(130, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnOK.KeyDown += new KeyEventHandler(this.btnOK_KeyDown);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(205, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.txtKey.BorderStyle = BorderStyle.FixedSingle;
      this.txtKey.Location = new Point(89, 30);
      this.txtKey.Name = "txtKey";
      this.txtKey.PasswordChar = '*';
      this.txtKey.Size = new Size(191, 20);
      this.txtKey.TabIndex = 0;
      this.lblUserId.AutoSize = true;
      this.lblUserId.Location = new Point(9, 32);
      this.lblUserId.Name = "lblUserId";
      this.lblUserId.Size = new Size(66, 13);
      this.lblUserId.TabIndex = 0;
      this.lblUserId.Text = "Security Key";
      this.pnlForm.Controls.Add((Control) this.pnlContents);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(2);
      this.pnlForm.Size = new Size(296, 100);
      this.pnlForm.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(296, 100);
      this.Controls.Add((Control) this.pnlForm);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmSecurity);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Authentication";
      this.pnlContents.ResumeLayout(false);
      this.pnlContents.PerformLayout();
      this.pnlControl.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmSecurity(string[] sLocks1)
    {
      this.InitializeComponent();
      this.sLocks = sLocks1;
      this.UpdateFont();
      this.DialogResult = DialogResult.Cancel;
      this.SetControlsText();
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblSecuredDjVu.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.sLocks.Length; ++index)
      {
        if (this.sLocks[index] == this.txtKey.Text)
        {
          this.DialogResult = DialogResult.OK;
          this.Close();
          return;
        }
      }
      this.txtKey.Text = string.Empty;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void SetControlsText()
    {
    }

    private void btnOK_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.btnOK_Click((object) null, (EventArgs) null);
    }

    public string Key
    {
      get
      {
        return this.txtKey.Text.Trim();
      }
    }
  }
}

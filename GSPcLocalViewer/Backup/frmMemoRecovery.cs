// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmMemoRecovery
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmMemoRecovery : Form
  {
    private frmViewer frmParent;
    private IContainer components;
    private Panel pnlForm;
    private Panel pnlContents;
    private Label lblRecoveryFile;
    private TextBox txtRecoveryFile;
    private Button btnRecoveryFileBrowse;
    private Panel pnlControl;
    private Button btnOK;
    private Button btnCancel;
    private Label lblMemoRecovery;
    private OpenFileDialog ofd;

    public frmMemoRecovery(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.UpdateFont();
      this.LoadResources();
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblMemoRecovery.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private void btnGlobalMemoFolderBrowse_Click(object sender, EventArgs e)
    {
      if (this.txtRecoveryFile.Text.Trim() != string.Empty && Directory.Exists(Path.GetDirectoryName(this.txtRecoveryFile.Text)))
        this.ofd.InitialDirectory = Path.GetDirectoryName(this.txtRecoveryFile.Text);
      else
        this.ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
      this.ofd.Filter = "Xml Files (*.xml)|*.xml";
      this.ofd.RestoreDirectory = false;
      if (this.ofd.ShowDialog() != DialogResult.OK)
        return;
      this.txtRecoveryFile.Text = this.ofd.FileName;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.txtRecoveryFile.Text.Trim().ToUpper().EndsWith(".XML"))
      {
        if (File.Exists(this.txtRecoveryFile.Text))
        {
          try
          {
            new XmlDocument().Load(this.txtRecoveryFile.Text);
            switch (MessageBox.Show(this.GetResource("Current local memos will be lost Do you want to proceed with memo recovery?", "MEMO_RECOVERY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
              case DialogResult.Yes:
                this.frmParent.RecoverLocalMemos(this.txtRecoveryFile.Text);
                this.Close();
                break;
              case DialogResult.No:
                this.Close();
                break;
            }
          }
          catch
          {
            int num = (int) MessageBox.Show(this.GetResource("(E-MRC-EM001) Failed to load specified object", "(E-MRC-EM001)_FAILED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
        }
        else
        {
          int num1 = (int) MessageBox.Show(this.GetResource("(E-MRC-EM002) Specified information does not exist", "(E-MRC-EM002)_INFORMATION", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show(this.GetResource("(E-MRC-EM003) Not in required format", "(E-MRC-EM003)_FORMAT", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Memo", "MEMO_RECOVERY", ResourceType.TITLE);
      this.lblMemoRecovery.Text = this.GetResource("Local Memo Recovery", "LOCAL_MEMO_RECOVERY", ResourceType.LABEL);
      this.lblRecoveryFile.Text = this.GetResource("Recovery File", "RECOVERY_FILE", ResourceType.LABEL);
      this.btnRecoveryFileBrowse.Text = this.GetResource("Browse", "BROWSE", ResourceType.BUTTON);
      this.btnOK.Text = this.GetResource("Recover", "RECOVER", ResourceType.BUTTON);
      this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MEMO_RECOVERY']";
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

    private void frmMemoRecovery_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.frmParent.HideDimmer();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlForm = new Panel();
      this.pnlContents = new Panel();
      this.lblMemoRecovery = new Label();
      this.pnlControl = new Panel();
      this.btnOK = new Button();
      this.btnCancel = new Button();
      this.btnRecoveryFileBrowse = new Button();
      this.txtRecoveryFile = new TextBox();
      this.lblRecoveryFile = new Label();
      this.ofd = new OpenFileDialog();
      this.pnlForm.SuspendLayout();
      this.pnlContents.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.SuspendLayout();
      this.pnlForm.Controls.Add((Control) this.pnlContents);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(2);
      this.pnlForm.Size = new Size(438, 152);
      this.pnlForm.TabIndex = 0;
      this.pnlContents.BackColor = Color.White;
      this.pnlContents.BorderStyle = BorderStyle.FixedSingle;
      this.pnlContents.Controls.Add((Control) this.lblMemoRecovery);
      this.pnlContents.Controls.Add((Control) this.pnlControl);
      this.pnlContents.Controls.Add((Control) this.btnRecoveryFileBrowse);
      this.pnlContents.Controls.Add((Control) this.txtRecoveryFile);
      this.pnlContents.Controls.Add((Control) this.lblRecoveryFile);
      this.pnlContents.Dock = DockStyle.Fill;
      this.pnlContents.Location = new Point(2, 2);
      this.pnlContents.Name = "pnlContents";
      this.pnlContents.Size = new Size(434, 148);
      this.pnlContents.TabIndex = 20;
      this.lblMemoRecovery.BackColor = Color.White;
      this.lblMemoRecovery.Dock = DockStyle.Top;
      this.lblMemoRecovery.ForeColor = Color.Black;
      this.lblMemoRecovery.Location = new Point(0, 0);
      this.lblMemoRecovery.Name = "lblMemoRecovery";
      this.lblMemoRecovery.Padding = new Padding(3, 7, 0, 0);
      this.lblMemoRecovery.Size = new Size(432, 27);
      this.lblMemoRecovery.TabIndex = 0;
      this.lblMemoRecovery.Text = "Local Memo Recovery";
      this.pnlControl.BackColor = Color.White;
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 115);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 15, 4);
      this.pnlControl.Size = new Size(432, 31);
      this.pnlControl.TabIndex = 4;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(267, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "Recover";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(342, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnRecoveryFileBrowse.Location = new Point(342, 68);
      this.btnRecoveryFileBrowse.Name = "btnRecoveryFileBrowse";
      this.btnRecoveryFileBrowse.Size = new Size(75, 23);
      this.btnRecoveryFileBrowse.TabIndex = 3;
      this.btnRecoveryFileBrowse.Text = "Browse";
      this.btnRecoveryFileBrowse.UseVisualStyleBackColor = true;
      this.btnRecoveryFileBrowse.Click += new EventHandler(this.btnGlobalMemoFolderBrowse_Click);
      this.txtRecoveryFile.BorderStyle = BorderStyle.FixedSingle;
      this.txtRecoveryFile.Location = new Point(17, 69);
      this.txtRecoveryFile.Name = "txtRecoveryFile";
      this.txtRecoveryFile.Size = new Size(319, 21);
      this.txtRecoveryFile.TabIndex = 2;
      this.lblRecoveryFile.AutoSize = true;
      this.lblRecoveryFile.Location = new Point(14, 53);
      this.lblRecoveryFile.Name = "lblRecoveryFile";
      this.lblRecoveryFile.Size = new Size(72, 13);
      this.lblRecoveryFile.TabIndex = 1;
      this.lblRecoveryFile.Text = "Recovery File";
      this.ofd.FileName = "openFileDialog1";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(438, 152);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmMemoRecovery);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Memo";
      this.FormClosing += new FormClosingEventHandler(this.frmMemoRecovery_FormClosing);
      this.pnlForm.ResumeLayout(false);
      this.pnlContents.ResumeLayout(false);
      this.pnlContents.PerformLayout();
      this.pnlControl.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}

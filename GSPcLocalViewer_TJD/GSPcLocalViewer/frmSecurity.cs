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

		public string Key
		{
			get
			{
				return this.txtKey.Text.Trim();
			}
		}

		public frmSecurity(string[] sLocks1)
		{
			this.InitializeComponent();
			this.sLocks = sLocks1;
			this.UpdateFont();
			base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.SetControlsText();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < (int)this.sLocks.Length; i++)
			{
				if (this.sLocks[i] == this.txtKey.Text)
				{
					base.DialogResult = System.Windows.Forms.DialogResult.OK;
					base.Close();
					return;
				}
			}
			this.txtKey.Text = string.Empty;
		}

		private void btnOK_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.btnOK_Click(null, null);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
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
			base.SuspendLayout();
			this.pnlContents.BackColor = Color.White;
			this.pnlContents.BorderStyle = BorderStyle.FixedSingle;
			this.pnlContents.Controls.Add(this.lblSecuredDjVu);
			this.pnlContents.Controls.Add(this.pnlControl);
			this.pnlContents.Controls.Add(this.txtKey);
			this.pnlContents.Controls.Add(this.lblUserId);
			this.pnlContents.Dock = DockStyle.Fill;
			this.pnlContents.Location = new Point(2, 2);
			this.pnlContents.Name = "pnlContents";
			this.pnlContents.Size = new System.Drawing.Size(292, 96);
			this.pnlContents.TabIndex = 20;
			this.lblSecuredDjVu.BackColor = Color.White;
			this.lblSecuredDjVu.Dock = DockStyle.Top;
			this.lblSecuredDjVu.ForeColor = Color.Black;
			this.lblSecuredDjVu.Location = new Point(0, 0);
			this.lblSecuredDjVu.Name = "lblSecuredDjVu";
			this.lblSecuredDjVu.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblSecuredDjVu.Size = new System.Drawing.Size(290, 27);
			this.lblSecuredDjVu.TabIndex = 22;
			this.lblSecuredDjVu.Text = "Secured Book";
			this.pnlControl.BackColor = Color.White;
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 63);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(1, 4, 10, 4);
			this.pnlControl.Size = new System.Drawing.Size(290, 31);
			this.pnlControl.TabIndex = 20;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(130, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnOK.KeyDown += new KeyEventHandler(this.btnOK_KeyDown);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(205, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.txtKey.BorderStyle = BorderStyle.FixedSingle;
			this.txtKey.Location = new Point(89, 30);
			this.txtKey.Name = "txtKey";
			this.txtKey.PasswordChar = '*';
			this.txtKey.Size = new System.Drawing.Size(191, 20);
			this.txtKey.TabIndex = 0;
			this.lblUserId.AutoSize = true;
			this.lblUserId.Location = new Point(9, 32);
			this.lblUserId.Name = "lblUserId";
			this.lblUserId.Size = new System.Drawing.Size(66, 13);
			this.lblUserId.TabIndex = 0;
			this.lblUserId.Text = "Security Key";
			this.pnlForm.Controls.Add(this.pnlContents);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(2);
			this.pnlForm.Size = new System.Drawing.Size(296, 100);
			this.pnlForm.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(296, 100);
			base.Controls.Add(this.pnlForm);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSecurity";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Authentication";
			this.pnlContents.ResumeLayout(false);
			this.pnlContents.PerformLayout();
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void SetControlsText()
		{
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblSecuredDjVu.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}
	}
}
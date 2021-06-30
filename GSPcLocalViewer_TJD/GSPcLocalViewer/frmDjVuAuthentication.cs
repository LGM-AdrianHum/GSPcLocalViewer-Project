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

		public string Password
		{
			get
			{
				return this.txtPassword.Text.Trim();
			}
		}

		public string UserId
		{
			get
			{
				return this.txtUserId.Text.Trim();
			}
		}

		public frmDjVuAuthentication(frmViewer frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.UpdateFont();
			base.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.LoadResources();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.txtPassword.Text != string.Empty && this.txtUserId.Text != string.Empty)
			{
				base.DialogResult = System.Windows.Forms.DialogResult.OK;
				return;
			}
			this.txtUserId.Text = string.Empty;
			this.txtPassword.Text = string.Empty;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='DJVU_AUTHENTICATION']");
				if (rType != ResourceType.TITLE)
				{
					if (rType == ResourceType.LABEL)
					{
						str = string.Concat(str, "/Resources[@Name='LABEL']");
					}
					else if (rType == ResourceType.BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='BUTTON']");
					}
					else if (rType == ResourceType.CHECK_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='CHECK_BOX']");
					}
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
					}
					else if (rType == ResourceType.COMBO_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='COMBO_BOX']");
					}
					else if (rType == ResourceType.GRID_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='GRID_VIEW']");
					}
					else if (rType == ResourceType.LIST_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='LIST_VIEW']");
					}
					else if (rType == ResourceType.MENU_BAR)
					{
						str = string.Concat(str, "/Resources[@Name='MENU_BAR']");
					}
					else if (rType == ResourceType.RADIO_BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='RADIO_BUTTON']");
					}
					else if (rType == ResourceType.CONTEXT_MENU)
					{
						str = string.Concat(str, "/Resources[@Name='CONTEXT_MENU']");
					}
					else if (rType == ResourceType.TOOLSTRIP)
					{
						str = string.Concat(str, "/Resources[@Name='TOOLSTRIP']");
					}
					str = string.Concat(str, "/Resource[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
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
			base.SuspendLayout();
			this.pnlContents.BackColor = Color.White;
			this.pnlContents.BorderStyle = BorderStyle.FixedSingle;
			this.pnlContents.Controls.Add(this.lblSecuredDjVu);
			this.pnlContents.Controls.Add(this.pnlControl);
			this.pnlContents.Controls.Add(this.txtPassword);
			this.pnlContents.Controls.Add(this.txtUserId);
			this.pnlContents.Controls.Add(this.lblPassword);
			this.pnlContents.Controls.Add(this.lblUserId);
			this.pnlContents.Dock = DockStyle.Fill;
			this.pnlContents.Location = new Point(2, 2);
			this.pnlContents.Name = "pnlContents";
			this.pnlContents.Size = new System.Drawing.Size(292, 116);
			this.pnlContents.TabIndex = 20;
			this.lblSecuredDjVu.BackColor = Color.White;
			this.lblSecuredDjVu.Dock = DockStyle.Top;
			this.lblSecuredDjVu.ForeColor = Color.Black;
			this.lblSecuredDjVu.Location = new Point(0, 0);
			this.lblSecuredDjVu.Name = "lblSecuredDjVu";
			this.lblSecuredDjVu.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblSecuredDjVu.Size = new System.Drawing.Size(290, 27);
			this.lblSecuredDjVu.TabIndex = 22;
			this.lblSecuredDjVu.Text = "Secured DjVu";
			this.pnlControl.BackColor = Color.White;
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 83);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(1, 4, 10, 4);
			this.pnlControl.Size = new System.Drawing.Size(290, 31);
			this.pnlControl.TabIndex = 20;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(130, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(205, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
			this.txtPassword.Location = new Point(89, 56);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(191, 20);
			this.txtPassword.TabIndex = 2;
			this.txtUserId.BorderStyle = BorderStyle.FixedSingle;
			this.txtUserId.Location = new Point(89, 30);
			this.txtUserId.Name = "txtUserId";
			this.txtUserId.Size = new System.Drawing.Size(191, 20);
			this.txtUserId.TabIndex = 1;
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new Point(9, 58);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(53, 13);
			this.lblPassword.TabIndex = 0;
			this.lblPassword.Text = "Password";
			this.lblUserId.AutoSize = true;
			this.lblUserId.Location = new Point(9, 32);
			this.lblUserId.Name = "lblUserId";
			this.lblUserId.Size = new System.Drawing.Size(41, 13);
			this.lblUserId.TabIndex = 0;
			this.lblUserId.Text = "User Id";
			this.pnlForm.Controls.Add(this.pnlContents);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(2);
			this.pnlForm.Size = new System.Drawing.Size(296, 120);
			this.pnlForm.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(296, 120);
			base.Controls.Add(this.pnlForm);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmDjVuAuthentication";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Authentication";
			this.pnlContents.ResumeLayout(false);
			this.pnlContents.PerformLayout();
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			base.ResumeLayout(false);
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

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblSecuredDjVu.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}
	}
}
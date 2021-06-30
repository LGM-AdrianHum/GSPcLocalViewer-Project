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

		public frmAbout(frmViewer frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.Font = Settings.Default.appFont;
			this.SetControlsText();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmAbout_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.frmParent.HideDimmer();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='ABOUT']");
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
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
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

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern long HideCaret(IntPtr hwnd);

		private void InitializeComponent()
		{
			this.picLogo = new PictureBox();
			this.txtAbout1 = new TextBox();
			this.txtAbout2 = new TextBox();
			this.pnlForm = new Panel();
			((ISupportInitialize)this.picLogo).BeginInit();
			this.pnlForm.SuspendLayout();
			base.SuspendLayout();
			this.picLogo.BackColor = Color.Black;
			this.picLogo.Dock = DockStyle.Top;
			this.picLogo.Image = Resources.Logo;
			this.picLogo.Location = new Point(0, 0);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(332, 27);
			this.picLogo.TabIndex = 0;
			this.picLogo.TabStop = false;
			this.txtAbout1.BackColor = Color.White;
			this.txtAbout1.BorderStyle = BorderStyle.None;
			this.txtAbout1.Dock = DockStyle.Top;
			this.txtAbout1.Location = new Point(20, 20);
			this.txtAbout1.Multiline = true;
			this.txtAbout1.Name = "txtAbout1";
			this.txtAbout1.ReadOnly = true;
			this.txtAbout1.Size = new System.Drawing.Size(292, 46);
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
			this.txtAbout2.Size = new System.Drawing.Size(292, 71);
			this.txtAbout2.TabIndex = 1;
			this.txtAbout2.TabStop = false;
			this.txtAbout2.Text = "Warning: This program is protected by copyright law. Unauthorized reproduction of this program or any portion of it, is considered as illegal and may result in criminal penalities.";
			this.txtAbout2.MouseDown += new MouseEventHandler(this.txtAbout2_MouseDown);
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.Controls.Add(this.txtAbout2);
			this.pnlForm.Controls.Add(this.txtAbout1);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 27);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(20);
			this.pnlForm.Size = new System.Drawing.Size(332, 157);
			this.pnlForm.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(332, 184);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.picLogo);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmAbout";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "About GSPcLocal";
			base.FormClosing += new FormClosingEventHandler(this.frmAbout_FormClosing);
			((ISupportInitialize)this.picLogo).EndInit();
			this.pnlForm.ResumeLayout(false);
			this.pnlForm.PerformLayout();
			base.ResumeLayout(false);
		}

		private void SetControlsText()
		{
			this.Text = this.GetResource("About GSPcLocal ", "ABOUT", ResourceType.TITLE);
			this.txtAbout1.Text = this.GetResource("Graphical Solution Package content Local  ", "GSP_LOCAL", ResourceType.LABEL);
			this.txtAbout1.Text = string.Concat(this.txtAbout1.Text.Replace("3.0.0.0", string.Empty), Application.ProductVersion);
			this.txtAbout2.Text = this.GetResource("Warning: This program is protected by copyright law. Unauthorized reproduction of this program or any portion of it, is considered as illegal and may result in criminal penalities.", "WARNING", ResourceType.LABEL);
		}

		private void txtAbout1_MouseDown(object sender, MouseEventArgs e)
		{
			frmAbout.HideCaret(this.txtAbout1.Handle);
		}

		private void txtAbout2_MouseDown(object sender, MouseEventArgs e)
		{
			frmAbout.HideCaret(this.txtAbout2.Handle);
		}
	}
}
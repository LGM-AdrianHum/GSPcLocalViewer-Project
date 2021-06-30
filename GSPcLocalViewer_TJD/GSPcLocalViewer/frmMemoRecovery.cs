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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnGlobalMemoFolderBrowse_Click(object sender, EventArgs e)
		{
			if (!(this.txtRecoveryFile.Text.Trim() != string.Empty) || !Directory.Exists(Path.GetDirectoryName(this.txtRecoveryFile.Text)))
			{
				this.ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			}
			else
			{
				this.ofd.InitialDirectory = Path.GetDirectoryName(this.txtRecoveryFile.Text);
			}
			this.ofd.Filter = "Xml Files (*.xml)|*.xml";
			this.ofd.RestoreDirectory = false;
			if (this.ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.txtRecoveryFile.Text = this.ofd.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (!this.txtRecoveryFile.Text.Trim().ToUpper().EndsWith(".XML"))
			{
				MessageBox.Show(this.GetResource("(E-MRC-EM003) Not in required format", "(E-MRC-EM003)_FORMAT", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				if (!File.Exists(this.txtRecoveryFile.Text))
				{
					MessageBox.Show(this.GetResource("(E-MRC-EM002) Specified information does not exist", "(E-MRC-EM002)_INFORMATION", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				try
				{
					(new XmlDocument()).Load(this.txtRecoveryFile.Text);
					System.Windows.Forms.DialogResult dialogResult = MessageBox.Show(this.GetResource("Current local memos will be lost Do you want to proceed with memo recovery?", "MEMO_RECOVERY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (dialogResult == System.Windows.Forms.DialogResult.Yes)
					{
						this.frmParent.RecoverLocalMemos(this.txtRecoveryFile.Text);
						base.Close();
					}
					else if (dialogResult == System.Windows.Forms.DialogResult.No)
					{
						base.Close();
					}
				}
				catch
				{
					MessageBox.Show(this.GetResource("(E-MRC-EM001) Failed to load specified object", "(E-MRC-EM001)_FAILED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
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

		private void frmMemoRecovery_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.frmParent.HideDimmer();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MEMO_RECOVERY']");
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
			base.SuspendLayout();
			this.pnlForm.Controls.Add(this.pnlContents);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(2);
			this.pnlForm.Size = new System.Drawing.Size(438, 152);
			this.pnlForm.TabIndex = 0;
			this.pnlContents.BackColor = Color.White;
			this.pnlContents.BorderStyle = BorderStyle.FixedSingle;
			this.pnlContents.Controls.Add(this.lblMemoRecovery);
			this.pnlContents.Controls.Add(this.pnlControl);
			this.pnlContents.Controls.Add(this.btnRecoveryFileBrowse);
			this.pnlContents.Controls.Add(this.txtRecoveryFile);
			this.pnlContents.Controls.Add(this.lblRecoveryFile);
			this.pnlContents.Dock = DockStyle.Fill;
			this.pnlContents.Location = new Point(2, 2);
			this.pnlContents.Name = "pnlContents";
			this.pnlContents.Size = new System.Drawing.Size(434, 148);
			this.pnlContents.TabIndex = 20;
			this.lblMemoRecovery.BackColor = Color.White;
			this.lblMemoRecovery.Dock = DockStyle.Top;
			this.lblMemoRecovery.ForeColor = Color.Black;
			this.lblMemoRecovery.Location = new Point(0, 0);
			this.lblMemoRecovery.Name = "lblMemoRecovery";
			this.lblMemoRecovery.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblMemoRecovery.Size = new System.Drawing.Size(432, 27);
			this.lblMemoRecovery.TabIndex = 0;
			this.lblMemoRecovery.Text = "Local Memo Recovery";
			this.pnlControl.BackColor = Color.White;
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 115);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(432, 31);
			this.pnlControl.TabIndex = 4;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(267, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "Recover";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(342, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnRecoveryFileBrowse.Location = new Point(342, 68);
			this.btnRecoveryFileBrowse.Name = "btnRecoveryFileBrowse";
			this.btnRecoveryFileBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnRecoveryFileBrowse.TabIndex = 3;
			this.btnRecoveryFileBrowse.Text = "Browse";
			this.btnRecoveryFileBrowse.UseVisualStyleBackColor = true;
			this.btnRecoveryFileBrowse.Click += new EventHandler(this.btnGlobalMemoFolderBrowse_Click);
			this.txtRecoveryFile.BorderStyle = BorderStyle.FixedSingle;
			this.txtRecoveryFile.Location = new Point(17, 69);
			this.txtRecoveryFile.Name = "txtRecoveryFile";
			this.txtRecoveryFile.Size = new System.Drawing.Size(319, 21);
			this.txtRecoveryFile.TabIndex = 2;
			this.lblRecoveryFile.AutoSize = true;
			this.lblRecoveryFile.Location = new Point(14, 53);
			this.lblRecoveryFile.Name = "lblRecoveryFile";
			this.lblRecoveryFile.Size = new System.Drawing.Size(72, 13);
			this.lblRecoveryFile.TabIndex = 1;
			this.lblRecoveryFile.Text = "Recovery File";
			this.ofd.FileName = "openFileDialog1";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(438, 152);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmMemoRecovery";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Memo";
			base.FormClosing += new FormClosingEventHandler(this.frmMemoRecovery_FormClosing);
			this.pnlForm.ResumeLayout(false);
			this.pnlContents.ResumeLayout(false);
			this.pnlContents.PerformLayout();
			this.pnlControl.ResumeLayout(false);
			base.ResumeLayout(false);
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

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblMemoRecovery.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}
	}
}
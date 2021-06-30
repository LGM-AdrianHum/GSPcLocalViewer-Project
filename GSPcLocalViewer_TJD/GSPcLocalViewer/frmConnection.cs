using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmConnection : Form
	{
		private frmViewer frmParent;

		private IContainer components;

		private Label lblHistoryLine;

		private Panel pnlProxyServer;

		private Label lblPassword;

		private Panel pnlLblHistory;

		private Label lblProxyServer;

		private Panel pnlControl;

		private Button btnOK;

		private Button btnCancel;

		private Panel pnlForm;

		private Label lblSeconds;

		private Label lblConnTimeOut;

		private Label lblIp;

		private Label lblLogin;

		private RadioButton rdoType3;

		private RadioButton rdoType2;

		private RadioButton rdoType0;

		private TextBox txtPassword;

		private TextBox txtLogin;

		private TextBox txtIP;

		private TextBox txtPort;

		private Label lblPort;

		private RadioButton rdoType1;

		private ComboBox listBoxSeconds;

		private Panel panel3;

		private Panel panel2;

		private Label label1;

		private Panel panel1;

		private Panel panel5;

		private Panel panel4;

		private Label label2;

		public frmConnection(frmViewer parent)
		{
			this.InitializeComponent();
			this.frmParent = parent;
			this.UpdateFont();
			this.LoadResources();
			if (!Settings.Default.appProxyLogin.Equals(string.Empty))
			{
				this.txtLogin.Text = Settings.Default.appProxyLogin;
			}
			if (!Settings.Default.appProxyPassword.Equals(string.Empty))
			{
				this.txtPassword.Text = Settings.Default.appProxyPassword;
			}
			if (!Settings.Default.appProxyPort.Equals(string.Empty))
			{
				this.txtPort.Text = Settings.Default.appProxyPort;
			}
			if (!Settings.Default.appProxyIP.Equals(string.Empty))
			{
				this.txtIP.Text = Settings.Default.appProxyIP;
			}
			if (!Settings.Default.appProxyTimeOut.Equals(string.Empty))
			{
				this.listBoxSeconds.Text = Settings.Default.appProxyTimeOut;
			}
			if (Settings.Default.appProxyType.Equals("0"))
			{
				this.rdoType0.Checked = true;
				return;
			}
			if (Settings.Default.appProxyType.Equals("1"))
			{
				this.rdoType1.Checked = true;
				return;
			}
			if (Settings.Default.appProxyType.Equals("2"))
			{
				this.rdoType2.Checked = true;
				return;
			}
			if (Settings.Default.appProxyType.Equals("3"))
			{
				this.rdoType3.Checked = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.rdoType0.Checked)
			{
				this.SaveSettings();
			}
			else if (this.rdoType2.Checked)
			{
				if (this.txtIP.Text.Equals(string.Empty))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM002) Proxy address is missing", "(W-CVC-WM002)_ADDRESS_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (!this.IsValidIP(this.txtIP.Text))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM001) Proxy address is not valid", "(W-CVC-WM001)_INVALID", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				this.SaveSettings();
			}
			else if (this.rdoType1.Checked)
			{
				if (this.txtIP.Text.Equals(string.Empty))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM002) Proxy address is missing", "(W-CVC-WM002)_ADDRESS_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (!this.IsValidIP(this.txtIP.Text))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM001) Proxy address is not valid", "(W-CVC-WM001)_INVALID", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				this.SaveSettings();
			}
			else if (this.rdoType3.Checked)
			{
				if (this.txtIP.Text.Equals(string.Empty))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM002) Proxy address is missing", "(W-CVC-WM002)_ADDRESS_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (!this.IsValidIP(this.txtIP.Text))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM001) Proxy address is not valid", "(W-CVC-WM001)_INVALID", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (this.txtLogin.Text.Equals(string.Empty))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM003) Proxy login is missing", "(W-CVC-WM003)_PROXY_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (this.txtPassword.Text.Equals(string.Empty))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM004) Proxy password is missing", "(W-CVC-WM004)_PASSWORD_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				this.SaveSettings();
			}
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmConnection_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveUserSettings();
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			if (base.Owner.GetType() == typeof(frmViewer))
			{
				this.frmParent.HideDimmer();
			}
		}

		private void frmConnection_Load(object sender, EventArgs e)
		{
			this.LoadUserSettings();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='CONNECTION']");
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
			this.pnlProxyServer = new Panel();
			this.panel5 = new Panel();
			this.lblIp = new Label();
			this.lblPassword = new Label();
			this.lblLogin = new Label();
			this.txtIP = new TextBox();
			this.txtLogin = new TextBox();
			this.txtPort = new TextBox();
			this.txtPassword = new TextBox();
			this.lblPort = new Label();
			this.panel4 = new Panel();
			this.label2 = new Label();
			this.panel3 = new Panel();
			this.lblConnTimeOut = new Label();
			this.listBoxSeconds = new ComboBox();
			this.lblSeconds = new Label();
			this.panel2 = new Panel();
			this.label1 = new Label();
			this.panel1 = new Panel();
			this.rdoType0 = new RadioButton();
			this.rdoType1 = new RadioButton();
			this.rdoType2 = new RadioButton();
			this.rdoType3 = new RadioButton();
			this.pnlLblHistory = new Panel();
			this.lblHistoryLine = new Label();
			this.lblProxyServer = new Label();
			this.pnlControl = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlForm = new Panel();
			this.pnlProxyServer.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.pnlLblHistory.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.pnlForm.SuspendLayout();
			base.SuspendLayout();
			this.pnlProxyServer.AutoScroll = true;
			this.pnlProxyServer.Controls.Add(this.panel5);
			this.pnlProxyServer.Controls.Add(this.panel4);
			this.pnlProxyServer.Controls.Add(this.panel3);
			this.pnlProxyServer.Controls.Add(this.panel2);
			this.pnlProxyServer.Controls.Add(this.panel1);
			this.pnlProxyServer.Controls.Add(this.pnlLblHistory);
			this.pnlProxyServer.Dock = DockStyle.Fill;
			this.pnlProxyServer.Location = new Point(0, 0);
			this.pnlProxyServer.Name = "pnlProxyServer";
			this.pnlProxyServer.Size = new System.Drawing.Size(407, 458);
			this.pnlProxyServer.TabIndex = 23;
			this.panel5.Controls.Add(this.lblIp);
			this.panel5.Controls.Add(this.lblPassword);
			this.panel5.Controls.Add(this.lblLogin);
			this.panel5.Controls.Add(this.txtIP);
			this.panel5.Controls.Add(this.txtLogin);
			this.panel5.Controls.Add(this.txtPort);
			this.panel5.Controls.Add(this.txtPassword);
			this.panel5.Controls.Add(this.lblPort);
			this.panel5.Dock = DockStyle.Fill;
			this.panel5.Location = new Point(0, 240);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(407, 218);
			this.panel5.TabIndex = 38;
			this.lblIp.AutoSize = true;
			this.lblIp.Location = new Point(39, 20);
			this.lblIp.Name = "lblIp";
			this.lblIp.Size = new System.Drawing.Size(74, 13);
			this.lblIp.TabIndex = 20;
			this.lblIp.Text = "Proxy Address";
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new Point(39, 149);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(82, 13);
			this.lblPassword.TabIndex = 8;
			this.lblPassword.Text = "Proxy Password";
			this.lblLogin.AutoSize = true;
			this.lblLogin.Location = new Point(39, 106);
			this.lblLogin.Name = "lblLogin";
			this.lblLogin.Size = new System.Drawing.Size(62, 13);
			this.lblLogin.TabIndex = 20;
			this.lblLogin.Text = "Proxy Login";
			this.txtIP.Location = new Point(42, 40);
			this.txtIP.Name = "txtIP";
			this.txtIP.Size = new System.Drawing.Size(277, 20);
			this.txtIP.TabIndex = 4;
			this.txtLogin.Location = new Point(42, 126);
			this.txtLogin.Name = "txtLogin";
			this.txtLogin.Size = new System.Drawing.Size(277, 20);
			this.txtLogin.TabIndex = 6;
			this.txtPort.Location = new Point(42, 83);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(277, 20);
			this.txtPort.TabIndex = 5;
			this.txtPassword.Location = new Point(42, 169);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(277, 20);
			this.txtPassword.TabIndex = 7;
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new Point(39, 63);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(55, 13);
			this.lblPort.TabIndex = 32;
			this.lblPort.Text = "Proxy Port";
			this.panel4.Controls.Add(this.label2);
			this.panel4.Dock = DockStyle.Top;
			this.panel4.Location = new Point(0, 212);
			this.panel4.Name = "panel4";
			this.panel4.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.panel4.Size = new System.Drawing.Size(407, 28);
			this.panel4.TabIndex = 37;
			this.label2.BackColor = Color.Transparent;
			this.label2.Dock = DockStyle.Fill;
			this.label2.ForeColor = Color.Blue;
			this.label2.Image = Resources.GroupLine0;
			this.label2.ImageAlign = ContentAlignment.MiddleLeft;
			this.label2.Location = new Point(7, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(385, 28);
			this.label2.TabIndex = 15;
			this.label2.Tag = "";
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.panel3.Controls.Add(this.lblConnTimeOut);
			this.panel3.Controls.Add(this.listBoxSeconds);
			this.panel3.Controls.Add(this.lblSeconds);
			this.panel3.Dock = DockStyle.Top;
			this.panel3.Location = new Point(0, 163);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(407, 49);
			this.panel3.TabIndex = 36;
			this.lblConnTimeOut.AutoSize = true;
			this.lblConnTimeOut.Location = new Point(39, 18);
			this.lblConnTimeOut.Name = "lblConnTimeOut";
			this.lblConnTimeOut.Size = new System.Drawing.Size(116, 13);
			this.lblConnTimeOut.TabIndex = 23;
			this.lblConnTimeOut.Text = "Connection TimeOut = ";
			this.listBoxSeconds.DropDownStyle = ComboBoxStyle.DropDownList;
			this.listBoxSeconds.FormattingEnabled = true;
			ComboBox.ObjectCollection items = this.listBoxSeconds.Items;
			object[] objArray = new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120" };
			items.AddRange(objArray);
			this.listBoxSeconds.Location = new Point(158, 15);
			this.listBoxSeconds.Name = "listBoxSeconds";
			this.listBoxSeconds.Size = new System.Drawing.Size(65, 21);
			this.listBoxSeconds.TabIndex = 3;
			this.lblSeconds.AutoSize = true;
			this.lblSeconds.Location = new Point(229, 18);
			this.lblSeconds.Name = "lblSeconds";
			this.lblSeconds.Size = new System.Drawing.Size(50, 13);
			this.lblSeconds.TabIndex = 25;
			this.lblSeconds.Text = "seconds.";
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = DockStyle.Top;
			this.panel2.Location = new Point(0, 135);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.panel2.Size = new System.Drawing.Size(407, 28);
			this.panel2.TabIndex = 35;
			this.label1.BackColor = Color.Transparent;
			this.label1.Dock = DockStyle.Fill;
			this.label1.ForeColor = Color.Blue;
			this.label1.Image = Resources.GroupLine0;
			this.label1.ImageAlign = ContentAlignment.MiddleLeft;
			this.label1.Location = new Point(7, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(385, 28);
			this.label1.TabIndex = 15;
			this.label1.Tag = "";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.panel1.Controls.Add(this.rdoType0);
			this.panel1.Controls.Add(this.rdoType1);
			this.panel1.Controls.Add(this.rdoType2);
			this.panel1.Controls.Add(this.rdoType3);
			this.panel1.Dock = DockStyle.Top;
			this.panel1.Location = new Point(0, 28);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(407, 107);
			this.panel1.TabIndex = 34;
			this.rdoType0.AutoSize = true;
			this.rdoType0.Location = new Point(42, 11);
			this.rdoType0.Name = "rdoType0";
			this.rdoType0.Size = new System.Drawing.Size(214, 17);
			this.rdoType0.TabIndex = 0;
			this.rdoType0.TabStop = true;
			this.rdoType0.Text = "Don't use proxy server (connect directly)";
			this.rdoType0.UseVisualStyleBackColor = true;
			this.rdoType0.CheckedChanged += new EventHandler(this.rdoType0_CheckedChanged);
			this.rdoType1.AutoSize = true;
			this.rdoType1.Location = new Point(42, 34);
			this.rdoType1.Name = "rdoType1";
			this.rdoType1.Size = new System.Drawing.Size(104, 17);
			this.rdoType1.TabIndex = 33;
			this.rdoType1.TabStop = true;
			this.rdoType1.Text = "Use HTTP proxy";
			this.rdoType1.UseVisualStyleBackColor = true;
			this.rdoType1.CheckedChanged += new EventHandler(this.rdoType1_CheckedChanged);
			this.rdoType2.AutoSize = true;
			this.rdoType2.Location = new Point(42, 57);
			this.rdoType2.Name = "rdoType2";
			this.rdoType2.Size = new System.Drawing.Size(117, 17);
			this.rdoType2.TabIndex = 1;
			this.rdoType2.TabStop = true;
			this.rdoType2.Text = "Use SOCKS4 proxy";
			this.rdoType2.UseVisualStyleBackColor = true;
			this.rdoType2.CheckedChanged += new EventHandler(this.rdoType2_CheckedChanged);
			this.rdoType3.AutoSize = true;
			this.rdoType3.Location = new Point(42, 80);
			this.rdoType3.Name = "rdoType3";
			this.rdoType3.Size = new System.Drawing.Size(117, 17);
			this.rdoType3.TabIndex = 2;
			this.rdoType3.TabStop = true;
			this.rdoType3.Text = "Use SOCKS5 proxy";
			this.rdoType3.UseVisualStyleBackColor = true;
			this.rdoType3.CheckedChanged += new EventHandler(this.rdoType3_CheckedChanged);
			this.pnlLblHistory.Controls.Add(this.lblHistoryLine);
			this.pnlLblHistory.Controls.Add(this.lblProxyServer);
			this.pnlLblHistory.Dock = DockStyle.Top;
			this.pnlLblHistory.Location = new Point(0, 0);
			this.pnlLblHistory.Name = "pnlLblHistory";
			this.pnlLblHistory.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblHistory.Size = new System.Drawing.Size(407, 28);
			this.pnlLblHistory.TabIndex = 16;
			this.lblHistoryLine.BackColor = Color.Transparent;
			this.lblHistoryLine.Dock = DockStyle.Fill;
			this.lblHistoryLine.ForeColor = Color.Blue;
			this.lblHistoryLine.Image = Resources.GroupLine0;
			this.lblHistoryLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblHistoryLine.Location = new Point(121, 0);
			this.lblHistoryLine.Name = "lblHistoryLine";
			this.lblHistoryLine.Size = new System.Drawing.Size(271, 28);
			this.lblHistoryLine.TabIndex = 15;
			this.lblHistoryLine.Tag = "";
			this.lblHistoryLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblProxyServer.BackColor = Color.Transparent;
			this.lblProxyServer.Dock = DockStyle.Left;
			this.lblProxyServer.ForeColor = Color.Blue;
			this.lblProxyServer.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblProxyServer.Location = new Point(7, 0);
			this.lblProxyServer.Name = "lblProxyServer";
			this.lblProxyServer.Size = new System.Drawing.Size(114, 28);
			this.lblProxyServer.TabIndex = 13;
			this.lblProxyServer.Tag = "";
			this.lblProxyServer.Text = "Proxy Server";
			this.lblProxyServer.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 458);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(407, 31);
			this.pnlControl.TabIndex = 18;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(242, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(317, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlProxyServer);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(2, 2);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(409, 491);
			this.pnlForm.TabIndex = 20;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(413, 495);
			base.Controls.Add(this.pnlForm);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 520);
			base.Name = "frmConnection";
			base.Padding = new System.Windows.Forms.Padding(2);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Connection";
			base.Load += new EventHandler(this.frmConnection_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmConnection_FormClosing);
			this.pnlProxyServer.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pnlLblHistory.ResumeLayout(false);
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public bool IsValidIP(string addr)
		{
			Regex regex = new Regex("^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
			bool flag = false;
			flag = (addr != "" ? regex.IsMatch(addr, 0) : false);
			return flag;
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Connection", "CONNECTION", ResourceType.TITLE);
			this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.lblProxyServer.Text = this.GetResource("Proxy Server", "PROXY_SERVER", ResourceType.LABEL);
			this.rdoType0.Text = this.GetResource("Don't use proxy server (connect directly)", "CONNECT_DIRECTLY", ResourceType.RADIO_BUTTON);
			this.rdoType1.Text = this.GetResource("Use HTTP proxy", "HTTP_PROXY", ResourceType.RADIO_BUTTON);
			this.rdoType2.Text = this.GetResource("Use SOCKS4 proxy", "SOCKS4_PROXY", ResourceType.RADIO_BUTTON);
			this.rdoType3.Text = this.GetResource("Use SOCKS5 proxy", "SOCKS5_PROXY", ResourceType.RADIO_BUTTON);
			this.lblConnTimeOut.Text = this.GetResource("Connection TimeOut =", "CONNECTION_TIMEOUT", ResourceType.LABEL);
			this.lblSeconds.Text = this.GetResource("seconds", "SECONDS", ResourceType.LABEL);
			this.lblIp.Text = this.GetResource("Proxy Address", "PROXY_ADDRESS", ResourceType.LABEL);
			this.lblPort.Text = this.GetResource("Proxy Port", "PROXY_PORT", ResourceType.LABEL);
			this.lblLogin.Text = this.GetResource("Proxy Login", "PROXY_LOGIN", ResourceType.LABEL);
			this.lblPassword.Text = this.GetResource("Proxy Password", "PROXY_PASSWORD", ResourceType.LABEL);
		}

		private void LoadUserSettings()
		{
			if (Settings.Default.frmConnectionLocation == new Point(0, 0))
			{
				base.StartPosition = FormStartPosition.CenterScreen;
				return;
			}
			base.Location = Settings.Default.frmConnectionLocation;
		}

		private void rdoType0_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rdoType0.Checked)
			{
				TextBox textBox = this.txtIP;
				TextBox textBox1 = this.txtLogin;
				TextBox textBox2 = this.txtPassword;
				int num = 0;
				bool flag = (bool)num;
				this.txtPort.Enabled = (bool)num;
				bool flag1 = flag;
				bool flag2 = flag1;
				textBox2.Enabled = flag1;
				bool flag3 = flag2;
				bool flag4 = flag3;
				textBox1.Enabled = flag3;
				textBox.Enabled = flag4;
			}
		}

		private void rdoType1_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rdoType1.Checked)
			{
				TextBox textBox = this.txtIP;
				TextBox textBox1 = this.txtLogin;
				TextBox textBox2 = this.txtPassword;
				int num = 1;
				bool flag = (bool)num;
				this.txtPort.Enabled = (bool)num;
				bool flag1 = flag;
				bool flag2 = flag1;
				textBox2.Enabled = flag1;
				bool flag3 = flag2;
				bool flag4 = flag3;
				textBox1.Enabled = flag3;
				textBox.Enabled = flag4;
			}
		}

		private void rdoType2_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rdoType2.Checked)
			{
				TextBox textBox = this.txtLogin;
				int num = 0;
				bool flag = (bool)num;
				this.txtPassword.Enabled = (bool)num;
				textBox.Enabled = flag;
				TextBox textBox1 = this.txtIP;
				int num1 = 1;
				bool flag1 = (bool)num1;
				this.txtPort.Enabled = (bool)num1;
				textBox1.Enabled = flag1;
			}
		}

		private void rdoType3_CheckedChanged(object sender, EventArgs e)
		{
			if (this.rdoType3.Checked)
			{
				TextBox textBox = this.txtIP;
				TextBox textBox1 = this.txtLogin;
				TextBox textBox2 = this.txtPassword;
				int num = 1;
				bool flag = (bool)num;
				this.txtPort.Enabled = (bool)num;
				bool flag1 = flag;
				bool flag2 = flag1;
				textBox2.Enabled = flag1;
				bool flag3 = flag2;
				bool flag4 = flag3;
				textBox1.Enabled = flag3;
				textBox.Enabled = flag4;
			}
		}

		public void SaveSettings()
		{
			if (this.rdoType0.Checked)
			{
				Settings.Default.appProxyType = "0";
			}
			if (this.rdoType1.Checked)
			{
				Settings.Default.appProxyType = "1";
			}
			if (this.rdoType2.Checked)
			{
				Settings.Default.appProxyType = "2";
			}
			if (this.rdoType3.Checked)
			{
				Settings.Default.appProxyType = "3";
			}
			Settings.Default.appProxyPort = this.txtPort.Text;
			Settings.Default.appProxyPassword = this.txtPassword.Text;
			Settings.Default.appProxyTimeOut = this.listBoxSeconds.Text;
			Settings.Default.appProxyIP = this.txtIP.Text;
			Settings.Default.appProxyLogin = this.txtLogin.Text;
		}

		private void SaveUserSettings()
		{
			if (base.WindowState == FormWindowState.Normal)
			{
				Settings.Default.frmConnectionLocation = base.Location;
				return;
			}
			Settings.Default.frmConnectionLocation = base.RestoreBounds.Location;
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}
	}
}
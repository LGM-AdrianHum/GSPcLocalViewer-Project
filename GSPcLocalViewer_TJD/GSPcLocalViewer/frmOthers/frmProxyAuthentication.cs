using GSPcLocalViewer;
using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer.frmOthers
{
	public class frmProxyAuthentication : Form
	{
		private IContainer components;

		private Panel pnlLblHistory;

		private Label lblHistoryLine;

		private Label lblProxyServer;

		private Panel pnlControl;

		private Button btnOK;

		private Button btnCancel;

		private Panel pnlMainContainer;

		private Label lblPassword;

		private Label lblLogin;

		private TextBox txtLogin;

		private TextBox txtPassword;

		private Label lblError;

		private frmViewer frmParent;

		public XmlDocument xmlDocument;

		private bool bNormalClosed;

		public frmProxyAuthentication(frmViewer parent)
		{
			this.InitializeComponent();
			this.frmParent = parent;
			this.UpdateFont();
			this.LoadXMLFirstTime();
			this.LoadResources();
		}

		public frmProxyAuthentication()
		{
			this.InitializeComponent();
			this.UpdateFont();
			this.LoadXMLFirstTime();
			this.LoadResources();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Dashbord.bShowProxy = false;
			Program.bShowProxyScreen = false;
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				this.bNormalClosed = true;
				if (this.txtLogin.Text.Equals(string.Empty))
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM003) Proxy login is missing", "(W-CVC-WM003)_PROXY_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else if (!this.txtPassword.Text.Equals(string.Empty))
				{
					this.SaveSettings();
					this.WriteUserSettingINI();
					base.Close();
				}
				else
				{
					MessageHandler.ShowMessage(this, this.GetResource("(W-CVC-WM004) Proxy password is missing", "(W-CVC-WM004)_PASSWORD_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			catch (Exception exception)
			{
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

		private void frmProxyAuthentication_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.bNormalClosed)
			{
				Dashbord.bShowProxy = false;
				Program.bShowProxyScreen = false;
			}
		}

		private void frmProxyAuthentication_Load(object sender, EventArgs e)
		{
			try
			{
				this.txtLogin.Text = Settings.Default.appProxyLogin;
				this.txtPassword.Text = Settings.Default.appProxyPassword;
			}
			catch (Exception exception)
			{
			}
		}

		public string GetOSLanguage()
		{
			CultureInfo.CurrentCulture.ClearCachedData();
			string displayName = CultureInfo.CurrentCulture.DisplayName;
			char[] chrArray = new char[] { ' ' };
			return displayName.Split(chrArray)[0].ToString();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='PROXYAUTHENTICATION']");
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
					resourceValue = this.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		public string GetResourceValue(string sDefaultValue, string xQuery)
		{
			string str;
			try
			{
				string str1 = string.Concat("/GSPcLocalViewer", xQuery);
				XmlNode xmlNodes = this.xmlDocument.SelectSingleNode(str1);
				if (xmlNodes == null)
				{
					str = sDefaultValue;
				}
				else if (this.xmlDocument == null)
				{
					str = sDefaultValue;
				}
				else if (xmlNodes.Attributes.Count <= 0 || xmlNodes.Attributes["Value"] == null)
				{
					str = sDefaultValue;
				}
				else
				{
					string value = xmlNodes.Attributes["Value"].Value;
					str = (string.IsNullOrEmpty(value) ? sDefaultValue : value);
				}
			}
			catch (Exception exception)
			{
				str = sDefaultValue;
			}
			return str;
		}

		private void InitializeComponent()
		{
			this.pnlLblHistory = new Panel();
			this.lblHistoryLine = new Label();
			this.lblProxyServer = new Label();
			this.pnlControl = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlMainContainer = new Panel();
			this.lblError = new Label();
			this.lblPassword = new Label();
			this.lblLogin = new Label();
			this.txtLogin = new TextBox();
			this.txtPassword = new TextBox();
			this.pnlLblHistory.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.pnlMainContainer.SuspendLayout();
			base.SuspendLayout();
			this.pnlLblHistory.Controls.Add(this.lblHistoryLine);
			this.pnlLblHistory.Controls.Add(this.lblProxyServer);
			this.pnlLblHistory.Dock = DockStyle.Top;
			this.pnlLblHistory.Location = new Point(0, 0);
			this.pnlLblHistory.Name = "pnlLblHistory";
			this.pnlLblHistory.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblHistory.Size = new System.Drawing.Size(261, 28);
			this.pnlLblHistory.TabIndex = 17;
			this.lblHistoryLine.BackColor = Color.Transparent;
			this.lblHistoryLine.Dock = DockStyle.Fill;
			this.lblHistoryLine.ForeColor = Color.Blue;
			this.lblHistoryLine.Image = Resources.GroupLine0;
			this.lblHistoryLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblHistoryLine.Location = new Point(121, 0);
			this.lblHistoryLine.Name = "lblHistoryLine";
			this.lblHistoryLine.Size = new System.Drawing.Size(125, 28);
			this.lblHistoryLine.TabIndex = 15;
			this.lblHistoryLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblProxyServer.BackColor = Color.Transparent;
			this.lblProxyServer.Dock = DockStyle.Left;
			this.lblProxyServer.ForeColor = Color.Blue;
			this.lblProxyServer.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblProxyServer.Location = new Point(7, 0);
			this.lblProxyServer.Name = "lblProxyServer";
			this.lblProxyServer.Size = new System.Drawing.Size(114, 28);
			this.lblProxyServer.TabIndex = 13;
			this.lblProxyServer.Text = "Proxy Server";
			this.lblProxyServer.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 146);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(261, 31);
			this.pnlControl.TabIndex = 1;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(96, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(171, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlMainContainer.Controls.Add(this.lblError);
			this.pnlMainContainer.Controls.Add(this.lblPassword);
			this.pnlMainContainer.Controls.Add(this.lblLogin);
			this.pnlMainContainer.Controls.Add(this.txtLogin);
			this.pnlMainContainer.Controls.Add(this.txtPassword);
			this.pnlMainContainer.Dock = DockStyle.Fill;
			this.pnlMainContainer.Location = new Point(0, 28);
			this.pnlMainContainer.Name = "pnlMainContainer";
			this.pnlMainContainer.Size = new System.Drawing.Size(261, 118);
			this.pnlMainContainer.TabIndex = 0;
			this.lblError.AutoSize = true;
			this.lblError.ForeColor = Color.Black;
			this.lblError.Location = new Point(23, 14);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(214, 13);
			this.lblError.TabIndex = 22;
			this.lblError.Text = "There is an error in the input (password etc.)";
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new Point(23, 79);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(82, 13);
			this.lblPassword.TabIndex = 8;
			this.lblPassword.Text = "Proxy Password";
			this.lblLogin.AutoSize = true;
			this.lblLogin.Location = new Point(23, 36);
			this.lblLogin.Name = "lblLogin";
			this.lblLogin.Size = new System.Drawing.Size(62, 13);
			this.lblLogin.TabIndex = 20;
			this.lblLogin.Text = "Proxy Login";
			this.txtLogin.Location = new Point(25, 52);
			this.txtLogin.Name = "txtLogin";
			this.txtLogin.Size = new System.Drawing.Size(210, 20);
			this.txtLogin.TabIndex = 0;
			this.txtPassword.Location = new Point(25, 95);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(210, 20);
			this.txtPassword.TabIndex = 1;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new System.Drawing.Size(261, 177);
			base.Controls.Add(this.pnlMainContainer);
			base.Controls.Add(this.pnlControl);
			base.Controls.Add(this.pnlLblHistory);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(267, 205);
			base.MinimizeBox = false;
			base.Name = "frmProxyAuthentication";
			base.ShowIcon = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Connection";
			base.TopMost = true;
			base.Load += new EventHandler(this.frmProxyAuthentication_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmProxyAuthentication_FormClosing);
			this.pnlLblHistory.ResumeLayout(false);
			this.pnlControl.ResumeLayout(false);
			this.pnlMainContainer.ResumeLayout(false);
			this.pnlMainContainer.PerformLayout();
			base.ResumeLayout(false);
		}

		private bool IsValidDateTime(string strInput)
		{
			bool flag;
			try
			{
				DateTime.ParseExact(strInput.Trim(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
				flag = true;
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Connection", "PROXYAUTHENTICATION", ResourceType.TITLE);
			this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.lblProxyServer.Text = this.GetResource("Proxy Server", "PROXY_SERVER", ResourceType.LABEL);
			this.lblLogin.Text = this.GetResource("Proxy Login", "PROXY_LOGIN", ResourceType.LABEL);
			this.lblPassword.Text = this.GetResource("Proxy Password", "PROXY_PASSWORD", ResourceType.LABEL);
			this.lblError.Text = this.GetResource("There is an error in the input (password etc.).", "PROXY_ERROR", ResourceType.LABEL);
		}

		public void LoadXMLFirstTime()
		{
			this.xmlDocument = null;
			try
			{
				if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
				{
					this.GetOSLanguage();
					string str = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] files = Directory.GetFiles(str, "*.xml");
					for (int i = 0; i < (int)files.Length; i++)
					{
						try
						{
							if (File.Exists(files[i]))
							{
								int num = files[i].IndexOf(".xml");
								int num1 = files[i].LastIndexOf("\\");
								string str1 = files[i].Substring(num1 + 1, num - num1 - 1);
								string str2 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml");
								XmlDocument xmlDocument = new XmlDocument();
								xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml"));
								XmlNode xmlNodes = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
								string value = xmlNodes.Attributes["EN_NAME"].Value;
								if (value.Length != 0 && value != null && value == Settings.Default.appLanguage && File.Exists(str2))
								{
									this.xmlDocument = new XmlDocument();
									this.xmlDocument.Load(str2);
									goto yoyo0;
								}
							}
						}
						catch
						{
						}
					}
				}
				else if (Settings.Default.appLanguage == null || Settings.Default.appLanguage.Length == 0)
				{
					Settings.Default.appLanguage = "English";
					string oSLanguage = this.GetOSLanguage();
					string str3 = string.Concat(Application.StartupPath, "\\Language XMLs\\");
					string[] strArrays = Directory.GetFiles(str3, "*.xml");
					for (int j = 0; j < (int)strArrays.Length; j++)
					{
						try
						{
							if (File.Exists(strArrays[j]))
							{
								int num2 = strArrays[j].IndexOf(".xml");
								int num3 = strArrays[j].LastIndexOf("\\");
								string str4 = strArrays[j].Substring(num3 + 1, num2 - num3 - 1);
								string str5 = string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml");
								XmlDocument xmlDocument1 = new XmlDocument();
								xmlDocument1.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str4, ".xml"));
								XmlNode xmlNodes1 = xmlDocument1.SelectSingleNode("//GSPcLocalViewer");
								string value1 = xmlNodes1.Attributes["EN_NAME"].Value;
								if (value1 == oSLanguage)
								{
									if (File.Exists(str5))
									{
										this.xmlDocument.Load(str5);
										break;
									}
								}
								else if (value1 == Settings.Default.appLanguage && File.Exists(str5))
								{
									this.xmlDocument.Load(str5);
									break;
								}
							}
						}
						catch
						{
						}
					}
				}
			yoyo0:
			}
			catch
			{
			}
			if (this.xmlDocument == null)
			{
				this.xmlDocument = new XmlDocument();
				Settings.Default.appLanguage = "English";
			}
		}

		public void SaveSettings()
		{
			try
			{
				Settings.Default.appProxyPassword = this.txtPassword.Text;
				Settings.Default.appProxyLogin = this.txtLogin.Text;
			}
			catch (Exception exception)
			{
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}

		private void WriteUserSettingINI()
		{
			try
			{
				string empty = string.Empty;
				string str = string.Concat(Application.StartupPath, "\\UserSet.ini");
				IniFileIO iniFileIO = new IniFileIO();
				iniFileIO.WriteValue("PROXY_SETTINGS", "USERNAME", Settings.Default.appProxyLogin.ToString(), str);
				iniFileIO.WriteValue("PROXY_SETTINGS", "PASSWORD", Settings.Default.appProxyPassword.ToString(), str);
			}
			catch
			{
			}
		}
	}
}
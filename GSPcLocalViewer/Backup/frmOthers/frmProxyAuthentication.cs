// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOthers.frmProxyAuthentication
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
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
      this.SuspendLayout();
      this.pnlLblHistory.Controls.Add((Control) this.lblHistoryLine);
      this.pnlLblHistory.Controls.Add((Control) this.lblProxyServer);
      this.pnlLblHistory.Dock = DockStyle.Top;
      this.pnlLblHistory.Location = new Point(0, 0);
      this.pnlLblHistory.Name = "pnlLblHistory";
      this.pnlLblHistory.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblHistory.Size = new Size(261, 28);
      this.pnlLblHistory.TabIndex = 17;
      this.lblHistoryLine.BackColor = Color.Transparent;
      this.lblHistoryLine.Dock = DockStyle.Fill;
      this.lblHistoryLine.ForeColor = Color.Blue;
      this.lblHistoryLine.Image = (Image) Resources.GroupLine0;
      this.lblHistoryLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblHistoryLine.Location = new Point(121, 0);
      this.lblHistoryLine.Name = "lblHistoryLine";
      this.lblHistoryLine.Size = new Size(125, 28);
      this.lblHistoryLine.TabIndex = 15;
      this.lblHistoryLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblProxyServer.BackColor = Color.Transparent;
      this.lblProxyServer.Dock = DockStyle.Left;
      this.lblProxyServer.ForeColor = Color.Blue;
      this.lblProxyServer.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblProxyServer.Location = new Point(7, 0);
      this.lblProxyServer.Name = "lblProxyServer";
      this.lblProxyServer.Size = new Size(114, 28);
      this.lblProxyServer.TabIndex = 13;
      this.lblProxyServer.Text = "Proxy Server";
      this.lblProxyServer.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 146);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 15, 4);
      this.pnlControl.Size = new Size(261, 31);
      this.pnlControl.TabIndex = 1;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(96, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.DialogResult = DialogResult.Cancel;
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(171, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlMainContainer.Controls.Add((Control) this.lblError);
      this.pnlMainContainer.Controls.Add((Control) this.lblPassword);
      this.pnlMainContainer.Controls.Add((Control) this.lblLogin);
      this.pnlMainContainer.Controls.Add((Control) this.txtLogin);
      this.pnlMainContainer.Controls.Add((Control) this.txtPassword);
      this.pnlMainContainer.Dock = DockStyle.Fill;
      this.pnlMainContainer.Location = new Point(0, 28);
      this.pnlMainContainer.Name = "pnlMainContainer";
      this.pnlMainContainer.Size = new Size(261, 118);
      this.pnlMainContainer.TabIndex = 0;
      this.lblError.AutoSize = true;
      this.lblError.ForeColor = Color.Black;
      this.lblError.Location = new Point(23, 14);
      this.lblError.Name = "lblError";
      this.lblError.Size = new Size(214, 13);
      this.lblError.TabIndex = 22;
      this.lblError.Text = "There is an error in the input (password etc.)";
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new Point(23, 79);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new Size(82, 13);
      this.lblPassword.TabIndex = 8;
      this.lblPassword.Text = "Proxy Password";
      this.lblLogin.AutoSize = true;
      this.lblLogin.Location = new Point(23, 36);
      this.lblLogin.Name = "lblLogin";
      this.lblLogin.Size = new Size(62, 13);
      this.lblLogin.TabIndex = 20;
      this.lblLogin.Text = "Proxy Login";
      this.txtLogin.Location = new Point(25, 52);
      this.txtLogin.Name = "txtLogin";
      this.txtLogin.Size = new Size(210, 20);
      this.txtLogin.TabIndex = 0;
      this.txtPassword.Location = new Point(25, 95);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new Size(210, 20);
      this.txtPassword.TabIndex = 1;
      this.AcceptButton = (IButtonControl) this.btnOK;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.White;
      this.CancelButton = (IButtonControl) this.btnCancel;
      this.ClientSize = new Size(261, 177);
      this.Controls.Add((Control) this.pnlMainContainer);
      this.Controls.Add((Control) this.pnlControl);
      this.Controls.Add((Control) this.pnlLblHistory);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MaximumSize = new Size(267, 205);
      this.MinimizeBox = false;
      this.Name = nameof (frmProxyAuthentication);
      this.ShowIcon = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Connection";
      this.TopMost = true;
      this.Load += new EventHandler(this.frmProxyAuthentication_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmProxyAuthentication_FormClosing);
      this.pnlLblHistory.ResumeLayout(false);
      this.pnlControl.ResumeLayout(false);
      this.pnlMainContainer.ResumeLayout(false);
      this.pnlMainContainer.PerformLayout();
      this.ResumeLayout(false);
    }

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

    private void frmProxyAuthentication_Load(object sender, EventArgs e)
    {
      try
      {
        this.txtLogin.Text = Settings.Default.appProxyLogin;
        this.txtPassword.Text = Settings.Default.appProxyPassword;
      }
      catch (Exception ex)
      {
      }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      try
      {
        this.bNormalClosed = true;
        if (this.txtLogin.Text.Equals(string.Empty))
        {
          int num1 = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM003) Proxy login is missing", "(W-CVC-WM003)_PROXY_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else if (this.txtPassword.Text.Equals(string.Empty))
        {
          int num2 = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM004) Proxy password is missing", "(W-CVC-WM004)_PASSWORD_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          this.SaveSettings();
          this.WriteUserSettingINI();
          this.Close();
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Dashbord.bShowProxy = false;
      Program.bShowProxyScreen = false;
      this.Close();
    }

    private void frmProxyAuthentication_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.bNormalClosed)
        return;
      Dashbord.bShowProxy = false;
      Program.bShowProxyScreen = false;
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
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

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='PROXYAUTHENTICATION']";
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
            return this.GetResourceValue(sDefaultValue, xQuery1);
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
        return this.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    public string GetResourceValue(string sDefaultValue, string xQuery)
    {
      try
      {
        XmlNode xmlNode = this.xmlDocument.SelectSingleNode("/GSPcLocalViewer" + xQuery);
        if (xmlNode == null || this.xmlDocument == null || (xmlNode.Attributes.Count <= 0 || xmlNode.Attributes["Value"] == null))
          return sDefaultValue;
        string str = xmlNode.Attributes["Value"].Value;
        if (!string.IsNullOrEmpty(str))
          return str;
        return sDefaultValue;
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    public void SaveSettings()
    {
      try
      {
        Settings.Default.appProxyPassword = this.txtPassword.Text;
        Settings.Default.appProxyLogin = this.txtLogin.Text;
      }
      catch (Exception ex)
      {
      }
    }

    public void LoadXMLFirstTime()
    {
      this.xmlDocument = (XmlDocument) null;
      try
      {
        if (Settings.Default.appLanguage != null || Settings.Default.appLanguage.Length != 0)
        {
          this.GetOSLanguage();
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            try
            {
              if (File.Exists(files[index]))
              {
                int num1 = files[index].IndexOf(".xml");
                int num2 = files[index].LastIndexOf("\\");
                string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                string str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value;
                if (str3.Length != 0)
                {
                  if (str3 != null)
                  {
                    if (str3 == Settings.Default.appLanguage)
                    {
                      if (File.Exists(str2))
                      {
                        this.xmlDocument = new XmlDocument();
                        this.xmlDocument.Load(str2);
                        break;
                      }
                    }
                  }
                }
              }
            }
            catch
            {
            }
          }
        }
        else
        {
          if (Settings.Default.appLanguage != null)
          {
            if (Settings.Default.appLanguage.Length != 0)
              goto label_27;
          }
          Settings.Default.appLanguage = "English";
          string osLanguage = this.GetOSLanguage();
          string[] files = Directory.GetFiles(Application.StartupPath + "\\Language XMLs\\", "*.xml");
          for (int index = 0; index < files.Length; ++index)
          {
            try
            {
              if (File.Exists(files[index]))
              {
                int num1 = files[index].IndexOf(".xml");
                int num2 = files[index].LastIndexOf("\\");
                string str1 = files[index].Substring(num2 + 1, num1 - num2 - 1);
                string str2 = Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Application.StartupPath + "\\Language XMLs\\" + str1 + ".xml");
                string str3 = xmlDocument.SelectSingleNode("//GSPcLocalViewer").Attributes["EN_NAME"].Value;
                if (str3 == osLanguage)
                {
                  if (File.Exists(str2))
                  {
                    this.xmlDocument.Load(str2);
                    break;
                  }
                }
                else if (str3 == Settings.Default.appLanguage)
                {
                  if (File.Exists(str2))
                  {
                    this.xmlDocument.Load(str2);
                    break;
                  }
                }
              }
            }
            catch
            {
            }
          }
        }
      }
      catch
      {
      }
label_27:
      if (this.xmlDocument != null)
        return;
      this.xmlDocument = new XmlDocument();
      Settings.Default.appLanguage = "English";
    }

    public string GetOSLanguage()
    {
      CultureInfo.CurrentCulture.ClearCachedData();
      return CultureInfo.CurrentCulture.DisplayName.Split(' ')[0].ToString();
    }

    private bool IsValidDateTime(string strInput)
    {
      try
      {
        DateTime.ParseExact(strInput.Trim(), "dd/MM/yyyy HH:mm", (IFormatProvider) CultureInfo.InvariantCulture);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private void WriteUserSettingINI()
    {
      try
      {
        string empty = string.Empty;
        string FileName = Application.StartupPath + "\\UserSet.ini";
        IniFileIO iniFileIo = new IniFileIO();
        iniFileIo.WriteValue("PROXY_SETTINGS", "USERNAME", Settings.Default.appProxyLogin.ToString(), FileName);
        iniFileIo.WriteValue("PROXY_SETTINGS", "PASSWORD", Settings.Default.appProxyPassword.ToString(), FileName);
      }
      catch
      {
      }
    }
  }
}

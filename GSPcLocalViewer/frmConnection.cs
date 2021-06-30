// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmConnection
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

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
        this.txtLogin.Text = Settings.Default.appProxyLogin;
      if (!Settings.Default.appProxyPassword.Equals(string.Empty))
        this.txtPassword.Text = Settings.Default.appProxyPassword;
      if (!Settings.Default.appProxyPort.Equals(string.Empty))
        this.txtPort.Text = Settings.Default.appProxyPort;
      if (!Settings.Default.appProxyIP.Equals(string.Empty))
        this.txtIP.Text = Settings.Default.appProxyIP;
      if (!Settings.Default.appProxyTimeOut.Equals(string.Empty))
        this.listBoxSeconds.Text = Settings.Default.appProxyTimeOut;
      if (Settings.Default.appProxyType.Equals("0"))
        this.rdoType0.Checked = true;
      else if (Settings.Default.appProxyType.Equals("1"))
        this.rdoType1.Checked = true;
      else if (Settings.Default.appProxyType.Equals("2"))
      {
        this.rdoType2.Checked = true;
      }
      else
      {
        if (!Settings.Default.appProxyType.Equals("3"))
          return;
        this.rdoType3.Checked = true;
      }
    }

    private void frmConnection_Load(object sender, EventArgs e)
    {
      this.LoadUserSettings();
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }

    public void SaveSettings()
    {
      if (this.rdoType0.Checked)
        Settings.Default.appProxyType = "0";
      if (this.rdoType1.Checked)
        Settings.Default.appProxyType = "1";
      if (this.rdoType2.Checked)
        Settings.Default.appProxyType = "2";
      if (this.rdoType3.Checked)
        Settings.Default.appProxyType = "3";
      Settings.Default.appProxyPort = this.txtPort.Text;
      Settings.Default.appProxyPassword = this.txtPassword.Text;
      Settings.Default.appProxyTimeOut = this.listBoxSeconds.Text;
      Settings.Default.appProxyIP = this.txtIP.Text;
      Settings.Default.appProxyLogin = this.txtLogin.Text;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (this.rdoType0.Checked)
        this.SaveSettings();
      else if (this.rdoType2.Checked)
      {
        if (this.txtIP.Text.Equals(string.Empty))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM002) Proxy address is missing", "(W-CVC-WM002)_ADDRESS_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (!this.IsValidIP(this.txtIP.Text))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM001) Proxy address is not valid", "(W-CVC-WM001)_INVALID", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        this.SaveSettings();
      }
      else if (this.rdoType1.Checked)
      {
        if (this.txtIP.Text.Equals(string.Empty))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM002) Proxy address is missing", "(W-CVC-WM002)_ADDRESS_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (!this.IsValidIP(this.txtIP.Text))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM001) Proxy address is not valid", "(W-CVC-WM001)_INVALID", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        this.SaveSettings();
      }
      else if (this.rdoType3.Checked)
      {
        if (this.txtIP.Text.Equals(string.Empty))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM002) Proxy address is missing", "(W-CVC-WM002)_ADDRESS_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (!this.IsValidIP(this.txtIP.Text))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM001) Proxy address is not valid", "(W-CVC-WM001)_INVALID", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (this.txtLogin.Text.Equals(string.Empty))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM003) Proxy login is missing", "(W-CVC-WM003)_PROXY_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (this.txtPassword.Text.Equals(string.Empty))
        {
          int num = (int) MessageHandler.ShowMessage((IWin32Window) this, this.GetResource("(W-CVC-WM004) Proxy password is missing", "(W-CVC-WM004)_PASSWORD_MISSING", ResourceType.POPUP_MESSAGE), this.frmParent.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        this.SaveSettings();
      }
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    public bool IsValidIP(string addr)
    {
      Regex regex = new Regex("^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");
      return !(addr == "") && regex.IsMatch(addr, 0);
    }

    private void rdoType0_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rdoType0.Checked)
        return;
      this.txtIP.Enabled = this.txtLogin.Enabled = this.txtPassword.Enabled = this.txtPort.Enabled = false;
    }

    private void rdoType3_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rdoType3.Checked)
        return;
      this.txtIP.Enabled = this.txtLogin.Enabled = this.txtPassword.Enabled = this.txtPort.Enabled = true;
    }

    private void rdoType1_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rdoType1.Checked)
        return;
      this.txtIP.Enabled = this.txtLogin.Enabled = this.txtPassword.Enabled = this.txtPort.Enabled = true;
    }

    private void rdoType2_CheckedChanged(object sender, EventArgs e)
    {
      if (!this.rdoType2.Checked)
        return;
      this.txtLogin.Enabled = this.txtPassword.Enabled = false;
      this.txtIP.Enabled = this.txtPort.Enabled = true;
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

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='CONNECTION']";
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

    private void frmConnection_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.SaveUserSettings();
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      if (this.Owner.GetType() != typeof (frmViewer))
        return;
      this.frmParent.HideDimmer();
    }

    private void LoadUserSettings()
    {
      if (Settings.Default.frmConnectionLocation == new Point(0, 0))
        this.StartPosition = FormStartPosition.CenterScreen;
      else
        this.Location = Settings.Default.frmConnectionLocation;
    }

    private void SaveUserSettings()
    {
      if (this.WindowState == FormWindowState.Normal)
        Settings.Default.frmConnectionLocation = this.Location;
      else
        Settings.Default.frmConnectionLocation = this.RestoreBounds.Location;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
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
      this.SuspendLayout();
      this.pnlProxyServer.AutoScroll = true;
      this.pnlProxyServer.Controls.Add((Control) this.panel5);
      this.pnlProxyServer.Controls.Add((Control) this.panel4);
      this.pnlProxyServer.Controls.Add((Control) this.panel3);
      this.pnlProxyServer.Controls.Add((Control) this.panel2);
      this.pnlProxyServer.Controls.Add((Control) this.panel1);
      this.pnlProxyServer.Controls.Add((Control) this.pnlLblHistory);
      this.pnlProxyServer.Dock = DockStyle.Fill;
      this.pnlProxyServer.Location = new Point(0, 0);
      this.pnlProxyServer.Name = "pnlProxyServer";
      this.pnlProxyServer.Size = new Size(407, 458);
      this.pnlProxyServer.TabIndex = 23;
      this.panel5.Controls.Add((Control) this.lblIp);
      this.panel5.Controls.Add((Control) this.lblPassword);
      this.panel5.Controls.Add((Control) this.lblLogin);
      this.panel5.Controls.Add((Control) this.txtIP);
      this.panel5.Controls.Add((Control) this.txtLogin);
      this.panel5.Controls.Add((Control) this.txtPort);
      this.panel5.Controls.Add((Control) this.txtPassword);
      this.panel5.Controls.Add((Control) this.lblPort);
      this.panel5.Dock = DockStyle.Fill;
      this.panel5.Location = new Point(0, 240);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(407, 218);
      this.panel5.TabIndex = 38;
      this.lblIp.AutoSize = true;
      this.lblIp.Location = new Point(39, 20);
      this.lblIp.Name = "lblIp";
      this.lblIp.Size = new Size(74, 13);
      this.lblIp.TabIndex = 20;
      this.lblIp.Text = "Proxy Address";
      this.lblPassword.AutoSize = true;
      this.lblPassword.Location = new Point(39, 149);
      this.lblPassword.Name = "lblPassword";
      this.lblPassword.Size = new Size(82, 13);
      this.lblPassword.TabIndex = 8;
      this.lblPassword.Text = "Proxy Password";
      this.lblLogin.AutoSize = true;
      this.lblLogin.Location = new Point(39, 106);
      this.lblLogin.Name = "lblLogin";
      this.lblLogin.Size = new Size(62, 13);
      this.lblLogin.TabIndex = 20;
      this.lblLogin.Text = "Proxy Login";
      this.txtIP.Location = new Point(42, 40);
      this.txtIP.Name = "txtIP";
      this.txtIP.Size = new Size(277, 20);
      this.txtIP.TabIndex = 4;
      this.txtLogin.Location = new Point(42, 126);
      this.txtLogin.Name = "txtLogin";
      this.txtLogin.Size = new Size(277, 20);
      this.txtLogin.TabIndex = 6;
      this.txtPort.Location = new Point(42, 83);
      this.txtPort.Name = "txtPort";
      this.txtPort.Size = new Size(277, 20);
      this.txtPort.TabIndex = 5;
      this.txtPassword.Location = new Point(42, 169);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new Size(277, 20);
      this.txtPassword.TabIndex = 7;
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new Point(39, 63);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new Size(55, 13);
      this.lblPort.TabIndex = 32;
      this.lblPort.Text = "Proxy Port";
      this.panel4.Controls.Add((Control) this.label2);
      this.panel4.Dock = DockStyle.Top;
      this.panel4.Location = new Point(0, 212);
      this.panel4.Name = "panel4";
      this.panel4.Padding = new Padding(7, 0, 15, 0);
      this.panel4.Size = new Size(407, 28);
      this.panel4.TabIndex = 37;
      this.label2.BackColor = Color.Transparent;
      this.label2.Dock = DockStyle.Fill;
      this.label2.ForeColor = Color.Blue;
      this.label2.Image = (Image) Resources.GroupLine0;
      this.label2.ImageAlign = ContentAlignment.MiddleLeft;
      this.label2.Location = new Point(7, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(385, 28);
      this.label2.TabIndex = 15;
      this.label2.Tag = (object) "";
      this.label2.TextAlign = ContentAlignment.MiddleLeft;
      this.panel3.Controls.Add((Control) this.lblConnTimeOut);
      this.panel3.Controls.Add((Control) this.listBoxSeconds);
      this.panel3.Controls.Add((Control) this.lblSeconds);
      this.panel3.Dock = DockStyle.Top;
      this.panel3.Location = new Point(0, 163);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(407, 49);
      this.panel3.TabIndex = 36;
      this.lblConnTimeOut.AutoSize = true;
      this.lblConnTimeOut.Location = new Point(39, 18);
      this.lblConnTimeOut.Name = "lblConnTimeOut";
      this.lblConnTimeOut.Size = new Size(116, 13);
      this.lblConnTimeOut.TabIndex = 23;
      this.lblConnTimeOut.Text = "Connection TimeOut = ";
      this.listBoxSeconds.DropDownStyle = ComboBoxStyle.DropDownList;
      this.listBoxSeconds.FormattingEnabled = true;
      this.listBoxSeconds.Items.AddRange(new object[120]
      {
        (object) "1",
        (object) "2",
        (object) "3",
        (object) "4",
        (object) "5",
        (object) "6",
        (object) "7",
        (object) "8",
        (object) "9",
        (object) "10",
        (object) "11",
        (object) "12",
        (object) "13",
        (object) "14",
        (object) "15",
        (object) "16",
        (object) "17",
        (object) "18",
        (object) "19",
        (object) "20",
        (object) "21",
        (object) "22",
        (object) "23",
        (object) "24",
        (object) "25",
        (object) "26",
        (object) "27",
        (object) "28",
        (object) "29",
        (object) "30",
        (object) "31",
        (object) "32",
        (object) "33",
        (object) "34",
        (object) "35",
        (object) "36",
        (object) "37",
        (object) "38",
        (object) "39",
        (object) "40",
        (object) "41",
        (object) "42",
        (object) "43",
        (object) "44",
        (object) "45",
        (object) "46",
        (object) "47",
        (object) "48",
        (object) "49",
        (object) "50",
        (object) "51",
        (object) "52",
        (object) "53",
        (object) "54",
        (object) "55",
        (object) "56",
        (object) "57",
        (object) "58",
        (object) "59",
        (object) "60",
        (object) "61",
        (object) "62",
        (object) "63",
        (object) "64",
        (object) "65",
        (object) "66",
        (object) "67",
        (object) "68",
        (object) "69",
        (object) "70",
        (object) "71",
        (object) "72",
        (object) "73",
        (object) "74",
        (object) "75",
        (object) "76",
        (object) "77",
        (object) "78",
        (object) "79",
        (object) "80",
        (object) "81",
        (object) "82",
        (object) "83",
        (object) "84",
        (object) "85",
        (object) "86",
        (object) "87",
        (object) "88",
        (object) "89",
        (object) "90",
        (object) "91",
        (object) "92",
        (object) "93",
        (object) "94",
        (object) "95",
        (object) "96",
        (object) "97",
        (object) "98",
        (object) "99",
        (object) "100",
        (object) "101",
        (object) "102",
        (object) "103",
        (object) "104",
        (object) "105",
        (object) "106",
        (object) "107",
        (object) "108",
        (object) "109",
        (object) "110",
        (object) "111",
        (object) "112",
        (object) "113",
        (object) "114",
        (object) "115",
        (object) "116",
        (object) "117",
        (object) "118",
        (object) "119",
        (object) "120"
      });
      this.listBoxSeconds.Location = new Point(158, 15);
      this.listBoxSeconds.Name = "listBoxSeconds";
      this.listBoxSeconds.Size = new Size(65, 21);
      this.listBoxSeconds.TabIndex = 3;
      this.lblSeconds.AutoSize = true;
      this.lblSeconds.Location = new Point(229, 18);
      this.lblSeconds.Name = "lblSeconds";
      this.lblSeconds.Size = new Size(50, 13);
      this.lblSeconds.TabIndex = 25;
      this.lblSeconds.Text = "seconds.";
      this.panel2.Controls.Add((Control) this.label1);
      this.panel2.Dock = DockStyle.Top;
      this.panel2.Location = new Point(0, 135);
      this.panel2.Name = "panel2";
      this.panel2.Padding = new Padding(7, 0, 15, 0);
      this.panel2.Size = new Size(407, 28);
      this.panel2.TabIndex = 35;
      this.label1.BackColor = Color.Transparent;
      this.label1.Dock = DockStyle.Fill;
      this.label1.ForeColor = Color.Blue;
      this.label1.Image = (Image) Resources.GroupLine0;
      this.label1.ImageAlign = ContentAlignment.MiddleLeft;
      this.label1.Location = new Point(7, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(385, 28);
      this.label1.TabIndex = 15;
      this.label1.Tag = (object) "";
      this.label1.TextAlign = ContentAlignment.MiddleLeft;
      this.panel1.Controls.Add((Control) this.rdoType0);
      this.panel1.Controls.Add((Control) this.rdoType1);
      this.panel1.Controls.Add((Control) this.rdoType2);
      this.panel1.Controls.Add((Control) this.rdoType3);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 28);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(407, 107);
      this.panel1.TabIndex = 34;
      this.rdoType0.AutoSize = true;
      this.rdoType0.Location = new Point(42, 11);
      this.rdoType0.Name = "rdoType0";
      this.rdoType0.Size = new Size(214, 17);
      this.rdoType0.TabIndex = 0;
      this.rdoType0.TabStop = true;
      this.rdoType0.Text = "Don't use proxy server (connect directly)";
      this.rdoType0.UseVisualStyleBackColor = true;
      this.rdoType0.CheckedChanged += new EventHandler(this.rdoType0_CheckedChanged);
      this.rdoType1.AutoSize = true;
      this.rdoType1.Location = new Point(42, 34);
      this.rdoType1.Name = "rdoType1";
      this.rdoType1.Size = new Size(104, 17);
      this.rdoType1.TabIndex = 33;
      this.rdoType1.TabStop = true;
      this.rdoType1.Text = "Use HTTP proxy";
      this.rdoType1.UseVisualStyleBackColor = true;
      this.rdoType1.CheckedChanged += new EventHandler(this.rdoType1_CheckedChanged);
      this.rdoType2.AutoSize = true;
      this.rdoType2.Location = new Point(42, 57);
      this.rdoType2.Name = "rdoType2";
      this.rdoType2.Size = new Size(117, 17);
      this.rdoType2.TabIndex = 1;
      this.rdoType2.TabStop = true;
      this.rdoType2.Text = "Use SOCKS4 proxy";
      this.rdoType2.UseVisualStyleBackColor = true;
      this.rdoType2.CheckedChanged += new EventHandler(this.rdoType2_CheckedChanged);
      this.rdoType3.AutoSize = true;
      this.rdoType3.Location = new Point(42, 80);
      this.rdoType3.Name = "rdoType3";
      this.rdoType3.Size = new Size(117, 17);
      this.rdoType3.TabIndex = 2;
      this.rdoType3.TabStop = true;
      this.rdoType3.Text = "Use SOCKS5 proxy";
      this.rdoType3.UseVisualStyleBackColor = true;
      this.rdoType3.CheckedChanged += new EventHandler(this.rdoType3_CheckedChanged);
      this.pnlLblHistory.Controls.Add((Control) this.lblHistoryLine);
      this.pnlLblHistory.Controls.Add((Control) this.lblProxyServer);
      this.pnlLblHistory.Dock = DockStyle.Top;
      this.pnlLblHistory.Location = new Point(0, 0);
      this.pnlLblHistory.Name = "pnlLblHistory";
      this.pnlLblHistory.Padding = new Padding(7, 0, 15, 0);
      this.pnlLblHistory.Size = new Size(407, 28);
      this.pnlLblHistory.TabIndex = 16;
      this.lblHistoryLine.BackColor = Color.Transparent;
      this.lblHistoryLine.Dock = DockStyle.Fill;
      this.lblHistoryLine.ForeColor = Color.Blue;
      this.lblHistoryLine.Image = (Image) Resources.GroupLine0;
      this.lblHistoryLine.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblHistoryLine.Location = new Point(121, 0);
      this.lblHistoryLine.Name = "lblHistoryLine";
      this.lblHistoryLine.Size = new Size(271, 28);
      this.lblHistoryLine.TabIndex = 15;
      this.lblHistoryLine.Tag = (object) "";
      this.lblHistoryLine.TextAlign = ContentAlignment.MiddleLeft;
      this.lblProxyServer.BackColor = Color.Transparent;
      this.lblProxyServer.Dock = DockStyle.Left;
      this.lblProxyServer.ForeColor = Color.Blue;
      this.lblProxyServer.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblProxyServer.Location = new Point(7, 0);
      this.lblProxyServer.Name = "lblProxyServer";
      this.lblProxyServer.Size = new Size(114, 28);
      this.lblProxyServer.TabIndex = 13;
      this.lblProxyServer.Tag = (object) "";
      this.lblProxyServer.Text = "Proxy Server";
      this.lblProxyServer.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlControl.Controls.Add((Control) this.btnOK);
      this.pnlControl.Controls.Add((Control) this.btnCancel);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(0, 458);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 15, 4);
      this.pnlControl.Size = new Size(407, 31);
      this.pnlControl.TabIndex = 18;
      this.btnOK.Dock = DockStyle.Right;
      this.btnOK.Location = new Point(242, 4);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new Size(75, 23);
      this.btnOK.TabIndex = 0;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new EventHandler(this.btnOK_Click);
      this.btnCancel.Dock = DockStyle.Right;
      this.btnCancel.Location = new Point(317, 4);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlProxyServer);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(2, 2);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(409, 491);
      this.pnlForm.TabIndex = 20;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(413, 495);
      this.Controls.Add((Control) this.pnlForm);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(400, 520);
      this.Name = nameof (frmConnection);
      this.Padding = new Padding(2);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Connection";
      this.Load += new EventHandler(this.frmConnection_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmConnection_FormClosing);
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
      this.ResumeLayout(false);
    }
  }
}

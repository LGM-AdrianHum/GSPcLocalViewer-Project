// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmOpenBookSearch
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmOpenBookSearch : Form
  {
    private const string dllZipper = "ZIPPER.dll";
    private frmOpenBook frmParent;
    private string statusText;
    private int p_ServerId;
    private Download objDownloader;
    private bool bEncrypted;
    private bool bCompressed;
    private string sServerKey;
    private IContainer components;
    private SplitContainer pnlForm;
    private Panel pnltvSearch;
    private Label lblSearch;
    private Panel pnlDetails;
    private Panel pnlBookInfo;
    private Panel pnlrtbBookInfo;
    private RichTextBox rtbBookInfo;
    private Label lblBookInfo;
    private Panel pnlSearchResults;
    private Label lblDetails;
    private BackgroundWorker bgWorker;
    private Panel pnlSearchCriteria;
    private DataGridView dgvSearchResults;
    private Panel pnlSearch;
    private Panel pnlControl;
    private Button btnSearch;
    private CheckBox checkBoxExactMatch;
    private CheckBox checkBoxMatchCase;
    private LabledTextBox ltbBookId;
    private LabledTextBox ltbBookCode;
    private Label lblAdvancedSearch;
    private Panel pnlrtbNoDetails;
    private RichTextBox rtbNoDetails;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private BackgroundWorker bgLoader;
    private PictureBox picLoading;
    private CustomComboBox cmbServers;
    private DataGridViewTextBoxColumn dgvcBookCode;
    private DataGridViewTextBoxColumn dgvcBookId;
    private DataGridViewTextBoxColumn dgvcUpdateDate;

    [DllImport("user32.dll")]
    public static extern long HideCaret(IntPtr hwnd);

    public frmOpenBookSearch(frmOpenBook frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.statusText = this.GetResource("Open books by searching", "OPEN_BY_SEARCHING", ResourceType.STATUS_MESSAGE);
      this.lblAdvancedSearch.Image.Tag = (object) "Closed";
      this.cmbServers.Items.Add((object) new Global.ComboBoxItem("Select a server...", (object) null));
      this.cmbServers.SelectedItem = this.cmbServers.Items[0];
      this.p_ServerId = 0;
      this.bEncrypted = false;
      this.bCompressed = false;
      this.objDownloader = new Download(this.frmParent);
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmOpenBookSearch_VisibleChanged(object sender, EventArgs e)
    {
      this.UpdateStatus();
    }

    private void frmOpenBookSearch_Load(object sender, EventArgs e)
    {
      try
      {
        this.cmbServers.BeginUpdate();
        for (int index = 0; index < Program.iniServers.Length; ++index)
        {
          string contents = Program.iniServers[index].items["SETTINGS", "DISPLAY_NAME"];
          object tag = (object) index;
          if (contents != string.Empty)
            this.cmbServers.Items.Add((object) new Global.ComboBoxItem(contents, tag));
        }
        this.cmbServers.EndUpdate();
        try
        {
          if (this.cmbServers.Items.Count == 2)
          {
            this.cmbServers.SelectedIndex = 1;
            this.cmbServers.Enabled = false;
          }
        }
        catch
        {
        }
      }
      catch
      {
        this.Hide();
        MessageHandler.ShowQuestion(this.GetResource("(E-OBS-EM001) Failed to load specified object", "(E-OBS-EM001) Failed to load specified object", ResourceType.POPUP_MESSAGE));
        this.Show();
      }
      this.pnlrtbNoDetails.BringToFront();
      this.rtbNoDetails.Clear();
      this.rtbNoDetails.SelectionColor = Color.Gray;
      this.rtbNoDetails.SelectedText = this.GetResource("Search a book", "SEARCH_A_BOOK", ResourceType.LABEL);
      this.rtbBookInfo.Clear();
      this.rtbBookInfo.SelectionColor = Color.Gray;
      this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
    }

    private void lblAdvancedSearch_Click(object sender, EventArgs e)
    {
      if (this.lblAdvancedSearch.Image.Tag.ToString() == "Opened")
      {
        this.lblAdvancedSearch.Image = (Image) Resources.GroupLine3;
        this.lblAdvancedSearch.Image.Tag = (object) "Closed";
        foreach (Control control in (ArrangedElementCollection) this.pnlSearch.Controls)
        {
          if (control.GetType() == typeof (LabledTextBox) && control != this.ltbBookCode && control != this.ltbBookId)
            control.Hide();
        }
      }
      else
      {
        if (!(this.lblAdvancedSearch.Image.Tag.ToString() == "Closed"))
          return;
        this.lblAdvancedSearch.Image = (Image) Resources.GroupLine2;
        this.lblAdvancedSearch.Image.Tag = (object) "Opened";
        for (int index = 0; index < this.pnlSearch.Controls.Count; ++index)
        {
          Control control = this.pnlSearch.Controls[index];
          if (control.GetType() == typeof (LabledTextBox) && control != this.ltbBookCode && control != this.ltbBookId)
          {
            LabledTextBox ltbxCurField = (LabledTextBox) control;
            this.GetAdvSearchFiledTitleFrmServerINI(ltbxCurField, ltbxCurField._ID);
          }
        }
      }
    }

    private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.pnlrtbNoDetails.BringToFront();
        this.rtbNoDetails.Clear();
        this.rtbNoDetails.SelectionColor = Color.Gray;
        this.rtbNoDetails.SelectedText = this.GetResource("Search a book", "SEARCH_A_BOOK", ResourceType.LABEL);
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
        this.pnlSearch.SuspendLayout();
        if (((ListControl) sender).SelectedIndex > 0)
        {
          this.ShowHideSearchControls(true);
          this.RemoveAdvancedSearchControls();
          this.AddAdvancedSearchControls();
        }
        else
          this.ShowHideSearchControls(false);
        this.pnlSearch.ResumeLayout();
      }
      catch
      {
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      object[] objArray = (object[]) e.Argument;
      string str1 = (string) objArray[0];
      string str2 = (string) objArray[1];
      string surl1_1 = (string) objArray[0] + "DataUpdate.xml";
      string str3 = (string) objArray[1] + "\\DataUpdate.xml";
      Global.ComboBoxItem objComboBoxItem = (Global.ComboBoxItem) objArray[2];
      try
      {
        this.p_ServerId = !objComboBoxItem.Tag.ToString().Contains("::") ? int.Parse(objComboBoxItem.Tag.ToString()) : int.Parse(objComboBoxItem.Tag.ToString().Substring(0, objComboBoxItem.Tag.ToString().IndexOf("::")));
      }
      catch
      {
      }
      this.bCompressed = false;
      if (Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
        this.bCompressed = true;
      string surl1_2;
      string str4;
      if (this.bCompressed)
      {
        surl1_2 = str1 + "Series.zip";
        str4 = str2 + "\\Series.zip";
      }
      else
      {
        surl1_2 = str1 + "Series.xml";
        str4 = str2 + "\\Series.xml";
      }
      bool flag = false;
      if (!Program.objAppMode.bWorkingOffline)
      {
        this.statusText = this.GetResource("Downloading……", "DOWNLOADING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        int result = 0;
        if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
          result = 0;
        this.objDownloader.DownloadFile(surl1_1, str3);
        if (File.Exists(str4))
        {
          if (result == 0)
            flag = true;
          else if (result < 1000)
          {
            DateTime dtServer = Global.DataUpdateDate(str3);
            if (Global.IntervalElapsed(Global.GetLocalDateOfFile(str4, this.p_ServerId), dtServer, result))
              flag = true;
          }
        }
        else
          flag = true;
      }
      if (flag && !Program.objAppMode.bWorkingOffline)
      {
        this.statusText = this.GetResource("Loading……", "LOADING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (!this.objDownloader.DownloadFile(surl1_2, str4) && !this.frmParent.IsDisposed)
        {
          this.statusText = this.GetResource("(E-OBS-EM005) Specified information does not exist", "(E-OBS-EM005)_INFORMATION_NOTEXIST", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          e.Result = (object) false;
        }
      }
      if (File.Exists(str4))
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.GetResource("Finished Loading", "FINISHED_LOADING", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        if (this.LoadAdvanceSearch(str4, objComboBoxItem))
        {
          this.statusText = this.GetResource("Enter the search criteria", "ENTER_SEARCH_CRITERIA", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          e.Result = (object) true;
        }
        else
        {
          this.statusText = this.GetResource("(E-OBS-EM006) Failed to create file/folder specified", "(E-OBS-EM006)_FAILED_FOLDER", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          e.Result = (object) false;
        }
      }
      else
      {
        if (this.frmParent.IsDisposed)
          return;
        this.statusText = this.GetResource("(E-OBS-EM007) Specified information does not exist", "(E-OBS-EM007)_INFORMATION_NOTEXIST", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        e.Result = (object) false;
      }
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      bool result = (bool) e.Result;
      if (this.frmParent.IsDisposed)
        return;
      this.HideLoading(this.pnlSearchCriteria);
      if (result)
        return;
      this.pnlControl.Visible = false;
      this.pnlSearch.Visible = false;
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      Hashtable searchCriteria = this.GetSearchCriteria();
      if (searchCriteria.Count > 0)
      {
        this.pnlSearchCriteria.Enabled = false;
        this.ShowLoading(this.pnlSearchResults);
        this.bgLoader.RunWorkerAsync((object) new object[1]
        {
          (object) searchCriteria
        });
      }
      else
      {
        this.pnlrtbNoDetails.BringToFront();
        this.rtbNoDetails.Clear();
        this.rtbNoDetails.SelectionColor = Color.Gray;
        this.rtbNoDetails.SelectedText = this.GetResource("Enter the search criteria", "ENTER_SEARCH_CRITERIA", ResourceType.LABEL);
        this.rtbBookInfo.Clear();
        this.rtbBookInfo.SelectionColor = Color.Gray;
        this.rtbBookInfo.SelectedText = this.GetResource("Downloading……", "DOWNLOADING", ResourceType.LABEL);
      }
    }

    private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      Hashtable hsCriteria = (Hashtable) ((object[]) e.Argument)[0];
      string empty = string.Empty;
      string str;
      try
      {
        string s = this.GetSelectedNodeTag();
        if (s.Contains("::"))
          s = s.Substring(0, s.IndexOf("::"));
        int index = int.Parse(s);
        str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[index].sIniKey + "\\Series.xml";
      }
      catch
      {
        e.Result = (object) this.GetResource("(E-OBS-EM006) Failed to create file/folder specified", "(E-OBS-EM006)_FAILED_FOLDER", ResourceType.STATUS_MESSAGE);
        return;
      }
      if (File.Exists(str))
        e.Result = (object) this.LoadSearchResults(str, hsCriteria);
      else
        e.Result = (object) this.GetResource("(E-OBS-EM007) Specified information does not exist", "(E-OBS-EM007)_INFORMATION_NOTEXIST", ResourceType.STATUS_MESSAGE);
    }

    private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      string result = (string) e.Result;
      this.HideLoading(this.pnlSearchResults);
      this.pnlSearchCriteria.Enabled = true;
      if (!(result != "ok"))
        return;
      this.pnlrtbNoDetails.BringToFront();
      this.rtbNoDetails.Clear();
      this.rtbNoDetails.SelectionColor = Color.Red;
      this.rtbNoDetails.SelectedText = result;
    }

    private void dgvSearchResults_SelectionChanged(object sender, EventArgs e)
    {
      XmlDocument xmlDocument = new XmlDocument();
      string empty = string.Empty;
      this.rtbBookInfo.SelectionFont = this.rtbBookInfo.Font;
      if (this.dgvSearchResults.SelectedRows != null && this.dgvSearchResults.SelectedRows.Count != 0 && this.dgvSearchResults.Tag != null)
      {
        if (this.dgvSearchResults.SelectedRows[0].Tag != null)
        {
          try
          {
            XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(this.dgvSearchResults.Tag.ToString()));
            XmlNode xmlNode1 = xmlDocument.ReadNode((XmlReader) xmlTextReader1);
            XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(this.dgvSearchResults.SelectedRows[0].Tag.ToString()));
            XmlNode xmlNode2 = xmlDocument.ReadNode((XmlReader) xmlTextReader2);
            this.rtbBookInfo.Clear();
            for (int index = 0; index < xmlNode1.Attributes.Count; ++index)
            {
              if (!xmlNode1.Attributes[index].Value.ToUpper().StartsWith("LEVEL") && xmlNode2.Attributes[xmlNode1.Attributes[index].Name] != null && (xmlNode1.Attributes[index].Value.ToUpper() != "BOOKCODE" && xmlNode1.Attributes[index].Value.ToUpper() != "ID"))
              {
                this.rtbBookInfo.SelectionColor = Color.Gray;
                this.rtbBookInfo.SelectedText = this.GetLanguage(xmlNode1.Attributes[index].Value, this.sServerKey) + ": ";
                this.rtbBookInfo.SelectionColor = Color.Black;
                this.rtbBookInfo.SelectedText = xmlNode2.Attributes[xmlNode1.Attributes[index].Name].Value + "\n";
              }
            }
            return;
          }
          catch
          {
            this.rtbBookInfo.Clear();
            this.rtbBookInfo.SelectionColor = Color.Red;
            this.rtbBookInfo.SelectedText = this.GetResource("(E-OBS-EM011) Failed to load specified object", "(E-OBS-EM011)_FAILED_LOAD", ResourceType.LABEL);
            return;
          }
        }
      }
      this.rtbBookInfo.Clear();
      this.rtbBookInfo.SelectionColor = Color.Gray;
      this.rtbBookInfo.SelectedText = this.GetResource("Select a book", "SELECT_A_BOOK", ResourceType.LABEL);
    }

    private void lblBookInfo_Click(object sender, EventArgs e)
    {
      if (this.pnlrtbBookInfo.Visible)
      {
        this.pnlrtbBookInfo.Visible = false;
        this.pnlBookInfo.Height -= this.pnlrtbBookInfo.Height;
        this.lblBookInfo.Image = (Image) Resources.GroupLine3;
      }
      else
      {
        this.pnlBookInfo.Height += this.pnlrtbBookInfo.Height;
        this.pnlrtbBookInfo.Visible = true;
        this.lblBookInfo.Image = (Image) Resources.GroupLine2;
      }
    }

    private void dgvSearchResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      XmlDocument xmlDocument = new XmlDocument();
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      if (this.dgvSearchResults.Tag == null)
        return;
      if (this.dgvSearchResults.SelectedRows[0].Tag == null)
        return;
      try
      {
        string bookPublishingId = this.dgvSearchResults.SelectedRows[0].Cells["dgvcBookId"].Value.ToString();
        XmlTextReader xmlTextReader1 = new XmlTextReader((TextReader) new StringReader(this.dgvSearchResults.Tag.ToString()));
        XmlNode schemaNode = xmlDocument.ReadNode((XmlReader) xmlTextReader1);
        XmlTextReader xmlTextReader2 = new XmlTextReader((TextReader) new StringReader(this.dgvSearchResults.SelectedRows[0].Tag.ToString()));
        XmlNode bookNode = xmlDocument.ReadNode((XmlReader) xmlTextReader2);
        for (int index = 0; index < schemaNode.Attributes.Count; ++index)
        {
          if (schemaNode.Attributes[index].Value.ToUpper().ToString().Trim() == "BOOKTYPE")
          {
            empty3 = schemaNode.Attributes[index].Name.ToString();
            break;
          }
        }
        if (bookNode.Attributes[empty3] != null)
        {
          string bookType = bookNode.Attributes[empty3].Value.ToString();
          this.frmParent.CloseAndLoadData(this.p_ServerId, bookPublishingId, bookNode, schemaNode, bookType);
        }
        else
        {
          this.statusText = this.GetResource("E-OBS-EM008) Not in required format", "(E-OBS-EM008)_NOT_FORMAT", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          MessageHandler.ShowInformation(this.statusText);
        }
      }
      catch
      {
      }
    }

    private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
    {
      frmOpenBookSearch.HideCaret(this.rtbBookInfo.Handle);
    }

    private bool LoadSeries(ref XmlDocument xmlDoc, int iServerId, string sFilePath)
    {
      bool flag = true;
      this.bCompressed = false;
      this.bEncrypted = false;
      try
      {
        if (Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          this.bEncrypted = true;
        if (Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[iServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON")
          this.bCompressed = true;
        if (this.bCompressed)
        {
          string empty = string.Empty;
          Global.Unzip(sFilePath);
          string filename = sFilePath.ToLower().Replace(".zip", ".xml");
          xmlDoc.Load(filename);
        }
        else
          xmlDoc.Load(sFilePath);
        if (this.bEncrypted)
        {
          string str = new AES().Decode(xmlDoc.InnerText, "0123456789ABCDEF");
          xmlDoc.DocumentElement.InnerXml = str;
        }
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    private void ShowHideSearchControls(bool value)
    {
      this.pnlSearch.Visible = value;
      this.pnlControl.Visible = value;
    }

    private void RemoveAdvancedSearchControls()
    {
      try
      {
        for (int index = this.pnlSearch.Controls.Count - 1; index >= 0; --index)
        {
          if (this.pnlSearch.Controls[index].GetType() == typeof (LabledTextBox) && this.pnlSearch.Controls[index] != this.ltbBookCode && this.pnlSearch.Controls[index] != this.ltbBookId)
            this.pnlSearch.Controls.Remove(this.pnlSearch.Controls[index]);
        }
      }
      catch
      {
      }
    }

    private void AddAdvancedSearchControls()
    {
      Global.ComboBoxItem selectedItem = (Global.ComboBoxItem) this.cmbServers.SelectedItem;
      if (!selectedItem.Tag.ToString().Contains("::"))
      {
        string empty = string.Empty;
        string path = string.Empty;
        try
        {
          int index = int.Parse(selectedItem.Tag.ToString());
          empty = Program.iniServers[index].items["SETTINGS", "CONTENT_PATH"];
          if (!empty.EndsWith("/"))
            empty += "/";
          path = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
          path = path + "\\" + Program.iniServers[index].sIniKey;
          if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        }
        catch
        {
          MessageHandler.ShowError(this.GetResource("(E-OBS-EM002) Failed to create file/folder specified", "(E-OBS-EM002)_FAILED_FOLDER", ResourceType.POPUP_MESSAGE));
        }
        this.ShowLoading(this.pnlSearchCriteria);
        this.bgWorker.RunWorkerAsync((object) new object[3]
        {
          (object) empty,
          (object) path,
          (object) selectedItem
        });
      }
      else
      {
        this.statusText = !this.LoadAdvanceSearch(string.Empty, selectedItem) ? this.GetResource("(E-OBS-EM006) Failed to create file/folder specified", "(E-OBS-EM006)_FAILED_FOLDER", ResourceType.STATUS_MESSAGE) : this.GetResource("Enter the search criteria", "ENTER_SEARCH_CRITERIA", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        this.pnlControl.Visible = true;
        this.pnlSearch.Visible = true;
      }
    }

    private bool LoadAdvanceSearch(string sFilePath, Global.ComboBoxItem objComboBoxItem)
    {
      XmlDocument xmlDoc = new XmlDocument();
      XmlNode xmlNode;
      if (objComboBoxItem.Tag.ToString().Contains("::"))
      {
        try
        {
          this.p_ServerId = int.Parse(objComboBoxItem.Tag.ToString().Substring(0, objComboBoxItem.Tag.ToString().IndexOf("::")));
        }
        catch
        {
          return false;
        }
        try
        {
          string empty = string.Empty;
          string str = objComboBoxItem.Tag.ToString();
          XmlTextReader xmlTextReader = new XmlTextReader((TextReader) new StringReader(str.Substring(str.IndexOf("::") + 2, str.Length - (str.IndexOf("::") + 2))));
          xmlNode = xmlDoc.ReadNode((XmlReader) xmlTextReader);
        }
        catch
        {
          return false;
        }
      }
      else
      {
        if (!File.Exists(sFilePath))
          return false;
        try
        {
          this.p_ServerId = int.Parse(objComboBoxItem.Tag.ToString());
        }
        catch
        {
          return false;
        }
        try
        {
          this.LoadSeries(ref xmlDoc, this.p_ServerId, sFilePath);
          xmlNode = xmlDoc.SelectSingleNode("//Schema");
        }
        catch
        {
          return false;
        }
      }
      if (xmlNode == null || xmlNode.Attributes.Count == 0)
        return false;
      this.EnableDisableLabledTextBox(this.ltbBookCode, false);
      this.EnableDisableLabledTextBox(this.ltbBookId, false);
      int index = !objComboBoxItem.Tag.ToString().Contains("::") ? int.Parse(objComboBoxItem.Tag.ToString()) : int.Parse(objComboBoxItem.Tag.ToString().Substring(0, 1));
      this.sServerKey = Program.iniServers[index].sIniKey;
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode.Attributes)
      {
        if (!attribute.Value.ToUpper().StartsWith("LEVEL") && !attribute.Value.ToUpper().Equals("BOOKCODE") && !attribute.Value.ToUpper().Equals("PUBLISHINGID"))
          this.AddControl(this.pnlSearch, (Control) new LabledTextBox(attribute.Value)
          {
            _Name = attribute.Name,
            _Caption = this.GetLanguage(attribute.Value, this.sServerKey),
            _ID = this.GetLanguage(attribute.Value, this.sServerKey)
          });
        if (attribute.Value.ToUpper().Equals("BOOKCODE"))
        {
          this.EnableDisableLabledTextBox(this.ltbBookCode, true);
          this.ltbBookCode._Name = attribute.Name;
        }
        if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
        {
          this.EnableDisableLabledTextBox(this.ltbBookId, true);
          this.ltbBookId._Name = attribute.Name;
        }
      }
      if (!objComboBoxItem.Tag.ToString().Contains("::"))
        objComboBoxItem.Tag = (object) (objComboBoxItem.Tag.ToString() + "::" + xmlNode.OuterXml);
      return true;
    }

    private void EnableDisableLabledTextBox(LabledTextBox ltb, bool enable)
    {
      if (ltb.InvokeRequired)
        ltb.Invoke((Delegate) new frmOpenBookSearch.EnableDisableLabledTextBoxDelegate(this.EnableDisableLabledTextBox), (object) ltb, (object) enable);
      else
        ltb.Enabled = enable;
    }

    private void AddControl(Panel pnl, Control ctl)
    {
      if (pnl.InvokeRequired)
      {
        pnl.Invoke((Delegate) new frmOpenBookSearch.AddControlDelegate(this.AddControl), (object) pnl, (object) ctl);
      }
      else
      {
        ctl.Dock = DockStyle.Top;
        pnl.Controls.Add(ctl);
        ctl.BringToFront();
        if (!(this.lblAdvancedSearch.Image.Tag.ToString() == "Closed"))
          return;
        ctl.Hide();
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        this.cmbServers.Enabled = false;
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading)
            control.Visible = false;
        }
        this.picLoading.Parent = (Control) parentPanel;
        this.picLoading.BringToFront();
        this.picLoading.Show();
      }
      catch
      {
      }
    }

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        if (this.cmbServers.Items.Count != 2)
          this.cmbServers.Enabled = true;
        foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
        {
          if (control != this.picLoading)
            control.Visible = true;
        }
        this.picLoading.Hide();
        this.picLoading.Parent = (Control) this;
      }
      catch
      {
      }
    }

    private void UpdateStatus()
    {
      if (this != this.frmParent.GetCurrentChildForm())
        return;
      if (this.frmParent.InvokeRequired)
        this.frmParent.Invoke((Delegate) new frmOpenBookSearch.StatusDelegate(this.frmParent.UpdateStatus), (object) this.statusText);
      else
        this.frmParent.UpdateStatus(this.statusText);
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblSearch.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.lblDetails.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgvSearchResults.Font = Settings.Default.appFont;
      this.dgvSearchResults.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgvSearchResults.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
      foreach (Control control in (ArrangedElementCollection) this.pnlSearch.Controls)
      {
        if (control.GetType() == typeof (LabledTextBox))
          control.Font = Settings.Default.appFont;
      }
    }

    private Hashtable GetSearchCriteria()
    {
      Hashtable hashtable = new Hashtable();
      for (int index = this.pnlSearch.Controls.Count - 1; index >= 0; --index)
      {
        if (this.pnlSearch.Controls[index].GetType() == typeof (LabledTextBox) && ((LabledTextBox) this.pnlSearch.Controls[index])._Text.Trim() != string.Empty)
        {
          if (this.pnlSearch.Controls[index].Enabled)
          {
            try
            {
              hashtable.Add((object) ((LabledTextBox) this.pnlSearch.Controls[index])._Name, (object) ((LabledTextBox) this.pnlSearch.Controls[index])._Text.Trim());
            }
            catch
            {
            }
          }
        }
      }
      return hashtable;
    }

    private string GetSelectedNodeTag()
    {
      if (this.cmbServers.InvokeRequired)
        return (string) this.cmbServers.Invoke((Delegate) new frmOpenBookSearch.GetSelectedNodeTagDelegate(this.GetSelectedNodeTag));
      return ((Global.ComboBoxItem) this.cmbServers.SelectedItem).Tag.ToString();
    }

    private string LoadSearchResults(string sSeriesFile, Hashtable hsCriteria)
    {
      if (this.dgvSearchResults.InvokeRequired)
        return (string) this.dgvSearchResults.Invoke((Delegate) new frmOpenBookSearch.LoadSearchResultsDelegate(this.LoadSearchResults), (object) sSeriesFile, (object) hsCriteria);
      try
      {
        XmlDocument xmlDoc = new XmlDocument();
        string index1 = string.Empty;
        string index2 = string.Empty;
        string index3 = string.Empty;
        this.dgvSearchResults.Rows.Clear();
        if (!this.LoadSeries(ref xmlDoc, this.p_ServerId, sSeriesFile))
          return this.GetResource("(E-OBS-EM008) Not in required format", "(E-OBS-EM008)_NOT_FORMAT", ResourceType.STATUS_MESSAGE);
        XmlNode xSchemaNode = xmlDoc.SelectSingleNode("//Schema");
        if (xSchemaNode == null)
          return this.GetResource("(E-OBS-EM009) Failed to load specified object", "(E-OBS-EM009)_FAILED_LOAD", ResourceType.STATUS_MESSAGE);
        this.dgvSearchResults.Tag = (object) xSchemaNode.OuterXml;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("BOOKCODE"))
            index1 = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
            index2 = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
            index3 = attribute.Name;
        }
        string xpath = "//Book";
        IDictionaryEnumerator enumerator = hsCriteria.GetEnumerator();
        while (enumerator.MoveNext())
        {
          try
          {
            if (this.checkBoxMatchCase.Checked)
            {
              if (this.checkBoxExactMatch.Checked)
                xpath = xpath + "[@" + enumerator.Key.ToString() + "='" + enumerator.Value.ToString() + "']";
              else
                xpath = xpath + "[contains(@" + enumerator.Key.ToString() + ",'" + enumerator.Value.ToString() + "')]";
            }
            else if (this.checkBoxExactMatch.Checked)
              xpath = xpath + "[translate(@" + enumerator.Key.ToString() + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + enumerator.Value.ToString().ToUpper() + "']";
            else
              xpath = xpath + "[contains(translate(@" + enumerator.Key.ToString() + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'" + enumerator.Value.ToString().ToUpper() + "')]";
          }
          catch
          {
          }
        }
        XmlNodeList xNodeList = xmlDoc.SelectNodes(xpath);
        if (xNodeList == null || xNodeList.Count == 0)
          return "No results found";
        foreach (XmlNode filterBooks in new GSPcLocalViewer.Classes.Filter(this.frmParent.frmParent).FilterBooksList(xSchemaNode, xNodeList))
        {
          if (filterBooks.Attributes.Count > 0)
          {
            this.dgvSearchResults.Rows.Add();
            this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Selected = false;
            this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Tag = (object) filterBooks.OuterXml;
            this.dgvcBookCode.HeaderText = this.GetLanguage(this.dgvcBookCode.HeaderText, this.sServerKey);
            this.dgvcBookId.HeaderText = this.GetLanguage(this.dgvcBookId.HeaderText, this.sServerKey);
            this.dgvcUpdateDate.HeaderText = this.GetLanguage(this.dgvcUpdateDate.HeaderText, this.sServerKey);
            this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[0].Value = (object) filterBooks.Attributes[index1].Value;
            this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[1].Value = (object) filterBooks.Attributes[index2].Value;
            this.dgvSearchResults.Rows[this.dgvSearchResults.Rows.Count - 1].Cells[2].Value = (object) filterBooks.Attributes[index3].Value;
            Application.DoEvents();
          }
        }
        this.dgvSearchResults.BringToFront();
        return "ok";
      }
      catch
      {
        return this.GetResource("(E-OBS-EM010) Encountered problem while searching", "(E-OBS-EM010)_ENCOUNTERED_PROBLEM", ResourceType.STATUS_MESSAGE);
      }
    }

    public void LoadResources()
    {
      this.lblSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.LABEL);
      this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
      this.checkBoxMatchCase.Text = this.GetResource("Match Case", "EXACT_CASE", ResourceType.CHECK_BOX);
      this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
      this.ltbBookCode._Caption = this.GetResource("Book Code", "BOOK_CODE", ResourceType.LABEL);
      this.ltbBookId._Caption = this.GetResource("Publishing Id", "PUBLISHING_ID", ResourceType.LABEL);
      this.lblAdvancedSearch.Text = this.GetResource("Advanced Search", "ADVANCED_SEARCH", ResourceType.LABEL);
      this.lblBookInfo.Text = this.GetResource("Book Information", "BOOK_INFORMATION", ResourceType.LABEL);
      this.lblDetails.Text = this.GetResource("Details", "DETAILS", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='OPEN_BOOKS']" + "/Screen[@Name='OPENBOOKSEARCH']";
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
            return this.frmParent.frmParent.GetResourceValue(sDefaultValue, xQuery1);
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
        return this.frmParent.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private string GetLanguage(string sKey, string sServerKey)
    {
      bool flag = false;
      string str1 = Settings.Default.appLanguage + "_GSP_" + sServerKey + ".ini";
      if (File.Exists(Application.StartupPath + "\\Language XMLs\\" + str1))
      {
        TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str1);
        string str2;
        while ((str2 = textReader.ReadLine()) != null)
        {
          if (str2.ToUpper() == "[OPENBOOK]")
            flag = true;
          else if (str2.Contains("=") && flag)
          {
            string[] strArray = str2.Split(new string[1]
            {
              "="
            }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
              if (strArray[0].ToString().ToUpper() == sKey.ToUpper())
                return strArray[1].ToString();
            }
            catch
            {
              return sKey;
            }
          }
          else if (str2.Contains("["))
            flag = false;
        }
      }
      return sKey;
    }

    private void GetAdvSearchFiledTitleFrmServerINI(LabledTextBox ltbxCurField, string strKeyID)
    {
      try
      {
        IniFileIO iniFileIo = new IniFileIO();
        string keyValue1 = iniFileIo.GetKeyValue("OPENBOOK_ADVSEARCH_SETTINGS", strKeyID.ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.p_ServerId].sIniKey + ".ini");
        if (!string.IsNullOrEmpty(keyValue1))
        {
          if (Convert.ToBoolean(keyValue1.Split('|')[0]))
          {
            string appLanguage = Settings.Default.appLanguage;
            if (appLanguage.ToUpper() == "ENGLISH")
            {
              string str = keyValue1.Split('|')[1];
              ltbxCurField._Caption = str;
              ltbxCurField.Show();
            }
            else
            {
              string keyValue2 = iniFileIo.GetKeyValue("OPENBOOK_ADVSEARCH_SETTINGS", strKeyID.ToString(), Application.StartupPath + "\\Language XMLs\\" + appLanguage + "_GSP_" + Program.iniServers[this.p_ServerId].sIniKey + ".ini");
              ltbxCurField._Caption = keyValue2;
              ltbxCurField.Show();
            }
          }
          else
            ltbxCurField.Hide();
        }
        else
          ltbxCurField.Show();
      }
      catch (Exception ex)
      {
        ltbxCurField.Show();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlForm = new SplitContainer();
      this.pnlSearchCriteria = new Panel();
      this.pnlSearch = new Panel();
      this.lblAdvancedSearch = new Label();
      this.ltbBookId = new LabledTextBox();
      this.ltbBookCode = new LabledTextBox();
      this.pnlControl = new Panel();
      this.btnSearch = new Button();
      this.checkBoxExactMatch = new CheckBox();
      this.checkBoxMatchCase = new CheckBox();
      this.pnltvSearch = new Panel();
      this.cmbServers = new CustomComboBox();
      this.lblSearch = new Label();
      this.pnlDetails = new Panel();
      this.pnlSearchResults = new Panel();
      this.dgvSearchResults = new DataGridView();
      this.pnlrtbNoDetails = new Panel();
      this.rtbNoDetails = new RichTextBox();
      this.pnlBookInfo = new Panel();
      this.pnlrtbBookInfo = new Panel();
      this.rtbBookInfo = new RichTextBox();
      this.lblBookInfo = new Label();
      this.lblDetails = new Label();
      this.bgWorker = new BackgroundWorker();
      this.bgLoader = new BackgroundWorker();
      this.picLoading = new PictureBox();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dgvcBookCode = new DataGridViewTextBoxColumn();
      this.dgvcBookId = new DataGridViewTextBoxColumn();
      this.dgvcUpdateDate = new DataGridViewTextBoxColumn();
      this.pnlForm.Panel1.SuspendLayout();
      this.pnlForm.Panel2.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.pnlSearchCriteria.SuspendLayout();
      this.pnlSearch.SuspendLayout();
      this.pnlControl.SuspendLayout();
      this.pnltvSearch.SuspendLayout();
      this.pnlDetails.SuspendLayout();
      this.pnlSearchResults.SuspendLayout();
      ((ISupportInitialize) this.dgvSearchResults).BeginInit();
      this.pnlrtbNoDetails.SuspendLayout();
      this.pnlBookInfo.SuspendLayout();
      this.pnlrtbBookInfo.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Panel1.BackColor = SystemColors.Control;
      this.pnlForm.Panel1.Controls.Add((Control) this.pnlSearchCriteria);
      this.pnlForm.Panel1.Controls.Add((Control) this.pnltvSearch);
      this.pnlForm.Panel1.Controls.Add((Control) this.lblSearch);
      this.pnlForm.Panel1MinSize = 270;
      this.pnlForm.Panel2.Controls.Add((Control) this.pnlDetails);
      this.pnlForm.Panel2.Controls.Add((Control) this.lblDetails);
      this.pnlForm.Panel2MinSize = 75;
      this.pnlForm.Size = new Size(578, 409);
      this.pnlForm.SplitterDistance = 270;
      this.pnlForm.TabIndex = 18;
      this.pnlSearchCriteria.BackColor = Color.White;
      this.pnlSearchCriteria.Controls.Add((Control) this.pnlSearch);
      this.pnlSearchCriteria.Controls.Add((Control) this.pnlControl);
      this.pnlSearchCriteria.Dock = DockStyle.Fill;
      this.pnlSearchCriteria.Location = new Point(0, 70);
      this.pnlSearchCriteria.Name = "pnlSearchCriteria";
      this.pnlSearchCriteria.Size = new Size(268, 337);
      this.pnlSearchCriteria.TabIndex = 16;
      this.pnlSearch.AutoScroll = true;
      this.pnlSearch.BackColor = Color.White;
      this.pnlSearch.Controls.Add((Control) this.lblAdvancedSearch);
      this.pnlSearch.Controls.Add((Control) this.ltbBookId);
      this.pnlSearch.Controls.Add((Control) this.ltbBookCode);
      this.pnlSearch.Dock = DockStyle.Fill;
      this.pnlSearch.Location = new Point(0, 59);
      this.pnlSearch.Name = "pnlSearch";
      this.pnlSearch.Padding = new Padding(15, 0, 0, 0);
      this.pnlSearch.Size = new Size(268, 278);
      this.pnlSearch.TabIndex = 23;
      this.pnlSearch.Visible = false;
      this.lblAdvancedSearch.BackColor = Color.Transparent;
      this.lblAdvancedSearch.Cursor = Cursors.Hand;
      this.lblAdvancedSearch.Dock = DockStyle.Top;
      this.lblAdvancedSearch.ForeColor = Color.Blue;
      this.lblAdvancedSearch.Image = (Image) Resources.GroupLine3;
      this.lblAdvancedSearch.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblAdvancedSearch.Location = new Point(15, 48);
      this.lblAdvancedSearch.Name = "lblAdvancedSearch";
      this.lblAdvancedSearch.Size = new Size(253, 41);
      this.lblAdvancedSearch.TabIndex = 12;
      this.lblAdvancedSearch.Tag = (object) "";
      this.lblAdvancedSearch.Text = "Advanced Search";
      this.lblAdvancedSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdvancedSearch.Click += new EventHandler(this.lblAdvancedSearch_Click);
      this.ltbBookId._Caption = "Publishing Id";
      this.ltbBookId._Name = "PublishingID";
      this.ltbBookId._Text = "";
      this.ltbBookId.Dock = DockStyle.Top;
      this.ltbBookId.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.ltbBookId.Location = new Point(15, 24);
      this.ltbBookId.Name = "ltbBookId";
      this.ltbBookId.Size = new Size(253, 24);
      this.ltbBookId.TabIndex = 1;
      this.ltbBookCode._Caption = "Book Code";
      this.ltbBookCode._Name = "BookCode";
      this.ltbBookCode._Text = "";
      this.ltbBookCode.Dock = DockStyle.Top;
      this.ltbBookCode.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.ltbBookCode.Location = new Point(15, 0);
      this.ltbBookCode.Name = "ltbBookCode";
      this.ltbBookCode.Size = new Size(253, 24);
      this.ltbBookCode.TabIndex = 0;
      this.pnlControl.Controls.Add((Control) this.btnSearch);
      this.pnlControl.Controls.Add((Control) this.checkBoxExactMatch);
      this.pnlControl.Controls.Add((Control) this.checkBoxMatchCase);
      this.pnlControl.Dock = DockStyle.Top;
      this.pnlControl.Location = new Point(0, 0);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 15, 4);
      this.pnlControl.Size = new Size(268, 59);
      this.pnlControl.TabIndex = 22;
      this.pnlControl.Visible = false;
      this.btnSearch.Image = (Image) Resources.Search2;
      this.btnSearch.ImageAlign = ContentAlignment.MiddleRight;
      this.btnSearch.Location = new Point(145, 7);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new Size(80, 23);
      this.btnSearch.TabIndex = 1;
      this.btnSearch.Text = "Search";
      this.btnSearch.TextAlign = ContentAlignment.MiddleLeft;
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
      this.checkBoxExactMatch.AutoSize = true;
      this.checkBoxExactMatch.Location = new Point(15, 11);
      this.checkBoxExactMatch.Name = "checkBoxExactMatch";
      this.checkBoxExactMatch.Size = new Size(85, 17);
      this.checkBoxExactMatch.TabIndex = 4;
      this.checkBoxExactMatch.Text = "Exact Match";
      this.checkBoxExactMatch.UseVisualStyleBackColor = true;
      this.checkBoxMatchCase.AutoSize = true;
      this.checkBoxMatchCase.Location = new Point(15, 34);
      this.checkBoxMatchCase.Name = "checkBoxMatchCase";
      this.checkBoxMatchCase.Size = new Size(82, 17);
      this.checkBoxMatchCase.TabIndex = 3;
      this.checkBoxMatchCase.Text = "Match Case";
      this.checkBoxMatchCase.UseVisualStyleBackColor = true;
      this.pnltvSearch.BackColor = Color.White;
      this.pnltvSearch.Controls.Add((Control) this.cmbServers);
      this.pnltvSearch.Dock = DockStyle.Top;
      this.pnltvSearch.Location = new Point(0, 27);
      this.pnltvSearch.Name = "pnltvSearch";
      this.pnltvSearch.Padding = new Padding(15, 15, 15, 0);
      this.pnltvSearch.Size = new Size(268, 43);
      this.pnltvSearch.TabIndex = 15;
      this.cmbServers.DrawMode = DrawMode.OwnerDrawVariable;
      this.cmbServers.DropDownHeight = 150;
      this.cmbServers.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbServers.FormattingEnabled = true;
      this.cmbServers.IntegralHeight = false;
      this.cmbServers.Location = new Point(15, 15);
      this.cmbServers.Name = "cmbServers";
      this.cmbServers.Size = new Size(210, 22);
      this.cmbServers.TabIndex = 12;
      this.cmbServers.SelectedIndexChanged += new EventHandler(this.cmbServers_SelectedIndexChanged);
      this.lblSearch.BackColor = Color.White;
      this.lblSearch.Dock = DockStyle.Top;
      this.lblSearch.ForeColor = Color.Black;
      this.lblSearch.Location = new Point(0, 0);
      this.lblSearch.Name = "lblSearch";
      this.lblSearch.Padding = new Padding(3, 7, 0, 0);
      this.lblSearch.Size = new Size(268, 27);
      this.lblSearch.TabIndex = 14;
      this.lblSearch.Text = "Search";
      this.pnlDetails.BackColor = Color.White;
      this.pnlDetails.Controls.Add((Control) this.pnlSearchResults);
      this.pnlDetails.Controls.Add((Control) this.pnlBookInfo);
      this.pnlDetails.Dock = DockStyle.Fill;
      this.pnlDetails.Location = new Point(0, 27);
      this.pnlDetails.Name = "pnlDetails";
      this.pnlDetails.Size = new Size(302, 380);
      this.pnlDetails.TabIndex = 15;
      this.pnlSearchResults.BackColor = Color.White;
      this.pnlSearchResults.Controls.Add((Control) this.dgvSearchResults);
      this.pnlSearchResults.Controls.Add((Control) this.pnlrtbNoDetails);
      this.pnlSearchResults.Dock = DockStyle.Fill;
      this.pnlSearchResults.Location = new Point(0, 0);
      this.pnlSearchResults.Name = "pnlSearchResults";
      this.pnlSearchResults.Size = new Size(302, 206);
      this.pnlSearchResults.TabIndex = 13;
      this.pnlSearchResults.Tag = (object) "";
      this.dgvSearchResults.AllowUserToAddRows = false;
      this.dgvSearchResults.AllowUserToDeleteRows = false;
      this.dgvSearchResults.AllowUserToResizeRows = false;
      this.dgvSearchResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dgvSearchResults.BackgroundColor = Color.White;
      this.dgvSearchResults.BorderStyle = BorderStyle.None;
      this.dgvSearchResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvSearchResults.Columns.AddRange((DataGridViewColumn) this.dgvcBookCode, (DataGridViewColumn) this.dgvcBookId, (DataGridViewColumn) this.dgvcUpdateDate);
      this.dgvSearchResults.Dock = DockStyle.Fill;
      this.dgvSearchResults.Location = new Point(0, 0);
      this.dgvSearchResults.MultiSelect = false;
      this.dgvSearchResults.Name = "dgvSearchResults";
      this.dgvSearchResults.ReadOnly = true;
      this.dgvSearchResults.RowHeadersVisible = false;
      this.dgvSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvSearchResults.Size = new Size(302, 206);
      this.dgvSearchResults.TabIndex = 0;
      this.dgvSearchResults.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvSearchResults_CellDoubleClick);
      this.dgvSearchResults.SelectionChanged += new EventHandler(this.dgvSearchResults_SelectionChanged);
      this.pnlrtbNoDetails.BackColor = Color.White;
      this.pnlrtbNoDetails.Controls.Add((Control) this.rtbNoDetails);
      this.pnlrtbNoDetails.Dock = DockStyle.Fill;
      this.pnlrtbNoDetails.Location = new Point(0, 0);
      this.pnlrtbNoDetails.Name = "pnlrtbNoDetails";
      this.pnlrtbNoDetails.Padding = new Padding(25, 10, 0, 0);
      this.pnlrtbNoDetails.Size = new Size(302, 206);
      this.pnlrtbNoDetails.TabIndex = 16;
      this.pnlrtbNoDetails.Tag = (object) "";
      this.rtbNoDetails.BackColor = Color.White;
      this.rtbNoDetails.BorderStyle = BorderStyle.None;
      this.rtbNoDetails.Dock = DockStyle.Fill;
      this.rtbNoDetails.Location = new Point(25, 10);
      this.rtbNoDetails.Name = "rtbNoDetails";
      this.rtbNoDetails.ReadOnly = true;
      this.rtbNoDetails.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbNoDetails.Size = new Size(277, 196);
      this.rtbNoDetails.TabIndex = 13;
      this.rtbNoDetails.TabStop = false;
      this.rtbNoDetails.Text = "";
      this.pnlBookInfo.BackColor = Color.White;
      this.pnlBookInfo.Controls.Add((Control) this.pnlrtbBookInfo);
      this.pnlBookInfo.Controls.Add((Control) this.lblBookInfo);
      this.pnlBookInfo.Dock = DockStyle.Bottom;
      this.pnlBookInfo.Location = new Point(0, 206);
      this.pnlBookInfo.Name = "pnlBookInfo";
      this.pnlBookInfo.Padding = new Padding(15, 10, 0, 0);
      this.pnlBookInfo.Size = new Size(302, 174);
      this.pnlBookInfo.TabIndex = 14;
      this.pnlBookInfo.Tag = (object) "";
      this.pnlrtbBookInfo.BackColor = Color.White;
      this.pnlrtbBookInfo.Controls.Add((Control) this.rtbBookInfo);
      this.pnlrtbBookInfo.Dock = DockStyle.Fill;
      this.pnlrtbBookInfo.Location = new Point(15, 38);
      this.pnlrtbBookInfo.Name = "pnlrtbBookInfo";
      this.pnlrtbBookInfo.Padding = new Padding(10, 0, 0, 0);
      this.pnlrtbBookInfo.Size = new Size(287, 136);
      this.pnlrtbBookInfo.TabIndex = 15;
      this.rtbBookInfo.BackColor = Color.White;
      this.rtbBookInfo.BorderStyle = BorderStyle.None;
      this.rtbBookInfo.Dock = DockStyle.Fill;
      this.rtbBookInfo.Location = new Point(10, 0);
      this.rtbBookInfo.Name = "rtbBookInfo";
      this.rtbBookInfo.ReadOnly = true;
      this.rtbBookInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbBookInfo.Size = new Size(277, 136);
      this.rtbBookInfo.TabIndex = 12;
      this.rtbBookInfo.TabStop = false;
      this.rtbBookInfo.Text = "";
      this.rtbBookInfo.MouseDown += new MouseEventHandler(this.rtbBookInfo_MouseDown);
      this.lblBookInfo.BackColor = Color.Transparent;
      this.lblBookInfo.Cursor = Cursors.Hand;
      this.lblBookInfo.Dock = DockStyle.Top;
      this.lblBookInfo.ForeColor = Color.Blue;
      this.lblBookInfo.Image = (Image) Resources.GroupLine2;
      this.lblBookInfo.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblBookInfo.Location = new Point(15, 10);
      this.lblBookInfo.Name = "lblBookInfo";
      this.lblBookInfo.Size = new Size(287, 28);
      this.lblBookInfo.TabIndex = 11;
      this.lblBookInfo.Tag = (object) "";
      this.lblBookInfo.Text = "Book Information";
      this.lblBookInfo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblBookInfo.Click += new EventHandler(this.lblBookInfo_Click);
      this.lblDetails.BackColor = Color.White;
      this.lblDetails.Dock = DockStyle.Top;
      this.lblDetails.ForeColor = Color.Black;
      this.lblDetails.Location = new Point(0, 0);
      this.lblDetails.Name = "lblDetails";
      this.lblDetails.Padding = new Padding(3, 7, 0, 0);
      this.lblDetails.Size = new Size(302, 27);
      this.lblDetails.TabIndex = 14;
      this.lblDetails.Text = "Details";
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.bgLoader.WorkerSupportsCancellation = true;
      this.bgLoader.DoWork += new DoWorkEventHandler(this.bgLoader_DoWork);
      this.bgLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgLoader_RunWorkerCompleted);
      this.picLoading.BackColor = Color.Transparent;
      this.picLoading.Dock = DockStyle.Fill;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(0, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(578, 409);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 20;
      this.picLoading.TabStop = false;
      this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn1.HeaderText = "Book Publishing Id";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn2.HeaderText = "UpdateDate";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.HeaderText = "UpdateDate";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 107;
      this.dgvcBookCode.HeaderText = "BookCode";
      this.dgvcBookCode.Name = "dgvcBookCode";
      this.dgvcBookCode.ReadOnly = true;
      this.dgvcBookId.HeaderText = "PublishingId";
      this.dgvcBookId.Name = "dgvcBookId";
      this.dgvcBookId.ReadOnly = true;
      this.dgvcUpdateDate.HeaderText = "UpdateDate";
      this.dgvcUpdateDate.Name = "dgvcUpdateDate";
      this.dgvcUpdateDate.ReadOnly = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(578, 409);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.picLoading);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmOpenBookSearch);
      this.Text = nameof (frmOpenBookSearch);
      this.Load += new EventHandler(this.frmOpenBookSearch_Load);
      this.VisibleChanged += new EventHandler(this.frmOpenBookSearch_VisibleChanged);
      this.pnlForm.Panel1.ResumeLayout(false);
      this.pnlForm.Panel2.ResumeLayout(false);
      this.pnlForm.ResumeLayout(false);
      this.pnlSearchCriteria.ResumeLayout(false);
      this.pnlSearch.ResumeLayout(false);
      this.pnlControl.ResumeLayout(false);
      this.pnlControl.PerformLayout();
      this.pnltvSearch.ResumeLayout(false);
      this.pnlDetails.ResumeLayout(false);
      this.pnlSearchResults.ResumeLayout(false);
      ((ISupportInitialize) this.dgvSearchResults).EndInit();
      this.pnlrtbNoDetails.ResumeLayout(false);
      this.pnlBookInfo.ResumeLayout(false);
      this.pnlrtbBookInfo.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    private delegate void EnableDisableLabledTextBoxDelegate(LabledTextBox ltb, bool enable);

    private delegate void AddControlDelegate(Panel pnl, Control ctl);

    private delegate void StatusDelegate(string status);

    private delegate string GetSelectedNodeTagDelegate();

    private delegate string LoadSearchResultsDelegate(string sSeriesFile, Hashtable hsCriteria);
  }
}

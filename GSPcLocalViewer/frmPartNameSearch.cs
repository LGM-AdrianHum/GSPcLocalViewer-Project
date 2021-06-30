// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmPartNameSearch
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using System.Xml.Linq;

namespace GSPcLocalViewer
{
  public class frmPartNameSearch : Form
  {
    private string attPartsListItem = string.Empty;
    private string attPageIdItem = string.Empty;
    private string attPageNameItem = string.Empty;
    private string attPartNumber = string.Empty;
    private string attPartName = string.Empty;
    private frmViewer frmParent;
    private frmSearch frmContainer;
    private XmlNodeList xnlPartlist;
    private string statusText;
    private string searchString;
    private string attPartIdElement;
    private string attPartNameElement;
    private IContainer components;
    private Label lblPartName;
    private Panel pnlControl;
    private Button btnSearch;
    private Panel pnlForm;
    private DataGridView dgViewSearch;
    private Panel pnlSearch;
    private TextBox txtPartName;
    private CheckBox checkBoxExactMatch;
    private CheckBox checkBoxMatchCase;
    private BackgroundWorker bgWorker;
    private Panel pnlSearchResults;
    private PictureBox picLoading;
    private StatusStrip ssStatus;
    private ToolStripStatusLabel lblStatus;
    private Button btnClearHistory;
    private CheckBox checkBoxSearchWholeBook;

    private void frmPartNameSearch_Load(object sender, EventArgs e)
    {
      Program.ReadSearchHistory(this.frmParent.sSearchHistoryPath, "PARTNAMESEARCH", Program.objPartNameSearchHistroyCollection);
      this.txtPartName.AutoCompleteCustomSource = Program.objPartNameSearchHistroyCollection;
      this.txtPartName.Focus();
    }

    private void frmPartNameSearch_FormClosing(object sender, FormClosingEventArgs e)
    {
      Program.WriteSearchHistory(this.frmParent.sSearchHistoryPath, "PARTNAMESEARCH", Program.objPartNameSearchHistroyCollection);
    }

    private void frmPartSearch_FormClosed(object sender, FormClosedEventArgs e)
    {
      this.frmParent.Enabled = true;
    }

    private void txtPartName_TextChanged(object sender, EventArgs e)
    {
      if (this.txtPartName.Text.Trim().Equals(string.Empty))
      {
        this.btnSearch.Enabled = false;
      }
      else
      {
        this.btnSearch.Enabled = true;
        this.searchString = this.txtPartName.Text;
      }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
      this.GridViewClearRows();
      this.txtPartName.AutoCompleteCustomSource.Add(this.txtPartName.Text);
      this.ShowLoading(this.pnlSearchResults);
      this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
      this.bgWorker.RunWorkerAsync();
    }

    private void btnClearHistory_Click(object sender, EventArgs e)
    {
      this.txtPartName.AutoCompleteCustomSource.Clear();
    }

    private void frmPartNameSearch_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.txtPartName.Text.Trim() != string.Empty && e.KeyCode == Keys.Return)
        this.btnSearch_Click((object) null, (EventArgs) null);
      if (e.KeyCode != Keys.Escape)
        return;
      this.frmContainer.Close();
    }

    private void dgViewSearch_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      try
      {
        if (this.dgViewSearch.Rows.Count == 0 || e.RowIndex < 0)
          return;
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        if (this.dgViewSearch["PageId", this.dgViewSearch.CurrentRow.Index].Value != null)
          empty1 = this.dgViewSearch["PageId", this.dgViewSearch.CurrentRow.Index].Value.ToString();
        if (this.dgViewSearch["PartName", this.dgViewSearch.CurrentRow.Index].Value != null)
          empty2 = this.dgViewSearch["PartNumber", this.dgViewSearch.CurrentRow.Index].Value.ToString();
        this.frmParent.OpenSearch(Program.iniServers[this.frmParent.ServerId].sIniKey, this.frmParent.BookPublishingId, empty1, "1", "1", empty2);
        this.frmParent.Enabled = true;
        this.frmContainer.CloseContainer();
      }
      catch (Exception ex)
      {
      }
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      this.UpdateStatus();
      this.UpdadetControls();
      if (this.checkBoxSearchWholeBook.Checked)
      {
        if (this.SearchInBookSearchXML())
          return;
        this.SearchPartsListXMLs();
      }
      else
        this.SearchPartsListXMLs();
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.dgViewSearch.Dock = DockStyle.None;
      this.dgViewSearch.Dock = DockStyle.Fill;
      this.HideLoading(this.pnlSearchResults);
      this.UpdadetControls();
    }

    private void txtPartName_KeyDown(object sender, KeyEventArgs e)
    {
    }

    public frmPartNameSearch(frmViewer frm, frmSearch frmSearch)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.frmContainer = frmSearch;
      this.UpdateFont();
      this.InitializeSearchGrid();
      this.LoadDataGridView();
      this.LoadResources();
    }

    private void InitializeSearchGrid()
    {
      if (this.dgViewSearch.InvokeRequired)
      {
        this.dgViewSearch.Invoke((Delegate) new frmPartNameSearch.InitializeSearchGridDelegate(this.InitializeSearchGrid));
      }
      else
      {
        try
        {
          DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn1.Name = "FieldId";
          viewTextBoxColumn1.HeaderText = "Field ID";
          viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn1.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewTextBoxColumn1);
          DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn2.Name = "DisplayName";
          viewTextBoxColumn2.HeaderText = "Display Name";
          viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn2.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewTextBoxColumn2);
          DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
          viewTextBoxColumn3.Name = "Width";
          viewTextBoxColumn3.HeaderText = "Col Width";
          viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewTextBoxColumn3.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewTextBoxColumn3);
          DataGridViewComboBoxColumn viewComboBoxColumn = new DataGridViewComboBoxColumn();
          viewComboBoxColumn.Name = "Alignment";
          viewComboBoxColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
          viewComboBoxColumn.Width = 100;
          this.dgViewSearch.Columns.Add((DataGridViewColumn) viewComboBoxColumn);
        }
        catch
        {
        }
      }
    }

    private void LoadDataGridView()
    {
      if (this.dgViewSearch.InvokeRequired)
      {
        this.dgViewSearch.Invoke((Delegate) new frmPartNameSearch.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
      }
      else
      {
        this.dgViewSearch.Columns.Clear();
        ArrayList arrayList = new ArrayList();
        List<string> stringList = new List<string>();
        IniFileIO iniFileIo = new IniFileIO();
        arrayList = new ArrayList();
        ArrayList keys = iniFileIo.GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "PARTNAME_SEARCH");
        for (int index = 0; index < keys.Count; ++index)
        {
          try
          {
            Global.AddSearchCol(new IniFileIO().GetKeyValue("PARTNAME_SEARCH", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini"), keys[index].ToString(), "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
          }
          catch
          {
          }
        }
        if (keys.Count != 0)
          return;
        Global.AddSearchCol("true|Part Name|C|100", "PartName", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
        Global.AddSearchCol("true|Part Number|C|100", "PartNumber", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
        Global.AddSearchCol("true|Page Name|C|100", "PageName", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
        Global.AddSearchCol("true|Page Id|C|100", "PageId", "PARTNAME_SEARCH", this.frmParent.ServerId, ref this.dgViewSearch);
      }
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmPartNameSearch.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = false;
          }
          this.picLoading.Parent = (Control) parentPanel;
          this.picLoading.Left = parentPanel.Width / 2 - this.picLoading.Width / 2;
          this.picLoading.Top = parentPanel.Height / 2 - this.picLoading.Height / 2;
          this.picLoading.BringToFront();
          this.picLoading.Show();
        }
      }
      catch
      {
      }
    }

    private void HideLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmPartNameSearch.HideLoadingDelegate(this.HideLoading), (object) this.pnlForm);
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) parentPanel.Controls)
          {
            if (control != this.picLoading)
              control.Enabled = true;
          }
          this.picLoading.Hide();
        }
      }
      catch
      {
      }
    }

    private void GridViewClearRows()
    {
      if (this.dgViewSearch.InvokeRequired)
        this.dgViewSearch.Invoke((Delegate) new frmPartNameSearch.GridViewClearRowsDelegate(this.GridViewClearRows));
      else
        this.dgViewSearch.Rows.Clear();
    }

    private void GridViewAddRow(DataGridViewRow tempRow)
    {
      if (this.dgViewSearch.InvokeRequired)
        this.dgViewSearch.Invoke((Delegate) new frmPartNameSearch.GridViewAddRowDelegate(this.GridViewAddRow), (object) tempRow);
      else
        this.dgViewSearch.Rows.Add(tempRow);
    }

    private void UpdadetControls()
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
          this.Invoke((Delegate) new frmPartNameSearch.UpdadetControlsDelegate(this.UpdadetControls));
        else
          this.pnlForm.Enabled = !this.pnlForm.Enabled;
      }
      catch
      {
      }
    }

    private void LoadResources()
    {
      this.lblPartName.Text = this.GetResource("Part Name", "PART_NAME", ResourceType.LABEL);
      this.checkBoxMatchCase.Text = this.GetResource("Match Case", "MATCH_CASE", ResourceType.CHECK_BOX);
      this.checkBoxExactMatch.Text = this.GetResource("Exact Match", "EXACT_MATCH", ResourceType.CHECK_BOX);
      this.btnSearch.Text = this.GetResource("Search", "SEARCH", ResourceType.BUTTON);
      this.btnClearHistory.Text = this.GetResource("Clear Search History", "CLEAR_SEARCH_HISTORY", ResourceType.BUTTON);
      this.checkBoxSearchWholeBook.Text = this.GetResource("Search Whole Book", "SEARCH_WHOLE_BOOK", ResourceType.CHECK_BOX);
      this.Text = this.GetResource("Search", "PART_NAME_SEARCH", ResourceType.TITLE);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='SEARCH']" + "/Screen[@Name='PART_NAME_SEARCH']";
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

    private bool LoadSearchResultsInGrid(string sFilePath, string PageName, string PageId)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        string sDecryptedInnerXML = string.Empty;
        if (!File.Exists(sFilePath))
          return false;
        try
        {
          xmlDocument.Load(sFilePath);
        }
        catch
        {
          return false;
        }
        bool bEncrypted = false;
        if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          bEncrypted = true;
        if (bEncrypted)
        {
          sDecryptedInnerXML = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = sDecryptedInnerXML;
        }
        XmlNode xSchemaNode = xmlDocument.SelectSingleNode("//Schema");
        if (xSchemaNode == null)
          return false;
        this.attPartIdElement = string.Empty;
        this.attPartNameElement = string.Empty;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("ID"))
            this.attPartIdElement = attribute.Name;
          else if (attribute.Value.ToUpper().Equals("PARTNAME"))
            this.attPartNameElement = attribute.Name;
        }
        if (this.attPartIdElement == string.Empty || this.attPartNameElement == string.Empty)
          return false;
        bool hankakuZenkakuFlag = Settings.Default.HankakuZenkakuFlag;
        string[] strArray = this.frmContainer.ConvertStringWidth(this.searchString);
        XmlNodeList xNodeList;
        if (this.checkBoxMatchCase.Checked)
        {
          if (this.checkBoxExactMatch.Checked)
          {
            if (hankakuZenkakuFlag && strArray.Length == 2)
            {
              string xpath = "//Part[@" + this.attPartNameElement + "='" + strArray[0] + "' or @" + this.attPartNameElement + "='" + strArray[1] + "']";
              xNodeList = xmlDocument.SelectNodes(xpath);
            }
            else
              xNodeList = xmlDocument.SelectNodes("//Part[@" + this.attPartNameElement + "='" + this.searchString + "']");
          }
          else if (hankakuZenkakuFlag && strArray.Length == 2)
          {
            string xpath = "//Part[contains(@" + this.attPartNameElement + ",'" + strArray[0] + "') or contains(@" + this.attPartNameElement + ",'" + strArray[1] + "')]";
            xNodeList = xmlDocument.SelectNodes(xpath);
          }
          else
            xNodeList = xmlDocument.SelectNodes("//Part[contains(@" + this.attPartNameElement + ",'" + this.searchString + "')]");
        }
        else if (this.checkBoxExactMatch.Checked)
        {
          if (hankakuZenkakuFlag && strArray.Length == 2)
          {
            string xpath = "//Part[translate(@" + this.attPartNameElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + strArray[0].ToUpper() + "' or translate(@" + this.attPartNameElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + strArray[1].ToUpper() + "']";
            xNodeList = xmlDocument.SelectNodes(xpath);
          }
          else
          {
            xNodeList = xmlDocument.SelectNodes("//Part[translate(@" + this.attPartNameElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')='" + this.searchString.ToUpper() + "']");
            if (xNodeList == null || xNodeList.Count == 0)
              xNodeList = this.SearchWithLINQ(sFilePath, bEncrypted, sDecryptedInnerXML, this.attPartNameElement, "Part");
          }
        }
        else if (hankakuZenkakuFlag && strArray.Length == 2)
        {
          string xpath = "//Part[contains(translate(@" + this.attPartNameElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'" + strArray[0].ToUpper() + "') or contains(translate(@" + this.attPartNameElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'" + strArray[1].ToUpper() + "')]";
          xNodeList = xmlDocument.SelectNodes(xpath);
        }
        else
        {
          xNodeList = xmlDocument.SelectNodes("//Part[contains(translate(@" + this.attPartNameElement + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),'" + this.searchString.ToUpper() + "')]");
          if (xNodeList == null || xNodeList.Count == 0)
            xNodeList = this.SearchWithLINQ(sFilePath, bEncrypted, sDecryptedInnerXML, this.attPartNameElement, "Part");
        }
        foreach (XmlNode filterParts in new GSPcLocalViewer.Classes.Filter(this.frmParent).FilterPartsList(xSchemaNode, xNodeList))
        {
          if (filterParts.Attributes.Count > 0)
          {
            new XmlDocument().LoadXml(filterParts.OuterXml);
            DataGridViewRow tempRow = new DataGridViewRow();
            for (int index = 0; index < this.dgViewSearch.Columns.Count; ++index)
            {
              DataGridViewTextBoxCell gridViewTextBoxCell = new DataGridViewTextBoxCell();
              foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
              {
                try
                {
                  if (this.dgViewSearch.Columns[index].Name.ToUpper().Equals(attribute.Value.ToUpper()))
                  {
                    if (filterParts != null)
                    {
                      if (filterParts.Attributes[attribute.Name] != null)
                        gridViewTextBoxCell.Value = (object) filterParts.Attributes[attribute.Name].Value;
                    }
                  }
                }
                catch
                {
                }
              }
              try
              {
                if (this.dgViewSearch.Columns[index].Name.ToUpper().Equals("PAGENAME"))
                  gridViewTextBoxCell.Value = (object) PageName;
                else if (this.dgViewSearch.Columns[index].Name.ToUpper().Equals("PAGEID"))
                  gridViewTextBoxCell.Value = (object) PageId;
                tempRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell);
              }
              catch
              {
              }
            }
            if (!this.frmParent.lstFilteredPages.Contains(PageName))
              this.GridViewAddRow(tempRow);
            Application.DoEvents();
          }
        }
        return true;
      }
      catch
      {
        return false;
      }
    }

    private bool LoadSearchResultsInGridA(string sFilePath, bool bEncrypted, string attBookXmlPageIdElement, string attBookXmlPageNameElement, XmlDocument objBookXmlDoc)
    {
      try
      {
        XmlDocument xmlDocument = new XmlDocument();
        if (!File.Exists(sFilePath))
          return false;
        try
        {
          xmlDocument.Load(sFilePath);
        }
        catch
        {
          return false;
        }
        if (bEncrypted)
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        XmlNodeList xNodeList;
        if (this.checkBoxMatchCase.Checked)
        {
          if (this.checkBoxExactMatch.Checked)
            xNodeList = xmlDocument.SelectNodes("//Part[@" + this.attPartName + "=\"" + this.searchString + "\"]");
          else
            xNodeList = xmlDocument.SelectNodes("//Part[contains(@" + this.attPartName + ",\"" + this.searchString + "\")]");
        }
        else if (this.checkBoxExactMatch.Checked)
          xNodeList = xmlDocument.SelectNodes("//Part[translate(@" + this.attPartName + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ')=\"" + this.searchString.ToUpper() + "\"]");
        else
          xNodeList = xmlDocument.SelectNodes("//Part[contains(translate(@" + this.attPartName + ",'abcdefghijklmnopqrstuvwxyz','ABCDEFGHIJKLMNOPQRSTUVWXYZ'),\"" + this.searchString.ToUpper() + "\")]");
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode("//Schema");
        XmlNodeList xmlNodeList = new GSPcLocalViewer.Classes.Filter(this.frmParent).FilterPartsList(xmlNode1, xNodeList);
        string empty1 = string.Empty;
        foreach (XmlNode xmlNode2 in xmlNodeList)
        {
          if (xmlNode2.Attributes.Count > 0)
          {
            DataGridViewRow tempRow = new DataGridViewRow();
            for (int index = 0; index < this.dgViewSearch.Columns.Count; ++index)
            {
              DataGridViewTextBoxCell gridViewTextBoxCell = new DataGridViewTextBoxCell();
              foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode2.Attributes)
              {
                string empty2 = string.Empty;
                if (xmlNode1.Attributes[attribute.Name] != null)
                {
                  string str = xmlNode1.Attributes[attribute.Name].Value;
                  if (this.dgViewSearch.Columns[index].Name.ToUpper() != "PAGEID")
                  {
                    if (this.dgViewSearch.Columns[index].Name.ToUpper().Equals(str.ToUpper()))
                      gridViewTextBoxCell.Value = (object) xmlNode2.Attributes[attribute.Name].Value;
                  }
                  else
                  {
                    try
                    {
                      XmlNode xmlNode3 = objBookXmlDoc.SelectSingleNode("//Pg[@" + attBookXmlPageNameElement + "=\"" + xmlNode2.Attributes[this.attPageNameItem].Value + "\"]");
                      if (xmlNode3 != null)
                      {
                        empty1 = xmlNode3.Attributes[attBookXmlPageNameElement].Value;
                        gridViewTextBoxCell.Value = (object) xmlNode3.Attributes[attBookXmlPageIdElement].Value;
                      }
                      else
                      {
                        XmlNode xndSchema = objBookXmlDoc.SelectSingleNode("//Schema");
                        this.attPageNameItem = this.GetAdvSearchMatchKeys(false, xmlNode1);
                        attBookXmlPageNameElement = this.GetAdvSearchMatchKeys(true, xndSchema);
                        XmlNode xmlNode4 = objBookXmlDoc.SelectSingleNode("//Pic[@" + attBookXmlPageNameElement + "=\"" + xmlNode2.Attributes[this.attPageNameItem].Value + "\"]");
                        if (xmlNode4 != null)
                          xmlNode3 = xmlNode4.ParentNode;
                        gridViewTextBoxCell.Value = (object) xmlNode3.Attributes[attBookXmlPageIdElement].Value;
                      }
                    }
                    catch
                    {
                    }
                  }
                }
              }
              tempRow.Cells.Add((DataGridViewCell) gridViewTextBoxCell);
            }
            if (!this.frmParent.lstFilteredPages.Contains(empty1))
              this.GridViewAddRow(tempRow);
            Application.DoEvents();
          }
        }
        return true;
      }
      catch
      {
        return false;
      }
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblPartName.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
      this.dgViewSearch.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
      this.dgViewSearch.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
    }

    private void UpdateStatus()
    {
      this.lblStatus.Text = this.statusText;
    }

    private XmlNodeList LoadPartsListNames(string BookXMLPath)
    {
      XmlDocument xmlDocument = new XmlDocument();
      if (!File.Exists(BookXMLPath))
        return (XmlNodeList) null;
      try
      {
        xmlDocument.Load(BookXMLPath);
      }
      catch
      {
        return (XmlNodeList) null;
      }
      bool flag = false;
      if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
        flag = true;
      if (flag)
      {
        string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
        xmlDocument.DocumentElement.InnerXml = str;
      }
      XmlNode xmlNode = xmlDocument.SelectSingleNode("//Schema");
      if (xmlNode == null)
        return (XmlNodeList) null;
      string str1 = string.Empty;
      string str2 = string.Empty;
      foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode.Attributes)
      {
        if (attribute.Value.ToUpper().Equals("ID"))
          str1 = attribute.Name;
        else if (attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
          str2 = attribute.Name;
      }
      if (str1 == "" || str2 == "")
        return (XmlNodeList) null;
      return xmlDocument.SelectNodes("//Pic");
    }

    private XmlDocument GetBookXmlDoc(string sBookXmlPath, bool bEncrypted, ref XmlNode xSchemaNode)
    {
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        if (File.Exists(sBookXmlPath))
        {
          try
          {
            xmlDocument.Load(sBookXmlPath);
          }
          catch
          {
          }
          if (bEncrypted)
          {
            string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
            xmlDocument.DocumentElement.InnerXml = str;
          }
          xSchemaNode = xmlDocument.SelectSingleNode("//Schema");
        }
      }
      catch
      {
      }
      return xmlDocument;
    }

    private void LoadSchemaValues(string _BookXmlPath)
    {
      XmlDocument xmlDocument = new XmlDocument();
      try
      {
        xmlDocument.Load(_BookXmlPath);
        bool flag = false;
        if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
          flag = true;
        if (flag)
        {
          string str = new AES().Decode(xmlDocument.InnerText, "0123456789ABCDEF");
          xmlDocument.DocumentElement.InnerXml = str;
        }
        XmlNode xmlNode = xmlDocument.SelectSingleNode("//Schema");
        if (xmlNode == null)
          return;
        foreach (XmlAttribute attribute in (XmlNamedNodeMap) xmlNode.Attributes)
        {
          if (attribute.Value.ToUpper().Equals("PAGENAME"))
            this.attPageNameItem = attribute.Name;
          if (attribute.Value.ToUpper().Equals("PARTSLISTFILE"))
            this.attPartsListItem = attribute.Name;
          if (attribute.Value.ToUpper().Equals("ID"))
            this.attPageIdItem = attribute.Name;
          if (attribute.Value.ToUpper().Equals("PARTNAME"))
            this.attPartName = attribute.Name;
          if (attribute.Value.ToUpper().Equals("PARTNUMBER"))
            this.attPartNumber = attribute.Name;
        }
      }
      catch
      {
      }
    }

    private bool SearchInBookSearchXML()
    {
      try
      {
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        string empty4 = string.Empty;
        string empty5 = string.Empty;
        bool flag = false;
        string str1 = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey + "\\" + this.frmParent.BookPublishingId;
        string str2 = str1 + "\\" + this.frmParent.BookPublishingId + "Search.zip";
        string str3 = str1 + "\\" + this.frmParent.BookPublishingId + "Search.xml";
        bool bEncrypted = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON";
        bool bCompressed = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"] != null && Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() == "ON";
        if (!File.Exists(str3))
        {
          if (File.Exists(str2))
          {
            try
            {
              Global.Unzip(str2);
            }
            catch
            {
            }
          }
        }
        if (!File.Exists(str3))
        {
          flag = true;
        }
        else
        {
          int result = 0;
          if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out result))
            result = 0;
          if (result == 0)
            flag = true;
          else if (result < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(str3, bCompressed, bEncrypted), Global.GetServerUpdateDateFromXmlNode(this.frmParent.SchemaNode, this.frmParent.BookNode), result))
            flag = true;
        }
        if (flag && !Program.objAppMode.bWorkingOffline)
        {
          this.GetResource("No result found", "NO_RESULT", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          string str4 = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
          string empty6 = string.Empty;
          string empty7 = string.Empty;
          string surl1;
          string sLocalPath;
          if (bCompressed)
          {
            surl1 = str4 + "/" + this.frmParent.BookPublishingId + "/" + this.frmParent.BookPublishingId + "Search.zip";
            sLocalPath = str2;
          }
          else
          {
            surl1 = str4 + "/" + this.frmParent.BookPublishingId + "/" + this.frmParent.BookPublishingId + "Search.xml";
            sLocalPath = str3;
          }
          if (!new Download().DownloadFile(surl1, sLocalPath))
            return false;
          if (bCompressed && File.Exists(str2))
            Global.Unzip(str2);
          if (!File.Exists(str3))
            return false;
        }
        if (!File.Exists(str3))
          return false;
        XmlNode xSchemaNode = (XmlNode) null;
        XmlDocument bookXmlDoc = this.GetBookXmlDoc(str1 + "\\" + this.frmParent.BookPublishingId + ".xml", bEncrypted, ref xSchemaNode);
        string attBookXmlPageNameElement = string.Empty;
        string attBookXmlPageIdElement = string.Empty;
        try
        {
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) xSchemaNode.Attributes)
          {
            if (attribute.Value.ToUpper() == "ID")
              attBookXmlPageIdElement = attribute.Name;
            if (attribute.Value.ToUpper() == "PAGENAME")
              attBookXmlPageNameElement = attribute.Name;
          }
          this.statusText = this.GetResource("Searching..", "SEARCHING", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
        catch
        {
        }
        this.LoadSchemaValues(str3);
        this.LoadSearchResultsInGridA(str3, bEncrypted, attBookXmlPageIdElement, attBookXmlPageNameElement, bookXmlDoc);
        this.statusText = this.dgViewSearch.Rows.Count.ToString() + " " + this.GetResource("result(s) found", "RESULT_FOUND", ResourceType.STATUS_MESSAGE);
        this.UpdateStatus();
        return true;
      }
      catch
      {
        return false;
      }
    }

    private void SearchPartsListXMLs()
    {
      try
      {
        string empty = string.Empty;
        string str = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] + "\\" + Program.iniServers[this.frmParent.ServerId].sIniKey + "\\" + this.frmParent.BookPublishingId;
        this.GridViewClearRows();
        if (File.Exists(str + "\\" + this.frmParent.BookPublishingId + ".xml"))
        {
          if (this.frmParent.IsDisposed)
            return;
          this.statusText = this.GetResource("Searching...", "SEARCHING", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
          this.LoadSchemaValues(str + "\\" + this.frmParent.BookPublishingId + ".xml");
          foreach (XmlNode loadPartsListName in this.LoadPartsListNames(str + "\\" + this.frmParent.BookPublishingId + ".xml"))
          {
            if (loadPartsListName.Attributes[this.attPartsListItem] != null)
            {
              string sFilePath = str + "\\" + loadPartsListName.Attributes[this.attPartsListItem].Value + ".xml";
              if (loadPartsListName.ParentNode.Attributes[this.attPageNameItem] != null && loadPartsListName.ParentNode.Attributes[this.attPageIdItem] != null)
                this.LoadSearchResultsInGrid(sFilePath, loadPartsListName.ParentNode.Attributes[this.attPageNameItem].Value, loadPartsListName.ParentNode.Attributes[this.attPageIdItem].Value);
            }
          }
          this.statusText = this.dgViewSearch.Rows.Count.ToString() + " " + this.GetResource("result(s) found", "RESULTS_FOUND", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
        else
        {
          if (this.frmParent.IsDisposed)
            return;
          this.statusText = this.GetResource("No result found", "NO_RESULT", ResourceType.STATUS_MESSAGE);
          this.UpdateStatus();
        }
      }
      catch
      {
      }
    }

    private string GetAdvSearchMatchKeys(bool bIsBookXML, XmlNode xndSchema)
    {
      string strInputVal = string.Empty;
      try
      {
        ArrayList arrayList = new ArrayList();
        ArrayList keys = new IniFileIO().GetKeys(Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini", "ADVANCE_SEARCH_MATCHING_KEYS");
        if (keys.Count > 0)
        {
          IniFileIO iniFileIo = new IniFileIO();
          if (bIsBookXML)
          {
            for (int index = 0; index < keys.Count; ++index)
            {
              string keyValue = iniFileIo.GetKeyValue("ADVANCE_SEARCH_MATCHING_KEYS", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
              if (keys[index].ToString().ToUpper() == "BOOK_XML_MATCHING_KEY")
              {
                strInputVal = keyValue;
                if (!this.ValidateAdvSearchKey(strInputVal, xndSchema))
                {
                  strInputVal = bIsBookXML ? "a4" : "a3";
                  break;
                }
                break;
              }
            }
            if (strInputVal == string.Empty)
              strInputVal = bIsBookXML ? "a4" : "a3";
          }
          else
          {
            for (int index = 0; index < keys.Count; ++index)
            {
              string keyValue = iniFileIo.GetKeyValue("ADVANCE_SEARCH_MATCHING_KEYS", keys[index].ToString(), Application.StartupPath + "\\GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini");
              if (keys[index].ToString().ToUpper() == "BOOK_SEARCH_XML_MATCHING_KEY")
              {
                strInputVal = keyValue;
                if (!this.ValidateAdvSearchKey(strInputVal, xndSchema))
                {
                  strInputVal = bIsBookXML ? "a4" : "a3";
                  break;
                }
                break;
              }
            }
            if (strInputVal == string.Empty)
              strInputVal = bIsBookXML ? "a4" : "a3";
          }
        }
        else
          strInputVal = bIsBookXML ? "a4" : "a3";
        return strInputVal;
      }
      catch (Exception ex)
      {
        return bIsBookXML ? "a4" : "a3";
      }
    }

    private bool ValidateAdvSearchKey(string strInputVal, XmlNode xndSchema)
    {
      try
      {
        foreach (XmlNode attribute in (XmlNamedNodeMap) xndSchema.Attributes)
        {
          if (attribute.Name.ToUpper().Trim() == strInputVal.ToUpper().Trim())
            return true;
        }
        return false;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    private XmlNodeList SearchWithLINQ(string XmlFilePath, bool bEncrypted, string sDecryptedInnerXML, string attIDElement, string searchType)
    {
      try
      {
        XElement xelement1;
        if (bEncrypted)
        {
          sDecryptedInnerXML = "<Parent>" + sDecryptedInnerXML + "</Parent>";
          xelement1 = XElement.Parse(sDecryptedInnerXML);
        }
        else
          xelement1 = XElement.Load(XmlFilePath);
        IEnumerable<XElement> xelements = !this.checkBoxExactMatch.Checked ? xelement1.DescendantsAndSelf((XName) searchType).Where<XElement>((Func<XElement, bool>) (h => h.Attribute((XName) attIDElement).Value.ToUpper().Contains(this.searchString.ToUpper()))) : xelement1.DescendantsAndSelf((XName) searchType).Where<XElement>((Func<XElement, bool>) (h => h.Attribute((XName) attIDElement).Value.ToUpper() == this.searchString.ToUpper()));
        XmlDocument xmlDocument = new XmlDocument();
        string str = string.Empty + "<Parent>";
        foreach (XElement xelement2 in xelements)
          str = str + xelement2.ToString() + "\n";
        string xml = str + "</Parent>";
        xmlDocument.LoadXml(xml);
        return xmlDocument.SelectNodes("//" + searchType);
      }
      catch (Exception ex)
      {
        return (XmlNodeList) null;
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
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      this.lblPartName = new Label();
      this.pnlControl = new Panel();
      this.btnClearHistory = new Button();
      this.btnSearch = new Button();
      this.pnlForm = new Panel();
      this.pnlSearchResults = new Panel();
      this.dgViewSearch = new DataGridView();
      this.pnlSearch = new Panel();
      this.checkBoxSearchWholeBook = new CheckBox();
      this.checkBoxExactMatch = new CheckBox();
      this.checkBoxMatchCase = new CheckBox();
      this.txtPartName = new TextBox();
      this.picLoading = new PictureBox();
      this.bgWorker = new BackgroundWorker();
      this.ssStatus = new StatusStrip();
      this.lblStatus = new ToolStripStatusLabel();
      this.pnlControl.SuspendLayout();
      this.pnlForm.SuspendLayout();
      this.pnlSearchResults.SuspendLayout();
      ((ISupportInitialize) this.dgViewSearch).BeginInit();
      this.pnlSearch.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.ssStatus.SuspendLayout();
      this.SuspendLayout();
      this.lblPartName.BackColor = Color.White;
      this.lblPartName.Dock = DockStyle.Top;
      this.lblPartName.ForeColor = Color.Black;
      this.lblPartName.Location = new Point(10, 0);
      this.lblPartName.Name = "lblPartName";
      this.lblPartName.Padding = new Padding(0, 7, 0, 0);
      this.lblPartName.Size = new Size(478, 27);
      this.lblPartName.TabIndex = 16;
      this.lblPartName.Text = "Part Name";
      this.pnlControl.Controls.Add((Control) this.checkBoxSearchWholeBook);
      this.pnlControl.Controls.Add((Control) this.btnClearHistory);
      this.pnlControl.Controls.Add((Control) this.btnSearch);
      this.pnlControl.Dock = DockStyle.Bottom;
      this.pnlControl.Location = new Point(10, 395);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(4, 4, 15, 4);
      this.pnlControl.Size = new Size(478, 31);
      this.pnlControl.TabIndex = 18;
      this.btnClearHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnClearHistory.Location = new Point(211, 5);
      this.btnClearHistory.Name = "btnClearHistory";
      this.btnClearHistory.Size = new Size(114, 23);
      this.btnClearHistory.TabIndex = 4;
      this.btnClearHistory.Text = "Clear Search History";
      this.btnClearHistory.UseVisualStyleBackColor = true;
      this.btnClearHistory.Click += new EventHandler(this.btnClearHistory_Click);
      this.btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.btnSearch.Enabled = false;
      this.btnSearch.Location = new Point(350, 5);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new Size(114, 23);
      this.btnSearch.TabIndex = 5;
      this.btnSearch.Text = "Search";
      this.btnSearch.UseVisualStyleBackColor = true;
      this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlSearchResults);
      this.pnlForm.Controls.Add((Control) this.pnlSearch);
      this.pnlForm.Controls.Add((Control) this.pnlControl);
      this.pnlForm.Controls.Add((Control) this.lblPartName);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Padding = new Padding(10, 0, 10, 0);
      this.pnlForm.Size = new Size(500, 428);
      this.pnlForm.TabIndex = 19;
      this.pnlSearchResults.Controls.Add((Control) this.dgViewSearch);
      this.pnlSearchResults.Dock = DockStyle.Fill;
      this.pnlSearchResults.Location = new Point(10, 82);
      this.pnlSearchResults.Name = "pnlSearchResults";
      this.pnlSearchResults.Size = new Size(478, 313);
      this.pnlSearchResults.TabIndex = 23;
      this.dgViewSearch.AllowDrop = true;
      this.dgViewSearch.AllowUserToAddRows = false;
      this.dgViewSearch.AllowUserToDeleteRows = false;
      this.dgViewSearch.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.White;
      this.dgViewSearch.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dgViewSearch.BackgroundColor = Color.White;
      this.dgViewSearch.BorderStyle = BorderStyle.Fixed3D;
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Control;
      gridViewCellStyle2.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle2.ForeColor = Color.Black;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgViewSearch.ColumnHeadersDefaultCellStyle = gridViewCellStyle2;
      this.dgViewSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgViewSearch.Dock = DockStyle.Fill;
      this.dgViewSearch.Location = new Point(0, 0);
      this.dgViewSearch.Name = "dgViewSearch";
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle3.BackColor = SystemColors.Control;
      gridViewCellStyle3.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      gridViewCellStyle3.ForeColor = Color.Black;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.dgViewSearch.RowHeadersDefaultCellStyle = gridViewCellStyle3;
      this.dgViewSearch.RowHeadersVisible = false;
      this.dgViewSearch.RowHeadersWidth = 32;
      this.dgViewSearch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      gridViewCellStyle4.BackColor = Color.White;
      gridViewCellStyle4.ForeColor = Color.Black;
      gridViewCellStyle4.SelectionBackColor = Color.SteelBlue;
      gridViewCellStyle4.SelectionForeColor = Color.White;
      this.dgViewSearch.RowsDefaultCellStyle = gridViewCellStyle4;
      this.dgViewSearch.RowTemplate.Height = 16;
      this.dgViewSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgViewSearch.ShowRowErrors = false;
      this.dgViewSearch.Size = new Size(478, 313);
      this.dgViewSearch.TabIndex = 4;
      this.dgViewSearch.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.dgViewSearch_CellMouseDoubleClick);
      this.pnlSearch.Controls.Add((Control) this.checkBoxExactMatch);
      this.pnlSearch.Controls.Add((Control) this.checkBoxMatchCase);
      this.pnlSearch.Controls.Add((Control) this.txtPartName);
      this.pnlSearch.Dock = DockStyle.Top;
      this.pnlSearch.Location = new Point(10, 27);
      this.pnlSearch.Name = "pnlSearch";
      this.pnlSearch.Size = new Size(478, 55);
      this.pnlSearch.TabIndex = 21;
      this.checkBoxSearchWholeBook.AutoSize = true;
      this.checkBoxSearchWholeBook.Checked = true;
      this.checkBoxSearchWholeBook.CheckState = CheckState.Checked;
      this.checkBoxSearchWholeBook.Location = new Point(4, 9);
      this.checkBoxSearchWholeBook.Name = "checkBoxSearchWholeBook";
      this.checkBoxSearchWholeBook.Size = new Size(116, 17);
      this.checkBoxSearchWholeBook.TabIndex = 4;
      this.checkBoxSearchWholeBook.Text = "Search whole book";
      this.checkBoxSearchWholeBook.UseVisualStyleBackColor = true;
      this.checkBoxExactMatch.AutoSize = true;
      this.checkBoxExactMatch.Location = new Point(240, 19);
      this.checkBoxExactMatch.Name = "checkBoxExactMatch";
      this.checkBoxExactMatch.Size = new Size(85, 17);
      this.checkBoxExactMatch.TabIndex = 2;
      this.checkBoxExactMatch.Text = "Exact Match";
      this.checkBoxExactMatch.UseVisualStyleBackColor = true;
      this.checkBoxMatchCase.AutoSize = true;
      this.checkBoxMatchCase.Location = new Point(355, 19);
      this.checkBoxMatchCase.Name = "checkBoxMatchCase";
      this.checkBoxMatchCase.Size = new Size(82, 17);
      this.checkBoxMatchCase.TabIndex = 3;
      this.checkBoxMatchCase.Text = "Match Case";
      this.checkBoxMatchCase.UseVisualStyleBackColor = true;
      this.txtPartName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
      this.txtPartName.AutoCompleteSource = AutoCompleteSource.CustomSource;
      this.txtPartName.Location = new Point(4, 17);
      this.txtPartName.Name = "txtPartName";
      this.txtPartName.Size = new Size(200, 21);
      this.txtPartName.TabIndex = 1;
      this.txtPartName.TextChanged += new EventHandler(this.txtPartName_TextChanged);
      this.txtPartName.KeyDown += new KeyEventHandler(this.txtPartName_KeyDown);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Image = (Image) Resources.Loading1;
      this.picLoading.Location = new Point(379, 246);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(34, 34);
      this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
      this.picLoading.TabIndex = 24;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.ssStatus.BackColor = SystemColors.Control;
      this.ssStatus.Items.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.lblStatus
      });
      this.ssStatus.Location = new Point(0, 428);
      this.ssStatus.Name = "ssStatus";
      this.ssStatus.Size = new Size(500, 22);
      this.ssStatus.TabIndex = 20;
      this.lblStatus.BackColor = SystemColors.Control;
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new Size(0, 17);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(500, 450);
      this.Controls.Add((Control) this.pnlForm);
      this.Controls.Add((Control) this.ssStatus);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.KeyPreview = true;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(450, 450);
      this.Name = nameof (frmPartNameSearch);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Search";
      this.Load += new EventHandler(this.frmPartNameSearch_Load);
      this.FormClosed += new FormClosedEventHandler(this.frmPartSearch_FormClosed);
      this.FormClosing += new FormClosingEventHandler(this.frmPartNameSearch_FormClosing);
      this.KeyDown += new KeyEventHandler(this.frmPartNameSearch_KeyDown);
      this.pnlControl.ResumeLayout(false);
      this.pnlControl.PerformLayout();
      this.pnlForm.ResumeLayout(false);
      this.pnlForm.PerformLayout();
      this.pnlSearchResults.ResumeLayout(false);
      ((ISupportInitialize) this.dgViewSearch).EndInit();
      this.pnlSearch.ResumeLayout(false);
      this.pnlSearch.PerformLayout();
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ssStatus.ResumeLayout(false);
      this.ssStatus.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void InitializeSearchGridDelegate();

    private delegate void LoadDataGridViewXmlDelegate();

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);

    private delegate void GridViewClearRowsDelegate();

    private delegate void GridViewAddRowDelegate(DataGridViewRow tempRow);

    private delegate void UpdadetControlsDelegate();
  }
}

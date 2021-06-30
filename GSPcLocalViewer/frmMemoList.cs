// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmMemoList
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmMemoList : Form
  {
    private IContainer components;
    private ToolStripContainer toolStripContainer1;
    private SplitContainer splitPnlMemoList;
    public frmMemoListTasks objFrmTasks;
    public frmMemoListLocal objFrmMemoLocal;
    public frmMemoListGlobal objFrmMemoGlobal;
    public frmMemoListAdmin objFrmMemoAdmin;
    public frmMemoListAll objFrmMemoAll;
    public XmlNodeList xnlLocalMemo;
    public XmlNodeList xnlGlobalMemo;
    public XmlNodeList xnlAdminMemo;
    public string sServerKey;
    public string sBookId;
    public string sPageId;
    public string sPicIndex;
    public string sListIndex;
    public string sPartNumber;
    public frmViewer frmParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.toolStripContainer1 = new ToolStripContainer();
      this.splitPnlMemoList = new SplitContainer();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.splitPnlMemoList.SuspendLayout();
      this.SuspendLayout();
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.splitPnlMemoList);
      this.toolStripContainer1.ContentPanel.Size = new Size(694, 342);
      this.toolStripContainer1.Dock = DockStyle.Fill;
      this.toolStripContainer1.Location = new Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new Size(694, 367);
      this.toolStripContainer1.TabIndex = 4;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.splitPnlMemoList.Dock = DockStyle.Fill;
      this.splitPnlMemoList.Location = new Point(0, 0);
      this.splitPnlMemoList.MinimumSize = new Size(684, 342);
      this.splitPnlMemoList.Name = "splitPnlMemoList";
      this.splitPnlMemoList.Panel1.AccessibleName = "pnlTasks";
      this.splitPnlMemoList.Panel1MinSize = 160;
      this.splitPnlMemoList.Panel2.AccessibleName = "pnlContents";
      this.splitPnlMemoList.Panel2MinSize = 530;
      this.splitPnlMemoList.Size = new Size(694, 342);
      this.splitPnlMemoList.SplitterDistance = 160;
      this.splitPnlMemoList.TabIndex = 1;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(694, 367);
      this.Controls.Add((Control) this.toolStripContainer1);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.IsMdiContainer = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(710, 405);
      this.Name = nameof (frmMemoList);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Memo List";
      this.Load += new EventHandler(this.frmMemo_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmMemo_FormClosing);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitPnlMemoList.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmMemoList(frmViewer frm, XmlNodeList xLocalMemoList, XmlNodeList xGlobalMemoList, XmlNodeList xAdminMemoList, string sPageId, string sPicIndex, string sListIndex, string sPartNumber)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.CreateForms();
      this.UpdateFont();
      this.LoadResources();
      this.xnlLocalMemo = xLocalMemoList;
      this.xnlGlobalMemo = xGlobalMemoList;
      this.xnlAdminMemo = xAdminMemoList;
      this.sServerKey = Program.iniServers[this.frmParent.ServerId].sIniKey;
      this.sBookId = this.frmParent.BookPublishingId;
      this.sPageId = sPageId;
      this.sPicIndex = sPicIndex;
      this.sListIndex = sListIndex;
      this.sPartNumber = sPartNumber;
    }

    private void frmMemo_Load(object sender, EventArgs e)
    {
      this.LoadUserSettings();
      this.objFrmTasks.Show();
      this.objFrmTasks.ShowTask(ViewAllMemoTasks.All);
    }

    private void frmMemo_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.frmParent.Enabled)
        this.frmParent.Enabled = true;
      if (!this.objFrmMemoLocal.IsDisposed)
        this.objFrmMemoLocal.Close();
      if (!this.objFrmMemoGlobal.IsDisposed)
        this.objFrmMemoGlobal.Close();
      if (!this.objFrmMemoAll.IsDisposed)
        this.objFrmMemoAll.Close();
      if (!this.objFrmMemoAdmin.IsDisposed)
        this.objFrmMemoAdmin.Close();
      if (!this.objFrmTasks.IsDisposed)
        this.objFrmTasks.Close();
      try
      {
        this.SaveUserSettings();
      }
      catch
      {
      }
      this.frmParent.HideDimmer();
    }

    private void CreateForms()
    {
      this.objFrmMemoLocal = new frmMemoListLocal(this);
      this.objFrmMemoLocal.TopLevel = false;
      this.objFrmMemoLocal.Dock = DockStyle.Fill;
      this.splitPnlMemoList.Panel2.Controls.Add((Control) this.objFrmMemoLocal);
      this.objFrmMemoGlobal = new frmMemoListGlobal(this);
      this.objFrmMemoGlobal.TopLevel = false;
      this.objFrmMemoGlobal.Dock = DockStyle.Fill;
      this.splitPnlMemoList.Panel2.Controls.Add((Control) this.objFrmMemoGlobal);
      this.objFrmMemoAdmin = new frmMemoListAdmin(this);
      this.objFrmMemoAdmin.TopLevel = false;
      this.objFrmMemoAdmin.Dock = DockStyle.Fill;
      this.splitPnlMemoList.Panel2.Controls.Add((Control) this.objFrmMemoAdmin);
      this.objFrmMemoAll = new frmMemoListAll(this);
      this.objFrmMemoAll.TopLevel = false;
      this.objFrmMemoAll.Dock = DockStyle.Fill;
      this.splitPnlMemoList.Panel2.Controls.Add((Control) this.objFrmMemoAll);
      this.objFrmTasks = new frmMemoListTasks(this);
      this.objFrmTasks.TopLevel = false;
      this.objFrmTasks.Dock = DockStyle.Fill;
      this.splitPnlMemoList.Panel1.Controls.Add((Control) this.objFrmTasks);
    }

    public void HideForms()
    {
      foreach (Control control in (ArrangedElementCollection) this.splitPnlMemoList.Panel2.Controls)
        control?.Hide();
    }

    public void CloseAndSaveSettings()
    {
      if (Settings.Default.EnableLocalMemo && this.objFrmMemoLocal.isMemoChanged)
      {
        this.DeletePrevMemos("local");
        this.AddNewMemos("local", this.objFrmMemoLocal.getMemos);
      }
      if (Settings.Default.EnableGlobalMemo && this.objFrmMemoGlobal.isMemoChanged)
      {
        this.DeletePrevMemos("global");
        this.AddNewMemos("global", this.objFrmMemoGlobal.getMemos);
      }
      if (this.objFrmMemoAll.isMemoChanged)
      {
        if (Settings.Default.EnableGlobalMemo)
        {
          this.DeletePrevMemos("global");
          this.AddNewMemos("global", this.objFrmMemoAll.getGlobalMemos);
        }
        if (Settings.Default.EnableLocalMemo)
        {
          this.DeletePrevMemos("local");
          this.AddNewMemos("local", this.objFrmMemoAll.getLocalMemos);
        }
      }
      this.Close();
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }

    public void OpenBookFromString(string reference)
    {
      this.frmParent.OpenBookFromString(reference);
    }

    private void AddNewMemos(string memoType, string[] sNodesArr)
    {
      if (sNodesArr == null || sNodesArr.Length <= 0)
        return;
      if (memoType.ToUpper().Equals("LOCAL"))
      {
        for (int index = 0; index < sNodesArr.Length; ++index)
        {
          XmlNode newChild;
          try
          {
            newChild = this.frmParent.xDocLocalMemo.ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(sNodesArr[index])));
          }
          catch
          {
            newChild = (XmlNode) null;
          }
          if (newChild != null)
            this.frmParent.xDocLocalMemo.DocumentElement.AppendChild(newChild);
        }
      }
      else
      {
        if (!memoType.ToUpper().Equals("GLOBAL"))
          return;
        for (int index = 0; index < sNodesArr.Length; ++index)
        {
          XmlNode newChild;
          try
          {
            newChild = this.frmParent.xDocGlobalMemo.ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(sNodesArr[index])));
          }
          catch
          {
            newChild = (XmlNode) null;
          }
          if (newChild != null)
            this.frmParent.xDocGlobalMemo.DocumentElement.AppendChild(newChild);
        }
      }
    }

    private void DeletePrevMemos(string memoType)
    {
      if (memoType.ToUpper().Equals("LOCAL"))
      {
        foreach (XmlNode selectNode in this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo"))
          this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(selectNode);
      }
      else
      {
        if (!memoType.ToUpper().Equals("GLOBAL"))
          return;
        foreach (XmlNode selectNode in this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo"))
          this.frmParent.xDocGlobalMemo.DocumentElement.RemoveChild(selectNode);
      }
    }

    public void OpenParentPage(string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex, string sPartNumber)
    {
      try
      {
        this.frmParent.OpenParentPage(sServerKey, sBookPubId, sPageId, sImageIndex, sListIndex, sPartNumber);
      }
      catch
      {
      }
    }

    private void SaveUserSettings()
    {
      Settings.Default.frmMemoListLocation = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Location : this.Location;
      Settings.Default.frmMemoListSize = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Size : this.Size;
      Settings.Default.frmMemoListState = this.WindowState;
    }

    private void LoadUserSettings()
    {
      if (Settings.Default.frmMemoListLocation.X == -1 && Settings.Default.frmMemoListLocation.Y == -1)
        this.CenterToParent();
      else
        this.Location = Settings.Default.frmMemoListLocation;
      this.Size = Settings.Default.frmMemoListSize;
      if (Settings.Default.frmMemoListState == FormWindowState.Minimized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = Settings.Default.frmMemoListState;
    }

    private void LoadResources()
    {
      this.Text = this.GetResource("Memo List", "MEMO_LIST", ResourceType.TITLE);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MEMO_LIST']";
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

    private XmlNodeList RemoveDuplicateMemoNodes(XmlNodeList xnlMemoNodes)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode element1 = (XmlNode) xmlDocument.CreateElement("Memos");
      XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//Memos/Memo");
      try
      {
        foreach (XmlNode xnlMemoNode in xnlMemoNodes)
        {
          if (xnlMemoNode.Attributes.Count > 0 && xnlMemoNode.Attributes["PartNo"] != null && xnlMemoNode.Attributes["PartNo"].Value.ToString() != "")
          {
            if (xmlNodeList.Count > 0)
            {
              foreach (XmlNode xmlNode in xmlNodeList)
              {
                if (xmlNode.Attributes.Count > 0 && xmlNode.Attributes["PartNo"] != null && xmlNode.Attributes["PartNo"].Value.ToString() != "")
                {
                  if (xnlMemoNode.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() && xnlMemoNode.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["BookId"].Value.ToString().Trim().ToUpper() && (xnlMemoNode.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["PartNo"].Value.ToString().Trim().ToUpper() && xnlMemoNode.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["Type"].Value.ToString().Trim().ToUpper()) && xnlMemoNode.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["Value"].Value.ToString().Trim().ToUpper())
                  {
                    if (xnlMemoNode.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNode.Attributes["Update"].Value.ToString().Trim().ToUpper())
                      break;
                  }
                  XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
                  for (int index = 0; index < xnlMemoNode.Attributes.Count; ++index)
                  {
                    XmlAttribute attribute = xmlDocument.CreateAttribute(xnlMemoNode.Attributes[index].Name);
                    attribute.Value = xnlMemoNode.Attributes[index].Value;
                    element2.Attributes.Append(attribute);
                  }
                  element1.AppendChild(element2);
                  xmlDocument.AppendChild(element1);
                  xmlNodeList = xmlDocument.SelectNodes("//Memos/Memo");
                }
              }
            }
            else
            {
              XmlNode element2 = (XmlNode) xmlDocument.CreateElement("Memo");
              for (int index = 0; index < xnlMemoNode.Attributes.Count; ++index)
              {
                XmlAttribute attribute = xmlDocument.CreateAttribute(xnlMemoNode.Attributes[index].Name);
                attribute.Value = xnlMemoNode.Attributes[index].Value;
                element2.Attributes.Append(attribute);
              }
              element1.AppendChild(element2);
              xmlDocument.AppendChild(element1);
              xmlNodeList = xmlDocument.SelectNodes("//Memos/Memo");
            }
          }
        }
        return xmlNodeList;
      }
      catch (Exception ex)
      {
        return xmlNodeList;
      }
    }
  }
}

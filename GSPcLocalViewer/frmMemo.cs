// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmMemo
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmMemo : Form
  {
    public frmMemoTasks objFrmTasks;
    public frmMemoLocal objFrmMemoLocal;
    public frmMemoGlobal objFrmMemoGlobal;
    public frmMemoAdmin objFrmMemoAdmin;
    public XmlNodeList xnlLocalMemo;
    public XmlNodeList xnlGlobalMemo;
    public XmlNodeList xnladminMemo;
    public string sServerKey;
    public string sBookId;
    public string sPageId;
    public string sPicIndex;
    public string sListIndex;
    public string sPartNumber;
    public frmViewer frmParent;
    private string sMemoScope;
    public int intMemoType;
    public bool bNoMemos;
    public bool bMemoDeleted;
    private IContainer components;
    private ToolStripContainer toolStripContainer1;
    private SplitContainer splitPnlMemo;

    public frmMemo(frmViewer frm, XmlNodeList xLocalMemoList, XmlNodeList xGlobalMemoList, XmlNodeList xAdminMemoList, string sPageId, string sPicIndex, string sListIndex, string sPartNumber, string sScope1)
    {
      this.InitializeComponent();
      this.intMemoType = this.GetMemoType();
      this.sMemoScope = sScope1;
      this.frmParent = frm;
      this.CreateForms();
      this.UpdateFont();
      if (sPartNumber.Trim().Equals(string.Empty))
        this.Text = this.GetResource("Picture Memo", "FORM_TITLE_PICTURE", ResourceType.LABEL);
      else
        this.Text = this.GetResource("Part Memo", "MEMO", ResourceType.TITLE);
      this.xnlLocalMemo = xLocalMemoList;
      this.xnlGlobalMemo = xGlobalMemoList;
      this.xnladminMemo = xAdminMemoList;
      this.sServerKey = Program.iniServers[this.frmParent.ServerId].sIniKey;
      this.sBookId = this.frmParent.BookPublishingId;
      this.sPageId = sPageId;
      this.sPicIndex = sPicIndex;
      this.sListIndex = sListIndex;
      this.sPartNumber = sPartNumber;
    }

    private void frmMemo_Load(object sender, EventArgs e)
    {
      this.objFrmTasks.Show();
      this.LoadUserSettings();
      if (this.bNoMemos)
        this.Close();
      if (this.intMemoType == 2)
        return;
      if (this.xnlLocalMemo != null && this.xnlLocalMemo.Count > 0 && (this.sMemoScope == "LOC" || this.sMemoScope == string.Empty))
        this.objFrmTasks.ShowTask(MemoTasks.Local);
      else if (this.xnlGlobalMemo != null && this.xnlGlobalMemo.Count > 0 && (this.sMemoScope == "GBL" || this.sMemoScope == string.Empty))
        this.objFrmTasks.ShowTask(MemoTasks.Global);
      else if (this.xnladminMemo != null && this.xnladminMemo.Count > 0 && (this.sMemoScope == "ADM" || this.sMemoScope == string.Empty))
        this.objFrmTasks.ShowTask(MemoTasks.Admin);
      else if (Settings.Default.EnableLocalMemo)
        this.objFrmTasks.ShowTask(MemoTasks.Local);
      else if (Settings.Default.EnableGlobalMemo)
      {
        this.objFrmTasks.ShowTask(MemoTasks.Global);
      }
      else
      {
        if (!Settings.Default.EnableAdminMemo)
          return;
        this.objFrmTasks.ShowTask(MemoTasks.Admin);
      }
    }

    private void frmMemo_FormClosing(object sender, FormClosingEventArgs e)
    {
      try
      {
        if (!this.sPartNumber.Trim().Equals(string.Empty))
          this.frmParent.objFrmPartlist.UpdateMemoIconOnSelectedRow();
        if (!this.frmParent.Enabled)
          this.frmParent.Enabled = true;
        if (!this.objFrmMemoLocal.IsDisposed)
          this.objFrmMemoLocal.Close();
        if (!this.objFrmMemoGlobal.IsDisposed)
          this.objFrmMemoGlobal.Close();
        if (!this.objFrmMemoAdmin.IsDisposed)
          this.objFrmMemoAdmin.Close();
        if (!this.objFrmTasks.IsDisposed)
          this.objFrmTasks.Close();
        this.SaveUserSettings();
        this.frmParent.HideDimmer();
        if (this.sPartNumber.Trim().Equals(string.Empty))
        {
          if (this.frmParent.objFrmTreeview.IsDisposed)
            return;
          this.frmParent.objFrmTreeview.Focus();
        }
        else
        {
          if (this.frmParent.objFrmPartlist.IsDisposed)
            return;
          this.frmParent.objFrmPartlist.Focus();
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void CreateForms()
    {
      this.objFrmMemoLocal = new frmMemoLocal(this);
      this.objFrmMemoLocal.TopLevel = false;
      this.objFrmMemoLocal.Dock = DockStyle.Fill;
      this.splitPnlMemo.Panel2.Controls.Add((Control) this.objFrmMemoLocal);
      this.objFrmMemoGlobal = new frmMemoGlobal(this);
      this.objFrmMemoGlobal.TopLevel = false;
      this.objFrmMemoGlobal.Dock = DockStyle.Fill;
      this.splitPnlMemo.Panel2.Controls.Add((Control) this.objFrmMemoGlobal);
      this.objFrmMemoAdmin = new frmMemoAdmin(this);
      this.objFrmMemoAdmin.TopLevel = false;
      this.objFrmMemoAdmin.Dock = DockStyle.Fill;
      this.splitPnlMemo.Panel2.Controls.Add((Control) this.objFrmMemoAdmin);
      this.objFrmTasks = new frmMemoTasks(this);
      this.objFrmTasks.TopLevel = false;
      this.objFrmTasks.Dock = DockStyle.Fill;
      this.splitPnlMemo.Panel1.Controls.Add((Control) this.objFrmTasks);
    }

    public void HideForms()
    {
      foreach (Control control in (ArrangedElementCollection) this.splitPnlMemo.Panel2.Controls)
        control?.Hide();
    }

    public void CloseAndSaveSettings()
    {
      try
      {
        if (Settings.Default.EnableLocalMemo && this.objFrmMemoLocal.isMemoChanged)
        {
          this.DeletePrevMemos("local");
          if (!this.bMemoDeleted)
          {
            string[] sNodesArr = this.FilterMemo(this.objFrmMemoLocal.getMemos, "LOCAL");
            if (sNodesArr != null && sNodesArr.Length > 0)
              this.AddNewMemos("local", sNodesArr);
          }
        }
        if (Settings.Default.EnableGlobalMemo && this.objFrmMemoGlobal.isMemoChanged)
        {
          this.DeletePrevMemos("global");
          if (!this.bMemoDeleted)
          {
            string[] sNodesArr = this.FilterMemo(this.objFrmMemoGlobal.getMemos, "GLOBAL");
            if (sNodesArr != null && sNodesArr.Length > 0)
              this.AddNewMemos("global", sNodesArr);
          }
        }
        if ((this.objFrmMemoLocal.bSaveMemoOnBookLevel || this.objFrmMemoGlobal.bSaveMemoOnBookLevel) && (this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked) && (this.sPartNumber != string.Empty && this.intMemoType != 2))
          return;
        this.Close();
      }
      catch (Exception ex)
      {
      }
    }

    public void CloseAndSaveSettings(string strMemoType)
    {
      try
      {
        if (strMemoType.ToUpper() == "LOCAL")
        {
          if (Settings.Default.EnableLocalMemo && this.objFrmMemoLocal.isMemoChanged)
          {
            this.DeletePrevMemos("local");
            if (this.intMemoType != 2 && this.objFrmMemoLocal.bSaveMemoOnBookLevel && this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked)
            {
              if (!this.bMemoDeleted)
              {
                string[] sNodesArr = this.FilterMemo(this.objFrmMemoLocal.getMemos, "LOCAL");
                if (sNodesArr != null && sNodesArr.Length > 0)
                  this.AddNewMemos("local", sNodesArr);
              }
            }
            else
              this.AddNewMemos("local", this.objFrmMemoLocal.getMemos);
          }
        }
        else if (strMemoType.ToUpper() == "GLOBAL" && Settings.Default.EnableGlobalMemo && this.objFrmMemoGlobal.isMemoChanged)
        {
          this.DeletePrevMemos("global");
          if (this.intMemoType != 2 && this.objFrmMemoGlobal.bSaveMemoOnBookLevel && this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked)
          {
            if (!this.bMemoDeleted)
            {
              string[] sNodesArr = this.FilterMemo(this.objFrmMemoGlobal.getMemos, "GLOBAL");
              if (sNodesArr != null && sNodesArr.Length > 0)
                this.AddNewMemos("global", sNodesArr);
            }
          }
          else
            this.AddNewMemos("global", this.objFrmMemoGlobal.getMemos);
        }
        if ((this.objFrmMemoLocal.bSaveMemoOnBookLevel || this.objFrmMemoGlobal.bSaveMemoOnBookLevel) && (this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked) && (this.sPartNumber != string.Empty && this.intMemoType != 2))
          return;
        this.Close();
      }
      catch (Exception ex)
      {
      }
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
          XmlNode newChild1;
          try
          {
            if (sNodesArr[index].Contains("^"))
            {
              string str = sNodesArr[index].TrimEnd('^').Trim();
              char[] chArray = new char[1]{ '^' };
              foreach (string s in str.Split(chArray))
              {
                XmlNode newChild2 = this.frmParent.xDocLocalMemo.ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
                if (newChild2 != null)
                  this.frmParent.xDocLocalMemo.DocumentElement.AppendChild(newChild2);
              }
              continue;
            }
            newChild1 = this.frmParent.xDocLocalMemo.ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(sNodesArr[index])));
          }
          catch
          {
            newChild1 = (XmlNode) null;
          }
          if (newChild1 != null)
            this.frmParent.xDocLocalMemo.DocumentElement.AppendChild(newChild1);
        }
      }
      else
      {
        if (!memoType.ToUpper().Equals("GLOBAL"))
          return;
        for (int index = 0; index < sNodesArr.Length; ++index)
        {
          XmlNode newChild1;
          try
          {
            if (sNodesArr[index].Contains("^"))
            {
              string str = sNodesArr[index].TrimEnd('^').Trim();
              char[] chArray = new char[1]{ '^' };
              foreach (string s in str.Split(chArray))
              {
                XmlNode newChild2 = this.frmParent.xDocGlobalMemo.ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(s)));
                if (newChild2 != null)
                  this.frmParent.xDocGlobalMemo.DocumentElement.AppendChild(newChild2);
              }
              continue;
            }
            newChild1 = this.frmParent.xDocGlobalMemo.ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(sNodesArr[index])));
          }
          catch
          {
            newChild1 = (XmlNode) null;
          }
          if (newChild1 != null)
            this.frmParent.xDocGlobalMemo.DocumentElement.AppendChild(newChild1);
        }
      }
    }

    private void DeletePrevMemos(string memoType)
    {
      bool flag = false;
      XmlNodeList xmlNodeList = (XmlNodeList) null;
      if (memoType.ToUpper().Equals("LOCAL"))
      {
        if (this.objFrmMemoLocal.bMultiMemoChange)
        {
          if (this.objFrmMemoLocal.strMemoChangedType != "")
          {
            string[] strArray1 = this.objFrmMemoLocal.strMemoChangedType.Split('|');
            this.objFrmMemoLocal.strDelMemoDate = this.objFrmMemoLocal.strDelMemoDate.TrimEnd('|');
            string[] strArray2 = this.objFrmMemoLocal.strDelMemoDate.Split('|');
            for (int index = 0; index < strArray1.Length; ++index)
            {
              string str = string.Empty;
              if (strArray1[index].ToUpper().Trim() == "TEXT")
                str = "txt";
              else if (strArray1[index].ToUpper().Trim() == "REFERENCE")
                str = "ref";
              else if (strArray1[index].ToUpper().Trim() == "HYPERLINK")
                str = "hyp";
              else if (strArray1[index].ToUpper().Trim() == "PROGRAM")
                str = "prg";
              xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@PicIndex='" + this.sPicIndex + "'][@ListIndex='" + this.sListIndex + "'][@Type='" + str + "'][@Update='" + strArray2[index] + "'][@PartNo='" + this.sPartNumber + "']");
            }
          }
          else
            xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@PicIndex='" + this.sPicIndex + "'][@ListIndex='" + this.sListIndex + "'][@PartNo='" + this.sPartNumber + "']");
          if (xmlNodeList != null && xmlNodeList.Count > 0)
          {
            foreach (XmlNode oldChild in xmlNodeList)
            {
              this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(oldChild);
              flag = true;
            }
          }
        }
        else if (this.objFrmMemoLocal.strMemoChangedType != "")
        {
          this.objFrmMemoLocal.strMemoChangedType = this.objFrmMemoLocal.strMemoChangedType.TrimEnd('|');
          if (this.objFrmMemoLocal.strMemoChangedType.Contains("|"))
          {
            string[] strArray1 = this.objFrmMemoLocal.strMemoChangedType.Split('|');
            this.objFrmMemoLocal.strDelMemoDate = this.objFrmMemoLocal.strDelMemoDate.TrimEnd('|');
            string[] strArray2 = this.objFrmMemoLocal.strDelMemoDate.Split('|');
            for (int index = 0; index < strArray1.Length; ++index)
            {
              string str = string.Empty;
              if (strArray1[index].ToUpper().Trim() == "TEXT")
                str = "txt";
              else if (strArray1[index].ToUpper().Trim() == "REFERENCE")
                str = "ref";
              else if (strArray1[index].ToUpper().Trim() == "HYPERLINK")
                str = "hyp";
              else if (strArray1[index].ToUpper().Trim() == "PROGRAM")
                str = "prg";
              if (this.objFrmMemoLocal.strDelMemoDate.ToString() != string.Empty)
              {
                if (!this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || this.objFrmMemoLocal.bMemoModifiedAtPartLevel)
                  xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@Type='" + str + "'][@Update='" + strArray2[index] + "'][@PartNo='" + this.sPartNumber + "']");
                else
                  xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@Update='" + strArray2[index] + "'][@PartNo='" + this.sPartNumber + "']");
              }
              else
                xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@PartNo='" + this.sPartNumber + "']");
              if (xmlNodeList != null)
              {
                foreach (XmlNode oldChild in xmlNodeList)
                  this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(oldChild);
                flag = true;
              }
            }
          }
          else
          {
            string str = string.Empty;
            if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "TEXT")
              str = "txt";
            else if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "REFERENCE")
              str = "ref";
            else if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "HYPERLINK")
              str = "hyp";
            else if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "PROGRAM")
              str = "prg";
            if (this.objFrmMemoLocal.strDelMemoDate.ToString() != string.Empty)
            {
              if (!this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || this.objFrmMemoLocal.bMemoModifiedAtPartLevel)
                xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@Type='" + str + "'][@Update='" + this.objFrmMemoLocal.strDelMemoDate.ToString() + "'][@PartNo='" + this.sPartNumber + "']");
              else
                xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@Update='" + this.objFrmMemoLocal.strDelMemoDate.ToString() + "'][@PartNo='" + this.sPartNumber + "']");
            }
            else
              xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@PartNo='" + this.sPartNumber + "']");
          }
        }
        if (!flag && xmlNodeList != null)
        {
          foreach (XmlNode oldChild in xmlNodeList)
            this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(oldChild);
        }
        this.objFrmMemoLocal.bMemoModifiedAtPartLevel = false;
        this.objFrmMemoLocal.strMemoChangedType = string.Empty;
        this.objFrmMemoLocal.strDelMemoDate = string.Empty;
      }
      else
      {
        if (!memoType.ToUpper().Equals("GLOBAL"))
          return;
        if (this.objFrmMemoGlobal.bMultiMemoChange)
        {
          if (this.objFrmMemoGlobal.strMemoChangedType != "")
          {
            string[] strArray1 = this.objFrmMemoGlobal.strMemoChangedType.Split('|');
            this.objFrmMemoGlobal.strDelMemoDate = this.objFrmMemoGlobal.strDelMemoDate.TrimEnd('|');
            string[] strArray2 = this.objFrmMemoGlobal.strDelMemoDate.Split('|');
            for (int index = 0; index < strArray1.Length; ++index)
            {
              string str = string.Empty;
              if (strArray1[index].ToUpper().Trim() == "TEXT")
                str = "txt";
              else if (strArray1[index].ToUpper().Trim() == "REFERENCE")
                str = "ref";
              else if (strArray1[index].ToUpper().Trim() == "HYPERLINK")
                str = "hyp";
              else if (strArray1[index].ToUpper().Trim() == "PROGRAM")
                str = "prg";
              xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@PicIndex='" + this.sPicIndex + "'][@ListIndex='" + this.sListIndex + "'][@Type='" + str + "'][@Update='" + strArray2[index] + "'][@PartNo='" + this.sPartNumber + "']");
            }
          }
          else
            xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@PicIndex='" + this.sPicIndex + "'][@ListIndex='" + this.sListIndex + "'][@PartNo='" + this.sPartNumber + "']");
        }
        else if (this.objFrmMemoGlobal.strMemoChangedType != "")
        {
          this.objFrmMemoGlobal.strMemoChangedType = this.objFrmMemoGlobal.strMemoChangedType.TrimEnd('|');
          if (this.objFrmMemoGlobal.strMemoChangedType.Contains("|"))
          {
            string[] strArray1 = this.objFrmMemoGlobal.strMemoChangedType.Split('|');
            this.objFrmMemoGlobal.strDelMemoDate = this.objFrmMemoGlobal.strDelMemoDate.TrimEnd('|');
            string[] strArray2 = this.objFrmMemoGlobal.strDelMemoDate.Split('|');
            for (int index = 0; index < strArray1.Length; ++index)
            {
              string str = string.Empty;
              if (strArray1[index].ToUpper().Trim() == "TEXT")
                str = "txt";
              else if (strArray1[index].ToUpper().Trim() == "REFERENCE")
                str = "ref";
              else if (strArray1[index].ToUpper().Trim() == "HYPERLINK")
                str = "hyp";
              else if (strArray1[index].ToUpper().Trim() == "PROGRAM")
                str = "prg";
              if (this.objFrmMemoGlobal.strDelMemoDate.ToString() != string.Empty)
              {
                if (!this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || this.objFrmMemoGlobal.bMemoModifiedAtPartLevel)
                  xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@Type='" + str + "'][@Update='" + strArray2[index] + "'][@PartNo='" + this.sPartNumber + "']");
                else
                  xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@Update='" + strArray2[index] + "'][@PartNo='" + this.sPartNumber + "']");
              }
              else
                xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@PartNo='" + this.sPartNumber + "']");
              if (xmlNodeList != null)
              {
                foreach (XmlNode oldChild in xmlNodeList)
                  this.frmParent.xDocGlobalMemo.DocumentElement.RemoveChild(oldChild);
                flag = true;
              }
            }
          }
          else
          {
            string str = string.Empty;
            if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "TEXT")
              str = "txt";
            else if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "REFERENCE")
              str = "ref";
            else if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "HYPERLINK")
              str = "hyp";
            else if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "PROGRAM")
              str = "prg";
            if (this.objFrmMemoGlobal.strDelMemoDate.ToString() != string.Empty)
            {
              if (!this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || this.objFrmMemoGlobal.bMemoModifiedAtPartLevel)
                xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@PageId='" + this.sPageId + "'][@Type='" + str + "'][@Update='" + this.objFrmMemoGlobal.strDelMemoDate.ToString() + "'][@PartNo='" + this.sPartNumber + "']");
              else
                xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@Update='" + this.objFrmMemoGlobal.strDelMemoDate.ToString() + "'][@PartNo='" + this.sPartNumber + "']");
            }
            else
              xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='" + str + "'][@PartNo='" + this.sPartNumber + "']");
          }
        }
        if (!flag && xmlNodeList != null)
        {
          foreach (XmlNode oldChild in xmlNodeList)
            this.frmParent.xDocGlobalMemo.DocumentElement.RemoveChild(oldChild);
        }
        this.objFrmMemoGlobal.bMemoModifiedAtPartLevel = false;
        this.objFrmMemoGlobal.strMemoChangedType = string.Empty;
        this.objFrmMemoGlobal.strDelMemoDate = string.Empty;
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MEMO']";
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

    private int GetMemoType()
    {
      try
      {
        return Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"] != null && Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"].ToString() == "2" ? 2 : 1;
      }
      catch (Exception ex)
      {
        return 1;
      }
    }

    private void SaveUserSettings()
    {
      Settings.Default.frmMemoLocation = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Location : this.Location;
      Settings.Default.frmMemoSize = this.WindowState != FormWindowState.Normal ? this.RestoreBounds.Size : this.Size;
      Settings.Default.frmMemoState = this.WindowState;
    }

    private void LoadUserSettings()
    {
      if (Settings.Default.frmMemoLocation.X == -1 && Settings.Default.frmMemoListLocation.Y == -1)
        this.CenterToParent();
      else
        this.Location = Settings.Default.frmMemoLocation;
      this.Size = Settings.Default.frmMemoSize;
      if (Settings.Default.frmMemoState == FormWindowState.Minimized)
        this.WindowState = FormWindowState.Normal;
      else
        this.WindowState = Settings.Default.frmMemoState;
    }

    private string[] FilterMemo(string[] arrMemoNodes, string strMemoType)
    {
      List<string> stringList = new List<string>();
      List<int> intList = new List<int>();
      try
      {
        if (arrMemoNodes == null)
          return (string[]) null;
        if (strMemoType == "LOCAL")
        {
          for (int index = 0; index < arrMemoNodes.Length; ++index)
          {
            XmlNodeList xmlNodeList = (XmlNodeList) null;
            XmlNode xmlNode1 = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(arrMemoNodes[index].ToString())));
            string upper = xmlNode1.Attributes["Type"].Value.ToString().ToUpper();
            if (upper == "TXT")
              xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='txt'][@PartNo='" + this.sPartNumber + "']");
            else if (upper == "REF")
              xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='ref'][@PartNo='" + this.sPartNumber + "']");
            else if (upper == "HYP")
              xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='hyp'][@PartNo='" + this.sPartNumber + "']");
            else if (upper == "PRG")
              xmlNodeList = this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='prg'][@PartNo='" + this.sPartNumber + "']");
            if (xmlNodeList != null && xmlNodeList.Count > 0)
            {
              bool flag = false;
              foreach (XmlNode xmlNode2 in xmlNodeList)
              {
                if (xmlNode2.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() && xmlNode2.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["BookId"].Value.ToString().Trim().ToUpper() && (xmlNode2.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["PartNo"].Value.ToString().Trim().ToUpper() && xmlNode2.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["Type"].Value.ToString().Trim().ToUpper()) && (xmlNode2.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["Value"].Value.ToString().Trim().ToUpper() && xmlNode2.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["Update"].Value.ToString().Trim().ToUpper()))
                {
                  intList.Add(index);
                  flag = true;
                  break;
                }
              }
              if (!flag)
                stringList.Add(xmlNode1.OuterXml);
            }
            else
              stringList.Add(xmlNode1.OuterXml);
          }
        }
        else
        {
          for (int index = 0; index < arrMemoNodes.Length; ++index)
          {
            XmlNodeList xmlNodeList = (XmlNodeList) null;
            XmlNode xmlNode1 = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(arrMemoNodes[index].ToString())));
            string upper = xmlNode1.Attributes["Type"].Value.ToString().ToUpper();
            if (upper == "TXT")
              xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='txt'][@PartNo='" + this.sPartNumber + "']");
            else if (upper == "REF")
              xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='ref'][@PartNo='" + this.sPartNumber + "']");
            else if (upper == "HYP")
              xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='hyp'][@PartNo='" + this.sPartNumber + "']");
            else if (upper == "PRG")
              xmlNodeList = this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo[@ServerKey='" + this.sServerKey + "'][@BookId='" + this.sBookId + "'][@Type='prg'][@PartNo='" + this.sPartNumber + "']");
            if (xmlNodeList != null && xmlNodeList.Count > 0)
            {
              bool flag = false;
              foreach (XmlNode xmlNode2 in xmlNodeList)
              {
                if (xmlNode2.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() && xmlNode2.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["BookId"].Value.ToString().Trim().ToUpper() && (xmlNode2.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["PartNo"].Value.ToString().Trim().ToUpper() && xmlNode2.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["Type"].Value.ToString().Trim().ToUpper()) && (xmlNode2.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["Value"].Value.ToString().Trim().ToUpper() && xmlNode2.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNode1.Attributes["Update"].Value.ToString().Trim().ToUpper()))
                {
                  intList.Add(index);
                  flag = true;
                  break;
                }
              }
              if (!flag)
                stringList.Add(xmlNode1.OuterXml);
            }
            else
              stringList.Add(xmlNode1.OuterXml);
          }
        }
        string[] strArray = new string[stringList.Count];
        for (int index = 0; index < stringList.Count; ++index)
          strArray[index] = stringList[index].ToString();
        return strArray;
      }
      catch (Exception ex)
      {
        return (string[]) null;
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
      this.toolStripContainer1 = new ToolStripContainer();
      this.splitPnlMemo = new SplitContainer();
      this.toolStripContainer1.ContentPanel.SuspendLayout();
      this.toolStripContainer1.SuspendLayout();
      this.splitPnlMemo.SuspendLayout();
      this.SuspendLayout();
      this.toolStripContainer1.ContentPanel.Controls.Add((Control) this.splitPnlMemo);
      this.toolStripContainer1.ContentPanel.Size = new Size(694, 333);
      this.toolStripContainer1.Dock = DockStyle.Fill;
      this.toolStripContainer1.Location = new Point(0, 0);
      this.toolStripContainer1.Name = "toolStripContainer1";
      this.toolStripContainer1.Size = new Size(694, 358);
      this.toolStripContainer1.TabIndex = 0;
      this.toolStripContainer1.Text = "toolStripContainer1";
      this.splitPnlMemo.Dock = DockStyle.Fill;
      this.splitPnlMemo.Location = new Point(0, 0);
      this.splitPnlMemo.MinimumSize = new Size(690, 331);
      this.splitPnlMemo.Name = "splitPnlMemo";
      this.splitPnlMemo.Panel1.AccessibleName = "pnlTasks";
      this.splitPnlMemo.Panel1MinSize = 160;
      this.splitPnlMemo.Panel2.AccessibleName = "pnlContents";
      this.splitPnlMemo.Panel2MinSize = 525;
      this.splitPnlMemo.Size = new Size(694, 333);
      this.splitPnlMemo.SplitterDistance = 162;
      this.splitPnlMemo.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Control;
      this.ClientSize = new Size(694, 358);
      this.Controls.Add((Control) this.toolStripContainer1);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.IsMdiContainer = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new Size(710, 396);
      this.Name = nameof (frmMemo);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Memo";
      this.Load += new EventHandler(this.frmMemo_Load);
      this.FormClosing += new FormClosingEventHandler(this.frmMemo_FormClosing);
      this.toolStripContainer1.ContentPanel.ResumeLayout(false);
      this.toolStripContainer1.ResumeLayout(false);
      this.toolStripContainer1.PerformLayout();
      this.splitPnlMemo.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}

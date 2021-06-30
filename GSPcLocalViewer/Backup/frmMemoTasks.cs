// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmMemoTasks
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmMemoTasks : Form
  {
    private string strMemoPriority = "LOCAL";
    private string strDateFormat = string.Empty;
    private TreeNode tnPreviouisNode = new TreeNode();
    private string strTextMemoState = "TRUE";
    private string strReferenceMemoState = "TRUE";
    private string strHyperlinkMemoState = "TRUE";
    private string strProgramMemoState = "TRUE";
    private IContainer components;
    private Panel pnlTasks;
    private Panel pnlTasks2;
    private Label lblTasksTitle;
    private Label lblView;
    private Label lblSpace1;
    private Label lblGlobalMemo;
    private Label lblLocalMemo;
    private Label lblAdminMemo;
    private Panel pnlMemoTypes;
    public TreeView tvMemoTypes;
    private frmMemo frmParent;
    public int intMemoType;
    private bool bDisabledMemo;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlTasks = new Panel();
      this.pnlMemoTypes = new Panel();
      this.tvMemoTypes = new TreeView();
      this.pnlTasks2 = new Panel();
      this.lblAdminMemo = new Label();
      this.lblGlobalMemo = new Label();
      this.lblLocalMemo = new Label();
      this.lblView = new Label();
      this.lblSpace1 = new Label();
      this.lblTasksTitle = new Label();
      this.pnlTasks.SuspendLayout();
      this.pnlMemoTypes.SuspendLayout();
      this.pnlTasks2.SuspendLayout();
      this.SuspendLayout();
      this.pnlTasks.BackColor = Color.White;
      this.pnlTasks.BorderStyle = BorderStyle.FixedSingle;
      this.pnlTasks.Controls.Add((Control) this.pnlMemoTypes);
      this.pnlTasks.Controls.Add((Control) this.pnlTasks2);
      this.pnlTasks.Controls.Add((Control) this.lblTasksTitle);
      this.pnlTasks.Dock = DockStyle.Fill;
      this.pnlTasks.ForeColor = Color.Black;
      this.pnlTasks.Location = new Point(0, 0);
      this.pnlTasks.Name = "pnlTasks";
      this.pnlTasks.Size = new Size(151, 392);
      this.pnlTasks.TabIndex = 0;
      this.pnlMemoTypes.BackColor = Color.White;
      this.pnlMemoTypes.Controls.Add((Control) this.tvMemoTypes);
      this.pnlMemoTypes.Dock = DockStyle.Fill;
      this.pnlMemoTypes.Location = new Point(0, 131);
      this.pnlMemoTypes.Name = "pnlMemoTypes";
      this.pnlMemoTypes.Size = new Size(149, 259);
      this.pnlMemoTypes.TabIndex = 0;
      this.tvMemoTypes.BorderStyle = BorderStyle.None;
      this.tvMemoTypes.Dock = DockStyle.Fill;
      this.tvMemoTypes.Location = new Point(0, 0);
      this.tvMemoTypes.Name = "tvMemoTypes";
      this.tvMemoTypes.Size = new Size(149, 259);
      this.tvMemoTypes.TabIndex = 0;
      this.tvMemoTypes.TabStop = false;
      this.tvMemoTypes.DrawNode += new DrawTreeNodeEventHandler(this.tvMemoTypes_DrawNode);
      this.tvMemoTypes.AfterSelect += new TreeViewEventHandler(this.tvMemoTypes_AfterSelect);
      this.tvMemoTypes.Click += new EventHandler(this.tvMemoTypes_Click);
      this.pnlTasks2.BackColor = Color.White;
      this.pnlTasks2.Controls.Add((Control) this.lblAdminMemo);
      this.pnlTasks2.Controls.Add((Control) this.lblGlobalMemo);
      this.pnlTasks2.Controls.Add((Control) this.lblLocalMemo);
      this.pnlTasks2.Controls.Add((Control) this.lblView);
      this.pnlTasks2.Controls.Add((Control) this.lblSpace1);
      this.pnlTasks2.Dock = DockStyle.Top;
      this.pnlTasks2.Location = new Point(0, 27);
      this.pnlTasks2.Name = "pnlTasks2";
      this.pnlTasks2.Padding = new Padding(15, 10, 15, 0);
      this.pnlTasks2.Size = new Size(149, 104);
      this.pnlTasks2.TabIndex = 0;
      this.lblAdminMemo.Cursor = Cursors.Hand;
      this.lblAdminMemo.Dock = DockStyle.Top;
      this.lblAdminMemo.Location = new Point(15, 80);
      this.lblAdminMemo.Name = "lblAdminMemo";
      this.lblAdminMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblAdminMemo.Size = new Size(119, 16);
      this.lblAdminMemo.TabIndex = 0;
      this.lblAdminMemo.Text = "Admin Memos";
      this.lblAdminMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdminMemo.Click += new EventHandler(this.lblAdminMemo_Click);
      this.lblGlobalMemo.Cursor = Cursors.Hand;
      this.lblGlobalMemo.Dock = DockStyle.Top;
      this.lblGlobalMemo.Location = new Point(15, 64);
      this.lblGlobalMemo.Name = "lblGlobalMemo";
      this.lblGlobalMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblGlobalMemo.Size = new Size(119, 16);
      this.lblGlobalMemo.TabIndex = 0;
      this.lblGlobalMemo.Text = "Global Memos";
      this.lblGlobalMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblGlobalMemo.Click += new EventHandler(this.lblGlobalMemo_Click);
      this.lblLocalMemo.Cursor = Cursors.Hand;
      this.lblLocalMemo.Dock = DockStyle.Top;
      this.lblLocalMemo.Location = new Point(15, 48);
      this.lblLocalMemo.Name = "lblLocalMemo";
      this.lblLocalMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblLocalMemo.Size = new Size(119, 16);
      this.lblLocalMemo.TabIndex = 0;
      this.lblLocalMemo.Text = "Local Memos";
      this.lblLocalMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblLocalMemo.Click += new EventHandler(this.lblLocalMemo_Click);
      this.lblView.BackColor = Color.Transparent;
      this.lblView.Dock = DockStyle.Top;
      this.lblView.ForeColor = Color.Blue;
      this.lblView.Image = (Image) Resources.GroupLine1;
      this.lblView.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblView.Location = new Point(15, 20);
      this.lblView.Name = "lblView";
      this.lblView.Size = new Size(119, 28);
      this.lblView.TabIndex = 0;
      this.lblView.Text = "View Options";
      this.lblView.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSpace1.Cursor = Cursors.Hand;
      this.lblSpace1.Dock = DockStyle.Top;
      this.lblSpace1.Location = new Point(15, 10);
      this.lblSpace1.Name = "lblSpace1";
      this.lblSpace1.Padding = new Padding(20, 0, 0, 0);
      this.lblSpace1.Size = new Size(119, 10);
      this.lblSpace1.TabIndex = 0;
      this.lblSpace1.TextAlign = ContentAlignment.MiddleLeft;
      this.lblTasksTitle.BackColor = Color.White;
      this.lblTasksTitle.Dock = DockStyle.Top;
      this.lblTasksTitle.ForeColor = Color.Black;
      this.lblTasksTitle.Location = new Point(0, 0);
      this.lblTasksTitle.Name = "lblTasksTitle";
      this.lblTasksTitle.Padding = new Padding(3, 7, 0, 0);
      this.lblTasksTitle.Size = new Size(149, 27);
      this.lblTasksTitle.TabIndex = 0;
      this.lblTasksTitle.Text = "Tasks";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(151, 392);
      this.Controls.Add((Control) this.pnlTasks);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmMemoTasks);
      this.Load += new EventHandler(this.frmMemoTasks_Load);
      this.pnlTasks.ResumeLayout(false);
      this.pnlMemoTypes.ResumeLayout(false);
      this.pnlTasks2.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmMemoTasks(frmMemo frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.UpdateFont();
      this.SetControlsText();
    }

    private void frmMemoTasks_Load(object sender, EventArgs e)
    {
      this.lblLocalMemo.Enabled = Settings.Default.EnableLocalMemo;
      this.lblGlobalMemo.Enabled = Settings.Default.EnableGlobalMemo;
      this.lblAdminMemo.Enabled = Settings.Default.EnableAdminMemo;
      this.GetMemoStates();
      this.intMemoType = this.GetMemoType();
      this.strMemoPriority = this.GetMemoPriority();
      this.strDateFormat = this.GetDateFormat();
      if (this.intMemoType == 2)
      {
        this.pnlTasks2.Visible = false;
        this.pnlMemoTypes.Visible = true;
        this.pnlTasks2.Dock = DockStyle.Bottom;
        if (!Settings.Default.EnableLocalMemo && !Settings.Default.EnableGlobalMemo && !Settings.Default.EnableAdminMemo)
        {
          this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoLocal.pnlBottom.Top = 33;
          this.frmParent.objFrmMemoLocal.Show();
          this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
        }
        else if (Settings.Default.EnableLocalMemo)
        {
          this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoLocal.pnlBottom.Top = 33;
          this.frmParent.objFrmMemoLocal.Show();
          this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
        }
        else if (Settings.Default.EnableGlobalMemo)
        {
          this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoGlobal.pnlBottom.Top = 33;
          this.frmParent.objFrmMemoGlobal.Show();
          this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
        }
        else if (Settings.Default.EnableAdminMemo)
        {
          this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
          this.frmParent.objFrmMemoAdmin.pnlBottom.Top = 33;
          this.frmParent.objFrmMemoAdmin.Show();
          this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
        }
        this.LoadMemo(true, true);
      }
      else
      {
        this.pnlMemoTypes.Visible = false;
        this.pnlTasks2.Visible = true;
        this.pnlTasks2.Dock = DockStyle.Top;
      }
    }

    private void lblGlobalMemo_Click(object sender, EventArgs e)
    {
      if (this.intMemoType == 2)
      {
        this.frmParent.frmParent.LoadMemos();
      }
      else
      {
        this.frmParent.frmParent.LoadMemos();
        this.frmParent.xnlGlobalMemo = this.frmParent.frmParent.reloadGlobalMemos(this.frmParent.sPartNumber);
        this.frmParent.objFrmMemoGlobal.reloadGlobalMemos();
      }
      this.HighlightList(ref this.lblGlobalMemo);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoGlobal.Show();
    }

    private void lblLocalMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblLocalMemo);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoLocal.Show();
    }

    private void lblAdminMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAdminMemo);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoAdmin.Show();
    }

    private void tvMemoTypes_AfterSelect(object sender, TreeViewEventArgs e)
    {
      try
      {
        this.tvMemoTypes.Refresh();
        if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "LOCALMEMO")
        {
          if (!Settings.Default.EnableLocalMemo)
          {
            this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
            this.bDisabledMemo = true;
          }
          else
          {
            this.frmParent.objFrmMemoLocal.ClearItems(true, false, false);
            this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
            this.frmParent.objFrmMemoLocal.pnlBottom.Top = 33;
            this.frmParent.objFrmMemoLocal.Show();
            this.lblLocalMemo_Click((object) null, (EventArgs) null);
            if (this.frmParent.objFrmMemoLocal.tsbAddTxtMemo.Checked && this.strTextMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
              this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
              this.frmParent.objFrmMemoLocal.tsbAddTxtMemo.Checked = true;
            }
            else if (this.frmParent.objFrmMemoLocal.tsbAddRefMemo.Checked && this.strReferenceMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("REFRENCE");
              this.frmParent.objFrmMemoLocal.tsbAddRefMemo.Checked = true;
              this.frmParent.objFrmMemoLocal.pnlRefMemo.BringToFront();
            }
            else if (this.frmParent.objFrmMemoLocal.tsbAddHypMemo.Checked && this.strHyperlinkMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
              this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
              this.frmParent.objFrmMemoLocal.tsbAddHypMemo.Checked = true;
            }
            else if (this.frmParent.objFrmMemoLocal.tsbAddPrgMemo.Checked && this.strProgramMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("PROGRAME");
              this.frmParent.objFrmMemoLocal.pnlPrgMemo.BringToFront();
              this.frmParent.objFrmMemoLocal.tsbAddPrgMemo.Checked = true;
            }
            else if (this.strTextMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
              this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
              this.frmParent.objFrmMemoLocal.tsbAddTxtMemo.Checked = true;
            }
            else if (this.strReferenceMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("REFRENCE");
              this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoLocal.tsbAddRefMemo.Checked = true;
              this.frmParent.objFrmMemoLocal.pnlRefMemo.BringToFront();
            }
            else if (this.strHyperlinkMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
              this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoLocal.tsbAddHypMemo.Checked = true;
              this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
            }
            else if (this.strProgramMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoLocal.SetTabProperty("PROGRAME");
              this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoLocal.tsbAddPrgMemo.Checked = true;
              this.frmParent.objFrmMemoLocal.pnlPrgMemo.BringToFront();
            }
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "GLOBALMEMO")
        {
          if (!Settings.Default.EnableGlobalMemo)
          {
            this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
            this.bDisabledMemo = true;
          }
          else
          {
            this.frmParent.objFrmMemoGlobal.ClearItems(true, false, false);
            this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
            this.frmParent.objFrmMemoGlobal.pnlBottom.Top = 33;
            this.frmParent.objFrmMemoGlobal.Show();
            this.lblGlobalMemo_Click((object) null, (EventArgs) null);
            if (this.frmParent.objFrmMemoGlobal.tsbAddTxtMemo.Checked && this.strTextMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
              this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
              this.frmParent.objFrmMemoGlobal.tsbAddTxtMemo.Checked = true;
            }
            else if (this.frmParent.objFrmMemoGlobal.tsbAddRefMemo.Checked && this.strReferenceMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("REFRENCE");
              this.frmParent.objFrmMemoGlobal.pnlRefMemo.BringToFront();
              this.frmParent.objFrmMemoGlobal.tsbAddRefMemo.Checked = true;
            }
            else if (this.frmParent.objFrmMemoGlobal.tsbAddHypMemo.Checked && this.strHyperlinkMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
              this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
              this.frmParent.objFrmMemoGlobal.tsbAddHypMemo.Checked = true;
            }
            else if (this.frmParent.objFrmMemoGlobal.tsbAddPrgMemo.Checked && this.strProgramMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("PROGRAME");
              this.frmParent.objFrmMemoGlobal.pnlPrgMemo.BringToFront();
              this.frmParent.objFrmMemoGlobal.tsbAddPrgMemo.Checked = true;
            }
            else if (this.strTextMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
              this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
              this.frmParent.objFrmMemoGlobal.tsbAddTxtMemo.Checked = true;
            }
            else if (this.strReferenceMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("REFRENCE");
              this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoGlobal.tsbAddRefMemo.Checked = true;
              this.frmParent.objFrmMemoGlobal.pnlRefMemo.BringToFront();
            }
            else if (this.strHyperlinkMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
              this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoGlobal.tsbAddHypMemo.Checked = true;
              this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
            }
            else if (this.strProgramMemoState.ToUpper() == "TRUE")
            {
              this.frmParent.objFrmMemoGlobal.SetTabProperty("PROGRAME");
              this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
              this.frmParent.objFrmMemoGlobal.tsbAddPrgMemo.Checked = true;
              this.frmParent.objFrmMemoGlobal.pnlPrgMemo.BringToFront();
            }
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "ADMINMEMO")
        {
          if (!Settings.Default.EnableAdminMemo)
          {
            this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
            this.bDisabledMemo = true;
          }
          else
          {
            this.frmParent.objFrmMemoAdmin.ClearItems(true, false, false);
            this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
            this.frmParent.objFrmMemoAdmin.pnlBottom.Top = 33;
            this.frmParent.objFrmMemoAdmin.Show();
            this.lblAdminMemo_Click((object) null, (EventArgs) null);
            if (this.frmParent.objFrmMemoAdmin.tsbAddHypMemo.Checked && this.strHyperlinkMemoState.ToUpper() == "TRUE")
            {
              if (this.strHyperlinkMemoState.ToUpper() == "DISABLED")
              {
                this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
                this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
              }
              this.frmParent.objFrmMemoAdmin.SetTabProperty("HYPERLINK");
              this.frmParent.objFrmMemoAdmin.pnlHypMemo.BringToFront();
            }
            else if (this.frmParent.objFrmMemoAdmin.tsbAddPrgMemo.Checked && this.strProgramMemoState.ToUpper() == "TRUE")
            {
              if (this.strProgramMemoState.ToUpper() == "DISABLED")
              {
                this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
                this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
              }
              this.frmParent.objFrmMemoAdmin.SetTabProperty("PROGRAME");
              this.frmParent.objFrmMemoAdmin.pnlPrgMemo.BringToFront();
            }
            else if (this.frmParent.objFrmMemoAdmin.tsbAddRefMemo.Checked && this.strReferenceMemoState.ToUpper() == "TRUE")
            {
              if (this.strReferenceMemoState.ToUpper() == "DISABLED")
              {
                this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
                this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
              }
              this.frmParent.objFrmMemoAdmin.SetTabProperty("REFRENCE");
              this.frmParent.objFrmMemoAdmin.pnlRefMemo.BringToFront();
            }
            else if (this.frmParent.objFrmMemoAdmin.tsbAddTxtMemo.Checked && this.strTextMemoState.ToUpper() == "TRUE")
            {
              if (this.strTextMemoState.ToUpper() == "DISABLED")
              {
                this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
                this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
              }
              this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
              this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
              this.frmParent.objFrmMemoAdmin.tsbAddTxtMemo.Checked = true;
            }
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "LOCALMEMO")
        {
          this.frmParent.objFrmMemoLocal.ClearItems(true, false, true);
          this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
          string[] strArray1 = this.tvMemoTypes.SelectedNode.Name.Split('|');
          string str1 = string.Empty;
          string str2 = strArray1[1];
          string str3 = strArray1[2];
          if (str3.ToUpper().Trim() == "HYPERLINK")
          {
            string dicLocalMemo = this.frmParent.objFrmMemoLocal.dicLocalMemoList[strArray1[4]];
            try
            {
              str1 = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(dicLocalMemo))).Attributes["Description"].Value.ToString();
            }
            catch (Exception ex)
            {
            }
          }
          else
            str1 = strArray1[0];
          string str4 = strArray1[3];
          if (str1.ToUpper().Contains("PIPESIGN"))
            str1 = str1.Replace("PIPESIGN", "|");
          if (str4.ToUpper().Contains("PIPESIGN"))
            str4 = str4.Replace("PIPESIGN", "|");
          if (strArray1.Length > 4 && str3.ToUpper() == "PROGRAM")
            str4 = str4 + "|" + strArray1[4];
          if (str3.ToUpper().Trim() == "TEXT" && this.strTextMemoState.ToUpper() != "FALSE")
          {
            if (this.strTextMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoLocal.Show();
            this.frmParent.objFrmMemoLocal.rtbTxtMemo.Text = str4;
            this.frmParent.objFrmMemoLocal.lblTxtMemoDate.Text = str2;
            this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
          }
          else if (str3.ToUpper().Trim() == "REFERENCE" && this.strReferenceMemoState.ToUpper() != "FALSE")
          {
            if (this.strReferenceMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoLocal.SetTabProperty("REFRENCE");
            this.frmParent.objFrmMemoLocal.Show();
            this.frmParent.objFrmMemoLocal.lblRefMemoDate.Text = str2;
            this.frmParent.objFrmMemoLocal.pnlRefMemo.BringToFront();
            string[] strArray2 = str4.Split(new string[1]
            {
              " "
            }, StringSplitOptions.None);
            if (strArray2.Length >= 2)
            {
              this.frmParent.objFrmMemoLocal.txtRefMemoServerKey.Text = strArray2[0];
              this.frmParent.objFrmMemoLocal.txtRefMemoBookId.Text = strArray2[1];
              this.frmParent.objFrmMemoLocal.txtRefMemoOtherRef.Text = string.Empty;
              for (int index = 2; index < strArray2.Length; ++index)
              {
                this.frmParent.objFrmMemoLocal.txtRefMemoOtherRef.Text += strArray2[index];
                if (index < strArray2.Length - 1)
                  this.frmParent.objFrmMemoLocal.txtRefMemoOtherRef.Text += " ";
              }
            }
            else
            {
              this.frmParent.objFrmMemoLocal.pnlError.BringToFront();
              return;
            }
          }
          else if (str3.ToUpper().Trim() == "HYPERLINK" && this.strHyperlinkMemoState.ToUpper() != "FALSE")
          {
            if (this.strHyperlinkMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
            this.frmParent.objFrmMemoLocal.Show();
            this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
            this.frmParent.objFrmMemoLocal.txtDescription.Text = str1;
            this.frmParent.objFrmMemoLocal.txtHypMemoUrl.Text = str4;
            this.frmParent.objFrmMemoLocal.lblHypMemoDate.Text = str2;
          }
          else if (str3.ToUpper().Trim() == "PROGRAM" && this.strProgramMemoState.ToUpper() != "FALSE")
          {
            if (this.strProgramMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoLocal.SetTabProperty("PROGRAME");
            this.frmParent.objFrmMemoLocal.Show();
            this.frmParent.objFrmMemoLocal.pnlPrgMemo.BringToFront();
            this.frmParent.objFrmMemoLocal.lblPrgMemoDate.Text = str2;
            string[] strArray2 = str4.Split(new string[1]
            {
              "|"
            }, StringSplitOptions.None);
            this.frmParent.objFrmMemoLocal.txtPrgMemoExePath.Text = strArray2[0];
            if (strArray2.Length > 1)
              this.frmParent.objFrmMemoLocal.txtPrgMemoCmdLine.Text = strArray2[1];
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "GLOBALMEMO")
        {
          this.frmParent.objFrmMemoGlobal.ClearItems(true, false, true);
          this.frmParent.objFrmMemoGlobal.Show();
          this.lblGlobalMemo_Click((object) null, (EventArgs) null);
          this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
          string[] strArray1 = this.tvMemoTypes.SelectedNode.Name.Split('|');
          string str1 = string.Empty;
          string str2 = strArray1[1];
          string str3 = strArray1[2];
          if (str3.ToUpper().Trim() == "HYPERLINK")
          {
            string dicGlobalMemo = this.frmParent.objFrmMemoGlobal.dicGlobalMemoList[strArray1[4]];
            try
            {
              str1 = new XmlDocument().ReadNode((XmlReader) new XmlTextReader((TextReader) new StringReader(dicGlobalMemo))).Attributes["Description"].Value.ToString();
            }
            catch (Exception ex)
            {
            }
          }
          else
            str1 = strArray1[0];
          string str4 = strArray1[3];
          if (str1.ToUpper().Contains("PIPESIGN"))
            str1 = str1.Replace("PIPESIGN", "|");
          if (str4.ToUpper().Contains("PIPESIGN"))
            str4 = str4.Replace("PIPESIGN", "|");
          if (strArray1.Length > 4 && str3.ToUpper() == "PROGRAM")
            str4 = str4 + "|" + strArray1[4];
          if (str3.ToUpper().Trim() == "TEXT" && this.strTextMemoState.ToUpper() != "FALSE")
          {
            if (this.strTextMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoGlobal.Show();
            this.frmParent.objFrmMemoGlobal.rtbTxtMemo.Text = str4;
            this.frmParent.objFrmMemoGlobal.lblTxtMemoDate.Text = str2;
            this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
          }
          else if (str3.ToUpper().Trim() == "REFERENCE" && this.strReferenceMemoState.ToUpper() != "FALSE")
          {
            if (this.strReferenceMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoGlobal.SetTabProperty("REFRENCE");
            this.frmParent.objFrmMemoGlobal.Show();
            this.frmParent.objFrmMemoGlobal.lblRefMemoDate.Text = str2;
            this.frmParent.objFrmMemoGlobal.pnlRefMemo.BringToFront();
            string[] strArray2 = str4.Split(new string[1]
            {
              " "
            }, StringSplitOptions.None);
            if (strArray2.Length >= 2)
            {
              this.frmParent.objFrmMemoGlobal.txtRefMemoServerKey.Text = strArray2[0];
              this.frmParent.objFrmMemoGlobal.txtRefMemoBookId.Text = strArray2[1];
              this.frmParent.objFrmMemoGlobal.txtRefMemoOtherRef.Text = string.Empty;
              for (int index = 2; index < strArray2.Length; ++index)
              {
                this.frmParent.objFrmMemoGlobal.txtRefMemoOtherRef.Text += strArray2[index];
                if (index < strArray2.Length - 1)
                  this.frmParent.objFrmMemoGlobal.txtRefMemoOtherRef.Text += " ";
              }
            }
            else
            {
              this.frmParent.objFrmMemoGlobal.pnlError.BringToFront();
              return;
            }
          }
          else if (str3.ToUpper().Trim() == "HYPERLINK" && this.strHyperlinkMemoState.ToUpper() != "FALSE")
          {
            if (this.strHyperlinkMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
            this.frmParent.objFrmMemoGlobal.Show();
            this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
            this.frmParent.objFrmMemoGlobal.txtDescription.Text = str1;
            this.frmParent.objFrmMemoGlobal.txtHypMemoUrl.Text = str4;
            this.frmParent.objFrmMemoGlobal.lblHypMemoDate.Text = str2;
          }
          else if (str3.ToUpper().Trim() == "PROGRAM" && this.strProgramMemoState.ToUpper() != "FALSE")
          {
            if (this.strProgramMemoState.ToUpper() == "DISABLED")
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
            }
            else
            {
              this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
              this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
            }
            this.frmParent.objFrmMemoGlobal.SetTabProperty("PROGRAME");
            this.frmParent.objFrmMemoGlobal.Show();
            this.frmParent.objFrmMemoGlobal.pnlPrgMemo.BringToFront();
            this.frmParent.objFrmMemoGlobal.lblPrgMemoDate.Text = str2;
            string[] strArray2 = str4.Split(new string[1]
            {
              "|"
            }, StringSplitOptions.None);
            this.frmParent.objFrmMemoGlobal.txtPrgMemoExePath.Text = strArray2[0];
            if (strArray2.Length > 1)
              this.frmParent.objFrmMemoGlobal.txtPrgMemoCmdLine.Text = strArray2[1];
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "ADMINMEMO")
        {
          this.frmParent.objFrmMemoAdmin.ClearItems(true, false, true);
          this.lblAdminMemo_Click((object) null, (EventArgs) null);
          this.frmParent.objFrmMemoAdmin.Show();
          this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
          this.frmParent.objFrmMemoAdmin.pnlBottom.Dock = DockStyle.Fill;
          string[] strArray1 = this.tvMemoTypes.SelectedNode.Name.Split('|');
          string str1 = strArray1[0];
          string str2 = strArray1[1];
          string str3 = strArray1[2];
          string str4 = strArray1[3];
          if (strArray1.Length > 4 && str3.ToUpper() == "PROGRAM")
            str4 = str4 + "|" + strArray1[4];
          if (str3.ToUpper().Trim() == "TEXT" && this.strTextMemoState.ToUpper() != "FALSE")
          {
            this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoAdmin.Show();
            this.frmParent.objFrmMemoAdmin.rtbTxtMemo.Text = str4;
            this.frmParent.objFrmMemoAdmin.lblTxtMemoDate.Text = str2;
            this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
          }
          else if (str3.ToUpper().Trim() == "REFERENCE" && this.strReferenceMemoState.ToUpper() != "FALSE")
          {
            this.frmParent.objFrmMemoAdmin.SetTabProperty("REFRENCE");
            this.frmParent.objFrmMemoAdmin.Show();
            this.frmParent.objFrmMemoAdmin.lblRefMemoDate.Text = str2;
            this.frmParent.objFrmMemoAdmin.pnlRefMemo.BringToFront();
            string[] strArray2 = str4.Split(new string[1]
            {
              " "
            }, StringSplitOptions.None);
            if (strArray2.Length >= 2)
            {
              this.frmParent.objFrmMemoAdmin.txtRefMemoServerKey.Text = strArray2[0];
              this.frmParent.objFrmMemoAdmin.txtRefMemoBookId.Text = strArray2[1];
              this.frmParent.objFrmMemoAdmin.txtRefMemoOtherRef.Text = string.Empty;
              for (int index = 2; index < strArray2.Length; ++index)
              {
                this.frmParent.objFrmMemoAdmin.txtRefMemoOtherRef.Text += strArray2[index];
                if (index < strArray2.Length - 1)
                  this.frmParent.objFrmMemoAdmin.txtRefMemoOtherRef.Text += " ";
              }
            }
            else
            {
              this.frmParent.objFrmMemoAdmin.pnlError.BringToFront();
              return;
            }
          }
          else if (str3.ToUpper().Trim() == "HYPERLINK" && this.strHyperlinkMemoState.ToUpper() != "FALSE")
          {
            this.frmParent.objFrmMemoAdmin.SetTabProperty("HYPERLINK");
            this.frmParent.objFrmMemoAdmin.Show();
            this.frmParent.objFrmMemoAdmin.pnlHypMemo.BringToFront();
            this.frmParent.objFrmMemoAdmin.txtHypMemoUrl.Text = str4;
            this.frmParent.objFrmMemoAdmin.txtDescription.Text = str1;
            this.frmParent.objFrmMemoAdmin.lblHypMemoDate.Text = str2;
          }
          else if (str3.ToUpper().Trim() == "PROGRAM" && this.strProgramMemoState.ToUpper() != "FALSE")
          {
            this.frmParent.objFrmMemoAdmin.SetTabProperty("PROGRAME");
            this.frmParent.objFrmMemoAdmin.Show();
            this.frmParent.objFrmMemoAdmin.pnlPrgMemo.BringToFront();
            this.frmParent.objFrmMemoAdmin.lblPrgMemoDate.Text = str2;
            string[] strArray2 = str4.Split(new string[1]
            {
              "|"
            }, StringSplitOptions.None);
            this.frmParent.objFrmMemoAdmin.txtPrgMemoExePath.Text = strArray2[0];
            if (strArray2.Length > 1)
              this.frmParent.objFrmMemoAdmin.txtPrgMemoCmdLine.Text = strArray2[1];
          }
        }
        this.tvMemoTypes.SelectedNode.EnsureVisible();
      }
      catch (Exception ex)
      {
      }
    }

    private void tvMemoTypes_Click(object sender, EventArgs e)
    {
      this.tnPreviouisNode = this.tvMemoTypes.SelectedNode;
    }

    private void tvMemoTypes_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
      string text = e.Node.Tag == null ? e.Node.Text : ((frmMemoTasks.TreeNodeText) e.Node.Tag).DisplayText;
      Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
      Color foreColor = e.Node.ForeColor;
      if (foreColor == Color.Empty)
        foreColor = e.Node.TreeView.ForeColor;
      if (e.Node == e.Node.TreeView.SelectedNode)
      {
        Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, this.tvMemoTypes.SelectedNode.Bounds.Width + 15, e.Bounds.Height);
        Color highlightText = SystemColors.HighlightText;
        e.Graphics.FillRectangle((Brush) new SolidBrush(Settings.Default.appHighlightBackColor), rect);
        TextRenderer.DrawText((IDeviceContext) e.Graphics, text, font, e.Bounds, Settings.Default.appHighlightForeColor, TextFormatFlags.VerticalCenter);
      }
      else
        TextRenderer.DrawText((IDeviceContext) e.Graphics, text, font, e.Bounds, foreColor, TextFormatFlags.VerticalCenter);
    }

    public void ShowTask(MemoTasks task)
    {
      switch (task)
      {
        case MemoTasks.Local:
          this.lblLocalMemo_Click((object) null, (EventArgs) null);
          break;
        case MemoTasks.Global:
          this.lblGlobalMemo_Click((object) null, (EventArgs) null);
          break;
        case MemoTasks.Admin:
          this.lblAdminMemo_Click((object) null, (EventArgs) null);
          break;
      }
    }

    private void HighlightList(ref Label lbl)
    {
      if (!lbl.Parent.Name.Equals(this.pnlTasks2.Name))
        return;
      for (int index = 0; index < this.pnlTasks2.Controls.Count; ++index)
      {
        if (this.pnlTasks2.Controls[index] == this.lblGlobalMemo | this.pnlTasks2.Controls[index] == this.lblLocalMemo | this.pnlTasks2.Controls[index] == this.lblAdminMemo)
        {
          this.pnlTasks2.Controls[index].BackColor = this.pnlTasks2.BackColor;
          this.pnlTasks2.Controls[index].ForeColor = this.pnlTasks2.ForeColor;
        }
      }
      lbl.BackColor = Settings.Default.appHighlightBackColor;
      lbl.ForeColor = Settings.Default.appHighlightForeColor;
    }

    private void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
      this.lblTasksTitle.Font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
    }

    private void SetControlsText()
    {
      this.lblTasksTitle.Text = this.GetResource("Tasks", "TASKS", ResourceType.LABEL);
      this.lblView.Text = this.GetResource("View Options", "VIEW_OPTIONS", ResourceType.LABEL);
      this.lblLocalMemo.Text = this.GetResource("Local Memos", "LOCAL_MEMO", ResourceType.LABEL);
      this.lblGlobalMemo.Text = this.GetResource("Global Memo", "GLOBAL_MEMO", ResourceType.LABEL);
      this.lblAdminMemo.Text = this.GetResource("Admin Memos", "ADMIN_MEMO", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MEMO']" + "/Screen[@Name='MEMO_TASKS']";
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

    private string GetMemoPriority()
    {
      try
      {
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"] == null)
          return "LOCAL";
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"].ToString().ToUpper() == "ADMIN")
        {
          this.strMemoPriority = "ADMIN";
          return this.strMemoPriority;
        }
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"].ToString().ToUpper() == "GLOBAL")
        {
          this.strMemoPriority = "GLOBAL";
          return this.strMemoPriority;
        }
        this.strMemoPriority = "LOCAL";
        return this.strMemoPriority;
      }
      catch (Exception ex)
      {
        return "LOCAL";
      }
    }

    private string GetDateFormat()
    {
      try
      {
        string str = string.Empty;
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"] != null)
        {
          if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() != "HIDDEN")
            str = Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString();
          else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() == "HIDDEN")
            str = "INVALID";
        }
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"] != null && Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString().ToUpper() != "HIDDEN")
          str = !(str.ToUpper() == "INVALID") ? str + " " + Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString() : Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString();
        this.strDateFormat = str;
        return this.strDateFormat;
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }

    private void GetMemoStates()
    {
      try
      {
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "TEXTMEMO"] != null)
          this.strTextMemoState = !(Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "TEXTMEMO"].ToString().ToUpper() == "DISABLED") ? (!(Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "TEXTMEMO"].ToString().ToUpper() == "FALSE") ? "TRUE" : "FALSE") : "DISABLED";
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "REFERENCEMEMO"] != null)
          this.strReferenceMemoState = !(Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "REFERENCEMEMO"].ToString().ToUpper() == "DISABLED") ? (!(Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "REFERENCEMEMO"].ToString().ToUpper() == "FALSE") ? "TRUE" : "FALSE") : "DISABLED";
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "HYPERLINKMEMO"] != null)
          this.strHyperlinkMemoState = !(Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "HYPERLINKMEMO"].ToString().ToUpper() == "DISABLED") ? (!(Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "HYPERLINKMEMO"].ToString().ToUpper() == "FALSE") ? "TRUE" : "FALSE") : "DISABLED";
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "PROGRAMMEMO"] == null)
          return;
        if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "PROGRAMMEMO"].ToString().ToUpper() == "DISABLED")
          this.strProgramMemoState = "DISABLED";
        else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "PROGRAMMEMO"].ToString().ToUpper() == "FALSE")
          this.strProgramMemoState = "FALSE";
        else
          this.strProgramMemoState = "TRUE";
      }
      catch (Exception ex)
      {
      }
    }

    private string MaxWidthString(Graphics graphics, Font font, List<string> list)
    {
      int num = 0;
      string str = string.Empty;
      foreach (string text in list)
      {
        TextRenderer.DrawText((IDeviceContext) graphics, text, font, new Point(0, 0), Color.Black);
        Size size = TextRenderer.MeasureText((IDeviceContext) graphics, text, font);
        if (num < size.Width)
        {
          num = size.Width;
          str = text;
        }
      }
      return str;
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

    private void LoadMemo(bool bPictureLevel, bool bPartLevel)
    {
      if (bPictureLevel && bPartLevel)
      {
        this.tvMemoTypes.Nodes.Clear();
        this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
        this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
        TreeNode node1 = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMO", ResourceType.LABEL));
        node1.Name = "LocalMemo";
        TreeNode node2 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL));
        node2.Name = "GlobalMemo";
        TreeNode node3 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMO", ResourceType.LABEL));
        node3.Name = "AdminMemo";
        if (this.strMemoPriority.ToUpper().Trim() == "LOCAL")
        {
          this.tvMemoTypes.Nodes.Add(node1);
          this.tvMemoTypes.Nodes.Add(node2);
          this.tvMemoTypes.Nodes.Add(node3);
        }
        else if (this.strMemoPriority.ToUpper().Trim() == "GLOBAL")
        {
          this.tvMemoTypes.Nodes.Add(node2);
          this.tvMemoTypes.Nodes.Add(node1);
          this.tvMemoTypes.Nodes.Add(node3);
        }
        else if (this.strMemoPriority.ToUpper().Trim() == "ADMIN")
        {
          this.tvMemoTypes.Nodes.Add(node3);
          this.tvMemoTypes.Nodes.Add(node1);
          this.tvMemoTypes.Nodes.Add(node2);
        }
        Graphics graphics = this.tvMemoTypes.CreateGraphics();
        Font font = this.tvMemoTypes.Font;
        try
        {
          if (Settings.Default.EnableLocalMemo)
          {
            if (this.frmParent.xnlLocalMemo != null)
            {
              if (this.frmParent.xnlLocalMemo.Count > 0)
              {
                foreach (XmlNode xNode in this.frmParent.xnlLocalMemo)
                {
                  List<string> memoDetails = this.GetMemoDetails(xNode);
                  Guid guid = Guid.NewGuid();
                  string type = string.Empty;
                  if (memoDetails[2].ToUpper() == "TEXT")
                  {
                    if (!(this.strTextMemoState.ToUpper() == "FALSE"))
                      type = "txt";
                    else
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "REFERENCE")
                  {
                    if (!(this.strReferenceMemoState.ToUpper() == "FALSE"))
                      type = "ref";
                    else
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "HYPERLINK")
                  {
                    if (!(this.strHyperlinkMemoState.ToUpper() == "FALSE"))
                      type = "hyp";
                    else
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "PROGRAM")
                  {
                    if (!(this.strProgramMemoState.ToUpper() == "FALSE"))
                      type = "prg";
                    else
                      continue;
                  }
                  string outerXml = this.frmParent.objFrmMemoLocal.CreateMemoNode(type, memoDetails[3], memoDetails[1]).OuterXml;
                  if (type == "hyp")
                    outerXml = this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, memoDetails[3], memoDetails[1], memoDetails[0]).OuterXml;
                  this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(guid.ToString(), outerXml);
                  string s = memoDetails[1].ToString();
                  try
                  {
                    DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                    string[] strArray = new string[4]
                    {
                      "yyyy/MM/dd hh:mm:ss",
                      "MM/dd hh:mm",
                      "yyyy/MM/dd",
                      "hh:mm:ss"
                    };
                    if (this.strDateFormat != string.Empty)
                    {
                      if (this.strDateFormat.ToUpper() == "INVALID")
                      {
                        s = "Unknown";
                      }
                      else
                      {
                        foreach (string str in strArray)
                        {
                          if (this.strDateFormat == str)
                            s = exact.ToString(this.strDateFormat);
                        }
                      }
                    }
                  }
                  catch
                  {
                  }
                  memoDetails[1] = s;
                  List<frmMemoTasks.TreeNodeText> treeNodeTextList = new List<frmMemoTasks.TreeNodeText>();
                  string empty = string.Empty;
                  string text;
                  if (type == "hyp")
                  {
                    if (memoDetails[0].Length > 25)
                      text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  }
                  else
                    text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  treeNodeTextList.Add(new frmMemoTasks.TreeNodeText(text));
                  foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTextList)
                  {
                    if (memoDetails[0].Contains("|"))
                      memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                    if (memoDetails[3].Contains("|"))
                      memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                    this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(new TreeNode()
                    {
                      Name = memoDetails[0] + "|" + memoDetails[1] + "|" + memoDetails[2] + "|" + memoDetails[3] + "|" + guid.ToString(),
                      Tag = (object) treeNodeText,
                      Text = this.MaxWidthString(graphics, font, treeNodeText.TextList)
                    });
                  }
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (Settings.Default.EnableGlobalMemo)
          {
            if (this.frmParent.xnlGlobalMemo != null)
            {
              if (this.frmParent.xnlGlobalMemo.Count > 0)
              {
                foreach (XmlNode xNode in this.frmParent.xnlGlobalMemo)
                {
                  List<string> memoDetails = this.GetMemoDetails(xNode);
                  Guid guid = Guid.NewGuid();
                  string type = string.Empty;
                  if (memoDetails[2].ToUpper() == "TEXT")
                  {
                    if (!(this.strTextMemoState.ToUpper() == "FALSE"))
                      type = "txt";
                    else
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "REFERENCE")
                  {
                    if (!(this.strReferenceMemoState.ToUpper() == "FALSE"))
                      type = "ref";
                    else
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "HYPERLINK")
                  {
                    if (!(this.strHyperlinkMemoState.ToUpper() == "FALSE"))
                      type = "hyp";
                    else
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "PROGRAM")
                  {
                    if (!(this.strProgramMemoState.ToUpper() == "FALSE"))
                      type = "prg";
                    else
                      continue;
                  }
                  string outerXml = this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, memoDetails[3], memoDetails[1]).OuterXml;
                  if (type == "hyp")
                    outerXml = this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, memoDetails[3], memoDetails[1], memoDetails[0]).OuterXml;
                  this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(guid.ToString(), outerXml);
                  string s = memoDetails[1].ToString();
                  try
                  {
                    DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                    string[] strArray = new string[4]
                    {
                      "yyyy/MM/dd hh:mm:ss",
                      "MM/dd hh:mm",
                      "yyyy/MM/dd",
                      "hh:mm:ss"
                    };
                    if (this.strDateFormat != string.Empty)
                    {
                      if (this.strDateFormat.ToUpper() == "INVALID")
                      {
                        s = "Unknown";
                      }
                      else
                      {
                        foreach (string str in strArray)
                        {
                          if (this.strDateFormat == str)
                            s = exact.ToString(this.strDateFormat);
                        }
                      }
                    }
                  }
                  catch
                  {
                  }
                  memoDetails[1] = s;
                  List<frmMemoTasks.TreeNodeText> treeNodeTextList = new List<frmMemoTasks.TreeNodeText>();
                  string empty = string.Empty;
                  string text;
                  if (type == "hyp")
                  {
                    if (memoDetails[0].Length > 25)
                      text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  }
                  else
                    text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  treeNodeTextList.Add(new frmMemoTasks.TreeNodeText(text));
                  foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTextList)
                  {
                    if (memoDetails[0].Contains("|"))
                      memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                    if (memoDetails[3].Contains("|"))
                      memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                    this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(new TreeNode()
                    {
                      Name = memoDetails[0] + "|" + memoDetails[1] + "|" + memoDetails[2] + "|" + memoDetails[3] + "|" + guid.ToString(),
                      Tag = (object) treeNodeText,
                      Text = this.MaxWidthString(graphics, font, treeNodeText.TextList)
                    });
                  }
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (Settings.Default.EnableAdminMemo)
          {
            if (this.frmParent.xnladminMemo != null)
            {
              if (this.frmParent.xnladminMemo.Count > 0)
              {
                foreach (XmlNode xNode in this.frmParent.xnladminMemo)
                {
                  List<string> memoDetails = this.GetMemoDetails(xNode);
                  if (memoDetails[2].ToUpper() == "TEXT")
                  {
                    if (this.strTextMemoState.ToUpper() == "FALSE")
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "REFERENCE")
                  {
                    if (this.strReferenceMemoState.ToUpper() == "FALSE")
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "HYPERLINK")
                  {
                    if (this.strHyperlinkMemoState.ToUpper() == "FALSE")
                      continue;
                  }
                  else if (memoDetails[2].ToUpper() == "PROGRAM" && this.strProgramMemoState.ToUpper() == "FALSE")
                    continue;
                  string s = memoDetails[1].ToString();
                  try
                  {
                    DateTime exact = DateTime.ParseExact(s, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
                    string[] strArray = new string[4]
                    {
                      "yyyy/MM/dd hh:mm:ss",
                      "MM/dd hh:mm",
                      "yyyy/MM/dd",
                      "hh:mm:ss"
                    };
                    if (this.strDateFormat != string.Empty)
                    {
                      if (this.strDateFormat.ToUpper() == "INVALID")
                      {
                        s = "Unknown";
                      }
                      else
                      {
                        foreach (string str in strArray)
                        {
                          if (this.strDateFormat == str)
                            s = exact.ToString(this.strDateFormat);
                        }
                      }
                    }
                  }
                  catch
                  {
                  }
                  memoDetails[1] = s;
                  List<frmMemoTasks.TreeNodeText> treeNodeTextList = new List<frmMemoTasks.TreeNodeText>();
                  string text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  treeNodeTextList.Add(new frmMemoTasks.TreeNodeText(text));
                  foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTextList)
                    this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Add(new TreeNode()
                    {
                      Name = memoDetails[0] + "|" + memoDetails[1] + "|" + memoDetails[2] + "|" + memoDetails[3],
                      Tag = (object) treeNodeText,
                      Text = this.MaxWidthString(graphics, font, treeNodeText.TextList)
                    });
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
      if (this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Count > 0 && Settings.Default.EnableLocalMemo && this.strMemoPriority.ToUpper().Trim() == "LOCAL")
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["LocalMemo"].Nodes[0];
      else if (this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Count > 0 && Settings.Default.EnableGlobalMemo && this.strMemoPriority.ToUpper().Trim() == "GLOBAL")
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["GlobalMemo"].Nodes[0];
      else if (this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Count > 0 && Settings.Default.EnableAdminMemo && this.strMemoPriority.ToUpper().Trim() == "ADMIN")
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["AdminMemo"].Nodes[0];
      else if (this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Count > 0 && Settings.Default.EnableLocalMemo)
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["LocalMemo"].Nodes[0];
      else if (this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Count > 0 && Settings.Default.EnableGlobalMemo)
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["GlobalMemo"].Nodes[0];
      else if (this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Count > 0 && Settings.Default.EnableAdminMemo)
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["AdminMemo"].Nodes[0];
      else if (this.frmParent.frmParent.bFromPopup)
      {
        try
        {
          if (this.frmParent == null)
            return;
          this.frmParent.bNoMemos = true;
        }
        catch (Exception ex)
        {
          this.frmParent.bNoMemos = true;
          this.Close();
        }
      }
      else
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes[0];
    }

    public string GetMultilingualValue(string text)
    {
      string empty = string.Empty;
      string str;
      try
      {
        switch (text.ToUpper())
        {
          case "TEXT":
            str = this.GetResource("Text", "TEXT_MEMO", ResourceType.LABEL);
            break;
          case "REFERENCE":
            str = this.GetResource("Refrence", "REFERENCE_MEMO", ResourceType.LABEL);
            break;
          case "HYPERLINK":
            str = this.GetResource("Hyperlink", "HYPERLINK_MEMO", ResourceType.LABEL);
            break;
          case "PROGRAM":
            str = this.GetResource("Program", "PROGRAM_MEMO", ResourceType.LABEL);
            break;
          default:
            str = text;
            break;
        }
      }
      catch (Exception ex)
      {
        return text;
      }
      return str;
    }

    private List<string> GetMemoDetails(XmlNode xNode)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      List<string> stringList = new List<string>();
      if (xNode.Attributes["Value"] == null || !(xNode.Attributes["Value"].Value.Trim() != string.Empty))
        return stringList;
      string str1 = xNode.Attributes["Value"].Value.Trim();
      string str2 = xNode.Attributes["Value"].Value.Trim().Length <= 25 ? xNode.Attributes["Value"].Value.Trim() : (!xNode.Attributes["Value"].Value.Contains("\n") ? xNode.Attributes["Value"].Value.Trim().Substring(0, 25) + "..." : xNode.Attributes["Value"].Value.Replace("\n", " ").Substring(0, 25) + "...");
      if (xNode.Attributes["Type"] == null || !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "TXT") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "REF") && (!(xNode.Attributes["Type"].Value.Trim().ToUpper() == "HYP") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "PRG")))
        return stringList;
      string str3 = !xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("TXT") ? (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("REF") ? (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("HYP") ? (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("PRG") ? "Unknown" : "Program") : "Hyperlink") : "Reference") : "Text";
      if (!(str3.ToUpper() == "TEXT") && !(str3.ToUpper() == "HYPERLINK"))
      {
        if (str2.Contains("||"))
          str2 = str2.Replace("||", " ");
        if (str2.Contains("|"))
          str2 = str2.Replace("|", " ");
      }
      if (str3 == "Hyperlink")
      {
        if (xNode.Attributes["Description"] != null)
        {
          str1 = xNode.Attributes["Value"].Value.Trim();
          str2 = xNode.Attributes["Description"].Value.Trim().Length <= 25 ? xNode.Attributes["Description"].Value.Trim() : (!xNode.Attributes["Description"].Value.Contains("\n") ? xNode.Attributes["Description"].Value.Trim() : xNode.Attributes["Description"].Value.Replace("\n", " "));
        }
        else
          str2 = "";
      }
      string str4 = xNode.Attributes["Update"] == null ? "Unknown" : (!(xNode.Attributes["Update"].Value.Trim() != string.Empty) ? "Unknown" : xNode.Attributes["Update"].Value.Trim());
      stringList.Add(str2);
      stringList.Add(str4);
      stringList.Add(str3);
      stringList.Add(str1);
      return stringList;
    }

    private int GetTreevieeItemHeight()
    {
      try
      {
        Font appFont = Settings.Default.appFont;
        Graphics graphics = Graphics.FromImage((Image) new Bitmap(2200, 2200));
        SizeF sizeF = new SizeF();
        return (int) graphics.MeasureString("SAMPLE LINE", appFont).Height * 3;
      }
      catch (Exception ex)
      {
        return 45;
      }
    }

    public void AddMemoToTree(string type, string value, string strMemoTyp, string strHypDescription)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string str1 = value.Length <= 25 ? value : value.Substring(0, 25) + "...";
      if (!type.ToUpper().Equals("TXT") && str1.Contains("|"))
        str1 = str1.Replace("|", " ");
      if (type == "hyp")
        str1 = strHypDescription.Length <= 25 ? strHypDescription : strHypDescription.Substring(0, 25) + "...";
      string text1 = !type.ToUpper().Equals("TXT") ? (!type.ToUpper().Equals("REF") ? (!type.ToUpper().Equals("HYP") ? (!type.ToUpper().Equals("PRG") ? "Unknown" : "Program") : "Hyperlink") : "Reference") : "Text";
      string str2 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
      Guid guid = Guid.NewGuid();
      string empty4 = string.Empty;
      try
      {
        if (strMemoTyp == "LocalMemo")
        {
          this.frmParent.objFrmMemoLocal.iMemoResultCounter = 0;
          if (this.frmParent.objFrmMemoLocal.bSaveMemoOnBookLevel && this.frmParent.objFrmMemoLocal.chkSaveBookLevelMemos.Checked && this.frmParent.sPartNumber != string.Empty)
          {
            for (int index = 0; index < this.frmParent.objFrmMemoLocal.lstResults.Count; ++index)
            {
              guid = Guid.NewGuid();
              string str3 = !(type == "hyp") ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
              this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(guid.ToString(), str3);
              ++this.frmParent.objFrmMemoLocal.iMemoResultCounter;
            }
          }
          else
          {
            string str3 = !(type == "hyp") ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
            this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(guid.ToString(), str3);
          }
        }
        else
        {
          this.frmParent.objFrmMemoGlobal.iMemoResultCounter = 0;
          if (this.frmParent.objFrmMemoGlobal.bSaveMemoOnBookLevel && this.frmParent.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked && this.frmParent.sPartNumber != string.Empty)
          {
            for (int index = 0; index < this.frmParent.objFrmMemoGlobal.lstResults.Count; ++index)
            {
              guid = Guid.NewGuid();
              string str3 = !(type == "hyp") ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
              this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(guid.ToString(), str3);
              ++this.frmParent.objFrmMemoGlobal.iMemoResultCounter;
            }
          }
          else
          {
            string str3 = !(type == "hyp") ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
            this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(guid.ToString(), str3);
          }
        }
      }
      catch (Exception ex)
      {
      }
      try
      {
        DateTime exact = DateTime.ParseExact(str2, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
        string[] strArray = new string[4]
        {
          "yyyy/MM/dd hh:mm:ss",
          "MM/dd hh:mm",
          "yyyy/MM/dd",
          "hh:mm:ss"
        };
        if (this.strDateFormat != string.Empty)
        {
          if (this.strDateFormat.ToUpper() == "INVALID")
          {
            str2 = "Unknown";
          }
          else
          {
            foreach (string str3 in strArray)
            {
              if (this.strDateFormat == str3)
                str2 = exact.ToString(this.strDateFormat);
            }
          }
        }
      }
      catch
      {
      }
      this.frmParent.objFrmTasks.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
      Graphics graphics = this.frmParent.objFrmTasks.tvMemoTypes.CreateGraphics();
      Font font = this.frmParent.objFrmTasks.tvMemoTypes.Font;
      List<frmMemoTasks.TreeNodeText> treeNodeTextList = new List<frmMemoTasks.TreeNodeText>();
      string text2 = str1 + "\r\n" + str2 + "\r\n" + this.GetMultilingualValue(text1);
      treeNodeTextList.Add(new frmMemoTasks.TreeNodeText(text2));
      foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTextList)
      {
        if (str1.Contains("|"))
          str1 = str1.Replace("|", "PIPESIGN");
        if (value.Contains("|"))
          value = value.Replace("|", "PIPESIGN");
        TreeNode node = new TreeNode();
        node.Name = str1 + "|" + str2 + "|" + text1 + "|" + value + "|" + guid.ToString();
        node.Tag = (object) treeNodeText;
        node.Text = this.MaxWidthString(graphics, font, treeNodeText.TextList);
        if (strMemoTyp == "LocalMemo")
        {
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(node);
          this.frmParent.objFrmMemoLocal.bMemoChanged = true;
        }
        else
        {
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(node);
          this.frmParent.objFrmMemoGlobal.bMemoChanged = true;
        }
      }
      this.tvMemoTypes.ExpandAll();
      this.tvMemoTypes.Refresh();
      this.tvMemoTypes.Focus();
      this.tvMemoTypes.SelectedNode.EnsureVisible();
    }

    public void UpdateMemoToTree(string type, string value, string strMemoTyp, string strHypDescription)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string str1 = value.Length <= 25 ? value : value.Substring(0, 25) + "...";
      if (!(type.ToUpper() == "TXT") && !(type.ToUpper() == "HYP") && str1.Contains("|"))
        str1 = str1.Replace("|", " ");
      string text1 = !type.ToUpper().Equals("TXT") ? (!type.ToUpper().Equals("REF") ? (!type.ToUpper().Equals("HYP") ? (!type.ToUpper().Equals("PRG") ? "Unknown" : "Program") : "Hyperlink") : "Reference") : "Text";
      if (type == "hyp")
        str1 = strHypDescription.Length <= 25 ? strHypDescription : strHypDescription.Substring(0, 25) + "...";
      string str2 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
      string empty4 = string.Empty;
      string str3 = string.Empty;
      string str4 = string.Empty;
      try
      {
        if (type != "prg")
        {
          empty4 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name.Split('|')[4];
          str3 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name.Split('|')[3];
          str4 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name.Split('|')[0];
        }
        else
        {
          empty4 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name.Split('|')[4];
          str3 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name.Split('|')[3];
          str4 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name.Split('|')[0];
        }
      }
      catch (Exception ex)
      {
      }
      if (str4.ToUpper().Contains("PIPESIGN"))
        str4 = str4.Replace("PIPESIGN", "|");
      if (str3.ToUpper().Contains("PIPESIGN"))
        str3 = str3.Replace("PIPESIGN", "|");
      bool flag = false;
      if (type == "hyp" && str4 != strHypDescription)
        flag = true;
      if (!(value != str3) && !flag)
        return;
      string empty5 = string.Empty;
      if (strMemoTyp == "LocalMemo")
      {
        if (this.frmParent.objFrmMemoLocal.bSaveMemoOnBookLevel && this.frmParent.objFrmMemoLocal.chkSaveBookLevelMemos.Checked && this.frmParent.sPartNumber != string.Empty)
        {
          for (int index = 0; index < this.frmParent.objFrmMemoLocal.lstResults.Count; ++index)
          {
            string str5 = !(type == "hyp") ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
            this.frmParent.objFrmMemoLocal.dicLocalMemoList.Remove(empty4.ToString());
            this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(empty4.ToString(), str5);
            ++this.frmParent.objFrmMemoLocal.iMemoResultCounter;
          }
        }
        else
        {
          string str5 = !(type == "hyp") ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
          this.frmParent.objFrmMemoLocal.dicLocalMemoList.Remove(empty4.ToString());
          this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(empty4.ToString(), str5);
        }
      }
      else if (this.frmParent.objFrmMemoGlobal.bSaveMemoOnBookLevel && this.frmParent.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked && this.frmParent.sPartNumber != string.Empty)
      {
        for (int index = 0; index < this.frmParent.objFrmMemoGlobal.lstResults.Count; ++index)
        {
          string str5 = !(type == "hyp") ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
          this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Remove(empty4.ToString());
          this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(empty4.ToString(), str5);
          ++this.frmParent.objFrmMemoGlobal.iMemoResultCounter;
        }
      }
      else
      {
        string str5 = !(type == "hyp") ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, str2).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, str2, strHypDescription).OuterXml;
        this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Remove(empty4.ToString());
        this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(empty4.ToString(), str5);
      }
      if (strMemoTyp == "LocalMemo")
      {
        if (this.frmParent.objFrmMemoLocal.strMemoChangedType != string.Empty)
        {
          this.frmParent.objFrmMemoLocal.strMemoChangedType += "|";
          this.frmParent.objFrmMemoLocal.strMemoChangedType += text1.ToUpper();
        }
        else
          this.frmParent.objFrmMemoLocal.strMemoChangedType = text1.ToUpper();
      }
      else if (this.frmParent.objFrmMemoLocal.strMemoChangedType != string.Empty)
      {
        this.frmParent.objFrmMemoGlobal.strMemoChangedType += "|";
        this.frmParent.objFrmMemoGlobal.strMemoChangedType += text1.ToUpper();
      }
      else
        this.frmParent.objFrmMemoGlobal.strMemoChangedType = text1.ToUpper();
      try
      {
        DateTime exact = DateTime.ParseExact(str2, "dd/MM/yyyy HH:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture);
        string[] strArray = new string[4]
        {
          "yyyy/MM/dd hh:mm:ss",
          "MM/dd hh:mm",
          "yyyy/MM/dd",
          "hh:mm:ss"
        };
        if (this.strDateFormat != string.Empty)
        {
          if (this.strDateFormat.ToUpper() == "INVALID")
          {
            str2 = "Unknown";
          }
          else
          {
            foreach (string str5 in strArray)
            {
              if (this.strDateFormat == str5)
                str2 = exact.ToString(this.strDateFormat);
            }
          }
        }
      }
      catch
      {
      }
      this.frmParent.objFrmTasks.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
      Graphics graphics = this.frmParent.objFrmTasks.tvMemoTypes.CreateGraphics();
      Font font = this.frmParent.objFrmTasks.tvMemoTypes.Font;
      List<frmMemoTasks.TreeNodeText> treeNodeTextList = new List<frmMemoTasks.TreeNodeText>();
      string text2 = str1 + "\r\n" + str2 + "\r\n" + this.GetMultilingualValue(text1);
      treeNodeTextList.Add(new frmMemoTasks.TreeNodeText(text2));
      foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTextList)
      {
        if (str1.Contains("|"))
          str1 = str1.Replace("|", "PIPESIGN");
        if (value.Contains("|"))
          value = value.Replace("|", "PIPESIGN");
        TreeNode treeNode = new TreeNode();
        treeNode.Name = str1 + "|" + str2 + "|" + text1 + "|" + value + "|" + empty4.ToString();
        treeNode.Tag = (object) treeNodeText;
        treeNode.Text = this.MaxWidthString(graphics, font, treeNodeText.TextList);
        if (strMemoTyp == "LocalMemo")
        {
          int index = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Index;
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes[index].Name = treeNode.Name;
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes[index].Tag = treeNode.Tag;
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes[index].Text = treeNode.Text;
          this.frmParent.objFrmMemoLocal.bMemoChanged = true;
        }
        else if (strMemoTyp == "GlobalMemo")
        {
          int index = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Index;
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes[index].Name = treeNode.Name;
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes[index].Tag = treeNode.Tag;
          this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes[index].Text = treeNode.Text;
          this.frmParent.objFrmMemoGlobal.bMemoChanged = true;
        }
      }
      this.tvMemoTypes.Update();
      this.tvMemoTypes.ExpandAll();
      this.tvMemoTypes.Focus();
      this.tvMemoTypes.SelectedNode.EnsureVisible();
    }

    private class TreeNodeText
    {
      private List<string> textList = new List<string>();

      public TreeNodeText(string text)
      {
        string str1 = text;
        string[] separator = new string[2]{ "\r\n", "\n" };
        int num = 1;
        foreach (string str2 in str1.Split(separator, (StringSplitOptions) num))
          this.textList.Add(str2);
      }

      public List<string> TextList
      {
        get
        {
          return this.textList;
        }
        set
        {
          this.textList = value;
        }
      }

      public string DisplayText
      {
        get
        {
          return string.Join("\r\n", this.textList.ToArray());
        }
      }
    }
  }
}

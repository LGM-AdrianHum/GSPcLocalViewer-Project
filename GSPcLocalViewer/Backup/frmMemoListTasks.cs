// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmMemoListTasks
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
  public class frmMemoListTasks : Form
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
    private Label lblPartMemo;
    private Label lblPictureMemo;
    private Label lblBothMemo;
    private Panel pnlTasks1;
    private Label lblAllMemo;
    private Label lblGlobalMemo;
    private Label lblLocalMemo;
    private Label lblTypes;
    private Label label5;
    private Label lblAdminMemo;
    private Panel pnlMemoTypes;
    public TreeView tvMemoTypes;
    private frmMemoList frmParent;
    public int intMemoType;
    private bool bMemoLoadedFirstTime;

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
      this.lblBothMemo = new Label();
      this.lblPartMemo = new Label();
      this.lblPictureMemo = new Label();
      this.lblView = new Label();
      this.lblSpace1 = new Label();
      this.pnlTasks1 = new Panel();
      this.label5 = new Label();
      this.lblAllMemo = new Label();
      this.lblAdminMemo = new Label();
      this.lblGlobalMemo = new Label();
      this.lblLocalMemo = new Label();
      this.lblTypes = new Label();
      this.lblTasksTitle = new Label();
      this.pnlTasks.SuspendLayout();
      this.pnlMemoTypes.SuspendLayout();
      this.pnlTasks2.SuspendLayout();
      this.pnlTasks1.SuspendLayout();
      this.SuspendLayout();
      this.pnlTasks.BackColor = Color.White;
      this.pnlTasks.BorderStyle = BorderStyle.FixedSingle;
      this.pnlTasks.Controls.Add((Control) this.pnlMemoTypes);
      this.pnlTasks.Controls.Add((Control) this.pnlTasks2);
      this.pnlTasks.Controls.Add((Control) this.pnlTasks1);
      this.pnlTasks.Controls.Add((Control) this.lblTasksTitle);
      this.pnlTasks.Dock = DockStyle.Fill;
      this.pnlTasks.ForeColor = Color.Black;
      this.pnlTasks.Location = new Point(0, 0);
      this.pnlTasks.Name = "pnlTasks";
      this.pnlTasks.Size = new Size(151, 392);
      this.pnlTasks.TabIndex = 8;
      this.pnlMemoTypes.BackColor = Color.White;
      this.pnlMemoTypes.Controls.Add((Control) this.tvMemoTypes);
      this.pnlMemoTypes.Dock = DockStyle.Fill;
      this.pnlMemoTypes.Location = new Point(0, 149);
      this.pnlMemoTypes.Name = "pnlMemoTypes";
      this.pnlMemoTypes.Size = new Size(149, 107);
      this.pnlMemoTypes.TabIndex = 13;
      this.tvMemoTypes.BorderStyle = BorderStyle.None;
      this.tvMemoTypes.Dock = DockStyle.Fill;
      this.tvMemoTypes.Location = new Point(0, 0);
      this.tvMemoTypes.Name = "tvMemoTypes";
      this.tvMemoTypes.Size = new Size(149, 107);
      this.tvMemoTypes.TabIndex = 0;
      this.tvMemoTypes.DrawNode += new DrawTreeNodeEventHandler(this.tvMemoTypes_DrawNode);
      this.tvMemoTypes.AfterSelect += new TreeViewEventHandler(this.tvMemoTypes_AfterSelect);
      this.tvMemoTypes.Click += new EventHandler(this.tvMemoTypes_Click);
      this.pnlTasks2.BackColor = Color.White;
      this.pnlTasks2.Controls.Add((Control) this.lblBothMemo);
      this.pnlTasks2.Controls.Add((Control) this.lblPartMemo);
      this.pnlTasks2.Controls.Add((Control) this.lblPictureMemo);
      this.pnlTasks2.Controls.Add((Control) this.lblView);
      this.pnlTasks2.Controls.Add((Control) this.lblSpace1);
      this.pnlTasks2.Dock = DockStyle.Bottom;
      this.pnlTasks2.Location = new Point(0, 256);
      this.pnlTasks2.Name = "pnlTasks2";
      this.pnlTasks2.Padding = new Padding(15, 10, 15, 0);
      this.pnlTasks2.Size = new Size(149, 134);
      this.pnlTasks2.TabIndex = 11;
      this.lblBothMemo.Cursor = Cursors.Hand;
      this.lblBothMemo.Dock = DockStyle.Top;
      this.lblBothMemo.Location = new Point(15, 80);
      this.lblBothMemo.Name = "lblBothMemo";
      this.lblBothMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblBothMemo.Size = new Size(119, 16);
      this.lblBothMemo.TabIndex = 24;
      this.lblBothMemo.Text = "Both";
      this.lblBothMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblBothMemo.Click += new EventHandler(this.lblBothMemo_Click);
      this.lblPartMemo.Cursor = Cursors.Hand;
      this.lblPartMemo.Dock = DockStyle.Top;
      this.lblPartMemo.Location = new Point(15, 64);
      this.lblPartMemo.Name = "lblPartMemo";
      this.lblPartMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblPartMemo.Size = new Size(119, 16);
      this.lblPartMemo.TabIndex = 22;
      this.lblPartMemo.Text = "Part Memo";
      this.lblPartMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPartMemo.Click += new EventHandler(this.lblPartMemo_Click);
      this.lblPictureMemo.Cursor = Cursors.Hand;
      this.lblPictureMemo.Dock = DockStyle.Top;
      this.lblPictureMemo.Location = new Point(15, 48);
      this.lblPictureMemo.Name = "lblPictureMemo";
      this.lblPictureMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblPictureMemo.Size = new Size(119, 16);
      this.lblPictureMemo.TabIndex = 23;
      this.lblPictureMemo.Text = "Picture Memo";
      this.lblPictureMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPictureMemo.Click += new EventHandler(this.lblPictureMemo_Click);
      this.lblView.BackColor = Color.Transparent;
      this.lblView.Dock = DockStyle.Top;
      this.lblView.ForeColor = Color.Blue;
      this.lblView.Image = (Image) Resources.GroupLine1;
      this.lblView.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblView.Location = new Point(15, 20);
      this.lblView.Name = "lblView";
      this.lblView.Size = new Size(119, 28);
      this.lblView.TabIndex = 20;
      this.lblView.Text = "View Options";
      this.lblView.TextAlign = ContentAlignment.MiddleLeft;
      this.lblSpace1.Cursor = Cursors.Hand;
      this.lblSpace1.Dock = DockStyle.Top;
      this.lblSpace1.Location = new Point(15, 10);
      this.lblSpace1.Name = "lblSpace1";
      this.lblSpace1.Padding = new Padding(20, 0, 0, 0);
      this.lblSpace1.Size = new Size(119, 10);
      this.lblSpace1.TabIndex = 19;
      this.lblSpace1.TextAlign = ContentAlignment.MiddleLeft;
      this.pnlTasks1.BackColor = Color.White;
      this.pnlTasks1.Controls.Add((Control) this.label5);
      this.pnlTasks1.Controls.Add((Control) this.lblAllMemo);
      this.pnlTasks1.Controls.Add((Control) this.lblAdminMemo);
      this.pnlTasks1.Controls.Add((Control) this.lblGlobalMemo);
      this.pnlTasks1.Controls.Add((Control) this.lblLocalMemo);
      this.pnlTasks1.Controls.Add((Control) this.lblTypes);
      this.pnlTasks1.Dock = DockStyle.Top;
      this.pnlTasks1.Location = new Point(0, 27);
      this.pnlTasks1.Name = "pnlTasks1";
      this.pnlTasks1.Padding = new Padding(15, 10, 15, 0);
      this.pnlTasks1.Size = new Size(149, 122);
      this.pnlTasks1.TabIndex = 12;
      this.label5.Cursor = Cursors.Hand;
      this.label5.Dock = DockStyle.Top;
      this.label5.Location = new Point(15, 102);
      this.label5.Name = "label5";
      this.label5.Padding = new Padding(20, 0, 0, 0);
      this.label5.Size = new Size(119, 10);
      this.label5.TabIndex = 19;
      this.label5.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAllMemo.Cursor = Cursors.Hand;
      this.lblAllMemo.Dock = DockStyle.Top;
      this.lblAllMemo.Location = new Point(15, 86);
      this.lblAllMemo.Name = "lblAllMemo";
      this.lblAllMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblAllMemo.Size = new Size(119, 16);
      this.lblAllMemo.TabIndex = 24;
      this.lblAllMemo.Text = "All Memos";
      this.lblAllMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAllMemo.Click += new EventHandler(this.lblAllMemo_Click);
      this.lblAdminMemo.Cursor = Cursors.Hand;
      this.lblAdminMemo.Dock = DockStyle.Top;
      this.lblAdminMemo.Location = new Point(15, 70);
      this.lblAdminMemo.Name = "lblAdminMemo";
      this.lblAdminMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblAdminMemo.Size = new Size(119, 16);
      this.lblAdminMemo.TabIndex = 25;
      this.lblAdminMemo.Text = "Admin Memos";
      this.lblAdminMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblAdminMemo.Click += new EventHandler(this.lblAdminMemo_Click);
      this.lblGlobalMemo.Cursor = Cursors.Hand;
      this.lblGlobalMemo.Dock = DockStyle.Top;
      this.lblGlobalMemo.Location = new Point(15, 54);
      this.lblGlobalMemo.Name = "lblGlobalMemo";
      this.lblGlobalMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblGlobalMemo.Size = new Size(119, 16);
      this.lblGlobalMemo.TabIndex = 22;
      this.lblGlobalMemo.Text = "Global Memos";
      this.lblGlobalMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblGlobalMemo.Click += new EventHandler(this.lblGlobalMemo_Click);
      this.lblLocalMemo.Cursor = Cursors.Hand;
      this.lblLocalMemo.Dock = DockStyle.Top;
      this.lblLocalMemo.Location = new Point(15, 38);
      this.lblLocalMemo.Name = "lblLocalMemo";
      this.lblLocalMemo.Padding = new Padding(20, 0, 0, 0);
      this.lblLocalMemo.Size = new Size(119, 16);
      this.lblLocalMemo.TabIndex = 23;
      this.lblLocalMemo.Text = "Local Memos";
      this.lblLocalMemo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblLocalMemo.Click += new EventHandler(this.lblLocalMemo_Click);
      this.lblTypes.BackColor = Color.Transparent;
      this.lblTypes.Dock = DockStyle.Top;
      this.lblTypes.ForeColor = Color.Blue;
      this.lblTypes.Image = (Image) Resources.GroupLine1;
      this.lblTypes.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblTypes.Location = new Point(15, 10);
      this.lblTypes.Name = "lblTypes";
      this.lblTypes.Size = new Size(119, 28);
      this.lblTypes.TabIndex = 20;
      this.lblTypes.Text = "Memo Types";
      this.lblTypes.TextAlign = ContentAlignment.MiddleLeft;
      this.lblTasksTitle.BackColor = Color.White;
      this.lblTasksTitle.Dock = DockStyle.Top;
      this.lblTasksTitle.ForeColor = Color.Black;
      this.lblTasksTitle.Location = new Point(0, 0);
      this.lblTasksTitle.Name = "lblTasksTitle";
      this.lblTasksTitle.Padding = new Padding(3, 7, 0, 0);
      this.lblTasksTitle.Size = new Size(149, 27);
      this.lblTasksTitle.TabIndex = 6;
      this.lblTasksTitle.Text = "Tasks";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(151, 392);
      this.Controls.Add((Control) this.pnlTasks);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (frmMemoListTasks);
      this.Load += new EventHandler(this.frmMemoTasks_Load);
      this.pnlTasks.ResumeLayout(false);
      this.pnlMemoTypes.ResumeLayout(false);
      this.pnlTasks2.ResumeLayout(false);
      this.pnlTasks1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public frmMemoListTasks(frmMemoList frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.MdiParent = (Form) frm;
      this.UpdateFont();
      this.LoadResources();
    }

    private void frmMemoTasks_Load(object sender, EventArgs e)
    {
      this.lblLocalMemo.Enabled = Settings.Default.EnableLocalMemo;
      this.lblGlobalMemo.Enabled = Settings.Default.EnableGlobalMemo;
      this.lblAdminMemo.Enabled = Settings.Default.EnableAdminMemo;
      if (Settings.Default.EnableGlobalMemo || Settings.Default.EnableLocalMemo || Settings.Default.EnableAdminMemo)
        this.lblBothMemo.Enabled = true;
      else
        this.lblBothMemo.Enabled = false;
      this.intMemoType = this.GetMemoType();
      this.strMemoPriority = this.GetMemoPriority();
      this.strDateFormat = this.GetDateFormat();
      this.GetMemoStates();
      if (this.intMemoType == 2)
      {
        this.pnlTasks1.Visible = false;
        this.pnlMemoTypes.Visible = true;
        this.pnlTasks2.Dock = DockStyle.Bottom;
        if (!Settings.Default.EnableLocalMemo && !Settings.Default.EnableGlobalMemo && !Settings.Default.EnableAdminMemo)
        {
          this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
          this.frmParent.objFrmMemoLocal.Show();
          this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
        }
        else if (Settings.Default.EnableLocalMemo)
        {
          this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
          this.frmParent.objFrmMemoLocal.Show();
          this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
        }
        else if (Settings.Default.EnableGlobalMemo)
        {
          this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
          this.frmParent.objFrmMemoGlobal.Show();
          this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
        }
        else if (Settings.Default.EnableAdminMemo)
        {
          this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
          this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
          this.frmParent.objFrmMemoAdmin.Show();
          this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
        }
        this.bMemoLoadedFirstTime = true;
        this.LoadMemo(true, true);
      }
      else
      {
        this.pnlMemoTypes.Visible = false;
        this.pnlTasks1.Visible = true;
        this.pnlTasks2.Dock = DockStyle.Top;
      }
    }

    private void tvMemoTypes_DrawNode(object sender, DrawTreeNodeEventArgs e)
    {
      string text = e.Node.Tag == null ? e.Node.Text : ((frmMemoListTasks.TreeNodeText) e.Node.Tag).DisplayText;
      Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
      Color foreColor = e.Node.ForeColor;
      if (foreColor == Color.Empty)
        foreColor = e.Node.TreeView.ForeColor;
      Rectangle rect = new Rectangle(e.Bounds.X, e.Bounds.Y, this.tvMemoTypes.SelectedNode.Bounds.Width + 15, e.Bounds.Height);
      if (e.Node == e.Node.TreeView.SelectedNode)
      {
        Color highlightText = SystemColors.HighlightText;
        e.Graphics.FillRectangle((Brush) new SolidBrush(Settings.Default.appHighlightBackColor), rect);
        TextRenderer.DrawText((IDeviceContext) e.Graphics, text, font, e.Bounds, Settings.Default.appHighlightForeColor, TextFormatFlags.VerticalCenter);
      }
      else
        TextRenderer.DrawText((IDeviceContext) e.Graphics, text, font, e.Bounds, foreColor, TextFormatFlags.VerticalCenter);
    }

    private void tvMemoTypes_AfterSelect(object sender, TreeViewEventArgs e)
    {
      try
      {
        this.frmParent.objFrmMemoLocal.ClearItems(true, false, true);
        this.frmParent.objFrmMemoGlobal.ClearItems(true, false, true);
        this.frmParent.objFrmMemoAdmin.ClearItems(true, false, true);
        if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "LOCALMEMO")
        {
          if (!Settings.Default.EnableLocalMemo)
          {
            this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
          }
          else
          {
            this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
            this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
            this.frmParent.objFrmMemoLocal.Show();
            this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "GLOBALMEMO")
        {
          if (!Settings.Default.EnableGlobalMemo)
          {
            this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
          }
          else
          {
            this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
            this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
            this.frmParent.objFrmMemoGlobal.Show();
            this.lblGlobalMemo_Click((object) null, (EventArgs) null);
            this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "ADMINMEMO")
        {
          if (!Settings.Default.EnableAdminMemo)
          {
            this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
          }
          else
          {
            this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
            this.frmParent.objFrmMemoAdmin.pnlBottom.Dock = DockStyle.Fill;
            this.frmParent.objFrmMemoAdmin.Show();
            this.lblAdminMemo_Click((object) null, (EventArgs) null);
            this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
          }
        }
        else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "LOCALMEMO")
        {
          this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
          string[] strArray1 = this.tvMemoTypes.SelectedNode.Name.Split('|');
          string str1 = strArray1[0];
          string str2 = strArray1[1];
          string str3 = strArray1[2];
          string str4 = strArray1[3];
          if (str1.ToUpper().Contains("PIPESIGN"))
            str1 = str1.Replace("PIPESIGN", "|");
          if (str4.ToUpper().Contains("PIPESIGN"))
            str4 = str4.Replace("PIPESIGN", "|");
          if (strArray1.Length > 4)
            str4 = str4 + "|" + strArray1[4];
          if (str3.ToUpper().Trim() == "TEXT")
          {
            this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoLocal.Show();
            this.frmParent.objFrmMemoLocal.rtbTxtMemo.Text = str4;
            this.frmParent.objFrmMemoLocal.lblTxtMemoDate.Text = str2;
            this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
          }
          else if (str3.ToUpper().Trim() == "REFERENCE")
          {
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
          else if (str3.ToUpper().Trim() == "HYPERLINK")
          {
            this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
            this.frmParent.objFrmMemoLocal.Show();
            this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
            this.frmParent.objFrmMemoLocal.txtDescription.Text = str1;
            this.frmParent.objFrmMemoLocal.txtHypMemoUrl.Text = str4;
            this.frmParent.objFrmMemoLocal.lblHypMemoDate.Text = str2;
          }
          else if (str3.ToUpper().Trim() == "PROGRAM")
          {
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
          this.frmParent.objFrmMemoGlobal.Show();
          this.lblGlobalMemo_Click((object) null, (EventArgs) null);
          this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
          this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
          string[] strArray1 = this.tvMemoTypes.SelectedNode.Name.Split('|');
          string str1 = strArray1[0];
          string str2 = strArray1[1];
          string str3 = strArray1[2];
          string str4 = strArray1[3];
          if (str1.ToUpper().Contains("PIPESIGN"))
            str1 = str1.Replace("PIPESIGN", "|");
          if (str4.ToUpper().Contains("PIPESIGN"))
            str4 = str4.Replace("PIPESIGN", "|");
          if (strArray1.Length > 4)
            str4 = str4 + "|" + strArray1[4];
          if (str3.ToUpper().Trim() == "TEXT")
          {
            this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoGlobal.Show();
            this.frmParent.objFrmMemoGlobal.rtbTxtMemo.Text = str4;
            this.frmParent.objFrmMemoGlobal.lblTxtMemoDate.Text = str2;
            this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
          }
          else if (str3.ToUpper().Trim() == "REFERENCE")
          {
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
          else if (str3.ToUpper().Trim() == "HYPERLINK")
          {
            this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
            this.frmParent.objFrmMemoGlobal.Show();
            this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
            this.frmParent.objFrmMemoGlobal.txtDescription.Text = str1;
            this.frmParent.objFrmMemoGlobal.txtHypMemoUrl.Text = str4;
            this.frmParent.objFrmMemoGlobal.lblHypMemoDate.Text = str2;
          }
          else if (str3.ToUpper().Trim() == "PROGRAM")
          {
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
          this.lblAdminMemo_Click((object) null, (EventArgs) null);
          this.frmParent.objFrmMemoAdmin.Show();
          this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
          this.frmParent.objFrmMemoAdmin.pnlBottom.Dock = DockStyle.Fill;
          string[] strArray1 = this.tvMemoTypes.SelectedNode.Name.Split('|');
          string str1 = strArray1[0];
          string str2 = strArray1[1];
          string str3 = strArray1[2];
          string str4 = strArray1[3];
          if (strArray1.Length > 4)
            str4 = str4 + "|" + strArray1[4];
          if (str3.ToUpper().Trim() == "TEXT")
          {
            this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
            this.frmParent.objFrmMemoAdmin.Show();
            this.frmParent.objFrmMemoAdmin.rtbTxtMemo.Text = str4;
            this.frmParent.objFrmMemoAdmin.lblTxtMemoDate.Text = str2;
            this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
          }
          else if (str3.ToUpper().Trim() == "REFERENCE")
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
          else if (str3.ToUpper().Trim() == "HYPERLINK")
          {
            this.frmParent.objFrmMemoAdmin.SetTabProperty("HYPERLINK");
            this.frmParent.objFrmMemoAdmin.Show();
            this.frmParent.objFrmMemoAdmin.pnlHypMemo.BringToFront();
            this.frmParent.objFrmMemoAdmin.txtDescription.Text = str1;
            this.frmParent.objFrmMemoAdmin.txtHypMemoUrl.Text = str4;
            this.frmParent.objFrmMemoAdmin.lblHypMemoDate.Text = str2;
          }
          else if (str3.ToUpper().Trim() == "PROGRAM")
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

    private void lblLocalMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblLocalMemo);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoLocal.Show();
    }

    private void lblGlobalMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblGlobalMemo);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoGlobal.Show();
    }

    private void lblAdminMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAdminMemo);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoAdmin.Show();
    }

    private void lblAllMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblAllMemo);
      this.frmParent.HideForms();
      this.frmParent.objFrmMemoAll.Show();
    }

    private void lblPictureMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPictureMemo);
      this.bMemoLoadedFirstTime = false;
      if (this.intMemoType == 2)
      {
        this.LoadMemo(true, false);
      }
      else
      {
        if (Settings.Default.EnableLocalMemo)
          this.frmParent.objFrmMemoLocal.LoadMemos(true, false);
        if (Settings.Default.EnableGlobalMemo)
          this.frmParent.objFrmMemoGlobal.LoadMemos(true, false);
        if (Settings.Default.EnableAdminMemo)
          this.frmParent.objFrmMemoAdmin.LoadMemos(true, false);
        if (!Settings.Default.EnableAdminMemo && !Settings.Default.EnableGlobalMemo && !Settings.Default.EnableLocalMemo)
          return;
        this.frmParent.objFrmMemoAll.LoadMemos(true, false);
      }
    }

    private void lblPartMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblPartMemo);
      this.bMemoLoadedFirstTime = false;
      if (this.intMemoType == 2)
      {
        this.LoadMemo(false, true);
      }
      else
      {
        if (Settings.Default.EnableLocalMemo)
          this.frmParent.objFrmMemoLocal.LoadMemos(false, true);
        if (Settings.Default.EnableGlobalMemo)
          this.frmParent.objFrmMemoGlobal.LoadMemos(false, true);
        if (Settings.Default.EnableAdminMemo)
          this.frmParent.objFrmMemoAdmin.LoadMemos(false, true);
        if (!Settings.Default.EnableAdminMemo && !Settings.Default.EnableGlobalMemo && !Settings.Default.EnableLocalMemo)
          return;
        this.frmParent.objFrmMemoAll.LoadMemos(false, true);
      }
    }

    private void lblBothMemo_Click(object sender, EventArgs e)
    {
      this.HighlightList(ref this.lblBothMemo);
      if (this.bMemoLoadedFirstTime)
        return;
      if (this.intMemoType == 2)
      {
        this.LoadMemo(true, true);
      }
      else
      {
        if (Settings.Default.EnableLocalMemo)
          this.frmParent.objFrmMemoLocal.LoadMemos(true, true);
        if (Settings.Default.EnableGlobalMemo)
          this.frmParent.objFrmMemoGlobal.LoadMemos(true, true);
        if (Settings.Default.EnableAdminMemo)
          this.frmParent.objFrmMemoAdmin.LoadMemos(true, true);
        if (!Settings.Default.EnableAdminMemo && !Settings.Default.EnableGlobalMemo && !Settings.Default.EnableLocalMemo)
          return;
        this.frmParent.objFrmMemoAll.LoadMemos(true, true);
      }
    }

    private void tvMemoTypes_Click(object sender, EventArgs e)
    {
      this.tnPreviouisNode = this.tvMemoTypes.SelectedNode;
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

    private void LoadMemo(bool bPictureLevel, bool bPartLevel)
    {
      if (bPictureLevel && !bPartLevel)
      {
        this.tvMemoTypes.Nodes.Clear();
        this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
        this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
        this.pnlTasks1.Hide();
        this.pnlMemoTypes.Dock = DockStyle.Fill;
        this.pnlTasks2.Show();
        this.pnlTasks2.Dock = DockStyle.Bottom;
        TreeNode node1 = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL));
        node1.Name = "LocalMemo";
        TreeNode node2 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL));
        node2.Name = "GlobalMemo";
        TreeNode node3 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL));
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
                  if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value == "")
                  {
                    List<string> memoDetails = this.GetMemoDetails(xNode);
                    List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                    string upper = memoDetails[2].ToUpper();
                    string empty = string.Empty;
                    string text;
                    if (upper == "HYPERLINK")
                    {
                      if (memoDetails[0].Length > 25)
                        text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                      else
                        text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    }
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
                    foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
                    {
                      if (memoDetails[0].Contains("|"))
                        memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                      if (memoDetails[3].Contains("|"))
                        memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                      this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(new TreeNode()
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
                  if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value == "")
                  {
                    List<string> memoDetails = this.GetMemoDetails(xNode);
                    List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                    string upper = memoDetails[2].ToUpper();
                    string empty = string.Empty;
                    string text;
                    if (upper == "HYPERLINK")
                    {
                      if (memoDetails[0].Length > 25)
                        text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                      else
                        text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    }
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
                    foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
                    {
                      if (memoDetails[0].Contains("|"))
                        memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                      if (memoDetails[3].Contains("|"))
                        memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                      this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(new TreeNode()
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
          }
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (Settings.Default.EnableAdminMemo)
          {
            if (this.frmParent.xnlAdminMemo != null)
            {
              if (this.frmParent.xnlAdminMemo.Count > 0)
              {
                foreach (XmlNode xNode in this.frmParent.xnlAdminMemo)
                {
                  if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value == "")
                  {
                    List<string> memoDetails = this.GetMemoDetails(xNode);
                    List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                    string text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + memoDetails[2];
                    treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
                    foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
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
        }
        catch (Exception ex)
        {
        }
      }
      else if (!bPictureLevel && bPartLevel)
      {
        this.tvMemoTypes.Nodes.Clear();
        this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
        this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
        TreeNode node1 = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL));
        node1.Name = "LocalMemo";
        TreeNode node2 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL));
        node2.Name = "GlobalMemo";
        TreeNode node3 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL));
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
                  if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value != "")
                  {
                    List<string> memoDetails = this.GetMemoDetails(xNode);
                    List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                    string upper = memoDetails[2].ToUpper();
                    string empty = string.Empty;
                    string text;
                    if (upper == "HYPERLINK")
                    {
                      if (memoDetails[0].Length > 25)
                        text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                      else
                        text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    }
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
                    foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
                    {
                      if (memoDetails[0].Contains("|"))
                        memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                      if (memoDetails[3].Contains("|"))
                        memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                      this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(new TreeNode()
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
                  if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value != "")
                  {
                    List<string> memoDetails = this.GetMemoDetails(xNode);
                    List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                    string upper = memoDetails[2].ToUpper();
                    string empty = string.Empty;
                    string text;
                    if (upper == "HYPERLINK")
                    {
                      if (memoDetails[0].Length > 25)
                        text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                      else
                        text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    }
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
                    foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
                    {
                      if (memoDetails[0].Contains("|"))
                        memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                      if (memoDetails[3].Contains("|"))
                        memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                      this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(new TreeNode()
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
          }
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (Settings.Default.EnableAdminMemo)
          {
            if (this.frmParent.xnlAdminMemo != null)
            {
              if (this.frmParent.xnlAdminMemo.Count > 0)
              {
                foreach (XmlNode xNode in this.frmParent.xnlAdminMemo)
                {
                  if (xNode.Attributes["PartNo"] != null && xNode.Attributes["PartNo"].Value != "")
                  {
                    List<string> memoDetails = this.GetMemoDetails(xNode);
                    List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                    string text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + memoDetails[2];
                    treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
                    foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
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
        }
        catch (Exception ex)
        {
        }
      }
      else if (bPictureLevel && bPartLevel)
      {
        this.tvMemoTypes.Nodes.Clear();
        this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
        this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
        TreeNode node1 = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL));
        node1.Name = "LocalMemo";
        TreeNode node2 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL));
        node2.Name = "GlobalMemo";
        TreeNode node3 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL));
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
                  List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                  string upper = memoDetails[2].ToUpper();
                  string empty = string.Empty;
                  string text;
                  if (upper == "HYPERLINK")
                  {
                    if (memoDetails[0].Length > 25)
                      text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  }
                  else
                    text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
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
                  foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
                  {
                    if (memoDetails[0].Contains("|"))
                      memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                    if (memoDetails[3].Contains("|"))
                      memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                    this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(new TreeNode()
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
                  List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                  string upper = memoDetails[2].ToUpper();
                  string empty = string.Empty;
                  string text;
                  if (upper == "HYPERLINK")
                  {
                    if (memoDetails[0].Length > 25)
                      text = memoDetails[0].Substring(0, 25) + "...\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                    else
                      text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  }
                  else
                    text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
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
                  foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
                  {
                    if (memoDetails[0].Contains("|"))
                      memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
                    if (memoDetails[3].Contains("|"))
                      memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
                    this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(new TreeNode()
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
        }
        catch (Exception ex)
        {
        }
        try
        {
          if (Settings.Default.EnableAdminMemo)
          {
            if (this.frmParent.xnlAdminMemo != null)
            {
              if (this.frmParent.xnlAdminMemo.Count > 0)
              {
                foreach (XmlNode xNode in this.frmParent.xnlAdminMemo)
                {
                  List<string> memoDetails = this.GetMemoDetails(xNode);
                  List<frmMemoListTasks.TreeNodeText> treeNodeTextList = new List<frmMemoListTasks.TreeNodeText>();
                  string text = memoDetails[0] + "\r\n" + memoDetails[1] + "\r\n" + this.GetMultilingualValue(memoDetails[2]);
                  treeNodeTextList.Add(new frmMemoListTasks.TreeNodeText(text));
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
                  foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTextList)
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
      else
        this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes[0];
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
        {
          str1 = xNode.Attributes["Value"].Value.Trim();
          str2 = " ";
        }
      }
      string s;
      if (xNode.Attributes["Update"] != null)
      {
        if (xNode.Attributes["Update"].Value.Trim() != string.Empty)
        {
          s = xNode.Attributes["Update"].Value.Trim();
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
                foreach (string str4 in strArray)
                {
                  if (this.strDateFormat == str4)
                    s = exact.ToString(this.strDateFormat);
                }
              }
            }
          }
          catch
          {
          }
        }
        else
          s = "Unknown";
      }
      else
        s = "Unknown";
      stringList.Add(str2);
      stringList.Add(s);
      stringList.Add(str3);
      stringList.Add(str1);
      return stringList;
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

    public void ShowTask(ViewAllMemoTasks task)
    {
      this.intMemoType = this.GetMemoType();
      if (this.intMemoType != 2)
      {
        switch (task)
        {
          case ViewAllMemoTasks.Local:
            this.lblLocalMemo_Click((object) null, (EventArgs) null);
            break;
          case ViewAllMemoTasks.Global:
            this.lblGlobalMemo_Click((object) null, (EventArgs) null);
            break;
          case ViewAllMemoTasks.All:
            this.lblAllMemo_Click((object) null, (EventArgs) null);
            break;
        }
      }
      this.lblBothMemo_Click((object) null, (EventArgs) null);
    }

    private void HighlightList(ref Label lbl)
    {
      if (lbl.Parent.Name.Equals(this.pnlTasks1.Name))
      {
        for (int index = 0; index < this.pnlTasks1.Controls.Count; ++index)
        {
          if (this.pnlTasks1.Controls[index] == this.lblGlobalMemo | this.pnlTasks1.Controls[index] == this.lblLocalMemo | this.pnlTasks1.Controls[index] == this.lblAdminMemo | this.pnlTasks1.Controls[index] == this.lblAllMemo)
          {
            this.pnlTasks1.Controls[index].BackColor = this.pnlTasks1.BackColor;
            this.pnlTasks1.Controls[index].ForeColor = this.pnlTasks1.ForeColor;
          }
        }
        lbl.BackColor = Settings.Default.appHighlightBackColor;
        lbl.ForeColor = Settings.Default.appHighlightForeColor;
      }
      if (!lbl.Parent.Name.Equals(this.pnlTasks2.Name))
        return;
      for (int index = 0; index < this.pnlTasks2.Controls.Count; ++index)
      {
        if (this.pnlTasks2.Controls[index] == this.lblPartMemo | this.pnlTasks2.Controls[index] == this.lblPictureMemo | this.pnlTasks2.Controls[index] == this.lblBothMemo)
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

    private void LoadResources()
    {
      this.lblTasksTitle.Text = this.GetResource("Tasks", "TASKS", ResourceType.LABEL);
      this.lblView.Text = this.GetResource("View Options", "VIEW_OPTIONS", ResourceType.LABEL);
      this.lblLocalMemo.Text = this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL);
      this.lblGlobalMemo.Text = this.GetResource("Global Memo", "GLOBAL_MEMO", ResourceType.LABEL);
      this.lblPartMemo.Text = this.GetResource("Part Memo", "PART_MEMO", ResourceType.LABEL);
      this.lblPictureMemo.Text = this.GetResource("Picture Memo", "PICTURE_MEMO", ResourceType.LABEL);
      this.lblAllMemo.Text = this.GetResource("All", "ALL", ResourceType.LABEL);
      this.lblBothMemo.Text = this.GetResource("Both", "BOTH", ResourceType.LABEL);
      this.lblAdminMemo.Text = this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL);
      this.lblTypes.Text = this.GetResource("Memo Types", "MEMO_TYPE", ResourceType.LABEL);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MEMO_LIST']" + "/Screen[@Name='MEMOLIST_TASKS']";
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

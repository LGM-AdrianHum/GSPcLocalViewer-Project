// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmInfo
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
  public class frmInfo : DockContent
  {
    private IContainer components;
    private Panel pnlForm;
    private BackgroundWorker bgWorker;
    private PictureBox picLoading;
    private Panel pnlPageInfo;
    private Panel pnlrtbPageInfo;
    private RichTextBox rtbPageInfo;
    private Label lblPageInfo;
    private Panel pnlBookInfo;
    private Panel pnlrtbBookInfo;
    private RichTextBox rtbBookInfo;
    private Label lblBookInfo;
    private Timer timer1;
    private frmViewer frmParent;
    private XmlNode schemaNodeParent;
    private XmlNode pageNodeParent;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (frmInfo));
      this.pnlForm = new Panel();
      this.pnlPageInfo = new Panel();
      this.pnlrtbPageInfo = new Panel();
      this.rtbPageInfo = new RichTextBox();
      this.lblPageInfo = new Label();
      this.pnlBookInfo = new Panel();
      this.pnlrtbBookInfo = new Panel();
      this.rtbBookInfo = new RichTextBox();
      this.lblBookInfo = new Label();
      this.picLoading = new PictureBox();
      this.bgWorker = new BackgroundWorker();
      this.timer1 = new Timer(this.components);
      this.pnlForm.SuspendLayout();
      this.pnlPageInfo.SuspendLayout();
      this.pnlrtbPageInfo.SuspendLayout();
      this.pnlBookInfo.SuspendLayout();
      this.pnlrtbBookInfo.SuspendLayout();
      ((ISupportInitialize) this.picLoading).BeginInit();
      this.SuspendLayout();
      this.pnlForm.AutoScroll = true;
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
      this.pnlForm.Controls.Add((Control) this.pnlPageInfo);
      this.pnlForm.Controls.Add((Control) this.pnlBookInfo);
      this.pnlForm.Controls.Add((Control) this.picLoading);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(297, 555);
      this.pnlForm.TabIndex = 19;
      this.pnlPageInfo.BackColor = Color.White;
      this.pnlPageInfo.Controls.Add((Control) this.pnlrtbPageInfo);
      this.pnlPageInfo.Controls.Add((Control) this.lblPageInfo);
      this.pnlPageInfo.Dock = DockStyle.Fill;
      this.pnlPageInfo.Location = new Point(0, 209);
      this.pnlPageInfo.Name = "pnlPageInfo";
      this.pnlPageInfo.Padding = new Padding(15, 10, 0, 0);
      this.pnlPageInfo.Size = new Size(295, 344);
      this.pnlPageInfo.TabIndex = 31;
      this.pnlPageInfo.Tag = (object) "";
      this.pnlPageInfo.Visible = false;
      this.pnlrtbPageInfo.BackColor = Color.White;
      this.pnlrtbPageInfo.Controls.Add((Control) this.rtbPageInfo);
      this.pnlrtbPageInfo.Dock = DockStyle.Fill;
      this.pnlrtbPageInfo.Location = new Point(15, 38);
      this.pnlrtbPageInfo.Name = "pnlrtbPageInfo";
      this.pnlrtbPageInfo.Padding = new Padding(10, 0, 0, 0);
      this.pnlrtbPageInfo.Size = new Size(280, 306);
      this.pnlrtbPageInfo.TabIndex = 15;
      this.rtbPageInfo.BackColor = Color.White;
      this.rtbPageInfo.BorderStyle = BorderStyle.None;
      this.rtbPageInfo.Dock = DockStyle.Fill;
      this.rtbPageInfo.Location = new Point(10, 0);
      this.rtbPageInfo.Name = "rtbPageInfo";
      this.rtbPageInfo.ReadOnly = true;
      this.rtbPageInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbPageInfo.Size = new Size(270, 306);
      this.rtbPageInfo.TabIndex = 12;
      this.rtbPageInfo.Text = "";
      this.rtbPageInfo.MouseDown += new MouseEventHandler(this.rtbPageInfo_MouseDown_1);
      this.lblPageInfo.BackColor = Color.Transparent;
      this.lblPageInfo.Cursor = Cursors.Hand;
      this.lblPageInfo.Dock = DockStyle.Top;
      this.lblPageInfo.ForeColor = Color.Blue;
      this.lblPageInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.GroupLine2;
      this.lblPageInfo.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblPageInfo.Location = new Point(15, 10);
      this.lblPageInfo.Name = "lblPageInfo";
      this.lblPageInfo.Size = new Size(280, 28);
      this.lblPageInfo.TabIndex = 11;
      this.lblPageInfo.Tag = (object) "";
      this.lblPageInfo.Text = "Page Information";
      this.lblPageInfo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblPageInfo.Click += new EventHandler(this.lblPageInfo_Click);
      this.pnlBookInfo.BackColor = Color.White;
      this.pnlBookInfo.Controls.Add((Control) this.pnlrtbBookInfo);
      this.pnlBookInfo.Controls.Add((Control) this.lblBookInfo);
      this.pnlBookInfo.Dock = DockStyle.Top;
      this.pnlBookInfo.Location = new Point(0, 0);
      this.pnlBookInfo.Name = "pnlBookInfo";
      this.pnlBookInfo.Padding = new Padding(15, 10, 0, 0);
      this.pnlBookInfo.Size = new Size(295, 209);
      this.pnlBookInfo.TabIndex = 32;
      this.pnlBookInfo.Tag = (object) "";
      this.pnlBookInfo.Visible = false;
      this.pnlrtbBookInfo.BackColor = Color.White;
      this.pnlrtbBookInfo.Controls.Add((Control) this.rtbBookInfo);
      this.pnlrtbBookInfo.Dock = DockStyle.Fill;
      this.pnlrtbBookInfo.Location = new Point(15, 38);
      this.pnlrtbBookInfo.Name = "pnlrtbBookInfo";
      this.pnlrtbBookInfo.Padding = new Padding(10, 0, 0, 0);
      this.pnlrtbBookInfo.Size = new Size(280, 171);
      this.pnlrtbBookInfo.TabIndex = 15;
      this.rtbBookInfo.BackColor = Color.White;
      this.rtbBookInfo.BorderStyle = BorderStyle.None;
      this.rtbBookInfo.CausesValidation = false;
      this.rtbBookInfo.Dock = DockStyle.Fill;
      this.rtbBookInfo.Location = new Point(10, 0);
      this.rtbBookInfo.Name = "rtbBookInfo";
      this.rtbBookInfo.ReadOnly = true;
      this.rtbBookInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
      this.rtbBookInfo.Size = new Size(270, 171);
      this.rtbBookInfo.TabIndex = 12;
      this.rtbBookInfo.Text = "";
      this.rtbBookInfo.MouseDown += new MouseEventHandler(this.rtbBookInfo_MouseDown);
      this.lblBookInfo.BackColor = Color.Transparent;
      this.lblBookInfo.Cursor = Cursors.Hand;
      this.lblBookInfo.Dock = DockStyle.Top;
      this.lblBookInfo.ForeColor = Color.Blue;
      this.lblBookInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.GroupLine2;
      this.lblBookInfo.ImageAlign = ContentAlignment.MiddleLeft;
      this.lblBookInfo.Location = new Point(15, 10);
      this.lblBookInfo.Name = "lblBookInfo";
      this.lblBookInfo.Size = new Size(280, 28);
      this.lblBookInfo.TabIndex = 11;
      this.lblBookInfo.Tag = (object) "";
      this.lblBookInfo.Text = "Book Information";
      this.lblBookInfo.TextAlign = ContentAlignment.MiddleLeft;
      this.lblBookInfo.Click += new EventHandler(this.lblBookInfo_Click);
      this.picLoading.BackColor = Color.White;
      this.picLoading.Dock = DockStyle.Fill;
      this.picLoading.Image = (Image) GSPcLocalViewer.Properties.Resources.Loading1;
      this.picLoading.Location = new Point(0, 0);
      this.picLoading.Name = "picLoading";
      this.picLoading.Size = new Size(295, 553);
      this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
      this.picLoading.TabIndex = 25;
      this.picLoading.TabStop = false;
      this.picLoading.Visible = false;
      this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
      this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(297, 555);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.HideOnClose = true;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (frmInfo);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Information";
      this.pnlForm.ResumeLayout(false);
      this.pnlPageInfo.ResumeLayout(false);
      this.pnlrtbPageInfo.ResumeLayout(false);
      this.pnlBookInfo.ResumeLayout(false);
      this.pnlrtbBookInfo.ResumeLayout(false);
      ((ISupportInitialize) this.picLoading).EndInit();
      this.ResumeLayout(false);
    }

    [DllImport("user32.dll")]
    private static extern bool LockWindowUpdate(IntPtr hWndLock);

    [DllImport("user32.dll")]
    public static extern long HideCaret(IntPtr hwnd);

    public frmInfo(frmViewer frm)
    {
      this.InitializeComponent();
      this.frmParent = frm;
      this.UpdateFont();
      this.LoadResources();
    }

    public void UpdateFont()
    {
      this.pnlForm.Font = Settings.Default.appFont;
    }

    public void LoadData(XmlNode schemaNode, XmlNode pageNode)
    {
      this.ShowLoading(this.pnlForm);
      this.bgWorker.RunWorkerAsync((object) new object[2]
      {
        (object) schemaNode,
        (object) pageNode
      });
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      this.frmParent.bObjFrmSelectionlistClosed = false;
      object[] objArray = (object[]) e.Argument;
      XmlNode schemaNode = (XmlNode) objArray[0];
      XmlNode pageNode = (XmlNode) objArray[1];
      this.LoadBookInfo();
      this.LoadPageInfo(schemaNode, pageNode);
      if (this.pnlBookInfo.Visible || this.pnlPageInfo.Visible)
        return;
      this.ShowPanels(true);
    }

    private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.frmParent.bObjFrmSelectionlistClosed = true;
      this.HideLoading(this.pnlForm);
    }

    private void ShowPanels(bool value)
    {
      if (this.pnlForm.InvokeRequired)
      {
        this.pnlForm.Invoke((Delegate) new frmInfo.ShowPanelsDelegate(this.ShowPanels), (object) value);
      }
      else
      {
        this.pnlBookInfo.BringToFront();
        this.pnlBookInfo.Visible = value;
        this.pnlPageInfo.BringToFront();
        this.pnlPageInfo.Visible = value;
      }
    }

    private string GetBookInfoLanguage(string sKey)
    {
      bool flag = false;
      string str1 = Settings.Default.appLanguage + "_GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini";
      if (File.Exists(Application.StartupPath + "\\Language XMLs\\" + str1))
      {
        TextReader textReader = (TextReader) new StreamReader(Application.StartupPath + "\\Language XMLs\\" + str1);
        string str2;
        while ((str2 = textReader.ReadLine()) != null)
        {
          if (str2.ToUpper() == "[VIEWER_INFORMATION]")
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

    private void LoadBookInfo()
    {
      if (this.rtbBookInfo.InvokeRequired)
      {
        this.rtbBookInfo.Invoke((Delegate) new frmInfo.LoadBookInfoDelegate(this.LoadBookInfo));
      }
      else
      {
        this.rtbBookInfo.SelectionFont = this.rtbBookInfo.Font;
        this.rtbBookInfo.Clear();
        try
        {
          XmlNode schemaNode = this.frmParent.SchemaNode;
          XmlNode bookNode = this.frmParent.BookNode;
          for (int index = 0; index < schemaNode.Attributes.Count; ++index)
          {
            if (!schemaNode.Attributes[index].Value.ToUpper().StartsWith("LEVEL") && schemaNode.Attributes[index].Value.ToUpper() != "SECURITYLOCKS" && (schemaNode.Attributes[index].Value.ToUpper() != "ID" && bookNode.Attributes[schemaNode.Attributes[index].Name] != null))
            {
              this.rtbBookInfo.SelectionColor = Color.Gray;
              this.rtbBookInfo.SelectedText = this.GetBookInfoLanguage(schemaNode.Attributes[index].Value) + ": ";
              this.rtbBookInfo.SelectionColor = Color.Black;
              string str = bookNode.Attributes[schemaNode.Attributes[index].Name].Value;
              if (str.ToUpper().EndsWith(".DJVU") || str.ToUpper().EndsWith(".JPEG") || str.ToUpper().EndsWith(".TIF"))
                str = str.Substring(0, str.IndexOf("."));
              this.rtbBookInfo.SelectedText = str + "\n";
            }
          }
        }
        catch
        {
          this.rtbBookInfo.Clear();
          this.rtbBookInfo.SelectionColor = Color.Red;
          this.rtbBookInfo.SelectedText = this.GetResource("(E-INF-EM001) Failed to load specified object", "(E-INF-EM001)_FAILED", ResourceType.LABEL);
        }
      }
    }

    private void LoadPageInfo(XmlNode schemaNode, XmlNode pageNode)
    {
      if (schemaNode == null || pageNode == null)
        return;
      if (this.rtbPageInfo.InvokeRequired)
      {
        this.rtbPageInfo.Invoke((Delegate) new frmInfo.LoadPageInfoDelegate(this.LoadPageInfo), (object) schemaNode, (object) pageNode);
      }
      else
      {
        this.schemaNodeParent = schemaNode;
        this.pageNodeParent = pageNode;
        this.rtbPageInfo.SelectionFont = this.rtbBookInfo.Font;
        this.rtbPageInfo.Clear();
        try
        {
          for (int index = 0; index < schemaNode.Attributes.Count; ++index)
          {
            if (pageNode.Attributes[schemaNode.Attributes[index].Name] != null && schemaNode.Attributes[index].Value.ToUpper() != "ID")
            {
              this.rtbPageInfo.SelectionColor = Color.Gray;
              this.rtbPageInfo.SelectedText = this.GetBookInfoLanguage(schemaNode.Attributes[index].Value) + ": ";
              this.rtbPageInfo.SelectionColor = Color.Black;
              this.rtbPageInfo.SelectedText = pageNode.Attributes[schemaNode.Attributes[index].Name].Value + "\n";
            }
          }
          if (!pageNode.HasChildNodes)
            return;
          int num = 0;
          foreach (XmlNode childNode in pageNode.ChildNodes)
          {
            if (childNode.Name.ToUpper() == "PIC")
            {
              ++num;
              this.rtbPageInfo.SelectionColor = Color.Gray;
              this.rtbPageInfo.SelectedText = "[" + this.GetBookInfoLanguage("Picture") + " " + (object) num + "]\n";
              for (int index = 0; index < schemaNode.Attributes.Count; ++index)
              {
                if (childNode.Attributes[schemaNode.Attributes[index].Name] != null && !schemaNode.Attributes[index].Value.ToUpper().StartsWith("FIELD"))
                {
                  this.rtbPageInfo.SelectionColor = Color.Gray;
                  this.rtbPageInfo.SelectedText = this.GetBookInfoLanguage(schemaNode.Attributes[index].Value) + ":";
                  this.rtbPageInfo.SelectionColor = Color.Black;
                  string str = childNode.Attributes[schemaNode.Attributes[index].Name].Value;
                  if (str.ToUpper().EndsWith(".DJVU") || str.ToUpper().EndsWith(".JPEG") || str.ToUpper().EndsWith(".TIF"))
                    str = str.Substring(0, str.IndexOf("."));
                  this.rtbPageInfo.SelectedText = str + "\n";
                }
              }
            }
          }
        }
        catch
        {
          this.rtbPageInfo.Clear();
          this.rtbPageInfo.SelectionColor = Color.Red;
          this.rtbPageInfo.SelectedText = this.GetResource("(E-INF-EM002) Failed to load specified object", "(E-INF-EM002)_FAILED", ResourceType.LABEL);
        }
      }
    }

    private void lblBookInfo_Click(object sender, EventArgs e)
    {
      if (this.pnlrtbBookInfo.Visible)
      {
        this.pnlrtbBookInfo.Visible = false;
        this.pnlBookInfo.Height -= this.pnlrtbBookInfo.Height;
        this.lblBookInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.GroupLine3;
      }
      else
      {
        this.pnlBookInfo.Height += this.pnlrtbBookInfo.Height;
        this.pnlrtbBookInfo.Visible = true;
        this.lblBookInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.GroupLine2;
      }
    }

    private void lblPageInfo_Click(object sender, EventArgs e)
    {
      if (this.pnlrtbPageInfo.Visible)
      {
        this.pnlrtbPageInfo.Visible = false;
        this.pnlPageInfo.Height -= this.pnlrtbPageInfo.Height;
        this.lblPageInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.GroupLine3;
      }
      else
      {
        this.pnlPageInfo.Height += this.pnlrtbPageInfo.Height;
        this.pnlrtbPageInfo.Visible = true;
        this.lblPageInfo.Image = (Image) GSPcLocalViewer.Properties.Resources.GroupLine2;
      }
    }

    private void frmInfo_VisibleChanged(object sender, EventArgs e)
    {
      this.frmParent.informationToolStripMenuItem.Checked = this.Visible;
    }

    public void ShowLoading()
    {
      this.ShowLoading(this.pnlForm);
    }

    private void ShowLoading(Panel parentPanel)
    {
      try
      {
        if (this.pnlForm.InvokeRequired)
        {
          this.Invoke((Delegate) new frmInfo.ShowLoadingDelegate(this.ShowLoading), (object) this.pnlForm);
        }
        else
        {
          this.picLoading.Parent = (Control) parentPanel;
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
          this.Invoke((Delegate) new frmInfo.HideLoadingDelegate(this.HideLoading), (object) this.pnlForm);
        }
        else
        {
          this.picLoading.Hide();
          this.picLoading.Size = new Size(32, 32);
          this.picLoading.Parent = (Control) this.pnlForm;
        }
      }
      catch
      {
      }
    }

    public void LoadResources()
    {
      this.Text = this.GetResource("Information", "INFORMATION", ResourceType.TITLE) + "      ";
      this.lblBookInfo.Text = this.GetResource("Book Information", "BOOK_INFORMATION", ResourceType.LABEL);
      this.lblPageInfo.Text = this.GetResource("Page Information", "PAGE_INFORMATION", ResourceType.LABEL);
      this.LoadBookInfo();
      this.LoadPageInfo(this.schemaNodeParent, this.pageNodeParent);
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MAIN_FORM']" + "/Screen[@Name='INFORMATION']";
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

    private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
    {
      frmInfo.HideCaret(this.rtbBookInfo.Handle);
    }

    private void rtbPageInfo_MouseDown_1(object sender, MouseEventArgs e)
    {
      frmInfo.HideCaret(this.rtbPageInfo.Handle);
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      frmInfo.HideCaret(this.rtbPageInfo.Handle);
      frmInfo.HideCaret(this.rtbBookInfo.Handle);
    }

    private delegate void ShowPanelsDelegate(bool value);

    private delegate void LoadBookInfoDelegate();

    private delegate void LoadPageInfoDelegate(XmlNode schemaNode, XmlNode pageNode);

    private delegate void ShowLoadingDelegate(Panel parentPanel);

    private delegate void HideLoadingDelegate(Panel parentPanel);
  }
}

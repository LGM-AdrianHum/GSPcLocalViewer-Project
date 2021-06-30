// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.LabledTextBox
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class LabledTextBox : UserControl
  {
    private IContainer components;
    private Panel pnlControl;
    private Label lbl;
    private TextBox txt;
    private ToolTip tt;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.pnlControl = new Panel();
      this.txt = new TextBox();
      this.lbl = new Label();
      this.tt = new ToolTip(this.components);
      this.pnlControl.SuspendLayout();
      this.SuspendLayout();
      this.pnlControl.Controls.Add((Control) this.txt);
      this.pnlControl.Controls.Add((Control) this.lbl);
      this.pnlControl.Dock = DockStyle.Fill;
      this.pnlControl.Location = new Point(0, 0);
      this.pnlControl.Name = "pnlControl";
      this.pnlControl.Padding = new Padding(2);
      this.pnlControl.Size = new Size(213, 24);
      this.pnlControl.TabIndex = 0;
      this.txt.BorderStyle = BorderStyle.FixedSingle;
      this.txt.Dock = DockStyle.Left;
      this.txt.Location = new Point(102, 2);
      this.txt.Name = "txt";
      this.txt.Size = new Size(109, 20);
      this.txt.TabIndex = 1;
      this.lbl.Dock = DockStyle.Left;
      this.lbl.Location = new Point(2, 2);
      this.lbl.Name = "lbl";
      this.lbl.Size = new Size(100, 20);
      this.lbl.TabIndex = 0;
      this.lbl.Text = "Caption";
      this.lbl.TextAlign = ContentAlignment.MiddleLeft;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.pnlControl);
      this.Name = nameof (LabledTextBox);
      this.Size = new Size(213, 24);
      this.pnlControl.ResumeLayout(false);
      this.pnlControl.PerformLayout();
      this.ResumeLayout(false);
    }

    public string _Name { get; set; }

    public string _Caption
    {
      get
      {
        return this.lbl.Text;
      }
      set
      {
        this.tt.SetToolTip((Control) this.lbl, value);
        this.lbl.Text = value;
      }
    }

    public string _Text
    {
      get
      {
        return this.txt.Text;
      }
      set
      {
        this.txt.Text = value;
      }
    }

    public string _ID
    {
      get
      {
        return this.lbl.Name;
      }
      set
      {
        this.tt.SetToolTip((Control) this.lbl, value);
        this.lbl.Name = value;
      }
    }

    public LabledTextBox()
    {
      this.InitializeComponent();
      this._Name = string.Empty;
      this.tt.SetToolTip((Control) this.lbl, this.lbl.Text);
    }

    public LabledTextBox(string Caption)
    {
      this.InitializeComponent();
      this._Name = string.Empty;
      this.lbl.Text = Caption;
      this.tt.SetToolTip((Control) this.lbl, this.lbl.Text);
    }

    public LabledTextBox(string Caption, string Text)
    {
      this.InitializeComponent();
      this._Name = string.Empty;
      this.lbl.Text = Caption;
      this.txt.Text = Text;
      this.tt.SetToolTip((Control) this.lbl, this.lbl.Text);
    }
  }
}

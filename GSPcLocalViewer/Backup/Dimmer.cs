// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Dimmer
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class Dimmer : Form
  {
    private IContainer components;

    public Dimmer()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.BackColor = SystemColors.Control;
    }

    private void Dimmer_Paint(object sender, PaintEventArgs e)
    {
      this.CreateGraphics().DrawRectangle(new Pen(SystemColors.Control, 1f), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleMode = AutoScaleMode.None;
      this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      this.AutoValidate = AutoValidate.Disable;
      this.ClientSize = new Size(342, 279);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Name = nameof (Dimmer);
      this.Opacity = 0.4;
      this.Padding = new Padding(1);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = nameof (Dimmer);
      this.Paint += new PaintEventHandler(this.Dimmer_Paint);
      this.ResumeLayout(false);
    }
  }
}

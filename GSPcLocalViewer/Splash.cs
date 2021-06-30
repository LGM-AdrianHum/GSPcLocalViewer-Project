// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Splash
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class Splash : Form
  {
    private IContainer components;
    private PictureBox pictureBox2;
    private ProgressBar progressBar1;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pictureBox2 = new PictureBox();
      this.progressBar1 = new ProgressBar();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.pictureBox2.BackColor = Color.Transparent;
      this.pictureBox2.Dock = DockStyle.Fill;
      this.pictureBox2.Image = (Image) Resources.Splash;
      this.pictureBox2.Location = new Point(0, 0);
      this.pictureBox2.Margin = new Padding(0);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(274, 173);
      this.pictureBox2.TabIndex = 0;
      this.pictureBox2.TabStop = false;
      this.progressBar1.Location = new Point(10, 146);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(252, 15);
      this.progressBar1.Style = ProgressBarStyle.Marquee;
      this.progressBar1.TabIndex = 2;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(274, 173);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.pictureBox2);
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Splash);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (Splash);
      this.TransparencyKey = Color.WhiteSmoke;
      this.FormClosing += new FormClosingEventHandler(this.Splash_FormClosing);
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
    }

    public Splash()
    {
      this.InitializeComponent();
    }

    private void Splash_FormClosing(object sender, FormClosingEventArgs e)
    {
    }
  }
}

using GSPcLocalViewer.Properties;
using System;
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

		public Splash()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.pictureBox2 = new PictureBox();
			this.progressBar1 = new ProgressBar();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			base.SuspendLayout();
			this.pictureBox2.BackColor = Color.Transparent;
			this.pictureBox2.Dock = DockStyle.Fill;
			this.pictureBox2.Image = Resources.Splash;
			this.pictureBox2.Location = new Point(0, 0);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(274, 173);
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			this.progressBar1.Location = new Point(10, 146);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(252, 15);
			this.progressBar1.Style = ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(274, 173);
			base.Controls.Add(this.progressBar1);
			base.Controls.Add(this.pictureBox2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Splash";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Splash";
			base.TransparencyKey = Color.WhiteSmoke;
			base.FormClosing += new FormClosingEventHandler(this.Splash_FormClosing);
			((ISupportInitialize)this.pictureBox2).EndInit();
			base.ResumeLayout(false);
		}

		private void Splash_FormClosing(object sender, FormClosingEventArgs e)
		{
		}
	}
}
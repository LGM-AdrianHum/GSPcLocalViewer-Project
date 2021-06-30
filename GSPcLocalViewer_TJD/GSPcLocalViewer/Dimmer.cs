using System;
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
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.BackColor = SystemColors.Control;
		}

		private void Dimmer_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphic = base.CreateGraphics();
			Pen pen = new Pen(SystemColors.Control, 1f);
			Rectangle rectangle = new Rectangle(0, 0, base.Width - 1, base.Height - 1);
			graphic.DrawRectangle(pen, rectangle);
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
			base.SuspendLayout();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			base.ClientSize = new System.Drawing.Size(342, 279);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "Dimmer";
			base.Opacity = 0.4;
			base.Padding = new System.Windows.Forms.Padding(1);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.Manual;
			this.Text = "Dimmer";
			base.Paint += new PaintEventHandler(this.Dimmer_Paint);
			base.ResumeLayout(false);
		}
	}
}
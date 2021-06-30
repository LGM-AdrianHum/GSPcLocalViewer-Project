using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	internal class CustomToolStripMenuItem : ToolStripMenuItem
	{
		private bool bOnDel;

		public CustomToolStripMenuItem(string sText)
		{
			base.Text = sText.Replace("&", "&&");
			this.bOnDel = false;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			try
			{
				Rectangle bounds = this.Bounds;
				Rectangle rectangle = new Rectangle(bounds.Width - 20, 2, 16, 17);
				if (e.X < rectangle.Left || e.X > rectangle.Right || e.Y < rectangle.Top || e.Y > rectangle.Bottom || !this.bOnDel)
				{
					this.OnOpen(this);
				}
				else
				{
					this.OnDelete(this);
				}
			}
			catch
			{
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			try
			{
				Rectangle bounds = this.Bounds;
				Rectangle rectangle = new Rectangle(bounds.Width - 20, 2, 16, 17);
				if (e.X >= rectangle.Left && e.X <= rectangle.Right && e.Y >= rectangle.Top && e.Y <= rectangle.Bottom && !this.bOnDel)
				{
					base.Invalidate();
					this.bOnDel = true;
				}
				else if ((e.X < rectangle.Left || e.X > rectangle.Right || e.Y < rectangle.Top || e.Y > rectangle.Bottom) && this.bOnDel)
				{
					base.Invalidate();
					this.bOnDel = false;
				}
			}
			catch
			{
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			SolidBrush solidBrush;
			base.OnPaint(e);
			Rectangle bounds = this.Bounds;
			Rectangle rectangle = new Rectangle(bounds.Width - 20, 2, 16, 17);
			if (this.bOnDel)
			{
				solidBrush = new SolidBrush(Color.LightBlue);
				e.Graphics.FillRectangle(solidBrush, rectangle);
				solidBrush.Color = Color.Blue;
				e.Graphics.DrawRectangle(new Pen(solidBrush, 1f), rectangle);
			}
			solidBrush = new SolidBrush(Color.Gray);
			Rectangle bounds1 = this.Bounds;
			Rectangle rectangle1 = new Rectangle(bounds1.Width - 15, 8, 6, 6);
			e.Graphics.DrawLine(new Pen(solidBrush, 2f), new Point(rectangle1.Right, rectangle1.Top), new Point(rectangle1.Left, rectangle1.Bottom));
			e.Graphics.DrawLine(new Pen(solidBrush, 2f), new Point(rectangle1.Left, rectangle1.Top), new Point(rectangle1.Right, rectangle1.Bottom));
		}

		public event CustomToolStripMenuItem.ClickHandler OnDelete;

		public event CustomToolStripMenuItem.ClickHandler OnOpen;

		public delegate void ClickHandler(CustomToolStripMenuItem sender);
	}
}
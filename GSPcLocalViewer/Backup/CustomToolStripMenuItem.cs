// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.CustomToolStripMenuItem
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  internal class CustomToolStripMenuItem : ToolStripMenuItem
  {
    private bool bOnDel;

    public event CustomToolStripMenuItem.ClickHandler OnOpen;

    public event CustomToolStripMenuItem.ClickHandler OnDelete;

    public CustomToolStripMenuItem(string sText)
    {
      this.Text = sText.Replace("&", "&&");
      this.bOnDel = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Rectangle rect = new Rectangle(this.Bounds.Width - 20, 2, 16, 17);
      if (this.bOnDel)
      {
        SolidBrush solidBrush = new SolidBrush(Color.LightBlue);
        e.Graphics.FillRectangle((Brush) solidBrush, rect);
        solidBrush.Color = Color.Blue;
        e.Graphics.DrawRectangle(new Pen((Brush) solidBrush, 1f), rect);
      }
      SolidBrush solidBrush1 = new SolidBrush(Color.Gray);
      Rectangle rectangle = new Rectangle(this.Bounds.Width - 15, 8, 6, 6);
      e.Graphics.DrawLine(new Pen((Brush) solidBrush1, 2f), new Point(rectangle.Right, rectangle.Top), new Point(rectangle.Left, rectangle.Bottom));
      e.Graphics.DrawLine(new Pen((Brush) solidBrush1, 2f), new Point(rectangle.Left, rectangle.Top), new Point(rectangle.Right, rectangle.Bottom));
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      try
      {
        Rectangle rectangle = new Rectangle(this.Bounds.Width - 20, 2, 16, 17);
        if (e.X >= rectangle.Left && e.X <= rectangle.Right && (e.Y >= rectangle.Top && e.Y <= rectangle.Bottom) && this.bOnDel)
          this.OnDelete(this);
        else
          this.OnOpen(this);
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
        Rectangle rectangle = new Rectangle(this.Bounds.Width - 20, 2, 16, 17);
        if (e.X >= rectangle.Left && e.X <= rectangle.Right && (e.Y >= rectangle.Top && e.Y <= rectangle.Bottom) && !this.bOnDel)
        {
          this.Invalidate();
          this.bOnDel = true;
        }
        else
        {
          if (e.X >= rectangle.Left && e.X <= rectangle.Right && (e.Y >= rectangle.Top && e.Y <= rectangle.Bottom) || !this.bOnDel)
            return;
          this.Invalidate();
          this.bOnDel = false;
        }
      }
      catch
      {
      }
    }

    public delegate void ClickHandler(CustomToolStripMenuItem sender);
  }
}

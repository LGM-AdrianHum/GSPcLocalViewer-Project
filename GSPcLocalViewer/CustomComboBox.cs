// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.CustomComboBox
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  internal class CustomComboBox : ComboBox
  {
    public CustomComboBox()
    {
      this.DrawMode = DrawMode.OwnerDrawVariable;
      this.DropDownHeight = this.ItemHeight * 10;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      if (e.Index <= -1)
        return;
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
      {
        SolidBrush solidBrush1 = new SolidBrush(Settings.Default.appHighlightBackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(Settings.Default.appHighlightForeColor);
        e.Graphics.DrawString(this.Items[e.Index].ToString(), Settings.Default.appFont, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
      else
      {
        SolidBrush solidBrush1 = new SolidBrush(this.BackColor);
        e.Graphics.FillRectangle((Brush) solidBrush1, e.Bounds);
        SolidBrush solidBrush2 = new SolidBrush(this.ForeColor);
        e.Graphics.DrawString(this.Items[e.Index].ToString(), Settings.Default.appFont, (Brush) solidBrush2, (RectangleF) e.Bounds);
      }
    }
  }
}

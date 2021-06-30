// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.CustomTreeView
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class CustomTreeView : TreeView
  {
    public CustomTreeView()
    {
      this.DrawMode = TreeViewDrawMode.OwnerDrawText;
    }

    protected override void OnDrawNode(DrawTreeNodeEventArgs e)
    {
      Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
      Color foreColor = e.Node.ForeColor;
      if (foreColor == Color.Empty)
        foreColor = e.Node.TreeView.ForeColor;
      if (e.Node == e.Node.TreeView.SelectedNode)
      {
        Color highlightText = SystemColors.HighlightText;
        e.Graphics.FillRectangle((Brush) new SolidBrush(Settings.Default.appHighlightBackColor), e.Bounds);
        TextRenderer.DrawText((IDeviceContext) e.Graphics, e.Node.Text, font, e.Bounds, Settings.Default.appHighlightForeColor, TextFormatFlags.Default);
      }
      else
      {
        e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
        TextRenderer.DrawText((IDeviceContext) e.Graphics, e.Node.Text, font, e.Bounds, foreColor, TextFormatFlags.Default);
      }
    }
  }
}

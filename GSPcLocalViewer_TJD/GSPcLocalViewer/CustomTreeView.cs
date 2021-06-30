using GSPcLocalViewer.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class CustomTreeView : TreeView
	{
		public CustomTreeView()
		{
			base.DrawMode = TreeViewDrawMode.OwnerDrawText;
		}

		protected override void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			System.Drawing.Font nodeFont = e.Node.NodeFont ?? e.Node.TreeView.Font;
			Color foreColor = e.Node.ForeColor;
			if (foreColor == Color.Empty)
			{
				foreColor = e.Node.TreeView.ForeColor;
			}
			if (e.Node != e.Node.TreeView.SelectedNode)
			{
				e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);
				TextRenderer.DrawText(e.Graphics, e.Node.Text, nodeFont, e.Bounds, foreColor, TextFormatFlags.Default);
				return;
			}
			foreColor = SystemColors.HighlightText;
			e.Graphics.FillRectangle(new SolidBrush(Settings.Default.appHighlightBackColor), e.Bounds);
			TextRenderer.DrawText(e.Graphics, e.Node.Text, nodeFont, e.Bounds, Settings.Default.appHighlightForeColor, TextFormatFlags.Default);
		}
	}
}
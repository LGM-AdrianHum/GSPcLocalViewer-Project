using GSPcLocalViewer.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	internal class CustomComboBox : ComboBox
	{
		public CustomComboBox()
		{
			base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			base.DropDownHeight = base.ItemHeight * 10;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Index > -1)
			{
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					SolidBrush solidBrush = new SolidBrush(Settings.Default.appHighlightBackColor);
					e.Graphics.FillRectangle(solidBrush, e.Bounds);
					solidBrush = new SolidBrush(Settings.Default.appHighlightForeColor);
					e.Graphics.DrawString(base.Items[e.Index].ToString(), Settings.Default.appFont, solidBrush, e.Bounds);
					return;
				}
				SolidBrush solidBrush1 = new SolidBrush(this.BackColor);
				e.Graphics.FillRectangle(solidBrush1, e.Bounds);
				solidBrush1 = new SolidBrush(this.ForeColor);
				e.Graphics.DrawString(base.Items[e.Index].ToString(), Settings.Default.appFont, solidBrush1, e.Bounds);
			}
		}
	}
}
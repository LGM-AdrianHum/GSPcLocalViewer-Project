using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GSPcLocalViewer
{
	internal class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
	{
		private Point checkBoxLocation;

		private System.Drawing.Size checkBoxSize;

		private bool _checked;

		private Point _cellLocation = new Point();

		private CheckBoxState _cbState = CheckBoxState.UncheckedNormal;

		public bool Checked
		{
			get
			{
				return this._checked;
			}
			set
			{
				this._checked = value;
				if (this.OnCheckBoxClicked != null)
				{
					base.DataGridView.InvalidateCell(this);
				}
			}
		}

		public DatagridViewCheckBoxHeaderCell()
		{
		}

		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			Point point = new Point(e.X + this._cellLocation.X, e.Y + this._cellLocation.Y);
			if (point.X >= this.checkBoxLocation.X && point.X <= this.checkBoxLocation.X + this.checkBoxSize.Width && point.Y >= this.checkBoxLocation.Y && point.Y <= this.checkBoxLocation.Y + this.checkBoxSize.Height)
			{
				this._checked = !this._checked;
				if (this.OnCheckBoxClicked != null)
				{
					this.OnCheckBoxClicked(this._checked);
					base.DataGridView.InvalidateCell(this);
				}
			}
			base.OnMouseClick(e);
		}

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
			Point x = new Point();
			System.Drawing.Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
			Point location = cellBounds.Location;
			x.X = location.X + cellBounds.Width / 2 - glyphSize.Width / 2;
			Point point = cellBounds.Location;
			x.Y = point.Y + cellBounds.Height / 2 - glyphSize.Height / 2;
			this._cellLocation = cellBounds.Location;
			this.checkBoxLocation = x;
			this.checkBoxSize = glyphSize;
			if (!this._checked)
			{
				this._cbState = CheckBoxState.UncheckedNormal;
			}
			else
			{
				this._cbState = CheckBoxState.CheckedNormal;
			}
			CheckBoxRenderer.DrawCheckBox(graphics, this.checkBoxLocation, this._cbState);
		}

		public event CheckBoxClickedHandler OnCheckBoxClicked;
	}
}
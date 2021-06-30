// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.DatagridViewCheckBoxHeaderCell
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GSPcLocalViewer
{
  internal class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
  {
    private Point _cellLocation = new Point();
    private CheckBoxState _cbState = CheckBoxState.UncheckedNormal;
    private Point checkBoxLocation;
    private Size checkBoxSize;
    private bool _checked;

    public event CheckBoxClickedHandler OnCheckBoxClicked;

    public bool Checked
    {
      get
      {
        return this._checked;
      }
      set
      {
        this._checked = value;
        if (this.OnCheckBoxClicked == null)
          return;
        this.DataGridView.InvalidateCell((DataGridViewCell) this);
      }
    }

    protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
    {
      base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
      Point point = new Point();
      Size glyphSize = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);
      point.X = cellBounds.Location.X + cellBounds.Width / 2 - glyphSize.Width / 2;
      point.Y = cellBounds.Location.Y + cellBounds.Height / 2 - glyphSize.Height / 2;
      this._cellLocation = cellBounds.Location;
      this.checkBoxLocation = point;
      this.checkBoxSize = glyphSize;
      this._cbState = !this._checked ? CheckBoxState.UncheckedNormal : CheckBoxState.CheckedNormal;
      CheckBoxRenderer.DrawCheckBox(graphics, this.checkBoxLocation, this._cbState);
    }

    protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
    {
      Point point = new Point(e.X + this._cellLocation.X, e.Y + this._cellLocation.Y);
      if (point.X >= this.checkBoxLocation.X && point.X <= this.checkBoxLocation.X + this.checkBoxSize.Width && (point.Y >= this.checkBoxLocation.Y && point.Y <= this.checkBoxLocation.Y + this.checkBoxSize.Height))
      {
        this._checked = !this._checked;
        if (this.OnCheckBoxClicked != null)
        {
          this.OnCheckBoxClicked(this._checked);
          this.DataGridView.InvalidateCell((DataGridViewCell) this);
        }
      }
      base.OnMouseClick(e);
    }
  }
}

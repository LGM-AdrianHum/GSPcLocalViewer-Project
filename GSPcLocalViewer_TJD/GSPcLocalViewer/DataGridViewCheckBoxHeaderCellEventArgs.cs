using System;

namespace GSPcLocalViewer
{
	public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
	{
		private bool _bChecked;

		public bool Checked
		{
			get
			{
				return this._bChecked;
			}
		}

		public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
		{
			this._bChecked = bChecked;
		}
	}
}
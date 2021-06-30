using System;
using System.Globalization;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class NumericTextBox : TextBox
	{
		private bool allowSpace;

		public bool AllowSpace
		{
			get
			{
				return this.allowSpace;
			}
			set
			{
				this.allowSpace = value;
			}
		}

		public decimal DecimalValue
		{
			get
			{
				return decimal.Parse(this.Text);
			}
		}

		public int IntValue
		{
			get
			{
				return int.Parse(this.Text);
			}
		}

		public NumericTextBox()
		{
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
			string numberDecimalSeparator = numberFormat.NumberDecimalSeparator;
			string numberGroupSeparator = numberFormat.NumberGroupSeparator;
			string negativeSign = numberFormat.NegativeSign;
			string str = e.KeyChar.ToString();
			if (char.IsDigit(e.KeyChar))
			{
				return;
			}
			if (!str.Equals(numberDecimalSeparator) && !str.Equals(numberGroupSeparator))
			{
				if (str.Equals(negativeSign))
				{
					return;
				}
				if (e.KeyChar == '\b')
				{
					return;
				}
				if (this.allowSpace && e.KeyChar == ' ')
				{
					return;
				}
				e.Handled = true;
			}
		}
	}
}
// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.NumericTextBox
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Globalization;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class NumericTextBox : TextBox
  {
    private bool allowSpace;

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
      string decimalSeparator = numberFormat.NumberDecimalSeparator;
      string numberGroupSeparator = numberFormat.NumberGroupSeparator;
      string negativeSign = numberFormat.NegativeSign;
      string str = e.KeyChar.ToString();
      if (char.IsDigit(e.KeyChar) || str.Equals(decimalSeparator) || (str.Equals(numberGroupSeparator) || str.Equals(negativeSign)) || (e.KeyChar == '\b' || this.allowSpace && e.KeyChar == ' '))
        return;
      e.Handled = true;
    }

    public int IntValue
    {
      get
      {
        return int.Parse(this.Text);
      }
    }

    public Decimal DecimalValue
    {
      get
      {
        return Decimal.Parse(this.Text);
      }
    }

    public bool AllowSpace
    {
      set
      {
        this.allowSpace = value;
      }
      get
      {
        return this.allowSpace;
      }
    }
  }
}

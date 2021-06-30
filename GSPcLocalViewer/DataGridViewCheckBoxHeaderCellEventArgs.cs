// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.DataGridViewCheckBoxHeaderCellEventArgs
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;

namespace GSPcLocalViewer
{
  public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
  {
    private bool _bChecked;

    public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
    {
      this._bChecked = bChecked;
    }

    public bool Checked
    {
      get
      {
        return this._bChecked;
      }
    }
  }
}

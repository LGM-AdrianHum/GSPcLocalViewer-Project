// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.SpecialRefColumn
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;
using System.Collections.Generic;

namespace GSPcLocalViewer
{
  internal class SpecialRefColumn
  {
    public string sRefKey = string.Empty;
    public string sRefValue = string.Empty;
    public int iRefWidth;

    public static SpecialRefColumn CheckRefKeyExist(List<SpecialRefColumn> lstSpecialColumn, string refKey)
    {
      try
      {
        foreach (SpecialRefColumn specialRefColumn in lstSpecialColumn)
        {
          if (specialRefColumn.sRefKey.ToUpper() == refKey)
            return specialRefColumn;
        }
        return (SpecialRefColumn) null;
      }
      catch (Exception ex)
      {
        return (SpecialRefColumn) null;
      }
    }
  }
}

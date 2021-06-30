// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.IniDictonary`3
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.Collections.Generic;

namespace GSPcLocalViewer
{
  public class IniDictonary<Tkey1, Tkey2, Tvalue> : Dictionary<SectionKey<Tkey1, Tkey2>, Tvalue>
  {
    public Tvalue this[Tkey1 section, Tkey2 key]
    {
      get
      {
        try
        {
          return this[new SectionKey<Tkey1, Tkey2>(section, key)];
        }
        catch (KeyNotFoundException ex)
        {
          return default (Tvalue);
        }
      }
      set
      {
        this[new SectionKey<Tkey1, Tkey2>(section, key)] = value;
      }
    }
  }
}

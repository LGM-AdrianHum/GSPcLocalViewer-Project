// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.SectionKey`2
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

namespace GSPcLocalViewer
{
  public class SectionKey<Tkey1, Tkey2>
  {
    public Tkey1 Key1 { get; set; }

    public Tkey2 Key2 { get; set; }

    public SectionKey(Tkey1 key1, Tkey2 key2)
    {
      this.Key1 = key1;
      this.Key2 = key2;
    }

    public override int GetHashCode()
    {
      return this.Key1.GetHashCode() ^ this.Key2.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      SectionKey<Tkey1, Tkey2> sectionKey = obj as SectionKey<Tkey1, Tkey2>;
      if (sectionKey == null || !this.Key1.Equals((object) sectionKey.Key1))
        return false;
      return this.Key2.Equals((object) sectionKey.Key2);
    }
  }
}

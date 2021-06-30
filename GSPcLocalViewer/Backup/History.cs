// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.History
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System.Collections;
using System.Xml;

namespace GSPcLocalViewer
{
  public class History
  {
    private int CurrentIndex;
    private Hashtable HistoryList;
    private bool DontAddToHistory;

    public History()
    {
      this.CurrentIndex = -1;
      this.HistoryList = new Hashtable();
      this.DontAddToHistory = false;
    }

    ~History()
    {
      this.HistoryList.Clear();
      this.HistoryList = (Hashtable) null;
    }

    public void ResetHistory()
    {
      this.CurrentIndex = -1;
      this.HistoryList.Clear();
      this.DontAddToHistory = false;
    }

    public void Add(XmlNode historyNode)
    {
      if (!this.DontAddToHistory)
      {
        if (this.HistoryList.Count == this.CurrentIndex + 1)
        {
          ++this.CurrentIndex;
          this.HistoryList.Add((object) this.CurrentIndex, (object) historyNode);
        }
        else
        {
          for (int index = this.HistoryList.Count - 1; index > this.CurrentIndex; --index)
            this.HistoryList.Remove((object) index);
          ++this.CurrentIndex;
          this.HistoryList.Add((object) this.CurrentIndex, (object) historyNode);
        }
        if (this.HistoryList.Count <= Settings.Default.HistorySize)
          return;
        Hashtable hashtable = new Hashtable();
        for (int index = 0; index < Settings.Default.HistorySize; ++index)
          hashtable.Add((object) index, this.HistoryList[(object) (this.HistoryList.Count - Settings.Default.HistorySize + index)]);
        this.HistoryList.Clear();
        this.HistoryList = hashtable;
        this.CurrentIndex = this.HistoryList.Count - 1;
      }
      else
        this.DontAddToHistory = false;
    }

    public XmlNode Current()
    {
      if (this.CurrentIndex < 0)
        return (XmlNode) null;
      if (this.HistoryList[(object) this.CurrentIndex] == null)
        return (XmlNode) null;
      this.DontAddToHistory = true;
      return (XmlNode) this.HistoryList[(object) this.CurrentIndex];
    }

    public XmlNode Back()
    {
      if (this.CurrentIndex <= 0)
        return (XmlNode) null;
      --this.CurrentIndex;
      if (this.HistoryList[(object) this.CurrentIndex] == null)
        return (XmlNode) null;
      this.DontAddToHistory = true;
      return (XmlNode) this.HistoryList[(object) this.CurrentIndex];
    }

    public Hashtable GetHistoryList
    {
      get
      {
        return this.HistoryList;
      }
    }

    public XmlNode Open(int iIndex)
    {
      if (this.HistoryList[(object) iIndex] == null)
        return (XmlNode) null;
      this.CurrentIndex = iIndex;
      this.DontAddToHistory = true;
      return (XmlNode) this.HistoryList[(object) iIndex];
    }

    public XmlNode Forward()
    {
      if (this.CurrentIndex + 1 >= this.HistoryList.Count)
        return (XmlNode) null;
      ++this.CurrentIndex;
      if (this.HistoryList[(object) this.CurrentIndex] == null)
        return (XmlNode) null;
      this.DontAddToHistory = true;
      return (XmlNode) this.HistoryList[(object) this.CurrentIndex];
    }

    public bool BackEnable
    {
      get
      {
        return this.HistoryList.Count > 0 && this.CurrentIndex + 1 > 1;
      }
    }

    public bool ForwardEnable
    {
      get
      {
        return this.HistoryList.Count > 0 && this.CurrentIndex + 1 < this.HistoryList.Count;
      }
    }

    public bool ListEnable
    {
      get
      {
        return this.HistoryList.Count > 0;
      }
    }
  }
}

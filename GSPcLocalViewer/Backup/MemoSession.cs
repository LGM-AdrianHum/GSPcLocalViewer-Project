// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.MemoSession
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.Collections;
using System.Xml;

namespace GSPcLocalViewer
{
  internal class MemoSession
  {
    private Hashtable globalMemoHash;
    private Hashtable localMemoHash;

    public MemoSession()
    {
      this.globalMemoHash = new Hashtable();
      this.localMemoHash = new Hashtable();
    }

    public void addLocalMemo(int serverId, XmlDocument xDocLocalMemo)
    {
      this.localMemoHash[(object) serverId] = (object) xDocLocalMemo;
    }

    public void addGlobalMemo(int serverId, XmlDocument xDocGlobalMemo)
    {
      this.globalMemoHash[(object) serverId] = (object) xDocGlobalMemo;
    }

    public XmlDocument getLocalMemoDoc(int serverId)
    {
      return (XmlDocument) this.localMemoHash[(object) serverId];
    }

    public XmlDocument getGlobalMemoDoc(int serverId)
    {
      return (XmlDocument) this.globalMemoHash[(object) serverId];
    }
  }
}

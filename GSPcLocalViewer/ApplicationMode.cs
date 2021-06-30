// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.ApplicationMode
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.Runtime.InteropServices;

namespace GSPcLocalViewer
{
  internal class ApplicationMode
  {
    public bool bWorkingOffline;
    public bool bFirstTime;
    public bool bFromPortal;

    [DllImport("wininet.dll")]
    private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

    public ApplicationMode()
    {
      this.bWorkingOffline = !this.InternetConnected;
      this.bFromPortal = false;
      this.bFirstTime = true;
    }

    public bool InternetConnected
    {
      get
      {
        int Description;
        return ApplicationMode.InternetGetConnectedState(out Description, 0);
      }
    }
  }
}

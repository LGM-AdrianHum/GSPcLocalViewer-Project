// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.RegistryReader
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using Microsoft.Win32;

namespace GSPcLocalViewer
{
  public class RegistryReader
  {
    public string Read(string KeyName, string subKey)
    {
      RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(KeyName);
      if (registryKey == null)
        return (string) null;
      try
      {
        return (string) registryKey.GetValue(subKey);
      }
      catch
      {
        return (string) null;
      }
    }
  }
}

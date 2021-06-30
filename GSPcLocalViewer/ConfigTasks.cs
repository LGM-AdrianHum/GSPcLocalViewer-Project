// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.ConfigTasks
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System;

namespace GSPcLocalViewer
{
  [Flags]
  [Serializable]
  public enum ConfigTasks
  {
    Viewer_Font = 0,
    Viewer_Color = 1,
    Viewer_General = 2,
    Memo_Folders = Viewer_General | Viewer_Color, // 0x00000003
    Memo_Settings = 4,
    Search_PageName = Memo_Settings | Viewer_Color, // 0x00000005
    Search_Text = Memo_Settings | Viewer_General, // 0x00000006
    Search_PartName = Search_Text | Viewer_Color, // 0x00000007
    Search_PartNumber = 8,
    Search_Advance = Search_PartNumber | Viewer_Color, // 0x00000009
    SelectionListSettings = Search_PartNumber | Viewer_General, // 0x0000000A
    PartListSettings = SelectionListSettings | Viewer_Color, // 0x0000000B
  }
}

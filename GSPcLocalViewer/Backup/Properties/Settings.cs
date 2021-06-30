// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.Properties.Settings
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GSPcLocalViewer.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "9.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [DefaultSettingValue("White")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public Color appHighlightForeColor
    {
      get
      {
        return (Color) this[nameof (appHighlightForeColor)];
      }
      set
      {
        this[nameof (appHighlightForeColor)] = (object) value;
      }
    }

    [DefaultSettingValue("MediumBlue")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public Color appHighlightBackColor
    {
      get
      {
        return (Color) this[nameof (appHighlightBackColor)];
      }
      set
      {
        this[nameof (appHighlightBackColor)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("Tahoma, 8.25pt")]
    [DebuggerNonUserCode]
    public Font appFont
    {
      get
      {
        return (Font) this[nameof (appFont)];
      }
      set
      {
        this[nameof (appFont)] = (object) value;
      }
    }

    [DefaultSettingValue("0, 0")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public Point frmOpenBookLocation
    {
      get
      {
        return (Point) this[nameof (frmOpenBookLocation)];
      }
      set
      {
        this[nameof (frmOpenBookLocation)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("750, 500")]
    [UserScopedSetting]
    public Size frmOpenBookSize
    {
      get
      {
        return (Size) this[nameof (frmOpenBookSize)];
      }
      set
      {
        this[nameof (frmOpenBookSize)] = (object) value;
      }
    }

    [DefaultSettingValue("Normal")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public FormWindowState frmOpenBookState
    {
      get
      {
        return (FormWindowState) this[nameof (frmOpenBookState)];
      }
      set
      {
        this[nameof (frmOpenBookState)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("10")]
    public int HistorySize
    {
      get
      {
        return (int) this[nameof (HistorySize)];
      }
      set
      {
        this[nameof (HistorySize)] = (object) value;
      }
    }

    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string GlobalMemoFolder
    {
      get
      {
        return (string) this[nameof (GlobalMemoFolder)];
      }
      set
      {
        this[nameof (GlobalMemoFolder)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("False")]
    public bool EnableAdminMemo
    {
      get
      {
        return (bool) this[nameof (EnableAdminMemo)];
      }
      set
      {
        this[nameof (EnableAdminMemo)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool EnableGlobalMemo
    {
      get
      {
        return (bool) this[nameof (EnableGlobalMemo)];
      }
      set
      {
        this[nameof (EnableGlobalMemo)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("True")]
    public bool EnableLocalMemo
    {
      get
      {
        return (bool) this[nameof (EnableLocalMemo)];
      }
      set
      {
        this[nameof (EnableLocalMemo)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("True")]
    public bool PopupPictureMemo
    {
      get
      {
        return (bool) this[nameof (PopupPictureMemo)];
      }
      set
      {
        this[nameof (PopupPictureMemo)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool LocalMemoBackup
    {
      get
      {
        return (bool) this[nameof (LocalMemoBackup)];
      }
      set
      {
        this[nameof (LocalMemoBackup)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string LocalMemoBackupFile
    {
      get
      {
        return (string) this[nameof (LocalMemoBackupFile)];
      }
      set
      {
        this[nameof (LocalMemoBackupFile)] = (object) value;
      }
    }

    [DefaultSettingValue("PAGE")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string DefaultPictureZoom
    {
      get
      {
        return (string) this[nameof (DefaultPictureZoom)];
      }
      set
      {
        this[nameof (DefaultPictureZoom)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    [UserScopedSetting]
    public bool MaintainZoom
    {
      get
      {
        return (bool) this[nameof (MaintainZoom)];
      }
      set
      {
        this[nameof (MaintainZoom)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool ShowPicToolbar
    {
      get
      {
        return (bool) this[nameof (ShowPicToolbar)];
      }
      set
      {
        this[nameof (ShowPicToolbar)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public bool ShowListToolbar
    {
      get
      {
        return (bool) this[nameof (ShowListToolbar)];
      }
      set
      {
        this[nameof (ShowListToolbar)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0, 0")]
    public Point frmSearchLocation
    {
      get
      {
        return (Point) this[nameof (frmSearchLocation)];
      }
      set
      {
        this[nameof (frmSearchLocation)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("750, 500")]
    public Size frmSearchSize
    {
      get
      {
        return (Size) this[nameof (frmSearchSize)];
      }
      set
      {
        this[nameof (frmSearchSize)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("Normal")]
    [UserScopedSetting]
    public FormWindowState frmSearchState
    {
      get
      {
        return (FormWindowState) this[nameof (frmSearchState)];
      }
      set
      {
        this[nameof (frmSearchState)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("English")]
    [DebuggerNonUserCode]
    public string appLanguage
    {
      get
      {
        return (string) this[nameof (appLanguage)];
      }
      set
      {
        this[nameof (appLanguage)] = (object) value;
      }
    }

    [DefaultSettingValue("0")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string appProxyType
    {
      get
      {
        return (string) this[nameof (appProxyType)];
      }
      set
      {
        this[nameof (appProxyType)] = (object) value;
      }
    }

    [DefaultSettingValue("5")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string appProxyTimeOut
    {
      get
      {
        return (string) this[nameof (appProxyTimeOut)];
      }
      set
      {
        this[nameof (appProxyTimeOut)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string appProxyPort
    {
      get
      {
        return (string) this[nameof (appProxyPort)];
      }
      set
      {
        this[nameof (appProxyPort)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string appProxyPassword
    {
      get
      {
        return (string) this[nameof (appProxyPassword)];
      }
      set
      {
        this[nameof (appProxyPassword)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string appProxyLogin
    {
      get
      {
        return (string) this[nameof (appProxyLogin)];
      }
      set
      {
        this[nameof (appProxyLogin)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    public string appProxyIP
    {
      get
      {
        return (string) this[nameof (appProxyIP)];
      }
      set
      {
        this[nameof (appProxyIP)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("100")]
    public int appZoomStep
    {
      get
      {
        return (int) this[nameof (appZoomStep)];
      }
      set
      {
        this[nameof (appZoomStep)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("True")]
    public bool appFitPageForMultiParts
    {
      get
      {
        return (bool) this[nameof (appFitPageForMultiParts)];
      }
      set
      {
        this[nameof (appFitPageForMultiParts)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [UserScopedSetting]
    [DefaultSettingValue("FITPAGE")]
    public string appCurrentZoom
    {
      get
      {
        return (string) this[nameof (appCurrentZoom)];
      }
      set
      {
        this[nameof (appCurrentZoom)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("IEXPLORE")]
    public string appWebBrowser
    {
      get
      {
        return (string) this[nameof (appWebBrowser)];
      }
      set
      {
        this[nameof (appWebBrowser)] = (object) value;
      }
    }

    [DefaultSettingValue("-4|-4|1032|742|Maximized")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string appCurrentSize
    {
      get
      {
        return (string) this[nameof (appCurrentSize)];
      }
      set
      {
        this[nameof (appCurrentSize)] = (object) value;
      }
    }

    [DefaultSettingValue("-1, -1")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public Point frmMemoListLocation
    {
      get
      {
        return (Point) this[nameof (frmMemoListLocation)];
      }
      set
      {
        this[nameof (frmMemoListLocation)] = (object) value;
      }
    }

    [DefaultSettingValue("0, 0")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public Size frmMemoListSize
    {
      get
      {
        return (Size) this[nameof (frmMemoListSize)];
      }
      set
      {
        this[nameof (frmMemoListSize)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("Normal")]
    [DebuggerNonUserCode]
    public FormWindowState frmMemoListState
    {
      get
      {
        return (FormWindowState) this[nameof (frmMemoListState)];
      }
      set
      {
        this[nameof (frmMemoListState)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0, 0")]
    public Point frmDataSizeLocation
    {
      get
      {
        return (Point) this[nameof (frmDataSizeLocation)];
      }
      set
      {
        this[nameof (frmDataSizeLocation)] = (object) value;
      }
    }

    [DefaultSettingValue("480, 450")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public Size frmDataSizeSize
    {
      get
      {
        return (Size) this[nameof (frmDataSizeSize)];
      }
      set
      {
        this[nameof (frmDataSizeSize)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("Normal")]
    [DebuggerNonUserCode]
    public FormWindowState frmDataSizeState
    {
      get
      {
        return (FormWindowState) this[nameof (frmDataSizeState)];
      }
      set
      {
        this[nameof (frmDataSizeState)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool OpenInCurrentInstance
    {
      get
      {
        return (bool) this[nameof (OpenInCurrentInstance)];
      }
      set
      {
        this[nameof (OpenInCurrentInstance)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("0, 0")]
    [DebuggerNonUserCode]
    public Point frmConnectionLocation
    {
      get
      {
        return (Point) this[nameof (frmConnectionLocation)];
      }
      set
      {
        this[nameof (frmConnectionLocation)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    public bool SideBySidePrinting
    {
      get
      {
        return (bool) this[nameof (SideBySidePrinting)];
      }
      set
      {
        this[nameof (SideBySidePrinting)] = (object) value;
      }
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool appPartsListInfoVisible
    {
      get
      {
        return (bool) this[nameof (appPartsListInfoVisible)];
      }
      set
      {
        this[nameof (appPartsListInfoVisible)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool MouseScrollContents
    {
      get
      {
        return (bool) this[nameof (MouseScrollContents)];
      }
      set
      {
        this[nameof (MouseScrollContents)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool MouseScrollPicture
    {
      get
      {
        return (bool) this[nameof (MouseScrollPicture)];
      }
      set
      {
        this[nameof (MouseScrollPicture)] = (object) value;
      }
    }

    [DefaultSettingValue("Black")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public Color PartsListInfoForeColor
    {
      get
      {
        return (Color) this[nameof (PartsListInfoForeColor)];
      }
      set
      {
        this[nameof (PartsListInfoForeColor)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("LightGray")]
    [DebuggerNonUserCode]
    public Color PartsListInfoBackColor
    {
      get
      {
        return (Color) this[nameof (PartsListInfoBackColor)];
      }
      set
      {
        this[nameof (PartsListInfoBackColor)] = (object) value;
      }
    }

    [DefaultSettingValue("True")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool RowSelectionMode
    {
      get
      {
        return (bool) this[nameof (RowSelectionMode)];
      }
      set
      {
        this[nameof (RowSelectionMode)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("300")]
    public int ListSplitterDistance
    {
      get
      {
        return (int) this[nameof (ListSplitterDistance)];
      }
      set
      {
        this[nameof (ListSplitterDistance)] = (object) value;
      }
    }

    [ApplicationScopedSetting]
    [DefaultSettingValue("http://localhost/GSPcLocal3.0/ServerSearch/Search.asmx")]
    [DebuggerNonUserCode]
    [SpecialSetting(SpecialSetting.WebServiceUrl)]
    public string GSPcLocalViewer_localhost_Search
    {
      get
      {
        return (string) this[nameof (GSPcLocalViewer_localhost_Search)];
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    public bool HankakuZenkakuFlag
    {
      get
      {
        return (bool) this[nameof (HankakuZenkakuFlag)];
      }
      set
      {
        this[nameof (HankakuZenkakuFlag)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("True")]
    public bool ExpandAllContents
    {
      get
      {
        return (bool) this[nameof (ExpandAllContents)];
      }
      set
      {
        this[nameof (ExpandAllContents)] = (object) value;
      }
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("1")]
    [UserScopedSetting]
    public int ExpandContentsLevel
    {
      get
      {
        return (int) this[nameof (ExpandContentsLevel)];
      }
      set
      {
        this[nameof (ExpandContentsLevel)] = (object) value;
      }
    }

    [DefaultSettingValue("Tahoma, 8.25pt")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public Font printFont
    {
      get
      {
        return (Font) this[nameof (printFont)];
      }
      set
      {
        this[nameof (printFont)] = (object) value;
      }
    }

    [DefaultSettingValue("Tahoma, 8.25pt")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public Font previewFont
    {
      get
      {
        return (Font) this[nameof (previewFont)];
      }
      set
      {
        this[nameof (previewFont)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DefaultSettingValue("-1, -1")]
    [DebuggerNonUserCode]
    public Point frmMemoLocation
    {
      get
      {
        return (Point) this[nameof (frmMemoLocation)];
      }
      set
      {
        this[nameof (frmMemoLocation)] = (object) value;
      }
    }

    [DefaultSettingValue("0, 0")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public Size frmMemoSize
    {
      get
      {
        return (Size) this[nameof (frmMemoSize)];
      }
      set
      {
        this[nameof (frmMemoSize)] = (object) value;
      }
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Normal")]
    public FormWindowState frmMemoState
    {
      get
      {
        return (FormWindowState) this[nameof (frmMemoState)];
      }
      set
      {
        this[nameof (frmMemoState)] = (object) value;
      }
    }

    private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
    {
    }

    private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.DjVuFeatures
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

namespace GSPcLocalViewer
{
  public class DjVuFeatures
  {
    public string sToolBar = string.Empty;
    public string sPopUpMenu = string.Empty;
    public string sLinks = string.Empty;
    public string sNavigation = string.Empty;
    public string sNavigationPane = string.Empty;
    public string sStartUpZoom = string.Empty;
    public string sRotationAngle = string.Empty;
    public string sEnableFunctions = string.Empty;
    public string sShortcutFunctions = string.Empty;
    private const long TBB_OPEN_CLOSE = 1;
    private const long TBB_EXPORT = 2;
    private const long TBB_PRINT = 4;
    private const long TBB_PAN = 8;
    private const long TBB_SELECT_ZOOM = 16;
    private const long TBB_SELECT_REGION = 32;
    private const long TBB_COPY_IMAGE = 64;
    private const long TBB_IMAGE_ZOOM = 128;
    private const long TBB_IMAGE_ROTATION = 256;
    private const long TBB_PAGE_NAVIGATION = 512;
    private const long TBB_THUMBNAIL = 1024;
    private const long TBB_TOOLBAR = 2048;
    private const long TBB_TEXT_SEARCH = 8192;
    private const long CTRL_ALLOWED = 1;
    private const long SHIFT_ALLOWED = 2;
    private const long M_ALLOWED = 4;
    public long nProvidedFunctions;
    public long nKeyboardShortcuts;
    public string sShowAnnoOnCopy;
    public string sShowAnnoOnPrint;
    public string sShowAnnoOnExport;

    private void DecryptCSSDjVuSettings()
    {
      try
      {
        if (Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "TOOLBAR"] != null)
        {
          this.sToolBar = Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "TOOLBAR"];
          this.sToolBar = Program.objAES.DLLDecode(this.sToolBar, "0123456789abcdef");
        }
        if (this.sToolBar.ToUpper() != "ON")
          Program.objAppFeatures.bDjVuToolbar = false;
        if (Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "POPUP_MENU"] != null)
        {
          this.sPopUpMenu = Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "POPUP_MENU"];
          this.sPopUpMenu = Program.objAES.DLLDecode(this.sPopUpMenu, "0123456789abcdef");
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "LINKS"] != null)
        {
          this.sLinks = Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "LINKS"];
          this.sLinks = Program.objAES.DLLDecode(this.sLinks, "0123456789abcdef");
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "NAVIGATION"] != null)
        {
          this.sNavigation = Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "NAVIGATION"];
          this.sNavigation = Program.objAES.DLLDecode(this.sNavigation, "0123456789abcdef");
        }
        if (this.sNavigation == "None")
        {
          this.sNavigationPane = "None";
        }
        else
        {
          if (Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "NAVIGATION_PANE"] != null)
          {
            this.sNavigationPane = Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "NAVIGATION_PANE"];
            this.sNavigationPane = Program.objAES.DLLDecode(this.sNavigationPane, "0123456789abcdef");
          }
          else
            this.sNavigationPane = "None";
          this.sNavigationPane = this.sNavigation + "+" + this.sNavigationPane;
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "ROTATION_ANGLE"] != null)
        {
          this.sRotationAngle = Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "ROTATION_ANGLE"];
          this.sRotationAngle = Program.objAES.DLLDecode(this.sRotationAngle, "0123456789abcdef");
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ANNO_ON_PRINT"] != null)
        {
          this.sShowAnnoOnPrint = Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ANNO_ON_PRINT"];
          this.sShowAnnoOnPrint = Program.objAES.DLLDecode(this.sShowAnnoOnPrint, "0123456789abcdef");
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ANNO_ON_EXPORT"] != null)
        {
          this.sShowAnnoOnExport = Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ANNO_ON_EXPORT"];
          this.sShowAnnoOnExport = Program.objAES.DLLDecode(this.sShowAnnoOnExport, "0123456789abcdef");
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ANNO_ON_COPY"] != null)
        {
          this.sShowAnnoOnCopy = Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ANNO_ON_COPY"];
          this.sShowAnnoOnCopy = Program.objAES.DLLDecode(this.sShowAnnoOnCopy, "0123456789abcdef");
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ENABLE_FUNCTIONS"] != null)
        {
          this.sEnableFunctions = Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "ENABLE_FUNCTIONS"];
          this.sEnableFunctions = Program.objAES.DLLDecode(this.sEnableFunctions, "0123456789abcdef");
        }
        if (Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "SHORTCUT_FUNCTIONS"] != null)
        {
          this.sShortcutFunctions = Program.iniDjVuCtl.items["DJVU_CTL_FUNCTIONS", "SHORTCUT_FUNCTIONS"];
          this.sShortcutFunctions = Program.objAES.DLLDecode(this.sShortcutFunctions, "0123456789abcdef");
        }
        this.nProvidedFunctions = 0L;
        if (this.sEnableFunctions != string.Empty)
        {
          if (this.sEnableFunctions.IndexOf("FILE") >= 0)
            this.nProvidedFunctions |= 1L;
          if (this.sEnableFunctions.IndexOf("PRINT") >= 0)
            this.nProvidedFunctions |= 4L;
          if (this.sEnableFunctions.IndexOf("EXPIMG") >= 0)
            this.nProvidedFunctions |= 2L;
          if (this.sEnableFunctions.IndexOf("SELZOOM") >= 0)
            this.nProvidedFunctions |= 16L;
          if (this.sEnableFunctions.IndexOf("SELRGN") >= 0)
            this.nProvidedFunctions |= 32L;
          if (this.sEnableFunctions.IndexOf("CPYIMG") >= 0)
            this.nProvidedFunctions |= 64L;
          if (this.sEnableFunctions.IndexOf("ROT") >= 0)
            this.nProvidedFunctions |= 256L;
          if (this.sEnableFunctions.IndexOf("TXTSRCH") >= 0)
            this.nProvidedFunctions |= 8192L;
        }
        this.nProvidedFunctions |= 8L;
        this.nProvidedFunctions |= 128L;
        this.nProvidedFunctions |= 1024L;
        this.nProvidedFunctions |= 512L;
        if (this.sToolBar == "on")
          this.nProvidedFunctions |= 2048L;
        this.nKeyboardShortcuts = 0L;
        if (!(this.sShortcutFunctions != ""))
          return;
        if (this.sShortcutFunctions.IndexOf("SELZOOM") >= 0)
          Program.objAppFeatures.bDjVuSelectZoom = true;
        if (this.sShortcutFunctions.IndexOf("CPYIMG") >= 0)
          Program.objAppFeatures.bDjVuCopyImage = true;
        if (this.sShortcutFunctions.IndexOf("MAG") < 0)
          return;
        Program.objAppFeatures.bDjVuMagnifier = true;
        this.nKeyboardShortcuts |= 4L;
      }
      catch
      {
      }
    }

    public DjVuFeatures()
    {
      this.DecryptCSSDjVuSettings();
    }
  }
}

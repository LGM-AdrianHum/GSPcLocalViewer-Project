using System;

namespace GSPcLocalViewer
{
	public class DjVuFeatures
	{
		private const long TBB_OPEN_CLOSE = 1L;

		private const long TBB_EXPORT = 2L;

		private const long TBB_PRINT = 4L;

		private const long TBB_PAN = 8L;

		private const long TBB_SELECT_ZOOM = 16L;

		private const long TBB_SELECT_REGION = 32L;

		private const long TBB_COPY_IMAGE = 64L;

		private const long TBB_IMAGE_ZOOM = 128L;

		private const long TBB_IMAGE_ROTATION = 256L;

		private const long TBB_PAGE_NAVIGATION = 512L;

		private const long TBB_THUMBNAIL = 1024L;

		private const long TBB_TOOLBAR = 2048L;

		private const long TBB_TEXT_SEARCH = 8192L;

		private const long CTRL_ALLOWED = 1L;

		private const long SHIFT_ALLOWED = 2L;

		private const long M_ALLOWED = 4L;

		public string sToolBar = string.Empty;

		public string sPopUpMenu = string.Empty;

		public string sLinks = string.Empty;

		public string sNavigation = string.Empty;

		public string sNavigationPane = string.Empty;

		public string sStartUpZoom = string.Empty;

		public string sRotationAngle = string.Empty;

		public string sEnableFunctions = string.Empty;

		public string sShortcutFunctions = string.Empty;

		public long nProvidedFunctions;

		public long nKeyboardShortcuts;

		public string sShowAnnoOnCopy;

		public string sShowAnnoOnPrint;

		public string sShowAnnoOnExport;

		public DjVuFeatures()
		{
			this.DecryptCSSDjVuSettings();
		}

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
				{
					Program.objAppFeatures.bDjVuToolbar = false;
				}
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
				if (this.sNavigation != "None")
				{
					if (Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "NAVIGATION_PANE"] == null)
					{
						this.sNavigationPane = "None";
					}
					else
					{
						this.sNavigationPane = Program.iniDjVuCtl.items["DJVU_CTL_VIEW", "NAVIGATION_PANE"];
						this.sNavigationPane = Program.objAES.DLLDecode(this.sNavigationPane, "0123456789abcdef");
					}
					this.sNavigationPane = string.Concat(this.sNavigation, "+", this.sNavigationPane);
				}
				else
				{
					this.sNavigationPane = "None";
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
				this.nProvidedFunctions = (long)0;
				if (this.sEnableFunctions != string.Empty)
				{
					if (this.sEnableFunctions.IndexOf("FILE") >= 0)
					{
						this.nProvidedFunctions |= (long)1;
					}
					if (this.sEnableFunctions.IndexOf("PRINT") >= 0)
					{
						this.nProvidedFunctions |= (long)4;
					}
					if (this.sEnableFunctions.IndexOf("EXPIMG") >= 0)
					{
						this.nProvidedFunctions |= (long)2;
					}
					if (this.sEnableFunctions.IndexOf("SELZOOM") >= 0)
					{
						this.nProvidedFunctions |= (long)16;
					}
					if (this.sEnableFunctions.IndexOf("SELRGN") >= 0)
					{
						this.nProvidedFunctions |= (long)32;
					}
					if (this.sEnableFunctions.IndexOf("CPYIMG") >= 0)
					{
						this.nProvidedFunctions |= (long)64;
					}
					if (this.sEnableFunctions.IndexOf("ROT") >= 0)
					{
						this.nProvidedFunctions |= (long)256;
					}
					if (this.sEnableFunctions.IndexOf("TXTSRCH") >= 0)
					{
						this.nProvidedFunctions |= (long)8192;
					}
				}
				this.nProvidedFunctions |= (long)8;
				this.nProvidedFunctions |= (long)128;
				this.nProvidedFunctions |= (long)1024;
				this.nProvidedFunctions |= (long)512;
				if (this.sToolBar == "on")
				{
					this.nProvidedFunctions |= (long)2048;
				}
				this.nKeyboardShortcuts = (long)0;
				if (this.sShortcutFunctions != "")
				{
					if (this.sShortcutFunctions.IndexOf("SELZOOM") >= 0)
					{
						Program.objAppFeatures.bDjVuSelectZoom = true;
					}
					if (this.sShortcutFunctions.IndexOf("CPYIMG") >= 0)
					{
						Program.objAppFeatures.bDjVuCopyImage = true;
					}
					if (this.sShortcutFunctions.IndexOf("MAG") >= 0)
					{
						Program.objAppFeatures.bDjVuMagnifier = true;
						this.nKeyboardShortcuts |= (long)4;
					}
				}
			}
			catch
			{
			}
		}
	}
}
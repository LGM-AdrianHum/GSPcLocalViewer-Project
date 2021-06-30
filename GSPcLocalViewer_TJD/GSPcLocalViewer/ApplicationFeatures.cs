using DjVuPrintLib;
using System;
using System.IO;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	internal class ApplicationFeatures
	{
		public bool bBookSearch;

		public bool bPageNameSearch;

		public bool bTextSearch;

		public bool bPartNumberSearch;

		public bool bPartNameSearch;

		public bool bPartAdvanceSearch;

		public bool bMemo;

		public bool bPrint;

		public bool bBookMark;

		public bool bReference;

		public bool bMenu;

		public bool bCopyRegion;

		public bool bFitPage;

		public bool bDownloadBook;

		public bool bDownloadBookAll;

		public bool bSelectiveZoom;

		public bool bPrintAll;

		public bool bPrintRange;

		public bool bHistory;

		public bool bHelpMenu;

		public bool bAboutMenu;

		public bool bDcMode;

		public bool bDjVuScroll;

		public bool bMiniMap;

		public bool bPrintUsingOcx;

		public bool bAdvanceSearchPageName;

		public bool bAdvanceSearchPartName;

		public bool bAdvanceSearchPartNumber;

		public bool bDirExeute;

		public bool bOpenBookScreen;

		public bool bThirdPartyBasket;

		public bool bDjVuToolbar;

		public bool bDjVuSelectZoom;

		public bool bDjVuMagnifier;

		public bool bDjVuCopyImage;

		public bool bPListSelMode;

		public bool bDjVuPan;

		public bool bDjVuSearch;

		public bool bDjVuSelectText;

		public bool bDjVuZoomIn;

		public bool bDjVuZoomOut;

		public bool bDjVuRotateLeft;

		public bool bDjVuRotateRight;

		public bool bDjVuNavPan;

		public bool bPartsList;

		public bool bSelectionList;

		public bool bDjVuZoomCombobox;

		public ApplicationFeatures(string sFeatures)
		{
			try
			{
				sFeatures = sFeatures.ToUpper();
				string[] strArrays = new string[] { "|" };
				string[] strArrays1 = sFeatures.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					if (strArrays1[i] == "PGNAMSRCH")
					{
						this.bPageNameSearch = true;
					}
					if (strArrays1[i] == "PRTNOSRCH")
					{
						this.bPartNumberSearch = true;
					}
					if (strArrays1[i] == "PRTNAMSRCH")
					{
						this.bPartNameSearch = true;
					}
					if (strArrays1[i] == "PRTADVSRCH")
					{
						this.bPartAdvanceSearch = true;
					}
					if (strArrays1[i] == "TXTSRCH")
					{
						this.bTextSearch = true;
					}
					if (strArrays1[i] == "MEMO")
					{
						this.bMemo = true;
					}
					if (strArrays1[i] == "PRINT")
					{
						this.bPrint = true;
					}
					if (strArrays1[i] == "BKMARK")
					{
						this.bBookMark = true;
					}
					this.bReference = true;
					if (strArrays1[i] == "MENU")
					{
						this.bMenu = true;
					}
					if (strArrays1[i] == "CPYRGN")
					{
						this.bCopyRegion = true;
					}
					if (strArrays1[i] == "FITPAGE")
					{
						this.bFitPage = true;
					}
					if (strArrays1[i] == "DLBOOK")
					{
						this.bDownloadBook = true;
					}
					if (strArrays1[i] == "DLBOOKALL")
					{
						this.bDownloadBookAll = true;
					}
					if (strArrays1[i] == "SELZOOM")
					{
						this.bSelectiveZoom = true;
					}
					if (strArrays1[i] == "PRINTALL")
					{
						this.bPrintAll = true;
					}
					if (strArrays1[i] == "PRINTRNG")
					{
						this.bPrintRange = true;
					}
					if (strArrays1[i] == "HIST")
					{
						this.bHistory = true;
					}
					if (strArrays1[i] == "HELP")
					{
						this.bHelpMenu = true;
					}
					if (strArrays1[i] == "ABOUT")
					{
						this.bAboutMenu = true;
					}
					if (strArrays1[i] == "DCMODE")
					{
						this.bDcMode = true;
					}
					if (strArrays1[i] == "DJVUSCROLL")
					{
						this.bDjVuScroll = true;
					}
					if (strArrays1[i] == "MINIMAP")
					{
						this.bMiniMap = true;
					}
					if (strArrays1[i] == "PRINTOCX")
					{
						this.bPrintUsingOcx = true;
					}
					if (strArrays1[i] == "PGNAMADSRCH")
					{
						this.bAdvanceSearchPageName = true;
					}
					if (strArrays1[i] == "PRTNAMADSRCH")
					{
						this.bAdvanceSearchPartName = true;
					}
					if (strArrays1[i] == "PRTNOADSRCH")
					{
						this.bAdvanceSearchPartNumber = true;
					}
					if (strArrays1[i] == "OPNBK")
					{
						this.bOpenBookScreen = true;
					}
					if (strArrays1[i] == "DIREXE")
					{
						this.bDirExeute = true;
					}
					if (strArrays1[i] == "BASKET")
					{
						this.bThirdPartyBasket = true;
					}
					if (strArrays1[i] == "DJVUPAN")
					{
						this.bDjVuPan = true;
					}
					if (strArrays1[i] == "DJVUSEARCH")
					{
						this.bDjVuSearch = true;
					}
					if (strArrays1[i] == "DJVUSELECTTEXT")
					{
						this.bDjVuSelectText = true;
					}
					if (strArrays1[i] == "DJVUZOOMIN")
					{
						this.bDjVuZoomIn = true;
					}
					if (strArrays1[i] == "DJVUZOOMOUT")
					{
						this.bDjVuZoomOut = true;
					}
					if (strArrays1[i] == "DJVUROTATELEFT")
					{
						this.bDjVuRotateLeft = true;
					}
					if (strArrays1[i] == "DJVUROTATERIGHT")
					{
						this.bDjVuRotateRight = true;
					}
					if (strArrays1[i] == "DJVUNAVPAN")
					{
						this.bDjVuNavPan = true;
					}
					if (strArrays1[i] == "DJVUTBR")
					{
						this.bDjVuToolbar = true;
					}
					if (strArrays1[i] == "PLISTSELMODE")
					{
						this.bPListSelMode = true;
					}
					if (strArrays1[i] == "PARTSLIST")
					{
						this.bPartsList = true;
					}
					if (strArrays1[i] == "SELECTIONLIST")
					{
						this.bSelectionList = true;
					}
					if (strArrays1[i] == "DJVUMAGINFICATIONLIST")
					{
						this.bDjVuZoomCombobox = true;
					}
				}
				if (this.bPrintUsingOcx)
				{
					try
					{
						DjVuFastPrintClass djVuFastPrintClass = new DjVuFastPrintClass();
					}
					catch
					{
						this.bPrintUsingOcx = false;
					}
					if (!File.Exists(string.Concat(Application.StartupPath, "\\PrintManager.exe")))
					{
						this.bPrintUsingOcx = false;
					}
				}
			}
			catch
			{
			}
		}
	}
}
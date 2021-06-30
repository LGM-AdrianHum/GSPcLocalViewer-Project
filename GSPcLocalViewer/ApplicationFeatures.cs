// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.ApplicationFeatures
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

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
        string[] strArray = sFeatures.Split(new string[1]
        {
          "|"
        }, StringSplitOptions.RemoveEmptyEntries);
        for (int index = 0; index < strArray.Length; ++index)
        {
          if (strArray[index] == "PGNAMSRCH")
            this.bPageNameSearch = true;
          if (strArray[index] == "PRTNOSRCH")
            this.bPartNumberSearch = true;
          if (strArray[index] == "PRTNAMSRCH")
            this.bPartNameSearch = true;
          if (strArray[index] == "PRTADVSRCH")
            this.bPartAdvanceSearch = true;
          if (strArray[index] == "TXTSRCH")
            this.bTextSearch = true;
          if (strArray[index] == "MEMO")
            this.bMemo = true;
          if (strArray[index] == "PRINT")
            this.bPrint = true;
          if (strArray[index] == "BKMARK")
            this.bBookMark = true;
          this.bReference = true;
          if (strArray[index] == "MENU")
            this.bMenu = true;
          if (strArray[index] == "CPYRGN")
            this.bCopyRegion = true;
          if (strArray[index] == "FITPAGE")
            this.bFitPage = true;
          if (strArray[index] == "DLBOOK")
            this.bDownloadBook = true;
          if (strArray[index] == "DLBOOKALL")
            this.bDownloadBookAll = true;
          if (strArray[index] == "SELZOOM")
            this.bSelectiveZoom = true;
          if (strArray[index] == "PRINTALL")
            this.bPrintAll = true;
          if (strArray[index] == "PRINTRNG")
            this.bPrintRange = true;
          if (strArray[index] == "HIST")
            this.bHistory = true;
          if (strArray[index] == "HELP")
            this.bHelpMenu = true;
          if (strArray[index] == "ABOUT")
            this.bAboutMenu = true;
          if (strArray[index] == "DCMODE")
            this.bDcMode = true;
          if (strArray[index] == "DJVUSCROLL")
            this.bDjVuScroll = true;
          if (strArray[index] == "MINIMAP")
            this.bMiniMap = true;
          if (strArray[index] == "PRINTOCX")
            this.bPrintUsingOcx = true;
          if (strArray[index] == "PGNAMADSRCH")
            this.bAdvanceSearchPageName = true;
          if (strArray[index] == "PRTNAMADSRCH")
            this.bAdvanceSearchPartName = true;
          if (strArray[index] == "PRTNOADSRCH")
            this.bAdvanceSearchPartNumber = true;
          if (strArray[index] == "OPNBK")
            this.bOpenBookScreen = true;
          if (strArray[index] == "DIREXE")
            this.bDirExeute = true;
          if (strArray[index] == "BASKET")
            this.bThirdPartyBasket = true;
          if (strArray[index] == "DJVUPAN")
            this.bDjVuPan = true;
          if (strArray[index] == "DJVUSEARCH")
            this.bDjVuSearch = true;
          if (strArray[index] == "DJVUSELECTTEXT")
            this.bDjVuSelectText = true;
          if (strArray[index] == "DJVUZOOMIN")
            this.bDjVuZoomIn = true;
          if (strArray[index] == "DJVUZOOMOUT")
            this.bDjVuZoomOut = true;
          if (strArray[index] == "DJVUROTATELEFT")
            this.bDjVuRotateLeft = true;
          if (strArray[index] == "DJVUROTATERIGHT")
            this.bDjVuRotateRight = true;
          if (strArray[index] == "DJVUNAVPAN")
            this.bDjVuNavPan = true;
          if (strArray[index] == "DJVUTBR")
            this.bDjVuToolbar = true;
          if (strArray[index] == "PLISTSELMODE")
            this.bPListSelMode = true;
          if (strArray[index] == "PARTSLIST")
            this.bPartsList = true;
          if (strArray[index] == "SELECTIONLIST")
            this.bSelectionList = true;
          if (strArray[index] == "DJVUMAGINFICATIONLIST")
            this.bDjVuZoomCombobox = true;
        }
        if (!this.bPrintUsingOcx)
          return;
        try
        {
          DjVuFastPrintClass vuFastPrintClass = new DjVuFastPrintClass();
        }
        catch
        {
          this.bPrintUsingOcx = false;
        }
        if (File.Exists(Application.StartupPath + "\\PrintManager.exe"))
          return;
        this.bPrintUsingOcx = false;
      }
      catch
      {
      }
    }
  }
}

using System;

namespace GSPcLocalViewer
{
	[Flags]
	[Serializable]
	public enum ConfigTasks
	{
		Viewer_Font,
		Viewer_Color,
		Viewer_General,
		Memo_Folders,
		Memo_Settings,
		Search_PageName,
		Search_Text,
		Search_PartName,
		Search_PartNumber,
		Search_Advance,
		SelectionListSettings,
		PartListSettings
	}
}
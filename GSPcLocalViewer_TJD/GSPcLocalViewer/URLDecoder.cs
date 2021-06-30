using System;

namespace GSPcLocalViewer
{
	internal class URLDecoder
	{
		public URLDecoder()
		{
		}

		public string URLDecode(string sEncodedString)
		{
			string empty;
			try
			{
				empty = Uri.UnescapeDataString(sEncodedString);
			}
			catch
			{
				empty = string.Empty;
			}
			return empty;
		}
	}
}
using System;
using System.Collections.Generic;

namespace GSPcLocalViewer
{
	internal class SpecialRefColumn
	{
		public string sRefKey = string.Empty;

		public string sRefValue = string.Empty;

		public int iRefWidth;

		public SpecialRefColumn()
		{
		}

		public static SpecialRefColumn CheckRefKeyExist(List<SpecialRefColumn> lstSpecialColumn, string refKey)
		{
			SpecialRefColumn specialRefColumn;
			try
			{
				foreach (SpecialRefColumn specialRefColumn1 in lstSpecialColumn)
				{
					if (specialRefColumn1.sRefKey.ToUpper() != refKey)
					{
						continue;
					}
					specialRefColumn = specialRefColumn1;
					return specialRefColumn;
				}
				specialRefColumn = null;
			}
			catch (Exception exception)
			{
				specialRefColumn = null;
			}
			return specialRefColumn;
		}
	}
}
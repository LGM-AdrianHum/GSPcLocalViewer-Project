using GSPcLocalViewer;
using System;
using System.Collections;
using System.Xml;

namespace GSPcLocalViewer.Classes
{
	internal class Filter
	{
		private frmViewer frmMain;

		public Filter(frmViewer fm)
		{
			this.frmMain = fm;
		}

		public XmlNodeList FilterBooksList(XmlNode xSchemaNode, XmlNodeList xNodeList)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("ID"))
				{
					arrayLists.Add(attribute.Name);
				}
				else if (!attribute.Value.ToUpper().Equals("PUBLISHINGID"))
				{
					if (!attribute.Value.ToUpper().Equals("UPDATEDATE"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.frmMain.p_ArgsF == null || (int)this.frmMain.p_ArgsF.Length <= 0)
			{
				return xNodeList;
			}
			return this.FilterNodeList(xNodeList, arrayLists);
		}

		private XmlNode FilterNode(XmlNode xNode, ArrayList arrMandatory)
		{
			string[] strArrays;
			bool flag = false;
			if (xNode.HasChildNodes)
			{
				foreach (XmlNode childNode in xNode.ChildNodes)
				{
					if (childNode.Name.ToUpper() == "SEL")
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value = childNode.Attributes[0].Value;
						string[] strArrays1 = new string[] { "," };
						strArrays = value.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays2 = strArrays;
						int num = 0;
						while (num < (int)strArrays2.Length)
						{
							if (!arrMandatory.Contains(strArrays2[num]))
							{
								num++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF = this.frmMain.p_ArgsF;
						for (int i = 0; i < (int)pArgsF.Length; i++)
						{
							string str = pArgsF[i];
							string[] strArrays3 = new string[] { "=" };
							string[] strArrays4 = str.Split(strArrays3, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays4.Length == 2)
							{
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									if (!(attribute.Name.ToUpper() == strArrays4[0].ToUpper()) || this.ValueMatchesSelectFilter(strArrays4[1], attribute.Value, false))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays5 = strArrays;
										for (int j = 0; j < (int)strArrays5.Length; j++)
										{
											string str1 = strArrays5[j];
											if (xNode.Attributes[str1] != null)
											{
												xNode.Attributes.Remove(xNode.Attributes[str1]);
											}
										}
									}
									else
									{
										xNode.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else if (childNode.Name.ToUpper() != "SWT")
					{
						if (!(childNode.Name.ToUpper() == "RNG") || childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value1 = childNode.Attributes[0].Value;
						string[] strArrays6 = new string[] { "," };
						strArrays = value1.Split(strArrays6, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays7 = strArrays;
						int num1 = 0;
						while (num1 < (int)strArrays7.Length)
						{
							if (!arrMandatory.Contains(strArrays7[num1]))
							{
								num1++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF1 = this.frmMain.p_ArgsF;
						for (int k = 0; k < (int)pArgsF1.Length; k++)
						{
							string str2 = pArgsF1[k];
							string[] strArrays8 = new string[] { "=" };
							string[] strArrays9 = str2.Split(strArrays8, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays9.Length == 2)
							{
								foreach (XmlAttribute xmlAttribute in childNode.Attributes)
								{
									if (!(xmlAttribute.Name.ToUpper() == strArrays9[0].ToUpper()) || this.ValueInRangeFilter(xmlAttribute.Value, strArrays9[1]))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays10 = strArrays;
										for (int l = 0; l < (int)strArrays10.Length; l++)
										{
											string str3 = strArrays10[l];
											if (xNode.Attributes[str3] != null)
											{
												xNode.Attributes.Remove(xNode.Attributes[str3]);
											}
										}
									}
									else
									{
										xNode.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value2 = childNode.Attributes[0].Value;
						string[] strArrays11 = new string[] { "," };
						strArrays = value2.Split(strArrays11, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays12 = strArrays;
						int num2 = 0;
						while (num2 < (int)strArrays12.Length)
						{
							if (!arrMandatory.Contains(strArrays12[num2]))
							{
								num2++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF2 = this.frmMain.p_ArgsF;
						for (int m = 0; m < (int)pArgsF2.Length; m++)
						{
							string str4 = pArgsF2[m];
							string[] strArrays13 = new string[] { "=" };
							string[] strArrays14 = str4.Split(strArrays13, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays14.Length == 2)
							{
								foreach (XmlAttribute attribute1 in childNode.Attributes)
								{
									if (!(attribute1.Name.ToUpper() == strArrays14[0].ToUpper()) || !(strArrays14[1].ToUpper() == "ON") || !(attribute1.Value.ToUpper() == "ON"))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays15 = strArrays;
										for (int n = 0; n < (int)strArrays15.Length; n++)
										{
											string str5 = strArrays15[n];
											if (xNode.Attributes[str5] != null)
											{
												xNode.Attributes.Remove(xNode.Attributes[str5]);
											}
										}
									}
									else
									{
										xNode.RemoveAll();
										break;
									}
								}
							}
						}
					}
				}
			}
			return xNode;
		}

		private XmlNodeList FilterNodeList(XmlNodeList xNodeList, ArrayList arrMandatory)
		{
			string[] strArrays;
			bool flag = false;
			foreach (XmlNode xmlNodes in xNodeList)
			{
				if (!xmlNodes.HasChildNodes)
				{
					continue;
				}
				foreach (XmlNode childNode in xmlNodes.ChildNodes)
				{
					if (childNode.Name.ToUpper() == "SEL")
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value = childNode.Attributes[0].Value;
						string[] strArrays1 = new string[] { "," };
						strArrays = value.Split(strArrays1, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays2 = strArrays;
						int num = 0;
						while (num < (int)strArrays2.Length)
						{
							if (!arrMandatory.Contains(strArrays2[num]))
							{
								num++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF = this.frmMain.p_ArgsF;
						for (int i = 0; i < (int)pArgsF.Length; i++)
						{
							string str = pArgsF[i];
							string[] strArrays3 = new string[] { "=" };
							string[] strArrays4 = str.Split(strArrays3, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays4.Length == 2)
							{
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									if (!(attribute.Name.ToUpper() == strArrays4[0].ToUpper()) || this.ValueMatchesSelectFilter(strArrays4[1], attribute.Value, false))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays5 = strArrays;
										for (int j = 0; j < (int)strArrays5.Length; j++)
										{
											string str1 = strArrays5[j];
											if (xmlNodes.Attributes[str1] != null)
											{
												xmlNodes.Attributes.Remove(xmlNodes.Attributes[str1]);
											}
										}
									}
									else
									{
										xmlNodes.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else if (childNode.Name.ToUpper() != "SWT")
					{
						if (!(childNode.Name.ToUpper() == "RNG") || childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value1 = childNode.Attributes[0].Value;
						string[] strArrays6 = new string[] { "," };
						strArrays = value1.Split(strArrays6, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays7 = strArrays;
						int num1 = 0;
						while (num1 < (int)strArrays7.Length)
						{
							if (!arrMandatory.Contains(strArrays7[num1]))
							{
								num1++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF1 = this.frmMain.p_ArgsF;
						for (int k = 0; k < (int)pArgsF1.Length; k++)
						{
							string str2 = pArgsF1[k];
							string[] strArrays8 = new string[] { "=" };
							string[] strArrays9 = str2.Split(strArrays8, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays9.Length == 2)
							{
								foreach (XmlAttribute xmlAttribute in childNode.Attributes)
								{
									if (!(xmlAttribute.Name.ToUpper() == strArrays9[0].ToUpper()) || this.ValueInRangeFilter(xmlAttribute.Value, strArrays9[1]))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays10 = strArrays;
										for (int l = 0; l < (int)strArrays10.Length; l++)
										{
											string str3 = strArrays10[l];
											if (xmlNodes.Attributes[str3] != null)
											{
												xmlNodes.Attributes.Remove(xmlNodes.Attributes[str3]);
											}
										}
									}
									else
									{
										xmlNodes.RemoveAll();
										break;
									}
								}
							}
						}
					}
					else
					{
						if (childNode.Attributes.Count <= 0 || !(childNode.Attributes[0].Name.ToUpper() == "TG"))
						{
							continue;
						}
						string value2 = childNode.Attributes[0].Value;
						string[] strArrays11 = new string[] { "," };
						strArrays = value2.Split(strArrays11, StringSplitOptions.RemoveEmptyEntries);
						flag = false;
						string[] strArrays12 = strArrays;
						int num2 = 0;
						while (num2 < (int)strArrays12.Length)
						{
							if (!arrMandatory.Contains(strArrays12[num2]))
							{
								num2++;
							}
							else
							{
								flag = true;
								break;
							}
						}
						string[] pArgsF2 = this.frmMain.p_ArgsF;
						for (int m = 0; m < (int)pArgsF2.Length; m++)
						{
							string str4 = pArgsF2[m];
							string[] strArrays13 = new string[] { "=" };
							string[] strArrays14 = str4.Split(strArrays13, StringSplitOptions.RemoveEmptyEntries);
							if ((int)strArrays14.Length == 2)
							{
								foreach (XmlAttribute attribute1 in childNode.Attributes)
								{
									if (!(attribute1.Name.ToUpper() == strArrays14[0].ToUpper()) || !(strArrays14[1].ToUpper() == "ON") || !(attribute1.Value.ToUpper() == "ON"))
									{
										continue;
									}
									if (!flag)
									{
										string[] strArrays15 = strArrays;
										for (int n = 0; n < (int)strArrays15.Length; n++)
										{
											string str5 = strArrays15[n];
											if (xmlNodes.Attributes[str5] != null)
											{
												xmlNodes.Attributes.Remove(xmlNodes.Attributes[str5]);
											}
										}
									}
									else
									{
										xmlNodes.RemoveAll();
										break;
									}
								}
							}
						}
					}
				}
			}
			return xNodeList;
		}

		public XmlNode FilterPage(XmlNode xSchemaNode, XmlNode xNode)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (!attribute.Value.ToUpper().Equals("ID"))
				{
					if (!attribute.Value.ToUpper().Equals("PAGENAME"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.frmMain.p_ArgsF == null || (int)this.frmMain.p_ArgsF.Length <= 0)
			{
				return xNode;
			}
			return this.FilterNode(xNode, arrayLists);
		}

		public XmlNodeList FilterPartsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("ID"))
				{
					arrayLists.Add(attribute.Name);
				}
				else if (!attribute.Value.ToUpper().Equals("LINKNUMBER"))
				{
					if (!attribute.Value.ToUpper().Equals("PARTNUMBER"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.frmMain.p_ArgsF == null || (int)this.frmMain.p_ArgsF.Length <= 0)
			{
				return xNodeList;
			}
			return this.FilterNodeList(xNodeList, arrayLists);
		}

		public XmlNodeList FilterPicsList(XmlNode xSchemaNode, XmlNodeList xNodeList)
		{
			ArrayList arrayLists = new ArrayList();
			foreach (XmlAttribute attribute in xSchemaNode.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("PICTUREFILE"))
				{
					arrayLists.Add(attribute.Name);
				}
				else if (!attribute.Value.ToUpper().Equals("UPDATEDATE"))
				{
					if (!attribute.Value.ToUpper().Equals("UPDATEDATEPIC"))
					{
						continue;
					}
					arrayLists.Add(attribute.Name);
				}
				else
				{
					arrayLists.Add(attribute.Name);
				}
			}
			if (this.frmMain.p_ArgsF == null || (int)this.frmMain.p_ArgsF.Length <= 0)
			{
				return xNodeList;
			}
			return this.FilterNodeList(xNodeList, arrayLists);
		}

		private bool ValueInRangeFilter(string range, string value)
		{
			int num;
			int num1;
			int num2;
			bool flag;
			string[] strArrays = new string[] { "**" };
			string[] strArrays1 = range.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			if ((int)strArrays1.Length != 2)
			{
				return false;
			}
			if (int.TryParse(strArrays1[0], out num) && int.TryParse(strArrays1[1], out num1))
			{
				if (num > num1)
				{
					int num3 = num;
					num = num1;
					num1 = num3;
				}
				if (!int.TryParse(value, out num2))
				{
					return false;
				}
				if (num2 >= num && num2 <= num1)
				{
					return true;
				}
				return false;
			}
			try
			{
				DateTime dateTime = DateTime.ParseExact(strArrays1[0], "dd/MM/yyyy", null);
				DateTime dateTime1 = DateTime.ParseExact(strArrays1[1], "dd/MM/yyyy", null);
				DateTime dateTime2 = DateTime.ParseExact(value, "dd/MM/yyyy", null);
				if (dateTime > dateTime1)
				{
					DateTime dateTime3 = dateTime;
					dateTime = dateTime1;
					dateTime1 = dateTime3;
				}
				flag = (!(dateTime2 >= dateTime) || !(dateTime2 <= dateTime1) ? false : true);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private bool ValueMatchesSelectFilter(string values1, string values2, bool caseSensitive)
		{
			bool flag;
			string[] strArrays = new string[] { "," };
			string[] strArrays1 = values1.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
			string[] strArrays2 = new string[] { "," };
			string[] strArrays3 = values2.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
			string[] strArrays4 = strArrays1;
			int num = 0;
		Label0:
			while (num < (int)strArrays4.Length)
			{
				string str = strArrays4[num];
				string[] strArrays5 = strArrays3;
				int num1 = 0;
				while (true)
				{
					if (num1 < (int)strArrays5.Length)
					{
						string str1 = strArrays5[num1];
						if (caseSensitive)
						{
							if (str.Equals(str1))
							{
								flag = true;
								break;
							}
						}
						else if (str.ToUpper().Equals(str1.ToUpper()))
						{
							flag = true;
							break;
						}
						num1++;
					}
					else
					{
						num++;
						goto Label0;
					}
				}
				return flag;
			}
			return false;
		}
	}
}
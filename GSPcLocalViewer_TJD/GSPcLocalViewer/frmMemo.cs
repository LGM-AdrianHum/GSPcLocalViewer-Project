using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmMemo : Form
	{
		public frmMemoTasks objFrmTasks;

		public frmMemoLocal objFrmMemoLocal;

		public frmMemoGlobal objFrmMemoGlobal;

		public frmMemoAdmin objFrmMemoAdmin;

		public XmlNodeList xnlLocalMemo;

		public XmlNodeList xnlGlobalMemo;

		public XmlNodeList xnladminMemo;

		public string sServerKey;

		public string sBookId;

		public string sPageId;

		public string sPicIndex;

		public string sListIndex;

		public string sPartNumber;

		public frmViewer frmParent;

		private string sMemoScope;

		public int intMemoType;

		public bool bNoMemos;

		public bool bMemoDeleted;

		private IContainer components;

		private ToolStripContainer toolStripContainer1;

		private SplitContainer splitPnlMemo;

		public frmMemo(frmViewer frm, XmlNodeList xLocalMemoList, XmlNodeList xGlobalMemoList, XmlNodeList xAdminMemoList, string sPageId, string sPicIndex, string sListIndex, string sPartNumber, string sScope1)
		{
			this.InitializeComponent();
			this.intMemoType = this.GetMemoType();
			this.sMemoScope = sScope1;
			this.frmParent = frm;
			this.CreateForms();
			this.UpdateFont();
			if (!sPartNumber.Trim().Equals(string.Empty))
			{
				this.Text = this.GetResource("Part Memo", "MEMO", ResourceType.TITLE);
			}
			else
			{
				this.Text = this.GetResource("Picture Memo", "FORM_TITLE_PICTURE", ResourceType.LABEL);
			}
			this.xnlLocalMemo = xLocalMemoList;
			this.xnlGlobalMemo = xGlobalMemoList;
			this.xnladminMemo = xAdminMemoList;
			this.sServerKey = Program.iniServers[this.frmParent.ServerId].sIniKey;
			this.sBookId = this.frmParent.BookPublishingId;
			this.sPageId = sPageId;
			this.sPicIndex = sPicIndex;
			this.sListIndex = sListIndex;
			this.sPartNumber = sPartNumber;
		}

		private void AddNewMemos(string memoType, string[] sNodesArr)
		{
			XmlTextReader xmlTextReader;
			XmlNode xmlNodes = null;
			if (sNodesArr != null && (int)sNodesArr.Length > 0)
			{
				if (memoType.ToUpper().Equals("LOCAL"))
				{
					for (int i = 0; i < (int)sNodesArr.Length; i++)
					{
						try
						{
							if (!sNodesArr[i].Contains("^"))
							{
								xmlTextReader = new XmlTextReader(new StringReader(sNodesArr[i]));
								xmlNodes = this.frmParent.xDocLocalMemo.ReadNode(xmlTextReader);
							}
							else
							{
								string str = sNodesArr[i];
								char[] chrArray = new char[] { '\u005E' };
								string[] strArrays = str.TrimEnd(chrArray).Trim().Split(new char[] { '\u005E' });
								for (int j = 0; j < (int)strArrays.Length; j++)
								{
									xmlTextReader = new XmlTextReader(new StringReader(strArrays[j]));
									xmlNodes = this.frmParent.xDocLocalMemo.ReadNode(xmlTextReader);
									if (xmlNodes != null)
									{
										this.frmParent.xDocLocalMemo.DocumentElement.AppendChild(xmlNodes);
									}
								}
								goto yoyo0;
							}
						}
						catch
						{
							xmlNodes = null;
						}
						if (xmlNodes != null)
						{
							this.frmParent.xDocLocalMemo.DocumentElement.AppendChild(xmlNodes);
						}
					yoyo0:
					}
					return;
				}
				if (memoType.ToUpper().Equals("GLOBAL"))
				{
					for (int k = 0; k < (int)sNodesArr.Length; k++)
					{
						try
						{
							if (!sNodesArr[k].Contains("^"))
							{
								xmlTextReader = new XmlTextReader(new StringReader(sNodesArr[k]));
								xmlNodes = this.frmParent.xDocGlobalMemo.ReadNode(xmlTextReader);
							}
							else
							{
								string str1 = sNodesArr[k];
								char[] chrArray1 = new char[] { '\u005E' };
								string[] strArrays1 = str1.TrimEnd(chrArray1).Trim().Split(new char[] { '\u005E' });
								for (int l = 0; l < (int)strArrays1.Length; l++)
								{
									xmlTextReader = new XmlTextReader(new StringReader(strArrays1[l]));
									xmlNodes = this.frmParent.xDocGlobalMemo.ReadNode(xmlTextReader);
									if (xmlNodes != null)
									{
										this.frmParent.xDocGlobalMemo.DocumentElement.AppendChild(xmlNodes);
									}
								}
								goto Label1;
							}
						}
						catch
						{
							xmlNodes = null;
						}
						if (xmlNodes != null)
						{
							this.frmParent.xDocGlobalMemo.DocumentElement.AppendChild(xmlNodes);
						}
					Label1:
					}
				}
			}
		}

		public void CloseAndSaveSettings()
		{
			try
			{
				if (Settings.Default.EnableLocalMemo && this.objFrmMemoLocal.isMemoChanged)
				{
					this.DeletePrevMemos("local");
					if (!this.bMemoDeleted)
					{
						string[] strArrays = this.FilterMemo(this.objFrmMemoLocal.getMemos, "LOCAL");
						if (strArrays != null && (int)strArrays.Length > 0)
						{
							this.AddNewMemos("local", strArrays);
						}
					}
				}
				if (Settings.Default.EnableGlobalMemo && this.objFrmMemoGlobal.isMemoChanged)
				{
					this.DeletePrevMemos("global");
					if (!this.bMemoDeleted)
					{
						string[] strArrays1 = this.FilterMemo(this.objFrmMemoGlobal.getMemos, "GLOBAL");
						if (strArrays1 != null && (int)strArrays1.Length > 0)
						{
							this.AddNewMemos("global", strArrays1);
						}
					}
				}
				if (!this.objFrmMemoLocal.bSaveMemoOnBookLevel && !this.objFrmMemoGlobal.bSaveMemoOnBookLevel || !this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked && !this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || !(this.sPartNumber != string.Empty) || this.intMemoType == 2)
				{
					base.Close();
				}
			}
			catch (Exception exception)
			{
			}
		}

		public void CloseAndSaveSettings(string strMemoType)
		{
			try
			{
				if (strMemoType.ToUpper() == "LOCAL")
				{
					if (Settings.Default.EnableLocalMemo && this.objFrmMemoLocal.isMemoChanged)
					{
						this.DeletePrevMemos("local");
						if (this.intMemoType == 2 || !this.objFrmMemoLocal.bSaveMemoOnBookLevel || !this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked)
						{
							this.AddNewMemos("local", this.objFrmMemoLocal.getMemos);
						}
						else if (!this.bMemoDeleted)
						{
							string[] strArrays = this.FilterMemo(this.objFrmMemoLocal.getMemos, "LOCAL");
							if (strArrays != null && (int)strArrays.Length > 0)
							{
								this.AddNewMemos("local", strArrays);
							}
						}
					}
				}
				else if (strMemoType.ToUpper() == "GLOBAL" && Settings.Default.EnableGlobalMemo && this.objFrmMemoGlobal.isMemoChanged)
				{
					this.DeletePrevMemos("global");
					if (this.intMemoType == 2 || !this.objFrmMemoGlobal.bSaveMemoOnBookLevel || !this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked)
					{
						this.AddNewMemos("global", this.objFrmMemoGlobal.getMemos);
					}
					else if (!this.bMemoDeleted)
					{
						string[] strArrays1 = this.FilterMemo(this.objFrmMemoGlobal.getMemos, "GLOBAL");
						if (strArrays1 != null && (int)strArrays1.Length > 0)
						{
							this.AddNewMemos("global", strArrays1);
						}
					}
				}
				if (!this.objFrmMemoLocal.bSaveMemoOnBookLevel && !this.objFrmMemoGlobal.bSaveMemoOnBookLevel || !this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked && !this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || !(this.sPartNumber != string.Empty) || this.intMemoType == 2)
				{
					base.Close();
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void CreateForms()
		{
			this.objFrmMemoLocal = new frmMemoLocal(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemo.Panel2.Controls.Add(this.objFrmMemoLocal);
			this.objFrmMemoGlobal = new frmMemoGlobal(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemo.Panel2.Controls.Add(this.objFrmMemoGlobal);
			this.objFrmMemoAdmin = new frmMemoAdmin(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemo.Panel2.Controls.Add(this.objFrmMemoAdmin);
			this.objFrmTasks = new frmMemoTasks(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemo.Panel1.Controls.Add(this.objFrmTasks);
		}

		private void DeletePrevMemos(string memoType)
		{
			string[] str;
			bool flag = false;
			XmlNodeList xmlNodeLists = null;
			if (!memoType.ToUpper().Equals("LOCAL"))
			{
				if (memoType.ToUpper().Equals("GLOBAL"))
				{
					if (this.objFrmMemoGlobal.bMultiMemoChange)
					{
						if (this.objFrmMemoGlobal.strMemoChangedType == "")
						{
							XmlDocument xmlDocument = this.frmParent.xDocGlobalMemo;
							string[] strArrays = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@PicIndex='", this.sPicIndex, "'][@ListIndex='", this.sListIndex, "'][@PartNo='", this.sPartNumber, "']" };
							xmlNodeLists = xmlDocument.SelectNodes(string.Concat(strArrays));
						}
						else
						{
							string[] strArrays1 = this.objFrmMemoGlobal.strMemoChangedType.Split(new char[] { '|' });
							frmMemoGlobal _frmMemoGlobal = this.objFrmMemoGlobal;
							string str1 = this.objFrmMemoGlobal.strDelMemoDate;
							char[] chrArray = new char[] { '|' };
							_frmMemoGlobal.strDelMemoDate = str1.TrimEnd(chrArray);
							string[] strArrays2 = this.objFrmMemoGlobal.strDelMemoDate.Split(new char[] { '|' });
							for (int i = 0; i < (int)strArrays1.Length; i++)
							{
								string empty = string.Empty;
								if (strArrays1[i].ToUpper().Trim() == "TEXT")
								{
									empty = "txt";
								}
								else if (strArrays1[i].ToUpper().Trim() == "REFERENCE")
								{
									empty = "ref";
								}
								else if (strArrays1[i].ToUpper().Trim() == "HYPERLINK")
								{
									empty = "hyp";
								}
								else if (strArrays1[i].ToUpper().Trim() == "PROGRAM")
								{
									empty = "prg";
								}
								XmlDocument xmlDocument1 = this.frmParent.xDocGlobalMemo;
								string[] strArrays3 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@PicIndex='", this.sPicIndex, "'][@ListIndex='", this.sListIndex, "'][@Type='", empty, "'][@Update='", strArrays2[i], "'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument1.SelectNodes(string.Concat(strArrays3));
							}
						}
					}
					else if (this.objFrmMemoGlobal.strMemoChangedType != "")
					{
						frmMemoGlobal _frmMemoGlobal1 = this.objFrmMemoGlobal;
						string str2 = this.objFrmMemoGlobal.strMemoChangedType;
						char[] chrArray1 = new char[] { '|' };
						_frmMemoGlobal1.strMemoChangedType = str2.TrimEnd(chrArray1);
						if (!this.objFrmMemoGlobal.strMemoChangedType.Contains("|"))
						{
							string empty1 = string.Empty;
							if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "TEXT")
							{
								empty1 = "txt";
							}
							else if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "REFERENCE")
							{
								empty1 = "ref";
							}
							else if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "HYPERLINK")
							{
								empty1 = "hyp";
							}
							else if (this.objFrmMemoGlobal.strMemoChangedType.ToUpper().Trim() == "PROGRAM")
							{
								empty1 = "prg";
							}
							if (this.objFrmMemoGlobal.strDelMemoDate.ToString() == string.Empty)
							{
								XmlDocument xmlDocument2 = this.frmParent.xDocGlobalMemo;
								str = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty1, "'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument2.SelectNodes(string.Concat(str));
							}
							else if (!this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || this.objFrmMemoGlobal.bMemoModifiedAtPartLevel)
							{
								XmlDocument xmlDocument3 = this.frmParent.xDocGlobalMemo;
								string[] str3 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@Type='", empty1, "'][@Update='", this.objFrmMemoGlobal.strDelMemoDate.ToString(), "'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument3.SelectNodes(string.Concat(str3));
							}
							else
							{
								XmlDocument xmlDocument4 = this.frmParent.xDocGlobalMemo;
								str = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty1, "'][@Update='", this.objFrmMemoGlobal.strDelMemoDate.ToString(), "'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument4.SelectNodes(string.Concat(str));
							}
						}
						else
						{
							string[] strArrays4 = this.objFrmMemoGlobal.strMemoChangedType.Split(new char[] { '|' });
							frmMemoGlobal _frmMemoGlobal2 = this.objFrmMemoGlobal;
							string str4 = this.objFrmMemoGlobal.strDelMemoDate;
							char[] chrArray2 = new char[] { '|' };
							_frmMemoGlobal2.strDelMemoDate = str4.TrimEnd(chrArray2);
							string[] strArrays5 = this.objFrmMemoGlobal.strDelMemoDate.Split(new char[] { '|' });
							for (int j = 0; j < (int)strArrays4.Length; j++)
							{
								string empty2 = string.Empty;
								if (strArrays4[j].ToUpper().Trim() == "TEXT")
								{
									empty2 = "txt";
								}
								else if (strArrays4[j].ToUpper().Trim() == "REFERENCE")
								{
									empty2 = "ref";
								}
								else if (strArrays4[j].ToUpper().Trim() == "HYPERLINK")
								{
									empty2 = "hyp";
								}
								else if (strArrays4[j].ToUpper().Trim() == "PROGRAM")
								{
									empty2 = "prg";
								}
								if (this.objFrmMemoGlobal.strDelMemoDate.ToString() == string.Empty)
								{
									XmlDocument xmlDocument5 = this.frmParent.xDocGlobalMemo;
									string[] strArrays6 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty2, "'][@PartNo='", this.sPartNumber, "']" };
									xmlNodeLists = xmlDocument5.SelectNodes(string.Concat(strArrays6));
								}
								else if (!this.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || this.objFrmMemoGlobal.bMemoModifiedAtPartLevel)
								{
									XmlDocument xmlDocument6 = this.frmParent.xDocGlobalMemo;
									string[] strArrays7 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@Type='", empty2, "'][@Update='", strArrays5[j], "'][@PartNo='", this.sPartNumber, "']" };
									xmlNodeLists = xmlDocument6.SelectNodes(string.Concat(strArrays7));
								}
								else
								{
									XmlDocument xmlDocument7 = this.frmParent.xDocGlobalMemo;
									string[] strArrays8 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty2, "'][@Update='", strArrays5[j], "'][@PartNo='", this.sPartNumber, "']" };
									xmlNodeLists = xmlDocument7.SelectNodes(string.Concat(strArrays8));
								}
								if (xmlNodeLists != null)
								{
									foreach (XmlNode xmlNodes in xmlNodeLists)
									{
										this.frmParent.xDocGlobalMemo.DocumentElement.RemoveChild(xmlNodes);
									}
									flag = true;
								}
							}
						}
					}
					if (!flag && xmlNodeLists != null)
					{
						foreach (XmlNode xmlNodes1 in xmlNodeLists)
						{
							this.frmParent.xDocGlobalMemo.DocumentElement.RemoveChild(xmlNodes1);
						}
					}
					this.objFrmMemoGlobal.bMemoModifiedAtPartLevel = false;
					this.objFrmMemoGlobal.strMemoChangedType = string.Empty;
					this.objFrmMemoGlobal.strDelMemoDate = string.Empty;
				}
				return;
			}
			if (this.objFrmMemoLocal.bMultiMemoChange)
			{
				if (this.objFrmMemoLocal.strMemoChangedType == "")
				{
					XmlDocument xmlDocument8 = this.frmParent.xDocLocalMemo;
					string[] strArrays9 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@PicIndex='", this.sPicIndex, "'][@ListIndex='", this.sListIndex, "'][@PartNo='", this.sPartNumber, "']" };
					xmlNodeLists = xmlDocument8.SelectNodes(string.Concat(strArrays9));
				}
				else
				{
					string[] strArrays10 = this.objFrmMemoLocal.strMemoChangedType.Split(new char[] { '|' });
					frmMemoLocal _frmMemoLocal = this.objFrmMemoLocal;
					string str5 = this.objFrmMemoLocal.strDelMemoDate;
					char[] chrArray3 = new char[] { '|' };
					_frmMemoLocal.strDelMemoDate = str5.TrimEnd(chrArray3);
					string[] strArrays11 = this.objFrmMemoLocal.strDelMemoDate.Split(new char[] { '|' });
					for (int k = 0; k < (int)strArrays10.Length; k++)
					{
						string empty3 = string.Empty;
						if (strArrays10[k].ToUpper().Trim() == "TEXT")
						{
							empty3 = "txt";
						}
						else if (strArrays10[k].ToUpper().Trim() == "REFERENCE")
						{
							empty3 = "ref";
						}
						else if (strArrays10[k].ToUpper().Trim() == "HYPERLINK")
						{
							empty3 = "hyp";
						}
						else if (strArrays10[k].ToUpper().Trim() == "PROGRAM")
						{
							empty3 = "prg";
						}
						XmlDocument xmlDocument9 = this.frmParent.xDocLocalMemo;
						str = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@PicIndex='", this.sPicIndex, "'][@ListIndex='", this.sListIndex, "'][@Type='", empty3, "'][@Update='", strArrays11[k], "'][@PartNo='", this.sPartNumber, "']" };
						xmlNodeLists = xmlDocument9.SelectNodes(string.Concat(str));
					}
				}
				if (xmlNodeLists != null && xmlNodeLists.Count > 0)
				{
					foreach (XmlNode xmlNodes2 in xmlNodeLists)
					{
						this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(xmlNodes2);
						flag = true;
					}
				}
			}
			else if (this.objFrmMemoLocal.strMemoChangedType != "")
			{
				frmMemoLocal _frmMemoLocal1 = this.objFrmMemoLocal;
				string str6 = this.objFrmMemoLocal.strMemoChangedType;
				char[] chrArray4 = new char[] { '|' };
				_frmMemoLocal1.strMemoChangedType = str6.TrimEnd(chrArray4);
				if (!this.objFrmMemoLocal.strMemoChangedType.Contains("|"))
				{
					string empty4 = string.Empty;
					if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "TEXT")
					{
						empty4 = "txt";
					}
					else if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "REFERENCE")
					{
						empty4 = "ref";
					}
					else if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "HYPERLINK")
					{
						empty4 = "hyp";
					}
					else if (this.objFrmMemoLocal.strMemoChangedType.ToUpper().Trim() == "PROGRAM")
					{
						empty4 = "prg";
					}
					if (this.objFrmMemoLocal.strDelMemoDate.ToString() == string.Empty)
					{
						XmlDocument xmlDocument10 = this.frmParent.xDocLocalMemo;
						string[] strArrays12 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty4, "'][@PartNo='", this.sPartNumber, "']" };
						xmlNodeLists = xmlDocument10.SelectNodes(string.Concat(strArrays12));
					}
					else if (!this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || this.objFrmMemoLocal.bMemoModifiedAtPartLevel)
					{
						XmlDocument xmlDocument11 = this.frmParent.xDocLocalMemo;
						string[] str7 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@Type='", empty4, "'][@Update='", this.objFrmMemoLocal.strDelMemoDate.ToString(), "'][@PartNo='", this.sPartNumber, "']" };
						xmlNodeLists = xmlDocument11.SelectNodes(string.Concat(str7));
					}
					else
					{
						XmlDocument xmlDocument12 = this.frmParent.xDocLocalMemo;
						string[] str8 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty4, "'][@Update='", this.objFrmMemoLocal.strDelMemoDate.ToString(), "'][@PartNo='", this.sPartNumber, "']" };
						xmlNodeLists = xmlDocument12.SelectNodes(string.Concat(str8));
					}
				}
				else
				{
					string[] strArrays13 = this.objFrmMemoLocal.strMemoChangedType.Split(new char[] { '|' });
					frmMemoLocal _frmMemoLocal2 = this.objFrmMemoLocal;
					string str9 = this.objFrmMemoLocal.strDelMemoDate;
					char[] chrArray5 = new char[] { '|' };
					_frmMemoLocal2.strDelMemoDate = str9.TrimEnd(chrArray5);
					string[] strArrays14 = this.objFrmMemoLocal.strDelMemoDate.Split(new char[] { '|' });
					for (int l = 0; l < (int)strArrays13.Length; l++)
					{
						string empty5 = string.Empty;
						if (strArrays13[l].ToUpper().Trim() == "TEXT")
						{
							empty5 = "txt";
						}
						else if (strArrays13[l].ToUpper().Trim() == "REFERENCE")
						{
							empty5 = "ref";
						}
						else if (strArrays13[l].ToUpper().Trim() == "HYPERLINK")
						{
							empty5 = "hyp";
						}
						else if (strArrays13[l].ToUpper().Trim() == "PROGRAM")
						{
							empty5 = "prg";
						}
						if (this.objFrmMemoLocal.strDelMemoDate.ToString() == string.Empty)
						{
							XmlDocument xmlDocument13 = this.frmParent.xDocLocalMemo;
							string[] strArrays15 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty5, "'][@PartNo='", this.sPartNumber, "']" };
							xmlNodeLists = xmlDocument13.SelectNodes(string.Concat(strArrays15));
						}
						else if (!this.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || this.objFrmMemoLocal.bMemoModifiedAtPartLevel)
						{
							XmlDocument xmlDocument14 = this.frmParent.xDocLocalMemo;
							string[] strArrays16 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@PageId='", this.sPageId, "'][@Type='", empty5, "'][@Update='", strArrays14[l], "'][@PartNo='", this.sPartNumber, "']" };
							xmlNodeLists = xmlDocument14.SelectNodes(string.Concat(strArrays16));
						}
						else
						{
							XmlDocument xmlDocument15 = this.frmParent.xDocLocalMemo;
							string[] strArrays17 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='", empty5, "'][@Update='", strArrays14[l], "'][@PartNo='", this.sPartNumber, "']" };
							xmlNodeLists = xmlDocument15.SelectNodes(string.Concat(strArrays17));
						}
						if (xmlNodeLists != null)
						{
							foreach (XmlNode xmlNodes3 in xmlNodeLists)
							{
								this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(xmlNodes3);
							}
							flag = true;
						}
					}
				}
			}
			if (!flag && xmlNodeLists != null)
			{
				foreach (XmlNode xmlNodes4 in xmlNodeLists)
				{
					this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(xmlNodes4);
				}
			}
			this.objFrmMemoLocal.bMemoModifiedAtPartLevel = false;
			this.objFrmMemoLocal.strMemoChangedType = string.Empty;
			this.objFrmMemoLocal.strDelMemoDate = string.Empty;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private string[] FilterMemo(string[] arrMemoNodes, string strMemoType)
		{
			string[] strArrays;
			List<string> strs = new List<string>();
			List<int> nums = new List<int>();
			try
			{
				if (arrMemoNodes == null)
				{
					strArrays = null;
				}
				else
				{
					if (strMemoType != "LOCAL")
					{
						for (int i = 0; i < (int)arrMemoNodes.Length; i++)
						{
							XmlNodeList xmlNodeLists = null;
							XmlDocument xmlDocument = new XmlDocument();
							XmlNode xmlNodes = null;
							XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(arrMemoNodes[i].ToString()));
							xmlNodes = xmlDocument.ReadNode(xmlTextReader);
							string upper = xmlNodes.Attributes["Type"].Value.ToString().ToUpper();
							if (upper == "TXT")
							{
								XmlDocument xmlDocument1 = this.frmParent.xDocGlobalMemo;
								string[] strArrays1 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='txt'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument1.SelectNodes(string.Concat(strArrays1));
							}
							else if (upper == "REF")
							{
								XmlDocument xmlDocument2 = this.frmParent.xDocGlobalMemo;
								string[] strArrays2 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='ref'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument2.SelectNodes(string.Concat(strArrays2));
							}
							else if (upper == "HYP")
							{
								XmlDocument xmlDocument3 = this.frmParent.xDocGlobalMemo;
								string[] strArrays3 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='hyp'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument3.SelectNodes(string.Concat(strArrays3));
							}
							else if (upper == "PRG")
							{
								XmlDocument xmlDocument4 = this.frmParent.xDocGlobalMemo;
								string[] strArrays4 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='prg'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists = xmlDocument4.SelectNodes(string.Concat(strArrays4));
							}
							if (xmlNodeLists == null || xmlNodeLists.Count <= 0)
							{
								strs.Add(xmlNodes.OuterXml);
							}
							else
							{
								bool flag = false;
								foreach (XmlNode xmlNodes1 in xmlNodeLists)
								{
									if (!(xmlNodes1.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["ServerKey"].Value.ToString().Trim().ToUpper()) || !(xmlNodes1.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["BookId"].Value.ToString().Trim().ToUpper()) || !(xmlNodes1.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["PartNo"].Value.ToString().Trim().ToUpper()) || !(xmlNodes1.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["Type"].Value.ToString().Trim().ToUpper()) || !(xmlNodes1.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["Value"].Value.ToString().Trim().ToUpper()) || !(xmlNodes1.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNodes.Attributes["Update"].Value.ToString().Trim().ToUpper()))
									{
										continue;
									}
									nums.Add(i);
									flag = true;
									break;
								}
								if (!flag)
								{
									strs.Add(xmlNodes.OuterXml);
								}
							}
						}
					}
					else
					{
						for (int j = 0; j < (int)arrMemoNodes.Length; j++)
						{
							XmlNodeList xmlNodeLists1 = null;
							XmlDocument xmlDocument5 = new XmlDocument();
							XmlNode xmlNodes2 = null;
							XmlTextReader xmlTextReader1 = new XmlTextReader(new StringReader(arrMemoNodes[j].ToString()));
							xmlNodes2 = xmlDocument5.ReadNode(xmlTextReader1);
							string str = xmlNodes2.Attributes["Type"].Value.ToString().ToUpper();
							if (str == "TXT")
							{
								XmlDocument xmlDocument6 = this.frmParent.xDocLocalMemo;
								string[] strArrays5 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='txt'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists1 = xmlDocument6.SelectNodes(string.Concat(strArrays5));
							}
							else if (str == "REF")
							{
								XmlDocument xmlDocument7 = this.frmParent.xDocLocalMemo;
								string[] strArrays6 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='ref'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists1 = xmlDocument7.SelectNodes(string.Concat(strArrays6));
							}
							else if (str == "HYP")
							{
								XmlDocument xmlDocument8 = this.frmParent.xDocLocalMemo;
								string[] strArrays7 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='hyp'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists1 = xmlDocument8.SelectNodes(string.Concat(strArrays7));
							}
							else if (str == "PRG")
							{
								XmlDocument xmlDocument9 = this.frmParent.xDocLocalMemo;
								string[] strArrays8 = new string[] { "//Memos/Memo[@ServerKey='", this.sServerKey, "'][@BookId='", this.sBookId, "'][@Type='prg'][@PartNo='", this.sPartNumber, "']" };
								xmlNodeLists1 = xmlDocument9.SelectNodes(string.Concat(strArrays8));
							}
							if (xmlNodeLists1 == null || xmlNodeLists1.Count <= 0)
							{
								strs.Add(xmlNodes2.OuterXml);
							}
							else
							{
								bool flag1 = false;
								foreach (XmlNode xmlNodes3 in xmlNodeLists1)
								{
									if (!(xmlNodes3.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["ServerKey"].Value.ToString().Trim().ToUpper()) || !(xmlNodes3.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["BookId"].Value.ToString().Trim().ToUpper()) || !(xmlNodes3.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["PartNo"].Value.ToString().Trim().ToUpper()) || !(xmlNodes3.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["Type"].Value.ToString().Trim().ToUpper()) || !(xmlNodes3.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["Value"].Value.ToString().Trim().ToUpper()) || !(xmlNodes3.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["Update"].Value.ToString().Trim().ToUpper()))
									{
										continue;
									}
									nums.Add(j);
									flag1 = true;
									break;
								}
								if (!flag1)
								{
									strs.Add(xmlNodes2.OuterXml);
								}
							}
						}
					}
					string[] str1 = new string[strs.Count];
					for (int k = 0; k < strs.Count; k++)
					{
						str1[k] = strs[k].ToString();
					}
					strArrays = str1;
				}
			}
			catch (Exception exception)
			{
				strArrays = null;
			}
			return strArrays;
		}

		private void frmMemo_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				if (!this.sPartNumber.Trim().Equals(string.Empty))
				{
					this.frmParent.objFrmPartlist.UpdateMemoIconOnSelectedRow();
				}
				if (!this.frmParent.Enabled)
				{
					this.frmParent.Enabled = true;
				}
				if (!this.objFrmMemoLocal.IsDisposed)
				{
					this.objFrmMemoLocal.Close();
				}
				if (!this.objFrmMemoGlobal.IsDisposed)
				{
					this.objFrmMemoGlobal.Close();
				}
				if (!this.objFrmMemoAdmin.IsDisposed)
				{
					this.objFrmMemoAdmin.Close();
				}
				if (!this.objFrmTasks.IsDisposed)
				{
					this.objFrmTasks.Close();
				}
				this.SaveUserSettings();
				this.frmParent.HideDimmer();
				if (this.sPartNumber.Trim().Equals(string.Empty))
				{
					if (!this.frmParent.objFrmTreeview.IsDisposed)
					{
						this.frmParent.objFrmTreeview.Focus();
					}
				}
				else if (!this.frmParent.objFrmPartlist.IsDisposed)
				{
					this.frmParent.objFrmPartlist.Focus();
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void frmMemo_Load(object sender, EventArgs e)
		{
			this.objFrmTasks.Show();
			this.LoadUserSettings();
			if (this.bNoMemos)
			{
				base.Close();
			}
			if (this.intMemoType != 2)
			{
				if (this.xnlLocalMemo != null && this.xnlLocalMemo.Count > 0 && (this.sMemoScope == "LOC" || this.sMemoScope == string.Empty))
				{
					this.objFrmTasks.ShowTask(MemoTasks.Local);
					return;
				}
				if (this.xnlGlobalMemo != null && this.xnlGlobalMemo.Count > 0 && (this.sMemoScope == "GBL" || this.sMemoScope == string.Empty))
				{
					this.objFrmTasks.ShowTask(MemoTasks.Global);
					return;
				}
				if (this.xnladminMemo != null && this.xnladminMemo.Count > 0 && (this.sMemoScope == "ADM" || this.sMemoScope == string.Empty))
				{
					this.objFrmTasks.ShowTask(MemoTasks.Admin);
					return;
				}
				if (Settings.Default.EnableLocalMemo)
				{
					this.objFrmTasks.ShowTask(MemoTasks.Local);
					return;
				}
				if (Settings.Default.EnableGlobalMemo)
				{
					this.objFrmTasks.ShowTask(MemoTasks.Global);
					return;
				}
				if (Settings.Default.EnableAdminMemo)
				{
					this.objFrmTasks.ShowTask(MemoTasks.Admin);
				}
			}
		}

		private int GetMemoType()
		{
			int num;
			try
			{
				num = (Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"] == null || !(Program.iniGSPcLocal.items["SETTINGS", "MEMO_TYPE"].ToString() == "2") ? 1 : 2);
			}
			catch (Exception exception)
			{
				num = 1;
			}
			return num;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MEMO']");
				if (rType != ResourceType.TITLE)
				{
					if (rType == ResourceType.LABEL)
					{
						str = string.Concat(str, "/Resources[@Name='LABEL']");
					}
					else if (rType == ResourceType.BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='BUTTON']");
					}
					else if (rType == ResourceType.CHECK_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='CHECK_BOX']");
					}
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
					}
					else if (rType == ResourceType.COMBO_BOX)
					{
						str = string.Concat(str, "/Resources[@Name='COMBO_BOX']");
					}
					else if (rType == ResourceType.GRID_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='GRID_VIEW']");
					}
					else if (rType == ResourceType.LIST_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='LIST_VIEW']");
					}
					else if (rType == ResourceType.MENU_BAR)
					{
						str = string.Concat(str, "/Resources[@Name='MENU_BAR']");
					}
					else if (rType == ResourceType.RADIO_BUTTON)
					{
						str = string.Concat(str, "/Resources[@Name='RADIO_BUTTON']");
					}
					else if (rType == ResourceType.CONTEXT_MENU)
					{
						str = string.Concat(str, "/Resources[@Name='CONTEXT_MENU']");
					}
					else if (rType == ResourceType.TOOLSTRIP)
					{
						str = string.Concat(str, "/Resources[@Name='TOOLSTRIP']");
					}
					str = string.Concat(str, "/Resource[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		public void HideForms()
		{
			foreach (Control control in this.splitPnlMemo.Panel2.Controls)
			{
				if (control == null)
				{
					continue;
				}
				control.Hide();
			}
		}

		private void InitializeComponent()
		{
			this.toolStripContainer1 = new ToolStripContainer();
			this.splitPnlMemo = new SplitContainer();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.splitPnlMemo.SuspendLayout();
			base.SuspendLayout();
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitPnlMemo);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(694, 333);
			this.toolStripContainer1.Dock = DockStyle.Fill;
			this.toolStripContainer1.Location = new Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(694, 358);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.splitPnlMemo.Dock = DockStyle.Fill;
			this.splitPnlMemo.Location = new Point(0, 0);
			this.splitPnlMemo.MinimumSize = new System.Drawing.Size(690, 331);
			this.splitPnlMemo.Name = "splitPnlMemo";
			this.splitPnlMemo.Panel1.AccessibleName = "pnlTasks";
			this.splitPnlMemo.Panel1MinSize = 160;
			this.splitPnlMemo.Panel2.AccessibleName = "pnlContents";
			this.splitPnlMemo.Panel2MinSize = 525;
			this.splitPnlMemo.Size = new System.Drawing.Size(694, 333);
			this.splitPnlMemo.SplitterDistance = 162;
			this.splitPnlMemo.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(694, 358);
			base.Controls.Add(this.toolStripContainer1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.IsMdiContainer = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(710, 396);
			base.Name = "frmMemo";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Memo";
			base.Load += new EventHandler(this.frmMemo_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmMemo_FormClosing);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitPnlMemo.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void LoadUserSettings()
		{
			if (Settings.Default.frmMemoLocation.X != -1 || Settings.Default.frmMemoListLocation.Y != -1)
			{
				base.Location = Settings.Default.frmMemoLocation;
			}
			else
			{
				base.CenterToParent();
			}
			base.Size = Settings.Default.frmMemoSize;
			if (Settings.Default.frmMemoState == FormWindowState.Minimized)
			{
				base.WindowState = FormWindowState.Normal;
				return;
			}
			base.WindowState = Settings.Default.frmMemoState;
		}

		public void OpenBookFromString(string reference)
		{
			this.frmParent.OpenBookFromString(reference);
		}

		private void SaveUserSettings()
		{
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmMemoLocation = base.RestoreBounds.Location;
			}
			else
			{
				Settings.Default.frmMemoLocation = base.Location;
			}
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmMemoSize = base.RestoreBounds.Size;
			}
			else
			{
				Settings.Default.frmMemoSize = base.Size;
			}
			Settings.Default.frmMemoState = base.WindowState;
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}
	}
}
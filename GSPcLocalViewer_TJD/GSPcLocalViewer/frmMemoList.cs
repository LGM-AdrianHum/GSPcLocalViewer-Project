using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmMemoList : Form
	{
		private IContainer components;

		private ToolStripContainer toolStripContainer1;

		private SplitContainer splitPnlMemoList;

		public frmMemoListTasks objFrmTasks;

		public frmMemoListLocal objFrmMemoLocal;

		public frmMemoListGlobal objFrmMemoGlobal;

		public frmMemoListAdmin objFrmMemoAdmin;

		public frmMemoListAll objFrmMemoAll;

		public XmlNodeList xnlLocalMemo;

		public XmlNodeList xnlGlobalMemo;

		public XmlNodeList xnlAdminMemo;

		public string sServerKey;

		public string sBookId;

		public string sPageId;

		public string sPicIndex;

		public string sListIndex;

		public string sPartNumber;

		public frmViewer frmParent;

		public frmMemoList(frmViewer frm, XmlNodeList xLocalMemoList, XmlNodeList xGlobalMemoList, XmlNodeList xAdminMemoList, string sPageId, string sPicIndex, string sListIndex, string sPartNumber)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.CreateForms();
			this.UpdateFont();
			this.LoadResources();
			this.xnlLocalMemo = xLocalMemoList;
			this.xnlGlobalMemo = xGlobalMemoList;
			this.xnlAdminMemo = xAdminMemoList;
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
			XmlNode xmlNodes;
			if (sNodesArr != null && (int)sNodesArr.Length > 0)
			{
				if (memoType.ToUpper().Equals("LOCAL"))
				{
					for (int i = 0; i < (int)sNodesArr.Length; i++)
					{
						try
						{
							xmlTextReader = new XmlTextReader(new StringReader(sNodesArr[i]));
							xmlNodes = this.frmParent.xDocLocalMemo.ReadNode(xmlTextReader);
						}
						catch
						{
							xmlNodes = null;
						}
						if (xmlNodes != null)
						{
							this.frmParent.xDocLocalMemo.DocumentElement.AppendChild(xmlNodes);
						}
					}
					return;
				}
				if (memoType.ToUpper().Equals("GLOBAL"))
				{
					for (int j = 0; j < (int)sNodesArr.Length; j++)
					{
						try
						{
							xmlTextReader = new XmlTextReader(new StringReader(sNodesArr[j]));
							xmlNodes = this.frmParent.xDocGlobalMemo.ReadNode(xmlTextReader);
						}
						catch
						{
							xmlNodes = null;
						}
						if (xmlNodes != null)
						{
							this.frmParent.xDocGlobalMemo.DocumentElement.AppendChild(xmlNodes);
						}
					}
				}
			}
		}

		public void CloseAndSaveSettings()
		{
			if (Settings.Default.EnableLocalMemo && this.objFrmMemoLocal.isMemoChanged)
			{
				this.DeletePrevMemos("local");
				this.AddNewMemos("local", this.objFrmMemoLocal.getMemos);
			}
			if (Settings.Default.EnableGlobalMemo && this.objFrmMemoGlobal.isMemoChanged)
			{
				this.DeletePrevMemos("global");
				this.AddNewMemos("global", this.objFrmMemoGlobal.getMemos);
			}
			if (this.objFrmMemoAll.isMemoChanged)
			{
				if (Settings.Default.EnableGlobalMemo)
				{
					this.DeletePrevMemos("global");
					this.AddNewMemos("global", this.objFrmMemoAll.getGlobalMemos);
				}
				if (Settings.Default.EnableLocalMemo)
				{
					this.DeletePrevMemos("local");
					this.AddNewMemos("local", this.objFrmMemoAll.getLocalMemos);
				}
			}
			base.Close();
		}

		private void CreateForms()
		{
			this.objFrmMemoLocal = new frmMemoListLocal(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemoList.Panel2.Controls.Add(this.objFrmMemoLocal);
			this.objFrmMemoGlobal = new frmMemoListGlobal(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemoList.Panel2.Controls.Add(this.objFrmMemoGlobal);
			this.objFrmMemoAdmin = new frmMemoListAdmin(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemoList.Panel2.Controls.Add(this.objFrmMemoAdmin);
			this.objFrmMemoAll = new frmMemoListAll(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemoList.Panel2.Controls.Add(this.objFrmMemoAll);
			this.objFrmTasks = new frmMemoListTasks(this)
			{
				TopLevel = false,
				Dock = DockStyle.Fill
			};
			this.splitPnlMemoList.Panel1.Controls.Add(this.objFrmTasks);
		}

		private void DeletePrevMemos(string memoType)
		{
			if (memoType.ToUpper().Equals("LOCAL"))
			{
				foreach (XmlNode xmlNodes in this.frmParent.xDocLocalMemo.SelectNodes("//Memos/Memo"))
				{
					this.frmParent.xDocLocalMemo.DocumentElement.RemoveChild(xmlNodes);
				}
			}
			else if (memoType.ToUpper().Equals("GLOBAL"))
			{
				foreach (XmlNode xmlNodes1 in this.frmParent.xDocGlobalMemo.SelectNodes("//Memos/Memo"))
				{
					this.frmParent.xDocGlobalMemo.DocumentElement.RemoveChild(xmlNodes1);
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmMemo_FormClosing(object sender, FormClosingEventArgs e)
		{
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
			if (!this.objFrmMemoAll.IsDisposed)
			{
				this.objFrmMemoAll.Close();
			}
			if (!this.objFrmMemoAdmin.IsDisposed)
			{
				this.objFrmMemoAdmin.Close();
			}
			if (!this.objFrmTasks.IsDisposed)
			{
				this.objFrmTasks.Close();
			}
			try
			{
				this.SaveUserSettings();
			}
			catch
			{
			}
			this.frmParent.HideDimmer();
		}

		private void frmMemo_Load(object sender, EventArgs e)
		{
			this.LoadUserSettings();
			this.objFrmTasks.Show();
			this.objFrmTasks.ShowTask(ViewAllMemoTasks.All);
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MEMO_LIST']");
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
			foreach (Control control in this.splitPnlMemoList.Panel2.Controls)
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
			this.splitPnlMemoList = new SplitContainer();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.splitPnlMemoList.SuspendLayout();
			base.SuspendLayout();
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitPnlMemoList);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(694, 342);
			this.toolStripContainer1.Dock = DockStyle.Fill;
			this.toolStripContainer1.Location = new Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(694, 367);
			this.toolStripContainer1.TabIndex = 4;
			this.toolStripContainer1.Text = "toolStripContainer1";
			this.splitPnlMemoList.Dock = DockStyle.Fill;
			this.splitPnlMemoList.Location = new Point(0, 0);
			this.splitPnlMemoList.MinimumSize = new System.Drawing.Size(684, 342);
			this.splitPnlMemoList.Name = "splitPnlMemoList";
			this.splitPnlMemoList.Panel1.AccessibleName = "pnlTasks";
			this.splitPnlMemoList.Panel1MinSize = 160;
			this.splitPnlMemoList.Panel2.AccessibleName = "pnlContents";
			this.splitPnlMemoList.Panel2MinSize = 530;
			this.splitPnlMemoList.Size = new System.Drawing.Size(694, 342);
			this.splitPnlMemoList.SplitterDistance = 160;
			this.splitPnlMemoList.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(694, 367);
			base.Controls.Add(this.toolStripContainer1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.IsMdiContainer = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(710, 405);
			base.Name = "frmMemoList";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Memo List";
			base.Load += new EventHandler(this.frmMemo_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmMemo_FormClosing);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitPnlMemoList.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Memo List", "MEMO_LIST", ResourceType.TITLE);
		}

		private void LoadUserSettings()
		{
			if (Settings.Default.frmMemoListLocation.X != -1 || Settings.Default.frmMemoListLocation.Y != -1)
			{
				base.Location = Settings.Default.frmMemoListLocation;
			}
			else
			{
				base.CenterToParent();
			}
			base.Size = Settings.Default.frmMemoListSize;
			if (Settings.Default.frmMemoListState == FormWindowState.Minimized)
			{
				base.WindowState = FormWindowState.Normal;
				return;
			}
			base.WindowState = Settings.Default.frmMemoListState;
		}

		public void OpenBookFromString(string reference)
		{
			this.frmParent.OpenBookFromString(reference);
		}

		public void OpenParentPage(string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex, string sPartNumber)
		{
			try
			{
				this.frmParent.OpenParentPage(sServerKey, sBookPubId, sPageId, sImageIndex, sListIndex, sPartNumber);
			}
			catch
			{
			}
		}

		private XmlNodeList RemoveDuplicateMemoNodes(XmlNodeList xnlMemoNodes)
		{
			XmlNodeList xmlNodeLists;
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNodes = xmlDocument.CreateElement("Memos");
			XmlNodeList xmlNodeLists1 = xmlDocument.SelectNodes("//Memos/Memo");
			try
			{
				foreach (XmlNode xnlMemoNode in xnlMemoNodes)
				{
					if (xnlMemoNode.Attributes.Count <= 0 || xnlMemoNode.Attributes["PartNo"] == null || !(xnlMemoNode.Attributes["PartNo"].Value.ToString() != ""))
					{
						continue;
					}
					if (xmlNodeLists1.Count <= 0)
					{
						XmlNode xmlNodes1 = xmlDocument.CreateElement("Memo");
						for (int i = 0; i < xnlMemoNode.Attributes.Count; i++)
						{
							XmlAttribute value = xmlDocument.CreateAttribute(xnlMemoNode.Attributes[i].Name);
							value.Value = xnlMemoNode.Attributes[i].Value;
							xmlNodes1.Attributes.Append(value);
						}
						xmlNodes.AppendChild(xmlNodes1);
						xmlDocument.AppendChild(xmlNodes);
						xmlNodeLists1 = xmlDocument.SelectNodes("//Memos/Memo");
					}
					else
					{
						foreach (XmlNode xmlNodes2 in xmlNodeLists1)
						{
							if (xmlNodes2.Attributes.Count <= 0 || xmlNodes2.Attributes["PartNo"] == null || !(xmlNodes2.Attributes["PartNo"].Value.ToString() != ""))
							{
								continue;
							}
							if (xnlMemoNode.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["ServerKey"].Value.ToString().Trim().ToUpper() && xnlMemoNode.Attributes["BookId"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["BookId"].Value.ToString().Trim().ToUpper() && xnlMemoNode.Attributes["PartNo"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["PartNo"].Value.ToString().Trim().ToUpper() && xnlMemoNode.Attributes["Type"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["Type"].Value.ToString().Trim().ToUpper() && xnlMemoNode.Attributes["Value"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["Value"].Value.ToString().Trim().ToUpper() && xnlMemoNode.Attributes["Update"].Value.ToString().Trim().ToUpper() == xmlNodes2.Attributes["Update"].Value.ToString().Trim().ToUpper())
							{
								break;
							}
							XmlNode xmlNodes3 = xmlDocument.CreateElement("Memo");
							for (int j = 0; j < xnlMemoNode.Attributes.Count; j++)
							{
								XmlAttribute xmlAttribute = xmlDocument.CreateAttribute(xnlMemoNode.Attributes[j].Name);
								xmlAttribute.Value = xnlMemoNode.Attributes[j].Value;
								xmlNodes3.Attributes.Append(xmlAttribute);
							}
							xmlNodes.AppendChild(xmlNodes3);
							xmlDocument.AppendChild(xmlNodes);
							xmlNodeLists1 = xmlDocument.SelectNodes("//Memos/Memo");
						}
					}
				}
				xmlNodeLists = xmlNodeLists1;
			}
			catch (Exception exception)
			{
				xmlNodeLists = xmlNodeLists1;
			}
			return xmlNodeLists;
		}

		private void SaveUserSettings()
		{
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmMemoListLocation = base.RestoreBounds.Location;
			}
			else
			{
				Settings.Default.frmMemoListLocation = base.Location;
			}
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmMemoListSize = base.RestoreBounds.Size;
			}
			else
			{
				Settings.Default.frmMemoListSize = base.Size;
			}
			Settings.Default.frmMemoListState = base.WindowState;
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}
	}
}
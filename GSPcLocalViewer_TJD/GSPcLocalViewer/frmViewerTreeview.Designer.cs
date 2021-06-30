using GSPcLocalViewer.frmPrint;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
	public class frmViewerTreeview : DockContent
	{
		private const string dllZipper = "ZIPPER.dll";

		private IContainer components;

		private Panel pnlForm;

		public CustomTreeView tvBook;

		private PictureBox picLoading;

		private BackgroundWorker bgWorker;

		private System.Windows.Forms.ContextMenuStrip cMenuStripPrintRange;

		private ToolStripMenuItem FromMenu;

		private ToolStripMenuItem toMenu;

		private System.Windows.Forms.ContextMenuStrip cmsTreeview;

		private ToolStripMenuItem copyToClipboardToolStripMenuItem;

		private ToolStripMenuItem exportToFileToolStripMenuItem;

		private SaveFileDialog dlgSaveFile;

		private ToolStripMenuItem commaSeparatedToolStripMenuItem;

		private ToolStripMenuItem tabSeparatedToolStripMenuItem;

		private ToolStripMenuItem commaSeparatedToolStripMenuItem1;

		private ToolStripMenuItem tabSeparatedToolStripMenuItem1;

		private ToolStripMenuItem tsmiPrintThisContent;

		private frmViewer frmParent;

		private string statusText;

		private TreeNode lastTreeNode;

		public static string ToSelectedNode;

		private string selectedNodeText;

		private string selectedNodeTag;

		public string rangePrintAttId;

		public static string FromSelectedNode;

		private bool p_Encrypted;

		private bool p_Compressed;

		private XmlNode objXmlSchemaNode;

		private Download objDownloader;

		public bool isPDF;

		public bool isDJVU;

		public bool isTiff;

		private bool bMuliRageKey;

		private bool bMultiRange;

		public string sDataType = string.Empty;

		public string CurrentNodeText
		{
			get
			{
				string empty;
				try
				{
					empty = this.tvBook.SelectedNode.Text.Trim();
				}
				catch
				{
					empty = string.Empty;
				}
				return empty;
			}
		}

		public CustomTreeView CurrentTreeView
		{
			get
			{
				return this.tvBook;
			}
		}

		public string From
		{
			get
			{
				return frmViewerTreeview.FromSelectedNode;
			}
			set
			{
				frmViewerTreeview.FromSelectedNode = value;
			}
		}

		public XmlNode PageNode
		{
			get
			{
				XmlNode firstChild;
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.tvBook.SelectedNode.Tag.ToString()));
					firstChild = xmlDocument.ReadNode(xmlTextReader).FirstChild;
				}
				catch
				{
					firstChild = null;
				}
				return firstChild;
			}
		}

		public XmlNode PageSchemaNode
		{
			get
			{
				XmlNode xmlNodes;
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.tvBook.Tag.ToString()));
					xmlNodes = xmlDocument.ReadNode(xmlTextReader);
				}
				catch
				{
					xmlNodes = null;
				}
				return xmlNodes;
			}
		}

		public XmlNode PicNode
		{
			get
			{
				XmlNode xmlNodes;
				XmlDocument xmlDocument = new XmlDocument();
				try
				{
					string empty = string.Empty;
					foreach (XmlAttribute attribute in this.PageSchemaNode.Attributes)
					{
						if (attribute.Value.ToUpper() != "PICTUREFILE")
						{
							continue;
						}
						empty = attribute.Name;
						break;
					}
					XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.tvBook.SelectedNode.Tag.ToString()));
					XmlNode xmlNodes1 = xmlDocument.ReadNode(xmlTextReader);
					string currentPicName = this.frmParent.objFrmPicture.CurrentPicName;
					string[] strArrays = new string[] { "//Pic[@", empty, "='", currentPicName, "']" };
					xmlNodes = xmlNodes1.SelectSingleNode(string.Concat(strArrays));
				}
				catch
				{
					xmlNodes = null;
				}
				return xmlNodes;
			}
		}

		public string To
		{
			get
			{
				return frmViewerTreeview.ToSelectedNode;
			}
			set
			{
				frmViewerTreeview.ToSelectedNode = value;
			}
		}

		public frmViewerTreeview(frmViewer frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.lastTreeNode = null;
			this.statusText = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
			this.objDownloader = new Download(this.frmParent);
			this.UpdateFont();
			this.LoadResources();
			this.isPDF = false;
			this.isDJVU = false;
			this.isTiff = false;
		}

		private void AddNode(TreeNode tNode, XmlNode xNodeSchama, XmlNode xNode, string sDisplayElement, string sIdElement, string PicType)
		{
			string value = xNode.Attributes[sDisplayElement].Value;
			xNode = this.frmParent.FilterPage(xNodeSchama, xNode);
			if (xNode.Attributes.Count == 0)
			{
				this.frmParent.lstFilteredPages.Add(value);
				return;
			}
			TreeNode treeNode = new TreeNode()
			{
				Text = xNode.Attributes[sDisplayElement].Value.Replace("&", "&&")
			};
			xNode.SelectSingleNode("//Pic");
			if (xNode.OuterXml.ToUpper().IndexOf("<PG", 3) <= 0)
			{
				treeNode.Tag = xNode.OuterXml;
			}
			else
			{
				treeNode.Tag = string.Concat(xNode.OuterXml.Substring(0, xNode.OuterXml.IndexOf("<Pg", 3)), "</Pg>");
			}
			this.TreeViewAddNode(tNode, treeNode);
			tNode = treeNode;
			if (xNode.HasChildNodes)
			{
				foreach (XmlNode childNode in xNode.ChildNodes)
				{
					if (!childNode.Name.ToUpper().Equals("PG"))
					{
						continue;
					}
					XmlAttributeCollection attributes = xNode.Attributes;
					this.AddNode(tNode, xNodeSchama, childNode, sDisplayElement, sIdElement, PicType);
				}
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.frmParent.bObjFrmTreeviewClosed = false;
			string empty = string.Empty;
			string item = string.Empty;
			try
			{
				empty = Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "CONTENT_PATH"];
				if (!empty.EndsWith("/"))
				{
					empty = string.Concat(empty, "/");
				}
				item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
				item = string.Concat(item, "\\", Program.iniServers[this.frmParent.ServerId].sIniKey);
				item = string.Concat(item, "\\", this.frmParent.BookPublishingId);
				if (!Directory.Exists(item))
				{
					Directory.CreateDirectory(item);
				}
				if (!this.p_Compressed)
				{
					string str = empty;
					string[] bookPublishingId = new string[] { str, this.frmParent.BookPublishingId, "/", this.frmParent.BookPublishingId, ".xml" };
					empty = string.Concat(bookPublishingId);
					item = string.Concat(item, "\\", this.frmParent.BookPublishingId, ".xml");
				}
				else
				{
					string str1 = empty;
					string[] strArrays = new string[] { str1, this.frmParent.BookPublishingId, "/", this.frmParent.BookPublishingId, ".zip" };
					empty = string.Concat(strArrays);
					item = string.Concat(item, "\\", this.frmParent.BookPublishingId, ".zip");
				}
			}
			catch
			{
				MessageHandler.ShowError(this.GetResource("(E-VTV-EM004) Failed to create file/folder specified", "(E-VTV-EM004)_FAILED", ResourceType.POPUP_MESSAGE));
			}
			this.TreeViewVisible(false);
			this.TreeViewClearNodes();
			int num = 0;
			if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
			{
				num = 0;
			}
			bool flag = false;
			if (!File.Exists(item))
			{
				flag = true;
			}
			else if (num == 0)
			{
				flag = true;
			}
			else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(item, this.p_Compressed, this.p_Encrypted), Global.GetServerUpdateDateFromXmlNode(this.frmParent.SchemaNode, this.frmParent.BookNode), num))
			{
				flag = true;
			}
			if (flag)
			{
				this.objDownloader.DownloadFile(empty, item);
			}
			if (File.Exists(item))
			{
				if (!this.frmParent.IsDisposed)
				{
					this.statusText = string.Concat("Loading ", this.frmParent.BookPublishingId, ".xml");
					this.UpdateStatus();
					if (!this.LoadBookInTree(item))
					{
						this.statusText = string.Concat(this.frmParent.BookPublishingId, this.GetResource("(E-VTV-EM005) Failed to load specified object", "(E-VTV-EM005)_FAILED", ResourceType.STATUS_MESSAGE));
						this.UpdateStatus();
						return;
					}
					this.statusText = string.Concat(this.frmParent.BookPublishingId, " ", this.GetResource("Finished loading", "FINISHED_LOADING", ResourceType.STATUS_MESSAGE));
					this.UpdateStatus();
					return;
				}
			}
			else if (!this.frmParent.IsDisposed)
			{
				this.statusText = string.Concat(this.frmParent.BookPublishingId, this.GetResource("(E-VTV-EM006) Failed to download specified object", "(E-VTV-EM006)_FAILED", ResourceType.STATUS_MESSAGE));
				this.UpdateStatus();
				MessageHandler.ShowWarning(string.Concat(this.frmParent.BookPublishingId, this.GetResource("(E-VTV-EM005) Failed to load specified object", "(E-VTV-EM005)_FAILED", ResourceType.POPUP_MESSAGE)));
				this.frmParent.LoadDataFromNode(this.frmParent.objHistory.Current());
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.frmParent.bObjFrmTreeviewClosed = true;
			this.frmParent.LoadBookmarks();
			this.frmParent.LoadMemos();
			this.HideLoading(this.pnlForm);
			this.frmParent.SelectTreeNode();
			this.TreeViewVisible(true);
			this.frmParent.SelListInitialize();
			this.ReadINI();
		}

		private void cMenuStripPrintRange_Opening(object sender, CancelEventArgs e)
		{
		}

		private void commaSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, ",");
			Clipboard.SetText(empty);
		}

		private void commaSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
			this.dlgSaveFile.RestoreDirectory = true;
			string empty = string.Empty;
			this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, ",");
			if (empty != string.Empty && this.dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					StreamWriter streamWriter = File.CreateText(this.dlgSaveFile.FileName);
					streamWriter.Write(empty);
					streamWriter.Close();
				}
				catch
				{
					MessageHandler.ShowWarning(this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.POPUP_MESSAGE));
				}
			}
		}

		private void coToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmViewerTreeview.ToSelectedNode = this.selectedNodeText;
			string str = "1";
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.selectedNodeTag));
				XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
				str = xmlNodes.Attributes[this.rangePrintAttId].Value.ToString();
			}
			catch
			{
			}
			foreach (Form openForm in Application.OpenForms)
			{
				if (!this.bMuliRageKey || !this.bMultiRange || openForm.GetType() != typeof(frmPageSpecified))
				{
					continue;
				}
				((frmPageSpecified)openForm).UpdatePrintThisGridCol(this.selectedNodeText, str);
				break;
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

		private void EnableDisableNavigationItems()
		{
			bool flag = false;
			bool flag1 = false;
			bool flag2 = false;
			bool flag3 = false;
			if (this.tvBook.SelectedNode != this.tvBook.Nodes[0])
			{
				flag = true;
				flag1 = true;
			}
			else
			{
				flag = false;
				flag1 = false;
			}
			if (this.tvBook.SelectedNode != this.lastTreeNode)
			{
				flag3 = true;
				flag2 = true;
			}
			else
			{
				flag3 = false;
				flag2 = false;
			}
			this.frmParent.EnableNavigationItems(flag, flag1, flag2, flag3);
		}

		private void ExpandTreeNode(TreeNode tNode, int iExpandLevel)
		{
			try
			{
				if (!this.tvBook.InvokeRequired)
				{
					try
					{
						if (tNode.Nodes.Count >= 0 && tNode.Level < iExpandLevel)
						{
							foreach (TreeNode node in tNode.Nodes)
							{
								this.ExpandTreeNode(node, iExpandLevel);
							}
						}
						if (tNode.Level < iExpandLevel)
						{
							tNode.Expand();
						}
					}
					catch
					{
					}
				}
				else
				{
					frmViewerTreeview.ExpandTreeNodeDelegate expandTreeNodeDelegate = new frmViewerTreeview.ExpandTreeNodeDelegate(this.ExpandTreeNode);
					object[] objArray = new object[] { tNode, iExpandLevel };
					base.Invoke(expandTreeNodeDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void FindLastTreeNode(TreeNode treeNode)
		{
			if (treeNode.Nodes.Count <= 0)
			{
				this.lastTreeNode = treeNode;
				return;
			}
			this.FindLastTreeNode(treeNode.Nodes[treeNode.Nodes.Count - 1]);
		}

		private TreeNode FindTreeNode(TreeNodeCollection nodes, string attIdElement, string id)
		{
			TreeNode treeNode;
			XmlDocument xmlDocument = new XmlDocument();
			IEnumerator enumerator = nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode current = (TreeNode)enumerator.Current;
					if (xmlDocument.ReadNode(new XmlTextReader(new StringReader(current.Tag.ToString()))).Attributes[attIdElement].Value.ToUpper() != id.ToUpper())
					{
						TreeNode treeNode1 = this.FindTreeNode(current.Nodes, attIdElement, id);
						if (treeNode1 == null)
						{
							continue;
						}
						treeNode = treeNode1;
						return treeNode;
					}
					else
					{
						treeNode = current;
						return treeNode;
					}
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return treeNode;
		}

		public TreeNode FindTreeNodeByPageName(TreeNodeCollection nodes, string pageName)
		{
			TreeNode treeNode;
			XmlDocument xmlDocument = new XmlDocument();
			IEnumerator enumerator = nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode current = (TreeNode)enumerator.Current;
					if (current.Text.ToUpper() != pageName.ToUpper())
					{
						TreeNode treeNode1 = this.FindTreeNodeByPageName(current.Nodes, pageName);
						if (treeNode1 == null)
						{
							continue;
						}
						treeNode = treeNode1;
						return treeNode;
					}
					else
					{
						treeNode = current;
						return treeNode;
					}
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return treeNode;
		}

		private void frmViewerTreeview_VisibleChanged(object sender, EventArgs e)
		{
			this.frmParent.contentsToolStripMenuItem.Checked = base.Visible;
		}

		private void FromMenu_Click(object sender, EventArgs e)
		{
			frmViewerTreeview.FromSelectedNode = this.selectedNodeText;
			string str = "1";
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.selectedNodeTag));
				XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
				str = xmlNodes.Attributes[this.rangePrintAttId].Value.ToString();
			}
			catch
			{
			}
			foreach (Form openForm in Application.OpenForms)
			{
				if (!this.bMuliRageKey || !this.bMultiRange)
				{
					if (openForm.GetType() != typeof(GSPcLocalViewer.frmPrint.frmPrint) || !((GSPcLocalViewer.frmPrint.frmPrint)openForm).printRangePages)
					{
						continue;
					}
					((GSPcLocalViewer.frmPrint.frmPrint)openForm).UpdateFrom(this.selectedNodeText, str);
					break;
				}
				else
				{
					if (openForm.GetType() != typeof(frmPageSpecified))
					{
						continue;
					}
					((frmPageSpecified)openForm).UpdateFromGridColumn(this.selectedNodeText, str);
					break;
				}
			}
		}

		private void GetDataTreeViewText(TreeNodeCollection nodes, ref string str, string delimiter)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				if (str != string.Empty)
				{
					str = string.Concat(str, "\r\n");
				}
				for (int j = 0; j < nodes[i].Level; j++)
				{
					str = string.Concat(str, delimiter);
				}
				str = string.Concat(str, nodes[i].Text.Replace("&&", "&"));
				if (nodes[i].Nodes.Count > 0)
				{
					this.GetDataTreeViewText(nodes[i].Nodes, ref str, delimiter);
				}
			}
		}

		public int GetNodesCount()
		{
			return this.tvBook.Nodes.Count;
		}

		public void GetPagesToDownload(ref ArrayList arrayPages, string sLocalPath, bool bCompressed)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.tvBook.Tag.ToString()));
			XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
			if (xmlNodes != null)
			{
				string empty = string.Empty;
				string name = string.Empty;
				string str = string.Empty;
				string empty1 = string.Empty;
				string name1 = string.Empty;
				foreach (XmlAttribute attribute in xmlNodes.Attributes)
				{
					if (attribute.Value.ToUpper() == "PICTUREFILE")
					{
						empty = attribute.Name;
					}
					if (attribute.Value.ToUpper() == "PARTSLISTFILE")
					{
						name = attribute.Name;
					}
					if (attribute.Value.ToUpper() == "UPDATEDATE")
					{
						str = attribute.Name;
					}
					if (attribute.Value.ToUpper() == "UPDATEDATEPIC")
					{
						empty1 = attribute.Name;
					}
					if (attribute.Value.ToUpper() != "UPDATEDATEPL")
					{
						continue;
					}
					name1 = attribute.Name;
				}
				if (empty1 == string.Empty)
				{
					empty1 = str;
				}
				if (name1 == string.Empty)
				{
					name1 = str;
				}
				for (int i = 0; i < this.tvBook.Nodes.Count; i++)
				{
					this.GetPagesToDownloadRec(ref arrayPages, this.tvBook.Nodes[i], sLocalPath, empty, name, empty1, name1, bCompressed);
				}
			}
		}

		public void GetPagesToDownloadRec(ref ArrayList arrayPages, TreeNode objTreeNode, string sLocalPath, string attPicElement, string attListElement, string attUpdateDatePICElement, string attUpdateDatePLElement, bool bCompressed)
		{
			DateTime dateTime;
			bool flag = false;
			bool flag1 = false;
			string empty = string.Empty;
			string value = string.Empty;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(objTreeNode.Tag.ToString());
			XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//Pic");
			int num = 0;
			if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
			{
				num = 0;
			}
			foreach (XmlNode xmlNodes in xmlNodeLists)
			{
				if (xmlNodes.Attributes[attPicElement] != null && xmlNodes.Attributes[attUpdateDatePICElement] != null)
				{
					empty = xmlNodes.Attributes[attPicElement].Value;
					if (!empty.ToUpper().EndsWith(".DJVU") && !empty.ToUpper().EndsWith(".PDF") && !empty.ToUpper().EndsWith(".TIF"))
					{
						empty = string.Concat(empty, ".djvu");
					}
					value = xmlNodes.Attributes[attUpdateDatePICElement].Value;
					dateTime = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-GB"));
					flag = false;
					if (empty.Trim().ToLower() != ".djvu")
					{
						if (!File.Exists(string.Concat(sLocalPath, "/", empty)))
						{
							flag = true;
						}
						else if (num == 0)
						{
							flag = true;
						}
						else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(string.Concat(sLocalPath, "/", empty), this.p_Compressed, this.p_Encrypted), dateTime, num))
						{
							flag = true;
						}
						if (!arrayPages.Contains(string.Concat(empty, "|*|*|", value)) && flag)
						{
							arrayPages.Add(string.Concat(empty, "|*|*|", value));
						}
					}
				}
				if (xmlNodes.Attributes[attListElement] == null || xmlNodes.Attributes[attUpdateDatePLElement] == null)
				{
					continue;
				}
				empty = xmlNodes.Attributes[attListElement].Value;
				if (!this.frmParent.CompressedDownload)
				{
					if (!empty.ToUpper().EndsWith(".XML"))
					{
						empty = string.Concat(empty, ".xml");
					}
				}
				else if (!empty.ToUpper().EndsWith(".XML") && !empty.ToUpper().EndsWith(".ZIP"))
				{
					empty = string.Concat(empty, ".zip");
				}
				else if (empty.ToUpper().EndsWith(".XML"))
				{
					empty = empty.Replace(".xml", ".zip");
				}
				value = xmlNodes.Attributes[attUpdateDatePLElement].Value;
				dateTime = DateTime.ParseExact(value, "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-GB"));
				flag1 = false;
				if (!File.Exists(string.Concat(sLocalPath, "/", empty)))
				{
					flag1 = true;
				}
				else if (num == 0)
				{
					flag1 = true;
				}
				else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(string.Concat(sLocalPath, "/", empty), this.p_Compressed, this.p_Encrypted), dateTime, num))
				{
					flag1 = true;
				}
				if (arrayPages.Contains(empty) || !flag1)
				{
					continue;
				}
				arrayPages.Add(string.Concat(empty, "|*|*|", value));
			}
			if (objTreeNode.Nodes.Count != 0)
			{
				for (int i = 0; i < objTreeNode.Nodes.Count; i++)
				{
					this.GetPagesToDownloadRec(ref arrayPages, objTreeNode.Nodes[i], sLocalPath, attPicElement, attListElement, attUpdateDatePICElement, attUpdateDatePLElement, bCompressed);
				}
			}
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MAIN_FORM']");
				str = string.Concat(str, "/Screen[@Name='CONTENTS']");
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
					resourceValue = this.frmParent.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					foreach (Control control in parentPanel.Controls)
					{
						if (control == this.picLoading)
						{
							continue;
						}
						control.Enabled = true;
					}
					this.picLoading.Hide();
				}
				else
				{
					frmViewerTreeview.HideLoadingDelegate hideLoadingDelegate = new frmViewerTreeview.HideLoadingDelegate(this.HideLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(hideLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		public void HideMenuStripItem()
		{
			this.cMenuStripPrintRange.Items["FromMenu"].Visible = false;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmViewerTreeview));
			this.pnlForm = new Panel();
			this.tvBook = new CustomTreeView();
			this.picLoading = new PictureBox();
			this.bgWorker = new BackgroundWorker();
			this.cMenuStripPrintRange = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.FromMenu = new ToolStripMenuItem();
			this.toMenu = new ToolStripMenuItem();
			this.tsmiPrintThisContent = new ToolStripMenuItem();
			this.cmsTreeview = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
			this.commaSeparatedToolStripMenuItem = new ToolStripMenuItem();
			this.tabSeparatedToolStripMenuItem = new ToolStripMenuItem();
			this.exportToFileToolStripMenuItem = new ToolStripMenuItem();
			this.commaSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
			this.tabSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
			this.dlgSaveFile = new SaveFileDialog();
			this.pnlForm.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			this.cMenuStripPrintRange.SuspendLayout();
			this.cmsTreeview.SuspendLayout();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.tvBook);
			this.pnlForm.Controls.Add(this.picLoading);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(296, 456);
			this.pnlForm.TabIndex = 1;
			this.tvBook.BorderStyle = BorderStyle.None;
			this.tvBook.Dock = DockStyle.Fill;
			this.tvBook.DrawMode = TreeViewDrawMode.OwnerDrawText;
			this.tvBook.Location = new Point(0, 0);
			this.tvBook.Name = "tvBook";
			this.tvBook.Size = new System.Drawing.Size(294, 454);
			this.tvBook.TabIndex = 0;
			this.tvBook.AfterSelect += new TreeViewEventHandler(this.tvBook_AfterSelect);
			this.tvBook.MouseDown += new MouseEventHandler(this.tvBook_MouseDown_1);
			this.tvBook.BeforeSelect += new TreeViewCancelEventHandler(this.tvBook_BeforeSelect);
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = GSPcLocalViewer.Properties.Resources.Loading1;
			this.picLoading.Location = new Point(3, 3);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 18;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			ToolStripItemCollection items = this.cMenuStripPrintRange.Items;
			ToolStripItem[] fromMenu = new ToolStripItem[] { this.FromMenu, this.toMenu, this.tsmiPrintThisContent };
			items.AddRange(fromMenu);
			this.cMenuStripPrintRange.Name = "cMenuStripPrintRange";
			this.cMenuStripPrintRange.Size = new System.Drawing.Size(166, 92);
			this.cMenuStripPrintRange.Opening += new CancelEventHandler(this.cMenuStripPrintRange_Opening);
			this.FromMenu.Name = "FromMenu";
			this.FromMenu.Size = new System.Drawing.Size(165, 22);
			this.FromMenu.Text = "Print From";
			this.FromMenu.Click += new EventHandler(this.FromMenu_Click);
			this.toMenu.Name = "toMenu";
			this.toMenu.Size = new System.Drawing.Size(165, 22);
			this.toMenu.Text = "Print To";
			this.toMenu.Click += new EventHandler(this.toMenu_Click_1);
			this.tsmiPrintThisContent.Name = "tsmiPrintThisContent";
			this.tsmiPrintThisContent.Size = new System.Drawing.Size(165, 22);
			this.tsmiPrintThisContent.Text = "Print This Contents";
			this.tsmiPrintThisContent.Click += new EventHandler(this.coToolStripMenuItem_Click);
			ToolStripItemCollection toolStripItemCollections = this.cmsTreeview.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.copyToClipboardToolStripMenuItem, this.exportToFileToolStripMenuItem };
			toolStripItemCollections.AddRange(toolStripItemArray);
			this.cmsTreeview.Name = "cmsPartslist";
			this.cmsTreeview.Size = new System.Drawing.Size(163, 48);
			ToolStripItemCollection dropDownItems = this.copyToClipboardToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.commaSeparatedToolStripMenuItem, this.tabSeparatedToolStripMenuItem };
			dropDownItems.AddRange(toolStripItemArray1);
			this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
			this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.copyToClipboardToolStripMenuItem.Text = "Copy To Clipboard";
			this.commaSeparatedToolStripMenuItem.Name = "commaSeparatedToolStripMenuItem";
			this.commaSeparatedToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.commaSeparatedToolStripMenuItem.Text = "Comma Separated";
			this.commaSeparatedToolStripMenuItem.Click += new EventHandler(this.commaSeparatedToolStripMenuItem_Click);
			this.tabSeparatedToolStripMenuItem.Name = "tabSeparatedToolStripMenuItem";
			this.tabSeparatedToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.tabSeparatedToolStripMenuItem.Text = "Tab Separated";
			this.tabSeparatedToolStripMenuItem.Click += new EventHandler(this.tabSeparatedToolStripMenuItem_Click);
			ToolStripItemCollection dropDownItems1 = this.exportToFileToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray2 = new ToolStripItem[] { this.commaSeparatedToolStripMenuItem1, this.tabSeparatedToolStripMenuItem1 };
			dropDownItems1.AddRange(toolStripItemArray2);
			this.exportToFileToolStripMenuItem.Name = "exportToFileToolStripMenuItem";
			this.exportToFileToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.exportToFileToolStripMenuItem.Text = "Export To File";
			this.commaSeparatedToolStripMenuItem1.Name = "commaSeparatedToolStripMenuItem1";
			this.commaSeparatedToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
			this.commaSeparatedToolStripMenuItem1.Text = "Comma Separated";
			this.commaSeparatedToolStripMenuItem1.Click += new EventHandler(this.commaSeparatedToolStripMenuItem1_Click);
			this.tabSeparatedToolStripMenuItem1.Name = "tabSeparatedToolStripMenuItem1";
			this.tabSeparatedToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
			this.tabSeparatedToolStripMenuItem1.Text = "Tab Separated";
			this.tabSeparatedToolStripMenuItem1.Click += new EventHandler(this.tabSeparatedToolStripMenuItem1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(296, 456);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.HideOnClose = true;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "frmViewerTreeview";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Contents";
			base.VisibleChanged += new EventHandler(this.frmViewerTreeview_VisibleChanged);
			this.pnlForm.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			this.cMenuStripPrintRange.ResumeLayout(false);
			this.cmsTreeview.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public void LoadBook()
		{
			if (!Global.SecurityLocksOpen(this.frmParent.BookNode, this.frmParent.SchemaNode, this.frmParent.ServerId, this.frmParent))
			{
				this.frmParent.Close();
				return;
			}
			if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"] == null)
			{
				this.p_Encrypted = false;
			}
			else if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() != "ON")
			{
				this.p_Encrypted = false;
			}
			else
			{
				this.p_Encrypted = true;
			}
			if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"] == null)
			{
				this.p_Compressed = false;
				this.frmParent.CompressedDownload = false;
			}
			else if (Program.iniServers[this.frmParent.p_ServerId].items["SETTINGS", "DATA_COMPRESSION"].ToUpper() != "ON")
			{
				this.p_Compressed = false;
				this.frmParent.CompressedDownload = false;
			}
			else
			{
				this.p_Compressed = true;
				this.frmParent.CompressedDownload = true;
			}
			this.statusText = string.Concat(this.GetResource("Downloading", "DOWNLOADING", ResourceType.STATUS_MESSAGE), " ", this.frmParent.BookPublishingId, ".xml");
			this.UpdateStatus();
			this.frmParent.ClearBookmarks();
			this.bgWorker.RunWorkerAsync();
		}

		private bool LoadBookInTree(string sFilePath)
		{
			bool flag;
			XmlDocument xmlDocument = new XmlDocument();
			this.frmParent.lstFilteredPages = new List<string>();
			if (!File.Exists(sFilePath))
			{
				return false;
			}
			if (!this.p_Compressed)
			{
				try
				{
					xmlDocument.Load(sFilePath);
				}
				catch
				{
					flag = false;
					return flag;
				}
			}
			else
			{
				try
				{
					Global.Unzip(sFilePath);
					xmlDocument.Load(sFilePath.ToLower().Replace(".zip", ".xml"));
				}
				catch
				{
				}
			}
			if (this.p_Encrypted)
			{
				try
				{
					string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
					xmlDocument.DocumentElement.InnerXml = str;
				}
				catch
				{
					flag = false;
					return flag;
				}
			}
			this.objXmlSchemaNode = xmlDocument.SelectSingleNode("//Schema");
			if (this.objXmlSchemaNode == null)
			{
				return false;
			}
			string empty = string.Empty;
			string name = string.Empty;
			string empty1 = string.Empty;
			foreach (XmlAttribute attribute in this.objXmlSchemaNode.Attributes)
			{
				if (attribute.Value.ToUpper().Equals("ID"))
				{
					empty = attribute.Name;
					this.rangePrintAttId = attribute.Name;
				}
				else if (!attribute.Value.ToUpper().Equals("PAGENAME"))
				{
					if (!attribute.Value.ToUpper().Equals("PICTUREFILE"))
					{
						continue;
					}
					empty1 = attribute.Name;
				}
				else
				{
					name = attribute.Name;
				}
			}
			if (empty == string.Empty || name == string.Empty)
			{
				return false;
			}
			this.tvBook.Tag = this.objXmlSchemaNode.OuterXml;
			foreach (XmlNode xmlNodes in xmlDocument.SelectNodes("//Book/Pg"))
			{
				this.AddNode(null, this.objXmlSchemaNode, xmlNodes, name, empty, empty1);
			}
			if (this.tvBook.Nodes.Count > 0)
			{
				this.FindLastTreeNode(this.tvBook.Nodes[this.tvBook.Nodes.Count - 1]);
			}
			try
			{
				this.frmParent.sFirstPageTitle = this.tvBook.Nodes[0].Text;
				this.frmParent.UpdateViewerTitle();
			}
			catch
			{
			}
			if (!Settings.Default.ExpandAllContents)
			{
				try
				{
					foreach (TreeNode node in this.tvBook.Nodes)
					{
						this.ExpandTreeNode(node, Settings.Default.ExpandContentsLevel - 1);
					}
				}
				catch
				{
				}
			}
			else
			{
				this.TreeViewExpandAllNodes();
			}
			return true;
		}

		public void LoadPictureInTree()
		{
			XmlDocument xmlDocument = new XmlDocument();
			this.frmParent.EnableAddPicMemoTSB(false);
			this.frmParent.EnableAddPartMemoMenu(false);
			this.frmParent.objFrmPicture.EnableAddPicMemoTSB(false);
			this.EnableDisableNavigationItems();
			try
			{
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.tvBook.Tag.ToString()));
				XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
				xmlTextReader = new XmlTextReader(new StringReader(this.tvBook.SelectedNode.Tag.ToString()));
				XmlNode xmlNodes1 = xmlDocument.ReadNode(xmlTextReader);
				if (!this.frmParent.objFrmInfo.IsDisposed)
				{
					this.frmParent.objFrmInfo.LoadData(xmlNodes, xmlNodes1);
				}
				this.frmParent.LoadPicture(xmlNodes, xmlNodes1);
			}
			catch (Exception exception)
			{
			}
		}

		public void LoadResources()
		{
			this.Text = string.Concat(this.GetResource("Contents", "CONTENTS", ResourceType.TITLE), "      ");
			this.FromMenu.Text = this.GetResource("Print From", "PRINT_FROM", ResourceType.CONTEXT_MENU);
			this.toMenu.Text = this.GetResource("Print To", "PRINT_TO", ResourceType.CONTEXT_MENU);
			this.tsmiPrintThisContent.Text = this.GetResource("Print This Contents", "PRINT_THIS_CONTENTS", ResourceType.CONTEXT_MENU);
			this.tabSeparatedToolStripMenuItem.Text = this.GetResource("Tab Separated", "TAB_SEPARATED", ResourceType.CONTEXT_MENU);
			this.tabSeparatedToolStripMenuItem1.Text = this.GetResource("Tab Separated", "TAB_SEPARATED_FILE", ResourceType.CONTEXT_MENU);
			this.copyToClipboardToolStripMenuItem.Text = this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.CONTEXT_MENU);
			this.commaSeparatedToolStripMenuItem.Text = this.GetResource("Comma Separated", "COMMA_SEPARATED", ResourceType.CONTEXT_MENU);
			this.commaSeparatedToolStripMenuItem1.Text = this.GetResource("Comma Separated", "COMMA_SEPARATED_FILE", ResourceType.CONTEXT_MENU);
			this.exportToFileToolStripMenuItem.Text = this.GetResource("Export To File", "EXPORT_TO_FILE", ResourceType.CONTEXT_MENU);
		}

		private void ReadINI()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.p_ServerId].sIniKey, ".ini");
				ArrayList arrayLists = new ArrayList();
				arrayLists = (new IniFileIO()).GetKeys(str, "PRINTER_SETTINGS");
				int num = 0;
				while (num < arrayLists.Count)
				{
					IniFileIO iniFileIO = new IniFileIO();
					string keyValue = iniFileIO.GetKeyValue("PRINTER_SETTINGS", arrayLists[num].ToString(), str);
					if (arrayLists[num].ToString() != "PAGE_SPECIFED_ACTION")
					{
						this.bMuliRageKey = false;
						num++;
					}
					else
					{
						this.bMuliRageKey = true;
						if (keyValue.ToString().ToUpper() != "MULTI")
						{
							if (keyValue.ToString().ToUpper() != "SINGLE")
							{
								break;
							}
							this.bMultiRange = false;
							break;
						}
						else
						{
							this.bMultiRange = true;
							break;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		public void SelectFirstNode()
		{
			if (this.tvBook.Nodes.Count > 0)
			{
				this.TVBookSelectFirstNode();
			}
		}

		public void SelectLastNode()
		{
			this.TVBookSelectLastNode();
		}

		public void SelectNextNode()
		{
			try
			{
				this.TVBookSelectNextNode();
			}
			catch
			{
			}
		}

		public void SelectPreviousNode()
		{
			try
			{
				this.TVBookSelectPreviousNode();
			}
			catch
			{
			}
		}

		public void SelectTreeNode(string pageId)
		{
			if (pageId != string.Empty)
			{
				XmlDocument xmlDocument = new XmlDocument();
				string name = "";
				string str = this.tvBook.Tag.ToString();
				foreach (XmlAttribute attribute in xmlDocument.ReadNode(new XmlTextReader(new StringReader(str))).Attributes)
				{
					if (!attribute.Value.ToUpper().Equals("ID"))
					{
						continue;
					}
					name = attribute.Name;
				}
				if (name == "")
				{
					if (this.tvBook.Nodes.Count > 0)
					{
						this.tvBook.SelectedNode = this.tvBook.Nodes[0];
					}
					return;
				}
				TreeNode treeNode = this.FindTreeNode(this.tvBook.Nodes, name, pageId);
				if (treeNode != null)
				{
					this.TVBookGotoPage(treeNode);
					return;
				}
				if (this.tvBook.Nodes.Count > 0 && treeNode != null)
				{
					this.TVBookGotoPage(treeNode);
				}
			}
			else if (this.tvBook.Nodes.Count > 0)
			{
				this.TVBookSelectFirstNode();
				return;
			}
		}

		public void SelectTreeNodeByPageName(string pageName)
		{
			if (pageName != string.Empty)
			{
				TreeNode treeNode = this.FindTreeNodeByPageName(this.tvBook.Nodes, pageName);
				if (treeNode != null)
				{
					this.tvBook.SelectedNode = treeNode;
					return;
				}
				if (this.tvBook.Nodes.Count > 0)
				{
					this.tvBook.SelectedNode = this.tvBook.Nodes[0];
				}
			}
			else if (this.tvBook.Nodes.Count > 0)
			{
				this.tvBook.SelectedNode = this.tvBook.Nodes[0];
				return;
			}
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					foreach (Control control in parentPanel.Controls)
					{
						if (control == this.picLoading)
						{
							continue;
						}
						control.Enabled = false;
					}
					this.picLoading.Left = parentPanel.Left + parentPanel.Width / 2 - this.picLoading.Width / 2;
					this.picLoading.Top = parentPanel.Top + parentPanel.Height / 2 - this.picLoading.Height / 2;
					this.picLoading.Parent = parentPanel;
					this.picLoading.BringToFront();
					this.picLoading.Show();
				}
				else
				{
					frmViewerTreeview.ShowLoadingDelegate showLoadingDelegate = new frmViewerTreeview.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		public void ShowLoading()
		{
			this.ShowLoading(this.pnlForm);
		}

		private void tabSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, "\t");
			Clipboard.SetText(empty);
		}

		private void tabSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
			this.dlgSaveFile.RestoreDirectory = true;
			string empty = string.Empty;
			this.GetDataTreeViewText(this.tvBook.Nodes, ref empty, "\t");
			if (empty != string.Empty && this.dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					StreamWriter streamWriter = File.CreateText(this.dlgSaveFile.FileName);
					streamWriter.Write(empty);
					streamWriter.Close();
				}
				catch
				{
					MessageHandler.ShowWarning(this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.POPUP_MESSAGE));
				}
			}
		}

		private void toMenu_Click_1(object sender, EventArgs e)
		{
			frmViewerTreeview.ToSelectedNode = this.selectedNodeText;
			string str = "1";
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlTextReader xmlTextReader = new XmlTextReader(new StringReader(this.selectedNodeTag));
				XmlNode xmlNodes = xmlDocument.ReadNode(xmlTextReader);
				str = xmlNodes.Attributes[this.rangePrintAttId].Value.ToString();
			}
			catch
			{
			}
			foreach (Form openForm in Application.OpenForms)
			{
				if (!this.bMuliRageKey || !this.bMultiRange)
				{
					if (openForm.GetType() != typeof(GSPcLocalViewer.frmPrint.frmPrint) || !((GSPcLocalViewer.frmPrint.frmPrint)openForm).printRangePages)
					{
						continue;
					}
					((GSPcLocalViewer.frmPrint.frmPrint)openForm).UpdateTo(this.selectedNodeText, str);
					break;
				}
				else
				{
					if (openForm.GetType() != typeof(frmPageSpecified))
					{
						continue;
					}
					((frmPageSpecified)openForm).UpdateToGridCol(this.selectedNodeText, str);
					break;
				}
			}
		}

		private void TreeViewAddNode(TreeNode tnParent, TreeNode tnChild)
		{
			if (!this.tvBook.InvokeRequired)
			{
				if (tnParent != null)
				{
					tnParent.Nodes.Add(tnChild);
					return;
				}
				this.tvBook.Nodes.Add(tnChild);
				return;
			}
			CustomTreeView customTreeView = this.tvBook;
			frmViewerTreeview.TreeViewAddNodeDelegate treeViewAddNodeDelegate = new frmViewerTreeview.TreeViewAddNodeDelegate(this.TreeViewAddNode);
			object[] objArray = new object[] { tnParent, tnChild };
			customTreeView.Invoke(treeViewAddNodeDelegate, objArray);
		}

		private void TreeViewClearNodes()
		{
			if (!this.tvBook.InvokeRequired)
			{
				this.tvBook.Nodes.Clear();
				return;
			}
			this.tvBook.Invoke(new frmViewerTreeview.TreeViewClearNodesDelegate(this.TreeViewClearNodes));
		}

		public void TreeViewClearSelection()
		{
			if (!this.tvBook.InvokeRequired)
			{
				this.tvBook.SelectedNode = null;
				return;
			}
			this.tvBook.Invoke(new frmViewerTreeview.TreeViewClearSelectionDelegate(this.TreeViewClearSelection));
		}

		private void TreeViewExpandAllNodes()
		{
			if (!this.tvBook.InvokeRequired)
			{
				this.tvBook.ExpandAll();
				return;
			}
			this.tvBook.Invoke(new frmViewerTreeview.TreeViewExpandAllNodesDelegate(this.TreeViewExpandAllNodes));
		}

		private void TreeViewVisible(bool visible)
		{
			if (!this.tvBook.InvokeRequired)
			{
				this.tvBook.Visible = visible;
				return;
			}
			CustomTreeView customTreeView = this.tvBook;
			frmViewerTreeview.TreeViewVisibleDelegate treeViewVisibleDelegate = new frmViewerTreeview.TreeViewVisibleDelegate(this.TreeViewVisible);
			object[] objArray = new object[] { visible };
			customTreeView.Invoke(treeViewVisibleDelegate, objArray);
		}

		private void tvBook_AfterSelect(object sender, TreeViewEventArgs e)
		{
			this.LoadPictureInTree();
			this.frmParent.LoadMemos();
		}

		private void tvBook_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (this.frmParent.objFrmPartlist.isWorking || this.frmParent.objFrmPicture.isWorking)
			{
				e.Cancel = true;
			}
		}

		private void tvBook_MouseDown_1(object sender, MouseEventArgs e)
		{
			Form current;
			bool flag = false;
			try
			{
				string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.p_ServerId].sIniKey, ".ini");
				ArrayList arrayLists = new ArrayList();
				List<string> strs = new List<string>();
				arrayLists = (new IniFileIO()).GetKeys(str, "PRINTER_SETTINGS");
				for (int i = 0; i < arrayLists.Count; i++)
				{
					IniFileIO iniFileIO = new IniFileIO();
					string keyValue = iniFileIO.GetKeyValue("PRINTER_SETTINGS", arrayLists[i].ToString(), str);
					if (arrayLists[i].ToString() == "PAGE_SPECIFED_ACTION")
					{
						if (keyValue.ToString().ToUpper() == "MULTI")
						{
							flag = true;
						}
						else if (keyValue.ToString().ToUpper() == "SINGLE")
						{
							flag = false;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Right)
				{
					bool flag1 = false;
					foreach (Form openForm in Application.OpenForms)
					{
						if (openForm.GetType() != typeof(GSPcLocalViewer.frmPrint.frmPrint) || !((GSPcLocalViewer.frmPrint.frmPrint)openForm).printRangePages)
						{
							continue;
						}
						flag1 = true;
						break;
					}
					if (!flag1)
					{
						this.tvBook.ContextMenuStrip = this.cmsTreeview;
					}
					else
					{
						IEnumerator enumerator = Application.OpenForms.GetEnumerator();
						try
						{
							do
							{
								if (!enumerator.MoveNext())
								{
									break;
								}
								current = (Form)enumerator.Current;
							}
							while (current.GetType() != typeof(frmPageSpecified));
						}
						finally
						{
							IDisposable disposable = enumerator as IDisposable;
							if (disposable != null)
							{
								disposable.Dispose();
							}
						}
						if (!flag)
						{
							this.cMenuStripPrintRange.Items[2].Visible = false;
						}
						else
						{
							this.cMenuStripPrintRange.Items[2].Visible = true;
						}
						this.tvBook.ContextMenuStrip = this.cMenuStripPrintRange;
						this.selectedNodeText = this.tvBook.HitTest(e.X, e.Y).Node.Text;
						this.selectedNodeTag = this.tvBook.HitTest(e.X, e.Y).Node.Tag.ToString();
					}
				}
			}
			catch
			{
			}
		}

		private void TVBookGotoPage(TreeNode pageID)
		{
			try
			{
				if (!this.tvBook.InvokeRequired)
				{
					this.tvBook.SelectedNode = pageID;
				}
				else
				{
					CustomTreeView customTreeView = this.tvBook;
					frmViewerTreeview.TVBookGotoPageDeligate tVBookGotoPageDeligate = new frmViewerTreeview.TVBookGotoPageDeligate(this.TVBookGotoPage);
					object[] objArray = new object[] { pageID };
					customTreeView.Invoke(tVBookGotoPageDeligate, objArray);
				}
			}
			catch
			{
			}
		}

		private void TVBookSelectFirstNode()
		{
			try
			{
				if (!this.tvBook.InvokeRequired)
				{
					this.tvBook.SelectedNode = this.tvBook.Nodes[0];
				}
				else
				{
					this.tvBook.Invoke(new frmViewerTreeview.TVBookSelectFirstNodeDeligate(this.TVBookSelectFirstNode), new object[0]);
				}
			}
			catch
			{
			}
		}

		private void TVBookSelectLastNode()
		{
			try
			{
				if (!this.tvBook.InvokeRequired)
				{
					this.tvBook.SelectedNode = this.lastTreeNode;
				}
				else
				{
					this.tvBook.Invoke(new frmViewerTreeview.TVBookSelectLastNodeDeligate(this.TVBookSelectLastNode), new object[0]);
				}
			}
			catch
			{
			}
		}

		private void TVBookSelectNextNode()
		{
			try
			{
				if (!this.tvBook.InvokeRequired)
				{
					try
					{
						if (this.tvBook.SelectedNode != this.lastTreeNode && this.tvBook.SelectedNode.NextVisibleNode != null)
						{
							this.tvBook.SelectedNode = this.tvBook.SelectedNode.NextVisibleNode;
						}
					}
					catch
					{
					}
				}
				else
				{
					this.tvBook.Invoke(new frmViewerTreeview.TVBookSelectNextNodeDeligate(this.TVBookSelectNextNode), new object[0]);
				}
			}
			catch
			{
			}
		}

		private void TVBookSelectPreviousNode()
		{
			try
			{
				if (!this.tvBook.InvokeRequired)
				{
					try
					{
						if (this.tvBook.SelectedNode != this.tvBook.Nodes[0] && this.tvBook.SelectedNode.PrevVisibleNode != null)
						{
							this.tvBook.SelectedNode = this.tvBook.SelectedNode.PrevVisibleNode;
						}
					}
					catch
					{
					}
				}
				else
				{
					this.tvBook.Invoke(new frmViewerTreeview.TVBookSelectPreviousNodeDeligate(this.TVBookSelectPreviousNode), new object[0]);
				}
			}
			catch
			{
			}
		}

		public void UpdateFont()
		{
			this.tvBook.Font = Settings.Default.appFont;
		}

		private void UpdateStatus()
		{
			if (!this.frmParent.InvokeRequired)
			{
				this.frmParent.UpdateStatus(this.statusText);
				return;
			}
			frmViewer _frmViewer = this.frmParent;
			frmViewerTreeview.StatusDelegate statusDelegate = new frmViewerTreeview.StatusDelegate(this.frmParent.UpdateStatus);
			object[] objArray = new object[] { this.statusText };
			_frmViewer.Invoke(statusDelegate, objArray);
		}

		private delegate void ExpandTreeNodeDelegate(TreeNode tNode, int iExpandLevel);

		private delegate void HideLoadingDelegate(Panel parentPanel);

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void StatusDelegate(string status);

		private delegate void TreeViewAddNodeDelegate(TreeNode tnParent, TreeNode tnChild);

		private delegate void TreeViewClearNodesDelegate();

		private delegate void TreeViewClearSelectionDelegate();

		private delegate void TreeViewExpandAllNodesDelegate();

		private delegate void TreeViewVisibleDelegate(bool visible);

		private delegate void TVBookGotoPageDeligate(TreeNode pageID);

		private delegate void TVBookSelectFirstNodeDeligate();

		private delegate void TVBookSelectLastNodeDeligate();

		private delegate void TVBookSelectNextNodeDeligate();

		private delegate void TVBookSelectPreviousNodeDeligate();
	}
}
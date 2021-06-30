using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmMemoTasks : Form
	{
		private IContainer components;

		private Panel pnlTasks;

		private Panel pnlTasks2;

		private Label lblTasksTitle;

		private Label lblView;

		private Label lblSpace1;

		private Label lblGlobalMemo;

		private Label lblLocalMemo;

		private Label lblAdminMemo;

		private Panel pnlMemoTypes;

		public TreeView tvMemoTypes;

		private frmMemo frmParent;

		public int intMemoType;

		private string strMemoPriority = "LOCAL";

		private string strDateFormat = string.Empty;

		private TreeNode tnPreviouisNode = new TreeNode();

		private bool bDisabledMemo;

		private string strTextMemoState = "TRUE";

		private string strReferenceMemoState = "TRUE";

		private string strHyperlinkMemoState = "TRUE";

		private string strProgramMemoState = "TRUE";

		public frmMemoTasks(frmMemo frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.UpdateFont();
			this.SetControlsText();
		}

		public void AddMemoToTree(string type, string value, string strMemoTyp, string strHypDescription)
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			empty = (value.Length <= 25 ? value : string.Concat(value.Substring(0, 25), "..."));
			if (!type.ToUpper().Equals("TXT") && empty.Contains("|"))
			{
				empty = empty.Replace("|", " ");
			}
			if (type == "hyp")
			{
				empty = (strHypDescription.Length <= 25 ? strHypDescription : string.Concat(strHypDescription.Substring(0, 25), "..."));
			}
			if (type.ToUpper().Equals("TXT"))
			{
				str = "Text";
			}
			else if (type.ToUpper().Equals("REF"))
			{
				str = "Reference";
			}
			else if (!type.ToUpper().Equals("HYP"))
			{
				str = (!type.ToUpper().Equals("PRG") ? "Unknown" : "Program");
			}
			else
			{
				str = "Hyperlink";
			}
			empty1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			Guid guid = Guid.NewGuid();
			string str1 = string.Empty;
			try
			{
				if (strMemoTyp != "LocalMemo")
				{
					this.frmParent.objFrmMemoGlobal.iMemoResultCounter = 0;
					if (!this.frmParent.objFrmMemoGlobal.bSaveMemoOnBookLevel || !this.frmParent.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || !(this.frmParent.sPartNumber != string.Empty))
					{
						str1 = (type != "hyp" ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
						this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(guid.ToString(), str1);
					}
					else
					{
						for (int i = 0; i < this.frmParent.objFrmMemoGlobal.lstResults.Count; i++)
						{
							guid = Guid.NewGuid();
							str1 = (type != "hyp" ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
							this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(guid.ToString(), str1);
							this.frmParent.objFrmMemoGlobal.iMemoResultCounter++;
						}
					}
				}
				else
				{
					this.frmParent.objFrmMemoLocal.iMemoResultCounter = 0;
					if (!this.frmParent.objFrmMemoLocal.bSaveMemoOnBookLevel || !this.frmParent.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || !(this.frmParent.sPartNumber != string.Empty))
					{
						str1 = (type != "hyp" ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
						this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(guid.ToString(), str1);
					}
					else
					{
						for (int j = 0; j < this.frmParent.objFrmMemoLocal.lstResults.Count; j++)
						{
							guid = Guid.NewGuid();
							str1 = (type != "hyp" ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
							this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(guid.ToString(), str1);
							this.frmParent.objFrmMemoLocal.iMemoResultCounter++;
						}
					}
				}
			}
			catch (Exception exception)
			{
			}
			try
			{
				DateTime dateTime = DateTime.ParseExact(empty1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				string[] strArrays = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
				if (this.strDateFormat != string.Empty)
				{
					if (this.strDateFormat.ToUpper() != "INVALID")
					{
						string[] strArrays1 = strArrays;
						for (int k = 0; k < (int)strArrays1.Length; k++)
						{
							string str2 = strArrays1[k];
							if (this.strDateFormat == str2)
							{
								empty1 = dateTime.ToString(this.strDateFormat);
							}
						}
					}
					else
					{
						empty1 = "Unknown";
					}
				}
			}
			catch
			{
			}
			this.frmParent.objFrmTasks.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
			Graphics graphic = this.frmParent.objFrmTasks.tvMemoTypes.CreateGraphics();
			System.Drawing.Font font = this.frmParent.objFrmTasks.tvMemoTypes.Font;
			List<frmMemoTasks.TreeNodeText> treeNodeTexts = new List<frmMemoTasks.TreeNodeText>();
			string[] multilingualValue = new string[] { empty, "\r\n", empty1, "\r\n", this.GetMultilingualValue(str) };
			treeNodeTexts.Add(new frmMemoTasks.TreeNodeText(string.Concat(multilingualValue)));
			foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTexts)
			{
				if (empty.Contains("|"))
				{
					empty = empty.Replace("|", "PIPESIGN");
				}
				if (value.Contains("|"))
				{
					value = value.Replace("|", "PIPESIGN");
				}
				TreeNode treeNode = new TreeNode();
				string[] strArrays2 = new string[] { empty, "|", empty1, "|", str, "|", value, "|", guid.ToString() };
				treeNode.Name = string.Concat(strArrays2);
				treeNode.Tag = treeNodeText;
				treeNode.Text = this.MaxWidthString(graphic, font, treeNodeText.TextList);
				if (strMemoTyp != "LocalMemo")
				{
					this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(treeNode);
					this.frmParent.objFrmMemoGlobal.bMemoChanged = true;
				}
				else
				{
					this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(treeNode);
					this.frmParent.objFrmMemoLocal.bMemoChanged = true;
				}
			}
			this.tvMemoTypes.ExpandAll();
			this.tvMemoTypes.Refresh();
			this.tvMemoTypes.Focus();
			this.tvMemoTypes.SelectedNode.EnsureVisible();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmMemoTasks_Load(object sender, EventArgs e)
		{
			this.lblLocalMemo.Enabled = Settings.Default.EnableLocalMemo;
			this.lblGlobalMemo.Enabled = Settings.Default.EnableGlobalMemo;
			this.lblAdminMemo.Enabled = Settings.Default.EnableAdminMemo;
			this.GetMemoStates();
			this.intMemoType = this.GetMemoType();
			this.strMemoPriority = this.GetMemoPriority();
			this.strDateFormat = this.GetDateFormat();
			if (this.intMemoType != 2)
			{
				this.pnlMemoTypes.Visible = false;
				this.pnlTasks2.Visible = true;
				this.pnlTasks2.Dock = DockStyle.Top;
				return;
			}
			this.pnlTasks2.Visible = false;
			this.pnlMemoTypes.Visible = true;
			this.pnlTasks2.Dock = DockStyle.Bottom;
			if (!Settings.Default.EnableLocalMemo && !Settings.Default.EnableGlobalMemo && !Settings.Default.EnableAdminMemo)
			{
				this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
				this.frmParent.objFrmMemoLocal.pnlBottom.Top = 33;
				this.frmParent.objFrmMemoLocal.Show();
				this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
			}
			else if (Settings.Default.EnableLocalMemo)
			{
				this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
				this.frmParent.objFrmMemoLocal.pnlBottom.Top = 33;
				this.frmParent.objFrmMemoLocal.Show();
				this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
			}
			else if (Settings.Default.EnableGlobalMemo)
			{
				this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
				this.frmParent.objFrmMemoGlobal.pnlBottom.Top = 33;
				this.frmParent.objFrmMemoGlobal.Show();
				this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
			}
			else if (Settings.Default.EnableAdminMemo)
			{
				this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
				this.frmParent.objFrmMemoAdmin.pnlBottom.Top = 33;
				this.frmParent.objFrmMemoAdmin.Show();
				this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
			}
			this.LoadMemo(true, true);
		}

		private string GetDateFormat()
		{
			string empty;
			try
			{
				string str = string.Empty;
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"] != null)
				{
					if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() != "HIDDEN")
					{
						str = Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString();
					}
					else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "DATE"].ToString().ToUpper() == "HIDDEN")
					{
						str = "INVALID";
					}
				}
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"] != null && Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString().ToUpper() != "HIDDEN")
				{
					str = (str.ToUpper() != "INVALID" ? string.Concat(str, " ", Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString()) : Program.iniServers[this.frmParent.frmParent.p_ServerId].items["DATE_SETTINGS", "TIME"].ToString());
				}
				this.strDateFormat = str;
				empty = this.strDateFormat;
			}
			catch (Exception exception)
			{
				empty = string.Empty;
			}
			return empty;
		}

		private List<string> GetMemoDetails(XmlNode xNode)
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			string str1 = string.Empty;
			List<string> strs = new List<string>();
			if (xNode.Attributes["Value"] == null || !(xNode.Attributes["Value"].Value.Trim() != string.Empty))
			{
				return strs;
			}
			str = xNode.Attributes["Value"].Value.Trim();
			if (xNode.Attributes["Value"].Value.Trim().Length <= 25)
			{
				empty = xNode.Attributes["Value"].Value.Trim();
			}
			else if (!xNode.Attributes["Value"].Value.Contains("\n"))
			{
				empty = string.Concat(xNode.Attributes["Value"].Value.Trim().Substring(0, 25), "...");
			}
			else
			{
				empty = xNode.Attributes["Value"].Value.Replace("\n", " ");
				empty = string.Concat(empty.Substring(0, 25), "...");
			}
			if (xNode.Attributes["Type"] == null || !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "TXT") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "REF") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "HYP") && !(xNode.Attributes["Type"].Value.Trim().ToUpper() == "PRG"))
			{
				return strs;
			}
			if (xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("TXT"))
			{
				empty1 = "Text";
			}
			else if (xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("REF"))
			{
				empty1 = "Reference";
			}
			else if (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("HYP"))
			{
				empty1 = (!xNode.Attributes["Type"].Value.Trim().ToUpper().Equals("PRG") ? "Unknown" : "Program");
			}
			else
			{
				empty1 = "Hyperlink";
			}
			if (!(empty1.ToUpper() == "TEXT") && !(empty1.ToUpper() == "HYPERLINK"))
			{
				if (empty.Contains("||"))
				{
					empty = empty.Replace("||", " ");
				}
				if (empty.Contains("|"))
				{
					empty = empty.Replace("|", " ");
				}
			}
			if (empty1 == "Hyperlink")
			{
				if (xNode.Attributes["Description"] == null)
				{
					empty = "";
				}
				else
				{
					str = xNode.Attributes["Value"].Value.Trim();
					if (xNode.Attributes["Description"].Value.Trim().Length <= 25)
					{
						empty = xNode.Attributes["Description"].Value.Trim();
					}
					else
					{
						empty = (!xNode.Attributes["Description"].Value.Contains("\n") ? xNode.Attributes["Description"].Value.Trim() : xNode.Attributes["Description"].Value.Replace("\n", " "));
					}
				}
			}
			if (xNode.Attributes["Update"] == null)
			{
				str1 = "Unknown";
			}
			else
			{
				str1 = (xNode.Attributes["Update"].Value.Trim() == string.Empty ? "Unknown" : xNode.Attributes["Update"].Value.Trim());
			}
			strs.Add(empty);
			strs.Add(str1);
			strs.Add(empty1);
			strs.Add(str);
			return strs;
		}

		private string GetMemoPriority()
		{
			string str;
			try
			{
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"] == null)
				{
					str = "LOCAL";
				}
				else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"].ToString().ToUpper() == "ADMIN")
				{
					this.strMemoPriority = "ADMIN";
					str = this.strMemoPriority;
				}
				else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "MEMO_PRIORITY"].ToString().ToUpper() != "GLOBAL")
				{
					this.strMemoPriority = "LOCAL";
					str = this.strMemoPriority;
				}
				else
				{
					this.strMemoPriority = "GLOBAL";
					str = this.strMemoPriority;
				}
			}
			catch (Exception exception)
			{
				str = "LOCAL";
			}
			return str;
		}

		private void GetMemoStates()
		{
			try
			{
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "TEXTMEMO"] != null)
				{
					if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "TEXTMEMO"].ToString().ToUpper() == "DISABLED")
					{
						this.strTextMemoState = "DISABLED";
					}
					else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "TEXTMEMO"].ToString().ToUpper() != "FALSE")
					{
						this.strTextMemoState = "TRUE";
					}
					else
					{
						this.strTextMemoState = "FALSE";
					}
				}
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "REFERENCEMEMO"] != null)
				{
					if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "REFERENCEMEMO"].ToString().ToUpper() == "DISABLED")
					{
						this.strReferenceMemoState = "DISABLED";
					}
					else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "REFERENCEMEMO"].ToString().ToUpper() != "FALSE")
					{
						this.strReferenceMemoState = "TRUE";
					}
					else
					{
						this.strReferenceMemoState = "FALSE";
					}
				}
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "HYPERLINKMEMO"] != null)
				{
					if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "HYPERLINKMEMO"].ToString().ToUpper() == "DISABLED")
					{
						this.strHyperlinkMemoState = "DISABLED";
					}
					else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "HYPERLINKMEMO"].ToString().ToUpper() != "FALSE")
					{
						this.strHyperlinkMemoState = "TRUE";
					}
					else
					{
						this.strHyperlinkMemoState = "FALSE";
					}
				}
				if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "PROGRAMMEMO"] != null)
				{
					if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "PROGRAMMEMO"].ToString().ToUpper() == "DISABLED")
					{
						this.strProgramMemoState = "DISABLED";
					}
					else if (Program.iniServers[this.frmParent.frmParent.p_ServerId].items["MEMO_SETTING", "PROGRAMMEMO"].ToString().ToUpper() != "FALSE")
					{
						this.strProgramMemoState = "TRUE";
					}
					else
					{
						this.strProgramMemoState = "FALSE";
					}
				}
			}
			catch (Exception exception)
			{
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

		public string GetMultilingualValue(string text)
		{
			string str;
			string empty = string.Empty;
			try
			{
				string upper = text.ToUpper();
				string str1 = upper;
				if (upper == null)
				{
					empty = text;
					return empty;
				}
				else if (str1 == "TEXT")
				{
					empty = this.GetResource("Text", "TEXT_MEMO", ResourceType.LABEL);
				}
				else if (str1 == "REFERENCE")
				{
					empty = this.GetResource("Refrence", "REFERENCE_MEMO", ResourceType.LABEL);
				}
				else if (str1 == "HYPERLINK")
				{
					empty = this.GetResource("Hyperlink", "HYPERLINK_MEMO", ResourceType.LABEL);
				}
				else
				{
					if (str1 != "PROGRAM")
					{
						empty = text;
						return empty;
					}
					empty = this.GetResource("Program", "PROGRAM_MEMO", ResourceType.LABEL);
				}
				return empty;
			}
			catch (Exception exception)
			{
				str = text;
			}
			return str;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MEMO']");
				str = string.Concat(str, "/Screen[@Name='MEMO_TASKS']");
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
					resourceValue = this.frmParent.frmParent.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.frmParent.frmParent.GetResourceValue(sDefaultValue, str);
				}
			}
			catch (Exception exception)
			{
				resourceValue = sDefaultValue;
			}
			return resourceValue;
		}

		private int GetTreevieeItemHeight()
		{
			int height;
			try
			{
				System.Drawing.Font @default = Settings.Default.appFont;
				Graphics graphic = Graphics.FromImage(new Bitmap(2200, 2200));
				SizeF sizeF = new SizeF();
				sizeF = graphic.MeasureString("SAMPLE LINE", @default);
				height = (int)sizeF.Height * 3;
			}
			catch (Exception exception)
			{
				height = 45;
			}
			return height;
		}

		private void HighlightList(ref Label lbl)
		{
			if (lbl.Parent.Name.Equals(this.pnlTasks2.Name))
			{
				for (int i = 0; i < this.pnlTasks2.Controls.Count; i++)
				{
					if (this.pnlTasks2.Controls[i] == this.lblGlobalMemo | this.pnlTasks2.Controls[i] == this.lblLocalMemo | this.pnlTasks2.Controls[i] == this.lblAdminMemo)
					{
						this.pnlTasks2.Controls[i].BackColor = this.pnlTasks2.BackColor;
						this.pnlTasks2.Controls[i].ForeColor = this.pnlTasks2.ForeColor;
					}
				}
				lbl.BackColor = Settings.Default.appHighlightBackColor;
				lbl.ForeColor = Settings.Default.appHighlightForeColor;
			}
		}

		private void InitializeComponent()
		{
			this.pnlTasks = new Panel();
			this.pnlMemoTypes = new Panel();
			this.tvMemoTypes = new TreeView();
			this.pnlTasks2 = new Panel();
			this.lblAdminMemo = new Label();
			this.lblGlobalMemo = new Label();
			this.lblLocalMemo = new Label();
			this.lblView = new Label();
			this.lblSpace1 = new Label();
			this.lblTasksTitle = new Label();
			this.pnlTasks.SuspendLayout();
			this.pnlMemoTypes.SuspendLayout();
			this.pnlTasks2.SuspendLayout();
			base.SuspendLayout();
			this.pnlTasks.BackColor = Color.White;
			this.pnlTasks.BorderStyle = BorderStyle.FixedSingle;
			this.pnlTasks.Controls.Add(this.pnlMemoTypes);
			this.pnlTasks.Controls.Add(this.pnlTasks2);
			this.pnlTasks.Controls.Add(this.lblTasksTitle);
			this.pnlTasks.Dock = DockStyle.Fill;
			this.pnlTasks.ForeColor = Color.Black;
			this.pnlTasks.Location = new Point(0, 0);
			this.pnlTasks.Name = "pnlTasks";
			this.pnlTasks.Size = new System.Drawing.Size(151, 392);
			this.pnlTasks.TabIndex = 0;
			this.pnlMemoTypes.BackColor = Color.White;
			this.pnlMemoTypes.Controls.Add(this.tvMemoTypes);
			this.pnlMemoTypes.Dock = DockStyle.Fill;
			this.pnlMemoTypes.Location = new Point(0, 131);
			this.pnlMemoTypes.Name = "pnlMemoTypes";
			this.pnlMemoTypes.Size = new System.Drawing.Size(149, 259);
			this.pnlMemoTypes.TabIndex = 0;
			this.tvMemoTypes.BorderStyle = BorderStyle.None;
			this.tvMemoTypes.Dock = DockStyle.Fill;
			this.tvMemoTypes.Location = new Point(0, 0);
			this.tvMemoTypes.Name = "tvMemoTypes";
			this.tvMemoTypes.Size = new System.Drawing.Size(149, 259);
			this.tvMemoTypes.TabIndex = 0;
			this.tvMemoTypes.TabStop = false;
			this.tvMemoTypes.DrawNode += new DrawTreeNodeEventHandler(this.tvMemoTypes_DrawNode);
			this.tvMemoTypes.AfterSelect += new TreeViewEventHandler(this.tvMemoTypes_AfterSelect);
			this.tvMemoTypes.Click += new EventHandler(this.tvMemoTypes_Click);
			this.pnlTasks2.BackColor = Color.White;
			this.pnlTasks2.Controls.Add(this.lblAdminMemo);
			this.pnlTasks2.Controls.Add(this.lblGlobalMemo);
			this.pnlTasks2.Controls.Add(this.lblLocalMemo);
			this.pnlTasks2.Controls.Add(this.lblView);
			this.pnlTasks2.Controls.Add(this.lblSpace1);
			this.pnlTasks2.Dock = DockStyle.Top;
			this.pnlTasks2.Location = new Point(0, 27);
			this.pnlTasks2.Name = "pnlTasks2";
			this.pnlTasks2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 0);
			this.pnlTasks2.Size = new System.Drawing.Size(149, 104);
			this.pnlTasks2.TabIndex = 0;
			this.lblAdminMemo.Cursor = Cursors.Hand;
			this.lblAdminMemo.Dock = DockStyle.Top;
			this.lblAdminMemo.Location = new Point(15, 80);
			this.lblAdminMemo.Name = "lblAdminMemo";
			this.lblAdminMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblAdminMemo.Size = new System.Drawing.Size(119, 16);
			this.lblAdminMemo.TabIndex = 0;
			this.lblAdminMemo.Text = "Admin Memos";
			this.lblAdminMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblAdminMemo.Click += new EventHandler(this.lblAdminMemo_Click);
			this.lblGlobalMemo.Cursor = Cursors.Hand;
			this.lblGlobalMemo.Dock = DockStyle.Top;
			this.lblGlobalMemo.Location = new Point(15, 64);
			this.lblGlobalMemo.Name = "lblGlobalMemo";
			this.lblGlobalMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblGlobalMemo.Size = new System.Drawing.Size(119, 16);
			this.lblGlobalMemo.TabIndex = 0;
			this.lblGlobalMemo.Text = "Global Memos";
			this.lblGlobalMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblGlobalMemo.Click += new EventHandler(this.lblGlobalMemo_Click);
			this.lblLocalMemo.Cursor = Cursors.Hand;
			this.lblLocalMemo.Dock = DockStyle.Top;
			this.lblLocalMemo.Location = new Point(15, 48);
			this.lblLocalMemo.Name = "lblLocalMemo";
			this.lblLocalMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblLocalMemo.Size = new System.Drawing.Size(119, 16);
			this.lblLocalMemo.TabIndex = 0;
			this.lblLocalMemo.Text = "Local Memos";
			this.lblLocalMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblLocalMemo.Click += new EventHandler(this.lblLocalMemo_Click);
			this.lblView.BackColor = Color.Transparent;
			this.lblView.Dock = DockStyle.Top;
			this.lblView.ForeColor = Color.Blue;
			this.lblView.Image = Resources.GroupLine1;
			this.lblView.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblView.Location = new Point(15, 20);
			this.lblView.Name = "lblView";
			this.lblView.Size = new System.Drawing.Size(119, 28);
			this.lblView.TabIndex = 0;
			this.lblView.Text = "View Options";
			this.lblView.TextAlign = ContentAlignment.MiddleLeft;
			this.lblSpace1.Cursor = Cursors.Hand;
			this.lblSpace1.Dock = DockStyle.Top;
			this.lblSpace1.Location = new Point(15, 10);
			this.lblSpace1.Name = "lblSpace1";
			this.lblSpace1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblSpace1.Size = new System.Drawing.Size(119, 10);
			this.lblSpace1.TabIndex = 0;
			this.lblSpace1.TextAlign = ContentAlignment.MiddleLeft;
			this.lblTasksTitle.BackColor = Color.White;
			this.lblTasksTitle.Dock = DockStyle.Top;
			this.lblTasksTitle.ForeColor = Color.Black;
			this.lblTasksTitle.Location = new Point(0, 0);
			this.lblTasksTitle.Name = "lblTasksTitle";
			this.lblTasksTitle.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblTasksTitle.Size = new System.Drawing.Size(149, 27);
			this.lblTasksTitle.TabIndex = 0;
			this.lblTasksTitle.Text = "Tasks";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(151, 392);
			base.Controls.Add(this.pnlTasks);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmMemoTasks";
			base.Load += new EventHandler(this.frmMemoTasks_Load);
			this.pnlTasks.ResumeLayout(false);
			this.pnlMemoTypes.ResumeLayout(false);
			this.pnlTasks2.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void lblAdminMemo_Click(object sender, EventArgs e)
		{
			this.HighlightList(ref this.lblAdminMemo);
			this.frmParent.HideForms();
			this.frmParent.objFrmMemoAdmin.Show();
		}

		private void lblGlobalMemo_Click(object sender, EventArgs e)
		{
			if (this.intMemoType != 2)
			{
				this.frmParent.frmParent.LoadMemos();
				this.frmParent.xnlGlobalMemo = this.frmParent.frmParent.reloadGlobalMemos(this.frmParent.sPartNumber);
				this.frmParent.objFrmMemoGlobal.reloadGlobalMemos();
			}
			else
			{
				this.frmParent.frmParent.LoadMemos();
			}
			this.HighlightList(ref this.lblGlobalMemo);
			this.frmParent.HideForms();
			this.frmParent.objFrmMemoGlobal.Show();
		}

		private void lblLocalMemo_Click(object sender, EventArgs e)
		{
			this.HighlightList(ref this.lblLocalMemo);
			this.frmParent.HideForms();
			this.frmParent.objFrmMemoLocal.Show();
		}

		private void LoadMemo(bool bPictureLevel, bool bPartLevel)
		{
			string[] item;
			string[] strArrays;
			int i;
			if (bPictureLevel && bPartLevel)
			{
				this.tvMemoTypes.Nodes.Clear();
				this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
				this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
				TreeNode treeNode = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMO", ResourceType.LABEL))
				{
					Name = "LocalMemo"
				};
				TreeNode treeNode1 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL))
				{
					Name = "GlobalMemo"
				};
				TreeNode treeNode2 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMO", ResourceType.LABEL))
				{
					Name = "AdminMemo"
				};
				if (this.strMemoPriority.ToUpper().Trim() == "LOCAL")
				{
					this.tvMemoTypes.Nodes.Add(treeNode);
					this.tvMemoTypes.Nodes.Add(treeNode1);
					this.tvMemoTypes.Nodes.Add(treeNode2);
				}
				else if (this.strMemoPriority.ToUpper().Trim() == "GLOBAL")
				{
					this.tvMemoTypes.Nodes.Add(treeNode1);
					this.tvMemoTypes.Nodes.Add(treeNode);
					this.tvMemoTypes.Nodes.Add(treeNode2);
				}
				else if (this.strMemoPriority.ToUpper().Trim() == "ADMIN")
				{
					this.tvMemoTypes.Nodes.Add(treeNode2);
					this.tvMemoTypes.Nodes.Add(treeNode);
					this.tvMemoTypes.Nodes.Add(treeNode1);
				}
				Graphics graphic = this.tvMemoTypes.CreateGraphics();
				System.Drawing.Font font = this.tvMemoTypes.Font;
				try
				{
					if (Settings.Default.EnableLocalMemo && this.frmParent.xnlLocalMemo != null && this.frmParent.xnlLocalMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes in this.frmParent.xnlLocalMemo)
						{
							List<string> memoDetails = this.GetMemoDetails(xmlNodes);
							Guid guid = Guid.NewGuid();
							string empty = string.Empty;
							if (memoDetails[2].ToUpper() == "TEXT")
							{
								if (this.strTextMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty = "txt";
							}
							else if (memoDetails[2].ToUpper() == "REFERENCE")
							{
								if (this.strReferenceMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty = "ref";
							}
							else if (memoDetails[2].ToUpper() == "HYPERLINK")
							{
								if (this.strHyperlinkMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty = "hyp";
							}
							else if (memoDetails[2].ToUpper() == "PROGRAM")
							{
								if (this.strProgramMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty = "prg";
							}
							string outerXml = this.frmParent.objFrmMemoLocal.CreateMemoNode(empty, memoDetails[3], memoDetails[1]).OuterXml;
							if (empty == "hyp")
							{
								outerXml = this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(empty, memoDetails[3], memoDetails[1], memoDetails[0]).OuterXml;
							}
							this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(guid.ToString(), outerXml);
							string str = memoDetails[1].ToString();
							try
							{
								DateTime dateTime = DateTime.ParseExact(str, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
								item = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
								string[] strArrays1 = item;
								if (this.strDateFormat != string.Empty)
								{
									if (this.strDateFormat.ToUpper() != "INVALID")
									{
										strArrays = strArrays1;
										for (i = 0; i < (int)strArrays.Length; i++)
										{
											string str1 = strArrays[i];
											if (this.strDateFormat == str1)
											{
												str = dateTime.ToString(this.strDateFormat);
											}
										}
									}
									else
									{
										str = "Unknown";
									}
								}
							}
							catch
							{
							}
							memoDetails[1] = str;
							List<frmMemoTasks.TreeNodeText> treeNodeTexts = new List<frmMemoTasks.TreeNodeText>();
							string empty1 = string.Empty;
							if (empty != "hyp")
							{
								string[] item1 = new string[] { memoDetails[0], "\r\n", memoDetails[1], "\r\n", this.GetMultilingualValue(memoDetails[2]) };
								empty1 = string.Concat(item1);
							}
							else if (memoDetails[0].Length <= 25)
							{
								string[] item2 = new string[] { memoDetails[0], "\r\n", memoDetails[1], "\r\n", this.GetMultilingualValue(memoDetails[2]) };
								empty1 = string.Concat(item2);
							}
							else
							{
								string[] strArrays2 = new string[] { memoDetails[0].Substring(0, 25), "...\r\n", memoDetails[1], "\r\n", this.GetMultilingualValue(memoDetails[2]) };
								empty1 = string.Concat(strArrays2);
							}
							treeNodeTexts.Add(new frmMemoTasks.TreeNodeText(empty1));
							foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTexts)
							{
								if (memoDetails[0].Contains("|"))
								{
									memoDetails[0] = memoDetails[0].Replace("|", "PIPESIGN");
								}
								if (memoDetails[3].Contains("|"))
								{
									memoDetails[3] = memoDetails[3].Replace("|", "PIPESIGN");
								}
								TreeNode treeNode3 = new TreeNode();
								string[] item3 = new string[] { memoDetails[0], "|", memoDetails[1], "|", memoDetails[2], "|", memoDetails[3], "|", guid.ToString() };
								treeNode3.Name = string.Concat(item3);
								treeNode3.Tag = treeNodeText;
								treeNode3.Text = this.MaxWidthString(graphic, font, treeNodeText.TextList);
								this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(treeNode3);
							}
						}
					}
				}
				catch (Exception exception)
				{
				}
				try
				{
					if (Settings.Default.EnableGlobalMemo && this.frmParent.xnlGlobalMemo != null && this.frmParent.xnlGlobalMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes1 in this.frmParent.xnlGlobalMemo)
						{
							List<string> strs = this.GetMemoDetails(xmlNodes1);
							Guid guid1 = Guid.NewGuid();
							string empty2 = string.Empty;
							if (strs[2].ToUpper() == "TEXT")
							{
								if (this.strTextMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty2 = "txt";
							}
							else if (strs[2].ToUpper() == "REFERENCE")
							{
								if (this.strReferenceMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty2 = "ref";
							}
							else if (strs[2].ToUpper() == "HYPERLINK")
							{
								if (this.strHyperlinkMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty2 = "hyp";
							}
							else if (strs[2].ToUpper() == "PROGRAM")
							{
								if (this.strProgramMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
								empty2 = "prg";
							}
							string outerXml1 = this.frmParent.objFrmMemoGlobal.CreateMemoNode(empty2, strs[3], strs[1]).OuterXml;
							if (empty2 == "hyp")
							{
								outerXml1 = this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(empty2, strs[3], strs[1], strs[0]).OuterXml;
							}
							this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(guid1.ToString(), outerXml1);
							string str2 = strs[1].ToString();
							try
							{
								DateTime dateTime1 = DateTime.ParseExact(str2, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
								string[] strArrays3 = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
								if (this.strDateFormat != string.Empty)
								{
									if (this.strDateFormat.ToUpper() != "INVALID")
									{
										string[] strArrays4 = strArrays3;
										for (int j = 0; j < (int)strArrays4.Length; j++)
										{
											string str3 = strArrays4[j];
											if (this.strDateFormat == str3)
											{
												str2 = dateTime1.ToString(this.strDateFormat);
											}
										}
									}
									else
									{
										str2 = "Unknown";
									}
								}
							}
							catch
							{
							}
							strs[1] = str2;
							List<frmMemoTasks.TreeNodeText> treeNodeTexts1 = new List<frmMemoTasks.TreeNodeText>();
							string empty3 = string.Empty;
							if (empty2 != "hyp")
							{
								string[] item4 = new string[] { strs[0], "\r\n", strs[1], "\r\n", this.GetMultilingualValue(strs[2]) };
								empty3 = string.Concat(item4);
							}
							else if (strs[0].Length <= 25)
							{
								string[] item5 = new string[] { strs[0], "\r\n", strs[1], "\r\n", this.GetMultilingualValue(strs[2]) };
								empty3 = string.Concat(item5);
							}
							else
							{
								string[] strArrays5 = new string[] { strs[0].Substring(0, 25), "...\r\n", strs[1], "\r\n", this.GetMultilingualValue(strs[2]) };
								empty3 = string.Concat(strArrays5);
							}
							treeNodeTexts1.Add(new frmMemoTasks.TreeNodeText(empty3));
							foreach (frmMemoTasks.TreeNodeText treeNodeText1 in treeNodeTexts1)
							{
								if (strs[0].Contains("|"))
								{
									strs[0] = strs[0].Replace("|", "PIPESIGN");
								}
								if (strs[3].Contains("|"))
								{
									strs[3] = strs[3].Replace("|", "PIPESIGN");
								}
								TreeNode treeNode4 = new TreeNode();
								item = new string[] { strs[0], "|", strs[1], "|", strs[2], "|", strs[3], "|", guid1.ToString() };
								treeNode4.Name = string.Concat(item);
								treeNode4.Tag = treeNodeText1;
								treeNode4.Text = this.MaxWidthString(graphic, font, treeNodeText1.TextList);
								this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(treeNode4);
							}
						}
					}
				}
				catch (Exception exception1)
				{
				}
				try
				{
					if (Settings.Default.EnableAdminMemo && this.frmParent.xnladminMemo != null && this.frmParent.xnladminMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes2 in this.frmParent.xnladminMemo)
						{
							List<string> memoDetails1 = this.GetMemoDetails(xmlNodes2);
							if (memoDetails1[2].ToUpper() == "TEXT")
							{
								if (this.strTextMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails1[2].ToUpper() == "REFERENCE")
							{
								if (this.strReferenceMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails1[2].ToUpper() == "HYPERLINK")
							{
								if (this.strHyperlinkMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails1[2].ToUpper() == "PROGRAM" && this.strProgramMemoState.ToUpper() == "FALSE")
							{
								continue;
							}
							string str4 = memoDetails1[1].ToString();
							try
							{
								DateTime dateTime2 = DateTime.ParseExact(str4, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
								item = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
								string[] strArrays6 = item;
								if (this.strDateFormat != string.Empty)
								{
									if (this.strDateFormat.ToUpper() != "INVALID")
									{
										strArrays = strArrays6;
										for (i = 0; i < (int)strArrays.Length; i++)
										{
											string str5 = strArrays[i];
											if (this.strDateFormat == str5)
											{
												str4 = dateTime2.ToString(this.strDateFormat);
											}
										}
									}
									else
									{
										str4 = "Unknown";
									}
								}
							}
							catch
							{
							}
							memoDetails1[1] = str4;
							List<frmMemoTasks.TreeNodeText> treeNodeTexts2 = new List<frmMemoTasks.TreeNodeText>();
							item = new string[] { memoDetails1[0], "\r\n", memoDetails1[1], "\r\n", this.GetMultilingualValue(memoDetails1[2]) };
							treeNodeTexts2.Add(new frmMemoTasks.TreeNodeText(string.Concat(item)));
							foreach (frmMemoTasks.TreeNodeText treeNodeText2 in treeNodeTexts2)
							{
								TreeNode treeNode5 = new TreeNode();
								item = new string[] { memoDetails1[0], "|", memoDetails1[1], "|", memoDetails1[2], "|", memoDetails1[3] };
								treeNode5.Name = string.Concat(item);
								treeNode5.Tag = treeNodeText2;
								treeNode5.Text = this.MaxWidthString(graphic, font, treeNodeText2.TextList);
								this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Add(treeNode5);
							}
						}
					}
				}
				catch (Exception exception2)
				{
				}
			}
			if (this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Count > 0 && Settings.Default.EnableLocalMemo && this.strMemoPriority.ToUpper().Trim() == "LOCAL")
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["LocalMemo"].Nodes[0];
				return;
			}
			if (this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Count > 0 && Settings.Default.EnableGlobalMemo && this.strMemoPriority.ToUpper().Trim() == "GLOBAL")
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["GlobalMemo"].Nodes[0];
				return;
			}
			if (this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Count > 0 && Settings.Default.EnableAdminMemo && this.strMemoPriority.ToUpper().Trim() == "ADMIN")
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["AdminMemo"].Nodes[0];
				return;
			}
			if (this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Count > 0 && Settings.Default.EnableLocalMemo)
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["LocalMemo"].Nodes[0];
				return;
			}
			if (this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Count > 0 && Settings.Default.EnableGlobalMemo)
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["GlobalMemo"].Nodes[0];
				return;
			}
			if (this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Count > 0 && Settings.Default.EnableAdminMemo)
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["AdminMemo"].Nodes[0];
				return;
			}
			if (!this.frmParent.frmParent.bFromPopup)
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes[0];
			}
			else
			{
				try
				{
					if (this.frmParent != null)
					{
						this.frmParent.bNoMemos = true;
					}
				}
				catch (Exception exception3)
				{
					this.frmParent.bNoMemos = true;
					base.Close();
				}
			}
		}

		private string MaxWidthString(Graphics graphics, System.Drawing.Font font, List<string> list)
		{
			int width = 0;
			string empty = string.Empty;
			foreach (string str in list)
			{
				TextRenderer.DrawText(graphics, str, font, new Point(0, 0), Color.Black);
				System.Drawing.Size size = TextRenderer.MeasureText(graphics, str, font);
				if (width >= size.Width)
				{
					continue;
				}
				width = size.Width;
				empty = str;
			}
			return empty;
		}

		private void SetControlsText()
		{
			this.lblTasksTitle.Text = this.GetResource("Tasks", "TASKS", ResourceType.LABEL);
			this.lblView.Text = this.GetResource("View Options", "VIEW_OPTIONS", ResourceType.LABEL);
			this.lblLocalMemo.Text = this.GetResource("Local Memos", "LOCAL_MEMO", ResourceType.LABEL);
			this.lblGlobalMemo.Text = this.GetResource("Global Memo", "GLOBAL_MEMO", ResourceType.LABEL);
			this.lblAdminMemo.Text = this.GetResource("Admin Memos", "ADMIN_MEMO", ResourceType.LABEL);
		}

		public void ShowTask(MemoTasks task)
		{
			switch (task)
			{
				case MemoTasks.Local:
				{
					this.lblLocalMemo_Click(null, null);
					return;
				}
				case MemoTasks.Global:
				{
					this.lblGlobalMemo_Click(null, null);
					return;
				}
				case MemoTasks.Admin:
				{
					this.lblAdminMemo_Click(null, null);
					return;
				}
				default:
				{
					return;
				}
			}
		}

		private void tvMemoTypes_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				this.tvMemoTypes.Refresh();
				if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "LOCALMEMO")
				{
					if (Settings.Default.EnableLocalMemo)
					{
						this.frmParent.objFrmMemoLocal.ClearItems(true, false, false);
						this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
						this.frmParent.objFrmMemoLocal.pnlBottom.Top = 33;
						this.frmParent.objFrmMemoLocal.Show();
						this.lblLocalMemo_Click(null, null);
						if (this.frmParent.objFrmMemoLocal.tsbAddTxtMemo.Checked && this.strTextMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
							this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
							this.frmParent.objFrmMemoLocal.tsbAddTxtMemo.Checked = true;
						}
						else if (this.frmParent.objFrmMemoLocal.tsbAddRefMemo.Checked && this.strReferenceMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("REFRENCE");
							this.frmParent.objFrmMemoLocal.tsbAddRefMemo.Checked = true;
							this.frmParent.objFrmMemoLocal.pnlRefMemo.BringToFront();
						}
						else if (this.frmParent.objFrmMemoLocal.tsbAddHypMemo.Checked && this.strHyperlinkMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
							this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
							this.frmParent.objFrmMemoLocal.tsbAddHypMemo.Checked = true;
						}
						else if (this.frmParent.objFrmMemoLocal.tsbAddPrgMemo.Checked && this.strProgramMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("PROGRAME");
							this.frmParent.objFrmMemoLocal.pnlPrgMemo.BringToFront();
							this.frmParent.objFrmMemoLocal.tsbAddPrgMemo.Checked = true;
						}
						else if (this.strTextMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
							this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
							this.frmParent.objFrmMemoLocal.tsbAddTxtMemo.Checked = true;
						}
						else if (this.strReferenceMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("REFRENCE");
							this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoLocal.tsbAddRefMemo.Checked = true;
							this.frmParent.objFrmMemoLocal.pnlRefMemo.BringToFront();
						}
						else if (this.strHyperlinkMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
							this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoLocal.tsbAddHypMemo.Checked = true;
							this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
						}
						else if (this.strProgramMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoLocal.SetTabProperty("PROGRAME");
							this.frmParent.objFrmMemoLocal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoLocal.tsbAddPrgMemo.Checked = true;
							this.frmParent.objFrmMemoLocal.pnlPrgMemo.BringToFront();
						}
					}
					else
					{
						this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
						this.bDisabledMemo = true;
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "GLOBALMEMO")
				{
					if (Settings.Default.EnableGlobalMemo)
					{
						this.frmParent.objFrmMemoGlobal.ClearItems(true, false, false);
						this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
						this.frmParent.objFrmMemoGlobal.pnlBottom.Top = 33;
						this.frmParent.objFrmMemoGlobal.Show();
						this.lblGlobalMemo_Click(null, null);
						if (this.frmParent.objFrmMemoGlobal.tsbAddTxtMemo.Checked && this.strTextMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
							this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
							this.frmParent.objFrmMemoGlobal.tsbAddTxtMemo.Checked = true;
						}
						else if (this.frmParent.objFrmMemoGlobal.tsbAddRefMemo.Checked && this.strReferenceMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("REFRENCE");
							this.frmParent.objFrmMemoGlobal.pnlRefMemo.BringToFront();
							this.frmParent.objFrmMemoGlobal.tsbAddRefMemo.Checked = true;
						}
						else if (this.frmParent.objFrmMemoGlobal.tsbAddHypMemo.Checked && this.strHyperlinkMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
							this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
							this.frmParent.objFrmMemoGlobal.tsbAddHypMemo.Checked = true;
						}
						else if (this.frmParent.objFrmMemoGlobal.tsbAddPrgMemo.Checked && this.strProgramMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("PROGRAME");
							this.frmParent.objFrmMemoGlobal.pnlPrgMemo.BringToFront();
							this.frmParent.objFrmMemoGlobal.tsbAddPrgMemo.Checked = true;
						}
						else if (this.strTextMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
							this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
							this.frmParent.objFrmMemoGlobal.tsbAddTxtMemo.Checked = true;
						}
						else if (this.strReferenceMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("REFRENCE");
							this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoGlobal.tsbAddRefMemo.Checked = true;
							this.frmParent.objFrmMemoGlobal.pnlRefMemo.BringToFront();
						}
						else if (this.strHyperlinkMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
							this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoGlobal.tsbAddHypMemo.Checked = true;
							this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
						}
						else if (this.strProgramMemoState.ToUpper() == "TRUE")
						{
							this.frmParent.objFrmMemoGlobal.SetTabProperty("PROGRAME");
							this.frmParent.objFrmMemoGlobal.ClearItems(false, false, true);
							this.frmParent.objFrmMemoGlobal.tsbAddPrgMemo.Checked = true;
							this.frmParent.objFrmMemoGlobal.pnlPrgMemo.BringToFront();
						}
					}
					else
					{
						this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
						this.bDisabledMemo = true;
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "ADMINMEMO")
				{
					if (Settings.Default.EnableAdminMemo)
					{
						this.frmParent.objFrmMemoAdmin.ClearItems(true, false, false);
						this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
						this.frmParent.objFrmMemoAdmin.pnlBottom.Top = 33;
						this.frmParent.objFrmMemoAdmin.Show();
						this.lblAdminMemo_Click(null, null);
						if (this.frmParent.objFrmMemoAdmin.tsbAddHypMemo.Checked && this.strHyperlinkMemoState.ToUpper() == "TRUE")
						{
							if (this.strHyperlinkMemoState.ToUpper() == "DISABLED")
							{
								this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
								this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
							}
							this.frmParent.objFrmMemoAdmin.SetTabProperty("HYPERLINK");
							this.frmParent.objFrmMemoAdmin.pnlHypMemo.BringToFront();
						}
						else if (this.frmParent.objFrmMemoAdmin.tsbAddPrgMemo.Checked && this.strProgramMemoState.ToUpper() == "TRUE")
						{
							if (this.strProgramMemoState.ToUpper() == "DISABLED")
							{
								this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
								this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
							}
							this.frmParent.objFrmMemoAdmin.SetTabProperty("PROGRAME");
							this.frmParent.objFrmMemoAdmin.pnlPrgMemo.BringToFront();
						}
						else if (this.frmParent.objFrmMemoAdmin.tsbAddRefMemo.Checked && this.strReferenceMemoState.ToUpper() == "TRUE")
						{
							if (this.strReferenceMemoState.ToUpper() == "DISABLED")
							{
								this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
								this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
							}
							this.frmParent.objFrmMemoAdmin.SetTabProperty("REFRENCE");
							this.frmParent.objFrmMemoAdmin.pnlRefMemo.BringToFront();
						}
						else if (this.frmParent.objFrmMemoAdmin.tsbAddTxtMemo.Checked && this.strTextMemoState.ToUpper() == "TRUE")
						{
							if (this.strTextMemoState.ToUpper() == "DISABLED")
							{
								this.frmParent.objFrmMemoAdmin.tsbSave.Enabled = false;
								this.frmParent.objFrmMemoAdmin.tsbSaveAll.Enabled = false;
							}
							this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
							this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
							this.frmParent.objFrmMemoAdmin.tsbAddTxtMemo.Checked = true;
						}
					}
					else
					{
						this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
						this.bDisabledMemo = true;
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "LOCALMEMO")
				{
					this.frmParent.objFrmMemoLocal.ClearItems(true, false, true);
					this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
					this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
					string[] strArrays = this.tvMemoTypes.SelectedNode.Name.Split(new char[] { '|' });
					string empty = string.Empty;
					string str = strArrays[1];
					string str1 = strArrays[2];
					if (str1.ToUpper().Trim() != "HYPERLINK")
					{
						empty = strArrays[0];
					}
					else
					{
						string str2 = strArrays[4];
						string item = this.frmParent.objFrmMemoLocal.dicLocalMemoList[str2];
						try
						{
							XmlDocument xmlDocument = new XmlDocument();
							XmlNode xmlNodes = xmlDocument.ReadNode(new XmlTextReader(new StringReader(item)));
							empty = xmlNodes.Attributes["Description"].Value.ToString();
						}
						catch (Exception exception)
						{
						}
					}
					string str3 = strArrays[3];
					if (empty.ToUpper().Contains("PIPESIGN"))
					{
						empty = empty.Replace("PIPESIGN", "|");
					}
					if (str3.ToUpper().Contains("PIPESIGN"))
					{
						str3 = str3.Replace("PIPESIGN", "|");
					}
					if ((int)strArrays.Length > 4 && str1.ToUpper() == "PROGRAM")
					{
						str3 = string.Concat(str3, "|", strArrays[4]);
					}
					if (str1.ToUpper().Trim() == "TEXT" && this.strTextMemoState.ToUpper() != "FALSE")
					{
						if (this.strTextMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.rtbTxtMemo.Text = str3;
						this.frmParent.objFrmMemoLocal.lblTxtMemoDate.Text = str;
						this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
					}
					else if (str1.ToUpper().Trim() == "REFERENCE" && this.strReferenceMemoState.ToUpper() != "FALSE")
					{
						if (this.strReferenceMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoLocal.SetTabProperty("REFRENCE");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.lblRefMemoDate.Text = str;
						this.frmParent.objFrmMemoLocal.pnlRefMemo.BringToFront();
						string[] strArrays1 = new string[] { " " };
						string[] strArrays2 = str3.Split(strArrays1, StringSplitOptions.None);
						if ((int)strArrays2.Length < 2)
						{
							this.frmParent.objFrmMemoLocal.pnlError.BringToFront();
							return;
						}
						else
						{
							this.frmParent.objFrmMemoLocal.txtRefMemoServerKey.Text = strArrays2[0];
							this.frmParent.objFrmMemoLocal.txtRefMemoBookId.Text = strArrays2[1];
							this.frmParent.objFrmMemoLocal.txtRefMemoOtherRef.Text = string.Empty;
							for (int i = 2; i < (int)strArrays2.Length; i++)
							{
								TextBox textBox = this.frmParent.objFrmMemoLocal.txtRefMemoOtherRef;
								textBox.Text = string.Concat(textBox.Text, strArrays2[i]);
								if (i < (int)strArrays2.Length - 1)
								{
									TextBox textBox1 = this.frmParent.objFrmMemoLocal.txtRefMemoOtherRef;
									textBox1.Text = string.Concat(textBox1.Text, " ");
								}
							}
						}
					}
					else if (str1.ToUpper().Trim() == "HYPERLINK" && this.strHyperlinkMemoState.ToUpper() != "FALSE")
					{
						if (this.strHyperlinkMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
						this.frmParent.objFrmMemoLocal.txtDescription.Text = empty;
						this.frmParent.objFrmMemoLocal.txtHypMemoUrl.Text = str3;
						this.frmParent.objFrmMemoLocal.lblHypMemoDate.Text = str;
					}
					else if (str1.ToUpper().Trim() == "PROGRAM" && this.strProgramMemoState.ToUpper() != "FALSE")
					{
						if (this.strProgramMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoLocal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoLocal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoLocal.SetTabProperty("PROGRAME");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.pnlPrgMemo.BringToFront();
						this.frmParent.objFrmMemoLocal.lblPrgMemoDate.Text = str;
						string[] strArrays3 = new string[] { "|" };
						string[] strArrays4 = str3.Split(strArrays3, StringSplitOptions.None);
						this.frmParent.objFrmMemoLocal.txtPrgMemoExePath.Text = strArrays4[0];
						if ((int)strArrays4.Length > 1)
						{
							this.frmParent.objFrmMemoLocal.txtPrgMemoCmdLine.Text = strArrays4[1];
						}
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "GLOBALMEMO")
				{
					this.frmParent.objFrmMemoGlobal.ClearItems(true, false, true);
					this.frmParent.objFrmMemoGlobal.Show();
					this.lblGlobalMemo_Click(null, null);
					this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
					this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
					string[] strArrays5 = this.tvMemoTypes.SelectedNode.Name.Split(new char[] { '|' });
					string empty1 = string.Empty;
					string str4 = strArrays5[1];
					string str5 = strArrays5[2];
					if (str5.ToUpper().Trim() != "HYPERLINK")
					{
						empty1 = strArrays5[0];
					}
					else
					{
						string str6 = strArrays5[4];
						string item1 = this.frmParent.objFrmMemoGlobal.dicGlobalMemoList[str6];
						try
						{
							XmlDocument xmlDocument1 = new XmlDocument();
							XmlNode xmlNodes1 = xmlDocument1.ReadNode(new XmlTextReader(new StringReader(item1)));
							empty1 = xmlNodes1.Attributes["Description"].Value.ToString();
						}
						catch (Exception exception1)
						{
						}
					}
					string str7 = strArrays5[3];
					if (empty1.ToUpper().Contains("PIPESIGN"))
					{
						empty1 = empty1.Replace("PIPESIGN", "|");
					}
					if (str7.ToUpper().Contains("PIPESIGN"))
					{
						str7 = str7.Replace("PIPESIGN", "|");
					}
					if ((int)strArrays5.Length > 4 && str5.ToUpper() == "PROGRAM")
					{
						str7 = string.Concat(str7, "|", strArrays5[4]);
					}
					if (str5.ToUpper().Trim() == "TEXT" && this.strTextMemoState.ToUpper() != "FALSE")
					{
						if (this.strTextMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.rtbTxtMemo.Text = str7;
						this.frmParent.objFrmMemoGlobal.lblTxtMemoDate.Text = str4;
						this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
					}
					else if (str5.ToUpper().Trim() == "REFERENCE" && this.strReferenceMemoState.ToUpper() != "FALSE")
					{
						if (this.strReferenceMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoGlobal.SetTabProperty("REFRENCE");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.lblRefMemoDate.Text = str4;
						this.frmParent.objFrmMemoGlobal.pnlRefMemo.BringToFront();
						string[] strArrays6 = new string[] { " " };
						string[] strArrays7 = str7.Split(strArrays6, StringSplitOptions.None);
						if ((int)strArrays7.Length < 2)
						{
							this.frmParent.objFrmMemoGlobal.pnlError.BringToFront();
							return;
						}
						else
						{
							this.frmParent.objFrmMemoGlobal.txtRefMemoServerKey.Text = strArrays7[0];
							this.frmParent.objFrmMemoGlobal.txtRefMemoBookId.Text = strArrays7[1];
							this.frmParent.objFrmMemoGlobal.txtRefMemoOtherRef.Text = string.Empty;
							for (int j = 2; j < (int)strArrays7.Length; j++)
							{
								TextBox textBox2 = this.frmParent.objFrmMemoGlobal.txtRefMemoOtherRef;
								textBox2.Text = string.Concat(textBox2.Text, strArrays7[j]);
								if (j < (int)strArrays7.Length - 1)
								{
									TextBox textBox3 = this.frmParent.objFrmMemoGlobal.txtRefMemoOtherRef;
									textBox3.Text = string.Concat(textBox3.Text, " ");
								}
							}
						}
					}
					else if (str5.ToUpper().Trim() == "HYPERLINK" && this.strHyperlinkMemoState.ToUpper() != "FALSE")
					{
						if (this.strHyperlinkMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
						this.frmParent.objFrmMemoGlobal.txtDescription.Text = empty1;
						this.frmParent.objFrmMemoGlobal.txtHypMemoUrl.Text = str7;
						this.frmParent.objFrmMemoGlobal.lblHypMemoDate.Text = str4;
					}
					else if (str5.ToUpper().Trim() == "PROGRAM" && this.strProgramMemoState.ToUpper() != "FALSE")
					{
						if (this.strProgramMemoState.ToUpper() != "DISABLED")
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = true;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = true;
						}
						else
						{
							this.frmParent.objFrmMemoGlobal.tsbSave.Enabled = false;
							this.frmParent.objFrmMemoGlobal.tsbSaveAll.Enabled = false;
						}
						this.frmParent.objFrmMemoGlobal.SetTabProperty("PROGRAME");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.pnlPrgMemo.BringToFront();
						this.frmParent.objFrmMemoGlobal.lblPrgMemoDate.Text = str4;
						string[] strArrays8 = new string[] { "|" };
						string[] strArrays9 = str7.Split(strArrays8, StringSplitOptions.None);
						this.frmParent.objFrmMemoGlobal.txtPrgMemoExePath.Text = strArrays9[0];
						if ((int)strArrays9.Length > 1)
						{
							this.frmParent.objFrmMemoGlobal.txtPrgMemoCmdLine.Text = strArrays9[1];
						}
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "ADMINMEMO")
				{
					this.frmParent.objFrmMemoAdmin.ClearItems(true, false, true);
					this.lblAdminMemo_Click(null, null);
					this.frmParent.objFrmMemoAdmin.Show();
					this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
					this.frmParent.objFrmMemoAdmin.pnlBottom.Dock = DockStyle.Fill;
					string[] strArrays10 = this.tvMemoTypes.SelectedNode.Name.Split(new char[] { '|' });
					string str8 = strArrays10[0];
					string str9 = strArrays10[1];
					string str10 = strArrays10[2];
					string str11 = strArrays10[3];
					if ((int)strArrays10.Length > 4 && str10.ToUpper() == "PROGRAM")
					{
						str11 = string.Concat(str11, "|", strArrays10[4]);
					}
					if (str10.ToUpper().Trim() == "TEXT" && this.strTextMemoState.ToUpper() != "FALSE")
					{
						this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoAdmin.Show();
						this.frmParent.objFrmMemoAdmin.rtbTxtMemo.Text = str11;
						this.frmParent.objFrmMemoAdmin.lblTxtMemoDate.Text = str9;
						this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
					}
					else if (str10.ToUpper().Trim() == "REFERENCE" && this.strReferenceMemoState.ToUpper() != "FALSE")
					{
						this.frmParent.objFrmMemoAdmin.SetTabProperty("REFRENCE");
						this.frmParent.objFrmMemoAdmin.Show();
						this.frmParent.objFrmMemoAdmin.lblRefMemoDate.Text = str9;
						this.frmParent.objFrmMemoAdmin.pnlRefMemo.BringToFront();
						string[] strArrays11 = new string[] { " " };
						string[] strArrays12 = str11.Split(strArrays11, StringSplitOptions.None);
						if ((int)strArrays12.Length < 2)
						{
							this.frmParent.objFrmMemoAdmin.pnlError.BringToFront();
							return;
						}
						else
						{
							this.frmParent.objFrmMemoAdmin.txtRefMemoServerKey.Text = strArrays12[0];
							this.frmParent.objFrmMemoAdmin.txtRefMemoBookId.Text = strArrays12[1];
							this.frmParent.objFrmMemoAdmin.txtRefMemoOtherRef.Text = string.Empty;
							for (int k = 2; k < (int)strArrays12.Length; k++)
							{
								TextBox textBox4 = this.frmParent.objFrmMemoAdmin.txtRefMemoOtherRef;
								textBox4.Text = string.Concat(textBox4.Text, strArrays12[k]);
								if (k < (int)strArrays12.Length - 1)
								{
									TextBox textBox5 = this.frmParent.objFrmMemoAdmin.txtRefMemoOtherRef;
									textBox5.Text = string.Concat(textBox5.Text, " ");
								}
							}
						}
					}
					else if (str10.ToUpper().Trim() == "HYPERLINK" && this.strHyperlinkMemoState.ToUpper() != "FALSE")
					{
						this.frmParent.objFrmMemoAdmin.SetTabProperty("HYPERLINK");
						this.frmParent.objFrmMemoAdmin.Show();
						this.frmParent.objFrmMemoAdmin.pnlHypMemo.BringToFront();
						this.frmParent.objFrmMemoAdmin.txtHypMemoUrl.Text = str11;
						this.frmParent.objFrmMemoAdmin.txtDescription.Text = str8;
						this.frmParent.objFrmMemoAdmin.lblHypMemoDate.Text = str9;
					}
					else if (str10.ToUpper().Trim() == "PROGRAM" && this.strProgramMemoState.ToUpper() != "FALSE")
					{
						this.frmParent.objFrmMemoAdmin.SetTabProperty("PROGRAME");
						this.frmParent.objFrmMemoAdmin.Show();
						this.frmParent.objFrmMemoAdmin.pnlPrgMemo.BringToFront();
						this.frmParent.objFrmMemoAdmin.lblPrgMemoDate.Text = str9;
						string[] strArrays13 = new string[] { "|" };
						string[] strArrays14 = str11.Split(strArrays13, StringSplitOptions.None);
						this.frmParent.objFrmMemoAdmin.txtPrgMemoExePath.Text = strArrays14[0];
						if ((int)strArrays14.Length > 1)
						{
							this.frmParent.objFrmMemoAdmin.txtPrgMemoCmdLine.Text = strArrays14[1];
						}
					}
				}
				this.tvMemoTypes.SelectedNode.EnsureVisible();
			}
			catch (Exception exception2)
			{
			}
		}

		private void tvMemoTypes_Click(object sender, EventArgs e)
		{
			this.tnPreviouisNode = this.tvMemoTypes.SelectedNode;
		}

		private void tvMemoTypes_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			string str = (e.Node.Tag == null ? e.Node.Text : ((frmMemoTasks.TreeNodeText)e.Node.Tag).DisplayText);
			System.Drawing.Font nodeFont = e.Node.NodeFont ?? e.Node.TreeView.Font;
			Color foreColor = e.Node.ForeColor;
			if (foreColor == Color.Empty)
			{
				foreColor = e.Node.TreeView.ForeColor;
			}
			if (e.Node != e.Node.TreeView.SelectedNode)
			{
				TextRenderer.DrawText(e.Graphics, str, nodeFont, e.Bounds, foreColor, TextFormatFlags.VerticalCenter);
				return;
			}
			int x = e.Bounds.X;
			int y = e.Bounds.Y;
			Rectangle bounds = this.tvMemoTypes.SelectedNode.Bounds;
			Rectangle rectangle = e.Bounds;
			Rectangle rectangle1 = new Rectangle(x, y, bounds.Width + 15, rectangle.Height);
			foreColor = SystemColors.HighlightText;
			e.Graphics.FillRectangle(new SolidBrush(Settings.Default.appHighlightBackColor), rectangle1);
			TextRenderer.DrawText(e.Graphics, str, nodeFont, e.Bounds, Settings.Default.appHighlightForeColor, TextFormatFlags.VerticalCenter);
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblTasksTitle.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}

		public void UpdateMemoToTree(string type, string value, string strMemoTyp, string strHypDescription)
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			empty = (value.Length <= 25 ? value : string.Concat(value.Substring(0, 25), "..."));
			if (!(type.ToUpper() == "TXT") && !(type.ToUpper() == "HYP") && empty.Contains("|"))
			{
				empty = empty.Replace("|", " ");
			}
			if (type.ToUpper().Equals("TXT"))
			{
				str = "Text";
			}
			else if (type.ToUpper().Equals("REF"))
			{
				str = "Reference";
			}
			else if (!type.ToUpper().Equals("HYP"))
			{
				str = (!type.ToUpper().Equals("PRG") ? "Unknown" : "Program");
			}
			else
			{
				str = "Hyperlink";
			}
			if (type == "hyp")
			{
				empty = (strHypDescription.Length <= 25 ? strHypDescription : string.Concat(strHypDescription.Substring(0, 25), "..."));
			}
			empty1 = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
			string str1 = string.Empty;
			string empty2 = string.Empty;
			string str2 = string.Empty;
			try
			{
				if (type == "prg")
				{
					string name = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name;
					char[] chrArray = new char[] { '|' };
					str1 = name.Split(chrArray)[4];
					string name1 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name;
					char[] chrArray1 = new char[] { '|' };
					empty2 = name1.Split(chrArray1)[3];
					string name2 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name;
					char[] chrArray2 = new char[] { '|' };
					str2 = name2.Split(chrArray2)[0];
				}
				else
				{
					string name3 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name;
					char[] chrArray3 = new char[] { '|' };
					str1 = name3.Split(chrArray3)[4];
					string str3 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name;
					char[] chrArray4 = new char[] { '|' };
					empty2 = str3.Split(chrArray4)[3];
					string name4 = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Name;
					char[] chrArray5 = new char[] { '|' };
					str2 = name4.Split(chrArray5)[0];
				}
			}
			catch (Exception exception)
			{
			}
			if (str2.ToUpper().Contains("PIPESIGN"))
			{
				str2 = str2.Replace("PIPESIGN", "|");
			}
			if (empty2.ToUpper().Contains("PIPESIGN"))
			{
				empty2 = empty2.Replace("PIPESIGN", "|");
			}
			bool flag = false;
			if (type == "hyp" && str2 != strHypDescription)
			{
				flag = true;
			}
			if (value != empty2 || flag)
			{
				string empty3 = string.Empty;
				if (strMemoTyp == "LocalMemo")
				{
					if (!this.frmParent.objFrmMemoLocal.bSaveMemoOnBookLevel || !this.frmParent.objFrmMemoLocal.chkSaveBookLevelMemos.Checked || !(this.frmParent.sPartNumber != string.Empty))
					{
						empty3 = (type != "hyp" ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
						this.frmParent.objFrmMemoLocal.dicLocalMemoList.Remove(str1.ToString());
						this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(str1.ToString(), empty3);
					}
					else
					{
						for (int i = 0; i < this.frmParent.objFrmMemoLocal.lstResults.Count; i++)
						{
							empty3 = (type != "hyp" ? this.frmParent.objFrmMemoLocal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoLocal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
							this.frmParent.objFrmMemoLocal.dicLocalMemoList.Remove(str1.ToString());
							this.frmParent.objFrmMemoLocal.dicLocalMemoList.Add(str1.ToString(), empty3);
							this.frmParent.objFrmMemoLocal.iMemoResultCounter++;
						}
					}
				}
				else if (!this.frmParent.objFrmMemoGlobal.bSaveMemoOnBookLevel || !this.frmParent.objFrmMemoGlobal.chkSaveBookLevelMemos.Checked || !(this.frmParent.sPartNumber != string.Empty))
				{
					empty3 = (type != "hyp" ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
					this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Remove(str1.ToString());
					this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(str1.ToString(), empty3);
				}
				else
				{
					for (int j = 0; j < this.frmParent.objFrmMemoGlobal.lstResults.Count; j++)
					{
						empty3 = (type != "hyp" ? this.frmParent.objFrmMemoGlobal.CreateMemoNode(type, value, empty1).OuterXml : this.frmParent.objFrmMemoGlobal.CreateMemoNodeHyperlink(type, value, empty1, strHypDescription).OuterXml);
						this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Remove(str1.ToString());
						this.frmParent.objFrmMemoGlobal.dicGlobalMemoList.Add(str1.ToString(), empty3);
						this.frmParent.objFrmMemoGlobal.iMemoResultCounter++;
					}
				}
				if (strMemoTyp == "LocalMemo")
				{
					if (this.frmParent.objFrmMemoLocal.strMemoChangedType == string.Empty)
					{
						this.frmParent.objFrmMemoLocal.strMemoChangedType = str.ToUpper();
					}
					else
					{
						frmMemoLocal _frmMemoLocal = this.frmParent.objFrmMemoLocal;
						_frmMemoLocal.strMemoChangedType = string.Concat(_frmMemoLocal.strMemoChangedType, "|");
						frmMemoLocal _frmMemoLocal1 = this.frmParent.objFrmMemoLocal;
						_frmMemoLocal1.strMemoChangedType = string.Concat(_frmMemoLocal1.strMemoChangedType, str.ToUpper());
					}
				}
				else if (this.frmParent.objFrmMemoLocal.strMemoChangedType == string.Empty)
				{
					this.frmParent.objFrmMemoGlobal.strMemoChangedType = str.ToUpper();
				}
				else
				{
					frmMemoGlobal _frmMemoGlobal = this.frmParent.objFrmMemoGlobal;
					_frmMemoGlobal.strMemoChangedType = string.Concat(_frmMemoGlobal.strMemoChangedType, "|");
					frmMemoGlobal _frmMemoGlobal1 = this.frmParent.objFrmMemoGlobal;
					_frmMemoGlobal1.strMemoChangedType = string.Concat(_frmMemoGlobal1.strMemoChangedType, str.ToUpper());
				}
				try
				{
					DateTime dateTime = DateTime.ParseExact(empty1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					string[] strArrays = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
					if (this.strDateFormat != string.Empty)
					{
						if (this.strDateFormat.ToUpper() != "INVALID")
						{
							string[] strArrays1 = strArrays;
							for (int k = 0; k < (int)strArrays1.Length; k++)
							{
								string str4 = strArrays1[k];
								if (this.strDateFormat == str4)
								{
									empty1 = dateTime.ToString(this.strDateFormat);
								}
							}
						}
						else
						{
							empty1 = "Unknown";
						}
					}
				}
				catch
				{
				}
				this.frmParent.objFrmTasks.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
				Graphics graphic = this.frmParent.objFrmTasks.tvMemoTypes.CreateGraphics();
				System.Drawing.Font font = this.frmParent.objFrmTasks.tvMemoTypes.Font;
				List<frmMemoTasks.TreeNodeText> treeNodeTexts = new List<frmMemoTasks.TreeNodeText>();
				string[] multilingualValue = new string[] { empty, "\r\n", empty1, "\r\n", this.GetMultilingualValue(str) };
				treeNodeTexts.Add(new frmMemoTasks.TreeNodeText(string.Concat(multilingualValue)));
				foreach (frmMemoTasks.TreeNodeText treeNodeText in treeNodeTexts)
				{
					if (empty.Contains("|"))
					{
						empty = empty.Replace("|", "PIPESIGN");
					}
					if (value.Contains("|"))
					{
						value = value.Replace("|", "PIPESIGN");
					}
					TreeNode treeNode = new TreeNode();
					string[] strArrays2 = new string[] { empty, "|", empty1, "|", str, "|", value, "|", str1.ToString() };
					treeNode.Name = string.Concat(strArrays2);
					treeNode.Tag = treeNodeText;
					treeNode.Text = this.MaxWidthString(graphic, font, treeNodeText.TextList);
					if (strMemoTyp != "LocalMemo")
					{
						if (strMemoTyp != "GlobalMemo")
						{
							continue;
						}
						int index = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Index;
						this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes[index].Name = treeNode.Name;
						this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes[index].Tag = treeNode.Tag;
						this.frmParent.objFrmTasks.tvMemoTypes.Nodes["GlobalMemo"].Nodes[index].Text = treeNode.Text;
						this.frmParent.objFrmMemoGlobal.bMemoChanged = true;
					}
					else
					{
						int tag = this.frmParent.objFrmTasks.tvMemoTypes.SelectedNode.Index;
						this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes[tag].Name = treeNode.Name;
						this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes[tag].Tag = treeNode.Tag;
						this.frmParent.objFrmTasks.tvMemoTypes.Nodes["LocalMemo"].Nodes[tag].Text = treeNode.Text;
						this.frmParent.objFrmMemoLocal.bMemoChanged = true;
					}
				}
				this.tvMemoTypes.Update();
				this.tvMemoTypes.ExpandAll();
				this.tvMemoTypes.Focus();
				this.tvMemoTypes.SelectedNode.EnsureVisible();
			}
		}

		private class TreeNodeText
		{
			private List<string> textList;

			public string DisplayText
			{
				get
				{
					return string.Join("\r\n", this.textList.ToArray());
				}
			}

			public List<string> TextList
			{
				get
				{
					return this.textList;
				}
				set
				{
					this.textList = value;
				}
			}

			public TreeNodeText(string text)
			{
				string[] strArrays = new string[] { "\r\n", "\n" };
				string[] strArrays1 = text.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < (int)strArrays1.Length; i++)
				{
					string str = strArrays1[i];
					this.textList.Add(str);
				}
			}
		}
	}
}
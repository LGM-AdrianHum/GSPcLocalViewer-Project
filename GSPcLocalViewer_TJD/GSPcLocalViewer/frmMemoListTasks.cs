using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmMemoListTasks : Form
	{
		private IContainer components;

		private Panel pnlTasks;

		private Panel pnlTasks2;

		private Label lblTasksTitle;

		private Label lblView;

		private Label lblSpace1;

		private Label lblPartMemo;

		private Label lblPictureMemo;

		private Label lblBothMemo;

		private Panel pnlTasks1;

		private Label lblAllMemo;

		private Label lblGlobalMemo;

		private Label lblLocalMemo;

		private Label lblTypes;

		private Label label5;

		private Label lblAdminMemo;

		private Panel pnlMemoTypes;

		public TreeView tvMemoTypes;

		private frmMemoList frmParent;

		public int intMemoType;

		private string strMemoPriority = "LOCAL";

		private string strDateFormat = string.Empty;

		private TreeNode tnPreviouisNode = new TreeNode();

		private string strTextMemoState = "TRUE";

		private string strReferenceMemoState = "TRUE";

		private string strHyperlinkMemoState = "TRUE";

		private string strProgramMemoState = "TRUE";

		private bool bMemoLoadedFirstTime;

		public frmMemoListTasks(frmMemoList frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.UpdateFont();
			this.LoadResources();
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
			if (Settings.Default.EnableGlobalMemo || Settings.Default.EnableLocalMemo || Settings.Default.EnableAdminMemo)
			{
				this.lblBothMemo.Enabled = true;
			}
			else
			{
				this.lblBothMemo.Enabled = false;
			}
			this.intMemoType = this.GetMemoType();
			this.strMemoPriority = this.GetMemoPriority();
			this.strDateFormat = this.GetDateFormat();
			this.GetMemoStates();
			if (this.intMemoType != 2)
			{
				this.pnlMemoTypes.Visible = false;
				this.pnlTasks1.Visible = true;
				this.pnlTasks2.Dock = DockStyle.Top;
				return;
			}
			this.pnlTasks1.Visible = false;
			this.pnlMemoTypes.Visible = true;
			this.pnlTasks2.Dock = DockStyle.Bottom;
			if (!Settings.Default.EnableLocalMemo && !Settings.Default.EnableGlobalMemo && !Settings.Default.EnableAdminMemo)
			{
				this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
				this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
				this.frmParent.objFrmMemoLocal.Show();
				this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
			}
			else if (Settings.Default.EnableLocalMemo)
			{
				this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
				this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
				this.frmParent.objFrmMemoLocal.Show();
				this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
			}
			else if (Settings.Default.EnableGlobalMemo)
			{
				this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
				this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
				this.frmParent.objFrmMemoGlobal.Show();
				this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
			}
			else if (Settings.Default.EnableAdminMemo)
			{
				this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
				this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
				this.frmParent.objFrmMemoAdmin.Show();
				this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
			}
			this.bMemoLoadedFirstTime = true;
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
					str = xNode.Attributes["Value"].Value.Trim();
					empty = " ";
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
			else if (xNode.Attributes["Update"].Value.Trim() == string.Empty)
			{
				str1 = "Unknown";
			}
			else
			{
				str1 = xNode.Attributes["Update"].Value.Trim();
				try
				{
					DateTime dateTime = DateTime.ParseExact(str1, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					string[] strArrays = new string[] { "yyyy/MM/dd hh:mm:ss", "MM/dd hh:mm", "yyyy/MM/dd", "hh:mm:ss" };
					if (this.strDateFormat != string.Empty)
					{
						if (this.strDateFormat.ToUpper() != "INVALID")
						{
							string[] strArrays1 = strArrays;
							for (int i = 0; i < (int)strArrays1.Length; i++)
							{
								string str2 = strArrays1[i];
								if (this.strDateFormat == str2)
								{
									str1 = dateTime.ToString(this.strDateFormat);
								}
							}
						}
						else
						{
							str1 = "Unknown";
						}
					}
				}
				catch
				{
				}
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
				str = string.Concat(str, "/Screen[@Name='MEMO_LIST']");
				str = string.Concat(str, "/Screen[@Name='MEMOLIST_TASKS']");
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
			if (lbl.Parent.Name.Equals(this.pnlTasks1.Name))
			{
				for (int i = 0; i < this.pnlTasks1.Controls.Count; i++)
				{
					if (this.pnlTasks1.Controls[i] == this.lblGlobalMemo | this.pnlTasks1.Controls[i] == this.lblLocalMemo | this.pnlTasks1.Controls[i] == this.lblAdminMemo | this.pnlTasks1.Controls[i] == this.lblAllMemo)
					{
						this.pnlTasks1.Controls[i].BackColor = this.pnlTasks1.BackColor;
						this.pnlTasks1.Controls[i].ForeColor = this.pnlTasks1.ForeColor;
					}
				}
				lbl.BackColor = Settings.Default.appHighlightBackColor;
				lbl.ForeColor = Settings.Default.appHighlightForeColor;
			}
			if (lbl.Parent.Name.Equals(this.pnlTasks2.Name))
			{
				for (int j = 0; j < this.pnlTasks2.Controls.Count; j++)
				{
					if (this.pnlTasks2.Controls[j] == this.lblPartMemo | this.pnlTasks2.Controls[j] == this.lblPictureMemo | this.pnlTasks2.Controls[j] == this.lblBothMemo)
					{
						this.pnlTasks2.Controls[j].BackColor = this.pnlTasks2.BackColor;
						this.pnlTasks2.Controls[j].ForeColor = this.pnlTasks2.ForeColor;
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
			this.lblBothMemo = new Label();
			this.lblPartMemo = new Label();
			this.lblPictureMemo = new Label();
			this.lblView = new Label();
			this.lblSpace1 = new Label();
			this.pnlTasks1 = new Panel();
			this.label5 = new Label();
			this.lblAllMemo = new Label();
			this.lblAdminMemo = new Label();
			this.lblGlobalMemo = new Label();
			this.lblLocalMemo = new Label();
			this.lblTypes = new Label();
			this.lblTasksTitle = new Label();
			this.pnlTasks.SuspendLayout();
			this.pnlMemoTypes.SuspendLayout();
			this.pnlTasks2.SuspendLayout();
			this.pnlTasks1.SuspendLayout();
			base.SuspendLayout();
			this.pnlTasks.BackColor = Color.White;
			this.pnlTasks.BorderStyle = BorderStyle.FixedSingle;
			this.pnlTasks.Controls.Add(this.pnlMemoTypes);
			this.pnlTasks.Controls.Add(this.pnlTasks2);
			this.pnlTasks.Controls.Add(this.pnlTasks1);
			this.pnlTasks.Controls.Add(this.lblTasksTitle);
			this.pnlTasks.Dock = DockStyle.Fill;
			this.pnlTasks.ForeColor = Color.Black;
			this.pnlTasks.Location = new Point(0, 0);
			this.pnlTasks.Name = "pnlTasks";
			this.pnlTasks.Size = new System.Drawing.Size(151, 392);
			this.pnlTasks.TabIndex = 8;
			this.pnlMemoTypes.BackColor = Color.White;
			this.pnlMemoTypes.Controls.Add(this.tvMemoTypes);
			this.pnlMemoTypes.Dock = DockStyle.Fill;
			this.pnlMemoTypes.Location = new Point(0, 149);
			this.pnlMemoTypes.Name = "pnlMemoTypes";
			this.pnlMemoTypes.Size = new System.Drawing.Size(149, 107);
			this.pnlMemoTypes.TabIndex = 13;
			this.tvMemoTypes.BorderStyle = BorderStyle.None;
			this.tvMemoTypes.Dock = DockStyle.Fill;
			this.tvMemoTypes.Location = new Point(0, 0);
			this.tvMemoTypes.Name = "tvMemoTypes";
			this.tvMemoTypes.Size = new System.Drawing.Size(149, 107);
			this.tvMemoTypes.TabIndex = 0;
			this.tvMemoTypes.DrawNode += new DrawTreeNodeEventHandler(this.tvMemoTypes_DrawNode);
			this.tvMemoTypes.AfterSelect += new TreeViewEventHandler(this.tvMemoTypes_AfterSelect);
			this.tvMemoTypes.Click += new EventHandler(this.tvMemoTypes_Click);
			this.pnlTasks2.BackColor = Color.White;
			this.pnlTasks2.Controls.Add(this.lblBothMemo);
			this.pnlTasks2.Controls.Add(this.lblPartMemo);
			this.pnlTasks2.Controls.Add(this.lblPictureMemo);
			this.pnlTasks2.Controls.Add(this.lblView);
			this.pnlTasks2.Controls.Add(this.lblSpace1);
			this.pnlTasks2.Dock = DockStyle.Bottom;
			this.pnlTasks2.Location = new Point(0, 256);
			this.pnlTasks2.Name = "pnlTasks2";
			this.pnlTasks2.Padding = new System.Windows.Forms.Padding(15, 10, 15, 0);
			this.pnlTasks2.Size = new System.Drawing.Size(149, 134);
			this.pnlTasks2.TabIndex = 11;
			this.lblBothMemo.Cursor = Cursors.Hand;
			this.lblBothMemo.Dock = DockStyle.Top;
			this.lblBothMemo.Location = new Point(15, 80);
			this.lblBothMemo.Name = "lblBothMemo";
			this.lblBothMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblBothMemo.Size = new System.Drawing.Size(119, 16);
			this.lblBothMemo.TabIndex = 24;
			this.lblBothMemo.Text = "Both";
			this.lblBothMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblBothMemo.Click += new EventHandler(this.lblBothMemo_Click);
			this.lblPartMemo.Cursor = Cursors.Hand;
			this.lblPartMemo.Dock = DockStyle.Top;
			this.lblPartMemo.Location = new Point(15, 64);
			this.lblPartMemo.Name = "lblPartMemo";
			this.lblPartMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblPartMemo.Size = new System.Drawing.Size(119, 16);
			this.lblPartMemo.TabIndex = 22;
			this.lblPartMemo.Text = "Part Memo";
			this.lblPartMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblPartMemo.Click += new EventHandler(this.lblPartMemo_Click);
			this.lblPictureMemo.Cursor = Cursors.Hand;
			this.lblPictureMemo.Dock = DockStyle.Top;
			this.lblPictureMemo.Location = new Point(15, 48);
			this.lblPictureMemo.Name = "lblPictureMemo";
			this.lblPictureMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblPictureMemo.Size = new System.Drawing.Size(119, 16);
			this.lblPictureMemo.TabIndex = 23;
			this.lblPictureMemo.Text = "Picture Memo";
			this.lblPictureMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblPictureMemo.Click += new EventHandler(this.lblPictureMemo_Click);
			this.lblView.BackColor = Color.Transparent;
			this.lblView.Dock = DockStyle.Top;
			this.lblView.ForeColor = Color.Blue;
			this.lblView.Image = Resources.GroupLine1;
			this.lblView.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblView.Location = new Point(15, 20);
			this.lblView.Name = "lblView";
			this.lblView.Size = new System.Drawing.Size(119, 28);
			this.lblView.TabIndex = 20;
			this.lblView.Text = "View Options";
			this.lblView.TextAlign = ContentAlignment.MiddleLeft;
			this.lblSpace1.Cursor = Cursors.Hand;
			this.lblSpace1.Dock = DockStyle.Top;
			this.lblSpace1.Location = new Point(15, 10);
			this.lblSpace1.Name = "lblSpace1";
			this.lblSpace1.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblSpace1.Size = new System.Drawing.Size(119, 10);
			this.lblSpace1.TabIndex = 19;
			this.lblSpace1.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlTasks1.BackColor = Color.White;
			this.pnlTasks1.Controls.Add(this.label5);
			this.pnlTasks1.Controls.Add(this.lblAllMemo);
			this.pnlTasks1.Controls.Add(this.lblAdminMemo);
			this.pnlTasks1.Controls.Add(this.lblGlobalMemo);
			this.pnlTasks1.Controls.Add(this.lblLocalMemo);
			this.pnlTasks1.Controls.Add(this.lblTypes);
			this.pnlTasks1.Dock = DockStyle.Top;
			this.pnlTasks1.Location = new Point(0, 27);
			this.pnlTasks1.Name = "pnlTasks1";
			this.pnlTasks1.Padding = new System.Windows.Forms.Padding(15, 10, 15, 0);
			this.pnlTasks1.Size = new System.Drawing.Size(149, 122);
			this.pnlTasks1.TabIndex = 12;
			this.label5.Cursor = Cursors.Hand;
			this.label5.Dock = DockStyle.Top;
			this.label5.Location = new Point(15, 102);
			this.label5.Name = "label5";
			this.label5.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.label5.Size = new System.Drawing.Size(119, 10);
			this.label5.TabIndex = 19;
			this.label5.TextAlign = ContentAlignment.MiddleLeft;
			this.lblAllMemo.Cursor = Cursors.Hand;
			this.lblAllMemo.Dock = DockStyle.Top;
			this.lblAllMemo.Location = new Point(15, 86);
			this.lblAllMemo.Name = "lblAllMemo";
			this.lblAllMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblAllMemo.Size = new System.Drawing.Size(119, 16);
			this.lblAllMemo.TabIndex = 24;
			this.lblAllMemo.Text = "All Memos";
			this.lblAllMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblAllMemo.Click += new EventHandler(this.lblAllMemo_Click);
			this.lblAdminMemo.Cursor = Cursors.Hand;
			this.lblAdminMemo.Dock = DockStyle.Top;
			this.lblAdminMemo.Location = new Point(15, 70);
			this.lblAdminMemo.Name = "lblAdminMemo";
			this.lblAdminMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblAdminMemo.Size = new System.Drawing.Size(119, 16);
			this.lblAdminMemo.TabIndex = 25;
			this.lblAdminMemo.Text = "Admin Memos";
			this.lblAdminMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblAdminMemo.Click += new EventHandler(this.lblAdminMemo_Click);
			this.lblGlobalMemo.Cursor = Cursors.Hand;
			this.lblGlobalMemo.Dock = DockStyle.Top;
			this.lblGlobalMemo.Location = new Point(15, 54);
			this.lblGlobalMemo.Name = "lblGlobalMemo";
			this.lblGlobalMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblGlobalMemo.Size = new System.Drawing.Size(119, 16);
			this.lblGlobalMemo.TabIndex = 22;
			this.lblGlobalMemo.Text = "Global Memos";
			this.lblGlobalMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblGlobalMemo.Click += new EventHandler(this.lblGlobalMemo_Click);
			this.lblLocalMemo.Cursor = Cursors.Hand;
			this.lblLocalMemo.Dock = DockStyle.Top;
			this.lblLocalMemo.Location = new Point(15, 38);
			this.lblLocalMemo.Name = "lblLocalMemo";
			this.lblLocalMemo.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lblLocalMemo.Size = new System.Drawing.Size(119, 16);
			this.lblLocalMemo.TabIndex = 23;
			this.lblLocalMemo.Text = "Local Memos";
			this.lblLocalMemo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblLocalMemo.Click += new EventHandler(this.lblLocalMemo_Click);
			this.lblTypes.BackColor = Color.Transparent;
			this.lblTypes.Dock = DockStyle.Top;
			this.lblTypes.ForeColor = Color.Blue;
			this.lblTypes.Image = Resources.GroupLine1;
			this.lblTypes.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblTypes.Location = new Point(15, 10);
			this.lblTypes.Name = "lblTypes";
			this.lblTypes.Size = new System.Drawing.Size(119, 28);
			this.lblTypes.TabIndex = 20;
			this.lblTypes.Text = "Memo Types";
			this.lblTypes.TextAlign = ContentAlignment.MiddleLeft;
			this.lblTasksTitle.BackColor = Color.White;
			this.lblTasksTitle.Dock = DockStyle.Top;
			this.lblTasksTitle.ForeColor = Color.Black;
			this.lblTasksTitle.Location = new Point(0, 0);
			this.lblTasksTitle.Name = "lblTasksTitle";
			this.lblTasksTitle.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblTasksTitle.Size = new System.Drawing.Size(149, 27);
			this.lblTasksTitle.TabIndex = 6;
			this.lblTasksTitle.Text = "Tasks";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(151, 392);
			base.Controls.Add(this.pnlTasks);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmMemoListTasks";
			base.Load += new EventHandler(this.frmMemoTasks_Load);
			this.pnlTasks.ResumeLayout(false);
			this.pnlMemoTypes.ResumeLayout(false);
			this.pnlTasks2.ResumeLayout(false);
			this.pnlTasks1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void lblAdminMemo_Click(object sender, EventArgs e)
		{
			this.HighlightList(ref this.lblAdminMemo);
			this.frmParent.HideForms();
			this.frmParent.objFrmMemoAdmin.Show();
		}

		private void lblAllMemo_Click(object sender, EventArgs e)
		{
			this.HighlightList(ref this.lblAllMemo);
			this.frmParent.HideForms();
			this.frmParent.objFrmMemoAll.Show();
		}

		private void lblBothMemo_Click(object sender, EventArgs e)
		{
			this.HighlightList(ref this.lblBothMemo);
			if (!this.bMemoLoadedFirstTime)
			{
				if (this.intMemoType == 2)
				{
					this.LoadMemo(true, true);
					return;
				}
				if (Settings.Default.EnableLocalMemo)
				{
					this.frmParent.objFrmMemoLocal.LoadMemos(true, true);
				}
				if (Settings.Default.EnableGlobalMemo)
				{
					this.frmParent.objFrmMemoGlobal.LoadMemos(true, true);
				}
				if (Settings.Default.EnableAdminMemo)
				{
					this.frmParent.objFrmMemoAdmin.LoadMemos(true, true);
				}
				if (Settings.Default.EnableAdminMemo || Settings.Default.EnableGlobalMemo || Settings.Default.EnableLocalMemo)
				{
					this.frmParent.objFrmMemoAll.LoadMemos(true, true);
				}
			}
		}

		private void lblGlobalMemo_Click(object sender, EventArgs e)
		{
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

		private void lblPartMemo_Click(object sender, EventArgs e)
		{
			this.HighlightList(ref this.lblPartMemo);
			this.bMemoLoadedFirstTime = false;
			if (this.intMemoType == 2)
			{
				this.LoadMemo(false, true);
				return;
			}
			if (Settings.Default.EnableLocalMemo)
			{
				this.frmParent.objFrmMemoLocal.LoadMemos(false, true);
			}
			if (Settings.Default.EnableGlobalMemo)
			{
				this.frmParent.objFrmMemoGlobal.LoadMemos(false, true);
			}
			if (Settings.Default.EnableAdminMemo)
			{
				this.frmParent.objFrmMemoAdmin.LoadMemos(false, true);
			}
			if (Settings.Default.EnableAdminMemo || Settings.Default.EnableGlobalMemo || Settings.Default.EnableLocalMemo)
			{
				this.frmParent.objFrmMemoAll.LoadMemos(false, true);
			}
		}

		private void lblPictureMemo_Click(object sender, EventArgs e)
		{
			this.HighlightList(ref this.lblPictureMemo);
			this.bMemoLoadedFirstTime = false;
			if (this.intMemoType == 2)
			{
				this.LoadMemo(true, false);
				return;
			}
			if (Settings.Default.EnableLocalMemo)
			{
				this.frmParent.objFrmMemoLocal.LoadMemos(true, false);
			}
			if (Settings.Default.EnableGlobalMemo)
			{
				this.frmParent.objFrmMemoGlobal.LoadMemos(true, false);
			}
			if (Settings.Default.EnableAdminMemo)
			{
				this.frmParent.objFrmMemoAdmin.LoadMemos(true, false);
			}
			if (Settings.Default.EnableAdminMemo || Settings.Default.EnableGlobalMemo || Settings.Default.EnableLocalMemo)
			{
				this.frmParent.objFrmMemoAll.LoadMemos(true, false);
			}
		}

		private void LoadMemo(bool bPictureLevel, bool bPartLevel)
		{
			string[] item;
			if (bPictureLevel && !bPartLevel)
			{
				this.tvMemoTypes.Nodes.Clear();
				this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
				this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
				this.pnlTasks1.Hide();
				this.pnlMemoTypes.Dock = DockStyle.Fill;
				this.pnlTasks2.Show();
				this.pnlTasks2.Dock = DockStyle.Bottom;
				TreeNode treeNode = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL))
				{
					Name = "LocalMemo"
				};
				TreeNode treeNode1 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL))
				{
					Name = "GlobalMemo"
				};
				TreeNode treeNode2 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL))
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
							if (xmlNodes.Attributes["PartNo"] == null || !(xmlNodes.Attributes["PartNo"].Value == ""))
							{
								continue;
							}
							List<string> memoDetails = this.GetMemoDetails(xmlNodes);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts = new List<frmMemoListTasks.TreeNodeText>();
							string upper = memoDetails[2].ToUpper();
							string empty = string.Empty;
							if (upper != "HYPERLINK")
							{
								item = new string[] { memoDetails[0], "\r\n", memoDetails[1], "\r\n", this.GetMultilingualValue(memoDetails[2]) };
								empty = string.Concat(item);
							}
							else if (memoDetails[0].Length <= 25)
							{
								item = new string[] { memoDetails[0], "\r\n", memoDetails[1], "\r\n", this.GetMultilingualValue(memoDetails[2]) };
								empty = string.Concat(item);
							}
							else
							{
								item = new string[] { memoDetails[0].Substring(0, 25), "...\r\n", memoDetails[1], "\r\n", this.GetMultilingualValue(memoDetails[2]) };
								empty = string.Concat(item);
							}
							treeNodeTexts.Add(new frmMemoListTasks.TreeNodeText(empty));
							foreach (frmMemoListTasks.TreeNodeText treeNodeText in treeNodeTexts)
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
								item = new string[] { memoDetails[0], "|", memoDetails[1], "|", memoDetails[2], "|", memoDetails[3] };
								treeNode3.Name = string.Concat(item);
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
							if (xmlNodes1.Attributes["PartNo"] == null || !(xmlNodes1.Attributes["PartNo"].Value == ""))
							{
								continue;
							}
							List<string> strs = this.GetMemoDetails(xmlNodes1);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts1 = new List<frmMemoListTasks.TreeNodeText>();
							string str = strs[2].ToUpper();
							string empty1 = string.Empty;
							if (str != "HYPERLINK")
							{
								item = new string[] { strs[0], "\r\n", strs[1], "\r\n", this.GetMultilingualValue(strs[2]) };
								empty1 = string.Concat(item);
							}
							else if (strs[0].Length <= 25)
							{
								item = new string[] { strs[0], "\r\n", strs[1], "\r\n", this.GetMultilingualValue(strs[2]) };
								empty1 = string.Concat(item);
							}
							else
							{
								item = new string[] { strs[0].Substring(0, 25), "...\r\n", strs[1], "\r\n", this.GetMultilingualValue(strs[2]) };
								empty1 = string.Concat(item);
							}
							treeNodeTexts1.Add(new frmMemoListTasks.TreeNodeText(empty1));
							foreach (frmMemoListTasks.TreeNodeText treeNodeText1 in treeNodeTexts1)
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
								item = new string[] { strs[0], "|", strs[1], "|", strs[2], "|", strs[3] };
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
					if (Settings.Default.EnableAdminMemo && this.frmParent.xnlAdminMemo != null && this.frmParent.xnlAdminMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes2 in this.frmParent.xnlAdminMemo)
						{
							if (xmlNodes2.Attributes["PartNo"] == null || !(xmlNodes2.Attributes["PartNo"].Value == ""))
							{
								continue;
							}
							List<string> memoDetails1 = this.GetMemoDetails(xmlNodes2);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts2 = new List<frmMemoListTasks.TreeNodeText>();
							item = new string[] { memoDetails1[0], "\r\n", memoDetails1[1], "\r\n", memoDetails1[2] };
							treeNodeTexts2.Add(new frmMemoListTasks.TreeNodeText(string.Concat(item)));
							foreach (frmMemoListTasks.TreeNodeText treeNodeText2 in treeNodeTexts2)
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
			else if (!bPictureLevel && bPartLevel)
			{
				this.tvMemoTypes.Nodes.Clear();
				this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
				this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
				TreeNode treeNode6 = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL))
				{
					Name = "LocalMemo"
				};
				TreeNode treeNode7 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL))
				{
					Name = "GlobalMemo"
				};
				TreeNode treeNode8 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL))
				{
					Name = "AdminMemo"
				};
				if (this.strMemoPriority.ToUpper().Trim() == "LOCAL")
				{
					this.tvMemoTypes.Nodes.Add(treeNode6);
					this.tvMemoTypes.Nodes.Add(treeNode7);
					this.tvMemoTypes.Nodes.Add(treeNode8);
				}
				else if (this.strMemoPriority.ToUpper().Trim() == "GLOBAL")
				{
					this.tvMemoTypes.Nodes.Add(treeNode7);
					this.tvMemoTypes.Nodes.Add(treeNode6);
					this.tvMemoTypes.Nodes.Add(treeNode8);
				}
				else if (this.strMemoPriority.ToUpper().Trim() == "ADMIN")
				{
					this.tvMemoTypes.Nodes.Add(treeNode8);
					this.tvMemoTypes.Nodes.Add(treeNode6);
					this.tvMemoTypes.Nodes.Add(treeNode7);
				}
				Graphics graphic1 = this.tvMemoTypes.CreateGraphics();
				System.Drawing.Font font1 = this.tvMemoTypes.Font;
				try
				{
					if (Settings.Default.EnableLocalMemo && this.frmParent.xnlLocalMemo != null && this.frmParent.xnlLocalMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes3 in this.frmParent.xnlLocalMemo)
						{
							if (xmlNodes3.Attributes["PartNo"] == null || !(xmlNodes3.Attributes["PartNo"].Value != ""))
							{
								continue;
							}
							List<string> strs1 = this.GetMemoDetails(xmlNodes3);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts3 = new List<frmMemoListTasks.TreeNodeText>();
							string upper1 = strs1[2].ToUpper();
							string str1 = string.Empty;
							if (upper1 != "HYPERLINK")
							{
								item = new string[] { strs1[0], "\r\n", strs1[1], "\r\n", this.GetMultilingualValue(strs1[2]) };
								str1 = string.Concat(item);
							}
							else if (strs1[0].Length <= 25)
							{
								item = new string[] { strs1[0], "\r\n", strs1[1], "\r\n", this.GetMultilingualValue(strs1[2]) };
								str1 = string.Concat(item);
							}
							else
							{
								item = new string[] { strs1[0].Substring(0, 25), "...\r\n", strs1[1], "\r\n", this.GetMultilingualValue(strs1[2]) };
								str1 = string.Concat(item);
							}
							treeNodeTexts3.Add(new frmMemoListTasks.TreeNodeText(str1));
							foreach (frmMemoListTasks.TreeNodeText treeNodeText3 in treeNodeTexts3)
							{
								if (strs1[0].Contains("|"))
								{
									strs1[0] = strs1[0].Replace("|", "PIPESIGN");
								}
								if (strs1[3].Contains("|"))
								{
									strs1[3] = strs1[3].Replace("|", "PIPESIGN");
								}
								TreeNode treeNode9 = new TreeNode();
								item = new string[] { strs1[0], "|", strs1[1], "|", strs1[2], "|", strs1[3] };
								treeNode9.Name = string.Concat(item);
								treeNode9.Tag = treeNodeText3;
								treeNode9.Text = this.MaxWidthString(graphic1, font1, treeNodeText3.TextList);
								this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(treeNode9);
							}
						}
					}
				}
				catch (Exception exception3)
				{
				}
				try
				{
					if (Settings.Default.EnableGlobalMemo && this.frmParent.xnlGlobalMemo != null && this.frmParent.xnlGlobalMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes4 in this.frmParent.xnlGlobalMemo)
						{
							if (xmlNodes4.Attributes["PartNo"] == null || !(xmlNodes4.Attributes["PartNo"].Value != ""))
							{
								continue;
							}
							List<string> memoDetails2 = this.GetMemoDetails(xmlNodes4);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts4 = new List<frmMemoListTasks.TreeNodeText>();
							string upper2 = memoDetails2[2].ToUpper();
							string empty2 = string.Empty;
							if (upper2 != "HYPERLINK")
							{
								item = new string[] { memoDetails2[0], "\r\n", memoDetails2[1], "\r\n", this.GetMultilingualValue(memoDetails2[2]) };
								empty2 = string.Concat(item);
							}
							else if (memoDetails2[0].Length <= 25)
							{
								item = new string[] { memoDetails2[0], "\r\n", memoDetails2[1], "\r\n", this.GetMultilingualValue(memoDetails2[2]) };
								empty2 = string.Concat(item);
							}
							else
							{
								item = new string[] { memoDetails2[0].Substring(0, 25), "...\r\n", memoDetails2[1], "\r\n", this.GetMultilingualValue(memoDetails2[2]) };
								empty2 = string.Concat(item);
							}
							treeNodeTexts4.Add(new frmMemoListTasks.TreeNodeText(empty2));
							foreach (frmMemoListTasks.TreeNodeText treeNodeText4 in treeNodeTexts4)
							{
								if (memoDetails2[0].Contains("|"))
								{
									memoDetails2[0] = memoDetails2[0].Replace("|", "PIPESIGN");
								}
								if (memoDetails2[3].Contains("|"))
								{
									memoDetails2[3] = memoDetails2[3].Replace("|", "PIPESIGN");
								}
								TreeNode treeNode10 = new TreeNode();
								item = new string[] { memoDetails2[0], "|", memoDetails2[1], "|", memoDetails2[2], "|", memoDetails2[3] };
								treeNode10.Name = string.Concat(item);
								treeNode10.Tag = treeNodeText4;
								treeNode10.Text = this.MaxWidthString(graphic1, font1, treeNodeText4.TextList);
								this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(treeNode10);
							}
						}
					}
				}
				catch (Exception exception4)
				{
				}
				try
				{
					if (Settings.Default.EnableAdminMemo && this.frmParent.xnlAdminMemo != null && this.frmParent.xnlAdminMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes5 in this.frmParent.xnlAdminMemo)
						{
							if (xmlNodes5.Attributes["PartNo"] == null || !(xmlNodes5.Attributes["PartNo"].Value != ""))
							{
								continue;
							}
							List<string> strs2 = this.GetMemoDetails(xmlNodes5);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts5 = new List<frmMemoListTasks.TreeNodeText>();
							item = new string[] { strs2[0], "\r\n", strs2[1], "\r\n", strs2[2] };
							treeNodeTexts5.Add(new frmMemoListTasks.TreeNodeText(string.Concat(item)));
							foreach (frmMemoListTasks.TreeNodeText treeNodeText5 in treeNodeTexts5)
							{
								TreeNode treeNode11 = new TreeNode();
								item = new string[] { strs2[0], "|", strs2[1], "|", strs2[2], "|", strs2[3] };
								treeNode11.Name = string.Concat(item);
								treeNode11.Tag = treeNodeText5;
								treeNode11.Text = this.MaxWidthString(graphic1, font1, treeNodeText5.TextList);
								this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Add(treeNode11);
							}
						}
					}
				}
				catch (Exception exception5)
				{
				}
			}
			else if (bPictureLevel && bPartLevel)
			{
				this.tvMemoTypes.Nodes.Clear();
				this.tvMemoTypes.DrawMode = TreeViewDrawMode.OwnerDrawText;
				this.tvMemoTypes.ItemHeight = this.GetTreevieeItemHeight();
				TreeNode treeNode12 = new TreeNode(this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL))
				{
					Name = "LocalMemo"
				};
				TreeNode treeNode13 = new TreeNode(this.GetResource("Global Memos", "GLOBAL_MEMO", ResourceType.LABEL))
				{
					Name = "GlobalMemo"
				};
				TreeNode treeNode14 = new TreeNode(this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL))
				{
					Name = "AdminMemo"
				};
				if (this.strMemoPriority.ToUpper().Trim() == "LOCAL")
				{
					this.tvMemoTypes.Nodes.Add(treeNode12);
					this.tvMemoTypes.Nodes.Add(treeNode13);
					this.tvMemoTypes.Nodes.Add(treeNode14);
				}
				else if (this.strMemoPriority.ToUpper().Trim() == "GLOBAL")
				{
					this.tvMemoTypes.Nodes.Add(treeNode13);
					this.tvMemoTypes.Nodes.Add(treeNode12);
					this.tvMemoTypes.Nodes.Add(treeNode14);
				}
				else if (this.strMemoPriority.ToUpper().Trim() == "ADMIN")
				{
					this.tvMemoTypes.Nodes.Add(treeNode14);
					this.tvMemoTypes.Nodes.Add(treeNode12);
					this.tvMemoTypes.Nodes.Add(treeNode13);
				}
				Graphics graphic2 = this.tvMemoTypes.CreateGraphics();
				System.Drawing.Font font2 = this.tvMemoTypes.Font;
				try
				{
					if (Settings.Default.EnableLocalMemo && this.frmParent.xnlLocalMemo != null && this.frmParent.xnlLocalMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes6 in this.frmParent.xnlLocalMemo)
						{
							List<string> memoDetails3 = this.GetMemoDetails(xmlNodes6);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts6 = new List<frmMemoListTasks.TreeNodeText>();
							string str2 = memoDetails3[2].ToUpper();
							string empty3 = string.Empty;
							if (str2 != "HYPERLINK")
							{
								item = new string[] { memoDetails3[0], "\r\n", memoDetails3[1], "\r\n", this.GetMultilingualValue(memoDetails3[2]) };
								empty3 = string.Concat(item);
							}
							else if (memoDetails3[0].Length <= 25)
							{
								item = new string[] { memoDetails3[0], "\r\n", memoDetails3[1], "\r\n", this.GetMultilingualValue(memoDetails3[2]) };
								empty3 = string.Concat(item);
							}
							else
							{
								item = new string[] { memoDetails3[0].Substring(0, 25), "...\r\n", memoDetails3[1], "\r\n", this.GetMultilingualValue(memoDetails3[2]) };
								empty3 = string.Concat(item);
							}
							treeNodeTexts6.Add(new frmMemoListTasks.TreeNodeText(empty3));
							if (memoDetails3[2].ToUpper() == "TEXT")
							{
								if (this.strTextMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails3[2].ToUpper() == "REFERENCE")
							{
								if (this.strReferenceMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails3[2].ToUpper() == "HYPERLINK")
							{
								if (this.strHyperlinkMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails3[2].ToUpper() == "PROGRAM" && this.strProgramMemoState.ToUpper() == "FALSE")
							{
								continue;
							}
							foreach (frmMemoListTasks.TreeNodeText treeNodeText6 in treeNodeTexts6)
							{
								if (memoDetails3[0].Contains("|"))
								{
									memoDetails3[0] = memoDetails3[0].Replace("|", "PIPESIGN");
								}
								if (memoDetails3[3].Contains("|"))
								{
									memoDetails3[3] = memoDetails3[3].Replace("|", "PIPESIGN");
								}
								TreeNode treeNode15 = new TreeNode();
								item = new string[] { memoDetails3[0], "|", memoDetails3[1], "|", memoDetails3[2], "|", memoDetails3[3] };
								treeNode15.Name = string.Concat(item);
								treeNode15.Tag = treeNodeText6;
								treeNode15.Text = this.MaxWidthString(graphic2, font2, treeNodeText6.TextList);
								this.tvMemoTypes.Nodes["LocalMemo"].Nodes.Add(treeNode15);
							}
						}
					}
				}
				catch (Exception exception6)
				{
				}
				try
				{
					if (Settings.Default.EnableGlobalMemo && this.frmParent.xnlGlobalMemo != null && this.frmParent.xnlGlobalMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes7 in this.frmParent.xnlGlobalMemo)
						{
							List<string> strs3 = this.GetMemoDetails(xmlNodes7);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts7 = new List<frmMemoListTasks.TreeNodeText>();
							string upper3 = strs3[2].ToUpper();
							string str3 = string.Empty;
							if (upper3 != "HYPERLINK")
							{
								item = new string[] { strs3[0], "\r\n", strs3[1], "\r\n", this.GetMultilingualValue(strs3[2]) };
								str3 = string.Concat(item);
							}
							else if (strs3[0].Length <= 25)
							{
								item = new string[] { strs3[0], "\r\n", strs3[1], "\r\n", this.GetMultilingualValue(strs3[2]) };
								str3 = string.Concat(item);
							}
							else
							{
								item = new string[] { strs3[0].Substring(0, 25), "...\r\n", strs3[1], "\r\n", this.GetMultilingualValue(strs3[2]) };
								str3 = string.Concat(item);
							}
							treeNodeTexts7.Add(new frmMemoListTasks.TreeNodeText(str3));
							if (strs3[2].ToUpper() == "TEXT")
							{
								if (this.strTextMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (strs3[2].ToUpper() == "REFERENCE")
							{
								if (this.strReferenceMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (strs3[2].ToUpper() == "HYPERLINK")
							{
								if (this.strHyperlinkMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (strs3[2].ToUpper() == "PROGRAM" && this.strProgramMemoState.ToUpper() == "FALSE")
							{
								continue;
							}
							foreach (frmMemoListTasks.TreeNodeText treeNodeText7 in treeNodeTexts7)
							{
								if (strs3[0].Contains("|"))
								{
									strs3[0] = strs3[0].Replace("|", "PIPESIGN");
								}
								if (strs3[3].Contains("|"))
								{
									strs3[3] = strs3[3].Replace("|", "PIPESIGN");
								}
								TreeNode treeNode16 = new TreeNode();
								item = new string[] { strs3[0], "|", strs3[1], "|", strs3[2], "|", strs3[3] };
								treeNode16.Name = string.Concat(item);
								treeNode16.Tag = treeNodeText7;
								treeNode16.Text = this.MaxWidthString(graphic2, font2, treeNodeText7.TextList);
								this.tvMemoTypes.Nodes["GlobalMemo"].Nodes.Add(treeNode16);
							}
						}
					}
				}
				catch (Exception exception7)
				{
				}
				try
				{
					if (Settings.Default.EnableAdminMemo && this.frmParent.xnlAdminMemo != null && this.frmParent.xnlAdminMemo.Count > 0)
					{
						foreach (XmlNode xmlNodes8 in this.frmParent.xnlAdminMemo)
						{
							List<string> memoDetails4 = this.GetMemoDetails(xmlNodes8);
							List<frmMemoListTasks.TreeNodeText> treeNodeTexts8 = new List<frmMemoListTasks.TreeNodeText>();
							item = new string[] { memoDetails4[0], "\r\n", memoDetails4[1], "\r\n", this.GetMultilingualValue(memoDetails4[2]) };
							treeNodeTexts8.Add(new frmMemoListTasks.TreeNodeText(string.Concat(item)));
							if (memoDetails4[2].ToUpper() == "TEXT")
							{
								if (this.strTextMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails4[2].ToUpper() == "REFERENCE")
							{
								if (this.strReferenceMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails4[2].ToUpper() == "HYPERLINK")
							{
								if (this.strHyperlinkMemoState.ToUpper() == "FALSE")
								{
									continue;
								}
							}
							else if (memoDetails4[2].ToUpper() == "PROGRAM" && this.strProgramMemoState.ToUpper() == "FALSE")
							{
								continue;
							}
							foreach (frmMemoListTasks.TreeNodeText treeNodeText8 in treeNodeTexts8)
							{
								TreeNode treeNode17 = new TreeNode();
								item = new string[] { memoDetails4[0], "|", memoDetails4[1], "|", memoDetails4[2], "|", memoDetails4[3] };
								treeNode17.Name = string.Concat(item);
								treeNode17.Tag = treeNodeText8;
								treeNode17.Text = this.MaxWidthString(graphic2, font2, treeNodeText8.TextList);
								this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Add(treeNode17);
							}
						}
					}
				}
				catch (Exception exception8)
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
			if (this.tvMemoTypes.Nodes["AdminMemo"].Nodes.Count <= 0 || !Settings.Default.EnableAdminMemo)
			{
				this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes[0];
				return;
			}
			this.tvMemoTypes.SelectedNode = this.tvMemoTypes.Nodes["AdminMemo"].Nodes[0];
		}

		private void LoadResources()
		{
			this.lblTasksTitle.Text = this.GetResource("Tasks", "TASKS", ResourceType.LABEL);
			this.lblView.Text = this.GetResource("View Options", "VIEW_OPTIONS", ResourceType.LABEL);
			this.lblLocalMemo.Text = this.GetResource("Local Memos", "LOCAL_MEMOS", ResourceType.LABEL);
			this.lblGlobalMemo.Text = this.GetResource("Global Memo", "GLOBAL_MEMO", ResourceType.LABEL);
			this.lblPartMemo.Text = this.GetResource("Part Memo", "PART_MEMO", ResourceType.LABEL);
			this.lblPictureMemo.Text = this.GetResource("Picture Memo", "PICTURE_MEMO", ResourceType.LABEL);
			this.lblAllMemo.Text = this.GetResource("All", "ALL", ResourceType.LABEL);
			this.lblBothMemo.Text = this.GetResource("Both", "BOTH", ResourceType.LABEL);
			this.lblAdminMemo.Text = this.GetResource("Admin Memos", "ADMIN_MEMOS", ResourceType.LABEL);
			this.lblTypes.Text = this.GetResource("Memo Types", "MEMO_TYPE", ResourceType.LABEL);
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

		public void ShowTask(ViewAllMemoTasks task)
		{
			this.intMemoType = this.GetMemoType();
			if (this.intMemoType != 2)
			{
				switch (task)
				{
					case ViewAllMemoTasks.Local:
					{
						this.lblLocalMemo_Click(null, null);
						break;
					}
					case ViewAllMemoTasks.Global:
					{
						this.lblGlobalMemo_Click(null, null);
						break;
					}
					case ViewAllMemoTasks.All:
					{
						this.lblAllMemo_Click(null, null);
						break;
					}
				}
			}
			this.lblBothMemo_Click(null, null);
		}

		private void tvMemoTypes_AfterSelect(object sender, TreeViewEventArgs e)
		{
			try
			{
				this.frmParent.objFrmMemoLocal.ClearItems(true, false, true);
				this.frmParent.objFrmMemoGlobal.ClearItems(true, false, true);
				this.frmParent.objFrmMemoAdmin.ClearItems(true, false, true);
				if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "LOCALMEMO")
				{
					if (Settings.Default.EnableLocalMemo)
					{
						this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
						this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
					}
					else
					{
						this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "GLOBALMEMO")
				{
					if (Settings.Default.EnableGlobalMemo)
					{
						this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
						this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
						this.frmParent.objFrmMemoGlobal.Show();
						this.lblGlobalMemo_Click(null, null);
						this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
					}
					else
					{
						this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Name.ToUpper() == "ADMINMEMO")
				{
					if (Settings.Default.EnableAdminMemo)
					{
						this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
						this.frmParent.objFrmMemoAdmin.pnlBottom.Dock = DockStyle.Fill;
						this.frmParent.objFrmMemoAdmin.Show();
						this.lblAdminMemo_Click(null, null);
						this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
					}
					else
					{
						this.tvMemoTypes.SelectedNode = this.tnPreviouisNode;
					}
				}
				else if (this.tvMemoTypes.SelectedNode.Parent.Name.ToUpper() == "LOCALMEMO")
				{
					this.frmParent.objFrmMemoLocal.pnlTop.Visible = false;
					this.frmParent.objFrmMemoLocal.pnlBottom.Dock = DockStyle.Fill;
					string[] strArrays = this.tvMemoTypes.SelectedNode.Name.Split(new char[] { '|' });
					string str = strArrays[0];
					string str1 = strArrays[1];
					string str2 = strArrays[2];
					string str3 = strArrays[3];
					if (str.ToUpper().Contains("PIPESIGN"))
					{
						str = str.Replace("PIPESIGN", "|");
					}
					if (str3.ToUpper().Contains("PIPESIGN"))
					{
						str3 = str3.Replace("PIPESIGN", "|");
					}
					if ((int)strArrays.Length > 4)
					{
						str3 = string.Concat(str3, "|", strArrays[4]);
					}
					if (str2.ToUpper().Trim() == "TEXT")
					{
						this.frmParent.objFrmMemoLocal.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.rtbTxtMemo.Text = str3;
						this.frmParent.objFrmMemoLocal.lblTxtMemoDate.Text = str1;
						this.frmParent.objFrmMemoLocal.pnlTxtMemo.BringToFront();
					}
					else if (str2.ToUpper().Trim() == "REFERENCE")
					{
						this.frmParent.objFrmMemoLocal.SetTabProperty("REFRENCE");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.lblRefMemoDate.Text = str1;
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
					else if (str2.ToUpper().Trim() == "HYPERLINK")
					{
						this.frmParent.objFrmMemoLocal.SetTabProperty("HYPERLINK");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.pnlHypMemo.BringToFront();
						this.frmParent.objFrmMemoLocal.txtDescription.Text = str;
						this.frmParent.objFrmMemoLocal.txtHypMemoUrl.Text = str3;
						this.frmParent.objFrmMemoLocal.lblHypMemoDate.Text = str1;
					}
					else if (str2.ToUpper().Trim() == "PROGRAM")
					{
						this.frmParent.objFrmMemoLocal.SetTabProperty("PROGRAME");
						this.frmParent.objFrmMemoLocal.Show();
						this.frmParent.objFrmMemoLocal.pnlPrgMemo.BringToFront();
						this.frmParent.objFrmMemoLocal.lblPrgMemoDate.Text = str1;
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
					this.frmParent.objFrmMemoGlobal.Show();
					this.lblGlobalMemo_Click(null, null);
					this.frmParent.objFrmMemoGlobal.pnlTop.Visible = false;
					this.frmParent.objFrmMemoGlobal.pnlBottom.Dock = DockStyle.Fill;
					string[] strArrays5 = this.tvMemoTypes.SelectedNode.Name.Split(new char[] { '|' });
					string str4 = strArrays5[0];
					string str5 = strArrays5[1];
					string str6 = strArrays5[2];
					string str7 = strArrays5[3];
					if (str4.ToUpper().Contains("PIPESIGN"))
					{
						str4 = str4.Replace("PIPESIGN", "|");
					}
					if (str7.ToUpper().Contains("PIPESIGN"))
					{
						str7 = str7.Replace("PIPESIGN", "|");
					}
					if ((int)strArrays5.Length > 4)
					{
						str7 = string.Concat(str7, "|", strArrays5[4]);
					}
					if (str6.ToUpper().Trim() == "TEXT")
					{
						this.frmParent.objFrmMemoGlobal.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.rtbTxtMemo.Text = str7;
						this.frmParent.objFrmMemoGlobal.lblTxtMemoDate.Text = str5;
						this.frmParent.objFrmMemoGlobal.pnlTxtMemo.BringToFront();
					}
					else if (str6.ToUpper().Trim() == "REFERENCE")
					{
						this.frmParent.objFrmMemoGlobal.SetTabProperty("REFRENCE");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.lblRefMemoDate.Text = str5;
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
					else if (str6.ToUpper().Trim() == "HYPERLINK")
					{
						this.frmParent.objFrmMemoGlobal.SetTabProperty("HYPERLINK");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.pnlHypMemo.BringToFront();
						this.frmParent.objFrmMemoGlobal.txtDescription.Text = str4;
						this.frmParent.objFrmMemoGlobal.txtHypMemoUrl.Text = str7;
						this.frmParent.objFrmMemoGlobal.lblHypMemoDate.Text = str5;
					}
					else if (str6.ToUpper().Trim() == "PROGRAM")
					{
						this.frmParent.objFrmMemoGlobal.SetTabProperty("PROGRAME");
						this.frmParent.objFrmMemoGlobal.Show();
						this.frmParent.objFrmMemoGlobal.pnlPrgMemo.BringToFront();
						this.frmParent.objFrmMemoGlobal.lblPrgMemoDate.Text = str5;
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
					this.lblAdminMemo_Click(null, null);
					this.frmParent.objFrmMemoAdmin.Show();
					this.frmParent.objFrmMemoAdmin.pnlTop.Visible = false;
					this.frmParent.objFrmMemoAdmin.pnlBottom.Dock = DockStyle.Fill;
					string[] strArrays10 = this.tvMemoTypes.SelectedNode.Name.Split(new char[] { '|' });
					string str8 = strArrays10[0];
					string str9 = strArrays10[1];
					string str10 = strArrays10[2];
					string str11 = strArrays10[3];
					if ((int)strArrays10.Length > 4)
					{
						str11 = string.Concat(str11, "|", strArrays10[4]);
					}
					if (str10.ToUpper().Trim() == "TEXT")
					{
						this.frmParent.objFrmMemoAdmin.SetTabProperty("TEXT");
						this.frmParent.objFrmMemoAdmin.Show();
						this.frmParent.objFrmMemoAdmin.rtbTxtMemo.Text = str11;
						this.frmParent.objFrmMemoAdmin.lblTxtMemoDate.Text = str9;
						this.frmParent.objFrmMemoAdmin.pnlTxtMemo.BringToFront();
					}
					else if (str10.ToUpper().Trim() == "REFERENCE")
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
					else if (str10.ToUpper().Trim() == "HYPERLINK")
					{
						this.frmParent.objFrmMemoAdmin.SetTabProperty("HYPERLINK");
						this.frmParent.objFrmMemoAdmin.Show();
						this.frmParent.objFrmMemoAdmin.pnlHypMemo.BringToFront();
						this.frmParent.objFrmMemoAdmin.txtDescription.Text = str8;
						this.frmParent.objFrmMemoAdmin.txtHypMemoUrl.Text = str11;
						this.frmParent.objFrmMemoAdmin.lblHypMemoDate.Text = str9;
					}
					else if (str10.ToUpper().Trim() == "PROGRAM")
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
			catch (Exception exception)
			{
			}
		}

		private void tvMemoTypes_Click(object sender, EventArgs e)
		{
			this.tnPreviouisNode = this.tvMemoTypes.SelectedNode;
		}

		private void tvMemoTypes_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			string str = (e.Node.Tag == null ? e.Node.Text : ((frmMemoListTasks.TreeNodeText)e.Node.Tag).DisplayText);
			System.Drawing.Font nodeFont = e.Node.NodeFont ?? e.Node.TreeView.Font;
			Color foreColor = e.Node.ForeColor;
			if (foreColor == Color.Empty)
			{
				foreColor = e.Node.TreeView.ForeColor;
			}
			int x = e.Bounds.X;
			int y = e.Bounds.Y;
			Rectangle bounds = this.tvMemoTypes.SelectedNode.Bounds;
			Rectangle rectangle = e.Bounds;
			Rectangle rectangle1 = new Rectangle(x, y, bounds.Width + 15, rectangle.Height);
			if (e.Node != e.Node.TreeView.SelectedNode)
			{
				TextRenderer.DrawText(e.Graphics, str, nodeFont, e.Bounds, foreColor, TextFormatFlags.VerticalCenter);
				return;
			}
			foreColor = SystemColors.HighlightText;
			e.Graphics.FillRectangle(new SolidBrush(Settings.Default.appHighlightBackColor), rectangle1);
			TextRenderer.DrawText(e.Graphics, str, nodeFont, e.Bounds, Settings.Default.appHighlightForeColor, TextFormatFlags.VerticalCenter);
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblTasksTitle.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
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
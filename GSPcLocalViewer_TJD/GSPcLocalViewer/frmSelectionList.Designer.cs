using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
	public class frmSelectionList : DockContent
	{
		private frmViewer frmParent;

		private string attPartNoElement;

		private string sParentPageArguments;

		private string sGoToPageArgs;

		private string gstrSelectionListHeader;

		private string gstrSelectionListColSequence;

		private IContainer components;

		private Panel pnlForm;

		public DataGridView dgPartslist;

		private PictureBox picLoading;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private ToolStrip toolStrip1;

		private ToolStripButton tsBtnAdd;

		private ToolStripButton tsBtnDeleteSelection;

		private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripSeparator toolStripSeparator2;

		private System.Windows.Forms.ContextMenuStrip cmsSelectionlist;

		private ToolStripMenuItem copyToClipboardToolStripMenuItem;

		private ToolStripMenuItem commaSeparatedToolStripMenuItem;

		private ToolStripMenuItem tabSeparatedToolStripMenuItem;

		private ToolStripMenuItem exportToFileToolStripMenuItem;

		private ToolStripMenuItem commaSeparatedToolStripMenuItem1;

		private ToolStripMenuItem tabSeparatedToolStripMenuItem1;

		private SaveFileDialog dlgSaveFile;

		private ToolStripButton tsbSelectAll;

		private ToolStripButton tsbClearSelection;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem selectAllToolStripMenuItem;

		private ToolStripMenuItem tsmClearSelection;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem deleteSelectionToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem goToPageToolStripMenuItem;

		private ToolStripButton tsBtnGoToPage;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton tsBtnDeleteAll;

		private DataGridViewTextBoxColumn Column1;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripButton tsBtnThirdPartyBasket;

		private ToolStripButton tsPrint;

		public frmSelectionList(frmViewer frm)
		{
			this.InitializeComponent();
			this.OnOffFeatures();
			this.frmParent = frm;
			this.UpdateFont();
			this.LoadResources();
			this.LoadTitle();
			this.sGoToPageArgs = string.Empty;
			this.gstrSelectionListHeader = string.Empty;
			this.gstrSelectionListColSequence = string.Empty;
			this.tsPrint.Visible = Program.objAppFeatures.bPrint;
		}

		private void AddColumnsSelectionListTable()
		{
			try
			{
				string str = this.gstrSelectionListHeader;
				char[] chrArray = new char[] { ',' };
				string[] strArrays = str.TrimEnd(chrArray).Split(new char[] { ',' });
				for (int i = 0; i < (int)strArrays.Length; i++)
				{
					if (strArrays[i] != string.Empty)
					{
						DataColumn dataColumn = new DataColumn()
						{
							ColumnName = strArrays[i]
						};
						this.frmParent.gdtselectionListTable.Columns.Add(dataColumn);
					}
				}
			}
			catch
			{
			}
		}

		public void AddNewRecord(DataGridViewRow dRow)
		{
			try
			{
				this.dgPartslist.Rows.Add();
				foreach (DataGridViewColumn column in this.dgPartslist.Columns)
				{
					this.dgPartslist[column.Name, this.dgPartslist.Rows.Count - 1].Value = dRow.Cells[column.Name].Value;
					try
					{
						if (column.Name == "PART_SLIST_KEY")
						{
							this.dgPartslist[column.Name, this.dgPartslist.Rows.Count - 1].Value = dRow.Cells[column.Name].Value;
						}
					}
					catch
					{
					}
				}
				this.dgPartslist.Rows[this.dgPartslist.Rows.Count - 1].Tag = "Manual";
				this.dgPartslist.Rows[this.dgPartslist.Rows.Count - 1].Selected = false;
				this.dgPartslist.FirstDisplayedScrollingRowIndex = this.dgPartslist.Rows.Count - 1;
			}
			catch
			{
			}
		}

		public void ChangeQuantity(string sPartNumber, string sQuantity)
		{
			try
			{
				if (this.dgPartslist.Rows.Count > 0)
				{
					foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
					{
						if (row.Cells["PART_SLIST_KEY"].Value.ToString() != sPartNumber)
						{
							continue;
						}
						row.Cells["QTY"].Value = sQuantity;
						break;
					}
				}
			}
			catch
			{
			}
		}

		public void ClearSelectionList()
		{
			this.dgPartslist.Rows.Clear();
		}

		private void cmsSelectionlist_Opening(object sender, CancelEventArgs e)
		{
			if (this.dgPartslist.SelectedRows.Count <= 0)
			{
				this.exportToFileToolStripMenuItem.Enabled = false;
				this.deleteSelectionToolStripMenuItem.Enabled = false;
				this.copyToClipboardToolStripMenuItem.Enabled = false;
			}
			else
			{
				this.deleteSelectionToolStripMenuItem.Enabled = true;
				this.copyToClipboardToolStripMenuItem.Enabled = true;
				this.exportToFileToolStripMenuItem.Enabled = true;
			}
			if (this.dgPartslist.SelectedRows.Count <= 1 && this.dgPartslist.Rows.Count > 0)
			{
				this.goToPageToolStripMenuItem.Enabled = true;
				return;
			}
			this.goToPageToolStripMenuItem.Enabled = false;
		}

		private void commaSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, ",");
			if (dataGridViewText != string.Empty)
			{
				Clipboard.SetText(dataGridViewText);
			}
		}

		private void commaSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
			this.dlgSaveFile.RestoreDirectory = true;
			string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, ",");
			if (dataGridViewText != string.Empty && this.dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					StreamWriter streamWriter = File.CreateText(this.dlgSaveFile.FileName);
					streamWriter.Write(dataGridViewText);
					streamWriter.Close();
				}
				catch
				{
					MessageHandler.ShowWarning(this.GetResource("(E-SLT-EM001) Failed to export specified object", "(E-SLT-EM001)_EXPORT", ResourceType.POPUP_MESSAGE));
				}
			}
		}

		public void DeleteRow(string sPartNumber)
		{
			try
			{
				if (this.dgPartslist.Rows.Count > 0)
				{
					foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
					{
						if (row.Cells["PART_SLIST_KEY"].Value.ToString() != sPartNumber)
						{
							continue;
						}
						this.dgPartslist.Rows.Remove(row);
						break;
					}
				}
			}
			catch
			{
			}
		}

		private void deleteSelectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsBtnDeleteSelection_Click(null, null);
		}

		private void dgPartslist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.RowIndex != -1 && this.dgPartslist.Columns[this.dgPartslist.CurrentCell.ColumnIndex].Name.ToUpper() == "QTY" && this.dgPartslist.Rows.Count > 0)
				{
					this.frmParent.ShowQuantityScreen(this.dgPartslist["PART_SLIST_KEY", this.dgPartslist.CurrentRow.Index].Value.ToString());
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				this.sGoToPageArgs = this.dgPartslist.Rows[this.dgPartslist.HitTest(e.X, e.Y).RowIndex].Tag.ToString();
			}
			catch
			{
			}
		}

		private void dgPartslist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			try
			{
				if (this.dgPartslist.Rows.Count <= 0)
				{
					this.tsBtnDeleteAll.Enabled = false;
					this.tsbSelectAll.Enabled = false;
					this.tsBtnDeleteAll.Enabled = false;
					this.tsBtnGoToPage.Enabled = false;
					this.tsPrint.Enabled = false;
				}
				else
				{
					this.tsBtnDeleteAll.Enabled = true;
					this.tsbSelectAll.Enabled = true;
					this.tsBtnDeleteAll.Enabled = true;
					this.tsBtnGoToPage.Enabled = true;
					this.tsPrint.Enabled = true;
				}
				if (!this.frmParent.bIsSelectionListPrint)
				{
					this.tsPrint.Enabled = false;
				}
				else
				{
					this.tsPrint.Enabled = true;
				}
				this.dgPartslist["AutoIndexColumn", e.RowIndex].Value = e.RowIndex;
			}
			catch
			{
			}
		}

		private void dgPartslist_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			try
			{
				if (this.dgPartslist.Rows.Count <= 0)
				{
					this.tsBtnDeleteAll.Enabled = false;
					this.tsbSelectAll.Enabled = false;
					this.tsBtnGoToPage.Enabled = false;
					this.tsPrint.Enabled = false;
				}
				else
				{
					this.tsBtnDeleteAll.Enabled = true;
					this.tsbSelectAll.Enabled = true;
					this.tsBtnGoToPage.Enabled = true;
					this.tsPrint.Enabled = true;
				}
				if (!this.frmParent.bIsSelectionListPrint)
				{
					this.tsPrint.Enabled = false;
				}
				else
				{
					this.tsPrint.Enabled = true;
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_SelectionChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dgPartslist.SelectedRows.Count != 0)
				{
					this.tsBtnDeleteSelection.Enabled = true;
					this.tsbClearSelection.Enabled = true;
				}
				else
				{
					this.tsBtnDeleteSelection.Enabled = false;
					this.tsbClearSelection.Enabled = false;
				}
				if (this.dgPartslist.SelectedRows.Count != 1)
				{
					this.tsBtnGoToPage.Enabled = false;
				}
				else
				{
					this.tsBtnGoToPage.Enabled = true;
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			try
			{
				if (e.Column.Name.ToUpper() != "LINKNUMBER")
				{
					e.SortResult = string.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
				}
				else
				{
					int num = 0;
					int num1 = 0;
					num = int.Parse(this.dgPartslist.Rows[e.RowIndex1].Cells["AutoIndexColumn"].Value.ToString());
					num1 = int.Parse(this.dgPartslist.Rows[e.RowIndex2].Cells["AutoIndexColumn"].Value.ToString());
					if (num > num1)
					{
						e.SortResult = 1;
					}
					else if (num >= num1)
					{
						e.SortResult = 0;
					}
					else
					{
						e.SortResult = -1;
					}
				}
				e.Handled = true;
			}
			catch
			{
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

		private string FindAttributeKey(string attVal, XmlNode xListSchema)
		{
			string name;
			try
			{
				foreach (XmlAttribute attribute in xListSchema.Attributes)
				{
					if (attribute.Value != attVal)
					{
						continue;
					}
					name = attribute.Name;
					return name;
				}
				name = string.Empty;
			}
			catch
			{
				name = string.Empty;
			}
			return name;
		}

		private void frmSelectionList_Load(object sender, EventArgs e)
		{
		}

		private void frmSelectionList_VisibleChanged(object sender, EventArgs e)
		{
			this.frmParent.selectionListToolStripMenuItem.Checked = base.Visible;
		}

		private string GetDataGridViewText(ref DataGridView GridView, bool IncludeHeader, bool SelectedRows, string Delimiter)
		{
			string empty = string.Empty;
			bool flag = false;
			int num = 0;
			while (num < GridView.Columns.Count)
			{
				if (!GridView.Columns[num].Visible)
				{
					num++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return string.Empty;
			}
			if (GridView.Rows.Count == 0)
			{
				return string.Empty;
			}
			if (SelectedRows && GridView.SelectedRows.Count == 0)
			{
				return string.Empty;
			}
			if (IncludeHeader)
			{
				for (int i = 0; i < GridView.Columns.Count; i++)
				{
					if (GridView.Columns[i].Visible && GridView.Columns[i].GetType().ToString() == typeof(DataGridViewTextBoxColumn).ToString())
					{
						empty = string.Concat(empty, this.GetWriteableValue(GridView.Columns[i].HeaderText), Delimiter);
					}
				}
				empty = string.Concat(empty.Remove(empty.Length - Delimiter.Length, Delimiter.Length), "\r\n");
			}
			for (int j = 0; j < GridView.Rows.Count; j++)
			{
				if (!SelectedRows)
				{
					for (int k = 0; k < GridView.Columns.Count; k++)
					{
						if (GridView.Columns[k].Visible && GridView.Columns[k].GetType().ToString() == typeof(DataGridViewTextBoxColumn).ToString())
						{
							empty = string.Concat(empty, this.GetWriteableValue(GridView.Rows[j].Cells[k].Value), Delimiter);
						}
					}
					empty = string.Concat(empty.Remove(empty.Length - Delimiter.Length, Delimiter.Length), "\r\n");
				}
				else if (GridView.SelectedRows.Contains(GridView.Rows[j]))
				{
					for (int l = 0; l < GridView.Columns.Count; l++)
					{
						if (GridView.Columns[l].Visible && GridView.Columns[l].GetType().ToString() == typeof(DataGridViewTextBoxColumn).ToString())
						{
							empty = string.Concat(empty, this.GetWriteableValue(GridView.Rows[j].Cells[l].Value), Delimiter);
						}
					}
					empty = string.Concat(empty.Remove(empty.Length - Delimiter.Length, Delimiter.Length), "\r\n");
				}
			}
			return empty;
		}

		private string GetDGHeaderCellValue(string sKey, string sDefaultHeaderValue)
		{
			string empty = string.Empty;
			bool flag = false;
			if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
			{
				empty = sDefaultHeaderValue;
			}
			else
			{
				string str = string.Concat(Settings.Default.appLanguage, "_GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini");
				if (!File.Exists(string.Concat(Application.StartupPath, "\\Language XMLs\\", str)))
				{
					empty = sDefaultHeaderValue;
				}
				else
				{
					TextReader streamReader = new StreamReader(string.Concat(Application.StartupPath, "\\Language XMLs\\", str));
					while (true)
					{
						string str1 = streamReader.ReadLine();
						string str2 = str1;
						if (str1 == null)
						{
							break;
						}
						if (str2.ToUpper() == "[SLIST_SETTINGS]")
						{
							flag = true;
						}
						else if (str2.Contains("=") && flag)
						{
							string[] strArrays = new string[] { "=" };
							string[] strArrays1 = str2.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
							if (strArrays1[0].ToString().ToUpper() == sKey.ToUpper())
							{
								empty = strArrays1[1];
								flag = false;
								break;
							}
						}
						else if (str2.Contains("["))
						{
							flag = false;
						}
					}
					if (empty == "")
					{
						empty = sDefaultHeaderValue;
					}
					streamReader.Close();
				}
			}
			return empty;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MAIN_FORM']");
				str = string.Concat(str, "/Screen[@Name='SELECTION_LIST']");
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

		public DataGridView GetSelectionList()
		{
			return this.dgPartslist;
		}

		private string GetWriteableValue(object o)
		{
			if (o == null || o == Convert.DBNull)
			{
				return "";
			}
			if (o.ToString().IndexOf(",") == -1)
			{
				return o.ToString();
			}
			return string.Concat("\"", o.ToString(), "\"");
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmSelectionList));
			this.pnlForm = new Panel();
			this.dgPartslist = new DataGridView();
			this.Column1 = new DataGridViewTextBoxColumn();
			this.cmsSelectionlist = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.selectAllToolStripMenuItem = new ToolStripMenuItem();
			this.tsmClearSelection = new ToolStripMenuItem();
			this.toolStripSeparator5 = new ToolStripSeparator();
			this.copyToClipboardToolStripMenuItem = new ToolStripMenuItem();
			this.commaSeparatedToolStripMenuItem = new ToolStripMenuItem();
			this.tabSeparatedToolStripMenuItem = new ToolStripMenuItem();
			this.exportToFileToolStripMenuItem = new ToolStripMenuItem();
			this.commaSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
			this.tabSeparatedToolStripMenuItem1 = new ToolStripMenuItem();
			this.toolStripSeparator3 = new ToolStripSeparator();
			this.deleteSelectionToolStripMenuItem = new ToolStripMenuItem();
			this.toolStripSeparator6 = new ToolStripSeparator();
			this.goToPageToolStripMenuItem = new ToolStripMenuItem();
			this.toolStrip1 = new ToolStrip();
			this.tsBtnGoToPage = new ToolStripButton();
			this.toolStripSeparator7 = new ToolStripSeparator();
			this.tsBtnAdd = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tsbClearSelection = new ToolStripButton();
			this.tsbSelectAll = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.tsBtnDeleteSelection = new ToolStripButton();
			this.toolStripSeparator8 = new ToolStripSeparator();
			this.tsBtnDeleteAll = new ToolStripButton();
			this.tsPrint = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.tsBtnThirdPartyBasket = new ToolStripButton();
			this.picLoading = new PictureBox();
			this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.dlgSaveFile = new SaveFileDialog();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.pnlForm.SuspendLayout();
			((ISupportInitialize)this.dgPartslist).BeginInit();
			this.cmsSelectionlist.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.dgPartslist);
			this.pnlForm.Controls.Add(this.toolStrip1);
			this.pnlForm.Controls.Add(this.picLoading);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(625, 266);
			this.pnlForm.TabIndex = 4;
			this.dgPartslist.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.dgPartslist.AllowUserToAddRows = false;
			this.dgPartslist.AllowUserToDeleteRows = false;
			this.dgPartslist.AllowUserToResizeRows = false;
			this.dgPartslist.BackgroundColor = Color.White;
			this.dgPartslist.BorderStyle = BorderStyle.None;
			this.dgPartslist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgPartslist.Columns.AddRange(new DataGridViewColumn[] { this.Column1 });
			this.dgPartslist.ContextMenuStrip = this.cmsSelectionlist;
			this.dgPartslist.Dock = DockStyle.Fill;
			this.dgPartslist.Location = new Point(0, 25);
			this.dgPartslist.Name = "dgPartslist";
			this.dgPartslist.RowHeadersVisible = false;
			this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgPartslist.Size = new System.Drawing.Size(623, 239);
			this.dgPartslist.TabIndex = 20;
			this.dgPartslist.MouseDown += new MouseEventHandler(this.dgPartslist_MouseDown);
			this.dgPartslist.SortCompare += new DataGridViewSortCompareEventHandler(this.dgPartslist_SortCompare);
			this.dgPartslist.CellDoubleClick += new DataGridViewCellEventHandler(this.dgPartslist_CellDoubleClick);
			this.dgPartslist.RowsAdded += new DataGridViewRowsAddedEventHandler(this.dgPartslist_RowsAdded);
			this.dgPartslist.RowsRemoved += new DataGridViewRowsRemovedEventHandler(this.dgPartslist_RowsRemoved);
			this.dgPartslist.SelectionChanged += new EventHandler(this.dgPartslist_SelectionChanged);
			this.Column1.HeaderText = "SelectionList";
			this.Column1.Name = "Column1";
			this.Column1.Visible = false;
			this.Column1.Width = 92;
			ToolStripItemCollection items = this.cmsSelectionlist.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.selectAllToolStripMenuItem, this.tsmClearSelection, this.toolStripSeparator5, this.copyToClipboardToolStripMenuItem, this.exportToFileToolStripMenuItem, this.toolStripSeparator3, this.deleteSelectionToolStripMenuItem, this.toolStripSeparator6, this.goToPageToolStripMenuItem };
			items.AddRange(toolStripItemArray);
			this.cmsSelectionlist.Name = "cmsPartslist";
			this.cmsSelectionlist.Size = new System.Drawing.Size(163, 154);
			this.cmsSelectionlist.Opening += new CancelEventHandler(this.cmsSelectionlist_Opening);
			this.selectAllToolStripMenuItem.Enabled = false;
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.selectAllToolStripMenuItem.Text = "Select All";
			this.selectAllToolStripMenuItem.Click += new EventHandler(this.selectAllToolStripMenuItem_Click);
			this.tsmClearSelection.Enabled = false;
			this.tsmClearSelection.Name = "tsmClearSelection";
			this.tsmClearSelection.Size = new System.Drawing.Size(162, 22);
			this.tsmClearSelection.Text = "Clear Selection";
			this.tsmClearSelection.Click += new EventHandler(this.tsmClearSelection_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(159, 6);
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
			ToolStripItemCollection toolStripItemCollections = this.exportToFileToolStripMenuItem.DropDownItems;
			ToolStripItem[] toolStripItemArray2 = new ToolStripItem[] { this.commaSeparatedToolStripMenuItem1, this.tabSeparatedToolStripMenuItem1 };
			toolStripItemCollections.AddRange(toolStripItemArray2);
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
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(159, 6);
			this.deleteSelectionToolStripMenuItem.Name = "deleteSelectionToolStripMenuItem";
			this.deleteSelectionToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.deleteSelectionToolStripMenuItem.Text = "Delete Selection";
			this.deleteSelectionToolStripMenuItem.Click += new EventHandler(this.deleteSelectionToolStripMenuItem_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(159, 6);
			this.goToPageToolStripMenuItem.Enabled = false;
			this.goToPageToolStripMenuItem.Name = "goToPageToolStripMenuItem";
			this.goToPageToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
			this.goToPageToolStripMenuItem.Text = "Go To Page";
			this.goToPageToolStripMenuItem.Click += new EventHandler(this.openPageToolStripMenuItem_Click);
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			ToolStripItemCollection items1 = this.toolStrip1.Items;
			ToolStripItem[] toolStripItemArray3 = new ToolStripItem[] { this.tsBtnGoToPage, this.toolStripSeparator7, this.tsBtnAdd, this.toolStripSeparator2, this.tsbClearSelection, this.tsbSelectAll, this.toolStripSeparator4, this.tsBtnDeleteSelection, this.toolStripSeparator8, this.tsBtnDeleteAll, this.tsPrint, this.toolStripSeparator1, this.tsBtnThirdPartyBasket };
			items1.AddRange(toolStripItemArray3);
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.toolStrip1.Size = new System.Drawing.Size(623, 25);
			this.toolStrip1.TabIndex = 21;
			this.tsBtnGoToPage.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnGoToPage.Enabled = false;
			this.tsBtnGoToPage.Image = GSPcLocalViewer.Properties.Resources.SelectionListGotopage;
			this.tsBtnGoToPage.ImageTransparentColor = Color.Magenta;
			this.tsBtnGoToPage.Name = "tsBtnGoToPage";
			this.tsBtnGoToPage.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsBtnGoToPage.Size = new System.Drawing.Size(23, 22);
			this.tsBtnGoToPage.Text = "Go To Page";
			this.tsBtnGoToPage.EnabledChanged += new EventHandler(this.tsBtnGoToPage_EnabledChanged);
			this.tsBtnGoToPage.Click += new EventHandler(this.tsBtnOpenParentPage_Click);
			this.toolStripSeparator7.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
			this.tsBtnAdd.Alignment = ToolStripItemAlignment.Right;
			this.tsBtnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnAdd.Image = GSPcLocalViewer.Properties.Resources.SelectionList_Add_record;
			this.tsBtnAdd.ImageTransparentColor = Color.Magenta;
			this.tsBtnAdd.Name = "tsBtnAdd";
			this.tsBtnAdd.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsBtnAdd.Size = new System.Drawing.Size(23, 22);
			this.tsBtnAdd.Text = "Add Record";
			this.tsBtnAdd.Click += new EventHandler(this.tsBtnAdd_Click);
			this.toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.tsbClearSelection.Alignment = ToolStripItemAlignment.Right;
			this.tsbClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbClearSelection.Enabled = false;
			this.tsbClearSelection.Image = GSPcLocalViewer.Properties.Resources.SelectionList_clear_selection;
			this.tsbClearSelection.ImageTransparentColor = Color.Magenta;
			this.tsbClearSelection.Name = "tsbClearSelection";
			this.tsbClearSelection.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbClearSelection.Size = new System.Drawing.Size(23, 22);
			this.tsbClearSelection.Text = "Clear Selection";
			this.tsbClearSelection.EnabledChanged += new EventHandler(this.tsbClearSelection_EnabledChanged);
			this.tsbClearSelection.Click += new EventHandler(this.tsbClearSelection_Click);
			this.tsbSelectAll.Alignment = ToolStripItemAlignment.Right;
			this.tsbSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSelectAll.Enabled = false;
			this.tsbSelectAll.Image = GSPcLocalViewer.Properties.Resources.SelectionList_select_all;
			this.tsbSelectAll.ImageTransparentColor = Color.Magenta;
			this.tsbSelectAll.Name = "tsbSelectAll";
			this.tsbSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbSelectAll.Size = new System.Drawing.Size(23, 22);
			this.tsbSelectAll.Text = "Select All";
			this.tsbSelectAll.EnabledChanged += new EventHandler(this.tsbSelectAll_EnabledChanged);
			this.tsbSelectAll.Click += new EventHandler(this.tsbSelectAll_Click);
			this.toolStripSeparator4.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			this.tsBtnDeleteSelection.Alignment = ToolStripItemAlignment.Right;
			this.tsBtnDeleteSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnDeleteSelection.Enabled = false;
			this.tsBtnDeleteSelection.Image = GSPcLocalViewer.Properties.Resources.SelectionList_DeleteSelection;
			this.tsBtnDeleteSelection.ImageTransparentColor = Color.Magenta;
			this.tsBtnDeleteSelection.Name = "tsBtnDeleteSelection";
			this.tsBtnDeleteSelection.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsBtnDeleteSelection.Size = new System.Drawing.Size(23, 22);
			this.tsBtnDeleteSelection.Text = "Delete Selection";
			this.tsBtnDeleteSelection.EnabledChanged += new EventHandler(this.tsBtnDeleteSelection_EnabledChanged);
			this.tsBtnDeleteSelection.Click += new EventHandler(this.tsBtnDeleteSelection_Click);
			this.toolStripSeparator8.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
			this.tsBtnDeleteAll.Alignment = ToolStripItemAlignment.Right;
			this.tsBtnDeleteAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnDeleteAll.Enabled = false;
			this.tsBtnDeleteAll.Image = GSPcLocalViewer.Properties.Resources.SelectionList__DeleteAll;
			this.tsBtnDeleteAll.ImageTransparentColor = Color.Magenta;
			this.tsBtnDeleteAll.Name = "tsBtnDeleteAll";
			this.tsBtnDeleteAll.Size = new System.Drawing.Size(23, 22);
			this.tsBtnDeleteAll.Text = "toolStripButton1";
			this.tsBtnDeleteAll.Click += new EventHandler(this.toolStripButton1_Click);
			this.tsPrint.Alignment = ToolStripItemAlignment.Right;
			this.tsPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsPrint.Enabled = false;
			this.tsPrint.Image = GSPcLocalViewer.Properties.Resources.Print;
			this.tsPrint.ImageTransparentColor = Color.Magenta;
			this.tsPrint.Name = "tsPrint";
			this.tsPrint.Size = new System.Drawing.Size(23, 22);
			this.tsPrint.Text = "Print";
			this.tsPrint.Click += new EventHandler(this.tsPrint_Click);
			this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.tsBtnThirdPartyBasket.Alignment = ToolStripItemAlignment.Right;
			this.tsBtnThirdPartyBasket.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnThirdPartyBasket.Image = GSPcLocalViewer.Properties.Resources.basket;
			this.tsBtnThirdPartyBasket.ImageTransparentColor = Color.Magenta;
			this.tsBtnThirdPartyBasket.Name = "tsBtnThirdPartyBasket";
			this.tsBtnThirdPartyBasket.Size = new System.Drawing.Size(23, 22);
			this.tsBtnThirdPartyBasket.Text = "Basket";
			this.tsBtnThirdPartyBasket.Click += new EventHandler(this.tsBtnThirdPartyBasket_Click);
			this.picLoading.BackColor = Color.White;
			this.picLoading.Dock = DockStyle.Fill;
			this.picLoading.Image = GSPcLocalViewer.Properties.Resources.Loading1;
			this.picLoading.Location = new Point(0, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(623, 264);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 19;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.dataGridViewTextBoxColumn1.HeaderText = "SelectionList";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Visible = false;
			this.dataGridViewTextBoxColumn1.Width = 1000;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(625, 266);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "frmSelectionList";
			this.Text = "Selection List";
			base.VisibleChanged += new EventHandler(this.frmSelectionList_VisibleChanged);
			this.pnlForm.ResumeLayout(false);
			this.pnlForm.PerformLayout();
			((ISupportInitialize)this.dgPartslist).EndInit();
			this.cmsSelectionlist.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
		}

		public void LoadResources()
		{
			this.Text = string.Concat(this.GetResource("Selection List", "SELECTION_LIST", ResourceType.TITLE), "      ");
			ToolStripButton toolStripButton = this.tsBtnDeleteSelection;
			string resource = this.GetResource("Delete Selection", "DELETE_SELECTION", ResourceType.TOOLSTRIP);
			string str = resource;
			this.Text = resource;
			toolStripButton.Text = str;
			ToolStripButton toolStripButton1 = this.tsbSelectAll;
			string resource1 = this.GetResource("Select All", "SELECT_ALL", ResourceType.TOOLSTRIP);
			string str1 = resource1;
			this.Text = resource1;
			toolStripButton1.Text = str1;
			ToolStripButton toolStripButton2 = this.tsbClearSelection;
			string resource2 = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.TOOLSTRIP);
			string str2 = resource2;
			this.Text = resource2;
			toolStripButton2.Text = str2;
			ToolStripButton toolStripButton3 = this.tsBtnAdd;
			string resource3 = this.GetResource("Add Record", "ADD_RECORD", ResourceType.TOOLSTRIP);
			string str3 = resource3;
			this.Text = resource3;
			toolStripButton3.Text = str3;
			ToolStripMenuItem toolStripMenuItem = this.selectAllToolStripMenuItem;
			string resource4 = this.GetResource("Select All", "SELECT_ALL", ResourceType.CONTEXT_MENU);
			string str4 = resource4;
			this.Text = resource4;
			toolStripMenuItem.Text = str4;
			ToolStripMenuItem toolStripMenuItem1 = this.tsmClearSelection;
			string resource5 = this.GetResource("Clear Selected", "CLEAR_SELECTED", ResourceType.CONTEXT_MENU);
			string str5 = resource5;
			this.Text = resource5;
			toolStripMenuItem1.Text = str5;
			this.copyToClipboardToolStripMenuItem.Text = this.GetResource("Copy To Clipboard", "COPY_TO_CLIPBOARD", ResourceType.CONTEXT_MENU);
			this.commaSeparatedToolStripMenuItem.Text = this.GetResource("Comma Seperated", "COMMA_SEPERATED", ResourceType.CONTEXT_MENU);
			this.tabSeparatedToolStripMenuItem.Text = this.GetResource("Tab Seperated", "TAB_SEPERATED", ResourceType.CONTEXT_MENU);
			this.exportToFileToolStripMenuItem.Text = this.GetResource("Export To File", "EXPORT_TO_FILE", ResourceType.CONTEXT_MENU);
			this.commaSeparatedToolStripMenuItem1.Text = this.GetResource("Comma Seperated", "COMMA_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
			this.tabSeparatedToolStripMenuItem1.Text = this.GetResource("Tab Seperated", "TAB_SEPERATEDFILE", ResourceType.CONTEXT_MENU);
			this.deleteSelectionToolStripMenuItem.Text = this.GetResource("Delete Selection", "DELETE_SELECTION", ResourceType.CONTEXT_MENU);
			ToolStripButton toolStripButton4 = this.tsBtnDeleteAll;
			string resource6 = this.GetResource("Delete All", "DELETE_ALL", ResourceType.TOOLSTRIP);
			string str6 = resource6;
			this.Text = resource6;
			toolStripButton4.Text = str6;
			ToolStripButton toolStripButton5 = this.tsBtnGoToPage;
			string resource7 = this.GetResource("Go to Page", "GO_TO_PAGE", ResourceType.TOOLSTRIP);
			string str7 = resource7;
			this.Text = resource7;
			toolStripButton5.Text = str7;
			ToolStripMenuItem toolStripMenuItem2 = this.goToPageToolStripMenuItem;
			string resource8 = this.GetResource("Go to Page", "GO_TO_PAGE", ResourceType.CONTEXT_MENU);
			string str8 = resource8;
			this.Text = resource8;
			toolStripMenuItem2.Text = str8;
			ToolStripButton toolStripButton6 = this.tsPrint;
			string resource9 = this.GetResource("Print", "PRINT", ResourceType.TOOLSTRIP);
			string str9 = resource9;
			this.Text = resource9;
			toolStripButton6.Text = str9;
			this.tsBtnThirdPartyBasket.Text = this.GetResource("Basket", "BASKET", ResourceType.TOOLSTRIP);
			this.LoadTitle();
		}

		public void LoadSelectionList()
		{
			try
			{
				this.frmParent.gdtselectionListTable.Clear();
				if (this.frmParent.gdtselectionListTable.Columns.Count == 0)
				{
					this.AddColumnsSelectionListTable();
				}
				string str = this.gstrSelectionListHeader;
				char[] chrArray = new char[] { ',' };
				string[] strArrays = str.TrimEnd(chrArray).Split(new char[] { ',' });
				string str1 = this.gstrSelectionListColSequence;
				char[] chrArray1 = new char[] { ',' };
				string[] strArrays1 = str1.TrimEnd(chrArray1).Split(new char[] { ',' });
				foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
				{
					DataRow dataRow = this.frmParent.gdtselectionListTable.NewRow();
					for (int i = 0; i < (int)strArrays1.Length; i++)
					{
						if (row.Cells[strArrays1[i]].Value != null)
						{
							dataRow[strArrays[i]] = row.Cells[strArrays1[i]].Value.ToString();
						}
					}
					this.frmParent.gdtselectionListTable.Rows.Add(dataRow);
				}
			}
			catch
			{
			}
		}

		public void LoadTitle()
		{
			this.Text = string.Concat(this.GetResource("Selection List", "SELECTION_LIST", ResourceType.TITLE), "      ");
		}

		private void OnOffFeatures()
		{
			try
			{
				this.tsBtnThirdPartyBasket.Visible = Program.objAppFeatures.bThirdPartyBasket;
				this.toolStripSeparator1.Visible = Program.objAppFeatures.bThirdPartyBasket;
			}
			catch
			{
			}
		}

		private void openPageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.tsBtnOpenParentPage_Click(null, null);
		}

		private void OpenURLInBrowser(string sUrl, string sWebPageTitle)
		{
			string item = Program.iniGSPcLocal.items["SETTINGS", "BROWSER"];
			if (!(item != string.Empty) || item == null)
			{
				using (Process process = Process.Start("IExplore.exe", sUrl))
				{
					if (process != null)
					{
						IntPtr handle = process.Handle;
						frmSelectionList.SetForegroundWindow(process.Handle);
						frmSelectionList.SetActiveWindow(process.Handle);
					}
				}
			}
			else if (item.ToUpper() != "IEXPLORE")
			{
				try
				{
					RegistryReader registryReader = new RegistryReader();
					string str = registryReader.Read(string.Concat("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", item, ".exe"), string.Empty);
					if (str != null)
					{
						using (Process process1 = Process.Start(str, sUrl))
						{
							if (process1 != null)
							{
								IntPtr intPtr = process1.Handle;
								frmSelectionList.SetForegroundWindow(process1.Handle);
								frmSelectionList.SetActiveWindow(process1.Handle);
							}
						}
					}
					else
					{
						using (Process process2 = Process.Start(sUrl))
						{
							if (process2 != null)
							{
								IntPtr handle1 = process2.Handle;
								frmSelectionList.SetForegroundWindow(process2.Handle);
								frmSelectionList.SetActiveWindow(process2.Handle);
							}
						}
					}
				}
				catch (Exception exception)
				{
				}
			}
			else
			{
				RegistryReader registryReader1 = new RegistryReader();
				string empty = string.Empty;
				empty = registryReader1.Read(string.Concat("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\", item, ".exe"), string.Empty);
				if (empty != null)
				{
					using (Process process3 = Process.Start(empty, sUrl))
					{
						if (process3 != null)
						{
							IntPtr intPtr1 = process3.Handle;
							frmSelectionList.SetForegroundWindow(process3.Handle);
							frmSelectionList.SetActiveWindow(process3.Handle);
						}
					}
				}
				else
				{
					using (Process process4 = Process.Start(sUrl))
					{
						if (process4 != null)
						{
							IntPtr handle2 = process4.Handle;
							frmSelectionList.SetForegroundWindow(process4.Handle);
							frmSelectionList.SetActiveWindow(process4.Handle);
						}
					}
				}
			}
		}

		public bool PartInSelectionList(string sPartNumber, string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex)
		{
			try
			{
				if (this.dgPartslist.Rows.Count > 0)
				{
					foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
					{
						if (row.Cells["PART_SLIST_KEY"].Value == null || !(row.Cells["PART_SLIST_KEY"].Value.ToString() == sPartNumber))
						{
							continue;
						}
						string str = row.Tag.ToString();
						string[] strArrays = new string[] { "**" };
						str.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		public void ReLoadPartlistColumns()
		{
			if (this.dgPartslist.InvokeRequired)
			{
				this.dgPartslist.Invoke(new frmSelectionList.selListReloadeDelegate(this.ReLoadPartlistColumns));
				return;
			}
			IniFileIO iniFileIO = new IniFileIO();
			ArrayList arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "SLIST_SETTINGS");
			for (int i = 0; i < this.frmParent.PartListGridView.Columns.Count; i++)
			{
				try
				{
					if (this.frmParent.PartListGridView.Columns[i].GetType().ToString() == typeof(DataGridViewTextBoxColumn).ToString())
					{
						DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
						{
							HeaderCell = new DataGridViewColumnHeaderCell()
						};
						bool flag = false;
						if (!arrayLists.Contains(this.frmParent.PartListGridView.Columns[i].Tag) || !arrayLists.Contains(this.frmParent.PartListGridView.Columns[i].Name.ToUpper().ToString()))
						{
							int num = 0;
							while (num < this.dgPartslist.Columns.Count)
							{
								if (this.dgPartslist.Columns[num].HeaderText.ToUpper() == this.frmParent.PartListGridView.Columns[i].HeaderText.ToUpper() || this.dgPartslist.Columns[num].Tag.ToString().ToUpper() == this.frmParent.PartListGridView.Columns[i].Tag.ToString().ToUpper() || this.dgPartslist.Columns[num].Name.ToUpper() == this.frmParent.PartListGridView.Columns[i].Name.ToUpper())
								{
									flag = true;
									break;
								}
								else
								{
									num++;
								}
							}
						}
						if (!flag)
						{
							dataGridViewTextBoxColumn.HeaderText = this.frmParent.PartListGridView.Columns[i].HeaderText.ToString();
							dataGridViewTextBoxColumn.Name = this.frmParent.PartListGridView.Columns[i].Tag.ToString();
							dataGridViewTextBoxColumn.Tag = this.frmParent.PartListGridView.Columns[i].Tag;
							dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
							dataGridViewTextBoxColumn.Visible = false;
							dataGridViewTextBoxColumn.ToolTipText = "HIDDEN";
							this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn);
						}
					}
				}
				catch
				{
				}
			}
			try
			{
				foreach (DataGridViewColumn selectionListColumn in this.frmParent.GetSelectionListColumns())
				{
					try
					{
						if (selectionListColumn.GetType().ToString() == typeof(DataGridViewTextBoxColumn).ToString())
						{
							DataGridViewTextBoxColumn str = new DataGridViewTextBoxColumn()
							{
								HeaderCell = new DataGridViewColumnHeaderCell()
							};
							bool flag1 = false;
							int num1 = 0;
							while (num1 < this.dgPartslist.Columns.Count)
							{
								if (this.dgPartslist.Columns[num1].Name.ToUpper() != selectionListColumn.Name.ToUpper())
								{
									num1++;
								}
								else
								{
									flag1 = true;
									break;
								}
							}
							if (!flag1)
							{
								str.HeaderText = selectionListColumn.HeaderText.ToString();
								str.Name = selectionListColumn.Name;
								str.Tag = selectionListColumn.Tag;
								str.Visible = false;
								this.dgPartslist.Columns.Add(str);
							}
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			this.dgPartslist.AllowUserToResizeColumns = true;
		}

		public void SaveSelectionListColumnSizes()
		{
			IniFileIO iniFileIO = new IniFileIO();
			ArrayList arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "SLIST_SETTINGS");
			Dictionary<string, string> strs = new Dictionary<string, string>();
			foreach (string arrayList in arrayLists)
			{
				strs.Add(arrayList, iniFileIO.GetKeyValue("SLIST_SETTINGS", arrayList, string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini")));
			}
			try
			{
				if (!base.IsDisposed && arrayLists.Count > 0)
				{
					foreach (DataGridViewColumn column in this.dgPartslist.Columns)
					{
						string str = strs[column.Name].ToString();
						char[] chrArray = new char[] { '|' };
						if (str.Split(chrArray)[2] != "0")
						{
							string[] strArrays = str.Split(new char[] { '|' });
							strArrays[2] = column.Width.ToString();
							str = "";
							string[] strArrays1 = strArrays;
							for (int i = 0; i < (int)strArrays1.Length; i++)
							{
								str = string.Concat(str, strArrays1[i], "|");
							}
							str = str.TrimEnd(new char[] { '|' });
						}
						Program.iniServers[this.frmParent.ServerId].UpdateItem("SLIST_SETTINGS", column.Name, str);
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.dgPartslist.SelectAll();
		}

		public void selListAddRemoveRecord(int ServerId, XmlNode xSchemaNode, DataGridViewRow dr, bool bAdd, string sParentPageArguments1)
		{
			try
			{
				this.ReLoadPartlistColumns();
				this.sParentPageArguments = sParentPageArguments1;
				object[] value = new object[this.dgPartslist.Columns.Count + 1];
				foreach (DataGridViewColumn column in this.dgPartslist.Columns)
				{
					string empty = string.Empty;
					foreach (XmlAttribute attribute in xSchemaNode.Attributes)
					{
						try
						{
							if (attribute.Value.ToUpper() == "PART_SLIST_KEY")
							{
								this.attPartNoElement = attribute.Name;
							}
							if (attribute.Value.ToUpper() == column.Tag.ToString().ToUpper())
							{
								empty = attribute.Value;
								break;
							}
						}
						catch
						{
						}
					}
					if (!(column.Name.ToUpper() != "QTY") || !(column.Name.ToUpper() != "PART_SLIST_KEY") || !(empty != string.Empty))
					{
						if (column.Name.ToUpper() != "PART_SLIST_KEY")
						{
							continue;
						}
						value[column.Index] = dr.Cells["PART_SLIST_KEY"].Value;
					}
					else
					{
						try
						{
							string str = this.FindAttributeKey(empty, xSchemaNode);
							if (dr.Cells[str] != null)
							{
								value[column.Index] = dr.Cells[str].Value;
							}
						}
						catch
						{
						}
					}
				}
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				dataGridViewRow.CreateCells(this.dgPartslist, value);
				if (!bAdd)
				{
					foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
					{
						bool flag = true;
						if (row.Cells["PART_SLIST_KEY"].Value == null)
						{
							flag = false;
						}
						else if (dr.Cells["PART_SLIST_KEY"].Value.ToString() != row.Cells["PART_SLIST_KEY"].Value.ToString())
						{
							flag = false;
						}
						if (!flag)
						{
							continue;
						}
						this.dgPartslist.Rows.Remove(row);
						break;
					}
				}
				else
				{
					try
					{
						int index = -1;
						if (this.dgPartslist.Columns.Contains("QTY"))
						{
							index = this.dgPartslist.Columns["QTY"].Index;
							if (dr.Cells["QTY"].Value == null)
							{
								dataGridViewRow.Cells[index].Value = "1";
							}
							else
							{
								int.Parse(dr.Cells["QTY"].Value.ToString());
								dataGridViewRow.Cells[index].Value = dr.Cells["QTY"].Value;
							}
						}
					}
					catch
					{
						try
						{
							dataGridViewRow.Cells[this.dgPartslist.Columns["QTY"].Index].Value = "1";
						}
						catch
						{
						}
					}
					dataGridViewRow.Tag = this.sParentPageArguments;
					this.dgPartslist.Rows.Add(dataGridViewRow);
					this.dgPartslist.Rows[dataGridViewRow.Index].Selected = false;
					this.dgPartslist.FirstDisplayedScrollingRowIndex = dataGridViewRow.Index;
				}
			}
			catch
			{
			}
		}

		public void selListInitialize()
		{
			if (this.dgPartslist.InvokeRequired)
			{
				this.dgPartslist.Invoke(new frmSelectionList.selListInitializeDelegate(this.selListInitialize));
				return;
			}
			if (this.dgPartslist.Rows.Count > 0 && this.dgPartslist.Columns.Count > 1)
			{
				IniFileIO iniFileIO = new IniFileIO();
				ArrayList arrayLists = new ArrayList();
				arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "SLIST_SETTINGS");
				for (int i = 0; i < arrayLists.Count; i++)
				{
					string empty = string.Empty;
					try
					{
						empty = iniFileIO.GetKeyValue("SLIST_SETTINGS", arrayLists[i].ToString().ToUpper(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
					}
					catch
					{
					}
					if (empty != null && empty != string.Empty)
					{
						string[] strArrays = new string[] { "|" };
						if (empty.Split(strArrays, StringSplitOptions.RemoveEmptyEntries)[3].ToUpper() == "TRUE")
						{
							this.dgPartslist.Columns[arrayLists[i].ToString()].Visible = true;
						}
						else
						{
							this.dgPartslist.Columns[arrayLists[i].ToString()].Visible = false;
						}
					}
				}
				return;
			}
			this.dgPartslist.Rows.Clear();
			this.dgPartslist.Columns.Clear();
			this.dgPartslist.AllowUserToAddRows = false;
			IniFileIO iniFileIO1 = new IniFileIO();
			ArrayList keys = new ArrayList();
			keys = iniFileIO1.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "SLIST_SETTINGS");
			this.frmParent.dicSLSettings = new Dictionary<string, string>();
			for (int j = 0; j < keys.Count; j++)
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					HeaderCell = new DataGridViewColumnHeaderCell()
				};
				string str = string.Empty;
				string empty1 = string.Empty;
				string str1 = string.Empty;
				string keyValue = string.Empty;
				string[] strArrays1 = null;
				try
				{
					keyValue = iniFileIO1.GetKeyValue("SLIST_SETTINGS", keys[j].ToString().ToUpper(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
				}
				catch
				{
				}
				if (keyValue != null && keyValue != string.Empty)
				{
					string[] strArrays2 = new string[] { "|" };
					strArrays1 = keyValue.Split(strArrays2, StringSplitOptions.RemoveEmptyEntries);
					string str2 = string.Concat("|True|True|", strArrays1[1], "|", strArrays1[2]);
					if ((int)strArrays1.Length == 3)
					{
						keyValue = string.Concat(keyValue, str2);
					}
					else if ((int)strArrays1.Length == 4)
					{
						keyValue = string.Concat(keyValue, str2);
					}
					string[] strArrays3 = new string[] { "|" };
					strArrays1 = keyValue.Split(strArrays3, StringSplitOptions.RemoveEmptyEntries);
					this.frmParent.dicSLSettings.Add(keys[j].ToString().ToUpper(), str2);
					try
					{
						str = strArrays1[0];
						frmSelectionList _frmSelectionList = this;
						_frmSelectionList.gstrSelectionListHeader = string.Concat(_frmSelectionList.gstrSelectionListHeader, str, ",");
						frmSelectionList _frmSelectionList1 = this;
						_frmSelectionList1.gstrSelectionListColSequence = string.Concat(_frmSelectionList1.gstrSelectionListColSequence, keys[j].ToString(), ",");
						empty1 = strArrays1[1];
						str1 = strArrays1[2];
					}
					catch
					{
					}
				}
				if (str != string.Empty)
				{
					dataGridViewTextBoxColumn.HeaderCell.Value = this.GetDGHeaderCellValue(keys[j].ToString(), str);
					dataGridViewTextBoxColumn.Name = keys[j].ToString();
					dataGridViewTextBoxColumn.Tag = keys[j].ToString();
					dataGridViewTextBoxColumn.Visible = true;
					if (empty1.ToUpper().Equals("R"))
					{
						dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
					}
					else if (!empty1.ToUpper().Equals("C"))
					{
						dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
					}
					else
					{
						dataGridViewTextBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
					}
					if (keys[j].ToString().ToUpper() != "PART_SLIST_KEY")
					{
						dataGridViewTextBoxColumn.Name = keys[j].ToString();
					}
					else
					{
						dataGridViewTextBoxColumn.Name = "PART_SLIST_KEY";
						dataGridViewTextBoxColumn.ToolTipText = "HIDDEN";
					}
					dataGridViewTextBoxColumn.DefaultCellStyle.NullValue = string.Empty;
					dataGridViewTextBoxColumn.HeaderCell.Style.Alignment = dataGridViewTextBoxColumn.DefaultCellStyle.Alignment;
					if (dataGridViewTextBoxColumn.Name == "QTY")
					{
						dataGridViewTextBoxColumn.ReadOnly = true;
					}
					if (str1 != null && str1 != string.Empty)
					{
						try
						{
							if (int.Parse(str1) <= 0)
							{
								dataGridViewTextBoxColumn.Width = 0;
								dataGridViewTextBoxColumn.Visible = false;
								this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn);
							}
							else
							{
								dataGridViewTextBoxColumn.Width = int.Parse(str1);
								if (strArrays1[3].ToString() != "True")
								{
									dataGridViewTextBoxColumn.Visible = false;
								}
								this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn);
							}
						}
						catch
						{
						}
					}
				}
			}
			DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
			{
				HeaderCell = new DataGridViewColumnHeaderCell()
			};
			dataGridViewTextBoxColumn1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			dataGridViewTextBoxColumn1.HeaderCell.Value = "AutoIndexColumn";
			dataGridViewTextBoxColumn1.Name = "AutoIndexColumn";
			dataGridViewTextBoxColumn1.Tag = "AutoIndexColumn";
			dataGridViewTextBoxColumn1.ToolTipText = "HIDDEN";
			dataGridViewTextBoxColumn1.Visible = false;
			dataGridViewTextBoxColumn1.ReadOnly = true;
			dataGridViewTextBoxColumn1.DefaultCellStyle.NullValue = string.Empty;
			this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn1);
			this.dgPartslist.SortCompare += new DataGridViewSortCompareEventHandler(this.dgPartslist_SortCompare);
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					HeaderCell = new DataGridViewColumnHeaderCell()
				};
				dataGridViewTextBoxColumn2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
				dataGridViewTextBoxColumn2.HeaderCell.Value = "PART_SLIST_KEY";
				dataGridViewTextBoxColumn2.Name = "PART_SLIST_KEY";
				dataGridViewTextBoxColumn2.Tag = "PART_SLIST_KEY";
				dataGridViewTextBoxColumn2.Visible = false;
				dataGridViewTextBoxColumn2.ReadOnly = true;
				dataGridViewTextBoxColumn2.ToolTipText = "HIDDEN";
				dataGridViewTextBoxColumn2.DefaultCellStyle.NullValue = string.Empty;
				this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn2);
			}
			catch
			{
			}
			this.dgPartslist.AllowUserToResizeColumns = true;
			bool flag = false;
			int num = 0;
			while (num < this.dgPartslist.Columns.Count)
			{
				if (!this.dgPartslist.Columns[num].Visible)
				{
					num++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				this.frmParent.ApplyPrintSettings(flag);
				this.tsBtnAdd.Enabled = true;
				this.tsPrint.Enabled = true;
				return;
			}
			this.frmParent.ApplyPrintSettings(flag);
			this.tsBtnAdd.Enabled = false;
			this.tsPrint.Enabled = false;
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetActiveWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		public void SetGridHeaderText()
		{
			try
			{
				if (!base.IsDisposed)
				{
					foreach (DataGridViewColumn column in this.dgPartslist.Columns)
					{
						try
						{
							if (Program.iniServers[this.frmParent.p_ServerId].items["SLIST_SETTINGS", column.Name.ToString().ToUpper()] != null)
							{
								IniFileIO iniFileIO = new IniFileIO();
								ArrayList arrayLists = new ArrayList();
								arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"), "SLIST_SETTINGS");
								int num = 0;
								while (num < arrayLists.Count)
								{
									if (arrayLists[num].ToString().ToUpper() != column.Name.ToUpper())
									{
										num++;
									}
									else
									{
										string keyValue = iniFileIO.GetKeyValue("SLIST_SETTINGS", arrayLists[num].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini"));
										keyValue = keyValue.Substring(0, keyValue.IndexOf("|"));
										column.HeaderText = this.GetDGHeaderCellValue(column.Name.ToUpper(), keyValue);
										break;
									}
								}
							}
						}
						catch
						{
						}
					}
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

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					this.picLoading.Left = parentPanel.Left + parentPanel.Width / 2 - this.picLoading.Width / 2;
					this.picLoading.Top = parentPanel.Top + parentPanel.Height / 2 - this.picLoading.Height / 2;
					this.picLoading.Parent = parentPanel;
					this.picLoading.BringToFront();
					this.picLoading.Show();
				}
				else
				{
					frmSelectionList.ShowLoadingDelegate showLoadingDelegate = new frmSelectionList.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		public void ShowQuantityScreen(string sPartNumber)
		{
			string str = "1";
			try
			{
				if (this.dgPartslist.Rows.Count > 0)
				{
					foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
					{
						if (row.Cells["PART_SLIST_KEY"].Value.ToString() != sPartNumber)
						{
							continue;
						}
						object value = row.Cells["QTY"].Value;
						if (value == null)
						{
							break;
						}
						str = value.ToString();
						break;
					}
				}
				frmSelectionListQuantity _frmSelectionListQuantity = new frmSelectionListQuantity(this.frmParent, sPartNumber, str);
				_frmSelectionListQuantity.ShowDialog(this.frmParent);
			}
			catch
			{
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

		public void SynSelectionList()
		{
			try
			{
				DataGridViewRowCollection selectionList = this.frmParent.GetSelectionList();
				string name = "";
				if (selectionList.Count != this.dgPartslist.Rows.Count)
				{
					for (int i = 0; i < selectionList.Count; i++)
					{
						this.dgPartslist.Rows.Add();
						DataGridViewRow item = this.dgPartslist.Rows[this.dgPartslist.Rows.Count - 1];
						for (int j = 0; j < this.dgPartslist.Columns.Count; j++)
						{
							name = this.dgPartslist.Columns[j].Name;
							try
							{
								item.Cells[name].Value = selectionList[i].Cells[name].Value;
							}
							catch
							{
							}
						}
						item.Tag = selectionList[i].Tag;
					}
				}
			}
			catch
			{
			}
		}

		private void tabSeparatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t");
			if (dataGridViewText != string.Empty)
			{
				Clipboard.SetText(dataGridViewText);
			}
		}

		private void tabSeparatedToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			this.dlgSaveFile.Filter = "txt files (*.txt)|*.txt";
			this.dlgSaveFile.RestoreDirectory = true;
			string dataGridViewText = this.GetDataGridViewText(ref this.dgPartslist, true, true, "\t");
			if (dataGridViewText != string.Empty && this.dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					StreamWriter streamWriter = File.CreateText(this.dlgSaveFile.FileName);
					streamWriter.Write(dataGridViewText);
					streamWriter.Close();
				}
				catch
				{
					MessageHandler.ShowWarning("Problems in exporting data to file");
				}
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			this.frmParent.UncheckAllRows();
			try
			{
				this.frmParent.ClearSelectionList();
			}
			catch
			{
			}
		}

		private void tsbClearSelection_Click(object sender, EventArgs e)
		{
			if (this.dgPartslist.Rows.Count > 0)
			{
				this.dgPartslist.ClearSelection();
				this.tsBtnDeleteSelection.Enabled = false;
				this.tsbClearSelection.Enabled = false;
			}
		}

		private void tsbClearSelection_EnabledChanged(object sender, EventArgs e)
		{
			this.tsmClearSelection.Enabled = this.tsbClearSelection.Enabled;
		}

		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
			this.dgPartslist.SelectAll();
		}

		private void tsbSelectAll_EnabledChanged(object sender, EventArgs e)
		{
			this.selectAllToolStripMenuItem.Enabled = this.tsbSelectAll.Enabled;
		}

		private void tsBtnAdd_Click(object sender, EventArgs e)
		{
			frmAddRecord _frmAddRecord = new frmAddRecord(this.frmParent, this.dgPartslist.Columns, this.frmParent.ServerId);
			_frmAddRecord.ShowDialog();
		}

		private void tsBtnDeleteSelection_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow selectedRow in this.dgPartslist.SelectedRows)
			{
				try
				{
					if (selectedRow.Tag != null && selectedRow.Tag.ToString() != "Manual")
					{
						this.frmParent.CheckUncheckRow(selectedRow.Cells["PART_SLIST_KEY"].Value.ToString(), selectedRow.Tag.ToString(), false);
					}
					else if (selectedRow.Tag != null && selectedRow.Tag.ToString() == "Manual")
					{
						this.frmParent.DeleteSelListRow(selectedRow.Cells["PART_SLIST_KEY"].Value.ToString());
					}
					if (this.dgPartslist.Rows.Contains(selectedRow))
					{
						this.dgPartslist.Rows.Remove(selectedRow);
					}
				}
				catch
				{
				}
			}
		}

		private void tsBtnDeleteSelection_EnabledChanged(object sender, EventArgs e)
		{
			this.deleteSelectionToolStripMenuItem.Enabled = this.tsBtnDeleteSelection.Enabled;
		}

		private void tsBtnGoToPage_EnabledChanged(object sender, EventArgs e)
		{
			this.goToPageToolStripMenuItem.Enabled = this.tsBtnGoToPage.Enabled;
		}

		private void tsBtnOpenParentPage_Click(object sender, EventArgs e)
		{
			try
			{
				string str = this.sGoToPageArgs;
				string[] strArrays = new string[] { "**" };
				string[] strArrays1 = str.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
				this.frmParent.OpenParentPage(strArrays1[0], strArrays1[1], strArrays1[2], strArrays1[3], strArrays1[4], strArrays1[5]);
			}
			catch
			{
			}
		}

		private void tsBtnPrint_Click(object sender, EventArgs e)
		{
			try
			{
				this.picLoading.BringToFront();
				this.picLoading.Show();
				this.frmParent.OpenPrintDialogue(3);
				this.picLoading.SendToBack();
				this.picLoading.Hide();
			}
			catch
			{
			}
		}

		public void tsBtnThirdPartyBasket_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			string str = string.Empty;
			string empty1 = string.Empty;
			try
			{
				if (Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL_TITLE"] != null)
				{
					str = Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL_TITLE"].ToString();
				}
				if (Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"] != null && Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"] != string.Empty)
				{
					empty = Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"].ToString();
					this.OpenURLInBrowser(empty, str);
					return;
				}
			}
			catch
			{
				MessageHandler.ShowError1(this.GetResource("(E-SLT-EM002) Failed to load specified object", "(E-SLT-EM002)_FAILED", ResourceType.POPUP_MESSAGE));
				return;
			}
			try
			{
				if (Program.iniServers[this.frmParent.ServerId].items["BASKET", "EXE"] != null)
				{
					empty1 = Program.iniServers[this.frmParent.ServerId].items["BASKET", "EXE"].ToString();
					if (Path.IsPathRooted(empty1))
					{
						Process.Start(empty1);
						return;
					}
					else
					{
						string str1 = Path.Combine(Application.StartupPath, empty1);
						Process.Start(str1);
						return;
					}
				}
			}
			catch
			{
				MessageHandler.ShowError1(this.GetResource("(E-SLT-EM003) Failed to load specified object", "(E-SLT-EM003)_FAILED", ResourceType.POPUP_MESSAGE));
				return;
			}
			try
			{
				if (empty == string.Empty && empty1 == string.Empty || Program.iniServers[this.frmParent.ServerId].items["BASKET", "URL"] != null)
				{
					MessageHandler.ShowError1(this.GetResource("(E-SLT-EM004) Failed to load specified object", "(E-SLT-EM004)_FAILED", ResourceType.POPUP_MESSAGE));
				}
			}
			catch
			{
			}
		}

		private void tsmClearSelection_Click(object sender, EventArgs e)
		{
			this.dgPartslist.ClearSelection();
		}

		private void tsPrint_Click(object sender, EventArgs e)
		{
			try
			{
				this.picLoading.BringToFront();
				this.picLoading.Show();
				this.frmParent.OpenPrintDialogue(3);
				this.picLoading.SendToBack();
				this.picLoading.Hide();
			}
			catch
			{
			}
		}

		public void UpdateFont()
		{
			this.dgPartslist.Font = Settings.Default.appFont;
			this.dgPartslist.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgPartslist.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		public delegate void selListInitializeDelegate();

		public delegate void selListReloadeDelegate();

		private delegate void ShowLoadingDelegate(Panel parentPanel);
	}
}
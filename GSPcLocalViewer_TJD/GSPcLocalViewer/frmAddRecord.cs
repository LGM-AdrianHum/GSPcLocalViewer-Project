using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmAddRecord : Form
	{
		private IContainer components;

		private Panel pnlForm;

		private DataGridView dgPartslist;

		private Panel pnlControl;

		private Button btnSave;

		private Button btnCancel;

		private frmViewer frmParent;

		private DataGridViewColumnCollection dgCols;

		private int serverId;

		public frmAddRecord(frmViewer objFrmViewer, DataGridViewColumnCollection dgCols1, int _serverId)
		{
			this.InitializeComponent();
			try
			{
				this.frmParent = objFrmViewer;
				this.dgCols = dgCols1;
				this.serverId = _serverId;
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					ReadOnly = true,
					Name = "Name"
				};
				dataGridViewTextBoxColumn.DefaultCellStyle.BackColor = Color.WhiteSmoke;
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
				{
					Name = "Value"
				};
				this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn);
				this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn1);
				ArrayList arrayLists = new ArrayList();
				List<string> strs = new List<string>();
				IniFileIO iniFileIO = new IniFileIO();
				arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"), "SLIST_SETTINGS");
				for (int i = 0; i < arrayLists.Count; i++)
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					iniFileIO1.GetKeyValue("SLIST_SETTINGS", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"));
					this.dgPartslist.Rows.Add();
				}
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					Name = "PART_SLIST_KEY",
					Visible = false
				};
				this.dgPartslist.Columns.Add(dataGridViewTextBoxColumn2);
				int headerText = 0;
				foreach (DataGridViewColumn dgCol in this.dgCols)
				{
					this.dgPartslist["Name", headerText].Value = dgCol.HeaderText;
					this.dgPartslist["Name", headerText].Tag = dgCol.Name;
					headerText++;
				}
				base.Height = this.dgPartslist.Rows.Count * 22 + this.pnlControl.Height + 35;
			}
			catch
			{
			}
			this.LoadResources();
			this.UpdateFont();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				this.dgPartslist.EndEdit();
				bool flag = false;
				if (this.dgPartslist.Rows.Count <= 0)
				{
					base.Close();
				}
				else
				{
					for (int i = 0; i < this.dgPartslist.Rows.Count; i++)
					{
						if (this.dgPartslist["Name", i].Tag.ToString().ToUpper() == "PARTNUMBER")
						{
							if (this.dgPartslist["Value", i].Value == null)
							{
								MessageBox.Show(this.GetResource("(W-ARD-WM001) Part number field must not be empty", "(W-ARD-WM001)_EMPTY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
							else if (this.frmParent.PartInSelectionList(this.dgPartslist["Value", i].Value.ToString().Trim()))
							{
								MessageBox.Show(this.GetResource("(W-ARD-WM004) Part already exists in selection list", "(W-ARD-WM004)_PART", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
						}
						if (this.dgPartslist["Name", i].Tag.ToString().ToUpper() == "QTY")
						{
							if (this.dgPartslist["Value", i].Value != null)
							{
								long num = (long)-1;
								if (!long.TryParse(this.dgPartslist["Value", i].Value.ToString(), out num))
								{
									MessageBox.Show(this.GetResource("(W-ARD-WM003) Quantity must be numeric", "(W-ARD-WM003)_NUMERIC", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
									return;
								}
							}
							else
							{
								MessageBox.Show(this.GetResource("(W-ARD-WM002) Quantity field must not be empty", "(W-ARD-WM002)_QUANTITY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
						}
						if (this.dgPartslist["Value", i].Value != null)
						{
							flag = true;
						}
					}
					if (flag)
					{
						DataGridView dataGridView = new DataGridView();
						int index = 9999;
						foreach (DataGridViewRow row in (IEnumerable)this.dgPartslist.Rows)
						{
							DataGridViewCell item = row.Cells[1];
							dataGridView.Columns.Add(row.Cells[0].Tag.ToString(), row.Cells[0].Value.ToString());
							if (row.Cells[0].Tag.ToString().ToUpper() != "PARTNUMBER")
							{
								continue;
							}
							index = row.Index;
						}
						dataGridView.Columns.Add("PART_SLIST_KEY", "PART_SLIST_KEY");
						DataGridViewRow dataGridViewRow = new DataGridViewRow();
						dataGridViewRow.CreateCells(dataGridView);
						int num1 = 0;
						foreach (DataGridViewRow row1 in (IEnumerable)this.dgPartslist.Rows)
						{
							if (num1 == index)
							{
								dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].Value = row1.Cells[1].Value;
								dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].ToolTipText = "HIDDEN";
								index = 8888;
							}
							dataGridViewRow.Cells[num1].Value = row1.Cells[1].Value;
							num1++;
							if (index == 8888 || row1.Cells[1].Value == null || !(row1.Cells[1].Value.ToString() != ""))
							{
								continue;
							}
							dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].Value = row1.Cells[1].Value;
							dataGridViewRow.Cells[dataGridViewRow.Cells.Count - 1].ToolTipText = "HIDDEN";
						}
						dataGridView.Rows.Add(dataGridViewRow);
						this.frmParent.AddNewRecord(dataGridView.Rows[0]);
						base.Close();
					}
					else
					{
						MessageBox.Show(this.GetResource("(W-ARD-WM005) All fields must not be empty", "(W-ARD-WM005)_EMPTY", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
			}
			catch
			{
			}
		}

		private void dgPartslist_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
		{
			this.Refresh();
		}

		private void dgPartslist_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.btnSave_Click(null, null);
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

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MAIN_FORM']");
				str = string.Concat(str, "/Screen[@Name='ADD_RECORD']");
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

		private void InitializeComponent()
		{
			this.pnlForm = new Panel();
			this.dgPartslist = new DataGridView();
			this.pnlControl = new Panel();
			this.btnSave = new Button();
			this.btnCancel = new Button();
			this.pnlForm.SuspendLayout();
			((ISupportInitialize)this.dgPartslist).BeginInit();
			this.pnlControl.SuspendLayout();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.Controls.Add(this.dgPartslist);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Padding = new System.Windows.Forms.Padding(5);
			this.pnlForm.Size = new System.Drawing.Size(342, 256);
			this.pnlForm.TabIndex = 0;
			this.dgPartslist.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.dgPartslist.AllowUserToAddRows = false;
			this.dgPartslist.AllowUserToDeleteRows = false;
			this.dgPartslist.AllowUserToResizeRows = false;
			this.dgPartslist.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			this.dgPartslist.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			this.dgPartslist.BackgroundColor = Color.White;
			this.dgPartslist.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgPartslist.ColumnHeadersVisible = false;
			this.dgPartslist.Dock = DockStyle.Fill;
			this.dgPartslist.Location = new Point(5, 5);
			this.dgPartslist.Margin = new System.Windows.Forms.Padding(60);
			this.dgPartslist.Name = "dgPartslist";
			this.dgPartslist.RowHeadersVisible = false;
			this.dgPartslist.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgPartslist.Size = new System.Drawing.Size(332, 215);
			this.dgPartslist.TabIndex = 21;
			this.dgPartslist.KeyDown += new KeyEventHandler(this.dgPartslist_KeyDown);
			this.dgPartslist.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dgPartslist_ColumnWidthChanged);
			this.pnlControl.Controls.Add(this.btnSave);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(5, 220);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 6, 0, 2);
			this.pnlControl.Size = new System.Drawing.Size(332, 31);
			this.pnlControl.TabIndex = 22;
			this.btnSave.Dock = DockStyle.Right;
			this.btnSave.Location = new Point(182, 6);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new EventHandler(this.btnSave_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(257, 6);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(342, 256);
			base.Controls.Add(this.pnlForm);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(350, 290);
			base.Name = "frmAddRecord";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Add Record";
			this.pnlForm.ResumeLayout(false);
			((ISupportInitialize)this.dgPartslist).EndInit();
			this.pnlControl.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public void LoadResources()
		{
			this.Text = this.GetResource("Add Record", "ADD_RECORD", ResourceType.TITLE);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.btnSave.Text = this.GetResource("Save", "SAVE", ResourceType.BUTTON);
		}

		public void UpdateFont()
		{
			this.dgPartslist.Font = Settings.Default.appFont;
			this.dgPartslist.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgPartslist.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}
	}
}
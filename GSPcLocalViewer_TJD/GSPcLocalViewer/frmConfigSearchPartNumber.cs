using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmConfigSearchPartNumber : Form
	{
		private IContainer components;

		private Label lblSearchSettings;

		private Panel pnlControl;

		private Button btnOk;

		private Button btnCancel;

		private Panel pnlForm;

		private DataGridView dgViewSearchSettings;

		private BackgroundWorker bgWorker;

		private PictureBox picLoading;

		private frmConfig frmParent;

		private int serverId;

		private Rectangle dragBoxFromMouseDown;

		private int rowIndexFromMouseDown;

		private int rowIndexOfItemUnderMouseToDrop;

		private string sLocalFile = string.Empty;

		private string sServerFile = string.Empty;

		private Download objDownloader;

		public frmConfigSearchPartNumber(frmConfig frm, int _serverId)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.serverId = _serverId;
			this.objDownloader = new Download();
			this.UpdateFont();
			this.LoadResources();
		}

		private void AddSearchSettingsRow(string sVal, string sKey)
		{
			try
			{
				string[] strArrays = sVal.Split(new char[] { '|' });
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				DataGridViewCheckBoxCell dataGridViewCheckBoxCell = new DataGridViewCheckBoxCell()
				{
					Value = strArrays[0]
				};
				dataGridViewRow.Cells.Add(dataGridViewCheckBoxCell);
				DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
				{
					Value = sKey
				};
				dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
				DataGridViewTextBoxCell dataGridViewTextBoxCell1 = new DataGridViewTextBoxCell()
				{
					Value = Global.GetDGHeaderCellValue("PARTNUMBER_SEARCH", sKey, strArrays[1], this.serverId)
				};
				dataGridViewRow.Cells.Add(dataGridViewTextBoxCell1);
				DataGridViewTextBoxCell dataGridViewTextBoxCell2 = new DataGridViewTextBoxCell()
				{
					Value = strArrays[3].ToString()
				};
				dataGridViewRow.Cells.Add(dataGridViewTextBoxCell2);
				DataGridViewComboBoxCell dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
				string[] strArrays1 = "L,R,C".ToString().Split(new char[] { ',' });
				dataGridViewComboBoxCell.Items.AddRange(strArrays1);
				string str = strArrays[2].ToString();
				string str1 = str;
				if (str != null)
				{
					if (str1 == "L")
					{
						dataGridViewComboBoxCell.Value = strArrays1[0];
						goto Label0;
					}
					else if (str1 == "R")
					{
						dataGridViewComboBoxCell.Value = strArrays1[1];
						goto Label0;
					}
					else
					{
						if (str1 != "C")
						{
							goto Label2;
						}
						dataGridViewComboBoxCell.Value = strArrays1[2];
						goto Label0;
					}
				}
			Label2:
				dataGridViewComboBoxCell.Value = strArrays1[0];
			Label0:
				dataGridViewRow.Cells.Add(dataGridViewComboBoxCell);
				dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
				this.dgViewSearchSettings.Rows.Add(dataGridViewRow);
			}
			catch
			{
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.InitializeSearchGrid();
			this.LoadDataGridView();
			this.SetControlsText();
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.HideLoading(this.pnlForm);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.Close();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.frmParent.CloseAndSaveSettings();
		}

		private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
		{
			for (int i = 0; i < this.dgViewSearchSettings.RowCount; i++)
			{
				this.dgViewSearchSettings[0, i].Value = ((CheckBox)this.dgViewSearchSettings.Controls.Find("checkboxHeader", true)[0]).Checked;
			}
			this.dgViewSearchSettings.EndEdit();
		}

		private void chkHeader_OnCheckBoxClicked(bool state)
		{
			this.dgViewSearchSettings.CellValueChanged -= new DataGridViewCellEventHandler(this.dgViewSearchSettings_CellValueChanged);
			this.dgViewSearchSettings.BeginEdit(true);
			if (this.dgViewSearchSettings.Columns.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)this.dgViewSearchSettings.Rows)
				{
					if (!(row.Cells[0] is DataGridViewCheckBoxCell))
					{
						continue;
					}
					row.Cells[0].Value = state;
				}
			}
			this.dgViewSearchSettings.EndEdit();
			this.dgViewSearchSettings.CellValueChanged += new DataGridViewCellEventHandler(this.dgViewSearchSettings_CellValueChanged);
		}

		private void dgViewSearchSettings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (this.dgViewSearchSettings.Columns.Count > 0 && e.RowIndex != -1 && e.ColumnIndex == this.dgViewSearchSettings.Columns["select"].Index)
				{
					int num = 0;
					num = 0;
					while (num < this.dgViewSearchSettings.Rows.Count && (!(this.dgViewSearchSettings.Rows[num].Cells[0] is DataGridViewCheckBoxCell) || (bool)this.dgViewSearchSettings.Rows[num].Cells[0].Value))
					{
						num++;
					}
					DatagridViewCheckBoxHeaderCell headerCell = (DatagridViewCheckBoxHeaderCell)this.dgViewSearchSettings.Columns[0].HeaderCell;
					if (num >= this.dgViewSearchSettings.Rows.Count)
					{
						headerCell.Checked = true;
					}
					else
					{
						headerCell.Checked = false;
					}
				}
			}
			catch
			{
			}
		}

		private void dgViewSearchSettings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dgViewSearchSettings.CurrentCell is DataGridViewCheckBoxCell)
				{
					this.dgViewSearchSettings.CommitEdit(DataGridViewDataErrorContexts.Commit);
				}
			}
			catch
			{
			}
		}

		private void dgViewSearchSettings_DragDrop(object sender, DragEventArgs e)
		{
			Point client = this.dgViewSearchSettings.PointToClient(new Point(e.X, e.Y));
			this.rowIndexOfItemUnderMouseToDrop = this.dgViewSearchSettings.HitTest(client.X, client.Y).RowIndex;
			if (this.rowIndexOfItemUnderMouseToDrop != -1 && e.Effect == DragDropEffects.Move)
			{
				DataGridViewRow data = e.Data.GetData(typeof(DataGridViewRow)) as DataGridViewRow;
				this.dgViewSearchSettings.Rows.RemoveAt(this.rowIndexFromMouseDown);
				this.dgViewSearchSettings.Rows.Insert(this.rowIndexOfItemUnderMouseToDrop, data);
				try
				{
					try
					{
						this.dgViewSearchSettings.CurrentCell = this.dgViewSearchSettings.Rows[this.rowIndexOfItemUnderMouseToDrop].Cells[1];
					}
					catch
					{
					}
				}
				finally
				{
					this.dragBoxFromMouseDown = new Rectangle();
				}
			}
		}

		private void dgViewSearchSettings_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void dgViewSearchSettings_MouseDown(object sender, MouseEventArgs e)
		{
			this.rowIndexFromMouseDown = this.dgViewSearchSettings.HitTest(e.X, e.Y).RowIndex;
			int columnIndex = this.dgViewSearchSettings.HitTest(e.X, e.Y).ColumnIndex;
			if (columnIndex != -1 && this.rowIndexFromMouseDown != -1)
			{
				this.dgViewSearchSettings.CurrentCell = this.dgViewSearchSettings.Rows[this.rowIndexFromMouseDown].Cells[columnIndex];
				this.dgViewSearchSettings.Rows[this.rowIndexFromMouseDown].Cells[columnIndex].Selected = true;
				this.dragBoxFromMouseDown = Rectangle.Empty;
				return;
			}
			if (this.rowIndexFromMouseDown == -1)
			{
				this.dragBoxFromMouseDown = Rectangle.Empty;
				return;
			}
			System.Drawing.Size dragSize = SystemInformation.DragSize;
			this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
		}

		private void dgViewSearchSettings_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left && this.dragBoxFromMouseDown != Rectangle.Empty && !this.dragBoxFromMouseDown.Contains(e.X, e.Y))
			{
				this.dgViewSearchSettings.DoDragDrop(this.dgViewSearchSettings.Rows[this.rowIndexFromMouseDown], DragDropEffects.Move);
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

		private void frmConfigBookPublishingIdSearch_Load(object sender, EventArgs e)
		{
			this.ShowLoading(this.pnlForm);
			this.bgWorker.RunWorkerAsync();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='CONFIGURATION']");
				str = string.Concat(str, "/Screen[@Name='PART_NUMBER']");
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
					else if (rType == ResourceType.POPUP_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='POPUP_MESSAGE']");
					}
					else if (rType == ResourceType.STATUS_MESSAGE)
					{
						str = string.Concat(str, "/Resources[@Name='STATUS_MESSAGE']");
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

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				foreach (Control control in parentPanel.Controls)
				{
					if (control == this.picLoading)
					{
						continue;
					}
					control.Visible = true;
				}
				this.picLoading.Hide();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle control = new DataGridViewCellStyle();
			DataGridViewCellStyle font = new DataGridViewCellStyle();
			DataGridViewCellStyle white = new DataGridViewCellStyle();
			this.lblSearchSettings = new Label();
			this.pnlControl = new Panel();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.pnlForm = new Panel();
			this.dgViewSearchSettings = new DataGridView();
			this.picLoading = new PictureBox();
			this.bgWorker = new BackgroundWorker();
			this.pnlControl.SuspendLayout();
			this.pnlForm.SuspendLayout();
			((ISupportInitialize)this.dgViewSearchSettings).BeginInit();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.lblSearchSettings.BackColor = Color.White;
			this.lblSearchSettings.Dock = DockStyle.Top;
			this.lblSearchSettings.ForeColor = Color.Black;
			this.lblSearchSettings.Location = new Point(0, 0);
			this.lblSearchSettings.Name = "lblSearchSettings";
			this.lblSearchSettings.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblSearchSettings.Size = new System.Drawing.Size(448, 27);
			this.lblSearchSettings.TabIndex = 16;
			this.lblSearchSettings.Text = "Search Settings";
			this.pnlControl.Controls.Add(this.btnOk);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 417);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(448, 31);
			this.pnlControl.TabIndex = 18;
			this.btnOk.Dock = DockStyle.Right;
			this.btnOk.Location = new Point(283, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(358, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.dgViewSearchSettings);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Controls.Add(this.lblSearchSettings);
			this.pnlForm.Controls.Add(this.picLoading);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(450, 450);
			this.pnlForm.TabIndex = 19;
			this.dgViewSearchSettings.AllowDrop = true;
			this.dgViewSearchSettings.AllowUserToAddRows = false;
			this.dgViewSearchSettings.AllowUserToDeleteRows = false;
			this.dgViewSearchSettings.AllowUserToResizeRows = false;
			dataGridViewCellStyle.BackColor = Color.White;
			this.dgViewSearchSettings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dgViewSearchSettings.BackgroundColor = Color.White;
			this.dgViewSearchSettings.BorderStyle = BorderStyle.Fixed3D;
			control.Alignment = DataGridViewContentAlignment.MiddleLeft;
			control.BackColor = SystemColors.Control;
			control.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			control.ForeColor = Color.Black;
			control.SelectionBackColor = SystemColors.Highlight;
			control.SelectionForeColor = SystemColors.HighlightText;
			control.WrapMode = DataGridViewTriState.True;
			this.dgViewSearchSettings.ColumnHeadersDefaultCellStyle = control;
			this.dgViewSearchSettings.ColumnHeadersHeight = 26;
			this.dgViewSearchSettings.Dock = DockStyle.Fill;
			this.dgViewSearchSettings.Location = new Point(0, 27);
			this.dgViewSearchSettings.Name = "dgViewSearchSettings";
			font.Alignment = DataGridViewContentAlignment.MiddleLeft;
			font.BackColor = SystemColors.Control;
			font.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			font.ForeColor = Color.Black;
			font.SelectionBackColor = SystemColors.Highlight;
			font.SelectionForeColor = SystemColors.HighlightText;
			font.WrapMode = DataGridViewTriState.True;
			this.dgViewSearchSettings.RowHeadersDefaultCellStyle = font;
			this.dgViewSearchSettings.RowHeadersWidth = 32;
			this.dgViewSearchSettings.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			white.BackColor = Color.White;
			white.ForeColor = Color.Black;
			white.SelectionBackColor = Color.SteelBlue;
			white.SelectionForeColor = Color.White;
			this.dgViewSearchSettings.RowsDefaultCellStyle = white;
			this.dgViewSearchSettings.RowTemplate.Height = 16;
			this.dgViewSearchSettings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgViewSearchSettings.ShowRowErrors = false;
			this.dgViewSearchSettings.Size = new System.Drawing.Size(448, 390);
			this.dgViewSearchSettings.TabIndex = 20;
			this.dgViewSearchSettings.CellValueChanged += new DataGridViewCellEventHandler(this.dgViewSearchSettings_CellValueChanged);
			this.dgViewSearchSettings.MouseDown += new MouseEventHandler(this.dgViewSearchSettings_MouseDown);
			this.dgViewSearchSettings.MouseMove += new MouseEventHandler(this.dgViewSearchSettings_MouseMove);
			this.dgViewSearchSettings.DragOver += new DragEventHandler(this.dgViewSearchSettings_DragOver);
			this.dgViewSearchSettings.CurrentCellDirtyStateChanged += new EventHandler(this.dgViewSearchSettings_CurrentCellDirtyStateChanged);
			this.dgViewSearchSettings.DragDrop += new DragEventHandler(this.dgViewSearchSettings_DragDrop);
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(2, 2);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.TabIndex = 23;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new System.Drawing.Size(450, 450);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmConfigSearchPartNumber";
			base.Load += new EventHandler(this.frmConfigBookPublishingIdSearch_Load);
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			((ISupportInitialize)this.dgViewSearchSettings).EndInit();
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
		}

		private void InitializeSearchGrid()
		{
			if (this.dgViewSearchSettings.InvokeRequired)
			{
				this.dgViewSearchSettings.Invoke(new frmConfigSearchPartNumber.InitializeSearchGridDelegate(this.InitializeSearchGrid));
				return;
			}
			try
			{
				DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn()
				{
					Name = "select"
				};
				DatagridViewCheckBoxHeaderCell datagridViewCheckBoxHeaderCell = new DatagridViewCheckBoxHeaderCell();
				datagridViewCheckBoxHeaderCell.OnCheckBoxClicked += new CheckBoxClickedHandler(this.chkHeader_OnCheckBoxClicked);
				datagridViewCheckBoxHeaderCell.Value = string.Empty;
				dataGridViewCheckBoxColumn.HeaderCell = datagridViewCheckBoxHeaderCell;
				dataGridViewCheckBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dataGridViewCheckBoxColumn.Frozen = true;
				dataGridViewCheckBoxColumn.TrueValue = true;
				dataGridViewCheckBoxColumn.FalseValue = false;
				dataGridViewCheckBoxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
				this.dgViewSearchSettings.Columns.Add(dataGridViewCheckBoxColumn);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					Name = "FieldId",
					HeaderText = "Field ID",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearchSettings.Columns.Add(dataGridViewTextBoxColumn);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
				{
					Name = "DisplayName",
					HeaderText = "Display Name",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearchSettings.Columns.Add(dataGridViewTextBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					Name = "Width",
					HeaderText = "Col Width",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearchSettings.Columns.Add(dataGridViewTextBoxColumn2);
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn()
				{
					Name = "Alignment",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSearchSettings.Columns.Add(dataGridViewComboBoxColumn);
			}
			catch
			{
			}
		}

		private void LoadDataGridView()
		{
			if (this.dgViewSearchSettings.InvokeRequired)
			{
				this.dgViewSearchSettings.Invoke(new frmConfigSearchPartNumber.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
				return;
			}
			this.dgViewSearchSettings.Rows.Clear();
			ArrayList arrayLists = new ArrayList();
			List<string> strs = new List<string>();
			IniFileIO iniFileIO = new IniFileIO();
			arrayLists = new ArrayList();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"), "PARTNUMBER_SEARCH");
			for (int i = 0; i < arrayLists.Count; i++)
			{
				try
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					string keyValue = iniFileIO1.GetKeyValue("PARTNUMBER_SEARCH", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"));
					this.AddSearchSettingsRow(keyValue, arrayLists[i].ToString());
				}
				catch
				{
				}
			}
			if (arrayLists.Count == 0)
			{
				this.AddSearchSettingsRow("true|Part Number|C|100", "PartNumber");
				this.AddSearchSettingsRow("true|Part Name|C|100", "PartName");
				this.AddSearchSettingsRow("true|Page Name|C|100", "PageName");
				this.AddSearchSettingsRow("true|Page ID|C|100", "ID");
			}
		}

		private void LoadResources()
		{
			this.lblSearchSettings.Text = this.GetResource("Search Settings", "SEARCH_SETTINGS", ResourceType.LABEL);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			try
			{
				this.dgViewSearchSettings.Columns["FieldId"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSearchSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSearchSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSearchSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
		}

		public void SaveSettings()
		{
			try
			{
				string empty = string.Empty;
				if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
				{
					for (int i = 0; i < this.dgViewSearchSettings.Rows.Count; i++)
					{
						Global.SaveToLanguageIni("PARTNUMBER_SEARCH", this.dgViewSearchSettings[1, i].Value.ToString(), this.dgViewSearchSettings[2, i].Value.ToString(), this.serverId);
					}
				}
				for (int j = 0; j < this.dgViewSearchSettings.Rows.Count; j++)
				{
					if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
					{
						empty = this.dgViewSearchSettings[2, j].Value.ToString();
					}
					else
					{
						empty = Global.GetEngHeaderVal("PARTNUMBER_SEARCH", this.dgViewSearchSettings[1, j].Value.ToString(), this.serverId);
						char[] chrArray = new char[] { '|' };
						empty = empty.Split(chrArray)[1];
					}
					string str = this.dgViewSearchSettings[1, j].Value.ToString();
					string[] strArrays = new string[] { this.dgViewSearchSettings[0, j].Value.ToString(), "|", empty, "|", this.dgViewSearchSettings[4, j].Value.ToString(), "|", this.dgViewSearchSettings[3, j].Value.ToString() };
					Global.SaveToServerIni("PARTNUMBER_SEARCH", str, string.Concat(strArrays), this.serverId);
				}
			}
			catch
			{
			}
		}

		private void SetControlsText()
		{
			this.lblSearchSettings.Text = this.GetResource("Search Settings", "SEARCH_SETTINGS", ResourceType.LABEL);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			try
			{
				this.dgViewSearchSettings.Columns["FieldId"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSearchSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSearchSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSearchSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				foreach (Control control in parentPanel.Controls)
				{
					if (control == this.picLoading || control == this.lblSearchSettings)
					{
						continue;
					}
					control.Visible = false;
				}
				this.picLoading.Left = parentPanel.Left + parentPanel.Width / 2 - this.picLoading.Width / 2;
				this.picLoading.Top = parentPanel.Top + parentPanel.Height / 2 - this.picLoading.Height / 2;
				this.picLoading.Parent = parentPanel;
				this.picLoading.BringToFront();
				this.picLoading.Show();
			}
			catch
			{
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblSearchSettings.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgViewSearchSettings.Font = Settings.Default.appFont;
			this.dgViewSearchSettings.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgViewSearchSettings.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		private delegate void InitializeSearchGridDelegate();

		private delegate void LoadDataGridViewXmlDelegate();
	}
}
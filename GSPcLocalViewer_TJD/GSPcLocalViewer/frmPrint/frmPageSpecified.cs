using GSPcLocalViewer;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmPrint
{
	public class frmPageSpecified : Form
	{
		private IContainer components;

		private Panel pnlGridView;

		private Panel pnlButtons;

		private DataGridView dgViewPrintList;

		private Button btnCancel;

		private Button btnOK;

		private BackgroundWorker bgWorker;

		private GSPcLocalViewer.frmPrint.frmPrint frmParent;

		private int intServerId;

		private bool bClearSelection;

		public frmPageSpecified(GSPcLocalViewer.frmPrint.frmPrint frm, int intTempServerID)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.intServerId = intTempServerID;
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.SetControlsText();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.Enabled = true;
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (this.CheckToAndFromColumns() == 1)
			{
				this.CopyGridView();
				this.EnablePrintControls();
				this.frmParent.Enabled = true;
				base.Close();
				return;
			}
			MessageBox.Show(this.GetResource("Please be sure to set the From and To.", "FILL_FROM_AND_TO", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private int CheckToAndFromColumns()
		{
			int num = 1;
			try
			{
				int count = this.dgViewPrintList.Rows.Count;
				num = (!(this.dgViewPrintList.Rows[count - 1].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[count - 1].Cells[2].Value.ToString() != "") ? 0 : 1);
			}
			catch (Exception exception)
			{
			}
			return num;
		}

		private void CopyGridView()
		{
			try
			{
				if (this.frmParent.dgvPrintListPrintFrm.Rows.Count > 0)
				{
					this.frmParent.dgvPrintListPrintFrm.Rows.Clear();
				}
				int height = 0;
				for (int i = 0; i < this.dgViewPrintList.Rows.Count; i++)
				{
					int value = this.frmParent.dgvPrintListPrintFrm.Rows.Add();
					this.frmParent.dgvPrintListPrintFrm.Rows[value].Cells[0].Value = this.dgViewPrintList.Rows[i].Cells[0].Value;
					this.frmParent.dgvPrintListPrintFrm.Rows[value].Cells[1].Value = this.dgViewPrintList.Rows[i].Cells[1].Value;
					this.frmParent.dgvPrintListPrintFrm.Rows[value].Cells[2].Value = this.dgViewPrintList.Rows[i].Cells[2].Value;
					this.frmParent.dgvPrintListPrintFrm.Rows[value].Cells[0].Tag = this.dgViewPrintList.Rows[i].Cells[0].Tag;
					this.frmParent.dgvPrintListPrintFrm.Rows[value].Cells[1].Tag = this.dgViewPrintList.Rows[i].Cells[1].Tag;
					this.frmParent.dgvPrintListPrintFrm.Rows[value].Cells[2].Tag = this.dgViewPrintList.Rows[i].Cells[2].Tag;
					height += this.frmParent.dgvPrintListPrintFrm.Rows[value].Height;
				}
				this.frmParent.dgvPrintListPrintFrm.Height = height;
			}
			catch (Exception exception)
			{
			}
		}

		private void dgViewPrintList_DeleteClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == 3)
				{
					this.dgViewPrintList.Rows.RemoveAt(e.RowIndex);
					this.bClearSelection = true;
				}
				this.dgViewPrintList.ClearSelection();
				if (this.dgViewPrintList.Rows.Count > 0)
				{
					this.updateGridSerialNo();
				}
				this.dgViewPrintList.Update();
			}
			catch (Exception exception)
			{
			}
		}

		private void dgViewPrintList_MouseUp(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					if (this.dgViewPrintList.HitTest(e.X, e.Y) == DataGridView.HitTestInfo.Nowhere)
					{
						this.dgViewPrintList.ClearSelection();
						this.dgViewPrintList.CurrentCell = null;
					}
					else if (this.dgViewPrintList.Rows.Count > 0)
					{
						DataGridView.HitTestInfo hitTestInfo = this.dgViewPrintList.HitTest(e.X, e.Y);
						this.dgViewPrintList.Rows[hitTestInfo.RowIndex].Selected = true;
						if (this.bClearSelection)
						{
							this.bClearSelection = false;
							this.dgViewPrintList.ClearSelection();
						}
					}
				}
			}
			catch (Exception exception)
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

		private void EnableAndDisableButtons()
		{
			if (this.dgViewPrintList.Rows.Count == 0)
			{
				this.btnOK.Enabled = false;
				return;
			}
			this.btnOK.Enabled = true;
		}

		private void EnablePrintControls()
		{
			try
			{
				if (this.frmParent.dgvPrintListPrintFrm.Rows.Count == 0)
				{
					this.frmParent.btnPreview.Enabled = false;
					this.frmParent.btnPrint.Enabled = false;
				}
				else if (this.frmParent.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
				{
					this.frmParent.btnPreview.Enabled = false;
					this.frmParent.btnPrint.Enabled = true;
				}
				else if (!this.frmParent.frmParent.objFrmTreeview.sDataType.ToUpper().Trim().EndsWith("PDF"))
				{
					this.frmParent.btnPreview.Enabled = true;
					this.frmParent.btnPrint.Enabled = true;
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void frmPageSpecified_Load(object sender, EventArgs e)
		{
			this.UpdateFont();
			this.initilizePageSpecifiedGrid();
			this.EnableAndDisableButtons();
			this.bgWorker.RunWorkerAsync();
		}

		private DataTable GetDataTableFromDGV(DataGridView dgv)
		{
			DataTable dataTable = new DataTable();
			try
			{
				foreach (DataGridViewColumn column in dgv.Columns)
				{
					if (!column.Visible)
					{
						continue;
					}
					dataTable.Columns.Add(column.Name);
				}
				object[] value = new object[dgv.Columns.Count];
				foreach (DataGridViewRow row in (IEnumerable)dgv.Rows)
				{
					for (int i = 0; i < row.Cells.Count; i++)
					{
						value[i] = row.Cells[i].Value;
					}
					dataTable.Rows.Add(value);
				}
			}
			catch (Exception exception)
			{
			}
			return dataTable;
		}

		private void GetPageSpecifiedGrid()
		{
			if (this.dgViewPrintList != null && this.dgViewPrintList.Columns.Count == 0)
			{
				DataGridViewColumn dataGridViewColumn = new DataGridViewColumn();
				this.dgViewPrintList.Columns.Add(dataGridViewColumn);
			}
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='PRINT']");
				str = string.Concat(str, "/Screen[@Name='PAGE_SPECIFIED']");
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

		private void InitializeComponent()
		{
			this.pnlGridView = new Panel();
			this.dgViewPrintList = new DataGridView();
			this.pnlButtons = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.bgWorker = new BackgroundWorker();
			this.pnlGridView.SuspendLayout();
			((ISupportInitialize)this.dgViewPrintList).BeginInit();
			this.pnlButtons.SuspendLayout();
			base.SuspendLayout();
			this.pnlGridView.Controls.Add(this.dgViewPrintList);
			this.pnlGridView.Dock = DockStyle.Top;
			this.pnlGridView.Location = new Point(0, 0);
			this.pnlGridView.Name = "pnlGridView";
			this.pnlGridView.Size = new System.Drawing.Size(518, 333);
			this.pnlGridView.TabIndex = 0;
			this.dgViewPrintList.AllowUserToAddRows = false;
			this.dgViewPrintList.AllowUserToResizeRows = false;
			this.dgViewPrintList.BackgroundColor = Color.White;
			this.dgViewPrintList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgViewPrintList.Dock = DockStyle.Fill;
			this.dgViewPrintList.Location = new Point(0, 0);
			this.dgViewPrintList.MultiSelect = false;
			this.dgViewPrintList.Name = "dgViewPrintList";
			this.dgViewPrintList.RowHeadersVisible = false;
			this.dgViewPrintList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgViewPrintList.Size = new System.Drawing.Size(518, 333);
			this.dgViewPrintList.TabIndex = 0;
			this.dgViewPrintList.TabStop = false;
			this.dgViewPrintList.MouseUp += new MouseEventHandler(this.dgViewPrintList_MouseUp);
			this.pnlButtons.Controls.Add(this.btnOK);
			this.pnlButtons.Controls.Add(this.btnCancel);
			this.pnlButtons.Dock = DockStyle.Bottom;
			this.pnlButtons.Location = new Point(0, 333);
			this.pnlButtons.Name = "pnlButtons";
			this.pnlButtons.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
			this.pnlButtons.Size = new System.Drawing.Size(518, 31);
			this.pnlButtons.TabIndex = 1;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(353, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(428, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new System.Drawing.Size(518, 364);
			base.Controls.Add(this.pnlButtons);
			base.Controls.Add(this.pnlGridView);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(528, 400);
			base.MinimizeBox = false;
			base.Name = "frmPageSpecified";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Page Specified";
			base.Load += new EventHandler(this.frmPageSpecified_Load);
			this.pnlGridView.ResumeLayout(false);
			((ISupportInitialize)this.dgViewPrintList).EndInit();
			this.pnlButtons.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void initilizePageSpecifiedGrid()
		{
			try
			{
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					Name = "no",
					HeaderText = "No",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 40,
					ReadOnly = true
				};
				this.dgViewPrintList.Columns.Add(dataGridViewTextBoxColumn);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
				{
					Name = "pageFrom",
					HeaderText = "From",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 180,
					ReadOnly = true
				};
				this.dgViewPrintList.Columns.Add(dataGridViewTextBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					Name = "pageTo",
					HeaderText = "To",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 180,
					ReadOnly = true
				};
				this.dgViewPrintList.Columns.Add(dataGridViewTextBoxColumn2);
				DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn()
				{
					Name = "Delete",
					HeaderText = "",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 115
				};
				this.dgViewPrintList.Columns.Add(dataGridViewButtonColumn);
				this.dgViewPrintList.CellClick += new DataGridViewCellEventHandler(this.dgViewPrintList_DeleteClick);
				if (this.frmParent.dgvPrintListPrintFrm.Rows.Count > 0)
				{
					for (int i = 0; i < this.frmParent.dgvPrintListPrintFrm.Rows.Count; i++)
					{
						int value = this.dgViewPrintList.Rows.Add();
						this.dgViewPrintList.Rows[value].Cells[0].Value = this.frmParent.dgvPrintListPrintFrm.Rows[i].Cells[0].Value;
						this.dgViewPrintList.Rows[value].Cells[1].Value = this.frmParent.dgvPrintListPrintFrm.Rows[i].Cells[1].Value;
						this.dgViewPrintList.Rows[value].Cells[2].Value = this.frmParent.dgvPrintListPrintFrm.Rows[i].Cells[2].Value;
						this.dgViewPrintList.Rows[value].Cells[3].Value = "Delete";
						this.dgViewPrintList.Rows[value].Cells[0].Tag = this.frmParent.dgvPrintListPrintFrm.Rows[i].Cells[0].Tag;
						this.dgViewPrintList.Rows[value].Cells[1].Tag = this.frmParent.dgvPrintListPrintFrm.Rows[i].Cells[1].Tag;
						this.dgViewPrintList.Rows[value].Cells[2].Tag = this.frmParent.dgvPrintListPrintFrm.Rows[i].Cells[2].Tag;
					}
					this.dgViewPrintList.ClearSelection();
				}
			}
			catch (Exception exception)
			{
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			try
			{
				this.frmParent.frmParent.EnableTreeView(false);
				this.frmParent.Enabled = true;
				e.Cancel = false;
				base.Dispose();
			}
			catch (Exception exception)
			{
			}
		}

		private int SelectPrintRange(ref string sRngStart, ref string sRngEnd, int intRowIndex)
		{
			int num;
			try
			{
				int num1 = 0;
				num1 = (int.Parse(sRngStart) <= int.Parse(sRngEnd) ? 0 : 1);
				num = num1;
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		private void SetControlsText()
		{
			this.Text = this.GetResource("Page Specified", "PAGE_SPECIFIED", ResourceType.TITLE);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
			try
			{
				this.dgViewPrintList.Columns["no"].HeaderText = this.GetResource("No", "NO", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewPrintList.Columns["pageFrom"].HeaderText = this.GetResource("From", "PAGE_FROM", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewPrintList.Columns["pageTo"].HeaderText = this.GetResource("To", "PAGE_TO", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.dgViewPrintList.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgViewPrintList.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		public void UpdateFromGridColumn(string strFrom, string sFromIndex)
		{
			try
			{
				try
				{
					if (this.dgViewPrintList.SelectedCells.Count <= 0)
					{
						int num = 0;
						int count = 0;
						if (this.dgViewPrintList.Rows.Count > 0)
						{
							count = this.dgViewPrintList.Rows.Count - 1;
						}
						int count1 = this.dgViewPrintList.Rows.Count;
						if (count1 == 0)
						{
							if (this.dgViewPrintList.Columns.Count == 0)
							{
								this.GetPageSpecifiedGrid();
							}
							num = 1;
							this.dgViewPrintList.Rows.Add();
							DataGridViewRow item = this.dgViewPrintList.Rows[0];
							item.Cells[0].Value = num.ToString();
							DataGridViewCell dataGridViewCell = item.Cells[1];
							dataGridViewCell.Value = strFrom;
							dataGridViewCell.Tag = sFromIndex;
							item.Cells[2].Value = "";
							item.Cells[3].Value = "Delete";
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
							num++;
							this.dgViewPrintList.ClearSelection();
						}
						else if (count1 <= 0 || count1 >= 10)
						{
							if (count1 > 0 && count1 == 10 && count != 10)
							{
								if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
								{
									string str = sFromIndex;
									string str1 = this.dgViewPrintList.Rows[count].Cells[2].Tag.ToString();
									if (this.SelectPrintRange(ref str, ref str1, count) != 1)
									{
										DataGridViewRow dataGridViewRow = this.dgViewPrintList.Rows[count];
										num = count1;
										dataGridViewRow.Cells[1].Value = strFrom;
										dataGridViewRow.Cells[1].Tag = sFromIndex;
										this.dgViewPrintList.Update();
										this.dgViewPrintList.Refresh();
										num++;
									}
									else
									{
										DataGridViewRow value = this.dgViewPrintList.Rows[count];
										num = count1;
										value.Cells[1].Value = value.Cells[2].Value;
										value.Cells[1].Tag = value.Cells[2].Tag;
										value.Cells[2].Value = strFrom;
										value.Cells[2].Tag = sFromIndex;
										this.dgViewPrintList.Update();
										this.dgViewPrintList.Refresh();
										num++;
									}
								}
								else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() == "")
								{
									DataGridViewRow item1 = this.dgViewPrintList.Rows[count];
									num = count1;
									item1.Cells[1].Value = strFrom.ToString();
									item1.Cells[1].Tag = sFromIndex;
									this.dgViewPrintList.Update();
									this.dgViewPrintList.Refresh();
									num++;
								}
								else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
								{
									MessageBox.Show(this.GetResource("The possible number is up to 10.  Please specify again after DELETE or print.", "RANGE_LIMIT", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
						{
							count++;
							num = count1 + 1;
							this.dgViewPrintList.Rows.Add();
							DataGridViewRow dataGridViewRow1 = this.dgViewPrintList.Rows[count];
							dataGridViewRow1.Cells[0].Value = num.ToString();
							DataGridViewCell dataGridViewCell1 = dataGridViewRow1.Cells[1];
							dataGridViewCell1.Value = strFrom;
							dataGridViewCell1.Tag = sFromIndex;
							dataGridViewRow1.Cells[2].Value = "";
							dataGridViewRow1.Cells[3].Value = "Delete";
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
							this.dgViewPrintList.ClearSelection();
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow item2 = this.dgViewPrintList.Rows[count];
							DataGridViewCell str2 = item2.Cells[1];
							DataGridViewCell dataGridViewCell2 = item2.Cells[2];
							if (Convert.ToInt32(sFromIndex) <= Convert.ToInt32(dataGridViewCell2.Tag.ToString()))
							{
								str2.Value = strFrom;
								str2.Tag = sFromIndex;
							}
							else
							{
								str2.Value = dataGridViewCell2.Value.ToString();
								str2.Tag = dataGridViewCell2.Tag.ToString();
								dataGridViewCell2.Value = strFrom;
								dataGridViewCell2.Tag = sFromIndex;
							}
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
							this.dgViewPrintList.ClearSelection();
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() == "")
						{
							DataGridViewRow dataGridViewRow2 = this.dgViewPrintList.Rows[count];
							DataGridViewCell item3 = dataGridViewRow2.Cells[1];
							item3.Value = strFrom;
							item3.Tag = sFromIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
							this.dgViewPrintList.ClearSelection();
						}
					}
					else
					{
						int rowIndex = this.dgViewPrintList.CurrentCell.RowIndex;
						if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow3 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell tag = dataGridViewRow3.Cells[1];
							DataGridViewCell dataGridViewCell3 = dataGridViewRow3.Cells[2];
							if (Convert.ToInt32(sFromIndex) <= Convert.ToInt32(dataGridViewCell3.Tag.ToString()))
							{
								tag.Value = strFrom;
								tag.Tag = sFromIndex;
							}
							else
							{
								tag.Value = dataGridViewCell3.Value;
								tag.Tag = dataGridViewCell3.Tag;
								dataGridViewCell3.Value = strFrom;
								dataGridViewCell3.Tag = sFromIndex;
							}
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() == "")
						{
							DataGridViewRow item4 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell dataGridViewCell4 = item4.Cells[1];
							dataGridViewCell4.Value = strFrom;
							dataGridViewCell4.Tag = sFromIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow4 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell value1 = dataGridViewRow4.Cells[1];
							DataGridViewCell item5 = dataGridViewRow4.Cells[2];
							if (Convert.ToInt32(sFromIndex) <= Convert.ToInt32(item5.Tag.ToString()))
							{
								value1.Value = strFrom;
								value1.Tag = sFromIndex;
							}
							else
							{
								value1.Value = item5.Value;
								value1.Tag = item5.Tag;
								item5.Value = strFrom;
								item5.Tag = sFromIndex;
							}
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							string str3 = sFromIndex;
							string str4 = this.dgViewPrintList.Rows[rowIndex].Cells[2].Tag.ToString();
							if (this.SelectPrintRange(ref str3, ref str4, rowIndex) != 1)
							{
								DataGridViewRow dataGridViewRow5 = this.dgViewPrintList.Rows[rowIndex];
								dataGridViewRow5.Cells[1].Value = strFrom;
								dataGridViewRow5.Cells[1].Tag = sFromIndex;
								this.dgViewPrintList.Update();
								this.dgViewPrintList.Refresh();
							}
							else
							{
								DataGridViewRow tag1 = this.dgViewPrintList.Rows[rowIndex];
								tag1.Cells[1].Value = tag1.Cells[2].Value;
								tag1.Cells[1].Tag = tag1.Cells[2].Tag;
								tag1.Cells[2].Value = strFrom;
								tag1.Cells[2].Tag = sFromIndex;
								this.dgViewPrintList.Update();
								this.dgViewPrintList.Refresh();
							}
						}
					}
				}
				catch (Exception exception)
				{
				}
			}
			finally
			{
				if (this.dgViewPrintList.Rows.Count > 0)
				{
					this.btnOK.Enabled = true;
				}
			}
		}

		private void updateGridSerialNo()
		{
			int count = this.dgViewPrintList.Rows.Count;
			int num = 1;
			for (int i = 0; i < count; i++)
			{
				DataGridViewRow item = this.dgViewPrintList.Rows[i];
				item.Cells[0].Value = num + i;
			}
		}

		public void UpdatePrintThisGridCol(string strPrintThis, string sPrintThisIndex)
		{
			try
			{
				try
				{
					if (this.dgViewPrintList.SelectedCells.Count <= 0)
					{
						int num = 0;
						int count = 0;
						if (this.dgViewPrintList.Rows.Count > 0)
						{
							count = this.dgViewPrintList.Rows.Count - 1;
						}
						int count1 = this.dgViewPrintList.Rows.Count;
						if (count1 == 0)
						{
							num = 1;
							this.dgViewPrintList.Rows.Add();
							DataGridViewRow item = this.dgViewPrintList.Rows[0];
							item.Cells[0].Value = num.ToString();
							DataGridViewCell dataGridViewCell = item.Cells[1];
							dataGridViewCell.Value = strPrintThis;
							dataGridViewCell.Tag = sPrintThisIndex;
							DataGridViewCell item1 = item.Cells[2];
							item1.Value = strPrintThis;
							item1.Tag = sPrintThisIndex;
							item.Cells[3].Value = "Delete";
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
							this.dgViewPrintList.ClearSelection();
							num++;
						}
						else if (count1 <= 0 || count1 >= 10)
						{
							if (count1 > 0 && count1 == 10 && count != 10)
							{
								if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
								{
									DataGridViewRow str = this.dgViewPrintList.Rows[count];
									num = count1;
									str.Cells[1].Value = strPrintThis.ToString();
									str.Cells[1].Tag = sPrintThisIndex.ToString();
									str.Cells[2].Value = strPrintThis.ToString();
									str.Cells[2].Tag = sPrintThisIndex.ToString();
									this.dgViewPrintList.Update();
									this.dgViewPrintList.Refresh();
									this.dgViewPrintList.ClearSelection();
									num++;
								}
								else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() == "")
								{
									DataGridViewRow dataGridViewRow = this.dgViewPrintList.Rows[count];
									num = count1;
									dataGridViewRow.Cells[1].Value = strPrintThis.ToString();
									dataGridViewRow.Cells[1].Tag = sPrintThisIndex.ToString();
									dataGridViewRow.Cells[2].Value = strPrintThis.ToString();
									dataGridViewRow.Cells[2].Tag = sPrintThisIndex.ToString();
									this.dgViewPrintList.Update();
									this.dgViewPrintList.Refresh();
									this.dgViewPrintList.ClearSelection();
									num++;
								}
								else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
								{
									MessageBox.Show(this.GetResource("The possible number is up to 10.  Please specify again after DELETE or print.", "RANGE_LIMIT", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
						{
							count++;
							num = count1 + 1;
							this.dgViewPrintList.Rows.Add();
							DataGridViewRow str1 = this.dgViewPrintList.Rows[count];
							str1.Cells[0].Value = num.ToString();
							DataGridViewCell dataGridViewCell1 = str1.Cells[1];
							dataGridViewCell1.Value = strPrintThis;
							dataGridViewCell1.Tag = sPrintThisIndex;
							DataGridViewCell item2 = str1.Cells[2];
							item2.Value = strPrintThis;
							item2.Tag = sPrintThisIndex;
							str1.Cells[3].Value = "Delete";
							this.dgViewPrintList.Update();
							this.dgViewPrintList.ClearSelection();
							this.dgViewPrintList.Refresh();
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow1 = this.dgViewPrintList.Rows[count];
							num = count1;
							dataGridViewRow1.Cells[1].Value = strPrintThis.ToString();
							dataGridViewRow1.Cells[1].Tag = sPrintThisIndex.ToString();
							dataGridViewRow1.Cells[2].Value = strPrintThis.ToString();
							dataGridViewRow1.Cells[2].Tag = sPrintThisIndex.ToString();
							this.dgViewPrintList.Update();
							this.dgViewPrintList.ClearSelection();
							this.dgViewPrintList.Refresh();
							num++;
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() == "")
						{
							DataGridViewRow str2 = this.dgViewPrintList.Rows[count];
							num = count1;
							str2.Cells[1].Value = strPrintThis.ToString();
							str2.Cells[1].Tag = sPrintThisIndex.ToString();
							str2.Cells[2].Value = strPrintThis.ToString();
							str2.Cells[2].Tag = sPrintThisIndex.ToString();
							this.dgViewPrintList.Update();
							this.dgViewPrintList.ClearSelection();
							this.dgViewPrintList.Refresh();
							num++;
						}
					}
					else
					{
						int rowIndex = this.dgViewPrintList.CurrentCell.RowIndex;
						if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() == "")
						{
							DataGridViewRow dataGridViewRow2 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell dataGridViewCell2 = dataGridViewRow2.Cells[1];
							dataGridViewCell2.Value = strPrintThis;
							dataGridViewCell2.Tag = sPrintThisIndex;
							DataGridViewCell item3 = dataGridViewRow2.Cells[2];
							item3.Value = strPrintThis;
							item3.Tag = sPrintThisIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow3 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell dataGridViewCell3 = dataGridViewRow3.Cells[1];
							dataGridViewCell3.Value = strPrintThis;
							dataGridViewCell3.Tag = sPrintThisIndex;
							DataGridViewCell item4 = dataGridViewRow3.Cells[2];
							item4.Value = strPrintThis;
							item4.Tag = sPrintThisIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow4 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell dataGridViewCell4 = dataGridViewRow4.Cells[1];
							dataGridViewCell4.Value = strPrintThis;
							dataGridViewCell4.Tag = sPrintThisIndex;
							DataGridViewCell item5 = dataGridViewRow4.Cells[2];
							item5.Value = strPrintThis;
							item5.Tag = sPrintThisIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow5 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell dataGridViewCell5 = dataGridViewRow5.Cells[1];
							dataGridViewCell5.Value = strPrintThis;
							dataGridViewCell5.Tag = sPrintThisIndex;
							DataGridViewCell item6 = dataGridViewRow5.Cells[2];
							item6.Value = strPrintThis;
							item6.Tag = sPrintThisIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
					}
				}
				catch (Exception exception)
				{
				}
			}
			finally
			{
				if (this.dgViewPrintList.Rows.Count > 0)
				{
					this.btnOK.Enabled = true;
				}
			}
		}

		public void UpdateToGridCol(string strTo, string sToIndex)
		{
			try
			{
				try
				{
					if (this.dgViewPrintList.SelectedCells.Count <= 0)
					{
						int count = 0;
						if (this.dgViewPrintList.Rows.Count > 0)
						{
							count = this.dgViewPrintList.Rows.Count - 1;
						}
						int num = 0;
						int count1 = this.dgViewPrintList.Rows.Count;
						if (count1 == 0)
						{
							if (this.dgViewPrintList.Columns.Count == 0)
							{
								this.GetPageSpecifiedGrid();
							}
							num = 1;
							this.dgViewPrintList.Rows.Add();
							DataGridViewRow item = this.dgViewPrintList.Rows[0];
							item.Cells[0].Value = num.ToString();
							item.Cells[1].Value = "";
							DataGridViewCell str = item.Cells[2];
							str.Value = strTo.ToString();
							str.Tag = sToIndex;
							item.Cells[3].Value = "Delete";
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
							this.dgViewPrintList.ClearSelection();
							num++;
						}
						else if (count1 <= 0 || count1 >= 10)
						{
							if (count1 > 0 && count1 == 10 && count != 10)
							{
								if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
								{
									DataGridViewRow dataGridViewRow = this.dgViewPrintList.Rows[count];
									num = count1;
									dataGridViewRow.Cells[2].Value = strTo.ToString();
									dataGridViewRow.Cells[2].Tag = sToIndex;
									this.dgViewPrintList.Update();
									this.dgViewPrintList.ClearSelection();
									this.dgViewPrintList.Refresh();
									num++;
								}
								else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() == "")
								{
									string str1 = this.dgViewPrintList.Rows[count].Cells[1].Tag.ToString();
									string str2 = sToIndex;
									if (this.SelectPrintRange(ref str1, ref str2, count) != 1)
									{
										DataGridViewRow item1 = this.dgViewPrintList.Rows[count];
										num = count1;
										item1.Cells[2].Value = strTo.ToString();
										item1.Cells[2].Tag = sToIndex;
										this.dgViewPrintList.Update();
										this.dgViewPrintList.ClearSelection();
										this.dgViewPrintList.Refresh();
										num++;
									}
									else
									{
										DataGridViewRow dataGridViewRow1 = this.dgViewPrintList.Rows[count];
										num = count1;
										dataGridViewRow1.Cells[2].Value = dataGridViewRow1.Cells[1].Value.ToString();
										dataGridViewRow1.Cells[2].Tag = dataGridViewRow1.Cells[1].Tag.ToString();
										dataGridViewRow1.Cells[1].Value = strTo.ToString();
										dataGridViewRow1.Cells[1].Tag = sToIndex;
										this.dgViewPrintList.Update();
										this.dgViewPrintList.ClearSelection();
										this.dgViewPrintList.Refresh();
										num++;
									}
								}
								else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
								{
									MessageBox.Show(this.GetResource("The possible number is up to 10.  Please specify again after DELETE or print.", "RANGE_LIMIT", ResourceType.POPUP_MESSAGE), "GSPcLocal Viewer", MessageBoxButtons.OK, MessageBoxIcon.Hand);
								}
							}
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() == "")
						{
							count++;
							num = count1 + 1;
							this.dgViewPrintList.Rows.Add();
							DataGridViewRow item2 = this.dgViewPrintList.Rows[count];
							item2.Cells[0].Value = num.ToString();
							item2.Cells[1].Value = "";
							DataGridViewCell dataGridViewCell = item2.Cells[2];
							dataGridViewCell.Value = strTo.ToString();
							dataGridViewCell.Tag = sToIndex;
							item2.Cells[3].Value = "Delete";
							this.dgViewPrintList.Update();
							this.dgViewPrintList.ClearSelection();
							this.dgViewPrintList.Refresh();
						}
						else if (this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow2 = this.dgViewPrintList.Rows[count];
							DataGridViewCell dataGridViewCell1 = dataGridViewRow2.Cells[2];
							dataGridViewCell1.Value = strTo.ToString();
							dataGridViewCell1.Tag = sToIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.ClearSelection();
							this.dgViewPrintList.Refresh();
						}
						else if (!(this.dgViewPrintList.Rows[count].Cells[1].Value.ToString() != "") || !(this.dgViewPrintList.Rows[count].Cells[2].Value.ToString() == ""))
						{
							num = count1 + 1;
							this.dgViewPrintList.Rows.Add();
							count++;
							DataGridViewRow item3 = this.dgViewPrintList.Rows[count];
							item3.Cells[0].Value = num.ToString();
							item3.Cells[1].Value = "";
							DataGridViewCell dataGridViewCell2 = item3.Cells[2];
							dataGridViewCell2.Value = strTo.ToString();
							dataGridViewCell2.Tag = sToIndex;
							item3.Cells[3].Value = "Delete";
							this.dgViewPrintList.Update();
							this.dgViewPrintList.ClearSelection();
							this.dgViewPrintList.Refresh();
						}
						else
						{
							DataGridViewRow dataGridViewRow3 = this.dgViewPrintList.Rows[count];
							DataGridViewCell value = dataGridViewRow3.Cells[2];
							DataGridViewCell dataGridViewCell3 = dataGridViewRow3.Cells[1];
							int num1 = Convert.ToInt32(dataGridViewCell3.Tag);
							if (Convert.ToInt32(sToIndex) >= num1)
							{
								value.Value = strTo.ToString();
								value.Tag = sToIndex;
							}
							else
							{
								value.Value = dataGridViewCell3.Value;
								value.Tag = dataGridViewCell3.Tag;
								dataGridViewCell3.Value = strTo;
								dataGridViewCell3.Tag = sToIndex;
							}
							this.dgViewPrintList.Update();
							this.dgViewPrintList.ClearSelection();
							this.dgViewPrintList.Refresh();
						}
					}
					else
					{
						int rowIndex = this.dgViewPrintList.CurrentCell.RowIndex;
						if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow item4 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell tag = item4.Cells[2];
							DataGridViewCell dataGridViewCell4 = item4.Cells[1];
							int num2 = Convert.ToInt32(dataGridViewCell4.Tag);
							if (Convert.ToInt32(sToIndex) >= num2)
							{
								tag.Value = strTo.ToString();
								tag.Tag = sToIndex;
							}
							else
							{
								tag.Value = dataGridViewCell4.Value;
								tag.Tag = dataGridViewCell4.Tag;
								dataGridViewCell4.Value = strTo;
								dataGridViewCell4.Tag = sToIndex;
							}
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						else if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() == "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() != "")
						{
							DataGridViewRow dataGridViewRow4 = this.dgViewPrintList.Rows[rowIndex];
							DataGridViewCell item5 = dataGridViewRow4.Cells[2];
							item5.Value = strTo;
							item5.Tag = sToIndex;
							this.dgViewPrintList.Update();
							this.dgViewPrintList.Refresh();
						}
						if (this.dgViewPrintList.Rows[rowIndex].Cells[1].Value.ToString() != "" && this.dgViewPrintList.Rows[rowIndex].Cells[2].Value.ToString() == "")
						{
							string str3 = this.dgViewPrintList.Rows[rowIndex].Cells[1].Tag.ToString();
							string str4 = sToIndex;
							if (this.SelectPrintRange(ref str3, ref str4, rowIndex) != 1)
							{
								DataGridViewRow dataGridViewRow5 = this.dgViewPrintList.Rows[rowIndex];
								dataGridViewRow5.Cells[2].Value = strTo;
								dataGridViewRow5.Cells[2].Tag = sToIndex;
								this.dgViewPrintList.Update();
								this.dgViewPrintList.Refresh();
							}
							else
							{
								DataGridViewRow str5 = this.dgViewPrintList.Rows[rowIndex];
								str5.Cells[2].Value = str5.Cells[1].Value.ToString();
								str5.Cells[2].Tag = str5.Cells[1].Tag.ToString();
								str5.Cells[1].Value = strTo.ToString();
								str5.Cells[1].Tag = sToIndex;
								this.dgViewPrintList.Update();
								this.dgViewPrintList.Refresh();
							}
						}
					}
				}
				catch (Exception exception)
				{
				}
			}
			finally
			{
				if (this.dgViewPrintList.Rows.Count > 0)
				{
					this.btnOK.Enabled = true;
				}
			}
		}
	}
}
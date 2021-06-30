using GSPcLocalViewer;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmConfiguration
{
	public class frmConfigSelectionList : Form
	{
		private IContainer components;

		private Panel pnlFrm;

		private Panel panel1;

		private Button btnOK;

		private Button btnCancel;

		private Label lblSelectionSettings;

		private BackgroundWorker bgWorker;

		private PictureBox picLoading;

		public DataGridView dgViewSelectionListSettings;

		private frmConfig frmParent;

		public int serverId;

		private Rectangle dragBoxFromMouseDown;

		private int rowIndexFromMouseDown;

		private int rowIndexOfItemUnderMouseToDrop;

		private string sLocalFile = string.Empty;

		private string sServerFile = string.Empty;

		private Download objDownloader;

		private Dictionary<string, string> dicSLSettings = new Dictionary<string, string>();

		public frmConfigSelectionList(frmConfig frm, int _serverId)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.serverId = _serverId;
			this.objDownloader = new Download();
			this.UpdateFont();
		}

		private void AddSelectionListSettingsRow(string sVal, string sKey)
		{
			try
			{
				string[] strArrays = sVal.Split(new char[] { '|' });
				if ((int)strArrays.Length == 3)
				{
					string[] strArrays1 = new string[] { sVal, "|True|True|", strArrays[1], "|", strArrays[2] };
					sVal = string.Concat(strArrays1);
				}
				else if ((int)strArrays.Length == 4)
				{
					string[] strArrays2 = new string[] { sVal, "|True|True|", strArrays[1], "|", strArrays[2] };
					sVal = string.Concat(strArrays2);
				}
				string[] strArrays3 = sVal.Split(new char[] { '|' });
				DataGridViewRow dataGridViewRow = new DataGridViewRow();
				DataGridViewCheckBoxCell dataGridViewCheckBoxCell = new DataGridViewCheckBoxCell()
				{
					Value = strArrays3[3]
				};
				dataGridViewRow.Cells.Add(dataGridViewCheckBoxCell);
				DataGridViewCheckBoxCell dataGridViewCheckBoxCell1 = new DataGridViewCheckBoxCell()
				{
					Value = strArrays3[4]
				};
				dataGridViewRow.Cells.Add(dataGridViewCheckBoxCell1);
				DataGridViewTextBoxCell dataGridViewTextBoxCell = new DataGridViewTextBoxCell()
				{
					Value = sKey
				};
				dataGridViewRow.Cells.Add(dataGridViewTextBoxCell);
				DataGridViewTextBoxCell dGHeaderCellValue = new DataGridViewTextBoxCell();
				if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
				{
					dGHeaderCellValue.Value = strArrays3[0];
				}
				else
				{
					dGHeaderCellValue.Value = Global.GetDGHeaderCellValue("SLIST_SETTINGS", sKey, strArrays3[0], this.serverId);
				}
				dataGridViewRow.Cells.Add(dGHeaderCellValue);
				DataGridViewTextBoxCell dataGridViewTextBoxCell1 = new DataGridViewTextBoxCell()
				{
					Value = strArrays3[6]
				};
				dataGridViewRow.Cells.Add(dataGridViewTextBoxCell1);
				DataGridViewComboBoxCell dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
				string[] strArrays4 = "L,R,C".ToString().Split(new char[] { ',' });
				dataGridViewComboBoxCell.Items.AddRange(strArrays4);
				string str = strArrays3[5].ToString();
				string str1 = str;
				if (str != null)
				{
					if (str1 == "L")
					{
						dataGridViewComboBoxCell.Value = strArrays4[0];
						goto Label0;
					}
					else if (str1 == "R")
					{
						dataGridViewComboBoxCell.Value = strArrays4[1];
						goto Label0;
					}
					else
					{
						if (str1 != "C")
						{
							goto Label2;
						}
						dataGridViewComboBoxCell.Value = strArrays4[2];
						goto Label0;
					}
				}
			Label2:
				dataGridViewComboBoxCell.Value = strArrays4[0];
			Label0:
				dataGridViewRow.Cells.Add(dataGridViewComboBoxCell);
				dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
				this.dgViewSelectionListSettings.Rows.Add(dataGridViewRow);
			}
			catch (Exception exception)
			{
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.InitializeSelectionListGrid();
			this.LoadDataGridView();
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.HideLoading(this.pnlFrm);
			this.SetControlsText();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.frmParent.CloseAndSaveSettings();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmConfigSelectionList_Load(object sender, EventArgs e)
		{
			this.ShowLoading(this.pnlFrm);
			this.bgWorker.RunWorkerAsync();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='CONFIGURATION']");
				str = string.Concat(str, "/Screen[@Name='SELECTIONLIST_SETTINGS']");
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
			this.pnlFrm = new Panel();
			this.picLoading = new PictureBox();
			this.dgViewSelectionListSettings = new DataGridView();
			this.lblSelectionSettings = new Label();
			this.panel1 = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.bgWorker = new BackgroundWorker();
			this.pnlFrm.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			((ISupportInitialize)this.dgViewSelectionListSettings).BeginInit();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.pnlFrm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlFrm.Controls.Add(this.picLoading);
			this.pnlFrm.Controls.Add(this.dgViewSelectionListSettings);
			this.pnlFrm.Controls.Add(this.lblSelectionSettings);
			this.pnlFrm.Controls.Add(this.panel1);
			this.pnlFrm.Dock = DockStyle.Fill;
			this.pnlFrm.Location = new Point(0, 0);
			this.pnlFrm.Name = "pnlFrm";
			this.pnlFrm.Size = new System.Drawing.Size(450, 450);
			this.pnlFrm.TabIndex = 0;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(417, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.TabIndex = 3;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.dgViewSelectionListSettings.AllowDrop = true;
			this.dgViewSelectionListSettings.AllowUserToAddRows = false;
			this.dgViewSelectionListSettings.AllowUserToDeleteRows = false;
			this.dgViewSelectionListSettings.AllowUserToResizeRows = false;
			dataGridViewCellStyle.BackColor = Color.White;
			this.dgViewSelectionListSettings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dgViewSelectionListSettings.BackgroundColor = Color.White;
			this.dgViewSelectionListSettings.BorderStyle = BorderStyle.Fixed3D;
			control.Alignment = DataGridViewContentAlignment.MiddleLeft;
			control.BackColor = SystemColors.Control;
			control.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			control.ForeColor = Color.Black;
			control.SelectionBackColor = SystemColors.Highlight;
			control.SelectionForeColor = SystemColors.HighlightText;
			control.WrapMode = DataGridViewTriState.True;
			this.dgViewSelectionListSettings.ColumnHeadersDefaultCellStyle = control;
			this.dgViewSelectionListSettings.ColumnHeadersHeight = 26;
			this.dgViewSelectionListSettings.Dock = DockStyle.Fill;
			this.dgViewSelectionListSettings.Location = new Point(0, 27);
			this.dgViewSelectionListSettings.Name = "dgViewSelectionListSettings";
			font.Alignment = DataGridViewContentAlignment.MiddleLeft;
			font.BackColor = SystemColors.Control;
			font.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			font.ForeColor = Color.Black;
			font.SelectionBackColor = SystemColors.Highlight;
			font.SelectionForeColor = SystemColors.HighlightText;
			font.WrapMode = DataGridViewTriState.True;
			this.dgViewSelectionListSettings.RowHeadersDefaultCellStyle = font;
			this.dgViewSelectionListSettings.RowHeadersWidth = 32;
			this.dgViewSelectionListSettings.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			white.BackColor = Color.White;
			white.ForeColor = Color.Black;
			white.SelectionBackColor = Color.SteelBlue;
			white.SelectionForeColor = Color.White;
			this.dgViewSelectionListSettings.RowsDefaultCellStyle = white;
			this.dgViewSelectionListSettings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgViewSelectionListSettings.ShowRowErrors = false;
			this.dgViewSelectionListSettings.Size = new System.Drawing.Size(448, 390);
			this.dgViewSelectionListSettings.TabIndex = 2;
			this.lblSelectionSettings.Dock = DockStyle.Top;
			this.lblSelectionSettings.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblSelectionSettings.Location = new Point(0, 0);
			this.lblSelectionSettings.Name = "lblSelectionSettings";
			this.lblSelectionSettings.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblSelectionSettings.Size = new System.Drawing.Size(448, 27);
			this.lblSelectionSettings.TabIndex = 1;
			this.lblSelectionSettings.Text = "List Settings";
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Dock = DockStyle.Bottom;
			this.panel1.Location = new Point(0, 417);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
			this.panel1.Size = new System.Drawing.Size(448, 31);
			this.panel1.TabIndex = 0;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(283, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(358, 4);
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
			base.ClientSize = new System.Drawing.Size(450, 450);
			base.Controls.Add(this.pnlFrm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmConfigSelectionList";
			this.Text = "Selection List Configurations";
			base.Load += new EventHandler(this.frmConfigSelectionList_Load);
			this.pnlFrm.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			((ISupportInitialize)this.dgViewSelectionListSettings).EndInit();
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void InitializeSelectionListGrid()
		{
			if (this.dgViewSelectionListSettings.InvokeRequired)
			{
				this.dgViewSelectionListSettings.Invoke(new frmConfigSelectionList.InitializeSelectionListGridDelegate(this.InitializeSelectionListGrid));
				return;
			}
			try
			{
				DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn()
				{
					Name = "EnableDisplay",
					HeaderText = "Disp",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 50
				};
				this.dgViewSelectionListSettings.Columns.Add(dataGridViewCheckBoxColumn);
				DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn()
				{
					Name = "EnablePrint",
					HeaderText = "Prt",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 50
				};
				this.dgViewSelectionListSettings.Columns.Add(dataGridViewCheckBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					Name = "FieldId",
					HeaderText = "Field Id",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100,
					ReadOnly = true
				};
				this.dgViewSelectionListSettings.Columns.Add(dataGridViewTextBoxColumn);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
				{
					Name = "DisplayName",
					HeaderText = "Display Name",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100,
					ReadOnly = true
				};
				this.dgViewSelectionListSettings.Columns.Add(dataGridViewTextBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					Name = "Width",
					HeaderText = "Print Width",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSelectionListSettings.Columns.Add(dataGridViewTextBoxColumn2);
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn()
				{
					Name = "Alignment",
					HeaderText = "Print Position",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewSelectionListSettings.Columns.Add(dataGridViewComboBoxColumn);
			}
			catch
			{
			}
		}

		private void LoadDataGridView()
		{
			if (this.dgViewSelectionListSettings.InvokeRequired)
			{
				this.dgViewSelectionListSettings.Invoke(new frmConfigSelectionList.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
				return;
			}
			this.dgViewSelectionListSettings.Rows.Clear();
			ArrayList arrayLists = new ArrayList();
			List<string> strs = new List<string>();
			IniFileIO iniFileIO = new IniFileIO();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"), "SLIST_SETTINGS");
			for (int i = 0; i < arrayLists.Count; i++)
			{
				try
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					string keyValue = iniFileIO1.GetKeyValue("SLIST_SETTINGS", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"));
					this.AddSelectionListSettingsRow(keyValue, arrayLists[i].ToString());
					string str = arrayLists[i].ToString();
					string[] strArrays = keyValue.Split(new char[] { '|' });
					if ((int)strArrays.Length > 0)
					{
						string str1 = string.Concat(strArrays[1], ",", strArrays[2]);
						this.dicSLSettings.Add(str, str1);
					}
				}
				catch (Exception exception)
				{
				}
			}
		}

		public bool SaveSettings()
		{
			bool flag;
			try
			{
				bool flag1 = false;
				bool flag2 = false;
				IEnumerator enumerator = ((IEnumerable)this.dgViewSelectionListSettings.Rows).GetEnumerator();
				try
				{
					do
					{
						if (!enumerator.MoveNext())
						{
							break;
						}
						DataGridViewRow current = (DataGridViewRow)enumerator.Current;
						DataGridViewCheckBoxCell item = (DataGridViewCheckBoxCell)current.Cells[0];
						DataGridViewCheckBoxCell dataGridViewCheckBoxCell = (DataGridViewCheckBoxCell)current.Cells[1];
						if (Convert.ToBoolean(item.Value))
						{
							flag1 = true;
						}
						if (!Convert.ToBoolean(dataGridViewCheckBoxCell.Value))
						{
							continue;
						}
						flag2 = true;
					}
					while (!flag2 || !flag1);
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				if (!flag1 || !flag2)
				{
					MessageBox.Show(this.GetResource("Please select always one or more Disp and Prt.", "SELECT_ONE_CHECK_BOX", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
					flag = false;
				}
				else
				{
					string empty = string.Empty;
					if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
					{
						for (int i = 0; i < this.dgViewSelectionListSettings.Rows.Count; i++)
						{
							Global.SaveToLanguageIni("SLIST_SETTINGS", this.dgViewSelectionListSettings[2, i].Value.ToString(), this.dgViewSelectionListSettings[3, i].Value.ToString(), this.serverId);
						}
					}
					for (int j = 0; j < this.dgViewSelectionListSettings.Rows.Count; j++)
					{
						string str = this.dgViewSelectionListSettings[0, j].Value.ToString();
						string str1 = this.dgViewSelectionListSettings[1, j].Value.ToString();
						string str2 = this.dgViewSelectionListSettings[2, j].Value.ToString();
						string engHeaderVal = Global.GetEngHeaderVal("SLIST_SETTINGS", this.dgViewSelectionListSettings[2, j].Value.ToString(), this.serverId);
						char[] chrArray = new char[] { '|' };
						engHeaderVal = engHeaderVal.Split(chrArray)[0];
						string str3 = this.dgViewSelectionListSettings[4, j].Value.ToString();
						string str4 = this.dgViewSelectionListSettings[5, j].Value.ToString();
						string str5 = this.dicSLSettings[str2].ToString();
						char[] chrArray1 = new char[] { ',' };
						string str6 = str5.Split(chrArray1)[0].ToString();
						char[] chrArray2 = new char[] { ',' };
						string str7 = str5.Split(chrArray2)[1].ToString();
						string[] strArrays = new string[] { engHeaderVal, "|", str6, "|", str7, "|", str, "|", str1, "|", str4, "|", str3 };
						string str8 = string.Concat(strArrays);
						if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
						{
							this.dgViewSelectionListSettings[3, j].Value.ToString();
						}
						Global.SaveToServerIni("SLIST_SETTINGS", str2, str8, this.serverId);
					}
					flag = true;
				}
			}
			catch (Exception exception)
			{
				flag = false;
			}
			return flag;
		}

		private void SetControlsText()
		{
			this.Text = this.GetResource("Selection List Configurations", "SELECTION_LIST_CONFIG", ResourceType.LABEL);
			this.lblSelectionSettings.Text = this.GetResource("List Settings", "LIST_SETTINGS", ResourceType.LABEL);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
			try
			{
				this.dgViewSelectionListSettings.Columns["EnableDisplay"].HeaderText = this.GetResource("Enable Display", "ENABLE_DISPALY", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSelectionListSettings.Columns["EnablePrint"].HeaderText = this.GetResource("Enable Print", "ENABLE_PRINT", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSelectionListSettings.Columns["FieldId"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSelectionListSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSelectionListSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewSelectionListSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
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
					if (control == this.picLoading || control == this.lblSelectionSettings)
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
			this.lblSelectionSettings.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgViewSelectionListSettings.Font = Settings.Default.appFont;
			this.dgViewSelectionListSettings.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgViewSelectionListSettings.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		private delegate void InitializeSelectionListGridDelegate();

		private delegate void LoadDataGridViewXmlDelegate();
	}
}
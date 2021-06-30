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
	public class frmConfigPartList : Form
	{
		private frmConfig frmParent;

		private int serverId;

		private Rectangle dragBoxFromMouseDown;

		private int rowIndexFromMouseDown;

		private int rowIndexOfItemUnderMouseToDrop;

		private string sLocalFile = string.Empty;

		private string sServerFile = string.Empty;

		private Download objDownloader;

		private Dictionary<string, string> dicPLSettings = new Dictionary<string, string>();

		private IContainer components;

		private Panel pnlFrm;

		private Panel PnlControl;

		private Button btnOK;

		private Button btnCancel;

		private Label lblPartListSettings;

		private PictureBox picLoading;

		private BackgroundWorker bgWorker;

		public DataGridView dgViewPartListSettings;

		public frmConfigPartList(frmConfig frm, int _serverId)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.serverId = _serverId;
			this.objDownloader = new Download();
			this.UpdateFont();
		}

		private void AddPartListSettingsRow(string sVal, string sKey)
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
				if ((int)strArrays3.Length == 7)
				{
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
						dGHeaderCellValue.Value = Global.GetDGHeaderCellValue("PLIST_SETTINGS", sKey, strArrays3[0], this.serverId);
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
							goto Label2;
						}
						else if (str1 == "R")
						{
							dataGridViewComboBoxCell.Value = strArrays4[1];
							goto Label2;
						}
						else
						{
							if (str1 != "C")
							{
								goto Label5;
							}
							dataGridViewComboBoxCell.Value = strArrays4[2];
							goto Label2;
						}
					}
				Label5:
					dataGridViewComboBoxCell.Value = strArrays4[0];
				Label2:
					dataGridViewRow.Cells.Add(dataGridViewComboBoxCell);
					dataGridViewRow.DefaultCellStyle.BackColor = Color.White;
					this.dgViewPartListSettings.Rows.Add(dataGridViewRow);
				}
				else if ((int)strArrays3.Length == 8)
				{
					DataGridViewRow white = new DataGridViewRow();
					DataGridViewCheckBoxCell dataGridViewCheckBoxCell2 = new DataGridViewCheckBoxCell()
					{
						Value = strArrays3[4]
					};
					white.Cells.Add(dataGridViewCheckBoxCell2);
					DataGridViewCheckBoxCell dataGridViewCheckBoxCell3 = new DataGridViewCheckBoxCell()
					{
						Value = strArrays3[5]
					};
					white.Cells.Add(dataGridViewCheckBoxCell3);
					DataGridViewTextBoxCell dataGridViewTextBoxCell2 = new DataGridViewTextBoxCell()
					{
						Value = sKey
					};
					white.Cells.Add(dataGridViewTextBoxCell2);
					DataGridViewTextBoxCell dataGridViewTextBoxCell3 = new DataGridViewTextBoxCell()
					{
						Value = strArrays3[0]
					};
					white.Cells.Add(dataGridViewTextBoxCell3);
					DataGridViewTextBoxCell dataGridViewTextBoxCell4 = new DataGridViewTextBoxCell()
					{
						Value = strArrays3[7]
					};
					white.Cells.Add(dataGridViewTextBoxCell4);
					DataGridViewComboBoxCell dataGridViewComboBoxCell1 = new DataGridViewComboBoxCell();
					string[] strArrays5 = "L,R,C".ToString().Split(new char[] { ',' });
					dataGridViewComboBoxCell1.Items.AddRange(strArrays5);
					string str2 = strArrays3[6].ToString();
					string str3 = str2;
					if (str2 != null)
					{
						if (str3 == "L")
						{
							dataGridViewComboBoxCell1.Value = strArrays5[0];
							goto Label0;
						}
						else if (str3 == "R")
						{
							dataGridViewComboBoxCell1.Value = strArrays5[1];
							goto Label0;
						}
						else
						{
							if (str3 != "C")
							{
								goto Label4;
							}
							dataGridViewComboBoxCell1.Value = strArrays5[2];
							goto Label0;
						}
					}
				Label4:
					dataGridViewComboBoxCell1.Value = strArrays5[0];
				Label0:
					white.Cells.Add(dataGridViewComboBoxCell1);
					white.DefaultCellStyle.BackColor = Color.White;
					this.dgViewPartListSettings.Rows.Add(white);
				}
			}
			catch (Exception exception)
			{
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.InitializePartsListGrid();
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

		private void frmConfigPartList_Load(object sender, EventArgs e)
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
				str = string.Concat(str, "/Screen[@Name='PARTSLIST_SETTINGS']");
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
			this.dgViewPartListSettings = new DataGridView();
			this.lblPartListSettings = new Label();
			this.PnlControl = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.bgWorker = new BackgroundWorker();
			this.pnlFrm.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			((ISupportInitialize)this.dgViewPartListSettings).BeginInit();
			this.PnlControl.SuspendLayout();
			base.SuspendLayout();
			this.pnlFrm.BackColor = Color.White;
			this.pnlFrm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlFrm.Controls.Add(this.picLoading);
			this.pnlFrm.Controls.Add(this.dgViewPartListSettings);
			this.pnlFrm.Controls.Add(this.lblPartListSettings);
			this.pnlFrm.Controls.Add(this.PnlControl);
			this.pnlFrm.Dock = DockStyle.Fill;
			this.pnlFrm.Location = new Point(0, 0);
			this.pnlFrm.Name = "pnlFrm";
			this.pnlFrm.Size = new System.Drawing.Size(450, 450);
			this.pnlFrm.TabIndex = 0;
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(2, 2);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.TabIndex = 3;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.dgViewPartListSettings.AllowDrop = true;
			this.dgViewPartListSettings.AllowUserToAddRows = false;
			this.dgViewPartListSettings.AllowUserToDeleteRows = false;
			this.dgViewPartListSettings.AllowUserToResizeRows = false;
			dataGridViewCellStyle.BackColor = Color.White;
			this.dgViewPartListSettings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle;
			this.dgViewPartListSettings.BackgroundColor = Color.White;
			this.dgViewPartListSettings.BorderStyle = BorderStyle.Fixed3D;
			control.Alignment = DataGridViewContentAlignment.MiddleLeft;
			control.BackColor = SystemColors.Control;
			control.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			control.ForeColor = Color.Black;
			control.SelectionBackColor = SystemColors.Highlight;
			control.SelectionForeColor = SystemColors.HighlightText;
			control.WrapMode = DataGridViewTriState.True;
			this.dgViewPartListSettings.ColumnHeadersDefaultCellStyle = control;
			this.dgViewPartListSettings.ColumnHeadersHeight = 26;
			this.dgViewPartListSettings.Dock = DockStyle.Fill;
			this.dgViewPartListSettings.Location = new Point(0, 27);
			this.dgViewPartListSettings.Name = "dgViewPartListSettings";
			font.Alignment = DataGridViewContentAlignment.MiddleLeft;
			font.BackColor = SystemColors.Control;
			font.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			font.ForeColor = Color.Black;
			font.SelectionBackColor = SystemColors.Highlight;
			font.SelectionForeColor = SystemColors.HighlightText;
			font.WrapMode = DataGridViewTriState.True;
			this.dgViewPartListSettings.RowHeadersDefaultCellStyle = font;
			this.dgViewPartListSettings.RowHeadersWidth = 32;
			this.dgViewPartListSettings.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			white.BackColor = Color.White;
			white.ForeColor = Color.Black;
			white.SelectionBackColor = Color.SteelBlue;
			white.SelectionForeColor = Color.White;
			this.dgViewPartListSettings.RowsDefaultCellStyle = white;
			this.dgViewPartListSettings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgViewPartListSettings.ShowRowErrors = false;
			this.dgViewPartListSettings.Size = new System.Drawing.Size(448, 390);
			this.dgViewPartListSettings.TabIndex = 2;
			this.lblPartListSettings.BackColor = Color.White;
			this.lblPartListSettings.Dock = DockStyle.Top;
			this.lblPartListSettings.ForeColor = Color.Black;
			this.lblPartListSettings.Location = new Point(0, 0);
			this.lblPartListSettings.Name = "lblPartListSettings";
			this.lblPartListSettings.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblPartListSettings.Size = new System.Drawing.Size(448, 27);
			this.lblPartListSettings.TabIndex = 1;
			this.lblPartListSettings.Text = "List Settings";
			this.PnlControl.Controls.Add(this.btnOK);
			this.PnlControl.Controls.Add(this.btnCancel);
			this.PnlControl.Dock = DockStyle.Bottom;
			this.PnlControl.Location = new Point(0, 417);
			this.PnlControl.Name = "PnlControl";
			this.PnlControl.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
			this.PnlControl.Size = new System.Drawing.Size(448, 31);
			this.PnlControl.TabIndex = 0;
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
			base.Name = "frmConfigPartList";
			this.Text = "Parts List Configurations";
			base.Load += new EventHandler(this.frmConfigPartList_Load);
			this.pnlFrm.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			((ISupportInitialize)this.dgViewPartListSettings).EndInit();
			this.PnlControl.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void InitializePartsListGrid()
		{
			if (this.dgViewPartListSettings.InvokeRequired)
			{
				this.dgViewPartListSettings.Invoke(new frmConfigPartList.InitializePartsListGridDelegate(this.InitializePartsListGrid));
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
				this.dgViewPartListSettings.Columns.Add(dataGridViewCheckBoxColumn);
				DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn()
				{
					Name = "EnablePrint",
					HeaderText = "Prt",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 50
				};
				this.dgViewPartListSettings.Columns.Add(dataGridViewCheckBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn()
				{
					Name = "FieldId",
					HeaderText = "Field Id",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100,
					ReadOnly = true
				};
				this.dgViewPartListSettings.Columns.Add(dataGridViewTextBoxColumn);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn()
				{
					Name = "DisplayName",
					HeaderText = "Display Name",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100,
					ReadOnly = true
				};
				this.dgViewPartListSettings.Columns.Add(dataGridViewTextBoxColumn1);
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn()
				{
					Name = "Width",
					HeaderText = "Print Width",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewPartListSettings.Columns.Add(dataGridViewTextBoxColumn2);
				DataGridViewComboBoxColumn dataGridViewComboBoxColumn = new DataGridViewComboBoxColumn()
				{
					Name = "Alignment",
					HeaderText = "Print Position",
					SortMode = DataGridViewColumnSortMode.NotSortable,
					Width = 100
				};
				this.dgViewPartListSettings.Columns.Add(dataGridViewComboBoxColumn);
			}
			catch
			{
			}
		}

		private void LoadDataGridView()
		{
			if (this.dgViewPartListSettings.InvokeRequired)
			{
				this.dgViewPartListSettings.Invoke(new frmConfigPartList.LoadDataGridViewXmlDelegate(this.LoadDataGridView));
				return;
			}
			this.dgViewPartListSettings.Rows.Clear();
			ArrayList arrayLists = new ArrayList();
			List<string> strs = new List<string>();
			IniFileIO iniFileIO = new IniFileIO();
			arrayLists = iniFileIO.GetKeys(string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"), "PLIST_SETTINGS");
			for (int i = 0; i < arrayLists.Count; i++)
			{
				try
				{
					IniFileIO iniFileIO1 = new IniFileIO();
					string keyValue = iniFileIO1.GetKeyValue("PLIST_SETTINGS", arrayLists[i].ToString(), string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.serverId].sIniKey, ".ini"));
					this.AddPartListSettingsRow(keyValue, arrayLists[i].ToString());
					string str = arrayLists[i].ToString();
					string[] strArrays = keyValue.Split(new char[] { '|' });
					if ((int)strArrays.Length > 0)
					{
						string str1 = string.Concat(strArrays[1], ",", strArrays[2]);
						this.dicPLSettings.Add(str, str1);
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
				IEnumerator enumerator = ((IEnumerable)this.dgViewPartListSettings.Rows).GetEnumerator();
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
					if (this.dgViewPartListSettings.Rows.Count > 0 && Settings.Default.appLanguage.ToUpper() != "ENGLISH")
					{
						for (int i = 0; i < this.dgViewPartListSettings.Rows.Count; i++)
						{
							Global.SaveToLanguageIni("PLIST_SETTINGS", this.dgViewPartListSettings[2, i].Value.ToString(), this.dgViewPartListSettings[3, i].Value.ToString(), this.serverId);
						}
					}
					for (int j = 0; j < this.dgViewPartListSettings.Rows.Count; j++)
					{
						string str = string.Empty;
						string str1 = this.dgViewPartListSettings[0, j].Value.ToString();
						string str2 = this.dgViewPartListSettings[1, j].Value.ToString();
						string str3 = this.dgViewPartListSettings[2, j].Value.ToString();
						string engHeaderVal = Global.GetEngHeaderVal("PLIST_SETTINGS", this.dgViewPartListSettings[2, j].Value.ToString(), this.serverId);
						string[] strArrays = engHeaderVal.Split(new char[] { '|' });
						engHeaderVal = strArrays[0];
						string str4 = this.dgViewPartListSettings[4, j].Value.ToString();
						string str5 = this.dgViewPartListSettings[5, j].Value.ToString();
						string str6 = this.dicPLSettings[str3].ToString();
						char[] chrArray = new char[] { ',' };
						string str7 = str6.Split(chrArray)[0].ToString();
						char[] chrArray1 = new char[] { ',' };
						string str8 = str6.Split(chrArray1)[1].ToString();
						if ((int)strArrays.Length == 4 || (int)strArrays.Length == 8)
						{
							string[] strArrays1 = new string[] { engHeaderVal, "|", str7, "|", str8, "|", strArrays[3], "|", str1, "|", str2, "|", str5, "|", str4 };
							str = string.Concat(strArrays1);
						}
						else
						{
							string[] strArrays2 = new string[] { engHeaderVal, "|", str7, "|", str8, "|", str1, "|", str2, "|", str5, "|", str4 };
							str = string.Concat(strArrays2);
						}
						string[] strArrays3 = new string[] { engHeaderVal, "|", str7, "|", str8, "|MEM|", str1, "|", str2, "|", str5, "|", str4 };
						string str9 = string.Concat(strArrays3);
						if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
						{
							this.dgViewPartListSettings[3, j].Value.ToString();
						}
						if (str3.ToUpper() != "MEM")
						{
							Global.SaveToServerIni("PLIST_SETTINGS", str3, str, this.serverId);
						}
						else
						{
							Global.SaveToServerIni("PLIST_SETTINGS", str3, str9, this.serverId);
						}
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
			this.Text = this.GetResource("Parts List Configurations", "PARTS_LIST_CONFIG", ResourceType.LABEL);
			this.lblPartListSettings.Text = this.GetResource("List Settings", "LIST_SETTINGS", ResourceType.LABEL);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
			try
			{
				this.dgViewPartListSettings.Columns["EnableDisplay"].HeaderText = this.GetResource("Enable Display", "ENABLE_DISPALY", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewPartListSettings.Columns["EnablePrint"].HeaderText = this.GetResource("Enable Print", "ENABLE_PRINT", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewPartListSettings.Columns["FieldId"].HeaderText = this.GetResource("Field Id", "FIELD_ID", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewPartListSettings.Columns["DisplayName"].HeaderText = this.GetResource("Display Name", "DISPLAY_NAME", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewPartListSettings.Columns["Width"].HeaderText = this.GetResource("Col Width", "COL_WIDTH", ResourceType.GRID_VIEW);
			}
			catch
			{
			}
			try
			{
				this.dgViewPartListSettings.Columns["Alignment"].HeaderText = this.GetResource("Col Alignment", "COL_ALIGNMENT", ResourceType.GRID_VIEW);
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
					if (control == this.picLoading || control == this.lblPartListSettings)
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
			this.lblPartListSettings.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.dgViewPartListSettings.Font = Settings.Default.appFont;
			this.dgViewPartListSettings.RowsDefaultCellStyle.SelectionBackColor = Settings.Default.appHighlightBackColor;
			this.dgViewPartListSettings.RowsDefaultCellStyle.SelectionForeColor = Settings.Default.appHighlightForeColor;
		}

		private delegate void InitializePartsListGridDelegate();

		private delegate void LoadDataGridViewXmlDelegate();
	}
}
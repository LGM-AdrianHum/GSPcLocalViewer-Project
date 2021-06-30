using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmSelectionListQuantity : Form
	{
		private frmViewer frmParent;

		private string strPartNumber = string.Empty;

		private IContainer components;

		private Panel pnlForm;

		private Label lblQuantity;

		private Label lblPartnumberVal;

		private Label lblPartNumber;

		private Button btnChangeQuantity;

		private Button btnCancel;

		private Button btnDelete;

		private NumericTextBox txtQuantity;

		public frmSelectionListQuantity(frmViewer frm, string sPartNumber, string sQuantity)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			string[] strArrays = sPartNumber.Split(new char[] { '\u005E' });
			this.lblPartnumberVal.Text = strArrays[(int)strArrays.Length - 1];
			this.strPartNumber = sPartNumber;
			this.txtQuantity.Text = sQuantity;
			this.UpdateFont();
			this.LoadResources();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnChangeQuantity_Click(object sender, EventArgs e)
		{
			this.frmParent.ChangeSelListQuantity(this.strPartNumber, this.txtQuantity.Text);
			base.Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			this.frmParent.DeleteSelListRow(this.strPartNumber);
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private string GetDisplayString(string sField)
		{
			string empty;
			try
			{
				string str = string.Concat(Settings.Default.appLanguage, "_GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini");
				if (Settings.Default.appLanguage.ToUpper() != "ENGLISH")
				{
					string keyValue = string.Empty;
					IniFileIO iniFileIO = new IniFileIO();
					keyValue = iniFileIO.GetKeyValue("SLIST_SETTINGS", sField, string.Concat(Application.StartupPath, "\\Language XMLs\\", str));
					empty = (keyValue == null || !(keyValue != string.Empty) ? string.Empty : keyValue);
				}
				else
				{
					string item = string.Empty;
					item = Program.iniServers[this.frmParent.p_ServerId].items["SLIST_SETTINGS", sField];
					if (item == null || !(item != string.Empty))
					{
						empty = string.Empty;
					}
					else
					{
						string[] strArrays = item.Split(new char[] { '|' });
						empty = (strArrays[0] == null || !(strArrays[0] != string.Empty) ? string.Empty : strArrays[0]);
					}
				}
			}
			catch
			{
				empty = string.Empty;
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
				str = string.Concat(str, "/Screen[@Name='SELECTION_LIST_QUANTITY']");
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
			this.txtQuantity = new NumericTextBox();
			this.btnCancel = new Button();
			this.btnDelete = new Button();
			this.btnChangeQuantity = new Button();
			this.lblQuantity = new Label();
			this.lblPartnumberVal = new Label();
			this.lblPartNumber = new Label();
			this.pnlForm.SuspendLayout();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.Controls.Add(this.txtQuantity);
			this.pnlForm.Controls.Add(this.btnCancel);
			this.pnlForm.Controls.Add(this.btnDelete);
			this.pnlForm.Controls.Add(this.btnChangeQuantity);
			this.pnlForm.Controls.Add(this.lblQuantity);
			this.pnlForm.Controls.Add(this.lblPartnumberVal);
			this.pnlForm.Controls.Add(this.lblPartNumber);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(287, 120);
			this.pnlForm.TabIndex = 0;
			this.txtQuantity.AllowSpace = false;
			this.txtQuantity.BorderStyle = BorderStyle.FixedSingle;
			this.txtQuantity.Location = new Point(110, 51);
			this.txtQuantity.MaxLength = 5;
			this.txtQuantity.Name = "txtQuantity";
			this.txtQuantity.Size = new System.Drawing.Size(75, 21);
			this.txtQuantity.TabIndex = 0;
			this.txtQuantity.TextChanged += new EventHandler(this.txtQuantity_TextChanged);
			this.txtQuantity.KeyDown += new KeyEventHandler(this.txtQuantity_KeyDown);
			this.btnCancel.Location = new Point(200, 86);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnDelete.Location = new Point(110, 86);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.Text = "Delete";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.btnChangeQuantity.Location = new Point(15, 86);
			this.btnChangeQuantity.Name = "btnChangeQuantity";
			this.btnChangeQuantity.Size = new System.Drawing.Size(75, 23);
			this.btnChangeQuantity.TabIndex = 6;
			this.btnChangeQuantity.Text = "Change Quantity";
			this.btnChangeQuantity.UseVisualStyleBackColor = true;
			this.btnChangeQuantity.Click += new EventHandler(this.btnChangeQuantity_Click);
			this.lblQuantity.AutoSize = true;
			this.lblQuantity.Location = new Point(15, 53);
			this.lblQuantity.Name = "lblQuantity";
			this.lblQuantity.Size = new System.Drawing.Size(49, 13);
			this.lblQuantity.TabIndex = 4;
			this.lblQuantity.Text = "Quantity";
			this.lblPartnumberVal.AutoSize = true;
			this.lblPartnumberVal.Location = new Point(107, 20);
			this.lblPartnumberVal.Name = "lblPartnumberVal";
			this.lblPartnumberVal.Size = new System.Drawing.Size(67, 13);
			this.lblPartnumberVal.TabIndex = 1;
			this.lblPartnumberVal.Text = "Part Number";
			this.lblPartNumber.AutoSize = true;
			this.lblPartNumber.Location = new Point(15, 20);
			this.lblPartNumber.Name = "lblPartNumber";
			this.lblPartNumber.Size = new System.Drawing.Size(67, 13);
			this.lblPartNumber.TabIndex = 0;
			this.lblPartNumber.Text = "Part Number";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(287, 120);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSelectionListQuantity";
			base.ShowIcon = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Change Quantity";
			this.pnlForm.ResumeLayout(false);
			this.pnlForm.PerformLayout();
			base.ResumeLayout(false);
		}

		public void LoadResources()
		{
			try
			{
				string empty = string.Empty;
				empty = this.GetDisplayString("QTY");
				this.lblQuantity.Text = (empty != string.Empty ? empty : this.GetResource("Quantity", "QUANTITY", ResourceType.LABEL));
				empty = this.GetDisplayString("PARTNUMBER");
				this.lblPartNumber.Text = (empty != string.Empty ? empty : this.GetResource("Part Number", "PART_NUMBER", ResourceType.LABEL));
				this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
				this.btnChangeQuantity.Text = this.GetResource("Change", "CHANGE", ResourceType.BUTTON);
				this.btnDelete.Text = this.GetResource("Delete", "DELETE", ResourceType.BUTTON);
				this.Text = this.GetResource("Change Quantity", "SELECTION_LIST_QUANTITY", ResourceType.TITLE);
			}
			catch
			{
			}
		}

		private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.frmParent.ChangeSelListQuantity(this.strPartNumber, this.txtQuantity.Text);
				base.Close();
			}
		}

		private void txtQuantity_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.txtQuantity.Text.Trim() == string.Empty || this.txtQuantity.Text.Trim() == "0")
				{
					this.txtQuantity.Text = "1";
				}
			}
			catch
			{
			}
		}

		public void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}
	}
}
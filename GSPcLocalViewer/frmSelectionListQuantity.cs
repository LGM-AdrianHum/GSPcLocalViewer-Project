// Decompiled with JetBrains decompiler
// Type: GSPcLocalViewer.frmSelectionListQuantity
// Assembly: GSPcLocalViewer, Version=3.3.0.66, Culture=neutral, PublicKeyToken=null
// MVID: 80A25D81-DD75-42F1-ABA1-9B7DE5145AE0
// Assembly location: C:\Program Files (x86)\KLTD\GSPcLocal\GSPcLocalViewer.exe

using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
  public class frmSelectionListQuantity : Form
  {
    private string strPartNumber = string.Empty;
    private frmViewer frmParent;
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
      string[] strArray = sPartNumber.Split('^');
      this.lblPartnumberVal.Text = strArray[strArray.Length - 1];
      this.strPartNumber = sPartNumber;
      this.txtQuantity.Text = sQuantity;
      this.UpdateFont();
      this.LoadResources();
    }

    private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.frmParent.ChangeSelListQuantity(this.strPartNumber, this.txtQuantity.Text);
      this.Close();
    }

    private void txtQuantity_TextChanged(object sender, EventArgs e)
    {
      try
      {
        if (!(this.txtQuantity.Text.Trim() == string.Empty) && !(this.txtQuantity.Text.Trim() == "0"))
          return;
        this.txtQuantity.Text = "1";
      }
      catch
      {
      }
    }

    private void btnChangeQuantity_Click(object sender, EventArgs e)
    {
      this.frmParent.ChangeSelListQuantity(this.strPartNumber, this.txtQuantity.Text);
      this.Close();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      this.frmParent.DeleteSelListRow(this.strPartNumber);
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    public void UpdateFont()
    {
      this.Font = Settings.Default.appFont;
    }

    public void LoadResources()
    {
      try
      {
        string empty = string.Empty;
        string displayString1 = this.GetDisplayString("QTY");
        this.lblQuantity.Text = displayString1 != string.Empty ? displayString1 : this.GetResource("Quantity", "QUANTITY", ResourceType.LABEL);
        string displayString2 = this.GetDisplayString("PARTNUMBER");
        this.lblPartNumber.Text = displayString2 != string.Empty ? displayString2 : this.GetResource("Part Number", "PART_NUMBER", ResourceType.LABEL);
        this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
        this.btnChangeQuantity.Text = this.GetResource("Change", "CHANGE", ResourceType.BUTTON);
        this.btnDelete.Text = this.GetResource("Delete", "DELETE", ResourceType.BUTTON);
        this.Text = this.GetResource("Change Quantity", "SELECTION_LIST_QUANTITY", ResourceType.TITLE);
      }
      catch
      {
      }
    }

    private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
    {
      try
      {
        string str = "" + "/Screen[@Name='MAIN_FORM']" + "/Screen[@Name='SELECTION_LIST_QUANTITY']";
        switch (rType)
        {
          case ResourceType.BUTTON:
            str += "/Resources[@Name='BUTTON']";
            break;
          case ResourceType.LABEL:
            str += "/Resources[@Name='LABEL']";
            break;
          case ResourceType.CHECK_BOX:
            str += "/Resources[@Name='CHECK_BOX']";
            break;
          case ResourceType.RADIO_BUTTON:
            str += "/Resources[@Name='RADIO_BUTTON']";
            break;
          case ResourceType.TITLE:
            string xQuery1 = str + "[@Name='" + sKey + "']";
            return this.frmParent.GetResourceValue(sDefaultValue, xQuery1);
          case ResourceType.COMBO_BOX:
            str += "/Resources[@Name='COMBO_BOX']";
            break;
          case ResourceType.LIST_VIEW:
            str += "/Resources[@Name='LIST_VIEW']";
            break;
          case ResourceType.GRID_VIEW:
            str += "/Resources[@Name='GRID_VIEW']";
            break;
          case ResourceType.TOOLSTRIP:
            str += "/Resources[@Name='TOOLSTRIP']";
            break;
          case ResourceType.MENU_BAR:
            str += "/Resources[@Name='MENU_BAR']";
            break;
          case ResourceType.CONTEXT_MENU:
            str += "/Resources[@Name='CONTEXT_MENU']";
            break;
          case ResourceType.STATUS_MESSAGE:
            str += "/Resources[@Name='STATUS_MESSAGE']";
            break;
          case ResourceType.POPUP_MESSAGE:
            str += "/Resources[@Name='POPUP_MESSAGE']";
            break;
        }
        string xQuery2 = str + "/Resource[@Name='" + sKey + "']";
        return this.frmParent.GetResourceValue(sDefaultValue, xQuery2);
      }
      catch (Exception ex)
      {
        return sDefaultValue;
      }
    }

    private string GetDisplayString(string sField)
    {
      try
      {
        string str1 = Settings.Default.appLanguage + "_GSP_" + Program.iniServers[this.frmParent.ServerId].sIniKey + ".ini";
        if (Settings.Default.appLanguage.ToUpper() == "ENGLISH")
        {
          string empty = string.Empty;
          string str2 = Program.iniServers[this.frmParent.p_ServerId].items["SLIST_SETTINGS", sField];
          if (str2 == null || !(str2 != string.Empty))
            return string.Empty;
          string[] strArray = str2.Split('|');
          if (strArray[0] != null && strArray[0] != string.Empty)
            return strArray[0];
          return string.Empty;
        }
        string empty1 = string.Empty;
        string keyValue = new IniFileIO().GetKeyValue("SLIST_SETTINGS", sField, Application.StartupPath + "\\Language XMLs\\" + str1);
        if (keyValue != null && keyValue != string.Empty)
          return keyValue;
        return string.Empty;
      }
      catch
      {
        return string.Empty;
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
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
      this.SuspendLayout();
      this.pnlForm.BackColor = Color.White;
      this.pnlForm.Controls.Add((Control) this.txtQuantity);
      this.pnlForm.Controls.Add((Control) this.btnCancel);
      this.pnlForm.Controls.Add((Control) this.btnDelete);
      this.pnlForm.Controls.Add((Control) this.btnChangeQuantity);
      this.pnlForm.Controls.Add((Control) this.lblQuantity);
      this.pnlForm.Controls.Add((Control) this.lblPartnumberVal);
      this.pnlForm.Controls.Add((Control) this.lblPartNumber);
      this.pnlForm.Dock = DockStyle.Fill;
      this.pnlForm.Location = new Point(0, 0);
      this.pnlForm.Name = "pnlForm";
      this.pnlForm.Size = new Size(287, 120);
      this.pnlForm.TabIndex = 0;
      this.txtQuantity.AllowSpace = false;
      this.txtQuantity.BorderStyle = BorderStyle.FixedSingle;
      this.txtQuantity.Location = new Point(110, 51);
      this.txtQuantity.MaxLength = 5;
      this.txtQuantity.Name = "txtQuantity";
      this.txtQuantity.Size = new Size(75, 21);
      this.txtQuantity.TabIndex = 0;
      this.txtQuantity.TextChanged += new EventHandler(this.txtQuantity_TextChanged);
      this.txtQuantity.KeyDown += new KeyEventHandler(this.txtQuantity_KeyDown);
      this.btnCancel.Location = new Point(200, 86);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new Size(75, 23);
      this.btnCancel.TabIndex = 8;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
      this.btnDelete.Location = new Point(110, 86);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new Size(75, 23);
      this.btnDelete.TabIndex = 7;
      this.btnDelete.Text = "Delete";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
      this.btnChangeQuantity.Location = new Point(15, 86);
      this.btnChangeQuantity.Name = "btnChangeQuantity";
      this.btnChangeQuantity.Size = new Size(75, 23);
      this.btnChangeQuantity.TabIndex = 6;
      this.btnChangeQuantity.Text = "Change Quantity";
      this.btnChangeQuantity.UseVisualStyleBackColor = true;
      this.btnChangeQuantity.Click += new EventHandler(this.btnChangeQuantity_Click);
      this.lblQuantity.AutoSize = true;
      this.lblQuantity.Location = new Point(15, 53);
      this.lblQuantity.Name = "lblQuantity";
      this.lblQuantity.Size = new Size(49, 13);
      this.lblQuantity.TabIndex = 4;
      this.lblQuantity.Text = "Quantity";
      this.lblPartnumberVal.AutoSize = true;
      this.lblPartnumberVal.Location = new Point(107, 20);
      this.lblPartnumberVal.Name = "lblPartnumberVal";
      this.lblPartnumberVal.Size = new Size(67, 13);
      this.lblPartnumberVal.TabIndex = 1;
      this.lblPartnumberVal.Text = "Part Number";
      this.lblPartNumber.AutoSize = true;
      this.lblPartNumber.Location = new Point(15, 20);
      this.lblPartNumber.Name = "lblPartNumber";
      this.lblPartNumber.Size = new Size(67, 13);
      this.lblPartNumber.TabIndex = 0;
      this.lblPartNumber.Text = "Part Number";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(287, 120);
      this.Controls.Add((Control) this.pnlForm);
      this.Font = new Font("Tahoma", 8.25f);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmSelectionListQuantity);
      this.ShowIcon = false;
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Change Quantity";
      this.pnlForm.ResumeLayout(false);
      this.pnlForm.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}

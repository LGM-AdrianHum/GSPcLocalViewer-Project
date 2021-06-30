using GSPcLocalViewer;
using GSPcLocalViewer.frmPrint;
using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmOthers
{
	public class frmPreviewProcessing : Form
	{
		private IContainer components;

		private Label lblPreviewProcessMsg;

		private PictureBox picPreviewLoading;

		private GSPcLocalViewer.frmPrint.frmPrint objParent;

		public frmPreviewProcessing(GSPcLocalViewer.frmPrint.frmPrint frmPrintDlg)
		{
			this.InitializeComponent();
			this.objParent = frmPrintDlg;
			this.SetControlsText();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmPreviewProcessing_Load(object sender, EventArgs e)
		{
			base.CenterToParent();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='PREVIEW_PROCESSING']");
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
					resourceValue = this.objParent.frmParent.GetResourceValue(sDefaultValue, str);
				}
				else
				{
					str = string.Concat(str, "[@Name='", sKey, "']");
					resourceValue = this.objParent.frmParent.GetResourceValue(sDefaultValue, str);
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
			this.lblPreviewProcessMsg = new Label();
			this.picPreviewLoading = new PictureBox();
			((ISupportInitialize)this.picPreviewLoading).BeginInit();
			base.SuspendLayout();
			this.lblPreviewProcessMsg.AutoSize = true;
			this.lblPreviewProcessMsg.Location = new Point(59, 84);
			this.lblPreviewProcessMsg.Name = "lblPreviewProcessMsg";
			this.lblPreviewProcessMsg.Size = new System.Drawing.Size(137, 13);
			this.lblPreviewProcessMsg.TabIndex = 0;
			this.lblPreviewProcessMsg.Text = "It's processing. Please wait.";
			this.picPreviewLoading.Image = Resources.Loading1;
			this.picPreviewLoading.Location = new Point(108, 28);
			this.picPreviewLoading.Name = "picPreviewLoading";
			this.picPreviewLoading.Size = new System.Drawing.Size(32, 32);
			this.picPreviewLoading.TabIndex = 1;
			this.picPreviewLoading.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new System.Drawing.Size(249, 106);
			base.ControlBox = false;
			base.Controls.Add(this.picPreviewLoading);
			base.Controls.Add(this.lblPreviewProcessMsg);
			base.Name = "frmPreviewProcessing";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Preview";
			base.Load += new EventHandler(this.frmPreviewProcessing_Load);
			((ISupportInitialize)this.picPreviewLoading).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void SetControlsText()
		{
			this.Text = this.GetResource("Preview", "PREVIEW", ResourceType.LABEL);
			this.lblPreviewProcessMsg.Text = this.GetResource("It's processing. Please wait.", "PREVIEW_PROC_MSG", ResourceType.LABEL);
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}
	}
}
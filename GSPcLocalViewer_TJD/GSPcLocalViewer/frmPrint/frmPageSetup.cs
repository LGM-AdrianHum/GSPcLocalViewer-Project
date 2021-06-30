using AxDjVuCtrlLib;
using GSPcLocalViewer;
using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GSPcLocalViewer.frmPrint
{
	public class frmPageSetup : Form
	{
		private const string dllDjVuDecoder = "DjVuDecoder.dll";

		private PaperSize pageSize;

		private int iSplittingOption;

		private frmViewer frmParent;

		private IContainer components;

		private AxDjVuCtrl objDjVuCtl;

		private Panel pnlControl;

		private Button btnCancel;

		private Panel pnlSplitPattern;

		private Panel pnlLblSplitPattern;

		private Label lblSplitPatternLine;

		private Label lblSplitPattern;

		private Label lblPortraitPicture;

		public Panel panelLandscape;

		private Label lblLandscapePicture;

		public Panel panelPortrait;

		private Panel pnlForm;

		public frmPageSetup(frmViewer objFrmViewer, int SplittingOption)
		{
			this.frmParent = objFrmViewer;
			this.pageSize = new PaperSize()
			{
				PaperName = "A4",
				Width = 827,
				Height = 1169
			};
			this.iSplittingOption = SplittingOption;
			this.InitializeComponent();
			this.LoadResources();
			this.objDjVuCtl.CurrentLanguage = this.frmParent.AppCurrentLanguage;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
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

		[DllImport("DjVuDecoder.dll", CharSet=CharSet.None, ExactSpelling=false)]
		internal static extern bool DjVuToJPEG(string DjVuPath, string ExportedPath);

		private void frmPageSetup_Paint(object sender, PaintEventArgs e)
		{
			this.SetPageImageResolutions();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='PRINT']");
				str = string.Concat(str, "/Screen[@Name='PAGE_SPLITTING']");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmPageSetup));
			this.pnlControl = new Panel();
			this.btnCancel = new Button();
			this.pnlSplitPattern = new Panel();
			this.pnlLblSplitPattern = new Panel();
			this.lblSplitPatternLine = new Label();
			this.lblSplitPattern = new Label();
			this.lblPortraitPicture = new Label();
			this.panelLandscape = new Panel();
			this.objDjVuCtl = new AxDjVuCtrl();
			this.lblLandscapePicture = new Label();
			this.panelPortrait = new Panel();
			this.pnlForm = new Panel();
			this.pnlControl.SuspendLayout();
			this.pnlSplitPattern.SuspendLayout();
			this.pnlLblSplitPattern.SuspendLayout();
			this.panelLandscape.SuspendLayout();
			((ISupportInitialize)this.objDjVuCtl).BeginInit();
			this.pnlForm.SuspendLayout();
			base.SuspendLayout();
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 206);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(15, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(432, 31);
			this.pnlControl.TabIndex = 19;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(342, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Close";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlSplitPattern.Controls.Add(this.pnlLblSplitPattern);
			this.pnlSplitPattern.Controls.Add(this.lblPortraitPicture);
			this.pnlSplitPattern.Controls.Add(this.panelLandscape);
			this.pnlSplitPattern.Controls.Add(this.lblLandscapePicture);
			this.pnlSplitPattern.Controls.Add(this.panelPortrait);
			this.pnlSplitPattern.Dock = DockStyle.Fill;
			this.pnlSplitPattern.Location = new Point(0, 0);
			this.pnlSplitPattern.Name = "pnlSplitPattern";
			this.pnlSplitPattern.Size = new System.Drawing.Size(432, 206);
			this.pnlSplitPattern.TabIndex = 2;
			this.pnlLblSplitPattern.Controls.Add(this.lblSplitPatternLine);
			this.pnlLblSplitPattern.Controls.Add(this.lblSplitPattern);
			this.pnlLblSplitPattern.Dock = DockStyle.Top;
			this.pnlLblSplitPattern.Location = new Point(0, 0);
			this.pnlLblSplitPattern.Name = "pnlLblSplitPattern";
			this.pnlLblSplitPattern.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblSplitPattern.Size = new System.Drawing.Size(432, 28);
			this.pnlLblSplitPattern.TabIndex = 20;
			this.lblSplitPatternLine.BackColor = Color.Transparent;
			this.lblSplitPatternLine.Dock = DockStyle.Fill;
			this.lblSplitPatternLine.ForeColor = Color.Blue;
			this.lblSplitPatternLine.Image = Resources.GroupLine0;
			this.lblSplitPatternLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblSplitPatternLine.Location = new Point(82, 0);
			this.lblSplitPatternLine.Name = "lblSplitPatternLine";
			this.lblSplitPatternLine.Size = new System.Drawing.Size(335, 28);
			this.lblSplitPatternLine.TabIndex = 15;
			this.lblSplitPatternLine.Tag = "";
			this.lblSplitPatternLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblSplitPattern.BackColor = Color.Transparent;
			this.lblSplitPattern.Dock = DockStyle.Left;
			this.lblSplitPattern.ForeColor = Color.Blue;
			this.lblSplitPattern.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblSplitPattern.Location = new Point(7, 0);
			this.lblSplitPattern.Name = "lblSplitPattern";
			this.lblSplitPattern.Size = new System.Drawing.Size(75, 28);
			this.lblSplitPattern.TabIndex = 12;
			this.lblSplitPattern.Tag = "";
			this.lblSplitPattern.Text = "Preview";
			this.lblSplitPattern.TextAlign = ContentAlignment.MiddleLeft;
			this.lblPortraitPicture.AutoSize = true;
			this.lblPortraitPicture.Location = new Point(73, 40);
			this.lblPortraitPicture.Name = "lblPortraitPicture";
			this.lblPortraitPicture.Size = new System.Drawing.Size(98, 13);
			this.lblPortraitPicture.TabIndex = 0;
			this.lblPortraitPicture.Text = "For Portrait Picture";
			this.panelLandscape.BackColor = Color.White;
			this.panelLandscape.Controls.Add(this.objDjVuCtl);
			this.panelLandscape.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.panelLandscape.Location = new Point(268, 71);
			this.panelLandscape.Name = "panelLandscape";
			this.panelLandscape.Size = new System.Drawing.Size(114, 114);
			this.panelLandscape.TabIndex = 6;
			this.objDjVuCtl.Enabled = true;
			this.objDjVuCtl.Location = new Point(0, 203);
			this.objDjVuCtl.Name = "objDjVuCtl";
			this.objDjVuCtl.OcxState = (AxHost.State)componentResourceManager.GetObject("objDjVuCtl.OcxState");
			this.objDjVuCtl.Size = new System.Drawing.Size(279, 10);
			this.objDjVuCtl.TabIndex = 28;
			this.lblLandscapePicture.AutoSize = true;
			this.lblLandscapePicture.CausesValidation = false;
			this.lblLandscapePicture.Location = new Point(268, 40);
			this.lblLandscapePicture.Name = "lblLandscapePicture";
			this.lblLandscapePicture.Size = new System.Drawing.Size(113, 13);
			this.lblLandscapePicture.TabIndex = 1;
			this.lblLandscapePicture.Text = "For Landscape Picture";
			this.panelPortrait.BackColor = Color.White;
			this.panelPortrait.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.panelPortrait.Location = new Point(52, 71);
			this.panelPortrait.Name = "panelPortrait";
			this.panelPortrait.Size = new System.Drawing.Size(129, 114);
			this.panelPortrait.TabIndex = 5;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlSplitPattern);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(2, 2);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(434, 239);
			this.pnlForm.TabIndex = 25;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new System.Drawing.Size(438, 243);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmPageSetup";
			base.Padding = new System.Windows.Forms.Padding(2);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Split Printing";
			base.Paint += new PaintEventHandler(this.frmPageSetup_Paint);
			this.pnlControl.ResumeLayout(false);
			this.pnlSplitPattern.ResumeLayout(false);
			this.pnlSplitPattern.PerformLayout();
			this.pnlLblSplitPattern.ResumeLayout(false);
			this.panelLandscape.ResumeLayout(false);
			((ISupportInitialize)this.objDjVuCtl).EndInit();
			this.pnlForm.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Split Printing", "PAGE_SPLITTING", ResourceType.TITLE);
			this.lblSplitPattern.Text = this.GetResource("Preview", "PREVIEW", ResourceType.LABEL);
			this.lblPortraitPicture.Text = this.GetResource("For Potrait Picture", "POTRAIT_PICTURE", ResourceType.LABEL);
			this.lblLandscapePicture.Text = this.GetResource("For Landscape Picture", "LANDSCAPE_PICTURE", ResourceType.LABEL);
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
		}

		private void SetPageImageResolutions()
		{
			decimal num;
			int width;
			int num1 = 5;
			int num2 = 1;
			if (this.iSplittingOption == 0)
			{
				num2 = 1;
			}
			else if (this.iSplittingOption == 1)
			{
				num2 = 2;
			}
			else if (this.iSplittingOption == 2)
			{
				num2 = 4;
			}
			else if (this.iSplittingOption == 3)
			{
				num2 = 8;
			}
			decimal num3 = Math.Round(decimal.Divide(this.pageSize.Width, this.pageSize.Height), 2);
			SolidBrush solidBrush = new SolidBrush(Color.Black);
			Pen pen = new Pen(solidBrush);
			Graphics graphic = this.panelPortrait.CreateGraphics();
			Graphics graphic1 = this.panelLandscape.CreateGraphics();
			this.panelPortrait.Refresh();
			this.panelLandscape.Refresh();
			if (this.iSplittingOption == 0)
			{
				num = Math.Ceiling(Math.Round(num3 * this.panelPortrait.Height, 2));
				int num4 = int.Parse(num.ToString());
				width = (this.panelPortrait.Width - num4) / 2;
				int num5 = int.Parse(width.ToString());
				graphic.DrawRectangle(pen, num5, 0, num4, this.panelPortrait.Height - 1);
				ControlPaint.DrawBorder3D(graphic, num5, 0, num4, this.panelPortrait.Height - 1, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
				ControlPaint.DrawBorder3D(graphic, num5, 0, num4, this.panelPortrait.Height - 1, Border3DStyle.RaisedOuter, Border3DSide.Right);
				System.Drawing.Font font = new System.Drawing.Font(this.panelPortrait.Font.Name, 8f);
				int num6 = num4 / 2;
				float single = (float)(num5 + (int.Parse(num6.ToString()) - 3));
				int height = this.panelPortrait.Height / 2;
				graphic.DrawString("1", font, solidBrush, single, (float)(int.Parse(height.ToString()) - 5));
				decimal num7 = Math.Ceiling(Math.Round(num3 * this.panelLandscape.Height, 2));
				int num8 = int.Parse(num7.ToString());
				int height1 = (this.panelLandscape.Height - num8) / 2;
				num5 = int.Parse(height1.ToString());
				graphic1.DrawRectangle(pen, 0, num5, this.panelLandscape.Width - 1, num8);
				ControlPaint.DrawBorder3D(graphic1, 0, num5, this.panelLandscape.Width - 1, num8, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
				ControlPaint.DrawBorder3D(graphic1, 0, num5, this.panelLandscape.Width - 1, num8, Border3DStyle.RaisedOuter, Border3DSide.Right);
				System.Drawing.Font font1 = new System.Drawing.Font(this.panelLandscape.Font.Name, 8f);
				int width1 = this.panelLandscape.Width / 2;
				float single1 = (float)(int.Parse(width1.ToString()) - 6);
				int num9 = num8 / 2;
				graphic1.DrawString("1", font1, solidBrush, single1, (float)(num5 + (int.Parse(num9.ToString()) - 5)));
			}
			else if (this.iSplittingOption == 1)
			{
				decimal num10 = Math.Ceiling(Math.Round(num3 * this.panelPortrait.Height, 2));
				int num11 = int.Parse(num10.ToString());
				int width2 = (this.panelPortrait.Width - num11) / 2;
				int num12 = int.Parse(width2.ToString());
				int height2 = (this.panelPortrait.Height - num1) / 2;
				int num13 = int.Parse(height2.ToString());
				int num14 = 0;
				for (int i = 0; i < num2; i++)
				{
					graphic.DrawRectangle(pen, num12, num14, num11, num13);
					ControlPaint.DrawBorder3D(graphic, num12, num14, num11, num13, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(graphic, num12, num14, num11, num13, Border3DStyle.RaisedOuter, Border3DSide.Right);
					string str = (i + 1).ToString();
					System.Drawing.Font font2 = new System.Drawing.Font(this.panelPortrait.Font.Name, 8f);
					int num15 = num11 / 2;
					float single2 = (float)(num12 + (int.Parse(num15.ToString()) - 3));
					int num16 = num13 / 2;
					graphic.DrawString(str, font2, solidBrush, single2, (float)(int.Parse(num16.ToString()) + num14 - 3));
					num14 = num14 + num13 + num1;
				}
				decimal num17 = Math.Ceiling(Math.Round(num3 * this.panelLandscape.Height, 2));
				num13 = int.Parse(num17.ToString());
				int width3 = (this.panelLandscape.Width - num1) / 2;
				num11 = int.Parse(width3.ToString());
				int height3 = (this.panelLandscape.Height - num13) / 2;
				num12 = int.Parse(height3.ToString());
				num14 = 0;
				for (int j = 0; j < num2; j++)
				{
					graphic1.DrawRectangle(pen, num14, num12, num11, num13);
					ControlPaint.DrawBorder3D(graphic1, num14, num12, num11, num13, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(graphic1, num14, num12, num11, num13, Border3DStyle.RaisedOuter, Border3DSide.Right);
					string str1 = (j + 1).ToString();
					System.Drawing.Font font3 = new System.Drawing.Font(this.panelLandscape.Font.Name, 8f);
					int num18 = num11 / 2;
					float single3 = (float)(int.Parse(num18.ToString()) + num14 - 3);
					int num19 = num13 / 2;
					graphic1.DrawString(str1, font3, solidBrush, single3, (float)(num12 + (int.Parse(num19.ToString()) - 3)));
					num14 = num14 + num11 + num1;
				}
			}
			else if (this.iSplittingOption == 2)
			{
				int height4 = (this.panelPortrait.Height - num1) / 2;
				int num20 = int.Parse(height4.ToString());
				decimal num21 = Math.Ceiling(Math.Round(num3 * num20, 2));
				int num22 = int.Parse(num21.ToString());
				int width4 = (this.panelPortrait.Width - num22 * 2 - num1) / 2;
				int num23 = int.Parse(width4.ToString());
				int num24 = 0;
				for (int k = 0; k < num2; k++)
				{
					graphic.DrawRectangle(pen, num23, num24, num22, num20);
					ControlPaint.DrawBorder3D(graphic, num23, num24, num22, num20, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(graphic, num23, num24, num22, num20, Border3DStyle.RaisedOuter, Border3DSide.Right);
					string str2 = (k + 1).ToString();
					System.Drawing.Font font4 = new System.Drawing.Font(this.panelPortrait.Font.Name, 8f);
					int num25 = num22 / 2;
					float single4 = (float)(num23 + (int.Parse(num25.ToString()) - 3));
					int num26 = num20 / 2;
					graphic.DrawString(str2, font4, solidBrush, single4, (float)(int.Parse(num26.ToString()) + num24 - 3));
					num23 = num23 + num22 + num1;
					if (k % 2 == 1)
					{
						num24 = num24 + num20 + num1;
						int width5 = (this.panelPortrait.Width - num22 * 2 - num1) / 2;
						num23 = int.Parse(width5.ToString());
					}
				}
				int width6 = (this.panelLandscape.Width - num1) / 2;
				num22 = int.Parse(width6.ToString());
				decimal num27 = Math.Ceiling(Math.Round(num3 * num22, 2));
				num20 = int.Parse(num27.ToString());
				int height5 = (this.panelLandscape.Height - num20 * 2 - num1) / 2;
				num23 = int.Parse(height5.ToString());
				num24 = 0;
				for (int l = 0; l < num2; l++)
				{
					graphic1.DrawRectangle(pen, num24, num23, num22, num20);
					ControlPaint.DrawBorder3D(graphic1, num24, num23, num22, num20, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(graphic1, num24, num23, num22, num20, Border3DStyle.RaisedOuter, Border3DSide.Right);
					string str3 = (l + 1).ToString();
					System.Drawing.Font font5 = new System.Drawing.Font(this.panelLandscape.Font.Name, 8f);
					int num28 = num22 / 2;
					float single5 = (float)(int.Parse(num28.ToString()) + num24 - 3);
					int num29 = num20 / 2;
					graphic1.DrawString(str3, font5, solidBrush, single5, (float)(num23 + (int.Parse(num29.ToString()) - 3)));
					num24 = num24 + num22 + num1;
					if (l % 2 == 1)
					{
						num23 = num23 + num20 + num1;
						num24 = 0;
					}
				}
			}
			else if (this.iSplittingOption == 3)
			{
				num = Math.Ceiling(Math.Round(num3 * this.panelPortrait.Height, 2));
				int num30 = int.Parse(num.ToString());
				num30 = (num30 - num1) / 2;
				width = (this.panelPortrait.Width - num30 * 2 - num1) / 2;
				int num31 = int.Parse(width.ToString());
				width = (this.panelPortrait.Height - num1 * 3) / 4;
				int num32 = int.Parse(width.ToString());
				int num33 = 0;
				for (int m = 0; m < num2; m++)
				{
					graphic.DrawRectangle(pen, num31, num33, num30, num32);
					ControlPaint.DrawBorder3D(graphic, num31, num33, num30, num32, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(graphic, num31, num33, num30, num32, Border3DStyle.RaisedOuter, Border3DSide.Right);
					string str4 = (m + 1).ToString();
					System.Drawing.Font font6 = new System.Drawing.Font(this.panelPortrait.Font.Name, 8f);
					width = num30 / 2;
					float single6 = (float)(num31 + (int.Parse(width.ToString()) - 5));
					width = num32 / 2;
					graphic.DrawString(str4, font6, solidBrush, single6, (float)(int.Parse(width.ToString()) + num33 - 5));
					num31 = num31 + num30 + num1;
					if (m % 2 == 1)
					{
						num33 = num33 + num32 + num1;
						width = (this.panelPortrait.Width - num30 * 2 - num1) / 2;
						num31 = int.Parse(width.ToString());
					}
				}
				num = Math.Ceiling(Math.Round(num3 * this.panelLandscape.Width, 2));
				num32 = int.Parse(num.ToString());
				num32 = (num32 - num1) / 2;
				width = (this.panelLandscape.Height - num32 * 2 - num1) / 2;
				num31 = int.Parse(width.ToString());
				width = (this.panelLandscape.Width - num1 * 3) / 4;
				num30 = int.Parse(width.ToString());
				num33 = 0;
				for (int n = 0; n < num2; n++)
				{
					graphic1.DrawRectangle(pen, num33, num31, num30, num32);
					ControlPaint.DrawBorder3D(graphic1, num33, num31, num30, num32, Border3DStyle.RaisedOuter, Border3DSide.Bottom);
					ControlPaint.DrawBorder3D(graphic1, num33, num31, num30, num32, Border3DStyle.RaisedOuter, Border3DSide.Right);
					string str5 = (n + 1).ToString();
					System.Drawing.Font font7 = new System.Drawing.Font(this.panelLandscape.Font.Name, 8f);
					width = num30 / 2;
					float single7 = (float)(int.Parse(width.ToString()) + num33 - 5);
					width = num32 / 2;
					graphic1.DrawString(str5, font7, solidBrush, single7, (float)(num31 + (int.Parse(width.ToString()) - 5)));
					num33 = num33 + num30 + num1;
					if (n % 2 == 1 && n == num2 / 2 - 1)
					{
						num31 = num31 + num32 + num1;
						num33 = 0;
					}
				}
			}
			graphic.Dispose();
			graphic1.Dispose();
		}
	}
}
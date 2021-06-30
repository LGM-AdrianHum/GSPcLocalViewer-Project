using GSPcLocalViewer.frmPrint;
using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Resources;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	internal class CustomPrintPreviewDialog : Form
	{
		private IContainer components;

		private ToolStrip _toolStrip;

		private ToolStripButton _btnPrint;

		private ToolStripButton _btnPageSetup;

		private CustomPrintPreviewControl _preview;

		private ToolStripSplitButton _btnZoom;

		private ToolStripMenuItem _itemActualSize;

		private ToolStripMenuItem _itemFullPage;

		private ToolStripMenuItem _itemPageWidth;

		private ToolStripMenuItem _itemTwoPages;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripMenuItem _item500;

		private ToolStripMenuItem _item200;

		private ToolStripMenuItem _item150;

		private ToolStripMenuItem _item100;

		private ToolStripMenuItem _item75;

		private ToolStripMenuItem _item50;

		private ToolStripMenuItem _item25;

		private ToolStripMenuItem _item10;

		private ToolStripButton _btnFirst;

		private ToolStripButton _btnPrev;

		private ToolStripTextBox _txtStartPage;

		private ToolStripLabel _lblPageCount;

		private ToolStripButton _btnNext;

		private ToolStripButton _btnLast;

		private ToolStripSeparator _separator;

		private ToolStripButton _btnCancel;

		private ToolStripSeparator toolStripSeparator2;

		private PrintDocument _doc;

		private PreviewManager frmParent;

		public PrintDocument Document
		{
			get
			{
				return this._doc;
			}
			set
			{
				if (this._doc != null)
				{
					this._doc.BeginPrint -= new PrintEventHandler(this._doc_BeginPrint);
					this._doc.EndPrint -= new PrintEventHandler(this._doc_EndPrint);
				}
				this._doc = value;
				if (this._doc != null)
				{
					this._doc.BeginPrint += new PrintEventHandler(this._doc_BeginPrint);
					this._doc.EndPrint += new PrintEventHandler(this._doc_EndPrint);
				}
				if (base.Visible)
				{
					this._preview.Document = this.Document;
				}
			}
		}

		public CustomPrintPreviewDialog() : this(null)
		{
		}

		public CustomPrintPreviewDialog(PreviewManager parentForm)
		{
			this.InitializeComponent();
			if (parentForm != null)
			{
				this.frmParent = parentForm;
				base.Size = parentForm.Size;
			}
			this.LoadResources();
		}

		private void _btnCancel_Click(object sender, EventArgs e)
		{
			if (!this._preview.IsRendering)
			{
				base.Close();
				return;
			}
			this._preview.Cancel();
		}

		private void _btnFirst_Click(object sender, EventArgs e)
		{
			this._preview.StartPage = 0;
			this._btnNext.Enabled = true;
			this._btnLast.Enabled = true;
			this._btnPrev.Enabled = false;
			this._btnFirst.Enabled = false;
		}

		private void _btnLast_Click(object sender, EventArgs e)
		{
			this._preview.StartPage = this._preview.PageCount - 1;
			this._btnNext.Enabled = false;
			this._btnLast.Enabled = false;
			this._btnPrev.Enabled = true;
			this._btnFirst.Enabled = true;
		}

		private void _btnNext_Click(object sender, EventArgs e)
		{
			CustomPrintPreviewControl startPage = this._preview;
			startPage.StartPage = startPage.StartPage + 1;
			if (this._preview.StartPage == this._preview.PageCount - 1)
			{
				this._btnNext.Enabled = false;
				this._btnLast.Enabled = false;
				this._btnPrev.Enabled = true;
				this._btnFirst.Enabled = true;
				return;
			}
			this._btnNext.Enabled = true;
			this._btnLast.Enabled = true;
			this._btnPrev.Enabled = true;
			this._btnFirst.Enabled = true;
		}

		private void _btnPageSetup_Click(object sender, EventArgs e)
		{
			using (PageSetupDialog pageSetupDialog = new PageSetupDialog())
			{
				pageSetupDialog.Document = this.Document;
				if (pageSetupDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this._preview.RefreshPreview();
				}
			}
		}

		private void _btnPrev_Click(object sender, EventArgs e)
		{
			CustomPrintPreviewControl startPage = this._preview;
			startPage.StartPage = startPage.StartPage - 1;
			if (this._preview.StartPage == 0)
			{
				this._btnNext.Enabled = true;
				this._btnLast.Enabled = true;
				this._btnPrev.Enabled = false;
				this._btnFirst.Enabled = false;
				return;
			}
			this._btnNext.Enabled = true;
			this._btnLast.Enabled = true;
			this._btnPrev.Enabled = true;
			this._btnFirst.Enabled = true;
		}

		private void _btnPrint_Click(object sender, EventArgs e)
		{
			using (PrintDialog printDialog = new PrintDialog())
			{
				printDialog.AllowSomePages = true;
				printDialog.AllowSelection = true;
				printDialog.UseEXDialog = true;
				printDialog.Document = this.Document;
				PrinterSettings printerSettings = printDialog.PrinterSettings;
				int num = 1;
				int num1 = num;
				printerSettings.FromPage = num;
				printerSettings.MinimumPage = num1;
				int pageCount = this._preview.PageCount;
				int num2 = pageCount;
				printerSettings.ToPage = pageCount;
				printerSettings.MaximumPage = num2;
				if (printDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
				{
					this._preview.Print();
				}
			}
		}

		private void _btnZoom_ButtonClick(object sender, EventArgs e)
		{
			this._preview.ZoomMode = (this._preview.ZoomMode == ZoomMode.ActualSize ? ZoomMode.FullPage : ZoomMode.ActualSize);
		}

		private void _btnZoom_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == this._itemActualSize)
			{
				this._preview.ZoomMode = ZoomMode.ActualSize;
			}
			else if (e.ClickedItem == this._itemFullPage)
			{
				this._preview.ZoomMode = ZoomMode.FullPage;
			}
			else if (e.ClickedItem == this._itemPageWidth)
			{
				this._preview.ZoomMode = ZoomMode.PageWidth;
			}
			else if (e.ClickedItem == this._itemTwoPages)
			{
				this._preview.ZoomMode = ZoomMode.TwoPages;
			}
			if (e.ClickedItem == this._item10)
			{
				this._preview.Zoom = 0.1;
				return;
			}
			if (e.ClickedItem == this._item100)
			{
				this._preview.Zoom = 1;
				return;
			}
			if (e.ClickedItem == this._item150)
			{
				this._preview.Zoom = 1.5;
				return;
			}
			if (e.ClickedItem == this._item200)
			{
				this._preview.Zoom = 2;
				return;
			}
			if (e.ClickedItem == this._item25)
			{
				this._preview.Zoom = 0.25;
				return;
			}
			if (e.ClickedItem == this._item50)
			{
				this._preview.Zoom = 0.5;
				return;
			}
			if (e.ClickedItem == this._item500)
			{
				this._preview.Zoom = 5;
				return;
			}
			if (e.ClickedItem == this._item75)
			{
				this._preview.Zoom = 0.75;
			}
		}

		private void _doc_BeginPrint(object sender, PrintEventArgs e)
		{
			ToolStripButton toolStripButton = this._btnPrint;
			int num = 0;
			bool flag = (bool)num;
			this._btnPageSetup.Enabled = (bool)num;
			toolStripButton.Enabled = flag;
		}

		private void _doc_EndPrint(object sender, PrintEventArgs e)
		{
			ToolStripButton toolStripButton = this._btnPrint;
			int num = 1;
			bool flag = (bool)num;
			this._btnPageSetup.Enabled = (bool)num;
			toolStripButton.Enabled = flag;
			try
			{
				base.Focus();
			}
			catch
			{
			}
		}

		private void _preview_PageCountChanged(object sender, EventArgs e)
		{
			base.Update();
			Application.DoEvents();
			this._lblPageCount.Text = string.Format("of {0}", this._preview.PageCount);
			if (this._preview.PageCount > 1)
			{
				this._btnNext.Enabled = true;
				this._btnLast.Enabled = true;
			}
		}

		private void _preview_StartPageChanged(object sender, EventArgs e)
		{
			int startPage = this._preview.StartPage + 1;
			this._txtStartPage.Text = startPage.ToString();
		}

		private void _txtStartPage_Enter(object sender, EventArgs e)
		{
			this._txtStartPage.SelectAll();
		}

		private void _txtStartPage_KeyPress(object sender, KeyPressEventArgs e)
		{
			char keyChar = e.KeyChar;
			if (keyChar == '\r')
			{
				this.CommitPageNumber();
				e.Handled = true;
				return;
			}
			if (keyChar > ' ' && !char.IsDigit(keyChar))
			{
				e.Handled = true;
			}
		}

		private void _txtStartPage_Validating(object sender, CancelEventArgs e)
		{
			this.CommitPageNumber();
		}

		private void CommitPageNumber()
		{
			int num;
			if (int.TryParse(this._txtStartPage.Text, out num))
			{
				this._preview.StartPage = num - 1;
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
				str = string.Concat(str, "/Screen[@Name='PRINT']");
				str = string.Concat(str, "/Screen[@Name='PRINT_PREVIEW']");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CustomPrintPreviewDialog));
			this._toolStrip = new ToolStrip();
			this._btnPrint = new ToolStripButton();
			this._btnPageSetup = new ToolStripButton();
			this._btnZoom = new ToolStripSplitButton();
			this._itemActualSize = new ToolStripMenuItem();
			this._itemFullPage = new ToolStripMenuItem();
			this._itemPageWidth = new ToolStripMenuItem();
			this._itemTwoPages = new ToolStripMenuItem();
			this.toolStripMenuItem1 = new ToolStripSeparator();
			this._item500 = new ToolStripMenuItem();
			this._item200 = new ToolStripMenuItem();
			this._item150 = new ToolStripMenuItem();
			this._item100 = new ToolStripMenuItem();
			this._item75 = new ToolStripMenuItem();
			this._item50 = new ToolStripMenuItem();
			this._item25 = new ToolStripMenuItem();
			this._item10 = new ToolStripMenuItem();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this._btnFirst = new ToolStripButton();
			this._btnPrev = new ToolStripButton();
			this._txtStartPage = new ToolStripTextBox();
			this._lblPageCount = new ToolStripLabel();
			this._btnNext = new ToolStripButton();
			this._btnLast = new ToolStripButton();
			this._separator = new ToolStripSeparator();
			this._btnCancel = new ToolStripButton();
			this._preview = new CustomPrintPreviewControl();
			this._toolStrip.SuspendLayout();
			base.SuspendLayout();
			this._toolStrip.GripStyle = ToolStripGripStyle.Hidden;
			ToolStripItemCollection items = this._toolStrip.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this._btnPrint, this._btnPageSetup, this._btnZoom, this.toolStripSeparator2, this._btnFirst, this._btnPrev, this._txtStartPage, this._lblPageCount, this._btnNext, this._btnLast, this._separator, this._btnCancel };
			items.AddRange(toolStripItemArray);
			this._toolStrip.Location = new Point(0, 0);
			this._toolStrip.Name = "_toolStrip";
			this._toolStrip.Size = new System.Drawing.Size(426, 25);
			this._toolStrip.TabIndex = 0;
			this._toolStrip.Text = "toolStrip1";
			this._btnPrint.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this._btnPrint.Image = (Image)componentResourceManager.GetObject("_btnPrint.Image");
			this._btnPrint.ImageTransparentColor = Color.Magenta;
			this._btnPrint.Name = "_btnPrint";
			this._btnPrint.Size = new System.Drawing.Size(23, 22);
			this._btnPrint.Text = "Print Document";
			this._btnPrint.Visible = false;
			this._btnPrint.Click += new EventHandler(this._btnPrint_Click);
			this._btnPageSetup.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this._btnPageSetup.Image = (Image)componentResourceManager.GetObject("_btnPageSetup.Image");
			this._btnPageSetup.ImageTransparentColor = Color.Magenta;
			this._btnPageSetup.Name = "_btnPageSetup";
			this._btnPageSetup.Size = new System.Drawing.Size(23, 22);
			this._btnPageSetup.Text = "Page Setup";
			this._btnPageSetup.Visible = false;
			this._btnPageSetup.Click += new EventHandler(this._btnPageSetup_Click);
			this._btnZoom.AutoToolTip = false;
			ToolStripItemCollection dropDownItems = this._btnZoom.DropDownItems;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this._itemActualSize, this._itemFullPage, this._itemPageWidth, this._itemTwoPages, this.toolStripMenuItem1, this._item500, this._item200, this._item150, this._item100, this._item75, this._item50, this._item25, this._item10 };
			dropDownItems.AddRange(toolStripItemArray1);
			this._btnZoom.Image = (Image)componentResourceManager.GetObject("_btnZoom.Image");
			this._btnZoom.ImageTransparentColor = Color.Magenta;
			this._btnZoom.Name = "_btnZoom";
			this._btnZoom.Size = new System.Drawing.Size(71, 22);
			this._btnZoom.Text = "&Zoom";
			this._btnZoom.ButtonClick += new EventHandler(this._btnZoom_ButtonClick);
			this._btnZoom.DropDownItemClicked += new ToolStripItemClickedEventHandler(this._btnZoom_DropDownItemClicked);
			this._itemActualSize.Image = (Image)componentResourceManager.GetObject("_itemActualSize.Image");
			this._itemActualSize.Name = "_itemActualSize";
			this._itemActualSize.Size = new System.Drawing.Size(135, 22);
			this._itemActualSize.Text = "Actual Size";
			this._itemFullPage.Image = (Image)componentResourceManager.GetObject("_itemFullPage.Image");
			this._itemFullPage.Name = "_itemFullPage";
			this._itemFullPage.Size = new System.Drawing.Size(135, 22);
			this._itemFullPage.Text = "Full Page";
			this._itemPageWidth.Image = (Image)componentResourceManager.GetObject("_itemPageWidth.Image");
			this._itemPageWidth.Name = "_itemPageWidth";
			this._itemPageWidth.Size = new System.Drawing.Size(135, 22);
			this._itemPageWidth.Text = "Page Width";
			this._itemTwoPages.Image = (Image)componentResourceManager.GetObject("_itemTwoPages.Image");
			this._itemTwoPages.Name = "_itemTwoPages";
			this._itemTwoPages.Size = new System.Drawing.Size(135, 22);
			this._itemTwoPages.Text = "Two Pages";
			this._itemTwoPages.Visible = false;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(132, 6);
			this.toolStripMenuItem1.Visible = false;
			this._item500.Name = "_item500";
			this._item500.Size = new System.Drawing.Size(135, 22);
			this._item500.Text = "500%";
			this._item500.Visible = false;
			this._item200.Name = "_item200";
			this._item200.Size = new System.Drawing.Size(135, 22);
			this._item200.Text = "200%";
			this._item200.Visible = false;
			this._item150.Name = "_item150";
			this._item150.Size = new System.Drawing.Size(135, 22);
			this._item150.Text = "150%";
			this._item150.Visible = false;
			this._item100.Name = "_item100";
			this._item100.Size = new System.Drawing.Size(135, 22);
			this._item100.Text = "100%";
			this._item100.Visible = false;
			this._item75.Name = "_item75";
			this._item75.Size = new System.Drawing.Size(135, 22);
			this._item75.Text = "75%";
			this._item75.Visible = false;
			this._item50.Name = "_item50";
			this._item50.Size = new System.Drawing.Size(135, 22);
			this._item50.Text = "50%";
			this._item50.Visible = false;
			this._item25.Name = "_item25";
			this._item25.Size = new System.Drawing.Size(135, 22);
			this._item25.Text = "25%";
			this._item25.Visible = false;
			this._item10.Name = "_item10";
			this._item10.Size = new System.Drawing.Size(135, 22);
			this._item10.Text = "10%";
			this._item10.Visible = false;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this._btnFirst.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this._btnFirst.Enabled = false;
			this._btnFirst.Image = Resources.Nav_First;
			this._btnFirst.ImageTransparentColor = Color.Red;
			this._btnFirst.Name = "_btnFirst";
			this._btnFirst.Size = new System.Drawing.Size(23, 22);
			this._btnFirst.Text = "First Page";
			this._btnFirst.Click += new EventHandler(this._btnFirst_Click);
			this._btnPrev.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this._btnPrev.Enabled = false;
			this._btnPrev.Image = Resources.Nav_Prev;
			this._btnPrev.ImageTransparentColor = Color.Red;
			this._btnPrev.Name = "_btnPrev";
			this._btnPrev.Size = new System.Drawing.Size(23, 22);
			this._btnPrev.Text = "Previous Page";
			this._btnPrev.Click += new EventHandler(this._btnPrev_Click);
			this._txtStartPage.AutoSize = false;
			this._txtStartPage.Name = "_txtStartPage";
			this._txtStartPage.Size = new System.Drawing.Size(32, 21);
			this._txtStartPage.TextBoxTextAlign = HorizontalAlignment.Center;
			this._txtStartPage.Validating += new CancelEventHandler(this._txtStartPage_Validating);
			this._txtStartPage.Enter += new EventHandler(this._txtStartPage_Enter);
			this._txtStartPage.KeyPress += new KeyPressEventHandler(this._txtStartPage_KeyPress);
			this._lblPageCount.Name = "_lblPageCount";
			this._lblPageCount.Size = new System.Drawing.Size(10, 22);
			this._lblPageCount.Text = " ";
			this._btnNext.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this._btnNext.Enabled = false;
			this._btnNext.Image = Resources.Nav_Next;
			this._btnNext.ImageTransparentColor = Color.Red;
			this._btnNext.Name = "_btnNext";
			this._btnNext.Size = new System.Drawing.Size(23, 22);
			this._btnNext.Text = "Next Page";
			this._btnNext.Click += new EventHandler(this._btnNext_Click);
			this._btnLast.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this._btnLast.Enabled = false;
			this._btnLast.Image = Resources.Nav_Last;
			this._btnLast.ImageTransparentColor = Color.Red;
			this._btnLast.Name = "_btnLast";
			this._btnLast.Size = new System.Drawing.Size(23, 22);
			this._btnLast.Text = "Last Page";
			this._btnLast.Click += new EventHandler(this._btnLast_Click);
			this._separator.Name = "_separator";
			this._separator.Size = new System.Drawing.Size(6, 25);
			this._separator.Visible = false;
			this._btnCancel.AutoToolTip = false;
			this._btnCancel.Image = (Image)componentResourceManager.GetObject("_btnCancel.Image");
			this._btnCancel.ImageTransparentColor = Color.Magenta;
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(63, 22);
			this._btnCancel.Text = "Cancel";
			this._btnCancel.Visible = false;
			this._btnCancel.Click += new EventHandler(this._btnCancel_Click);
			this._preview.AutoScroll = true;
			this._preview.Dock = DockStyle.Fill;
			this._preview.Document = null;
			this._preview.Location = new Point(0, 25);
			this._preview.Name = "_preview";
			this._preview.Size = new System.Drawing.Size(426, 323);
			this._preview.TabIndex = 1;
			this._preview.PageCountChanged += new EventHandler(this._preview_PageCountChanged);
			this._preview.StartPageChanged += new EventHandler(this._preview_StartPageChanged);
			base.AutoScaleDimensions = new SizeF(96f, 96f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			base.ClientSize = new System.Drawing.Size(426, 348);
			base.Controls.Add(this._preview);
			base.Controls.Add(this._toolStrip);
			base.Name = "CustomPrintPreviewDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Print Preview";
			base.WindowState = FormWindowState.Maximized;
			this._toolStrip.ResumeLayout(false);
			this._toolStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Print Preview", "PRINT_PREVIEW", ResourceType.TITLE);
			this._btnFirst.Text = this.GetResource("First Page", "FIRST_PAGE", ResourceType.BUTTON);
			this._btnPrev.Text = this.GetResource("Previous page", "PREVIOUS_PAGE", ResourceType.BUTTON);
			this._btnNext.Text = this.GetResource("Next Page", "NEXT_PAGE", ResourceType.BUTTON);
			this._btnLast.Text = this.GetResource("Last Page", "LAST_PAGE", ResourceType.BUTTON);
			this._btnZoom.Text = this.GetResource("Zoom", "ZOOM", ResourceType.BUTTON);
			this._itemActualSize.Text = this.GetResource("1:1", "1:1", ResourceType.BUTTON);
			this._itemFullPage.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.BUTTON);
			this._itemPageWidth.Text = this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.BUTTON);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			if (this._preview.IsRendering && !e.Cancel)
			{
				this._preview.Cancel();
			}
		}

		protected override void OnShown(EventArgs e)
		{
			try
			{
				if (this.frmParent.objParentPrintDlg.objPreviewProcessingDlg != null)
				{
					this.frmParent.objParentPrintDlg.objPreviewProcessingDlg.Hide();
				}
				base.OnShown(e);
				this._preview.Document = this.Document;
			}
			catch
			{
				if (this.frmParent.objParentPrintDlg.objPreviewProcessingDlg != null)
				{
					this.frmParent.objParentPrintDlg.objPreviewProcessingDlg.Hide();
				}
			}
		}
	}
}
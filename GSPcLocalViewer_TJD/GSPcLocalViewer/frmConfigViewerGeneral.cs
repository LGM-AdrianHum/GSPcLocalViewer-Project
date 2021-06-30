using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmConfigViewerGeneral : Form
	{
		private frmConfig frmParent;

		private IContainer components;

		private Label lblGeneral;

		private Button btnOK;

		private Button btnCancel;

		private Panel pnlForm;

		private Panel pnlPicture;

		private CheckBox chkMaintainZoomWhileNavigating;

		private Label lblPicture;

		private Label lblDefaultZoom;

		private Panel pnlcmbDefaultZoom;

		private Panel pnlLblPicture;

		private Label lblPictureLine;

		private Panel pnlPartslist;

		private Panel pnlLblPartslist;

		private Label lblPartslistLine;

		private Label lblPartslist;

		private CheckBox chkShowPartslistToolbar;

		private CheckBox chkShowPictureToolbar;

		private Panel pnlLanguage;

		private Panel pnlLang;

		public ComboBox cmbLanguagesList;

		private Label lblDefaultLanguage;

		private Panel panel2;

		private Label label2;

		private Label lblLanguage;

		private NumericTextBox txtZoomStep;

		private Label lblZoomStep;

		private CheckBox chkFitPageForMulitParts;

		private ComboBox cmbDefaultZoom;

		private Panel pnlHistory;

		private CheckBox chkOpenBookInCurrentWindow;

		private NumericTextBox txtHistorySize;

		private Label lblHistorySize;

		private Panel pnlLblHistory;

		private Label lblHistoryLine;

		private Label lblOther;

		private CheckBox chkSideBySidePrinting;

		private CheckBox chkMouseWheelScrollOnPicture;

		private CheckBox chkMouseWheelScrollOnContents;

		private CheckBox chkContentsExpandAll;

		private CheckBox chkHankakuZenKaku;

		private NumericUpDown numCmbContentsExpandLevel;

		private Label lblContentsExpandLevel;

		private Panel panel1;

		public string GetApplicationLanguage
		{
			get
			{
				return this.cmbLanguagesList.SelectedItem.ToString();
			}
		}

		public string GetDefaultZoom
		{
			get
			{
				if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX)))
				{
					return "WIDTH";
				}
				if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX)))
				{
					return "FITPAGE";
				}
				if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("One to One", "ONE_TO_ONE", ResourceType.COMBO_BOX)))
				{
					return "ONE2ONE";
				}
				if (this.cmbDefaultZoom.Text.Trim().Equals(this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX)))
				{
					return "STRETCH";
				}
				return this.cmbDefaultZoom.Text;
			}
		}

		public bool GetExpandAllContentsFlag
		{
			get
			{
				bool @checked;
				try
				{
					@checked = this.chkContentsExpandAll.Checked;
				}
				catch
				{
					@checked = true;
				}
				return @checked;
			}
		}

		public int GetExpandContentsLevel
		{
			get
			{
				int num;
				try
				{
					decimal value = this.numCmbContentsExpandLevel.Value;
					num = int.Parse(value.ToString());
				}
				catch
				{
					num = 1;
				}
				return num;
			}
		}

		public bool GetFitPageForMultiParts
		{
			get
			{
				bool @checked;
				try
				{
					@checked = this.chkFitPageForMulitParts.Checked;
				}
				catch
				{
					@checked = true;
				}
				return @checked;
			}
		}

		public bool GetHankakuZenkakuFlag
		{
			get
			{
				bool @checked;
				try
				{
					@checked = this.chkHankakuZenKaku.Checked;
				}
				catch
				{
					@checked = true;
				}
				return @checked;
			}
		}

		public int GetHistorySize
		{
			get
			{
				int num = 0;
				if (!int.TryParse(this.txtHistorySize.Text, out num))
				{
					return Settings.Default.HistorySize;
				}
				if (num >= 0)
				{
					return num;
				}
				return Settings.Default.HistorySize;
			}
		}

		public bool GetMaintainZoom
		{
			get
			{
				return this.chkMaintainZoomWhileNavigating.Checked;
			}
		}

		public bool GetMouseWheelScrollContents
		{
			get
			{
				return this.chkMouseWheelScrollOnContents.Checked;
			}
		}

		public bool GetMouseWheelScrollPicture
		{
			get
			{
				return this.chkMouseWheelScrollOnPicture.Checked;
			}
		}

		public bool GetOpenBookinCurrentWindow
		{
			get
			{
				return this.chkOpenBookInCurrentWindow.Checked;
			}
		}

		public bool GetShowListToolbar
		{
			get
			{
				return this.chkShowPartslistToolbar.Checked;
			}
		}

		public bool GetShowPicToolbar
		{
			get
			{
				return this.chkShowPictureToolbar.Checked;
			}
		}

		public bool GetSideBySide
		{
			get
			{
				return this.chkSideBySidePrinting.Checked;
			}
		}

		public int GetZoomStep
		{
			get
			{
				int num;
				try
				{
					num = int.Parse(this.txtZoomStep.Text);
				}
				catch
				{
					num = 100;
				}
				return num;
			}
		}

		public frmConfigViewerGeneral(frmConfig frm)
		{
			this.InitializeComponent();
			try
			{
				this.frmParent = frm;
				base.MdiParent = frm;
				this.UpdateFont();
				this.LoadResources();
				this.txtHistorySize.Text = Settings.Default.HistorySize.ToString();
				this.chkOpenBookInCurrentWindow.Checked = Settings.Default.OpenInCurrentInstance;
				this.chkMaintainZoomWhileNavigating.Checked = Settings.Default.MaintainZoom;
				if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("WIDTH"))
				{
					this.cmbDefaultZoom.Text = this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX);
				}
				else if (Settings.Default.DefaultPictureZoom.ToUpper().Contains("FITPAGE"))
				{
					this.cmbDefaultZoom.Text = this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX);
				}
				else if (Settings.Default.DefaultPictureZoom.ToUpper().Equals("ONE2ONE"))
				{
					this.cmbDefaultZoom.Text = this.GetResource("One To One", "ONE_TO_ONE", ResourceType.COMBO_BOX);
				}
				else if (!Settings.Default.DefaultPictureZoom.ToUpper().Equals("STRETCH"))
				{
					this.cmbDefaultZoom.Text = Settings.Default.DefaultPictureZoom;
				}
				else
				{
					this.cmbDefaultZoom.Text = this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX);
				}
				this.chkShowPictureToolbar.Checked = Settings.Default.ShowPicToolbar;
				this.chkShowPartslistToolbar.Checked = Settings.Default.ShowListToolbar;
				this.LoadXML();
				if (!this.cmbLanguagesList.Items.Contains("English"))
				{
					this.cmbLanguagesList.Items.Add("English");
				}
				this.SetDisplayNameIndex();
				this.cmbLanguagesList.SelectedItem = Settings.Default.appLanguage;
				if (this.cmbLanguagesList.SelectedItem == null)
				{
					this.cmbLanguagesList.SelectedItem = "English";
				}
				if (!Settings.Default.appZoomStep.Equals(string.Empty))
				{
					this.txtZoomStep.Text = Settings.Default.appZoomStep.ToString();
				}
				this.chkFitPageForMulitParts.Checked = Settings.Default.appFitPageForMultiParts;
				this.chkSideBySidePrinting.Checked = Settings.Default.SideBySidePrinting;
				this.chkMouseWheelScrollOnContents.Visible = Program.objAppFeatures.bDjVuScroll;
				this.chkMouseWheelScrollOnPicture.Visible = Program.objAppFeatures.bDjVuScroll;
				this.chkMouseWheelScrollOnContents.Checked = Settings.Default.MouseScrollContents;
				this.chkMouseWheelScrollOnPicture.Checked = Settings.Default.MouseScrollPicture;
				this.chkShowPictureToolbar.Visible = !Program.objAppFeatures.bDjVuToolbar;
				if (this.frmParent.frmParent.BookType == "GSC")
				{
					this.pnlPartslist.Hide();
				}
				this.chkContentsExpandAll.Checked = Settings.Default.ExpandAllContents;
				this.chkHankakuZenKaku.Checked = Settings.Default.HankakuZenkakuFlag;
				this.numCmbContentsExpandLevel.Value = Settings.Default.ExpandContentsLevel;
			}
			catch
			{
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.frmParent.CloseAndSaveSettings();
		}

		private void chkContentsExpandAll_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				this.numCmbContentsExpandLevel.Enabled = !this.chkContentsExpandAll.Checked;
			}
			catch
			{
			}
		}

		private void cmbLanguagesList_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmConfigViewerGeneral_Load(object sender, EventArgs e)
		{
			this.OnOffFeatures();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='CONFIGURATION']");
				str = string.Concat(str, "/Screen[@Name='GENERAL']");
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
			this.lblGeneral = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlForm = new Panel();
			this.pnlHistory = new Panel();
			this.txtHistorySize = new NumericTextBox();
			this.pnlLblHistory = new Panel();
			this.lblHistoryLine = new Label();
			this.lblOther = new Label();
			this.numCmbContentsExpandLevel = new NumericUpDown();
			this.lblContentsExpandLevel = new Label();
			this.chkContentsExpandAll = new CheckBox();
			this.chkHankakuZenKaku = new CheckBox();
			this.chkMouseWheelScrollOnPicture = new CheckBox();
			this.chkMouseWheelScrollOnContents = new CheckBox();
			this.chkSideBySidePrinting = new CheckBox();
			this.chkOpenBookInCurrentWindow = new CheckBox();
			this.lblHistorySize = new Label();
			this.panel1 = new Panel();
			this.pnlLanguage = new Panel();
			this.lblDefaultLanguage = new Label();
			this.panel2 = new Panel();
			this.label2 = new Label();
			this.lblLanguage = new Label();
			this.pnlLang = new Panel();
			this.cmbLanguagesList = new ComboBox();
			this.pnlPartslist = new Panel();
			this.pnlLblPartslist = new Panel();
			this.lblPartslistLine = new Label();
			this.lblPartslist = new Label();
			this.chkShowPartslistToolbar = new CheckBox();
			this.pnlPicture = new Panel();
			this.chkFitPageForMulitParts = new CheckBox();
			this.txtZoomStep = new NumericTextBox();
			this.lblZoomStep = new Label();
			this.pnlcmbDefaultZoom = new Panel();
			this.cmbDefaultZoom = new ComboBox();
			this.chkShowPictureToolbar = new CheckBox();
			this.chkMaintainZoomWhileNavigating = new CheckBox();
			this.lblDefaultZoom = new Label();
			this.pnlLblPicture = new Panel();
			this.lblPictureLine = new Label();
			this.lblPicture = new Label();
			this.pnlForm.SuspendLayout();
			this.pnlHistory.SuspendLayout();
			this.pnlLblHistory.SuspendLayout();
			((ISupportInitialize)this.numCmbContentsExpandLevel).BeginInit();
			this.panel1.SuspendLayout();
			this.pnlLanguage.SuspendLayout();
			this.panel2.SuspendLayout();
			this.pnlLang.SuspendLayout();
			this.pnlPartslist.SuspendLayout();
			this.pnlLblPartslist.SuspendLayout();
			this.pnlPicture.SuspendLayout();
			this.pnlcmbDefaultZoom.SuspendLayout();
			this.pnlLblPicture.SuspendLayout();
			base.SuspendLayout();
			this.lblGeneral.BackColor = Color.White;
			this.lblGeneral.Dock = DockStyle.Top;
			this.lblGeneral.ForeColor = Color.Black;
			this.lblGeneral.Location = new Point(0, 0);
			this.lblGeneral.Name = "lblGeneral";
			this.lblGeneral.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblGeneral.Size = new System.Drawing.Size(448, 27);
			this.lblGeneral.TabIndex = 16;
			this.lblGeneral.Text = "General";
			this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnOK.Location = new Point(282, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.btnCancel.Location = new Point(358, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlHistory);
			this.pnlForm.Controls.Add(this.panel1);
			this.pnlForm.Controls.Add(this.pnlLanguage);
			this.pnlForm.Controls.Add(this.pnlPartslist);
			this.pnlForm.Controls.Add(this.pnlPicture);
			this.pnlForm.Controls.Add(this.lblGeneral);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(450, 569);
			this.pnlForm.TabIndex = 19;
			this.pnlHistory.AutoScroll = true;
			this.pnlHistory.Controls.Add(this.txtHistorySize);
			this.pnlHistory.Controls.Add(this.pnlLblHistory);
			this.pnlHistory.Controls.Add(this.numCmbContentsExpandLevel);
			this.pnlHistory.Controls.Add(this.lblContentsExpandLevel);
			this.pnlHistory.Controls.Add(this.chkContentsExpandAll);
			this.pnlHistory.Controls.Add(this.chkHankakuZenKaku);
			this.pnlHistory.Controls.Add(this.chkMouseWheelScrollOnPicture);
			this.pnlHistory.Controls.Add(this.chkMouseWheelScrollOnContents);
			this.pnlHistory.Controls.Add(this.chkSideBySidePrinting);
			this.pnlHistory.Controls.Add(this.chkOpenBookInCurrentWindow);
			this.pnlHistory.Controls.Add(this.lblHistorySize);
			this.pnlHistory.Dock = DockStyle.Fill;
			this.pnlHistory.Location = new Point(0, 292);
			this.pnlHistory.Name = "pnlHistory";
			this.pnlHistory.Size = new System.Drawing.Size(448, 245);
			this.pnlHistory.TabIndex = 28;
			this.txtHistorySize.AllowSpace = false;
			this.txtHistorySize.BorderStyle = BorderStyle.FixedSingle;
			this.txtHistorySize.Location = new Point(134, 26);
			this.txtHistorySize.MaxLength = 2;
			this.txtHistorySize.Name = "txtHistorySize";
			this.txtHistorySize.Size = new System.Drawing.Size(124, 21);
			this.txtHistorySize.TabIndex = 9;
			this.pnlLblHistory.Controls.Add(this.lblHistoryLine);
			this.pnlLblHistory.Controls.Add(this.lblOther);
			this.pnlLblHistory.Dock = DockStyle.Top;
			this.pnlLblHistory.Location = new Point(0, 0);
			this.pnlLblHistory.Name = "pnlLblHistory";
			this.pnlLblHistory.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblHistory.Size = new System.Drawing.Size(448, 28);
			this.pnlLblHistory.TabIndex = 16;
			this.lblHistoryLine.BackColor = Color.Transparent;
			this.lblHistoryLine.Dock = DockStyle.Fill;
			this.lblHistoryLine.ForeColor = Color.Blue;
			this.lblHistoryLine.Image = Resources.GroupLine0;
			this.lblHistoryLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblHistoryLine.Location = new Point(82, 0);
			this.lblHistoryLine.Name = "lblHistoryLine";
			this.lblHistoryLine.Size = new System.Drawing.Size(351, 28);
			this.lblHistoryLine.TabIndex = 15;
			this.lblHistoryLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblOther.BackColor = Color.Transparent;
			this.lblOther.Dock = DockStyle.Left;
			this.lblOther.ForeColor = Color.Blue;
			this.lblOther.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblOther.Location = new Point(7, 0);
			this.lblOther.Name = "lblOther";
			this.lblOther.Size = new System.Drawing.Size(75, 28);
			this.lblOther.TabIndex = 13;
			this.lblOther.Text = "Other";
			this.lblOther.TextAlign = ContentAlignment.MiddleLeft;
			this.numCmbContentsExpandLevel.Location = new Point(217, 183);
			NumericUpDown num = this.numCmbContentsExpandLevel;
			int[] numArray = new int[] { 10, 0, 0, 0 };
			num.Maximum = new decimal(numArray);
			NumericUpDown numericUpDown = this.numCmbContentsExpandLevel;
			int[] numArray1 = new int[] { 1, 0, 0, 0 };
			numericUpDown.Minimum = new decimal(numArray1);
			this.numCmbContentsExpandLevel.Name = "numCmbContentsExpandLevel";
			this.numCmbContentsExpandLevel.Size = new System.Drawing.Size(40, 21);
			this.numCmbContentsExpandLevel.TabIndex = 26;
			this.numCmbContentsExpandLevel.TextAlign = HorizontalAlignment.Center;
			NumericUpDown num1 = this.numCmbContentsExpandLevel;
			int[] numArray2 = new int[] { 1, 0, 0, 0 };
			num1.Value = new decimal(numArray2);
			this.lblContentsExpandLevel.AutoSize = true;
			this.lblContentsExpandLevel.Location = new Point(19, 187);
			this.lblContentsExpandLevel.Name = "lblContentsExpandLevel";
			this.lblContentsExpandLevel.Size = new System.Drawing.Size(140, 13);
			this.lblContentsExpandLevel.TabIndex = 24;
			this.lblContentsExpandLevel.Text = "Contents expanded to level";
			this.chkContentsExpandAll.AutoSize = true;
			this.chkContentsExpandAll.Location = new Point(21, 165);
			this.chkContentsExpandAll.Name = "chkContentsExpandAll";
			this.chkContentsExpandAll.Size = new System.Drawing.Size(123, 17);
			this.chkContentsExpandAll.TabIndex = 23;
			this.chkContentsExpandAll.Text = "Expand All Contents";
			this.chkContentsExpandAll.UseVisualStyleBackColor = true;
			this.chkContentsExpandAll.CheckedChanged += new EventHandler(this.chkContentsExpandAll_CheckedChanged);
			this.chkHankakuZenKaku.AutoSize = true;
			this.chkHankakuZenKaku.Location = new Point(21, 142);
			this.chkHankakuZenKaku.Name = "chkHankakuZenKaku";
			this.chkHankakuZenKaku.Size = new System.Drawing.Size(171, 17);
			this.chkHankakuZenKaku.TabIndex = 22;
			this.chkHankakuZenKaku.Text = "Search HanKaku <-> ZenKaku";
			this.chkHankakuZenKaku.UseVisualStyleBackColor = true;
			this.chkMouseWheelScrollOnPicture.AutoSize = true;
			this.chkMouseWheelScrollOnPicture.Location = new Point(21, 119);
			this.chkMouseWheelScrollOnPicture.Name = "chkMouseWheelScrollOnPicture";
			this.chkMouseWheelScrollOnPicture.Size = new System.Drawing.Size(169, 17);
			this.chkMouseWheelScrollOnPicture.TabIndex = 21;
			this.chkMouseWheelScrollOnPicture.Text = "Mouse Wheel Scroll on Picture";
			this.chkMouseWheelScrollOnPicture.UseVisualStyleBackColor = true;
			this.chkMouseWheelScrollOnContents.AutoSize = true;
			this.chkMouseWheelScrollOnContents.Location = new Point(21, 96);
			this.chkMouseWheelScrollOnContents.Name = "chkMouseWheelScrollOnContents";
			this.chkMouseWheelScrollOnContents.Size = new System.Drawing.Size(180, 17);
			this.chkMouseWheelScrollOnContents.TabIndex = 20;
			this.chkMouseWheelScrollOnContents.Text = "Mouse Wheel Scroll on Contents";
			this.chkMouseWheelScrollOnContents.UseVisualStyleBackColor = true;
			this.chkSideBySidePrinting.AutoSize = true;
			this.chkSideBySidePrinting.Location = new Point(21, 73);
			this.chkSideBySidePrinting.Name = "chkSideBySidePrinting";
			this.chkSideBySidePrinting.Size = new System.Drawing.Size(123, 17);
			this.chkSideBySidePrinting.TabIndex = 19;
			this.chkSideBySidePrinting.Text = "Side By Side Printing";
			this.chkSideBySidePrinting.UseVisualStyleBackColor = true;
			this.chkOpenBookInCurrentWindow.AutoSize = true;
			this.chkOpenBookInCurrentWindow.Location = new Point(21, 50);
			this.chkOpenBookInCurrentWindow.Name = "chkOpenBookInCurrentWindow";
			this.chkOpenBookInCurrentWindow.Size = new System.Drawing.Size(170, 17);
			this.chkOpenBookInCurrentWindow.TabIndex = 17;
			this.chkOpenBookInCurrentWindow.Text = "Open Book in Current Window";
			this.chkOpenBookInCurrentWindow.UseVisualStyleBackColor = true;
			this.lblHistorySize.AutoSize = true;
			this.lblHistorySize.Location = new Point(20, 28);
			this.lblHistorySize.Name = "lblHistorySize";
			this.lblHistorySize.Size = new System.Drawing.Size(63, 13);
			this.lblHistorySize.TabIndex = 8;
			this.lblHistorySize.Text = "History Size";
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Dock = DockStyle.Bottom;
			this.panel1.Location = new Point(0, 537);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(448, 30);
			this.panel1.TabIndex = 29;
			this.pnlLanguage.Controls.Add(this.lblDefaultLanguage);
			this.pnlLanguage.Controls.Add(this.panel2);
			this.pnlLanguage.Controls.Add(this.pnlLang);
			this.pnlLanguage.Dock = DockStyle.Top;
			this.pnlLanguage.Location = new Point(0, 237);
			this.pnlLanguage.Name = "pnlLanguage";
			this.pnlLanguage.Size = new System.Drawing.Size(448, 55);
			this.pnlLanguage.TabIndex = 27;
			this.lblDefaultLanguage.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.lblDefaultLanguage.AutoSize = true;
			this.lblDefaultLanguage.Location = new Point(20, 33);
			this.lblDefaultLanguage.Name = "lblDefaultLanguage";
			this.lblDefaultLanguage.Size = new System.Drawing.Size(92, 13);
			this.lblDefaultLanguage.TabIndex = 18;
			this.lblDefaultLanguage.Text = "Default Language";
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.lblLanguage);
			this.panel2.Dock = DockStyle.Top;
			this.panel2.Location = new Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.panel2.Size = new System.Drawing.Size(448, 28);
			this.panel2.TabIndex = 17;
			this.label2.BackColor = Color.Transparent;
			this.label2.Dock = DockStyle.Fill;
			this.label2.ForeColor = Color.Blue;
			this.label2.Image = Resources.GroupLine0;
			this.label2.ImageAlign = ContentAlignment.MiddleLeft;
			this.label2.Location = new Point(82, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(351, 28);
			this.label2.TabIndex = 15;
			this.label2.TextAlign = ContentAlignment.MiddleLeft;
			this.lblLanguage.BackColor = Color.Transparent;
			this.lblLanguage.Dock = DockStyle.Left;
			this.lblLanguage.ForeColor = Color.Blue;
			this.lblLanguage.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblLanguage.Location = new Point(7, 0);
			this.lblLanguage.Name = "lblLanguage";
			this.lblLanguage.Size = new System.Drawing.Size(75, 28);
			this.lblLanguage.TabIndex = 12;
			this.lblLanguage.Text = "Language";
			this.lblLanguage.TextAlign = ContentAlignment.MiddleLeft;
			this.pnlLang.BorderStyle = BorderStyle.FixedSingle;
			this.pnlLang.Controls.Add(this.cmbLanguagesList);
			this.pnlLang.Location = new Point(134, 31);
			this.pnlLang.Name = "pnlLang";
			this.pnlLang.Size = new System.Drawing.Size(124, 21);
			this.pnlLang.TabIndex = 20;
			this.cmbLanguagesList.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbLanguagesList.DropDownWidth = 121;
			this.cmbLanguagesList.FlatStyle = FlatStyle.Flat;
			this.cmbLanguagesList.FormattingEnabled = true;
			this.cmbLanguagesList.Location = new Point(0, 0);
			this.cmbLanguagesList.Name = "cmbLanguagesList";
			this.cmbLanguagesList.Size = new System.Drawing.Size(122, 21);
			this.cmbLanguagesList.TabIndex = 19;
			this.cmbLanguagesList.SelectedIndexChanged += new EventHandler(this.cmbLanguagesList_SelectedIndexChanged);
			this.pnlPartslist.Controls.Add(this.pnlLblPartslist);
			this.pnlPartslist.Controls.Add(this.chkShowPartslistToolbar);
			this.pnlPartslist.Dock = DockStyle.Top;
			this.pnlPartslist.Location = new Point(0, 182);
			this.pnlPartslist.Name = "pnlPartslist";
			this.pnlPartslist.Size = new System.Drawing.Size(448, 55);
			this.pnlPartslist.TabIndex = 26;
			this.pnlLblPartslist.Controls.Add(this.lblPartslistLine);
			this.pnlLblPartslist.Controls.Add(this.lblPartslist);
			this.pnlLblPartslist.Dock = DockStyle.Top;
			this.pnlLblPartslist.Location = new Point(0, 0);
			this.pnlLblPartslist.Name = "pnlLblPartslist";
			this.pnlLblPartslist.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblPartslist.Size = new System.Drawing.Size(448, 28);
			this.pnlLblPartslist.TabIndex = 17;
			this.lblPartslistLine.BackColor = Color.Transparent;
			this.lblPartslistLine.Dock = DockStyle.Fill;
			this.lblPartslistLine.ForeColor = Color.Blue;
			this.lblPartslistLine.Image = Resources.GroupLine0;
			this.lblPartslistLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblPartslistLine.Location = new Point(82, 0);
			this.lblPartslistLine.Name = "lblPartslistLine";
			this.lblPartslistLine.Size = new System.Drawing.Size(351, 28);
			this.lblPartslistLine.TabIndex = 15;
			this.lblPartslistLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblPartslist.BackColor = Color.Transparent;
			this.lblPartslist.Dock = DockStyle.Left;
			this.lblPartslist.ForeColor = Color.Blue;
			this.lblPartslist.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblPartslist.Location = new Point(7, 0);
			this.lblPartslist.Name = "lblPartslist";
			this.lblPartslist.Size = new System.Drawing.Size(75, 28);
			this.lblPartslist.TabIndex = 12;
			this.lblPartslist.Text = "Partslist";
			this.lblPartslist.TextAlign = ContentAlignment.MiddleLeft;
			this.chkShowPartslistToolbar.AutoSize = true;
			this.chkShowPartslistToolbar.Location = new Point(22, 31);
			this.chkShowPartslistToolbar.Name = "chkShowPartslistToolbar";
			this.chkShowPartslistToolbar.Size = new System.Drawing.Size(178, 17);
			this.chkShowPartslistToolbar.TabIndex = 1;
			this.chkShowPartslistToolbar.Text = "Show Partslist Toolbar In Frame";
			this.chkShowPartslistToolbar.UseVisualStyleBackColor = true;
			this.pnlPicture.Controls.Add(this.chkFitPageForMulitParts);
			this.pnlPicture.Controls.Add(this.txtZoomStep);
			this.pnlPicture.Controls.Add(this.lblZoomStep);
			this.pnlPicture.Controls.Add(this.pnlcmbDefaultZoom);
			this.pnlPicture.Controls.Add(this.chkShowPictureToolbar);
			this.pnlPicture.Controls.Add(this.chkMaintainZoomWhileNavigating);
			this.pnlPicture.Controls.Add(this.lblDefaultZoom);
			this.pnlPicture.Controls.Add(this.pnlLblPicture);
			this.pnlPicture.Dock = DockStyle.Top;
			this.pnlPicture.Location = new Point(0, 27);
			this.pnlPicture.Name = "pnlPicture";
			this.pnlPicture.Size = new System.Drawing.Size(448, 155);
			this.pnlPicture.TabIndex = 25;
			this.chkFitPageForMulitParts.AutoSize = true;
			this.chkFitPageForMulitParts.Location = new Point(21, 77);
			this.chkFitPageForMulitParts.Name = "chkFitPageForMulitParts";
			this.chkFitPageForMulitParts.Size = new System.Drawing.Size(162, 17);
			this.chkFitPageForMulitParts.TabIndex = 20;
			this.chkFitPageForMulitParts.Text = "Zoom fit Page for multi parts";
			this.chkFitPageForMulitParts.UseVisualStyleBackColor = true;
			this.txtZoomStep.AllowSpace = false;
			this.txtZoomStep.BorderStyle = BorderStyle.FixedSingle;
			this.txtZoomStep.Location = new Point(134, 128);
			this.txtZoomStep.MaxLength = 3;
			this.txtZoomStep.Name = "txtZoomStep";
			this.txtZoomStep.Size = new System.Drawing.Size(124, 21);
			this.txtZoomStep.TabIndex = 19;
			this.lblZoomStep.AutoSize = true;
			this.lblZoomStep.Location = new Point(20, 130);
			this.lblZoomStep.Name = "lblZoomStep";
			this.lblZoomStep.Size = new System.Drawing.Size(58, 13);
			this.lblZoomStep.TabIndex = 18;
			this.lblZoomStep.Text = "Zoom Step";
			this.pnlcmbDefaultZoom.BorderStyle = BorderStyle.FixedSingle;
			this.pnlcmbDefaultZoom.Controls.Add(this.cmbDefaultZoom);
			this.pnlcmbDefaultZoom.Location = new Point(134, 100);
			this.pnlcmbDefaultZoom.Name = "pnlcmbDefaultZoom";
			this.pnlcmbDefaultZoom.Size = new System.Drawing.Size(124, 21);
			this.pnlcmbDefaultZoom.TabIndex = 14;
			this.cmbDefaultZoom.BackColor = SystemColors.Window;
			this.cmbDefaultZoom.Dock = DockStyle.Fill;
			this.cmbDefaultZoom.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDefaultZoom.DropDownWidth = 121;
			this.cmbDefaultZoom.FlatStyle = FlatStyle.Flat;
			this.cmbDefaultZoom.FormattingEnabled = true;
			ComboBox.ObjectCollection items = this.cmbDefaultZoom.Items;
			object[] objArray = new object[] { "300%", "150%", "100%", "50%", "25%" };
			items.AddRange(objArray);
			this.cmbDefaultZoom.Location = new Point(0, 0);
			this.cmbDefaultZoom.Name = "cmbDefaultZoom";
			this.cmbDefaultZoom.Size = new System.Drawing.Size(122, 21);
			this.cmbDefaultZoom.TabIndex = 14;
			this.chkShowPictureToolbar.AutoSize = true;
			this.chkShowPictureToolbar.Location = new Point(21, 31);
			this.chkShowPictureToolbar.Name = "chkShowPictureToolbar";
			this.chkShowPictureToolbar.Size = new System.Drawing.Size(173, 17);
			this.chkShowPictureToolbar.TabIndex = 1;
			this.chkShowPictureToolbar.Text = "Show Picture Toolbar In Frame";
			this.chkShowPictureToolbar.UseVisualStyleBackColor = true;
			this.chkMaintainZoomWhileNavigating.AutoSize = true;
			this.chkMaintainZoomWhileNavigating.Location = new Point(21, 54);
			this.chkMaintainZoomWhileNavigating.Name = "chkMaintainZoomWhileNavigating";
			this.chkMaintainZoomWhileNavigating.Size = new System.Drawing.Size(264, 17);
			this.chkMaintainZoomWhileNavigating.TabIndex = 1;
			this.chkMaintainZoomWhileNavigating.Text = "Maintain Zoom Level On Pictures While Navigating";
			this.chkMaintainZoomWhileNavigating.UseVisualStyleBackColor = true;
			this.lblDefaultZoom.AutoSize = true;
			this.lblDefaultZoom.Location = new Point(20, 106);
			this.lblDefaultZoom.Name = "lblDefaultZoom";
			this.lblDefaultZoom.Size = new System.Drawing.Size(71, 13);
			this.lblDefaultZoom.TabIndex = 8;
			this.lblDefaultZoom.Text = "Default Zoom";
			this.pnlLblPicture.Controls.Add(this.lblPictureLine);
			this.pnlLblPicture.Controls.Add(this.lblPicture);
			this.pnlLblPicture.Dock = DockStyle.Top;
			this.pnlLblPicture.Location = new Point(0, 0);
			this.pnlLblPicture.Name = "pnlLblPicture";
			this.pnlLblPicture.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblPicture.Size = new System.Drawing.Size(448, 28);
			this.pnlLblPicture.TabIndex = 17;
			this.lblPictureLine.BackColor = Color.Transparent;
			this.lblPictureLine.Dock = DockStyle.Fill;
			this.lblPictureLine.ForeColor = Color.Blue;
			this.lblPictureLine.Image = Resources.GroupLine0;
			this.lblPictureLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblPictureLine.Location = new Point(82, 0);
			this.lblPictureLine.Name = "lblPictureLine";
			this.lblPictureLine.Size = new System.Drawing.Size(351, 28);
			this.lblPictureLine.TabIndex = 15;
			this.lblPictureLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblPicture.BackColor = Color.Transparent;
			this.lblPicture.Dock = DockStyle.Left;
			this.lblPicture.ForeColor = Color.Blue;
			this.lblPicture.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblPicture.Location = new Point(7, 0);
			this.lblPicture.Name = "lblPicture";
			this.lblPicture.Size = new System.Drawing.Size(75, 28);
			this.lblPicture.TabIndex = 12;
			this.lblPicture.Text = "Picture";
			this.lblPicture.TextAlign = ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(450, 569);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmConfigViewerGeneral";
			base.Load += new EventHandler(this.frmConfigViewerGeneral_Load);
			this.pnlForm.ResumeLayout(false);
			this.pnlHistory.ResumeLayout(false);
			this.pnlHistory.PerformLayout();
			this.pnlLblHistory.ResumeLayout(false);
			((ISupportInitialize)this.numCmbContentsExpandLevel).EndInit();
			this.panel1.ResumeLayout(false);
			this.pnlLanguage.ResumeLayout(false);
			this.pnlLanguage.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.pnlLang.ResumeLayout(false);
			this.pnlPartslist.ResumeLayout(false);
			this.pnlPartslist.PerformLayout();
			this.pnlLblPartslist.ResumeLayout(false);
			this.pnlPicture.ResumeLayout(false);
			this.pnlPicture.PerformLayout();
			this.pnlcmbDefaultZoom.ResumeLayout(false);
			this.pnlLblPicture.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		private void LoadResources()
		{
			this.lblGeneral.Text = this.GetResource("General", "GENERAL", ResourceType.LABEL);
			this.lblPicture.Text = this.GetResource("Picture", "PICTURE", ResourceType.LABEL);
			this.chkShowPictureToolbar.Text = this.GetResource("Show Picture Toolbar in Frame", "PICTURE_TOOLBAR_FRAME", ResourceType.CHECK_BOX);
			this.chkMaintainZoomWhileNavigating.Text = this.GetResource("Maintain Zoom Level during navigation", "MAINTAIN_ZOOM", ResourceType.CHECK_BOX);
			this.lblDefaultZoom.Text = this.GetResource("Default Zoom", "DEFAULT_ZOOM", ResourceType.LABEL);
			this.lblPartslist.Text = this.GetResource("Parts List", "PARTS_LIST", ResourceType.LABEL);
			this.chkShowPartslistToolbar.Text = this.GetResource("Show Parts List Toolbar in Frame", "PARTSLIST_TOOLBAR_FRAME", ResourceType.CHECK_BOX);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.cmbDefaultZoom.Items.Add(this.GetResource("Fit Page", "FIT_PAGE", ResourceType.COMBO_BOX));
			this.cmbDefaultZoom.Items.Add(this.GetResource("Fit Width", "FIT_WIDTH", ResourceType.COMBO_BOX));
			this.cmbDefaultZoom.Items.Add(this.GetResource("One to One", "ONE_TO_ONE", ResourceType.COMBO_BOX));
			this.cmbDefaultZoom.Items.Add(this.GetResource("Stretch", "STRETCH", ResourceType.COMBO_BOX));
			this.lblDefaultLanguage.Text = this.GetResource("Default Language", "DEFAULT_LANGUAGE", ResourceType.LABEL);
			this.lblLanguage.Text = this.GetResource("Language", "LANGUAGE", ResourceType.LABEL);
			this.lblOther.Text = this.GetResource("Other", "OTHER", ResourceType.LABEL);
			this.lblHistorySize.Text = this.GetResource("History", "HISTORY_SIZE", ResourceType.LABEL);
			this.chkOpenBookInCurrentWindow.Text = this.GetResource("Open Book in Current Window", "OPEN_BOOK_CURRENT", ResourceType.CHECK_BOX);
			this.chkFitPageForMulitParts.Text = this.GetResource("Zoom Fit Page for multi parts", "ZOOMFIT_MULTIPARTS", ResourceType.CHECK_BOX);
			this.lblZoomStep.Text = this.GetResource("Zoom Step", "ZOOM_STEP", ResourceType.LABEL);
			this.chkSideBySidePrinting.Text = this.GetResource("Side By Side Printing", "SIDE_BY_SIDE_PRINTING", ResourceType.CHECK_BOX);
			this.chkMouseWheelScrollOnPicture.Text = this.GetResource("Use mouse wheel scroll on Images", "WHEELSCROLL_IMAGES", ResourceType.CHECK_BOX);
			this.chkMouseWheelScrollOnContents.Text = this.GetResource("Use mouse wheel scroll on Contents", "MOUSEWHEEL_CONTENTS", ResourceType.CHECK_BOX);
			this.chkHankakuZenKaku.Text = this.GetResource("Search HanKaku <-> ZenKaku", "HANKAKU_ZENKAKU", ResourceType.CHECK_BOX);
			this.chkContentsExpandAll.Text = this.GetResource("Expand All Contents", "CONTENTS_EXPAND_ALL", ResourceType.CHECK_BOX);
			this.lblContentsExpandLevel.Text = this.GetResource("Contents Expanded to Level", "CONTENTS_EXPAND_LEVEL", ResourceType.LABEL);
		}

		private void LoadXML()
		{
			try
			{
				string str = string.Concat(Application.StartupPath, "\\Language XMLs\\");
				string[] files = Directory.GetFiles(str, "*.xml");
				for (int i = 0; i < (int)files.Length; i++)
				{
					if (File.Exists(files[i]))
					{
						try
						{
							int num = files[i].IndexOf(".xml");
							int num1 = files[i].LastIndexOf("\\");
							string str1 = files[i].Substring(num1 + 1, num - num1 - 1);
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1, ".xml"));
							XmlNode xmlNodes = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
							string value = xmlNodes.Attributes["Language"].Value;
							if (!this.cmbLanguagesList.Items.Contains(value))
							{
								this.cmbLanguagesList.Items.Add(value);
							}
						}
						catch
						{
						}
					}
				}
			}
			catch
			{
			}
		}

		public void OnOffFeatures()
		{
			try
			{
				this.lblHistorySize.Visible = Program.objAppFeatures.bHistory;
				this.txtHistorySize.Visible = Program.objAppFeatures.bHistory;
				this.chkMouseWheelScrollOnContents.Visible = Program.objAppFeatures.bDjVuScroll;
				this.chkMouseWheelScrollOnPicture.Visible = Program.objAppFeatures.bDjVuScroll;
				this.btnOK.Visible = true;
				this.btnCancel.Visible = true;
			}
			catch
			{
			}
		}

		private void SetDisplayNameIndex()
		{
			try
			{
				bool flag = false;
				string str = string.Concat(Application.StartupPath, "\\Language XMLs\\");
				string[] files = Directory.GetFiles(str, "*.xml");
				for (int i = 0; i < (int)files.Length; i++)
				{
					try
					{
						if (File.Exists(files[i]))
						{
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(files[i]);
							XmlDocument xmlDocument = new XmlDocument();
							xmlDocument.Load(string.Concat(Application.StartupPath, "\\Language XMLs\\", fileNameWithoutExtension, ".xml"));
							XmlNode xmlNodes = xmlDocument.SelectSingleNode("//GSPcLocalViewer");
							string value = xmlNodes.Attributes["EN_NAME"].Value;
							string value1 = xmlNodes.Attributes["Language"].Value;
							if (value.ToLower() == Settings.Default.appLanguage.ToLower())
							{
								this.cmbLanguagesList.SelectedItem = value1;
								flag = true;
							}
						}
					}
					catch
					{
					}
				}
				if (!flag)
				{
					this.cmbLanguagesList.SelectedItem = "English";
				}
			}
			catch
			{
			}
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblGeneral.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}
	}
}
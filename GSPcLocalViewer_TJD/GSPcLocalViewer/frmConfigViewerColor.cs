using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmConfigViewerColor : Form
	{
		private IContainer components;

		private Panel pnlForm;

		private Label lblColorGeneral;

		private Panel pnlControl;

		private Button btnOK;

		private Button btnCancel;

		private BackgroundWorker bgWorker;

		private PictureBox picLoading;

		private Panel pnlColor;

		private ComboBox cmbForeColorPartsList;

		private ComboBox cmbBackColorPartsList;

		private Label label1;

		private Label lblBackColor;

		private Panel pnlFont;

		private Label lblColorPartsList;

		private ComboBox cmbForeColorGeneral;

		private ComboBox cmbBackColorGeneral;

		private Label label2;

		private Label lblBackColorGeneral;

		private frmConfig frmParent;

		private ArrayList colorArray;

		public Color GetHighlightBackColorGeneral
		{
			get
			{
				if (this.cmbBackColorGeneral.SelectedItem == null)
				{
					return Settings.Default.appHighlightBackColor;
				}
				return Color.FromKnownColor((KnownColor)this.colorArray[this.cmbBackColorGeneral.SelectedIndex]);
			}
		}

		public Color GetHighlightBackColorPartsList
		{
			get
			{
				if (this.cmbBackColorPartsList.SelectedItem == null)
				{
					return Settings.Default.PartsListInfoBackColor;
				}
				return Color.FromKnownColor((KnownColor)this.colorArray[this.cmbBackColorPartsList.SelectedIndex]);
			}
		}

		public Color GetHighlightForeColorGeneral
		{
			get
			{
				if (this.cmbForeColorGeneral.SelectedItem == null)
				{
					return Settings.Default.appHighlightForeColor;
				}
				return Color.FromKnownColor((KnownColor)this.colorArray[this.cmbForeColorGeneral.SelectedIndex]);
			}
		}

		public Color GetHighlightForeColorPartsList
		{
			get
			{
				if (this.cmbForeColorPartsList.SelectedItem == null)
				{
					return Settings.Default.PartsListInfoForeColor;
				}
				return Color.FromKnownColor((KnownColor)this.colorArray[this.cmbForeColorPartsList.SelectedIndex]);
			}
		}

		public frmConfigViewerColor(frmConfig frm)
		{
			this.InitializeComponent();
			this.colorArray = new ArrayList();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.cmbBackColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbForeColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbBackColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbForeColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
			this.UpdateFont();
			this.LoadResources();
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.LoadComboBoxes();
			Thread.Sleep(500);
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.HideLoading(this.pnlForm);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.frmParent.CloseAndSaveSettings();
		}

		private void cmbBackColorGeneral_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle bounds = e.Bounds;
			if (e.Index >= 0)
			{
				Rectangle rectangle = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4)
				{
					Width = 50
				};
				bounds = new Rectangle(rectangle.Right + 4, bounds.Y, bounds.Width, bounds.Height);
				SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor)this.colorArray[e.Index]));
				graphics.FillRectangle(solidBrush, rectangle);
				StringFormat stringFormat = new StringFormat()
				{
					Alignment = StringAlignment.Near
				};
				Console.WriteLine(e.State.ToString());
				e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1f), rectangle);
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					e.Graphics.FillRectangle(new SolidBrush(Settings.Default.appHighlightBackColor), bounds);
					Graphics graphic = e.Graphics;
					Color color = solidBrush.Color;
					graphic.DrawString(color.Name, this.cmbBackColorGeneral.Font, new SolidBrush(Settings.Default.appHighlightForeColor), bounds, stringFormat);
					return;
				}
				e.Graphics.FillRectangle(new SolidBrush(this.cmbBackColorGeneral.BackColor), bounds);
				Graphics graphics1 = e.Graphics;
				Color color1 = solidBrush.Color;
				graphics1.DrawString(color1.Name, this.cmbBackColorGeneral.Font, new SolidBrush(this.cmbBackColorGeneral.ForeColor), bounds, stringFormat);
			}
		}

		private void cmbBackColorPartsList_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle bounds = e.Bounds;
			if (e.Index >= 0)
			{
				Rectangle rectangle = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4)
				{
					Width = 50
				};
				bounds = new Rectangle(rectangle.Right + 4, bounds.Y, bounds.Width, bounds.Height);
				SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor)this.colorArray[e.Index]));
				graphics.FillRectangle(solidBrush, rectangle);
				StringFormat stringFormat = new StringFormat()
				{
					Alignment = StringAlignment.Near
				};
				Console.WriteLine(e.State.ToString());
				e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1f), rectangle);
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					e.Graphics.FillRectangle(new SolidBrush(Settings.Default.PartsListInfoBackColor), bounds);
					Graphics graphic = e.Graphics;
					Color color = solidBrush.Color;
					graphic.DrawString(color.Name, this.cmbBackColorPartsList.Font, new SolidBrush(Settings.Default.PartsListInfoForeColor), bounds, stringFormat);
					return;
				}
				e.Graphics.FillRectangle(new SolidBrush(this.cmbBackColorPartsList.BackColor), bounds);
				Graphics graphics1 = e.Graphics;
				Color color1 = solidBrush.Color;
				graphics1.DrawString(color1.Name, this.cmbBackColorPartsList.Font, new SolidBrush(this.cmbBackColorPartsList.ForeColor), bounds, stringFormat);
			}
		}

		private void cmbForeColorGeneral_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle bounds = e.Bounds;
			if (e.Index >= 0)
			{
				Rectangle rectangle = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4)
				{
					Width = 50
				};
				bounds = new Rectangle(rectangle.Right + 4, bounds.Y, bounds.Width, bounds.Height);
				SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor)this.colorArray[e.Index]));
				graphics.FillRectangle(solidBrush, rectangle);
				StringFormat stringFormat = new StringFormat()
				{
					Alignment = StringAlignment.Near
				};
				Console.WriteLine(e.State.ToString());
				e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1f), rectangle);
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					e.Graphics.FillRectangle(new SolidBrush(Settings.Default.appHighlightBackColor), bounds);
					Graphics graphic = e.Graphics;
					Color color = solidBrush.Color;
					graphic.DrawString(color.Name, this.cmbForeColorGeneral.Font, new SolidBrush(Settings.Default.appHighlightForeColor), bounds, stringFormat);
					return;
				}
				e.Graphics.FillRectangle(new SolidBrush(this.cmbForeColorGeneral.BackColor), bounds);
				Graphics graphics1 = e.Graphics;
				Color color1 = solidBrush.Color;
				graphics1.DrawString(color1.Name, this.cmbForeColorGeneral.Font, new SolidBrush(this.cmbForeColorGeneral.ForeColor), bounds, stringFormat);
			}
		}

		private void cmbForeColorPartsList_DrawItem(object sender, DrawItemEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle bounds = e.Bounds;
			if (e.Index >= 0)
			{
				Rectangle rectangle = new Rectangle(bounds.X + 2, bounds.Y + 2, bounds.Width - 4, bounds.Height - 4)
				{
					Width = 50
				};
				bounds = new Rectangle(rectangle.Right + 4, bounds.Y, bounds.Width, bounds.Height);
				SolidBrush solidBrush = new SolidBrush(Color.FromKnownColor((KnownColor)this.colorArray[e.Index]));
				graphics.FillRectangle(solidBrush, rectangle);
				StringFormat stringFormat = new StringFormat()
				{
					Alignment = StringAlignment.Near
				};
				Console.WriteLine(e.State.ToString());
				e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1f), rectangle);
				if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
				{
					e.Graphics.FillRectangle(new SolidBrush(Settings.Default.PartsListInfoBackColor), bounds);
					Graphics graphic = e.Graphics;
					Color color = solidBrush.Color;
					graphic.DrawString(color.Name, this.cmbForeColorPartsList.Font, new SolidBrush(Settings.Default.PartsListInfoForeColor), bounds, stringFormat);
					return;
				}
				e.Graphics.FillRectangle(new SolidBrush(this.cmbForeColorPartsList.BackColor), bounds);
				Graphics graphics1 = e.Graphics;
				Color color1 = solidBrush.Color;
				graphics1.DrawString(color1.Name, this.cmbForeColorPartsList.Font, new SolidBrush(this.cmbForeColorPartsList.ForeColor), bounds, stringFormat);
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

		private void frmConfigTemp_Load(object sender, EventArgs e)
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
				str = string.Concat(str, "/Screen[@Name='COLOR']");
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
			this.pnlForm = new Panel();
			this.pnlColor = new Panel();
			this.cmbForeColorPartsList = new ComboBox();
			this.cmbBackColorPartsList = new ComboBox();
			this.label1 = new Label();
			this.lblBackColor = new Label();
			this.lblColorPartsList = new Label();
			this.pnlControl = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlFont = new Panel();
			this.cmbForeColorGeneral = new ComboBox();
			this.cmbBackColorGeneral = new ComboBox();
			this.label2 = new Label();
			this.lblBackColorGeneral = new Label();
			this.lblColorGeneral = new Label();
			this.picLoading = new PictureBox();
			this.bgWorker = new BackgroundWorker();
			this.pnlForm.SuspendLayout();
			this.pnlColor.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.pnlFont.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlColor);
			this.pnlForm.Controls.Add(this.lblColorPartsList);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Controls.Add(this.pnlFont);
			this.pnlForm.Controls.Add(this.lblColorGeneral);
			this.pnlForm.Controls.Add(this.picLoading);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(450, 475);
			this.pnlForm.TabIndex = 17;
			this.pnlColor.AutoScroll = true;
			this.pnlColor.Controls.Add(this.cmbForeColorPartsList);
			this.pnlColor.Controls.Add(this.cmbBackColorPartsList);
			this.pnlColor.Controls.Add(this.label1);
			this.pnlColor.Controls.Add(this.lblBackColor);
			this.pnlColor.Dock = DockStyle.Top;
			this.pnlColor.Location = new Point(0, 242);
			this.pnlColor.Name = "pnlColor";
			this.pnlColor.Size = new System.Drawing.Size(448, 196);
			this.pnlColor.TabIndex = 20;
			this.cmbForeColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbForeColorPartsList.DropDownStyle = ComboBoxStyle.Simple;
			this.cmbForeColorPartsList.IntegralHeight = false;
			this.cmbForeColorPartsList.ItemHeight = 16;
			this.cmbForeColorPartsList.Location = new Point(231, 29);
			this.cmbForeColorPartsList.Name = "cmbForeColor";
			this.cmbForeColorPartsList.Size = new System.Drawing.Size(202, 152);
			this.cmbForeColorPartsList.TabIndex = 5;
			this.cmbForeColorPartsList.DrawItem += new DrawItemEventHandler(this.cmbForeColorPartsList_DrawItem);
			this.cmbBackColorPartsList.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbBackColorPartsList.DropDownStyle = ComboBoxStyle.Simple;
			this.cmbBackColorPartsList.IntegralHeight = false;
			this.cmbBackColorPartsList.ItemHeight = 16;
			this.cmbBackColorPartsList.Location = new Point(15, 29);
			this.cmbBackColorPartsList.Name = "cmbBackColor";
			this.cmbBackColorPartsList.Size = new System.Drawing.Size(200, 152);
			this.cmbBackColorPartsList.TabIndex = 4;
			this.cmbBackColorPartsList.DrawItem += new DrawItemEventHandler(this.cmbBackColorPartsList_DrawItem);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(228, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(97, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Highlight fore color";
			this.lblBackColor.AutoSize = true;
			this.lblBackColor.Location = new Point(12, 13);
			this.lblBackColor.Name = "lblBackColor";
			this.lblBackColor.Size = new System.Drawing.Size(99, 13);
			this.lblBackColor.TabIndex = 2;
			this.lblBackColor.Text = "Highlight back color";
			this.lblColorPartsList.BackColor = Color.White;
			this.lblColorPartsList.Dock = DockStyle.Top;
			this.lblColorPartsList.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblColorPartsList.ForeColor = Color.Black;
			this.lblColorPartsList.Location = new Point(0, 215);
			this.lblColorPartsList.Name = "lblColorPartsList";
			this.lblColorPartsList.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblColorPartsList.Size = new System.Drawing.Size(448, 27);
			this.lblColorPartsList.TabIndex = 23;
			this.lblColorPartsList.Text = "Color : Parts List Information";
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 442);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(4, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(448, 31);
			this.pnlControl.TabIndex = 18;
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
			this.pnlFont.AutoScroll = true;
			this.pnlFont.Controls.Add(this.cmbForeColorGeneral);
			this.pnlFont.Controls.Add(this.cmbBackColorGeneral);
			this.pnlFont.Controls.Add(this.label2);
			this.pnlFont.Controls.Add(this.lblBackColorGeneral);
			this.pnlFont.Dock = DockStyle.Top;
			this.pnlFont.Location = new Point(0, 27);
			this.pnlFont.Name = "pnlFont";
			this.pnlFont.Size = new System.Drawing.Size(448, 188);
			this.pnlFont.TabIndex = 19;
			this.cmbForeColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbForeColorGeneral.DropDownStyle = ComboBoxStyle.Simple;
			this.cmbForeColorGeneral.IntegralHeight = false;
			this.cmbForeColorGeneral.ItemHeight = 16;
			this.cmbForeColorGeneral.Location = new Point(233, 26);
			this.cmbForeColorGeneral.Name = "cmbForeColorGeneral";
			this.cmbForeColorGeneral.Size = new System.Drawing.Size(202, 152);
			this.cmbForeColorGeneral.TabIndex = 9;
			this.cmbForeColorGeneral.DrawItem += new DrawItemEventHandler(this.cmbForeColorGeneral_DrawItem);
			this.cmbBackColorGeneral.DrawMode = DrawMode.OwnerDrawFixed;
			this.cmbBackColorGeneral.DropDownStyle = ComboBoxStyle.Simple;
			this.cmbBackColorGeneral.IntegralHeight = false;
			this.cmbBackColorGeneral.ItemHeight = 16;
			this.cmbBackColorGeneral.Location = new Point(17, 26);
			this.cmbBackColorGeneral.Name = "cmbBackColorGeneral";
			this.cmbBackColorGeneral.Size = new System.Drawing.Size(200, 152);
			this.cmbBackColorGeneral.TabIndex = 8;
			this.cmbBackColorGeneral.DrawItem += new DrawItemEventHandler(this.cmbBackColorGeneral_DrawItem);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(230, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Highlight fore color";
			this.lblBackColorGeneral.AutoSize = true;
			this.lblBackColorGeneral.Location = new Point(14, 10);
			this.lblBackColorGeneral.Name = "lblBackColorGeneral";
			this.lblBackColorGeneral.Size = new System.Drawing.Size(99, 13);
			this.lblBackColorGeneral.TabIndex = 7;
			this.lblBackColorGeneral.Text = "Highlight back color";
			this.lblColorGeneral.BackColor = Color.White;
			this.lblColorGeneral.Dock = DockStyle.Top;
			this.lblColorGeneral.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.lblColorGeneral.ForeColor = Color.Black;
			this.lblColorGeneral.Location = new Point(0, 0);
			this.lblColorGeneral.Name = "lblColorGeneral";
			this.lblColorGeneral.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblColorGeneral.Size = new System.Drawing.Size(448, 27);
			this.lblColorGeneral.TabIndex = 16;
			this.lblColorGeneral.Text = "Color : General";
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(2, 1);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(32, 32);
			this.picLoading.TabIndex = 22;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(450, 475);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmConfigViewerColor";
			base.Load += new EventHandler(this.frmConfigTemp_Load);
			this.pnlForm.ResumeLayout(false);
			this.pnlColor.ResumeLayout(false);
			this.pnlColor.PerformLayout();
			this.pnlControl.ResumeLayout(false);
			this.pnlFont.ResumeLayout(false);
			this.pnlFont.PerformLayout();
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
		}

		private void LoadComboBoxes()
		{
			if (this.pnlForm.InvokeRequired)
			{
				this.pnlForm.Invoke(new frmConfigViewerColor.LoadComboBoxesDelegate(this.LoadComboBoxes));
				return;
			}
			InstalledFontCollection installedFontCollection = new InstalledFontCollection();
			for (KnownColor i = KnownColor.ActiveBorder; i < KnownColor.YellowGreen; i += KnownColor.ActiveBorder)
			{
				this.cmbBackColorPartsList.Items.Add(i.ToString());
				this.cmbForeColorPartsList.Items.Add(i.ToString());
				this.cmbBackColorGeneral.Items.Add(i.ToString());
				this.cmbForeColorGeneral.Items.Add(i.ToString());
				this.colorArray.Add(i);
			}
			this.cmbBackColorGeneral.SelectedItem = Settings.Default.appHighlightBackColor.Name;
			this.cmbForeColorGeneral.SelectedItem = Settings.Default.appHighlightForeColor.Name;
			this.cmbBackColorPartsList.SelectedItem = Settings.Default.PartsListInfoBackColor.Name;
			this.cmbForeColorPartsList.SelectedItem = Settings.Default.PartsListInfoForeColor.Name;
		}

		private void LoadResources()
		{
			this.lblColorGeneral.Text = string.Concat(this.GetResource("Color", "COLOR", ResourceType.LABEL), " : ", this.GetResource("General", "GENERAL", ResourceType.LABEL));
			this.lblColorPartsList.Text = string.Concat(this.GetResource("Color", "COLOR", ResourceType.LABEL), " : ", this.GetResource("Partlist Information", "PARTLIST_INFORMATION", ResourceType.LABEL));
			this.lblBackColor.Text = this.GetResource("Highlight back color", "BACKCOLOR_PART", ResourceType.LABEL);
			this.lblBackColorGeneral.Text = this.GetResource("Highlight back color", "BACKCOLOR_GENERAL", ResourceType.LABEL);
			this.label2.Text = this.GetResource("Highlight fore color", "FORECOLOR_GENERAL", ResourceType.LABEL);
			this.label1.Text = this.GetResource("Highlight fore color", "FORECOLOR_PART", ResourceType.LABEL);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				foreach (Control control in parentPanel.Controls)
				{
					if (control == this.picLoading || control == this.lblColorGeneral)
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
			this.lblColorGeneral.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
			this.lblColorPartsList.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}

		private delegate void LoadComboBoxesDelegate();
	}
}
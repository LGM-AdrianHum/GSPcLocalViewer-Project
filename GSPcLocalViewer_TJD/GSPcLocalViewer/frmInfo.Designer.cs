using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace GSPcLocalViewer
{
	public class frmInfo : DockContent
	{
		private IContainer components;

		private Panel pnlForm;

		private BackgroundWorker bgWorker;

		private PictureBox picLoading;

		private Panel pnlPageInfo;

		private Panel pnlrtbPageInfo;

		private RichTextBox rtbPageInfo;

		private Label lblPageInfo;

		private Panel pnlBookInfo;

		private Panel pnlrtbBookInfo;

		private RichTextBox rtbBookInfo;

		private Label lblBookInfo;

		private Timer timer1;

		private frmViewer frmParent;

		private XmlNode schemaNodeParent;

		private XmlNode pageNodeParent;

		public frmInfo(frmViewer frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.UpdateFont();
			this.LoadResources();
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.frmParent.bObjFrmSelectionlistClosed = false;
			object[] argument = (object[])e.Argument;
			XmlNode xmlNodes = (XmlNode)argument[0];
			XmlNode xmlNodes1 = (XmlNode)argument[1];
			this.LoadBookInfo();
			this.LoadPageInfo(xmlNodes, xmlNodes1);
			if (!this.pnlBookInfo.Visible && !this.pnlPageInfo.Visible)
			{
				this.ShowPanels(true);
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.frmParent.bObjFrmSelectionlistClosed = true;
			this.HideLoading(this.pnlForm);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmInfo_VisibleChanged(object sender, EventArgs e)
		{
			this.frmParent.informationToolStripMenuItem.Checked = base.Visible;
		}

		private string GetBookInfoLanguage(string sKey)
		{
			string str;
			bool flag = false;
			string str1 = string.Concat(Settings.Default.appLanguage, "_GSP_", Program.iniServers[this.frmParent.ServerId].sIniKey, ".ini");
			if (File.Exists(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1)))
			{
				TextReader streamReader = new StreamReader(string.Concat(Application.StartupPath, "\\Language XMLs\\", str1));
				while (true)
				{
					string str2 = streamReader.ReadLine();
					string str3 = str2;
					if (str2 == null)
					{
						return sKey;
					}
					if (str3.ToUpper() == "[VIEWER_INFORMATION]")
					{
						flag = true;
					}
					else if (str3.Contains("=") && flag)
					{
						string[] strArrays = new string[] { "=" };
						string[] strArrays1 = str3.Split(strArrays, StringSplitOptions.RemoveEmptyEntries);
						try
						{
							if (strArrays1[0].ToString().ToUpper() == sKey.ToUpper())
							{
								flag = false;
								str = strArrays1[1].ToString();
								break;
							}
						}
						catch
						{
							str = sKey;
							break;
						}
					}
					else if (str3.Contains("["))
					{
						flag = false;
					}
				}
				return str;
			}
			return sKey;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MAIN_FORM']");
				str = string.Concat(str, "/Screen[@Name='INFORMATION']");
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

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		public static extern long HideCaret(IntPtr hwnd);

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					this.picLoading.Hide();
					this.picLoading.Size = new System.Drawing.Size(32, 32);
					this.picLoading.Parent = this.pnlForm;
				}
				else
				{
					frmInfo.HideLoadingDelegate hideLoadingDelegate = new frmInfo.HideLoadingDelegate(this.HideLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(hideLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmInfo));
			this.pnlForm = new Panel();
			this.pnlPageInfo = new Panel();
			this.pnlrtbPageInfo = new Panel();
			this.rtbPageInfo = new RichTextBox();
			this.lblPageInfo = new Label();
			this.pnlBookInfo = new Panel();
			this.pnlrtbBookInfo = new Panel();
			this.rtbBookInfo = new RichTextBox();
			this.lblBookInfo = new Label();
			this.picLoading = new PictureBox();
			this.bgWorker = new BackgroundWorker();
			this.timer1 = new Timer(this.components);
			this.pnlForm.SuspendLayout();
			this.pnlPageInfo.SuspendLayout();
			this.pnlrtbPageInfo.SuspendLayout();
			this.pnlBookInfo.SuspendLayout();
			this.pnlrtbBookInfo.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			base.SuspendLayout();
			this.pnlForm.AutoScroll = true;
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlPageInfo);
			this.pnlForm.Controls.Add(this.pnlBookInfo);
			this.pnlForm.Controls.Add(this.picLoading);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(297, 555);
			this.pnlForm.TabIndex = 19;
			this.pnlPageInfo.BackColor = Color.White;
			this.pnlPageInfo.Controls.Add(this.pnlrtbPageInfo);
			this.pnlPageInfo.Controls.Add(this.lblPageInfo);
			this.pnlPageInfo.Dock = DockStyle.Fill;
			this.pnlPageInfo.Location = new Point(0, 209);
			this.pnlPageInfo.Name = "pnlPageInfo";
			this.pnlPageInfo.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
			this.pnlPageInfo.Size = new System.Drawing.Size(295, 344);
			this.pnlPageInfo.TabIndex = 31;
			this.pnlPageInfo.Tag = "";
			this.pnlPageInfo.Visible = false;
			this.pnlrtbPageInfo.BackColor = Color.White;
			this.pnlrtbPageInfo.Controls.Add(this.rtbPageInfo);
			this.pnlrtbPageInfo.Dock = DockStyle.Fill;
			this.pnlrtbPageInfo.Location = new Point(15, 38);
			this.pnlrtbPageInfo.Name = "pnlrtbPageInfo";
			this.pnlrtbPageInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnlrtbPageInfo.Size = new System.Drawing.Size(280, 306);
			this.pnlrtbPageInfo.TabIndex = 15;
			this.rtbPageInfo.BackColor = Color.White;
			this.rtbPageInfo.BorderStyle = BorderStyle.None;
			this.rtbPageInfo.Dock = DockStyle.Fill;
			this.rtbPageInfo.Location = new Point(10, 0);
			this.rtbPageInfo.Name = "rtbPageInfo";
			this.rtbPageInfo.ReadOnly = true;
			this.rtbPageInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtbPageInfo.Size = new System.Drawing.Size(270, 306);
			this.rtbPageInfo.TabIndex = 12;
			this.rtbPageInfo.Text = "";
			this.rtbPageInfo.MouseDown += new MouseEventHandler(this.rtbPageInfo_MouseDown_1);
			this.lblPageInfo.BackColor = Color.Transparent;
			this.lblPageInfo.Cursor = Cursors.Hand;
			this.lblPageInfo.Dock = DockStyle.Top;
			this.lblPageInfo.ForeColor = Color.Blue;
			this.lblPageInfo.Image = GSPcLocalViewer.Properties.Resources.GroupLine2;
			this.lblPageInfo.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblPageInfo.Location = new Point(15, 10);
			this.lblPageInfo.Name = "lblPageInfo";
			this.lblPageInfo.Size = new System.Drawing.Size(280, 28);
			this.lblPageInfo.TabIndex = 11;
			this.lblPageInfo.Tag = "";
			this.lblPageInfo.Text = "Page Information";
			this.lblPageInfo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblPageInfo.Click += new EventHandler(this.lblPageInfo_Click);
			this.pnlBookInfo.BackColor = Color.White;
			this.pnlBookInfo.Controls.Add(this.pnlrtbBookInfo);
			this.pnlBookInfo.Controls.Add(this.lblBookInfo);
			this.pnlBookInfo.Dock = DockStyle.Top;
			this.pnlBookInfo.Location = new Point(0, 0);
			this.pnlBookInfo.Name = "pnlBookInfo";
			this.pnlBookInfo.Padding = new System.Windows.Forms.Padding(15, 10, 0, 0);
			this.pnlBookInfo.Size = new System.Drawing.Size(295, 209);
			this.pnlBookInfo.TabIndex = 32;
			this.pnlBookInfo.Tag = "";
			this.pnlBookInfo.Visible = false;
			this.pnlrtbBookInfo.BackColor = Color.White;
			this.pnlrtbBookInfo.Controls.Add(this.rtbBookInfo);
			this.pnlrtbBookInfo.Dock = DockStyle.Fill;
			this.pnlrtbBookInfo.Location = new Point(15, 38);
			this.pnlrtbBookInfo.Name = "pnlrtbBookInfo";
			this.pnlrtbBookInfo.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnlrtbBookInfo.Size = new System.Drawing.Size(280, 171);
			this.pnlrtbBookInfo.TabIndex = 15;
			this.rtbBookInfo.BackColor = Color.White;
			this.rtbBookInfo.BorderStyle = BorderStyle.None;
			this.rtbBookInfo.CausesValidation = false;
			this.rtbBookInfo.Dock = DockStyle.Fill;
			this.rtbBookInfo.Location = new Point(10, 0);
			this.rtbBookInfo.Name = "rtbBookInfo";
			this.rtbBookInfo.ReadOnly = true;
			this.rtbBookInfo.ScrollBars = RichTextBoxScrollBars.Vertical;
			this.rtbBookInfo.Size = new System.Drawing.Size(270, 171);
			this.rtbBookInfo.TabIndex = 12;
			this.rtbBookInfo.Text = "";
			this.rtbBookInfo.MouseDown += new MouseEventHandler(this.rtbBookInfo_MouseDown);
			this.lblBookInfo.BackColor = Color.Transparent;
			this.lblBookInfo.Cursor = Cursors.Hand;
			this.lblBookInfo.Dock = DockStyle.Top;
			this.lblBookInfo.ForeColor = Color.Blue;
			this.lblBookInfo.Image = GSPcLocalViewer.Properties.Resources.GroupLine2;
			this.lblBookInfo.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblBookInfo.Location = new Point(15, 10);
			this.lblBookInfo.Name = "lblBookInfo";
			this.lblBookInfo.Size = new System.Drawing.Size(280, 28);
			this.lblBookInfo.TabIndex = 11;
			this.lblBookInfo.Tag = "";
			this.lblBookInfo.Text = "Book Information";
			this.lblBookInfo.TextAlign = ContentAlignment.MiddleLeft;
			this.lblBookInfo.Click += new EventHandler(this.lblBookInfo_Click);
			this.picLoading.BackColor = Color.White;
			this.picLoading.Dock = DockStyle.Fill;
			this.picLoading.Image = GSPcLocalViewer.Properties.Resources.Loading1;
			this.picLoading.Location = new Point(0, 0);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(295, 553);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 25;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(297, 555);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.HideOnClose = true;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "frmInfo";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			this.Text = "Information";
			this.pnlForm.ResumeLayout(false);
			this.pnlPageInfo.ResumeLayout(false);
			this.pnlrtbPageInfo.ResumeLayout(false);
			this.pnlBookInfo.ResumeLayout(false);
			this.pnlrtbBookInfo.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			base.ResumeLayout(false);
		}

		private void lblBookInfo_Click(object sender, EventArgs e)
		{
			if (this.pnlrtbBookInfo.Visible)
			{
				this.pnlrtbBookInfo.Visible = false;
				this.pnlBookInfo.Height = this.pnlBookInfo.Height - this.pnlrtbBookInfo.Height;
				this.lblBookInfo.Image = GSPcLocalViewer.Properties.Resources.GroupLine3;
				return;
			}
			this.pnlBookInfo.Height = this.pnlBookInfo.Height + this.pnlrtbBookInfo.Height;
			this.pnlrtbBookInfo.Visible = true;
			this.lblBookInfo.Image = GSPcLocalViewer.Properties.Resources.GroupLine2;
		}

		private void lblPageInfo_Click(object sender, EventArgs e)
		{
			if (this.pnlrtbPageInfo.Visible)
			{
				this.pnlrtbPageInfo.Visible = false;
				this.pnlPageInfo.Height = this.pnlPageInfo.Height - this.pnlrtbPageInfo.Height;
				this.lblPageInfo.Image = GSPcLocalViewer.Properties.Resources.GroupLine3;
				return;
			}
			this.pnlPageInfo.Height = this.pnlPageInfo.Height + this.pnlrtbPageInfo.Height;
			this.pnlrtbPageInfo.Visible = true;
			this.lblPageInfo.Image = GSPcLocalViewer.Properties.Resources.GroupLine2;
		}

		private void LoadBookInfo()
		{
			if (this.rtbBookInfo.InvokeRequired)
			{
				this.rtbBookInfo.Invoke(new frmInfo.LoadBookInfoDelegate(this.LoadBookInfo));
				return;
			}
			this.rtbBookInfo.SelectionFont = this.rtbBookInfo.Font;
			this.rtbBookInfo.Clear();
			try
			{
				XmlNode schemaNode = this.frmParent.SchemaNode;
				XmlNode bookNode = this.frmParent.BookNode;
				for (int i = 0; i < schemaNode.Attributes.Count; i++)
				{
					if (!schemaNode.Attributes[i].Value.ToUpper().StartsWith("LEVEL") && schemaNode.Attributes[i].Value.ToUpper() != "SECURITYLOCKS" && schemaNode.Attributes[i].Value.ToUpper() != "ID" && bookNode.Attributes[schemaNode.Attributes[i].Name] != null)
					{
						this.rtbBookInfo.SelectionColor = Color.Gray;
						this.rtbBookInfo.SelectedText = string.Concat(this.GetBookInfoLanguage(schemaNode.Attributes[i].Value), ": ");
						this.rtbBookInfo.SelectionColor = Color.Black;
						string value = bookNode.Attributes[schemaNode.Attributes[i].Name].Value;
						if (value.ToUpper().EndsWith(".DJVU") || value.ToUpper().EndsWith(".JPEG") || value.ToUpper().EndsWith(".TIF"))
						{
							value = value.Substring(0, value.IndexOf("."));
						}
						this.rtbBookInfo.SelectedText = string.Concat(value, "\n");
					}
				}
			}
			catch
			{
				this.rtbBookInfo.Clear();
				this.rtbBookInfo.SelectionColor = Color.Red;
				this.rtbBookInfo.SelectedText = this.GetResource("(E-INF-EM001) Failed to load specified object", "(E-INF-EM001)_FAILED", ResourceType.LABEL);
			}
		}

		public void LoadData(XmlNode schemaNode, XmlNode pageNode)
		{
			this.ShowLoading(this.pnlForm);
			this.bgWorker.RunWorkerAsync(new object[] { schemaNode, pageNode });
		}

		private void LoadPageInfo(XmlNode schemaNode, XmlNode pageNode)
		{
			if (schemaNode != null && pageNode != null)
			{
				if (this.rtbPageInfo.InvokeRequired)
				{
					RichTextBox richTextBox = this.rtbPageInfo;
					frmInfo.LoadPageInfoDelegate loadPageInfoDelegate = new frmInfo.LoadPageInfoDelegate(this.LoadPageInfo);
					object[] objArray = new object[] { schemaNode, pageNode };
					richTextBox.Invoke(loadPageInfoDelegate, objArray);
					return;
				}
				this.schemaNodeParent = schemaNode;
				this.pageNodeParent = pageNode;
				this.rtbPageInfo.SelectionFont = this.rtbBookInfo.Font;
				this.rtbPageInfo.Clear();
				try
				{
					for (int i = 0; i < schemaNode.Attributes.Count; i++)
					{
						if (pageNode.Attributes[schemaNode.Attributes[i].Name] != null && schemaNode.Attributes[i].Value.ToUpper() != "ID")
						{
							this.rtbPageInfo.SelectionColor = Color.Gray;
							this.rtbPageInfo.SelectedText = string.Concat(this.GetBookInfoLanguage(schemaNode.Attributes[i].Value), ": ");
							this.rtbPageInfo.SelectionColor = Color.Black;
							this.rtbPageInfo.SelectedText = string.Concat(pageNode.Attributes[schemaNode.Attributes[i].Name].Value, "\n");
						}
					}
					if (pageNode.HasChildNodes)
					{
						int num = 0;
						foreach (XmlNode childNode in pageNode.ChildNodes)
						{
							if (childNode.Name.ToUpper() != "PIC")
							{
								continue;
							}
							num++;
							this.rtbPageInfo.SelectionColor = Color.Gray;
							string str = "Picture";
							RichTextBox richTextBox1 = this.rtbPageInfo;
							object[] bookInfoLanguage = new object[] { "[", this.GetBookInfoLanguage(str), " ", num, "]\n" };
							richTextBox1.SelectedText = string.Concat(bookInfoLanguage);
							for (int j = 0; j < schemaNode.Attributes.Count; j++)
							{
								if (childNode.Attributes[schemaNode.Attributes[j].Name] != null && !schemaNode.Attributes[j].Value.ToUpper().StartsWith("FIELD"))
								{
									this.rtbPageInfo.SelectionColor = Color.Gray;
									this.rtbPageInfo.SelectedText = string.Concat(this.GetBookInfoLanguage(schemaNode.Attributes[j].Value), ":");
									this.rtbPageInfo.SelectionColor = Color.Black;
									string value = childNode.Attributes[schemaNode.Attributes[j].Name].Value;
									if (value.ToUpper().EndsWith(".DJVU") || value.ToUpper().EndsWith(".JPEG") || value.ToUpper().EndsWith(".TIF"))
									{
										value = value.Substring(0, value.IndexOf("."));
									}
									this.rtbPageInfo.SelectedText = string.Concat(value, "\n");
								}
							}
						}
					}
				}
				catch
				{
					this.rtbPageInfo.Clear();
					this.rtbPageInfo.SelectionColor = Color.Red;
					this.rtbPageInfo.SelectedText = this.GetResource("(E-INF-EM002) Failed to load specified object", "(E-INF-EM002)_FAILED", ResourceType.LABEL);
				}
			}
		}

		public void LoadResources()
		{
			this.Text = string.Concat(this.GetResource("Information", "INFORMATION", ResourceType.TITLE), "      ");
			this.lblBookInfo.Text = this.GetResource("Book Information", "BOOK_INFORMATION", ResourceType.LABEL);
			this.lblPageInfo.Text = this.GetResource("Page Information", "PAGE_INFORMATION", ResourceType.LABEL);
			this.LoadBookInfo();
			this.LoadPageInfo(this.schemaNodeParent, this.pageNodeParent);
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern bool LockWindowUpdate(IntPtr hWndLock);

		private void rtbBookInfo_MouseDown(object sender, MouseEventArgs e)
		{
			frmInfo.HideCaret(this.rtbBookInfo.Handle);
		}

		private void rtbPageInfo_MouseDown_1(object sender, MouseEventArgs e)
		{
			frmInfo.HideCaret(this.rtbPageInfo.Handle);
		}

		public void ShowLoading()
		{
			this.ShowLoading(this.pnlForm);
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				if (!this.pnlForm.InvokeRequired)
				{
					this.picLoading.Parent = parentPanel;
					this.picLoading.BringToFront();
					this.picLoading.Show();
				}
				else
				{
					frmInfo.ShowLoadingDelegate showLoadingDelegate = new frmInfo.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { this.pnlForm };
					base.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void ShowPanels(bool value)
		{
			if (!this.pnlForm.InvokeRequired)
			{
				this.pnlBookInfo.BringToFront();
				this.pnlBookInfo.Visible = value;
				this.pnlPageInfo.BringToFront();
				this.pnlPageInfo.Visible = value;
				return;
			}
			Panel panel = this.pnlForm;
			frmInfo.ShowPanelsDelegate showPanelsDelegate = new frmInfo.ShowPanelsDelegate(this.ShowPanels);
			object[] objArray = new object[] { value };
			panel.Invoke(showPanelsDelegate, objArray);
		}

		public void UpdateFont()
		{
			this.pnlForm.Font = Settings.Default.appFont;
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			frmInfo.HideCaret(this.rtbPageInfo.Handle);
			frmInfo.HideCaret(this.rtbBookInfo.Handle);
		}

		private delegate void HideLoadingDelegate(Panel parentPanel);

		private delegate void LoadBookInfoDelegate();

		private delegate void LoadPageInfoDelegate(XmlNode schemaNode, XmlNode pageNode);

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void ShowPanelsDelegate(bool value);
	}
}
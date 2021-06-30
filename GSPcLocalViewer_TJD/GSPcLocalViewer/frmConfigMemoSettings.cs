using GSPcLocalViewer.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmConfigMemoSettings : Form
	{
		private IContainer components;

		private Label lblMemo;

		private Panel pnlControl;

		private Button btnOK;

		private Button btnCancel;

		private Panel pnlForm;

		private FolderBrowserDialog dlgBrowseFolder;

		private Panel pnlGlobalMemo;

		private Panel pnlMemoBehaviour;

		private CheckBox chkPopupPictureMemo;

		private Panel pnlLocalMemo;

		private Button btnGlobalMemoFolderBrowse;

		private Label lblGlobalMemoFolder;

		private TextBox txtGlobalMemoFolder;

		private CheckBox chkEnableGlobalMemo;

		private CheckBox chkLocalMemoBackup;

		private CheckBox chkEnableLocalMemo;

		private Button btnLocalMemoBackupFileBrowse;

		private Label lblLocalMemoBackupFile;

		private TextBox txtLocalMemoBackupFile;

		private SaveFileDialog dlgSaveFile;

		private Label lblBehaviour;

		private Label lblDetails;

		private Panel pnlLblDetails;

		private Label lblDetailsLine;

		private Panel pnlLblBehaviour;

		private Label lblBehaviourLine;

		private Panel pnlAdminMemo;

		private CheckBox chkEnableAdminMemo;

		private frmConfig frmParent;

		public frmConfigMemoSettings(frmConfig frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			base.MdiParent = frm;
			this.UpdateFont();
			this.chkPopupPictureMemo.Checked = Settings.Default.PopupPictureMemo;
			this.chkEnableAdminMemo.Checked = Settings.Default.EnableAdminMemo;
			this.chkEnableGlobalMemo.Checked = Settings.Default.EnableGlobalMemo;
			this.chkEnableLocalMemo.Checked = Settings.Default.EnableLocalMemo;
			this.chkLocalMemoBackup.Checked = Settings.Default.LocalMemoBackup;
			this.txtGlobalMemoFolder.Text = Settings.Default.GlobalMemoFolder;
			this.txtGlobalMemoFolder.Enabled = this.chkEnableGlobalMemo.Checked;
			this.btnGlobalMemoFolderBrowse.Enabled = this.chkEnableGlobalMemo.Checked;
			this.txtLocalMemoBackupFile.Text = Settings.Default.LocalMemoBackupFile;
			this.chkLocalMemoBackup.Enabled = this.chkEnableLocalMemo.Checked;
			this.txtLocalMemoBackupFile.Enabled = (!this.chkLocalMemoBackup.Checked ? false : this.chkEnableLocalMemo.Checked);
			this.btnLocalMemoBackupFileBrowse.Enabled = (!this.chkLocalMemoBackup.Checked ? false : this.chkEnableLocalMemo.Checked);
			this.LoadResources();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.frmParent.Close();
		}

		private void btnGlobalMemoFolderBrowse_Click(object sender, EventArgs e)
		{
			this.dlgBrowseFolder.Description = this.GetResource("Select Global Memo Folder", "SELECT_FOLDER", ResourceType.LABEL);
			this.dlgBrowseFolder.ShowNewFolderButton = true;
			this.dlgBrowseFolder.RootFolder = Environment.SpecialFolder.Desktop;
			if (Directory.Exists(this.txtGlobalMemoFolder.Text))
			{
				this.dlgBrowseFolder.SelectedPath = this.txtGlobalMemoFolder.Text;
			}
			if (this.dlgBrowseFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.txtGlobalMemoFolder.Text = this.dlgBrowseFolder.SelectedPath;
			}
		}

		private void btnLocalMemoBackupFileBrowse_Click(object sender, EventArgs e)
		{
			if (!(this.txtLocalMemoBackupFile.Text.Trim() != string.Empty) || !Directory.Exists(Path.GetDirectoryName(this.txtLocalMemoBackupFile.Text)))
			{
				this.dlgSaveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			}
			else
			{
				this.dlgSaveFile.InitialDirectory = Path.GetDirectoryName(this.txtLocalMemoBackupFile.Text);
			}
			this.dlgSaveFile.Filter = "xml files (*.xml)|*.xml";
			this.dlgSaveFile.RestoreDirectory = false;
			if (this.dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.txtLocalMemoBackupFile.Text = this.dlgSaveFile.FileName;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.frmParent.CloseAndSaveSettings();
		}

		public bool CheckSettings()
		{
			bool flag;
			if (this.chkEnableGlobalMemo.Checked)
			{
				if (!Directory.Exists(this.txtGlobalMemoFolder.Text))
				{
					this.frmParent.objFrmTasks.lblMemosSettings_Click(null, null);
					MessageHandler.ShowWarning(this.GetResource("E-CMS-EM002) Specified information does not exist", "(E-CMS-EM002)_INFORMATION", ResourceType.POPUP_MESSAGE));
					return false;
				}
				try
				{
					FileStream fileStream = File.Create(string.Concat(this.txtGlobalMemoFolder.Text, "\\rights.txt"), 128, FileOptions.DeleteOnClose);
					fileStream.Close();
				}
				catch
				{
					this.frmParent.objFrmTasks.lblMemosSettings_Click(null, null);
					MessageHandler.ShowWarning(this.GetResource("(E-CMS-EM001) Failed to save specified object", "(E-CMS-EM001)_FAILED", ResourceType.POPUP_MESSAGE));
					flag = false;
					return flag;
				}
			}
			if (this.chkLocalMemoBackup.Checked)
			{
				if (!this.txtLocalMemoBackupFile.Text.Trim().ToUpper().EndsWith(".XML"))
				{
					this.frmParent.objFrmTasks.lblMemosSettings_Click(null, null);
					MessageHandler.ShowWarning(this.GetResource("(E-CMS-EM003) Not in required format", "(E-CMS-EM003)_FORMAT", ResourceType.POPUP_MESSAGE));
					return false;
				}
				if (!File.Exists(this.txtLocalMemoBackupFile.Text))
				{
					try
					{
						StreamWriter streamWriter = new StreamWriter(this.txtLocalMemoBackupFile.Text, false);
						streamWriter.WriteLine("<?xml version='1.0' encoding='utf-8'?>");
						streamWriter.WriteLine("<Memos/>");
						streamWriter.Close();
					}
					catch
					{
						this.frmParent.objFrmTasks.lblMemosSettings_Click(null, null);
						MessageHandler.ShowWarning(this.GetResource("(E-CMS-EM004) Failed to create file/folder specified", "(E-CMS-EM004)_FAILED", ResourceType.POPUP_MESSAGE));
						flag = false;
						return flag;
					}
				}
			}
			return true;
		}

		private void chkEnableGlobalMemo_CheckedChanged(object sender, EventArgs e)
		{
			this.txtGlobalMemoFolder.Enabled = this.chkEnableGlobalMemo.Checked;
			this.btnGlobalMemoFolderBrowse.Enabled = this.chkEnableGlobalMemo.Checked;
		}

		private void chkEnableLocalMemo_CheckedChanged(object sender, EventArgs e)
		{
			this.chkLocalMemoBackup.Enabled = this.chkEnableLocalMemo.Checked;
			this.txtLocalMemoBackupFile.Enabled = this.chkEnableLocalMemo.Checked;
			this.btnLocalMemoBackupFileBrowse.Enabled = this.chkEnableLocalMemo.Checked;
		}

		private void chkLocalMemoBackup_CheckedChanged(object sender, EventArgs e)
		{
			this.txtLocalMemoBackupFile.Enabled = this.chkLocalMemoBackup.Checked;
			this.btnLocalMemoBackupFileBrowse.Enabled = this.chkLocalMemoBackup.Checked;
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
				str = string.Concat(str, "/Screen[@Name='CONFIGURATION']");
				str = string.Concat(str, "/Screen[@Name='MEMO']");
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
			this.lblMemo = new Label();
			this.pnlControl = new Panel();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlForm = new Panel();
			this.pnlLocalMemo = new Panel();
			this.btnLocalMemoBackupFileBrowse = new Button();
			this.chkLocalMemoBackup = new CheckBox();
			this.lblLocalMemoBackupFile = new Label();
			this.chkEnableLocalMemo = new CheckBox();
			this.txtLocalMemoBackupFile = new TextBox();
			this.pnlGlobalMemo = new Panel();
			this.pnlLblDetails = new Panel();
			this.lblDetailsLine = new Label();
			this.lblDetails = new Label();
			this.btnGlobalMemoFolderBrowse = new Button();
			this.lblGlobalMemoFolder = new Label();
			this.txtGlobalMemoFolder = new TextBox();
			this.chkEnableGlobalMemo = new CheckBox();
			this.pnlMemoBehaviour = new Panel();
			this.pnlLblBehaviour = new Panel();
			this.lblBehaviourLine = new Label();
			this.lblBehaviour = new Label();
			this.chkPopupPictureMemo = new CheckBox();
			this.dlgBrowseFolder = new FolderBrowserDialog();
			this.dlgSaveFile = new SaveFileDialog();
			this.pnlAdminMemo = new Panel();
			this.chkEnableAdminMemo = new CheckBox();
			this.pnlControl.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.pnlLocalMemo.SuspendLayout();
			this.pnlGlobalMemo.SuspendLayout();
			this.pnlLblDetails.SuspendLayout();
			this.pnlMemoBehaviour.SuspendLayout();
			this.pnlLblBehaviour.SuspendLayout();
			this.pnlAdminMemo.SuspendLayout();
			base.SuspendLayout();
			this.lblMemo.BackColor = Color.White;
			this.lblMemo.Dock = DockStyle.Top;
			this.lblMemo.ForeColor = Color.Black;
			this.lblMemo.Location = new Point(0, 0);
			this.lblMemo.Name = "lblMemo";
			this.lblMemo.Padding = new System.Windows.Forms.Padding(3, 7, 0, 0);
			this.lblMemo.Size = new System.Drawing.Size(448, 27);
			this.lblMemo.TabIndex = 16;
			this.lblMemo.Text = "Memo";
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Bottom;
			this.pnlControl.Location = new Point(0, 417);
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
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlAdminMemo);
			this.pnlForm.Controls.Add(this.pnlLocalMemo);
			this.pnlForm.Controls.Add(this.pnlGlobalMemo);
			this.pnlForm.Controls.Add(this.pnlMemoBehaviour);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Controls.Add(this.lblMemo);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(0, 0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(450, 450);
			this.pnlForm.TabIndex = 18;
			this.pnlLocalMemo.Controls.Add(this.btnLocalMemoBackupFileBrowse);
			this.pnlLocalMemo.Controls.Add(this.chkLocalMemoBackup);
			this.pnlLocalMemo.Controls.Add(this.lblLocalMemoBackupFile);
			this.pnlLocalMemo.Controls.Add(this.chkEnableLocalMemo);
			this.pnlLocalMemo.Controls.Add(this.txtLocalMemoBackupFile);
			this.pnlLocalMemo.Dock = DockStyle.Top;
			this.pnlLocalMemo.Location = new Point(0, 215);
			this.pnlLocalMemo.Name = "pnlLocalMemo";
			this.pnlLocalMemo.Size = new System.Drawing.Size(448, 108);
			this.pnlLocalMemo.TabIndex = 27;
			this.btnLocalMemoBackupFileBrowse.Location = new Point(358, 68);
			this.btnLocalMemoBackupFileBrowse.Name = "btnLocalMemoBackupFileBrowse";
			this.btnLocalMemoBackupFileBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnLocalMemoBackupFileBrowse.TabIndex = 5;
			this.btnLocalMemoBackupFileBrowse.Text = "Browse";
			this.btnLocalMemoBackupFileBrowse.UseVisualStyleBackColor = true;
			this.btnLocalMemoBackupFileBrowse.Click += new EventHandler(this.btnLocalMemoBackupFileBrowse_Click);
			this.chkLocalMemoBackup.AutoSize = true;
			this.chkLocalMemoBackup.Location = new Point(20, 33);
			this.chkLocalMemoBackup.Name = "chkLocalMemoBackup";
			this.chkLocalMemoBackup.Size = new System.Drawing.Size(162, 17);
			this.chkLocalMemoBackup.TabIndex = 3;
			this.chkLocalMemoBackup.Text = "Take Backup of Local Memos";
			this.chkLocalMemoBackup.UseVisualStyleBackColor = true;
			this.chkLocalMemoBackup.CheckedChanged += new EventHandler(this.chkLocalMemoBackup_CheckedChanged);
			this.lblLocalMemoBackupFile.AutoSize = true;
			this.lblLocalMemoBackupFile.Location = new Point(20, 53);
			this.lblLocalMemoBackupFile.Name = "lblLocalMemoBackupFile";
			this.lblLocalMemoBackupFile.Size = new System.Drawing.Size(118, 13);
			this.lblLocalMemoBackupFile.TabIndex = 4;
			this.lblLocalMemoBackupFile.Text = "Local Memo Backup File";
			this.chkEnableLocalMemo.AutoSize = true;
			this.chkEnableLocalMemo.Location = new Point(20, 10);
			this.chkEnableLocalMemo.Name = "chkEnableLocalMemo";
			this.chkEnableLocalMemo.Size = new System.Drawing.Size(121, 17);
			this.chkEnableLocalMemo.TabIndex = 3;
			this.chkEnableLocalMemo.Text = "Enable Local Memos";
			this.chkEnableLocalMemo.UseVisualStyleBackColor = true;
			this.chkEnableLocalMemo.CheckedChanged += new EventHandler(this.chkEnableLocalMemo_CheckedChanged);
			this.txtLocalMemoBackupFile.BorderStyle = BorderStyle.FixedSingle;
			this.txtLocalMemoBackupFile.Location = new Point(20, 69);
			this.txtLocalMemoBackupFile.Name = "txtLocalMemoBackupFile";
			this.txtLocalMemoBackupFile.Size = new System.Drawing.Size(337, 21);
			this.txtLocalMemoBackupFile.TabIndex = 3;
			this.pnlGlobalMemo.Controls.Add(this.pnlLblDetails);
			this.pnlGlobalMemo.Controls.Add(this.btnGlobalMemoFolderBrowse);
			this.pnlGlobalMemo.Controls.Add(this.lblGlobalMemoFolder);
			this.pnlGlobalMemo.Controls.Add(this.txtGlobalMemoFolder);
			this.pnlGlobalMemo.Controls.Add(this.chkEnableGlobalMemo);
			this.pnlGlobalMemo.Dock = DockStyle.Top;
			this.pnlGlobalMemo.Location = new Point(0, 102);
			this.pnlGlobalMemo.Name = "pnlGlobalMemo";
			this.pnlGlobalMemo.Size = new System.Drawing.Size(448, 113);
			this.pnlGlobalMemo.TabIndex = 26;
			this.pnlLblDetails.Controls.Add(this.lblDetailsLine);
			this.pnlLblDetails.Controls.Add(this.lblDetails);
			this.pnlLblDetails.Dock = DockStyle.Top;
			this.pnlLblDetails.Location = new Point(0, 0);
			this.pnlLblDetails.Name = "pnlLblDetails";
			this.pnlLblDetails.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblDetails.Size = new System.Drawing.Size(448, 28);
			this.pnlLblDetails.TabIndex = 18;
			this.lblDetailsLine.BackColor = Color.Transparent;
			this.lblDetailsLine.Dock = DockStyle.Fill;
			this.lblDetailsLine.ForeColor = Color.Blue;
			this.lblDetailsLine.Image = Resources.GroupLine0;
			this.lblDetailsLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblDetailsLine.Location = new Point(82, 0);
			this.lblDetailsLine.Name = "lblDetailsLine";
			this.lblDetailsLine.Size = new System.Drawing.Size(351, 28);
			this.lblDetailsLine.TabIndex = 15;
			this.lblDetailsLine.Tag = "";
			this.lblDetailsLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblDetails.BackColor = Color.Transparent;
			this.lblDetails.Dock = DockStyle.Left;
			this.lblDetails.ForeColor = Color.Blue;
			this.lblDetails.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblDetails.Location = new Point(7, 0);
			this.lblDetails.Name = "lblDetails";
			this.lblDetails.Size = new System.Drawing.Size(75, 28);
			this.lblDetails.TabIndex = 14;
			this.lblDetails.Tag = "";
			this.lblDetails.Text = "Details";
			this.lblDetails.TextAlign = ContentAlignment.MiddleLeft;
			this.btnGlobalMemoFolderBrowse.Location = new Point(358, 71);
			this.btnGlobalMemoFolderBrowse.Name = "btnGlobalMemoFolderBrowse";
			this.btnGlobalMemoFolderBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnGlobalMemoFolderBrowse.TabIndex = 5;
			this.btnGlobalMemoFolderBrowse.Text = "Browse";
			this.btnGlobalMemoFolderBrowse.UseVisualStyleBackColor = true;
			this.btnGlobalMemoFolderBrowse.Click += new EventHandler(this.btnGlobalMemoFolderBrowse_Click);
			this.lblGlobalMemoFolder.AutoSize = true;
			this.lblGlobalMemoFolder.Location = new Point(20, 56);
			this.lblGlobalMemoFolder.Name = "lblGlobalMemoFolder";
			this.lblGlobalMemoFolder.Size = new System.Drawing.Size(100, 13);
			this.lblGlobalMemoFolder.TabIndex = 4;
			this.lblGlobalMemoFolder.Text = "Global Memo Folder";
			this.txtGlobalMemoFolder.BorderStyle = BorderStyle.FixedSingle;
			this.txtGlobalMemoFolder.Location = new Point(20, 72);
			this.txtGlobalMemoFolder.Name = "txtGlobalMemoFolder";
			this.txtGlobalMemoFolder.Size = new System.Drawing.Size(337, 21);
			this.txtGlobalMemoFolder.TabIndex = 3;
			this.chkEnableGlobalMemo.AutoSize = true;
			this.chkEnableGlobalMemo.Location = new Point(20, 36);
			this.chkEnableGlobalMemo.Name = "chkEnableGlobalMemo";
			this.chkEnableGlobalMemo.Size = new System.Drawing.Size(126, 17);
			this.chkEnableGlobalMemo.TabIndex = 2;
			this.chkEnableGlobalMemo.Text = "Enable Global Memos";
			this.chkEnableGlobalMemo.UseVisualStyleBackColor = true;
			this.chkEnableGlobalMemo.CheckedChanged += new EventHandler(this.chkEnableGlobalMemo_CheckedChanged);
			this.pnlMemoBehaviour.Controls.Add(this.pnlLblBehaviour);
			this.pnlMemoBehaviour.Controls.Add(this.chkPopupPictureMemo);
			this.pnlMemoBehaviour.Dock = DockStyle.Top;
			this.pnlMemoBehaviour.Location = new Point(0, 27);
			this.pnlMemoBehaviour.Name = "pnlMemoBehaviour";
			this.pnlMemoBehaviour.Size = new System.Drawing.Size(448, 75);
			this.pnlMemoBehaviour.TabIndex = 24;
			this.pnlLblBehaviour.Controls.Add(this.lblBehaviourLine);
			this.pnlLblBehaviour.Controls.Add(this.lblBehaviour);
			this.pnlLblBehaviour.Dock = DockStyle.Top;
			this.pnlLblBehaviour.Location = new Point(0, 0);
			this.pnlLblBehaviour.Name = "pnlLblBehaviour";
			this.pnlLblBehaviour.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlLblBehaviour.Size = new System.Drawing.Size(448, 28);
			this.pnlLblBehaviour.TabIndex = 18;
			this.lblBehaviourLine.BackColor = Color.Transparent;
			this.lblBehaviourLine.Dock = DockStyle.Fill;
			this.lblBehaviourLine.ForeColor = Color.Blue;
			this.lblBehaviourLine.Image = Resources.GroupLine0;
			this.lblBehaviourLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblBehaviourLine.Location = new Point(82, 0);
			this.lblBehaviourLine.Name = "lblBehaviourLine";
			this.lblBehaviourLine.Size = new System.Drawing.Size(351, 28);
			this.lblBehaviourLine.TabIndex = 15;
			this.lblBehaviourLine.Tag = "";
			this.lblBehaviourLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblBehaviour.BackColor = Color.Transparent;
			this.lblBehaviour.Dock = DockStyle.Left;
			this.lblBehaviour.ForeColor = Color.Blue;
			this.lblBehaviour.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblBehaviour.Location = new Point(7, 0);
			this.lblBehaviour.Name = "lblBehaviour";
			this.lblBehaviour.Size = new System.Drawing.Size(75, 28);
			this.lblBehaviour.TabIndex = 12;
			this.lblBehaviour.Tag = "";
			this.lblBehaviour.Text = "Behaviour";
			this.lblBehaviour.TextAlign = ContentAlignment.MiddleLeft;
			this.chkPopupPictureMemo.AutoSize = true;
			this.chkPopupPictureMemo.Checked = true;
			this.chkPopupPictureMemo.CheckState = CheckState.Checked;
			this.chkPopupPictureMemo.Location = new Point(20, 36);
			this.chkPopupPictureMemo.Name = "chkPopupPictureMemo";
			this.chkPopupPictureMemo.Size = new System.Drawing.Size(131, 17);
			this.chkPopupPictureMemo.TabIndex = 1;
			this.chkPopupPictureMemo.Text = "Popup Picture Memos ";
			this.chkPopupPictureMemo.UseVisualStyleBackColor = true;
			this.pnlAdminMemo.Controls.Add(this.chkEnableAdminMemo);
			this.pnlAdminMemo.Dock = DockStyle.Top;
			this.pnlAdminMemo.Location = new Point(0, 323);
			this.pnlAdminMemo.Name = "pnlAdminMemo";
			this.pnlAdminMemo.Size = new System.Drawing.Size(448, 32);
			this.pnlAdminMemo.TabIndex = 28;
			this.chkEnableAdminMemo.AutoSize = true;
			this.chkEnableAdminMemo.Location = new Point(20, 6);
			this.chkEnableAdminMemo.Name = "chkEnableAdminMemo";
			this.chkEnableAdminMemo.Size = new System.Drawing.Size(126, 17);
			this.chkEnableAdminMemo.TabIndex = 1;
			this.chkEnableAdminMemo.Text = "Enable Admin Memos";
			this.chkEnableAdminMemo.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = Color.White;
			base.ClientSize = new System.Drawing.Size(450, 450);
			base.Controls.Add(this.pnlForm);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 178);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "frmConfigMemoSettings";
			this.pnlControl.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnlLocalMemo.ResumeLayout(false);
			this.pnlLocalMemo.PerformLayout();
			this.pnlGlobalMemo.ResumeLayout(false);
			this.pnlGlobalMemo.PerformLayout();
			this.pnlLblDetails.ResumeLayout(false);
			this.pnlMemoBehaviour.ResumeLayout(false);
			this.pnlMemoBehaviour.PerformLayout();
			this.pnlLblBehaviour.ResumeLayout(false);
			this.pnlAdminMemo.ResumeLayout(false);
			this.pnlAdminMemo.PerformLayout();
			base.ResumeLayout(false);
		}

		private void LoadResources()
		{
			this.lblMemo.Text = this.GetResource("Memo", "MEMO", ResourceType.LABEL);
			this.lblBehaviour.Text = this.GetResource("Behaviour", "BEHAVIOUR", ResourceType.LABEL);
			this.chkPopupPictureMemo.Text = this.GetResource("Popup Picture Memos", "POPUP_PICTURE_MEMOS", ResourceType.CHECK_BOX);
			this.pnlAdminMemo.Text = this.GetResource("Bookmarks Information", "BOOKMARKS_INFORMATION", ResourceType.LABEL);
			this.lblDetails.Text = this.GetResource("Details", "DETAIL", ResourceType.LABEL);
			this.chkEnableGlobalMemo.Text = this.GetResource("Enable Global Memos", "ENABLE_GLOBAL_MEMOS", ResourceType.CHECK_BOX);
			this.lblGlobalMemoFolder.Text = this.GetResource("Global Memo Folder", "GLOBAL_MEMO", ResourceType.LABEL);
			this.btnGlobalMemoFolderBrowse.Text = this.GetResource("Browse", "BROWSEGLOBAL", ResourceType.BUTTON);
			this.chkEnableLocalMemo.Text = this.GetResource("Enable Local Memos", "ENABLE_LOCAL_MEMOS", ResourceType.CHECK_BOX);
			this.chkLocalMemoBackup.Text = this.GetResource("Take Backup of Local Memos", "TAKE_BACKUP_OF_LOCAL_MEMOS", ResourceType.CHECK_BOX);
			this.lblLocalMemoBackupFile.Text = this.GetResource("Local Memo Backup File", "LOCAL_MEMO_BACKUP_FILE", ResourceType.LABEL);
			this.btnLocalMemoBackupFileBrowse.Text = this.GetResource("Browse", "BROWSELOCAL", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
			this.chkEnableAdminMemo.Text = this.GetResource("Enable Administrator Memos", "ENABLE_ADMINISTRATOR_MEMOS", ResourceType.CHECK_BOX);
		}

		public void SaveSettings()
		{
			Settings.Default.PopupPictureMemo = this.chkPopupPictureMemo.Checked;
			Settings.Default.EnableAdminMemo = this.chkEnableAdminMemo.Checked;
			if (this.chkEnableGlobalMemo.Checked && this.txtGlobalMemoFolder.Text.Trim().ToUpper() != Settings.Default.GlobalMemoFolder.Trim().ToUpper())
			{
				this.frmParent.ChangeGlobalMemoPath(Settings.Default.GlobalMemoFolder, this.txtGlobalMemoFolder.Text);
			}
			Settings.Default.EnableGlobalMemo = this.chkEnableGlobalMemo.Checked;
			Settings.Default.GlobalMemoFolder = this.txtGlobalMemoFolder.Text;
			Settings.Default.EnableLocalMemo = this.chkEnableLocalMemo.Checked;
			Settings.Default.LocalMemoBackup = this.chkLocalMemoBackup.Checked;
			Settings.Default.LocalMemoBackupFile = this.txtLocalMemoBackupFile.Text;
		}

		private void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
			this.lblMemo.Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold, this.Font.Unit, this.Font.GdiCharSet, this.Font.GdiVerticalFont);
		}
	}
}
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmDataSize : Form
	{
		private IContainer components;

		private Panel pnlForm;

		private PictureBox picLoading;

		private Panel pnlContents;

		private BackgroundWorker bgWorker;

		private ColumnHeader columnHeader1;

		private ColumnHeader columnHeader2;

		private ColumnHeader columnHeader3;

		private ColumnHeader columnHeader4;

		private ColumnHeader columnHeader5;

		private ColumnHeader columnHeader6;

		private TreeListView tlvFolders;

		private ColumnHeader colName;

		private ColumnHeader colSize;

		private ColumnHeader colDate;

		private ImageList ilTreeList;

		private ToolStrip toolStrip1;

		private ToolStripButton tsbClearSelection;

		private ToolStripButton tsbSelectAll;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton tsBtnDeleteSelection;

		private Panel pnlDiskSpace;

		private Panel pnlControl;

		private Button btnCancel;

		private Panel pnlDownloadedDataHeader;

		private Label lblDownloadedDataLine;

		private Label lblDownloadedDataHeader;

		private Label lblStatus;

		private Panel pnlDiskSpaceHeader;

		private Label lblDiskSpaceHeaderLine;

		private Label lblDiskSpaceHeader;

		private Panel pnlDiskSpace1;

		private Panel pnlDiskSpaceValue;

		private NumericTextBox txtDiskSpaceValue;

		private ComboBox cmbDiskSpaceValue;

		private CheckBox chkDiskSpace;

		private StatusStrip ssStatus;

		private ToolStripStatusLabel toolStripStatusLabel1;

		private ToolStripStatusLabel tsStatus;

		private Panel pnlSplitter1;

		private Button btnOK;

		private Panel pnltlvFolders;

		private Panel pnlDiskSpaceAndControl;

		private BackgroundWorker bgChangeDataSize;

		private Label lblInUseBooks;

		private ColumnHeader colStatus;

		private frmViewer frmParent;

		private bool currentlyLoadingData;

		private bool currentlyCalculatingSpace;

		private long stAllocatedSize;

		private long stFolderSize;

		private long stFreeSpace;

		private string sBookInUseMsg;

		private Thread thCalculateSpace;

		public frmDataSize(frmViewer frm)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.sBookInUseMsg = string.Empty;
			this.stAllocatedSize = (long)0;
			this.stFolderSize = (long)0;
			this.stFreeSpace = (long)0;
			this.currentlyLoadingData = true;
			this.currentlyCalculatingSpace = false;
			this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
			this.UpdateFont();
			this.LoadResources();
			this.tsStatus.Text = string.Empty;
			this.cmbDiskSpaceValue.SelectedItem = "GB";
			this.chkDiskSpace.Checked = false;
			this.pnlDiskSpaceValue.Visible = false;
			this.tlvFolders.HighlightBackColor = Settings.Default.appHighlightBackColor;
			this.tlvFolders.HighlightForeColor = Settings.Default.appHighlightForeColor;
		}

		private void bgChangeDataSize_DoWork(object sender, DoWorkEventArgs e)
		{
			long argument = (long)((object[])e.Argument)[0];
			if (argument <= (long)10485760 && argument != (long)0)
			{
				e.Result = new Global.OkMsg(false, this.GetResource("Allocated data size should be greater than 10MB", "ALLOCATE_MORE_SPACE", ResourceType.POPUP_MESSAGE));
				return;
			}
			if (argument == (long)0)
			{
				Program.iniGSPcLocal.UpdateItem("SETTINGS", "DATA_SIZE", string.Empty);
				e.Result = new Global.OkMsg(true, string.Empty);
				return;
			}
			if (Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"] == null)
			{
				e.Result = new Global.OkMsg(false, this.GetResource("Data path does not exist", "NO_PATH", ResourceType.POPUP_MESSAGE));
				return;
			}
			this.ShowLoading(this.pnlDiskSpaceAndControl);
			string text = this.tsStatus.Text;
			this.UpdateStatus(this.GetResource("Processing disk space...", "PROCESSING_DISK_SPACE", ResourceType.STATUS_MESSAGE));
			string item = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
			string str = item.Substring(0, 1);
			DirectoryInfo directoryInfo = new DirectoryInfo(item);
			long freeSpace = DataSize.GetFreeSpace(str);
			long dirSize = freeSpace + DataSize.GetDirSize(directoryInfo);
			if (argument <= dirSize)
			{
				string str1 = DataSize.FormattedSize(argument);
				Program.iniGSPcLocal.UpdateItem("SETTINGS", "DATA_SIZE", str1);
				e.Result = new Global.OkMsg(true, string.Empty);
			}
			else
			{
				string str2 = DataSize.FormattedSize(dirSize);
				e.Result = new Global.OkMsg(false, string.Concat(this.GetResource("Allocated data size should be less than available space", "ALLOCATE_LESS", ResourceType.POPUP_MESSAGE), " [", str2, "]"));
			}
			this.UpdateStatus(text);
		}

		private void bgChangeDataSize_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Global.OkMsg result;
			MessageBoxIcon messageBoxIcon;
			try
			{
				result = (Global.OkMsg)e.Result;
			}
			catch
			{
				result = new Global.OkMsg(false, "Err");
			}
			this.HideLoading(this.pnlDiskSpaceAndControl);
			messageBoxIcon = (!result.ok ? MessageBoxIcon.Hand : MessageBoxIcon.Asterisk);
			if (result.msg.Trim() != string.Empty)
			{
				MessageHandler.ShowMessage(this, result.msg, this.Text, MessageBoxButtons.OK, messageBoxIcon);
			}
			if (result.ok)
			{
				DataSize.ReInitialize();
				this.frmParent.DisposeNotification();
				if (this.chkDiskSpace.Checked)
				{
					this.frmParent.RunDataSizeChecking();
				}
				base.Close();
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			ListViewItem.ListViewSubItem listViewSubItem;
			this.currentlyLoadingData = true;
			string empty = string.Empty;
			empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
			try
			{
				string[] directories = Directory.GetDirectories(empty);
				for (int i = 0; i < (int)directories.Length; i++)
				{
					string str = directories[i];
					try
					{
						TreeListViewItem treeListViewItem = new TreeListViewItem()
						{
							Tag = str,
							Text = (new DirectoryInfo(str)).Name
						};
						treeListViewItem.Expand();
						treeListViewItem.ImageIndex = 2;
						string[] strArrays = Directory.GetDirectories(str);
						ArrayList arrayLists = this.frmParent.ListOfInUseBooks();
						string[] strArrays1 = strArrays;
						for (int j = 0; j < (int)strArrays1.Length; j++)
						{
							string str1 = strArrays1[j];
							TreeListViewItem name = new TreeListViewItem()
							{
								Tag = str1
							};
							DirectoryInfo directoryInfo = new DirectoryInfo(str1);
							name.Text = directoryInfo.Name;
							name.ImageIndex = 3;
							treeListViewItem.Items.Add(name);
							if (!arrayLists.Contains(directoryInfo.Name))
							{
								listViewSubItem = name.SubItems.Add(string.Empty);
							}
							else
							{
								listViewSubItem = name.SubItems.Add(this.sBookInUseMsg);
								name.Checked = false;
							}
							listViewSubItem.Name = "Status";
							DirectoryInfo directoryInfo1 = new DirectoryInfo(str1);
							string str2 = DataSize.FormattedSize(DataSize.GetDirSize(directoryInfo1));
							name.SubItems.Add(str2).Name = "Size";
							string str3 = directoryInfo1.LastWriteTime.ToString();
							name.SubItems.Add(str3).Name = "Date";
						}
						if (treeListViewItem.ChildrenCount > 0)
						{
							this.TreeListAddItem(treeListViewItem);
						}
					}
					catch
					{
					}
				}
			}
			catch (Exception exception)
			{
				this.SetMessage(exception.Message);
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.HideLoading(this.pnltlvFolders);
			this.toolStrip1.Enabled = true;
			this.currentlyLoadingData = false;
			if (this.tlvFolders.Items.Count == 0)
			{
				this.lblStatus.Text = this.GetResource("No books exist", "NO_BOOKS_EXIST", ResourceType.STATUS_MESSAGE);
				this.lblStatus.BringToFront();
				this.tlvFolders.Enabled = false;
				this.tsBtnDeleteSelection.Enabled = false;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			string empty = string.Empty;
			if (this.chkDiskSpace.Checked)
			{
				try
				{
					empty = this.txtDiskSpaceValue.Text;
					empty = string.Concat(empty, this.cmbDiskSpaceValue.SelectedItem.ToString());
				}
				catch
				{
					empty = string.Empty;
				}
			}
			long dataSizeLong = DataSize.GetDataSizeLong();
			long num = DataSize.FormattedSize(empty);
			string str = string.Empty;
			if (this.GetStatusText().Contains(this.GetResource("Overflow", "OVERFLOW", ResourceType.LABEL)))
			{
				MessageBox.Show(this, this.GetResource("Allocated data size should be greater than existing data space", "ALLOCATE_MORE", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (dataSizeLong == num)
			{
				base.Close();
				return;
			}
			this.bgChangeDataSize.RunWorkerAsync(new object[] { num });
		}

		private void CalculateRemainingSpaceStatus()
		{
			this.stAllocatedSize = (long)0;
			this.stFolderSize = (long)0;
			this.stFreeSpace = (long)0;
			string empty = string.Empty;
			this.currentlyCalculatingSpace = true;
			this.UpdateStatus(this.GetResource("Processing disk space...", "PROCESSING_DISK_SPACE", ResourceType.STATUS_MESSAGE));
			if (!this.chkDiskSpace.Checked)
			{
				try
				{
					empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
					empty = empty.Substring(0, 1);
					this.stFreeSpace = DataSize.GetFreeSpace(empty);
				}
				catch
				{
				}
				try
				{
					empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
					if (empty != null && Directory.Exists(empty))
					{
						this.stFolderSize = DataSize.GetDirSize(new DirectoryInfo(empty));
					}
				}
				catch
				{
				}
				try
				{
					this.stAllocatedSize = this.stFreeSpace + this.stFolderSize;
				}
				catch
				{
				}
			}
			else
			{
				try
				{
					empty = this.GetTxtDiskSpace();
					empty = string.Concat(empty, this.GetCmbDiskSpace());
					this.stAllocatedSize = DataSize.FormattedSize(empty);
				}
				catch
				{
				}
				try
				{
					empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
					if (empty != null && Directory.Exists(empty))
					{
						this.stFolderSize = DataSize.GetDirSize(new DirectoryInfo(empty));
					}
				}
				catch
				{
				}
				try
				{
					this.stFreeSpace = this.stAllocatedSize - this.stFolderSize;
				}
				catch
				{
				}
			}
			while (this.currentlyLoadingData)
			{
				Thread.Sleep(1);
			}
			this.SetRemainingSpaceStatus();
			this.currentlyCalculatingSpace = false;
		}

		private void chkDiskSpace_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.chkDiskSpace.Checked)
			{
				this.pnlDiskSpaceValue.Visible = false;
			}
			else
			{
				string empty = string.Empty;
				if (Directory.Exists(Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"]))
				{
					this.pnlDiskSpaceValue.Visible = true;
				}
				else
				{
					MessageHandler.ShowMessage(this, this.GetResource("Data path does not exist", "NO_PATH", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					this.chkDiskSpace.Checked = false;
				}
			}
			if (this.thCalculateSpace.IsAlive)
			{
				this.thCalculateSpace.Abort();
			}
			while (this.thCalculateSpace.IsAlive)
			{
			}
			this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
			this.thCalculateSpace.Start();
		}

		private void cmbDiskSpaceValue_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.thCalculateSpace.IsAlive)
			{
				this.thCalculateSpace.Abort();
			}
			while (this.thCalculateSpace.IsAlive)
			{
			}
			this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
			this.thCalculateSpace.Start();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void frmDataSize_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.SaveUserSettings();
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			if (base.Owner.GetType() == typeof(frmViewer))
			{
				this.frmParent.HideDimmer();
			}
		}

		private void frmDataSize_Load(object sender, EventArgs e)
		{
			this.LoadUserSettings();
			string empty = string.Empty;
			empty = Program.iniGSPcLocal.items["SETTINGS", "DATA_DIRECTORY"];
			if (!Directory.Exists(empty))
			{
				this.lblStatus.Text = this.GetResource("Data path does not exist", "NO_PATH", ResourceType.STATUS_MESSAGE);
				this.lblStatus.BringToFront();
				this.tlvFolders.Enabled = false;
				this.tsBtnDeleteSelection.Enabled = false;
				return;
			}
			if ((int)Directory.GetDirectories(empty).Length <= 0)
			{
				this.lblStatus.Text = this.GetResource("No books exist", "NO_BOOKS_EXIST", ResourceType.STATUS_MESSAGE);
				this.lblStatus.BringToFront();
				this.tlvFolders.Enabled = false;
				this.tsBtnDeleteSelection.Enabled = false;
				return;
			}
			if (this.thCalculateSpace.IsAlive)
			{
				this.thCalculateSpace.Abort();
			}
			while (this.thCalculateSpace.IsAlive)
			{
			}
			this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
			this.thCalculateSpace.Start();
			this.toolStrip1.Enabled = false;
			this.ShowLoading(this.pnltlvFolders);
			this.bgWorker.RunWorkerAsync();
			if (!DataSize.IsDataSizeApplied())
			{
				this.chkDiskSpace.Checked = false;
				this.pnlDiskSpaceValue.Visible = false;
				return;
			}
			this.chkDiskSpace.Checked = true;
			this.pnlDiskSpaceValue.Visible = true;
			string dataSizeString = DataSize.GetDataSizeString();
			this.txtDiskSpaceValue.Text = DataSize.ExtractNumbers(dataSizeString, true);
			this.cmbDiskSpaceValue.SelectedItem = DataSize.ExtractAlphabets(dataSizeString, false).ToUpper().Trim();
		}

		private string GetCmbDiskSpace()
		{
			if (this.cmbDiskSpaceValue.InvokeRequired)
			{
				return (string)this.cmbDiskSpaceValue.Invoke(new frmDataSize.GetCmbDiskSpaceDelegate(this.GetCmbDiskSpace));
			}
			if (this.cmbDiskSpaceValue.SelectedItem == null)
			{
				return string.Empty;
			}
			return this.cmbDiskSpaceValue.SelectedItem.ToString();
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='DATA_SIZE']");
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
					else if (rType == ResourceType.TREE_VIEW)
					{
						str = string.Concat(str, "/Resources[@Name='TREE_VIEW']");
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

		public string GetStatusText()
		{
			if (!this.ssStatus.InvokeRequired)
			{
				return this.tsStatus.Text;
			}
			return (string)this.ssStatus.Invoke(new frmDataSize.GetStatusTextDelegate(this.GetStatusText));
		}

		private string GetTxtDiskSpace()
		{
			if (!this.cmbDiskSpaceValue.InvokeRequired)
			{
				return this.txtDiskSpaceValue.Text.Trim();
			}
			return (string)this.txtDiskSpaceValue.Invoke(new frmDataSize.GetTxtDiskSpaceDelegate(this.GetTxtDiskSpace));
		}

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				if (!parentPanel.InvokeRequired)
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
					this.picLoading.Size = new System.Drawing.Size(32, 32);
					this.picLoading.Parent = this.pnlForm;
				}
				else
				{
					frmDataSize.HideLoadingDelegate hideLoadingDelegate = new frmDataSize.HideLoadingDelegate(this.HideLoading);
					object[] objArray = new object[] { parentPanel };
					parentPanel.Invoke(hideLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			TreeListViewItemCollection.TreeListViewItemCollectionComparer treeListViewItemCollectionComparer = new TreeListViewItemCollection.TreeListViewItemCollectionComparer();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmDataSize));
			this.pnlForm = new Panel();
			this.pnlContents = new Panel();
			this.pnltlvFolders = new Panel();
			this.tlvFolders = new TreeListView();
			this.colName = new ColumnHeader();
			this.colStatus = new ColumnHeader();
			this.colSize = new ColumnHeader();
			this.colDate = new ColumnHeader();
			this.ilTreeList = new ImageList(this.components);
			this.lblStatus = new Label();
			this.pnlDownloadedDataHeader = new Panel();
			this.lblDownloadedDataLine = new Label();
			this.lblDownloadedDataHeader = new Label();
			this.picLoading = new PictureBox();
			this.pnlDiskSpaceAndControl = new Panel();
			this.pnlControl = new Panel();
			this.lblInUseBooks = new Label();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			this.pnlDiskSpace = new Panel();
			this.pnlDiskSpace1 = new Panel();
			this.pnlDiskSpaceValue = new Panel();
			this.txtDiskSpaceValue = new NumericTextBox();
			this.cmbDiskSpaceValue = new ComboBox();
			this.pnlSplitter1 = new Panel();
			this.chkDiskSpace = new CheckBox();
			this.pnlDiskSpaceHeader = new Panel();
			this.lblDiskSpaceHeaderLine = new Label();
			this.lblDiskSpaceHeader = new Label();
			this.bgWorker = new BackgroundWorker();
			this.columnHeader1 = new ColumnHeader();
			this.columnHeader2 = new ColumnHeader();
			this.columnHeader3 = new ColumnHeader();
			this.columnHeader4 = new ColumnHeader();
			this.columnHeader5 = new ColumnHeader();
			this.columnHeader6 = new ColumnHeader();
			this.toolStrip1 = new ToolStrip();
			this.tsbClearSelection = new ToolStripButton();
			this.tsbSelectAll = new ToolStripButton();
			this.toolStripSeparator4 = new ToolStripSeparator();
			this.tsBtnDeleteSelection = new ToolStripButton();
			this.ssStatus = new StatusStrip();
			this.toolStripStatusLabel1 = new ToolStripStatusLabel();
			this.tsStatus = new ToolStripStatusLabel();
			this.bgChangeDataSize = new BackgroundWorker();
			this.pnlForm.SuspendLayout();
			this.pnlContents.SuspendLayout();
			this.pnltlvFolders.SuspendLayout();
			this.pnlDownloadedDataHeader.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			this.pnlDiskSpaceAndControl.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.pnlDiskSpace.SuspendLayout();
			this.pnlDiskSpace1.SuspendLayout();
			this.pnlDiskSpaceValue.SuspendLayout();
			this.pnlDiskSpaceHeader.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.ssStatus.SuspendLayout();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlContents);
			this.pnlForm.Controls.Add(this.pnlDiskSpaceAndControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(2, 27);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(540, 457);
			this.pnlForm.TabIndex = 2;
			this.pnlContents.Controls.Add(this.pnltlvFolders);
			this.pnlContents.Controls.Add(this.lblStatus);
			this.pnlContents.Controls.Add(this.pnlDownloadedDataHeader);
			this.pnlContents.Controls.Add(this.picLoading);
			this.pnlContents.Dock = DockStyle.Fill;
			this.pnlContents.Location = new Point(0, 0);
			this.pnlContents.Name = "pnlContents";
			this.pnlContents.Padding = new System.Windows.Forms.Padding(2);
			this.pnlContents.Size = new System.Drawing.Size(538, 368);
			this.pnlContents.TabIndex = 21;
			this.pnltlvFolders.Controls.Add(this.tlvFolders);
			this.pnltlvFolders.Dock = DockStyle.Fill;
			this.pnltlvFolders.Location = new Point(2, 30);
			this.pnltlvFolders.Name = "pnltlvFolders";
			this.pnltlvFolders.Size = new System.Drawing.Size(534, 336);
			this.pnltlvFolders.TabIndex = 20;
			this.tlvFolders.AllowColumnReorder = true;
			this.tlvFolders.CheckBoxes = CheckBoxesTypes.Recursive;
			ListView.ColumnHeaderCollection columns = this.tlvFolders.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.colName, this.colStatus, this.colSize, this.colDate };
			columns.AddRange(columnHeaderArray);
			treeListViewItemCollectionComparer.Column = 0;
			treeListViewItemCollectionComparer.SortOrder = SortOrder.Ascending;
			this.tlvFolders.Comparer = treeListViewItemCollectionComparer;
			this.tlvFolders.Dock = DockStyle.Fill;
			this.tlvFolders.HideSelection = false;
			this.tlvFolders.HighlightBackColor = Color.Blue;
			this.tlvFolders.HighlightForeColor = Color.White;
			this.tlvFolders.Location = new Point(0, 0);
			this.tlvFolders.MultiSelect = false;
			this.tlvFolders.Name = "tlvFolders";
			this.tlvFolders.Size = new System.Drawing.Size(534, 336);
			this.tlvFolders.SmallImageList = this.ilTreeList;
			this.tlvFolders.TabIndex = 1;
			this.tlvFolders.UseCompatibleStateImageBehavior = false;
			this.tlvFolders.ItemChecked += new ItemCheckedEventHandler(this.tlvFolders_ItemChecked);
			this.tlvFolders.BeforeExpand += new TreeListViewCancelEventHandler(this.tlvFolders_BeforeExpand);
			this.tlvFolders.BeforeCollapse += new TreeListViewCancelEventHandler(this.tlvFolders_BeforeCollapse);
			this.colName.Text = "Folder Name";
			this.colName.Width = 201;
			this.colStatus.Text = "Status";
			this.colStatus.Width = 93;
			this.colSize.Text = "Size";
			this.colSize.TextAlign = HorizontalAlignment.Right;
			this.colSize.Width = 93;
			this.colDate.Text = "Modified Date";
			this.colDate.TextAlign = HorizontalAlignment.Right;
			this.colDate.Width = 135;
			this.ilTreeList.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("ilTreeList.ImageStream");
			this.ilTreeList.TransparentColor = Color.Transparent;
			this.ilTreeList.Images.SetKeyName(0, "");
			this.ilTreeList.Images.SetKeyName(1, "directory.png");
			this.ilTreeList.Images.SetKeyName(2, "directory_opened.png");
			this.ilTreeList.Images.SetKeyName(3, "book_blue.png");
			this.lblStatus.BorderStyle = BorderStyle.FixedSingle;
			this.lblStatus.Dock = DockStyle.Fill;
			this.lblStatus.ForeColor = Color.Gray;
			this.lblStatus.Location = new Point(2, 30);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
			this.lblStatus.Size = new System.Drawing.Size(534, 336);
			this.lblStatus.TabIndex = 2;
			this.lblStatus.Text = "label1";
			this.pnlDownloadedDataHeader.Controls.Add(this.lblDownloadedDataLine);
			this.pnlDownloadedDataHeader.Controls.Add(this.lblDownloadedDataHeader);
			this.pnlDownloadedDataHeader.Dock = DockStyle.Top;
			this.pnlDownloadedDataHeader.Location = new Point(2, 2);
			this.pnlDownloadedDataHeader.Name = "pnlDownloadedDataHeader";
			this.pnlDownloadedDataHeader.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlDownloadedDataHeader.Size = new System.Drawing.Size(534, 28);
			this.pnlDownloadedDataHeader.TabIndex = 19;
			this.lblDownloadedDataLine.BackColor = Color.Transparent;
			this.lblDownloadedDataLine.Dock = DockStyle.Fill;
			this.lblDownloadedDataLine.ForeColor = Color.Blue;
			this.lblDownloadedDataLine.Image = Resources.GroupLine0;
			this.lblDownloadedDataLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblDownloadedDataLine.Location = new Point(139, 0);
			this.lblDownloadedDataLine.Name = "lblDownloadedDataLine";
			this.lblDownloadedDataLine.Size = new System.Drawing.Size(380, 28);
			this.lblDownloadedDataLine.TabIndex = 15;
			this.lblDownloadedDataLine.Tag = "";
			this.lblDownloadedDataLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblDownloadedDataHeader.BackColor = Color.Transparent;
			this.lblDownloadedDataHeader.Dock = DockStyle.Left;
			this.lblDownloadedDataHeader.ForeColor = Color.Blue;
			this.lblDownloadedDataHeader.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblDownloadedDataHeader.Location = new Point(7, 0);
			this.lblDownloadedDataHeader.Name = "lblDownloadedDataHeader";
			this.lblDownloadedDataHeader.Size = new System.Drawing.Size(132, 28);
			this.lblDownloadedDataHeader.TabIndex = 12;
			this.lblDownloadedDataHeader.Tag = "";
			this.lblDownloadedDataHeader.Text = "Downloaded Data";
			this.lblDownloadedDataHeader.TextAlign = ContentAlignment.MiddleLeft;
			this.picLoading.BackColor = Color.White;
			this.picLoading.Dock = DockStyle.Fill;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(2, 2);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(534, 364);
			this.picLoading.SizeMode = PictureBoxSizeMode.CenterImage;
			this.picLoading.TabIndex = 18;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.pnlDiskSpaceAndControl.Controls.Add(this.pnlControl);
			this.pnlDiskSpaceAndControl.Controls.Add(this.pnlDiskSpace);
			this.pnlDiskSpaceAndControl.Dock = DockStyle.Bottom;
			this.pnlDiskSpaceAndControl.Location = new Point(0, 368);
			this.pnlDiskSpaceAndControl.Name = "pnlDiskSpaceAndControl";
			this.pnlDiskSpaceAndControl.Size = new System.Drawing.Size(538, 87);
			this.pnlDiskSpaceAndControl.TabIndex = 24;
			this.pnlControl.Controls.Add(this.lblInUseBooks);
			this.pnlControl.Controls.Add(this.btnOK);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Dock = DockStyle.Fill;
			this.pnlControl.Location = new Point(0, 56);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(7, 4, 15, 4);
			this.pnlControl.Size = new System.Drawing.Size(538, 31);
			this.pnlControl.TabIndex = 22;
			this.lblInUseBooks.BackColor = Color.Transparent;
			this.lblInUseBooks.Dock = DockStyle.Left;
			this.lblInUseBooks.ForeColor = Color.Black;
			this.lblInUseBooks.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblInUseBooks.Location = new Point(7, 4);
			this.lblInUseBooks.Name = "lblInUseBooks";
			this.lblInUseBooks.Size = new System.Drawing.Size(257, 23);
			this.lblInUseBooks.TabIndex = 13;
			this.lblInUseBooks.Tag = "";
			this.lblInUseBooks.Text = "Books you are viewing can not be deleted.";
			this.lblInUseBooks.TextAlign = ContentAlignment.MiddleLeft;
			this.btnOK.Dock = DockStyle.Right;
			this.btnOK.Location = new Point(373, 4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.Dock = DockStyle.Right;
			this.btnCancel.Location = new Point(448, 4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.pnlDiskSpace.Controls.Add(this.pnlDiskSpace1);
			this.pnlDiskSpace.Controls.Add(this.pnlDiskSpaceHeader);
			this.pnlDiskSpace.Dock = DockStyle.Top;
			this.pnlDiskSpace.Location = new Point(0, 0);
			this.pnlDiskSpace.Name = "pnlDiskSpace";
			this.pnlDiskSpace.Size = new System.Drawing.Size(538, 56);
			this.pnlDiskSpace.TabIndex = 23;
			this.pnlDiskSpace1.Controls.Add(this.pnlDiskSpaceValue);
			this.pnlDiskSpace1.Controls.Add(this.pnlSplitter1);
			this.pnlDiskSpace1.Controls.Add(this.chkDiskSpace);
			this.pnlDiskSpace1.Dock = DockStyle.Top;
			this.pnlDiskSpace1.Location = new Point(0, 28);
			this.pnlDiskSpace1.Name = "pnlDiskSpace1";
			this.pnlDiskSpace1.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
			this.pnlDiskSpace1.Size = new System.Drawing.Size(538, 22);
			this.pnlDiskSpace1.TabIndex = 21;
			this.pnlDiskSpaceValue.BorderStyle = BorderStyle.FixedSingle;
			this.pnlDiskSpaceValue.Controls.Add(this.txtDiskSpaceValue);
			this.pnlDiskSpaceValue.Controls.Add(this.cmbDiskSpaceValue);
			this.pnlDiskSpaceValue.Dock = DockStyle.Left;
			this.pnlDiskSpaceValue.Location = new Point(236, 0);
			this.pnlDiskSpaceValue.Name = "pnlDiskSpaceValue";
			this.pnlDiskSpaceValue.Size = new System.Drawing.Size(75, 22);
			this.pnlDiskSpaceValue.TabIndex = 19;
			this.txtDiskSpaceValue.AllowSpace = false;
			this.txtDiskSpaceValue.BorderStyle = BorderStyle.None;
			this.txtDiskSpaceValue.Location = new Point(1, 4);
			this.txtDiskSpaceValue.MaxLength = 4;
			this.txtDiskSpaceValue.Name = "txtDiskSpaceValue";
			this.txtDiskSpaceValue.Size = new System.Drawing.Size(34, 13);
			this.txtDiskSpaceValue.TabIndex = 15;
			this.txtDiskSpaceValue.Text = "1.0";
			this.txtDiskSpaceValue.TextChanged += new EventHandler(this.txtDiskSpaceValue_TextChanged);
			this.cmbDiskSpaceValue.BackColor = SystemColors.Window;
			this.cmbDiskSpaceValue.Dock = DockStyle.Right;
			this.cmbDiskSpaceValue.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cmbDiskSpaceValue.FlatStyle = FlatStyle.Flat;
			this.cmbDiskSpaceValue.FormattingEnabled = true;
			this.cmbDiskSpaceValue.Items.AddRange(new object[] { "GB", "MB" });
			this.cmbDiskSpaceValue.Location = new Point(34, 0);
			this.cmbDiskSpaceValue.Name = "cmbDiskSpaceValue";
			this.cmbDiskSpaceValue.Size = new System.Drawing.Size(39, 21);
			this.cmbDiskSpaceValue.TabIndex = 14;
			this.cmbDiskSpaceValue.SelectedIndexChanged += new EventHandler(this.cmbDiskSpaceValue_SelectedIndexChanged);
			this.pnlSplitter1.Dock = DockStyle.Left;
			this.pnlSplitter1.Location = new Point(214, 0);
			this.pnlSplitter1.Name = "pnlSplitter1";
			this.pnlSplitter1.Size = new System.Drawing.Size(22, 22);
			this.pnlSplitter1.TabIndex = 20;
			this.chkDiskSpace.AutoSize = true;
			this.chkDiskSpace.Checked = true;
			this.chkDiskSpace.CheckState = CheckState.Checked;
			this.chkDiskSpace.Dock = DockStyle.Left;
			this.chkDiskSpace.Location = new Point(30, 0);
			this.chkDiskSpace.Name = "chkDiskSpace";
			this.chkDiskSpace.Size = new System.Drawing.Size(184, 22);
			this.chkDiskSpace.TabIndex = 18;
			this.chkDiskSpace.Text = "Allocate space for data download";
			this.chkDiskSpace.UseVisualStyleBackColor = true;
			this.chkDiskSpace.CheckedChanged += new EventHandler(this.chkDiskSpace_CheckedChanged);
			this.pnlDiskSpaceHeader.Controls.Add(this.lblDiskSpaceHeaderLine);
			this.pnlDiskSpaceHeader.Controls.Add(this.lblDiskSpaceHeader);
			this.pnlDiskSpaceHeader.Dock = DockStyle.Top;
			this.pnlDiskSpaceHeader.Location = new Point(0, 0);
			this.pnlDiskSpaceHeader.Name = "pnlDiskSpaceHeader";
			this.pnlDiskSpaceHeader.Padding = new System.Windows.Forms.Padding(7, 0, 15, 0);
			this.pnlDiskSpaceHeader.Size = new System.Drawing.Size(538, 28);
			this.pnlDiskSpaceHeader.TabIndex = 20;
			this.lblDiskSpaceHeaderLine.BackColor = Color.Transparent;
			this.lblDiskSpaceHeaderLine.Dock = DockStyle.Fill;
			this.lblDiskSpaceHeaderLine.ForeColor = Color.Blue;
			this.lblDiskSpaceHeaderLine.Image = Resources.GroupLine0;
			this.lblDiskSpaceHeaderLine.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblDiskSpaceHeaderLine.Location = new Point(141, 0);
			this.lblDiskSpaceHeaderLine.Name = "lblDiskSpaceHeaderLine";
			this.lblDiskSpaceHeaderLine.Size = new System.Drawing.Size(382, 28);
			this.lblDiskSpaceHeaderLine.TabIndex = 15;
			this.lblDiskSpaceHeaderLine.Tag = "";
			this.lblDiskSpaceHeaderLine.TextAlign = ContentAlignment.MiddleLeft;
			this.lblDiskSpaceHeader.BackColor = Color.Transparent;
			this.lblDiskSpaceHeader.Dock = DockStyle.Left;
			this.lblDiskSpaceHeader.ForeColor = Color.Blue;
			this.lblDiskSpaceHeader.ImageAlign = ContentAlignment.MiddleLeft;
			this.lblDiskSpaceHeader.Location = new Point(7, 0);
			this.lblDiskSpaceHeader.Name = "lblDiskSpaceHeader";
			this.lblDiskSpaceHeader.Size = new System.Drawing.Size(134, 28);
			this.lblDiskSpaceHeader.TabIndex = 12;
			this.lblDiskSpaceHeader.Tag = "";
			this.lblDiskSpaceHeader.Text = "Disk Space";
			this.lblDiskSpaceHeader.TextAlign = ContentAlignment.MiddleLeft;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			this.columnHeader1.Text = "FolderName";
			this.columnHeader1.Width = 257;
			this.columnHeader2.Text = "Size";
			this.columnHeader2.Width = 120;
			this.columnHeader3.Text = "Modified Date";
			this.columnHeader3.Width = 152;
			this.columnHeader4.Text = "FolderName";
			this.columnHeader4.Width = 257;
			this.columnHeader5.Text = "Size";
			this.columnHeader5.Width = 120;
			this.columnHeader6.Text = "Modified Date";
			this.columnHeader6.Width = 152;
			this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
			ToolStripItemCollection items = this.toolStrip1.Items;
			ToolStripItem[] toolStripItemArray = new ToolStripItem[] { this.tsbClearSelection, this.tsbSelectAll, this.toolStripSeparator4, this.tsBtnDeleteSelection };
			items.AddRange(toolStripItemArray);
			this.toolStrip1.Location = new Point(2, 2);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(540, 25);
			this.toolStrip1.TabIndex = 22;
			this.tsbClearSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbClearSelection.Image = Resources.SelectionList_clear_selection;
			this.tsbClearSelection.ImageTransparentColor = Color.Magenta;
			this.tsbClearSelection.Name = "tsbClearSelection";
			this.tsbClearSelection.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbClearSelection.Size = new System.Drawing.Size(23, 22);
			this.tsbClearSelection.Text = "Clear Selection";
			this.tsbClearSelection.Click += new EventHandler(this.tsbClearSelection_Click);
			this.tsbSelectAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsbSelectAll.Image = Resources.SelectionList_select_all;
			this.tsbSelectAll.ImageTransparentColor = Color.Magenta;
			this.tsbSelectAll.Name = "tsbSelectAll";
			this.tsbSelectAll.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsbSelectAll.Size = new System.Drawing.Size(23, 22);
			this.tsbSelectAll.Text = "Select All";
			this.tsbSelectAll.Click += new EventHandler(this.tsbSelectAll_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			this.tsBtnDeleteSelection.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.tsBtnDeleteSelection.Image = Resources.SelectionList_DeleteSelection;
			this.tsBtnDeleteSelection.ImageTransparentColor = Color.Magenta;
			this.tsBtnDeleteSelection.Name = "tsBtnDeleteSelection";
			this.tsBtnDeleteSelection.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tsBtnDeleteSelection.Size = new System.Drawing.Size(23, 22);
			this.tsBtnDeleteSelection.Text = "Delete Selection";
			this.tsBtnDeleteSelection.Click += new EventHandler(this.tsBtnDeleteSelection_Click);
			ToolStripItemCollection toolStripItemCollections = this.ssStatus.Items;
			ToolStripItem[] toolStripItemArray1 = new ToolStripItem[] { this.toolStripStatusLabel1, this.tsStatus };
			toolStripItemCollections.AddRange(toolStripItemArray1);
			this.ssStatus.Location = new Point(2, 484);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new System.Drawing.Size(540, 22);
			this.ssStatus.TabIndex = 23;
			this.toolStripStatusLabel1.BackColor = SystemColors.Control;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			this.tsStatus.Name = "tsStatus";
			this.tsStatus.Size = new System.Drawing.Size(87, 17);
			this.tsStatus.Text = "Remaining space";
			this.bgChangeDataSize.DoWork += new DoWorkEventHandler(this.bgChangeDataSize_DoWork);
			this.bgChangeDataSize.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgChangeDataSize_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(544, 508);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.ssStatus);
			base.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(480, 535);
			base.Name = "frmDataSize";
			base.Padding = new System.Windows.Forms.Padding(2);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Manage Disk Space";
			base.Load += new EventHandler(this.frmDataSize_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmDataSize_FormClosing);
			this.pnlForm.ResumeLayout(false);
			this.pnlContents.ResumeLayout(false);
			this.pnltlvFolders.ResumeLayout(false);
			this.pnlDownloadedDataHeader.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			this.pnlDiskSpaceAndControl.ResumeLayout(false);
			this.pnlControl.ResumeLayout(false);
			this.pnlDiskSpace.ResumeLayout(false);
			this.pnlDiskSpace1.ResumeLayout(false);
			this.pnlDiskSpace1.PerformLayout();
			this.pnlDiskSpaceValue.ResumeLayout(false);
			this.pnlDiskSpaceValue.PerformLayout();
			this.pnlDiskSpaceHeader.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Data Cleanup", "DATA_SIZE", ResourceType.TITLE);
			this.tsbSelectAll.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.TOOLSTRIP);
			this.tsbClearSelection.Text = this.GetResource("Unselect All", "UNSELECT_ALL", ResourceType.TOOLSTRIP);
			this.tsBtnDeleteSelection.Text = this.GetResource("Delete Selected", "DELETE_SELECTED", ResourceType.TOOLSTRIP);
			this.lblDiskSpaceHeader.Text = this.GetResource("Disk Space", "DISK_SPACE", ResourceType.LABEL);
			this.chkDiskSpace.Text = this.GetResource("Allocate space for data download", "ALLOCATE_SPACE", ResourceType.CHECK_BOX);
			this.lblDownloadedDataHeader.Text = this.GetResource("Downloaded Data", "DOWNLOADED_DATA", ResourceType.LABEL);
			this.sBookInUseMsg = (this.GetResource("Book in use", "BOOK_IN_USE", ResourceType.LABEL) == null ? "Book\u00a0in\u00a0use" : this.GetResource("Book\u00a0in\u00a0use", "BOOK_IN_USE", ResourceType.LABEL));
			this.lblInUseBooks.Text = (this.GetResource("Books you are viewing can not be deleted.", "CANNOT_DELETE_BOOK", ResourceType.LABEL) == null ? "Books you are viewing can not be deleted." : this.GetResource("Books you are viewing can not be deleted.", "CANNOT_DELETE_BOOK", ResourceType.LABEL));
			this.tlvFolders.Columns[0].Text = this.GetResource("Folder Name", "FOLDER_NAME", ResourceType.TREE_VIEW);
			this.tlvFolders.Columns[1].Text = (this.GetResource("Status", "STATUS", ResourceType.TREE_VIEW) == null ? "Status" : this.GetResource("Status", "STATUS", ResourceType.TREE_VIEW));
			this.tlvFolders.Columns[2].Text = this.GetResource("Size", "SIZE", ResourceType.TREE_VIEW);
			this.tlvFolders.Columns[3].Text = this.GetResource("Modified Date", "MODIFIED_DATE", ResourceType.TREE_VIEW);
			this.btnOK.Text = this.GetResource("OK", "OK", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
		}

		private void LoadUserSettings()
		{
			if (Settings.Default.frmDataSizeLocation != new Point(0, 0))
			{
				base.Location = Settings.Default.frmDataSizeLocation;
			}
			else
			{
				base.StartPosition = FormStartPosition.CenterScreen;
			}
			base.Size = Settings.Default.frmDataSizeSize;
			if (Settings.Default.frmDataSizeState == FormWindowState.Minimized)
			{
				base.WindowState = FormWindowState.Normal;
				return;
			}
			base.WindowState = Settings.Default.frmDataSizeState;
		}

		private void SaveUserSettings()
		{
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmDataSizeLocation = base.RestoreBounds.Location;
			}
			else
			{
				Settings.Default.frmDataSizeLocation = base.Location;
			}
			if (base.WindowState != FormWindowState.Normal)
			{
				Settings.Default.frmDataSizeSize = base.RestoreBounds.Size;
			}
			else
			{
				Settings.Default.frmDataSizeSize = base.Size;
			}
			Settings.Default.frmDataSizeState = base.WindowState;
		}

		private void SetMessage(string msg)
		{
			if (!this.lblStatus.InvokeRequired)
			{
				this.lblStatus.Text = msg;
				this.lblStatus.BringToFront();
				return;
			}
			Label label = this.lblStatus;
			frmDataSize.SetMessageDelegate setMessageDelegate = new frmDataSize.SetMessageDelegate(this.SetMessage);
			object[] objArray = new object[] { msg };
			label.Invoke(setMessageDelegate, objArray);
		}

		private void SetRemainingSpaceStatus()
		{
			long num = (long)0;
			string empty = string.Empty;
			string str = string.Empty;
			foreach (TreeListViewItem item in this.tlvFolders.Items)
			{
				if (!item.Checked)
				{
					continue;
				}
				foreach (TreeListViewItem treeListViewItem in item.Items)
				{
					if (!treeListViewItem.Checked)
					{
						continue;
					}
					ListViewItem.ListViewSubItem listViewSubItem = treeListViewItem.SubItems["Size"];
					if (listViewSubItem == null)
					{
						continue;
					}
					num += DataSize.FormattedSize(listViewSubItem.Text);
				}
			}
			empty = DataSize.FormattedSize(num + this.stFreeSpace);
			str = DataSize.FormattedSize(this.stAllocatedSize);
			if (this.stFreeSpace <= (long)0)
			{
				this.UpdateStatus(string.Concat(DataSize.FormattedSize(this.stFreeSpace * (long)-1), " ", this.GetResource("Overflow", "OVERFLOW", ResourceType.STATUS_MESSAGE)));
				return;
			}
			string[] resource = new string[] { empty, "/", str, " ", this.GetResource("Remaining", "REMAINING", ResourceType.STATUS_MESSAGE) };
			this.UpdateStatus(string.Concat(resource));
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				if (!parentPanel.InvokeRequired)
				{
					foreach (Control control in parentPanel.Controls)
					{
						if (control == this.picLoading)
						{
							continue;
						}
						control.Visible = false;
					}
					this.picLoading.Parent = parentPanel;
					this.picLoading.BringToFront();
					this.picLoading.Show();
				}
				else
				{
					frmDataSize.ShowLoadingDelegate showLoadingDelegate = new frmDataSize.ShowLoadingDelegate(this.ShowLoading);
					object[] objArray = new object[] { parentPanel };
					parentPanel.Invoke(showLoadingDelegate, objArray);
				}
			}
			catch
			{
			}
		}

		private void tlvFolders_BeforeCollapse(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.ImageIndex == 2)
			{
				e.Item.ImageIndex = 1;
			}
		}

		private void tlvFolders_BeforeExpand(object sender, TreeListViewCancelEventArgs e)
		{
			if (e.Item.ImageIndex == 1)
			{
				e.Item.ImageIndex = 2;
			}
		}

		private void tlvFolders_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			try
			{
				if (e.Item.SubItems.Count == 4 && e.Item.SubItems["Status"].Text == this.sBookInUseMsg)
				{
					e.Item.Checked = false;
					return;
				}
			}
			catch
			{
			}
			if (!this.currentlyLoadingData && !this.currentlyCalculatingSpace)
			{
				this.SetRemainingSpaceStatus();
			}
		}

		private void TreeListAddItem(TreeListViewItem item)
		{
			if (!this.tlvFolders.InvokeRequired)
			{
				this.tlvFolders.Items.Add(item);
				return;
			}
			TreeListView treeListView = this.tlvFolders;
			frmDataSize.TreeListAddItemDelegate treeListAddItemDelegate = new frmDataSize.TreeListAddItemDelegate(this.TreeListAddItem);
			object[] objArray = new object[] { item };
			treeListView.Invoke(treeListAddItemDelegate, objArray);
		}

		private void tsbClearSelection_Click(object sender, EventArgs e)
		{
			foreach (TreeListViewItem item in this.tlvFolders.Items)
			{
				item.Checked = false;
			}
		}

		private void tsbSelectAll_Click(object sender, EventArgs e)
		{
			foreach (TreeListViewItem item in this.tlvFolders.Items)
			{
				item.Checked = false;
				item.Checked = true;
			}
		}

		private void tsBtnDeleteSelection_Click(object sender, EventArgs e)
		{
			if ((int)this.tlvFolders.CheckedItems.Length > 0)
			{
				try
				{
					if (MessageBox.Show(this.GetResource("Are you sure you want to delete the selected data", "DELETE_DATA", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
					{
						this.tlvFolders.SuspendLayout();
						TreeListViewItem[] checkedItems = this.tlvFolders.CheckedItems;
						for (int i = (int)checkedItems.Length - 1; i >= 0; i--)
						{
							if (checkedItems[i].SubItems.Count == 4)
							{
								Directory.Delete(checkedItems[i].Tag.ToString(), true);
								checkedItems[i].Remove();
							}
						}
						checkedItems = this.tlvFolders.CheckedItems;
						for (int j = (int)checkedItems.Length - 1; j >= 0; j--)
						{
							if (checkedItems[j].SubItems.Count == 1)
							{
								if (checkedItems[j].ChildrenCount <= 0)
								{
									checkedItems[j].Remove();
								}
								else
								{
									checkedItems[j].Checked = false;
								}
							}
						}
						this.tlvFolders.ResumeLayout();
					}
					if (this.tlvFolders.Items.Count == 0)
					{
						this.lblStatus.Text = this.GetResource("No books exist", "NO_BOOKS_EXIST", ResourceType.STATUS_MESSAGE);
						this.lblStatus.BringToFront();
						this.tlvFolders.Enabled = false;
						this.tsBtnDeleteSelection.Enabled = false;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					MessageHandler.ShowMessage(this, string.Concat(this.GetResource("(E-VDS-EM001) Failed to delete specified object", "(E-VDS-EM001)_FAILED", ResourceType.POPUP_MESSAGE), "\r\n", exception.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void txtDiskSpaceValue_TextChanged(object sender, EventArgs e)
		{
			if (this.thCalculateSpace.IsAlive)
			{
				this.thCalculateSpace.Abort();
			}
			while (this.thCalculateSpace.IsAlive)
			{
			}
			this.thCalculateSpace = new Thread(new ThreadStart(this.CalculateRemainingSpaceStatus));
			this.thCalculateSpace.Start();
		}

		public void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}

		public void UpdateStatus(string status)
		{
			if (!this.ssStatus.InvokeRequired)
			{
				this.tsStatus.Text = status;
				return;
			}
			StatusStrip statusStrip = this.ssStatus;
			frmDataSize.UpdateStatusDelegate updateStatusDelegate = new frmDataSize.UpdateStatusDelegate(this.UpdateStatus);
			object[] objArray = new object[] { status };
			statusStrip.Invoke(updateStatusDelegate, objArray);
		}

		private delegate string GetCmbDiskSpaceDelegate();

		private delegate string GetStatusTextDelegate();

		private delegate string GetTxtDiskSpaceDelegate();

		private delegate void HideLoadingDelegate(Panel parentPanel);

		private delegate void SetMessageDelegate(string msg);

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void TreeListAddItemDelegate(TreeListViewItem item);

		private delegate void UpdateStatusDelegate(string status);
	}
}
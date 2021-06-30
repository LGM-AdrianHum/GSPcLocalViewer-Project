using FileDownloaderDLL;
using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class frmMultipleBooksDownload : Form
	{
		private frmViewer frmParent;

		private string sLocalPath;

		private string sServerPath;

		private bool isWorking;

		private bool isListLoaded;

		private int iSuccessCount;

		private ArrayList BooksList;

		private int bookCounter;

		private FileDownloader objFileDownloader;

		public frmMultipleBooksDownload.UpdateProgressbarsDel progrDelegate;

		private IContainer components;

		private Button btnDownload;

		private BackgroundWorker bgWorker;

		private ProgressBar progressOverall;

		private ProgressBar progressCurrentFile;

		private StatusStrip ssStatus;

		private ToolStripStatusLabel lblStatus;

		private BackgroundWorker bgLoader;

		private Button btnCancel;

		private Label lblDownloaded;

		private Panel pnlControl;

		private Panel pnlForm;

		private ListView bookListView;

		private Label lblCurrentProgress;

		private Label lblOverallProgress;

		private Label lblCurrentPictureDownload;

		private SplitContainer splitContainer1;

		private ListView listBooks;

		private ColumnHeader colHeadFileName;

		private ColumnHeader colHeadStatus;

		private PictureBox picLoading;

		private CheckBox chkboxSelectAll;

		private ColumnHeader columnHeader1;

		public frmMultipleBooksDownload(frmViewer frm, string _sLocalPath, string _sServerPath)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.sLocalPath = _sLocalPath;
			if (this.sLocalPath.EndsWith("\\"))
			{
				this.sLocalPath = this.sLocalPath.Remove(this.sLocalPath.LastIndexOf("\\"));
			}
			this.sServerPath = _sServerPath;
			if (this.sServerPath.EndsWith("/"))
			{
				this.sServerPath = this.sServerPath.Remove(this.sServerPath.LastIndexOf("/"));
			}
			this.progressOverall.Maximum = 100;
			this.progressOverall.Minimum = 0;
			this.progressOverall.Value = 0;
			this.progressCurrentFile.Value = 0;
			this.bookCounter = 0;
			this.UpdateFont();
			this.UpdateStatus(string.Empty);
			this.isWorking = false;
			this.isListLoaded = false;
			this.LoadResources();
		}

		private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			DateTime dateTime;
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			this.ListClearItems();
			string empty = string.Empty;
			empty = (!this.frmParent.CompressedDownload ? ".xml" : ".zip");
			for (int i = 0; i < this.bookListView.Items.Count; i++)
			{
				string[] strArrays = this.BooksList[i].ToString().Split(new char[] { '|' });
				if (this.BookListGetBookStatus(i) && (int)strArrays.Length == 3)
				{
					string str = strArrays[0].ToString();
					string str1 = string.Concat(this.sLocalPath, "\\", str, "\\");
					if (!Directory.Exists(str1))
					{
						Directory.CreateDirectory(str1);
					}
					if (File.Exists(string.Concat(str1, str, empty)))
					{
						string empty1 = string.Empty;
						empty1 = strArrays[2];
						try
						{
							dateTime = DateTime.ParseExact(empty1, "dd/MM/yyyy HH:mm:ss", null);
						}
						catch
						{
							try
							{
								dateTime = DateTime.ParseExact(empty1, "dd/MM/yyyy", null);
							}
							catch
							{
								dateTime = new DateTime();
							}
						}
						int num = 0;
						if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
						{
							num = 0;
						}
						if (num == 0)
						{
							string[] strArrays1 = new string[] { this.sServerPath, "/", str, "/", str, empty };
							this.DownloadFile(string.Concat(strArrays1), string.Concat(str1, str, empty));
						}
						else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(string.Concat(str1, str, empty), this.frmParent.p_ServerId), dateTime, num))
						{
							string[] strArrays2 = new string[] { this.sServerPath, "/", str, "/", str, empty };
							this.DownloadFile(string.Concat(strArrays2), string.Concat(str1, str, empty));
						}
					}
					else
					{
						string[] strArrays3 = new string[] { this.sServerPath, "/", str, "/", str, empty };
						this.DownloadFile(string.Concat(strArrays3), string.Concat(str1, str, empty));
					}
					try
					{
						if (this.frmParent.CompressedDownload && File.Exists(string.Concat(str1, str, empty)))
						{
							Global.Unzip(string.Concat(str1, str, empty));
						}
					}
					catch
					{
					}
					ArrayList arrayLists = new ArrayList();
					if (File.Exists(string.Concat(str1, str, ".xml")))
					{
						arrayLists = this.GetAllPagesToDownload(string.Concat(str1, str, ".xml"));
					}
					this.frmParent.BookDowloadAddSearchXmlFile(ref arrayLists, str1, str, strArrays[1]);
					if (arrayLists.Count > 0)
					{
						string[] strArrays4 = new string[] { string.Concat("[", str, "]"), string.Empty };
						this.ListAddItem(new ListViewItem(strArrays4));
						this.bookCounter++;
					}
					for (int j = 0; j < arrayLists.Count; j++)
					{
						string str2 = arrayLists[j].ToString();
						string[] strArrays5 = new string[] { "|*|*|" };
						string[] strArrays6 = str2.Split(strArrays5, StringSplitOptions.None);
						string str3 = strArrays6[0];
						string[] empty2 = new string[] { str3, string.Empty };
						ListViewItem listViewItem = new ListViewItem(empty2);
						if ((int)strArrays6.Length == 2)
						{
							listViewItem.Tag = strArrays6[1];
						}
						this.ListAddItem(listViewItem);
					}
				}
			}
		}

		private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.progressCurrentFile.Minimum = 0;
			this.progressCurrentFile.Maximum = 100;
			this.progressCurrentFile.Value = 0;
			this.progressOverall.Minimum = 0;
			if (this.listBooks.Items.Count - this.bookCounter <= 0)
			{
				this.progressOverall.Maximum = 0;
			}
			else
			{
				this.progressOverall.Maximum = this.listBooks.Items.Count - this.bookCounter;
			}
			this.progressOverall.Value = 0;
			this.HideLoading(this.splitContainer1.Panel2);
			this.btnDownload.Enabled = true;
			this.btnCancel.Enabled = true;
			this.btnCancel.Text = "Cancel";
			bool flag = false;
			int num = 0;
			while (num < this.bookListView.Items.Count)
			{
				if (!this.BookListGetBookStatus(num))
				{
					num++;
				}
				else
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.UpdateStatus(this.GetResource("(W-MBD-WM001) Please select at least 1 book to download", "(W-MBD-WM001)_SELECT", ResourceType.STATUS_MESSAGE));
			}
			else if (this.listBooks.Items.Count != 0)
			{
				this.UpdateStatus(this.GetResource("Ready……", "READY", ResourceType.STATUS_MESSAGE));
			}
			else
			{
				this.UpdateStatus(this.GetResource("Books are already updated", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
			}
			if (!this.isListLoaded)
			{
				this.isListLoaded = true;
				this.progressOverall.Value = 0;
				this.progressCurrentFile.Value = 0;
				this.btnDownload.Enabled = false;
				this.UpdateStatus(string.Empty);
				if (!this.isWorking)
				{
					this.isWorking = true;
					Thread thread = new Thread(new ThreadStart(this.DownloadFilesList))
					{
						Name = "DownloadThread"
					};
					thread.Start();
				}
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string empty = string.Empty;
			this.iSuccessCount = 0;
			string str = string.Empty;
			int num = 0;
			DataSize.miliSecInterval = 2000;
			for (int i = 0; i < this.listBooks.Items.Count; i++)
			{
				if (!this.isWorking)
				{
					return;
				}
				if (!base.IsDisposed)
				{
					empty = this.ListGetFileName(i);
					if (!empty.Contains("["))
					{
						string[] resource = new string[] { this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL), " ", str, " >> ", empty };
						this.UpdateProgress(string.Concat(resource));
						string[] strArrays = new string[] { this.sServerPath, "/", str, "/", empty };
						string str1 = string.Concat(strArrays);
						string[] strArrays1 = new string[] { this.sLocalPath, "\\", str, "\\", empty };
						if (!this.DownloadFile(str1, string.Concat(strArrays1)))
						{
							this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
						}
						else
						{
							this.ListEditStatus(i, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
							this.iSuccessCount++;
						}
						num++;
					}
					else
					{
						str = empty.Substring(empty.IndexOf("[") + 1, empty.Length - 2);
					}
					this.OverallProgressUpdate(num);
					if (DataSize.spaceLeft < (long)10485760)
					{
						this.btnCancel_Click(null, null);
					}
				}
			}
		}

		private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.UpdateProgressLabel(e.ProgressPercentage);
			this.UpdateFileProgress(e.ProgressPercentage);
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!this.isWorking)
			{
				this.UpdateStatus(this.GetResource("Downloading cancelled", "DOWNLOADING_CANCELLED", ResourceType.STATUS_MESSAGE));
			}
			else
			{
				this.isWorking = false;
				bool flag = false;
				int num = 0;
				while (num < this.bookListView.Items.Count)
				{
					if (!this.BookListGetBookStatus(num))
					{
						num++;
					}
					else
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.UpdateStatus(this.GetResource("(W-MBD-WM001) Please select at least 1 book to download", "(W-MBD-WM001)_SELECT", ResourceType.STATUS_MESSAGE));
				}
				else if (this.listBooks.Items.Count != 0)
				{
					this.UpdateProgress("");
					this.lblCurrentProgress.Text = "";
				}
				else
				{
					this.UpdateStatus(this.GetResource("Books are already updated", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
				}
			}
			this.isListLoaded = false;
			this.HideLoading(this.splitContainer1.Panel2);
			this.btnDownload.Enabled = true;
			this.bookListView.Enabled = true;
			this.chkboxSelectAll.Enabled = true;
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
			if (DataSize.spaceLeft < (long)10485760)
			{
				MessageBox.Show(this.frmParent, this.GetResource("Book download canceled because maximum download limit was reached", "MAXIMUM_LIMIT_REACHED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.btnCancel_Click(null, null);
				DataSize.miliSecInterval = 20000;
			}
		}

		private bool BookListGetBookStatus(int index)
		{
			if (!this.bookListView.InvokeRequired)
			{
				return this.bookListView.Items[index].Checked;
			}
			ListView listView = this.bookListView;
			frmMultipleBooksDownload.getBookCheckedStatusDelegate _getBookCheckedStatusDelegate = new frmMultipleBooksDownload.getBookCheckedStatusDelegate(this.BookListGetBookStatus);
			object[] objArray = new object[] { index };
			return (bool)listView.Invoke(_getBookCheckedStatusDelegate, objArray);
		}

		private void bookListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = this.bookListView.Columns[0].Width;
		}

		private void bookListView_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			try
			{
				if (!e.Item.Checked)
				{
					this.chkboxSelectAll.Checked = false;
				}
				else if (e.Item.Checked)
				{
					bool flag = true;
					int num = 0;
					while (num < this.bookListView.Items.Count)
					{
						if (this.bookListView.Items[num].Checked)
						{
							num++;
						}
						else
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.chkboxSelectAll.Checked = true;
					}
				}
			}
			catch
			{
			}
		}

		private void bookListView_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void bookListView_Resize(object sender, EventArgs e)
		{
			this.bookListView.Columns[0].Width = this.bookListView.Width - 10;
		}

		private void BooksListClearItems()
		{
			if (!this.bookListView.InvokeRequired)
			{
				this.bookListView.Items.Clear();
				return;
			}
			this.bookListView.Invoke(new frmMultipleBooksDownload.ListClearItemsDelegate(this.BooksListClearItems));
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (!this.isWorking)
			{
				base.Close();
				return;
			}
			this.isWorking = false;
			Application.DoEvents();
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
		}

		private void btnDownload_Click(object sender, EventArgs e)
		{
			if (!this.isListLoaded)
			{
				this.bookCounter = 0;
				this.btnDownload.Enabled = false;
				this.btnCancel.Enabled = false;
				this.chkboxSelectAll.Enabled = false;
				this.bookListView.Enabled = false;
				this.UpdateStatus(this.GetResource("Checking Books to download……", "CHECKING_TO_DOWNLOAD", ResourceType.STATUS_MESSAGE));
				this.ShowLoading(this.splitContainer1.Panel2);
				this.bgLoader.RunWorkerAsync();
			}
		}

		private void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkboxSelectAll.Tag.ToString() == "BYMOUSE")
			{
				this.chkboxSelectAll.Tag = string.Empty;
				if (this.chkboxSelectAll.Checked)
				{
					for (int i = 0; i < this.bookListView.Items.Count; i++)
					{
						this.bookListView.Items[i].Checked = true;
					}
				}
				else if (!this.chkboxSelectAll.Checked)
				{
					for (int j = 0; j < this.bookListView.Items.Count; j++)
					{
						this.bookListView.Items[j].Checked = false;
					}
				}
			}
			this.chkboxSelectAll.Tag = string.Empty;
		}

		private void chkboxSelectAll_MouseDown(object sender, MouseEventArgs e)
		{
			try
			{
				this.chkboxSelectAll.Tag = "BYMOUSE";
			}
			catch
			{
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

		private bool DownloadFile(string surl1, string sLocalPath)
		{
			DateTime now;
			TimeSpan timeSpan;
			bool flag;
			Application.DoEvents();
			FileDownloader fileDownloader = new FileDownloader();
			long fileLength = (long)0;
			long bytesDownloaded = (long)0;
			int num = 0;
			try
			{
				int num1 = 0;
				int num2 = 0;
				int.TryParse(Settings.Default.appProxyTimeOut, out num1);
				if (Settings.Default.appProxyType != "0")
				{
					int num3 = fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
					if (num3 == 407)
					{
						try
						{
							Program.bShowProxyScreen = true;
							if (!Dashbord.bShowProxy)
							{
								flag = false;
							}
							else
							{
								this.ShowProxyAuthWarning();
								while (Program.bShowProxyScreen)
								{
									num3 = fileDownloader.DownloadingFileWithProxy(surl1, sLocalPath, Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
									if (num3 == 407)
									{
										if (!Dashbord.bShowProxy)
										{
											flag = false;
											return flag;
										}
										else
										{
											this.ShowProxyAuthWarning();
										}
									}
									else if (num3 != 1)
									{
										Program.bShowProxyScreen = false;
										fileLength = (long)fileDownloader.get_FileLength();
										this.progressCurrentFile.Minimum = 0;
										this.progressCurrentFile.Maximum = 100;
										now = DateTime.Now;
										num2 = 0;
										do
										{
											bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
											num = (int)(bytesDownloaded * (long)100 / fileLength);
											if (!this.frmParent.IsDisposed)
											{
												if (num2 != num)
												{
													now = DateTime.Now;
													num2 = num;
													this.bgWorker.ReportProgress(num);
												}
												timeSpan = DateTime.Now - now;
											}
											else
											{
												fileDownloader.StopDownloadingFile();
												flag = false;
												return flag;
											}
										}
										while (bytesDownloaded < fileLength && timeSpan.Seconds < num1);
										this.bgWorker.ReportProgress(num);
										flag = true;
										return flag;
									}
									else
									{
										Program.bShowProxyScreen = false;
										flag = false;
										return flag;
									}
								}
								flag = true;
							}
						}
						catch (Exception exception)
						{
							flag = false;
						}
					}
					else if (num3 != 1)
					{
						fileLength = (long)fileDownloader.get_FileLength();
						this.progressCurrentFile.Minimum = 0;
						this.progressCurrentFile.Maximum = 100;
						now = DateTime.Now;
						num2 = 0;
						do
						{
							bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
							num = (int)(bytesDownloaded * (long)100 / fileLength);
							if (!this.frmParent.IsDisposed)
							{
								if (num2 != num)
								{
									now = DateTime.Now;
									num2 = num;
									this.bgWorker.ReportProgress(num);
								}
								timeSpan = DateTime.Now - now;
							}
							else
							{
								fileDownloader.StopDownloadingFile();
								flag = false;
								return flag;
							}
						}
						while (bytesDownloaded < fileLength && timeSpan.Seconds < num1);
						this.bgWorker.ReportProgress(num);
						flag = true;
					}
					else
					{
						flag = false;
					}
				}
				else if (fileDownloader.DownloadingFile(surl1, sLocalPath, string.Concat(Settings.Default.appProxyTimeOut, "000")) != 1)
				{
					fileLength = (long)fileDownloader.get_FileLength();
					this.progressCurrentFile.Minimum = 0;
					this.progressCurrentFile.Maximum = 100;
					now = DateTime.Now;
					num2 = 0;
					do
					{
						bytesDownloaded = (long)fileDownloader.get_BytesDownloaded();
						num = (bytesDownloaded != (long)0 ? (int)(bytesDownloaded * (long)100 / fileLength) : 0);
						if (!this.frmParent.IsDisposed)
						{
							if (num2 != num)
							{
								now = DateTime.Now;
								num2 = num;
								this.bgWorker.ReportProgress(num);
							}
							timeSpan = DateTime.Now - now;
						}
						else
						{
							fileDownloader.StopDownloadingFile();
							flag = false;
							return flag;
						}
					}
					while (bytesDownloaded < fileLength && timeSpan.Seconds < num1);
					this.bgWorker.ReportProgress(num);
					flag = (bytesDownloaded != fileLength ? false : true);
				}
				else
				{
					flag = false;
				}
			}
			catch
			{
				try
				{
					fileDownloader.StopDownloadingFile();
				}
				catch
				{
				}
				flag = false;
			}
			return flag;
		}

		private void DownloadFilesList()
		{
			TimeSpan now;
			int i = 0;
			try
			{
				int num = 0;
				int num1 = 0;
				int.TryParse(Settings.Default.appProxyTimeOut, out num);
				DataSize.miliSecInterval = 2000;
				string empty = string.Empty;
				string str = string.Empty;
				this.iSuccessCount = 0;
				this.objFileDownloader = new FileDownloader();
				int num2 = 0;
				long bytesDownloaded = (long)0;
				int fileLength = 0;
				base.Invoke(new frmMultipleBooksDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
				for (i = 0; i < this.listBooks.Items.Count; i++)
				{
					int num3 = 0;
					if (!this.isWorking)
					{
						break;
					}
					if (!base.IsDisposed)
					{
						base.Invoke(new frmMultipleBooksDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
						empty = this.ListGetFileName(i);
						if (!empty.Contains("["))
						{
							string[] resource = new string[] { this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL), " ", str, " >> ", empty };
							this.UpdateProgress(string.Concat(resource));
							FileDownloader fileDownloader = this.objFileDownloader;
							string[] strArrays = new string[] { this.sServerPath, "/", str, "/", empty };
							string str1 = string.Concat(strArrays);
							string[] strArrays1 = new string[] { this.sLocalPath, "\\", str, "\\", empty };
							if (fileDownloader.DownloadingFile(str1, string.Concat(strArrays1), string.Concat(Settings.Default.appProxyTimeOut, "000")) != 1)
							{
								num1 = 0;
								DateTime dateTime = DateTime.Now;
								do
								{
									if (this.frmParent.IsDisposed)
									{
										this.objFileDownloader.StopDownloadingFile();
										if (Thread.CurrentThread.Name == "DownloadThread")
										{
											Thread.CurrentThread.Abort();
										}
									}
									bytesDownloaded = (long)this.objFileDownloader.get_BytesDownloaded();
									if (bytesDownloaded != (long)0)
									{
										fileLength = (int)(bytesDownloaded * (long)100 / (long)this.objFileDownloader.get_FileLength());
									}
									else
									{
										fileLength = 0;
										bytesDownloaded = (long)-1;
									}
									if (num1 != fileLength)
									{
										dateTime = DateTime.Now;
										num1 = fileLength;
										this.progrDelegate = new frmMultipleBooksDownload.UpdateProgressbarsDel(this.UpdateProgress);
										frmMultipleBooksDownload.UpdateProgressbarsDel updateProgressbarsDel = this.progrDelegate;
										object[] objArray = new object[] { fileLength };
										base.Invoke(updateProgressbarsDel, objArray);
									}
									now = DateTime.Now - dateTime;
									if (now.Seconds < num || num3 >= 3)
									{
										continue;
									}
									num3++;
									Application.DoEvents();
									FileDownloader fileDownloader1 = this.objFileDownloader;
									string[] strArrays2 = new string[] { this.sServerPath, "/", str, "/", empty };
									string str2 = string.Concat(strArrays2);
									string[] strArrays3 = new string[] { this.sLocalPath, "\\", str, "\\", empty };
									fileDownloader1.DownloadingFile(str2, string.Concat(strArrays3), string.Concat(Settings.Default.appProxyTimeOut, "000"));
									dateTime = DateTime.Now;
									now = DateTime.Now - dateTime;
									bytesDownloaded = (long)this.objFileDownloader.get_BytesDownloaded();
									if (bytesDownloaded == (long)0)
									{
										bytesDownloaded = (long)-1;
										fileLength = 0;
									}
									else
									{
										fileLength = (int)(bytesDownloaded * (long)100 / (long)this.objFileDownloader.get_FileLength());
									}
									num1 = fileLength;
									this.progrDelegate = new frmMultipleBooksDownload.UpdateProgressbarsDel(this.UpdateProgress);
									frmMultipleBooksDownload.UpdateProgressbarsDel updateProgressbarsDel1 = this.progrDelegate;
									object[] objArray1 = new object[] { fileLength };
									base.Invoke(updateProgressbarsDel1, objArray1);
								}
								while (bytesDownloaded < (long)this.objFileDownloader.get_FileLength() && now.Seconds < num);
								if (bytesDownloaded != (long)this.objFileDownloader.get_FileLength())
								{
									try
									{
										this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
										string[] strArrays4 = new string[] { this.sLocalPath, "/", str, "/", empty };
										if (File.Exists(string.Concat(strArrays4)))
										{
											string[] strArrays5 = new string[] { this.sLocalPath, "\\", str, "\\", empty };
											File.Delete(string.Concat(strArrays5));
										}
									}
									catch
									{
									}
								}
								else
								{
									try
									{
										if (!empty.ToUpper().EndsWith("XML") && !empty.ToUpper().EndsWith("ZIP"))
										{
											string[] strArrays6 = new string[] { this.sLocalPath, "\\", str, "\\", empty };
											Global.ChangeDjVUModifiedDate(string.Concat(strArrays6), this.ListGetFileDateFromListNodeTag(i));
										}
									}
									catch
									{
									}
									this.ListEditStatus(i, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
									this.iSuccessCount++;
								}
								if (DataSize.spaceLeft < (long)10485760)
								{
									this.btnCancel_Click(null, null);
								}
							}
							else
							{
								this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
							}
							num2++;
							this.OverallProgressUpdate(num2);
						}
						else
						{
							str = empty.Substring(empty.IndexOf("[") + 1, empty.Length - 2);
						}
					}
				}
				base.Invoke(new frmMultipleBooksDownload.UpdateProgressDelegate(this.DownloadingComplete));
			}
			catch
			{
				try
				{
					this.objFileDownloader.StopDownloadingFile();
				}
				catch
				{
				}
				this.ListEditStatus(i, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
			}
		}

		private void DownloadingComplete()
		{
			if (!this.isWorking)
			{
				this.UpdateStatus(this.GetResource("Downloading cancelled", "DOWNLOADING_CANCELLED", ResourceType.STATUS_MESSAGE));
			}
			else
			{
				this.isWorking = false;
				bool flag = false;
				int num = 0;
				while (num < this.bookListView.Items.Count)
				{
					if (!this.BookListGetBookStatus(num))
					{
						num++;
					}
					else
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.UpdateStatus(this.GetResource("(W-MBD-WM001) Please select at least 1 book to download", "(W-MBD-WM001)_SELECT", ResourceType.STATUS_MESSAGE));
				}
				else if (this.listBooks.Items.Count != 0)
				{
					this.UpdateProgress("");
					this.lblCurrentProgress.Text = "";
				}
				else
				{
					this.UpdateStatus(this.GetResource("Books are already updated", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
				}
			}
			this.isListLoaded = false;
			this.btnDownload.Enabled = true;
			this.bookListView.Enabled = true;
			this.chkboxSelectAll.Enabled = true;
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
			if (DataSize.spaceLeft < (long)10485760)
			{
				MessageBox.Show(this.frmParent, this.GetResource("Book download canceled because maximum download limit was reached", "MAXIMUM_LIMIT_REACHED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.btnCancel_Click(null, null);
				DataSize.miliSecInterval = 20000;
			}
		}

		private void frmAllBooksDownload_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			this.frmParent.HideDimmer();
		}

		private void frmAllBooksDownload_Load(object sender, EventArgs e)
		{
			this.listBooks.Columns[0].Width = 200;
			this.listBooks.Columns[1].Width = 105;
			this.btnDownload.Enabled = true;
			this.btnCancel.Enabled = true;
			if (!this.ReadSeriesFile())
			{
				this.UpdateStatus(this.GetResource("Books not Loaded", "BOOKS_NOT_LOADED", ResourceType.STATUS_MESSAGE));
				this.btnDownload.Enabled = false;
				this.btnCancel.Enabled = false;
			}
		}

		public ArrayList GetAllPagesToDownload(string sLocalPath)
		{
			ArrayList arrayLists = new ArrayList();
			XmlDocument xmlDocument = new XmlDocument();
			DateTime dateTime = new DateTime();
			bool flag = false;
			bool flag1 = false;
			string empty = string.Empty;
			string value = string.Empty;
			try
			{
				xmlDocument.Load(sLocalPath);
				if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
				{
					string str = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
					xmlDocument.DocumentElement.InnerXml = str;
				}
				XmlElement documentElement = xmlDocument.DocumentElement;
				XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
				if (xmlNodes != null)
				{
					string name = string.Empty;
					string empty1 = string.Empty;
					string name1 = string.Empty;
					string str1 = string.Empty;
					string empty2 = string.Empty;
					foreach (XmlAttribute attribute in xmlNodes.Attributes)
					{
						if (attribute.Value.ToUpper() == "PICTUREFILE")
						{
							name = attribute.Name;
						}
						if (attribute.Value.ToUpper() == "PARTSLISTFILE")
						{
							empty1 = attribute.Name;
						}
						if (attribute.Value.ToUpper() == "UPDATEDATE")
						{
							name1 = attribute.Name;
						}
						if (attribute.Value.ToUpper() == "UPDATEDATEPIC")
						{
							str1 = attribute.Name;
						}
						if (attribute.Value.ToUpper() != "UPDATEDATEPL")
						{
							continue;
						}
						empty2 = attribute.Name;
					}
					if (str1 == string.Empty)
					{
						str1 = name1;
					}
					if (empty2 == string.Empty)
					{
						empty2 = name1;
					}
					XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//Pg");
					for (int i = 0; i < xmlNodeLists.Count; i++)
					{
						XmlNodeList xmlNodeLists1 = xmlNodeLists[i].SelectNodes("//Pic");
						int num = 0;
						if (Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"] != null && !int.TryParse(Program.iniGSPcLocal.items["DOWNLOAD", "INTERVAL"], out num))
						{
							num = 0;
						}
						string str2 = sLocalPath.Substring(0, sLocalPath.LastIndexOf("\\"));
						foreach (XmlNode xmlNodes1 in xmlNodeLists1)
						{
							if (xmlNodes1.Attributes[name] != null && xmlNodes1.Attributes[str1] != null)
							{
								empty = xmlNodes1.Attributes[name].Value;
								if (!empty.ToUpper().EndsWith(".DJVU") && !empty.ToUpper().EndsWith(".PDF") && !empty.ToUpper().EndsWith(".TIF"))
								{
									empty = string.Concat(empty, ".djvu");
								}
								value = xmlNodes1.Attributes[str1].Value;
								try
								{
									dateTime = Convert.ToDateTime(value, new CultureInfo("en-GB"));
								}
								catch
								{
								}
								flag = false;
								if (!File.Exists(string.Concat(str2, "\\", empty)))
								{
									flag = true;
								}
								else if (num == 0)
								{
									flag = true;
								}
								else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(string.Concat(str2, "/", empty), this.frmParent.ServerId), dateTime, num))
								{
									flag = true;
								}
								if (!arrayLists.Contains(string.Concat(empty, "|*|*|", value)) && flag)
								{
									arrayLists.Add(string.Concat(empty, "|*|*|", value));
								}
							}
							else if (xmlNodes1.Attributes[name] != null)
							{
								empty = xmlNodes1.Attributes[name].Value;
								if (!empty.ToUpper().EndsWith(".DJVU") && !empty.ToUpper().EndsWith(".PDF") && !empty.ToUpper().EndsWith(".TIF"))
								{
									empty = string.Concat(empty, ".djvu");
								}
								if (!arrayLists.Contains(string.Concat(empty, "|*|*|", value)))
								{
									arrayLists.Add(string.Concat(empty, "|*|*|", value));
								}
							}
							if (xmlNodes1.Attributes[empty1] == null || xmlNodes1.Attributes[empty2] == null)
							{
								if (xmlNodes1.Attributes[empty1] == null)
								{
									continue;
								}
								empty = xmlNodes1.Attributes[empty1].Value;
								if (this.frmParent.CompressedDownload)
								{
									if (!empty.ToUpper().EndsWith(".XML") && !empty.ToUpper().EndsWith(".ZIP"))
									{
										empty = string.Concat(empty, ".zip");
									}
									else if (empty.ToUpper().EndsWith(".XML"))
									{
										empty = empty.Replace(".xml", ".zip");
									}
								}
								if (!empty.ToUpper().EndsWith(".XML") && !empty.ToUpper().EndsWith(".PDF") && !empty.ToUpper().EndsWith(".TIF"))
								{
									empty = string.Concat(empty, ".xml");
								}
								if (arrayLists.Contains(string.Concat(empty, "|*|*|", value)))
								{
									continue;
								}
								arrayLists.Add(string.Concat(empty, "|*|*|", value));
							}
							else
							{
								empty = xmlNodes1.Attributes[empty1].Value;
								if (!this.frmParent.CompressedDownload)
								{
									if (!empty.ToUpper().EndsWith(".XML") && !empty.ToUpper().EndsWith(".PDF") && !empty.ToUpper().EndsWith(".TIF"))
									{
										empty = string.Concat(empty, ".xml");
									}
								}
								else if (!empty.ToUpper().EndsWith(".XML") && !empty.ToUpper().EndsWith(".ZIP"))
								{
									empty = string.Concat(empty, ".zip");
								}
								else if (empty.ToUpper().EndsWith(".XML"))
								{
									empty = empty.Replace(".xml", ".zip");
								}
								value = xmlNodes1.Attributes[empty2].Value;
								try
								{
									dateTime = Convert.ToDateTime(value, new CultureInfo("en-GB"));
								}
								catch
								{
								}
								flag1 = false;
								if (!File.Exists(string.Concat(str2, "/", empty)))
								{
									flag1 = true;
								}
								else if (num == 0)
								{
									flag1 = true;
								}
								else if (num < 1000 && Global.IntervalElapsed(Global.GetLocalDateOfFile(string.Concat(str2, "/", empty), this.frmParent.ServerId), dateTime, num))
								{
									flag1 = true;
								}
								if (arrayLists.Contains(string.Concat(empty, "|*|*|", value)) || !flag1)
								{
									continue;
								}
								arrayLists.Add(string.Concat(empty, "|*|*|", value));
							}
						}
					}
				}
			}
			catch (XmlException xmlException)
			{
				try
				{
					File.Delete(sLocalPath);
				}
				catch
				{
				}
			}
			catch
			{
			}
			return arrayLists;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='MULTIPLE_BOOKS_DOWNLOAD']");
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

		private void HideLoading(Panel parentPanel)
		{
			try
			{
				this.picLoading.Hide();
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{
			this.pnlForm = new Panel();
			this.pnlControl = new Panel();
			this.splitContainer1 = new SplitContainer();
			this.chkboxSelectAll = new CheckBox();
			this.bookListView = new ListView();
			this.columnHeader1 = new ColumnHeader();
			this.picLoading = new PictureBox();
			this.listBooks = new ListView();
			this.colHeadFileName = new ColumnHeader();
			this.colHeadStatus = new ColumnHeader();
			this.lblCurrentProgress = new Label();
			this.lblOverallProgress = new Label();
			this.lblCurrentPictureDownload = new Label();
			this.lblDownloaded = new Label();
			this.btnCancel = new Button();
			this.btnDownload = new Button();
			this.progressOverall = new ProgressBar();
			this.progressCurrentFile = new ProgressBar();
			this.bgWorker = new BackgroundWorker();
			this.bgLoader = new BackgroundWorker();
			this.ssStatus = new StatusStrip();
			this.lblStatus = new ToolStripStatusLabel();
			this.pnlForm.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			this.ssStatus.SuspendLayout();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(2, 2);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(500, 344);
			this.pnlForm.TabIndex = 0;
			this.pnlControl.Controls.Add(this.splitContainer1);
			this.pnlControl.Controls.Add(this.lblCurrentProgress);
			this.pnlControl.Controls.Add(this.lblOverallProgress);
			this.pnlControl.Controls.Add(this.lblCurrentPictureDownload);
			this.pnlControl.Controls.Add(this.lblDownloaded);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Controls.Add(this.btnDownload);
			this.pnlControl.Controls.Add(this.progressOverall);
			this.pnlControl.Controls.Add(this.progressCurrentFile);
			this.pnlControl.Dock = DockStyle.Fill;
			this.pnlControl.Location = new Point(0, 0);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(10);
			this.pnlControl.Size = new System.Drawing.Size(498, 342);
			this.pnlControl.TabIndex = 1;
			this.splitContainer1.Location = new Point(13, 93);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.chkboxSelectAll);
			this.splitContainer1.Panel1.Controls.Add(this.bookListView);
			this.splitContainer1.Panel2.Controls.Add(this.picLoading);
			this.splitContainer1.Panel2.Controls.Add(this.listBooks);
			this.splitContainer1.Size = new System.Drawing.Size(471, 233);
			this.splitContainer1.SplitterDistance = 157;
			this.splitContainer1.TabIndex = 35;
			this.chkboxSelectAll.AutoSize = true;
			this.chkboxSelectAll.BackColor = SystemColors.Control;
			this.chkboxSelectAll.Location = new Point(8, 3);
			this.chkboxSelectAll.Name = "chkboxSelectAll";
			this.chkboxSelectAll.Size = new System.Drawing.Size(70, 17);
			this.chkboxSelectAll.TabIndex = 36;
			this.chkboxSelectAll.Text = "Select All";
			this.chkboxSelectAll.UseVisualStyleBackColor = false;
			this.chkboxSelectAll.MouseDown += new MouseEventHandler(this.chkboxSelectAll_MouseDown);
			this.chkboxSelectAll.CheckedChanged += new EventHandler(this.chkboxSelectAll_CheckedChanged);
			this.bookListView.BackColor = Color.White;
			this.bookListView.CheckBoxes = true;
			this.bookListView.Columns.AddRange(new ColumnHeader[] { this.columnHeader1 });
			this.bookListView.Dock = DockStyle.Fill;
			this.bookListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.bookListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.bookListView.Location = new Point(0, 0);
			this.bookListView.Name = "bookListView";
			this.bookListView.ShowGroups = false;
			this.bookListView.Size = new System.Drawing.Size(157, 233);
			this.bookListView.TabIndex = 30;
			this.bookListView.UseCompatibleStateImageBehavior = false;
			this.bookListView.View = View.Details;
			this.bookListView.Resize += new EventHandler(this.bookListView_Resize);
			this.bookListView.ItemChecked += new ItemCheckedEventHandler(this.bookListView_ItemChecked);
			this.bookListView.MouseMove += new MouseEventHandler(this.bookListView_MouseMove);
			this.bookListView.ColumnWidthChanging += new ColumnWidthChangingEventHandler(this.bookListView_ColumnWidthChanging);
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 153;
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(143, 103);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(34, 34);
			this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
			this.picLoading.TabIndex = 26;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.listBooks.BackColor = Color.White;
			ListView.ColumnHeaderCollection columns = this.listBooks.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.colHeadFileName, this.colHeadStatus };
			columns.AddRange(columnHeaderArray);
			this.listBooks.Dock = DockStyle.Fill;
			this.listBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.listBooks.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			this.listBooks.Location = new Point(0, 0);
			this.listBooks.Name = "listBooks";
			this.listBooks.Size = new System.Drawing.Size(310, 233);
			this.listBooks.TabIndex = 27;
			this.listBooks.UseCompatibleStateImageBehavior = false;
			this.listBooks.View = View.Details;
			this.listBooks.ColumnWidthChanging += new ColumnWidthChangingEventHandler(this.listBooks_ColumnWidthChanging);
			this.colHeadFileName.Text = "File Name";
			this.colHeadFileName.Width = 204;
			this.colHeadStatus.Text = "Status";
			this.colHeadStatus.Width = 102;
			this.lblCurrentProgress.AutoSize = true;
			this.lblCurrentProgress.Location = new Point(348, 5);
			this.lblCurrentProgress.Name = "lblCurrentProgress";
			this.lblCurrentProgress.Size = new System.Drawing.Size(0, 13);
			this.lblCurrentProgress.TabIndex = 34;
			this.lblOverallProgress.AutoSize = true;
			this.lblOverallProgress.Location = new Point(13, 41);
			this.lblOverallProgress.Name = "lblOverallProgress";
			this.lblOverallProgress.Size = new System.Drawing.Size(0, 13);
			this.lblOverallProgress.TabIndex = 33;
			this.lblCurrentPictureDownload.AutoSize = true;
			this.lblCurrentPictureDownload.Location = new Point(13, 3);
			this.lblCurrentPictureDownload.Name = "lblCurrentPictureDownload";
			this.lblCurrentPictureDownload.Size = new System.Drawing.Size(0, 13);
			this.lblCurrentPictureDownload.TabIndex = 32;
			this.lblDownloaded.AutoSize = true;
			this.lblDownloaded.Location = new Point(262, 2);
			this.lblDownloaded.Name = "lblDownloaded";
			this.lblDownloaded.Size = new System.Drawing.Size(0, 13);
			this.lblDownloaded.TabIndex = 5;
			this.btnCancel.Location = new Point(409, 55);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Close";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnDownload.Location = new Point(409, 15);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(75, 23);
			this.btnDownload.TabIndex = 3;
			this.btnDownload.Text = "Download";
			this.btnDownload.UseVisualStyleBackColor = true;
			this.btnDownload.Click += new EventHandler(this.btnDownload_Click);
			this.progressOverall.Location = new Point(12, 55);
			this.progressOverall.Name = "progressOverall";
			this.progressOverall.Size = new System.Drawing.Size(375, 19);
			this.progressOverall.TabIndex = 1;
			this.progressCurrentFile.Location = new Point(12, 19);
			this.progressCurrentFile.Name = "progressCurrentFile";
			this.progressCurrentFile.Size = new System.Drawing.Size(375, 19);
			this.progressCurrentFile.TabIndex = 0;
			this.bgWorker.WorkerReportsProgress = true;
			this.bgWorker.WorkerSupportsCancellation = true;
			this.bgWorker.DoWork += new DoWorkEventHandler(this.bgWorker_DoWork);
			this.bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
			this.bgWorker.ProgressChanged += new ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
			this.bgLoader.DoWork += new DoWorkEventHandler(this.bgLoader_DoWork);
			this.bgLoader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgLoader_RunWorkerCompleted);
			this.ssStatus.Items.AddRange(new ToolStripItem[] { this.lblStatus });
			this.ssStatus.Location = new Point(2, 346);
			this.ssStatus.Name = "ssStatus";
			this.ssStatus.Size = new System.Drawing.Size(500, 22);
			this.ssStatus.TabIndex = 27;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(38, 17);
			this.lblStatus.Text = "Ready";
			this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(504, 370);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.ssStatus);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmMultipleBooksDownload";
			base.Padding = new System.Windows.Forms.Padding(2);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Multiple Books Download";
			base.Load += new EventHandler(this.frmAllBooksDownload_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmAllBooksDownload_FormClosing);
			this.pnlForm.ResumeLayout(false);
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			((ISupportInitialize)this.picLoading).EndInit();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void InitliazeProgressbars()
		{
			this.progressCurrentFile.Minimum = 0;
			this.progressCurrentFile.Maximum = 100;
			this.progressCurrentFile.Value = 0;
		}

		private void ListAddItem(ListViewItem item)
		{
			if (this.listBooks.InvokeRequired)
			{
				ListView listView = this.listBooks;
				frmMultipleBooksDownload.ListAddItemDelegate listAddItemDelegate = new frmMultipleBooksDownload.ListAddItemDelegate(this.ListAddItem);
				object[] objArray = new object[] { item };
				listView.Invoke(listAddItemDelegate, objArray);
				return;
			}
			if (item.Text.Contains("["))
			{
				item.Font = new System.Drawing.Font(this.listBooks.Font.FontFamily, this.listBooks.Font.Size, FontStyle.Bold, GraphicsUnit.Point, 0);
			}
			this.listBooks.Items.Add(item);
		}

		private void listBooks_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				e.Cancel = true;
				e.NewWidth = this.listBooks.Columns[1].Width;
			}
		}

		private void ListClearItems()
		{
			if (!this.listBooks.InvokeRequired)
			{
				this.listBooks.Items.Clear();
				return;
			}
			this.listBooks.Invoke(new frmMultipleBooksDownload.ListClearItemsDelegate(this.ListClearItems));
		}

		private void ListEditStatus(int index, string status)
		{
			if (this.listBooks.InvokeRequired)
			{
				ListView listView = this.listBooks;
				frmMultipleBooksDownload.ListEditStatusDelegate listEditStatusDelegate = new frmMultipleBooksDownload.ListEditStatusDelegate(this.ListEditStatus);
				object[] objArray = new object[] { index, status };
				listView.Invoke(listEditStatusDelegate, objArray);
				return;
			}
			if (this.listBooks.Columns.Count > 1)
			{
				this.listBooks.Items[index].SubItems[1].Text = status;
			}
			try
			{
				this.listBooks.EnsureVisible(index);
			}
			catch
			{
			}
		}

		private string ListGetFileDateFromListNodeTag(int index)
		{
			string empty;
			if (this.listBooks.InvokeRequired)
			{
				ListView listView = this.listBooks;
				frmMultipleBooksDownload.getFileDateFromListNodeTagDelegate _getFileDateFromListNodeTagDelegate = new frmMultipleBooksDownload.getFileDateFromListNodeTagDelegate(this.ListGetFileDateFromListNodeTag);
				object[] objArray = new object[] { index };
				return (string)listView.Invoke(_getFileDateFromListNodeTagDelegate, objArray);
			}
			try
			{
				empty = (this.listBooks.Columns.Count <= 0 ? string.Empty : this.listBooks.Items[index].Tag.ToString());
			}
			catch
			{
				empty = string.Empty;
			}
			return empty;
		}

		private string ListGetFileName(int index)
		{
			if (this.listBooks.InvokeRequired)
			{
				ListView listView = this.listBooks;
				frmMultipleBooksDownload.getFileNameDelegate _getFileNameDelegate = new frmMultipleBooksDownload.getFileNameDelegate(this.ListGetFileName);
				object[] objArray = new object[] { index };
				return (string)listView.Invoke(_getFileNameDelegate, objArray);
			}
			if (this.listBooks.Columns.Count <= 0)
			{
				return string.Empty;
			}
			return this.listBooks.Items[index].SubItems[0].Text;
		}

		private void LoadResources()
		{
			this.Text = this.GetResource("Multiple Books Download", "MULTIPLE_BOOKS_DOWNLOAD", ResourceType.TITLE);
			this.btnDownload.Text = this.GetResource("Download", "DOWNLOAD", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
			this.chkboxSelectAll.Text = this.GetResource("Select All", "SELECT_ALL", ResourceType.LIST_VIEW);
			this.listBooks.Columns[0].Text = this.GetResource("File Name", "FILE_NAME", ResourceType.LIST_VIEW);
			this.listBooks.Columns[1].Text = this.GetResource("Status", "STATUS", ResourceType.LIST_VIEW);
			this.lblStatus.Text = this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE);
		}

		private void OverallProgressUpdate(int value)
		{
			if (this.progressOverall.InvokeRequired)
			{
				ProgressBar progressBar = this.progressOverall;
				frmMultipleBooksDownload.OverallProgressUpdateDelegate overallProgressUpdateDelegate = new frmMultipleBooksDownload.OverallProgressUpdateDelegate(this.OverallProgressUpdate);
				object[] objArray = new object[] { value };
				progressBar.Invoke(overallProgressUpdateDelegate, objArray);
				return;
			}
			if (value <= this.progressOverall.Maximum && value >= this.progressOverall.Minimum)
			{
				Label label = this.lblOverallProgress;
				string[] resource = new string[] { this.GetResource("Overall Progress:", "OVERALL_PROGRESS", ResourceType.LABEL), " ", value.ToString(), " / ", null };
				int count = this.listBooks.Items.Count - this.bookCounter;
				resource[4] = count.ToString();
				label.Text = string.Concat(resource);
				this.progressOverall.Value = value;
			}
		}

		private bool ReadSeriesFile()
		{
			bool flag;
			this.BooksList = new ArrayList();
			string empty = string.Empty;
			empty = (!this.frmParent.CompressedDownload ? ".xml" : ".zip");
			try
			{
				if (this.frmParent.CompressedDownload && File.Exists(string.Concat(this.sLocalPath, "\\Series", empty)))
				{
					Global.Unzip(string.Concat(this.sLocalPath, "\\Series", empty));
				}
			}
			catch
			{
			}
			string str = string.Concat(this.sLocalPath, "\\Series.xml");
			XmlDocument xmlDocument = new XmlDocument();
			if (File.Exists(str))
			{
				try
				{
					xmlDocument.Load(str);
				}
				catch
				{
					flag = false;
					return flag;
				}
				if (Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"] != null && Program.iniServers[this.frmParent.ServerId].items["SETTINGS", "DATA_ENCRYPTION"].ToUpper() == "ON")
				{
					try
					{
						string str1 = (new AES()).Decode(xmlDocument.InnerText, "0123456789ABCDEF");
						xmlDocument.DocumentElement.InnerXml = str1;
					}
					catch
					{
						flag = false;
						return flag;
					}
				}
				XmlNode xmlNodes = xmlDocument.SelectSingleNode("//Schema");
				if (xmlNodes == null)
				{
					return false;
				}
				string name = string.Empty;
				string empty1 = string.Empty;
				string name1 = string.Empty;
				foreach (XmlAttribute attribute in xmlNodes.Attributes)
				{
					try
					{
						if (attribute.Value.ToUpper().Equals("PUBLISHINGID"))
						{
							name = attribute.Name;
						}
						else if (attribute.Value.ToUpper().Equals("UPDATEDATE"))
						{
							empty1 = attribute.Name;
						}
						else if (attribute.Value.ToUpper().Equals("BOOKTYPE"))
						{
							name1 = attribute.Name;
						}
					}
					catch
					{
					}
				}
				if (name == string.Empty || empty1 == string.Empty)
				{
					return false;
				}
				XmlNodeList xmlNodeLists = xmlDocument.SelectNodes("//Book");
				if (xmlNodeLists.Count <= 0)
				{
					return false;
				}
				for (int i = 0; i < xmlNodeLists.Count; i++)
				{
					try
					{
						string empty2 = string.Empty;
						if (xmlNodeLists[i].Attributes[name] == null)
						{
							empty2 = string.Concat(empty2, "|");
						}
						else
						{
							empty2 = string.Concat(empty2, xmlNodeLists[i].Attributes[name].Value.ToString(), "|");
							this.bookListView.Items.Add(empty2.ToString().Substring(0, empty2.Length - 1));
						}
						empty2 = (xmlNodeLists[i].Attributes[name1] == null ? string.Concat(empty2, "|") : string.Concat(empty2, xmlNodeLists[i].Attributes[name1].Value.ToString(), "|"));
						if (xmlNodeLists[i].Attributes[empty1] != null)
						{
							empty2 = string.Concat(empty2, xmlNodeLists[i].Attributes[empty1].Value.ToString());
						}
						this.BooksList.Add(empty2.ToString());
					}
					catch
					{
					}
				}
			}
			return true;
		}

		private void ShowLoading(Panel parentPanel)
		{
			try
			{
				this.picLoading.Parent = parentPanel;
				this.picLoading.Left = parentPanel.Width / 2 - this.picLoading.Width / 2;
				this.picLoading.Top = parentPanel.Height / 2 - this.picLoading.Height / 2;
				this.picLoading.BringToFront();
				this.picLoading.Show();
			}
			catch
			{
			}
		}

		private void ShowProxyAuthenticationError()
		{
			frmProxyAuthentication _frmProxyAuthentication = new frmProxyAuthentication()
			{
				Owner = this.frmParent
			};
			if (!this.frmParent.InvokeRequired)
			{
				_frmProxyAuthentication.ShowDialog();
				return;
			}
			this.frmParent.Invoke(new frmMultipleBooksDownload.ShowProxyAuthenticationDelegate(this.ShowProxyAuthenticationError));
		}

		private void ShowProxyAuthWarning()
		{
			if (!base.InvokeRequired)
			{
				this.ShowProxyAuthenticationError();
				return;
			}
			base.Invoke(new frmMultipleBooksDownload.ShowProxyAuthWarningDelegate(this.ShowProxyAuthWarning));
		}

		private void UpdateFileProgress(int prog)
		{
			if (!this.progressCurrentFile.InvokeRequired)
			{
				if (prog >= this.progressCurrentFile.Minimum && prog <= this.progressCurrentFile.Maximum)
				{
					this.progressCurrentFile.Value = prog;
				}
				return;
			}
			ProgressBar progressBar = this.progressCurrentFile;
			frmMultipleBooksDownload.UpdateFileProgressDelegate updateFileProgressDelegate = new frmMultipleBooksDownload.UpdateFileProgressDelegate(this.UpdateFileProgress);
			object[] objArray = new object[] { prog };
			progressBar.Invoke(updateFileProgressDelegate, objArray);
		}

		public void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}

		private void UpdateProgress(string progressStatus)
		{
			if (!this.lblCurrentPictureDownload.InvokeRequired)
			{
				this.lblCurrentPictureDownload.Text = progressStatus;
				return;
			}
			Label label = this.lblCurrentPictureDownload;
			frmMultipleBooksDownload.StatusDelegate statusDelegate = new frmMultipleBooksDownload.StatusDelegate(this.UpdateProgress);
			object[] objArray = new object[] { progressStatus };
			label.Invoke(statusDelegate, objArray);
		}

		public void UpdateProgress(int ProgressValue)
		{
			try
			{
				if (ProgressValue <= this.progressCurrentFile.Maximum && ProgressValue >= this.progressCurrentFile.Minimum)
				{
					this.lblCurrentProgress.Text = string.Concat(ProgressValue.ToString(), "%");
					this.progressCurrentFile.Value = ProgressValue;
				}
			}
			catch
			{
			}
		}

		private void UpdateProgressLabel(int prog)
		{
			if (!this.lblCurrentProgress.InvokeRequired)
			{
				this.lblCurrentProgress.Text = string.Concat(prog.ToString(), "%");
				return;
			}
			ProgressBar progressBar = this.progressCurrentFile;
			frmMultipleBooksDownload.UpdateProgressLabelDelegate updateProgressLabelDelegate = new frmMultipleBooksDownload.UpdateProgressLabelDelegate(this.UpdateProgressLabel);
			object[] objArray = new object[] { prog };
			progressBar.Invoke(updateProgressLabelDelegate, objArray);
		}

		private void UpdateStatus(string status)
		{
			if (!this.ssStatus.InvokeRequired)
			{
				this.lblStatus.Text = status;
				return;
			}
			StatusStrip statusStrip = this.ssStatus;
			frmMultipleBooksDownload.StatusDelegate statusDelegate = new frmMultipleBooksDownload.StatusDelegate(this.UpdateStatus);
			object[] objArray = new object[] { status };
			statusStrip.Invoke(statusDelegate, objArray);
		}

		private delegate void BooksListClearItemsDelegate();

		private delegate bool getBookCheckedStatusDelegate(int index);

		private delegate string getFileDateFromListNodeTagDelegate(int index);

		private delegate string getFileNameDelegate(int index);

		private delegate void HideLoadingDelegate(Panel parentPanel);

		private delegate void ListAddItemDelegate(ListViewItem item);

		private delegate void ListClearItemsDelegate();

		private delegate void ListEditStatusDelegate(int index, string status);

		private delegate void OverallProgressUpdateDelegate(int value);

		private delegate void ProgressStatusDelegate(string progressStatus);

		private delegate void ShowLoadingDelegate(Panel parentPanel);

		private delegate void ShowProxyAuthenticationDelegate();

		private delegate void ShowProxyAuthWarningDelegate();

		private delegate void StatusDelegate(string status);

		private delegate void UpdateFileProgressDelegate(int prog);

		public delegate void UpdateProgressbarsDel(int iProcess);

		public delegate void UpdateProgressDelegate();

		private delegate void UpdateProgressLabelDelegate(int prog);
	}
}
using FileDownloaderDLL;
using GSPcLocalViewer.frmOthers;
using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace GSPcLocalViewer
{
	public class frmSingleBookDownload : Form
	{
		private frmViewer frmParent;

		private string sLocalPath;

		private string sServerPath;

		private bool isWorking;

		private bool isListLoaded;

		private int iSuccessCount;

		private FileDownloader objFileDownloader;

		public frmSingleBookDownload.UpdateProgressbarsDel progrDelegate;

		private IContainer components;

		private Panel pnlForm;

		private BackgroundWorker bgWorker;

		private Panel pnlControl;

		private ProgressBar progressOverall;

		private ProgressBar progressCurrentFile;

		private Panel pnlFiles;

		private Button btnCancel;

		private Button btnDownload;

		private ListView listBooks;

		private ColumnHeader colHeadFileName;

		private ColumnHeader colHeadStatus;

		private Label lblDownloaded;

		private BackgroundWorker bgLoader;

		private PictureBox picLoading;

		private StatusStrip ssStatus;

		private ToolStripStatusLabel lblStatus;

		private Label lblOverallProgress;

		private Label lblImageProgress;

		private Label lblCurrentProgress;

		public frmSingleBookDownload(frmViewer frm, string _sLocalPath, string _sServerPath)
		{
			this.InitializeComponent();
			this.frmParent = frm;
			this.sLocalPath = _sLocalPath;
			this.sServerPath = _sServerPath;
			this.progressOverall.Maximum = 100;
			this.progressOverall.Minimum = 0;
			this.progressOverall.Value = 0;
			this.progressCurrentFile.Value = 0;
			this.UpdateFont();
			this.UpdateStatus(string.Empty);
			this.isWorking = false;
			this.isListLoaded = false;
			this.LoadResources();
		}

		private void bgLoader_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				ArrayList arrayLists = this.frmParent.SingleBookDownloadList(this.sLocalPath);
				this.ListClearItems();
				for (int i = 0; i < arrayLists.Count; i++)
				{
					try
					{
						string str = arrayLists[i].ToString();
						string[] strArrays = new string[] { "|*|*|" };
						string[] strArrays1 = str.Split(strArrays, StringSplitOptions.None);
						string str1 = strArrays1[0];
						string[] empty = new string[] { str1, string.Empty };
						ListViewItem listViewItem = new ListViewItem(empty);
						if ((int)strArrays1.Length == 2)
						{
							listViewItem.Tag = strArrays1[1];
						}
						this.ListAddItem(listViewItem);
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
		}

		private void bgLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.progressCurrentFile.Minimum = 0;
			this.progressCurrentFile.Maximum = 100;
			this.progressCurrentFile.Value = 0;
			this.progressOverall.Minimum = 0;
			this.progressOverall.Maximum = this.listBooks.Items.Count;
			this.progressOverall.Value = 0;
			this.HideLoading(this.pnlFiles);
			this.btnDownload.Enabled = true;
			this.btnCancel.Enabled = true;
			if (this.listBooks.Items.Count != 0)
			{
				this.UpdateStatus(this.GetResource("Ready", "READY", ResourceType.STATUS_MESSAGE));
			}
			else
			{
				this.UpdateStatus(this.GetResource("Book is already updated.", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
			}
			if (!this.isListLoaded)
			{
				this.isListLoaded = true;
				this.progressOverall.Value = 0;
				this.progressCurrentFile.Value = 0;
				this.btnDownload.Enabled = false;
				this.UpdateStatus(string.Empty);
				this.ShowLoading(this.pnlFiles);
				if (!this.isWorking)
				{
					this.isWorking = true;
					this.bgWorker.RunWorkerAsync();
				}
			}
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			string empty = string.Empty;
			this.iSuccessCount = 0;
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
					this.UpdateProgress(string.Concat(this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL), " ", empty));
					Console.WriteLine(string.Concat(this.sLocalPath, "\\", empty));
					if (!this.DownloadFile(string.Concat(this.sServerPath, "/", empty), string.Concat(this.sLocalPath, "\\", empty)))
					{
						this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
					}
					else
					{
						this.ListEditStatus(i, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
						this.iSuccessCount++;
					}
					this.OverallProgressUpdate(i + 1);
					if (DataSize.spaceLeft < (long)10485760)
					{
						this.btnCancel_Click(null, null);
					}
				}
			}
		}

		private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (this.progressCurrentFile.Value != e.ProgressPercentage)
			{
				Label label = this.lblCurrentProgress;
				int progressPercentage = e.ProgressPercentage;
				label.Text = string.Concat(progressPercentage.ToString(), "%");
				this.progressCurrentFile.Value = e.ProgressPercentage;
			}
		}

		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!this.isWorking)
			{
				this.UpdateStatus(this.GetResource("Downloading cancelled.", "DOWNLOADING_CANCELLED", ResourceType.STATUS_MESSAGE));
			}
			else
			{
				this.isWorking = false;
				if (this.listBooks.Items.Count != 0)
				{
					this.UpdateProgress("");
					this.lblCurrentProgress.Text = "";
				}
				else
				{
					this.UpdateStatus(this.GetResource("Book is already updated.", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
				}
			}
			this.isListLoaded = false;
			this.HideLoading(this.pnlFiles);
			this.btnDownload.Enabled = true;
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
			if (DataSize.spaceLeft < (long)10485760)
			{
				MessageBox.Show(this.frmParent, this.GetResource("Book download canceled because maximum download limit was reached", "MAXIMUM_LIMIT_REACHED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.btnCancel_Click(null, null);
				DataSize.miliSecInterval = 20000;
			}
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
				this.btnDownload.Enabled = false;
				this.btnCancel.Enabled = false;
				this.UpdateStatus(this.GetResource("Checking files to download……", "CHECKING_TO_DOWNLOAD", ResourceType.STATUS_MESSAGE));
				this.bgLoader.RunWorkerAsync();
				this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
				return;
			}
			this.progressOverall.Value = 0;
			this.progressCurrentFile.Value = 0;
			this.btnCancel.Text = this.GetResource("Cancel", "CANCEL", ResourceType.BUTTON);
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
			this.bgWorker.ReportProgress(0);
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
										num2 = 0;
										now = DateTime.Now;
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
						num2 = 0;
						now = DateTime.Now;
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
					num2 = 0;
					now = DateTime.Now;
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
			DateTime now;
			TimeSpan timeSpan;
			int i = 0;
			try
			{
				int num = 0;
				int.TryParse(Settings.Default.appProxyTimeOut, out num);
				string empty = string.Empty;
				this.iSuccessCount = 0;
				this.objFileDownloader = new FileDownloader();
				long bytesDownloaded = (long)0;
				int fileLength = 0;
				for (i = 0; i < this.listBooks.Items.Count && this.isWorking; i++)
				{
					if (!base.IsDisposed)
					{
						empty = this.ListGetFileName(i);
						this.UpdateProgress(string.Concat(this.GetResource("Downloading", "DOWNLOADING", ResourceType.LABEL), " ", empty));
						if (Settings.Default.appProxyType != "0")
						{
							int num1 = this.objFileDownloader.DownloadingFileWithProxy(string.Concat(this.sServerPath, "/", empty), string.Concat(this.sLocalPath, "\\", empty), Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
							if (num1 == 407)
							{
								try
								{
									Program.bShowProxyScreen = true;
									if (!Dashbord.bShowProxy)
									{
										this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
									}
									else
									{
										this.ShowProxyAuthWarning();
									}
									while (Program.bShowProxyScreen && Dashbord.bShowProxy)
									{
										num1 = this.objFileDownloader.DownloadingFileWithProxy(string.Concat(this.sServerPath, "/", empty), string.Concat(this.sLocalPath, "\\", empty), Settings.Default.appProxyIP, Settings.Default.appProxyPort, Settings.Default.appProxyLogin, Settings.Default.appProxyPassword, string.Concat(Settings.Default.appProxyTimeOut, "000"));
										if (num1 == 407)
										{
											if (!Dashbord.bShowProxy)
											{
												this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
											}
											else
											{
												this.ShowProxyAuthWarning();
											}
										}
										else if (num1 != 1)
										{
											Program.bShowProxyScreen = false;
											base.Invoke(new frmSingleBookDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
											int num2 = 0;
											now = DateTime.Now;
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
												try
												{
													fileLength = (int)(bytesDownloaded * (long)100 / (long)this.objFileDownloader.get_FileLength());
												}
												catch (Exception exception)
												{
												}
												if (num2 != fileLength)
												{
													num2 = fileLength;
													now = DateTime.Now;
													this.progrDelegate = new frmSingleBookDownload.UpdateProgressbarsDel(this.UpdateProgress);
													frmSingleBookDownload.UpdateProgressbarsDel updateProgressbarsDel = this.progrDelegate;
													object[] objArray = new object[] { fileLength };
													base.Invoke(updateProgressbarsDel, objArray);
												}
												Application.DoEvents();
												timeSpan = DateTime.Now - now;
											}
											while (bytesDownloaded < (long)this.objFileDownloader.get_FileLength() && timeSpan.Seconds < num);
											try
											{
												if (!empty.ToUpper().EndsWith("XML") && !empty.ToUpper().EndsWith("ZIP"))
												{
													Global.ChangeDjVUModifiedDate(string.Concat(this.sLocalPath, "\\", empty), this.ListGetFileDateFromListNodeTag(i));
												}
											}
											catch
											{
											}
											this.ListEditStatus(i, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
											this.iSuccessCount++;
											this.OverallProgressUpdate(i + 1);
											if (DataSize.spaceLeft >= (long)10485760)
											{
												break;
											}
											this.btnCancel_Click(null, null);
											break;
										}
										else
										{
											Program.bShowProxyScreen = false;
											this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
										}
									}
								}
								catch (Exception exception1)
								{
								}
							}
							else if (num1 != 1)
							{
								base.Invoke(new frmSingleBookDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
								int num3 = 0;
								now = DateTime.Now;
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
									try
									{
										fileLength = (int)(bytesDownloaded * (long)100 / (long)this.objFileDownloader.get_FileLength());
									}
									catch (Exception exception2)
									{
									}
									if (num3 != fileLength)
									{
										num3 = fileLength;
										now = DateTime.Now;
										this.progrDelegate = new frmSingleBookDownload.UpdateProgressbarsDel(this.UpdateProgress);
										frmSingleBookDownload.UpdateProgressbarsDel updateProgressbarsDel1 = this.progrDelegate;
										object[] objArray1 = new object[] { fileLength };
										base.Invoke(updateProgressbarsDel1, objArray1);
									}
									Application.DoEvents();
									timeSpan = DateTime.Now - now;
								}
								while (bytesDownloaded < (long)this.objFileDownloader.get_FileLength() && timeSpan.Seconds < num);
								try
								{
									if (!empty.ToUpper().EndsWith("XML") && !empty.ToUpper().EndsWith("ZIP"))
									{
										Global.ChangeDjVUModifiedDate(string.Concat(this.sLocalPath, "\\", empty), this.ListGetFileDateFromListNodeTag(i));
									}
								}
								catch
								{
								}
								this.ListEditStatus(i, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
								this.iSuccessCount++;
								this.OverallProgressUpdate(i + 1);
								if (DataSize.spaceLeft < (long)10485760)
								{
									this.btnCancel_Click(null, null);
								}
							}
							else
							{
								this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
							}
						}
						else if (this.objFileDownloader.DownloadingFile(string.Concat(this.sServerPath, "/", empty), string.Concat(this.sLocalPath, "\\", empty), string.Concat(Settings.Default.appProxyTimeOut, "000")) != 1)
						{
							base.Invoke(new frmSingleBookDownload.UpdateProgressDelegate(this.InitliazeProgressbars));
							int num4 = 0;
							now = DateTime.Now;
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
								fileLength = (int)(bytesDownloaded * (long)100 / (long)this.objFileDownloader.get_FileLength());
								if (num4 != fileLength)
								{
									now = DateTime.Now;
									num4 = fileLength;
									this.progrDelegate = new frmSingleBookDownload.UpdateProgressbarsDel(this.UpdateProgress);
									frmSingleBookDownload.UpdateProgressbarsDel updateProgressbarsDel2 = this.progrDelegate;
									object[] objArray2 = new object[] { fileLength };
									base.Invoke(updateProgressbarsDel2, objArray2);
								}
								Application.DoEvents();
								timeSpan = DateTime.Now - now;
								if (timeSpan.Seconds < num)
								{
									continue;
								}
								Console.WriteLine(string.Concat(this.sServerPath, "/", empty));
							}
							while (bytesDownloaded < (long)this.objFileDownloader.get_FileLength() && timeSpan.Seconds < num);
							try
							{
								if (!empty.ToUpper().EndsWith("XML") && !empty.ToUpper().EndsWith("ZIP"))
								{
									Global.ChangeDjVUModifiedDate(string.Concat(this.sLocalPath, "\\", empty), this.ListGetFileDateFromListNodeTag(i));
								}
							}
							catch
							{
							}
							this.ListEditStatus(i, this.GetResource("Success", "SUCCESS", ResourceType.LIST_VIEW));
							this.iSuccessCount++;
							this.OverallProgressUpdate(i + 1);
							if (DataSize.spaceLeft < (long)10485760)
							{
								this.btnCancel_Click(null, null);
							}
						}
						else
						{
							this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
						}
					}
				}
				base.Invoke(new frmSingleBookDownload.UpdateProgressDelegate(this.DownloadingComplete));
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
				this.ListEditStatus(i, this.GetResource("Failed", "FAILED", ResourceType.LIST_VIEW));
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
				if (this.listBooks.Items.Count != 0)
				{
					this.UpdateProgress("");
					this.lblCurrentProgress.Text = "";
				}
				else
				{
					this.UpdateStatus(this.GetResource("Book is already updated.", "ALREADY_UPDATED", ResourceType.STATUS_MESSAGE));
				}
			}
			this.isListLoaded = false;
			this.btnDownload.Enabled = true;
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
			if (DataSize.spaceLeft < (long)10485760)
			{
				MessageBox.Show(this.frmParent, this.GetResource("Book download canceled because maximum download limit was reached", "MAXIMUM_LIMIT_REACHED", ResourceType.POPUP_MESSAGE), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.btnCancel_Click(null, null);
				DataSize.miliSecInterval = 20000;
			}
		}

		private void frmBookDownload_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.frmParent.Enabled)
			{
				this.frmParent.Enabled = true;
			}
			this.frmParent.HideDimmer();
		}

		private void frmBookDownload_Load(object sender, EventArgs e)
		{
			this.listBooks.Columns[0].Width = 300;
			this.listBooks.Columns[1].Width = 100;
			this.btnDownload.Enabled = false;
			this.btnCancel.Enabled = false;
			this.UpdateStatus(this.GetResource("Checking files to download……", "CHECKING_TO_DOWNLOAD", ResourceType.STATUS_MESSAGE));
			this.ShowLoading(this.pnlFiles);
			this.bgLoader.RunWorkerAsync();
			this.isListLoaded = true;
		}

		private string GetResource(string sDefaultValue, string sKey, ResourceType rType)
		{
			string resourceValue;
			try
			{
				string str = "";
				str = string.Concat(str, "/Screen[@Name='SINGLE_BOOK_DOWNLOAD']");
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
			this.pnlFiles = new Panel();
			this.listBooks = new ListView();
			this.colHeadFileName = new ColumnHeader();
			this.colHeadStatus = new ColumnHeader();
			this.picLoading = new PictureBox();
			this.pnlControl = new Panel();
			this.lblCurrentProgress = new Label();
			this.lblOverallProgress = new Label();
			this.lblImageProgress = new Label();
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
			this.pnlFiles.SuspendLayout();
			((ISupportInitialize)this.picLoading).BeginInit();
			this.pnlControl.SuspendLayout();
			this.ssStatus.SuspendLayout();
			base.SuspendLayout();
			this.pnlForm.BackColor = Color.White;
			this.pnlForm.BorderStyle = BorderStyle.FixedSingle;
			this.pnlForm.Controls.Add(this.pnlFiles);
			this.pnlForm.Controls.Add(this.pnlControl);
			this.pnlForm.Dock = DockStyle.Fill;
			this.pnlForm.Location = new Point(2, 2);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(459, 344);
			this.pnlForm.TabIndex = 0;
			this.pnlFiles.Controls.Add(this.listBooks);
			this.pnlFiles.Controls.Add(this.picLoading);
			this.pnlFiles.Dock = DockStyle.Bottom;
			this.pnlFiles.Location = new Point(0, 88);
			this.pnlFiles.Name = "pnlFiles";
			this.pnlFiles.Padding = new System.Windows.Forms.Padding(10);
			this.pnlFiles.Size = new System.Drawing.Size(457, 254);
			this.pnlFiles.TabIndex = 0;
			this.listBooks.BackColor = Color.White;
			ListView.ColumnHeaderCollection columns = this.listBooks.Columns;
			ColumnHeader[] columnHeaderArray = new ColumnHeader[] { this.colHeadFileName, this.colHeadStatus };
			columns.AddRange(columnHeaderArray);
			this.listBooks.Location = new Point(12, 7);
			this.listBooks.Name = "listBooks";
			this.listBooks.Size = new System.Drawing.Size(444, 234);
			this.listBooks.TabIndex = 2;
			this.listBooks.UseCompatibleStateImageBehavior = false;
			this.listBooks.View = View.Details;
			this.colHeadFileName.Text = "File Name";
			this.colHeadFileName.Width = 206;
			this.colHeadStatus.Text = "Status";
			this.colHeadStatus.Width = 102;
			this.picLoading.BackColor = Color.White;
			this.picLoading.Image = Resources.Loading1;
			this.picLoading.Location = new Point(215, 111);
			this.picLoading.Name = "picLoading";
			this.picLoading.Size = new System.Drawing.Size(34, 34);
			this.picLoading.SizeMode = PictureBoxSizeMode.AutoSize;
			this.picLoading.TabIndex = 26;
			this.picLoading.TabStop = false;
			this.picLoading.Visible = false;
			this.pnlControl.Controls.Add(this.lblCurrentProgress);
			this.pnlControl.Controls.Add(this.lblOverallProgress);
			this.pnlControl.Controls.Add(this.lblImageProgress);
			this.pnlControl.Controls.Add(this.lblDownloaded);
			this.pnlControl.Controls.Add(this.btnCancel);
			this.pnlControl.Controls.Add(this.btnDownload);
			this.pnlControl.Controls.Add(this.progressOverall);
			this.pnlControl.Controls.Add(this.progressCurrentFile);
			this.pnlControl.Dock = DockStyle.Fill;
			this.pnlControl.Location = new Point(0, 0);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Padding = new System.Windows.Forms.Padding(10);
			this.pnlControl.Size = new System.Drawing.Size(457, 342);
			this.pnlControl.TabIndex = 1;
			this.lblCurrentProgress.AutoSize = true;
			this.lblCurrentProgress.Location = new Point(321, 8);
			this.lblCurrentProgress.Name = "lblCurrentProgress";
			this.lblCurrentProgress.Size = new System.Drawing.Size(0, 13);
			this.lblCurrentProgress.TabIndex = 8;
			this.lblOverallProgress.AutoSize = true;
			this.lblOverallProgress.Location = new Point(14, 45);
			this.lblOverallProgress.Name = "lblOverallProgress";
			this.lblOverallProgress.Size = new System.Drawing.Size(0, 13);
			this.lblOverallProgress.TabIndex = 7;
			this.lblImageProgress.AutoSize = true;
			this.lblImageProgress.Location = new Point(14, 8);
			this.lblImageProgress.Name = "lblImageProgress";
			this.lblImageProgress.Size = new System.Drawing.Size(0, 13);
			this.lblImageProgress.TabIndex = 6;
			this.lblDownloaded.AutoSize = true;
			this.lblDownloaded.Location = new Point(262, 2);
			this.lblDownloaded.Name = "lblDownloaded";
			this.lblDownloaded.Size = new System.Drawing.Size(0, 13);
			this.lblDownloaded.TabIndex = 5;
			this.btnCancel.Location = new Point(376, 59);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Close";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			this.btnDownload.Location = new Point(376, 22);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(75, 23);
			this.btnDownload.TabIndex = 3;
			this.btnDownload.Text = "Download";
			this.btnDownload.UseVisualStyleBackColor = true;
			this.btnDownload.Click += new EventHandler(this.btnDownload_Click);
			this.progressOverall.Location = new Point(12, 61);
			this.progressOverall.Name = "progressOverall";
			this.progressOverall.Size = new System.Drawing.Size(344, 19);
			this.progressOverall.TabIndex = 1;
			this.progressCurrentFile.Location = new Point(12, 24);
			this.progressCurrentFile.Name = "progressCurrentFile";
			this.progressCurrentFile.Size = new System.Drawing.Size(344, 19);
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
			this.ssStatus.Size = new System.Drawing.Size(459, 22);
			this.ssStatus.TabIndex = 27;
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(38, 17);
			this.lblStatus.Text = "Ready";
			this.lblStatus.TextAlign = ContentAlignment.MiddleLeft;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.ClientSize = new System.Drawing.Size(463, 370);
			base.Controls.Add(this.pnlForm);
			base.Controls.Add(this.ssStatus);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSingleBookDownload";
			base.Padding = new System.Windows.Forms.Padding(2);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Single Book Download";
			base.Load += new EventHandler(this.frmBookDownload_Load);
			base.FormClosing += new FormClosingEventHandler(this.frmBookDownload_FormClosing);
			this.pnlForm.ResumeLayout(false);
			this.pnlFiles.ResumeLayout(false);
			this.pnlFiles.PerformLayout();
			((ISupportInitialize)this.picLoading).EndInit();
			this.pnlControl.ResumeLayout(false);
			this.pnlControl.PerformLayout();
			this.ssStatus.ResumeLayout(false);
			this.ssStatus.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public void InitliazeProgressbars()
		{
			this.progressCurrentFile.Minimum = 0;
			this.progressCurrentFile.Maximum = 100;
		}

		private void ListAddItem(ListViewItem item)
		{
			if (!this.listBooks.InvokeRequired)
			{
				this.listBooks.Items.Add(item);
				return;
			}
			ListView listView = this.listBooks;
			frmSingleBookDownload.ListAddItemDelegate listAddItemDelegate = new frmSingleBookDownload.ListAddItemDelegate(this.ListAddItem);
			object[] objArray = new object[] { item };
			listView.Invoke(listAddItemDelegate, objArray);
		}

		private void ListClearItems()
		{
			if (!this.listBooks.InvokeRequired)
			{
				this.listBooks.Items.Clear();
				return;
			}
			this.listBooks.Invoke(new frmSingleBookDownload.ListClearItemsDelegate(this.ListClearItems));
		}

		private void ListEditStatus(int index, string status)
		{
			if (this.listBooks.InvokeRequired)
			{
				ListView listView = this.listBooks;
				frmSingleBookDownload.ListEditStatusDelegate listEditStatusDelegate = new frmSingleBookDownload.ListEditStatusDelegate(this.ListEditStatus);
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
				frmSingleBookDownload.getFileDateFromListNodeTagDelegate _getFileDateFromListNodeTagDelegate = new frmSingleBookDownload.getFileDateFromListNodeTagDelegate(this.ListGetFileDateFromListNodeTag);
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
				frmSingleBookDownload.getFileNameDelegate _getFileNameDelegate = new frmSingleBookDownload.getFileNameDelegate(this.ListGetFileName);
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
			this.Text = this.GetResource("Single Book Download", "SINGLE_BOOK_DOWNLOAD", ResourceType.TITLE);
			this.btnDownload.Text = this.GetResource("Download", "DOWNLOAD", ResourceType.BUTTON);
			this.btnCancel.Text = this.GetResource("Close", "CLOSE", ResourceType.BUTTON);
			this.listBooks.Columns[0].Text = this.GetResource("File Name", "FILE_NAME", ResourceType.LIST_VIEW);
			this.listBooks.Columns[1].Text = this.GetResource("Status", "STATUS", ResourceType.LIST_VIEW);
			this.lblStatus.Text = this.GetResource("Ready", "READY", ResourceType.LABEL);
		}

		private void OverallProgressUpdate(int value)
		{
			if (this.progressOverall.InvokeRequired)
			{
				ProgressBar progressBar = this.progressOverall;
				frmSingleBookDownload.OverallProgressUpdateDelegate overallProgressUpdateDelegate = new frmSingleBookDownload.OverallProgressUpdateDelegate(this.OverallProgressUpdate);
				object[] objArray = new object[] { value };
				progressBar.Invoke(overallProgressUpdateDelegate, objArray);
				return;
			}
			Label label = this.lblOverallProgress;
			object[] resource = new object[] { this.GetResource("Overall Progress:", "OVERALL_PROGRESS", ResourceType.LABEL), value.ToString(), " / ", this.listBooks.Items.Count };
			label.Text = string.Concat(resource);
			this.progressOverall.Value = value;
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
			this.frmParent.Invoke(new frmSingleBookDownload.ShowProxyAuthenticationDelegate(this.ShowProxyAuthenticationError));
		}

		private void ShowProxyAuthWarning()
		{
			if (!base.InvokeRequired)
			{
				this.ShowProxyAuthenticationError();
				return;
			}
			base.Invoke(new frmSingleBookDownload.ShowProxyAuthWarningDelegate(this.ShowProxyAuthWarning));
		}

		public void UpdateFont()
		{
			this.Font = Settings.Default.appFont;
		}

		private void UpdateProgress(string progressStatus)
		{
			if (!this.lblImageProgress.InvokeRequired)
			{
				this.lblImageProgress.Text = progressStatus;
				return;
			}
			Label label = this.lblImageProgress;
			frmSingleBookDownload.StatusDelegate statusDelegate = new frmSingleBookDownload.StatusDelegate(this.UpdateProgress);
			object[] objArray = new object[] { progressStatus };
			label.Invoke(statusDelegate, objArray);
		}

		public void UpdateProgress(int ProgressValue)
		{
			this.lblCurrentProgress.Text = string.Concat(ProgressValue.ToString(), "%");
			this.progressCurrentFile.Value = ProgressValue;
			this.progressCurrentFile.PerformStep();
		}

		private void UpdateStatus(string status)
		{
			if (!this.ssStatus.InvokeRequired)
			{
				this.lblStatus.Text = status;
				return;
			}
			StatusStrip statusStrip = this.ssStatus;
			frmSingleBookDownload.StatusDelegate statusDelegate = new frmSingleBookDownload.StatusDelegate(this.UpdateStatus);
			object[] objArray = new object[] { status };
			statusStrip.Invoke(statusDelegate, objArray);
		}

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

		public delegate void UpdateProgressbarsDel(int iProcess);

		public delegate void UpdateProgressDelegate();
	}
}
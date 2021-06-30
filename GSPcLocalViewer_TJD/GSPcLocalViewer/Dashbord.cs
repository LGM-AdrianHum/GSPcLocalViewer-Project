using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Pipes;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace GSPcLocalViewer
{
	public class Dashbord : Form
	{
		private const uint SWP_NOSIZE = 1;

		private const uint SWP_NOMOVE = 2;

		private const uint TOPMOST_FLAGS = 3;

		private IContainer components;

		private Label lblDashbord;

		private BackgroundWorker bgWorker_PIPES;

		private NotifyIcon notifyIcon1;

		private BackgroundWorker bgWorker_SLWebInterface;

		private frmViewer objFrmViewer;

		private ArrayList arrViewer;

		private NamedPipeServerStream pipeServer;

		private Thread thDataSize;

		private NamedPipeServerStream pipeServerPage;

		private string strMessage = "";

		private readonly static IntPtr HWND_TOPMOST;

		private readonly static IntPtr HWND_NOTOPMOST;

		public static bool bShowProxy;

		private string strPageInfo = "";

		private int nodeid = 1;

		protected override System.Windows.Forms.CreateParams CreateParams
		{
			get
			{
				System.Windows.Forms.CreateParams createParams = base.CreateParams;
				System.Windows.Forms.CreateParams exStyle = createParams;
				exStyle.ExStyle = exStyle.ExStyle | 128;
				return createParams;
			}
		}

		static Dashbord()
		{
			Dashbord.HWND_TOPMOST = new IntPtr(-1);
			Dashbord.HWND_NOTOPMOST = new IntPtr(-2);
			Dashbord.bShowProxy = true;
		}

		public Dashbord()
		{
			this.InitializeComponent();
			base.Visible = false;
			this.arrViewer = new ArrayList();
			try
			{
				this.bgWorker_PIPES.RunWorkerAsync();
				this.bgWorker_SLWebInterface.RunWorkerAsync();
				if (DataSize.IsDataSizeApplied())
				{
					this.thDataSize = new Thread(new ThreadStart(this.CheckDataSize));
					this.thDataSize.Start();
				}
			}
			catch
			{
			}
		}

		public void AddNewRecord(DataGridViewRow dRow)
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmSelectionlist.AddNewRecord(dRow);
				}
			}
			catch
			{
			}
		}

		private string AddPartToSelectionInterface(string sPart, string sQuantity)
		{
			DataGridViewColumnCollection columns = this.GetSelectionListGrid().Columns;
			DataGridView dataGridView = new DataGridView();
			for (int i = 0; i < columns.Count; i++)
			{
				dataGridView.Columns.Add(columns[i].Name, columns[i].HeaderText);
			}
			DataGridViewColumn dataGridViewColumn = new DataGridViewColumn()
			{
				ToolTipText = "HIDDEN",
				Name = "PART_SLIST_KEY",
				Tag = "PART_SLIST_KEY"
			};
			dataGridView.Columns.Add(dataGridViewColumn);
			DataGridViewRow dataGridViewRow = new DataGridViewRow();
			dataGridViewRow.CreateCells(dataGridView);
			for (int j = 0; j < dataGridViewRow.Cells.Count; j++)
			{
				if (dataGridView.Columns[j].Name.ToUpper().Equals("PARTNUMBER"))
				{
					if (((frmViewer)this.arrViewer[0]).PartInSelectionList(sPart))
					{
						string str = "";
						foreach (DataGridViewRow row in (IEnumerable)this.GetSelectionListGrid().Rows)
						{
							if (row.Cells["PARTNUMBER"].Value.ToString() != sPart)
							{
								continue;
							}
							str = row.Cells["QTY"].Value.ToString();
						}
						sQuantity = string.Concat(int.Parse(str) + int.Parse(sQuantity));
						this.ChangeQuantity(sPart, sQuantity);
						return sQuantity;
					}
					dataGridViewRow.Cells["PART_SLIST_KEY"].Value = sPart;
					dataGridViewRow.Cells[j].Value = sPart;
				}
				else if (!dataGridView.Columns[j].Name.ToUpper().Equals("QTY"))
				{
					dataGridViewRow.Cells[j].Value = string.Empty;
				}
				else
				{
					dataGridViewRow.Cells[j].Value = sQuantity;
				}
			}
			dataGridView.Rows.Add(dataGridViewRow);
			dataGridView.EndEdit();
			this.AddNewRecord(dataGridView.Rows[0]);
			return sQuantity;
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		private void bgWorker_PIPES_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
				NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream("0123456789ABCDEF", PipeDirection.Out);
				NamedPipeServerStream namedPipeServerStream1 = namedPipeServerStream;
				this.pipeServer = namedPipeServerStream;
				using (namedPipeServerStream1)
				{
					this.pipeServer.WaitForConnection();
					try
					{
						DataGridView dataGridView = new DataGridView();
						DataGridView selectionList = new DataGridView();
						selectionList = ((frmViewer)this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
						foreach (DataGridViewColumn column in selectionList.Columns)
						{
							dataGridView.Columns.Add(column.Clone() as DataGridViewColumn);
						}
						foreach (DataGridViewRow row in (IEnumerable)selectionList.Rows)
						{
							int num = dataGridView.Rows.Add(row.Clone() as DataGridViewRow);
							foreach (DataGridViewCell cell in row.Cells)
							{
								dataGridView.Rows[num].Cells[cell.ColumnIndex].Value = cell.Value;
							}
						}
						int count = dataGridView.Columns.Count;
						for (int i = 0; i < count; i++)
						{
							IniFileIO iniFileIO = new IniFileIO();
							ArrayList arrayLists = new ArrayList();
							string empty = string.Empty;
							string str = string.Concat(Application.StartupPath, "\\GSP_", Program.iniServers[this.objFrmViewer.ServerId].sIniKey, ".ini");
							arrayLists = iniFileIO.GetKeys(str, "SLIST_SETTINGS");
							for (int j = 0; j < arrayLists.Count; j++)
							{
								if (arrayLists[j].ToString() == dataGridView.Columns[i].Tag.ToString())
								{
									dataGridView.Columns[i].Visible = true;
									empty = iniFileIO.GetKeyValue("SLIST_SETTINGS", arrayLists[j].ToString().ToUpper(), str);
									char[] chrArray = new char[] { '|' };
									if ((int)empty.Split(chrArray).Length != 3)
									{
										char[] chrArray1 = new char[] { '|' };
										if ((int)empty.Split(chrArray1).Length == 4)
										{
											string[] strArrays = new string[] { empty, "|True|True|", null, null, null };
											char[] chrArray2 = new char[] { '|' };
											strArrays[2] = empty.Split(chrArray2)[1];
											strArrays[3] = "|";
											char[] chrArray3 = new char[] { '|' };
											strArrays[4] = empty.Split(chrArray3)[2];
											empty = string.Concat(strArrays);
										}
									}
									else
									{
										string[] strArrays1 = new string[] { empty, "|True|True|", null, null, null };
										char[] chrArray4 = new char[] { '|' };
										strArrays1[2] = empty.Split(chrArray4)[1];
										strArrays1[3] = "|";
										char[] chrArray5 = new char[] { '|' };
										strArrays1[4] = empty.Split(chrArray5)[2];
										empty = string.Concat(strArrays1);
									}
									char[] chrArray6 = new char[] { '|' };
									if (empty.Split(chrArray6)[4].ToString() == "False")
									{
										dataGridView.Columns[i].Visible = false;
									}
								}
							}
						}
						DataSet dataSet = new DataSet();
						dataSet = this.SLDataGridViewToDataTable(dataGridView);
						using (StreamWriter streamWriter = new StreamWriter(this.pipeServer))
						{
							dataSet.WriteXml(streamWriter);
						}
					}
					catch (IOException oException)
					{
						Console.WriteLine("ERROR: {0}", oException.Message);
					}
				}
			}
			catch
			{
			}
		}

		private void bgWorker_PIPES_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				this.bgWorker_PIPES.RunWorkerAsync();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void bgWorker_SLWebInterface_DoWork(object sender, DoWorkEventArgs e)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			try
			{
				this.SLWebDataSend();
			}
			catch
			{
			}
		}

		private void bgWorker_SLWebInterface_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				this.bgWorker_SLWebInterface.RunWorkerAsync();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern bool BringWindowToTop(HandleRef hWnd);

		public void ChangeApplicationMode(bool bWorkOffline)
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).workOffLineMenuItem.Checked = bWorkOffline;
					if (!bWorkOffline)
					{
						((frmViewer)this.arrViewer[i]).lblMode.ToolTipText = "Working Online";
						((frmViewer)this.arrViewer[i]).lblMode.Image = Resources.online;
					}
					else
					{
						((frmViewer)this.arrViewer[i]).lblMode.ToolTipText = "Working Offline";
						((frmViewer)this.arrViewer[i]).lblMode.Image = Resources.offline;
					}
					((frmViewer)this.arrViewer[i]).tsbSingleBookDownload.Enabled = !bWorkOffline;
					((frmViewer)this.arrViewer[i]).tsbMultipleBooksDownload.Enabled = !bWorkOffline;
					((frmViewer)this.arrViewer[i]).singleBookToolStripMenuItem.Enabled = !bWorkOffline;
					((frmViewer)this.arrViewer[i]).multipleBooksToolStripMenuItem.Enabled = !bWorkOffline;
				}
			}
			catch
			{
			}
		}

		public void ChangeQuantity(string sPartNumber, string sQuantity)
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmSelectionlist.ChangeQuantity(sPartNumber, sQuantity);
				}
			}
			catch
			{
			}
		}

		public void ChangeSelListQuantity(string sPartNumber, string sQuantity)
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmSelectionlist.ChangeQuantity(sPartNumber, sQuantity);
				}
			}
			catch
			{
			}
		}

		private void CheckDataSize()
		{
			try
			{
				while (true)
				{
					Thread.Sleep(DataSize.miliSecInterval);
					DataSize.UpdateSpaceVars();
					DataSize.miliSecInterval = 20000;
				}
			}
			catch
			{
			}
		}

		public void CheckUncheckRow(string iRowNumber, string sPartArguments, bool bCheck)
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmPartlist.CheckUncheckRow(iRowNumber, bCheck);
				}
			}
			catch
			{
			}
		}

		public void CheckUncheckRow(string sPartNumber, bool bCheck)
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmPartlist.CheckUncheckRow(sPartNumber, bCheck);
				}
			}
			catch
			{
			}
		}

		public void ClearSelectionList()
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmSelectionlist.ClearSelectionList();
				}
			}
			catch
			{
			}
		}

		public void CloseAll()
		{
			try
			{
				do
				{
					try
					{
						((frmViewer)this.arrViewer[0]).Close();
					}
					catch
					{
					}
				}
				while (this.arrViewer.Count != 0);
				this.arrViewer.Clear();
				base.Close();
			}
			catch
			{
			}
		}

		public void CloseViewer(frmViewer f)
		{
			if (f == null)
			{
				base.Close();
			}
			else
			{
				if (this.arrViewer.Count > 0)
				{
					this.arrViewer.Remove(f);
				}
				if (this.arrViewer.Count == 0)
				{
					base.Close();
					return;
				}
			}
		}

		private void Dashbord_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.thDataSize != null && this.thDataSize.IsAlive)
			{
				this.thDataSize.Interrupt();
			}
			try
			{
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				folderPath = string.Concat(folderPath, "\\", Application.ProductName);
				foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
				{
					string str = Uri.EscapeDataString(installedPrinter);
					if (!File.Exists(string.Concat(folderPath, "\\", str, ".bin")))
					{
						continue;
					}
					File.Delete(string.Concat(folderPath, "\\", str, ".bin"));
				}
			}
			catch (Exception exception)
			{
			}
		}

		private DataSet DataGridViewToDataTable(DataGridView dGridView)
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add();
			try
			{
				for (int i = 0; i < dGridView.Columns.Count; i++)
				{
					if (dGridView.Columns[i].ToolTipText != "HIDDEN")
					{
						string empty = string.Empty;
						try
						{
							empty = (dGridView.Columns[i].HeaderText == null || dataSet.Tables[0].Columns.Contains(dGridView.Columns[i].HeaderText) ? dGridView.Columns[i].Name : dGridView.Columns[i].HeaderText);
						}
						catch
						{
							empty = string.Empty;
						}
						try
						{
							if (empty != null)
							{
								dataSet.Tables[0].Columns.Add(empty);
							}
						}
						catch
						{
						}
					}
				}
				for (int j = 0; j < dGridView.Rows.Count; j++)
				{
					DataRow str = dataSet.Tables[0].NewRow();
					for (int k = 0; k < dGridView.Columns.Count; k++)
					{
						string empty1 = string.Empty;
						try
						{
							empty1 = (dGridView.Columns[k].HeaderText == null ? dGridView.Columns[k].Name : dGridView.Columns[k].HeaderText);
						}
						catch
						{
							empty1 = string.Empty;
						}
						try
						{
							if (dGridView[k, j].Value == null)
							{
								str[empty1] = string.Empty;
							}
							else
							{
								str[empty1] = dGridView[k, j].Value.ToString();
							}
						}
						catch
						{
						}
					}
					dataSet.Tables[0].Rows.Add(str);
				}
			}
			catch
			{
			}
			return dataSet;
		}

		public void DeleteRow(string sPartNumber)
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmSelectionlist.DeleteRow(sPartNumber);
				}
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

		public void DisposeNotification()
		{
			this.notifyIcon1.Dispose();
		}

		public void FirstTime(string[] args)
		{
			Program.bNoViewerOpen = true;
			this.objFrmViewer = new frmViewer(this);
			this.arrViewer.Add(this.objFrmViewer);
			this.objFrmViewer.SetArguments(args);
			this.objFrmViewer.Show();
			uint windowThreadProcessId = Dashbord.GetWindowThreadProcessId(Dashbord.GetForegroundWindow(), IntPtr.Zero);
			uint currentThreadId = Dashbord.GetCurrentThreadId();
			if (windowThreadProcessId == currentThreadId)
			{
				Dashbord.BringWindowToTop(this.objFrmViewer.Handle);
				Dashbord.ShowWindow(this.objFrmViewer.Handle, 5);
			}
			else
			{
				Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, true);
				Dashbord.BringWindowToTop(this.objFrmViewer.Handle);
				Dashbord.ShowWindow(this.objFrmViewer.Handle, 5);
				Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, false);
			}
			this.objFrmViewer.Activate();
		}

		private string getBookString()
		{
			string empty = string.Empty;
			if (this.arrViewer.Count != 0)
			{
				empty = "<Books>\n";
				ArrayList arrayLists = this.arrViewer;
				for (int i = 0; i < arrayLists.Count; i++)
				{
					XmlNode bookNode = ((frmViewer)arrayLists[i]).BookNode;
					if (bookNode != null)
					{
						XmlNode schemaNode = ((frmViewer)arrayLists[i]).SchemaNode;
						empty = string.Concat(empty, "<Book ");
						empty = string.Concat(empty, this.getXMLString(schemaNode, bookNode));
						empty = string.Concat(empty, "ServerKey=\"", Program.iniServers[((frmViewer)arrayLists[i]).ServerId].sIniKey, "\" ");
						empty = string.Concat(empty, "/>\n");
					}
					else
					{
						empty = string.Concat(empty, "<Book />\n");
					}
				}
				empty = string.Concat(empty, "</Books>");
			}
			return empty;
		}

		public string GetCurrentPageID(ArrayList tempArray, int i)
		{
			if (!((frmViewer)tempArray[i]).CurrentTreeView.InvokeRequired)
			{
				int index = ((frmViewer)tempArray[i]).CurrentTreeView.SelectedNode.Index;
				this.strPageInfo = index.ToString();
			}
			else
			{
				CustomTreeView currentTreeView = ((frmViewer)tempArray[i]).CurrentTreeView;
				Dashbord.GetCurrentPageIDDelegate getCurrentPageIDDelegate = new Dashbord.GetCurrentPageIDDelegate(this.GetCurrentPageID);
				object[] objArray = new object[] { tempArray, i };
				currentTreeView.Invoke(getCurrentPageIDDelegate, objArray);
			}
			return this.strPageInfo;
		}

		public string GetCurrentPageInfo(int i)
		{
			if (!((frmViewer)this.arrViewer[i]).CurrentTreeView.InvokeRequired)
			{
				this.strPageInfo = ((frmViewer)this.arrViewer[i]).CurrentTreeView.SelectedNode.Tag.ToString();
			}
			else
			{
				CustomTreeView currentTreeView = ((frmViewer)this.arrViewer[i]).CurrentTreeView;
				Dashbord.GetCurrentPageInfoDelegate getCurrentPageInfoDelegate = new Dashbord.GetCurrentPageInfoDelegate(this.GetCurrentPageInfo);
				object[] objArray = new object[] { i };
				currentTreeView.Invoke(getCurrentPageInfoDelegate, objArray);
			}
			return this.strPageInfo;
		}

		[DllImport("kernel32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern uint GetCurrentThreadId();

		public string GetCurrentTVNode(ArrayList tempArray, int i)
		{
			if (!((frmViewer)tempArray[i]).CurrentTreeView.InvokeRequired)
			{
				this.strPageInfo = ((frmViewer)tempArray[i]).CurrentTreeView.SelectedNode.Text.ToString();
			}
			else
			{
				CustomTreeView currentTreeView = ((frmViewer)tempArray[i]).CurrentTreeView;
				Dashbord.GetCurrentTVNodeDelegate getCurrentTVNodeDelegate = new Dashbord.GetCurrentTVNodeDelegate(this.GetCurrentTVNode);
				object[] objArray = new object[] { tempArray, i };
				currentTreeView.Invoke(getCurrentTVNodeDelegate, objArray);
			}
			return this.strPageInfo;
		}

		private string getFilters()
		{
			string str = "";
			str = "<Filters>\n";
			for (int i = 0; i < this.arrViewer.Count; i++)
			{
				str = string.Concat(str, "<Book ");
				if (((frmViewer)this.arrViewer[i]).BookNode != null)
				{
					int num = 0;
					while (num < ((frmViewer)this.arrViewer[i]).SchemaNode.Attributes.Count)
					{
						if (!((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[num].Value.ToUpper().Equals("PUBLISHINGID"))
						{
							num++;
						}
						else
						{
							string str1 = "";
							str1 = SecurityElement.Escape(((frmViewer)this.arrViewer[i]).BookNode.Attributes[((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[num].Name].Value);
							if (str1 == "")
							{
								break;
							}
							string str2 = str;
							string[] value = new string[] { str2, ((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[num].Value, "=\"", str1, "\"" };
							str = string.Concat(value);
							break;
						}
					}
					str = string.Concat(str, " ServerKey=\"", Program.iniServers[((frmViewer)this.arrViewer[i]).ServerId].sIniKey, "\">\n");
					str = string.Concat(str, "<Filter");
					string[] filterArgs = ((frmViewer)this.arrViewer[i]).getFilterArgs();
					if (filterArgs != null && (int)filterArgs.Length > 0)
					{
						string[] strArrays = filterArgs;
						for (int j = 0; j < (int)strArrays.Length; j++)
						{
							string str3 = strArrays[j].Replace("\"", "");
							string[] strArrays1 = str3.Split(new char[] { '=' });
							string str4 = str;
							string[] strArrays2 = new string[] { str4, " ", strArrays1[0], "=\"", strArrays1[1], "\"" };
							str = string.Concat(strArrays2);
						}
					}
					str = string.Concat(str, " />\n");
					str = string.Concat(str, "</Book>\n");
				}
				else
				{
					str = string.Concat(str, ">\n<Filter />\n</Book>\n");
				}
			}
			str = string.Concat(str, "</Filters>");
			return str;
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern IntPtr GetForegroundWindow();

		private int GetNodeRecursive(TreeNode treeNode, string name)
		{
			int num;
			if (treeNode.Text.ToUpper().Equals(name.ToUpper()))
			{
				return treeNode.Index;
			}
			IEnumerator enumerator = treeNode.Nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode current = (TreeNode)enumerator.Current;
					this.nodeid++;
					if (this.GetNodeRecursive(current, name) == -1)
					{
						continue;
					}
					num = this.nodeid;
					return num;
				}
				return -1;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return num;
		}

		private string getPageString()
		{
			string str = "";
			string str1 = "";
			if (this.arrViewer.Count != 0)
			{
				try
				{
					str = "<Pages>\n";
					for (int i = 0; i < this.arrViewer.Count; i++)
					{
						XmlDocument xmlDocument = new XmlDocument();
						if (((frmViewer)this.arrViewer[i]).BookNode != null)
						{
							xmlDocument.LoadXml(this.GetCurrentPageInfo(i));
							XmlNode documentElement = xmlDocument.DocumentElement;
							string innerXml = documentElement.InnerXml;
							xmlDocument.LoadXml(((frmViewer)this.arrViewer[i]).CurrentTreeView.Tag.ToString());
							XmlNode xmlNodes = xmlDocument.DocumentElement;
							str = string.Concat(str, "<Page ");
							str = string.Concat(str, this.getXMLString(xmlNodes, documentElement));
							int num = 0;
							while (num < ((frmViewer)this.arrViewer[i]).SchemaNode.Attributes.Count)
							{
								if (!((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[num].Value.ToUpper().Equals("PUBLISHINGID"))
								{
									num++;
								}
								else
								{
									str1 = SecurityElement.Escape(((frmViewer)this.arrViewer[i]).BookNode.Attributes[((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[num].Name].Value);
									if (str1 == "")
									{
										break;
									}
									string str2 = str;
									string[] value = new string[] { str2, "Book", ((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[num].Value, "=\"", str1, "\" " };
									str = string.Concat(value);
									break;
								}
							}
							str = string.Concat(str, "ServerKey=\"", Program.iniServers[((frmViewer)this.arrViewer[i]).ServerId].sIniKey, "\" ");
							str = string.Concat(str, ">\n");
							foreach (XmlNode childNode in documentElement.ChildNodes)
							{
								if (!childNode.Name.ToUpper().Equals("PIC"))
								{
									continue;
								}
								str = string.Concat(str, "<", childNode.Name, " ");
								str = string.Concat(str, this.getXMLString(xmlNodes, childNode));
								str = string.Concat(str, "/>\n");
							}
							str = string.Concat(str, "</Page>\n");
						}
					}
				}
				catch
				{
				}
				if (str.Equals("<Pages>\n"))
				{
					str = string.Concat(str, "<Page />\n");
				}
				str = string.Concat(str, "</Pages>");
			}
			return str;
		}

		public DataGridViewRowCollection GetSelectionList()
		{
			DataGridViewRowCollection rows;
			try
			{
				int num = -1;
				int num1 = 0;
				while (num1 < this.arrViewer.Count - 1)
				{
					if (((frmViewer)this.arrViewer[num1]).sBookType.ToUpper() != "GSP")
					{
						num1++;
					}
					else
					{
						num = num1;
						break;
					}
				}
				rows = ((frmViewer)this.arrViewer[num]).objFrmSelectionlist.dgPartslist.Rows;
			}
			catch
			{
				rows = null;
			}
			return rows;
		}

		public DataGridViewColumnCollection GetSelectionListColumns()
		{
			DataGridViewColumnCollection columns;
			try
			{
				columns = ((frmViewer)this.arrViewer[0]).objFrmSelectionlist.dgPartslist.Columns;
			}
			catch
			{
				columns = null;
			}
			return columns;
		}

		private string GetSelectionListData()
		{
			string empty = string.Empty;
			DataGridView dataGridView = new DataGridView();
			dataGridView = ((frmViewer)this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
			empty = string.Concat(empty, "<Parts>\n");
			for (int i = 0; i < dataGridView.RowCount; i++)
			{
				empty = string.Concat(empty, "<Part ");
				for (int j = 0; j < dataGridView.Columns.Count; j++)
				{
					if (dataGridView.Columns[j].ToolTipText != "HIDDEN" && dataGridView[j, i].Value != null && !(dataGridView[j, i].Value.ToString() == ""))
					{
						string str = string.Empty;
						str = dataGridView[j, i].Value.ToString();
						str = str.Replace("&", "&amp;");
						str = str.Replace("\"", "&quot;");
						str = str.Replace(">", "&gt;");
						str = str.Replace("<", "&lt;");
						str = str.Replace("'", "&apos;");
						if (!dataGridView.Columns[j].Name.ToUpper().Equals("QTY"))
						{
							string str1 = empty;
							string[] name = new string[] { str1, dataGridView.Columns[j].Name, "=\"", str, "\" " };
							empty = string.Concat(name);
						}
						else
						{
							empty = string.Concat(empty, "Quantity=\"", str, "\" ");
						}
					}
				}
				empty = string.Concat(empty, " />\n");
			}
			if (dataGridView.RowCount == 0)
			{
				empty = string.Concat(empty, "<Part />");
			}
			empty = string.Concat(empty, "</Parts>");
			return empty;
		}

		public DataGridView GetSelectionListGrid()
		{
			if (!((frmViewer)this.arrViewer[0]).objFrmSelectionlist.InvokeRequired)
			{
				((frmViewer)this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
			}
			else
			{
				((frmViewer)this.arrViewer[0]).objFrmSelectionlist.Invoke(new Dashbord.GetSelectionListGridDelegate(this.GetSelectionListGrid), new object[0]);
			}
			return ((frmViewer)this.arrViewer[0]).objFrmSelectionlist.GetSelectionList();
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

		private string getXMLString(XmlNode xSchema, XmlNode xNode)
		{
			string str = "";
			try
			{
				for (int i = 0; i < xNode.Attributes.Count; i++)
				{
					int num = 0;
					while (num < xSchema.Attributes.Count)
					{
						if (!xNode.Attributes[i].Name.Equals(xSchema.Attributes[num].Name))
						{
							num++;
						}
						else
						{
							string str1 = str;
							string[] value = new string[] { str1, xSchema.Attributes[num].Value, "=\"", SecurityElement.Escape(xNode.Attributes[xSchema.Attributes[num].Name].Value), "\" " };
							str = string.Concat(value);
							break;
						}
					}
				}
			}
			catch
			{
			}
			return str;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Dashbord));
			this.lblDashbord = new Label();
			this.bgWorker_PIPES = new BackgroundWorker();
			this.notifyIcon1 = new NotifyIcon(this.components);
			this.bgWorker_SLWebInterface = new BackgroundWorker();
			base.SuspendLayout();
			this.lblDashbord.AutoSize = true;
			this.lblDashbord.Font = new System.Drawing.Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblDashbord.Location = new Point(12, 9);
			this.lblDashbord.Name = "lblDashbord";
			this.lblDashbord.Size = new System.Drawing.Size(254, 48);
			this.lblDashbord.TabIndex = 0;
			this.lblDashbord.Text = "MAIN Form\r\nused to launch multiple instances of viewer\r\nthis form is never shown\r\n";
			this.bgWorker_PIPES.DoWork += new DoWorkEventHandler(this.bgWorker_PIPES_DoWork);
			this.bgWorker_PIPES.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_PIPES_RunWorkerCompleted);
			this.notifyIcon1.Text = "notifyIcon1";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.Click += new EventHandler(this.notifyIcon1_Click);
			this.bgWorker_SLWebInterface.DoWork += new DoWorkEventHandler(this.bgWorker_SLWebInterface_DoWork);
			this.bgWorker_SLWebInterface.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgWorker_SLWebInterface_RunWorkerCompleted);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(285, 73);
			base.Controls.Add(this.lblDashbord);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Dashbord";
			base.Opacity = 0;
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Dashbord";
			base.WindowState = FormWindowState.Minimized;
			base.FormClosing += new FormClosingEventHandler(this.Dashbord_FormClosing);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public ArrayList ListOfInUseBooks()
		{
			ArrayList arrayLists = new ArrayList();
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					arrayLists.Add(((frmViewer)this.arrViewer[i]).BookPublishingId);
				}
			}
			catch
			{
			}
			return arrayLists;
		}

		public void NextTime(string[] args)
		{
			Program.bNoViewerOpen = false;
			frmViewer _frmViewer = new frmViewer(this);
			this.arrViewer.Add(_frmViewer);
			_frmViewer.SetArguments(args);
			int num = -1;
			try
			{
				int num1 = 0;
				while (num1 < (int)Program.iniServers.Length)
				{
					if (Program.iniServers[num1].sIniKey.ToUpper() != args[1].ToUpper())
					{
						num1++;
					}
					else
					{
						num = num1;
						break;
					}
				}
			}
			catch
			{
			}
			if (num != -1)
			{
				_frmViewer.SetCurrentServerID(num);
			}
			_frmViewer.Show();
			_frmViewer.objFrmPicture.HighLightText = Program.HighLightText;
			_frmViewer.objFrmPicture.DjVuPageNumber = Program.DjVuPageNumber;
			uint windowThreadProcessId = Dashbord.GetWindowThreadProcessId(Dashbord.GetForegroundWindow(), IntPtr.Zero);
			uint currentThreadId = Dashbord.GetCurrentThreadId();
			if (windowThreadProcessId == currentThreadId)
			{
				Dashbord.BringWindowToTop(_frmViewer.Handle);
				Dashbord.ShowWindow(_frmViewer.Handle, 5);
			}
			else
			{
				Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, true);
				Dashbord.BringWindowToTop(_frmViewer.Handle);
				Dashbord.ShowWindow(_frmViewer.Handle, 5);
				Dashbord.AttachThreadInput(windowThreadProcessId, currentThreadId, false);
			}
			_frmViewer.Activate();
		}

		private void notifyIcon1_Click(object sender, EventArgs e)
		{
			this.notifyIcon1.ShowBalloonTip(5000);
		}

		private int PageIDRecursive(TreeView treeView, string name)
		{
			int num;
			this.nodeid = 0;
			IEnumerator enumerator = treeView.Nodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					TreeNode current = (TreeNode)enumerator.Current;
					this.nodeid++;
					if (this.GetNodeRecursive(current, name) == -1)
					{
						continue;
					}
					num = this.nodeid;
					return num;
				}
				return -1;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return num;
		}

		public bool PartInSelectionListA(string sPartNumber, string sServerKey, string sBookPubId, string sPageId, string sImageIndex, string sListIndex)
		{
			bool flag;
			try
			{
				flag = ((frmViewer)this.arrViewer[0]).objFrmSelectionlist.PartInSelectionList(sPartNumber, sServerKey, sBookPubId, sPageId, sImageIndex, sListIndex);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public void RunDataSizeChecking()
		{
			if (this.thDataSize != null && this.thDataSize.IsAlive)
			{
				this.thDataSize.Interrupt();
				while (this.thDataSize.IsAlive)
				{
				}
			}
			if ((this.thDataSize == null || !this.thDataSize.IsAlive) && DataSize.IsDataSizeApplied())
			{
				this.thDataSize = new Thread(new ThreadStart(this.CheckDataSize));
				this.thDataSize.Start();
			}
		}

		public void SelListAddRemoveRow(int ServerId, XmlNode xSchemaNode, DataGridViewRow dr, bool bAddRow, string sTag)
		{
			try
			{
				if (this.arrViewer.Count > 0)
				{
					for (int i = 0; i < this.arrViewer.Count; i++)
					{
						((frmViewer)this.arrViewer[i]).objFrmSelectionlist.selListAddRemoveRecord(ServerId, xSchemaNode, dr, bAddRow, sTag);
					}
				}
			}
			catch
			{
			}
		}

		public void SetBookType(string SBookType)
		{
			try
			{
				((frmViewer)this.arrViewer[this.arrViewer.Count - 1]).sBookType = SBookType;
			}
			catch
			{
			}
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern IntPtr SetFocus(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		public void ShowNotification(string title, string text, string tooltip)
		{
			if (this.notifyIcon1.Container == null)
			{
				this.notifyIcon1 = new NotifyIcon(this.components);
				this.notifyIcon1.Click += new EventHandler(this.notifyIcon1_Click);
			}
			this.notifyIcon1.Icon = base.Icon;
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.Text = tooltip;
			this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
			this.notifyIcon1.BalloonTipTitle = title;
			this.notifyIcon1.BalloonTipText = text;
			this.notifyIcon1.ShowBalloonTip(5000);
		}

		[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);

		private DataSet SLDataGridViewToDataTable(DataGridView dGridView)
		{
			DataSet dataSet = new DataSet();
			dataSet.Tables.Add();
			List<string> strs = new List<string>();
			try
			{
				for (int i = 0; i < dGridView.Columns.Count; i++)
				{
					if (dGridView.Columns[i].Visible)
					{
						string empty = string.Empty;
						try
						{
							if (dGridView.Columns[i].HeaderText != null && dataSet.Tables[0].Columns.Count == 0)
							{
								empty = dGridView.Columns[i].HeaderText;
							}
							else if (dGridView.Columns[i].HeaderText != null && dataSet.Tables[0].Columns.Count > 0)
							{
								foreach (DataColumn column in dataSet.Tables[0].Columns)
								{
									if (column.ColumnName.ToUpper().Trim() == dGridView.Columns[i].HeaderText.ToUpper().Trim())
									{
										continue;
									}
									empty = dGridView.Columns[i].HeaderText;
									break;
								}
							}
						}
						catch
						{
							empty = string.Empty;
						}
						try
						{
							if (empty != null)
							{
								dataSet.Tables[0].Columns.Add(empty);
								strs.Add(empty);
							}
						}
						catch
						{
						}
					}
				}
				int count = dataSet.Tables[0].Columns.Count;
				for (int j = 0; j < dGridView.Rows.Count - 1; j++)
				{
					DataRow str = dataSet.Tables[0].NewRow();
					for (int k = 0; k < dGridView.Columns.Count; k++)
					{
						bool flag = false;
						string headerText = string.Empty;
						int num = 0;
						while (num < strs.Count)
						{
							if (dGridView.Columns[k].HeaderText.ToUpper() != strs[num].ToUpper())
							{
								num++;
							}
							else
							{
								headerText = dGridView.Columns[k].HeaderText;
								flag = true;
								break;
							}
						}
						if (flag)
						{
							try
							{
								if (dGridView[k, j].Value == null)
								{
									str[headerText] = string.Empty;
								}
								else
								{
									str[headerText] = dGridView[k, j].Value.ToString();
								}
							}
							catch
							{
							}
						}
					}
					dataSet.Tables[0].Rows.Add(str);
				}
			}
			catch
			{
			}
			return dataSet;
		}

		private void SLWebDataSend()
		{
			this.strMessage = "";
			string empty = string.Empty;
			try
			{
				NamedPipeServerStream namedPipeServerStream = new NamedPipeServerStream("universal", PipeDirection.InOut);
				NamedPipeServerStream namedPipeServerStream1 = namedPipeServerStream;
				this.pipeServerPage = namedPipeServerStream;
				using (namedPipeServerStream1)
				{
					this.pipeServerPage.WaitForConnection();
					byte[] numArray = new byte[256];
					this.pipeServerPage.Read(numArray, 0, 256);
					this.strMessage = Encoding.Unicode.GetString(numArray).TrimEnd(new char[1]);
					try
					{
						string[] strArrays = this.strMessage.Split(new char[] { '|' });
						string upper = strArrays[0].ToUpper();
						string str = upper;
						if (upper != null)
						{
							switch (str)
							{
								case "SLST":
								{
									empty = this.GetSelectionListData();
									break;
								}
								case "BINF":
								{
									empty = this.getBookString();
									break;
								}
								case "PINF":
								{
									empty = this.getPageString();
									break;
								}
								case "FLTR":
								{
									empty = this.getFilters();
									break;
								}
								case "ADDP":
								{
									bool flag = false;
									int num = 0;
									while (num < this.arrViewer.Count)
									{
										if (((frmViewer)this.arrViewer[num]).BookType != "GSP")
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
										empty = "0";
										break;
									}
									else
									{
										string[] strArrays1 = this.strMessage.Split(new char[] { '|' });
										empty = this.AddPartToSelectionInterface(strArrays1[1], strArrays1[2]);
										break;
									}
								}
								case "PNAV":
								case "GTPI":
								case "GTPN":
								{
									int num1 = -1;
									int num2 = 0;
									while (num2 < this.arrViewer.Count)
									{
										if (!Program.iniServers[((frmViewer)this.arrViewer[num2]).ServerId].sIniKey.ToUpper().Equals(strArrays[1].ToUpper()))
										{
											num2++;
										}
										else
										{
											num1 = num2;
											break;
										}
									}
									if (num1 <= -1 || num1 >= this.arrViewer.Count)
									{
										break;
									}
									int item = 0;
									ArrayList arrayLists = new ArrayList();
									for (int i = 0; i < this.arrViewer.Count; i++)
									{
										for (int j = 0; j < ((frmViewer)this.arrViewer[i]).SchemaNode.Attributes.Count; j++)
										{
											if (((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[j].Value.ToUpper().Equals("PUBLISHINGID") && ((frmViewer)this.arrViewer[i]).BookNode.Attributes[((frmViewer)this.arrViewer[i]).SchemaNode.Attributes[j].Name].Value.ToUpper().Equals(strArrays[2].ToUpper()))
											{
												arrayLists.Add(null);
												arrayLists[item] = this.arrViewer[i];
												item++;
											}
										}
									}
									for (int k = 0; k < arrayLists.Count; k++)
									{
										if (!(strArrays[3] != "") || k + 1 == int.Parse(strArrays[3]))
										{
											int num3 = 0;
											string upper1 = strArrays[4].ToUpper();
											string str1 = upper1;
											if (upper1 != null)
											{
												if (str1 == "FSRT")
												{
													((frmViewer)arrayLists[k]).objFrmTreeview.SelectFirstNode();
													num3 = this.PageIDRecursive(((frmViewer)arrayLists[k]).CurrentTreeView, this.GetCurrentTVNode(arrayLists, k));
													empty = num3.ToString();
													goto yoyo1;
												}
												else if (str1 == "PREV")
												{
													((frmViewer)arrayLists[k]).objFrmTreeview.SelectPreviousNode();
													num3 = this.PageIDRecursive(((frmViewer)arrayLists[k]).CurrentTreeView, this.GetCurrentTVNode(arrayLists, k));
													empty = num3.ToString();
													goto yoyo1;
												}
												else if (str1 == "NEXT")
												{
													((frmViewer)arrayLists[k]).objFrmTreeview.SelectNextNode();
													num3 = this.PageIDRecursive(((frmViewer)arrayLists[k]).CurrentTreeView, this.GetCurrentTVNode(arrayLists, k));
													empty = num3.ToString();
													goto yoyo1;
												}
												else
												{
													if (str1 != "LAST")
													{
														goto yoyo4;
													}
													((frmViewer)arrayLists[k]).objFrmTreeview.SelectLastNode();
													num3 = this.PageIDRecursive(((frmViewer)arrayLists[k]).CurrentTreeView, this.GetCurrentTVNode(arrayLists, k));
													empty = num3.ToString();
													goto yoyo1;
												}
											}
											if (strArrays[0].ToUpper().Equals("GTPI"))
											{
												num3 = int.Parse(strArrays[4]);
												if (num3 <= 0)
												{
													empty = "0";
												}
												else
												{
													((frmViewer)arrayLists[k]).objFrmTreeview.SelectTreeNode(num3.ToString());
													string currentTVNode = this.GetCurrentTVNode(arrayLists, k);
													int num4 = this.PageIDRecursive(((frmViewer)arrayLists[k]).CurrentTreeView, currentTVNode);
													empty = num4.ToString();
												}
											}
											else if (strArrays[0].ToUpper().Equals("GTPN"))
											{
												num3 = this.PageIDRecursive(((frmViewer)arrayLists[k]).CurrentTreeView, strArrays[4]);
												if (num3 <= 0)
												{
													empty = "0";
												}
												else
												{
													((frmViewer)arrayLists[k]).objFrmTreeview.SelectTreeNode(num3.ToString());
													empty = num3.ToString();
												}
											}
										}
									yoyo1:
									}
									break;
								}
								default:
								{
									goto yoyo0;
								}
							}
						}
						else
						{
							goto yoyo0;
						}
					}
					catch
					{
					}
				yoyo3:
					if (empty == "")
					{
						empty = "0";
					}
					this.WriteToPipe(empty, this.strMessage);
				}
			}
			catch
			{
			}
			return;
		yoyo0:
			empty = "0";
			goto yoyo3;
		}

		public void UncheckAllRows()
		{
			try
			{
				for (int i = 0; i < this.arrViewer.Count; i++)
				{
					((frmViewer)this.arrViewer[i]).objFrmPartlist.UncheckAllRows();
				}
			}
			catch
			{
			}
		}

		private void WriteToPipe(string strDatatoSend, string strMessage)
		{
			if (!strMessage.ToUpper().Equals("SLST"))
			{
				byte[] bytes = Encoding.Unicode.GetBytes(strDatatoSend);
				this.pipeServerPage.Write(bytes, 0, (int)bytes.Length);
			}
			else
			{
				DataSet dataSet = new DataSet();
				dataSet.ReadXml(new XmlTextReader(new StringReader(strDatatoSend)));
				using (StreamWriter streamWriter = new StreamWriter(this.pipeServerPage))
				{
					dataSet.WriteXml(string.Concat(Application.StartupPath, "\\1.xml"));
					dataSet.WriteXml(streamWriter);
				}
			}
		}

		public delegate void FirstTimeDelegate(string[] args);

		public delegate string GetCurrentPageIDDelegate(ArrayList tempArray, int i);

		public delegate string GetCurrentPageInfoDelegate(int i);

		public delegate string GetCurrentTVNodeDelegate(ArrayList tempArray, int i);

		public delegate DataGridView GetSelectionListGridDelegate();

		public delegate void NextTimeDelegate(string[] args);
	}
}
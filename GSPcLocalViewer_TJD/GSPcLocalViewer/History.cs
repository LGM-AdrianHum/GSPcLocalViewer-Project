using GSPcLocalViewer.Properties;
using System;
using System.Collections;
using System.Xml;

namespace GSPcLocalViewer
{
	public class History
	{
		private int CurrentIndex;

		private Hashtable HistoryList;

		private bool DontAddToHistory;

		public bool BackEnable
		{
			get
			{
				if (this.HistoryList.Count > 0 && this.CurrentIndex + 1 > 1)
				{
					return true;
				}
				return false;
			}
		}

		public bool ForwardEnable
		{
			get
			{
				if (this.HistoryList.Count > 0 && this.CurrentIndex + 1 < this.HistoryList.Count)
				{
					return true;
				}
				return false;
			}
		}

		public Hashtable GetHistoryList
		{
			get
			{
				return this.HistoryList;
			}
		}

		public bool ListEnable
		{
			get
			{
				if (this.HistoryList.Count > 0)
				{
					return true;
				}
				return false;
			}
		}

		public History()
		{
			this.CurrentIndex = -1;
			this.HistoryList = new Hashtable();
			this.DontAddToHistory = false;
		}

		public void Add(XmlNode historyNode)
		{
			if (this.DontAddToHistory)
			{
				this.DontAddToHistory = false;
			}
			else
			{
				if (this.HistoryList.Count != this.CurrentIndex + 1)
				{
					for (int i = this.HistoryList.Count - 1; i > this.CurrentIndex; i--)
					{
						this.HistoryList.Remove(i);
					}
					this.CurrentIndex++;
					this.HistoryList.Add(this.CurrentIndex, historyNode);
				}
				else
				{
					this.CurrentIndex++;
					this.HistoryList.Add(this.CurrentIndex, historyNode);
				}
				if (this.HistoryList.Count > Settings.Default.HistorySize)
				{
					Hashtable hashtables = new Hashtable();
					for (int j = 0; j < Settings.Default.HistorySize; j++)
					{
						hashtables.Add(j, this.HistoryList[this.HistoryList.Count - Settings.Default.HistorySize + j]);
					}
					this.HistoryList.Clear();
					this.HistoryList = hashtables;
					this.CurrentIndex = this.HistoryList.Count - 1;
					hashtables = null;
					return;
				}
			}
		}

		public XmlNode Back()
		{
			if (this.CurrentIndex <= 0)
			{
				return null;
			}
			this.CurrentIndex--;
			if (this.HistoryList[this.CurrentIndex] == null)
			{
				return null;
			}
			this.DontAddToHistory = true;
			return (XmlNode)this.HistoryList[this.CurrentIndex];
		}

		public XmlNode Current()
		{
			if (this.CurrentIndex < 0)
			{
				return null;
			}
			if (this.HistoryList[this.CurrentIndex] == null)
			{
				return null;
			}
			this.DontAddToHistory = true;
			return (XmlNode)this.HistoryList[this.CurrentIndex];
		}

		~History()
		{
			this.HistoryList.Clear();
			this.HistoryList = null;
		}

		public XmlNode Forward()
		{
			if (this.CurrentIndex + 1 >= this.HistoryList.Count)
			{
				return null;
			}
			this.CurrentIndex++;
			if (this.HistoryList[this.CurrentIndex] == null)
			{
				return null;
			}
			this.DontAddToHistory = true;
			return (XmlNode)this.HistoryList[this.CurrentIndex];
		}

		public XmlNode Open(int iIndex)
		{
			if (this.HistoryList[iIndex] == null)
			{
				return null;
			}
			this.CurrentIndex = iIndex;
			this.DontAddToHistory = true;
			return (XmlNode)this.HistoryList[iIndex];
		}

		public void ResetHistory()
		{
			this.CurrentIndex = -1;
			this.HistoryList.Clear();
			this.DontAddToHistory = false;
		}
	}
}
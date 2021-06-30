using System;
using System.Collections;
using System.Xml;

namespace GSPcLocalViewer
{
	internal class MemoSession
	{
		private Hashtable globalMemoHash;

		private Hashtable localMemoHash;

		public MemoSession()
		{
			this.globalMemoHash = new Hashtable();
			this.localMemoHash = new Hashtable();
		}

		public void addGlobalMemo(int serverId, XmlDocument xDocGlobalMemo)
		{
			this.globalMemoHash[serverId] = xDocGlobalMemo;
		}

		public void addLocalMemo(int serverId, XmlDocument xDocLocalMemo)
		{
			this.localMemoHash[serverId] = xDocLocalMemo;
		}

		public XmlDocument getGlobalMemoDoc(int serverId)
		{
			return (XmlDocument)this.globalMemoHash[serverId];
		}

		public XmlDocument getLocalMemoDoc(int serverId)
		{
			return (XmlDocument)this.localMemoHash[serverId];
		}
	}
}
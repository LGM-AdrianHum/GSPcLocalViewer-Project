using System;
using System.Runtime.InteropServices;

namespace GSPcLocalViewer
{
	internal class ApplicationMode
	{
		public bool bWorkingOffline;

		public bool bFirstTime;

		public bool bFromPortal;

		public bool InternetConnected
		{
			get
			{
				int num;
				return ApplicationMode.InternetGetConnectedState(out num, 0);
			}
		}

		public ApplicationMode()
		{
			this.bWorkingOffline = !this.InternetConnected;
			this.bFromPortal = false;
			this.bFirstTime = true;
		}

		[DllImport("wininet.dll", CharSet=CharSet.None, ExactSpelling=false)]
		private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);
	}
}
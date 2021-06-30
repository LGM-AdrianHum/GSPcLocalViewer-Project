using Microsoft.Win32;
using System;

namespace GSPcLocalViewer
{
	public class RegistryReader
	{
		public RegistryReader()
		{
		}

		public string Read(string KeyName, string subKey)
		{
			string value;
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(KeyName);
			if (registryKey == null)
			{
				return null;
			}
			try
			{
				value = (string)registryKey.GetValue(subKey);
			}
			catch
			{
				value = null;
			}
			return value;
		}
	}
}
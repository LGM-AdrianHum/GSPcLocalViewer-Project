using System;
using System.Collections.Generic;
using System.Reflection;

namespace GSPcLocalViewer
{
	public class IniDictonary<Tkey1, Tkey2, Tvalue> : Dictionary<SectionKey<Tkey1, Tkey2>, Tvalue>
	{
		public Tvalue this[Tkey1 section, Tkey2 key]
		{
			get
			{
				Tvalue item;
				try
				{
					item = base[new SectionKey<Tkey1, Tkey2>(section, key)];
				}
				catch (KeyNotFoundException keyNotFoundException)
				{
					item = default(Tvalue);
				}
				return item;
			}
			set
			{
				base[new SectionKey<Tkey1, Tkey2>(section, key)] = value;
			}
		}

		public IniDictonary()
		{
		}
	}
}
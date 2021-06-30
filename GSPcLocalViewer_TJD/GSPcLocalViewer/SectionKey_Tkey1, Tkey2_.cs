using System;
using System.Runtime.CompilerServices;

namespace GSPcLocalViewer
{
	public class SectionKey<Tkey1, Tkey2>
	{
		public Tkey1 Key1
		{
			get;
			set;
		}

		public Tkey2 Key2
		{
			get;
			set;
		}

		public SectionKey(Tkey1 key1, Tkey2 key2)
		{
			this.Key1 = key1;
			this.Key2 = key2;
		}

		public override bool Equals(object obj)
		{
			SectionKey<Tkey1, Tkey2> sectionKey = obj as SectionKey<Tkey1, Tkey2>;
			if (sectionKey == null)
			{
				return false;
			}
			if (!this.Key1.Equals(sectionKey.Key1))
			{
				return false;
			}
			return this.Key2.Equals(sectionKey.Key2);
		}

		public override int GetHashCode()
		{
			return this.Key1.GetHashCode() ^ this.Key2.GetHashCode();
		}
	}
}
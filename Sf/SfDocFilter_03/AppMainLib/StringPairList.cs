using System;
using System.Collections;

namespace AppMainLib
{
	/// <summary>
	/// ArrayList mit StringPair-Daten
	/// </summary>
	/// <remarks></remarks>
	public class StringPairList : ArrayList
	{
		public StringPairList() : base()
		{
		}

		public void Add(StringPair ivp)
		{
			base.Add(ivp);
		}

		public void Add(string Id, string Value)
		{
			StringPair _Pair = new StringPair(Id, Value);
			base.Add(_Pair);
		}

		public void Insert(int Index, StringPair ivp)
		{
			base.Insert(Index, ivp);
		}

	} // end of class

} // end of namespace

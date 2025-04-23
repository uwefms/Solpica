using System;
using System.Collections;


namespace AppMainLib
{
	public class IdValuePairList : ArrayList, IEnumerable
	{
		public void Add(IdValuePair ivp)
		{
			base.Add(ivp);
		}


		public void Add(int? id, string value)
		{
			IdValuePair pair = new IdValuePair(id, value);
			base.Add(pair);
		}

		public void Insert(int index, IdValuePair ivp)
		{
			base.Insert(index, ivp);
		}

	} // end of class

} // end of namespace









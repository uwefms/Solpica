using System;

namespace AppMainLib
{
	/// <summary>
	/// Paar aus Id und Wert
	/// </summary>
	/// <remarks></remarks>
	public class IdValuePair
	{

		// public IdValuePair(int strId, string strValue)
		public IdValuePair(int? strId, string strValue)
		{
			Id = strId;
			Value = strValue;
		}

		/// <summary>
		/// ID
		/// </summary>
		/// <value>ID-Wert als Integer</value>
		/// <returns></returns>
		/// <remarks></remarks>		
		public int? Id { get; }

		/// <summary>
		/// Wert zur ID
		/// </summary>
		/// <value>Stringwert</value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string Value { get; }

		public override string ToString()
		{
			return this.Value;
		}

	} // end of class

} // end of namespace

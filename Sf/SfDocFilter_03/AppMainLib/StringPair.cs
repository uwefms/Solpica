using System;

namespace AppMainLib
{
	/// <summary>
	/// Paar aus zwei Strings
	/// </summary>
	/// <remarks></remarks>
	public class StringPair
	{
		public StringPair(string strId, string strValue)
		{
			Id = strId;
			Value = strValue;
		}

		/// <summary>
		/// ID
		/// </summary>
		/// <value>ID-Wert als String</value>
		/// <returns></returns>
		/// <remarks></remarks>
		public string Id { get; }

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
	}
}

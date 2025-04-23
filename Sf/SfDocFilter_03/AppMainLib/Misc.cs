using Microsoft.VisualBasic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;


namespace AppMainLib
{
    /// <summary>
    /// Sammlung allgemeiner statischer Methoden 
    /// </summary>
    public class Misc
	{
		#region Konstruktor 

		public Misc()
		{
		}

		#endregion

		/// <summary>
		/// Checkt auf gültige email Adresse
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		public static bool IsValidEmail(string email)
		{
			string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
			var regex = new Regex(pattern, RegexOptions.IgnoreCase);
			return regex.IsMatch(email);
		}



		#region StrTran mit Überladungen

		/// <summary>
		/// Searches one string into another string and replaces all occurences with
		/// a blank character.
		/// <pre>
		/// Example:
		/// StrTran("Joe Doe", "o");		//returns "J e D e" :)
		/// </pre>
		/// </summary>
		/// <param name="cSearchIn"> </param>
		/// <param name="cSearchFor"> </param>
		public static string StrTran(string cSearchIn, string cSearchFor)
		{
			//Create the StringBuilder
			StringBuilder sb = new StringBuilder(cSearchIn);

			//Call the Replace() method of the StringBuilder
			return sb.Replace(cSearchFor, " ").ToString();
		}

		/// <summary>
		/// Searches one string into another string and replaces all occurences with
		/// a third string.
		/// <pre>
		/// Example:
		/// StrTran("Joe Doe", "o", "ak");		//returns "Jake Dake" 
		/// </pre>
		/// </summary>
		/// <param name="cSearchIn"> </param>
		/// <param name="cSearchFor"> </param>
		/// <param name="cReplaceWith"> </param>
		public static string StrTran(string cSearchIn, string cSearchFor, string cReplaceWith)
		{
			//Create the StringBuilder
			StringBuilder sb = new StringBuilder(cSearchIn);

			//Call the Replace() method of the StringBuilder and specify the string to replace with
			return sb.Replace(cSearchFor, cReplaceWith).ToString();
		}

		/// Searches one string into another string and replaces each occurences with
		/// a third string. The fourth parameter specifies the starting occurence and the 
		/// number of times it should be replaced
		/// <pre>
		/// Example:
		/// StrTran("Joe Doe", "o", "ak", 2, 1);		//returns "Joe Dake" 
		/// </pre>
		public static string StrTran(string cSearchIn, string cSearchFor, string cReplaceWith, int nStartoccurence, int nCount)
		{
			//Create the StringBuilder
			StringBuilder sb = new StringBuilder(cSearchIn);

			//Call the Replace() method of the StringBuilder specifying the replace with string, occurence and count
			return sb.Replace(cSearchFor, cReplaceWith, nStartoccurence, nCount).ToString();
		}

		#endregion

		/// <summary>
		/// Entfernt alle alphanumerischen Zeichen aus String (zb. bei HNr Zusatz)
		/// </summary>
		/// <param name="cExpression"></param>
		/// <returns></returns>
		public static string OnlyDigits(string cExpression)
		{
			string cRetVal;

			try
			{
				cRetVal = Regex.Replace(cExpression, "[^0-9.]", "");
			}
			catch
			{
				cRetVal = "";
			}

			return cRetVal;

		} // end


		/// <summary>
		/// Receives a string and the number of characters as parameters and returns the
		/// specified number of rightmost characters of that string
		/// <pre>
		/// Example:
		/// Right("Joe Doe", 3);	//returns "Doe"
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nDigits"> </param>
		public static string Right(string cExpression, int nDigits)
		{
			string cRetVal;

			try
			{
				cRetVal = cExpression.Substring(cExpression.Length - nDigits);
			}
			catch
			{
				cRetVal = "";
			}

			return cRetVal;

		} // end

		public static string Left(string cExpression, int nDigits)
		{
			string cRetVal;
			try
			{
				cRetVal = cExpression.Substring(0, nDigits);
			}
			catch
			{
				cRetVal = "";
			}

			return cRetVal;
		}

		#region String2List - Umwandlung eines Delimiter separierten Strings in eine String Liste

		public static List<string> String2List(string cVal)
		{
			return String2List(cVal, ',');
		}

		public static List<string> String2List(string cVal, char cSep)
		{
			return cVal.Split(cSep).ToList();
		}


		#endregion

		/// <summary>
		/// Erzeugt Dateinamen aus Datum und Uhrzeit
		/// </summary>
		/// <param name="cExtention">optionale Extension - der Punkt wird automatisch hinzugefügt</param>
		/// <param name="cSuffix">optionaler Suffix</param>
		/// <returns></returns>
		public static string GetFileNameFromDate(string cExtension, string cSuffix)
		{
			if (cExtension.Length > 0)
			{
				cExtension = "." + cExtension.Trim();
			}

			return StringLib.StrTran(System.DateTime.Today.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) + "_" + System.DateTime.Now.ToLongTimeString() + cSuffix.Trim() + cExtension, ":", ".");

		} // end


		public static string InsertLayoutPraefix(string cPraeFix, string cFullFileName)
		{
			string cRet = "";

			string cTmp = PathHelper.CmParseWoExt(cFullFileName);

			string cPlainName = "";

			// wenn Präfix schon vorhanden - nicht verdoppeln
			if (cTmp.Length > cPraeFix.Length && cTmp.Substring(0, cPraeFix.Length) == cPraeFix)
			{
				// cPlainName = cTmp + ".xml";
                cPlainName = cTmp + ".bin";
			}
			else
			{
				// cPlainName = cPraeFix + "_" + StringLib.CmParseWoExt(cFullFileName) + ".xml";
                cPlainName = cPraeFix + "_" + PathHelper.CmParseWoExt(cFullFileName) + ".bin";
			}

			// cRet = StringLib.CmParseFilePath(cFullFileName) + cPlainName;
			cRet = PathHelper.CmParseFilePath(cFullFileName) + cPlainName;

			//  bubu
			return cRet;

		} // end


		/// <summary>
		/// Wandelt Hochkomma in speicherbaren string um bim Speichern in SQL Tabelle
		/// </summary>
		/// <param name="cInput"></param>
		/// <returns></returns>
		public static string FixHochkomma(string cInput)
		{
			string cOut = cInput;

			try
			{
				if (cInput.IndexOf("'") > -1)
				{
					cOut = StringLib.StrTran(cInput, "'", "''");
				}
			}
			catch
			{
				cOut = cInput;
			}

			return cOut;

		} // end

		#region Bescheid und Sollstellungs Pdf Namen

		/// <summary>
		/// Erzeugt Pdf Namen aus datum
		/// </summary>
		/// <returns></returns>
		public static string GetPdfNameFromDate(string cZusatz)
		{
			return StringLib.StrTran(System.DateTime.Today.ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) + "_" + System.DateTime.Now.ToLongTimeString() + cZusatz.Trim() + ".pdf", ":", ".");

		} // end

		/// <summary>
		/// Erzeugt Bescheide Pdf Namen
		/// </summary>
		/// <returns></returns>
		public static string GetBescheidPdfName()
		{
			return GetPdfNameFromDate("_Bescheid");
		}

		public static string GetBescheidPdfName(string cZusatz)
		{
			return GetPdfNameFromDate("_Bescheid" + cZusatz);
		}

		/// <summary>
		/// Sollstellungs Pdf Name
		/// </summary>
		/// <returns></returns>
		public static string GetSollstPdfName()
		{
			return GetPdfNameFromDate("_Sollst");
		}

		/// <summary>
		/// Sollstellungs Referenzliste Pdf Name
		/// </summary>
		/// <returns></returns>
		public static string GetSollstRefPdfName()
		{
			return GetPdfNameFromDate("_SollstRef");
		}

		#endregion

		#region GetAge - ermittelt Alter aus Geburtsdatum

		/// <summary>
		/// Erzeugt das Alter aus dem Geburtsdatum
		/// </summary>
		/// <param name="aBirthDate"></param>
		/// <returns></returns>
		public static int GetAge(DateTime aBirthDate)
		{
			try
			{
				if (aBirthDate.Date == System.DateTime.MaxValue || aBirthDate.Date == System.DateTime.MinValue ||
					aBirthDate.Date.ToShortDateString() == "01.01.1753" || aBirthDate.Date.ToShortDateString() == "01.01.1900")
				{
					return 0;
				}

				TimeSpan span = DateTime.Now.Subtract(aBirthDate);
				return span.Days / 365;
			}
			// catch (System.Exception e)
			catch
			{
				return 0;
			}

		} // -- end

		/// <summary>
		/// Ermittelt Alter aus Geburtsdatum (Alternativmethode)
		/// </summary>
		/// <param name="dBirthday">Geburtstag</param>
		/// <returns>Alter in Jahren</returns>
		public static int GetAgeFromDate(DateTime? dBirthday)
		{
			int nAge = 0;

			if (dBirthday != null)
			{
				DateTime myBirthday = (DateTime)dBirthday;

				nAge = DateTime.Now.Year - (myBirthday.Year);

				myBirthday = myBirthday.AddYears(nAge);
				if (DateTime.Now.CompareTo(myBirthday) < 0)
				{
					nAge--;
				}
			}

			return nAge;

		} // end

		#endregion

		#region FindInDv - ermittelt Wert aus DataView

		/// <summary>
		/// Gibt gesamte DataRow zu einem ID Wert zurück
		/// </summary>
		/// <param name="dv"></param>
		/// <param name="cFieldSeekName"></param>
		/// <param name="cFilterValue"></param>
		/// <returns></returns>
		/// 
		#nullable enable
		public static DataRowView? FindInDv(DataView dv, string cFieldSeekName, string cFilterValue)
		#nullable disable
		{
			#nullable enable
			DataRowView? dr = null;
			#nullable disable

			if (dv == null)
			{
				return null;
			}

			dv.RowFilter = "";
			string cOldSort = dv.Sort;

			dv.Sort = cFieldSeekName;

			int nFind = dv.Find(cFilterValue);

			if (nFind != -1)
			{
				try
				{
					dr = dv[nFind];
				}
				catch // (System.Exception ex)
				{
				}

			}

			dv.Sort = cOldSort;

			return dr;

		} // -- end


		/// <summary>
		/// FindInDv - ermittelt Wert aus DataView
		/// </summary>
		/// <param name="dv">zu durchsuchender DataView</param>
		/// <param name="cFieldSeekName">zu suchendes Feld</param>
		/// <param name="cSeekValue">Wert, nach dem gesucht wird</param>
		/// <param name="cRetCol">Rückgabefeld dessen Wert zu ermitteln ist</param>
		/// <returns>Wert des Rückgabefledes als string</returns>
		public static string FindInDv(DataView dv, string cFieldSeekName, string cSeekValue, string cRetCol)
		{
			string cRetVal = "";

			if (dv == null)
			{
				return "";
			}

			dv.RowFilter = "";
			string cOldSort = dv.Sort;

			dv.Sort = cFieldSeekName;

			int nFind = dv.Find(cSeekValue);

			if (nFind != -1)
			{
				try
				{
					cRetVal = dv[nFind][cRetCol].ToString();
				}
				catch // (System.Exception ex)
				{
				}

			}

			dv.Sort = cOldSort;

			return cRetVal;

		} // -- end

		/// <summary>
		/// FindInDv - ermittelt Wert aus DataView
		/// </summary>
		/// <param name="dv">zu durchsuchender DataView</param>
		/// <param name="cFieldSeekName">Komma separierte zu indizierende Felderliste</param>
		/// <param name="val_List">Objektliste mit zu suchenden Werten</param>
		/// <param name="cRetCol">Rückgabefeld dessen Wert zu ermitteln ist</param>
		/// <returns>Wert des Rückgabefeldes als string</returns>
		public static string FindInDv(DataView dv, string cFieldSeekList, object[] val_List, string cRetCol)
		{
			string cRetVal = "";

			if (dv == null)
			{
				return "";
			}

			dv.RowFilter = "";

			string cOldSort = dv.Sort;

			dv.Sort = cFieldSeekList;

			int nFind = dv.Find(val_List);

			if (nFind != -1)
			{
				try
				{
					cRetVal = dv[nFind][cRetCol].ToString();
				}
				catch // (System.Exception ex)
				{
				}
			}

			dv.Sort = cOldSort;

			return cRetVal;

		} // -- end

		#endregion

		#region LeftStr - Receives a string and the number of characters as parameters and returns the substring

		/// <summary>
		/// Receives a string and the number of characters as parameters and returns the
		/// specified number of leftmost characters of that string
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Left("Joe Doe", 3);	//returns "Joe"
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nDigits"> </param>
		public static string LeftStr(string cExpression, int nDigits)
		{
			string cRetVal;
			try
			{
				cRetVal = cExpression.Substring(0, nDigits);
			}
			catch
			{
				cRetVal = "";
			}

			// return cExpression.Substring(0, nDigits);

			return cRetVal;
		}

		#endregion

		#region Null - Convert Methoden

		/// <summary>
		/// Konvertiert einen Wert zu einer Interger Zahl. Ist der Wert Null oder DbNull so wird einer 0 wieder zurückgeliefert.
		/// </summary>
		/// <param name="value">Zu prüfender Objektwert</param>
		/// <returns>Ist der Wert Null oder DbNull so wird einer 0 wieder zurückgeliefert.</returns>
		public static int NullToInt(object value)
		{
			if (value is int)
			{
				return (int)value;
			}
			else
			{
				try
				{
					if ((value == null) || (value == DBNull.Value) || (value.ToString() == ""))
					{
						return 0;
					}
					else
					{
						return Convert.ToInt32(value);
					}

					//                     if ((value == null) || (value == System.DBNull.Value) || (value.ToString() == ""))
					//                     {
					//                         return 0;
					//                     }
					//                     else
					//                     {
					//                         return Convert.ToInt32(value);
					//                     }
				}
				catch
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// Konvertiert einen Wert zu einer Long Zahl. Ist der Wert Null oder DbNull so wird einer 0 wieder zurückgeliefert.
		/// </summary>
		/// <param name="value">Zu prüfender Objektwert</param>
		/// <returns>Ist der Wert Null oder DbNull so wird einer 0 wieder zurückgeliefert.</returns>
		public static long NullToLong(object value)
		{
			if (value is int)
			{
				return (long)value;
			}
			else
			{
				try
				{
					if ((value == null) || (value == System.DBNull.Value) || (value.ToString() == ""))
					{
						return 0;
					}
					else
					{
						return Convert.ToInt64(value);
					}
				}
				catch
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// Konvertiert einen Wert zu eine Zahl vom Typ DOUBLE. Ist der Wert Null oder DbNull so wird die Zahl 0 wieder zurückgeliefert.
		/// </summary>
		/// <param name="value">Zu prüfender Objektwert</param>
		/// <returns>Ist der Wert Null oder DbNull so wird die Zahl 0 wieder zurückgeliefert.</returns>
		public static double NullToDouble(object value)
		{
			if (value is double)
			{
				return (double)value;
			}
			else
			{
				try
				{
					if ((value == null) || (value == System.DBNull.Value) || (value.ToString() == ""))
					{
						return 0;
					}
					else
					{
						return Convert.ToDouble(value);
					}
				}
				catch
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// Konvertiert einen Wert zu einer Zeichenkette. Ist der Wert Null oder DbNull so wird eine leere Zeichenkette zurückgeliefert.
		/// Zudem wird die Zeichenkette geglättet (Leerzeichen an Anfang und Ende werden entfernt)
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Ist der Wert Null oder DbNull so wird eine leere Zeichenkette zurückgeliefert.</returns>
		public static string NullToString(object value)
		{
			if (value is string)
			{
				return ((string)value).Trim();
			}
			else
			{
				try
				{
					if ((value == null) || (value == System.DBNull.Value) || (value.ToString() == ""))
					{
						return "";
					}
					else
					{
						return value.ToString();
					}
				}
				catch
				{
					return "";
				}
			}
		}

		/// <summary>
		/// Konvertiert einen Wert zu einer Zeichenkette. Ist der Wert Null oder DbNull so wird eine Zeichenkette "unbekannt" zurückgeliefert.
		/// Zudem wird die Zeichenkette geglättet (Leerzeichen an Anfang und Ende werden entfernt)
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Ist der Wert Null oder DbNull so wird eine Zeichenkette "unbekannt" zurückgeliefert.</returns>
		public static string NullToStringUnbekannt(object value)
		{
			string hlp = "";
			if (value is string)
			{
				hlp = ((string)value).Trim();
			}
			else
			{
				try
				{
					if ((value == null) || (value == System.DBNull.Value) || (value.ToString() == ""))
					{
						return "unbekannt";
					}
					else
					{
						hlp = value.ToString();
					}
				}
				catch
				{
					return "unbekannt";
				}
			}
			// if (hlp.Length == 0)
			if (string.IsNullOrEmpty(hlp))
				hlp = "unbekannt";

			return hlp;

		} // end


		/// <summary>
		/// Konvertiert einen Wert zu einem DateTime-Typ. Ist der Wert Null oder DbNull so wird DateTime.MinValue wieder zurückgeliefert.
		/// </summary>
		/// <param name="value">Zu prüfender Objektwert</param>
		/// <returns>Ist der Wert Null oder DbNull so wird DateTime.MinValue wieder zurückgeliefert.</returns>
		public static DateTime NullToDateTime(object value)
		{
			if (value is DateTime)
			{
				return (DateTime)value;
			}
			else
			{
				try
				{
					if ((value == null) || (value == System.DBNull.Value) || (value.ToString() == ""))
					{
						// return MinDateTime;
						return System.DateTime.MinValue;
					}
					else
					{
						return Convert.ToDateTime(value);
					}
				}
				catch
				{
					// return MinDateTime;
					return System.DateTime.MinValue;
				}
			}
		}

		#endregion

		#region auskommentiert - bitte Hash Klasse benutzen

		//         /// <summary>
		//         /// Hashen eines strings
		//         /// </summary>
		//         /// <param name="cPw"></param>
		//         /// <returns></returns>
		//         public static string HashPw(string cPw)
		//         {
		//             string cRetVal = "";
		// 
		//             byte[] textBytes1 = System.Text.Encoding.Default.GetBytes(cPw);
		// 
		//             try
		//             {
		//                 System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
		// 
		//                 cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
		// 
		//                 byte[] hash1 = cryptHandler.ComputeHash(textBytes1);
		// 
		//                 foreach (byte a in hash1)
		//                 {
		//                     if (a < 16)
		//                     {
		//                         cRetVal += "0" + a.ToString("x");
		//                     }
		//                     else
		//                     {
		//                         cRetVal += a.ToString("x");
		//                     }
		//                 }
		//             }
		//             catch
		//             {
		//                 cRetVal = "";
		//             }
		// 
		//             return cRetVal;
		// 
		//         }
		#endregion

		#region *** Logic ***

		/// <summary>
		/// Wandelt String in entsprechenden logischen String um
		/// </summary>
		/// <param name="cValue"></param>
		/// <returns></returns>
		public static string AdjustStringLogic(string cValue)
		{
			string cRetval = "N";

			cValue = cValue.ToUpper().Trim();

			if (cValue == "Y" || cValue == "TRUE" || cValue == "1" || cValue == "WAHR" || cValue == "T" || cValue == ".T." || cValue == "J")
			{
				cRetval = "Y";
			}
			else
			{
				cRetval = "N";
			}

			return cRetval;

		} // end

		/// <summary>
		/// Wandelt boolschen Wert in String um
		/// </summary>
		/// <param name="lValue"></param>
		/// <returns></returns>
		public static string AdjustStringLogic(bool lValue)
		{
			string cRetval = "N";

			if (lValue)
			{
				cRetval = "Y";
			}
			else
			{
				cRetval = "N";
			}

			return cRetval;

		} // end



		/// <summary>
		/// erzeugt aus String boolschen Wert
		/// </summary>
		/// <param name="cValue"></param>
		/// <returns></returns>
		public static bool GenBool(string cValue)
		{
			bool lRetval = false;

			cValue = cValue.ToUpper().Trim();

			if (cValue == "Y" || cValue == "TRUE" || cValue == "1" || cValue == "WAHR" || cValue == "T" || cValue == ".T." || cValue == "J")
			{
				lRetval = true;
			}

			return lRetval;

		} // end	

		/// <summary>
		/// Konvertiert einen Wert zu einem logischen Wert. Ist der Wert Null oder DbNull so wird false zurückgeliefert.
		/// </summary>
		/// <param name="value">Zu prüfender Objektwert</param>
		/// <returns>Ist der Wert Null oder DbNull so wird false zurückgeliefert.</returns>
		public static bool ObjToBool(object value)
		{
			if (value is bool)
			{
				return (bool)value;
			}
			else
			{
				if ((value == null) || (value == System.DBNull.Value) || (value.ToString() == ""))
				{
					return false;
				}
				else
				{
					if (value.ToString().ToUpper() == "TRUE" ||
						  value.ToString().ToUpper() == "J" ||
						   value.ToString().ToUpper() == "Y" ||
							value.ToString().ToUpper() == "T" ||
							 value.ToString() == "1" ||
							  value.ToString() == "-1")
					{
						return true;
					}
					else if (value.ToString().ToUpper() == "FALSE" ||
							  value.ToString().ToUpper() == "N" ||
							   value.ToString().ToUpper() == "F" ||
								value.ToString() == "0")
					{
						return false;
					}
					else
					{
						try
						{
							return Convert.ToBoolean(value);
						}
						catch
						{
							return false;
						}
					}
				}
			}

		} // -- end

		#endregion Logic 

		#region GetNullableValue / GetStringValue / GetValue - neue Version

		public delegate bool TryParseDelegate<T>(string s, out T result);

		/// <summary>
		/// Wandelt Objekt in String um
		/// </summary>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <returns>Stringumwandlung OHNE Trim</returns>
		public static string GetStringValue(object oVal)
		{
			if (oVal is null)
			{
				return "";
			}
			else
			{
				return oVal.ToString();
			}
		}


		/// <summary>
		/// Wandelt Objekt in String um mit default Rückgabe bei Leerstring
		/// </summary>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <param name="cDefault">defaultwert, wenn Objekt NULL oder String.Empty ist</param>
		/// <returns>Stringumwandlung OHNE Trim</returns>
		public static string GetStringValue(object oVal, string cDefault)
		{
			if (string.IsNullOrEmpty(oVal.ToString()))
			{
				return cDefault;
			}
			else
			{
				return oVal.ToString();
			}
		}

		#region GetValue

		/// <summary>
		/// Wandelt Objekt in typisierten Wert
		/// </summary>
		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <returns>typisiertes Objekt oder Error bei Parse Error oder leerem Objekt</returns>
		public static T GetValue<T>(object oVal) where T : struct
		{
			return GetValue<T>(oVal, true, default(T), 2);
		}

		/// <summary>
		/// Wandelt Objekt in typisierten Wert
		/// </summary>
		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		/// <returns>typisiertes Objekt oder Error bei Parse Error oder leerem Objekt</returns>
		public static T GetValue<T>(object oVal, bool ExceptionOnParseError) where T : struct
		{
			return GetValue<T>(oVal, ExceptionOnParseError, default(T), 2);
		}

		public static T GetValue<T>(object oVal, bool ExceptionOnParseError, T DefVal) where T : struct
		{
			return GetValue<T>(oVal, ExceptionOnParseError, DefVal, 2);
		}

		/// <summary>
		/// Wandelt Objekt in typisierten Wert
		/// </summary>
		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		/// <param name="DefVal">default Wert, wenn Parsen scheitert</param>
		/// <returns>typisiertes Objekt oder Error bei Parse Error oder leerem Objekt</returns>
		public static T GetValue<T>(object oVal, bool ExceptionOnParseError, T DefVal, int nDecimal) where T : struct
		{
			bool lParseResult;
			string cType = default(T).GetType().ToString();

			string cSep = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

			if (cType == "System.Byte")
			{
				lParseResult = Try_Parse<Byte>(oVal.ToString(), out byte result, byte.TryParse, (byte)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.Int16")
			{
				lParseResult = Try_Parse<Int16>(oVal.ToString(), out short result, short.TryParse, (short)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.Int32")
			{
				lParseResult = Try_Parse<Int32>(oVal.ToString(), out int result, int.TryParse, (int)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.Int64")
			{
				lParseResult = Try_Parse<Int64>(oVal.ToString(), out long result, long.TryParse, (long)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.Boolean")
			{
				lParseResult = Try_Parse<bool>(oVal.ToString(), out bool result, bool.TryParse, (bool)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.DateTime")
			{
				lParseResult = Try_Parse<DateTime>(oVal.ToString(), out DateTime result, DateTime.TryParse, (DateTime)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.TimeSpan")
			{
				lParseResult = Try_Parse<TimeSpan>(oVal.ToString(), out TimeSpan result, TimeSpan.TryParse, (TimeSpan)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.Double")
			{
				// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
				// 		        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

				string cNumber = oVal.ToString();

				if (cSep == ",")
				{
					if (cNumber.Contains(","))
					{
						// ("Komma").Dump();

						cNumber = cNumber.Replace(",", ".");
					}
				}
				else
				{
					// nix machen
					if (cNumber.Contains("."))
					{
						// ("Punkt").Dump();					
						// cNumber = cNumber.Replace("." , ",");	
					}
					else if (cNumber.Contains(","))
					{
						// ("Komma").Dump();

						cNumber = cNumber.Replace(",", ".");
					}

				}


				// lParseResult = Double.TryParse(oVal.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out result);
				lParseResult = double.TryParse(cNumber, NumberStyles.Float, CultureInfo.InvariantCulture, out double result);

				oVal = result;

				// lParseResult = Try_Parse<Double>(oVal.ToString().Replace(",","."), out result, Double.TryParse, (Double)(object)DefVal, ExceptionOnParseError);								
				lParseResult = Try_Parse<Double>(oVal.ToString(), out result, double.TryParse, (double)(object)DefVal, ExceptionOnParseError);

				// 	            if (originalCulture != null)
				// 				{
				//                     Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				result = Math.Round(result, nDecimal, MidpointRounding.AwayFromZero);

				return (T)(object)result;
			}
			else if (cType == "System.Decimal")
			{

				// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
				// 		        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

				string cNumber = oVal.ToString();

				if (cSep == ",")
				{
					if (cNumber.Contains(","))
					{
						// ("Komma").Dump();

						cNumber = cNumber.Replace(",", ".");
					}
				}
				else
				{
					// nix machen
					if (cNumber.Contains("."))
					{
						// ("Punkt").Dump();					
						// cNumber = cNumber.Replace("." , ",");	
					}
					else if (cNumber.Contains(","))
					{
						// ("Komma").Dump();

						cNumber = cNumber.Replace(",", ".");
					}

				}


				// lParseResult = Decimal.TryParse(oVal.ToString(), NumberStyles.Currency, CultureInfo.InvariantCulture, out result);
				// lParseResult = Decimal.TryParse(oVal.ToString(), NumberStyles.Currency, CultureInfo.InvariantCulture, out result);
				lParseResult = decimal.TryParse(cNumber, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal result);

				oVal = result;

				// lParseResult = Try_Parse<Decimal>(oVal.ToString().Replace(",", "."), out result, Decimal.TryParse, (Decimal)(object)DefVal, ExceptionOnParseError);
				lParseResult = Try_Parse<Decimal>(oVal.ToString(), out result, decimal.TryParse, (decimal)(object)DefVal, ExceptionOnParseError);

				// 	            if (originalCulture != null)
				// 				{
				//                     Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				result = Math.Round(result, nDecimal, MidpointRounding.AwayFromZero);

				return (T)(object)result;
			}
			else if (cType == "System.Single")
			{
				lParseResult = Try_Parse<Single>(oVal.ToString(), out float result, float.TryParse, (float)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.Char")
			{
				lParseResult = Try_Parse<Char>(oVal.ToString(), out char result, char.TryParse, (char)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else if (cType == "System.Guid")
			{
				lParseResult = Try_Parse<Guid>(oVal.ToString(), out Guid result, Guid.TryParse, (Guid)(object)DefVal, ExceptionOnParseError);
				return (T)(object)result;
			}
			else
			{
				// throw new MainLib_Exception("Type " + default(T).GetType().ToString() + " not supported");
				// throw;
				throw new Exception("Type " + default(T).GetType().ToString() + " not supported");
			}

		} // end

		#endregion

		#region GetNullableValue

		/// <summary>
		/// Wandelt Nullable Objekt in typisierten Wert
		/// </summary>
		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <returns>typisiertes Objekt oder Error bei Parse Error</returns>
		public static Nullable<T> GetNullableValue<T>(object oVal) where T : struct
		{
			return GetNullableValue<T>(oVal, true, null, 2);
		}

		/// <summary>
		/// Wandelt Nullable Objekt in typisierten Wert
		/// </summary>
		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		/// <returns>typisiertes Objekt oder Error bei Parse Error und ExceptionOnParseError = false bzw. default wert des Typen, wenn Parsen scheitert und ExceptionOnParseError = false</returns>
		public static Nullable<T> GetNullableValue<T>(object oVal, bool ExceptionOnParseError) where T : struct
		{
			return GetNullableValue<T>(oVal, ExceptionOnParseError, null, 2);
		}

		public static Nullable<T> GetNullableValue<T>(object oVal, bool ExceptionOnParseError, Nullable<T> DefVal) where T : struct
		{
			return GetNullableValue<T>(oVal, ExceptionOnParseError, DefVal, 2);
		}

		/// <summary>
		/// Wandelt Nullable Objekt in typisierten Wert
		/// </summary>
		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		/// <param name="oVal">umzuwandelndes Objekt</param>
		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		/// <param name="DefVal">default Wert, wenn Parsen scheitert und ExceptionOnParseError = false</param>
		/// <returns>typisiertes Objekt oder Error bei Parse Error und ExceptionOnParseError = false bzw. DefVal, wenn Parsen scheitert und ExceptionOnParseError = false</returns>		
		public static Nullable<T> GetNullableValue<T>(object oVal, bool ExceptionOnParseError, Nullable<T> DefVal, int nDecimal) where T : struct
		{
			bool lParseResult;
			string cType = default(T).GetType().ToString();
			string cSep = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

			if (oVal == null)
			{
				oVal = "";
			}

			if (cType == "System.Byte")
			{
				lParseResult = Try_ParseNullable<Byte>(oVal.ToString(), out byte? result, byte.TryParse, (byte?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Int16")
			{
				lParseResult = Try_ParseNullable<Int16>(oVal.ToString(), out short? result, short.TryParse, (short?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Int32")
			{
				lParseResult = Try_ParseNullable<int>(oVal.ToString(), out int? result, int.TryParse, (int?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Int64")
			{
				lParseResult = Try_ParseNullable<Int64>(oVal.ToString(), out long? result, long.TryParse, (long?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Boolean")
			{
				lParseResult = Try_ParseNullable<bool>(oVal.ToString(), out bool? result, bool.TryParse, (bool?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.DateTime")
			{
				lParseResult = Try_ParseNullable<DateTime>(oVal.ToString(), out DateTime? result, DateTime.TryParse, (DateTime?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.TimeSpan")
			{
				lParseResult = Try_ParseNullable<TimeSpan>(oVal.ToString(), out TimeSpan? result, TimeSpan.TryParse, (TimeSpan?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Double")
			{
				// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
				// 				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

				string cNumber = "";

				if (oVal != null)
				{
					cNumber = oVal.ToString();

					if (cSep == ",")
					{
						if (cNumber.Contains(","))
						{
							// ("Komma").Dump();

							cNumber = cNumber.Replace(",", ".");
						}
					}
					else
					{
						// nix machen
						if (cNumber.Contains("."))
						{
							// ("Punkt").Dump();					
							// cNumber = cNumber.Replace("." , ",");	
						}
						else if (cNumber.Contains(","))
						{
							// ("Komma").Dump();

							cNumber = cNumber.Replace(",", ".");
						}

					}


					lParseResult = double.TryParse(cNumber, NumberStyles.Float, CultureInfo.InvariantCulture, out double result1);

					oVal = result1;

				}


				// lParseResult = Try_Parse<Double>(oVal.ToString().Replace(",","."), out result, Double.TryParse, (Double)(object)DefVal, ExceptionOnParseError);								
				lParseResult = Try_ParseNullable<Double>(oVal.ToString(), out double? result, double.TryParse, (double)(object)DefVal, ExceptionOnParseError);

				// lParseResult = Try_ParseNullable<Double>(oVal.ToString().Replace(",","."), out result, Double.TryParse, (Double?)(object)DefVal, ExceptionOnParseError);

				//                 if (originalCulture != null)
				// 				{
				//                     Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				if (result != null)
				{
					result = Math.Round((double)result, nDecimal, MidpointRounding.AwayFromZero);
				}

				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Decimal")
			{
				// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
				// 		        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

				string cNumber = "";

				if (oVal != null)
				{
					cNumber = oVal.ToString();

					if (cSep == ",")
					{
						if (cNumber.Contains(","))
						{
							// ("Komma").Dump();

							cNumber = cNumber.Replace(",", ".");
						}
					}
					else
					{
						// nix machen
						if (cNumber.Contains("."))
						{
							// ("Punkt").Dump();					
							// cNumber = cNumber.Replace("." , ",");	
						}
						else if (cNumber.Contains(","))
						{
							// ("Komma").Dump();

							cNumber = cNumber.Replace(",", ".");
						}

					}


					lParseResult = decimal.TryParse(cNumber, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal result1);

					oVal = result1;

				}


				// lParseResult = Try_ParseNullable<Decimal>(oVal.ToString(), out result, Decimal.TryParse, (Decimal?)(object)DefVal, ExceptionOnParseError);
				// lParseResult = Try_ParseNullable<Decimal>(oVal.ToString().Replace(",","."), out result, Decimal.TryParse, (Decimal?)(object)DefVal, ExceptionOnParseError);

				lParseResult = Try_ParseNullable<Decimal>(oVal.ToString(), out decimal? result, decimal.TryParse, (decimal)(object)DefVal, ExceptionOnParseError);

				//              if (originalCulture != null)
				// 				{
				//                     Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				if (result != null)
				{
					result = Math.Round((decimal)result, nDecimal, MidpointRounding.AwayFromZero);
				}

				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Single")
			{
				lParseResult = Try_ParseNullable<Single>(oVal.ToString(), out float? result, float.TryParse, (float?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Char")
			{
				lParseResult = Try_ParseNullable<Char>(oVal.ToString(), out char? result, char.TryParse, (char?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else if (cType == "System.Guid")
			{
				lParseResult = Try_ParseNullable<Guid>(oVal.ToString(), out Guid? result, Guid.TryParse, (Guid?)(object)DefVal, ExceptionOnParseError);
				return (Nullable<T>)(object)result;
			}
			else
			{
				// throw new MainLib_Exception("Type " + default(T).GetType().ToString() + " not supported");
				throw new Exception("Type " + default(T).GetType().ToString() + " not supported");
			}

		} // end

		#endregion

		#region auskommentiert
		// 
		// 		/// <summary>
		// 		/// Wandelt Objekt in typisierten Wert
		// 		/// </summary>
		// 		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		// 		/// <param name="oVal">umzuwandelndes Objekt</param>
		// 		/// <returns>typisiertes Objekt oder Error bei Parse Error oder leerem Objekt</returns>
		// 		public static T GetValue<T>(object oVal) where T : struct
		// 		{
		// 			return GetValue<T>(oVal, true, default(T));
		// 		}
		// 
		// 		/// <summary>
		// 		/// Wandelt Objekt in typisierten Wert
		// 		/// </summary>
		// 		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		// 		/// <param name="oVal">umzuwandelndes Objekt</param>
		// 		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		// 		/// <returns>typisiertes Objekt oder Error bei Parse Error oder leerem Objekt</returns>
		// 		public static T GetValue<T>(object oVal, bool ExceptionOnParseError) where T : struct
		// 		{
		// 			return GetValue<T>(oVal, ExceptionOnParseError, default(T));
		// 		}
		// 			
		// 		/// <summary>
		// 		/// Wandelt Objekt in typisierten Wert
		// 		/// </summary>
		// 		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		// 		/// <param name="oVal">umzuwandelndes Objekt</param>
		// 		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		// 		/// <param name="DefVal">default Wert, wenn Parsen scheitert</param>
		// 		/// <returns>typisiertes Objekt oder Error bei Parse Error oder leerem Objekt</returns>
		// 		public static T GetValue<T>(object oVal, bool ExceptionOnParseError, T DefVal) where T : struct
		// 		{
		// 			bool lParseResult;			
		// 			string cType =  default(T).GetType().ToString() ;
		// 			
		// 			if ( cType == "System.Byte" )
		// 			{				
		// 				Byte result;
		// 				lParseResult = Try_Parse<Byte>(oVal.ToString(), out result, Byte.TryParse, (Byte)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;				
		// 			}
		// 			else if ( cType == "System.Int16" )
		// 			{				
		// 				Int16 result;
		// 				lParseResult = Try_Parse<Int16>(oVal.ToString(), out result, Int16.TryParse, (Int16)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Int32" )
		// 			{		
		// 				int result;
		// 				lParseResult = Try_Parse<Int32>(oVal.ToString(), out result, Int32.TryParse, (Int32)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Int64" )
		// 			{			
		// 				Int64 result;
		// 				lParseResult = Try_Parse<Int64>(oVal.ToString(), out result, Int64.TryParse, (Int64)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Boolean" )
		// 			{				
		// 				bool result;
		// 				lParseResult = Try_Parse<bool>(oVal.ToString(), out result, bool.TryParse, (bool)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.DateTime" )
		// 			{				
		// 				DateTime result;
		// 				lParseResult = Try_Parse<DateTime>(oVal.ToString(), out result, DateTime.TryParse, (DateTime)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.TimeSpan" )
		// 			{				
		// 				TimeSpan result;
		// 				lParseResult = Try_Parse<TimeSpan>(oVal.ToString(), out result, TimeSpan.TryParse, (TimeSpan)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Double" )
		// 			{				
		// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
		// 		        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		// 
		// 				Double result;
		// 				// lParseResult = Try_Parse<Double>(oVal.ToString(), out result, Double.TryParse, (Double)(object)DefVal, ExceptionOnParseError);
		// 				lParseResult = Try_Parse<Double>(oVal.ToString().Replace(",","."), out result, Double.TryParse, (Double)(object)DefVal, ExceptionOnParseError);								
		// 
		// 	            if (originalCulture != null)
		// 				{
		//                     Thread.CurrentThread.CurrentCulture = originalCulture;
		// 				}
		// 
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Decimal" )
		// 			{				
		// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
		// 		        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		// 
		// 				Decimal result;
		// 				// lParseResult = Try_Parse<Decimal>(oVal.ToString(), out result, Decimal.TryParse, (Decimal)(object)DefVal, ExceptionOnParseError);
		// 				lParseResult = Try_Parse<Decimal>(oVal.ToString().Replace(",","."), out result, Decimal.TryParse, (Decimal)(object)DefVal, ExceptionOnParseError);
		// 
		// 	            if (originalCulture != null)
		// 				{
		//                     Thread.CurrentThread.CurrentCulture = originalCulture;
		// 				}
		// 				
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Single" )
		// 			{			
		// 				Single result;
		// 				lParseResult = Try_Parse<Single>(oVal.ToString(), out result, Single.TryParse, (Single)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Char" )
		// 			{			
		// 				Char result;
		// 				lParseResult = Try_Parse<Char>(oVal.ToString(), out result, Char.TryParse, (Char)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else if ( cType == "System.Guid" )
		// 			{				
		// 				Guid result;
		// 				lParseResult = Try_Parse<Guid>(oVal.ToString(), out result, Guid.TryParse, (Guid)(object)DefVal, ExceptionOnParseError);
		// 				return (T)(object)result;
		// 			}
		// 			else
		// 			{				
		// 				// throw new Hathor_Exception("Der " + default(T).GetType().ToString() + " Typ wird nicht unterstützt");				
		// 				// throw new MainLib_Exception("Der " + default(T).GetType().ToString() + " Typ wird nicht unterstützt");				
		// 				throw new MainLib_Exception("Type " + default(T).GetType().ToString() + " not supported");				
		// 			}
		// 						
		// 		} // end
		// 
		// 		/// <summary>
		// 		/// Wandelt Nullable Objekt in typisierten Wert
		// 		/// </summary>
		// 		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		// 		/// <param name="oVal">umzuwandelndes Objekt</param>
		// 		/// <returns>typisiertes Objekt oder Error bei Parse Error</returns>
		// 		public static Nullable<T> GetNullableValue<T>(object oVal) where T : struct
		// 		{
		// 			return GetNullableValue<T>(oVal, true, default(T));
		// 		}
		// 
		// 		/// <summary>
		// 		/// Wandelt Nullable Objekt in typisierten Wert
		// 		/// </summary>
		// 		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		// 		/// <param name="oVal">umzuwandelndes Objekt</param>
		// 		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		// 		/// <returns>typisiertes Objekt oder Error bei Parse Error und ExceptionOnParseError = false bzw. default wert des Typen, wenn Parsen scheitert und ExceptionOnParseError = false</returns>
		// 		public static Nullable<T> GetNullableValue<T>(object oVal, bool ExceptionOnParseError) where T : struct
		// 		{
		// 			return GetNullableValue<T>(oVal, ExceptionOnParseError, default(T));
		// 		}
		// 
		// 		/// <summary>
		// 		/// Wandelt Nullable Objekt in typisierten Wert
		// 		/// </summary>
		// 		/// <typeparam name="T">typisierter RückgabeWert</typeparam>
		// 		/// <param name="oVal">umzuwandelndes Objekt</param>
		// 		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		// 		/// <param name="DefVal">default Wert, wenn Parsen scheitert und ExceptionOnParseError = false</param>
		// 		/// <returns>typisiertes Objekt oder Error bei Parse Error und ExceptionOnParseError = false bzw. DefVal, wenn Parsen scheitert und ExceptionOnParseError = false</returns>
		// 		public static Nullable<T> GetNullableValue<T>(object oVal, bool ExceptionOnParseError, Nullable<T> DefVal) where T : struct
		// 		{
		// 			bool lParseResult;
		// 			string cType =  default(T).GetType().ToString() ;
		// 
		// 			if ( cType == "System.Byte" )
		// 			{			
		// 				Byte? result;
		// 				lParseResult = Try_ParseNullable<Byte>(oVal.ToString(), out result, Byte.TryParse, (Byte?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Int16" )
		// 			{				
		// 				Int16? result;
		// 				lParseResult = Try_ParseNullable<Int16>(oVal.ToString(), out result, Int16.TryParse, (Int16?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Int32" )
		// 			{				
		// 				int? result;
		// 				lParseResult = Try_ParseNullable<int>(oVal.ToString(), out result, int.TryParse, (int?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Int64" )
		// 			{				
		// 				Int64? result;
		// 				lParseResult = Try_ParseNullable<Int64>(oVal.ToString(), out result, Int64.TryParse, (Int64?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Boolean" )
		// 			{			
		// 				bool? result;
		// 				lParseResult = Try_ParseNullable<bool>(oVal.ToString(), out result, bool.TryParse, (bool?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.DateTime" )
		// 			{			
		// 				DateTime? result;
		// 				lParseResult = Try_ParseNullable<DateTime>(oVal.ToString(), out result, DateTime.TryParse, (DateTime?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.TimeSpan" )
		// 			{			
		// 				TimeSpan? result;
		// 				lParseResult = Try_ParseNullable<TimeSpan>(oVal.ToString(), out result, TimeSpan.TryParse, (TimeSpan?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Double" )
		// 			{			
		// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
		// 				Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		// 
		// 				Double? result;
		// 				// lParseResult = Try_ParseNullable<Double>(oVal.ToString(), out result, Double.TryParse, (Double?)(object)DefVal, ExceptionOnParseError);
		// 				lParseResult = Try_ParseNullable<Double>(oVal.ToString().Replace(",","."), out result, Double.TryParse, (Double?)(object)DefVal, ExceptionOnParseError);
		// 
		//                 if (originalCulture != null)
		// 				{
		//                     Thread.CurrentThread.CurrentCulture = originalCulture;
		// 				}
		// 						
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Decimal" )
		// 			{
		// 				CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
		// 		        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		// 
		// 				Decimal? result;
		// 				// lParseResult = Try_ParseNullable<Decimal>(oVal.ToString(), out result, Decimal.TryParse, (Decimal?)(object)DefVal, ExceptionOnParseError);
		// 				lParseResult = Try_ParseNullable<Decimal>(oVal.ToString().Replace(",","."), out result, Decimal.TryParse, (Decimal?)(object)DefVal, ExceptionOnParseError);
		// 
		//                 if (originalCulture != null)
		// 				{
		//                     Thread.CurrentThread.CurrentCulture = originalCulture;
		// 				}
		// 
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Single" )
		// 			{			
		// 				Single? result;
		// 				lParseResult = Try_ParseNullable<Single>(oVal.ToString(), out result, Single.TryParse, (Single?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Char" )
		// 			{			
		// 				Char? result;
		// 				lParseResult = Try_ParseNullable<Char>(oVal.ToString(), out result, Char.TryParse, (Char?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else if ( cType == "System.Guid" )
		// 			{			
		// 				Guid? result;
		// 				lParseResult = Try_ParseNullable<Guid>(oVal.ToString(), out result, Guid.TryParse, (Guid?)(object)DefVal, ExceptionOnParseError);
		// 				return (Nullable<T>)(object)result;
		// 			}
		// 			else
		// 			{
		// 				// throw new ElaParseException("Der " + default(T).GetType().ToString() + " Typ wird nicht unterstützt");
		// 				// throw new MainLib_Exception("Der " + default(T).GetType().ToString() + " Typ wird nicht unterstützt");
		// 				throw new MainLib_Exception("Type " + default(T).GetType().ToString() + " not supported");
		// 			}
		// 						
		// 		} // end
		#endregion

		/// <summary>
		/// Interne Parse Methode für NICHT Nullable Werte
		/// </summary>
		/// <typeparam name="T">generischer Typ der Methode</typeparam>
		/// <param name="s">zu parsendes Objekt als String</param>
		/// <param name="result">Parse Ergebnis oder bei Error default Wert</param>
		/// <param name="tryParse">typisierter TryParseDelegate</param>
		/// <param name="DefVal">default Wert, wenn Parsen scheitert</param>
		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		/// <returns>logischer Wert, ob TryParse ohne Error beendet wurde</returns>
		private static bool Try_Parse<T>(string s, out T result, TryParseDelegate<T> tryParse, T DefVal, bool ExceptionOnParseError) where T : struct
		{
			//             CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
			//             Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			T tmp = default(T);

			if (!DefVal.Equals(tmp))
			{
				tmp = DefVal;
			}

			// if ( string.IsNullOrWhiteSpace(s) )
			if (string.IsNullOrEmpty(s))
			{
				if (ExceptionOnParseError)
				{
					// throw new ElaParseException("Der " + default(T).GetType().ToString() + " Typ darf nicht leer oder NULL sein");
					// throw new MainLib_Exception("Der " + default(T).GetType().ToString() + " Typ darf nicht leer oder NULL sein");
					// throw new MainLib_Exception("Type " + default(T).GetType().ToString() + " must not be NULL or empty");
					throw new Exception("Type " + default(T).GetType().ToString() + " must not be NULL or empty");
				}

				result = tmp;

				//                 if (originalCulture != null)
				// 				{
				//                     Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				return false;
			}
			else
			{
				bool success = tryParse(s, out tmp);

				if (!success)
				{
					result = DefVal;

					if (ExceptionOnParseError)
					{
						// throw new ElaParseException("Fehler beim Parsen von [" + s + "] in den " + default(T).GetType().ToString() + " Typ ");
						// throw new MainLib_Exception("Fehler beim Parsen von [" + s + "] in den " + default(T).GetType().ToString() + " Typ ");
						// throw new MainLib_Exception("Parsing Error at [" + s + "] into " + default(T).GetType().ToString() + " type ");
						throw new Exception("Parsing Error at [" + s + "] into " + default(T).GetType().ToString() + " type ");
					}
				}
				else
				{
					result = tmp;
				}

				//                 if (originalCulture != null)
				// 				{
				//                     Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				return success;
			}

		} // end

		/// <summary>
		/// Interne Parse Methode für Nullable Werte
		/// </summary>
		/// <typeparam name="T">generischer Typ der Methode</typeparam>
		/// <param name="s">zu parsendes Objekt als String</param>
		/// <param name="result">Parse Ergebnis oder bei Error default Wert</param>
		/// <param name="tryParse">typisierter TryParseDelegate</param>
		/// <param name="DefVal">default Wert, wenn Parsen scheitert</param>
		/// <param name="ExceptionOnParseError">Exception bei Parse Error - oder Rückgabe default Wert</param>
		/// <returns>logischer Wert, ob TryParse ohne Error beendet wurde</returns>
		private static bool Try_ParseNullable<T>(string s, out Nullable<T> result, TryParseDelegate<T> tryParse, Nullable<T> DefVal, bool ExceptionOnParseError) where T : struct
		{
			//             CultureInfo originalCulture			= Thread.CurrentThread.CurrentCulture;
			//             Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			Nullable<T> tmp = default(T);

			if (!DefVal.Equals(tmp))
			{
				tmp = DefVal;
			}

			// if ( string.IsNullOrWhiteSpace(s) )
			if (string.IsNullOrEmpty(s))
			{
				result = tmp;

				// 				if (originalCulture != null)
				// 				{
				// 					Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				return true;
			}
			else
			{
				bool success = tryParse(s, out T temp);

				if (!success)
				{
					if (ExceptionOnParseError)
					{
						// throw new MainLib_Exception("Fehler beim Parsen von [" + s + "] in den " + default(T).GetType().ToString() + " Typ ");
						// throw new MainLib_Exception("Parsing Error at [" + s + "] into " + default(T).GetType().ToString() + " type ");
						throw new Exception("Parsing Error at [" + s + "] into " + default(T).GetType().ToString() + " type ");
					}

					result = DefVal;
				}
				else
				{
					result = temp;
				}

				// 				if (originalCulture != null)
				// 				{
				// 					Thread.CurrentThread.CurrentCulture = originalCulture;
				// 				}

				return success;
			}


		} // end

		#endregion GetNullableValue / GetValue

		#region Datums und Zeitfunktionen

		private static long _lastTimeStamp = 1;

		public static byte[] GetTimeStamp()
		{
			var stamp = System.Threading.Interlocked.Add(ref _lastTimeStamp, 1);
			return System.Text.ASCIIEncoding.ASCII.GetBytes(stamp.ToString());
		}


		/// <summary>
		/// Wandelt Datetime in Ticks
		/// </summary>
		/// <param name="dtInput"></param>
		/// <returns></returns>
		public static long ConvertDateTimeToTicks(DateTime dtInput)
		{
			long ticks = 0;
			ticks = dtInput.Ticks;
			return ticks;
		}

		/// <summary>
		/// Wandelt Ticks in Datetime
		/// </summary>
		/// <param name="lticks"></param>
		/// <returns></returns>
		public static DateTime ConvertTicksToDateTime(long lticks)
		{
			DateTime dtresult = new DateTime(lticks);
			return dtresult;
		}

		/// <summary>
		/// Receives a DateTime as a parameter and returns the current month formatted as a string
		/// <pre>
		/// Example:
		/// DateTime tDateTime = DateTime.Now;
		/// string lcDate = VFPToolkit.dates.CMonth(tDateTime);	//returns "May"
		/// </pre>
		/// </summary>
		/// <param name="dDate"></param>
		/// <returns></returns>
		public static string CMonth(System.DateTime dDate)
		{
			return dDate.ToString("MMMM");
		}

		/// <summary>
		/// Checkt, ob ein Datumswert null entspricht
		/// </summary>
		/// <param name="dDate"></param>
		/// <returns></returns>
		public static bool DateIsNullOrEmpty(System.DateTime dDate)
		{
			try
			{
				if (dDate == System.DateTime.MaxValue || dDate == System.DateTime.MinValue)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch // (System.Exception ex)
			{
				return true;
			}

		} // end

		/// <summary>
		/// Überprüft, ob datetime in NULL zu verwandeln ist
		/// </summary>
		/// <param name="dDate"></param>
		/// <returns></returns>
		public static object RetDateNull(System.DateTime dDate)
		{
			object oRet = System.DBNull.Value;

			if (!DateIsNullOrEmpty(dDate))
			{
				oRet = dDate;
			}

			return oRet;
		}


		public static object DateToDBNull(object value)
		{
			if (value != null && value != System.DBNull.Value)
			{
				if ((value != null) && (DateTime.Compare(Convert.ToDateTime(value), new DateTime(0x7ac3789f9c1c000L)) >= 0))
				{
					return value;
				}
			}

			return DBNull.Value;
		}

		/// <summary>
		/// Gibt DBNull zurück, wenn Value = NULL
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static object NullToDBNull(object value)
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			return value;
		}

		/// <summary>
		/// Returns the number of seconds since midnight
		/// <example>
		/// Example:
		/// double nTotalSeconds = VFPToolkit.dates.Seconds();
		/// </example>
		/// </summary>
		/// <returns></returns>
		public static double Seconds()
		{
			//Create the timespan object get the time between the dates
			System.TimeSpan st = System.DateTime.Now.Subtract(System.DateTime.Today);

			//Return the number of seconds
			return st.Duration().TotalMilliseconds / 1000;
		}

		public static double Seconds(DateTime date)
		{
			//Create the timespan object get the time between the dates
			System.TimeSpan st = date.Subtract(DateTime.UtcNow.ToLocalTime());

			//Return the number of seconds
			return st.Duration().TotalMilliseconds / 1000;
		}


		#region Datums Umwandlungen

		/// <summary>
		/// Liefert Wochentag Nr für Datum (So = 0  - Sa = 6 )
		/// </summary>
		/// <param name="dDate"></param>
		/// <returns></returns>
		public static int GetDayNoByDate(System.DateTime dDate)
		{
			int nRetval = 0;

			try
			{
				nRetval = (int)dDate.DayOfWeek;
			}
			catch
			{
				nRetval = 0;
			}

			return nRetval;
		}

		/// <summary>
		/// Liefert den Wochentag als Zahl zurück 1 = Montag / 0 = Sonntag
		/// </summary>
		/// <param name="cTag">deutsche oder englische Tagesbezeichnung</param>
		/// <returns></returns>
		public static int WeekDay2Int(string cTag)
		{
			int nRet = 0;

			if (cTag == "Montag" || cTag == "Monday")
			{
				nRet = 1;
			}
			else if (cTag == "Dienstag" || cTag == "Tuesday")
			{
				nRet = 2;
			}
			else if (cTag == "Mittwoch" || cTag == "Wednesday")
			{
				nRet = 3;
			}
			else if (cTag == "Donnerstag" || cTag == "Thursday")
			{
				nRet = 4;
			}
			else if (cTag == "Freitag" || cTag == "Friday")
			{
				nRet = 5;
			}
			else if (cTag == "Samstag" || cTag == "Saturday")
			{
				nRet = 6;
			}
			else if (cTag == "Sonntag" || cTag == "Sunday")
			{
				nRet = 0;
			}

			return nRet;
		}


		/// <summary>
		/// Liefert den deutschen Wochentag als String zurück
		/// </summary>
		/// <param name="nTag">Zahlen von 0 (Sonntag) bis 6 (Samstag) - </param>
		/// <returns></returns>
		public static string Int2WeekDay(int nTag)
		{
			string cRet = "";

			if (nTag == 1)
			{
				cRet = "Montag";
			}
			else if (nTag == 2)
			{
				cRet = "Dienstag";
			}
			else if (nTag == 3)
			{
				cRet = "Mittwoch";
			}
			else if (nTag == 4)
			{
				cRet = "Donnerstag";
			}
			else if (nTag == 5)
			{
				cRet = "Freitag";
			}
			else if (nTag == 6)
			{
				cRet = "Samstag";
			}
			else if (nTag == 0 || nTag == 7)
			{
				cRet = "Sonntag";
			}

			return cRet;
		}


		/// <summary>
		/// errechnet Kalenderwoche aus Datum
		/// </summary>
		/// <param name="datum"></param>
		/// <returns></returns>
		public static int Date2KW(DateTime datum)
		{
			if (datum.DayOfWeek >= DayOfWeek.Monday)
			{
				datum = datum.AddDays(7 - (int)datum.DayOfWeek);
			}

			int kw = System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(datum, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

			return kw;

		}

		/// <summary>
		/// Erzeugt Datum aus TagNr für Scheduler Datums+Zeit Angaben
		/// Wird kein Datum mitgegeben, wird das Tagesdatum verwendet
		/// z. B. dOut1 = SetDateByTagNr(1, new DateTime(2020,5,4))  wobei 1 Montag entspricht
		/// bzw. dOut1 = SetDateByTagNr(1)  wobei 1 Montag entspricht
		/// </summary>
		/// <param name="nTagNr"></param>
		/// <param name="dt1"></param>
		/// <returns></returns>
		public static DateTime SetDateByTagNr(int nTagNr, DateTime? dt1 = null)
		{
			DateTime dt = DateTime.Today;

			if (dt1 == null)
			{
				dt = DateTime.Today;
			}
			else
			{
				dt = (DateTime)dt1;
			}

			var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
			var diff = dt.DayOfWeek - (culture.DateTimeFormat.FirstDayOfWeek + nTagNr - 1);

			var nAdd = 0;

			if (diff < 0)
			{
				diff += 7;
				nAdd = 7;
			}

			return dt.AddDays(-diff + nAdd).Date;
		}

		/// <summary>
		/// Erzeugt Datum aus TagNr, Zeitangabe als Datum und ggf. Referenzdatum für Scheduler Datums+Zeit Angaben
		/// Wird kein Datum mitgegeben, wird das Tagesdatum verwendet
		/// z. B. dOut1 = SetDateByTagNr(1, DateTime.Now, new DateTime(2020,5,4))  wobei 1 Montag entspricht
		/// bzw. dOut1 = SetDateByTagNr(1, DateTime.Now)  wobei 1 Montag entspricht
		/// </summary>
		/// <param name="nTagNr">Wochentages Nummer Mo = 1 So = 7 </param>
		/// <param name="dTime">Zeitangabe als Datum</param>
		/// <param name="dt1">Referenzdatum - wenn leer dann aktuelles Tagesdatum</param>
		/// <returns></returns>
		public static DateTime SetDateByTagNrTime(int nTagNr, DateTime dTime, DateTime? dt1 = null)
		{
			DateTime dt = DateTime.Today;
			TimeSpan time = TimeSpan.Parse(dTime.ToShortTimeString());

			if (dt1 == null)
			{
				dt = DateTime.Today;
			}
			else
			{
				dt = (DateTime)dt1;
			}

			var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
			var diff = dt.DayOfWeek - (culture.DateTimeFormat.FirstDayOfWeek + nTagNr - 1);

			var nAdd = 0;

			if (diff < 0)
			{
				diff += 7;
				nAdd = 7;
			}

			return dt.AddDays(-diff + nAdd).Date.Add(time);
		}

		/// <summary>
		/// Erzeugt Datum aus TagNr, Zeitangabe als string und ggf. Referenzdatum für Scheduler Datums+Zeit Angaben
		/// Wird kein Datum mitgegeben, wird das Tagesdatum verwendet
		/// z. B. dOut1 = SetDateByTagNr(1, "11:30", new DateTime(2020,5,4))  wobei 1 Montag entspricht
		/// bzw. dOut1 = SetDateByTagNr(1, "11:30")  wobei 1 Montag entspricht
		/// </summary>
		/// <param name="nTagNr">Wochentages Nummer Mo = 1 So = 7 </param>
		/// <param name="dTime">Zeitangabe als Datum</param>
		/// <param name="dt1">Referenzdatum - wenn leer dann aktuelles Tagesdatum</param>
		/// <returns></returns>		
		public static DateTime SetDateByTagNrTime(int nTagNr, string cTime, DateTime? dt1 = null)
		{
			DateTime dt = DateTime.Today;
			TimeSpan time = TimeSpan.Parse(cTime);

			if (dt1 == null)
			{
				dt = DateTime.Today;
			}
			else
			{
				dt = (DateTime)dt1;
			}

			var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
			var diff = dt.DayOfWeek - (culture.DateTimeFormat.FirstDayOfWeek + nTagNr - 1);

			var nAdd = 0;

			if (diff < 0)
			{
				diff += 7;
				nAdd = 7;
			}

			return dt.AddDays(-diff + nAdd).Date.Add(time);
		}

		#endregion Datums Umwandlungen

		#endregion Datums und Zeitfunktionen

		#region SOM Erzeugung

		/// <summary>
		/// Erzeugung einer Zufallszahl
		/// </summary>
		/// <param name="nDigits" Zahl der maximalen Stellen (default = 4)></param>
		/// <returns></returns>
		public static int GetRandom(int nDigits = 4)
		{
			if (nDigits < 1 || nDigits > 6)
			{
				nDigits = 4;
			}

			string text = System.Guid.NewGuid().ToString();

			uint accumulator = 0;
			for (int i = 0; i < text.Length; i++)
			{
				var leftRotate4bit = (accumulator << 4) | (accumulator >> -4);
				accumulator = leftRotate4bit ^ text[i];
			}

			int nTmp = (int)accumulator;

			int nRandom = (Math.Abs(nTmp) % (int)(Math.Pow(10, nDigits))); // max 4 stellig

			return nRandom;

		} // end

		/// <summary>
		/// Erzeugung eines (S)chüler (O)rdnugs (M)erkmals (Schülernummer) aus Geburtsdatum / Tagesdatum 
		/// und einer nDigits-stelligen Zufallszahl, die jeweils mit Nullen bei Bedarf aufgefüllt wird.
		/// </summary>
		/// <param name="dDate" Geburtsdatum, beliebiges Datum oder null(Tagesdatum + nTage über Zufallszahl) ></param>
		/// <param name="nDigits" Menge der maximalen Stellen></param>
		/// <returns></returns>
		public static string GenSom(DateTime? dDate, int nDigits = 4)
		{
			if (nDigits < 4 || nDigits > 6)
			{
				nDigits = 4;
			}

			if (dDate is null)
			{
				dDate = DateTime.Today.AddDays(GetRandom(3));
			}

			string cDay = dDate.Value.Day.ToString();

			string cMonth = dDate.Value.ToString("ddMMyyyy").Substring(2, 2);

			string cYear = dDate.Value.ToString("ddMMyyyy").Substring(4, 4);

			int nRandom = GetRandom(nDigits);
			string cRandom = nRandom.ToString(new String('0', nDigits)); // 0 nDigits mal wiederholen

			return cDay + cMonth + cYear + cRandom;

		} // end

		/// <summary>
		/// erzeugt Som aus Geburtsdatum
		/// </summary>
		/// <param name="dDate"></param>
		/// <returns></returns>
		public static string GenSom(DateTime dDate)
		{
			string cDay = dDate.Day.ToString();

			string cMonth = dDate.ToString("ddMMyyyy").Substring(2, 2);

			string cYear = dDate.ToString("ddMMyyyy").Substring(4, 4);

			string cTmp = StrTran(Seconds().ToString(), ",", "");

			string cSecs = "0000";

			if (cTmp.Length >= 4)
			{
				cSecs = Right(cTmp, 4);
			}

			return cDay + cMonth + cYear + cSecs;

		} // end		       

		#endregion  SOM Erzeugung

		#region File Funktionen

		/// <summary>
		/// Bewegt Datei nach cDestination
		/// Existiert eine Datei schon im Zielverzeichnis, wird sie gelöscht
		/// </summary>
		/// <param name="cFullSourceFile">Source File Name mit Verzeichnis </param>
		/// <param name="cDestinationPath">Zielverzeichnis</param>
		public static void MoveFile(string cFullSourceFile, string cDestinationPath)
		{
			if (!File.Exists(cFullSourceFile))
			{
				return;
			}

			string cFileName = Path.GetFileName(cFullSourceFile);

			if (cFileName == null)
			{
				return;
			}

			string cFullDestinationFile = Path.Combine(cDestinationPath, cFileName);

			if (File.Exists(cFullDestinationFile))
			{
				File.Delete(cFullDestinationFile);
			}
			try
			{
				File.Move(cFullSourceFile, cFullDestinationFile);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("Fehler beim Verschieben der Datei", ex);
			}

		} // end

		#endregion File Funktionen

		#region Schuljahresberechnung / Einschulungsdatum

		/// <summary>
		/// erzeugt Einschulungsstatus aus Geburtsdatum und Einschulungsdatum
		/// </summary>
		/// <param name="Geburtsdatum"></param>
		/// <param name="Einschulungsdatum"></param>
		/// <returns></returns>
		public static string GetEinschulungsstatus(DateTime? Geburtsdatum, DateTime? Einschulungsdatum)
		{
			string cRetVal = "";

			if (Geburtsdatum != null && Einschulungsdatum != null)
			{
				if (((DateTime)Geburtsdatum).Year <= ((DateTime)Einschulungsdatum).Year - 6 && ((DateTime)Geburtsdatum).AddYears(6) <= new DateTime(((DateTime)Einschulungsdatum).Year, 9, 30))
				{
					cRetVal = "schulpflichtig";
				}
				else
				{
					cRetVal = "kann-Kind";
				}
			}

			return cRetVal;

		} // end

		/// <summary>
		/// Gibt aktuelles Schuljahr als int zurück
		/// </summary>
		/// <returns></returns>
		public static int GetCurrentSchuljahr()
		{
			string cSchulj = StringLib.Token(GetCurrentSchulj(true), "-", 1);

			int nRetVal = 0;

			try
			{
				nRetVal = Convert.ToInt32(cSchulj);
			}
			catch // (System.Exception ex)
			{
				nRetVal = 0;
			}

			return nRetVal;

		}

		/// <summary>
		/// Gibt das aktuelle Schuljahr mit entsprechendem Halbjahr zurück 
		/// </summary>
		/// <param name="lShort">Flag, ob Lang- oder Kurzform</param>
		/// <returns></returns>
		public static string GetCurrentSchulj(bool lShort)
		{
			string cRetVal = "";

			string cHalbjahr = "1";

			string cYear = "";

			if (System.DateTime.Today.Month < 8 && System.DateTime.Today.Month > 2) // zw. März und August
			{
				cHalbjahr = "2";

				cYear = (System.DateTime.Today.Year).ToString().Substring(2, 2);

				if (lShort)
				{
					cRetVal = (System.DateTime.Today.Year - 1).ToString() + "-2";
				}
				else
				{
					cRetVal = (System.DateTime.Today.Year - 1).ToString() + " / " + cYear + "  - 2. Halbjahr";
				}
			}

			if (cHalbjahr == "1")
			{
				if (System.DateTime.Today.Month < 3) // Jan + Feb. 1. Halbjahr
				{
					cYear = (System.DateTime.Today.Year).ToString().Substring(2, 2);

					if (lShort)
					{
						cRetVal = (System.DateTime.Today.Year - 1).ToString() + "-1";
					}
					else
					{
						cRetVal = (System.DateTime.Today.Year - 1).ToString() + " / " + cYear + "  - 1. Halbjahr";
					}
				}
				else // August - Dezember 1. Halbj.
				{
					cYear = (System.DateTime.Today.Year + 1).ToString().Substring(2, 2);

					if (lShort)
					{
						cRetVal = System.DateTime.Today.Year.ToString() + "-1";
					}
					else
					{
						cRetVal = System.DateTime.Today.Year.ToString() + " / " + cYear + "  - 1. Halbjahr";
					}
				}
			}

			return cRetVal;

		} // end of method GetCurrentSchulj

		/// <summary>
		/// Gibt das nächste Schuljahr mit entsprechendem Halbjahr zurück 
		/// </summary>
		/// <param name="lShort">Flag, ob Lang- oder Kurzform</param>
		/// <returns></returns>
		public static string GetNextSchulj(bool lShort)
		{
			string cRetVal = "";

			string cCurrent = GetCurrentSchulj(true);

			int nYear = Convert.ToInt32(StringLib.Token(cCurrent, "-", 1));

			string cPart = "1";

			if (lShort)
			{

				cRetVal = Convert.ToString(nYear + 1) + "-" + cPart;
			}
			else
			{
				string cYear = (nYear + 1).ToString().Substring(2, 2);

				cRetVal = nYear.ToString() + " / " + cYear + "   " + cPart + "." + " Halbjahr";
			}

			return cRetVal;

		} // end  

		/// <summary>
		/// Erzeugt langes aus kurzem Schuljahr
		/// </summary>
		/// <param name="lShort">Flag, ob Lang- oder Kurzform</param>
		/// <returns></returns>
		public static string GetLongSchulj(string cShort)
		{
			string cRetVal = "";

			int nYear = Convert.ToInt32(StringLib.Token(cShort, "-", 1));

			string cPart = StringLib.Token(cShort, "-", 2);

			string cYear = (nYear + 1).ToString().Substring(2, 2);

			cRetVal = nYear.ToString() + " / " + cYear + "   " + cPart + "." + " Halbjahr";

			return cRetVal;

		} // end  

		#endregion Schuljahresberechnung

		#region Dezimalstellen Umwandlung

		/// <summary>
		/// Returns a String (of a Double) with Dec.Separator = Dot or Comma,
		/// depending on Current Culture Decimal Separator
		/// Dot=46, Comma=44. So, (44 xor 2) = 46, (46 xor 2) = 44...
		/// </summary>
		/// <param name="cStr"></param>
		/// <returns></returns>
		public static string Str2DSCulture(string cStr)
		{
			// string cSep = Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

			char Dec_Separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);

			return cStr.Replace(((char)((int)(Dec_Separator) ^ 2)).ToString(), Dec_Separator.ToString());

		} // end


		/// <summary>
		/// Returns a String (of a Double) with Dec.Separator = Dot
		/// </summary>
		/// <param name="Str"></param>
		/// <returns></returns>
		public static string Str2DSDot(string Str)
		{
			return Str.Replace(",", ".");
		}

		#endregion

		#region Exceptions

		public static string GetInnerException(Exception ex)
		{
			if (ex.InnerException != null)
			{
				return string.Format("{0} > {1} ", ex.InnerException.Message, GetInnerException(ex.InnerException));
			}

			return string.Empty;
		}


		public static string GetAllException(Exception ex)
		{
			return ex.Message + "  --   " + GetInnerException(ex);
		}

		#endregion

		#region Converting ObjectArray to Datatable

		public static DataTable ConvertToDataTable(object[] array)
		{
			PropertyInfo[] properties = array.GetType().GetElementType().GetProperties();
			DataTable dt = CreateDataTable(properties);
			if (array.Length != 0)
			{
				foreach (object o in array)
					FillData(properties, dt, o);
			}
			return dt;
		}

		/// <summary>
		/// Hilfsmethode für ConvertToDataTable
		/// </summary>
		/// <param name="properties"></param>
		/// <returns></returns>
		private static DataTable CreateDataTable(PropertyInfo[] properties)
		{
			DataTable dt = new DataTable();
			DataColumn dc = null;
			foreach (PropertyInfo pi in properties)
			{
				// 				dc = new DataColumn();
				// 				dc.ColumnName = pi.Name;
				// 				dc.DataType = pi.PropertyType;
				// 				dt.Columns.Add(dc);

				dc = new DataColumn
				{
					ColumnName = pi.Name,
					DataType = pi.PropertyType
				};
				dt.Columns.Add(dc);

			}
			return dt;
		}

		/// <summary>
		/// Hilfsmethode für ConvertToDataTable
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="dt"></param>
		/// <param name="o"></param>
		private static void FillData(PropertyInfo[] properties, DataTable dt, object o)
		{
			DataRow dr = dt.NewRow();
			foreach (PropertyInfo pi in properties)
			{
				dr[pi.Name] = pi.GetValue(o, null);
			}
			dt.Rows.Add(dr);
		}

		/// <summary>
		/// Erzeugt DataTable aus List<T>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name=list - die umzuwandelde Liste></param>
		/// <param name=HideNull Flag, ob null Werte als Leerzeichen (default true)></param>
		/// <returns></returns>
		public static DataTable ListToDataTable<T>(List<T> list, bool HideNull = true)
		{
			DataTable dt = new DataTable();

			foreach (PropertyInfo info in typeof(T).GetProperties())
			{
				// dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
				var column = new DataColumn
				{
					ColumnName = info.Name,
					DataType = info.PropertyType.Name.Contains("Nullable") ? typeof(string) : info.PropertyType
				};

				dt.Columns.Add(column);

			}

			foreach (T t in list)
			{
				DataRow row = dt.NewRow();
				foreach (PropertyInfo info in typeof(T).GetProperties())
				{
					System.Diagnostics.Debug.WriteLine(info.GetValue(t, null));

					if (HideNull && info.GetValue(t, null) is null)
					{
						row[info.Name] = "";
					}
					else
					{
						row[info.Name] = info.GetValue(t, null);
					}

				}
				dt.Rows.Add(row);
			}
			return dt;

		} // end

		#endregion

		/// <summary>
		/// Wandelt eine Komma separierte Liste der Form "1|one, 2|two" in eine DataTable um
		/// </summary>
		/// <param name="cListe"></param>
		/// <returns></returns>
		public static DataTable IntStringListToDt(string cListe)
		{
			List<string> oCheck = Misc.String2List(cListe);

			List<IntStringRec> oListe = new List<IntStringRec>();

			for (int i = 0; i < oCheck.Count; i++)
			{
				string oItem = oCheck[i];

				IntStringRec oRec = new IntStringRec()
{
					Id = Convert.ToInt32(StringLib.Token(oItem, "|", 1)),
					Value = StringLib.Token(oItem, "|", 2)
				};

				oListe.Add(oRec);
			}

			DataTable dt = Misc.ConvertToDataTable(oListe.ToArray());

			return dt;
		}

		/// <summary>
		/// Wandelt eine Komma separierte Liste der Form "1|one, 2|two" in eine IdValuePairList um
		/// </summary>
		/// <param name="cListe"></param>
		/// <returns></returns>
		public static IdValuePairList IntStringListToIdValuePairList(string cListe)
		{
			List<string> oCheck = Misc.String2List(cListe);

			IdValuePairList oListe = new IdValuePairList();

			for (int i = 0; i < oCheck.Count; i++)
			{
				string oItem = oCheck[i];

				IntStringRec oRec = new IntStringRec()
{
					Id = Convert.ToInt32(StringLib.Token(oItem, "|", 1)),
					Value = StringLib.Token(oItem, "|", 2)
				};

				oListe.Add(oRec);
			}

			return oListe;

		} // end

		public static void ShowDebugInfo(bool IsWasm, string cInfo)
		{
			if (IsWasm)
			{
				Console.WriteLine(cInfo);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine(cInfo);
			}

		} // end

		/// <summary>
		/// convert datatable to list
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static List<ExpandoObject> GenerateListFromTable(DataTable input)
		{
		    var list = new List<ExpandoObject>();

			if (input != null) 
			{
				foreach (DataRow row in input.Rows)
				{
				    System.Dynamic.ExpandoObject e = new System.Dynamic.ExpandoObject();

				    foreach (DataColumn col in input.Columns)
					{ 
				        e.TryAdd(col.ColumnName, row.ItemArray[col.Ordinal]);
					}

				    list.Add(e);
				}

			}

		    return list;

		} // end

	} // end of class

	/// <summary>
	/// Hilfs Modell für DataTable Struktur
	/// </summary>
	public class IntStringRec
	{
		public int Id { get; set; }
		public string Value { get; set; }
	}


	public static class ListExtensions
	{
		/// <summary>
		/// Erzeugt DataTable aus List<T>
		/// Aufruf: List<T> oList = new List<T>()
		///		DataTable dt = oList.ToDataTable(false);
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name=list - die umzuwandelde Liste></param>
		/// <param name=HideNull Flag, ob null Werte als Leerzeichen (default true)></param>
		/// <returns></returns>
		public static DataTable ToDataTable<T>(this List<T> list, bool HideNull = true)
		{
			DataTable table = new DataTable(typeof(T).Name);

			//Get Properites of List Fiels
			PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			//Create Columns as Fields of List
			foreach (PropertyInfo propertyInfo in props)
			{
				var column = new DataColumn
				{
					ColumnName = propertyInfo.Name,
					DataType = propertyInfo.PropertyType.Name.Contains("Nullable") ? typeof(string) : propertyInfo.PropertyType
				};

				table.Columns.Add(column);
			}

			//Fill DataTable with Rows of List
			foreach (var item in list)
			{
				var values = new object[props.Length];

				for (var i = 0; i < props.Length; i++)
				{
					if (HideNull && props[i].GetValue(item, null) is null)
					{
						values[i] = "";
					}
					else
					{
						values[i] = props[i].GetValue(item, null);
					}
				}

				table.Rows.Add(values);
			}

			return table;
		}

	} // end


} // end of namespace

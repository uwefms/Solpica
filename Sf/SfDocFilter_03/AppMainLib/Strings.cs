using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AppMainLib
{
	public static class StringLib
	{
		/// <summary>
		/// Removes leading and trailing spaces from cExpression
		/// </summary>
		/// <param name="cExpression"></param>
		public static string AllTrim(string cExpression)
		{
			return cExpression.Trim();
		}


		/// <summary>
		/// Receives a character as a parameter and returns its ANSI code
		/// <pre>
		/// Example
		/// Asc('#');		//returns 35
		/// </pre>
		/// </summary>
		/// <param name="cCharacter"> </param>
		public static int Asc(char cCharacter)
		{
			return (int)cCharacter;
		}

		/// <summary>
		/// Receives two strings as parameters and searches for one string within another. 
		/// If found, returns the beginning numeric position otherwise returns 0
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.At("D", "Joe Doe");	//returns 5
		/// </pre>
		/// </summary>
		/// <param name="cSearchFor"> </param>
		/// <param name="cSearchIn"> </param>
		public static int At(string cSearchFor, string cSearchIn)
		{
			return cSearchIn.IndexOf(cSearchFor) + 1;
		}

		/// <summary>
		/// Receives two strings and an occurence position (1st, 2nd etc) as parameters and 
		/// searches for one string within another for that position. 
		/// If found, returns the beginning numeric position otherwise returns 0
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.At("o", "Joe Doe", 1);	//returns 2
		/// VFPToolkit.StringLib.At("o", "Joe Doe", 2);	//returns 6
		/// </pre>
		/// </summary>
		/// <param name="cSearchFor"> </param>
		/// <param name="cSearchIn"> </param>
		/// <param name="nOccurence"> </param>
		public static int At(string cSearchFor, string cSearchIn, int nOccurence)
		{
			return __at(cSearchFor, cSearchIn, nOccurence, 1);
		}

		/// Private Implementation: This is the actual implementation of the At() and RAt() functions. 
		/// Receives two strings, the expression in which search is performed and the expression to search for. 
		/// Also receives an occurence position and the mode (1 or 0) that specifies whether it is a search
		/// from Left to Right (for At() function)  or from Right to Left (for RAt() function)
#pragma warning disable IDE1006 // Naming Styles
		private static int __at(string cSearchFor, string cSearchIn, int nOccurence, int nMode)
#pragma warning restore IDE1006 // Naming Styles
		{
			//In this case we actually have to locate the occurence
			int i;
			int nOccured = 0;
			int nPos;

			if (nMode == 1)
			{
				nPos = 0;
			}
			else
			{
				nPos = cSearchIn.Length;
			}

			//Loop through the string and get the position of the requiref occurence
			for (i = 1; i <= nOccurence; i++)
			{
				if (nMode == 1) { nPos = cSearchIn.IndexOf(cSearchFor, nPos); }
				else { nPos = cSearchIn.LastIndexOf(cSearchFor, nPos); }

				if (nPos < 0)
				{
					//This means that we did not find the item
					break;
				}
				else
				{
					//Increment the occured counter based on the current mode we are in
					nOccured++;

					//Check if this is the occurence we are looking for
					if (nOccured == nOccurence)
					{
						return nPos + 1;
					}
					else
					{
						if (nMode == 1) { nPos++; }
						else { nPos--; }

					}
				}
			}
			//We never found our guy if we reached here
			return 0;
		}


		/// <summary>
		/// Receives two strings as parameters and searches for one string within another. 
		/// This function ignores the case and if found, returns the beginning numeric position 
		/// otherwise returns 0
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.AtC("d", "Joe Doe");	//returns 5
		/// </pre>
		/// </summary>
		/// <param name="cSearchFor"> </param>
		/// <param name="cSearchIn"> </param>
		public static int AtC(string cSearchFor, string cSearchIn)
		{
			return cSearchIn.ToLower().IndexOf(cSearchFor.ToLower()) + 1;
		}

		/// <summary>
		/// Receives two strings and an occurence position (1st, 2nd etc) as parameters and 
		/// searches for one string within another for that position. This function ignores the
		/// case of both the strings and if found, returns the beginning numeric position 
		/// otherwise returns 0.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.AtC("d", "Joe Doe", 1);	//returns 5
		/// VFPToolkit.StringLib.AtC("O", "Joe Doe", 2);	//returns 6
		/// </pre>
		/// </summary>
		/// <param name="cSearchFor"> </param>
		/// <param name="cSearchIn"> </param>
		/// <param name="nOccurence"> </param>
		public static int AtC(string cSearchFor, string cSearchIn, int nOccurence)
		{
			return __at(cSearchFor.ToLower(), cSearchIn.ToLower(), nOccurence, 1);
		}

		/// <summary>
		/// Receives an integer ANSI code and returns a character associated with it
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Chr(35);		//returns '#'
		/// </pre>
		/// </summary>
		/// <param name="nAnsiCode"> </param>
		public static char Chr(int nAnsiCode)
		{
			return (char)nAnsiCode;
		}

		/// <summary>
		/// Replaces each character in a character expression that matches a character
		/// in a second character expression with the corresponding character in a 
		/// third character expression
		/// </summary>
		/// <example>
		/// Console.WriteLine(ChrTran("ABCDEF", "ACE", "XYZ"));  //Displays XBYDZF
		/// Console.WriteLine(ChrTran("ABCD", "ABC", "YZ"));	//Displays YZD
		/// Console.WriteLine(ChrTran("ABCDEF", "ACE", "XYZQRST"));	//Displays XBYDZF
		/// </example>
		/// <param name="cSearchIn"> </param>
		/// <param name="cSearchFor"> </param>
		/// <param name="cReplaceWith"> </param>
		public static string ChrTran(string cSearchIn, string cSearchFor, string cReplaceWith)
		{
			string lcRetVal = cSearchIn;
			string cReplaceChar;
			for (int i = 0; i < cSearchFor.Length; i++)
			{
				if (cReplaceWith.Length <= i)
					cReplaceChar = "";
				else
					cReplaceChar = cReplaceWith[i].ToString();

				lcRetVal = StrTran(lcRetVal, cSearchFor[i].ToString(), cReplaceChar);
			}
			return lcRetVal;
		}


		/// <summary>
		/// Converts character type data to binary type character string. Just a place
		/// holder function as this becomes irrelevent now.
		/// </summary>
		/// <param name="cExpression"> </param>
		public static string CreateBinary(string cExpression)
		{
			//This decorator simply returns the expression back
			//This is because we used this function to converts VFP strings to binary type
			//character string so they could be passed to an ActiveX control or
			//automation object. In the .NET Framework this becomes irrelevent
			return cExpression;
		}

		/// <summary>
		/// Receives a file name as a parameter and returns the contents of that file
		/// as a string.
		/// </summary>
		/// Example:
		/// VFPToolkit.StringLib.FileToStr("c:\\My Folders\\MyFile.txt");	//returns the contents of the file
		/// </pre>
		/// </summary>
		/// <param name="cFileName"> </param>
		public static string FileToStr(string cFileName)
		{
			//Create a StreamReader and open the file
			StreamReader oReader = System.IO.File.OpenText(cFileName);

			//Read all the contents of the file in a string
			string lcString = oReader.ReadToEnd();

			//Close the StreamReader and return the string
			oReader.Close();
			return lcString;
		}

		/// <summary>
		/// Receives a string as a parameter and counts the number of words in that string
		/// <pre>
		/// Example:
		/// string lcString = "Joe Doe is a good man";
		/// VFPToolkit.StringLib.GetWordCount(lcString);		//returns 6
		/// </pre>
		/// </summary>
		/// <param name="cString"> </param>
		public static long GetWordCount(string cString)
		{
			int i;
			long nLength = cString.Length;
			long nWordCount = 0;

			//Begin by checking for the first word
			if (!Char.IsWhiteSpace(cString[0]))
			{
				nWordCount++;
			}

			//Now look for white spaces and count each word
			for (i = 0; i < nLength; i++)
			{
				//Check for a space to begin counting a word
				if (Char.IsWhiteSpace(cString[i]))
				{
					//We think we encountered a word
					//Remove any following white spaces if any after this word
					do
					{
						//Check if we have reached the limit and if so then exit the loop
						i++;
						if (i >= nLength) { break; }
						if (!Char.IsWhiteSpace(cString[i]))
						{
							nWordCount++;
							break;
						}
					} while (true);

				}

			}
			return nWordCount;
		}

		/// <summary>
		/// Based on the position specified, returns a word from a string 
		/// Receives a string as a parameter and counts the number of words in that string
		/// <pre>
		/// Example:
		/// string lcString = "Joe Doe is a good man";
		/// VFPToolkit.StringLib.GetWordNumber(lcString, 5);		//returns "good"
		/// </pre>
		/// </summary>
		/// <param name="cString"> </param>
		/// <param name="nWordPosition"> </param>
		public static string GetWordNumb(string cString, int nWordPosition)
		{
			int nBeginPos = StringLib.At(" ", cString, nWordPosition - 1);
			int nEndPos = StringLib.At(" ", cString, nWordPosition);
			return StringLib.SubStr(cString, nBeginPos + 1, nEndPos - 1 - nBeginPos);
		}


		/// <summary>
		/// Returns a bool indicating if the first character in a string is an alphabet or not
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.IsAlpha("Joe Doe");		//returns true
		/// 
		/// Tip: This method uses Char.IsAlpha(char) to check if it is an alphabet or not. 
		///      In order to check if the first character is a digit use Char.IsDigit(char)
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		public static bool IsAlpha(string cExpression)
		{
			//Check if the first character is a letter
			return Char.IsLetter(cExpression[0]);
		}

		/// <summary>
		/// Checks if the first character of a string is a lowercase char and if so then returns true
		/// </summary>
		/// <param name="cExpression"></param>
		/// <returns></returns>
		public static bool IsLower(string cExpression)
		{
			try
			{
				//Get the first character in the string
				string lcString = cExpression.Substring(0, 1);

				//Return a bool indicating if the char is an lowercase or not
				return lcString == lcString.ToLower();
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				//In case of an error return false
				return false;
			}
#pragma warning restore CA1031 // Do not catch general exception types
		}

		/// <summary>
		/// Checks if the first character of a string is an uppercase and if so then returns true
		/// </summary>
		/// <param name="cExpression"></param>
		/// <returns></returns>
		public static bool IsUpper(string cExpression)
		{
			try
			{
				//Get the first character in the string
				string lcString = cExpression.Substring(0, 1);

				//Return a bool indicating if the char is an uppercase or not
				return lcString == lcString.ToUpper();
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				//In case of an error return false
				return false;
			}
#pragma warning restore CA1031 // Do not catch general exception types
		}

		/// <summary>
		/// Receives a string and the number of characters as parameters and returns 
		/// the specified number of leftmost characters of that string
		/// </summary>
		/// <param name="cExpression"></param>
		/// <param name="nDigits"></param>
		/// <returns></returns>
		public static string Left(string cExpression, int nDigits)
		{
			string cRetVal;

			if (cExpression.Trim().Length <= nDigits)
			{
				cRetVal = cExpression.Trim();
			}
			else
			{
				try
				{
					cRetVal = cExpression.Substring(0, nDigits);
				}
#pragma warning disable CA1031 // Do not catch general exception types
				catch (Exception)
				{
					cRetVal = "";
				}
#pragma warning restore CA1031 // Do not catch general exception types
			}

			return cRetVal;
		}

		/// <summary>
		/// Receives a string as a parameter and returns the length of the string
		/// <pre>
		/// Example:
		/// string MyString = "Hi there";
		/// VFPToolkit.StringLib.Len(MyString);	//returns 8
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		public static int Len(string cExpression)
		{
			return cExpression.Length;
		}

		/// <summary>
		/// Receives a string as a parameter and converts it to lowercase
		/// </summary>
		/// <param name="cExpression"> </param>
		public static string Lower(string cExpression)
		{
			//Call the ToLower() method of the string object
			return cExpression.ToLower();
		}

		/// <summary>
		/// Removes the leading spaces in cExpression
		/// </summary>
		/// <param name="cExpression"> </param>
		public static string LTrim(string cExpression)
		{
			//Hint: Pass null as the first parameter to remove white spaces
			return cExpression.TrimStart(null);
		}

		/// <summary>
		/// Returns the number of occurences of a character within a string
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Occurs('o', "Joe Doe");		//returns 2
		/// 
		/// Tip: If we have a string say lcString, then lcString[3] gives us the 3rd character in the string
		/// </pre>
		/// </summary>
		/// <param name="cChar"> </param>
		/// <param name="cExpression"> </param>
		public static int Occurs(char tcChar, string cExpression)
		{
			int i, nOccured = 0;

			//Loop through the string
			for (i = 0; i < cExpression.Length; i++)
			{
				//Check if each expression is equal to the one we want to check against
				if (cExpression[i] == tcChar)
				{
					//if  so increment the counter
					nOccured++;
				}
			}
			return nOccured;
		}
		/// <summary>
		/// Returns the number of occurences of one string within another string
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Occurs("oe", "Joe Doe");		//returns 2
		/// VFPToolkit.StringLib.Occurs("Joe", "Joe Doe");		//returns 1
		/// 
		/// Tip: String.IndexOf() searches the string (starting from left) for another character or string expression
		/// </pre>
		/// </summary>
		/// <param name="cString"> </param>
		/// <param name="cExpression"> </param>
		public static int Occurs(string cString, string cExpression)
		{
			int nPos = 0;
			int nOccured = 0;
			do
			{
				try
				{
					//Look for the search string in the expression
					nPos = cExpression.IndexOf(cString, nPos);
				}
#pragma warning disable CA1031 // Do not catch general exception types
				catch (Exception)
				{
					nPos = -1;
				}
#pragma warning restore CA1031 // Do not catch general exception types

				if (nPos < 0)
				{
					//This means that we did not find the item
					break;
				}
				else
				{
					//Increment the occured counter based on the current mode we are in
					nOccured++;
					nPos++;
				}
			} while (true);

			//Return the number of occurences
			return nOccured;
		}

		/// <summary>
		/// Receives a string and the length of the result string as parameters. Pads blank 
		/// characters on the both sides of this string and returns a string with the length specified.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.PadL("Joe Doe", 10);		//returns " Joe Doe  "
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nResultSize"> </param>
		public static string PadC(string cExpression, int nResultSize)
		{
			//Determine the number of padding characters
			int nPaddTotal = nResultSize - cExpression.Length;
			int lnHalfLength = (int)(nPaddTotal / 2);

			string lcString = PadL(cExpression, cExpression.Length + lnHalfLength);
			return lcString.PadRight(nResultSize);
		}

		/// <summary>
		/// Receives a string, the length of the result string and the padding character as 
		/// parameters. Pads the padding character on both sides of this string and returns a string 
		/// with the length specified.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.PadL("Joe Doe", 10, 'x');		//returns "xJoe Doexx"
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nResultSize"> </param>
		/// <param name="cPaddingChar"> </param>
		public static string PadC(string cExpression, int nResultSize, char cPaddingChar)
		{
			//Determine the number of padding characters
			int nPaddTotal = nResultSize - cExpression.Length;
			int lnHalfLength = (int)(nPaddTotal / 2);

			string lcString = PadL(cExpression, cExpression.Length + lnHalfLength, cPaddingChar);
			return lcString.PadRight(nResultSize, cPaddingChar);
		}

		/// <summary>
		/// Receives a string and the length of the result string as parameters. Pads blank 
		/// characters on the left of this string and returns a string with the length specified.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.PadL("Joe Doe", 10);		//returns "   Joe Doe"
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nResultSize"> </param>
		public static string PadL(string cExpression, int nResultSize)
		{ return cExpression.PadLeft(nResultSize); }

		/// <summary>
		/// Receives a string, the length of the result string and the padding character as 
		/// parameters. Pads the padding character on the left of this string and returns a string 
		/// with the length specified.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.PadL("Joe Doe", 10, 'x');		//returns "xxxJoe Doe"
		/// 
		/// Tip: Use single quote to create a character type data and double quotes for strings
		/// </pre>
		/// </summary>
		public static string PadL(string cExpression, int nResultSize, char cPaddingChar)
		{ return cExpression.PadLeft(nResultSize, cPaddingChar); }

		/// <summary>
		/// Receives a string and the length of the result string as parameters. Pads blank 
		/// characters on the right of this string and returns a string with the length specified.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.PadL("Joe Doe", 10);		//returns "Joe Doe   "
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nResultSize"> </param>
		// 		public static string PadR(string cExpression, int nResultSize)
		// 		{return cExpression.PadRight(nResultSize);}


		/// <summary>
		/// Receives a string, the length of the result string and the padding character as 
		/// parameters. Pads the padding character on the right of this string and returns a string 
		/// with the length specified.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.PadL("Joe Doe", 10, 'x');		//returns "Joe Doexxx"
		/// 
		/// Tip: Use single quote to create a character type data and double quotes for strings
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nResultSize"> </param>
		/// <param name="cPaddingChar"> </param>
		// 		public static string PadR(string cExpression, int nResultSize, char cPaddingChar)
		// 		{return cExpression.PadRight(nResultSize, cPaddingChar);}

		public static string PadR(string cExpression, int nResultSize, char cPaddingChar)
		{
			int nExLen = cExpression.Length;

			int nLen = 0;

			if (nResultSize - nExLen >= 0)
			{
				nLen = nResultSize - nExLen;
			}

			return cExpression.PadRight(nLen, cPaddingChar);
		}

		public static string PadR(string cExpression, int nResultSize)
		{
			int nExLen = cExpression.Length;

			int nLen = 0;

			if (nResultSize - nExLen >= 0)
			{
				nLen = nResultSize - nExLen;
			}

			return cExpression.PadRight(nLen);
		}

		/// <summary>
		/// Hilfsmethode für Proper
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		private static string Evaluator(Match match)
		{
			return match.Value.ToLower();
		}


		/// <summary>
		/// Entspricht der Proper Funktion
		/// </summary>
		/// <param name="inputString"></param>
		/// <returns></returns>
		public static string Proper(String inputString)
		{
			Regex rex = new Regex(@"\B[A-Z,ÖÄÜ]");
			return rex.Replace(inputString, new MatchEvaluator(StringLib.Evaluator));
		}

		/// <summary>
		/// Receives two strings as parameters and searches for one string within another. 
		/// The search is performed starting from Right to Left and if found, returns the 
		/// beginning numeric position otherwise returns 0
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.RAt("o", "Joe Doe");	//returns 6
		/// </pre>
		/// </summary>
		/// <param name="cSearchFor"> </param>
		/// <param name="cSearchIn"> </param>
		public static int RAt(string cSearchFor, string cSearchIn)
		{
			return cSearchIn.LastIndexOf(cSearchFor) + 1;
		}

		/// <summary>
		/// Receives two strings as parameters and an occurence position as parameters. 
		/// The function searches for one string within another and the search is performed 
		/// starting from Right to Left and if found, returns the beginning numeric position 
		/// otherwise returns 0
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.RAt("o", "Joe Doe", 1);	//returns 6
		/// VFPToolkit.StringLib.RAt("o", "Joe Doe", 2);	//returns 2
		/// </pre>
		/// </summary>
		/// <param name="cSearchFor"> </param>
		/// <param name="cSearchIn"> </param>
		/// <param name="nOccurence"> </param>
		public static int RAt(string cSearchFor, string cSearchIn, int nOccurence)
		{
			return __at(cSearchFor, cSearchIn, nOccurence, 0);
		}

		/// <summary>
		/// Receives a string expression and a numeric value indicating number of time
		/// and replicates that string for the specified number of times.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Replicate("Joe", 5);		//returns JoeJoeJoeJoeJoe
		/// 
		/// Tip: Use a StringBuilder when lengthy string manipulations are required.
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nTimes"> </param>
		public static string Replicate(string cExpression, int nTimes)
		{
			//Create a stringBuilder
			StringBuilder sb = new StringBuilder();

			//Insert the expression into the StringBuilder for nTimes
			sb.Insert(0, cExpression, nTimes);

			//Convert it to a string and return it back
			return sb.ToString();
		}

		/// <summary>
		/// Receives a string and the number of characters as parameters and returns the
		/// specified number of rightmost characters of that string
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Right("Joe Doe", 3);	//returns "Doe"
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nDigits"> </param>
		public static string Right(string cExpression, int nDigits)
		{
			string cRetVal;

			if (cExpression.Trim().Length <= nDigits)
			{
				cRetVal = cExpression.Trim();
			}
			else
			{
				try
				{
					cRetVal = cExpression.Substring(cExpression.Length - nDigits);
				}
#pragma warning disable CA1031 // Do not catch general exception types
				catch (Exception)
				{
					cRetVal = "";
				}
#pragma warning restore CA1031 // Do not catch general exception types
			}

			return cRetVal;
		}

		#region alte Version
		// 		public static string Right(string cExpression, int nDigits)
		// 		{
		// 			string cRetVal;
		// 
		// 			try
		// 			{
		// 				cRetVal = cExpression.Substring(cExpression.Length - nDigits);
		// 			}
		// 			catch
		// 			{
		// 				cRetVal = "";
		// 			}
		// 
		// 			// return cExpression.Substring(cExpression.Length - nDigits);
		// 
		// 			return cRetVal;
		// 		}
		#endregion

		/// <summary>
		/// Removes the trailing spaces in cExpression
		/// </summary>
		/// <example>
		/// VfpToolkit.StringLib.RTrim("VFPToolkitNET     "); //returns "VFPToolkitNET"
		/// </example>
		/// <param name="cExpression"> </param>
		public static string RTrim(string cExpression)
		{
			//Hint: Pass null as the first parameter to remove white spaces
			return cExpression.TrimEnd(null);
		}

		/// <summary>
		/// Receives an integer as a parameter and returns an empty string of that length
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Space(20);	//returns a string with 20 spaces
		/// </pre>
		/// </summary>
		/// <param name="nSpaces"> </param>
		public static string Space(int nSpaces)
		{
			//Create a new string and return those many spaces in it
			char val = ' ';
			return new string(val, nSpaces);
		}

		/// <summary>
		/// Receives an integer/decimal/double as a parameter and converts it to a string.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Str(135);	//returns "135"
		/// </pre>
		/// </summary>
		/// <param name="nNumber"> </param>
		public static string Str(int nNumber)
		{
			return nNumber.ToString();
		}

		/// <summary>
		/// Overloaded method for receiving a parameter of type double
		/// </summary>
		/// <param name="nNumber"> </param>
		public static string Str(double nNumber)
		{
			return nNumber.ToString();
		}

		/// <summary>
		/// Overloaded method for receiving a parameter of type decimal
		/// </summary>
		/// <param name="nNumber"> </param>
		public static string Str(decimal nNumber)
		{
			return nNumber.ToString();
		}


		/// <summary>
		/// Receives a string along with starting and ending delimiters and returns the 
		/// part of the string between the delimiters. Receives a beginning occurence
		/// to begin the extraction from and also receives a flag (0/1) where 1 indicates
		/// that the search should be case insensitive.
		/// <pre>
		/// Example:
		/// string cExpression = "JoeDoeJoeDoe";
		/// VFPToolkit.StringLib.StrExtract(cExpression, "o", "eJ", 1, 0);		//returns "eDo"
		/// </pre>
		/// </summary>
		public static string StrExtract(string cSearchExpression, string cBeginDelim, string cEndDelim, int nBeginOccurence, int nFlags)
		{
			string cstring = cSearchExpression;
			string cb = cBeginDelim;
			string ce = cEndDelim;
			string lcRetVal = "";

			//Check for case-sensitive or insensitive search
			if (nFlags == 1)
			{
				cstring = cstring.ToLower();
				cb = cb.ToLower();
				ce = ce.ToLower();
			}

			//Lookup the position in the string
			int nbpos = At(cb, cstring, nBeginOccurence) + cb.Length - 1;
			int nepos = cstring.IndexOf(ce, nbpos + 1);

			//Extract the part of the strign if we get it right
			if (nepos > nbpos)
			{
#pragma warning disable IDE0057 // Use range operator
				lcRetVal = cSearchExpression.Substring(nbpos, nepos - nbpos);
#pragma warning restore IDE0057 // Use range operator
				// lcRetVal = cSearchExpression[nbpos..nepos];
			}

			return lcRetVal;
		}

		/// <summary>
		/// Receives a string and a delimiter as parameters and returns a string starting 
		/// from the position after the delimiter
		/// <pre>
		/// Example:
		/// string cExpression = "JoeDoeJoeDoe";
		/// VFPToolkit.StringLib.StrExtract(cExpression, "o");		//returns "eDoeJoeDoe"
		/// </pre>
		/// </summary>
		/// <param name="cSearchExpression"> </param>
		/// <param name="cBeginDelim"> </param>
		public static string StrExtract(string cSearchExpression, string cBeginDelim)
		{
			int nbpos = At(cBeginDelim, cSearchExpression);
			return cSearchExpression.Substring(nbpos + cBeginDelim.Length - 1);
		}

		/// <summary>
		/// Receives a string along with starting and ending delimiters and returns the 
		/// part of the string between the delimiters
		/// <pre>
		/// Example:
		/// string cExpression = "JoeDoeJoeDoe";
		/// VFPToolkit.StringLib.StrExtract(cExpression, "o", "eJ");		//returns "eDo"
		/// </pre>
		/// </summary>
		/// <param name="cSearchExpression"> </param>
		/// <param name="cBeginDelim"> </param>
		/// <param name="cEndDelim"> </param>
		public static string StrExtract(string cSearchExpression, string cBeginDelim, string cEndDelim)
		{
			return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, 1, 0);
		}

		/// <summary>
		/// Receives a string along with starting and ending delimiters and returns the 
		/// part of the string between the delimiters. It also receives a beginning occurence
		/// to begin the extraction from.
		/// <pre>
		/// Example:
		/// string cExpression = "JoeDoeJoeDoe";
		/// VFPToolkit.StringLib.StrExtract(cExpression, "o", "eJ", 2);		//returns ""
		/// </pre>
		/// </summary>
		/// <param name="cSearchExpression"> </param>
		/// <param name="cBeginDelim"> </param>
		/// <param name="cEndDelim"> </param>
		/// <param name="nBeginOccurence"> </param>
		public static string StrExtract(string cSearchExpression, string cBeginDelim, string cEndDelim, int nBeginOccurence)
		{
			return StrExtract(cSearchExpression, cBeginDelim, cEndDelim, nBeginOccurence, 0);
		}

		/// <summary>
		/// Receives a string and a file name as parameters and writes the contents of the
		/// string to that file
		/// <pre>
		/// Example:
		/// string lcString = "This is the line we want to insert in our file.";
		/// VFPToolkit.StringLib.StrToFile(lcString, "c:\\My Folders\\MyFile.txt");
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="cFileName"> </param>
		public static void StrToFile(string cExpression, string cFileName)
		{
			//Check if the sepcified file exists
			if (System.IO.File.Exists(cFileName) == true)
			{
				//If so then Erase the file first as in this case we are overwriting
				System.IO.File.Delete(cFileName);
			}

			//Create the file if it does not exist and open it
			FileStream oFs = new FileStream(cFileName, FileMode.CreateNew, FileAccess.ReadWrite);

			//Create a writer for the file
			StreamWriter oWriter = new StreamWriter(oFs);

			//Write the contents
			oWriter.Write(cExpression);
			oWriter.Flush();
			oWriter.Close();

			oFs.Close();
		}
		/// <summary>
		/// Receives a string and a file name as parameters and writes the contents of the
		/// string to that file. Receives an additional parameter specifying whether the 
		/// contents should be appended at the end of the file
		/// <pre>
		/// Example:
		/// string lcString = "This is the line we want to insert in our file.";
		/// VFPToolkit.StringLib.StrToFile(lcString, "c:\\My Folders\\MyFile.txt");
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="cFileName"> </param>
		/// <param name="lAdditive"> </param>
#pragma warning disable IDE0060 // Remove unused parameter
		public static void StrToFile(string cExpression, string cFileName, bool lAdditive)
#pragma warning restore IDE0060 // Remove unused parameter
		{
			//Create the file if it does not exist and open it
			FileStream oFs = new FileStream(cFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

			//Create a writer for the file
			StreamWriter oWriter = new StreamWriter(oFs);

			//Set the pointer to the end of file
			oWriter.BaseStream.Seek(0, SeekOrigin.End);

			//Write the contents
			oWriter.Write(cExpression);
			oWriter.Flush();
			oWriter.Close();
			oFs.Close();
		}

		/// <summary>
		/// Searches one string into another string and replaces all occurences with
		/// a blank character.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.StrTran("Joe Doe", "o");		//returns "J e D e" :)
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
		/// VFPToolkit.StringLib.StrTran("Joe Doe", "o", "ak");		//returns "Jake Dake" 
		/// </pre>
		/// </summary>
		/// <param name="cSearchIn"> </param>
		/// <param name="cSearchFor"> </param>
		/// <param name="cReplaceWith"> </param>
		public static string StrTran(string cSearchIn, string cSearchFor, string cReplaceWith)
		{
			//Create the StringBuilder
			StringBuilder sb = new StringBuilder(cSearchIn);

			//There is a bug in the replace method of the StringBuilder
			// sb.Replace(cSearchFor, cReplaceWith);

			//Call the Replace() method of the StringBuilder and specify the string to replace with
			return sb.Replace(cSearchFor, cReplaceWith).ToString();
		}

		/// Searches one string into another string and replaces each occurences with
		/// a third string. The fourth parameter specifies the starting occurence and the 
		/// number of times it should be replaced
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.StrTran("Joe Doe", "o", "ak", 2, 1);		//returns "Joe Dake" 
		/// </pre>
		public static string StrTran(string cSearchIn, string cSearchFor, string cReplaceWith, int nStartoccurence, int nCount)
		{
			//Create the StringBuilder
			StringBuilder sb = new StringBuilder(cSearchIn);

			//There is a bug in the replace method of the StringBuilder
			// sb.Replace(cSearchFor, cReplaceWith);

			//Call the Replace() method of the StringBuilder specifying the replace with string, occurence and count
			return sb.Replace(cSearchFor, cReplaceWith, nStartoccurence, nCount).ToString();
		}


		/// <summary>
		/// Receives a string (cExpression) as a parameter and replaces a specified number 
		/// of characters in that string (nCharactersReplaced) from a specified location
		/// (nStartReplacement) with a specified string (cReplacement)
		/// <pre>
		/// Example:
		/// string lcString = "Joe Doe";
		/// string lcReplace = "Foo ";
		/// VFPToolkit.StringLib.Stuff(lcString, 5, 0, lcReplace);	//returns "Joe Foo Doe";
		/// VFPToolkit.StringLib.Stuff(lcString, 5, 3, lcReplace);	//returns "Joe Foo ";
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nStartReplacement"> </param>
		/// <param name="nCharactersReplaced"> </param>
		/// <param name="cReplacement"> </param>
		public static string Stuff(string cExpression, int nStartReplacement, int nCharactersReplaced, string cReplacement)
		{
			//Create a stringbuilder to work with the string
			StringBuilder sb = new StringBuilder(cExpression);

			if (nCharactersReplaced + nStartReplacement - 1 >= cExpression.Length)
				nCharactersReplaced = cExpression.Length - nStartReplacement + 1;


			//First remove the characters specified in nCharacterReplaced
			if (nCharactersReplaced != 0)
			{
				sb.Remove(nStartReplacement - 1, nCharactersReplaced);
			}

			//Now Add the new string at the right location
			//sb.Insert(0,cExpression,nTimes);
			sb.Insert(nStartReplacement - 1, cReplacement);
			return sb.ToString();
		}


		/// <summary>
		/// Receives a string as a parameter and returns a part of the string based on the parameters specified.
		/// <pre>
		/// string lcName = "Joe Doe";
		/// SubStr(lcName, 1, 3);	//returns "Joe"
		/// SubStr(lcName, 5);	//returns Doe
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nStartPosition"> </param>
		public static string SubStr(string cExpression, int nStartPosition)
		{
			return cExpression.Substring(nStartPosition - 1);
		}

		/// <summary>
		/// Overloaded method for SubStr() that receives starting position and length
		/// </summary>
		/// <param name="cExpression"> </param>
		/// <param name="nStartPosition"> </param>
		/// <param name="nLength"> </param>
		public static string SubStr(string cExpression, int nStartPosition, int nLength)
		{
			nStartPosition--;
			if ((nLength + nStartPosition) > cExpression.Length)
				return cExpression.Substring(nStartPosition);
			else
				return cExpression.Substring(nStartPosition, nLength);
		}

		/// <summary>
		/// Removes leading and trailing spaces from cExpression
		/// </summary>
		/// <param name="cExpression"> </param>
		public static string Trim(string cExpression)
		{
			return cExpression.Trim();
		}


		/// <summary>
		/// Receives a string as a parameter and converts it to uppercase
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Upper("MyName");	//returns "MYNAME"
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		public static string Upper(string cExpression)
		{
			//Call the ToUpper() method of the string object
			return cExpression.ToUpper();
		}


		/// <summary>
		/// Receives a string and converts it to an integer
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.Val("1325");	//returns 1325
		/// </pre>
		/// </summary>
		/// <param name="cExpression"> </param>
		public static int Val(string cExpression)
		{
			//Remove all the spaces and commas from the string
			//Get the integer portion of the string
			return Int32.Parse(cExpression, NumberStyles.Any);
		}


		public static int Val(string cExpression, bool lBlankAsZero)
		{
			cExpression = cExpression.Trim();

			if (lBlankAsZero == true && string.IsNullOrEmpty(cExpression.Trim()))
			{
				cExpression = "0";
			}

			//Remove all the spaces and commas from the string
			//Get the integer portion of the string
			return Int32.Parse(cExpression, NumberStyles.Any);
		}


		public static int Val(string cExpression, bool lBlankAsZero, bool lStripFromLetters)
		{
			cExpression = cExpression.Trim();

			if (lBlankAsZero == true && string.IsNullOrEmpty(cExpression.Trim()))
			{
				cExpression = "0";
			}

			if (lStripFromLetters == true)
			{
				int nLen = cExpression.Trim().Length;

				string cOut = "";

				for (int i = 0; i < nLen; i++)
				{
					string cToken = cExpression.Substring(i, 1);

					if (StringLib.IsDigit(cToken))
					{
						cOut += cToken;
					}
				}

				cExpression = cOut;
			}

			//Remove all the spaces and commas from the string
			//Get the integer portion of the string
			return Int32.Parse(cExpression, NumberStyles.Any);
		}

		/// <summary>
		/// Receives a string and converts it to an integer
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.AtLine("Is", "Is Life Beautiful? \r\n It sure is");	//returns 1
		/// </pre>
		/// </summary>
		/// <param name="tcSearchExpression"></param>
		/// <param name="tcExpressionSearched"></param>
		/// <returns></returns>
		public static int AtLine(string tcSearchExpression, string tcExpressionSearched)
		{
			string lcString;
			int nPosition;
			int nCount = 0;

			try
			{
				nPosition = StringLib.At(tcSearchExpression, tcExpressionSearched);
				if (nPosition > 0)
				{
					lcString = StringLib.SubStr(tcExpressionSearched, 1, nPosition - 1);
					nCount = StringLib.Occurs(@"\r", lcString) + 1;
				}
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				nCount = 0;
			}
#pragma warning restore CA1031 // Do not catch general exception types

			return nCount;
		}

		/// <summary>
		/// Receives a search expression and string to search as parameters and returns an integer specifying
		/// the line where it was found. This function starts it search from the end of the string.
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.RAtLine("sure", "Is Life Beautiful? \r\n It sure is") 'returns 2
		/// </pre>
		/// </summary>
		/// <param name="tcSearchExpression"></param>
		/// <param name="tcExpressionSearched"></param>
		/// <returns></returns>
		public static int RAtLine(string tcSearchExpression, string tcExpressionSearched)
		{
			string lcString;
			int nPosition;
			int nCount = 0;

			try
			{
				nPosition = StringLib.RAt(tcSearchExpression, tcExpressionSearched);
				if (nPosition > 0)
				{
					lcString = StringLib.SubStr(tcExpressionSearched, 1, nPosition - 1);
					nCount = StringLib.Occurs(@"\r", lcString) + 1;
				}
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				nCount = 0;
			}
#pragma warning restore CA1031 // Do not catch general exception types

			return nCount;
		}

		/// <summary>
		/// Returns the line number of the first occurence of a string expression within 
		/// another string expression without regard to case (upper or lower)
		/// <pre>
		/// Example:
		/// VFPToolkit.StringLib.AtCLine("Is Life Beautiful? \r\n It sure is", "Is");	//returns 1
		/// </pre>
		/// </summary>
		/// <param name="tcSearchExpression"></param>
		/// <param name="tcExpressionSearched"></param>
		/// <returns></returns>
		public static int AtCLine(string tcSearchExpression, string tcExpressionSearched)
		{
			return AtLine(tcSearchExpression.ToLower(), tcExpressionSearched.ToLower());
		}

		/// <summary>
		/// Receives a string as a parameter and returns a bool indicating if the left most
		/// character in the string is a valid digit.
		/// <pre>
		/// Example:
		/// if(VFPToolkit.StringLib.IsDigit("1Kamal")){...}	//returns true
		/// </pre>
		/// </summary>
		/// <param name="tcExpression"></param>
		/// <returns></returns>
		public static bool IsDigit(string tcString)
		{
			//get the first character in the string
			try
			{
				char c = tcString[0];
				return Char.IsDigit(c);
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				return false;
			}
#pragma warning restore CA1031 // Do not catch general exception types
		}

		/// <summary>
		/// Returns the number of lines in a string
		/// <pre>
		/// Example:
		/// int lnLines = VFPToolkit.StringLib.MemLines(lcMyLongString);
		/// </pre>
		/// </summary>
		/// <param name="tcString"></param>
		/// <returns></returns>
		public static int MemLines(string tcString)
		{
			if (string.IsNullOrEmpty(tcString.Trim()))
				return 0;
			else
				return StringLib.Occurs("\\r", tcString) + 1;
		}

		/// <summary>
		/// Receives a string and a line number as parameters and returns the
		/// specified line in that string
		/// <pre>
		/// Example:
		/// string lcCity = VFPToolkit.StringLib.MLine(tcAddress, 2); // Not that you would want to do something like this but you could ;)
		/// </pre>
		/// </summary>
		/// <param name="tcString"></param>
		/// <param name="tnLineNo"></param>
		/// <returns></returns>
		public static string MLine(string tcString, int tnLineNo)
		{
			string[] aLines = tcString.Split('\r');
			string lcRetVal = "";
			try
			{
				lcRetVal = aLines[tnLineNo - 1];
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				//Ignore the exception as MLINE always returns a value
			}
#pragma warning restore CA1031 // Do not catch general exception types

			return lcRetVal;
		}

		//  From here ClassMate string functions [19.9.2002]

		/// <summary>
		/// Returns trimmed upper string
		/// </summary>
		/// <param name="cText"></param>
		/// <returns></returns>
		public static string UpperTrim(string cText)
		{
			return StringLib.Upper(StringLib.AllTrim(cText));
		}

		/// <summary>
		/// This function accepts a full file name, including path, and returns the drive and directory portion.
		/// The return value is in the format: C:\Directory\
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns>This function returns cFile minus the file name and extension.  
		/// If an error is encountered, NULL_STRING is returned.</returns>
		public static string CmParseFilePath(string cFile)
		{
			return StringLib.CmParseDrive(cFile) + StringLib.CmParsePath(cFile);
		}


		/// <summary>
		/// 	  This function accepts a full file name, including path, and returns the drive name.
		///   PARAMETERS:
		/// 	  cFile - The full file name.
		///   RETURN VALUES:
		/// 	  This function returns the drive where the file is located.  If an error is encountered, NULL_STRING is returned.
		///   REMARKS:
		/// 	  The return value is in the format:  C:
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns></returns>
		public static string CmParseDrive(string cFile)
		{
			int nLoc;
			string cDrive;

			cFile = StringLib.AllTrim(cFile);

			nLoc = StringLib.At(":", cFile);
			cDrive = StringLib.Upper(StringLib.Left(cFile, nLoc));

			if (StringLib.Len(cDrive) != 2 || !StringLib.IsAlpha(cDrive))
			{
				cDrive = "";
			}
			return cDrive;
		}

		/// <summary>
		/// 	 This function accepts a full file name, including path, and returns the directory (path) portion.
		/// 
		///  PARAMETERS:
		/// 	 cFile - The full file name.
		/// 
		///  RETURN VALUES:
		/// 	 This function returns the directory where the file is located.  If an error is encountered, NULL_STRING is returned.
		/// 
		///  REMARKS:
		/// 	 The return value is in the format:	 \Directory\SubDir\
		/// 
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns></returns>
		public static string CmParsePath(string cFile)
		{
			string cPath;
			int nLoc1;
			int nLoc2;

			cFile = StringLib.AllTrim(cFile);
			cPath = "";

			nLoc1 = StringLib.RAt(":", cFile);
			nLoc2 = StringLib.RAt("\\", cFile);

			if (nLoc2 > nLoc1)
			{
				cPath = StringLib.Upper(StringLib.SubStr(cFile, nLoc1 + 1, nLoc2 - nLoc1));
			}
			return cPath;
		}


		/// <summary>
		/// 		 This function accepts a full file name, including path, and returns the full file name without the
		/// 		 file extension.
		/// 
		/// PARAMETERS:
		/// 	 cFile - The full file name.
		/// 
		/// RETURN VALUES:
		/// 		 This function returns cFile minus the file extension.  If an error is encountered, NULL_STRING is
		/// 		 returned.
		/// 
		/// REMARKS:
		/// 	 The return value is in the format:
		/// 
		/// 		 C:\Directory\File
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns></returns>
		public static string CmParseAllWoExt(string cFile)
		{
			return StringLib.CmParseDrive(cFile) + StringLib.CmParsePath(cFile) +
					StringLib.CmParseWoExt(cFile);
		}


		/// <summary>
		/// 	This function accepts a full file name, including the path, and returns the file extension.
		/// 
		/// PARAMETERS:
		/// 	cFile - The full file name.
		/// 
		/// RETURN VALUES:
		/// 	This function returns the file extension of the file specified by cFile.  If an error is encountered, NULL_STRING is returned.
		/// 
		/// REMARKS:
		/// 	The return value is in the format:
		/// 
		/// 		.DBF
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns></returns>
		public static string CmParseExt(string cFile)
		{
			int nLoc1;
			int nLoc2;
			string cExt;

			cFile = StringLib.AllTrim(cFile);
			cExt = "";
			nLoc1 = StringLib.RAt(".", cFile);
			nLoc2 = StringLib.RAt("\\", cFile);

			if (nLoc1 > nLoc2)
			{
				cExt = StringLib.Upper(StringLib.SubStr(cFile, nLoc1, StringLib.Len(cFile) - nLoc1 + 1));
			}

			return cExt;
		}



		/// <summary>
		/// 	 This function accepts a full file name, including path, and returns the file name and extension portion.
		/// 
		/// PARAMETERS:
		/// 	 cFile - The full file name.
		/// 
		/// RETURN VALUES:
		/// 	 This function returns the file name and extension.  If an error is encountered, NULL_STRING is returned.
		/// 
		/// REMARKS:
		/// 	 The return value is in the format:
		/// 
		/// 		 File.Ext
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns></returns>
		public static string CmParseName(string cFile)
		{
			return StringLib.CmParseWoExt(cFile) + StringLib.CmParseExt(cFile);
		}



		/// <summary>
		///   This function accepts a full file name, including path, and returns the file name without the extension.
		/// 
		/// PARAMETERS:
		///   cFile - The full file name.
		/// 
		/// RETURN VALUES:
		///   This function returns the file name without the extension.  If an error is encountered, NULL_STRING is returned.
		/// 
		/// REMARKS:
		///   The return value is in the format:
		/// 
		/// 	  FileName
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns></returns>
		public static string CmParseWoExt(string cFile)
		{
			string cName;
			int nLoc1;
			int nLoc2;

			cFile = StringLib.AllTrim(cFile);
			cName = "";

			nLoc1 = StringLib.RAt("\\", cFile);         // find last directory mark
			nLoc2 = StringLib.RAt(":", cFile);          // find drive mark

			nLoc1 = System.Math.Max(nLoc1, nLoc2);          // take the right most position
			nLoc2 = StringLib.RAt(".", cFile);          // find ext sep


			// nLoc2		=iif(nLoc2 == 0, UH.MainLib_MS.StringLib.Len(cFile) + 1, nLoc2)
			nLoc2 = nLoc2 == 0 ? StringLib.Len(cFile) + 1 : nLoc2;

			//MyValue = IIF(x=y, .T., .F.) now becomes																  
			//MyValue = x == y ? True : False

			if (nLoc2 > nLoc1)
			{
				// cName	= StringLib.Upper(StringLib.SubStr(cFile, nLoc1 + 1, nLoc2 - nLoc1 - 1));
				cName = StringLib.SubStr(cFile, nLoc1 + 1, nLoc2 - nLoc1 - 1);
			}

			return cName;
		}

		public static string CmJustPath(string cPath)
		{
			//Get the full path of this path	
			string lcPath = cPath.Trim();
			//If the file contains a backslash then remove it and return the path onlyfile name get rid of it	
			if (lcPath.IndexOf('\\') == -1)
				return "";
			else
				return lcPath.Substring(0, lcPath.LastIndexOf('\\'));
		}

		/// <summary>
		/// Zählt die durch sep getrennten Tokens
		/// Ist kein Separator vorhanden, wird -1 zurückgegeben
		/// </summary>
		/// <param name="str">zu untersuchender String</param>
		/// <param name="sep">Separator</param>
		/// <returns></returns>
		public static int TokCount(string str, string sep)
		{
			string[] res;

			char[] charSep = sep.ToCharArray();

			res = str.Split(charSep);

			int nRet = res.Length;

			return nRet;
		}

		/// <summary>
		/// Gibt den durch sep getrennten Token an der Position tokpos (1 basiert !!) zurück
		/// Ist kein Wert zu ermittlen, wird ? zurückgegeben
		/// </summary>
		/// <param name="str">zu untersuchender String</param>
		/// <param name="sep">Separator</param>
		/// <param name="oneBasedTokpos">entsprechendes Token oder ?</param>
		/// <returns></returns>
		public static string Token(string str, string sep, int oneBasedTokpos)
		{
			string cRet;

			string[] res;

			char[] charSep = sep.ToCharArray();

			res = str.Split(charSep);

			try
			{
				cRet = res[oneBasedTokpos - 1];
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				cRet = "?";
			}
#pragma warning restore CA1031 // Do not catch general exception types

			return cRet;
		}

        #region KillWhere - entfernt WHERE Clause aus Where Bedingung

        /// <summary>
        /// KillWhere - entfernt WHERE Clause aus Where Bedingung als Vorbereitung auf das komplette
        /// Entfernen allen SQL spezifischen Codes aus der Applikation
        /// </summary>
        /// <param name="cWhere"></param>
        /// <returns></returns>
        public static string KillWhere(string cWhere)
        {
            string cRetval = StrTran(cWhere, "WHERE ", "");

			cRetval = StrTran(cRetval, "[[", "[");

			cRetval = StrTran(cRetval, "]]", "]");

            return cRetval;

        } // -- end of method GenBool

        #endregion


	} // end of class	

} // end of namespace

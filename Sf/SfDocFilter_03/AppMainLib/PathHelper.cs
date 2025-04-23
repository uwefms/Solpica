using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMainLib
{
	public class PathHelper
	{
        public static string GetFullPath(string relativePath)
        {
            // Get the base directory (the directory where the application is running)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Combine the base directory with the relative path
            string combinedPath = Path.Combine(baseDirectory, relativePath);

            // Convert the combined path to a full path
            string fullPath = Path.GetFullPath(combinedPath);

            return fullPath;
        }


		public static bool IsFullPath(string cDir)
		{
			bool lRetVal = false;

			if (!string.IsNullOrEmpty(cDir.Trim()) && cDir.Length > 2)
			{
				if (cDir.Contains(@":\"))
				{
					lRetVal = true;
				}
			}

			return lRetVal;

		} // end

		/// <summary>
		/// This function accepts a full file name, including path, and returns the drive and directory portion.
		/// The return value is in the format: C:\Directory\
		/// </summary>
		/// <param name="cFile"></param>
		/// <returns>This function returns cFile minus the file name and extension.  
		/// If an error is encountered, NULL_STRING is returned.</returns>
		public static string CmParseFilePath(string cFile)
		{
			return CmParseDrive(cFile) + CmParsePath(cFile);
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

			cFile = cFile.Trim();

			// nLoc = StringLib.At(":", cFile);
			nLoc = cFile.IndexOf(":");


		//public static int At(string cSearchFor, string cSearchIn)
		//{
		//	return cSearchIn.IndexOf(cSearchFor) + 1;
		//}

			cDrive = (StringLib.Left(cFile, nLoc)).ToUpper();

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

			cFile = cFile.Trim();

			cPath = "";

			nLoc1 = StringLib.RAt(":", cFile);
			nLoc2 = StringLib.RAt("\\", cFile);

			if (nLoc2 > nLoc1)
			{
				// cPath = StringLib.Upper(StringLib.SubStr(cFile, nLoc1 + 1, nLoc2 - nLoc1));
				cPath = (StringLib.SubStr(cFile, nLoc1 + 1, nLoc2 - nLoc1)).ToUpper();
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
			return CmParseDrive(cFile) + CmParsePath(cFile) + CmParseWoExt(cFile);
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

			cFile = cFile.Trim();
			cExt = "";
			nLoc1 = StringLib.RAt(".", cFile);
			nLoc2 = StringLib.RAt("\\", cFile);

			if (nLoc1 > nLoc2)
			{
				cExt = (StringLib.SubStr(cFile, nLoc1, StringLib.Len(cFile) - nLoc1 + 1)).ToUpper();
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
			//return StringLib.CmParseWoExt(cFile) + StringLib.CmParseExt(cFile);
			return CmParseWoExt(cFile) + CmParseExt(cFile);
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

			cFile = cFile.Trim();
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

		public static string GetLastDirectoryName(string path)
		{
			return Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar));
		}



	}
}

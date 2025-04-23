using System.Security.Cryptography;
using System.Text;

namespace AppMainLib
{
	public static class Hash
	{
		public static string CreateHash(string strSource)
		{			
			if (!string.IsNullOrEmpty(strSource))
			{
				UnicodeEncoding uEncode = new UnicodeEncoding();

				byte[] bytSource = uEncode.GetBytes(strSource.Trim());

				// SHA1Managed SHA1 = new SHA1Managed();
				var SHA1 = System.Security.Cryptography.SHA1.Create();

				byte[] bytHash = SHA1.ComputeHash(bytSource);
				return Convert.ToBase64String(bytHash);
			}
			else
			{
				// return null;
				return "";
			}

		} // end

	} // end of class

} // end of namespace


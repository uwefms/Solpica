
using System.Data;
using System.Text;

using AppMainLib;

using Dapper;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using SfDocFilter.Dal;

namespace SfDocFilter.DalSql
{
	public class OnBaseDal : IOnBaseDal
	{
		private readonly IConfiguration? configuration;

		public OnBaseDal(IConfiguration? config)
		{
		    this.configuration = config;
		}

		public IDbConnection Connection
		{
			get
			{
				return new SqlConnection(configuration!.GetConnectionString("SqlContext"));
			}
		}

		public async Task<List<OnBaseDto>> GetOnBaseListAsync(string? cFilter)
		{
			IEnumerable<OnBaseDto> oRet;

			#region Explanation why using IEnumerable vs List

			/*

			Using IEnumerable instead of List directly in Dapper can be beneficial in several scenarios:
			1.	Deferred Execution:
			•	IEnumerable supports deferred execution, meaning the query is not executed until you start iterating over the collection. This can be useful if you want to delay the execution of the query until the data is actually needed.
			2.	Memory Efficiency:
			•	When working with large datasets, using IEnumerable can be more memory-efficient. It allows you to process the data in a streaming fashion, rather than loading the entire dataset into memory at once.
			3.	Performance:
			•	If you only need to iterate over the data once or perform a single operation, using IEnumerable can be more performant as it avoids the overhead of creating a List.
			4.	Flexibility:
			•	Declaring the result as IEnumerable provides more flexibility. You can choose to convert it to a List or any other collection type later, depending on your needs.
			5.	Readability and Maintainability:
			•	Using IEnumerable can make your code more readable and maintainable by clearly indicating that the data is being retrieved in a deferred manner.

			*/

			#endregion Explanation why using IEnumerable va List

			#region Select statement

			StringBuilder sb = new StringBuilder();

			sb.Append(" SELECT ");

			sb.Append(" * ");

			#region Schüler Daten

			//sb.Append("	 [Kis_Id]");

			//sb.Append("	,[SchuelerGuid]");
			//sb.Append("	,[Sch_FullName]");
			//sb.Append("	,[Geburtsdatum]");
			//sb.Append("	,[Alter]");
			//sb.Append("	,[AnmeldeStatus]");

			#endregion Schüler Daten

			#region KIS Vorschlag

			//sb.Append("	,[VorschlagSchulstufe]");
			//sb.Append("	,[VorschlagSchulArt]");
			//sb.Append("	,[VorschlagSchulForm]");
			//sb.Append("	,[VorschlagSchulName]");
			//sb.Append("	,[VorschlagSchulId]");

			//sb.Append("	,[VorschlagSchulEmail]");

			#endregion KIS Vorschlag

			#region Schuldaten bereits angemeldet

			//sb.Append("	,[AnmeldeSchulNr]");
			//sb.Append("	,[AnmeldeSchulName]");
			//sb.Append("	,[AnmeldeSchulId]");
			//sb.Append("	,[AnmeldeSchulOrt]");
			//sb.Append("	,[AnmeldeSchulPlz]");
			//sb.Append("	,[AnmeldeSchulForm]");
			//sb.Append("	,[AnmeldeSchulArt]");
			//sb.Append("	,[AnmeldeSchulBem]");

			//sb.Append("	,[AufnahmeZum]");
			//sb.Append("	,[AufnahmeZumTxt]");
			//sb.Append("	,[Quartal_Schuleintritt]");
			//sb.Append("	,[AufnahmeJahr]");

			#endregion Schuldaten bereits angemeldet

			sb.Append("	FROM [TestData] ");

			if (!string.IsNullOrEmpty(cFilter))
			{
				sb.Append(" WHERE " + cFilter );
			}

			sb.Append("	ORDER BY [Last_name], [First_name]");

			#endregion Select statement

			using (IDbConnection conn = Connection)
			{
				// var p = new DynamicParameters();								
				// p.Add("AktSchulId", AktSchulId, dbType: DbType.Int32, direction: ParameterDirection.Input);
				
				try
				{					
					oRet = await conn.QueryAsync<OnBaseDto>(sb.ToString(), null, commandType: CommandType.Text);
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
					throw;
				} 				
  		    }
   
  		    return oRet.AsList();		
			
		}
	}
}

using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Microsoft.Extensions.Configuration;

using Dapper;

using SfDocFilter.Dal;
using AppMainLib;

namespace SfDocFilter.DalSqlite
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
				var connection = new SQLiteConnection(configuration!.GetConnectionString("SqlContextSqlite"));
				connection.Open();
				return connection;
			}
		}


		public async Task<List<OnBaseDto>> GetOnBaseListAsync(string? cFilter)
		{
			IEnumerable<OnBaseDto> oRet;
					
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


			throw new NotImplementedException();
		}
	}
}

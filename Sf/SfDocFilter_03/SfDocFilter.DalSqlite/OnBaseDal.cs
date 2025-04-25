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

public async Task<List<OnBaseDto>> GetOnBaseListAsync(FilterDto? oFilter)
		{
			IEnumerable<OnBaseDto> oRet;

			#region Select statement

			StringBuilder sb = new StringBuilder();

			sb.Append(" SELECT ");					
			sb.Append("  [TestDataId]      ");
			sb.Append(" ,[first_name]	     ");
			sb.Append(" ,[last_name]	     ");
			sb.Append(" ,[full_name]	     ");
			sb.Append(" ,[birth_date]	     ");
			sb.Append(" ,[gender]		     ");
			sb.Append(" ,[nationality]	     ");
			sb.Append(" ,[occupation]	     ");
			sb.Append(" ,[marital_status]  ");
			sb.Append(" ,[street_address]  ");
			sb.Append(" ,[city]			 ");
			sb.Append(" ,[state]			");
			sb.Append(" ,[country]			");
			sb.Append(" ,[postal_code]		");
			sb.Append(" ,[phone_number]	");
			sb.Append(" ,[email]			");
			sb.Append(" ,[first_name1]		");
			sb.Append(" ,[last_name1]		");
			sb.Append(" ,[document_number]	 ");
			sb.Append(" ,[document_type]	 ");
			sb.Append(" ,[issue_date]	   ");
			sb.Append(" ,[expiry_date]	   ");
			sb.Append("	FROM [TestData] ");

			var p = new DynamicParameters();

			// Add WHERE clause if oFilter is not null
			if (oFilter != null)
			{
			    List<string> conditions = new List<string>();

			    if (!string.IsNullOrEmpty(oFilter.First_name))
				{ 
			        conditions.Add("[first_name] LIKE @First_name");
					p.Add("First_name", oFilter.First_name, dbType: DbType.String, direction: ParameterDirection.Input, 50);
				}

			    if (!string.IsNullOrEmpty(oFilter.Last_name))
				{ 					
			        conditions.Add("[last_name] LIKE @Last_name");
					p.Add("Last_name" , oFilter.Last_name ,dbType: DbType.String ,direction: ParameterDirection.Input	,50);
				}

			    if (!string.IsNullOrEmpty(oFilter.Full_name))
				{ 
			        conditions.Add("[full_name] LIKE @Full_name");
					p.Add("Full_name" , oFilter.Full_name ,dbType: DbType.String ,direction: ParameterDirection.Input	,100);
				}

			    if (oFilter.Birth_date != default)
				{ 
			        conditions.Add("[birth_date] = @Birth_date");
					p.Add("Birth_date" , oFilter.Birth_date ,dbType: DbType.DateTime ,direction: ParameterDirection.Input	);
				}

			    if (!string.IsNullOrEmpty(oFilter.Gender))
				{ 
			        conditions.Add("[gender] = @Gender");
					p.Add("Gender" , oFilter.Gender ,dbType: DbType.String ,direction: ParameterDirection.Input	,10);
				}

			    if (!string.IsNullOrEmpty(oFilter.Nationality))
				{
					conditions.Add("[nationality] LIKE @Nationality");
					p.Add("Nationality" , oFilter.Nationality ,dbType: DbType.String ,direction: ParameterDirection.Input	,100);
				}

			    if (!string.IsNullOrEmpty(oFilter.Occupation))
				{
					conditions.Add("[occupation] LIKE @Occupation");
					p.Add("Occupation" , oFilter.Occupation ,dbType: DbType.String ,direction: ParameterDirection.Input	,100);
				}

			    if (!string.IsNullOrEmpty(oFilter.Marital_status))
				{
					conditions.Add("[marital_status] = @Marital_status");
					p.Add("Marital_status" , oFilter.Marital_status ,dbType: DbType.String ,direction: ParameterDirection.Input	,20);
				}

				if (!string.IsNullOrEmpty(oFilter.City))
				{ 
			        conditions.Add("[city] LIKE @City");
					p.Add("City" , oFilter.City ,dbType: DbType.String ,direction: ParameterDirection.Input	,50);
				}

				if (!string.IsNullOrEmpty(oFilter.State))
				{
					conditions.Add("[state] LIKE @State");
					p.Add("State" , oFilter.State ,dbType: DbType.String ,direction: ParameterDirection.Input	,50);
				}

				if (!string.IsNullOrEmpty(oFilter.Country))
				{
					conditions.Add("[country] LIKE @Country");
					p.Add("Country" , oFilter.Country ,dbType: DbType.String ,direction: ParameterDirection.Input	,100);
				}
				
				if (!string.IsNullOrEmpty(oFilter.Postal_code))
				{
					conditions.Add("[postal_code] LIKE @Postal_code");
					p.Add("Postal_code" , oFilter.Postal_code ,dbType: DbType.String ,direction: ParameterDirection.Input	,20);
				}

				if (!string.IsNullOrEmpty(oFilter.Email))
				{
					conditions.Add("[email] LIKE @Email");
					p.Add("Email" , oFilter.Email ,dbType: DbType.String ,direction: ParameterDirection.Input	,100);
				}

			    if (!string.IsNullOrEmpty(oFilter.First_name1))
				{ 
			        conditions.Add("[first_name1] LIKE @First_name1");
					p.Add("First_name1", oFilter.First_name1, dbType: DbType.String, direction: ParameterDirection.Input, 50);
				}

			    if (!string.IsNullOrEmpty(oFilter.Last_name1))
				{ 					
			        conditions.Add("[last_name1] LIKE @Last_name1");
					p.Add("Last_name1" , oFilter.Last_name1 ,dbType: DbType.String ,direction: ParameterDirection.Input	,50);
				}

				if (!string.IsNullOrEmpty(oFilter.Document_type))
				{
					conditions.Add("[document_type] LIKE @Document_type");
					p.Add("Document_type" , oFilter.Document_type ,dbType: DbType.String ,direction: ParameterDirection.Input	,50);
				}

				if (oFilter.Issue_date != default)
				{
					conditions.Add("[issue_date] = @Issue_date");
					p.Add("Issue_date" , oFilter.Issue_date ,dbType: DbType.DateTime ,direction: ParameterDirection.Input	);
				}

				if (oFilter.Expiry_date != default)
				{
					conditions.Add("[expiry_date] = @Expiry_date");
					p.Add("Expiry_date" , oFilter.Expiry_date ,dbType: DbType.DateTime ,direction: ParameterDirection.Input	);
				}

				if (conditions.Any())
			    {
			        sb.Append(" WHERE ");
			        sb.Append(string.Join(" AND ", conditions));
			    }
			}

			sb.Append("	ORDER BY [Last_name], [First_name]");
			
			// var test = sb.ToString();

			#endregion Select statement

			using (IDbConnection conn = Connection)
			{				
				try
				{					
					oRet = await conn.QueryAsync<OnBaseDto>(sb.ToString(), p, commandType: CommandType.Text);
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

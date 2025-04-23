using AppMainLib;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Text;

namespace AppDataLibAdo
{
    /// <summary>
    /// Datenzugriffslayer - neue Version
    /// </summary>
    [Serializable]	
	public class AdoDal : IDisposable
	{
		#region Felder

		[NonSerialized]
		private DbConnection? _Connection;

		[NonSerialized]
		private string? _dbConnectionString;

		[NonSerialized]
		private string? _dbProviderName;

		private bool? _disposedValue;

		[NonSerialized]
		private DbParameterCollection? _ParaCollection;

		#endregion

		#region Konstruktor
       		
		private AdoDal()
		{
		}

        public static AdoDal CreateDal(string connectionString, DBProviders provider)
		{
			string providerName = "";
			switch (provider)
			{
				case DBProviders.Sql:
					providerName = "System.Data.SQLClient";
					break;

				case DBProviders.OleDB:
					providerName = "System.Data.OleDb";
					break;

				case DBProviders.Odbc:
					providerName = "System.Data.Odbc";
					break;

				case DBProviders.SQLite:
					providerName = "System.Data.SQLite";
					break;

			}

			return CreateDal(connectionString, providerName);

		} // end  
        		
		public static AdoDal CreateDal(IConfiguration AllConfig, string configSectionName)
		{		
			return new AdoDal
			{
				MyConnectionString = AllConfig.GetSection("ConnectionStrings").GetSection(configSectionName).Value!,
				MyProviderName = AllConfig.GetSection("ConnectionStrings").GetSection("ProviderName").Value!
			};

		} // end
        		
		public static AdoDal CreateDal(IConfiguration AllConfig)
		{		
			return new AdoDal
			{
				MyConnectionString = AllConfig.GetSection("ConnectionStrings").GetSection("SqlContext").Value!,
				MyProviderName = AllConfig.GetSection("ConnectionStrings").GetSection("ProviderName").Value!
			};
		} // end
        		
		public static AdoDal CreateDal(string connectionString, string providerName)
		{		
			return new AdoDal { MyConnectionString = connectionString, MyProviderName = providerName };

		} // end

		#endregion

		#region Properties

		private const int nTimeOut = 600;

		public static int AppTimeOut
		{
			get
			{
				return nTimeOut;
			}
		}

		private DbConnection MyConnection
		{
			get
			{
				return CreateDBConnection();
			}
		}

		public DbConnection DalConnection
		{
			get
			{
				DbConnection connection = CreateDBConnection();

				try
				{
					connection.OpenAsync();
				}
				catch (System.Exception ex)
				{					
					System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
					
					throw;
				}

				return connection;
			}
		}

		private string? MyConnectionString
		{
			get
			{
				return this._dbConnectionString;
			}
			set
			{
				this._dbConnectionString = value;
			}
		}

		private string? MyProviderName
		{
			get
			{
				return this._dbProviderName;
			}
			set
			{
				this._dbProviderName = value;
			}
		}

		public DbParameterCollection? Parameters
		{
			get
			{
				return this._ParaCollection;
			}
		}

		#endregion

        #region CreateDBCommand

		private async Task<DbCommand> CreateDBCommandAsync(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command;

			try
			{
				DbCommand? myCommand = this.MyConnection.CreateCommand();

				myCommand.CommandType = cmdType;

				myCommand.CommandText = sqlCommand;

				myCommand.CommandTimeout = cmdTimeOut;

				myCommand.Parameters.Clear();

				if (dbParamList != null)
				{
					foreach (DbParameter parameter in dbParamList)
					{
						if ((this.MyProviderName!.ToUpper() != "SYSTEM.DATA.ORACLECLIENT") || (cmdType != CommandType.StoredProcedure))
						{
							parameter.ParameterName = string.Format(this.GetParameterFormat(), parameter.ParameterName);
						}

						myCommand.Parameters.Add(parameter);
					}
				}

				command = myCommand;
			}
			catch (Exception ex1)
			{
				System.Diagnostics.Debug.WriteLine(ex1.Message + " - " + Misc.GetInnerException(ex1));
				throw;
			}

			
			return await Task.FromResult(command);

		} // end

		private async Task<DbConnection> CreateDBConnectionAsync()
		{
			DbConnection? connection = null;

			try
			{
				connection = new SqlConnection(MyConnectionString)
				{					
					ConnectionString = MyConnectionString
				}; //could also be Sqlite etc

			}
			catch (Exception ex)
			{
				// Exception exception = ex;
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
				throw;
			}
            			
			return await Task.FromResult(connection);

		} // end

		private async Task<DbDataAdapter> CreateDBDataAdapterAsync()
		{
			DbDataAdapter? adapter = null;

			try
			{
				adapter = new SqlDataAdapter();
			}
			catch (Exception ex)
			{		
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
				throw;
			}
						
			return await Task.FromResult(adapter);

		} // end

		public DbCommand CreateDBCommand(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command;

			try
			{
				DbCommand myCommand = this.MyConnection.CreateCommand();

				myCommand.CommandType = cmdType;


				myCommand.CommandText = sqlCommand;

				myCommand.CommandTimeout = cmdTimeOut;

				myCommand.Parameters.Clear();

				if (dbParamList != null)
				{
					foreach (DbParameter parameter in dbParamList)
					{
						if ((this.MyProviderName!.ToUpper() != "SYSTEM.DATA.ORACLECLIENT") || (cmdType != CommandType.StoredProcedure))
						{
							parameter.ParameterName = string.Format(this.GetParameterFormat(), parameter.ParameterName);
						}

						myCommand.Parameters.Add(parameter);
					}
				}

				command = myCommand;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
				throw;
			}

			return command;

		} // end

        #endregion CreateDBCommand

		private DbConnection CreateDBConnection()
		{
			DbConnection? connection = null;

			try
			{
				connection = new SqlConnection(MyConnectionString)
				{					
					ConnectionString = MyConnectionString
				}; //could also be Sqlite etc

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
				
				throw;

			}

			return connection;

		} // end

        private DbDataAdapter CreateDBDataAdapter()
        {
            DbDataAdapter? adapter = null;

            try
            {
                adapter = new SqlDataAdapter();
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                throw;
            }

            return adapter;

        } // end

        #region CreateParameter mit Überladungen

        public static DbParameter CreateParameter()
		{
			DbParameter parameter;
			try
			{				
				parameter = new SqlParameter();
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;			
			}

			return parameter;

		} // end

		public DbParameter CreateParameter(string parameterName, DbType dbType, ParameterDirection direction)
		{
			return CreateParameter(parameterName, dbType, direction, 100, null!);
		}

		public DbParameter CreateParameter(string parameterName, DbType dbType, ParameterDirection direction, int size, object value)
		{
			DbParameter para;

			try
			{
				para = new SqlParameter
				{
					IsNullable = true,
					ParameterName = parameterName,
					DbType = dbType,
					Direction = direction,
					SourceColumnNullMapping = true,
					Size = size
				};

				if ((dbType == DbType.Date) | (dbType == DbType.DateTime))
				{
					para.Value = Misc.DateToDBNull(value);
				}
				else
				{
					para.Value = Misc.NullToDBNull(value);
				}

			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;

				// throw ex;
			}

			return para;

		} // end

		public DbParameter CreateParameter(string connectionString, string providerName, string parameterName, DbType dbType, ParameterDirection direction)
		{
			return CreateDal(connectionString, providerName).CreateParameter(parameterName, dbType, direction);
		}

		public DbParameter CreateParameter(string connectionString, string providerName, string parameterName, DbType dbType, ParameterDirection direction, int size, object value)
		{
			return CreateDal(connectionString, providerName).CreateParameter(parameterName, dbType, direction, size, value);
		}

		#endregion CreateParameter mit Überladungen

		#region ExecuteCommandAsync

		public async Task<int> ExecuteCommandAsync(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand command = null!;
			int num;
			try
			{
				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				command.Connection!.Open();
				num = await command.ExecuteNonQueryAsync();
			}
			catch (DbException ex)
			{
				DbException innerException = ex;
				DalException exception2 = new DalException("Error DB Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;

				// throw ex;
			}
			finally
			{
				command.Connection!.Close();
				command.Dispose();
			}

			return num;

		} // end

		public async Task<int> ExecuteCommandAsync(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return await ExecuteCommandAsync(sqlCommand, cmdType, 30, dbParamList);
		}

		//public static async Task<int> ExecuteCommandAsync(string configSectionName, string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		//{
		//	return await ExecuteCommandAsync(configSectionName, sqlCommand, cmdType, 30, dbParamList);
		//}

		//public static async Task<int> ExecuteCommandAsync(string configSectionName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		//{
		//	return await CreateDal(configSectionName).ExecuteCommandAsync(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		//}

		public static async Task<int> ExecuteCommandAsync(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbdbParamList)
		{
			return await ExecuteCommandAsync(connectionString, providerName, sqlCommand, cmdType, 30, dbdbParamList);
		}

		public static async Task<int> ExecuteCommandAsync(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return await CreateDal(connectionString, providerName).ExecuteCommandAsync(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		#endregion ExecuteCommandAsync

		#region ExecuteCommand

		public int ExecuteCommand(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand command = null!;
			int num;
			try
			{
				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				command.Connection!.Open();
				num = command.ExecuteNonQuery();
			}
			catch (DbException ex)
			{
				DbException innerException = ex;
				DalException exception2 = new DalException("Error DB Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;								
			}
			finally
			{
				command.Connection!.Close();
				command.Dispose();
			}

			return num;

		} // end

		public int ExecuteCommand(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return this.ExecuteCommand(sqlCommand, cmdType, 30, dbParamList);
		}

		public static int ExecuteCommand(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbdbParamList)
		{
			return ExecuteCommand(connectionString, providerName, sqlCommand, cmdType, 30, dbdbParamList);
		}

		public static int ExecuteCommand(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return CreateDal(connectionString, providerName).ExecuteCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		#endregion ExecuteCommand

		#region GetDataTableAsync / GetDataTableCollectionAsync

		public async Task<DataTable> GetDataTableAsync(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return await this.GetDataTableAsync(sqlCommand, cmdType, 30, dbParamList);
		}

		public async Task<DataTable> GetDataTableAsync(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			var result = await GetDataTableCollectionAsync(sqlCommand, cmdType, cmdTimeOut, dbParamList);

			return result[0];
		}

		public static async Task<DataTable> GetDataTableAsync(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return await GetDataTableAsync(connectionString, providerName, sqlCommand, cmdType, 30, dbParamList);
		}

		public static async Task<DataTable> GetDataTableAsync(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			// return await GetDataTableCollectionAsync(connectionString, providerName, sqlCommand, cmdType, cmdTimeOut, dbParamList)[0];
			var result = await GetDataTableCollectionAsync(connectionString, providerName, sqlCommand, cmdType, cmdTimeOut, dbParamList);

			return result[0];
		}

		public async Task<DataTableCollection> GetDataTableCollectionAsync(string sqlCommand, CommandType cmdType)
		{
			return await GetDataTableCollectionAsync(sqlCommand, cmdType, 30, null!);
		}

		public static async Task<DataTableCollection> GetDataTableCollectionAsync(string connectionString, string providerName, string sqlCommand, CommandType cmdType)
		{
			return await CreateDal(connectionString, providerName).GetDataTableCollectionAsync(sqlCommand, cmdType, 30, new DbParameter[0]);
		}

		public static async Task<DataTableCollection> GetDataTableCollectionAsync(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return await CreateDal(connectionString, providerName).GetDataTableCollectionAsync(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		public async Task<DataTableCollection> GetDataTableCollectionAsync(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;
			DataTableCollection tables;
			try
			{
				DataSet dataSet = new DataSet();
				command = await CreateDBCommandAsync(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				DbDataAdapter adapter = await CreateDBDataAdapterAsync();
				adapter.SelectCommand = command;
				adapter.Fill(dataSet);
				tables = dataSet.Tables;
			}
			catch (DbException exception1)
			{
				DbException innerException = exception1;
				DalException exception2 = new DalException("Error DB Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;

				// throw ex;
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

			return tables;

		} // end

		#endregion GetDataTableAsync / GetDataTableCollectionAsync

		#region GetDataTable / GetDataTableCollection

		public DataTable GetDataTable(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return this.GetDataTable(sqlCommand, cmdType, 30, dbParamList);
		}

		public DataTable GetDataTable(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return this.GetDataTableCollection(sqlCommand, cmdType, cmdTimeOut, dbParamList)[0];
		}

		public static DataTable GetDataTable(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return GetDataTable(connectionString, providerName, sqlCommand, cmdType, 30, dbParamList);
		}

		public static DataTable GetDataTable(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return GetDataTableCollection(connectionString, providerName, sqlCommand, cmdType, cmdTimeOut, dbParamList)[0];
		}

		public DataTableCollection GetDataTableCollection(string sqlCommand, CommandType cmdType)
		{
			return this.GetDataTableCollection(sqlCommand, cmdType, 30, null!);
		}

		public static DataTableCollection GetDataTableCollection(string connectionString, string providerName, string sqlCommand, CommandType cmdType)
		{
			return CreateDal(connectionString, providerName).GetDataTableCollection(sqlCommand, cmdType, 30, new DbParameter[0]);
		}

		public static DataTableCollection GetDataTableCollection(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return CreateDal(connectionString, providerName).GetDataTableCollection(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		public DataTableCollection GetDataTableCollection(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;
			DataTableCollection tables;
			try
			{
				DataSet dataSet = new DataSet();
				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				DbDataAdapter adapter = this.CreateDBDataAdapter();
				adapter.SelectCommand = command;
				adapter.Fill(dataSet);
				tables = dataSet.Tables;
			}
			catch (DbException exception1)
			{
				DbException innerException = exception1;
				DalException exception2 = new DalException("Error DB Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;

				// throw ex;
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

			return tables;

		} // end

		#endregion GetDataTable / GetDataTableCollection

		public static DataTable GetDataTableFromCsv(string fileName, string delimiter, string fieldBounds, bool firstRowHasNames)
		{
			DataTable table;

			StreamReader? reader = null;

			try
			{
				if (string.IsNullOrEmpty(fileName))
				{
					Exception exception = new Exception("No filename specified!");
					throw exception;
				}
				if (string.IsNullOrEmpty(delimiter))
				{
					Exception exception2 = new Exception("No field delimiter specified!");
					throw exception2;
				}
				if (!File.Exists(fileName))
				{
					Exception exception3 = new Exception("File not present or no permission to open file!");
					throw exception3;
				}

				DataTable table2 = new DataTable();
				
				reader = new StreamReader(fileName, Encoding.UTF8);

				bool flag = false;
				string oldValue = "";

				if (!string.IsNullOrEmpty(fieldBounds))
				{
					oldValue = fieldBounds + delimiter + fieldBounds;
				}

				while (!reader.EndOfStream)
				{
					string str2 = reader.ReadLine()!;

					if (!string.IsNullOrEmpty(fieldBounds))
					{
						str2 = str2.Replace(oldValue, delimiter).Trim(fieldBounds.ToCharArray());
					}

					string[] values = str2.Split(delimiter.ToCharArray());

					bool flag2 = true;
					if (!flag)
					{
						int num = 0;
						foreach (string str3 in values)
						{
							if (firstRowHasNames)
							{
								table2.Columns.Add(str3);
								flag2 = false;
							}
							else
							{
								table2.Columns.Add("Feld" + num.ToString());
								num++;
							}
							flag = true;
						}

					}

					try
					{
						if (flag2)
						{
							table2.Rows.Add(values);

							flag = true;
						}
						continue;
					}
					catch (Exception ex)
					{
						Exception exception = ex;
						throw;

						// throw ex;
					}
				}
				reader.Close();
				table = table2;
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;
			}
			finally
			{
				reader!.Close();
			}

			return table;

		} // end

		#region GetIdValuePairList

		public IdValuePairList GetIdValuePairList(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return this.GetIdValuePairList(sqlCommand, cmdType, 30, dbParamList);
		}

		public IdValuePairList GetIdValuePairList(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;
			IdValuePairList? list = null;

			try
			{
				list = new IdValuePairList();

				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				command.Connection!.Open();

				using (DbDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						// neue Variante für IdValuePairList mit int? anstatt int
						int? nValue = (reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0));

						list.Add(nValue, reader.GetString(1));
					}

					reader.Close();
				}

			}
			catch (DbException exception1)
			{
				DbException innerException = exception1;
				DalException exception2 = new DalException("Error DB Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;								
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

			return list;

		} // end

		public static IdValuePairList GetIdValuePairList(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return CreateDal(connectionString, providerName).GetIdValuePairList(sqlCommand, cmdType, 30, dbParamList);
		}

		public static IdValuePairList GetIdValuePairList(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return CreateDal(connectionString, providerName).GetIdValuePairList(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		#endregion GetIdValuePairList

		#region GetSchemaTable

		public DataTable GetSchemaTable()
		{
			DataTable table;

			try
			{
				using (DbConnection connection = this.MyConnection)
				{
					connection.Open();
					table = connection.GetSchema();
					connection.Close();
				}

			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;

				// throw ex;
			}

			return table;

		} // end

		public DataTable GetSchemaTable(string collectionName)
		{
			DataTable table;
			try
			{

				using (DbConnection connection = this.MyConnection)
				{
					connection.Open();
					table = connection.GetSchema(collectionName);
					connection.Close();
				}
			}
			catch (Exception ex)
			{				
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
				
				throw;
			}

			return table;

		} // end

		public static DataTable GetSchemaTable(string ConnectionString, string ProviderName)
		{
			return CreateDal(ConnectionString, ProviderName).GetSchemaTable();
		}

		public DataTable GetSchemaTable(string collectionName, string[] restrictionValues)
		{
			DataTable table;
			try
			{
				using (DbConnection connection = this.MyConnection)
				{
					connection.Open();
					table = connection.GetSchema(collectionName, restrictionValues);
					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;

				// throw ex;
			}

			return table;

		} // end
		public static DataTable GetSchemaTable(string ConnectionString, string ProviderName, string collectionName)
		{
			return CreateDal(ConnectionString, ProviderName).GetSchemaTable(collectionName);
		}

		public static DataTable GetSchemaTable(string ConnectionString, string ProviderName, string collectionName, string[] restrictionValues)
		{
			return CreateDal(ConnectionString, ProviderName).GetSchemaTable(collectionName, restrictionValues);
		}

		#endregion GetSchemaTable

		#region GetStringCol mit Überladungen

		public List<string> GetStringCol(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return this.GetStringCol(sqlCommand, cmdType, 30, dbParamList);
		}

		public List<string> GetStringCol(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;
			List<string> list;

			try
			{
				list = new List<string>();

				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				command.Connection!.Open();
				using (DbDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						list.Add(reader.GetString(0));
					}
					reader.Close();
				}
			}
			catch // (Exception ex)
			{
				// Exception exception = ex;
				throw;

				// throw ex;
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

			return list;

		} // end

		public static List<string> GetStringCol(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return GetStringCol(connectionString, providerName, sqlCommand, cmdType, 30, dbParamList);
		}

		public static List<string> GetStringCol(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return CreateDal(connectionString, providerName).GetStringCol(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		#endregion GetStringCol mit Überladungen

		#region GetStringPairList

		public StringPairList GetStringPairList(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return this.GetStringPairList(sqlCommand, cmdType, 30, dbParamList);
		}

		public StringPairList GetStringPairList(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;
			StringPairList list;
			try
			{
				list = new StringPairList();

				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				command.Connection!.Open();
				using (DbDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						list.Add(reader.GetString(0), reader.GetString(1));
					}
					reader.Close();
				}								
			}
			catch (DbException exception1)
			{
				
				DbException innerException = exception1;
				DalException exception2 = new DalException("Error DB-Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{
				// System.Diagnostics.Debug.WriteLine(ex.Message + " - " + ex.InnerException);
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));				
				throw;
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

			return list;

		} // end

		public static StringPairList GetStringPairList(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return CreateDal(connectionString, providerName).GetStringPairList(sqlCommand, cmdType, 30, dbParamList);
		}

		public static StringPairList GetStringPairList(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return CreateDal(connectionString, providerName).GetStringPairList(sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		#endregion GetStringPairList

		#region ExecScalarCommand

		/// <summary>
		/// Führt einen ExecScalar Befehl auf der Datenbank aus
		/// </summary>
		/// <param name="SQLString"></param>
		/// <param name="CommandType"></param>
		/// <param name="CmdTimeOut"></param>
		/// <param name="ParamList"></param>
		/// <returns></returns>
		public object ExecScalarCommand(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;

			try
			{
				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				command.Connection!.Open();
				object obj = command.ExecuteScalar()!;
				return obj;
			}
			catch (DbException ex)
			{
				// throw ex;
				// System.Diagnostics.Debug.WriteLine(ex.Message + " - " + ex.InnerException);
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));				
				throw;
			}
			catch (Exception ex)
			{
				// System.Diagnostics.Debug.WriteLine(ex.Message + " - " + ex.InnerException);
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));				
				// throw ex;
				throw;
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

		} // end

		public object ExecScalarCommand(string SQLString, CommandType CommandType, params DbParameter[] ParamList)
		{
			return ExecScalarCommand(SQLString, CommandType, 30, ParamList);
		}

		#endregion

		#region GetDtReader mit Überladungen

		public DataTableReader GetDtReader(string sqlCommand, CommandType cmdType)
		{
			return this.GetDtReader(sqlCommand, cmdType, 30, null!);
		}

		public DataTableReader GetDtReader(string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return this.GetDtReader(sqlCommand, cmdType, 30, dbParamList);
		}

		public static DataTableReader GetDtReader(string connectionString, string providerName, string sqlCommand, CommandType cmdType, params DbParameter[] dbParamList)
		{
			return GetDtReader(connectionString, providerName, sqlCommand, cmdType, 30, dbParamList);
		}

		public static DataTableReader GetDtReader(string connectionString, string providerName, string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			return GetDtReader(connectionString, providerName, sqlCommand, cmdType, cmdTimeOut, dbParamList);
		}

		public static DataTableReader GetDtReader(string connectionString, string providerName, string sqlCommand, CommandType cmdType)
		{
			return CreateDal(connectionString, providerName).GetDtReader(sqlCommand, cmdType, 30, new DbParameter[0]);
		}

		public DataTableReader GetDtReader(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;

			DataTableReader? DtRdr = null;

			try
			{
				DataSet dataSet = new DataSet();
				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				DbDataAdapter adapter = this.CreateDBDataAdapter();
				adapter.SelectCommand = command;
				adapter.Fill(dataSet);

				if (dataSet.Tables.Count > 0)
				{
					DtRdr = dataSet.CreateDataReader();
				}
				else
				{
					DtRdr = new DataTableReader(new DataTable());
				}

			}
			catch (DbException exception1)
			{
				DbException innerException = exception1;
				DalException exception2 = new DalException("Error DB Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{			
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
				throw;
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

			return DtRdr;

		} // end

		public async Task<DataTableReader> GetDtReaderAsync(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;

			DataTableReader? DtRdr = null;

			try
			{
				DataSet dataSet = new DataSet();
				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				DbDataAdapter adapter = this.CreateDBDataAdapter();
				adapter.SelectCommand = command;

				adapter.Fill(dataSet);
				
				if (dataSet.Tables.Count > 0)
				{
					DtRdr = dataSet.CreateDataReader();					
				}
				else
				{
					DtRdr = new DataTableReader(new DataTable());
				}

			}
			catch (DbException exception1)
			{
				DbException innerException = exception1;
				DalException exception2 = new DalException("Error DB Access!", innerException);
				throw exception2;
			}
			catch (Exception ex)
			{				
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));
				
				throw;
			}
			finally
			{
				command!.Connection!.Close();
				command.Dispose();
			}

			return await Task.FromResult(DtRdr);

		} // end

		#endregion GetDtReader mit Überladungen

		#region ExecSqlDataReader mit Überladungen

		/// <summary>
		/// Öffnet DataReader
		/// </summary>
		/// <param name="SQLString"></param>
		/// <param name="CommandType"></param>
		/// <param name="HoldConnection"></param>
		/// <param name="CmdTimeOut"></param>
		/// <param name="ParamList"></param>
		/// <returns></returns>		
		public SqlDataReader ExecSqlDataReader(string sqlCommand, CommandType cmdType, int cmdTimeOut, params DbParameter[] dbParamList)
		{
			if (this.MyProviderName!.ToUpper() == "SYSTEM.DATA.SQLCLIENT")
			{
				SqlCommand? command = null;

				try
				{
					command = (SqlCommand)this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
					command.Connection.Open();

					SqlDataReader I = command.ExecuteReader(CommandBehavior.CloseConnection);
					return I;
				}
				catch (DbException ex)
				{					
					System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));				
					throw;

				}
				catch (Exception ex)
				{					
					System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));				
				
					throw;
				}
			}
			else
			{
				DalException exception = new DalException("kein MS SQL Provider verwendet angegeben!");
				throw exception;
			}


		} // end

		public SqlDataReader ExecSqlDataReader(string SQLString, CommandType CommandType, params DbParameter[] ParamList)
		{
			return ExecSqlDataReader(SQLString, CommandType, 30, ParamList);
		}

		#endregion

		#region ExecIDataReader mit Überladungen

		/// <summary>
		/// Öffnet DataReader
		/// </summary>
		/// <param name="SQLString"></param>
		/// <param name="CommandType"></param>
		/// <param name="HoldConnection"></param>
		/// <param name="CmdTimeOut"></param>
		/// <param name="ParamList"></param>
		/// <returns></returns>
		public IDataReader ExecIDataReader(string sqlCommand, CommandType cmdType, int cmdTimeOut, CommandBehavior oBehavior, params DbParameter[] dbParamList)
		{
			DbCommand? command = null;

			try
			{
				command = this.CreateDBCommand(sqlCommand, cmdType, cmdTimeOut, dbParamList);
				command.Connection!.Open();

				// IDataReader I = command.ExecuteReader(CommandBehavior.CloseConnection);
				IDataReader I = command.ExecuteReader(oBehavior);
				return I;
			}
			catch (DbException ex)
			{
				// throw ex;
				// System.Diagnostics.Debug.WriteLine(ex.Message + " - " + ex.InnerException);
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));				
				throw;

			}
			catch (Exception ex)
			{
				// System.Diagnostics.Debug.WriteLine(ex.Message + " - " + ex.InnerException);
				System.Diagnostics.Debug.WriteLine(ex.Message + " - " + Misc.GetInnerException(ex));				
				// throw ex;
				throw;
			}

		} // end

		public IDataReader ExecIDataReader(string SQLString, CommandType CommandType, params DbParameter[] ParamList)
		{
			return ExecIDataReader(SQLString, CommandType, 10, CommandBehavior.CloseConnection, ParamList);
		}

		#endregion

		#region Helper Methods

		private string GetParameterFormat()
		{
			string? str;
			try
			{
				if (this.MyProviderName!.ToUpper() == "SYSTEM.DATA.SQLCLIENT")
				{
					return "@{0}";
				}

				str = Convert.ToString(this.GetSchemaTable(DbMetaDataCollectionNames.DataSourceInformation).Rows[0]["ParameterMarkerFormat"]);
			}
			catch (Exception ex)
			{
				Exception exception = ex;
				throw;
			}

			return str!;

		} // end

		#endregion Helper Methods

		public enum DBProviders
		{
			Sql,
			OleDB,
			Odbc,
			SQLite
		}

        #region Dispose

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (this._disposedValue == false)
			{
				if ((this._Connection != null) && (this._Connection.State == ConnectionState.Open))
				{
					this._Connection.Close();
				}
				if (disposing)
				{
					this._Connection = null;
					this._ParaCollection = null;
				}
			}

			this._disposedValue = true;

		} // end

		#endregion Dispose

	} // end of class


	public class DalException : Exception
	{
		public DalException()
		{
		}

		public DalException(string errorMessage) : base(errorMessage)
		{
		}

		public DalException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
		{
		}

	} // end of class

} // end of namspace

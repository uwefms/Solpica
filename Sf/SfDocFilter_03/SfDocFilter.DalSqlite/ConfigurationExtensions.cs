using Microsoft.Extensions.DependencyInjection;

using SfDocFilter.Dal;
using SfDocFilter.DalSqlite;

namespace SfDocFilter.Configuration
{
	public static class ConfigurationExtensions
	{
		public static void AddDalSqlite(this IServiceCollection services)
		{
			services.AddTransient<IOnBaseDal, OnBaseDal>();

			//services.AddTransient<IUserDal, UserDal>();
			//services.AddTransient<ILookupDal, LookupDal>();
			//services.AddTransient<IKisDal, KisDal>();
			//services.AddTransient<IMailService, MailService>();
						
			AppConst.Constants.DataModel = "Sqlite-Db";

		} // end

	}
}

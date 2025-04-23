using Microsoft.Extensions.DependencyInjection;

using SfDocFilter.Dal;
using SfDocFilter.DalSql;


namespace SfDocFilter.Configuration
{
	public static class ConfigurationExtensions
	{
		public static void AddDalSql(this IServiceCollection services)
		{
			services.AddTransient<IOnBaseDal, OnBaseDal>();

			//services.AddTransient<IUserDal, UserDal>();
			//services.AddTransient<ILookupDal, LookupDal>();
			//services.AddTransient<IKisDal, KisDal>();
			//services.AddTransient<IMailService, MailService>();
						
			AppConst.Constants.DataModel = "Sql-Db";

		} // end
	}
}

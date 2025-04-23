using Microsoft.Extensions.DependencyInjection;

using SfDocFilter.Dal;
using SfDocFilter.DalMock;


namespace SfDocFilter.Configuration
{
	public static class ConfigurationExtensions
	{
		public static void AddDalMock(this IServiceCollection services)
		{
			services.AddTransient<IOnBaseDal, OnBaseDal>();


			//services.AddTransient<IUserDal, UserDal>();
			//services.AddTransient<ILookupDal, LookupDal>();
			//services.AddTransient<IKisDal, KisDal>();
			//services.AddTransient<IMailService, MailService>();

			AppConst.Constants.DataModel = "Mock-Db";

		} // end
	}
}

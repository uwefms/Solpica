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

			AppConst.Constants.DataModel = "Mock-Db";

		} // end
	}
}


using SfDocFilter.Dal;

namespace SfDocFilter.DalMock
{
	public class OnBaseDal : IOnBaseDal
	{
		public Task<List<OnBaseDto>> GetOnBaseListAsync(string? cFilter)
		{
			throw new NotImplementedException();
		}
	}
}

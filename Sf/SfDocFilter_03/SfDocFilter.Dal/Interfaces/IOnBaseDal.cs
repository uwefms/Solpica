namespace SfDocFilter.Dal
{
	public interface IOnBaseDal
	{
		Task<List<OnBaseDto>> GetOnBaseListAsync(string? cFilter);

		Task<List<OnBaseDto>> GetOnBaseListAsync(FilterDto? oFilter);
		
	}
}

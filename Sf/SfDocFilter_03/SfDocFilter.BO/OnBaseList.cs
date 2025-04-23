using SfDocFilter.Dal;

namespace SfDocFilter.BO
{
	public class OnBaseList
	{
		private readonly IOnBaseDal? OnBaseDal;

		// public List<OnBaseDto>? Items { get; set; }

		public OnBaseList(IOnBaseDal? onBaseDal)
		{
			OnBaseDal = onBaseDal;

			// Items = new List<OnBaseDto>();
		}

		public async Task<List<OnBaseDto>> GetOnBaseListAsync(string? cFilter)
		{
			List<OnBaseDto> oRet = new List<OnBaseDto>();
			if (OnBaseDal != null)
			{
				oRet = (await OnBaseDal.GetOnBaseListAsync(cFilter)).ToList();
			}
			return oRet;
		}

		//public void AddItem(OnBaseDto item)
		//{
		//	Items.Add(item);
		//}

	}
}


using SfDocFilter.Dal;

namespace SfDocFilter.DalMock
{
	public class OnBaseDal : IOnBaseDal
	{
		public async Task<List<OnBaseDto>> GetOnBaseListAsync(string? cFilter)
		{			
			List<OnBaseDto> oOnBaseList = new List<OnBaseDto>();

			var result = (from x in MockDb.OnBaseListData						  
			// where (x.AktSchulId == AktSchulId)
			select new OnBaseDto
			{
				 TestDataId     = x.TestDataId     
				,First_name     = x.First_name     
				,Last_name      = x.Last_name      
				,Full_name      = x.Full_name      
				,Birth_date     = x.Birth_date     
				,Gender         = x.Gender         
				,Nationality    = x.Nationality    
				,Occupation     = x.Occupation     
				,Marital_status = x.Marital_status 
				,Street_address = x.Street_address 
				,City           = x.City           
				,State          = x.State          
				,Country        = x.Country        
				,Postal_code    = x.Postal_code    
				,Phone_number   = x.Phone_number   
				,Email          = x.Email          
				,First_name1    = x.First_name1    
				,Last_name1     = x.Last_name1     
				,Document_number= x.Document_number
				,Document_type  = x.Document_type  
				,Issue_date     = x.Issue_date     
				,Expiry_date    = x.Expiry_date    

			});

			oOnBaseList = result.ToList();

			return await Task.FromResult(oOnBaseList);
		}

		public async Task<List<OnBaseDto>> GetOnBaseListAsync(FilterDto? oFilter)
		{
			List<OnBaseDto> oOnBaseList = new List<OnBaseDto>();

			var result = MockDb.OnBaseListData.AsQueryable();

			// Apply filters if oFilter is not null
			if (oFilter != null)
			{
			    if (!string.IsNullOrEmpty(oFilter.First_name))
			        result = result.Where(x => x.First_name != null && x.First_name.Contains(oFilter.First_name, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Last_name))
			        result = result.Where(x => x.Last_name != null && x.Last_name.Contains(oFilter.Last_name, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Full_name))
			        result = result.Where(x => x.Full_name != null && x.Full_name.Contains(oFilter.Full_name, StringComparison.OrdinalIgnoreCase));

			    if (oFilter.Birth_date != default)
			        result = result.Where(x => x.Birth_date.Date == oFilter.Birth_date.Date);

			    if (!string.IsNullOrEmpty(oFilter.Gender))
			        result = result.Where(x => x.Gender != null && x.Gender.Equals(oFilter.Gender, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Nationality))
			        result = result.Where(x => x.Nationality != null && x.Nationality.Contains(oFilter.Nationality, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Occupation))
			        result = result.Where(x => x.Occupation != null && x.Occupation.Contains(oFilter.Occupation, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Marital_status))
			        result = result.Where(x => x.Marital_status != null && x.Marital_status.Equals(oFilter.Marital_status, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.City))
			        result = result.Where(x => x.City != null && x.City.Contains(oFilter.City, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.State))
			        result = result.Where(x => x.State != null && x.State.Contains(oFilter.State, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Country))
			        result = result.Where(x => x.Country != null && x.Country.Contains(oFilter.Country, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Postal_code))
			        result = result.Where(x => x.Postal_code != null && x.Postal_code.Contains(oFilter.Postal_code, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Email))
			        result = result.Where(x => x.Email != null && x.Email.Contains(oFilter.Email, StringComparison.OrdinalIgnoreCase));

			    if (!string.IsNullOrEmpty(oFilter.Document_type))
			        result = result.Where(x => x.Document_type != null && x.Document_type.Contains(oFilter.Document_type, StringComparison.OrdinalIgnoreCase));

			    if (oFilter.Issue_date != default)
			        result = result.Where(x => x.Issue_date.Date == oFilter.Issue_date.Date);

			    if (oFilter.Expiry_date != default)
			        result = result.Where(x => x.Expiry_date.Date == oFilter.Expiry_date.Date);
			}

			oOnBaseList = result.Select(x => new OnBaseDto
			{
			    TestDataId		= x.TestDataId,
			    First_name		= x.First_name,
			    Last_name		= x.Last_name,
			    Full_name		= x.Full_name,
			    Birth_date		= x.Birth_date,
			    Gender			= x.Gender,
			    Nationality		= x.Nationality,
			    Occupation		= x.Occupation,
			    Marital_status	= x.Marital_status,
			    Street_address	= x.Street_address,
			    City			= x.City,
			    State			= x.State,
			    Country			= x.Country,
			    Postal_code		= x.Postal_code,
			    Phone_number	= x.Phone_number,
			    Email			= x.Email,
			    First_name1		= x.First_name1,
			    Last_name1		= x.Last_name1,
			    Document_number = x.Document_number,
			    Document_type	= x.Document_type,
			    Issue_date		= x.Issue_date,
			    Expiry_date		= x.Expiry_date
			}).ToList();

			return await Task.FromResult(oOnBaseList);

			//var result = (from x in MockDb.OnBaseListData						  
			//// where (x.AktSchulId == AktSchulId)
			//select new OnBaseDto
			//{
			//	 TestDataId     = x.TestDataId     
			//	,First_name     = x.First_name     
			//	,Last_name      = x.Last_name      
			//	,Full_name      = x.Full_name      
			//	,Birth_date     = x.Birth_date     
			//	,Gender         = x.Gender         
			//	,Nationality    = x.Nationality    
			//	,Occupation     = x.Occupation     
			//	,Marital_status = x.Marital_status 
			//	,Street_address = x.Street_address 
			//	,City           = x.City           
			//	,State          = x.State          
			//	,Country        = x.Country        
			//	,Postal_code    = x.Postal_code    
			//	,Phone_number   = x.Phone_number   
			//	,Email          = x.Email          
			//	,First_name1    = x.First_name1    
			//	,Last_name1     = x.Last_name1     
			//	,Document_number= x.Document_number
			//	,Document_type  = x.Document_type  
			//	,Issue_date     = x.Issue_date     
			//	,Expiry_date    = x.Expiry_date    

			//});

			//oOnBaseList = result.ToList();

			//return await Task.FromResult(oOnBaseList);

		}
	}
}

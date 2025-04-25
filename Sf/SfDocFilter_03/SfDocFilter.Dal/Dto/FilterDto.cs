namespace SfDocFilter.Dal
{
	[Serializable]
	public class FilterDto
	{
		public FilterDto(){}
           
	    public string? First_name       { get; set; } // NVARCHAR(50) NULL
	    public string? Last_name        { get; set; } // NVARCHAR(50) NULL,
        public string? Full_name        { get; set; } // NVARCHAR(100) NULL,
        public DateTime Birth_date      { get; set; } // DATETIME NULL,
        public string? Gender           { get; set; } // NVARCHAR(10) NULL,
        public string? Nationality      { get; set; } // NVARCHAR(100) NULL,
        public string? Occupation       { get; set; } // NVARCHAR(100) NULL,
        public string? Marital_status   { get; set; } // NVARCHAR(20) NULL,
        public string? Street_address   { get; set; } // NVARCHAR(100) NULL,
        public string? City             { get; set; } // NVARCHAR(50) NULL,
        public string? State            { get; set; } // NVARCHAR(50) NULL,
        public string? Country          { get; set; } // NVARCHAR(100) NULL,
        public string? Postal_code      { get; set; } // NVARCHAR(20) NULL,
        public string? Phone_number     { get; set; } // NVARCHAR(50) NULL,
        public string? Email            { get; set; } // NVARCHAR(100) NULL,
        public string? First_name1      { get; set; } // NVARCHAR(50) NULL,
        public string? Last_name1       { get; set; } // NVARCHAR(50) NULL,
        
        public string? Document_type    { get; set; } // NVARCHAR(50) NULL,
        public DateTime Issue_date      { get; set; }
        public DateTime Expiry_date     { get; set; }
	}
}
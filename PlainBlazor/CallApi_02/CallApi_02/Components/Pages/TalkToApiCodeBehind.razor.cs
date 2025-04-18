
namespace CallApi_02.Components.Pages
{
	public partial class TalkToApiCodeBehind
	{        
        // private readonly string BaseUrl = "https://localhost:7137";
        private readonly string BaseUrl = "";

        private List<WeatherForecast>? weatherForecasts;

        private List<DirectoryFileModel>? directoryFiles;

        // advanced usage of HttpClient
        private readonly IHttpClientFactory _httpClientFactory;

		public TalkToApiCodeBehind(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

            if (string.IsNullOrEmpty(BaseUrl))
            {
                // Read the new host address from WebSrv.txt
                BaseUrl = File.ReadAllText(@"c:\dev\WebSrv.txt").Trim();
			}
        }


        private async Task GetWeatherForecast()
        {   
            try
            {
                var client = _httpClientFactory.CreateClient();

                // Set the X-Api-Key header
                client.DefaultRequestHeaders.Add("X-Api-Key", "bubu1");

                 // Make the GET request
                var response = await client.GetFromJsonAsync<List<WeatherForecast>>($"{BaseUrl}/weatherforecast");

                weatherForecasts = response!;

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }			
        }

        private async Task GetAllDirs()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Add("X-Api-Key", "bubu1");

                // Make the GET request
                directoryFiles = await client.GetFromJsonAsync<List<DirectoryFileModel>>($"{BaseUrl}/api/file/getalldirs");

                if (directoryFiles == null || !directoryFiles.Any())
                {
                    Console.WriteLine("No directories found.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public class WeatherForecast
        {
            public DateOnly Date { get; set; }
            public int TemperatureC { get; set; }
            public string? Summary { get; set; }
        }

        public class DirectoryFileModel
        {
            public string? DirName { get; set; } = string.Empty;        
        	public string? FileName { get; set; } = string.Empty;
        }
	}
}

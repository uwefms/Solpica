// Sample call from client side

namespace ApiTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // string BaseUrl = "https://localhost:7137";
            string BaseUrl = "";

            if (string.IsNullOrEmpty(BaseUrl))
            {
                // Read the new host address from WebSrv.txt
                BaseUrl = File.ReadAllText(@"c:\dev\WebSrv.txt").Trim();
			}

            using var client = new HttpClient();

			// Set additional header for x-api-key
			client.DefaultRequestHeaders.Add("X-Api-Key", "bubu1");

            // Call with ApiKey in Header (see FileServer.http in FileServer project)
			// var response = await client.GetAsync($"{BaseUrl}/api/file/GetAllDirs");

			// Call with ApiKey in Header (see FileServer.http in FileServer project)
			// var response = await client.GetAsync($"{BaseUrl}/WeatherForecast");

			// Call with ApiKey in URL (see FileServer.http in FileServer project)
			var response = await client.GetAsync($"{BaseUrl}/WeatherForecast/?ApiKey=bubu2");
           
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}

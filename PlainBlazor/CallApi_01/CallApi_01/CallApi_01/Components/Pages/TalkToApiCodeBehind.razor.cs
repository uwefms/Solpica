using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CallApi_01.Components.Pages
{
	public partial class TalkToApiCodeBehind
	{
        [Inject] HttpClient Http { get; set; } = default!;

        private readonly string BaseUrl = "https://localhost:7137";
        // private readonly string BaseUrl = "";

		public TalkToApiCodeBehind()
        {
            if (string.IsNullOrEmpty(BaseUrl))
            {
                // Read the new host address from WebSrv.txt
                BaseUrl = File.ReadAllText(@"c:\dev\WebSrv.txt").Trim();
			}
        }

        private List<WeatherForecast>? weatherForecasts;

        private List<DirectoryFileModel>? directoryFiles;

        private async Task GetWeatherForecast()
        {   
            var response = await Http.GetFromJsonAsync<List<WeatherForecast>>($"{BaseUrl}/weatherforecast/?ApiKey=bubu2");

			// Uncomment the following line to test with a hardcoded URL
			// var response = await Http.GetFromJsonAsync<List<WeatherForecast>>("https://localhost:7137/weatherforecast/?ApiKey=bubu2");            

			weatherForecasts = response!;
        }

        protected override async Task OnInitializedAsync()
         {
             try
             {
                 // Set the X-Api-Key header
                 // Http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("X-Api-Key", "bubu1");
                Http.DefaultRequestHeaders.Add("X-Api-Key", "bubu1");

                 // Make the GET request
                weatherForecasts = await Http.GetFromJsonAsync<List<WeatherForecast>>("https://pedavdev.de/FileServer/weatherforecast");
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error fetching weather forecast: {ex.Message}");
             }
         }



   //     private async Task GetAllDir()
   //     {   
   //         using var client = new HttpClient();
   //         client.DefaultRequestHeaders.Add("X-Api-Key", "bubu1");

   //         var response = await client.GetAsync($"{BaseUrl}/api/file/getalldir");

   //         if (response.IsSuccessStatusCode)
   //         {
   //             var content = await response.Content.ReadAsStringAsync();
			//	// Console.WriteLine(content);

			//	// Deserialize the JSON response into a list of DirectoryFileModel
   //             directoryFiles = System.Text.Json.JsonSerializer.Deserialize<List<DirectoryFileModel>>(content);
			//}
			//else
   //         {
   //             Console.WriteLine($"Error: {response.StatusCode}");
   //         }

   //     }



        //private async Task GetAllDir()
        //{
        //    try
        //    {
        //        // Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiHeaderKey", "bubu1");

        //               // Set the X-Api-Key header
        //        Http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("X-Api-Key", "bubu1");

        //        // Make the API call using the injected HttpClient
        //        directoryFiles = await Http.GetFromJsonAsync<List<DirectoryFileModel>>($"{BaseUrl}/api/file/getalldir");
        
        //        if (directoryFiles == null || !directoryFiles.Any())
        //        {
        //            Console.WriteLine("No directories found.");
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        Console.WriteLine($"Request error: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}


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


/*

// Sample call from client side
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", "bubu1");

        var response = await client.GetAsync("http://yourserver/api/file/getalldir");
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
*/





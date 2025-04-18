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

        // private readonly string BaseUrl = "https://localhost:7137";
        private readonly string BaseUrl = "";

		public TalkToApiCodeBehind()
        {
            if (string.IsNullOrEmpty(BaseUrl))
            {
                // Read the new host address from WebSrv.txt
                BaseUrl = File.ReadAllText(@"c:\dev\WebSrv.txt").Trim();
			}
        }

        private List<WeatherForecast>? weatherForecasts;

        private async Task GetWeatherForecast()
        {   
            var response = await Http.GetFromJsonAsync<List<WeatherForecast>>($"{BaseUrl}/weatherforecast/?ApiKey=bubu2");

			// Uncomment the following line to test with a hardcoded URL
			// var response = await Http.GetFromJsonAsync<List<WeatherForecast>>("https://localhost:7137/weatherforecast/?ApiKey=bubu2");            

			weatherForecasts = response!;
        }

        public class WeatherForecast
        {
            public DateOnly Date { get; set; }
            public int TemperatureC { get; set; }
            public string? Summary { get; set; }
        }
	}
}
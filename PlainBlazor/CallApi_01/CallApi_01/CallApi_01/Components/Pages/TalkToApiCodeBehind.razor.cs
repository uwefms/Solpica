using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CallApi_01.Components.Pages
{
	public partial class TalkToApiCodeBehind
	{
        [Inject] HttpClient Http { get; set; } = default!;

		public TalkToApiCodeBehind(){}

        private List<WeatherForecast>? weatherForecasts;

        private async Task GetWeatherForecast()
        {        
            var response = await Http.GetFromJsonAsync<List<WeatherForecast>>("https://localhost:7137/weatherforecast/?ApiKey=bubu2");
            // var response = await Http.GetFromJsonAsync<List<WeatherForecast>>("https://pedavdev.de/FileServer/weatherforecast/?ApiKey=bubu2");

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

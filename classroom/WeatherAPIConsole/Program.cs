using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace WeatherAPIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This application demonstrates a working API by getting basic weather for any US Zip Code.");

            Console.Write("Enter a zip code: ");
            string zipcode = Console.ReadLine();

            Console.Write("Enter your API key for OpenWeatherMap: ");
            string apiKey = Console.ReadLine();

            DoWeatherSearch(zipcode, apiKey);
        }

        private static void DoWeatherSearch(string zipcode, string apiKey)
        {
            WeatherAPIResult model = null;

            HttpClient client = new HttpClient();
            
            string uri = $"http://api.openweathermap.org/data/2.5/weather?" +
                $"zip={zipcode},us&units=imperial&appid={apiKey}";

            var task = client.GetAsync(uri)
                .ContinueWith((taskForResponse) =>
                {
                    HttpResponseMessage response = taskForResponse.Result;
                    var processJson = response.Content.ReadAsAsync<WeatherAPIResult>();
                    processJson.Wait();
                    model = processJson.Result;
                });

            task.Wait();
            DisplaySearchResults(model);
        }

        private static void DisplaySearchResults(WeatherAPIResult model)
        {
            Console.WriteLine($"\nTemperature (F): {model.Main.Temperature}");
            Console.WriteLine($"Humidity: {model.Main.Humidity}%");
            Console.WriteLine($"Pressure: {model.Main.Pressure}");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    public class WeatherAPIResult
    {
        [JsonProperty("main")]
        public MainData Main { get; set; }
    }


    public class MainData
    {
        [JsonProperty("temp")]
        public decimal Temperature { get; set; }
        [JsonProperty("humidity")]
        public decimal Humidity { get; set; }
        [JsonProperty("pressure")]
        public decimal Pressure { get; set; }
    }
}

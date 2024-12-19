using MaxMind.GeoIP2;
using MyWebSity.Models;
using System.Net;
using System.Runtime;
using System.Text.Json;

namespace MyWebSity.Services
{
    public class GeoIpService
    {
        private readonly string _databasePath;
        private readonly IHttpClientFactory _httpClientFactory;

        public GeoIpService(IWebHostEnvironment env, IHttpClientFactory httpClientFactory)
        {
            _databasePath = Path.Combine(env.ContentRootPath, "App_Data/GeoLite2-City.mmdb");
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CityInfo?> GetCityInfo(IPAddress ipAddress)
        {
            if (ipAddress == null)
            {
                return null;
            }
            try
            {
                using (var reader = new DatabaseReader(_databasePath))
                {
                    var city = reader.City(ipAddress);
                    var cityName = city?.City?.Name;
                    if (cityName == null)
                    {
                        return null;
                    }
                    var cityInfo = new CityInfo { CityName = cityName };
                    await GetWeatherData(cityInfo);
                    return cityInfo;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        private async Task GetWeatherData(CityInfo cityInfo)
        {
            if (cityInfo.CityName != null)
            {

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    string apiKey = "YOUR_OPENWEATHERMAP_API_KEY"; // Замените на свой ключ API
                    string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityInfo.CityName}&appid={apiKey}&units=metric";
                    try
                    {
                        var response = await httpClient.GetAsync(apiUrl);
                        response.EnsureSuccessStatusCode();
                        var json = await response.Content.ReadAsStringAsync();

                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            if (root.TryGetProperty("weather", out JsonElement weatherArray) && weatherArray.GetArrayLength() > 0)
                            {
                                var weather = weatherArray[0];
                                if (weather.TryGetProperty("description", out JsonElement description))
                                {
                                    cityInfo.WeatherDescription = description.GetString();
                                }
                            }
                            if (root.TryGetProperty("main", out JsonElement main))
                            {
                                if (main.TryGetProperty("temp", out JsonElement temp))
                                {
                                    cityInfo.Temperature = temp.GetDouble();
                                }
                            }
                            if (root.TryGetProperty("wind", out JsonElement wind))
                            {
                                if (wind.TryGetProperty("speed", out JsonElement speed))
                                {
                                    cityInfo.WindSpeed = speed.GetDouble();
                                }
                            }
                        }

                    }
                    catch (HttpRequestException)
                    {
                        // Обработайте ошибку запроса к API погоды.
                        return;
                    }
                }
            }
        }
    }
}

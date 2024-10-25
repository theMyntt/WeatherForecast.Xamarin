using System;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using WeatherForecast.Xamarin.Models;

namespace WeatherForecast.Xamarin.Services
{
	public class ForecastService
	{
        private readonly HttpClient _http;
        private readonly JsonSerializerSettings _jsonSettings;

        public ForecastService()
		{
            _http = new HttpClient();

            // Using JsonSerializerSettings because this version dont have System.Text.Json
            // So i'm using Newtonsoft.Json
            _jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public async Task<ForecastDTO> Perform(int cityCode) => await ApplyInternalLogic(cityCode);

        public async Task<ForecastDTO> ApplyInternalLogic(int cityCode)
        {
            Uri uri = new Uri($"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{cityCode}");

            try
            {
                var response = await _http.GetAsync(uri);

                if (!response.IsSuccessStatusCode)
                    return new ForecastDTO();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ForecastDTO>(json, _jsonSettings);
            } catch(Exception)
            {
                return new ForecastDTO();
            }
        }
    }
}


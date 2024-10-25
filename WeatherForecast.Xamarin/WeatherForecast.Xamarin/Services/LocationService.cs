using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.Xamarin.Models;

namespace WeatherForecast.Xamarin.Services
{
    public class LocationService
	{
		private readonly HttpClient _http;
		private readonly JsonSerializerSettings _jsonSettings;

        public LocationService()
		{
			_http = new();

			// Using JsonSerializerSettings because this version dont have System.Text.Json
			// So i'm using Newtonsoft.Json
			_jsonSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				Formatting = Formatting.Indented,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
		}

        public async Task<ObservableCollection<LocationDTO>?> PerformFindAll() => await ApplyInternalLogicForFindAll();
		public async Task<ObservableCollection<LocationDTO>?> PerformFindSpecific(string query) => await ApplyInternalLogicForFindSpecific(query);

        private async Task<ObservableCollection<LocationDTO>?> ApplyInternalLogicForFindAll()
		{
			Uri uri = new("https://brasilapi.com.br/api/cptec/v1/cidade");

			try
			{
				var response = await _http.GetAsync(uri);

				if (!response.IsSuccessStatusCode) return null;

				return await FormatLocations(response);
			} catch (Exception)
			{
				return null;
			}
		}

		private async Task<ObservableCollection<LocationDTO>?> ApplyInternalLogicForFindSpecific(string query)
		{
			Uri uri = new($"https://brasilapi.com.br/api/cptec/v1/cidade/{query.Trim()}");

			try
			{
				var response = await _http.GetAsync(uri);

				if (!response.IsSuccessStatusCode) return null;

				return await FormatLocations(response);
			} catch (Exception)
			{
				return null;
			}
		}

		private async Task<ObservableCollection<LocationDTO>?> FormatLocations(HttpResponseMessage response)
		{
			var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ObservableCollection<LocationDTO>>(json, _jsonSettings);
        }
	}
}

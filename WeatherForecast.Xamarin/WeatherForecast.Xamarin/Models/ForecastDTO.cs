using System.Collections.Generic;

namespace WeatherForecast.Xamarin.Models
{
    public struct ForecastDTO
	{
		public string Cidade { get; set; }
		public string Estado { get; set; }
		public string Atualizado_em { get; set; }
		IEnumerable<WeatherDTO> Clima { get; set; }
	}

	public struct WeatherDTO
	{
		public string Data { get; set; }
		public string Codicao { get; set; }
		public string Condicao_desc { get; set; }
		public int Min { get; set; }
		public int Max { get; set; }
		public int Indice_iv { get; set; }
	}
}


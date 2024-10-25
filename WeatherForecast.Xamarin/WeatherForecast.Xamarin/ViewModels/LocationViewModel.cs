using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WeatherForecast.Xamarin.Models;
using WeatherForecast.Xamarin.Services;
using Xamarin.Forms;

namespace WeatherForecast.Xamarin.ViewModels
{
    public partial class LocationViewModel : ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<LocationDTO> locations;

		[ObservableProperty]
		private string query;

		private readonly LocationService _service;

		public ICommand GetSpecific { get; }
		public ICommand Clear { get; }

		public LocationViewModel()
		{
			_service = new LocationService();

			GetSpecific = new Command(GetSpecificLocation);
			Clear = new Command(ClearFilters);
		}

		public async void GetLocations()
		{
			Locations = await _service.PerformFindAll();
		}

		public async void GetSpecificLocation()
		{
			if (string.IsNullOrEmpty(Query))
			{
				Locations = await _service.PerformFindAll();
				return;
			}
			Locations = await _service.PerformFindSpecific(Query);
		}

		public async void ClearFilters()
		{
			Query = "";
			Locations = await _service.PerformFindAll();
		}

		public async void NavigateToForecast(int cityCode)
		{
			await Shell.Current.GoToAsync($"///Forecast?CityCode={cityCode}");
		}
	}
}


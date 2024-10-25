using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WeatherForecast.Xamarin.Models;
using WeatherForecast.Xamarin.Services;
using Xamarin.Forms;

namespace WeatherForecast.Xamarin.ViewModels
{
    public partial class LocationViewModel : INotifyPropertyChanged 
	{
		private ObservableCollection<LocationDTO> locations;
		private string query;

		public ObservableCollection<LocationDTO> Locations
		{
			get => locations;
			set => SetProperty(ref locations, value);
		}

		public string Query
		{
			get => query;
			set => SetProperty(ref query, value);
		}

		private readonly LocationService _service;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GetSpecific { get; }
		public ICommand Clear { get; }
		public ICommand GoToForecast { get; }

		public LocationViewModel()
		{
			_service = new LocationService();

			GetSpecific = new Command(GetSpecificLocation);
			Clear = new Command(ClearFilters);
			GoToForecast = new Command<int>(NavigateToForecast);

            GetLocations();
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}


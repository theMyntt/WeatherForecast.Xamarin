using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WeatherForecast.Xamarin.Models;
using WeatherForecast.Xamarin.Services;

namespace WeatherForecast.Xamarin.ViewModels
{
	public class ForecastViewModel : INotifyPropertyChanged
	{
        private ForecastDTO _location;
        private int _cityCode;

        public ForecastDTO Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public int CityCode
        {
            get => _cityCode;
            set => SetProperty(ref _cityCode, value);
        }

        private readonly ForecastService _service;

		public ForecastViewModel()
		{
            _service = new ForecastService();
            CityCode = 244;
		}

        public async Task GetLocationForecast()
        {
            Location = await _service.Perform(CityCode);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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


using System;
using System.Collections.Generic;
using WeatherForecast.Xamarin.ViewModels;
using Xamarin.Forms;

namespace WeatherForecast.Xamarin.Views
{
	[QueryProperty("CityCode", "CityCode")]
	public partial class ForecastView : ContentPage
	{
		private readonly ForecastViewModel _viewModel;

		public string CityCode { get; set; }

		public ForecastView ()
		{
			InitializeComponent();
			_viewModel = new ForecastViewModel();
            BindingContext = _viewModel;
		}

        protected override async void OnAppearing()
        {
            if (!string.IsNullOrEmpty(CityCode))
            {
                _viewModel.CityCode = int.Parse(CityCode);
            }
            await _viewModel.GetLocationForecast();

            base.OnAppearing();
        }
    }
}


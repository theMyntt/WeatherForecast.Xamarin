using System;
using System.Collections.Generic;
using WeatherForecast.Xamarin.ViewModels;
using Xamarin.Forms;

namespace WeatherForecast.Xamarin.Views
{	
	public partial class LocationView : ContentPage
	{	
		public LocationView ()
		{
			InitializeComponent();
			BindingContext = new LocationViewModel();
		}
	}
}


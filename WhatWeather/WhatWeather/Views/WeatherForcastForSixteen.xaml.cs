using System;
using WhatWeather.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherForcastForSixteen : ContentPage
    {
        public WeatherForcastForSixteen()
        {
            InitializeComponent();
            GetWeatherDatafor16Dayes();
        }

        private void GetWeatherDatafor16Dayes()
        {
            var climate = new CityClimateVM();
            climate.Latitude = Convert.ToDecimal(Application.Current.Properties["lat"]);
            climate.Longitude = Convert.ToDecimal(Application.Current.Properties["lng"]);
            climate.Cnt = 16;

            this.BindingContext = climate;
        }
    }
}

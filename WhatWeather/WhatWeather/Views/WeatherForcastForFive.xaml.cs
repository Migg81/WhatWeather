using System;
using WhatWeather.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherForcastForFive : ContentPage
    {
        public WeatherForcastForFive()
        {
            InitializeComponent();
            GetWeatherDatafor7Dayes();
        }

        private void GetWeatherDatafor7Dayes()
        {
            var climate = new CityClimate5day3hourVM();
            climate.Latitude = Convert.ToDecimal(Application.Current.Properties["lat"]);
            climate.Longitude = Convert.ToDecimal(Application.Current.Properties["lng"]);

            this.BindingContext = climate;
        }
    }
}

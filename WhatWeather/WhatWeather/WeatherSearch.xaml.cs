using System;
using WhatWeather.BackEndService;
using WhatWeather.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWeather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherSearch : ContentPage
    {
        public SearchForecast MyWeather { get; set; }
        public WeatherSearch()
        {
            InitializeComponent();
            MyWeather = new SearchForecast();

            this.BindingContext = MyWeather;
        }
        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            var autoCopletSearchText = new SearchForecast();

            this.BindingContext = autoCopletSearchText;
        }

        private async void EmployeeListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var city = (City)e.Item;
                MyWeather.CityWeather =  await CoreService.GetWeatherByLatLng(city.Latitude, city.Longitude);

                MyWeather.IsCitylistVisibla = false;

                CityWeatherLV.ItemsSource = MyWeather.CityWeather.Weathers;
            }
        }
    }
}

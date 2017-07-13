using System;
using WhatWeather.BackEndService;
using WhatWeather.Model;
using WhatWeather.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWeather
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherSearchByDateAndCity : ContentPage
    {
        public WeatherHistory CityWeatherHistory { get; set; }
        public WeatherSearchByDateAndCity()
        {
            InitializeComponent();
            CityWeatherHistory = new WeatherHistory();
            BindingContext = CityWeatherHistory;
        }


        private void CityListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var city = (City)e.Item;
                //MyWeather.CityWeather = await CoreService.GetWeatherByLatLng(city.Latitude, city.Longitude);

                CityWeatherHistory.IsCitylistVisibla = false;
                CityWeatherHistory.Latitude = city.Latitude;
                CityWeatherHistory.Longitude = city.Longitude;
                CityWeatherHistory.SearchText = city.DisplayName;

                //CityWeatherLV.ItemsSource = MyWeather.CityWeather.Weathers;
            }
        }
    }

   }

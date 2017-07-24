using System;
using WhatWeather.Model;
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
            MyWeather.IsCityListItemSelected = true;
            MyWeather.IsRecordAvailable = false;
        }

        private async void CityListview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                var city = (City)e.Item;
                MyWeather.CityWeather = await CoreService.GetWeatherByLatLng(city.Latitude, city.Longitude);
                CitySearch.Text = city.DisplayName;
                MyWeather.IsCitylistVisibla = false;

                if (MyWeather.CityWeather.Climate != null && MyWeather.CityWeather.Climate.Count > 0)
                {
                    CityWeatherLV.ItemsSource = MyWeather.CityWeather.Climate;
                    MyWeather.IsRecordAvailable = true;
                }
                else
                {
                    MyWeather.IsRecordAvailable = false;
                }
            }
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            //CityListview.SelectedItem = null;
            //var selected = (ViewCell)(sender);
            //if (selected.View.BackgroundColor.Equals(Color.FromHex("#af4448")))
            //{
            //    selected.View.BackgroundColor = Color.Transparent;
            //}
            //else
            //{
            //    selected.View.BackgroundColor = Color.FromHex("#af4448");
            //}
        }
    }
}

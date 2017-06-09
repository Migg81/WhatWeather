using System;
using WhatWeather.BackEndService;
using WhatWeather.NativeService;
using Xamarin.Forms;

namespace WhatWeather
{
    public partial class MainPage : ContentPage
    {
        IMyLocation LocService { get; set; }
        ILocationEventArgs LocationArg { get; set; }
        public MainPage()
        {
            InitializeComponent();
            LocService = DependencyService.Get<IMyLocation>();
            GetCurentLocation();
        }

        protected override async void OnAppearing()
        {
           await GetWeatherDetails();    
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            LocService.StopLocationUpdates();
        }

        private async System.Threading.Tasks.Task GetWeatherDetails()
        {
            //City CityWeatherDetails = await CoreService.GetWeather("94040", "us");
            if (Application.Current.Properties.ContainsKey("lat") && Application.Current.Properties.ContainsKey("lng"))
            {
                var lat = Application.Current.Properties["lat"] ;
                var lng = Application.Current.Properties["lng"];

                City CityWeatherDetails = await CoreService.GetWeatherByLatLng(Convert.ToDecimal(lat), Convert.ToDecimal(lng));
                this.BindingContext = CityWeatherDetails;
                listWeather.ItemsSource = CityWeatherDetails.Weathers;
            }
           else
            {
                //to do
            }
        }

        private async void GetCurentLocation()
        {
            LocationArg = new LocationEventArgs();

            LocService.LocationObtained += async (object sender,
                ILocationEventArgs e) =>
            {
                LocationArg.lat = e.lat;
                LocationArg.lng = e.lng;

                Application.Current.Properties["lat"] = e.lat;
                Application.Current.Properties["lng"] = e.lng;

                await Application.Current.SavePropertiesAsync();

                await GetWeatherDetails();
            };

            LocService.ObtainMyLocation();
        }

        async void getWeatherBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeatherSearch(), false);
        }
    }
}

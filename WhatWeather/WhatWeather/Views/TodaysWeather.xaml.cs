using System;
using WhatWeather.NativeService;
using WhatWeather.ViewModel;
using Xamarin.Forms;

namespace WhatWeather
{
    public partial class TodaysWeather : ContentPage
    {
        IMyLocation LocService { get; set; }
        ILocationEventArgs LocationArg { get; set; }

        TodayesWeatherVM  todayesVM { get; set; }
        public TodaysWeather()
        {
            InitializeComponent();
            LocService = DependencyService.Get<IMyLocation>();
            GetCurentLocation();
            todayesVM = new TodayesWeatherVM();
            this.BindingContext = todayesVM;
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

        private async System.Threading.Tasks.Task GetWeatherDetails()
        {
            ////City CityWeatherDetails = await CoreService.GetWeather("94040", "us");
            //if (Application.Current.Properties.ContainsKey("lat") && Application.Current.Properties.ContainsKey("lng"))
            //{
            //    var lat = Application.Current.Properties["lat"];
            //    var lng = Application.Current.Properties["lng"];

            //    City CityWeatherDetails = await CoreService.GetWeatherByLatLng(Convert.ToDecimal(lat), Convert.ToDecimal(lng));
            //    this.BindingContext = CityWeatherDetails;
            //    //listWeather.ItemsSource = CityWeatherDetails.Climate;
            //    list.ItemsSource = CityWeatherDetails.Climate;

            //    await todayesVM.GetWeatherDetails();
            //}
            //else
            //{
            //    //to do
            //}

            await todayesVM.GetWeatherDetails();

            list.ItemsSource = todayesVM.Climate;
            list.HeightRequest = 170;
        }

        //private async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        //{
        //    var action =await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Weather by City", "Advance Search");

        //    switch (action)
        //    {
        //        case "Weather by City":
        //            WeatherByCity();
        //            break;
        //        case "Advance Search":
        //            AdvanceSearch();
        //            break;

        //    }
        //}

        //private async void AdvanceSearch()
        //{
        //    await Navigation.PushAsync(new WeatherSearchByDateAndCity(), false);
        //}

        private async void WeatherByCity(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WhatWeather.Views.FavariteCityes(), false);
        }
    }
}

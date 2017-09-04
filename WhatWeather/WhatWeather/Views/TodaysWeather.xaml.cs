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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            todayesVM = new TodayesWeatherVM();
            this.BindingContext = todayesVM;
            GetWeatherDetails();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            LocService.StopLocationUpdates();
        }

        private void GetCurentLocation()
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
            };

            LocService.ObtainMyLocation();
        }

        private void GetWeatherDetails()
        {
            try
            {
                todayesVM.GetWeatherDetails();
                list.ItemsSource = todayesVM.Climate;
                list.HeightRequest = 170;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private async void WeatherByCity(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WhatWeather.Views.Favarite_Cityes(), false);
        }
    }
}

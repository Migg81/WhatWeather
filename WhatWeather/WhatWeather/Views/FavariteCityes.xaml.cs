using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Pages;
using Xamarin.Forms.Xaml;

namespace WhatWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavariteCityes : Xamarin.Forms.Pages.ListDataPage
    {
        public FavariteCityes()
        {
            InitializeComponent();
        }

        private void PopulateFavariteCity()
        {
            if (Application.Current.Properties.Keys.Contains("FavariteCityes"))
            {
                var cityDetails = (List<WhatWeather.ViewModel.FavariteCity>)Application.Current.Properties["FavariteCityes"];
                var jsondata = JsonConvert.SerializeObject(cityDetails);
                var JsonDataSource = new JsonDataSource();
                JsonDataSource.Source = jsondata;

                this.DataSource = JsonDataSource; 
            }
        }

        private async void AddFavariteCity(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeatherSearch(), false);
        }

        protected override void OnAppearing()
        {
            PopulateFavariteCity();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}

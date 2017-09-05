using System;
using System.Collections.ObjectModel;
using WhatWeather.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favarite_Cityes : ContentPage
    {
        public WhatWeather.ViewModel.FavariteCity FavaCity { get; set; }
        public Favarite_Cityes()
        {
            FavaCity = new WhatWeather.ViewModel.FavariteCity();
            this.BindingContext = FavaCity;
            InitializeComponent();
            PopulateFavariteCity();
        }

        private void PopulateFavariteCity()
        { 
            if (Application.Current.Properties.Keys.Contains("FavariteCityes"))
            {
                var cityDetails = (ObservableCollection<FavariteCityModel>)Application.Current.Properties["FavariteCityes"];

                FavaCity.FavariteCityes = cityDetails;

                listCitys.ItemsSource = FavaCity.FavariteCityes;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PopulateFavariteCity();
        }

        private async void AddFavariteCity(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeatherSearch(), false);
        }
    }
}

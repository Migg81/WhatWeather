using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatWeather.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favarite_Cityes : ContentPage
    {
        public Favarite_Cityes()
        {
            InitializeComponent();
            PopulateFavariteCity();
        }

        private void PopulateFavariteCity()
        {
            var favariteCity = new WhatWeather.ViewModel.FavariteCity();
            this.BindingContext = favariteCity;
            if (Application.Current.Properties.Keys.Contains("FavariteCityes"))
            {
                var cityDetails = (List<FavariteCityModel>)Application.Current.Properties["FavariteCityes"];
                //var jsondata = JsonConvert.SerializeObject(cityDetails);
                //var JsonDataSource = new JsonDataSource();
                //JsonDataSource.Source = jsondata;

                favariteCity.FavariteCityes = cityDetails;

                listCitys.ItemsSource = favariteCity.FavariteCityes;
            }
        }
        protected override void OnAppearing()
        {
            PopulateFavariteCity();
        }

        private async void AddFavariteCity(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WeatherSearch(), false);
        }

        private  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //await Navigation.PopToRootAsync();
            //await Navigation.RemovePage(new TodaysWeather());
            //PushAsync(new TodaysWeather(), false);

           
        }
    }
}

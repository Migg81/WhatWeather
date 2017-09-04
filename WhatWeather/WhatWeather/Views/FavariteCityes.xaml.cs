using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WhatWeather.Model;
using WhatWeather.ViewModel;
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
           // var favariteCity = new WhatWeather.ViewModel.FavariteCity();
            //this.BindingContext = favariteCity;
        }

        private void PopulateFavariteCity()
        {
            if (Application.Current.Properties.Keys.Contains("FavariteCityes"))
            {
                var cityDetails = (List<FavariteCityModel>)Application.Current.Properties["FavariteCityes"];
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

        //protected override void OnAppearing()
        //{
        //    PopulateFavariteCity();
        //}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //var selectedcity = this.t;
            // e.GetType().

           // var tt = this.SelectedItem;
           
            //await Navigation.PopToRootAsync();

          //  await Navigation.PushAsync(new WhatWeather.Views.(), false);
        }
    }
}

using WhatWeather.Model;
using WhatWeather.BackEndService;
using WhatWeather.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Linq;


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
                MyWeather.IsFavoriteCity = false;
                //Add to favarite list
                lblFavariteCityName.Text = MyWeather.CityWeather.DisplayName;
                Addtofavarite(MyWeather.CityWeather);

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

        private void Addtofavarite(City city)
        {
            var tempFavariteCity = new FavariteCity()
            {
                Name = city.Name,
                Tempurature = city.TodaysTemp,
                ImageUrl = city.Climate[0].WeatherIcon,
                Country = city.Country,
            };
            if (Application.Current.Properties.Keys.Contains("TempFavariteCity"))
            {
                Application.Current.Properties["TempFavariteCity"] = tempFavariteCity; 
            }
            else
            {
                Application.Current.Properties.Add("TempFavariteCity", tempFavariteCity);
            }
        }

        private void chkFavorite_Toggled(object sender, ToggledEventArgs e)
        {
            List<FavariteCity> favariteCityes;

            if (Application.Current.Properties.Keys.Contains("FavariteCityes"))
            {
                favariteCityes = (List<FavariteCity>)Application.Current.Properties["FavariteCityes"];
                ToggeledFavariteCityes(favariteCityes);
            }
            else
            {
                favariteCityes = new List<FavariteCity>();
                ToggeledFavariteCityes(favariteCityes);
            }
        }

        private void ToggeledFavariteCityes(List<FavariteCity> favariteCityes)
        {
            if (!chkFavorite.IsToggled)
            {
                var TempFavariteCity = (FavariteCity)Application.Current.Properties["TempFavariteCity"];
                var cityToberemoved = favariteCityes.FirstOrDefault(c => c.Name.Equals(TempFavariteCity.Name));
                favariteCityes.Remove(cityToberemoved);
            }
            else
            {
                var tempFavariteCity = (FavariteCity)Application.Current.Properties["TempFavariteCity"];
                favariteCityes.Add(tempFavariteCity);
                Application.Current.Properties["FavariteCityes"] = favariteCityes;
            }
        }
    }
}

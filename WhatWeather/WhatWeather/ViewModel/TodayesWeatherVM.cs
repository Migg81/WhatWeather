using System;
using System.Collections.Generic;
using System.ComponentModel;
using WhatWeather.BackEndService;
using WhatWeather.Model;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace WhatWeather.ViewModel
{
    public class TodayesWeatherVM: INotifyPropertyChanged
    {
        private string displayName;

        public string DisplayName
        {
            get { return Name + "," + Country; }
            set {
                displayName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
            }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set
            {
                country = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Country"));
            }
        }

        private Weather weathers;

        public Weather Weathers
        {
            get { return weathers; }
            set
            {
                weathers = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Weathers"));
            }
        }

        private string todaysTemp;

        public string TodaysTemp
        {
            get { return todaysTemp; }
            set
            {
                todaysTemp = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TodaysTemp"));
            }
        }

        private List<Weather> climate;

        public List<Weather> Climate
        {
            get { return climate; }
            set
            {
                climate = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Climate"));
            }
        }

        private decimal latitude;

        public decimal Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
            }
        }

        private decimal longitude;

        public decimal Longitude
        {
            get { return longitude; }
            set
            {
                longitude = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GetWeatherDetails()
        {
            //City CityWeatherDetails = await CoreService.GetWeather("94040", "us");
            if (Application.Current.Properties.ContainsKey("lat") && Application.Current.Properties.ContainsKey("lng"))
            {
                var lat = Application.Current.Properties["lat"];
                var lng = Application.Current.Properties["lng"];

                City CityWeatherDetails =  CoreService.GetWeatherByLatLng(Convert.ToDecimal(lat), Convert.ToDecimal(lng));

                this.DisplayName = CityWeatherDetails.DisplayName;
                this.Climate= CityWeatherDetails.Climate;
                this.Country= CityWeatherDetails.Country;
                this.Latitude = CityWeatherDetails.Latitude;
                this.Longitude = CityWeatherDetails.Longitude;
                this.Name = CityWeatherDetails.Name;
                this.TodaysTemp = CityWeatherDetails.TodaysTemp;
                this.Weathers = CityWeatherDetails.Weathers;
            }
            else
            {
                //to do
            }
        }
    }
}

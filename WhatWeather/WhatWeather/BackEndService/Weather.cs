using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatWeather.BackEndService
{
    public class Weather
    {
        public string Title { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string Displayname
        {
            get
            {
                return string.Format("{0}/{1}",this.MaxTemperature,this.MinTemperature);
            }
        }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Visibility { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string Date { get; set; }
        public string CurrentDate { get; set; }
        public string WeatherIcon { get; set; }

        public Weather()
        {
            //Because labels bind to these values, set them to an empty string to  
            //ensure that the label appears on all platforms by default.  
            this.Title = " ";
            this.MinTemperature = " ";
            this.MaxTemperature = " ";
            this.Wind = " ";
            this.Humidity = " ";
            this.Visibility = " ";
            this.Sunrise = " ";
            this.Sunset = " ";
            this.Date = " ";
            this.CurrentDate = " ";
            this.WeatherIcon = " ";
        }
    }
}

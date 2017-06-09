using System.Collections.Generic;

namespace WhatWeather.BackEndService
{
    public class City
    {
        /// <summary>
        /// To dispaly in Auto Search
        /// </summary>
        private string displayNamear;

        public string DisplayName
        {
            get { return Name +"," + Country; }
            set { displayNamear = value; }
        }

        public string Name { get; set; }
        public string Country { get; set; }
        public string TodaysTemp { get; set; }
        public List<Weather> Weathers { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public string WindDirection { get; set; }
        
    }
    
}

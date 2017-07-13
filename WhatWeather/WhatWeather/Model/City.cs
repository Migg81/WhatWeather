using System.Collections.Generic;

namespace WhatWeather.Model
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
        public Weather Weathers { get; set; }
        public string TodaysTemp { get; set; }
        public List<Weather> Climate { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }   
    }
    
}

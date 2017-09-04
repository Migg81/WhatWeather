using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatWeather.Model
{
    public class FavariteCityModel
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public string Tempurature { get; set; }
        
        public string ImageUrl { get; set; }

        public GeoLocation GeoLocation { get; set; }
    }
}

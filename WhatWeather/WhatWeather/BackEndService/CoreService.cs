using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace WhatWeather.BackEndService
{
    class CoreService
    {
        //Sign up for a free API key at http://openweathermap.org/appid  
        static string key = "112c89fceae302ca757e122806fa8115";
        static string apiWeatherUri = "http://api.openweathermap.org/data/2.5/forecast/daily?";
        public static async Task<City> GetWeather(string zipCode,string countryCode)
        {

            string queryString = apiWeatherUri + "zip=" + zipCode + ","+ countryCode + "&appid=" + key + "&units=imperial&cnt=3";

            //Make sure developers running this sample replaced the API key
            if (key == "YOUR API KEY HERE")
            {
                throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
            }

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            List<Weather> weathers = new List<Weather>();

            if (results["list"] != null)
            {
                var cityWeatherDetails = new City();
                cityWeatherDetails.Name = results["city"]["name"];
                cityWeatherDetails.Country = results["country"];
                cityWeatherDetails.Latitude = results["city"]["coord"]["lon"];
                cityWeatherDetails.Latitude = results["city"]["coord"]["lat"];
                cityWeatherDetails.TodaysTemp = (string)results["list"][0]["temp"]["day"] + " F";

                foreach (var result in results["list"])
                {
                    Weather weather = new Weather()
                    {
                        MinTemperature = (string)result["temp"]["min"] + " F",
                        MaxTemperature = (string)result["temp"]["max"] + " F",
                        Wind = (string)result["speed"] + " mph",
                        Humidity = (string)result["humidity"] + " %",
                        Visibility = (string)result["weather"][0]["main"]
                    };
                    DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                    weather.CurrentDate = time.AddSeconds((double)result["dt"]).ToString("MMMM dd, yyyy");
                   
                    cityWeatherDetails.Weathers = weathers;

                    var icon = "http://openweathermap.org/img/w/" + result["weather"][0]["icon"] + ".png";
                    weather.WeatherIcon = icon;

                    weathers.Add(weather);
                }          
                return cityWeatherDetails;
            }
            else
            {
                return null;
            }
        }

        public static async Task<City> GetWeatherByLatLng(decimal lat, decimal lon)
        {
            //api.openweathermap.org/data/2.5/forecast/daily?lat={lat}&lon={lon}&cnt={cnt}
            string queryString = apiWeatherUri + "lat=" + lat + "&" + "lon=" + lon + "&appid=" + key + "&units=imperial&cnt=3";

            //Make sure developers running this sample replaced the API key
            if (key == "YOUR API KEY HERE")
            {
                throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
            }

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            List<Weather> weathers = new List<Weather>();

            if (results["list"] != null)
            {
                var cityWeatherDetails = new City();
                cityWeatherDetails.Name = results["city"]["name"];
                cityWeatherDetails.Country = results["country"];
                cityWeatherDetails.Longitude = results["city"]["coord"]["lon"];
                cityWeatherDetails.Latitude = results["city"]["coord"]["lat"];
                cityWeatherDetails.TodaysTemp = (string)results["list"][0]["temp"]["day"] + " F";
                cityWeatherDetails.Wind = (string)results["list"][0]["speed"] + " mph";
                cityWeatherDetails.Humidity = (string)results["list"][0]["humidity"] + " %";
                cityWeatherDetails.Pressure= (string)results["list"][0]["pressure"] ;
                cityWeatherDetails.WindDirection = (string)results["list"][0]["deg"];

                foreach (var result in results["list"])
                {
                    Weather weather = new Weather()
                    {
                        MinTemperature = (string)result["temp"]["min"] + " F",
                        MaxTemperature = (string)result["temp"]["max"] + " F",
                        Visibility = (string)result["weather"][0]["main"]
                    };
                    DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                    weather.CurrentDate = time.AddSeconds((double)result["dt"]).ToString("MMMM dd, yyyy");

                    cityWeatherDetails.Weathers = weathers;

                    var icon = "http://openweathermap.org/img/w/" + result["weather"][0]["icon"] + ".png";
                    weather.WeatherIcon = icon;

                    weathers.Add(weather);
                }
                return cityWeatherDetails;
            }
            else
            {
                return null;
            }
        }

        public static ObservableCollection<City> GetCity(string cityName)
        {
            var cityApiUri1 = "http://api.geonames.org/searchJSON?q=" + cityName + "&maxRows=10&username=migg81";
            
            var client1 = new HttpClient();

            var response1 = client1.GetAsync(cityApiUri1).Result;

            dynamic results = null;
            if (response1 != null)
            {
                string json = response1.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject(json);
            }

            ObservableCollection<City> cityes = new ObservableCollection<City>();

            if (results["geonames"] != null)
            {
                foreach (var result in results["geonames"])
                {
                    City city = new City()
                    {
                        Country = (string)result["countryName"],
                        Latitude = (decimal)result["lat"],
                        Longitude = (decimal)result["lng"],
                        Name = (string)result["name"]
                    };
                    cityes.Add(city);
                }
            }
            return cityes;
        }
    }
}

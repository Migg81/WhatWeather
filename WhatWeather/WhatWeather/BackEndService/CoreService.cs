using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using WhatWeather.Model;

namespace WhatWeather.BackEndService
{
    class CoreService
    {
        //Sign up for a free API key at http://openweathermap.org/appid  
        static string key = "112c89fceae302ca757e122806fa8115";
        static string apiWeatherUri = "http://api.openweathermap.org/data/2.5/forecast/daily?";

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
        public static async Task<City> GetWeather(string zipCode,string countryCode)
        {

            string queryString = apiWeatherUri + "zip=" + zipCode + ","+ countryCode + "&appid=" + key + "&units=imperial&cnt=3";

            //Make sure developers running this sample replaced the API key
            if (key == "YOUR API KEY HERE")
            {
                throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
            }

            Task<dynamic> task = Task.Run<dynamic>(async () => await DataService.GetDataFromService(queryString));
            dynamic results = task.Result;

            //dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            List<Weather> weathers = new List<Weather>();

            if (results["list"] != null)
            {
                var cityWeatherDetails = new City()
                {
                    Name = results["city"]["name"],
                    Country = results["country"],
                    Latitude = results["city"]["coord"]["lon"]
                };
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
                   
                    cityWeatherDetails.Climate = weathers;

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

        public static City GetWeatherByLatLng(decimal lat, decimal lon)
        {
            try
            {
                //api.openweathermap.org/data/2.5/forecast/daily?lat={lat}&lon={lon}&cnt={cnt}
                string queryString = apiWeatherUri + "lat=" + lat + "&" + "lon=" + lon + "&appid=" + key + "&units=imperial&cnt=3";
                if (key == "YOUR API KEY HERE")
                {
                    throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
                }

                Task<dynamic> task = Task.Run<dynamic>(async () => await DataService.GetDataFromService(queryString));
                dynamic results = task.Result;


                //dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
                List<Weather> weathers = new List<Weather>();

                if (results["list"] != null)
                {
                    var cityWeatherDetails = new City();
                    cityWeatherDetails.Name = results["city"]["name"];
                    cityWeatherDetails.Country = results["country"];
                    cityWeatherDetails.Longitude = results["city"]["coord"]["lon"];
                    cityWeatherDetails.Latitude = results["city"]["coord"]["lat"];
                    cityWeatherDetails.TodaysTemp = (string)results["list"][0]["temp"]["day"] + " F";

                    Weather cityweather = new Weather()
                    {

                        Wind = (string)results["list"][0]["speed"] + " mph",
                        Humidity = (string)results["list"][0]["humidity"] + " %",
                        Pressure = (string)results["list"][0]["pressure"],
                        WindDirection = (string)results["list"][0]["deg"],
                    };

                    cityWeatherDetails.Weathers = cityweather;

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

                        cityWeatherDetails.Climate = weathers;

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
            catch (Exception ex )
            {

                throw ex;
            }
            
        }
        public static async Task<City> GetHistoricDataByStartAndEndDate(decimal lat, decimal lon,DateTime startDate, DateTime enddate)
        {
            var Sdate = (Int32)(DateTime.UtcNow.Subtract(startDate)).TotalSeconds;
            var Edate = (Int32)(DateTime.UtcNow.Subtract(enddate)).TotalSeconds;

            //http://history.openweathermap.org/data/2.5/history/city?lat={lat}&lon={lon}&type=hour&start={start}&end={end}
            string queryString = string.Format(@"http://history.openweathermap.org/data/2.5/history/city?lat={0}&lon={1}&type=hour&start={2}&end={3}&appid={4}",lat,lon, Sdate,Edate,key); 

            //Make sure developers running this sample replaced the API key
            if (key == "YOUR API KEY HERE")
            {
                throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
            }

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            List<Weather> weathers = new List<Weather>();
            return null;
        }
        public static City GetHistoricDataUpto16Dayes(decimal lat, decimal lon, int cnt)
        {
            var openWetherURI = string.Format("{0}lat={1}&lon={2}&cnt={3}&appid={4}", apiWeatherUri, lat, lon, cnt, key);
            if (key == "YOUR API KEY HERE")
            {
                throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
            }

            Task<dynamic> task = Task.Run<dynamic>(async () => await DataService.GetDataFromService(openWetherURI));
            dynamic results = task.Result;

            List<Weather> weathers = new List<Weather>();
            if (results["list"] != null)
            {
                var cityWeatherDetails = new City();
                cityWeatherDetails.Name = results["city"]["name"];
                cityWeatherDetails.Country = results["country"];
                cityWeatherDetails.Longitude = results["city"]["coord"]["lon"];
                cityWeatherDetails.Latitude = results["city"]["coord"]["lat"];
                cityWeatherDetails.TodaysTemp = (string)results["list"][0]["temp"]["day"] + " F";

                Weather cityweather = new Weather()
                {
                    Wind = (string)results["list"][0]["speed"] + " mph",
                    Humidity = (string)results["list"][0]["humidity"] + " %",
                    Pressure = (string)results["list"][0]["pressure"],
                    WindDirection = (string)results["list"][0]["deg"],
                };
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

                    cityWeatherDetails.Climate = weathers;

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

        public static City Get5day3HourForecastData(decimal lat, decimal lon)
        {
            try
            {
                var openWetherURI = string.Format("{0}lat={1}&lon={2}&appid={3}", "http://api.openweathermap.org/data/2.5/forecast?", lat, lon, key);
                if (key == "YOUR API KEY HERE")
                {
                    throw new ArgumentException("You must obtain an API key from openweathermap.org/appid and save it in the 'key' variable.");
                }

                Task<dynamic> task = Task.Run<dynamic>(async () => await DataService.GetDataFromService(openWetherURI));
                dynamic results = task.Result;

                List<Weather> weathers = new List<Weather>();
                if (results["list"] != null)
                {
                    var cityWeatherDetails = new City();
                    cityWeatherDetails.Name = results["city"]["name"];
                    cityWeatherDetails.Country = results["country"];
                    cityWeatherDetails.Longitude = results["city"]["coord"]["lon"];
                    cityWeatherDetails.Latitude = results["city"]["coord"]["lat"];
                    //cityWeatherDetails.TodaysTemp = (string)results["list"][0]["temp"]["day"] + " F";

                    Weather cityweather = new Weather()
                    {
                        Wind = (string)results["list"][0]["speed"] + " mph",
                        Humidity = (string)results["list"][0]["humidity"] + " %",
                        Pressure = (string)results["list"][0]["pressure"],
                        WindDirection = (string)results["list"][0]["deg"],
                    };
                    foreach (var result in results["list"])
                    {
                        Weather weather = new Weather()
                        {
                            MinTemperature = (string)result["main"]["temp_min"] + " F",
                            MaxTemperature = (string)result["main"]["temp_max"] + " F",
                            Visibility = (string)result["weather"][0]["main"],
                            CurrentDate= (string)result["dt_txt"]
                        };
                        //DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                        //weather.CurrentDate = time.AddSeconds((double)result["dt"]).ToString("MMMM dd, yyyy");

                        cityWeatherDetails.Climate = weathers;

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
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}

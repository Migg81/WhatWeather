namespace WhatWeather.Model
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
        public string Pressure { get; set; }
        public string WindDirection { get; set; }
    }
}

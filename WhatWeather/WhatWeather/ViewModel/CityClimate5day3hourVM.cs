using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WhatWeather.BackEndService;
using WhatWeather.Model;

namespace WhatWeather.ViewModel
{
    public class CityClimate5day3hourVM : INotifyPropertyChanged
    {
        public CityClimate5day3hourVM()
        {
            IsRecordAvailable = false;
            //climate.Weather = await CoreService.GetHistoricDataUpto16Dayes(climate.Latitude, climate.Longitude, climate.Cnt);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public List<Weather> climate;


        public List<Weather> Climate
        {
            get
            {
                //Get weather data for 7 dayes
                var cityWeather = CoreService.GetHistoricDataUpto16Dayes(Latitude, Longitude,7).Climate;
                if (cityWeather != null && cityWeather.Count > 0)
                {
                    IsRecordAvailable = true;
                }
                else
                {
                    IsRecordAvailable = false;
                }

                return cityWeather;
            }
            set
            {
                climate = value;
                OnPropertyChanged();
            }
        }

        private bool isRecordAvailable;

        public bool IsRecordAvailable
        {
            get { return isRecordAvailable; }
            set {
                isRecordAvailable = value;
                OnPropertyChanged();
            }
        }


        private decimal latitude;

        public decimal Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        private decimal longitude;

        public decimal Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
    }
}

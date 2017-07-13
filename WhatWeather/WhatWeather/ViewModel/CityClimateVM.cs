using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WhatWeather.BackEndService;
using WhatWeather.Model;

namespace WhatWeather.ViewModel
{
    public class CityClimateVM: INotifyPropertyChanged
    {
        public CityClimateVM()
        {
            //climate.Weather = await CoreService.GetHistoricDataUpto16Dayes(climate.Latitude, climate.Longitude, climate.Cnt);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private int cnt;

        public int Cnt
        {
            get { return cnt; }
            set
            {
                cnt = value;
                OnPropertyChanged();
            }
        }

        public List<Weather> climate;


        public List<Weather> Climate
        {
            get
            {
                var cityWeather = CoreService.GetHistoricDataUpto16Dayes(Latitude, Longitude, Cnt).Climate;
                return cityWeather;
            }
            set
            {
                climate = value;
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

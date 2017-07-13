using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WhatWeather.BackEndService;
using WhatWeather.Model;
using System;

namespace WhatWeather.ViewModel
{
    public class WeatherHistory: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool isCitylistVisibla;

        public bool IsCitylistVisibla
        {
            get { return isCitylistVisibla; }
            set
            {
                isCitylistVisibla = value;
                OnPropertyChanged();
            }
        }

        private City historicalCityWether;

        public City HistoricalCityWether
        {
            get { return historicalCityWether; }
            set
            {
                historicalCityWether = value;
                OnPropertyChanged();
            }
        }


        #region Command and associated methods for SearchCommand
        private Xamarin.Forms.Command _searchCommand;


        public System.Windows.Input.ICommand SearchCommand
        {
            get
            {
                _searchCommand = _searchCommand ?? new Xamarin.Forms.Command(DoSearchCommand, CanExecuteSearchCommand);
                return _searchCommand;
            }
        }
        private void DoSearchCommand()
        {
            // Refresh the list, which will automatically apply the search text
            // var coreService = new CoreService();
            Ctyes = new ObservableCollection<City>(CoreService.GetCity(searchText));
        }

        private bool CanExecuteSearchCommand()
        {
            return true;
        }

        private Xamarin.Forms.Command historicalDataCommand;


        public System.Windows.Input.ICommand HistoricalDataCommand
        {
            get
            {
                historicalDataCommand = historicalDataCommand ?? new Xamarin.Forms.Command(DoHistoricalDataCommand, CanExecuteHistoricalDataCommand);
                return historicalDataCommand;
            }
        }
        private void DoHistoricalDataCommand()
        {
            // Refresh the list, which will automatically apply the search text
            // var coreService = new CoreService();
            HistoricalCityWether = CoreService.GetHistoricDataByStartAndEndDate(Latitude,Longitude,Startdate,EndDate).Result;
        }

        private bool CanExecuteHistoricalDataCommand()
        {
            return true;
        }
        #endregion
        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value ?? string.Empty;

                    OnPropertyChanged("SearchText");

                    // Perform the search
                    if (SearchCommand.CanExecute(null))
                    {
                        SearchCommand.Execute(null);
                    }
                }
            }
        }

        private ObservableCollection<City> ctyes;

        public ObservableCollection<City> Ctyes
        {
            get { return ctyes; }
            set
            {
                ctyes = value;
                IsCitylistVisibla = true;
                OnPropertyChanged();
            }
        }

        private DateTime startDate;

        public DateTime Startdate
        {
            get
            {
                if (startDate == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return startDate;
                }
            }
            set
            {
                if (startDate == null)
                {
                    startDate = DateTime.Now;
                }
                else
                {
                    startDate = value;
                }
                OnPropertyChanged();
            }
        }

        private DateTime endDate;

        public DateTime EndDate
        {
            get
            {
                if (endDate == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return endDate;
                }
            }
            set
            {
                if(endDate==null)
                {
                    endDate = DateTime.Now;
                }
                else
                {
                    endDate = value;
                }
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

﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WhatWeather.BackEndService;
using WhatWeather.Model;
using Xamarin.Forms;

namespace WhatWeather.ViewModel
{
    public class SearchForecast : INotifyPropertyChanged
    {  
        private string searchText;
        private ObservableCollection<City> ctyCollection { get; set; }
        private City weather;

        private bool isCityListItemSelected;
        public bool IsCityListItemSelected
        {
            get { return isCityListItemSelected; }
            set
            {
                isCityListItemSelected = value;
                OnPropertyChanged("IsCityListItemSelected");
            }
        }
        private bool isCitylistVisibla;
        public bool IsCitylistVisibla {
            get { return isCitylistVisibla; }
            set
            {
                isCitylistVisibla = value;
                OnPropertyChanged("IsCitylistVisibla");
            }
        }

        
        private bool isFavoriteCity;
        public bool IsFavoriteCity
        {
            get { return isFavoriteCity; }
            set
            {
                isFavoriteCity = value;
                OnPropertyChanged("IsFavoriteCity");
            }
        }

        public City CityWeather
        {
            get { return weather; }
            set {
                weather = value;
                OnPropertyChanged("CityWeather");
            }
        }
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
                        this.IsCitylistVisibla = true;
                    }
                }
            }
        }


        public ObservableCollection<City> CtyCollection
        {
            get
            {
                return ctyCollection;
            }
            set
            {
                if (ctyCollection != value)
                {
                    ctyCollection = value;
                    OnPropertyChanged("CtyCollection");
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isRecordAvailable;

        public bool IsRecordAvailable
        {
            get { return isRecordAvailable; }
            set
            {
                isRecordAvailable = value;
                OnPropertyChanged();
            }
        }

        #region Command and associated methods for SearchCommand
        private Xamarin.Forms.Command _searchCommand;

        public event PropertyChangedEventHandler PropertyChanged;

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
            CtyCollection = new ObservableCollection<City>(CoreService.GetCity(searchText));
        }
        private bool CanExecuteSearchCommand()
        {
            return true;
        }
        #endregion

    }
}

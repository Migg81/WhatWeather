using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WhatWeather.Model;
using Xamarin.Forms;

namespace WhatWeather.ViewModel
{
    public class FavariteCity: INotifyPropertyChanged
    {
        //public FavariteCity()
        //{
        //    tapCommand = new Command(OnTapped);
        //}
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation _navigation;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    //private string displayName;

    //public string DisplayName
    //{
    //    get { return Name + "," + Country; }
    //    set { displayName = value; OnPropertyChanged("DisplayName"); }
    //}

    //private string name;

    //public string Name
    //{
    //    get { return name; }
    //    set {
    //        name = value;
    //        OnPropertyChanged("Name");
    //    }
    //}

    //private string country;

    //public string Country
    //{
    //    get { return country; }
    //    set {
    //        country = value;
    //        OnPropertyChanged("Country");
    //    }
    //}

    //private string tempurature;

    //public string Tempurature
    //{
    //    get { return tempurature; }
    //    set
    //    {
    //        tempurature = value;
    //        OnPropertyChanged("Tempurature");
    //    }
    //}

    //private string imageUrl;
    //public string ImageUrl
    //{
    //    get { return imageUrl; }
    //    set
    //    {
    //        imageUrl = value;
    //        OnPropertyChanged("ImageUrl");
    //    }
    //}

    public List<FavariteCityModel> myFavariteCityModel;

        public List<FavariteCityModel> FavariteCityes
        {
            get { return myFavariteCityModel; }
            set {
                myFavariteCityModel = value;
                OnPropertyChanged("FavariteCityes");
            }
        }


        #region command
        //public GeoLocation GeoLocation { get; set; }


        //ICommand tapCommand;


        //public ICommand TapCommand
        //{
        //    get { return tapCommand; }
        //}
        //void OnTapped()
        //{

        //    //Application.Current.Properties["lat"] = GeoLocation.Latitude;
        //    //Application.Current.Properties["lng"] = GeoLocation.Longitude;
        //}

        private Xamarin.Forms.Command favariteCityTapped;


        public System.Windows.Input.ICommand FavariteCityTapped
        {
            get
            {
                favariteCityTapped = favariteCityTapped ?? new Xamarin.Forms.Command(DoTappedCommandAsync, CanExecuteTappedCommand);
                return favariteCityTapped;
            }
        }
        private void DoTappedCommandAsync(object geoloc)
        {
            // Refresh the list, which will automatically apply the search text
            if (geoloc == null)
                return;
            var gelocation = (GeoLocation)geoloc;

            Application.Current.Properties["lat"] = gelocation.Latitude;
            Application.Current.Properties["lng"] = gelocation.Longitude;
            Application.Current.MainPage = new NavigationPage(new WhatWeather.WetaherTabbed());
        }

        private bool CanExecuteTappedCommand(object geoloc)
        {
            return true;
        }

        #endregion
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WhatWeather.Model;
using Xamarin.Forms;

namespace WhatWeather.ViewModel
{
    public class FavariteCity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<FavariteCityModel> myFavariteCityModel;

        public ObservableCollection<FavariteCityModel> FavariteCityes
        {
            get { return myFavariteCityModel; }
            set
            {
                myFavariteCityModel = value;
                OnPropertyChanged("FavariteCityes");
            }
        }

        #region command

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

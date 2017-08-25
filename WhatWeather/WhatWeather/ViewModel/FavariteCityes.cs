using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WhatWeather.ViewModel
{
    public class FavariteCity: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string displayName;

        public string DisplayName
        {
            get { return Name + "," + Country; }
            set { displayName = value; OnPropertyChanged("DisplayName"); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string country;

        public string Country
        {
            get { return country; }
            set {
                country = value;
                OnPropertyChanged("Country");
            }
        }

        private string tempurature;

        public string Tempurature
        {
            get { return tempurature; }
            set
            {
                tempurature = value;
                OnPropertyChanged("Tempurature");
            }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }
    }
}

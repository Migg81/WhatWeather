using System;

using Xamarin.Forms;

namespace WhatWeather.UserControl
{
    public partial class LVWeatherForecast : ListView
    {
        public LVWeatherForecast()
        {
            InitializeComponent();
        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {
            //var selected = (ViewCell)(sender);
            //if (selected.View.BackgroundColor.Equals(Color.FromHex("#af4448")))
            //{
            //    selected.View.BackgroundColor = Color.Transparent;
            //}
            //else
            //{
            //    selected.View.BackgroundColor = Color.FromHex("#af4448");
            //}

        }
    }
}

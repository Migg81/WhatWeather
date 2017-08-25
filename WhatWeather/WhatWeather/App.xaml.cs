using WhatWeather.Views;
using Xamarin.Forms;

namespace WhatWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new WhatWeather.MainPage();
             MainPage = new NavigationPage(new WhatWeather.WetaherTabbed());
            //MainPage = new NavigationPage(new FavariteCityes());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

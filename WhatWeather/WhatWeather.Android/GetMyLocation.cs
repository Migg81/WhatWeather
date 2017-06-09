using System;
using Android.Content;
using Android.OS;
using Android.Runtime;
using WhatWeather.Droid;
using WhatWeather.NativeService;
using Android.Locations;

[assembly: Xamarin.Forms.Dependency(typeof(GetMyLocation))]
namespace WhatWeather.Droid
{

    public class GetMyLocation : Java.Lang.Object, IMyLocation, ILocationListener
    {

        public GetMyLocation() { }

        public double lat { get; set; }
        public double lng { get ; set ; }

        LocationManager locMgr;

        public event EventHandler<ILocationEventArgs> LocationObtained;

        public void ObtainMyLocation()
        {
            locMgr = Android.App.Application.Context.GetSystemService(Context.LocationService) as LocationManager;
            Criteria locationCriteria = new Criteria();

            locationCriteria.Accuracy = Accuracy.Coarse;
            locationCriteria.PowerRequirement = Power.Medium;

            var locationProvider = locMgr.GetBestProvider(locationCriteria, true);

            if (locMgr.IsProviderEnabled(locationProvider))
            {
                locMgr.RequestLocationUpdates(locationProvider, 2000, 1, this);
            }
        }

        public void StopLocationUpdates()
        {
            //base.OnPause();
            locMgr.RemoveUpdates(this);
        }
        public void OnLocationChanged(Location location)
        {
            LocationEventArgs args = new LocationEventArgs()
            {
                lat = location.Latitude,
                lng = location.Longitude
            };

            LocationObtained(this, args);
        }

        public void OnProviderDisabled(string provider)
        {
            //To Do
        }

        public void OnProviderEnabled(string provider)
        {
            //To Do
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //To Do
        }

    }
}
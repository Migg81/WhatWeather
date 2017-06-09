using System;

namespace WhatWeather.NativeService
{
    public interface IMyLocation
    {
        void ObtainMyLocation();
        event EventHandler<ILocationEventArgs> LocationObtained;
        void StopLocationUpdates();
    }
}

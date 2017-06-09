using System;

namespace WhatWeather.NativeService
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}

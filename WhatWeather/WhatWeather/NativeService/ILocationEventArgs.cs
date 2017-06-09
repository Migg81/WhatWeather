namespace WhatWeather.NativeService
{
    public interface ILocationEventArgs
    {
        double lat { get; set; }
        double lng { get; set; }
    }
}

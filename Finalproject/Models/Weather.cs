using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{

    public class DarkSky
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string timezone { get; set; }
        public Currently currently { get; set; }
        public Minutely minutely { get; set; }
        public Hourly hourly { get; set; }
        public Daily daily { get; set; }
        public Flags flags { get; set; }
        public decimal offset { get; set; }
    }

    public class Currently
    {
        public decimal time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public decimal nearestStormDistance { get; set; }
        public decimal nearestStormBearing { get; set; }
        public decimal precipIntensity { get; set; }
        public decimal precipProbability { get; set; }
        public decimal temperature { get; set; }
        public decimal apparentTemperature { get; set; }
        public decimal dewPoint { get; set; }
        public decimal humidity { get; set; }
        public decimal pressure { get; set; }
        public decimal windSpeed { get; set; }
        public decimal windGust { get; set; }
        public decimal windBearing { get; set; }
        public decimal cloudCover { get; set; }
        public decimal uvIndex { get; set; }
        public decimal visibility { get; set; }
        public decimal ozone { get; set; }
    }

    public class Minutely
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public DSDatum[] data { get; set; }
    }

    public class DSDatum
    {
        public decimal time { get; set; }
        public decimal precipIntensity { get; set; }
        public decimal precipProbability { get; set; }
    }

    public class Hourly
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public DSDatum1[] data { get; set; }
    }

    public class DSDatum1
    {
        public decimal time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public decimal precipIntensity { get; set; }
        public decimal precipProbability { get; set; }
        public decimal temperature { get; set; }
        public decimal apparentTemperature { get; set; }
        public decimal dewPoint { get; set; }
        public decimal humidity { get; set; }
        public decimal pressure { get; set; }
        public decimal windSpeed { get; set; }
        public decimal windGust { get; set; }
        public decimal windBearing { get; set; }
        public decimal cloudCover { get; set; }
        public decimal uvIndex { get; set; }
        public decimal visibility { get; set; }
        public decimal ozone { get; set; }
        public string precipType { get; set; }
    }

    public class Daily
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public Datum2[] data { get; set; }
    }

    public class Datum2
    {
        public decimal time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public decimal sunriseTime { get; set; }
        public decimal sunsetTime { get; set; }
        public decimal moonPhase { get; set; }
        public decimal precipIntensity { get; set; }
        public decimal precipIntensityMax { get; set; }
        public decimal precipIntensityMaxTime { get; set; }
        public decimal precipProbability { get; set; }
        public string precipType { get; set; }
        public decimal temperatureHigh { get; set; }
        public decimal temperatureHighTime { get; set; }
        public decimal temperatureLow { get; set; }
        public decimal temperatureLowTime { get; set; }
        public decimal apparentTemperatureHigh { get; set; }
        public decimal apparentTemperatureHighTime { get; set; }
        public decimal apparentTemperatureLow { get; set; }
        public decimal apparentTemperatureLowTime { get; set; }
        public decimal dewPoint { get; set; }
        public decimal humidity { get; set; }
        public decimal pressure { get; set; }
        public decimal windSpeed { get; set; }
        public decimal windGust { get; set; }
        public decimal windGustTime { get; set; }
        public decimal windBearing { get; set; }
        public decimal cloudCover { get; set; }
        public decimal uvIndex { get; set; }
        public decimal uvIndexTime { get; set; }
        public string visibility { get; set; }
        public decimal ozone { get; set; }
        public decimal temperatureMin { get; set; }
        public decimal temperatureMinTime { get; set; }
        public decimal temperatureMax { get; set; }
        public decimal temperatureMaxTime { get; set; }
        public decimal apparentTemperatureMin { get; set; }
        public decimal apparentTemperatureMinTime { get; set; }
        public decimal apparentTemperatureMax { get; set; }
        public decimal apparentTemperatureMaxTime { get; set; }
    }

    public class Flags
    {
        public string[] sources { get; set; }
        public decimal neareststation { get; set; }
        public string units { get; set; }
    }

}

using Hedfan.Schedules.Airports;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public static class ExampleAirports
    {
        public static Airport LondonLuton { get; } = new Airport
        {
            Name = "London Luton Airport",
            City = "London",
            Country = "United Kingdom",
            Iata = "LTN",
            Icao = "EGGW",
            Latitude = 51.874698638916016,
            Longitude = -0.36833301186561584,
            Altitude = 526,
            Timezone = "Europe/London",
            Source = "OurAirports"
        };

        public static Airport WinnipegStAndrews { get; } = new Airport
        {
            Name = "Winnipeg / St. Andrews Airport",
            City = "Winnipeg",
            Country = "Canada",
            Iata = null,
            Icao = "CYAV",
            Latitude = 50.0564002991,
            Longitude = -97.03250122070001,
            Altitude = 760,
            Timezone = "America/Winnipeg",
            Source = "OurAirports"
        };
    }
}
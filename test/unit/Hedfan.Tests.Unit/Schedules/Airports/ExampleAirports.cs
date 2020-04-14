using Hedfan.Schedules.Airports;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public static class ExampleAirports
    {
        public static Airport LondonLuton { get; } = new Airport(new AirportBuilder
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
        });
    }
}
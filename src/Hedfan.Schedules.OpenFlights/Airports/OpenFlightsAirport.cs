using CsvHelper.Configuration.Attributes;

namespace Hedfan.Schedules.Airports
{
    internal class OpenFlightsAirport
    {
        [Index(8)]
        public int Altitude { get; set; }

        [Index(2)]
        public string City { get; set; }

        [Index(3)]
        public string Country { get; set; }

        [Index(4)]
        public string Iata { get; set; }

        [Index(5)]
        public string Icao { get; set; }

        [Index(6)]
        public double Latitude { get; set; }

        [Index(7)]
        public double Longitude { get; set; }

        [Index(1)]
        public string Name { get; set; }

        [Index(13)]
        public string Source { get; set; }

        [Index(11)]
        public string Timezone { get; set; }
    }
}
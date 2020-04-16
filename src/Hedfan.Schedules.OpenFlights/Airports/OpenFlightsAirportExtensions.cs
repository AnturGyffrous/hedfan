using System.Linq;

namespace Hedfan.Schedules.Airports
{
    internal static class OpenFlightsAirportExtensions
    {
        internal static Airport ToAirport(this OpenFlightsAirport openFlightsAirport) =>
            new Airport
            {
                Name = openFlightsAirport.Name,
                City = openFlightsAirport.City.NullIf("\\N", string.Empty),
                Country = openFlightsAirport.Country,
                Iata = openFlightsAirport.Iata.NullIf("\\N", string.Empty),
                Icao = openFlightsAirport.Icao,
                Latitude = openFlightsAirport.Latitude,
                Longitude = openFlightsAirport.Longitude,
                Altitude = openFlightsAirport.Altitude,
                Timezone = openFlightsAirport.Timezone.NullIf("\\N", string.Empty),
                Source = openFlightsAirport.Source
            };

        internal static string NullIf(this string value, params string[] matches) => matches.Contains(value) ? null : value;
    }
}
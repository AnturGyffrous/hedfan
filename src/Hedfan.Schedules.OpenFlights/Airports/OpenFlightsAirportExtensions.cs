namespace Hedfan.Schedules.Airports
{
    internal static class OpenFlightsAirportExtensions
    {
        internal static Airport ToAirport(this OpenFlightsAirport openFlightsAirport) =>
            new Airport(new AirportBuilder
            {
                Name = openFlightsAirport.Name,
                City = openFlightsAirport.City,
                Country = openFlightsAirport.Country,
                Iata = openFlightsAirport.Iata,
                Icao = openFlightsAirport.Icao,
                Latitude = openFlightsAirport.Latitude,
                Longitude = openFlightsAirport.Longitude,
                Altitude = openFlightsAirport.Altitude,
                Timezone = openFlightsAirport.Timezone,
                Source = openFlightsAirport.Source
            });
    }
}
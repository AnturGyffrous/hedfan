using System.Threading.Tasks;

namespace Hedfan.Schedules.Airports
{
    public class OpenFlightsAirportStore : IAirportStore
    {
        public Task<Airport> FindByIataAsync(string iata) => throw new System.NotImplementedException();
    }
}

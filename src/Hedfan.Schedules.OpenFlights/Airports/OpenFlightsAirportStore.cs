using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Airports
{
    public class OpenFlightsAirportStore : IAirportStore
    {
        private readonly IEnumerable<Airport> _airports;

        public OpenFlightsAirportStore(IEnumerable<Airport> airports)
        {
            _airports = airports;
        }

        public Task<Airport> FindByIataAsync(string iata, CancellationToken cancellationToken = new CancellationToken()) =>
            Task.FromResult(_airports.FirstOrDefault(x => x.Iata == iata));

        public Task<Airport> FindByIcaoAsync(string icao, CancellationToken cancellationToken = new CancellationToken()) =>
            Task.FromResult(_airports.FirstOrDefault(x => x.Icao == icao));
    }
}
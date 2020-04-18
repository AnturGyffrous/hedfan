using System.Threading;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Airports
{
    public interface IAirportStore
    {
        Task<Airport> FindByIataAsync(string iata, CancellationToken cancellationToken = new CancellationToken());

        Task<Airport> FindByIcaoAsync(string icao, CancellationToken cancellationToken = new CancellationToken());
    }
}
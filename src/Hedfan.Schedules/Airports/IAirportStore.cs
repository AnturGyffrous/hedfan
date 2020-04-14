using System.Threading.Tasks;

namespace Hedfan.Schedules.Airports
{
    public interface IAirportStore
    {
        Task<Airport> FindByIataAsync(string iata);

        Task<Airport> FindByIcaoAsync(string icao);
    }
}
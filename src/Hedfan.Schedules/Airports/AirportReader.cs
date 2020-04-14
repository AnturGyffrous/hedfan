using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Airports
{
    public abstract class AirportReader : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        public abstract Airport GetAirport();

        public abstract Task<Airport> GetAirportAsync();

        public abstract IEnumerable<Airport> GetAirports();

        public abstract IAsyncEnumerable<Airport> GetAirportsAsync();

        public abstract bool Read();

        public abstract Task<bool> ReadAsync();
    }
}
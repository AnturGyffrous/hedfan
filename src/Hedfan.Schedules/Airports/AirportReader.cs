using System;
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

        public abstract bool Read();

        public abstract Task<bool> ReadAsync();
    }
}
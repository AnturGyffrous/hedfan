using System;

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

        public abstract bool Read();
    }
}
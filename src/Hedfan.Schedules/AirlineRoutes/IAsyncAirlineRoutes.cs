using System.Collections.Generic;

namespace Hedfan.Schedules.AirlineRoutes
{
    public interface IAsyncAirlineRoutes : IEnumerable<AirlineRoute>, IAsyncEnumerable<AirlineRoute>
    {
    }
}
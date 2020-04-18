using Hedfan.Schedules.Airports;

namespace Hedfan.Schedules.AirlineRoutes
{
    public class AirlineRoute
    {
        public Airport Destination { get; set; }

        public Airport Origin { get; set; }
    }
}
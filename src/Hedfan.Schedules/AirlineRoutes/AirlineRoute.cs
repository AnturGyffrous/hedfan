using Hedfan.Schedules.Airlines;
using Hedfan.Schedules.Airports;

namespace Hedfan.Schedules.AirlineRoutes
{
    public class AirlineRoute
    {
        public Airline Airline { get; set; }

        public Airport Destination { get; set; }

        public Airport Origin { get; set; }
    }
}
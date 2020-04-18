using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Hedfan.Schedules.Airports;
using Hedfan.Schedules.Properties;

using Newtonsoft.Json;

namespace Hedfan.Schedules.AirlineRoutes
{
    public class EasyJetRoutes : IAirlineRoutes
    {
        private readonly IEnumerable<AirlineRoute> _routes;

        public EasyJetRoutes(IAirportStore airportStore)
        {
            _routes = JsonConvert.DeserializeObject<List<EasyJetOriginAirport>>(Resources.EasyJetRouteData).SelectMany(x => x.ConnectedTo, (o, d) => new AirlineRoute
            {
                Origin = airportStore.FindByIataAsync(o.Iata).GetAwaiter().GetResult(),
                Destination = airportStore.FindByIataAsync(d.Iata).GetAwaiter().GetResult()
            });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<AirlineRoute> GetEnumerator() => _routes.GetEnumerator();
    }
}
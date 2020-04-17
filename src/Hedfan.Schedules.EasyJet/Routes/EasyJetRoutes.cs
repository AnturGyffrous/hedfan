using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Hedfan.Schedules.Airports;
using Hedfan.Schedules.Properties;

using Newtonsoft.Json;

namespace Hedfan.Schedules.Routes
{
    public class EasyJetRoutes : IRoutes
    {
        private readonly IEnumerable<Route> _routes;

        public EasyJetRoutes(IAirportStore airportStore)
        {
            _routes = JsonConvert.DeserializeObject<List<EasyJetOriginAirport>>(Resources.EasyJetRouteData).SelectMany(x => x.ConnectedTo, (o, d) => new Route
            {
                Origin = airportStore.FindByIataAsync(o.Iata).GetAwaiter().GetResult(),
                Destination = airportStore.FindByIataAsync(d.Iata).GetAwaiter().GetResult()
            });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Route> GetEnumerator() => _routes.GetEnumerator();
    }
}
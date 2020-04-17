using System.Collections;
using System.Collections.Generic;

using Hedfan.Schedules.Airports;
using Hedfan.Schedules.Properties;

using Newtonsoft.Json;

namespace Hedfan.Schedules.Routes
{
    public class EasyJetRoutes : IRoutes
    {
        public EasyJetRoutes()
        {
            var routes = JsonConvert.DeserializeObject<List<EasyJetOriginAirport>>(Resources.EasyJetRouteData);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Route> GetEnumerator() => throw new System.NotImplementedException();
    }
}
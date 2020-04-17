using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Hedfan.Schedules.Routes
{
    public class EasyJetRoutes : IRoutes
    {
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Route> GetEnumerator() => throw new System.NotImplementedException();
    }
}
using System.Collections;
using System.Collections.Generic;

namespace Hedfan.Schedules.Routes
{
    public class EasyJetRoutes : IRoutes
    {
        public IEnumerator<Route> GetEnumerator() => throw new System.NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
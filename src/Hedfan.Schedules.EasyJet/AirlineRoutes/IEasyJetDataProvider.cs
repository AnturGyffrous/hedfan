using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hedfan.Schedules.AirlineRoutes
{
    public interface IEasyJetDataProvider
    {
        Task<IEnumerable<AirlineRoute>> GetRoutesAsync(HttpClient httpClient);
    }
}
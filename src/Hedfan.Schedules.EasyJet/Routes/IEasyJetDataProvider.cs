using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Routes
{
    public interface IEasyJetDataProvider
    {
        Task<IEnumerable<Route>> GetRoutesAsync(HttpClient httpClient);
    }
}
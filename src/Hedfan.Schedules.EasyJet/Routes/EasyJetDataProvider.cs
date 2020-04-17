using System.Net.Http;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Routes
{
    public class EasyJetDataProvider : IEasyJetDataProvider
    {
        public Task<Route> GetRoutesAsync(HttpClient httpClient) => throw new System.NotImplementedException();
    }
}
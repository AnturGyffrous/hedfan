using System.Net.Http;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Routes
{
    public class EasyJetDataProvider : IEasyJetDataProvider
    {
        public async Task<Route> GetRoutesAsync(HttpClient httpClient)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.easyjet.com/en/cheap-flights/timetables");

            var response = await httpClient.SendAsync(request);
        }
    }
}
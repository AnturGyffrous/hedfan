using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Airports
{
    public static class OpenFlightsAirportDownloader
    {
        public static async Task<Stream> Download(HttpClient httpClient)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://raw.githubusercontent.com/jpatokal/openflights/master/data/airports.dat");

            var response = await httpClient.SendAsync(request);

            return await response.Content.ReadAsStreamAsync();
        }
    }
}
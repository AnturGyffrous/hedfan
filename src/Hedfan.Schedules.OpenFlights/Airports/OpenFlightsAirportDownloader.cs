using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hedfan.Schedules.Airports
{
    public static class OpenFlightsAirportDownloader
    {
        public static async Task<Stream> Download(HttpClient httpClient)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://raw.githubusercontent.com/jpatokal/openflights/master/data/airports.dat");
            if (!httpClient.DefaultRequestHeaders.UserAgent.Any())
            {
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(
                    typeof(OpenFlightsAirportDownloader).FullName,
                    typeof(OpenFlightsAirportDownloader).Assembly.GetName().Version.ToString()));
            }

            var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync();
        }
    }
}
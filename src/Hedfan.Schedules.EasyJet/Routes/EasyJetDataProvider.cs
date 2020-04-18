using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Hedfan.Schedules.Airports;

using HtmlAgilityPack;

using Newtonsoft.Json;

namespace Hedfan.Schedules.Routes
{
    public class EasyJetDataProvider : IEasyJetDataProvider
    {
        private static readonly Regex RouteArrayJsonRegex = new Regex("\\[\\{.*\"Iata\".*:.*\\}\\]", RegexOptions.Compiled);

        private readonly IAirportStore _airportStore;

        public EasyJetDataProvider(IAirportStore airportStore)
        {
            _airportStore = airportStore;
        }

        public async Task<IEnumerable<Route>> GetRoutesAsync(HttpClient httpClient)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.easyjet.com/en/cheap-flights/timetables");

            var response = await httpClient.SendAsync(request);

            var doc = new HtmlDocument();
            doc.LoadHtml(await response.Content.ReadAsStringAsync());
            var routeArrayJson = doc.DocumentNode
                .SelectNodes("//script")
                .Where(x => !string.IsNullOrEmpty(x.InnerText))
                .Select(x => RouteArrayJsonRegex.Match(x.InnerText))
                .Where(x => x.Success)
                .Select(x => x.Value)
                .FirstOrDefault();

            return JsonConvert.DeserializeObject<List<EasyJetOriginAirport>>(routeArrayJson)
                .SelectMany(x => x.ConnectedTo, (origin, destination) => new Route
                {
                    Origin = _airportStore.FindByIataAsync(origin.Iata).GetAwaiter().GetResult(),
                    Destination = _airportStore.FindByIataAsync(destination.Iata).GetAwaiter().GetResult()
                });
        }
    }
}
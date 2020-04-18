using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;

using Hedfan.Schedules.Airlines;
using Hedfan.Schedules.Airports;

using HtmlAgilityPack;

using Newtonsoft.Json;

namespace Hedfan.Schedules.AirlineRoutes
{
    public class EasyJetRoutePicker : IAsyncAirlineRoutes
    {
        private static readonly Airline EasyJet = new Airline { Name = "easyJet", CompanyName = "EasyJet UK Limited", Iata = "U2", Icao = "EZY", Callsign = "EASY" };

        private static readonly Regex RouteArrayJsonRegex = new Regex("\\[\\{.*\"Iata\".*:.*\\}\\]", RegexOptions.Compiled);

        private readonly IAirportStore _airportStore;

        private readonly HttpClient _httpClient;

        public EasyJetRoutePicker(IAirportStore airportStore, HttpClient httpClient)
        {
            _airportStore = airportStore;
            _httpClient = httpClient;
        }

        public async IAsyncEnumerator<AirlineRoute> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.easyjet.com/en/cheap-flights/timetables");

            var response = await _httpClient.SendAsync(request, cancellationToken);

            var doc = new HtmlDocument();
            doc.LoadHtml(await response.Content.ReadAsStringAsync());
            var routeArrayJson = doc.DocumentNode
                .SelectNodes("//script")
                .Where(x => !string.IsNullOrEmpty(x.InnerText))
                .Select(x => RouteArrayJsonRegex.Match(x.InnerText))
                .Where(x => x.Success)
                .Select(x => x.Value)
                .FirstOrDefault();

            foreach (var route in JsonConvert.DeserializeObject<List<EasyJetOriginAirport>>(routeArrayJson)
                .SelectMany(x => x.ConnectedTo, (o, d) => new { Origin = o, Destination = d }))
            {
                yield return new AirlineRoute
                {
                    Airline = EasyJet,
                    Origin = await _airportStore.FindByIataAsync(route.Origin.Iata),
                    Destination = await _airportStore.FindByIataAsync(route.Destination.Iata)
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<AirlineRoute> GetEnumerator()
        {
            var asyncEnumerator = GetAsyncEnumerator();

            try
            {
                while (asyncEnumerator.MoveNextAsync().GetAwaiter().GetResult())
                {
                    yield return asyncEnumerator.Current;
                }
            }
            finally
            {
                asyncEnumerator.DisposeAsync().GetAwaiter().GetResult();
            }
        }
    }
}
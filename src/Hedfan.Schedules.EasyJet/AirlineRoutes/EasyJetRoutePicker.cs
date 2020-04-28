using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;

using Hedfan.Schedules.Airlines;
using Hedfan.Schedules.Airports;

using HtmlAgilityPack;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Hedfan.Schedules.AirlineRoutes
{
    public class EasyJetRoutePicker : IAirlineRoutes
    {
        private static readonly Airline EasyJet = new Airline { Name = "easyJet", CompanyName = "EasyJet UK Limited", Iata = "U2", Icao = "EZY", Callsign = "EASY" };

        private static readonly Regex RouteArrayJsonRegex = new Regex("\\[\\{.*\"Iata\".*:.*\\}\\]", RegexOptions.Compiled);

        private readonly IAirportStore _airportStore;

        private readonly HttpClient _httpClient;

        private readonly ILogger _logger;

        public EasyJetRoutePicker(IAirportStore airportStore, HttpClient httpClient, ILogger<EasyJetRoutePicker> logger)
        {
            _airportStore = airportStore;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async IAsyncEnumerator<AirlineRoute> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://www.easyjet.com/en/cheap-flights/timetables");
            if (!_httpClient.DefaultRequestHeaders.UserAgent.Any())
            {
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue(
                    typeof(EasyJetRoutePicker).FullName,
                    typeof(EasyJetRoutePicker).Assembly.GetName().Version.ToString()));
            }
            
            var response = await _httpClient.SendAsync(request, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

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
                cancellationToken.ThrowIfCancellationRequested();
                var origin = await _airportStore.FindByIataAsync(route.Origin.Iata, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                if (origin == null)
                {
                    _logger.LogWarning($"Unable to find an airport with the IATA {route.Origin.Iata} for the route {route.Origin.Iata}->{route.Destination.Iata}.");
                }

                var destination = await _airportStore.FindByIataAsync(route.Destination.Iata, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                if (destination == null)
                {
                    _logger.LogWarning($"Unable to find an airport with the IATA {route.Destination.Iata} for the route {route.Origin.Iata}->{route.Destination.Iata}.");
                }

                yield return new AirlineRoute
                {
                    Airline = EasyJet,
                    Origin = origin,
                    Destination = destination
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
using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;

using Hedfan.Schedules.AirlineRoutes;
using Hedfan.Schedules.Airports;

using Xunit;
using Xunit.Abstractions;

namespace Hedfan.Tests.Integration
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Test1()
        {
            using var httpClient = new HttpClient();
            AirportReader reader = new OpenFlightsAirportReader(await OpenFlightsAirportDownloader.Download(httpClient));
            IAirportStore airportStore = new OpenFlightsAirportStore(reader.GetAirports());

            IAirlineRoutes easyJetRoutes = new EasyJetRoutePicker(airportStore, httpClient, _output.BuildLoggerFor<EasyJetRoutePicker>());
            AirlineRoute result = null;
            await foreach (var route in easyJetRoutes)
            {
                if (route.Origin?.Iata == "LTN" && route.Destination?.Iata == "EDI")
                {
                    result = route;
                }
            }

            result.Should().NotBeNull();
            result?.Airline.Icao.Should().Be("EZY");
            result?.Origin.Icao.Should().Be("EGGW");
            result?.Destination.Icao.Should().Be("EGPH");
        }
    }
}
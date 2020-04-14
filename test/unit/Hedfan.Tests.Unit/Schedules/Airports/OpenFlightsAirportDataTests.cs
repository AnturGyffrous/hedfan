using System.Collections.Generic;
using System.Threading.Tasks;

using AutoFixture;

using FluentAssertions;

using Hedfan.Schedules.Airports;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class OpenFlightsAirportDataTests
    {
        public OpenFlightsAirportDataTests()
        {
            _fixture = new Fixture();

            _fixture.Inject<IEnumerable<Airport>>(_fixture.Create<OpenFlightsAirportData>());

            _fixture.Register<IAirportStore>(() => _fixture.Create<OpenFlightsAirportStore>());
        }

        private readonly IFixture _fixture;

        [Fact]
        public async Task OpenFlightsAirportDataShouldLoadAllAirports()
        {
            // Arrange
            var airportStore = _fixture.Create<IAirportStore>();

            // Act
            var result = await airportStore.FindByIcaoAsync("YNGU");

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Ngukurr Airport");
            result.Country.Should().Be("Australia");
            result.Iata.Should().Be("RPM");
            result.Latitude.Should().Be(-14.722800254821777);
            result.Longitude.Should().Be(134.7469940185547);
            result.Altitude.Should().Be(45);
        }
    }
}
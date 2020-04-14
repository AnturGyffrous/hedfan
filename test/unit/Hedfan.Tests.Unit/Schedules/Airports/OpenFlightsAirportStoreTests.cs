using System.Collections.Generic;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;

using FluentAssertions;

using Hedfan.Schedules.Airports;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class OpenFlightsAirportStoreTests
    {
        public OpenFlightsAirportStoreTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Inject<IEnumerable<Airport>>(new[] { LondonLutonAirport });

            _fixture.Register<IAirportStore>(() => _fixture.Create<OpenFlightsAirportStore>());
        }

        private static readonly Airport LondonLutonAirport = new Airport { Iata = "LTN", Icao = "EGGW" };

        private readonly IFixture _fixture;

        [Fact]
        public async Task FindByIataAsyncShouldReturnAirport()
        {
            // Arrange
            var airportStore = _fixture.Create<IAirportStore>();

            // Act
            var airport = await airportStore.FindByIataAsync(LondonLutonAirport.Iata);

            // Assert
            airport.Should().Be(LondonLutonAirport);
        }

        [Fact]
        public async Task FindByIataAsyncShouldReturnNullWhenNoMatchingAirportCanBeFound()
        {
            // Arrange
            var airportStore = _fixture.Create<IAirportStore>();

            // Act
            var airport = await airportStore.FindByIataAsync(_fixture.Create<string>());

            // Assert
            airport.Should().BeNull();
        }

        [Fact]
        public async Task FindByIcaoAsyncShouldReturnAirport()
        {
            // Arrange
            var airportStore = _fixture.Create<IAirportStore>();

            // Act
            var airport = await airportStore.FindByIcaoAsync(LondonLutonAirport.Icao);

            // Assert
            airport.Should().Be(LondonLutonAirport);
        }

        [Fact]
        public async Task FindByIcaoAsyncShouldReturnNullWhenNoMatchingAirportCanBeFound()
        {
            // Arrange
            var airportStore = _fixture.Create<IAirportStore>();

            // Act
            var airport = await airportStore.FindByIcaoAsync(_fixture.Create<string>());

            // Assert
            airport.Should().BeNull();
        }
    }
}
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

            _fixture.Register<IAirportStore>(() => _fixture.Create<OpenFlightsAirportStore>());
        }

        private readonly IFixture _fixture;

        [Fact]
        public async Task FindByIataAsyncShouldReturnAirport()
        {
            // Arrange
            var airportStore = _fixture.Create<IAirportStore>();

            // Act
            var airport = await airportStore.FindByIataAsync(_fixture.Create<string>());

            // Assert
            airport.Should().NotBeNull();
        }
    }
}
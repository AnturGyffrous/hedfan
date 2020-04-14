using AutoFixture;

using FluentAssertions;

using Hedfan.Schedules.Airports;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class AirportTests
    {
        public AirportTests()
        {
            _fixture = new Fixture();

            _fixture.Customize<Airport>(c => c
                .With(x => x.Iata, "LTN")
                .With(x => x.Icao, "EGGW"));
        }

        private readonly IFixture _fixture;

        [Fact]
        public void ToStringShouldReturnUserFriendlyAirportDescription()
        {
            // Arrange
            var airport = _fixture.Create<Airport>();

            // Act
            var result = airport.ToString();

            // Assert
            result.Should().Be("EGGW London Luton Airport (LTN)");
        }
    }
}
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
                .With(x => x.Name, "London Luton Airport")
                .With(x => x.City, "London")
                .With(x => x.Country, "United Kingdom")
                .With(x => x.Iata, "LTN")
                .With(x => x.Icao, "EGGW")
                .With(x => x.Latitude, 51.874698638916016)
                .With(x => x.Longitude, -0.36833301186561584)
                .With(x => x.Altitude, 526)
                .With(x => x.Timezone, "Europe/London")
                .With(x => x.Source, "OurAirports")
            );
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
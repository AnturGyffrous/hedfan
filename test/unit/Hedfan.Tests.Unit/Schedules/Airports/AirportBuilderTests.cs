using AutoFixture;
using AutoFixture.AutoMoq;

using FluentAssertions;

using Hedfan.Schedules.Airports;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class AirportBuilderTests
    {
        public AirportBuilderTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        private readonly IFixture _fixture;

        [Fact]
        public void AirportBuilderCanBeUsedToCreateAnAirport()
        {
            // Arrange
            var builder = _fixture.Create<AirportBuilder>();

            // Act
            var airport = new Airport(builder);

            // Assert
            airport.Name.Should().Be(builder.Name);
            airport.City.Should().Be(builder.City);
            airport.Country.Should().Be(builder.Country);
            airport.Iata.Should().Be(builder.Iata);
            airport.Icao.Should().Be(builder.Icao);
            airport.Latitude.Should().Be(builder.Latitude);
            airport.Longitude.Should().Be(builder.Longitude);
            airport.Altitude.Should().Be(builder.Altitude);
            airport.Timezone.Should().Be(builder.Timezone);
            airport.Source.Should().Be(builder.Source);
        }
    }
}
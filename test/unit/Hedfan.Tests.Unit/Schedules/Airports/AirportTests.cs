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

        private class AirportSubClass : Airport
        {
        }

        [Fact]
        public void EqualsShouldReturnTrueIfPropertiesAreTheSameEvenIfOtherIsDerivedType()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            };

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EqualsShouldReturnTrueIfTheReferencesAreDifferentButThePropertiesAreTheSame()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = _fixture.Create<Airport>();

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EqualsShouldReturnTrueIfTheReferencesAreTheSame()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = airport1;

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GetHashCodeShouldReturnTheSameValueForAirportsWithTheSameValues()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = _fixture.Create<Airport>();

            // Act
            var hashCode1 = airport1.GetHashCode();
            var hashCode2 = airport2.GetHashCode();

            // Assert
            airport1.Should().NotBeSameAs(airport2);
            hashCode1.Should().Be(hashCode2);
        }

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
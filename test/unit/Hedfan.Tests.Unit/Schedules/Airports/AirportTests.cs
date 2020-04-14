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
                .With(x => x.Icao, "EGGW")
                .With(x => x.Name, "London Luton Airport"));
        }

        private readonly IFixture _fixture;

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
using System;

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
        public void ConstructorShouldCreateNewAirportUsingAirportBuilder()
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

        [Fact]
        public void ConstructorShouldNotThrowIfIataIsNull()
        {
            // Arrange
            var builder = _fixture.Build<AirportBuilder>().Without(x => x.Iata).Create();

            // Act
            Func<Airport> act = () => new Airport(builder);

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void ConstructorShouldThrowArgumentNullExceptionIfCountryIsNull()
        {
            // Arrange
            var builder = _fixture.Build<AirportBuilder>().Without(x => x.Country).Create();

            // Act
            Func<Airport> act = () => new Airport(builder);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter '{nameof(Airport.Country)}')");
        }

        [Fact]
        public void ConstructorShouldThrowArgumentNullExceptionIfIcaoIsNull()
        {
            // Arrange
            var builder = _fixture.Build<AirportBuilder>().Without(x => x.Icao).Create();

            // Act
            Func<Airport> act = () => new Airport(builder);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter '{nameof(Airport.Icao)}')");
        }

        [Fact]
        public void ConstructorShouldThrowArgumentNullExceptionIfNameIsNull()
        {
            // Arrange
            var builder = _fixture.Build<AirportBuilder>().Without(x => x.Name).Create();

            // Act
            Func<Airport> act = () => new Airport(builder);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter '{nameof(Airport.Name)}')");
        }

        [Fact]
        public void ConstructorShouldThrowArgumentNullExceptionIfSourceIsNull()
        {
            // Arrange
            var builder = _fixture.Build<AirportBuilder>().Without(x => x.Source).Create();

            // Act
            Func<Airport> act = () => new Airport(builder);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage($"Value cannot be null. (Parameter '{nameof(Airport.Source)}')");
        }
    }
}
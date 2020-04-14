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

            _fixture.Customize<AirportBuilder>(c => c
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
            public AirportSubClass(AirportBuilder builder) : base(builder)
            {
            }
        }

        [Fact]
        public void EqualsOperatorShouldReturnFalseWhenWhenTheAirportsAreDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = "Luton International Airport",
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1 == airport2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsOperatorShouldReturnTrueWhenWhenTheAirportsHaveTheSameValues()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = _fixture.Create<Airport>();

            // Act
            var result = airport1 == airport2;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportAltitudeIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = 83,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportCityIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = "Luton",
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportCountryIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = "England",
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportIataIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = airport1.Country,
                Iata = "LHR",
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportIcaoIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = "EGLL",
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportLatitudeIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = 51.4706,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportLongitudeIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = -0.461941,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportNameIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = "Luton International Airport",
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportSourceIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
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
                Source = "Other"
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfTheAirportTimezoneIsDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = airport1.Name,
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = "America/Argentina/Ushuaia",
                Source = airport1.Source
            });

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void EqualsShouldReturnTrueIfPropertiesAreTheSameButIataIsNull()
        {
            // Arrange
            var builder = new AirportBuilder
            {
                Name = "Winnipeg / St. Andrews Airport",
                City = "Winnipeg",
                Country = "Canada",
                Iata = null,
                Icao = "CYAV",
                Longitude = 50.0564002991,
                Latitude = -97.03250122070001,
                Altitude = 760,
                Timezone = "America/Winnipeg",
                Source = "OurAirports"
            };

            var airport1 = new AirportSubClass(builder);
            var airport2 = new AirportSubClass(builder);

            // Act
            var result = airport1.Equals(airport2);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void EqualsShouldReturnTrueIfPropertiesAreTheSameEvenIfOtherIsDerivedType()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
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
            });

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
        public void NotEqualsOperatorShouldReturnFalseWhenWhenTheAirportsHaveTheSameValues()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = _fixture.Create<Airport>();

            // Act
            var result = airport1 != airport2;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void NotEqualsOperatorShouldReturnTrueWhenWhenTheAirportsAreDifferent()
        {
            // Arrange
            var airport1 = _fixture.Create<Airport>();
            var airport2 = new AirportSubClass(new AirportBuilder
            {
                Name = "Luton International Airport",
                City = airport1.City,
                Country = airport1.Country,
                Iata = airport1.Iata,
                Icao = airport1.Icao,
                Longitude = airport1.Longitude,
                Latitude = airport1.Latitude,
                Altitude = airport1.Altitude,
                Timezone = airport1.Timezone,
                Source = airport1.Source
            });

            // Act
            var result = airport1 != airport2;

            // Assert
            result.Should().BeTrue();
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
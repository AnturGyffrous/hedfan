using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoFixture;

using FluentAssertions;

using Hedfan.Schedules.Airports;
using Hedfan.Tests.Unit.Properties;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class OpenFlightsAirportReaderTests
    {
        public OpenFlightsAirportReaderTests()
        {
            _fixture = new Fixture();

            _fixture.Inject<Stream>(new MemoryStream(Encoding.UTF8.GetBytes(Resources.OpenFlightsAirportDataSample)));
            _fixture.Register<AirportReader>(() => _fixture.Create<OpenFlightsAirportReader>());
        }

        private readonly IFixture _fixture;

        [Fact]
        public void DisposeShouldNotThrowEvenAfterTheReaderHasBeenDisposed()
        {
            // Arrange
            var reader = _fixture.Create<AirportReader>();
            reader.Dispose();

            // Act
            Action act = () => reader.Dispose();

            // Assert
            act.Should().NotThrow();
        }

        [Fact]
        public void GetAirportShouldReturnAirport()
        {
            // Arrange
            var reader = _fixture.Create<AirportReader>();
            Enumerable.Repeat(reader, 3).ToList().ForEach(x => x.Read());

            // Act
            var airport = reader.GetAirport();

            // Assert
            airport.Name.Should().Be(ExampleAirports.LondonLuton.Name);
            airport.City.Should().Be(ExampleAirports.LondonLuton.City);
            airport.Country.Should().Be(ExampleAirports.LondonLuton.Country);
            airport.Iata.Should().Be(ExampleAirports.LondonLuton.Iata);
            airport.Icao.Should().Be(ExampleAirports.LondonLuton.Icao);
            airport.Icao.Should().Be(ExampleAirports.LondonLuton.Icao);
            airport.Latitude.Should().Be(ExampleAirports.LondonLuton.Latitude);
            airport.Longitude.Should().Be(ExampleAirports.LondonLuton.Longitude);
            airport.Altitude.Should().Be(ExampleAirports.LondonLuton.Altitude);
            airport.Timezone.Should().Be(ExampleAirports.LondonLuton.Timezone);
            airport.Source.Should().Be(ExampleAirports.LondonLuton.Source);
        }

        [Fact]
        public async Task ReadAsyncShouldReturnFalseWhenNoMoreAirportsCanBeReadFromTheStream()
        {
            // Arrange
            using var reader = _fixture.Create<AirportReader>();
            Enumerable.Repeat(reader, 9).ToList().ForEach(async x => await x.ReadAsync());

            // Act
            var result = await reader.ReadAsync();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task ReadAsyncShouldReturnTrueWhenAnotherAirportCanBeReadFromTheStream()
        {
            // Arrange
            using var reader = _fixture.Create<AirportReader>();

            // Act
            var result = await reader.ReadAsync();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ReadAsyncShouldThrowObjectDisposedExceptionIfTheReaderHasBeenDisposed()
        {
            // Arrange
            var reader = _fixture.Create<AirportReader>();
            reader.Dispose();

            // Act
            Func<Task> act = async () => await reader.ReadAsync();

            // Assert
            act.Should().Throw<ObjectDisposedException>();
        }

        [Fact]
        public void ReadShouldReturnFalseWhenNoMoreAirportsCanBeReadFromTheStream()
        {
            // Arrange
            using var reader = _fixture.Create<AirportReader>();
            Enumerable.Repeat(reader, 9).ToList().ForEach(x => x.Read());

            // Act
            var result = reader.Read();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void ReadShouldReturnTrueWhenAnotherAirportCanBeReadFromTheStream()
        {
            // Arrange
            using var reader = _fixture.Create<AirportReader>();

            // Act
            var result = reader.Read();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ReadShouldThrowObjectDisposedExceptionIfTheReaderHasBeenDisposed()
        {
            // Arrange
            var reader = _fixture.Create<AirportReader>();
            reader.Dispose();

            // Act
            Action act = () => reader.Read();

            // Assert
            act.Should().Throw<ObjectDisposedException>();
        }
    }
}
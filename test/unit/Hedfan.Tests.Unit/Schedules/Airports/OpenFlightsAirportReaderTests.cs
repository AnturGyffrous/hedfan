using System.IO;
using System.Text;

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
        public void ReadShouldReturnTrueWhenAnotherAirportCanBeReadFromTheStream()
        {
            // Arrange
            using var reader = _fixture.Create<AirportReader>();

            // Act
            var result = reader.Read();

            // Assert
            result.Should().BeTrue();
        }
    }
}
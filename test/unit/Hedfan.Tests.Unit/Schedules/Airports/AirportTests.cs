using FluentAssertions;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class AirportTests
    {
        [Fact]
        public void ToStringShouldReturnUserFriendlyAirportDescription()
        {
            // Arrange
            var airport = ExampleAirports.LondonLuton;

            // Act
            var result = airport.ToString();

            // Assert
            result.Should().Be("EGGW London Luton Airport (LTN)");
        }
    }
}
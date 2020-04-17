using System.Collections;
using System.Linq;

using AutoFixture;
using AutoFixture.AutoMoq;

using FluentAssertions;

using Hedfan.Schedules.Airports;
using Hedfan.Schedules.Routes;
using Hedfan.Tests.Unit.Schedules.Airports;

using Moq;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Routes
{
    public class EasyJetRoutesTests
    {
        public EasyJetRoutesTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            var airportStore = _fixture.Freeze<Mock<IAirportStore>>();
            airportStore
                .Setup(x => x.FindByIataAsync(ExampleAirports.LondonLuton.Iata))
                .ReturnsAsync(ExampleAirports.LondonLuton);
            airportStore
                .Setup(x => x.FindByIataAsync(ExampleAirports.Glasgow.Iata))
                .ReturnsAsync(ExampleAirports.Glasgow);

            _fixture.Register<IRoutes>(() => _fixture.Create<EasyJetRoutes>());
        }

        private readonly IFixture _fixture;

        [Fact]
        public void GetEnumeratorShouldReturnLondonLutonToGlasgowRoute()
        {
            // Arrange
            var routes = _fixture.Create<IRoutes>();

            // Act
            var route = routes.First(x => x.Origin.Iata == ExampleAirports.LondonLuton.Iata && x.Destination.Iata == ExampleAirports.Glasgow.Iata);

            // Assert
            route.Origin.Icao.Should().Be(ExampleAirports.LondonLuton.Icao);
            route.Destination.Icao.Should().Be(ExampleAirports.Glasgow.Icao);
        }

        [Fact]
        public void NonGenericGetEnumeratorShouldReturnLondonLutonToGlasgowRoute()
        {
            // Arrange
            var routes = _fixture.Create<IRoutes>() as IEnumerable;
            Route result = null;

            // Act
            foreach (var o in routes)
            {
                if (o is Route route)
                {
                    if (route.Origin.Iata == ExampleAirports.LondonLuton.Iata && route.Destination.Iata == ExampleAirports.Glasgow.Iata)
                    {
                        result = route;
                        break;
                    }
                }
            }

            // Assert
            result.Should().NotBeNull();
            result?.Origin.Icao.Should().Be(ExampleAirports.LondonLuton.Icao);
            result?.Destination.Icao.Should().Be(ExampleAirports.Glasgow.Icao);
        }
    }
}
using System.Linq;

using AutoFixture;
using AutoFixture.AutoMoq;

using Hedfan.Schedules.Routes;
using Hedfan.Tests.Unit.Schedules.Airports;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Routes
{
    public class EasyJetRoutesTests
    {
        public EasyJetRoutesTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

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
        }
    }
}
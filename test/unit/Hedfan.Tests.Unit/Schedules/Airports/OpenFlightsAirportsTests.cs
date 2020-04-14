using AutoFixture;
using AutoFixture.AutoMoq;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class OpenFlightsAirportsTests
    {
        public OpenFlightsAirportsTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Register<IAirportStore>();
        }

        private readonly IFixture _fixture;
    }
}
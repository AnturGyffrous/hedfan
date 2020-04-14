using AutoFixture;
using AutoFixture.AutoMoq;

using Hedfan.Schedules.Airports;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class OpenFlightsAirportsTests
    {
        public OpenFlightsAirportsTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Register<IAirportStore>(() => _fixture.Create<OpenFlightsAirportStore>());
        }

        private readonly IFixture _fixture;
    }
}
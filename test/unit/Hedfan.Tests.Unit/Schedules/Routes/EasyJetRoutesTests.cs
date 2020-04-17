using AutoFixture;
using AutoFixture.AutoMoq;

using Hedfan.Schedules.Routes;

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
    }
}
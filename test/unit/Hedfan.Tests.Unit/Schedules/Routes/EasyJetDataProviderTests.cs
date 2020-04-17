using AutoFixture;
using AutoFixture.AutoMoq;

using Hedfan.Schedules.Routes;

namespace Hedfan.Tests.Unit.Schedules.Routes
{
    public class EasyJetDataProviderTests
    {
        public EasyJetDataProviderTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Register<IEasyJetDataProvider>(() => _fixture.Create<EasyJetDataProvider>());
        }

        private readonly IFixture _fixture;
    }
}
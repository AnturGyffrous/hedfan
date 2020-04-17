using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;

using Hedfan.Schedules.Routes;

using Xunit;

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

        [Fact]
        public async Task GetRoutesAsyncShouldDownloadLatestRoutesFromEasyJetWebsite()
        {
            // Arrange
            var dataProvider = _fixture.Create<IEasyJetDataProvider>();

            // Act
            var routes = await dataProvider.GetRoutesAsync();
        }
    }
}
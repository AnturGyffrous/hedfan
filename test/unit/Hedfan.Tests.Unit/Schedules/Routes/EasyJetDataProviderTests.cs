using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;

using Hedfan.Schedules.Routes;
using Hedfan.Tests.Unit.Properties;

using Moq;
using Moq.Protected;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Routes
{
    public class EasyJetDataProviderTests
    {
        public EasyJetDataProviderTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Freeze<Mock<HttpMessageHandler>>()
                .Protected()
                .As<ISendAsyncProtectedMembers>()
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Resources.EasyJetTimetables, Encoding.UTF8, "text/html")
                });

            _fixture.Register(() => new HttpClient(_fixture.Create<HttpMessageHandler>()));

            _fixture.Register<IEasyJetDataProvider>(() => _fixture.Create<EasyJetDataProvider>());
        }

        private readonly IFixture _fixture;

        private interface ISendAsyncProtectedMembers
        {
            Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
        }

        [Fact]
        public async Task GetRoutesAsyncShouldDownloadLatestRoutesFromEasyJetWebsite()
        {
            // Arrange
            var dataProvider = _fixture.Create<IEasyJetDataProvider>();

            // Act
            var routes = await dataProvider.GetRoutesAsync(_fixture.Create<HttpClient>());
        }
    }
}
﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;

using FluentAssertions;

using Hedfan.Schedules.Airports;
using Hedfan.Tests.Unit.Properties;

using Moq;
using Moq.Protected;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.Airports
{
    public class OpenFlightsAirportDownloaderTests
    {
        public OpenFlightsAirportDownloaderTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _fixture.Freeze<Mock<HttpMessageHandler>>()
                .Protected()
                .As<ISendAsyncProtectedMembers>()
                .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Encoding.UTF8.GetString(Resources.OpenFlightsAirportDataSample), Encoding.UTF8, "text/plain")
                });

            _fixture.Register(() => new HttpClient(_fixture.Create<HttpMessageHandler>()));
        }

        private readonly IFixture _fixture;

        private interface ISendAsyncProtectedMembers
        {
            Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
        }

        [Fact]
        public async Task DownloadShouldUseHardcodedUrlToDownloadLatestData()
        {
            // Arrange
            var stream = await OpenFlightsAirportDownloader.Download(_fixture.Create<HttpClient>());
            var reader = new OpenFlightsAirportReader(stream);
            var airportStore = new OpenFlightsAirportStore(reader.GetAirports());

            // Act
            var result = await airportStore.FindByIcaoAsync("YNGU");

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Ngukurr Airport");
            result.Country.Should().Be("Australia");
            result.Iata.Should().Be("RPM");
            result.Latitude.Should().Be(-14.722800254821777);
            result.Longitude.Should().Be(134.7469940185547);
            result.Altitude.Should().Be(45);

            _fixture.Create<Mock<HttpMessageHandler>>()
                .Protected()
                .As<ISendAsyncProtectedMembers>()
                .Verify(x => x.SendAsync(
                    It.Is<HttpRequestMessage>(m => m.RequestUri == new Uri("https://raw.githubusercontent.com/jpatokal/openflights/master/data/airports.dat")),
                    It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DownloadShouldUseHardcodedUserAgentIfNoneIsSetInClient()
        {
            // Arrange
            async Task Act()
            {
                await OpenFlightsAirportDownloader.Download(_fixture.Create<HttpClient>());
            }

            // Act
            await Act();

            // Assert
            _fixture.Create<Mock<HttpMessageHandler>>()
                .Protected()
                .As<ISendAsyncProtectedMembers>()
                .Verify(x => x.SendAsync(
                    It.Is<HttpRequestMessage>(m => m.Headers.UserAgent.ToString().Contains(nameof(OpenFlightsAirportDownloader))),
                    It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DownloadShouldUseUserAgentOfClientIfOneIsSet()
        {
            // Arrange
            const string productName = "UnitTest";
            const string productVersion = "1.0";

            async Task Act()
            {
                var client = _fixture.Create<HttpClient>();
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(productName, productVersion));
                await OpenFlightsAirportDownloader.Download(client);
            }

            // Act
            await Act();

            // Assert
            _fixture.Create<Mock<HttpMessageHandler>>()
                .Protected()
                .As<ISendAsyncProtectedMembers>()
                .Verify(x => x.SendAsync(
                    It.Is<HttpRequestMessage>(m => m.Headers.UserAgent.ToString() == $"{productName}/{productVersion}"),
                    It.IsAny<CancellationToken>()));
        }
    }
}
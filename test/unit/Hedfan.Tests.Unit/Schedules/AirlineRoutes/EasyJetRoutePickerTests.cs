﻿using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AutoFixture;
using AutoFixture.AutoMoq;

using FluentAssertions;

using Hedfan.Schedules.AirlineRoutes;
using Hedfan.Schedules.Airports;
using Hedfan.Tests.Unit.Properties;
using Hedfan.Tests.Unit.Schedules.Airports;

using Moq;
using Moq.Protected;

using Xunit;

namespace Hedfan.Tests.Unit.Schedules.AirlineRoutes
{
    public class EasyJetRoutePickerTests
    {
        public EasyJetRoutePickerTests()
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

            var airportStore = _fixture.Freeze<Mock<IAirportStore>>();
            airportStore
                .Setup(x => x.FindByIataAsync(ExampleAirports.LondonLuton.Iata))
                .ReturnsAsync(ExampleAirports.LondonLuton);
            airportStore
                .Setup(x => x.FindByIataAsync(ExampleAirports.Glasgow.Iata))
                .ReturnsAsync(ExampleAirports.Glasgow);

            _fixture.Register<IAsyncAirlineRoutes>(() => _fixture.Create<EasyJetRoutePicker>());
        }

        private readonly IFixture _fixture;

        private interface ISendAsyncProtectedMembers
        {
            Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
        }

        [Fact]
        public async Task GetAsyncEnumeratorShouldDownloadLatestRoutesFromEasyJetWebsite()
        {
            // Arrange
            AirlineRoute result = null;

            // Act
            await foreach (var route in _fixture.Create<IAsyncAirlineRoutes>())
            {
                if (route.Origin.Iata == ExampleAirports.LondonLuton.Iata && route.Destination.Iata == ExampleAirports.Glasgow.Iata)
                {
                    result = route;
                    break;
                }
            }

            // Assert
            result.Should().NotBeNull();
            result?.Airline.Icao.Should().Be("EZY");
            result?.Origin.Icao.Should().Be(ExampleAirports.LondonLuton.Icao);
            result?.Destination.Icao.Should().Be(ExampleAirports.Glasgow.Icao);
        }
    }
}
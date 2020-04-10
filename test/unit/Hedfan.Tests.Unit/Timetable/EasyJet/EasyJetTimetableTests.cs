using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;

using Hedfan.Tests.Unit.Properties;

using Newtonsoft.Json;

using Xunit;

namespace Hedfan.Tests.Unit.Timetable.EasyJet
{
    public class EasyJetTimetableTests
    {
        [Fact]
        public void DeserializeObjectShouldDeserializeEasyJetTimetableCallbackBrsEdi()
        {
            var timetable = JsonConvert.DeserializeObject<EasyJetTimetable>(Resources.EasyJetTimetableCallbackLtnGla);

            timetable.PricesAvailable.Should().BeTrue();
            timetable.SurchargeOutboundOneWay.Should().Be(0);
            timetable.SurchargePerLegRoundTrip.Should().Be(0);
            timetable.OutboundLegs.Should().HaveCount(12);
            timetable.OutboundLegs.ElementAt(3).CheapestPrice.Should().Be(26.99m);
            timetable.OutboundLegs.ElementAt(3).MonthDate.Should().Be(new DateTime(2020, 7, 1));
            timetable.OutboundLegs.ElementAt(3).ShortMonthName.Should().Be("Jul");
            timetable.OutboundLegs.ElementAt(3).Days.Should().HaveCount(31);
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Date.Should().Be(new DateTime(2020, 7, 10));
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).DayInitial.Should().Be("F");
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).FilterPrices.Should().ContainInOrder(new[] { 80.99m, 60.99m, 41.99m });
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.Should().HaveCount(10);
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.ElementAt(5).FlightNumber.Should().Be(75);
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.ElementAt(5).Id.Should().Be(7365460);
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.ElementAt(5).IsApprox.Should().Be(false);
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.ElementAt(5).IsSoldOut.Should().Be(false);
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.ElementAt(5).LocalArrTime.Should().Be(new DateTime(2020, 7, 10, 16, 50, 0));
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.ElementAt(5).LocalDepTime.Should().Be(new DateTime(2020, 7, 10, 15, 35, 0));
            timetable.OutboundLegs.ElementAt(3).Days.ElementAt(9).Flights.ElementAt(5).Price.Should().Be(60.99m);
            timetable.ReturnLegs.Should().HaveCount(12);
            timetable.ReturnLegs.ElementAt(3).CheapestPrice.Should().Be(24.99m);
            timetable.ReturnLegs.ElementAt(3).MonthDate.Should().Be(new DateTime(2020, 7, 1));
            timetable.ReturnLegs.ElementAt(3).ShortMonthName.Should().Be("Jul");
            timetable.ReturnLegs.ElementAt(3).Days.Should().HaveCount(31);
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Date.Should().Be(new DateTime(2020, 7, 13));
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).DayInitial.Should().Be("M");
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).FilterPrices.Should().ContainInOrder(new[] { 28.99m, 0m, 24.99m });
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.Should().HaveCount(10);
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.ElementAt(0).FlightNumber.Should().Be(66);
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.ElementAt(0).Id.Should().Be(7349422);
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.ElementAt(0).IsApprox.Should().Be(false);
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.ElementAt(0).IsSoldOut.Should().Be(false);
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.ElementAt(0).LocalArrTime.Should().Be(new DateTime(2020, 7, 13, 8, 20, 0));
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.ElementAt(0).LocalDepTime.Should().Be(new DateTime(2020, 7, 13, 7, 0, 0));
            timetable.ReturnLegs.ElementAt(3).Days.ElementAt(12).Flights.ElementAt(0).Price.Should().Be(28.99m);
        }
    }

    public class EasyJetTimetable
    {
        [JsonProperty("outboundLeg")]
        public IEnumerable<Month> OutboundLegs { get; set; }

        public bool PricesAvailable { get; set; }

        [JsonProperty("returnLeg")]
        public IEnumerable<Month> ReturnLegs { get; set; }

        [JsonProperty("surchargeOutbound_oneWay")]
        public decimal SurchargeOutboundOneWay { get; set; }

        [JsonProperty("surchargePerLeg_roundTrip")]
        public decimal SurchargePerLegRoundTrip { get; set; }
    }

    public class Month
    {
        public decimal CheapestPrice { get; set; }

        public IEnumerable<Day> Days { get; set; }

        public DateTime MonthDate { get; set; }

        public string ShortMonthName { get; set; }
    }

    public class Day
    {
        public DateTime Date { get; set; }

        public string DayInitial { get; set; }

        public IEnumerable<decimal> FilterPrices { get; set; }

        public IEnumerable<Flight> Flights { get; set; }
    }

    public class Flight
    {
        [JsonProperty("flightNum")]
        public int FlightNumber { get; set; }

        public int Id { get; set; }

        public bool IsApprox { get; set; }

        public bool IsSoldOut { get; set; }

        public DateTime LocalArrTime { get; set; }

        public DateTime LocalDepTime { get; set; }

        public decimal Price { get; set; }
    }
}
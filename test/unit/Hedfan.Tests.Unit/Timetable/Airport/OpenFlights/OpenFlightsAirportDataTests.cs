using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

using CsvHelper;
using CsvHelper.Configuration.Attributes;

using FluentAssertions;

using Hedfan.Tests.Unit.Properties;

using Xunit;

namespace Hedfan.Tests.Unit.Timetable.Airport.OpenFlights
{
    public class OpenFlightsAirportDataTests
    {
        [Fact]
        public void GetRecordsShouldReadDataFromStream()
        {
            using var reader = new StringReader(Encoding.UTF8.GetString(Resources.OpenFlightsAirportDataSample));
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            var records = csv.GetRecords<OpenFlightsAirport>().ToList();

            records.ElementAt(0).AirportId.Should().Be(15);
            records.ElementAt(0).Name.Should().Be("Ísafjörður Airport");
            records.ElementAt(1).City.Should().Be("Winnipeg");
            records.ElementAt(2).Country.Should().Be("United Kingdom");
            records.ElementAt(2).Iata.Should().Be("LTN");
            records.ElementAt(2).Icao.Should().Be("EGGW");
            records.ElementAt(3).Latitude.Should().Be(51.4706);
            records.ElementAt(3).Longitude.Should().Be(-0.461941);
            records.ElementAt(4).Altitude.Should().Be(26);
            records.ElementAt(4).Dst.Should().Be("E");
            records.ElementAt(6).UtcOffset.Should().Be("-3");
            records.ElementAt(6).Timezone.Should().Be("America/Argentina/Ushuaia");
            records.ElementAt(8).Source.Should().Be("OurAirports");
        }
    }

    public class OpenFlightsAirport
    {
        [Index(0)]
        public int AirportId { get; set; }

        [Index(8)]
        public int Altitude { get; set; }

        [Index(2)]
        public string City { get; set; }

        [Index(3)]
        public string Country { get; set; }

        [Index(10)]
        public string Dst { get; set; }

        [Index(4)]
        public string Iata { get; set; }

        [Index(5)]
        public string Icao { get; set; }

        [Index(6)]
        public double Latitude { get; set; }

        [Index(7)]
        public double Longitude { get; set; }

        [Index(1)]
        public string Name { get; set; }

        [Index(13)]
        public string Source { get; set; }

        [Index(11)]
        public string Timezone { get; set; }

        [Index(9)]
        public string UtcOffset { get; set; }
    }
}
using System.Globalization;
using System.IO;
using System.Linq;

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
            using var reader = new StringReader(Resources.OpenFlightsAirportDataSample);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            var records = csv.GetRecords<OpenFlightsAirport>().ToList();

            records.ElementAt(0).AirportId.Should().Be(17);
            records.ElementAt(0).Name.Should().Be("Patreksfjörður Airport");
            records.ElementAt(0).City.Should().Be("Patreksfjordur");
            records.ElementAt(1).Country.Should().Be("United Kingdom");
            records.ElementAt(1).Iata.Should().Be("LTN");
            records.ElementAt(1).Icao.Should().Be("EGGW");
            records.ElementAt(1).Latitude.Should().Be(51.874698638916016);
            records.ElementAt(1).Longitude.Should().Be(-0.36833301186561584);
            records.ElementAt(2).Altitude.Should().Be(26);
            records.ElementAt(2).Dst.Should().Be('E');
            records.ElementAt(3).UtcOffset.Should().Be(8);
            records.ElementAt(3).Timezone.Should().Be("Asia/Shanghai");
            records.ElementAt(3).Source.Should().Be("OurAirports");
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
        public char Dst { get; set; }

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
        public decimal UtcOffset { get; set; }
    }
}
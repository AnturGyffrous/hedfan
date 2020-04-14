using System.Globalization;
using System.IO;

using CsvHelper;

namespace Hedfan.Schedules.Airports
{
    public class OpenFlightsAirportReader : AirportReader
    {
        private readonly CsvReader _csvReader;

        private readonly StreamReader _streamReader;

        public OpenFlightsAirportReader(Stream stream)
        {
            _streamReader = new StreamReader(stream);
            _csvReader = new CsvReader(_streamReader, CultureInfo.InvariantCulture);
            _csvReader.Configuration.HasHeaderRecord = false;
        }

        public override bool Read() => _csvReader.Read();
    }
}
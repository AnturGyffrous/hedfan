using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper;

namespace Hedfan.Schedules.Airports
{
    public class OpenFlightsAirportReader : AirportReader
    {
        private readonly CsvReader _csvReader;

        private readonly StreamReader _streamReader;

        private bool _disposed;

        public OpenFlightsAirportReader(Stream stream)
        {
            _streamReader = new StreamReader(stream);
            _csvReader = new CsvReader(_streamReader, CultureInfo.InvariantCulture);
            _csvReader.Configuration.HasHeaderRecord = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _streamReader.Dispose();
                _csvReader.Dispose();
            }

            _disposed = true;
        }

        public override Airport GetAirport() => _csvReader.GetRecord<OpenFlightsAirport>().ToAirport();

        public override Task<Airport> GetAirportAsync() => Task.FromResult(GetAirport());

        public override IEnumerable<Airport> GetAirports() =>
            _csvReader
                .GetRecords<OpenFlightsAirport>()
                .Select(x => x.ToAirport());

        public override async IAsyncEnumerable<Airport> GetAirportsAsync()
        {
            await foreach (var openFlightsAirport in _csvReader.GetRecordsAsync<OpenFlightsAirport>())
            {
                yield return openFlightsAirport.ToAirport();
            }
        }

        public override bool Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenFlightsAirportReader));
            }

            return _csvReader.Read();
        }

        public override Task<bool> ReadAsync()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenFlightsAirportReader));
            }

            return _csvReader.ReadAsync();
        }
    }
}
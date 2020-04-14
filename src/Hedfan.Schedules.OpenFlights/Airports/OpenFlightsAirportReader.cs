using System;
using System.Globalization;
using System.IO;

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

        public override bool Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenFlightsAirportReader));
            }

            return _csvReader.Read();
        }
    }
}
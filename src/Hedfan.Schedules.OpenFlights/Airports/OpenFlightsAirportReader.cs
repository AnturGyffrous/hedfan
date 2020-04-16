using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using CsvHelper;

namespace Hedfan.Schedules.Airports
{
    public class OpenFlightsAirportReader : AirportReader
    {
        private readonly CsvReader _csvReader;

        private readonly IMapper _mapper;

        private readonly StreamReader _streamReader;

        private bool _disposed;

        public OpenFlightsAirportReader(Stream stream)
        {
            _streamReader = new StreamReader(stream);
            _csvReader = new CsvReader(_streamReader, CultureInfo.InvariantCulture);
            _csvReader.Configuration.HasHeaderRecord = false;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<string, string>().ConvertUsing<StringTypeConverter>();
                cfg.CreateMap<OpenFlightsAirport, Airport>();
            });

            _mapper = config.CreateMapper();
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

        public override Airport GetAirport() => _mapper.Map<Airport>(_csvReader.GetRecord<OpenFlightsAirport>());

        public override Task<Airport> GetAirportAsync() => Task.FromResult(GetAirport());

        public override IEnumerable<Airport> GetAirports() =>
            _csvReader
                .GetRecords<OpenFlightsAirport>()
                .Select(x => _mapper.Map<Airport>(x));

        public override async IAsyncEnumerable<Airport> GetAirportsAsync()
        {
            await foreach (var openFlightsAirport in _csvReader.GetRecordsAsync<OpenFlightsAirport>())
            {
                yield return _mapper.Map<Airport>(openFlightsAirport);
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

        private class StringTypeConverter : ITypeConverter<string, string>
        {
            public string Convert(string source, string destination, ResolutionContext context)
            {
                return new[] { "\\N", string.Empty }.Contains(source) ? null : source;
            }
        }
    }
}
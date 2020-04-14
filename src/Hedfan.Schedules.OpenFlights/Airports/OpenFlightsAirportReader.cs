using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.Configuration.Attributes;

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

        public override Airport GetAirport()
        {
            var airport = _csvReader.GetRecord<OpenFlightsAirport>();

            var builder = new AirportBuilder
            {
                Name = airport.Name,
                City = airport.City,
                Country = airport.Country,
                Iata = airport.Iata,
                Icao = airport.Icao,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude,
                Altitude = airport.Altitude,
                Timezone = airport.Timezone,
                Source = airport.Source
            };

            return new Airport(builder);
        }

        public override Task<Airport> GetAirportAsync() => Task.FromResult(GetAirport());

        public override IEnumerable<Airport> GetAirports() =>
            _csvReader
                .GetRecords<OpenFlightsAirport>()
                .Select(airport => new AirportBuilder
                {
                    Name = airport.Name,
                    City = airport.City,
                    Country = airport.Country,
                    Iata = airport.Iata,
                    Icao = airport.Icao,
                    Latitude = airport.Latitude,
                    Longitude = airport.Longitude,
                    Altitude = airport.Altitude,
                    Timezone = airport.Timezone,
                    Source = airport.Source
                })
                .Select(builder => new Airport(builder));

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

        private class OpenFlightsAirport
        {
            [Index(8)]
            public int Altitude { get; set; }

            [Index(2)]
            public string City { get; set; }

            [Index(3)]
            public string Country { get; set; }

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
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Hedfan.Schedules.Properties;

namespace Hedfan.Schedules.Airports
{
    public class OpenFlightsAirportData : IEnumerable<Airport>
    {
        private readonly OpenFlightsAirportReader _reader = new OpenFlightsAirportReader(new MemoryStream(Resources.airports));

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Airport> GetEnumerator() => _reader.GetAirports().GetEnumerator();
    }
}
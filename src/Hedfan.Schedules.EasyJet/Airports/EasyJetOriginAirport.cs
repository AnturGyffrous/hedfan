using System.Collections.Generic;

namespace Hedfan.Schedules.Airports
{
    internal class EasyJetOriginAirport : EasyJetAirport
    {
        public string AbbreviatedName { get; set; }

        public IEnumerable<EasyJetDestinationAirport> ConnectedTo { get; set; }
    }
}
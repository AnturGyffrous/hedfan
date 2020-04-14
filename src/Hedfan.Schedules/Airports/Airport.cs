using System;

namespace Hedfan.Schedules.Airports
{
    public class Airport
    {
        public string Iata { get; set; }

        public string Icao { get; set; }

        public string Name { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = StringComparer.InvariantCulture.GetHashCode(Iata);
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(Icao);
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(Name);
                return hashCode;
            }
        }

        public override string ToString() => $"{Icao} {Name} ({Iata})";
    }
}
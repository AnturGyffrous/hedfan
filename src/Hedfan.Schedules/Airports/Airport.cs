using System;

namespace Hedfan.Schedules.Airports
{
    public class Airport
    {
        public int Altitude { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Iata { get; set; }

        public string Icao { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Name { get; set; }

        public string Source { get; set; }

        public string Timezone { get; set; }

        protected bool Equals(Airport other) =>
            string.Equals(Name, other.Name, StringComparison.InvariantCulture) &&
            string.Equals(City, other.City, StringComparison.InvariantCulture) &&
            string.Equals(Country, other.Country, StringComparison.InvariantCulture) &&
            string.Equals(Iata, other.Iata, StringComparison.InvariantCulture) &&
            string.Equals(Icao, other.Icao, StringComparison.InvariantCulture) &&
            Latitude.Equals(other.Latitude) &&
            Longitude.Equals(other.Longitude) &&
            Altitude == other.Altitude &&
            string.Equals(Timezone, other.Timezone, StringComparison.InvariantCulture) &&
            string.Equals(Source, other.Source, StringComparison.InvariantCulture);

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is Airport other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = StringComparer.InvariantCulture.GetHashCode(Name);
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(City);
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(Country);
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(Iata);
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(Icao);
                hashCode = (hashCode * 397) ^ Latitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
                hashCode = (hashCode * 397) ^ Altitude.GetHashCode();
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(Timezone);
                hashCode = (hashCode * 397) ^ StringComparer.InvariantCulture.GetHashCode(Source);
                return hashCode;
            }
        }

        public static bool operator ==(Airport left, Airport right) => Equals(left, right);

        public static bool operator !=(Airport left, Airport right) => !Equals(left, right);

        public override string ToString() => $"{Icao} {Name} ({Iata})";
    }
}
using System;

namespace Hedfan.Schedules.Airports
{
    public class Airport
    {
        public Airport(AirportBuilder builder)
        {
            Name = builder.Name ?? throw new ArgumentNullException(nameof(builder.Name));
            City = builder.City ?? throw new ArgumentNullException(nameof(builder.City));
            Country = builder.Country ?? throw new ArgumentNullException(nameof(builder.Country));
            Iata = builder.Iata;
            Icao = builder.Icao ?? throw new ArgumentNullException(nameof(builder.Icao));
            Latitude = builder.Latitude;
            Longitude = builder.Longitude;
            Altitude = builder.Altitude;
            Timezone = builder.Timezone ?? throw new ArgumentNullException(nameof(builder.Timezone));
            Source = builder.Source ?? throw new ArgumentNullException(nameof(builder.Source));
        }

        public int Altitude { get; }

        public string City { get; }

        public string Country { get; }

        public string Iata { get; }

        public string Icao { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string Name { get; }

        public string Source { get; }

        public string Timezone { get; }

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
                hashCode = (hashCode * 397) ^ (Iata != null ? StringComparer.InvariantCulture.GetHashCode(Iata) : 0);
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
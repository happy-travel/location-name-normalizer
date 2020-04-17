using System.Collections.Generic;
using LocationNameNormalizer.Models;

namespace LocationNameNormalizer
{
    public readonly struct CountryNode
    {
        public CountryNode(Country country, Dictionary<string, Locality> localities)
        {
            Country = country;
            Localities = localities;
        }


        public bool Equals(in CountryNode other) => (Country, Localities).Equals((other.Country, other.Localities));


        public override bool Equals(object obj) => obj is CountryNode other && Equals(other);


        public override int GetHashCode() => (Country, Localities).GetHashCode();


        public Country Country { get; }
        public Dictionary<string, Locality> Localities { get; }
    }
}
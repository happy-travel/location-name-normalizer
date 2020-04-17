using System.Collections.Generic;
using System.Linq;
using LocationNameNormalizer.Extensions;
using LocationNameNormalizer.Models;

namespace LocationNameNormalizer
{
    public class DefaultLocationNameNormalizer : ILocationNameNormalizer
    {
        public DefaultLocationNameNormalizer(ILocationNameRetriever locationNamesRetriever)
        {
            _locationNamesRetriever = locationNamesRetriever;
        }


        public void Init()
        {
            if (_countryIndex != null && _countryIndex.Any())
                return;

            var countries = _locationNamesRetriever.RetrieveCountries();
            var result = new Dictionary<string, CountryNode>();
            foreach (var country in countries)
            foreach (var countryName in country.Names)
            {
                var localities = new Dictionary<string, Locality>();
                foreach (var locality in country.Localities)
                foreach (var localityName in locality.Names)
                    localities.Add(localityName, locality);

                var node = new CountryNode(country, localities);
                result.Add(countryName, node);
            }

            _countryIndex = result;
        }


        public string GetNormalizedCountryName(string countryName)
        {
            var normalizedName = countryName.ToNormalizedName();
            var node = GetCountryNode(normalizedName);
            
            return node.Equals(default) ? normalizedName : node.Country.KeyName.ToTitleCase();
        }


        public string GetNormalizedLocalityName(string countryName, string localityName)
        {
            var normalizedCountryName = countryName.ToNormalizedName();
            var normalizedLocalityName = localityName.ToNormalizedName();

            var node = GetCountryNode(normalizedCountryName);
            if (node.Equals(default))
                return normalizedLocalityName;

            var locality = GetLocality(node, normalizedLocalityName);

            return locality == null ? normalizedLocalityName : locality.KeyName.ToTitleCase();
        }


        public List<string> GetCountryNames(string countryName)
        {
            var node = GetCountryNode(countryName);
            if (node.Equals(default))
                return new List<string>(0);
            
            return node.Country.Names.ToTitleCase();
        }


        public List<string> GetLocalityNames(string countryName, string localityName)
        {
            var node = GetCountryNode(countryName);
            if (node.Equals(default))
                return new List<string>(0);

            var locality = GetLocality(node, localityName);
            if (locality is null)
                return new List<string>(0);
            
            return locality.Names.ToTitleCase();
        }


        private static CountryNode GetCountryNode(string countryName)
        {
            var upperCased = countryName.ToUpperInvariant();
            _countryIndex.TryGetValue(upperCased, out var node);

            return node;
        }


        private static Locality GetLocality(in CountryNode node, string localityName)
        {
            var localities = node.Localities;
            var upperCased = localityName.ToUpperInvariant();
            localities.TryGetValue(upperCased, out var locality);

            return locality;
        }
        
        
        private static Dictionary<string, CountryNode> _countryIndex;
        private readonly ILocationNameRetriever _locationNamesRetriever;
    }
}
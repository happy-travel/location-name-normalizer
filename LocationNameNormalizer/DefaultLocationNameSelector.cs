using System.Collections.Generic;
using System.Linq;
using LocationNameNormalizer.Extensions;
using LocationNameNormalizer.Models;

namespace LocationNameNormalizer
{
    public class DefaultLocationNameSelector : IDefaultLocationNamesSelector
    {
        public DefaultLocationNameSelector(INameNormalizer nameNormalizer, ILocationNamesRetriever locationNamesRetriever)
        {
            _nameNormalizer = nameNormalizer;

            if (_countries == null || !_countries.Any())
                _countries = locationNamesRetriever.RetrieveCountries();
        }


        public string GetDefaultCountryName(string countryName)
        {
            var normalizedName = _nameNormalizer.Normalize(countryName);
            var country = GetCountry(normalizedName);
            
            return country == null ? normalizedName : country.KeyName.ToTitleCase();
        }


        public string GetDefaultLocalityName(string countryName, string localityName)
        {
            var normalizedCountryName = _nameNormalizer.Normalize(countryName);
            var normalizedLocalityName = _nameNormalizer.Normalize(localityName);

            var country = GetCountry(normalizedCountryName);
            if (country is null)
                return normalizedLocalityName;

            var locality = GetLocality(country, normalizedLocalityName);

            return locality == null ? normalizedLocalityName : locality.KeyName.ToTitleCase();
        }


        public List<string> GetCountryNames(string countryName)
        {
            var country = GetCountry(countryName);
            var countries = new List<string>();
            
            if (country is null)
                return countries;
            
            countries.AddRange(country.Names);

            return countries.ToTitleCase();
        }


        public List<string> GetLocalityNames(string countryName, string localityName)
        {
            var country = GetCountry(countryName);
            var localities = new List<string>();

            if (country is null)
                return localities;

            var locality = GetLocality(country, localityName);
            if (locality is null)
                return localities;
            
            localities.AddRange(locality.Names);

            return localities.ToTitleCase();
        }


        private static Country GetCountry(string countryName)
        {
            var upperCased = countryName.ToUpperInvariant();
            return _countries.SingleOrDefault(c => c.KeyName.Equals(upperCased)) 
                ?? _countries.SingleOrDefault(c => c.Names.Select(n => n).Contains(upperCased));
        }


        private static Locality GetLocality(Country country, string localityName)
        {
            var upperCased = localityName.ToUpperInvariant();
            return country.Localities.SingleOrDefault(l => l.KeyName.Equals(upperCased))
                ?? country.Localities.SingleOrDefault(l => l.Names.Select(i => i).Contains(upperCased));
        }
        
        
        private readonly INameNormalizer _nameNormalizer;
        private static List<Country> _countries;
    }
}
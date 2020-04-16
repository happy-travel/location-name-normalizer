using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LocationsNamesNormalizer.Extensions;
using LocationsNamesNormalizer.Models;

namespace LocationsNamesNormalizer
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
            var country = _countries.FirstOrDefault(c => c.Names.Contains(normalizedName));
            return country == null ? normalizedName : country.KeyName.ToTitleCase();
        }


        public string GetDefaultLocalityName(string countryName, string localityName)
        {
            var normalizedCountryName = _nameNormalizer.Normalize(countryName);
            var normalizedLocalityName = _nameNormalizer.Normalize(localityName);
            var country = _countries.FirstOrDefault(c => c.Names.Contains(normalizedCountryName));
            var locality = country?.Localities.FirstOrDefault(l => l.Names.Contains(normalizedLocalityName));

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
            var normalizedName = countryName.ToUpperInvariant();
            return _countries.SingleOrDefault(c => c.KeyName.Equals(normalizedName)) 
                ?? _countries.SingleOrDefault(c => c.Names.Select(n => n).Contains(normalizedName));
        }


        private static Locality GetLocality(Country country, string localityName)
        {
            var normalizedName = localityName.ToUpperInvariant();
            return country.Localities.SingleOrDefault(l => l.KeyName.Equals(normalizedName))
                ?? country.Localities.SingleOrDefault(l => l.Names.Select(i => i).Contains(normalizedName));
        }
        
        
        private readonly INameNormalizer _nameNormalizer;
        private static List<Country> _countries;
    }
}
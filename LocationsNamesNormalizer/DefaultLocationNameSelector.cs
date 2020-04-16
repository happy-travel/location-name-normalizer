using System.Collections.Generic;
using System.Linq;
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
            return country == null ? normalizedName : country.KeyName;
        }


        public string GetDefaultLocalityName(string countryName, string localityName)
        {
            var normalizedCountryName = _nameNormalizer.Normalize(countryName);
            var normalizedLocalityName = _nameNormalizer.Normalize(localityName);
            var country = _countries.FirstOrDefault(c => c.Names.Contains(normalizedCountryName));
            var locality = country?.Localities.FirstOrDefault(l => l.Names.Contains(normalizedLocalityName));

            return locality == null ? normalizedLocalityName : locality.KeyName;
        }


        public List<string> GetCountryNames(string countryName)
        {
            var country = GetCountry(countryName);
            var countries = new List<string>();
            
            if (country is null)
                return countries;
            
            countries.AddRange(country.Names);

            return countries;
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

            return localities;
        }


        private Country GetCountry(string countryName)
        {
            return _countries.SingleOrDefault(c => c.KeyName.ToUpperInvariant().Equals(countryName.ToUpperInvariant())) 
                ?? _countries.SingleOrDefault(c => c.Names.Select(n => n.ToUpperInvariant()).Contains(countryName.ToUpperInvariant()));
        }


        private Locality GetLocality(Country country, string localityName)
        {
            return country.Localities.SingleOrDefault(l => l.KeyName.ToUpperInvariant().Equals(localityName.ToUpperInvariant()))
                ?? country.Localities.SingleOrDefault(l => l.Names.Select(i => i.ToUpperInvariant()).Contains(localityName.ToUpperInvariant()));
        }
        
        
        private readonly INameNormalizer _nameNormalizer;
        private static List<Country> _countries;
    }
}
using System.Collections.Generic;
using System.IO;
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


        private readonly INameNormalizer _nameNormalizer;
        private static List<Country> _countries;
    }
}
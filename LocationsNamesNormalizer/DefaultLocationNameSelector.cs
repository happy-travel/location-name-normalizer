using System.Collections.Generic;
using System.IO;
using System.Linq;
using LocationsNamesNormalizer.Models;
using System.Text.Json;

namespace LocationsNamesNormalizer
{
    public static class DefaultLocationNameSelector
    {
        static DefaultLocationNameSelector()
        {
            _countries = new List<Country>();
            using var reader = new StreamReader(CountriesFilePath);
            var json = reader.ReadToEnd();
            _countries = JsonSerializer.Deserialize<List<Country>>(json);
        }


        public static string GetDefaultCountryName(string countryName)
        {
            var normalizedName = NameNormalizer.Normalize(countryName);
            var country = _countries.FirstOrDefault(c => c.Names.Contains(normalizedName));
            return country == null ? normalizedName : country.KeyName;
        }


        public static string GetDefaultLocalityName(string countryName, string localityName)
        {
            var normalizedCountryName = NameNormalizer.Normalize(countryName);
            var normalizedLocalityName = NameNormalizer.Normalize(localityName);
            var country = _countries.FirstOrDefault(c => c.Names.Contains(normalizedCountryName));
            var locality = country?.Localities.FirstOrDefault(l => l.Names.Contains(normalizedLocalityName));

            return locality == null ? normalizedLocalityName : locality.KeyName;
        }


        private const string CountriesFilePath = "Countries.json";
        private static readonly List<Country> _countries;
    }
}
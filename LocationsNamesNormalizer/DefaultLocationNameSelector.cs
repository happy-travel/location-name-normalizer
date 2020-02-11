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
            var country = _countries.FirstOrDefault(c => c.Names.Contains(countryName));
            return country == null ? countryName : country.KeyName;
        }


        public static string GetDefaultLocalityName(string countryName, string localityName)
        {
            var country = _countries.FirstOrDefault(c => c.Names.Contains(countryName));
            var locality = country?.Localities.FirstOrDefault(l => l.Names.Contains(localityName));

            return locality == null ? localityName : locality.KeyName;
        }


        private const string CountriesFilePath = "Countries.json";
        private static readonly List<Country> _countries;
    }
}
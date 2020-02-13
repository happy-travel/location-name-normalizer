using System.Collections.Generic;
using System.IO;
using LocationsNamesNormalizer.Models;
using System.Text.Json;

namespace LocationsNamesNormalizer
{
    public class LocationNamesRetriever : ILocationNamesRetriever
    {
        public List<Country> RetrieveCountries()
        {
            try
            {
                var json = File.ReadAllText(CountriesFilePath);
                return JsonSerializer.Deserialize<List<Country>>(json);
            }
            catch
            {
                return new List<Country>();
            }
        }


        private const string CountriesFilePath = "Countries.json";
    }
}
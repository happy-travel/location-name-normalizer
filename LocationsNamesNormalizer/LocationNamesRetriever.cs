using System.Collections.Generic;
using System.IO;
using LocationsNamesNormalizer.Models;
using System.Reflection;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LocationsNamesNormalizer
{
    public class LocationNamesRetriever : ILocationNamesRetriever
    {
        public List<Country> RetrieveCountries()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using var stream = assembly.GetManifestResourceStream(CountriesResourceName);
                if (stream == null)
                    return new List<Country>();

                using var streamReader = new StreamReader(stream);
                using var jsonTextReader = new JsonTextReader(streamReader);
                var serializer = new JsonSerializer();
                return serializer.Deserialize<List<Country>>(jsonTextReader);
            }
            catch
            {
                return new List<Country>();
            }
        }


        private const string CountriesResourceName = "LocationsNamesNormalizer.Countries.json";
    }
}
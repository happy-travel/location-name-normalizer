using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LocationNameNormalizer.Models;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LocationNameNormalizer
{
    public class FileLocationNameRetriever : ILocationNameRetriever
    {
        public List<Country> RetrieveCountries()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{CountriesResourceName}");
            if (stream is null)
                throw new Exception($"The resource file '{CountriesResourceName}' was not found.");

            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();

            return serializer.Deserialize<List<Country>>(jsonTextReader);
        }


        private const string CountriesResourceName = "Countries.json";
    }
}
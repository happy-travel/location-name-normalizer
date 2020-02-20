using System.Collections.Generic;

namespace LocationsNamesNormalizer.Models
{
    public class Country
    {
        public string KeyName { get; set; }
        public List<string> Names { get; set; }
        public List<Locality> Localities { get; set; }
    }
}
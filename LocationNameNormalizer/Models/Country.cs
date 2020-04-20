using System.Collections.Generic;

namespace LocationNameNormalizer.Models
{
    public class Country
    {
        public string KeyName { get; set; }
        public List<string> Names { get; set; }
        public List<Locality> Localities { get; set; }
    }
}
using System.Collections.Generic;

namespace LocationNameNormalizer.Models
{
    public class Country
    {
        public NameWithVariants Code { get; set; }
        public NameWithVariants Name { get; set; }
        public List<Locality> Localities { get; set; }
    }
}
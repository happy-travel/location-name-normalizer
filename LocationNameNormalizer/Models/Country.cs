using System.Collections.Generic;

namespace HappyTravel.LocationNameNormalizer.Models
{
    public class Country
    {
        public NameWithVariants Code { get; set; }
        public NameWithVariants Name { get; set; }
        public List<Locality> Localities { get; set; }
    }
}
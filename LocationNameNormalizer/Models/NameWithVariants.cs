using System.Collections.Generic;

namespace HappyTravel.LocationNameNormalizer.Models
{
    public class NameWithVariants
    {
        public string Primary { get; set; }
        public List<string> Variants { get; set; }
    }
}
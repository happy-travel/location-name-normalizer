using System.Collections.Generic;

namespace LocationNameNormalizer.Models
{
    public class NameWithVariants
    {
        public string Default { get; set; }
        public List<string> Variants { get; set; }
    }
}
using System.Collections.Generic;

namespace LocationNameNormalizer
{
    public interface ILocationNameNormalizer
    {
        string GetNormalizedCountryName(string countryName);

        string GetNormalizedLocalityName(string countryName, string localityName);

        List<string> GetCountryNames(string countryName);

        List<string> GetLocalityNames(string countryName, string localityName);
    }
}
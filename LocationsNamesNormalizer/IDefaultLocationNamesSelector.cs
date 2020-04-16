using System.Collections.Generic;

namespace LocationsNamesNormalizer
{
    public interface IDefaultLocationNamesSelector
    {
        string GetDefaultCountryName(string countryName);

        string GetDefaultLocalityName(string countryName, string localityName);

        List<string> GetCountryNames(string countryName);

        List<string> GetLocalityNames(string countryName, string localityName);
    }
}
using System.Collections.Generic;
using LocationsNamesNormalizer.Models;

namespace LocationsNamesNormalizer
{
    public interface ILocationNamesRetriever
    {
        List<Country> RetrieveCountries();
    }
}
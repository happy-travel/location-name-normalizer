using System.Collections.Generic;
using LocationNameNormalizer.Models;

namespace LocationNameNormalizer
{
    public interface ILocationNamesRetriever
    {
        List<Country> RetrieveCountries();
    }
}
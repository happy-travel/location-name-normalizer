using System.Collections.Generic;
using LocationNameNormalizer.Models;

namespace LocationNameNormalizer
{
    public interface ILocationNameRetriever
    {
        List<Country> RetrieveCountries();
    }
}
using System.Collections.Generic;
using HappyTravel.LocationNameNormalizer.Models;

namespace HappyTravel.LocationNameNormalizer
{
    public interface ILocationNameRetriever
    {
        List<Country> RetrieveCountries();
    }
}
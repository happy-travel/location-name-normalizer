namespace LocationsNamesNormalizer
{
    public interface IDefaultLocationNamesSelector
    {
        string GetDefaultCountryName(string countryName);

        string GetDefaultLocalityName(string countryName, string localityName);
    }
}
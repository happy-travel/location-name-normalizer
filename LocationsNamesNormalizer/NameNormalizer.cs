using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using LocationsNamesNormalizer.Extensions;

namespace LocationsNamesNormalizer
{
    public class NameNormalizer : INameNormalizer
    {
        public string Normalize(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            //if value is html encoded, after normalization html tags will be in uppercase,because we also want that value to be in titleCase
            return value.Trim()
                .ToHtmlDecoded()
                .Replace("&", "and")
                .ToStringWithoutSpecialCharacters()
                .ToTitleCase();
        }
       
    }
}
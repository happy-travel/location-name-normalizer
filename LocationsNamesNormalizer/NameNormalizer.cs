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

            return value.Trim()
                .ToHtmlDecoded()
                .ToStringWithoutHtmlTags()
                .Replace("&", "and")
                .ToStringWithoutSpecialCharacters()
                .ToTitleCase();
        }
    }
}
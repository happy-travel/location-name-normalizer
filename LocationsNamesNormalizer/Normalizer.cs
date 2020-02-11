using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace LocationsNamesNormalizer
{
    public static class Normalizer
    {
        public static string Normalize(string value)
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


        private static string ToTitleCase(this string value)
        {
            var textInfo = new CultureInfo(DefaultCultureName).TextInfo;

            return textInfo.ToTitleCase(value.ToLower());
        }


        private static string ToHtmlDecoded(this string value) => WebUtility.HtmlDecode(value);

        private static string ToStringWithoutSpecialCharacters(this string value) => Regex.Replace(value, SpecialCharactersProcessingPattern, "");

        private const string DefaultCultureName = "en-US";
        private const string SpecialCharactersProcessingPattern = "[^a-zA-Z0-9_.; ,<>/-]+";
    }
}
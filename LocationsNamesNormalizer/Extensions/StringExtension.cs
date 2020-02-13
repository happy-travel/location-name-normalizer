using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

namespace LocationsNamesNormalizer.Extensions
{
    internal static class StringExtension
    {
        public static string ToTitleCase(this string value)
        {
            var textInfo = new CultureInfo(DefaultCultureName).TextInfo;

            return textInfo.ToTitleCase(value.ToLower());
        }


        public static string ToHtmlDecoded(this string value) => WebUtility.HtmlDecode(value);

        public static string ToStringWithoutSpecialCharacters(this string value) => Regex.Replace(value, SpecialCharactersProcessingPattern, "");

        private const string DefaultCultureName = "en-US";
        private const string SpecialCharactersProcessingPattern = "[^a-zA-Z0-9_.; ,<>/-]+";
    }
}
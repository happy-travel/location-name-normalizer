using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

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


        public static string ToStringWithoutHtmlTags(this string value)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(value);
            if (doc.DocumentNode.Descendants().Any(n => n.NodeType != HtmlNodeType.Text))
                return Regex.Replace(value, HtmlTagsProcessingPattern, "", RegexOptions.Compiled);

            return value;
        }


        public static string ToStringWithoutSpecialCharacters(this string value)
            => Regex.Replace(value, SpecialCharactersProcessingPattern, "", RegexOptions.Compiled);


        private const string HtmlTagsProcessingPattern = "<.*?>";
        private const string DefaultCultureName = "en-US";
        private const string SpecialCharactersProcessingPattern = "[^a-zA-Z0-9_.; ,-]+";
    }
}
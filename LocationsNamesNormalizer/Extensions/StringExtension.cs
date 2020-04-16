using System.Collections.Generic;
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
            => ToTitleCase(new List<string>(1) {value})
                .SingleOrDefault();


        public static List<string> ToTitleCase(this List<string> values)
        {
            var textInfo = new CultureInfo(DefaultCultureName).TextInfo;
            return values.Select(v => textInfo.ToTitleCase(v)).ToList();
        }


        public static string ToHtmlDecoded(this string value) => WebUtility.HtmlDecode(value);


        public static string ToStringWithoutHtmlTags(this string value)
        {
            var document = new HtmlDocument();
            document.LoadHtml(value);
            if (document.DocumentNode.Descendants().Any(n => n.NodeType != HtmlNodeType.Text))
                return document.DocumentNode.InnerText;

            return value;
        }


        public static string ToStringWithoutSpecialCharacters(this string value)
            => Regex.Replace(value, SpecialCharactersProcessingPattern, "", RegexOptions.Compiled);


        private const string DefaultCultureName = "en-US";
        private const string SpecialCharactersProcessingPattern = @"[^\p{L}0-9_.(); ,-]+";
    }
}
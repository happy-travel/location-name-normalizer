using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LocationNameNormalizer.Extensions
{
    public static class StringExtensions
    {
        public static string ToNormalizedName(this string target)
        {
            if (string.IsNullOrEmpty(target))
                return string.Empty;

            return target.Trim()
                .ToHtmlDecoded()
                .ToStringWithoutHtmlTags()
                .Replace("&", "and")
                .ToStringWithoutSpecialCharacters()
                .ToTitleCase();
        }


        internal static string ToTitleCase(this string value)
            => ToTitleCase(new List<string>(1) {value})
                .SingleOrDefault();


        internal static List<string> ToTitleCase(this List<string> values)
        {
            var textInfo = new CultureInfo(DefaultCultureName).TextInfo;

            //ToLower() must be used, otherwise we get uppercase 
            return values.Select(v => textInfo.ToTitleCase(v.ToLower())).ToList();
        }


        internal static string ToHtmlDecoded(this string value) => WebUtility.HtmlDecode(value);


        internal static string ToStringWithoutHtmlTags(this string value)
        {
            var document = new HtmlDocument();
            document.LoadHtml(value);
            if (document.DocumentNode.Descendants().Any(n => n.NodeType != HtmlNodeType.Text))
                return document.DocumentNode.InnerText;

            return value;
        }


        internal static string ToStringWithoutSpecialCharacters(this string value)
            => Regex.Replace(value, SpecialCharactersProcessingPattern, "", RegexOptions.Compiled);


        private const string DefaultCultureName = "en-US";
        private const string SpecialCharactersProcessingPattern = @"[^\p{L}0-9_.(); ,-]+";
    }
}
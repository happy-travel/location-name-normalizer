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
        public static string ToNormalizedName(this string target, CultureInfo cultureInfo = default)
        {
            if (string.IsNullOrEmpty(target))
                return string.Empty;

            return target.Trim()
                .ToHtmlDecoded()
                .ToStringWithoutHtmlTags()
                .Replace("&", " and ")
                .ToStringWithoutSpecialCharacters()
                .ToStringWithoutMultipleWhitespaces()
                .ToStringWithoutSpacesBeforePunctuations()
                .ToTitleCase(cultureInfo);
        }


        internal static string ToTitleCase(this string value, CultureInfo cultureInfo = default)
            => ToTitleCase(new List<string>(1) { value }, cultureInfo)
                .SingleOrDefault();


        internal static List<string> ToTitleCase(this List<string> values, CultureInfo cultureInfo = default)
        {
            var culture = cultureInfo ?? new CultureInfo(DefaultCultureName);
            var textInfo = culture.TextInfo;

            //ToLower() must be used, otherwise we get uppercase 
            return values.Select(v => textInfo.ToTitleCase(v.ToLower(culture))).ToList();
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
            => Regex.Replace(value, SpecialCharactersProcessingPattern, " ", RegexOptions.Compiled | RegexOptions.CultureInvariant);


        internal static string ToStringWithoutMultipleWhitespaces(this string value)
            => Regex.Replace(value, MultipleSpacesProcessingPattern, " ", RegexOptions.Compiled | RegexOptions.CultureInvariant);


        internal static string ToStringWithoutSpacesBeforePunctuations(this string value)
            => Regex.Replace(value, SpacesBeforePunctuationsProcessingPattern, "", RegexOptions.Compiled | RegexOptions.CultureInvariant);


        private const string DefaultCultureName = "en-US";
        private const string SpecialCharactersProcessingPattern = @"[^\p{L}0-9_.(); ,'ʼ]+";
        private const string MultipleSpacesProcessingPattern = @"\s+";
        private const string SpacesBeforePunctuationsProcessingPattern = @"\s+(?=[_.();,'ʼ])";
    }
}
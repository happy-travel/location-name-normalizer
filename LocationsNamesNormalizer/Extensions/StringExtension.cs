using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
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
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(value);
            if (document.DocumentNode.Descendants().Any(n => n.NodeType != HtmlNodeType.Text))
            {
                return document.DocumentNode.InnerText;
            }

            return value;
        }


        public static string ToStringWithoutSpecialCharacters(this string value)
            => Regex.Replace(value, SpecialCharactersProcessingPattern, "", RegexOptions.Compiled);


        private const string DefaultCultureName = "en-US";
        private const string SpecialCharactersProcessingPattern = @"[^\p{L}0-9_.(); ,-]+";
    }
}
using LocationNameNormalizer.Extensions;

namespace LocationNameNormalizer
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
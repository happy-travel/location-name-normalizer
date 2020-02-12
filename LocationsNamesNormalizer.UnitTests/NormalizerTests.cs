using System.Net;
using Xunit;

namespace LocationsNamesNormalizer.UnitTests
{
    public class NormalizerTests
    {
        [Fact]
        public void UpperCase_strings_Should_Be_Normalized()
        {
            string notNormalizedName = " PIAZZALE ROMA, VENICE, ITALY ";

            var normalizedName = NameNormalizer.Normalize(notNormalizedName);

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void With_some_special_characters_strings_should_be_normalized()
        {
            string notNormalizedName = " PIAZZALE ROMA,!@? VENICE, ITALY ";
            var normalizedName = NameNormalizer.Normalize(notNormalizedName);

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void With_html_string_should_be_normalized()
        {
            string notNormalizedName = WebUtility.HtmlEncode("<b>PIAZZALE ROMA,!@? VENICE, ITALY</b>");
            var normalizedName = NameNormalizer.Normalize(notNormalizedName);
            //since normalizer return string in title case, html tags also will be in uppercase
            Assert.True(normalizedName == "<B>Piazzale Roma, Venice, Italy</B>");
        }
    }
}
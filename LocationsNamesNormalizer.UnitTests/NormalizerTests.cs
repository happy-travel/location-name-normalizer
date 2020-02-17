using System.Net;
using Xunit;

namespace LocationsNamesNormalizer.UnitTests
{
    public class NormalizerTests
    {
        public NormalizerTests()
        {
            _nameNormalizer = new NameNormalizer();
        }


        [Fact]
        public void UpperCase_strings_Should_Be_Normalized()
        {
            string notNormalizedName = " PIAZZALE ROMA, VENICE, ITALY ";

            var normalizedName = _nameNormalizer.Normalize(notNormalizedName);

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void With_some_special_characters_strings_should_be_normalized()
        {
            string notNormalizedName = " PIAZZALE ROMA,!@? VENICE, ITALY ";
            var normalizedName = _nameNormalizer.Normalize(notNormalizedName);

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void With_html_string_should_be_normalized()
        {
            string notNormalizedName = "&lt;b&gt;PIAZZALE ROMA<,!@? VENICE, ITALY&lt;/b&gt;";
            var normalizedName = _nameNormalizer.Normalize(notNormalizedName);

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        private readonly INameNormalizer _nameNormalizer;
    }
}
using Xunit;

namespace LocationNameNormalizer.UnitTests
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
            string notNormalizedName = "&lt;b&gt;PIAZZALE ROMA<,!@?©¶' VENICE, ITALY&lt;/b&gt;";
            var normalizedName = _nameNormalizer.Normalize(notNormalizedName);

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void Null_string_should_be_replaced_with_empty()
        {
            var normalizedName = _nameNormalizer.Normalize(null);
            Assert.True(normalizedName == string.Empty);
        }


        [Fact]
        public void Letters_from_any_language_should_not_be_removed()
        {
            string notNormalizedName = "ՎԵՆԵՏԻԿ, ԻՏԱԼԻԱ";
            var normalizedName = _nameNormalizer.Normalize(notNormalizedName);
            Assert.True(normalizedName == "Վենետիկ, Իտալիա");
        }


        private readonly INameNormalizer _nameNormalizer;
    }
}
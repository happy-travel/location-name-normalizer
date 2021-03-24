using Xunit;
using HappyTravel.LocationNameNormalizer.Extensions;

namespace LocationNameNormalizer.UnitTests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void UpperCase_strings_Should_Be_Normalized()
        {
            string notNormalizedName = " PIAZZALE ROMA, VENICE, ITALY ";

            var normalizedName = notNormalizedName.ToNormalizedName();

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void With_some_special_characters_strings_should_be_normalized()
        {
            string notNormalizedName = " PIAZZALE ROMA,!@? VENICE, ITALY ";
            var normalizedName = notNormalizedName.ToNormalizedName();

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void With_html_string_should_be_normalized()
        {
            string notNormalizedName = "&lt;b&gt;PIAZZALE ROMA<,!@?©¶ VENICE, ITALY&lt;/b&gt;";
            var normalizedName = notNormalizedName.ToNormalizedName();

            Assert.True(normalizedName == "Piazzale Roma, Venice, Italy");
        }


        [Fact]
        public void Null_string_should_be_replaced_with_empty()
        {
            var normalizedName = ((string)null).ToNormalizedName();
            Assert.True(normalizedName == string.Empty);
        }


        [Fact]
        public void Letters_from_any_language_should_not_be_removed()
        {
            string notNormalizedName = "ՎԵՆԵՏԻԿ, ԻՏԱԼԻԱ";
            var normalizedName = notNormalizedName.ToNormalizedName();
            Assert.True(normalizedName == "Վենետիկ, Իտալիա");
        }

        [Fact]
        public void Multiple_spaces_must_be_replaced_with_one()
        {
            string notNormalizedName = "Marina Residences      Palm Jumeriah Apartments";
            var normalizedName = notNormalizedName.ToNormalizedName();
            Assert.True(normalizedName == "Marina Residences Palm Jumeriah Apartments");
        }


        [Fact]
        public void Spaces_before_punctuations_should_be_removed()
        {
            string notNormalizedName = "Marina Residences Palm , Jumeriah Apartments";
            var normalizedName = notNormalizedName.ToNormalizedName();
            Assert.True(normalizedName == "Marina Residences Palm, Jumeriah Apartments");
        }
    }
}
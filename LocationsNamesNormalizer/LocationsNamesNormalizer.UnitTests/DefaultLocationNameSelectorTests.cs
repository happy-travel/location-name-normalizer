using Xunit;

namespace LocationsNamesNormalizer.UnitTests
{
    public class DefaultLocationNameSelectorTests
    {
        [Fact]
        public void Default_country_name_should_be_selected_right()
        {
            var defaultName = DefaultLocationNameSelector.GetDefaultCountryName("Great Britain");
            Assert.True(defaultName == "United Kingdom");
        }

        [Fact]
        public void Default_locality_name_should_be_selected_right()
        {
            var defaultName = DefaultLocationNameSelector.GetDefaultLocalityName("Great Britain", "TestName");
            Assert.True(defaultName == "London");
        }
    }
}
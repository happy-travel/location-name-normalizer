using Xunit;

namespace LocationsNamesNormalizer.UnitTests
{
    public class DefaultLocationNameSelectorTests
    {
        public DefaultLocationNameSelectorTests()
        {
            _defaultLocationNamesSelector = new DefaultLocationNameSelector(new NameNormalizer(), new LocationNamesRetriever());
        }


        [Fact]
        public void Default_country_name_should_be_selected_right()
        {
            var defaultName = _defaultLocationNamesSelector.GetDefaultCountryName("Great Britain");
            Assert.True(defaultName == "United Kingdom");
        }


        [Fact]
        public void Default_locality_name_should_be_selected_right()
        {
            var defaultName = _defaultLocationNamesSelector.GetDefaultLocalityName("Great Britain", "LONDON");
            Assert.True(defaultName == "London");
        }


        private readonly IDefaultLocationNamesSelector _defaultLocationNamesSelector;
    }
}
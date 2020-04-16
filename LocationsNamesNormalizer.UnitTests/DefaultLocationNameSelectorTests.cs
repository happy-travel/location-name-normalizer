using System.Linq;
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


        [Fact]
        public void Not_existing_locality_should_be_normalized()
        {
            var defaultName = _defaultLocationNamesSelector.GetDefaultLocalityName("ghgh", "hgh");
            Assert.True(defaultName == "Hgh");
        }


        [Fact]
        public void Not_existing_country_should_be_normalized()
        {
            var defaultName = _defaultLocationNamesSelector.GetDefaultCountryName("fhghgh");
            Assert.True(defaultName == "Fhghgh");
        }


        [Fact]
        public void All_country_names_should_be_returned()
        {
            var countriesByKeyName = _defaultLocationNamesSelector.GetCountryNames("Czechia");
            var countriesByNotKeyName = _defaultLocationNamesSelector.GetCountryNames("Czech Republic");

            Assert.Contains("Czechia", countriesByKeyName);
            Assert.Contains("Czech Republic", countriesByKeyName);
            Assert.Contains("The Czech Republic", countriesByKeyName);
            Assert.True(countriesByKeyName.SequenceEqual(countriesByNotKeyName));
        }

        
        [Fact]
        public void All_locality_names_should_be_returned()
        {
            var localityNamesByKeyName= _defaultLocationNamesSelector.GetLocalityNames("Russia", "Moscow");
            var localityNamesByNotKeyName = _defaultLocationNamesSelector.GetLocalityNames("Russian federation", "Moskwa");
            
            Assert.Contains("Moscow", localityNamesByKeyName);
            Assert.Contains("Moskwa", localityNamesByKeyName);
            Assert.True( localityNamesByKeyName.SequenceEqual(localityNamesByNotKeyName));
        }
        
        private readonly IDefaultLocationNamesSelector _defaultLocationNamesSelector;
    }
}
using System.Collections.Generic;
using System.Linq;
using LocationNameNormalizer.Models;
using Moq;
using Xunit;

namespace LocationNameNormalizer.UnitTests
{
    public class DefaultLocationNameNormalizerTests
    {
        public DefaultLocationNameNormalizerTests()
        {
            var locationNameRetrieverMock = new Mock<ILocationNameRetriever>();
            locationNameRetrieverMock.Setup(m => m.RetrieveCountries()).Returns(new List<Country>
            {
                new Country
                {
                    KeyName = "THE UNITED KINGDOM",
                    Names = new List<string>
                    {
                        "THE UNITED KINGDOM",
                        "UNITED KINGDOM",
                        "THE UNITED KINGDOM OF GREAT BRITAIN AND NORTHERN IRELAND",
                        "UNITED KINGDOM OF GREAT BRITAIN AND NORTHERN IRELAND",
                        "GREAT BRITAIN"
                    },
                    Localities = new List<Locality>
                    {
                        {
                            new Locality
                            {
                                KeyName = "LONDON",
                                Names = new List<string> {"LONDON"}
                            }
                        }
                    }
                },
                new Country
                {
                    KeyName = "THE RUSSIAN FEDERATION",
                    Names = new List<string>
                    {
                        "THE RUSSIAN FEDERATION",
                        "RUSSIAN FEDERATION",
                        "RUSSIA"
                    },
                    Localities = new List<Locality>
                    {
                        new Locality
                        {
                            KeyName = "MOSCOW",
                            Names = new List<string>
                            {
                                "MOSCOW",
                                "MOSKWA",
                                "MOSKVA"
                            }
                        }
                    }
                },
                new Country
                {
                    KeyCode = "CZ",
                    Codes = new List<string> {"CZ", "C"},
                    KeyName = "THE CZECH REPUBLIC",
                    Names = new List<string>
                    {
                        "THE CZECH REPUBLIC",
                        "CZECH REPUBLIC",
                        "CZECHIA"
                    },
                    Localities = new List<Locality>
                    {
                        new Locality
                        {
                            KeyName = "PRAGUE",
                            Names = new List<string> {"PRAGUE"}
                        }
                    }
                }
            });
            _defaultLocationNamesSelector = new DefaultLocationNameNormalizer(locationNameRetrieverMock.Object);
            ((DefaultLocationNameNormalizer) _defaultLocationNamesSelector).Init();
        }


        [Fact]
        public void Default_country_name_should_be_selected_right()
        {
            var defaultName = _defaultLocationNamesSelector.GetNormalizedCountryName("Great Britain");
            Assert.True(defaultName == "The United Kingdom");
        }


        [Fact]
        public void Default_locality_name_should_be_selected_right()
        {
            var defaultName = _defaultLocationNamesSelector.GetNormalizedLocalityName("Great Britain", "LONDON");
            Assert.True(defaultName == "London");
        }


        [Fact]
        public void Not_existing_locality_should_be_normalized()
        {
            var defaultName = _defaultLocationNamesSelector.GetNormalizedLocalityName("ghgh", "hgh");
            Assert.True(defaultName == "Hgh");
        }


        [Fact]
        public void Not_existing_country_should_be_normalized()
        {
            var defaultName = _defaultLocationNamesSelector.GetNormalizedCountryName("fhghgh");
            Assert.True(defaultName == "Fhghgh");
        }


        [Fact]
        public void All_country_names_should_be_returned()
        {
            var countriesByKeyName = _defaultLocationNamesSelector.GetCountryNames("The Czech Republic");
            var countriesByNotKeyName = _defaultLocationNamesSelector.GetCountryNames("Czech Republic");

            Assert.Contains("Czechia", countriesByKeyName);
            Assert.Contains("Czech Republic", countriesByKeyName);
            Assert.Contains("The Czech Republic", countriesByKeyName);
            Assert.True(countriesByKeyName.SequenceEqual(countriesByNotKeyName));
        }


        [Fact]
        public void All_locality_names_should_be_returned()
        {
            var localityNamesByKeyName = _defaultLocationNamesSelector.GetLocalityNames("Russia", "Moscow");
            var localityNamesByNotKeyName = _defaultLocationNamesSelector.GetLocalityNames("Russian federation", "Moskwa");

            Assert.Contains("Moscow", localityNamesByKeyName);
            Assert.Contains("Moskwa", localityNamesByKeyName);
            Assert.True(localityNamesByKeyName.SequenceEqual(localityNamesByNotKeyName));
        }

        [Fact]
        public void Default_country_code_should_be_selected_right()
        {
            var defaultCountryCode = _defaultLocationNamesSelector.GetNormalizedCountryCode("The Czech Republic", "C");
            Assert.True(defaultCountryCode == "CZ");
        }

        [Fact]
        public void Not_existing_country_should_return_passed_code()
        {
            var defaultCountryCode = _defaultLocationNamesSelector.GetNormalizedCountryCode("Italy", "I");
            Assert.True(defaultCountryCode == "I");
        }
        

        private readonly ILocationNameNormalizer _defaultLocationNamesSelector;
    }
}
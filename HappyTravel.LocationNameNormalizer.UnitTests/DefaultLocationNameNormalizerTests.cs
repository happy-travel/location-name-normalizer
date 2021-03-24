using System.Collections.Generic;
using System.Linq;
using HappyTravel.LocationNameNormalizer.Models;
using Moq;
using Xunit;

namespace HappyTravel.LocationNameNormalizer.UnitTests
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
                    Name = new NameWithVariants
                    {
                        Primary = "The United Kingdom",
                        Variants =  new List<string>
                        {
                            "The United Kingdom",
                            "United Kingdom",
                            "The United Kingdom Of Great Britain And Norther Ireland",
                            "United Kingdom Of Great Britain And Norther Ireland",
                            "Great Britain"
                        }
                    },
                    Localities = new List<Locality>
                    {
                        {
                            new Locality
                            {
                               Name = new NameWithVariants
                               {
                                   Primary = "London",
                                   Variants =  new List<string> {"London"}
                               }
                            }
                        }
                    }
                },
                new Country
                {
                    Name = new NameWithVariants
                    {
                        Primary = "The Russian Federation",
                        Variants =  new List<string>
                        {
                            "The Russian Federation",
                            "Russian Federation",
                            "Russia"
                        }
                    },
                    Localities = new List<Locality>
                    {
                        new Locality
                        {
                            Name = new NameWithVariants
                            {
                                Primary = "Moscow",
                                Variants = new List<string>
                                {
                                    "Moscow",
                                    "Moskwa",
                                    "Moskva"
                                }
                            }
                        }
                    }
                },
                new Country
                {
                    Code = new NameWithVariants
                    {
                        Primary = "CZ",
                        Variants = new List<string> {"CZ", "C"}
                    },
                    Name = new NameWithVariants
                    {
                        Primary = "The Czech Republic",
                        Variants = new List<string>
                        {
                            "The Czech Republic",
                            "Czech Republic",
                            "Czechia"
                        }
                    },
                    Localities = new List<Locality>
                    {
                        new Locality
                        {
                            Name = new NameWithVariants
                            {
                                Primary = "Prague",
                                Variants = new List<string> {"Prague"}
                            }
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
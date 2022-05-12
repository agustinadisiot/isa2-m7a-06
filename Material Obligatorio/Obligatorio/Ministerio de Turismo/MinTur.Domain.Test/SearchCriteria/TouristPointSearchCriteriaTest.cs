using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MinTur.Domain.Test.SearchCriteria
{
    [TestClass]
    public class TouristPointSearchCriteriaTest
    {
        [TestMethod]
        public void SearchCriteriaAcceptsByCategoriesWithNoRegionId() 
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                Categories = new List<Category>()
                {
                    new Category() { Id = 1, Name = "Ciudades" },
                    new Category() { Id = 2, Name = "Pueblos" }
                }
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este"
            };
            touristPoint.AddCategory(new Category() { Id = 1, Name = "Ciudades" });
            touristPoint.AddCategory(new Category() { Id = 2, Name = "Pueblos" });

            Assert.IsTrue(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void SearchCriteriaDeniesByCategoriesWithNoRegionId() 
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                Categories = new List<Category>()
                {
                    new Category() { Id = 1, Name = "Ciudades" },
                    new Category() { Id = 2, Name = "Pueblos" }
                }
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este"
            };
            touristPoint.AddCategory(new Category() { Id = 5, Name = "Playas" });
            touristPoint.AddCategory(new Category() { Id = 1, Name = "Ciudades" });

            Assert.IsFalse(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void EmptySearchCriteriaAcceptsAnyTouristPoint()
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria();
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                RegionId = 65,
                Name = "Florida"
            };
            touristPoint.AddCategory(new Category() { Id = 5, Name = "Pueblos" });

            Assert.IsTrue(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void SearchCriteriaAcceptsByRegionIdWithoutCategories()
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                RegionId = 5
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este",
                RegionId = 5
            };

            Assert.IsTrue(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void SearchCriteriaDeniesByRegionIdWithoutCategories()
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                RegionId = 9
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este",
                RegionId = 5
            };

            Assert.IsFalse(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void SearchCriteriaAcceptsByRegionIdAndCategories()
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                Categories = new List<Category>()
                {
                    new Category() { Id = 1, Name = "Ciudades" },
                    new Category() { Id = 2, Name = "Pueblos" }
                },
                RegionId = 7
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este",
                RegionId = 7
            };
            touristPoint.AddCategory(new Category() { Id = 1, Name = "Ciudades" });
            touristPoint.AddCategory(new Category() { Id = 2, Name = "Pueblos" });

            Assert.IsTrue(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void SearchCriteriaDeniesBecauseItMatchesRegionIdButNotTheCategories()
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                Categories = new List<Category>()
                {
                    new Category() { Id = 1, Name = "Ciudades" },
                    new Category() { Id = 2, Name = "Pueblos" }
                },
                RegionId = 7
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este",
                RegionId = 7
            };
            touristPoint.AddCategory(new Category() { Id = 1, Name = "Ciudades" });

            Assert.IsFalse(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void SearchCriteriaDeniesBecauseItMatchesCategoriesButNotTheRegionId()
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                Categories = new List<Category>()
                {
                    new Category() { Id = 1, Name = "Ciudades" }
                },
                RegionId = 34
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este",
                RegionId = 7
            };
            touristPoint.AddCategory(new Category() { Id = 1, Name = "Ciudades" });

            Assert.IsFalse(searchCriteria.MatchesCriteria(touristPoint));
        }

        [TestMethod]
        public void SearchCriteriaDeniesBecauseItDoesntMatchAny()
        {
            TouristPointSearchCriteria searchCriteria = new TouristPointSearchCriteria()
            {
                Categories = new List<Category>()
                {
                    new Category() { Id = 1, Name = "Ciudades" },
                    new Category() { Id = 2, Name = "Pueblos" }
                },
                RegionId = 34
            };
            TouristPoint touristPoint = new TouristPoint()
            {
                Id = 4,
                Name = "Punta del Este",
                RegionId = 7
            };
            touristPoint.AddCategory(new Category() { Id = 1, Name = "Ciudades" });

            Assert.IsFalse(searchCriteria.MatchesCriteria(touristPoint));
        }
    }
}

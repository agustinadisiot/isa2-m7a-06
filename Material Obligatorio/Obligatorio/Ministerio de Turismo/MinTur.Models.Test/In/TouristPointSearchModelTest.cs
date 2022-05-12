using MinTur.Domain.BusinessEntities;
using MinTur.Domain.SearchCriteria;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class TouristPointSearchModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpectedWithCategories() 
        {
            TouristPointSearchModel model = new TouristPointSearchModel();
            model.CategoriesId.Add(2);
            model.CategoriesId.Add(3);
            model.CategoriesId.Add(4);

            TouristPointSearchCriteria entity = model.ToEntity();

            Assert.IsTrue(model.CategoriesId.Count == entity.Categories.Count);
            foreach (Category category in entity.Categories)
            {
                Assert.IsTrue(model.CategoriesId.Contains(category.Id));
            }
        }

        [TestMethod]
        public void ToEntityReturnsAsExpectedWithNoCategories()
        {
            TouristPointSearchModel model = new TouristPointSearchModel();

            TouristPointSearchCriteria entity = model.ToEntity();

            Assert.IsTrue(entity.Categories.Count == 0);
        }

    }
}

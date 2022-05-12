using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class TouristPointIntentModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected()
        {
            TouristPointIntentModel touristPointIntentModel = new TouristPointIntentModel()
            {
                Name = "Punta del este",
                Description = "Descripyion...",
                RegionId = 3,
                Image = "jsafigrehgeruhwdjiw",
                CategoriesId = new List<int>() { 1, 3, 84 }
            };
            TouristPoint touristPoint = touristPointIntentModel.ToEntity();

            Assert.AreEqual(touristPointIntentModel.Name, touristPoint.Name);
            Assert.AreEqual(touristPointIntentModel.Description, touristPoint.Description);
            Assert.AreEqual(touristPointIntentModel.RegionId, touristPoint.RegionId);
            Assert.AreEqual(touristPointIntentModel.Image, touristPoint.Image.Data);
            CollectionAssert.AreEquivalent(touristPointIntentModel.CategoriesId,
                touristPoint.TouristPointCategory.Select(tc => tc.CategoryId).ToList());
        }
    }
}

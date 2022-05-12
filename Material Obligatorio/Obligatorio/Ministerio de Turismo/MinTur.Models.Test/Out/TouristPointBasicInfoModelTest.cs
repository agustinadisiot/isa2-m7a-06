using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;
using System.Linq;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class TouristPointBasicInfoModelTest
    {
        [TestMethod]
        public void TouristPointBasicInfoModelGetsCreatedAsExpected()
        {
            Region region = new Region() { Id = 1, Name = "Metropolitana" };
            Category category1 = new Category() { Id = 1, Name = "Ciudades" };
            Category category2 = new Category() { Id = 2, Name = "Playas" };
            TouristPoint touristPoint = new TouristPoint 
            {
                Id = 5, 
                Name = "Punta del Este",
                Description = "Lugar muy lindo",
                RegionId = region.Id,
                Region = region,
                Image = new Image() { Id = 1, Data = "Imagen 1" }
            };
            touristPoint.AddCategory(category1);
            touristPoint.AddCategory(category2);

            TouristPointBasicInfoModel touristPointBasicInfoModel = new TouristPointBasicInfoModel(touristPoint);

            Assert.IsTrue(touristPoint.Id == touristPointBasicInfoModel.Id);
            Assert.IsTrue(touristPoint.Name == touristPointBasicInfoModel.Name);
            Assert.IsTrue(touristPoint.Description == touristPointBasicInfoModel.Description);
            Assert.IsTrue(touristPoint.Image.Data == touristPointBasicInfoModel.Image.Data);
            Assert.IsTrue(touristPointBasicInfoModel.Region.Id == touristPoint.Region.Id);
            Assert.IsTrue(touristPoint.TouristPointCategory.Count == touristPointBasicInfoModel.Categories.Count);
            Assert.IsTrue(touristPoint.TouristPointCategory.Select(tc => tc.CategoryId).
                Except(touristPointBasicInfoModel.Categories.Select(c => c.Id)).Count() == 0);
        }
    }
}

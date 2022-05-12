using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;

namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class RegionBasicInfoModelTest
    {
        [TestMethod]
        public void RegionBasicInfoModelGetsCreatedAsExpected() 
        {
            Region region = new Region { Id = 5, Name = "Metropolitana" };
            RegionBasicInfoModel regionBasicInfoModel = new RegionBasicInfoModel(region);

            Assert.IsTrue(region.Id == regionBasicInfoModel.Id);
            Assert.IsTrue(region.Name == regionBasicInfoModel.Name);
        }
    }
}

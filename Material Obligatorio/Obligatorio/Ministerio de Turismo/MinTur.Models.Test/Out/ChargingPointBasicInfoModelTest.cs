using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.Out;


namespace MinTur.Models.Test.Out
{
    [TestClass]
    public class ChargingPointBasicInfoModelTest
    {
        [TestMethod]
        public void ChargingPointBasicInfoModelGetsCreatedAsExpected()
        {
            Region region = new Region() { Id = 1, Name = "Metropolitana" };
            ChargingPoint chargingPoint = new ChargingPoint 
            {
                Id = 5, 
                Name = "Punta del Este",
                Description = "Lugar muy lindo",
                RegionId = region.Id,
                Region = region,
                Address = new string('a', 20)
            };

            ChargingPointBasicInfoModel chargingPointBasicInfoModel = new ChargingPointBasicInfoModel(chargingPoint);

            Assert.IsTrue(chargingPoint.Id == chargingPointBasicInfoModel.Id);
            Assert.IsTrue(chargingPoint.Name == chargingPointBasicInfoModel.Name);
            Assert.IsTrue(chargingPoint.Description == chargingPointBasicInfoModel.Description);
            Assert.IsTrue(chargingPoint.Address == chargingPointBasicInfoModel.Address);
            Assert.IsTrue(chargingPoint.Region.Id == chargingPointBasicInfoModel.Region.Id );
        }
    }
}

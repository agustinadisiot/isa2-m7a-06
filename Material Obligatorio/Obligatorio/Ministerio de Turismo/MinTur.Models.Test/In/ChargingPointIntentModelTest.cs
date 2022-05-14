using MinTur.Domain.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Models.In;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class ChargingPointIntentModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected()
        {
            ChargingPointIntentModel chargingPointIntentModel = new ChargingPointIntentModel()
            {
                Name = "Punta del este",
                Description = "Descripyion...",
                RegionId = 3,
                Address = "jsafigrehgeruhwdjiw",
            };
            ChargingPoint chargingPoint = chargingPointIntentModel.ToEntity();

            Assert.AreEqual(chargingPointIntentModel.Name, chargingPoint.Name);
            Assert.AreEqual(chargingPointIntentModel.Description, chargingPoint.Description);
            Assert.AreEqual(chargingPointIntentModel.RegionId, chargingPoint.RegionId);
            Assert.AreEqual(chargingPointIntentModel.Address, chargingPoint.Address);
        }
    }
}

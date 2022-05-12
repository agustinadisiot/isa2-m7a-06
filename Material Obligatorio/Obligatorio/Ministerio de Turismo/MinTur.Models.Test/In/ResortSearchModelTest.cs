using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinTur.Domain.SearchCriteria;
using MinTur.Models.In;

namespace MinTur.Models.Test.In
{
    [TestClass]
    public class ResortSearchModelTest
    {
        [TestMethod]
        public void ToEntityReturnsAsExpected() 
        {
            ResortSearchModel resortSearchModel = new ResortSearchModel()
            {
                Available = true,
                TouristPointId = 3,
                ClientSearch = true
            };
            ResortSearchCriteria resortSearchCriteria = resortSearchModel.ToEntity();

            Assert.AreEqual(resortSearchModel.TouristPointId, resortSearchCriteria.TouristPointId);
            Assert.AreEqual(resortSearchModel.Available, resortSearchCriteria.Available);
        }

        [TestMethod]
        public void ToEntityReturnsAsExpectedWithoutTouristPointId()
        {
            ResortSearchModel resortSearchModel = new ResortSearchModel()
            {
                Available = true,
                ClientSearch = true
            };
            ResortSearchCriteria resortSearchCriteria = resortSearchModel.ToEntity();

            Assert.IsNull(resortSearchCriteria.TouristPointId);
            Assert.AreEqual(resortSearchModel.Available, resortSearchCriteria.Available);
        }

        [TestMethod]
        public void ToEntityReturnsAsExpectedWithoutAvailable()
        {
            ResortSearchModel resortSearchModel = new ResortSearchModel()
            {
                TouristPointId = 3,
                ClientSearch = true
            };
            ResortSearchCriteria resortSearchCriteria = resortSearchModel.ToEntity();

            Assert.IsNull(resortSearchCriteria.Available);
            Assert.AreEqual(resortSearchModel.TouristPointId, resortSearchCriteria.TouristPointId);
        }
    }
}

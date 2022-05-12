using MinTur.Domain.BusinessEntities;
using MinTur.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace MinTur.Domain.Test.BusinessEntities
{
    [TestClass]
    public class ChargingPointTest
    {
        [TestMethod]
        public void ChargingPointValid()
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = "Punta Este Estacion2",
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = "PuertoDePuntaDelEsteYMaldonadoPuertoDePuntaDelEsteYMaldonado",
            };
            Assert.AreEqual(1234, chargingPoint.Id);
            Assert.AreEqual("Punta Este Estacion2", chargingPoint.Name);
            Assert.AreEqual("PuertoDePuntaDelEsteYMaldonado", chargingPoint.Address);
            Assert.AreEqual(3, chargingPoint.RegionId);
            Assert.AreEqual("PuertoDePuntaDelEsteYMaldonadoPuertoDePuntaDelEsteYMaldonado", chargingPoint.Description);
        }

    }
}
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
        
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ChargingPointWithInvalidIdFailsValidation() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 12345,
                Name = "Valid",
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = new string('a', 60 ),
            };
            chargingPoint.ValidOrFail();
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ChargingPointWithInvalidNameFailsValidation() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = "Inva_lid! Name**",
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = new string('a', 60 ),
            };
            chargingPoint.ValidOrFail();
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ChargingPointWithInvalidNameTooLongFails() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = new string('a', 22 ),
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = new string('a', 60 ),
            };
            chargingPoint.ValidOrFail();
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ChargingPointWithInvalidDescriptionTooLongFails() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = new string('a', 20 ),
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = new string('a', 63 ),
            };
            chargingPoint.ValidOrFail();
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ChargingPointWithInvalidDescriptionFailsValidation() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = "Valid Name",
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = new string('a', 10 ) + "_!***",
            };
            chargingPoint.ValidOrFail();
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ChargingPointWithInvalidAddressTooLongFails() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = new string('a', 20 ),
                Address = new string('a', 33 ),
                RegionId = 3,
                Description = new string('a', 60 ),
            };
            chargingPoint.ValidOrFail();
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidRequestDataException))]
        public void ChargingPointWithInvalidAddressFailsValidation() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = "Valid Name",
                Address = "!!Invalid_**",
                RegionId = 3,
                Description = new string('a', 10 ) + "_!***",
            };
            chargingPoint.ValidOrFail();
        }

    }
}
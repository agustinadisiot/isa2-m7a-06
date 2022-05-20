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
                Region = new Region{Name = "Metropolitano", Id = 3}
            };

            Assert.AreEqual(1234, chargingPoint.Id);
            Assert.AreEqual("Punta Este Estacion2", chargingPoint.Name);
            Assert.AreEqual("PuertoDePuntaDelEsteYMaldonado", chargingPoint.Address);
            Assert.AreEqual(3, chargingPoint.RegionId);
            Assert.AreEqual("PuertoDePuntaDelEsteYMaldonadoPuertoDePuntaDelEsteYMaldonado", chargingPoint.Description);
            Assert.AreEqual(chargingPoint.RegionId, chargingPoint.Region.Id);

        }
        
        [TestMethod]
        public void RegionWithChargingPoints()
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1234,
                Name = "Punta Este Estacion2",
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = "PuertoDePuntaDelEsteYMaldonadoPuertoDePuntaDelEsteYMaldonado"
            };

            Region region = new Region { Name = "Metro", Id = 3 };
            region.ChargingPoints.Add(chargingPoint);
            ChargingPoint newChargingPoint = region.ChargingPoints.FirstOrDefault(c => c.Id == chargingPoint.Id);
            Assert.AreEqual(newChargingPoint.Id, chargingPoint.Id);

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
            chargingPoint.ValidateId();
        }
        
        [TestMethod]
        public void ChargingPointWithValidIdFailsValidation2() 
        {
            ChargingPoint chargingPoint = new ChargingPoint()
            {
                Id = 1,
                Name = "Valid",
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = 3,
                Description = new string('a', 60 ),
            };
            chargingPoint.ValidateId();
            Assert.AreEqual(chargingPoint.Id, 1);
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
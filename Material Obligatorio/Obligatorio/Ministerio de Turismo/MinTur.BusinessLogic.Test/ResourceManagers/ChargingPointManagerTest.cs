using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogic.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;
using MinTur.Domain.SearchCriteria;

namespace MinTur.BusinessLogic.Test.ResourceManagers
{
    [TestClass]
    public class ChargingPointManagerTest
    {
        private List<ChargingPoint> _chargingPoints;
        private List<Resort> _resorts;
        private Mock<IRepositoryFacade> _repositoryFacadeMock;
        private Mock<ChargingPoint> _chargingPointMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _chargingPoints = new List<ChargingPoint>();
            _resorts = new List<Resort>();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);
            _chargingPointMock = new Mock<ChargingPoint>(MockBehavior.Strict);

            LoadChargingPoints();
        }
        
        private void LoadChargingPoints()
        {
            Region region1 = new Region() { Id = 0, Name = "Metropolitana" };
            Region region2 = new Region() { Id = 1, Name = "Centro Sur" };

            ChargingPoint chargingPoint1 = new ChargingPoint()
            {
                Id = 1234,
                Name = "Ancap Mdeo",
                Address = "PuertoDeMontevideo",
                RegionId = region1.Id,
                Description = new string('a', 60 ),
                Region = region1
            };
            
            ChargingPoint chargingPoint2 = new ChargingPoint()
            {
                Id = 4321,
                Name = "Ancap Maldonado",
                Address = "PuertoDePuntaDelEsteYMaldonado",
                RegionId = region2.Id,
                Description = new string('a', 60 ),
                Region = region2
            };
        }
        #endregion
        
        

        [TestMethod]
        public void RegisterChargingPointReturnsAsExpected() 
        {
            int chargingPointId = 9494;
            ChargingPoint chargingPointWithSpecificId = CreateChargingPointWithSpecificId(chargingPointId);

            _chargingPointMock.Setup(t => t.ValidOrFail());
            _repositoryFacadeMock.Setup(r => r.StoreChargingPoint(_chargingPointMock.Object)).Returns(chargingPointId);

            ChargingPointManager chargingPointManager = new ChargingPointManager(_repositoryFacadeMock.Object);
            ChargingPoint registerChargingPoint = chargingPointManager.RegisterChargingPoint(_chargingPointMock.Object);

            _chargingPointMock.VerifyAll();
            _repositoryFacadeMock.VerifyAll();
            Assert.AreEqual(chargingPointWithSpecificId, registerChargingPoint);
        }

        #region Helpers
        public ChargingPoint CreateChargingPointWithSpecificId(int chargingPointId) 
        {
            return new ChargingPoint()
            {
                Id = chargingPointId,
                Name = "Ancap Colonia",
                Address = "Cerca de Tarariras",
                RegionId = 3,
                Description = new string('a', 60 ),
                Region = new Region()
                {
                    Id = 3,
                    Name = "Metropolitana"
                },
            };
        }
        #endregion
    }
}
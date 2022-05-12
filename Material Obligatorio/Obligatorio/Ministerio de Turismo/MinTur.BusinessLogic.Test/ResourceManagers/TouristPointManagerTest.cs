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
    public class TouristPointManagerTest
    {
        private List<TouristPoint> _touristPoints;
        private List<Resort> _resorts;
        private Mock<IRepositoryFacade> _repositoryFacadeMock;
        private Mock<TouristPoint> _touristPointMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _touristPoints = new List<TouristPoint>();
            _resorts = new List<Resort>();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);
            _touristPointMock = new Mock<TouristPoint>(MockBehavior.Strict);

            LoadTouristPoints();
        }

        private void LoadTouristPoints()
        {
            Region region1 = new Region() { Id = 0, Name = "Metropolitana" };
            Region region2 = new Region() { Id = 1, Name = "Centro Sur" };

            TouristPoint touristPoint1 = new TouristPoint()
            {
                Id = 0,
                Name = "Punta Del Este",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 1, Data = "sgdfgersrth" },
                RegionId = region1.Id,
                Region = region1,
            };

            TouristPoint touristPoint2 = new TouristPoint()
            {
                Id = 1,
                Name = "Cabo Polonio",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 2, Data = "gfdhthtrjy" },
                RegionId = region2.Id,
                Region = region2,
            };

        }
        #endregion

        [TestMethod]
        public void GetAllTouristPointsByMatchingCriteriaReturnedAsExpected()
        {
            TouristPointSearchCriteria touristPointSearchCriteria = new TouristPointSearchCriteria();
            _repositoryFacadeMock.Setup(r => r.GetAllTouristPointsByMatchingCriteria(touristPointSearchCriteria.MatchesCriteria)).Returns(_touristPoints);
            TouristPointManager touristPointManager = new TouristPointManager(_repositoryFacadeMock.Object);

            List<TouristPoint> retrievedTouristPoints = touristPointManager.GetAllTouristPointsByMatchingCriteria(touristPointSearchCriteria);
            _repositoryFacadeMock.VerifyAll();
            CollectionAssert.AreEquivalent(retrievedTouristPoints, _touristPoints);
        }

        [TestMethod]
        public void GetTouristPointByIdReturnsAsExpected()
        {
            TouristPoint touristPointToFind = new TouristPoint()
            {
                Id = 1,
                Name = "Punta del Este",
                Region = new Region()
                {
                    Id = 0,
                    Name = "Metropolitana"
                },
                Description = "Test"
            };
            _repositoryFacadeMock.Setup(r => r.GetTouristPointById(It.IsAny<int>())).Returns(touristPointToFind);

            TouristPointManager touristPointManager = new TouristPointManager(_repositoryFacadeMock.Object);
            TouristPoint retrievedTouristPoint = touristPointManager.GetTouristPointById(touristPointToFind.Id);

            _repositoryFacadeMock.VerifyAll();
            Assert.IsTrue(retrievedTouristPoint.Equals(touristPointToFind));
        }


        [TestMethod]
        public void RegisterTouristPointReturnsAsExpected() 
        {
            int newTouristPointId = 94;
            TouristPoint createdTouristPoint = CreateTouristPointWithSpecificId(newTouristPointId);

            _touristPointMock.Setup(t => t.ValidOrFail());
            _repositoryFacadeMock.Setup(r => r.StoreTouristPoint(_touristPointMock.Object)).Returns(newTouristPointId);
            _repositoryFacadeMock.Setup(r => r.GetTouristPointById(newTouristPointId)).Returns(createdTouristPoint);

            TouristPointManager touristPointManager = new TouristPointManager(_repositoryFacadeMock.Object);
            TouristPoint retrievedTouristPoint = touristPointManager.RegisterTouristPoint(_touristPointMock.Object);

            _touristPointMock.VerifyAll();
            _repositoryFacadeMock.VerifyAll();
            Assert.AreEqual(createdTouristPoint, retrievedTouristPoint);
        }

        #region Helpers
        public TouristPoint CreateTouristPointWithSpecificId(int touristPointId) 
        {
            return new TouristPoint()
            {
                Id = touristPointId,
                Name = "Punta del Este",
                Region = new Region()
                {
                    Id = 3,
                    Name = "Metropolitana"
                },
                Description = "Descripcion...",
                RegionId = 3,
                Image = new Image() { Id = 3, Data = "jsafigrehgeruhwdjiw" }
            };
        }
        public void LoadResorts(TouristPoint relatedTouristPoint)
        {
            Resort resort1 = new Resort()
            {
                Id = 3,
                Address = "Direccion",
                Description = "Descripcion ....",
                Name = "Hotel Italiano",
                PricePerNight = 520,
                Stars = 4,
                TouristPoint = relatedTouristPoint,
                TouristPointId = relatedTouristPoint.Id
            };
            Resort resort2 = new Resort()
            {
                Id = 9,
                Address = "Direccion 2",
                Description = "Descripcion 2 ....",
                Name = "Hotel Aleman",
                PricePerNight = 330,
                Stars = 5,
                TouristPoint = relatedTouristPoint,
                TouristPointId = relatedTouristPoint.Id
            };

            relatedTouristPoint.Resorts.Add(resort1);
            relatedTouristPoint.Resorts.Add(resort2);
            _resorts.Add(resort1);
            _resorts.Add(resort2);
        }
        #endregion
    }
}
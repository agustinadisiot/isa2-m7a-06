using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogic.ResourceManagers;
using MinTur.DataAccessInterface.Facades;
using MinTur.Domain.BusinessEntities;
using System.Collections.Generic;

namespace MinTur.BusinessLogic.Test.ResourceManagers
{
    [TestClass]
    public class RegionManagerTest
    {
        private List<Region> _regions;
        private List<TouristPoint> _touristPoints;
        private Mock<IRepositoryFacade> _repositoryFacadeMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _regions = new List<Region>();
            _touristPoints = new List<TouristPoint>();
            _repositoryFacadeMock = new Mock<IRepositoryFacade>(MockBehavior.Strict);

            LoadRegions();
        }

        private void LoadRegions()
        {
            Region region1 = new Region() { Id = 0, Name = "Metropolitana" };
            Region region2 = new Region() { Id = 1, Name = "Centro Sur" };
            Region region3 = new Region() { Id = 2, Name = "Litoral Norte" };
            Region region4 = new Region() { Id = 3, Name = "Corredor Pájaros Pintados" };

            _regions.Add(region1);
            _regions.Add(region2);
            _regions.Add(region3);
            _regions.Add(region4);
        }
        #endregion

        [TestMethod]
        public void GetAllRegionsReturnsAsExpected()
        {
            _repositoryFacadeMock.Setup(r => r.GetAllRegions()).Returns(_regions);

            RegionManager regionManager = new RegionManager(_repositoryFacadeMock.Object);
            List<Region> retrievedRegions = regionManager.GetAllRegions();

            _repositoryFacadeMock.VerifyAll();
            CollectionAssert.AreEquivalent(retrievedRegions, _regions);
        }

        [TestMethod]
        public void GetRegionByIdReturnsAsExpected()
        {
            Region regionToFind = new Region() { Id = 3, Name = "Metropolitana" };
            _repositoryFacadeMock.Setup(r => r.GetRegionById(regionToFind.Id)).Returns(regionToFind);

            RegionManager regionManager = new RegionManager(_repositoryFacadeMock.Object);
            Region retrievedRegion = regionManager.GetRegionById(regionToFind.Id);

            _repositoryFacadeMock.VerifyAll();
            Assert.IsTrue(retrievedRegion.Equals(regionToFind));
        }

        #region Helpers/Loaders
        private void LoadTouristPoints(Region region)
        {
            TouristPoint touristPoint1 = new TouristPoint()
            {
                Id = 0,
                Name = "Punta Del Este",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 1, Data = "dsfagfdsg" },
                RegionId = region.Id,
                Region = region,
            };
            touristPoint1.AddCategory(new Category() { Id = 1, Name = "Playas" });

            TouristPoint touristPoint2 = new TouristPoint()
            {
                Id = 1,
                Name = "Cabo Polonio",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 2, Data = "htryjtyjytjt" },
                RegionId = region.Id,
                Region = region,
            };
            touristPoint2.AddCategory(new Category() { Id = 3, Name = "Reservado" });

            region.TouristPoints.Add(touristPoint1);
            region.TouristPoints.Add(touristPoint2);
        }
        #endregion
    }
}

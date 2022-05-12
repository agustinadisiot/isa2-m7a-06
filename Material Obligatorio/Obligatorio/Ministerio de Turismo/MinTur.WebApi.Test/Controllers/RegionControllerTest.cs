using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.WebApi.Controllers;
using MinTur.Models.Out;
using System.Collections.Generic;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class RegionControllerTest
    {
        private List<Region> _regions;
        private List<RegionBasicInfoModel> _regionModels;
        private List<TouristPoint> _touristPoints;
        private List<TouristPointBasicInfoModel> _touristPointModels;
        private Mock<IRegionManager> _regionManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp() 
        {
            _regions = new List<Region>();
            _regionModels = new List<RegionBasicInfoModel>();
            _touristPoints = new List<TouristPoint>();
            _touristPointModels = new List<TouristPointBasicInfoModel>();
            _regionManagerMock = new Mock<IRegionManager>(MockBehavior.Strict);

            LoadRegions();
            LoadRegionModels();
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
        private void LoadRegionModels() 
        {
            foreach(Region region in _regions) 
            {
                _regionModels.Add(new RegionBasicInfoModel(region));
            }
        }
        #endregion

        [TestMethod]
        public void GetRegionsOKTest() 
        {
            _regionManagerMock.Setup(r => r.GetAllRegions()).Returns(_regions);
            RegionController regionController = new RegionController(_regionManagerMock.Object);

            IActionResult result = regionController.GetAll();
            OkObjectResult okResult = result as OkObjectResult;
            List<RegionBasicInfoModel> responseModel = okResult.Value as List<RegionBasicInfoModel>;

            _regionManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(responseModel, _regionModels);
        }

        [TestMethod]
        public void GetSpecificRegionOKTest()
        {
            Region returnedRegion = new Region() { Id = 2, Name = "Metropolitana" };

            _regionManagerMock.Setup(r => r.GetRegionById(It.IsAny<int>())).Returns(returnedRegion);
            RegionController regionController = new RegionController(_regionManagerMock.Object);

            IActionResult result = regionController.GetSpecificRegion(returnedRegion.Id);
            OkObjectResult okResult = result as OkObjectResult;
            RegionBasicInfoModel responseModel = okResult.Value as RegionBasicInfoModel;

            _regionManagerMock.VerifyAll();
            Assert.IsTrue(responseModel.Equals(new RegionBasicInfoModel(returnedRegion)));
        }

        #region Helpers/Loaders
        private void LoadTouristPoints(Region region)
        {
            TouristPoint touristPoint1 = new TouristPoint()
            {
                Id = 0,
                Name = "Punta Del Este",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 1, Data = "wqdwqgreghwqeqw" },
                RegionId = region.Id,
                Region = region,
            };
            touristPoint1.AddCategory(new Category() { Id = 1, Name = "Playas" });

            TouristPoint touristPoint2 = new TouristPoint()
            {
                Id = 1,
                Name = "Cabo Polonio",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 2, Data = "gfdhthtrjy" },
                RegionId = region.Id,
                Region = region,
            };
            touristPoint2.AddCategory(new Category() { Id = 3, Name = "Reservado" });

            region.TouristPoints.Add(touristPoint1);
            region.TouristPoints.Add(touristPoint2);

            _touristPoints.Add(touristPoint1);
            _touristPoints.Add(touristPoint2);
        }
        private void LoadTouristPointsModels()
        {
            foreach (TouristPoint touristPoint in _touristPoints)
            {
                _touristPointModels.Add(new TouristPointBasicInfoModel(touristPoint));
            }
        }
        #endregion

    }
}

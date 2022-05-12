using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MinTur.BusinessLogicInterface.ResourceManagers;
using MinTur.Domain.BusinessEntities;
using MinTur.Models.Out;
using System.Collections.Generic;
using MinTur.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using MinTur.Models.In;
using MinTur.Domain.SearchCriteria;

namespace MinTur.WebApi.Test.Controllers
{
    [TestClass]
    public class TouristPointControllerTest
    {
        private List<TouristPoint> _touristPoints;
        private List<TouristPointBasicInfoModel> _touristPointsModel;
        private Mock<ITouristPointManager> _touristPointManagerMock;

        #region SetUp
        [TestInitialize]
        public void SetUp()
        {
            _touristPoints = new List<TouristPoint>();
            _touristPointsModel = new List<TouristPointBasicInfoModel>();
            _touristPointManagerMock = new Mock<ITouristPointManager>(MockBehavior.Strict);

            LoadTouristPoints();
            LoadTouristPointsModels();
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
                Image = new Image() { Id = 1, Data = "sadfawgewrsge" },
                RegionId = region1.Id,
                Region = region1,
            };
            touristPoint1.AddCategory(new Category() { Id = 1, Name = "Playas" });

            TouristPoint touristPoint2 = new TouristPoint()
            {
                Id = 1,
                Name = "Cabo Polonio",
                Description = "Donde el lujo y la naturaleza convergen: Punta del Este es reconocido internacionalmente como...",
                Image = new Image() { Id = 2, Data = "gfdhthtrjy" },
                RegionId = region2.Id,
                Region = region2,
            };
            touristPoint2.AddCategory(new Category() { Id = 3, Name = "Reservado" });

        }
        private void LoadTouristPointsModels()
        {
            foreach (TouristPoint touristPoint in _touristPoints)
            {
                _touristPointsModel.Add(new TouristPointBasicInfoModel(touristPoint));
            }
        }
        #endregion

        [TestMethod]
        public void GetAllTouristPointsOKTest()
        {
            _touristPointManagerMock.Setup(t => t.GetAllTouristPointsByMatchingCriteria(It.IsAny<TouristPointSearchCriteria>())).Returns(_touristPoints);
            TouristPointController touristPointController = new TouristPointController(_touristPointManagerMock.Object);

            IActionResult result = touristPointController.GetAll(new TouristPointSearchModel());
            OkObjectResult okResult = result as OkObjectResult;
            List<TouristPointBasicInfoModel> responseModel = okResult.Value as List<TouristPointBasicInfoModel>;

            _touristPointManagerMock.VerifyAll();
            CollectionAssert.AreEquivalent(responseModel, _touristPointsModel);
        }

        [TestMethod]
        public void GetSpecificTouristPointOKTest()
        {
            TouristPoint returnedTouristPoint = CreateTouristPoint();

            _touristPointManagerMock.Setup(c => c.GetTouristPointById(It.IsAny<int>())).Returns(returnedTouristPoint);
            TouristPointController touristPointController = new TouristPointController(_touristPointManagerMock.Object);

            IActionResult result = touristPointController.GetSpecificTouristPoint(returnedTouristPoint.Id);
            OkObjectResult okResult = result as OkObjectResult;
            TouristPointBasicInfoModel responseModel = okResult.Value as TouristPointBasicInfoModel;

            _touristPointManagerMock.VerifyAll();
            Assert.IsTrue(responseModel.Equals(new TouristPointBasicInfoModel(returnedTouristPoint)));
        }

        [TestMethod]
        public void CreateTouristPointCreatedAtTest()
        {
            TouristPointIntentModel touristPointIntentModel = CreateTouristPointIntentModel();
            TouristPoint expectedTouristPoint = CreateTouristPoint();

            _touristPointManagerMock.Setup(t => t.RegisterTouristPoint(touristPointIntentModel.ToEntity())).Returns(expectedTouristPoint);
            TouristPointController touristPointController = new TouristPointController(_touristPointManagerMock.Object);

            IActionResult result = touristPointController.CreateTouristPoint(touristPointIntentModel);
            CreatedResult createdResult = result as CreatedResult;

            _touristPointManagerMock.VerifyAll();
            Assert.IsTrue(createdResult.StatusCode == StatusCodes.Status201Created);
            Assert.AreEqual(new TouristPointBasicInfoModel(expectedTouristPoint), createdResult.Value);
        }

        #region Helpers
        public TouristPointIntentModel CreateTouristPointIntentModel()
        {
            return new TouristPointIntentModel()
            {
                Name = "Punta del este",
                Description = "Descripcion...",
                RegionId = 3,
                Image = "jsafigrehgeruhwdjiw",
                CategoriesId = new List<int>() { 1, 3, 84 }
            };
        }
        public TouristPoint CreateTouristPoint()
        {
            return new TouristPoint()
            {
                Id = 1,
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
        #endregion
    }
}